﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/SuperAdminMaster.Master" AutoEventWireup="true" CodeBehind="PaymentConfigurationMaster.aspx.cs" Inherits="DPS.SuperAdmin.PaymentConfigurationMaster" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="modal fade" id="mymodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Delete Client</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Are you sure. You want to delete ?
   
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col">
                            <div class="d-flex justify-content-end align-items-center">
                                <div class="col-sm-flex">&nbsp;&nbsp;</div>
                                <div class="col-sm-flex">
                                    <asp:Button ID="btnCloseModal" Style="border-radius: 5px" runat="server" Text="No" CssClass="btn btn-block btn-cancel font-weight-medium auth-form-btn" data-dismiss="modal" />
                                </div>
                                <div class="col-sm-flex">&nbsp;&nbsp;</div>
                                <div class="col-sm-flex">
                                    <asp:Button ID="btnSaveChanges" Style="border-radius: 5px; background-color: red" runat="server" Text="Yes" OnClick="btnsave_Click" CssClass="btn btn-block btn-save font-weight-medium auth-form-btn" />
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="form-wrapper">
            <div class="row" style="background-color: transparent">
                <div class="col-md-12">
                </div>
                <div class="col-md-12 ">
                    <div class="row">
                        <div class="col">
                            <div class="d-flex justify-content-start align-items-center">
                                <div class="col-sm-flex">
                                    <asp:LinkButton ID="LinkButtonAdd" runat="server" class="btn btn-info" Style="border-radius: 5px" OnClick="LinkButtonAdd_Click"><i class="fa fa-plus" style="font-size:medium"></i> &nbsp;School Payment Configuration</asp:LinkButton>
                                    <%--<asp:Button ID="btnAddDesignation" runat="server" class="btn btn-outline-info btn-fw" Text="Add Designation" OnClick="btnAddDesignation_Click" />--%>
                                </div>
                                <div class="col-sm-flex">&nbsp;&nbsp;</div>
                                <div class="col-sm-flex">
                                    <asp:LinkButton ID="LinkButtonPublish" runat="server" class="btn btn-info" Style="border-radius: 5px" OnClick="LinkButtonPublish_Click"><i class="fa fa-pencil" style="font-size:medium"></i> &nbsp;Publish</asp:LinkButton>
                                    <%--<asp:Button ID="btnAddDesignation" runat="server" class="btn btn-outline-info btn-fw" Text="Add Designation" OnClick="btnAddDesignation_Click" />--%>
                                </div>
                                <%--  <div class="col-sm-flex">&nbsp;&nbsp;</div>
<div class="col-sm-flex">
   
    <asp:LinkButton ID="LinkButtonExportExcel" runat="server" class="btn btn-outline-success btn-fw icon-excelcss" Style="border-radius: 5px" OnClick="LinkButtonExportExcel_Click"><i class="fa fa-file-excel-o" style="font-size:medium"></i></asp:LinkButton>
</div>
<div class="col-sm-flex">&nbsp;&nbsp;</div>
<div class="col-sm-flex">
   
    <asp:LinkButton ID="LinkButtonExportpdf" runat="server" class="btn btn-outline-danger btn-fw icon-pdfcss" Style="border-radius: 5px" OnClick="LinkButtonExportpdf_Click"><i class="fa fa-file-pdf-o" style="font-size:medium"></i></asp:LinkButton>
</div>--%>
                            </div>
                        </div>
                        <div class="col">
                            <div class="d-flex justify-content-end align-items-center">
                                <div class="col-sm-flex">&nbsp;&nbsp;</div>
                                <div class="col-sm-flex">
                                    <%--<asp:TextBox ID="txtSearch" CssClass="searchform-control" runat="server" onKeyPress="javascript:ApplySearch();" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtSearch" AutoCompleteType="Disabled" placeholder="Search..." CssClass="form-control" runat="server" onkeyup="filterGrid()" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-md-12" style="padding-top: 10px">
                    <div class="row" style="padding: 5px">
                        <div class="col">
                            <div class="d-flex justify-content-start align-items-center">
                                <span style="font-size: small; color: #aaaaaa">Show </span>&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlentities" Style="font-size: small; color: #aaaaaa" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlentities_SelectedIndexChanged">
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;<span style="font-size: small; color: #aaaaaa"> Rows</span>
                            </div>
                        </div>
                        <div class="col">
                            <div class="d-flex justify-content-end align-items-center">
                                <div class="col-sm-flex">
                                    <%--<asp:Button ID="btnExportToExcel" runat="server" class="normalbtn btn-reverseaiut" Text="Export To Excel" OnClick="btnExportToExcel_Click" />  normalbtn btn-reverseaiut--%>
                                    <asp:LinkButton ID="LinkButtonExportExcel" runat="server" class="" Style="border-radius: 5px" OnClick="LinkButtonExportExcel_Click"><i class="fa fa-file-excel-o" style="font-size:large;color:forestgreen"></i></asp:LinkButton>
                                </div>
                                <div class="col-sm-flex">&nbsp;&nbsp;</div>
                                <div class="col-sm-flex">
                                    <%--<asp:Button ID="btnExportToExcel" runat="server" class="normalbtn btn-reverseaiut" Text="Export To Excel" OnClick="btnExportToExcel_Click" />--%>
                                    <asp:LinkButton ID="LinkButtonExportpdf" runat="server" class="" Style="border-radius: 5px" OnClick="LinkButtonExportpdf_Click"><i class="fa fa-file-pdf-o" style="font-size:large;color:lightcoral"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" Width="100%" BackColor="White" BorderColor="#dbdade"
                            OnSorting="GridView1_Sorting" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false"
                            AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="5" EmptyDataText="No data found." ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" SortExpression="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="School Name" SortExpression="NAME">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMCC_CODEname" runat="server" Text='<%# Eval("NAME") %>' CssClass="wrap-text" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MCC Code" SortExpression="MCC_CODE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMCC_CODE" runat="server" Text='<%# Eval("MCC_CODE") %>' CssClass="wrap-text" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Merchant ID" SortExpression="MERCHANT_ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMERCHANT_ID" runat="server" Text='<%# Eval("MERCHANT_ID") %>' CssClass="wrap-text" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User ID" SortExpression="USER_ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUSER_ID" runat="server" Text='<%# Eval("USER_ID") %>' CssClass="wrap-text" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Password" SortExpression="MERCHANT_PASSWORD">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMERCHANT_PASSWORD" runat="server" Text='<%# Eval("MERCHANT_PASSWORD") %>' CssClass="wrap-text" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Product ID" SortExpression="PRODUCT_ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPRODUCT_ID" runat="server" Text='<%# Eval("PRODUCT_ID") %>' CssClass="wrap-text" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Transaction Currency" SortExpression="TRANSACTION_CURRENCY">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRANSACTION_CURRENCY" runat="server" Text='<%# Eval("TRANSACTION_CURRENCY") %>' CssClass="wrap-text" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Req AES Key" SortExpression="REQUEST_AES_KEY">
                                    <ItemTemplate>
                                        <asp:Label ID="lblREQUEST_AES_KEY" runat="server" Text='<%# Eval("REQUEST_AES_KEY") %>' CssClass="wrap-text" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Req HASH Key" SortExpression="REQUEST_HASH_KEY">
                                    <ItemTemplate>
                                        <asp:Label ID="lblREQUEST_HASH_KEY" runat="server" Text='<%# Eval("REQUEST_HASH_KEY") %>' CssClass="wrap-text" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Res AES Key" SortExpression="RESPONSE_AES_KEY">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRESPONSE_AES_KEY" runat="server" Text='<%# Eval("RESPONSE_AES_KEY") %>' CssClass="wrap-text" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Res HASH Key" SortExpression="RESPONSE_HASH_KEY">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRESPONSE_HASH_KEY" runat="server" Text='<%# Eval("RESPONSE_HASH_KEY") %>' CssClass="wrap-text" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="HASH Algo" SortExpression="HASH_ALGORITHM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHASH_ALGORITHM" runat="server" Text='<%# Eval("HASH_ALGORITHM") %>' CssClass="wrap-text" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Customer A/C No." SortExpression="CUSTOMER_ACCOUNT_NUMBER">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCUSTOMER_ACCOUNT_NUMBER" runat="server" Text='<%# Eval("CUSTOMER_ACCOUNT_NUMBER") %>' CssClass="wrap-text" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Active">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Eval("IS_ACTIVE") %>' Enabled="true" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Style="background-color: #028dce; border: 0px solid black; border-radius: 5px" CommandName="EditCommand" CommandArgument='<%# Eval("ID") %>' OnClick="LinkButtonEdit_Click"><i class="fa fa-pencil" style="color:white;font-size:small;padding:5px"></i></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton2" runat="server" Style="background-color: red; border: 0px solid black; border-radius: 5px" CommandName="DeleteCommand" CommandArgument='<%# Eval("ID") %>' OnClick="LinkButtonDelete_Click"><i class="fa fa-trash" style="color:white;font-size:small;padding:5px"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
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
                    <br />
                </div>
                <script>
                    document.getElementById('txtSearch').addEventListener('keyup', function () {
                        var filterValue = this.value.toLowerCase();
                        var rows = document.querySelectorAll('#<%= GridView1.ClientID %> tbody tr');

                        // Skip the first row (header row)
                        for (var i = 1; i < rows.length; i++) {
                            var row = rows[i];
                            var cells = row.querySelectorAll('td');
                            var rowText = '';

                            // Concatenate text content of all cells in the row
                            cells.forEach(function (cell) {
                                rowText += cell.textContent.toLowerCase() + ' ';
                            });

                            // Toggle row visibility based on whether it matches the filter
                            if (rowText.indexOf(filterValue) > -1) {
                                row.style.display = '';
                            } else {
                                row.style.display = 'none';
                            }
                        }
                    });
                </script>
                <script>
                    function sortTable(columnIndex) {
                        var table, rows, switching, i, x, y, shouldSwitch;
                        table = document.getElementById("GridView1");
                        switching = true;

                        while (switching) {
                            switching = false;
                            rows = table.rows;
                            for (i = 1; i < (rows.length - 1); i++) {
                                shouldSwitch = false;
                                x = rows[i].getElementsByTagName("TD")[columnIndex];
                                y = rows[i + 1].getElementsByTagName("TD")[columnIndex];
                                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                                    shouldSwitch = true;
                                    break;
                                }
                            }
                            if (shouldSwitch) {
                                rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                                switching = true;
                            }
                        }
                    }
                </script>
                <script>
                    document.addEventListener("DOMContentLoaded", function () {
                        var headers = document.querySelectorAll("#<%= GridView1.ClientID %> th");
                        headers.forEach(function (header, index) {
                            header.addEventListener("click", function () {
                                sortTable(index);
                            });
                        });
                    });
                </script>


            </div>
        </div>
    </div>
</asp:Content>
