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
    public partial class EditPaymentConfiguration : System.Web.UI.Page
    {
        SchoolPaymentConfigurationBLL schoolDatabaseBLL = new SchoolPaymentConfigurationBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSchools();
                int schoolID = int.Parse(Session["EditClientPGID"].ToString());
                DataTable dt = schoolDatabaseBLL.GetSchoolPaymentConfigurationById(schoolID);
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["MCC_CODE"].ToString();
                    ddlSchool.SelectedValue = dt.Rows[0]["CLIENT_ID"].ToString();
                    txtMerchantId.Text = dt.Rows[0]["MERCHANT_ID"].ToString();
                    txtUserId.Text = dt.Rows[0]["USER_ID"].ToString();
                    txtMerchantPassword.Text = dt.Rows[0]["MERCHANT_PASSWORD"].ToString();
                    txtProductId.Text = dt.Rows[0]["PRODUCT_ID"].ToString();
                   // txtTransactionCurrency.Text = dt.Rows[0]["TRANSACTION_CURRENCY"].ToString();
                    txtRequestAesKey.Text = dt.Rows[0]["REQUEST_AES_KEY"].ToString();
                    txtRequestHashKey.Text = dt.Rows[0]["REQUEST_HASH_KEY"].ToString();
                    txtResponseAesKey.Text = dt.Rows[0]["RESPONSE_AES_KEY"].ToString();
                    txtResponseHashKey.Text = dt.Rows[0]["RESPONSE_HASH_KEY"].ToString();
                    //txtHashAlgorithm.Text = dt.Rows[0]["HASH_ALGORITHM"].ToString();
                    //txtCustomerAccountNumber.Text = dt.Rows[0]["CUSTOMER_ACCOUNT_NUMBER"].ToString();
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
            Response.Redirect("PaymentConfigurationMaster.aspx");
        }

        protected async void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                int schoolDatabaseID = int.Parse(Session["EditClientPGID"].ToString());

                bool ischecked = chkActive.Checked == true ? true : false;

                SchoolPaymentConfiguration schoolPaymentConfiguration = new SchoolPaymentConfiguration();
                schoolPaymentConfiguration.Id = schoolDatabaseID;
                schoolPaymentConfiguration.ClientId = int.Parse(ddlSchool.SelectedValue.ToString());
                schoolPaymentConfiguration.MccCode = txtName.Text;
                schoolPaymentConfiguration.MerchantId = txtMerchantId.Text;
                schoolPaymentConfiguration.UserId = txtUserId.Text;
                schoolPaymentConfiguration.MerchantPassword = txtMerchantPassword.Text;
                schoolPaymentConfiguration.ProductId = txtProductId.Text;
                schoolPaymentConfiguration.TransactionCurrency = "INR";//txtTransactionCurrency.Text;
                schoolPaymentConfiguration.RequestAesKey = txtRequestAesKey.Text;
                schoolPaymentConfiguration.RequestHashKey = txtRequestHashKey.Text;
                schoolPaymentConfiguration.ResponseAesKey = txtResponseAesKey.Text;
                schoolPaymentConfiguration.ResponseHashKey = txtResponseHashKey.Text;
                schoolPaymentConfiguration.HashAlgorithm = "SHA1";// txtHashAlgorithm.Text;
                schoolPaymentConfiguration.CustomerAccountNumber = "12345678";//txtCustomerAccountNumber.Text;
                schoolPaymentConfiguration.IsActive = ischecked;
                schoolPaymentConfiguration.UpdatedBy = "Admin";
                schoolPaymentConfiguration.UpdatedOn = DateTime.Now;


                // Instantiate SchoolBLL and call the UpdateSchool method
                SchoolPaymentConfigurationBLL schoolBLL = new SchoolPaymentConfigurationBLL();
                int result = schoolBLL.UpdateSchoolPaymentConfiguration(schoolPaymentConfiguration);

                // Check if the school was updated successfully
                if (result > 0)
                {
                    // Notify success
                    string successScript = "alert('School Payment configuration updated successfully!');";
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