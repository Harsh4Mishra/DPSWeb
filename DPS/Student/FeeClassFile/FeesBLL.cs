using System;
using System.Collections.Generic;
using System.Data;

namespace DPS.Student.FeeClassFile
{
    public class FeesBLL
    {
        public DataTable GetStudentDetailByScholarNo(string scholarNo)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                FeesDAL schoolDAL = new FeesDAL();
                DataTable result = schoolDAL.GetStudentDetailByScholarNo(scholarNo);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred  {ex.Message}.", ex);
            }
        }
        public DataSet StudentFeeParameterDetail(string scholarNo)
        {
            try
            {
                // Instantiate SchoolDAL
                FeesDAL schoolDAL = new FeesDAL();

                // Call the method that retrieves the data
                var result = schoolDAL.StudentFeeParameterDetail(scholarNo);

                return result; // Return the tuple containing both DataTables
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred: {ex.Message}.", ex);
            }
        }
        public DataTable GetPaidFeeByScholarNo(string scholarNo)
        {
            try
            {
                // Instantiate SchoolDAL
                FeesDAL schoolDAL = new FeesDAL();

                // Call the method that retrieves the data
                var result = schoolDAL.GetPaidFeeByScholarNo(scholarNo);

                return result; // Return the tuple containing both DataTables
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred: {ex.Message}.", ex);
            }
        }
        public DataTable GetFeeTransactionRequestById(int id)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                FeesDAL schoolDAL = new FeesDAL();
                DataTable result = schoolDAL.GetFeeTransactionRequestById(id);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException($"An error occurred  {ex.Message}.", ex);
            }
        }
        public int AddFeeTransactionRequest(FeeTransactionRequest feeTransaction)
        {
            if (feeTransaction == null)
                throw new ArgumentNullException(nameof(feeTransaction));

            try
            {
                // Instantiate SchoolDAL and call the method
                FeesDAL schoolDAL = new FeesDAL();
                int result = schoolDAL.AddFeeTransactionRequest(feeTransaction);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while adding the new school.", ex);
            }
        }
        public int AddFeeTransactionValue(string textValue, string encryptedValue)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                FeesDAL schoolDAL = new FeesDAL();
                int result = schoolDAL.AddFeeTransactionValue(textValue,encryptedValue);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while adding the new school.", ex);
            }
        }
        public int AddFeeReceiptPrintOnline(DateTime receiptDt, string scholarNo)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                FeesDAL schoolDAL = new FeesDAL();
                int result = schoolDAL.AddFeeReceiptPrintOnline(receiptDt, scholarNo);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while adding the new school.", ex);
            }
        }
        public int AddFeeTransactionOnline(FeeTransactionModel model)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                FeesDAL schoolDAL = new FeesDAL();
                int result = schoolDAL.AddFeeTransactionOnline(model);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while adding the new school.", ex);
            }
        }
        public int InsertFeeTransDetailOnline(List<FeeTransDetailOnline> feeDetails)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                FeesDAL schoolDAL = new FeesDAL();
                int result = schoolDAL.InsertFeeTransDetailOnline(feeDetails);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while adding the new school.", ex);
            }
        }
        public int GetSerialNoByFeeName(string feeName)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                FeesDAL schoolDAL = new FeesDAL();
                int result = (int)schoolDAL.GetSerialNoByFeeName(feeName);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while adding the new school.", ex);
            }
        }
        public int GetOrderNoByFeeType(string feeType)
        {
            try
            {
                // Instantiate SchoolDAL and call the method
                FeesDAL schoolDAL = new FeesDAL();
                int result = (int)schoolDAL.GetOrderNoByFeeType(feeType);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                // LogException(ex);
                throw new ApplicationException("An error occurred while adding the new school.", ex);
            }
        }
    }
}