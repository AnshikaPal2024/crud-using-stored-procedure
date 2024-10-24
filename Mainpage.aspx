<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mainpage.aspx.cs" Inherits="MyPractice.SignIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SignIn</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link href="StyleSheet1.css" rel="stylesheet" />

    <style>
        #background {
            background-image: url('Images/pic2.jpg'); /* Your image path */
            background-size: cover; /* Adjust image display */
            width: 100%;
            height: 850px;
            position: relative;
            display: flex;
            flex-direction: column;
            justify-content: center; 
            align-items: flex-start; 
            padding: 20px; 
            color: white; 
            text-align: left; 
            margin-bottom:700px;
        }

        .label {
            font-size: 24px; /* Adjust font size */
            margin-bottom: 20px; /* Space between label and button */
        }

        .button {
            padding: 10px 20px; 
            background-color: #007BFF; 
            color: white; 
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px; 
        }

        .button:hover {
            background-color: #0056b3; /* Darker shade on hover */
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <header>
            <nav class="navbar navbar-expand-lg navbar-dark">
                <a class="navbar-brand" href="#">Website</a>
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
        
       
        <div id="background">
            <div class="label">Sign Up for Login</div>
            <asp:Button ID="btnlogin" runat="server" Text="Login" CssClass="login-button" OnClick="btnlogin_Click"/>
        </div>
    </form>
</body>
</html>
