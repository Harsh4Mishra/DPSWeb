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
    <script src="https://pgtest.atomtech.in/staticdata/ots/js/atomcheckout.js" type="text/javascript"></script>
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
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12 justify-content-center text-center align-center">
                    <br />
                    <h3>Payment Details</h3>
                    <br />
                </div>
                <div class="col-sm-6">
                    <div class="row">
                        <div id="personalDetail" runat="server" visible="false">
                        <div class="section-header col-sm-12">Personal Details</div>
                        <div class="col-sm-6">
                            <label>Scholar No:</label>
                            <asp:TextBox ID="txtScholarNo" runat="server" CssClass=" form-control readonly-field" ReadOnly="true" />
                        </div>
                        <div class="col-sm-6">
                            <label>Student Name:</label>
                            <asp:TextBox ID="txtStudentName" runat="server" CssClass="form-control readonly-field" ReadOnly="true" />
                        </div>
                        <div class="col-sm-6">
                            <label>Date of Birth:</label>
                            <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control readonly-field" ReadOnly="true" />
                        </div>
                        <div class="col-sm-6">
                            <label>Gender:</label>
                            <asp:TextBox ID="txtSex" runat="server" CssClass="form-control readonly-field" ReadOnly="true" />
                        </div>
                        <div class="col-sm-6">
                            <label>Father's Name:</label>
                            <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control readonly-field" ReadOnly="true" />
                        </div>
                        <div class="col-sm-6">
                            <label>Father's Phone:</label>
                            <asp:TextBox ID="txtFatherPhone" runat="server" CssClass="form-control readonly-field" ReadOnly="true" />
                        </div>
                        <div class="col-sm-6">
                            <label>Class:</label>
                            <asp:TextBox ID="txtClass" runat="server" CssClass="form-control readonly-field" ReadOnly="true" />
                        </div>
                        <div class="col-sm-6">
                            <label>Section:</label>
                            <asp:TextBox ID="txtSection" runat="server" CssClass="form-control readonly-field" ReadOnly="true" />
                        </div>
                            </div>
                        <div class="section-header col-sm-12">Fee Amount</div>
                        <div class="col-sm-6">
                            <label>Total Fee Amount:</label>
                            <asp:TextBox ID="txtFeeAmount" runat="server" CssClass="form-control readonly-field" ReadOnly="true" />
                        </div>
                        <div class="col-sm-6">
                            <label>Fine Amount:</label>
                            <asp:TextBox ID="txtFineAmount" runat="server" CssClass="form-control readonly-field" ReadOnly="true" />
                        </div>
                        <div class="col-sm-6">
                            <label>Final Amount:</label>
                            <asp:TextBox ID="txtfinalAmount" runat="server" CssClass="form-control readonly-field" ReadOnly="true" />
                        </div>
                        <div class="col-sm-6"><br />
                            <asp:Button ID="Button1" Style="border-radius: 5px" class="btn btn-block btn-save font-weight-medium auth-form-btn" runat="server" Text="Pay" OnClick="Button1_Click" />
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
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
            </div>



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
