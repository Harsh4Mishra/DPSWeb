using System;

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

                lblSchoolName.Text = GetInitials(schoolName);
                lblMainSchoolName.Text = schoolName;
                lblLogoText.Text=schoolName[0].ToString();
                // Highlight the menu item corresponding to the current page
                HighlightMenuItem(currentPageUrl);
            }
        }
        public string GetInitials(string input)
        {
            // Split the input string by spaces
            string[] words = input.Split(' ');

            // Initialize a result string
            string result = "";

            // Loop through each word and take the first letter
            foreach (string word in words)
            {
                if (!string.IsNullOrEmpty(word)) // Check if the word is not empty
                {
                    result += char.ToUpper(word[0]); // Append the uppercase first letter
                }
            }

            return result;
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