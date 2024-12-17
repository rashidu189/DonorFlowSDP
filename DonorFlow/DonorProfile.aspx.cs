using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DonorFlow
{
    public partial class DonorProfile : System.Web.UI.Page
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
                        siteMaster.LinkButton15Property.Text = Session["Full_Name"].ToString();
                        LabelUserID.Text = Session["User_ID"].ToString();
                        LabelEmail.Text = Session["Email_Address"].ToString();
                        UserId = Session["User_ID"].ToString().Trim();
                    }
                }
                if (!IsPostBack)
                {
                    DonorDetails();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions properly
                Response.Write(ex.Message);
            }
        }
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.CommandText = @"UPDATE User_tbl SET Full_Name = @FullName, Phone_Number = @PhoneNo, Address = @Address, Email_Address = @Email, Date_Of_Birth = @DOB WHERE User_ID = @UserId";

                        cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@PhoneNo", txtMobileNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmailId.Text.Trim());
                        cmd.Parameters.AddWithValue("@DOB", DateTime.Parse(txtDob.Text.Trim()).ToString("yyyy-MM-dd"));

                        // Use the dynamic UserId from Session instead of hardcoding
                        cmd.Parameters.AddWithValue("@UserId", UserId);

                        cmd.ExecuteNonQuery();

                        string imageUrl = "Resources/success.png";
                        string message = $"<img src='{imageUrl}' alt='Success' style='width:20px;height:20px;' /> Your information has been successfully updated.";
                        Session["AlertMessage"] = message;
                        Session["AlertType"] = "alert-success";

                        Response.Redirect(Request.RawUrl);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtconfirmPassword.Text)|| string.IsNullOrEmpty(txtnewPassword.Text))
            {
                string imageUrl = "Resources/error.png";
                string message = $"<img src='{imageUrl}' alt='Danger' style='width:20px;height:20px;' /> Please Enter both Fields.";
                Session["AlertMessage"] = message;
                Session["AlertType"] = "alert-danger";
            }
            else if(txtconfirmPassword.Text.ToString().Trim()== txtnewPassword.Text.ToString().Trim())
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.CommandText = @"UPDATE User_tbl SET Password = @CPwd WHERE User_ID = @UserID";

                        cmd.Parameters.AddWithValue("@CPwd", txtconfirmPassword.Text.Trim());
                        cmd.Parameters.AddWithValue("@NewPwd", txtnewPassword.Text.Trim());

                        // Use the dynamic UserId from Session instead of hardcoding
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.ExecuteNonQuery();

                        string imageUrl = "Resources/success.png";
                        string message = $"<img src='{imageUrl}' alt='Success' style='width:20px;height:20px;' /> Your Password has been successfully Changed.";
                        Session["AlertMessage"] = message;
                        Session["AlertType"] = "alert-success";

                        Response.Redirect(Request.RawUrl);
                    }
                    conn.Close();
                }
            }
            else
            {
                string imageUrl = "Resources/error.png";
                string message = $"<img src='{imageUrl}' alt='Danger' style='width:20px;height:20px;' /> Confirm Password and New Password are not equal .";
                Session["AlertMessage"] = message;
                Session["AlertType"] = "alert-danger";
            }


        }
        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"DELETE FROM User_tbl WHERE User_ID = @UserId";
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.ExecuteNonQuery();

                    string imageUrl = "Resources/error.png";
                    string message = $"<img src='{imageUrl}' alt='Danger' style='width:20px;height:20px;' /> Your Account is Already Deleted.";
                    Session["AlertMessage"] = message;
                    Session["AlertType"] = "alert-danger";

                    Response.Redirect("LoginPage.aspx");
                }
                conn.Close();
            }
        }
        public void DonorDetails()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = @"SELECT [Full_Name],[Email_Address],[Phone_Number],[Date_Of_Birth],[Address],[Status],[Password] FROM User_tbl WHERE User_ID = @UserId";

                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        txtFullName.Text = dr.GetValue(0).ToString().Trim();
                        txtEmailId.Text = dr.GetValue(1).ToString().Trim();
                        txtMobileNo.Text = dr.GetValue(2).ToString().Trim();
                        DateTime dobValue = Convert.ToDateTime(dr.GetValue(3).ToString().Trim());
                        txtDob.Text = dobValue.ToString("yyyy-MM-dd").Trim();
                        txtAddress.Text = dr.GetValue(4).ToString().Trim();
                        txtStatus.Text = dr.GetValue(5).ToString().Trim();
                        txtcurrentPassword.Text = dr.GetValue(6).ToString().Trim();
                    }

                }
                conn.Close();
            }
        }
    }
}