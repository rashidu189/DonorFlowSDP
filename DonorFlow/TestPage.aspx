<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="DonorFlow.TestPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<asp:Literal ID="litCampaigns" runat="server"></asp:Literal>



<style>
    .progress-container {
        width: 100%;
        background-color: #f3f3f3;
        border-radius: 5px;
        margin-bottom: 10px;
    }

    .progress-bar {
        height: 20px;
        background-color: #4caf50;
        border-radius: 5px;
        text-align: center;
        color: white;
        line-height: 20px;
    }

    .campaign-container {
        margin-bottom: 20px;
        padding: 10px;
        border: 1px solid #ddd;
        border-radius: 5px;
    }

    .campaign-header {
        font-weight: bold;
    }
</style>


    <script>
        function updateProgressBar(campaignId, goal, amount) {
            var percentage = (amount / goal) * 100;
            document.getElementById('progress-bar-' + campaignId).style.width = percentage + '%';
        }
    </script>



</asp:Content>
