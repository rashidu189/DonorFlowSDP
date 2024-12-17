using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DonorFlow
{
    public partial class ManageUsers : System.Web.UI.Page
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
                FillGridView();

            }
        }
        protected void RefreshBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);

        }
        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            string userName = TextBox3.Text.ToString().Trim();
            string userRole = DropDownList2.SelectedItem.Value.ToString().Trim();
            string userId = TextBox1.Text.ToString().Trim();
            string status = DropDownList1.SelectedItem.Value.ToString().Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT [Full_Name] AS [User Name],[Role] AS [User Role],[User_ID] AS [User ID],[Email_Address] AS [User Email],[Status] AS [Status] FROM User_tbl
                                  WHERE [Full_Name] LIKE @UserName AND [Role] LIKE @UserRole AND [User_ID] LIKE @UserID AND [Status] LIKE @Status";

                // Open the connection
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                // Use SqlCommand to execute the query with parameters
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@UserName", "%" + userName + "%");
                    cmd.Parameters.AddWithValue("@UserRole", "%" + userRole + "%");
                    cmd.Parameters.AddWithValue("@UserID", "%" + userId + "%");
                    cmd.Parameters.AddWithValue("@Status", "%" + status + "%");

                    // Execute the query and fill the data table
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Close the connection
                    conn.Close();

                    // Bind the results to the GridView
                    UserManagement.DataSource = dt;
                    UserManagement.DataBind();
                }
            }

        }

        public void FillGridView()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                // Use a SQL query to select data from the table
                string query = "SELECT [Full_Name] AS [User Name],[Role] AS [User Role],[User_ID] AS [User ID],[Email_Address] AS [User Email],[Status] AS [Status] FROM User_tbl";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                // No need to set the CommandType when using a SQL query
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                conn.Close();

                UserManagement.DataSource = dt;
                UserManagement.DataBind();
            }


        }
        public void UserManagement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = UserManagement.Rows[rowIndex];

                string userName = row.Cells[0].Text;
                string userRole = row.Cells[1].Text;
                string userId = row.Cells[2].Text;

                Response.Redirect("ManageUserProfile.aspx?userName=" + userName + "&userRole=" + userRole + "&userId=" + userId);


            }
        }
    }
}