﻿using DPS.SuperAdmin.SchoolClassFile;
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
using DPS.SuperAdmin.SchoolDatabaseClassFile;
using DPS.Encryption;

namespace DPS.SuperAdmin
{
    public partial class SchoolDatabaseMaster : System.Web.UI.Page
    {
        private DataTable dataTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.PageSize = 5;
                //InitializeDataTable();
                BindClient();
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
            this.BindClient();
        }
        protected void LinkButtonAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddSchoolDatabase.aspx");
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
                SchoolDatabaseBLL schoolBLL = new SchoolDatabaseBLL();
                int result = schoolBLL.UpdateSchoolDatabaseActive(checkedSchoolIds, isActives, updatedBy);

                // Check if the schools were updated successfully
                if (result > 0)
                {
                    // Notify success
                    string successScript = "alert('School Database records activated successfully!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);
                }
                else
                {
                    // Notify failure
                    string failureScript = "alert('Failed to activate school database records.');";
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
            BindClient();
        }
        protected void LinkButtonExportExcel_Click(object sender, EventArgs e)
        {
            ExportData("Excel");
        }
        protected void LinkButtonExportpdf_Click(object sender, EventArgs e)
        {
            ExportData("PDF");
        }
        protected void ddlentities_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newSize = Convert.ToInt32(ddlentities.SelectedValue);
            GridView1.PageSize = newSize;
            BindClient();
        }
        protected void LinkButtonEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string commandArgument = btn.CommandArgument;
            Session["EditClientDatabaseID"] = commandArgument;
            // Handle the LinkButton click event as needed
            Response.Redirect("EditSchoolDatabase.aspx");
        }
        protected void LinkButtonDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string commandArgument = btn.CommandArgument;
            Session["DeleteClientDatabaseID"] = commandArgument;
            // Handle the LinkButton click event as needed

            string script = "$('#mymodal').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
            BindClient();
            //Response.Redirect("EditGroup.aspx");
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                int schoolID = int.Parse(Session["DeleteClientDatabaseID"].ToString());

                // Instantiate SchoolBLL and call the UpdateSchool method
                SchoolDatabaseBLL schoolBLL = new SchoolDatabaseBLL();
                int result = schoolBLL.DeleteSchoolDatabase(schoolID, "Admin");

                // Check if the school was updated successfully
                if (result > 0)
                {
                    // Notify success
                    string successScript = "alert('School Database deleted successfully!');";
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
            BindClient();
        }
        #endregion

        #region methods
        protected string GetImageUrl(object logo)
        {
            string logoPath = logo.ToString();
            // Assuming images are stored in a folder named "Images" in the root of your web application
            return string.Format("~/{0}", logoPath);
        }

        public void BindClient()
        {
            try
            {
                SchoolDatabaseBLL schoolDatabaseBLL = new SchoolDatabaseBLL();
                DataTable dt = schoolDatabaseBLL.GetAllSchoolDatabases();
                Session["SchoolDatabase"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred while retrieving the data.');", true);
            }

        }
        public void ExportData(string type)
        {
            // Sample GridView
            DataTable dtFromSession = Session["SchoolDatabase"] as DataTable;
            // Populated with data (example)

            // List of selected columns you want to extract (based on your GridView's TemplateField names)
            List<string> selectedColumns = new List<string> { "NAME", "DATABASE_NAME", "IS_IN_USED", "ACADEMIC_YEAR" };

            // Convert GridView to DataTable
            GridViewToDataTableConverter dtConverter = new GridViewToDataTableConverter();
            DataTable dt = dtConverter.GetSelectedColumnsDataTable(dtFromSession, selectedColumns);

            if (dt.Rows.Count > 0)
            {
                var columnNames = new List<string> { "NAME", "DATABASE NAME", "CURRENTLY USED", "ACADEMIC YEAR"};
                // var memoryStream = ExcelWorker.ConvertDataTableToExcelInMemory(dataTable, columnNames,"Harsh","SchoolDataBase Master Report");

                string generatedBy = "Harsh";
                string reportTitle = "SchoolDataBase Data Report"; // Custom title
                if (type == "Excel")
                {
                    using (var excelStream = ExcelWorker.ConvertDataTableToExcelInMemory(dt, columnNames, generatedBy, reportTitle))
                    {
                        string fileName = $"SchoolDataBaseData_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
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
                        string fileName = $"SchoolDataBaseData_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
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

        #endregion
    }
}