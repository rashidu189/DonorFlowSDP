<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="DonorFlow.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Hero Section -->
    <div class="hero">
        <div class="hero-overlay"></div>
        <div class="hero-content">
            <h1>Welcome to CrowdFundPro</h1>
            <p>Empower dreams, support causes, and make a difference together.</p>
            <a href="signup.html" class="btn btn-primary btn-lg">Get Started</a>
            <a href="#features" class="btn btn-outline-light btn-lg">Learn More</a>
        </div>
    </div>

    <!-- Features Section -->
    <section id="features" class="features">
        <div class="container">
            <h2>Why Choose CrowdFundPro?</h2>
            <div class="row">
                <div class="col-md-4 feature-item">
                    <h3>Easy Campaign Setup</h3>
                    <p>Launch your fundraising campaign in minutes with our intuitive tools.</p>
                </div>
                <div class="col-md-4 feature-item">
                    <h3>Secure Payments</h3>
                    <p>Ensure your transactions are safe with our industry-leading payment gateway.</p>
                </div>
                <div class="col-md-4 feature-item">
                    <h3>Community Support</h3>
                    <p>Connect with a global audience and share your vision with the world.</p>
                </div>
            </div>
        </div>
    </section>



    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Custom CSS -->
    <style>
        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
        }

        .hero {
            background: url('https://via.placeholder.com/1920x1080') no-repeat center center;
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            color: #fff;
            text-align: center;
            position: relative;
        }

        .hero-overlay {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5);
            z-index: 1;
        }

        .hero-content {
            position: relative;
            z-index: 2;
        }

        .hero h1 {
            font-size: 3rem;
            margin-bottom: 1rem;
        }

        .hero p {
            font-size: 1.2rem;
            margin-bottom: 2rem;
        }

        .btn-primary {
            background-color: #007bff;
            border: none;
        }

        .btn-primary:hover {
            background-color: #0056b3;
        }

        .features {
            padding: 4rem 2rem;
            text-align: center;
        }

        .features h2 {
            margin-bottom: 2rem;
        }

        .feature-item {
            margin-bottom: 2rem;
        }

        .footer {
            background-color: #f8f9fa;
            padding: 2rem 0;
            text-align: center;
        }

    </style>


</asp:Content>
