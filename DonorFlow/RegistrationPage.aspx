<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrationPage.aspx.cs" Inherits="DonorFlow.RegistrationPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <div style="max-width: 450px; margin: auto; padding: 20px; border: 2px solid #ccc; border-radius: 10px;">
            <center><h2 style="margin-bottom:30px;">Registration Form</h2></center>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please fix the following errors:" CssClass="validation-summary" />

            <div>
                <label for="FullName">Full Name:</label><br />
                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter your full name" required="required"></asp:TextBox>
            </div>

            <div style="margin-top: 10px;">
                <label for="Email">Email Address:</label><br />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter your email" TextMode="Email" required="required"></asp:TextBox>
            </div>

            <div style="margin-top: 10px;">
                <label for="PhoneNumber">Phone Number:</label><br />
                <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" placeholder="Enter your phone number" TextMode="Phone" required="required"></asp:TextBox>
            </div>

            <div style="margin-top: 10px;">
                <label for="DateOfBirth">Date of Birth (optional):</label><br />
                <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control" placeholder="Select your date of birth" TextMode="Date" ></asp:TextBox>
            </div>

            <div style="margin-top: 10px;">
                <label for="Address">Address:</label><br />
                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter your address" required="required"></asp:TextBox>
            </div>

        <div class="row">
            <div class="col mx-auto">
                <center>
                    <div class="form-group">
                        <asp:Button class="btn btn-red text-center" ID="LoginBtn" runat="server" Text="Register" OnClick="RegisterBtn_Click" Style="margin-top: 15px; width:250px;" />
                    </div>
                </center>
            </div>
        </div>
        </div>




    <style>

        .btn-red {
            background-color: #c72818; 
            color: white; 
        }

        .btn-red:hover {
            background-color: #db3423; 
            color: white;
        }

    </style>

</asp:Content>
