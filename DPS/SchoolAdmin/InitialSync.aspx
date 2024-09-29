<%@ Page Title="" Language="C#" MasterPageFile="~/SchoolAdmin/SchoolMaster.Master" AutoEventWireup="true" CodeBehind="InitialSync.aspx.cs" Inherits="DPS.SchoolAdmin.InitialSync" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
    <div class="form-wrapper">
        <div class="row">
            <div class="col-md-12">
                <h4><b>Add School</b></h4>
                <br />
            </div>
            <!-- Logo Field -->
            <div class="col-md-4">
                <label>Select Database <sub>(Select .mdb file only.)</sub></label><span style="color: red">*</span><br />
                <div class="form-group">
                    <asp:FileUpload ID="fuDatabase" CssClass="form-control" ValidationGroup="Employee" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorLogo" runat="server" ErrorMessage="Provide Database file. " ControlToValidate="fuDatabase" ForeColor="Red" ValidationGroup="Employee" ToolTip="Provide Database file"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-flex">
                        &nbsp;&nbsp;&nbsp;
                    </div>
                    <div class="col-md-flex">
                        <asp:Button ID="btnsave" Style="border-radius: 5px" class="btn btn-block btn-save font-weight-medium auth-form-btn" runat="server" ValidationGroup="Employee" Text="Save" OnClick="btnsave_Click" />
                    </div>
                    <div class="col-md-flex">
                        &nbsp;&nbsp;&nbsp;
                    </div>
                    <div class="col-md-flex">
                        <asp:Button ID="btnCancel" Style="border-radius: 5px" class="btn btn-block btn-cancel font-weight-medium auth-form-btn" runat="server" Text="Back" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
