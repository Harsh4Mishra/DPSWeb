using DPS.SuperAdmin.SchoolClassFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DPS.SchoolAdmin.TransactionClassFile
{
    public class TransactionBLL
    {
        public DataTable GetFeeTransactionSummaryOffLine(
                            string className = null,
                            string sectionName = null,
                            DateTime? fromDate = null,
                            DateTime? toDate = null)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                TransactionDAL schoolDAL = new TransactionDAL();
                DataTable result = schoolDAL.GetFeeTransactionSummaryOffLine(className, sectionName, fromDate, toDate);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred  {ex.Message}.", ex);
            }
        }
        public DataTable GetFeeTransactionSummaryONLine(
                            string className = null,
                            string sectionName = null,
                            DateTime? fromDate = null,
                            DateTime? toDate = null)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                TransactionDAL schoolDAL = new TransactionDAL();
                DataTable result = schoolDAL.GetFeeTransactionSummaryONLine(className, sectionName, fromDate, toDate);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred  {ex.Message}.", ex);
            }
        }
        public DataTable GetFeeTransactionSummaryBoth(
                            string className = null,
                            string sectionName = null,
                            DateTime? fromDate = null,
                            DateTime? toDate = null)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                TransactionDAL schoolDAL = new TransactionDAL();
                DataTable result = schoolDAL.GetFeeTransactionSummaryBoth(className, sectionName, fromDate, toDate);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred  {ex.Message}.", ex);
            }
        }
        public DataTable GetDistinctClassNames()
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                TransactionDAL schoolDAL = new TransactionDAL();
                DataTable result = schoolDAL.GetDistinctClassNames();
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred  {ex.Message}.", ex);
            }
        }
        public DataTable GetSectionNamesByClassName(
                            string className = null)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                TransactionDAL schoolDAL = new TransactionDAL();
                DataTable result = schoolDAL.GetSectionNamesByClassName(className);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred  {ex.Message}.", ex);
            }
        }
    }
}