<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login1.aspx.cs" Inherits="MyPractice.Login1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link href="StyleSheet1.css" rel="stylesheet" />

    <style>   
        html, body {
            height: 100%;
            margin: 0;
            overflow: hidden;
        }

        .container {
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .card {
            border-radius: 30px;
            background-color: rgba(255, 255, 255, 0.9);
            margin-right: 20px; /* Space between card and image */
        }

        .form-control:focus {
            border-color: #007bff;
            box-shadow: none;
        }

        #ForgotPassword {
            display: none; 
        }

        .image-container {
            max-width: 30%;
            margin-right: 20px;
        }

        .image-container img {
            max-width: 100%;
            height: auto;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="image-container">
                <img src="images/undraw_voting_nvu7.svg" alt=" image" />
            </div>
           
            <div id="Login" class="card p-4" style="max-width: 400px; width: 100%;">
                <h3 class="text-center mb-4" style="color:darkblue;">Login Form</h3>
                <div class="form-group">
                    <asp:TextBox ID="txtname" runat="server" CssClass="form-control mb-3" Placeholder="Enter Name" />
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="password" CssClass="form-control mb-3" Placeholder="Enter Password" />
                </div>
                <div class="d-flex justify-content-between align-items-center mb-1">
                    <div style="color:darkblue;" class="form-check">
                        <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="form-check-input" />
                        <label class="form-check-label" for="chkRememberMe">Remember Me</label>
                    </div>
                    <a style="color:darkblue;" href="#" onclick="toggleForgotPassword();">Forgot Password?</a>
                </div>
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block" Text="Login" OnClick="btnSave_Click" />
                <div style="color:darkblue;" class="text-center mt-3">
                    Don't have an account? <a href="Register.aspx" class="text-danger" style="font-weight:bold;">Sign Up</a>
                </div>
            </div>

            <div id="ForgotPassword" class="card p-4" style="max-width: 400px; width: 100%; margin-top: 20px;">
                <h3 class="text-center mb-4 text-primary">Retrieve Password</h3>
                <div class="form-group">
                    <label for="email">Enter Email Id</label>
                    <div class="input-group-prepend">
                        <span class="input-group-text" style="background-color:cornflowerblue; color:white; display:inline-block; border:none;">@</span>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter your email"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblmsg" runat="server" Text="" CssClass="text-danger mt-2"></asp:Label>
                </div>
                <asp:Button ID="Submitbtn" runat="server" Text="Submit" CssClass="btn btn-success btn-block" OnClick="Submitbtn_Click" />
                <div class="text-center mt-3">
                    <a style="color:darkblue;" href="#" onclick="toggleForgotPassword();">Back to Login</a>
                </div>
            </div>
        </div>
    </form>

    <script>
        function toggleForgotPassword() {
            var loginDiv = document.getElementById('Login');
            var forgotPasswordDiv = document.getElementById('ForgotPassword');
            if (forgotPasswordDiv.style.display === "none" || forgotPasswordDiv.style.display === "") {
                loginDiv.style.display = "none"; // Hide the login card
                forgotPasswordDiv.style.display = "block"; // Show the forgot password section
            } else {
                loginDiv.style.display = "block"; // Show the login card
                forgotPasswordDiv.style.display = "none"; // Hide the forgot password section
            }
        }

        document.addEventListener('DOMContentLoaded', () => {
            const forgotPasswordDiv = document.getElementById('ForgotPassword');
            forgotPasswordDiv.style.display = 'none'; // Initially hide the forgot password section
        });
    </script>
    
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
