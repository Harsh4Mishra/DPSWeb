using DPS.Student.FeeClassFile;
using System;
using System.Data;

namespace DPS.Student
{
    public partial class PaymentPage : System.Web.UI.Page
    {
        FeesBLL fees = new FeesBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string selectedMonths = Request.QueryString["selectedMonths"];

                if (!string.IsNullOrEmpty(selectedMonths))
                {
                    DataTable feedt = (DataTable)Session["MyDataTable"];
                    // Split the selected months into an array
                    string[] monthsArray = selectedMonths.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    DataTable dt = new DataTable();
                    string scholarno=Session["StudentPayFee"].ToString();
                    dt = fees.GetStudentDetailByScholarNo(scholarno);
                    if(dt.Rows.Count>0)
                    {
                        // Example personal details
                        txtScholarNo.Text = dt.Rows[0]["Scholarno"].ToString();
                        txtStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                        txtDOB.Text = dt.Rows[0]["DOB"].ToString();
                        txtSex.Text = dt.Rows[0]["Sex"].ToString();
                        txtFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                        txtFatherPhone.Text = dt.Rows[0]["FatherPhone"].ToString();
                        txtClass.Text = dt.Rows[0]["ClassName"].ToString();
                        txtSection.Text = dt.Rows[0]["SectionName"].ToString();
                    }

                    GridView1.DataSource = feedt;
                    GridView1.DataBind();
                }
            }
        }
    }
}