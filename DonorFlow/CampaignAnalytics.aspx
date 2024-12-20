<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CampaignAnalytics.aspx.cs" Inherits="DonorFlow.CampaignAnalytics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-4" style="margin-left:100px;">
            <div class="dashboard">
                <label style="font-weight: bold; display: block;">You Created Campaigns Status</label>
                <canvas id="progressChart1" width="300" height="150"></canvas>
            </div>
        </div>

        <div class="col-md-4" style="margin-left:200px;">
            <div class="dashboard">
                <label style="font-weight: bold; display: block;">You Created Campaigns Approval</label>
                <canvas id="progressChart" width="300" height="150"></canvas>
            </div>
        </div>
    </div>
    <script type="text/javascript">

    var UserId = "<%= UserId %>"; // Replace with actual server-side UserId variable

        $.ajax({
            url: "api/userCampaignStatus?UserId=" + UserId, // API endpoint with UserId query parameter
            type: "GET", // HTTP method (GET, POST, etc.)
            success: function (response) {
                const labels = response.map(item => item.ApprovalStatus); // Approval Status Labels (Approved, Not Approved)
                const data = response.map(item => item.ApprovalCount); // Counts

                // Create the Chart.js doughnut chart
                const ctx = document.getElementById('progressChart1').getContext('2d');
                new Chart(ctx, {
                    type: 'doughnut', // Donut chart type
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Campaigns Status',
                            data: data,
                            backgroundColor: [
                                'rgba(0, 255, 0, 0.5)',
                                'rgba(255, 0, 0, 0.5)'
                            ],
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        animation: {
                            animateRotate: true,
                            animateScale: true,
                        },
                        plugins: {
                            legend: {
                                display: true,
                                position: 'top'
                            },
                            tooltip: {
                                callbacks: {
                                    label: function (context) {
                                        return `${context.label}: ${context.raw} campaigns`;
                                    }
                                }
                            },
                            datalabels: {
                                formatter: (value, ctx) => {
                                    let sum = ctx.chart.data.datasets[0].data.reduce((a, b) => a + b, 0);
                                    let percentage = ((value / sum) * 100).toFixed(1) + "%";
                                    return percentage;
                                },
                                color: '#fff',
                                font: {
                                    weight: 'bold',
                                    size: 14
                                }
                            }
                        }
                    },
                    plugins: [ChartDataLabels] // Enable the datalabels plugin
                });
            },
            error: function (xhr, status, error) {
                // Handle errors (if any)
                console.error("Error:", error);
            }
        });

    </script>


        <script type="text/javascript">

            var UserId = "<%= UserId %>"; // Replace with actual server-side UserId variable

            $.ajax({
                url: "api/userCampaignApproval/userCampaignApprovalStatus?UserId=" + UserId, // API endpoint with UserId query parameter
                type: "GET", // HTTP method (GET, POST, etc.)
                success: function (response) {
                    const labels = response.map(item => item.ApprovalStatus); // Approval Status Labels (Approved, Not Approved)
                    const data = response.map(item => item.ApprovalCount); // Counts

                    // Create the Chart.js doughnut chart
                    const ctx = document.getElementById('progressChart').getContext('2d');
                    new Chart(ctx, {
                        type: 'doughnut', // Donut chart type
                        data: {
                            labels: labels,
                            datasets: [{
                                label: 'Campaign Approval Status',
                                data: data,
                                backgroundColor: [
                                    'rgba(204,0,102, 0.5)', // Color for "Not Approved"
                                    'rgba(0, 255, 0, 0.5)', // Color for "Approved"
                                    'rgba(255, 0, 0, 0.5)'

                                ],
                                borderWidth: 1
                            }]
                        },
                        options: {
                            responsive: true,
                            animation: {
                                animateRotate: true,
                                animateScale: true,
                            },
                            plugins: {
                                legend: {
                                    display: true,
                                    position: 'top'
                                },
                                tooltip: {
                                    callbacks: {
                                        label: function (context) {
                                            return `${context.label}: ${context.raw} campaigns`;
                                        }
                                    }
                                },
                                datalabels: {
                                    formatter: (value, ctx) => {
                                        let sum = ctx.chart.data.datasets[0].data.reduce((a, b) => a + b, 0);
                                        let percentage = ((value / sum) * 100).toFixed(1) + "%";
                                        return percentage;
                                    },
                                    color: '#fff',
                                    font: {
                                        weight: 'bold',
                                        size: 14
                                    }
                                }
                            }
                        },
                        plugins: [ChartDataLabels] // Enable the datalabels plugin
                    });
                },
                error: function (xhr, status, error) {
                    // Handle errors (if any)
                    console.error("Error:", error);
                }
            });

        </script>

        <div class="container">
        <div class="card">
            <div class="card-header">You Created Campaign Indetail Status</div>
            <div class="card-body">
                <asp:GridView ID="gvTransactions" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False">
                    <Columns>
                    <asp:BoundField DataField="Campaign_ID" HeaderText="Campaign ID" />
                    <asp:BoundField DataField="Campaign_Title" HeaderText="Campaign Title" />
                    <asp:BoundField DataField="Donation_Goal" HeaderText="Donation Goal" />
                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                    <asp:BoundField DataField="EndDate" HeaderText="End Date" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="IS_Approved" HeaderText="Approval Status" />
                    <asp:BoundField DataField="Transfer_Amount" HeaderText="Transferred Amount" />
                    <asp:BoundField DataField="Goal_Status" HeaderText="Goal Status" />
                    <asp:BoundField DataField="Progress_Percentage" HeaderText="Progress Percentage" />

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
