Imports SisCajas.Funciones
Imports System.Globalization
Public Class detaConsultaPagos
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents txtNumFact As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNumFactSunat As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNeto As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCuotaIni As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtSaldo As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents dgrDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtFecha As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtOfiVta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtViasPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtFechaPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtCliente As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtMontos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtNumPedido As System.Web.UI.HtmlControls.HtmlInputHidden

    'PROY-27440 INI
    Protected WithEvents btnAutorizar As System.Web.UI.WebControls.Button
    Protected WithEvents HidCodOpera As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidDesOpera As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidTipoOpera As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidTipoTran As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidTipoPOS As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidEstTrans As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidPtoVenta As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidNroPedido As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidIntAutPos As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidTipoMoneda As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidTransMC As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidApliPOS As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidTipoTarjeta As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidTipoPago As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidEstAnulGrilla As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents lblTipoTarjeta As System.Web.UI.WebControls.Label
    Protected WithEvents hidIsVerFormPag As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidNroTelefono As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidAutorizacion As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents lblEnvioPos As System.Web.UI.WebControls.Label
    Protected WithEvents HidDatoPosVisa As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidDatoPosMC As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidFila0 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidFila1 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidFila2 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidFila3 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidFila4 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidContIntento0 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidContIntento1 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidContIntento2 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidContIntento3 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidContIntento4 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidIdCabez As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidDatoAuditPos As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidAnulacionExitosa As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidAnulacionRechazada As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidAnulacionIncompleta As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidCajeroPOS As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidUsuario As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidMsjCajero As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidMsjIpDesconfigurado As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidIPTransaccion As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidIpLocal As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents HidTipMonPOSVISA As System.Web.UI.HtmlControls.HtmlInputText

    'PROY-27440 FIN
    'PROY-31949
    Protected WithEvents HidNumIntentosAnular As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjErrorNumIntentos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjErrorTimeOut As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjNumIntentosPago As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected WithEvents HidFlagCajaCerrada As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjCajaCerrada As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidGuardarTrans As System.Web.UI.HtmlControls.HtmlInputHidden
    'PROY-31949
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    'PROY-27440 INI
    Dim nameFilePos As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivoPos As String = objFileLog.Log_CrearNombreArchivo(nameFilePos)
    Dim bolIsVerFormPag As Boolean
    Dim strIdentifyLog As String
    Dim strIdentifyLogPOS As String
    Dim bolPagoTarjetaManual As Boolean

    'PROY-27440 FIN
    Dim objLog As New SICAR_Log
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim drDocSel As DataRow
            Dim strOficinaVta As String
            If Not Page.IsPostBack Then
                If Not Session("drFilaDoc") Is Nothing Then
                    drDocSel = CType(Session("drFilaDoc"), DataRow)
                    strOficinaVta = Request.QueryString("pc_OfiVta")
                    txtFecha.Value = Request.QueryString("pc_fecha")
                    'PROY-27440 INI
                    bolIsVerFormPag = Request.QueryString("FormaPago")
                    If bolIsVerFormPag = True Then
                        hidIsVerFormPag.Value = "0" ''Entra como Forma de Pago
                    Else
                        hidIsVerFormPag.Value = "1" ''Entra a anular
                    End If
                    hidUsuario.Value = Session("USUARIO")
                    'PROY-27440 FIN
                    txtOfiVta.Value = strOficinaVta
                    hidNroTelefono.Value = Request.QueryString("numeroTelefono") 'PROY-27440 
                    load_data_param_pos(drDocSel) 'PROY-27440 
                    LeeDatos(drDocSel, strOficinaVta)
                End If
            End If
            CargarGrilla() 'PROY-31949
            Me.load_values_pos()  'PROY-27440 
        End If
    End Sub
    'PROY-31949 PROY-30166
    Private Sub CargarGrilla(Optional ByVal CI As Decimal = 0)

        Dim dsDetalle As DataTable
        dsDetalle = CType(Session("FormasPAgo"), DataTable)


        If HidIntAutPos.Value = "1" Then

            strIdentifyLogPOS = "detaConsultaPagos: [" + txtNumPedido.Value + "] "
            Dim dtFormasPago As New DataTable
            Dim dtFormasPagoTrans As New DataTable
            Dim dtFormasPagoTransVIS As New DataTable
            Dim dtFormasPagoTransMCD As New DataTable
            Dim dtFormasPagoTransAMX As New DataTable
            Dim dtFormasPagoTransDIN As New DataTable
            Dim i As Integer
            Dim objSicarDB As New COM_SIC_Activaciones.clsTransaccionPOS
            Dim strCodRptTipPos As String = ""
            Dim strMsgRptTipPos As String = ""
            Dim strCodRptForm As String = ""
            Dim strMsgRptForm As String = ""
            Dim strOficina As String = Session("ALMACEN")
            Dim strTipoPago As String = ClsKeyPOS.strTipPagoDP '' 11 Documentos Pagados


            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " ***********************************************************")
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " *******************LEERDATOS() - INICIO *******************")


            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "   *** INI ObtenerFormasDePagoTrans *** ")
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "       PKG_SISCAJ_POS.SICASS_DETAPAGO ")

            dtFormasPagoTransVIS = objSicarDB.ObtenerFormasDePagoTrans(txtNumPedido.Value, "VIS", "", strTipoPago, strCodRptForm, strMsgRptForm).Tables(0)
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "    IN: " & txtNumPedido.Value & "     IN: VIS     Tipo Pago: " & strTipoPago & "     OUT Cod.Rpta: " & strCodRptForm & "     OUT Msj. Rpta: " & strMsgRptForm)

            dtFormasPagoTransMCD = objSicarDB.ObtenerFormasDePagoTrans(txtNumPedido.Value, "MCD", "", strTipoPago, strCodRptForm, strMsgRptForm).Tables(0)
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "    IN: " & txtNumPedido.Value & "     IN: MCD     Tipo Pago: " & strTipoPago & "     OUT Cod.Rpta: " & strCodRptForm & "     OUT Msj. Rpta: " & strMsgRptForm)

            dtFormasPagoTransAMX = objSicarDB.ObtenerFormasDePagoTrans(txtNumPedido.Value, "AMX", "", strTipoPago, strCodRptForm, strMsgRptForm).Tables(0)
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "    IN: " & txtNumPedido.Value & "     IN: AMX     Tipo Pago: " & strTipoPago & "     OUT Cod.Rpta: " & strCodRptForm & "     OUT Msj. Rpta: " & strMsgRptForm)

            dtFormasPagoTransDIN = objSicarDB.ObtenerFormasDePagoTrans(txtNumPedido.Value, "DIN", "", strTipoPago, strCodRptForm, strMsgRptForm).Tables(0)
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "    IN: " & txtNumPedido.Value & "     IN: DIN     Tipo Pago: " & strTipoPago & "     OUT Cod.Rpta: " & strCodRptForm & "     OUT Msj. Rpta: " & strMsgRptForm)
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "   *** FIN ObtenerFormasDePagoTrans ***")

            If Not dtFormasPagoTransVIS Is Nothing AndAlso dtFormasPagoTransVIS.Rows.Count > 0 Or _
                        Not dtFormasPagoTransMCD Is Nothing AndAlso dtFormasPagoTransMCD.Rows.Count > 0 Or _
                        Not dtFormasPagoTransAMX Is Nothing AndAlso dtFormasPagoTransAMX.Rows.Count > 0 Or _
                        Not dtFormasPagoTransDIN Is Nothing AndAlso dtFormasPagoTransDIN.Rows.Count > 0 Then

                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "    Total Pagos VISA: " & dtFormasPagoTransVIS.Rows.Count)
                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "    Total Pagos MCD: " & dtFormasPagoTransMCD.Rows.Count)
                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "    Total Pagos AMX: " & dtFormasPagoTransAMX.Rows.Count)
                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "    Total Pagos DIN: " & dtFormasPagoTransDIN.Rows.Count)

                For i = 0 To dsDetalle.Rows.Count - 1
                    Dim strCodTarjSAP As String = dsDetalle.Rows(i).Item("DEPAV_FORMAPAGO")

                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " == " & "   *** INICIO Obtener_TipoPOS == ")
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & "  = " & "       PKG_SISCAJ_POS.SICASS_VIAS_PAGO  ")
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & "  = " & "        IN Cod SAP: " & strCodTarjSAP)
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & "  = " & "        IN Cod Oficina: " & strOficina)
                    Dim strResult As String = objSicarDB.Obtener_TipoPOS(strCodTarjSAP, strOficina, strCodRptTipPos, strMsgRptTipPos)
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & "  = " & "        OUT Cod Rpta: " & strCodRptTipPos)
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: CargarGrilla)"
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & "  = " & "        OUT Msj Rpta: " & strMsgRptTipPos & MaptPath)
                    'FIN PROY-140126
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " == " & "   *** FIN Obtener_TipoPOS == ")

                    If strCodRptTipPos = "0" And (strResult.Split("#")(0)).Length > 0 Then
                        Dim strCodTarjEquival As String
                        Dim ArrFormasPago As ArrayList
                        strCodTarjEquival = strResult.Split("#")(0) '' MCD --> MCD#TARJETA CMR SAGA

                        dsDetalle.Rows.Item(i)("EQUIVALENCIA") = strCodTarjEquival '' MCD / VIS

                        If strCodTarjEquival = "VIS" And dtFormasPagoTransVIS.Rows.Count > 0 Then

                            For j As Integer = 0 To dtFormasPagoTransVIS.Rows.Count - 1

                                If dsDetalle.Rows.Item(i)("DEPAN_IMPORTE") = dtFormasPagoTransVIS.Rows(j).Item("MONTO") Then
                            dsDetalle.Rows.Item(i)("INDICE") = i
                                    HidIdCabez.Value = dtFormasPagoTransVIS.Rows(j).Item("TRNSN_ID_CAB")
                                    hidCajeroPOS.Value = dtFormasPagoTransVIS.Rows(j).Item("CAJERO")
                                    hidIPTransaccion.Value = dtFormasPagoTransVIS.Rows(j).Item("POSV_IPCAJA")
                                    dsDetalle.Rows.Item(i)("FORMA_PAGO") = dtFormasPagoTransVIS.Rows(j).Item("FORMA_PAGO") 'TARJETA VISA/TARJETA MASTERCARD /AMERICAN EXPRESS
                                    dsDetalle.Rows.Item(i)("NRO_REFERENCIAPOS") = dtFormasPagoTransVIS.Rows(j).Item("TRNSV_ID_REF")
                                    dsDetalle.Rows.Item(i)("TIPO_TARJETA") = dtFormasPagoTransVIS.Rows(j).Item("TIPO_TARJETA") ' VISA/MASTERCARD
                                    dsDetalle.Rows.Item(i)("NRO_TARJETA") = dtFormasPagoTransVIS.Rows(j).Item("NRO_TARJETA")
                                    dsDetalle.Rows.Item(i)("TIPO_TARJETAPOS") = dtFormasPagoTransVIS.Rows(j).Item("TIPO_TARJETA_POS") 'VISA AMEX MASTERCARD
                                    dsDetalle.Rows.Item(i)("ESTADO_ANULACION") = dtFormasPagoTransVIS.Rows(j).Item("ESTADO_ANULACION")
                                    dsDetalle.Rows.Item(i)("RESULTADO_PROCESO") = dtFormasPagoTransVIS.Rows(j).Item("RESULTADO_PROCESO")

                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " ### VISA - INICIO ###")
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "       HidIdCabez: " & HidIdCabez.Value)
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "       hidCajeroPOS: " & hidCajeroPOS.Value)
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "       hidIPTransaccion: " & hidIPTransaccion.Value)
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "       Forma Pago: " & dtFormasPagoTransVIS.Rows(j).Item("FORMA_PAGO"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "       ID Referencia: " & dtFormasPagoTransVIS.Rows(j).Item("TRNSV_ID_REF"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "       Tipo Tarjeta: " & dtFormasPagoTransVIS.Rows(j).Item("TIPO_TARJETA"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "       Nº Tarjeta: " & dtFormasPagoTransVIS.Rows(j).Item("NRO_TARJETA"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "       Tipo Tarjeta: " & dtFormasPagoTransVIS.Rows(j).Item("TIPO_TARJETA_POS"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "       Estado Anulación: " & dtFormasPagoTransVIS.Rows(j).Item("ESTADO_ANULACION"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "       Resultado Proceso: " & dtFormasPagoTransVIS.Rows(j).Item("RESULTADO_PROCESO"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " ### VISA - FIN ###")

                                    dtFormasPagoTransVIS.Rows.RemoveAt(j)

                                    Exit For
                            End If
                            Next

                        ElseIf strCodTarjEquival = "MCD" And dtFormasPagoTransMCD.Rows.Count > 0 Then

                            For j As Integer = 0 To dtFormasPagoTransMCD.Rows.Count - 1

                                If dsDetalle.Rows.Item(i)("DEPAN_IMPORTE") = dtFormasPagoTransMCD.Rows(j).Item("MONTO") Then

                            dsDetalle.Rows.Item(i)("INDICE") = i
                                    HidIdCabez.Value = dtFormasPagoTransMCD.Rows(j).Item("TRNSN_ID_CAB")
                                    hidCajeroPOS.Value = dtFormasPagoTransMCD.Rows(j).Item("CAJERO")
                                    hidIPTransaccion.Value = dtFormasPagoTransMCD.Rows(j).Item("POSV_IPCAJA")
                                    dsDetalle.Rows.Item(i)("FORMA_PAGO") = dtFormasPagoTransMCD.Rows(j).Item("FORMA_PAGO") 'TARJETA VISA/TARJETA MASTERCARD /AMERICAN EXPRESS
                                    dsDetalle.Rows.Item(i)("NRO_REFERENCIAPOS") = dtFormasPagoTransMCD.Rows(j).Item("TRNSV_ID_REF")
                                    dsDetalle.Rows.Item(i)("TIPO_TARJETA") = dtFormasPagoTransMCD.Rows(j).Item("TIPO_TARJETA") ' VISA/MASTERCARD
                                    dsDetalle.Rows.Item(i)("NRO_TARJETA") = dtFormasPagoTransMCD.Rows(j).Item("NRO_TARJETA")
                                    dsDetalle.Rows.Item(i)("TIPO_TARJETAPOS") = dtFormasPagoTransMCD.Rows(j).Item("TIPO_TARJETA_POS") 'VISA AMEX MASTERCARD
                                    dsDetalle.Rows.Item(i)("ESTADO_ANULACION") = dtFormasPagoTransMCD.Rows(j).Item("ESTADO_ANULACION")
                                    dsDetalle.Rows.Item(i)("RESULTADO_PROCESO") = dtFormasPagoTransMCD.Rows(j).Item("RESULTADO_PROCESO")

                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " ### MCD - INICIO ###")
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "      HidIdCabez: " & HidIdCabez.Value)
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "      hidCajeroPOS: " & hidCajeroPOS.Value)
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "      hidIPTransaccion: " & hidIPTransaccion.Value)
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "      Forma Pago: " & dtFormasPagoTransMCD.Rows(j).Item("FORMA_PAGO"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "      ID Referencia: " & dtFormasPagoTransMCD.Rows(j).Item("TRNSV_ID_REF"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "      Tipo Tarjeta: " & dtFormasPagoTransMCD.Rows(j).Item("TIPO_TARJETA"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "      Nº Tarjeta: " & dtFormasPagoTransMCD.Rows(j).Item("NRO_TARJETA"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "      Tipo Tarjeta: " & dtFormasPagoTransMCD.Rows(j).Item("TIPO_TARJETA_POS"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "      Estado Anulación: " & dtFormasPagoTransMCD.Rows(j).Item("ESTADO_ANULACION"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "      Resultado Proceso: " & dtFormasPagoTransMCD.Rows(j).Item("RESULTADO_PROCESO"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " ### MCD - FIN ###")

                                    dtFormasPagoTransMCD.Rows.RemoveAt(j)
                                    Exit For
                            End If


                            Next

                        ElseIf strCodTarjEquival = "AMX" And dtFormasPagoTransAMX.Rows.Count > 0 Then

                            For j As Integer = 0 To dtFormasPagoTransAMX.Rows.Count - 1

                                If dsDetalle.Rows.Item(i)("DEPAN_IMPORTE") = dtFormasPagoTransAMX.Rows(j).Item("MONTO") Then
                            dsDetalle.Rows.Item(i)("INDICE") = i
                                    HidIdCabez.Value = dtFormasPagoTransAMX.Rows(j).Item("TRNSN_ID_CAB")
                                    hidCajeroPOS.Value = dtFormasPagoTransAMX.Rows(j).Item("CAJERO")
                                    hidIPTransaccion.Value = dtFormasPagoTransAMX.Rows(j).Item("POSV_IPCAJA")
                                    dsDetalle.Rows.Item(i)("FORMA_PAGO") = dtFormasPagoTransAMX.Rows(j).Item("FORMA_PAGO") 'TARJETA VISA/TARJETA MASTERCARD /AMERICAN EXPRESS
                                    dsDetalle.Rows.Item(i)("NRO_REFERENCIAPOS") = dtFormasPagoTransAMX.Rows(j).Item("TRNSV_ID_REF")
                                    dsDetalle.Rows.Item(i)("TIPO_TARJETA") = dtFormasPagoTransAMX.Rows(j).Item("TIPO_TARJETA") ' VISA/MASTERCARD
                                    dsDetalle.Rows.Item(i)("NRO_TARJETA") = dtFormasPagoTransAMX.Rows(j).Item("NRO_TARJETA")
                                    dsDetalle.Rows.Item(i)("TIPO_TARJETAPOS") = dtFormasPagoTransAMX.Rows(j).Item("TIPO_TARJETA_POS") 'VISA AMEX MASTERCARD
                                    dsDetalle.Rows.Item(i)("ESTADO_ANULACION") = dtFormasPagoTransAMX.Rows(j).Item("ESTADO_ANULACION")
                                    dsDetalle.Rows.Item(i)("RESULTADO_PROCESO") = dtFormasPagoTransAMX.Rows(j).Item("RESULTADO_PROCESO")

                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " ### AMX - INICIO ###")
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     HidIdCabez: " & HidIdCabez.Value)
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     hidCajeroPOS: " & hidCajeroPOS.Value)
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     hidIPTransaccion: " & hidIPTransaccion.Value)
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     Forma Pago: " & dtFormasPagoTransAMX.Rows(j).Item("FORMA_PAGO"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     ID Referencia: " & dtFormasPagoTransAMX.Rows(j).Item("TRNSV_ID_REF"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     Tipo Tarjeta: " & dtFormasPagoTransAMX.Rows(j).Item("TIPO_TARJETA"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     Nº Tarjeta: " & dtFormasPagoTransAMX.Rows(j).Item("NRO_TARJETA"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     Tipo Tarjeta: " & dtFormasPagoTransAMX.Rows(j).Item("TIPO_TARJETA_POS"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     Estado Anulación: " & dtFormasPagoTransAMX.Rows(j).Item("ESTADO_ANULACION"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     Resultado Proceso: " & dtFormasPagoTransAMX.Rows(j).Item("RESULTADO_PROCESO"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " ### AMX - FIN ###")

                                    dtFormasPagoTransAMX.Rows.RemoveAt(j)

                                    Exit For

                            End If
                            Next

                        ElseIf strCodTarjEquival = "DIN" And dtFormasPagoTransDIN.Rows.Count > 0 Then

                            For j As Integer = 0 To dtFormasPagoTransDIN.Rows.Count - 1

                                If dsDetalle.Rows.Item(i)("DEPAN_IMPORTE") = dtFormasPagoTransDIN.Rows(j).Item("MONTO") Then
                            dsDetalle.Rows.Item(i)("INDICE") = i
                                    HidIdCabez.Value = dtFormasPagoTransDIN.Rows(j).Item("TRNSN_ID_CAB")
                                    hidCajeroPOS.Value = dtFormasPagoTransDIN.Rows(j).Item("CAJERO")
                                    hidIPTransaccion.Value = dtFormasPagoTransDIN.Rows(j).Item("POSV_IPCAJA")
                                    dsDetalle.Rows.Item(i)("FORMA_PAGO") = dtFormasPagoTransDIN.Rows(j).Item("FORMA_PAGO") 'TARJETA VISA/TARJETA MASTERCARD /AMERICAN EXPRESS
                                    dsDetalle.Rows.Item(i)("NRO_REFERENCIAPOS") = dtFormasPagoTransDIN.Rows(j).Item("TRNSV_ID_REF")
                                    dsDetalle.Rows.Item(i)("TIPO_TARJETA") = dtFormasPagoTransDIN.Rows(j).Item("TIPO_TARJETA") ' VISA/MASTERCARD
                                    dsDetalle.Rows.Item(i)("NRO_TARJETA") = dtFormasPagoTransDIN.Rows(j).Item("NRO_TARJETA")
                                    dsDetalle.Rows.Item(i)("TIPO_TARJETAPOS") = dtFormasPagoTransDIN.Rows(j).Item("TIPO_TARJETA_POS") 'VISA AMEX MASTERCARD
                                    dsDetalle.Rows.Item(i)("ESTADO_ANULACION") = dtFormasPagoTransDIN.Rows(j).Item("ESTADO_ANULACION")
                                    dsDetalle.Rows.Item(i)("RESULTADO_PROCESO") = dtFormasPagoTransDIN.Rows(j).Item("RESULTADO_PROCESO")

                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " ### DIN - INICIO ###")
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     HidIdCabez: " & HidIdCabez.Value)
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     hidCajeroPOS: " & hidCajeroPOS.Value)
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     hidIPTransaccion: " & hidIPTransaccion.Value)
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     Forma Pago: " & dtFormasPagoTransDIN.Rows(j).Item("FORMA_PAGO"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     ID Referencia: " & dtFormasPagoTransDIN.Rows(j).Item("TRNSV_ID_REF"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     Tipo Tarjeta: " & dtFormasPagoTransDIN.Rows(j).Item("TIPO_TARJETA"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     Nº Tarjeta: " & dtFormasPagoTransDIN.Rows(j).Item("NRO_TARJETA"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     Tipo Tarjeta: " & dtFormasPagoTransDIN.Rows(j).Item("TIPO_TARJETA_POS"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     Estado Anulación: " & dtFormasPagoTransDIN.Rows(j).Item("ESTADO_ANULACION"))
                                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "     Resultado Proceso: " & dtFormasPagoTransDIN.Rows(j).Item("RESULTADO_PROCESO"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " ### DIN - FIN ###")

                                    dtFormasPagoTransDIN.Rows.RemoveAt(j)
                                    Exit For
                        End If

                            Next

                        Else



                        End If
                    End If
                Next

                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "   *** INI Validando si es Transacción del Cajero *** ")
                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "       IP LOCAL : " & hidIpLocal.Value)
                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "       IP DONDE SE REALIZÓ EL PAGO : " & hidIPTransaccion.Value)

                If hidIpLocal.Value <> hidIPTransaccion.Value Then
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " -        " & ClsKeyPOS.strMsjValidacionCajero)
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "   *** FIN Validando si es Transacción del Cajero *** ")
                Else
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " -        Transaccion se realizó en esta Caja.")
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "   *** FIN Validando si es Transacción del Cajero *** ")
                End If
                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " ******************* LEERDATOS() - FIN   *******************")
            End If
        End If  'Integración Automática Activa
        'PROY-30166 INI
        Dim strTipoPagouP As String
        If (CI > 0) Then
            For x As Int32 = dsDetalle.Rows.Count - 1 To 0 Step -1
                strTipoPagouP = dsDetalle.Rows(x).ItemArray(2)

                If strTipoPagouP = "ZCUO" Then
                    dsDetalle.Rows.RemoveAt(x)
                End If
            Next

        End If

        Session("FormasPAgo") = Nothing
        Session("FormasPAgo") = dsDetalle

        dgrDetalle.DataSource = dsDetalle
        dgrDetalle.DataBind()

        'PROY-30166 FIN
    End Sub

    Private Sub LeeDatos(ByVal drDocSel As DataRow, ByVal strOficinaVta As String)
        Dim strMensaje As String


        txtNumFact.Value = CType(drDocSel("PEDIN_NROPEDIDO"), String)
        txtNumFactSunat.Value = CType(drDocSel("PAGOC_CODSUNAT"), String)
        txtCuotaIni.Value = CType(drDocSel("PAGON_INICIAL"), Decimal).ToString("N2")
        txtNeto.Value = CType(drDocSel("PEDIN_PAGO"), Decimal).ToString("N2")
        txtSaldo.Value = CType(drDocSel("PEDIN_SALDO"), Decimal).ToString("N2")
        txtFechaPago.Value = CType(drDocSel("PEDID_FECHADOCUMENTO"), String)
        txtCliente.Value = CStr(drDocSel("PEDIV_NOMBRECLIENTE"))
        txtNumPedido.Value = CStr(drDocSel("PEDIN_NROPEDIDO"))

        'PROY-30166 INI
        'txtNeto.Value = CType(drDocSel("PEDIN_PAGO"), Decimal).ToString("N2")
        Dim Neto As Decimal = CType(drDocSel("PEDIN_PAGO"), Decimal).ToString("N2")
        Dim CI As Decimal = CType(drDocSel("PAGON_INICIAL"), Decimal).ToString("N2")
        If CI > 0 Then
            txtNeto.Value = Neto + CI
        Else
            txtNeto.Value = Neto
        End If
        'PROY-30166 FIN
        Dim obPagos As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim dsDetalle As DataTable = obPagos.ConsultarFormasPago(txtNumPedido.Value)

        'PROY-27440 INI
        bolPagoTarjetaManual = False
        dsDetalle.Columns.Add("INDICE", GetType(String))
        dsDetalle.Columns.Add("EQUIVALENCIA", GetType(String))
        dsDetalle.Columns.Add("TRANSACCION_ID", GetType(String))
        dsDetalle.Columns.Add("FORMA_PAGO", GetType(String))
        dsDetalle.Columns.Add("NRO_REFERENCIAPOS", GetType(String))
        dsDetalle.Columns.Add("TIPO_TARJETA", GetType(String))
        dsDetalle.Columns.Add("NRO_TARJETA", GetType(String))
        dsDetalle.Columns.Add("TIPO_TARJETAPOS", GetType(String))
        dsDetalle.Columns.Add("ESTADO_ANULACION", GetType(String))
        dsDetalle.Columns.Add("RESULTADO_PROCESO", GetType(String))

        Session("FormasPAgo") = Nothing
        Session("FormasPAgo") = dsDetalle
        CargarGrilla(CI)'PROY-30166

        'PROY-27440 FIN
        'If dsDetalle.Rows.Count <= 0 Then
        '    strMensaje = "No se Obtuvieron las Formas de Pago"
        'End If

        'If Len(strMensaje) > 0 Then
        '    Session("strMensajeCaja") = strMensaje
        '    Regresar()
        'End If

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Regresar()
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click


        Dim sDocumentoSap As String = Request.Item("rbPagos")
        Dim obj As New COM_SIC_Cajas.clsCajas
        Dim ConsultaMssap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim TrsMssap As New COM_SIC_Activaciones.clsTrsMsSap
        '******WEB SERVICE DE ANULACIONES
        Dim objClsPagosWS As New COM_SIC_Activaciones.clsPagosWS

        'Variables
        Dim dsPedido As DataSet
        Dim AnuID As DataSet
        Dim CodAnulacion As String
        Dim K_ANUPN_ID As Integer
        Dim strCodOficina As String = Session("ALMACEN")
        '******VARIABLES PARA EL WEB SERVICE DE ANULACION
        Dim ClaseFactura As String
        Dim Sociedad As String
        Dim CanalDistribucion As String
        Dim Sector As String
        Dim DocuSAP As String
        Dim NroCompensacion As String
        Dim PuntoVenta As String
        Dim fechaContable As String
        Dim PAGOID As String
        Dim esNC As String
        '*Variables parde Retorno del WS
        Dim K_COD_RESPUESTA As String
        Dim K_MSJ_RESPUESTA As String
        Dim K_ID_TRANSACCION As String
        '**Para LOGS
        Dim strIdentifyLog As String

        'LMEDRANO para anular Rentas Adelantadas
        Dim ConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim nroGeneradoSap As String

        '**Variables para AUDITORIA
        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        Dim wParam1 As Long
        Dim wParam2 As String
        Dim wParam3 As String
        Dim wParam4 As Long
        Dim wParam5 As Integer
        Dim wParam6 As String
        Dim wParam7 As Long
        Dim wParam8 As Long
        Dim wParam9 As Long
        Dim wParam10 As String
        Dim wParam11 As Integer
        Dim Detalle(5, 3) As String

        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcPPag")
        wParam5 = 1
        wParam6 = "Anulación de documentos"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtPAnu")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        Detalle(4, 1) = "Oficina"
        Detalle(4, 2) = Session("ALMACEN")
        Detalle(4, 3) = "oficina"

        Detalle(5, 1) = "Usuario"
        Detalle(5, 2) = Session("USUARIO")
        Detalle(5, 3) = "Usuario"


        Dim objConf As New COM_SIC_Configura.clsConfigura
        Dim intAutoriza As Integer
        Dim strTipoTienda As String = Session("CANAL")
        Dim strNomVendedor As String = ""
        Dim Sociedad_Venta_Pago As String = ConfigurationSettings.AppSettings("Sociedad_venta_pago")


        Dim strCodUsuario As String = Session("USUARIO")  ' se debe leer de una variable de sesion
        Dim i As Integer
        Dim drDocSel As DataRow
        drDocSel = CType(Session("drFilaDoc"), DataRow)

        '**Para LOGS
        strIdentifyLog = Funciones.CheckStr(drDocSel.Item("PEDIN_NROPEDIDO"))

        Try
            'PROY-27440 INI
            If (HidIntAutPos.Value = "1") Then

                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " ***********************************************************")
                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " **************** INI btnGrabar_Click()   ******************")
                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "   ** INI VALIDA ESTADO DE PAGOS **")

                Dim strMensajeBloqueo = ClsKeyPOS.strAnulMesjBloqueante.Replace("{0}", "Anular")

                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " HidGuardarTrans: " & HidGuardarTrans.Value)

                If (HidGuardarTrans.Value = "0") Then
                            Response.Write("<script>alert('" & strMensajeBloqueo & " ');</script>")
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "   ** FIN VALIDA ESTADO DE PAGOS **")
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " **************** FIN btnGrabar_Click()   ******************")
                            Exit Sub
                        End If

                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "   ** FIN VALIDA ESTADO DE PAGOS **")
                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " **************** FIN btnGrabar_Click()   ******************")
            End If
            'PROY-27440 FIN
            '***Consulta PEDIDO 
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio Consultar Pedido - SP: SSAPSS_PEDIDO")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Nro PEDIDO: " & drDocSel.Item("PEDIN_NROPEDIDO"))
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Punto de Venta: " & ConsultaPuntoVenta(Session("ALMACEN")))
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Cod Interlocutor: " & "")

            dsPedido = ConsultaMssap.ConsultaPedido(drDocSel.Item("PEDIN_NROPEDIDO"), "", "")

            nroGeneradoSap = drDocSel.Item("PEDIN_NROPEDIDO")

            If dsPedido.Tables.Count > 0 Then
                If dsPedido.Tables(0).Rows.Count > 0 Then

                    PAGOID = IIf(IsDBNull(dsPedido.Tables(0).Rows(0).Item("PAGON_IDPAGO")), "", dsPedido.Tables(0).Rows(0).Item("PAGON_IDPAGO"))
                    'PAGOID = dsPedido.Tables(0).Rows(0).Item("PAGON_IDPAGO")
                    esNC = IIf(IsDBNull(dsPedido.Tables(0).Rows(0).Item("PEDIC_ISFORMAPAGO")), "", dsPedido.Tables(0).Rows(0).Item("PEDIC_ISFORMAPAGO"))
                    ClaseFactura = dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ID DE PAGO: " & PAGOID)
                End If
            Else

            End If
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "FIN Consultar Pedido - SP: SSAPSS_PEDIDO")
            '**FIN Consulta Pedido

            '***** INI: Valida Asignacion y Cierre de caja - TS-CCC *****
            Dim dsResultado As DataSet
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim strCierreCajaMensaje As String = String.Empty
            Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
            Dim fechaPedido As String
            Try
                If Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO")) <> "" Then
                    fechaPedido = CDate(dsPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO")).ToString("dd/MM/yyyy")
                End If
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio Validar Asignacion y caja cerrada")
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Oficina : " & Session("ALMACEN"))
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Fecha : " & fechaPedido)
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Cajero : " & codUsuario)
                Dim asignacionCajeroMensaje = objOffline.VerificarAsignacionCajero(Session("ALMACEN"), codUsuario, fechaPedido)
                If asignacionCajeroMensaje <> String.Empty Then
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE : " & asignacionCajeroMensaje)
                    Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", asignacionCajeroMensaje)
                    Me.RegisterStartupScript("RegistraAlerta", script)
                    Exit Sub
                End If

                dsResultado = objOffline.GetDatosAsignacionCajero(Session("ALMACEN"), fechaPedido, codUsuario)
                If Not dsResultado Is Nothing Then
                    For cont As Int32 = 0 To dsResultado.Tables(0).Rows.Count - 1
                        If dsResultado.Tables(0).Rows(cont).Item("CAJA_CERRADA") = "S" Then
                            strCierreCajaMensaje = "Caja Cerrada, no es posible anular."
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE : " & strCierreCajaMensaje)
                            Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strCierreCajaMensaje)
                            Me.RegisterStartupScript("RegistraAlerta", script)
                            Exit Sub
                        End If
                    Next
                End If
            Catch ex As Exception
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE ERROR : " & ex.Message.ToString())
            Finally
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Fin Validar Asignacion y caja cerrada")
                'PROY-30166 INI
                If (dsResultado Is Nothing) Then
                    objOffline = Nothing
                Else
                dsResultado.Dispose()
                objOffline = Nothing
                End If
                'PROY-30166 FIN
            End Try
            '***** FIN: Valida Asignacion y Cierre de caja - TS-CCC *****

            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INICIO Validacion si la NC esta como Forma de Pago")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "NC Usada como Medio de Pago?: " & esNC)

            If esNC = "1" Then
                Response.Write("<script>alert('No se pueden Anular Notas de Crédito que fueron usadas como medio de pago');</script>")
                Exit Sub
            Else
                'INI: NOTAS DE CANJE
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [INICIO][VALIDACION NOTAS CANJE]")
                If ClaseFactura = ConfigurationSettings.AppSettings("ClaseNotaCanje") Then
                    Dim nroPedidoNC As String = drDocSel.Item("PEDIN_NROPEDIDO")
                    Dim flagAnularCanje As String = ConfigurationSettings.AppSettings("strFlagAnular")
                    Dim estadoExitoCanje As String = ConfigurationSettings.AppSettings("idExitoCanje")
                    Dim estadoPagadoCanje As String = ConfigurationSettings.AppSettings("idPagadoCanje")
                    Dim k_cod_rpta, k_msj_rpta As String
                    Dim dtConsultaCanje As DataTable

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [INICIO][ConsultaCanje][IN NRO PEDIDO: " & nroPedidoNC & "]")
                    dtConsultaCanje = ConsultaMssap.ConsultaCanje(nroPedidoNC, k_cod_rpta, k_msj_rpta)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [FIN][ConsultaCanje][OUT CODLOG: " & k_cod_rpta & "][OUT DESLOG: " & k_msj_rpta & "]")

                    If Not IsNothing(dtConsultaCanje) AndAlso dtConsultaCanje.Rows.Count > 0 Then
                        If Funciones.CheckStr(dtConsultaCanje.Rows(0).Item("CANJV_ESTADO")) = estadoExitoCanje Or _
                            Funciones.CheckStr(dtConsultaCanje.Rows(0).Item("CANJV_ESTADO")) = estadoPagadoCanje Then

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "         [INICIO][ActualizarEstadoNotaCanje][IN NRO PEDIDO: " & nroPedidoNC & "][IN FLAG: " & flagAnularCanje & "]")
                            Dim actualizaEstado As String = TrsMssap.ActualizarEstadoNotaCanje(nroPedidoNC, flagAnularCanje, k_cod_rpta, k_msj_rpta)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "         [FIN][ActualizarEstadoNotaCanje][OUT CODLOG: " & k_cod_rpta & "][OUT DESLOG: " & k_msj_rpta & "]")

                        Else
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [No se encontró registro del Canje en SAP]")
                            Session("msgNotaCanje") = ConfigurationSettings.AppSettings("msgNoExisteCanjeSAP")
                            Response.Redirect("poolConsultaPagos.aspx", False)
                            Exit Sub
                        End If
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [No se encontró registro del Canje]")
                        Session("msgNotaCanje") = ConfigurationSettings.AppSettings("msgNoExisteCanjeBD")
                        Response.Redirect("poolConsultaPagos.aspx", False)
                        Exit Sub
                    End If
                    
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [FIN][VALIDACION NOTAS DE CANJE]")
                'FIN: NOTAS DE CANJE

                '***Inicio ANULACION DE PAGO
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INICIO Anulacion de pago - SP: SSAPSU_ANULARPAGO")
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN ID PAGO: " & Funciones.CheckInt(PAGOID))
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN ID PEDIDO: " & drDocSel.Item("PEDIN_NROPEDIDO"))


                AnuID = TrsMssap.AnularPago(PAGOID, drDocSel.Item("PEDIN_NROPEDIDO"))

                Dim IDAnulacion As String
                IDAnulacion = Funciones.CheckStr(IIf(IsDBNull(AnuID.Tables(0).Rows(0).Item("ANUPN_ID")), "", AnuID.Tables(0).Rows(0).Item("ANUPN_ID")))

                If ClaseFactura = ConfigurationSettings.AppSettings("ClaseNotaCanje") Then
                    If Len(Trim(IDAnulacion)) > 0 Then
                        'cambiar estado flujo 
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INICIO ACTUALIZAR ESTADO FLUJO SP - SSAPSU_ESTADOFLUJODEVOLUCION")
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN ID PEDIDO: " & drDocSel("PEDIN_NROPEDIDO"))
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN ID ANULACION: " & IDAnulacion)
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN PROCESO: " & ConfigurationSettings.AppSettings("ANULA_PAGO"))

                        TrsMssap.ActualizaEstadoFlujo(drDocSel("PEDIN_NROPEDIDO"), IDAnulacion, ConfigurationSettings.AppSettings("ANULA_PAGO"))

                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "FIN ACTUALIZAR ESTADO FLUJO SP - SSAPSU_ESTADOFLUJODEVOLUCION")
                    End If
                End If

                Dim resp As Boolean
                Dim codApli, userApli, numAsociado, dravDescTrs, dravCodPago As String

                If ClaseFactura = ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA") Then
                    'Dim resp As Boolean
                    'Dim codApli, userApli, numAsociado, dravDescTrs As String

                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "OBTENIENDO DATOS PARA LA ANULACIÓN")
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "NRO. GENRADO SAP" & nroGeneradoSap)

                    resp = ConsultaPvu.ObtenerDatosTransaccionales(nroGeneradoSap, numAsociado, codApli, userApli, dravDescTrs, dravCodPago)

                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "DATOS OBNTENIDOS")
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "NRO. GENRADO SAP" & nroGeneradoSap)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "NRO. ASOCIADO" & numAsociado)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "CÓDIGO APLICACIÓN" & codApli)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "USUARIO APLICACIÓN" & userApli)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "DESC. TRS." & dravDescTrs)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "CODIGO DE PAGO :" & dravCodPago)

                    If resp = False Then
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Resp :" & resp)
                        'Throw New Exception(" Error al Anular la Renta Adelantada. Volver a Intentar")
                    End If

                    Dim numAnulado As Int64 = 0
                    Dim codRespuesta As Int64 = 0
                    Dim msgRespuesta As String = String.Empty
                    Dim TrsPvu As New COM_SIC_Activaciones.clsTrsPvu

                    TrsPvu.AnularExtornarPagoDRA(codApli, userApli, numAsociado, dravDescTrs, numAnulado, codRespuesta, msgRespuesta)

                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "RESPUESTA ANULACIÓN")
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "NRO. ANULADO" & numAnulado)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "CÓDIGO RESPUESTA" & codRespuesta)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE RESPUESTA" & msgRespuesta)
                End If

                '******Inicio DE ANULACION en Flujo de Caja
                Dim p_codRpta As String = ""
                Dim p_MsgRpta As String = ""

                Dim strUsuario As String = Session("USUARIO")
                Dim strOficina As String = Session("ALMACEN")

                Dim tipo_oficina As String = IIf(IsDBNull(dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOOFICINA")), "", dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOOFICINA"))

                If ClaseFactura = ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA") And tipo_oficina = ConfigurationSettings.AppSettings("Tipo_Oficina_Cadena") Then
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INGRESO DE PARAMETROS PARA LA ANULACIÓN PAgos DRA_CADENA(SP:MIG_ACTESTADODRA)")
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "NRO. PEDIDO:  " & dravCodPago)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ESTADO ANULADO: " & ConfigurationSettings.AppSettings("Estado_Anulado_DRA_Cadena"))
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "USUARIO APLICACION: " & ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"))

                    Try
                        obj.AnularPago_CuadreCaja_cadena(dravCodPago, ConfigurationSettings.AppSettings("Estado_Anulado_DRA_Cadena"), ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), p_codRpta, p_MsgRpta)
                    Catch ex As Exception
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR EN EL SP:MIG_ACTESTADODRA")
                        'INI PROY-140126
                        Dim MaptPath As String
                        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                        MaptPath = "( Class : " & MaptPath & "; Function: btnGrabar_Click)"
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR: " & ex.Message.ToString() & MaptPath)
                        'FIN PROY-140126

                    End Try
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Salida DE PARAMETROS PARA LA ANULACIÓN PAgos - Flujo de Caja DRA_CADENA(SP:MIG_ACTESTADODRA)")
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "p_codRpta:  " & p_codRpta)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "p_MsgRpta:  " & p_MsgRpta)
                Else
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INGRESO DE PARAMETROS PARA LA ANULACIÓN en el Flujo de Caja (SP:MIG_ActEstadoVentasFactMedio)")
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "NRO. PEDIDO:  " & drDocSel.Item("PEDIN_NROPEDIDO"))
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ESTADO ANULADO: " & ConfigurationSettings.AppSettings("AnulacionPago_FlujoCaja"))
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "USUARIO APLICACION: " & ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"))

                    Try
                        obj.RegistrarAnulaDetallePago(drDocSel.Item("PEDIN_NROPEDIDO"), ConfigurationSettings.AppSettings("AnulacionPago_FlujoCaja"), ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), p_codRpta, p_MsgRpta)
                    Catch ex As Exception
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR EN EL SP:MIG_ActEstadoVentasFactMedio")
                        'INI PROY-140126
                        Dim MaptPath As String
                        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                        MaptPath = "( Class : " & MaptPath & "; Function: btnGrabar_Click)"
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR: " & ex.Message.ToString() & MaptPath)
                        'FIN PROY-140126

                    End Try

                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "SALIDA DE PARAMETROS PARA LA ANULACIÓN en el Flujo de Caja (SP:MIG_ActEstadoVentasFactMedio)")
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "p_codRpta:  " & p_codRpta)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "p_MsgRpta:  " & p_MsgRpta)
                End If

                Dim nImporte As Decimal

                Dim ds_Detalle As DataTable = CType(Session("FormasPAgo"), DataTable) 'ConsultaMssap.ConsultarFormasPago(txtNumPedido.Value)

                Dim j As Integer = 0
                Try
                    objLog.Log_WriteLog(pathFile, strArchivo, txtNumPedido.Value & " - " & "Consulta Formas de PAgo : SP: SSAPSS_FORMASDEPAGO")

                    If ds_Detalle.Rows.Count > 0 Then
                        If Not IsNothing(ds_Detalle) Then
                            For j = 0 To ds_Detalle.Rows.Count - 1
                                If (ds_Detalle.Rows.Item(j)("DEPAV_FORMAPAGO") = "ZEFE") Then
                                    nImporte = nImporte + CType(ds_Detalle.Rows.Item(j)("DEPAN_IMPORTE"), Decimal)
                                End If
                            Next
                        End If
                    End If
                Catch ex As Exception
                    objLog.Log_WriteLog(pathFile, strArchivo, txtNumPedido.Value & " - " & "error Consulta Formas de PAgo : SP: SSAPSS_FORMASDEPAGO")
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: btnGrabar_Click)"
                    objLog.Log_WriteLog(pathFile, strArchivo, txtNumPedido.Value & " - " & "ERROR: " & ex.Message.ToString() & MaptPath)
                    'FIN PROY-140126

                End Try

                objLog.Log_WriteLog(pathFile, strArchivo, txtNumPedido.Value & " - " & "FIN Consulta Formas de PAgo : SP: SSAPSS_FORMASDEPAGO")
                Session("FormasPAgo") = Nothing

                Dim fechaCajas As String = ""
                If Not dsPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO") Is Nothing Then
                    fechaCajas = CDate(dsPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO")).ToString("yyyyMMdd")
                Else
                    fechaCajas = Format(Now.Year, "0000").ToString.Trim & Format(Now.Month, "00").ToString.Trim & Format(Now.Day, "00").ToString.Trim
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- fechaCajas : " & fechaCajas.ToString)

                Try
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INSERTAR EFECTIVO EN EL SP:PKG_SISCAJ.SISCAJI_EFECTIVO")
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INPUT strOficina : " & strOficina)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INPUT strUsuario : " & strUsuario)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INPUT nImporte : " & nImporte * (-1))
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Cuotas : " & CStr(drDocSel.Item("DEPEN_NROCUOTA")))
                    Dim dFecha As Date
                    Dim sFecha As String = Format(Now.Day, "00").ToString.Trim & "/" & Format(Now.Month, "00").ToString.Trim & "/" & Format(Now.Year, "0000").ToString.Trim
                    If Not dsPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO") Is Nothing Then
                        dFecha = CDate(dsPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO"))
                        sFecha = dFecha.ToString("dd/MM/yyyy")
                    End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Fecha Pedido : " & sFecha)
                    If CInt(drDocSel.Item("DEPEN_NROCUOTA")) = 0 Then  ' No se contabliza el pago en cuotas
                        If ClaseFactura = ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA") And tipo_oficina = ConfigurationSettings.AppSettings("Tipo_Oficina_Cadena") Then
                            obj.FP_InsertaEfectivo(strOficina, strUsuario, nImporte * (-1))
                        ElseIf ClaseFactura = ConfigurationSettings.AppSettings("strTipDoc") Then
                            obj.FP_InsertaEfectivo(strOficina, strUsuario, nImporte) 'NC = 0007 - Valor en Positivo para Anulacion de NC
                        Else
                            obj.FP_InsertaEfectivo(strOficina, strUsuario, nImporte * (-1), sFecha)
                        End If
                        'obj.FP_InsertaEfectivo(strOficina, strUsuario, nImporte * (-1))
                    End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin FP_InsertaEfectivo.")
                Catch ex As Exception
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR EN EL SP:PKG_SISCAJ.SISCAJI_EFECTIVO")
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: btnGrabar_Click)"
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR: " & ex.Message.ToString() & MaptPath)
                    'FIN PROY-140126

                End Try


                '******Fin DE ANULACION en Flujo de Caja
                If ClaseFactura <> ConfigurationSettings.AppSettings("ClaseNotaCanje") Then
                    If ClaseFactura <> ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA") Then
                        '******WEB SERVICE DE ANULACION
                        If ConfigurationSettings.AppSettings("LanzarServicio") = "0" Then
                            Sociedad = IIf(IsDBNull(AnuID.Tables(0).Rows(0).Item("PAGOV_SOCIEDAD")), "", AnuID.Tables(0).Rows(0).Item("PAGOV_SOCIEDAD"))
                            NroCompensacion = IIf(IsDBNull(AnuID.Tables(0).Rows(0).Item("PAGOV_PAGOSAP")), "", AnuID.Tables(0).Rows(0).Item("PAGOV_PAGOSAP"))
                            PuntoVenta = IIf(IsDBNull(AnuID.Tables(0).Rows(0).Item("PAGOV_OFICINAVENTA")), "", AnuID.Tables(0).Rows(0).Item("PAGOV_OFICINAVENTA"))
                            fechaContable = IIf(IsDBNull(AnuID.Tables(0).Rows(0).Item("PAGOD_FECHACONTA")), "", AnuID.Tables(0).Rows(0).Item("PAGOD_FECHACONTA"))

                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INICIO WEB SERVICE DE ANULACION DE PAGO")
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Id_Pago : " & PAGOID)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN NRO PEDIDO: " & drDocSel("PEDIN_NROPEDIDO"))
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Clase Factura: " & ClaseFactura)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Sociedad: " & Sociedad)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Canal de Distribucion: " & CanalDistribucion)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Sector: " & Sector)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Documento SAP: " & DocuSAP)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Fecha Contable: " & fechaContable)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Sociedad Venta Pago: " & Sociedad_Venta_Pago)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Documento Compansacion  " & NroCompensacion)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Punto de Venta : " & PuntoVenta)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Flag: " & ConfigurationSettings.AppSettings("FLAG_ANULAR_PAGO"))
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Usuario: " & "")
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Terminal: " & "")
                            Try
                                objClsPagosWS.AnularPedidoPago(drDocSel("PEDIN_NROPEDIDO"), _
                                                                                            PAGOID, "", Sociedad, _
                                                                                            "", "", "", fechaContable, Sociedad_Venta_Pago, NroCompensacion, PuntoVenta, ConfigurationSettings.AppSettings("FLAG_ANULAR_PAGO"), CurrentUser, CurrentTerminal, _
                                                                                        K_COD_RESPUESTA, K_MSJ_RESPUESTA, K_ID_TRANSACCION)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Exito inicio invocacion el Webservice Anulacion de Pagos WS")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     ID K_COD_RESPUESTA: " & K_COD_RESPUESTA)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     ID MSJ DE RESPUESTA: " & K_MSJ_RESPUESTA)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    K_ID_TRANSACCION: " & K_ID_TRANSACCION)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Exito FIN invocacion el Webservice Anulacion de Pagos WS")

                            Catch ex As Exception

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Fallo el Webservice Anulacion de Pagos WS")
                                'INI PROY-140126
                                Dim MaptPath As String
                                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                                MaptPath = "( Class : " & MaptPath & "; Function: btnGrabar_Click)"
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Exception: " & ex.ToString() & MaptPath)
                                'FIN PROY-140126

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     ID K_COD_RESPUESTA: " & K_COD_RESPUESTA)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     ID MSJ DE RESPUESTA: " & K_MSJ_RESPUESTA)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    K_ID_TRANSACCION: " & K_ID_TRANSACCION)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Anulacion de Pagos WS")

                            End Try
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "FIN WEB SERVICE DE ANULACION DE Pago")
                        End If
                        '******FIN DE WS DE ANULACION
                    End If
                End If
                'Else
                'Mi código

                'gdelasca Inicio Proy-9067

                'PROY-140360 IDEA NUEVO IDEA-46301
                Try
                    Dim clsConsultaPvuCuota As New COM_SIC_Activaciones.clsConsultaPvu
                    Dim objContrato As DataSet
                    Dim dsAcuerdo As DataSet
                    Dim P_CODIGO_RESPUESTA As String = ""
                    Dim P_MENSAJE_RESPUESTA As String = ""
                    Dim strEstadoFlag As String = "9"
                    Dim sMsjRespuesta As String
                    Dim strCodigoPedido As String = dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")
                    Dim objClsCajas As New COM_SIC_Cajas.clsCajas
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Inicio Actualizar Flag Envio")
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Codigo Pedido " & strCodigoPedido)
                    If (dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA") = ConfigurationSettings.AppSettings("strTVPostpago") And dsPedido.Tables(0).Rows(0).Item("DEPEN_NROCUOTA") = 1 And (dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION") = ConfigurationSettings.AppSettings("strDTVRenovacion") Or dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION") = ConfigurationSettings.AppSettings("strDTVAlta"))) Then
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Llamada al metodo ObtenerDrsap")
                        objContrato = clsConsultaPvuCuota.ObtenerDrsap(strCodigoPedido, P_CODIGO_RESPUESTA, P_MENSAJE_RESPUESTA)
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Resultado Consulta Contrato - metodo ObtenerDrsap")
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Codigo Respuesta :" & P_CODIGO_RESPUESTA)
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Mensaje Respuesta :" & P_MENSAJE_RESPUESTA)
                        'objContrato = clsConsultaPvuCuota.ObtenerDrsap(strCodigoPedido, P_COD_RESP, P_MSG_RESP)
                        Dim strContrato As String = objContrato.Tables(0).Rows(0).Item("ID_CONTRATO")
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Nro. Contrato" & strContrato)
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - FIN CONSULTA CONTRATO")
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Llamada el metodo ConsultaAcuerdoPCS")
                        dsAcuerdo = clsConsultaPvuCuota.ConsultaAcuerdoPCS(strContrato, DBNull.Value)
                        Dim strNroSEC As String = Funciones.CheckStr(dsAcuerdo.Tables(0).Rows(0).Item("contn_numero_sec")) 'Session("strNumSEC") ' objClsCajas.ObtenerSecByPedido(strNumPedido)
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Numero SEC :" & strNroSEC)
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - FIN CONSULTA ACUERDO")
                        If (ValidarModalidadVentaContratoCode(strNroSEC)) Then
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Inicio llamada al metodo FnActualizaFlagCuota")
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Numero de Pedido" & dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA"))
                            clsConsultaPvuCuota.FnActualizaFlagCuota(strCodigoPedido, strEstadoFlag, sMsjRespuesta)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Estado Flag : " & strEstadoFlag)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - MensajeRespuesta : " & sMsjRespuesta)

                            If dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION") = ConfigurationSettings.AppSettings("strDTVAlta") Then
                                Dim strIdentificador As String = ""
                                Dim strCodRespAnulacion As String
                                Dim strMsgRespAnulacion As String
                                Dim strEstadoTipififacion As String = "03" 'dsPedido.Tables(1).Rows(0).Item("DEPEV_ESTADO") '"03"
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - INICIO ANULACION TIPIFICACION DE ALTA/PORTA")
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Parametros de Entrada")
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Identificador" & strIdentificador)
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - NroPedido" & strCodigoPedido)
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Estado Pedido" & strEstadoTipififacion)
                                clsConsultaPvuCuota.FnAnularTipificacion(strIdentificador, strCodigoPedido, strEstadoTipififacion, strCodRespAnulacion, strMsgRespAnulacion)
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Resultado Anulacion Tipificacion ")
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Codigo Resultado : " & strCodRespAnulacion)
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - Mensage Resultado : " & strMsgRespAnulacion)
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PROY-140360-IDEA-46301 - FIN ANULACION TIPIFICACION DE ALTA/PORTA")

                            End If
                        End If
                    End If
                Catch ex As Exception
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & ex.ToString())
                End Try

                'FIN PROY-140360 IDEA-46301

                Try
                                        
                    Dim ObsAnuRenoAnti As String
                    Dim v_Obtenido As DataSet
                    Dim vCodMaterialSaliente As String = ""
                    Dim vCodSerieSaliente As String = ""
                    Dim objPvu As New COM_SIC_Activaciones.clsTrsPvu
                    Dim vCodRsptActualiza As Integer
                    Dim vMsjRsptaActualiza As String = ""

                    Dim vCursorxIdPedidoActual As Object
                    Dim vCodRpta As String
                    Dim vMsjRpta As String
                    Dim DsCanjeEquipo As DataSet

                    DsCanjeEquipo = objPvu.ConsultaEstRenoAnticipada(nroGeneradoSap, vCursorxIdPedidoActual, vCodRpta, vMsjRpta)

                    If Not IsNothing(DsCanjeEquipo) AndAlso DsCanjeEquipo.Tables(0).Rows.Count > 0 Then


                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--INICIO MÉTODO ActualizarRenoAnticipada()--")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp Nº PEDIDO:" & nroGeneradoSap)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp ESTADO :" & ConfigurationSettings.AppSettings("EstRenoAntiAnulPago").ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp CodMAterial:" & vCodMaterialSaliente)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp CodSerie:" & vCodSerieSaliente)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp CodUsuario:" & CurrentUser)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp Observacion:" & ConfigurationSettings.AppSettings("ObsAnulaPagoRenoAnti").ToString())

                        v_Obtenido = objPvu.ActualizarRenoAnticipada(nroGeneradoSap, ConfigurationSettings.AppSettings("EstRenoAntiAnulPago").ToString(), vCodSerieSaliente, vCodMaterialSaliente, CurrentUser, ConfigurationSettings.AppSettings("ObsAnulaPagoRenoAnti").ToString(), vCodRsptActualiza, vMsjRsptaActualiza)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--FIN Metodo ActualizarRenoAnticipada()--")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out COD RESPUESTA:" & vCodRsptActualiza)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out MSJ RESPUESTA:" & vMsjRsptaActualiza)

                    Else

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR Metodo  ConsultarVentaRenovPostCAC No retorna Registros")
                    End If

                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "error Metod. ActualizarRenoAnticipada:" & ex.Message.ToString())
                End Try

                'gdelasca FIN Proy-9067

                'INC000000961273 - INICIO
                Try
                    Dim descOpePortaPost As String = ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_POST")
                    Dim descOpePortaPre As String = ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_PRE")
                    If dsPedido.Tables(0).Rows(0).Item("PEDIV_DESCTIPOOPERACION") = descOpePortaPost Or dsPedido.Tables(0).Rows(0).Item("PEDIV_DESCTIPOOPERACION") = descOpePortaPre Then
                        Dim flagActivaSimcard As String = ConfigurationSettings.AppSettings("constActivaSimcardsWS")
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - flagActivaSimcard: " & flagActivaSimcard)
                        If flagActivaSimcard.Equals("0") Then
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INICIO AnulaTelefonoPortabilidad")
                            Anula_Portabilidad(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO"))
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "FIN AnulaTelefonoPortabilidad")
                        End If
                    End If
                Catch ex As Exception
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR ANULA PORTABILIDAD: " & ex.Message)
                End Try
                'INC000000961273 - FIN

                '**AUDITORIA
                objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)

                Response.Write("<script language=jscript> alert('Proceso de Anulación de Pago realizado con éxito!!!'); </script>")
                
                ActualizarWL() 
                Regresar()
            End If
            'Else
            '    Response.Write("<script language=jscript> alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador'); </script>")
            'End If
        Catch ex As Exception
            wParam5 = 0
            wParam6 = "Error en Anulación de documentos. " & ex.Message
            objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try

    End Sub

    ' INICIO PROY-140360-IDEA-46301 
    Public Function ValidarModalidadVentaContratoCode(ByVal strNumSEC As String) As Boolean

        Dim strIdentifyLog As String = CStr(Session("USUARIO")) & " - " & strNumSEC
        Dim objActivaciones As New COM_SIC_Activaciones.ClsCambioPlanPostpago
        Dim lista As New ArrayList

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-140360-IDEA-46301 Validar Modalidad Venta Contrato Code- INICIO")
            ValidarModalidadVentaContratoCode = False

            lista = objActivaciones.ConsultaSolicitud_NROSEC(Funciones.CheckStr(strNumSEC))

            If Not lista Is Nothing And lista.Count > 0 Then
                For Each item As COM_SIC_Activaciones.clsSolicitudPersona In lista
                    If Funciones.CheckStr(item.MODALIDAD_VENTA).Equals("2") Then
                        ValidarModalidadVentaContratoCode = True
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  PROY-140360-IDEA-46301 Modalidad de Venta : " & Funciones.CheckStr(item.MODALIDAD_VENTA))
                        Exit For
                    End If
                Next
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-140360-IDEA-46301 No hay datos de la sec")
            End If
        Catch ex As Exception
            ValidarModalidadVentaContratoCode = False
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-140360-IDEA-46301 Error al validar Modalidad ContratoCode : " & ex.Message.ToString)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " IPROY-140360-IDEA-46301 Validar Modalidad Venta ContratoCode - FIN")
        End Try

        Return ValidarModalidadVentaContratoCode
    End Function 

    Private Sub ActualizarWL()
        Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim Rpta As New Integer
        Dim NRO_Telefono As String
        Dim Estado As String
        Dim IMEI As String
        Dim dtResultado As DataTable
        Dim drResultado As DataRow
        Dim DocSunat As String

        DocSunat = Request.QueryString("docSunat")
        NRO_Telefono = Request.QueryString("numeroTelefono")

        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio del mètodo ActualizarWL")
        dtResultado = objClsConsultaPvu.ConsultaVentaRenoWL(NRO_Telefono, "2")
        If dtResultado.Rows.Count > 0 Then
            drResultado = dtResultado.Rows(0)
            Estado = drResultado.Item("WLRPV_ESTADO")
            IMEI = drResultado.Item("WLRPV_IMEI_EQUIPO")
        End If

        If Estado = "2" And IMEI <> "" Then
            Rpta = objClsConsultaPvu.ActualizaVentaRenoWL(NRO_Telefono, "1")
            If Rpta = "0" Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin del mètodo ActualizarWL - Actualizado OK")
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin del mètodo ActualizarWL - Error al Actualizar")
            End If
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin del mètodo ActualizarWL - No Actualizado")
        End If
        'Consultar datos de la Venta en WLTable por ID de Venta.
        'Actualizar el Estado de ese ID de Venta a 1
    End Sub
  

    Private Sub Regresar()
        Session("FechaPago") = txtFecha.Value.Substring(6, 2) + "/" + txtFecha.Value.Substring(4, 2) + "/" + txtFecha.Value.Substring(0, 4)
        Response.Redirect("poolConsultaPagos.aspx")
    End Sub

    Private Sub ActualizaPago(ByVal strNroPedido As String)

        Dim intResultado As Integer
        Dim resultado As Integer
        Dim objCajas As New COM_SIC_Cajas.clsCajas

        If Not strNroPedido.Trim = "" Then
            resultado = objCajas.ActualizaPago(strNroPedido, 2, intResultado)
        End If

    End Sub

    Public Sub AnularRenovacionRPM6(ByVal nroTelefono As String)

        Dim objBSCS As New COM_SIC_Cajas.clsCajas
        Dim Accion, CodOcc, NroCuotas As Integer
        Dim Monto As Double
        Dim CodRenov, Remark, DescTickler, MensajeFinal, TipoTickler As String
        MensajeFinal = String.Empty

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_RenovacionRPC6" 'ConfigurationSettings.AppSettings("constNameLogRegistroDOL")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRegistroDOL")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = nroTelefono
        Dim strOficina As String = Funciones.CheckStr(Session("OFICINA"))

        Try
            Dim ArrayParametros() As String = Split(ConfigurationSettings.AppSettings("strAnular"), ";")
            CodRenov = ArrayParametros(0).ToString()
            Accion = Integer.Parse(ArrayParametros(1))
            CodOcc = Integer.Parse(ArrayParametros(2))
            NroCuotas = Integer.Parse(ArrayParametros(3))
            Monto = Double.Parse(ArrayParametros(4))
            Remark = String.Empty
            DescTickler = ConfigurationSettings.AppSettings("strDesTicklerRPC6") 'String.Empty
            TipoTickler = ArrayParametros(7).ToString()

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio AnularRenovacionRPM6 detaConsultaPagos.aspx-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  nroTelefono : " & FormatoTelefono(nroTelefono))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodRenov : " & CodRenov)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  DescTickler : " & DescTickler)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strCodUsrRenovRPC : " & ConfigurationSettings.AppSettings("strCodUsrRenovRPC"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Cod. Oficina : " & Session("ALMACEN"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Oficina : " & strOficina)

            Dim Respuesta As String = objBSCS.FP_RegistrarRenovacionRPM6(FormatoTelefono(nroTelefono), CodRenov, Accion, DescTickler, strOficina, ConfigurationSettings.AppSettings("strCodUsrRenovRPC"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Respuesta : " & Respuesta)

            Select Case Split(Respuesta, ";")(0).ToString()
                Case "0"
                    MensajeFinal = "La anulación se ha realizado con exito"
                Case Else
                    MensajeFinal = "Ha ocurrido un error en la anulación. Por favor comuníquese con el Administrador."
            End Select

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  MensajeFinal : " & MensajeFinal)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------Fin AnularRenovacionRPM6--------------")

            objBSCS.FP_RegistrarLogRenovacionRPM6(FormatoTelefono(nroTelefono), CodRenov, Accion, CodOcc, NroCuotas, Monto, Remark, TipoTickler, DescTickler, ConfigurationSettings.AppSettings("strCodUsrRenovRPC"), Session("strUsuario"), Split(Respuesta, ";")(1).ToString(), Split(Respuesta, ";")(2).ToString(), strOficina)
            Session.Add("ErrorAnulacionRPM6", MensajeFinal)
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: AnularRenovacionRPM6)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ERROR : " & ex.Message & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------Fin AnularRenovacionRPM6--------------")

            MensajeFinal = "Ha ocurrido un error en la anulación. Por favor comuníquese con el Administrador."
            Session.Add("ErrorAnulacionRPM6", MensajeFinal)
        End Try
    End Sub
    Function FormatoTelefono(ByVal telefono)
        Dim aux
        aux = telefono
        If aux <> "" Then
            Dim longitud
            Dim posicion
            longitud = Len(telefono)
            If longitud > 0 Then
                'posicion = 1
                Do While InStr(1, aux, "0") = 1
                    aux = Mid(aux, 2, Len(aux))
                Loop
            End If
            If InStr(1, aux, "1") = 1 Then    'Si es lima adicionar 0 adelante
                aux = "0" & aux
            End If
        End If
        If aux = "" Then
            FormatoTelefono = telefono
        Else
            FormatoTelefono = aux
        End If
    End Function

    Private Function ListaMaterialesBBA(ByVal strDocSap As String) As String

        Dim strListMateriales As String = ""
        Dim strGrupo As String = ConfigurationSettings.AppSettings("constGrupoPrepagoEspecial")

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogActivacionPrepago")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogActivacionPrepago")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = strDocSap & "|" & strGrupo

        Try

            Dim dsMaterial As DataSet
            Dim objSicarDB As New COM_SIC_Activaciones.clsBDSiscajas
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio ListaMaterialesBBA-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strGrupo : " & strGrupo)

            dsMaterial = objSicarDB.FP_ConsultaMaterialBBA(strGrupo)

            If Not IsNothing(dsMaterial) AndAlso dsMaterial.Tables(0).Rows.Count > 0 Then
                For idx As Integer = 0 To dsMaterial.Tables(0).Rows.Count - 1
                    strListMateriales += "|" & dsMaterial.Tables(0).Rows(idx).Item("SPARV_KEY") & ";" & _
                                                dsMaterial.Tables(0).Rows(idx).Item("SPARV_VALUE") & ";" & _
                                                dsMaterial.Tables(0).Rows(idx).Item("SPARV_VALUE2") & ";" & _
                                                dsMaterial.Tables(0).Rows(idx).Item("SPARV_VALUE3")
                Next
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  strListMateriales : " & strListMateriales)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin ListaMaterialesBBA-----------")

        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: ListaMaterialesBBA)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  ERROR : " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin ListaMaterialesBBA-----------")
        Finally
            objFileLog = Nothing
        End Try

        Return strListMateriales
    End Function
    Private Function Get_CodOpcionBBA(ByVal strCodMaterial As String, ByVal strListMaterial As String, ByVal strListaPrecio As String, ByVal strCampana As String, ByVal strDocSap As String) As String

        Dim arrListMateriales As String()
        Dim arrMaterial As String()
        Dim strOpcion As String = ""

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogActivacionPrepago")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogActivacionPrepago")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = strDocSap & "|" & strCodMaterial

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio Get_CodOpcionBBA-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strCodMaterial : " & strCodMaterial)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strListMaterial : " & strListMaterial)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strListaPrecio : " & strListaPrecio)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strCampana : " & strCampana)

            arrListMateriales = strListMaterial.Split("|")

            If arrListMateriales.Length > 0 Then
                For idx As Integer = 0 To arrListMateriales.Length - 1
                    arrMaterial = arrListMateriales(idx).Split(";")
                    If arrMaterial.Length > 3 Then
                        If strCodMaterial.ToUpper() = arrMaterial(0).ToUpper() _
                        And strListaPrecio.ToUpper() = arrMaterial(2).ToUpper() _
                        And strCampana.ToUpper() = arrMaterial(3).ToUpper() Then

                            strOpcion = arrMaterial(1).ToString()
                            Exit For
                        End If
                    End If
                Next
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  strOpcion : " & strOpcion)
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: Get_CodOpcionBBA)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  ERROR : " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin Get_CodOpcionBBA-----------")
            objFileLog = Nothing
        End Try

        Return strOpcion

    End Function

#Region "Anulacion Renovacion Corporativa"

    Public Sub AnularPagoRenovCorp(ByVal p_nro_pedido As String, ByVal p_tipo_renov As String)

        Dim oDatos As New COM_SIC_Cajas.clsCajas
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_RenovacionCorporativa"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRenovacion")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "---------------Inicio Anulacion Pago Renovacion-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   INP  Nro Pedido : " & p_nro_pedido)
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   INP  Tipo Venta : " & p_tipo_renov)

            Dim blnExito As Boolean = oDatos.FP_AnularPagoRenovCorp(p_nro_pedido, p_tipo_renov)

            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   OUT  Respuesta : " & blnExito)

        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: AnularPagoRenovCorp)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   ERROR : " & ex.Message & MaptPath)
            'FIN PROY-140126

        Finally
            oDatos = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "----------------Fin Anulacion Pago Renovacion--------------")
        End Try
    End Sub

    Public Sub AnularRenovCorpBSCS(ByVal p_nro_pedido As String, ByVal p_tipo_renov As String)

        Dim oConsulta As New COM_SIC_Cajas.clsCajas
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_RenovacionCorporativa"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRenovacion")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

        Dim strOut As String
        Dim MsgError As String
        Dim LineasError As String = ""
        Dim MensajeFinal As String = ""

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "----------------INICIO ANULACION PAGO RENOVACION--------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   INP  Nro Pedido : " & p_nro_pedido)
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   INP  Tipo Venta : " & p_tipo_renov)

            'Consultar las líneas para la Renovación
            Dim dsLineas As DataSet = oConsulta.FP_ConsultarDatosRenov(p_nro_pedido, p_tipo_renov)

            If Not IsNothing(dsLineas) AndAlso dsLineas.Tables(0).Rows.Count > 0 Then

                For x As Integer = 0 To dsLineas.Tables(0).Rows.Count - 1

                    Dim telefono As String = dsLineas.Tables(0).Rows(x)(0)

                    objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "-----------------------------------------------------------")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   INP  Nro Telefono     : " & telefono)

                    'Anular datos de la Renovacion en BSCS
                    'strOut = oConsulta.FP_AnularRenovCorpBSCS(telefono, MsgError)
                    strOut = oConsulta.FP_AnularRenovCorpBSCSNuevo(telefono, MsgError)

                    If Not strOut.Equals("0") Then
                        LineasError = LineasError & ";" & telefono
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   OUT  Codigo : " & strOut)
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: AnularRenovCorpBSCS)"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   OUT  Mensaje : " & CheckStr(MsgError) & MaptPath)
                    'FIN PROY-140126

                Next

                If Not LineasError = "" Then
                    LineasError = Right(LineasError, Len(LineasError) - 1)
                    MensajeFinal = "Ocurrió un Error en la anulación en BSCS de la(s) siguiente(s) línea(s): " & LineasError

                    Session.Add("AnulacionBSCSRenovacionCorporativa", MensajeFinal)
                End If
            Else
                'INI PROY-140126
                Dim MaptPath As String
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: AnularRenovCorpBSCS)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   ERROR : " & "No se encontró Datos de Líneas." & MaptPath)
                'FIN PROY-140126              
            End If

        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: AnularRenovCorp)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   ERROR : " & ex.Message & MaptPath)
            'FIN PROY-140126            
        Finally
            oConsulta = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "----------------FIN ANULACION PAGO RENOVACION--------------")
        End Try
    End Sub

#End Region

    Private Function Valida_Anula_Prom(ByVal strNroSap As String) As Integer
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim intResult As Integer

        Try
            intResult = objCajas.validaAnulaVtaProm(strNroSap)
        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "')</script>")
        End Try
        Return intResult
    End Function

    Private Function Anula_Vta_Prom(ByVal strNroSap As String, ByVal strEstado As String) As Integer
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim intResult As Integer

        Try
            intResult = objCajas.AnulaVtaProm(strNroSap, strEstado)
        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "')</script>")
        End Try
        Return intResult
    End Function

    Public Sub AnularBonoPrepago(ByVal nroTelefono As String, ByVal nroIMEI As String, ByVal nroSap As String)

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_RenovacionPrepago"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRenovacionPrepago")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim codSalida, msgSalida, strMensaje As String
        Dim strMensajeError As String

        Try
            nroTelefono = "51" & nroTelefono
            Dim input As String = String.Format("{0}|{1}", nroTelefono, nroIMEI)

            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "Inicio Anulación Renovación Prepago")
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "      nroLinea|imei: " & input)

            'Anulación Bono Prepago
            Dim oRenovaPrepago As New COM_SIC_Activaciones.clsBonoRenovaPrepago
            codSalida = oRenovaPrepago.AnularBonoPrepago(nroSap, nroTelefono, nroIMEI, msgSalida)

            'Consulta de Mensaje Renovación Prepago
            Dim codGrupo As Integer = CInt(ConfigurationSettings.AppSettings("ConstGrupoMensajeAnulacion"))
            Dim dsMensajes As DataSet = (New COM_SIC_Cajas.clsCajas).ObtenerParamByGrupo(codGrupo)
            If Not IsNothing(dsMensajes) Then
                For i As Integer = 0 To dsMensajes.Tables(0).Rows.Count - 1
                    Dim codigo As String = CheckStr(dsMensajes.Tables(0).Rows(i).Item("PARAV_VALOR")).Split("|")(0)
                    Dim mensaje As String = CheckStr(dsMensajes.Tables(0).Rows(i).Item("PARAV_VALOR")).Split("|")(1)

                    If codigo = codSalida Then
                        strMensaje = String.Format(mensaje, nroTelefono)
                        Exit For
                    End If

                    If codigo = "9" Then
                        strMensajeError = String.Format(mensaje, nroTelefono)
                    End If
                Next
            End If

        Catch ex As Exception
            strMensaje = strMensajeError
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: AnularBonoPrepago)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "      Error: " & ex.Message & MaptPath)
            'FIN PROY-140126            
        Finally
            Dim out As String = String.Format("{0}|{1}|{2}", codSalida, msgSalida, strMensaje)
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "      codSalida|msgSalida|Mensaje: " & out)
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "Fin Anulación Renovación Prepago")

            Session.Add("mensajeErrorBonoPrepago", strMensaje)
        End Try

    End Sub

    Private Sub ActualizarPlanRoaming(ByVal NroPedido As String, ByVal CodEstado As String)
        Dim dsLineas As New DataSet
        Dim oConsulta As New COM_SIC_Cajas.clsCajas

        Dim oLog As New SICAR_Log
        Dim nameFile As String = "LogProcesoRoaming"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRegistroDOL")
        Dim strArchivo As String = oLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = NroPedido

        Try

            oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Anulacion Roaming")
            oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------------------------")
            oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Input  --> " & "NroPedido: " & NroPedido)

            'Consulta de Nro de Linea
            dsLineas = oConsulta.ObtenerLineasRoaming(NroPedido)

            If Not IsNothing(dsLineas) Then

                For i As Integer = 0 To dsLineas.Tables(0).Rows.Count - 1

                    Dim linea As String = dsLineas.Tables(0).Rows(i).Item("ROAMV_NRO_LINEA")
                    Dim plan As String = dsLineas.Tables(0).Rows(i).Item("ROAMV_COD_PLAN")

                    oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Input  --> " & "Linea: " & linea)
                    oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Input  --> " & "Plan: " & plan)
                    oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Input  --> " & "Estado: " & CodEstado)

                    Dim mensaje As String = oConsulta.ActualizarLineasRoaming(linea, plan, CodEstado)

                    oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Output --> " & "Mensaje: " & mensaje)
                Next
            End If
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: ActualizarPlanRoaming)"
            oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Output --> " & "Error: " & ex.Message & MaptPath)
            'FIN PROY-140126

        Finally
            oConsulta = Nothing
            dsLineas = Nothing
            oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Anulacion Roaming")
        End Try

    End Sub
    Private Sub RegistrarAuditoria(ByVal CodTrans As String, ByVal descTrans As String)
        Try
            Dim nombreHost As String = System.Net.Dns.GetHostName
            Dim nombreServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nombreHost)
            Dim usuario_id As String = CurrentUser
            Dim ipcliente As String = CurrentTerminal
            Dim strMensaje As String

            Dim strCodServ As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            Dim objAuditoriaWS As New COM_SIC_Activaciones.clsAuditoriaWS
            Dim auditoriaGrabado As Boolean
            auditoriaGrabado = objAuditoriaWS.RegistrarAuditoria(CodTrans, strCodServ, ipcliente, nombreHost, ipServer, nombreServer, usuario_id, "", "0", descTrans)

        Catch ex As Exception
            ' Throw New Exception("Error. Registrar Auditoria.")
        End Try
    End Sub
    '****************************************************'
    '****************************************************'
    '++++METODO PARA HACER CONSULTA DEL PUNTO DE VENTA+++'
    '****************************************************'
    '****************************************************'
    Private Function ConsultaPuntoVenta(ByVal P_OVENC_CODIGO As String) As String
        Dim strIdentifyLog As String = ""
        Try
            Dim obj As New COM_SIC_Activaciones.clsConsultaMsSap
            Dim dsReturn As DataSet
            dsReturn = obj.ConsultaPuntoVenta(P_OVENC_CODIGO)
            If dsReturn.Tables(0).Rows.Count > 0 Then
                Return dsReturn.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA")
            End If
            Return Nothing
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Error al tratar de consultar el Punto de Venta")
            Exit Function
        End Try
    End Function
'PROY-27440 INI
    Private Sub load_values_pos()
        strIdentifyLogPOS = "detaConsultaPagos: [" + txtNumPedido.Value + "] "
        Dim strArrys As String()
        Dim strEstadoAnu As String
        Dim strDescAnul As String

        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " ***********************************************************")
        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " **************** INI load_values_pos()   ******************")

        If Me.HidFila0.Value.Trim() <> "" Then
            strArrys = Me.HidFila0.Value.Split("|")  'Estado|Desc|Contador|IsIncompleto

            strEstadoAnu = "" : strEstadoAnu = strArrys(0).Substring(strArrys(0).IndexOf("=") + 1) ' Estado Anulación
            dgrDetalle.Items(0).Cells(9).Text = strEstadoAnu

            strDescAnul = "" : strDescAnul = strArrys(1).Substring(strArrys(1).IndexOf("=") + 1) 'Desc Anul
            dgrDetalle.Items(0).Cells(10).Text = strDescAnul
            HidContIntento0.Value = Funciones.CheckInt(strArrys(2).Substring(strArrys(2).IndexOf("=") + 1)) 'Contador
        Else
            HidContIntento0.Value = Funciones.CheckInt("1")

        End If

        If Me.HidFila1.Value.Trim() <> "" Then
            strArrys = Me.HidFila1.Value.Split("|")

            strEstadoAnu = "" : strEstadoAnu = strArrys(0).Substring(strArrys(0).IndexOf("=") + 1)
            dgrDetalle.Items(1).Cells(9).Text = strEstadoAnu

            strDescAnul = "" : strDescAnul = strArrys(1).Substring(strArrys(1).IndexOf("=") + 1)
            dgrDetalle.Items(1).Cells(10).Text = strDescAnul

            HidContIntento1.Value = Funciones.CheckInt(strArrys(2).Substring(strArrys(2).IndexOf("=") + 1))
        Else
            HidContIntento1.Value = Funciones.CheckInt("1")
        End If

        If Me.HidFila2.Value.Trim() <> "" Then
            strArrys = Me.HidFila2.Value.Split("|")

            strEstadoAnu = "" : strEstadoAnu = strArrys(0).Substring(strArrys(0).IndexOf("=") + 1)
            dgrDetalle.Items(2).Cells(9).Text = strEstadoAnu

            strDescAnul = "" : strDescAnul = strArrys(1).Substring(strArrys(1).IndexOf("=") + 1)
            dgrDetalle.Items(2).Cells(10).Text = strDescAnul

            HidContIntento2.Value = Funciones.CheckInt(strArrys(2).Substring(strArrys(2).IndexOf("=") + 1))

        Else
            HidContIntento2.Value = Funciones.CheckInt("1")
        End If

        If Me.HidFila3.Value.Trim() <> "" Then
            strArrys = Me.HidFila3.Value.Split("|")

            strEstadoAnu = "" : strEstadoAnu = strArrys(0).Substring(strArrys(0).IndexOf("=") + 1)
            dgrDetalle.Items(3).Cells(9).Text = strEstadoAnu

            strDescAnul = "" : strDescAnul = strArrys(1).Substring(strArrys(1).IndexOf("=") + 1)
            dgrDetalle.Items(3).Cells(10).Text = strDescAnul

            HidContIntento3.Value = Funciones.CheckInt(strArrys(2).Substring(strArrys(2).IndexOf("=") + 1))
        Else
            HidContIntento3.Value = Funciones.CheckInt("1")
        End If


        If Me.HidFila4.Value.Trim() <> "" Then
            strArrys = Me.HidFila0.Value.Split("|")

            strEstadoAnu = "" : strEstadoAnu = strArrys(0).Substring(strArrys(0).IndexOf("=") + 1)
            dgrDetalle.Items(4).Cells(9).Text = strEstadoAnu

            strDescAnul = "" : strDescAnul = strArrys(1).Substring(strArrys(1).IndexOf("=") + 1)
            dgrDetalle.Items(4).Cells(10).Text = strDescAnul

            HidContIntento4.Value = Funciones.CheckInt(strArrys(2).Substring(strArrys(2).IndexOf("=") + 1))
        Else
            HidContIntento4.Value = Funciones.CheckInt("1")
        End If

        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "  ** HidFila0: " & HidFila0.Value)
        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "  ** HidFila1: " & HidFila1.Value)
        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "  ** HidFila2: " & HidFila2.Value)
        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "  ** HidFila3: " & HidFila3.Value)
        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "  ** HidFila4: " & HidFila4.Value)
        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " **************** FIN load_values_pos()   ******************")
    End Sub

    Private Sub load_data_param_pos(ByVal drDocSel As DataRow)
        strIdentifyLogPOS = "detaConsultaPagos: [" + CStr(drDocSel("PEDIN_NROPEDIDO")) + "] "

        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " ***********************************************************")
        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " *********** INICIO load_data_param_pos()    ***************")
        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " ***********************************************************")

        Dim objSicarDB As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim strIp As String = CurrentTerminal()
        Dim strEstadoPos As String = "1"
        Dim strTipoTarj As String = ""
        Dim ds As DataSet
        Dim strMensaje As String = ""
        Dim strIpClient As String = Funciones.CheckStr(Session("IpLocal"))
        hidIpLocal.Value = strIpClient
        Dim bolMsjV As Boolean = False
        Dim bolMsjM As Boolean = False

        Me.HidCodOpera.Value = ClsKeyPOS.strCodOpeVE & "|" & ClsKeyPOS.strCodOpeVC & "|" & ClsKeyPOS.strCodOpeAN
        Me.HidDesOpera.Value = ClsKeyPOS.strDesOpeVE & "|" & ClsKeyPOS.strDesOpeVC & "|" & ClsKeyPOS.strDesOpeAN
        Me.HidTipoOpera.Value = ClsKeyPOS.strOpeFina & "|" & ClsKeyPOS.strNoFina 'OPE FI(90) - OPE NOFI 91
        Me.HidTipoTarjeta.Value = ClsKeyPOS.strCodTarjetaVS & "|" & ClsKeyPOS.strCodTarjetaMC
        Me.HidTipoPOS.Value = ClsKeyPOS.strTipoPosVI & "|" & ClsKeyPOS.strTipoPosMC _
        & "|" & ClsKeyPOS.strTipoPosAM & "|" & ClsKeyPOS.strTipoPosDI
        Me.HidTipoTran.Value = ClsKeyPOS.strTipoTransPAG & "|" & ClsKeyPOS.strTipoTransANU & "|" _
            & ClsKeyPOS.strTipoTransRIM & "|" & ClsKeyPOS.strTipoTransRDO & "|" & _
            ClsKeyPOS.strTipoTransRTO & "|" & ClsKeyPOS.strTipoTransAPP & "|" & ClsKeyPOS.strTipoTransCIP
        Me.HidEstTrans.Value = ClsKeyPOS.strEstTRanPen & "|" & ClsKeyPOS.strEstTRanPro _
        & "|" & ClsKeyPOS.strEstTRanAce & "|" & ClsKeyPOS.strEstTRanRec & "|" & ClsKeyPOS.strEstTRanInc '0PENDIENTE |1EN PROCESO |2ACEPTADO|3RECHAZADO|4INCOMPLETO
        Me.HidApliPOS.Value = ClsKeyPOS.strConstMC_POS
        Me.HidTipoMoneda.Value = ClsKeyPOS.strTipoMonSoles & "|" & ClsKeyPOS.strMonedaMC_Soles '1|604
        Me.HidTipMonPOSVISA.Value = ClsKeyPOS.strMonedaVisa_Soles
        Me.HidTipoPago.Value = ClsKeyPOS.strTipPagoDP '11  Documentos Pagados
        Me.HidPtoVenta.Value = Funciones.CheckStr(Session("ALMACEN"))
        Me.HidEstAnulGrilla.Value = ClsKeyPOS.strEstadAnul

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & "load_data_param_pos : Validacion Integracion INI")
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & "load_data_param_pos : " & "HidPtoVenta : " & HidPtoVenta.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & "load_data_param_pos : " & "strIpClient : " & strIpClient)


        Dim strCodRptaFlag As String = ""
        Dim strMsgRptaFlag As String = ""

        Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS

        'INI CONSULTA INTEGRACION AUTOMATICO POS

        Dim strFlagIntAut As String = ""

        strCodRptaFlag = "" : strMsgRptaFlag = ""
        objConsultaPos.Obtener_Integracion_Auto(Funciones.CheckStr(Me.HidPtoVenta.Value), strIpClient, String.Empty, strFlagIntAut, strCodRptaFlag, strMsgRptaFlag)

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & "load_data_param_pos : " & "strFlagIntAut : " & Funciones.CheckStr(strFlagIntAut))
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & "load_data_param_pos : " & "strCodRptaFlag : " & Funciones.CheckStr(strCodRptaFlag))
        'INI PROY-140126
        Dim MaptPath As String
        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
        MaptPath = "( Class : " & MaptPath & "; Function: load_data_param_pos)"
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & "load_data_param_pos : " & "strMsgRptaFlag : " & Funciones.CheckStr(strMsgRptaFlag) & MaptPath)
        'FIN PROY-140126



        Me.HidIntAutPos.Value = Funciones.CheckStr(strFlagIntAut)

        'FIN CONSULTA INTEGRACION AUTOMATICO POS
        'PROY-31949 - Inicio

        HidNumIntentosAnular.Value = ClsKeyPOS.strNumIntentosAnular
        HidMsjErrorNumIntentos.Value = ClsKeyPOS.strMsjErrorNumIntentos
        HidMsjErrorTimeOut.Value = ClsKeyPOS.strMsjErrorTimeOut
        HidMsjNumIntentosPago.Value = ClsKeyPOS.strMsjPagoNumIntentos


        Dim dsCajeroA As New DataSet
        Dim objOfflineCaja As New COM_SIC_OffLine.clsOffline
        Dim cultureNameX As String = "es-PE"
        Dim cultureX As CultureInfo = New CultureInfo(cultureNameX)
        Dim dateTimeValueCaja As DateTime = Convert.ToDateTime(DateTime.Now, cultureX)
        Dim sFechaCaj As String = dateTimeValueCaja.ToLocalTime.ToShortDateString
        dateTimeValueCaja = Convert.ToDateTime(DateTime.Now, cultureX)
        sFechaCaj = dateTimeValueCaja.ToString("dd/MM/yyyy")
        dsCajeroA = objOfflineCaja.GetDatosAsignacionCajero(HidPtoVenta.Value, sFechaCaj, Funciones.CheckStr(Session("USUARIO")).PadLeft(10, "0"))

        ' Validar cierre de caja
        If dsCajeroA.Tables(0).Rows.Count > 0 Then
            For cont As Int32 = 0 To dsCajeroA.Tables(0).Rows.Count - 1
                If dsCajeroA.Tables(0).Rows(cont).Item("CAJA_CERRADA") = "S" Then
                    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "MENSAJE : " & ClsKeyPOS.strMsjCajaCerrada)
                    HidMsjCajaCerrada.Value = ClsKeyPOS.strMsjCajaCerrada
                    HidFlagCajaCerrada.Value = 1
                Else
                    HidFlagCajaCerrada.Value = 0
                End If
            Next
        Else
            HidMsjCajaCerrada.Value = ClsKeyPOS.strMsjCajaCerrada
            HidFlagCajaCerrada.Value = 1
        End If


        'PROY-31949 - Fin

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & "load_data_param_pos : Validacion Integracion FIN")



        Me.HidTransMC.Value = ClsKeyPOS.strTranMC_Compra & "|" & ClsKeyPOS.strTranMC_Anulacion & "|" & ClsKeyPOS.strTranMC_RepDetallado & _
           "|" & ClsKeyPOS.strTranMC_RepTotales & "|" & ClsKeyPOS.strTranMC_ReImpresion & "|" & ClsKeyPOS.strTranMC_Cierre & "|" & ClsKeyPOS.strPwdComercio_MC

        Me.HidDatoAuditPos.Value = Funciones.CheckStr(CurrentTerminal()) & "|" & _
           Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")) & "|" & _
           Funciones.CheckStr(Session("USUARIO"))
        Me.hidAnulacionRechazada.Value = ClsKeyPOS.strAnulacionRechazada
        Me.hidAnulacionExitosa.Value = ClsKeyPOS.strAnulacionExitosa
        Me.hidAnulacionIncompleta.Value = ClsKeyPOS.strAnulacionIncompleta
        Me.hidMsjCajero.Value = ClsKeyPOS.strMsjValidacionCajero
        Me.hidMsjIpDesconfigurado.Value = ClsKeyPOS.strIPMsjDesconfigurado

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidCodOpera : " & HidCodOpera.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidDesOpera : " & HidDesOpera.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidTipoOpera : " & HidTipoOpera.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidTipoTarjeta : " & HidTipoTarjeta.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidTipoPOS : " & HidTipoPOS.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidTipoTran : " & HidTipoTran.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidEstTrans : " & HidEstTrans.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidApliPOS : " & HidApliPOS.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidTransMC : " & HidTransMC.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidTipoMoneda : " & HidTipoMoneda.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidTipMonPOSVISA : " & HidTipMonPOSVISA.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidTipoPago : " & HidTipoPago.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidPtoVenta : " & HidPtoVenta.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidEstAnulGrilla : " & HidEstAnulGrilla.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidIntAutPos : " & HidIntAutPos.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "HidDatoAuditPos : " & HidDatoAuditPos.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "hidAnulacionRechazada : " & hidAnulacionRechazada.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "hidAnulacionExitosa : " & hidAnulacionExitosa.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "hidAnulacionIncompleta : " & hidAnulacionIncompleta.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "hidMsjCajero : " & hidMsjCajero.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "hidMsjIpDesconfigurado : " & hidMsjIpDesconfigurado.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "CurrentTerminal : " & strIp)
        'DATOS DEL POS
        Me.HidDatoPosVisa.Value = ""
        Me.HidDatoPosMC.Value = ""

        If HidIntAutPos.Value = "1" And HidIdCabez.Value.Length >= 0 Then

            'VISA DATOS INICIO
            strTipoTarj = "V"

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "== INI ConsultarDatosPOS VISA ==")
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "PKG_SISCAJ_POS.SISCAJS_CON_POS_MC")
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "IN strIpClient : " & strIpClient)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "IN HidPtoVenta : " & HidPtoVenta.Value)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "IN strEstadoPos : " & strEstadoPos)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "IN strTipoVisa : " & strTipoTarj)
            ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoTarj)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "OUT Total VISA : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

            If ds.Tables(0).Rows.Count > 0 Then
                Me.HidDatoPosVisa.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
                & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
                & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
                & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
                & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
                & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
                & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
                & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA"))
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "OUT HidDatoPosVisa : " & HidDatoPosVisa.Value)
            Else
                bolMsjV = True
            End If
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "== FIN ConsultarDatosPOS VISA ==")
            'VISA FIN

            'MC INICIO
            strTipoTarj = "M"
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "== INI ConsultarDatosPOS MCD ==")
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "PKG_SISCAJ_POS.SISCAJS_CON_POS_MC")
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "IN strIpClient : " & strIpClient)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "IN HidPtoVenta : " & HidPtoVenta.Value)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "IN strEstadoPos : " & strEstadoPos)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "IN strTipoVisa : " & strTipoTarj)
            ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoTarj)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "OUT Total MCD : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

            If ds.Tables(0).Rows.Count > 0 Then
                Me.HidDatoPosMC.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
                & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
                & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
                & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
                & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
                & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
                & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
                & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA"))
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "OUT HidDatoPosMC : " & HidDatoPosMC.Value)

            Else
                bolMsjM = True
            End If
            'MC FIN
            If bolMsjV = True Or bolMsjM = True Then
                strMensaje = hidMsjIpDesconfigurado.Value
                Response.Write("<script>alert('" & strMensaje & "');</script>")
            End If

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & "== FIN ConsultarDatosPOS MCD ==")
        End If

        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " - " & " *********** FIN load_data_param_pos() -   *****************")

    End Sub

    Private Sub dgrDetalle_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrDetalle.ItemDataBound
        Dim valor = e.Item.Cells(8).Text
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            If (HidIntAutPos.Value = "1") Then 'Si está activo la integración con POS 1=SI \\ 0=NO
                If e.Item.Cells(6).Text.Equals("VISA") Or e.Item.Cells(6).Text.Equals("MASTERCARD") Or e.Item.Cells(6).Text.Equals("AMEX") Or e.Item.Cells(6).Text.Equals("DINERS") Then  'Pago con Tarjeta
                    If bolIsVerFormPag = True Then 'Se va a Anular Pago
                        e.Item.Cells(12).Visible = False
                        e.Item.Cells(13).Visible = False
                    Else  ''Solo ver Formas de PAgo
                        e.Item.Cells(12).Visible = True
                        e.Item.Cells(13).Visible = True
                    End If
                Else
                    e.Item.Cells(12).Visible = False
                    e.Item.Cells(13).Visible = False
                End If ''Visa y MCD
            Else '' 00->> Nada de POS
                e.Item.Cells(12).Visible = False
                e.Item.Cells(13).Visible = False
            End If
        End If
    End Sub
    'PROY-27440 FIN



    'INC000000961273 - INICIO
    Private Sub Anula_Portabilidad(ByVal strNroPedido As String)

        Dim i, intResultado As Integer
        Dim resultado As Integer = 0
        Dim oListaTelefono As DataSet
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim strTipoVenta As Integer = 0

        Dim blnOK As Boolean
        Dim strMensaje As String
        Dim objSans As New NEGOCIO_SIC_SANS.SansNegocio

        Try
            If Not strNroPedido = "" Then
                oListaTelefono = objCajas.FP_Get_TelefonosPorta(strNroPedido, strTipoVenta, intResultado)

                If intResultado = 0 Then
                    For i = 0 To oListaTelefono.Tables(0).Rows.Count - 1
                        Dim msisdn As String = ""
                        Dim NroSEC As String = ""
                        NroSEC = oListaTelefono.Tables(0).Rows(i).Item("SOLIN_CODIGO")
                        msisdn = oListaTelefono.Tables(0).Rows(i).Item("PORT_NUMERO")

                        objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - " & "Telefono: " & i)
                        objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - " & "NroSEC: " & NroSEC)
                        objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - " & "msisdn: " & msisdn)

                        blnOK = objSans.Set_AnulaTelefonoPortable(msisdn, strMensaje)

                        objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - " & "blnOK: " & blnOK)
                        objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - " & "strMensaje: " & strMensaje)
                    Next
        End If
        End If
        Catch ex As Exception
            Response.Write("<script>alert('" & "Error. Anulacion de Venta de Portabilidad. " & ex.Message & "')</script>")
        End Try
    End Sub
    'INC000000961273 - FIN
End Class