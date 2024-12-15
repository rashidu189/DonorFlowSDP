<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="DonorFlow.TestPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


            <div>
            <h2>Upload Image</h2>
            <asp:FileUpload ID="fileUpload" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
            <br />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>



    <asp:Image ID="imgDisplay" runat="server" Height="200px" Width="200px" />


</asp:Content>
