Imports System.Globalization

Public Class SICAR_FormaPagos
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnAutorizar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCerrar As System.Web.UI.WebControls.Button
    Protected WithEvents txtCliente As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNumFactSunat As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNumFact As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents Text1 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents Text2 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents Datagrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblEnvioPos As System.Web.UI.WebControls.Label
    Protected WithEvents txtNombCli As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtBolFact As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechPago As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNroPedido As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtMonto As System.Web.UI.HtmlControls.HtmlInputText
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
    Protected WithEvents HidApliPOS As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMonedaVisa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFlagGuardar As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoReporte As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents hdnFechaBusqueda As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidEstadoServicioPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnTipoCambioPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnTipoDocumento As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents numeroDocumentoHiden As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnPuntoDeVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnBinAdquiriente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodComercio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents intCanal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents intAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnRutaLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDetalleLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoPosVisa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoPosMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIdCabez As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoAuditPos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidSR As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIpData As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents pnFormaPago As System.Web.UI.WebControls.Panel
    Protected WithEvents hidMsjCajero As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected WithEvents HidNumIntentosAnular As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjErrorNumIntentos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjErrorTimeOut As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFlagCajaCerrada As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjCajaCerrada As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidNumIntentosPago As System.Web.UI.HtmlControls.HtmlInputHidden
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Public Recibos As String
    Dim dsDeuda As DataSet
    Public strUrlBack As String
    Public Const cteCODMONEDA_SOLES = "604"
    Public Const cteVALMONEDA_SOLES = "PEN"
    Public Const cteCODMONEDA_DOLARES = "840"
    Public Const cteVALMONEDA_DOLARES = "USD"
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecFijoPaginas")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecFijoPaginas")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strNumeroDeudaSAP As String
    'PROY-27440'-Inicio
    Private Sub load_data_param_pos()
        objFileLog.Log_WriteLog(pathFile, strArchivo, strArchivo & "[SICAR_FORMA_PAGOS]")

        Me.HidPtoVenta.Value = Funciones.CheckStr(Session("ALMACEN"))
        'CNH 2017-10-09 INI
        Dim strIpClient As String = Funciones.CheckStr(Session("IpLocal"))

        objFileLog.Log_WriteLog(pathFile, strArchivo, strArchivo & "load_data_param_pos : Validacion Integracion INI")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strArchivo & "load_data_param_pos : " & "HidPtoVenta : " & HidPtoVenta.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strArchivo & "load_data_param_pos : " & "strIpClient : " & strIpClient)


        Dim strCodRptaFlag As String = ""
        Dim strMsgRptaFlag As String = ""

        Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS

        'INI CONSULTA INTEGRACION AUTOMATICO POS

        Dim strFlagIntAut As String = ""

        strCodRptaFlag = "" : strMsgRptaFlag = ""
        objConsultaPos.Obtener_Integracion_Auto(Funciones.CheckStr(Me.HidPtoVenta.Value), strIpClient, String.Empty, strFlagIntAut, strCodRptaFlag, strMsgRptaFlag)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strArchivo & "load_data_param_pos : " & "strFlagIntAut : " & Funciones.CheckStr(strFlagIntAut))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strArchivo & "load_data_param_pos : " & "strCodRptaFlag : " & Funciones.CheckStr(strCodRptaFlag))

        'INI PROY-140126
        Dim MaptPath As String
        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
        MaptPath = "( Class : " & MaptPath & "; Function: load_data_param_pos)"
        objFileLog.Log_WriteLog(pathFile, strArchivo, strArchivo & "load_data_param_pos : " & "strMsgRptaFlag : " & Funciones.CheckStr(strMsgRptaFlag) & MaptPath)
        'FIN PROY-140126



        Me.HidIntAutPos.Value = Funciones.CheckStr(strFlagIntAut)

        'FIN CONSULTA INTEGRACION AUTOMATICO POS
        'CNH 2017-10-09 FIN
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
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Devoluciones" & " - " & "MENSAJE : " & ClsKeyPOS.strMsjCajaCerrada)
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


        ' Me.HidIntAutPos.Value = ClsKeyPOS.strIntegrAutoPOS

        Me.HidCodOpera.Value = ClsKeyPOS.strCodOpeVE & "|" & _
        ClsKeyPOS.strCodOpeVC & "|" & ClsKeyPOS.strCodOpeAN '01-VENTA|03-VENTA CUOTA|04-ANULACION

        Me.HidDesOpera.Value = ClsKeyPOS.strDesOpeVE & "|" & _
        ClsKeyPOS.strDesOpeVC & "|" & ClsKeyPOS.strDesOpeAN
        Me.HidTipoOpera.Value = ClsKeyPOS.strOpeFina & "|" & ClsKeyPOS.strNoFina
        Me.HidTipoTarjeta.Value = ClsKeyPOS.strCodTarjetaVS & "|" & ClsKeyPOS.strCodTarjetaMC

        Me.HidTipoPago.Value = ClsKeyPOS.strTipPagoRP '12 Devoluciones Procesadas

        Me.HidEstTrans.Value = ClsKeyPOS.strEstTRanPen & "|" & ClsKeyPOS.strEstTRanPro _
        & "|" & ClsKeyPOS.strEstTRanAce & "|" & ClsKeyPOS.strEstTRanRec & "|" & ClsKeyPOS.strEstTRanInc

        Me.HidTipoPOS.Value = ClsKeyPOS.strTipoPosVI & "|" & ClsKeyPOS.strTipoPosMC _
        & "|" & ClsKeyPOS.strTipoPosAM & "|" & ClsKeyPOS.strTipoPosDI

        Me.HidTipoTran.Value = ClsKeyPOS.strTipoTransPAG & "|" & ClsKeyPOS.strTipoTransANU & "|" _
        & ClsKeyPOS.strTipoTransRIM & "|" & ClsKeyPOS.strTipoTransRDO & "|" & _
        ClsKeyPOS.strTipoTransRTO & "|" & ClsKeyPOS.strTipoTransAPP & "|" & ClsKeyPOS.strTipoTransCIP

        Me.HidTransMC.Value = ClsKeyPOS.strTranMC_Compra & "|" & ClsKeyPOS.strTranMC_Anulacion & "|" _
        & ClsKeyPOS.strTranMC_RepDetallado & _
        "|" & ClsKeyPOS.strTranMC_RepTotales & "|" & ClsKeyPOS.strTranMC_ReImpresion & _
        "|" & ClsKeyPOS.strTranMC_Cierre & "|" & ClsKeyPOS.strPwdComercio_MC

        Me.HidMonedaMC.Value = ClsKeyPOS.strMonedaMC_Soles & "|" & ClsKeyPOS.strMonedaMC_Dolares

        Me.HidApliPOS.Value = ClsKeyPOS.strConstMC_POS

        Me.HidMonedaVisa.Value = ClsKeyPOS.strMonedaVisa_Soles & "|" & ClsKeyPOS.strMonedaVisa_Dolares

        Me.HidTipoMoneda.Value = ClsKeyPOS.strTipoMonSoles
        Me.hidMsjCajero.Value = ClsKeyPOS.strMsjValidacionCajero
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

        Dim objSicarDB As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim strMensaje As String = ClsKeyPOS.strIPMsjDesconfigurado
        'Dim strIp As String = CurrentTerminal()
        Dim strEstadoPos As String = "1"
        Dim strTipoVisa As String = "V"
        Dim ds As DataSet
        Me.HidDatoAuditPos.Value = Funciones.CheckStr(strIpClient) & "|" & _
        Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")) & "|" & _
        Funciones.CheckStr(Session("USUARIO"))

        objFileLog.Log_WriteLog(pathFile, strArchivo, strArchivo & "load_data_param_pos : " & "strIpClient : " & strIpClient)
        'VISA INICIO
        strTipoVisa = "V"
        ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoVisa)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.HidDatoPosVisa.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
            & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
            & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
            & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
            & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
            & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
            & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
            & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strArchivo & "load_data_param_pos : " & "HidDatoPosVisa : " & HidDatoPosVisa.Value)
        Else
            'strMensaje = "Ocurrió un error al tratar de recuperar los datos del POS. Comuníquese con soporte."
            Response.Write("<script>alert('" & strMensaje & "');</script>")
            HidIpData.Value = "E"
            Response.End()
        End If
        'VISA FIN

        'MC INICIO
        strTipoVisa = "M"
        ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoVisa)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.HidDatoPosMC.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
            & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
            & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
            & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
            & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
            & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
            & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
            & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strArchivo & "load_data_param_pos : " & "HidDatoPosMC : " & HidDatoPosMC.Value)
        Else
            'strMensaje = "Ocurrió un error al tratar de recuperar los datos del POS. Comuníquese con soporte."
            Response.Write("<script>alert('" & strMensaje & "');</script>")
            HidIpData.Value = "E"
            Response.End()
        End If
        'MC FIN

    End Sub
    'PROY-27440 FIN
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
            Dim strtipo As String = Request.QueryString("pacc")
            Dim strfecha As String = Request.QueryString("pfecha")
            Dim strid As String = Request.QueryString("pid")
            Dim strNombre As String = Request.QueryString("pnombre")
            Dim strMonto As String = Request.QueryString("pmonto")
            strUrlBack = Request.UrlReferrer.ToString()
            If Not Page.IsPostBack Then
                'PROY-27440'-Inicio
                load_data_param_pos()
                'PROY-27440'-Fin
                txtMonto.Value = strMonto.ToString
                txtBolFact.Value = strid.ToString.Trim
                txtFechPago.Value = strfecha.ToString
                txtNombCli.Value = strNombre.ToString
                hdnFechaBusqueda.Value = txtFechPago.Value
                numeroDocumentoHiden.Value = txtBolFact.Value
                If Not Request.QueryString("es") Is Nothing AndAlso Request.QueryString("es") <> String.Empty Then
                    hidEstadoServicioPago.Value = Request.QueryString("es")
                Else
                    hidEstadoServicioPago.Value = String.Empty
                End If
            End If
            Me.RegisterStartupScript("VerificarSwitch", "<script language=javascript>f_InicializarDatos('" & strtipo & "');</script>")  'PROY-27440'
        End If
    End Sub

    Private Sub btnCancelar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.ServerClick

        Response.Redirect("grdDocumentos.aspx?fecbus=" + hdnFechaBusqueda.Value.Trim())

    End Sub


    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        'strFechaTrans.ToShortDateString() = DateTime.Today.ToShortDateString()
        Dim sMensaje As String = String.Empty
        
        Dim sPaginaInicial As String = "grdDocumentos.aspx"
        Dim strIdentifyLog As String = Me.txtNroPedido.Value
        'validacion de caja
        '***** INI :: Valida la asignacion de caja *****
        Dim objOfflineCaja As New COM_SIC_OffLine.clsOffline
        Dim strPedido As String
        Dim strFecha As String
        Dim strNombre As String
        Dim strMontoPago As String
        Dim strFechaTrans As Date
        Dim dsCajeroA As DataSet
        strFechaTrans = txtFechPago.Value
        strIdentifyLog = txtBolFact.Value
        strPedido = txtNroPedido.Value
        strNombre = txtNombCli.Value
        strMontoPago = txtMonto.Value
        'Dim sFecha As String = Convert.ToDateTime(strFechaTrans).ToLocalTime.ToShortDateString

        Dim sFecha As String = String.Format("{0:dd/MM/yyyy}", strFechaTrans)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strFechaTrans : " & strFechaTrans)

        Dim sCajero As String = Funciones.CheckStr(Session("USUARIO")).PadLeft(10, "0")
        Dim sOficina As String = Funciones.CheckStr(Session("ALMACEN"))
        Dim strEstadoTrans = hidEstadoServicioPago.Value
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-[INICIO]Grabar Anulacion SICAR_FormaPagos.")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-[PEDIDO]=> " & strPedido)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-[NOMBRE]=> " & strNombre)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-[MONTO]=> " & strMontoPago)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-[ESTADO PAGO]=> " & strEstadoTrans)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-[CAJERO]=> " & sCajero)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-[OFICINA]=> " & sOficina)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "*************[INICIO VALIDACION]*************")
        If (strFechaTrans <> DateTime.Today.ToShortDateString()) Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Error al Anular,la fecha de pago no es del día.")

            If HidIpData.Value = "E" Then
                Response.Write("<script>alert('Erro al Anular,la fecha de pago no es del día.');document.location='grdDocumentos.aspx?fecbus=" & hdnFechaBusqueda.Value.Trim() & "';</script>")
            Else
                Response.Write("<script>alert('Erro al Anular,la fecha de pago no es del día.');</script>")
            End If

            Exit Sub
        End If
        dsCajeroA = objOfflineCaja.GetDatosAsignacionCajero(sOficina, sFecha, sCajero)
        If (dsCajeroA Is Nothing OrElse dsCajeroA.Tables(0).Rows.Count <= 0) Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Cantidad de Registros devueltos: " & dsCajeroA.Tables(0).Rows.Count.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- No se pudo determinar el numero de la caja asignada.")
            Response.Write("<script>alert('No se encontro el Nùmero/Nombre de caja asignada.');document.location='" & sPaginaInicial & "';</script>")
            Exit Sub
        End If
        If Not dsCajeroA Is Nothing Then
            For cont As Int32 = 0 To dsCajeroA.Tables(0).Rows.Count - 1
                If dsCajeroA.Tables(0).Rows(cont).Item("CAJA_CERRADA") = "S" Then
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE : " & "Caja Cerrada, no es posible realizar el pago.")
                    Dim script = String.Format("<script language=javascript>alert('{0}');document.location='" & sPaginaInicial & "';</script>", "Caja Cerrada, no es posible realizar el pago.")
                    Me.RegisterStartupScript("RegistraAlerta", script)
                    Exit Sub
                End If
            Next
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "*************[FIN VALIDACION]*************")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Anular cmdAnular_ServerClick")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")

        Try
            Dim obAnul As New COM_SIC_Recaudacion.clsAnulaciones
            Dim intAutoriza As Integer
            Dim objConf As New COM_SIC_Configura.clsConfigura

            If hidEstadoServicioPago.Value = "" Then
                hidEstadoServicioPago.Value = "1"
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Anular Pago - Inicio")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio  FP_Inserta_Aut_Transac ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    INP CANAL:  " & Session("CANAL"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    INP ALMACEN:  " & Session("ALMACEN"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    INP USUARIO:  " & Session("USUARIO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    INP NOMBRE_COMPLETO:  " & Session("NOMBRE_COMPLETO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    INP txtNumeroDeudaSAP:  " & txtBolFact.Value)


            ''CALLBACK ORACLE

      'CNH-INI
      intAutoriza = 1
      'CNH-FIN


            If intAutoriza = 1 Then
                Dim dTipoCambio As Double = Funciones.CheckDbl(hdnTipoCambioPago.Value)
                '''CALLBACK ORACLE
                Dim strResult As String = obAnul.AnularPago(ConfigurationSettings.AppSettings("CodAplicacion"), Session("CANAL"), Me.hdnRutaLog.Value, _
                                        Me.hdnDetalleLog.Value, Me.hdnPuntoDeVenta.Value, Me.intCanal.Value, _
                                        Me.hdnBinAdquiriente.Value, Me.hdnCodComercio.Value, Me.hdnUsuario.Value, _
                                        Me.numeroDocumentoHiden.Value)

                '---logs de recibos procesados
                If Not obAnul.sbLineasLog Is Nothing Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, obAnul.sbLineasLog.ToString())
                End If
                obAnul.sbLineasLog = Nothing '//reinicializa
                '--Más logs
                'INI PROY-140126
                Dim MaptPath As String
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: btnGrabar_Click)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT strResult:  " & strResult & MaptPath)
                'FIN PROY-140126
                Dim arrMensaje() As String = strResult.Split("@")

                If ExisteError(arrMensaje) Then
                    If HidSR.Value = "1" Then
                        Response.Write("<SCRIPT> alert('" & arrMensaje(1) & "');document.location='" & sPaginaInicial & "'; </script>")
                    Else
                        Response.Write("<SCRIPT> alert('" & arrMensaje(1) & "'); </script>")
                    End If
                    sMensaje = "Hubo un error. " & arrMensaje(1)
                Else
                    '//E75810


                    '---
                    sMensaje = "Se procesó exitósamente la anulación."
                    Response.Write("<SCRIPT> alert('Se procesó exitosamente la anulación.'); document.location='" & sPaginaInicial & "';</SCRIPT>")
                End If
            Else
                sMensaje = "Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador."
                Response.Write("<script language=jscript> alert('" & sMensaje & "');document.location='" & sPaginaInicial & "'; </script>")
            End If

        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: btnGrabar_Click)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR: " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

            sMensaje = "Hubo un error. " & ex.Message
            Response.Write("<script language=jscript> alert('" & ex.Message & "');document.location='" & sPaginaInicial & "'; </script>")
        Finally
            sMensaje = "Anulación de Recaudación. " & sMensaje & ". Datos: Canal=" & intCanal.Value & "|PDV=" & hdnPuntoDeVenta.Value & "|Cajero=" & _
                                    hdnUsuario.Value & "|Nro. Documento SAP=" & Me.txtNroPedido.Value
            '---
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Anular Pago - Fin")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Anular cmdAnular_ServerClick")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[FIN] Anular FormaPagos SICAR")
            objFileLog = Nothing
            '--registra auditoria
            RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("ConsOperFinNoFin_codTrsAuditoria"))
        End Try
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
End Class
