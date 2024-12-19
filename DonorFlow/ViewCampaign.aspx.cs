using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static DonorFlow.DonationPayment;

namespace DonorFlow
{
    public partial class DonationPayment : System.Web.UI.Page
    {
        string UserId = string.Empty;
        string CampaignId = string.Empty;
        public string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=macro_campus_db;Integrated Security=True;");
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Get the master page as SiteMaster
                SiteMaster siteMaster = (SiteMaster)Page.Master;

                if (siteMaster != null)
                {
                    if (Session["role"] == null || string.IsNullOrEmpty(Session["role"].ToString()))
                    {
                        // Make LinkButton15 visible if the role is null or empty
                        siteMaster.LinkButton15Property.Visible = true;
                    }
                    else if (Session["role"].Equals("DonorFlow_User"))
                    {
                        // Set the text of LinkButton15 based on session data
                        siteMaster.LinkButton1Property.Text = Session["Full_Name"].ToString();
                        UserId = Session["User_ID"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions properly
                Response.Write(ex.Message);
            }
            if (!IsPostBack)
            {
                LoadCampaigns();
            }
        }

        [WebMethod]
        public static List<Dictionary<string, string>> GetCampaigns()
        {
            string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=DonorFlow_DB;Integrated Security=True;";
            string query = "SELECT [Campaign_Title], [Image_Path], [Description], [Donation_Goal], [EndDate], [Campaign_ID] FROM Campaigns";
            List<Dictionary<string, string>> campaigns = new List<Dictionary<string, string>>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var imagePath = reader["Image_Path"].ToString();
                    imagePath = imagePath.Replace("~", "");
                    // Resolve the server-side path to a client-side accessible URL.
                    string imageUrl = "https://localhost:44318" + imagePath;

                    var campaign = new Dictionary<string, string>
            {
                { "Campaign_Title", reader["Campaign_Title"].ToString() },
                { "Image_Path", imageUrl }, // Use the resolved URL here
                { "Description", reader["Description"].ToString() },
                { "Donation_Goal", reader["Donation_Goal"].ToString() },
                { "EndDate", DateTime.Parse(reader["EndDate"].ToString()).ToString("dd/MM/yyyy") },
                { "Campaign_ID", reader["Campaign_ID"].ToString() }

            };
                    campaigns.Add(campaign);
                }
            }
            return campaigns;
        }

        private void LoadCampaigns()
        {
            string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=DonorFlow_DB;Integrated Security=True;";
            string query = "SELECT [Campaign_Title], [Image_Path], [Description], [Donation_Goal], [EndDate], [Campaign_ID] FROM Campaigns";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string imagePath = reader["Image_Path"].ToString().Replace("~", "");
                    string imageUrl = "https://localhost:44318" + imagePath;

                    string formattedEndDate = DateTime.TryParse(reader["EndDate"].ToString(), out DateTime endDate)
                        ? endDate.ToString("dd MMM yyyy") // Example: "25 Dec 2024"
                        : "Invalid Date";

                    var campaignHtml = $@"
                <div class='campaign-card'>
                    <h3>{reader["Campaign_Title"]}</h3>
                    <img src='{imageUrl}' alt='{reader["Campaign_Title"]}' />
                    <p>{reader["Description"]}</p>
                    <h6>Donation Goal: ${reader["Donation_Goal"]}</h6>
                    <p>End Date: {formattedEndDate}</p>
                    <a href='CampaignInfo.aspx?CampaignId={HttpUtility.UrlEncode(reader["Campaign_ID"].ToString())}'>View Details</a>
                </div>";
                    campaignsContainer.InnerHtml += campaignHtml;
                }
            }
        }
        protected void ViewDetails_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string campaignId = btn.CommandArgument;

            // Redirect to the information page with the campaign title as a query parameter
            Response.Redirect($"CampaignInfo.aspx?CampaignId={HttpUtility.UrlEncode(campaignId)}");
        }
    }
}
