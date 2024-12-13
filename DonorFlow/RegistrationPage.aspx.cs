using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;

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
            string role = "Donor";
            DateTime regDate = DateTime.Now;

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
                    // Handle any errors that may have occurred
                    string imageUrl = "Resources/error.png";
                    string message = $"<img src='{imageUrl}' alt='Error Image' style='width:20px;height:20px;' /> An error occurred: {ex.Message}";
                    Session["AlertMessage"] = message;
                    Session["AlertType"] = "alert-danger";
                    conn.Close();
                }

                try
                {
                    conn.Open();
                    SqlCommand cmdInsert = conn.CreateCommand();
                    cmdInsert.CommandType = System.Data.CommandType.Text;
                    cmdInsert.CommandText = @"
                                    INSERT INTO User_tbl (
                                    Full_Name , Email_Address , Phone_Number , Date_Of_Birth , Address , Status , Registered_Date , Password , Role
                                    ) VALUES (
                                        @FullName, @Email, @PhoneNo, @DOB, @Address, @Status,
                                        @RegisteredDate, @Password, @Role
                                    )";

                    // Add parameters
                    cmdInsert.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                    cmdInsert.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmdInsert.Parameters.AddWithValue("@PhoneNo", txtPhoneNumber.Text.Trim());
                    cmdInsert.Parameters.AddWithValue("@DOB", txtDateOfBirth.Text.Trim());
                    cmdInsert.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmdInsert.Parameters.AddWithValue("@Status", userStatus);
                    cmdInsert.Parameters.AddWithValue("@RegisteredDate", regDate);
                    cmdInsert.Parameters.AddWithValue("@Password", ctxtpwd.Text.Trim());
                    cmdInsert.Parameters.AddWithValue("@Role", role);


                    // Execute the insert command
                    cmdInsert.ExecuteNonQuery();
                    conn.Close();

                    string imageUrl = "Resources/success.png";
                    string message = $"<img src='{imageUrl}' alt='Success Image' style='width:20px;height:20px;' /> Registration is Succesfully Completed.";

                    Session["AlertMessage"] = message;
                    Session["AlertType"] = "alert-success";
                }
                catch (Exception ex)
                {
                    // Handle any errors that may have occurred
                    string imageUrl = "Resources/error.png";
                    string message = $"<img src='{imageUrl}' alt='Error Image' style='width:20px;height:20px;' /> An error occurred: {ex.Message}";
                    Session["AlertMessage"] = message;
                    Session["AlertType"] = "alert-danger";
                    conn.Close();
                }
            }
        }





    }
}