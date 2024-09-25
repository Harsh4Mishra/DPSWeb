using DPS.SuperAdmin.SchoolClassFile;
using DPS.SuperAdmin.SchoolDatabaseClassFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace DPS.SuperAdmin
{
    public partial class EditSchoolDatabase : System.Web.UI.Page
    {
        SchoolDatabaseBLL schoolDatabaseBLL = new SchoolDatabaseBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSchools();
                GenerateAcademicYears();
                int schoolID = int.Parse(Session["EditClientDatabaseID"].ToString());
                DataTable dt = schoolDatabaseBLL.GetSchoolDatabaseById(schoolID);
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["DATABASE_NAME"].ToString();
                    ddlSchool.SelectedValue = dt.Rows[0]["CLIENT_ID"].ToString();
                    chkActive.Checked = Boolean.Parse(dt.Rows[0]["IS_ACTIVE"].ToString());
                    ddlAcademicYear.SelectedValue = dt.Rows[0]["ACADEMIC_YEAR"].ToString();
                    chkisInUsed.Checked = Boolean.Parse(dt.Rows[0]["IS_IN_USED"].ToString());
                }
            }
        }
        private void BindSchools()
        {
            try
            {
                SchoolBLL schoolBLL = new SchoolBLL();
                // Instantiate SchoolBLL and call the method
                DataTable dt = schoolBLL.GetAllActiveSchools();

                // Clear existing items
                ddlSchool.Items.Clear();

                // Add the default item
                ddlSchool.Items.Insert(0, new ListItem("Select School", "0"));

                // Bind the DataTable to the DropDownList
                ddlSchool.DataSource = dt;
                ddlSchool.DataTextField = "Name";  // Field to display in the dropdown
                ddlSchool.DataValueField = "ID";   // Field to use for postback
                ddlSchool.DataBind();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred while retrieving the data.');", true);
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
            foreach (var item in academicYears)
            {
                ddlAcademicYear.Items.Insert(count, new ListItem(item, item));
                count++;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            Response.Redirect("SchoolDatabaseMaster.aspx");
        }

        protected async void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                int schoolDatabaseID = int.Parse(Session["EditClientDatabaseID"].ToString());

                bool ischecked = chkActive.Checked == true ? true : false;

                // Instantiate SchoolBLL and call the UpdateSchool method
                SchoolDatabaseBLL schoolBLL = new SchoolDatabaseBLL();
                bool isinused = chkisInUsed.Checked == true ? true : false;
                int result = schoolBLL.UpdateSchoolDatabase(schoolDatabaseID,int.Parse(ddlSchool.SelectedValue.ToString()),txtName.Text, ischecked,"Admin",ddlAcademicYear.SelectedValue, isinused);

                // Check if the school was updated successfully
                if (result > 0)
                {
                    // Notify success
                    string successScript = "alert('School Database updated successfully!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);

                }
                else
                {
                    // Notify failure
                    string failureScript = "alert('Failed to update school database.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "FailureAlert", failureScript, true);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and notify the user
                // LogException(ex); // Implement a logging method here
                string errorScript = $"alert('An error occurred: {ex.Message}');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", errorScript, true);
            }
        }
    }
}