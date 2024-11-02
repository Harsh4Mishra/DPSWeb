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
    public partial class StudentSync : System.Web.UI.Page
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
                    // Define the main directory and subdirectory
                    string mainDir = Server.MapPath("~/Databases");
                    string subDir = Path.Combine(mainDir, database); // Replace with your desired subdirectory name
                    /*string mdfFilePath = Path.Combine(subDir, fuDatabase.FileName ); */// Define the path for the .mdf file
                    string fileName = Path.GetFileName(fuDatabase.FileName);
                    string extension = Path.GetExtension(fileName);
                    string mdfFilePath = Path.Combine(subDir, fileName);


                    //string extension = Path.GetExtension(fuDatabase.FileName);
                    if (extension.Equals(".mdb", StringComparison.OrdinalIgnoreCase))
                    {
                        // Step 1: Check if the "Databases" folder exists
                        if (!Directory.Exists(mainDir))
                        {
                            Directory.CreateDirectory(mainDir);
                        }

                        // Step 2: Check if the subdirectory exists, if not, create it
                        if (!Directory.Exists(subDir))
                        {
                            Directory.CreateDirectory(subDir);
                        }

                        // Step 3: Check if the .mdf file already exists, if yes, delete it
                        if (File.Exists(mdfFilePath))
                        {
                            File.Delete(mdfFilePath);
                        }


                        // Save the uploaded file
                        fuDatabase.SaveAs(mdfFilePath);
                        // Optionally inform the user that the upload was successful
                        //string successScript = "alert('Database uploaded successfully.');";
                        //ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);
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
                // Handle exceptions (logging, alerting the user, etc.)
                string successScript = "alert('An error occurred: " + ex.Message + "');";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);
            }
        }
        private void TransferData(string mdfFilePath, string databaseName)
        {
            string accessConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={mdfFilePath};";

            //string accessConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=your_access_file.accdb;";
            //string sqlServerConnectionString = @"Data Source=HSWW-S056\SQLEXPRESS;Initial Catalog=" + databaseName + ";Integrated Security=True;Trust Server Certificate=True;MultipleActiveResultSets=True";
            string sqlServerConnectionString = @"Data Source=DESKTOP-MB1QN8B\SQLEXPRESS;Initial Catalog=" + databaseName + ";Integrated Security=True;MultipleActiveResultSets=True";

            using (OleDbConnection accessConnection = new OleDbConnection(accessConnectionString))
            using (SqlConnection sqlConnection = new SqlConnection(sqlServerConnectionString))
            {
                accessConnection.Open();
                sqlConnection.Open();

                List<string> tablesName = new List<string>();

                tablesName.Add("AreaMaster");
                tablesName.Add("ClassMaster");
                tablesName.Add("ClassSectionAllotment");
                tablesName.Add("FeeGroup");
                tablesName.Add("FeeMonth");
                tablesName.Add("FeeNameMaster");
                tablesName.Add("FeeParameter");
                tablesName.Add("FeeReceiptPrint");
                tablesName.Add("FeeStructure");
                tablesName.Add("FeeTransaction");
                tablesName.Add("FeeTransDetail");
                tablesName.Add("FeeTypeMaster");
                tablesName.Add("StudentConveyanceEntry");
                tablesName.Add("StudentFeeType");
                tablesName.Add("StudentFeeWriteOff");
                tablesName.Add("StudentMaster");
                tablesName.Add("studentstatus");

                // Truncate each table before bulk inserting data
                foreach (string tableName in tablesName)
                {
                    TruncateTable(sqlConnection, tableName);
                }
                //DataTable tables = accessConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                foreach (string row in tablesName)
                {
                    string tableName = row.ToString();
                    DataTable schemaTable = accessConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName, null });

                    BulkInsertDataIntoSqlServer(accessConnection, sqlConnection, tableName);


                }
                // Insert data from SyncMaster after all tables have been processed
                InsertSelectedData(sqlConnection);
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
            catch(Exception ex)
            {

            }
        }
        private void InsertSelectedData(SqlConnection sqlConnection)
        {
            string insertQuery = @"update [SyncMaster] set LastStudentSyncOn=GETDATE()";

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