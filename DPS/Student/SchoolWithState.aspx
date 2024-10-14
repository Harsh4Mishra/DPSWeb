<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchoolWithState.aspx.cs" Inherits="DPS.Student.SchoolWithState" %>

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
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-scroller">
            <div class="container-fluid page-body-wrapper full-page-wrapper">
                <div class="aiut-wrapper d-flex align-items-center auth px-0">
                    <div class="row w-100 mx-0">
                        <div class="col-lg-4 mx-auto">
                            <div class="auth-form-light text-left py-5 px-4 px-sm-5" style="border-radius: 10px 10px">
                                <div class="brand-logo text-center">
                                    <img src="../Images/Logo/logo.png" alt="logo"><br />
                                    <br />
                                    <%--<h4><b></b></h4>--%>
                                </div>

                                <h6 class="font-weight-light">Sign in to continue.</h6>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlState" runat="server" ValidationGroup="Employee" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" ></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select state." ControlToValidate="ddlState" ForeColor="Red" ValidationGroup="Employee" InitialValue="0" ToolTip="Please select a state."></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlSchool" runat="server" ValidationGroup="Employee" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select state." ControlToValidate="ddlSchool" ForeColor="Red" ValidationGroup="Employee" InitialValue="0" ToolTip="Please select a state."></asp:RequiredFieldValidator>
                                </div>
                                <div class="mt-3">
                                    <%--<a  class="btn btn-block btn-aiut btn-lg font-weight-medium auth-form-btn"  href="../../index.html">SIGN IN</a>--%>
                                    <asp:Button ID="Button1" class="btn btn-block btn-aiut btn-lg font-weight-medium auth-form-btn" runat="server" ValidationGroup="Employee" Text="SIGN IN" OnClick="Button1_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- content-wrapper ends -->
            </div>
            <!-- page-body-wrapper ends -->
        </div>
        <!-- container-scroller -->
        <!-- plugins:js -->
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
        <!-- endinject -->
    </form>
</body>
</html>
