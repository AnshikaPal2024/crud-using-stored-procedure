<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExceIimport.aspx.cs" Inherits="MyPractice.ExceIimport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Excel Import</title>
</head>
<body>
    <form id="form1" runat="server">
       <%-- <div>
            <asp:FileUpload ID="fileupload1" runat="server" Accept=".xls, .xlsx" />
            <asp:Button ID="btnupload" runat="server" Text="Import File" OnClick="btnupload_Click" />
            <asp:Label ID="lblError" runat="server" ForeColor="Red" />
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>--%>
        <div>
            <asp:FileUpload ID="fileupload" runat="server" AccessKey=".xls,.xlsx" />
            <asp:Button ID="uploadbtn" runat="server" Text="Impoet File" OnClick="uploadbtn_Click" />

        </div>
    </form>
</body>
</html>
