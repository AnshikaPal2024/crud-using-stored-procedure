<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userpage.aspx.cs" Inherits="MyPractice.userpage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Page</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <!-- Bootstrap Bundle JS (includes Popper.js) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <script>
        function launchModal() {
            var myModal = new bootstrap.Modal(document.getElementById('mymodel'));
            myModal.show();
        }

        function launchModel() {
            var ModelChangePassword = new bootstrap.Modal(document.getElementById('ModelChangePassword'));
            ModelChangePassword.show();
        }
    </script>
</head>
<body style="font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f8f9fa;">
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-dark fixed-top" style="background-color: #004B49; padding: 1rem;">
            <div class="container d-flex justify-content-center">
                <h1 style="color: white; margin: 0; font-size: 1.75rem;">User Data</h1>
            </div>
        </nav>


        <div class="container text-center" style="margin-top: 80px;">
            <asp:Button ID="Logout" runat="server" Text="Logout" OnClick="Logout_Click" CssClass="btn btn-crimson"  Style="margin-right: 10px; color:white; background-color:forestgreen;" />
            <asp:Button ID="btnpdf" runat="server" Text="Export To Pdf" OnClick="btnpdf_Click" CssClass="btn btn-crimson" Style="background-color: forestgreen; color: white; border-radius: 5px; padding: 6px; margin-left: 10px;" />

            <div class="table-responsive mt-4">
                <asp:GridView ID="gvdata" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered"
                    BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Id" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Password" HeaderText="Password" />
                        <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                        <asp:BoundField DataField="Gender" HeaderText="Gender" />
                        <asp:BoundField DataField="Usertype" HeaderText="UserType" />
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                </asp:GridView>
            </div>

            <div class="mb-4 text-center">
                <asp:Button ID="EditButton" runat="server" Text="Edit" OnClick="EditButton_Click" CssClass="btn btn-primary" Style="margin-right: 10px;" />
                <asp:Button ID="ChangePassword" runat="server" Text="Change Password" OnClick="ChangePassword_Click" CssClass="btn btn-warning" Style="margin-right: 10px;" />
            </div>
        </div>

        <!-- Modal edit -->
        <div class="modal fade" id="mymodel" tabindex="-1" aria-labelledby="modelTitle" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #007bff; color: #ffffff;">
                        <h5 class="modal-title" id="modelTitle">Edit the Details</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="Name" runat="server" Text="Name"></asp:Label>
                        <asp:TextBox ID="txtname1" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                        <asp:Label ID="Mobile" runat="server" Text="Mobile"></asp:Label>
                        <asp:TextBox ID="txtmobile2" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                        <asp:Label ID="Email" runat="server" Text="Email"></asp:Label>
                        <asp:TextBox ID="txtemail3" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                        <asp:Label ID="Gender" runat="server" Text="Gender"></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-select mb-2">
                            <asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="modal-footer" style="background-color: #f1f1f1;">
                        <asp:Button ID="SaveButton" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="SaveButton_Click1" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Change Password Modal -->
        <div class="modal fade" id="ModelChangePassword" tabindex="-1" aria-labelledby="modalTitle" aria-hidden="true" data-bs-backdrop="static">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #007bff; color: #ffffff;">
                        <h5 class="modal-title" id="modalTitle">Change the User Password</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="txtid" runat="server" Text="Id"></asp:Label>
                        <asp:TextBox ID="txtid1" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                        <asp:Label ID="txtold" runat="server" Text="Old Password"></asp:Label>
                        <asp:TextBox ID="txtoldpassword" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                        <asp:Label ID="txtpassword" runat="server" Text="New Password"></asp:Label>
                        <asp:TextBox ID="txtpassword2" runat="server" TextMode="Password" CssClass="form-control mb-2"></asp:TextBox>
                    </div>
                    <div class="modal-footer" style="background-color: #f1f1f1;">
                        <asp:Button ID="PasswordButton2" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="PasswordButton2_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" Visible="False"></asp:Label>
    </form>
</body>
</html>
