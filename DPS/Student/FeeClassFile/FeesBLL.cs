using DPS.SchoolAdmin.TransactionClassFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

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
        public (DataTable FeeDetails, DataTable MonthlyFees) StudentFeeParameterDetail(string scholarNo)
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

    }
}