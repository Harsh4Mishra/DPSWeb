<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/SuperAdminMaster.Master" AutoEventWireup="true" CodeBehind="AddPaymentConfiguration.aspx.cs" Inherits="DPS.SuperAdmin.AddPaymentConfiguration" Async="true" %>

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
                    <h4><b>Add School Payment Configuration</b></h4>
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
                    <label>Merchant Code</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtName" CssClass="form-control" ReadOnly="true" AutoCompleteType="Disabled" ValidationGroup="Employee" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Provide Merchant Code" ControlToValidate="txtName" ForeColor="Red" ValidationGroup="Employee" ToolTip="Provide Merchant Code"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>Merchant ID</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtMerchantId" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="PaymentConfig" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMerchantId" runat="server" ErrorMessage="Provide Merchant ID" ControlToValidate="txtMerchantId" ForeColor="Red" ValidationGroup="PaymentConfig" ToolTip="Provide Merchant ID"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-md-4">
                    <label>User ID</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtUserId" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="PaymentConfig" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserId" runat="server" ErrorMessage="Provide User ID" ControlToValidate="txtUserId" ForeColor="Red" ValidationGroup="PaymentConfig" ToolTip="Provide User ID"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-md-4">
                    <label>Merchant Password</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtMerchantPassword" CssClass="form-control" TextMode="Password" AutoCompleteType="Disabled" ValidationGroup="PaymentConfig" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMerchantPassword" runat="server" ErrorMessage="Provide Merchant Password" ControlToValidate="txtMerchantPassword" ForeColor="Red" ValidationGroup="PaymentConfig" ToolTip="Provide Merchant Password"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-md-4">
                    <label>Product ID</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtProductId" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="PaymentConfig" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorProductId" runat="server" ErrorMessage="Provide Product ID" ControlToValidate="txtProductId" ForeColor="Red" ValidationGroup="PaymentConfig" ToolTip="Provide Product ID"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-md-4">
                    <label>Transaction Currency</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtTransactionCurrency" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="PaymentConfig" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTransactionCurrency" runat="server" ErrorMessage="Provide Transaction Currency" ControlToValidate="txtTransactionCurrency" ForeColor="Red" ValidationGroup="PaymentConfig" ToolTip="Provide Transaction Currency"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-md-4">
                    <label>Request AES Key</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtRequestAesKey" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="PaymentConfig" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRequestAesKey" runat="server" ErrorMessage="Provide Request AES Key" ControlToValidate="txtRequestAesKey" ForeColor="Red" ValidationGroup="PaymentConfig" ToolTip="Provide Request AES Key"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-md-4">
                    <label>Request Hash Key</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtRequestHashKey" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="PaymentConfig" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRequestHashKey" runat="server" ErrorMessage="Provide Request Hash Key" ControlToValidate="txtRequestHashKey" ForeColor="Red" ValidationGroup="PaymentConfig" ToolTip="Provide Request Hash Key"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-md-4">
                    <label>Response AES Key</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtResponseAesKey" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="PaymentConfig" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorResponseAesKey" runat="server" ErrorMessage="Provide Response AES Key" ControlToValidate="txtResponseAesKey" ForeColor="Red" ValidationGroup="PaymentConfig" ToolTip="Provide Response AES Key"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-md-4">
                    <label>Response Hash Key</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtResponseHashKey" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="PaymentConfig" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorResponseHashKey" runat="server" ErrorMessage="Provide Response Hash Key" ControlToValidate="txtResponseHashKey" ForeColor="Red" ValidationGroup="PaymentConfig" ToolTip="Provide Response Hash Key"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-md-4">
                    <label>Hash Algorithm</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtHashAlgorithm" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="PaymentConfig" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorHashAlgorithm" runat="server" ErrorMessage="Provide Hash Algorithm" ControlToValidate="txtHashAlgorithm" ForeColor="Red" ValidationGroup="PaymentConfig" ToolTip="Provide Hash Algorithm"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-md-4">
                    <label>Customer Account Number</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:TextBox ID="txtCustomerAccountNumber" CssClass="form-control" AutoCompleteType="Disabled" ValidationGroup="PaymentConfig" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCustomerAccountNumber" runat="server" ErrorMessage="Provide Customer Account Number" ControlToValidate="txtCustomerAccountNumber" ForeColor="Red" ValidationGroup="PaymentConfig" ToolTip="Provide Customer Account Number"></asp:RequiredFieldValidator>
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
