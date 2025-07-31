Imports System.Globalization
Imports COM_SIC_Activaciones 'INICIATIVA-318

Public Class docRecBusiness
    Inherits SICAR_WebBase'27440

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTC As System.Web.UI.WebControls.Label
    Protected WithEvents cboTipDocumento1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDoc1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipDocumento2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDoc2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipDocumento3 As System.Web.UI.WebControls.DropDownList
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
    Protected WithEvents txtNumeroDocumentos As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtValorDeuda As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hdnMensaje As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents DGReporte As System.Web.UI.WebControls.DataGrid
    Protected WithEvents NombreCliente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents MontoTotalPagado As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents strTrama As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents strRecibos As System.Web.UI.HtmlControls.HtmlInputHidden

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
    Protected WithEvents HidFila1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFila2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFila3 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFila4 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFila5 As System.Web.UI.HtmlControls.HtmlInputHidden
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
    Protected WithEvents LnkPos1 As System.Web.UI.HtmlControls.HtmlAnchor
    Protected WithEvents icoTranPos1 As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents HidTipPOS1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipPOS2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipPOS3 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidF1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidF2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidF3 As System.Web.UI.HtmlControls.HtmlInputHidden
'PROY-27440 FIN
    Protected WithEvents pnlPagos As System.Web.UI.WebControls.Panel
    Protected WithEvents cmdConsultaNC As System.Web.UI.WebControls.Button
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

    Dim blnError As Boolean
    Dim strArrTraces As String = ""

    'PROY-27440 INI
    Public objFileLog As New SICAR_Log
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

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_values_pos : " & "HidFila1 : " & HidFila1.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_values_pos : " & "HidFila2 : " & HidFila2.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_values_pos : " & "HidFila3 : " & HidFila3.Value)

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
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : Validacion Integracion FIN")

        Me.HidCodOpera.Value = ClsKeyPOS.strCodOpeVE & "|" & _
        ClsKeyPOS.strCodOpeREC & "|" & ClsKeyPOS.strCodOpeAN

        Me.HidDesOpera.Value = ClsKeyPOS.strDesOpeVE & "|" & _
        ClsKeyPOS.strDesOpeREC & "|" & ClsKeyPOS.strDesOpeAN
        Me.HidTipoOpera.Value = ClsKeyPOS.strOpeFina & "|" & ClsKeyPOS.strNoFina 'OPE FI(90)
        Me.HidTipoTarjeta.Value = ClsKeyPOS.strCodTarjetaVS & "|" & ClsKeyPOS.strCodTarjetaMC

        Me.HidTipoPago.Value = ClsKeyPOS.strTipPagoRCC '05 RECAUDACION CLIENTES COORPORATIVOS

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
            ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoVisa)

            Dim arrIPDesc(2) As String
            arrIPDesc(0) = strIpClient

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
        Dim strPedidoLog As String = "Pedido: [" & Funciones.CheckStr(Request("strRUC")) & "] "

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Inicio : ")

        Dim dsPedidoPOS As DataSet
        Dim objPedidoPOS As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim strNroPedido As String = Funciones.CheckStr(Request("strRUC"))
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
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")

        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim dsData As New DataSet
            Dim cteContador As Double
            Dim i As Integer
            Dim objRecbusiness As New COM_SIC_RecBusiness.clsRecBusiness

            btnGrabar.Attributes.Add("onClick", "f_validaCajaCerrada();f_Grabar()") 'INICIATIVA-318
            txtMonto1.Attributes.Add("onBlur", "f_Recalcular(this)")
            txtMonto2.Attributes.Add("onBlur", "f_Recalcular(this)")
            txtMonto3.Attributes.Add("onBlur", "f_Recalcular(this)")
            'nhuaringa 18/04/2012
            txtMonto1.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            txtMonto2.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            txtMonto3.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            txtRecibidoPen.Attributes.Add("onBlur", "CalculoVuelto()")
            txtRecibidoUsd.Attributes.Add("onBlur", "CalculoVuelto()")

            Act_Desact_Seleccion()'INICIATIVA-318

            If Len(Trim(Session("strMensaje"))) > 0 Then
                Response.Write("<script language=javascript>alert('" & Session("strMensaje") & "')</script>")
                Session("strMensaje") = ""
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



            If Not Page.IsPostBack Then

                Me.load_data_param_pos()   'PROY-27440 

                dsData = objRecbusiness.FP_DeudaXRucDNI(CStr(Request("strRUC")))
                DGReporte.DataSource = dsData.Tables(0)
                DGReporte.DataBind()
                LlenaCombos()
                LeeDatosValidar()
                'For i = 0 To DGReporte.Items.Count - 1
                '    cteContador = cteContador + DGReporte.Items(i).Cells(6).Text
                'Next
                txtIdentificadorCliente.Value = Request("strRUC")
                txtNumeroDocumentos.Value = DGReporte.Items.Count
                lblTC.Text = ObtenerTipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"))
                Me.validar_pedido_pos() 'PROY-31949
            Else
                load_values_pos()  'PROY-27440  
            End If

           
        End If
    End Sub

    Private Function ObtenerTipoCambio(ByVal strFecha As String) As String
        'Dim obPagos As New SAP_SIC_Pagos.clsPagos
        'Return obPagos.Get_TipoCambio(strFecha).ToString("N2")
        Return (New COM_SIC_OffLine.clsOffline).Obtener_TipoCambio(strFecha).ToString("N3") 'aotane 05.08.2013
    End Function

    Private Sub LlenaCombos()
        Dim objPagos As New SAP_SIC_Pagos.clsPagos
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim dtVias As DataTable

        'Dim dsFormaPago As DataSet = objPagos.Get_ConsultaViasPago(Session("ALMACEN"))

        '' TODO: CAMBIADO POR JYMMY TORRES
        Dim dsFormaPago As DataSet = objOffline.Obtener_ConsultaViasPago(Session("ALMACEN"))
        '' CAMBIADO HASTA AQUI

        dtVias = New DataTable
        dtVias = VerificarVias(dsFormaPago)

        cboLoad(dtVias, cboTipDocumento1)
        cboLoad(dtVias, cboTipDocumento2)
        cboLoad(dtVias, cboTipDocumento3)

        cboTipDocumento1.Items.Insert(0, "") 
        cboTipDocumento2.Items.Insert(0, "")
        cboTipDocumento3.Items.Insert(0, "")

        Dim dvAux As New DataView
        Dim ExitFormaPago As Boolean = False
        Dim iItems As Integer = 0
        Dim valorCboFormasPagoPorDefecto As String = CStr(ConfigurationSettings.AppSettings("cboForPagoPorDefecto"))
        dvAux.Table = dtVias
        iItems = dvAux.Count

        For i As Int32 = 0 To iItems - 1
            If valorCboFormasPagoPorDefecto = CStr(dvAux.Item(i).Item(0)) Then
                ExitFormaPago = True
            End If
        Next

        cboTipDocumento1.SelectedValue = ""

        If ExitFormaPago Then
            'INICIATIVA-318 INI
            Try
            cboTipDocumento1.SelectedValue = valorCboFormasPagoPorDefecto
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
                '    Response.Write("<script language=javascript>window.open('../Pagos/frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")
                'End If
                'FIN-PROY-140773- SICAR 
            Catch ex As Exception
                cboTipDocumento1.SelectedIndex = 0
            End Try
            'INICIATIVA-318 FIN
        End If
     
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
        'CAMBIADO POR FFS INICIO

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim intSAP = objOffline.Get_ConsultaSAP

        '***************LEE TARJETAS CREDITO
        'Dim objSap As New SAP_SIC_Pagos.clsPagos

        Dim dsTmp As DataSet
        'FFS
        'Trabajar en Modo desconectado de ZAP de Tarjetas de Credito

        'If intSAP = 1 Then
        '    Dim objSap As Object
        '    objSap = New SAP_SIC_Pagos.clsPagos
        '    dsTmp = objSap.Get_Tarjeta_Credito()
        '    objSap = Nothing
        'Else
        Dim objSap As New COM_SIC_OffLine.clsOffline
        dsTmp = objSap.Obtener_Tarjeta_Credito()
        objSap = Nothing
        'End If
        'CAMBIADO POR FFS FIN

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
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim objCajas As New COM_SIC_Cajas.clsCajas

        Dim cteCODIGO_RUTALOG As String = ConfigurationSettings.AppSettings("cteCODIGO_RUTALOG")
        Dim cteCODIGO_DETALLELOG As String = ConfigurationSettings.AppSettings("cteCODIGO_DETALLELOG")
        Dim cteCODIGO_CANAL As String = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")
        Dim i As Integer
        Dim j As Integer
        'nhuaringa  23/04/2012
        'Dim chkSelect As New CheckBox
        'Dim txtMontoPag As New TextBox
        Dim strRecibosPagar As String
        Dim strFechaHoraTransac As String
        Dim pstrNumeroOperacionAcreedor As String
        Dim pstrNumeroOperacionCobranza As String
        Dim strTramaRecibos As String


        ''' VERIFICACION DE CUADRE DE CAJA 05.02.2014 TS-JTN
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
        If (objOffline.CuadreCajeroRealizado(Session("ALMACEN"), codUsuario)) Then
            Response.Write("<script>alert('Error: No puede realizar esta operacion, ya realizo cuadre de caja')</script>")
            Exit Sub
        End If
        ''' FIN VERIFICACION DE CUADRE DE CAJA


        Dim strTramaAnul As String

        'Correccion para enviar numero y no recibo
        Dim strTipIden As String
        Dim strDocIden As String
        'fin Correccion para enviar numero y no recibo

        'nhuaringa 19/04/2012
        Dim SumaMonto As Double = 0.0
        Dim resto As Double
        Dim MontoPago As Double
        Dim fin As Boolean = False

        'nhuaringa
        If txtMonto1.Text <> String.Empty Then
            'Me.txtMonto1.Text = 0.0
            SumaMonto = SumaMonto + CDbl(Me.txtMonto1.Text.ToString.Trim)
        End If
        If txtMonto2.Text <> String.Empty Then
            'Me.txtMonto2.Text = 0.0
            SumaMonto = SumaMonto + CDbl(Me.txtMonto2.Text.ToString.Trim)
        End If
        If txtMonto3.Text <> String.Empty Then
            'Me.txtMonto3.Text = 0.0
            SumaMonto = SumaMonto + CDbl(Me.txtMonto3.Text.ToString.Trim)
        End If

        'SumaMonto = CDbl(Me.txtMonto1.Text.Trim) + CDbl(Me.txtMonto2.Text.Trim) + CDbl(Me.txtMonto3.Text.Trim)
        resto = SumaMonto

        blnError = False
        Dim dblMontoDeuda As Double
        Dim constActSelecManual As String = ConfigurationSettings.AppSettings("constActSelecManual") 'INICIATIVA-318
        For i = 0 To DGReporte.Items.Count - 1
            'INICIATIVA-318 INI
            Dim chk As New CheckBox
            chk = DGReporte.Items(i).FindControl("chkItem")
            If chk.Checked Or constActSelecManual = "0" Then
            'INICIATIVA-318 FIN
                If resto > FormatNumber(CDbl(DGReporte.Items(i).Cells(7).Text), 2) Then 'INICIATIVA-318
                If i < DGReporte.Items.Count - 1 Then
                        MontoPago = CDbl(DGReporte.Items(i).Cells(7).Text)'INICIATIVA-318
                    resto = resto - MontoPago
                    resto = FormatNumber(resto, 2)
                Else
                    MontoPago = resto
                    fin = True
                End If
            Else
                resto = FormatNumber(resto, 2)
                MontoPago = resto
                fin = True
            End If

            'nhuaringa 19/04/2012
            'chkSelect = DGReporte.Items(i).FindControl("chkSelect")
            'txtMontoPag = DGReporte.Items(i).FindControl("txtMontoPag")
            'If Not IsNothing(chkSelect) Then

            'If chkSelect.Checked Then

                If Trim(DGReporte.Items(i).Cells(3).Text) <> "" And Trim(DGReporte.Items(i).Cells(3).Text) <> "&nbsp;" Then 'INICIATIVA-318
                strTipIden = "01"
                    strDocIden = Trim(DGReporte.Items(i).Cells(3).Text) 'INICIATIVA-318
            Else
                strTipIden = "02"
                    strDocIden = Trim(DGReporte.Items(i).Cells(4).Text) 'INICIATIVA-318
            End If
                dblMontoDeuda += Funciones.CheckDbl(DGReporte.Items(i).Cells(7).Text) 'INICIATIVA-318

		'INICIATIVA-318 INI
                strRecibosPagar = "REC;Recibo postpago;604;REC;" & DGReporte.Items(i).Cells(4).Text & ";" & _
                      Right(DGReporte.Items(i).Cells(5).Text, 4) & Mid(DGReporte.Items(i).Cells(5).Text, 4, 2) & _
                      Left(DGReporte.Items(i).Cells(5).Text, 2) & ";" & Right(DGReporte.Items(i).Cells(6).Text, 4) & _
                      Mid(DGReporte.Items(i).Cells(6).Text, 4, 2) & Left(DGReporte.Items(i).Cells(6).Text, 2) & ";" & _
                      DGReporte.Items(i).Cells(7).Text
                'INICIATIVA-318 FIN
            'CStr(CDbl(DGReporte.Items(i).Cells(6).Text) * 100).PadLeft(11, "0")
            'Response.Write(ObtenerTramaFormasDePagoSAP(intChecked)) : Response.End()

            PagarDocumentos(cteCODIGO_RUTALOG, cteCODIGO_DETALLELOG, Session("ALMACEN"), cteCODIGO_CANAL, _
                            Session("ALMACEN"), Session("ALMACEN"), Session("USUARIO"), strTipIden, strDocIden, _
                            ObtenerTramaFormasDePagoSAP(CDbl(MontoPago)), CDbl(MontoPago), strRecibosPagar, "00000", _
                            strFechaHoraTransac, pstrNumeroOperacionCobranza, pstrNumeroOperacionAcreedor)

            If Len(Trim(strFechaHoraTransac)) > 0 Then
            'INICIATIVA-318 INI
                    strTramaAnul = strTramaAnul & DGReporte.Items(i).Cells(7).Text & ";604;REC;" & pstrNumeroOperacionCobranza & ";" & pstrNumeroOperacionAcreedor & ";REC;" & DGReporte.Items(i).Cells(4).Text & ";" & strArrTraces & _
                ";" & strFechaHoraTransac & "|"

                    strTramaRecibos = strTramaRecibos & DGReporte.Items(i).Cells(4).Text & ";" & "Recibo postpago" & ";" & DGReporte.Items(i).Cells(5).Text & "|"
            'INICIATIVA-318 FIN
            End If

            'Format(Now.Month, "00") & Format(Now.Day, "00") & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Right(Format(Now.Year, "0000"), 2) & "|"
            'End If

            'End If
            If fin = True Then
                Exit For
            End If
            End If 'INICIATIVA-318
        Next
        If Len(Trim(strTramaAnul)) > 0 Then
            strTramaAnul = Left(strTramaAnul, Len(strTramaAnul) - 1)
        End If
        If Len(strTramaRecibos) > 0 Then
            strTramaRecibos = Left(strTramaRecibos, Len(strTramaRecibos) - 1)
        End If

        ' Response.Write(strTramaAnul) : Response.End()
        Dim dblMontoPagado As Double
        If Not blnError Then
            If cboTipDocumento1.SelectedValue = "ZEFE" Then
                objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto1.Text))
                dblMontoPagado += Funciones.CheckDbl(txtMonto1.Text)
            End If
            If cboTipDocumento2.SelectedValue = "ZEFE" Then
                objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto2.Text))
                dblMontoPagado += Funciones.CheckDbl(txtMonto2.Text)
            End If
            If cboTipDocumento3.SelectedValue = "ZEFE" Then
                objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto3.Text))
                dblMontoPagado += Funciones.CheckDbl(txtMonto3.Text)
            End If
            Session("strMensaje") = "Proceso realizado con éxito"
            NombreCliente.Value = DGReporte.Items(0).Cells(1).Text
            MontoTotalPagado.Value = Format(CDbl(IIf(Len(txtMonto1.Text) > 0, txtMonto1.Text, "0")) + CDbl(IIf(Len(txtMonto2.Text) > 0, txtMonto2.Text, "0")) + CDbl(IIf(Len(txtMonto3.Text) > 0, txtMonto3.Text, "0")), "########0.00")
            strTrama.Value = cboTipDocumento1.SelectedItem.Text & ";" & txtDoc1.Text & "|" & cboTipDocumento2.SelectedItem.Text & ";" & txtDoc2.Text & "|" & cboTipDocumento3.SelectedItem.Text & ";" & txtDoc3.Text
            strRecibos.Value = strTramaRecibos

            Dim strMontoFavor As String = ""
            strMontoFavor = Format(Math.Round(dblMontoPagado - dblMontoDeuda, 2), "########0.00")

            'nhuaringa 07052012
            Response.Redirect("docRecBusiness.aspx?strRUC=" & Request("strRUC") & "&NombreCliente=" & NombreCliente.Value & "-" & txtIdentificadorCliente.Value & "&MontoTotalPagado=" & MontoTotalPagado.Value & "&strTrama=" & strTrama.Value & "&strRecibos=" & strRecibos.Value & "&strMontoFavor=" & strMontoFavor)
            'Response.Redirect("docRecBusiness.aspx?strRUC=" & Request("strRUC") & "&NombreCliente=" & NombreCliente.Value & "-" & txtIdentificadorCliente.Value & "&MontoTotalPagado=" & SumaMonto & "&strTrama=" & strTrama.Value & "&strRecibos=" & strRecibos.Value & "&strMontoFavor=" & strMontoFavor)
        Else
            ' reversar toda la operacion
            Dim arrTrama() As String
            Dim arrLinTrama() As String
            Response.Write("<SCRIPT> alert('No todos los recibos se procesaron correctamente. Verificar el pool de recaudaciones procesadas');</SCRIPT>")
            If Len(Trim(strTramaAnul)) > 0 Then
                arrTrama = strTramaAnul.Split("|")
                For i = 0 To arrTrama.Length - 1
                    arrLinTrama = arrTrama(i).Split(";")
                    AnularPagos("02", arrLinTrama(6), arrTrama(i))
                Next
            End If
        End If
    End Sub

    Private Function ObtenerTramaFormasDePagoSAP(ByVal dblMontoRec As Double) As String
        Dim strResp As String = ""

        Dim dblTotal As Double = 0
        Dim dblPor1 As Double = 0
        Dim dblPor2 As Double = 0
        Dim dblPor3 As Double = 0

        'Sacando el total
        If IsNumeric(Me.txtMonto1.Text) AndAlso Decimal.Parse(Me.txtMonto1.Text) > 0 Then
            dblTotal += CDbl(Me.txtMonto1.Text)
        End If
        If IsNumeric(Me.txtMonto2.Text) AndAlso Decimal.Parse(Me.txtMonto2.Text) > 0 Then
            dblTotal += CDbl(Me.txtMonto2.Text)
        End If
        If IsNumeric(Me.txtMonto3.Text) AndAlso Decimal.Parse(Me.txtMonto3.Text) > 0 Then
            dblTotal += CDbl(Me.txtMonto3.Text)
        End If

        'Calculando el porcentaje por via de pago
        If IsNumeric(Me.txtMonto1.Text) AndAlso Decimal.Parse(Me.txtMonto1.Text) > 0 Then
            dblPor1 = Math.Round(CDbl(Me.txtMonto1.Text) * 100 / dblTotal, 2)
        End If

        If IsNumeric(Me.txtMonto2.Text) AndAlso Decimal.Parse(Me.txtMonto2.Text) > 0 Then
            dblPor2 = Math.Round(CDbl(Me.txtMonto2.Text) * 100 / dblTotal, 2)
        End If

        If IsNumeric(Me.txtMonto3.Text) AndAlso Decimal.Parse(Me.txtMonto3.Text) > 0 Then
            dblPor3 = Math.Round(CDbl(Me.txtMonto3.Text) * 100 / dblTotal, 2)
        End If

        'Armando la trama con el porcentaje recibido.
        If IsNumeric(Me.txtMonto1.Text) AndAlso Decimal.Parse(Me.txtMonto1.Text) > 0 Then
            strResp = Me.cboTipDocumento1.SelectedValue.Trim() + ";" + CStr(Math.Round(dblMontoRec * dblPor1 / 100, 2)) + ";" + Me.txtDoc1.Text.Trim() + ";;" + Me.cboTipDocumento1.SelectedItem.Text.Trim
        End If
        If IsNumeric(Me.txtMonto2.Text) AndAlso Decimal.Parse(Me.txtMonto2.Text) > 0 Then
            strResp += "|" + Me.cboTipDocumento2.SelectedValue.Trim() + ";" + CStr(Math.Round(dblMontoRec * dblPor2 / 100, 2)) + ";" + Me.txtDoc2.Text.Trim() + ";;" + Me.cboTipDocumento2.SelectedItem.Text.Trim
        End If
        If IsNumeric(Me.txtMonto3.Text) AndAlso Decimal.Parse(Me.txtMonto3.Text) > 0 Then
            strResp += "|" + Me.cboTipDocumento3.SelectedValue.Trim() + ";" + CStr(Math.Round(dblMontoRec * dblPor3 / 100, 2)) + ";" + Me.txtDoc3.Text.Trim() + ";;" + Me.cboTipDocumento3.SelectedItem.Text.Trim
        End If


        'If IsNumeric(Me.txtMonto1.Text) AndAlso Decimal.Parse(Me.txtMonto1.Text) > 0 Then
        '    strResp = Me.cboTipDocumento1.SelectedValue.Trim() + ";" + Me.txtMonto1.Text.Trim() + ";" + Me.txtDoc1.Text.Trim() + ";;"
        'End If
        'If IsNumeric(Me.txtMonto2.Text) AndAlso Decimal.Parse(Me.txtMonto2.Text) > 0 Then
        '    strResp += "|" + Me.cboTipDocumento2.SelectedValue.Trim() + ";" + Me.txtMonto2.Text.Trim() + ";" + Me.txtDoc2.Text.Trim() + ";;"
        'End If
        'If IsNumeric(Me.txtMonto3.Text) AndAlso Decimal.Parse(Me.txtMonto3.Text) > 0 Then
        '    strResp += "|" + Me.cboTipDocumento3.SelectedValue.Trim() + ";" + Me.txtMonto3.Text.Trim() + ";" + Me.txtDoc3.Text.Trim() + ";;"
        'End If
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
                            ByVal strNumeroTrace, ByRef strFechaHoraTransac, _
                            ByRef pstrNumeroOperacionCobranza, ByRef pstrNumeroOperacionAcreedor)

        Dim obPagos As New COM_SIC_Recaudacion.clsPagos

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
        Dim Detalle(7, 3) As String

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        ' fin de variables de auditoria

        'AUDITORIA
        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcReBS")
        wParam5 = 1
        wParam6 = "Recaudacion de Clientes Business"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtRBus")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        Detalle(1, 1) = "RUC"
        Detalle(1, 2) = CStr(Request("strRUC"))
        Detalle(1, 3) = "RUC"

        Detalle(2, 1) = "OfVta"
        Detalle(2, 2) = strPuntoDeVenta
        Detalle(2, 3) = "Oficina de Venta"

        Detalle(3, 1) = "Canal"
        Detalle(3, 2) = strCanal
        Detalle(3, 3) = "Canal"

        Detalle(4, 1) = "Usuario"
        Detalle(4, 2) = strCodigoCajero
        Detalle(4, 3) = "Usuario"

        Detalle(5, 1) = "TipIden"
        Detalle(5, 2) = strTipoIdentificadorDeudor
        Detalle(5, 3) = "Tipo de Identificador"

        Detalle(6, 1) = "Ident"
        Detalle(6, 2) = strNumeroIdentificadorDeudor
        Detalle(6, 3) = "identificador"

        Detalle(7, 1) = "Recibos"
        Detalle(7, 2) = strRecibosPagar
        Detalle(7, 3) = "Trama de Recibos"

        Detalle(7, 1) = "Trace"
        Detalle(7, 2) = strNumeroTrace
        Detalle(7, 3) = "Trace"


        'FIN DE AUDITORIA

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

        Dim strRespuesta As String = obPagos.Pagar( _
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
                            codigoProcesador, _
                             , , , strFechaHoraTransac, pstrNumeroOperacionCobranza, pstrNumeroOperacionAcreedor)
        'decVuelto, , , , strFechaHoraTransac, pstrNumeroOperacionCobranza, pstrNumeroOperacionAcreedor)

        Dim arrMensaje() As String = strRespuesta.Split("@")
        strArrTraces = obPagos.gstrTracePago
        If ExisteError(arrMensaje) Then

            wParam5 = 0
            wParam6 = "Error en Recaudacion de Clientes Business."

            If arrMensaje.Length > 1 Then
                If InStr(1, arrMensaje(1), ";") > 0 Then
                    Dim arrMensajeError() As String
                    arrMensajeError = arrMensaje(1).Split(";")
                    If arrMensajeError.Length >= 5 Then
                        Response.Write("<script> alert('" + arrMensajeError(4) + "');  </script>")
                        wParam6 = wParam6 & arrMensajeError(4)
                    Else
                        wParam6 = wParam6 & arrMensajeError(1)
                        Response.Write("<script> alert('" + arrMensajeError(1) + "');  </script>")
                    End If
                Else
                    wParam6 = wParam6 & arrMensaje(1)
                    Response.Write("<script> alert('" + arrMensaje(1) + "');  </script>")
                End If
            Else
                wParam6 = wParam6 & "Verifique los datos ingresados. Por favor vuelva a intentar."
                Response.Write("<script> alert('Verifique los datos ingresados. Por favor vuelva a intentar.');  </script>")
            End If
            blnError = True
        Else


            'If cboTipDocumento1.SelectedValue = "ZEFE" Then
            '    objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto1.Text))
            'End If
            'If cboTipDocumento2.SelectedValue = "ZEFE" Then
            '    objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto2.Text))
            'End If
            'If cboTipDocumento3.SelectedValue = "ZEFE" Then
            '    objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto3.Text))
            'End If

            'Mandar a la página de consulta a SAP.
            arrMensaje = strRespuesta.Split("@")
            Dim arrCabecera() As String = arrMensaje(1).Split(";")
            Dim strNumeroDeuda, strAccion

            'PROY-27440 INI
            Dim strNumeroRec As String = Funciones.CheckStr(arrCabecera(0))
            Dim strIdentifyLog As String = strNumeroIdentificadorDeudor

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & "- Inicio Actualizar cabezera " & "	strNumeroDeuda: " & strNumeroRec)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & "- Inicio Actualizar cabezera " & " HidIdCabez: " & Me.HidIdCabez.Value)
            If (strNumeroRec <> "" And Funciones.CheckStr(Me.HidIdCabez.Value) <> "") Then
                Me.actualizar_codigo_recuadacion(strNumeroRec, Me.HidIdCabez.Value)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & "- Fin Actualizar cabezera " & "	strNumeroDeuda: " & strNumeroRec)
            End If
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & "- " & "	strNumeroDeuda: " & strNumeroRec)
            'PROY-27440 FIN
            'strNumeroDeuda = arrCabecera(cteNUMERODEUDA)
            'strAccion = cteACCION_CONFIRMACION

            'Response.Redirect("conDocumentos.aspx?act=" & strAccion & "&num=" & strNumeroDeuda)
        End If
        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)

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

    Private Sub AnularPagos(ByVal strTipoIdentificacionDeudor As String, ByVal strNumeroIdentificadorDeudor As String, ByVal strTrama As String)
        ' Try
        Dim obAnul As New COM_SIC_Recaudacion.clsAnulaciones

        Dim cteCODIGO_RUTALOG As String = ConfigurationSettings.AppSettings("cteCODIGO_RUTALOG")
        Dim cteCODIGO_DETALLELOG As String = ConfigurationSettings.AppSettings("cteCODIGO_DETALLELOG")
        Dim cteCODIGO_CANAL As String = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")

        'Dim strResult As String = obAnul.AnularPago(ConfigurationSettings.AppSettings("CodAplicacion"), Session("CANAL"), cteCODIGO_RUTALOG, _
        '                cteCODIGO_DETALLELOG, _
        '                Session("ALMACEN"), _
        '                cteCODIGO_CANAL, _
        '                Session("ALMACEN"), _
        '                Session("ALMACEN"), _
        '                Session("USUARIO"), _
        '                strTransac, , , , False)

        Dim strResult As String = obAnul.Anular(cteCODIGO_RUTALOG, cteCODIGO_DETALLELOG, Session("ALMACEN"), cteCODIGO_CANAL, _
        Session("ALMACEN"), Session("ALMACEN"), Session("USUARIO"), strTipoIdentificacionDeudor, strNumeroIdentificadorDeudor, strTrama)

        If Len(strResult) > 0 Then
            Dim arrMensaje() As String = strResult.Split("@")
            If ExisteError(arrMensaje) Then
                Response.Write("<SCRIPT> alert('" + arrMensaje(1) + "');</SCRIPT>")
            Else
                Response.Write("<SCRIPT> alert('Se procesó exitosamente la anulación.'); document.location='grdDocumentos.aspx';</SCRIPT>")
            End If
        End If
        'Catch ex As Exception
        'Response.Write("<SCRIPT> alert('" + ex.Message + "');</SCRIPT>")
        ' End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("RecBusiness.aspx")
    End Sub
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

    'PROY-27440 INI
    Private Sub actualizar_codigo_recuadacion(ByVal strIdRecaudacion As String, ByVal strCodCabez As String)

        Dim strCodRpt As String = ""
        Dim strMsgRpt As String = ""
        Dim strPedidoLog As String = "strIdRecaudacion: [" & Funciones.CheckStr(strIdRecaudacion) & "] "
        Dim strTipoPago As String

        Try
            strTipoPago = Me.HidTipoPago.Value
            strPedidoLog = Devolver_TipoPago_POS(strTipoPago) & ": [" & Funciones.CheckStr(strCodCabez) & "] "
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "Inicio")

            Dim objEntity As New COM_SIC_Activaciones.BeEnvioTransacPOS
            objEntity.TransId = strCodCabez
            objEntity.FlagPago = "2"
            objEntity.idCabecera = strCodCabez
            objEntity.numPedido = strIdRecaudacion
            objEntity.IdRefAnu = ""

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "strCodCabez: " & strCodCabez)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "strIdRecaudacion: " & strIdRecaudacion)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "FlagPago: " & objEntity.FlagPago)

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

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "ActualizarTransaction Ini")
            Dim objSicarDB As New COM_SIC_Activaciones.BWEnvioTransacPOS

            If strCodRpt <> "0" Then

                Dim objTransascPos As New COM_SIC_Activaciones.clsTransaccionPOS

                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "ActualizarTransaction - Error en el Servicio " & "strCodRpt : " & strCodRpt)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "ActualizarTransaction - Error en el Servicio " & "strMsgRpt : " & strMsgRpt)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - Inicio")

                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - SP: PKG_SISCAJ_POS.SICASU_TRANSPOS")
                objTransascPos.UpdateCabTransaccPOS(objEntity, strCodRpt, strMsgRpt)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - strCodRpt:" & strCodRpt)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - strMsgRpt:" & strMsgRpt)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - Fin")

            End If

            objSicarDB.ActualizarTransaction(objEntity, strCodRpt, strMsgRpt)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "ActualizarTransaction Fin")

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "strMsgRpt: " & strMsgRpt)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "strCodRpt: " & strCodRpt)

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "Fin")
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: actualizar_codigo_recaudacion)"
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "Error: " & ex.Message & MaptPath)
            'FIN PROY-140126

        End Try
    End Sub
    'PROY-27440 FIN

    'INICIATIVA-318 INI
    Private Sub DGReporte_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DGReporte.ItemDataBound
        Dim chkBoxAll As CheckBox
        chkBoxAll = e.Item.FindControl("chkAll")
        If Not IsNothing(chkBoxAll) Then
            chkBoxAll.Attributes.Add("onClick", "f_CheckAll();")
        End If

        Dim chkBoxItem As CheckBox
        chkBoxItem = e.Item.FindControl("chkItem")
        If Not IsNothing(chkBoxItem) Then
            chkBoxItem.Attributes.Add("onClick", "EstablecerMonto_Pagar2('txtMonto1', 'cboTipDocumento1');") 'INICIATIVA - 529
        End If
    End Sub

    Private Sub Act_Desact_Seleccion()
        Dim constActSelecManual As String = ConfigurationSettings.AppSettings("constActSelecManual")
        If constActSelecManual = "1" Then
            txtMonto1.Attributes.Add("onkeyup", "javascript:MontoEfectivoRestriccion2(f_CalculoDeuda_TablaMiles2('DGReporte', '7', '1'), 'txtMonto1', 'txtMonto1', 'txtMonto2', 'txtMonto3', 'cboTipDocumento1');RedondearEfectivo('txtMonto1','cboTipDocumento1');")
            txtMonto2.Attributes.Add("onkeyup", "javascript:MontoEfectivoRestriccion2(f_CalculoDeuda_TablaMiles2('DGReporte', '7', '1'), 'txtMonto2', 'txtMonto1', 'txtMonto2', 'txtMonto3', 'cboTipDocumento2');RedondearEfectivo('txtMonto2','cboTipDocumento2');")
            txtMonto3.Attributes.Add("onkeyup", "javascript:MontoEfectivoRestriccion2(f_CalculoDeuda_TablaMiles2('DGReporte', '7', '1'), 'txtMonto3', 'txtMonto1', 'txtMonto2', 'txtMonto3', 'cboTipDocumento3');RedondearEfectivo('txtMonto3','cboTipDocumento3');")
            cboTipDocumento1.Attributes.Add("onchange", "javascript:MontoEfectivoRestriccion2(f_CalculoDeuda_TablaMiles2('DGReporte', '7', '1'), 'txtMonto1', 'txtMonto1', 'txtMonto2', 'txtMonto3', 'cboTipDocumento1');RedondearEfectivo('txtMonto1','cboTipDocumento1');f_bloqueo_fila(this,1);")
            cboTipDocumento2.Attributes.Add("onchange", "javascript:MontoEfectivoRestriccion2(f_CalculoDeuda_TablaMiles2('DGReporte', '7', '1'), 'txtMonto2', 'txtMonto1', 'txtMonto2', 'txtMonto3', 'cboTipDocumento2');RedondearEfectivo('txtMonto2','cboTipDocumento2');f_bloqueo_fila(this,2);")
            cboTipDocumento3.Attributes.Add("onchange", "javascript:MontoEfectivoRestriccion2(f_CalculoDeuda_TablaMiles2('DGReporte', '7', '1'), 'txtMonto3', 'txtMonto1', 'txtMonto2', 'txtMonto3', 'cboTipDocumento3');RedondearEfectivo('txtMonto3','cboTipDocumento3');f_bloqueo_fila(this,3);")
        Else
            DGReporte.Columns(0).HeaderStyle.CssClass = "hiddencol"
            DGReporte.Columns(0).ItemStyle.CssClass = "hiddencol"
            txtMonto1.Attributes.Add("onkeyup", "javascript:MontoEfectivoRestriccion(f_CalculoDeuda_TablaMiles('DGReporte', '7', '1'), 'txtMonto1', 'txtMonto1', 'txtMonto2', 'txtMonto3', 'cboTipDocumento1');RedondearEfectivo('txtMonto1','cboTipDocumento1');")
            txtMonto2.Attributes.Add("onkeyup", "javascript:MontoEfectivoRestriccion(f_CalculoDeuda_TablaMiles('DGReporte', '7', '1'), 'txtMonto2', 'txtMonto1', 'txtMonto2', 'txtMonto3', 'cboTipDocumento2');RedondearEfectivo('txtMonto2','cboTipDocumento2');")
            txtMonto3.Attributes.Add("onkeyup", "javascript:MontoEfectivoRestriccion(f_CalculoDeuda_TablaMiles('DGReporte', '7', '1'), 'txtMonto3', 'txtMonto1', 'txtMonto2', 'txtMonto3', 'cboTipDocumento3');RedondearEfectivo('txtMonto3','cboTipDocumento3');")
            cboTipDocumento1.Attributes.Add("onchange", "javascript:MontoEfectivoRestriccion(f_CalculoDeuda_TablaMiles('DGReporte', '7', '1'), 'txtMonto1', 'txtMonto1', 'txtMonto2', 'txtMonto3', 'cboTipDocumento1');RedondearEfectivo('txtMonto1','cboTipDocumento1');f_bloqueo_fila(this,1);")
            cboTipDocumento2.Attributes.Add("onchange", "javascript:MontoEfectivoRestriccion(f_CalculoDeuda_TablaMiles('DGReporte', '7', '1'), 'txtMonto2', 'txtMonto1', 'txtMonto2', 'txtMonto3', 'cboTipDocumento2');RedondearEfectivo('txtMonto2','cboTipDocumento2');f_bloqueo_fila(this,2);")
            cboTipDocumento3.Attributes.Add("onchange", "javascript:MontoEfectivoRestriccion(f_CalculoDeuda_TablaMiles('DGReporte', '7', '1'), 'txtMonto3', 'txtMonto1', 'txtMonto2', 'txtMonto3', 'cboTipDocumento3');RedondearEfectivo('txtMonto3','cboTipDocumento3');f_bloqueo_fila(this,3);")
        End If
    End Sub
    'INICIATIVA-318 FIN


    Private Function ConsultaParametrosFormaPagoPerfil(ByVal strIdenLog As String)

        Try

            Dim objpvuDB As New COM_SIC_Activaciones.clsConsultaPvu
            Dim oParamteros As New COM_SIC_Activaciones.BEParametros
            Dim strCodGrupo As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("key_ParanGrupoFormaPagoPerfil"))

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

        'If cboTipDocumento1.SelectedIndex = 0 Then
        '    bolAutorizado = True
        'Else
        '    bolAutorizado = ObtenerAprobacionFormaPago(cboTipDocumento1.SelectedValue)
        'End If

        'If (bolAutorizado = False) Then
        '    cboTipDocumento1.SelectedIndex = 0
        '    Response.Write("<script language=javascript>alert('Usuario no cuenta con los permisos sufiientes para la forma de Pago');</script>")
        '    Response.Write("<script language=javascript>window.open('../Pagos/frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")
        'End If
        'FIN-PROY-140773- SICAR 
    End Sub

    Private Sub cboTipDocumento2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipDocumento2.SelectedIndexChanged
        'INI-PROY-140773- SICAR 
        'Dim bolAutorizado As Boolean
        'Dim strFormaPago As String
        'Dim strCombo As String

        'strCombo = "cboTipDocumento2"
        'strFormaPago = cboTipDocumento2.SelectedValue

        'If cboTipDocumento2.SelectedIndex = 0 Then
        '    bolAutorizado = True
        'Else
        '    bolAutorizado = ObtenerAprobacionFormaPago(cboTipDocumento2.SelectedValue)
        'End If

        'If (bolAutorizado = False) Then
        '    cboTipDocumento2.SelectedIndex = 0
        '    Response.Write("<script language=javascript>alert('Usuario no cuenta con los permisos sufiientes para la forma de Pago');</script>")
        '    Response.Write("<script language=javascript>window.open('../Pagos/frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")
        'End If
        'FIN-PROY-140773- SICAR 
    End Sub

    Private Sub cboTipDocumento3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipDocumento3.SelectedIndexChanged
        'INI-PROY-140773- SICAR 
        'Dim bolAutorizado As Boolean
        'Dim strFormaPago As String
        'Dim strCombo As String

        'strCombo = "cboTipDocumento3"
        'strFormaPago = cboTipDocumento3.SelectedValue

        'If cboTipDocumento3.SelectedIndex = 0 Then
        '    bolAutorizado = True
        'Else
        '    bolAutorizado = ObtenerAprobacionFormaPago(cboTipDocumento3.SelectedValue)
        'End If

        'If (bolAutorizado = False) Then
        '    cboTipDocumento3.SelectedIndex = 0
        '    Response.Write("<script language=javascript>alert('Usuario no cuenta con los permisos sufiientes para la forma de Pago');</script>")
        '    Response.Write("<script language=javascript>window.open('../Pagos/frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")
        'End If
        'FIN-PROY-140773- SICAR 
    End Sub
End Class