<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Uploadfile.aspx.cs" Inherits="MyPractice.Uploadfile" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Excel File Upload</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="container">
            <h2 class="mt-3">Import Excel Data to GridView</h2>
            <div class="form-group">
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control-file" />
            </div>
            <div class="form-group">
                <asp:Button ID="btnUpload" runat="server" Text="upload  files" OnClick="btnUpload_Click" CssClass="btn btn-primary" />
            </div>
            <asp:GridView ID="gvExcelData" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-striped" />
        </div>
    </form>
</body>
</html>