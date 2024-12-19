<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentPage.aspx.cs" Inherits="DonorFlow.PaymentPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<script src="https://www.paypal.com/sdk/js?client-id=AdT9FPLsWFhwf37-BR3Ek1N99JNEHhLBY3IqsRrba_YQHh011FkBGyC4I2NBHyKWAdEpCluorr80CMyq"></script>

<div id="paypal-button-container"></div>

<script type="text/javascript">
    var donateAmount = "<%= Request.QueryString["DonateAmount"] %>";

    paypal.Buttons({
        createOrder: function (data, actions) {
            return actions.order.create({
                purchase_units: [{
                    amount: {
                        value: donateAmount
                    }
                }]
            });
        },
        onApprove: function (data, actions) {
            return actions.order.capture().then(function (details) {
                alert('Transaction completed by ' + details.payer.name.given_name);
            });
        }
    }).render('#paypal-button-container');
</script>


<style>


</style>

        <div>
        </div>
</body>
</html>


