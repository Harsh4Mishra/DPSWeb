using DPS.SuperAdmin.SchoolClassFile;
using System;
using System.Data;

namespace DPS.Student
{
    public partial class FeeDetailList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                // Retrieve values from the query string
                string school = Request.QueryString["School"];

                if (!string.IsNullOrEmpty(school))
                {
                    SchoolBLL schoolBLL = new SchoolBLL();
                    DataTable dt = new DataTable();
                    dt = schoolBLL.GetSchoolById(int.Parse(school));
                    if(dt.Rows.Count>0)
                    {
                        lblName.Text = dt.Rows[0]["NAME"].ToString();
                        lbladdress.Text = dt.Rows[0]["ADDRESS"].ToString();
                        lblEmailID.Text = dt.Rows[0]["EMAIL_ID"].ToString();
                        lblContact.Text = dt.Rows[0]["PHONE_NUMBER"].ToString();
                        Session["databaseName"] = dt.Rows[0]["ID_DATABASE"].ToString();
                    }
                }
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {

        }
    }
}