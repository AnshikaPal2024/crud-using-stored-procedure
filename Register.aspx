<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MyPractice.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <title>Sign Up</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
            font-family: Arial, sans-serif;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .container {
            display: flex;
            width: 800px; /* Adjust width as needed */
        }

        .image-container {
            flex: 1;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .card-container {
            flex: 1;
            padding: 20px;
        }

        .card {
            border-radius: 10px;
            box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);
            background-color: rgba(255, 255, 255, 0.9);
        }

        .text-crimson {
            color: crimson;
        }

        h2 {
            text-align: center;
        }

        .image-container img {
            max-width: 100%; /* Ensure it fits within the flex item */
            height: auto;
        }
    </style>
</head>
<body>
    <div class="card ">
    <form id="form1" runat="server">
        <div class="container">
            <div class="image-container">
                <img src="images/register.svg" alt="Signup Image" />
            </div>
            <div class="card-container">
                <div class="card p-4">
                    <h2  style="font-weight:bold; color:darkblue">Sign Up</h2>
                
                    <div class="form-group">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control mb-2" Placeholder="Enter Name" />
                        <asp:RequiredFieldValidator ID="RfvName" ForeColor="Red" ControlToValidate="txtName" runat="server" ErrorMessage="First fill the name" Display="Dynamic" />
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-2" Placeholder="Enter Email" TextMode="Email" />
                        <asp:RequiredFieldValidator ID="RfvEmail" ForeColor="Red" runat="server" ErrorMessage="Fill the Email" Display="Dynamic" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control mb-2" Placeholder="Enter Password" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="RfvPassword" ForeColor="Red" runat="server" ErrorMessage="Password is required" Display="Dynamic" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control mb-2" Placeholder="Enter Mobile" />
                        <asp:RegularExpressionValidator ID="Rfvmobile" runat="server" ForeColor="Red" ErrorMessage="Mobile number must be 10 digits" Display="Dynamic" ControlToValidate="txtMobile" ValidationExpression="^\d{10}$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-select mb-2">
                            <asp:ListItem Text="Select" Value="" />
                            <asp:ListItem Text="Male" Value="Male" />
                            <asp:ListItem Text="Female" Value="Female" />
                        </asp:DropDownList>
                    </div>
                    <div class="form-group mb-2">
                        <label>User Type</label>
                        <div class="form-check">
                            <asp:RadioButton ID="rbAdmin" runat="server" GroupName="UserType" CssClass="form-check-input" />
                            <label class="form-check-label" for="rbAdmin">Admin</label>
                        </div>
                        <div class="form-check">
                            <asp:RadioButton ID="rbUser" runat="server" GroupName="UserType" CssClass="form-check-input" />
                            <label class="form-check-label" for="rbUser">User</label>
                        </div>
                    </div>
                    <div class="text-center mb-2">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click1" />
                    </div  >
                    <p style="font-weight:bold; color:darkblue;">Have already an account? <a href="Login1.aspx" class="fw-bold text-body"><u class="text-crimson">Login here</u></a></p>
                </div>
                
                </div>
            
            </div>
     </form>
        </div>
      
   

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>