using DPS.SuperAdmin.SchoolClassFile;
using DPS.SuperAdmin.SchoolDatabaseClassFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPS.SuperAdmin
{
    public partial class AddSchoolDatabase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Generate a unique file name based on the current timestamp
                string timestamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
                txtName.Text = timestamp;
                BindSchools();
                GenerateAcademicYears();
            }
        }
        public void GenerateAcademicYears()
        {
            List<string> academicYears = new List<string>();

            // Get the current year
            int currentYear = DateTime.Now.Year;

            // Determine the starting year of the current academic year
            int academicYearStart = (DateTime.Now.Month >= 8) ? currentYear : currentYear - 1;

            // Add the current academic year
            academicYears.Add($"{academicYearStart}-{academicYearStart + 1}");

            // Generate the next 5 academic years
            for (int i = 1; i <= 5; i++)
            {
                academicYears.Add($"{academicYearStart + i}-{academicYearStart + i + 1}");
            }

            ddlAcademicYear.Items.Insert(0, new ListItem("Select Academic Year", "0"));
            int count = 1;
            foreach(var item in academicYears)
            {
                ddlAcademicYear.Items.Insert(count, new ListItem(item, item));
                count++;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SchoolDatabaseMaster.aspx");
        }
        private void BindSchools()
        {
            try
            {
                // Instantiate SchoolBLL and call the method
                SchoolBLL schoolBLL = new SchoolBLL();
                DataTable dt = schoolBLL.GetAllActiveSchools();

                // Clear existing items
                ddlSchool.Items.Clear();

                // Bind the DataTable to the DropDownList
                ddlSchool.DataSource = dt;
                ddlSchool.DataTextField = "Name";  // Field to display in the dropdown
                ddlSchool.DataValueField = "ID";   // Field to use for postback
                ddlSchool.DataBind();

                ddlSchool.Items.Insert(0, new ListItem("Select School", "0"));
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred while retrieving the data.');", true);
            }
        }
        protected async void btnsave_Click(object sender, EventArgs e)
        {
            try
            {

                // Instantiate SchoolBLL and call the AddSchool method
                SchoolDatabaseBLL schoolBLL = new SchoolDatabaseBLL();
                bool isinused = chkisInUsed.Checked == true ? true : false;

                int result = schoolBLL.AddSchoolDatabase(int.Parse(ddlSchool.SelectedValue.ToString()),txtName.Text,"Admin",ddlAcademicYear.SelectedValue,isinused);

                // Check if the school was added successfully
                if (result > 0)
                {
                    // Notify success
                    string successScript = "alert('School Database added successfully!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);
                }
                else
                {
                    // Notify failure
                    string failureScript = "alert('Failed to add school.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "FailureAlert", failureScript, true);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and notify the user
                // LogException(ex);
                string errorScript = $"alert('An error occurred: {ex.Message}');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", errorScript, true);
            }
        }
    }
}