﻿
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="QIS.Web.Secure.TransaksiLab" %>

<%@ Register TagPrefix="Module" TagName="webcal" Src="../../../incl/calendar.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Informasi Transaksi dan Hasil Laboratorium </title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href='/qistoollib/css/styles_blue.css' type="text/css" rel="Stylesheet">
    <script language="javascript" type="text/javascript" src="/qistoollib/scripts/JScript.js"></script>
    <%--<script src='<%= UrlBase + "/JSDLGGlobalVar.aspx" %>' language="javascript"></script>--%>
    <script language="javascript">
        function Tengah() {
            var xcenter = (screen.availWidth - 800) / 2;
            var ycenter = (screen.availHeight - 600) / 2;
            self.resizeTo(800, 500);
            self.moveTo(xcenter, ycenter);
        }
    </script>
</head>
<body onload="Tengah();">
    <form id="frmtranspenunjang" runat="server">
    <table border="0" width="100%" height="100%" cellspacing="0" cellpadding="2">
        <tr>
            <td width="100%" height="100%" valign="top">
                <div style="width: 100%; height: 100%; overflow: auto;">
                    <table cellspacing="10" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td align="left">
                                <table cellspacing="0" cellpadding="5" width="100%">
                                    <tr class="rheader">
                                        <td class="rheadercol" align="left" height="25">
                                            Informasi Transaksi dan Hasil Laboratorium (<asp:Label ID="lblNamaPasien" runat="server" />)
                                        </td>
                                    </tr>
                                    <tr class="rbody">
                                        <td class="rbodycol" align="middle">
                                            <table width="100%">
                                                <!-- PAGE CONTENT BEGIN HERE -->
                                                <tr>
                                                    <td width="100%" align="Right" style="display: none">
                                                        <asp:TextBox ID="txtNomorRegistrasi" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%">
                                                        <div style="overflow: Auto; width: 100%; height: 280px">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="100%">
                                                                        <asp:DataGrid ID="grdDetailTransaksi" runat="server" AutoGenerateColumns="False"
                                                                            CellSpacing="0" BorderColor="Gainsboro" BorderWidth="1" GridLines="Horizontal"
                                                                            Height="100%" Width="100%" AllowPaging="False" PageSize="20" DataKeyField="nolab"
                                                                            ShowFooter="True" CellPadding="0">
                                                                            <HeaderStyle CssClass="gridHeaderStyle" />
                                                                            <AlternatingItemStyle CssClass="gridAlternatingItemStyle" />
                                                                            <EditItemStyle Font-Bold="false" />
                                                                            <PagerStyle Mode="NumericPages" HorizontalAlign="right" />
                                                                            <Columns>
                                                                                <asp:TemplateColumn runat="server" HeaderText="Tanggal" HeaderStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" Style="margin-left: 5; margin-right: 5" Text='<%# DataBinder.Eval(Container.DataItem, "tgldaftar", "{0:dd-MM-yyyy}") %>'
                                                                                            ID="_lblTanggal" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn runat="server" HeaderText="No.Bukti" HeaderStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" Style="margin-left: 5; margin-right: 5" Text='<%# DataBinder.Eval(Container.DataItem, "nolab") %>'
                                                                                            ID="_lblNoLab" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn runat="server" HeaderText="Approved" HeaderStyle-HorizontalAlign="Right"
                                                                                    ItemStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox runat="server" Style="margin-left: 5; margin-right: 5" Checked='<%# DataBinder.Eval(Container.DataItem, "validasi") %>'
                                                                                            Enabled="False" ID="_lblValid" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:ButtonColumn ItemStyle-HorizontalAlign="Center" CommandName="__PrintHasil" Text="<img src=/qistoollib/images/tbprint.png border=0 align=absmiddle alt='Cetak Hasil'>" Visible="True">
                                                                                </asp:ButtonColumn>
                                                                            </Columns>
                                                                        </asp:DataGrid>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <!-- PAGE CONTENT END HERE -->
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <!-- VALIDATION SUMMARY BEGIN HERE -->
                                <table cellspacing="0" cellpadding="5" width="100%">
                                    <tr>
                                        <td align="left">
                                            <p>
                                                <asp:ValidationSummary ID="ValidationSummary" runat="server" HeaderText="Field(s) berikut harus diisi atau perlu diperbaiki." />
                                        </td>
                                    </tr>
                                </table>
                                <!-- VALIDATION SUMMARY END HERE -->
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
    <script src='/qislib/scripts/common/common.js' language="javascript"></script>
</body>
</html>
