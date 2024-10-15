<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentPage.aspx.cs" Inherits="DPS.Student.PaymentPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>DPS</title>
    <!-- plugins:css -->

    <link rel="stylesheet" href="../StyleSheet/vendors/feather/feather.css">
    <link rel="stylesheet" href="../StyleSheet/vendors/ti-icons/css/themify-icons.css">
    <link rel="stylesheet" href="../StyleSheet/vendors/css/vendor.bundle.base.css">
    <!-- endinject -->
    <!-- Plugin css for this page -->
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <link href="../StyleSheet/css/vertical-layout-light/style.css" rel="stylesheet" />
    <!-- endinject -->
    <link rel="shortcut icon" type="image/x-icon" href="../Images/Icon/icon.png">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <style>
        .readonly-field {
            border: none;
            background-color: #f9f9f9;
            width: 300px;
            padding: 5px;
        }

        .section-header {
            font-weight: bold;
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="section-header">Personal Details</div>
            <div>
                <label>Scholar No:</label>
                <asp:TextBox ID="txtScholarNo" runat="server" CssClass="readonly-field" ReadOnly="true" />
            </div>
            <div>
                <label>Student Name:</label>
                <asp:TextBox ID="txtStudentName" runat="server" CssClass="readonly-field" ReadOnly="true" />
            </div>
            <div>
                <label>Date of Birth:</label>
                <asp:TextBox ID="txtDOB" runat="server" CssClass="readonly-field" ReadOnly="true" />
            </div>
            <div>
                <label>Sex:</label>
                <asp:TextBox ID="txtSex" runat="server" CssClass="readonly-field" ReadOnly="true" />
            </div>
            <div>
                <label>Father's Name:</label>
                <asp:TextBox ID="txtFatherName" runat="server" CssClass="readonly-field" ReadOnly="true" />
            </div>
            <div>
                <label>Father's Phone:</label>
                <asp:TextBox ID="txtFatherPhone" runat="server" CssClass="readonly-field" ReadOnly="true" />
            </div>
            <div>
                <label>Class:</label>
                <asp:TextBox ID="txtClass" runat="server" CssClass="readonly-field" ReadOnly="true" />
            </div>
            <div>
                <label>Section:</label>
                <asp:TextBox ID="txtSection" runat="server" CssClass="readonly-field" ReadOnly="true" />
            </div>

            <div class="section-header">Fees Details</div>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" Width="100%" BackColor="White" BorderColor="#dbdade"
                BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false"
                EmptyDataText="No data found." ShowHeaderWhenEmpty="true">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblFeeName" runat="server" Text='<%# Eval("FeeName") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="33%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                            <asp:Label ID="lblFeeType" runat="server" Text='<%# Eval("FeeType") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="33%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("FeeAmount") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="33%" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#028dce" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="45px" />

                <PagerStyle HorizontalAlign="left" CssClass="paging" />
                <RowStyle ForeColor="black" Font-Size="small" Height="45px" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
        </div>
        <script src="../StyleSheet/vendors/js/vendor.bundle.base.js"></script>
        <!-- endinject -->
        <!-- Plugin js for this page -->
        <!-- End plugin js for this page -->
        <!-- inject:js -->
        <script src="../StyleSheet/js/off-canvas.js"></script>
        <script src="../StyleSheet/js/hoverable-collapse.js"></script>
        <script src="../StyleSheet/js/template.js"></script>
        <script src="../StyleSheet/js/settings.js"></script>
        <script src="../StyleSheet/js/todolist.js"></script>
    </form>
</body>
</html>
