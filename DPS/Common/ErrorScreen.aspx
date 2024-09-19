<%@ Page Title="" Language="C#" MasterPageFile="~/Common/OutlineMaster.Master" AutoEventWireup="true" CodeBehind="ErrorScreen.aspx.cs" Inherits="DPS.Common.ErrorScreen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auth-form-light text-left py-5 px-4 px-sm-5" style="border-radius: 10px 10px">
    <div class="brand-logo text-center">
        <img src="../Images/Logo/logo.png" alt="logo"><br />
        <br />
        <%--<h4><b>Business Automation System</b></h4>--%>
    </div>
    <center>
    <h1 class="font-weight-light"> <font color="red">Error !</font> </h1><br />
    <p>An unexpected error occurred. Please try again later.</p>
    </center>
</div>
</asp:Content>
