<%@ Page Title="" Language="C#" MasterPageFile="~/Common/OutlineMaster.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="DPS.Common.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auth-form-light text-left py-5 px-4 px-sm-5" style="border-radius: 10px 10px">
    <div class="brand-logo text-center">
        <img src="../Images/Logo/logo.png" alt="logo"><br />
        <br />
        <%--<h4><b>Business Automation System</b></h4>--%>
    </div>

    <h6 class="font-weight-light">Provide Username.</h6>
    <div class="form-group">
        <asp:TextBox class="form-control form-control-lg" ID="txtexampleInputEmail1" runat="server" placeholder="Username"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Provide UserName" ControlToValidate="txtexampleInputEmail1" ForeColor="Red" ValidationGroup="login" ToolTip="Provide UserName">*</asp:RequiredFieldValidator>
    </div>
    <div class="mt-3">
        <%--<a  class="btn btn-block btn-aiut btn-lg font-weight-medium auth-form-btn"  href="../../index.html">SIGN IN</a>--%>
        <asp:Button ID="Button1" class="btn btn-block btn-aiut btn-lg font-weight-medium auth-form-btn" runat="server" ValidationGroup="login" Text="PROCEED" OnClick="Button1_Click" />
    </div>
    <div class="my-2 d-flex justify-content-between align-items-center">
        <a href="LoginPage.aspx" class="auth-link text-black">Sign In</a>
    </div>
</div>
</asp:Content>
