using DPS.Encryption;
using DPS.SuperAdmin.SchoolClassFile;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace DPS.Student
{
    public partial class SchoolWithState : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindStates();
            }
        }
        private void BindStates()
        {
            try
            {
                // Instantiate SchoolBLL and call the method
                SchoolBLL schoolBLL = new SchoolBLL();
                DataTable dt = schoolBLL.GetAllStates();

                // Clear existing items
                ddlState.Items.Clear();

                // Add the default item
                ddlState.Items.Insert(0, new ListItem("Select State", "0"));

                // Bind the DataTable to the DropDownList
                ddlState.DataSource = dt;
                ddlState.DataTextField = "Name";  // Field to display in the dropdown
                ddlState.DataValueField = "ID";   // Field to use for postback
                ddlState.DataBind();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred while retrieving the data.');", true);
            }
        }
        private void BindSchools()
        {
            try
            {
                // Instantiate SchoolBLL and call the method
                SchoolBLL schoolBLL = new SchoolBLL();
                DataTable dt = schoolBLL.GetSchoolByStateId(int.Parse(ddlState.SelectedValue.ToString()));

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("FeeDetailList.aspx?School="+ddlSchool.SelectedValue.ToString()+"");
            }
            catch (Exception ex)
            {
                string errorScript = $"alert('An error occurred: {ex.Message}');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", errorScript, true);
            }
            finally
            {

            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSchools();
        }
    }
}