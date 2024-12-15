using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
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
                LoadImage();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand fetchCmd = conn.CreateCommand())
                {
                    fetchCmd.CommandType = System.Data.CommandType.Text;
                    fetchCmd.CommandText = @"SELECT [Image_Path]
                                     FROM Campaigns
                                     WHERE [Created User] = DF00003";


                    using (SqlDataReader reader = fetchCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                        }
                    }
                }
            }


        }

        // Method to fetch and display the image
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            /*
            if (fileUpload.HasFile)
            {
                try
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

                        // Display success message
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "Image uploaded successfully!";
                    }
                    else
                    {
                        lblMessage.Text = "Please upload a valid image file (.jpg, .jpeg, .png, .gif).";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                }
            }
            else
            {
                lblMessage.Text = "Please select a file to upload.";
            }*/


        }
        private void LoadImage()
        {
            
            string query = "Select [Image_Path] from Campaigns where ID = 3";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //cmd.Parameters.AddWithValue("@Condition", "value"); // Replace with your actual condition
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string imagePath = reader["Image_Path"].ToString();
                        imgDisplay.ImageUrl = imagePath; // Assign to an Image control
                    }

                    reader.Close();
                }
            }
        }

    }
}
