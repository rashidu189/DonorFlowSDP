using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DonorFlow
{
    public partial class LoginPage : System.Web.UI.Page
    {
        private static string hashedPassword;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DonorFlowConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd2 = conn.CreateCommand();
                    cmd2.CommandType = System.Data.CommandType.Text;
                    cmd2.CommandText = "SELECT * FROM User_tbl WHERE Email_Address = '" + txtEmail.Text.ToString() + "' and Password = '" + txtPassword.Text.ToString() + "'";
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.Read())
                    {
                        Session["User_ID"] = dr2.GetValue(1).ToString().Trim();
                        Session["Full_Name"] = dr2.GetValue(2).ToString().Trim();
                        Session["Email_Address"] = dr2.GetValue(3).ToString().Trim();
                        Session["Phone_Number"] = dr2.GetValue(4).ToString().Trim();
                        Session["Date_Of_Birth"] = dr2.GetValue(5).ToString().Trim();
                        Session["Address"] = dr2.GetValue(6).ToString().Trim();
                        Session["Status"] = dr2.GetValue(7).ToString().Trim();
                        Session["Registered_Date"] = dr2.GetValue(8).ToString().Trim();
                        Session["Password"] = dr2.GetValue(9).ToString().Trim();
                        Session["Role"] = dr2.GetValue(10).ToString().Trim();
                        Session["role"] = "DonorFlow_User";

                        conn.Close();

                        string DuserStatus = "Active";
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "SELECT COUNT(*) FROM User_tbl WHERE Email_Address = '" + txtEmail.Text.ToString() + "' and Password = '" + txtPassword.Text.ToString() + "' and Status = '" + DuserStatus + "' ";
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        string memId = txtEmail.Text.ToString();
                        string mempwd = txtPassword.Text.ToString();
                        cmd.ExecuteNonQuery();

                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            try
                            {

                                using (SqlCommand cmd1 = new SqlCommand("SELECT Role,Full_Name FROM User_tbl WHERE Email_Address = @Email AND Password = @Password", conn))
                                {

                                    cmd1.CommandType = System.Data.CommandType.Text;
                                    cmd1.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
                                    cmd1.Parameters.AddWithValue("@Password", txtPassword.Text.ToString());

                                    using (SqlDataReader dr = cmd1.ExecuteReader())
                                    {
                                        if (dr.Read())
                                        {
                                            string F_Name = dr["Full_Name"].ToString();
                                            string U_Type = dr["Role"].ToString().Trim();
                                            if (U_Type == "Donor")
                                            {
                                                string imageUrl = "Resources/success.png";
                                                string message = $"<img src='{imageUrl}' alt='Success Image' style='width:20px;height:20px;' /> Hi " + F_Name + ", you've successfully logged in. Welcome to the DonorFlow";

                                                Session["AlertMessage"] = message;
                                                Session["AlertType"] = "alert-success";
                                                Response.Redirect("DonorHomePage.aspx",false);
                                                

                                            }
                                            else if (U_Type == "Campaign Creator")
                                            {
                                                string imageUrl = "Resources/success.png";
                                                string message = $"<img src='{imageUrl}' alt='Success Image' style='width:20px;height:20px;' /> Hi " + F_Name + ", you've successfully logged in. Welcome to the DonorFlow";

                                                Session["AlertMessage"] = message;
                                                Session["AlertType"] = "alert-success";
                                                Response.Redirect("CampaignCreatorsHomePage.aspx", false);

                                            }
                                            else if (U_Type == "Administrator")
                                            {
                                                string imageUrl = "Resources/success.png";
                                                string message = $"<img src='{imageUrl}' alt='Success Image' style='width:20px;height:20px;' /> Hi " + F_Name + ", you've successfully logged in. Welcome to the DonorFlow";

                                                Session["AlertMessage"] = message;
                                                Session["AlertType"] = "alert-success";
                                                Response.Redirect("AdministratorHomePage.aspx", false);

                                            }
                                        }
                                        else
                                        {
                                            string imageUrl = "Resources/error.png";
                                            string message = $"<img src='{imageUrl}' alt='Danger Image' style='width:20px;height:20px;' /> Unknown User.";
                                            Session["AlertMessage"] = message;
                                            Session["AlertType"] = "alert-danger";
                                        }

                                    }
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

                        }
                    }
                    else
                    {
                        {
                            string imageUrl = "Resources/error.png"; // Update this to the actual path of your image
                            string message = $"<img src='{imageUrl}' alt='Success Image' style='width:20px;height:20px;' /> Login failed. Please check your email and password and try again.";

                            Session["AlertMessage"] = message;
                            Session["AlertType"] = "alert-danger"; // Adding the alert type
                                                                   //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please Try Again later');", true);
                        }
                        conn.Close();
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
            }
        }
        [WebMethod]
        public void ResetBtn_Click(object sender, EventArgs e)
        {
            string email = TextBox1.Text.Trim();
            const string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=DonorFlow_DB;Integrated Security=True;";

            if (string.IsNullOrWhiteSpace(email))
            {
               // return "Invalid input data.";
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Email_Address FROM User_tbl WHERE Email_Address = @EmailId AND Status = 'Active'";
                    using (SqlCommand cmd6 = new SqlCommand(query, conn))
                    {
                        cmd6.Parameters.AddWithValue("@EmailId", email);

                        var emailFromDb = cmd6.ExecuteScalar()?.ToString().Trim();
                        if (emailFromDb == null || emailFromDb.ToString() != email)
                        {
                            //return "Invalid Email.";
                        }
                        else
                        {
                            // Generate and hash new password
                            string newPassword = Guid.NewGuid().ToString().Substring(0, 8);  // Reduced the length of the password
                            using (SHA256 sha256Hash = SHA256.Create())
                            {
                                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
                                StringBuilder builder = new StringBuilder();
                                foreach (byte b in bytes)
                                {
                                    builder.Append(b.ToString("x2"));
                                }
                                hashedPassword = builder.ToString();
                                hashedPassword = hashedPassword.Substring(0, 8);

                                string updateQuery = "UPDATE User_tbl SET Password = @Password WHERE Email_Address = @EmailId";
                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@Password", hashedPassword);
                                    updateCmd.Parameters.AddWithValue("@EmailId", email);
                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                        }

                    }


                    // Send email
                    SendPasswordEmail(email, hashedPassword);
                    //return "Password reset successfully. Check your email for the new password.";
                }
            }
            catch 
            {
               // return "An error occurred: " + ex.Message;
            }
        }

        private static void SendPasswordEmail( string email, string hashedPassword)
        {
            string fullNameFromDb = string.Empty;
            const string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=DonorFlow_DB;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Full_Name FROM User_tbl WHERE Email_Address = @EmailId AND Status LIKE '%Active%'";
                using (SqlCommand cmd8 = new SqlCommand(query, conn))
                {
                    cmd8.Parameters.AddWithValue("@EmailId", email);
                    fullNameFromDb = cmd8.ExecuteScalar()?.ToString().Trim();
                    if (fullNameFromDb == null)
                    {

                    }
                }
            }


            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("rashidumilan100@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Password Reset";
                mail.Body = $@"<html>
                                <head>
                                    <style>
                                        body {{{{ font-family: Arial, sans-serif; }}}}
                                        .container {{{{ margin: 20px; padding: 10px; }}}}
                                        .content {{{{ font-size: 16px; }}}}
                                        .footer {{{{ margin-top: 20px; font-size: 12px; color: gray; }}}}
                                    </style>
                                </head>
                                <body>
                                    <div class='container'>
                                        <div class='content'>
                                            <p>Dear {fullNameFromDb},</p>
                                            <p>Your new password is: <strong>{hashedPassword}</strong></p>
                                            <p>If you did not request this change, please contact support immediately.</p>
                                        </div>
                                        <div class='footer'>
                                            <p>Best regards,</p>
                                            <br>DonorFlow Support Team.....
            
                                        </div>
                                    </div>
                                </body>
                                <br><hr style=""font-family: arial, sans-serif; font-size: 10pt;color:#e74c3c;""><table cellpadding=""10px"">
                                                        <tr><td colspan=""3"" style=""background-color:white;color:#e74c3c text-align:center;""><b>© Note: This is an auto generated email. Please don't reply to this email.</b></td>
                                                        </tr><tr><td style=""background-color:#e74c3c;color:white;text-align:center;"">©</td>
                                                        <td style=""background-color:#e74c3c;color:white;text-align:center;"">DonorFlow</td>
                                                        <td style=""background-color:#e74c3c;color:white;text-align:center;"">DonorFlow SYSTEM</table>

                                </div>  
                                </html>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("donorflow0@gmail.com", "ygfr pkan dcqr indn");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}