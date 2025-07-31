Imports COM_SIC_Cajas
Imports System.Globalization

Public Class resDocumentosFijoPaginas
    Inherits SICAR_WebBase '''System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents hdnMensaje As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnPuntoDeVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnBinAdquiriente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodComercio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnRutaLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDetalleLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtIdentificadorCliente As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNombreCliente As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNumeroDocumentos As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtValorDeuda As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cboTipDocumento1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDoc1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTarjCred As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBIN As System.Web.UI.WebControls.TextBox
    Protected WithEvents hdnEjecutado As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblTC As System.Web.UI.WebControls.Label
    Protected WithEvents hidCanal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidNumeroTrace As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTipoIdentificador As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidIdentificador As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents dgDocumentosPago As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidFPCodigoEfectivo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCodigoServicio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidFPCodigoCheque As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtRecibidoPen As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtVuelto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecibidoUsd As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents lblTipoMonedaDeuda As System.Web.UI.WebControls.Label
    Protected WithEvents txtTotalRecibosDolares As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTotalRecibosSoles As System.Web.UI.WebControls.TextBox
    Protected WithEvents pnlDOCTotalDolares As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlBancoGirador01 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents divBancoGirador01 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents spnBotones As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents spnTitulo As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lblSoloing As System.Web.UI.WebControls.Label
    Protected WithEvents txtNombreCli_old As System.Web.UI.WebControls.TextBox
    Protected WithEvents anteriorButton As System.Web.UI.WebControls.Button
    Protected WithEvents siguienteButton As System.Web.UI.WebControls.Button
    Protected WithEvents chkDocumentosConsultar As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtNumeroDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents primeroButton As System.Web.UI.WebControls.Button
    Protected WithEvents ultimoButton As System.Web.UI.WebControls.Button
    Protected WithEvents paginacionLabel As System.Web.UI.WebControls.Label
    Protected WithEvents totalRecibos As System.Web.UI.WebControls.Label
    Protected WithEvents hiddTotal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txttotalOculto As System.Web.UI.WebControls.TextBox
    Protected WithEvents hidPagina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidUltimoRecibo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hiddTotalOriginal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hiddPaginaSinAjuste As System.Web.UI.HtmlControls.HtmlInputHidden

    'PROY-27440 INI
    Protected WithEvents HidPtoVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIntAutPos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidGrabAuto As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidCodOpera As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDesOpera As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoOpera As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoTarjeta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidEstTrans As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoPOS As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoTran As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipPOS1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipPOS2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipPOS3 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFila1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFila2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFila3 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents LnkPos1 As System.Web.UI.HtmlControls.HtmlAnchor
    Protected WithEvents icoTranPos1 As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents HidTipoMoneda As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTransMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMonedaMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidApliPOS As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMonedaVisa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoPosVisa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoPosMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoAuditPos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIdCabez As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblEnvioPos As System.Web.UI.WebControls.Label
    Protected WithEvents hidF1 As System.Web.UI.HtmlControls.HtmlInputHidden
    'PROY-27440 FIN

    'Proy-31949 INICIO
    Protected WithEvents hdnFlagIntAutPos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidNumIntentosPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidNumIntentosAnular As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjErrorNumIntentos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjErrorTimeOut As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjNumIntentosPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFlagCajaCerrada As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjCajaCerrada As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMedioPagoPermitidas As System.Web.UI.HtmlControls.HtmlInputHidden
    'Proy-31949 FIN

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object
#End Region
    'PROY-27440 INI
    Public objFileLogPos As New SICAR_Log
    Dim nameFilePos As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivoPos As String = objFileLogPos.Log_CrearNombreArchivo(nameFilePos)
    'PROY-27440 FIN

    'PROY-27440 INI
    Private Sub load_data_param_pos()
        Dim strPedidoLog As String = "Identificador Recibos: [" & Funciones.CheckStr(Request.QueryString("ID")) & "] "

        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " =======================================")
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " ===== INICIO load_data_param_pos  =====")

        Me.HidTipoPago.Value = ClsKeyPOS.strTipPagoRCFP '10  Recaudación Clientes Fijos y Paginas
        Me.HidPtoVenta.Value = Funciones.CheckStr(Session("ALMACEN"))

        Dim strIpClient As String = Funciones.CheckStr(Session("IpLocal"))

        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : Validacion Integracion INI")
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidPtoVenta : " & HidPtoVenta.Value)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIpClient : " & strIpClient)


        Dim strCodRptaFlag As String = ""
        Dim strMsgRptaFlag As String = ""

        Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS

        'INI CONSULTA INTEGRACION AUTOMATICO POS

        Dim strFlagIntAut As String = ""

        strCodRptaFlag = "" : strMsgRptaFlag = ""
        objConsultaPos.Obtener_Integracion_Auto(Funciones.CheckStr(Me.HidPtoVenta.Value), strIpClient, String.Empty, strFlagIntAut, strCodRptaFlag, strMsgRptaFlag)

        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strFlagIntAut : " & Funciones.CheckStr(strFlagIntAut))
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strCodRptaFlag : " & Funciones.CheckStr(strCodRptaFlag))
        'INI PROY-140126
        Dim MaptPath As String
        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
        MaptPath = "( Class : " & MaptPath & "; Function: load_data_param_pos)"
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strMsgRptaFlag : " & Funciones.CheckStr(strMsgRptaFlag) & MaptPath)
        'FIN PROY-140126
        Me.HidIntAutPos.Value = Funciones.CheckStr(strFlagIntAut)
        'FIN CONSULTA INTEGRACION AUTOMATICO POS

        'INI CONSULTA PAGO AUTOMATICO POS
        Dim strFlagPagAut As String = ""

        objConsultaPos.Obtener_Pago_Auto(Funciones.CheckStr(Me.HidPtoVenta.Value), strIpClient, String.Empty, strFlagPagAut, strCodRptaFlag, strMsgRptaFlag)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strFlagPagAut : " & Funciones.CheckStr(strFlagPagAut))
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strCodRptaFlag : " & Funciones.CheckStr(strCodRptaFlag))
        'INI PROY-140126
        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
        MaptPath = "( Class : " & MaptPath & "; Function: load_data_param_pos)"
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strMsgRptaFlag : " & Funciones.CheckStr(strMsgRptaFlag) & MaptPath)
        'FIN PROY-140126
        Me.HidGrabAuto.Value = Funciones.CheckStr(strFlagPagAut)

        'FIN CONSULTA PAGO AUTOMATICO POS

        'Proy-31949 Inicio
        Me.hdnFlagIntAutPos.Value = Funciones.CheckStr(Session("FlagIntAutPos"))
        HidNumIntentosPago.Value = ClsKeyPOS.strNumIntentosPago
        HidNumIntentosAnular.Value = ClsKeyPOS.strNumIntentosAnular
        HidMsjErrorNumIntentos.Value = ClsKeyPOS.strMsjErrorNumIntentos
        HidMsjErrorTimeOut.Value = ClsKeyPOS.strMsjErrorTimeOut
        HidMsjNumIntentosPago.Value = ClsKeyPOS.strMsjPagoNumIntentos

        Dim dsCajeroA As DataSet
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
                    objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " - " & "MENSAJE : " & ClsKeyPOS.strMsjCajaCerrada)
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
        'Proy-31949 Fin

        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : Validacion Integracion FIN")
        Me.HidCodOpera.Value = ClsKeyPOS.strCodOpeVE & "|" & _
            ClsKeyPOS.strCodOpeREC & "|" & ClsKeyPOS.strCodOpeAN

        Me.HidDesOpera.Value = ClsKeyPOS.strDesOpeVE & "|" & _
            ClsKeyPOS.strDesOpeREC & "|" & ClsKeyPOS.strDesOpeAN
        Me.HidTipoOpera.Value = ClsKeyPOS.strOpeFina & "|" & ClsKeyPOS.strNoFina 'OPE FI(90)
        Me.HidTipoTarjeta.Value = ClsKeyPOS.strCodTarjetaVS & "|" & ClsKeyPOS.strCodTarjetaMC

        Dim strTipoPago As String = Me.HidTipoPago.Value
        strPedidoLog = Devolver_TipoPago_POS(strTipoPago) & " Identificador : [" & Funciones.CheckStr(Request.QueryString("ID")) & "] "
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "  HidIntAutPos : " & HidIntAutPos.Value)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "  HidGrabAuto : " & HidGrabAuto.Value)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "  HidCodOpera : " & HidCodOpera.Value)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "  HidTipoOpera : " & HidCodOpera.Value)

        Me.HidEstTrans.Value = ClsKeyPOS.strEstTRanPen & "|" & ClsKeyPOS.strEstTRanPro _
        & "|" & ClsKeyPOS.strEstTRanAce & "|" & ClsKeyPOS.strEstTRanRec & "|" & ClsKeyPOS.strEstTRanInc

        Me.HidTipoPOS.Value = ClsKeyPOS.strTipoPosVI & "|" & ClsKeyPOS.strTipoPosMC _
        & "|" & ClsKeyPOS.strTipoPosAM & "|" & ClsKeyPOS.strTipoPosDI

        Me.HidTipoTran.Value = ClsKeyPOS.strTipoTransPAG & "|" & ClsKeyPOS.strTipoTransANU & "|" _
        & ClsKeyPOS.strTipoTransRIM & "|" & ClsKeyPOS.strTipoTransRDO & "|" & _
        ClsKeyPOS.strTipoTransRTO & "|" & ClsKeyPOS.strTipoTransAPP & "|" & ClsKeyPOS.strTipoTransCIP

        Me.HidTipoMoneda.Value = ClsKeyPOS.strTipoMonSoles

        Me.HidTransMC.Value = ClsKeyPOS.strTranMC_Compra & "|" & ClsKeyPOS.strTranMC_Anulacion & "|" _
        & ClsKeyPOS.strTranMC_RepDetallado & _
        "|" & ClsKeyPOS.strTranMC_RepTotales & "|" & ClsKeyPOS.strTranMC_ReImpresion & _
            "|" & ClsKeyPOS.strTranMC_Cierre & "|" & ClsKeyPOS.strPwdComercio_MC

        Me.HidMonedaMC.Value = ClsKeyPOS.strMonedaMC_Soles & "|" & ClsKeyPOS.strMonedaMC_Dolares
        Me.HidApliPOS.Value = ClsKeyPOS.strConstMC_POS
        Me.HidMonedaVisa.Value = ClsKeyPOS.strMonedaVisa_Soles & "|" & ClsKeyPOS.strMonedaVisa_Dolares

        'DATOS DEL POS
        Me.HidDatoPosVisa.Value = ""
        Me.HidDatoPosMC.Value = ""

        Me.HidDatoAuditPos.Value = Funciones.CheckStr(CurrentTerminal()) & "|" & _
        Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")) & "|" & _
        Funciones.CheckStr(Session("USUARIO"))

        If Me.HidIntAutPos.Value = "1" Then
            Dim objSicarDB As New COM_SIC_Activaciones.clsTransaccionPOS
            Dim strIp As String = CurrentTerminal()
            Dim strEstadoPos As String = "1"
            Dim strTipoVisa As String = "V"
            Dim ds As DataSet
            Dim bvalida As Integer = 0

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIpClient : " & strIpClient)

            'VISA INICIO
            strTipoVisa = "V"
            ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoVisa)

            Dim arrIPDesc(2) As String
            arrIPDesc(0) = strIpClient

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " strIp : " & strIp)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " strIpClient : " & strIpClient)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " HidPtoVenta : " & HidPtoVenta.Value)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " strEstadoPos : " & strEstadoPos)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " strTipoVisa : " & strTipoVisa)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " Total VISA " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

            Dim strMensajeVisa As String = ClsKeyPOS.strIPMsjDesconfigurado

            If ds.Tables(0).Rows.Count > 0 Then
                Me.HidDatoPosVisa.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
                & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
                & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
                & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
                & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
                & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
                & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
                & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA")) _
                & "|POSV_FLAGPOS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSC_FLG_SICAR")) 'Proy-31949

                objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidDatoPosVisa : " & HidDatoPosVisa.Value)
            Else
                bvalida = 1
                Response.Write("<script>alert('" & strMensajeVisa & "');</script>")
            End If
            'VISA FIN

            'MC INICIO
            strTipoVisa = "M"
            ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoVisa)

            Dim strMensajeMC As String = ClsKeyPOS.strIPMsjDesconfigurado

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " strIp : " & strIp)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " HidPtoVenta : " & HidPtoVenta.Value)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " strEstadoPos : " & strEstadoPos)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " strTipoVisa : " & strTipoVisa)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " Count : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

            If ds.Tables(0).Rows.Count > 0 Then
                Me.HidDatoPosMC.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
                & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
                & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
                & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
                & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
                & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
                & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
                & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA")) _
                & "|POSV_FLAGPOS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSC_FLG_SICAR")) 'Proy-31949

                objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " HidDatoPosMC : " & HidDatoPosMC.Value)
            Else
                If bvalida = 0 Then
                    Response.Write("<script>alert('" & strMensajeMC & "');</script>")
                End If
            End If
            'MC FIN

            'Proy-31949 Inicio
            'MEDIOS DE PAGO PERMITIDO CON EL POS - INICIO'
            Dim dtViaPagoPOS As DataTable
            Dim strMsgRpta As String = String.Empty
            Dim strCodRpta As String = String.Empty
            Dim objConsultasTarjetaPos As New COM_SIC_Activaciones.clsTarjetasPOS
            Dim mediosPagoPermitidos As String
            mediosPagoPermitidos = ""
            dtViaPagoPOS = objConsultasTarjetaPos.ConsultarViasPagoPos("", strCodRpta, strMsgRpta)

            If dtViaPagoPOS.Rows.Count > 0 Then
                For Each item As DataRow In dtViaPagoPOS.Rows
                    mediosPagoPermitidos = mediosPagoPermitidos & Funciones.CheckStr(item("CCINS")) & ";" & Funciones.CheckStr(item("TIP_TARJETA")) & "|"
                Next
            End If
            HidMedioPagoPermitidas.Value = mediosPagoPermitidos
            'MEDIOS DE PAGO PERMITIDO CON EL POS - FIN
            'Proy-31949 Fin

        End If
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " ===== FIN load_data_param_pos  =====")

    End Sub
    'PROY-27440 FIN

    'Proy-31949 Inicio
    Private Sub validar_pedido_pos()
        Dim strPedidoLog As String = "Pedido: [" & Funciones.CheckStr(Request.QueryString("ID")) & "] "

        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Inicio : ")

        Dim dsPedidoPOS As DataSet
        Dim objPedidoPOS As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim strNroPedido As String = Funciones.CheckStr(Request.QueryString("ID"))
        Dim strNroRef As String = ""
        Dim strCodRpta As String = ""
        Dim strMsgRpta As String = ""
        Dim strTipoPago As String = Funciones.CheckStr(Me.HidTipoPago.Value)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "strTipoPago : " & strTipoPago)

        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Inicio ConsultarPedidoPOS ")
        dsPedidoPOS = objPedidoPOS.ConsultarPedidoPOS(strNroPedido, strNroRef, strCodRpta, strMsgRpta)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "strCodRpta : " & strCodRpta)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "strMsgRpta : " & strMsgRpta)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Fin ConsultarPedidoPOS ")
        Dim intIndexCombo As Int32 = 0
        Dim strTipoTarjeta As String = ""
        Dim strTipoTarjetaSap As String = ""
        Dim strComboTipoTarj As String = String.Empty

        If strCodRpta = "0" AndAlso Not dsPedidoPOS Is Nothing AndAlso dsPedidoPOS.Tables.Count > 0 AndAlso dsPedidoPOS.Tables(0).Rows.Count > 0 Then

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Count : " & Funciones.CheckStr(dsPedidoPOS.Tables(0).Rows.Count))

            Dim intRows As Integer = dsPedidoPOS.Tables(0).Rows.Count
            If intRows > 1 Then
                intRows = 1
            End If

            For i As Int32 = 0 To intRows - 1
                Dim objTxt As HtmlInputHidden
                Dim objCombo As DropDownList
                Dim objTipPos As HtmlInputHidden
                strTipoTarjeta = ""

                objTxt = CType(Me.FindControl("HidFila" & i + 1), HtmlInputHidden)
                objCombo = CType(Me.FindControl("cboTipDocumento" & i + 1), DropDownList)
                strTipoTarjeta = Funciones.CheckStr(dsPedidoPOS.Tables(0).Rows(i)("TRNSV_TIPO_TARJETA_POS"))
                objTipPos = CType(Me.FindControl("HidTipPOS" & i + 1), HtmlInputHidden)

                Select Case strTipoTarjeta
                    Case "VISA"
                        strTipoTarjetaSap = "ZVIS"
                        objTipPos.Value = "VIS"
                    Case "MASTERCARD"
                        strTipoTarjetaSap = "ZMCD"
                        objTipPos.Value = "MCD"
                    Case "AMEX"
                        strTipoTarjetaSap = "ZAEX"
                        objTipPos.Value = "AMX"
                    Case "DINERS"
                        strTipoTarjetaSap = "ZDIN"
                        objTipPos.Value = "DIN"
                End Select

                For row As Int32 = 0 To objCombo.Items.Count - 1
                    strComboTipoTarj = objCombo.Items(row).Value.Substring(0, objCombo.Items(row).Value.Length - 2)

                    If strComboTipoTarj = strTipoTarjetaSap Then
                        intIndexCombo = objCombo.Items.IndexOf(objCombo.Items.FindByValue(objCombo.Items(row).Value))
                    End If
                Next

                objTxt.Value = "Monto=" & Funciones.CheckStr(dsPedidoPOS.Tables(0).Rows(i)("TRNSN_MONTO")) & _
                "|Tarjeta=" & Funciones.CheckStr(dsPedidoPOS.Tables(0).Rows(i)("TRNSV_NRO_TARJETA")) & _
                "|ComboIndex=" & intIndexCombo & _
                "|NroReferncia=" & Funciones.CheckStr(dsPedidoPOS.Tables(0).Rows(i)("TRNSV_ID_REF"))

                objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "objTxt.Value : " & objTxt.Value)


                Me.HidIdCabez.Value = Funciones.CheckStr(dsPedidoPOS.Tables(0).Rows(i)("TRNSN_ID_CAB"))
                objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "HidIdCabez : " & HidIdCabez.Value)
            Next
        End If

        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Fin : ")

    End Sub
    'Proy-31949 Fin

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

'#End Region     'PROY-27440 

    '*****************************************************'
    '* Constantes de la Página
    '*****************************************************'
    Public Const cteNOMBRECLIENTE = 0
    Public Const cteIDENTIFICADORCLIENTE = 1
    Public Const cteNUMEROOPERACIONCOBRANZA = 2
    Public Const cteVALORDEUDA = 3
    Public Const cteNUMERODOCUMENTOS = 4
    Public Const cteFECHAHORATRANSACCION = 5
    Public Const cteNUMEROTRACE = 6
    Public Const cteNUMERODEUDA = 0
    Public Const cteCODMONEDA_SOLES = "604"
    Public Const cteVALMONEDA_SOLES = "PEN"
    Public Const cteCODMONEDA_DOLARES = "840"
    Public Const cteVALMONEDA_DOLARES = "USD"
    Private Const cteRECAUDACIONDOLARES = 103


    Public dsPaginacion As DataSet
    Public dtPaginacion As New DataTable
    Const cteACCION_CONFIRMACION As Byte = 1 '//E75810 jtnd
    Private numeroDdocumentosDevueltos%

    'Public Recibos() As String

    Property PAGINA_ANTERIOR() As String
        Get
            Return Funciones.CheckStr(Me.ViewState("PAGINA_ANTERIOR"))
        End Get
        Set(ByVal Value As String)
            Me.ViewState("PAGINA_ANTERIOR") = Value
        End Set
    End Property

#Region "Eventos"
    'PROY-27440 INI
    Private Sub load_values_pos()
        Dim strTipoPago As String = Me.HidTipoPago.Value
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, "load_values_pos : " & "strTipoPago: " & strTipoPago)
        Dim strPedidoLog As String = Devolver_TipoPago_POS(strTipoPago) & "Identificador : [" & Funciones.CheckStr(Request.QueryString("pIdent")) & "] "

        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_values_pos : " & "Inicio")
        Dim strArrys As String()
        Dim strMonto As String
        Dim strTarjeta As String

        If Me.HidFila1.Value.Trim() <> "" Then
            strArrys = Me.HidFila1.Value.Split("|")
            'MONTO
            strMonto = "" : strMonto = strArrys(0).Substring(strArrys(0).IndexOf("=") + 1)
            Me.txtMonto1.Text = strMonto
            'NRO TARJETA
            strTarjeta = "" : strTarjeta = strArrys(1).Substring(strArrys(1).IndexOf("=") + 1)
            Me.txtDoc1.Text = strTarjeta
            'TIPO DE TARJETA 
            Me.cboTipDocumento1.SelectedIndex = Funciones.CheckInt(strArrys(2).Substring(strArrys(2).IndexOf("=") + 1))

        End If

        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_values_pos : " & "HidFila1 : " & HidFila1.Value)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_values_pos : " & "HidFila2 : " & HidFila2.Value)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_values_pos : " & "HidFila3 : " & HidFila3.Value)

    End Sub
    'PROY-27440 FIN
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Try
                If Not Page.IsPostBack Then

                    Me.load_data_param_pos()     'PROY-27440 FIN
                    'hiddTotal.Value = 0
                    '---
                    InicializaControles()
                    LlenaCombos()
                    LeeDatosValidar()
                    '--- constFPCodigoCheque

                    Me.validar_pedido_pos() 'Proy-31949

                    Me.Session("hayMasDocumentos") = False
                    Me.Session("posicionNuevaConsulta") = 0
                    Me.Session("tamanioPaginacion") = 0
                    Me.Session("posicionUltimaConsulta") = 0
                    numeroDdocumentosDevueltos = 0
                    ObtenerDocumentosPorPagar() '''AQUI LLAMA AL ST- CONSULTA RECIBOS AL ST
                    If (Not txtNombreCliente.Value.StartsWith("#C#")) Then
                        primeroButton.Enabled = False
                        siguienteButton.Enabled = False
                        anteriorButton.Enabled = False
                        ultimoButton.Enabled = False
                    End If
                Else 
                    Me.load_values_pos()  'PROY-27440 FIN
                End If
                If (cboTipDocumento1.SelectedValue = ConfigurationSettings.AppSettings("constFPCodigoCheque")) Then
                    divBancoGirador01.Style.Item("display") = "block"
                Else
                    divBancoGirador01.Style.Item("display") = "none"
                End If
            Catch ex As Exception
                Response.Write("<script language=jscript> alert('" + ex.Message + "'); window.location='" & Me.PAGINA_ANTERIOR & "';</script>")
                Response.End()
            End Try
        End If
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim sTramaFormaPagoSAP As String
        Dim sTramaCodMedioPago As String
        Dim objclsOffline As New COM_SIC_OffLine.clsOffline
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Try
            sTramaFormaPagoSAP = Me.ObtenerTramaFormaPagoSAP() '''METODO LOCAL
            Dim sMontoTotalAPagar As String = Me.ObtenerMontoTotalPagar() '''METODO LOCAL
            '--Se hace esta parte porque cuando se trae del lado cliente, se trae la trama separada por coma ",".
            'Dim sTramaRecibosST As String = Me.ObtenerTramaRecibosST() '''METODO LOCAL
            Dim sTramaRecibosST As String = String.Empty
            Me.Registrar_Paginacion()
            dsPaginacion = objclsOffline.GetConsultaPaginacion(Session.SessionID, 0)

            ''' VERIFICACION DE CUADRE DE CAJA 05.02.2014 TS-JTN
            Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
            If (objclsOffline.CuadreCajeroRealizado(Session("ALMACEN"), codUsuario)) Then
                Throw New InvalidOperationException("No puede realizar esta operacion, ya realizo cuadre de caja")
            End If
            ''' FIN VERIFICACION

            ''' TS-CCC *** INI :: VALIDACION DE MONTO MAXIMO EN EFECTIVO *****
            Dim dblEfecCaja As Double
            Dim dblTolerancia As Double
            Dim dsEfectivo As DataSet

            Dim cultureNameCaja As String = "es-PE"
            Dim cultureCaja As CultureInfo = New CultureInfo(cultureNameCaja)
            Dim dateTimeValueCajaEfe As DateTime
            dateTimeValueCajaEfe = Convert.ToDateTime(DateTime.Now, cultureCaja)
            Dim sFechaCaj0 As String = dateTimeValueCajaEfe.ToLocalTime.ToShortDateString

            dsEfectivo = objCajas.FP_Get_ListaParamOficina(Session("CANAL"), ConfigurationSettings.AppSettings("CodAplicacion"), Session("ALMACEN"))
            dblEfecCaja = objCajas.FP_CalculaEfectivo(Session("ALMACEN"), Session("USUARIO"), sFechaCaj0)
            dblTolerancia = dsEfectivo.Tables(0).Rows(0).Item("CAJA_TOLERANCIA_SOL")

            If dblEfecCaja >= dsEfectivo.Tables(0).Rows(0).Item("CAJA_MAX_DISP_SOL") + dblTolerancia Then
                Throw New InvalidOperationException("Ha alcanzado su maximo disponible de efectivo en caja. Debe depositar en caja buzón")
            End If
            ''' TS-CCC *** FIN :: VALIDACION DE MONTO MAXIMO EN EFECTIVO *****

            Dim ultimoReciboMarcado As String = ""
            Dim numeroReciboMontoMayor$ = ""

            Dim mayor# = 0
            Dim fechaMayor As DateTime = DateTime.MinValue


            If (Not Convert.IsDBNull(dsPaginacion.Tables(0).Rows(0)("ULTIMORECIBO"))) Then
                ultimoReciboMarcado = Convert.ToString(dsPaginacion.Tables(0).Rows(0)("ULTIMORECIBO")).Split(CChar("|"))(1)
            End If

            If (Not IsNothing(dsPaginacion)) Then
                Dim tbRecibos As DataTable = dsPaginacion.Tables(0)

                For Each fila As DataRow In tbRecibos.Rows
                    ' obtenemos la fecha de emision de la trama
                    Dim fecha$ = Convert.ToString(fila("TRAMA_RECIBO")).Split(CChar(";"))(5)
                    Dim dia% = Convert.ToInt32(fecha.Substring(6, 2))
                    Dim mes% = Convert.ToInt32(fecha.Substring(4, 2))
                    Dim año% = Convert.ToInt32(fecha.Substring(0, 4))
                    Dim fechaCompare As DateTime = New DateTime(año, mes, dia)

                    If (fechaMayor < fechaCompare) Then
                        fechaMayor = fechaCompare
                        numeroReciboMontoMayor = fila("NUMERO_RECIBO")
                    End If
                Next

                numeroReciboMontoMayor = numeroReciboMontoMayor.Trim()

                Dim filaRecibo$ = ""
                Dim monto$ = ""
                Dim montoSinRedondeo# = 0
                Dim montoConRedondeo# = 0
                Dim diferenciaRedondeo# = 0

                For Each fila As DataRow In tbRecibos.Rows
                    montoSinRedondeo += Convert.ToDouble(fila("MONTO"))
                Next
                montoConRedondeo = Convert.ToDouble(redondeoSicar(montoSinRedondeo))

                diferenciaRedondeo = montoSinRedondeo - montoConRedondeo
                diferenciaRedondeo = Math.Round(diferenciaRedondeo, 2)


                For Each fila As DataRow In tbRecibos.Rows
                    Dim ultimoReciboIterate$ = (String.Format("{0}", fila("NUMERO_RECIBO"))).Trim()
                    Dim montoLoop$ = String.Empty
                    filaRecibo = String.Format("{0}", fila("TRAMA_RECIBO"))
                    montoLoop = String.Format("{0}", FormatNumber(fila("MONTO"), 2))

                    Dim formaPago$ = cboTipDocumento1.SelectedValue
                    Dim monedaPago$ = Convert.ToString(fila("TRAMA_RECIBO")).Split(CChar(";"))(2)
                    'constFPCodigoEfectivo
                    'constMONCodigoSoles
                    If (numeroReciboMontoMayor = ultimoReciboIterate) And (formaPago = ConfigurationSettings.AppSettings("constFPCodigoEfectivo")) And (monedaPago = ConfigurationSettings.AppSettings("constMONCodigoSoles")) Then
                        monto = (montoLoop - diferenciaRedondeo) * 100
                    Else
                        monto = String.Format("{0}", fila("MONTO") * 100)
                    End If

                    If (monto < 0) Then
                        Throw New InvalidOperationException("Monto no se ajusta")
                    End If
                    monto = monto.PadLeft(11, CChar("0"))
                    filaRecibo = String.Concat(filaRecibo.Substring(0, filaRecibo.Length - 11), monto)

                    If (monto > 0) Then
                        sTramaRecibosST += String.Format("{0}|", filaRecibo)
                    End If
                    'sTramaRecibosST += String.Format("{0}|", filaRecibo)
                Next
                '''''''''''''''''''''''''
            End If

            sTramaRecibosST = Strings.Left(sTramaRecibosST, sTramaRecibosST.Length - 1)

            'Dim sTramaRecibosST As String = Me.ObtenerTramaRecibosST() '''METODO LOCAL

            '--se añade este bloque para no impedir que se procese el pago por la ausencia de DNI o RUC
            If Len(Trim(txtIdentificadorCliente.Value)) = 0 Then
                txtIdentificadorCliente.Value = "99999999"
            End If
            If Len(Trim(txtNombreCliente.Value)) = 0 Then
                Throw New Exception("El Nombre no puede estar vacío")
            End If
            Dim sEstadoPago As String = Me.DeterminaEstadoPago(hidCodigoServicio.Value)

            Me.PagarDocumentos_23(hdnRutaLog.Value, hdnDetalleLog.Value, hdnPuntoDeVenta.Value, hidCanal.Value, _
                                hdnBinAdquiriente.Value, hdnCodComercio.Value, hdnUsuario.Value, hidTipoIdentificador.Value, _
                                hidIdentificador.Value, sEstadoPago, sTramaFormaPagoSAP, sMontoTotalAPagar, _
                                sTramaRecibosST, hidNumeroTrace.Value)
        Catch ex As InvalidOperationException
            Dim sMensaje$ = ex.Message
            Dim strPaginaRetorno$ = Mid(Request.ServerVariables("HTTP_REFERER"), InStrRev(Request.ServerVariables("HTTP_REFERER"), "/") + 1)
            Dim sriptError$ = String.Format("<script type='text/javascript'> alert('{0}'); window.location='{1}'; </script>", sMensaje, strPaginaRetorno)
            Response.Write(sriptError)
            spnBotones.Attributes("style") = "display:block"
            spnTitulo.Attributes("style") = "display:none"
            hdnEjecutado.Value = String.Empty
        Catch ex As Exception
            Dim sMensaje$ = Left(ex.Message, ex.Message.Length - 1)
            Dim strPaginaRetorno$ = Mid(Request.ServerVariables("HTTP_REFERER"), InStrRev(Request.ServerVariables("HTTP_REFERER"), "/") + 1)
            Dim sriptError$ = String.Format("<script type='text/javascript'> alert('{0}'); window.location='{1}'; </script>", sMensaje, strPaginaRetorno)
            Response.Write(sriptError)
            spnBotones.Attributes("style") = "display:block"
            spnTitulo.Attributes("style") = "display:none"
            hdnEjecutado.Value = String.Empty
        Finally
            objCajas = Nothing
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            objOffline.EliminaPaginacion(Session.SessionID)
            objOffline = Nothing
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        objOffline.EliminaPaginacion(Session.SessionID)
        Response.Redirect("bsqDocumentos.aspx") 'INICIATIVA-565
    End Sub

    Private Sub dgDocumentosPago_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDocumentosPago.ItemDataBound
        '--
        If (e.Item.ItemType = ListItemType.Header) Then
            '--establece simbolo para la columna Monto a Pagar
            Dim lblControl As Label = CType(e.Item.FindControl("lblDOCSimboloTipoMoneda"), Label)
            If Not lblControl Is Nothing Then
                lblControl.Text = "S/"
                Dim sCodFactServicioDolares As String = ConfigurationSettings.AppSettings("constCodigoServFactDolares").ToString
                If hidCodigoServicio.Value = sCodFactServicioDolares Then
                    lblControl.Text = "$"
                End If
            End If
        ElseIf (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim chkControl As HtmlInputCheckBox = CType(e.Item.FindControl("chbxDOC_Seleccionar"), HtmlInputCheckBox)
            If Not chkControl Is Nothing Then
                chkControl.Attributes.Add("onclick", String.Format("javascript:activarInactivarIngresoMonto(this.id,'{0}_txtDOC_MontoPagar', '{0}_lblDOC_Deuda');", e.Item.ClientID))
            End If

            Dim txtControl As TextBox = CType(e.Item.FindControl("txtDOC_MontoPagar"), TextBox)
            If Not txtControl Is Nothing Then
                txtControl.Attributes.Add("onblur", String.Format("javascript:actualizaMontoPagoRecibo(this.id, '{0}_lblDOC_TipoMoneda', '{0}_lblDOC_Deuda', '{0}_chbxDOC_Seleccionar');", e.Item.ClientID))
            End If
        End If
    End Sub
#End Region

    Private Sub InicializaControles()
        '---
        txtRecibidoPen.Attributes.Add("onChange", "javascript:calculaVuelto();")
        txtRecibidoUsd.Attributes.Add("onChange", "javascript:calculaVuelto();")

        '--
        Dim strPaginaRetorno As String
        strPaginaRetorno = Mid(Request.ServerVariables("HTTP_REFERER"), InStrRev(Request.ServerVariables("HTTP_REFERER"), "/") + 1) '"recBusNomAp.aspx"
        Me.PAGINA_ANTERIOR = strPaginaRetorno

        If Not Request.QueryString("TI") Is Nothing Then
            Me.hidTipoIdentificador.Value = Request.QueryString("TI")
        End If

        If Not Request.QueryString("CS") Is Nothing Then
            Me.hidCodigoServicio.Value = Request.QueryString("CS") '//viene con prefijo 101,102, ETC
        End If

        If Not Request.QueryString("ID") Is Nothing Then
            Me.hidIdentificador.Value = Request.QueryString("ID") '//viene con prefijo 23
        End If
        hdnPuntoDeVenta.Value = Session("ALMACEN")
        hidCanal.Value = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")
        hdnUsuario.Value = Session("USUARIO")
        hdnBinAdquiriente.Value = Session("ALMACEN") '''cteCODIGO_BINADQUIRIENTE =ConfigurationSettings.AppSettings("cteCODIGO_BINADQUIRIENTE").ToString
        hdnCodComercio.Value = Session("ALMACEN")
        hdnRutaLog.Value = ConfigurationSettings.AppSettings("cteCODIGO_RUTALOG")
        hdnDetalleLog.Value = ConfigurationSettings.AppSettings("cteCODIGO_DETALLELOG")
        lblTC.Text = ObtenerTipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000")) '//ObtenerTipoCambio(Now.ToString("d"))
        txtRecibidoPen.Text = 0
        txtRecibidoUsd.Text = 0
        hidFPCodigoEfectivo.Value = ConfigurationSettings.AppSettings("constFPCodigoEfectivo")
        hidFPCodigoCheque.Value = ConfigurationSettings.AppSettings("constFPCodigoCheque")
        '--muestra u oculta panel par total de montos en Dolares, caso Servicio Dolares (103)
        Dim sSECodigoFactDolares As String = ConfigurationSettings.AppSettings("constCodigoServFactDolares")
        If (hidCodigoServicio.Value = sSECodigoFactDolares) Then
            pnlDOCTotalDolares.Attributes.Add("style", "display:block; text-align:right;")
        Else
            pnlDOCTotalDolares.Attributes.Add("style", "display:none")
        End If
    End Sub

    Private Sub LlenaCombos()
        '--carga combo de Vias o Formas de Pago
        'Dim objOffline As New COM_SIC_OffLine.clsOffline
        '''Dim intSAP = objOffline.Get_ConsultaSAP
        '''Dim objPagos As Object
        '''If intSAP = 1 Then
        '''    objPagos = New SAP_SIC_Pagos.clsPagos
        '''Else
        '''    objPagos = New COM_SIC_OffLine.clsOffline
        '''End If
        'Dim dsFormaPago As DataSet = objPagos.Get_ConsultaViasPago(Session("ALMACEN"))
        '''cboLoad(dsFormaPago, cboTipDocumento1)

        Dim clsConsulta As New clsConsultaGeneral
        '---carga combo de Medios o Vias de Pago  Obtener_ConsultaViasPago

        Dim dtaViasPago As DataTable = clsConsulta.FP_Get_ListaViasPago ''' NO SAP
        'Dim dtaViasPago As DataTable = objOffline.Obtener_ConsultaViasPago(Session("ALMACEN")).Tables(0) ''' NO SAP

        Dim strViasNoPermitas As String = ConfigurationSettings.AppSettings("constViasSapRestringidaFija")
        Dim dtViasFiltro As New DataTable("ViasPagoF")
        Dim dv As New DataView
        Dim iItems As Integer = 0
        Dim fila As DataRow
        dv.Table = dtaViasPago
        dtViasFiltro = dtaViasPago.Clone()
        dv.RowFilter = "CODIGO_SAP_SGA NOT IN (" & strViasNoPermitas & ")"
        iItems = dv.Count
        For x As Int32 = 0 To iItems - 1
            fila = dtViasFiltro.NewRow()
            fila.Item(0) = dv.Item(x).Item(0)
            fila.Item(1) = dv.Item(x).Item(1)
            dtViasFiltro.Rows.Add(fila)
        Next

        cboTipDocumento1.DataSource = dtViasFiltro '''FORMA DE PAGO
        cboTipDocumento1.DataValueField = "CODIGO_SAP_SGA"
        cboTipDocumento1.DataTextField = "DESCRIPCION"
        'cboTipDocumento1.DataValueField = "CCINS"
        'cboTipDocumento1.DataTextField = "VTEXT"
        cboTipDocumento1.DataBind()

        dtaViasPago = Nothing
        '--carga combo de Banco Girador
        Dim dtaBancos As DataTable = clsConsulta.FP_Get_ListaBancos() '''NO SAP
        Dim drwFila As DataRow
        drwFila = dtaBancos.NewRow '//crea fila seleccionar
        drwFila("CODIGO_BANCO") = String.Empty
        drwFila("DESCRIPCION") = "--SELECCIONAR--"
        dtaBancos.Rows.InsertAt(drwFila, 0)

        ddlBancoGirador01.DataSource = dtaBancos '''BANCO GIRADOR
        ddlBancoGirador01.DataValueField = "CODIGO_BANCO"
        ddlBancoGirador01.DataTextField = "DESCRIPCION"
        ddlBancoGirador01.DataBind()
        dtaBancos = Nothing
        'inicializa valor pro defecto de VIA DE PAGO POR DEFECTO
        cboTipDocumento1.SelectedValue = hidFPCodigoEfectivo.Value

        clsConsulta = Nothing

    End Sub

    Private Sub LeeDatosValidar()

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        '''CAMBIADO POR JTN

        'Dim intSAP = objOffline.Get_ConsultaSAP

        '***************LEE TARJETAS CREDITO
        'Dim objSap As New SAP_SIC_Pagos.clsPagos
        Dim objSap As New COM_SIC_OffLine.clsOffline
        'Dim objSap As Object
        'If intSAP = 1 Then
        '    objSap = New SAP_SIC_Pagos.clsPagos
        'Else
        '    objSap = New COM_SIC_OffLine.clsOffline
        'End If

        Dim dsTmp As DataSet = objSap.Obtener_Tarjeta_Credito()
        '''CAMBIADO HASTA AQUI

        Dim dr As DataRow
        txtTarjCred.Text = String.Empty
        For Each dr In dsTmp.Tables(0).Rows
            txtTarjCred.Text += CStr(dr(0)) + ";"
        Next

        '*************leee BIN
        Dim obCajas As New COM_SIC_Cajas.clsCajas
        dsTmp = obCajas.FP_ListaBIN() '''NO SAP

        txtBIN.Text = String.Empty
        For Each dr In dsTmp.Tables(0).Rows
            txtBIN.Text += CStr(dr(0)) + ";"
        Next

    End Sub

    Private Sub cboLoad(ByVal dsFormaPago As DataSet, ByRef cboCampo As DropDownList)
        cboCampo.DataSource = dsFormaPago.Tables(0)
        cboCampo.DataTextField = "VTEXT"
        cboCampo.DataValueField = "CCINS"
        cboCampo.DataBind()
    End Sub

    Private Function ObtenerTipoCambio(ByVal strFecha As String) As Decimal

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        'Dim intSAP = objOffline.Get_ConsultaSAP
        '''CAMBIADO POR JTN
        'Dim obPagos As Object
        'If intSAP = 1 Then
        '    obPagos = New SAP_SIC_Pagos.clsPagos
        'Else
        '    obPagos = New COM_SIC_OffLine.clsOffline
        'End If

        '''CAMBIADO HASTA AQUI
        Dim obPagos As New COM_SIC_OffLine.clsOffline
        'If intSAP = 1 Then
        Return Format(obPagos.Obtener_TipoCambio(strFecha), "#######0.000") 'aotane 05.08.2013
        'Else
        '    Return 0
        'End If
    End Function

    '''LLAMA AL ST
    Private Sub ObtenerDocumentosPorPagar()
        Dim sMensaje As String
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecFijoPaginas")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecFijoPaginas")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = hidIdentificador.Value
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ObtenerDocumentosPorPagar.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP ofVenta  " & hdnPuntoDeVenta.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP strTipoIdentificador:  " & hidTipoIdentificador.Value)
            '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP strIdentificador:  " & hidIdentificador.Value)

            Dim obRecaud As New COM_SIC_Recaudacion.clsConsultas
            Dim hayDocumentosPorConsultar As Boolean = False

            Dim posicionNuevaConsultaDocumento% = IIf(IsNothing(Me.Session("posicionNuevaConsulta")), 0, Me.Session("posicionNuevaConsulta"))
            'Me.Session("tamanioPaginacion") = IIf((Me.Session("tamanioPaginacion") = 0), Me.Session("posicionNuevaConsulta"), Me.Session("tamanioPaginacion"))
            Me.Session("posicionUltimaConsulta") = posicionNuevaConsultaDocumento
            anteriorButton.Enabled = Not ((Me.Session("posicionUltimaConsulta") = 0) Or posicionNuevaConsultaDocumento > Me.Session("posicionUltimaConsulta"))
            primeroButton.Enabled = anteriorButton.Enabled

            ''CONSULTA RCIBOS EN EL ST
            Dim strResult As String = obRecaud.ConsultarRecibos_23(hdnRutaLog.Value, hdnDetalleLog.Value, hdnPuntoDeVenta.Value, _
                                                            hidCanal.Value, hdnBinAdquiriente.Value, hdnCodComercio.Value, _
                                                            hdnUsuario.Value, hidTipoIdentificador.Value, hidIdentificador.Value, hidCodigoServicio.Value, hayDocumentosPorConsultar, posicionNuevaConsultaDocumento, numeroDdocumentosDevueltos)


            'Dim strResult As String = "0@PERU FASHIONS S.A.C.;               ;000062563141;59;2;0310174829;0000011282@101;RECIBO DEALER;604;101;05001165252;20021211;20021211;00000000100|101;RECIBO DEALER;604;101;05001165252;20021211;20021211;00000005800"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & obRecaud.sbLineasLog.ToString())
            obRecaud.sbLineasLog = Nothing '//reinicializa
            '---
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: ObtenerDocumentosPorPagar)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT strResult:  " & strResult & MaptPath)
            'FIN PROY-140126


            Dim arrMensaje() As String = strResult.Split("@")
            Dim arrCabecera() As String
            Dim arrDetalle() As String

            '*******Si hay error
            If ExisteError(arrMensaje) OrElse arrMensaje.Length < 3 Then  '
                sMensaje = arrMensaje(1).Trim()
                'INI PROY-140126
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: ObtenerDocumentosPorPagar)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT Error:  " & sMensaje & MaptPath)
                'FIN PROY-140126               
                Response.Write("<script> alert('" + sMensaje + "'); window.location='" & Me.PAGINA_ANTERIOR & "'; </script>")
            Else   '** si no hay error
                Dim dValor As Double
                arrCabecera = arrMensaje(1).Split(";")
                arrDetalle = arrMensaje(2).Split("|")
                '--
                Me.txtNombreCliente.Value = arrCabecera(cteNOMBRECLIENTE)
                dValor = Funciones.CheckDbl(arrCabecera(cteVALORDEUDA))
                Me.txtValorDeuda.Value = String.Format("{0:f2}", dValor) '//se asume viene siempre en soles
                '''Me.txtIdentificadorCliente.Value = arrCabecera(cteIDENTIFICADORCLIENTE)
                Me.txtIdentificadorCliente.Value = "99999999"
                Me.txtNumeroDocumentos.Value = arrCabecera(cteNUMERODOCUMENTOS)

                Me.Session("tamanioPaginacion") = IIf((Me.Session("tamanioPaginacion") = 0), arrCabecera(cteNUMERODOCUMENTOS), Me.Session("tamanioPaginacion"))

                Me.hidNumeroTrace.Value = arrCabecera(cteNUMEROTRACE)
                '--Convierte a  Soles para mostrar los montos en soles para el caso de servicio Fact. En Dolares
                Dim dValorDeuda As Double = Funciones.CheckDbl(arrCabecera(cteVALORDEUDA))
                Dim sCodFactServicioDolares As String = ConfigurationSettings.AppSettings("constCodigoServFactDolares")
                If (Me.hidCodigoServicio.Value = sCodFactServicioDolares) Then
                    Dim dValTipoCambio As Double = Funciones.CheckDbl(lblTC.Text)
                    If dValTipoCambio <= 0 Then
                        sMensaje = "El valor del tipo de cambio es inválido."
                        Throw New ArgumentOutOfRangeException("", "El valor del Tipo de Cambio es inválido.")
                    Else
                        dValorDeuda = Math.Round(dValTipoCambio * dValorDeuda, 2)
                    End If
                    lblTipoMonedaDeuda.Text = "$ "
                Else
                    lblTipoMonedaDeuda.Text = "S/ "
                End If
                '--forma de pago
                Me.txtMonto1.Text = Funciones.CheckStr(dValorDeuda)
                '---seccion monto de pago efectivo
                Me.txtRecibidoPen.Text = Me.txtMonto1.Text
                Me.txtRecibidoUsd.Text = String.Format("{0:f2}", 0)
                Me.txtVuelto.Text = String.Format("{0:f2}", 0)
                '--Seccion detalle - recibos
                Me.txtTotalRecibosSoles.Text = String.Format("{0:f2}", 0)
                Dim iNroDoc As Integer = CInt(Me.txtNumeroDocumentos.Value)
                If ((iNroDoc <= 0) Or (arrDetalle.Length <> iNroDoc)) Then
                    sMensaje = "Datos inválidos en la información de los Recibos. Por favor vuelva a intentar o comuníquese con el Help Desk Comercial."
                    Response.Write("<script> alert('" & sMensaje & "');  window.location='" & Me.PAGINA_ANTERIOR & "'; </script>")
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT strRecibos:  " & arrMensaje(2))
                    CargarRecibosRecaudados(arrDetalle)
                    '--
                    sMensaje = "OK"
                End If
                ''JYMMY INICIO
                Me.Session("hayMasDocumentos") = hayDocumentosPorConsultar
                Me.Session("posicionNuevaConsulta") = posicionNuevaConsultaDocumento
                Me.Session("cantidadDocumentosDevueltos") = numeroDdocumentosDevueltos ''TOTAL

                siguienteButton.Enabled = hayDocumentosPorConsultar
                ultimoButton.Enabled = hayDocumentosPorConsultar

                Dim arr As New ArrayList
                Dim tamanioPaginacion% = Convert.ToInt32(Me.Session("tamanioPaginacion"))
                Dim index%
                For index = 1 To numeroDdocumentosDevueltos
                    Dim modulo = index Mod tamanioPaginacion
                    If modulo = 0 Then arr.Add(index)
                Next

                ''14 / 7 RESTO 0 NO SE AUMENTA EN EL TOTAL DE PAGINAS

                'paginacionLabel.Text = String.Format("  Página {0} de {1}", IIf((arr.IndexOf(posicionNuevaConsultaDocumento) + 1) = 0, arr.Count + 1, arr.IndexOf(posicionNuevaConsultaDocumento) + 1), IIf((((numeroDdocumentosDevueltos Mod tamanioPaginacion%) Or numeroDdocumentosDevueltos = tamanioPaginacion) = 0), arr.Count, arr.Count + 1))
                Session("paginaActual") = IIf((arr.IndexOf(posicionNuevaConsultaDocumento) + 1) = 0, arr.Count + 1, arr.IndexOf(posicionNuevaConsultaDocumento) + 1)
                paginacionLabel.Text = String.Format("     Página {0} de {1}", IIf((arr.IndexOf(posicionNuevaConsultaDocumento) + 1) = 0, arr.Count + 1, arr.IndexOf(posicionNuevaConsultaDocumento) + 1), IIf(((numeroDdocumentosDevueltos Mod tamanioPaginacion%) = 0), arr.Count, arr.Count + 1))

                totalRecibos.Text = "Documentos - Total: " + numeroDdocumentosDevueltos.ToString
                ''JYMMY  FIN
            End If
        Catch ex As Exception
            sMensaje = "Hubo un error. " & ex.Message
            MensajeError("Consulta Clientes Fijo y Páginas", sMensaje)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ObtenerDocumentosPorPagar.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog = Nothing
            sMensaje = "Consulta Clientes Fijo y Páginas. " & sMensaje & ". Datos: Canal=" & hidCanal.Value & "|PDV=" & hdnPuntoDeVenta.Value & "|Cajero=" & _
                                    hdnUsuario.Value & "|Tipo Identificador=" & hidTipoIdentificador.Value & "|Identificador=" & hidIdentificador.Value

            'RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("codTrxFijaPagConsulta"))

            'Registrar_Paginacion()
            'ConsultaPaginacion()
        End Try
    End Sub

    Private Sub CargarRecibosRecaudados(ByVal pRecibos() As String)
        '--
        Dim cteRECIBO_CODIGOSERVICIO As Short = 0
        Dim cteRECIBO_DESCRIPCIONSERVICIO As Short = 1
        Dim cteRECIBO_MONEDASERVICIO As Short = 2
        Dim cteRECIBO_TIPODOCUMENTO As Short = 3
        Dim cteRECIBO_NUMERODOCUMENTO As Short = 4
        Dim cteRECIBO_FECHAEMISION As Short = 5
        Dim cteRECIBO_FECHAVENCIMIENTO As Short = 6
        Dim cteRECIBO_MONTODOCUMENTO As Short = 7

        Dim datRecibos As New DataTable '//tabla de recibos recaudados para cargar en grilla
        '---crea tabla
        datRecibos.Columns.Add(New DataColumn("NUMERO_CUENTA", GetType(String)))
        datRecibos.Columns.Add(New DataColumn("RAZON_SOCIAL", GetType(String)))
        datRecibos.Columns.Add(New DataColumn("TELEFONO_REFERENCIA", GetType(String)))
        datRecibos.Columns.Add(New DataColumn("NUMERO_RECIBO", GetType(String)))
        datRecibos.Columns.Add(New DataColumn("FECHA_EMISION", GetType(String)))  '//Date
        datRecibos.Columns.Add(New DataColumn("FECHA_VENCIMIENTO", GetType(String)))  '//Date
        datRecibos.Columns.Add(New DataColumn("DEUDA", GetType(String)))
        datRecibos.Columns.Add(New DataColumn("TIPO_MONEDA", GetType(String)))
        datRecibos.Columns.Add(New DataColumn("CODIGO_SERVICIO", GetType(String)))
        datRecibos.Columns.Add(New DataColumn("DESC_SERVICIO", GetType(String)))
        datRecibos.Columns.Add(New DataColumn("CODIGO_TIPODOC", GetType(String)))
        '//datRecibos.Columns.Add(New DataColumn("MONTO_PAGAR", GetType(String)))
        '---
        If Not pRecibos Is Nothing AndAlso pRecibos.Length > 0 Then
            '--ordena datos por Fecha Vencimiento
            Dim strTempo As String
            Dim strFila1(), strFila2() As String
            Dim i, j As Integer

            For i = 0 To pRecibos.Length - 1

                For j = i + 1 To pRecibos.Length - 1
                    strFila1 = pRecibos(i).Split(";")
                    strFila2 = pRecibos(j).Split(";")

                    If strFila1(cteRECIBO_FECHAVENCIMIENTO) > strFila2(cteRECIBO_FECHAVENCIMIENTO) Then
                        strTempo = pRecibos(i)
                        pRecibos(i) = pRecibos(j)
                        pRecibos(j) = strTempo
                    End If

                Next

            Next


            Dim NumeroRecibo, SesionID As String

            '--carga datos
            Dim drwFila As DataRow
            Dim arrRecibo() As String
            Dim sDeuda As String
            For i = 0 To pRecibos.Length - 1
                If Len(Trim(pRecibos(i))) > 0 Then
                    '---
                    arrRecibo = pRecibos(i).Split(";")
                    '---
                    drwFila = datRecibos.NewRow
                    drwFila("NUMERO_CUENTA") = String.Empty '//????provisional
                    drwFila("RAZON_SOCIAL") = txtNombreCliente.Value  '//????provisional
                    drwFila("TELEFONO_REFERENCIA") = String.Empty '//????provisional
                    drwFila("CODIGO_SERVICIO") = arrRecibo(cteRECIBO_CODIGOSERVICIO)
                    drwFila("DESC_SERVICIO") = arrRecibo(cteRECIBO_DESCRIPCIONSERVICIO)
                    drwFila("CODIGO_TIPODOC") = arrRecibo(cteRECIBO_TIPODOCUMENTO)
                    drwFila("NUMERO_RECIBO") = arrRecibo(cteRECIBO_NUMERODOCUMENTO)
                    drwFila("FECHA_EMISION") = FormateoFecha(arrRecibo(cteRECIBO_FECHAEMISION), 0)
                    drwFila("FECHA_VENCIMIENTO") = FormateoFecha(arrRecibo(cteRECIBO_FECHAVENCIMIENTO), 0)
                    drwFila("TIPO_MONEDA") = FormateoMoneda(arrRecibo(cteRECIBO_MONEDASERVICIO))
                    sDeuda = FormateoMonto(arrRecibo(cteRECIBO_MONTODOCUMENTO)).ToString("f2")
                    drwFila("DEUDA") = sDeuda

                    '//drwFila("MONTO_PAGAR") =
                    datRecibos.Rows.Add(drwFila) '//agrega fila
                End If
            Next
        End If
        '---
        dgDocumentosPago.DataSource = datRecibos
        dgDocumentosPago.DataBind()

        'AGREGADO POR FFS INICIO
        'If Me.Session("hayMasDocumentos") Then
        ConsultaPaginacion()
        'End If
        'AGREGADO POR FFS FIN
    End Sub


    Private Sub PagarDocumentos_23(ByVal strRutaLog, ByVal strDetalleLog, ByVal strPuntoDeVenta, ByVal strCanal, _
                                    ByVal strBinAdquiriente, ByVal strCodComercio, ByVal strCodigoCajero, ByVal strTipoIdentificadorDeudor, _
                                    ByVal strNumeroIdentificadorDeudor, ByVal pCodigoEstadoPago, ByVal strFormasPago, _
                                    ByVal dblMontoTotalPagar, ByVal strRecibosPagar, ByVal strNumeroTrace)
        '---
        Dim sMensaje As String
        Dim sPaginaDestino As String
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecFijoPaginas")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecFijoPaginas")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = strNumeroIdentificadorDeudor
        '---
        Try
            Dim obPagos As New COM_SIC_Recaudacion.clsPagos
            Dim objCajas As New COM_SIC_Cajas.clsCajas
            Dim decImpPen, decImpUsd, decVuelto As Double
            '----
            decImpPen = Funciones.CheckDbl(txtRecibidoPen.Text)
            decImpUsd = Funciones.CheckDbl(txtRecibidoUsd.Text)
            decVuelto = Funciones.CheckDbl(txtVuelto.Text)
            Dim dValorTipoCambioDolares As Double = Funciones.CheckDbl(lblTC.Text)
            '----
            objFileLog.Log_WriteLog(pathFile, strArchivo, "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio PagarDocumentos_23")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "----------------------------------------------------------------")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP strFormasPago: " & strFormasPago)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP strRecibosPagar: " & strRecibosPagar)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP pCodigoEstadoPago:  " & pCodigoEstadoPago)
            '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP dblMontoTotalPagar:  " & dblMontoTotalPagar)
            '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP decImpPen:  " & decImpPen)
            '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP decImpUsd:  " & decImpUsd)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP decVuelto:  " & decVuelto)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP dValorTipoCambioDolares:  " & dValorTipoCambioDolares)
            '--
            Dim strRespuesta As String
            strRespuesta = obPagos.PagarRecibos_23( _
                                                    strRutaLog, strDetalleLog, strPuntoDeVenta, Session("OFICINA"), strCanal, strBinAdquiriente, _
                                                    strCodComercio, strCodigoCajero, Session("NOMBRE_COMPLETO"), strTipoIdentificadorDeudor, strNumeroIdentificadorDeudor, pCodigoEstadoPago, _
                                                    strFormasPago, dblMontoTotalPagar, strRecibosPagar, strNumeroTrace, Me.txtIdentificadorCliente.Value, _
                                                    strCanal, ConfigurationSettings.AppSettings("codAplicacion"), decImpPen, decImpUsd, decVuelto, _
                                                    dValorTipoCambioDolares)
            '---logs de recibos procesados
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & obPagos.sbLineasLog.ToString())
            obPagos.sbLineasLog = Nothing '//reinicializa
            '---
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT strRespuesta:  " & strRespuesta)
            Dim arrMensaje() As String = strRespuesta.Split("@")
            '''arrMensaje--> tramaOperacion
            If ExisteError(arrMensaje) Then
                If arrMensaje.Length > 1 Then
                    If InStr(1, arrMensaje(1), ";") > 0 Then '''PRIMER ELEMENTO ES UNA TRAMA DE ERROR???
                        Dim arrMensajeError() As String
                        arrMensajeError = arrMensaje(1).Split(";")
                        If arrMensajeError.Length >= 5 Then
                            Session("strMENSREC") = arrMensajeError(4)
                            sMensaje = arrMensajeError(4)
                        Else
                            Session("strMENSREC") = arrMensajeError(1)
                            sMensaje = arrMensajeError(1)
                        End If
                    Else
                        Session("strMENSREC") = arrMensaje(1)
                        sMensaje = arrMensaje(1)
                    End If
                Else
                    sMensaje = "Verifique los datos ingresados. Por favor vuelva a intentar."
                    Session("strMENSREC") = sMensaje
                End If
                '--inicializa datos para auditoria
                sMensaje = "Hubo un error. " & sMensaje
                '---
                sPaginaDestino = Me.PAGINA_ANTERIOR
            Else '//no existe error
                '---
                If cboTipDocumento1.SelectedValue = hidFPCodigoEfectivo.Value Then
                    objCajas.FP_InsertaEfectivo(hdnPuntoDeVenta.Value, hdnUsuario.Value, CDbl(txtTotalRecibosSoles.Text.Trim)) '' SD_632877
                    ''objCajas.FP_InsertaEfectivo(hdnPuntoDeVenta.Value, hdnUsuario.Value, CDbl(txtMonto1.Text.Trim)) ''' NO SAP
                End If
                '--inicializa datos para auditoria
                sMensaje = "OK"
                '---obtiene datos para ir a página de consulta a SAP.
                arrMensaje = strRespuesta.Split("@")
                Dim arrCabecera() As String = arrMensaje(1).Split(";")
                Dim strAccion As String = CStr(cteACCION_CONFIRMACION)
                Dim strNumeroDeuda As String = arrCabecera(cteNUMERODEUDA)
                '--se envia Tipo de cambio de pago para poder anular desde la siguiente pagina (caso Servicio 103 en Dòlares)
                'PROY-27440 INI
                Dim strNumeroRec As String = Funciones.CheckStr(arrCabecera(0))

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " === INICIO Actualizar cabecera === ")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	strNumeroDeuda: " & strNumeroRec)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "    HidIdCabez: " & Me.HidIdCabez.Value)

                If (strNumeroRec <> "" And Funciones.CheckStr(Me.HidIdCabez.Value) <> "") Then
                    Me.actualizar_codigo_recuadacion(strNumeroDeuda, Me.HidIdCabez.Value)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	Número Rec: " & strNumeroRec)
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "    strNumeroDeuda: " & strNumeroRec)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "    strAccion: " & Funciones.CheckStr(strAccion))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " === FIN Actualizar cabecera === ")
                'PROY-27440 FIN
                sPaginaDestino = "conDocumentos.aspx?act=" & strAccion & "&num=" & strNumeroDeuda & "&tc=" & lblTC.Text & "&es=" & pCodigoEstadoPago
                Me.Session("PAGINA_INICIAL") = Me.PAGINA_ANTERIOR    '//para ir a la pàgina de inicio al terminar en la siguiente pagina
            End If
            '---
        Catch ex As Exception
            sMensaje = "Hubo un error. " & ex.Message
            MensajeError("Recaudación Clientes Fijo y Páginas", sMensaje)
            sPaginaDestino = Me.PAGINA_ANTERIOR
        Finally
            sMensaje = "Pagos Cliente Fijo y Páginas. " & sMensaje & ". Datos: Canal=" & strCanal & "|PDV=" & strPuntoDeVenta & "|Cajero=" & strCodigoCajero & _
                                "|Tipo Identificador=" & strTipoIdentificadorDeudor & "|Identificador=" & strNumeroIdentificadorDeudor
            '---
            objFileLog.Log_WriteLog(pathFile, strArchivo, "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin PagarDocumentos_23.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "----------------------------------------------------------------")
            objFileLog = Nothing
            '---
            RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("codTrxFijaPagGrabarPago"))
            Response.Redirect(sPaginaDestino)
        End Try
    End Sub
    'PROY-27440 INI
    Private Sub actualizar_codigo_recuadacion(ByVal strIdRecaudacion As String, ByVal strCodCabez As String)

        Dim strCodRpt As String = ""
        Dim strMsgRpt As String = ""
        Dim strPedidoLog As String = "strIdRecaudacion: [" & Funciones.CheckStr(strIdRecaudacion) & "] "
        Dim strTipoPago As String

        Try
            strTipoPago = Me.HidTipoPago.Value
            strPedidoLog = Devolver_TipoPago_POS(strTipoPago) & ": [" & Funciones.CheckStr(strCodCabez) & "] "
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "Inicio")

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "=== INICIO actualizar_codigo_recuadacion()  ===")

            Dim objEntity As New COM_SIC_Activaciones.BeEnvioTransacPOS
            objEntity.TransId = strCodCabez
            objEntity.FlagPago = "2"
            objEntity.idCabecera = strCodCabez
            objEntity.numPedido = strIdRecaudacion
            objEntity.IdRefAnu = ""

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "  strCodCabez : " & strCodCabez)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "  strIdRecaudacion : " & strIdRecaudacion)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "  FlagPago : " & objEntity.FlagPago)

            objEntity.estadoTransaccion = ""
            objEntity.IdTransPos = ""
            objEntity.IdTransPos = ""
            objEntity.monedaOperacion = ""
            objEntity.montoOperacion = ""
            objEntity.nroRegistro = ""
            objEntity.numVoucher = ""
            objEntity.numTransaccion = ""
            objEntity.codRespTransaccion = ""
            objEntity.descTransaccion = ""
            objEntity.codAprobTransaccion = ""
            objEntity.tipoPos = ""
            objEntity.nroTarjeta = ""
            objEntity.fechaTransaccionPos = ""
            objEntity.horaTransaccionPos = ""
            objEntity.fecExpiracion = ""
            objEntity.nombreCliente = ""
            objEntity.impresionVoucher = ""
            objEntity.numSeriePos = ""
            objEntity.nombreEquipoPos = ""
            objEntity.ipCliente = ""
            objEntity.ipServidor = ""
            objEntity.nombrePcCliente = ""
            objEntity.nombrePcServidor = ""
            objEntity.usuarioRed = ""
            objEntity.UserAplicacion = ""
            objEntity.codCajero = ""
            objEntity.codAnulador = ""

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "=== INI ActualizarTransaction() : ===")
            Dim objSicarDB As New COM_SIC_Activaciones.BWEnvioTransacPOS
            objSicarDB.ActualizarTransaction(objEntity, strCodRpt, strMsgRpt)

            If strCodRpt <> "0" Then

                Dim objTransascPos As New COM_SIC_Activaciones.clsTransaccionPOS

                objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "ActualizarTransaction - Error en el Servicio " & "strCodRpt : " & strCodRpt)
                objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "ActualizarTransaction - Error en el Servicio " & "strMsgRpt : " & strMsgRpt)
                objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - Inicio")

                objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - SP: PKG_SISCAJ_POS.SICASU_TRANSPOS")
                objTransascPos.UpdateCabTransaccPOS(objEntity, strCodRpt, strMsgRpt)
                objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - strCodRpt:" & strCodRpt)
                objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - strMsgRpt:" & strMsgRpt)
                objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - Fin")

            End If

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "  strMsgRpt: " & strMsgRpt)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "  strCodRpt: " & strCodRpt)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "=== FIN ActualizarTransaction() : ===")
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "=== FIN actualizar_codigo_recuadacion()  ===")
        Catch ex As Exception
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "  Error: " & ex.Message)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "=== FIN actualizar_codigo_recuadacion()  ===")
        End Try
    End Sub
    'PROY-27440 FIN

    Private Sub RegistrarAuditoria(ByVal DesTrx As String, ByVal CodTrx As String)
        Try
            Dim user As String = Me.CurrentUser
            Dim ipHost As String = CurrentTerminal
            Dim nameHost As String = System.Net.Dns.GetHostName
            Dim nameServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nameServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nameHost)

            Dim CadMensaje As String
            Dim CodServicio As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            Dim oAuditoria As New COM_SIC_Activaciones.clsAuditoriaWS

            oAuditoria.RegistrarAuditoria(CodTrx, _
                                            CodServicio, _
                                            ipHost, _
                                            nameHost, _
                                            ipServer, _
                                            nameServer, _
                                            user, _
                                            "", _
                                            "0", _
                                            DesTrx)

        Catch ex As Exception
            ' Throw New Exception("Error Registrar Auditoria.")
        End Try

    End Sub

    Private Function ObtenerTramaFormaPagoSAP() As String

        Dim sTextoFP As String
        Dim sCodigoSAP_SGA As String = String.Empty
        Dim sCodigoBco01 As String = String.Empty

        Dim sFormaPago As String = String.Empty
        Dim sCodigoMedioPago As String = String.Empty

        'If IsNumeric(Me.txtMonto1.Text) AndAlso Decimal.Parse(Me.txtMonto1.Text.Trim()) > 0 Then'QUITADO POR JYMMYT

        sCodigoSAP_SGA = Me.cboTipDocumento1.SelectedValue.Trim()
        If (sCodigoSAP_SGA = hidFPCodigoCheque.Value) Then
            sCodigoBco01 = ddlBancoGirador01.SelectedValue
        End If

        '--determina codigo medio de pago a enviar a través de La trama|  ES UN NUMERO
        '''DONDE SE ENVIA LA DESCRIPCION
        Dim objConsulta As New clsConsultaGeneral
        sCodigoMedioPago = objConsulta.FP_GetCodigoTramaViaPago(sCodigoSAP_SGA)
        objConsulta = Nothing
        If sCodigoMedioPago = String.Empty Then
            Throw New Exception("No existe correspondencia de Forma de Pago a enviar.")
        End If
        'JYMMY INICIO
        ''' COMENTADO ALINEACION 03.01.2014
        sFormaPago = sCodigoSAP_SGA + sCodigoMedioPago + ";" + Me.txtTotalRecibosSoles.Text.Trim() + ";" + Me.txtDoc1.Text.Trim() + ";" + sCodigoBco01 + ";" + cboTipDocumento1.SelectedItem.Text
        '''
        'sFormaPago = sCodigoSAP_SGA + sCodigoMedioPago + ";" + Me.txtTotalRecibosSoles.Text.Trim() + ";" + Me.txtDoc1.Text.Trim() + ";" + sCodigoBco01 + ";"
        ';;ZEFE;4331.45;;;
        'End If'''JYMMY FIN
        '---
        Return sFormaPago
    End Function

    Private Function ObtenerMontoTotalPagar() As Decimal
        Dim decResult As Decimal = 0
        If IsNumeric(Me.txtMonto1.Text) Then
            decResult += Decimal.Parse(Me.txtTotalRecibosSoles.Text.Trim()) 'ALINEACION SD_632877
            ''decResult += Decimal.Parse(Me.txtMonto1.Text.Trim()) '
        End If
        '--
        Return decResult
    End Function

    Private Function ObtenerTramaRecibosST() As String
        '---
        Dim chkControl As HtmlInputCheckBox
        Dim hidControl As HtmlInputHidden
        Dim lblControl As Label
        Dim txtControl As TextBox
        Dim sTipoMoneda As String
        Dim dFecha As Date
        Dim sTramaRecibos As String = String.Empty
        For Each dgiFila As DataGridItem In dgDocumentosPago.Items
            '--recupera datos
            chkControl = CType(dgiFila.FindControl("chbxDOC_Seleccionar"), HtmlInputCheckBox)
            If ((Not chkControl Is Nothing) AndAlso (chkControl.Checked)) Then
                '--posicion 0
                hidControl = CType(dgiFila.FindControl("hidDOC_CodServicio"), HtmlInputHidden)
                sTramaRecibos = sTramaRecibos & ObtenerValorHiddenGrid(hidControl)   '//Ejm: 101
                '--posicion 1    
                hidControl = CType(dgiFila.FindControl("hidDOC_DescServicio"), HtmlInputHidden)
                sTramaRecibos = sTramaRecibos & ";" & ObtenerValorHiddenGrid(hidControl)
                '--posicion 2
                lblControl = CType(dgiFila.FindControl("lblDOC_TipoMoneda"), Label)
                sTipoMoneda = ObtenerValorLabelGrid(lblControl)
                '----
                If (sTipoMoneda = cteVALMONEDA_SOLES) Then
                    sTipoMoneda = cteCODMONEDA_SOLES
                ElseIf (sTipoMoneda = cteVALMONEDA_DOLARES) Then
                    sTipoMoneda = cteCODMONEDA_DOLARES
                Else
                    sTipoMoneda = String.Empty
                End If

                sTramaRecibos = sTramaRecibos & ";" & sTipoMoneda
                '--posicion 3
                hidControl = CType(dgiFila.FindControl("hidDOC_CodTipoDoc"), HtmlInputHidden)
                sTramaRecibos = sTramaRecibos & ";" & ObtenerValorHiddenGrid(hidControl)
                '--posicion 4
                lblControl = CType(dgiFila.FindControl("lblDOC_Recibo"), Label)   '//nro recibo
                sTramaRecibos = sTramaRecibos & ";" & ObtenerValorLabelGrid(lblControl)

                '--posicion 5
                Dim sFecha As String
                lblControl = CType(dgiFila.FindControl("lblDOC_FechaEmision"), Label)
                sFecha = ObtenerValorLabelGrid(lblControl)
                If sFecha <> String.Empty Then
                    sFecha = sFecha.Substring(6, 4) & sFecha.Substring(3, 2) & sFecha.Substring(0, 2)
                End If
                sTramaRecibos = sTramaRecibos & ";" & sFecha

                '--posicion 6
                lblControl = CType(dgiFila.FindControl("lblDOC_FechaVencimiento"), Label)
                sFecha = ObtenerValorLabelGrid(lblControl)
                If sFecha <> String.Empty Then
                    sFecha = sFecha.Substring(6, 4) & sFecha.Substring(3, 2) & sFecha.Substring(0, 2)
                End If
                sTramaRecibos = sTramaRecibos & ";" & sFecha

                '--posicion 7                
                Dim dMonto As Double
                Dim sMonto As String
                lblControl = CType(dgiFila.FindControl("lblDOC_Deuda"), Label)
                dMonto = Funciones.CheckDbl(lblControl.Text)
                sMonto = String.Format("{0:D11}", CLng(dMonto * 100))
                sTramaRecibos = sTramaRecibos & ";" & sMonto

                '--Posicion 8
                txtControl = CType(dgiFila.FindControl("txtDOC_MontoPagar"), TextBox)
                dMonto = Funciones.CheckDbl(txtControl.Text)
                If dMonto <= 0 Then
                    Throw New ArgumentOutOfRangeException("", "El Monto a Pagar debe ser mayor a Cero.")
                End If
                sMonto = String.Format("{0:D11}", CLng(dMonto * 100))
                sTramaRecibos = sTramaRecibos & ";" & sMonto & "|"
            End If
        Next
        '--quita ultimo caracter de separacion de filas |
        If sTramaRecibos.Length > 0 Then
            sTramaRecibos = sTramaRecibos.Substring(0, sTramaRecibos.Length - 1)
        End If
        '--retorna trama
        Return sTramaRecibos
    End Function

    Private Function ObtenerTramaRecibosST(ByVal numeroRecibo As String) As String
        '---
        Dim chkControl As HtmlInputCheckBox
        Dim hidControl As HtmlInputHidden
        Dim lblControl As Label
        Dim lblRecibo As Label
        Dim txtControl As TextBox
        Dim sTipoMoneda As String
        Dim dFecha As Date
        Dim sTramaRecibos As String = String.Empty
        For Each dgiFila As DataGridItem In dgDocumentosPago.Items

            lblRecibo = CType(dgiFila.FindControl("lblDOC_Recibo"), Label)
            If numeroRecibo = ObtenerValorLabelGrid(lblRecibo) Then
                '--recupera datos
                chkControl = CType(dgiFila.FindControl("chbxDOC_Seleccionar"), HtmlInputCheckBox)
                If ((Not chkControl Is Nothing) AndAlso (chkControl.Checked)) Then
                    '--posicion 0
                    hidControl = CType(dgiFila.FindControl("hidDOC_CodServicio"), HtmlInputHidden)
                    sTramaRecibos = sTramaRecibos & ObtenerValorHiddenGrid(hidControl)   '//Ejm: 101
                    '--posicion 1    
                    hidControl = CType(dgiFila.FindControl("hidDOC_DescServicio"), HtmlInputHidden)
                    sTramaRecibos = sTramaRecibos & ";" & ObtenerValorHiddenGrid(hidControl)
                    '--posicion 2
                    lblControl = CType(dgiFila.FindControl("lblDOC_TipoMoneda"), Label)
                    sTipoMoneda = ObtenerValorLabelGrid(lblControl)
                    '----
                    If (sTipoMoneda = cteVALMONEDA_SOLES) Then
                        sTipoMoneda = cteCODMONEDA_SOLES
                    ElseIf (sTipoMoneda = cteVALMONEDA_DOLARES) Then
                        sTipoMoneda = cteCODMONEDA_DOLARES
                    Else
                        sTipoMoneda = String.Empty
                    End If

                    sTramaRecibos = sTramaRecibos & ";" & sTipoMoneda
                    '--posicion 3
                    hidControl = CType(dgiFila.FindControl("hidDOC_CodTipoDoc"), HtmlInputHidden)
                    sTramaRecibos = sTramaRecibos & ";" & ObtenerValorHiddenGrid(hidControl)
                    '--posicion 4
                    lblControl = CType(dgiFila.FindControl("lblDOC_Recibo"), Label)   '//nro recibo
                    sTramaRecibos = sTramaRecibos & ";" & ObtenerValorLabelGrid(lblControl)

                    '--posicion 5
                    Dim sFecha As String
                    lblControl = CType(dgiFila.FindControl("lblDOC_FechaEmision"), Label)
                    sFecha = ObtenerValorLabelGrid(lblControl)
                    If sFecha <> String.Empty Then
                        sFecha = sFecha.Substring(6, 4) & sFecha.Substring(3, 2) & sFecha.Substring(0, 2)
                    End If
                    sTramaRecibos = sTramaRecibos & ";" & sFecha

                    '--posicion 6
                    lblControl = CType(dgiFila.FindControl("lblDOC_FechaVencimiento"), Label)
                    sFecha = ObtenerValorLabelGrid(lblControl)
                    If sFecha <> String.Empty Then
                        sFecha = sFecha.Substring(6, 4) & sFecha.Substring(3, 2) & sFecha.Substring(0, 2)
                    End If
                    sTramaRecibos = sTramaRecibos & ";" & sFecha

                    '--posicion 7                
                    Dim dMonto As Double
                    Dim sMonto As String
                    lblControl = CType(dgiFila.FindControl("lblDOC_Deuda"), Label)
                    dMonto = Funciones.CheckDbl(lblControl.Text)
                    sMonto = String.Format("{0:D11}", CLng(dMonto * 100))
                    sTramaRecibos = sTramaRecibos & ";" & sMonto

                    '--Posicion 8
                    txtControl = CType(dgiFila.FindControl("txtDOC_MontoPagar"), TextBox)
                    dMonto = Funciones.CheckDbl(txtControl.Text)
                    If dMonto <= 0 Then
                        Throw New ArgumentOutOfRangeException("", "El Monto a Pagar debe ser mayor a Cero.")
                    End If
                    sMonto = String.Format("{0:D11}", CLng(dMonto * 100))
                    sTramaRecibos = sTramaRecibos & ";" & sMonto '& "|"
                    Exit For
                End If
            End If
        Next
        Return sTramaRecibos
    End Function

    Protected Function DeterminaEstadoPago(ByVal pCodigoServicio As String) As String
        '---determina codigo de estado del pago
        Dim sCodigoServPaginasClaro As String = ConfigurationSettings.AppSettings("constCodigoServPaginasClaro").ToString
        Dim sCodEstadoTransaccion As String = ConfigurationSettings.AppSettings("constEstadoPagoFijo").ToString
        Dim sEstadoCanceladoTelmexDolares As String = ConfigurationSettings.AppSettings("constCodigoServFactDolares").ToString

        If pCodigoServicio = sCodigoServPaginasClaro Then
            sCodEstadoTransaccion = ConfigurationSettings.AppSettings("constEstadoPagoPaginas").ToString
        End If

        If pCodigoServicio = sEstadoCanceladoTelmexDolares Then
            sCodEstadoTransaccion = ConfigurationSettings.AppSettings("constEstadoPagoFijoDolares").ToString
        End If

        Return sCodEstadoTransaccion
    End Function

    Private Function ObtenerValorLabelGrid(ByVal pLabel As Label)
        Dim sValor As String
        If (pLabel Is Nothing) Then
            sValor = String.Empty
        Else
            sValor = pLabel.Text.Trim
        End If
        '---
        Return sValor
    End Function

    Private Function ObtenerValorTextBoxGrid(ByVal pTxtControl As TextBox)
        Dim sValor As String
        If (pTxtControl Is Nothing) Then
            sValor = String.Empty
        Else
            sValor = pTxtControl.Text
        End If
        '---
        Return sValor
    End Function

    Private Function ObtenerValorHiddenGrid(ByVal pHidControl As HtmlInputHidden)
        Dim sValor As String
        If (pHidControl Is Nothing) Then
            sValor = String.Empty
        Else
            sValor = pHidControl.Value
        End If
        '---
        Return sValor
    End Function

    Private Function ExisteError(ByVal arrMensaje() As String) As Boolean
        Dim bresult As Boolean = True

        If arrMensaje(0).Trim().Length > 0 Then
            If IsNumeric(arrMensaje(0).Trim()) Then
                If Integer.Parse(arrMensaje(0).Trim()) = 0 Then
                    bresult = False
                End If
            End If
        End If

        Return bresult
    End Function

    Public Function FormateoFecha(ByVal strFecha As String, ByVal intTipo As Integer) As String
        Dim strNuevaFecha As String
        Select Case intTipo
            Case 0
                If Len(Trim(strFecha)) > 0 Then
                    strNuevaFecha = Mid(strFecha, 7, 2) & "/" & Mid(strFecha, 5, 2) & "/" & Mid(strFecha, 1, 4)
                End If
            Case Else
                strNuevaFecha = strFecha
        End Select

        Return strNuevaFecha
    End Function

    Public Function FormateoMoneda(ByVal strCodigoMoneda As String)
        Dim strNuevaMoneda As String
        Select Case strCodigoMoneda
            Case cteCODMONEDA_SOLES
                strNuevaMoneda = cteVALMONEDA_SOLES
            Case cteCODMONEDA_DOLARES
                strNuevaMoneda = cteVALMONEDA_DOLARES
            Case Else
                strNuevaMoneda = String.Empty
        End Select
        FormateoMoneda = strNuevaMoneda
    End Function

    Public Function FormateoMonto(ByVal pMonto As String) As Double
        Dim sSeparador As String = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator()
        pMonto = pMonto.Insert(9, sSeparador)
        Dim dMonto As Double = Funciones.CheckDbl(pMonto)
        Return (dMonto)
    End Function

    Public Sub MensajeError(ByVal pTitulo As String, ByVal pMensaje As String)
        If Not Me.IsStartupScriptRegistered(pTitulo) Then
            Me.RegisterStartupScript(pTitulo, "<script> alert('" + pMensaje + "'); </script>")
        End If
    End Sub

    Private Sub siguienteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles siguienteButton.Click
        If Me.Session("hayMasDocumentos") Then
            Registrar_Paginacion()
            Me.ObtenerDocumentosPorPagar()
        End If
    End Sub

    Private Sub anteriorButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles anteriorButton.Click
        Registrar_Paginacion()
        If Me.Session("posicionNuevaConsulta") > 0 Then
            Me.Session("posicionNuevaConsulta") = Me.Session("posicionUltimaConsulta") - Convert.ToInt32(Me.Session("tamanioPaginacion"))
            Me.ObtenerDocumentosPorPagar()
        Else
            anteriorButton.Enabled = False
        End If
        ConsultaPaginacion()
    End Sub

    Private Sub primeroButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles primeroButton.Click
        Registrar_Paginacion()
        Me.Session("posicionNuevaConsulta") = 0
        Me.ObtenerDocumentosPorPagar()
        anteriorButton.Enabled = False
        primeroButton.Enabled = False
        ConsultaPaginacion()
    End Sub

    Private Sub ultimoButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ultimoButton.Click
        Dim tamanioPaginacion% = Convert.ToInt32(txtNumeroDocumentos.Value)
        Registrar_Paginacion()
        ''SI ES MULTIPLO DEL TAMAÑO DE PAGINACION 
        ''IIf(((numeroDdocumentosDevueltos Mod 7) = 0), arr.Count, arr.Count + 1)
        Dim numeroDdocumentosDevueltos% = Me.Session("cantidadDocumentosDevueltos")
        Me.Session("posicionNuevaConsulta") = IIf(((numeroDdocumentosDevueltos Mod tamanioPaginacion) = 0), numeroDdocumentosDevueltos - tamanioPaginacion, (numeroDdocumentosDevueltos \ tamanioPaginacion) * tamanioPaginacion)
        Me.ObtenerDocumentosPorPagar()
        ultimoButton.Enabled = False
        siguienteButton.Enabled = False
        ConsultaPaginacion()
    End Sub

    Sub Registrar_Paginacion()
        Dim objclsOffline As New COM_SIC_OffLine.clsOffline
        Dim NumeroRecibo, SessionID, Monto As String
        Dim Seleccion As String
        Dim ChkSeleccion As CheckBox
        Dim lblDOC_RazonSocial As New System.Web.UI.WebControls.Label
        Dim dt As DataTable
        Dim NroPagina As String = Session("paginaActual")
        Dim tramaRecibo As String = ""
        SessionID = Session.SessionID
        'inicio jtn///
        Dim pagina$ = paginacionLabel.Text.Trim.Split(CChar(" "))(1)
        Dim ultimoRecibo$
        If (hidUltimoRecibo.Value <> "") Then
            ultimoRecibo = String.Format("{1}|{0}", hidUltimoRecibo.Value, pagina)
        End If
        'fin jtn///
        For Each dgiFila As DataGridItem In dgDocumentosPago.Items
            Dim chkControl As HtmlInputCheckBox = CType(dgiFila.FindControl("chbxDOC_Seleccionar"), HtmlInputCheckBox)
            Dim montoOriginalrecibo As String
            Dim precioRecibo# = 0
            NumeroRecibo = CType(dgiFila.FindControl("lblDOC_Recibo"), Label).Text
            montoOriginalrecibo = Convert.ToDouble(CType(dgiFila.FindControl("lblDOC_Deuda"), Label).Text) * 100
            If (CType(dgiFila.FindControl("txtDOC_MontoPagar"), TextBox).Text <> "") Then
                precioRecibo = Convert.ToDouble(CType(dgiFila.FindControl("txtDOC_MontoPagar"), TextBox).Text) * 100
            End If
            'Monto = CType(dgiFila.FindControl("txtDOC_MontoPagar"), TextBox).Text' jtn
            Monto = precioRecibo.ToString()
            If chkControl.Checked = True Then
                Seleccion = "1"
                tramaRecibo = Me.ObtenerTramaRecibosST(NumeroRecibo)
            Else
                Seleccion = "0"
            End If
            objclsOffline.Set_Registra_Paginacion(NumeroRecibo, Seleccion, SessionID, Monto, NroPagina, tramaRecibo, montoOriginalrecibo, ultimoRecibo)
        Next
    End Sub

    Private Sub ConsultaPaginacion()
        'AGREGADO POR FFS INICIO
        If (Not IsPostBack) Then Return
        Dim objclsOffline As New COM_SIC_OffLine.clsOffline
        Dim NumeroRecibo, SessionID As String
        SessionID = Session.SessionID
        hiddTotal.Value = 0
        Dim montoOriginal# = 0
        Dim montoPaginacionSR# = 0

        'Session("SessionID") = Session.SessionID
        Dim Seleccion, Monto As String
        Dim total As Decimal = 0
        Dim Numero_Pagina As Integer = Me.Session("paginaActual")
        Dim totalObtenido As Boolean = False
        Dim codigoServicio As Integer = Convert.ToInt32(hidCodigoServicio.Value)
        Dim currentPage$ = paginacionLabel.Text.Trim.Split(CChar(" "))(1)
        Try
            dsPaginacion = objclsOffline.GetConsultaPaginacion(SessionID, Numero_Pagina)
            dtPaginacion = dsPaginacion.Tables(0)
            For Each row As DataRow In dtPaginacion.Rows
                montoOriginal += row("Monto_Original")
            Next
            ' montoPaginacionSR = Convert.ToDouble(dsPaginacion.Tables(1).Rows(0).Item("SUMA"))
            'lblDOC_Deuda
            viewstate("dtPaginacion") = dtPaginacion
            Dim sumaPorPagina# = 0
            If (dtPaginacion.Rows.Count > 0) Then
                For Each dgiFila As DataGridItem In dgDocumentosPago.Items
                    Dim chkControl As HtmlInputCheckBox = CType(dgiFila.FindControl("chbxDOC_Seleccionar"), HtmlInputCheckBox)
                    NumeroRecibo = CType(dgiFila.FindControl("lblDOC_Recibo"), Label).Text
                    'objclsOffline.GetConsultaPaginacion(NumeroRecibo, SessionID, Seleccion, Monto)
                    If fSeleccionado(NumeroRecibo, SessionID, Monto) Then
                        chkControl.Checked = True
                        CType(dgiFila.FindControl("txtDOC_MontoPagar"), TextBox).CssClass = "clsInputEnable"
                        CType(dgiFila.FindControl("txtDOC_MontoPagar"), TextBox).ReadOnly = False
                        CType(dgiFila.FindControl("txtDOC_MontoPagar"), TextBox).Text = FormatNumber(Monto.Trim, 2)
                        sumaPorPagina += Monto
                        montoPaginacionSR += Convert.ToDouble(CType(dgiFila.FindControl("lblDOC_Deuda"), Label).Text)
                        Dim txtControl As TextBox = CType(dgiFila.FindControl("txtDOC_MontoPagar"), TextBox)
                    End If
                Next
            End If
            'hidPagina.Value = Convert.ToString(dsPaginacion.Tables(1).Rows(0).Item("SUMA")) 'sumaPorPagina
            hidPagina.Value = sumaPorPagina
            hiddTotalOriginal.Value = Math.Round(montoOriginal, 2)
            hiddPaginaSinAjuste.Value = montoPaginacionSR

            If Not totalObtenido Then
                'Dim totalMontosMarcados$ = objclsOffline.GetConsultaPaginacion(SessionID, Numero_Pagina).Tables(1).Rows(0).Item("SUMA")
                Dim totalMontosMarcados$ = Convert.ToString(dsPaginacion.Tables(1).Rows(0).Item("SUMA"))    'objclsOffline.GetSumaMontosPaginacion(SessionID)

                'inicio jtn
                If codigoServicio = cteRECAUDACIONDOLARES Then
                    Dim tipoCambio As Double = Convert.ToDouble(lblTC.Text)
                    hiddTotal.Value = IIf(totalMontosMarcados = "", 0, totalMontosMarcados)
                    txtTotalRecibosDolares.Text = String.Format("{0:F}", hiddTotal.Value)
                    Dim totalCambio# = Math.Round(txtTotalRecibosDolares.Text * tipoCambio, 2)

                    txtTotalRecibosSoles.Text = String.Format("{0:F}", totalCambio)
                    txtMonto1.Text = txtTotalRecibosSoles.Text
                    txtRecibidoPen.Text = txtTotalRecibosSoles.Text
                Else
                    txtTotalRecibosSoles.Text = IIf(totalMontosMarcados = "", 0, totalMontosMarcados)
                    hiddTotal.Value = IIf(totalMontosMarcados = "", 0, totalMontosMarcados)
                    txtRecibidoPen.Text = hiddTotal.Value
                    txtMonto1.Text = txtRecibidoPen.Text

                    txtTotalRecibosSoles.Text = redondeoSicar(txtTotalRecibosSoles.Text)

                End If
                'fin jtn
                totalObtenido = True
            End If
            'AGREGADO POR FFS FIN
        Catch ex As Exception
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            objOffline.EliminaPaginacion(Session.SessionID)
            hidPagina.Value = 0
            hiddTotal.Value = 0
            Dim sMensaje$ = Left(ex.Message, ex.Message.Length - 1)
            Dim strPaginaRetorno$ = Mid(Request.ServerVariables("HTTP_REFERER"), InStrRev(Request.ServerVariables("HTTP_REFERER"), "/") + 1)
            Dim sriptError$ = String.Format("<script type='text/javascript'> alert('{0}'); window.location='{1}'; </script>", sMensaje, strPaginaRetorno)
            Response.Write(sriptError)
        End Try
    End Sub

    Function fSeleccionado(ByVal NumeroRecibo, ByVal SessionID, ByRef Monto) As Boolean
        Dim dtPag As New DataTable
        dtPag = CType(viewstate("dtPaginacion"), DataTable)
        If dtPag.Select("NUMERO_RECIBO='" & NumeroRecibo & "' AND  SESSION_ID='" & SessionID & "'").Length > 0 Then
            Monto = dtPaginacion.Select("NUMERO_RECIBO='" & NumeroRecibo & "' AND  SESSION_ID='" & SessionID & "'")(0).Item("MONTO")
            Return True
        Else
            Monto = 0
            Return False
        End If
    End Function

    'Function fTotalOtrasPaginas(ByVal NroPaginaActual As Integer) As Boolean
    '    Dim dtPag As New DataTable
    '    dtPag = CType(viewstate("dtPaginacion"), DataTable)
    '    If dtPag.Select("NOT NUMERO_PAGINA=" & NroPaginaActual).Length > 0 Then
    '        Monto = dtPaginacion.Select("NUMERO_RECIBO='" & NumeroRecibo & "' AND  SESSION_ID='" & SessionID & "'")(0).Item("MONTO")
    '        Return True
    '    Else
    '        Monto = 0
    '        Return False
    '    End If
    'End Function


    Function redondeoSicar(ByVal numeroRedondear As String) As String
        Dim separadorDecimal As String = "."
        Dim indiceSeparador As Integer = numeroRedondear.IndexOf(separadorDecimal)
        If (indiceSeparador = -1) Then Return numeroRedondear
        Dim parteDecimal As String = numeroRedondear.Split(separadorDecimal)(1)

        Dim nuevoNumero As String = numeroRedondear.Substring(0, indiceSeparador + 2)
        Dim nuevoDigito As String = String.Empty
        If (parteDecimal.Length = 2) Then
            Dim ultimoDigito As Integer = Convert.ToInt32(parteDecimal.Substring(1, 1))
            Select Case ultimoDigito
                Case 0 To 9
                    nuevoDigito = "0"
            End Select
        End If
        nuevoNumero += nuevoDigito
        Return nuevoNumero
    End Function

End Class



