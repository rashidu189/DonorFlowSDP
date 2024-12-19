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
    public partial class DonationHistoryD : System.Web.UI.Page
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
                        UserId = Session["User_Id"].ToString();
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
                BindTransactionData();

            }
        }
        private void BindTransactionData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                // Use a SQL query to select data from the table
                string query = @"SELECT TH.[Transaction_ID] AS [TransactionID]
                                  ,CAM.[Campaign_ID] AS [Campaign_ID]
	                              ,CAM.[Campaign_Title] AS [Campaign_Tittle]
                                  ,TH.[Transfer_Amount] AS [PaidAmount]
                                  ,TH.[TransferedDate] AS [Transfered_Date]
                                  ,TH.[TransferedUser] AS [Transfered_User]
                              FROM TransactionHistory TH
                              LEFT JOIN Campaigns CAM ON CAM.[Campaign_ID] = TH.[Campaign_ID]
                              WHERE TH.[TransferedUser] = @userId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@userId", UserId);

                    // Execute the query and fill the data table
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        gvTransactions.DataSource = dt;
                        gvTransactions.DataBind();
                    }
                    else
                    {
                        // If no rows, display a message
                        gvTransactions.DataSource = null;
                        gvTransactions.DataBind();
                        lblNoTransactionMessage.Text = "No transactions found.";
                        lblNoTransactionMessage.Visible = true;
                    }
                    // Close the connection
                    conn.Close();

                }
            }

        }
    }
}