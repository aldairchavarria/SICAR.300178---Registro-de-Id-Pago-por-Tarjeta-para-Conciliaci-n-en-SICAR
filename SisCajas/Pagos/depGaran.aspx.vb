Imports SisCajas.Funciones
Imports System.Text
Imports System.IO
Imports COM_SIC_Activaciones
Imports SisCajas.clsActivaciones
Public Class depGaran
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtNumDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecReg As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecVenc As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtImporte As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboViaPago As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCorrRef As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents txtTipoDoc As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkImprimir As System.Web.UI.HtmlControls.HtmlInputCheckBox
    Protected WithEvents txtTipoCargo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumOperacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTitulo As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim drFila As DataRow
    Dim dsReturn As DataSet
    Dim objPagos As New SAP_SIC_Pagos.clsPagos
    Dim objLog As New SICAR_Log
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objLog.Log_CrearNombreArchivo(nameFile)
    Dim clsConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
    Dim objTrsMsSap As New COM_SIC_Activaciones.clsTrsMsSap


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
            Dim dsTipoDoc As DataSet
            Dim dsFormaPago As DataSet
            Dim dsCorr As DataSet
            Dim i As Integer
            drFila = Session("DocSel")

            btnGrabar.Attributes.Add("onClick", "f_Validar()")

            'Session("FechaPago") = drFila.Item("FKDAT")
            Session("FechaPago") = drFila.Item("PAGOD_FECHACONTA")
            'dsReturn = objPagos.Get_DepositoGarantia(drFila.Item("VBELN")) ' TODOEB
            dsReturn = clsConsultaMsSap.ConsultaPedido(drFila.Item("PEDIN_NROPEDIDO"), Session("ALMACEN"), "")

            If Not Page.IsPostBack Then
                dsTipoDoc = objPagos.Get_LeeTipoDocCliente()

               txtFecReg.Text = dsReturn.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO")
                txtFecVenc.Text = dsReturn.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO")
                txtImporte.Text = dsReturn.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")
                txtNumDocumento.Text = dsReturn.Tables(0).Rows(0).Item("CLIEV_NRODOCCLIENTE")

                txtTipoCargo.Text = IIf(dsReturn.Tables(0).Rows(0).Item("CLDOC") = ConfigurationSettings.AppSettings("cteDesTipoCargoFijoDG"), ConfigurationSettings.AppSettings("cteTipoCargoFijoDG"), ConfigurationSettings.AppSettings("cteTipoCargoFijoRA"))

                'txtTipoCargo.Text = IIf(dsReturn.Tables(0).Rows(0).Item("CLDOC") = ConfigurationSettings.AppSettings("cteDesTipoCargoFijoDG"), ConfigurationSettings.AppSettings("cteTipoCargoFijoDG"), ConfigurationSettings.AppSettings("cteTipoCargoFijoRA")) TODOEB
                txtTipoCargo.Text = "DL"
                ' If dsReturn.Tables(0).Rows(0).Item("CLDOC") = ConfigurationSettings.AppSettings("cteDesTipoCargoFijoDX") Then TODOEB
                ' txtTipoCargo.Text = ConfigurationSettings.AppSettings("cteTipoCargoFijoDX") TODOEB
                ' End If TODOEB
				
                lblTitulo.Text = txtTipoCargo.Text

                txtNumOperacion.Text = dsReturn.Tables(0).Rows(0).Item("NRO_OPERACION")

                For i = 0 To dsTipoDoc.Tables(0).Rows.Count - 1
                    ' If dsTipoDoc.Tables(0).Rows(i).Item("J_1ATODC") = dsReturn.Tables(0).Rows(0).Item("TIPO_DOC_CLIENTE") Then TODOEB
                    If dsTipoDoc.Tables(0).Rows(i).Item("J_1ATODC") = dsReturn.Tables(0).Rows(0).Item("CLIEC_TIPODOCCLIENTE") Then
                        txtTipoDoc.Text = dsTipoDoc.Tables(0).Rows(i).Item("TEXT30")
                    End If
                Next

                dsFormaPago = objPagos.Get_ConsultaViasPago(Session("ALMACEN"))
                'dsFormaPago = clsOffline.Obtener_ConsultaViasPago(Session("ALMACEN"))

                Dim dtVias As DataTable
                dtVias = New DataTable
                dtVias = VerificarVias(dsFormaPago)

                cboViaPago.DataSource = dtVias
                'cboViaPago.DataSource = dsFormaPago.Tables(0)
                cboViaPago.DataTextField = "VTEXT"
                cboViaPago.DataValueField = "CCINS"
                cboViaPago.DataBind()

                Try
                    cboViaPago.SelectedValue = "ZEFE"
                Catch ex As Exception
                    cboViaPago.SelectedIndex = 0
                End Try
                'cboViaPago.SelectedValue = "ZEFE"
                'cboViaPago.Enabled = False



                If Trim(dsReturn.Tables(0).Rows(0).Item("PAGOC_CODSUNAT")) <> "" Then

                    txtCorrRef.Text = Right(dsReturn.Tables(0).Rows(0).Item("PAGOC_CODSUNAT"), 5)
                    'If Trim(dsReturn.Tables(0).Rows(0).Item("XBLNR")) <> "" Then TODOEB
                    'txtCorrRef.Text = Right(dsReturn.Tables(0).Rows(0).Item("XBLNR"), 5) TODOEB
                Else
                    'dsCorr = objPagos.Get_NroCorrGarantia(Session("ALMACEN"), dsReturn.Tables(0).Rows(0).Item("CLDOC"), txtCorrRef.Text)
                    dsCorr = objPagos.Get_NroCorrGarantia(Session("ALMACEN"), "DL", "")
                End If
            Else
                cboViaPago.SelectedValue = Request.Item("cboViaPago")
                'cboViaPago.SelectedValue = "ZEFE"
        End If
        End If
    End Sub
    Private Function VerificarVias(ByVal ds As DataSet) As DataTable

        Dim StrRectricciones As String
        Dim StrEfectivo, StrCheque, StrTarjeta, StrDebito, StrOtros As String
        Dim ArrEfectivo, ArrCheque, ArrTarjeta, ArrDebito, ArrOtros, ArrTotal As ArrayList
        Dim LOpciones As New ArrayList
        Dim dtVias As DataTable = New DataTable("ViasPago")
        Dim boolexiste As Boolean

        Try
            If Session("WS_OpcionesPagina") Is Nothing Then
                Response.Write("<script> alert('Error: No se ubica el perfil para las vias de pago'); </script>")
            Else
                LOpciones = Session("WS_OpcionesPagina")
            End If

            StrRectricciones = ConfigurationSettings.AppSettings("constCodigoViasSap")

            StrEfectivo = ExtraerCadena(StrRectricciones)
            StrCheque = ExtraerCadena(StrRectricciones)
            StrTarjeta = ExtraerCadena(StrRectricciones)
            StrDebito = ExtraerCadena(StrRectricciones)
            StrOtros = ExtraerCadena(StrRectricciones)

            ArrEfectivo = ExtraerValores(StrEfectivo)
            ArrCheque = ExtraerValores(StrCheque)
            ArrTarjeta = ExtraerValores(StrTarjeta)
            ArrDebito = ExtraerValores(StrDebito)
            ArrOtros = ExtraerValores(StrOtros)

            Dim oEfectivo = ConfigurationSettings.AppSettings("constOpcPagEfectivo")
            Dim oCheque = ConfigurationSettings.AppSettings("constOpcPagCheque")
            Dim oTarjeta = ConfigurationSettings.AppSettings("constOpcPagTarjeta")
            Dim oDebito = ConfigurationSettings.AppSettings("constOpcPagDebito")
            Dim oOtros = ConfigurationSettings.AppSettings("constOpcPagOtros")

            ArrTotal = New ArrayList

            For Each cad As String In LOpciones
                If cad = oEfectivo Then
                    For Each SCad As String In ArrEfectivo
                        ArrTotal.Add(SCad)
                    Next
                ElseIf cad = oCheque Then
                    For Each SCad As String In ArrCheque
                        ArrTotal.Add(SCad)
                    Next
                ElseIf cad = oTarjeta Then
                    For Each SCad As String In ArrTarjeta
                        ArrTotal.Add(SCad)
                    Next
                ElseIf cad = oDebito Then
                    For Each SCad As String In ArrDebito
                        ArrTotal.Add(SCad)
                    Next
                ElseIf cad = oOtros Then
                    For Each SCad As String In ArrOtros
                        ArrTotal.Add(SCad)
                    Next
                End If
            Next

            If ArrTotal.Count = 0 Then
                dtVias = ds.Tables(0)
            Else
                dtVias = ds.Tables(0).Clone
                For Each sRow As DataRow In ds.Tables(0).Rows
                    'boolexiste = False
                    'For Each sCad As String In ArrTotal
                    '    If CStr(sRow(0)) = sCad Then
                    '        boolexiste = True
                    '    End If
                    'Next
                    'If Not boolexiste Then
                    '    dtVias.ImportRow(sRow)
                    'End If
                    For Each Rpt As String In ArrTotal
                        If Rpt = sRow(0) Then
                            dtVias.ImportRow(sRow)
                        End If
                    Next
                Next
            End If

            Return dtVias

        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "');</script>")
            Response.End()
        End Try
    End Function

    Private Function ExtraerCadena(ByRef Cadena As String) As String

        Dim Arr As String = String.Empty
        Dim Posicion As Int32 = 1
        Dim Valor As String

        If Cadena = String.Empty Then
            Return Arr
        End If

        Posicion = InStr(Cadena, ";")

        If Posicion <> 0 Then
            Valor = Mid(Cadena, 1, Posicion - 1)
            Cadena = Mid(Cadena, Posicion + 1)
            Arr += Valor
        Else
            Arr += Cadena
        End If

        Return Arr

    End Function
    Private Function ExtraerValores(ByVal Cadena As String) As ArrayList

        Dim Arr As New ArrayList
        Dim Posicion As Int32 = 1
        Dim Valor As String

        If Cadena = String.Empty Then
            Return Arr
        End If

        While Posicion <> 0
            Posicion = InStr(Cadena, ",")
            If Posicion <> 0 Then
                Valor = Mid(Cadena, 1, Posicion - 1)
                Cadena = Mid(Cadena, Posicion + 1)
                Arr.Add(Valor)
            Else
                Arr.Add(Cadena)
            End If
        End While

        Return Arr

    End Function
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim strBELNR As String
        Dim strXBLNR As String
        Dim strMensaje As String
        Dim strNumeroSAP As String
        Dim dsResult As DataSet
        Dim strIND_ACTIV_BSCS As String
        Dim conEquipo As Boolean
        Dim strValSAP As String
        Dim blnDebeSerRevisado As Boolean
        Dim strEstado As String
        Dim dsEstado As DataSet
        Dim strRespuesta As String
        Dim objActivaciones As New COM_SIC_Activaciones.clsActivacion

        Dim i As Integer
        Dim strItems As String

        Dim dsPagoDep As DataSet
        Dim dsLinea As DataRow
        Dim dsAcuerdo As DataSet

        Dim strContrato As String
        Dim strNroSEC As String

        Dim blnPrimerPago As Boolean

        Dim PorMigracion As String
        Dim PorRenovacion As String
        Dim PorReposicion As String
        Dim PorAprobador As String
        Dim EstadoActualAcuerdo As String
        Dim strObsEstado As String

        Dim strNroDocSEC, strTipDocSEC, strNroAprobacion, strMotivoMig, strConSinEq As String


        Dim objContrato As DataSet

        Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu


        objContrato = objClsConsultaPvu.ObtenerDrsap(drFila.Item("PEDIN_NROPEDIDO"), "", "")


        Dim objCajas As New COM_SIC_Cajas.clsCajas

        Dim strIdentifyLog = CheckStr(drFila.Item("PEDIN_NROPEDIDO"))
        Dim claseDoc As String = CheckStr(dsReturn.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"))

        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio Set_DepositoGarantia (Zpvu_Rfc_Trs_Depos_Garantia)")
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inp P_KUNNR: ")
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inp P_STCDT: " & dsReturn.Tables(0).Rows(0).Item("CLIEC_TIPODOCCLIENTE"))
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inp P_STCDT1: " & txtNumDocumento.Text)
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inp P_VKBUR: " & Session("ALMACEN"))
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inp P_FECREG: " & txtFecReg.Text)
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inp P_FECVEN: " & txtFecVenc.Text)
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inp P_IMPORT: " & txtImporte.Text)
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inp P_WAERS: ")
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inp P_VIAPAG: " & cboViaPago.SelectedValue)
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inp P_CORRE: " & txtCorrRef.Text)
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inp Usuario: " & Session("USUARIO"))
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inp ClDoc: " & dsReturn.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"))
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inp NroOperacion: " & txtNumOperacion.Text)
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inp strNumDepGaran: " & drFila.Item("PEDIN_NROPEDIDO"))

        dsPagoDep = objPagos.Set_DepositoGarantia("", dsReturn.Tables(0).Rows(0).Item("CLIEC_TIPODOCCLIENTE"), txtNumDocumento.Text, Session("ALMACEN"), txtFecReg.Text, txtFecVenc.Text, txtImporte.Text, "", cboViaPago.SelectedValue, txtCorrRef.Text, Session("USUARIO"), _
                              dsReturn.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"), "", txtNumOperacion.Text, drFila.Item("PEDIN_NROPEDIDO"), strBELNR, strXBLNR)


        Dim K_NROLOG As String
        Dim K_DESLOG As String
        Dim K_NROLOG_DET As String
        Dim K_DESLOG_DET As String

        Dim K_PAGON_IDPAGO As Double
        Dim K_PAGOC_CORRELATIVO As String

        objTrsMsSap.RegistrarPago(drFila.Item("PEDIN_NROPEDIDO"), DateTime.Now, "PAG", "CAJ2", _
                                                 2, "S", Session("USUARIO"), _
                                                 DateTime.Now, Session("USUARIO"), DateTime.Now, _
                                                 K_NROLOG, K_DESLOG, K_PAGON_IDPAGO, K_PAGOC_CORRELATIVO)

        objTrsMsSap.RegistrarDetallePago(K_PAGON_IDPAGO, "CASH", "", "S", txtImporte.Text, Session("USUARIO"), DateTime.Now, Session("USUARIO"), DateTime.Now, "", 0, K_NROLOG_DET, K_DESLOG_DET)

        dsPagoDep = objPagos.Set_DepositoGarantia("", dsReturn.Tables(0).Rows(0).Item("TIPO_DOC_CLIENTE"), txtNumDocumento.Text, Session("ALMACEN"), txtFecReg.Text, txtFecVenc.Text, txtImporte.Text, "", cboViaPago.SelectedValue, txtCorrRef.Text, Session("USUARIO"), _
                              dsReturn.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"), "", txtNumOperacion.Text, drFila.Item("PEDIN_NROPEDIDO"), strBELNR, strXBLNR)


        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Out BELNR: " & strBELNR)
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Out XBLNR: " & strXBLNR)

        'For i = 0 To dsPagoDep.Tables(0).Rows.Count - 1
        '    If dsPagoDep.Tables(0).Rows(i).Item(0) = "E" Then
        '        strMensaje = dsPagoDep.Tables(0).Rows(i).Item(3)
        '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Out error: " & strMensaje)
        '    End If
        'Next TODOEB
        If K_NROLOG <> "OK" Then
            strMensaje = dsPagoDep.Tables(0).Rows(i).Item(3)
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: btnGrabar_Click)"
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Out error: " & strMensaje & MaptPath)
            'FIN PROY-140126

        End If
        'Next
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Fin Set_DepositoGarantia (Zpvu_Rfc_Trs_Depos_Garantia)")

        If Len(Trim(strMensaje)) = 0 Then
            Dim strDocSap As String = CheckStr(drFila.Item("PEDIN_NROPEDIDO"))
            dsLinea = dsReturn.Tables(0).Rows(0)

            blnPrimerPago = True
            If Len(Trim(dsLinea.Item(8))) > 0 Then
                blnPrimerPago = False
            End If

            dsLinea.Item(7) = strBELNR
            dsLinea.Item(8) = strXBLNR
            dsLinea.Item(15) = Session("USUARIO")
            dsLinea.Item(16) = Format(Day(Now.Date), "00") & "/" & Format(Month(Now.Date), "00") & "/" & Format(Year(Now.Date), "0000")

            For i = 0 To dsReturn.Tables(0).Columns.Count - 1
                If Not (i = 16 Or i = 17 Or i = 20) Then
                    strItems = strItems & dsLinea.Item(i)
                End If
                If i = 20 Then
                    strItems = strItems & txtNumOperacion.Text
                End If
                If i < dsReturn.Tables(0).Columns.Count - 1 Then
                    strItems = strItems & ";"
                End If
            Next
            'Response.Write(strItems) : Response.End()

            'activacion contrato por pago de renta adelantada por Migracion 
            strContrato = objContrato.Tables(0).Rows(0).Item("ID_CONTRATO")   'drFila.Item("NRO_CONTRATO")

            If Not IsNumeric(strContrato) Then
                strContrato = "KO"
            End If

            Dim codRespConvergente As String = ""
            Dim strMontoPago As String = txtImporte.Text
            'codRespConvergente = EjecutarActivacion(strDocSap, strBELNR, strMontoPago, CurrentUser, CurrentTerminal, strNroSEC)

            If codRespConvergente = CheckStr(ConfigurationSettings.AppSettings("codRespDocSapNoConvergente")) Then
                If strContrato <> "KO" And CDbl(strContrato) > 0 Then
                    dsAcuerdo = objPagos.Get_ConsultaAcuerdoPCS(strContrato)
                    If dsAcuerdo.Tables(0).Rows.Count > 0 Then
                        PorMigracion = dsAcuerdo.Tables(0).Rows(0).Item("MIGRACION")
                        PorRenovacion = dsAcuerdo.Tables(0).Rows(0).Item("RENOVACION")
                        PorReposicion = dsAcuerdo.Tables(0).Rows(0).Item("REPOSICION")
                        PorAprobador = dsAcuerdo.Tables(0).Rows(0).Item("APROBADOR")
                        EstadoActualAcuerdo = dsAcuerdo.Tables(0).Rows(0).Item("ESTADO")
                        strNroAprobacion = dsAcuerdo.Tables(0).Rows(0).Item("NRO_APROBACION")
                        'CARIAS
                        strNroSEC = dsAcuerdo.Tables(0).Rows(0).Item("NRO_CARPETA_OBS")
                        strNroDocSEC = dsAcuerdo.Tables(0).Rows(0).Item("CLIENTE")
                        strTipDocSEC = dsAcuerdo.Tables(0).Rows(0).Item("TIPO_DOC_CLIENTE")
                        strMotivoMig = dsAcuerdo.Tables(0).Rows(0).Item("MOTIVO_MIG")
                        strConSinEq = dsAcuerdo.Tables(0).Rows(0).Item("CON_SIN_EQ")
                        'FIN CARIAS

                        'WSOTOMAYOR: inicio Validar Activaciones Business
                        Dim strTipoCliAct
                        strTipoCliAct = dsAcuerdo.Tables(0).Rows(0).Item("TIPO_CLI_ACT")
                        'WSOTOMAYOR: Fin Validar Activaciones Business                       

                        dsResult = objPagos.Get_ConsultaEstadoPCS(strContrato)
                        For i = 0 To dsResult.Tables(0).Rows.Count - 1
                            If dsResult.Tables(0).Rows(i).Item("ESTADO") = "6" Then
                                strObsEstado = dsResult.Tables(0).Rows(i).Item("OBSERVACION")
                            End If
                        Next

                        If EstadoActualAcuerdo = "6" Then

                            'dsResult = objPagos.Get_ParamGlobal(Session("ALMACEN"))
                            dsResult = objPagos.Get_ParamGlobal(Session("ALMACEN"))
                            strIND_ACTIV_BSCS = dsResult.Tables(0).Rows(0).Item("IND_ACTIV_BSCS")

                            'Mejora en Bandeja de Renovaciones
                            'If dsAcuerdo.Tables(1).Rows(0).Item("COD_EQUIPO") <> "" Then
                            ' conEquipo = True
                            'Else
                            ' conEquipo = False
                            'End If

                            strValSAP = dsResult.Tables(0).Rows(0).Item("REN_REP_HDC")

                            If (Trim(PorRenovacion) <> "" Or Trim(PorReposicion) <> "") And (strValSAP <> "X") Then          'Not(valSAP=="X"): VIA_DSF = ‘S’
                                blnDebeSerRevisado = True
                            End If
                            If blnDebeSerRevisado Then
                                strEstado = ConfigurationSettings.AppSettings("CTE_ESTREVISADO")
                            Else
                                strEstado = ConfigurationSettings.AppSettings("CTE_ESTNUEVO")
                            End If

                            dsEstado = objPagos.Set_EstadoAcuerdoPCS(strContrato, strEstado, ConfigurationSettings.AppSettings("gStrUsuarioActivacionWS"), "", "", "", strObsEstado, "")

                            'If Trim(PorMigracion) <> "" Or Trim(PorRenovacion) <> "" Or Trim(PorReposicion) <> "" Then
                            If (Trim(PorMigracion) <> "" And Trim(strMotivoMig) = "") Or Trim(PorRenovacion) <> "" Or Trim(PorReposicion) <> "" Then
                                strRespuesta = ""
                            Else
                                If Trim(PorAprobador) = "4" Then
                                    strRespuesta = ""
                                Else

                                    'CARIAS: Post + Control

                                    'Dim strTIPO_ACTIV_CLTE As String
                                    'strTIPO_ACTIV_CLTE = drFila.Item("TIPO_ACTIV_CLTE")

                                    'If Trim(strTIPO_ACTIV_CLTE) = "" Then
                                    '    strTIPO_ACTIV_CLTE = 0
                                    'End If

                                    'If strTIPO_ACTIV_CLTE = 0 Then
                                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-----Inicio getCuentasCliente-----")
                                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Input: strTipDocSEC --> " & strTipDocSEC)
                                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Input: strNroDocSEC --> " & strNroDocSEC)
                                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Input: strNroAprobacion --> " & strNroAprobacion)

                                    '    Dim strResultEAI As String
                                    '    Dim objCuentas As New COM_SIC_Activaciones.clsCuenta
                                    '    Dim arrCuentas As New ArrayList
                                    '    Dim strMsgCuentasCliente As String
                                    '    arrCuentas = objActivaciones.getCuentasCliente(strTipDocSEC, strNroDocSEC, strNroAprobacion, strResultEAI, strMsgCuentasCliente)

                                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Outpu: strResultEAI --> " & strResultEAI)
                                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Outpu: strMsgCuentasCliente --> " & strMsgCuentasCliente)
                                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-----Fin getCuentasCliente-----")

                                    '    If strResultEAI = "1" Then
                                    '        Dim cantLineas As Integer = 0
                                    '        Dim cantCuentas As Integer = arrCuentas.Count
                                    '        For Each cuenta As COM_SIC_Activaciones.clsCuenta In arrCuentas
                                    '            If Not cuenta.linea Is Nothing Then
                                    '                cantLineas = cantLineas + cuenta.linea.Length
                                    '            End If
                                    '        Next
                                    '        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     cantLineas --> " & cantLineas.ToString())
                                    '        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     cantCuentas --> " & cantCuentas.ToString())
                                    '        If cantLineas <= 19 Then
                                    '            If cantCuentas <= 9 Then
                                    '                objPagos.Set_ActualizaPCS(strContrato, "1", "1", "")
                                    '                strTIPO_ACTIV_CLTE = "1"
                                    '            End If
                                    '        End If
                                    '    End If
                                    '    'strValores = strTipDocSEC & ";" & strNroDocSEC & ";" & strNroAprobacion             'dsSEC.Tables(0).Rows(0).Item("SOLIV_NUM_OPE_FIN")
                                    '    'strResultEAI = objActivaciones.FK_CuentasClienteRecurrente(strValores, ConfigurationSettings.AppSettings("gStrUrlSvrACR"), ConfigurationSettings.AppSettings("gStrServicioMCR"), ConfigurationSettings.AppSettings("gStrUsuarioACR"))
                                    '    'If UCase(Left(Trim(strResultEAI), 1)) <> "X" Then
                                    '    '    arrCampos = Split(strResultEAI, "?")
                                    '    '    arrValorError = Split(arrCampos(0), "$")
                                    '    '    arrCampoError = Split(arrCampos(1), "$")

                                    '    '    arrValorCuenta = Split(arrValorError(1), "|")
                                    '    '    arrNombreCuenta = Split(arrCampoError(1), "|")

                                    '    '    If UBound(Split(arrCampos(0), "#")) < 19 Then
                                    '    '        If UBound(arrValorCuenta) < 9 Then
                                    '    '            objPagos.Set_ActualizaPCS(, "1", "1", "")
                                    '    '            strTIPO_ACTIV_CLTE = "1"
                                    '    '        End If
                                    '    '    End If

                                    '    'End If
                                    '    'End If
                                    '    'End If
                                    '    'FIN Verifica Post + Control
                                    '    ' End If
                                    'End If
                                    'FIN CARIAS: Post + Control

                                    'Response.Write("<script language=javascript>alert('" & strTipoCliAct & "')</script>")

                                    'WSOTOMAYOR: inicio Validar Activaciones Business
                                    'If Len(Trim(strConSinEq)) > 0 Then
                                    'If Trim(strTipoCliAct) = "01" Then
                                    '    strTIPO_ACTIV_CLTE = "1"           ' se fuerza a la activacion recurrente para las ventas business cadenas
                                    'End If
                                    'FIN WSOTOMAYOR

                                    'Dim objFileLog As New SICAR_Log
                                    'Dim nameFile As String = "LogActivacionPostpago"
                                    'Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogCambioSIM")
                                    'Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
                                    'Dim strIdentifyLog As String = 

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-----Inicio Proceso Activacion-----")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input: IND_ACTIV_BSCS --> " & strIND_ACTIV_BSCS)


                                    'If Trim(drPagos.Item("NUMBR")) = "" Then							'CARIAS: Indica que el pedido no vino por Running Program 24/04/2007

                                    'VALIDAR EVERIS
                                    'If strIND_ACTIV_BSCS <> "" Then
                                    '    If CheckStr(drFila.Item("ACTIVACION_LINEA")) = ConfigurationSettings.AppSettings("gStrValorActLin") Then
                                    '        If CheckStr(strMotivoMig) <> "104" And PorMigracion = ConfigurationSettings.AppSettings("migracionRA") Then
                                    '            Try
                                    '                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Output : " & strContrato)
                                    '                strRespuesta = objActivaciones.FK_ActivacionClienteRecurrente(strContrato)
                                    '            Catch ex As Exception
                                    '                strRespuesta = "Error Activacion:" & ex.Message.ToString()
                                    '            End Try
                                    '            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Output strRespuesta: " & strRespuesta)
                                    '        End If
                                    '    End If
                                    'End If
                                    'End If
                                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-----Fin Proceso Activacion-----")
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            'CARIAS: SEC marcada como pagada
            If Len(Trim(strNroSEC)) > 0 Then
                objCajas.FP_Actualiza_Pago_Solicitud(strNroSEC, "X", "")
            End If
            'FIN CARIAS: SEC marcada como pagada

            'CARIAS: se comenta la llamada a esta RFC
            'dsPagoDep = objPagos.Set_ModificaDepositoGarantia(strItems)
            'For i = 0 To dsPagoDep.Tables(1).Rows.Count - 1
            '    If dsPagoDep.Tables(1).Rows(i).Item(0) = "E" Then
            '        strMensaje = dsPagoDep.Tables(1).Rows(i).Item(3)
            '    End If
            'Next
            'FIN CARIAS

            If Len(Trim(strMensaje)) = 0 Then
                If Len(Trim(dsLinea.Item(6))) > 0 Then  ' Numero de Pedido
                    If blnPrimerPago Then
                        If CLng(Trim(dsLinea.Item(6))) = 0 And CStr(dsLinea.Item(5)) <> "0000000000" Then  ' SI tiene contrato (DTH no tiene contrato)
                            dsAcuerdo = objPagos.Get_ConsultaAcuerdoPCS(dsLinea.Item(5))
                            dsAcuerdo = objPagos.Set_EstadoAcuerdoPCS(dsLinea.Item(5), "0", ConfigurationSettings.AppSettings("gStrUsuarioActivacionWS"), "", "", "", dsAcuerdo.Tables(0).Rows(0).Item("OBSERVACIONES"), "")
                        End If
                    End If
                End If

                If cboViaPago.SelectedValue = "ZEFE" Then
                    objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtImporte.Text))
                End If

                'diario electronico
                Dim intOper As Int32
                intOper = objCajas.FP_Cab_Oper(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), Session("USUARIO"), dsReturn.Tables(0).Rows(0).Item("CLIEC_TIPODOCCLIENTE"), txtNumDocumento.Text, drFila.Item("PAGOD_FECHACONTA"), drFila.Item("PEDIN_NROPEDIDO"), strXBLNR, CDbl(txtImporte.Text), 0, CDbl(txtImporte.Text), "P")
                objCajas.FP_Det_Oper(intOper, 1, "", "", "", 0, CDbl(txtImporte.Text), 0, CDbl(txtImporte.Text))
                objCajas.FP_Pag_Oper(intOper, 1, cboViaPago.SelectedValue, "", CDbl(txtImporte.Text))

                Dim sUrl As String = "PoolPagos.aspx"

                'Dim strNroDG As String = drFila.Item("NRO_DEP_GARANTIA") TODOEB
                Dim strTipDoc As String = drFila.Item("PAGOD_FECHACONTA")

                If chkImprimir.Checked Then
                    ''strNroDG + _
                    sUrl += "?pImp=S" + _
                              "&pDocSap=" + _
                              "&pDocSunat=" + _
                              "&pNroDG=" + "" + _
                              "&pTipDoc=" + strTipDoc

                Else
                    sUrl += "?pImp=N"
                End If

                Response.Redirect(sUrl)

            Else
                Response.Write("<script language=javascript>alert('" & strMensaje & "');</script>")
            End If
        Else
            Response.Write("<script language=javascript>alert('" & strMensaje & "');</script>")
        End If


    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("PoolPagos.aspx")
    End Sub

    Private Function GenerarArchivoTramaSGA(ByVal sNroContrato As String, ByVal strDocSapDTH As String, ByRef nombreArchivo As String) As Boolean
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim dtDTH As DataSet
        Dim cadena1DTH, cadena6DTH, codErrorDTH, descErrorDTH As String

        Dim prefijoArchivo As String = ConfigurationSettings.AppSettings("PrefijoArchivoSGA").ToString()
        Dim strRutaDestino As String = ConfigurationSettings.AppSettings("strRutaDestinoDTH")
        Dim strRutaOrigen As String = ConfigurationSettings.AppSettings("strRutaOrigenDTH")

        Dim sb As StringBuilder = New StringBuilder
        Dim sw As StringWriter = New StringWriter(sb)
        Dim idLog As String = strDocSapDTH

        Try
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Inicio ObtenerTramaSGA")
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp sNroContrato:" & sNroContrato)
            dtDTH = objCajas.ObtenerTramaSGA(sNroContrato, cadena1DTH, cadena6DTH, codErrorDTH, descErrorDTH)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out codErrorDTH:" & codErrorDTH)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Fin ObtenerTramaSGA")

            If codErrorDTH.Equals("00") Then

                sw.Write(cadena1DTH)
                sw.Write(sw.NewLine)
                sw.Write(dtDTH.Tables(1).Rows(0).Item(0))
                sw.Write(sw.NewLine)
                sw.Write(dtDTH.Tables(2).Rows(0).Item(0))
                If dtDTH.Tables(0).Rows.Count > 0 Then
                    For dth As Integer = 0 To dtDTH.Tables(0).Rows.Count - 1
                        sw.Write(sw.NewLine)
                        sw.Write(dtDTH.Tables(0).Rows(dth).Item(0))
                    Next
                End If
                sw.Write(sw.NewLine)
                sw.Write(cadena6DTH)
                sw.Close()

                Dim strFecha As String = String.Format("{0:ddMMyy}", Now)
                Dim correlativoSGA As String = objCajas.getCorrelativoSGA()
                objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "correlativoSGA:" & correlativoSGA)
                If correlativoSGA = "" Then
                    correlativoSGA = sNroContrato
                End If
                nombreArchivo = prefijoArchivo & strFecha & correlativoSGA
                saveFile(nombreArchivo, sb.ToString)

                objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Archivo DTH:" & nombreArchivo & ":" & sb.ToString())

                Dim mensajeFTP As String
                objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Inicio UploadArchivoFTP")
                objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp strRutaOrigen:" & strRutaOrigen)
                objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp nombreArchivo:" & nombreArchivo)
                objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp strRutaDestino:" & strRutaDestino)
                mensajeFTP = UploadArchivoFTPSGA(strRutaOrigen, nombreArchivo, strRutaDestino)
                objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out mensajeFTP:" & mensajeFTP)
                objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Fin UploadArchivoFTP")

                If mensajeFTP <> "" Then
                    Response.Write("<script> alert('No se pudo enviar archivo de sincronización DTH.'); </script>")
                    Return False
                Else
                    Return True
                End If
            Else
                objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "ERROR:" & descErrorDTH)
                Return False
            End If
        Catch ex As Exception
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "ERROR GenerarArchivoTramaSGA:" & ex.Message.ToString())
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "ERROR GenerarArchivoTramaSGA:" & ex.StackTrace.ToString())
            Return False
        End Try

    End Function

    Public Function saveFile(ByVal strNombreArchivoLog As String, ByVal strTexto As String) As String
        Dim objFSO As Scripting.FileSystemObject
        Dim objFile0 As Scripting.TextStream
        Dim Archivo As String
        Dim Resul As String = ""
        Dim strLOGPATH As String = ConfigurationSettings.AppSettings("strRutaOrigenDTH")

        Try
            Archivo = strLOGPATH & "\" & strNombreArchivoLog.Trim()
            objFSO = New Scripting.FileSystemObject
            If Not objFSO.FileExists(Archivo) Then
                objFile0 = objFSO.CreateTextFile(Archivo, True, False)
            Else
                objFSO.DeleteFile(Archivo)
                objFile0 = objFSO.CreateTextFile(Archivo, True, False)
            End If

            objFile0.Write(strTexto)

            objFile0.Close()
            objFile0 = Nothing
            objFSO = Nothing
        Catch ex As Exception
            objFSO = Nothing
            Resul = ex.Message
        End Try
        Return Resul
    End Function

    Private Function UploadArchivoFTPSGA(ByVal rutaOrigen As String, ByVal nombreArchivo As String, ByVal rutaDestino As String) As String

        Dim strMensaje As String = ""

        Try
            Dim pRutaArchivoOrigen As String
            Dim pRutaArchivoDestino As String
            Dim objFileFTP As New ControlProxyFTP

            pRutaArchivoOrigen = rutaOrigen & "\" & nombreArchivo
            pRutaArchivoDestino = rutaDestino & "/" & nombreArchivo
            objFileFTP.PutFileFTPSGA(pRutaArchivoOrigen, pRutaArchivoDestino)

        Catch ex As Exception
            objLog.Log_WriteLog(pathFile, strArchivo, "ERROR UploadArchivoFTP: " & ex.Message.ToString())
            objLog.Log_WriteLog(pathFile, strArchivo, "ERROR UploadArchivoFTP: " & ex.StackTrace.ToString())
            strMensaje = ex.Message()
        End Try

        Return strMensaje

    End Function

    Private Function GenerarSot(ByVal nroSec As String, ByVal strDocSap As String) As SGAResponseVenta
        Dim oConsulta As New clsBDSiscajas
        Dim oTransaccion As New SGATransaction
        Dim oVenta As New SGAVenta
        Dim oListaServicios As New ArrayList
        Dim oListaPromocion As New ArrayList
        Dim idLog As String = strDocSap & " - " & nroSec
        Dim oSGAResponseTrs As New SGAResponseVenta
        oSGAResponseTrs.codRepuesta = -1
        Dim cadenaRegVenta, cadenaServicio, cadenaPromocion As String

        Try

            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Inicio ObtenerDatosVentaSGA")
            oConsulta.ObtenerDatosVentaSGA(nroSec, oVenta, oListaServicios, oListaPromocion)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Fin ObtenerDatosVentaSGA")

            oVenta.usuarioRegistro = CurrentUser
            cadenaRegVenta = oTransaccion.ObtenerCadenaRegVentaSGA(oVenta)
            cadenaServicio = oTransaccion.ObtenerCadenaServicioSGA(oListaServicios)
            cadenaPromocion = oTransaccion.ObtenerCadenaPromocionSGA(oListaPromocion)

            Dim oAudit As New ItemGenerico
            oAudit.CODIGO = nroSec
            oAudit.DESCRIPCION = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            oAudit.DESCRIPCION2 = CurrentTerminal

            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Inicio GenerarSot")
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "WS    : " & ConfigurationSettings.AppSettings("constSGATransaccion_Url"))
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Method: " & "procesarGeneracionProy")
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp cadenaRegVenta: " & cadenaRegVenta)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp cadenaServicio: " & cadenaServicio)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp cadenaPromocion: " & cadenaPromocion)

            oSGAResponseTrs = oTransaccion.GenerarSot(cadenaRegVenta, cadenaServicio, cadenaPromocion, oAudit)

            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out codRepuesta: " & oSGAResponseTrs.codRepuesta)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out msgRepuesta: " & oSGAResponseTrs.msgRepuesta)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out codsolot: " & oSGAResponseTrs.codsolot)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out numslc: " & oSGAResponseTrs.numslc)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out fecagenda: " & oSGAResponseTrs.fecagenda)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out hora: " & oSGAResponseTrs.hora)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Fin GenerarSot")
        Catch ex As Exception
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "ERROR GenerarSot: " & ex.Message.ToString())
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "ERROR GenerarSot: " & ex.StackTrace.ToString())

            Dim oAnular As New clsAnulaciones
            Dim oSGAResponse As New SGAResponseVenta
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Inicio RollBackSot")
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp nroSec: " & nroSec)
            oSGAResponse = oAnular.RollBackSot(nroSec, strDocSap, CurrentTerminal)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out codRepuesta: " & oSGAResponse.codRepuesta)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out msgRepuesta: " & oSGAResponse.msgRepuesta)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Fin RollBackSot")
            Throw New Exception("No se pudo generar la SOT.")
        End Try
        Return oSGAResponseTrs
    End Function
End Class
