Imports COM_SIC_INActChip
Imports System.IO
Imports COM_SIC_Activaciones
Imports SisCajas.Funciones
Imports SisCajas.clsActivaciones
Imports System.Text
Imports System.Net
Imports System.Globalization
Imports COM_SIC_Cajas               '*** agregado para el flujo de caja ***'
Imports COM_SIC_FacturaElectronica  '**FE **'
Imports NEGOCIO_SIC_SANS

Public Class devolEfectivo
    Inherits SICAR_WebBase


#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents txtFecConta As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNotaCre As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtBolFact As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtMonto As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdConsultaNC As System.Web.UI.WebControls.Button
    Protected WithEvents hidMensajeNC As System.Web.UI.HtmlControls.HtmlInputHidden
    'PROY-27440 FIN
    Protected WithEvents txtNombCli As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechPago As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNroPedido As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents HidPtoVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIntAutPos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidCodOpera As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDesOpera As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoOpera As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoTarjeta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidEstTrans As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoPOS As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoTran As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoMoneda As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTransMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMonedaMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMonedaVisa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblEnvioPos As System.Web.UI.WebControls.Label
    Protected WithEvents HidApliPOS As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFlagGuardar As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidCajero As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected WithEvents HidDatoPosVisa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoPosMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjCajero As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIdCabez As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoAuditPos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjErrorGuardarTrans As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTransacExitosa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTransacIncompleta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjValIP As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMontoTotal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMontoEfectivo As System.Web.UI.HtmlControls.HtmlInputHidden
    'PROY-27440 FIN

    Protected WithEvents HidNumIntentosAnular As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjErrorNumIntentos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjErrorTimeOut As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFlagCajaCerrada As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjCajaCerrada As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidNumIntentosPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidPedidoOrigenIot As System.Web.UI.HtmlControls.HtmlInputHidden
    'PROY-140379

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogDevolEfectivo")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strIdentifyLog As String
    Dim Tipo_Documento As String
    'PROY-27440 INI
    Dim nameFilePos As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivoPos As String = objFileLog.Log_CrearNombreArchivo(nameFilePos)
    'PROY-27440 FIN
    'PROY-29380 INI
    Dim fechDocuPago As DateTime
    'PROY-29380 FIN

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                'PROY-27440'-Inicio
                ' txtNotaCre.Attributes.Add("onBlur", "f_ConsultaNC()")
                Session("flag_Switch") = "0"
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "Integracion SICAR - POS - INICIO CARGANDO PARAMETROS - DEVOLUCIONES")
                load_data_param_pos()
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "Integracion SICAR - POS - FIN CARGANDO PARAMETROS - DEVOLUCIONES")
                'PROY-27440'-Fin
                strIdentifyLog = txtNotaCre.Value
                txtFecConta.Value = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000") 'Now.Date.ToString("d")
                txtMonto.Value = "0.00"
            End If

            Me.RegisterStartupScript("VerificarSwitch", "<script language=javascript>f_validar_switch('" & Session("flag_Switch") & "');</script>") 'PROY-27440'

        End If
    End Sub
    'PROY-27440'-Inicio

    Private Sub load_data_param_pos()

        Me.HidPtoVenta.Value = Funciones.CheckStr(Session("ALMACEN"))
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidPtoVenta : " & HidPtoVenta.Value)

        Me.HidCajero.Value = Funciones.CheckStr(Session("USUARIO"))
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidCajero : " & HidCajero.Value)

        Dim strIpClient As String = Funciones.CheckStr(Session("IpLocal"))

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - Validacion Integracion INI")
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidPtoVenta : " & HidPtoVenta.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "strIpClient : " & strIpClient)


        Dim strCodRptaFlag As String = ""
        Dim strMsgRptaFlag As String = ""

        Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS

        'INI CONSULTA INTEGRACION AUTOMATICO POS

        Dim strFlagIntAut As String = ""

        strCodRptaFlag = "" : strMsgRptaFlag = ""
        objConsultaPos.Obtener_Integracion_Auto(Funciones.CheckStr(Me.HidPtoVenta.Value), strIpClient, String.Empty, strFlagIntAut, strCodRptaFlag, strMsgRptaFlag)

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "strFlagIntAut : " & Funciones.CheckStr(strFlagIntAut))
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "strCodRptaFlag : " & Funciones.CheckStr(strCodRptaFlag))
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "strMsgRptaFlag : " & Funciones.CheckStr(strMsgRptaFlag))

        Me.HidIntAutPos.Value = Funciones.CheckStr(strFlagIntAut)

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " -  Validacion Integracion FIN")

        'FIN CONSULTA INTEGRACION AUTOMATICO POS

        'FIN CONSULTA PAGO AUTOMATICO POS
        'PROY-31949 - Inicio

        HidNumIntentosAnular.Value = ClsKeyPOS.strNumIntentosAnular
        HidMsjErrorNumIntentos.Value = ClsKeyPOS.strMsjErrorNumIntentos
        HidMsjErrorTimeOut.Value = ClsKeyPOS.strMsjErrorTimeOut

        Dim objOfflineCaja As New COM_SIC_OffLine.clsOffline
        Dim cultureNameX As String = "es-PE"
        Dim cultureX As CultureInfo = New CultureInfo(cultureNameX)
        Dim dateTimeValueCaja As DateTime = Convert.ToDateTime(DateTime.Now, cultureX)
        Dim sFechaCaj As String = dateTimeValueCaja.ToLocalTime.ToShortDateString
        Dim dsCajeroA As DataSet
        dateTimeValueCaja = Convert.ToDateTime(DateTime.Now, cultureX)
        sFechaCaj = dateTimeValueCaja.ToString("dd/MM/yyyy")
        dsCajeroA = objOfflineCaja.GetDatosAsignacionCajero(HidPtoVenta.Value, sFechaCaj, Funciones.CheckStr(Session("USUARIO")).PadLeft(10, "0"))

        ' Validar cierre de caja
        If dsCajeroA.Tables(0).Rows.Count > 0 Then
            For cont As Int32 = 0 To dsCajeroA.Tables(0).Rows.Count - 1
                If dsCajeroA.Tables(0).Rows(cont).Item("CAJA_CERRADA") = "S" Then
                    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "MENSAJE : " & ClsKeyPOS.strMsjCajaCerrada)
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

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidIntAutPos : " & HidIntAutPos.Value)


        Me.HidCodOpera.Value = ClsKeyPOS.strCodOpeVE & "|" & _
                               ClsKeyPOS.strCodOpeVC & "|" & _
                               ClsKeyPOS.strCodOpeAN '01-VENTA|03-VENTA CUOTA|04-ANULACION
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidCodOpera : " & HidCodOpera.Value)

        Me.HidDesOpera.Value = ClsKeyPOS.strDesOpeVE & "|" & _
                               ClsKeyPOS.strDesOpeVC & "|" & _
                               ClsKeyPOS.strDesOpeAN
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidDesOpera : " & HidDesOpera.Value)

        Me.HidTipoOpera.Value = ClsKeyPOS.strOpeFina & "|" & ClsKeyPOS.strNoFina
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidTipoOpera : " & HidTipoOpera.Value)

        Me.HidTipoTarjeta.Value = ClsKeyPOS.strCodTarjetaVS & "|" & ClsKeyPOS.strCodTarjetaMC
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidTipoTarjeta : " & HidTipoTarjeta.Value)

        Me.HidTipoPago.Value = ClsKeyPOS.strTipPagoDEV '13 Devoluciones
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidTipoPago : " & HidTipoPago.Value)

        Me.HidEstTrans.Value = ClsKeyPOS.strEstTRanPen & "|" & _
                               ClsKeyPOS.strEstTRanPro & "|" & _
                               ClsKeyPOS.strEstTRanAce & "|" & _
                               ClsKeyPOS.strEstTRanRec & "|" & _
                               ClsKeyPOS.strEstTRanInc
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidEstTrans : " & HidEstTrans.Value)

        Me.HidTipoPOS.Value = ClsKeyPOS.strTipoPosVI & "|" & _
                              ClsKeyPOS.strTipoPosMC & "|" & _
                              ClsKeyPOS.strTipoPosAM & "|" & _
                              ClsKeyPOS.strTipoPosDI
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidTipoPOS : " & HidTipoPOS.Value)

        Me.HidTipoTran.Value = ClsKeyPOS.strTipoTransPAG & "|" & _
                               ClsKeyPOS.strTipoTransANU & "|" & _
                               ClsKeyPOS.strTipoTransRIM & "|" & _
                               ClsKeyPOS.strTipoTransRDO & "|" & _
                               ClsKeyPOS.strTipoTransRTO & "|" & _
                               ClsKeyPOS.strTipoTransAPP & "|" & _
                               ClsKeyPOS.strTipoTransCIP
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidTipoTran : " & HidTipoTran.Value)

        Me.HidTransMC.Value = ClsKeyPOS.strTranMC_Compra & "|" & _
                              ClsKeyPOS.strTranMC_Anulacion & "|" & _
                              ClsKeyPOS.strTranMC_RepDetallado & "|" & _
                              ClsKeyPOS.strTranMC_RepTotales & "|" & _
                              ClsKeyPOS.strTranMC_ReImpresion & "|" & _
                              ClsKeyPOS.strTranMC_Cierre & "|" & _
                              ClsKeyPOS.strPwdComercio_MC
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidTransMC : " & HidTransMC.Value)

        Me.HidMonedaMC.Value = ClsKeyPOS.strMonedaMC_Soles & "|" & ClsKeyPOS.strMonedaMC_Dolares
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidMonedaMC : " & HidMonedaMC.Value)

        Me.HidApliPOS.Value = ClsKeyPOS.strConstMC_POS
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidApliPOS : " & HidApliPOS.Value)

        Me.HidMonedaVisa.Value = ClsKeyPOS.strMonedaVisa_Soles & "|" & ClsKeyPOS.strMonedaVisa_Dolares
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidMonedaVisa : " & HidMonedaVisa.Value)

        Me.HidTipoMoneda.Value = ClsKeyPOS.strTipoMonSoles
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidTipoMoneda : " & HidTipoMoneda.Value)

        Me.HidMsjCajero.Value = ClsKeyPOS.strMsjValidacionCajero
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidMsjCajero : " & HidMsjCajero.Value)

        Me.HidMsjErrorGuardarTrans.Value = ClsKeyPOS.strsjErrorGuardarTrans
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidMsjErrorGuardarTrans : " & HidMsjErrorGuardarTrans.Value)

        Me.HidTransacExitosa.Value = ClsKeyPOS.strAnulacionExitosa
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidTransacExitosa : " & HidTransacExitosa.Value)

        Me.HidTransacIncompleta.Value = ClsKeyPOS.strAnulacionIncompleta
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidTransacIncompleta : " & HidTransacIncompleta.Value)

        'DATOS DEL POS
        Me.HidDatoPosVisa.Value = ""
        Me.HidDatoPosMC.Value = ""

        Me.HidDatoAuditPos.Value = Funciones.CheckStr(CurrentTerminal()) & "|" & _
        Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")) & "|" & _
        Funciones.CheckStr(Session("USUARIO"))

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidDatoAuditPos: " & HidDatoAuditPos.Value)

        Dim objSicarDB As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim strIp As String = CurrentTerminal()
        Dim strEstadoPos As String = "1"
        Dim strTipoVisa As String = "V"
        Dim ds As DataSet

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "strIpClient : " & strIpClient)

        'VISA INICIO
        strTipoVisa = "V"
        ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoVisa)

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "strIpClient : " & strIpClient)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "strEstadoPos : " & strEstadoPos)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "strTipoVisa : " & strTipoVisa)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "Count : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

        Dim strMensaje As String = ""


        If ds.Tables(0).Rows.Count > 0 Then
            Me.HidDatoPosVisa.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
            & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
            & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
            & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
            & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
            & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
            & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
            & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA"))

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidDatoPosVisa : " & HidDatoPosVisa.Value)
        Else
            strMensaje = String.Format(ClsKeyPOS.strIPMsjDesconfigurado)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "MSJ - ERROR:" & strMensaje)
            'Response.Write("<script>alert('" & strMensaje & "');</script>")
        End If
        'VISA FIN

        'MC INICIO
        strTipoVisa = "M"
        ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoVisa)

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "strIpClient : " & strIpClient)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "strEstadoPos : " & strEstadoPos)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "strTipoVisa : " & strTipoVisa)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "Count : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

        If ds.Tables(0).Rows.Count > 0 Then
            Me.HidDatoPosMC.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
            & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
            & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
            & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
            & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
            & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
            & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
            & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA"))

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "HidDatoPosMC : " & HidDatoPosMC.Value)
        Else
            strMensaje = String.Format(ClsKeyPOS.strIPMsjDesconfigurado)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Devoluciones" & " - " & "MSJ - ERROR:" & strMensaje)

        End If

        If (strMensaje <> "" And HidIntAutPos.Value = "1") Then
            Response.Write("<script>alert('" & strMensaje & "');</script>")
            HidMsjValIP.Value = strMensaje
        End If
        'MC FIN

    End Sub
    'PROY-27440 FIN

    Private Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        Try
            '/*----------------------------------------------------------------------------------------------------------------*/
            '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
            '/*----------------------------------------------------------------------------------------------------------------*/
            Dim msjRV_Devuelta As String
            Dim msjRV_NoRegistrada As String
            Dim msjRV_FallaValidaLinea As String
            Dim msjRV_LineaSinSaldo As String
            Dim msjRV_ErrorDevolucion As String
            Dim msjRV_ErrorCambioEstado As String
            Dim codGrupoMensajes As Integer = Funciones.CheckDbl(ConfigurationSettings.AppSettings("constGrupoParam_MensajesReversaRecargaVirtual"))

            Dim dsMensajesRV As DataSet = (New COM_SIC_Cajas.clsCajas).ObtenerParamByGrupo(codGrupoMensajes)
            '/*----------------------------------------------------------------------------------------------------------------*/
            '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
            '/*----------------------------------------------------------------------------------------------------------------*/
            If ValidaDatos() Then


                If (Session("flag_Switch") = 0) Then  'PROY-27440'
                    GrabarDevolucion()
                    txtBolFact.Value = ""
                    txtNotaCre.Value = ""
                    txtMonto.Value = "0.00"
                    'PROY-27440'-Inicio

                    txtNroPedido.Value = ""
                    txtNombCli.Value = ""
                    txtFechPago.Value = ""
                    fechDocuPago = Nothing 'PROY-29380 - RMR
                Else
                    If (Me.HidFlagGuardar.Value = 1) Then
                GrabarDevolucion()
                txtBolFact.Value = ""
                txtNotaCre.Value = ""
                txtMonto.Value = "0.00"
                        txtNroPedido.Value = ""
                        txtNombCli.Value = ""
                        txtFechPago.Value = ""
                        fechDocuPago = Nothing 'PROY-29380 - RMR

                        Session("flag_Switch") = 0
                        Me.RegisterStartupScript("ValidarPagos", "<script language=javascript>f_validar_switch('" & Session("flag_Switch") & "');</script>")
                    Else
                        Me.RegisterStartupScript("CargarPagos", "<script language=javascript>f_CargarFormasPago();</script>")
                        Me.RegisterStartupScript("MensajeAlerta", "<script language=javascript>alert('" & String.Format(ClsKeyPOS.strAnulMesjBloqueante, "hacer efectivo la devolucion") & "');</script>")

                    End If
                End If
                'PROY-27440'-Inicio


            End If
        Catch ex As Exception
            'PROY-27440'-Inicio
            If (Session("flag_Switch") = 1) Then
                Me.RegisterStartupScript("ValidarPagos", "<script language=javascript>f_CargarFormasPago();</script>")
            End If
            'PROY-27440'-Fin
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: btnGrabar_Click)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Exception: " & ex.ToString() & MaptPath)
            'FIN PROY-140126

            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try

    End Sub
    'PROY-27440 - INICIO
    Private Function ValidarCaja() As Boolean
        '******** PARAMETROS ******** 
        Dim objclsConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim strTipoOfiVta As String = Session("CANAL")
        Dim strUsuario As String = Session("USUARIO")
        Dim strOficina As String = Session("ALMACEN")
        Dim strCodOficina As String = ""
        Dim objOfflineCaja As New COM_SIC_OffLine.clsOffline
        Dim dsPedidoCaj As DataSet
        Dim dsCajeroA As DataSet
        Dim dtPagos, dtncanje As DataTable
        Dim drFila As DataRow
        strCodOficina = ConsultaPuntoVenta(strOficina)
        strIdentifyLog = txtNotaCre.Value
        ValidarCaja = True
        Dim i As Integer
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ====================================================")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "  INICIO Consulta Pool para buscar la Nota de Credito")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "    [IN ESTADO: " & ConfigurationSettings.AppSettings("PEDIC_ESTADO") & "]")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "    [IN FECHA: " & DateTime.ParseExact(txtFecConta.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture) & "]")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "    [IN PDV: " & strCodOficina & "]")
            dtPagos = objclsConsultaMsSap.ConsultaPoolPagos(ConfigurationSettings.AppSettings("PEDIC_ESTADO"), DateTime.ParseExact(txtFecConta.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), strCodOficina, "")
            For i = 0 To dtPagos.Rows.Count - 1
                If dtPagos.Rows(i).Item("PAGOC_CODSUNAT") = txtNotaCre.Value Then
                    drFila = dtPagos.Rows(i)
                End If
            Next

            If drFila Is Nothing Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "  Nota de Crédito/Canje no encontrada")
                Response.Write("<script>alert('Nota de Crédito/Canje no encontrada.')</script>")
                Return False

            End If
            '****Obtiene Datos del pedido ****
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - INI Valida Asignacion de Caja ")
            Dim iNroPedidoNC As Integer = Funciones.CheckInt(drFila("PEDIN_NROPEDIDO"))
            dsPedidoCaj = objclsConsultaMsSap.ConsultaPedido(iNroPedidoNC, "", "")

            Dim cultureNameX As String = "es-PE"
            Dim cultureX As CultureInfo = New CultureInfo(cultureNameX)
            Dim dateTimeValueCaja As DateTime = Convert.ToDateTime(DateTime.Now, cultureX)
            Dim dFecha As Date
            Dim sFecha As String = dateTimeValueCaja.ToLocalTime.ToShortDateString
            Dim sCajero As String = Funciones.CheckStr(Session("USUARIO")).PadLeft(10, "0")
            Dim sOficina As String = Funciones.CheckStr(Session("ALMACEN"))
            If Not dsPedidoCaj.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO") Is Nothing Then
                dFecha = CDate(dsPedidoCaj.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO"))
                sFecha = dFecha.ToString("dd/MM/yyyy")
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Fecha Pedido : " & sFecha)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Oficina : " & sOficina)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Cajero : " & sCajero)
            dsCajeroA = objOfflineCaja.GetDatosAsignacionCajero(sOficina, sFecha, sCajero)
            If (dsCajeroA Is Nothing OrElse dsCajeroA.Tables(0).Rows.Count <= 0) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Cantidad de Registros devueltos: " & dsCajeroA.Tables(0).Rows.Count.ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- No se pudo determinar el numero de la caja asignada.")
                Response.Write("<script>alert('No se encontro el Nùmero/Nombre de caja asignada.')</script>")
                Return False
            End If
            ' Validar cierre de caja
            If Not dsCajeroA Is Nothing Then
                For cont As Int32 = 0 To dsCajeroA.Tables(0).Rows.Count - 1
                    If dsCajeroA.Tables(0).Rows(cont).Item("CAJA_CERRADA") = "S" Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE : " & "Caja Cerrada, no es posible realizar el pago.")
                        Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", "Caja Cerrada, no es posible realizar la devolucion.")
                        Me.RegisterStartupScript("RegistraAlerta", script)
                        Return False
                    End If
                Next
            End If
        Catch ex As Exception
            Return False
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Error al tratar de consultar los datos de CAJA")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Error: " & ex.Message.ToString)
        Finally
            objOfflineCaja = Nothing
            dsCajeroA.Dispose()
            dsPedidoCaj.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - FIN Valida Asignacion de Caja ")
        End Try
    End Function
    'PROY-27440 - FIN
    Public Sub GrabarDevolucion()

        '******* CLASES ********
        Dim objCaja As New COM_SIC_Cajas.clsCajas
        Dim objClsPagosWS As New COM_SIC_Activaciones.clsPagosWS
        Dim objConf As New COM_SIC_Configura.clsConfigura
        Dim objclsConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim objTrsConsultaMsSap As New COM_SIC_Activaciones.clsTrsMsSap
        Dim objPool As New SAP_SIC_Pagos.clsPagos
        Dim obSap As New SAP_SIC_Pagos.clsPagos

        '******** PARAMETROS ******** 
        Dim strTipoOfiVta As String = Session("CANAL")
        Dim strUsuario As String = Session("USUARIO")
        Dim strOficina As String = Session("ALMACEN")
        Dim strCodOficina As String = ""

        '*********************
        Dim strMoneda As String = "PEN"
        Dim nImporte As Decimal = Decimal.Parse(txtMonto.Value)
        Dim strFecha As String = txtFecConta.Value
        '**********************

        Dim dsResult As DataSet
        Dim intAutoriza As Integer
        Dim drFila As DataRow
        Dim dtPagos, dtncanje As DataTable
        Dim i As Integer

        strCodOficina = ConsultaPuntoVenta(strOficina)
        strIdentifyLog = txtNotaCre.Value

        Dim strTipoDevol As String
        strTipoDevol = "E"

        Dim strTelefonoDevolucion As String 'INICIATIVA - 489

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ====================================================")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "  INICIO Consulta Pool para buscar la Nota de Credito")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "    [IN ESTADO: " & ConfigurationSettings.AppSettings("PEDIC_ESTADO") & "]")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "    [IN FECHA: " & DateTime.ParseExact(txtFecConta.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture) & "]")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "    [IN PDV: " & strCodOficina & "]")
            dtPagos = objclsConsultaMsSap.ConsultaPoolPagos(ConfigurationSettings.AppSettings("PEDIC_ESTADO"), DateTime.ParseExact(txtFecConta.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), strCodOficina, "")

            For i = 0 To dtPagos.Rows.Count - 1
                If dtPagos.Rows(i).Item("PAGOC_CODSUNAT") = txtNotaCre.Value Then
                    drFila = dtPagos.Rows(i)
                End If
            Next

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "  FIN Consulta Pool para buscar la Nota de Credito")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "  INICIO Consulta Pool para buscar la Nota de Canje")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "    [IN ESTADO: " & ConfigurationSettings.AppSettings("PEDIC_ESTADO") & "]")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "    [IN FECHA: " & DateTime.ParseExact(txtFecConta.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture) & "]")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "    [IN PDV: " & strCodOficina & "]")

            dtncanje = objclsConsultaMsSap.ConsultaPoolNotasCanje(ConfigurationSettings.AppSettings("PEDIC_ESTADO"), DateTime.ParseExact(txtFecConta.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), strCodOficina, "")

            For i = 0 To dtncanje.Rows.Count - 1
                If dtncanje.Rows(i).Item("PAGOC_CODSUNAT") = txtNotaCre.Value Then
                    drFila = dtncanje.Rows(i)
                End If
            Next
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "  FIN Consulta Pool para buscar la Nota de Canje")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ====================================================")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Error al consulta la Nota de Crédito/Canje: " & ex.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ====================================================")
        End Try

        If drFila Is Nothing Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "  Nota de Crédito/Canje no encontrada")
            Throw New ApplicationException("Nota de Crédito/Canje no encontrada")
        End If

        Dim dsPedido As DataSet
        Dim dsVendedor As DataSet
        Dim strIdVendedor As String = ""
        Dim strNomVendedor As String = ""
        Dim NLOG As String = ""
        Dim DLOG As String = ""
        Dim dsValidaDevol As DataSet

        '******* Variables para mandar al Stored Procedure**********

        Dim NroSunat_DocOrigen As String = ""
        Dim ValArray As String()
        Dim Numero_Documento_Origen As String = ""
        Dim tipoDoc_Sap As String = ""
        Dim SerieSunat As String = ""
        Dim strNumSunat As String = ""

        '******* Variables para mandar al WebService**********
        Dim Cod_PdV As String = ""
        Dim Fecha_Conta As String = ""
        Dim CorrSunat_Origen As String = ""
        Dim CorrSunat_NC As String = ""
        Dim Monto As String = ""
        Dim Usuario As String = ""
        Dim IdPedido_NC As String = ""
        Dim IdPago_NC As String = ""
        ' Dim CurrentUser As String
        'Dim CurrentTerminal As String
        Dim Moneda As String = ""
        Dim Origen As String = ""
        Dim Sociedad As String = ""

        Dim k_cod_rpta As String
        Dim k_msj_rpta As String
        Dim k_id_transaccion As String

        'Dim arreglo As String
        Dim contador As Integer = 0
        Dim Obtener_Guion As Integer = 0
        Dim Obtener_Guion_2 As Integer = 0
        Dim Obtener_Guion_3 As Integer = 0

        Dim blnEsNotaCanje As Boolean = False

        '***** INI :: Valida la asignacion de caja *****
        Dim objOfflineCaja As New COM_SIC_OffLine.clsOffline
        Dim dsPedidoCaj As DataSet
        Dim dsCajeroA As DataSet

        Dim NroPedidoDevolucion As Integer = Funciones.CheckInt(drFila("PEDIN_NROPEDIDO"))'PROY-26366

        Try
            '****Obtiene Datos del pedido ****
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - INI Valida Asignacion de Caja ")
            Dim iNroPedidoNC As Integer = Funciones.CheckInt(drFila("PEDIN_NROPEDIDO"))
            dsPedidoCaj = objclsConsultaMsSap.ConsultaPedido(iNroPedidoNC, "", "")

            Dim cultureNameX As String = "es-PE"
            Dim cultureX As CultureInfo = New CultureInfo(cultureNameX)
            Dim dateTimeValueCaja As DateTime = Convert.ToDateTime(DateTime.Now, cultureX)
            Dim dFecha As Date
            Dim sFecha As String = dateTimeValueCaja.ToLocalTime.ToShortDateString
            Dim sCajero As String = Funciones.CheckStr(Session("USUARIO")).PadLeft(10, "0")
            Dim sOficina As String = Funciones.CheckStr(Session("ALMACEN"))
            If Not dsPedidoCaj.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO") Is Nothing Then
                dFecha = CDate(dsPedidoCaj.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO"))
                sFecha = dFecha.ToString("dd/MM/yyyy")
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Fecha Pedido : " & sFecha)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Oficina : " & sOficina)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Cajero : " & sCajero)
            dsCajeroA = objOfflineCaja.GetDatosAsignacionCajero(sOficina, sFecha, sCajero)
            If (dsCajeroA Is Nothing OrElse dsCajeroA.Tables(0).Rows.Count <= 0) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Cantidad de Registros devueltos: " & dsCajeroA.Tables(0).Rows.Count.ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- No se pudo determinar el numero de la caja asignada.")
                Response.Write("<script>alert('No se encontro el Nùmero/Nombre de caja asignada.')</script>")
                Exit Sub
            End If

            ' Validar cierre de caja
            If Not dsCajeroA Is Nothing Then
                For cont As Int32 = 0 To dsCajeroA.Tables(0).Rows.Count - 1
                    If dsCajeroA.Tables(0).Rows(cont).Item("CAJA_CERRADA") = "S" Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE : " & "Caja Cerrada, no es posible realizar el pago.")
                        Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", "Caja Cerrada, no es posible realizar el pago.")
                        Me.RegisterStartupScript("RegistraAlerta", script)
                        Exit Sub
                    End If
                Next
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Error al tratar de consultar los datos de CAJA")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Error: " & ex.Message.ToString)
        Finally
            objOfflineCaja = Nothing
            dsCajeroA.Dispose()
            dsPedidoCaj.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - FIN Valida Asignacion de Caja ")
        End Try
        '***** FIN :: Valida la asignacion de caja *****

        Dim msg_corr_error As String = ""
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " ===INICIO VALIDA AUTORIZACION===")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN CANAL: " & Session("CANAL"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN PDV: " & Session("ALMACEN"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN COD APLIC: " & ConfigurationSettings.AppSettings("codAplicacion"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN USUARIO: " & Session("USUARIO"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN NOM USUARIO: " & Session("NOMBRE_COMPLETO"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN TIPO DOC: ")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN NRO DOC: ")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN NOM CLIENTE: " & drFila("PEDIV_NOMBRECLIENTE"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN NRO TELEFONO: ")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN NRO SUNAT: " & drFila("PAGOC_CODSUNAT"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN NRO PEDIDO: " & drFila("PEDIN_NROPEDIDO"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN MONTO NETO: 0")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN ID_CONF_TRAN: 2")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN SALDO INI: 0")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN ING_EFECTIVO: 0")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN CAJA BUZON: 0")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN REMESA: 0")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN MONTO PEN: 0")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN MONTO SOB: 0")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN MOTIVO: ")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN ASESOR: " & strNomVendedor)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     IN MONTO DEV: " & nImporte)

        intAutoriza = objConf.FP_Inserta_Aut_Transac(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), Session("USUARIO"), Session("NOMBRE_COMPLETO"), "", "", _
                                  drFila("PEDIV_NOMBRECLIENTE"), "", drFila("PAGOC_CODSUNAT"), drFila("PEDIN_NROPEDIDO"), 0, 2, 0, 0, 0, 0, 0, 0, "", strNomVendedor, nImporte)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT RESULTADO AUTORIZACION: " & Funciones.CheckStr(intAutoriza))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " ===FIN VALIDA AUTORIZACION===")

        If intAutoriza = 1 Then

            'Verificando en el modulo la nota de nota de credito  	
            Try
                Tipo_Documento = Session("Tipo_Documento")
                Session("Tipo_Documento") = Nothing
                Numero_Documento_Origen = Funciones.CheckStr(txtBolFact.Value).Trim
                strIdentifyLog = txtNotaCre.Value.Trim()
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Numero_Documento_Origen(Antes de transformar) : " & Numero_Documento_Origen)

                Dim arrayCorrOrigen As String()
                Dim Numero_Documento_Origen2 As String
                Dim Numero_Documento_Origen3 As String

                Dim strCorrelativoCanje As String = txtNotaCre.Value.Trim()
                If Not strCorrelativoCanje.StartsWith("NC") Then

                    Obtener_Guion = InStr(Numero_Documento_Origen, "-")
                    If Obtener_Guion > 0 Then
                        Numero_Documento_Origen2 = Numero_Documento_Origen.Substring(Obtener_Guion, Numero_Documento_Origen.Length - Obtener_Guion)
                        Obtener_Guion_2 = InStr(Numero_Documento_Origen2, "-")
                        If Obtener_Guion_2 > 0 Then
                            Numero_Documento_Origen3 = Numero_Documento_Origen2.Substring(Obtener_Guion_2, Numero_Documento_Origen2.Length - Obtener_Guion_2)
                            Obtener_Guion_3 = InStr(Numero_Documento_Origen3, "-")
                            If Obtener_Guion_3 > 0 Then
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Numero_Documento_Origen(Antes de transformar con 3 Guiones) : " & Numero_Documento_Origen)
                                arrayCorrOrigen = Numero_Documento_Origen.Split("-")
                                Numero_Documento_Origen = Obtener_serie_correlativo(Numero_Documento_Origen, "0", Tipo_Documento)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " salida Numero_Documento_Origen(Antes de transformar con 3 Guiones) : " & Numero_Documento_Origen)
                            Else
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Numero_Documento_Origen(Antes de transformar con 2 Guiones) : " & Numero_Documento_Origen)
                                arrayCorrOrigen = Numero_Documento_Origen.Split("-")
                                If arrayCorrOrigen(0).Length > 2 Then
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " salida Numero_Documento_Origen 2 guiones (No es correlativo Sunat)) : " & Numero_Documento_Origen)
                                Else
                                    Numero_Documento_Origen = Obtener_serie_correlativo(Numero_Documento_Origen, "1", Tipo_Documento)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " salida Numero_Documento_Origen(Antes de transformar con 2 Guiones) : " & Numero_Documento_Origen)
                                End If
                            End If
                        Else
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Numero_Documento_Origen(Antes de transformar con 1 Guion) : " & Numero_Documento_Origen)
                            arrayCorrOrigen = Numero_Documento_Origen.Split("-")
                            If arrayCorrOrigen(0).StartsWith("B") Or arrayCorrOrigen(0).StartsWith("F") Then
                                Numero_Documento_Origen = Obtener_serie_correlativo(Numero_Documento_Origen, "2", Tipo_Documento)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " salida Numero_Documento_Origen(Antes de transformar con 1 Guion) : " & Numero_Documento_Origen)
                            Else
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " salida Numero_Documento_Origen(Antes de transformar con 1 Guion (No es correlativo Sunat))) : " & Numero_Documento_Origen)
                            End If
                        End If
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Numero_Documento_Origen(Antes de transformar sin Guion) : " & Numero_Documento_Origen)
                        'Numero_Documento_Origen = Obtener_serie_correlativo(Numero_Documento_Origen, "3", Tipo_Documento)
                        msg_corr_error = "1"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " salida Numero_Documento_Origen(Antes de transformar sin Guion) : " & Numero_Documento_Origen)
                        Throw New ApplicationException(msg_corr_error)
                    End If
                End If

                Dim nroPedidoNC As Integer = Funciones.CheckInt(drFila("PEDIN_NROPEDIDO"))
                Dim idPagoNC As Integer = Funciones.CheckInt(drFila("PAGON_IDPAGO"))
                Dim dblImporte As Double = Funciones.CheckDbl(nImporte)

                '**** INICIO NOTA DE CANJE ****'
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INICIO VALIDACION NOTA DE CANJE")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [CONSULTA PEDIDO][IN NRO PEDIDO: " & nroPedidoNC & "]")
                dsPedido = objclsConsultaMsSap.ConsultaPedido(nroPedidoNC, "", "")
                Dim claseFactCanje As String
                If (Not dsPedido Is Nothing) AndAlso (dsPedido.Tables.Count > 0) AndAlso (dsPedido.Tables(0).Rows.Count > 0) Then
                    claseFactCanje = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [CONSULTA CLASE FACTURA][OUT CLASE FACTURA: " & claseFactCanje & "]")
                End If

                If claseFactCanje = ConfigurationSettings.AppSettings("ClaseNotaCanje") Then
                    Dim flagPagadoCanje As String = ConfigurationSettings.AppSettings("strFlagPagar")
                    Dim estadoPendienteCanje As String = ConfigurationSettings.AppSettings("idPendienteCanje")
                    Dim estadoExitoCanje As String = ConfigurationSettings.AppSettings("idExitoCanje")
                    Dim escenarioAnuladoCanje As String = ConfigurationSettings.AppSettings("idEscenarioAnu")
                    Dim escenarioPagarCanje As String = ConfigurationSettings.AppSettings("idEscenarioPag")
                    Dim dtConsultaCanje As DataTable

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [INICIO][ConsultaCanje][IN NRO PEDIDO: " & nroPedidoNC & "]")
                    '/*----------------------------------------------------------------------------------------------------------------*/
                    '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                    '/*----------------------------------------------------------------------------------------------------------------*/
                    'dtConsultaCanje = objclsConsultaMsSap.ConsultaCanje(nroPedidoNC, k_cod_rpta, k_msj_rpta)
                    dtConsultaCanje = objclsConsultaMsSap.ConsultaDevolucion(nroPedidoNC, k_cod_rpta, k_msj_rpta)
                    '/*----------------------------------------------------------------------------------------------------------------*/
                    '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                    '/*----------------------------------------------------------------------------------------------------------------*/
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [FIN][ConsultaCanje][OUT CODLOG: " & k_cod_rpta & "][OUT DESLOG: " & k_msj_rpta & "]")

                    If Not IsNothing(dtConsultaCanje) AndAlso dtConsultaCanje.Rows.Count > 0 Then
                        If (Funciones.CheckStr(dtConsultaCanje.Rows(0).Item("CANJV_ESTADO")) = estadoPendienteCanje And _
                            Funciones.CheckStr(dtConsultaCanje.Rows(0).Item("CANJV_ESCENARIO")) = escenarioPagarCanje) Or _
                            (Funciones.CheckStr(dtConsultaCanje.Rows(0).Item("CANJV_ESTADO")) = estadoExitoCanje And _
                            Funciones.CheckStr(dtConsultaCanje.Rows(0).Item("CANJV_ESCENARIO")) = escenarioAnuladoCanje) Then

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "         [INICIO][ActualizarEstadoNotaCanje][IN NRO PEDIDO: " & nroPedidoNC & "][IN FLAG: " & flagPagadoCanje & "]")
                            Dim actualizaEstado As String = objTrsConsultaMsSap.ActualizarEstadoNotaCanje(nroPedidoNC, flagPagadoCanje, k_cod_rpta, k_msj_rpta)
                            blnEsNotaCanje = True
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "         [FIN][ActualizarEstadoNotaCanje][OUT CODLOG: " & k_cod_rpta & "][OUT DESLOG: " & k_msj_rpta & "]")

                        Else
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [No se encontró registro del Canje en SAP]")
                            Session("msgDevolucion") = ConfigurationSettings.AppSettings("msgNoExisteCanjeSAP")
                            Response.Redirect("poolPagos.aspx", False)
                            Exit Sub
                        End If
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [No se encontró registro del Canje]")
                        Session("msgDevolucion") = ConfigurationSettings.AppSettings("msgNoExisteCanjeBD")
                        Response.Redirect("poolPagos.aspx", False)
                        Exit Sub
                    End If
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [CONSULTA CLASE FACTURA][OUT NOTA DE CANJE?: " & blnEsNotaCanje & "]")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " FIN VALIDACION NOTA DE CANJE")
                '**** FIN NOTA DE CANJE ****'

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " *****Parametro de Entrada al Stored (SSAPSU_DEVOLUCIONEFECTIVO) que trae los datos para enviar al WebService*********")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     PEDIN_NROPEDIDO : " & nroPedidoNC)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     PAGON_IDPAGO : " & idPagoNC)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Importe : " & dblImporte)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Numero_Documento_Origen : " & Numero_Documento_Origen)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Usuario : " & strUsuario)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Evaluando Parametro Opcional PAGOSWS")
                'PROY-29380 - RMR INI
                'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fecha Pago : " & DateTime.Parse(txtFechPago.Value).ToShortDateString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fecha Pago : " & DateTime.Parse(fechDocuPago).ToShortDateString())
                'PROY-29380 - RMR FIN
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fecha Devolucion : " & DateTime.Today.ToShortDateString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Tipo DOC: " & dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"))

                Dim TipoDoc As String
                TipoDoc = dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")

                If TipoDoc = "0007" Then

                    'If (DateTime.Parse(txtFechPago.Value).ToShortDateString() = DateTime.Today.ToShortDateString()) 'PROY-29380 - RMR
                    If (DateTime.Parse(fechDocuPago).ToShortDateString() = DateTime.Today.ToShortDateString()) Then 'PROY-29380 - RMR
                        strTipoDevol = "I"
                    Else
                        strTipoDevol = "E"
                    End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input Opcional P_DEVOL : " & strTipoDevol)
                End If

                dsValidaDevol = objclsConsultaMsSap.Verificar_Devolucion_Efectivo(nroPedidoNC, idPagoNC, dblImporte, Numero_Documento_Origen, strUsuario, strTipoDevol, NLOG, DLOG)

                If (Not dsValidaDevol Is Nothing) AndAlso (dsValidaDevol.Tables.Count > 0) AndAlso (dsValidaDevol.Tables(0).Rows.Count > 0) Then
                    Cod_PdV = Funciones.CheckStr(dsValidaDevol.Tables(0).Rows(0).Item("CODPDV"))
                    Fecha_Conta = Funciones.CheckStr(dsValidaDevol.Tables(0).Rows(0).Item("FECHA"))
                    CorrSunat_Origen = Funciones.CheckStr(dsValidaDevol.Tables(0).Rows(0).Item("REFORIGEN"))
                    CorrSunat_NC = Funciones.CheckStr(dsValidaDevol.Tables(0).Rows(0).Item("REFERENCIA"))
                    Monto = Funciones.CheckStr(dsValidaDevol.Tables(0).Rows(0).Item("MONTO"))
                    Usuario = Funciones.CheckStr(dsValidaDevol.Tables(0).Rows(0).Item("USUARIO"))
                    IdPedido_NC = Funciones.CheckStr(dsValidaDevol.Tables(0).Rows(0).Item("IDPEDIDO"))
                    IdPago_NC = Funciones.CheckStr(dsValidaDevol.Tables(0).Rows(0).Item("IDPAGO"))
                    Moneda = Funciones.CheckStr(dsValidaDevol.Tables(0).Rows(0).Item("MONEDA"))
                    Origen = Funciones.CheckStr(dsValidaDevol.Tables(0).Rows(0).Item("ORIGEN"))
                    Sociedad = Funciones.CheckStr(dsValidaDevol.Tables(0).Rows(0).Item("SOCIEDAD"))
                    strTipoDevol = Funciones.CheckStr(dsValidaDevol.Tables(0).Rows(0).Item("P_DEVOL"))
                Else
                    hidMensajeNC.Value = "Ha fallado el proceso de Devolución. Volver a intentar."
                    txtBolFact.Value = ""
                    txtMonto.Value = ""
                    msg_corr_error = "2"
                    'Response.Write("<script>alert('" & hidMensajeNC.Value & "')</script>")
                    'Exit Sub
                    Throw New ApplicationException(msg_corr_error)
                End If

            Catch ex As Exception

                'INI PROY-140126
                Dim MaptPath As String
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: GrabarDevolucion)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Exception: " & ex.ToString() & MaptPath)
                'FIN PROY-140126
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     msg_corr_error: " & msg_corr_error)

                txtBolFact.Value = ""
                txtMonto.Value = ""

                If msg_corr_error = "1" Then
                    Throw New ApplicationException(System.Configuration.ConfigurationSettings.AppSettings("MSG_Corr_Origen_Incorrecto"))
                ElseIf msg_corr_error = "2" Then
                    Throw New ApplicationException(hidMensajeNC.Value)
                Else
                    Throw New ApplicationException("Ha fallado el proceso de Devolución. Volver a intentar.")
                End If
            End Try

            If DLOG = "OK" Then
                If Not blnEsNotaCanje Then

                    ActualizarWL(Numero_Documento_Origen)
                    'enviar alWebService
                    If ConfigurationSettings.AppSettings("LanzarServicio") = "0" Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- INICIO PAGOSWS - DEVOLUCION----")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input Sociedad : " & Sociedad)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input Cod_PdV : " & Cod_PdV)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input Fecha_Conta : " & Fecha_Conta)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input CorrSunat_Origen : " & CorrSunat_Origen)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input CorrSunat_NC : " & CorrSunat_NC)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input Origen : " & Origen)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input Monto : " & Monto)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input Usuario : " & Usuario)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input IdPedido_NC : " & IdPedido_NC)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input IdPago_NC : " & IdPago_NC)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input CurrentUser : " & CurrentUser)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input CurrentTerminal : " & CurrentTerminal)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input Moneda : " & Moneda)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input Opcional P_DEVOL : " & strTipoDevol)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strTipoOfiVta: " & strTipoOfiVta)


                        Try
                            'PROY-31949-Inicio

                            If strTipoOfiVta = "MT" Then
                                objClsPagosWS.DevolucionEfectivo(Sociedad, Cod_PdV, Fecha_Conta, CorrSunat_Origen, CorrSunat_NC, Origen, Monto, "", IdPedido_NC, IdPago_NC, CurrentUser, CurrentTerminal, Moneda, strTipoDevol, k_cod_rpta, k_msj_rpta, k_id_transaccion)
                            Else
                                objClsPagosWS.DevolucionEfectivo(Sociedad, Cod_PdV, Fecha_Conta, CorrSunat_Origen, CorrSunat_NC, Origen, Monto, "", IdPedido_NC, IdPago_NC, CurrentUser, CurrentTerminal, Moneda, strTipoDevol, k_cod_rpta, k_msj_rpta, k_id_transaccion)
                            End If

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- FIN CONSULTA PAGOSWS - DEVOLUCION---")

                            'PROY-31949-Fin

                        Catch ex As Exception
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Fallo el Webservice DevolucionEfectivoWS")
                            'INI PROY-140126
                            Dim MaptPath As String
                            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                            MaptPath = "( Class : " & MaptPath & "; Function: GrabarDevolucion)"
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Exception: " & ex.ToString() & MaptPath)
                            'FIN PROY-140126

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     ID TRANSACCION: " & k_id_transaccion)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     ID MSJ DE RESPUESTA: " & k_msj_rpta)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     k_cod_rpta: " & k_cod_rpta)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin PAGOSWS")
                        End Try

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " No fallo el webservice DevolucionEfectivoWS")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     ID TRANSACCION: " & k_id_transaccion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     ID MSJ DE RESPUESTA: " & k_msj_rpta)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     k_cod_rpta: " & k_cod_rpta)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin PAGOSWS")
                    End If
                End If

                '********************************************************************************************************************
                'INICIO PROY-26366 FIDELIZACION Y CLARO PUNTOS FASE 2

                Try

                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1}", strIdentifyLog, "Inicio Devolucion de Claro Puntos"))
                    'PROY-26366 LOG - INI
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1}", strIdentifyLog, "NroPedidoDevolucion: ", NroPedidoDevolucion))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1}", strIdentifyLog, "strOficina: ", strOficina))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1}", strIdentifyLog, "Numero_Documento_Origen: ", Numero_Documento_Origen))
                    'PROY-26366 LOG - FIN
                    Dim claropuntos As Boolean = DevolverClaroPuntos(NroPedidoDevolucion, strOficina, Numero_Documento_Origen)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1} {2}", strIdentifyLog,"Fin Devolucion de Claro Puntos", claropuntos.ToString()))

                Catch e_cpuntos As Exception
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: GrabarDevolucion)"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Error Devolucion de Claro Puntos: Mensaje : " + e_cpuntos.Message.ToString() & MaptPath)
                    'FIN PROY-140126
                End Try

                'FIN PROY-26366 FIDELIZACION Y CLARO PUNTOS FASE 2

                'Inicio - Proceso de Cuadre de Caja Para la Devolucion
                Dim fechaCajas As String = ""
                Dim dsDatosPedido As DataSet
                'fechaCajas = Format(Now.Year, "0000").ToString.Trim & Format(Now.Month, "00").ToString.Trim & Format(Now.Day, "00").ToString.Trim
                'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- fechaCajas : " & fechaCajas.ToString)
                dsDatosPedido = objclsConsultaMsSap.ConsultaPedido(IdPedido_NC, "", "")
                strTelefonoDevolucion = IIf(dsDatosPedido.Tables(1).Rows(0).IsNull("DEPEV_NROTELEFONO"), "", dsDatosPedido.Tables(1).Rows(0).Item("DEPEV_NROTELEFONO")) 'INICIATIVA - 489
                If Not dsDatosPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO") Is Nothing Then
                    fechaCajas = CDate(dsDatosPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO")).ToString("yyyyMMdd")
                Else
                    fechaCajas = Format(Now.Year, "0000").ToString.Trim & Format(Now.Month, "00").ToString.Trim & Format(Now.Day, "00").ToString.Trim
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- fechaCajas : " & fechaCajas.ToString)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Inicio método : FP_InsertaEfectivo")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Oficina: " & strOficina)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Usuario: " & strUsuario)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Importe: - " & nImporte.ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     fecha: " & fechaCajas)
                Dim dFecha As Date
                Dim sFecha As String = Format(Now.Day, "00").ToString.Trim & "/" & Format(Now.Month, "00").ToString.Trim & "/" & Format(Now.Year, "0000").ToString.Trim
                If Not dsDatosPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO") Is Nothing Then
                    dFecha = CDate(dsDatosPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO"))
                    sFecha = dFecha.ToString("dd/MM/yyyy")
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Fecha Pedido : " & sFecha)
                Try
                    '27440 - INICIO
                    If (Session("flag_Switch") = 1) Then
                        nImporte = Convert.ToDouble(HidMontoEfectivo.Value)
                    End If
                    '27440 - FIN

                    objCaja.FP_InsertaEfectivo(strOficina, strUsuario, nImporte * (-1), sFecha)
                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Entro al Error de insertar efectivo")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Error " & ex.Message.ToString())
                End Try

                'objCaja.FP_InsertaEfectivo(strOficina, strUsuario, nImporte * (-1))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Fin método : FP_InsertaEfectivo")

                ''''1. Registro de pago: TABLA "TI_VENTAS_FACT"
                '''*Para las notas de crèdito el monto del saldo enviado debe ser 0.00

                Dim PEDIC_CLASEFACTURA As String = "" 'valor que sera reemplazado por una key si es una recarga virtual frecuente
                Dim resCajas As Integer
                Dim P_ID_TI_VENTAS_FACT As String = ""
                Dim P_MSGERR As String = ""
                Try
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio método : Consultar Pago Caja")

                    PEDIC_CLASEFACTURA = Funciones.CheckStr(dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"))

                    resCajas = objCaja.RegistrarPago(Session("ALMACEN"), fechaCajas, _
                                                   Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & Session("USUARIO")), 10), _
                                                   dsDatosPedido.Tables(0).Rows(0).Item("PEDIV_DESCCLASEFACTURA"), _
                                                   IdPedido_NC, txtNotaCre.Value.Trim(), _
                                                   Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & dsDatosPedido.Tables(0).Rows(0).Item("VENDEDOR")), 10), _
                                                   System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                                   PEDIC_CLASEFACTURA, _
                                                   dsDatosPedido.Tables(0).Rows(0).Item("DEPEN_NROCUOTA"), _
                                                   dsDatosPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), _
                                                   IIf(dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA") <> ConfigurationSettings.AppSettings("strTipDoc"), dsDatosPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), 0), _
                                                   Numero_Documento_Origen, _
                                                   System.Configuration.ConfigurationSettings.AppSettings("ESTADO_VENTA_FLUJOCAJA"), _
                                                   Funciones.CheckStr(Right(System.Net.Dns.GetHostName, 2)), _
                                                   System.Configuration.ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), _
                                                   P_ID_TI_VENTAS_FACT, P_MSGERR)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin método : RegistrarPago")
                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al tratar de ejecutar el método:  RegistrarPago")
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: GrabarDevolucion)"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Exception: " & ex.ToString() & MaptPath)
                    'FIN PROY-140126

                End Try

                Try
                    If P_ID_TI_VENTAS_FACT > 0 Then

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Código generado en la tabla TI_VENTAS_FACT=> " & P_ID_TI_VENTAS_FACT.ToString)

                        resCajas = objCaja.RegistrarPagoDetalle(P_ID_TI_VENTAS_FACT, fechaCajas, IdPedido_NC, _
                                                           txtNotaCre.Value.Trim(), PEDIC_CLASEFACTURA, _
                                                            System.Configuration.ConfigurationSettings.AppSettings("Medio_Pago_FlujoCaja_Devolucion"), _
                                                            "", _
                                                            dsDatosPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), _
                                                            System.Configuration.ConfigurationSettings.AppSettings("ESTADO_VENTA_FLUJOCAJA"), _
                                                            System.Configuration.ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), _
                                                            P_MSGERR)
                    End If
                    '-----------------------------------------------------------
                    'PROY 26210 EGSC BEGIN - ADD TIPI DEVOLUCION EFECTIVO:

                    '*************************************************************
                    'ELIMINAR LA PROGRAMACION DE CAMBIO DE PLAN - MOD: RMZ
                    Dim strMsj = ""
                    Dim telefono As String = IIf(dsDatosPedido.Tables(1).Rows(0).IsNull("DEPEV_NROTELEFONO"), "", dsDatosPedido.Tables(1).Rows(0).Item("DEPEV_NROTELEFONO"))
                    If (Me.EliminarProgramacionCambioPlan(telefono)) Then
                        strMsj = strMsj & System.Configuration.ConfigurationSettings.AppSettings("strMsjCambioPlan")
                        Response.Write("<script>alert('" & strMsj & "')</script>")
                    End If
                    '*************************************************************
                    Dim strFlagTipificacion = ConfigurationSettings.AppSettings("Cons_flag_tipificaciones") 'ADD: PROY 26210 - RMZ
                    If strFlagTipificacion = "1" Then
                        Me.TipificarGenerarDevolucionEnEfectivo(dsDatosPedido.Tables(0).Rows(0), dsDatosPedido.Tables(1).Rows(0))
                    Else
                    If P_MSGERR <> "" Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al registrar el Pago detalle.")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " P_MSGERR: " & P_MSGERR)
                    'PROY-26366 - FASE I - INICIO
                    Else
                        'PROY-26366 LOG - INI
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INI Tipificaciones_general_canje() ")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "dsDatosPedido: " & dsDatosPedido.ToString())
                        Tipificaciones_general_canje(dsDatosPedido, "")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Tipificaciones_general_canje() ")
                        'PROY-26366 LOG - FIN
                    'PROY-26366 - FASE I - FIN
                    End If
                    End If
                    'PROY 26210 EGSC BEGIN
                    '-----------------------------------------------------------
                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al tratar de ejecutar el método:  RegistrarPagoDetalle")
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: GrabarDevolucion)"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Exception: " & ex.ToString() & MaptPath)
                    'FIN PROY-140126

                End Try

                'Fin - Proceso de Cuadre de Caja Para la Devolucion
                '********************************************************************************************************************

                'Session("msgDevolucion") = ConfigurationSettings.AppSettings("msgDevolucionOK")
                'Response.Redirect("PoolPagos.aspx", False)


                '//PROY-140379 INI
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "############################ INICIO BLOQUE APPLE WATCH ############################")
                Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "############################ PROCESO DE DEVOLCUION APPLE WATCH ############################")
                Dim telefonoAP As String = "51" + IIf(dsDatosPedido.Tables(1).Rows(0).IsNull("DEPEV_NROTELEFONO"), "", dsDatosPedido.Tables(1).Rows(0).Item("DEPEV_NROTELEFONO"))
                Dim objConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
                Dim dtResultMotivo As DataTable
                Dim dr As System.Data.DataRow
                Dim StrDescripcionMotivo As String
                Dim mensaje As String = String.Empty
                Dim blnResEjecucion As Boolean = False
                Dim dtConsultaCanjeSW As DataTable
                Dim idx As Integer = 0

                dtConsultaCanjeSW = objclsConsultaMsSap.ConsultaCanje(IdPedido_NC, "", "")
              
                Dim StrCodigoMotivo As String = Funciones.CheckStr(dtConsultaCanjeSW.Rows(0).Item("CANJV_MOTIVO"))
                Dim TipoOper_Devol As String = Funciones.CheckStr(COM_SIC_Seguridad.ReadKeySettings.Key_TipoOper_Devol)
                Dim strMaterialEnt As String = Funciones.CheckStr(dtConsultaCanjeSW.Rows(0).Item("CANJV_ENT_COD_MAT"))

                Dim blnMatEntEncontrado As Boolean = False
                Dim strSplitMate() As String = Funciones.CheckStr(COM_SIC_Seguridad.ReadKeySettings.Key_MaterialPermitidoSW).Split("|")
                Dim strMatEncontrado As String = String.Empty
                For x As Integer = 0 To strSplitMate.Length - 1 Step 1
                    If strSplitMate(x).PadLeft(18, "0") = strMaterialEnt Then
                        blnMatEntEncontrado = True
                        Exit For
                    End If
                Next x

                If blnMatEntEncontrado Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== Equipo entrante iphone [Material entrante] [" & strMaterialEnt & "]")
                dtResultMotivo = objConsultaPvu.ConsultaMotivosCanje(TipoOper_Devol, "", 1, "", "")
                For Each dr In dtResultMotivo.Rows
                    If dtResultMotivo.Rows(idx).Item("MOTI_CODIGO") = StrCodigoMotivo Then
                        StrDescripcionMotivo = dtResultMotivo.Rows(idx).Item("MOTI_DESCRIP")
                        Exit For
                    End If
                    idx += 1
                Next

                        Dim strSerie_entrante As String = Funciones.CheckStr(dtConsultaCanjeSW.Rows(0).Item("CANJV_ENT_SERIE_MAT"))
                Dim strIccid_baja As String = String.Empty
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- TipoOper_Devol :" & TipoOper_Devol)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- strSerie_entrante :" & strSerie_entrante)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- StrCodigoMotivo :" & StrCodigoMotivo)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- StrDescripcionMotivo :" & StrDescripcionMotivo)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- strIccid_baja :" & strIccid_baja)

                blnResEjecucion = ejecutarConsultarProcesarIOT(telefonoAP, StrDescripcionMotivo, strSerie_entrante, strIccid_baja)

                If Not blnResEjecucion Then
                    '//mensaje = "Ha ocurrido un error al ejecutar la baja del servicio Apple Watch de la linea " & telefonoAP & " en IOTDB"
                        mensaje = "Ha ocurrido un error al invocar el servicio bssiotconsultasprocesos metodo: consultarProcesarIOT"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- blnResEjecucion :" & blnResEjecucion)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & mensaje)
                    Response.Write("<script language=jscript> alert('" + mensaje + "'); </script>")
                End If
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== Equipo entrante no es iphone : " & strMaterialEnt)
                End If
                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error en bloque Apple Watch")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error Message : " & ex.Message)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error stackTrace : " & ex.StackTrace)
                End Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "############################ FIN BLOQUE APPLE WATCH ############################")
                '//PROY-140379 FIN


                Try
                    'gdelasca Inicio Proy-9067

                    Dim vObClsTrsPvu As New COM_SIC_Activaciones.clsTrsPvu
                    Dim vCursorxIdPedidoActual As Object
                    Dim vCodRpta As String
                    Dim vMsjRpta As String
                    Dim DsCanjeEquipo As DataSet
                    Dim numPedin As Integer = Funciones.CheckInt(drFila("PEDIN_NROPEDIDO"))

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Metodo ConsultaEquipoCanje_XIdPedidoActual()")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp N_NRO_PEDIDO:" & numPedin)

                    DsCanjeEquipo = vObClsTrsPvu.ConsultaEquipoCanje_XIdPedidoActual(numPedin, vCursorxIdPedidoActual, vCodRpta, vMsjRpta)

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out CU_CONSULTA_CANJE:" & vCursorxIdPedidoActual)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Cod Rspta:" & vCodRpta)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Msj Respta:" & vMsjRpta)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Metodo ConsultaEquipoCanje_XIdPedidoActual()")


                    If Not IsNothing(DsCanjeEquipo) AndAlso DsCanjeEquipo.Tables(0).Rows.Count > 0 Then

                        Dim vCanjIdPadre As String
                        vCanjIdPadre = DsCanjeEquipo.Tables(0).Rows(0).Item("CANJI_ID_PADRE").ToString()

                        Dim vEntr_Serie As String
                        Dim vEntr_NroDoc As String
                        Dim vEntr_TipoDoc As String
                        vEntr_Serie = DsCanjeEquipo.Tables(0).Rows(0).Item("CANJV_ENT_SERIE_MAT").ToString()
                        vEntr_TipoDoc = DsCanjeEquipo.Tables(0).Rows(0).Item("CANJV_TIPODOCCLIENTE").ToString()
                        vEntr_NroDoc = DsCanjeEquipo.Tables(0).Rows(0).Item("CANJV_NRODOCCLIENTE").ToString()

                        Dim vCodMaterialSaliente As String
                        Dim vCodSerieSaliente As String
                        vCodMaterialSaliente = DsCanjeEquipo.Tables(0).Rows(0).Item("CANJV_SAL_COD_MAT").ToString()
                        vCodSerieSaliente = DsCanjeEquipo.Tables(0).Rows(0).Item("CANJV_SAL_SERIE_MAT").ToString()

                        Dim vSerieMat As String
                        Dim vNumDoc As Integer
                        Dim vTipoDoc As String
                        Dim vCursorxSeriTipoNum As Object
                        Dim vNumLog As String
                        Dim vDesLog As String
                        Dim DsSeriTipoNum As DataSet
                        Dim vNumPedidoOriginalBF As String

                        Dim vCursorOriginal As Object
                        Dim vCodRptaOrigen As Integer
                        Dim vMsjRptaOrigen As String
                        Dim DsPedidoOrigenBF As DataSet
                        Dim vDataOrigenBF As String
                        Dim vEstadoOrigen As String

                        Dim vCodRsptActualiza As String
                        Dim vMsjRsptaActualiza As String
                        Dim ObserPagoCanje As String
                        ObserPagoCanje = ConfigurationSettings.AppSettings("PagoCanje1").ToString() + " " + vCodSerieSaliente + " " + ConfigurationSettings.AppSettings("PagoCanje2").ToString() + " " + vCodMaterialSaliente

                        If vCanjIdPadre <> "" Then 'Tiene Padre

                            Dim vCursorxIdPadre As Object
                            Dim vCodRptaPadre As String
                            Dim vMsjRptaPadre As String
                            Dim DsPadre As DataSet

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Metodo ConsultaEquipoCanje_xIdPadre()")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp K_CANJI_ID:" & vCanjIdPadre)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp CU_CANJE:" & vCursorxIdPadre)

                            DsPadre = vObClsTrsPvu.ConsultaEquipoCanje_xIdPadre(vCanjIdPadre, vCursorxIdPadre, vCodRptaPadre, vMsjRptaPadre)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Cod Rspta:" & vCodRptaPadre)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Msj Respta:" & vMsjRptaPadre)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Metodo ConsultaEquipoCanje_xIdPadre()")

                            If Not IsNothing(DsPadre) AndAlso DsPadre.Tables(0).Rows.Count > 0 Then
                                vSerieMat = DsPadre.Tables(0).Rows(0).Item("CANJV_ENT_SERIE_MAT").ToString()
                                vNumDoc = DsPadre.Tables(0).Rows(0).Item("CANJV_NRODOCCLIENTE").ToString()
                                vTipoDoc = DsPadre.Tables(0).Rows(0).Item("CANJV_TIPODOCCLIENTE").ToString()

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Metodo ConsultaEquipoCanje_xSeriTipoNum()")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp K_SERIE:" & vSerieMat)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp K_NRO_DOC:" & vNumDoc)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp K_TIPO_DOC:" & vTipoDoc)

                                DsSeriTipoNum = vObClsTrsPvu.ConsultaEquipoCanje_xSeriTipoNum(vSerieMat, vTipoDoc, vNumDoc, vCursorxSeriTipoNum, vNumLog, vDesLog)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out CU_PEDIDO:" & vCursorxSeriTipoNum)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Num Log:" & vNumLog)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Msj Log:" & vDesLog)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Metodo ConsultaEquipoCanje_xSeriTipoNum()")

                                If Not IsNothing(DsSeriTipoNum) AndAlso DsSeriTipoNum.Tables(0).Rows.Count > 0 Then
                                    vNumPedidoOriginalBF = DsSeriTipoNum.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO").ToString()

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Metodo ConsultaEstRenoAnticipada()")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp N_NRO_PEDIDO:" & vNumPedidoOriginalBF)

                                    DsPedidoOrigenBF = vObClsTrsPvu.ConsultaEstRenoAnticipada(vNumPedidoOriginalBF, vCursorOriginal, vCodRptaOrigen, vMsjRptaOrigen)

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out P_CUR_SALIDA:" & vCursorOriginal)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Cod Rspt:" & vCodRptaOrigen)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Msj Rspt:" & vMsjRptaOrigen)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Metodo ConsultaEstRenoAnticipada()")

                                    If Not IsNothing(DsPedidoOrigenBF) AndAlso DsPedidoOrigenBF.Tables(0).Rows.Count > 0 Then
                                        vDataOrigenBF = DsPedidoOrigenBF.Tables(0).Rows(0).Item("SARN_NRO_PEDIDO").ToString()
                                        vEstadoOrigen = DsPedidoOrigenBF.Tables(0).Rows(0).Item("SARV_ESTADO").ToString()

                                        If vEstadoOrigen = "PAGADO" Then

                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Metodo ActualizarRenoAnticipada()")
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp N_NRO_PEDIDO:" & vDataOrigenBF)
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_ESTADO:" & "")
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_COD_EQUIPO:" & vCodSerieSaliente)
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_COD_MATERIAL:" & vCodMaterialSaliente)
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_USU_ACTUALIZACION:" & CurrentUser)
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp CL_OBSERVACIONES:" & ObserPagoCanje)

                                            vObClsTrsPvu.ActualizarRenoAnticipada(vDataOrigenBF, ConfigurationSettings.AppSettings("EstdDevEfecPag"), "", "", CurrentUser, ConfigurationSettings.AppSettings("ObsDevEfecPag"), vCodRsptActualiza, vMsjRsptaActualiza)

                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Cod Rspt:" & vCodRsptActualiza)
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Msj Rspt:" & vMsjRsptaActualiza)
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Metodo ActualizarRenoAnticipada()")
                                        ElseIf vEstadoOrigen = "PROCESADO" Then

                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Metodo ActualizarRenoAnticipada()")
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp N_NRO_PEDIDO:" & vDataOrigenBF)
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_ESTADO:" & "")
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_COD_EQUIPO:" & vCodSerieSaliente)
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_COD_MATERIAL:" & vCodMaterialSaliente)
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_USU_ACTUALIZACION:" & CurrentUser)
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp CL_OBSERVACIONES:" & ObserPagoCanje)

                                            vObClsTrsPvu.ActualizarRenoAnticipada(vDataOrigenBF, ConfigurationSettings.AppSettings("EstRollDevEfecti"), "", "", CurrentUser, ConfigurationSettings.AppSettings("ObsDevEfecPag"), vCodRsptActualiza, vMsjRsptaActualiza)

                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Cod Rspt:" & vCodRsptActualiza)
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Msj Rspt:" & vMsjRsptaActualiza)
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Metodo ActualizarRenoAnticipada()")

                                        End If
                                    End If
                                End If
                            End If

                        ElseIf vCanjIdPadre = "" Then

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Metodo ConsultaEquipoCanje_xSeriTipoNum()")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp K_SERIE:" & vEntr_Serie)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp K_TIPO_DOC:" & vEntr_TipoDoc)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp K_NRO_DOC:" & vEntr_NroDoc)

                            DsSeriTipoNum = vObClsTrsPvu.ConsultaEquipoCanje_xSeriTipoNum(vEntr_Serie, vEntr_TipoDoc, vEntr_NroDoc, vCursorxSeriTipoNum, vNumLog, vDesLog)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out CU_PEDIDO:" & vCursorxSeriTipoNum)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Num Log:" & vNumLog)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Msj Log:" & vDesLog)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Metodo ConsultaEquipoCanje_xSeriTipoNum()")

                            If Not IsNothing(DsSeriTipoNum) AndAlso DsSeriTipoNum.Tables(0).Rows.Count > 0 Then

                                vNumPedidoOriginalBF = DsSeriTipoNum.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Metodo ConsultaEstRenoAnticipada()")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp N_NRO_PEDIDO:" & vNumPedidoOriginalBF)

                                DsPedidoOrigenBF = vObClsTrsPvu.ConsultaEstRenoAnticipada(vNumPedidoOriginalBF, vCursorOriginal, vCodRptaOrigen, vMsjRptaOrigen)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out P_CUR_SALIDA:" & vCursorOriginal)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Cod Rspt:" & vCodRptaOrigen)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Msj Rspt:" & vMsjRptaOrigen)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Metodo ConsultaEstRenoAnticipada()")

                                If Not IsNothing(DsPedidoOrigenBF) AndAlso DsPedidoOrigenBF.Tables(0).Rows.Count > 0 Then
                                    vDataOrigenBF = DsPedidoOrigenBF.Tables(0).Rows(0).Item("SARN_NRO_PEDIDO")
                                    vEstadoOrigen = DsPedidoOrigenBF.Tables(0).Rows(0).Item("SARV_ESTADO")

                                    If vEstadoOrigen = "PAGADO" Then

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Metodo ActualizarRenoAnticipada()")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp N_NRO_PEDIDO:" & vDataOrigenBF)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_ESTADO:" & "")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_COD_EQUIPO:" & vCodSerieSaliente)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_COD_MATERIAL:" & vCodMaterialSaliente)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_USU_ACTUALIZACION:" & CurrentUser)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp CL_OBSERVACIONES:" & ObserPagoCanje)

                                        vObClsTrsPvu.ActualizarRenoAnticipada(vDataOrigenBF, ConfigurationSettings.AppSettings("EstdDevEfecPag"), "", "", CurrentUser, ConfigurationSettings.AppSettings("ObsDevEfecPag"), vCodRsptActualiza, vMsjRsptaActualiza)

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Cod Rspt:" & vCodRsptActualiza)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Msj Rspt:" & vMsjRsptaActualiza)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Metodo ActualizarRenoAnticipada()")
                                    ElseIf vEstadoOrigen = "PROCESADO" Then

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Metodo ActualizarRenoAnticipada()")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp N_NRO_PEDIDO:" & vDataOrigenBF)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_ESTADO:" & "")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_COD_EQUIPO:" & vCodSerieSaliente)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_COD_MATERIAL:" & vCodMaterialSaliente)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp V_USU_ACTUALIZACION:" & CurrentUser)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp CL_OBSERVACIONES:" & ObserPagoCanje)

                                        vObClsTrsPvu.ActualizarRenoAnticipada(vDataOrigenBF, ConfigurationSettings.AppSettings("EstRollDevEfecti"), "", "", CurrentUser, ConfigurationSettings.AppSettings("ObsDevEfecPag"), vCodRsptActualiza, vMsjRsptaActualiza)

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Cod Rspt:" & vCodRsptActualiza)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Msj Rspt:" & vMsjRsptaActualiza)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Metodo ActualizarRenoAnticipada()")

                                    End If

                                End If
                            End If
                        Else
                        End If
                    End If

                Catch ex As Exception

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE ERROR : " & ex.Message.ToString())

                End Try
                'gdelasca FIN Proy-9067

            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta del SP (SSAPSU_DEVOLUCIONEFECTIVO)")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Respuesta del SP (SSAPSU_DEVOLUCIONEFECTIVO)  : " & DLOG)
                Throw New ApplicationException("Ha fallado el proceso de Devolución. Volver a intentar.")
                Exit Sub
            End If
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "No esta Autorizado para realizar esta Transacción")
            Response.Write("<script language=jscript> alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador'); </script>")
        End If

        'INICIATIVA-489 - INICIO - Devolucion CBIO
        Dim objActivacionCBIO As New ClsActivacionCBIO
        Dim strCodResp As String
        Dim strMsgResp As String
        Dim strTelefono As String = strTelefonoDevolucion

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 Consulta Transaccion")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 Consulta origen linea: " & strTelefono)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 Codigo Transaccion Devolucion: " & Funciones.CheckStr(ConfigurationSettings.AppSettings("strDevolucionCBIO")))
            Dim arrListaNum As ArrayList = objActivacionCBIO.consultaTransaccion(strTelefono, Funciones.CheckStr(ConfigurationSettings.AppSettings("strDevolucionCBIO")), strCodResp, strMsgResp)

            objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 strCodResp: " & strCodResp)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 strMsgResp: " & strMsgResp)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 arrListaPrepago: " & IIf(arrListaNum Is Nothing, "No Existe", arrListaNum.Count))

            If (arrListaNum.Count > 0 And strCodResp = "0" And strMsgResp = "OK") Then
                Try
                    strCodResp = String.Empty
                    strMsgResp = String.Empty

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 Listar Request Desactivacion Contrato")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 Telefono: " & strTelefono)
                    Dim arrListaRequestDesactContrato As ArrayList = objActivacionCBIO.ListarRequestDesactivacionContrato(strTelefono, strCodResp, strMsgResp)

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 strCodResp: " & strCodResp)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 strMsgResp: " & strMsgResp)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 arrListaRequestDesactContrato: " & IIf(arrListaRequestDesactContrato Is Nothing, "No Existe", arrListaRequestDesactContrato.Count))

                    If (arrListaRequestDesactContrato.Count > 0 And strCodResp = "0" And strMsgResp = "OK") Then
                        Try
                            Dim oBWDesactivacionContratoCBIO As New BWDesactivacionContratoCBIO
                            Dim oBEContratoCBIO As New BEContratoCBIO
                            Dim strCodRespuesta As String
                            Dim strMsjRespuesta As String

                            oBEContratoCBIO.apellidosCliente = arrListaRequestDesactContrato(0).CONTV_APE_MAT & " " & arrListaRequestDesactContrato(0).CONTV_APE_PAT
                            oBEContratoCBIO.billCycleId = arrListaRequestDesactContrato(0).CICLOFACTURACION
                            oBEContratoCBIO.CacDac = arrListaRequestDesactContrato(0).CONTV_OFICINA_VENTA_DESC
                            oBEContratoCBIO.cicloFact = arrListaRequestDesactContrato(0).CICLOFACTURACION
                            oBEContratoCBIO.clienteCta = arrListaRequestDesactContrato(0).CONTV_COD_BSCS
                            oBEContratoCBIO.co_id = arrListaRequestDesactContrato(0).CONTN_NUMERO_CONTRATO
                            oBEContratoCBIO.coIdPub = arrListaRequestDesactContrato(0).SOACV_COID_PUB
                            oBEContratoCBIO.cs_id = arrListaRequestDesactContrato(0).CONTN_CUSTOMER_ID
                            oBEContratoCBIO.csIdPub = arrListaRequestDesactContrato(0).SOACV_CSID_PUB
                            oBEContratoCBIO.cuentaAsesor = String.Empty
                            oBEContratoCBIO.emailCliente = arrListaRequestDesactContrato(0).CORREO
                            oBEContratoCBIO.fecha_actual = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.mmm'Z'")
                            oBEContratoCBIO.fecha_ejecucion = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.mmm'Z'")
                            oBEContratoCBIO.flag_ND_PCS = "0"
                            oBEContratoCBIO.flag_occ_apadece = "0"
                            oBEContratoCBIO.idTipoCliente = "1" '"2"
                            oBEContratoCBIO.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff")
                            oBEContratoCBIO.montoPCS = txtMonto.Value
                            oBEContratoCBIO.motivo = Funciones.CheckStr(ConfigurationSettings.AppSettings("strCodigoMotivoDevolucionCBIO"))
                            oBEContratoCBIO.msisdn = strTelefono
                            oBEContratoCBIO.mto_fidelizacion = ""
                            oBEContratoCBIO.NDArea = "0"
                            oBEContratoCBIO.NDMotivo = "0"
                            oBEContratoCBIO.NDSubMotivo = Nothing
                            oBEContratoCBIO.nombresCliente = arrListaRequestDesactContrato(0).CONTV_NOMBRE
                            oBEContratoCBIO.numDoc = arrListaRequestDesactContrato(0).CONTV_NRO_DOC_CLIENTE
                            oBEContratoCBIO.p_observaciones = Funciones.CheckStr(ConfigurationSettings.AppSettings("strObservacionesDevolucionCBIO"))
                            oBEContratoCBIO.usuario_aplicacion = CurrentUser
                            oBEContratoCBIO.password_usuario = String.Empty
                            oBEContratoCBIO.poId = arrListaRequestDesactContrato(0).PLNV_PO_ID
                            oBEContratoCBIO.trace = "1"
                            oBEContratoCBIO.usuario_sistema = CurrentUser

                            oBWDesactivacionContratoCBIO.DesactivacionContratoCBIO(oBEContratoCBIO, strCodRespuesta, strMsjRespuesta)

                        Catch ex As Exception
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta del SERVICIO (DesactivacionContratoCBIO)")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE ERROR : " & ex.Message.ToString())
                        End Try

                    End If

                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta del SP (KG_SISACT_GENERAL_CBIO.SISACTSS_DESACTIV_CONTRATO)")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE ERROR : " & ex.Message.ToString())
                End Try

            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta del SP (KG_SISACT_GENERAL_CBIO.SISACTSS_TRANSACCION)")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE ERROR : " & ex.Message.ToString())
        End Try
        'INICIATIVA-489 - FIN - Devolucion CBIO

    End Sub

    Private Sub ActualizarWL(ByVal Numero_Documento_Origen As String)
        Dim Fact As String = ""
        Dim dsDatos As New DataSet
        Dim Doc_Sap As String = ""
        Dim Resultado As String = ""
        Dim NLOG As String = ""
        Dim DLOG As String = ""
        Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, "INICIO del método ActualizarWL")
            'Fact = txtBolFact.Value.Trim.ToString 'Factura Sunat
            Fact = Numero_Documento_Origen
            dsDatos = objConsultaMsSap.ConsultaPedidoXCorrelativo(Fact, Funciones.CheckStr(ConsultaPuntoVenta(Session("ALMACEN"))), NLOG, DLOG)
            'Doc_SapNuevo
            Dim Doc_SapNuevo As String = ""
            If Not dsDatos Is Nothing AndAlso dsDatos.Tables.Count > 0 AndAlso dsDatos.Tables(0).Rows.Count > 0 Then
                Doc_Sap = Funciones.CheckStr(dsDatos.Tables(0).Rows(0).Item(21))

                objFileLog.Log_WriteLog(pathFile, strArchivo, "Número de Pedido: " & Doc_Sap)
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicio Actualiza WhiteList")
                Resultado = objClsConsultaPvu.ActualizarWLxDocSap(Doc_Sap, "0")
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin Actualiza WhiteList")
                If Resultado = "0" Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " Actualizado OK")
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " Error al Actualizar")
                End If
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, "FIN del método ActualizarWL")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Error método ActualizarWL")
        End Try
    End Sub




    Private Function ValidaDatos() As Boolean
        Dim bresult As Boolean = True

        If txtFecConta.Value.Trim().Length = 0 Then
            Throw New ApplicationException("No ha ingresado la fecha de contabilización...!!")
        End If

        'If Not IsDate(txtFecConta.Value) Then
        '    Throw New ApplicationException("Error en el formato de la fecha...!!")
        'End If

        If txtBolFact.Value.Trim().Length = 0 Then
            Throw New ApplicationException("No ha ingresado el número de la factura/boleta...!!")
        End If
        If txtNotaCre.Value.Trim().Length = 0 Then
            Throw New ApplicationException("No ha ingresado el número de la nota de crédito...!!")
        End If
        If txtMonto.Value.Trim().Length = 0 Then
            Throw New ApplicationException("No ha ingresado el monto a aplicar...!!")
        End If
        If Not IsNumeric(txtMonto.Value) Then
            Throw New ApplicationException("Error en el ingreso del monto a aplicar...!!")
        End If
        Return bresult
    End Function

    Private Function ConsultaPuntoVenta(ByVal P_OVENC_CODIGO As String) As String
        Try
            Dim obj As New COM_SIC_Activaciones.clsConsultaMsSap
            Dim dsReturn As DataSet
            dsReturn = obj.ConsultaPuntoVenta(P_OVENC_CODIGO)
            If dsReturn.Tables(0).Rows.Count > 0 Then
                Return dsReturn.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA")
            End If
            Return Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function


    Private Sub cmdConsultaNC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConsultaNC.Click
        Try
            'Proy-27440- Inicio'
            If (Me.HidIntAutPos.Value = "1") Then
                If (ValidarCaja() = False) Then
                    Me.RegisterStartupScript("ValidarSwitch", "<script language=javascript>f_Cancelar();</script>")
                    Session("flag_Switch") = "0"

                    Exit Sub
                End If
            End If
            'Proy-27440- Fin'
            If ConsultaFormaPagoNotaCredito() = False Then

                'hidMensajeNC.Value = "No existe ninguna nota de crédito para el documento ingresado."
                'Response.Write("<script>alert('No existe ninguna nota de crédito para el documento ingresado.')</script>")
                txtBolFact.Value = ""
                txtMonto.Value = ""
                'Response.Write("<script>alert(hidMensajeNC.Value)</script>")
                Response.Write("<script>alert('" & hidMensajeNC.Value & "')</script>")
                'Return False
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function ConsultaFormaPagoNotaCredito() As Boolean
        Dim strDocumento As String = ""
        Dim sw As Double = True
        Try

            strDocumento = txtNotaCre.Value.Trim()
            strIdentifyLog = strDocumento

            If strDocumento.Length > 0 Then
                '**Valida si es Nota de Crédito o Nota de Canje
                If (strDocumento.ToString.Substring(0, 2) = "E7") Or (strDocumento.ToString.StartsWith("NC")) Then
                    If ConsultaPedidoXCorrelativoGenerado(strDocumento.Trim.ToString) = False Then
                        sw = False
                    End If
                Else
                    sw = False
                    hidMensajeNC.Value = "No existe ninguna nota de crédito para el documento ingresado."
                End If
            Else
                sw = False
                hidMensajeNC.Value = "No existe ninguna nota de crédito para el documento ingresado."
            End If

            Return sw
        Catch ex As Exception

        End Try
    End Function

    Private Function ConsultaPedidoXCorrelativoGenerado(ByVal strDocumento As String) As Boolean
        Try
            Dim dsDatos As DataSet
            Dim NLOG As String = ""
            Dim DLOG As String = ""
            Dim sw As Boolean = True
            'PEDIN_PEDIDOSAP = ""
            'PROY-27440'-Inicio
            Dim TipoDoc As String = ""
            Dim Switch As String = ""
            Dim strCodRpta As String = ""
            Dim strMsgRpta As String = ""
            Dim dsDatosOrigen As DataSet
            Dim pedidoNC As String = ""
            Dim pedidoOrigen As Integer = 0
            Dim dsPagos As DataSet
            Dim MontoTotal As Double = 0
            Dim MontoEfectivo As Double = 0
            'PROY-27440'-Fin

            Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Consulta de la Informacion para validar (SSAPSS_PEDIDOCORRELATIVO) ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Input CorrelativoNC : " & strDocumento.Trim.ToString)

            dsDatos = objConsultaMsSap.ConsultaPedidoXCorrelativo(strDocumento.Trim.ToString, Funciones.CheckStr(ConsultaPuntoVenta(Session("ALMACEN"))), NLOG, DLOG)
            If Not dsDatos Is Nothing Then
                If dsDatos.Tables(0).Rows.Count >= 1 Then
                    'Validaciòn Datos NC
                    If ValidaDatosNotaCredito(dsDatos) Then
                        '** guarda los datos de la nota de Credito.
                        txtBolFact.Value = Funciones.CheckStr(IIf(IsDBNull(dsDatos.Tables(0).Rows(0).Item("PEDIV_NROREFNCND")), "", dsDatos.Tables(0).Rows(0).Item("PEDIV_NROREFNCND")))
                        txtMonto.Value = Funciones.CheckStr(IIf(IsDBNull(dsDatos.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")), "", dsDatos.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")))

                        'PROY-29380 - RMR INI                        
                        fechDocuPago = Funciones.CheckStr(IIf(IsDBNull(dsDatos.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO")), "", Funciones.CheckDate(dsDatos.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO")).ToShortDateString()))
                        txtFechPago.Value = Funciones.CheckStr(IIf(IsDBNull(fechDocuPago), "", Format(fechDocuPago, "dd/MM/yyyy")))
                        'txtFechPago.Value = Funciones.CheckStr(IIf(IsDBNull(dsDatos.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO")), "", Format(dsDatos.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO"), "dd/MM/yyyy")))
                        'PROY-29380 - RMR FIN

                        'Tipo_Documento = Funciones.CheckStr(dsDatos.Tables(0).Rows(0).Item("PEDIC_TIPODOCCLIENTE"))
                        Session("Tipo_Documento") = Funciones.CheckStr(dsDatos.Tables(0).Rows(0).Item("PEDIC_TIPODOCCLIENTE"))
                        'strIdentifyLog = Funciones.CheckStr(IIf(IsDBNull(dsDatos.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")), "", dsDatos.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")))

                        'Proy-27440 - INICIO
                        pedidoNC = Funciones.CheckStr(IIf(IsDBNull(dsDatos.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")), "", dsDatos.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")))

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Consulta Informacion Pedido Origen (SSAPSS_CONSULTAPEDIDOORIGEN) ")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Input Pedido NC : " & pedidoNC)


                        Dim objProteccionMovil As New COM_SIC_Activaciones.clsProteccionMovil

                        objProteccionMovil.ConsultarPedidoOrigen(Funciones.CheckInt(pedidoNC), pedidoOrigen, strCodRpta, strMsgRpta)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Output pedidoOrigen : " & pedidoOrigen)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Output strCodRpta : " & strCodRpta)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Output strMsgRpta : " & strMsgRpta)

                        HidPedidoOrigenIot.Value = pedidoOrigen 'PROY-140379

                        If (pedidoOrigen > 0 And strCodRpta = 0) Then

                            dsDatosOrigen = objConsultaMsSap.ConsultaPedido(pedidoOrigen, "", "")
                            Dim dtDetallePagos As DataTable
                            Dim obPagos As New COM_SIC_Activaciones.clsConsultaMsSap
                            'Dim dtDetallePagos As DataTable = obPagos.ConsultarFormasPago(pedidoOrigen)
                            'txtFechPago.Value = Funciones.CheckStr(IIf(IsDBNull(dtDetallePagos.Rows(0).Item("DEPAD_FECHACREA")), "", Funciones.CheckDate(dtDetallePagos.Rows(0).Item("DEPAD_FECHACREA")).ToShortDateString()))

						If dsDatosOrigen.Tables(0).Rows.Count <= 0 Then
							hidMensajeNC.Value = "Error al consultar Pedido Origen"
							Return False
						End If

                            If dsDatosOrigen.Tables(0).Rows(0).Item("PEDIC_TIPOOFICINA") = ConfigurationSettings.AppSettings("constCodTipoCAC") Then
                                dtDetallePagos = obPagos.ConsultarFormasPago(pedidoOrigen)
                            Else
                                dtDetallePagos = obPagos.ConsultarFormasPagoDac(pedidoOrigen)
                            End If
							
						If dtDetallePagos.Rows.Count <= 0 Then
							hidMensajeNC.Value = "Error al consultar Fecha de Pago de Pedido Origen"
							Return False
						End If

                            'PROY-29380 - RMR INI
                            fechDocuPago = Funciones.CheckStr(IIf(IsDBNull(dtDetallePagos.Rows(0).Item("DEPAD_FECHACREA")), "", Funciones.CheckDate(dtDetallePagos.Rows(0).Item("DEPAD_FECHACREA")).ToShortDateString()))
                            'PROY-29380 - RMR FIN				
                            txtFechPago.Value = Funciones.CheckStr(IIf(IsDBNull(dtDetallePagos.Rows(0).Item("DEPAD_FECHACREA")), "", Funciones.CheckDate(dtDetallePagos.Rows(0).Item("DEPAD_FECHACREA")).ToShortDateString()))

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " FECHA PAGO :" & dsDatos.Tables(0).Rows(0).Item("PAGOD_FECHACONTA"))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " CLIEV_NOMBRE :" & dsDatosOrigen.Tables(0).Rows(0).Item("CLIEV_NOMBRE"))

                            txtNroPedido.Value = pedidoOrigen

                            txtNombCli.Value = Funciones.CheckStr(IIf(IsDBNull(dsDatosOrigen.Tables(0).Rows(0).Item("CLIEV_NOMBRE")), "", dsDatosOrigen.Tables(0).Rows(0).Item("CLIEV_NOMBRE")))

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Validar Switch POS ")

                            TipoDoc = Funciones.CheckStr(IIf(IsDBNull(dsDatos.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")), "", dsDatos.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")))
                            Switch = Me.HidIntAutPos.Value

                            'Inicio New
                            Dim objConsultaPagosPos As New COM_SIC_Activaciones.clsTransaccionPOS

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " CARGAR FORMAS DE PAGO - SICASS_DETAPAGO")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INPUT pedidoOrigen :" & pedidoOrigen)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INPUT TIPO PAGO :" & HidTipoPago.Value)

                            dsPagos = objConsultaPagosPos.ObtenerFormasDePagoTrans(pedidoOrigen, "", "", HidTipoPago.Value, strCodRpta, strMsgRpta)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " OUT strCodRpta :" & strCodRpta)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " OUT strMsgRpta :" & strMsgRpta)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " OUT TOTAL REGISTROS :" & dsPagos.Tables(0).Rows.Count)

                            If (dsPagos.Tables(0).Rows.Count > 0 And strCodRpta = 0) Then

                                Dim i As Integer
                                Dim monto As Decimal = 0
                                Dim formaPago As String = ""

                                For i = 0 To dsPagos.Tables(0).Rows.Count - 1
                                    monto = Funciones.CheckDbl(IIf(IsDBNull(dsPagos.Tables(0).Rows(i).Item("MONTO")), 0, dsPagos.Tables(0).Rows(i).Item("MONTO")))
                                    formaPago = Funciones.CheckStr(IIf(IsDBNull(dsPagos.Tables(0).Rows(i).Item("FORMA_PAGO")), "", dsPagos.Tables(0).Rows(i).Item("FORMA_PAGO"))).Trim

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " OUT " & i & " : FORMA DE PAGO :" & formaPago)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " OUT " & i & " : MONTO :" & monto)

                                    If formaPago = "EFECTIVO" Then
                                        MontoEfectivo = MontoEfectivo + monto
                                    End If
                                    MontoTotal = MontoTotal + monto
                                Next

                            End If

                            HidMontoEfectivo.Value = MontoEfectivo
                            HidMontoTotal.Value = MontoTotal

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " HidMontoEfectivo :" & HidMontoEfectivo.Value)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " HidMontoTotal :" & HidMontoTotal.Value)

                            'Fin New

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " TipoDoc :" & TipoDoc)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Switch :" & Switch)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " EstadoSwitch  :" & Session("flag_Switch"))

                            'If (TipoDoc = "0007" And Switch = "1" And DateTime.Parse(txtFechPago.Value).ToShortDateString() = DateTime.Today.ToShortDateString()) Then 'PROY-29380 - RMR
                            If (TipoDoc = "0007" And Switch = "1" And DateTime.Parse(fechDocuPago).ToShortDateString() = DateTime.Today.ToShortDateString()) Then 'PROY-29380 - RMR

                                Session("flag_Switch") = "1"
                                Me.RegisterStartupScript("ValidarSwitch", "<script language=javascript>f_validar_switch('" & Session("flag_Switch") & "');</script>")

                                Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS
                                Dim codRespuesta As String = ""
                                Dim msjRespuesta As String = ""
                                Me.RegisterStartupScript("LlenarFormasPago", "<script language=javascript>f_CargarFormasPago();</script>")

                            Else
                                Session("flag_Switch") = "0"
                                Me.RegisterStartupScript("ValidarSwitch", "<script language=javascript>f_validar_switch('" & Session("flag_Switch") & "');</script>")
                            End If


                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " EstadoSwitch  :" & Session("flag_Switch"))
                        'Proy-27440 - FIN

                    Else
                        sw = False
                        'hidMensajeNC.Value = "No existe ninguna nota de crédito para el documento ingresado."
                    End If
                Else
                    sw = False
                    hidMensajeNC.Value = "No existe ninguna nota de crédito para el documento ingresado."
                    'Response.Write("<script>alert('No existe ninguna nota de crédito para el documento ingresado.')</script>")
                    Return False
                End If
            Else
                sw = False
                hidMensajeNC.Value = "No existe ninguna nota de crédito para el documento ingresado."
                'Response.Write("<script>alert('No existe ninguna nota de crédito para el documento ingresado.')</script>")
                Return False
            End If

            Return sw
        Catch ex As Exception
            'Response.Write("<script>alert('" & Session("msgCaducidadRMP6") & "')</script>")
        End Try
    End Function

    Private Function ValidaDatosNotaCredito(ByVal dsdatos As DataSet) As Boolean
        Try
            Dim strTipoDoc As String = ""
            Dim strNroDoc As String = ""
            Dim strTipoDoc_origen As String = ""
            Dim strNroDoc_origen As String = ""
            Dim dsPedido As DataSet

            If Not dsdatos Is Nothing Then
                If dsdatos.Tables(0).Rows.Count >= 1 Then
                    'Validaciòn3:
                    '**Verifica no se haya usado con atenrioridad... ***'
                    If dsdatos.Tables(0).Rows(0).Item("PEDIC_ISFORMAPAGO") = 1 Then
                        hidMensajeNC.Value = "La nota de crédito ya fue utilizada en un pago anterior."
                        'Response.Write("<script>alert('La nota de crédito ya fue utilizada en un pago anterior.')</script>")
                        Return False
                        Exit Function
                    End If
                End If
            End If
            hidMensajeNC.Value = ""
            Return True
        Catch ex As Exception

        End Try
    End Function

    Private Function Obtener_serie_correlativo(ByVal K_CU_CORRELATIVOFE As String, ByVal tipo As String, ByVal tipoDocCliente As String) As String

        Try

            Dim arrayCorr As String()
            Dim SerieSunat As String = ""
            Dim strCorrSunat As String = ""
            Dim cadena As String = ""
            Dim tipoDoc_Sap As String = ""
            Dim tipoDoc_Sap_Origen As String = ""
            Dim Nro_Referencia_Sunat As String = ""

            If tipoDocCliente = System.Configuration.ConfigurationSettings.AppSettings("tipo_Docu_RUC") Then
                tipoDoc_Sap = "E1"
                tipoDoc_Sap_Origen = "F"
            Else
                tipoDoc_Sap = "E3"
                tipoDoc_Sap_Origen = "B"
            End If

            'tipo 0 es con 3 guiones
            'tipo 1 es con 2 guiones
            'tipo 2 es con 1 guion
            'tipo 3 es sin guion
            If tipo = "0" Then
                arrayCorr = CheckStr(K_CU_CORRELATIVOFE).Split("-")
                SerieSunat = (arrayCorr(2).ToString).Substring(1, (arrayCorr(2).ToString).Length - 1)
                Nro_Referencia_Sunat = tipoDoc_Sap & "-" & tipoDoc_Sap_Origen & Format(Funciones.CheckInt(SerieSunat), "000") & "-" & Format(Funciones.CheckInt(Funciones.CheckStr(arrayCorr(3))), "00000000")
            ElseIf tipo = "1" Then
                arrayCorr = CheckStr(K_CU_CORRELATIVOFE).Split("-")
                'caso  01-1234-12344556
                'caso  03-1234-12344556
                'caso  12-1234-12344556

                If Funciones.CheckStr(arrayCorr(0)) = "01" Or Funciones.CheckStr(arrayCorr(0)) = "03" Or Funciones.CheckStr(arrayCorr(0)) = "12" Then
                    Nro_Referencia_Sunat = K_CU_CORRELATIVOFE
                Else
                    SerieSunat = (arrayCorr(1).ToString).Substring(1, (arrayCorr(1).ToString).Length - 1)
                    Nro_Referencia_Sunat = tipoDoc_Sap & "-" & tipoDoc_Sap_Origen & Format(Funciones.CheckInt(SerieSunat), "000") & "-" & Format(Funciones.CheckInt(Funciones.CheckStr(arrayCorr(2))), "00000000")
                End If
            ElseIf tipo = "2" Then
                arrayCorr = CheckStr(K_CU_CORRELATIVOFE).Split("-")
                SerieSunat = (arrayCorr(0).ToString).Substring(1, (arrayCorr(0).ToString).Length - 1)
                Nro_Referencia_Sunat = tipoDoc_Sap & "-" & tipoDoc_Sap_Origen & Format(Funciones.CheckInt(SerieSunat), "000") & "-" & Format(Funciones.CheckInt(Funciones.CheckStr(arrayCorr(1))), "00000000")
            ElseIf tipo = "3" Then
                Nro_Referencia_Sunat = K_CU_CORRELATIVOFE
            End If

            Return Nro_Referencia_Sunat
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function DevolverClaroPuntos(ByVal NroPedidoDevolucion As String, _
                                        ByVal strOficina As String, _
                                        ByVal Numero_Documento_Origen As String) As Boolean

        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap

        Dim canjePuntos As DataTable
        Dim canjePuntosDet As DataTable
        Dim dsListarCanjePuntos As DataSet
        Dim dsDatosPedido As DataSet
        Dim dsDatos As New DataSet

        Dim drCanjePuntos As DataRow
        Dim drCanjePuntosDet As DataRow

        Dim Doc_Sap As String = String.Empty
        Dim Cod_Rpta As String = String.Empty
        Dim Msj_Rpta As String = String.Empty
        Dim Nro_Telefono As String = String.Empty
        Dim Serie_Equipo As String = String.Empty

        Dim Almacen As String = Funciones.CheckStr(ConsultaPuntoVenta(Session("ALMACEN")))

        Dim i As Integer
        Dim Resultado_Dev As Boolean = False

        Dim FLAG_CANJE As String = String.Empty
        Dim USUARIO_CANJE As String = String.Empty
        Dim DOCUMENTO_SAP As String = String.Empty
        Dim NRO_LINEA As String = String.Empty
        Dim ID_CCLUB As String = String.Empty
        Dim NUM_DOC As String = String.Empty
        Dim PUNTOS_USADOS As String = String.Empty
        Dim SOLES_DESCUENTO As String = String.Empty
        Dim TIPO_DOC As String = String.Empty
        Dim SERIE_ARTICULO As String = String.Empty

        Dim CONS_FLAG_CANJE As String = ConfigurationSettings.AppSettings("CONS_FLAG_CANJE")

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1}", strIdentifyLog, "Inicio Devolucion de Claro Puntos"))

        Try

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1}", strIdentifyLog, " INICIO - Obtener el Nro de Pedido Original de la Venta"))

            dsDatos = objConsultaMsSap.ConsultaPedidoXCorrelativo(Numero_Documento_Origen, Almacen, Cod_Rpta, Msj_Rpta)

            If Not dsDatos Is Nothing AndAlso dsDatos.Tables.Count > 0 AndAlso dsDatos.Tables(0).Rows.Count > 0 Then
                Doc_Sap = Funciones.CheckStr(dsDatos.Tables(0).Rows(0).Item(21))
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1} : {2}", strIdentifyLog, " FIN - Obtener el Nro de Pedido Original de la Venta", Doc_Sap))


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1}", strIdentifyLog, " INICIO - Consultar Datos del Pedido de Devolucion"))

            dsDatosPedido = objConsultaMsSap.ConsultaPedido(NroPedidoDevolucion, "", "")

            If dsDatosPedido.Tables.Count > 0 AndAlso dsDatosPedido.Tables(0).Rows.Count > 0 Then

                Serie_Equipo = Funciones.CheckStr(dsDatosPedido.Tables(1).Rows(0).Item("SERIC_CODSERIE"))
                Nro_Telefono = Funciones.CheckStr(dsDatosPedido.Tables(1).Rows(0).Item("DEPEV_NROTELEFONO"))

            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1} : {2}", strIdentifyLog, " Serie del Equipo", Serie_Equipo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1} : {2}", strIdentifyLog, " Numero de Telefono", Nro_Telefono))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1}", strIdentifyLog, " FIN - Consultar Datos del Pedido de Devolucion"))


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1}", strIdentifyLog, "INICIO - Verificar si existen Descuento Claro Puntos"))

            dsListarCanjePuntos = objCajas.ListarCanjePuntosDet(Doc_Sap)

            If Not dsListarCanjePuntos Is Nothing And dsListarCanjePuntos.Tables.Count > 0 Then
                canjePuntos = dsListarCanjePuntos.Tables(0)
                canjePuntosDet = dsListarCanjePuntos.Tables(1)

                For i = 0 To canjePuntos.Rows.Count - 1
                    drCanjePuntos = canjePuntos.Rows(i)
                Next

                If Funciones.CheckStr(drCanjePuntos.Item("FLAG_CANJE")) = CONS_FLAG_CANJE Then

                    For i = 0 To canjePuntosDet.Rows.Count - 1

                        drCanjePuntosDet = canjePuntosDet.Rows(i)

                        If Serie_Equipo = Funciones.CheckStr(drCanjePuntosDet.Item("SERIE_ARTICULO")) Then

                            Resultado_Dev = devolucionDescuentoEquipo(drCanjePuntos, drCanjePuntosDet, strOficina, Nro_Telefono)

                            If Resultado_Dev = True Then

                                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1} : {2}", strIdentifyLog, "Se realizo la devolucion de Claro puntos de la serie", Serie_Equipo))
                            Else

                                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1} : {2}", strIdentifyLog, "No se realizo la devolucion de Claro puntos de la serie", Serie_Equipo))
                            End If
                        End If
                    Next

                Else

                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1} : {2}", strIdentifyLog, "No se realio la devolucion de Claro Puntos, FLAG_CANJE no coincide", Doc_Sap))

                End If

            Else

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1} : {2}", strIdentifyLog, "No existen ClaroPuntos con el numero pedido", Doc_Sap))

            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1} : {2}", strIdentifyLog, "Fin - Devolucion de Claro Puntos", Doc_Sap))

        Catch ex As Exception

            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: DevolverClaroPuntos)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1} : {2}", strIdentifyLog, "No se Realizado la devolucion de los ClaroPuntos", ex.ToString()) & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1} : {2}", strIdentifyLog, "Fin - Devolucion de Claro Puntos", Doc_Sap))

            Resultado_Dev = False

        End Try

        Return Resultado_Dev

    End Function

    Public Function devolucionDescuentoEquipo(ByVal drcanjePuntos As DataRow, _
                                              ByVal drcanjePuntosDet As DataRow, _
                                              ByVal PuntoVenta As String, _
                                              ByVal Nro_Telefono As String) As Boolean

        Dim blnRespuesta As Boolean

        Dim Proceso As String = ConfigurationSettings.AppSettings("CONST_VTA_RENOVACION")
        Dim Aplicacion As String = ConfigurationSettings.AppSettings("CONS_APLICACION")
        Dim Cod_Rpta As String = String.Empty
        Dim Msj_Rpta As String = String.Empty
        Dim IdVenta As String = drcanjePuntos.Item("DOCUMENTO_SAP")
        Dim Linea As String = Nro_Telefono
        Dim TipoDoc As String = drcanjePuntos.Item("ID_CCLUB")
        Dim NumDoc As String = drcanjePuntos.Item("NUM_DOC")
        Dim PtosDevueltos As String = drcanjePuntosDet.Item("PUNTOS_USADOS")
        Dim SolesDevuelto As String = drcanjePuntosDet.Item("SOLES_DESCUENTO")

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} : {2}", strIdentifyLog, "Inicio CanjeDescuentoEquipo()", ConfigurationSettings.AppSettings("ConsDevolucionClaroPuntosWS")))

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} : {2}", strIdentifyLog, "Id de Venta", IdVenta))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} : {2}", strIdentifyLog, "Numero Linea", Linea))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} : {2}", strIdentifyLog, "Tipo Documento", TipoDoc))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} : {2}", strIdentifyLog, "Numero Documento", NumDoc))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} : {2}", strIdentifyLog, "Puntos Devueltos", PtosDevueltos))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} : {2}", strIdentifyLog, "Soles Devueltos", SolesDevuelto))

        Try

            Dim _devolucionClaroPuntos As New DevolucionClaroPuntosWS.DevolucionPuntosClaroClubWSService

            _devolucionClaroPuntos.Url = ConfigurationSettings.AppSettings("ConsDevolucionClaroPuntosWS")
            _devolucionClaroPuntos.Timeout = Convert.ToInt32((ConfigurationSettings.AppSettings("ConsDevolucionClaroPuntosWSTimeOut")))


            Dim devolverClaroPuntos As New DevolucionClaroPuntosWS.devolverPuntosClaroClubRequest
            Dim Respuesta As New DevolucionClaroPuntosWS.devolverPuntosClaroClubResponse

            Dim auditRequestCP As New DevolucionClaroPuntosWS.auditRequestType


            auditRequestCP.usuarioAplicacion = CurrentUser
            auditRequestCP.nombreAplicacion = Aplicacion
            auditRequestCP.ipAplicacion = Request.ServerVariables("REMOTE_HOST")
            auditRequestCP.idTransaccion = IdVenta

            Dim idSolicitud As String = Now.ToString("yyyyMMddHHmmss")

            devolverClaroPuntos.auditRequest = auditRequestCP

            devolverClaroPuntos.idSolicitud = idSolicitud
            devolverClaroPuntos.puntoVenta = PuntoVenta
            devolverClaroPuntos.idVenta = IdVenta
            devolverClaroPuntos.proceso = Proceso
            devolverClaroPuntos.linea = Linea
            devolverClaroPuntos.tipoDoc = TipoDoc
            devolverClaroPuntos.numDoc = NumDoc
            devolverClaroPuntos.ptosDevueltos = PtosDevueltos
            devolverClaroPuntos.solesDevueltos = SolesDevuelto


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} : {2}", strIdentifyLog, "Usuario", auditRequestCP.usuarioAplicacion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} : {2}", strIdentifyLog, "Nombre Aplicacion", auditRequestCP.nombreAplicacion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} : {2}", strIdentifyLog, "IP de Aplicacion", auditRequestCP.ipAplicacion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} : {2}", strIdentifyLog, "Id de Transaccion", auditRequestCP.idTransaccion))


            Respuesta = _devolucionClaroPuntos.devolverPuntosClaroClub(devolverClaroPuntos)

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} : {2}", strIdentifyLog, "Cod_Respuesta", Respuesta.auditResponse.codigoRespuesta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} : {2}", strIdentifyLog, "Msj_Respuesta", Respuesta.auditResponse.mensajeRespuesta))

            If Respuesta.auditResponse.codigoRespuesta = "0" Then
                blnRespuesta = True
            ElseIf Respuesta.auditResponse.codigoRespuesta = "-1" Then
                blnRespuesta = False
            Else
                Throw New Exception("Ocurrió un error al realizar el canje de los Puntos Claro Club.")
            End If
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: devolucionDescuentoEquipo)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} {2}", strIdentifyLog, "Mensaje_Error : ", ex.Message.ToString()) & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} {2}", strIdentifyLog, "Mensaje_Pila : ", ex.StackTrace.ToString()))
            Throw New Exception("Ocurrió un error al realizar el canje de los Puntos Claro Club.")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} {1}", strIdentifyLog, "Fin CanjeDescuentoEquipo()"))
        End Try

        Return blnRespuesta
    End Function

    'PROY-26366-IDEA-34247 FASE 1 - INICIO
    Function Tipificaciones_general_canje(ByVal dsPedido As DataSet, ByVal Var_Correlativo6 As String)

        ' Creacion TIPIFICACION - INTERACCION
        Dim dsCliente As DataSet

        Dim objIdContacto As String
        Dim contactoId As Int64
        Dim nroDocCliente As String
        Dim nombreCliente As String
        Dim apellidoCliente As String
        Dim clarifyTelef As String
        Dim strEmpleado As String = Session("strUsuario")
        Dim oInteraccion As New COM_SIC_INActChip.Interaccion
        Dim oPlantilla As New COM_SIC_INActChip.PlantillaInteraccion
        Dim dtConsultaCanje As DataSet
        Dim objclsConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim vObClsTrsPvu As New COM_SIC_Activaciones.clsTrsPvu
        Dim VobClsClarify As New COM_SIC_Activaciones.clsClarify
        Dim oInteraccion_Clarify As New COM_SIC_Activaciones.clsDatosInteraccion
        Dim ResultadoCLarify As String
        Dim idWSClarify, MensajeWSClarify As String


        Dim flagInter As String
        Dim mensajeInter As String
        Dim idInteraccion As String
        Dim strParametros As String
        Dim strNroPedido As String
        '--IDENTIFICADOR DEL LOG :-----------------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim strIdentifyLog As String = "Tipificaciones"
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim vCursorxIdPedidoActual As Object
        Dim vCodRpta As Integer
        Dim vMsjRpta As String
        Dim strMotivoCanje As String
        Dim StrTipoCliente As String

        'validar canje
        Try
            '----------------------------------------------------------------------------------------------------------------
            '--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
            '----------------------------------------------------------------------------------------------------------------  
            Dim telefonosVenta As String = ""
            Dim telefVenta As String = ""
            Dim listaTelefonosVenta As String()
            Dim StrMonto As String = ""
            Dim StrPrecioVenta As String = ""
            Dim StrPrecio_Prepago As String = ""
            Dim StrImei_Devolver As String = ""
            Dim StrImei_Nuevo As String = ""
            Dim strTipoMaterial As String = "" 'TV0004:CHIP / TV0003:EQUIPO

            ' INICIO - Seleccion de Lineas (NUMERO TELEFONICO) en el detalle del Pedido dsPedido.Tables(1)
            If Not dsPedido Is Nothing Then
                If dsPedido.Tables(1).Rows.Count > 0 Then
                    For Each itemDetalle As DataRow In dsPedido.Tables(1).Rows
                        Dim varAgregar As Int16 = 0
                        telefVenta = Funciones.CheckStr(itemDetalle("DEPEV_NROTELEFONO"))

                        If telefonosVenta <> "" Then
                            listaTelefonosVenta = telefonosVenta.Split(";")
                            If listaTelefonosVenta.Length > 0 Then
                                For idx As Integer = 0 To listaTelefonosVenta.Length - 1
                                    If listaTelefonosVenta(idx) = telefVenta Then
                                        varAgregar = 1
                                        Exit For
                                    End If
                                Next
                            End If
                        Else
                            telefonosVenta = telefVenta
                            varAgregar = 1
                        End If

                        If varAgregar = 0 Then
                            telefonosVenta = telefonosVenta & ";" & telefVenta
                        End If
                    Next
                End If
            End If

            listaTelefonosVenta = telefonosVenta.Split(";")
            'FIN - Seleccion de Lineas (NUMERO TELEFONICO) en el detalle del Pedido dsPedido.Tables(1)

            'INICIO - Grabar la tipificacion por cada Linea en "listaTelefonosVenta"
            For idx As Integer = 0 To listaTelefonosVenta.Length - 1
                clarifyTelef = listaTelefonosVenta(idx)
                'Next
                'clarifyTelef = Funciones.CheckStr(dsPedido.Tables(1).Rows(0).Item("DEPEV_NROTELEFONO"))
                '----------------------------------------------------------------------------------------------------------------
                '--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                '----------------------------------------------------------------------------------------------------------------  

            clarifyTelef = Funciones.CheckStr(dsPedido.Tables(1).Rows(0).Item("DEPEV_NROTELEFONO"))
            strNroPedido = Funciones.CheckStr(dsPedido.Tables(0).Rows(0)("PEDIN_NROPEDIDO"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Metodo ConsultaEquipoCanje_XIdPedidoActual_2()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp N_NRO_PEDIDO:" & strNroPedido)

                '----------------------------------------------------------------------------------------------------------------
                '--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                '---------------------------------------------------------------------------------------------------------------- 
                'dtConsultaCanje = vObClsTrsPvu.ConsultaEquipoCanje_XIdPedidoActual_2(strNroPedido, vCursorxIdPedidoActual, vCodRpta, vMsjRpta)
                dtConsultaCanje = vObClsTrsPvu.ConsultaDevolucion(strNroPedido, vCursorxIdPedidoActual, vCodRpta, vMsjRpta)
                '----------------------------------------------------------------------------------------------------------------
                '--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                '---------------------------------------------------------------------------------------------------------------- 

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out CU_CONSULTA_CANJE:" & vCursorxIdPedidoActual)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Cod Rspta:" & vCodRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Msj Respta:" & vMsjRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Metodo ConsultaEquipoCanje_XIdPedidoActual_2()")

            If Not IsNothing(dtConsultaCanje) AndAlso dtConsultaCanje.Tables(0).Rows.Count > 0 Then

                ' Cadena de Parametros
                strParametros = ""
                strParametros = strParametros & clarifyTelef & "|"
                strParametros = strParametros & "" & "|"
                strParametros = strParametros & "" & "|"
                strParametros = strParametros & 1

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo ConsultaCliente()")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametros : " & strParametros)

                ' Consulta de Id Cliente Clarify
                Dim oTipificacion As New COM_SIC_INActChip.clsTipificacion
                'PROY-26366 LOG -  INI
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "clarifyTelef: " & clarifyTelef)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "contactoId: " & contactoId)
                dsCliente = oTipificacion.ConsultaCliente(clarifyTelef, "", contactoId, CInt("1"), "", "")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "OUT dsCliente: " & dsCliente.Tables(0).Rows.Count)
                'PROY-26366 LOG -  FIN
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo ConsultaCliente()")

                If Not IsNothing(dsCliente) Then
                    If dsCliente.Tables(0).Rows.Count > 0 Then
                        objIdContacto = dsCliente.Tables(0).Rows(0).Item("OBJID_CONTACTO")
                        nroDocCliente = dsCliente.Tables(0).Rows(0).Item("NRO_DOC")
                        nombreCliente = dsCliente.Tables(0).Rows(0).Item("NOMBRES")
                        apellidoCliente = dsCliente.Tables(0).Rows(0).Item("APELLIDOS")

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Contacto: " & objIdContacto)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nro Doc. Cliente: " & nroDocCliente)

                        strMotivoCanje = dtConsultaCanje.Tables(0).Rows(0).Item("CANJV_MOTIVO").ToString()
                        StrTipoCliente = dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA").ToString()

                        Dim NroGrupoTipificacion As String
                        Dim IntNro As Integer = 1 
                        NroGrupoTipificacion = ConfigurationSettings.AppSettings("StrKeyTipificacionDevolucionEfecGeneral") ''987165811010

                        oInteraccion_Clarify.TELEFONO = clarifyTelef

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo consultarDatosUsuario()")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametros : " & oInteraccion_Clarify.TELEFONO.ToString())
                        ResultadoCLarify = VobClsClarify.consultarDatosUsuario(oInteraccion_Clarify, idWSClarify, MensajeWSClarify)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo consultarDatosUsuario()")

                        If ResultadoCLarify = "" Then
                            StrTipoCliente = "0"
                        Else
                            ResultadoCLarify = ResultadoCLarify.ToUpper()
                            If (ResultadoCLarify.IndexOf("PREPAGO")) >= 0 Then
                                StrTipoCliente = "02"
                            Else
                                StrTipoCliente = "01"
                            End If

                        End If

     
                        Dim StrNotas As String = ""
                        Dim DsPedidoOrigen As DataSet
                        Dim vCursorxSeriTipoNum As Object
                        Dim vNumLog As String
                        Dim vDesLog As String
                        Dim vNumPedidoOriginal As String
                        Dim strNroContrato As String
                        Dim DsContrato As DataSet
                        Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu

                        Dim DsMAterial As DataSet
                        Dim P_COD_RESP As String = ""
                        Dim P_MSG_RESP As String = ""

                        ' PARAMETROS DE RETORNO DE CONSULTA PEDIDO PVU
                        Dim P_CODIGO_RESPUESTA As String = ""
                        Dim P_MENSAJE_RESPUESTA As String = ""
                        Dim C_VENTA As DataTable
                        Dim C_VENTA_DET As DataTable

                        Dim K_COD_RESPUESTA As String
                        Dim K_MSJ_RESPUESTA As String
                        Dim K_ID_TRANSACCION As String

                        Dim p_limpiador As Integer = 0
                        Dim C_VENTA_Prepago As DataSet

                        'FIN PARAMETROS DE RETORNO DE CONSULTA PEDIDO PVU

                            '----------------------------------------------------------------------------------------------------------------
                            '--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                            '---------------------------------------------------------------------------------------------------------------- 
                            'Recorrer el dataSet dtConsultaCanje (ssapss_consultadevolucion) para verificar y recorrer los registros correspondientes 
                            'a la Linea Telefonica a Tipificar (obtener si es equipo, pack, o chip)

                            'dsPedido : PKG_CONSULTA.SSAPSS_PEDIDO
                            'dtConsultaCanje = vObClsTrsPvu.ConsultaDevolucion(strNroPedido) -- pkg_canje_jr.ssapss_consultadevolucion

                            Dim dsDatosVenta As New DataSet 're Ubicado

                            'Dim strTipoMaterial As String = "" 'TV0004:CHIP / TV0003:EQUIPO
                            Dim StrPrecioVentaChip As String = "" 'Precio Venta CHIP
                            Dim StrIccid_Devolver As String = "" 'ICCID
                            Dim StrPrecio_PrepagoChip As String = "" 'Precio Prepago CHIP
                            Dim StrModelo_DevolverEquipo As String = "" 'Modelo del equipo

                            Dim registrosPorLinea As Integer
                            Dim expresionFiltro As String 'condicion para filtrar dataTables
                            Dim dsPedidoPorLinea() As DataRow 'DataTable filtrado
                            Dim dtConsultaCanjePorLinea() As DataRow 'DataTable filtrado
                            Dim strSerieLinea As String
                            Dim isChipEquipoOrPack As String = ""

                            Dim isDatosInteraccionCargados As Boolean = False
                            Dim isParametrosCargados As Boolean = False

                            expresionFiltro = "DEPEV_NROTELEFONO = " & clarifyTelef
                            dsPedidoPorLinea = dsPedido.Tables(1).Select(expresionFiltro) 'DataSet filtrado con los registros de una misma linea. (chip, equipo, pack)

                            registrosPorLinea = dsPedidoPorLinea.Length 'Cantidad de registros de una misma linea

                            If registrosPorLinea = 0 Then
                                'Error, debe haber por lo menos un registro
                            ElseIf (registrosPorLinea = 1) Then
                                isChipEquipoOrPack = ""
                                'corresponde a un CHIP o un EQUIPO
                            ElseIf (registrosPorLinea = 2) Then
                                'corresponde a un PACK
                                isChipEquipoOrPack = "PACK"
                            Else
                                'Error, no pueden haber mas de dos registros para una misma liena
                            End If

                            For i As Integer = 0 To dsPedidoPorLinea.GetUpperBound(0)
                                strSerieLinea = dsPedidoPorLinea(i)("SERIC_CODSERIE")
                                'Next
                                expresionFiltro = "CANJV_ENT_SERIE_MAT = " & strSerieLinea
                                dtConsultaCanjePorLinea = dtConsultaCanje.Tables(0).Select(expresionFiltro)
                                '----------------------------------------------------------------------------------------------------------------
                                '--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                                '---------------------------------------------------------------------------------------------------------------- 

                        Dim StrCampana As String = ""
                        Dim StrDescuentoEquipo As String = ""
                        Dim StrPlan As String = ""
                        Dim StrNroConstrato As String = ""
                        Dim StrNroReclamoResolucion As String = ""
                        Dim StrPedido As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0)("PEDIN_NROPEDIDO"))
                        Dim strDesTipOpe As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0)("PEDIV_DESCTIPOOPERACION"))
                        Dim sTipoOperacion As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION"))
                        Dim sTipoVenta As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA"))
                                '/*----------------------------------------------------------------------------------------------------------------*/
                                '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                '/*----------------------------------------------------------------------------------------------------------------*/
                                Dim vSerieMat As String = Funciones.CheckStr(dtConsultaCanjePorLinea(0)("CANJV_ENT_SERIE_MAT"))
                                Dim vTipoDoc As String = Funciones.CheckStr(dtConsultaCanjePorLinea(0).Item("CANJV_TIPODOCCLIENTE"))
                                Dim vNumDoc As String = Funciones.CheckStr(dtConsultaCanjePorLinea(0).Item("CANJV_NRODOCCLIENTE"))
                                'Dim vSerieMat As String = Funciones.CheckStr(dtConsultaCanje.Tables(0).Rows(0)("CANJV_ENT_SERIE_MAT"))
                                'Dim vTipoDoc As String = Funciones.CheckStr(dtConsultaCanje.Tables(0).Rows(0).Item("CANJV_TIPODOCCLIENTE"))
                                'Dim vNumDoc As String = Funciones.CheckStr(dtConsultaCanje.Tables(0).Rows(0).Item("CANJV_NRODOCCLIENTE"))
                                '/*----------------------------------------------------------------------------------------------------------------*/
                                '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                '/*----------------------------------------------------------------------------------------------------------------*/

                        '************************************************************************************************************'
                        'CONSULTA PEDIDO Origen:

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Metodo ConsultaEquipoCanje_xSeriTipoNum_2()")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp K_SERIE:" & vSerieMat)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp K_NRO_DOC:" & vNumDoc)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp K_TIPO_DOC:" & vTipoDoc)

                        DsPedidoOrigen = vObClsTrsPvu.ConsultaEquipoCanje_xSeriTipoNum_2(vSerieMat, vTipoDoc, vNumDoc, vCursorxSeriTipoNum, vNumLog, vDesLog)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out CU_CONSULTA_CANJE:" & vCursorxSeriTipoNum)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Cod Rspta:" & vNumLog)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out Valor Msj Respta:" & vDesLog)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Metodo ConsultaEquipoCanje_xSeriTipoNum_2()")

                        If Not IsNothing(DsPedidoOrigen) AndAlso DsPedidoOrigen.Tables(0).Rows.Count > 0 Then
                            vNumPedidoOriginal = DsPedidoOrigen.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")

                        Else
                            vNumPedidoOriginal = ""
                        End If

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo DatosInteraccion_x_Operacion()")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Motivo Canje : " & strMotivoCanje)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Tipo Cliente : " & StrTipoCliente)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nro Grupo Tipificación : " & NroGrupoTipificacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nro : " & IntNro)

                                '----------------------------------------------------------------------------------------------------------------
                                '--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                                '---------------------------------------------------------------------------------------------------------------- 
                                If Not (isDatosInteraccionCargados) Then 'solo se cargara una vez en caso de packs
                        oInteraccion = DatosInteraccion_x_Operacion(strMotivoCanje, StrTipoCliente, NroGrupoTipificacion, IntNro)
                                    isDatosInteraccionCargados = True
                                End If
                                '----------------------------------------------------------------------------------------------------------------
                                '--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                                '---------------------------------------------------------------------------------------------------------------- 
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo DatosInteraccion_x_Operacion()")

                        oInteraccion.AGENTE = strEmpleado
                        oInteraccion.OBJID_CONTACTO = objIdContacto
                                '----------------------------------------------------------------------------------------------------------------
                                '--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                                '---------------------------------------------------------------------------------------------------------------- 
                                oInteraccion.TELEFONO = listaTelefonosVenta(idx)
                                'oInteraccion.TELEFONO = dsPedido.Tables(1).Rows(0).Item("DEPEV_NROTELEFONO")
                                '----------------------------------------------------------------------------------------------------------------
                                '--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                                '---------------------------------------------------------------------------------------------------------------- 
                                ' Dim dsDatosVenta As New DataSet
                        Dim NroGrupoTipificacionGeneral As String
                        NroGrupoTipificacionGeneral = ConfigurationSettings.AppSettings("StrKeyTipificacionGeneral")
                        Dim DatosGenerales As String()
                        Dim StrValor As String = ""

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo ObtenerSISACT_Parametros()")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nrop Grupo Tipificación : " & NroGrupoTipificacionGeneral)
                                '----------------------------------------------------------------------------------------------------------------
                                '--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                                '---------------------------------------------------------------------------------------------------------------- 
                                If Not (isParametrosCargados) Then 'solo se cargara una vez en caso de packs
                                    '----------------------------------------------------------------------------------------------------------------
                                    '--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                                    '---------------------------------------------------------------------------------------------------------------- 
                        dsDatosVenta = (New COM_SIC_Activaciones.clsConsultaPvu).ObtenerSISACT_Parametros(NroGrupoTipificacionGeneral)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo ObtenerSISACT_Parametros()")

                        If Not dsDatosVenta Is Nothing Then
                            If dsDatosVenta.Tables(0).Rows.Count > 0 Then

                                For Each item As DataRow In dsDatosVenta.Tables(0).Rows
                                    StrValor = Funciones.CheckStr(item("PARAV_VALOR"))
                                    DatosGenerales = StrValor.Split("|")

                                    oInteraccion.METODO = DatosGenerales(0)
                                    oInteraccion.TIPO_INTER = DatosGenerales(1)
                                    oInteraccion.USUARIO_PROCESO = "USRSICAR"
                                    oInteraccion.FLAG_CASO = DatosGenerales(3)
                                    oInteraccion.RESULTADO = DatosGenerales(4)
                                    oInteraccion.HECHO_EN_UNO = DatosGenerales(5)
                                    oInteraccion.Caso = DatosGenerales(6)

                                    Exit For
                                Next
                            End If
                        End If
                                    '----------------------------------------------------------------------------------------------------------------
                                    '--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                                    '---------------------------------------------------------------------------------------------------------------- 
                                    isParametrosCargados = True
                                End If
                                '----------------------------------------------------------------------------------------------------------------
                                '--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                                '---------------------------------------------------------------------------------------------------------------- 
                        If (StrTipoCliente = ConfigurationSettings.AppSettings("strTVPostpago")) Then
                            'POSTPAGO                        
                            'CONSULTA EL CONTRATO:
                            Try
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Obtener contrato.")
                                '
                                DsContrato = objClsConsultaPvu.ObtenerDrsap(vNumPedidoOriginal, P_COD_RESP, P_MSG_RESP)
                                '
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato Obtenido: " & Funciones.CheckStr(DsContrato.Tables(0).Rows(0).Item("ID_CONTRATO")))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato P_COD_RESP : " & Funciones.CheckStr(P_COD_RESP))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato P_MSG_RESP : " & Funciones.CheckStr(P_MSG_RESP))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Obtener contrato.")
                                '
                                StrDescuentoEquipo = Funciones.CheckStr(DsContrato.Tables(1).Rows(0)("DESCUENTO"))
                                StrNroConstrato = Funciones.CheckStr(DsContrato.Tables(1).Rows(0)("ID_CONTRATO"))
                            Catch ex As Exception
                                '
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Error en obtener contrato :" & StrPedido)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Err." & ex.Message.ToString)
                                '
                            End Try
                            '************************************************************************************************************'
                        Else
                            'PREPAGO
                            StrDescuentoEquipo = ""
                            StrNroConstrato = ""

                        End If

                        If StrTipoCliente = ConfigurationSettings.AppSettings("strTVPrepago") Then
                                '*******************************************************************************************************
                                'Consulta las siguientes Tablas.
                                '**1. sisact_venta_prepago. 
                                '**2. sisact_detalle_venta_prepago. 
                                Try
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia consulta datos Pedido Prepagos Alta y Portabilidad en PVU ")
                                    objClsConsultaPvu.ConsultarPedidosPrepago(vNumPedidoOriginal, P_CODIGO_RESPUESTA, P_MENSAJE_RESPUESTA, C_VENTA, C_VENTA_DET)

                                    StrCampana = Funciones.CheckStr(C_VENTA_DET.Rows(0).Item("DVPR_DES_CAMPANA"))
                                StrPlan = Funciones.CheckStr(C_VENTA_DET.Rows(0).Item("DVPR_DES_PLAN")) 'PROY-26366 

                                    If P_MENSAJE_RESPUESTA <> "OK" Then
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  No se encontro el pedido en la tabla : sisact_info_venta_sap. pkg:sisact_pkg_venta sp:sp_con_venta_x_docsap => " & vNumPedidoOriginal)
                                    End If
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta datos Pedido Prepagos Alta y Porta en PVU ")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_MENSAJE_RESPUESTA: " & P_MENSAJE_RESPUESTA)
                                Catch ex As Exception
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Error al ejecutar el sp: SP_CON_VENTA_PREPAGO , para el pedido => " & vNumPedidoOriginal)
                                        'INI PROY-140126
                                        Dim MaptPath As String
                                        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                                        MaptPath = "( Class : " & MaptPath & "; Function: Tipificaciones_general_canje)"
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Exception: " & ex.ToString() & MaptPath)
                                        'FIN PROY-140126

                                End Try

                            If StrCampana.Equals("") And StrPlan.Equals("") Then

                                'Consulta las siguientes Tablas.
                                '**1. SISACT_VENTA_REPO_PRE.
                                p_limpiador = 1
                                Try
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia consulta datos pedido Prepago diferentes a las Altas y Portabilidad en PVU")
                                    C_VENTA_Prepago = objClsConsultaPvu.ObtenerDatosVentaPrepago(vNumPedidoOriginal, P_MENSAJE_RESPUESTA)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta datos pedido Prepago diferentes a las Altas y Portabilidad en PVU, P_MENSAJE_RESPUESTA :  " & P_MENSAJE_RESPUESTA)
                                Catch ex As Exception
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Ingreso a EX : " & ex.Message.ToString())
                                End Try
                            End If

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "p_limpiador :  " & p_limpiador)

                            If p_limpiador = 1 Then
                                If Not C_VENTA_Prepago Is Nothing Then
                                    If C_VENTA_Prepago.Tables.Count > 0 Then
                                        If C_VENTA_Prepago.Tables(0).Rows.Count > 0 Then
                                            StrCampana = Funciones.CheckStr(C_VENTA_Prepago.Tables(0).Rows(0).Item("CAMPANIA_DES"))
                                            StrPlan = Funciones.CheckStr(C_VENTA_Prepago.Tables(0).Rows(0).Item("PLAN_TARIFARIO_DES"))
                                        End If
                                    End If
                                End If
                            Else
                                If Not C_VENTA_DET Is Nothing Then
                                    If C_VENTA_DET.Rows.Count > 0 Then
                                        StrCampana = Funciones.CheckStr(C_VENTA_DET.Rows(0).Item("DVPR_DES_CAMPANA"))
                                        StrPlan = Funciones.CheckStr(C_VENTA_DET.Rows(0).Item("DVPR_DES_PLAN")) 'PROY-26366
                                    End If
                                End If
                            End If

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "campania_Pre :  " & StrCampana)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "plan_pre :  " & StrPlan)

                        Else
                            '*******************************************************************************************************
                            '**1. sisact_info_venta_sap => consulta con el numero de pedido(nro_documento), retorna el n_id_venta.
                            '**2. sisact_ap_venta v / sisact_ap_contrato, donde  v.id_documento=n_id_venta => c_venta
                            '**3. sisact_ap_venta_detalle vd => vd.id_documento = n_id_venta => c_venta_det  
                            Try
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia consulta datos Pedido PostPsgo en PVU ")
                                objClsConsultaPvu.ConsultarPedidosPVU(vNumPedidoOriginal, _
                                                                              P_CODIGO_RESPUESTA, _
                                                                              P_MENSAJE_RESPUESTA, _
                                                                              C_VENTA, _
                                                                              C_VENTA_DET)

                                If P_MENSAJE_RESPUESTA <> "OK" Then
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  No se encontro el pedido en la tabla : sisact_info_venta_sap. pkg:sisact_pkg_venta sp:sp_con_venta_x_docsap => " & vNumPedidoOriginal)
                                End If

                                If Not C_VENTA_DET Is Nothing Then
                                    If C_VENTA_DET.Rows.Count > 0 Then
                                        StrCampana = Funciones.CheckStr(C_VENTA_DET.Rows(0).Item("campana_desc"))
                                        StrPlan = Funciones.CheckStr(C_VENTA_DET.Rows(0).Item("plan_desc"))

                                    End If
                                End If

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "campania_Post :  " & StrCampana)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "plan_post :  " & StrPlan)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta datos Pedido PostPsgo en PVU")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_MENSAJE_RESPUESTA: " & P_MENSAJE_RESPUESTA)
                            Catch ex As Exception
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Error al ejecutar el sp: SP_CON_VENTA_X_DOCSAP , para el pedido => " & vNumPedidoOriginal)
                                        'INI PROY-140126
                                        Dim MaptPath As String
                                        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                                        MaptPath = "( Class : " & MaptPath & "; Function: Tipificaciones_general_canje)"
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Exception: " & ex.ToString() & MaptPath)
                                        'FIN PROY-140126

                            End Try
                            '*******************************************************************************************************
                        End If

                        Try
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia consulta datos Pedido PostPsgo en PVU Pedido Devol")
                            objClsConsultaPvu.ConsultarPedidosPVU(StrPedido, _
                                                                          P_CODIGO_RESPUESTA, _
                                                                          P_MENSAJE_RESPUESTA, _
                                                                          C_VENTA, _
                                                                          C_VENTA_DET)

                            If P_MENSAJE_RESPUESTA <> "OK" Then
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  No se encontro el pedido en la tabla : sisact_info_venta_sap. pkg:sisact_pkg_venta sp:sp_con_venta_x_docsap => " & StrPedido)
                            End If

                            If Not C_VENTA Is Nothing Then
                                If C_VENTA.Rows.Count > 0 Then
                                    StrNroReclamoResolucion = Funciones.CheckStr(C_VENTA.Rows(0).Item("observacion"))
                                End If
                            End If

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "StrNroReclamoResolucion :  " & StrNroReclamoResolucion)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta datos Pedido PostPsgo en PVU")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_MENSAJE_RESPUESTA: " & P_MENSAJE_RESPUESTA)
                        Catch ex As Exception
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Error al ejecutar el sp: SP_CON_VENTA_X_DOCSAP , para el pedido => " & StrPedido)
                                    'INI PROY-140126
                                    Dim MaptPath As String
                                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                                    MaptPath = "( Class : " & MaptPath & "; Function: Tipificaciones_general_canje)"
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Exception: " & ex.ToString() & MaptPath)
                                    'FIN PROY-140126

                        End Try

                        Dim StrNombreCliente As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0)("CLIEV_NOMBRE"))
                        Dim dFecha As Date
                        dFecha = CDate(dsPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO"))
                        Dim StrFechaVenta As String = dFecha.ToString("dd/MM/yyyy")

                        Dim StrTipoDoc As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0)("CLIEC_TIPODOCCLIENTE"))
                        Dim strTipoDocDes As String

                        Try
                            Dim objConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
                            Dim dsTipDoc As DataSet
                            Dim item As Integer
                            dsTipDoc = objConsultaPvu.ConsultaTipoDocumento("")
                            For Each row As DataRow In dsTipDoc.Tables(0).Rows
                                If row("TDOCC_CODIGO") = StrTipoDoc Then
                                    strTipoDocDes = row("TDOCV_DESCRIPCION")
                                    Exit For
                                End If
                            Next

                        Catch ex As Exception
                            If StrTipoDoc.Equals("01") Then
                                strTipoDocDes = "DNI"
                            Else
                                If StrTipoDoc.Equals("02") Then
                                    strTipoDocDes = "CIP"
                                Else
                                    If StrTipoDoc.Equals("04") Then
                                        strTipoDocDes = "CE"
                                    Else
                                        If StrTipoDoc.Equals("06") Then
                                            strTipoDocDes = "RUC"
                                        Else
                                            If StrTipoDoc.Equals("07") Then
                                                strTipoDocDes = "PASAPORTE"
                                            Else
                                                strTipoDocDes = "DNI"
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End Try

                        Dim StrNroDoc As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0)("CLIEV_NRODOCCLIENTE"))
                        Dim StrNroDocRef As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0)("PEDIV_NROREFNCND"))
                                '/*----------------------------------------------------------------------------------------------------------------*/
                                '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                '/*----------------------------------------------------------------------------------------------------------------*/
                                Dim strNroLinea As String = Funciones.CheckStr(dsPedidoPorLinea(i)("DEPEV_NROTELEFONO"))
                                Dim StrMaterial As String = Funciones.CheckStr(dsPedidoPorLinea(i)("DEPEV_DESCMATERIAL"))

                                Dim StrImeiVendido As String = Funciones.CheckStr(dsPedidoPorLinea(i)("SERIC_CODSERIE"))
                                Dim StrModeloVendido As String = Funciones.CheckStr(dsPedidoPorLinea(i)("DEPEV_DESCMATERIAL"))

                                If StrMonto = "" Then
                                    StrMonto = Math.Round(Funciones.CheckDbl(dsPedidoPorLinea(i)("DEPEN_PRECIOVENTA")), 2).ToString()
                                End If
                                If StrPrecioVenta = "" Then
                                    StrPrecioVenta = Math.Round(Funciones.CheckDbl(dsPedidoPorLinea(i)("DEPEN_PRECIOVENTA")), 2)
                                End If
                                If StrPrecio_Prepago = "" Then
                                    StrPrecio_Prepago = Math.Round(Funciones.CheckDbl(dsPedidoPorLinea(i)("DEPEN_PRECIOVENTA")), 2)
                                End If

                                Dim StrModelo_Devolver As String = Funciones.CheckStr(dtConsultaCanjePorLinea(0)("CANJV_ENT_COD_MAT"))

                                '/*----------------------------------------------------------------------------------------------------------------*/
                                '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                '/*----------------------------------------------------------------------------------------------------------------*/

                        Dim PrecioPrepago As Decimal

                        Try
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo ObtenerPrecio_Prepago()")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Modelo Devolver : " & StrModelo_Devolver)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Línea Prepago : " & ConfigurationSettings.AppSettings("LPprepago").ToString())
                            PrecioPrepago = (New COM_SIC_Activaciones.clsConsultaPvu).ObtenerPrecio_Prepago(StrModelo_Devolver, ConfigurationSettings.AppSettings("LPprepago").ToString())
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo ObtenerPrecio_Prepago()")
                            StrPrecio_Prepago = Math.Round(PrecioPrepago, 2)
                        Catch ex As Exception
                            StrPrecio_Prepago = StrPrecioVenta
                        End Try

                                '/*----------------------------------------------------------------------------------------------------------------*/
                                '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                '/*----------------------------------------------------------------------------------------------------------------*/
                                Dim StrListaPrecios As String = Funciones.CheckStr(dsPedidoPorLinea(i)("DEPEV_DESCRIPCIONLP"))
                                Dim StrMotivoDevol As String = Funciones.CheckStr(dtConsultaCanjePorLinea(0)("CANJV_MOTIVO"))

                        Dim dFechaDevol As Date
                                dFechaDevol = CDate(dtConsultaCanjePorLinea(0)("CANJD_FEC_REG"))
                        Dim StrFechaDevol As String = dFechaDevol.ToString("dd/MM/yyyy")

                                Dim StrOst As String = Funciones.CheckStr(dtConsultaCanjePorLinea(0)("CANJV_REFERENCIA"))

                                If StrImei_Devolver = "" Then
                                    StrImei_Devolver = Funciones.CheckStr(dtConsultaCanjePorLinea(0)("CANJV_ENT_SERIE_MAT"))
                                End If
                                If StrImei_Nuevo = "" Then
                                    StrImei_Nuevo = Funciones.CheckStr(dtConsultaCanjePorLinea(0)("CANJV_SAL_SERIE_MAT"))
                                End If
                                
                                '/*----------------------------------------------------------------------------------------------------------------*/
                                '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                '/*----------------------------------------------------------------------------------------------------------------*/

                        Try
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo ConsultaMaterial()")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Modelo Devolver : " & StrModelo_Devolver)
                            DsMAterial = objclsConsultaMsSap.ConsultaMaterial(StrModelo_Devolver)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo ConsultaMaterial()")
                            StrModelo_Devolver = DsMAterial.Tables(0).Rows(0)("MATEV_DESCMATERIAL")
                                    '/*----------------------------------------------------------------------------------------------------------------*/
                                    '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                    '/*----------------------------------------------------------------------------------------------------------------*/
                                    strTipoMaterial = DsMAterial.Tables(0).Rows(0)("MATEC_TIPOMATERIAL")
                                    '/*----------------------------------------------------------------------------------------------------------------*/
                                    '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                    '/*----------------------------------------------------------------------------------------------------------------*/
                        Catch ex As Exception
                                    '/*----------------------------------------------------------------------------------------------------------------*/
                                    '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                    '/*----------------------------------------------------------------------------------------------------------------*/
                                    StrModelo_Devolver = Funciones.CheckStr(dtConsultaCanjePorLinea(0)("CANJV_ENT_COD_MAT"))
                                    'StrModelo_Devolver = Funciones.CheckStr(dtConsultaCanje.Tables(0).Rows(0)("CANJV_ENT_COD_MAT"))
                                    '/*----------------------------------------------------------------------------------------------------------------*/
                                    '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                    '/*----------------------------------------------------------------------------------------------------------------*/
                        End Try

                                '/*----------------------------------------------------------------------------------------------------------------*/
                                '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                '/*----------------------------------------------------------------------------------------------------------------*/
                                Dim StrModalidadVenta As String = Funciones.CheckStr(dtConsultaCanjePorLinea(0)("CANJV_TIPO_CANJE"))
                                'Dim StrModalidadVenta As String = Funciones.CheckStr(dtConsultaCanje.Tables(0).Rows(0)("CANJV_TIPO_CANJE"))
                                '/*----------------------------------------------------------------------------------------------------------------*/
                                '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                '/*----------------------------------------------------------------------------------------------------------------*/

                        If (StrModalidadVenta = "1") Then
                            StrModalidadVenta = "Canje de Gama  Similar"
                        Else
                            If (StrModalidadVenta = "2") Then
                                StrModalidadVenta = "Canje de Gama Superior"
                            Else
                                If (StrModalidadVenta = "3") Then
                                    StrModalidadVenta = "Devolucion de Efectivo"
                                End If
                            End If
                        End If

                        Dim strOficina As String = Session("ALMACEN")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo ConsultaPuntoVentaTipi()")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Oficina : " & strOficina)
                        Dim StrPunto_Venta As String = ConsultaPuntoVentaTipi(strOficina)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo ConsultaPuntoVentaTipi()")

                                '/*----------------------------------------------------------------------------------------------------------------*/
                                '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                '/*----------------------------------------------------------------------------------------------------------------*/
                                If (registrosPorLinea = 1) Then
                                    If strTipoMaterial = "TV0003" Then 'EQUIPO
                                        isChipEquipoOrPack = "EQUIPO"
                                    ElseIf strTipoMaterial = "TV0004" Then  'CHIP
                                        isChipEquipoOrPack = "CHIP"
                                    End If
                                End If

                                If isChipEquipoOrPack = "CHIP" Then
                                    StrPrecioVentaChip = Math.Round(Funciones.CheckDbl(dsPedidoPorLinea(i)("DEPEN_PRECIOVENTA")), 2)
                                    StrIccid_Devolver = Funciones.CheckStr(dtConsultaCanjePorLinea(0)("CANJV_ENT_SERIE_MAT"))
                                    If PrecioPrepago > 0 Then
                                        StrPrecio_PrepagoChip = Math.Round(PrecioPrepago, 2)
                                    Else
                                        StrPrecio_PrepagoChip = StrPrecioVentaChip
                                    End If
                                ElseIf isChipEquipoOrPack = "PACK" Then
                                    If strTipoMaterial = "TV0004" Then 'CHHIP
                                        StrPrecioVentaChip = Math.Round(Funciones.CheckDbl(dsPedidoPorLinea(i)("DEPEN_PRECIOVENTA")), 2)
                                        StrIccid_Devolver = Funciones.CheckStr(dtConsultaCanjePorLinea(0)("CANJV_ENT_SERIE_MAT"))
                                        If PrecioPrepago > 0 Then
                                            StrPrecio_PrepagoChip = Math.Round(PrecioPrepago, 2)
                                        Else
                                            StrPrecio_PrepagoChip = StrPrecioVentaChip
                                        End If
                                    ElseIf strTipoMaterial = "TV0003" Then 'EQUIPO
                                        StrPrecioVenta = Math.Round(Funciones.CheckDbl(dsPedidoPorLinea(i)("DEPEN_PRECIOVENTA")), 2)
                                        StrImei_Devolver = Funciones.CheckStr(dtConsultaCanjePorLinea(0)("CANJV_ENT_SERIE_MAT"))
                                        StrModelo_DevolverEquipo = StrModelo_Devolver
                                    End If
                                End If

                                StrNotas = ""
                                StrMonto = Math.Round(Funciones.CheckDbl(StrPrecioVenta) + Funciones.CheckDbl(StrPrecioVentaChip), 2)
                                '/*----------------------------------------------------------------------------------------------------------------*/
                                '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                '/*----------------------------------------------------------------------------------------------------------------*/

                        StrNotas = StrNotas & "NOMBRE : " & StrNombreCliente & vbCrLf
                        StrNotas = StrNotas & "NÚMERO DE CONTRATO : " & StrNroConstrato & vbCrLf
                        StrNotas = StrNotas & "TIPO DE DOC. IDENTIDAD : " & strTipoDocDes & vbCrLf
                        StrNotas = StrNotas & "NRO. DE DOC. IDENTIDAD : " & StrNroDoc & vbCrLf
                        StrNotas = StrNotas & "FECHA DE VENTA : " & StrFechaVenta & vbCrLf
                        StrNotas = StrNotas & "NRO DE LÍNEA : " & strNroLinea & vbCrLf
                        StrNotas = StrNotas & "MOTIVO DEVOLUCIÓN : " & oInteraccion.SERVAFECT & vbCrLf
                        StrNotas = StrNotas & "FECHA DEVOLUCIÓN : " & StrFechaDevol & vbCrLf
                                '----------------------------------------------------------------------------------------------------------------
                                '--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                                '---------------------------------------------------------------------------------------------------------------- 
                                If isChipEquipoOrPack = "CHIP" Then
                                    StrNotas = StrNotas & "PRECIO DE VENTA CHIP : " & StrPrecioVentaChip & vbCrLf
                                    StrNotas = StrNotas & "PRECIO PREPAGO : " & StrPrecio_PrepagoChip & vbCrLf
                                    StrNotas = StrNotas & "LISTA DE PRECIOS : " & StrListaPrecios & vbCrLf
                                    StrNotas = StrNotas & "ICCID : " & StrIccid_Devolver & vbCrLf
                                ElseIf isChipEquipoOrPack = "EQUIPO" Then
                                    StrNotas = StrNotas & "PRECIO DE VENTA EQUIPO : " & StrPrecioVenta & vbCrLf
                        StrNotas = StrNotas & "PRECIO PREPAGO : " & StrPrecio_Prepago & vbCrLf
                        StrNotas = StrNotas & "LISTA DE PRECIOS : " & StrListaPrecios & vbCrLf
                        StrNotas = StrNotas & "IMEI A DEVOLVER : " & StrImei_Devolver & vbCrLf
                        StrNotas = StrNotas & "MODELO A DEVOLVER : " & StrModelo_Devolver & vbCrLf
                                ElseIf isChipEquipoOrPack = "PACK" Then
                                    StrNotas = StrNotas & "PRECIO DE VENTA EQUIPO : " & StrPrecioVenta & vbCrLf
                                    StrNotas = StrNotas & "PRECIO PREPAGO : " & StrPrecio_Prepago & vbCrLf
                                    StrNotas = StrNotas & "LISTA DE PRECIOS : " & StrListaPrecios & vbCrLf
                                    StrNotas = StrNotas & "IMEI A DEVOLVER : " & StrImei_Devolver & vbCrLf
                                    StrNotas = StrNotas & "MODELO A DEVOLVER : " & StrModelo_DevolverEquipo & vbCrLf
                                    StrNotas = StrNotas & "PRECIO DE VENTA CHIP : " & StrPrecioVentaChip & vbCrLf
                                    StrNotas = StrNotas & "ICCID : " & StrIccid_Devolver & vbCrLf
                                End If
                                '----------------------------------------------------------------------------------------------------------------
                                '--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                                '---------------------------------------------------------------------------------------------------------------- 
                        StrNotas = StrNotas & "PLAN : " & StrPlan & vbCrLf
                        StrNotas = StrNotas & "MODALIDAD DE VENTA : " & StrModalidadVenta & vbCrLf
                        StrNotas = StrNotas & "NRO. DOC. REF. : " & StrNroDocRef & vbCrLf
                        StrNotas = StrNotas & "CAMPAÑA : " & StrCampana & vbCrLf
                        StrNotas = StrNotas & "OST : " & StrOst & vbCrLf
                        StrNotas = StrNotas & "NRO. RECLAMO/RESULUCIÓN : " & StrNroReclamoResolucion & vbCrLf
                        StrNotas = StrNotas & "MONTO : " & StrMonto & vbCrLf
                        StrNotas = StrNotas & "PUNTO DE VENTA : " & StrPunto_Venta & vbCrLf
                        StrNotas = StrNotas & "USUARIO : " & strEmpleado & vbCrLf
                                '----------------------------------------------------------------------------------------------------------------
                                '--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                                '---------------------------------------------------------------------------------------------------------------- 
                            Next

                        oInteraccion.NOTAS = StrNotas

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo CrearInteraccion()")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "oInteraccion: " & oInteraccion.NOTAS) 'PROY-26366 LOG
                        oTipificacion.CrearInteraccion(oInteraccion, idInteraccion, flagInter, mensajeInter)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Interaccion : " & idInteraccion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Metodo : " & flagInter)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeInter)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo CrearInteraccion()")

                        If Trim(flagInter) = "OK" Then

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo CrearInteraccionDetalle()")
                            oInteraccion.ID_INTERACCION = idInteraccion
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "oInteraccion: " & oInteraccion.NOTAS) 'PROY-26366 LOG
                            oTipificacion.CrearInteraccionDetalle(oInteraccion, idInteraccion, flagInter, mensajeInter)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Interaccion : " & idInteraccion)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Metodo : " & flagInter)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeInter)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo CrearInteraccionDetalle()")
                        End If
                            '----------------------------------------------------------------------------------------------------------------
                            '--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                            '---------------------------------------------------------------------------------------------------------------- 
                    End If

                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error. Cliente no existe en Clarify. No pudo realizar la creacion de la Interaccion.")
                End If
            End If
                '----------------------------------------------------------------------------------------------------------------
                '--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
                '---------------------------------------------------------------------------------------------------------------- 
            Next
            'FIN - Grabar la tipificacion por cada Linea en "listaTelefonosVenta"
            '----------------------------------------------------------------------------------------------------------------
            '--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
            '----------------------------------------------------------------------------------------------------------------  
        Catch ex As Exception

        End Try
    End Function

    Private Function DatosInteraccion_x_Operacion(ByVal Strmotivo As String, ByVal StTipoCliente As String, ByVal NroGrupoTipificacion As String, ByVal IntNro As Integer) As Interaccion
        Dim dsDatosVenta As New DataSet
        Dim oInteraccion As New Interaccion
        Dim DatosGenerales As String()
        Dim DatosGenerales1 As String()
        Dim DatosGenarales1_0 As String()
        Dim DatosGenarales1_1 As String()
        Dim DatosGenarales1_2 As String()
        Dim DatosGenarales1_3 As String()
        Dim StrValor As String = ""
        Dim StrValor1 As String = ""

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo ObtenerSISACT_Parametros()")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametro : ")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nrop Grupo Tipificación : " & NroGrupoTipificacion)
        dsDatosVenta = (New COM_SIC_Activaciones.clsConsultaPvu).ObtenerSISACT_Parametros(NroGrupoTipificacion)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo ObtenerSISACT_Parametros()")

        If Not dsDatosVenta Is Nothing Then
            If dsDatosVenta.Tables(0).Rows.Count > 0 Then

                If (IntNro = 1) Then
                    For Each item As DataRow In dsDatosVenta.Tables(0).Rows
                        StrValor = Funciones.CheckStr(item("PARAV_VALOR"))
                        StrValor1 = Funciones.CheckStr(item("PARAV_VALOR1"))

                        DatosGenerales = StrValor.Split("|")
                        DatosGenerales1 = StrValor1.Split("|")

                        If (DatosGenerales(0).Equals(StTipoCliente)) Then

                            If (DatosGenerales(1).Equals(Strmotivo)) Then
                                DatosGenarales1_0 = DatosGenerales1(0).Split(";")
                                DatosGenarales1_1 = DatosGenerales1(1).Split(";")
                                DatosGenarales1_2 = DatosGenerales1(2).Split(";")
                                DatosGenarales1_3 = DatosGenerales1(3).Split(";")

                                oInteraccion.TIPO_CODIGO = DatosGenarales1_0(0)
                                oInteraccion.TIPO = DatosGenarales1_0(1)
                                oInteraccion.CLASE_CODIGO = DatosGenarales1_1(0)
                                oInteraccion.CLASE = DatosGenarales1_1(1)
                                oInteraccion.SUBCLASE_CODIGO = DatosGenarales1_2(0)
                                oInteraccion.SUBCLASE = DatosGenarales1_2(1)
                                oInteraccion.SERVAFECT_CODE = DatosGenarales1_3(0)
                                oInteraccion.SERVAFECT = DatosGenarales1_3(1)
                                Exit For
                            End If
                        End If
                    Next
                End If
            End If
        End If
        Return oInteraccion
    End Function

    Private Function ConsultaPuntoVentaTipi(ByVal P_OVENC_CODIGO As String) As String
        Try
            Dim obj As New COM_SIC_Activaciones.clsConsultaMsSap
            Dim dsReturn As DataSet
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo ConsultaPuntoVenta()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codigo : " & P_OVENC_CODIGO)
            dsReturn = obj.ConsultaPuntoVenta(P_OVENC_CODIGO)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo ConsultaPuntoVenta()")
            If dsReturn.Tables(0).Rows.Count > 0 Then
                Return "CAC" & "-" & dsReturn.Tables(0).Rows(0).Item("OVENV_DESCRIPCION") & "-" & dsReturn.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA")
            End If
            Return Nothing
        Catch ex As Exception
            Return ""
        End Try
    End Function
    'PROY-26366-IDEA-34247 FASE 1 - FIN

'*****************************************************************
    'PROY 26210 EGSC BEGIN ADD METHODS 
    '*****************************************************************
#Region "EGSC ADD DEVOLUCION"

    'Metodo que realiza :
    '   - Genera la tipificacion :  "Devolución en Efectivo"
    '   - Para el motivo devolucion que pertenece a "ValidarMotivoDevolucion"
    '       - Genera tipificacion: "Devolucion por Anulacion"
    '       - Elimina el cambio de plan.
    '       - El acuerdo en el SIGA se cambia a estado ANULADO
    '       - Se libera la serie IMEI   
    Private Sub TipificarGenerarDevolucionEnEfectivo(ByVal datoPedido As DataRow, ByVal datoDetalle As DataRow)

        ' Se registra la tipificacion : "Devolucion en Efectivo"   ---> NO VA

        Dim strMsj = ""
        'Se evalua que el motivo de devolucion esta en la plantilla :  
        Dim motivo_pedido As String = datoPedido.Item("PEDIV_MOTIVOPEDIDO")
        Dim nro_pedido As String = datoPedido.Item("PEDIN_NROPEDIDO")
        Dim telefono As String = IIf(datoDetalle.IsNull("DEPEV_NROTELEFONO"), "", datoDetalle.Item("DEPEV_NROTELEFONO"))
        Dim serie_equipo As String = IIf(datoDetalle.IsNull("SERIC_CODSERIE"), "", datoDetalle.Item("SERIC_CODSERIE"))


        If (Me.ValidarMotivoDevolucion(motivo_pedido)) Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-- Se berifica si se libero IMEI ")

            '*************************************************************
            '** CAMBIAR ESTADO A ANULADO EN EL SIGA :
            'If (Me.CambiarEstadoAnulado()) Then
            '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-- Se anulo el estado en el SIGA ")
            '    strMsj = strMsj & System.Configuration.ConfigurationSettings.AppSettings("strMsjAcuerdoAnulado") & "|"
            'End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-- Se verfica si se elimina cambio de plan ")

            Me.GenerarTipificacionDevolucionAnulacion(datoPedido, datoDetalle, strMsj)

        End If

    End Sub

    'INICIATIVA - 219 - INICIO - DEVOLUCIONES
Public Function EliminarProgramacionCambioPlan(ByVal strNumero As String) As Boolean
        'MOD: P26210 RMZ - ConsultaProgramacionWS
        Dim strEmpleado As String = Session("strusuario")
        Dim strUrl As String = ConfigurationSettings.AppSettings("consConsultaProgramacion")
        Dim objCambioPlan As COM_SIC_Activaciones.CambioPlanPostpago
        Dim strTipTransaccion As String = String.Empty
        Dim strFechaDesde As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("consFechaDesde"))
        Dim strFechaHasta As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("consFechaHasta"))
        Dim strAsesor As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("consAsesor"))
        Dim strCuenta As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("consCuenta"))
        Dim strCodInteraccion As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("consCodInteraccion"))
        Dim strCadDac As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("consCadDac"))
        Dim strServCod As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("consServCod"))
        Dim strTipServicio As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("consTipoServicio"))
        Dim strEstadoTrans As String = String.Empty
        Dim mensajeError As String = String.Empty
        'INICIATIVA - 219 - INICIO
        'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " INI: CONSULTA PROGRAMACION WS --> " & strNumero)
        'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " URL --> " & strUrl)
        'objCambioPlan = (New COM_SIC_Activaciones.clsConsultaProgramacion).ConsultaProgramacion(strNumero, strTipTransaccion, strEstadoTrans, strTipServicio, strFechaDesde, _
        '                                                                                    strFechaHasta, strAsesor, strCuenta, strCodInteraccion, strCadDac, strServCod, strEmpleado, mensajeError) 
        'INICIATIVA - 219 - FIN
        'INICIATIVA - 219 - INICIO
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " INI: CONSULTAR PROGRAMACIONES WS --> " & strNumero)
        Dim objMessageRequestConsultarProgramaciones As New MessageRequestConsultarProgramaciones
        Dim objMessageResponseConsultarProgramaciones As New MessageResponseConsultarProgramaciones
        Dim objBLDatosCBIO As New BLDatosCBIO
        Dim blExitoSer As Boolean = False
        Dim blTieneProgPendiente As Boolean = False

        objMessageRequestConsultarProgramaciones.consultarProgramacionesRequest.msisdn = strNumero
        objMessageRequestConsultarProgramaciones.consultarProgramacionesRequest.asesor = strAsesor
        objMessageRequestConsultarProgramaciones.consultarProgramacionesRequest.tipoTransaccion = strTipTransaccion
        objMessageRequestConsultarProgramaciones.consultarProgramacionesRequest.fechaDesde = strFechaDesde
        objMessageRequestConsultarProgramaciones.consultarProgramacionesRequest.fechaHasta = strFechaHasta
        objMessageRequestConsultarProgramaciones.consultarProgramacionesRequest.cuenta = strCuenta
        objMessageRequestConsultarProgramaciones.consultarProgramacionesRequest.cadDac = strCadDac
        objMessageRequestConsultarProgramaciones.consultarProgramacionesRequest.codigoInteraccion = strCodInteraccion

        objMessageResponseConsultarProgramaciones = objBLDatosCBIO.ConsultarProgramacionesCBIO(objMessageRequestConsultarProgramaciones, blExitoSer, blTieneProgPendiente)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Response: blExitoSer --> " & Funciones.CheckStr(blExitoSer))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Response: blTieneProgPendiente --> " & Funciones.CheckStr(blTieneProgPendiente))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " FIN: CONSULTAR PROGRAMACIONES WS --> " & strNumero)

        'INICIATIVA - 219 - FIN
        'If mensajeError.Equals(String.Empty) Then 'INICIATIVA - 219 - INICIO 
        If blExitoSer And blTieneProgPendiente Then
            If objMessageResponseConsultarProgramaciones.consultarProgramacionesResponse.responseData.listaProgramaciones.Length > 0 Then
                For Each objListaProgramacion As listaProgramaciones In objMessageResponseConsultarProgramaciones.consultarProgramacionesResponse.responseData.listaProgramaciones
        Dim oCambioPlan As New NEGOCIO_SIC_SANS.CambioPlanPostpagoNg
            Dim CodCambioPlan As Integer = Funciones.CheckInt(ConfigurationSettings.AppSettings("consPostCambioPlan"))
        Dim EstCambioPlan As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("consPostpagoValorUno"))
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Response: ServiCod --> " & objCambioPlan.strServiCod)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Response: Estado --> " & objCambioPlan.strServiEst)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Response: ServiCod --> " & objListaProgramacion.serviCod)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Response: Estado --> " & objListaProgramacion.estado)
                    'If (objCambioPlan.strServiCod = CodCambioPlan And objCambioPlan.strServiEst = EstCambioPlan) Then
                    If (objListaProgramacion.serviCod = CodCambioPlan And objListaProgramacion.estado = EstCambioPlan) Then
                        'Validar si la lineas es CBIO
                        Dim objActivacionCBIO As New ClsActivacionCBIO
                        Dim strCodResp As String
                        Dim strMsgResp As String
                        Dim prm3 As String
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 Consulta Transaccion")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 Consulta origen linea: " & strNumero)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 Codigo Transaccion Devolucion: " & Funciones.CheckStr(ConfigurationSettings.AppSettings("strDevolucionCBIO")))
                        Dim arrListaNum As ArrayList = objActivacionCBIO.consultaTransaccion(strNumero, Funciones.CheckStr(ConfigurationSettings.AppSettings("strDevolucionCBIO")), strCodResp, strMsgResp)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 strCodResp: " & strCodResp)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 strMsgResp: " & strMsgResp)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciativa-219 arrListaPrepago: " & IIf(arrListaNum Is Nothing, "No Existe", arrListaNum.Count))

                        If (arrListaNum.Count > 0 And strCodResp = "0" And strMsgResp = "OK") Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "<-- Ini RollbackCambioPlanCBIO --> ")
                            Dim objRollbackCambioPlanCBIO As New BLDatosCBIO
                            prm3 = objRollbackCambioPlanCBIO.RollbackCambioPlanCBIO(strNumero, CodCambioPlan, EstCambioPlan)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Response: prm3 --> " & prm3)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "<-- Fin RollbackCambioPlanCBIO --> ")
                        Else
        'Borra programacion : 0 borrado - 1 no borrado - -1 error   
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "<-- Ini RollbackCambioPlan --> ")
                            prm3 = oCambioPlan.RollbackCambioPlan(strNumero, CodCambioPlan, EstCambioPlan)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Response: prm3 --> " & prm3)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "<-- Fin RollbackCambioPlan --> ")
                        End If

            Return True
                        'Else
                        '    Return True
                    End If
                Next
        End If
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Response: mensajeError --> " & mensajeError)
        Return False
        End If
        'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " FIN: CONSULTA PROGRAMACION WS --> " & strNumero)
        'MOD: P26210 RMZ - ConsultaProgramacionWS

    End Function
    'INICIATIVA - 219 - FIN - DEVOLUCIONES

'Registra la tipificacion y escribe un javascript para mostrar datos de tipificacion.
    Private Sub GenerarTipificacionDevolucionAnulacion(ByVal drTipi As DataRow, ByVal drDeta As DataRow, ByVal strMensaje As String)
        Try
            '***********
            Dim strIdIte As String = Me.RegistrarTipificacionDPA(CurrentUser, drTipi, drDeta)

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error : " & ex.Message.ToString())

        End Try

    End Sub

    'Registra la tipificacion: DEVOLUCION POR ANULACION
    Private Function RegistrarTipificacionDPA(ByVal usuario As String, ByVal drTipi As DataRow, ByVal drDeta As DataRow) As String
        Dim numeroTelefono As String = IIf(drDeta.IsNull("DEPEV_NROTELEFONO"), "", drDeta.Item("DEPEV_NROTELEFONO"))
        Dim oInteraccion = New COM_SIC_INActChip.Interaccion
        Dim strIdentifyLog As String = ""

        With oInteraccion

            '.OBJID_CONTACTO = objId
            .TELEFONO = numeroTelefono
            .TIPO = ConfigurationSettings.AppSettings("CONS_RENOVACION_TIPO_NC")
            .CLASE = ConfigurationSettings.AppSettings("CONS_DEVOANU_CLASE")
            .SUBCLASE = ConfigurationSettings.AppSettings("CONS_DEVOANU_SUBCLASE")
            .USUARIO_PROCESO = ConfigurationSettings.AppSettings("CONS_RENOVACION_USUARIO_NC")
            .AGENTE = usuario
            .TIPO_CODIGO = ConfigurationSettings.AppSettings("CONS_DEVOANU_TIPO_COD")
            .CLASE_CODIGO = ConfigurationSettings.AppSettings("CONS_DEVOANU_COD_CL_NC")
            .SUBCLASE_CODIGO = ConfigurationSettings.AppSettings("CONS_DEVOANU_OD_SCL_NC")

            .METODO = ConfigurationSettings.AppSettings("CONS_RENOVACION_METODO_NC")
            .FLAG_CASO = ConfigurationSettings.AppSettings("CONS_RENOVACION_FLAG_NC")
            .RESULTADO = ConfigurationSettings.AppSettings("CONS_RENOVACION_RESULTADO_NC")

        End With
        Dim flagInter As String
        Dim mensajeInter As String
        Dim idInteraccion As String
        Dim strOk As String
        Try
            strIdentifyLog = drTipi("PEDIN_NROPEDIDO")
            Dim oTipificacion As New COM_SIC_INActChip.clsTipificacion

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- DEVOLUCIÓN POR ANULACIÓN ---")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Tipificacion  ---")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo Tipificacion")

            oTipificacion.CrearInteraccion(oInteraccion, idInteraccion, flagInter, mensajeInter)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Interaccion : " & idInteraccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Metodo : " & flagInter)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeInter)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo Tipificacion")

            'Registro del detalle Tipificacion : 

            'EGSC COMENTADO TIPI - PROCESO RENOVACION CAC:  Dim strInteraccion As String = TipificacionCambioPlanRenov(numeroTelefono, documentoSAP, vendedor, notasCambioPlan)

            If (flagInter.ToUpper().Equals("OK")) Then 'egsc 666

                Dim fecha As DateTime

                fecha = IIf(drTipi.IsNull("PEDID_FECHAENTREGA"), New Date(1900, 1, 1), drTipi("PEDID_FECHAENTREGA").ToString())

                Dim oPlantilla1 As New COM_SIC_INActChip.PlantillaInteraccion
                oPlantilla1.X_ADDRESS = drTipi("CLIEV_NOMBRE") 'Nombre del Cliente
                oPlantilla1.X_DOCUMENT_NUMBER = drTipi("CLIEV_NRODOCCLIENTE") 'Nro de documento identidad
                oPlantilla1.X_CLAROLOCAL1 = IIf(Session("ALMACEN") Is Nothing, "", Session("ALMACEN")) & " - " & IIf(Session("OFICINA") Is Nothing, "", Session("OFICINA"))     ' drTipi("OFICV_CODOFICINA") 'Centro de Atencion
                oPlantilla1.X_OTHER_PHONE = numeroTelefono ' drTipi("CLIEV_TELEFONOCLIENTE") 'Nro de linea
                oPlantilla1.X_AMOUNT_UNIT = drTipi("INPAN_TOTALDOCUMENTO").ToString 'Monto efectivo a devolver 


                'oPlantilla1.X_MODEL = drTipi("PEDIV_MOTIVOPEDIDO").ToString  'Motivo de la devolucion  

                'EGSC : Obtener nombre del motivo devolucion : 
                Dim objTipoDevo As New COM_SIC_Cajas.clsCajas
                Dim flEncontrado = False
                oPlantilla1.X_MODEL = objTipoDevo.GetNombreTipoDevo(drTipi("PEDIV_MOTIVOPEDIDO").ToString, ConfigurationSettings.AppSettings("CONS_TIPO_OPE_DEVO"), "1", flEncontrado)
                oPlantilla1.X_MODEL = IIf(flEncontrado, oPlantilla1.X_MODEL, drTipi("PEDIV_MOTIVOPEDIDO").ToString)

                '--------------------------------------------------------------
                oPlantilla1.X_IMEI = drDeta("SERIC_CODSERIE") 'Serie - IMEI 
                oPlantilla1.X_MONTH = IIf(fecha.Year = 1900, "", fecha.ToString("dd/MM/yyyy")) 'fecha
                oPlantilla1.X_INTER_3 = IIf(fecha.Year = 1900, "", fecha.ToString("hh:mm:ss")) 'hora
                oPlantilla1.X_DISTRICT = IIf(drTipi.IsNull("CLIEV_DIRECCIONCLIENTE"), "", drTipi("CLIEV_DIRECCIONCLIENTE")) 'Dirección
                oPlantilla1.X_REFERENCE_PHONE = IIf(drTipi.IsNull("CLIEV_TELEFONOCLIENTE"), "", drTipi("CLIEV_TELEFONOCLIENTE")) 'Tel. Referencia
                oPlantilla1.X_CLAROLOCAL2 = IIf(drDeta.IsNull("DEPEV_DESCMATERIAL"), "", drDeta("DEPEV_DESCMATERIAL")) 'Marca y Modelo
                oPlantilla1.X_INTER_5 = "" 'Condición del Equipo 
                oPlantilla1.X_NAME_LEGAL_REP = Session("strUsuario") 'Nombre del asesor
                oPlantilla1.X_INTER_6 = Session("NOMBRE_COMPLETO") 'Código del Asesor
                oPlantilla1.X_TYPE_DOCUMENT = drTipi("CLIEC_TIPOCLIENTE") 'Tipo de cliente

                '--------------------------------------------------------------
                'AQUI SE AGREGA EL TIPO DE CLIENTE 
                '--------------------------------------------------------------
                Try
                    Dim objServicio As New clsClienteBSCS
                    Dim strempleado As String = Session("strUsuario") 'PROY 26210 Agregado por RMZ
                    Dim oSIACPostpagoConsultas As New clsDatosPostpagoNegocios
                    Dim objBLCbio As New BLDatosCBIO 'INICIATIVA-219

                    objServicio = objBLCbio.LeerDatosCliente(numeroTelefono, "", strempleado) 'INICIATIVA-219
                    'oSIACPostpagoConsultas.LeerDatosCliente(numeroTelefono, "", strempleado, "") 'PROY 26210 Modificado por RMZ

                    oPlantilla1.X_TYPE_DOCUMENT = objServicio.tipo_cliente
                Catch

                End Try


                'CLIEV_RLNRODOCUMENTO    numeroTelefono 
                'IMEI Equipo devuelto   --> EGSC Falta agregar IMEI

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo InsertarPlantilla()")
                oTipificacion.InsertarPlantillaInteraccion(oPlantilla1, idInteraccion, flagInter, mensajeInter)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Interaccion : " & idInteraccion)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Metodo : " & flagInter)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeInter)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nombre del Cliente : " & oPlantilla1.X_ADDRESS)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nro de documento identidad : " & oPlantilla1.X_DOCUMENT_NUMBER)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Centro de Atencion : " & oPlantilla1.X_CLAROLOCAL1)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nro de linea : " & oPlantilla1.X_OTHER_PHONE)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Monto efectivo a devolver  : " & oPlantilla1.X_AMOUNT_UNIT).ToString()
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Motivo de la devolucion : " & oPlantilla1.X_MODEL).ToString()
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Serie - IMEI : " & oPlantilla1.X_IMEI)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fecha : " & oPlantilla1.X_MONTH)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Hora : " & oPlantilla1.X_INTER_3)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Dirección : " & oPlantilla1.X_DISTRICT)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Tel. Referencia : " & oPlantilla1.X_REFERENCE_PHONE)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Marca y Modelo : " & oPlantilla1.X_CLAROLOCAL2)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Condición del Equipo : " & oPlantilla1.X_INTER_5)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nombre del asesor : " & oPlantilla1.X_NAME_LEGAL_REP)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Código del Asesor : " & oPlantilla1.X_INTER_6)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Tipo de cliente : " & oPlantilla1.X_TYPE_DOCUMENT)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo InsertarPlantilla()")
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin Tipificacion ---")

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error : " & ex.Message.ToString())

        End Try

        Return idInteraccion

    End Function

    'Valida que el motivo ingresado pertenece a la lista de estados configurado.
    Private Function ValidarMotivoDevolucion(ByVal motivo As String) As Boolean
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Buscar si el motivo " & motivo & " se encuentra en la lista configurada en CONS_KEYS_MOT_DEVOLUCION")

            Dim keys As String = ConfigurationSettings.AppSettings("CONS_KEYS_MOT_DEVOLUCION")

            Dim items As String() = keys.Split(",")

            For Each item As String In items

                If (motivo = item) Then
                    Return True
                End If

            Next
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error : " & ex.Message.ToString())
        End Try

        Return False  ' falta implementar
    End Function




#End Region
    '*****************************************************************
    'PROY 26210 EGSC END ADD METHODS 
    '*****************************************************************
    '*****************************************************************
    'ADD: PROY 26210 - RMZ
    '*****************************************************************
    Private Function Obtener_NroSec_PostPago(ByVal strNroPedido As String) As String
        Dim objContrato As DataSet          '*** guardar la informaciòn recuperada del contrato - RMZ ***'
        Dim dsAcuerdo As DataSet
        Dim strNroSec As String = ""
        Dim P_COD_RESP As String = ""
        Dim P_MSG_RESP As String = ""
        Dim strContrato As String = ""
        Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Obtener SEC - Devolucion Efectivo")
        objContrato = objClsConsultaPvu.ObtenerDrsap(strNroPedido, P_COD_RESP, P_MSG_RESP)
        If Not (objContrato Is Nothing) Then
            If objContrato.Tables(0).Rows(0).Item("ID_CONTRATO") <> Nothing Then
                strContrato = Funciones.CheckStr(objContrato.Tables(0).Rows(0).Item("ID_CONTRATO"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato Obtenido: " & Funciones.CheckStr(objContrato.Tables(0).Rows(0).Item("ID_CONTRATO")))
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato Obtenido: ")
            End If
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato Obtenido: ")
        End If

        dsAcuerdo = objClsConsultaPvu.ConsultaAcuerdoPCS(Funciones.CheckInt64(strContrato), DBNull.Value)

        If Not dsAcuerdo Is Nothing Then
            strNroSec = Funciones.CheckStr(dsAcuerdo.Tables(0).Rows(0).Item("contn_numero_sec"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "         OUT Sec: " & strNroSec)
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "         OUT Sec: " & strNroSec)
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Obtener SEC - Devolucion Efectivo")
        Return strNroSec

    End Function
    '*****************************************************************
    'ADD: PROY 26210 - RMZ
    '*****************************************************************
    '//PROY-140379 INI
    Private Function ejecutarConsultarProcesarIOT(ByVal telefono As String, ByVal StrDescripcionMotivo As String, ByVal strSerie_entrante As String, ByVal strIccid_baja As String) As Boolean
        Dim objConsultar As New COM_SIC_Activaciones.BWConsultasProcesos_IOT

        Dim strResult As String
        Dim strParametroBaja As String = Funciones.CheckStr(COM_SIC_Seguridad.ReadKeySettings.Key_consultarProcesarIotDarBaja)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ejecutarConsultarProcesarIOT")
        Dim objOpcional As New BElistaOpcional
        Try

            strResult = objConsultar.consultarProcesarUnico(telefono, StrDescripcionMotivo, CurrentUser, strParametroBaja, strSerie_entrante, strIccid_baja, Request.ServerVariables("LOCAL_ADDR"), CurrentTerminal)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado consultarProcesarUnico: " & strResult)
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "error ejecutarConsultarProcesarIOT Message : " & ex.Message)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "error ejecutarConsultarProcesarIOT stackTrace : " & ex.StackTrace)
            Return False
        End Try
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ejecutarConsultarProcesarIOT")
        Return True

    End Function
    '//PROY-140379 INI
End Class