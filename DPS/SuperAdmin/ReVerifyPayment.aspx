<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/SuperAdminMaster.Master" AutoEventWireup="true" CodeBehind="ReVerifyPayment.aspx.cs" Inherits="DPS.SuperAdmin.ReVerifyPayment" %>

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
                    <h4><b>Re-Verify Payment</b></h4>
                    <br />
                </div>

                <div class="col-md-4">
                    <label>Select School</label><span style="color: red">*</span><br />
                    <div class="form-group">
                        <asp:DropDownList ID="ddlSchool" runat="server" ValidationGroup="Employee" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged"></asp:DropDownList>
                        
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" Width="100%" BackColor="White" BorderColor="#dbdade"
                         BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false"
                             EmptyDataText="No data found." ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Scholar No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdDatabase" runat="server" Text='<%# Eval("ScholarNumber") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="16%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Student Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("StudentName") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="16%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="16%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Transaction ID" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransactionID" runat="server" Text='<%# Eval("TransactionID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="16%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Transaction Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("AtomID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="16%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Is Verified">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsReverified" runat="server" Text='<%# Eval("IsReverified") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="16%" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#028dce" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="45px" />

                            <PagerStyle HorizontalAlign="left" CssClass="paging" />
                            <RowStyle ForeColor="black" Font-Size="small" Height="45px" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-flex">
                            &nbsp;&nbsp;&nbsp;
                        </div>
                        <div class="col-md-flex">
                            <asp:Button ID="btnsave" Style="border-radius: 5px" class="btn btn-block btn-save font-weight-medium auth-form-btn" runat="server" Text="Re-Verify" OnClick="btnsave_Click" />
                        </div>
                        <div class="col-md-flex">
                            &nbsp;&nbsp;&nbsp;
                        </div>
                        <div class="col-md-flex">
                           <%-- <asp:Button ID="btnCancel" Style="border-radius: 5px" class="btn btn-block btn-cancel font-weight-medium auth-form-btn" runat="server" Text="Back" OnClick="btnCancel_Click" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
