using DPS.SuperAdmin.SchoolClassFile;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPS.SuperAdmin.PaymentConfigurationClassFIle;

namespace DPS.SuperAdmin
{
    public partial class PaymentConfigurationMaster : System.Web.UI.Page
    {
        private DataTable dataTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.PageSize = 5;
                //InitializeDataTable();
                BindSchoolPaymentConfiguration();
            }
        }

        #region Events
        protected void LinkButton_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string commandArgument = btn.CommandArgument;
            // Handle the LinkButton click event as needed
        }
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)GridView1.DataSource;

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
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.BindSchoolPaymentConfiguration();
        }
        protected void LinkButtonAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddPaymentConfiguration.aspx");
        }
        protected void LinkButtonPublish_Click(object sender, EventArgs e)
        {


            try
            {
                List<int> checkedSchoolIds = new List<int>();
                List<bool> isActives = new List<bool>();

                foreach (GridViewRow row in GridView1.Rows)
                {
                    // Find the CheckBox in the current row
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkIsActive");

                    if (chkSelect != null)
                    {
                        // Retrieve the School ID from the DataKeys
                        Label lblId = (Label)row.FindControl("lblId");
                        int schoolId = Convert.ToInt32(lblId.Text);
                        checkedSchoolIds.Add(schoolId);
                        bool isActive = chkSelect.Checked == true ? true : false;
                        isActives.Add(isActive);

                    }
                }
                string updatedBy = "Admin"; // Example value for updatedBy

                // Instantiate SchoolBLL and call the UpdateSchoolActive method
                SchoolPaymentConfigurationBLL schoolBLL = new SchoolPaymentConfigurationBLL();
                int result = schoolBLL.UpdateSchoolPaymentConfigurationActive(checkedSchoolIds, isActives, updatedBy);

                // Check if the schools were updated successfully
                if (result > 0)
                {
                    // Notify success
                    string successScript = "alert('Payment Configuration records activated successfully!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);
                }
                else
                {
                    // Notify failure
                    string failureScript = "alert('Failed to activate school records.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "FailureAlert", failureScript, true);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and notify the user
                // LogException(ex); // Implement a logging method here
                string errorScript = $"alert('An error occurred: {ex.Message}');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", errorScript, true);
            }
            BindSchoolPaymentConfiguration();
        }
        protected void LinkButtonExportExcel_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }
        protected void LinkButtonExportpdf_Click(object sender, EventArgs e)
        {
            ExportToPDF();
        }
        protected void ddlentities_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newSize = Convert.ToInt32(ddlentities.SelectedValue);
            GridView1.PageSize = newSize;
            BindSchoolPaymentConfiguration();
        }
        protected void LinkButtonEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string commandArgument = btn.CommandArgument;
            Session["EditClientPGID"] = commandArgument;
            // Handle the LinkButton click event as needed
            Response.Redirect("EditPaymentConfiguration.aspx");
        }
        protected void LinkButtonDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string commandArgument = btn.CommandArgument;
            Session["DeleteClientPGID"] = commandArgument;
            // Handle the LinkButton click event as needed

            string script = "$('#mymodal').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
            BindSchoolPaymentConfiguration();
            //Response.Redirect("EditGroup.aspx");
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                int schoolID = int.Parse(Session["DeleteClientPGID"].ToString());

                // Instantiate SchoolBLL and call the UpdateSchool method
                SchoolPaymentConfigurationBLL schoolBLL = new SchoolPaymentConfigurationBLL();
                int result = schoolBLL.DeleteSchoolPaymentConfiguration(schoolID, "Admin");

                // Check if the school was updated successfully
                if (result > 0)
                {
                    // Notify success
                    string successScript = "alert('School Payment Configuration deleted successfully!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);

                }
                else
                {
                    // Notify failure
                    string failureScript = "alert('Failed to delete school.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "FailureAlert", failureScript, true);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and notify the user
                // LogException(ex); // Implement a logging method here
                string errorScript = $"alert('An error occurred: {ex.Message}');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", errorScript, true);
            }
            BindSchoolPaymentConfiguration();
        }
        #endregion

        #region methods
        protected string GetImageUrl(object logo)
        {
            string logoPath = logo.ToString();
            // Assuming images are stored in a folder named "Images" in the root of your web application
            return string.Format("~/{0}", logoPath);
        }

        public void BindSchoolPaymentConfiguration()
        {
            try
            {
                SchoolPaymentConfigurationBLL schoolBLL = new SchoolPaymentConfigurationBLL();
                DataTable dt = schoolBLL.GetAllSchoolPaymentConfigurations();
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred while retrieving the data.');", true);
            }

        }

        private void ExportGridToExcel()
        {
            //ExportUtility.ExportToExcel(GridView1, "Employee" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Client" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView1.GridLines = GridLines.Both;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.RenderControl(htmltextwrtter);

            // Get the HTML string
            string html = strwritter.ToString();

            // Find and remove the last column from both header and data rows
            int lastColumnIndex = html.LastIndexOf("<td");
            if (lastColumnIndex != -1)
            {
                int endIndex = html.IndexOf("</tr>", lastColumnIndex);
                if (endIndex != -1)
                {
                    // Remove the last column from the header row
                    int headerEndIndex = html.IndexOf("</tr>", 0);
                    html = html.Remove(lastColumnIndex, endIndex - lastColumnIndex);

                    // Remove the corresponding cell from each data row
                    int dataIndex = html.IndexOf("<tr>", headerEndIndex);
                    while (dataIndex != -1)
                    {
                        int dataEndIndex = html.IndexOf("</tr>", dataIndex);
                        if (dataEndIndex != -1)
                        {
                            html = html.Remove(lastColumnIndex, dataEndIndex - dataIndex);
                        }
                        dataIndex = html.IndexOf("<tr>", dataIndex + 1);
                    }
                }
            }

            Response.Write(html);
            //Response.Write(strwritter.ToString());
            Response.End();
        }
        protected void ExportToPDF()
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=GridViewData.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.AllowPaging = false;
            GridView1.DataBind();
            GridView1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A2, 7f, 7f, 7f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."
        }

        #endregion
    }
}