<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewCampaign.aspx.cs" Inherits="DonorFlow.DonationPayment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


<div id="campaignsContainer" runat="server">
    <!-- The campaigns will be dynamically added here -->
</div>

<script type="text/javascript">
    // AJAX to fetch campaigns and load into the page
    function loadCampaigns() {
        $.ajax({
            type: "POST",
            url: "ViewCampaign.aspx/GetCampaigns",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                const campaignsContainer = document.getElementById("campaignsContainer");
                campaignsContainer.innerHTML = ""; // Clear previous content

                response.d.forEach(campaign => {
                    campaignsContainer.innerHTML +=
                        <div class="campaign-card">
                            <h3>${campaign.Campaign_Title}</h3>
                            <img src="${campaign.Image_Path}" alt="${campaign.Campaign_Title}" />
                            <p>${campaign.Description}</p>
                            <p>${campaign.Donation_Goal}</p>
                            <div class="progress-container">
                                <div id="progress-bar" class="progress-bar">0%</div>
                            </div>
                            <p>${campaign.formattedEndDate}</p>
                            <a href="CampaignInfo.aspx?CampaignId=${encodeURIComponent(campaign.Campaign_ID)}" class="btn btn-primary">View Details</a>
                        </div>;


                });
            },
            error: function (error) {
                console.error("Error fetching campaigns:", error);
            }
        });
    }

    // Call the function on page load
    window.onload = loadCampaigns;





</script>





<script>




</script>


    <style>

    .campaign-card {
        width: 300px; /* Fixed width for all cards */
        height: 400px; /* Fixed height for all cards */
        margin: 10px;
        padding: 15px;
        border: 1px solid #ccc;
        border-radius: 8px;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        display: inline-block;
        vertical-align: top;
        text-align: center;
        overflow: hidden; /* Prevent overflow of content */
        background-color: #fff; /* Optional: Set background color */
    }

    .campaign-card img {
        width: 100%; /* Ensure the image spans the full width of the card */
        height: 150px; /* Fixed height for all images */
        object-fit: cover; /* Crop the image to maintain its aspect ratio */
        border-radius: 5px;
        margin-bottom: 10px;
    }

    .campaign-card h3 {
        font-size: 18px; /* Adjust title size */
        margin: 10px 0;
        text-overflow: ellipsis; /* Truncate text if it's too long */
        overflow: hidden;
        white-space: nowrap;
    }

    .campaign-card p {
        font-size: 14px; /* Adjust description size */
        color: #555;
        overflow: hidden; /* Hide overflowing content */
        text-overflow: ellipsis; /* Add ellipsis for truncated text */
        display: -webkit-box;
        -webkit-line-clamp: 3; /* Limit description to 3 lines */
        -webkit-box-orient: vertical;
    }

    #campaignsContainer {
        display: flex;
        flex-wrap: wrap;
        justify-content: space-around; /* Distribute cards evenly */
    }
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
</style>

</asp:Content>
