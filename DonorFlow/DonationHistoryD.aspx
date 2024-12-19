<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DonationHistoryD.aspx.cs" Inherits="DonorFlow.DonationHistoryD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


        <div class="container">
        <div class="card">
            <div class="card-header">Transaction History</div>
            <div class="card-body">
                <asp:GridView ID="gvTransactions" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="TransactionID" HeaderText="Transaction ID" />
                        <asp:BoundField DataField="Campaign_ID" HeaderText="Campaign ID"/>
                        <asp:BoundField DataField="Campaign_Tittle" HeaderText="Campaign_Tittle" />
                        <asp:BoundField DataField="PaidAmount" HeaderText="Amount"/>
                        <asp:BoundField DataField="Transfered_Date" HeaderText="Transfered Date"/>
                        <asp:BoundField DataField="Transfered_User" HeaderText="Transfered User" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblNoTransactionMessage"  runat="server" CssClass="text-danger" Visible="false"></asp:Label>

            </div>
        </div>
    </div>

        <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
        }
        .container {
            margin-top: 50px;
        }
        .card {
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
        }
        .card-header {
            background-color: #007bff;
            color: white;
            font-size: 20px;
            text-align: center;
        }
        table {
            width: 100%;
            margin: 20px 0;
        }
        table th, table td {
            text-align: center;
            padding: 10px;
        }
        .btn-view-details {
            color: white;
            background-color: #007bff;
            border: none;
            padding: 5px 10px;
            border-radius: 4px;
            cursor: pointer;
        }
    </style>

</asp:Content>
