<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="MyPractice.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        <div>
            <asp:Label ID="txtpass" runat="server" Text="Password:"></asp:Label>
            <asp:TextBox ID="txtpassword" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click1" />
        </div>
        <asp:Label ID="encryptedpassword" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
