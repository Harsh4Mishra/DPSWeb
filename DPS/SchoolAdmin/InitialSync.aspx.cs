using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

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
        protected async void btnsave_Click(object sender, EventArgs e)
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
                        TransferData(mdfFilePath,database);
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
        private void TransferData(string mdfFilePath,string databaseName)
        {
            string accessConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={mdfFilePath};";

            //string accessConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=your_access_file.accdb;";
            string sqlServerConnectionString = @"Data Source=DESKTOP-MB1QN8B\SQLEXPRESS;Initial Catalog="+databaseName+";Integrated Security=True";

            using (OleDbConnection accessConnection = new OleDbConnection(accessConnectionString))
            using (SqlConnection sqlConnection = new SqlConnection(sqlServerConnectionString))
            {
                accessConnection.Open();
                sqlConnection.Open();

                DataTable tables = accessConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                foreach (DataRow row in tables.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();
                    DataTable schemaTable = accessConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName, null });

                    CreateSqlServerTable(sqlConnection, schemaTable, tableName);
                    BulkInsertDataIntoSqlServer(accessConnection, sqlConnection, tableName);

                    if(tableName=="FeeReceiptPrint"|| tableName=="FeeTransaction" || tableName=="FeeTransDetail")
                    {
                        string newTableName = tableName + "Online";
                        CreateSqlServerTable(sqlConnection, schemaTable, newTableName);

                    }
                }
            }
        }

        private void CreateSqlServerTable(SqlConnection sqlConnection, DataTable schemaTable, string tableName)
        {
            string createTableQuery = $"CREATE TABLE [{tableName}] (";

            foreach (DataRow column in schemaTable.Rows)
            {
                string columnName = column["COLUMN_NAME"].ToString();
                string dataType = column["DATA_TYPE"].ToString();
                string sqlDataType = GetSqlDataType(column["COLUMN_NAME"].ToString());
                if (tableName == "FeeReceiptPrintOnline" && columnName == "ReceiptNo")
                {
                    createTableQuery += $"[{columnName}] {sqlDataType} IDENTITY(50000, 1) PRIMARY KEY , ";
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
            if (accessDataType == "Short Text")
            {
                return "VARCHAR(MAX)";
            }
            else if (accessDataType == "Date/Time")
            {
                return "DATETIME";
            }
            else if (accessDataType == "Yes/No")
            {
                return "BIT";
            }
            else if (accessDataType == "Number")
            {
                return "INT";
            }
            else if (accessDataType == "OLE Object")
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

        private void BulkInsertDataIntoSqlServer(OleDbConnection accessConnection, SqlConnection sqlConnection, string tableName)
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
    }
}