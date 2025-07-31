Public Class detPagoCuotas
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTC As System.Web.UI.WebControls.Label
    Protected WithEvents cboTipDocumento1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboBanco1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDoc1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipDocumento2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboBanco2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDoc2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipDocumento3 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboBanco3 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDoc3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecibidoPen As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtVuelto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecibidoUsd As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents txtTarjCred As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBIN As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIdentificadorCliente As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNombreCliente As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents dgCuotas As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Td2 As System.Web.UI.HtmlControls.HtmlTableCell

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    'INICIATIVA-529: ASIGSAP INI
    Public objFileLog As New SICAR_Log
    Dim nameFilePos As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivoPos As String = objFileLog.Log_CrearNombreArchivo(nameFilePos)
    'INICIATIVA-529: ASIGSAP FIN

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim objRecauda As New SAP_SIC_Recaudacion.clsRecaudacion
            Dim objRecaudaSans As New NEGOCIO_SIC_SANS.SansNegocio

            Dim strName As String
            Dim strDoc As String
            Dim strDocSAP As String
            Dim dblSubtot As Double
            Dim dblIGV As Double
            Dim dblTotal As Double
            Dim dsCuotas As DataSet

            txtMonto1.Attributes.Add("onChange", "javascript:f_Recalcular(this);")
            txtMonto2.Attributes.Add("onChange", "javascript:f_Recalcular(this);")
            txtMonto3.Attributes.Add("onChange", "javascript:f_Recalcular(this);")
            txtMonto1.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            txtMonto2.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            txtMonto3.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            txtRecibidoPen.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            txtRecibidoUsd.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            txtRecibidoPen.Attributes.Add("onChange", "javascript:CalculoVuelto();")
            txtRecibidoUsd.Attributes.Add("onChange", "javascript:CalculoVuelto();")
            btnGrabar.Attributes.Add("onClick", "f_validaCajaCerrada();f_Grabar()") 'INICIATIVA-318
            If Not Page.IsPostBack Then
                txtIdentificadorCliente.Value = Session("ClienteDAC")

                'dsCuotas = objRecauda.Get_DatosCuotas(Session("ClienteDAC"), strName, strDoc, strDocSAP, dblSubtot, dblIGV, dblTotal)
                Dim usuario_id As String = Session("codUsuario")
                dsCuotas = objRecaudaSans.Get_DatosCuotas(Session("ClienteDAC"), strName, strDoc, strDocSAP, dblSubtot, dblIGV, dblTotal, "", "", usuario_id)

                If Not dsCuotas Is Nothing Then
                dgCuotas.DataSource = dsCuotas.Tables(0)
                dgCuotas.DataBind()
                End If


                'If dblMonto < 0 Then
                '    tdDeuda.InnerText = "Deuda"
                'Else
                '    tdDeuda.InnerText = "Saldo a Favor"
                'End If
                'txtMonto.Value = Math.Abs(dblMonto)
                txtNombreCliente.Value = strName
                'lblTC.Text = ObtenerTipoCambio(Now.ToString("d"))
                lblTC.Text = ObtenerTipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"))
                LlenaCombos()
                LeeDatosValidar()
            End If
        End If
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim objRecauda As New SAP_SIC_Recaudacion.clsRecaudacion
        Dim strTrama As String = ""
        Dim strTramaRep As String = ""
        Dim i As Integer
        Dim dsResult As DataSet
        Dim blnError As Boolean
        Dim dblTotal As Double = 0
        Dim strNroAt As String
        Dim strName1 As String
        Dim strStcd1 As String
        Dim strTelefono As String

        Dim objCaja As New COM_SIC_Cajas.clsCajas

        ''Variables de Auditoria
        'Dim wParam1 As Long
        'Dim wParam2 As String
        'Dim wParam3 As String
        'Dim wParam4 As Long
        'Dim wParam5 As Integer
        'Dim wParam6 As String
        'Dim wParam7 As Long
        'Dim wParam8 As Long
        'Dim wParam9 As Long
        'Dim wParam10 As String
        'Dim wParam11 As Integer
        'Dim Detalle(5, 3) As String

        'Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        '' fin de variables de auditoria

        Dim intOper As Int32

        strTrama = txtDoc1.Text & ";" & txtMonto1.Text & ";" & cboTipDocumento1.SelectedValue & "|"


        If txtMonto2.Text.Trim <> "" Then
            strTrama = strTrama & txtDoc2.Text & ";" & txtMonto2.Text & ";" & cboTipDocumento2.SelectedValue & "|"
        End If

        If txtMonto3.Text.Trim <> "" Then
            strTrama = strTrama & txtDoc3.Text & ";" & txtMonto3.Text & ";" & cboTipDocumento3.SelectedValue & "|"
        End If

        strTrama = Left(strTrama, Len(strTrama) - 1)

        'AUDITORIA
        'wParam1 = Session("codUsuario")
        'wParam2 = Request.ServerVariables("REMOTE_ADDR")
        'wParam3 = Request.ServerVariables("SERVER_NAME")
        'wParam4 = ConfigurationSettings.AppSettings("gConstOpcRDAC")
        'wParam5 = 1
        'wParam6 = "Recaudacion de DACs"
        'wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        'wParam8 = ConfigurationSettings.AppSettings("gConstEvtRDac")
        'wParam9 = Session("codPerfil")
        'wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        'wParam11 = 1

        'Detalle(1, 1) = "OfVta"
        'Detalle(1, 2) = Session("ALMACEN")
        'Detalle(1, 3) = "Oficina de venta"

        'Detalle(2, 1) = "DAC"
        'Detalle(2, 2) = Session("ClienteDAC")
        'Detalle(2, 3) = "Cliente DAC"

        'Detalle(3, 1) = "Fecha"
        'Detalle(3, 2) = Now.ToString("d")
        'Detalle(3, 3) = "Fecha"

        'Detalle(4, 1) = "Trama"
        'Detalle(4, 2) = strTrama
        'Detalle(4, 3) = "Trama de Pagos"

        'Detalle(5, 1) = "Usuario"
        'Detalle(5, 2) = Session("USUARIO")
        'Detalle(5, 3) = "Usuario"
        ''FIN DE AUDITORIA

        'Response.Write("<script language=javascript>alert('" & Session("ALMACEN") & " / " & Session("USUARIO") & " / " & Session("ClienteDAC") & " / " & strTrama & " / " & strName1 & " / " & strNroAt & " / " & strStcd1 & "')</script>")

        'INICIATIVA-529: ASIGSAP INI
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Set_PagoCuotas: " & "Inicio")
        Dim strOficina As String = ObtenerCodigoOficinaVenta(CStr(Session("ALMACEN")))
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "strOficina: " & strOficina)
        dsResult = objRecauda.Set_PagoCuotas(strOficina, Session("USUARIO"), Session("ClienteDAC"), Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"), strTrama, strName1, strNroAt, strStcd1)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Set_PagoCuotas: " & "Fin")
        'INICIATIVA-529: ASIGSAP FIN

        blnError = False
        For i = 0 To dsResult.Tables(0).Rows.Count - 1
            If dsResult.Tables(0).Rows(i).Item("TYPE") = "W" Or dsResult.Tables(0).Rows(i).Item("TYPE") = "E" Then
                blnError = True
                'wParam5 = 0
                'wParam6 = "Error en Recaudacion de DACs." & dsResult.Tables(0).Rows(i).Item("MESSAGE")
                'objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                Response.Write("<script language=javascript>alert('" & "1" & dsResult.Tables(0).Rows(i).Item("MESSAGE") & "')</script>")
                Exit For
            End If
        Next

        If Not blnError Then

            strTramaRep = cboTipDocumento1.SelectedItem.Text & ";" & txtDoc1.Text & ";" & txtMonto1.Text & "|"

            If cboTipDocumento1.SelectedValue = "ZEFE" Then
                objCaja.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto1.Text))
            End If

            intOper = objCaja.FP_Cab_Oper(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), Session("USUARIO"), "", _
                                                                                   Session("ClienteDAC"), "RCUO", "", strNroAt, _
                                                                                   dblTotal, 0, dblTotal, "C", _
                                                                                   CDbl(txtRecibidoPen.Text), CDbl(txtRecibidoUsd.Text), CDbl(txtVuelto.Text))

            objCaja.FP_Det_Oper(intOper, 1, "", "", "", 1, 0, 0, 0)

            objCaja.FP_Pag_Oper(intOper, 1, cboTipDocumento1.SelectedValue, txtDoc1.Text, CDbl(txtMonto1.Text))

            dblTotal += CDbl(txtMonto1.Text)
            If txtMonto2.Text.Trim <> "" Then
                strTramaRep = strTramaRep & cboTipDocumento2.SelectedItem.Text & ";" & txtDoc2.Text & ";" & txtMonto2.Text & "|"
                dblTotal += CDbl(txtMonto2.Text)
                If cboTipDocumento2.SelectedValue = "ZEFE" Then
                    objCaja.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto2.Text))
                End If
                objCaja.FP_Pag_Oper(intOper, 2, cboTipDocumento2.SelectedValue, txtDoc2.Text, CDbl(txtMonto2.Text))
            End If

            If txtMonto3.Text.Trim <> "" Then
                strTramaRep = strTramaRep & cboTipDocumento3.SelectedItem.Text & ";" & txtDoc3.Text & ";" & txtMonto3.Text & "|"
                dblTotal += CDbl(txtMonto3.Text)
                If cboTipDocumento3.SelectedValue = "ZEFE" Then
                    objCaja.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto3.Text))
                End If
                objCaja.FP_Pag_Oper(intOper, 3, cboTipDocumento3.SelectedValue, txtDoc3.Text, CDbl(txtMonto3.Text))
            End If

            strTramaRep = Left(strTramaRep, Len(strTramaRep) - 1)
            strTelefono = Session("ClienteDAC")
            Session("ClienteDAC") = ""
            Session("strMensajeDAC") = "El pago se registro correctamente"
            ' objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
            Response.Redirect("recPagoCuotas.aspx?strTrama=" & strTramaRep & "&strMonto=" & Format(Math.Round(dblTotal, 2), "#######0.00") & "&Dealer=" & Replace(txtNombreCliente.Value, "&", " ") & "&strTelefono=" & strTelefono)

        End If

    End Sub

    Private Sub LlenaCombos()
        Dim objPagos As New SAP_SIC_Pagos.clsPagos
        Dim objConf As New COM_SIC_Configura.clsConfigura

        Dim dsFormaPago As DataSet = objPagos.Get_ConsultaViasPago(Session("ALMACEN"))
        Dim dsBancos As DataSet = objConf.FP_Lista_Bancos()

        cboLoad(dsFormaPago, cboTipDocumento1)
        cboLoad(dsFormaPago, cboTipDocumento2)
        cboLoad(dsFormaPago, cboTipDocumento3)

        cboLoadBan(dsBancos, cboBanco1)
        cboLoadBan(dsBancos, cboBanco2)
        cboLoadBan(dsBancos, cboBanco3)

        cboTipDocumento2.Items.Insert(0, "")
        cboTipDocumento3.Items.Insert(0, "")

        cboBanco1.Items.Insert(0, "")
        cboBanco2.Items.Insert(0, "")
        cboBanco3.Items.Insert(0, "")
        'VIA DE PAGO POR DEFECTO
        cboTipDocumento1.SelectedValue = "ZEFE"
        cboTipDocumento2.SelectedValue = ""
        cboTipDocumento3.SelectedValue = ""

    End Sub

    Private Sub cboLoad(ByVal dsFormaPago As DataSet, ByRef cboCampo As DropDownList)
        cboCampo.DataSource = dsFormaPago.Tables(0)
        cboCampo.DataTextField = "VTEXT"
        cboCampo.DataValueField = "CCINS"
        cboCampo.DataBind()

    End Sub

    Private Sub cboLoadBan(ByVal dsBanco As DataSet, ByRef cboCampo As DropDownList)
        cboCampo.DataSource = dsBanco.Tables(0)
        cboCampo.DataTextField = "BANC_DESC"
        cboCampo.DataValueField = "ID_CONFBANC"
        cboCampo.DataBind()

    End Sub

    Private Sub LeeDatosValidar()
        '***************LEE TARJETAS CREDITO
        Dim objSap As New SAP_SIC_Pagos.clsPagos
        Dim dsTmp As DataSet = objSap.Get_Tarjeta_Credito()
        Dim dr As DataRow
        txtTarjCred.Text = ""
        For Each dr In dsTmp.Tables(0).Rows
            txtTarjCred.Text += CStr(dr(0)) + ";"
        Next

        '*************leee BIN
        Dim obCajas As New COM_SIC_Cajas.clsCajas
        dsTmp = obCajas.FP_ListaBIN()

        txtBIN.Text = ""
        For Each dr In dsTmp.Tables(0).Rows
            txtBIN.Text += CStr(dr(0)) + ";"
        Next

    End Sub

    Private Function ObtenerTipoCambio(ByVal strFecha As String) As String

        Dim obPagos As New SAP_SIC_Pagos.clsPagos
        'Return obPagos.Get_TipoCambio(strFecha).ToString("N2")
        Return obPagos.Get_TipoCambio(strFecha).ToString("N3") 'aotane 05.08.2013

    End Function

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("recPagoCuotas.aspx")
    End Sub

    'INICIATIVA-529: ASIGSAP INI
    Private Function ObtenerCodigoOficinaVenta(ByVal strOficinaVta As String) As String
        Dim strVersionSap As String = ConfigurationSettings.AppSettings("strVersionSap")
        Dim resultado As String = String.Empty
        If strVersionSap.Equals("6") Then
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim dsOficinas As DataSet = objOffline.Obtener_NewCodeOficinaVenta(strOficinaVta)
            Dim drOficina As DataRow

            If Not dsOficinas Is Nothing Then
                If dsOficinas.Tables(0).Rows.Count > 0 Then
                    drOficina = dsOficinas.Tables(0).Rows(0)
                    With drOficina
                        resultado = CStr(.Item("PAOFC_OFICINAVENTAS"))
                    End With
                End If
            End If
        ElseIf strVersionSap.Equals("4") Then
            resultado = strOficinaVta
        End If
        Return resultado
    End Function
    'INICIATIVA-529: ASIGSAP FIN

End Class
