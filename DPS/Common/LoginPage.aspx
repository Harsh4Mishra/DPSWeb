<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="DPS.Common.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
    <link rel="stylesheet" href="style.css">
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <img src="./assets/Fav_icon.png" alt="" style="margin-left: 10%;">
            <h2 class="text-center">Login</h2>

            <div class="input-box">
                <asp:TextBox class="form-control" ID="txtexampleInputEmail1" runat="server" placeholder="Username"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Provide UserName" ControlToValidate="txtexampleInputEmail1" ForeColor="Red" ValidationGroup="login" ToolTip="Provide UserName">*</asp:RequiredFieldValidator>
                <i class='bx bxs-user'></i>
            </div>

            <div class="input-box">
                <%--  <asp:TextBox class="form-control" ID="txtexampleInputPassword1" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Provide Password" ControlToValidate="txtexampleInputPassword1" ForeColor="Red" ValidationGroup="login" ToolTip="Provide Password">*</asp:RequiredFieldValidator>
                <i class='bx bxs-lock-alt'></i>--%>
                <asp:TextBox class="form-control" ID="txtexampleInputPassword1" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Provide Password" ControlToValidate="txtexampleInputPassword1" ForeColor="Red" ValidationGroup="login" ToolTip="Provide Password">*</asp:RequiredFieldValidator>

                <!-- Password Toggle Icon -->
                <span id="togglePassword" style="position: absolute; right: 0px; top: 25px; cursor: pointer;">
                    <i class='bx bxs-lock-alt'></i>
                    <!-- You can replace this with any icon of your choice -->
                </span>
            </div>

            <div class="remember-forget">
                <%--<asp:CheckBox ID="CheckBox1" runat="server" Text="Remember me" />--%>
                <a href="ForgotPassword.aspx" class="auth-link text-black">Forgot password?</a>
            </div>

            <asp:Button ID="Button1" class="btn" runat="server" ValidationGroup="login" Text="Login" OnClick="Button1_Click" />


        </div>
        <!-- Add JavaScript to toggle password visibility -->
        <script type="text/javascript">
    document.getElementById("togglePassword").addEventListener("click", function () {
        var passwordField = document.getElementById("<%= txtexampleInputPassword1.ClientID %>");
        var icon = this.querySelector("i");

        if (passwordField.type === "password") {
            passwordField.type = "text"; // Change to text to show password
            icon.classList.remove('bxs-lock-alt'); // Replace the lock icon
            icon.classList.add('bxs-lock-open'); // Show unlocked icon
        } else {
            passwordField.type = "password"; // Hide the password
            icon.classList.remove('bxs-lock-open'); // Replace the open lock icon
            icon.classList.add('bxs-lock-alt'); // Show the locked icon
        }
    });
</script>
    </form>
</body>
</html>
