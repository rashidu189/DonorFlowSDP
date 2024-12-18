using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DonorFlow
{
    public partial class ManageCampaigns : System.Web.UI.Page
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
            string campaignId = TextBox3.Text.ToString().Trim();
            string campaignTittle = TextBox2.Text.ToString().Trim();
            string startDate = TextBox1.Text.ToString().Trim();
            string enddate = TextBox4.Text.ToString().Trim();
            string status = DropDownList1.SelectedItem.Value.ToString().Trim();
            Console.WriteLine("Status selected: " + status);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT [Campaign_ID] AS [Campaign ID],[Campaign_Title] AS [Campaign Title],[StartDate] AS [Start Date],[EndDate] AS [End Date],[Status] AS [Status] FROM Campaigns 
                                 WHERE [Campaign_ID] LIKE @CampaignId AND [Campaign_Title] LIKE @CampaignTittle AND [StartDate] LIKE @StartDate AND [EndDate] LIKE @EndDate AND [Status] LIKE @Status";

                // Open the connection
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                // Use SqlCommand to execute the query with parameters
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@CampaignId", "%" + campaignId + "%");
                    cmd.Parameters.AddWithValue("@CampaignTittle", "%" + campaignTittle + "%");
                    cmd.Parameters.AddWithValue("@StartDate", "%" + startDate + "%");
                    cmd.Parameters.AddWithValue("@EndDate", "%" + enddate + "%");
                    cmd.Parameters.AddWithValue("@Status",  status );

                    // Execute the query and fill the data table
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Close the connection
                    conn.Close();

                    // Bind the results to the GridView
                    CampaignManagement.DataSource = dt;
                    CampaignManagement.DataBind();
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
                string query = "SELECT [Campaign_ID] AS [Campaign ID],[Campaign_Title] AS [Campaign Title],[StartDate] AS [Start Date],[EndDate] AS [End Date],[Status] AS [Status] FROM Campaigns";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                // No need to set the CommandType when using a SQL query
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                conn.Close();

                CampaignManagement.DataSource = dt;
                CampaignManagement.DataBind();
            }


        }
        public void CampaignManagement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = CampaignManagement.Rows[rowIndex];

                string campaignId = row.Cells[0].Text;
                string campaignTittle = row.Cells[1].Text;

                Response.Redirect("CampaignDetails.aspx?campaignId=" + campaignId + "&campaignTittle=" + campaignTittle);


            }
        }
    }
}