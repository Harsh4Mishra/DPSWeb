using DPS.Student.FeeClassFile;
using DPS.SuperAdmin.SchoolClassFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPS.Student
{
    public partial class Receipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string scholarNo = Session["ScholarNo"].ToString();
                int schoolID = int.Parse(Session["SchoolId"].ToString());
                DataTable dt = new DataTable();
                SchoolBLL schoolBLL = new SchoolBLL();
                dt = schoolBLL.GetSchoolById(schoolID);
                if (dt.Rows.Count > 0)
                {
                    lblSchoolName.Text = dt.Rows[0]["NAME"].ToString();
                    lblSchoolAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                    lblSchoolEmail.Text = dt.Rows[0]["EMAIL_ID"].ToString();
                    lblSchoolPhone.Text = dt.Rows[0]["PHONE_NUMBER"].ToString();
                    string imagepath = "../" + dt.Rows[0]["LOGO"].ToString();
                    Image1.ImageUrl = imagepath;
                    Session["databaseName"] = dt.Rows[0]["ID_DATABASE"].ToString();
                   
                }
                // Set dynamic values
                FeesBLL fees = new FeesBLL();
                DataTable dt2 = new DataTable();
                dt2 = fees.GetStudentDetailByScholarNo(scholarNo);
                if (dt2.Rows.Count > 0)
                {
                    // Example personal details
                    lblScholarNo.Text = dt2.Rows[0]["Scholarno"].ToString();
                    lblStudentName.Text = dt2.Rows[0]["StudentName"].ToString();
                    lblfeecategory.Text = dt2.Rows[0]["Caste"].ToString();
                    lblStudentStream.Text = dt2.Rows[0]["AppliedStream"].ToString();
                    lblFatherName.Text = dt2.Rows[0]["FatherName"].ToString();
                    lblstudentclass.Text = dt2.Rows[0]["ClassName"].ToString();
                    lblstudentsection.Text = dt2.Rows[0]["SectionName"].ToString();
                }
                lblreceiptNo.Text = Session["ReceiptNo"].ToString();
                

                DataTable feedt = (DataTable)Session["NoFineDataTable"];
                lblFineAmt.Text= Session["FineAmountTotal"].ToString();
                // Bind data to GridView
                GridViewFeeDetails.DataSource = feedt;
                GridViewFeeDetails.DataBind();
            }
        }
    }
}