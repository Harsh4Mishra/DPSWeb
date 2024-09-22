using DPS.SuperAdmin.PaymentConfigurationClassFIle;
using DPS.SuperAdmin.SchoolClassFile;
using DPS.SuperAdmin.SchoolDatabaseClassFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPS.SuperAdmin
{
    public partial class AddPaymentConfiguration : System.Web.UI.Page
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
                // Create an instance of SchoolPaymentConfiguration and populate it with form data
                SchoolPaymentConfiguration paymentConfig = new SchoolPaymentConfiguration
                {
                    ClientId = int.Parse(ddlSchool.SelectedValue), // Assuming ddlSchool has the ClientId
                    MccCode = txtName.Text,
                    MerchantId = txtMerchantId.Text,
                    UserId = txtUserId.Text,
                    MerchantPassword = txtMerchantPassword.Text,
                    ProductId = txtProductId.Text,
                    TransactionCurrency = txtTransactionCurrency.Text,
                    RequestAesKey = txtRequestAesKey.Text,
                    RequestHashKey = txtRequestHashKey.Text,
                    ResponseAesKey = txtResponseAesKey.Text,
                    ResponseHashKey = txtResponseHashKey.Text,
                    HashAlgorithm = txtHashAlgorithm.Text,
                    CustomerAccountNumber = txtCustomerAccountNumber.Text,
                    IsActive = chkActive.Checked,
                    CreatedBy = "Admin", // Set as needed
                    CreatedOn = DateTime.Now // Capture current timestamp
                };

                // Instantiate the BLL class and call the method to save the configuration
                SchoolPaymentConfigurationBLL paymentConfigBLL = new SchoolPaymentConfigurationBLL();
                int result =  paymentConfigBLL.AddSchoolPaymentConfiguration(paymentConfig);

                // Check if the configuration was added successfully
                if (result > 0)
                {
                    // Notify success
                    string successScript = "alert('Payment configuration added successfully!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);
                }
                else
                {
                    // Notify failure
                    string failureScript = "alert('Failed to add payment configuration.');";
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