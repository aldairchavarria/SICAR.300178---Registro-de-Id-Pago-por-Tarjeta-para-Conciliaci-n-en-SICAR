Imports System.Web.Services
Imports COM_SIC_Adm_Cajas
Imports COM_SIC_Recaudacion
Imports System.Globalization
Imports COM_SIC_Activaciones 'INICIATIVA-318

Public Class detCobranzaDAC
    Inherits SICAR_WebBase '27440

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
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
    Protected WithEvents txtIdentificadorCliente As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNombreCliente As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtTarjCred As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBIN As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboBanco1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboBanco2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboBanco3 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents tdDeuda As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents txtMonto As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents totalRecibos As System.Web.UI.WebControls.Label
    Protected WithEvents dgDocumentosPago As System.Web.UI.WebControls.DataGrid
    Protected WithEvents divDocumentos As System.Web.UI.HtmlControls.HtmlGenericControl

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

    'Proy-31949 INICIO
    Protected WithEvents hdnFlagIntAutPos As System.Web.UI.HtmlControls.HtmlInputHidden
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

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object
#End Region
    'PROY-27440 INI

    Public objFileLog As New SICAR_Log

    'Proy-31949 Inicio
    Private Sub validar_pedido_pos()
        Dim strPedidoLog As String = "Pedido: [" & Funciones.CheckStr(Session("ClienteDAC")) & "] "

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "validar_pedido_pos : " & "Inicio : ")

        Dim dsPedidoPOS As DataSet
        Dim objPedidoPOS As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim strNroPedido As String = Funciones.CheckStr(Session("ClienteDAC"))
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

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : Validacion Integracion FIN")

        Me.HidCodOpera.Value = ClsKeyPOS.strCodOpeVE & "|" & _
        ClsKeyPOS.strCodOpeREC & "|" & ClsKeyPOS.strCodOpeAN

        Me.HidDesOpera.Value = ClsKeyPOS.strDesOpeVE & "|" & _
        ClsKeyPOS.strDesOpeREC & "|" & ClsKeyPOS.strDesOpeAN
        Me.HidTipoOpera.Value = ClsKeyPOS.strOpeFina & "|" & ClsKeyPOS.strNoFina 'OPE FI(90)
        Me.HidTipoTarjeta.Value = ClsKeyPOS.strCodTarjetaVS & "|" & ClsKeyPOS.strCodTarjetaMC

        Me.HidTipoPago.Value = ClsKeyPOS.strTipPagoRDAC '05 RECAUDACION DAC

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

#Region "Variables"
    ' Public objFileLog As New SICAR_Log 'PROY-27440 
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacionDAC")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    'PROY-27440 INI
    Dim nameFilePos As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivoPos As String = objFileLog.Log_CrearNombreArchivo(nameFilePos)
    'PROY-27440 FIN
    Dim arrParametrosFormaPagoPerfil As ArrayList   'INICIATIVA-318
    Dim strIdentifyLogGeneral As String = "" ' INICIATIVA-318
    Dim strCodPerfilFormaPago As String = ""  ' INICIATIVA-318

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")

        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            ''INICIO :: Modificado POR CCC-TS
            'Dim objRecauda As New SAP_SIC_Recaudacion.clsRecaudacion
            Dim dblMonto As Double
            Dim strName As String
            Dim strDoc As String
            ''FIN:: Modificado POR CCC-TS

            txtMonto1.Attributes.Add("onBlur", "javascript:f_Recalcular(this);")
            txtMonto2.Attributes.Add("onBlur", "javascript:f_Recalcular(this);")
            txtMonto3.Attributes.Add("onBlur", "javascript:f_Recalcular(this);")
            txtMonto1.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            txtMonto2.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            txtMonto3.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            txtRecibidoPen.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            txtRecibidoUsd.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            txtRecibidoPen.Attributes.Add("onChange", "javascript:CalculoVuelto();")
            txtRecibidoUsd.Attributes.Add("onChange", "javascript:CalculoVuelto();")

            btnGrabar.Attributes.Add("onClick", "f_validaCajaCerrada();f_Grabar()") 'INICIATIVA-318 INI

        '//--

            'INICIATIVA-318
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
                'PROY-27440 INI
                Me.load_data_param_pos()
                'PROY-27440 FIN
            ''INICIO :: MODIFICADO POR CCC-TS
            Dim strPaginaRetorno As String
            strPaginaRetorno = Mid(Request.ServerVariables("HTTP_REFERER"), InStrRev(Request.ServerVariables("HTTP_REFERER"), "/") + 1) '"recBusNomAp.aspx"
            Me.PAGINA_ANTERIOR = strPaginaRetorno
            Dim strCliente As String = Session("ClienteDAC")
            Dim respuestaDeuda As String = String.Empty
            Dim strVersionSap As String = ConfigurationSettings.AppSettings("strVersionSap")
            txtIdentificadorCliente.Value = strCliente 'Session("ClienteDAC")

            'Get IP
            Dim Ip As String = String.Empty
            Dim listaIp As String
            Dim hostName As String = System.Net.Dns.GetHostName
            Dim local As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(hostName)
            For Each I_ip As System.Net.IPAddress In local.AddressList
                Ip = I_ip.ToString
                Exit For
            Next
            ViewState("strIpAddress") = Ip
            'GET NODO
            Dim nodo As String = Right(System.Net.Dns.GetHostName, 2)
            ViewState("strNodo") = nodo

            'objRecauda.Zpvu_Rfc_Con_Dealer_Deuda_Tot(Session("ClienteDAC"), strName, dblMonto, strDoc)

            '//-- GB 05/2015
            If (Session("OpcionPago") = ConfigurationSettings.AppSettings("PrefijoPagoDAC")) Then
                ConsultarDeudaDAC(strVersionSap, strCliente, strName, dblMonto, strDoc)

                ''FIN:: MODIFICADO POR CCC-TS
                If dblMonto >= 0 Then
                    tdDeuda.InnerText = "Deuda"
                Else
                    tdDeuda.InnerText = "Saldo a Favor"
                End If
                txtMonto.Value = Math.Abs(dblMonto)
                txtNombreCliente.Value = strName

                If strDoc <> String.Empty Then
                    ViewState("strNroFiscal") = strDoc
                End If
                divDocumentos.Style("display") = "none"
            Else
                'Response.Write("<script language=javascript>f_grillaVisible();</script>")
                Dim constDRA As String = ConfigurationSettings.AppSettings("PrefijoPagoDRA")
                ConsultarDeudaDRA(constDRA & strCliente)
                'ConsultarDeudaDRA(constDRA & strCliente.PadLeft(10, "0"))
                divDocumentos.Style("display") = "block"
            End If
            '//--

            'lblTC.Text = ObtenerTipoCambio(Now.ToString("d"))
            lblTC.Text = ObtenerTipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"))
            LlenaCombos()
            LeeDatosValidar()
            Else
            'PROY-27440 INI
                load_values_pos()
            'PROY-27440 FIN
        End If

            'Proy-31949 Inicio
            Me.validar_pedido_pos()
            'Proy-31949 Fin
        End If
    End Sub

    Private Sub LlenaCombos()
        'CAMBIADO POR CCC-TS
        'Dim objPagos As New SAP_SIC_Pagos.clsPagos
        Dim objConf As New COM_SIC_Configura.clsConfigura
        Dim objPagos As New COM_SIC_OffLine.clsOffline
        Dim dtVias As DataTable

        'Dim dsFormaPago As DataSet = objPagos.Get_ConsultaViasPago(Session("ALMACEN"))
        Dim dsFormaPago As DataSet = objPagos.Obtener_ConsultaViasPago(Session("ALMACEN"))
        Dim dsBancos As DataSet = objConf.FP_Lista_Bancos()

        'CAMBIADO POR CCC-TS
        dtVias = New DataTable
        dtVias = VerificarVias(dsFormaPago)

        cboLoad(dtVias, cboTipDocumento1)
        cboLoad(dtVias, cboTipDocumento2)
        cboLoad(dtVias, cboTipDocumento3)

        cboLoadBan(dsBancos, cboBanco1)
        cboLoadBan(dsBancos, cboBanco2)
        cboLoadBan(dsBancos, cboBanco3)

        cboTipDocumento1.Items.Insert(0, "") 'INICIATIVA-318
        cboTipDocumento2.Items.Insert(0, "")
        cboTipDocumento3.Items.Insert(0, "")

        cboBanco1.Items.Insert(0, "")
        cboBanco2.Items.Insert(0, "")
        cboBanco3.Items.Insert(0, "")
        'VIA DE PAGO POR DEFECTO
        'INICIATIVA-318 INI
        Try
        cboTipDocumento1.SelectedValue = "ZEFE"
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
        cboTipDocumento2.SelectedValue = ""
        cboTipDocumento3.SelectedValue = ""




    End Sub

    Private Sub cboLoad(ByVal dsFormaPago As DataTable, ByRef cboCampo As DropDownList)
        cboCampo.DataSource = dsFormaPago
        cboCampo.DataTextField = "VTEXT"
        cboCampo.DataValueField = "CCINS"
        cboCampo.DataBind()

    End Sub

    Private Sub cboLoadBan(ByVal dsBanco As DataSet, ByRef cboCampo As DropDownList)
        cboCampo.DataSource = dsBanco.Tables(0)
        cboCampo.DataTextField = "BANC_DESC"
        cboCampo.DataValueField = "ID_CONFBANC"
        'cboCampo.DataTextField = "BANKA"
        'cboCampo.DataValueField = "BANKL"
        cboCampo.DataBind()

    End Sub

    Private Sub LeeDatosValidar()
        'CAMBIADO POR CCC-TS

        '***************LEE TARJETAS CREDITO
        'Dim objSap As New SAP_SIC_Pagos.clsPagos        
        Dim objSap As New COM_SIC_OffLine.clsOffline
        'Dim dsTmp As DataSet = objSap.Get_Tarjeta_Credito()
        Dim dsTmp As DataSet = objSap.Obtener_Tarjeta_Credito()
        objSap = Nothing

        'CAMBIADO POR CCC-TS

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
        ''CAMBIADO POR CCC-TS
        Response.Redirect("CobranzaDACs.aspx", False) 'Proy-31949
        'Response.Redirect(Me.PAGINA_ANTERIOR)
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        ''INICIO :: Modificado por CCC-TS
        'Dim objRecauda As New SAP_SIC_Recaudacion.clsRecaudacion
        Dim strIdentifyLog As String = Me.txtIdentificadorCliente.Value
        Dim mtoTotalPagado As Double

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio btnGrabar_Click")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

        ''FIN :: Modificado por CCC-TS

        Dim strTrama As String = ""
        Dim strTramaRep As String = ""
        Dim i As Integer
        Dim dsResult As DataSet
        Dim blnError As Boolean
        Dim dblTotal As Double = 0
        Dim strNroAt As String
        Dim strCodDAC As String

        Dim objCaja As New COM_SIC_Cajas.clsCajas

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
        Dim Detalle(5, 3) As String

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        ' fin de variables de auditoria

        Dim intOper As Int32

        'WSOTOMAYOR envio de parametro Banco
        strTrama = txtDoc1.Text & ";" & txtMonto1.Text & ";" & cboTipDocumento1.SelectedValue & ";" & cboBanco1.SelectedValue & ";" & cboTipDocumento1.SelectedItem.Text.Trim() & "|"
        mtoTotalPagado = CDbl(txtMonto1.Text)

        If txtMonto2.Text.Trim <> "" Then
            strTrama = strTrama & txtDoc2.Text & ";" & txtMonto2.Text & ";" & cboTipDocumento2.SelectedValue & ";" & cboBanco2.SelectedValue & ";" & cboTipDocumento2.SelectedItem.Text.Trim() & "|"
            mtoTotalPagado += CDbl(txtMonto2.Text)
        End If
        If txtMonto3.Text.Trim <> "" Then
            strTrama = strTrama & txtDoc3.Text & ";" & txtMonto3.Text & ";" & cboTipDocumento3.SelectedValue & ";" & cboBanco3.SelectedValue & ";" & cboTipDocumento3.SelectedItem.Text.Trim() & "|"
            mtoTotalPagado += CDbl(txtMonto3.Text)
        End If
        'FIN WSOTOMAYOR

        'strTrama = txtDoc1.Text & ";" & txtMonto1.Text & ";" & cboTipDocumento1.SelectedValue & "|"
        'If txtMonto2.Text.Trim <> "" Then
        '    strTrama = strTrama & txtDoc2.Text & ";" & txtMonto2.Text & ";" & cboTipDocumento2.SelectedValue & "|"
        'End If
        'If txtMonto3.Text.Trim <> "" Then
        '    strTrama = strTrama & txtDoc3.Text & ";" & txtMonto3.Text & ";" & cboTipDocumento3.SelectedValue & "|"
        'End If

        ''INICIO :: AGREGADO POR CCC-TS
        Dim objPagos As New COM_SIC_Recaudacion.clsPagos
        ''FIN :: AGREGADO POR CCC-TS

        Try
            strTrama = Left(strTrama, Len(strTrama) - 1)

            'AUDITORIA
            wParam1 = Session("codUsuario")
            wParam2 = Request.ServerVariables("REMOTE_ADDR")
            wParam3 = Request.ServerVariables("SERVER_NAME")
            wParam4 = ConfigurationSettings.AppSettings("gConstOpcRDAC")
            wParam5 = 1
            wParam6 = "Recaudacion de DACs"
            wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
            wParam8 = ConfigurationSettings.AppSettings("gConstEvtRDac")
            wParam9 = Session("codPerfil")
            wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
            wParam11 = 1

            Detalle(1, 1) = "OfVta"
            Detalle(1, 2) = Session("ALMACEN")
            Detalle(1, 3) = "Oficina de venta"

            Detalle(2, 1) = "DAC"
            Detalle(2, 2) = Session("ClienteDAC")
            Detalle(2, 3) = "Cliente DAC"

            Detalle(3, 1) = "Fecha"
            Detalle(3, 2) = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000")  'Now.ToString("d")
            Detalle(3, 3) = "Fecha"

            Detalle(4, 1) = "Trama"
            Detalle(4, 2) = strTrama
            Detalle(4, 3) = "Trama de Pagos"

            Detalle(5, 1) = "Usuario"
            Detalle(5, 2) = Session("USUARIO")
            Detalle(5, 3) = "Usuario"
            'FIN DE AUDITORIA

            'Response.Write("<script language=javascript>alert('" & Session("ALMACEN") & " / " & Session("ClienteDAC") & " / " & "Fecha de Hoy" & " / " & strTrama & " / " & Session("USUARIO") & " / " & strNroAt & "')</script>")
            'dsResult = objRecauda.Set_RecaudacionDAC(Session("ALMACEN"), Session("ClienteDAC"), Now.ToString("d"), strTrama, Session("USUARIO"), strNroAt)
            'dsResult = objRecauda.Set_RecaudacionDAC(Session("ALMACEN"), Session("ClienteDAC"), Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"), strTrama, Session("USUARIO"), strNroAt)

            'INICIO::CAMBIADO POR CCC-TS
            Dim strOficinaAntigua As String = CStr(Session("ALMACEN"))
            Dim strOficina As String = ObtenerCodigoOficinaVenta(CStr(Session("ALMACEN"))) 'Cambiado por pruebas 27.01.2015 'CStr(Session("ALMACEN"))
            Dim strCliente As String = txtIdentificadorCliente.Value.ToString().Trim()
            Dim strNombreCliente As String = txtNombreCliente.Value.ToString().Trim()
            Dim strFecha As String = Format(Now.Year, "0000") & "-" & Format(Now.Month, "00") & "-" & Format(Now.Day, "00")
            Dim strUsuario As String = CStr(Session("USUARIO"))
            Dim strTUser As String = CStr(Session("strUsuario"))
            Dim strNodo As String = CStr(ViewState("strNodo"))
            Dim strIPApp As String = CStr(ViewState("strIpAddress"))
            Dim strResult As String = String.Empty
            Dim strVersionSap As String = ConfigurationSettings.AppSettings("strVersionSap")
            Dim strDefinir As String = "1"
            Dim strNroFiscal As String = String.Empty
            Dim strTipoPago As String = String.Empty

            If Not ViewState("strNroFiscal") Is Nothing Then
                strNroFiscal = CStr(ViewState("strNroFiscal"))
            End If

            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
            If (objOffline.CuadreCajeroRealizado(Session("ALMACEN"), codUsuario)) Then
                Response.Write("<script>alert('Error: No puede realizar esta operacion, ya realizo cuadre de caja')</script>")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error: No puede realizar esta operacion, ya realizo cuadre de caja")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN btnGrabar_Click")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
                Exit Sub
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio objPagos - Metodo Set_RecaudacionDAC")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Almacen  : " & strOficina)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Almacen SAP  : " & strOficinaAntigua)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cliente  : " & strCliente)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nombre Cliente  : " & strNombreCliente)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fecha  : " & strFecha)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Monto total  : " & mtoTotalPagado.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Trama  : " & strTrama)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Usuario  : " & strUsuario)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nodo  : " & strNodo)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Número Identificación Fiscal : " & strNroFiscal)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin")

            'dsResult = objRecauda.Set_RecaudacionDAC(Session("ALMACEN"), txtIdentificadorCliente.Value.ToString().Trim(), Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"), strTrama, Session("USUARIO"), strNroAt) ''SD_758802
            'GB 04/2015
            If (Session("OpcionPago") = ConfigurationSettings.AppSettings("PrefijoPagoDAC")) Then
                strTipoPago = ConfigurationSettings.AppSettings("TipoPagoDAC")
                strResult = objPagos.Set_RecaudacionDAC(strVersionSap, strOficina, strOficinaAntigua, strCliente, strNombreCliente, strFecha, mtoTotalPagado.ToString(), strNodo, strTrama, strUsuario, strTUser, strIPApp, strDefinir, strNroFiscal, strNroAt, strTipoPago)  ''SD_758802

                Dim aResultados() As String = strResult.Split("@")
                If Not aResultados Is Nothing Then
                    If aResultados(0) Is Nothing Or aResultados(0) = "E" Then
                        Dim arrayError() As String = aResultados(1).Split(";")
                        blnError = True
                        wParam5 = 0
                        Session("strMensajeDAC") = arrayError(0).ToString()
                        wParam6 = "Error en recaudacion de dacs. " & arrayError(0).ToString()
                        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje error : " & wParam6)
                        Response.Write("<script language=javascript>alert('" & arrayError(0).ToString() & "')</script>")
                    Else
                        blnError = False
                    End If
                Else
                    blnError = False
                End If

            Else '******* DRA **************

                'strCliente = strCliente.PadLeft(10, "0")
                Dim strCodPagoDra As String = ConfigurationSettings.AppSettings("PrefijoPagoDRA") & strCliente
                Dim strTipoDoc As String = String.Empty
                Dim strNroDoc As String = String.Empty
                Dim strDescripDoc As String = String.Empty
                Dim strImportePago As String = String.Empty
                Dim strTraceID As String ' lo lleno en la clase clsPagos
                Dim strCodPDV As String = CStr(Session("ALMACEN"))  'ObtenerCodigoOficinaVenta(CStr(Session("ALMACEN")))
                Dim strListaMedioPago As String
                Dim strImporteTotalPago As Decimal = 0

                Dim strNroOpePagoSISACT As String
                Dim strCodRespuesta As String
                Dim strMsgRespuesta As String

                For i = 0 To dgDocumentosPago.Items.Count - 1
                    strTipoDoc = CType(dgDocumentosPago.Items(i).FindControl("lblTipoDoc"), Label).Text
                    strNroDoc = CType(dgDocumentosPago.Items(i).FindControl("lblNroDoc"), Label).Text
                    strDescripDoc = CType(dgDocumentosPago.Items(i).FindControl("lblServicio"), Label).Text
                    strImportePago = CType(dgDocumentosPago.Items(i).FindControl("lblImporte"), Label).Text
                Next

                '02;60023;4552-3673-4562-3117;38.00|00,60023,4551-3671-4562-3127,138.00|
                Dim strMensajeAlertMP As String
                If (Me.txtMonto1.Text.Trim() <> "") Then
                    Dim objMP As New COM_SIC_OffLine.clsOffline
                    Dim ds As DataSet = objMP.Get_NuevoCodMedioPago(Me.cboTipDocumento1.SelectedValue)
                    Dim strNewCodMedioPago1 As String = String.Empty
                    Dim swError As Boolean = False
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(ds.Tables(0).Rows(0).Item(0)) Then
                            strNewCodMedioPago1 = ds.Tables(0).Rows(0).Item(0)
                            If strNewCodMedioPago1.ToUpper() = "NO APLICA" Then
                                swError = True
                            End If
                        Else
                            swError = True
                        End If
                    Else
                        swError = True
                    End If

                    If swError Then
                        strMensajeAlertMP = "Medio de Pago 1 Inválido para Pago de DRA"
                        Response.Write("<script language=jscript> alert('" & strMensajeAlertMP & "'); </script>")
                        Exit Sub
                    End If

                    strListaMedioPago = strNewCodMedioPago1 & ";" & Me.cboBanco1.SelectedValue & ";" & Me.txtDoc1.Text.Trim() & ";" & Me.txtMonto1.Text.Trim()
                    strImporteTotalPago = Convert.ToDecimal(Me.txtMonto1.Text)
                End If

                If (Me.txtMonto2.Text.Trim() <> "") Then
                    Dim objMP As New COM_SIC_OffLine.clsOffline
                    Dim ds As DataSet = objMP.Get_NuevoCodMedioPago(Me.cboTipDocumento2.SelectedValue)
                    Dim strNewCodMedioPago2 As String = String.Empty
                    Dim swError As Boolean = False
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(ds.Tables(0).Rows(0).Item(0)) Then
                            strNewCodMedioPago2 = ds.Tables(0).Rows(0).Item(0)
                            If strNewCodMedioPago2.ToUpper() = "NO APLICA" Then
                                swError = True
                            End If
                        Else
                            swError = True
                        End If
                    Else
                        swError = True
                    End If

                    If swError Then
                        strMensajeAlertMP = "Medio de Pago 2 Inválido para Pago de DRA"
                        Response.Write("<script language=jscript> alert('" & strMensajeAlertMP & "'); </script>")
                        Exit Sub
                    End If

                    strListaMedioPago += "|" & strNewCodMedioPago2 & ";" & Me.cboBanco2.SelectedValue & ";" & Me.txtDoc2.Text.Trim() & ";" & Me.txtMonto2.Text.Trim()
                    strImporteTotalPago = strImporteTotalPago + Convert.ToDecimal(Me.txtMonto2.Text)
                End If

                If (Me.txtMonto3.Text.Trim() <> "") Then
                    Dim objMP As New COM_SIC_OffLine.clsOffline
                    Dim ds As DataSet = objMP.Get_NuevoCodMedioPago(Me.cboTipDocumento3.SelectedValue)
                    Dim strNewCodMedioPago3 As String = String.Empty
                    Dim swError As Boolean = False
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(ds.Tables(0).Rows(0).Item(0)) Then
                            strNewCodMedioPago3 = ds.Tables(0).Rows(0).Item(0)
                            If strNewCodMedioPago3.ToUpper() = "NO APLICA" Then
                                swError = True
                            End If
                        Else
                            swError = True
                        End If
                    Else
                        swError = True
                    End If

                    If swError Then
                        strMensajeAlertMP = "Medio de Pago 3 Inválido para Pago de DRA"
                        Response.Write("<script language=jscript> alert('" & strMensajeAlertMP & "'); </script>")
                        Exit Sub
                    End If

                    strListaMedioPago += "|" & strNewCodMedioPago3 & ";" & Me.cboBanco3.SelectedValue & ";" & Me.txtDoc3.Text.Trim() & ";" & Me.txtMonto3.Text.Trim()
                    strImporteTotalPago = strImporteTotalPago + Convert.ToDecimal(Me.txtMonto3.Text)
                End If

                strTipoPago = ConfigurationSettings.AppSettings("TipoPagoDRA")

                strResult = objPagos.Set_RecaudacionDRA(strCodPagoDra, strTipoDoc, strNroDoc, strDescripDoc, _
                                                        strImportePago, strTraceID, strCodPDV, strListaMedioPago, _
                                                        strImporteTotalPago, strNroOpePagoSISACT, strCodRespuesta, _
                                                        strMsgRespuesta, strTipoPago, strOficinaAntigua, strCodPagoDra, _
                                                        strNombreCliente, strFecha, mtoTotalPagado.ToString(), strUsuario, _
                                                        mtoTotalPagado.ToString(), strNodo, strTUser, strTrama, strOficina)

                If (strCodRespuesta = 0) Then
                    'Session("strMensajeDAC") = "El pago se registro correctamente"
                    blnError = False
                Else
                    Session("strMensajeDAC") = "Error al realizar Pago DRA"
                    blnError = True
                End If
            End If

            If Not blnError Then

                strTramaRep = cboTipDocumento1.SelectedItem.Text & ";" & txtDoc1.Text & ";" & txtMonto1.Text & "|"

                If cboTipDocumento1.SelectedValue = "ZEFE" Then
                    objCaja.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto1.Text))
                End If

                intOper = objCaja.FP_Cab_Oper(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), Session("USUARIO"), "", _
                                                                        Session("ClienteDAC"), "RDAC", "", strNroAt, _
                                                                        dblTotal, 0, dblTotal, "D", _
                                                                        CDbl(txtRecibidoPen.Text), CDbl(txtRecibidoUsd.Text), CDbl(txtVuelto.Text))

                objCaja.FP_Det_Oper(intOper, 1, "", "", "", 1, 0, 0, 0)

                objCaja.FP_Pag_Oper(intOper, 1, cboTipDocumento1.SelectedValue, txtDoc1.Text, CDbl(txtMonto1.Text))

                dblTotal += CDbl(txtMonto1.Text)
                If txtMonto2.Text.Trim <> "" Then
                    strTramaRep = strTramaRep & cboTipDocumento2.SelectedItem.Text & ";" & txtDoc2.Text & ";" & txtMonto2.Text & "|"
                    dblTotal += CDbl(txtMonto2.Text)
                    If cboTipDocumento2.SelectedValue = "ZEFE" Then
                        objCaja.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto2.Text))
                    End If
                    objCaja.FP_Pag_Oper(intOper, 2, cboTipDocumento2.SelectedValue, txtDoc2.Text, CDbl(txtMonto2.Text))
                End If

                If txtMonto3.Text.Trim <> "" Then
                    strTramaRep = strTramaRep & cboTipDocumento3.SelectedItem.Text & ";" & txtDoc3.Text & ";" & txtMonto3.Text & "|"
                    dblTotal += CDbl(txtMonto3.Text)
                    If cboTipDocumento3.SelectedValue = "ZEFE" Then
                        objCaja.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto3.Text))
                    End If
                    objCaja.FP_Pag_Oper(intOper, 3, cboTipDocumento3.SelectedValue, txtDoc3.Text, CDbl(txtMonto3.Text))
                End If

                strTramaRep = Left(strTramaRep, Len(strTramaRep) - 1)
                'strCodDAC = Session("ClienteDAC")
                strCodDAC = txtIdentificadorCliente.Value.ToString().Trim()
                Session("ClienteDAC") = ""
                Session("strMensajeDAC") = "El pago se registro correctamente"
                objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)

                'Proy-31949 Ini
            'PROY-27440 INI
            Dim strNumeroRec As String = Funciones.CheckStr(strNroFiscal)
                objFilelog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Inicio Actualizar cabezera " & "	strNumeroDeuda: " & strNumeroRec)
                objFilelog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Inicio Actualizar cabezera " & " HidIdCabez: " & Me.HidIdCabez.Value)
            If (strNumeroRec <> "" And Funciones.CheckStr(Me.HidIdCabez.Value) <> "") Then
                Me.actualizar_codigo_recuadacion(strNumeroRec, Me.HidIdCabez.Value)
                    objFilelog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Fin Actualizar cabezera " & "	strNumeroDeuda: " & strNumeroRec)
            End If
                objFilelog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strNumeroDeuda: " & strNumeroRec)
                objFilelog = Nothing
            'PROY-27440 FIN

                Response.Redirect("CobranzaDACs.aspx?strTrama=" & strTramaRep & "&strMonto=" & Format(Math.Round(dblTotal, 2), "#######0.00") & "&Dealer=" & strCodDAC & " - " & Replace(txtNombreCliente.Value, "&", " "), False)
                'Proy-31949 Fin
            Else
                If (Session("OpcionPago") = ConfigurationSettings.AppSettings("PrefijoPagoDRA")) Then
                    Response.Write("<script language=jscript>window.location='" & Me.PAGINA_ANTERIOR & "';</script>")
                End If
			End If

  
        Catch ex1 As Threading.ThreadAbortException
            'DO NOTHING
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje ERROR: " & ex.Message.ToString())
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); window.location='" & Me.PAGINA_ANTERIOR & "';</script>")
        Finally

'PROY-27440 INI
            Dim objFilelog As New SICAR_Log
'PROY-27440 FIN
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin btnGrabar_Click")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try
    End Sub

    Private Function ObtenerTipoCambio(ByVal strFecha As String) As String
        '''TODO CAMBIADO POR CCC-TS

        'Dim obPagos As New SAP_SIC_Pagos.clsPagos
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim obPagos As Object
        obPagos = New COM_SIC_OffLine.clsOffline

        'Return obPagos.Get_TipoCambio(strFecha).ToString("N3") 'aotane 05.08.2013
        Return Format(obPagos.Obtener_TipoCambio(strFecha), "#######0.000")

        '''TODO CAMBIADO POR CCC-TS
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

    'AGREGADO POR CCC-TS
#Region "Recaudacion DAC"

    Property PAGINA_ANTERIOR() As String
        Get
            Return Funciones.CheckStr(Me.ViewState("PAGINA_ANTERIOR"))
        End Get
        Set(ByVal Value As String)
            Me.ViewState("PAGINA_ANTERIOR") = Value
        End Set
    End Property

    Private Sub ConsultarDeudaDAC(ByVal strVersionSap As String, _
                                    ByVal strCliente As String, _
                                    ByRef strName As String, _
                                    ByRef dMontoDeuda As Decimal, _
                                    ByRef strDoc As String)
        Dim dValorDeuda As Decimal
        Dim strNombre As String = String.Empty
        Dim strDocuemnto As String = String.Empty
        Dim obRecaud As New COM_SIC_Recaudacion.clsConsultas
        Dim strUsuario As String = CStr(Session("strUsuario"))
        Dim strIPApp As String = CStr(ViewState("strIpAddress"))

        Try
            Dim strResult As String = obRecaud.ConsultarDeudaDAC(strVersionSap, strCliente, strUsuario, strIPApp, _
                                                strName, dMontoDeuda, strDoc)
            'PARA VALLES        
            Dim arrMensaje() As String = strResult.Split("@")
            Dim arrDatos() As String

            '*******Si hay error
            If ExisteError(arrMensaje) Then
                Response.Write("<script language=jscript> alert('" + arrMensaje(0).Trim() + "'); window.location='" & Me.PAGINA_ANTERIOR & "';</script>")
                Exit Sub
            End If
        Catch ex1 As Threading.ThreadAbortException
            'DO NOTINHG
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); window.location='" & Me.PAGINA_ANTERIOR & "';</script>")
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

    Private Function ObtenerCodigoOficinaVenta(ByVal strOficinaVta As String) As String
        Dim strVersionSap As String = ConfigurationSettings.AppSettings("strVersionSap")
        Dim resultado As String = String.Empty
        If strVersionSap.Equals("6") Then
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim dsOficinas As DataSet = objOffline.Obtener_NewCodeOficinaVenta(strOficinaVta)
            Dim drOficina As DataRow

            If Not dsOficinas Is Nothing Then
                If dsOficinas.Tables(0).Rows.Count > 0 Then
                    drOficina = dsOficinas.Tables(0).Rows(0)
                    With drOficina
                        resultado = CStr(.Item("PAOFC_OFICINAVENTAS"))
                    End With
                End If
            End If
        ElseIf strVersionSap.Equals("4") Then
            resultado = strOficinaVta
        End If
        Return resultado
    End Function

#End Region


    '//-- GB 05/2015
#Region "DRA"

    Private Sub ConsultarDeudaDRA(ByVal strCliente As String)
        Dim obRecaud As New COM_SIC_Recaudacion.clsConsultas
        Dim dsResultadp As DataSet
        Dim strCodIdCliente As String = String.Empty
        Dim strRazonSocialCliente As String = String.Empty
        Dim strImporteDRA As String = String.Empty
        Dim strMsgRpta As String = String.Empty

        Try
            dsResultadp = obRecaud.GetDeudaDRA(strCliente, strCodIdCliente, strRazonSocialCliente, strImporteDRA, strMsgRpta)
            Me.txtNombreCliente.Value = strRazonSocialCliente
            Me.txtMonto.Value = strImporteDRA
            Me.txtMonto1.Text = strImporteDRA

            If Not strMsgRpta.Equals(String.Empty) Then
                'Response.Write("<script language=jscript> alert('" + strMsgRpta.Trim() + "'); window.location='" & Me.PAGINA_ANTERIOR & "';</script>")
                Session("strMensajeDAC") = strMsgRpta.Trim()
                Response.Redirect("CobranzaDACs.aspx")
            Else
                Session("strMensajeDAC") = ""
                dgDocumentosPago.DataSource = dsResultadp.Tables(0)
                dgDocumentosPago.DataBind()
            End If
        Catch ex1 As Threading.ThreadAbortException
            'DO NOTINHG
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); window.location='" & Me.PAGINA_ANTERIOR & "';</script>")
        Finally
            dsResultadp.Dispose()
            obRecaud = Nothing
        End Try
    End Sub

#End Region
    '//--

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
    'INICIATIVA-318 INI
End Class
