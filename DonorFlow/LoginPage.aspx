<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="DonorFlow.LoginPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="max-width: 400px; margin: auto; padding: 20px; border: 2px solid #ccc; border-radius: 10px; margin-top:50px;">
        <div class="row">
            <div class="col">
                <center>
                    <img width="150" src="Images/MainLogo/MainLogo.png" alt="DonorFlow Logo" class="rounded-circle me-2" style="height: 100px; width: 100px; object-fit: cover; border: 3px solid orange;" />
                </center>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <center>
                    <h3>Donor Login</h3>
                </center>
            </div>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please fix the following errors:" CssClass="validation-summary" />

        <div>
            <label for="Email">Email:</label><br />
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Your Email" required="required"></asp:TextBox>
        </div>

        <div style="margin-top: 10px;">
            <label for="Password">Password:</label><br />
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter Your Password" TextMode="Password" required="required"></asp:TextBox>
        </div>

        <div class="row">
            <div class="col mx-auto">
                <center>
                    <div class="form-group">
                        <asp:Button class="btn btn-blue text-center" ID="LoginBtn" runat="server" Text="Login" OnClick="LoginBtn_Click" Style="margin-top: 15px; width:250px;" />
                    </div>
                </center>
            </div>
        </div>
        <div class="row px-3 mb-4">
            <asp:LinkButton ID="LinkButton2" runat="server" class="ml-auto forgot-btn mb-0 text-center" Style="color: black; margin-top: 20px;"
                OnClientClick="showForgotPasswordModal(); return false;">
                            Forgot Password?
                        </asp:LinkButton>
        </div>
    </div>
                        <div class="modal fade" id="forgotPasswordModal" tabindex="-1" aria-labelledby="forgotPasswordLabel" aria-hidden="true">
                        <div class="modal-dialog custom-size mx-auto">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="forgotPasswordLabel">Forgot Password</h5>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="mb-3">
                                            <label for="email" class="form-label">Email</label>
                                            <!--<input type="email" class="form-control" id="email" name="email">-->
                                             <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Enter Your Email" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                <div class="modal-footer">
                                    <!--<button type="button" class="btn btn-primary" id="resetPasswordButton">Reset Password</button>-->
                                    <asp:Button class="btn btn-blue text-center" ID="ResetBtn" runat="server" Text="Reset Password" OnClick="ResetBtn_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    </div> 
    








    <script>

    <!--Show and Hide Password Button-->
    function togglePassword() {
        var passwordField = document.getElementById("password");
        if (passwordField.type === "password") {
            passwordField.type = "text";
            document.querySelector(".btn-outline-secondary").textContent = "Hide";
        } else {
            passwordField.type = "password";
            document.querySelector(".btn-outline-secondary").textContent = "Show";
        }
    }

        // Forgot Password Script

      /*  document.getElementById("resetPasswordButton").addEventListener("click", function () {
            const email = document.getElementById("email").value.trim();

            if (!email) {
                alert("Please fill in email fields.");
                return;
            }

            fetch("LoginPage.aspx/ResetPassword", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ email })
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! Status: ${response.status}`);
                    }
                    return response.json();
                })
                .then(data => {
                    if (data.d) {
                        alert(data.d);
                    } else {
                        alert("An unexpected error occurred.");
                    }
                })
                .catch(error => console.error("Error:", error));
        });*/

        //Show Modal Script
        function showForgotPasswordModal() {
            const modal = new bootstrap.Modal(document.getElementById('forgotPasswordModal'));
            modal.show();
        }

        // clear textboxes
        $('#forgotPasswordModal').on('hidden.bs.modal', function () {
            document.getElementById('email').value = '';
        });


    </script>

    <!--Background Image-->
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



        .custom-size {
            max-width: 450px; /* Adjust as needed */
        }
        .btn-blue {
            background-color: #0056b3; 
            color: white; 
        }

        .btn-blue:hover {
            background-color: #007bff; 
            color: white;
        }

    </style>



</asp:Content>


