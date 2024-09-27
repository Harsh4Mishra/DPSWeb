using DPS.Encryption;
using DPS.SuperAdmin.SchoolClassFile;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPS.Common
{
    public partial class SetNewPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string qValue = Request.QueryString["q"];
                UTFService utfService = new UTFService();
                var emailID = utfService.Decrypt(qValue);

                SchoolBLL schoolBLL = new SchoolBLL();
                DataTable dt = new DataTable();
                dt = schoolBLL.GetSchoolDetailsByEmail(emailID);
                if (dt.Rows.Count > 0)
                {
                    AesService aesService = new AesService();
                    var chipperText = aesService.GenerateRandomKey();
                    var UpdatedHashKey = aesService.EncryptString(chipperText, txtexampleInputEmail1.Text);

                    string schoolName = dt.Rows[0]["NAME"].ToString();

                    var result = schoolBLL.UpdateNewPassword(emailID, UpdatedHashKey, chipperText, schoolName);
                    //await _unitOfWork.CommitTransactionAsync(cancellationToken);

                    if (result != null)
                    {
                        Response.Redirect("LoginScreen.aspx");
                    }
                    else
                    {
                        string errorScript = $"alert('Password Not Updated');";
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
    }
}