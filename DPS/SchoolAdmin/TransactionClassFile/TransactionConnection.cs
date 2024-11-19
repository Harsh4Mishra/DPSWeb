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
                    connection = @"Data Source=DESKTOP-MB1QN8B\SQLEXPRESS;Initial Catalog=" + databaseName + ";Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=120;";
                    //connection = "Data Source=150.242.203.229;Initial Catalog=" + databaseName+ ";User Id=dpsuser;Password=dps@123;Integrated Security=False;MultipleActiveResultSets=True"; // Replace with your default
                }
            }

            return connection;
        }
    }
}