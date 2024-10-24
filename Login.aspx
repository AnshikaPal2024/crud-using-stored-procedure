<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="MyPractice.Login" %>

<!DOCTYPE html>
<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Form</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link href="StyleSheet1.css" rel="stylesheet" />
    <style>
        html, body {
            height: 100%;
            margin: 0;
            overflow: hidden;
        }

        body {
            background-image: url('https://wallpapers.com/images/hd/simple-pictures-amwy38yukfhoh65i.jpg');
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            font-family: Arial, sans-serif;
        }

        .navbar {
            background-color: darkslategrey;
        }

        .navbar-nav .nav-link {
            color: white;
            padding: 10px 15px;
            transition: background-color 0.3s, border-radius 0.3s;
        }

        .navbar-nav .nav-link:hover {
            background-color: dodgerblue;
            border-radius: 5px;
        }

        .card {
            border-radius: 20px;
          
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
        }

        .form-control:focus {
            border-color: #007bff;
            box-shadow: none;
        }
        

        #Login {
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        #ForgotPassword {
            display: none; /* Initially hidden */
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <header>
            <nav class="navbar navbar-expand-lg navbar-dark">
                <a class="navbar-brand" href="#">Website</a>
                <div class="collapse navbar-collapse">
                    <ul class="navbar-nav mr-auto">
                        <li class="nav-item"><a class="nav-link" href="#">Home</a></li>
                        <li class="nav-item"><a class="nav-link" href="#">About</a></li>
                        <li class="nav-item"><a class="nav-link" href="#">Contact</a></li>
                    </ul>
             </div>
                  <div id="toggle">
                    <div class="button">
                        <a href="#">Light<br />
                            <h6>Mode</h6>
                        </a>
                        <span>Dark<br />
                            <h6>Mode</h6>
                        </span>
                    </div>
                </div>
         
            <script>
                document.addEventListener('DOMContentLoaded', () => {
                    const body = document.querySelector('body');
                    const toggle = document.getElementById('toggle');

                    toggle.onclick = function () {
                        toggle.classList.toggle('active');
                        body.classList.toggle('active');
                    };
                });
            </script>
            </nav>
        </header>

        <div id="Login">
            <div class="card p-4" style="max-width: 400px; width: 100%;">
                <h3 class="text-center mb-4 text-primary">Login Form</h3>
                <div class="form-group">
                    <asp:TextBox ID="txtname" runat="server" CssClass="form-control mb-3" Placeholder="Enter Name" />
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="password" CssClass="form-control mb-3" Placeholder="Enter Password" />
                </div>
                <div class="d-flex justify-content-between align-items-center mb-1">
                    <div class="form-check">
                        <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="form-check-input" />
                        <label class="form-check-label" for="chkRememberMe">Remember Me</label>
                    </div>
                    <a href="#" onclick="toggleForgotPassword();">Forgot Password?</a>
                </div>
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnSave_Click" Text="Login" />
                <div class="text-center mt-3">
                    Don't have an account? <a href="Register.aspx" class="text-primary">Sign Up</a>
                </div>
            </div>
        </div>
        <div class="row justify-content-center">
        <div id="ForgotPassword"  class ="card p-4" style="max-width: 400px; width: 100%; margin:100px;">
            <h3 class="text-center mb-4 text-primary">Retrieve Password</h3>
            <div class="form-group">
                <label for="email">Enter Email Id</label>
                <div class="input-group-prepend">
                <span class="input-group-text" style="background-color:cornflowerblue; color:white;  display:inline-block; border:none;">@</span>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter your email"></asp:TextBox></div>
                <asp:Label ID="lblmsg" runat="server" Text="" CssClass="text-danger mt-2"></asp:Label>
            </div>
            <asp:Button ID="Submitbtn" runat="server" Text="Submit" CssClass="btn btn-success btn-block" OnClick="Submitbtn_Click" />
            <div class="text-center mt-3">
                <a href="#" onclick="toggleForgotPassword();">Back to Login</a>
            </div>
        </div>
            </div>
    </form>
    

    <script>
        function toggleForgotPassword() {
            var loginDiv = document.getElementById('Login');
            var forgotPasswordDiv = document.getElementById('ForgotPassword');
            if (forgotPasswordDiv.style.display === "none" || forgotPasswordDiv.style.display === "") {
                loginDiv.style.display = "none";
                forgotPasswordDiv.style.display = "block";
            } else {
                loginDiv.style.display = "flex";
                forgotPasswordDiv.style.display = "none";
            }
        }
    </script>

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>--%>
