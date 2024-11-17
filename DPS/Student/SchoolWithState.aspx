<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchoolWithState.aspx.cs" Inherits="DPS.Student.SchoolWithState" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>DPS</title>
    <!-- plugins:css -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
    <link rel="stylesheet" href="./login_style.css">
    <!-- inject:css -->
    <link href="../StyleSheet/css/vertical-layout-light/style.css" rel="stylesheet" />
    <!-- endinject -->
    <link rel="shortcut icon" type="image/x-icon" href="../Images/Icon/icon.png">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" width="100%">
            <div class="row d-flex" style="display: flex;width:100%;padding-left:10%;margin:0px 100px">
                <div class="col-md-12 wrapper">
                    <img src="./assets/Fav_icon.png" alt="" style="margin-left: 30%;">
                    <div class="row">
                        <div class="col-md-6">
                            <h4 class="mt-3">How to initiate payment ?</h4>
                            <ul>
                                <li class="mb-1">Step-1: select your state.</li>
                                <li class="mb-1">Step-2: Select your <b>Institite/School</b> </li>
                                <li class="mb-1">Step-3: Click on <b>Proceed to pay...</b> button</li>
                                <li class="mb-1">Step-4: Now select your fee month</li>
                                <li class="mb-1">Step-5: Click to pay</li>
                            </ul>
                        </div>
                        <!-- <div class="col-md-1"></div> -->
                        <div class="col-md-1" style="padding: 20px 0 10px 30px;">
                            <div style="background: black; height: 100%; width: 2px;"></div>
                        </div>
                        <div class="col-md-5">
                            <h2 class="mt-3">Student Portal</h2>

                            <div class="input-box">
                                <asp:DropDownList ID="ddlState" runat="server" ValidationGroup="Employee" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select state." ControlToValidate="ddlState" ForeColor="Red" ValidationGroup="Employee" InitialValue="0" ToolTip="Please select a state."></asp:RequiredFieldValidator>
                                <i class='bx bxs-chevron-down'></i>
                            </div>

                            <div class="input-box">
                                 <asp:DropDownList ID="ddlSchool" runat="server" ValidationGroup="Employee" CssClass="form-control"></asp:DropDownList>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select state." ControlToValidate="ddlSchool" ForeColor="Red" ValidationGroup="Employee" InitialValue="0" ToolTip="Please select a state."></asp:RequiredFieldValidator>
                                <i class='bx bxs-chevron-down'></i>
                            </div>
                            <asp:Button ID="Button1" class="btn" runat="server" ValidationGroup="Employee" Text="Proceed to pay ..." OnClick="Button1_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>





<%--        <div class="container-scroller">
            <div class="container-fluid page-body-wrapper full-page-wrapper">
                <div class="aiut-wrapper d-flex align-items-center auth px-0">
                    <div class="row w-100 mx-0">
                        <div class="col-lg-4 mx-auto">
                            <div class="auth-form-light text-left py-5 px-4 px-sm-5" style="border-radius: 10px 10px">
                                <div class="brand-logo text-center">
                                    <img src="../Images/Logo/logo.png" alt="logo"><br />
                                    <br />
                                </div>

                                <h6 class="font-weight-light">Select State</h6>
                                <div class="form-group">
                                </div>
                                <h6 class="font-weight-light">Select School</h6>
                                <div class="form-group">
                                   
                                </div>
                                <div class="mt-3">
                                  
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="../StyleSheet/vendors/js/vendor.bundle.base.js"></script>
        <script src="../StyleSheet/js/off-canvas.js"></script>
        <script src="../StyleSheet/js/hoverable-collapse.js"></script>
        <script src="../StyleSheet/js/template.js"></script>
        <script src="../StyleSheet/js/settings.js"></script>
        <script src="../StyleSheet/js/todolist.js"></script>--%>
    </form>
</body>
</html>
