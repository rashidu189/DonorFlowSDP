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
    public partial class CampaignCreatePage : System.Web.UI.Page
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
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string status = "Inactive";
            string isApproved = "0";
            DateTime createdDate = DateTime.Now;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
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

                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.CommandText = @"INSERT INTO Campaigns ([Campaign_Title],[Description],[Donation_Goal],[StartDate],[EndDate],[Status],[IS_Approved],[Image_Path],[Created User],[Created Date])
                                VALUES (@Title,@Description,@Goal,@StartDate,@EndDate,@Status,@IsApproved,@ImagePath,@UserId,@CreatedDate)";

                            cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                            cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                            cmd.Parameters.AddWithValue("@Goal", txtGoal.Text.Trim());
                            cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtStartDate.Text.Trim()).ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtEndDate.Text.Trim()).ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@Status", status);
                            cmd.Parameters.AddWithValue("@IsApproved", isApproved);
                            cmd.Parameters.AddWithValue("@ImagePath", "~/Campaign_Images/" + fileUpload.FileName);
                            cmd.Parameters.AddWithValue("@UserId", UserId); // Use Session or dynamic UserId
                            cmd.Parameters.AddWithValue("@CreatedDate", createdDate);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                conn.Close();
            }

            string imageUrl = "Resources/success.png";
            string message = $"<img src='{imageUrl}' alt='Success' style='width:20px;height:20px;' /> " + txtTitle.Text.Trim() + " Campaign has been successfully Created.";
            Session["AlertMessage"] = message;
            Session["AlertType"] = "alert-success";

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Clear form fields
           /* txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtGoal.Text = string.Empty;
            txtDeadline.Text = string.Empty;
            fileImage.Attributes.Clear();

            // Reset campaign details display
            lblTitleV.Text = "Campaign Title: [Enter Title]";
            lblDescriptionV.Text = "Description: [Enter Description]";
            lblGoalV.Text = "Donation Goal: $0.00";
            lblDeadlineV.Text = "Deadline: MM/DD/YYYY";
            imgCampaign.ImageUrl = "";*/
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand fetchCmd = conn.CreateCommand())
                {
                    fetchCmd.CommandType = System.Data.CommandType.Text;
                    fetchCmd.CommandText = @"SELECT TOP 1 [Campaign_Title], [Description], [Donation_Goal], [StartDate], [EndDate], [Image_Path]
                                     FROM Campaigns
                                     WHERE [Created User] = @UserId
                                     ORDER BY [Created Date] DESC";

                    fetchCmd.Parameters.AddWithValue("@UserId", UserId); // Use Session or dynamic UserId

                    using (SqlDataReader reader = fetchCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblCampaignTitle.Text = reader["Campaign_Title"].ToString();
                            lblDescriptionC.Text = reader["Description"].ToString();
                            lblDonationGoal.Text = "$" + reader["Donation_Goal"].ToString();
                            lblDates.Text = "From " + Convert.ToDateTime(reader["StartDate"]).ToString("dd MMM yyyy") +
                                            " to " + Convert.ToDateTime(reader["EndDate"]).ToString("dd MMM yyyy");

                            string imagePath = reader["Image_Path"].ToString();
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
                }
            }
            // Fetch the latest inserted campaign

        }
    }
}