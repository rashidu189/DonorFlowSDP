using PayPal;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DonorFlow
{
    public partial class PaymentPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string campaignId = Request.QueryString["CampaignId"];
            string userId = Request.QueryString["UserId"];
            string donateAmount = Request.QueryString["DonateAmount"];

            if (!string.IsNullOrEmpty(campaignId) && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(donateAmount))
            {

                    // Replace with your actual APIContext initialization logic
                    var apiContext = new APIContext("EAOD13cIYEG350AObBRpMpdx6axNNx-9ix-39yZM4aaicdpE5-04Rm9qw_cMrHgzp8NuP1xFRqSL-EHC");

                    // Create a PaymentExecution object with the payer ID
                    var paymentExecution = new PaymentExecution
                    {
                        payer_id = userId
                    };

                    // Create a Payment object with the payment ID
                    var payment = new Payment
                    {
                        id = campaignId
                    };

                    try
                    {
                        var executedPayment = payment.Execute(apiContext, paymentExecution);

                        if (executedPayment.state.Equals("approved", StringComparison.OrdinalIgnoreCase))
                        {
                            Response.Write("<script>alert('Payment approved successfully!');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Payment not approved. Try again.');</script>");
                        }
                    }
                    catch
                    {

                    }



            }
            else
            {
                //Response.Write("<script>alert('Invalid query parameters.');</script>");
            }
        }

    }
}