<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="DonorFlow.TestPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


                <div class="mt-4 text-center">
                    <!-- Change Password Button -->
                    <button class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#forgotPasswordModal">Change Password</button>
                    <!-- Delete Account Button -->
                    <button class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#deleteAccountModal">Delete Account</button>
                    <asp:Button CssClass="btn btn-warning" ID="Button1" runat="server" Text="Reset Password" OnClientClick="showForgotPasswordModal(); return false;"/>
                </div>

<!-- Change Password Modal -->
                    <div class="modal fade" id="forgotPasswordModal" tabindex="-1" aria-labelledby="forgotPasswordLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="forgotPasswordLabel">Forgot Password</h5>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="mb-3">
                                            <label for="email" class="form-label">Email</label>
                                           <!-- <input type="email" class="form-control" id="email" name="email">-->
                                            <asp:TextBox ID="txtREmail" runat="server" CssClass="form-control" placeholder="Enter Your Email" />
                                        </div>
                                    </div>
                                <div class="modal-footer">
                                    <!--<button type="button" class="btn btn-primary" id="resetPasswordButton">Reset Password</button>-->
                                    <asp:Button CssClass="btn btn-red resetbtn" ID="ResetPasswordNew" runat="server" Text="Reset Password" />
                                </div>
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
                <button type="button" class="btn btn-danger">Delete Account</button>
            </div>
        </div>
    </div>
</div>

<script>
    //Show Modal Script
    function showForgotPasswordModal() {
        const modal = new bootstrap.Modal(document.getElementById('forgotPasswordModal'));
        modal.show();
    }

</script>

</asp:Content>
