<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="DonorFlow.ManageUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


  <div class="container-fluid" style="margin-top:40px;">  
    <div class="StudentManagement">
       <div class="card-body Newmember">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label8" CssClass="txtbox" runat="server" Text="User Name"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="TextBox3" CssClass="txtbox" runat="server" Style="width: 250px;"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label9" CssClass="txtbox" runat="server" Text="User Role"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="DropDownList2" CssClass="txtbox" runat="server" Style="width: 250px;">
                        <asp:ListItem Text="Donor" Value="Donor"></asp:ListItem>
                        <asp:ListItem Text="Campaign Creator" Value="Campaign Creator"></asp:ListItem>
                        <asp:ListItem Text="Administrator" Value="Administrator"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label1" CssClass="txtbox" runat="server" Text="User ID"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="TextBox1" CssClass="txtbox" runat="server" Style="width: 250px;"></asp:TextBox>
                </td>
                <td colspan="1">
                    <asp:Button class="btn btn-success text-center SearchBtn" ID="SearchBtn" runat="server" Text="Search" OnClick="SearchBtn_Click" />
                </td>
                <td colspan="1">
                    <asp:Button class="btn btn-info text-center RefreshBtn" ID="RefreshBtn" runat="server" Text="Refresh" OnClick="RefreshBtn_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" CssClass="txtbox" runat="server" Text="Status"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="DropDownList1" CssClass="txtbox" runat="server" Style="width: 250px;">
                        <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="UserManagement" runat="server" AutoGenerateColumns="false" CssClass="gridview-style" OnRowCommand="UserManagement_RowCommand">
            <Columns>
                <asp:BoundField DataField="User Name" HeaderText="User Name" />
                <asp:BoundField DataField="User Role" HeaderText="User Role" />
                <asp:BoundField DataField="User ID" HeaderText="User ID" />
                <asp:BoundField DataField="User Email" HeaderText="User Email" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'
                            Text="Edit" CssClass="edit-button" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>


    </div>
        </div>
</div>



<style>

        .SearchBtn{
            margin-left: 20px;
            background-color:#3e3eee;
        }
        .RefreshBtn{
            margin-left: 20px;
            background-color:#eed33e;
        }
        .txtbox{
            margin-left: 20px;
            white-space: nowrap;
        }

        /* GridView Styling */
        .gridview-style {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
            font-size: 14px;
            text-align: left;
        }

        .gridview-style th, .gridview-style td {
            padding: 12px 15px;
            border: 3px solid #ddd;
            
        }

        .gridview-style th {
            background-color: #060606;
            color: white;
            font-weight: bold;
        }

        .gridview-style tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .gridview-style tr:hover {
            background-color: #f1f1f1;
        }

        /* Styling for the TextBox (Select Date) */
        .form-control {
            width: 100%;
            padding: 8px;
            font-size: 16px;
            border-radius: 4px;
            border: 1px solid #ccc;
            box-sizing: border-box;
        }

        /* Button Styling */
        .edit-button {
            background-color: #060606;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px;
            text-decoration: none;
            display: inline-block;
            text-align: center;
        }

        .edit-button:hover {
            background-color: #45a049;
        }


</style>



<script>





</script>


</asp:Content>
