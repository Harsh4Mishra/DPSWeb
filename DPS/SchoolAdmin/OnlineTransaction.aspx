<%@ Page Title="" Language="C#" MasterPageFile="~/SchoolAdmin/SchoolMaster.Master" AutoEventWireup="true" CodeBehind="OnlineTransaction.aspx.cs" Inherits="DPS.SchoolAdmin.OnlineTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                    <asp:DropDownList ID="ddlClass" Width="100%" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-sm-flex">&nbsp;&nbsp;</div>
                                <div class="col-sm-flex">
                                    <asp:DropDownList ID="ddlSection" Width="100%" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-sm-flex">&nbsp;&nbsp;</div>
                                <div class="col-sm-flex">
                                    <asp:TextBox ID="TextBox1" runat="server" placeholder="From Date" AutoCompleteType="Disabled" CssClass="form-control" />
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox1"
                                        Format="dd/MM/yyyy"></asp:CalendarExtender>
                                </div>
                                <div class="col-sm-flex">&nbsp;&nbsp;</div>
                                <div class="col-sm-flex">
                                    <asp:TextBox ID="TextBox2" runat="server" placeholder="Till Date" AutoCompleteType="Disabled" CssClass="form-control" />
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox2"
                                        Format="dd/MM/yyyy"></asp:CalendarExtender>
                                </div>
                                <div class="col-sm-flex">&nbsp;&nbsp;</div>
                                <div class="col-sm-flex">

                                    <asp:LinkButton ID="linkButtonfilter" runat="server" class="btn btn-outline-success btn-fw icon-excelcss" Style="border-radius: 5px" OnClick="linkButtonfilter_Click"><i class="fa fa-filter" style="font-size:medium"></i></asp:LinkButton>
                                </div>
                                <div class="col-sm-flex">&nbsp;&nbsp;</div>
                                <div class="col-sm-flex">
                                    <%--<asp:TextBox ID="txtSearch" CssClass="searchform-control" runat="server" onKeyPress="javascript:ApplySearch();" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtSearch" AutoCompleteType="Disabled" placeholder="Search..." CssClass="form-control" runat="server" onkeyup="filterGrid()" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <%-- <div class="col-sm-flex">&nbsp;&nbsp;</div>
<div class="col-sm-flex">
   
    <asp:LinkButton ID="LinkButtonExportpdf" runat="server" class="btn btn-outline-danger btn-fw icon-pdfcss" Style="border-radius: 5px" OnClick="LinkButtonExportpdf_Click"><i class="fa fa-file-pdf-o" style="font-size:medium"></i></asp:LinkButton>
</div>--%>
                            </div>
                        </div>
                        <%-- <div class="col">
                            <div class="d-flex justify-content-end align-items-center">
                                
                            </div>
                        </div>--%>
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
                            AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="5" EmptyDataText="No data found." ShowHeaderWhenEmpty="true"
                             ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="ScholarNo" SortExpression="ScholarNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("ScholarNo") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Student Name" SortExpression="StudentName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("StudentName") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" Width="12%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Class" SortExpression="ClassName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("ClassName") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Section" SortExpression="SectionName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("SectionName") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Receipt No" SortExpression="ReceiptNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Eval("ReceiptNo") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Receipt Dt" SortExpression="ReceiptDt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReceiptDt" runat="server" Text='<%# Eval("ReceiptDt") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                </asp:TemplateField>
                              <%--  <asp:TemplateField HeaderText="Fee Amt" SortExpression="TotFeeAmt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotFeeAmt" runat="server" Text='<%# Eval("TotFeeAmt") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="9%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fine Amt" SortExpression="FineAmt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFineAmt" runat="server" Text='<%# Eval("FineAmt") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="9%" />
                                </asp:TemplateField>--%>

                              <%--  <asp:TemplateField HeaderText="Discount Amt" SortExpression="TotDisAmt" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotDisAmt" runat="server" Text='<%# Eval("TotDisAmt") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="9%" />
                                </asp:TemplateField>--%>
                             <%--   <asp:TemplateField HeaderText="Recived Amt" SortExpression="TotRecAmt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotRecAmt" runat="server" Text='<%# Eval("TotRecAmt") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="9%" />
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Online Amt" SortExpression="OnlineAmt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOnlineAmt" runat="server" Text='<%# Eval("OnlineAmt") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Online Ref No." SortExpression="OnlineRefNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOnlineRefNo" runat="server" Text='<%# Eval("OnlineRefNo") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#028dce" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
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
