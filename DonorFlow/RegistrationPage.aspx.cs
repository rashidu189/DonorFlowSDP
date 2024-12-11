using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace DonorFlow
{
    public partial class RegistrationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            // Server-side validation
            if (string.IsNullOrEmpty(txtFullName.Text.Trim()))
            {
                string imageUrl = "Resources/error.png";
                string message = $"<img src='{imageUrl}' alt='Danger Image' style='width:20px;height:20px;' /> Full name is required.";
                Session["AlertMessage"] = message;
                Session["AlertType"] = "alert-danger";
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                string imageUrl = "Resources/error.png";
                string message = $"<img src='{imageUrl}' alt='Danger Image' style='width:20px;height:20px;' /> Email Address is required.";
                Session["AlertMessage"] = message;
                Session["AlertType"] = "alert-danger";
                return;
            }

            if (string.IsNullOrEmpty(txtPhoneNumber.Text.Trim()) || txtPhoneNumber.Text.Length != 10)
            {
                string imageUrl = "Resources/error.png";
                string message = $"<img src='{imageUrl}' alt='Danger Image' style='width:20px;height:20px;' /> Please enter a valid 10-digit Phone Number.";
                Session["AlertMessage"] = message;
                Session["AlertType"] = "alert-danger";
                return;
            }

            if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
            {
                string imageUrl = "Resources/error.png";
                string message = $"<img src='{imageUrl}' alt='Danger Image' style='width:20px;height:20px;' /> Address is required.";
                Session["AlertMessage"] = message;
                Session["AlertType"] = "alert-danger";
                return;
            }
            if (string.IsNullOrEmpty(txtpwd.Text.Trim()))
            {
                string imageUrl = "Resources/error.png";
                string message = $"<img src='{imageUrl}' alt='Danger Image' style='width:20px;height:20px;' /> Password is required.";
                Session["AlertMessage"] = message;
                Session["AlertType"] = "alert-danger";
                return;
            }
            if (string.IsNullOrEmpty(ctxtpwd.Text.Trim()))
            {
                string imageUrl = "Resources/error.png";
                string message = $"<img src='{imageUrl}' alt='Danger Image' style='width:20px;height:20px;' /> Confirm Password is required.";
                Session["AlertMessage"] = message;
                Session["AlertType"] = "alert-danger";
                return;
            }
            if (string.IsNullOrEmpty(txtDateOfBirth.Text.Trim()))
            {
                string imageUrl = "Resources/error.png";
                string message = $"<img src='{imageUrl}' alt='Danger Image' style='width:20px;height:20px;' /> Date of Birth is required.";
                Session["AlertMessage"] = message;
                Session["AlertType"] = "alert-danger";
                return;
            }
            string userStatus = "Active";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    SqlCommand cmdCheck = conn.CreateCommand();
                    cmdCheck.CommandType = System.Data.CommandType.Text;
                    cmdCheck.CommandText = "SELECT COUNT(*) FROM User_tbl WHERE Email_Address = @EmailAddress ";
                    cmdCheck.Parameters.AddWithValue("@EmailAddress", txtEmail.Text.Trim());

                    int count = (int)cmdCheck.ExecuteScalar();
                    conn.Close();

                    if (count > 0)
                    {
                        string imageUrl = "Resources/error.png";
                        string message = $"<img src='{imageUrl}' alt='Error Image' style='width:20px;height:20px;' /> This User Email Address is already Joined System!";
                        Session["AlertMessage"] = message;
                        Session["AlertType"] = "alert-danger";
                        return;
                    }

                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Response.Write($"<script>alert('Error: {ex.Message}');</script>");
                }

                try
                {

                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Error: {ex.Message}');</script>");
                }
            }
        }





    }
}