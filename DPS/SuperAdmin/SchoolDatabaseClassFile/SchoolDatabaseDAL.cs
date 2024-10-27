using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DPS.SuperAdmin.SchoolDatabaseClassFile
{
    public class SchoolDatabaseDAL
    {
        private string _connectionString;

        public SchoolDatabaseDAL()
        {
            // Retrieve the connection string from the web.config file
            _connectionString = ConfigurationManager.ConnectionStrings["SchoolMasterDb"].ConnectionString;
        }

        // Method to get all schools Database and return as a DataTable
        public DataTable GetAllSchoolDatabases()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GET_ALL_SCHOOL_DATABASE_MASTERS", connection))
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

        // Method to get a school Database by ID and return as a DataTable
        public DataTable GetSchoolDatabaseById(int id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GET_SCHOOL_DATABASE_MASTER_BY_ID", connection))
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

        // Method to add a new school Database and return the number of rows affected
        public int AddSchoolDatabase(int clientId, string databaseName, string createdBy,string academicYear,bool isinused)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("ADD_SCHOOL_DATABASE_MASTER", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CLIENT_ID", clientId);
                    command.Parameters.AddWithValue("@DATABASE_NAME", databaseName);
                    command.Parameters.AddWithValue("@CREATED_BY", createdBy);
                    command.Parameters.AddWithValue("@ACADEMIC_YEAR", academicYear);
                    command.Parameters.AddWithValue("@IS_IN_USED", isinused);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Method to update an existing school Database and return the number of rows affected
        public int UpdateSchoolDatabase(int id, int clientId, string databaseName, bool isActive, string updatedBy, string academicYear, bool isinused)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE_SCHOOL_DATABASE_MASTER", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@CLIENT_ID", clientId);
                    command.Parameters.AddWithValue("@DATABASE_NAME", databaseName);
                    command.Parameters.AddWithValue("@IS_ACTIVE", isActive);
                    command.Parameters.AddWithValue("@UPDATED_BY", updatedBy);
                    command.Parameters.AddWithValue("@ACADEMIC_YEAR", academicYear);
                    command.Parameters.AddWithValue("@IS_IN_USED", isinused);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Method to mark a school Database as deleted and return the number of rows affected
        public int DeleteSchoolDatabase(int id, string deletedBy)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("DELETE_SCHOOL_DATABASE_MASTER", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@DELETED_BY", deletedBy);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
        public int DeleteCurrentSchoolDatabase(int id, string deletedBy)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("DELETE_CURRENT_SCHOOL_DATABASE_MASTER", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@DELETED_BY", deletedBy);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Method to update active status for a school Database
        public int UpdateSchoolDatabaseActive(int id, bool isActive, string updatedBy)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE_SCHOOL_DATABASE_MASTER_ACTIVE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@IS_ACTIVE", isActive);
                    command.Parameters.AddWithValue("@UPDATED_BY", updatedBy);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Method to update active status for a list of school Database IDs
        public int UpdateSchoolDatabaseActive(List<int> ids, List<bool> isActive, string updatedBy)
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
                            using (SqlCommand command = new SqlCommand("UPDATE_SCHOOL_DATABASE_MASTER_ACTIVE", connection, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Id", ids[i]);
                                command.Parameters.AddWithValue("@IS_ACTIVE", isActive[i]);
                                command.Parameters.AddWithValue("@UPDATED_BY", updatedBy);

                                rowsAffected += command.ExecuteNonQuery();
                            }
                        }

                        // Commit the transaction if all updates succeed
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