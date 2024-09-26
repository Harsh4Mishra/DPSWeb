using DPS.Encryption;
using DPS.SuperAdmin.SchoolClassFile;
using System;
using System.Data;

namespace DPS.Common
{
    public partial class LoginScreen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SchoolBLL schoolBLL = new SchoolBLL();
                DataTable dt = new DataTable();
                dt = schoolBLL.GetSchoolDetailsByEmail(txtexampleInputEmail1.Text);
                if (dt.Rows != null)
                {
                    if (int.Parse(dt.Rows[0]["PASSWORD_ATTEMPTS"].ToString()) <= 5)
                    {
                        var hashKey = dt.Rows[0]["PASSWORD_HASH_KEY"].ToString();//mployeeList.PasswordHashKey;
                        var saltKey = dt.Rows[0]["PASSWORD_SALT_KEY"].ToString();//employeeList.PasswordSaltKey;
                        AesService aesCryptoService = new AesService();
                        var UpdatedHashKey = aesCryptoService.EncryptString(saltKey, txtexampleInputPassword1.Text);
                        if (UpdatedHashKey == hashKey)
                        {
                            string clientID= dt.Rows[0]["ID"].ToString();
                            string schoolName = dt.Rows[0]["NAME"].ToString();
                            string emailId = dt.Rows[0]["EMAIL_ID"].ToString();
                            string logo = dt.Rows[0]["LOGO"].ToString();
                            string databaseName = dt.Rows[0]["ID_DATABASE"].ToString();
                            string academicYear = dt.Rows[0]["ACADEMIC_YEAR"].ToString();

                            Session["schoolName"] = schoolName;
                            Session["emailId"] = emailId;
                            Session["logo"]= logo;
                            Session["databaseName"] = databaseName;
                            Session["academicYear"] = academicYear;
                            Session["ClientID"] = clientID;

                            if(schoolName == "DPS")
                            {
                                Response.Redirect("../SuperAdmin/IndexPage.aspx");
                            }
                            else
                            {

                            }

                        }
                        else
                        {

                            int numberOfAttempts = int.Parse(dt.Rows[0]["PASSWORD_ATTEMPTS"].ToString());
                            numberOfAttempts = numberOfAttempts + 1;
                            string updatedBy = txtexampleInputEmail1.Text;

                            var result = schoolBLL.UpdatePasswordAttemptsByEmail(txtexampleInputEmail1.Text, numberOfAttempts, updatedBy);

                            string errorScript = $"alert('Invalid Credentials!');";
                            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", errorScript, true);

                        }
                    }
                    else
                    {

                        string errorScript = $"alert('Attempts limit has exceeded please set new credential by clicking on forgot password.');";
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", errorScript, true);
                    }
                }
                else
                {
                    string errorScript = $"alert('No such Employee Found. Please provide proper Employee Code');";
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", errorScript, true);
                }
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
    }
}