﻿Option Strict Off
Option Explicit On

Imports System
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Math
Imports System.IO
Imports Microsoft.VisualBasic


Imports System.Data.SqlTypes

Namespace QIS.Web.EMR
    Public Class AssesmentPerawat
        Inherits PageBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region " Control Events "
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim timeMaskFormat = Common.Constants.MaskFormat.TIME_MASK
            txtJamDatang.Mask = timeMaskFormat
            txtJamLayan.Mask = timeMaskFormat
            txtJamCP.Mask = timeMaskFormat
            txtJamDisposisi.Mask = timeMaskFormat

            If Not Me.IsPostBack Then
                ReadQueryString()

                prepareDDL()
                PrepareScreen()
            End If
        End Sub

        Private Sub grdTodayPatient_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdTodayPatient.ItemCommand
            Select Case e.CommandName
                Case "GetRegistration"
                    Dim _lbtnRegistrationNo As LinkButton = CType(e.Item.FindControl("_lbtnRegistrationNo"), LinkButton)
                    Dim _lblTransactionNo As Label = CType(e.Item.FindControl("_lblTransactionNo"), Label)
                    Dim _lblPhysicianName As Label = CType(e.Item.FindControl("_lblPhysicianName"), Label)
                    Dim _lblPengkajianID As Label = CType(e.Item.FindControl("_lblPengkajianID"), Label)
                    lblPBRegistrationNo.Text = _lbtnRegistrationNo.Text.Trim
                    lblPBTransactionNo.Text = _lblTransactionNo.Text.Trim
                    lblPBPhysicianName.Text = _lblPhysicianName.Text.Trim
                    txtPengkajianID.Text = _lblPengkajianID.Text.Trim
                    OpenRegistration(ddlDepartmentFilter.SelectedValue.Trim, lblPBRegistrationNo.Text.Trim, lblPBTransactionNo.Text.Trim, _lblPengkajianID.Text.Trim)
            End Select
        End Sub

        Private Sub grdNurseAssessment_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdNurseAssessment.ItemCommand
            Select Case e.CommandName
                Case "Revise"
                    Dim _lblPengkajianID As Label = CType(e.Item.FindControl("_lblPengkajianID"), Label)
                    txtPengkajianID.Text = _lblPengkajianID.Text.Trim
                    If txtPengkajianID.Text = String.Empty Then txtPengkajianID.Text = "0"
                    OpenNurseAssesment(CInt(txtPengkajianID.Text.Trim))
            End Select
        End Sub

        Private Sub grdNurseNotes_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdNurseNotes.ItemCommand
            Select Case e.CommandName
                Case "Revise"
                    Dim _lblNurseNotesID As Label = CType(e.Item.FindControl("_lblNurseNotesID"), Label)
                    txtNurseNotesID.Text = _lblNurseNotesID.Text.Trim
                    If txtNurseNotesID.Text = String.Empty Then txtNurseNotesID.Text = "0"
                    OpenNurseNotes(CInt(txtNurseNotesID.Text.Trim))
            End Select
        End Sub

        Private Sub lbtnBack_Click(sender As Object, e As System.EventArgs) Handles lbtnBack.Click
            PrepareScreenNurseAssesment()
            pnlPatientList.Visible = True
            pnlPatientRecord.Visible = False
            UpdateViewGridTodayPatient(String.Empty, calTgl.selectedDate)
        End Sub

        Private Sub lbtnNewSOAP_Click(sender As Object, e As System.EventArgs) Handles lbtnNewSOAP.Click
            PrepareScreenNurseAssesment()
            commonFunction.Focus(Me, txtAsalInformasiNama.ClientID)
        End Sub

        Private Sub lbtnSaveSOAP_Click(sender As Object, e As System.EventArgs) Handles lbtnSaveSOAP.Click
            If txtPengkajianID.Text.Trim = String.Empty Or txtPengkajianID.Text = "0" Or IsNumeric(txtPengkajianID.Text.Trim) = False Then
                InsertNurseAssesment()
                'PrepareScreenNurseAssesment()
            Else
                UpdateNurseAssesment()
                'PrepareScreenNurseAssesment()
            End If
        End Sub

        Private Sub lbtnBackNotes_Click(sender As Object, e As System.EventArgs) Handles lbtnBackNotes.Click
            PrepareScreenNurseAssesment()
            pnlPatientList.Visible = True
            pnlPatientRecord.Visible = False
            UpdateViewGridTodayPatient(String.Empty, calTgl.selectedDate)
        End Sub

        Private Sub lbtnNewNotes_Click(sender As Object, e As System.EventArgs) Handles lbtnNewNotes.Click
            PrepareScreenNurseNotes()
            commonFunction.Focus(Me, txtNurseNotes.ClientID)
        End Sub

        Private Sub lbtnSaveNotes_Click(sender As Object, e As System.EventArgs) Handles lbtnSaveNotes.Click
            If txtNurseNotesID.Text.Trim = String.Empty Or txtNurseNotesID.Text = "0" Or IsNumeric(txtNurseNotesID.Text.Trim) = False Then
                InsertNurseNotes()
                PrepareScreenNurseNotes()
            Else
                UpdateNurseNotes()
                PrepareScreenNurseNotes()
            End If
        End Sub

        Private Sub btnSearchPatient_Click(sender As Object, e As System.EventArgs) Handles btnSearchPatient.Click
            UpdateViewGridTodayPatient(txtSearchPatient.Text.Trim, calTgl.selectedDate)
        End Sub

        Private Sub txtttvTinggiBadan_TextChanged(sender As Object, e As System.EventArgs) Handles txtttvTinggiBadan.TextChanged
            txtttvIndexMassaTubuh.Text = CountBMI(CDec(txtttvTinggiBadan.Text.Trim), CDec(txtttvBeratBadan.Text.Trim)).ToString.Trim
            commonFunction.Focus(Me, txtttvIndexMassaTubuh.ClientID)
        End Sub

        Private Sub txtttvBeratBadan_TextChanged(sender As Object, e As System.EventArgs) Handles txtttvBeratBadan.TextChanged
            txtttvIndexMassaTubuh.Text = CountBMI(CDec(txtttvTinggiBadan.Text.Trim), CDec(txtttvBeratBadan.Text.Trim)).ToString.Trim
            commonFunction.Focus(Me, txtttvIndexMassaTubuh.ClientID)
        End Sub

        Private Sub rblNyeriSkala_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblNyeriSkala.SelectedIndexChanged
            txtNyeriSkala.Text = rblNyeriSkala.SelectedValue.Trim
            commonFunction.Focus(Me, txtNyeriSkala.ClientID)
        End Sub

        Private Sub ddlSkorPerubahanBeratBadan_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSkorPerubahanBeratBadan.SelectedIndexChanged
            Dim intPenurunanBeratBadan As Integer = 0
            Dim oBR As New Common.BussinessRules.CommonCode
            With oBR
                .GroupCode = Common.Constants.GroupCode.NAStatusPerubahanBeratBadan_SCode
                .Code = ddlSkorPerubahanBeratBadan.SelectedValue.Trim
                If .SelectOne.Rows.Count > 0 Then
                    intPenurunanBeratBadan = CInt(.Value.Trim)
                End If
            End With
            oBR.Dispose()
            oBR = Nothing

            txtSkorNutrisi.Text = CountNutritionScore(intPenurunanBeratBadan, CInt(rblIsTidakNafsuMakan.SelectedValue.Trim)).ToString.Trim
            commonFunction.Focus(Me, txtSkorNutrisi.ClientID)
        End Sub

        Private Sub rblIsTidakNafsuMakan_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblIsTidakNafsuMakan.SelectedIndexChanged
            Dim intPenurunanBeratBadan As Integer = 0
            Dim oBR As New Common.BussinessRules.CommonCode
            With oBR
                .GroupCode = Common.Constants.GroupCode.NAStatusPerubahanBeratBadan_SCode
                .Code = ddlSkorPerubahanBeratBadan.SelectedValue.Trim
                If .SelectOne.Rows.Count > 0 Then
                    intPenurunanBeratBadan = CInt(.Value.Trim)
                End If
            End With
            oBR.Dispose()
            oBR = Nothing

            txtSkorNutrisi.Text = CountNutritionScore(intPenurunanBeratBadan, CInt(rblIsTidakNafsuMakan.SelectedValue.Trim)).ToString.Trim
            commonFunction.Focus(Me, txtSkorNutrisi.ClientID)
        End Sub

        Private Sub ddlDepartmentFilter_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDepartmentFilter.SelectedIndexChanged
            If ddlDepartmentFilter.SelectedValue = "EMERGENCY" Then
                _pnlTriage.Visible = True
            Else
                _pnlTriage.Visible = False
            End If
            commonFunction.SetDDL_Table(ddlServiceUnit, "ProfileUnit", MyBase.LoggedOnProfileID.Trim + "^" + ddlDepartmentFilter.SelectedValue.Trim)
            UpdateViewGridTodayPatient(txtSearchPatient.Text.Trim, calTgl.selectedDate)
        End Sub

        Private Sub ddlServiceUnit_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlServiceUnit.SelectedIndexChanged
            UpdateViewGridTodayPatient(txtSearchPatient.Text.Trim, calTgl.selectedDate)
        End Sub

        Private Sub RadTabStrip3_TabClick(sender As Object, e As Telerik.Web.UI.RadTabStripEventArgs) Handles RadTabStrip3.TabClick
            UpdateViewGridCatatanMedis()
            UpdateViewGridCatatanPerawat()
        End Sub

        Private Sub chkKeluar_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkKeluar.CheckedChanged
            If chkKeluar.Checked Then
                pnlDisposisi.Visible = True
            Else
                pnlDisposisi.Visible = False
            End If
        End Sub

        Private Sub ddlNyeriMetode_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlNyeriMetode.SelectedIndexChanged
            txtNyeriSkala.Text = String.Empty
            If ddlNyeriMetode.SelectedItem.Text = "FLACC" Then
                pnlWajah.Visible = True
                commonFunction.SetDDL_Table(ddlWajah, "CommonCode", "FLACCWAJAH")
                pnlGerakBawah.Visible = True
                commonFunction.SetDDL_Table(ddlGerakBawah, "CommonCode", "FLACCLE")
                commonFunction.SetDDL_Table(ddlAktivitas, "CommonCode", "FLACCAKTIVITAS")
                pnlTangis.Visible = True
                commonFunction.SetDDL_Table(ddlTangis, "CommonCode", "FLACCTANGIS")
                commonFunction.SetDDL_Table(ddlBicara, "CommonCode", "FLACCBICARA")
                pnlPola.Visible = False
                pnlGerakan.Visible = False
                pnlNRS.Visible = False
            ElseIf ddlNyeriMetode.SelectedItem.Text = "NIPS" Then
                pnlWajah.Visible = True
                commonFunction.SetDDL_Table(ddlWajah, "CommonCode", "NIPSWAJAH")
                pnlTangis.Visible = True
                commonFunction.SetDDL_Table(ddlTangis, "CommonCode", "NIPSTANGIS")
                pnlPola.Visible = True
                commonFunction.SetDDL_Table(ddlNapas, "CommonCode", "NIPSNAPAS")
                commonFunction.SetDDL_Table(ddlTungkai, "CommonCode", "NIPSTUNGKAI")
                commonFunction.SetDDL_Table(ddlSadar, "CommonCode", "NIPSSADAR")
                pnlGerakBawah.Visible = False
                pnlGerakan.Visible = False
                pnlNRS.Visible = False
            ElseIf ddlNyeriMetode.SelectedItem.Text = "Skala BPS" Then
                pnlWajah.Visible = True
                commonFunction.SetDDL_Table(ddlWajah, "CommonCode", "BPSWAJAH")
                pnlGerakan.Visible = True
                commonFunction.SetDDL_Table(ddlGerak, "CommonCode", "BPSGERAKATAS")
                commonFunction.SetDDL_Table(ddlKompensasi, "CommonCode", "BPSGERAKATAS")
                pnlGerakBawah.Visible = False
                pnlTangis.Visible = False
                pnlPola.Visible = False
                pnlNRS.Visible = False
            ElseIf ddlNyeriMetode.SelectedItem.Text = "NRS" Then
                pnlNRS.Visible = True
                commonFunction.SetDDL_Table(ddlNRS, "CommonCode", "NRS")
                pnlWajah.Visible = False
                pnlGerakBawah.Visible = False
                pnlTangis.Visible = False
                pnlPola.Visible = False
                pnlGerakan.Visible = False
            Else
                pnlWajah.Visible = False
                pnlGerakBawah.Visible = False
                pnlTangis.Visible = False
                pnlPola.Visible = False
                pnlGerakan.Visible = False
                pnlNRS.Visible = False
            End If
        End Sub

        Private Sub ddlWajah_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlWajah.SelectedIndexChanged, ddlGerakBawah.SelectedIndexChanged
            If txtNyeriSkala.Text = String.Empty Then
                txtNyeriSkala.Text = ddlWajah.SelectedValue.Trim
            Else
                txtNyeriSkala.Text = CStr(CDec(txtNyeriSkala.Text) + CDec(ddlWajah.SelectedValue))
            End If
        End Sub
        Private Sub dddlGerakBawah_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlGerakBawah.SelectedIndexChanged
            If txtNyeriSkala.Text = String.Empty Then
                txtNyeriSkala.Text = CDec(ddlGerakBawah.SelectedValue)
            Else
                txtNyeriSkala.Text = CStr(CDec(txtNyeriSkala.Text) + CDec(ddlGerakBawah.SelectedValue))
            End If
        End Sub
        Private Sub ddlAktivitas_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAktivitas.SelectedIndexChanged
            If txtNyeriSkala.Text = String.Empty Then
                txtNyeriSkala.Text = CDec(ddlAktivitas.SelectedValue)
            Else
                txtNyeriSkala.Text = CStr(CDec(txtNyeriSkala.Text) + CDec(ddlAktivitas.SelectedValue))
            End If
        End Sub
        Private Sub ddlTangis_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTangis.SelectedIndexChanged
            If txtNyeriSkala.Text = String.Empty Then
                txtNyeriSkala.Text = CDec(ddlTangis.SelectedValue)
            Else
                txtNyeriSkala.Text = CStr(CDec(txtNyeriSkala.Text) + CDec(ddlTangis.SelectedValue))
            End If
        End Sub
        Private Sub ddlBicara_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBicara.SelectedIndexChanged
            If txtNyeriSkala.Text = String.Empty Then
                txtNyeriSkala.Text = CDec(ddlBicara.SelectedValue)
            Else
                txtNyeriSkala.Text = CStr(CDec(txtNyeriSkala.Text) + CDec(ddlBicara.SelectedValue))
            End If
        End Sub
        Private Sub ddlNapas_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlNapas.SelectedIndexChanged
            If txtNyeriSkala.Text = String.Empty Then
                txtNyeriSkala.Text = CDec(ddlNapas.SelectedValue)
            Else
                txtNyeriSkala.Text = CStr(CDec(txtNyeriSkala.Text) + CDec(ddlNapas.SelectedValue))
            End If
        End Sub
        Private Sub ddlTungkai_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTungkai.SelectedIndexChanged
            If txtNyeriSkala.Text = String.Empty Then
                txtNyeriSkala.Text = CDec(ddlTungkai.SelectedValue)
            Else
                txtNyeriSkala.Text = CStr(CDec(txtNyeriSkala.Text) + CDec(ddlTungkai.SelectedValue))
            End If
        End Sub
        Private Sub ddlSadar_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSadar.SelectedIndexChanged
            If txtNyeriSkala.Text = String.Empty Then
                txtNyeriSkala.Text = CDec(ddlSadar.SelectedValue)
            Else
                txtNyeriSkala.Text = CStr(CDec(txtNyeriSkala.Text) + CDec(ddlSadar.SelectedValue))
            End If
        End Sub
        Private Sub ddlGerak_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlGerak.SelectedIndexChanged
            If txtNyeriSkala.Text = String.Empty Then
                txtNyeriSkala.Text = CDec(ddlGerak.SelectedValue)
            Else
                txtNyeriSkala.Text = CStr(CDec(txtNyeriSkala.Text) + CDec(ddlGerak.SelectedValue))
            End If
        End Sub
        Private Sub ddlKompensasi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlKompensasi.SelectedIndexChanged
            If txtNyeriSkala.Text = String.Empty Then
                txtNyeriSkala.Text = CDec(ddlKompensasi.SelectedValue)
            Else
                txtNyeriSkala.Text = CStr(CDec(txtNyeriSkala.Text) + CDec(ddlKompensasi.SelectedValue))
            End If
        End Sub
        Private Sub ddlNRS_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlNRS.SelectedIndexChanged
            If txtNyeriSkala.Text = String.Empty Then
                txtNyeriSkala.Text = CDec(ddlNRS.SelectedValue)
            Else
                txtNyeriSkala.Text = CStr(CDec(txtNyeriSkala.Text) + CDec(ddlNRS.SelectedValue))
            End If
        End Sub
#End Region

#Region " Private Functions "
        Private Function ReadQueryString() As Boolean

        End Function

        Private Sub prepareDDL()
            commonFunction.SetDDL_Table(ddlAssessmentType, "CommonCode", Common.Constants.GroupCode.EMRAssessmentType_SCode)
            commonFunction.SetDDL_Table(ddlAsalInformasi, "CommonCode", Common.Constants.GroupCode.NAAsalInformasi_SCode)
            commonFunction.SetDDL_Table(ddlAsalInformasiHubungan, "CommonCode", Common.Constants.GroupCode.NAHubungan_SCode)
            commonFunction.SetDDL_Table(ddlJenisAlergi, "CommonCode", Common.Constants.GroupCode.NAJenisAlergi_SCode)
            commonFunction.SetDDL_Table(ddlKesadaran, "StandardField", Common.Constants.StandardField.Kesadaran_SField)
            commonFunction.SetDDL_Table(ddlttvNadiDenyut, "CommonCode", Common.Constants.GroupCode.NADenyutNadi_SCode)
            commonFunction.SetDDL_Table(ddlNyeriMetode, "CommonCode", Common.Constants.GroupCode.NAMetodeNyeri_SCode)
            commonFunction.SetDDL_Table(ddlNyeriKualitas, "CommonCode", Common.Constants.GroupCode.NAKualitasNyeri_SCode)
            commonFunction.SetDDL_Table(ddlNyeriKarakteristik, "CommonCode", Common.Constants.GroupCode.NAKarakteristikNyeri_SCode)
            commonFunction.SetDDL_Table(ddlNyeriBerkurang, "CommonCode", Common.Constants.GroupCode.NANyeriBerkurang_SCode)
            commonFunction.SetDDL_Table(ddlNyeriBertambah, "CommonCode", Common.Constants.GroupCode.NANyeriBertambah_SCode)
            commonFunction.SetDDL_Table(ddlAktivitasMobilisasi, "CommonCode", Common.Constants.GroupCode.NAAktivitasMobilisasi_SCode)
            commonFunction.SetDDL_Table(ddlStatusPsikologi, "CommonCode", Common.Constants.GroupCode.NAStatusPsikologi_SCode)
            commonFunction.SetDDL_Table(ddlStatusMental, "CommonCode", Common.Constants.GroupCode.NAStatusMental_SCode)
            commonFunction.SetDDL_Table(ddlStatusPernikahan, "StandardField", Common.Constants.StandardField.StatusPernikahan_SField)
            commonFunction.SetDDL_Table(ddlStatusTempatTinggal, "CommonCode", Common.Constants.GroupCode.NAStatusTempatTinggal_SCode)
            commonFunction.SetDDL_Table(ddlPekerjaan, "StandardField", Common.Constants.StandardField.Pekerjaan_SField)
            commonFunction.SetDDL_Table(ddlPenjaminBayar, "StandardField", Common.Constants.StandardField.JenisInstansi_SField)
            commonFunction.SetDDL_Table(ddlStatusGizi, "CommonCode", Common.Constants.GroupCode.NAStatusGizi_SCode)
            commonFunction.SetDDL_Table(ddlSkorPerubahanBeratBadan, "CommonCodeCode", Common.Constants.GroupCode.NAStatusPerubahanBeratBadan_SCode)
            commonFunction.SetDDL_Table(ddlPendidikanPasien, "StandardField", Common.Constants.StandardField.Pendidikan_SField)
            commonFunction.SetDDL_Table(ddlBahasaPasien, "CommonCode", Common.Constants.GroupCode.NABahasa_SCode)
            commonFunction.SetDDL_Table(ddlAgamaPasien, "StandardField", Common.Constants.StandardField.Agama_SField)
            commonFunction.SetDDL_Table(ddlCaraBelajarDisukai, "CommonCode", Common.Constants.GroupCode.NACaraBelajar_SCode)
            commonFunction.SetDDL_Table(ddlRoomCode, "Room", String.Empty)
            commonFunction.SetDDL_Table(ddlServiceUnit, "ProfileUnit", MyBase.LoggedOnProfileID.Trim + "^" + ddlDepartmentFilter.SelectedValue.Trim)
            commonFunction.SetDDL_Table(ddlKdTriage, "StandardField", Common.Constants.StandardField.Triage_SField)
            commonFunction.SetDDL_Table(ddlKdnKlr, "StandardField", Common.Constants.StandardField.KeadaanKeluar_SField)
            commonFunction.SetDDL_Table(ddlCrKlr, "StandardField", Common.Constants.StandardField.CaraKeluar_SField)
        End Sub

        Private Sub PrepareScreen()
            calTgl.selectedDate = Date.Now
            pnlNurseAssesment.Visible = False
            pnlPatientList.Visible = True
            pnlPatientRecord.Visible = False
            txtSearchPatient.Text = String.Empty

            UpdateViewGridTodayPatient(String.Empty, calTgl.selectedDate)
        End Sub

        Private Sub PrepareScreenNurseAssesment()
            calTglDatang.selectedDate = Date.Now
            calTglLayan.selectedDate = Date.Now
            calTglCP.selectedDate = Date.Now
            txtJamDatang.Text = Format(Date.Now, commonFunction.FORMAT_TIME)
            txtJamLayan.Text = Format(Date.Now, commonFunction.FORMAT_TIME)
            txtJamCP.Text = Format(Date.Now, commonFunction.FORMAT_TIME)
            If ddlDepartmentFilter.SelectedValue = "EMERGENCY" Then
                _pnlTriage.Visible = True
            Else
                _pnlTriage.Visible = False
            End If
            ddlKdTriage.SelectedIndex = 0

            txtPengkajianID.Text = String.Empty
            ddlAsalInformasi.SelectedIndex = 0
            txtAsalInformasiNama.Text = String.Empty
            ddlAsalInformasiHubungan.SelectedIndex = 0
            txtKeluhanUtama.Text = String.Empty
            txtRiwayatKeluhanSaatIni.Text = String.Empty
            rblIsRiwayatAlergi.SelectedValue = "0"
            ddlJenisAlergi.SelectedIndex = 0
            txtReaksiAlergi.Text = String.Empty
            ddlKesadaran.SelectedIndex = 0
            txtttvTekananDarah.Text = "0"
            txtttvNadi.Text = "0"
            ddlttvNadiDenyut.SelectedIndex = 0
            txtttvSuhu.Text = "0"
            txtttvPernafasan.Text = "0"
            txtttvBeratBadan.Text = "0"
            txtttvTinggiBadan.Text = "0"
            txtttvIndexMassaTubuh.Text = "0"
            rblIsNyeri.SelectedValue = "0"
            txtNyeriLokasi.Text = String.Empty
            txtNyeriDurasi.Text = String.Empty
            txtNyeriSkala.Text = "0"
            rblNyeriSkala.SelectedValue = "0"
            ddlNyeriMetode.SelectedIndex = 0
            ddlNyeriKualitas.SelectedIndex = 0
            ddlNyeriKarakteristik.SelectedIndex = 0
            ddlNyeriBerkurang.SelectedIndex = 0
            ddlNyeriBertambah.SelectedIndex = 0
            txtNyeriBerkurangKeterangan.Text = String.Empty
            txtNyeriBertambahKeterangan.Text = String.Empty
            ddlAktivitasMobilisasi.SelectedIndex = 0
            txtKategoriResikoJatuh.Text = String.Empty
            rblIsDekubitas.SelectedValue = "0"
            ddlStatusPsikologi.SelectedIndex = 0
            txtStatusPsikologiKeterangan.Text = String.Empty
            ddlStatusMental.SelectedIndex = 0
            txtStatusMentalKeterangan.Text = String.Empty
            ddlStatusPernikahan.SelectedIndex = 0
            rblIsHubunganKeluargaBaik.SelectedValue = "0"
            ddlStatusTempatTinggal.SelectedIndex = 0
            rblIsBeribadahTeratur.SelectedValue = "0"
            ddlPekerjaan.SelectedIndex = 0
            ddlPenjaminBayar.SelectedIndex = 0
            ddlStatusGizi.SelectedIndex = 0
            ddlSkorPerubahanBeratBadan.SelectedIndex = 0
            rblIsTidakNafsuMakan.SelectedValue = "0"
            txtSkorNutrisi.Text = "0"
            rblIsPerluKonsultasiGizi.SelectedValue = "0"
            rblIsHamil.SelectedValue = "0"
            txtHamilGravidKeterangan.Text = String.Empty
            txtHamilParaKeterangan.Text = String.Empty
            txtHamilAbortusKeterangan.Text = String.Empty
            rblIsBersediaMenerimaInformasi.SelectedValue = "0"
            rblIsHambatanEdukasi.SelectedValue = "0"
            chkIsHambatanPendengaran.Checked = False
            chkIsHambatanPenglihatan.Checked = False
            chkIsHambatanKognitif.Checked = False
            chkIsHambatanFisik.Checked = False
            chkIsHambatanBudaya.Checked = False
            chkIsHambatanEmosi.Checked = False
            chkIsHambatanBahasa.Checked = False
            txtHambatanLainnya.Text = String.Empty
            ddlPendidikanPasien.SelectedIndex = 0
            ddlBahasaPasien.SelectedIndex = 0
            txtBahasaLainnya.Text = String.Empty
            ddlAgamaPasien.SelectedIndex = 0
            txtBudayaPasien.Text = String.Empty
            txtPerluPenterjemah.Text = String.Empty
            ddlCaraBelajarDisukai.SelectedIndex = 0
            chkIsDiagnosa.Checked = False
            chkIsNyeri.Checked = False
            chkIsDietNutrisi.Checked = False
            chkIsAlatMedis.Checked = False
            chkIsTerapi.Checked = False
            txtInformasiDiinginkan.Text = String.Empty
            chkIsKIE.Checked = False
            chkIsObatPulang.Checked = False
            chkIsFotoRadiologi.Checked = False
            chkIsLaboratorium.Checked = False
            chkIsEKG.Checked = False
            chkIsKIEAPS.Checked = False
            chkIsTTDAPS.Checked = False
            ddlRoomCode.SelectedIndex = 0
            txtReferToHospitalName.Text = String.Empty
            rblIsDeath.SelectedValue = "0"
            txtDeathTime.Text = String.Empty

            chkKeluar.Checked = False
            pnlDisposisi.Visible = False

            calTglDisposisi.selectedDate = Date.Now
            txtJamDisposisi.Text = Format(Date.Now, commonFunction.FORMAT_TIME)
            ddlKdnKlr.SelectedIndex = 0
            ddlCrKlr.SelectedIndex = 0

            CheckFirstAssessmentByRegistrationNo()
        End Sub

        Private Sub PrepareScreenNurseNotes()
            calTglCP.selectedDate = Date.Now
            txtJamCP.Text = Format(Date.Now, commonFunction.FORMAT_TIME)
            txtNurseNotesID.Text = String.Empty
            txtNurseNotes.Text = String.Empty
            chkIsPhysicianConfirmed.Checked = False
            UpdateViewGridNurseNotes()
        End Sub

        Private Function CountBMI(ByVal decTinggiBadan As Decimal, ByVal decBeratBadan As Decimal) As Decimal
            Dim decBMI As Decimal = 0
            If decTinggiBadan > 0 And decBeratBadan > 0 Then
                decBMI = Round((decBeratBadan / ((decTinggiBadan / 100) * (decTinggiBadan / 100))), 2)
            End If
            Return decBMI
        End Function

        Private Function CountNutritionScore(ByVal intPenurunanBeratBadan As Integer, ByVal intTidakNafsuMakan As Integer) As Integer
            Dim intNutritionScore As Integer = 0
            intNutritionScore = intPenurunanBeratBadan + intTidakNafsuMakan
            If intNutritionScore > 2 Then rblIsPerluKonsultasiGizi.SelectedValue = "1"
            Return intNutritionScore
        End Function

        Private Sub UpdateViewGridTodayPatient(ByVal strSearch As String, ByVal Tanggal As Date)
            Dim dt As New DataTable
            Dim oBR As New Common.BussinessRules.EMR
            With oBR
                .DepartmentID = ddlDepartmentFilter.SelectedValue.Trim
                dt = .GetPatientTodayByUnit(strSearch, ddlServiceUnit.SelectedValue.Trim, Tanggal)

                grdTodayPatient.DataSource = dt
                grdTodayPatient.DataBind()
            End With
            oBR.Dispose()
            oBR = Nothing
        End Sub

        Private Sub UpdateViewGridNurseAssessment()
            Dim oBr As New Common.BussinessRules.NurseAssesment
            With oBr
                .RegistrationNo = lblPBRegistrationNo.Text.Trim
                grdNurseAssessment.DataSource = .GetNurseAssesmentByRegistrationNo()
                grdNurseAssessment.DataBind()
            End With
            oBr.Dispose()
            oBr = Nothing
        End Sub

        Private Sub UpdateViewGridNurseNotes()
            Dim oBr As New Common.BussinessRules.EMR
            With oBr
                .RegistrationNo = lblPBRegistrationNo.Text.Trim
                grdNurseNotes.DataSource = .GetNurseNotesByRegistrationNo()
                grdNurseNotes.DataBind()
            End With
            oBr.Dispose()
            oBr = Nothing
        End Sub

        Private Sub UpdateViewGridCatatanMedis()
            Dim oBR As New Common.BussinessRules.EMR
            With oBR
                .RegistrationNo = lblPBRegistrationNo.Text.Trim
                grdCatatanMedis.DataSource = .GetPatientResumeByRegistrationNo()
                grdCatatanMedis.DataBind()
            End With
            oBR.Dispose()
            oBR = Nothing
        End Sub

        Private Sub UpdateViewGridCatatanPerawat()
            Dim oBR As New Common.BussinessRules.EMR
            With oBR
                .RegistrationNo = lblPBRegistrationNo.Text.Trim
                grdCatatanPerawat.DataSource = .GetNurseNotesByRegistrationNo()
                grdCatatanPerawat.DataBind()
            End With
            oBR.Dispose()
            oBR = Nothing
        End Sub

        Private Function CheckFirstAssessmentByRegistrationNo() As Boolean
            Dim bolFA As Boolean = False
            Dim oBr As New Common.BussinessRules.NurseAssesment
            With oBr
                .RegistrationNo = lblPBRegistrationNo.Text.Trim
                If .GetNurseAssessmentFirstAssessmentByRegistrationNo.Rows.Count > 0 Then
                    ddlAssessmentType.SelectedIndex = 1
                    bolFA = True
                Else
                    ddlAssessmentType.SelectedIndex = 0
                    bolFA = False
                End If
            End With
            oBr.Dispose()
            oBr = Nothing

            Return bolFA
        End Function
#End Region

#Region " C,R,U,D "
        Private Function IsUserPhysician(ByVal strUserID As String) As Boolean
            Dim bolIsPhysician As Boolean = False
            Dim oBR As New Common.BussinessRules.User
            With oBR
                .UserID = strUserID.Trim
                If .SelectOne.Rows.Count > 0 Then
                    bolIsPhysician = .isPhysician
                Else
                    bolIsPhysician = False
                End If
            End With
            oBR.Dispose()
            oBR = Nothing

            Return bolIsPhysician
        End Function

        Private Sub OpenRegistration(ByVal DepartmentID As String, ByVal RegistrationNo As String, ByVal TransactionNo As String, ByVal PengkajianID As String)
            Dim oBR As New Common.BussinessRules.EMR
            With oBR
                .DepartmentID = DepartmentID.Trim
                .RegistrationNo = RegistrationNo.Trim
                .TransactionNo = TransactionNo.Trim
                If .GetRegistration.Rows.Count > 0 Then
                    pnlPatientList.Visible = False
                    pnlPatientRecord.Visible = True
                    lblPBMRN.Text = .MRN.Trim
                    lblPBPatientName.Text = .PatientName.Trim
                    lblPBPatientGender.Text = .PatientGender.Trim
                    lblPBPatientDOB.Text = Format(.PatientDOB, commonFunction.FORMAT_DATE)
                    lblPBRegistrationNo.Text = .RegistrationNo.Trim
                    lblPBTransactionNo.Text = .TransactionNo.Trim
                    lblPBRegistrationDate.Text = Format(.RegistrationDate, commonFunction.FORMAT_DATE)
                    lblPBRegistrationTime.Text = .RegistrationTime.Trim
                    lblPBServiceUnitID.Text = .ServiceUnitID.Trim
                    lblPBServiceUnitName.Text = .ServiceUnitName.Trim
                    lblPBBusinessPartnerName.Text = .BusinessPartnerName.Trim
                    If lblPBPatientGender.Text = "P" Then
                        imgPBPatient.ImageUrl = "/qistoollib/images/person-female.png"
                    Else
                        imgPBPatient.ImageUrl = "/qistoollib/images/person-male.png"
                    End If
                    pnlNurseAssesment.Visible = True
                    'OpenNurseAssesment(CInt(PengkajianID))
                Else
                    pnlPatientList.Visible = True
                    pnlPatientRecord.Visible = False
                    lblPBMRN.Text = String.Empty
                    lblPBPatientName.Text = String.Empty
                    lblPBPatientGender.Text = String.Empty
                    lblPBPatientDOB.Text = String.Empty
                    lblPBRegistrationNo.Text = String.Empty
                    lblPBTransactionNo.Text = String.Empty
                    lblPBRegistrationDate.Text = String.Empty
                    lblPBRegistrationTime.Text = String.Empty
                    lblPBServiceUnitID.Text = String.Empty
                    lblPBServiceUnitName.Text = String.Empty
                    lblPBBusinessPartnerName.Text = String.Empty
                    pnlNurseAssesment.Visible = False                    
                End If
            End With
            oBR.Dispose()
            oBR = Nothing

            PrepareScreenNurseAssesment()
            UpdateViewGridNurseAssessment()
            UpdateViewGridNurseNotes()
            UpdateViewGridCatatanMedis()
            UpdateViewGridCatatanPerawat()
        End Sub

        Private Sub OpenNurseAssesment(ByVal PengkajianID As Integer)
            Dim oBR As New Common.BussinessRules.NurseAssesment
            With oBR
                .PengkajianID = PengkajianID
                If .GetNurseAssesmentByID.Rows.Count > 0 Then
                    If .DepartmentID = "EMERGENCY" Then
                        ddlKdTriage.SelectedValue = .Triage.Trim
                        calTglDatang.selectedDate = .TglDatang
                        calTglLayan.selectedDate = .TglLayan
                        txtJamDatang.Text = .JamDatang
                        txtJamLayan.Text = .JamLayan
                    End If
                    ddlAssessmentType.SelectedValue = .AssessmentTypeSCode.Trim
                    ddlAsalInformasi.SelectedValue = .GCAsalInformasi.Trim
                    txtAsalInformasiNama.Text = .AsalInformasiNama.Trim
                    ddlAsalInformasiHubungan.SelectedValue = .GCAsalInformasiHubungan.Trim
                    txtKeluhanUtama.Text = .KeluhanUtama.Trim
                    txtRiwayatKeluhanSaatIni.Text = .RiwayatKeluhanSaatIni.Trim
                    If .IsRiwayatAlergi.ToString.Trim = "true" Then
                        rblIsRiwayatAlergi.SelectedValue = "1"
                    Else
                        rblIsRiwayatAlergi.SelectedValue = "0"
                    End If
                    ddlJenisAlergi.SelectedValue = .GCJenisAlergi.Trim
                    txtReaksiAlergi.Text = .ReaksiAlergi.Trim
                    ddlKesadaran.SelectedValue = .GCKesadaran.Trim
                    txtttvTekananDarah.Text = .ttvTekananDarah.Trim
                    txtttvNadi.Text = .ttvNadi.ToString.Trim
                    ddlttvNadiDenyut.SelectedValue = .GCttvNadiDenyut.Trim
                    txtttvSuhu.Text = .ttvSuhu.ToString.Trim
                    txtttvPernafasan.Text = .ttvPernafasan.ToString.Trim
                    txtttvBeratBadan.Text = .ttvBeratBadan.ToString.Trim
                    txtttvTinggiBadan.Text = .ttvTinggiBadan.ToString.Trim
                    txtttvIndexMassaTubuh.Text = .ttvIndexMasaTubuh.ToString.Trim
                    If .IsNyeri.ToString.Trim = "true" Then
                        rblIsNyeri.SelectedValue = "1"
                    Else
                        rblIsNyeri.SelectedValue = "0"
                    End If
                    txtNyeriLokasi.Text = .NyeriLokasi.Trim
                    txtNyeriDurasi.Text = .NyeriDurasi.Trim
                    txtNyeriSkala.Text = .NyeriSkala.Trim
                    rblNyeriSkala.SelectedValue = .NyeriSkalaNum.ToString.Trim
                    ddlNyeriMetode.SelectedValue = .GCNyeriMetode.Trim
                    ddlNyeriKualitas.SelectedValue = .GCNyeriKualitas.Trim
                    ddlNyeriKarakteristik.SelectedValue = .GCNyeriKarakteristik.Trim
                    ddlNyeriBerkurang.SelectedValue = .GCNyeriBerkurang.Trim
                    ddlNyeriBertambah.SelectedValue = .GCNyeriBertambah.Trim
                    txtNyeriBerkurangKeterangan.Text = .NyeriBerkurangKeterangan.Trim
                    txtNyeriBertambahKeterangan.Text = .NyeriBertambahKeterangan.Trim
                    ddlAktivitasMobilisasi.SelectedValue = .GCAktivitasMobilisasi.Trim
                    txtKategoriResikoJatuh.Text = .KategoriResikoJatuh.Trim
                    If .IsResikoDekubitas.ToString.Trim = "true" Then
                        rblIsDekubitas.SelectedValue = "1"
                    Else
                        rblIsDekubitas.SelectedValue = "0"
                    End If
                    ddlStatusPsikologi.SelectedValue = .GCStatusPsikologi.Trim
                    txtStatusPsikologiKeterangan.Text = .StatusPsikologiKeterangan.Trim
                    ddlStatusMental.SelectedValue = .GCStatusMental.Trim
                    txtStatusMentalKeterangan.Text = .StatusMentalKeterangan.Trim
                    ddlStatusPernikahan.SelectedValue = .GCStatusPernikahan.Trim
                    If .IsHubunganKeluargaPasienBaik.ToString.Trim = "true" Then
                        rblIsHubunganKeluargaBaik.SelectedValue = "1"
                    Else
                        rblIsHubunganKeluargaBaik.SelectedValue = "0"
                    End If
                    ddlStatusTempatTinggal.SelectedValue = .GCStatusTempatTinggal.Trim
                    If .IsBeribadahTeratur.ToString.Trim = "true" Then
                        rblIsBeribadahTeratur.SelectedValue = "1"
                    Else
                        rblIsBeribadahTeratur.SelectedValue = "0"
                    End If
                    ddlPekerjaan.SelectedValue = .GCPekerjaan.Trim
                    ddlPenjaminBayar.SelectedValue = .GCPenjaminBayar.Trim
                    ddlStatusGizi.SelectedValue = .GCStatusGizi.Trim
                    ddlSkorPerubahanBeratBadan.SelectedValue = .GCSkorPerubahanBeratBadan.Trim
                    If .IsTidakNafsuMakan.ToString.Trim = "true" Then
                        rblIsTidakNafsuMakan.SelectedValue = "1"
                    Else
                        rblIsTidakNafsuMakan.SelectedValue = "0"
                    End If
                    txtSkorNutrisi.Text = .SkorNutrisi.ToString.Trim
                    If .IsPerluKonsultasiGizi.ToString.Trim = "true" Then
                        rblIsPerluKonsultasiGizi.SelectedValue = "1"
                    Else
                        rblIsPerluKonsultasiGizi.SelectedValue = "0"
                    End If
                    If .IsHamil.ToString.Trim = "true" Then
                        rblIsHamil.SelectedValue = "1"
                    Else
                        rblIsHamil.SelectedValue = "0"
                    End If
                    txtHamilGravidKeterangan.Text = .HamilGravidKeterangan.Trim
                    txtHamilParaKeterangan.Text = .HamilParaKeterangan.Trim
                    txtHamilAbortusKeterangan.Text = .HamilAbortusKeterangan.Trim
                    If .IsBersediaMenerimaInformasi.ToString.Trim = "true" Then
                        rblIsBersediaMenerimaInformasi.SelectedValue = "1"
                    Else
                        rblIsBersediaMenerimaInformasi.SelectedValue = "0"
                    End If
                    If .IsHambatanEdukasi.ToString.Trim = "true" Then
                        rblIsHambatanEdukasi.SelectedValue = "1"
                    Else
                        rblIsHambatanEdukasi.SelectedValue = "0"
                    End If
                    chkIsHambatanPendengaran.Checked = .IsHambatanPendengaran
                    chkIsHambatanPenglihatan.Checked = .IsHambatanPenglihatan
                    chkIsHambatanKognitif.Checked = .IsHambatanKognitif
                    chkIsHambatanFisik.Checked = .IsHambatanFisik
                    chkIsHambatanBudaya.Checked = .IsHambatanBudaya
                    chkIsHambatanEmosi.Checked = .IsHambatanEmosi
                    chkIsHambatanBahasa.Checked = .IsHambatanBahasa
                    txtHambatanLainnya.Text = .HambatanLainnya.Trim
                    ddlPendidikanPasien.SelectedValue = .GCPendidikanPasien.Trim
                    ddlBahasaPasien.SelectedValue = .GCBahasaPasien.Trim
                    txtBahasaLainnya.Text = .BahasaLainnya.Trim
                    ddlAgamaPasien.SelectedValue = .GCAgamaPasien.Trim
                    txtBudayaPasien.Text = .BudayaPasien.Trim
                    txtPerluPenterjemah.Text = .PerluPenterjemah.Trim
                    ddlCaraBelajarDisukai.SelectedValue = .GCCaraBelajarDisukai.Trim
                    chkIsDiagnosa.Checked = .isInformasiDiagnosa
                    chkIsNyeri.Checked = .isInformasiNyeri
                    chkIsDietNutrisi.Checked = .isInformasiDietNutrisi
                    chkIsAlatMedis.Checked = .isInformasiAlatMedis
                    chkIsTerapi.Checked = .isInformasiTerapi
                    txtInformasiDiinginkan.Text = .InformasiDiinginkanKeteragan.Trim
                    chkIsKIE.Checked = .IsKIE
                    chkIsObatPulang.Checked = .IsObatPulang
                    chkIsFotoRadiologi.Checked = .IsFotoRadiologi
                    chkIsLaboratorium.Checked = .IsLaboratorium
                    chkIsEKG.Checked = .IsEKG
                    chkIsKIEAPS.Checked = .IsKIEAPS
                    chkIsTTDAPS.Checked = .IsTTDAPS
                    ddlRoomCode.SelectedValue = .RoomCode.Trim
                    txtReferToHospitalName.Text = .ReferToHospitalName.Trim
                    If .IsDeath.ToString.Trim = "true" Then
                        rblIsDeath.SelectedValue = "1"
                    Else
                        rblIsDeath.SelectedValue = "0"
                    End If
                    txtDeathTime.Text = .DeathTime.Trim
                    calTglDisposisi.selectedDate = .TglDisposisi
                    txtJamDisposisi.Text = .JamDisposisi.Trim
                    ddlKdnKlr.SelectedItem.Value = .KeadaanKeluar.Trim
                    ddlCrKlr.SelectedItem.Value = .CaraKeluar.Trim
                Else
                    calTglDisposisi.selectedDate = Date.Now
                    txtJamDisposisi.Text = Format(Date.Now, commonFunction.FORMAT_TIME)
                    ddlKdnKlr.SelectedIndex = 0
                    ddlCrKlr.SelectedIndex = 0
                    ddlAsalInformasi.SelectedIndex = 0
                    txtAsalInformasiNama.Text = String.Empty
                    ddlAsalInformasiHubungan.SelectedIndex = 0
                    txtKeluhanUtama.Text = String.Empty
                    txtRiwayatKeluhanSaatIni.Text = String.Empty
                    rblIsRiwayatAlergi.SelectedValue = "0"
                    ddlJenisAlergi.SelectedIndex = 0
                    txtReaksiAlergi.Text = String.Empty
                    ddlKesadaran.SelectedIndex = 0
                    txtttvTekananDarah.Text = "0"
                    txtttvNadi.Text = "0"
                    ddlttvNadiDenyut.SelectedIndex = 0
                    txtttvSuhu.Text = "0"
                    txtttvPernafasan.Text = "0"
                    txtttvBeratBadan.Text = "0"
                    txtttvTinggiBadan.Text = "0"
                    txtttvIndexMassaTubuh.Text = "0"
                    rblIsNyeri.SelectedValue = "0"
                    txtNyeriLokasi.Text = String.Empty
                    txtNyeriDurasi.Text = String.Empty
                    txtNyeriSkala.Text = "0"
                    rblNyeriSkala.SelectedValue = "0"
                    ddlNyeriMetode.SelectedIndex = 0
                    ddlNyeriKualitas.SelectedIndex = 0
                    ddlNyeriKarakteristik.SelectedIndex = 0
                    ddlNyeriBerkurang.SelectedIndex = 0
                    ddlNyeriBertambah.SelectedIndex = 0
                    txtNyeriBerkurangKeterangan.Text = String.Empty
                    txtNyeriBertambahKeterangan.Text = String.Empty
                    ddlAktivitasMobilisasi.SelectedIndex = 0
                    txtKategoriResikoJatuh.Text = String.Empty
                    rblIsDekubitas.SelectedValue = "0"
                    ddlStatusPsikologi.SelectedIndex = 0
                    txtStatusPsikologiKeterangan.Text = String.Empty
                    ddlStatusMental.SelectedIndex = 0
                    txtStatusMentalKeterangan.Text = String.Empty
                    ddlStatusPernikahan.SelectedIndex = 0
                    rblIsHubunganKeluargaBaik.SelectedValue = "0"
                    ddlStatusTempatTinggal.SelectedIndex = 0
                    rblIsBeribadahTeratur.SelectedValue = "0"
                    ddlPekerjaan.SelectedIndex = 0
                    ddlPenjaminBayar.SelectedIndex = 0
                    ddlStatusGizi.SelectedIndex = 0
                    ddlSkorPerubahanBeratBadan.SelectedIndex = 0
                    rblIsTidakNafsuMakan.SelectedValue = "0"
                    txtSkorNutrisi.Text = "0"
                    rblIsPerluKonsultasiGizi.SelectedValue = "0"
                    rblIsHamil.SelectedValue = "0"
                    txtHamilGravidKeterangan.Text = String.Empty
                    txtHamilParaKeterangan.Text = String.Empty
                    txtHamilAbortusKeterangan.Text = String.Empty
                    rblIsBersediaMenerimaInformasi.SelectedValue = "0"
                    rblIsHambatanEdukasi.SelectedValue = "0"
                    chkIsHambatanPendengaran.Checked = False
                    chkIsHambatanPenglihatan.Checked = False
                    chkIsHambatanKognitif.Checked = False
                    chkIsHambatanFisik.Checked = False
                    chkIsHambatanBudaya.Checked = False
                    chkIsHambatanEmosi.Checked = False
                    chkIsHambatanBahasa.Checked = False
                    txtHambatanLainnya.Text = String.Empty
                    ddlPendidikanPasien.SelectedIndex = 0
                    ddlBahasaPasien.SelectedIndex = 0
                    txtBahasaLainnya.Text = String.Empty
                    ddlAgamaPasien.SelectedIndex = 0
                    txtBudayaPasien.Text = String.Empty
                    txtPerluPenterjemah.Text = String.Empty
                    ddlCaraBelajarDisukai.SelectedIndex = 0
                    chkIsDiagnosa.Checked = False
                    chkIsNyeri.Checked = False
                    chkIsDietNutrisi.Checked = False
                    chkIsAlatMedis.Checked = False
                    chkIsTerapi.Checked = False
                    txtInformasiDiinginkan.Text = String.Empty
                    chkIsKIE.Checked = False
                    chkIsObatPulang.Checked = False
                    chkIsFotoRadiologi.Checked = False
                    chkIsLaboratorium.Checked = False
                    chkIsEKG.Checked = False
                    chkIsKIEAPS.Checked = False
                    chkIsTTDAPS.Checked = False
                    ddlRoomCode.SelectedIndex = 0
                    txtReferToHospitalName.Text = String.Empty
                    rblIsDeath.SelectedValue = "0"
                    txtDeathTime.Text = String.Empty
                End If
            End With
            oBR.Dispose()
            oBR = Nothing
        End Sub

        Private Sub InsertNurseAssesment()
            If CheckFirstAssessmentByRegistrationNo() = True And ddlAssessmentType.SelectedValue = "01" Then
                commonFunction.MsgBox(Me, "Sudah ada asesmen Awal. Silahkan lanjutkan dengan memilih tipe asesmen Lanjutan.")
                Exit Sub
            End If
            Dim oBR As New Common.BussinessRules.NurseAssesment
            With oBR
                .DepartmentID = ddlDepartmentFilter.SelectedValue.Trim
                .RegistrationNo = lblPBRegistrationNo.Text.Trim
                .GCAsalInformasi = ddlAsalInformasi.SelectedValue.Trim
                .AsalInformasiNama = txtAsalInformasiNama.Text.Trim
                .GCAsalInformasiHubungan = ddlAsalInformasiHubungan.SelectedValue.Trim
                .Triage = ddlKdTriage.SelectedValue.Trim
                .AssessmentTypeSCode = ddlAssessmentType.SelectedValue.Trim
                .KeluhanUtama = txtKeluhanUtama.Text.Trim
                .RiwayatKeluhanSaatIni = txtRiwayatKeluhanSaatIni.Text.Trim
                .IsRiwayatAlergi = CBool(rblIsRiwayatAlergi.SelectedValue.Trim)
                .GCJenisAlergi = ddlJenisAlergi.SelectedValue.Trim
                .ReaksiAlergi = txtReaksiAlergi.Text.Trim
                .GCKesadaran = ddlKesadaran.SelectedValue.Trim
                .ttvTekananDarah = txtttvTekananDarah.Text.Trim 'CDec(IIf(IsNumeric(txtttvTekananDarah.Text.Trim) = True, CDec(txtttvTekananDarah.Text.Trim), 0).ToString.Trim)
                .ttvNadi = CDec(IIf(IsNumeric(txtttvNadi.Text.Trim) = True, CDec(txtttvNadi.Text.Trim), 0).ToString.Trim)
                .GCttvNadiDenyut = ddlttvNadiDenyut.SelectedValue.Trim
                .ttvSuhu = CDec(IIf(IsNumeric(txtttvSuhu.Text.Trim) = True, CDec(txtttvSuhu.Text.Trim), 0).ToString.Trim)
                .ttvPernafasan = CDec(IIf(IsNumeric(txtttvPernafasan.Text.Trim) = True, CDec(txtttvPernafasan.Text.Trim), 0).ToString.Trim)
                .ttvBeratBadan = CDec(IIf(IsNumeric(txtttvBeratBadan.Text.Trim) = True, CDec(txtttvBeratBadan.Text.Trim), 0).ToString.Trim)
                .ttvTinggiBadan = CDec(IIf(IsNumeric(txtttvTinggiBadan.Text.Trim) = True, CDec(txtttvTinggiBadan.Text.Trim), 0).ToString.Trim)
                .ttvIndexMasaTubuh = CDec(IIf(IsNumeric(txtttvIndexMassaTubuh.Text.Trim) = True, CDec(txtttvIndexMassaTubuh.Text.Trim), 0).ToString.Trim)
                .IsNyeri = CBool(rblIsNyeri.SelectedValue.Trim)
                .NyeriLokasi = txtNyeriLokasi.Text.Trim
                .NyeriDurasi = txtNyeriDurasi.Text.Trim
                .NyeriSkala = txtNyeriSkala.Text.Trim
                .NyeriSkalaNum = CInt(rblNyeriSkala.SelectedValue.Trim)
                .GCNyeriMetode = ddlNyeriMetode.SelectedValue.Trim
                .GCNyeriKualitas = ddlNyeriKualitas.SelectedValue.Trim
                .GCNyeriKarakteristik = ddlNyeriKarakteristik.SelectedValue.Trim
                .GCNyeriBerkurang = ddlNyeriBerkurang.SelectedValue.Trim
                .GCNyeriBertambah = ddlNyeriBertambah.SelectedValue.Trim
                .NyeriBerkurangKeterangan = txtNyeriBerkurangKeterangan.Text.Trim
                .NyeriBertambahKeterangan = txtNyeriBertambahKeterangan.Text.Trim
                .GCAktivitasMobilisasi = ddlAktivitasMobilisasi.SelectedValue.Trim
                .KategoriResikoJatuh = txtKategoriResikoJatuh.Text.Trim
                .IsResikoDekubitas = CBool(rblIsDekubitas.SelectedValue.Trim)
                .GCStatusPsikologi = ddlStatusPsikologi.SelectedValue.Trim
                .StatusPsikologiKeterangan = txtStatusPsikologiKeterangan.Text.Trim
                .GCStatusMental = ddlStatusMental.SelectedValue.Trim
                .StatusMentalKeterangan = txtStatusMentalKeterangan.Text.Trim
                .GCStatusPernikahan = ddlStatusPernikahan.SelectedValue.Trim
                .IsHubunganKeluargaPasienBaik = CBool(rblIsHubunganKeluargaBaik.SelectedValue.Trim)
                .GCStatusTempatTinggal = ddlStatusTempatTinggal.SelectedValue.Trim
                .IsBeribadahTeratur = CBool(rblIsBeribadahTeratur.SelectedValue.Trim)
                .GCPekerjaan = ddlPekerjaan.SelectedValue.Trim
                .GCPenjaminBayar = ddlPenjaminBayar.SelectedValue.Trim
                .PenjaminBayarCode = String.Empty
                .GCStatusGizi = ddlStatusGizi.SelectedValue.Trim
                .GCSkorPerubahanBeratBadan = ddlSkorPerubahanBeratBadan.SelectedValue.Trim
                .IsTidakNafsuMakan = CBool(rblIsTidakNafsuMakan.SelectedValue.Trim)
                .SkorNutrisi = CInt(IIf(IsNumeric(txtSkorNutrisi.Text.Trim) = True, CInt(txtSkorNutrisi.Text.Trim), 0).ToString.Trim)
                .IsPerluKonsultasiGizi = CBool(rblIsPerluKonsultasiGizi.SelectedValue.Trim)
                .IsHamil = CBool(rblIsHamil.SelectedValue.Trim)
                .HamilGravidKeterangan = txtHamilGravidKeterangan.Text.Trim
                .HamilParaKeterangan = txtHamilParaKeterangan.Text.Trim
                .HamilAbortusKeterangan = txtHamilAbortusKeterangan.Text.Trim
                .IsBersediaMenerimaInformasi = CBool(rblIsBersediaMenerimaInformasi.SelectedValue.Trim)
                .IsHambatanEdukasi = CBool(rblIsHambatanEdukasi.SelectedValue.Trim)
                .IsHambatanPendengaran = chkIsHambatanPendengaran.Checked
                .IsHambatanPenglihatan = chkIsHambatanPenglihatan.Checked
                .IsHambatanKognitif = chkIsHambatanKognitif.Checked
                .IsHambatanFisik = chkIsHambatanFisik.Checked
                .IsHambatanBudaya = chkIsHambatanBudaya.Checked
                .IsHambatanEmosi = chkIsHambatanEmosi.Checked
                .IsHambatanBahasa = chkIsHambatanBahasa.Checked
                .HambatanLainnya = txtHambatanLainnya.Text.Trim
                .GCPendidikanPasien = ddlPendidikanPasien.SelectedValue.Trim
                .GCBahasaPasien = ddlBahasaPasien.SelectedValue.Trim
                .BahasaLainnya = txtBahasaLainnya.Text.Trim
                .BahasaDaerah = String.Empty
                .GCAgamaPasien = ddlAgamaPasien.SelectedValue.Trim
                .BudayaPasien = txtBudayaPasien.Text.Trim
                .PerluPenterjemah = txtPerluPenterjemah.Text.Trim
                .GCCaraBelajarDisukai = ddlCaraBelajarDisukai.SelectedValue.Trim
                .GCInformasiDiinginkan = String.Empty
                .isInformasiDiagnosa = chkIsDiagnosa.Checked
                .isInformasiNyeri = chkIsNyeri.Checked
                .isInformasiDietNutrisi = chkIsDietNutrisi.Checked
                .isInformasiAlatMedis = chkIsAlatMedis.Checked
                .isInformasiTerapi = chkIsTerapi.Checked
                .InformasiDiinginkanKeteragan = txtInformasiDiinginkan.Text.Trim
                .IsKIE = chkIsKIE.Checked
                .IsObatPulang = chkIsObatPulang.Checked
                .IsFotoRadiologi = chkIsFotoRadiologi.Checked
                .IsLaboratorium = chkIsLaboratorium.Checked
                .IsEKG = chkIsEKG.Checked
                .IsKIEAPS = chkIsKIEAPS.Checked
                .IsTTDAPS = chkIsTTDAPS.Checked
                .RoomCode = ddlRoomCode.SelectedValue.Trim
                .ReferToHospitalName = txtReferToHospitalName.Text.Trim
                .IsDeath = CBool(rblIsDeath.SelectedValue.Trim)
                .DeathTime = txtDeathTime.Text.Trim
                .CreatedBy = MyBase.LoggedOnUserID.Trim
                .LastUpdatedBy = MyBase.LoggedOnUserID.Trim
               
                If .Insert() Then
                    .TglDatang = calTglDatang.selectedDate
                    .TglLayan = calTglLayan.selectedDate
                    .JamDatang = txtJamDatang.Text.Trim
                    .JamLayan = txtJamLayan.Text.Trim

                    If chkKeluar.Checked Then
                        .TglDisposisi = calTglDisposisi.selectedDate
                        .JamDisposisi = txtJamDisposisi.Text.Trim
                        .KeadaanKeluar = ddlKdnKlr.SelectedValue.Trim
                        .CaraKeluar = ddlCrKlr.SelectedValue.Trim
                    End If

                    .UpdateRegistration(chkKeluar.Checked)
                End If
            End With
            oBR.Dispose()
            oBR = Nothing

            UpdateViewGridNurseAssessment()
        End Sub

        Private Sub UpdateNurseAssesment()
            Dim oBR As New Common.BussinessRules.NurseAssesment
            With oBR
                .PengkajianID = CInt(txtPengkajianID.Text.Trim)
                .DepartmentID = ddlDepartmentFilter.SelectedValue.Trim
                .RegistrationNo = lblPBRegistrationNo.Text.Trim
                .GCAsalInformasi = ddlAsalInformasi.SelectedValue.Trim
                .AsalInformasiNama = txtAsalInformasiNama.Text.Trim
                .GCAsalInformasiHubungan = ddlAsalInformasiHubungan.SelectedValue.Trim
                .KeluhanUtama = txtKeluhanUtama.Text.Trim
                .RiwayatKeluhanSaatIni = txtRiwayatKeluhanSaatIni.Text.Trim
                .IsRiwayatAlergi = CBool(rblIsRiwayatAlergi.SelectedValue.Trim)
                .GCJenisAlergi = ddlJenisAlergi.SelectedValue.Trim
                .ReaksiAlergi = txtReaksiAlergi.Text.Trim
                .GCKesadaran = ddlKesadaran.SelectedValue.Trim
                .ttvTekananDarah = txtttvTekananDarah.Text.Trim 'CDec(IIf(IsNumeric(txtttvTekananDarah.Text.Trim) = True, CDec(txtttvTekananDarah.Text.Trim), 0).ToString.Trim)
                .ttvNadi = CDec(IIf(IsNumeric(txtttvNadi.Text.Trim) = True, CDec(txtttvNadi.Text.Trim), 0).ToString.Trim)
                .GCttvNadiDenyut = ddlttvNadiDenyut.SelectedValue.Trim
                .ttvSuhu = CDec(IIf(IsNumeric(txtttvSuhu.Text.Trim) = True, CDec(txtttvSuhu.Text.Trim), 0).ToString.Trim)
                .ttvPernafasan = CDec(IIf(IsNumeric(txtttvPernafasan.Text.Trim) = True, CDec(txtttvPernafasan.Text.Trim), 0).ToString.Trim)
                .ttvBeratBadan = CDec(IIf(IsNumeric(txtttvBeratBadan.Text.Trim) = True, CDec(txtttvBeratBadan.Text.Trim), 0).ToString.Trim)
                .ttvTinggiBadan = CDec(IIf(IsNumeric(txtttvTinggiBadan.Text.Trim) = True, CDec(txtttvTinggiBadan.Text.Trim), 0).ToString.Trim)
                .ttvIndexMasaTubuh = CDec(IIf(IsNumeric(txtttvIndexMassaTubuh.Text.Trim) = True, CDec(txtttvIndexMassaTubuh.Text.Trim), 0).ToString.Trim)
                .IsNyeri = CBool(rblIsNyeri.SelectedValue.Trim)
                .NyeriLokasi = txtNyeriLokasi.Text.Trim
                .NyeriDurasi = txtNyeriDurasi.Text.Trim
                .NyeriSkala = txtNyeriSkala.Text.Trim
                .NyeriSkalaNum = CInt(rblNyeriSkala.SelectedValue.Trim)
                .GCNyeriMetode = ddlNyeriMetode.SelectedValue.Trim
                .GCNyeriKualitas = ddlNyeriKualitas.SelectedValue.Trim
                .GCNyeriKarakteristik = ddlNyeriKarakteristik.SelectedValue.Trim
                .GCNyeriBerkurang = ddlNyeriBerkurang.SelectedValue.Trim
                .GCNyeriBertambah = ddlNyeriBertambah.SelectedValue.Trim
                .NyeriBerkurangKeterangan = txtNyeriBerkurangKeterangan.Text.Trim
                .NyeriBertambahKeterangan = txtNyeriBertambahKeterangan.Text.Trim
                .GCAktivitasMobilisasi = ddlAktivitasMobilisasi.SelectedValue.Trim
                .KategoriResikoJatuh = txtKategoriResikoJatuh.Text.Trim
                .IsResikoDekubitas = CBool(rblIsDekubitas.SelectedValue.Trim)
                .GCStatusPsikologi = ddlStatusPsikologi.SelectedValue.Trim
                .StatusPsikologiKeterangan = txtStatusPsikologiKeterangan.Text.Trim
                .GCStatusMental = ddlStatusMental.SelectedValue.Trim
                .StatusMentalKeterangan = txtStatusMentalKeterangan.Text.Trim
                .GCStatusPernikahan = ddlStatusPernikahan.SelectedValue.Trim
                .IsHubunganKeluargaPasienBaik = CBool(rblIsHubunganKeluargaBaik.SelectedValue.Trim)
                .GCStatusTempatTinggal = ddlStatusTempatTinggal.SelectedValue.Trim
                .IsBeribadahTeratur = CBool(rblIsBeribadahTeratur.SelectedValue.Trim)
                .GCPekerjaan = ddlPekerjaan.SelectedValue.Trim
                .GCPenjaminBayar = ddlPenjaminBayar.SelectedValue.Trim
                .PenjaminBayarCode = String.Empty
                .GCStatusGizi = ddlStatusGizi.SelectedValue.Trim
                .GCSkorPerubahanBeratBadan = ddlSkorPerubahanBeratBadan.SelectedValue.Trim
                .IsTidakNafsuMakan = CBool(rblIsTidakNafsuMakan.SelectedValue.Trim)
                .SkorNutrisi = CInt(IIf(IsNumeric(txtSkorNutrisi.Text.Trim) = True, CInt(txtSkorNutrisi.Text.Trim), 0).ToString.Trim)
                .IsPerluKonsultasiGizi = CBool(rblIsPerluKonsultasiGizi.SelectedValue.Trim)
                .IsHamil = CBool(rblIsHamil.SelectedValue.Trim)
                .HamilGravidKeterangan = txtHamilGravidKeterangan.Text.Trim
                .HamilParaKeterangan = txtHamilParaKeterangan.Text.Trim
                .HamilAbortusKeterangan = txtHamilAbortusKeterangan.Text.Trim
                .IsBersediaMenerimaInformasi = CBool(rblIsBersediaMenerimaInformasi.SelectedValue.Trim)
                .IsHambatanEdukasi = CBool(rblIsHambatanEdukasi.SelectedValue.Trim)
                .IsHambatanPendengaran = chkIsHambatanPendengaran.Checked
                .IsHambatanPenglihatan = chkIsHambatanPenglihatan.Checked
                .IsHambatanKognitif = chkIsHambatanKognitif.Checked
                .IsHambatanFisik = chkIsHambatanFisik.Checked
                .IsHambatanBudaya = chkIsHambatanBudaya.Checked
                .IsHambatanEmosi = chkIsHambatanEmosi.Checked
                .IsHambatanBahasa = chkIsHambatanBahasa.Checked
                .HambatanLainnya = txtHambatanLainnya.Text.Trim
                .GCPendidikanPasien = ddlPendidikanPasien.SelectedValue.Trim
                .GCBahasaPasien = ddlBahasaPasien.SelectedValue.Trim
                .BahasaLainnya = txtBahasaLainnya.Text.Trim
                .BahasaDaerah = String.Empty
                .GCAgamaPasien = ddlAgamaPasien.SelectedValue.Trim
                .BudayaPasien = txtBudayaPasien.Text.Trim
                .PerluPenterjemah = txtPerluPenterjemah.Text.Trim
                .GCCaraBelajarDisukai = ddlCaraBelajarDisukai.SelectedValue.Trim
                .GCInformasiDiinginkan = String.Empty
                .isInformasiDiagnosa = chkIsDiagnosa.Checked
                .isInformasiNyeri = chkIsNyeri.Checked
                .isInformasiDietNutrisi = chkIsDietNutrisi.Checked
                .isInformasiAlatMedis = chkIsAlatMedis.Checked
                .isInformasiTerapi = chkIsTerapi.Checked
                .InformasiDiinginkanKeteragan = txtInformasiDiinginkan.Text.Trim
                .IsKIE = chkIsKIE.Checked
                .IsObatPulang = chkIsObatPulang.Checked
                .IsFotoRadiologi = chkIsFotoRadiologi.Checked
                .IsLaboratorium = chkIsLaboratorium.Checked
                .IsEKG = chkIsEKG.Checked
                .IsKIEAPS = chkIsKIEAPS.Checked
                .IsTTDAPS = chkIsTTDAPS.Checked
                .RoomCode = ddlRoomCode.SelectedValue.Trim
                .ReferToHospitalName = txtReferToHospitalName.Text.Trim
                .IsDeath = CBool(rblIsDeath.SelectedValue.Trim)
                .DeathTime = txtDeathTime.Text.Trim
                .LastUpdatedBy = MyBase.LoggedOnUserID.Trim

                If .Update() Then
                    .TglDatang = calTglDatang.selectedDate
                    .TglLayan = calTglLayan.selectedDate
                    .JamDatang = txtJamDatang.Text.Trim
                    .JamLayan = txtJamLayan.Text.Trim
                    .Triage = ddlKdTriage.SelectedValue.Trim

                    If chkKeluar.Checked Then
                        .TglDisposisi = calTglDisposisi.selectedDate
                        .JamDisposisi = txtJamDisposisi.Text.Trim
                        .KeadaanKeluar = ddlKdnKlr.SelectedValue.Trim
                        .CaraKeluar = ddlCrKlr.SelectedValue.Trim
                    End If
                    .UpdateRegistration(chkKeluar.Checked)
                End If
            End With
            oBR.Dispose()
            oBR = Nothing

            UpdateViewGridNurseAssessment()
        End Sub

        Private Sub OpenNurseNotes(ByVal NurseNotesID As Integer)
            Dim oBR As New Common.BussinessRules.EMR
            With oBR
                .NurseNotesID = NurseNotesID
                If .GetNurseNotesByID.Rows.Count > 0 Then
                    txtNurseNotes.Text = .NurseNotes.Trim
                    chkIsPhysicianConfirmed.Checked = .IsPhysicianConfirmed
                    calTglCP.selectedDate = .JournalDate
                    txtJamCP.Text = .JournalTime.Trim
                Else
                    txtNurseNotes.Text = String.Empty
                    chkIsPhysicianConfirmed.Checked = False
                    calTglCP.selectedDate = Date.Now
                    txtJamCP.Text = Format(Date.Now, commonFunction.FORMAT_TIME)
                End If
            End With
            oBR.Dispose()
            oBR = Nothing
        End Sub

        Private Sub InsertNurseNotes()
            Dim oBR As New Common.BussinessRules.EMR
            With oBR
                .DepartmentID = ddlDepartmentFilter.SelectedValue.Trim
                .ServiceUnitID = lblPBServiceUnitID.Text.Trim
                .RegistrationNo = lblPBRegistrationNo.Text.Trim
                .MRN = lblPBMRN.Text.Trim
                .NurseNotes = txtNurseNotes.Text.Trim
                .JournalDate = calTglCP.selectedDate
                .JournalTime = txtJamCP.Text.Trim
                .CreatedBy = MyBase.LoggedOnUserID.Trim
                .LastUpdatedBy = MyBase.LoggedOnUserID.Trim
                .InsertNurseNotes()
            End With
            oBR.Dispose()
            oBR = Nothing
        End Sub

        Private Sub UpdateNurseNotes()
            Dim oBR As New Common.BussinessRules.EMR
            With oBR
                .NurseNotesID = CInt(txtNurseNotesID.Text.Trim)
                .DepartmentID = ddlDepartmentFilter.SelectedValue.Trim
                .ServiceUnitID = lblPBServiceUnitID.Text.Trim
                .RegistrationNo = lblPBRegistrationNo.Text.Trim
                .MRN = lblPBMRN.Text.Trim
                .JournalDate = calTglCP.selectedDate
                .JournalTime = txtJamCP.Text.Trim
                .NurseNotes = txtNurseNotes.Text.Trim
                .CreatedBy = MyBase.LoggedOnUserID.Trim
                .LastUpdatedBy = MyBase.LoggedOnUserID.Trim
                .UpdateNurseNotes()
            End With
            oBR.Dispose()
            oBR = Nothing
        End Sub
#End Region

    End Class

End Namespace