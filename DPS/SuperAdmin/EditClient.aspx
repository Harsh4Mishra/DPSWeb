<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/SuperAdminMaster.Master" AutoEventWireup="true" CodeBehind="EditClient.aspx.cs" Inherits="DPS.SuperAdmin.EditClient" Async="true" %>

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
                    <h4><b>Modify School</b></h4>
                    <br />
                </div>
                <div class="col-md-4">
                    <label>Name</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtName" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="Employee" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Provide School Name" ControlToValidate="txtName" ForeColor="Red" ValidationGroup="Employee" ToolTip="Provide School Name"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>Address</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtAddress" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="Employee" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Provide School Address" ControlToValidate="txtAddress" ForeColor="Red" ValidationGroup="Employee" ToolTip="Provide School Address"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>City</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtCity" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="Employee" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Provide School City" ControlToValidate="txtCity" ForeColor="Red" ValidationGroup="Employee" ToolTip="Provide School City"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>State</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:DropDownList ID="ddlState" runat="server" ValidationGroup="Employee" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select state." ControlToValidate="ddlState" ForeColor="Red" ValidationGroup="Employee" InitialValue="0" ToolTip="Please select a state."></asp:RequiredFieldValidator>
                    </div>
                </div>
                <!-- Country Field -->
                <div class="col-md-4">
                    <label>Country</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtCountry" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="Employee" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCountry" runat="server" ErrorMessage="Provide Country" ControlToValidate="txtCountry" ForeColor="Red" ValidationGroup="Employee" ToolTip="Provide Country"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <!-- Phone Number Field -->
                <div class="col-md-4">
                    <label>Phone Number</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="Employee" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhoneNumber" runat="server" ErrorMessage="Provide Phone Number" ControlToValidate="txtPhoneNumber" ForeColor="Red" ValidationGroup="Employee" ToolTip="Provide Phone Number"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <!-- Pincode Field -->
                <div class="col-md-4">
                    <label>Pincode</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtPincode" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="Employee" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPincode" runat="server" ErrorMessage="Provide Pincode" ControlToValidate="txtPincode" ForeColor="Red" ValidationGroup="Employee" ToolTip="Provide Pincode"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <!-- Email ID Field -->
                <div class="col-md-4">
                    <label>Email ID</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtEmailId" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="Employee" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmailId" runat="server" ErrorMessage="Provide Email ID" ControlToValidate="txtEmailId" ForeColor="Red" ValidationGroup="Employee" ToolTip="Provide Email ID"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmailId" runat="server" ErrorMessage="Invalid Email Format" ControlToValidate="txtEmailId" ForeColor="Red" ValidationGroup="Employee"
                            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ToolTip="Enter a valid email address"></asp:RegularExpressionValidator>
                    </div>
                </div>

                <!-- Logo Field -->
                <div class="col-md-4">
                    <label>Logo</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:FileUpload ID="fuLogo" CssClass="form-control" ValidationGroup="Employee" runat="server" />
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorLogo" runat="server" ErrorMessage="Provide Logo" ControlToValidate="fuLogo" ForeColor="Red" ValidationGroup="Employee" ToolTip="Provide Logo"></asp:RequiredFieldValidator>--%><br />
                        <asp:Image ID="Image1" runat="server" Height="100px" Width="100px" />
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
