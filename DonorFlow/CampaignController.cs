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
                                WHEN [IS_Approved] = 'Approved' THEN 'Approved'
                                WHEN [IS_Approved] = 'Rejected' THEN 'Rejected'
                                ELSE 'Unknown' -- Ensure a value is returned in the ELSE clause
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

        [HttpGet]
        [Route("api/donor/total")]
        public IHttpActionResult GetTotalDonors()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;
            try
            {
                int totalDonors = 0;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT([User_ID]) AS NumberofUsers FROM User_tbl WHERE [Role] = 'Donor'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        totalDonors = (int)command.ExecuteScalar();
                    }
                }

                return Ok(totalDonors);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("api/campaigncreator/total")]
        public IHttpActionResult GetTotalCampaignCreators()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;
            try
            {
                int totalcampaigncreators = 0;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT([User_ID]) AS NumberofUsers FROM User_tbl WHERE [Role] = 'Campaign Creator'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        totalcampaigncreators = (int)command.ExecuteScalar();
                    }
                }

                return Ok(totalcampaigncreators);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("api/admin/total")]
        public IHttpActionResult GetTotalAdmins()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;
            try
            {
                int totaladmins = 0;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT([User_ID]) AS NumberofUsers FROM User_tbl WHERE [Role] = 'Administrator'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        totaladmins = (int)command.ExecuteScalar();
                    }
                }

                return Ok(totaladmins);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("api/revenue/summary")]
        public IHttpActionResult GetRevenueSummary()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;
            try
            {
                decimal revenue = 0;
                string fromDate = string.Empty;
                string toDate = DateTime.Now.ToString("yyyy-MM-dd");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    SUM([Transfer_Amount]) AS Revenue,
                    CONVERT(VARCHAR(10), MIN([TransferedDate]), 120) AS FromDate,
                    CONVERT(VARCHAR(10), GETDATE(), 120) AS ToDate
                FROM 
                    TransactionHistory";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                revenue = reader.IsDBNull(0) ? 0 : reader.GetDecimal(0);
                                fromDate = reader.IsDBNull(1) ? toDate : reader.GetString(1);
                            }
                        }
                    }
                }

                return Ok(new { Revenue = revenue, FromDate = fromDate, ToDate = toDate });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [RoutePrefix("api/transaction")]
        public class TransactionController : ApiController
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;

            [HttpGet]
            [Route("revenue")]
            public IHttpActionResult GetRevenueData()
            {
                List<RevenueData> revenueDataList = new List<RevenueData>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT 
                                        [Transfer_Amount] AS Revenue,
                                        CONVERT(VARCHAR(10), [TransferedDate], 120) AS FromDate
                                    FROM
                                        TransactionHistory";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            revenueDataList.Add(new RevenueData
                            {
                                Revenue = Convert.ToDecimal(reader["Revenue"]),
                                FromDate = Convert.ToString(reader["FromDate"])
                            });
                        }
                    }
                }

                return Ok(revenueDataList);
            }
        }

        public class RevenueData
        {
            public decimal Revenue { get; set; }
            public string FromDate { get; set; }
        }

        [RoutePrefix("api")]
        public class UserCampaignController : ApiController
        {
            [HttpGet]
            [Route("userCampaignStatus")]
            public IHttpActionResult GetUserCampaignStatusData(string UserId)
            {
                Console.WriteLine($"UserId received: {UserId}");
                if (string.IsNullOrEmpty(UserId))
                {
                    return BadRequest("UserId is required");
                }

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;
                string query = @"
                    SELECT COUNT([Status]) AS CountofStatus, [Status] AS Status
                    FROM Campaigns
                    WHERE [Created User] = @UserId
                    GROUP BY [Status]";

                List<object> approvalData = new List<object>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", UserId);
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

        }

        [RoutePrefix("api/userCampaignApproval")]  // Different RoutePrefix for this controller
        public class UserCampaignApproveController : ApiController
        {
            [HttpGet]
            [Route("userCampaignApprovalStatus")]  // This will be "api/userCampaignApproval/userCampaignApprovalStatus"
            public IHttpActionResult GetUserCampaignApprovalData(string UserId)
            {
                Console.WriteLine($"UserId received: {UserId}");
                if (string.IsNullOrEmpty(UserId))
                {
                    return BadRequest("UserId is required");
                }

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;
                string query = @"
            SELECT 
                CASE 
                    WHEN [IS_Approved] = '0' THEN 'Not Approved'
                    WHEN [IS_Approved] = 'Approved' THEN 'Approved'
                    WHEN [IS_Approved] = 'Rejected' THEN 'Rejected'
                    ELSE 'Unknown'
                END AS Approval_Status,
                COUNT([IS_Approved]) AS Approval_Count
            FROM Campaigns
            WHERE [Created User] = @UserId
            GROUP BY [IS_Approved]";

                List<object> approvalData = new List<object>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", UserId);
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
        }

    }
}