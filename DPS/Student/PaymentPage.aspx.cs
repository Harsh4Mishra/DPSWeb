using DPS.Student.FeeClassFile;
using System;
using System.Data;
using System.Linq;
using System.Web.DynamicData;

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

                    // Filter the DataTable based on monthsArray
                    var filteredRows = feedt.AsEnumerable()
                        .Where(row => monthsArray.Contains(row.Field<string>("FeeType")));

                    // Create a new DataTable for the filtered results
                    DataTable filteredDataTable = filteredRows.CopyToDataTable();

                    // Calculate the sum of FeeAmount where FeeName != "Fine"
                    decimal sumNotFine = filteredDataTable.AsEnumerable()
                        .Where(row => row.Field<string>("FeeName") != "Fine")
                        .Sum(row => row.Field<decimal>("FeeAmount"));

                    // Calculate the sum of FeeAmount where FeeName = "Fine"
                    decimal sumFine = filteredDataTable.AsEnumerable()
                        .Where(row => row.Field<string>("FeeName") == "Fine")
                        .Sum(row => row.Field<decimal>("FeeAmount"));

                    int sumFeeValue = Convert.ToInt32(sumNotFine);
                    int sumFineValue = Convert.ToInt32(sumFine);


                    txtFeeAmount.Text = sumFeeValue.ToString();
                    txtFineAmount.Text = sumFineValue.ToString();

                    int FinalAmount = txtFeeAmount.Text == "" ? 0 : int.Parse(txtFeeAmount.Text) + txtFineAmount.Text == "" ? 0 : int.Parse(txtFineAmount.Text);

                    txtfinalAmount.Text = FinalAmount.ToString();
                   

                    GridView1.DataSource = filteredDataTable;
                    GridView1.DataBind();
                }
            }
        }
    }
}