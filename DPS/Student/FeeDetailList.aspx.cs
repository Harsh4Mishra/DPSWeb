using DPS.Student.FeeClassFile;
using DPS.SuperAdmin.SchoolClassFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace DPS.Student
{
    public partial class FeeDetailList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve values from the query string
                string school = Request.QueryString["School"];

                if (!string.IsNullOrEmpty(school))
                {
                    SchoolBLL schoolBLL = new SchoolBLL();
                    DataTable dt = new DataTable();
                    dt = schoolBLL.GetSchoolById(int.Parse(school));
                    if (dt.Rows.Count > 0)
                    {
                        lblName.Text = dt.Rows[0]["NAME"].ToString();
                        lbladdress.Text = dt.Rows[0]["ADDRESS"].ToString();
                        lblEmailID.Text = dt.Rows[0]["EMAIL_ID"].ToString();
                        lblContact.Text = dt.Rows[0]["PHONE_NUMBER"].ToString();
                        string imagepath = "../"+dt.Rows[0]["LOGO"].ToString();
                        Image1.ImageUrl=imagepath;
                        Session["databaseName"] = dt.Rows[0]["ID_DATABASE"].ToString();
                        Session["SchoolName"] = school;
                    }
                }
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            FeesBLL fees = new FeesBLL();
            DataTable dt = new DataTable();
            string scholarno = txtScholarNo.Text;
            dt = fees.GetStudentDetailByScholarNo(scholarno);
            if (dt.Rows.Count > 0)
            {
                // Example personal details
                TextBox1.Text = dt.Rows[0]["Scholarno"].ToString();
                txtStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                txtDOB.Text = dt.Rows[0]["DOB"].ToString();
                txtSex.Text = dt.Rows[0]["Sex"].ToString();
                txtFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                txtFatherPhone.Text = dt.Rows[0]["FatherPhone"].ToString();
                txtClass.Text = dt.Rows[0]["ClassName"].ToString();
                txtSection.Text = dt.Rows[0]["SectionName"].ToString();
            }
            DataTable newdt = new DataTable();
            GridView1.DataSource = newdt;
            GridView1.DataBind();
            feelist.Visible = false;
            proceedbutton.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<string> selectedFeeMonths = new List<string>();
            
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkIsActive = (CheckBox)row.FindControl("chkIsActive");
                Label lblFeeMonth = (Label)row.FindControl("lblIdDatabase");

                if (chkIsActive != null && chkIsActive.Checked)
                {
                    selectedFeeMonths.Add(lblFeeMonth.Text);
                }
            }

            // Here you can use the selectedFeeMonths list as needed
            // For demonstration, let's show a message with selected FeeMonths
            if (selectedFeeMonths.Count > 0)
            {
                string selectedMonths = string.Join(",", selectedFeeMonths);
                // Redirect to the target page with the selected months as a query string
                Response.Redirect($"PaymentPage.aspx?selectedMonths={Server.UrlEncode(selectedMonths)}");
            }
            else
            {
                string failureScript = "alert('Please select feemonth');";
                ClientScript.RegisterStartupScript(this.GetType(), "FailureAlert", failureScript, true);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Session["StudentPayFee"] = txtScholarNo.Text;
                FeesBLL fees = new FeesBLL();
                // Call the method to get the FeeDetails and MonthlyFees
                DataSet ds = fees.StudentFeeParameterDetail(txtScholarNo.Text);

                Session["MyDataTable"] = ds.Tables[0];
                GridView1.DataSource = ds.Tables[1];
                GridView1.DataBind();
                Button1.Visible = true;
                feelist.Visible = true;
                proceedbutton.Visible = true;
            }
            catch (ApplicationException ex)
            {
                // Handle the application exception
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}