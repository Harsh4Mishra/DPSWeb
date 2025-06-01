<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfflineFeeTransaction.aspx.cs" Inherits="DPS.SchoolAdmin.OfflineFeeTransaction" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 20%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <table class="auto-style1">
                <tr>
                    <td>
                        <table class="auto-style1">
                            <tr>
                                <td class="auto-style2">
                                    <asp:DropDownList ID="ddlClass" Width="100%" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList></td>
                                <td class="auto-style2">
                                    <asp:DropDownList ID="ddlSection" Width="100%" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList></td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="TextBox1" runat="server" placeholder="From Date" AutoCompleteType="Disabled" CssClass="form-control" />
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox1"
                                        Format="dd/MM/yyyy"></asp:CalendarExtender>
                                </td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="TextBox2" runat="server" placeholder="Till Date" AutoCompleteType="Disabled" CssClass="form-control" />
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox2"
                                        Format="dd/MM/yyyy"></asp:CalendarExtender>
                                </td>
                                <td class="auto-style2">
                                    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        <asp:Label ID="lblFromDate" runat="server" Text="Label"></asp:Label>
                        <asp:Label ID="lbltodate" runat="server" Text="Label"></asp:Label>
                        <br />
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
                                <asp:TemplateField HeaderText="Cheque Amt" SortExpression="ChequeAmt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChequeAmt" runat="server" Text='<%# Eval("ChequeAmt") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash Amt" SortExpression="CashRecAmt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCashRecAmt" runat="server" Text='<%# Eval("CashRecAmt") %>' />
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
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
