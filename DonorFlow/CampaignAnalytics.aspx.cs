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
    public partial class CampaignAnalytics : System.Web.UI.Page
    {
        public string UserId { get; set; }
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
                BindTransactionData();

            }
        }
        private void BindTransactionData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                // Use a SQL query to select data from the table
                string query = @"SELECT 
                            CAM.[Campaign_ID],
                            CAM.[Campaign_Title],
                            CAM.[Donation_Goal],
                            FORMAT(CAM.[StartDate], 'yyyy-MM-dd') AS StartDate,
                            FORMAT(CAM.[EndDate], 'yyyy-MM-dd') AS EndDate,
                            CAM.[Status],
                            CASE
                                WHEN CAM.[IS_Approved] = '0' THEN 'Not Approved'
                                ELSE CAM.[IS_Approved]
                            END AS IS_Approved,
                            ISNULL(
                                (SELECT SUM(TH.[Transfer_Amount])
                                 FROM TransactionHistory TH
                                 WHERE TH.[Campaign_ID] = CAM.[Campaign_ID]
                                ), 0) AS [Transfer_Amount],
                            CASE
                                WHEN CAM.[Donation_Goal] <= 
                                     ISNULL(
                                        (SELECT SUM(TH.[Transfer_Amount])
                                         FROM TransactionHistory TH
                                         WHERE TH.[Campaign_ID] = CAM.[Campaign_ID]
                                        ), 0) THEN 'Completed Goal'
                                ELSE 'Incomplete Goal'
                            END AS Goal_Status,
                            CASE
                                WHEN CAM.[Donation_Goal] = 0 THEN '0.00%'
                                ELSE 
                                    CONCAT(
                                        ROUND(
                                            ISNULL(
                                                (SELECT SUM(TH.[Transfer_Amount])
                                                 FROM TransactionHistory TH
                                                 WHERE TH.[Campaign_ID] = CAM.[Campaign_ID]
                                                ), 0) * 100.0 / CAM.[Donation_Goal], 2), '%')
                            END AS [Progress_Percentage]
                        FROM Campaigns CAM
                        WHERE CAM.[Created User] = @userId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@userId", UserId);

                    // Execute the query and fill the data table
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        gvTransactions.DataSource = dt;
                        gvTransactions.DataBind();
                    }
                    else
                    {
                        // If no rows, display a message
                        gvTransactions.DataSource = null;
                        gvTransactions.DataBind();
                        lblNoTransactionMessage.Text = "No campaignss found.";
                        lblNoTransactionMessage.Visible = true;
                    }
                    // Close the connection
                    conn.Close();

                }
            }

        }
    }
}