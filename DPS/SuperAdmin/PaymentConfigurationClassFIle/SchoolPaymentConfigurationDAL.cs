using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace DPS.SuperAdmin.PaymentConfigurationClassFIle
{
    public class SchoolPaymentConfigurationDAL
    {
        private string _connectionString;

        public SchoolPaymentConfigurationDAL()
        {
            // Retrieve the connection string from the web.config file
            _connectionString = ConfigurationManager.ConnectionStrings["SchoolMasterDb"].ConnectionString;
        }

        // Method to get all active school payment configurations
        public DataTable GetAllSchoolPaymentConfigurations()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GET_ALL_SCHOOL_PAYMENT_CONFIGURATIONS", connection))
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

        // Method to get a school payment configuration by ID
        public DataTable GetSchoolPaymentConfigurationById(int id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GET_SCHOOL_PAYMENT_CONFIGURATION_BY_ID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // Method to add a new school payment configuration
        public int AddSchoolPaymentConfiguration(SchoolPaymentConfiguration config)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("ADD_SCHOOL_PAYMENT_CONFIGURATION", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CLIENT_ID", config.ClientId);
                    command.Parameters.AddWithValue("@MCC_CODE", config.MccCode);
                    command.Parameters.AddWithValue("@MERCHANT_ID", config.MerchantId);
                    command.Parameters.AddWithValue("@USER_ID", config.UserId);
                    command.Parameters.AddWithValue("@MERCHANT_PASSWORD", config.MerchantPassword);
                    command.Parameters.AddWithValue("@PRODUCT_ID", config.ProductId);
                    command.Parameters.AddWithValue("@TRANSACTION_CURRENCY", config.TransactionCurrency);
                    command.Parameters.AddWithValue("@REQUEST_AES_KEY", config.RequestAesKey);
                    command.Parameters.AddWithValue("@REQUEST_HASH_KEY", config.RequestHashKey);
                    command.Parameters.AddWithValue("@RESPONSE_AES_KEY", config.ResponseAesKey);
                    command.Parameters.AddWithValue("@RESPONSE_HASH_KEY", config.ResponseHashKey);
                    command.Parameters.AddWithValue("@HASH_ALGORITHM", config.HashAlgorithm);
                    command.Parameters.AddWithValue("@CUSTOMER_ACCOUNT_NUMBER", config.CustomerAccountNumber);
                    command.Parameters.AddWithValue("@CREATED_BY", config.CreatedBy);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Method to update an existing school payment configuration
        public int UpdateSchoolPaymentConfiguration(SchoolPaymentConfiguration config)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE_SCHOOL_PAYMENT_CONFIGURATION", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", config.Id);
                    command.Parameters.AddWithValue("@CLIENT_ID", config.ClientId);
                    command.Parameters.AddWithValue("@MCC_CODE", config.MccCode);
                    command.Parameters.AddWithValue("@MERCHANT_ID", config.MerchantId);
                    command.Parameters.AddWithValue("@USER_ID", config.UserId);
                    command.Parameters.AddWithValue("@MERCHANT_PASSWORD", config.MerchantPassword);
                    command.Parameters.AddWithValue("@PRODUCT_ID", config.ProductId);
                    command.Parameters.AddWithValue("@TRANSACTION_CURRENCY", config.TransactionCurrency);
                    command.Parameters.AddWithValue("@REQUEST_AES_KEY", config.RequestAesKey);
                    command.Parameters.AddWithValue("@REQUEST_HASH_KEY", config.RequestHashKey);
                    command.Parameters.AddWithValue("@RESPONSE_AES_KEY", config.ResponseAesKey);
                    command.Parameters.AddWithValue("@RESPONSE_HASH_KEY", config.ResponseHashKey);
                    command.Parameters.AddWithValue("@HASH_ALGORITHM", config.HashAlgorithm);
                    command.Parameters.AddWithValue("@CUSTOMER_ACCOUNT_NUMBER", config.CustomerAccountNumber);
                    command.Parameters.AddWithValue("@IS_ACTIVE", config.IsActive);
                    command.Parameters.AddWithValue("@UPDATED_BY", config.UpdatedBy);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Method to mark a school payment configuration as deleted
        public int DeleteSchoolPaymentConfiguration(int id, string deletedBy)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("DELETE_SCHOOL_PAYMENT_CONFIGURATION", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@DELETED_BY", deletedBy);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Method to update active status for a school payment configuration
        public int UpdateSchoolPaymentConfigurationActive(List<int> ids, List<bool> isActive, string updatedBy)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                // Start a transaction
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        for (int i = 0; i < ids.Count; i++)
                        {
                            using (SqlCommand command = new SqlCommand("UPDATE_SCHOOL_PAYMENT_CONFIGURATION_ACTIVE", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@Id", ids[i]);
                                command.Parameters.AddWithValue("@IsActive", isActive[i]);
                                command.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                                rowsAffected += command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Rollback the transaction if any update fails
                        transaction.Rollback();
                        throw;
                    }
                    
                }
            }
            return rowsAffected;
        }
    }
}