Imports System.Web.Services
'Imports System.Data.OracleClient
'Imports COM_SIC_Cajas
Imports COM_SIC_Adm_Cajas
Imports COM_SIC_Recaudacion
Imports System.Globalization
Imports COM_SIC_Activaciones 'INICIATIVA-318

Public Class RecPagoDocId_Det
    'Inherits System.Web.UI.Page
    Inherits SICAR_WebBase '''System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtCantDoc As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipDocumento1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDoc1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipDocumento2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDoc2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipDocumento3 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDoc3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnLimpiar As System.Web.UI.WebControls.Button
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents txtDNI As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipoServicio As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cmdBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents hdTipoCambio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnPuntoDeVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnBinAdquiriente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodComercio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents intCanal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtNumeroTrace As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnRutaLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDetalleLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnombreComercio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdCodAquirienteReenvia As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtTipoIdentificador As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidRecibos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtIdentificador As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdDeudaPorCuenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents primeroButton As System.Web.UI.WebControls.Button
    Protected WithEvents anteriorButton As System.Web.UI.WebControls.Button
    Protected WithEvents siguienteButton As System.Web.UI.WebControls.Button
    Protected WithEvents ultimoButton As System.Web.UI.WebControls.Button
    Protected WithEvents lblTC As System.Web.UI.WebControls.Label
    Protected WithEvents txtMontoTotalDni As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecibidoPen As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtVuelto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecibidoUsd As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTarjCred As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBIN As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox

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
    Protected WithEvents lblEnvioPos As System.Web.UI.WebControls.Label
    Protected WithEvents HidFila4 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFila5 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoPosVisa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoPosMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoAuditPos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIdCabez As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Hidden2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Hidden3 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidF1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidF2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidF3 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidNomCliente As System.Web.UI.HtmlControls.HtmlInputHidden
    'PROY-27440 FIN

    'Proy-31949 INICIO
    Protected WithEvents hdnMsgMayor As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnMsgMenor As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidNumIntentosPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidNumIntentosAnular As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjErrorNumIntentos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjErrorTimeOut As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjNumIntentosPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFlagCajaCerrada As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsjCajaCerrada As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMedioPagoPermitidas As System.Web.UI.HtmlControls.HtmlInputHidden
    'Proy-31949 FIN
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
    Public Recibos() As String
    Dim strPaginaRetorno As String
    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Private boolData As Boolean = False
    'PROY-27440 INI
    Dim nameFilePos As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivoPos As String = objFileLog.Log_CrearNombreArchivo(nameFilePos)
    'PROY-27440 FIN
    'Public oTable As New DataTable 'Recibira la info de la BD
    'PROY-27440 INI
    Dim arrParametrosFormaPagoPerfil As ArrayList   'INICIATIVA-318
    Dim strIdentifyLogGeneral As String = "" ' INICIATIVA-318
    Dim strCodPerfilFormaPago As String = ""  ' INICIATIVA-318

    'Proy-31949 Inicio
    Private Sub validar_pedido_pos()
        Dim strPedidoLog As String = "Pedido: [" & Funciones.CheckStr(Request.QueryString("pdni")) & "] "

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Inicio : ")

        Dim dsPedidoPOS As DataSet
        Dim objPedidoPOS As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim strNroPedido As String = Funciones.CheckStr(Request.QueryString("pdni"))
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
    'Proy-31949 Fin

    Private Sub load_data_param_pos()
        Dim strPedidoLog As String = "Recaudacion x DNI : [" & Funciones.CheckStr(Request.QueryString("pdni")) & "] "

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

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : Validacion Integracion FIN")

        'Proy-31949 Inicio
        Me.hdnMsgMayor.Value = ClsKeyPOS.strMsjErrorAdelanto
        Me.hdnMsgMenor.Value = ClsKeyPOS.strMsjErrorParcial

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
        Me.HidFlagCajaCerrada.Value = 0
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
        'Proy-31949 Fin

        Me.HidCodOpera.Value = ClsKeyPOS.strCodOpeVE & "|" & _
        ClsKeyPOS.strCodOpeREC & "|" & ClsKeyPOS.strCodOpeAN

        Me.HidDesOpera.Value = ClsKeyPOS.strDesOpeVE & "|" & _
        ClsKeyPOS.strDesOpeREC & "|" & ClsKeyPOS.strDesOpeAN
        Me.HidTipoOpera.Value = ClsKeyPOS.strOpeFina & "|" & ClsKeyPOS.strNoFina 'OPE FI(90)
        Me.HidTipoTarjeta.Value = ClsKeyPOS.strCodTarjetaVS & "|" & ClsKeyPOS.strCodTarjetaMC

        Me.HidTipoPago.Value = ClsKeyPOS.strTipPagoRXDNI 'REC POR DNI

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
                & "|POSV_FLAGPOS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSC_FLG_SICAR")) 'Proy-31949

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

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIp : " & strIpClient)
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
                & "|POSV_FLAGPOS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSC_FLG_SICAR")) 'Proy-31949

                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidDatoPosMC : " & HidDatoPosMC.Value)
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
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "Fin : ")
    End Sub

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
    'PROY-27440 FIN
    Function GetTipoServicio(ByVal param As String) As String
        Dim retorno$
        Select Case param
            Case "01"
                retorno = "M"
            Case "02"
                retorno = "F"
        End Select
        Return retorno
    End Function

    Function GetGrupoHijoCount(ByVal oTable As DataTable, ByVal I_SERVICIO As String, ByVal grupo As String) As Integer
        Dim total% = 0
        Dim fila As DataRow
        For Each fila In oTable.Rows
            Dim miGrupo$ = fila("DOCN_FLAGGRUPO").ToString()
            Dim el_servicio$ = fila("DOCV_TIPO_SERVICIO").ToString().ToUpper()
            If (el_servicio <> "") Then
                If (el_servicio = I_SERVICIO) Then
                    If (miGrupo = grupo) Then
                        total = total + 1
                    End If
                End If
            End If
        Next
        Return total
    End Function

    Public Function Get_Data(ByVal dni$, _
                             ByVal P_COD_REENVIA$, _
                             ByVal P_CANAL$, _
                             ByVal I_SERVICIO$, _
                             ByRef status$, _
                             ByRef message$, _
                             ByRef strDescripcionRptaAux$) As DataTable

        objFileLog.Log_WriteLog(pathFile, strArchivo, "--------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio Function Get_Data")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "--------------------------------------------------")
        'INPUT
        Dim sMensaje As String = "" 'JCPM
        Dim P_ID_TRANSACCION$ = DateTime.Now.ToString("yyyyMMddhhmmss")
        Dim P_COD_APLICACION$ = "SICAR"
        'Dim P_COD_BANCO$ = "520200" 'Dim P_COD_REENVIA$ = hdCodAquirienteReenvia.Value
        Dim P_COD_BANCO$ = ConfigurationSettings.AppSettings("BancoSicar")
        Dim P_COD_MONEDA$ = "604"
        Dim P_COD_TIPO_SERV$ = "REC"
        Dim P_POS_ULT_DOCUM$ = "0"
        Dim P_COD_TIPO_IDENT$ = "04"
        Dim P_COD_TIPO_DOC_ID$ = "1"
        Dim P_NUM_DOCUMENTO_ID$ = "01" + dni
        Dim P_NOMBRE_COMERCIO$ = ""
        Dim P_NUMERO_COMERCIO$ = ""
        Dim P_COD_AGENCIA$ = "" 'Dim P_CANAL$ = intCanal.Value
        Dim P_COD_CIUDAD$ = ""
        Dim P_COD_TERMINAL$ = ""
        Dim P_COD_PLAZA$ = ""
        Dim P_NRO_REFERENCIA$ = ""
        'objFileLog.Log_WriteLog(pathFile, strArchivo, "-----CLSDEUDADNI-----")
        'Dim objCuenta As New clsDeudaDni
        'objFileLog.Log_WriteLog(pathFile, strArchivo, "---------------ClsConsultasOAC---------------")
        Dim objCuentas As New ClsConsultasOAC
        Dim oTable As New DataTable 'Recibira la info de la BD
        ' objFileLog.Log_WriteLog(pathFile, strArchivo, "-----Se llenaron los input-----")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Se Empieza a cargar el DataTable")
        'oTable = objCuenta.Listar_DeudaDNI(P_ID_TRANSACCION, P_COD_APLICACION, P_COD_BANCO, P_COD_REENVIA, P_COD_MONEDA, P_COD_TIPO_SERV, P_POS_ULT_DOCUM, P_COD_TIPO_IDENT, P_COD_TIPO_DOC_ID, _
        '                                   P_NUM_DOCUMENTO_ID, P_NOMBRE_COMERCIO, P_NUMERO_COMERCIO, P_COD_AGENCIA, P_CANAL, P_COD_CIUDAD, P_COD_TERMINAL, P_COD_PLAZA, P_NRO_REFERENCIA).Tables(0)

        oTable = objCuentas.Get_Deuda_DNI(P_ID_TRANSACCION, _
                                     P_COD_APLICACION, _
                                     P_COD_BANCO, _
                                     P_COD_REENVIA, _
                                     P_COD_MONEDA, _
                                     P_COD_TIPO_SERV, _
                                     P_POS_ULT_DOCUM, _
                                     P_COD_TIPO_IDENT, _
                                     P_NUM_DOCUMENTO_ID, _
                                     P_NOMBRE_COMERCIO, _
                                     P_NUMERO_COMERCIO, _
                                     P_COD_AGENCIA, _
                                     P_CANAL, _
                                     P_COD_CIUDAD, _
                                     P_COD_TERMINAL, _
                                     P_COD_PLAZA, _
                                     P_NRO_REFERENCIA, _
                                     status, _
                                     message, _
                                     strDescripcionRptaAux)

        objFileLog.Log_WriteLog(pathFile, strArchivo, "Total Filas DataTable Obtenidos del WS : " & oTable.Rows.Count.ToString())
        'If Not status Is Nothing And Convert.ToInt32(status) = 0 Then
        If (I_SERVICIO <> "" And I_SERVICIO <> "00" And I_SERVICIO <> "Todos") Then 'Si es Movil o Fijo

            objFileLog.Log_WriteLog(pathFile, strArchivo, "----------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Se aplica el filtro de Tipo de Servicio  : " & I_SERVICIO)

            Dim dt As New DataTable
            dt = oTable.Clone()
            Dim fila As DataRow
            For Each fila In oTable.Rows
                Dim el_servicio$ = fila("DOCV_TIPO_SERVICIO").ToString().ToUpper()
                Dim oFila As DataRow = dt.NewRow
                oFila.ItemArray = fila.ItemArray
                If (el_servicio = GetTipoServicio(I_SERVICIO)) Then
                    dt.Rows.Add(oFila)
                Else
                    If (el_servicio = "") Then 'Fila Padre
                        Dim totalHijos% = GetGrupoHijoCount(oTable, GetTipoServicio(I_SERVICIO), fila("DOCN_FLAGGRUPO").ToString())
                        If (totalHijos > 0) Then
                            dt.Rows.Add(oFila)
                        End If
                    End If
                End If
            Next
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Total de Registros Filtrados             : " & IIf(oTable Is Nothing, "0", dt.Rows.Count.ToString()))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "----------------------------------------------------------")

            sMensaje = "RECAUDACIÓN POR DNI. " & ". Datos: Canal=" & intCanal.Value & "|PDV=" & hdnPuntoDeVenta.Value & "|Cajero=" & hdnUsuario.Value & "|Nro. DNI=" & Me.txtDNI.Text & "|Tipo de Servicio=" & Me.cboTipoServicio.SelectedItem.Text
            RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("codTrxConsultaRecaudacion"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, "--------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Function Get_Data")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "--------------------------------------------------")

            Return dt
        End If
        'End If

        sMensaje = "RECAUDACIÓN POR DNI. " & ". Datos: Canal=" & intCanal.Value & "|PDV=" & hdnPuntoDeVenta.Value & "|Cajero=" & hdnUsuario.Value & "|Nro. DNI=" & Me.txtDNI.Text & "|Tipo de Servicio=" & Me.cboTipoServicio.SelectedItem.Text
        RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("codTrxConsultaRecaudacion"))

        objFileLog.Log_WriteLog(pathFile, strArchivo, "--------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Function Get_Data")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "--------------------------------------------------")

        Return oTable
    End Function

    Public Function Get_Lista(ByVal dni, _
                              ByVal P_COD_REENVIA, _
                              ByVal P_CANAL, _
                              ByVal I_SERVICIO) As String
        'Dim objCuenta As New clsDeudaDni
        Dim status$ = ""
        Dim message$ = ""
        Dim strDescripcionRptaAux$ = ""
        Dim oTable As New DataTable 'Recibira la info de la BD
        oTable = Nothing
        If Not Session("DataDni") Is Nothing Then
            oTable = Session("DataDni")
            Session("DataDni") = Nothing
        Else
            oTable = Get_Data(dni, _
                                   P_COD_REENVIA, _
                                   P_CANAL, _
                                   I_SERVICIO, _
                                   status, _
                                   message, _
                                   strDescripcionRptaAux)
        End If

        Dim i%
        Dim cadena$ = ""
        Dim totalFilas% = oTable.Rows.Count
        If (totalFilas > 0) Then 'Correcto
            cadena = "C"
        Else 'Incorrecto
            cadena = "I;" + strDescripcionRptaAux
        End If
        For i = 0 To totalFilas - 1
            Dim detalle% = Integer.Parse(oTable.Rows(i)("DOCN_FLAGDETALLE").ToString())
            Dim grupo% = Integer.Parse(oTable.Rows(i)("DOCN_FLAGGRUPO").ToString())
            Dim oCuenta() As String = oTable.Rows(i)("DOCV_COD_CUENTA").ToString().Split("-")
            Dim customerId$ = oCuenta(0)
            Dim cuenta$ = oCuenta(1)
            'Dim cuenta$ = oTable.Rows(i)("DOCV_COD_CUENTA").ToString()
            Dim razonSocial$ = oTable.Rows(i)("DOCV_RAZONSOCIAL").ToString()
            Dim telfRef$ = oTable.Rows(i)("DOCV_TELFREF").ToString()
            Dim recibo$ = oTable.Rows(i)("DOCV_RECIBO").ToString()
            Dim fecEmision$ = IIf(detalle = 1, oTable.Rows(i)("DOCD_FECHA_EMISION").ToString(), "")
            Dim fecVencimiento$ = IIf(detalle = 1, oTable.Rows(i)("DOCD_FECHA_VENCIMIENTO").ToString(), "")
            Dim moneda$ = oTable.Rows(i)("DOCN_COD_MONEDA").ToString()
            moneda = IIf(moneda <> "", IIf(moneda = "1", "PEN", "USD"), "")
            Dim importe$ = IIf(detalle = 1, oTable.Rows(i)("DOCN_IMPORTE").ToString(), oTable.Rows(i)("DOCN_TOTAL").ToString())
            importe = importe.Replace(",", ".")
            Dim serv_doc$ = oTable.Rows(i)("DOCV_TIPO_SERVICIO_DOC").ToString() 'REC/101
            Dim serv_tipo$ = oTable.Rows(i)("DOCV_TIPO_SERVICIO").ToString() 'M/F
            If (i > 0) Then
                cadena = cadena + "|"
            End If
            If (i = 0) Then
                cadena = cadena + "@" & cuenta + ";" + razonSocial + ";" + telfRef + ";" + recibo + ";" + fecEmision + ";" + fecVencimiento + ";" + moneda + ";" + importe + ";" + grupo.ToString() + ";" + detalle.ToString() + ";" + customerId + ";" + serv_doc + ";" + serv_tipo
                'objFileLog.Log_WriteLog(pathFile, strArchivo, "CADENA DE DATOS " & cadena.ToString())
            ElseIf (i > 0) Then
                cadena = cadena + cuenta + ";" + razonSocial + ";" + telfRef + ";" + recibo + ";" + fecEmision + ";" + fecVencimiento + ";" + moneda + ";" + importe + ";" + grupo.ToString() + ";" + detalle.ToString() + ";" + customerId + ";" + serv_doc + ";" + serv_tipo
                'objFileLog.Log_WriteLog(pathFile, strArchivo, "CADENA DE DATOS " & cadena.ToString())
            End If
        Next
        objFileLog.Log_WriteLog(pathFile, strArchivo, "CADENA DE DATOS PARA EL JS : " & cadena.ToString())
        Return cadena
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
            oAuditoria.RegistrarAuditoria(CodTrx, CodServicio, ipHost, nameHost, ipServer, nameServer, user, "", "0", DesTrx)
        Catch ex As Exception
            Throw New Exception("Error Registrar Auditoria.")
        End Try
    End Sub

    'Proy-31949 - Inicio
    Private Sub ValidarSalirPagoDocID(ByVal strMensaje As String)
        Response.Write("<script> alert('" + strMensaje + "'); window.location='" & strPaginaRetorno & "'; </script>")
    End Sub
    'Proy-31949 - Fin

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Session User is Nothing")
            Exit Sub
        Else

            txtMonto1.Attributes.Add("onChange", "javascript:f_Recalcular(this);")
            txtMonto2.Attributes.Add("onChange", "javascript:f_Recalcular(this);")
            txtMonto3.Attributes.Add("onChange", "javascript:f_Recalcular(this);")
            txtRecibidoPen.Attributes.Add("onChange", "javascript:CalculoVuelto();")
            txtRecibidoUsd.Attributes.Add("onChange", "javascript:CalculoVuelto();")

            btnGrabar.Attributes.Add("onClick", "f_validaCajaCerrada()") 'INICIATIVA-318

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
                'Boton Buscar
                If Not Request.QueryString("pMetodo") Is Nothing Then
                    Select Case (Request.QueryString("pMetodo"))
                        Case "Listar"
                            'input SP
                            Dim pdni$ = Request.QueryString("pdni")
                            Dim phdnBinAdquiriente$ = Request.QueryString("hdnBinAdquiriente")
                            Dim I_SERVICIO$ = Request.QueryString("cboTipoServicio")
                            'Dim phdnombreComercio$ = Request.QueryString("hdnombreComercio")
                            'Dim phdnCodComercio$ = Request.QueryString("hdnCodComercio")
                            Dim pintCanal$ = Request.QueryString("intCanal")
                            Response.Write(Get_Lista(pdni, phdnBinAdquiriente, pintCanal, I_SERVICIO))
                            boolData = True
                            Response.End()
                            Return
                    End Select
                End If
                Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
                Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
                If Not Page.IsPostBack Then
                    Session("DataDni") = Nothing

                    'PROY-27440 INI
                    Me.load_data_param_pos()
                    'PROY-27440 FIN

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "--------------------------------------------------")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio RecPagoDocId_Det.aspx Page_Load")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "--------------------------------------------------")

                    If Not Request.QueryString("pdni") Is Nothing Then
                        Me.txtIdentificador.Value = Request.QueryString("pdni")
                        txtDNI.Text = Request.QueryString("pdni")
                    End If
                    'If InStr(1, Request.ServerVariables("HTTP_REFERER"), "recListaNombreApellido") > 0 Then
                    strPaginaRetorno = Mid(Request.ServerVariables("HTTP_REFERER"), InStrRev(Request.ServerVariables("HTTP_REFERER"), "/") + 1) '"recBusNomAp.aspx"
                    'Else
                    '   strPaginaRetorno = "bsqDocumentos.aspx"
                    'End If
                    LeeDatosValidar()
                    LeeParametros() 'Leer Configuracion
                    'Verificar que exista la Tabla de consulta x DNI
                    Dim oTable As New DataTable
                    Dim status$ = ""
                    Dim message$ = ""
                    Dim strDescripcionRptaAux$
                    oTable = Get_Data(Request.QueryString("pdni"), _
                                      hdnBinAdquiriente.Value, _
                                      intCanal.Value, _
                                      Request.QueryString("pservicio"), _
                                      status, _
                                      message, _
                                      strDescripcionRptaAux)
                    Session("DataDni") = oTable
                    If (oTable.Rows.Count = 0) Then
                        'Proy-31949 - Inicio
                        'Response.Write("<script> alert('" + strDescripcionRptaAux + "'); window.location='" & strPaginaRetorno & "'; </script>")
                        ValidarSalirPagoDocID(strDescripcionRptaAux)
                        'Proy-31949 - Fin
                    Else
                        cboTipoServicio.SelectedValue = Request.QueryString("pservicio")
                    End If
                    Session("PagAnt") = strPaginaRetorno
                    LlenaCombos()

                    'Proy-31949 Inicio
                    Me.validar_pedido_pos()
                    'Proy-31949 Fin

                Else
                    strPaginaRetorno = Session("PagAnt")
                    'PROY-27440 INI
                    Me.load_values_pos()
                    'PROY-27440 FIN
                End If
                boolData = False
            Catch ex As Exception
                If (boolData = False) Then
                    'Proy-31949 - Inicio
                    'Response.Write("<script language=jscript> alert('" + ex.Message + "'); window.location='" & strPaginaRetorno & "';</script>")
                    ValidarSalirPagoDocID(Funciones.CheckStr(ex.Message))
                    'Proy-31949 - Fin
                End If
            End Try
        End If
    End Sub

    Private Sub LeeParametros()
        hdnPuntoDeVenta.Value = Session("ALMACEN")
        intCanal.Value = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")
        hdnUsuario.Value = Session("USUARIO")
        hdnBinAdquiriente.Value = Session("ALMACEN")   'cteCODIGO_BINADQUIRIENTE
        hdnCodComercio.Value = Session("ALMACEN")
        hdnombreComercio.Value = ConfigurationSettings.AppSettings("CONST_NOMBRE_COMERCIO")
        Me.Session("PAGINA_INICIAL") = "RecPagoDocId.aspx"
        hdnRutaLog.Value = ConfigurationSettings.AppSettings("cteCODIGO_RUTALOG")
        hdnDetalleLog.Value = ConfigurationSettings.AppSettings("cteCODIGO_DETALLELOG")
        hdTipoCambio.Value = ObtenerTipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"))
        ''solo desarrollo
        hdTipoCambio.Value = Replace(hdTipoCambio.Value, ",", ".")
        hdCodAquirienteReenvia.Value = ConfigurationSettings.AppSettings("CONST_BIN_REENVIO")
        txtTipoIdentificador.Value = "01"

        lblTC.Text = ObtenerTipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"))
        ''solo desarrollo
        ''lblTC.Text = Replace(lblTC.Text, ",", ".")

    End Sub

    Private Function ObtenerTipoCambio(ByVal strFecha As String) As String
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        'Dim intSAP = objOffline.Get_ConsultaSAP
        'FFS
        'Trabajar en Modo desconectado Tipo de Cambio
        'Dim obPagos As New SAP_SIC_Pagos.clsPagos
        Dim obPagos As Object
        'If intSAP = 1 Then
        '    obPagos = New SAP_SIC_Pagos.clsPagos
        'Else
        obPagos = New COM_SIC_OffLine.clsOffline
        'End If
        'Return obPagos.Get_TipoCambio(strFecha).ToString("N2")
        'If intSAP = 1 Then
        '    Return Format(obPagos.Get_TipoCambio(strFecha), "#######0.00")
        'Else
        Return Format(obPagos.Obtener_TipoCambio(strFecha), "#######0.000") 'aotane 05.08.2013
        'Return 0
        'End If
    End Function

    Private Sub LlenaCombos()
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim intSAP = objOffline.Get_ConsultaSAP
        Dim dtVias As DataTable
        'CAMBIADO POR FFS INICIO
        'Trabajar en Modo desconectado de ZAP
        Dim dsFormaPago As DataSet
        Dim objPagos As Object
        'If intSAP = 1 Then
        '    objPagos = New SAP_SIC_Pagos.clsPagos
        '    dsFormaPago = objPagos.Get_ConsultaViasPago(Session("ALMACEN"))
        'Else
        objPagos = New COM_SIC_OffLine.clsOffline
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Se procede a llenar el dataset dsFormaPago")
        dsFormaPago = objPagos.Obtener_ConsultaViasPago(Session("ALMACEN"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, "El dsFormaPago se llena")
        'End If
        'Dim dsFormaPago As DataSet = objPagos.Get_ConsultaViasPago(Session("ALMACEN"))
        'CAMBIADO POR FFS FIN
        dtVias = New DataTable
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio metodo VerificarVias")
        dtVias = VerificarVias(dsFormaPago)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin metodo VerificarVias")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Se procede a llenar los combos")
        cboLoad(dtVias, cboTipDocumento1)
        cboLoad(dtVias, cboTipDocumento2)
        cboLoad(dtVias, cboTipDocumento3)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Se termina de llenar los combos")
        cboTipDocumento1.Items.Insert(0, "") 'INICIATIVA-318
        cboTipDocumento2.Items.Insert(0, "")
        cboTipDocumento3.Items.Insert(0, "")
        'VIA DE PAGO POR DEFECTO
        Try
            cboTipDocumento1.SelectedValue = "ZEFE"
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
            '    Response.Write("<script language=javascript>window.open('../Pagos/frmValidarUsuario.aspx?v_flujo=" + "FormaPago" + "&v_formapago=" + strFormaPago + "&v_combo=" + strCombo + "','SICAR','width=290px,height=190px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=100,top=100');</script>")

            'End If
            'INI-PROY-140773- SICAR 
            'INICIATIVA-318 FIN
        Catch ex As Exception
            cboTipDocumento1.SelectedIndex = 0
        End Try
        'cboTipDocumento1.SelectedValue = "ZEFE"
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
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio metodo LeeDatosValidar")

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim intSAP = objOffline.Get_ConsultaSAP

        '***************LEE TARJETAS CREDITO
        'Dim objSap As New SAP_SIC_Pagos.clsPagos

        Dim dsTmp As DataSet

        objFileLog.Log_WriteLog(pathFile, strArchivo, "Se procede a cargar el dsTmp Tarjeta Credito")
        Dim objSap As New COM_SIC_OffLine.clsOffline
        dsTmp = objSap.Obtener_Tarjeta_Credito()
        objSap = Nothing
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Se termina de cargar el dsTmp Tarjeta Credito")

        Dim dr As DataRow
        txtTarjCred.Text = ""
        For Each dr In dsTmp.Tables(0).Rows
            txtTarjCred.Text += CStr(dr(0)) + ";"
        Next

        objFileLog.Log_WriteLog(pathFile, strArchivo, "Se procede a cargar el dsTmp Bin")
        '*************leee BIN
        Dim obCajas As New COM_SIC_Cajas.clsCajas
        dsTmp = obCajas.FP_ListaBIN()
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Se termina de cargar el dsTmp Bin")

        txtBIN.Text = ""
        For Each dr In dsTmp.Tables(0).Rows
            txtBIN.Text += CStr(dr(0)) + ";"
        Next


    End Sub

    Private Function VerificarVias(ByVal ds As DataSet) As DataTable
        Dim StrRectricciones As String
        Dim StrEfectivo, StrCheque, StrTarjeta, StrDebito, StrOtros As String
        Dim ArrEfectivo, ArrCheque, ArrTarjeta, ArrDebito, ArrOtros, ArrTotal As ArrayList
        Dim LOpciones As New ArrayList
        Dim dtVias As DataTable = New DataTable("ViasPago")
        Dim boolexiste As Boolean
        Dim dtViasFiltro As New DataTable("ViasPagoF") 'CCC
        Try
            If Session("WS_OpcionesPagina") Is Nothing Then
                'Proy-31949 Inicio
                'Response.Write("<script> alert('Error: No se ubica el perfil para las vias de pago'); window.location='" & strPaginaRetorno & "'; </script>")
                ValidarSalirPagoDocID("Error: No se ubica el perfil para las vias de pago")
                'Proy-31949 Fin
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
			objFileLog.Log_WriteLog(pathFile, strArchivo, "CADENA ArrEfectivo : " & StrEfectivo)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "CADENA ArrCheque : " & StrCheque)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "CADENA ArrTarjeta : " & StrTarjeta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "CADENA ArrDebito : " & StrDebito)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "CADENA ArrOtros : " & StrOtros)
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
				objFileLog.Log_WriteLog(pathFile, strArchivo, "ViasPago : " & fila.Item(0))
                objFileLog.Log_WriteLog(pathFile, strArchivo, "ViasPago : " & fila.Item(1))
            Next
            Return dtViasFiltro
            'FIN CCC - SD_631020
        Catch ex As Exception
            'Response.Write("<script language=jscript> alert('" + ex.Message + "');</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-----ERROR en el metodo VerificarVias ----")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Mensaje : " & ex.Message)
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

    'Proy - 31949 Inicio
    'Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
    '    Dim ruta$ = "RecPagoDocId.aspx"
    '    Response.Redirect(ruta)
    'End Sub
    'Proy - 31949 Fin

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
        Return strResp.Trim
    End Function

    Private Function ObtenerTramaRecibosST(ByVal strRecibos As String) As String
        Return strRecibos.Replace(",", "|")
    End Function

    Private Sub inicializaObjeto()
        cboTipDocumento1.SelectedValue = "ZEFE"
        cboTipDocumento2.SelectedValue = ""
        cboTipDocumento3.SelectedValue = ""
        txtDoc1.Text = ""
        txtDoc2.Text = ""
        txtDoc3.Text = ""
        txtMonto1.Text = ""
        txtMonto2.Text = ""
        txtMonto3.Text = ""
        txtDoc1.Enabled = False
        txtDoc2.Enabled = False
        txtDoc3.Enabled = False
        txtMonto2.Enabled = False
        txtMonto3.Enabled = False
        txtRecibidoPen.Text = ""
        txtRecibidoUsd.Text = "0.00"
        txtVuelto.Text = ""
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim strIdentifyLog As String = Me.txtTipoIdentificador.Value & "|" & Me.txtIdentificador.Value
        'Dim strIdentifyLog As String = ""
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio btnGrabar_Click")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        ''' VERIFICACION DE CUADRE DE CAJA 05.02.2014 TS-JTN
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
        If (objOffline.CuadreCajeroRealizado(Session("ALMACEN"), codUsuario)) Then
            Response.Write("<script>alert('Error: No puede realizar esta operacion, ya realizo cuadre de caja')</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error: No puede realizar esta operacion, ya realizo cuadre de caja")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN btnGrabar_Click")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            inicializaObjeto()
            Exit Sub
        End If
        ''' FIN VERIFICACION
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim MontoTotalAPagar, TramaFormasPagoSAP
        Try
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
                Response.Write("<script>alert('Error: Ha alcanzado su maximo disponible de efectivo en caja. Debe depositar en caja buzón')</script>")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error: Ha alcanzado su maximo disponible de efectivo en caja. Debe depositar en caja buzón")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN btnGrabar_Click")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
                inicializaObjeto()
                Exit Sub
            End If
            ''' TS-CCC *** FIN :: VALIDACION DE MONTO MAXIMO EN EFECTIVO *****
            MontoTotalAPagar = Me.ObtenerMontoTotalPagar()
            TramaFormasPagoSAP = Me.ObtenerTramaFormasDePagoSAP()
            '********************************************************************************************
            '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	MontoTotalAPagar:" & Funciones.CheckStr(MontoTotalAPagar))
            '********************************************************************************************
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	TramaFormasPagoSAP:" & Funciones.CheckStr(TramaFormasPagoSAP))

            'Proy-31949 -- Inicio

            'Se hace esta parte porque cuando se trae del lado cliente, se trae la trama separada por coma ",".
            Dim sTramaRecibosST As String = Me.ObtenerTramaRecibosST(hidRecibos.Value)
            sTramaRecibosST = sTramaRecibosST.Substring(1, sTramaRecibosST.Length - 1)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	sTramaRecibosST:" & Funciones.CheckStr(sTramaRecibosST))
            'Me.Recibos = TramaRecibosST.Split("|")
            ' se añade este bloque para no impedir que se procese el pago por la ausencia de DNI o RUC
            If Len(Trim(txtDNI.Text)) = 0 Then
                txtDNI.Text = "99999999"
            End If
            'fin de bloque
            'If Len(Trim(txtIdentificadorCliente.Value)) = 0 Or Len(Trim(txtNombreCliente.Value)) = 0 Then
            'If Len(Trim(txtNombreCliente.Value)) = 0 Then
            '    'Throw New Exception("El Nombre o el RUC no pueden estar vacios")
            '    Throw New Exception("El Nombre no puede estar vacio")
            'End If

            Me.PagarDocumentos( _
                 Me.hdnRutaLog.Value, _
                 Me.hdnDetalleLog.Value, _
                 Me.hdnPuntoDeVenta.Value, _
                 Me.intCanal.Value, _
                 Me.hdnBinAdquiriente.Value, _
                 Me.hdnCodComercio.Value, _
                 Me.hdnUsuario.Value, _
                 Me.txtTipoIdentificador.Value, _
                 Me.txtDNI.Text, _
                 TramaFormasPagoSAP, _
                 MontoTotalAPagar, _
                 sTramaRecibosST, _
                 txtNumeroTrace.Value)

            'Proy-31949 -- Fin

            'Threading.Thread.Sleep(20000) 'forzando demora de 20 segundos.
        Catch ex As Exception
            'Proy-31949 Inicio
            'Response.Write("<script language=jscript> alert('" + ex.Message + "'); window.location='" & strPaginaRetorno & "';</script>")
            ValidarSalirPagoDocID(Funciones.CheckStr(ex.Message))
            'Proy-31949 Fin
        Finally
            objCajas = Nothing
            objOffline = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin btnGrabar_Click")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try
    End Sub

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
        Try
            Dim sMensaje$
            Dim strIdentifyLog As String = Me.txtTipoIdentificador.Value & "|" & Me.txtIdentificador.Value
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
            Dim decImpPen, decImpUsd, decVuelto As Decimal
            decImpPen = Decimal.Parse(IIf(txtRecibidoPen.Text.Trim() = "", "0.00", txtRecibidoPen.Text))
            decImpUsd = Decimal.Parse(IIf(txtRecibidoUsd.Text.Trim() = "", "0.00", txtRecibidoUsd.Text))
            decVuelto = Decimal.Parse(IIf(txtVuelto.Text.Trim() = "", "0.00", txtVuelto.Text))
            'decImpPen = Decimal.Parse(IIf(hdDeudaPorCuenta.Value.Trim() = "", "0.00", hdDeudaPorCuenta.Value))
            'decImpUsd = 0
            'decVuelto = decImpPen - dblMontoTotalPagar

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

            'CAMBIADO POR FFS
            Dim strRespuesta As String = obPagos.Pagar_DNI( _
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
                 Me.txtIdentificador.Value, _
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
                codigoProcesador, "", "", "", "", "", "", True)
            ' FIN FFS

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strRespuesta: " & strRespuesta)
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
                            'oAuditoria.RegistrarAuditoria(CodTrx, CodServicio, ipHost, nameHost, ipServer, nameServer, User, "", "0", DesTrx) ' ADD JCPM
                            Response.Redirect(strPaginaRetorno)
                        Else
                            Session("strMENSREC") = arrMensajeError(1)
                            'Response.Write("<script> alert('" + arrMensajeError(1) + "');  </script>")
                            wParam5 = 0
                            wParam6 = "Error en Recaudacion Postpago." & Session("strMENSREC")
                            objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                            Response.Redirect(strPaginaRetorno)
                        End If
                    Else
                        Session("strMENSREC") = arrMensaje(1)
                        'Response.Write("<script> alert('" + arrMensaje(1) + "');  </script>")
                        wParam5 = 0
                        wParam6 = "Error en Recaudacion Postpago." & Session("strMENSREC")
                        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                        Response.Redirect(strPaginaRetorno)
                    End If
                Else
                    Session("strMENSREC") = "Verifique los datos ingresados. Por favor vuelva a intentar."
                    'Response.Write("<script> alert('Verifique los datos ingresados. Por favor vuelva a intentar.');  </script>")
                    wParam5 = 0
                    wParam6 = "Error en Recaudacion Postpago." & Session("strMENSREC")
                    objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)

                    sMensaje = "Error en Recaudacion Postpago. " & sMensaje & ". Datos: Canal=" & intCanal.Value & "|PDV=" & hdnPuntoDeVenta.Value & "|Cajero=" & hdnUsuario.Value & "|Nro. DNI=" & Me.txtDNI.Text & "|Tipo de Servicio=" & Me.cboTipoServicio.SelectedItem.Text
                    'RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("codTrxFijaPagAnularPago")) ' codTrxFijaPagAnularPago
                    RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("codTrxRealizaPago")) ' CODIGO JCPM

                    'Response.Redirect(strPaginaRetorno)
                    'Proy-31949 Inicio
                    'Response.Write("<script language=jscript> alert('" + strRespuesta + "'); window.location='" & strPaginaRetorno & "';</script>")
                    ValidarSalirPagoDocID(strRespuesta)
                    'Proy-31949 Fin
                End If
            Else
                If cboTipDocumento1.SelectedValue = "ZEFE" Then
                    objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto1.Text))
                    '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	FP_InsertaEfectivo: " & txtMonto1.Text)
                End If
                If cboTipDocumento2.SelectedValue = "ZEFE" Then
                    objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto2.Text))
                    '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	FP_InsertaEfectivo: " & txtMonto2.Text)
                End If
                If cboTipDocumento3.SelectedValue = "ZEFE" Then
                    objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto3.Text))
                    '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	FP_InsertaEfectivo: " & txtMonto3.Text)
                End If
                'Mandar a la página de consulta a SAP.
                arrMensaje = strRespuesta.Split("@")
                Dim arrCabecera() As String = arrMensaje(1).Split(";")
                Dim strNumeroDeuda, strAccion
                strNumeroDeuda = arrCabecera(cteNUMERODEUDA)
                strAccion = cteACCION_CONFIRMACION
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strNumeroDeuda: " & strNumeroDeuda)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strAccion: " & strAccion)

                'PROY-27440 INI
                Dim strNumeroRec As String = strNumeroDeuda
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Inicio Actualizar cabezera " & "	strNumeroDeuda: " & strNumeroRec)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Inicio Actualizar cabezera " & " HidIdCabez: " & Me.HidIdCabez.Value)
                If (strNumeroRec <> "" And Funciones.CheckStr(Me.HidIdCabez.Value) <> "") Then
                    Me.actualizar_codigo_recuadacion(strNumeroRec, Me.HidIdCabez.Value)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Fin Actualizar cabezera " & "	strNumeroDeuda: " & strNumeroRec)
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strNumeroDeuda: " & strNumeroRec)
                'PROY-27440 FIN
                sMensaje = "Pago de Recaudación. " & sMensaje & ". Datos: Canal=" & intCanal.Value & "|PDV=" & hdnPuntoDeVenta.Value & "|Cajero=" & hdnUsuario.Value & "|Nro. DNI=" & Me.txtDNI.Text & "|Tipo de Servicio=" & Me.cboTipoServicio.SelectedItem.Text
                'RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("codTrxFijaPagAnularPago")) ' codTrxFijaPagAnularPago
                RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("codTrxRealizaPago")) ' CODIGO JCPM
                objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                Response.Redirect("conDocumentos.aspx?act=" & strAccion & "&num=" & strNumeroDeuda & "&pdni=" & strNumeroIdentificadorDeudor)
            End If
        Catch ex As Exception
            'Proy-31949 Inicio
            'Response.Write("<script language=jscript> alert('" + ex.Message + "'); window.location='" & strPaginaRetorno & "';</script>")
            ValidarSalirPagoDocID(Funciones.CheckStr(ex.Message))
            'Proy-31949 Fin
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
            objSicarDB.ActualizarTransaction(objEntity, strCodRpt, strMsgRpt)

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

    Private Sub cmdBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click

    End Sub
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
        hddcboFormaPago.Value = cboTipDocumento2.SelectedValue
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
        hddcboFormaPago.Value = cboTipDocumento3.SelectedValue
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
    'INICIATIVA-318 FIN
End Class
