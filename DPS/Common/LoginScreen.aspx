<%@ Page Title="" Language="C#" MasterPageFile="~/Common/OutlineMaster.Master" AutoEventWireup="true" CodeBehind="LoginScreen.aspx.cs" Inherits="DPS.Common.LoginScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auth-form-light text-left py-5 px-4 px-sm-5" style="border-radius: 10px 10px">
        <div class="brand-logo text-center">
            <img src="../Images/Logo/logo.png" alt="logo"><br />
            <br />
            <%--<h4><b></b></h4>--%>
        </div>

        <h6 class="font-weight-light">Sign in to continue.</h6>
        <div class="form-group">
            <asp:TextBox class="form-control form-control-lg" ID="exampleInputEmail1" runat="server" placeholder="Username"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Provide UserName" ControlToValidate="exampleInputEmail1" ForeColor="Red" ValidationGroup="login" ToolTip="Provide UserName">*</asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:TextBox class="form-control form-control-lg" ID="exampleInputPassword1" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Provide Password" ControlToValidate="exampleInputPassword1" ForeColor="Red" ValidationGroup="login" ToolTip="Provide Password">*</asp:RequiredFieldValidator>
        </div>
        <div class="mt-3">
            <%--<a  class="btn btn-block btn-aiut btn-lg font-weight-medium auth-form-btn"  href="../../index.html">SIGN IN</a>--%>
            <asp:Button ID="Button1" class="btn btn-block btn-aiut btn-lg font-weight-medium auth-form-btn" runat="server" ValidationGroup="login" Text="SIGN IN" />
        </div>
        <div class="my-2 d-flex justify-content-between align-items-center">
            <a href="ForgotPassword.aspx" class="auth-link text-black">Forgot password?</a>
        </div>
    </div>
</asp:Content>
