using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DonorFlow
{
    public class CampaignController : ApiController
    {
        private readonly string connectionString = "Data Source=DESKTOP-KUTNUTJ\\SQLEXPRESS;Initial Catalog=DonorFlow_DB;Integrated Security=True;";

        [HttpGet]
        [Route("api/GetCampaignTitles")]
        public IHttpActionResult GetCampaignTitles()
        {
            try
            {
                List<object> campaignData = new List<object>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                    SELECT Campaign_Title, COUNT(*) AS Value
                    FROM Campaigns
                    GROUP BY Campaign_Title";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                campaignData.Add(new
                                {
                                    Title = reader["Campaign_Title"].ToString(),
                                    Value = Convert.ToInt32(reader["Value"])
                                });
                            }
                        }
                    }
                }

                return Ok(campaignData); // Returns JSON format automatically
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Return error details if something goes wrong
            }
        }

        [HttpGet]
        [Route("api/revenue")]
        public IHttpActionResult GetRevenueData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;

           string query = @"
                SELECT SUM(TH.[Transfer_Amount]) AS TotalAmount,
                       UT.[Role]
                FROM TransactionHistory TH
                LEFT JOIN User_tbl UT ON TH.[TransferedUser] = UT.[User_ID]
                GROUP BY UT.[Role];";

            List<object> data = new List<object>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        data.Add(new
                        {
                            Role = reader["Role"].ToString(),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"])
                        });
                    }
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                // Log exception details here
                return InternalServerError(ex);
            }

        }
        [HttpGet]
        [Route("api/campaignApproval")]
        public IHttpActionResult GetCampaignApprovalData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;
            string query = @"
            SELECT 
                CASE 
                    WHEN [IS_Approved] = '0' THEN 'Not Approved'
                    ELSE 'Approved'
                END AS Approval_Status,
                COUNT([IS_Approved]) AS Approval_Count
            FROM Campaigns
            GROUP BY [IS_Approved]";

            List<object> approvalData = new List<object>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    approvalData.Add(new
                    {
                        ApprovalStatus = reader["Approval_Status"].ToString(),
                        ApprovalCount = Convert.ToInt32(reader["Approval_Count"])
                    });
                }
            }

            return Ok(approvalData);
        }
        [HttpGet]
        [Route("api/campaignStatus")]
        public IHttpActionResult GetCampaignStatusData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;
            string query = @"
                            SELECT COUNT([Status]) AS CountofStatus,
                                   [Status] AS Status
                            FROM Campaigns
                            GROUP BY [Status]";

            List<object> approvalData = new List<object>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    approvalData.Add(new
                    {
                        ApprovalStatus = reader["Status"].ToString(),
                        ApprovalCount = Convert.ToInt32(reader["CountofStatus"])
                    });
                }
            }

            return Ok(approvalData);
        }
        [HttpGet]
        [Route("api/activeCampaigns")]
        public IHttpActionResult GetActiveCampaignsData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;
            string query = @"
            SELECT [Campaign_Title], [Donation_Goal], [EndDate] 
            FROM Campaigns
            WHERE [EndDate] >= CONVERT(VARCHAR, GETDATE(), 23)";

            List<object> campaignsData = new List<object>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    campaignsData.Add(new
                    {
                        CampaignTitle = reader["Campaign_Title"].ToString(),
                        DonationGoal = Convert.ToDecimal(reader["Donation_Goal"]),
                        EndDate = Convert.ToDateTime(reader["EndDate"])
                    });
                }
            }

            return Ok(campaignsData);
        }
    }
}