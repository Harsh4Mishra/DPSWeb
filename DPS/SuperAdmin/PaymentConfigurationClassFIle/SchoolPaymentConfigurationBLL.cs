using System;
using System.Collections.Generic;
using System.Data;

namespace DPS.SuperAdmin.PaymentConfigurationClassFIle
{
    public class SchoolPaymentConfigurationBLL
    {
        // Method to get all school payment configurations
        public DataTable GetAllSchoolPaymentConfigurations()
        {
            try
            {
                // Instantiate SchoolPaymentConfigurationDAL and call the method
                SchoolPaymentConfigurationDAL dal = new SchoolPaymentConfigurationDAL();
                DataTable result = dal.GetAllSchoolPaymentConfigurations();
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while retrieving all school payment configurations.", ex);
            }
        }

        // Method to get a school payment configuration by ID
        public DataTable GetSchoolPaymentConfigurationById(int id)
        {
            try
            {
                // Instantiate SchoolPaymentConfigurationDAL and call the method
                SchoolPaymentConfigurationDAL dal = new SchoolPaymentConfigurationDAL();
                DataTable result = dal.GetSchoolPaymentConfigurationById(id);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred while retrieving the school payment configuration with ID {id}.", ex);
            }
        }

        // Method to add a new school payment configuration
        public int AddSchoolPaymentConfiguration(SchoolPaymentConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            try
            {
                // Instantiate SchoolPaymentConfigurationDAL and call the method
                SchoolPaymentConfigurationDAL dal = new SchoolPaymentConfigurationDAL();
                int result = dal.AddSchoolPaymentConfiguration(config);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while adding the new school payment configuration.", ex);
            }
        }

        // Method to update an existing school payment configuration
        public int UpdateSchoolPaymentConfiguration(SchoolPaymentConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            try
            {
                // Instantiate SchoolPaymentConfigurationDAL and call the method
                SchoolPaymentConfigurationDAL dal = new SchoolPaymentConfigurationDAL();
                int result = dal.UpdateSchoolPaymentConfiguration(config);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while updating the school payment configuration.", ex);
            }
        }

        // Method to delete a school payment configuration
        public int DeleteSchoolPaymentConfiguration(int id, string deletedBy)
        {
            if (string.IsNullOrWhiteSpace(deletedBy))
                throw new ArgumentException("DeletedBy cannot be null or empty", nameof(deletedBy));

            try
            {
                // Instantiate SchoolPaymentConfigurationDAL and call the method
                SchoolPaymentConfigurationDAL dal = new SchoolPaymentConfigurationDAL();
                int result = dal.DeleteSchoolPaymentConfiguration(id, deletedBy);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred while deleting the school payment configuration with ID {id}.", ex);
            }
        }

        // Method to update active status for a school payment configuration
        public int UpdateSchoolPaymentConfigurationActive(List<int> id, List<bool> isActive, string updatedBy)
        {
            if (string.IsNullOrWhiteSpace(updatedBy))
                throw new ArgumentException("UpdatedBy cannot be null or empty", nameof(updatedBy));

            try
            {
                // Instantiate SchoolPaymentConfigurationDAL and call the method
                SchoolPaymentConfigurationDAL dal = new SchoolPaymentConfigurationDAL();
                int result = dal.UpdateSchoolPaymentConfigurationActive(id, isActive, updatedBy);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while updating the school payment configuration active status.", ex);
            }
        }
    }
}