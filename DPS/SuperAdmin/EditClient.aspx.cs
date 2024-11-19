using DPS.SuperAdmin.SchoolClassFile;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace DPS.SuperAdmin
{
    public partial class EditClient : System.Web.UI.Page
    {
        SchoolBLL schoolBLL = new SchoolBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStates();
                int schoolID = int.Parse(Session["EditClientID"].ToString());
                DataTable dt = schoolBLL.GetSchoolById(schoolID);
                if(dt.Rows.Count>0)
                {
                    string baseURL = ConfigurationManager.AppSettings["RedirectURL"].ToString();
                    txtName.Text = dt.Rows[0]["NAME"].ToString();
                    txtAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                    txtCity.Text = dt.Rows[0]["CITY"].ToString();
                    ddlState.SelectedValue = dt.Rows[0]["ID_STATE"].ToString();
                    txtCountry.Text = dt.Rows[0]["COUNTRY"].ToString();
                    txtPhoneNumber.Text = dt.Rows[0]["PHONE_NUMBER"].ToString();
                    txtPincode.Text = dt.Rows[0]["PINCODE"].ToString();
                    txtEmailId.Text = dt.Rows[0]["EMAIL_ID"].ToString();
                    chkActive.Checked = Boolean.Parse(dt.Rows[0]["ISACTIVE"].ToString());
                    Image1.ImageUrl = baseURL + dt.Rows[0]["LOGO"].ToString();
                    ViewState["LogoPath"]= dt.Rows[0]["LOGO"].ToString();
                    ViewState["DatabaseName"]= dt.Rows[0]["ID_DATABASE"].ToString();
                }
            }
        }
        private void BindStates()
        {
            try
            {
                // Instantiate SchoolBLL and call the method
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
          
            Response.Redirect("Clientaster.aspx");
        }

        protected async void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                int schoolID = int.Parse(Session["EditClientID"].ToString());
                // Create an instance of SchoolMaster and populate it with form data
                SchoolMaster school = new SchoolMaster
                {
                    Id = schoolID, // Assuming there's a hidden field for school ID
                    Name = txtName.Text,
                    Address = txtAddress.Text,
                    City = txtCity.Text,
                    IdState = int.Parse(ddlState.SelectedValue), // State selected in the dropdown
                    Country = txtCountry.Text,
                    PhoneNumber = txtPhoneNumber.Text,
                    Pincode = txtPincode.Text,
                    EmailId = txtEmailId.Text,
                    IdDatabase= ViewState["DatabaseName"].ToString(),
                    IsActive = chkActive.Checked==true?true:false // Assuming there's a checkbox for active/inactive status
                };

                // Check if a file was uploaded
                if (fuLogo.HasFile)
                {
                    // Validate the file extension
                    string fileExtension = Path.GetExtension(fuLogo.FileName).ToLower();
                    if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".jpeg")
                    {
                        // Generate a unique file name based on the current timestamp
                        string timestamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
                        string newFileName = $"{timestamp}{fileExtension}";

                        // Define the folder path and the full path for saving the uploaded file
                        string folderPath = Server.MapPath("~/SchoolLogo/"); // Folder where files will be stored
                        string filePath = Path.Combine(folderPath, newFileName);

                        // Ensure the folder exists
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // Save the file with the new name
                        fuLogo.SaveAs(filePath);

                        // Update the school object's Logo property
                        school.Logo = $"/SchoolLogo/{newFileName}"; // Store the path to the logo
                    }
                    else
                    {
                        // Notify invalid file type
                        string invalidFileScript = "alert('Invalid file type. Only .png, .jpg, .jpeg files are allowed.');";
                        ClientScript.RegisterStartupScript(this.GetType(), "InvalidFileAlert", invalidFileScript, true);
                        return;
                    }
                }
                else
                {
                    string path = ViewState["LogoPath"].ToString();
                    school.Logo = path;
                }

                // Set other properties of the SchoolMaster object
                school.UpdatedBy = "Admin"; // Set as needed
                school.UpdatedOn = DateTime.Now; // Set updated timestamp

                // Instantiate SchoolBLL and call the UpdateSchool method
                SchoolBLL schoolBLL = new SchoolBLL();
                int result = schoolBLL.UpdateSchool(school);

                // Check if the school was updated successfully
                if (result > 0)
                {
                    // Notify success
                    string successScript = "alert('School updated successfully!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);

                }
                else
                {
                    // Notify failure
                    string failureScript = "alert('Failed to update school.');";
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