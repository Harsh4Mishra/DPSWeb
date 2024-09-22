using DPS.SuperAdmin.SchoolClassFile;
using DPS.SuperAdmin.SchoolDatabaseClassFile;
using System;
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
                int schoolID = int.Parse(Session["EditClientDatabaseID"].ToString());
                DataTable dt = schoolDatabaseBLL.GetSchoolDatabaseById(schoolID);
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["DATABASE_NAME"].ToString();
                    ddlSchool.SelectedValue = dt.Rows[0]["CLIENT_ID"].ToString();
                    chkActive.Checked = Boolean.Parse(dt.Rows[0]["IS_ACTIVE"].ToString());
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
                int result = schoolBLL.UpdateSchoolDatabase(schoolDatabaseID,int.Parse(ddlSchool.SelectedValue.ToString()),txtName.Text, ischecked,"Admin");

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