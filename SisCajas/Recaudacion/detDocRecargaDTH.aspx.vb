Imports COM_SIC_INActChip
Imports COM_SIC_Cajas
Imports SisCajas.Funciones
Imports System.Globalization
Imports System.Configuration

Imports System.IO
Imports COM_SIC_Activaciones
Imports SisCajas.clsActivaciones
Imports System.Text
Imports System.Net

 
Public Class detDocRecargaDTH
    Inherits SICAR_WebBase

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents lblTC As System.Web.UI.WebControls.Label
    Protected WithEvents txtNombreCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumeroDocumentos As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtValorDeuda As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTipoDocumento1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDocumento1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTipoDocumento2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDocumento2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTipoDocumento3 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDocumento3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecibidoSoles As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtVuelto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecibidoUsd As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents strRecibos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnMensaje As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnEjecutado As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTipoIdentificador As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTextIdentificador As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTarjetaCredito As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidBIN As System.Web.UI.HtmlControls.HtmlInputHidden

    Public Recibos() As String
    Protected WithEvents hidNumeroTrace As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTipoDocCliente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidNroDocCliente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidMonto As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidMensaje As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidNroFilas As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected Fecha As String
    Protected Canal As String
    Protected Usuario As String
    Protected PuntoVenta As String
    Protected CodAplicacion As String
    Protected dblTotal As Double
    Protected dblPrecio As Double
    Protected dblPrecioIGV As Double
    Protected dblIGV As Double
    Protected dblDescuento As Double
    Protected TipoAudi As String
    Protected MensajeAudi As String
    Protected CodImpresionTicket As String

    Protected param_in As String
    Protected param_out As String

    Protected NroOperacion As String
    Protected FechaExpiracion As String

    Public Const COD_MONEDA_SOLES = "604"
    Public Const VAL_MONEDA_SOLES = "PEN"
    Public Const COD_MONEDA_DOLARES = "840"
    Public Const VAL_MONEDA_DOLARES = "USD"
    Public Const COD_ACCION_CONFIRMACION = "1"

    Dim oCajas As New COM_SIC_Cajas.clsCajas
    Dim oPagos As New SAP_SIC_Pagos.clsPagos
    Dim oSapCajas As New SAP_SIC_Cajas.clsCajas
    Dim oVentas As New SAP_SIC_Ventas.clsVentas

    Protected WithEvents hidNroPedido As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtIdentificadorCliente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtNombres As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRazonSocial As System.Web.UI.WebControls.TextBox
    Protected WithEvents hidFlagCliente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtApellidosPat As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtApellidosMat As System.Web.UI.WebControls.TextBox
    Protected WithEvents hidNroRecarga As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidFecVencimiento As System.Web.UI.HtmlControls.HtmlInputHidden

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
    'PROY-27440 FIN

    'Proy-31949 INICIO
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
    Public objFileLog As New SICAR_Log
    Dim nameFilePos As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    'Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS") 'INC000004584664
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogRecarga") 'INC000004584664
    Dim strArchivoPos As String = objFileLog.Log_CrearNombreArchivo(nameFilePos)
    'PROY-27440 FIN

    'Proy-31949 Inicio
    Private Sub validar_pedido_pos()
        Dim strPedidoLog As String = "Pedido: [" & Funciones.CheckStr(Request.QueryString("textoIdentificador")) & "] "

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Inicio : ")

        Dim dsPedidoPOS As DataSet
        Dim objPedidoPOS As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim strNroPedido As String = Funciones.CheckStr(Request.QueryString("textoIdentificador"))
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

            Dim intRows As Integer = dsPedidoPOS.Tables(0).Rows.Count
            If intRows > 3 Then
                intRows = 2
            End If

            For i As Int32 = 0 To intRows - 1
                Dim objTxt As HtmlInputHidden
                Dim objCombo As DropDownList
                Dim objTipPos As HtmlInputHidden
                strTipoTarjeta = ""

                objTxt = CType(Me.FindControl("HidFila" & i + 1), HtmlInputHidden)
                objCombo = CType(Me.FindControl("ddlTipoDocumento" & i + 1), DropDownList)
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

        'Proy-31949 Inicio
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

        'FIN CONSULTA PAGO AUTOMATICO POS

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : Validacion Integracion FIN")

        Me.HidCodOpera.Value = ClsKeyPOS.strCodOpeVE & "|" & _
        ClsKeyPOS.strCodOpeREC & "|" & ClsKeyPOS.strCodOpeAN

        Me.HidDesOpera.Value = ClsKeyPOS.strDesOpeVE & "|" & _
        ClsKeyPOS.strDesOpeREC & "|" & ClsKeyPOS.strDesOpeAN
        Me.HidTipoOpera.Value = ClsKeyPOS.strOpeFina & "|" & ClsKeyPOS.strNoFina 'OPE FI(90)
        Me.HidTipoTarjeta.Value = ClsKeyPOS.strCodTarjetaVS & "|" & ClsKeyPOS.strCodTarjetaMC

        Me.HidTipoPago.Value = ClsKeyPOS.strTipPagoPDTH 'RECARGA DTH

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

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIp : " & strIpClient)
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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

'#End Region 'PROY-27440 

    Dim COD_CANAL As String = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")
    Dim COD_RUTA_LOG As String = ConfigurationSettings.AppSettings("cteCODIGO_RUTALOG")
    Dim COD_DETALLE_LOG As String = ConfigurationSettings.AppSettings("cteCODIGO_DETALLELOG")
    Dim COD_BINADQUIRIENTE As String = ConfigurationSettings.AppSettings("cteCODIGO_BINADQUIRIENTE")

    '******************************************************************
    '**Variables, registro pedido mssap:
    Dim objAct As New COM_SIC_Activaciones.clsTrsMsSap
    Dim objMsSAP As New COM_SIC_Activaciones.clsConsultaMsSap

    Dim K_PEDIN_NROPEDIDO As Int64
    Dim K_NROLOG As String = ""
    Dim K_DESLOG As String = ""
    Dim K_PAGON_IDPAGO As Int64
    Dim K_PAGOC_CORRELATIVO As String = ""
    Dim K_PAGOV_CORRELATIVO As String = ""
    Dim K_NROLOG_DET As String = ""
    Dim K_DESLOG_DET As String = ""
    Dim strClasePedido As String = ""
    Dim strDescrClasePedido As String = ""
    Dim strIdentifyLog As String = ""
    Dim K_CU_CORRELATIVOFE As String = ""

    Dim K_COD_RESPUESTA As String
    Dim K_MSJ_RESPUESTA As String
    Dim K_ID_TRANSACCION As String
    '******************************************************************

    ' Codigo de Trx Recarga DTH
    'Dim COD_TRX_PED_SAP As String = ConfigurationSettings.AppSettings("COD_TRX_PED_SAP")
    'Dim COD_TRX_PAGO_ST As String = ConfigurationSettings.AppSettings("COD_TRX_PAGO_ST")
    'Dim COD_TRX_PAGO_SAP As String = ConfigurationSettings.AppSettings("COD_TRX_PAGO_SAP")
    'Dim COD_TRX_REG_BD As String = ConfigurationSettings.AppSettings("COD_TRX_REG_BD")

    'Dim objFileLog As New SICAR_Log 'PROY-27440 
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRegistroDOL")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo("Log_RecargaDTH")


    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga") 'INC000004584664
    

    Dim strArchivo1 As String = objFileLog.Log_CrearNombreArchivo(ConfigurationSettings.AppSettings("constNameLogRecaudacion")) ''INC668487
    Dim pathFile1 As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion") ''INC668487

    Dim codigoOperacion% = 0
    Dim numeroRefSunat$ = ""
    Dim numeroOperacionPago% = 0

    Dim valoresPago As New DatosPago

    Public dtIGV As DataTable
'PROY-27440 - INI
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
            Me.txtDocumento1.Text = strTarjeta
            'TIPO DE TARJETA 
            Me.ddlTipoDocumento1.SelectedIndex = Funciones.CheckInt(strArrys(2).Substring(strArrys(2).IndexOf("=") + 1))

        End If

        If Me.HidFila2.Value.Trim() <> "" Then
            strArrys = Me.HidFila2.Value.Split("|")
            'MONTO
            strMonto = "" : strMonto = strArrys(0).Substring(strArrys(0).IndexOf("=") + 1)
            Me.txtMonto2.Text = strMonto
            'NRO TARJETA
            strTarjeta = "" : strTarjeta = strArrys(1).Substring(strArrys(1).IndexOf("=") + 1)
            Me.txtDocumento2.Text = strTarjeta
            'TIPO DE TARJETA 
            Me.ddlTipoDocumento2.SelectedIndex = Funciones.CheckInt(strArrys(2).Substring(strArrys(2).IndexOf("=") + 1))
        End If

        If Me.HidFila3.Value.Trim() <> "" Then
            strArrys = Me.HidFila3.Value.Split("|")
            'MONTO
            strMonto = "" : strMonto = strArrys(0).Substring(strArrys(0).IndexOf("=") + 1)
            Me.txtMonto3.Text = strMonto
            'NRO TARJETA
            strTarjeta = "" : strTarjeta = strArrys(1).Substring(strArrys(1).IndexOf("=") + 1)
            Me.txtDocumento3.Text = strTarjeta
            'TIPO DE TARJETA 
            Me.ddlTipoDocumento3.SelectedIndex = Funciones.CheckInt(strArrys(2).Substring(strArrys(2).IndexOf("=") + 1))
        End If

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_values_pos : " & "HidFila1 : " & HidFila1.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_values_pos : " & "HidFila2 : " & HidFila2.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_values_pos : " & "HidFila3 : " & HidFila3.Value)
    End Sub
''PROY-27440 -FIN

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If Session("USUARIO") Is Nothing Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        End If

        ' Eventos Controles
        txtMonto1.Attributes.Add("onBlur", "f_Recalcular(this);")
        txtMonto2.Attributes.Add("onBlur", "f_Recalcular(this);")
        txtMonto3.Attributes.Add("onBlur", "f_Recalcular(this);")
        txtMonto1.Attributes.Add("onkeypress", "ValidaNumero(this);")
        txtMonto2.Attributes.Add("onkeypress", "ValidaNumero(this);")
        txtMonto3.Attributes.Add("onkeypress", "ValidaNumero(this);")
        txtRecibidoSoles.Attributes.Add("onkeypress", "ValidaNumero(this);")
        txtRecibidoUsd.Attributes.Add("onkeypress", "ValidaNumero(this);")
        txtRecibidoSoles.Attributes.Add("onChange", "CalculoVuelto();")
        txtRecibidoUsd.Attributes.Add("onChange", "CalculoVuelto();")
        btnGrabar.Attributes.Add("OnClick", "return f_validaCajaCerrada();f_Grabar();") 'INICIATIVA-318 INI

        ' Variables Generales
        Fecha = String.Format("{0:dd/MM/yyyy}", Now)
        Canal = Session("CANAL")
        Usuario = Session("USUARIO")
        PuntoVenta = Session("ALMACEN")
        CodAplicacion = ConfigurationSettings.AppSettings("codAplicacion")
        CodImpresionTicket = Funciones.CheckStr(Session("CodImprTicket"))

        If Not Funciones.CheckStr(strRecibos.Value) = "" Then
            Recibos = strRecibos.Value.Split(",")
        End If

        If Not IsPostBack Then
            Me.load_data_param_pos()   'PROY-27440 
            Inicio()
        Else
            'PROY-27440 INI
            Me.load_values_pos()
            'PROY-27440 FIN
        End If

    End Sub

    Private Sub Inicio()

        Try
            Session("Pago") = False 'INC000004584664
            hidTipoDocCliente.Value = Request.QueryString("tipoDocCliente")
            hidNroDocCliente.Value = Request.QueryString("nroDocCliente")
            hidTipoIdentificador.Value = Request.QueryString("tipoIdentificador")
            hidTextIdentificador.Value = Request.QueryString("textoIdentificador")
            hidNroRecarga.Value = Request.QueryString("nroRecarga")

            LlenarCombos() 'CONSULTA LAS VIAS DE PAGO-RFC
            LeeDatosValidar() 'CONSULTA LAS TARJETAS DE CREDITP
            'BuscarCliente() 'CONSULTA DATOS CLIENTE RFC->PENDIENTE CREAR TABLA O VISTA + SPs
            ObtenerDocumentosxPagar() 'LLAMA AL ST Y RFC REGISTRA LOG
            lblTC.Text = ObtenerTipoCambio() ' obtiene el tipo de cambio RFC
            Me.validar_pedido_pos() 'Proy-31949
        Catch ex As Exception
            hidMensaje.Value = "Error. Consulta Parametros Recarga Virtual DTH. " & ex.Message
        End Try
    End Sub

    Private Function ObtenerMontoMinimoRecarga() As Double
        Dim objSicarDB As New COM_SIC_Cajas.clsCajas
        Dim dsParam As DataSet
        Dim dblMontoMinimo As Double
        Try
            dsParam = objSicarDB.FP_ConsultaParametros(ConfigurationSettings.AppSettings("constGrupoRecargaDTH").ToString())
            If Not IsNothing(dsParam) AndAlso dsParam.Tables(0).Rows.Count > 0 Then
                For idx As Integer = 0 To dsParam.Tables(0).Rows.Count - 1
                    If Funciones.CheckStr(dsParam.Tables(0).Rows(idx).Item("SPARN_CODIGO")).Equals(ConfigurationSettings.AppSettings("constCodParamMontoMinimoRDTH").ToString()) Then
                        dblMontoMinimo = Funciones.CheckStr(dsParam.Tables(0).Rows(idx).Item("SPARV_VALUE"))
                        Exit For
                    End If
                Next
            End If

            '''objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "dblMontoMinimo : " & dblMontoMinimo.ToString())
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "ERROR ObtenerMontoMinimoRecarga: " & ex.Message.ToString())
        End Try
        objSicarDB = Nothing
        Return dblMontoMinimo
    End Function

#Region "Funciones Generales"

    Private Sub LlenarCombos()

        Dim intSAP As String
        Dim dsFormaPago As DataSet
        Dim dtVias As DataTable

        intSAP = (New COM_SIC_OffLine.clsOffline).Get_ConsultaSAP
        '''CAMBIADO POR JTN
        '''
        'If intSAP = 1 Then
        'dsFormaPago = (New SAP_SIC_Pagos.clsPagos).Get_ConsultaViasPago(PuntoVenta)
        'Else
        '    dsFormaPago = (New COM_SIC_OffLine.clsOffline).Get_ConsultaViasPago(PuntoVenta)
        'End If . !
        dsFormaPago = (New COM_SIC_OffLine.clsOffline).Obtener_ConsultaViasPago(PuntoVenta)
        '''CAMBIADO HASTA AQUI
        '''

        If (Not IsNothing(dsFormaPago)) AndAlso (dsFormaPago.Tables(0).Rows.Count > 0) Then

            dtVias = New DataTable
            dtVias = VerificarVias(dsFormaPago)

            ComboLoad(dtVias, ddlTipoDocumento1)
            ComboLoad(dtVias, ddlTipoDocumento2)
            ComboLoad(dtVias, ddlTipoDocumento3)

            ddlTipoDocumento2.Items.Insert(0, "")
            ddlTipoDocumento3.Items.Insert(0, "")

            ' Vía de Pago por Defecto
            ddlTipoDocumento1.SelectedValue = "ZEFE"
            ddlTipoDocumento2.SelectedValue = ""
            ddlTipoDocumento3.SelectedValue = ""
        End If

        dsFormaPago = Nothing
    End Sub

    Private Sub ComboLoad(ByVal dsFormaPago As DataTable, ByRef ddlCampo As DropDownList)

        With ddlCampo
            .DataSource = dsFormaPago
            .DataTextField = "VTEXT"
            .DataValueField = "CCINS"
            .DataBind()
        End With

    End Sub

    Private Sub LeeDatosValidar()

        Dim intSAP As String
        Dim dsTarjeta As DataSet

        intSAP = (New COM_SIC_OffLine.clsOffline).Get_ConsultaSAP

        '''CAMBIADO POR JTN
        '''
        'If intSAP = 1 Then
        '    dsTarjeta = (New SAP_SIC_Pagos.clsPagos).Get_Tarjeta_Credito()
        'Else
        '    dsTarjeta = (New COM_SIC_OffLine.clsOffline).Get_Tarjeta_Credito()
        'End If
        dsTarjeta = (New COM_SIC_OffLine.clsOffline).Obtener_Tarjeta_Credito()
        ''' CAMBIADO HASTA AQUI
        '''
        Dim drFila As DataRow

        For Each drFila In dsTarjeta.Tables(0).Rows
            hidTarjetaCredito.Value += Funciones.CheckStr(drFila(0)) + ";"
        Next

        dsTarjeta = (New COM_SIC_Cajas.clsCajas).FP_ListaBIN()

        For Each drFila In dsTarjeta.Tables(0).Rows
            hidBIN.Value += Funciones.CheckStr(drFila(0)) + ";"
        Next

        drFila = Nothing
        dsTarjeta = Nothing
    End Sub

    Private Function ObtenerTipoCambio() As String
        '''CAMBIADO POR JTN
        'Dim intSAP As Integer = (New COM_SIC_OffLine.clsOffline).Get_ConsultaSAP
        'Dim obPagos As Object
        'If intSAP = 1 Then
        '    obPagos = New SAP_SIC_Pagos.clsPagos
        'Else
        '    obPagos = New COM_SIC_OffLine.clsOffline
        'End If

        'If intSAP = 1 Then
        '    Return Format(obPagos.Get_TipoCambio(Fecha), "#######0.00")
        'Else
        '    Return 0
        'End If
        Dim clsOfflineMode As New COM_SIC_OffLine.clsOffline
        Dim dblTipoCambio# = clsOfflineMode.Obtener_TipoCambio(Fecha)
        Return Format(dblTipoCambio, "#######0.000") 'aotane 05.08.2013
        'Return String.Format("{0:N}", dblTipoCambio)
        '''CAMBIADO HASTA AQUI

    End Function

    Public Function FormatoMonto(ByVal strMonto As String) As Double
        Dim valueReturn#
        Dim xvalue As Double
        If IsNumeric(strMonto) Then
            Return Convert.ToDouble(strMonto)
        End If
        Return 0
        'Return Funciones.CheckDbl(Mid(strMonto, 1, 9) & "." & Mid(strMonto, 10, 2))''CAMBIADO POR JYMMY TORRES
    End Function

    Public Function FormatoFecha(ByVal strFecha As String, ByVal intTipo As Integer) As String

        Dim strNuevaFecha As String

        Select Case intTipo
            Case 0
                If Len(Trim(strFecha)) > 0 Then
                    ''TODO: CAMBIADO POR JYMMY TORRES
                    'Dim fechaReturn As DateTime = CDate(strFecha)
                    strNuevaFecha = Mid(strFecha, 7, 2) & "/" & Mid(strFecha, 5, 2) & "/" & Mid(strFecha, 1, 4)
                    'strNuevaFecha = fechaReturn.ToString("yy/MM/dd")
                End If
            Case Else
                strNuevaFecha = strFecha
        End Select

        Return strNuevaFecha
    End Function

    Public Function FormatoMoneda(ByVal Moneda As String, ByVal TipoMoneda As String)

        Dim strMoneda As String

        Select Case TipoMoneda
            Case COD_MONEDA_SOLES
                strMoneda = VAL_MONEDA_SOLES
            Case COD_MONEDA_DOLARES
                strMoneda = VAL_MONEDA_DOLARES
            Case Else
                strMoneda = Moneda
        End Select

        FormatoMoneda = strMoneda
    End Function

    Private Function ObtenerTramaFormaPago() As String
        Dim TramaPagos As String = ""

        If IsNumeric(txtMonto1.Text) AndAlso Funciones.CheckDbl(txtMonto1.Text) > 0 Then
            TramaPagos = Funciones.CheckStr(ddlTipoDocumento1.SelectedValue)
            TramaPagos += ";" + txtMonto1.Text
            TramaPagos += ";" + Funciones.CheckStr(txtDocumento1.Text) + ";;"
        End If
        If IsNumeric(txtMonto2.Text) AndAlso Funciones.CheckDbl(txtMonto2.Text) > 0 Then
            TramaPagos += "|" + Funciones.CheckStr(ddlTipoDocumento2.SelectedValue)
            TramaPagos += ";" + txtMonto2.Text
            TramaPagos += ";" + Funciones.CheckStr(txtDocumento2.Text) + ";;"
        End If
        If IsNumeric(txtMonto3.Text) AndAlso Funciones.CheckDbl(txtMonto3.Text) > 0 Then
            TramaPagos += "|" + Funciones.CheckStr(ddlTipoDocumento3.SelectedValue)
            TramaPagos += ";" + txtMonto3.Text
            TramaPagos += ";" + Funciones.CheckStr(txtDocumento3.Text) + ";;"
        End If

        Return TramaPagos
    End Function

    Private Function ObtenerMontoTotalPagar() As Double
        Dim MontoPagar As Double = 0.0

        If IsNumeric(txtMonto1.Text) Then
            MontoPagar += Funciones.CheckDbl(txtMonto1.Text)
        End If
        If IsNumeric(txtMonto2.Text) Then
            MontoPagar += Funciones.CheckDbl(txtMonto2.Text)
        End If
        If IsNumeric(txtMonto3.Text) Then
            MontoPagar += Funciones.CheckDbl(txtMonto3.Text)
        End If

        Return MontoPagar
    End Function

    Private Function PrecioSinIGV(ByVal monto As Double) As Double
        dtIGV = Session("Lista_Impuesto")
        If dtIGV.Rows.Count = 0 Then
            Dim strScriptMensaje As String = "Error de Indicador"
            strScriptMensaje = String.Format("<script language='javascript' type='text/javascript'>alert('No existe IGV configurado');</script>")
            If (Not Page.IsStartupScriptRegistered("jsValidacion")) Then Page.RegisterStartupScript("jsValidacion", strScriptMensaje)
            Exit Function
        End If
        Dim MontoPagar As Double = 0.0
        Dim IGVactual As Decimal = 0.0

        For Each row As DataRow In dtIGV.Rows
            If (Date.Now() >= CDate(row("impudFecIniVigencia").ToString.Trim) And Date.Now() <= CDate(row("impudFecFinVigencia").ToString.Trim) And CInt(row("impunTipDoc").ToString.Trim) = 0) Then
                IGVactual = Math.Round(CDec(row("IGV").ToString.Trim()) / 100, 2)
                Exit For
            End If
        Next
        MontoPagar = monto / (1 + Funciones.CheckDbl(IGVactual))
        Return Math.Round(MontoPagar, 2)
    End Function

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

#End Region

#Region "Buscar/Registrar/Actualizar Cliente en Sap"

    Private Function BuscarCliente() As Boolean
        'Dim dsCliente As DataSet
        'Dim TipoDocCliente As String = hidTipoDocCliente.Value
        'Dim NroDocCliente As String = hidNroDocCliente.Value

        ''''CAMBIADO POR JTN
        'Dim objOffLine As New COM_SIC_OffLine.clsOffline
        'dsCliente = objOffLine.GetConsultaCliente(PuntoVenta, TipoDocCliente, NroDocCliente)
        ''''Zpvu_Rfc_Con_Cliente
        ''dsCliente = oPagos.Get_ConsultaCliente(PuntoVenta, TipoDocCliente, NroDocCliente)
        ''''CAMBIADO HASTA AQUI
        ''''
        'If Not IsNothing(dsCliente) AndAlso dsCliente.Tables(0).Rows.Count > 0 Then
        '    ' Mostrar Informacion del Cliente
        '    If Right("00" & TipoDocCliente, 2) = "06" Then
        '        txtNombreCliente.Text = Funciones.CheckStr(dsCliente.Tables(0).Rows(0)(6))
        '    Else
        '        txtNombreCliente.Text = Funciones.CheckStr(dsCliente.Tables(0).Rows(0)(3)) & " " & _
        '                                Funciones.CheckStr(dsCliente.Tables(0).Rows(0)(4)) & " " & _
        '                                Funciones.CheckStr(dsCliente.Tables(0).Rows(0)(5))
        '    End If
        '    hidFlagCliente.Value = "S"
        'Else
        '    hidFlagCliente.Value = "N"
        'End If

        'dsCliente = Nothing
        Return True
    End Function

    Private Function UpdateCliente(ByVal dsCliente As DataSet) As Boolean
        Dim NroDocCliente As String = hidNroDocCliente.Value
        Dim DclienteSAP(64) As String
        '''AÑADIDO POR JTN
        Dim clsOffline As New COM_SIC_OffLine.clsOffline
        '''AÑADIDO HASTA AQUI

        Try
            If Not IsNothing(dsCliente) AndAlso dsCliente.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To dsCliente.Tables(0).Columns.Count - 1
                    DclienteSAP(i) = Funciones.CheckStr(dsCliente.Tables(0).Rows(0).Item(i))
                Next
                If CheckStr(dsCliente.Tables(0).Rows(0).Item(2)) = "" Then
                    DclienteSAP(7) = "1900/01/01"
                    DclienteSAP(2) = ConfigurationSettings.AppSettings("strTipoClienteConsumer")
                End If
                If CheckStr(DclienteSAP(62)) = "" Then
                    DclienteSAP(62) = "1"
                End If
                If CheckStr(DclienteSAP(7)) = "" Then
                    DclienteSAP(7) = "1900/01/01"
                End If
            End If

            Dim dtCliente As New DataSet
            Dim strMensaje As String = ""

            '''CAMBIADO POR JTN
            ' Creacion Cliente Sap
            'dtCliente = oVentas.Set_ActualizaCreaClienteSap(PuntoVenta, DclienteSAP)
            '''CAMBIO LA LOGICA PARA EK ORACLE
            '''SE ENVIA EL NRODOCCLIENTE SE CONSIDERA COMO PK
            '''SE ACTUALIZAN O CREAN DEPENDENDO SI EXISTE O NO EL NRODOCCLIENTE
            dtCliente = clsOffline.SetActualizaCreaClienteSap(NroDocCliente, DclienteSAP)
            '''CAMBIADO HASTA AQUI

            For i As Integer = 0 To dtCliente.Tables(1).Rows.Count - 1
                If dtCliente.Tables(1).Rows(i).Item("TYPE") = "E" Then
                    strMensaje = Funciones.CheckStr(dtCliente.Tables(1).Rows(i).Item("MESSAGE"))
                End If
            Next

            If Not strMensaje = "" Then
                hidMensaje.Value = "Error. Cliente no fue actualizado en SAP. " & strMensaje
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function RegistrarCliente() As Boolean
        Dim dsCliente As DataSet
        Dim TipoDocCliente As String = hidTipoDocCliente.Value
        Dim NroDocCliente As String = hidNroDocCliente.Value

        Try
            '''CAMBIADO POR JTN
            'dsCliente = oPagos.Get_ConsultaCliente(PuntoVenta, TipoDocCliente, NroDocCliente)
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            dsCliente = objOffline.GetConsultaCliente(PuntoVenta, TipoDocCliente, NroDocCliente)
            ''' CAMBIADO HASTA AQUI
            If Not IsNothing(dsCliente) AndAlso dsCliente.Tables(0).Rows.Count > 0 Then
                Return UpdateCliente(dsCliente) ''' SI EXISTE EL CLIENTE LO ACTUALIZA
            Else
                Dim DclienteSAP(64) As String '''ARREGLO CON DATOS DEL CLIENTE NUEVO
                DclienteSAP(0) = NroDocCliente
                DclienteSAP(1) = TipoDocCliente
                DclienteSAP(2) = ConfigurationSettings.AppSettings("strTipoClienteConsumer")

                If Right("00" & TipoDocCliente, 2) = "06" Then
                    DclienteSAP(6) = Funciones.CheckStr(txtRazonSocial.Text)
                Else
                    DclienteSAP(3) = Funciones.CheckStr(txtNombres.Text)
                    DclienteSAP(4) = Funciones.CheckStr(txtApellidosMat.Text)
                    DclienteSAP(5) = Funciones.CheckStr(txtApellidosPat.Text)
                End If

                DclienteSAP(7) = "1900/01/01"
                DclienteSAP(8) = ""
                DclienteSAP(9) = ""
                DclienteSAP(10) = ""
                DclienteSAP(11) = " "
                DclienteSAP(12) = "00"
                DclienteSAP(13) = ""
                DclienteSAP(14) = ""
                DclienteSAP(15) = ""
                DclienteSAP(18) = ""
                DclienteSAP(19) = ""
                DclienteSAP(20) = ""
                DclienteSAP(21) = ""
                DclienteSAP(22) = ""
                DclienteSAP(23) = ""
                DclienteSAP(24) = ""
                DclienteSAP(25) = ""
                DclienteSAP(26) = ""
                DclienteSAP(27) = ""
                DclienteSAP(28) = ""
                DclienteSAP(29) = ""
                DclienteSAP(31) = ""
                DclienteSAP(32) = ""
                DclienteSAP(33) = ""
                DclienteSAP(34) = ""
                DclienteSAP(35) = ""
                DclienteSAP(36) = "0.00"
                DclienteSAP(37) = "0.00"
                DclienteSAP(38) = ""
                DclienteSAP(39) = ""
                DclienteSAP(40) = ""
                DclienteSAP(41) = "0.00"
                DclienteSAP(42) = ""
                DclienteSAP(43) = ""
                DclienteSAP(44) = ""
                DclienteSAP(45) = ""
                DclienteSAP(46) = ""
                DclienteSAP(47) = ""
                DclienteSAP(48) = ""
                DclienteSAP(49) = ""
                DclienteSAP(50) = ""
                DclienteSAP(51) = ""
                DclienteSAP(52) = ""
                DclienteSAP(53) = ""
                DclienteSAP(54) = ""
                DclienteSAP(55) = ""
                DclienteSAP(56) = "01"
                DclienteSAP(57) = ""
                DclienteSAP(58) = "00"
                DclienteSAP(59) = "00"
                DclienteSAP(60) = "0"
                DclienteSAP(61) = ""
                DclienteSAP(62) = ""

                Dim dtCliente As New DataSet
                Dim strMensaje As String = ""
                '''CAMBIADO POR JTN
                ' Creacion Cliente Sap
                'dtCliente = oVentas.Set_ActualizaCreaClienteSap(PuntoVenta, DclienteSAP)
                '''LOGICA CAMBIADA PARA DESCONEXION DE SAP
                dtCliente = objOffline.SetActualizaCreaClienteSap(NroDocCliente, DclienteSAP)
                'CAMBIADO HASTA AQUI
                'For i As Integer = 0 To dtCliente.Tables(1).Rows.Count - 1
                '    If dtCliente.Tables(1).Rows(i).Item("TYPE") = "E" Then
                '        strMensaje = Funciones.CheckStr(dtCliente.Tables(1).Rows(i).Item("MESSAGE"))
                '    End If
                'Next
                Dim row As DataRow
                For Each row In dtCliente.Tables(0).Rows
                    If row.Item("TYPE") = "E" Then
                        strMensaje = row.Item("MESSAGE")
                    End If
                Next

                If Not strMensaje = "" Then
                    hidMensaje.Value = "Error. Cliente no puede ser registrado en SAP. " & strMensaje
                    Return False
                End If
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

#End Region

#Region "Generar Cadena de Pedido Sap"

    Function CadenaCabecera() As String
        Dim VTWEG, VKORG As String
        Dim TipoDocVenta As String
        Dim dsOficina As DataSet
        Dim arrayCabecera(49) As String

        '''CAMBIADO POR JTN
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        ' Obtener Parametros Generales del Punto de Venta
        dsOficina = objOffline.ParametrosVenta(PuntoVenta)
        'dsOficina = (New SAP_SIC_Pagos.clsPagos).Get_ParamGlobal(PuntoVenta)
        ''' CAMBIADO HASTA AQUI
        '''
        VTWEG = Funciones.CheckStr(dsOficina.Tables(0).Rows(0).Item("VTWEG"))
        VKORG = Funciones.CheckStr(dsOficina.Tables(0).Rows(0).Item("VKORG"))

        ' Obtener TIPO Documento de Venta
        TipoDocVenta = IIf(hidTipoDocCliente.Value = "06", _
                            ConfigurationSettings.AppSettings("TipoDocVentaFact"), _
                            ConfigurationSettings.AppSettings("TipoDocVentaPed"))

        ' Consulta de Precios
        dblPrecioIGV = ObtenerMontoTotalPagar() 'Funciones.CheckDbl(hidMonto.Value)
        dblPrecio = PrecioSinIGV(dblPrecioIGV)

        arrayCabecera(1) = TipoDocVenta
        arrayCabecera(2) = PuntoVenta
        arrayCabecera(3) = Fecha
        arrayCabecera(4) = hidTipoDocCliente.Value
        arrayCabecera(5) = hidNroDocCliente.Value
        arrayCabecera(6) = ""
        arrayCabecera(7) = "L"
        arrayCabecera(9) = dblPrecio
        arrayCabecera(10) = dblPrecioIGV - dblPrecio
        arrayCabecera(11) = dblPrecioIGV
        arrayCabecera(16) = "02"
        arrayCabecera(27) = Usuario
        arrayCabecera(24) = ""
        arrayCabecera(25) = ""
        arrayCabecera(29) = "01"
        arrayCabecera(39) = ""
        arrayCabecera(48) = VKORG '' TODO: CODIGO DE TIENDA
        arrayCabecera(49) = VTWEG '' TODO: 
        dsOficina = Nothing

        CadenaCabecera = Join(arrayCabecera, ";")
    End Function

    Function CadenaDetallePedido() As String
        Dim arrayDetalle(27) As String
        Dim strServicio, strDesServicio As String

        ' Obtener Parametros Recarga Virtual
        strServicio = ConfigurationSettings.AppSettings("strCodArticuloDTH")
        strDesServicio = ConfigurationSettings.AppSettings("strDesArticuloDTH")

        arrayDetalle(1) = "000001"
        arrayDetalle(2) = strServicio
        arrayDetalle(3) = strDesServicio
        arrayDetalle(4) = "01"
        arrayDetalle(5) = "PVP"
        arrayDetalle(6) = "0001"
        arrayDetalle(7) = "NO DEFINIDO"
        arrayDetalle(8) = ""
        arrayDetalle(9) = "1" 'Funciones.CheckDbl(hidMonto.Value)
        arrayDetalle(10) = dblPrecio
        arrayDetalle(11) = dblPrecioIGV
        arrayDetalle(12) = "0"
        arrayDetalle(13) = "0"
        arrayDetalle(14) = "0"
        arrayDetalle(15) = arrayDetalle(10)
        arrayDetalle(16) = dblPrecioIGV - dblPrecio
        arrayDetalle(18) = "000"
        arrayDetalle(19) = "NO APLICA"
        arrayDetalle(20) = ""
        arrayDetalle(22) = "A10130010" '"A10130011"
        arrayDetalle(25) = "0000000000000000"
        CadenaDetallePedido = Join(arrayDetalle, ";")
    End Function

    '' TODO: CREADO POR JYMMY TORRES
    Function crearTramaCabeceraPedido() As String
        Dim arrayCabecera(14) As String
        Dim strServicio, strDesServicio, moneda, canal As String
        Dim VTWEG, VKORG As String
        Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))

        strServicio = ConfigurationSettings.AppSettings("strCodArticuloDTH")
        strDesServicio = ConfigurationSettings.AppSettings("strDesArticuloDTH")

        Dim tramaRecibos() As String = strRecibos.Value.Split(CChar(";"))
        canal = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        ' Obtener Parametros Generales del Punto de Venta
        '''CAMBIADO POR JYMMY TORRES
        Dim dsOficina As DataSet = objOffline.ParametrosVenta(PuntoVenta)
        If dsOficina.Tables.Count > 0 AndAlso dsOficina.Tables(0).Rows.Count > 0 Then
            VKORG = Funciones.CheckStr(dsOficina.Tables(0).Rows(0).Item("VKORG"))
            VTWEG = Funciones.CheckStr(dsOficina.Tables(0).Rows(0).Item("VTWEG"))
        Else
            VKORG = ""
            VTWEG = ""
        End If
        'ZPDT
        arrayCabecera(0) = ConfigurationSettings.AppSettings("constAUARTDTH").ToString '-->CLASE DOCUMENTO DE VENTAS v
        arrayCabecera(1) = VKORG '-->ORGANIZACION DE VENTAS v
        arrayCabecera(2) = VTWEG '-->CANAL MT
        arrayCabecera(3) = "10" '-->SECTOR
        arrayCabecera(4) = "1" '-->TIPO NUMERO IDENTIFICACION FISCAL
        arrayCabecera(5) = hidNroDocCliente.Value '-->CODIGO CLIENTE SCI ?
        arrayCabecera(6) = DateTime.Today.ToString("dd/MM/yyyy") '-->FECHA DOCUMENTO
        arrayCabecera(7) = PuntoVenta '-->OFICINA DE VENTA
        arrayCabecera(8) = Request.QueryString("numeroConsulta") ''hidTextIdentificador.Value '-->NUMERO DE REFERENCIA
        arrayCabecera(9) = codUsuario '-->NUMERO CLIENTE
        arrayCabecera(10) = "PEN" '-->MONEDA
        arrayCabecera(11) = "02" '-->TIPO VENTA
        arrayCabecera(12) = "01" '-->CLASE VENTA
        arrayCabecera(13) = txtNombreCliente.Text
        arrayCabecera(14) = ""
        'ConfigurationSettings.AppSettings("nombreClienteGenerico").ToString cambiado
        valoresPago.ClaseFactura = IIf(hidTipoDocCliente.Value = "06", ConfigurationSettings.AppSettings("constClaseFactura"), ConfigurationSettings.AppSettings("constClaseVoleta"))
        valoresPago.FechaPago = DateTime.Today
        valoresPago.NumeroDocumento = ""
        valoresPago.ReferenciaSunat = IIf(valoresPago.ReferenciaSunat = "", "0000000000000000", valoresPago.ReferenciaSunat)
        Return Join(arrayCabecera, ";")
        '''CAMBIADO HASTA AQUI
    End Function

    Function crearTramaDetallePedido() As String
        Dim arrayDetalle(14) As String

        Dim strServicio, strDesServicio, moneda, canal As String
        strServicio = ConfigurationSettings.AppSettings("strCodArticuloDTH")
        strDesServicio = ConfigurationSettings.AppSettings("strDesArticuloDTH")

        Dim tramaRecibos() As String = strRecibos.Value.Split(CChar(","))

        canal = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")


        dblPrecioIGV = ObtenerMontoTotalPagar() 'Funciones.CheckDbl(hidMonto.Value)
        dblPrecio = PrecioSinIGV(dblPrecioIGV)

        arrayDetalle(0) = "10" '-->POSICION DOCUMENTO DE VENTAS
        arrayDetalle(1) = ConfigurationSettings.AppSettings("strCodArticuloDTH") '-->NUMERO MATERIAL
        arrayDetalle(2) = "1" '-->CANTIDAD PEDIDO ACUMULADA KWMENG
        arrayDetalle(3) = "1" '-->CAMPAÑA
        arrayDetalle(4) = "1" '-->PLAN TARIFARIO SCI
        arrayDetalle(5) = "000000000" '-->NUMERO TELEFONO
        arrayDetalle(6) = "01" '-->INDICADOR UTILIZACION
        arrayDetalle(7) = dblPrecioIGV 'Usuario '-->CODIGO VENDEDOR GENERICO RECARGAS VIRT/ CARLOS ARIAS DIJO QUE ES EL VALOR DEL MONTO TOTAL DE LA RECAUDACION
        arrayDetalle(8) = dblPrecio '-->VALOR DE LA VENTA
        arrayDetalle(9) = "0" '-->DESCUENTO 1
        arrayDetalle(10) = dblPrecioIGV - dblPrecio '-->IGV
        arrayDetalle(11) = dblPrecioIGV '-->VALOR DE LA VENTA
        arrayDetalle(12) = "0" '-->NUMERO RECARGA VIRTUAL
        arrayDetalle(13) = ConfigurationSettings.AppSettings("strDesArticuloDTH") '-->NUMERO RECARGA VIRTUAL strDesArticuloDTH
        Return Join(arrayDetalle, ";")
    End Function

    Function crearTramaPago() As String
        Dim arrayPagos(13) As String
        Dim viasPagoSap As String() = ConfigurationSettings.AppSettings("constCodigoViasSap").Split(CChar(";"))
        Dim strServicio, strDesServicio, moneda, canal As String

        Dim tramaRecibos() As String = strRecibos.Value.Split(CChar(","))

        canal = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")
        dblPrecioIGV = ObtenerMontoTotalPagar()
        arrayPagos(0) = "" '-->ORGANIZACION DE VENTAS
        arrayPagos(1) = PuntoVenta '-->OFICINA 0006
        arrayPagos(2) = viasPagoSap(0) '-->VIA PAGO ZEFE
        arrayPagos(3) = "" '--> CONC BUSQUEDA
        arrayPagos(4) = "" '-->SOLICITANTE
        arrayPagos(5) = dblPrecioIGV '-->IMPORTE
        arrayPagos(6) = "PEN" '-->MONEDA
        arrayPagos(7) = "" '-->TIPO CAMBIO
        arrayPagos(8) = "" '-->REFERENCIA SUNAT ACTUALIZAR
        arrayPagos(9) = viasPagoSap(0) '-->GLOSA VIA DE PAGO
        arrayPagos(10) = DateTime.Today.ToString("dd/MM/yyyy") '-->FECHA PEDIDO
        arrayPagos(11) = "" '-->CONDICION PAGO
        arrayPagos(12) = "" '-->NRO EXACTUS
        arrayPagos(13) = "001" '-->POS
        Return Join(arrayPagos, ";")
    End Function
    '' CREADO HASTA AQUI
#End Region

#Region "Eventos Botones"

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Dim pagina As String = "bsqDocRecargaDTH.aspx"
        Response.Redirect(pagina)
    End Sub

    Dim fecExpira As String

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- Inicio btnGrabar_Click " & "	flagRecargaDTH: " & Session("flagRecargaDTH")) 'INC000004584664
            If Session("Pago") = True Then Exit Sub 'INC000004584664
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- Inicio btnGrabar_Click " & "	flagRecargaDTH: " & Session("flagRecargaDTH")) 'INC000004584664
        Dim dblMonto, dblMontoMinimo As Double
        'nhuaringa
        Dim objPagos As New COM_SIC_Recaudacion.clsPagosDTH
        Dim objClsTrsMsSap As New COM_SIC_Activaciones.clsTrsMsSap
        Dim strDocumentos As String
        Dim IdentificadorCliente, NumeroTrace As String
        Dim TipoIdentificador, TextIdentificador As String
        Dim TramaReciboST, TramaFormaPago, strRespuesta As String

        Dim datosCabecera, datosDertalle As String

        Dim MontoTotalPago As Double
        Dim arrayMensaje() As String

        dblMonto = ObtenerMontoTotalPagar()
        dblMontoMinimo = ObtenerMontoMinimoRecarga() ''3

        'nhuaringa
        ' Formato de Envio ST
        TramaReciboST = strRecibos.Value.Replace(",", "|") ''INPUT HIDDEN
        TramaFormaPago = ObtenerTramaFormaPago()
        MontoTotalPago = ObtenerMontoTotalPagar()

        '''objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "dblMonto : " & dblMonto.ToString())

        '''objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "dblMontoMinimo : " & dblMontoMinimo.ToString())

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: btnGrabar_Click(), dblMontoMinimo: {1}", CheckStr(hidNroRecarga.Value), CheckStr(dblMontoMinimo)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: btnGrabar_Click(), dblMonto: {1}", CheckStr(hidNroRecarga.Value), CheckStr(dblMonto)))
     
        If dblMonto < dblMontoMinimo Then
            hidMensaje.Value = "RECARGA MINIMA S/ " & dblMontoMinimo.ToString()
            Response.Redirect("bsqDocRecargaDTH.aspx?strMensaje=" & hidMensaje.Value)
            'IMPLEMENTA LA LOGICA DEL CONSULTAPEDIDO
        Else
            'If RegistrarCliente() Then 'ok v
            If RegistrarPedidoMssap() Then              '--> Registro del Pedido en MSSAP.
                If RegistrarPedido() Then               '-->MAPEO Y ENVIO DE DATOS DE REGISTRA PEDIDO
                    If RegistrarPagoST() Then           'ok v -->LOGICA DE DE PAGOS IMPLEMENTADA………………
                        'If RegistrarPagoSap() Then      ' ok --> PAGO EN ST OK
                        'PROY-27440 INI
                        Dim strNumeroRec As String = Funciones.CheckStr(TextIdentificador)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Inicio Actualizar cabezera " & "	strNumeroDeuda: " & strNumeroRec)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Inicio Actualizar cabezera " & " HidIdCabez: " & Me.HidIdCabez.Value)
                        If (K_PEDIN_NROPEDIDO > 0 And Funciones.CheckStr(Me.HidIdCabez.Value) <> "") Then
                            Me.actualizar_codigo_recuadacion(K_PEDIN_NROPEDIDO, Me.HidIdCabez.Value)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Fin Actualizar cabezera " & "	strNumeroDeuda: " & strNumeroRec)
                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strNumeroDeuda: " & strNumeroRec)
                        'PROY-27440 FIN
                        If RegistrarPagoMssap() Then
                            If RegistrarDatosFlujoCaja() Then   '****Registro datos Caja  HARDCODE****' 
                                ''nhuaringa
                                ''INICIO JTN
                                'Dim objOffline As New COM_SIC_OffLine.clsOffline
                                'objOffline.SetNumeroSunat(codigoOperacion, valoresPago.ReferenciaSunat, numeroOperacionPago, ddlTipoDocumento1.SelectedValue)
                                'valoresPago.NumeroDocumento = codigoOperacion
                                ''FIN JTN
                                If ObtenerMontoTotalPagar() >= Me.txtValorDeuda.Text Then
                                    fecExpira = hidFecVencimiento.Value
                                Else
                                    fecExpira = ""
                                End If
                            End If
                            FechaExpiracion = fecExpira
                            ImpresionTicket()
                        End If
                    Else
                        '***Anula Pedido  
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio Anular Pedido - SP: SSAPSU_ANULARPEDIDO")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Nro Pedido: " & K_PEDIN_NROPEDIDO)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Motivo: " & ConfigurationSettings.AppSettings("Motivo_Anulacion"))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Estado: " & ConfigurationSettings.AppSettings("Estado_Anulacion"))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Tipo de Oficina: " & ConfigurationSettings.AppSettings("PEDIC_CLASEPEDIDO"))

                        objClsTrsMsSap.AnularPedido(K_PEDIN_NROPEDIDO, ConfigurationSettings.AppSettings("Motivo_Anulacion"), ConfigurationSettings.AppSettings("Estado_Anulacion"), ConfigurationSettings.AppSettings("PEDIC_CLASEPEDIDO"))

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "FIN Anular Pedido - SP: SSAPSU_ANULARPEDIDO")
                        '**Fin Anulacion Pedido
                    End If
                End If
            End If
        End If

    End Sub

#End Region

#Region "Obtener Documentos a Pagar ST"

    Private Sub ObtenerDocumentosxPagar()
        Dim arrayMensaje() As String
        Dim arrayDetalle() As String
        Dim arrayCabecera() As String
        Dim oRecaudacion As New COM_SIC_Recaudacion.clsConsultas
        Dim CadenaResultado, TipoIdentificador, TextoIdentificador As String

        TipoIdentificador = hidTipoIdentificador.Value
        TextoIdentificador = hidTextIdentificador.Value

        Try
            'Consulta a OLC Recibos a Pagar
            CadenaResultado = oRecaudacion.ConsultarRecibos( _
                                                            COD_RUTA_LOG, _
                                                            COD_DETALLE_LOG, _
                                                            PuntoVenta, _
                                                            COD_CANAL, _
                                                            PuntoVenta, _
                                                            PuntoVenta, _
                                                            Usuario, _
                                                            TipoIdentificador, _
                                                            TextoIdentificador)

            arrayMensaje = CadenaResultado.Split("@")
            If (Not IsNothing(arrayMensaje)) AndAlso Funciones.CheckStr(arrayMensaje(0)) = "00" Then 'ANTES EL COMPONENTE RETORNABA 0| EL WS RETORNA 00
                If arrayMensaje.Length > 1 Then

                    arrayCabecera = arrayMensaje(1).Split(";")
                    arrayDetalle = arrayMensaje(2).Split("|")

                    Dim montoPago As Double
                    montoPago = Funciones.CheckDbl(arrayCabecera(3))

                    ''TODO: AÑADIDO POR JYMMY TORRES
                    Dim nombres() As String = arrayCabecera(0).Split(CChar(" "))
                    'txtApellidosPat.Text = nombres(0)
                    'txtApellidosMat.Text = nombres(1)
                    'txtNombres.Text = nombres(2)
                    txtNombreCliente.Text = arrayCabecera(0)
                    ''AÑADIDO HASTA AQUI

                    txtMonto1.Text = montoPago
                    txtValorDeuda.Text = montoPago
                    txtRecibidoSoles.Text = montoPago
                    hidMonto.Value = montoPago
                    txtVuelto.Text = "0.00"
                    txtRecibidoUsd.Text = "0.00"
                    txtIdentificadorCliente.Value = arrayCabecera(1)
                    txtNumeroDocumentos.Text = arrayCabecera(4)
                    hidNumeroTrace.Value = arrayCabecera(6)

                    If arrayDetalle.Length <> CInt(txtNumeroDocumentos.Text) Then
                        Throw New Exception("Error. Datos Inválidos sobre la información de los Recibos a Pagar.")
                    End If

                    Recibos = arrayDetalle
                    strRecibos.Value = Join(Recibos, ",")

                    objFileLog.Log_WriteLog(pathFile1, strArchivo1, "MontoPago : " & montoPago.ToString)

                    If (CInt(montoPago) <> montoPago) Then
                        objFileLog.Log_WriteLog(pathFile1, strArchivo1, System.Configuration.ConfigurationSettings.AppSettings("MensajeErrorConfiguracionFacturacion"))
                        Throw New Exception(System.Configuration.ConfigurationSettings.AppSettings("MensajeErrorConfiguracionFacturacion"))
                    End If

                Else
                    Throw New Exception("Error. Retorno de Consulta Documentos a Pagar ST.")
                End If

            Else
                Throw New Exception("Consulta Documentos. " & Funciones.CheckStr(arrayMensaje(1)))
            End If

        Catch ex As Exception
            hidMensaje.Value = ex.Message
            Response.Redirect("bsqDocRecargaDTH.aspx?strMensaje=" & hidMensaje.Value)
        Finally
            ' Registro de Auditoria
            MensajeAudi = "Consulta Documentos de Pago Recarga Virtual DTH. " & PuntoVenta & "|" & Usuario & "|" & TipoIdentificador & "|" & TextoIdentificador
            RegistrarAuditoria(MensajeAudi, ConfigurationSettings.AppSettings("codTrxDocumentosDTH"))
        End Try

    End Sub

#End Region

#Region "Registrar Pedido Mssap"
    Private Function RegistrarPedidoMssap() As Boolean
        '***Por modificar / desarrollar:
        Dim monto_pedido_mssap As Double
        Dim dsPedido, dsReturn As DataSet
        Dim strCabecera, strDetalle As String
        Dim strNroPedido, strFactura As String
        Dim strNroDocCliente, strDocCliente As String
        Dim strRealizado, strEntrega, strNroContrato, strRefHistorico As String
        Dim dblValorDescuento As Double
        Dim arrayCabecera() As String
        Dim blnPedidoOK As Boolean = True

        '''AGREGADO POR JTN
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        '''AGREGADI HASTA AQUI

        Try
            ' Validacion del Vendedor y Punto de Venta
            '''CAMBIADO POR JTN
            'dsReturn = oVentas.Get_VerificaVendedor(PuntoVenta, Usuario) RFC DE VENTAS
            'dsReturn = objOffline.GetVerificaVendedor(PuntoVenta, Usuario)
            '''CAMBIADO HASTA AQUI
            '''VALIDACION RETIRADA POR PS
            'For i As Integer = 0 To dsReturn.Tables(0).Rows.Count - 1
            '    If dsReturn.Tables(0).Rows(i).Item("TYPE") = "E" Then
            '        Throw New Exception("Vendedor no autorizado.")
            '    End If
            'Next
            '''
            '''CAMBIADO POR JTN
            ' Validacion Cierre de Caja
            'dsReturn = oVentas.Get_ConsultaCierreCaja(PuntoVenta, Fecha, "", strRealizado)
            dsReturn = objOffline.GetConsultaCierreCaja(PuntoVenta, Fecha, "", strRealizado) ''
            '''CAMBIADO HASTA AQUI
            For i As Integer = 0 To dsReturn.Tables(0).Rows.Count - 1
                If dsReturn.Tables(0).Rows(i).Item("TYPE") = "E" Then
                    Throw New Exception("Consulta Cierre Caja. " & CheckStr(dsReturn.Tables(0).Rows(i).Item("MESSAGE")))
                End If
            Next
            If strRealizado = "S" Then
                Throw New Exception("No se puede realizar pedidos porque se realizó el cierre de caja.")
            End If

            Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
            Dim asignacionCajeroMensaje = objOffline.VerificarAsignacionCajero(Session("ALMACEN"), codUsuario)
            If asignacionCajeroMensaje <> String.Empty Then
                Throw New Exception(asignacionCajeroMensaje)
            End If

            ''' VERIFICACION DE CUADRE DE CAJA 05.02.2014 TS-JTN
            If (objOffline.CuadreCajeroRealizado(Session("ALMACEN"), codUsuario)) Then
                Throw New Exception("No puede realizar esta operacion, ya realizo cuadre de caja")
            End If
            ''' FIN VERIFICACION

            '''COMENTADO POR JYMMY TORRES
            ' Obtener Cadena para Generacion del Pedido
            'strCabecera = CadenaCabecera() ''LLAMA RFC SAP PARAMETROS VENTA
            'strDetalle = CadenaDetallePedido()
            'arrayCabecera = Split(strCabecera, ";")
            ''FIN COMENTADO 

            Dim strUsuario, strEstado As String
            strUsuario = Session("USUARIO")
            strEstado = "1"

            ''AGREGADO POR FFS INICIO
            'Dim facturas As String
            'Dim arrayFacturas As String
            'valoresPago.ClaseFactura = IIf(hidTipoDocCliente.Value = "06", ConfigurationSettings.AppSettings("constClaseFactura"), ConfigurationSettings.AppSettings("constClaseVoleta"))
            ''strUsuario = Session("USUARIO")
            'arrayFacturas = arrayFacturas & strUsuario & ";"
            'arrayFacturas = arrayFacturas & PuntoVenta & ";"
            'arrayFacturas = arrayFacturas & strEstado & ";"
            'arrayFacturas = arrayFacturas & valoresPago.ClaseFactura & ";"
            'facturas = arrayFacturas
            ''AGREGADO POR FFS FIN


            ' Pametros de Entrada
            param_in = "Cabecera:" & strCabecera & "|" & "Detalle:" & strDetalle

            '***IDG*******************************************************************'
            '***Registro del pedido en MSSAP:


            If Not Me.Request.QueryString("tipoDocCliente") Is Nothing Then
                If Me.Request.QueryString("tipoDocCliente") = "06" Then
                    strClasePedido = System.Configuration.ConfigurationSettings.AppSettings("PEDIC_CLASEFACTURA")
                    strDescrClasePedido = System.Configuration.ConfigurationSettings.AppSettings("PEDIC_DESCCLASEFACTURA")
                Else
                    strClasePedido = System.Configuration.ConfigurationSettings.AppSettings("PEDIC_CLASEBOLETA")                'cboClasePedido1.SelectedValue
                    strDescrClasePedido = System.Configuration.ConfigurationSettings.AppSettings("PEDIC_DESCCLASEBOLETA")
                End If
            End If

            Dim dateTimeValue As DateTime
            Dim cultureName As String = "es-PE"
            Dim strFecha As String
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Fechas:")
            strFecha = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- strFecha" & strFecha)
            Dim culture As CultureInfo = New CultureInfo(cultureName)
            dateTimeValue = Convert.ToDateTime(strFecha, culture)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- dateTimeValue: " & Funciones.CheckStr(dateTimeValue))

            '**Datos del Cliente**
            Dim arrCliente() As String
            Dim vCliente As String = ""
            Dim cNombre As String = ""
            Dim cApellidoPaterno As String = ""
            Dim cApellidoMaterno As String = ""
            Dim cRazonSocial As String = ""
            Dim cFechaNacimiento As String = ""
            Dim cCorreo As String = ""
            Dim cTelefono As String = Funciones.CheckStr("000000000")
            Dim cEstadoCivil As String = ""
            Dim cDireccion As String = "-"

            If Len(Trim(Session("datosCliente"))) > 0 Then
                vCliente = Session("datosCliente")
            End If

            If vCliente.Length > 0 Then
                arrCliente = vCliente.Split(";")
                cNombre = Funciones.CheckStr(arrCliente(3)) 'arrCliente(3)
                cApellidoPaterno = Funciones.CheckStr(arrCliente(4)) ' arrCliente(4)
                cApellidoMaterno = Funciones.CheckStr(arrCliente(5)) 'arrCliente(5)
                cRazonSocial = Funciones.CheckStr(arrCliente(6)) 'arrCliente(6)
                cFechaNacimiento = Funciones.CheckStr(arrCliente(7)) 'arrCliente(7)
                cTelefono = Funciones.CheckStr(arrCliente(8)) 'arrCliente(8)
                cCorreo = Funciones.CheckStr(arrCliente(9)) 'arrCliente(9)
                cEstadoCivil = IIf(Funciones.CheckStr(arrCliente(11)) = "", "S", Funciones.CheckStr(arrCliente(11)))
                cDireccion = IIf(Funciones.CheckStr(arrCliente(18)) = "", "-", Funciones.CheckStr(arrCliente(18)))
            End If

            '** PASO 1: consultamos los datos de la oficina para obtener el interlocutor
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Consultamos el : INTEV_CODINTERLOCUTOR")
            Dim INTEV_CODINTERLOCUTOR As String = objMsSAP.ConsultaPuntoVenta(Session("ALMACEN")).Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Consultamos el : " & INTEV_CODINTERLOCUTOR)

            '** Paso 2: Consultamos el OFICC_ORGVENTA
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Consultamos el : OFICC_ORGVENTA")
            Dim OFICC_ORGVENTA As String = objMsSAP.ConsultaOfina(INTEV_CODINTERLOCUTOR, "").Tables(0).Rows(0).Item("OFICC_ORGVENTA")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Consultamos el : " & OFICC_ORGVENTA)

            '** Paso 3: Registramos el Pedido
            objAct.RegistrarPedido(INTEV_CODINTERLOCUTOR, "", _
                                                                        ConfigurationSettings.AppSettings("PEDIC_TIPODOCUMENTO"), _
                                                                        OFICC_ORGVENTA, _
                                                                        ConfigurationSettings.AppSettings("PEDIC_CANALVENTA"), _
                                                                        ConfigurationSettings.AppSettings("PEDIC_SECTOR"), _
                                                                        ConfigurationSettings.AppSettings("TIPO_VENTA"), _
                                                                        Funciones.CheckDate(dateTimeValue), _
                                                                        ConfigurationSettings.AppSettings("PEDIV_MOTIVOPEDIDO"), _
                                                                        Funciones.CheckStr(strClasePedido), _
                                                                        Funciones.CheckStr(strDescrClasePedido), _
                                                                        "", _
                                                                        ConfigurationSettings.AppSettings("PEDIC_CLASEPEDIDO"), _
                                                                        ConfigurationSettings.AppSettings("PEDIC_CODTIPOOPERACION"), _
                                                                        ConfigurationSettings.AppSettings("PEDIV_DESCTIPOOPERACION_DTH"), _
                                                                        Funciones.CheckDate(dateTimeValue), _
                                                                        ConfigurationSettings.AppSettings("constAplicacion"), _
                                                                        Session("USUARIO"), _
                                                                        ConfigurationSettings.AppSettings("PEDIC_ESTADO"), _
                                                                        ConfigurationSettings.AppSettings("ISRENTA"), _
                                                                        ConfigurationSettings.AppSettings("PEDIDO_ALTA"), _
                                                                        ConfigurationSettings.AppSettings("PEDIV_UBIGEO"), _
                                                                        ConfigurationSettings.AppSettings("ESQUEMACALCULO_RV"), _
                                                                        Funciones.CheckStr(hidTipoDocCliente.Value), _
                                                                        Funciones.CheckStr(hidNroDocCliente.Value), _
                                                                        ConfigurationSettings.AppSettings("PEDIC_TIPOCLIENTE"), _
                                                                        cNombre, cApellidoPaterno, cApellidoMaterno, _
                                                                        dateTimeValue, cRazonSocial, cCorreo, _
                                                                        cTelefono, _
                                                                        cEstadoCivil, cDireccion, "0", _
                                                                        ConfigurationSettings.AppSettings("DISTRITO_CLIENTE_GENERICO"), _
                                                                        System.Configuration.ConfigurationSettings.AppSettings("CODDPTO_CLIENTE"), _
                                                                        ConfigurationSettings.AppSettings("PAIS_CLIENTE_GENERICO"), "", "", _
                                                                        "", "", "", _
                                                                        ConfigurationSettings.AppSettings("tipo_oficina"), _
                                                                        "", Session("USUARIO"), dateTimeValue, _
                                                                        Session("USUARIO"), dateTimeValue, "0", _
                                                                        K_PEDIN_NROPEDIDO, K_NROLOG, K_DESLOG)


            If K_PEDIN_NROPEDIDO <> 0 And K_DESLOG = "OK" Then

                Session("Pago") = True
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Session(Pago) =>" & Session("Pago") & "K_PEDIN_NROPEDIDO =>" & K_PEDIN_NROPEDIDO) 'INC000004584664
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "La cabecera del pedido de RV fue registrado correctamente  =>" & K_PEDIN_NROPEDIDO)
                '*** Paso 4: Registramos el detalle para el # peridod insertado.
                K_NROLOG = ""
                K_DESLOG = ""
                monto_pedido_mssap = ObtenerMontoTotalPagar()

                dtIGV = Session("Lista_Impuesto")
                If dtIGV.Rows.Count = 0 Then
                    Dim strScriptMensaje As String = "Error de Indicador"
                    strScriptMensaje = String.Format("<script language='javascript' type='text/javascript'>alert('No existe IGV configurado');</script>")
                    If (Not Page.IsStartupScriptRegistered("jsValidacion")) Then Page.RegisterStartupScript("jsValidacion", strScriptMensaje)
                    Exit Function
                End If
                Dim IGVactual As Decimal = 0.0
                For Each row As DataRow In dtIGV.Rows
                    If (Date.Now() >= CDate(row("impudFecIniVigencia").ToString.Trim) And Date.Now() <= CDate(row("impudFecFinVigencia").ToString.Trim) And CInt(row("impunTipDoc").ToString.Trim) = 0) Then
                        IGVactual = Math.Round(CDec(row("IGV").ToString.Trim()) / 100, 2)
                        Exit For
                    End If
                Next

                Dim recEfectiva# = CDbl(IIf(txtValorDeuda.Text.Trim.Length > 0, Funciones.CheckDbl(txtValorDeuda.Text), 0))
                Dim valVenta# = recEfectiva / (1 + Funciones.CheckDbl(IGVactual))
                valVenta = Math.Round(valVenta, 2)

                Try

                    '**** REGISTRAMOS EL DETALLE DEL PEDIDO ***************************************************************************'
                    '**** CK: Verificar los montos enviados en la recarga virtual: ***'
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Inicio del registro del Detalle de la RV - Recarga Virtual")
                    objAct.RegistraDetallePedido(K_PEDIN_NROPEDIDO, INTEV_CODINTERLOCUTOR, "", _
                    "", ConfigurationSettings.AppSettings("strCodArticuloDTH"), _
                      ConfigurationSettings.AppSettings("strDesArticuloDTH"), 1, _
                     valVenta, _
                     "000000000", "", 0, 0, 0, "", "", _
                     "00000" & Funciones.CheckStr(Session("USUARIO")), _
                    dateTimeValue, _
                    "00000" & Funciones.CheckStr(Session("USUARIO")), _
                    dateTimeValue, K_NROLOG, K_DESLOG)

                    '********************************************'**********************************************************************'
                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Error al registrar el detalle del pedido => " & K_PEDIN_NROPEDIDO)
                    Response.Write("<script>alert('Ocurrio un error al registrar el detalle del pedido. Comuniquelo a su centro de Soporte')</script>")
                    Exit Function
                End Try

                If K_DESLOG <> "OK" Then
                    Response.Write("<script>alert('Ocurrio un error al registrar el detalle del pedido. Comuniquelo a su centro de Soporte')</script>")
                    Exit Function
                End If

                '************************* Inicio Actualiza el Ajuste de redondeo***********************
                Dim strIdentificador As String = Funciones.CheckStr(K_PEDIN_NROPEDIDO)
                Try
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " - Inicio Actualiza el Ajuste de redondeo despues de guardar el detalle de la RV - Recarga DTH")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     Metodo: ActualizarAjusteRedondeo - SP: SSAPSU_ACTUALIZARPEDIDO")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -        IN Nro Pedido : " & Funciones.CheckStr(K_PEDIN_NROPEDIDO))

                    objAct.ActualizarAjusteRedondeo(K_PEDIN_NROPEDIDO)

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " - FIN Actualiza el Ajuste de redondeo despues de guardar el detalle de la RV - Recarga DTH")

                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " - Error al Actualiza el Ajuste del redondeo en el detalle del pedido de la RV => " & K_PEDIN_NROPEDIDO)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Err." & ex.Message.ToString)
                End Try

                '************************* Fin Actualiza el Ajuste de redondeo***********************


            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Error al registrar la cabecera del pedido - Recarga Virtual")
                Response.Write("<script>alert('Ocurrio un error al registrar el pedido, favor de volver a intentar.')</script>")
                Exit Function
            End If

            '***FDG*******************************************************************'

            'hidNroPedido.Value = strFactura
            hidNroPedido.Value = K_PEDIN_NROPEDIDO

        Catch ex As Exception
            blnPedidoOK = False
            hidMensaje.Value = "Ocurrió un error en la Creación Pedido. " & ex.Message
        Finally
            'Limpiar variables
            dsReturn = Nothing
            dsPedido = Nothing
            ' Registro de Auditoria
            'MensajeAudi = "Grabar Pedido en Sap Recarga Virtual DTH. " & PuntoVenta & "|" & Usuario & "|" & strNroPedido
            RegistrarAuditoria(MensajeAudi, ConfigurationSettings.AppSettings("codTrxGrabarPedido"))
            ' Registrar Log
            'param_out = "Nro.Pedido:" & hidNroPedido.Value
            'objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Inicio Creacion Pedido")
            'objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Input  : " & param_in)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Output : " & param_out)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Fin Creacion Pedido")
            'RegistrarLog(COD_TRX_PED_SAP, param_in, param_out, hidMensaje.Value)
        End Try
        Return blnPedidoOK
    End Function
#End Region


#Region "Registrar Pedido Sap"

    Private Function RegistrarPedido() As Boolean
        Dim dsPedido, dsReturn As DataSet
        Dim strCabecera, strDetalle As String
        Dim strNroPedido, strFactura As String
        Dim strNroDocCliente, strDocCliente As String
        Dim strRealizado, strEntrega, strNroContrato, strRefHistorico As String
        Dim dblValorDescuento As Double
        Dim arrayCabecera() As String
        Dim blnPedidoOK As Boolean = True

        '''AGREGADO POR JTN
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        '''AGREGADI HASTA AQUI

        Try
            ' Validacion del Vendedor y Punto de Venta
            '''CAMBIADO POR JTN
            'dsReturn = oVentas.Get_VerificaVendedor(PuntoVenta, Usuario) RFC DE VENTAS
            'dsReturn = objOffline.GetVerificaVendedor(PuntoVenta, Usuario)
            '''CAMBIADO HASTA AQUI
            '''VALIDACION RETIRADA POR PS
            'For i As Integer = 0 To dsReturn.Tables(0).Rows.Count - 1
            '    If dsReturn.Tables(0).Rows(i).Item("TYPE") = "E" Then
            '        Throw New Exception("Vendedor no autorizado.")
            '    End If
            'Next
            '''
            '''CAMBIADO POR JTN
            ' Validacion Cierre de Caja
            'dsReturn = oVentas.Get_ConsultaCierreCaja(PuntoVenta, Fecha, "", strRealizado)
            dsReturn = objOffline.GetConsultaCierreCaja(PuntoVenta, Fecha, "", strRealizado) ''
            '''CAMBIADO HASTA AQUI
            For i As Integer = 0 To dsReturn.Tables(0).Rows.Count - 1
                If dsReturn.Tables(0).Rows(i).Item("TYPE") = "E" Then
                    Throw New Exception("Consulta Cierre Caja. " & CheckStr(dsReturn.Tables(0).Rows(i).Item("MESSAGE")))
                End If
            Next
            If strRealizado = "S" Then
                Throw New Exception("No se puede realizar pedidos porque se realizó el cierre de caja.")
            End If

            Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
            Dim asignacionCajeroMensaje = objOffline.VerificarAsignacionCajero(Session("ALMACEN"), codUsuario)
            If asignacionCajeroMensaje <> String.Empty Then
                Throw New Exception(asignacionCajeroMensaje)
            End If

            ''' VERIFICACION DE CUADRE DE CAJA 05.02.2014 TS-JTN
            If (objOffline.CuadreCajeroRealizado(Session("ALMACEN"), codUsuario)) Then
                Throw New Exception("No puede realizar esta operacion, ya realizo cuadre de caja")
            End If
            ''' FIN VERIFICACION

            '''COMENTADO POR JYMMY TORRES
            ' Obtener Cadena para Generacion del Pedido
            'strCabecera = CadenaCabecera() ''LLAMA RFC SAP PARAMETROS VENTA
            'strDetalle = CadenaDetallePedido()
            'arrayCabecera = Split(strCabecera, ";")
            ''FIN COMENTADO 

            Dim strUsuario, strEstado As String
            strUsuario = Session("USUARIO")
            strEstado = "1"

            ''AGREGADO POR FFS INICIO
            'Dim facturas As String
            'Dim arrayFacturas As String
            'valoresPago.ClaseFactura = IIf(hidTipoDocCliente.Value = "06", ConfigurationSettings.AppSettings("constClaseFactura"), ConfigurationSettings.AppSettings("constClaseVoleta"))
            ''strUsuario = Session("USUARIO")
            'arrayFacturas = arrayFacturas & strUsuario & ";"
            'arrayFacturas = arrayFacturas & PuntoVenta & ";"
            'arrayFacturas = arrayFacturas & strEstado & ";"
            'arrayFacturas = arrayFacturas & valoresPago.ClaseFactura & ";"
            'facturas = arrayFacturas
            ''AGREGADO POR FFS FIN


            ' Pametros de Entrada
            param_in = "Cabecera:" & strCabecera & "|" & "Detalle:" & strDetalle



            '''
            '''CAMBIADO POR JTN
            ' Creacion de Pedido Sap
            'dsPedido = oVentas.Set_CreaPedidoA(strCabecera, strDetalle, "", "", arrayCabecera, _
            '                                    strEntrega, strFactura, strNroContrato, strNroDocCliente, _
            '                                    strNroPedido, strRefHistorico, strDocCliente, dblValorDescuento)

            'dsPedido = objOffline.CrearPedidoA(strCabecera, strDetalle, "", "", arrayCabecera, _
            '                                    strEntrega, strFactura, strNroContrato, strNroDocCliente, _
            '                                    strNroPedido, strRefHistorico, strDocCliente, dblValorDescuento) '''-->I

            codigoOperacion = objOffline.CreaPedidoFactura("", "", crearTramaCabeceraPedido, crearTramaDetallePedido, crearTramaPago)

            '''CAMBIADO HASTA AQUI
            '''
            'For i As Integer = 0 To dsPedido.Tables(1).Rows.Count - 1
            '    If dsPedido.Tables(1).Rows(i).Item(0) = "E" Then
            '        Throw New Exception("Error RFC. " & CheckStr(dsPedido.Tables(1).Rows(i).Item(3)))
            '    End If
            'Next

            ' Almacenar Nro Pedido y Factura generado de Sap
            'hidNroPedido.Value = strFactura
            'hidFactura.Value = strFactura

        Catch ex As Exception
            blnPedidoOK = False
            hidMensaje.Value = "Ocurrió un error en la Creación Pedido. " & ex.Message
        Finally
            'Limpiar variables
            dsReturn = Nothing
            dsPedido = Nothing
            ' Registro de Auditoria
            MensajeAudi = "Grabar Pedido en Sap Recarga Virtual DTH. " & PuntoVenta & "|" & Usuario & "|" & strNroPedido
            RegistrarAuditoria(MensajeAudi, ConfigurationSettings.AppSettings("codTrxGrabarPedido"))
            ' Registrar Log
            param_out = "Nro.Pedido:" & hidNroPedido.Value
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Inicio Creacion Pedido")
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Input  : " & param_in)
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Output : " & param_out)
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Fin Creacion Pedido")
            'RegistrarLog(COD_TRX_PED_SAP, param_in, param_out, hidMensaje.Value)
        End Try
        Return blnPedidoOK
    End Function

#End Region

#Region "Registrar Pago MSSAP"

    Private Function RegistrarDatosFlujoCaja() As Boolean
        '**Variables***'
        Dim dsDatosPedido As DataSet
        Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim objCajas As COM_SIC_Cajas.clsCajas  '** Flujo Caja **'
        Dim fechaCajas As String = ""
        Dim flagFlujoCaja As Boolean = False

        Dim resCajas As Integer = 0
        Dim P_CODERR As String = ""
        Dim P_MSGERR As String = ""
        Dim strNombreCaja As String = ""
        Dim P_ID_TI_VENTAS_FACT As String = ""
        '*********************************'

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Inicio del Registro Flujo Caja DTH")
        strIdentifyLog = Funciones.CheckStr(K_PEDIN_NROPEDIDO)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Nùmero pedido : " & strIdentifyLog.ToString)

        '**LOG EJMPLO: ********************************************************'
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "")
        '**********************************************************************'


        Try
            'fechaCajas = Format(Now.Year, "0000").ToString.Trim & Format(Now.Month, "00").ToString.Trim & Format(Now.Day, "00").ToString.Trim
            'objFileLog.Log_WriteLog(pathFile, strArchivo, "Fecha : " & fechaCajas.ToString)

            If K_PEDIN_NROPEDIDO > 0 Then
                dsDatosPedido = objConsultaMsSap.ConsultaPedido(K_PEDIN_NROPEDIDO, "", "")
                If Not dsDatosPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO") Is Nothing Then
                    fechaCajas = CDate(dsDatosPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO")).ToString("yyyyMMdd")
                Else
                    fechaCajas = Format(Now.Year, "0000").ToString.Trim & Format(Now.Month, "00").ToString.Trim & Format(Now.Day, "00").ToString.Trim
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fecha : " & fechaCajas.ToString)

                Dim PEDIC_CLASEFACTURA As String = ""
                Try
                    objCajas = New COM_SIC_Cajas.clsCajas   '** Flujo Caja **' Registro de pago: TABLA "TI_VENTAS_FACT"

                    PEDIC_CLASEFACTURA = System.Configuration.ConfigurationSettings.AppSettings("DESC_BOLETA_RDTH")

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio método : RegistrarPago")
                    resCajas = objCajas.RegistrarPago(Session("ALMACEN"), fechaCajas, _
                                                   Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & Session("USUARIO")), 10), _
                                                   dsDatosPedido.Tables(0).Rows(0).Item("PEDIV_DESCCLASEFACTURA"), _
                                                   K_PEDIN_NROPEDIDO, _
                                                   Obtener_serie_correlativo(K_CU_CORRELATIVOFE, "6"), _
                                                   Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & dsDatosPedido.Tables(0).Rows(0).Item("VENDEDOR")), 10), _
                                                   System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                                   PEDIC_CLASEFACTURA, _
                                                   Funciones.CheckInt64(dsDatosPedido.Tables(0).Rows(0).Item("DEPEN_NROCUOTA")), _
                                                   Funciones.CheckDbl(dsDatosPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")), _
                                                    IIf(dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA") <> ConfigurationSettings.AppSettings("strTipDoc"), Funciones.CheckDbl(dsDatosPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")), 0), _
                                                    "", _
                                                     System.Configuration.ConfigurationSettings.AppSettings("ESTADO_VENTA_FLUJOCAJA"), _
                                                     Funciones.CheckStr(Right(System.Net.Dns.GetHostName, 2)), _
                                                    System.Configuration.ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), _
                                                    P_ID_TI_VENTAS_FACT, P_MSGERR, hidTextIdentificador.Value, numeroOperacionPago)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin método : RegistrarPago")
                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al tratar de ejecutar el método:  RegistrarPago")
                End Try


                If P_ID_TI_VENTAS_FACT > 0 Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Código generado en la tabla TI_VENTAS_FACT=> " & P_ID_TI_VENTAS_FACT.ToString)

                    '2. Registro RegistrarPagoDetalle
                    If Funciones.CheckStr(dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")) <> ConfigurationSettings.AppSettings("strTipDoc") Then '** Las notas de crèdito no van a formar parte de este proceso **'
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia el proceso para el RegistrarPagoDetalle")

                        Dim strMonto As String = "" 'PROY-27440
                        Dim strTipoDocPago As String = "" 'PROY-27440
                        For i As Integer = 1 To hidNroFilas.Value
                            Dim txtMonto As Double = 0
                            Dim cboTipDocumento As String = ""
                            Dim cboDescTipDocumento As String = ""
 'PROY-27440 - INI
                            Dim objTxt As TextBox
                            objTxt = CType(Me.FindControl("txtMonto" & i), TextBox)
                            strMonto = "" : strMonto = Trim(objTxt.Text)

                            Dim objCbo As DropDownList
                            objCbo = CType(Me.FindControl("ddlTipoDocumento" & i), DropDownList)
                            strTipoDocPago = "" : strTipoDocPago = Trim(objCbo.SelectedValue)

                            cboTipDocumento = strTipoDocPago
                            txtMonto = Double.Parse(strMonto)
'PROY-27440 - FIN
                            cboDescTipDocumento = DevuelteTipoDescPago(cboTipDocumento)
                            resCajas = 0

                            resCajas = objCajas.RegistrarPagoDetalle(P_ID_TI_VENTAS_FACT, fechaCajas, _
                                K_PEDIN_NROPEDIDO, _
                                Obtener_serie_correlativo(K_CU_CORRELATIVOFE, "6"), PEDIC_CLASEFACTURA, _
                                cboTipDocumento, _
                               "", _
                               txtMonto, _
                               System.Configuration.ConfigurationSettings.AppSettings("ESTADO_VENTA_FLUJOCAJA"), _
                               System.Configuration.ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), _
                               P_MSGERR)

                        Next
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Termina el proceso para el RegistrarPagoDetalle")
                        '* FIN 2 *'

                        '* 3. RegistrarVentaDetalle, consulto el pedido para asegurar la data
                        '** Registro de los MATERIALES(TI_MATER_FACT):
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia el proceso para el RegistrarVentaDetalle.")
                        If Not dsDatosPedido Is Nothing Then
                            If dsDatosPedido.Tables(1).Rows.Count > 0 Then
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia el proceso para registrar la venta en el detalle.")
                                resCajas = 0
                                For k As Integer = 0 To dsDatosPedido.Tables(1).Rows.Count - 1
                                    resCajas = objCajas.RegistrarVentaDetalle(Session("ALMACEN"), fechaCajas, _
                                                                                Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & Session("USUARIO")), 10), _
                                                                                dsDatosPedido.Tables(0).Rows(0).Item("PEDIV_DESCCLASEFACTURA"), _
                                                                                K_PEDIN_NROPEDIDO, Obtener_serie_correlativo(K_CU_CORRELATIVOFE, "6"), _
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
                                    Exit Function
                                Else
                                    '**********************************************
                                    '*** 4. Registramos RegistrarDetalleCuota   ***'
                                    '*** DETALLA EL NUMERO DE CUOTAS DEL PEDIDO ***'
                                    '**********************************************
                                    resCajas = 0
                                    If dsDatosPedido.Tables(0).Rows.Count > 0 Then
                                        '**REGISTRA LAS CUTOAS DEL PEDIDO SI EXISTIERA **'
                                        If dsDatosPedido.Tables(0).Rows(0).Item("DEPEN_NROCUOTA") > 0 Then
                                            resCajas = objCajas.RegistrarDetalleCuota(P_ID_TI_VENTAS_FACT, fechaCajas, _
                                                                                      Funciones.CheckStr(K_PEDIN_NROPEDIDO), _
                                                                                      Obtener_serie_correlativo(K_CU_CORRELATIVOFE, "6"), dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"), _
                                                                                      dsDatosPedido.Tables(0).Rows(0).Item("DEPEN_NROCUOTA"), _
                                                                                      Funciones.CheckDbl(dsDatosPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")), _
                                                                                      System.Configuration.ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), _
                                                                                      P_MSGERR)
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


                    flagFlujoCaja = True
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Error devuelto : " & P_MSGERR.ToString)
                    Response.Write("<script>alert('" & P_MSGERR & "')</script>")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Error, no se realizo el registro en TI_VENTAS_FACT")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Se realiza el rollback al mòdulo.")
                    DeshacerCambiosPagos(K_PAGON_IDPAGO)
                    Exit Function
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Registro datos del Flujo Caja")
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - No se realizo el registro de datos en el flujo de caja DTH.")
            End If

            Return flagFlujoCaja

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Ocurrio un error al tratar de registrar los datos del flujo caja DTH.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Err. " & ex.Message.ToString)
        End Try
    End Function
    '	inicio INC000003270254 
    Private Function f_NumeroFilas() As Integer

        Dim contador As Integer
        contador = 0

        If (Funciones.CheckStr(txtMonto1.Text) <> "") Then
            contador = contador + 1
        End If

        If (Funciones.CheckStr(txtMonto2.Text) <> "") Then
            contador = contador + 1
        End If

        If (Funciones.CheckStr(txtMonto3.Text) <> "") Then
            contador = contador + 1
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(hidNroRecarga.Value) & " - contador : " & Funciones.CheckStr(contador))

        Return contador
    End Function
    'fin	INC000003270254 

    Private Function RegistrarPagoMssap() As Boolean
        '**Por desarrolla/modifica.
        Dim dsConfigCaja, dsReturn As DataSet
        Dim dsPedido As DataSet
        Dim dblEfectivoCaja, dblTolerancia As Double
        Dim dblMaxDisponible, dblEfectivo, dblTotalTarjeta As Double
        Dim strCompleto, strCorrelativo As String
        Dim strNroSunat, strNroAsignaSUNAT, strDetallePago As String
        Dim strDocSunat As String
        Dim param_in As String
        Dim blnSunat As Boolean
        Dim drDocSapxPagar As DataRow
        Dim blnPagoSapOK As Boolean = True


        '************************************************************************************
        '*** Variables para WS pagos: ******************************************************* 
        '************************************************************************************ 
        Dim objClsPagosWS As New COM_SIC_Activaciones.clsPagosWS
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim dsConsultaDoc As DataSet
        Dim arrValor As String()
        Dim Origen As String = "3"
        Dim FE_NRO_ERROR As Int16 = 0
        Dim FE_DES_ERROR As String = ""
        Dim K_PAGOV_RECUP_CORRELATIVO As String = ""
        Dim flag_paperless As String = ""
        Dim correlativo_recu As String = ""
        '        Dim K_CU_CORRELATIVOFE As String = ""
        Dim consultaDato As DataSet
        Dim K_CORRC_TIPODOC_REFERENCIA As String = ""
        '*************************************************************************************


        objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Inicio Registrar Pago BD SICAR")
        '''AGREGADO POR JTN
        Dim objOffLine As New COM_SIC_OffLine.clsOffline
        '''AGREGADO HASTA AQUI
        Try
            ' Obtener Documento Sap x Pagar
            'drDocSapxPagar = ObtenerDocSapxPagar() ''××××××××--->
            ''TODO: CAMBIADO POR JYMMY TORRES
            'strDocSunat = drDocSapxPagar.Item("XBLNR")
            strDocSunat = valoresPago.ReferenciaSunat
            ''CAMBIADO HASTA AQUI
            ' Validar Configuracion de Caja para el Punto de Venta
            dsConfigCaja = oCajas.FP_Get_ListaParamOficina(Canal, CodAplicacion, PuntoVenta) '''NO SAP
            ''TODO: CAMBIADO POR JYMMY TORRES
            'dblEfectivoCaja = Funciones.CheckDbl(oCajas.FP_CalculaEfectivo(PuntoVenta, Usuario, drDocSapxPagar.Item("FKDAT")))
            dblEfectivoCaja = Funciones.CheckDbl(oCajas.FP_CalculaEfectivo(PuntoVenta, Usuario, valoresPago.FechaPago))
            ''CAMBIADO HASTA AQUI

            If IsNothing(dsConfigCaja) Then
                Throw New Exception("No existe configuracion de caja para este Punto de Venta.")
            End If

            dblTolerancia = Funciones.CheckDbl(dsConfigCaja.Tables(0).Rows(0)("CAJA_TOLERANCIA_SOL"))
            dblMaxDisponible = Funciones.CheckDbl(dsConfigCaja.Tables(0).Rows(0)("CAJA_MAX_DISP_SOL"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: RegistrarPagoMssap(), dblEfectivoCaja: {1}", CheckStr(hidNroRecarga.Value), CheckStr(dblEfectivoCaja)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: RegistrarPagoMssap(), dblMaxDisponible: {1}", CheckStr(hidNroRecarga.Value), CheckStr(dblMaxDisponible)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: RegistrarPagoMssap(), dblTolerancia: {1}", CheckStr(hidNroRecarga.Value), CheckStr(dblTolerancia)))
          
            If dblEfectivoCaja >= dblMaxDisponible + dblTolerancia Then
                Throw New Exception("Ha alcanzado su maximo disponible de efectivo en caja.")
            Else
                '' TODO: CAMBIADO POR JYMMY TORRES
                'blnSunat = (drDocSapxPagar.Item("XBLNR") = "" Or drDocSapxPagar.Item("XBLNR") = "0000000000000000")
                Dim dsDatosCaja As DataSet = objOffLine.Get_CajaOficinas(Session("ALMACEN"))


                Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
                Dim codigoCaja As String = objOffLine.ObtenerCaja(codUsuario, Session("ALMACEN"))



                dblEfectivo = 0.0
                dblTotalTarjeta = 0.0
                strDetallePago = ""

                objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " &  "hidNroFilas: " & Funciones.CheckStr(hidNroFilas.Value))

                '	INC000003270254 
                If (Funciones.CheckStr(hidNroFilas.Value) = "") Then
                    hidNroFilas.Value = f_NumeroFilas()
                End If
                '	INC000003270254 

                objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value &   "- " & "INC000003270254 - hidNroFilas: " & Funciones.CheckStr(hidNroFilas.Value)) '	INC000003270254 


                'PROY-27440 INI
                Dim strMonto As String = ""
                Dim strTipoDocPago As String = ""
                'PROY-27440 FIN
                For i As Integer = 1 To hidNroFilas.Value

                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: RegistrarPagoMssap(), For {1} To {2}", CheckStr(hidNroRecarga.Value), CheckStr(i), CheckStr(hidNroFilas.Value)))
                    Dim ddlTipoDocumento As String
                    Dim txtMonto As Double
'PROY-27440 INI
                    Dim objTxt As TextBox
                    objTxt = CType(Me.FindControl("txtMonto" & i), TextBox)
                    strMonto = "" : strMonto = Trim(objTxt.Text)

                    Dim objCbo As DropDownList
                    objCbo = CType(Me.FindControl("ddlTipoDocumento" & i), DropDownList)
                    strTipoDocPago = "" : strTipoDocPago = Trim(objCbo.SelectedValue)


                    txtMonto = Double.Parse(strMonto)
                    ddlTipoDocumento = strTipoDocPago
'PROY-27440 FIN
                    strDetallePago = "TC01" 'crearTramaCabeceraPedido.Split(CChar(";"))(1) + strDetallePago & ";"
                    strDetallePago = strDetallePago & PuntoVenta & ";"
                    strDetallePago = strDetallePago & ddlTipoDocumento & ";"
                    strDetallePago = strDetallePago & ";"
                    strDetallePago = strDetallePago & ";"
                    strDetallePago = strDetallePago & txtMonto & ";"
                    strDetallePago = strDetallePago & "PEN" & ";"
                    strDetallePago = strDetallePago & ";"
                    strDetallePago = strDetallePago & strNroAsignaSUNAT & ";"
                    strDetallePago = strDetallePago & ddlTipoDocumento & ";"
                    '' TODO: CAMBIADO POR JYMMY TORRES
                    'strDetallePago = strDetallePago & drDocSapxPagar.Item("FKDAT") & ";
                    strDetallePago = strDetallePago & valoresPago.FechaPago & ";"""
                    ''CAMBIADO HASTA AQUI
                    strDetallePago = strDetallePago & ";"
                    strDetallePago = strDetallePago & ";"

                    ' Obtener Monto Total en Efectivo
                    If ddlTipoDocumento = "ZEFE" Then
                        dblEfectivo += txtMonto
                    End If

                    If i < CInt(hidNroFilas.Value) Then
                        strDetallePago = strDetallePago & "|"
                    End If

                    ' Obtener Monto Total en Tarjetas 
                    If ddlTipoDocumento <> "ZEFE" And ddlTipoDocumento <> "ZEAM" And ddlTipoDocumento <> "ZEOV" And ddlTipoDocumento <> "TDPP" And ddlTipoDocumento <> "ZDEL" Then
                        dblTotalTarjeta += txtMonto
                    End If
                Next

                objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "strDetallePago: " & strDetallePago)

                If (dblEfectivo + dblEfectivoCaja) >= (dblTolerancia + dblMaxDisponible) Then
                    Throw New Exception("Este pago excede su tolerancia de monto máximo de efectivo en caja.")
                End If

                blnSunat = (valoresPago.ReferenciaSunat = "" Or valoresPago.ReferenciaSunat = "0000000000000000")


                ' Modificacion registro de importe recibido y vuelto
                Dim dblMontoSoles, dblMontoUSD, dblVuelto As Double
                Dim totalDeuda#, igv#
                Try
                    ' Se inserta el efectivo obtenido
                    Try
                        oCajas.FP_InsertaEfectivo(PuntoVenta, Usuario, dblEfectivo)
                    Catch ex As Exception

                    End Try
                    ''NO RFC

                    ' Escribir en diario electronico
                    Dim intOperacion As Integer
                    '''CAMBIADO POR JTN
                    'dsReturn = oPagos.Get_ConsultaPedido("", PuntoVenta, drDocSapxPagar.Item("VBELN"), "") '''rfc
                    'dsReturn = objOffLine.GetConsultaPedido("", PuntoVenta, valoresPago.NumeroDocumento, "")
                    '''CAMBIADO HASTA AQUI

                    'If Not IsNothing(dsReturn) Then

                    dblMontoSoles = Funciones.CheckDbl(txtRecibidoSoles.Text)
                    dblMontoUSD = Funciones.CheckDbl(txtRecibidoUsd.Text)
                    dblVuelto = Funciones.CheckDbl(txtVuelto.Text)




                    totalDeuda = Funciones.CheckDbl(txtValorDeuda.Text)
                    igv = crearTramaDetallePedido.Split(CChar(";"))(10)


                    intOperacion = oCajas.FP_Cab_Oper(Canal, PuntoVenta, CodAplicacion, Usuario, _
                                                        valoresPago.ClaseFactura, _
                                                        "", _
                                                        valoresPago.ClaseFactura, _
                                                        valoresPago.NumeroDocumento, _
                                                        valoresPago.ReferenciaSunat, _
                                                        0, _
                                                        igv, _
                                                        totalDeuda, "P", _
                                                        dblMontoSoles, dblMontoUSD, dblVuelto) ''' NO SAP

                    objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "intOperacion: " & intOperacion)

                    'For i As Integer = 0 To dsReturn.Tables(1).Rows.Count - 1
                    '    oCajas.FP_Det_Oper(intOperacion, i + 1, _
                    '                        "", _
                    '                        "", _
                    '                        hidTextIdentificador.Value, _
                    '                        1, _
                    '                        0, _
                    '                        igv, _
                    '                        totalDeuda)
                    'Next

                    Dim arrayDetPago() As String
                    Dim arrayLinDetPago() As String

                    arrayDetPago = Split(strDetallePago, "|")

                    objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "arrayDetPago.Length: " & arrayDetPago.Length())

                    Dim strNroTarjta As String = "" 'PROY-27440
                    For i As Integer = 0 To hidNroFilas.Value - 1
                        Try
                            'PROY-27440 - INI 
                            'INC000004396325
                            objFileLog.Log_WriteLog(pathFilePos, strArchivo, hidNroRecarga.Value & " INC000004396325 - arrayDetPago[" & i & "]: " & arrayDetPago(i))
                            'INC000004396325
                            Dim objTxt As TextBox
                            objTxt = CType(Me.FindControl("txtDocumento" & i + 1), TextBox)
                            strNroTarjta = "" : strNroTarjta = Trim(objTxt.Text)
                            'PROY-27440 - FIN
                            arrayLinDetPago = Split(arrayDetPago(i), ";")
                            oCajas.FP_Pag_Oper(intOperacion, i + 1, arrayLinDetPago(2), strNroTarjta, Funciones.CheckDbl(arrayLinDetPago(5))) 'PROY-27440
                        Catch ex As Exception
                            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Error FP_Pag_Oper: " & ex.Message.ToString())
                        End Try
                    Next

                    'End If


                    '************************************************************************************************************'
                    '***REGISTRAMOS EL PAGO - MSSATP:

                    Dim txtMonto As Double
                    Dim cboTipDocumento As String
                    Dim cboDescTipDocumento As String
                    K_NROLOG = ""
                    K_DESLOG = ""

                    '*** DATOS PEDIDOS: *****************************************
                    dsPedido = objMsSAP.ConsultaPedido(K_PEDIN_NROPEDIDO, "", "")
                    '**************************************************************
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: RegistrarPagoMssap(), K_PEDIN_NROPEDIDO: {1}", CheckStr(hidNroRecarga.Value), CheckStr(K_PEDIN_NROPEDIDO)))
                    If K_PEDIN_NROPEDIDO <> 0 Then
                        'objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(K_PEDIN_NROPEDIDO).ToString & "- " & "Inicio del registro del pago")
                        Try
                            objAct.RegistrarPago(K_PEDIN_NROPEDIDO, DBNull.Value, _
                                                System.Configuration.ConfigurationSettings.AppSettings("ESTADO_PAG"), _
                                                "CAJA PRUEBA", _
                                                0, _
                                                System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                                Session("USUARIO"), _
                                                DBNull.Value, _
                                                Session("USUARIO"), _
                                                DBNull.Value, _
                                                K_NROLOG, _
                                                K_DESLOG, _
                                                K_PAGON_IDPAGO, _
                                                K_PAGOC_CORRELATIVO)
                        Catch ex As Exception
                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(K_PEDIN_NROPEDIDO).ToString & "- " & "Error al registrar el pago.")
                        End Try



                        'objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(K_PEDIN_NROPEDIDO).ToString & "- " & "Fin del registro del pago")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: RegistrarPagoMssap(), K_PAGON_IDPAGO: {1}", CheckStr(hidNroRecarga.Value), CheckStr(K_PAGON_IDPAGO)))
                        If K_PAGON_IDPAGO <> 0 Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: RegistrarPagoMssap(), K_DESLOG: {1}", CheckStr(hidNroRecarga.Value), CheckStr(K_DESLOG)))
                            If K_DESLOG = "OK" Then
                                Session.Add("msgErrorGenerarSot", "Registro de Pago Correctamente")

                                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'
                                For i As Integer = 1 To hidNroFilas.Value
'PROY-27440 - INI
                                    Dim objTxt As TextBox
                                    objTxt = CType(Me.FindControl("txtMonto" & i), TextBox)
                                    strMonto = "" : strMonto = Trim(objTxt.Text)

                                    Dim objCbo As DropDownList
                                    objCbo = CType(Me.FindControl("ddlTipoDocumento" & i), DropDownList)
                                    strTipoDocPago = "" : strTipoDocPago = Trim(objCbo.SelectedValue)
                                    cboTipDocumento = strTipoDocPago
                                    txtMonto = Double.Parse(strMonto)
'PROY-27440 - FIN
                                    cboDescTipDocumento = DevuelteTipoDescPago(cboTipDocumento)

                                    objAct.RegistrarDetallePago(K_PAGON_IDPAGO, _
                                                                        cboTipDocumento, cboDescTipDocumento, _
                                                                        System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                                                        txtMonto, Session("USUARIO"), DBNull.Value, _
                                                                        Session("USUARIO"), DBNull.Value, _
                                                                        "", 0, _
                                                                         K_NROLOG_DET, K_DESLOG_DET)
                                Next
                                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'
                                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: RegistrarPagoMssap(), K_DESLOG_DET: {1}", CheckStr(hidNroRecarga.Value), CheckStr(K_DESLOG_DET)))
                                If K_DESLOG_DET = "OK" Then

                                    Try
                                        '============================================================================================================================================================================================================================
                                        'CORRELATIVO Y ACTUALIZACIÒN DEL PAGO PEDIDO'
                                        '============================================================================================================================================================================================================================
                                        K_NROLOG = ""
                                        K_DESLOG = ""

                                        dsConsultaDoc = objCajas.ConsultaDocPagos(K_PEDIN_NROPEDIDO, Origen)
                                        If dsConsultaDoc.Tables.Count > 0 AndAlso dsConsultaDoc.Tables(0).Rows.Count > 0 Then
                                            flag_paperless = Funciones.CheckStr(dsConsultaDoc.Tables(0).Rows(0).Item("FLAG_PAPERLESS"))
                                            correlativo_recu = Funciones.CheckStr(dsConsultaDoc.Tables(0).Rows(0).Item("REFERENCIA"))
                                            arrValor = correlativo_recu.Trim.ToString.Split("|")
                                            If arrValor(1).ToString <> "" Then
                                                K_PAGOV_RECUP_CORRELATIVO = arrValor(1)
                                            End If
                                        End If

                                        If K_PAGOV_RECUP_CORRELATIVO <> "" Then
                                            K_CU_CORRELATIVOFE = K_PAGOV_RECUP_CORRELATIVO
                                        Else
                                            '++ IDG: ++++++++++++++++++++++++++++++++++!
                                            If Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")) = ConfigurationSettings.AppSettings("strTipDoc") Then
                                                consultaDato = objMsSAP.ConsultaPedidoXCorrelativo(dsPedido.Tables(0).Rows(0).Item("PEDIV_NROREFNCND"), "", "", "")
                                                K_CORRC_TIPODOC_REFERENCIA = Microsoft.VisualBasic.Right(Funciones.CheckStr(consultaDato.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")), 2)
                                            End If
                                            objAct.CalculoCorrelativoFE(dsPedido.Tables(0).Rows(0).Item("OFICV_CODOFICINA"), _
                                                                                Microsoft.VisualBasic.Right(Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")).ToString, 2), _
                                                                                K_CORRC_TIPODOC_REFERENCIA, FE_NRO_ERROR, FE_DES_ERROR, K_CU_CORRELATIVOFE)
                                            '++ FDG ++++++++++++++++++++++++++++++++++++++!
                                        End If
                                        '***objAct.CalculoCorrelativoSUNAT(dsPedido.Tables(0).Rows(0).Item("OFICV_CODOFICINA"), Microsoft.VisualBasic.Right(Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")).ToString, 2), K_PEDIN_NROPEDIDO, K_PAGOV_CORRELATIVO)
                                        '***//

                                        If K_CU_CORRELATIVOFE <> "" Then
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(K_PEDIN_NROPEDIDO).ToString & "- Inicia mètodo : objAct.FP_Grabar_PagoBF")
                                            '*** Inicio Bloque A  ****'
                                            If K_PAGOV_RECUP_CORRELATIVO = "" Then
                                                '***Grabar el correlativo el correlativo Nuevo en SICAR_TRS_PAGOS**
                                                'Dim Origen As String = "3" 'Valor para validar que sea SICAR 6.0
                                                Dim corrCompleto As String = ""
                                                Dim updCorrCompleto As String = ""

                                                Dim resultado As String = objAct.FP_Grabar_PagoBF(K_PEDIN_NROPEDIDO, _
                                                       Microsoft.VisualBasic.Right(Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")), 2), _
                                                       Obtener_serie_correlativo(K_CU_CORRELATIVOFE, "1"), _
                                                       Obtener_serie_correlativo(K_CU_CORRELATIVOFE, "2"), _
                                                       CurrentUser.ToString, _
                                                       Origen, _
                                                       Session("ALMACEN"))
                                                If resultado = "" Then
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out Resultado           :" & "No se ha Grabado los Datos del Pago.")
                                                Else
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out Resultado         :" & "Registro de Datos del pago Exitoso.")
                                                    If (Microsoft.VisualBasic.Right(Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")), 2) = "01" Or Microsoft.VisualBasic.Right(Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")), 2) = "03") Then
                                                        corrCompleto = "00" & "|" & K_CU_CORRELATIVOFE
                                                    Else
                                                        corrCompleto = Microsoft.VisualBasic.Right(Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")), 2) & "|" & K_CU_CORRELATIVOFE
                                                    End If
                                                    updCorrCompleto = objCajas.SP_UPD_FLAG_PAPER(K_PEDIN_NROPEDIDO, "V", corrCompleto)
                                                End If
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FP_Grabar_PagoBF - Fin del Registro de datos de la Boleta o Factura.")
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")
                                                '***********************************************'
                                            End If
                                            '** Fin Bloque A  ***'


                                            '****ACTUALIZACIÒN DEL PAGO *****************************************************************************************************************'
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(K_PEDIN_NROPEDIDO).ToString & "- Fin mètodo : objTrsMsSap.ActualizarPagodelPedido")
                                            objAct.ActualizarPagodelPedido(K_PEDIN_NROPEDIDO, _
                                                                                K_PAGON_IDPAGO, _
                                                                                    K_CU_CORRELATIVOFE, _
                                                                                        dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"), _
                                                                                        dsPedido.Tables(0).Rows(0).Item("OFICV_CODOFICINA"), _
                                                                                        K_NROLOG, K_DESLOG)

                                            '*********************************************************************************************************************'

                                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(K_PEDIN_NROPEDIDO).ToString & "- Fin mètodo : objTrsMsSap.ActualizarPagodelPedido")
                                        Else
                                            '** RollBack en el Correlativo ************************'
                                            DeshacerCambiosPagos(K_PAGON_IDPAGO)
                                            blnPagoSapOK = False
                                            '***********************************************************'
                                        End If

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: RegistrarPagoMssap(), K_DESLOG: {1}", CheckStr(hidNroRecarga.Value), CheckStr(K_DESLOG)))
                                        If K_DESLOG = "OK" Then
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(K_PEDIN_NROPEDIDO).ToString & "- " & "  Registro de forma correcta el detalle del Pago :" & K_PAGON_IDPAGO)
                                            '·····························································································
                                            If flag_paperless = "" Then
                                                Try

                                                    Paperless(K_CU_CORRELATIVOFE, K_PEDIN_NROPEDIDO, K_PAGON_IDPAGO, Origen)

                                                Catch ex As Exception

                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Error en envio a Paperless - Rollback Pago y Pedido")
                                                    DeshacerCambiosPagos(K_PAGON_IDPAGO)
                                                    '***Anula Pedido  
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio Anular Pedido - SP: SSAPSU_ANULARPEDIDO")
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Nro Pedido: " & K_PEDIN_NROPEDIDO)
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Motivo: " & "STO")
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Estado: " & "ANU")
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Tipo de Oficina: " & ConfigurationSettings.AppSettings("PEDIC_CLASEPEDIDO"))

                                                    objAct.AnularPedido(K_PEDIN_NROPEDIDO, "STO", "ANU", ConfigurationSettings.AppSettings("PEDIC_CLASEPEDIDO"))

                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "FIN Anular Pedido - SP: SSAPSU_ANULARPEDIDO")
                                                    '**Fin Anulacion Pedido

                                                End Try

                                            End If
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(K_PEDIN_NROPEDIDO).ToString & "- " & "  Registro de forma correcta el detalle del Pago :" & K_PAGON_IDPAGO)
                                            '·····························································································
                                            '***DISPARAMOS EL SAP DE PAGOS *********'
                                            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: RegistrarPagoMssap(), LanzarServicio: {1}", CheckStr(hidNroRecarga.Value), CheckStr(ConfigurationSettings.AppSettings("LanzarServicio"))))
                                        
                                            If ConfigurationSettings.AppSettings("LanzarServicio") = "0" Then
                                                objClsPagosWS.RegistrarPagoSap(Funciones.CheckInt64(K_PEDIN_NROPEDIDO), Funciones.CheckInt64(K_PAGON_IDPAGO), CurrentUser, CurrentTerminal, K_COD_RESPUESTA, K_MSJ_RESPUESTA, K_ID_TRANSACCION, Session("USUARIO"))
                                            End If
                                            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                        Else
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(K_PEDIN_NROPEDIDO).ToString & "- " & "  No registro de forma correcta el detalle del Pago :" & K_PAGON_IDPAGO)
                                            '** RollBack en la actualizaciòn de estados del pago ************************'
                                            DeshacerCambiosPagos(K_PAGON_IDPAGO)
                                            blnPagoSapOK = False
                                            '*************************************************'
                                        End If
                                        '============================================================================================================================================================================================================================
                                    Catch ex As Exception
                                        Session.Add("msgErrorGenerarSot", "Ocurrio un error al tratar de obtener el Correlativo")
                                        'Response.Redirect("PoolPagos.aspx")
                                    End Try
                                Else
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(K_PEDIN_NROPEDIDO).ToString & "- " & "  Ocurrio un error al tratar de registrar el detalle del pedido :" & K_PAGON_IDPAGO)
                                    '** RollBack al registro del pago ************************'
                                    DeshacerCambiosPagos(K_PAGON_IDPAGO)
                                    blnPagoSapOK = False
                                    '*************************************************'
                                End If
                                'Session.Add("msgErrorGenerarSot", "Registro de Pago Correctamente")
                            Else
                                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(K_PEDIN_NROPEDIDO).ToString & "- " & "  El registro del pago :" & K_PAGON_IDPAGO & " se ejecuto con errores.")

                                '** RollBack al registro del pago ************************'
                                DeshacerCambiosPagos(K_PAGON_IDPAGO)
                                blnPagoSapOK = False
                                '*************************************************'
                            End If
                        End If
                    End If
                    '************************************************************************************************************'


                Catch ex As Exception
                    ' Registro de Log
                    param_out = ""
                    param_in = "Monto:" & dblMontoSoles & "|" & "Vuelto:" & dblVuelto
                    objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Inicio Registrar Pago BD")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Input  : " & param_in)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Output : " & param_out)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Fin Registrar Pago BD")
                    'RegistrarLog(COD_TRX_PAGO_SAP, param_in, param_out, "Registro Libro Diario. " & ex.Message)
                End Try

            End If
        Catch ex As Exception
            hidMensaje.Value = "Ocurrio un error en el Pago. " & ex.Message
            blnPagoSapOK = False
        Finally
            'MensajeAudi = "Registro de Pago Sap Recarga Virtual DTH. " & PuntoVenta & "|" & Usuario & "|" & valoresPago.NumeroDocumento 'drDocSapxPagar.Item("VBELN") CAMBIADO POR JYMMY TORRES
            RegistrarAuditoria(MensajeAudi, ConfigurationSettings.AppSettings("codTrxGrabarPagoSap"))
        End Try
        Return blnPagoSapOK
    End Function


    'Private Function Paperless(ByVal K_CU_CORRELATIVOFE As String, _
    '                            ByVal K_NRO_PEDIDO As Int64, ByVal origen As String) As String

    '    Dim objFE As New COM_SIC_FacturaElectronica.PaperLess
    '    Dim objCajas As COM_SIC_Cajas.clsCajas
    '    Dim mensajePpl As String = ""
    '    Dim estado As String = ""
    '    Dim referenciaBF As String = ""
    '    Dim SociedadSap As String = ""
    '    Dim strCorrSunat As String = ""
    '    Dim strIdentifyLog As String = ""


    '    referenciaBF = Obtener_serie_correlativo(K_CU_CORRELATIVOFE, "3")
    '    strCorrSunat = Obtener_serie_correlativo(K_CU_CORRELATIVOFE, "2")

    '    strIdentifyLog = Funciones.CheckStr(K_NRO_PEDIDO)

    '    SociedadSap = "PE01" 'drPagos.Item("SOCIEDAD")      '**HARDCODE**'
    '    mensajePpl = _
    '    objFE.GenerarFacturaElectronicaMSSAP( _
    '        strIdentifyLog, _
    '        Session("ALMACEN"), _
    '        referenciaBF, _
    '        SociedadSap)

    '    RegistrarAuditoria("Envio factura electrónica NumSunat = " & strCorrSunat, CheckStr(ConfigurationSettings.AppSettings("codTrsPaperless")))

    '    estado = mensajePpl

    '    If estado = "F" Then
    '        estado = "E"
    '    End If

    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Actualización del Estado de la Boleta o Factura (SP_UPD_ESTADO_DOCUM)")
    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Numero de DocSap   :" & strIdentifyLog)
    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Origen             :" & origen)
    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Estado             :" & estado)

    '    Dim updateEstado As String = objCajas.Actualizar_Estado_Pago(strIdentifyLog, origen, estado)

    '    If updateEstado = "" Then
    '        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out updateEstado  :" & updateEstado)

    '    Else
    '        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out updateEstado  :" & updateEstado)
    '    End If
    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin de la ctualización del Estado de la Boleta o Factura.")
    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")

    'End Function

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

        'referenciaBF = Obtener_serie_correlativo(K_CU_CORRELATIVOFE, "3")
        referenciaBF = Obtener_serie_correlativo(K_CU_CORRELATIVOFE, "5")
        strCorrSunat = Obtener_serie_correlativo(K_CU_CORRELATIVOFE, "2")

        strIdentifyLog = Funciones.CheckStr(K_NRO_PEDIDO)

        SociedadSap = ConfigurationSettings.AppSettings("Cod_SociedadPE").ToString()'MOD: PROY 32815
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


    'Private Function Obtener_serie_correlativo(ByVal K_CU_CORRELATIVOFE As String, ByVal tipo As String) As String

    '    Try

    '        Dim arrayCorr As String()
    '        Dim referenciaBF As String = ""
    '        Dim SociedadSap As String = ""
    '        Dim SerieSunat As String = ""
    '        Dim strCorrSunat As String = ""
    '        Dim cadena As String = ""
    '        'Dim strCorrSunat As String = ""

    '        arrayCorr = CheckStr(K_CU_CORRELATIVOFE).Split("-")
    '        SerieSunat = (arrayCorr(0).ToString).Substring(1, (arrayCorr(0).ToString).Length - 1) 'serie
    '        strCorrSunat = (CInt(arrayCorr(1).ToString)).ToString
    '        referenciaBF = Funciones.CheckStr(SerieSunat).ToString.Trim & "-" & Funciones.CheckStr(strCorrSunat).ToString.Trim

    '        Select Case tipo
    '            Case 1
    '                cadena = SerieSunat
    '            Case 2
    '                cadena = strCorrSunat
    '            Case 3
    '                cadena = referenciaBF
    '        End Select

    '        Return cadena
    '    Catch ex As Exception
    '        Return ""
    '    End Try

    'End Function

    Private Function Obtener_serie_correlativo(ByVal K_CU_CORRELATIVOFE As String, ByVal tipo As String) As String

        Try

            Dim arrayCorr As String()
            Dim referenciaBF As String = ""
            Dim referenciaBF2 As String = ""
            Dim SociedadSap As String = ""
            Dim SerieSunat As String = ""
            Dim strCorrSunat As String = ""
            Dim cadena As String = ""
            Dim tipoDoc_Sap As String = ""
            Dim Nro_Referencia_Sunat As String = ""
            Dim SerieSunatPP As String = ""
            Dim Nro_Referencia_Sunat_FlujoCaja As String = ""

            arrayCorr = CheckStr(K_CU_CORRELATIVOFE).Split("-")
            SerieSunat = (arrayCorr(0).ToString).Substring(1, (arrayCorr(0).ToString).Length - 1) 'serie
            SerieSunatPP = arrayCorr(0).ToString
            strCorrSunat = (CInt(arrayCorr(1).ToString)).ToString
            referenciaBF = Funciones.CheckStr(SerieSunat).ToString.Trim & "-" & Funciones.CheckStr(strCorrSunat).ToString.Trim
            referenciaBF2 = Funciones.CheckStr(SerieSunatPP).ToString.Trim & "-" & Funciones.CheckStr(strCorrSunat).ToString.Trim



            If arrayCorr(0).StartsWith("B") Then
                tipoDoc_Sap = "E3"
            ElseIf arrayCorr(0).StartsWith("F") Then
                tipoDoc_Sap = "E1"
            End If

            Nro_Referencia_Sunat = tipoDoc_Sap & "-" & Format(Funciones.CheckInt(SerieSunat), "00000") & "-" & Format(Funciones.CheckInt(strCorrSunat), "0000000")
            Nro_Referencia_Sunat_FlujoCaja = tipoDoc_Sap & "-" & SerieSunatPP & "-" & Format(Funciones.CheckInt(strCorrSunat), "00000000")

            Select Case tipo
                Case 1
                    cadena = SerieSunat
                Case 2
                    cadena = strCorrSunat
                Case 3
                    cadena = referenciaBF
                Case 4
                    cadena = Nro_Referencia_Sunat
                Case 5
                    cadena = referenciaBF2
                Case 6
                    cadena = Nro_Referencia_Sunat_FlujoCaja
            End Select

            Return cadena
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Sub DeshacerCambiosPagos(ByVal K_PAGON_IDPAGO As Int64)
        '*** PROCESO DE ROLLBACK AL PROCESO DE PAGOS *****************************************'
        If K_PAGON_IDPAGO > 0 Then
            Dim K_NROLOG As String = ""
            Dim K_DESLOG As String = ""

            objAct.DeshacerCambiosPedidoPagado(K_PAGON_IDPAGO, K_NROLOG, K_DESLOG)
            'If K_DESLOG = "OK" Then
            '    Response.Redirect("PoolPagos.aspx")
            'End If
        End If
    End Sub

    Public Function DevuelteTipoDescPago(ByVal cboTipDocumento As String) As String

        Dim cboDescTipDocumento As String


        If cboTipDocumento = "ZEFE" Then
            cboDescTipDocumento = "EFECTIVO"
        ElseIf cboTipDocumento = "ZCHQ" Then
            cboDescTipDocumento = "PAGO CON CHEQUE"
        ElseIf cboTipDocumento = "ZDEL" Then
            cboDescTipDocumento = "VISA ELECTRON"
        ElseIf cboTipDocumento = "AMEX" Or cboTipDocumento = "ZDIN" Or cboTipDocumento = "ZCAR" Or cboTipDocumento = "VIIN" _
                Or cboTipDocumento = "ZACE" Or cboTipDocumento = "ZCRS" Or cboTipDocumento = "ZSAG" Or cboTipDocumento = "ZCZO" _
                Or cboTipDocumento = "ZDMT" Or cboTipDocumento = "ZMCD" Or cboTipDocumento = "ZRIP" Or cboTipDocumento = "ZVIS" Then

            If cboTipDocumento = "AMEX" Then
                cboDescTipDocumento = "AMERICAN EXPRESS"
            ElseIf cboTipDocumento = "ZDIN" Then
                cboDescTipDocumento = "DINNERS"
            ElseIf cboTipDocumento = "ZCAR" Then
                cboDescTipDocumento = "NET CARD"
            ElseIf cboTipDocumento = "VIIN" Then
                cboDescTipDocumento = "TARJ. VISA INTERNET"
            ElseIf cboTipDocumento = "ZACE" Then
                cboDescTipDocumento = "TARJETA ACE HOME CEN"
            ElseIf cboTipDocumento = "ZCRS" Then
                cboDescTipDocumento = "TARJETA CARSA"
            ElseIf cboTipDocumento = "ZSAG" Then
                cboDescTipDocumento = "TARJETA CMR SAGA"
            ElseIf cboTipDocumento = "ZCZO" Then
                cboDescTipDocumento = "TARJETA CURACAO"
            ElseIf cboTipDocumento = "ZDMT" Then
                cboDescTipDocumento = "TARJETA MAESTRO"
            ElseIf cboTipDocumento = "ZMCD" Then
                cboDescTipDocumento = "TARJETA MASTERCARD"
            ElseIf cboTipDocumento = "ZRIP" Then
                cboDescTipDocumento = "TARJETA RIPLEY"
            Else
                cboDescTipDocumento = "TARJETA VISA"
            End If
            '"AMEX,ZDIN,ZCAR,VIIN,ZACE,ZCRS,ZSAG,ZCZO,ZDMT,ZMCD,ZRIP,ZVIS"
        Else
            '"ZVSB,ZEAM,ZEOV,TDPP,ZCIB"
            If cboTipDocumento = "ZVSB" Then
                cboDescTipDocumento = "CANAL ALTER. SCOTIA"
            ElseIf cboTipDocumento = "ZEAM" Then
                cboDescTipDocumento = "EMP. AMERICA MOVIL"
            ElseIf cboTipDocumento = "ZEOV" Then
                cboDescTipDocumento = "EMP. OVERALL"
            ElseIf cboTipDocumento = "TDPP" Then
                cboDescTipDocumento = "TRANSF.DEUD.POSTPAGO"
                'ElseIf cboTipDocumento = "ZCIB" Then
            ElseIf cboTipDocumento = "ZNCR" Then
                cboDescTipDocumento = "NOTA DE CREDITO"
            Else
                cboDescTipDocumento = "VOUCHER INTERBANCARI"
            End If
        End If

        Return cboDescTipDocumento

    End Function


#End Region

#Region "Registrar Pago Sap"

    Private Function RegistrarPagoSap() As Boolean
        Dim dsConfigCaja, dsReturn As DataSet
        Dim dblEfectivoCaja, dblTolerancia As Double
        Dim dblMaxDisponible, dblEfectivo, dblTotalTarjeta As Double
        Dim strCompleto, strCorrelativo As String
        Dim strNroSunat, strNroAsignaSUNAT, strDetallePago As String
        Dim strDocSunat As String
        Dim param_in As String
        Dim blnSunat As Boolean
        Dim drDocSapxPagar As DataRow
        Dim blnPagoSapOK As Boolean = True

        objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Inicio Registrar Pago BD SICAR")
        '''AGREGADO POR JTN
        Dim objOffLine As New COM_SIC_OffLine.clsOffline
        '''AGREGADO HASTA AQUI
        Try
            ' Obtener Documento Sap x Pagar
            'drDocSapxPagar = ObtenerDocSapxPagar() ''××××××××--->
            ''TODO: CAMBIADO POR JYMMY TORRES
            'strDocSunat = drDocSapxPagar.Item("XBLNR")
            strDocSunat = valoresPago.ReferenciaSunat
            ''CAMBIADO HASTA AQUI
            ' Validar Configuracion de Caja para el Punto de Venta
            dsConfigCaja = oCajas.FP_Get_ListaParamOficina(Canal, CodAplicacion, PuntoVenta) '''NO SAP
            ''TODO: CAMBIADO POR JYMMY TORRES
            'dblEfectivoCaja = Funciones.CheckDbl(oCajas.FP_CalculaEfectivo(PuntoVenta, Usuario, drDocSapxPagar.Item("FKDAT")))
            dblEfectivoCaja = Funciones.CheckDbl(oCajas.FP_CalculaEfectivo(PuntoVenta, Usuario, valoresPago.FechaPago))
            ''CAMBIADO HASTA AQUI

            If IsNothing(dsConfigCaja) Then
                Throw New Exception("No existe configuracion de caja para este Punto de Venta.")
            End If

            dblTolerancia = Funciones.CheckDbl(dsConfigCaja.Tables(0).Rows(0)("CAJA_TOLERANCIA_SOL"))
            dblMaxDisponible = Funciones.CheckDbl(dsConfigCaja.Tables(0).Rows(0)("CAJA_MAX_DISP_SOL"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: RegistrarPagoSap(), dblEfectivoCaja: {1}", CheckStr(hidNroRecarga.Value), CheckStr(dblEfectivoCaja)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: RegistrarPagoSap(), dblMaxDisponible: {1}", CheckStr(hidNroRecarga.Value), CheckStr(dblMaxDisponible)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: RegistrarPagoSap(), dblTolerancia: {1}", CheckStr(hidNroRecarga.Value), CheckStr(dblTolerancia)))
            
            If dblEfectivoCaja >= dblMaxDisponible + dblTolerancia Then
                Throw New Exception("Ha alcanzado su maximo disponible de efectivo en caja.")
            Else
                '' TODO: CAMBIADO POR JYMMY TORRES
                'blnSunat = (drDocSapxPagar.Item("XBLNR") = "" Or drDocSapxPagar.Item("XBLNR") = "0000000000000000")
                Dim dsDatosCaja As DataSet = objOffLine.Get_CajaOficinas(Session("ALMACEN"))

                'Dim codigoCaja As String = String.Empty
                'If Not IsNothing(dsDatosCaja) Then
                '    If dsDatosCaja.Tables(0).Rows.Count > 0 Then codigoCaja = dsDatosCaja.Tables(0).Rows(0)("CASNR")
                'End If

                Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
                Dim codigoCaja As String = objOffLine.ObtenerCaja(codUsuario, Session("ALMACEN"))



                dblEfectivo = 0.0
                dblTotalTarjeta = 0.0
                strDetallePago = ""

                objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "hidNroFilas: " & hidNroFilas.Value)

                Dim strMonto As String = "" 'PROY-27440 
                Dim strTipoDocPago As String = ""'PROY-27440 

                For i As Integer = 1 To hidNroFilas.Value

                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("hidNroRecarga.Value: {0} - INC000003270254, Metodo: RegistrarPagoSap(), For {1} To {2}", CheckStr(hidNroRecarga.Value), CheckStr(i), CheckStr(hidNroFilas.Value)))
                    Dim ddlTipoDocumento As String
                    Dim txtMonto As Double
'PROY-27440 - INI
                    Dim objTxt As TextBox
                    objTxt = CType(Me.FindControl("txtMonto" & i), TextBox)
                    strMonto = "" : strMonto = Trim(objTxt.Text)

                    Dim objCbo As DropDownList
                    objCbo = CType(Me.FindControl("ddlTipoDocumento" & i), DropDownList)
                    strTipoDocPago = "" : strTipoDocPago = Trim(objCbo.SelectedValue)


                    txtMonto = Double.Parse(strMonto)
                    ddlTipoDocumento = strTipoDocPago
'PROY-27440 -FIN
                    strDetallePago = "TC01" 'crearTramaCabeceraPedido.Split(CChar(";"))(1) + strDetallePago & ";"
                    strDetallePago = strDetallePago & PuntoVenta & ";"
                    strDetallePago = strDetallePago & ddlTipoDocumento & ";"
                    strDetallePago = strDetallePago & ";"
                    strDetallePago = strDetallePago & ";"
                    strDetallePago = strDetallePago & txtMonto & ";"
                    strDetallePago = strDetallePago & "PEN" & ";"
                    strDetallePago = strDetallePago & ";"
                    strDetallePago = strDetallePago & strNroAsignaSUNAT & ";"
                    strDetallePago = strDetallePago & ddlTipoDocumento & ";"
                    '' TODO: CAMBIADO POR JYMMY TORRES
                    'strDetallePago = strDetallePago & drDocSapxPagar.Item("FKDAT") & ";
                    strDetallePago = strDetallePago & valoresPago.FechaPago & ";"""
                    ''CAMBIADO HASTA AQUI
                    strDetallePago = strDetallePago & ";"
                    strDetallePago = strDetallePago & ";"

                    ' Obtener Monto Total en Efectivo
                    If ddlTipoDocumento = "ZEFE" Then
                        dblEfectivo += txtMonto
                    End If

                    If i < CInt(hidNroFilas.Value) Then
                        strDetallePago = strDetallePago & "|"
                    End If

                    ' Obtener Monto Total en Tarjetas 
                    If ddlTipoDocumento <> "ZEFE" And ddlTipoDocumento <> "ZEAM" And ddlTipoDocumento <> "ZEOV" And ddlTipoDocumento <> "TDPP" And ddlTipoDocumento <> "ZDEL" Then
                        dblTotalTarjeta += txtMonto
                    End If
                Next

                objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "strDetallePago: " & strDetallePago)

                If (dblEfectivo + dblEfectivoCaja) >= (dblTolerancia + dblMaxDisponible) Then
                    Throw New Exception("Este pago excede su tolerancia de monto máximo de efectivo en caja.")
                End If



                'valoresPago.ReferenciaSunat = oPagos.ObtenerUltimoCorrelativoSunat(PuntoVenta, valoresPago.ClaseFactura, codigoCaja)
                valoresPago.ReferenciaSunat = oPagos.ObtenerUltimoCorrelativoSunat(Session("ALMACEN"), valoresPago.ClaseFactura, Trim(Session("CodImprTicket")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Numero Referencia Sunat: " & valoresPago.ReferenciaSunat)

                blnSunat = (valoresPago.ReferenciaSunat = "" Or valoresPago.ReferenciaSunat = "0000000000000000")
                ''CAMBIADO HASTA AQUI
                ' Obtener Numero Sunat
                '''TODO: SE MANTIENE LA LOGICA DE OBTENER NUMERO DE SUNAT
                'dsReturn = oPagos.Get_NumeroSUNAT(PuntoVenta, drDocSapxPagar.Item("FKART"), CodImpresionTicket, "", strCompleto, strNroSunat) CAMBIADO POR FACTURA OFFLINE
                'dsReturn = oPagos.Get_NumeroSUNAT(PuntoVenta, valoresPago.ClaseFactura, CodImpresionTicket, "", strCompleto, strNroSunat)

                '''CAMBIADO HASTA AQUI
                ' Asignacion de Numero de Sunat
                strCompleto = valoresPago.ReferenciaSunat
                strNroSunat = strCompleto.Split(CChar("-"))(2)
                strNroAsignaSUNAT = IIf(blnSunat, strCompleto, strNroSunat)

                objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "blnSunat: " & blnSunat)
                If blnSunat Then
                    '''TODO: EL NUEVO RFC TRAERA Y ACTUALIZARA EL NUMERODE SUNAT
                    'If Left(strCompleto, 2) = ConfigurationSettings.AppSettings("k_Prefijo_Ticket") And CodImpresionTicket <> "" Then
                    '    '''CAMBIADO POR JTN
                    '    'dsReturn = oSapCajas.Set_NroSunatCajero(drDocSapxPagar.Item("VBELN"), strNroSunat, "", PuntoVenta, "", _
                    '    '                                        drDocSapxPagar.Item("FKDAT"), strDetallePago, "", "", Usuario, _
                    '    '                                        CodImpresionTicket, "", strDocSunat)
                    '    'Else
                    '    '    dsReturn = oSapCajas.Set_NroSunatCajero(drDocSapxPagar.Item("VBELN"), strNroSunat, "", PuntoVenta, _
                    '    '                                            "", drDocSapxPagar.Item("FKDAT"), strDetallePago, "", "", Usuario, _
                    '    '                                            "", "", strDocSunat)

                    '    dsReturn = objOffLine.SetNroSunatCajero(drDocSapxPagar.Item("VBELN"), strNroSunat, "", PuntoVenta, "", _
                    '                                            drDocSapxPagar.Item("FKDAT"), strDetallePago, "", "", Usuario, _
                    '                                            CodImpresionTicket, "", strDocSunat)
                    'Else
                    '    dsReturn = objOffLine.SetNroSunatCajero(drDocSapxPagar.Item("VBELN"), strNroSunat, "", PuntoVenta, _
                    '                                            "", drDocSapxPagar.Item("FKDAT"), strDetallePago, "", "", Usuario, _
                    '                                            "", "", strDocSunat)
                    '    '''CAMBIADO HASTA AQUI
                    'End If
                Else
                    '''CAMBIADO POR JTN
                    'dsReturn = oSapCajas.Set_PagosCajero(drDocSapxPagar.Item("FKDAT"), strDetallePago, Usuario, "")
                    ''dsReturn = objOffLine.SetPagosCajero(valoresPago.FechaPago.ToShortDateString, strDetallePago, Usuario, valoresPago.ReferenciaSunat, Session("ALMACEN"), codigoOperacion)
                    '''CAMBIADO HASTA AQUI
                End If



                ' Modificacion registro de importe recibido y vuelto
                Dim dblMontoSoles, dblMontoUSD, dblVuelto As Double
                Dim totalDeuda#, igv#
                Try
                    ' Se inserta el efectivo obtenido
                    Try
                        oCajas.FP_InsertaEfectivo(PuntoVenta, Usuario, dblEfectivo) ''NO RFC
                    Catch ex As Exception

                    End Try


                    ' Escribir en diario electronico
                    Dim intOperacion As Integer
                    '''CAMBIADO POR JTN
                    'dsReturn = oPagos.Get_ConsultaPedido("", PuntoVenta, drDocSapxPagar.Item("VBELN"), "") '''rfc
                    'dsReturn = objOffLine.GetConsultaPedido("", PuntoVenta, valoresPago.NumeroDocumento, "")
                    '''CAMBIADO HASTA AQUI

                    If Not IsNothing(dsReturn) Then

                        dblMontoSoles = Funciones.CheckDbl(txtRecibidoSoles.Text)
                        dblMontoUSD = Funciones.CheckDbl(txtRecibidoUsd.Text)
                        dblVuelto = Funciones.CheckDbl(txtVuelto.Text)




                        totalDeuda = Funciones.CheckDbl(txtValorDeuda.Text)
                        igv = crearTramaDetallePedido.Split(CChar(";"))(10)


                        intOperacion = oCajas.FP_Cab_Oper(Canal, PuntoVenta, CodAplicacion, Usuario, _
                                                            valoresPago.ClaseFactura, _
                                                            "", _
                                                            valoresPago.ClaseFactura, _
                                                            valoresPago.NumeroDocumento, _
                                                            valoresPago.ReferenciaSunat, _
                                                            0, _
                                                            igv, _
                                                            totalDeuda, "P", _
                                                            dblMontoSoles, dblMontoUSD, dblVuelto) ''' NO SAP

                        objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "intOperacion: " & intOperacion)

                        'For i As Integer = 0 To dsReturn.Tables(1).Rows.Count - 1
                        '    oCajas.FP_Det_Oper(intOperacion, i + 1, _
                        '                        "", _
                        '                        "", _
                        '                        hidTextIdentificador.Value, _
                        '                        1, _
                        '                        0, _
                        '                        igv, _
                        '                        totalDeuda)
                        'Next

                        Dim arrayDetPago() As String
                        Dim arrayLinDetPago() As String

                        arrayDetPago = Split(strDetallePago, "|")

                        objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "arrayDetPago.Length: " & arrayDetPago.Length())
                        Dim strNroTarjta As String = "" 'PROY-27440
                        For i As Integer = 0 To hidNroFilas.Value - 1
                            Try
                                'PROY-27440 - INI 
                                'INC000004396325
                                objFileLog.Log_WriteLog(pathFilePos, strArchivo, hidNroRecarga.Value & " INC000004396325 - arrayDetPago[" & i & "]: " & arrayDetPago(i))
                                'INC000004396325
                                Dim objTxt As TextBox
                                objTxt = CType(Me.FindControl("txtDocumento" & i + 1), TextBox)
                                strNroTarjta = "" : strNroTarjta = Trim(objTxt.Text)
                                'PROY-27440 - FIN
                                arrayLinDetPago = Split(arrayDetPago(i), ";")
                                oCajas.FP_Pag_Oper(intOperacion, i + 1, arrayLinDetPago(2), strNroTarjta, Funciones.CheckDbl(arrayLinDetPago(5))) 'PROY-27440
                            Catch ex As Exception
                                objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Error FP_Pag_Oper: " & ex.Message.ToString())
                            End Try
                        Next
                    End If

                Catch ex As Exception
                    ' Registro de Log
                    param_out = ""
                    param_in = "Monto:" & dblMontoSoles & "|" & "Vuelto:" & dblVuelto
                    objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Inicio Registrar Pago BD")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Input  : " & param_in)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Output : " & param_out)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Fin Registrar Pago BD")
                    'RegistrarLog(COD_TRX_PAGO_SAP, param_in, param_out, "Registro Libro Diario. " & ex.Message)
                End Try

            End If
        Catch ex As Exception
            hidMensaje.Value = "Ocurrio un error en el Pago. " & ex.Message
            blnPagoSapOK = False
            ' Anulacion de Pedido
            AnularDocSapxPagar()
        Finally
            ' Registro de Auditoria
            MensajeAudi = "Registro de Pago Sap Recarga Virtual DTH. " & PuntoVenta & "|" & Usuario & "|" & valoresPago.NumeroDocumento 'drDocSapxPagar.Item("VBELN") CAMBIADO POR JYMMY TORRES
            RegistrarAuditoria(MensajeAudi, ConfigurationSettings.AppSettings("codTrxGrabarPagoSap"))
            ' Registro de Log
            param_in = "Cadena_Pago:" & strDetallePago & "|" & "Nro.Sunat:" & strNroSunat & "|" & "Doc.Sunat:" & strDocSunat

            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Input  : " & param_in)
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Output : " & hidMensaje.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "blnPagoSapOK : " & blnPagoSapOK)
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Fin Registrar Pago BD SICAR")
            'RegistrarLog(COD_TRX_PAGO_SAP, param_in, param_out, hidMensaje.Value)
        End Try

        Return blnPagoSapOK

    End Function

#End Region

#Region "Registrar Pago ST"

    Private Function RegistrarPagoST() As Boolean
        Dim strDocumentos As String
        Dim IdentificadorCliente, NumeroTrace As String
        Dim TipoIdentificador, TextIdentificador As String
        Dim TramaReciboST, TramaFormaPago, strRespuesta As String
        Dim MontoTotalPago As Double
        Dim arrayMensaje() As String

        Dim objPagos As New COM_SIC_Recaudacion.clsPagosDTH
        Dim blnPagoOK As Boolean = True
        objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Inicio Registrar Pago ST")
        Try
            TipoIdentificador = hidTipoIdentificador.Value
            TextIdentificador = hidTextIdentificador.Value
            IdentificadorCliente = txtIdentificadorCliente.Value
            NumeroTrace = hidNumeroTrace.Value


            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "TipoIdentificador: " & TipoIdentificador)
            '''objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "TextIdentificador: " & TextIdentificador)
            '''objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "IdentificadorCliente: " & IdentificadorCliente)


            ' Formato de Envio ST
            TramaReciboST = strRecibos.Value.Replace(",", "|")
            TramaFormaPago = ObtenerTramaFormaPago()
            MontoTotalPago = ObtenerMontoTotalPagar()

            ' No impedir procese del pago por la ausencia de DNI o RUC
            If IsNothing(IdentificadorCliente) OrElse Funciones.CheckStr(IdentificadorCliente) = "" Then
                IdentificadorCliente = "99999999"
            End If

            Dim dblMontoSoles, dblMontoUSD, dblVuelto As Double

            dblMontoSoles = Funciones.CheckDbl(txtRecibidoSoles.Text)
            dblMontoUSD = Funciones.CheckDbl(txtRecibidoUsd.Text)
            dblVuelto = Funciones.CheckDbl(txtVuelto.Text)

            strRespuesta = objPagos.Pagar( _
                                            COD_RUTA_LOG, _
                                            COD_DETALLE_LOG, _
                                            PuntoVenta, _
                                            COD_CANAL, _
                                            PuntoVenta, _
                                            PuntoVenta, _
                                            Usuario, _
                                            TipoIdentificador, _
                                            TextIdentificador, _
                                            TramaFormaPago, _
                                            MontoTotalPago, _
                                            TramaReciboST, _
                                            "", _
                                            "", _
                                            "", _
                                            NroOperacion, _
                                            strDocumentos, _
                                            numeroOperacionPago)

            arrayMensaje = strRespuesta.Split("@")

            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "strRespuesta: " & strRespuesta)
            If (Not IsNothing(arrayMensaje)) AndAlso CheckStr(arrayMensaje(0)) = "00" Then
                ' Capturar la Fecha de Expiracion
                Dim arrayDocumentos() As String = CheckStr(strDocumentos).Split("|")

                If (Not IsNothing(arrayDocumentos)) AndAlso arrayDocumentos.Length > 0 Then
                    Dim arrayLinea() As String = arrayDocumentos(0).Split(";")
                    If (Not IsNothing(arrayLinea)) AndAlso arrayLinea.Length > 6 Then
                        FechaExpiracion = FormatoFecha(CheckStr(arrayLinea(6)), 0)
                    End If
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Error: " & CheckStr(arrayMensaje(1)))
                Throw New Exception(CheckStr(arrayMensaje(1)))
            End If

        Catch ex As Exception
            blnPagoOK = False
            hidMensaje.Value = "Ocurrió un error al realizar el Pago en el ST. " & ex.Message
            ' Anulacion de Documento en Sap
            'FFS
            'AnularDocSapPagado()
            'FFS
        Finally
            ' Registro de Auditoria
            MensajeAudi = "Registro de Pago ST de Recarga Virtual DTH. " & PuntoVenta & "|" & Usuario & "|" & TipoIdentificador & "|" & TextIdentificador
            RegistrarAuditoria(MensajeAudi, ConfigurationSettings.AppSettings("codTrxGrabarPagoST"))
            ' Registrar de Log
            param_in = "TramaST:" & TramaReciboST & "|" & "TramaPago:" & TramaFormaPago
            param_out = "Retorno:" & strRespuesta & "|" & "Nro.Operacion:" & NroOperacion

            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Input  : " & param_in)
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Output : " & param_out)
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Fin Registrar Pago ST")
            'RegistrarLog(COD_TRX_PAGO_ST, param_in, param_out, hidMensaje.Value)
        End Try

        Return blnPagoOK
    End Function

#End Region

#Region "Impresion Ticket"

    Private Sub ImpresionTicket()
        Dim drDocSapPagado As DataRow
        Dim strDocSap As String
        Dim strDocSunat As String
        Dim strNroDG As String
        Dim strTipDoc As String

        Dim sUrl As String = "../Pagos/Terminar.aspx"

        objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Inicio Impresion de Ticket")
        Try
            ' Obtener Documento Sap Pagado
            'drDocSapPagado = ObtenerDocSapPagado()

            Dim objOffline As New COM_SIC_OffLine.clsOffline


            'strDocSap = drDocSapPagado.Item("VBELN")
            'strDocSunat = drDocSapPagado.Item("XBLNR")
            'strNroDG = drDocSapPagado.Item("NRO_DEP_GARANTIA")
            'strTipDoc = drDocSapPagado.Item("FKART")
            strDocSap = Me.K_PEDIN_NROPEDIDO 'valoresPago.NumeroDocumento
            strDocSunat = Me.K_PAGOV_CORRELATIVO 'valoresPago.ReferenciaSunat
            strNroDG = "0000000000"
            strTipDoc = Request.QueryString("tipoDocCliente") 'valoresPago.ClaseFactura
            Dim dsReturn As DataSet

            Dim flagImpresion As Boolean = (CodImpresionTicket <> "")

            'dsReturn = oPagos.Get_ParamGlobal(PuntoVenta)
            dsReturn = objOffline.ParametrosVenta(PuntoVenta)
            ' Si la impresion no es por SAP, abrir cuadro de impresoras
            If CheckStr(dsReturn.Tables(0).Rows(0).Item("IMPRESION_SAP")) = "" Then
                If flagImpresion Then

                    sUrl += "?pImp=S"
                    sUrl += "&pDocSap=" + strDocSap
                    sUrl += "&pDocSunat=" + strDocSunat
                    sUrl += "&pNroDG=" + strNroDG
                    sUrl += "&pTipDoc=" + strTipDoc
                    sUrl += "&strCodPago=" + CheckStr(NroOperacion)
                    sUrl += "&strFechaExpira=" + CheckStr(FechaExpiracion)
                    sUrl += "&strNroRecarga=" + CheckStr(hidNroRecarga.Value)
                    sUrl += "&isOffline=0"
                    'sUrl += "&isOffline=1"
                Else
                    sUrl += "?pImp=N"
                End If
            End If
        Catch ex As Exception
            param_in = "Factura:" & strDocSap & "|" & "Nro.Operacion:" & NroOperacion & "|" & "Nro.Recarga:" & CheckStr(hidNroRecarga.Value)

            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Input  : " & param_in)
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Output : " & ex.Message)
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Fin Impresion de Ticket")
            'RegistrarLog(COD_TRX_PAGO_SAP, param_in, param_out, "Impresion Ticket: " & ex.Message)
        Finally
            oCajas = Nothing
            oPagos = Nothing
            oVentas = Nothing
            oSapCajas = Nothing
            Response.Redirect(sUrl)
        End Try
    End Sub

#End Region

#Region "Anulacion Documento Sap"

    Private Function ObtenerDocSapxPagar() As DataRow
        Dim dsReturn As DataSet
        Dim drDocSapxPagar As DataRow
        Dim oPagos As New SAP_SIC_Pagos.clsPagos
        Dim nroFactura As String = CheckStr(hidNroPedido.Value)
        '''AGREGADO POR JTN
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        '''AGREGADO HASTA AQUI
        Try
            '''CAMBIADO POR JTN
            ' Obtener Documento Sap x Pagar del Cliente
            'dsReturn = oPagos.Get_ConsultaPoolFactura(PuntoVenta, Fecha, "I", "", "", "", "", "1")
            dsReturn = objOffline.ConsultarPoolFacturas(PuntoVenta, Fecha, "I", "", "", "", "", "1")
            '''CAMBIADO HASTA AQUI
            If (Not IsNothing(dsReturn)) AndAlso (dsReturn.Tables(0).Rows.Count > 0) Then
                Dim fila As DataRow
                For Each fila In dsReturn.Tables(0).Rows
                    If CheckStr(fila.Item("VBELN")) = nroFactura Then
                        drDocSapxPagar = CType(fila, DataRow)
                        Exit For
                    End If
                Next
                fila = Nothing
            End If
        Catch ex As Exception
            drDocSapxPagar = Nothing
        End Try
        oPagos = Nothing
        dsReturn = Nothing
        Return drDocSapxPagar
    End Function

    Private Function ObtenerDocSapPagado() As DataRow
        Dim dsReturn As DataSet
        Dim drDocSapPagado As DataRow
        Dim oPagos As New SAP_SIC_Pagos.clsPagos
        Dim nroFactura As String = CheckStr(hidNroPedido.Value)
        Dim objOffline As New COM_SIC_OffLine.clsOffline

        Try
            ' Obtener Documento Sap Pagado del Cliente
            'dsReturn = oPagos.Get_ConsultaPagosUsuario(Fecha, Fecha, "", Usuario, PuntoVenta)
            '''CAMBIADO POR JTN
            'dsReturn = oPagos.Get_ConsultaPagosUsuario(Fecha, "", "", Usuario, PuntoVenta)
            dtIGV = Session("Lista_Impuesto")
            Dim IGVactual As Decimal = 0.0
            For Each row As DataRow In dtIGV.Rows
                If (Date.Now() >= CDate(row("impudFecIniVigencia").ToString.Trim) And Date.Now() <= CDate(row("impudFecFinVigencia").ToString.Trim) And CInt(row("impunTipDoc").ToString.Trim) = 0) Then
                    IGVactual = Math.Round(CDec(row("IGV").ToString.Trim()) / 100, 2)
                    Exit For
                End If
            Next
            dsReturn = objOffline.GetConsultaPagosUsuario(Fecha, "", "", Usuario, PuntoVenta, IGVactual)
            '''CAMBIADO HASTA AQUI
            'Incidencia demora en la carga de pool documentos pagados --> Solicidtud de Luis Palacios enviar solo una fecha 18/06/2012

            If (Not IsNothing(dsReturn)) AndAlso (dsReturn.Tables(0).Rows.Count > 0) Then
                Dim fila As DataRow
                For Each fila In dsReturn.Tables(0).Rows
                    If CheckStr(fila.Item("VBELN")) = nroFactura Then
                        drDocSapPagado = CType(fila, DataRow)
                        Exit For
                    End If
                Next
                fila = Nothing
            End If
        Catch ex As Exception
            drDocSapPagado = Nothing
        End Try
        dsReturn = Nothing
        Return drDocSapPagado
    End Function

    Private Sub AnularDocSapxPagar()
        Dim oAnular As New clsAnulaciones
        Dim drDocSapxPagar As DataRow
        objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Inicio Obtener Documento x Pagar")
        Try
            ' Obtener Documento x Pagar
            drDocSapxPagar = ObtenerDocSapxPagar() '-->
            param_in = "Factura:" & CheckStr(drDocSapxPagar("VBELN")) & "|" & "Pedido:" & CheckStr(drDocSapxPagar("PEDIDO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Input  : " & param_in)
            ' Anulación en Sap de Documento x Pagar
            oAnular.AnularViasPago(CheckStr(drDocSapxPagar("VBELN")), _
                                    CheckStr(drDocSapxPagar("FKART")), _
                                    CheckStr(drDocSapxPagar("XBLNR")), _
                                    CheckStr(drDocSapxPagar("FKDAT")), _
                                    CheckStr(drDocSapxPagar("PEDIDO")), _
                                    CheckStr(drDocSapxPagar("NRO_DEP_GARANTIA")), _
                                    CheckStr(drDocSapxPagar("NRO_REF_DEP_GAR")), _
                                    CheckStr(drDocSapxPagar("NRO_CONTRATO")), _
                                    CheckStr(drDocSapxPagar("NRO_OPE_INFOCORP")), _
                                    CheckStr(drDocSapxPagar("CODIGO_APROBACIO")), _
                                    Canal, _
                                    PuntoVenta, _
                                    CodImpresionTicket, _
                                    Usuario, _
                                    CheckDbl(drDocSapxPagar("PAGOS")), _
                                    , _
                                    "")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Output : " & ex.Message)
            'RegistrarLog(COD_TRX_PAGO_SAP, param_in, param_out, "Anulacion Doc. Sap x Pagar: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Fin Obtener Documento x Pagar")
            drDocSapxPagar = Nothing
            oAnular = Nothing
        End Try
    End Sub

    Private Sub AnularDocSapPagado()
        Dim oAnular As New clsAnulaciones
        Dim drDocSapPagado As DataRow
        Try
            ' Obtener Documento Pagado
            drDocSapPagado = ObtenerDocSapPagado()
            ' Anulación en Sap de Documento Pagado
            If (IsNothing(drDocSapPagado)) Then Return
            oAnular.AnularViasPago(CheckStr(drDocSapPagado("VBELN")), _
                                    CheckStr(drDocSapPagado("FKART")), _
                                    CheckStr(drDocSapPagado("XBLNR")), _
                                    CheckStr(drDocSapPagado("FKDAT")), _
                                    CheckStr(drDocSapPagado("PEDIDO")), _
                                    CheckStr(drDocSapPagado("NRO_DEP_GARANTIA")), _
                                    CheckStr(drDocSapPagado("NRO_REF_DEP_GAR")), _
                                    CheckStr(drDocSapPagado("NRO_CONTRATO")), _
                                    CheckStr(drDocSapPagado("NRO_OPE_INFOCORP")), _
                                    CheckStr(drDocSapPagado("CODIGO_APROBACIO")), _
                                    Canal, _
                                    PuntoVenta, _
                                    CodImpresionTicket, _
                                    Usuario, _
                                    CheckDbl(drDocSapPagado("PAGOS")), _
                                    1, _
                                    "")
        Catch ex As Exception
            param_in = "Factura:" & CheckStr(drDocSapPagado("VBELN")) & "|" & "Pedido:" & CheckStr(drDocSapPagado("PEDIDO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Inicio Obtener Documento Pagado")
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Input  : " & param_in)
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Output : " & ex.Message)
            objFileLog.Log_WriteLog(pathFile, strArchivo, hidNroRecarga.Value & "- " & "Fin Obtener Documento Pagado")
            'RegistrarLog(COD_TRX_PAGO_ST, param_in, param_out, "Anulacion Doc. Sap Pagado: " & ex.Message)
        Finally
            drDocSapPagado = Nothing
            oAnular = Nothing
        End Try
    End Sub

#End Region

#Region "Log/Auditoria"

    'Private Sub RegistrarLog(ByVal v_transaccion As String, ByVal v_params_in As String, ByVal v_params_out As String, ByVal v_exception As String)

    '    Dim v_ip_cliente As String = Request.ServerVariables("REMOTE_ADDR")
    '    Dim v_ip_servidor As String = HttpContext.Current.Request.UserHostAddress
    '    Dim v_nro_recarga As String = hidNroRecarga.Value
    '    Dim oLog As New COM_SIC_Cajas.clsLog

    '    Try
    '        oLog.RegistrarLogDTH(v_nro_recarga, v_transaccion, CurrentUser(), PuntoVenta, v_ip_cliente, v_ip_servidor, _
    '                                v_params_in, v_params_out, CheckDbl(hidMonto.Value), v_exception)
    '    Catch ex As Exception
    '        ' Flujo debe continuar ...
    '    End Try

    'End Sub

    Private Sub RegistrarAuditoria(ByVal DesTrx As String, ByVal CodTrx As String)
        Try
            Dim user As String = CurrentUser
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
#End Region

    Private Class DatosPago
        Public FechaPago As Date
        Public ReferenciaSunat As String
        Public ClaseFactura As String
        Public NumeroDocumento As String
    End Class

    'PROY-27440 INI
    Private Sub actualizar_codigo_recuadacion(ByVal strNroPedido As String, ByVal strCodCabez As String)

        Dim strCodRpt As String = ""
        Dim strMsgRpt As String = ""
        Dim strPedidoLog As String = "strNroPedido: [" & Funciones.CheckStr(strNroPedido) & "] "
        Dim strTipoPago As String

        Try
            strTipoPago = Me.HidTipoPago.Value
            strPedidoLog = Devolver_TipoPago_POS(strTipoPago) & ": [" & Funciones.CheckStr(strCodCabez) & "] "
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "Inicio")

            Dim objEntity As New COM_SIC_Activaciones.BeEnvioTransacPOS
            objEntity.TransId = strCodCabez
            objEntity.FlagPago = "2"
            objEntity.idCabecera = strCodCabez
            objEntity.numPedido = strNroPedido
            objEntity.IdRefAnu = ""

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "strCodCabez: " & strCodCabez)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "actualizar_codigo_recuadacion : " & "strIdRecaudacion: " & strNroPedido)
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

End Class
