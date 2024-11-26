using DPS.SchoolAdmin.TransactionClassFile;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

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
        public DataTable GetFeeTransactionRequestById(int id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GET_FEE_TRANSACTION_REQUEST_BY_ID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@ID", id);

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
        public DataTable GetPaidFeeByScholarNo(string scholarNo)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetPaidFeeByScholarNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@ScholarNo", scholarNo);

                    connection.Open();

                    SqlDataAdapter sda = new SqlDataAdapter(command);
                    sda.Fill(dt);
                }
            }

            return dt;
        }
        public int AddFeeTransactionRequest(FeeTransactionRequest feeTransaction)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("ADD_FEE_TRANSACTION_REQUEST", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ScholarNumber", feeTransaction.ScholarNumber);
                    command.Parameters.AddWithValue("@StudentName", feeTransaction.StudentName);
                    command.Parameters.AddWithValue("@Amount", feeTransaction.Amount);
                    command.Parameters.AddWithValue("@TransactionID", feeTransaction.TransactionID);
                    command.Parameters.AddWithValue("@TransactionDate", feeTransaction.TransactionDate);
                    command.Parameters.AddWithValue("@AtomID", feeTransaction.TransactionDate);
                    command.Parameters.AddWithValue("@T1", feeTransaction.T1);
                    command.Parameters.AddWithValue("@T2", feeTransaction.T2);
                    command.Parameters.AddWithValue("@T3", feeTransaction.T3);
                    command.Parameters.AddWithValue("@T4", feeTransaction.T4);
                    command.Parameters.AddWithValue("@T5", feeTransaction.T5);
                    command.Parameters.AddWithValue("@T6", feeTransaction.T6);
                    command.Parameters.AddWithValue("@T7", feeTransaction.T7);
                    command.Parameters.AddWithValue("@T8", feeTransaction.T8);
                    command.Parameters.AddWithValue("@T9", feeTransaction.T9);
                    command.Parameters.AddWithValue("@T10", feeTransaction.T10);
                    command.Parameters.AddWithValue("@T11", feeTransaction.T11);
                    command.Parameters.AddWithValue("@T12", feeTransaction.T12);
                    command.Parameters.AddWithValue("@T13", feeTransaction.T13);
                    command.Parameters.AddWithValue("@T14", feeTransaction.T14);
                    command.Parameters.AddWithValue("@T15", feeTransaction.T15);
                    command.Parameters.AddWithValue("@T16", feeTransaction.T16);
                    command.Parameters.AddWithValue("@T17", feeTransaction.T17);
                    command.Parameters.AddWithValue("@T18", feeTransaction.T18);
                    command.Parameters.AddWithValue("@T19", feeTransaction.T19);
                    command.Parameters.AddWithValue("@T20", feeTransaction.T20);
                    command.Parameters.AddWithValue("@CreatedBy", feeTransaction.CreatedBy);

                    // Add output parameter for the new ID
                    SqlParameter outputIdParam = new SqlParameter("@NewID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    connection.Open();
                    command.ExecuteNonQuery(); // Execute the command

                    // Retrieve the new ID from the output parameter
                    return (int)outputIdParam.Value;
                }
            }
        }

        public int AddFeeTransactionValue(string textValue, string encryptedValue)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("ADD_FEE_TRANSACTION_VALUE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TextValue", textValue);
                    command.Parameters.AddWithValue("@EncryptedValue", encryptedValue);

                    connection.Open();
                    return command.ExecuteNonQuery(); // Returns the number of rows affected
                }
            }
        }
        public int AddFeeReceiptPrintOnline(DateTime receiptDt, string scholarNo)
        {
            int receiptNo;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT_FEE_RECEIPT_PRINT_ONLINE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@ReceiptDt", receiptDt);
                    command.Parameters.AddWithValue("@ScholarNo", scholarNo);

                    // Add output parameter for the generated ReceiptNo
                    SqlParameter outputParam = new SqlParameter("@ReceiptNo", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    connection.Open();
                    command.ExecuteNonQuery(); // Execute the insert command

                    // Retrieve the output value
                    receiptNo = (int)outputParam.Value;
                }
            }

            return receiptNo;
        }
        public int AddFeeTransactionOnline(FeeTransactionModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT_FEE_TRANSACTION_ONLINE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters for FeeTransactionOnline
                    command.Parameters.AddWithValue("@ReceiptNo", model.ReceiptNo);
                    command.Parameters.AddWithValue("@ReceiptDt", model.ReceiptDt);
                    command.Parameters.AddWithValue("@ScholarNo", model.ScholarNo);
                    command.Parameters.AddWithValue("@BillBookNo", model.BillBookNo);
                    command.Parameters.AddWithValue("@TotFeeAmt", model.TotFeeAmt);
                    command.Parameters.AddWithValue("@FineAmt", model.FineAmt);
                    command.Parameters.AddWithValue("@TotDisAmt", model.TotDisAmt);
                    command.Parameters.AddWithValue("@TotRecAmt", model.TotRecAmt);
                    command.Parameters.AddWithValue("@OnlineAmt", model.OnlineAmt);
                    command.Parameters.AddWithValue("@OnlineRefNo", model.OnlineRefNo);
                    command.Parameters.AddWithValue("@OnlineDt", model.OnlineDt);
                    command.Parameters.AddWithValue("@AmtInWords", model.AmtInWords);
                    command.Parameters.AddWithValue("@StudentName", model.StudentName);
                    command.Parameters.AddWithValue("@Amount", model.Amount);
                    command.Parameters.AddWithValue("@TransactionID", model.TransactionID);
                    command.Parameters.AddWithValue("@TransactionDate", model.TransactionDate);
                    command.Parameters.AddWithValue("@IsReverified", model.IsReverified);
                    command.Parameters.AddWithValue("@CreatedBy", model.ScholarNo);
                    command.Parameters.AddWithValue("@BankName", model.BankName);
                    command.Parameters.AddWithValue("@BankTransaction", model.BankTransaction);
                    command.Parameters.AddWithValue("@AtomID", model.AtomID);
                    command.Parameters.AddWithValue("@TransactionType", model.TransactionType);

                    // Add parameters for T1 to T20
                    command.Parameters.AddWithValue("@T1", model.T1);
                    command.Parameters.AddWithValue("@T2", model.T2);
                    command.Parameters.AddWithValue("@T3", model.T3);
                    command.Parameters.AddWithValue("@T4", model.T4);
                    command.Parameters.AddWithValue("@T5", model.T5);
                    command.Parameters.AddWithValue("@T6", model.T6);
                    command.Parameters.AddWithValue("@T7", model.T7);
                    command.Parameters.AddWithValue("@T8", model.T8);
                    command.Parameters.AddWithValue("@T9", model.T9);
                    command.Parameters.AddWithValue("@T10", model.T10);
                    command.Parameters.AddWithValue("@T11", model.T11);
                    command.Parameters.AddWithValue("@T12", model.T12);
                    command.Parameters.AddWithValue("@T13", model.T13);
                    command.Parameters.AddWithValue("@T14", model.T14);
                    command.Parameters.AddWithValue("@T15", model.T15);
                    command.Parameters.AddWithValue("@T16", model.T16);
                    command.Parameters.AddWithValue("@T17", model.T17);
                    command.Parameters.AddWithValue("@T18", model.T18);
                    command.Parameters.AddWithValue("@T19", model.T19);
                    command.Parameters.AddWithValue("@T20", model.T20);
                    // Add output parameter
                    SqlParameter outputParam = new SqlParameter("@RowsAffected", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    return (int)outputParam.Value; // Returns the number of rows affected
                }
            }
        }
        public int InsertFeeTransDetailOnline(List<FeeTransDetailOnline> feeDetails)
        {
            int totalRowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                foreach (var detail in feeDetails)
                {
                    using (SqlCommand command = new SqlCommand("INSERT_FEE_TRANS_DETAIL_ONLINE", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ReceiptNo", detail.ReceiptNo);
                        command.Parameters.AddWithValue("@FeeType", detail.FeeType);
                        command.Parameters.AddWithValue("@FeeName", detail.FeeName);
                        command.Parameters.AddWithValue("@PrevBalAmt", detail.PrevBalAmt);
                        command.Parameters.AddWithValue("@FeeAmt", detail.FeeAmt);
                        command.Parameters.AddWithValue("@DisAmt", detail.DisAmt);
                        command.Parameters.AddWithValue("@PaidFeeAmt", detail.PaidFeeAmt);
                        command.Parameters.AddWithValue("@FeeTypeSeqNo", detail.FeeTypeSeqNo);
                        command.Parameters.AddWithValue("@FeeHeadSeqNo", detail.FeeHeadSeqNo);

                        SqlParameter outputParam = new SqlParameter("@RowsAffected", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        command.ExecuteNonQuery();
                        totalRowsAffected += (int)outputParam.Value;
                    }
                }
            }

            return totalRowsAffected; // Return the total number of rows affected
        }
        public int? GetSerialNoByFeeName(string feeName)
        {
            int? serialNo = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetSerialNoByFeeName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FeeName", feeName);

                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        serialNo = (int)result;
                    }
                }
            }

            return serialNo;
        }
        public int? GetOrderNoByFeeType(string feeType)
        {
            int? orderNo = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetOrderNoByFeeType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FeeType", feeType);

                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        orderNo = (int)result;
                    }
                }
            }

            return orderNo;
        }

    }
}