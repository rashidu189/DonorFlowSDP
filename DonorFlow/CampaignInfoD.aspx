<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CampaignInfoD.aspx.cs" Inherits="DonorFlow.CampaignInfoD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

            <!-- Campaign Creation Section -->
<div class="campaign-container">
    <div class="campaigncreate">
        <div class="container">
            <h2>View Campaign Details</h2>
            <p class="subtitle">Selected Donation Campaigns.</p>

            <div class="campaign-details-row" style="align-items: center; margin-top: 20px;">
                <!-- Campaign Title -->
                <div class="campaign-item" style="margin-top: 20px; margin-bottom: 20px;">
                    <label style="font-weight: bold;">Campaign Title:</label>
                    <asp:Label ID="txtTitle1" runat="server" CssClass="custom-textbox" Text="Enter campaign title" Required="true"></asp:Label>
                </div>

                <!-- Campaign Image -->
                <div class="campaign-item">
                    <p>
                        <asp:Image ID="imgDisplay" runat="server" Height="200px" Width="500px" CssClass="campaign-image img-fluid rounded" />
                    </p>
                </div>

                <!-- Campaign Description -->
                <div class="campaign-item" style="margin-top: 20px;">
                    <label style="font-weight: bold;">Description:</label>
                    <asp:Label ID="txtDescription1" runat="server" Text="Enter campaign description" Readonly="true"></asp:Label>
                </div>

                <!-- Donation Goal -->
                <div class="campaign-item" style="margin-top: 20px;">
                    <label style="font-weight: bold;">Donation Goal: </label>
                    <asp:Label ID="txtGoal1" runat="server" Text="Enter campaign Goal" Readonly="true"></asp:Label>
                </div>
                <div class="campaign-item" style="margin-top: 20px;">
                    <asp:Literal ID="litCampaigns" runat="server"></asp:Literal>
                </div>
                <!-- Campaign Start Date -->
                <div class="campaign-item" style="margin-top: 20px;">
                    <label style="font-weight: bold;">Campaign Start Date:</label>
                    <asp:Label ID="txtStartDate1" runat="server" Text="Enter campaign Start Date" Readonly="true"></asp:Label>
                </div>

                <!-- Campaign End Date -->
                <div class="campaign-item" style="margin-top: 20px;">
                    <label style="font-weight: bold;">Campaign End Date:</label>
                    <asp:Label ID="txtEndDate1" runat="server" Text="Enter campaign End Date" Readonly="true"></asp:Label>
                </div>
            </div>

            <div class="campaign-item" style="margin-top: 20px;">
                <label style="font-weight: bold; margin-bottom: 10px;">Donate Amount</label>
                <div class="input-group">
                    <span class="input-group-text">$</span>
                    <asp:TextBox ID="DAmount" runat="server" CssClass="form-control" TextMode="Number" Required="true" Placeholder="Enter Donate Amount"></asp:TextBox>
                </div>
            </div>


            <asp:Button ID="btnDonateNow" runat="server" Text="Donate Now" CssClass="btn btn-primary" style="margin-top: 20px;" OnClick="btnDonateNow_Click" />
            <asp:Button ID="TestPay" runat="server" Text="Test Pay" CssClass="btn btn-primary" style="margin-top: 20px;" OnClick="TestPay_Click" />
        </div>
    </div>
</div>


<style>

    .progress-container {
        width: 100%;
        background-color: #f3f3f3;
        border-radius: 20px 0 0 20px;
        margin-bottom: 10px;
        overflow: hidden;
        width: 300px; 

    }

    .progress-bar {
        height: 20px;
        background-color: #4caf50;        
        text-align: center;
        color: white;
        line-height: 20px;
        border-radius: 20px 0 0 20px;
    }

</style>


    <script>
        function updateProgressBar(campaignId, goal, amount) {
            var percentage = (amount / goal) * 100;
            document.getElementById('progress-bar-' + campaignId).style.width = percentage + '%';
        }
    </script>

</asp:Content>
