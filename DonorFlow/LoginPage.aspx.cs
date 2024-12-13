using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DonorFlow
{
    public partial class LoginPage : System.Web.UI.Page
    {
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
                                                Response.Redirect("DonorHomePage.aspx", false);

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