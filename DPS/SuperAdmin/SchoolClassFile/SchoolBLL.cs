using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DPS.SuperAdmin.SchoolClassFile
{
    public class SchoolBLL
    {
        // Method to get all states
        public DataTable GetAllStates()
        {
            try
            {
                // Instantiate SchoolDAL or a relevant DAL class for states
                SchoolDAL schoolDAL = new SchoolDAL(); // Assuming SchoolDAL is being used; replace if needed

                // Call the method to get all states
                DataTable result = schoolDAL.GetAllStates(); // Make sure this method is defined in SchoolDAL or replace with the correct DAL

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while retrieving all states.", ex);
            }
        }

        // Method to get all schools
        public DataTable GetAllSchools()
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDAL schoolDAL = new SchoolDAL();
                DataTable result = schoolDAL.GetAllSchools();
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while retrieving all schools.", ex);
            }
        }

        // Method to get all active schools
        public DataTable GetAllActiveSchools()
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDAL schoolDAL = new SchoolDAL();
                DataTable result = schoolDAL.GetAllActiveSchools();
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while retrieving active schools.", ex);
            }
        }

        // Method to get a school by ID
        public DataTable GetSchoolById(int id)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDAL schoolDAL = new SchoolDAL();
                DataTable result = schoolDAL.GetSchoolById(id);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred while retrieving the school with ID {id}.", ex);
            }
        }
        public DataTable GetSchoolDetailsByEmail(string emailid)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDAL schoolDAL = new SchoolDAL();
                DataTable result = schoolDAL.GetSchoolDetailsByEmail(emailid);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred while retrieving the school with emailID {emailid}.", ex);
            }
        }
        public int UpdatePasswordAttemptsByEmail(string emailId, int passwordAttempts, string updatedBy)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDAL schoolDAL = new SchoolDAL();
                int result = schoolDAL.UpdatePasswordAttemptsByEmail(emailId, passwordAttempts, updatedBy);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred while updating password attempts.{ex.Message}", ex);
            }
        }

        public int UpdatePasswordLinkVisitedByEmail(string emailId, bool passwordlinkVisited, string updatedBy)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDAL schoolDAL = new SchoolDAL();
                int result = schoolDAL.UpdatePasswordLinkVisitedByEmail(emailId, passwordlinkVisited, updatedBy);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred while updating password attempts.{ex.Message}", ex);
            }
        }

        // Method to add a new school
        public int AddSchool(SchoolMaster school)
        {
            if (school == null)
                throw new ArgumentNullException(nameof(school));

            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDAL schoolDAL = new SchoolDAL();
                int result = schoolDAL.AddSchool(school);
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
        public int UpdateSchool(SchoolMaster school)
        {
            if (school == null)
                throw new ArgumentNullException(nameof(school));

            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDAL schoolDAL = new SchoolDAL();
                int result = schoolDAL.UpdateSchool(school);
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
        public int DeleteSchool(int id, string deletedBy)
        {
            if (string.IsNullOrWhiteSpace(deletedBy))
                throw new ArgumentException("DeletedBy cannot be null or empty", nameof(deletedBy));

            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDAL schoolDAL = new SchoolDAL();
                int result = schoolDAL.DeleteSchool(id, deletedBy);
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
        public int UpdateSchoolActive(List<int> ids, List<bool> isActive, string updatedBy)
        {
            if (ids == null || ids.Count == 0)
                throw new ArgumentException("IDs list cannot be null or empty", nameof(ids));

            if (string.IsNullOrWhiteSpace(updatedBy))
                throw new ArgumentException("UpdatedBy cannot be null or empty", nameof(updatedBy));

            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDAL schoolDAL = new SchoolDAL();
                int result = schoolDAL.UpdateSchoolActive(ids, isActive, updatedBy);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while updating school active statuses.", ex);
            }
        }

        // Method to update school synchronization status
        public int UpdateSchoolDbSyncronized(int id, string updatedBy)
        {
            if (string.IsNullOrWhiteSpace(updatedBy))
                throw new ArgumentException("UpdatedBy cannot be null or empty", nameof(updatedBy));

            try
            {
                // Instantiate SchoolDAL and call the method
                SchoolDAL schoolDAL = new SchoolDAL();
                int result = schoolDAL.UpdateSchoolDbSyncronized(id, updatedBy);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred while updating synchronization status for school with ID {id}.", ex);
            }
        }
    }
}