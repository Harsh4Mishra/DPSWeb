using System.Web;

namespace DPS.SchoolAdmin.TransactionClassFile
{
    public class TransactionConnection
    {
        public string ConnectionString()
        {
            string connection = "";

            // Access the session variable
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                // Assuming your connection string is stored in session with the key "ConnectionString"
                string databaseName = HttpContext.Current.Session["databaseName"] as string;

                // Optionally, handle the case where the session variable is not set
                if (!string.IsNullOrEmpty(databaseName))
                {
                    // Provide a default connection string or handle the error
                    //connection = @"Server=85.25.185.85\MSSQLSERVER2017Initial Catalog=" + databaseName + ";User Id=DPSERP;Password=Dpstech@123;MultipleActiveResultSets=True;Connect Timeout=1200;";
                    connection = "Data Source=150.242.203.229;Initial Catalog=" + databaseName + ";User Id=dpsuser;Password=dps@123;Integrated Security=False;MultipleActiveResultSets=True;Connect Timeout=60000;"; // Replace with your default
                }
            }

            return connection;
        }
    }
}