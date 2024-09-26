using DPS.Encryption;
using DPS.SuperAdmin.SchoolClassFile;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPS.Common
{
    public partial class ForgotPassword : System.Web.UI.Page
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
                if(dt.Rows.Count>0)
                {
                    int result = schoolBLL.UpdatePasswordLinkVisitedByEmail(txtexampleInputEmail1.Text,false, txtexampleInputEmail1.Text);
                    if(result>0)
                    {
                        UTFService uTFService = new UTFService();
                        string encData = uTFService.Encrypt(txtexampleInputEmail1.Text);
                        string baseURL = ConfigurationManager.AppSettings["RedirectURL"].ToString();
                        string passwordPageName = ConfigurationManager.AppSettings["SetPasswordPage"].ToString();
                        string setPasswordURL = baseURL + passwordPageName + encData;
                        string schoolName = dt.Rows[0]["NAME"].ToString();

                        string subject = "Reset Your Account Password...";
                        string body = MailBody(schoolName, setPasswordURL);
                        string emailid = txtexampleInputEmail1.Text;
                        _mailService.SendMail(subject, body, emailid);
                    }
                    else
                    {
                        string errorScript = $"alert('Link Not Generated');";
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", errorScript, true);
                    }
                }
                else
                {
                    string errorScript = $"alert('No such School Found. Please provide proper School Mail ID');";
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", errorScript, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }


        public string MailBody(string employeeName, string url)
        {
            return $@"
             <!DOCTYPE html>
             <html>
             <head>
                 <style>
                     body {{
                         font-family: Arial, sans-serif;
                         line-height: 1.6;
                     }}
                     .container {{
                         max-width: 600px;
                         margin: 0 auto;
                         padding: 20px;
                         border: 1px solid #ddd;
                         border-radius: 5px;
                         background-color: #f9f9f9;
                     }}
                     .header {{
                         text-align: center;
                         border-bottom: 1px solid #ddd;
                         margin-bottom: 20px;
                         padding-bottom: 10px;
                     }}
                     .content {{
                         text-align: left;
                     }}
                     .footer {{
                         text-align: center;
                         margin-top: 20px;
                         font-size: 0.9em;
                         color: #777;
                     }}
                 </style>
             </head>
             <body>
                 <div class='container'>
                     <div class='header'>
                         <h2>Password Reset Request</h2>
                     </div>
                     <div class='content'>
                         <p>Dear {employeeName},</p>
                         <p>We received a request to reset your password. Please click the link below to reset your password:</p>
                         <p><a href='{url}'>Click here</a> to set your new password</p>
                         <p>If you did not request a password reset, please ignore this email or contact support if you have questions.</p>
                         <p>Best regards,<br/>The Support Team</p>
                     </div>
                     <div class='footer'>
                         <p>© {DateTime.Now.Year} DPS. All rights reserved.</p>
                     </div>
                 </div>
             </body>
             </html>";
        }
    }
}