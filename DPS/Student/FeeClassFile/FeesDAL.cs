using DPS.SchoolAdmin.TransactionClassFile;
using System.Data.SqlClient;
using System.Data;
using System;

namespace DPS.Student.FeeClassFile
{
    public class FeesDAL
    {
        private string _connectionString;

        public FeesDAL()
        {
            TransactionConnection transactionConnection = new TransactionConnection();
            // Retrieve the connection string from the web.config file
            _connectionString = transactionConnection.ConnectionString();//ConfigurationManager.ConnectionStrings["SchoolMasterDb"].ConnectionString;
        }
        public DataTable GetStudentDetailByScholarNo(string scholarNo)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetStudentDetailByScholarNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@ScholarNo", scholarNo);//(object)className ?? DBNull.Value
                    
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }
        public DataSet StudentFeeParameterDetail(string scholarNo)
        {
            DataSet dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("StudentFeeParameterDetail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@ScholarNo", scholarNo);

                    connection.Open();
                    
                    SqlDataAdapter sda = new SqlDataAdapter(command);
                    sda.Fill(dataSet);
                }
            }

            return dataSet;
        }

    }
}