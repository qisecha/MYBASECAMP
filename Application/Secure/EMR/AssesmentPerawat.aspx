﻿<%@ Register TagPrefix="Module" TagName="Copyright" Src="../../incl/copyright.ascx" %>
<%@ Register TagPrefix="Module" TagName="RadMenu" Src="../../incl/RadMenu.ascx" %>
<%@ Register TagPrefix="Module" TagName="CSSToolbar" Src="../../incl/CSSToolbar.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="Module" TagName="Calendar" Src="../../incl/calendar.ascx" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI" Assembly="eWorld.UI, Version=1.9.0.0, Culture=neutral, PublicKeyToken=24d65337282035f2" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AssesmentPerawat.aspx.vb"
    Inherits="QIS.Web.EMR.AssesmentPerawat" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>EMR - Assesment Perawat</title>
    <link rel="title icon" href="/qistoollib/images/favicon.png" />
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="/qistoollib/css/styles_blue.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="/qistoollib/scripts/JScript.js"></script>
    <style type="text/css">
        #ulRepProjects
        {
            width: 100%;
            margin: 0;
            padding: 0;                     
        }
        #ulRepProjects li
        {
            list-style-type: none;
            display: inline-block; /* FF3.6; Chrome10+ */                     
            *display: inline;
            background: #eeeeee;
            width: 280px;
            height: 150px;
            margin: 5px;
        }
        #ulRepProjectGroup
        {
            width: 100%;
            margin: 0;
            padding: 0;                     
        }
        #ulRepProjectGroup li
        {
            list-style-type: none;            
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <table width="100%" cellpadding="2" cellspacing="0">
        <tr>
            <td width="100%" valign="top" colspan="3">
                <!-- BEGIN PAGE HEADER -->
                <Module:RadMenu ID="RadMenu" runat="server" />
                <!-- END PAGE HEADER -->
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="rheader">
                            Assesment Perawat
                        </td>
                    </tr>
                    <tr>
                        <td class="hseparator" style="width: 100%;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="100%" valign="top">
                <asp:Panel ID="pnlPatientList" runat="server">
                    <table width="100%">
                        <tr>
                            <td>
                                <telerik:RadTabStrip RenderMode="Lightweight" ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage2"
                                    Skin="Windows7" SelectedIndex="0" AutoPostBack="true">
                                    <Tabs>
                                        <telerik:RadTab Text="Pasien">
                                        </telerik:RadTab>
                                        <telerik:RadTab Text="Data Rekam Medis" Visible="false">
                                        </telerik:RadTab>
                                    </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage ID="RadMultiPage2" CssClass="RadMultiPage" runat="server" SelectedIndex="0">
                                    <telerik:RadPageView ID="RadPageView3" runat="server">
                                        <table cellpadding="2" width="100%">
                                            <tr>
                                                <td class="right">
                                                   Tanggal
                                                </td>
                                                <td>
                                                   <Module:Calendar ID="calTgl" runat="server" DontResetDate="true"></Module:Calendar>
                                                </td>
                                                <td class="right">
                                                    Cari Pasien
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSearchPatient" runat="server" Width="200"></asp:TextBox>
                                                    <asp:Button ID="btnSearchPatient" runat="server" Text="Cari" Width="100" />
                                                </td>
                                                <td class="right">
                                                    Departemen
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDepartmentFilter" runat="server" Width="150" AutoPostBack="true">
                                                        <asp:ListItem Value="OUTPATIENT" Text="RAWAT JALAN"></asp:ListItem>
                                                        <asp:ListItem Value="EMERGENCY" Text="RAWAT DARURAT"></asp:ListItem>
                                                        <asp:ListItem Value="INPATIENT" Text="RAWAT INAP"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlServiceUnit" runat="server" Width="150" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:DataGrid ID="grdTodayPatient" runat="server" BorderWidth="0" GridLines="None"
                                                        Width="100%" CellPadding="2" CellSpacing="1" ShowHeader="true" ShowFooter="false"
                                                        AutoGenerateColumns="false">
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="gridHeaderStyle" />
                                                        <ItemStyle CssClass="gridItemStyle" />
                                                        <AlternatingItemStyle CssClass="gridAlternatingItemStyle" />
                                                        <PagerStyle Mode="NumericPages" HorizontalAlign="right" />
                                                        <Columns>
                                                            <asp:TemplateColumn runat="server" HeaderText="Pengkajian" ItemStyle-Width="50" ItemStyle-VerticalAlign="Top">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" ID="_chkIsNurseAssesment" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "IsNurseAssesment")%>' />
                                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PengkajianID") %>'
                                                                        ID="_lblPengkajianID" Visible="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn runat="server" HeaderText="Jam" ItemStyle-Width="50" ItemStyle-VerticalAlign="top">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "RegistrationTime")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn runat="server" HeaderText="No. Registrasi" ItemStyle-Width="120"
                                                                ItemStyle-VerticalAlign="top">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %>'
                                                                        ID="_lbtnRegistrationNo" ForeColor="Blue" CommandName="GetRegistration" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn runat="server" HeaderText="No. Transaksi" ItemStyle-Width="120"
                                                                ItemStyle-VerticalAlign="top">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>'
                                                                        ID="_lblTransactionNo" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn runat="server" HeaderText="Nama Pasien" ItemStyle-VerticalAlign="top">
                                                                <ItemTemplate>
                                                                    <table cellpadding="1" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txtweak">
                                                                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PhysicianName") %>'
                                                                                    ID="_lblPhysicianName" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn runat="server" HeaderText="No. MR" ItemStyle-Width="120" ItemStyle-VerticalAlign="top">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "MRN")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn runat="server" HeaderText="L/P" ItemStyle-Width="50" ItemStyle-VerticalAlign="top">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "PatientGender")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn runat="server" HeaderText="Tgl. Lahir" ItemStyle-Width="80" ItemStyle-VerticalAlign="top">
                                                                <ItemTemplate>
                                                                    <%# Format(DataBinder.Eval(Container.DataItem, "PatientDOB"), "dd-MMM-yyyy")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn runat="server" HeaderText="Unit Pelayanan" ItemStyle-VerticalAlign="top">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn runat="server" HeaderText="Penjamin" ItemStyle-VerticalAlign="top">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "BusinessPartnerName")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView4" runat="server">
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlPatientRecord" runat="server">
                    <table cellpadding="0" width="100%">
                        <tr>
                            <td class="projectbanner projectbanner_heading1">
                                <table width="100%" cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td rowspan="3" style="background: #eeeeee; width: 100;" class="center">
                                            <asp:Image ID="imgPBPatient" runat="server" BorderStyle="None" Width="70" />
                                        </td>
                                        <td class="projectbanner_heading1 right">
                                            Pasien:
                                        </td>
                                        <td class="projectbanner_heading1">
                                            <asp:Label runat="server" ID="lblPBPatientName"></asp:Label>
                                            &nbsp;(<asp:Label runat="server" ID="lblPBPatientGender"></asp:Label>)
                                        </td>
                                        <td class="projectbanner right">
                                            Tanggal & Jam Daftar:
                                        </td>
                                        <td class="projectbanner">
                                            <asp:Label runat="server" ID="lblPBRegistrationDate" Font-Bold="true"></asp:Label>
                                            &nbsp;<asp:Label runat="server" ID="lblPBRegistrationTime" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="projectbanner_heading1 right">
                                            Reg#:
                                        </td>
                                        <td class="projectbanner_heading1">
                                            <asp:Label runat="server" ID="lblPBRegistrationNo"></asp:Label>
                                            |&nbsp;<asp:Label runat="server" ID="lblPBMRN"></asp:Label>
                                        </td>
                                        <td class="projectbanner right">
                                            Tanggal Lahir:
                                        </td>
                                        <td class="projectbanner">
                                            <asp:Label runat="server" ID="lblPBPatientDOB" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td class="projectbanner right">
                                            Unit:
                                        </td>
                                        <td class="projectbanner">
                                            <asp:Label runat="server" ID="lblPBServiceUnitName" Font-Bold="true"></asp:Label>
                                            <asp:Label runat="server" ID="lblPBServiceUnitID" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="projectbanner_heading1 right">
                                            Txn#:
                                        </td>
                                        <td class="projectbanner_heading1">
                                            <asp:Label runat="server" ID="lblPBTransactionNo"></asp:Label>
                                        </td>
                                        <td class="projectbanner right">
                                            Dokter:
                                        </td>
                                        <td class="projectbanner">
                                            <asp:Label runat="server" ID="lblPBPhysicianName" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td class="projectbanner right">
                                            Penjamin:
                                        </td>
                                        <td class="projectbanner">
                                            <asp:Label runat="server" ID="lblPBBusinessPartnerName" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadTabStrip RenderMode="Lightweight" ID="RadTabStrip3" runat="server" MultiPageID="RadMultiPage3"
                                    Skin="Windows7" SelectedIndex="0" AutoPostBack="true">
                                    <Tabs>
                                        <telerik:RadTab Text="Asesmen Keperawatan">
                                        </telerik:RadTab>
                                        <telerik:RadTab Text="Keperawatan">
                                        </telerik:RadTab>
                                        <telerik:RadTab Text="Catatan Perawat">
                                        </telerik:RadTab>
                                        <telerik:RadTab Text="CPPT">
                                        </telerik:RadTab>
                                    </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage ID="RadMultiPage3" CssClass="RadMultiPage" runat="server" SelectedIndex="0">
                                    <telerik:RadPageView ID="rpvAsesmenKeperawatan" runat="server">
                                        <asp:Panel ID="pnlNurseAssesment" runat="server">
                                            <table class="ToolbarM" cellpadding="1" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td class="left padding-LR-5">
                                                        <asp:LinkButton runat="server" ID="lbtnBack" ToolTip="Kembali ke Daftar Pasien" CausesValidation="false"
                                                            Width="48"><img src="/qistoollib/images/tbback.png" alt="" border="0" /></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbtnNewSOAP" ToolTip="Tambah" CausesValidation="false"
                                                            Width="48" Visible="false"><img src="/qistoollib/images/tbnew.png" alt="" border="0" /></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbtnSaveSOAP" ToolTip="Simpan" CausesValidation="false"
                                                            Width="48"><img src="/qistoollib/images/tbsave.png" alt="" border="0" /></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdNurseAssessment" runat="server" BorderWidth="0" GridLines="None"
                                                            Width="100%" CellPadding="2" CellSpacing="1" ShowHeader="true" ShowFooter="false"
                                                            AutoGenerateColumns="false">
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="gridHeaderStyle" />
                                                            <ItemStyle CssClass="gridItemStyle" />
                                                            <AlternatingItemStyle CssClass="gridAlternatingItemStyle" />
                                                            <PagerStyle Mode="NumericPages" HorizontalAlign="right" />
                                                            <Columns>
                                                                <asp:TemplateColumn runat="server" HeaderText="" ItemStyle-Width="30" ItemStyle-VerticalAlign="Top">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="_lblPengkajianID" Text='<%# DataBinder.Eval(Container.DataItem, "PengkajianID")%>'
                                                                            Visible="false"></asp:Label>
                                                                        <asp:ImageButton runat="server" ID="_ibtnEdit" ImageUrl="/qistoollib/images/edit.png"
                                                                            CommandName="Revise" ToolTip="Revisi" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn runat="server" HeaderText="Tipe Asesmen" ItemStyle-Width="150"
                                                                    ItemStyle-VerticalAlign="Top">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "AssessmentTypeName") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn runat="server" HeaderText="Dibuat Pada" ItemStyle-Width="250"
                                                                    ItemStyle-VerticalAlign="Top">
                                                                    <ItemTemplate>
                                                                        <%# Format(DataBinder.Eval(Container.DataItem, "CreatedDate"), "dd-MMM-yyyy hh:mm")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn runat="server" HeaderText="Dibuat Oleh" ItemStyle-Width="250"
                                                                    ItemStyle-VerticalAlign="Top">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table cellpadding="2" cellspacing="1" width="100%">
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Tipe Asesmen
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlAssessmentType" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td rowspan="3" style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Asal Informasi
                                                                </td>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Informasi dari
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPengkajianID" runat="server" Visible="false"></asp:TextBox>
                                                                    <asp:DropDownList ID="ddlAsalInformasi" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Nama pemberi informasi
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAsalInformasiNama" runat="server" Width="400"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Hubungan
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlAsalInformasiHubungan" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;" colspan="4">
                                                    </td>
                                                </tr>
                                                <asp:Panel runat="server" ID="_pnlTriage" Visible="false">
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                   Triage
                                                                </td>
                                                                <td>
                                                                    <asp:dropdownlist id="ddlKdTriage" runat="server" width="150"></asp:dropdownlist>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Tanggal Kejadian  
                                                                </td>
                                                                <td>
                                                                   <module:Calendar id="calTglDatang" runat="server" dontresetdate="true"></module:Calendar>
                                                                   &nbsp;
                                                                   <ew:maskedtextbox id="txtJamDatang" runat="server" autopostback="False" width="40" />
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Tanggal Layan  
                                                                </td>
                                                                <td>
                                                                   <module:Calendar id="calTglLayan" runat="server" dontresetdate="true"></module:Calendar>
                                                                   &nbsp;
                                                                   <ew:maskedtextbox id="txtJamLayan" runat="server" autopostback="False" width="40" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:panel>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Keluhan Utama
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtKeluhanUtama" runat="server" Width="554" TextMode="MultiLine"
                                                                        Height="50"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Riwayat Keluhan Saat ini
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtRiwayatKeluhanSaatIni" runat="server" Width="554" TextMode="MultiLine"
                                                                        Height="50"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td rowspan="2" style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Riwayat Alergi
                                                                </td>
                                                                <td rowspan="2" style="width: 150;">
                                                                    <asp:RadioButtonList ID="rblIsRiwayatAlergi" runat="server">
                                                                        <asp:ListItem Text="Tidak Ada" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Ada" Value="1"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Jenis Alergi
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlJenisAlergi" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Reaksi Alergi
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtReaksiAlergi" runat="server" Width="150"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Kesadaran
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlKesadaran" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td rowspan="4" style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Tanda Vital
                                                                </td>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Tekanan Darah (mmHg)
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtttvTekananDarah" runat="server" Width="150"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Berat Badan (kg)
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtttvBeratBadan" runat="server" Width="150" AutoPostBack="true"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Nadi (x/mnt)
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtttvNadi" runat="server" Width="150"></asp:TextBox>
                                                                    <asp:DropDownList ID="ddlttvNadiDenyut" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Tinggi Badan (cm)
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtttvTinggiBadan" runat="server" Width="150" AutoPostBack="true"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Suhu (C)
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtttvSuhu" runat="server" Width="150"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 150; background-color: #00CCFF;">
                                                                    Index Massa Tubuh
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtttvIndexMassaTubuh" runat="server" Width="150"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Pernafasan (x/mnt)
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtttvPernafasan" runat="server" Width="150"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Nyeri?
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblIsNyeri" runat="server">
                                                                        <asp:ListItem Text="Tidak Ada Nyeri" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Ada Nyeri" Value="1"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td rowspan="4" valign="bottom">
                                                                    <asp:RadioButtonList ID="rblNyeriSkala" runat="server" RepeatDirection="Horizontal"
                                                                        RepeatLayout="Table" AutoPostBack="true">
                                                                        <asp:ListItem Text='<img src="/qistoollib/images/wb0.png" />' Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text='<img src="/qistoollib/images/wb2.png" />' Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text='<img src="/qistoollib/images/wb4.png" />' Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text='<img src="/qistoollib/images/wb6.png" />' Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text='<img src="/qistoollib/images/wb8.png" />' Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text='<img src="/qistoollib/images/wb10.png" />' Value="10"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td rowspan="19" style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Penilaian Nyeri
                                                                </td>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Lokasi Nyeri
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNyeriLokasi" runat="server" Width="150"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Durasi Nyeri
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNyeriDurasi" runat="server" Width="150"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #00CCFF;">
                                                                    Skala Nyeri
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNyeriSkala" runat="server" Width="150"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Metode
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlNyeriMetode" runat="server" Width="150" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <asp:Panel ID="pnlNRS" runat="server" Visible = "false">
                                                             <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Jenis Nyeri
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlNRS" runat="server" Width="250" AutoPostBack ="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlWajah" runat="server" Visible = "false">
                                                             <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Wajah
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlWajah" runat="server" Width="250" AutoPostBack ="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            </asp:Panel>
                                                             <asp:Panel ID="pnlGerakBawah" runat="server" Visible = "false">
                                                             <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Anggota Gerak Bawah
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlGerakBawah" runat="server" Width="250" AutoPostBack ="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Aktivitas
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlAktivitas" runat="server" Width="250" AutoPostBack ="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlTangis" runat="server" Visible = "false">
                                                             <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Menangis
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlTangis" runat="server" Width="250" AutoPostBack ="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlBicara" runat="server" Visible = "false">
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Bicara atau Bersuara
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlBicara" runat="server" Width="250" AutoPostBack ="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            </asp:Panel>
                                                             <asp:Panel ID="pnlPola" runat="server" Visible = "false">
                                                             <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Pola Napas
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlNapas" runat="server" Width="250" AutoPostBack ="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Tungkai
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlTungkai" runat="server" Width="250" AutoPostBack ="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Tingkat Kesadaran
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlSadar" runat="server" Width="250" AutoPostBack ="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlGerakan" runat="server" Visible = "false">
                                                             <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Extremitas Atas
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlGerak" runat="server" Width="250" AutoPostBack ="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Kompensasi Terhadap Ventilator /
                                                                    Vokalisasi
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlKompensasi" runat="server" Width="250" AutoPostBack ="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            </asp:Panel>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Kualitas Nyeri
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlNyeriKualitas" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Karakteristik
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlNyeriKarakteristik" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Nyeri Berkurang dengan
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:DropDownList ID="ddlNyeriBerkurang" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtNyeriBerkurangKeterangan" runat="server" Width="302"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Nyeri Bertambah dengan
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:DropDownList ID="ddlNyeriBertambah" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtNyeriBertambahKeterangan" runat="server" Width="302"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Aktivitas dan Mobilisasi
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlAktivitasMobilisasi" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Resiko Jatuh
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtKategoriResikoJatuh" runat="server" Width="302"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Resiko Dekubitus
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblIsDekubitas" runat="server">
                                                                        <asp:ListItem Text="Tidak" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Ya" Value="1"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td rowspan="6" style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Status Psikososial
                                                                </td>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Status Psikologi
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlStatusPsikologi" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtStatusPsikologiKeterangan" runat="server" Width="302"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Status Mental
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlStatusMental" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtStatusMentalKeterangan" runat="server" Width="302"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Status Pernikahan
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlStatusPernikahan" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Hubungan Keluarga
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblIsHubunganKeluargaBaik" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Baik" Value="1" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Tidak Baik" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Tempat Tinggal
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlStatusTempatTinggal" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Kebiasaan Beribadah Teratur
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblIsBeribadahTeratur" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Ya" Value="1" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Tidak" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td rowspan="2" style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Status Ekonomi
                                                                </td>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Pekerjaan
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlPekerjaan" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Penjamin Bayar
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlPenjaminBayar" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td rowspan="7" style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Status Nutrisi
                                                                </td>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Status Gizi Bayi/Anak
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlStatusGizi" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td rowspan="4" style="width: 150; background-color: #F0F0F0;">
                                                                    Status Gizi Dewasa
                                                                </td>
                                                                <td colspan="2">
                                                                    Adakah perubahan berat badan signifikan dalam 6 bulan terakhir?
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlSkorPerubahanBeratBadan" runat="server" Width="150" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Apakah asupan makanan berkurang karena tidak nafsu makan dalam 5 hari terakhir?
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblIsTidakNafsuMakan" runat="server" RepeatDirection="Horizontal"
                                                                        AutoPostBack="true">
                                                                        <asp:ListItem Text="Tidak" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Ya" Value="1"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #00CCFF;">
                                                                    Skor Nutrisi
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSkorNutrisi" runat="server" Width="150"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #00CCFF;">
                                                                    Perlu Konsultasi Gizi
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblIsPerluKonsultasiGizi" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Tidak" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Ya (Skor > 2)" Value="1"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td rowspan="2" style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Status Kehamilan
                                                                </td>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Hamil?
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblIsHamil" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Tidak Hamil" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Hamil" Value="1"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Gravid
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtHamilGravidKeterangan" runat="server" Width="150"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Para
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtHamilParaKeterangan" runat="server" Width="150"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Abortus
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtHamilAbortusKeterangan" runat="server" Width="150"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td rowspan="10" style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Edukasi Pasien & Keluarga
                                                                </td>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Kesediaan Menerima Informasi
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblIsBersediaMenerimaInformasi" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Ya" Value="1" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Tidak" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td rowspan="2" style="width: 150; background-color: #F0F0F0;">
                                                                    Hambatan Dalam Edukasi
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblIsHambatanEdukasi" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Tidak" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Ya" Value="1"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkIsHambatanPendengaran" runat="server" Text="Pendengaran" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkIsHambatanPenglihatan" runat="server" Text="Penglihatan" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkIsHambatanKognitif" runat="server" Text="Kognitif" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkIsHambatanFisik" runat="server" Text="Fisik" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkIsHambatanBudaya" runat="server" Text="Budaya" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkIsHambatanEmosi" runat="server" Text="Emosi" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkIsHambatanBahasa" runat="server" Text="Bahasa" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtHambatanLainnya" runat="server" Width="150"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Tingkat Pendidikan Pasien
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlPendidikanPasien" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Bahasa Sehari-hari
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlBahasaPasien" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtBahasaLainnya" runat="server" Width="150"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Agama
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlAgamaPasien" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Budaya/Nilai khusus
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBudayaPasien" runat="server" Width="302"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Perlu Penterjemah
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPerluPenterjemah" runat="server" Width="302"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Cara Belajar Disukai
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlCaraBelajarDisukai" runat="server" Width="150">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td rowspan="2" style="width: 150; background-color: #F0F0F0;">
                                                                    Informasi yang Diinginkan
                                                                </td>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkIsDiagnosa" runat="server" Text="Diagnosa Penyakit" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkIsDietNutrisi" runat="server" Text="Diet dan Nutrisi" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkIsTerapi" runat="server" Text="Terapi" />
                                                                            </td>
                                                                       </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkIsNyeri" runat="server" Text="Manajemen Nyeri" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkIsAlatMedis" runat="server" Text="Penggunaan Alat Medis" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtInformasiDiinginkan" runat="server" Width="150"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                         </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;" colspan="4">
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class="hseparator" style="width: 100%;" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                     <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td rowspan="5" style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Disposisi Pasien
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkKeluar" runat="server" Text="Disposisi" AutoPostBack="True" />
                                                                </td>
                                                            </tr>
                                                           <asp:Panel ID="pnlDisposisi" runat="server" Visible = "false">
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Tanggal / Jam
                                                                </td>
                                                                <td>
                                                                    <module:Calendar id="calTglDisposisi" runat="server" dontresetdate="true"></module:Calendar>
                                                                    &nbsp;
                                                                    <ew:maskedtextbox id="txtJamDisposisi" runat="server" style="text-align: center;"
                                                                        width="23%" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td  style="width: 150; background-color: #F0F0F0;">
                                                                    Keadaan Keluar
                                                                </td>
                                                                <td>
                                                                    <asp:dropdownlist id="ddlKdnKlr" runat="server" ></asp:dropdownlist>
                                                                </td>
                                                             </tr>
                                                            <tr>
                                                                <td  style="width: 150; background-color: #F0F0F0;">
                                                                    Cara Keluar
                                                                </td>
                                                                <td>
                                                                    <asp:dropdownlist id="ddlCrKlr" runat="server"></asp:dropdownlist>
                                                                </td>
                                                             </tr>
                                                             </asp:Panel>
                                                        </table>
                                                     </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td rowspan="5" style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                    Informasi Pemindahan Ruangan/Pemulangan Pasien
                                                                </td>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Pulang
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsKIE" runat="server" Text="KIE" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsObatPulang" runat="server" Text="Obat Pulang" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsFotoRadiologi" runat="server" Text="Foto Radiologi" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsLaboratorium" runat="server" Text="Laboratorium" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsEKG" runat="server" Text="EKG" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Pulang atas permintaan sendiri
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsKIEAPS" runat="server" Text="KIE" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIsTTDAPS" runat="server" Text="Tanda tangan persyaratan pulang atas permintaan sendiri" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Masuk Rumah Sakit
                                                                </td>
                                                                <td colspan="5">
                                                                    <asp:DropDownList ID="ddlRoomCode" runat="server" Width="302">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Dirujuk ke Rumah Sakit
                                                                </td>
                                                                <td colspan="5">
                                                                    <asp:TextBox ID="txtReferToHospitalName" runat="server" Width="302"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150; background-color: #F0F0F0;">
                                                                    Meninggal?
                                                                </td>
                                                                <td colspan="5">
                                                                    <asp:RadioButtonList ID="rblIsDeath" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Tidak" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Ya" Value="1"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                    &nbsp;Jam&nbsp;
                                                                    <asp:TextBox ID="txtDeathTime" runat="server" Width="100"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="rpvKeperawatan" runat="server">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <table cellpadding="2" cellspacing="1" width="100%" style="background-color: #DFE9F5;">
                                                <tr>
                                                   <td valign="top" class="rheader" style="background-color: #ffffff;">
                                                        <asp:DataGrid ID="DataGrid1" runat="server" BorderWidth="0" GridLines="None"
                                                            Width="100%" CellPadding="2" CellSpacing="1" ShowHeader="false" ShowFooter="false"
                                                            AutoGenerateColumns="false">
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="gridHeaderStyle" />
                                                            <ItemStyle CssClass="gridItemStyle" />
                                                            <AlternatingItemStyle CssClass="gridAlternatingItemStyle" />
                                                            <PagerStyle Mode="NumericPages" HorizontalAlign="right" />
                                                            <Columns>
                                                                <asp:TemplateColumn runat="server" HeaderText="Informasi Kunjungan" ItemStyle-Width="250"
                                                                    ItemStyle-VerticalAlign="Top">
                                                                    <ItemTemplate>
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 50px; font-weight: bold;" valign="top">
                                                                                        Unit
                                                                                </td>
                                                                                <td valign="top" style="font-weight: bold;" colspan="2">
                                                                                         :&nbsp;<%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%></td>
                                                                            </tr>
                                                                         
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td> 
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="rpvCatatanPerawat" runat="server">
                                        <asp:Panel ID="pnlNurseNotes" runat="server">
                                            <table class="ToolbarM" cellpadding="1" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td class="left padding-LR-5">
                                                        <asp:LinkButton runat="server" ID="lbtnBackNotes" ToolTip="Kembali ke Daftar Pasien"
                                                            CausesValidation="false" Width="48"><img src="/qistoollib/images/tbback.png" alt="" border="0" /></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbtnNewNotes" ToolTip="Tambah" CausesValidation="false"
                                                            Width="48"><img src="/qistoollib/images/tbnew.png" alt="" border="0" /></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbtnSaveNotes" ToolTip="Simpan" CausesValidation="false"
                                                            Width="48"><img src="/qistoollib/images/tbsave.png" alt="" border="0" /></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdNurseNotes" runat="server" BorderWidth="0" GridLines="None"
                                                            Width="100%" CellPadding="2" CellSpacing="1" ShowHeader="true" ShowFooter="false"
                                                            AutoGenerateColumns="false">
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="gridHeaderStyle" />
                                                            <ItemStyle CssClass="gridItemStyle" />
                                                            <AlternatingItemStyle CssClass="gridAlternatingItemStyle" />
                                                            <PagerStyle Mode="NumericPages" HorizontalAlign="right" />
                                                            <Columns>
                                                                <asp:TemplateColumn runat="server" HeaderText="" ItemStyle-Width="30" ItemStyle-VerticalAlign="Top">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="_lblNurseNotesID" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'
                                                                            Visible="false"></asp:Label>
                                                                        <asp:ImageButton runat="server" ID="_ibtnEdit" ImageUrl="/qistoollib/images/edit.png"
                                                                            CommandName="Revise" ToolTip="Revisi" Visible='<%# NOT DataBinder.Eval(Container.DataItem, "IsRevised") AND NOT DataBinder.Eval(Container.DataItem, "IsPhysicianConfirmed")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn runat="server" HeaderText="Informasi Kunjungan" ItemStyle-Width="250"
                                                                    ItemStyle-VerticalAlign="Top">
                                                                    <ItemTemplate>
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 50px;" valign="top">
                                                                                    Tanggal
                                                                                </td>
                                                                                <td valign="top">
                                                                                    :&nbsp;<%# Format(DataBinder.Eval(Container.DataItem, "CreatedDate"), "dd-MMM-yyyy hh:mm")%></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" valign="top">
                                                                                    <asp:Panel runat="server" ID="_pnlRevised" Visible='<%# DataBinder.Eval(Container.DataItem, "IsRevised")%>'>
                                                                                        <table cellpadding="2" cellspacing="1" style="background-color: Red;">
                                                                                            <tr>
                                                                                                <td style="color: White;">
                                                                                                    REVISED
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="txtweak" style="background-color: White;">
                                                                                                    on
                                                                                                    <%# Format(DataBinder.Eval(Container.DataItem, "LastUpdatedDate"), "dd-MMM-yyyy hh:mm")%><br />
                                                                                                    by
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "LastUpdatedByUserName")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel runat="server" ID="_pnlCreated" Visible='<%# NOT DataBinder.Eval(Container.DataItem, "IsRevised")%>'>
                                                                                        <table cellpadding="2" cellspacing="1" style="background-color: Olive;">
                                                                                            <tr>
                                                                                                <td style="color: White;">
                                                                                                    CREATED
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="txtweak" style="background-color: White;">
                                                                                                    on
                                                                                                    <%# Format(DataBinder.Eval(Container.DataItem, "CreatedDate"), "dd-MMM-yyyy hh:mm")%><br />
                                                                                                    by
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "CreatedByUserName")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn runat="server" HeaderText="Catatan Perawat" ItemStyle-VerticalAlign="Top">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="_txtNurseNotes" runat="server" TextMode="MultiLine" Font-Names="Segoe-UI,Arial"
                                                                            Width="100%" Height="100" ReadOnly="true" Text='<%# DataBinder.Eval(Container.DataItem, "NurseNotes")%>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn runat="server" HeaderText="Konfirmasi Dokter" ItemStyle-Width="100"
                                                                    ItemStyle-VerticalAlign="Top">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="_chkIsPhysicianConfirmed" runat="server" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "IsPhysicianConfirmed")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="hseparator" style="width: 100%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="2" cellspacing="1" width="100%">
                                                                       <tr>
                                                                            <td style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                                Tanggal Pemeriksaan  
                                                                            </td>
                                                                            <td>
                                                                               <module:Calendar id="calTglCP" runat="server" dontresetdate="true"></module:Calendar>
                                                                               &nbsp;
                                                                               <ew:maskedtextbox id="txtJamCP" runat="server" autopostback="False" width="40" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                                Catatan Perawat
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtNurseNotesID" runat="server" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtNurseNotes" runat="server" TextMode="Multiline" Width="554" Height="100"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 200; background-color: #30CD06; color: #ffffff;">
                                                                                Konfirmasi Dokter
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkIsPhysicianConfirmed" runat="server" Enabled="false" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="rpvCPPT" runat="server">
                                        <asp:Panel ID="pnlCPPT" runat="server">
                                            <table cellpadding="2" cellspacing="1" width="100%" style="background-color: #DFE9F5;">
                                                <tr>
                                                    <td class="rheader" style="background-color: #ffffff;">
                                                        Catatan Medis
                                                    </td>
                                                    <td class="rheader" style="background-color: #ffffff;">
                                                        Catatan Perawat
                                                    </td>
                                                    <td class="rheader" style="background-color: #ffffff;">
                                                        Catatan Unit Lainnya
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" class="rheader" style="background-color: #ffffff;">
                                                        <asp:DataGrid ID="grdCatatanMedis" runat="server" BorderWidth="0" GridLines="None"
                                                            Width="100%" CellPadding="2" CellSpacing="1" ShowHeader="false" ShowFooter="false"
                                                            AutoGenerateColumns="false">
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="gridHeaderStyle" />
                                                            <ItemStyle CssClass="gridItemStyle" />
                                                            <AlternatingItemStyle CssClass="gridAlternatingItemStyle" />
                                                            <PagerStyle Mode="NumericPages" HorizontalAlign="right" />
                                                            <Columns>
                                                                <asp:TemplateColumn runat="server" HeaderText="Informasi Kunjungan" ItemStyle-Width="250"
                                                                    ItemStyle-VerticalAlign="Top">
                                                                    <ItemTemplate>
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 50px; font-weight: bold;" valign="top">
                                                                                    Tanggal
                                                                                </td>
                                                                                <td valign="top" style="font-weight: bold;">
                                                                                    :&nbsp;<%# Format(DataBinder.Eval(Container.DataItem, "CreatedDate"), "dd-MMM-yyyy hh:mm")%></td>
                                                                                <td style="width: 150;" align="right">
                                                                                    <asp:Panel runat="server" ID="_pnlRevised" Visible='<%# DataBinder.Eval(Container.DataItem, "IsRevised")%>'>
                                                                                        <table cellpadding="2" cellspacing="1" style="background-color: Red;">
                                                                                            <tr>
                                                                                                <td style="color: White;">
                                                                                                    REVISED
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel runat="server" ID="_pnlCreated" Visible='<%# NOT DataBinder.Eval(Container.DataItem, "IsRevised")%>'>
                                                                                        <table cellpadding="2" cellspacing="1" style="background-color: Olive;">
                                                                                            <tr>
                                                                                                <td style="color: White;">
                                                                                                    CREATED
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 50px; font-weight: bold;" valign="top">
                                                                                        Unit
                                                                                </td>
                                                                                <td valign="top" style="font-weight: bold;" colspan="2">
                                                                                         :&nbsp;<%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 50px; font-weight: bold;" valign="top">
                                                                                    Dokter
                                                                                </td>
                                                                                <td valign="top" style="font-weight: bold;" colspan="2">
                                                                                    :&nbsp;<%# DataBinder.Eval(Container.DataItem, "PhysicianName")%></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 50px; font-weight: bold;" valign="top">
                                                                                    Asesmen
                                                                                </td>
                                                                                <td valign="top" style="font-weight: bold;" colspan="2">
                                                                                    :&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "AssessmentTypeName")%></b></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td valign="top" colspan="3">
                                                                                    <asp:TextBox ID="_txtSOAPNotes" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "SOAPNotes") %>'
                                                                                        Width="100%" Height="150" ReadOnly="true" Font-Names="Segoe UI" BorderStyle="None"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                    <td valign="top" class="rheader" style="background-color: #ffffff;">
                                                        <asp:DataGrid ID="grdCatatanPerawat" runat="server" BorderWidth="0" GridLines="None"
                                                            Width="100%" CellPadding="2" CellSpacing="1" ShowHeader="false" ShowFooter="false"
                                                            AutoGenerateColumns="false">
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="gridHeaderStyle" />
                                                            <ItemStyle CssClass="gridItemStyle" />
                                                            <AlternatingItemStyle CssClass="gridAlternatingItemStyle" />
                                                            <PagerStyle Mode="NumericPages" HorizontalAlign="right" />
                                                            <Columns>
                                                                <asp:TemplateColumn runat="server" HeaderText="Informasi Kunjungan" ItemStyle-Width="250"
                                                                    ItemStyle-VerticalAlign="Top">
                                                                    <ItemTemplate>
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 50px; font-weight: bold;" valign="top">
                                                                                    Tanggal
                                                                                </td>
                                                                                <td valign="top" style="font-weight: bold;">
                                                                                    :&nbsp;<%# Format(DataBinder.Eval(Container.DataItem, "CreatedDate"), "dd-MMM-yyyy hh:mm")%></td>
                                                                                <td style="width: 150;" align="right">
                                                                                    <asp:Panel runat="server" ID="_pnlRevised" Visible='<%# DataBinder.Eval(Container.DataItem, "IsRevised")%>'>
                                                                                        <table cellpadding="2" cellspacing="1" style="background-color: Red;">
                                                                                            <tr>
                                                                                                <td style="color: White;">
                                                                                                    REVISED
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel runat="server" ID="_pnlCreated" Visible='<%# NOT DataBinder.Eval(Container.DataItem, "IsRevised")%>'>
                                                                                        <table cellpadding="2" cellspacing="1" style="background-color: Olive;">
                                                                                            <tr>
                                                                                                <td style="color: White;">
                                                                                                    CREATED
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 50px; font-weight: bold;" valign="top">
                                                                                    Perawat
                                                                                </td>
                                                                                <td valign="top" style="font-weight: bold;">
                                                                                    :&nbsp;<%# DataBinder.Eval(Container.DataItem, "CreatedByUserName")%></td>
                                                                                <td valign="top" style="font-weight: bold; text-align: right;">
                                                                                    <asp:CheckBox ID="_chkIsPhysicianConfirmed" runat="server" Enabled="false" Text="Konfirmasi Dokter"
                                                                                        Checked='<%# DataBinder.Eval(Container.DataItem, "IsPhysicianConfirmed")%>' />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:TextBox ID="_txtNurseNotes" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "NurseNotes") %>'
                                                                                        Width="100%" Height="150" ReadOnly="true" Font-Names="Segoe UI" BorderStyle="None"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                    <td class="rheader" style="background-color: #ffffff;">
                                                        <asp:DataGrid ID="grdCatatanUnitLainnya" runat="server">
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <%--<div id="divRightPanelInformationContent">
        <div class="rightPanelContent borderBox">
            <asp:Button ID="btnRelatedLinkInfoTransaksiLab" Text="Go" runat="server" CssClass="sbttn" />
            <div class="qmmtitle">Info Transaksi dan Hasil Laboratorium</div>
            <div class="qmdescription">Informasi transaksi dan hasil laboratorium</div>
        </div>
        <div class="rightPanelContent borderBox">
            <asp:Button ID="btnRelatedLinkInfoTransaksiPenunjang" Text="Go" runat="server" CssClass="sbttn" />
            <div class="qmmtitle">Info Transaksi dan Hasil Penunjang Medis</div>
            <div class="qmdescription">Informasi transaksi dan hasil penunjang medis lainnya</div>
        </div>
    </div>--%>
    </form>
</body>
</html>
