using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPS.SchoolAdmin.TransactionClassFile;
using DPS.Encryption;

namespace DPS.SchoolAdmin
{
    public partial class OfflineTransaction : System.Web.UI.Page
    {
        TransactionBLL transactionBLL = new TransactionBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindClass();
                BindTransactionDetail();
            }
        }
        public void BindClass()
        {
            DataTable dt = new DataTable();
            dt = transactionBLL.GetDistinctClassNames();

            ddlClass.Items.Clear(); // Clear any existing items

            if (dt.Rows.Count > 0)
            {
                ddlClass.DataSource = dt;
                ddlClass.DataValueField = "ClassName"; // Change this if ClassID is the value field
                ddlClass.DataTextField = "ClassName"; // Assuming ClassName is the display field
                ddlClass.DataBind(); // Bind the data to the dropdown

                // Optionally, re-insert the default item after data binding
                ddlClass.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Class", ""));
            }
            else
            {
                // Optionally handle the case where no rows are returned
                ddlClass.Items.Add(new System.Web.UI.WebControls.ListItem("No classes available", ""));
            }

        }
        public void BindSection()
        {
            DataTable dt = new DataTable();
            dt = transactionBLL.GetSectionNamesByClassName(ddlClass.SelectedValue.ToString());

            ddlSection.Items.Clear(); // Clear any existing items

            if (dt.Rows.Count > 0)
            {
                ddlSection.DataSource = dt;
                ddlSection.DataValueField = "SectionName"; // Change this if ClassID is the value field
                ddlSection.DataTextField = "SectionName"; // Assuming ClassName is the display field
                ddlSection.DataBind(); // Bind the data to the dropdown

                // Optionally, re-insert the default item after data binding
                ddlSection.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Class", ""));
            }
            else
            {
                // Optionally handle the case where no rows are returned
                ddlSection.Items.Add(new System.Web.UI.WebControls.ListItem("No classes available", ""));
            }

        }
        protected void ddlentities_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newSize = Convert.ToInt32(ddlentities.SelectedValue);
            GridView1.PageSize = newSize;
            BindTransactionDetail();
        }
        public void BindTransactionDetail()
        {
            DataTable dt = new DataTable();

            // Retrieve the selected class and section values
            string className = string.IsNullOrWhiteSpace(ddlClass.SelectedValue) ? null : ddlClass.SelectedValue;
            string sectionName = string.IsNullOrWhiteSpace(ddlSection.SelectedValue) ? null : ddlSection.SelectedValue;

            // Parse the dates from the text boxes, handling possible format issues
            DateTime? fromDate = null;
            DateTime? toDate = null;

            if (DateTime.TryParse(TextBox1.Text, out DateTime parsedFromDate))
            {
                fromDate = parsedFromDate;
            }

            if (DateTime.TryParse(TextBox2.Text, out DateTime parsedToDate))
            {
                parsedToDate = parsedToDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                toDate = parsedToDate;
            }

            // Call the BLL method with the retrieved parameters
            dt = transactionBLL.GetFeeTransactionSummaryOffLine(className, sectionName, fromDate, toDate);

            // Store the DataTable in ViewState for sorting
            ViewState["TransactionData"] = dt;
            Session["UserDataTable"] = dt;
            // Bind the result to the GridView
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable dt = ViewState["TransactionData"] as DataTable;

                if (dt != null)
                {
                    DataView dv = dt.DefaultView;
                    string sortExpression = e.SortExpression;
                    string sortDirection = ViewState["SortDirection"] as string == "ASC" ? "DESC" : "ASC";

                    ViewState["SortDirection"] = sortDirection;
                    dv.Sort = sortExpression + " " + sortDirection;

                    GridView1.DataSource = dv;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred while sorting the data.');", true);
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Retrieve the values from the current row
                //decimal totFeeAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotFeeAmt"));
                //decimal fineAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FineAmt"));
                //decimal totRecAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotRecAmt"));
                decimal chequeAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ChequeAmt"));
                decimal cashRecAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CashRecAmt"));
                //decimal onlineAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OnlineAmt"));

                // Store the values in ViewState or local variables to sum them
                //if (ViewState["TotalFeeAmt"] == null) ViewState["TotalFeeAmt"] = 0m;
                //if (ViewState["TotalFineAmt"] == null) ViewState["TotalFineAmt"] = 0m;
                //if (ViewState["TotalRecAmt"] == null) ViewState["TotalRecAmt"] = 0m;
                if (ViewState["TotalChequeAmt"] == null) ViewState["TotalChequeAmt"] = 0m;
                if (ViewState["TotalCashRecAmt"] == null) ViewState["TotalCashRecAmt"] = 0m;
                //if (ViewState["TotalOnlineAmt"] == null) ViewState["TotalOnlineAmt"] = 0m;

                //ViewState["TotalFeeAmt"] = (decimal)ViewState["TotalFeeAmt"] + totFeeAmt;
                //ViewState["TotalFineAmt"] = (decimal)ViewState["TotalFineAmt"] + fineAmt;
                //ViewState["TotalRecAmt"] = (decimal)ViewState["TotalRecAmt"] + totRecAmt;
                ViewState["TotalChequeAmt"] = (decimal)ViewState["TotalChequeAmt"] + chequeAmt;
                ViewState["TotalCashRecAmt"] = (decimal)ViewState["TotalCashRecAmt"] + cashRecAmt;
                //ViewState["TotalOnlineAmt"] = (decimal)ViewState["TotalOnlineAmt"] + onlineAmt;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                // Set the footer values
                //e.Row.Cells[6].Text = ViewState["TotalFeeAmt"].ToString();
                //e.Row.Cells[7].Text = ViewState["TotalFineAmt"].ToString();
                //e.Row.Cells[8].Text = ViewState["TotalRecAmt"].ToString();
                e.Row.Cells[6].Text = ViewState["TotalChequeAmt"].ToString();
                e.Row.Cells[7].Text = ViewState["TotalCashRecAmt"].ToString();
                //e.Row.Cells[11].Text = ViewState["TotalOnlineAmt"].ToString();
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.BindTransactionDetail();
        }
        protected void LinkButtonExportExcel_Click(object sender, EventArgs e)
        {
            ExportData("Excel");
        }
        protected void LinkButtonExportpdf_Click(object sender, EventArgs e)
        {
            ExportData("PDF");
        }
        public void ExportData(string type)
        {
            // Sample GridView
            DataTable dtFromSession = Session["UserDataTable"] as DataTable;
            // Populated with data (example)

            // List of selected columns you want to extract (based on your GridView's TemplateField names)
            List<string> selectedColumns = new List<string> { "ScholarNo", "StudentName", "ClassName", "SectionName", "ReceiptNo", "ReceiptDt", "TotFeeAmt", "FineAmt", "TotRecAmt", "ChequeAmt", "CashRecAmt" };

            // Convert GridView to DataTable
            GridViewToDataTableConverter dtConverter = new GridViewToDataTableConverter();
            DataTable dt = dtConverter.GetSelectedColumnsDataTable(dtFromSession, selectedColumns);

            if (dt.Rows.Count > 0)
            {
                var columnNames = new List<string> { "Scholar No.", "Student Name", "Class", "Section", "Receipt No.", "Receipt Dt", "Total Fee Amt", "Fine Amt", "Total Receive Amt", "Cheque Amt", "Cash Receive Amt" };
                // var memoryStream = ExcelWorker.ConvertDataTableToExcelInMemory(dataTable, columnNames,"Harsh","School Master Report");

                string generatedBy = "DPS";
                string reportTitle = "Offline Transaction Data Report"; // Custom title
                if (type == "Excel")
                {
                    using (var excelStream = ExcelWorker.ConvertDataTableToExcelInMemory(dt, columnNames, generatedBy, reportTitle))
                    {
                        string fileName = $"OfflineTransaction_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", $"attachment; filename={fileName}");
                        Response.BinaryWrite(excelStream.ToArray());
                        Response.End();
                    }
                }
                if (type == "PDF")
                {
                    using (var pdfStream = PDFWorker.ConvertDataTableToPdfInMemory(dt, columnNames, selectedColumns, generatedBy, reportTitle))
                    {
                        string fileName = $"OfflineTransaction_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", $"attachment; filename={fileName}");
                        Response.BinaryWrite(pdfStream.ToArray());
                        Response.End();
                    }
                }

            }
            else
            {
                // Handle the case where the session data is null.
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", $"showMessage('No data to export', 'error');", true);
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSection();
            BindTransactionDetail();
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTransactionDetail();
        }

        protected void linkButtonfilter_Click(object sender, EventArgs e)
        {
            BindTransactionDetail();

        }
    }
}