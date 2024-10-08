using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace DPS.SchoolAdmin.TransactionClassFile
{
    public class TransactionDAL
    {
        private string _connectionString;

        public TransactionDAL()
        {
            TransactionConnection transactionConnection = new TransactionConnection();
            // Retrieve the connection string from the web.config file
            _connectionString = transactionConnection.ConnectionString();//ConfigurationManager.ConnectionStrings["SchoolMasterDb"].ConnectionString;
        }
        public DataTable GetFeeTransactionSummaryOffLine(
                            string className = null,
                            string sectionName = null,
                            DateTime? fromDate = null,
                            DateTime? toDate = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetFeeTransactionSummaryOffLine", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@ClassName", (object)className ?? DBNull.Value);//(object)className ?? DBNull.Value
                    command.Parameters.AddWithValue("@SectionName", (object)sectionName ?? DBNull.Value);//(object)sectionName ?? DBNull.Value
                    command.Parameters.AddWithValue("@FromDate", (object)fromDate ?? DBNull.Value);//(object)fromDate ?? DBNull.Value
                    command.Parameters.AddWithValue("@ToDate", (object)toDate ?? DBNull.Value);//(object)toDate ?? DBNull.Value

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GetFeeTransactionSummaryONLine(
                            string className = null,
                            string sectionName = null,
                            DateTime? fromDate = null,
                            DateTime? toDate = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetFeeTransactionSummaryONLine", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@ClassName", (object)className ?? DBNull.Value);//(object)className ?? DBNull.Value
                    command.Parameters.AddWithValue("@SectionName", (object)sectionName ?? DBNull.Value);//(object)sectionName ?? DBNull.Value
                    command.Parameters.AddWithValue("@FromDate", (object)fromDate ?? DBNull.Value);//(object)fromDate ?? DBNull.Value
                    command.Parameters.AddWithValue("@ToDate", (object)toDate ?? DBNull.Value);//(object)toDate ?? DBNull.Value

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GetFeeTransactionSummaryBoth(
                            string className = null,
                            string sectionName = null,
                            DateTime? fromDate = null,
                            DateTime? toDate = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetFeeTransactionSummaryBoth", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@ClassName", (object)className ?? DBNull.Value);//(object)className ?? DBNull.Value
                    command.Parameters.AddWithValue("@SectionName", (object)sectionName ?? DBNull.Value);//(object)sectionName ?? DBNull.Value
                    command.Parameters.AddWithValue("@FromDate", (object)fromDate ?? DBNull.Value);//(object)fromDate ?? DBNull.Value
                    command.Parameters.AddWithValue("@ToDate", (object)toDate ?? DBNull.Value);//(object)toDate ?? DBNull.Value

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GetDistinctClassNames()
        {
            DataTable dt = new DataTable();
            TransactionConnection transactionConnection = new TransactionConnection();
            // Retrieve the connection string from the web.config file
            _connectionString = transactionConnection.ConnectionString();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetDistinctClassNames", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }
        public DataTable GetSectionNamesByClassName(
                            string className = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetSectionNamesByClassName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@ClassName", (object)className ?? DBNull.Value);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}