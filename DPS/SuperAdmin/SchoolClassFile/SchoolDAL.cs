using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace DPS.SuperAdmin.SchoolClassFile
{
    public class SchoolDAL
    {
        private string _connectionString;

        public SchoolDAL()
        {
            // Retrieve the connection string from the web.config file
            _connectionString = ConfigurationManager.ConnectionStrings["SchoolMasterDb"].ConnectionString;
        }
        public DataTable GetAllStates()
        {
            DataTable dt = new DataTable();
            try
            {
                // Instantiate SqlConnection with the connection string
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Instantiate SqlCommand with the stored procedure name
                    using (SqlCommand command = new SqlCommand("GET_ALL_STATES", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Instantiate SqlDataAdapter to fill the DataTable
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            // Fill the DataTable with the result of the stored procedure
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while retrieving all states.", ex);
            }

            return dt;
        }

        // Method to get all schools and return as a DataTable
        public DataTable GetAllSchools()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GET_ALL_SCHOOLS", connection))
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

        // Method to get all active schools and return as a DataTable
        public DataTable GetAllActiveSchools()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GET_ALL_ACTIVE_SCHOOLS", connection))
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

        // Method to get a school by ID and return as a DataTable
        public DataTable GetSchoolById(int id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GET_SCHOOL_BY_ID", connection))
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

        public DataTable GetSchoolByStateId(int id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GET_SCHOOL_BY_STATE_ID", connection))
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
        public DataTable GetSchoolDetailsByEmail(string emailid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetSchoolDetailsByEmail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", emailid);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public int UpdatePasswordAttemptsByEmail(string emailId, int passwordAttempts, string updatedBy)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdatePasswordAttemptsByEmail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmailID", emailId);
                    command.Parameters.AddWithValue("@PasswordAttempts", passwordAttempts);
                    command.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
        public int UpdatePasswordLinkVisitedByEmail(string emailId, bool passwordlinkVisited, string updatedBy)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdatePasswordLinkVisitedByEmail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmailID", emailId);
                    command.Parameters.AddWithValue("@PasswordLinkVisited", passwordlinkVisited);
                    command.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                    connection.Open();
                    
                    int rowaffected = command.ExecuteNonQuery();
                    return rowaffected;
                }
            }
        }
        public int UpdateNewPassword(string emailId, string passwordHashKey,string passwordSaltKey, string updatedBy)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdateNewPassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@emailId", emailId);
                    command.Parameters.AddWithValue("@passwordHashKey", passwordHashKey);
                    command.Parameters.AddWithValue("@passwordSaltKey", passwordSaltKey);
                    command.Parameters.AddWithValue("@updatedBy", updatedBy);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Method to add a new school and return the number of rows affected
        public int AddSchool(SchoolMaster school)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("ADD_SCHOOL", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", school.Name);
                    command.Parameters.AddWithValue("@Address", school.Address);
                    command.Parameters.AddWithValue("@City", school.City);
                    command.Parameters.AddWithValue("@IdState", school.IdState);
                    command.Parameters.AddWithValue("@Country", school.Country);
                    command.Parameters.AddWithValue("@PhoneNumber", school.PhoneNumber);
                    command.Parameters.AddWithValue("@Pincode", school.Pincode);
                    command.Parameters.AddWithValue("@EmailId", school.EmailId);
                    command.Parameters.AddWithValue("@Logo", school.Logo);
                    command.Parameters.AddWithValue("@IdDatabase", school.IdDatabase);
                    command.Parameters.AddWithValue("@IsActive", school.IsActive);
                    command.Parameters.AddWithValue("@CreatedBy", school.CreatedBy);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Method to update an existing school and return the number of rows affected
        public int UpdateSchool(SchoolMaster school)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE_SCHOOL", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", school.Id);
                    command.Parameters.AddWithValue("@Name", school.Name);
                    command.Parameters.AddWithValue("@Address", school.Address);
                    command.Parameters.AddWithValue("@City", school.City);
                    command.Parameters.AddWithValue("@IdState", school.IdState);
                    command.Parameters.AddWithValue("@Country", school.Country);
                    command.Parameters.AddWithValue("@PhoneNumber", school.PhoneNumber);
                    command.Parameters.AddWithValue("@Pincode", school.Pincode);
                    command.Parameters.AddWithValue("@EmailId", school.EmailId);
                    command.Parameters.AddWithValue("@Logo", school.Logo);
                    command.Parameters.AddWithValue("@IdDatabase", school.IdDatabase);
                    command.Parameters.AddWithValue("@IsActive", school.IsActive);
                    command.Parameters.AddWithValue("@UpdatedBy", school.UpdatedBy);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Method to mark a school as deleted and return the number of rows affected
        public int DeleteSchool(int id, string deletedBy)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("DELETE_SCHOOL", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@deletedBy", deletedBy);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Method to update active status for a list of school IDs
        public int UpdateSchoolActive(List<int> ids, List<bool> isActive, string updatedBy)
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
                        int actvCount = 0;
                        foreach (int id in ids)
                        {
                            using (SqlCommand command = new SqlCommand("UPDATE_SCHOOL_ACTIVE", connection, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Id", id);
                                command.Parameters.AddWithValue("@isActive", isActive[actvCount]);
                                command.Parameters.AddWithValue("@updatedBy", updatedBy);

                                rowsAffected += command.ExecuteNonQuery();
                            }
                            actvCount++;
                        }

                        // Commit the transaction if all updates succeed
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction if any update fails
                        transaction.Rollback();
                        // Log or handle the exception as needed
                        throw new ApplicationException("An error occurred while updating school records.", ex);
                    }
                }
            }

            return rowsAffected;
        }

        // Method to update school synchronization status and return the number of rows affected
        public int UpdateSchoolDbSyncronized(int id, string updatedBy)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE_SCHOOL_DB_SYNCRONIZED", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@updatedBy", updatedBy);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}