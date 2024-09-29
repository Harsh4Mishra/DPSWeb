using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPS.SuperAdmin
{
    public partial class SuperAdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string currentPageUrl = Request.Url.AbsolutePath;

                // Highlight the menu item corresponding to the current page
                HighlightMenuItem(currentPageUrl);
            }
        }
        private void HighlightMenuItem(string currentPageUrl)
        {
            // Determine which menu item corresponds to the current page and highlight its <li> element
            if (currentPageUrl.EndsWith("IndexPage.aspx"))
            {
                dashboard.Attributes["class"] = "nav-item active";
            }
            else if (currentPageUrl.EndsWith("ClientMaster.aspx") || currentPageUrl.EndsWith("AddClient.aspx") || currentPageUrl.EndsWith("EditClient.aspx") ||
                    currentPageUrl.EndsWith("DatabaseMaster.aspx") || currentPageUrl.EndsWith("AddDatabase.aspx") || currentPageUrl.EndsWith("EditDatabase.aspx") ||
                    currentPageUrl.EndsWith("PaymentMaster.aspx") || currentPageUrl.EndsWith("AddPayment.aspx") || currentPageUrl.EndsWith("EditPayment.aspx"))
            {
                master.Attributes["class"] = "nav-item active";
            }
            else if (currentPageUrl.EndsWith("Page3.aspx"))
            {
                activity.Attributes["class"] = "nav-item active";
            }
        }
    }
}