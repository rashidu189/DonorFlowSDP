<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUserProfile.aspx.cs" Inherits="DonorFlow.ManageUserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container rounded bg-white mt-5 mb-5">
        <div class="row">
            <div class="col-md-3 border-right">
                <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                    <img class="rounded-circle mt-5" style="width: 150px;"
                        src="https://st3.depositphotos.com/15648834/17930/v/600/depositphotos_179308454-stock-illustration-unknown-person-silhouette-glasses-profile.jpg">
                    <asp:Label ID="LabelUserRole" runat="server" Text="UserRole" Style="color: forestgreen; font-weight: bold; margin-bottom: 10px;"></asp:Label>
                    <asp:Label ID="LabelUserID" runat="server" Text="UserId" CssClass="font-weight-bold"></asp:Label>
                    <asp:Label ID="LabelEmail" runat="server" Text="Email" CssClass="text-black-50"></asp:Label>
                </div>
            </div>
            <div class="col-md-5 border-right">
                <div class="p-3 py-5">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h4 class="text-right">User Account Settings</h4>
                    </div>
                    <div class="row mt-2">
                        <div class="col-md-12">
                            <label class="labels">Name</label><asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Placeholder="Full Name"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-12">
                            <label class="labels">Mobile Number</label><asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" Placeholder="Mobile No"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <label class="labels">Address</label><asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Placeholder="Address"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <label class="labels">Email ID</label><asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" Placeholder="Email ID"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-6">
                            <label class="labels">Date of Birth</label><asp:TextBox ID="txtDob" runat="server" CssClass="form-control" Placeholder="DOB" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="labels">User Status</label>
                            <asp:DropDownList ID="DStatus" CssClass="txtbox" runat="server" Style="width: 250px;">
                                <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <label class="labels">User Role</label>
                            <asp:DropDownList ID="DUserRole" CssClass="txtbox" runat="server" Style="width: 250px;">
                                <asp:ListItem Text="Donor" Value="Donor"></asp:ListItem>
                                <asp:ListItem Text="Campaign Creator" Value="Campaign Creator"></asp:ListItem>
                                <asp:ListItem Text="Administrator" Value="Administrator"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="mt-5 text-center">
                        <asp:Button CssClass="btn btn-primary profile-button" ID="SaveBtn" runat="server" Text="Save Profile" OnClick="SaveBtn_Click" />
                    </div>
                    <div class="mt-4 text-center">
                        <!-- Change Password Button -->
                        <asp:Button CssClass="btn btn-warning" ID="Button1" runat="server" Text="Change Password" OnClientClick="showchangePasswordModal(); return false;" />
                        <!-- Delete Account Button -->
                        <asp:Button CssClass="btn btn-danger" ID="Button2" runat="server" Text="Delete Account" OnClientClick="showdeleteAccountModal(); return false;" />
                    </div>
                </div>

            </div>
        </div>
    </div>


<!-- Change Password Modal -->
<div class="modal fade" id="changePasswordModal" tabindex="-1" aria-labelledby="changePasswordModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="changePasswordModalLabel">Change Password</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">

                    <div class="mb-3">
                        <label for="currentPassword" class="form-label">Current Password</label>
                        <asp:TextBox ID="txtcurrentPassword" runat="server" CssClass="form-control" Placeholder="Enter current password" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="newPassword" class="form-label">New Password</label>
                        <asp:TextBox ID="txtnewPassword" runat="server" CssClass="form-control" Placeholder="Enter new password" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="confirmPassword" class="form-label">Confirm Password</label>
                        <asp:TextBox ID="txtconfirmPassword" runat="server" CssClass="form-control" Placeholder="Confirm new password" TextMode="Password"></asp:TextBox>
                    </div>

            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                <asp:Button CssClass="btn btn-primary" ID="UpdateBtn" runat="server" Text="Update Password" OnClick="UpdateBtn_Click"/>
            </div>
        </div>
    </div>
</div>

<!-- Delete Account Modal -->
<div class="modal fade" id="deleteAccountModal" tabindex="-1" aria-labelledby="deleteAccountModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="deleteAccountModalLabel">Delete Account</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <p>Are you sure you want to delete your account? This action cannot be undone.</p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                <asp:Button CssClass="btn btn-danger" ID="DeleteBtn" runat="server" Text="Delete Account" OnClick="DeleteBtn_Click"/>
            </div>
        </div>
    </div>
</div>

<script>

    //Show Modal Script
    function showchangePasswordModal() {
        const modal = new bootstrap.Modal(document.getElementById('changePasswordModal'));
        modal.show();
    }

    //Show Modal Script
    function showdeleteAccountModal() {
        const modal = new bootstrap.Modal(document.getElementById('deleteAccountModal'));
        modal.show();
    }

</script>



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

        body {
            background: white;
        }

        .form-control:focus {
            box-shadow: none;
            border-color: #BA68C8
        }

        .profile-button {
            background: rgb(99, 39, 120);
            box-shadow: none;
            border: none
        }

        .profile-button:hover {
            background: #682773
        }

        .profile-button:focus {
            background: #682773;
            box-shadow: none
        }

        .profile-button:active {
            background: #682773;
            box-shadow: none
        }

        .back:hover {
            color: #682773;
            cursor: pointer
        }

        .labels {
            font-size: 11px
        }

        .add-experience:hover {
            background: #BA68C8;
            color: #fff;
            cursor: pointer;
            border: solid 1px #BA68C8
        }
    </style>

</asp:Content>
