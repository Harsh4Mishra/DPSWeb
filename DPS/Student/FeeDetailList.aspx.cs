using DPS.Student.FeeClassFile;
using DPS.SuperAdmin.SchoolClassFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
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
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<string> selectedFeeMonths = new List<string>();
            
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkIsActive = (CheckBox)row.FindControl("chkIsActive");
                Label lblFeeMonth = (Label)row.FindControl("lblFeeMonth");
                string feeMonthText = lblFeeMonth.Text;
                string[] splitFeeMonth = feeMonthText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (chkIsActive != null && chkIsActive.Checked)
                {
                    selectedFeeMonths.Add(splitFeeMonth[0].ToString());
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

                DataTable paidFeedt = new DataTable();
                paidFeedt = fees.GetPaidFeeByScholarNo(txtScholarNo.Text);
                List<string> paidList = new List<string>();
                foreach(DataRow dr in paidFeedt.Rows)
                {
                    paidList.Add(dr["FeeType"].ToString());
                }
                Session["PaidFeeMonths"] = paidList;

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
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Retrieve the FeeMonth value from the data row
                string feeMonth = DataBinder.Eval(e.Row.DataItem, "FeeMonth")?.ToString(); // Use null conditional operator to avoid null reference

                // Get the CheckBox and Label controls from the row
                CheckBox chkIsActive = (CheckBox)e.Row.FindControl("chkIsActive");
                Label lblFeeMonth = (Label)e.Row.FindControl("lblFeeMonth");

                // Check if the session value exists and is valid
                List<string> paidFeeMonths = (List<string>)Session["PaidFeeMonths"];

                // Check if paidFeeMonths list is null or empty
                if (paidFeeMonths != null && feeMonth != null && paidFeeMonths.Contains(feeMonth))
                {
                    // If the FeeMonth is in the paidFeeMonths list, set the label text to "Paid"
                    lblFeeMonth.Text = feeMonth + " (Paid)";

                    // Disable the checkbox for paid months
                    chkIsActive.Enabled = false;
                }
                else
                {
                    // If the FeeMonth is not in the paidFeeMonths list, set the label text to "Unpaid"
                    lblFeeMonth.Text = feeMonth + " (Unpaid)";

                    // Enable the checkbox for unpaid months
                    chkIsActive.Enabled = true;
                }
            }
        }


    }
}