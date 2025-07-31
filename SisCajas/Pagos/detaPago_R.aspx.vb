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



Public Class detaPago_R
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cboTipDocumento1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboTipDocumento2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboTipDocumento3 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboTipDocumento4 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboTipDocumento5 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNomCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNeto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCuotaIni As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSaldo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCorrelativo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCompleto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumSunat As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto5 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDoc1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDoc2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDoc3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDoc4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDoc5 As System.Web.UI.WebControls.TextBox
    Protected WithEvents hidNumFilas As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents RefProp As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents RefImp As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents txtTarjCred As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBIN As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTipoCambio As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecibidoPen As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtVuelto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecibidoUsd As System.Web.UI.WebControls.TextBox
    Protected WithEvents divPagos As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents txtNotaCred As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents txtFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnCargarDocumento As System.Web.UI.WebControls.Button
    Protected WithEvents lblArchivo As System.Web.UI.WebControls.Label
    Protected WithEvents imgElimDoc As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ddlTipDoc As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTipDoc As System.Web.UI.WebControls.Label
    Protected WithEvents hidTipoDocDOL As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblArchivoDOL As System.Web.UI.WebControls.Label
    Protected WithEvents hidFlagCargaDoc As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidFlagCargaDocDOL As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidFlagRenovacionRMP As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidFlagVentaCuota As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidMontoCuota As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidDocSap As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidMensajeNC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidNroSunatEntero As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidNumeroTelefono As System.Web.UI.HtmlControls.HtmlInputHidden

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
    Protected WithEvents HidFila4 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFila5 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents LnkPos1 As System.Web.UI.HtmlControls.HtmlAnchor

    Protected WithEvents icoTranPos1 As System.Web.UI.HtmlControls.HtmlImage

    Protected WithEvents HidTipoMoneda As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTransMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMonedaMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidApliPOS As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMonedaVisa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblEnvioPos As System.Web.UI.WebControls.Label

    Protected WithEvents HidDatoPosVisa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoPosMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoAuditPos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIdCabez As System.Web.UI.HtmlControls.HtmlInputHidden


    Protected WithEvents HidIntAutPosMC As System.Web.UI.HtmlControls.HtmlInputHidden

    'PROY-27440 FIN

    Protected WithEvents hidF1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidF2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidF3 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidF4 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidF5 As System.Web.UI.HtmlControls.HtmlInputHidden


    Protected WithEvents pnlPagos As System.Web.UI.WebControls.Panel
    Protected WithEvents cmdConsultaNC As System.Web.UI.WebControls.Button
    Protected WithEvents chkImprimir As System.Web.UI.WebControls.CheckBox

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
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim dsFormaPago As DataSet
    'MODIFICADO EMANUEL BARBARAN EVERIS PERU
    Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
    Dim objTrsMsSap As New COM_SIC_Activaciones.clsTrsMsSap

    '*************************************************************************************************'
    'VARIABLES FLUJO CAJA'
    Dim objCajas As COM_SIC_Cajas.clsCajas            '*** para el registro de datos flujo de caja ***'
    Dim PEDIN_PEDIDOSAP As String = ""

    Dim arrayListaRefSunat(7) As String                 '** Correlativo generado NC         **'
    Dim arrayListaReferencia(7) As String               '** Numero SAP de la NC             **'
    Dim arrayListaRefPedido(7) As String                '** Numero pedido de la NC          **'

    Dim resCajas As Integer = 0
    Dim P_CODERR As String = ""
    Dim P_MSGERR As String = ""
    Dim strNombreCaja As String = ""
    Dim P_ID_TI_VENTAS_FACT As String = ""
    Dim fechaCajas As String = ""
    Dim dsDatosPedido As DataSet
    Dim objOfflineCaja As COM_SIC_OffLine.clsOffline
    Dim dsCajeroA As DataSet
    Dim dsCajeroB As DataSet
    '*************************************************************************************************'

    Dim objPagos As New SAP_SIC_Pagos.clsPagos
    Dim objVentas As New SAP_SIC_Ventas.clsVentas
    Dim objSapCajas As New SAP_SIC_Cajas.clsCajas
    Dim drPagos As DataRow
    Dim blnError As Boolean
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRentaCac")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim numeroTelefono As String = ""
    Dim numeroReferenciaSunat$ = ""
    Dim montoRecarga As String = String.Empty
    Dim numeroOperacion$
    Dim numeroOperacionST$

    Dim desdeRVFrecuente As Boolean = False
    'PROY-27440 INI
    Dim nameFilePos As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivoPos As String = objFileLog.Log_CrearNombreArchivo(nameFilePos)
    'PROY-27440 FIN
    Dim arrParametrosFormaPagoPerfil As ArrayList   'INICIATIVA-318
    Dim strIdentifyLogGeneral As String = "" ' INICIATIVA-318
    Dim strCodPerfilFormaPago As String = ""  ' INICIATIVA-318

    'PROY-27440 INI
    Private Sub load_values_pos()
        Dim strTipoPago As String = Me.HidTipoPago.Value
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "load_values_pos : " & "strTipoPago: " & strTipoPago)
        Dim strPedidoLog As String = Devolver_TipoPago_POS(strTipoPago) & "Identificador : [" & Funciones.CheckStr(Request.QueryString("pIdent")) & "] "

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_values_pos : " & "Inicio")
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

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "load_values_pos : " & "HidFila1 : " & HidFila1.Value)

    End Sub

    Private Sub load_data_param_pos()
        Dim strPedidoLog As String = "Pedido: [" & Funciones.CheckStr(Request.QueryString("pDocSap")) & "] "

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "Inicio : ")

        Me.HidPtoVenta.Value = Funciones.CheckStr(Session("ALMACEN"))

        Dim strIpClient As String = Funciones.CheckStr(Session("IpLocal"))

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : Validacion Integracion INI")
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidPtoVenta : " & HidPtoVenta.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIpClient : " & strIpClient)


        Dim strCodRptaFlag As String = ""
        Dim strMsgRptaFlag As String = ""

        Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS

        'INI CONSULTA INTEGRACION AUTOMATICO POS

        Dim strFlagIntAut As String = ""

        strCodRptaFlag = "" : strMsgRptaFlag = ""
        objConsultaPos.Obtener_Integracion_Auto(Funciones.CheckStr(Me.HidPtoVenta.Value), strIpClient, String.Empty, strFlagIntAut, strCodRptaFlag, strMsgRptaFlag)

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strFlagIntAut : " & Funciones.CheckStr(strFlagIntAut))
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strCodRptaFlag : " & Funciones.CheckStr(strCodRptaFlag))
        'INI PROY-140126
        Dim MaptPath As String
        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
        MaptPath = "( Class : " & MaptPath & "; Function: load_data_param_pos)"
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strMsgRptaFlag : " & Funciones.CheckStr(strMsgRptaFlag) & MaptPath)
        'FIN PROY-140126

        Me.HidIntAutPos.Value = Funciones.CheckStr(strFlagIntAut)

        'FIN CONSULTA INTEGRACION AUTOMATICO POS

        'INI CONSULTA PAGO AUTOMATICO POS

        Dim strFlagPagAut As String = ""

        objConsultaPos.Obtener_Pago_Auto(Funciones.CheckStr(Me.HidPtoVenta.Value), strIpClient, String.Empty, strFlagPagAut, strCodRptaFlag, strMsgRptaFlag)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strFlagPagAut : " & Funciones.CheckStr(strFlagPagAut))
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strCodRptaFlag : " & Funciones.CheckStr(strCodRptaFlag))
        'INI PROY-140126
        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
        MaptPath = "( Class : " & MaptPath & "; Function: load_data_param_pos)"
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strMsgRptaFlag : " & Funciones.CheckStr(strMsgRptaFlag) & MaptPath)
        'FIN PROY-140126

        Me.HidGrabAuto.Value = Funciones.CheckStr(strFlagPagAut)

        'FIN CONSULTA PAGO AUTOMATICO POS
        'PROY-31949 - Inicio
        HidNumIntentosPago.Value = ClsKeyPOS.strNumIntentosPago
        HidNumIntentosAnular.Value = ClsKeyPOS.strNumIntentosAnular
        HidMsjErrorNumIntentos.Value = ClsKeyPOS.strMsjErrorNumIntentos
        HidMsjErrorTimeOut.Value = ClsKeyPOS.strMsjErrorTimeOut
        HidMsjNumIntentosPago.Value = ClsKeyPOS.strMsjPagoNumIntentos

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
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : Validacion Integracion FIN")

        Me.HidCodOpera.Value = ClsKeyPOS.strCodOpeVE & "|" & _
        ClsKeyPOS.strCodOpeVC & "|" & ClsKeyPOS.strCodOpeAN

        Me.HidDesOpera.Value = ClsKeyPOS.strDesOpeVE & "|" & _
        ClsKeyPOS.strDesOpeVC & "|" & ClsKeyPOS.strDesOpeAN
        Me.HidTipoOpera.Value = ClsKeyPOS.strOpeFina & "|" & ClsKeyPOS.strNoFina 'OPE FI(90)
        Me.HidTipoTarjeta.Value = ClsKeyPOS.strCodTarjetaVS & "|" & ClsKeyPOS.strCodTarjetaMC

        If Not Request.QueryString("docSunat") Is Nothing Then
            Me.HidTipoPago.Value = ClsKeyPOS.strTipPagoDxP '03 Documentos por pagar
        Else
            Me.HidTipoPago.Value = ClsKeyPOS.strTipPagoVR '01 Venta Rapida
        End If

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidIntAutPos : " & HidIntAutPos.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidGrabAuto : " & HidGrabAuto.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidCodOpera : " & HidCodOpera.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidTipoOpera : " & HidCodOpera.Value)

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

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIpClient : " & strIpClient)

            'VISA INICIO
            strTipoVisa = "V"

            Dim arrIPDesc(2) As String
            arrIPDesc(0) = strIpClient

            ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoVisa)

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIp : " & strIp)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIpClient : " & strIpClient)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidPtoVenta : " & HidPtoVenta.Value)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strEstadoPos : " & strEstadoPos)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strTipoVisa : " & strTipoVisa)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos Visa : " & "Count : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

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

                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidDatoPosVisa : " & HidDatoPosVisa.Value)
            Else
                bvalida = 1
                Response.Write("<script>alert('" & strMensajeVisa & "');</script>")
            End If
            'VISA FIN

            'MC INICIO
            strTipoVisa = "M"
            ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoVisa)

            Dim strMensajeMC As String = ClsKeyPOS.strIPMsjDesconfigurado

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIp : " & strIp)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidPtoVenta : " & HidPtoVenta.Value)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strEstadoPos : " & strEstadoPos)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strTipoVisa : " & strTipoVisa)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos MC : " & "Count : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

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

                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidDatoPosMC : " & HidDatoPosMC.Value)
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
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "Fin : ")
    End Sub
    Private Sub validar_pedido_pos()
        Dim strPedidoLog As String = "Pedido: [" & Funciones.CheckStr(Request.QueryString("pDocSap")) & "] "

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Inicio : ")

        Dim dsPedidoPOS As DataSet
        Dim objPedidoPOS As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim strNroPedido As String = Funciones.CheckStr(Request.QueryString("pDocSap"))
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
                    cboTipDocumento1.SelectedIndex = 0
                Else
                    hddComboAutorizador.Value = ""
                    hddEnvioAutorizador.Value = ""
                    cboTipDocumento1.SelectedValue = hddcboFormaPago.Value
                End If
            End If
            If (hddComboAutorizador.Value = "cboTipDocumento3") Then
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
            If (hddComboAutorizador.Value = "cboTipDocumento4") Then
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
            If (hddComboAutorizador.Value = "cboTipDocumento5") Then
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
            'INICIATIVA-318 FIN
            ConsultaParametrosFormaPagoPerfil(strIdentifyLogGeneral) ' INICIATIVA-318

            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio de la pàgina formas de pago.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio Page_Load")

            'cboTipDocumento1.Attributes.Add("onChange", "f_CambiaArti()")

            '**IDG***************************************************************************************
            If Not IsPostBack Then

                'PROY-27440 INI
                Me.load_data_param_pos()
                'PROY-27440 FIN


                txtDoc1.Attributes.Add("onBlur", "f_ConsultaNC()")
                ''txtDoc2.Attributes.Add("onBlur", "f_ConsultaNC()")
                ''txtDoc3.Attributes.Add("onBlur", "f_ConsultaNC()")
                ''txtDoc4.Attributes.Add("onBlur", "f_ConsultaNC()")
                ''txtDoc5.Attributes.Add("onBlur", "f_ConsultaNC()")
                'txtMonto1.Attributes.Add("onBlur", "f_ConsultaNC()")
                'txtMonto2.Attributes.Add("onBlur", "f_ConsultaNC()")
                'txtMonto3.Attributes.Add("onBlur", "f_ConsultaNC()")
                'txtMonto4.Attributes.Add("onBlur", "f_ConsultaNC()")
                'txtMonto5.Attributes.Add("onBlur", "f_ConsultaNC()")
            End If
            '**FDG***************************************************************************************





            Dim strCorrelativo As String
            Dim strCompleto As String

            Dim dsReturn As DataSet
            Dim dsPedido As DataSet
            Dim i As Integer
            drPagos = Session("DocSel")
            '--IDENTIFICADOR DEL LOG :-----------------------------------------------------------------------------------------------------------------------------------------------------------------
            Dim strIdentifyLog As String = IIf(Session("recargaVirtual"), String.Format("--[{0}]--", numeroOperacion), Funciones.CheckStr(drPagos.Item("PEDIN_NROPEDIDO")))
            '-------------------------------------------------------------------------------------------------------------------------------------------------------------------

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio asignaciòn de variables por POST.")
            If Not Request.QueryString("pDocSap") Is Nothing Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Var numeroOperacion : " & Request.QueryString("pDocSap"))
                numeroOperacion = Request.QueryString("pDocSap")
            End If

            If Not Request.QueryString("numeroTelefono") Is Nothing Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Var numeroTelefono : " & Request.QueryString("numeroTelefono"))
                numeroTelefono = Request.QueryString("numeroTelefono")
            End If

            If Not Request.QueryString("montoRecarga") Is Nothing Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Var montoRecarga : " & Request.QueryString("montoRecarga"))
                montoRecarga = Request.QueryString("montoRecarga")
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin asignaciòn de variables por POST.")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Asigna Fecha a la variable Session(FechaPago): " & String.Format("{0:dd/MM/yyyy}", DateTime.Now).ToString)
            Session("FechaPago") = String.Format("{0:dd/MM/yyyy}", DateTime.Now)



            ''JTN INICIO
            Dim TIPO_VENTA, NRO_CUOTAS, NUMERO_CONTRATO, ARTICULO, TIPO_DOC_CLIENTE As String
            Dim NRO_APROBACION$ = ""
            Dim CLASE_VENTA$ = "01"

            '***********************************************************************************'
            '**ESTA PUESTO EN DURO:
            NRO_CUOTAS = "00"
            TIPO_VENTA = "02"
            NUMERO_CONTRATO = ""
            ARTICULO = "SERECVILIM"
            TIPO_DOC_CLIENTE = "01"


            Me.hidDocSap.Value = numeroOperacion
            Me.hidNumeroTelefono.Value = numeroTelefono
            ''JTN FIN

            'AVILLAR

            Dim objOffline As New COM_SIC_OffLine.clsOffline
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Verifica si el pedido es una recarga virtal.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Var recargaVirtual: " & Funciones.CheckStr(Session("recargaVirtual")))
            Dim isRecargaVirtual As Boolean = Session("recargaVirtual")


            '*** FORMAS DE PAGO *******************************************************************************************************************************************
            Dim PuntoVentaSinergia As String = ""

            Try
                PuntoVentaSinergia = Funciones.CheckStr(objConsultaMsSap.ConsultaPuntoVenta(Session("ALMACEN")).Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA"))
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al consulta los Puntos de Venta para Sinergia")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error: " & PuntoVentaSinergia)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & ex.Message.ToString)
                PuntoVentaSinergia = ""
            End Try

            If PuntoVentaSinergia = "" Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PuntoVentaSinergia: " & PuntoVentaSinergia)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MEnsaje: No existe Codigo de Punto de Venta Nuevo.")
                Response.Write("<script>alert('No existe Codigo de Punto de Venta.')</script>")
                Exit Sub
            End If


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio consulta las formas de Pago")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Método: Obtener_ConsultaViasPago(USRSICAR.PCK_SICAR_OFF_SAP.PROC_DATOS_VIAS_PAGO)")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parámetros: ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param1: " & PuntoVentaSinergia)
            dsFormaPago = objOffline.Obtener_ConsultaViasPago(Session("ALMACEN"))

            Session("Vias_Pago") = Nothing
            Session("Vias_Pago") = dsFormaPago
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta las formas de Pago")
            '**************************************************************************************************************************************************************

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio asignaciòn de variables: ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nombre del Cliente: " & Funciones.CheckStr(drPagos.Item("PEDIV_NOMBRECLIENTE")))
            txtNomCliente.Text = drPagos.Item("PEDIV_NOMBRECLIENTE")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Valor Neto: " & Funciones.CheckStr(Format(drPagos.Item("INPAN_TOTALDOCUMENTO"), "###0.00")))
            txtNeto.Text = Format(drPagos.Item("INPAN_TOTALDOCUMENTO"), "###0.00")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Valor Cuota Inicial: " & Funciones.CheckStr(Format(drPagos.Item("PAGON_INICIAL"), "###0.00")))
            txtCuotaIni.Text = Format(drPagos.Item("PAGON_INICIAL"), "###0.00")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin asignaciòn de variables: ")

            Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia asignaciòn del saldo - pedido")
                If CDbl(drPagos.Item("PEDIN_PAGO")) <> 0 Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Var INPAN_TOTALDOCUMENTO: " & Funciones.CheckStr(Format(drPagos.Item("INPAN_TOTALDOCUMENTO"), "###0.00")))
                    txtSaldo.Text = Format(drPagos.Item("INPAN_TOTALDOCUMENTO"), "###0.00")
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Var PEDIN_SALDO: " & Funciones.CheckStr(Format(drPagos.Item("PEDIN_SALDO"), "###0.00")))
                    txtSaldo.Text = Format(drPagos.Item("PEDIN_SALDO"), "###0.00")
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin asignaciòn del saldo - pedido")
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al tratar de asignar el saldo - pedido")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & ex.Message.ToString)
            End Try


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia validaciòn si el pedido seleccionado es una NC")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "isNotaCredigo : " & Funciones.CheckStr(Session("NC")))
            Dim isNotaCredigo As Boolean = Session("NC")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Termina validaciòn si el pedido seleccionado es una NC")

            If Not Page.IsPostBack Then
                'INICIATIVA-318 INI
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicializa los controles")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Método: inicializaControles")
                Me.inicializaControles()
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin controles")
                'INICIATIVA-318 FIN

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia asignaciòn el importe total")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INPAN_TOTALDOCUMENTO : " & Funciones.CheckStr(Format(drPagos.Item("INPAN_TOTALDOCUMENTO"), "###0.00")))
                txtMonto1.Text = Format(drPagos.Item("INPAN_TOTALDOCUMENTO"), "###0.00")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin asignaciòn el importe total")

                Session("Carga") = 0
                hidFlagCargaDoc.Value = "0"

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Asigna(PEDIN_NRODOCUMENTO): " & Funciones.CheckStr(Request.QueryString("pDocSap")))
                Dim PEDIN_NRODOCUMENTO As Int64 = Request.QueryString("pDocSap")

                'Prepago cuotas 23/10/2006

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Evalua si el pedido a procesar no es una RV.")
                If Not isRecargaVirtual Then
                    'dsPedido = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drPagos.Item("PEDIV_PEDIDOSAP"), "") ''TODO: CALLBACK SAP
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "El pedido No es una RV")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicial consulta del Pedido(objConsultaMsSap.ConsultaPedido -  PKG_MSSAP.SSAPSS_PEDIDO)")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Paràmetros: ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param1: " & Funciones.CheckStr(PEDIN_NRODOCUMENTO))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param2: " & "")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param3: " & "")
                    dsPedido = objConsultaMsSap.ConsultaPedido(PEDIN_NRODOCUMENTO, "", "")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta del Pedido.")

                    Try
                        If Not dsPedido Is Nothing Then
                            NRO_CUOTAS = 0
                            'TIPO_VENTA = dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") TODOE
                            TIPO_VENTA = drPagos.Item("PEDIC_TIPOVENTA")
                            'NUMERO_CONTRATO = CheckStr(dsPedido.Tables(0).Rows(0).Item("NUMERO_CONTRATO")) TODOE
                            TIPO_DOC_CLIENTE = dsPedido.Tables(0).Rows(0)("CLIEC_TIPODOCCLIENTE")
                        Else
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "No se encontraron registros para el pedido: " & Funciones.CheckStr(PEDIN_NRODOCUMENTO))
                        End If
                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al asignar los valores del pedido.")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Err. " & ex.Message.ToString)
                    End Try
                End If

                'si es venta prepago y ademas tiene cuotas
                If TIPO_VENTA = ConfigurationSettings.AppSettings("strTVPrepago") And CDbl(NRO_CUOTAS) > 0 Then
                    txtMonto1.Text = Math.Round(CDbl(txtMonto1.Text) * CDbl(ConfigurationSettings.AppSettings("gConstPorcPrePago")) / 100, 2)
                ElseIf TIPO_VENTA = ConfigurationSettings.AppSettings("strTVPostpago") And CheckInt64(NRO_CUOTAS) > 0 Then
                    Try
                        Dim nroContrato As String = NUMERO_CONTRATO     '** dg: verificar en el datos del pedido **'
                        Dim nroSEC As Int64
                        Dim objCajas As New COM_SIC_Cajas.clsCajas
                        Dim dblCuotaInicial As Double
                        Dim dsAcuerdo As New DataSet

                        'dsAcuerdo = objPagos.Get_ConsultaAcuerdoPCS(nroContrato) ''TODO: CALLBACK SAP PARA OBTENER EL NUMERO DE APROBACION
                        'nroSEC = CheckInt64(NRO_APROBACION)

                        '***** VERIFICA SI ES UNA VENTA EN CUOTAS ******'
                        '***** Verificar la NroSEC *********************'
                        Dim flagVentaCuota As String = objCajas.Consulta_Venta_Cuota(nroSEC, dblCuotaInicial)
                        If flagVentaCuota = "1" Then
                            hidFlagVentaCuota.Value = flagVentaCuota
                            hidMontoCuota.Value = dblCuotaInicial
                            txtCuotaIni.Text = Format(dblCuotaInicial, "###0.00")
                            txtMonto1.Text = Format(dblCuotaInicial, "###0.00")
                        End If

                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR Consulta_Venta_Cuota:" & ex.Message.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR Consulta_Venta_Cuota:" & ex.StackTrace.ToString())
                        hidFlagVentaCuota.Value = "0"
                    End Try
                End If

                hidFlagRenovacionRMP.Value = "N"

                If Not Session("msgCaducidadRMP6") Is Nothing And Session("msgCaducidadRMP6") <> "" Then
                    Response.Write("<script>alert('" & Session("msgCaducidadRMP6") & "')</script>")
                    Session.Remove("msgCaducidadRMP6")
                End If

                txtRecibidoPen.Text = txtMonto1.Text
                txtRecibidoUsd.Text = "0.00"
                txtVuelto.Text = "0.00"
                ''TODO: CAMBIADO POR JYMMY TORRES
                Me.txtTipoCambio.Text = objOffline.Obtener_TipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000")).ToString("N3") 'aotane 05.08.2013
                ''CAMBIADO HASTA AQUI

                ''TODO: CARGA LAS TARJETAS DE CREDITO
                LeeDatosValidar()

            End If

            If Trim(Session("CodImprTicket")) <> "" Then
                txtNumSunat.CssClass = "clsInputDisable"
                txtNumSunat.ReadOnly = True
                chkImprimir.Checked = True
                chkImprimir.Enabled = False
            End If

            btnGrabar.Attributes.Add("onClick", "f_autoAsignacionCaja();f_Grabar();") 'INICIATIVA-318 - INICIATIVA-529

            If (Not IsPostBack) Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Get_NumeroSUNAT(ZPVU_RFC_TRS_GET_NRO_SUNAT)")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp ALMACEN: " & Session("ALMACEN"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp ClaseFactura(PEDIC_CLASEFACTURA): " & Funciones.CheckStr(drPagos.Item("PEDIC_CLASEFACTURA"))) 'TODOEB

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp CodImprTicket: " & Session("CodImprTicket"))

                Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))



                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out strCompleto(Referencia): " & strCompleto)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out strCorrelativo(Ultimo numero): " & strCorrelativo)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Get_NumeroSUNAT")

                txtCorrelativo.Text = strCorrelativo
                txtCompleto.Text = strCompleto

            End If

            'PAGOC_CODSUNAT()
            'TODOEB()
            RefProp.Visible = True
            RefImp.Visible = True


            Me.DataBind()
            'SeleccionarEfectivo(cboTipDocumento1)
            If Not IsPostBack Then 'INICIATIVA-318
                cboTipDocumento1.Items.Insert(0, "") 'INICIATIVA-318
            cboTipDocumento2.Items.Insert(0, "")
            cboTipDocumento3.Items.Insert(0, "")
            cboTipDocumento4.Items.Insert(0, "")
            cboTipDocumento5.Items.Insert(0, "")

            '***IDG*****************************************'
            ValidaFormasPagoAsignadas()
            '***FDG*****************************************'
            End If 'INICIATIVA-318

            '*** Si es NC o TG, no se va a mostrar el detalle de la formas de pago:
            Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Se identifica el tipo de documento que se esta procesando.")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Para el caso de NC/TG, se oculta el detalle de formas de pago.")
                If dsPedido Is Nothing Then
                    dsPedido = objConsultaMsSap.ConsultaPedido(Funciones.CheckInt64(numeroOperacion), "", "")
                End If

                '******************************************************************************************************************************************************************************************************************************************************************************************************************************************'
                If ((Funciones.CheckDbl(dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")) = 0 And dsPedido.Tables(0).Rows(0).Item("PEDIC_ESQUEMACALCULO") = ConfigurationSettings.AppSettings("ESQUEMACALCULO_TG") And dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA") <> ConfigurationSettings.AppSettings("strTipDoc")) Or _
                    (Funciones.CheckDbl(dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")) >= 0 And dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA") = ConfigurationSettings.AppSettings("strTipDoc"))) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Oculta las formas de pago.")
                    divPagos.Visible = False
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Muestra las formas de pago.")
                    divPagos.Visible = True
                End If
                '******************************************************************************************************************************************************************************************************************************************************************************************************************************************'

                'PROY-27440 INI
                If Not IsPostBack Then
                    Me.validar_pedido_pos()
                Else
                    load_values_pos()
                End If

                'PROY-27440 FIN

            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Error al tratar de ocultar las formas de pago")
            End Try
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Page_Load")
    End Sub

    Private Sub ValidaFormasPagoAsignadas()
        '** IDG ********************************************************************
        '*** validaciòn para que surge por la forma de pago Nota Crédito ***'
        Try
            If Me.txtDoc1.Text.Trim.ToString.Length = 0 Then

        '24740 INI
        Dim viasPagoPOS As String() = ConfigurationSettings.AppSettings("constForPagoPOS").Split(CChar(";"))
        If hidF1.Value = "" Then
          Me.cboTipDocumento1.SelectedValue = "ZEFE"
                    'INICIATIVA-318 INI
                    'INI-PROY-140773- SICAR
                    'Dim bolAutorizado As Boolean
                    'Dim strFormaPago As String
                    'Dim strCombo As String
                    hddcboFormaPago.Value = cboTipDocumento1.SelectedValue
                    'strFormaPago = cboTipDocumento1.SelectedValue
                    'strCombo = "cboTipDocumento1"
                    'bolAutorizado = ObtenerAprobacionFormaPago(cboTipDocumento1.SelectedValue)

                    'If (bolAutorizado = False) Then
                    '    cboTipDocumento1.SelectedIndex = 0
                    '    Response.Write("<script language=javascript>alert('Usuario no cuenta con los permisos sufiientes para la forma de Pago');</script>")
                    '    Response.Write("<script language=javascript>window.open('frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")

                    'End If
                    'INI-PROY-140773- SICAR
                    'INICIATIVA-318 FIN
        Else
          For Each item As String In viasPagoPOS
            If Funciones.CheckStr(hidF1.Value) = item Then
              cboTipDocumento1.SelectedValue = hidF1.Value
              Exit For
            End If
          Next
        End If
        '24740 FIN


            Else
                If txtDoc1.Text.Trim.ToString.Length > 0 Then
                    If Me.txtDoc1.Text.Trim.ToString.Substring(0, 1) = "B" Or Me.txtDoc1.Text.Trim.ToString.Substring(0, 1) = "F" Then
                        cboTipDocumento1.SelectedValue = "ZNCR"
                    Else
                        If hidF1.Value <> "" Then
                            cboTipDocumento1.SelectedValue = hidF1.Value
                        End If
                    End If
                Else
                    If hidF1.Value <> "" Then
                        cboTipDocumento1.SelectedValue = hidF1.Value
                    End If
                End If
            End If

            If txtDoc2.Text.Trim.ToString.Length > 0 Then
                If Me.txtDoc2.Text.ToString.Substring(0, 1) = "B" Or Me.txtDoc2.Text.ToString.Substring(0, 1) = "F" Then
                    cboTipDocumento2.SelectedValue = "ZNCR"
                Else
                    If hidF2.Value <> "" Then
                        cboTipDocumento2.SelectedValue = hidF2.Value
                    End If
                End If
            Else
                If hidF2.Value <> "" Then
                    cboTipDocumento2.SelectedValue = hidF2.Value
                End If
            End If

            If txtDoc3.Text.Trim.ToString.Length > 0 Then
                If Me.txtDoc3.Text.ToString.Substring(0, 1) = "B" Or Me.txtDoc3.Text.ToString.Substring(0, 1) = "F" Then
                    cboTipDocumento3.SelectedValue = "ZNCR"
                Else
                    If hidF3.Value <> "" Then
                        cboTipDocumento3.SelectedValue = hidF3.Value
                    End If
                End If
            Else
                If hidF3.Value <> "" Then
                    cboTipDocumento3.SelectedValue = hidF3.Value
                End If
            End If

            If txtDoc4.Text.Trim.ToString.Length > 0 Then
                If Me.txtDoc4.Text.ToString.Substring(0, 1) = "B" Or Me.txtDoc4.Text.ToString.Substring(0, 1) = "F" Then
                    cboTipDocumento4.SelectedValue = "ZNCR"
                Else
                    If hidF4.Value <> "" Then
                        cboTipDocumento4.SelectedValue = hidF4.Value
                    End If
                End If
            Else
                If hidF4.Value <> "" Then
                    cboTipDocumento4.SelectedValue = hidF4.Value
                End If
            End If

            If txtDoc5.Text.Trim.ToString.Length > 0 Then
                If Me.txtDoc5.Text.ToString.Substring(0, 1) = "B" Or Me.txtDoc5.Text.ToString.Substring(0, 1) = "F" Then
                    cboTipDocumento5.SelectedValue = "ZNCR"
                Else
                    If hidF5.Value <> "" Then
                        cboTipDocumento5.SelectedValue = hidF5.Value
                    End If
                End If
            Else
                If hidF5.Value <> "" Then
                    cboTipDocumento5.SelectedValue = hidF5.Value
                End If
            End If

        Catch ex As Exception
            cboTipDocumento1.SelectedIndex = 0
            cboTipDocumento2.SelectedIndex = 0
            cboTipDocumento3.SelectedIndex = 0
            cboTipDocumento4.SelectedIndex = 0
            cboTipDocumento5.SelectedIndex = 0
        End Try
        '** FDG *******************************************************************'
    End Sub

    Private Sub cboLoad(ByVal dsFormaPago As DataTable, ByRef cboCampo As DropDownList)

        Try
            cboCampo.DataSource = dsFormaPago
            cboCampo.DataTextField = "VTEXT"
            cboCampo.DataValueField = "CCINS"
            cboCampo.DataBind()
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Error al cargar los combos de las formas de pago.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Err." & ex.Message.ToString)
        End Try
    End Sub


    Public Function DevuelteTipoDescPago(ByVal cboTipDocumento As String) As String


        Dim cboDescTipDocumento As String


        Dim ds_FormaPago As DataSet = CType(Session("Vias_Pago"), DataSet)

        Dim j As Integer = 0
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - " & "Utilizacion de la variable Session: Vias_Pago")

            If ds_FormaPago.Tables(0).Rows.Count > 0 Then
                If Not IsNothing(ds_FormaPago) Then
                    For j = 0 To ds_FormaPago.Tables(0).Rows.Count - 1
                        If (ds_FormaPago.Tables(0).Rows(j)("CCINS") = cboTipDocumento) Then
                            cboDescTipDocumento = Trim(ds_FormaPago.Tables(0).Rows(j)("VTEXT"))
                            Exit For
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - " & "error Utilizacion de la variable Session: Vias_Pago")
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: DevuelveTipoDescPago)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - " & "ERROR: " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

        End Try
        Session("FormasPAgo") = Nothing

        Return cboDescTipDocumento
        'FPPB

    End Function


    '***PROCESO DE GUARDAR LOS PAGOS ****'
    Private Sub guardarConectado()

        Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        '==================================================================='
        ' PARAMETROS DE RETORNO DE GUARDAR PAGO
        Dim K_PAGOV_CORRELATIVO As String = ""
        Dim K_PAGOC_CORRELATIVO As String = ""

        Dim consultaDato As DataSet

        Dim K_NROLOG As String = ""
        Dim K_DESLOG As String = ""
        Dim K_NROLOG_DET As String = ""
        Dim K_DESLOG_DET As String = ""
        Dim K_PAGON_IDPAGO As Int64 = 0
        Dim K_CU_CORRELATIVOFE As String = ""

        Dim objContrato As DataSet          '*** guardar la informaciòn recuperada del contrato - dg ***'
        Dim P_COD_RESP As String = ""       '*** variables del contrato -dg ***'
        Dim P_MSG_RESP As String = ""       '*** variables del contrato -dg ***'

        'FIN DE PARAMETROS DE RETORNO DE GUARDAR PAGO 
        '==================================================================='


        ' PARAMETROS DE RETORNO DE CONSULTA PEDIDO PVU
        Dim P_CODIGO_RESPUESTA As String = ""
        Dim P_MENSAJE_RESPUESTA As String = ""
        Dim C_VENTA As DataTable
        Dim C_VENTA_DET As DataTable
        'FIN PARAMETROS DE RETORNO DE CONSULTA PEDIDO PVU

        'PROY-27440 INI
        Dim objTxt As TextBox
        Dim objCbo As DropDownList
        'PROY-27440 FIN

        Dim strNumAsignado As String
        Dim strNumSunat As String
        Dim i As Integer
        Dim j As Integer
        Dim strDetallePago As String
        Dim dsResult As DataSet
        Dim strNumAsignaSUNAT As String
        Dim strErrorMsg As String
        Dim blnSunat As Boolean
        Dim strNroDoc As String
        Dim strPrepago As String
        Dim strPostPago As String
        Dim intCeros As Integer
        Dim blnFlagPago As Boolean
        Dim strMensaje As String
        Dim strEstado As String
        Dim strCadenaCam As String
        Dim strContrato As String           '*** en el conectado ****'

        Dim PorMigracion As String
        Dim PorRenovacion As String
        Dim PorReposicion As String
        Dim PorAprobador As String
        Dim EstadoActualAcuerdo As String
        Dim strObsEstado As String

        'CARIAS
        'Conectado
        Dim strNroSEC, strNroDocSEC, strTipDocSEC, strNroAprobacion, strMotivoMig, strConSinEq As String
        'FIN CARIAS

        Dim strIND_ACTIV_BSCS As String
        Dim dsAcuerdo As DataSet
        Dim dsEstado As DataSet
        Dim dsDatGenerales As DataSet
        Dim conEquipo As Boolean

        Dim strValSAP As String
        Dim blnDebeSerRevisado As Boolean
        Dim strRespuesta As String

        Dim dblEfectivo As Double
        Dim dblEfecCaja As Double
        Dim dblTolerancia As Double
        Dim dsEfectivo As DataSet
        Dim dblMontoTarjeta As Double
        Dim dblTotalTarjeta As Double

        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim objActivaciones As New COM_SIC_Activaciones.clsActivacion
        Dim objConfig As New COM_SIC_Configura.clsConfigura
        Dim objClsPagosWS As New COM_SIC_Activaciones.clsPagosWS

        Dim objActiv As New COM_SIC_Activaciones.clsBDSiscajas
        Dim msgj As String = ""


        '****************************** Para Impresion
        'Dim strDocSap As String = "" 'drPagos.Item("VBELN")TODOEB
        Dim strDocSap As String = drPagos.Item("PEDIN_NROPEDIDO")


        Dim strDocSunat As String = ""
        Dim sTipoVenta, sTipoOperacion As String

        'Dim strDocSunat As String = drPagos.Item("XBLNR")TODOEB
        ' Dim strDocSunat As String = drPagos.Item("PAGOC_CODSUNAT") '"AC111"

        Dim strNroDG As String = "" 'drPagos.Item("NRO_DEP_GARANTIA")TODOEB
        'Dim strTipDoc As String = drPagos.Item("FKART")TODOEB

        '****************************************************************************
        'VARIABLES: INFORMACIÒN DEL PEDIDO
        '****************************************************************************
        Dim strTipDoc As String = drPagos.Item("PEDIC_CLASEFACTURA")

        '****************************************************************************

        Dim intOper As Int32
        '*******************************

        ' FLAG PILOTO
        Dim booDol As Boolean = True

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
        Dim Detalle(,) As Object
        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        Dim dtDatos As New DataTable
        Dim objBus As New COM_SIC_Cajas.clsCajas

        'NRO CONTRATO  TODOEB 
        Dim strIdentifyLog As String = drPagos.Item("PEDIN_NROPEDIDO")      'strNroPedidoSAP
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Grabar pagos.")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")

        '************************************************************************************************************'
        'CONSULTA EL CONTRATO:
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Obtener contrato.")
            If Not Session("recargaVirtual") Then
                objContrato = objClsConsultaPvu.ObtenerDrsap(drPagos.Item("PEDIN_NROPEDIDO"), P_COD_RESP, P_MSG_RESP)
            Else
                objContrato = Nothing
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato Obtenido: " & Funciones.CheckStr(objContrato.Tables(0).Rows(0).Item("ID_CONTRATO")))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato P_COD_RESP : " & Funciones.CheckStr(P_COD_RESP))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato P_MSG_RESP : " & Funciones.CheckStr(P_MSG_RESP))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Obtener contrato.")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Error en obtener contrato :" & drPagos.Item("PEDIN_NROPEDIDO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Err." & ex.Message.ToString)
        End Try
        '************************************************************************************************************'

        'Dim strIdentifyLog As String = drPagos.Item("PEDIN_NROPEDIDO")      'strNroPedidoSAP
        'Dim strIdentifyLog As String = drPagos.Item("VBELN") TODOEB

        'Validacion de Tarjeta de Credito
        'If Not valida_tarjeta() Then Exit Sub
        'Fin Validacion de Tarjeta de Credito

        'dsEfectivo = objCajas.FP_Get_ListaParamOficina(Session("CANAL"), ConfigurationSettings.AppSettings("CodAplicacion"), Session("ALMACEN"))
        'dblEfecCaja = objCajas.FP_CalculaEfectivo(Session("ALMACEN"), "60138")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Consulta el efectivo para la caja")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Parametro de consulta :" & Session("ALMACEN"))
        '** CONSULTA EFECTIVO:
        dsEfectivo = objCajas.FP_Get_ListaParamOficina(Session("CANAL"), ConfigurationSettings.AppSettings("CodAplicacion"), Session("ALMACEN"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Fin consulta paràmetros oficina")

        'dblEfecCaja = objCajas.FP_CalculaEfectivo(Session("ALMACEN"), Session("USUARIO"), drPagos.Item("FKDAT")) TODOEB
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Inicia Consulta Càlculo Efectivo")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Paràmetros: Punto Venta: " & Session("ALMACEN") & "  Fecha: " & DateTime.Now)
        'dblEfecCaja = objCajas.FP_CalculaEfectivo(Session("ALMACEN"), Session("USUARIO"), DateTime.Now)
        '** CONSULTA EFECTIVO CAJA:

        Dim dsPedido As DataSet
        '****** CONSULTA PRINCIPAL PARA EL PREOCESO *****'
        '***************************************************************************************************'
        '***************************************************************************************************'
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta los datos del pedido=> " & Funciones.CheckStr(drPagos.Item("PEDIN_NROPEDIDO")))
        dsPedido = objConsultaMsSap.ConsultaPedido(drPagos.Item("PEDIN_NROPEDIDO"), "", "") 'TODOEB
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta pedido.")
        '***************************************************************************************************'
        '***************************************************************************************************'
        '***************************************************************************************************'

        Dim cultureNameCaja As String = "es-PE"
        Dim cultureCaja As CultureInfo = New CultureInfo(cultureNameCaja)
        Dim dateTimeValueCajaEfe As DateTime
        dateTimeValueCajaEfe = Convert.ToDateTime(DateTime.Now, cultureCaja)
        Dim dFechaCaj0 As Date
        Dim sFechaCaj0 As String = dateTimeValueCajaEfe.ToLocalTime.ToShortDateString
        'PROY-140397-MCKINSEY -> JSQ INICIO
        sFechaCaj0 = DateTime.Now().ToString("dd/MM/yyyy")
        'PROY-140397-MCKINSEY -> JSQ FIN
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Fecha Pedido : " & sFechaCaj0)

        dblEfecCaja = objCajas.FP_CalculaEfectivo(Session("ALMACEN"), Session("USUARIO"), sFechaCaj0)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Paràmetros: Punto Venta: " & Session("ALMACEN") & "  Fecha: " & Funciones.CheckStr(DateTime.Now))

        '*** AVILLAR  ****'
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -Consulta el monto configurado de tarjeta.")
            dsDatGenerales = objConfig.FP_Lista_Param_Tarjeta(1)
            'Valor de monto de tarjeta configurado.
            If Not dsDatGenerales Is Nothing Then
                dblMontoTarjeta = dsDatGenerales.Tables(0).Rows(0).Item("PARTN_MONTO")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -dblMontoTarjeta: " & Funciones.CheckStr(dsDatGenerales.Tables(0).Rows(0).Item("PARTN_MONTO")))
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Error al consultar el monto configurado para la tarjeta.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Err." & ex.Message.ToString)
        End Try
        ' *** AVILLAR  ****'


        If dsEfectivo.Tables(0).Rows.Count = 0 Then
            Response.Write("<script>alert('No existe configuracion de caja para este punto de venta.')</script>")
            Exit Sub
        End If

        dblTolerancia = dsEfectivo.Tables(0).Rows(0).Item("CAJA_TOLERANCIA_SOL")

        If dblEfecCaja >= dsEfectivo.Tables(0).Rows(0).Item("CAJA_MAX_DISP_SOL") + dblTolerancia Then
            Response.Write("<script>alert('Ha alcanzado su maximo disponible de efectivo en caja. Debe depositar en caja buzón')</script>")
        Else

            'blnSunat = (drPagos.Item("XBLNR") = "" Or drPagos.Item("XBLNR") = "0000000000000000") TODOEB
            If Not Convert.IsDBNull(drPagos.Item("PAGOC_CODSUNAT")) Then
                blnSunat = (drPagos.Item("PAGOC_CODSUNAT") = "" Or drPagos.Item("PAGOC_CODSUNAT") = "0000000000000000")
            Else
                blnSunat = False
            End If

            'blnSunat = False
            ' objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PAGOC_CODSUNAT : " & drPagos.Item("PAGOC_CODSUNAT").ToString()) 'TODOEB
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "AC111 : " & "AC111")
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "blnSunat : " & blnSunat)

            If Trim(txtNumSunat.Text) = "" Then
                strNumSunat = txtCorrelativo.Text
            Else
                strNumSunat = txtNumSunat.Text
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "NumSunat : " & strNumSunat.ToString())

            If blnSunat Then
                strNumAsignaSUNAT = txtCompleto.Text
            Else
                strNumAsignaSUNAT = strNumSunat
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strNumAsignaSUNAT : " & strNumAsignaSUNAT.ToString())

            'Prepago cuotas 23/10/2006
            'Dim dsPedido As DataSet '= objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drPagos.Item("VBELN"), "") TODOEB

            ''****** CONSULTA PRINCIPAL PARA EL PREOCESO *****'
            ''***************************************************************************************************'
            ''***************************************************************************************************'
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta los datos del pedido=> " & Funciones.CheckStr(drPagos.Item("PEDIN_NROPEDIDO")))
            'dsPedido = objConsultaMsSap.ConsultaPedido(drPagos.Item("PEDIN_NROPEDIDO"), "", "") 'TODOEB
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta pedido.")
            ''***************************************************************************************************'
            ''***************************************************************************************************'
            ''***************************************************************************************************'

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Validacion que se ha pagado el documento")
            Dim valida_pago As String = ""

            Try
                valida_pago = dsPedido.Tables(0).Rows(0).Item("PEDIC_ESTADO")

                If valida_pago = ConfigurationSettings.AppSettings("ESTADO_PAG") Then
                    Session("Valida_Pago_MSG") = "El documento se encuentra Pagado."
                    Response.Redirect("PoolPagos.aspx", False)
                    Exit Sub
                End If
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & ex.StackTrace)
                Session("Valida_Pago_MSG") = "Ocurrio un error al consultar los datos para el Pago"
                Response.Redirect("PoolPagos.aspx", False)
                Exit Sub
            End Try

            Dim K_COD_RESPUESTA As String
            Dim K_MSJ_RESPUESTA As String
            Dim K_ID_TRANSACCION As String

            '*******************************************************************************************************
            '**1. sisact_info_venta_sap => consulta con el numero de pedido(nro_documento), retorna el n_id_venta.
            '**2. sisact_ap_venta v / sisact_ap_contrato, donde  v.id_documento=n_id_venta => c_venta
            '**3. sisact_ap_venta_detalle vd => vd.id_documento = n_id_venta => c_venta_det  
            Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia consulta datos pedido en PVU")
                objClsConsultaPvu.ConsultarPedidosPVU(drPagos.Item("PEDIN_NROPEDIDO"), _
                                                              P_CODIGO_RESPUESTA, _
                                                              P_MENSAJE_RESPUESTA, _
                                                              C_VENTA, _
                                                              C_VENTA_DET)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta datos pedido en PVU")
                If P_MENSAJE_RESPUESTA <> "OK" Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  No se encontro el pedido en la tabla : sisact_info_venta_sap. pkg:sisact_pkg_venta sp:sp_con_venta_x_docsap => " & drPagos.Item("PEDIN_NROPEDIDO"))
                End If
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Error, no encontro datos al ejecutar el sp: SP_CON_VENTA_X_DOCSAP , para el pedido => " & drPagos.Item("PEDIN_NROPEDIDO"))
            End Try
            '*******************************************************************************************************

            '-------------------------------------------------------------------------------------------------------'
            '*** Son DataTables que retornan del procedimiento, y consultan el PVU con el NroPedido             ***'
            '*** actualmente van a retornan en vacio, debido a que el pedido no se registra en la tabla:        ***'
            '##     Tabla: sisact_info_venta_sap => sap.nro_documento = PEDIN_NROPEDIDO'
            '##     sisact_ap_venta v, sisact_ap_contrato co =>C_VENTA
            '##     sisact_ap_venta_detalle => C_VENTA_DET
            '-------------------------------------------------------------------------------------------------------'


            If C_VENTA.Rows.Count = 0 Then
                C_VENTA = Nothing
            End If

            If C_VENTA_DET.Rows.Count = 0 Then
                C_VENTA_DET = Nothing
            End If


            C_VENTA_DET = C_VENTA_DET
            C_VENTA = C_VENTA

            Dim dblTotPreCuo As Double
            Dim dblTotalReg As Double
            'si es venta prepago y ademas tiene cuotas  
            'inicio promocion modem + laptop



            '********************************************************************************************'
            '** Estas variables no estan siendo utilizadas, lo comentamos
            'sTipoVenta = CheckStr(drPagos.Item("PEDIC_TIPOVENTA"))
            'sTipoOperacion = CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION"))
            '********************************************************************************************'


            ' E75893 - Validacion Adjuntar Documento Cliente solo Ventas Altas Prepago

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia ValidacionVentaDOL")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ValidacionVentaDOL")


            '***IDG****'
            '*****Validación Pago del documento total **********************************************************************************************'
            If Trim(txtMonto1.Text) <> "" Then
                dblTotalReg = CDbl(txtMonto1.Text)
            End If
            If Trim(txtMonto2.Text) <> "" Then
                dblTotalReg += CDbl(txtMonto2.Text)
            End If
            If Trim(txtMonto3.Text) <> "" Then
                dblTotalReg += CDbl(txtMonto3.Text)
            End If
            If Trim(txtMonto4.Text) <> "" Then
                dblTotalReg += CDbl(txtMonto4.Text)
            End If
            If Trim(txtMonto5.Text) <> "" Then
                dblTotalReg += CDbl(txtMonto5.Text)
            End If

            If Math.Round(dblTotalReg, 2) <> Math.Round(Funciones.CheckDbl(txtNeto.Text), 2) Then
                Response.Write("<script>alert('No se permiten pagos parciales. El pago debe ser por el total del documento.')</script>")
                Exit Sub
            End If
            '***************************************************************************************************************************************'
            '***FDG******'



            '----- CAMPAÑA PRE + POST
            If blnSunat Then
                'dsResult = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drPagos.Item("VBELN"), "")
                dsResult = objConsultaMsSap.ConsultaPedido(drPagos.Item("PEDIN_NROPEDIDO"), "", "")

                If Not IsNothing(dsResult) Then
                    'strNroDoc = dsResult.Tables(0).Rows(0).Item("DOCUMENTO") TODOEB
                    'strNroDoc = dsResult.Tables(0).Rows(0).Item("PEDIV_NRODOCCLIENTE")
                    strNroDoc = dsResult.Tables(0).Rows(0).Item("CLIEV_NRODOCCLIENTE")
                Else
                    strNroDoc = ""
                End If

                blnFlagPago = True
            End If
            ' FIN CAMPAÑA PRE + POST
            dblEfectivo = 0
            dblTotalTarjeta = 0
            If hidNumFilas.Value = "" Then hidNumFilas.Value = 0
            strDetallePago = ""
'PROY-27440 INI
            Dim strMonto As String = ""

            Dim strTipoDocPago As String = ""
'PROY-27440 FIN

            For i = 1 To hidNumFilas.Value

                'PROY-27440 INI

                objTxt = CType(Me.FindControl("txtMonto1"), TextBox)
                strMonto = "" : strMonto = Trim(objTxt.Text)

                objCbo = CType(Me.FindControl("cboTipDocumento1"), DropDownList)
                strTipoDocPago = "" : strTipoDocPago = Trim(objCbo.SelectedValue)

                strDetallePago = strDetallePago & ";"
                'strDetallePago = strDetallePago & Session("ALMACEN") & ";" ' Concatenar detalles de pag
                strDetallePago = strDetallePago & Session("ALMACEN") & ";"    ' Concatenar detalles de pago
                strDetallePago = strDetallePago & strTipoDocPago & ";"    ' Codigo del Tipo de documento (Via de Pago) 'PROY-27440
                strDetallePago = strDetallePago & ";"
                strDetallePago = strDetallePago & ";"
                strDetallePago = strDetallePago & strMonto & ";"    ' Monto de pago  'PROY-27440
                'If (Session("VarVal2") = Session("CANAL")) Then      ' averiguar en que variable se guarda canal - MONEDA
                strDetallePago = strDetallePago & "PEN" & ";"
                'Else
                '    strDetallePago = strDetallePago & "L" & ";"
                'End If

                strDetallePago = strDetallePago & ";"

                strDetallePago = strDetallePago & strNumAsignaSUNAT & ";"
                strDetallePago = strDetallePago & Trim(strTipoDocPago) & ";"    ' GLOSA QUE ES LO MISMO DE VIA_PAGO 'PROY-27440
                '  strDetallePago = strDetallePago &  DateTime.Now & ";"    ' Fecha de documento que esta en el detalle - F_PEDIDO TODOEB
                strDetallePago = strDetallePago & DateTime.Now & ";"    ' Fecha de documento que esta en el detalle - F_PEDIDO
                strDetallePago = strDetallePago & ";"

                'If (Session("VarVal2") = Session("CANAL")) Then
                strDetallePago = strDetallePago & ";"
                'Else
                '    strDetallePago = strDetallePago & strTipoFact & ";"
                'End If

                If Trim(strTipoDocPago) = "ZEFE" Then
                    '27444 INI

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Monto Efectivo : " & strMonto)

                    dblEfectivo += CDbl(Trim(strMonto))
                    '27444 FIN
                End If

                If (i < CInt(hidNumFilas.Value)) Then
                    strDetallePago = strDetallePago & "|"
                End If

                'Valida sumatoria de tarjetas....
                'AVILLAR : ZNCR
'27440 INI
                If Trim(strTipoDocPago) <> "ZNCR" And Trim(strTipoDocPago) <> "ZEFE" And Trim(strTipoDocPago) <> "ZEAM" And Trim(strTipoDocPago) <> "ZEOV" And Trim(strTipoDocPago) <> "TDPP" And Trim(strTipoDocPago) <> "ZDEL" Then
                    dblTotalTarjeta += CDbl(Trim(strMonto))
                End If
'27440 FIN
                'FIN AVILLAR
            Next


            If (dblEfectivo + dblEfecCaja) >= dblTolerancia + dsEfectivo.Tables(0).Rows(0).Item("CAJA_MAX_DISP_SOL") Then
                Response.Write("<script>alert('Este pago excede su tolerancia de monto máximo de efectivo en caja')</script>")
                Session("strMensajeCaja") = "Este pago excedió su tolerancia de monto máximo de efectivo en caja"
            End If
            'Else

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ListarDatosCabeceraVenta - strDocSap: " & strDocSap)
            dtDatos = objBus.ListarDatosCabeceraVenta(strDocSap)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ListarDatosCabeceraVenta - dtDatos: " & dtDatos.Rows.Count.ToString)

            ' INICIO FMES :DETERMINA SI TIENE EL FLAG DE PILOTO VENTA EN CAC
            If dtDatos.Rows.Count > 0 Then
                If Funciones.CheckStr(dtDatos.Rows(0).Item("VEPR_FLAG_VENTA_CAC").ToString()) = "1" Then
                    booDol = False
                End If
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ListarDatosCabeceraVenta - booDol: " & booDol.ToString)
            ' FIN FMES :DETERMINA SI TIENE EL FLAG DE PILOTO VENTA EN CAC


            If 1 = 1 Then 'blnSunat Then TODOEB

                ''''***IDG*****************************************************************************************************************************************************'
                '''**** CONSULTA DATOS DEL CAJERO ****'
                Dim cultureNameX As String = "es-PE"
                Dim cultureX As CultureInfo = New CultureInfo(cultureNameX)
                Dim dateTimeValueCaja As DateTime
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Iniciamos la consulta de los datos CAJA")
                dateTimeValueCaja = Convert.ToDateTime(DateTime.Now, cultureX)
                Dim strFecha As String = ""
                strFecha = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000")

                Try
                    objOfflineCaja = New COM_SIC_OffLine.clsOffline
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "---------------------------------------------------- ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Paràmetros : GetDatosAsignacionCajero")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param1: " & Funciones.CheckStr(Session("ALMACEN")))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param2: " & Funciones.CheckStr(dateTimeValueCaja.ToShortDateString))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param2: " & Funciones.CheckStr(strFecha.ToString))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param3: " & "00000" & Funciones.CheckStr(Session("USUARIO")))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "---------------------------------------------------- ")



                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-----------------------------------------------------------------------------")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Test de fechas:")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- strFecha " & strFecha.ToString)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- dateTimeValueCaja " & dateTimeValueCaja.ToLocalTime.ToShortDateString)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- fecha pedido " & CStr(dsPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO")))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-----------------------------------------------------------------------------")


                    'dsCajeroA = objOfflineCaja.GetDatosAsignacionCajero(Funciones.CheckStr(Session("ALMACEN")), Funciones.CheckStr(dateTimeValueCaja.ToShortDateString), "00000" & Funciones.CheckStr(Session("USUARIO")).ToString)
                    Dim dFecha As Date
                    Dim sFecha As String = dateTimeValueCaja.ToLocalTime.ToShortDateString
                    'PROY-140397-MCKINSEY -> JSQ INICIO
                    sFecha = DateTime.Now().ToString("dd/MM/yyyy")
                    'PROY-140397-MCKINSEY -> JSQ FIN
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Fecha Pedido : " & sFecha)
                    Dim codigo_vendedor_session As String = ""
                    codigo_vendedor_session = Funciones.CheckStr(Session("USUARIO")).PadLeft(10, "0")

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "---------------------------------------------------- ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Paràmetros : GetDatosAsignacionCajero")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param1: " & Funciones.CheckStr(Session("ALMACEN")))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param2: " & Funciones.CheckStr(dateTimeValueCaja.ToShortDateString))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param2: " & Funciones.CheckStr(strFecha.ToString))
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param3: " & "00000" & Funciones.CheckStr(Session("USUARIO")))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param3: " & codigo_vendedor_session)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "---------------------------------------------------- ")

                    'dsCajeroA = objOfflineCaja.GetDatosAsignacionCajero(Funciones.CheckStr(Session("ALMACEN")), sFecha, "00000" & Funciones.CheckStr(Session("USUARIO")).ToString)
                    dsCajeroA = objOfflineCaja.GetDatosAsignacionCajero(Funciones.CheckStr(Session("ALMACEN")), sFecha, codigo_vendedor_session)
                    If (dsCajeroA Is Nothing OrElse dsCajeroA.Tables(0).Rows.Count <= 0) Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Cantidad de Registros devueltos: " & dsCajeroA.Tables(0).Rows.Count.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- No se pudo determinar el numero de la caja asignada.")
                        Response.Write("<script>alert('No se encontro el Nùmero/Nombre de caja asignado.')</script>")
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
                End Try

                '**Buscamos el nombre de la Caja
                If dsCajeroA.Tables(0).Rows.Count > 0 Then
                    dsCajeroB = objOfflineCaja.Get_CajaOficinas(Session("ALMACEN"))
                    If (dsCajeroB Is Nothing OrElse dsCajeroA.Tables(0).Rows.Count <= 0) Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- No se pudo determinar el nombre de la caja asignada.")
                        Response.Write("<script>alert('No se encontro el Nùmero/Nombre de caja asignado.')</script>")
                        Exit Sub
                    Else
                        For c As Int16 = 0 To dsCajeroB.Tables(0).Rows.Count - 1
                            '**Comparamos los numeros de caja:
                            If dsCajeroA.Tables(0).Rows(0).Item("CAJA") = dsCajeroB.Tables(0).Rows(c).Item("CASNR") Then
                                strNombreCaja = Funciones.CheckStr(dsCajeroB.Tables(0).Rows(c).Item("BEZEI")).ToString.Trim
                            End If
                        Next
                    End If
                End If
                '***FDG*****************************************************************************************************************************************************'

                '**IDG**
                '***Cuando el pago se hace con forma NC y no cumple las validaciones ***

                ''DMZ - Comentado porque no se usa Nota de Credito
                'Try
                '    If ConsultaFormaPagoNotaCredito() = False Then
                '        Exit Sub    '** terminamos el proceso **'
                '    End If
                'Catch ex As Exception

                'End Try
                ''DMZ - Comentado porque no se usa Nota de Credito
                '**FDG*********************************************************************

                '''INI : Validacion de Medios de pago perimitidos RA TS-CCC
                '****************************Consulta de Datos********************************
                Dim Tipo_docume_nuevo As String
                Dim swError As Boolean = False
                Dim strMensajeAlertMP As String

'27440 INI

                objCbo = CType(Me.FindControl("cboTipDocumento1"), DropDownList)
                strTipoDocPago = "" : strTipoDocPago = Trim(objCbo.SelectedValue)

                Dim cboMedioPagoSeleted As String = strTipoDocPago

'27440 FIN
                Try
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "INI Log para obtener el Formas de Pago de PVU.  Stored Procedure : Get_NuevoCodMedioPago ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "IN Medio De pago: " & cboMedioPagoSeleted)
                    Dim dsMP As DataSet = objOfflineCaja.Get_NuevoCodMedioPago(cboMedioPagoSeleted)
                    If dsMP.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(dsMP.Tables(0).Rows(0).Item(0)) Then
                            Tipo_docume_nuevo = dsMP.Tables(0).Rows(0).Item(0)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "OUT Tipo_docume_nuevo: " & Tipo_docume_nuevo)
                            If Tipo_docume_nuevo.ToUpper() = "NO APLICA" Then
                                swError = True
                            End If
                        Else
                            swError = True
                        End If
                    Else
                        swError = True
                    End If
                    If swError Then
                        strMensajeAlertMP = "Medio de Pago Invalido para Pago de DRA"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Error al obtener el EQ Formas de Pago PVU: " & strMensajeAlertMP)
                        Response.Write("<script language=jscript> alert('" & strMensajeAlertMP & "'); </script>")
                        Exit Sub
                    End If
                Catch ex As Exception
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: guardarConectado)"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception: " & ex.ToString & MaptPath)
                    'FIN PROY-140126

                Finally
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "FIN Log para obtener el Formas de Pago de PVU.  Stored Procedure : Get_NuevoCodMedioPago ")
                End Try
                '''FIN : Validacion de Medios de pago perimitidos RA - TS-CCC

                Try
                    '*************************'
                    '**** BLOQUE II *******:
                    '*************************'
                    Dim txtMonto As Double
                    Dim cboTipDocumento As String
                    Dim cboDescTipDocumento As String

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio del registro del pago")
                    objTrsMsSap.RegistrarPago(drPagos.Item("PEDIN_NROPEDIDO"), DBNull.Value, _
                                                 System.Configuration.ConfigurationSettings.AppSettings("ESTADO_PAG"), _
                                                 "CAJA PRUEBA", _
                                                 0, _
                                                  System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                                  Session("USUARIO"), _
                                                  DBNull.Value, _
                                                  Session("USUARIO"), _
                                                  DBNull.Value, _
                                                  K_NROLOG, K_DESLOG, K_PAGON_IDPAGO, K_PAGOC_CORRELATIVO)


                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin del registro del pago")
                    If K_DESLOG = "OK" Then
                        Session.Add("msgErrorGenerarSot", "Registro de Pago Correctamente")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Registro de forma correcta del la cabecera del pago :" & K_PAGON_IDPAGO)

                        '************************************************************************
                        If (dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA") <> ConfigurationSettings.AppSettings("strTipDoc") And Funciones.CheckDbl(dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")) > 0) Then
                            '** se preocesan los otros tipos de documentos.
                            For i = 1 To hidNumFilas.Value

                                'PROY-27440 INI

                                objTxt = CType(Me.FindControl("txtMonto1"), TextBox)
                                strMonto = "" : strMonto = Trim(objTxt.Text)


                                objCbo = CType(Me.FindControl("cboTipDocumento1"), DropDownList)
                                strTipoDocPago = "" : strTipoDocPago = Trim(objCbo.SelectedValue)

                                cboTipDocumento = strTipoDocPago
                                txtMonto = Double.Parse(strMonto)

'PROY-27440 FIN
                                cboDescTipDocumento = DevuelteTipoDescPago(cboTipDocumento)

                                objTrsMsSap.RegistrarDetallePago(K_PAGON_IDPAGO, cboTipDocumento, _
                                                                    cboDescTipDocumento, _
                                                                    System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                                                    txtMonto, _
                                                                    Session("USUARIO"), _
                                                                    DBNull.Value, Session("USUARIO"), _
                                                                    DBNull.Value, _
                                                                    IIf(arrayListaReferencia(i) = "", "", Funciones.CheckStr(arrayListaReferencia(i))), _
                                                                    arrayListaRefPedido(i), _
                                                                    K_NROLOG_DET, K_DESLOG_DET)
                            Next
                        Else
                            '** Registro del detalle de pago de la Nota de Crèdito/ TG  **
                            cboDescTipDocumento = DevuelteTipoDescPago(cboTipDocumento1.SelectedValue)
                            objTrsMsSap.RegistrarDetallePago(K_PAGON_IDPAGO, cboTipDocumento1.SelectedValue, _
                                                              cboDescTipDocumento, _
                                                             System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                                             Funciones.CheckDbl(txtMonto1.Text), _
                                                             Session("USUARIO"), _
                                                             DBNull.Value, Session("USUARIO"), _
                                                             DBNull.Value, _
                                                             "", 0, _
                                                             K_NROLOG_DET, K_DESLOG_DET)
                        End If

                        '************************************************************************

                        Dim FE_NRO_ERROR As Int16 = 0
                        Dim FE_DES_ERROR As String = ""
                        Dim K_PAGOV_RECUP_CORRELATIVO As String = ""
                        Dim flag_paperless As String = ""
                        Dim correlativo_recu As String = ""

                        Dim Origen As String = "3" 'Valor para validar que sea SICAR 6.0


                        If K_DESLOG_DET = "OK" Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Registro de forma correcta del detalle del pago :" & K_DESLOG_DET & " - " & K_PAGON_IDPAGO)
                            Try
                                '=================================================================================================================================================================================================================='
                                'CORRELATIVO Y ACTUALIZACIÒN DEL PAGO PEDIDO'
                                '=================================================================================================================================================================================================================='

                                K_NROLOG = ""
                                K_DESLOG = ""

                                '************************************REnta Adelante Valor por por defecto del Correlativo en caso de ser Renta*********************************
                                If Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")) = ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA") Then
                                    K_CU_CORRELATIVOFE = ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA")
                                End If
                                '************************************REnta Adelante Valor por por defecto del Correlativo en caso de ser Renta*********************************
                                If K_CU_CORRELATIVOFE <> "" Then
                                    K_NROLOG = ""
                                    K_DESLOG = ""

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Inicia actualizar pago: ActualizarPagodelPedido")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "SP: " & "PKG_MSSAP.SSAPSU_UPDATEPAGO")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param: " & Funciones.CheckStr(drPagos.Item("PEDIN_NROPEDIDO")))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param: " & Funciones.CheckStr(K_PAGON_IDPAGO))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param: " & Funciones.CheckStr(K_CU_CORRELATIVOFE))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param: PEDIC_CLASEFACTURA" & Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param: " & Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("OFICV_CODOFICINA")))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param: " & Funciones.CheckStr(K_NROLOG))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param: " & Funciones.CheckStr(K_DESLOG))

                                    'PEDIC_CLASEFACTURA
                                    objTrsMsSap.ActualizarPagodelPedido(drPagos.Item("PEDIN_NROPEDIDO"), _
                                                                                        K_PAGON_IDPAGO, _
                                                                                        K_CU_CORRELATIVOFE, _
                                                                                        dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"), _
                                                                                        dsPedido.Tables(0).Rows(0).Item("OFICV_CODOFICINA"), _
                                                                                        K_NROLOG, K_DESLOG)

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  K_DESLOG :" & K_DESLOG)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  K_NROLOG :" & K_NROLOG)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Fin actualizar pago: ActualizarPagodelPedido")
                                Else
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  ERROR EN EL Correlativo :" & K_CU_CORRELATIVOFE)
                                    '** RollBack a los Pagos ************************'
                                    DeshacerCambiosPagos(K_PAGON_IDPAGO)
                                    Response.Write("<script>alert('No tiene configurado correlativo para el punto de venta : ' & '" & dsPedido.Tables(0).Rows(0).Item("OFICV_CODOFICINA") & "')</script>")
                                    Exit Sub
                                    '*************************************************'
                                End If


                                If K_DESLOG = "OK" Then
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  sE ACTUALIZORN LOS PAGOS :" & K_PAGON_IDPAGO)
                                Else
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  ERROR EN LA ACTUALIZACIÒN")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  K_DESLOG :" & K_DESLOG)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  K_NROLOG :" & K_NROLOG)
                                    '** RollBack a los Pagos en la Actualizaciòn pagos ************************'
                                    DeshacerCambiosPagos(K_PAGON_IDPAGO)
                                    '*************************************************'
                                End If
                                '=================================================================================================================================================================================================================='
                            Catch ex As Exception
                                'INI PROY-140126
                                Dim MaptPath As String
                                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                                MaptPath = "( Class : " & MaptPath & "; Function: guardarConectado)"
                                objFileLog.Log_WriteLog(pathFile, strArchivo, " - " & "ERROR: " & ex.Message.ToString() & MaptPath)
                                'FIN PROY-140126
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  K_DESLOG :" & K_DESLOG)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  K_NROLOG :" & K_NROLOG)
                                DeshacerCambiosPagos(K_PAGON_IDPAGO)
                                Session.Add("msgErrorGenerarSot", "Ocurrio un error al tratar de obtener el Correlativo")
                                Response.Redirect("PoolPagos.aspx")
                            End Try
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Registro de forma correcta el detalle del Pago :" & K_PAGON_IDPAGO)
                        Else
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Ocurrio un error al tratar de registrar el detalle del pedido :" & K_PAGON_IDPAGO)
                            '** RollBack a los Pagos ************************'
                            DeshacerCambiosPagos(K_PAGON_IDPAGO)
                            '*************************************************'
                        End If
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  No registro de forma correcta el Pago :" & K_PAGON_IDPAGO)
                        '** RollBack al registro del pago ************************'
                        DeshacerCambiosPagos(K_PAGON_IDPAGO)
                        '*************************************************'
                    End If
                Catch ex As Exception
                    Session.Add("msgErrorGenerarSot", "Error Al registrar Pagos")
                    Response.Redirect("PoolPagos.aspx")
                End Try

                ''FIN DEL METODO GRABAR TRY
                Dim blnErrorPago As Boolean = False

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Fin del proceso de pagos")

                If K_DESLOG <> "OK" Then
                    strErrorMsg = K_DESLOG
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR : " & strErrorMsg)
                End If



                If Len(Trim(strErrorMsg)) > 0 Then     'se produjo algun error al momento de pagar
                    Session("strMensajeCaja") = strErrorMsg
                    Response.Redirect("PoolPagos.aspx")
                Else
                    '**Inicio Pago de Rentas Adelantadas en Tablas propias de Rentas
                    '*********************************************************************************************************

                    Dim respuesta As Boolean = False

                    Dim listaTipoDoc As DataSet
                    Dim tipoDocumento As String
                    Dim tipoDocIdent As String = ""
                    Dim codigoPago As String
                    Dim cod_pago As String
                    Dim sImporte As String
                    Dim sImporte2 As String
                    'Dim sFechaReg As String
                    'Dim sFecVencimiento As String
                    'Dim sNroDocumento As String
                    Dim sNroAcuerdo As String
                    ' Dim sTipoDocIdentidad As String
                    Dim sCodDocumento As String
                    Dim sDesDocumento As String = ""
                    Dim dsDra As DataSet
                    Dim dsFormaPago As DataSet

                    Dim txtMonto_1 As Double
                    Dim cboTipDocumento_1 As String
                    'Dim Tipo_docume_nuevo As String
                    Dim cboDescTipDocumento_1 As String
                    Dim Numero_Tarjeta As String
                    Dim h As Integer
                    Dim m As Integer

                    '27440 INI
                    objTxt = CType(Me.FindControl("txtMonto1"), TextBox)
                    strMonto = "" : strMonto = Trim(objTxt.Text)


                    objCbo = CType(Me.FindControl("cboTipDocumento1"), DropDownList)
                    strTipoDocPago = "" : strTipoDocPago = Trim(objCbo.SelectedValue)

                    cboTipDocumento_1 = strTipoDocPago

                    txtMonto_1 = Funciones.CheckDbl(strMonto)


                    Dim strNumero_Tarjeta = ""
                    Dim objTxtTarjeta As TextBox
                    objTxtTarjeta = CType(Me.FindControl("txtDoc1"), TextBox)
                    strNumero_Tarjeta = "" : strNumero_Tarjeta = Trim(objTxtTarjeta.Text)

                    Numero_Tarjeta = strNumero_Tarjeta 'verificar

                    '27440 FIN




                    Dim cod_Rpta As String = ""
                    Dim msg_Rpta As String = ""


                    '****************************Consulta de Datos********************************

                    'Comentado por SD_668541
                    'Try
                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Log para obtener el Formas de Pago de PVU.  Stored Procedure : sisacss_CodPago_TipoDoc ")
                    '    dsFormaPago = objConsultaMsSap.Lista_Formas_Pago_pvu()
                    '    If dsFormaPago.Tables(0).Rows.Count > 0 Then
                    '        For j = 0 To dsFormaPago.Tables(0).Rows.Count - 1
                    '            If cboTipDocumento_1 = dsFormaPago.Tables(0).Rows(j).Item("CODIGO_ANTIGUO_MEDIO").ToString Then
                    '                Tipo_docume_nuevo = dsFormaPago.Tables(0).Rows(j).Item("COD_NUEVO_MEDIO").ToString
                    '                Exit For
                    '            End If
                    '        Next
                    '    End If
                    'Catch ex As Exception
                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception: " & ex.ToString)
                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Se realiza el rollback al mòdulo.")
                    '    DeshacerCambiosPagos(K_PAGON_IDPAGO)
                    'End Try

                    Try

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Log para obtener el codigo DRAV.  Stored Procedure : sisacss_CodPago_TipoDoc ")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codigo_Pedido: " & drPagos.Item("PEDIN_NROPEDIDO"))

                        respuesta = objConsultaMsSap.obtenerTipoDocumento_y_codigoPago(drPagos.Item("PEDIN_NROPEDIDO"), tipoDocumento, codigoPago)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "codigoPago: " & codigoPago)

                        cod_pago = codigoPago

                        dsDra = objConsultaMsSap.Consultar_Pago_Dra("", "", cod_pago, cod_Rpta, msg_Rpta)

                        If dsDra.Tables(0).Rows.Count > 0 Then
                            'sImporte2 = dsDra.Tables(1).Rows(0)("DRAN_IMPORTE_DRA")
                            sImporte = Funciones.CheckStr(dsDra.Tables(1).Rows(0)("DRAN_IMPORTE_DRA").ToString)
                            'sImporte=sImporte2.IndexOf(

                            'sNroDocumento = dsDra.Tables(0).Rows(0)("DRAV_DOCUMENTO_CLIENTE").ToString	

                            sNroAcuerdo = dsDra.Tables(1).Rows(0)("DRAV_NRO_DRA").ToString
                            'sTipoDocIdentidad = tipoDocumento
                            'sFechaReg = DateTime.Parse(dsDra.Tables(1).Rows(0)("DRAD_FECHA_EMISION").ToString).ToShortDateString
                            'sFecVencimiento = DateTime.Parse(dsDra.Tables(1).Rows(0)("DRAD_FECHA_VENCIMIENTO").ToString).ToShortDateString
                        Else
                            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception: " & ex.ToString)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Se realiza el rollback al mòdulo.")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " dsDra.Tables(0).Rows.Count : " & Funciones.CheckStr(dsDra.Tables(0).Rows.Count))
                            'DeshacerCambiosPagos(K_PAGON_IDPAGO)
                        End If


                        '*********************Grabar Datos*****************************************
                        Dim DRAV_COD_APLIC As String = ConfigurationSettings.AppSettings("Codigo_Aplicacion_DRA")
                        Dim DRAV_USUARIO_APLIC As String = ConfigurationSettings.AppSettings("Usuario_Aplicacion")
                        Dim DRAC_TIPO_RA As String = ConfigurationSettings.AppSettings("Nombre_Corte_Renta")
                        Dim DRAV_DESCRIPCION_DOC As String = ConfigurationSettings.AppSettings("Descripcion_Renta")
                        Dim DRAV_CODIGO_MONEDA As String = System.Configuration.ConfigurationSettings.AppSettings("MONEDA")
                        Dim DRAD_FECHA_PAGO As String = DateTime.Now.ToString
                        Dim DRAN_TRACE_ID As String = System.Configuration.ConfigurationSettings.AppSettings("DRAN_TRACE_ID")
                        Dim DRAN_NUM_OPE_PAGO As String = ConfigurationSettings.AppSettings("Codigo_Aplicacion_DRA") 'Numero_Tarjeta ' 
                        Dim DRAV_COD_BANCO As String = System.Configuration.ConfigurationSettings.AppSettings("Cod_Banco") '"12450" 'Verificar con papucho
                        Dim DRAV_COD_PDV As String = Session("ALMACEN")
                        'Dim CADENA_MEDIO_PAGO As String = Tipo_docume_nuevo + ";" + DRAV_COD_BANCO + ";" + Numero_Tarjeta + ";" + txtMonto1.Text 'Me.ddlformaPago.SelectedValue + ";" + Me.ddlBanco.SelectedValue + ";" + Me.txtCheque.Text + ";" + importe
                        Dim CADENA_MEDIO_PAGO As String = Tipo_docume_nuevo + ";" + DRAV_COD_BANCO + ";" + Numero_Tarjeta + ";" + sImporte ' validar el importe de sisact

                        Dim ID_OPE_PAGO_SISACT As Int64
                        Dim COD_RPTAA As Int64
                        Dim MSG_RPTAA As String = ""
                        Dim retorno As Boolean = False

                        '*********************Log para Grabar en la tabla *****************************************
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Log para Grabar parametros de entrada al Stored Procedure : SP_CREAR_PAGO_DRA ")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAV_COD_APLIC: " & DRAV_COD_APLIC)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAV_USUARIO_APLIC: " & DRAV_USUARIO_APLIC)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "cod_pago: " & cod_pago)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAC_TIPO_RA: " & DRAC_TIPO_RA)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sNroAcuerdo: " & sNroAcuerdo)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAV_DESCRIPCION_DOC: " & DRAV_DESCRIPCION_DOC)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAV_CODIGO_MONEDA: " & DRAV_CODIGO_MONEDA)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "txtMonto1.Text: " & txtMonto1.Text)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sImporte : " & sImporte)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAD_FECHA_PAGO: " & DRAD_FECHA_PAGO)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAN_TRACE_ID: " & DRAN_TRACE_ID)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAN_NUM_OPE_PAGO: " & DRAN_NUM_OPE_PAGO)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAV_COD_BANCO: " & DRAV_COD_BANCO)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "CADENA_MEDIO_PAGO: " & CADENA_MEDIO_PAGO)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAV_COD_PDV: " & DRAV_COD_PDV)


                        retorno = objConsultaMsSap.Crear_Pago_Dra(DRAV_COD_APLIC, DRAV_USUARIO_APLIC, cod_pago, DRAC_TIPO_RA, sNroAcuerdo, DRAV_DESCRIPCION_DOC, _
                                                        DRAV_CODIGO_MONEDA, sImporte, DRAD_FECHA_PAGO, DRAN_TRACE_ID, DRAN_NUM_OPE_PAGO, _
                                                        DRAV_COD_BANCO, DRAV_COD_PDV, CADENA_MEDIO_PAGO, ID_OPE_PAGO_SISACT, COD_RPTAA, MSG_RPTAA)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Parametros de Salida del Stored Procedure : SP_CREAR_PAGO_DRA ")

                        If COD_RPTAA = 0 And retorno Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Parametros de Salida Validos del Stored Procedure : SP_CREAR_PAGO_DRA ")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ID_OPE_PAGO_SISACT: " & ID_OPE_PAGO_SISACT)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "COD_RPTAA: " & COD_RPTAA)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MSG_RPTAA: " & MSG_RPTAA)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "FIN Parametros de Salida Validos del Stored Procedure : SP_CREAR_PAGO_DRA ")


                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Parametros de Entrada AsignarPagoAcuerdosXDocSap PAgo Exitoso ")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in strDocSap: " & strDocSap)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in Referencia: " & sNroAcuerdo)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in mporte: " & sImporte)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in Current user: " & CurrentUser)

                            objActiv.AsignarPagoAcuerdosXDocSap(strDocSap, sNroAcuerdo, CheckDbl(sImporte), CurrentUser, msgj)


                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "out msg: " & msgj)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " FIN Parametros de Entrada AsignarPagoAcuerdosXDocSap PAgo Exitoso ")

                        Else
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Parametros de Salida No Validos del Stored Procedure : SP_CREAR_PAGO_DRA ")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ID_OPE_PAGO_SISACT: " & ID_OPE_PAGO_SISACT)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "COD_RPTAA: " & COD_RPTAA)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MSG_RPTAA: " & MSG_RPTAA)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Se realiza el rollback al mòdulo.")

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "inicio ROllback Parametros de Entrada AsignarPagoAcuerdosXDocSap PAgo Exitoso ")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in strDocSap: " & strDocSap)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in Referencia: " & "")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in mporte: " & "0")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in Current user: " & CurrentUser)
                            objActiv.AsignarPagoAcuerdosXDocSap(strDocSap, "", 0, CurrentUser, msgj)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "out msg: " & msgj)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " FIN ROllback Parametros de Entrada AsignarPagoAcuerdosXDocSap PAgo Exitoso ")

                            'DeshacerCambiosPagos(K_PAGON_IDPAGO)
                            'Exit Sub
                        End If

                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Catch del Stored Procedure : SP_CREAR_PAGO_DRA ")
                        'INI PROY-140126
                        Dim MaptPath As String
                        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                        MaptPath = "( Class : " & MaptPath & "; Function: guardarConectado)"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception: " & ex.ToString & MaptPath)
                        'FIN PROY-140126

                        'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Se realiza el rollback al mòdulo.")
                        'DeshacerCambiosPagos(K_PAGON_IDPAGO)
                        'Exit Sub
                    End Try
                    '**FIN Pago de Rentas Adelantadas en Tablas propias de Rentas
                    '*********************************************************************************************************

                    'objCajas.FP_InsertaEfectivo(Session("ALMACEN"), "60138", dblEfectivo)  'Se inserta el efectivo obtenido
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio FP_InsertaEfectivo.")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp ALMACEN: " & Session("ALMACEN"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp USUARIO: " & Session("USUARIO"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp dblEfectivo: " & CStr(dblEfectivo))
                    Dim dFecha As Date
                    Dim sFecha As String = Format(Now.Day, "00").ToString.Trim & "/" & Format(Now.Month, "00").ToString.Trim & "/" & Format(Now.Year, "0000").ToString.Trim
                  
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Fecha Pedido : " & sFecha)

                    Try
                        objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), dblEfectivo, sFecha)     'Se inserta el efectivo obtenido
                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Entro al Error de insertar efectivo")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Error " & ex.Message.ToString())
                    End Try

                    'objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), dblEfectivo)     'Se inserta el efectivo obtenido
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin FP_InsertaEfectivo.")


                    '***********************************************************************************************************
                    '**INICIO FC***'
                    '*** REGISTRO DE DATOS PARA EL FLUJO DE CAJA ***'
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Registro datos del Flujo Caja")
                    objCajas = New COM_SIC_Cajas.clsCajas
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Seteamos la fecha para la concatenaciòn")
                    Dim cultureName As String = "es-PE"
                    Dim culture As CultureInfo = New CultureInfo(cultureName)
                    Dim dateTimeValue As DateTime
                    dateTimeValue = Convert.ToDateTime(DateTime.Now, culture)
                    Dim vDRAV_NRO_DRA As String = sNroAcuerdo

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "---------------------------------------------------------------------------------------------------------- ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Registro datos del Flujo Caja")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Formato de fechas para FC: yyyymmdd")

                    '''*****fechaCajas = dateTimeValue.ToShortDateString.Substring(6, 4) & dateTimeValue.ToShortDateString.Substring(3, 2) & dateTimeValue.ToShortDateString.Substring(0, 2)
                    'PROY-140397-MCKINSEY -> JSQ INICIO
                        fechaCajas = Format(Now.Year, "0000").ToString.Trim & Format(Now.Month, "00").ToString.Trim & Format(Now.Day, "00").ToString.Trim
                    'PROY-140397-MCKINSEY -> JSQ FIN
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- fechaCajas : " & fechaCajas.ToString)
                    dsDatosPedido = objConsultaMsSap.ConsultaPedido(drPagos.Item("PEDIN_NROPEDIDO"), "", "")
                    ''''1. Registro de pago: TABLA "TI_VENTAS_FACT"
                    '''*Para las notas de crèdito el monto del saldo enviado debe ser 0.00

                    Dim PEDIC_CLASEFACTURA As String = "" 'valor que sera reemplazado por una key si es una recarga virtual frecuente

                    Try
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio método : RegistrarPago")

                        If dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEPEDIDO") = System.Configuration.ConfigurationSettings.AppSettings("PEDIC_CLASEPEDIDO_RVF") Then
                            PEDIC_CLASEFACTURA = System.Configuration.ConfigurationSettings.AppSettings("DESC_BOLETA_RVF")
                        Else
                            PEDIC_CLASEFACTURA = Funciones.CheckStr(dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"))
                        End If

                        resCajas = objCajas.RegistrarPago(Session("ALMACEN"), fechaCajas, _
                                                       Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & Session("USUARIO")), 10), _
                                                       dsDatosPedido.Tables(0).Rows(0).Item("PEDIV_DESCCLASEFACTURA"), _
                                                       strDocSap, vDRAV_NRO_DRA, _
                                                       Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & dsDatosPedido.Tables(0).Rows(0).Item("VENDEDOR")), 10), _
                                                       System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                                       PEDIC_CLASEFACTURA, _
                                                       dsDatosPedido.Tables(0).Rows(0).Item("DEPEN_NROCUOTA"), _
                                                       dsDatosPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), _
                                                       IIf(dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA") <> ConfigurationSettings.AppSettings("strTipDoc"), dsDatosPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), 0), _
                                                       IIf(IsDBNull(dsDatosPedido.Tables(0).Rows(0).Item("PEDIV_NROREFNCND")), "", dsDatosPedido.Tables(0).Rows(0).Item("PEDIV_NROREFNCND")), _
                                                       System.Configuration.ConfigurationSettings.AppSettings("ESTADO_VENTA_FLUJOCAJA"), _
                                                       Funciones.CheckStr(Right(System.Net.Dns.GetHostName, 2)), _
                                                       System.Configuration.ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), _
                                                       P_ID_TI_VENTAS_FACT, P_MSGERR)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin método : RegistrarPago")
                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al tratar de ejecutar el método:  RegistrarPago")
                    End Try

                    If P_ID_TI_VENTAS_FACT > 0 Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Código generado en la tabla TI_VENTAS_FACT=> " & P_ID_TI_VENTAS_FACT.ToString)

                        '''''''''2. Registro RegistrarPagoDetalle
                        If Funciones.CheckStr(dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")) <> ConfigurationSettings.AppSettings("strTipDoc") Then '** Las notas de crèdito no van a formar parte de este proceso **'
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia el proceso para el RegistrarPagoDetalle")
                            For i = 1 To hidNumFilas.Value
                                Dim txtMonto As Double
                                Dim cboTipDocumento As String
                                Dim cboDescTipDocumento As String

                                'PROY-27440 INI

                                objTxt = CType(Me.FindControl("txtMonto1"), TextBox)
                                strMonto = "" : strMonto = Trim(objTxt.Text)


                                cboTipDocumento = strTipoDocPago
                                txtMonto = Double.Parse(strMonto)
                                cboDescTipDocumento = DevuelteTipoDescPago(cboTipDocumento)
                                resCajas = 0

                                '''''''''''**Registra los medios de pago seleccionado.
                                '''''''''IIf(arrayListaReferencia(i) = "", "", Funciones.CheckStr(arrayListaReferencia(i))), _
                                resCajas = objCajas.RegistrarPagoDetalle(P_ID_TI_VENTAS_FACT, fechaCajas, strDocSap, _
                                                               vDRAV_NRO_DRA, PEDIC_CLASEFACTURA, _
                                                               cboTipDocumento, _
                                                               IIf(arrayListaRefSunat(i) = "", "", Funciones.CheckStr(arrayListaRefSunat(i))), _
                                                               txtMonto, _
                                                               System.Configuration.ConfigurationSettings.AppSettings("ESTADO_VENTA_FLUJOCAJA"), _
                                                               System.Configuration.ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), _
                                                               P_MSGERR)

                            Next
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Termina el proceso para el RegistrarPagoDetalle")
                            '''''''''''''''''''* FIN 2 *'

                            ''''''''''* 3. RegistrarVentaDetalle, consulto el pedido para asegurar la data
                            '''''''''** Registro de los MATERIALES(TI_MATER_FACT):
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia el proceso para el RegistrarVentaDetalle.")
                            If Not dsDatosPedido Is Nothing Then
                                If dsDatosPedido.Tables(1).Rows.Count > 0 Then
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia el proceso para registrar la venta en el detalle.")
                                    resCajas = 0
                                    For k As Integer = 0 To dsDatosPedido.Tables(1).Rows.Count - 1
                                        resCajas = objCajas.RegistrarVentaDetalle(Session("ALMACEN"), fechaCajas, _
                                                                                    Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & Session("USUARIO")), 10), _
                                                                                    dsDatosPedido.Tables(0).Rows(0).Item("PEDIV_DESCCLASEFACTURA"), _
                                                                                    strDocSap, vDRAV_NRO_DRA, _
                                                                                    Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & dsDatosPedido.Tables(0).Rows(0).Item("VENDEDOR")), 10), _
                                                                                    dsDatosPedido.Tables(1).Rows(k).Item("DEPEC_CODMATERIAL"), _
                                                                                    dsDatosPedido.Tables(1).Rows(k).Item("DEPEV_DESCMATERIAL"), _
                                                                                    "UN", 1, _
                                                                                    dsDatosPedido.Tables(1).Rows(k).Item("DEPEN_PRECIOIGV"), _
                                                                                    IIf(IsDBNull(dsDatosPedido.Tables(1).Rows(k).Item("SERIC_CODSERIE")), "", dsDatosPedido.Tables(1).Rows(k).Item("SERIC_CODSERIE")), _
                                                                                    IIf(IsDBNull(dsDatosPedido.Tables(1).Rows(k).Item("DEPEV_CODIGOLP")), "", dsDatosPedido.Tables(1).Rows(k).Item("DEPEV_CODIGOLP")), _
                                                                                    IIf(IsDBNull(dsDatosPedido.Tables(1).Rows(k).Item("DEPEV_DESCRIPCIONLP")), "", dsDatosPedido.Tables(1).Rows(k).Item("DEPEV_DESCRIPCIONLP")), _
                                                                                    Funciones.CheckStr(Right(System.Net.Dns.GetHostName, 2)), _
                                                                                    System.Configuration.ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), _
                                                                                    P_MSGERR)
                                    Next
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin el proceso para registrar la venta en el detalle.")
                                    If P_MSGERR <> "" Then
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al registrar la venta detalle.")
                                        DeshacerCambiosPagos(K_PAGON_IDPAGO)
                                        Response.Write("<script>alert('" & P_MSGERR & "')</script>")
                                        Exit Sub
                                    Else
                                        '**********************************************
                                        '*** 4. Registramos RegistrarDetalleCuota   ***'
                                        '*** DETALLA EL NUMERO DE CUOTAS DEL PEDIDO ***'
                                        '**********************************************
                                        resCajas = 0
                                        If dsDatosPedido.Tables(0).Rows.Count > 0 Then
                                            '**REGISTRA LAS CUTOAS DEL PEDIDO SI EXISTIERA **'
                                            If dsDatosPedido.Tables(0).Rows(0).Item("DEPEN_NROCUOTA") > 0 Then
                                                '''For c As Integer = 1 To dsDatosPedido.Tables(0).Rows(0).Item("DEPEN_NROCUOTA")
                                                resCajas = objCajas.RegistrarDetalleCuota(P_ID_TI_VENTAS_FACT, fechaCajas, _
                                                                                          Funciones.CheckStr(strDocSap), _
                                                                                          vDRAV_NRO_DRA, dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"), _
                                                                                          dsDatosPedido.Tables(0).Rows(0).Item("DEPEN_NROCUOTA"), _
                                                                                          Funciones.CheckDbl(dsDatosPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")), _
                                                                                          System.Configuration.ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), _
                                                                                          P_MSGERR)
                                                ''''Next
                                            End If
                                        End If
                                    End If
                                Else
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "No se encontraron datos al consultar el pedido.")
                                End If
                            Else
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "La consulta no se realizo correctamente.")
                            End If
                        End If  '** FIN IF DE LA NC
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Error devuelto : " & P_MSGERR.ToString)
                        Response.Write("<script>alert('" & P_MSGERR & "')</script>")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Error, no se realizo el registro en TI_VENTAS_FACT")
                        'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Se realiza el rollback al mòdulo.")
                        'DeshacerCambiosPagos(K_PAGON_IDPAGO)
                        Exit Sub
                    End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Registro datos del Flujo Caja")
                    '**FIN FC
                    '*********************************************************************************************************

                End If

            End If
            'K_NROLOG, K_DESLOG


            Dim sUrl As String = "PoolPagos.aspx"
            Dim RecibidoDolares As Double
            RecibidoDolares = CDbl(txtTipoCambio.Text) * CDbl(txtRecibidoUsd.Text)
            strDocSunat = ""
            Dim pTipDoc As String = "DG"
            Dim flagVentaEquipoPrepago As String = "0"

            Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Llama a la impresiòn del documento: " & Funciones.CheckStr(dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")))

                sUrl &= "?pImp=S" & _
               "&pDocSap=" & strDocSap & _
               "&pDocSunat=" & strDocSunat & _
               "&pNroDG=" & strNroDG & _
                       "&pTipDoc=" & pTipDoc & _
                       "&strEfectivo=" & txtNeto.Text & _
                       "&strRecibidoUS=" & CStr(RecibidoDolares) & _
                       "&strRecibido=" & txtRecibidoPen.Text & _
                       "&strEntregar=" & txtVuelto.Text & _
                       "&flagVentaEquipoPrepago=" & flagVentaEquipoPrepago & _
                       "&strOrigen=" & 1
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Pedido ya fue pagado.")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Error al tratar de ejecutar la impresiòn.")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            End Try
            Response.Redirect(sUrl)
        End If
    End Sub
    '=== Fin Mètodo: guardarConectado

    '******************Inicio - Funcion Paperless******************

    Private Function Paperless(ByVal K_CU_CORRELATIVOFE As String, _
                                    ByVal K_NRO_PEDIDO As Int64, ByVal K_PAGON_IDPAGO As Int64, ByVal origen As String) As String

        Dim objFE As New COM_SIC_FacturaElectronica.PaperLess
        Dim objCajas As New COM_SIC_Cajas.clsCajas

        Dim mensajePpl As String = ""
        Dim estado As String = ""
        Dim referenciaBF As String = ""
        Dim SociedadSap As String = ""
        Dim strCorrSunat As String = ""
        Dim strIdentifyLog As String = ""

        referenciaBF = Obtener_serie_correlativo(K_CU_CORRELATIVOFE, "3")
        strCorrSunat = Obtener_serie_correlativo(K_CU_CORRELATIVOFE, "2")

        strIdentifyLog = Funciones.CheckStr(K_NRO_PEDIDO)

        SociedadSap = ConfigurationSettings.AppSettings("Cod_SociedadPE").ToString() 'MOD: PROY 32815
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio de envio al metodo (GenerarFacturaElectronicaMSSAP)")
        mensajePpl = _
        objFE.GenerarFacturaElectronicaMSSAP( _
            strIdentifyLog, _
K_PAGON_IDPAGO, _
            Session("ALMACEN"), _
            referenciaBF, _
            SociedadSap)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin de envio al metodo (GenerarFacturaElectronicaMSSAP)")

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio de envio al metodo (RegistrarAuditoria)")
        RegistrarAuditoria("Envio factura electrónica NumSunat = " & strCorrSunat, CheckStr(ConfigurationSettings.AppSettings("codTrsPaperless")))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin de envio al metodo (RegistrarAuditoria)")

        estado = mensajePpl

        If estado = "F" Then
            estado = "E"
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Actualización del Estado de la Boleta o Factura (SP_UPD_ESTADO_DOCUM)")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Numero de DocSap   :" & strIdentifyLog)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Origen             :" & origen)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Estado             :" & estado)

        Dim updateEstado As String = objCajas.Actualizar_Estado_Pago(strIdentifyLog, origen, estado)

        If updateEstado = "" Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out updateEstado  :" & updateEstado)

        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out updateEstado  :" & updateEstado)
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin de la ctualización del Estado de la Boleta o Factura.")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")

    End Function

    Private Function Obtener_serie_correlativo(ByVal K_CU_CORRELATIVOFE As String, ByVal tipo As String) As String

        Try

            Dim arrayCorr As String()
            Dim referenciaBF As String = ""
            Dim SociedadSap As String = ""
            Dim SerieSunat As String = ""
            Dim strCorrSunat As String = ""
            Dim cadena As String = ""
            Dim tipoDoc_Sap As String = ""
            Dim Nro_Referencia_Sunat As String = ""

            arrayCorr = CheckStr(K_CU_CORRELATIVOFE).Split("-")
            SerieSunat = (arrayCorr(0).ToString).Substring(1, (arrayCorr(0).ToString).Length - 1) 'serie
            strCorrSunat = (CInt(arrayCorr(1).ToString)).ToString
            referenciaBF = Funciones.CheckStr(SerieSunat).ToString.Trim & "-" & Funciones.CheckStr(strCorrSunat).ToString.Trim

            If arrayCorr(0).StartsWith("B") Then
                tipoDoc_Sap = "E3"
            ElseIf arrayCorr(0).StartsWith("F") Then
                tipoDoc_Sap = "E1"
            End If

            Nro_Referencia_Sunat = tipoDoc_Sap & "-" & Format(Funciones.CheckInt(SerieSunat), "00000") & "-" & Format(Funciones.CheckInt(strCorrSunat), "0000000")

            Select Case tipo
                Case 1
                    cadena = SerieSunat
                Case 2
                    cadena = strCorrSunat
                Case 3
                    cadena = referenciaBF
                Case 4
                    cadena = Nro_Referencia_Sunat
            End Select

            Return cadena
        Catch ex As Exception
            Return ""
        End Try

    End Function

    '************************Registro de Auditoria de Paperless
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

    '***************************************************************************************************

    Private Sub DeshacerCambiosPagos(ByVal K_PAGON_IDPAGO As Int64)
        '*** PROCESO DE ROLLBACK AL PROCESO DE PAGOS *****************************************'
        If K_PAGON_IDPAGO > 0 Then
            Dim K_NROLOG As String = ""
            Dim K_DESLOG As String = ""
            Dim strIdentifyLog As String = String.Format("--[{0}]--", numeroOperacion)

            objTrsMsSap.DeshacerCambiosPedidoPagado(K_PAGON_IDPAGO, K_NROLOG, K_DESLOG)
            If K_DESLOG = "OK" Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  EL RollBack al pedido :" & K_PAGON_IDPAGO & " se ejecuto con errores.")
                Response.Redirect("PoolPagos.aspx")
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  EL RollBack al pedido :" & K_PAGON_IDPAGO & " se ejecuto correctamene.")
            End If
        End If
    End Sub


    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        'Try
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia el mètodo guardarConectado ")
        Dim isRecargaVirtual As Boolean = Session("recargaVirtual")
        guardarConectado()
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin del mètodo guardarConectado")
        'Catch ex As Exception
        '    objFileLog.Log_WriteLog(pathFile, strArchivo, "")
        'End Try
    End Sub

    ' Inicio E77568

#Region "Tipificaciones"

    Public Function Tipificacion1(ByVal strIdentifyLog As String, ByVal numeroTelefono As String, ByVal documentoSAP As String, ByVal vendedor As String) As String
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio Tipificacion1() ---")

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "numeroTelefono: " & numeroTelefono)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "documentoSAP: " & documentoSAP)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "vendedor: " & vendedor)
        Dim idInteraccion1 As String = Tipificacion1(numeroTelefono, documentoSAP, vendedor)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin Tipificacion1() ---")

        Return idInteraccion1
    End Function

    Public Sub Tipificacion2(ByVal strIdentifyLog As String, ByVal numeroTelefono As String, ByVal documentoSAP As String, ByVal vendedor As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio Tipificacion2() ---")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "numeroTelefono: " & numeroTelefono)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "documentoSAP: " & documentoSAP)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "vendedor: " & vendedor)

        Tipificacion2(numeroTelefono, documentoSAP, vendedor)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin Tipificacion2() ---")
    End Sub

    Public Sub Tipificacion3(ByVal strIdentifyLog As String, ByVal numeroTelefono As String, ByVal documentoSAP As String, ByVal vendedor As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio Tipificacion3() ---")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "numeroTelefono: " & numeroTelefono)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "documentoSAP: " & documentoSAP)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "vendedor: " & vendedor)

        Tipificacion3(numeroTelefono, documentoSAP, vendedor)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin Tipificacion3() ---")
    End Sub

    Public Sub Tipificacion4(ByVal strIdentifyLog As String, ByVal numeroTelefono As String, ByVal documentoSAP As String, ByVal vendedor As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio Tipificacion4() ---")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "numeroTelefono: " & numeroTelefono)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "documentoSAP: " & documentoSAP)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "vendedor: " & vendedor)

        Tipificacion4(numeroTelefono, documentoSAP, vendedor)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin Tipificacion4() ---")
    End Sub

#End Region

    ' Fin E77568

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Cancelar()
    End Sub

    '** ENTRA AQUI PARA LOS PROCESOS DE RECARGAS VIRTUALES **'
    '** EL PARAMETRO strNumAsignado, NO ES USADO. **'

    Private Sub Cancelar()

        Session("numeroTelefono") = Nothing
        Session("numeroOperacionSAP") = Nothing
        Session("recargaVirtual") = Nothing
        Session("DocSel") = Nothing

        If Session("VentaR") = "1" Then
            Session("VentaR") = ""
            Response.Redirect("Terminar.aspx")
        Else
            Session("FechaPago") = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
            Response.Redirect("PoolPagos.aspx")
        End If
    End Sub

    Private Function Decimales(ByVal valor)
        If Trim(valor) <> "" Then
            If CDbl(Trim(valor)) <> 0 Then
                Decimales = Left(valor, Len(valor) - 2) & "." & Mid(valor, Len(valor) - 1, 2)
            Else
                Decimales = "0.00"
            End If
        Else
            Decimales = "0.00"
        End If
    End Function

    Private Function valida_tarjeta() As Boolean
        Dim bReturn As Boolean = True
        Dim objSap As New SAP_SIC_Pagos.clsPagos
        Dim dvTmp As DataView

        Dim dsTarjetas As DataSet = objSap.Get_Tarjeta_Credito()
        If cboTipDocumento1.SelectedValue.Trim().Length > 0 Then
            dvTmp = New DataView(dsTarjetas.Tables(0), "CCINS='" + cboTipDocumento1.SelectedValue + "'", "CCINS", DataViewRowState.CurrentRows)
            If dvTmp.Table.Rows.Count > 0 Then
                Dim obCajas As New COM_SIC_Cajas.clsCajas
                If Not obCajas.FP_ValidaBin(txtDoc1.Text.Substring(0, 4)) Then
                    Response.Write("<script language=jscript> alert('El Prefijo de la Tarjeta seleccionada no se encuentra ..!!'); </script>")
                End If
            End If
        End If
        Return bReturn
    End Function

    Private Sub LeeDatosValidar()
        '***************LEE TARJETAS CREDITO
        Dim objSap As New SAP_SIC_Pagos.clsPagos
        'Dim dsTmp As DataSet = objSap.Get_Tarjeta_Credito() ''TODO: CALLBACK SAP
        ''TODO: CAMBIADO POR JYMMY TORRES
        Dim dsTmp As DataSet = (New COM_SIC_OffLine.clsOffline).Get_Tarjeta_Credito
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

    Private Sub UploadArchivo(ByVal sDocSap As String, ByVal sArchivo As String)
        'Dim user As String = HttpContext.Current.User.Identity.Name
        'Dim user2 As String = System.Security.Principal.WindowsIdentity.GetCurrent.Name
        'Dim impersonationContext As System.Security.Principal.WindowsImpersonationContext
        'Dim currentWindowsIdentity As System.Security.Principal.WindowsIdentity

        'currentWindowsIdentity = CType(HttpContext.Current.User.Identity, System.Security.Principal.WindowsIdentity)
        'impersonationContext = currentWindowsIdentity.Impersonate()

        If Path.GetFileName(sArchivo).ToString <> "" Then
            'Constante con la ruta: \\fileserver1\intradoc\SICAR
            Dim sPath As String = ConfigurationSettings.AppSettings("k_gStrRutaUploadDoc")
            'Dim sPath As String = Server.MapPath("~/Upload")

            'Se verifica la existencia del archivo local
            Dim dir As DirectoryInfo = New DirectoryInfo(sPath)
            'Dim files As FileInfo() = dir.GetFiles()

            'Dim count As Integer = files.Length
            'Dim i As Integer
            If (File.Exists(sPath & "\" & Path.GetFileName(sArchivo))) Then
                Response.Write("<script language='javascript'>alert('Este archivo ya existe')</script>")
                Exit Sub
            End If

            'Concatenando la ruta y el nuevo nombre del archivo
            Dim Destino As String = sPath & "\" & Session("ALMACEN") & "_" & Date.Now.ToString("yyyyMMdd") & "_" & sDocSap & Path.GetExtension(sArchivo)

            ' Haciendo Upload del Archivo
            txtFile.PostedFile.SaveAs(Destino)

            'impersonationContext.Undo()

            lblArchivo.Style.Item("display") = ""
            lblArchivo.Text = CStr(Session("ALMACEN") & "_" & Date.Now.ToString("yyyyMMdd") & "_" & sDocSap & Path.GetExtension(sArchivo))
            btnCargarDocumento.Style.Item("display") = "none"
            Session("Carga") = 1
            'Return True
        Else
            Session("Carga") = 0
            Response.Write("<script language='javascript'>alert('Debe elegir un archivo')</script>")
            'Return False
            'impersonationContext.Undo()
        End If
    End Sub

    Private Sub UploadArchivo4(ByVal sDocSap As String, ByVal sArchivo As String)
        Dim objDocumento As Object
        Session("ALMACEN") = "Prueba"
        'Dim sArchivo As String = txtFile.Value
        ' Dim sDocSap As String = "dato"
        Dim sPath As String = ConfigurationSettings.AppSettings("k_gStrRutaUploadDoc") & "\"
        Dim objDocumento2 As Object
        Dim lstrOrigen, lstrDestino As String
        Dim blnExisteArchivo As Boolean = False

        objDocumento = CreateObject("Documento_Util.clsUtilitario")
        objDocumento2 = CreateObject("LoadDocumento.clsLoad")
        Dim impersonationContext As System.Security.Principal.WindowsImpersonationContext
        Dim currentWindowsIdentity As System.Security.Principal.WindowsIdentity

        currentWindowsIdentity = CType(HttpContext.Current.User.Identity, System.Security.Principal.WindowsIdentity)
        impersonationContext = currentWindowsIdentity.Impersonate()

        blnExisteArchivo = objDocumento.FP_existeArchivo(sPath & Path.GetFileName(sArchivo))

        If Not blnExisteArchivo Then
            lstrOrigen = sArchivo
            lstrDestino = sPath & "\" & Session("ALMACEN") & "_" & Date.Now.ToString("yyyyMMdd") & "_" & sDocSap & Path.GetExtension(sArchivo)
            Call objDocumento2.FP_Load(CStr(lstrOrigen), CStr(lstrDestino))
        End If
        objDocumento = Nothing

        impersonationContext.Undo()
    End Sub

    Private Sub UploadArchivo2(ByVal sDocSap As String, ByVal sArchivo As String)

        'Dim impersonationContext As System.Security.Principal.WindowsImpersonationContext
        'Dim currentWindowsIdentity As System.Security.Principal.WindowsIdentity

        ''currentWindowsIdentity = CType(HttpContext.Current.User.Identity, System.Security.Principal.WindowsIdentity)
        'currentWindowsIdentity = CType(HttpContext.Current.User.Identity, System.Security.Principal.WindowsIdentity)
        'impersonationContext = currentWindowsIdentity.Impersonate()

        If Path.GetFileName(sArchivo).ToString <> "" Then
            'Constante con la ruta: \\fileserver1\intradoc\SICAR
            Dim RequestXForm As Object
            Dim objFile As Object

            Dim sPath As String = ConfigurationSettings.AppSettings("k_gStrRutaUploadDoc")

            RequestXForm = CreateObject("ABCUpload4.XForm")
            RequestXForm.MaxUploadSize = 138860800
            RequestXForm.AbsolutePath = True
            RequestXForm.Overwrite = True

            objFile = RequestXForm("txtFile")(1)
            If (objFile.rawFileName <> "") Then
                sArchivo = objFile.rawFileName
            End If

            'Concatenando la ruta y el nuevo nombre del archivo
            Dim Destino As String = sPath & "\" & Session("ALMACEN") & "_" & CStr(Date.Now.ToString("yyyyMMdd")) & "_" & sDocSap & Path.GetExtension(sArchivo)

            'Grabamos el archivo en el origen
            objFile.Save(Destino)

            'impersonationContext.Undo()

            lblArchivo.Style.Item("display") = ""
            lblArchivo.Text = CStr(Session("ALMACEN") & "_" & Date.Now.ToString("yyyyMMdd") & "_" & sDocSap & Path.GetExtension(sArchivo))
            btnCargarDocumento.Style.Item("display") = "none"
            Session("Carga") = 1
            'Return True
        Else
            Session("Carga") = 0
            Response.Write("<script language='javascript'>alert('Debe elegir un archivo')</script>")
            'Return False
        End If
        hidFlagCargaDoc.Value = Session("Carga")
    End Sub

    Private Sub UploadArchivo3(ByVal sDocSap As String, ByVal sArchivo As String)

        Dim lObjDocumento As Object
        Dim objDocumento As Object
        Dim lstrRutaOrigen, lstrNombreOrigen, lstrRutaDestino, lstrNombreDestino As String
        Dim blnExisteArchivo As Boolean = False
        Dim blnGrabarArchivo As Boolean = False

        Dim sPath As String = ConfigurationSettings.AppSettings("k_gStrRutaUploadDoc")

        objDocumento = CreateObject("Documento_Util.clsUtilitario")
        Dim impersonationContext As System.Security.Principal.WindowsImpersonationContext
        Dim currentWindowsIdentity As System.Security.Principal.WindowsIdentity

        currentWindowsIdentity = CType(HttpContext.Current.User.Identity, System.Security.Principal.WindowsIdentity)
        impersonationContext = currentWindowsIdentity.Impersonate()

        blnExisteArchivo = objDocumento.FP_existeArchivo(sPath & Path.GetFileName(sArchivo))

        If Not blnExisteArchivo Then
            lstrRutaOrigen = Path.GetDirectoryName(sArchivo)
            lstrNombreOrigen = Path.GetFileName(sArchivo)
            lstrRutaDestino = sPath
            lstrNombreDestino = Session("ALMACEN") & "_" & Date.Now.ToString("yyyyMMdd") & "_" & sDocSap & Path.GetExtension(sArchivo)
            blnGrabarArchivo = objDocumento.FP_CopiaArchivo(CStr(lstrNombreOrigen), CStr(lstrNombreDestino), "", lstrRutaOrigen, lstrRutaDestino)
        End If
        objDocumento = Nothing

        Response.Write("<script language='javascript'>alert('" & blnGrabarArchivo.ToString & "')</script>")    'Si es TRUE grabo correctamente

        If blnGrabarArchivo.ToString = "true" Then
            Session("Carga") = 1
        Else
            Session("Carga") = 0
        End If

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

    Private Sub ActualizaPago(ByVal strNroPedido As String)

        Dim intResultado As Integer
        Dim resultado As Integer
        Dim objCajas As New COM_SIC_Cajas.clsCajas

        If Not strNroPedido.Trim = "" Then
            resultado = objCajas.ActualizaPago(strNroPedido, 1, intResultado)
        End If

    End Sub

    Function FormatoTelefonoPrepago(ByVal telefono)
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
        End If
        If aux = "" Then
            FormatoTelefonoPrepago = telefono
        Else
            FormatoTelefonoPrepago = aux
        End If
    End Function

    Private Function ValidaCadena(ByVal cadena As String) As String
        Dim strNew As String = ""
        If Trim(cadena) <> "" Then
            For i As Int32 = 0 To cadena.Length - 1
                If cadena.Chars(i) <> "'" Then
                    strNew = strNew + cadena.Chars(i)
                End If
            Next
        End If
        Return strNew
    End Function

    'Function ValidacionVentaDOL(ByVal nroDocumSAP As String, ByVal nroPedido As String) As Boolean

    '    Dim dsPedido As New DataSet
    '    Dim strCodArticulo As String
    '    Dim flag As Boolean = False

    '    Try
    '        'dsPedido = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), nroDocumSAP, "")
    '        Dim objMsSAP As New COM_SIC_Activaciones.clsConsultaMsSap
    '        Dim strAlmacen As String = objMsSAP.ConsultaPuntoVenta(Session("ALMACEN")).Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA")

    '        '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'
    '        If strAlmacen.Trim <> "" Then
    '            dsPedido = objConsultaMsSap.ConsultaPedido(nroPedido, "", "")
    '        End If
    '        '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'

    '        If Not IsNothing(dsPedido) Then
    '            If dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA") = ConfigurationSettings.AppSettings("strTVPrepago") And _
    '                dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEPEDIDO") = ConfigurationSettings.AppSettings("strDTVAlta") Then

    '                For i As Integer = 0 To dsPedido.Tables(1).Rows.Count - 1
    '                    strCodArticulo = dsPedido.Tables(1).Rows(i).Item("MATEC_TIPOMATERIAL") 'TODOEB
    '                    ' Validacion de la Venta de Pack Simcard
    '                    If strCodArticulo.ToString.Trim = ConfigurationSettings.AppSettings("constChips") Then
    '                        'Cambio SIM IVR: Insertar Stock PEL
    '                        Dim listaMatCambioSIM As String = ConfigurationSettings.AppSettings("codMatChipRepuesto")
    '                        If listaMatCambioSIM.IndexOf(strCodArticulo) = -1 Then
    '                            flag = True
    '                            Exit For
    '                        End If
    '                    End If
    '                Next

    '                ' Validacion si es Prepago Portabilidad
    '                Dim objCajas As New COM_SIC_Cajas.clsCajas
    '                Dim dsTelefPorta As New DataSet
    '                Dim strTipoVenta As Integer = 0
    '                Dim retornoPorta As Integer

    '                If Not Trim(nroPedido) = "" Then
    '                    Try
    '                        'dsTelefPorta = objCajas.FP_Get_TelefonosPorta(nroPedido, strTipoVenta, retornoPorta)
    '                        If Not dsPedido Is Nothing Then
    '                            dsTelefPorta = objCajas.FP_Get_TelefonosPorta(nroPedido, dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA"), retornoPorta)
    '                        End If
    '                    Catch ex As Exception
    '                        retornoPorta = 1
    '                    End Try

    '                    ' Retorna Datos Portabilidad
    '                    If (retornoPorta = 0) And (Not IsNothing(dsTelefPorta)) Then
    '                        flag = False
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        flag = False
    '    End Try

    '    Return flag
    'End Function

    Private Function Get_CodOpcionBBA(ByVal strCodMaterial As String, ByVal strListaPrecio As String, ByVal strCampana As String, ByVal strGrupoEquipo As String, ByVal strDocSap As String) As String

        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim arrListMateriales As String()
        Dim arrMaterial As String()
        Dim strOpcion As String = ""
        Dim strIdentifyLog As String = strDocSap

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio Get_CodOpcionBBA-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strCodMaterial : " & strCodMaterial)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strGrupoEquipo : " & strGrupoEquipo)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strListaPrecio : " & strListaPrecio)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strCampana : " & strCampana)

            strOpcion = objCajas.Obtener_codigo_opcion(strCodMaterial, strListaPrecio, strCampana, strGrupoEquipo)
            ' SE CAMBIA POR UN STORED QUE OBTIENE EL CODIGO DE OPCION DIRECTAMENTE A PARTIR DE PARAMETROS 
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  strOpcion : " & strOpcion)
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: Get_CodOpcionBBA)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  ERROR : " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  ERROR : " & ex.StackTrace.ToString())
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin Get_CodOpcionBBA-----------")
        End Try

        Return strOpcion
    End Function

    Private Function IsContratoVacio(ByVal sNroContrato As String) As Boolean
        sNroContrato = sNroContrato.Replace("0", "")
        Return (sNroContrato.Trim().Equals(""))
    End Function

    Private Function Tipificacion1(ByVal numeroTelefono As String, ByVal documentoSAP As String, ByVal usuario As String) As String

        Dim strIdentifyLog As String = documentoSAP

        'Guardar Interaccion 
        Dim oInteraccion = New COM_SIC_INActChip.Interaccion

        With oInteraccion
            '.OBJID_CONTACTO = objId
            .TELEFONO = numeroTelefono
            .TIPO = ConfigurationSettings.AppSettings("CONS_RENOVACION_TIPO1")
            .CLASE = ConfigurationSettings.AppSettings("CONS_RENOVACION_CLASE1")
            .SUBCLASE = ConfigurationSettings.AppSettings("CONS_RENOVACION_SUBCLASE1")
            .METODO = ConfigurationSettings.AppSettings("CONS_RENOVACION_METODO")
            .TIPO_INTER = ConfigurationSettings.AppSettings("CONS_RENOVACION_TIPO_INTER")
            .AGENTE = usuario
            .USUARIO_PROCESO = ConfigurationSettings.AppSettings("CONS_RENOVACION_USUARIO")
            .FLAG_CASO = ConfigurationSettings.AppSettings("CONS_RENOVACION_FLAG")
            .RESULTADO = ConfigurationSettings.AppSettings("CONS_RENOVACION_RESULTADO")
            .HECHO_EN_UNO = ConfigurationSettings.AppSettings("CONS_RENOVACION_HECHO")

        End With
        Dim oTipificacion As New COM_SIC_INActChip.clsTipificacion
        Dim flagInter As String
        Dim mensajeInter As String
        Dim idInteraccion As String


        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio Region Renovacion de Equipo Postpago CAC ---")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Tipificacion 1 ---")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo Tipificacion1()")

        Try

            oTipificacion.CrearInteraccion(oInteraccion, idInteraccion, flagInter, mensajeInter)


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Interaccion : " & idInteraccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Metodo : " & flagInter)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeInter)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo Tipificacion1()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin Tipificacion 1 ---")

            ' RegistraPlantilla(numeroTelefono, idInteraccion, documentoSAP)

            ' Inicio E77568
            ' PS_MejorasRenovEquiposCAC_2.0
            Dim objCajas As New COM_SIC_Cajas.clsCajas
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio - Actualizar Numero de Interaccion en Venta Renovacion Postpago - SISACT ---")

            If (objCajas.ActualizarInteraccion_VentaRenovPostCAC(idInteraccion, documentoSAP)) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Actualizó correctamente Numero de Interacción")
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin Actualizar Numero de Interaccion en Venta Renovacion Postpago - SISACT ---")
            ' PS_MejorasRenovEquiposCAC_2.0
            ' Fin E77568
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "error Tipificacion1:" & ex.Message.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "error Tipificacion1:" & ex.StackTrace.ToString())
        End Try

        Return idInteraccion
    End Function

    Private Function Tipificacion2(ByVal numeroTelefono As String, ByVal documentoSAP As String, ByVal usuario As String)

        Dim objCajas As New COM_SIC_Cajas.clsCajas

        Dim strIdentifyLog As String = documentoSAP

        'Guardar Interaccion 
        Dim oInteraccion = New COM_SIC_INActChip.Interaccion
        With oInteraccion
            '.OBJID_CONTACTO = objId
            .TELEFONO = numeroTelefono
            .TIPO = ConfigurationSettings.AppSettings("CONS_RENOVACION_TIPO2")
            .CLASE = ConfigurationSettings.AppSettings("CONS_RENOVACION_CLASE2")
            .SUBCLASE = ConfigurationSettings.AppSettings("CONS_RENOVACION_SUBCLASE2")
            .METODO = ConfigurationSettings.AppSettings("CONS_RENOVACION_METODO")
            .TIPO_INTER = ConfigurationSettings.AppSettings("CONS_RENOVACION_TIPO_INTER")
            .AGENTE = usuario
            .USUARIO_PROCESO = ConfigurationSettings.AppSettings("CONS_RENOVACION_USUARIO")
            .FLAG_CASO = ConfigurationSettings.AppSettings("CONS_RENOVACION_FLAG")
            .RESULTADO = ConfigurationSettings.AppSettings("CONS_RENOVACION_RESULTADO")
            .HECHO_EN_UNO = ConfigurationSettings.AppSettings("CONS_RENOVACION_HECHO")
            ' Inicio E77568
            .NOTAS = ConfigurationSettings.AppSettings("TIPIFICACION2_NOTAS")
            ' Fin E77568
        End With
        Dim oTipificacion As New COM_SIC_INActChip.clsTipificacion
        Dim flagInter As String
        Dim mensajeInter As String
        Dim idInteraccion As String



        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio Region Renovacion de Equipo Postpago CAC ---")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Tipificacion 2 ---")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo Tipificacion2()")

        Try
            oTipificacion.CrearInteraccion(oInteraccion, idInteraccion, flagInter, mensajeInter)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Interaccion : " & idInteraccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Metodo : " & flagInter)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeInter)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo Tipificacion2()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin Tipificacion 2 ---")

            'RegistraPlantilla(numeroTelefono, idInteraccion, documentoSAP)


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio - Actualizar Numero de Interaccion en Venta Renovacion Postpago - SISACT ---")

            If (objCajas.ActualizarInteraccion_VentaRenovPostCAC(idInteraccion, documentoSAP)) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Actualizó correctamente Numero de Interacción")
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin Actualizar Numero de Interaccion en Venta Renovacion Postpago - SISACT ---")


        Catch ex As Exception
        End Try

    End Function

    Private Function Tipificacion3(ByVal numeroTelefono As String, ByVal documentoSAP As String, ByVal usuario As String)

        Dim strIdentifyLog As String = documentoSAP

        'Guardar Interaccion 
        Dim oInteraccion = New COM_SIC_INActChip.Interaccion
        With oInteraccion
            '.OBJID_CONTACTO = objId
            .TELEFONO = numeroTelefono
            .TIPO = ConfigurationSettings.AppSettings("CONS_RENOVACION_TIPO3")
            .CLASE = ConfigurationSettings.AppSettings("CONS_RENOVACION_CLASE3")
            .SUBCLASE = ConfigurationSettings.AppSettings("CONS_RENOVACION_SUBCLASE3")
            .METODO = ConfigurationSettings.AppSettings("CONS_RENOVACION_METODO")
            .TIPO_INTER = ConfigurationSettings.AppSettings("CONS_RENOVACION_TIPO_INTER")
            .AGENTE = usuario
            .USUARIO_PROCESO = ConfigurationSettings.AppSettings("CONS_RENOVACION_USUARIO")
            .FLAG_CASO = ConfigurationSettings.AppSettings("CONS_RENOVACION_FLAG")
            .RESULTADO = ConfigurationSettings.AppSettings("CONS_RENOVACION_RESULTADO")
            .HECHO_EN_UNO = ConfigurationSettings.AppSettings("CONS_RENOVACION_HECHO")

        End With
        Dim oTipificacion As New COM_SIC_INActChip.clsTipificacion
        Dim flagInter As String
        Dim mensajeInter As String
        Dim idInteraccion As String



        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio Region Renovacion de Equipo Postpago CAC ---")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Tipificacion 3 ---")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo Tipificacion3()")

        Try
            oTipificacion.CrearInteraccion(oInteraccion, idInteraccion, flagInter, mensajeInter)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Interaccion : " & idInteraccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Metodo : " & flagInter)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeInter)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo Tipificacion3()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin Tipificacion 3 ---")

            'RegistraPlantilla(numeroTelefono, idInteraccion, documentoSAP)

            ' Inicio E77568
            ' PS_MejorasRenovEquiposCAC_2.0
            Dim objCajas As New COM_SIC_Cajas.clsCajas
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio - Actualizar Numero de Interaccion en Venta Renovacion Postpago - SISACT ---")

            If (objCajas.ActualizarInteraccion_VentaRenovPostCAC(idInteraccion, documentoSAP)) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Actualizó correctamente Numero de Interacción")
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin Actualizar Numero de Interaccion en Venta Renovacion Postpago - SISACT ---")
            ' PS_MejorasRenovEquiposCAC_2.0
            ' Fin E77568
        Catch ex As Exception
        End Try

    End Function

    Private Function Tipificacion4(ByVal numeroTelefono As String, ByVal documentoSAP As String, ByVal usuario As String)

        Dim strIdentifyLog As String = documentoSAP

        'Guardar Interaccion 
        Dim oInteraccion = New COM_SIC_INActChip.Interaccion
        With oInteraccion
            '.OBJID_CONTACTO = objId
            .TELEFONO = numeroTelefono
            .TIPO = ConfigurationSettings.AppSettings("CONS_RENOVACION_TIPO4")
            .CLASE = ConfigurationSettings.AppSettings("CONS_RENOVACION_CLASE4")
            .SUBCLASE = ConfigurationSettings.AppSettings("CONS_RENOVACION_SUBCLASE4")
            .METODO = ConfigurationSettings.AppSettings("CONS_RENOVACION_METODO")
            .TIPO_INTER = ConfigurationSettings.AppSettings("CONS_RENOVACION_TIPO_INTER")
            .AGENTE = usuario
            .USUARIO_PROCESO = ConfigurationSettings.AppSettings("CONS_RENOVACION_USUARIO")
            .FLAG_CASO = ConfigurationSettings.AppSettings("CONS_RENOVACION_FLAG")
            .RESULTADO = ConfigurationSettings.AppSettings("CONS_RENOVACION_RESULTADO")
            .HECHO_EN_UNO = ConfigurationSettings.AppSettings("CONS_RENOVACION_HECHO")

        End With
        Dim oTipificacion As New COM_SIC_INActChip.clsTipificacion
        Dim flagInter As String
        Dim mensajeInter As String
        Dim idInteraccion As String



        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio Region Renovacion de Equipo Postpago CAC ---")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Tipificacion 4 ---")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo Tipificacion4()")

        Try
            oTipificacion.CrearInteraccion(oInteraccion, idInteraccion, flagInter, mensajeInter)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Interaccion : " & idInteraccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Metodo : " & flagInter)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeInter)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo Tipificacion4()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin Tipificacion 4 ---")

            'RegistraPlantilla(numeroTelefono, idInteraccion, documentoSAP)

            ' Inicio E77568
            ' PS_MejorasRenovEquiposCAC_2.0
            Dim objCajas As New COM_SIC_Cajas.clsCajas
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio - Actualizar Numero de Interaccion en Venta Renovacion Postpago - SISACT ---")

            If (objCajas.ActualizarInteraccion_VentaRenovPostCAC(idInteraccion, documentoSAP)) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Actualizó correctamente Numero de Interacción")
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin Actualizar Numero de Interaccion en Venta Renovacion Postpago - SISACT ---")
            ' PS_MejorasRenovEquiposCAC_2.0
            ' Fin E77568

        Catch ex As Exception
        End Try

    End Function

    Function FormatoTelefonoSinCeros(ByVal telefono)
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
        End If
        If aux = "" Then
            FormatoTelefonoSinCeros = telefono
        Else
            FormatoTelefonoSinCeros = aux
        End If
    End Function

    Private Function ListaParamPromocion(ByVal strGrupo As String, ByVal strIdentifyLog As String) As String

        Dim strListMateriales As String = ""

        Try
            Dim dsParam As DataSet
            Dim list_pre As String
            Dim objSicarDB As New COM_SIC_Cajas.clsCajas
            Dim promocion As String
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio FP_ConsultaParametros Promocion")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp: " & strGrupo)
            dsParam = objSicarDB.FP_ConsultaParametros(strGrupo)
            objSicarDB = Nothing
            If Not IsNothing(dsParam) AndAlso dsParam.Tables(0).Rows.Count > 0 Then
                For idx As Integer = 0 To dsParam.Tables(0).Rows.Count - 1
                    strListMateriales += "|" & dsParam.Tables(0).Rows(idx).Item("SPARV_KEY") & ";" & _
                                                dsParam.Tables(0).Rows(idx).Item("SPARV_VALUE") & ";" & _
                                                dsParam.Tables(0).Rows(idx).Item("SPARV_GRUPO")
                Next
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT CantidadRegistros:  " & Funciones.CheckStr(dsParam.Tables(0).Rows.Count))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin FP_ConsultaParametros promocion")

        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: ListaParamPromocion)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  ERROR : " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin FP_ConsultaParametros promocion-----------")

        End Try

        Return strListMateriales

    End Function
    Private Function Get_CodPromocion(ByVal strListaPrecio As String, ByVal strListPromocion As String, ByVal strIdentifyLog As String) As String

        Dim arrListPromocion As String()
        Dim arrMaterial As String()
        Dim codPromocion As String = ""

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio Get_CodPromocion-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strListPromocion : " & strListPromocion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strListaPrecio : " & strListaPrecio)

            arrListPromocion = strListPromocion.Split("|")

            If arrListPromocion.Length > 0 Then
                For idx As Integer = 0 To arrListPromocion.Length - 1
                    arrMaterial = arrListPromocion(idx).Split(";")
                    If arrMaterial.Length > 2 Then
                        If strListaPrecio.ToUpper() = arrMaterial(0).ToUpper() Then
                            codPromocion = arrMaterial(1).ToString()
                            Exit For
                        End If
                    End If
                Next
            End If

            If (codPromocion = "") Then
                codPromocion = "0000"
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  codPromocion : " & codPromocion)
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: Get_CodPromocion)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  ERROR : " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin Get_CodPromocion-----------")
        End Try

        Return codPromocion

    End Function

    Private Sub inicializaControles()
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio proceso de inicializaciòn de controles.")
        Dim dtVias As DataTable
        dtVias = New DataTable
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia al método: VerificarVias ")
        dtVias = VerificarVias(dsFormaPago) ''CALLBAK QUE REVISA LA FILA
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin al método: VerificarVias ")

        txtMonto1.Attributes.Add("onChange", "javascript:f_Recalcular(1)")
        txtMonto2.Attributes.Add("onChange", "javascript:f_Recalcular(2)")
        txtMonto3.Attributes.Add("onChange", "javascript:f_Recalcular(3)")
        txtMonto4.Attributes.Add("onChange", "javascript:f_Recalcular(4)")
        txtMonto5.Attributes.Add("onChange", "javascript:f_Recalcular(5)")

        txtMonto1.Attributes.Add("onkeypress", "javascript:ValidaNumero(this)")
        txtMonto2.Attributes.Add("onkeypress", "javascript:ValidaNumero(this)")
        txtMonto3.Attributes.Add("onkeypress", "javascript:ValidaNumero(this)")
        txtMonto4.Attributes.Add("onkeypress", "javascript:ValidaNumero(this)")
        txtMonto5.Attributes.Add("onkeypress", "javascript:ValidaNumero(this)")

        txtRecibidoPen.Attributes.Add("onkeypress", "javascript:ValidaNumero(this)")
        txtRecibidoUsd.Attributes.Add("onkeypress", "javascript:ValidaNumero(this)")

        txtRecibidoPen.Attributes.Add("onChange", "javascript:CalculoVuelto()")
        txtRecibidoUsd.Attributes.Add("onChange", "javascript:CalculoVuelto()")

        cboLoad(dtVias, cboTipDocumento1)
        cboLoad(dtVias, cboTipDocumento2)
        cboLoad(dtVias, cboTipDocumento3)
        cboLoad(dtVias, cboTipDocumento4)
        cboLoad(dtVias, cboTipDocumento5)
    End Sub

    Private Sub cmdConsultaNC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConsultaNC.Click
        Try
            If ConsultaFormaPagoNotaCredito() = False Then
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function ConsultaFormaPagoNotaCredito() As Boolean
        Dim strDocumento As String = ""
        Dim sw As Double = True
        Dim valArray As String()
        Dim serie As String = ""
        Try
            For i As Integer = 0 To hidNumFilas.Value
                If Trim(Request.Form("cboTipDocumento" & (i + 1))) = "ZNCR" Then
                    strDocumento = Request.Item("txtDoc" & (i + 1))
                    If strDocumento.Trim.Length > 0 Then

                        valArray = strDocumento.ToString.Split("-")
                        serie = valArray(1)
                        'If (strDocumento.ToString.Substring(0, 1) = "B" Or strDocumento.ToString.Substring(0, 1) = "F") Then
                        '    If ConsultaPedidoXCorrelativoGenerado(strDocumento.Trim.ToString, i) = False Then
                        '        sw = False
                        '    End If
                        'End If
                        If (serie.StartsWith("B") Or serie.StartsWith("F")) Then
                            If ConsultaPedidoXCorrelativoGenerado(strDocumento.Trim.ToString, i) = False Then
                                sw = False
                            End If
                        End If
                    End If
                End If
            Next

            Return sw
        Catch ex As Exception

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
                    strTipoDoc = dsdatos.Tables(0).Rows(0).Item("PEDIC_TIPODOCCLIENTE")
                    strNroDoc = dsdatos.Tables(0).Rows(0).Item("PEDIV_NRODOCCLIENTE")

                    'Validaciòn 1:
                    'If dsdatos.Tables(0).Rows(0).Item("PEDIC_ESTADO") <> "PAG" Then
                    '    hidMensajeNC.Value = "La nota de crèdito seleccionada no se encuentra procesada."
                    '    Response.Write("<script>alert('La nota de crèdito seleccionada no se encuentra procesada.')</script>")
                    '    Return False
                    '    Exit Function
                    'End If

                    '**Inicio consultamos pedido original 
                    dsPedido = objConsultaMsSap.ConsultaPedido(drPagos.Item("PEDIN_NROPEDIDO"), "", "") 'TODOEB
                    If Not dsPedido Is Nothing Then
                        If dsPedido.Tables(0).Rows.Count > 0 Then
                            strTipoDoc_origen = dsPedido.Tables(0).Rows(0).Item("CLIEC_TIPODOCCLIENTE")
                            strNroDoc_origen = dsPedido.Tables(0).Rows(0).Item("CLIEV_NRODOCCLIENTE")
                        End If
                    End If
                    '**Fin consulta original

                    'Validaciòn 2:
                    '** Valida si ambos pedidos son del mismo cliente  **'
                    If (strTipoDoc_origen <> strTipoDoc Or strNroDoc_origen <> strNroDoc) Then
                        hidMensajeNC.Value = "La nota de crédito utilizada no corresponde al cliente"
                        Response.Write("<script>alert('La nota de crédito utilizada no corresponde al cliente.')</script>")
                        Return False
                        Exit Function
                    End If

                    'Validaciòn3:
                    '**Verifica no se haya usado con atenrioridad... ***'
                    If dsdatos.Tables(0).Rows(0).Item("PEDIC_ISFORMAPAGO") = 1 Then
                        hidMensajeNC.Value = "La nota de crédito ya fue utilizada en un pago anterior."
                        Response.Write("<script>alert('La nota de crédito ya fue utilizada en un pago anterior.')</script>")
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

    Private Function ConsultaPedidoXCorrelativoGenerado(ByVal strDocumento As String, ByVal pos As Integer) As Boolean
        Try
            Dim dsDatos As DataSet
            Dim NLOG As String = ""
            Dim DLOG As String = ""
            Dim sw As Boolean = True
            PEDIN_PEDIDOSAP = ""


            dsDatos = objConsultaMsSap.ConsultaPedidoXCorrelativo(strDocumento.Trim.ToString, "", NLOG, DLOG)
            If Not dsDatos Is Nothing Then
                If dsDatos.Tables(0).Rows.Count >= 1 Then
                    'Validaciòn Datos NC
                    If ValidaDatosNotaCredito(dsDatos) Then
                        '** guarda los correlativos de las nc usadas para el pago.
                        arrayListaRefSunat(pos + 1) = Funciones.CheckStr(IIf(IsDBNull(dsDatos.Tables(0).Rows(0).Item("PEDIV_NROSUNAT")), "", dsDatos.Tables(0).Rows(0).Item("PEDIV_NROSUNAT")))
                        arrayListaReferencia(pos + 1) = Funciones.CheckStr(IIf(IsDBNull(dsDatos.Tables(0).Rows(0).Item("PEDIN_PEDIDOSAP")), "", dsDatos.Tables(0).Rows(0).Item("PEDIN_PEDIDOSAP")))
                        arrayListaRefPedido(pos + 1) = Funciones.CheckStr(IIf(IsDBNull(dsDatos.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")), "", dsDatos.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")))

                        Select Case pos
                            Case 0
                                txtMonto1.Text = Format(dsDatos.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), "###0.00")
                            Case 1
                                txtMonto2.Text = Format(dsDatos.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), "###0.00")
                            Case 2
                                txtMonto3.Text = Format(dsDatos.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), "###0.00")
                            Case 3
                                txtMonto4.Text = Format(dsDatos.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), "###0.00")
                            Case 4
                                txtMonto5.Text = Format(dsDatos.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), "###0.00")
                        End Select
                    Else
                        sw = False
                    End If
                Else
                    sw = False
                    hidMensajeNC.Value = "No existe ninguna nota de crédito para el documento ingresado."
                    Response.Write("<script>alert('No existe ninguna nota de crédito para el documento ingresado.')</script>")
                    Return False
                End If
            Else
                sw = False
                hidMensajeNC.Value = "No existe ninguna nota de crédito para el documento ingresado."
                Response.Write("<script>alert('No existe ninguna nota de crédito para el documento ingresado.')</script>")
                Return False
            End If

            Return sw
        Catch ex As Exception
            'Response.Write("<script>alert('" & Session("msgCaducidadRMP6") & "')</script>")
        End Try
    End Function

    Private Function VerificarVias(ByVal ds As DataSet) As DataTable

        Dim StrRectricciones As String
        Dim StrEfectivo, StrCheque, StrTarjeta, StrDebito, StrOtros As String
        Dim ArrEfectivo, ArrCheque, ArrTarjeta, ArrDebito, ArrOtros, ArrTotal As ArrayList
        Dim LOpciones As New ArrayList
        Dim dtVias As DataTable = New DataTable("ViasPago")
        Dim boolexiste As Boolean

        Try
            If Session("WS_OpcionesPagina") Is Nothing Then
                Response.Write("<script> alert('Error: No se ubica el perfil para las vias de pago');</script>")
            Else
                LOpciones = Session("WS_OpcionesPagina")
            End If

            'objFileLog.Log_WriteLog(pathFile, strArchivo, "VerificarVias: " & "- constCodigoViasSap : " & ConfigurationSettings.AppSettings("constCodigoViasSap").ToString)
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
    'INICIATIVA-318 INI
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

    Private Sub cboTipDocumento1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipDocumento1.SelectedIndexChanged
        'INI-PROY-140773- SICAR 
        'Dim bolAutorizado As Boolean
        'Dim strFormaPago As String
        'Dim strCombo As String

        'strCombo = "cboTipDocumento1"
        'strFormaPago = cboTipDocumento1.SelectedValue
        hddcboFormaPago.Value = cboTipDocumento1.SelectedValue
        'If cboTipDocumento1.SelectedIndex = 0 Then
        '    bolAutorizado = True
        'Else
        '    bolAutorizado = ObtenerAprobacionFormaPago(cboTipDocumento1.SelectedValue)
        'End If

        'If (bolAutorizado = False) Then
        '    cboTipDocumento1.SelectedIndex = 0
        '    Response.Write("<script language=javascript>alert('Usuario no cuenta con los permisos sufiientes para la forma de Pago');</script>")
        '    Response.Write("<script language=javascript>window.open('frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")
        'End If
        'FIN-PROY-140773- SICAR 
    End Sub
    Private Sub cboTipDocumento2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipDocumento2.SelectedIndexChanged
        'INI-PROY-140773- SICAR 
        'Dim bolAutorizado As Boolean
        'Dim strFormaPago As String
        'Dim strCombo As String

        'strCombo = "cboTipDocumento1"
        'strFormaPago = cboTipDocumento1.SelectedValue
        hddcboFormaPago.Value = cboTipDocumento1.SelectedValue
        'If cboTipDocumento1.SelectedIndex = 0 Then
        '    bolAutorizado = True
        'Else
        '    bolAutorizado = ObtenerAprobacionFormaPago(cboTipDocumento1.SelectedValue)
        'End If

        'If (bolAutorizado = False) Then
        '    cboTipDocumento1.SelectedIndex = 0
        '    Response.Write("<script language=javascript>alert('Usuario no cuenta con los permisos sufiientes para la forma de Pago');</script>")
        '    Response.Write("<script language=javascript>window.open('frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")
        'End If
        'FIN-PROY-140773- SICAR 
    End Sub

    Private Sub cboTipDocumento3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipDocumento3.SelectedIndexChanged
        'INI-PROY-140773- SICAR 
        'Dim bolAutorizado As Boolean
        'Dim strFormaPago As String
        'Dim strCombo As String

        'strCombo = "cboTipDocumento1"
        'strFormaPago = cboTipDocumento1.SelectedValue
        hddcboFormaPago.Value = cboTipDocumento1.SelectedValue
        'If cboTipDocumento1.SelectedIndex = 0 Then
        '    bolAutorizado = True
        'Else
        '    bolAutorizado = ObtenerAprobacionFormaPago(cboTipDocumento1.SelectedValue)
        'End If

        'If (bolAutorizado = False) Then
        '    cboTipDocumento1.SelectedIndex = 0
        '    Response.Write("<script language=javascript>alert('Usuario no cuenta con los permisos sufiientes para la forma de Pago');</script>")
        '    Response.Write("<script language=javascript>window.open('frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")
        'End If
        'FIN-PROY-140773- SICAR 
    End Sub

    Private Sub cboTipDocumento4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipDocumento4.SelectedIndexChanged
        'INI-PROY-140773- SICAR 
        'Dim bolAutorizado As Boolean
        'Dim strFormaPago As String
        'Dim strCombo As String

        'strCombo = "cboTipDocumento1"
        'strFormaPago = cboTipDocumento1.SelectedValue
        hddcboFormaPago.Value = cboTipDocumento1.SelectedValue
        'If cboTipDocumento1.SelectedIndex = 0 Then
        '    bolAutorizado = True
        'Else
        '    bolAutorizado = ObtenerAprobacionFormaPago(cboTipDocumento1.SelectedValue)
        'End If

        'If (bolAutorizado = False) Then
        '    cboTipDocumento1.SelectedIndex = 0
        '    Response.Write("<script language=javascript>alert('Usuario no cuenta con los permisos sufiientes para la forma de Pago');</script>")
        '    Response.Write("<script language=javascript>window.open('frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")
        'End If
        'FIN-PROY-140773- SICAR 
    End Sub

    Private Sub cboTipDocumento5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipDocumento5.SelectedIndexChanged
        'INI-PROY-140773- SICAR 
        'Dim bolAutorizado As Boolean
        'Dim strFormaPago As String
        'Dim strCombo As String

        'strCombo = "cboTipDocumento1"
        'strFormaPago = cboTipDocumento1.SelectedValue
        hddcboFormaPago.Value = cboTipDocumento1.SelectedValue
        'If cboTipDocumento1.SelectedIndex = 0 Then
        '    bolAutorizado = True
        'Else
        '    bolAutorizado = ObtenerAprobacionFormaPago(cboTipDocumento1.SelectedValue)
        'End If

        'If (bolAutorizado = False) Then
        '    cboTipDocumento1.SelectedIndex = 0
        '    Response.Write("<script language=javascript>alert('Usuario no cuenta con los permisos sufiientes para la forma de Pago');</script>")
        '    Response.Write("<script language=javascript>window.open('frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")
        'End If
        'FIN-PROY-140773- SICAR 
    End Sub
    'INICIATIVA-318 FIN
End Class