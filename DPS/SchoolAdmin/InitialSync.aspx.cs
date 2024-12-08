using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using DPS.Encryption;

namespace DPS.SchoolAdmin
{
    public partial class InitialSync : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {

            try
            {
                // Step 4: Check if a file is uploaded and save it
                if (fuDatabase.HasFile)
                {
                    // Ensure Session["databaseName"] is not null
                    if (Session["databaseName"] != null)
                    {
                        string database = Session["databaseName"].ToString();
                        // Define the main directory and subdirectory
                        string mainDir = Server.MapPath("~/Databases");
                       //string networkPath = ConfigurationManager.AppSettings["DatabaseNetworkPath"];
                        string subDir = Path.Combine(mainDir, database); // Replace with your desired subdirectory name
                        /*string mdfFilePath = Path.Combine(subDir, fuDatabase.FileName ); */// Define the path for the .mdf file
                        string fileName = Path.GetFileName(fuDatabase.FileName);
                        string extension = Path.GetExtension(fileName);
                        string mdfFilePath = Path.Combine(subDir, fileName);
                        //string newFilePath= "http://epay.dpserp.com/Databases/"+ database + "/"+fileName;

                        //string networkPath = ConfigurationManager.AppSettings["DatabaseNetworkPath"]; // Example: \\epay.dpserp.com\Databases
                        //if (!networkPath.EndsWith("\\"))
                        //{
                        //    networkPath += "\\";
                        //}
                        ////// Get the database name from the session
                        ////string database = Session["databaseName"].ToString();

                        //// Directly concatenate the network path with the database folder (subdirectory) and the file name
                        //string subDir2 = networkPath + "\\" + database; // Direct concatenation for network path


                        //// Directly concatenate the full file path for the .mdb file
                        //string mdfFilePath2 = subDir2 + "\\" + fileName; // Direct concatenation for file path




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
                    else
                    {
                        string successScript = "alert('Database name not found in session.');";
                        ClientScript.RegisterStartupScript(this.GetType(), "SessionError", successScript, true);
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
<<<<<<< HEAD
            string sqlServerConnectionString = @"Data Source=DESKTOP-MB1QN8B\SQLEXPRESS;Initial Catalog=" + databaseName + ";Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=120;";
            //string sqlServerConnectionString = @"Data Source=150.242.203.229;Initial Catalog=" + databaseName + ";User Id=dpsuser;Password=dps@123;Integrated Security=False;MultipleActiveResultSets=True";
=======
            //string sqlServerConnectionString = @"Server=85.25.185.85\MSSQLSERVER2017;Initial Catalog=" + databaseName + ";User Id=DPSERP;Password=Dpstech@123;MultipleActiveResultSets=True;Connect Timeout=1200;";
            string sqlServerConnectionString = @"Data Source=150.242.203.229;Initial Catalog=" + databaseName + ";User Id=dpsuser;Password=dps@123;Integrated Security=False;MultipleActiveResultSets=True;Connect Timeout=50000;";
>>>>>>> Harsh

            using (OleDbConnection accessConnection = new OleDbConnection(accessConnectionString))
            using (SqlConnection sqlConnection = new SqlConnection(sqlServerConnectionString))
            {
                accessConnection.Open();
                sqlConnection.Open();

                // Create the DatabaseDetail table if it doesn't exist
                CreateDatabaseDetailTable(accessConnection);

                // Insert a record into the DatabaseDetail table
                InsertDatabaseDetail(accessConnection, databaseName);

                // Add OnlineReceiptNumber column to FeeReceiptPrint table
                AddColumnToFeeReceiptPrint(accessConnection);

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

                //DataTable tables = accessConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                foreach (string row in tablesName)
                {
                    string tableName = row.ToString();
                    if (tableName != "MSysAccessObjects" || tableName != "MSysAccessXML")
                    {

                        //DataTable schemaTable = accessConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName, null });
                        DataTable datadt = new DataTable();
                        datadt = GetDataFromTable(accessConnection, tableName);

                        CreateSqlServerTable(sqlConnection, datadt, tableName);
                        BulkInsertDataIntoSqlServer(accessConnection, sqlConnection, tableName, datadt);

                        if (tableName == "FeeReceiptPrint" || tableName == "FeeTransaction" || tableName == "FeeTransDetail")
                        {
                            string newTableName = tableName + "Online";
                            CreateSqlServerTable(sqlConnection, datadt, newTableName);

                        }
                    }
                }
                // Insert data from SyncMaster after all tables have been processed
                InsertSelectedData(sqlConnection);
            }
        }
        private void CreateDatabaseDetailTable(OleDbConnection accessConnection)
        {
            string createTableQuery = "CREATE TABLE DatabaseDetail (DatabaseName TEXT(255))";

            using (OleDbCommand createTableCommand = new OleDbCommand(createTableQuery, accessConnection))
            {
                try
                {
                    createTableCommand.ExecuteNonQuery();
                }
                catch (OleDbException ex)
                {
                    // Handle error if the table already exists or another error occurs
                    if (ex.ErrorCode != -2146828218) // Error code for "Table already exists"
                    {
                        throw; // Rethrow the exception for further handling
                    }
                }
            }
        }
        private void InsertDatabaseDetail(OleDbConnection accessConnection, string databaseName)
        {

            UTFService utfService = new UTFService();
            string encValue = utfService.Encrypt(databaseName);

            string insertQuery = "INSERT INTO DatabaseDetail (DatabaseName) VALUES (@DatabaseName)";

            using (OleDbCommand insertCommand = new OleDbCommand(insertQuery, accessConnection))
            {
                insertCommand.Parameters.AddWithValue("@DatabaseName", encValue);
                insertCommand.ExecuteNonQuery();
            }
        }
        private void CreateSqlServerTable(SqlConnection sqlConnection, DataTable schemaTable, string tableName)
        {
            string createTableQuery = $"CREATE TABLE [{tableName}] (";


            foreach (DataColumn column in schemaTable.Columns)
            {
                string columnName = column.ColumnName;//column["COLUMN_NAME"].ToString();
                string dataType = column.DataType.Name;
               
                string sqlDataType = GetSqlDataType(dataType);
                if (tableName == "FeeReceiptPrintOnline" && columnName == "ReceiptNo")
                {
                    createTableQuery += $"[{columnName}] INT IDENTITY(50000, 1) PRIMARY KEY , ";
                }
                else
                {
                    createTableQuery += $"[{columnName}] {sqlDataType}, ";
                }
            }

            createTableQuery = createTableQuery.TrimEnd(',', ' ') + ")";

            using (SqlCommand createTableCommand = new SqlCommand(createTableQuery, sqlConnection))
            {
                createTableCommand.ExecuteNonQuery();
            }
        }
        private string GetSqlDataType(string accessDataType)
        {

            if (accessDataType == "String")
            {
                return "VARCHAR(MAX)";
            }
            else if (accessDataType == "Double")
            {
                return "Decimal(10,2)";
            }
            else if (accessDataType == "DateTime")
            {
                return "DATETIME";
            }
            else if (accessDataType == "Boolean")
            {
                return "BIT";
            }
            else if (accessDataType == "Int32" || accessDataType == "Int16")
            {
                return "INT";
            }
            else if (accessDataType == "Byte[]")
            {
                return "VARCHAR(MAX)";
            }
            else if (accessDataType == "AutoNumber")
            {
                return "INT";
            }
            else
            {
                return "NVARCHAR(255)"; // Default type
            }
        }
        private void BulkInsertDataIntoSqlServer(OleDbConnection accessConnection, SqlConnection sqlConnection, string tableName, DataTable dataTable)
        {

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
            //string selectQuery = $"SELECT * FROM [{tableName}]";

            //using (OleDbCommand selectCommand = new OleDbCommand(selectQuery, accessConnection))
            //using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand))
            //using (DataTable dataTable = new DataTable())
            //{
            //    adapter.Fill(dataTable); // Fill the DataTable with Access data


            //}
        }
        private DataTable GetDataFromTable(OleDbConnection accessConnection, string tableName)
        {
            string selectQuery = $"SELECT * FROM [{tableName}]";

            using (OleDbCommand selectCommand = new OleDbCommand(selectQuery, accessConnection))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand))
            using (DataTable dataTable = new DataTable())
            {
                adapter.Fill(dataTable); // Fill the DataTable with Access data
                return dataTable;

            }
        }
        private void InsertSelectedData(SqlConnection sqlConnection)
        {
            string insertQuery = @"INSERT INTO [SyncMaster] (InitialSyncOn) values(getdate())";

            using (SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection))
            {
                insertCommand.ExecuteNonQuery();
            }
        }
        private void AddColumnToFeeReceiptPrint(OleDbConnection accessConnection)
        {
            string alterTableQuery = "ALTER TABLE FeeReceiptPrint ADD COLUMN OnlineReceiptNumber Int";

            using (OleDbCommand alterTableCommand = new OleDbCommand(alterTableQuery, accessConnection))
            {
                try
                {
                    alterTableCommand.ExecuteNonQuery();
                }
                catch (OleDbException ex)
                {
                    // Handle error if the column already exists or another error occurs
                    if (ex.ErrorCode != -2147217904) // Error code for "Column already exists"
                    {
                        throw; // Rethrow the exception for further handling
                    }
                }
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