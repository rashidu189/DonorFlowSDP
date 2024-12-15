<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CampaignCreatePage.aspx.cs" Inherits="DonorFlow.CampaignCreatePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<!-- Campaign Creation Section -->
<div class="campaign-container">
    <div class="campaigncreate">
        <div class="container">
            <h2>Create a Campaign</h2>
            <p class="subtitle">Your Donation Campaigns.</p>

            <asp:Label ID="lblTitle" runat="server" Text="Campaign Title"></asp:Label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Placeholder="Enter campaign title" Required="true"></asp:TextBox>

            <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" Placeholder="Enter campaign description" Required="true"></asp:TextBox>

            <asp:Label ID="lblGoal" runat="server" Text="Donation Goal ($)"></asp:Label>
            <asp:TextBox ID="txtGoal" runat="server" CssClass="form-control" Placeholder="Enter donation goal" TextMode="Number" Required="true"></asp:TextBox>

            <asp:Label ID="lblStartDate" runat="server" Text="Start Date"></asp:Label>
            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TextMode="Date" Required="true"></asp:TextBox>

            <asp:Label ID="lblEndDate" runat="server" Text="End Date"></asp:Label>
            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TextMode="Date" Required="true"></asp:TextBox>

            <asp:Label ID="lblImage" runat="server" Text="Campaign Image" Style="margin-top:20px;"></asp:Label>
            <!--<asp:FileUpload ID="fileImage" runat="server" CssClass="form-control" Required="true" />-->
            <asp:FileUpload ID="fileUpload" runat="server" Style="margin-top:20px;" />

            <div class="buttons">
                <asp:Button ID="btnSubmit" runat="server" Text="Create Campaign" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnView" runat="server" Text="View campaign" CssClass="btn btn-secondary" OnClick="btnView_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="btnClear_Click" />
            </div>
        </div>
    </div>

    <!-- Display the Created Campaign -->
<div class="latest-campaign">
<div class="container mt-5">
    <div class="card shadow">
        <div class="card-header text-center">
            <h2>Your Created Campaign</h2>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-12">
                    <p><strong>Title: </strong>
                        <asp:Label ID="lblCampaignTitle" runat="server" CssClass="campaign-title"></asp:Label>
                    </p>
               </div>
                <!-- Campaign Image -->
                <div class="col-md-12 text-center">
                    <p><asp:Image ID="imgDisplay" runat="server" Height="200px" Width="500px" CssClass="campaign-image img-fluid rounded" /></p>
                </div>
                <!-- Campaign Details -->
                <div class="col-md-12">
                    <p><strong>Description: </strong>
                        <asp:Label ID="lblDescriptionC" runat="server" CssClass="campaign-description"></asp:Label>
                    </p>
                    <p><strong>Donation Goal: </strong>
                        <asp:Label ID="lblDonationGoal" runat="server" CssClass="campaign-goal"></asp:Label>
                    </p>
                    <p><strong>Duration :</strong>
                        <asp:Label ID="lblDates" runat="server" CssClass="campaign-dates"></asp:Label>
                    </p>
                </div>
            </div>
        </div>
    </div>
</div>
</div>


</div>




  <script>
    const campaignForm = document.getElementById('campaignForm');
    const clearButton = document.getElementById('clearButton');

    // Show success message and alert on form submission
    campaignForm.addEventListener('submit', function (e) {
      alert('Campaign created successfully!');
    });

    // Clear button functionality to reset form fields
    clearButton.addEventListener('click', function () {
      campaignForm.reset();
    });
  </script>

<style>

    .campaigncreate{
        margin-left:50px;
    }

    h2 {
      color: #77266a;
      font-size: 26px;
      margin-bottom: 10px;
    }
    .subtitle {
      color: #555;
      font-size: 14px;
      margin-bottom: 15px;
    }
    label {
      font-weight: bold;
      display: block;
      margin-top: 15px;
      text-align: left;
    }
    input[type="text"],
    input[type="number"],
    input[type="date"],
    textarea {
      width: 100%;
      padding: 10px;
      margin-top: 5px;
      border: 1px solid #ccc;
      border-radius: 4px;
      font-size: 14px;
    }
    textarea {
      resize: vertical;
    }
    input[type="file"] {
      margin-top: 5px;
    }
    .buttons {
      display: flex;
      gap: 15px;
      margin-top: 20px;
      justify-content: center;
    }
    button {
      background-color: #115663;
      color: white;
      border: none;
      border-radius: 4px;
      padding: 10px 15px;
      font-size: 14px;
      cursor: pointer;
      flex: 1;
      max-width: 150px;
    }
    button:hover {
      background-color: #0e4752;
    }

    /*View Campaign*/

    .campaign-container {
        display: flex;
        justify-content: space-between;
        align-items: flex-start;
        gap: 20px;
        margin: 20px;
    }

    .campaigncreate, .campaignview {
        flex: 1;
    }

    .campaigncreate {
        background-color: #f9f9f9;
        padding: 20px;
        border: 1px solid #ddd;
        border-radius: 8px;
    }

    .campaignview {
        background-color: #ffffff;
        padding: 20px;
        border: 1px solid #ddd;
        border-radius: 8px;
    }

    .campaign-card {
        border: 1px solid #ccc;
        border-radius: 8px;
        padding: 10px;
        margin-bottom: 10px;
        text-align: center;
        background-color: #f5f5f5;
    }

    .campaign-card img {
        max-width: 100%;
        height: auto;
        border-radius: 8px;
        margin-bottom: 10px;
    }

    .latest-campaign .container {
        width: 500px; /* Reduce the width */
        height: auto; /* Adjust height dynamically */
        padding: 20px; /* Adjust padding */
    }



</style>

</asp:Content>
