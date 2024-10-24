<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forgotpassword.aspx.cs" Inherits="MyPractice.Forgotpassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Retrieve Password</title>
    <!-- Bootstrap CSS -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <div id="Forgotpassword">
    <form id="form1" runat="server">
        <div id="password" style="margin-top: 50px;">
            <div class="row justify-content-center">
                <div class="col-md-6 col-lg-4">
                    <div class="panel" style="border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);">
                        <div class="panel-heading" style="background-color: #007bff; color: white; text-align: center; padding: 10px; border-radius: 8px 8px 0 0;">
                            <h3 class="panel-title" style="margin: 0;">Retrieve Password</h3>
                        </div>
                        <div class="panel-body" style="padding: 20px;">
                            <div class="form-group">
                                <label for="txtEmail">Enter Email Id</label>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" style="background-color: #007bff; display:inline-block; color: white; border: none;">@</span>
                                    </div>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter your email" style="border-radius: 0; border-left: none;"></asp:TextBox>
                                </div>
                            </div>
                            <asp:Label ID="lblmsg" runat="server" Text="" CssClass="text-danger" style="display: block; margin-top: 10px;"></asp:Label>                            <div class="mt-3">
                                <asp:Button ID="Submitbtn" runat="server" Text="Submit" CssClass="btn btn-success btn-block" style="background-color: #28a745; border: none;" OnClick="Submitbtn_Click" />                 </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </form>
        </div>
        
        <!-- Bootstrap JS and dependencies -->
        <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
   
</body>
</html>
