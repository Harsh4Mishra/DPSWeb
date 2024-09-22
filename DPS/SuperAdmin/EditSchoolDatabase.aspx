<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/SuperAdminMaster.Master" AutoEventWireup="true" CodeBehind="EditSchoolDatabase.aspx.cs" Inherits="DPS.SuperAdmin.EditSchoolDatabase" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Include DHTMLX CSS -->
<link rel="stylesheet" href="https://cdn.dhtmlx.com/suite/edge/suite.css">
<!-- Include DHTMLX JS -->
<script src="https://cdn.dhtmlx.com/suite/edge/suite.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
    <div class="form-wrapper">
        <div class="row">
            <div class="col-md-12">
                <h4><b>Edit School Database</b></h4>
                <br />
            </div>
            <div class="col-md-4">
                <label>Select School</label><span style="color: red">*</span><br />
                <div class="form-group">
                    <asp:DropDownList ID="ddlSchool" runat="server" ValidationGroup="Employee" CssClass="form-control"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select school." ControlToValidate="ddlSchool" ForeColor="Red" ValidationGroup="Employee" InitialValue="0" ToolTip="Please select a school."></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <label>Database Name</label><span style="color: red">*</span><br />
                <div class="form-group">
                    <asp:TextBox ID="txtName" CssClass="form-control" AutoCompleteType="Disabled" ReadOnly="true" ValidationGroup="Employee" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Provide School Name" ControlToValidate="txtName" ForeColor="Red" ValidationGroup="Employee" ToolTip="Provide School Name"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="col-md-4">
                <label>Active</label><br />
                <div class="form-group">
                    <asp:CheckBox ID="chkActive" runat="server" />
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
