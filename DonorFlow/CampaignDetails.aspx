<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CampaignDetails.aspx.cs" Inherits="DonorFlow.CampaignDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Campaign Creation Section -->
    <div class="campaign-container">
        <div class="campaigncreate">
            <div class="container">
                <h2>Edit Campaign Details</h2>
                <p class="subtitle">Selected Donation Campaigns.</p>

                <!-- Campaign Image -->
                <div class="col-md-12">
                    <p>
                        <asp:Image ID="imgDisplay" runat="server" Height="200px" Width="500px" CssClass="campaign-image img-fluid rounded" />
                    </p>
                </div>
                     <div class="col-md-3">
                                <asp:Label ID="lblImage" runat="server" Text="Change Campaign Image" Style="margin-top: 20px;"></asp:Label>
                <!--<asp:FileUpload ID="fileImage" runat="server" CssClass="form-control" Required="true" />-->
                <asp:FileUpload ID="fileUpload" runat="server" Style="margin-top: 20px;" />
                         </div>
                <div class="row align-items-center" style="margin-top:20px;">
                    <div class="col-md-3">
                        <asp:Label ID="labelId" runat="server" Text="Campaign ID"></asp:Label>
                        <asp:TextBox ID="txtId" runat="server" CssClass="form-control" Placeholder="Campaign Id" Readonly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblTitle" runat="server" Text="Campaign Title"></asp:Label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control custom-textbox" Placeholder="Enter campaign title" Required="true"></asp:TextBox>
                    </div>

                </div>
                <div class="row align-items-center" style="margin-top:20px;">
                    <div class="col-md-3">
                        <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" Placeholder="Enter campaign description" Required="true"></asp:TextBox>
                    </div>

                    <div class="col-md-3">
                        <asp:Label ID="lblGoal" runat="server" Text="Donation Goal ($)"></asp:Label>
                        <asp:TextBox ID="txtGoal" runat="server" CssClass="form-control" Placeholder="Enter donation goal" TextMode="Number" Required="true"></asp:TextBox>
                    </div>
                </div>
                <div class="row align-items-center" style="margin-top:20px;">
                    <div class="col-md-3">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date"></asp:Label>
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TextMode="Date" Required="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date"></asp:Label>
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TextMode="Date" Required="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblcreatedDate" runat="server" Text="Created Date"></asp:Label>
                        <asp:TextBox ID="txtCreatedDate" runat="server" CssClass="form-control" Readonly="true"></asp:TextBox>
                    </div>
                </div>
                                <div class="row align-items-center" style="margin-top:20px;">
                                        <div class="col-md-3">
                            <label class="labels">Campaign Status</label>
                            <asp:DropDownList ID="DStatus" CssClass="form-control" runat="server" Style="width: 250px;">
                                <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblIsApproved" runat="server" Text="Is Approved"></asp:Label>
                        <asp:TextBox ID="txtIsApproved" runat="server" CssClass="form-control" Readonly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblCreatedUser" runat="server" Text="Created User"></asp:Label>
                        <asp:TextBox ID="txtCreatedUser" runat="server" CssClass="form-control" Readonly="true"></asp:TextBox>
                    </div>
                                    </div>
                <div class="row align-items-center" style="margin-top:20px;">

                     <div class="col-md-1">
                <asp:Button CssClass="btn btn-primary update-button" ID="UpdateBtn" runat="server" Text="Update" OnClick="UpdateBtn_Click" />
                         </div>
                                         <div class="col-md-1">
                <asp:Button CssClass="btn btn-success approve-button" ID="ApproveBtn" runat="server" Text="Approve" OnClick="ApproveBtn_Click" />
                         </div>
                                         <div class="col-md-1">
                <asp:Button CssClass="btn btn-danger reject-button" ID="RejectBtn" runat="server" Text="Reject" OnClick="RejectBtn_Click" />
                         </div>
                                         <div class="col-md-1">
                <asp:Button CssClass="btn btn-danger delete-button" ID="DeleteBtnN" runat="server" Text="Delete" OnClientClick="showdeleteCampaignModal(); return false;"/>
                         </div>
                    </div>
            </div>
        </div>
    </div>

<!-- Delete Campaign Modal -->
<div class="modal fade" id="deleteCampaignModal" tabindex="-1" aria-labelledby="deleteAccountModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="deleteAccountModalLabel">Delete Campaign</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <p>Are you sure you want to delete this campaign? This action cannot be undone.</p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                <asp:Button CssClass="btn btn-danger" ID="DeleteBtn" runat="server" Text="Delete Campaign" OnClick="DeleteBtn_Click"/>
            </div>
        </div>
    </div>
</div>

<style>

        /* Modal Header - Custom CSS to override Bootstrap styles */
        .modal-header .btn-close {
            margin: auto;
            margin-top: initial;
            margin-right: initial;
            margin-bottom: initial;
            margin-left: auto;
            background-color:red;
        }

    .update-button{
        background-color:blue;
    }
    .approve-button{
        background-color:forestgreen;
    }
    .reject-button{
        background-color:red;
    }
    .delete-button{
        background-color:red;
    }

    .campaign-card img {
        max-width: 100%;
        height: auto;
        border-radius: 8px;
        margin-bottom: 10px;
    }
    .campaigncreate{
        margin-left:100px;
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
    .label {
      font-weight: bold;
      display: block;
      margin-top: 15px;
      text-align: left;
    }



</style>

<script>

    //Show Modal Script
    function showdeleteCampaignModal() {
        const modal = new bootstrap.Modal(document.getElementById('deleteCampaignModal'));
        modal.show();
    }

</script>

</asp:Content>
