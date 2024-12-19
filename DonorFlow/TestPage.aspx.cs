using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DonorFlow
{
    public partial class TestPage : System.Web.UI.Page
    {
        public string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCampaignData();
            }
        }

        private void LoadCampaignData()
        {
            // Database connection and query
            string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=DonorFlow_DB;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT CAM.Campaign_ID, CAM.Donation_Goal, 
                   COALESCE(SUM(TH.Transfer_Amount), 0) AS TotalPaid_Amount,
                   CASE 
                       WHEN CAM.Donation_Goal > 0 THEN (COALESCE(SUM(TH.Transfer_Amount), 0) / CAM.Donation_Goal) * 100
                       ELSE 0
                   END AS ProgressPercentage
            FROM Campaigns CAM
            LEFT JOIN dbo.TransactionHistory TH ON CAM.Campaign_ID = TH.Campaign_ID
            GROUP BY CAM.Campaign_ID, CAM.Donation_Goal";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

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
                        Campaign ID: {row["Campaign_ID"]}<br />
                        Donation Goal: {donationGoal:C}<br />
                        Total Paid Amount: {totalPaidAmount:C}
                    </div>
                    <div class='progress-container'>
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
