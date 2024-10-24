<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adminpage.aspx.cs" Inherits="MyPractice.Adminpage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Application</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            padding-top: 56px;
        }

        .navbar-custom {
            background-color:#004B49; /*this color is deep jungle green*/
            border-color: white;
            top: 0;
            width: 100%;
            z-index: 1000;
            position: fixed;
            height: 70px;
            padding-bottom:20px;
        }

        .btn-custom {
            background-color: #004B49; /* Change button color */
            color: white;
        }

            .btn-custom:hover {
                background-color: #004B49; /* Change hover color */
                color: white;
            }

        .table-responsive {
            border: 1px solid #dee2e6;
            border-radius: 0.25rem;
            background-color: white;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            margin-top:40px;
            margin-bottom:20px;
        }

        .footer-button {
            text-align: center;
            margin-top: 5px;
        }

        .header-title {
            color:white; /* Change header title color */
            margin-top: 20px;
        }

        .logout-button {
            float: right;
        }

        #navbar_btn {
            text-align: left;
            margin-top:20px; 
            margin-bottom:10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-custom">
            <div class="container-fluid">
                <div class="container" style="text-align: center">
            <h1 class="header-title">Admin Data</h1>
        </div>
                </div>
                <asp:Button ID="LogoutButton" runat="server" CssClass="btn btn-primary" Text="Logout" OnClick="LogoutButton_Click"
                    Style="background-color:#E25825; border-radius: 5px; font-size: 16px;" />
           
            <!-- Closing container-fluid -->
        </nav>

     

        <%--   <div class="form-group">
            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control-file" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload Files" OnClick="btnUpload_Click" CssClass="btn btn-primary" />
        </div>--%>

        <div class="table-responsive">
            <asp:GridView ID="gvExcelData" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-striped" />
            <asp:GridView ID="gvdata" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                OnRowCancelingEdit="gvdata_RowCancelingEdit" OnRowDataBound="gvdata_RowDataBound"
                OnRowDeleting="gvdata_RowDeleting" OnRowEditing="gvdata_RowEditing"
                OnRowUpdating="gvdata_RowUpdating" ShowFooter="True" CellPadding="4"  CssClass="table table-striped" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" GridLines="Horizontal">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-HorizontalAlign="Center" ReadOnly="True">
                        <ItemStyle CssClass="text-center"></ItemStyle>
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtname" runat="server" Text='<%# Bind("Name") %>' class="form-control" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="txtName" runat="server" Text='<%# Bind("Name") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="Name1" runat="server" class="form-control"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Email">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtemail" runat="server" Text='<%# Bind("Email") %>' class="form-control" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="txtEmail" runat="server" Text='<%# Bind("Email") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="Email2" runat="server" class="form-control"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Password">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtpassword" runat="server" Text='<%# Bind("Password") %>' class="form-control" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="txtPassword" runat="server" Text='<%# Bind("Password") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="Password3" runat="server" class="form-control"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Mobile">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtmobile" runat="server" Text='<%# Bind("Mobile") %>' class="form-control" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="txtMobile" runat="server" Text='<%# Bind("Mobile") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="Mobile4" runat="server" class="form-control"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gender">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-select">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelGender" runat="server" Text='<%# Bind("Gender") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-select" Width="100px">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="UserType">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-select">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Admin</asp:ListItem>
                                <asp:ListItem>User</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelUserType" runat="server" Text='<%# Bind("UserType") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-select" Width="100px">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Admin</asp:ListItem>
                                <asp:ListItem>User</asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-primary" BackColor="#004B49" ForeColor="White" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" CssClass="btn btn-primary" BackColor="#004B49" ForeColor="White" />

                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-primary" BackColor="#004B49" ForeColor="White" />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-primary" BackColor="#004B49" ForeColor="White" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Import">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlImport" runat="server"
                                NavigateUrl='<%# Eval("pdf") %>'
                                Text="Import file"
                                Target="_blank"
                                CssClass="text-primary" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:FileUpload ID="importFile" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>




                    <asp:TemplateField HeaderText="Actions">
                        <FooterTemplate>
                            <div class="footer-button">
                                <asp:Button ID="InsertButton" runat="server" Text="Insert" OnClick="InsertButton_Click" CommandName="Save" CssClass="btn btn-danger" />
                            </div>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Images">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" />
                        <EditItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl(Eval("image")) %>' Width="100px" Height="100px" />
                            <asp:FileUpload ID="FileUpload2" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl(Eval("image")) %>' Width="100px" Height="100px" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>

                <FooterStyle BackColor="White" ForeColor="#333333" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#487575" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#275353" />
            </asp:GridView>
        </div>
        
                     <div class="d-flex"  id="navbar_btn">
                    <asp:Button ID="Buttonpdf" runat="server" Text="Export To PDF" OnClick="Buttonpdf_Click"
                        CssClass="btn btn-custom" Style="margin-right: 10px;" Font-Bold="True" />

                    <asp:Button ID="btnexcel" runat="server" Text="Export To Excel" OnClick="btnexcel_Click"
                        CssClass="btn btn-custom" Style="margin-right: 10px;" Font-Bold="true" />

                    <asp:Button ID="btnadmin" runat="server" Text="Export Admin Data" OnClick="ExportAdminDetails_Click"
                        CssClass="btn btn-custom" Style="margin-right: 10px;" Font-Bold="True" />
             </div>

        <!-- Bootstrap JS Bundle with Popper -->
        <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.bundle.min.js"></script>
    </form>

</body>
</html>
