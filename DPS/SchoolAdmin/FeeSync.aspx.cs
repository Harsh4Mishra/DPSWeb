using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPS.SchoolAdmin
{
    public partial class FeeSync : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 4: Check if a file is uploaded and save it
                if (fuDatabase.HasFile)
                {
                    string database = Session["databaseName"].ToString();
                    string mainDir = Server.MapPath("~/Databases");
                    string subDir = Path.Combine(mainDir, database);
                    string fileName = Path.GetFileName(fuDatabase.FileName);
                    string extension = Path.GetExtension(fileName);
                    string mdfFilePath = Path.Combine(subDir, fileName);

                    if (extension.Equals(".mdb", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!Directory.Exists(mainDir))
                        {
                            Directory.CreateDirectory(mainDir);
                        }

                        if (!Directory.Exists(subDir))
                        {
                            Directory.CreateDirectory(subDir);
                        }

                        if (File.Exists(mdfFilePath))
                        {
                            File.Delete(mdfFilePath);
                        }

                        fuDatabase.SaveAs(mdfFilePath);
                        TransferData(mdfFilePath, database);
                        string successScript = "alert('Database Syncronized Successfully');";
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);
                    }
                    else
                    {
                        string successScript = "alert('Please upload a .mdb file only.');";
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);
                    }
                }
            }
            catch (Exception ex)
            {
                string successScript = "alert('An error occurred: " + ex.Message + "');";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);
            }
        }

        private void TransferData(string mdfFilePath, string databaseName)
        {
            int lastInsertedReceiptNo = 0;
            string accessConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={mdfFilePath};";
            string sqlServerConnectionString = @"Data Source=DESKTOP-MB1QN8B\SQLEXPRESS;Initial Catalog="+databaseName+ ";Integrated Security=True;MultipleActiveResultSets=True;;Connect Timeout=120;";
            //string sqlServerConnectionString = @"Data Source=150.242.203.229;Initial Catalog="+databaseName+";User Id=dpsuser;Password=dps@123;Integrated Security=False;MultipleActiveResultSets=True";

            using (OleDbConnection accessConnection = new OleDbConnection(accessConnectionString))
            using (SqlConnection sqlConnection = new SqlConnection(sqlServerConnectionString))
            {
                accessConnection.Open();
                sqlConnection.Open();

                long lastReceiptNo = GetLastReceiptNo(accessConnection);
                string lastTransactionIDSync = GetLastTransactionIDSync(sqlConnection);

                string selectSql = "SELECT ReceiptNo, ReceiptDt, ScholarNo FROM FeeReceiptPrintOnline";
                if (!string.IsNullOrWhiteSpace(lastTransactionIDSync))
                {
                    selectSql += " WHERE ReceiptNo > " + lastInsertedReceiptNo;
                }

                // Use SqlDataAdapter to fetch data into a DataTable
                DataTable receiptTable = new DataTable();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectSql, sqlConnection))
                {
                    dataAdapter.Fill(receiptTable);
                }

                foreach (DataRow row in receiptTable.Rows)
                {
                    long incrementedReceiptNo = ++lastReceiptNo;  // Increment receipt number
                    string onlineReceiptNumber = row["ReceiptNo"].ToString();
                    string receiptDt = row["ReceiptDt"].ToString();
                    string scholarNo = row["ScholarNo"].ToString();

                    string insertSql = "INSERT INTO FeeReceiptPrint (OnlineReceiptNumber, ReceiptDt, ReceiptNo, ScholarNo) " +
                                       "VALUES (?, ?, ?, ?)";

                    using (OleDbCommand insertCommand = new OleDbCommand(insertSql, accessConnection))
                    {
                        insertCommand.Parameters.AddWithValue("?", int.Parse(onlineReceiptNumber));
                        insertCommand.Parameters.AddWithValue("?", Convert.ToDateTime(receiptDt).Date);
                        insertCommand.Parameters.AddWithValue("?", incrementedReceiptNo);
                        insertCommand.Parameters.AddWithValue("?", scholarNo);
                        insertCommand.ExecuteNonQuery();
                    }

                    // Call to the existing InsertFeeTransaction method
                    InsertFeeTransaction(sqlConnection, accessConnection, incrementedReceiptNo.ToString(), onlineReceiptNumber);
                    lastInsertedReceiptNo = int.Parse(onlineReceiptNumber);
                }

                if (lastInsertedReceiptNo != 0)
                {
                    InsertSelectedData(sqlConnection, lastInsertedReceiptNo);
                }






                List<string> tablesName = new List<string>();
                tablesName.Add("FeeReceiptPrint");
                tablesName.Add("FeeTransaction");
                tablesName.Add("FeeTransDetail");

                // Truncate each table before bulk inserting data
                foreach (string tableName in tablesName)
                {
                    TruncateTable(sqlConnection, tableName);
                }
                //DataTable tables = accessConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                foreach (string row in tablesName)
                {
                    string tableName = row.ToString();
                    BulkInsertDataIntoSqlServer(accessConnection, sqlConnection, tableName);
                }

            }
        }

        private void TruncateTable(SqlConnection sqlConnection, string tableName)
        {
            string truncateQuery = $"TRUNCATE TABLE [{tableName}]";
            using (SqlCommand truncateCommand = new SqlCommand(truncateQuery, sqlConnection))
            {
                truncateCommand.ExecuteNonQuery();
            }
        }
        private void BulkInsertDataIntoSqlServer(OleDbConnection accessConnection, SqlConnection sqlConnection, string tableName)
        {
            try
            {
                string selectQuery = $"SELECT * FROM [{tableName}]";

                using (OleDbCommand selectCommand = new OleDbCommand(selectQuery, accessConnection))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand))
                using (DataTable dataTable = new DataTable())
                {
                    adapter.Fill(dataTable); // Fill the DataTable with Access data

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
                    {
                        bulkCopy.DestinationTableName = tableName;

                        // Map columns if necessary
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                        }

                        // Perform bulk insert
                        bulkCopy.WriteToServer(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private long GetLastReceiptNo(OleDbConnection accessConnection)
        {
            string query = "SELECT MAX(ReceiptNo) AS LastReceiptNo FROM FeeReceiptPrint";
            using (OleDbCommand command = new OleDbCommand(query, accessConnection))
            {
                object result = command.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToInt64(result) : 0;
            }
        }

        private string GetLastTransactionIDSync(SqlConnection sqlConnection)
        {
            string query = "SELECT TOP 1 LastTransactionIDSync FROM [SyncMaster] ORDER BY ID DESC";
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                object result = command.ExecuteScalar();
                return result != DBNull.Value ? result.ToString() : string.Empty;
            }
        }

        public void InsertFeeTransaction(SqlConnection sqlConnection, OleDbConnection accessConnection, string incrementedReceiptNumber,string onlineRecNo)
        {
            string transactionSql = "SELECT * FROM FeeTransactionOnline WHERE ReceiptNo = @ReceiptNo";

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(transactionSql, sqlConnection))
            {
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ReceiptNo", onlineRecNo);
                DataTable transactionTable = new DataTable();
                dataAdapter.Fill(transactionTable);

                string insertTransactionSql = "INSERT INTO FeeTransaction (AmtInWords, BankAcTag, BillBookNo, CancelDate, " +
                                               "CancelReceipt, CancelTime, CashAcTag, CashRecAmt, ChequeAmt, ChequeDt, " +
                                               "ChequeNo, CreatedOn, CreatedTime, Data1, Data10, Data2, Data3, Data4, " +
                                               "Data5, Data6, Data7, Data8, Data9, FineAmt, Narration, OnlineAmt, " +
                                               "OnlineDt, OnlineRefNo, ReceiptDt, ReceiptNo, ScholarNo, TotDisAmt, " +
                                               "TotFeeAmt, TotRecAmt, UserId, VoucherTag) " +
                                               "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                using (OleDbCommand insertTransactionCommand = new OleDbCommand(insertTransactionSql, accessConnection))
                {
                    // Define parameters explicitly with appropriate types
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // AmtInWords
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // BankAcTag
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // BillBookNo
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Date);     // CancelDate
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Boolean);  // CancelReceipt
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Date);     // CancelTime
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // CashAcTag
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Numeric);   // CashRecAmt
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Numeric);   // ChequeAmt
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Date);     // ChequeDt
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // ChequeNo
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Date);     // CreatedOn
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Date);     // CreatedTime
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // Data1
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Boolean);  // Data10
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // Data2
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // Data3
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // Data4
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // Data5
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // Data6
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Numeric);   // Data7
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Numeric);   // Data8
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Numeric);   // Data9
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Numeric);   // FineAmt
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // Narration
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Numeric);   // OnlineAmt
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Date);     // OnlineDt
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // OnlineRefNo
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Date);     // ReceiptDt
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Numeric);   // ReceiptNo
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // ScholarNo
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Numeric);   // TotDisAmt
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Numeric);   // TotFeeAmt
                    insertTransactionCommand.Parameters.Add("?", OleDbType.Numeric);   // TotRecAmt
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // UserId
                    insertTransactionCommand.Parameters.Add("?", OleDbType.VarChar);  // VoucherTag

                    if (accessConnection.State != ConnectionState.Open)
                    {
                        accessConnection.Open();
                    }

                    foreach (DataRow row in transactionTable.Rows)
                    {
                        // Validate and assign values
                        try
                        {
                            insertTransactionCommand.Parameters[0].Value = row["AmtInWords"] == DBNull.Value ? (object)DBNull.Value : row["AmtInWords"].ToString();
                            insertTransactionCommand.Parameters[1].Value = row["BankAcTag"] == DBNull.Value ? (object)DBNull.Value : row["BankAcTag"].ToString();
                            insertTransactionCommand.Parameters[2].Value = row["BillBookNo"] == DBNull.Value ? (object)DBNull.Value : row["BillBookNo"].ToString();
                            insertTransactionCommand.Parameters[3].Value = row["CancelDate"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDateTime(row["CancelDate"]).Date;
                            insertTransactionCommand.Parameters[4].Value = row["CancelReceipt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToBoolean(row["CancelReceipt"]);
                            insertTransactionCommand.Parameters[5].Value = row["CancelTime"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDateTime(row["CancelTime"]).Date;
                            insertTransactionCommand.Parameters[6].Value = row["CashAcTag"] == DBNull.Value ? (object)DBNull.Value : row["CashAcTag"].ToString();
                            insertTransactionCommand.Parameters[7].Value = row["CashRecAmt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["CashRecAmt"]);
                            insertTransactionCommand.Parameters[8].Value = row["ChequeAmt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["ChequeAmt"]);
                            insertTransactionCommand.Parameters[9].Value = row["ChequeDt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDateTime(row["ChequeDt"]).Date;
                            insertTransactionCommand.Parameters[10].Value = row["ChequeNo"] == DBNull.Value ? (object)DBNull.Value : row["ChequeNo"].ToString();
                            insertTransactionCommand.Parameters[11].Value = row["CreatedOn"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDateTime(row["CreatedOn"]).Date;
                            insertTransactionCommand.Parameters[12].Value = row["CreatedTime"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDateTime(row["CreatedTime"]).Date;
                            insertTransactionCommand.Parameters[13].Value = row["Data1"] == DBNull.Value ? (object)DBNull.Value : row["Data1"].ToString();
                            insertTransactionCommand.Parameters[14].Value = row["Data10"] == DBNull.Value ? (object)DBNull.Value : Convert.ToBoolean(row["Data10"]);
                            insertTransactionCommand.Parameters[15].Value = row["Data2"] == DBNull.Value ? (object)DBNull.Value : row["Data2"].ToString();
                            insertTransactionCommand.Parameters[16].Value = row["Data3"] == DBNull.Value ? (object)DBNull.Value : row["Data3"].ToString();
                            insertTransactionCommand.Parameters[17].Value = row["Data4"] == DBNull.Value ? (object)DBNull.Value : row["Data4"].ToString();
                            insertTransactionCommand.Parameters[18].Value = row["Data5"] == DBNull.Value ? (object)DBNull.Value : row["Data5"].ToString();
                            insertTransactionCommand.Parameters[19].Value = row["Data6"] == DBNull.Value ? (object)DBNull.Value : row["Data6"].ToString();
                            insertTransactionCommand.Parameters[20].Value = row["Data7"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["Data7"]);
                            insertTransactionCommand.Parameters[21].Value = row["Data8"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["Data8"]);
                            insertTransactionCommand.Parameters[22].Value = row["Data9"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["Data9"]);
                            insertTransactionCommand.Parameters[23].Value = row["FineAmt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["FineAmt"]);
                            insertTransactionCommand.Parameters[24].Value = row["Narration"] == DBNull.Value ? (object)DBNull.Value : row["Narration"].ToString();
                            insertTransactionCommand.Parameters[25].Value = row["OnlineAmt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["OnlineAmt"]);
                            insertTransactionCommand.Parameters[26].Value = row["OnlineDt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDateTime(row["OnlineDt"]).Date;
                            insertTransactionCommand.Parameters[27].Value = row["OnlineRefNo"] == DBNull.Value ? (object)DBNull.Value : row["OnlineRefNo"].ToString();
                            insertTransactionCommand.Parameters[28].Value = row["ReceiptDt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDateTime(row["ReceiptDt"]).Date;
                            insertTransactionCommand.Parameters[29].Value = incrementedReceiptNumber; // ReceiptNo
                            insertTransactionCommand.Parameters[30].Value = row["ScholarNo"] == DBNull.Value ? (object)DBNull.Value : row["ScholarNo"].ToString();
                            insertTransactionCommand.Parameters[31].Value = row["TotDisAmt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["TotDisAmt"]);
                            insertTransactionCommand.Parameters[32].Value = row["TotFeeAmt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["TotFeeAmt"]);
                            insertTransactionCommand.Parameters[33].Value = row["TotRecAmt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["TotRecAmt"]);
                            insertTransactionCommand.Parameters[34].Value = row["UserId"] == DBNull.Value ? (object)DBNull.Value : row["UserId"].ToString();
                            insertTransactionCommand.Parameters[35].Value = row["VoucherTag"] == DBNull.Value ? (object)DBNull.Value : row["VoucherTag"].ToString();

                           

                            // Execute insert command
                            insertTransactionCommand.ExecuteNonQuery();

                            // Call InsertFeeTransDetail
                            InsertFeeTransDetail(sqlConnection, accessConnection, incrementedReceiptNumber,onlineRecNo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error inserting row: {ex.Message}");
                        }
                    }
                }

            }
        }
        private static void InsertFeeTransDetail(SqlConnection sqlConnection, OleDbConnection accessConnection, string incrementedReceiptNo, string receiptNo)
        {
            string detailSql = "SELECT * FROM FeeTransDetailOnline WHERE ReceiptNo = @ReceiptNo";

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(detailSql, sqlConnection))
            {
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ReceiptNo", receiptNo);
                DataTable detailTable = new DataTable();
                dataAdapter.Fill(detailTable);

                string insertDetailSql = "INSERT INTO FeeTransDetail (Data1, Data2, Data3, Data4, Data5, Data6, DisAmt, FeeAmt, " +
                                          "FeeHeadSeqNo, FeeName, FeeType, FeeTypeSeqNo, PaidFeeAmt, PrevBalAmt, ReceiptNo) " +
                                          "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                using (OleDbCommand insertDetailCommand = new OleDbCommand(insertDetailSql, accessConnection))
                {
                    // Define parameters with appropriate types
                    insertDetailCommand.Parameters.Add("?", OleDbType.VarChar);  // Data1
                    insertDetailCommand.Parameters.Add("?", OleDbType.VarChar);  // Data2
                    insertDetailCommand.Parameters.Add("?", OleDbType.VarChar);  // Data3
                    insertDetailCommand.Parameters.Add("?", OleDbType.Numeric);   // Data4
                    insertDetailCommand.Parameters.Add("?", OleDbType.Numeric);   // Data5
                    insertDetailCommand.Parameters.Add("?", OleDbType.Numeric);   // Data6
                    insertDetailCommand.Parameters.Add("?", OleDbType.Numeric);   // DisAmt
                    insertDetailCommand.Parameters.Add("?", OleDbType.Numeric);   // FeeAmt
                    insertDetailCommand.Parameters.Add("?", OleDbType.Numeric);   // FeeHeadSeqNo
                    insertDetailCommand.Parameters.Add("?", OleDbType.VarChar);   // FeeName
                    insertDetailCommand.Parameters.Add("?", OleDbType.VarChar);   // FeeType
                    insertDetailCommand.Parameters.Add("?", OleDbType.Numeric);   // FeeTypeSeqNo
                    insertDetailCommand.Parameters.Add("?", OleDbType.Numeric);   // PaidFeeAmt
                    insertDetailCommand.Parameters.Add("?", OleDbType.Numeric);   // PrevBalAmt
                    insertDetailCommand.Parameters.Add("?", OleDbType.Numeric);   // ReceiptNo

                    if (accessConnection.State != ConnectionState.Open)
                    {
                        accessConnection.Open();
                    }

                    foreach (DataRow row in detailTable.Rows)
                    {
                        try
                        {
                            insertDetailCommand.Parameters[0].Value = row["Data1"] == DBNull.Value ? (object)DBNull.Value : row["Data1"].ToString();
                            insertDetailCommand.Parameters[1].Value = row["Data2"] == DBNull.Value ? (object)DBNull.Value : row["Data2"].ToString();
                            insertDetailCommand.Parameters[2].Value = row["Data3"] == DBNull.Value ? (object)DBNull.Value : row["Data3"].ToString();
                            insertDetailCommand.Parameters[3].Value = row["Data4"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["Data4"]);
                            insertDetailCommand.Parameters[4].Value = row["Data5"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["Data5"]);
                            insertDetailCommand.Parameters[5].Value = row["Data6"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["Data6"]);
                            insertDetailCommand.Parameters[6].Value = row["DisAmt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["DisAmt"]);
                            insertDetailCommand.Parameters[7].Value = row["FeeAmt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["FeeAmt"]);
                            insertDetailCommand.Parameters[8].Value = row["FeeHeadSeqNo"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["FeeHeadSeqNo"]);
                            insertDetailCommand.Parameters[9].Value = row["FeeName"] == DBNull.Value ? (object)DBNull.Value : row["FeeName"].ToString();
                            insertDetailCommand.Parameters[10].Value = row["FeeType"] == DBNull.Value ? (object)DBNull.Value : row["FeeType"].ToString();
                            insertDetailCommand.Parameters[11].Value = row["FeeTypeSeqNo"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["FeeTypeSeqNo"]);
                            insertDetailCommand.Parameters[12].Value = row["PaidFeeAmt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["PaidFeeAmt"]);
                            insertDetailCommand.Parameters[13].Value = row["PrevBalAmt"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["PrevBalAmt"]);
                            insertDetailCommand.Parameters[14].Value = incrementedReceiptNo;  // Using the passed receipt number

                            // Execute insert command
                            insertDetailCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error inserting row: {ex.Message}");
                        }
                    }
                }
            }
        }
        private void InsertSelectedData(SqlConnection sqlConnection,int lastid)
        {
            string insertQuery = @"update [SyncMaster] set LastTransactionSyncOn=GETDATE(),LastTransactionIDSync="+lastid;

            using (SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection))
            {
                insertCommand.ExecuteNonQuery();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string database = Session["databaseName"].ToString();
            // Define the main directory and subdirectory
            string mainDir = Server.MapPath("~/Databases");
            string subDir = Path.Combine(mainDir, database); // Subdirectory name
            string fileName = "DpsSms.mdb"; // Set your actual file name here
            string mdbFilePath = Path.Combine(subDir, fileName);

            if (File.Exists(mdbFilePath))
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream"; // Set content type for binary file
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.TransmitFile(mdbFilePath);
                Response.End();
            }
            else
            {
                // Handle the case where the file does not exist
                Response.Write("File not found.");
            }
        }

    }
}