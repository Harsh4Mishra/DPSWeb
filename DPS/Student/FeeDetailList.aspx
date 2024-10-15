<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FeeDetailList.aspx.cs" Inherits="DPS.Student.FeeDetailList" %>

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

</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12" style="display: flex; justify-content: center; align-items: center; text-align: center;">
                    <table style="margin: auto;">
                        <tr>
                            <td>
                                <asp:Image ID="Image1" runat="server" Height="100px" Width="100px" /></td>
                            <td>
                                <asp:Label ID="lblName" runat="server" Font-Bold="true"></asp:Label><br />
                                <asp:Label ID="lbladdress" runat="server" Font-Bold="false"></asp:Label><br />
                                <i class="fa fa-envelope"></i>:
                                <asp:Label ID="lblEmailID" runat="server" Font-Bold="false"></asp:Label><br />
                                <i class="fa fa-phone"></i>:
                                <asp:Label ID="lblContact" runat="server" Font-Bold="false"></asp:Label>


                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <label>Scholar Number</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtScholarNo" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="Employee" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Provide Scholar Number" ControlToValidate="txtScholarNo" ForeColor="Red" ValidationGroup="Employee" ToolTip="Provide Scholar Number"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-flex">
                    &nbsp;&nbsp;&nbsp;
                </div>
                <div class="col-md-flex">
                    <asp:Button ID="btnsave" Style="border-radius: 5px" class="btn btn-block btn-save font-weight-medium auth-form-btn" runat="server" ValidationGroup="Employee" Text="Search" OnClick="btnsave_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3"></div>
                <div class="col-sm-6">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" Width="100%" BackColor="White" BorderColor="#dbdade"
                             BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false"
                             EmptyDataText="No data found." ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:TemplateField HeaderText="Select Fees">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsActive" runat="server"  Enabled="true" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="33%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Fee Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdDatabase" runat="server" Text='<%# Eval("FeeMonth") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="33%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="TotalFee">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("TotalFee") %>' />
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
                <div class="col-sm-3"></div>
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
