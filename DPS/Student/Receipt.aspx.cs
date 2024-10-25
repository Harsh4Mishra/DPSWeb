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
        public string LogoPath { get; set; }
        protected string SchoolName { get; set; }
        protected string SchoolAddress { get; set; }
        protected string SchoolPhone { get; set; }
        protected string SchoolEmail { get; set; }
        protected string ReceiptNo { get; set; }
        protected string ScholarNo { get; set; }
        protected string StudentName { get; set; }
        protected string FatherName { get; set; }
        protected string FeeCategory { get; set; }
        protected string StudentClass { get; set; }
        protected string StudentSection { get; set; }
        protected string StudentStream { get; set; }

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
                    SchoolName = dt.Rows[0]["NAME"].ToString();
                    SchoolAddress = dt.Rows[0]["ADDRESS"].ToString();
                    SchoolEmail = dt.Rows[0]["EMAIL_ID"].ToString();
                    SchoolPhone = dt.Rows[0]["PHONE_NUMBER"].ToString();
                    string imagepath = "../" + dt.Rows[0]["LOGO"].ToString();
                    LogoPath = imagepath;
                    Session["databaseName"] = dt.Rows[0]["ID_DATABASE"].ToString();
                   
                }
                // Set dynamic values
                FeesBLL fees = new FeesBLL();
                DataTable dt2 = new DataTable();
                dt2 = fees.GetStudentDetailByScholarNo(scholarNo);
                if (dt.Rows.Count > 0)
                {
                    // Example personal details
                    ScholarNo = dt.Rows[0]["Scholarno"].ToString();
                    StudentName = dt.Rows[0]["StudentName"].ToString();
                    FeeCategory = dt.Rows[0]["Caste"].ToString();
                    StudentStream = dt.Rows[0]["AppliedStream"].ToString();
                    FatherName = dt.Rows[0]["FatherName"].ToString();
                    StudentClass = dt.Rows[0]["ClassName"].ToString();
                    StudentSection = dt.Rows[0]["SectionName"].ToString();
                }
                ReceiptNo = Session["ReceiptNo"].ToString();


                DataTable feedt = (DataTable)Session["NoFineDataTable"];

                // Bind data to GridView
                GridViewFeeDetails.DataSource = feedt;
                GridViewFeeDetails.DataBind();
            }
        }
    }
}