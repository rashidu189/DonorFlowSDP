using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DonorFlow
{
    public partial class CampaignDetails : System.Web.UI.Page
    {
        string CampaignId = string.Empty;
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
                        siteMaster.LinkButton2Property.Visible = true;
                    }
                    else if (Session["role"].Equals("DonorFlow_User"))
                    {
                        // Set the text of LinkButton15 based on session data
                        siteMaster.LinkButton2Property.Text = Session["Full_Name"].ToString();

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
                CampaignId = Request.QueryString["campaignId"];
                string Tittle = Request.QueryString["campaignTittle"];
                CampaignNDetails();

            }
        }
        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            CampaignId = Request.QueryString["campaignId"];

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (fileUpload.HasFile)
                    {
                        // Check if the uploaded file is an image (optional)
                        string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                        if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif")
                        {
                            // Define the directory to save the uploaded image
                            string folderPath = Server.MapPath("~/Campaign_Images/");
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath); // Create the directory if it doesn't exist
                            }

                            // Define the full path of the uploaded file
                            string filePath = Path.Combine(folderPath, fileUpload.FileName);

                            // Save the uploaded image to the server
                            fileUpload.SaveAs(filePath);

                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandType = System.Data.CommandType.Text;

                                cmd.CommandText = @"UPDATE Campaigns SET Campaign_Title = @CampaignTittle, Description = @Description, Donation_Goal = @DonationGoal, StartDate = @StartedDate, EndDate = @EndDate, Status = @CampaignStatus, Image_Path = @ImagePath WHERE Campaign_ID = @CampaignId";

                                cmd.Parameters.AddWithValue("@CampaignTittle", txtTitle.Text.Trim());
                                cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                                cmd.Parameters.AddWithValue("@DonationGoal", txtGoal.Text.Trim());
                                cmd.Parameters.AddWithValue("@StartedDate", DateTime.Parse(txtStartDate.Text.Trim()).ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtEndDate.Text.Trim()).ToString("yyyy-MM-dd"));

                                cmd.Parameters.AddWithValue("@CampaignStatus", DStatus.SelectedValue);
                                cmd.Parameters.AddWithValue("@ImagePath", "~/Campaign_Images/" + fileUpload.FileName);

                                // Use the dynamic UserId from Session instead of hardcoding
                                cmd.Parameters.AddWithValue("@CampaignId", CampaignId);

                                cmd.ExecuteNonQuery();

                                string imageUrl = "Resources/success.png";
                                string message = $"<img src='{imageUrl}' alt='Success' style='width:20px;height:20px;' /> User Account information has been successfully updated.";
                                Session["AlertMessage"] = message;
                                Session["AlertType"] = "alert-success";

                                Response.Redirect(Request.RawUrl);
                            }
                        }                 

                    }
                    else
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = System.Data.CommandType.Text;

                            cmd.CommandText = @"UPDATE Campaigns SET Campaign_Title = @CampaignTittle, Description = @Description, Donation_Goal = @DonationGoal, StartDate = @StartedDate, EndDate = @EndDate, Status = @CampaignStatus WHERE Campaign_ID = @CampaignId";

                            cmd.Parameters.AddWithValue("@CampaignTittle", txtTitle.Text.Trim());
                            cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                            cmd.Parameters.AddWithValue("@DonationGoal", txtGoal.Text.Trim());
                            cmd.Parameters.AddWithValue("@StartedDate", DateTime.Parse(txtStartDate.Text.Trim()).ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtEndDate.Text.Trim()).ToString("yyyy-MM-dd"));

                            cmd.Parameters.AddWithValue("@CampaignStatus", DStatus.SelectedValue);

                            // Use the dynamic UserId from Session instead of hardcoding
                            cmd.Parameters.AddWithValue("@CampaignId", CampaignId);

                            cmd.ExecuteNonQuery();

                            string imageUrl = "Resources/success.png";
                            string message = $"<img src='{imageUrl}' alt='Success' style='width:20px;height:20px;' /> User Account information has been successfully updated.";
                            Session["AlertMessage"] = message;
                            Session["AlertType"] = "alert-success";

                            Response.Redirect(Request.RawUrl);

                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }
        }
        protected void ApproveBtn_Click(object sender, EventArgs e)
        {
            CampaignId = Request.QueryString["campaignId"];

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"UPDATE Campaigns SET IS_Approved = 'Approved', Status = 'Active' WHERE Campaign_ID = @CampaignId";
                    cmd.Parameters.AddWithValue("@CampaignId", CampaignId);
                    cmd.ExecuteNonQuery();

                    string imageUrl = "Resources/success.png";
                    string message = $"<img src='{imageUrl}' alt='Success' style='width:20px;height:20px;' /> This Campaign is Already Approved.";
                    Session["AlertMessage"] = message;
                    Session["AlertType"] = "alert-success";

                }
                conn.Close();
            }
        }
        protected void RejectBtn_Click(object sender, EventArgs e)
        {
            CampaignId = Request.QueryString["campaignId"];

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"UPDATE Campaigns SET IS_Approved = 'Rejected', Status = 'Inactive' WHERE Campaign_ID = @CampaignId";
                    cmd.Parameters.AddWithValue("@CampaignId", CampaignId);
                    cmd.ExecuteNonQuery();

                    string imageUrl = "Resources/error.png";
                    string message = $"<img src='{imageUrl}' alt='Danger' style='width:20px;height:20px;' /> This Campaign is Already Rejected.";
                    Session["AlertMessage"] = message;
                    Session["AlertType"] = "alert-danger";

                }
                conn.Close();
            }
        }
        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            CampaignId = Request.QueryString["campaignId"];

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"DELETE FROM Campaigns WHERE Campaign_ID = @CampaignId";
                    cmd.Parameters.AddWithValue("@CampaignId", CampaignId);
                    cmd.ExecuteNonQuery();

                    string imageUrl = "Resources/error.png";
                    string message = $"<img src='{imageUrl}' alt='Danger' style='width:20px;height:20px;' /> This Campaign is Already Deleted.";
                    Session["AlertMessage"] = message;
                    Session["AlertType"] = "alert-danger";

                    Response.Redirect("ManageCampaigns.aspx");
                }
                conn.Close();
            }
        }
        public void CampaignNDetails()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = @"SELECT [Campaign_ID],[Campaign_Title],[Description],[IS_Approved],[Status],[Created User],[Donation_Goal],[StartDate],[EndDate],[Created Date],[Image_Path] FROM Campaigns WHERE Campaign_ID = @CampaignId";

                    cmd.Parameters.AddWithValue("@CampaignId", CampaignId);

                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        txtId.Text = dr.GetValue(0).ToString().Trim();
                        txtTitle.Text = dr.GetValue(1).ToString().Trim();
                        txtDescription.Text = dr.GetValue(2).ToString().Trim();
                        string IsApproved = dr.GetValue(3).ToString().Trim();
                        if(IsApproved== "0")
                        {
                            txtIsApproved.Text = "Pending Approvel";
                        }
                        else
                        {
                            txtIsApproved.Text = dr.GetValue(3).ToString().Trim();
                        }

                        string campaignStatus = dr.GetValue(4).ToString().Trim();
                        DStatus.SelectedValue = campaignStatus;
                        txtCreatedUser.Text = dr.GetValue(5).ToString().Trim();
                        txtGoal.Text = dr.GetValue(6).ToString().Trim();
                        DateTime startDate = Convert.ToDateTime(dr.GetValue(7).ToString().Trim());
                        txtStartDate.Text = startDate.ToString("yyyy-MM-dd").Trim();
                        DateTime endDate = Convert.ToDateTime(dr.GetValue(8).ToString().Trim());
                        txtEndDate.Text = endDate.ToString("yyyy-MM-dd").Trim();
                        DateTime createdDate = Convert.ToDateTime(dr.GetValue(9).ToString().Trim());
                        txtCreatedDate.Text = createdDate.ToString("yyyy-MM-dd").Trim();

                        string imagePath = dr.GetValue(10).ToString().Trim();
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            imgDisplay.ImageUrl = imagePath; // Set the image source dynamically
                        }
                        else
                        {
                            imgDisplay.ImageUrl = "~/images/default.jpg"; // Fallback for missing image
                        }

                    }

                }
                conn.Close();
            }
        }
    }
}