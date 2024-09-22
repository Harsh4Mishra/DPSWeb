using System;
using System.Collections.Generic;
using System.Data;

namespace DPS.SuperAdmin.SchoolDatabaseClassFile
{
    public class SchoolDatabaseBLL
    {
        // Method to get all schools
        public DataTable GetAllSchoolDatabases()
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDatabaseDAL schoolDatabaseDAL = new SchoolDatabaseDAL();
                DataTable result = schoolDatabaseDAL.GetAllSchoolDatabases();
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while retrieving all schools.", ex);
            }
        }

        // Method to get a school by ID
        public DataTable GetSchoolDatabaseById(int id)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDatabaseDAL schoolDatabaseDAL = new SchoolDatabaseDAL();
                DataTable result = schoolDatabaseDAL.GetSchoolDatabaseById(id);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred while retrieving the school with ID {id}.", ex);
            }
        }

        // Method to add a new school
        public int AddSchoolDatabase(int clientId, string databaseName, string createdBy)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDatabaseDAL schoolDatabaseDAL = new SchoolDatabaseDAL();
                int result = schoolDatabaseDAL.AddSchoolDatabase(clientId, databaseName, createdBy);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while adding the new school.", ex);
            }
        }

        // Method to update an existing school
        public int UpdateSchoolDatabase(int id, int clientId, string databaseName, bool isActive, string updatedBy)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDatabaseDAL schoolDatabaseDAL = new SchoolDatabaseDAL();
                int result = schoolDatabaseDAL.UpdateSchoolDatabase(id, clientId, databaseName, isActive, updatedBy);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while updating the school.", ex);
            }
        }

        // Method to delete a school
        public int DeleteSchoolDatabase(int id, string deletedBy)
        {
            if (string.IsNullOrWhiteSpace(deletedBy))
                throw new ArgumentException("DeletedBy cannot be null or empty", nameof(deletedBy));

            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDatabaseDAL schoolDatabaseDAL = new SchoolDatabaseDAL();
                int result = schoolDatabaseDAL.DeleteSchoolDatabase(id, deletedBy);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred while deleting the school with ID {id}.", ex);
            }
        }

        // Method to update active status for a list of school IDs
        public int UpdateSchoolDatabaseActive(List<int> ids, List<bool> isActive, string updatedBy)
        {
            if (ids == null || ids.Count == 0)
                throw new ArgumentException("IDs list cannot be null or empty", nameof(ids));

            if (string.IsNullOrWhiteSpace(updatedBy))
                throw new ArgumentException("UpdatedBy cannot be null or empty", nameof(updatedBy));

            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDatabaseDAL schoolDatabaseDAL = new SchoolDatabaseDAL();
                int result = schoolDatabaseDAL.UpdateSchoolDatabaseActive(ids, isActive, updatedBy);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while updating school active statuses.", ex);
            }
        }

    }
}