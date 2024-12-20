<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardPage.aspx.cs" Inherits="DonorFlow.DashboardPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">






    <script>

        $(document).ready(function () {
            // Set dummy data for Total Students
            var totalStudents = 10;
            $("#totalStudentCount").text(totalStudents);

            // Set dummy data for Total Employees
            var totalEmployees = 5;
            $("#totalEmployeeCount").text(totalEmployees);

            // Set dummy data for Total Revenue
            var totalRevenue = 3;
            $("#totalRevenue").text(totalRevenue);
        });



    </script>


    <div class="container col-md-12">
        <div class="row">
            <div class="col-lg-4 col-md-6 mb-4">
                <div class="card text-white bg-dark mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Total Donors</h5>
                        <p id="totalStudentCount" class="card-text">Loading...</p>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-6 mb-4">
                <div class="card text-white bg-dark mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Total Campaign Creators</h5>
                        <p id="totalEmployeeCount" class="card-text">Loading...</p>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-6 mb-4">
                <div class="card text-white bg-dark mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Total Administrators</h5>
                        <p id="totalRevenue" class="card-text">Loading...</p>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="dashboard" style="width: 100%; height: 300px;">
                    <label style="font-weight: bold; display: block;">Campaigns Status</label>
                    <canvas id="progressChart1"></canvas>
                </div>
            </div>

            <div class="col-md-4">
                <div class="dashboard" style="width: 100%; height: 300px;">
                    <label style="font-weight: bold; display: block;">Campaigns Approval</label>
                    <canvas id="progressChart"></canvas>
                </div>
            </div>

            <div class="col-md-4">
                <div class="dashboard" style="width: 100%; height: 300px;">
                    <label style="font-weight: bold; display: block;">Donated Amount</label>
                    <canvas id="revenueChart" style="width: 100%; height: 100%;"></canvas>
                </div>
            </div>
        </div>

        <div class="row" style="margin-top: 60px; margin-bottom:100px;">
            <div class="col-md-4">
                <div class="dashboard" style="width: 100%; height: 300px;">
                    <label style="font-weight: bold; display: block;">List of Campaign Catagory</label>
                    <div class="chart-container" style="width: 400px; height: 400px; margin: 20px auto;">
                        <canvas id="pieChart"></canvas>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="dashboard" style="width: 100%; height: 300px;">
                    <label style="font-weight: bold; display: block;">Active Campaigns and Donation Goals</label>
                    <canvas id="campaignChart" style="width: 800px; height: 500px; margin: 20px auto;"></canvas>
                </div>
            </div>
        </div>

    </div>





<script>
    document.addEventListener("DOMContentLoaded", () => {
        // Fetch data from the API
        $.ajax({
            url: '/api/campaignStatus',
            method: 'GET',
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
                                'rgba(255, 0, 0, 0.5)',
                                'rgba(0, 255, 0, 0.5)' 
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
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
    });
</script>


    <script>
        $(document).ready(function () {
            const ctx = document.getElementById('campaignChart').getContext('2d');

            // Fetch data from the API
            $.ajax({
                url: '/api/activeCampaigns',
                method: 'GET',
                success: function (response) {
                    const labels = response.map(item => item.CampaignTitle); // Campaign Titles (X-axis)
                    const data = response.map(item => item.DonationGoal); // Donation Goals (Y-axis)

                    new Chart(ctx, {
                        type: 'bar', // Bar chart type
                        data: {
                            labels: labels,
                            datasets: [{
                                label: 'Donation Goals ($)',
                                data: data,
                                backgroundColor: 'rgba(54, 162, 235, 0.6)', // Bar color
                                borderColor: 'rgba(54, 162, 235, 1)',
                                borderWidth: 1
                            }]
                        },
                        options: {
                            responsive: true,
                            plugins: {
                                legend: {
                                    display: true,
                                    position: 'top'
                                },
                                tooltip: {
                                    callbacks: {
                                        label: function (context) {
                                            return `${context.label}: $${context.raw.toFixed(2)}`;
                                        }
                                    }
                                }
                            },
                            scales: {
                                x: {
                                    title: {
                                        display: true,
                                        text: 'Campaign Titles',
                                        color: '#333',
                                        font: {
                                            size: 14
                                        }
                                    }
                                },
                                y: {
                                    beginAtZero: true,
                                    title: {
                                        display: true,
                                        text: 'Donation Goal ($)',
                                        color: '#333',
                                        font: {
                                            size: 14
                                        }
                                    }
                                }
                            }
                        }
                    });
                },
                error: function (error) {
                    console.error('Error fetching data:', error);
                }
            });
        });
</script>

<script>
    fetch('/api/GetCampaignTitles')
        .then(response => response.json())
        .then(data => {
            console.log("Campaign Data:", data);
            // Use the data (e.g., populate a pie chart)
        })
        .catch(error => console.error("Error fetching data:", error));
</script>


    <script>
        // Fetch data from Web API
        fetch('/api/GetCampaignTitles')
            .then(response => response.json())
            .then(data => {
                // Extract labels (campaign titles) and values
                const labels = data.map(item => item.Title);
                const values = data.map(item => item.Value);

                // Configure and render the pie chart
                const ctx = document.getElementById('pieChart').getContext('2d');
                new Chart(ctx, {
                    type: 'pie',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Campaign Distribution',
                            data: values,
                            backgroundColor: [
                                'rgba(255, 99, 132, 0.6)',
                                'rgba(54, 162, 235, 0.6)',
                                'rgba(255, 206, 86, 0.6)',
                                'rgba(75, 192, 192, 0.6)',
                                'rgba(153, 102, 255, 0.6)',
                                'rgba(255, 159, 64, 0.6)'
                            ],
                            borderColor: [
                                'rgba(255, 99, 132, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(255, 206, 86, 1)',
                                'rgba(75, 192, 192, 1)',
                                'rgba(153, 102, 255, 1)',
                                'rgba(255, 159, 64, 1)'
                            ],
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            legend: {
                                position: 'top'
                            },
                            tooltip: {
                                enabled: true
                            }
                        }
                    }
                });
            })
            .catch(error => console.error("Error fetching data:", error));
    </script>

<script>
    $(document).ready(function () {
        const ctx = document.getElementById('revenueChart').getContext('2d');

        // Fetch data from API
        $.ajax({
            url: '/api/revenue', // API endpoint
            method: 'GET',
            success: function (response) {
                const labels = response.map(item => item.Role); // X-axis labels
                const data = response.map(item => item.TotalAmount); // Y-axis data

                new Chart(ctx, {
                    type: 'bar', // Bar chart type
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Donated Amount by Role ($)',
                            data: data,
                            backgroundColor: [
                                'rgba(75, 192, 192, 0.6)',
                                'rgba(255, 99, 132, 0.6)',
                                'rgba(255, 206, 86, 0.6)',
                                'rgba(54, 162, 235, 0.6)',
                                'rgba(153, 102, 255, 0.6)'
                            ],
                            borderColor: [
                                'rgba(75, 192, 192, 1)',
                                'rgba(255, 99, 132, 1)',
                                'rgba(255, 206, 86, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(153, 102, 255, 1)'
                            ],
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            legend: {
                                display: true,
                                position: 'top'
                            },
                            tooltip: {
                                callbacks: {
                                    label: function (context) {
                                        return `${context.label}: $${context.raw.toFixed(2)}`;
                                    }
                                }
                            }
                        },
                        scales: {
                            x: {
                                title: {
                                    display: true,
                                    text: 'Roles',
                                    color: '#333',
                                    font: {
                                        size: 14
                                    }
                                }
                            },
                            y: {
                                beginAtZero: true,
                                title: {
                                    display: true,
                                    text: 'Donated Amount ($)',
                                    color: '#333',
                                    font: {
                                        size: 14
                                    }
                                }
                            }
                        }
                    }
                });
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
    });
</script>

<script>
    document.addEventListener("DOMContentLoaded", () => {
        // Fetch data from the API
        $.ajax({
            url: '/api/campaignApproval',
            method: 'GET',
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
                                'rgba(255, 0, 0, 0.5)',
                                'rgba(0, 255, 0, 0.5)' // Color for "Approved"
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
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
    });
</script>



<script>
    document.addEventListener("DOMContentLoaded", () => {

        // Fetch data from the API
        $.ajax({
            url: '/api/activeCampaigns',
            method: 'GET',
            success: function (response) {
                const labels = response.map(item => item.CampaignTitle); // Campaign Titles (X-axis)
                const data = response.map(item => item.DonationGoal); // Donation Goals (Y-axis)

                new Chart(ctx, {
                    type: 'bar', // Bar chart type
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Donation Goals ($)',
                            data: data,
                            backgroundColor: 'rgba(54, 162, 235, 0.6)', // Bar color
                            borderColor: 'rgba(54, 162, 235, 1)',
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            legend: {
                                display: true,
                                position: 'top'
                            },
                            tooltip: {
                                callbacks: {
                                    label: function (context) {
                                        return `${context.label}: $${context.raw.toFixed(2)}`;
                                    }
                                }
                            }
                        },
                        scales: {
                            x: {
                                title: {
                                    display: true,
                                    text: 'Campaign Titles',
                                    color: '#333',
                                    font: {
                                        size: 14
                                    }
                                }
                            },
                            y: {
                                beginAtZero: true,
                                title: {
                                    display: true,
                                    text: 'Donation Goal ($)',
                                    color: '#333',
                                    font: {
                                        size: 14
                                    }
                                }
                            }
                        }
                    }
                });
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
    });
</script>

<style>
    .dashboard-card {
    background-color: white;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    text-align: center;
}

html, body {
    height: 100%;
    margin: 0;
}

body {
    display: flex;
    flex-direction: column;
}

.container {
    flex: 1;
    padding-bottom: 50px; /* Optional, adds some space at the bottom */
}

</style>

</asp:Content>
