<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DonorFlow.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>


</head>
    <body>

                                <!-- Progress Chart 1 -->
                        <div class="chart-container" style="width: 300px; height: auto; margin: auto; margin-left: -40px; margin-bottom: 40px;">
                            <canvas id="progressChart"></canvas>
                        </div>
     <!--   <div class="container-fluid" style="background-color: #bcbcc4;">
            <div class="row">

                <!-- Main Content -->
          <!--      <main class="DBoard" style="background-color: white;">
                    <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                        <h1 class="h2">Dashboard</h1>
                    </div>

                    <!-- Dashboard Cards -->
            <!--        <div class="row">
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="card text-white bg-primary mb-3">
                                <div class="card-body">
                                    <h5 class="card-title">Total Students</h5>
                                    <p id="totalStudentCount" class="card-text">Loading...</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="card text-white bg-success mb-3">
                                <div class="card-body">
                                    <h5 class="card-title">Total Employees</h5>
                                    <p id="totalEmployeeCount" class="card-text">Loading...</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="card text-white bg-info mb-3">
                                <div class="card-body">
                                    <h5 class="card-title">Revenue</h5>
                                    <p id="totalRevenue" class="card-text">Loading...</p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col" style="margin-left: 55px;">
                            <h5 class="card-title">Count Of User Lists</h5>
                        </div>
                        <div class="col">
                            <h5 class="card-title" style="margin-left: 170px;">Course Approval</h5>
                        </div>
                        <div class="col">
                            <h5 class="card-title" style="margin-left: 100px;">Leave Approval</h5>
                        </div>
                    </div>
                    <div class="row">

                        <!-- Enrollment by Semester - Bar Chart -->
            <!--            <div class="chart-container" style="width: 500px; height: auto; margin: auto; margin-left: 50px;">
                            <canvas id="barChart"></canvas>
                        </div>

                        <!-- Progress Chart 1 -->
                 <!--        <div class="chart-container" style="width: 300px; height: auto; margin: auto; margin-left: -40px; margin-bottom: 40px;">
                            <canvas id="progressChart"></canvas>
                        </div>

                        <!-- Progress Chart 2 -->
              <!--           <div class="chart-container" style="width: 300px; height: auto; margin: auto; margin-left: -40px; margin-bottom: 40px;">
                            <canvas id="progressChart2"></canvas>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col" style="margin-left: 55px;">
                            <h5 class="card-title">Courses per Faculty</h5>
                        </div>
                        <div class="col" style="margin-left: -50px;">
                            <h5 class="card-title">Batches per Faculty</h5>
                        </div>
                    </div>
                    <div class="row">

                        <!--Polar Area Chart-->
            <!--             <div class="col-md-6">
                            <div class="chart-container" style="width: 350px; height: auto; margin-left: 60px; margin-top: 50px;">
                                <canvas id="polarAreaChart"></canvas>
                            </div>
                        </div>



                        <!-- Combined Stacked Bar and Line Chart -->
          <!--               <div class="col-md-6">
                            <div class="chart-container" style="width: 800px; height: auto; margin-left: -200px; margin-top: 50px;">
                                <canvas id="combinedChart"></canvas>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col" style="margin-left: 55px; margin-top: 30px;">
                            <h5 class="card-title">A Percentage of Grades of All Students</h5>
                        </div>
                        <div class="col" style="margin-left: 10px; margin-top: 30px;">
                            <h5 class="card-title">Profit and Cost of  From previous years to Current Year</h5>
                        </div>
                    </div>
                    <div class="row">

                        <!-- Pie Chart -->
         <!--                <div class="col-md-6">
                            <div class="chart-container" style="width: 400px; margin-left: 40px; height: auto; margin-top: 20px; margin-bottom: 20px;">
                                <canvas id="pieChart"></canvas>
                            </div>
                        </div>


                        <!-- Line Graph -->
        <!--                 <div class="col-md-6">
                            <div class="chart-container" style="width: 800px; margin-left: -200px; height: auto; margin-top: 20px; margin-bottom: 20px;">
                                <canvas id="lineChart"></canvas>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
        </div>


<!--CSS-->

<!--Background Image-->
  <!--  <style>

    .chart-card {
        height: 100%; /* Ensures card takes up full height */
    }

    .chart-card .card-body {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        height: 300px; /* Sets a consistent height for each chart */
    }

    #combinedChart {
        max-height: 250px; /* Ensures consistent canvas size */
    }




    </style>


<!--Java Script-->

    <script>


        document.addEventListener("DOMContentLoaded", () => {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/GetCourseApprovalChartData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = JSON.parse(response.d)[0]; // Get the first row of data

                    // Prepare data for Chart.js
                    const chartData = [
                        data.Approved,      // Approved count
                        data.Not_Approved   // Not Approved count
                    ];

                    // Set the labels
                    const labels = ["Approved", "Not Approved"];

                    // Create the Chart.js doughnut chart
                    const ctx = document.getElementById("progressChart").getContext("2d");
                    new Chart(ctx, {
                        type: 'doughnut',
                        data: {
                            labels: labels,
                            datasets: [{
                                label: "Progress",
                                backgroundColor: ["rgba(62, 149, 205, 0.7)", "rgba(142, 94, 162, 0.7)"], // Transparent colors
                                data: chartData
                            }]
                        },
                        options: {
                            animation: {
                                animateRotate: true,
                                animateScale: true,
                            },
                            plugins: {
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
                        plugins: [ChartDataLabels]
                    });
                },
                error: function (err) {
                    console.log("Error: " + err.responseText);
                }
            });
        });

        // Total Student

       $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/GetTotalStudentChartData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // Parse the JSON response
                    var data = JSON.parse(response.d);
                    // Update the total student count in the card
                    $("#totalStudentCount").text(data[0].TotalStudent);
                },
                error: function () {
                    $("#totalStudentCount").text("Error loading data");
                }
            });
        });

        // Total Employee

        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/GetTotalEmployeeChartData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // Parse the JSON response
                    var data = JSON.parse(response.d);
                    // Update the total student count in the card
                    $("#totalEmployeeCount").text(data[0].TotalEmployee);
                },
                error: function () {
                    $("#totalEmployeeCount").text("Error loading data");
                }
            });
        });


        //Total Revenue

        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/GetTotalRevenueChartData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // Parse the JSON response
                    var data = JSON.parse(response.d);
                    // Update the total student count in the card
                    $("#totalRevenue").text(data[0].TotalRevenue);
                },
                error: function () {
                    $("#totalRevenue").text("Error loading data");
                }
            });
        });



        // User List Bar Chart

        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/GetUserListChartData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = JSON.parse(response.d);

                    // Prepare data for Chart.js
                    var labels = [];
                    var values = [];

                    data.forEach(function (item) {
                        labels.push(item.User_Type);       // Populate the label (User_Type)
                        values.push(item.User_List);       // Populate the count (User_List)
                    });

                    // Initialize Chart.js
                    const barCtx = document.getElementById('barChart').getContext('2d');
                    new Chart(barCtx, {
                        type: 'bar',
                        data: {
                            labels: labels,
                            datasets: [{
                                label: 'User List',
                                data: values,
                                backgroundColor: 'rgba(54, 162, 235, 0.7)',
                            }]
                        },
                        options: {
                            scales: {
                                y: {
                                    beginAtZero: true
                                }
                            }
                        }
                    });
                },
                error: function (err) {
                    console.log("Error: " + err.responseText);
                }
            });
        });

        // Grade Wise Pie Chart

        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/GetGradeWiseChartData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // Parse the JSON response to get chart data
                    const resultData = JSON.parse(response.d);
                    const labels = [];
                    const data = [];

                    resultData.forEach(row => {
                        labels.push(row.Grade);           // Assuming "Grade" is your label
                        data.push(row.Count_of_Grade);    // Assuming "Count_of_Grade" is the count
                    });

                    // Create pie chart with dynamic data
                    const pieCtx = document.getElementById('pieChart').getContext('2d');
                    new Chart(pieCtx, {
                        type: 'pie',
                        data: {
                            labels: labels,
                            datasets: [{
                                data: data,
                                backgroundColor: [
                                    'rgba(76, 175, 80, 0.7)',   // Green with 0.7 opacity
                                    'rgba(255, 152, 0, 0.7)',   // Orange with 0.7 opacity
                                    'rgba(33, 150, 243, 0.7)',  // Blue with 0.7 opacity
                                    'rgba(233, 30, 99, 0.7)',   // Pink with 0.7 opacity
                                    'rgba(156, 39, 176, 0.7)'   // Purple with 0.7 opacity
                                ]
                            }]
                        },
                        options: {
                            plugins: {
                                datalabels: {
                                    formatter: (value, ctx) => {
                                        let total = ctx.chart.data.datasets[0].data.reduce((a, b) => a + b, 0);
                                        let percentage = ((value / total) * 100).toFixed(1) + "%";
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
                        plugins: [ChartDataLabels] // Enable the plugin
                    });
                },
                error: function (error) {
                    console.log("Error fetching data:", error);
                }
            });
        });


        // Line Chart (Profit and Cost)

        const ctx = document.getElementById('lineChart').getContext('2d');
        const lineChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ['2018', '2019', '2020', '2021', '2022', '2023', '2024'],
                datasets: [{
                    label: 'Revenue',
                    data: [7000000, 7550000, 8500000, 10500000, 15000000, 27000000, 57650000],
                    borderColor: 'rgba(82, 239, 108, 1)',
                    backgroundColor: 'rgba(82, 239, 108, 0.5)',
                    borderWidth: 2,
                    tension: 0.1 // For smooth curve line
                }, {
                    label: 'Cost', // New label
                    data: [13000000, 3500000, 1500000, 8500000, 550000, 250000, 8500000], // Data for second dataset
                    borderColor: 'rgba(247, 52, 72, 1)',
                    backgroundColor: 'rgba(247, 52, 72, 0.5)',
                    borderWidth: 2,
                    tension: 0.1 // For smooth curve line
                }]
            },
            options: {
                responsive: true,
                scales: {
                    x: {
                        beginAtZero: true
                    },
                    y: {
                        beginAtZero: true
                    }
                },
                plugins: {
                    legend: {
                        display: true,
                        position: 'top'
                    }
                }
            }
        });



        // Batch Wise Bar Chart

        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/GetFacultyWiseBatchListChartData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = JSON.parse(response.d);

                    // Prepare data for Chart.js
                    var labels = [];
                    var values = [];

                    data.forEach(function (item) {
                        labels.push(item.Faculty);       // Populate the label (Faculty)
                        values.push(item.BatchNo);       // Populate the count (Batch No)
                    });

                    // Initialize Chart.js
                    const barCtx = document.getElementById('combinedChart').getContext('2d');
                    new Chart(barCtx, {
                        type: 'bar',
                        data: {
                            labels: labels,
                            datasets: [{
                                label: 'Batches Per Faculty',
                                data: values,
                                backgroundColor: 'rgb(247, 44, 112,0.7)',
                            }]
                        },
                        options: {
                            scales: {
                                y: {
                                    beginAtZero: true
                                }
                            }
                        }
                    });
                },
                error: function (err) {
                    console.log("Error: " + err.responseText);
                }
            });
        });



        // Pending Course Approval

        document.addEventListener("DOMContentLoaded", () => {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/GetCourseApprovalChartData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = JSON.parse(response.d)[0]; // Get the first row of data

                    // Prepare data for Chart.js
                    const chartData = [
                        data.Approved,      // Approved count
                        data.Not_Approved   // Not Approved count
                    ];

                    // Set the labels
                    const labels = ["Approved", "Not Approved"];

                    // Create the Chart.js doughnut chart
                    const ctx = document.getElementById("progressChart").getContext("2d");
                    new Chart(ctx, {
                        type: 'doughnut',
                        data: {
                            labels: labels,
                            datasets: [{
                                label: "Progress",
                                backgroundColor: ["rgba(62, 149, 205, 0.7)", "rgba(142, 94, 162, 0.7)"], // Transparent colors
                                data: chartData
                            }]
                        },
                        options: {
                            animation: {
                                animateRotate: true,
                                animateScale: true,
                            },
                            plugins: {
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
                        plugins: [ChartDataLabels]
                    });
                },
                error: function (err) {
                    console.log("Error: " + err.responseText);
                }
            });
        });


        // Pending Leave Approval

        document.addEventListener("DOMContentLoaded", () => {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/GetLeaveApprovalChartData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = JSON.parse(response.d)[0]; // Get the first row of data

                    // Prepare data for Chart.js
                    const chartData = [
                        data.Approved,      // Approved count
                        data.Not_Approved   // Not Approved count
                    ];

                    // Set the labels
                    const labels = ["Approved", "Not Approved"];

                    // Create the Chart.js doughnut chart
                    const ctx = document.getElementById("progressChart2").getContext("2d");
                    new Chart(ctx, {
                        type: 'doughnut',
                        data: {
                            labels: labels,
                            datasets: [{
                                label: "Leave Approval",
                                backgroundColor: ["rgba(62, 205, 67, 0.7)", "rgba(227, 79, 79, 0.7)"], // Transparent colors
                                data: chartData
                            }]
                        },
                        options: {
                            animation: {
                                animateRotate: true,
                                animateScale: true,
                            },
                            plugins: {
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
                        plugins: [ChartDataLabels]
                    });
                },
                error: function (err) {
                    console.log("Error: " + err.responseText);
                }
            });
        });




        // Polar Area Chart (Course List)

        document.addEventListener("DOMContentLoaded", () => {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/GetFacultyWiseCourseListChartData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = JSON.parse(response.d);

                    // Prepare data for Chart.js
                    var labels = data.map(row => row.Faculty);
                    var courseCounts = data.map(row => row.CourseCount);

                    // Create the Chart.js polar area chart
                    const ctx = document.getElementById('polarAreaChart').getContext('2d');
                    const polarAreaChart = new Chart(ctx, {
                        type: 'polarArea',
                        data: {
                            labels: labels,
                            datasets: [{
                                label: 'Courses per Faculty',
                                data: courseCounts,
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
                            scales: {
                                r: {
                                    angleLines: {
                                        display: true
                                    },
                                    suggestedMin: 0,
                                    suggestedMax: Math.max(...courseCounts) + 5 // Adjust max value for scale
                                }
                            }
                        }
                    });
                },
                error: function (err) {
                    console.log("Error: " + err.responseText);
                }
            });
        });


    </script>

</body>
    </html>