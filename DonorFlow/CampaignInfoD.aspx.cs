using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DonorFlow
{
    public partial class CampaignInfoD : System.Web.UI.Page
    {
        string UserId = string.Empty;
        public string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;
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
                        siteMaster.LinkButton15Property.Text = Session["Full_Name"].ToString();
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
                string campaignId = Request.QueryString["CampaignId"];
                if (!string.IsNullOrEmpty(campaignId))
                {
                    LoadCampaignDetails(campaignId);
                    LoadCampaignData(campaignId);
                }
            }
        }
        private void LoadCampaignDetails(string campaignId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT [Campaign_Title], [Image_Path], [Description], [Donation_Goal], [StartDate], [EndDate] FROM Campaigns WHERE [Campaign_ID] = @CampaignID";

                // Open the connection (only once)
                conn.Open();

                // Use SqlCommand to execute the query with parameters
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CampaignID", campaignId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtTitle1.Text = reader["Campaign_Title"].ToString();
                        string imagePath = reader["Image_Path"].ToString();
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            imgDisplay.ImageUrl = imagePath; // Set the image source dynamically
                        }
                        else
                        {
                            imgDisplay.ImageUrl = "~/images/default.jpg"; // Fallback for missing image
                        }
                        txtDescription1.Text = reader["Description"].ToString();
                        txtGoal1.Text = "$" + reader["Donation_Goal"].ToString();
                        txtStartDate1.Text = DateTime.Parse(reader["StartDate"].ToString()).ToString("dd MMM yyyy");
                        txtEndDate1.Text = DateTime.Parse(reader["EndDate"].ToString()).ToString("dd MMM yyyy");
                    }
                }
            }

        }
        protected void btnDonateNow_Click(object sender, EventArgs e)
        {

            string donateAmount = DAmount.Text.ToString();
            string campaignId = Request.QueryString["CampaignId"];
            string userId = Session["User_ID"].ToString().Trim();


            // Construct the query string with multiple parameters
            string redirectUrl = $"PaymentPage.aspx?CampaignId={HttpUtility.UrlEncode(campaignId)}&UserId={HttpUtility.UrlEncode(userId)}&DonateAmount={donateAmount}";

            // Redirect with the constructed URL
            Response.Redirect(redirectUrl);


        }
        protected void TestPay_Click(object sender, EventArgs e)
        {
            float donateAmount = float.Parse(DAmount.Text);
            string campaignId = Request.QueryString["CampaignId"];
            string userId = Session["User_ID"].ToString().Trim();
            DateTime transferedDate = DateTime.Now;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"INSERT INTO TransactionHistory ([Campaign_ID], [TransferedUser], [Transfer_Amount], [TransferedDate])
                                VALUES (@CampaignID,@TransferedUser,@TransferAmount,@TransfereDate)";

                    cmd.Parameters.AddWithValue("@CampaignID", campaignId);
                    cmd.Parameters.AddWithValue("@TransferedUser", userId);
                    cmd.Parameters.AddWithValue("@TransferAmount", donateAmount);
                    cmd.Parameters.AddWithValue("@TransfereDate", transferedDate);

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    string imageUrl = "Resources/success.png";
                    string message = $"<img src='{imageUrl}' alt='Success' style='width:20px;height:20px;' /> Your Transaction is Successfully.";
                    Session["AlertMessage"] = message;
                    Session["AlertType"] = "alert-success";

                    Response.Redirect(Request.RawUrl);
                }
            }

        }
        private void LoadCampaignData(string campaignId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    // Fixed the WHERE clause to include the campaignId parameter
                    cmd.CommandText = @"
            SELECT CAM.Campaign_ID, CAM.Donation_Goal, 
                   COALESCE(SUM(TH.Transfer_Amount), 0) AS TotalPaid_Amount,
                   CASE 
                       WHEN CAM.Donation_Goal > 0 THEN (COALESCE(SUM(TH.Transfer_Amount), 0) / CAM.Donation_Goal) * 100
                       ELSE 0
                   END AS ProgressPercentage
            FROM Campaigns CAM
            LEFT JOIN dbo.TransactionHistory TH ON CAM.Campaign_ID = TH.Campaign_ID
            WHERE CAM.Campaign_ID = @CampaignId
            GROUP BY CAM.Campaign_ID, CAM.Donation_Goal";

                    // Adding the campaignId parameter to the SQL command
                    cmd.Parameters.AddWithValue("@CampaignId", campaignId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader); // Corrected part to load data into DataTable

                    // Dynamically generate HTML for each campaign
                    string htmlContent = "";
                    foreach (DataRow row in dt.Rows)
                    {
                        decimal donationGoal = Convert.ToDecimal(row["Donation_Goal"]);
                        decimal totalPaidAmount = Convert.ToDecimal(row["TotalPaid_Amount"]);
                        decimal progressPercentage = Convert.ToDecimal(row["ProgressPercentage"]);

                        // Calculate the progress percentage (if needed)
                        progressPercentage = donationGoal > 0 ? (totalPaidAmount / donationGoal) * 100 : 0;



                        // Build the HTML content for each campaign
                        htmlContent += $@"
                <div class='campaign-container'>
                    <div class='campaign-header'>
                        <label style=""font-weight: bold;"">Total Paid Amount:</label> {totalPaidAmount:C}
                    </div>
                    <div class='progress-container' style=""margin-top: 20px;"">
                        <div class='progress-bar' style='width: {progressPercentage}%;'>
                            {progressPercentage:0.##}%
                        </div>
                    </div>
                </div>";
                    }

                    // Set the dynamically generated HTML content to the Literal control
                    litCampaigns.Text = htmlContent;
                }
            }
        }

    }
}