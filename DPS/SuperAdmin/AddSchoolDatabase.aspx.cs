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

                // Add the default item
                

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
                int result = schoolBLL.AddSchoolDatabase(int.Parse(ddlSchool.SelectedValue.ToString()),txtName.Text,"Admin");

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