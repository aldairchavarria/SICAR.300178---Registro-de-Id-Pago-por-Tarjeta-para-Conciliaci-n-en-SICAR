Imports System.Globalization
Imports COM_SIC_Activaciones 'INICIATIVA-318
Public Class resDocSinDeuda
    Inherits SICAR_WebBase 'PROY-27440 INI

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents hdnMensaje As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnPuntoDeVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnBinAdquiriente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodComercio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents intCanal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtNumeroTrace As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnRutaLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDetalleLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtIdentificador As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtTipoIdentificador As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblTC As System.Web.UI.WebControls.Label
    Protected WithEvents txtIdentificadorCliente As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNombreCliente As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cboTipDocumento1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboTipDocumento2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboTipDocumento3 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDoc1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDoc2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDoc3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecibidoPen As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtVuelto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecibidoUsd As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents txtTarjCred As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBIN As System.Web.UI.WebControls.TextBox
    Protected WithEvents strRecibos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtTipoCli As System.Web.UI.HtmlControls.HtmlInputText

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
    Protected WithEvents HidTipPOS4 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipPOS5 As System.Web.UI.HtmlControls.HtmlInputHidden
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
    Protected WithEvents hidF2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidF3 As System.Web.UI.HtmlControls.HtmlInputHidden
    'PROY-27440 FIN
    Protected WithEvents HidNumIntentosPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidNumIntentosAnular As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjErrorNumIntentos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjErrorTimeOut As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjNumIntentosPago As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected WithEvents HidFlagCajaCerrada As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjCajaCerrada As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMedioPagoPermitidas As System.Web.UI.HtmlControls.HtmlInputHidden
    'INICIATIVA-318 INI
    Protected WithEvents hddComboAutorizador As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hddEnvioAutorizador As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hddcboFormaPago As System.Web.UI.HtmlControls.HtmlInputHidden
    'INICIATIVA-318 FIN
#End Region
    'PROY-27440 INI
    Public objFileLogPos As New SICAR_Log
    Dim nameFilePos As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivoPos As String = objFileLogPos.Log_CrearNombreArchivo(nameFilePos)
    'PROY-27440 FIN

    'PROY-27440 INI
    Dim arrParametrosFormaPagoPerfil As ArrayList   'INICIATIVA-318
    Dim strIdentifyLogGeneral As String = "" ' INICIATIVA-318
    Dim strCodPerfilFormaPago As String = ""  ' INICIATIVA-318
    
    Private Sub load_data_param_pos()
        Dim strPedidoLog As String = "Identificador Recibos: [" & Funciones.CheckStr(Request.QueryString("pIdent")) & "] "
        Me.HidTipoPago.Value = ClsKeyPOS.strTipPagoPAP 'PAGO ADELANTADO POSTPAGO
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

         'PROY-31949 - Inicio
        HidNumIntentosPago.Value = ClsKeyPOS.strNumIntentosPago
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
                    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " - " & "MENSAJE : " & ClsKeyPOS.strMsjCajaCerrada)
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

        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : Validacion Integracion FIN")
        'RECAUDACION=>strCodOpeVC
        Me.HidCodOpera.Value = ClsKeyPOS.strCodOpeVE & "|" & _
        ClsKeyPOS.strCodOpeREC & "|" & ClsKeyPOS.strCodOpeAN

        Me.HidDesOpera.Value = ClsKeyPOS.strDesOpeVE & "|" & _
        ClsKeyPOS.strDesOpeREC & "|" & ClsKeyPOS.strDesOpeAN
        Me.HidTipoOpera.Value = ClsKeyPOS.strOpeFina & "|" & ClsKeyPOS.strNoFina 'OPE FI(90)
        Me.HidTipoTarjeta.Value = ClsKeyPOS.strCodTarjetaVS & "|" & ClsKeyPOS.strCodTarjetaMC

        Dim strTipoPago As String = Me.HidTipoPago.Value
        strPedidoLog = Devolver_TipoPago_POS(strTipoPago) & " Identificador : [" & Funciones.CheckStr(Request.QueryString("pIdent")) & "] "
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidIntAutPos : " & HidIntAutPos.Value)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidGrabAuto : " & HidGrabAuto.Value)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidCodOpera : " & HidCodOpera.Value)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidTipoOpera : " & HidCodOpera.Value)

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

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIp : " & strIp)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIpClient : " & strIpClient)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidPtoVenta : " & HidPtoVenta.Value)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strEstadoPos : " & strEstadoPos)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strTipoVisa : " & strTipoVisa)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos Visa : " & "Count : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

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
                & "|POSV_FLAGPOS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSC_FLG_SICAR"))

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

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIp : " & strIp)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidPtoVenta : " & HidPtoVenta.Value)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strEstadoPos : " & strEstadoPos)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strTipoVisa : " & strTipoVisa)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos MC : " & "Count : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

            If ds.Tables(0).Rows.Count > 0 Then
                Me.HidDatoPosMC.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
                & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
                & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
                & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
                & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
                & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
                & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
                & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA")) _
                & "|POSV_FLAGPOS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSC_FLG_SICAR"))

                objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidDatoPosMC : " & HidDatoPosMC.Value)
            Else
                If bvalida = 0 Then
                    Response.Write("<script>alert('" & strMensajeMC & "');</script>")
                End If
            End If
            'MC FIN
            'MEDIOS DE PAGO PERMITIDO CON EL POS - INICIO'
            Dim dtViaPagoPOS As DataTable
            Dim strMsgRpta As String
            Dim strCodRpta As String
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
            'MEDIOS DE PAGO PERMITIDO CON EL POS - FIN'
        End If
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "Fin : ")

    End Sub
     Private Sub validar_pedido_pos()
        Dim strPedidoLog As String = "Pedido: [" & Funciones.CheckStr(Request.QueryString("pIdent")) & "] "

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Inicio : ")

        Dim dsPedidoPOS As DataSet
        Dim objPedidoPOS As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim strNroPedido As String = Funciones.CheckStr(Request.QueryString("pIdent"))
        Dim strNroRef As String = ""
        Dim strCodRpta As String = ""
        Dim strMsgRpta As String = ""
        Dim strTipoPago As String = Funciones.CheckStr(Me.HidTipoPago.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "strTipoPago : " & strTipoPago)


        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Inicio ConsultarPedidoPOS ")
        dsPedidoPOS = objPedidoPOS.ConsultarPedidoPOS(strNroPedido, strNroRef, strCodRpta, strMsgRpta)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "strCodRpta : " & strCodRpta)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "strMsgRpta : " & strMsgRpta)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Fin ConsultarPedidoPOS ")
        Dim intIndexCombo As Int32 = 0
        Dim strTipoTarjeta As String = ""
        Dim strTipoTarjetaSap As String = ""

        If strCodRpta = "0" AndAlso Not dsPedidoPOS Is Nothing AndAlso dsPedidoPOS.Tables.Count > 0 AndAlso dsPedidoPOS.Tables(0).Rows.Count > 0 Then

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Count : " & Funciones.CheckStr(dsPedidoPOS.Tables(0).Rows.Count))


            For i As Int32 = 0 To dsPedidoPOS.Tables(0).Rows.Count - 1
                Dim objTxt As HtmlInputHidden
                objTxt = CType(Me.FindControl("HidFila" & i + 1), HtmlInputHidden)

                Dim objCombo As DropDownList
                objCombo = CType(Me.FindControl("cboTipDocumento" & i + 1), DropDownList)

                strTipoTarjeta = ""
                strTipoTarjeta = Funciones.CheckStr(dsPedidoPOS.Tables(0).Rows(i)("TRNSV_TIPO_TARJETA_POS"))

                Dim objTipPos As HtmlInputHidden
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

                intIndexCombo = objCombo.Items.IndexOf(objCombo.Items.FindByValue(strTipoTarjetaSap))

                objTxt.Value = "Monto=" & Funciones.CheckStr(dsPedidoPOS.Tables(0).Rows(i)("TRNSN_MONTO")) & _
                "|Tarjeta=" & Funciones.CheckStr(dsPedidoPOS.Tables(0).Rows(i)("TRNSV_NRO_TARJETA")) & _
                "|ComboIndex=" & intIndexCombo & _
                "|NroReferncia=" & Funciones.CheckStr(dsPedidoPOS.Tables(0).Rows(i)("TRNSV_ID_REF"))

                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "objTxt.Value : " & objTxt.Value)


                Me.HidIdCabez.Value = Funciones.CheckStr(dsPedidoPOS.Tables(0).Rows(i)("TRNSN_ID_CAB"))
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "HidIdCabez : " & HidIdCabez.Value)
            Next
        End If

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Fin : ")

    End Sub
    'PROY-27440 FIN

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

'#End Region 'PROY-27440

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
    Const cteACCION_CONFIRMACION = 1

	Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

    Public Recibos() As String
'PROY-27440 - INI
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

        If Me.HidFila2.Value.Trim() <> "" Then
            strArrys = Me.HidFila2.Value.Split("|")
            'MONTO
            strMonto = "" : strMonto = strArrys(0).Substring(strArrys(0).IndexOf("=") + 1)
            Me.txtMonto2.Text = strMonto
            'NRO TARJETA
            strTarjeta = "" : strTarjeta = strArrys(1).Substring(strArrys(1).IndexOf("=") + 1)
            Me.txtDoc2.Text = strTarjeta
            'TIPO DE TARJETA 
            Me.cboTipDocumento2.SelectedIndex = Funciones.CheckInt(strArrys(2).Substring(strArrys(2).IndexOf("=") + 1))
        End If

        If Me.HidFila3.Value.Trim() <> "" Then
            strArrys = Me.HidFila3.Value.Split("|")
            'MONTO
            strMonto = "" : strMonto = strArrys(0).Substring(strArrys(0).IndexOf("=") + 1)
            Me.txtMonto3.Text = strMonto
            'NRO TARJETA
            strTarjeta = "" : strTarjeta = strArrys(1).Substring(strArrys(1).IndexOf("=") + 1)
            Me.txtDoc3.Text = strTarjeta
            'TIPO DE TARJETA 
            Me.cboTipDocumento3.SelectedIndex = Funciones.CheckInt(strArrys(2).Substring(strArrys(2).IndexOf("=") + 1))
        End If

        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_values_pos : " & "HidFila1 : " & HidFila1.Value)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_values_pos : " & "HidFila2 : " & HidFila2.Value)
        objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_values_pos : " & "HidFila3 : " & HidFila3.Value)


    End Sub
'PROY-27440 - FIN

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Dim objRecBusiness As New COM_SIC_RecBusiness.clsRecBusiness
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
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

            If (strRecibos.Value.Trim().Length > 0) Then
                Recibos = strRecibos.Value.Split(",")
            End If
            'INICIATIVA-318 INI
            If (hddComboAutorizador.Value = "cboTipDocumento1") Then
                If (hddEnvioAutorizador.Value = "ERROR") Then
                    hddComboAutorizador.Value = ""
                    hddEnvioAutorizador.Value = ""
                    cboTipDocumento1.SelectedIndex = 0
                Else
                    hddComboAutorizador.Value = ""
                    hddEnvioAutorizador.Value = ""
                    cboTipDocumento1.SelectedValue = hddcboFormaPago.Value
                End If
            End If

            If (hddComboAutorizador.Value = "cboTipDocumento2") Then
                If (hddEnvioAutorizador.Value = "ERROR") Then
                    hddComboAutorizador.Value = ""
                    hddEnvioAutorizador.Value = ""
                    cboTipDocumento2.SelectedIndex = 0
                Else
                    hddComboAutorizador.Value = ""
                    hddEnvioAutorizador.Value = ""
                    cboTipDocumento2.SelectedValue = hddcboFormaPago.Value
                End If
            End If
            If (hddComboAutorizador.Value = "cboTipDocumento3") Then
                If (hddEnvioAutorizador.Value = "ERROR") Then
                    hddComboAutorizador.Value = ""
                    hddEnvioAutorizador.Value = ""
                    cboTipDocumento3.SelectedIndex = 0
                Else
                    hddComboAutorizador.Value = ""
                    hddEnvioAutorizador.Value = ""
                    cboTipDocumento3.SelectedValue = hddcboFormaPago.Value
                End If
            End If
            'INICIATIVA-318 FIN

            ConsultaParametrosFormaPagoPerfil(strIdentifyLogGeneral) ' INICIATIVA-318

            Try
                If Not Page.IsPostBack Then
                    Me.load_data_param_pos()  'PROY-27440 
                    If Not Request.QueryString("pTipoIdent") Is Nothing Then
                        Me.txtTipoIdentificador.Value = Request.QueryString("pTipoIdent")
                    End If
                    If Not Request.QueryString("pIdent") Is Nothing Then
                        Me.txtIdentificador.Value = Request.QueryString("pIdent")
                    End If
                    LlenaCombos()
                    LeeDatosValidar()
                    LeeParametros()
                    ObtenerDocumentosPorPagar(Me.hdnRutaLog.Value, _
                                                            Me.hdnDetalleLog.Value, _
                                                            Me.hdnPuntoDeVenta.Value, _
                                                            Me.intCanal.Value, _
                                                            Me.hdnBinAdquiriente.Value, _
                                                            Me.hdnCodComercio.Value, _
                                                            Me.hdnUsuario.Value, _
                                                            Me.txtTipoIdentificador.Value, _
                                                            Me.txtIdentificador.Value)

                    'CARIAS: modificacion para que traiga el tipo de cliente (16-08-2006)
                    'If txtTipoIdentificador.Value = "01" Then
                    '    txtTipoCli.Value = objRecBusiness.FP_TipoCliente(txtIdentificador.Value)
                    'End If
                    'CARIAS: fin de modificacion
                     Me.validar_pedido_pos() 'PROY-31949
                Else
                    'PROY-27440 INI
                    load_values_pos()
                    'PROY-27440 FIN
                End If
            Catch ex As Exception
                Response.Write("<script language=jscript> alert('" + ex.Message + "'); window.location='bsqDocSinDeuda.aspx';</script>")
            End Try
        End If
    End Sub

    Private Sub LeeParametros()

        Dim cteCODIGO_CANAL As String = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")
        Dim cteCODIGO_BINADQUIRIENTE As String = ConfigurationSettings.AppSettings("cteCODIGO_BINADQUIRIENTE")
        Dim cteCODIGO_RUTALOG As String = ConfigurationSettings.AppSettings("cteCODIGO_RUTALOG")
        Dim cteCODIGO_DETALLELOG As String = ConfigurationSettings.AppSettings("cteCODIGO_DETALLELOG")

        Me.hdnPuntoDeVenta.Value = Session("ALMACEN")
        Me.intCanal.Value = cteCODIGO_CANAL
        Me.hdnUsuario.Value = Session("USUARIO")
        Me.hdnBinAdquiriente.Value = Session("ALMACEN") 'cteCODIGO_BINADQUIRIENTE
        Me.hdnCodComercio.Value = Session("ALMACEN")
        Me.hdnRutaLog.Value = cteCODIGO_RUTALOG
        Me.hdnDetalleLog.Value = cteCODIGO_DETALLELOG
        'Me.lblTC.Text = ObtenerTipoCambio(Now.ToString("d"))
        Me.lblTC.Text = ObtenerTipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"))

    End Sub


    Private Function ObtenerTipoCambio(ByVal strFecha As String) As String

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        '' TODO CAMBIADO POR JYMMY TORRES
        'Dim intSAP = objOffline.Get_ConsultaSAP

        ''Dim obPagos As New SAP_SIC_Pagos.clsPagos
        'Dim obPagos As Object
        'If intSAP = 1 Then
        '    obPagos = New SAP_SIC_Pagos.clsPagos
        'Else
        '    obPagos = New COM_SIC_OffLine.clsOffline
        'End If

        ''Return obPagos.Get_TipoCambio(strFecha).ToString("N2")
        'If intSAP = 1 Then
        '    Return Format(obPagos.Get_TipoCambio(strFecha), "#######0.00")
        'Else
        '    Return 0
        'End If
        ''CAMBIADO HASTA AQUI
        Return Format(objOffline.Obtener_TipoCambio(strFecha), "#######0.000") 'aotane 05.08.2013
    End Function

    Private Sub ObtenerDocumentosPorPagar( _
                           ByVal strRutaLog As String, _
                           ByVal strDetalleLog As String, _
                           ByVal strPuntoDeVenta As String, _
                           ByVal intCanal As String, _
                           ByVal strBinAdquiriente As String, _
                           ByVal strCodComercio As String, _
                           ByVal strUsuario As String, _
                           ByVal strTipoIdentificador As String, _
                           ByVal strIdentificador As String)

        'Dim obRecaud As New COM_SIC_Recaudacion.clsConsultas

        'Response.Write(strRutaLog + "<br>")
        'Response.Write(strDetalleLog + "<br>")
        'Response.Write(strPuntoDeVenta + "<br>")
        'Response.Write(intCanal + "<br>")
        'Response.Write(strBinAdquiriente + "<br>")
        'Response.Write(strCodComercio + "<br>")
        'Response.Write(strUsuario + "<br>")
        'Response.Write(strTipoIdentificador + "<br>")
        'Response.Write(strIdentificador + "<br>")
        'Response.End()

        'Dim strResult As String = obRecaud.ConsultarRecibos(strRutaLog, _
        '                                                   strDetalleLog, _
        '                                                   strPuntoDeVenta, _
        '                                                   intCanal, _
        '                                                   strBinAdquiriente, _
        '                                                   strCodComercio, _
        '                                                   strUsuario, _
        '                                                   strTipoIdentificador, _
        '                                                   strIdentificador)


objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio Obtener Documentos por Pagar")
        Dim objRecBusiness As New COM_SIC_RecBusiness.clsRecBusiness
        Dim strResultado As String
        Try
objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio UltimoReciboDatos: " & "- " & strIdentificador)
            strResultado = objRecBusiness.FP_UltimoReciboDatos(strIdentificador)
        Catch ex As Exception
objFileLog.Log_WriteLog(pathFile, strArchivo, " UltimoReciboDatos - ERROR: " & ex.Message.ToString)
            strResultado = "0"
        End Try
objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin UltimoReciboDatos")
        'Dim arrMensaje() As String = strResult.Split("@")
        Dim arrCabecera() As String
        'Dim arrDetalle() As String
        'Response.Write("<script> alert('" + strResult + "');</script>")
        'Response.Write("respuesta: " & strResult) : Response.End()
        '*******Si hay error
objFileLog.Log_WriteLog(pathFile, strArchivo, "resultado: " & strResultado)
        If Len(Trim(strResultado)) = 0 Or strResultado = "0" Then
            Response.Write("<script> alert('No se pudo obtener información del número telefónico'); window.location='bsqDocSinDeuda.aspx'; </script>")
        Else '** si no hay error
            arrCabecera = Split(strResultado, ";") 'arrMensaje(1).Split(";")
            'arrDetalle = arrMensaje(2).Split("|")
objFileLog.Log_WriteLog(pathFile, strArchivo, "Cabecera: " & strResultado)
            Me.txtNombreCliente.Value = arrCabecera(2) 'arrCabecera(cteNOMBRECLIENTE)
            'Me.txtValorDeuda.Value = "" 'arrCabecera(cteVALORDEUDA)
            'Me.txtMonto1.Text = "" 'arrCabecera(cteVALORDEUDA)
            'Me.txtRecibidoPen.Text = "" 'arrCabecera(cteVALORDEUDA)
            'Me.txtRecibidoUsd.Text = "0.00"
            'Me.txtVuelto.Text = "0.00"
            Me.txtIdentificadorCliente.Value = arrCabecera(1) 'arrCabecera(cteIDENTIFICADORCLIENTE)
            'Me.txtNumeroDocumentos.Value = "" 'arrCabecera(cteNUMERODOCUMENTOS)
            Me.txtNumeroTrace.Value = "" 'arrCabecera(cteNUMEROTRACE)

            If Trim(Me.txtNombreCliente.Value) = "-1" Then
                Throw New Exception("No se encontraron datos de cliente")
            End If

            'If (arrDetalle.Length) <> CInt(Me.txtNumeroDocumentos.Value) Then
            '    Response.Write("<script> alert('Datos Invalidos en la información de los Recibos. Por favor vuelva a intentar o comuniquese con el Help Desk Comercial.');  window.location='bsqDocumentos.aspx'; </script>")
            'End If
            'Recibos = "REC"
            strRecibos.Value = "REC;Recibo Postpago;604;REC;" & arrCabecera(0) 'Join(Recibos, ",")
objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Obtener Documentos por Pagar")
        End If
    End Sub

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
                '12345678
                '20051103
                If Len(Trim(strFecha)) > 0 Then
                    strNuevaFecha = Mid(strFecha, 7, 2) & "/" & Mid(strFecha, 5, 2) & "/" & Mid(strFecha, 1, 4)
                End If
            Case Else
                strNuevaFecha = strFecha
        End Select

        Return strNuevaFecha
    End Function

    Public Function FormateoMoneda(ByVal strFecha As String, ByVal strMoneda As String)
        Dim strNuevaMoneda
        Select Case strMoneda
            Case cteCODMONEDA_SOLES
                strNuevaMoneda = cteVALMONEDA_SOLES
            Case cteCODMONEDA_DOLARES
                strNuevaMoneda = cteVALMONEDA_DOLARES
            Case Else
                strNuevaMoneda = strFecha
        End Select
        FormateoMoneda = strNuevaMoneda
    End Function


    Public Function FormateoMonto(ByVal strMonto As String) As Double
        Dim strNuevoMonto As String
        strNuevoMonto = Mid(strMonto, 1, 9) & "." & Mid(strMonto, 10, 2)
        Return CDbl(strNuevoMonto)
    End Function

    ''**********

    Private Sub LlenaCombos()
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim dtVias As DataTable
        'Dim intSAP = objOffline.Get_ConsultaSAP

        ''TODO CAMBIADO POR JYMMY TORRES
        'Dim objPagos As New SAP_SIC_Pagos.clsPagos
        'Dim objPagos As Object
        'If intSAP = 1 Then
        '    objPagos = New SAP_SIC_Pagos.clsPagos
        'Else
        '    objPagos = New COM_SIC_OffLine.clsOffline
        'End If
        ''CAMBIADO HASTA AQUI


        Dim dsFormaPago As DataSet = objOffline.Obtener_ConsultaViasPago(Session("ALMACEN"))

        dtVias = New DataTable
        dtVias = VerificarVias(dsFormaPago)

        cboLoad(dtVias, cboTipDocumento1)
        cboLoad(dtVias, cboTipDocumento2)
        cboLoad(dtVias, cboTipDocumento3)

        cboTipDocumento1.Items.Insert(0, "") 'INICIATIVA-318
        cboTipDocumento2.Items.Insert(0, "")
        cboTipDocumento3.Items.Insert(0, "")
        'INICIATIVA-318 INI
        Try
        'VIA DE PAGO POR DEFECTO
        cboTipDocumento1.SelectedValue = "ZEFE"

            Dim bolAutorizado As Boolean
            Dim strFormaPago As String
            Dim strCombo As String
            hddcboFormaPago.Value = cboTipDocumento1.SelectedValue
            strFormaPago = cboTipDocumento1.SelectedValue
            strCombo = "cboTipDocumento1"
            bolAutorizado = ObtenerAprobacionFormaPago(cboTipDocumento1.SelectedValue)

            If (bolAutorizado = False) Then
                cboTipDocumento1.SelectedIndex = 0
                Response.Write("<script language=javascript>alert('Usuario no cuenta con los permisos sufiientes para la forma de Pago');</script>")
                Response.Write("<script language=javascript>window.open('../Pagos/frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")
            End If

        Catch ex As Exception
            cboTipDocumento1.SelectedIndex = 0
        End Try
        'INICIATIVA-318 FIN

        cboTipDocumento2.SelectedValue = ""
        cboTipDocumento3.SelectedValue = ""


    End Sub


    Private Sub cboLoad(ByVal dsFormaPago As DataTable, ByRef cboCampo As DropDownList)
        cboCampo.DataSource = dsFormaPago
        cboCampo.DataTextField = "VTEXT"
        cboCampo.DataValueField = "CCINS"
        cboCampo.DataBind()

    End Sub

    Private Sub LeeDatosValidar()

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        'Dim intSAP = objOffline.Get_ConsultaSAP

        '***************LEE TARJETAS CREDITO
        'Dim objSap As New SAP_SIC_Pagos.clsPagos
        'Dim objSap As Object
        'If intSAP = 1 Then
        '    objSap = New SAP_SIC_Pagos.clsPagos
        'Else
        '    objSap = New COM_SIC_OffLine.clsOffline
        'End If


        Dim dsTmp As DataSet = objOffline.Get_Tarjeta_Credito()
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



    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("bsqDocumentos.aspx")
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim MontoTotalAPagar, TramaFormasPagoSAP

        Try

            ''' VERIFICACION DE CUADRE DE CAJA 05.02.2014 TS-JTN
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
            If (objOffline.CuadreCajeroRealizado(Session("ALMACEN"), codUsuario)) Then
                Throw New Exception("No puede realizar esta operacion, ya realizo cuadre de caja")
            End If
            ''' FIN VERIFICACION

            ObtenerDocumentosPorPagar(Me.hdnRutaLog.Value, _
                                                    Me.hdnDetalleLog.Value, _
                                                    Me.hdnPuntoDeVenta.Value, _
                                                    Me.intCanal.Value, _
                                                    Me.hdnBinAdquiriente.Value, _
                                                    Me.hdnCodComercio.Value, _
                                                    Me.hdnUsuario.Value, _
                                                    Me.txtTipoIdentificador.Value, _
                                                Me.txtIdentificador.Value)

            MontoTotalAPagar = Me.ObtenerMontoTotalPagar()
            TramaFormasPagoSAP = Me.ObtenerTramaFormasDePagoSAP()

            'Se hace esta parte porque cuando se trae del lado cliente, se trae la trama separada por coma ",".
            Dim TramaRecibosST As String = Me.ObtenerTramaRecibosST(strRecibos.Value)
            'Me.Recibos = TramaRecibosST.Split("|")

            ' se añade este bloque para no impedir que se procese el pago por la ausencia de DNI o RUC
            If Len(Trim(txtIdentificadorCliente.Value)) = 0 Or Trim(txtIdentificadorCliente.Value) = "-1" Then
                txtIdentificadorCliente.Value = "99999999"
            End If
            'fin de bloque

            'If Len(Trim(txtIdentificadorCliente.Value)) = 0 Or Len(Trim(txtNombreCliente.Value)) = 0 Then
            If Len(Trim(txtNombreCliente.Value)) = 0 Or Trim(txtNombreCliente.Value) = "-1" Then
                'Throw New Exception("El Nombre o el RUC no pueden estar vacios")
                Throw New Exception("El Nombre no puede estar vacio")
            End If


            Me.PagarDocumentos( _
                                Me.hdnRutaLog.Value, _
                                Me.hdnDetalleLog.Value, _
                                Me.hdnPuntoDeVenta.Value, _
                                Me.intCanal.Value, _
                                Me.hdnBinAdquiriente.Value, _
                                Me.hdnCodComercio.Value, _
                                Me.hdnUsuario.Value, _
                                Me.txtTipoIdentificador.Value, _
                                Me.txtIdentificador.Value, _
                                TramaFormasPagoSAP, _
                                MontoTotalAPagar, _
                                TramaRecibosST, _
                                txtNumeroTrace.Value)

            'Threading.Thread.Sleep(20000) 'forzando demora de 20 segundos.
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); window.location='bsqDocSinDeuda.aspx';</script>")
        End Try
    End Sub


    Private Function ObtenerTramaRecibosST(ByVal strRecibos As String) As String

        Return strRecibos.Replace(",", "|")

    End Function


    Private Function ObtenerTramaFormasDePagoSAP() As String
        Dim strResp As String = ""

        If IsNumeric(Me.txtMonto1.Text) AndAlso Decimal.Parse(Me.txtMonto1.Text) > 0 Then
            strResp = Me.cboTipDocumento1.SelectedValue.Trim() + ";" + Me.txtMonto1.Text.Trim() + ";" + Me.txtDoc1.Text.Trim() + ";;" + Me.cboTipDocumento1.SelectedItem.Text.Trim
        End If
        If IsNumeric(Me.txtMonto2.Text) AndAlso Decimal.Parse(Me.txtMonto2.Text) > 0 Then
            strResp += "|" + Me.cboTipDocumento2.SelectedValue.Trim() + ";" + Me.txtMonto2.Text.Trim() + ";" + Me.txtDoc2.Text.Trim() + ";;" + Me.cboTipDocumento2.SelectedItem.Text.Trim
        End If
        If IsNumeric(Me.txtMonto3.Text) AndAlso Decimal.Parse(Me.txtMonto3.Text) > 0 Then
            strResp += "|" + Me.cboTipDocumento3.SelectedValue.Trim() + ";" + Me.txtMonto3.Text.Trim() + ";" + Me.txtDoc3.Text.Trim() + ";;" + Me.cboTipDocumento3.SelectedItem.Text.Trim
        End If
        Return strResp
    End Function

    Private Function ObtenerMontoTotalPagar() As Decimal
        Dim decResult As Decimal = 0

        If IsNumeric(Me.txtMonto1.Text) Then
            decResult += Decimal.Parse(Me.txtMonto1.Text)
        End If
        If IsNumeric(Me.txtMonto2.Text) Then
            decResult += Decimal.Parse(Me.txtMonto2.Text)
        End If
        If IsNumeric(Me.txtMonto3.Text) Then
            decResult += Decimal.Parse(Me.txtMonto3.Text)
        End If

        Return decResult
    End Function


    Private Sub PagarDocumentos( _
                            ByVal strRutaLog, _
                            ByVal strDetalleLog, _
                            ByVal strPuntoDeVenta, _
                            ByVal strCanal, _
                            ByVal strBinAdquiriente, _
                            ByVal strCodComercio, _
                            ByVal strCodigoCajero, _
                            ByVal strTipoIdentificadorDeudor, _
                            ByVal strNumeroIdentificadorDeudor, _
                            ByVal strFormasPago, _
                            ByVal dblMontoTotalPagar, _
                            ByVal strRecibosPagar, _
                            ByVal strNumeroTrace)


        Dim obPagos As New COM_SIC_Recaudacion.clsPagos
        Dim objCajas As New COM_SIC_Cajas.clsCajas

        'Variables de Auditoria
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
        Dim Detalle(9, 3) As String

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria

        ' fin de variables de auditoria

        'AUDITORIA
        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcPRec")
        wParam5 = 1
        wParam6 = "Recaudacion Postpago"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtPRec")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        Detalle(1, 1) = "OfVta"
        Detalle(1, 2) = strPuntoDeVenta
        Detalle(1, 3) = "Oficina de Venta"

        Detalle(2, 1) = "Canal"
        Detalle(2, 2) = strCanal
        Detalle(2, 3) = "Canal"

        Detalle(3, 1) = "TipoIden"
        Detalle(3, 2) = strTipoIdentificadorDeudor
        Detalle(3, 3) = "Tipo de Identificador"

        Detalle(4, 1) = "Identificador"
        Detalle(4, 2) = strNumeroIdentificadorDeudor
        Detalle(4, 3) = "Identificador"

        Detalle(5, 1) = "Usuario"
        Detalle(5, 2) = strCodigoCajero
        Detalle(5, 3) = "Usuario"

        Detalle(6, 1) = "FormPag"
        Detalle(6, 2) = strFormasPago
        Detalle(6, 3) = "Trama de formas de pago"

        Detalle(7, 1) = "Monto"
        Detalle(7, 2) = dblMontoTotalPagar
        Detalle(7, 3) = "Monto"

        Detalle(8, 1) = "Recibos"
        Detalle(8, 2) = strRecibosPagar
        Detalle(8, 3) = "Trama de Recibos"

        Detalle(9, 1) = "Trace"
        Detalle(9, 2) = strNumeroTrace
        Detalle(9, 3) = "Trace"

        'FIN AUDITORIA


        'Response.Write(strRutaLog + "<br>")
        'Response.Write(strDetalleLog + "<br>")
        'Response.Write(strPuntoDeVenta + "<br>")
        'Response.Write(strCanal + "<br>")
        'Response.Write(strBinAdquiriente + "<br>")
        'Response.Write(strCodComercio + "<br>")
        'Response.Write(strCodigoCajero + "<br>")
        'Response.Write(strTipoIdentificadorDeudor + "<br>")
        'Response.Write(strNumeroIdentificadorDeudor + "<br>")
        'Response.Write(strFormasPago + "<br>")
        'Response.Write(CStr(dblMontoTotalPagar) + "<br>")
        'Response.Write(CStr(strRecibosPagar) + "<br>")
        'Response.Write(CStr(strNumeroTrace) + "<br>")
        'Response.End()
        Dim decImpPen, decImpUsd, decVuelto As Decimal
        decImpPen = Decimal.Parse(IIf(txtRecibidoPen.Text.Trim() = "", "0.00", txtRecibidoPen.Text))
        decImpUsd = Decimal.Parse(IIf(txtRecibidoUsd.Text.Trim() = "", "0.00", txtRecibidoUsd.Text))
        decVuelto = Decimal.Parse(IIf(txtVuelto.Text.Trim() = "", "0.00", txtVuelto.Text))

        Dim numeroMac$, codigoAcreedor$, binAdquirienteRenvio$, codigoFormato$, nombreComercio$, codigoPlazaRecaudador$, medioPagoAuxiliar$, codigoEstadoDeudor$, codigoProcesador$

        numeroMac = ConfigurationSettings.AppSettings("CONST_MAC")
        codigoAcreedor = ConfigurationSettings.AppSettings("CONST_ACREEDOR")
        binAdquirienteRenvio = ConfigurationSettings.AppSettings("CONST_BIN_REENVIO")
        codigoFormato = ConfigurationSettings.AppSettings("CONST_CODIGO_FORMATO")
        nombreComercio = ConfigurationSettings.AppSettings("CONST_NOMBRE_COMERCIO")
        codigoPlazaRecaudador = ConfigurationSettings.AppSettings("CONST_CODIGO_PLAZA")
        medioPagoAuxiliar = ConfigurationSettings.AppSettings("CONST_MEDIO_PAGO_AUX")
        codigoEstadoDeudor = ConfigurationSettings.AppSettings("CONST_ESTADO_DEUDOR")
        codigoProcesador = ConfigurationSettings.AppSettings("CONST_PROCESADOR")


        Dim strRespuesta As String = obPagos.PagarSR( _
                            strRutaLog, _
                            strDetalleLog, _
                            strPuntoDeVenta, _
                            Session("OFICINA"), _
                            strCanal, _
                            strBinAdquiriente, _
                            strCodComercio, _
                            strCodigoCajero, _
                            Session("NOMBRE_COMPLETO"), _
                            strTipoIdentificadorDeudor, _
                            strNumeroIdentificadorDeudor, _
                            strFormasPago, _
                            dblMontoTotalPagar, _
                            strRecibosPagar, _
                            strNumeroTrace, _
                            Me.txtIdentificadorCliente.Value, _
                            Session("CANAL"), _
                            ConfigurationSettings.AppSettings("codAplicacion"), _
                            decImpPen, _
                            decImpUsd, _
                            decVuelto, _
                            numeroMac, _
                            codigoAcreedor, _
                            binAdquirienteRenvio, _
                            codigoFormato, _
                            nombreComercio, _
                            codigoPlazaRecaudador, _
                            medioPagoAuxiliar, _
                            codigoEstadoDeudor, _
                            codigoProcesador)
        'Response.Write(strRespuesta) : Response.End()
        Dim arrMensaje() As String = strRespuesta.Split("@")

        If ExisteError(arrMensaje) Then
            If arrMensaje.Length > 1 Then
                If InStr(1, arrMensaje(1), ";") > 0 Then
                    Dim arrMensajeError() As String
                    arrMensajeError = arrMensaje(1).Split(";")
                    If arrMensajeError.Length >= 5 Then
                        Session("strMENSREC") = arrMensajeError(4)
                        'Response.Write("<script> alert('" + arrMensajeError(4) + "');  </script>")
                        wParam5 = 0
                        wParam6 = "Error en Recaudacion Postpago." & Session("strMENSREC")
                        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                        Response.Redirect("bsqDocSinDeuda.aspx")
                    Else
                        Session("strMENSREC") = arrMensajeError(1)
                        'Response.Write("<script> alert('" + arrMensajeError(1) + "');  </script>")
                        wParam5 = 0
                        wParam6 = "Error en Recaudacion Postpago." & Session("strMENSREC")
                        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                        Response.Redirect("bsqDocSinDeuda.aspx")
                    End If

                Else
                    Session("strMENSREC") = arrMensaje(1)
                    'Response.Write("<script> alert('" + arrMensaje(1) + "');  </script>")
                    wParam5 = 0
                    wParam6 = "Error en Recaudacion Postpago." & Session("strMENSREC")
                    objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                    Response.Redirect("bsqDocSinDeuda.aspx")
                End If
            Else
                Session("strMENSREC") = "Verifique los datos ingresados. Por favor vuelva a intentar."
                'Response.Write("<script> alert('Verifique los datos ingresados. Por favor vuelva a intentar.');  </script>")
                wParam5 = 0
                wParam6 = "Error en Recaudacion Postpago." & Session("strMENSREC")
                objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                Response.Redirect("bsqDocSinDeuda.aspx")
            End If
        Else


            If cboTipDocumento1.SelectedValue = "ZEFE" Then
                objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto1.Text))
            End If
            If cboTipDocumento2.SelectedValue = "ZEFE" Then
                objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto2.Text))
            End If
            If cboTipDocumento3.SelectedValue = "ZEFE" Then
                objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto3.Text))
            End If

            'Mandar a la página de consulta a SAP.
            arrMensaje = strRespuesta.Split("@")
            Dim arrCabecera() As String = arrMensaje(1).Split(";")
            Dim strNumeroDeuda, strAccion

            strNumeroDeuda = arrCabecera(cteNUMERODEUDA)
            strAccion = cteACCION_CONFIRMACION

            'PROY-27440 INI
            Dim strNumeroRec As String = Funciones.CheckStr(arrCabecera(0))
            Dim strPedidoLog As String = "strIdRecaudacion: [" & Funciones.CheckStr(strNumeroRec) & "] "

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "- Inicio Actualizar cabezera " & "	strNumeroDeuda: " & strNumeroRec)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "- Inicio Actualizar cabezera " & " HidIdCabez: " & Me.HidIdCabez.Value)

            If (strNumeroRec <> "" And Funciones.CheckStr(Me.HidIdCabez.Value) <> "") Then
                Me.actualizar_codigo_recuadacion(strNumeroDeuda, Me.HidIdCabez.Value)
                objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "- Fin Actualizar cabezera " & "	strNumeroRec: " & strNumeroRec)
            End If

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "- " & "	strNumeroDeuda: " & strNumeroRec)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "- " & "	strAccion: " & Funciones.CheckStr(strAccion))
            'PROY-27440 FIN
            objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)

            Response.Redirect("conDocumentos.aspx?act=" & strAccion & "&num=" & strNumeroDeuda)
        End If


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

            Dim objEntity As New COM_SIC_Activaciones.BeEnvioTransacPOS
            objEntity.TransId = strCodCabez
            objEntity.FlagPago = "2"
            objEntity.idCabecera = strCodCabez
            objEntity.numPedido = strIdRecaudacion
            objEntity.IdRefAnu = ""

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "strCodCabez: " & strCodCabez)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "strIdRecaudacion: " & strIdRecaudacion)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "FlagPago: " & objEntity.FlagPago)

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

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "ActualizarTransaction Ini")
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


            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "ActualizarTransaction Fin")

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "strMsgRpt: " & strMsgRpt)
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "strCodRpt: " & strCodRpt)

            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "Fin")
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: actualizar_codigo_recaudacion)"
            objFileLogPos.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "Error: " & ex.Message & MaptPath)
            'FIN PROY-140126

        End Try
    End Sub
    'PROY-27440 FIN


    'INI CCC - SD_631020
    Private Function VerificarVias(ByVal ds As DataSet) As DataTable

        Dim dtVias As DataTable = New DataTable("ViasPago")
        Dim dtViasFiltro As New DataTable("ViasPagoF") 'CCC

        Try
            dtVias = ds.Tables(0).Clone
            For Each sRow As DataRow In ds.Tables(0).Rows
                dtVias.ImportRow(sRow)
            Next

            'INI CCC - SD_631020
            Dim strViasNoPermitas As String = FormatCadenaConComillas(ConfigurationSettings.AppSettings("constViasSapRestringida"))
            Dim dv As New DataView
            Dim iItems As Integer = 0
            Dim fila As DataRow
            dv.Table = dtVias
            dtViasFiltro = dtVias.Clone()
            dv.RowFilter = "CCINS NOT IN (" & strViasNoPermitas & ")"
            iItems = dv.Count
            For x As Int32 = 0 To iItems - 1
                fila = dtViasFiltro.NewRow()
                fila.Item(0) = dv.Item(x).Item(0)
                fila.Item(1) = dv.Item(x).Item(1)
                dtViasFiltro.Rows.Add(fila)
            Next
            Return dtViasFiltro
            'FIN CCC - SD_631020
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "');</script>")
            Response.End()
        End Try
    End Function
    'FIN CCC - SD_631020

    'INI CCC - SD_631020
    Private Function FormatCadenaConComillas(ByVal Cadena As String) As String

        Dim Arr As String
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
                Arr = Arr & "'" & Valor & "',"
            Else
                Arr = Arr & "'" & Cadena & "'"
            End If
        End While

        Return Arr
    End Function
    'FIN CCC - SD_631020
    'INICIATIVA-318 INI
    Private Sub cboTipDocumento1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipDocumento1.SelectedIndexChanged
        Dim bolAutorizado As Boolean
        Dim strFormaPago As String
        Dim strCombo As String

        strCombo = "cboTipDocumento1"
        strFormaPago = cboTipDocumento1.SelectedValue
        hddcboFormaPago.Value = cboTipDocumento1.SelectedValue
        If cboTipDocumento1.SelectedIndex = 0 Then
            bolAutorizado = True
        Else
            bolAutorizado = ObtenerAprobacionFormaPago(cboTipDocumento1.SelectedValue)
        End If

        If (bolAutorizado = False) Then
            cboTipDocumento1.SelectedIndex = 0
            Response.Write("<script language=javascript>alert('Usuario no cuenta con los permisos sufiientes para la forma de Pago');</script>")
            Response.Write("<script language=javascript>window.open('../Pagos/frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")
        End If
    End Sub

    Private Sub cboTipDocumento2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipDocumento2.SelectedIndexChanged
        Dim bolAutorizado As Boolean
        Dim strFormaPago As String
        Dim strCombo As String

        strCombo = "cboTipDocumento2"
        strFormaPago = cboTipDocumento2.SelectedValue
        hddcboFormaPago.Value = cboTipDocumento2.SelectedValue
        If cboTipDocumento2.SelectedIndex = 0 Then
            bolAutorizado = True
        Else
            bolAutorizado = ObtenerAprobacionFormaPago(cboTipDocumento2.SelectedValue)
        End If

        If (bolAutorizado = False) Then
            cboTipDocumento2.SelectedIndex = 0
            Response.Write("<script language=javascript>alert('Usuario no cuenta con los permisos sufiientes para la forma de Pago');</script>")
            Response.Write("<script language=javascript>window.open('../Pagos/frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")
        End If
    End Sub

    Private Sub cboTipDocumento3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipDocumento3.SelectedIndexChanged
        Dim bolAutorizado As Boolean
        Dim strFormaPago As String
        Dim strCombo As String

        strCombo = "cboTipDocumento3"
        strFormaPago = cboTipDocumento3.SelectedValue
        hddcboFormaPago.Value = cboTipDocumento3.SelectedValue
        If cboTipDocumento3.SelectedIndex = 0 Then
            bolAutorizado = True
        Else
            bolAutorizado = ObtenerAprobacionFormaPago(cboTipDocumento3.SelectedValue)
        End If

        If (bolAutorizado = False) Then
            cboTipDocumento3.SelectedIndex = 0
            Response.Write("<script language=javascript>alert('Usuario no cuenta con los permisos sufiientes para la forma de Pago');</script>")
            Response.Write("<script language=javascript>window.open('../Pagos/frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")
        End If
    End Sub

    Private Function ConsultaParametrosFormaPagoPerfil(ByVal strIdenLog As String)

        Try

            Dim objpvuDB As New COM_SIC_Activaciones.clsConsultaPvu
            Dim oParamteros As New COM_SIC_Activaciones.BEParametros
            Dim strCodGrupo As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("key_ParanGrupoFormaPagoPerfil"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdenLog & "- " & "--Inicio Proceso Consulta Parametros--")
            If strCodGrupo <> "" Then
                arrParametrosFormaPagoPerfil = objpvuDB.ConsultaParametros(strCodGrupo)
            End If

            Dim codAplicacion As String = ConfigurationSettings.AppSettings("codAplicacion")
            Dim objAuditoriaWS As New AuditoriaWS.EbsAuditoriaService
            Dim oAccesoRequest As New AuditoriaWS.AccesoRequest
            Dim oAccesoResponse As New AuditoriaWS.AccesoResponse

            objAuditoriaWS.Url = ConfigurationSettings.AppSettings("consRutaWSSeguridad").ToString()
            objAuditoriaWS.Credentials = System.Net.CredentialCache.DefaultCredentials
            objAuditoriaWS.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("ConstTimeOutEmpleado").ToString())

            oAccesoRequest.usuario = Session("strUsuario")
            oAccesoRequest.aplicacion = codAplicacion

            oAccesoResponse = objAuditoriaWS.leerDatosUsuario(oAccesoRequest)

            If oAccesoResponse.resultado.estado = "1" Then

                If oAccesoResponse.auditoria.AuditoriaItem.item.Length > 0 Then
                    Dim item As New COM_SIC_Seguridad.EntidadConsulSeguridad
                    For i As Integer = 0 To oAccesoResponse.auditoria.AuditoriaItem.item.Length - 1
                        strCodPerfilFormaPago = strCodPerfilFormaPago & oAccesoResponse.auditoria.AuditoriaItem.item(i).perfil & ","
                    Next
                    strCodPerfilFormaPago = strCodPerfilFormaPago.Substring(0, strCodPerfilFormaPago.Length - 1)
                Else
                    strCodPerfilFormaPago = Session("codPerfil")
                End If

            End If

        Catch ex As Exception

        End Try
    End Function
    Public Function ObtenerAprobacionFormaPago(ByVal strFormaPago As String) As Boolean

        Dim strValor As String
        Dim strValor1 As String
        Dim strPerfilusuario As String = ""
        Dim strConfirma As String
        Dim sArrayPerfiles As String()
        Dim bolAutorizado As Boolean


        Try

            strPerfilusuario = Session("codPerfil")

            If strCodPerfilFormaPago.Length > 0 Then
                sArrayPerfiles = strCodPerfilFormaPago.Split(","c)
            Else
                sArrayPerfiles = strPerfilusuario.Split("$"c)
            End If

            For Each item As BEParametros In arrParametrosFormaPagoPerfil
                strValor = item.strValor.Split("|")(0)
                strValor1 = item.strValor1.Split("|")(0)
                Select Case strValor
                    Case strFormaPago
                        For Each sPerfil As String In sArrayPerfiles
                            If strValor1 = sPerfil Then
                                bolAutorizado = True
                                Exit For
                            Else
                                bolAutorizado = False
                            End If
                        Next

                        If (bolAutorizado) Then
                            Exit For
                        End If
                End Select
            Next
        Catch ex As Exception
        End Try
        Return bolAutorizado
    End Function
    'INICIATIVA-318 FIN

End Class



'Public Class resDocumentos
'    Inherits System.Web.UI.Page

'#Region " Código generado por el Diseñador de Web Forms "

'    'El Diseñador de Web Forms requiere esta llamada.
'    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

'    End Sub
'    Protected WithEvents hdnMensaje As System.Web.UI.HtmlControls.HtmlInputHidden
'    Protected WithEvents hdnPuntoDeVenta As System.Web.UI.HtmlControls.HtmlInputHidden
'    Protected WithEvents hdnUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
'    Protected WithEvents hdnBinAdquiriente As System.Web.UI.HtmlControls.HtmlInputHidden
'    Protected WithEvents hdnCodComercio As System.Web.UI.HtmlControls.HtmlInputHidden
'    Protected WithEvents intCanal As System.Web.UI.HtmlControls.HtmlInputHidden
'    Protected WithEvents txtNumeroTrace As System.Web.UI.HtmlControls.HtmlInputHidden
'    Protected WithEvents hdnRutaLog As System.Web.UI.HtmlControls.HtmlInputHidden
'    Protected WithEvents hdnDetalleLog As System.Web.UI.HtmlControls.HtmlInputHidden
'    Protected WithEvents txtIdentificador As System.Web.UI.HtmlControls.HtmlInputHidden
'    Protected WithEvents txtTipoIdentificador As System.Web.UI.HtmlControls.HtmlInputHidden
'    Protected WithEvents lblTC As System.Web.UI.WebControls.Label
'    Protected WithEvents txtIdentificadorCliente As System.Web.UI.HtmlControls.HtmlInputText
'    Protected WithEvents txtNombreCliente As System.Web.UI.HtmlControls.HtmlInputText
'    Protected WithEvents txtNumeroDocumentos As System.Web.UI.HtmlControls.HtmlInputText
'    Protected WithEvents txtValorDeuda As System.Web.UI.HtmlControls.HtmlInputText
'    Protected WithEvents cboTipDocumento1 As System.Web.UI.WebControls.DropDownList
'    Protected WithEvents cboTipDocumento2 As System.Web.UI.WebControls.DropDownList
'    Protected WithEvents cboTipDocumento3 As System.Web.UI.WebControls.DropDownList
'    Protected WithEvents txtDoc1 As System.Web.UI.WebControls.TextBox
'    Protected WithEvents txtDoc2 As System.Web.UI.WebControls.TextBox
'    Protected WithEvents txtDoc3 As System.Web.UI.WebControls.TextBox
'    Protected WithEvents txtMonto1 As System.Web.UI.WebControls.TextBox
'    Protected WithEvents txtMonto2 As System.Web.UI.WebControls.TextBox
'    Protected WithEvents txtMonto3 As System.Web.UI.WebControls.TextBox
'    Protected WithEvents txtRecibidoPen As System.Web.UI.WebControls.TextBox
'    Protected WithEvents txtVuelto As System.Web.UI.WebControls.TextBox
'    Protected WithEvents txtRecibidoUsd As System.Web.UI.WebControls.TextBox
'    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
'    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
'    Protected WithEvents txtTarjCred As System.Web.UI.WebControls.TextBox
'    Protected WithEvents txtBIN As System.Web.UI.WebControls.TextBox
'    Protected WithEvents strRecibos As System.Web.UI.HtmlControls.HtmlInputHidden

'    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
'    'No se debe eliminar o mover.
'    Private designerPlaceholderDeclaration As System.Object

'    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
'        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
'        'No la modifique con el editor de código.
'        InitializeComponent()
'    End Sub

'#End Region

'    '*****************************************************'
'    '* Constantes de la Página
'    '*****************************************************'
'    Public Const cteNOMBRECLIENTE = 0
'    Public Const cteIDENTIFICADORCLIENTE = 1
'    Public Const cteNUMEROOPERACIONCOBRANZA = 2
'    Public Const cteVALORDEUDA = 3
'    Public Const cteNUMERODOCUMENTOS = 4
'    Public Const cteFECHAHORATRANSACCION = 5
'    Public Const cteNUMEROTRACE = 6
'    Public Const cteNUMERODEUDA = 0
'    Public Const cteCODMONEDA_SOLES = "604"
'    Public Const cteVALMONEDA_SOLES = "PEN"
'    Public Const cteCODMONEDA_DOLARES = "840"
'    Public Const cteVALMONEDA_DOLARES = "USD"
'    Const cteACCION_CONFIRMACION = 1

'    Public Recibos() As String


'    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

'        txtMonto1.Attributes.Add("onChange", "javascript:f_Recalcular(this);")
'        txtMonto2.Attributes.Add("onChange", "javascript:f_Recalcular(this);")
'        txtMonto3.Attributes.Add("onChange", "javascript:f_Recalcular(this);")
'        txtMonto1.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
'        txtMonto2.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
'        txtMonto3.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
'        txtRecibidoPen.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
'        txtRecibidoUsd.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
'        txtRecibidoPen.Attributes.Add("onChange", "javascript:CalculoVuelto();")
'        txtRecibidoUsd.Attributes.Add("onChange", "javascript:CalculoVuelto();")
'        btnGrabar.Attributes.Add("onClick", "f_Grabar()")

'        If (strRecibos.Value.Trim().Length > 0) Then
'            Recibos = strRecibos.Value.Split(",")
'        End If

'        If Not Page.IsPostBack Then
'            Try
'                If Not Request.QueryString("pTipoIdent") Is Nothing Then
'                    Me.txtTipoIdentificador.Value = Request.QueryString("pTipoIdent")
'                End If
'                If Not Request.QueryString("pIdent") Is Nothing Then
'                    Me.txtIdentificador.Value = Request.QueryString("pIdent")
'                End If
'                LlenaCombos()

'                LeeDatosValidar()
'                LeeParametros()
'                ObtenerDocumentosPorPagar(Me.hdnRutaLog.Value, _
'                                                        Me.hdnDetalleLog.Value, _
'                                                        Me.hdnPuntoDeVenta.Value, _
'                                                        Me.intCanal.Value, _
'                                                        Me.hdnBinAdquiriente.Value, _
'                                                        Me.hdnCodComercio.Value, _
'                                                        Me.hdnUsuario.Value, _
'                                                        Me.txtTipoIdentificador.Value, _
'                                                    Me.txtIdentificador.Value)
'            Catch ex As Exception
'                Response.Write("<script language=jscript> alert('" + ex.Message + "'); window.location='bsqDocumentos.aspx';</script>")
'            End Try
'        End If
'    End Sub

'    Private Sub LeeParametros()

'        Dim cteCODIGO_CANAL As String = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")
'        Dim cteCODIGO_BINADQUIRIENTE As String = ConfigurationSettings.AppSettings("cteCODIGO_BINADQUIRIENTE")
'        Dim cteCODIGO_RUTALOG As String = ConfigurationSettings.AppSettings("cteCODIGO_RUTALOG")
'        Dim cteCODIGO_DETALLELOG As String = ConfigurationSettings.AppSettings("cteCODIGO_DETALLELOG")

'        Me.hdnPuntoDeVenta.Value = Session("ALMACEN")
'        Me.intCanal.Value = cteCODIGO_CANAL
'        Me.hdnUsuario.Value = Session("USUARIO")
'        Me.hdnBinAdquiriente.Value = Session("ALMACEN") 'cteCODIGO_BINADQUIRIENTE
'        Me.hdnCodComercio.Value = Session("ALMACEN")
'        Me.hdnRutaLog.Value = cteCODIGO_RUTALOG
'        Me.hdnDetalleLog.Value = cteCODIGO_DETALLELOG
'        Me.lblTC.Text = ObtenerTipoCambio(Now.ToString("d"))

'    End Sub


'    Private Function ObtenerTipoCambio(ByVal strFecha As String) As String

'        Dim obPagos As New SAP_SIC_Pagos.clsPagos
'        Return obPagos.Get_TipoCambio(strFecha).ToString("N2")

'    End Function

'    Private Sub ObtenerDocumentosPorPagar( _
'                           ByVal strRutaLog As String, _
'                           ByVal strDetalleLog As String, _
'                           ByVal strPuntoDeVenta As String, _
'                           ByVal intCanal As String, _
'                           ByVal strBinAdquiriente As String, _
'                           ByVal strCodComercio As String, _
'                           ByVal strUsuario As String, _
'                           ByVal strTipoIdentificador As String, _
'                           ByVal strIdentificador As String)

'        Dim obRecaud As New COM_SIC_Recaudacion.clsConsultas

'        'Response.Write(strRutaLog + "<br>")
'        'Response.Write(strDetalleLog + "<br>")
'        'Response.Write(strPuntoDeVenta + "<br>")
'        'Response.Write(intCanal + "<br>")
'        'Response.Write(strBinAdquiriente + "<br>")
'        'Response.Write(strCodComercio + "<br>")
'        'Response.Write(strUsuario + "<br>")
'        'Response.Write(strTipoIdentificador + "<br>")
'        'Response.Write(strIdentificador + "<br>")
'        'Response.End()

'        Dim strResult As String = obRecaud.ConsultarRecibos(strRutaLog, _
'                                                            strDetalleLog, _
'                                                            strPuntoDeVenta, _
'                                                            intCanal, _
'                                                            strBinAdquiriente, _
'                                                            strCodComercio, _
'                                                            strUsuario, _
'                                                            strTipoIdentificador, _
'                                                            strIdentificador)


'        Dim arrMensaje() As String = strResult.Split("@")
'        Dim arrCabecera() As String
'        Dim arrDetalle() As String
'        'Response.Write("<script> alert('" + strResult + "');</script>")

'        '*******Si hay error
'        If ExisteError(arrMensaje) OrElse arrMensaje.Length < 3 Then  '
'            Response.Write("<script> alert('" + arrMensaje(1).Trim() + "'); window.location='bsqDocumentos.aspx'; </script>")
'        Else '** si no hay error
'            arrCabecera = arrMensaje(1).Split(";")
'            arrDetalle = arrMensaje(2).Split("|")

'            Me.txtNombreCliente.Value = arrCabecera(cteNOMBRECLIENTE)
'            Me.txtValorDeuda.Value = arrCabecera(cteVALORDEUDA)
'            Me.txtMonto1.Text = arrCabecera(cteVALORDEUDA)
'            Me.txtRecibidoPen.Text = arrCabecera(cteVALORDEUDA)
'            Me.txtRecibidoUsd.Text = "0.00"
'            Me.txtVuelto.Text = "0.00"
'            Me.txtIdentificadorCliente.Value = arrCabecera(cteIDENTIFICADORCLIENTE)
'            Me.txtNumeroDocumentos.Value = arrCabecera(cteNUMERODOCUMENTOS)
'            Me.txtNumeroTrace.Value = arrCabecera(cteNUMEROTRACE)
'            If (arrDetalle.Length) <> CInt(Me.txtNumeroDocumentos.Value) Then
'                Response.Write("<script> alert('Datos Invalidos en la información de los Recibos. Por favor vuelva a intentar o comuniquese con el Help Desk Comercial.');  window.location='bsqDocumentos.aspx'; </script>")
'            End If
'            Recibos = arrDetalle
'            strRecibos.Value = Join(Recibos, ",")
'        End If

'    End Sub

'    Private Function ExisteError(ByVal arrMensaje() As String) As Boolean
'        Dim bresult As Boolean = True

'        If arrMensaje(0).Trim().Length > 0 Then
'            If IsNumeric(arrMensaje(0).Trim()) Then
'                If Integer.Parse(arrMensaje(0).Trim()) = 0 Then
'                    bresult = False
'                End If
'            End If
'        End If

'        Return bresult
'    End Function



'    Public Function FormateoFecha(ByVal strFecha As String, ByVal intTipo As Integer) As String
'        Dim strNuevaFecha As String
'        Select Case intTipo
'            Case 0
'                '12345678
'                '20051103
'                If Len(Trim(strFecha)) > 0 Then
'                    strNuevaFecha = Mid(strFecha, 7, 2) & "/" & Mid(strFecha, 5, 2) & "/" & Mid(strFecha, 1, 4)
'                End If
'            Case Else
'                strNuevaFecha = strFecha
'        End Select

'        Return strNuevaFecha
'    End Function

'    Public Function FormateoMoneda(ByVal strFecha As String, ByVal strMoneda As String)
'        Dim strNuevaMoneda
'        Select Case strMoneda
'            Case cteCODMONEDA_SOLES
'                strNuevaMoneda = cteVALMONEDA_SOLES
'            Case cteCODMONEDA_DOLARES
'                strNuevaMoneda = cteVALMONEDA_DOLARES
'            Case Else
'                strNuevaMoneda = strFecha
'        End Select
'        FormateoMoneda = strNuevaMoneda
'    End Function


'    Public Function FormateoMonto(ByVal strMonto As String) As Double
'        Dim strNuevoMonto As String
'        strNuevoMonto = Mid(strMonto, 1, 9) & "." & Mid(strMonto, 10, 2)
'        Return CDbl(strNuevoMonto)
'    End Function

'    ''**********

'    Private Sub LlenaCombos()
'        Dim objPagos As New SAP_SIC_Pagos.clsPagos

'        Dim dsFormaPago As DataSet = objPagos.Get_ConsultaViasPago(Session("ALMACEN"))

'        cboLoad(dsFormaPago, cboTipDocumento1)
'        cboLoad(dsFormaPago, cboTipDocumento2)
'        cboLoad(dsFormaPago, cboTipDocumento3)


'        cboTipDocumento2.Items.Insert(0, "")
'        cboTipDocumento3.Items.Insert(0, "")
'        'VIA DE PAGO POR DEFECTO
'        cboTipDocumento1.SelectedValue = "ZEFE"
'        cboTipDocumento2.SelectedValue = ""
'        cboTipDocumento3.SelectedValue = ""


'    End Sub


'    Private Sub cboLoad(ByVal dsFormaPago As DataSet, ByRef cboCampo As DropDownList)
'        cboCampo.DataSource = dsFormaPago.Tables(0)
'        cboCampo.DataTextField = "VTEXT"
'        cboCampo.DataValueField = "CCINS"
'        cboCampo.DataBind()

'    End Sub

'    Private Sub LeeDatosValidar()
'        '***************LEE TARJETAS CREDITO
'        Dim objSap As New SAP_SIC_Pagos.clsPagos
'        Dim dsTmp As DataSet = objSap.Get_Tarjeta_Credito()
'        Dim dr As DataRow
'        txtTarjCred.Text = ""
'        For Each dr In dsTmp.Tables(0).Rows
'            txtTarjCred.Text += CStr(dr(0)) + ";"
'        Next

'        '*************leee BIN
'        Dim obCajas As New COM_SIC_Cajas.clsCajas
'        dsTmp = obCajas.FP_ListaBIN()

'        txtBIN.Text = ""
'        For Each dr In dsTmp.Tables(0).Rows
'            txtBIN.Text += CStr(dr(0)) + ";"
'        Next

'    End Sub



'    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
'        Response.Redirect("bsqDocumentos.aspx")
'    End Sub

'    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
'        Dim MontoTotalAPagar, TramaFormasPagoSAP

'        Try

'            MontoTotalAPagar = Me.ObtenerMontoTotalPagar()
'            TramaFormasPagoSAP = Me.ObtenerTramaFormasDePagoSAP()

'            'Se hace esta parte porque cuando se trae del lado cliente, se trae la trama separada por coma ",".

'            Dim TramaRecibosST As String = Me.ObtenerTramaRecibosST(strRecibos.Value)
'            'Me.Recibos = TramaRecibosST.Split("|")



'            Me.PagarDocumentos( _
'                                Me.hdnRutaLog.Value, _
'                                Me.hdnDetalleLog.Value, _
'                                Me.hdnPuntoDeVenta.Value, _
'                                Me.intCanal.Value, _
'                                Me.hdnBinAdquiriente.Value, _
'                                Me.hdnCodComercio.Value, _
'                                Me.hdnUsuario.Value, _
'                                Me.txtTipoIdentificador.Value, _
'                                Me.txtIdentificador.Value, _
'                                TramaFormasPagoSAP, _
'                                MontoTotalAPagar, _
'                                TramaRecibosST, _
'                                txtNumeroTrace.Value)


'        Catch ex As Exception
'            Response.Write("<script language=jscript> alert('" + ex.Message + "'); window.location='bsqDocumentos.aspx';</script>")
'        End Try
'    End Sub


'    Private Function ObtenerTramaRecibosST(ByVal strRecibos As String) As String

'        Return strRecibos.Replace(",", "|")

'    End Function


'    Private Function ObtenerTramaFormasDePagoSAP() As String
'        Dim strResp As String = ""

'        If IsNumeric(Me.txtMonto1.Text) AndAlso Decimal.Parse(Me.txtMonto1.Text) > 0 Then
'            strResp = Me.cboTipDocumento1.SelectedValue.Trim() + ";" + Me.txtMonto1.Text.Trim() + ";" + Me.txtDoc1.Text.Trim() + ";;"
'        End If
'        If IsNumeric(Me.txtMonto2.Text) AndAlso Decimal.Parse(Me.txtMonto2.Text) > 0 Then
'            strResp += "|" + Me.cboTipDocumento2.SelectedValue.Trim() + ";" + Me.txtMonto2.Text.Trim() + ";" + Me.txtDoc2.Text.Trim() + ";;"
'        End If
'        If IsNumeric(Me.txtMonto3.Text) AndAlso Decimal.Parse(Me.txtMonto3.Text) > 0 Then
'            strResp += "|" + Me.cboTipDocumento3.SelectedValue.Trim() + ";" + Me.txtMonto3.Text.Trim() + ";" + Me.txtDoc3.Text.Trim() + ";;"
'        End If

'        Return strResp
'    End Function

'    Private Function ObtenerMontoTotalPagar() As Decimal
'        Dim decResult As Decimal = 0

'        If IsNumeric(Me.txtMonto1.Text) Then
'            decResult += Decimal.Parse(Me.txtMonto1.Text)
'        End If
'        If IsNumeric(Me.txtMonto2.Text) Then
'            decResult += Decimal.Parse(Me.txtMonto2.Text)
'        End If
'        If IsNumeric(Me.txtMonto3.Text) Then
'            decResult += Decimal.Parse(Me.txtMonto3.Text)
'        End If

'        Return decResult
'    End Function


'    Private Sub PagarDocumentos( _
'                            ByVal strRutaLog, _
'                            ByVal strDetalleLog, _
'                            ByVal strPuntoDeVenta, _
'                            ByVal strCanal, _
'                            ByVal strBinAdquiriente, _
'                            ByVal strCodComercio, _
'                            ByVal strCodigoCajero, _
'                            ByVal strTipoIdentificadorDeudor, _
'                            ByVal strNumeroIdentificadorDeudor, _
'                            ByVal strFormasPago, _
'                            ByVal dblMontoTotalPagar, _
'                            ByVal strRecibosPagar, _
'                            ByVal strNumeroTrace)


'        Dim obPagos As New COM_SIC_Recaudacion.clsPagos
'        Dim objCajas As New COM_SIC_Cajas.clsCajas

'        'Response.Write(strRutaLog + "<br>")
'        'Response.Write(strDetalleLog + "<br>")
'        'Response.Write(strPuntoDeVenta + "<br>")
'        'Response.Write(strCanal + "<br>")
'        'Response.Write(strBinAdquiriente + "<br>")
'        'Response.Write(strCodComercio + "<br>")
'        'Response.Write(strCodigoCajero + "<br>")
'        'Response.Write(strTipoIdentificadorDeudor + "<br>")
'        'Response.Write(strNumeroIdentificadorDeudor + "<br>")
'        'Response.Write(strFormasPago + "<br>")
'        'Response.Write(CStr(dblMontoTotalPagar) + "<br>")
'        'Response.Write(CStr(strRecibosPagar) + "<br>")
'        'Response.Write(CStr(strNumeroTrace) + "<br>")
'        'Response.End()
'        Dim decImpPen, decImpUsd, decVuelto As Decimal
'        decImpPen = Decimal.Parse(IIf(txtRecibidoPen.Text.Trim() = "", "0.00", txtRecibidoPen.Text))
'        decImpUsd = Decimal.Parse(IIf(txtRecibidoUsd.Text.Trim() = "", "0.00", txtRecibidoUsd.Text))
'        decVuelto = Decimal.Parse(IIf(txtVuelto.Text.Trim() = "", "0.00", txtVuelto.Text))

'        Dim strRespuesta As String = obPagos.Pagar( _
'                            strRutaLog, _
'                            strDetalleLog, _
'                            strPuntoDeVenta, _
'                            strCanal, _
'                            strBinAdquiriente, _
'                            strCodComercio, _
'                            strCodigoCajero, _
'                            strTipoIdentificadorDeudor, _
'                            strNumeroIdentificadorDeudor, _
'                            strFormasPago, _
'                            dblMontoTotalPagar, _
'                            strRecibosPagar, _
'                            strNumeroTrace, _
'                            Me.txtIdentificadorCliente.Value, _
'                            Session("CANAL"), _
'                            ConfigurationSettings.AppSettings("codAplicacion"), _
'                            decImpPen, _
'                            decImpUsd, _
'                            decVuelto)

'        Dim arrMensaje() As String = strRespuesta.Split("@")

'        If ExisteError(arrMensaje) Then
'            If arrMensaje.Length > 1 Then
'                If InStr(1, arrMensaje(1), ";") > 0 Then
'                    Dim arrMensajeError() As String
'                    arrMensajeError = arrMensaje(1).Split(";")
'                    If arrMensajeError.Length >= 5 Then
'                        Response.Write("<script> alert('" + arrMensajeError(4) + "');  </script>")
'                    Else
'                        Response.Write("<script> alert('" + arrMensajeError(1) + "');  </script>")
'                    End If

'                Else
'                    Response.Write("<script> alert('" + arrMensaje(1) + "');  </script>")
'                End If
'            Else
'                Response.Write("<script> alert('Verifique los datos ingresados. Por favor vuelva a intentar.');  </script>")
'            End If
'        Else


'            If cboTipDocumento1.SelectedValue = "ZEFE" Then
'                objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto1.Text))
'            End If
'            If cboTipDocumento2.SelectedValue = "ZEFE" Then
'                objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto2.Text))
'            End If
'            If cboTipDocumento3.SelectedValue = "ZEFE" Then
'                objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto3.Text))
'            End If

'            'Mandar a la página de consulta a SAP.
'            arrMensaje = strRespuesta.Split("@")
'            Dim arrCabecera() As String = arrMensaje(1).Split(";")
'            Dim strNumeroDeuda, strAccion

'            strNumeroDeuda = arrCabecera(cteNUMERODEUDA)
'            strAccion = cteACCION_CONFIRMACION

'            Response.Redirect("conDocumentos.aspx?act=" & strAccion & "&num=" & strNumeroDeuda)
'        End If


'    End Sub
'End Class

