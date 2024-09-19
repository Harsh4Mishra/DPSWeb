using DPS.SuperAdmin.SchoolClassFile;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPS.SuperAdmin
{
    public partial class AddClient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStates();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientMaster.aspx");
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

        protected async void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
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

                        // Create an instance of SchoolMaster and populate it with form data
                        SchoolMaster school = new SchoolMaster
                        {
                            Name = txtName.Text,
                            Address = txtAddress.Text,
                            City = txtCity.Text,
                            IdState = int.Parse(ddlState.SelectedValue),
                            Country = txtCountry.Text,
                            PhoneNumber = txtPhoneNumber.Text,
                            Pincode = txtPincode.Text,
                            EmailId = txtEmailId.Text,
                            Logo = $"/SchoolLogo/{newFileName}", // Store the path to the logo
                            IdDatabase = timestamp, // Set IdDatabase to the timestamp
                            IsActive = true, // Set default value or based on form data
                            CreatedBy = "Admin" // Set as needed
                        };

                        // Instantiate SchoolBLL and call the AddSchool method
                        SchoolBLL schoolBLL = new SchoolBLL();
                        int result = schoolBLL.AddSchool(school);

                        // Check if the school was added successfully
                        if (result > 0)
                        {
                            // Notify success
                            string successScript = "alert('School added successfully!');";
                            ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);
                        }
                        else
                        {
                            // Notify failure
                            string failureScript = "alert('Failed to add school.');";
                            ClientScript.RegisterStartupScript(this.GetType(), "FailureAlert", failureScript, true);
                        }
                    }
                    else
                    {
                        // Notify invalid file type
                        string invalidFileScript = "alert('Invalid file type. Only .png, .jpg, .jpeg files are allowed.');";
                        ClientScript.RegisterStartupScript(this.GetType(), "InvalidFileAlert", invalidFileScript, true);
                    }
                }
                else
                {
                    // Notify file not selected
                    string noFileSelectedScript = "alert('Please select a file to upload.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "NoFileSelectedAlert", noFileSelectedScript, true);
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