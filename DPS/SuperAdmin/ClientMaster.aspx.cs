using DPS.SuperAdmin.SchoolClassFile;
using DPS.SuperAdmin.SchoolDatabaseClassFile;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace DPS.SuperAdmin
{
    public partial class ClientMaster : System.Web.UI.Page
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
            Response.Redirect("AddClient.aspx");
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
                SchoolBLL schoolBLL = new SchoolBLL();
                int result = schoolBLL.UpdateSchoolActive(checkedSchoolIds, isActives, updatedBy);

                // Check if the schools were updated successfully
                if (result > 0)
                {
                    // Notify success
                    string successScript = "alert('School records activated successfully!');";
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
            BindClient();
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
            BindClient();
        }
        protected void LinkButtonSync_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string commandArgument = btn.CommandArgument;
            Session["SyncClientID"] = commandArgument;

            string timestamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
            string database = "CID" + timestamp;
            SchoolDatabaseBLL schooldbBLL = new SchoolDatabaseBLL();
            string academicYear = GetCurrentAcademicYear();
            schooldbBLL.DeleteCurrentSchoolDatabase(int.Parse(commandArgument.ToString()), "SuperAdmin");
            schooldbBLL.AddSchoolDatabase(int.Parse(commandArgument.ToString()), database, "SuperAdmin", academicYear, true);



            string connectionString = "Server=DESKTOP-MB1QN8B\\SQLEXPRESS;Integrated Security=true;"; // Use appropriate connection string
            string dbName = database; // Name of the database to create
            /*string sqlFilePath = @"../Script/School.sql";*/ // Path to your .sql file
            string sqlFilePath = Server.MapPath("~/Script/School.sql");


            try
            {
                // Step 1: Create Database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand createDbCommand = new SqlCommand($"CREATE DATABASE {dbName}", connection);
                    createDbCommand.ExecuteNonQuery();
                }

                // Step 2: Execute .sql file
                string sql = File.ReadAllText(sqlFilePath);
                string[] sqlCommands = sql.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

                using (SqlConnection connection = new SqlConnection($"{connectionString}Database={dbName};"))
                {
                    connection.Open();
                    foreach (var commandText in sqlCommands)
                    {
                        string trimmedCommand = commandText.Trim(); // Trim whitespace
                        if (!string.IsNullOrEmpty(trimmedCommand)) // Check if not empty
                        {
                            using (SqlCommand executeSqlCommand = new SqlCommand(trimmedCommand, connection))
                            {
                                executeSqlCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
                SchoolBLL scBLL = new SchoolBLL();
                scBLL.UpdateSchoolDbSyncronized(int.Parse(commandArgument.ToString()), "SuperAdmin");
                string successScript = "alert('Database Created and Syncronized successfully!');";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);
                BindClient();
            }
            catch (Exception ex)
            {
                string successScript = "alert('" + ex.Message + "');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", successScript, true);
            }



            // Handle the LinkButton click event as needed
            // Response.Redirect("EditClient.aspx");
        }
        public string GetCurrentAcademicYear()
        {
            var currentDate = DateTime.Now;
            int year = currentDate.Year;

            if (currentDate.Month >= 4) // April or later
            {
                return $"{year}-{year + 1}";
            }
            else // Before April
            {
                return $"{year - 1}-{year}";
            }
        }
        protected void LinkButtonEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string commandArgument = btn.CommandArgument;
            Session["EditClientID"] = commandArgument;
            // Handle the LinkButton click event as needed
            Response.Redirect("EditClient.aspx");
        }
        protected void LinkButtonDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string commandArgument = btn.CommandArgument;
            Session["DeleteClientID"] = commandArgument;
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
                int schoolID = int.Parse(Session["DeleteClientID"].ToString());

                // Instantiate SchoolBLL and call the UpdateSchool method
                SchoolBLL schoolBLL = new SchoolBLL();
                int result = schoolBLL.DeleteSchool(schoolID, "Admin");

                // Check if the school was updated successfully
                if (result > 0)
                {
                    // Notify success
                    string successScript = "alert('School deleted successfully!');";
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
                SchoolBLL schoolBLL = new SchoolBLL();
                DataTable dt = schoolBLL.GetAllSchools();
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