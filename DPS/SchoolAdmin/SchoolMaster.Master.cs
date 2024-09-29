using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPS.SchoolAdmin
{
    public partial class SchoolMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string currentPageUrl = Request.Url.AbsolutePath;
                string schoolName=Session["schoolName"].ToString();
                lblSchoolName.Text=schoolName;
                lblLogoText.Text=schoolName[0].ToString();
                // Highlight the menu item corresponding to the current page
                HighlightMenuItem(currentPageUrl);
            }
        }
        private void HighlightMenuItem(string currentPageUrl)
        {
            // Determine which menu item corresponds to the current page and highlight its <li> element
            if (currentPageUrl.EndsWith("SchoolDashboard.aspx"))
            {
                dashboard.Attributes["class"] = "nav-item active";
            }
            else if (currentPageUrl.EndsWith("InitialSync.aspx") || currentPageUrl.EndsWith("StudentSync.aspx") ||
                    currentPageUrl.EndsWith("FeeSync.aspx") )
            {
                master.Attributes["class"] = "nav-item active";
            }
            else if (currentPageUrl.EndsWith("OnlineTransaction.aspx") || currentPageUrl.EndsWith("OfflineTransaction.aspx") ||
                    currentPageUrl.EndsWith("BothTransaction.aspx"))
            {
                activity.Attributes["class"] = "nav-item active";
            }
        }
    }
}