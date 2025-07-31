'Imports COM_SIC_Procesa_Pagos
Imports System.Globalization

Public Class repCuadreProceso
    Inherits SICAR_WebBase 'proy-27440

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtSaldo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIngreso As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRemesa As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMontoP As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkCierre As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents hldVerif As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents trRemesa As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trCierre As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents bntForzado As System.Web.UI.WebControls.Button
    Protected WithEvents btnRecalcula As System.Web.UI.WebControls.Button
    Protected WithEvents txtCajaCheque As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCaja As System.Web.UI.WebControls.TextBox
    Protected WithEvents procesarHandler As System.Web.UI.WebControls.Button
    Protected WithEvents lblMontoP As System.Web.UI.WebControls.Label

    'PROY-27440 INI
    Protected WithEvents HidPtoVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIntAutPos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidGrabAuto As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoOpera As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoOperaMC As System.Web.UI.HtmlControls.HtmlInputHidden
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
    Protected WithEvents HidCodOpera As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDesOpera As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidFila1 As System.Web.UI.HtmlControls.HtmlInputHidden    
    Protected WithEvents txtDifVisa As System.Web.UI.WebControls.TextBox
    Protected WithEvents trDifVisa As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents txtDifMCD As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDifAmex As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDifDiners As System.Web.UI.WebControls.TextBox
    Protected WithEvents trDifMCD As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trDifAmex As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trDifDiners As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents HidIntAutPosMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMsgRsptMov As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoMoneda As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTransMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMonedaMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidApliPOS As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMonedaVisa As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected WithEvents HidDatoPosVisa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoPosMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoAuditPos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIdCabez As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnGrabarOculto As System.Web.UI.WebControls.Button
    Protected WithEvents HidValidaExitoPOS As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIntAutPosRep As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidPosIdVISA As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidPosIdMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIntVisa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIntMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIntentosCuadre As System.Web.UI.HtmlControls.HtmlInputHidden

    'PROY-27440 FIN
    'INICIATIVA-318 ARQUEO INI
    Protected WithEvents btnRegregsar As System.Web.UI.WebControls.Button
    Protected WithEvents hidEstado As System.Web.UI.HtmlControls.HtmlInputHidden
    'INICIATIVA-318 ARQUEO INI

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim objPagos As New SAP_SIC_Pagos.clsPagos
    Dim objSapCaja As New SAP_SIC_Cajas.clsCajas
    Dim objCaja As COM_SIC_Cajas.clsCajas 'Modificado por TS-CCC
    Dim dsReturn As DataSet
    Dim dblSaldo As Double
    Dim dblCajaB As Double
    Dim i As Integer
    Dim strUsuario As String
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = "Log_CuadreCaja"
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogActivacionPrepago")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim objPagoFijoPaginas As New COM_SIC_Procesa_Pagos.PagoFijoPaginas
    Dim objAsignacionCajas As New COM_SIC_Procesa_Pagos.AsignacionCajas
    Dim objPedido As New COM_SIC_Procesa_Pagos.Pedido
    Dim objclsOffline As COM_SIC_OffLine.clsOffline 'Modificado por TS-CCC
    Dim objAnulaciones As New COM_SIC_Procesa_Pagos.Anulaciones
    Dim strNodo As String 'Agregado por TS-CCC
    Dim strCajaAsignadaID As String = String.Empty 'Agregado por TS-CCC
    Private Const PAGADO As String = "PG" 'Agregado por TS-CCC
    Private Const WAITING As String = "W"
    Private Const PROCESS As String = "N"

    'PROY-27440 INI
    Dim nameFilePos As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivoPos As String = objFileLog.Log_CrearNombreArchivo(nameFilePos)
    'PROY-27440 FIN


    'PROY-27440 INI
    Private Sub load_data_param_pos()

        Me.HidPtoVenta.Value = Funciones.CheckStr(Session("ALMACEN"))

        Dim strIdentifyLog As String = "Cuadre Caja | " & Session("ALMACEN") & "|" & IIf(UCase(Request.Item("tipocuadre")) = "I", Session("USUARIO"), "")

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "Integracion SICAR - POS")

        Dim strIpClient As String = Funciones.CheckStr(Session("IpLocal"))

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - Validacion Integracion INI")
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidPtoVenta : " & HidPtoVenta.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "strIpClient : " & strIpClient)


        Dim strCodRptaFlag As String = ""
        Dim strMsgRptaFlag As String = ""

        Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS

        'INI CONSULTA INTEGRACION AUTOMATICO POS

        Dim strFlagIntAut As String = ""

        strCodRptaFlag = "" : strMsgRptaFlag = ""
        objConsultaPos.Obtener_Integracion_Auto(Funciones.CheckStr(Me.HidPtoVenta.Value), strIpClient, String.Empty, strFlagIntAut, strCodRptaFlag, strMsgRptaFlag)

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "strFlagIntAut : " & Funciones.CheckStr(strFlagIntAut))
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "strCodRptaFlag : " & Funciones.CheckStr(strCodRptaFlag))
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "strMsgRptaFlag : " & Funciones.CheckStr(strMsgRptaFlag))

        Me.HidIntAutPos.Value = Funciones.CheckStr(strFlagIntAut)

        'FIN CONSULTA INTEGRACION AUTOMATICO POS

        Me.HidIntAutPosRep.Value = Funciones.CheckStr(strFlagIntAut)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "Integracion POS : " & HidIntAutPos.Value)

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - Validacion Integracion FIN")

        Me.HidPtoVenta.Value = Funciones.CheckStr(Session("ALMACEN"))
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidPtoVenta : " & HidPtoVenta.Value)

        Me.HidCodOpera.Value = ClsKeyPOS.strCodOpeVE & "|" & ClsKeyPOS.strCodOpeVC & "|" & ClsKeyPOS.strCodOpeAN
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidCodOpera: " & HidCodOpera.Value)

        Me.HidDesOpera.Value = ClsKeyPOS.strDesOpeVE & "|" & ClsKeyPOS.strDesOpeVC & "|" & ClsKeyPOS.strDesOpeAN
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidDesOpera: " & HidDesOpera.Value)

        Me.HidTipoOpera.Value = ClsKeyPOS.strNoFina 'OPE  NO FI(91)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidTipoOpera: " & HidTipoOpera.Value)

        Me.HidTipoOperaMC.Value = ClsKeyPOS.strOpeOFF 'OPE NO FI MC(12)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidTipoOperaMC: " & HidTipoOperaMC.Value)

        Me.HidTipoTarjeta.Value = ClsKeyPOS.strCodTarjetaVS & "|" & ClsKeyPOS.strCodTarjetaMC
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidTipoTarjeta: " & HidTipoTarjeta.Value)

        Me.HidEstTrans.Value = ClsKeyPOS.strEstTRanPen & "|" & _
                               ClsKeyPOS.strEstTRanPro & "|" & _
                               ClsKeyPOS.strEstTRanAce & "|" & _
                               ClsKeyPOS.strEstTRanRec & "|" & _
                               ClsKeyPOS.strEstTRanInc

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidEstTrans: " & HidEstTrans.Value)

        Me.HidTipoPOS.Value = ClsKeyPOS.strTipoPosVI & "|" & _
                              ClsKeyPOS.strTipoPosMC & "|" & _
                              ClsKeyPOS.strTipoPosAM & "|" & _
                              ClsKeyPOS.strTipoPosDI

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidTipoPOS: " & HidTipoPOS.Value)

        Me.HidTipoTran.Value = ClsKeyPOS.strTipoTransCIP
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidTipoTran: " & HidTipoTran.Value)

        Me.HidTipoMoneda.Value = ClsKeyPOS.strTipoMonSoles
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidTipoMoneda: " & HidTipoMoneda.Value)

        Me.HidTransMC.Value = ClsKeyPOS.strTranMC_Compra & "|" & _
                              ClsKeyPOS.strTranMC_Anulacion & "|" & _
                              ClsKeyPOS.strTranMC_RepDetallado & "|" & _
                              ClsKeyPOS.strTranMC_RepTotales & "|" & _
                              ClsKeyPOS.strTranMC_ReImpresion & "|" & _
                              ClsKeyPOS.strTranMC_Cierre & "|" & _
                              ClsKeyPOS.strPwdComercio_MC

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidTransMC: " & HidTransMC.Value)

        Me.HidMonedaMC.Value = ClsKeyPOS.strMonedaMC_Soles & "|" & ClsKeyPOS.strMonedaMC_Dolares
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidMonedaMC: " & HidMonedaMC.Value)

        Me.HidApliPOS.Value = ClsKeyPOS.strConstMC_POS
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidApliPOS: " & HidApliPOS.Value)

        Me.HidMonedaVisa.Value = ClsKeyPOS.strMonedaVisa_Soles & "|" & ClsKeyPOS.strMonedaVisa_Dolares
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidMonedaVisa: " & HidMonedaVisa.Value)

        Me.HidIntentosCuadre.Value = ClsKeyPOS.strNumIntentosCuadre

        'DATOS DEL POS

        Me.HidDatoPosVisa.Value = ""
        Me.HidDatoPosMC.Value = ""

        Me.HidDatoAuditPos.Value = Funciones.CheckStr(CurrentTerminal()) & "|" & _
        Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")) & "|" & _
        Funciones.CheckStr(Session("USUARIO"))

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidDatoAuditPos: " & HidDatoAuditPos.Value)
        If Me.HidIntAutPos.Value = "1" Then
            Dim objSicarDB As New COM_SIC_Activaciones.clsTransaccionPOS
            Dim strIp As String = CurrentTerminal()
            Dim strEstadoPos As String = "1"
            Dim strTipoVisa As String = "V"
            Dim ds As DataSet
            Dim bvalida As Integer = 0
            Dim strMensaje As String = ""

            'VISA INICIO
            strTipoVisa = "V"

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "strIpClient : " & strIpClient)

            ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoVisa)

            Dim arrIPDesc(2) As String
            arrIPDesc(0) = strIpClient


            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "strEstadoPos : " & strEstadoPos)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "strTipoVisa : " & strTipoVisa)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "Count : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

            Dim strMensajeVisa As String = ClsKeyPOS.strIPMsjDesconfigurado

            If ds.Tables(0).Rows.Count > 0 Then

                Me.HidPosIdVISA.Value = Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID"))

                Me.HidDatoPosVisa.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
                & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
                & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
                & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
                & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
                & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
                & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
                & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA")) _
                & "|POSV_FLAGPOS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSC_FLG_SICAR"))

                Me.HidIntVisa.Value = Funciones.CheckStr(ds.Tables(0).Rows(0)("POSC_FLG_SICAR"))
            Else
                bvalida = 1
                HidIntAutPosRep.Value = 0
                Response.Write("<script>alert('" & strMensajeVisa & "');</script>")
            End If

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidDatoPosVisa: " & HidDatoPosVisa.Value)

            'VISA FIN

            'MC INICIO
            strTipoVisa = "M"
            ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoVisa)

            Dim strMensajeMC As String = ClsKeyPOS.strIPMsjDesconfigurado

            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "Count : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

            If ds.Tables(0).Rows.Count > 0 Then

                Me.HidPosIdMC.Value = Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID"))

                Me.HidDatoPosMC.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
                & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
                & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
                & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
                & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
                & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
                & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
                & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA")) _
                & "|POSV_FLAGPOS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSC_FLG_SICAR"))

                Me.HidIntMC.Value = Funciones.CheckStr(ds.Tables(0).Rows(0)("POSC_FLG_SICAR"))
            Else
                HidIntAutPosRep.Value = 0
                If bvalida = 0 Then
                    Response.Write("<script>alert('" & strMensajeMC & "');</script>")
                End If
            End If
        End If
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "HidDatoPosMC: " & HidDatoPosMC.Value)
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
            Dim dblRemesa As Double

            btnBuscar.Attributes.Add("onClick", "f_Buscar()")
            btnGrabar.Attributes.Add("onClick", "f_Aprobar()")
            btnRecalcula.Attributes.Add("onClick", "f_OcultarBotonera()")
            bntForzado.Attributes.Add("onClick", "f_OcultarBotonera()")

            ''' CUADRE POR CAJERO = I
            ''' CUADRE POR PDV = Nothing

            If UCase(Request.Item("tipocuadre")) = "I" Then
                trRemesa.Visible = False    'se oculta la fila de Remesa
                btnRecalcula.Visible = False
                strUsuario = Session("USUARIO")
            Else
                trRemesa.Visible = True
                btnRecalcula.Visible = False
                strUsuario = ""
            End If

            If Not Page.IsPostBack Then
                'trRemesa.Visible = False
                objFileLog.Log_WriteLog(pathFile, strArchivo, "INC000002440268" & "- " & "   INC000002440268 - If Not Page.IsPostBack - Caja Asignada: " & strCajaAsignadaID)
                txtFecha.Text = Format(Now.Date.Day, "00") & "/" & Format(Now.Date.Month, "00") & "/" & Format(Now.Date.Year, "0000")
                'PROY-27440-INICIO
                txtDifAmex.Text = "0.00"
                txtDifMCD.Text = "0.00"
                txtDifVisa.Text = "0.00"
                txtDifDiners.Text = "0.00"
                'PROY-27440-Fin
            End If

            'Fecha limite

            objclsOffline = New COM_SIC_OffLine.clsOffline
            Dim flagSinergia As String
            Dim strIdentifyLog As String = Session("ALMACEN") & "|" & strUsuario
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Inicio: GetFechaOficinaSinergia - PageLoad()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  IN OFICINA : " & CStr(Session("ALMACEN")))
            Dim strFechaLim As String = objclsOffline.GetFechaOficinaSinergia(CStr(Session("ALMACEN")), flagSinergia)
            'Dim strFechaLim As String = ConfigurationSettings.AppSettings("FechaFinConsultaRFC_Cuadre")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  OUT FECHA : " & strFechaLim)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  OUT FLAG : " & flagSinergia)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Fin: GetFechaOficinaSinergia - PageLoad()")

            Dim anio As Integer = CInt(strFechaLim.Substring(6, 4))
            Dim mes As Integer = CInt(strFechaLim.Substring(3, 2))
            Dim dia As Integer = CInt(strFechaLim.Substring(0, 2))
            Dim dFechaLim As New Date(anio, mes, dia)
            Dim dFecha As Date = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)

            'PROY-27440 INI
            
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Integracion SICAR - POS - INICIO CARGANDO PARAMETROS - CUADRE DE CAJA")
            Me.load_data_param_pos()
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Integracion SICAR - POS - FIN CARGANDO PARAMETROS - CUADRE DE CAJA")

            If HidIntAutPos.Value = "1" Then
                trDifVisa.Visible = True
                trDifMCD.Visible = True
                trDifDiners.Visible = True
                trDifAmex.Visible = True
            Else
                trDifVisa.Visible = False
                trDifMCD.Visible = False
                trDifDiners.Visible = False
                trDifAmex.Visible = False
            End If

            'PROY-27440 FIN

            If dFecha < dFechaLim Then
                Session("FlujoAnteriorCuadre") = True
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  PageLoad() - PROCESA CUADRE CON EL FLUJO ANTERIOR")
            Else
                Session("FlujoAnteriorCuadre") = False
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  PageLoad() - PROCESA CUADRE CON EL FLUJO SINERGIA")
            End If

            objCaja = New COM_SIC_Cajas.clsCajas 'Modificado por TS-CCC
            Try
                If Len(Trim(strUsuario)) > 0 Then
                    txtRemesa.Text = "0.00"
                Else
                    'dblRemesa = objCaja.FP_CalculaRemesa(Session("ALMACEN"), CDate(txtFecha.Text))
                    dblRemesa = objCaja.FP_CalculaRemesa(Session("ALMACEN"), txtFecha.Text)
                    txtRemesa.Text = Format(dblRemesa, "######0.00")
                End If

                If CBool(Session("FlujoAnteriorCuadre")) Then 'flujo anterior
                    dsReturn = objPagos.Get_CuadreCajaConsulta(txtFecha.Text, Session("ALMACEN"), strUsuario, dblSaldo, dblCajaB)
                    'saldo de efectivo del dia anterior
                    txtSaldo.Text = dblSaldo
                    'contenido de caja buzon en efectivo
                    'txtCaja.Text = Format(objCaja.FP_CalculaCajaBuzon(Session("ALMACEN"), strUsuario, CDate(txtFecha.Text)), "######0.00")
                    txtIngreso.Text = 0 'INICIATIVA-318
                    txtCaja.Text = Format(objCaja.FP_CalculaCajaBuzon(Session("ALMACEN"), strUsuario, txtFecha.Text), "######0.00")
                    'total de caja buzon cheque x JC 05/11/2007
                    txtCajaCheque.Text = Format(objCaja.FP_CalculaCajaBuzonCheque(Session("ALMACEN"), strUsuario, txtFecha.Text), "######0.00")
                Else 'Nuevo Flujo
                    txtSaldo.Text = 0
                    txtMontoP.Text = ""
                    txtIngreso.Text = 0 'INICIATIVA-318
                    txtCaja.Text = Format(objCaja.FP_CalculaCajaBuzon(Session("ALMACEN"), strUsuario, txtFecha.Text), "######0.00")
                    txtCajaCheque.Text = Format(objCaja.FP_CalculaCajaBuzonCheque(Session("ALMACEN"), strUsuario, txtFecha.Text), "######0.00")
                    strNodo = Right(System.Net.Dns.GetHostName, 2)
                    ViewState("MensajeDocumentosPend") = Nothing
                End If
                'INICIATIVA-529 INI
                'If Request.QueryString("tipocuadre") = "i" Then
                    MontosFaltantesSobrantes()
                'End If
                'INICIATIVA-529 FIN

                'INICIATIVA-318 ARQUEO INI
                If Request.QueryString("botonregresar") = "1" Then
                    Me.btnRegregsar.Visible = True
                End If
                'INICIATIVA-318 ARQUEO FIN

                'FIN PRE_CUADRE_OFFLINE :: Agregado por TS-CCC
            Catch ex As Exception
                Response.Write("<script> alert('" + ex.Message + "');</script>")
            Finally
                objCaja = Nothing
                objclsOffline = Nothing
            End Try
        End If
    End Sub

'proy-27440 todo el contenido de este metodo se paso a metodo nuevo btnGrabarOculto_Click
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

                Try

            If chkCierre.Checked = True And Me.HidIntAutPos.Value = "1" And UCase(Request.Item("tipocuadre")) = "I" Then
                Me.RegisterStartupScript("Registra", "<script>CerrarLotePOS();</script>")
                Else
                Call btnGrabarOculto_Click(sender, e)
                End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ProcesoCuadre(ByVal blnValidacion As Boolean)
        Dim strMensaje As String
        Dim dblIngreso As Double
        Dim dblEfectivo As Decimal
        Dim dblFaltante As Decimal
        Dim dblSobrante As Decimal
        Dim dblEfectivoDia As Double
        Dim dsSobresRemesa As DataSet
        Dim dvSobres As New DataView

        Dim rDblFaltante As Double
        Dim rDblSobrante As Double

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
        Dim Detalle(10, 3) As String

        Dim strFechaBuzon As String
        Dim strFechaEnviada As String
        Dim datFechaBuzon As Date

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        ' fin de variables de auditoria

        Dim strIdentifyLog As String = Session("ALMACEN") & "|" & strUsuario

        Dim dMtoCajaBuzonAnterior As Double = 0 'Agregado TS-CCC
        Dim dMtoCajaBuzonAnteriorPendiente As Double = 0 'Agregado TS-CCC
        Dim dMtoRemesaAnteriorEnviadaHoy As Double = 0 'Agregado TS-CCC
        Dim strFechaAnterior As String 'Agregado TS-CCC
        Dim dRemesaHoy As Double = 0 'Agregado TS-CCC
        Dim dMtoCajaBuzonAnteriorNoEnviadoAyer As Double = 0 'Agregado TS-CCC
        Dim dblCuadreEfecDia As Decimal = 0 'Agregado TS-CCC
        Dim dblTotEfecDia As Decimal 'Agregado TS-CCC

        objCaja = New COM_SIC_Cajas.clsCajas 'Modificado TS-CCC
        objclsOffline = New COM_SIC_OffLine.clsOffline 'Modificado TS-CCC
        Try
            'AUDITORIA
            wParam1 = Session("codUsuario")
            wParam2 = Request.ServerVariables("REMOTE_ADDR")
            wParam3 = Request.ServerVariables("SERVER_NAME")

            wParam5 = 1
            wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")

            wParam9 = Session("codPerfil")
            wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
            wParam11 = 1

            'FIN DE AUDITORIA

            'INI -- TS-CCC - CUADRE INDIVIDUAL
            If UCase(Request.Item("tipocuadre")) = "I" Then
                Dim codCajero As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio FP_CalculaEfectivoDia")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp ALMACEN:" & CStr(Session("ALMACEN")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp CAJERO:" & CStr(Session("USUARIO")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp FECHA:" & txtFecha.Text)
                dblTotEfecDia = objCaja.FP_CalculaEfectivoDia(Session("ALMACEN"), Session("USUARIO"), txtFecha.Text)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT dblTotEfecDia:" & dblTotEfecDia.ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin FP_CalculaEfectivoDia")

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio GetCalcularEfeCaja")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp ALMACEN:" & CStr(Session("ALMACEN")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp CAJERO:" & codCajero)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp FECHA:" & CDate(txtFecha.Text).ToString("yyyyMMdd"))
                dblCuadreEfecDia = objclsOffline.GetCalcularEfeCaja(CStr(Session("ALMACEN")), codCajero, CDate(txtFecha.Text).ToString("yyyyMMdd"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT dblCuadreEfecDia:" & dblCuadreEfecDia.ToString())

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio - Recalculo Pre - Proceso")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI Recalcular - FP_InsertaEfectivo")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Oficina Vta : " & CStr(Session("ALMACEN")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Cajero : " & CStr(Session("USUARIO")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Diferencia de efectivo : " & CStr((dblCuadreEfecDia - dblTotEfecDia)))

                If Math.Round(dblTotEfecDia, 2) <> Math.Round(dblCuadreEfecDia, 2) Then
                    objCaja.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), (dblCuadreEfecDia - dblTotEfecDia), txtFecha.Text)
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: ProcesoCuadre)"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT MENSAJE : " & "Se actualizó el saldo de caja." & MaptPath)
                    'FIN PROY-140126

                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN Recalcular - FP_InsertaEfectivo")

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin - Recalculo Pre - Proceso")
            End If
            'FIN -- TS-CCC

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio FP_CalculaEfectivo")
            'dblEfectivo = objCaja.FP_CalculaEfectivo(Session("ALMACEN"), strUsuario, CDate(txtFecha.Text))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp ALMACEN:" & Session("ALMACEN"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp strUsuario:" & strUsuario)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtFecha:" & txtFecha.Text)

            If CBool(Session("FlujoAnteriorCuadre")) Then 'Flujo Anterior
                dblEfectivo = objCaja.FP_CalculaEfectivo(Session("ALMACEN"), strUsuario, txtFecha.Text)
            Else 'Nuevo Flujo
                dblEfectivo = objclsOffline.GetEfectivo(Session("ALMACEN"), strUsuario, txtFecha.Text)
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   out dblEfectivo:" & dblEfectivo.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin FP_CalculaEfectivo")

            'AGREGADO POR FFS INICIO
            'IIf(txtIngreso.Text = "", dblIngreso = 0, dblIngreso = CDbl(txtIngreso.Text))

            If txtIngreso.Text = "" Then
                dblIngreso = 0
            Else
                dblIngreso = CDbl(txtIngreso.Text)
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   dblIngreso:" & dblIngreso.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   blnValidacion:" & blnValidacion)

            If blnValidacion Then
                If (Math.Round(dblIngreso, 2) - Math.Round(dblEfectivo, 2)) < 0 Then  'diferencia de montos
                    txtMontoP.Text = Format(Math.Abs((Math.Round(dblIngreso, 2) - Math.Round(dblEfectivo, 2))), "######0.00")
                    'INICIATIVA-529 INI
                    'Me.RegisterStartupScript("diferencias", "<script language=javascript>alert('Existen diferencias de ingreso de efectivo')</script>")
                    ''Response.Write("<script language=javascript>alert('Existen diferencias de ingreso de efectivo')</script>")
                    'chkCierre.Checked = False
                    'trCierre.Visible = False
                    'If Not Request.QueryString("tipocuadre") = "i" Then
                    'bntForzado.Visible = True
                    'End If
                    'INICIATIVA-529 FIN

                    If Not CBool(Session("FlujoAnteriorCuadre")) Then 'Nuevo Flujo
                        'INICIO PRE_CUADRE_OFFLINE :: Agregado por TS-CCC
                        lblMontoP.Text = "Efectivo Faltante :" 'PROY -27440
                        Dim dDiferenciaFaltante As Double = Math.Abs(Math.Round(dblIngreso, 2) - Math.Round(dblEfectivo, 2))
                        Me.setLogCuadreCaja(UCase(Request.Item("tipocuadre")), dblIngreso.ToString(), txtCaja.Text, txtCajaCheque.Text, _
                                                dDiferenciaFaltante, CDbl(txtRemesa.Text).ToString(), "Existen diferencias de ingreso de efectivo", strNodo, strCajaAsignadaID)
                        'FIN PRE_CUADRE_OFFLINE :: Agregado por TS-CCC
                    End If

                    'PROY-27440 - INICIO'
                    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "VALIDANDO SWTICH INTEGRAR CON POS : " & HidIntAutPos.Value)

                    If HidIntAutPos.Value = "1" Then
                        Dim dsDatosCalculados As DataSet
                        Dim drDatosCalculados As DataRow
                        Dim strCodUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
                        If UCase(Request.Item("tipocuadre")) = "I" Then
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencias SICAR - POS - GetDatosCuadre")
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencias SICAR - POS - TipoCuadre : I")
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencias SICAR - POS - Oficina Vta : " & CStr(Session("ALMACEN")))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencias SICAR - POS -  Fecha : " & CDate(txtFecha.Text).ToString("yyyyMMdd"))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencias SICAR - POS -  Cajero : " & strCodUsuario)
                            dsDatosCalculados = objclsOffline.GetDatosCuadre(CStr(Session("ALMACEN")), CDate(txtFecha.Text).ToString("yyyyMMdd"), strCodUsuario)

                        Else
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencias SICAR - POS - GetDatosCuadre")
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencias SICAR - POS - TipoCuadre : G")
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencias SICAR - POS - Oficina Vta : " & CStr(Session("ALMACEN")))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencias SICAR - POS - Fecha : " & CDate(txtFecha.Text).ToString("yyyyMMdd"))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencias SICAR - POS - Cajero : 0")
                            dsDatosCalculados = objclsOffline.GetDatosCuadre(CStr(Session("ALMACEN")), CDate(txtFecha.Text).ToString("yyyyMMdd"), "0")
                        End If

                        If dsDatosCalculados.Tables(0).Rows.Count > 0 Then
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencias SICAR - POS - Datos encontrados ")
                            drDatosCalculados = dsDatosCalculados.Tables(0).Rows(0)  'Primera fila.
                        Else
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencias SICAR - POS - Datos no encontrados")
                            drDatosCalculados = Nothing
                        End If

                        If Not IsNothing(drDatosCalculados) Then
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencia SICAR - POS - INICIO")
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "DIFERENCIA VISA")
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "VISA - SICAR :" & drDatosCalculados("TOT_VNT_VIS"))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "VISA - DEVOLUCION :" & drDatosCalculados("TOT_VNT_VIS_DEV"))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "VISA - POS :" & drDatosCalculados("TOT_VNT_VIS_POS"))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "DIFERENCIA MASTERCARD")
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "MCD - SICAR :" & drDatosCalculados("TOT_VNT_MC"))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "MCD - DEVOLUCION :" & drDatosCalculados("TOT_VNT_MC_DEV"))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "MCD - POS :" & drDatosCalculados("TOT_VNT_MC_POS"))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "DIFERENCIA AMERICAN EXPRESS")
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "AMEX - SICAR :" & drDatosCalculados("TOT_VNT_AMX"))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "AMEX - DEVOLUCION :" & drDatosCalculados("TOT_VNT_AMX_DEV"))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "AMEX - POS :" & drDatosCalculados("TOT_VNT_AMX_POS"))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "DIFERENCIA DINERS")
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "DINERS - SICAR :" & drDatosCalculados("TOT_VNT_DIN"))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "DINERS - DEVOLUCION :" & drDatosCalculados("TOT_VNT_DIN_DEV"))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "DINERS - POS :" & drDatosCalculados("TOT_VNT_DIN_POS"))
                            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencia SICAR - POS - FIN")

                            Dim dTotalVisa As Double = CDbl(drDatosCalculados("TOT_VNT_VIS"))
                            Dim dTotalVisaDev As Double = CDbl(drDatosCalculados("TOT_VNT_VIS_DEV"))
                            Dim dTotalVisaPos As Double = CDbl(drDatosCalculados("TOT_VNT_VIS_POS"))
                            Dim dTotaMCD As Double = CDbl(drDatosCalculados("TOT_VNT_MC"))
                            Dim dTotalMCDDev As Double = CDbl(drDatosCalculados("TOT_VNT_MC_DEV"))
                            Dim dTotalMCDPos As Double = CDbl(drDatosCalculados("TOT_VNT_MC_POS"))

                            Dim dTotalAmex As Double = CDbl(drDatosCalculados("TOT_VNT_AMX"))
                            Dim dTotalAmexDev As Double = CDbl(drDatosCalculados("TOT_VNT_AMX_DEV"))
                            Dim dTotalAmexPos As Double = CDbl(drDatosCalculados("TOT_VNT_AMX_POS"))
                            Dim dTotalDiners As Double = CDbl(drDatosCalculados("TOT_VNT_DIN"))
                            Dim dTotalDinersDev As Double = CDbl(drDatosCalculados("TOT_VNT_DIN_DEV"))
                            Dim dTotalDinersPos As Double = CDbl(drDatosCalculados("TOT_VNT_DIN_POS"))

                            txtDifVisa.Text = Format(CDbl(dTotalVisa - dTotalVisaPos + dTotalVisaDev), "######0.00")
                            txtDifMCD.Text = Format(CDbl(dTotaMCD - dTotalMCDPos + dTotalMCDDev), "######0.00")
                            txtDifAmex.Text = Format(CDbl(dTotalAmex - dTotalAmexPos + dTotalAmexDev), "######0.00")
                            txtDifDiners.Text = Format(CDbl(dTotalDiners - dTotalDinersPos + dTotalDinersDev), "######0.00")

                        Else
                            txtDifVisa.Text = "0.00"
                            txtDifMCD.Text = "0.00"
                            txtDifAmex.Text = "0.00"
                            txtDifDiners.Text = "0.00"
                        End If
                    End If
                    'PROY-27440 - FIN'
                    'INICIATIVA-318 INI
                    'If Request.QueryString("tipocuadre") = "i" Then
                        ForzarCuadre()
                    'End If
                    'INICIATIVA-318 FIN
                    Exit Sub
                Else
                    trCierre.Visible = True
                    txtMontoP.Text = "0.00"
                End If
            End If

            If Not CBool(Session("FlujoAnteriorCuadre")) Then 'Nuevo Flujo
                'Se valida caja individual anterior si se cerro.
                Dim strMensajeCierreAnterior As String = String.Empty
                If UCase(Request.Item("tipocuadre")) = "I" Then
                    If chkCierre.Checked Then
                        strMensajeCierreAnterior = objclsOffline.GetCierreCajaIndividualAnterior(Session("ALMACEN"), strCajaAsignadaID, txtFecha.Text)
                        If Not strMensajeCierreAnterior.Equals(String.Empty) Then
                            Dim strMsjContinuar As String = " El proceso continuará."
                            Dim scriptDP$ = String.Format("<script language=javascript>alert('{0}')</script>", strMensajeCierreAnterior & strMsjContinuar)
                            Me.RegisterStartupScript("RegistraAlertaCCA", scriptDP)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & strMensajeCierreAnterior & strMsjContinuar)
                        End If
                    End If
                Else 'Se valida caja general anterior si se cerro.
                    Dim flagSinergia As String
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Inicio: GetFechaOficinaSinergia")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  IN OFICINA : " & CStr(Session("ALMACEN")))
                    Dim strFechaLim As String = objclsOffline.GetFechaOficinaSinergia(CStr(Session("ALMACEN")), flagSinergia)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  OUT FECHA : " & strFechaLim)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  OUT FLAG : " & flagSinergia)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Fin: GetFechaOficinaSinergia")

                    'Dim strFechaLim As String = ConfigurationSettings.AppSettings("FechaFinConsultaRFC_RepCuadre")
                    Dim anio As Integer = CInt(strFechaLim.Substring(6, 4))
                    Dim mes As Integer = CInt(strFechaLim.Substring(3, 2))
                    Dim dia As Integer = CInt(strFechaLim.Substring(0, 2))
                    Dim dFechaLim As New Date(anio, mes, dia)
                    Dim dFecha As Date = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)

                    If Not (dFecha = dFechaLim) Then 'Diferente de la fecha del Pase a produccion
                        If chkCierre.Checked Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INI  GetCierreCajaGeneralAnterior()")
                            strMensajeCierreAnterior = objclsOffline.GetCierreCajaGeneralAnterior(Session("ALMACEN"), txtFecha.Text)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "OUT  strMensajeCierreAnterior : " & strMensajeCierreAnterior)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN  GetCierreCajaGeneralAnterior()")
                            If Not strMensajeCierreAnterior.Equals(String.Empty) Then
                                Dim scriptDP$ = String.Format("<script language=javascript>alert('{0}')</script>", strMensajeCierreAnterior)
                                Me.RegisterStartupScript("RegistraAlertaCCA", scriptDP)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & strMensajeCierreAnterior)
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If

            dblFaltante = 0
            dblSobrante = 0

            'If hldVerif.Value = "1" Then  'Validaciones en javascript correctas
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP ofVenta  " & Session("ALMACEN"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     tipocuadre  " & UCase(Request.Item("tipocuadre")))
            If UCase(Request.Item("tipocuadre")) = "I" Then
                strUsuario = Session("USUARIO")
                txtRemesa.Text = "0.00"
                wParam6 = "Proceso de Cuadre por Cajero"
                wParam4 = ConfigurationSettings.AppSettings("gConstOpcPCCJ")
                wParam8 = ConfigurationSettings.AppSettings("gConstEvtPCCJ")
            Else
                strUsuario = ""
                wParam6 = "Proceso de Cuadre por PDV"
                wParam4 = ConfigurationSettings.AppSettings("gConstOpcPCPV")
                wParam8 = ConfigurationSettings.AppSettings("gConstEvtPCPV")

                ' If CDbl(txtRemesa.Text) = 0 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio FP_BolsasLibres")
                dsSobresRemesa = objCaja.FP_BolsasLibres(Session("ALMACEN"), "")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin FP_BolsasLibres")

                If CBool(Session("FlujoAnteriorCuadre")) Then 'Flujo Anterior
                    If Not IsNothing(dsSobresRemesa) Then
                        For i = 0 To dsSobresRemesa.Tables(0).Rows.Count - 1
                            datFechaBuzon = dsSobresRemesa.Tables(0).Rows(i).Item("BUZON_FECHA")
                            strFechaBuzon = Format(Year(datFechaBuzon), "0000") & Format(Month(datFechaBuzon), "00") & Format(Day(datFechaBuzon), "00")
                            strFechaEnviada = Right(txtFecha.Text, 4) & Mid(txtFecha.Text, 4, 2) & Left(txtFecha.Text, 2)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha buzon : " & strFechaBuzon)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha Emviada : " & strFechaEnviada)

                            If strFechaBuzon < strFechaEnviada Then
                                Me.RegisterStartupScript("noremesa", "<script language=javascript>alert('No se ha enviado la remesa completa correspondiente')</script>")
                                ''Response.Write("<script language=javascript>alert('No se ha enviado la remesa completa correspondiente')</script>")
                                Exit Sub
                            End If
                        Next

                        'dvSobres.Table = dsSobresRemesa.Tables(0)
                        'dvSobres.RowFilter = "BUZON_FECHA<'" & txtFecha.Text & "'"

                        'If dvSobres.Count > 0 Then
                        '    Response.Write("<script language=javascript>alert('No se ha enviado la remesa completa correspondiente')</script>")
                        '    Exit Sub
                        'End If
                    End If
                    'End If
                Else
                    'Cuadre General: Obtiene fecha y monto total del buzon anterior
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio: GetCajaBuzonAnterior")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Oficina Venta : " & CStr(Session("ALMACEN")))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha : " & txtFecha.Text)
                    dMtoCajaBuzonAnterior = objclsOffline.GetCajaBuzonAnterior(CStr(Session("ALMACEN")), txtFecha.Text, strFechaAnterior, dMtoCajaBuzonAnteriorPendiente, dMtoRemesaAnteriorEnviadaHoy, dMtoCajaBuzonAnteriorNoEnviadoAyer)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT MontoCajaBuzonAnterior : " & Math.Round(dMtoCajaBuzonAnterior, 2).ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT MontoCajaBuzonAnteriorPendiente : " & Math.Round(dMtoCajaBuzonAnteriorPendiente, 2).ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT Fecha Anterior : " & strFechaAnterior)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT MtoRemesaAnteriorEnviadaHoy : " & Math.Round(dMtoRemesaAnteriorEnviadaHoy, 2).ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin: GetCajaBuzonAnterior")

                    'INICIO PRE_CUADRE_OFFLINE :: Agregado por TS-CCC
                    If dMtoCajaBuzonAnteriorPendiente > 0 Then
                        If Not IsNothing(dsSobresRemesa) Then
                            For i = 0 To dsSobresRemesa.Tables(0).Rows.Count - 1
                                datFechaBuzon = dsSobresRemesa.Tables(0).Rows(i).Item("BUZON_FECHA")
                                strFechaBuzon = Format(Year(datFechaBuzon), "0000") & Format(Month(datFechaBuzon), "00") & Format(Day(datFechaBuzon), "00")
                                strFechaEnviada = Right(txtFecha.Text, 4) & Mid(txtFecha.Text, 4, 2) & Left(txtFecha.Text, 2)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha buzon : " & strFechaBuzon)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha Emviada : " & strFechaEnviada)

                                If strFechaBuzon < strFechaEnviada Then
                                    Dim strMsjEnvioRem As String = "El monto de envió de remesa " & Format(Math.Round(dMtoRemesaAnteriorEnviadaHoy, 2), "######0.00") & " es menor al total del Buzón anterior pendiente de enviar " & Format(Math.Round(dMtoCajaBuzonAnteriorNoEnviadoAyer, 2), "######0.00") & " del dia " & strFechaAnterior
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MENSAJE : No se ha enviado la remesa completa correspondiente.")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MENSAJE : " & strMsjEnvioRem)
                                    Me.RegisterStartupScript("noremesa1", "<script language=javascript>alert('" & strMsjEnvioRem & " ')</script>")
                                    Me.setLogCuadreCaja(UCase(Request.Item("tipocuadre")), txtIngreso.Text, txtCaja.Text, txtCajaCheque.Text, _
                                                    0, CDbl(txtRemesa.Text).ToString(), strMsjEnvioRem, strNodo, strCajaAsignadaID)
                                    If chkCierre.Checked Then
                                        Me.RegisterStartupScript("noremesa2", "<script language=javascript>alert('No fue posible cierre debido a diferencia de remesa.')</script>")
                                        Exit Sub 'Termina flujo
                                    Else
                                        Exit For 'Continua Flujo
                                    End If
                                End If
                            Next
                        End If
                    Else
                        'Valida Hay remesas enviadas de sobre generados el dia de hoy
                        Dim strMensajeRemesaAdic As String = String.Empty
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio GetRemesaHoy")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp ALMACEN: " & Session("ALMACEN"))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtFecha: " & txtFecha.Text)
                        dRemesaHoy = objclsOffline.GetRemesaHoy(Session("ALMACEN"), txtFecha.Text)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out Remesa Hoy: " & Math.Round(dRemesaHoy, 2).ToString())
                        strMensajeRemesaAdic = "Se envió : " & Format(Math.Round(dRemesaHoy, 2), "######0.00") & " de remesas de sobres del dia de hoy : " & txtFecha.Text
                        If dRemesaHoy > 0 Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MENSAJE : " & strMensajeRemesaAdic)
                            Me.RegisterStartupScript("noremesa2", "<script language=javascript>alert('" & strMensajeRemesaAdic & " ')</script>")
                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin GetRemesaHoy")
                    End If
                End If
                'FIN PRE_CUADRE_OFFLINE :: Agregado por TS-CCC
            End If

            If (Math.Round(dblIngreso, 2) - Math.Round(dblEfectivo, 2)) < 0 Then
                dblFaltante = (Math.Round(dblIngreso, 2) - Math.Round(dblEfectivo, 2))
            Else
                dblSobrante = (Math.Round(dblIngreso, 2) - Math.Round(dblEfectivo, 2))
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   dblFaltante:" & dblFaltante.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   dblSobrante:" & dblSobrante.ToString())

            'dblEfectivoDia = objCaja.FP_CalculaEfectivoDia(Session("ALMACEN"), Session("USUARIO"), txtFecha.Text)
            'If Math.Round(CDbl(txtCaja.Text), 2) <> Math.Round(dblEfectivoDia, 2) Then
            '    dblSobrante += Math.Abs(Math.Round(CDbl(txtCaja.Text), 2) - Math.Round(dblEfectivoDia, 2))
            'End If

            'AUDITORIA
            Detalle(1, 1) = "Fecha"
            Detalle(1, 2) = txtFecha.Text
            Detalle(1, 3) = "Fecha"

            Detalle(2, 1) = "OfVta"
            Detalle(2, 2) = Session("ALMACEN")
            Detalle(2, 3) = "Oficina de Venta"

            Detalle(3, 1) = "Saldo"
            Detalle(3, 2) = CDbl(txtSaldo.Text)
            Detalle(3, 3) = "Saldo"

            Detalle(4, 1) = "Ingreso"
            'Detalle(4, 2) = CDbl(txtIngreso.Text)
            If txtIngreso.Text = "" Then
                Detalle(4, 2) = 0
            Else
                Detalle(4, 2) = CDbl(txtIngreso.Text)
            End If
            Detalle(4, 3) = "Ingreso"

            Detalle(5, 1) = "Caja"
            Detalle(5, 2) = CDbl(txtCaja.Text)
            Detalle(5, 3) = "Caja Buzon"

            Detalle(6, 1) = "Remesa"
            Detalle(6, 2) = CDbl(txtRemesa.Text)
            Detalle(6, 3) = "Remesa"

            Detalle(7, 1) = "Faltante"
            Detalle(7, 2) = dblFaltante
            Detalle(7, 3) = "Monto Faltante"

            Detalle(8, 1) = "Sobrante"
            Detalle(8, 2) = dblSobrante
            Detalle(8, 3) = "Monto Sobrante"

            Detalle(9, 1) = "Cierre"
            Detalle(9, 2) = IIf(chkCierre.Checked, "X", "(vacio)")
            Detalle(9, 3) = "Cierre"

            Detalle(10, 1) = "Usuario"
            Detalle(10, 2) = IIf(Len(Trim(strUsuario)) > 0, strUsuario, "(vacio)")
            Detalle(10, 3) = "Usuario"

            'FIN DE AUDITORIA
            'AGREGADO POR FFS INICIO
            Dim dblIngres As Double = 0
            If CBool(Session("FlujoAnteriorCuadre")) Then 'Flujo Anterior
                If txtIngreso.Text = "" Then
                    dblIngres = 0
                Else
                    dblIngres = CDbl(txtIngreso.Text)
                End If
            End If
            'AGREGADO POR FFS FIN

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   tipocuadre:" & UCase(Request.Item("tipocuadre")))

            'INICIO :: MODIFICADO POR TS-CCC
            If CBool(Session("FlujoAnteriorCuadre")) Then 'Flujo Anterior
                If UCase(Request.Item("tipocuadre")) = "I" Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio Set_CuadreCajero")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtFecha: " & txtFecha.Text)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp ALMACEN: " & Session("ALMACEN"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtSaldo: " & txtSaldo.Text)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtIngreso: " & txtIngreso.Text)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtCaja: " & txtCaja.Text)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtCajaCheque: " & txtCajaCheque.Text)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtRemesa: " & txtRemesa.Text)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp dblFaltante: " & dblFaltante.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp dblSobrante: " & dblSobrante.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp chkCierre: " & IIf(chkCierre.Checked, "X", ""))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp strUsuario: " & strUsuario)
                    dsReturn = objSapCaja.Set_CuadreCajero(txtFecha.Text, Session("ALMACEN"), CDbl(txtSaldo.Text), dblIngres, CDbl(txtCaja.Text), CDbl(txtCajaCheque.Text), CDbl(txtRemesa.Text), dblFaltante, dblSobrante, IIf(chkCierre.Checked, "X", ""), strUsuario)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin Set_CuadreCajero")
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio Get_CuadreCajaProceso")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtFecha: " & txtFecha.Text)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp ALMACEN: " & Session("ALMACEN"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtSaldo: " & txtSaldo.Text)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtIngreso: " & txtIngreso.Text)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtCaja: " & txtCaja.Text)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtCajaCheque: " & txtCajaCheque.Text)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtRemesa: " & txtRemesa.Text)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp dblFaltante: " & dblFaltante.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp dblSobrante: " & dblSobrante.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp chkCierre: " & IIf(chkCierre.Checked, "X", ""))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp strUsuario: " & strUsuario)
                    dsReturn = objPagos.Get_CuadreCajaProceso(txtFecha.Text, Session("ALMACEN"), CDbl(txtSaldo.Text), dblIngres, CDbl(txtCaja.Text), CDbl(txtCajaCheque.Text), CDbl(txtRemesa.Text), dblFaltante, dblSobrante, IIf(chkCierre.Checked, "X", ""), strUsuario)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin Get_CuadreCajaProceso")
                End If

                For i = 0 To dsReturn.Tables(0).Rows.Count - 1
                    If dsReturn.Tables(0).Rows(i).Item("TYPE") = "E" Then
                        strMensaje = dsReturn.Tables(0).Rows(i).Item("MESSAGE")
                        wParam5 = 0
                        wParam6 = "Error en " & wParam6 & ". " & strMensaje
                    End If
                Next
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   strMensaje: " & strMensaje)
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio setCuadreMontos")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtFecha: " & txtFecha.Text)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp ALMACEN: " & Session("ALMACEN"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtSaldo: " & txtSaldo.Text) 'ts-ccc
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtIngreso: " & txtIngreso.Text)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtCaja: " & txtCaja.Text)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtCajaCheque: " & txtCajaCheque.Text)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtRemesa: " & txtRemesa.Text)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp dblFaltante: " & dblFaltante.ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp dblSobrante: " & dblSobrante.ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp chkCierre: " & IIf(chkCierre.Checked, "X", ""))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp strUsuario: " & strUsuario)

                strMensaje = setCuadreMontos(UCase(Request.Item("tipocuadre")), dblSobrante, _
                                        dblFaltante, blnValidacion, dMtoCajaBuzonAnterior, _
                                        strFechaAnterior, dRemesaHoy, dMtoRemesaAnteriorEnviadaHoy, _
                                        dMtoCajaBuzonAnteriorPendiente, rDblSobrante, rDblFaltante)

                dblSobrante = rDblSobrante
                dblFaltante = rDblFaltante

                If dblSobrante > 0 Then
                    txtMontoP.Text = Format(Math.Abs(dblSobrante), "######0.00")
                    lblMontoP.Text = "Efectivo Sobrante :" 'PROY -27440
                End If

                If Math.Abs(dblFaltante) >= 0 And dblSobrante = 0 Then
                    txtMontoP.Text = Format(Math.Abs(dblFaltante), "######0.00")
                    lblMontoP.Text = "Efectivo Faltante :" 'PROY -27440
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin setCuadreMontos")
            End If
            'FIN :: MODIFICADO POR TS-CCC

            If Len(Trim(strMensaje)) > 0 Then
                Me.RegisterStartupScript("errorCuadre", "<script language=javascript>alert('" & strMensaje & "')</script>")
                ''Response.Write("<script language=javascript>alert('" & strMensaje & "')</script>")
                If Not CBool(Session("FlujoAnteriorCuadre")) Then  'Nuevo Flujo
                    'INICIO PRE_CUADRE_OFFLINE :: Agregado por TS-CCC
                    Me.setLogCuadreCaja(UCase(Request.Item("tipocuadre")), dblIngreso.ToString(), txtCaja.Text, txtCajaCheque.Text, _
                                                            dblFaltante.ToString(), CDbl(txtRemesa.Text).ToString(), strMensaje, strNodo, strCajaAsignadaID)
                    'FIN PRE_CUADRE_OFFLINE :: Agregado por TS-CCC
                End If
            Else
                If trCierre.Visible Then  'para que solo se visualice el primer mensaje
                    Dim strOficina As String = Session("ALMACEN")
                    Dim strUsuario As String = Session("USUARIO")
                    Dim strFecha As String = txtFecha.Text
                    Dim strCaja As String = "0" 'Session("CANAL")
                    Dim strEstado As String = "S"

                    If CBool(Session("FlujoAnteriorCuadre")) Then   'Flujo Anterior
                        Me.RegisterStartupScript("exito", "<script language=javascript>alert('Proceso enviado con éxito')</script>")
                    Else 'Nuevo Flujo
                        'INICIO PRE_CUADRE_OFFLINE :: Agregado por TS-CCC
                        Dim strMontoMinimoFaltante As String = ConfigurationSettings.AppSettings("MONTO_MINIMO_FALTANTE")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   out MONTO_MINIMO_FALTANTE : " & strMontoMinimoFaltante)
                        If Not strMontoMinimoFaltante.Equals(String.Empty) Then
                            If Math.Abs(dblFaltante) > CDbl(strMontoMinimoFaltante) Then
                                'Me.RegisterStartupScript("diferenciasMtoMinFalt", "<script language=javascript>alert('Imposible cierre diario, Ingreso Faltante muy grande.')</script>")
                                'INI PROY-140126
                                Dim MaptPath As String
                                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                                MaptPath = "( Class : " & MaptPath & "; Function: ProcesoCuadre)"
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ProcesoCuadre Mensaje : " & "Imposible cierre diario, Ing.Faltante muy grande." & MaptPath)
                                'FIN PROY-140126 
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ProcesoCuadre Mensaje : " & "Imposible cierre diario, Ing.Faltante muy grande.")
                                Me.setLogCuadreCaja(UCase(Request.Item("tipocuadre")), dblIngreso.ToString(), txtCaja.Text, txtCajaCheque.Text, _
                                                        dblFaltante.ToString(), CDbl(txtRemesa.Text).ToString(), "Imposible cierre diario, Ing.Faltante muy grande.", strNodo, strCajaAsignadaID)
                                'If chkCierre.Checked Then
                                '    Exit Sub
                                'End If
                            End If
                        Else
                            Me.RegisterStartupScript("conf_mmFaltante", "<script language=javascript>alert('No se ha configurado el valor de monto mínimo faltante.')</script>")
                            'INI PROY-140126
                            Dim MaptPath As String
                            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                            MaptPath = "( Class : " & MaptPath & "; Function: ProcesoCuadre)"
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ProcesoCuadre Mensaje : " & "No se ha configurado el valor de monto mínimo faltante." & MaptPath)
                            'FIN PROY-140126 

                            Me.setLogCuadreCaja(UCase(Request.Item("tipocuadre")), dblIngreso.ToString(), txtCaja.Text, txtCajaCheque.Text, _
                                                    dblFaltante.ToString(), CDbl(txtRemesa.Text).ToString(), "No se ha configurado el valor de monto mínimo faltante.", strNodo, strCajaAsignadaID)
                            Exit Sub
                        End If
                        'FIN PRE_CUADRE_OFFLINE :: Agregado por TS-CCC
                    End If

                    If (chkCierre.Checked) Then
                        Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
                        Dim objclsOffline As New COM_SIC_OffLine.clsOffline

                        If CBool(Session("FlujoAnteriorCuadre")) Then   'Flujo Anterior
                            objclsOffline.ActualizarEstadoCuadre(strOficina, codUsuario, txtFecha.Text)
                        Else 'Nuevo Flujo
                            'INICIO PRE_CUADRE_OFFLINE :: Modificado por TS-CCC
                            Dim strMensajeCdrGen As String = String.Empty
                            If UCase(Request.Item("tipocuadre")) = "I" Then
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Inicio Actualizar Estado Cuadre Individual")
                                objclsOffline.ActualizarEstadoCuadre(strOficina, codUsuario, txtFecha.Text)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Fin Actualizar Estado Cuadre Individual")
                            Else
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Inicio Actualizar Estado Cuadre General")
                                strMensajeCdrGen = objclsOffline.ActualizarTransCuadreCajeroGen(strCajaAsignadaID, "S")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Inicio Actualizar Estado Cuadre General")
                                If Not strMensajeCdrGen.Equals(String.Empty) Then
                                    Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strMensajeCdrGen)
                                    Me.RegisterStartupScript("RegistraAlerta33", script)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "No se cerro caja. Mensaje: " & strMensajeCdrGen)
                                    Exit Sub
                                End If
                            End If
                            'FIN PRE_CUADRE_OFFLINE :: Modificado por TS-CCC
                        End If
                    End If

                    If Not CBool(Session("FlujoAnteriorCuadre")) Then   'Nuevo Flujo
                        If Not ViewState("MensajeDocumentosPend") Is Nothing Then
                            Me.RegisterStartupScript("exito", "<script language=javascript>alert('Proceso termino con éxito, pero existen documentos pendientes por pagar')</script>")
                            strMensaje = "Proceso termino con éxito, pero existen documentos pendientes por pagar"
                            Me.hidEstado.Value = "1" 'INICIATIVA-318 - ARQUEO
                        Else
                            Me.RegisterStartupScript("exito", "<script language=javascript>alert('Proceso terminado con éxito')</script>")
                            strMensaje = "Proceso terminado con éxito"
                            Me.hidEstado.Value = "1" 'INICIATIVA-318 - ARQUEO
                        End If

                        Me.setLogCuadreCaja(UCase(Request.Item("tipocuadre")), dblIngreso.ToString(), _
                                             txtCaja.Text, txtCajaCheque.Text, Math.Abs(CDbl(dblFaltante.ToString())).ToString(), _
                                             CDbl(txtRemesa.Text).ToString(), strMensaje, strNodo, strCajaAsignadaID)

                    End If

                End If
            End If
            objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
            'End If
        Catch ex1 As InvalidCastException
            Me.RegisterStartupScript("errorCuadre2", "<script language=javascript>alert('Monto ingresado invalido.')</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " ProcesoCuadre - ERROR :  " & ex1.Message)
        Catch ex As Exception
            Me.RegisterStartupScript("errorCuadre1", "<script language=javascript>alert('" & ex.Message & "')</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " ProcesoCuadre - ERROR :  " & ex.Message)
        Finally
            objAudiBus = Nothing
            objCaja = Nothing
            objclsOffline = Nothing
        End Try
    End Sub

    Private Sub bntForzado_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntForzado.Click
        ForzarCuadre()'INICIATIVA-318
    End Sub
'PROY-140397-MCKINSEY -> JSQ INICIO
    Private Function Validar_Documentos_Multipunto(ByVal dtDocumentos As DataTable, ByVal FechaConsulta As String) As Integer
        Dim contDocPresenciales As Integer = 0
        Dim contDocPresenciales2 As Integer = 0
        Dim totGenDoc As Integer = 0
        Dim FechaDocumento As String
        Dim strIdentifyLog As String = Session("ALMACEN") & "|" & strUsuario
        For Each dr As DataRow In dtDocumentos.Rows
            FechaDocumento = CDate(dr("PEDID_FECHADOCUMENTO")).ToString("dd/MM/yyyy")
            If (FechaDocumento = FechaConsulta) Then
            Dim sFlagMultiPunto As String = Trim(dr("FLAG_MULTIPUNTO"))
            Dim sPagocCodSunat As String = Trim(dr("PAGOC_CODSUNAT"))
                Dim sPedicClaseFac As String = Trim(dr("PEDIC_CLASEFACTURA"))
            If (sFlagMultiPunto = "0") Then
                contDocPresenciales += 1
                ElseIf (sFlagMultiPunto = "1" And sPagocCodSunat <> "0000000000000000" And sPedicClaseFac <> "0000") Then
                contDocPresenciales2 += 1
            End If
            End If
        Next
        totGenDoc = contDocPresenciales + contDocPresenciales2
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   cantDocPresenciales : " & contDocPresenciales)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   cantDocNoPresenciales : " & contDocPresenciales2)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Cant DocTotal Sin Pagar : " & totGenDoc)
        Return totGenDoc
    End Function
'PROY-140397-MCKINSEY -> JSQ FIN
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click

    End Sub

    Private Sub btnRecalcula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecalcula.Click
        'FFS INICIO (SE AGREGO METODO)

        '' AUTORIZACION DE TRANSACCION DE PRE-CUADRE TS-JTN
        If Not chkCierre.Checked Then
            Dim objConf As New COM_SIC_Configura.clsConfigura
            Dim autorizaOperacion As Boolean = False
            Dim tipoCuadre$ = UCase(Request.Item("tipocuadre"))
            Dim idOperacion As String = String.Format("CU-{0}", Session("USUARIO"))
            Dim intAutoriza% = objConf.FP_Inserta_Aut_Transac(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), Session("USUARIO"), Session("NOMBRE_COMPLETO"), "", "", _
                      "", "", "", idOperacion, 0, 5, 0, 0, 0, 0, 0, 0, "")

            autorizaOperacion = (intAutoriza = 1)
            If Not autorizaOperacion Then
                Dim script$ = "<script language=javascript>alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador.')</script>"
                Me.RegisterStartupScript("RegistraAlerta3", script)
                Exit Sub
            End If
        End If
        '' FIN AUTORIZACION DE TRANSACCION DE PRE-CUADRE TS-JTN

        objclsOffline = New COM_SIC_OffLine.clsOffline 'Modificado TS-CCC
        objCaja = New COM_SIC_Cajas.clsCajas 'Modificado TS-CCC
        Dim strIdentifyLog As String = Session("ALMACEN") & "|" & strUsuario 'Add TS-CCC
        Try
            Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
            Dim asignacionCajeroMensaje = objclsOffline.VerificarAsignacionCajero(Session("ALMACEN"), codUsuario, txtFecha.Text)
            If asignacionCajeroMensaje <> String.Empty Then
                Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", asignacionCajeroMensaje)
                Me.RegisterStartupScript("RegistraAlerta3", script)
                Exit Sub
            End If

            'INICIO PRE_CUADRE_OFFLINE :: Agregado por TS-CCC
            Dim dsResultadoCC As DataSet
            Dim strTipoCuadre As String = UCase(Request.Item("tipocuadre"))
            Dim strCierreCajaMensaje As String = String.Empty
            Dim strCajaBuzonMensaje As String = String.Empty
            Dim strMensajeGen As String

            If CBool(Session("FlujoAnteriorCuadre")) Then   'Flujo Anterior
                ''Response.Write("<script language=javascript>alert('Se procederá a actualizar los pagos y asignaciones de caja en SAP')</script>")
                Me.RegisterStartupScript("RegistraAlerta1", "<script language=javascript>alert('Se procederá a actualizar los pagos y asignaciones de caja en SAP')</script>")
            Else 'Nuevo Flujo
                If strTipoCuadre.Equals("I") Then
                    dsResultadoCC = objclsOffline.GetDatosAsignacionCajero(Session("ALMACEN"), txtFecha.Text, codUsuario)
                    If Not dsResultadoCC Is Nothing Then
                        For i = 0 To dsResultadoCC.Tables(0).Rows.Count - 1
                            If dsResultadoCC.Tables(0).Rows(i).Item("CAJA_CERRADA") <> "N" Then
                                asignacionCajeroMensaje = "La caja individual de la oficina de venta " & CStr(Session("ALMACEN")) & " ya ha sido cerrada."
                                Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", asignacionCajeroMensaje)
                                Me.RegisterStartupScript("RegistraAlertaCC", script)
                                Exit Sub
                            End If
                        Next
                    End If
                Else
                    strMensajeGen = objclsOffline.VerificarOficinaCerrada(Session("ALMACEN"), codUsuario, CDate(txtFecha.Text).ToString("yyyyMMdd"))
                    If strMensajeGen <> String.Empty Then
                        Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strMensajeGen)
                        Me.RegisterStartupScript("RegistraAlerta8", script)
                        Exit Sub
                    End If
                End If

                '<P_CAJA_BUZON> igual a cero el proceso termina indicando el error.
                'If CDbl(txtCaja.Text) = 0 Then
                '    strCajaBuzonMensaje = "Debe ingresar un monto a Caja Buzón."
                '    Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strCajaBuzonMensaje)
                '    Me.RegisterStartupScript("RegistraAlertaCB", script)
                '    Exit Sub
                'End If

                'Obtiene la caja asignada al cajero
                If strTipoCuadre.Equals("I") Then
                    strCajaAsignadaID = objclsOffline.ObtenerCajaAsignadaCuadreIndividual(Session("ALMACEN"), codUsuario, txtFecha.Text)
                Else
                    'Se obtiene la caja asignada en caso no este asignado una caja se asigna una.
                    strCajaAsignadaID = objclsOffline.ObtenerCajaAsignadaCuadreGeneral(Session("ALMACEN"), codUsuario, CDate(txtFecha.Text).ToString("yyyyMMdd"))
                    Dim strMensajeCajaAsignacion As String = String.Empty
                    If strCajaAsignadaID.Equals("0") Then
                        strMensajeCajaAsignacion = objclsOffline.SetTransCuadreCajeroGen(Session("ALMACEN"), CDate(txtFecha.Text).ToString("yyyyMMdd"), codUsuario, "N", strCajaAsignadaID)
                    End If

                    If Not strMensajeCajaAsignacion.Equals(String.Empty) Then
                        Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strMensajeCajaAsignacion)
                        Me.RegisterStartupScript("RegistraAlertaRCA", script)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "No se asigno caja. Mensaje: " & strMensajeCajaAsignacion)
                        Exit Sub
                    End If
                End If

                ' Se eliminan los registros de la Tabla <TI_MONTOS_CUAD> 
                Dim strMontosMensaje As String = String.Empty
                strMontosMensaje = objclsOffline.EliminarMontosVentas(Session("ALMACEN"), _
                                        codUsuario, CDate(txtFecha.Text).ToString("yyyyMMdd"), strTipoCuadre)
                If Not strMontosMensaje.Equals(String.Empty) Then
                    Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strMontosMensaje)
                    Me.RegisterStartupScript("RegistraAlertaMM", script)
                    Exit Sub
                End If

            End If
            'FIN PRE_CUADRE_OFFLINE :: Agregado por TS-CCC

            Dim mensajes(2) As String
            Dim mensajeAlert$
            'Dim 
            Dim procesaronPagos As Boolean = False
            If CBool(Session("FlujoAnteriorCuadre")) Then   'Flujo Anterior
                procesaronPagos = fIngresarPagosyAsignacionesSAP(mensajes)
            Else 'Nuevo Flujo
                procesaronPagos = fIngresarPagosSicar(mensajes)
            End If

            If procesaronPagos Then
                Dim dblTotEfecDia As Decimal
                Dim dblCuadreEfecDia As Decimal = 0
                Dim strMensaje As String = String.Empty
                Dim i As Integer
                'Response.Write("<script language=javascript>alert('Se actualizaron con éxito los pagos y asignaciones de caja en SAP.')</script>")
                mensajeAlert = String.Format("<script language=javascript>alert('{0}\n{1}\n{2}')</script>", mensajes(0), mensajes(1), mensajes(2))
                'dblTotEfecDia = objCaja.FP_CalculaEfectivoDia(Session("ALMACEN"), Session("USUARIO"), CDate(txtFecha.Text))
                dblTotEfecDia = objCaja.FP_CalculaEfectivoDia(Session("ALMACEN"), Session("USUARIO"), txtFecha.Text)

                Dim dblFaltante As Decimal = 0
                Dim dblSobrante As Decimal = 0
                Dim rDblFaltante As Decimal
                Dim rDblSobrante As Decimal
                Dim dblIngreso As Double

                If CBool(Session("FlujoAnteriorCuadre")) Then   'Flujo Anterior
                    dsReturn = objSapCaja.Set_CuadreCajero(txtFecha.Text, Session("ALMACEN"), CDbl(txtSaldo.Text), 0, CDbl(txtCaja.Text), CDbl(txtCajaCheque.Text), CDbl(txtRemesa.Text), 0, 0, "", strUsuario)
                    For i = 0 To dsReturn.Tables(0).Rows.Count - 1
                        If dsReturn.Tables(0).Rows(i).Item("TYPE") = "E" Then
                            'Response.Write("<script language=javascript>alert('" & dsReturn.Tables(0).Rows(i).Item("MESSAGE") & "')</script>")
                            Me.RegisterStartupScript("RegistraAlerta21", "<script language=javascript>alert('Se actualizó el saldo de caja')</script>")
                            Exit Sub
                        End If
                    Next
                    dsReturn = objPagos.Get_CuadreCajaResumDia(txtFecha.Text, Session("ALMACEN"), Session("USUARIO"))
                    For i = 0 To dsReturn.Tables(0).Rows.Count - 1

                        If Trim(UCase(dsReturn.Tables(0).Rows(i).Item("DSCUA"))) = ConfigurationSettings.AppSettings("gConstIngEfe") _
                            Or Trim(UCase(dsReturn.Tables(0).Rows(i).Item("DSCUA"))) = ConfigurationSettings.AppSettings("gConstDepGar") _
                            Or Trim(UCase(dsReturn.Tables(0).Rows(i).Item("DSCUA"))) = ConfigurationSettings.AppSettings("gConstDevol") Then

                            dblCuadreEfecDia += CDbl(dsReturn.Tables(0).Rows(i).Item("MONTO"))
                        End If
                    Next
                Else 'Nuevo Flujo
                    If txtIngreso.Text = "" Then
                        dblIngreso = 0
                    Else
                        dblIngreso = CDbl(txtIngreso.Text)
                    End If

                    objclsOffline = New COM_SIC_OffLine.clsOffline
                    Dim dMtoCajaBuzonAnterior As Double = 0
                    Dim dMtoCajaBuzonAnteriorPendiente As Double = 0
                    Dim dMtoRemesaAnteriorEnviadaHoy As Double = 0
                    Dim strFechaAnterior As String
                    Dim dRemesaHoy As Double = 0
                    Dim dMtoCajaBuzonAnteriorNoEnviadoAyer As Double = 0
                    Dim dsSobresRemesa As DataSet
                    Dim strFechaBuzon As String
                    Dim strFechaEnviada As String
                    Dim datFechaBuzon As Date

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio Recalcular - FP_BolsasLibres")
                    dsSobresRemesa = objCaja.FP_BolsasLibres(Session("ALMACEN"), "")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin Recalcular -  FP_BolsasLibres")

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI RECALCULAR: GetCajaBuzonAnterior")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Oficina Venta : " & CStr(Session("ALMACEN")))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha : " & txtFecha.Text)
                    dMtoCajaBuzonAnterior = objclsOffline.GetCajaBuzonAnterior(CStr(Session("ALMACEN")), txtFecha.Text, strFechaAnterior, dMtoCajaBuzonAnteriorPendiente, dMtoRemesaAnteriorEnviadaHoy, dMtoCajaBuzonAnteriorNoEnviadoAyer)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT MontoCajaBuzonAnterior : " & Math.Round(dMtoCajaBuzonAnterior, 2).ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT MontoCajaBuzonAnteriorPendiente : " & Math.Round(dMtoCajaBuzonAnteriorPendiente, 2).ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT Fecha Anterior : " & strFechaAnterior)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT MtoRemesaAnteriorEnviadaHoy : " & Math.Round(dMtoRemesaAnteriorEnviadaHoy, 2).ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin Recalcular - GetCajaBuzonAnterior")

                    If dMtoCajaBuzonAnteriorPendiente > 0 Then
                        If Not IsNothing(dsSobresRemesa) Then
                            For i = 0 To dsSobresRemesa.Tables(0).Rows.Count - 1
                                datFechaBuzon = dsSobresRemesa.Tables(0).Rows(i).Item("BUZON_FECHA")
                                strFechaBuzon = Format(Year(datFechaBuzon), "0000") & Format(Month(datFechaBuzon), "00") & Format(Day(datFechaBuzon), "00")
                                strFechaEnviada = Right(txtFecha.Text, 4) & Mid(txtFecha.Text, 4, 2) & Left(txtFecha.Text, 2)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha buzon : " & strFechaBuzon)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha Emviada : " & strFechaEnviada)

                                If strFechaBuzon < strFechaEnviada Then
                                    Dim strMsjEnvioRem As String = "El monto de envió de remesa " & Format(Math.Round(dMtoRemesaAnteriorEnviadaHoy, 2), "######0.00") & " es menor al total del Buzón anterior pendiente de enviar " & Format(Math.Round(dMtoCajaBuzonAnteriorNoEnviadoAyer, 2), "######0.00") & " del dia " & strFechaAnterior
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MENSAJE : No se ha enviado la remesa completa correspondiente.")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MENSAJE : " & strMsjEnvioRem)
                                    Me.RegisterStartupScript("noremesa1", "<script language=javascript>alert('" & strMsjEnvioRem & " ')</script>")
                                    Me.setLogCuadreCaja(UCase(Request.Item("tipocuadre")), txtIngreso.Text, txtCaja.Text, txtCajaCheque.Text, _
                                                    0, CDbl(txtRemesa.Text).ToString(), strMsjEnvioRem, strNodo, strCajaAsignadaID)
                                    If chkCierre.Checked Then
                                        Me.RegisterStartupScript("noremesa2", "<script language=javascript>alert('No fue posible cierre debido a diferencia de remesa.')</script>")
                                        Exit Sub 'Termina flujo
                                    Else
                                        Exit For 'Continua Flujo
                                    End If
                                End If
                            Next
                        End If
                    Else
                        'Valida Hay remesas enviadas de sobre generados el dia de hoy
                        Dim strMensajeRemesaAdic As String = String.Empty
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio Recalcular - GetRemesaHoy")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp ALMACEN: " & Session("ALMACEN"))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtFecha: " & txtFecha.Text)
                        dRemesaHoy = objclsOffline.GetRemesaHoy(Session("ALMACEN"), txtFecha.Text)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out Remesa Hoy: " & Math.Round(dRemesaHoy, 2).ToString())
                        strMensajeRemesaAdic = "Se envió : " & Format(Math.Round(dRemesaHoy, 2), "######0.00") & " de remesas de sobres del dia de hoy : " & txtFecha.Text
                        If dRemesaHoy > 0 Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MENSAJE : " & strMensajeRemesaAdic)
                            Me.RegisterStartupScript("noremesa2", "<script language=javascript>alert('" & strMensajeRemesaAdic & " ')</script>")
                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin Recalcular  - GetRemesaHoy")
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio Recalcular - setCuadreMontos")

                    strMensaje = setCuadreMontos(UCase(Request.Item("tipocuadre")), dblSobrante, _
                                                    dblFaltante, True, dMtoCajaBuzonAnterior, _
                                                    strFechaAnterior, dRemesaHoy, dMtoRemesaAnteriorEnviadaHoy, _
                                                    dMtoCajaBuzonAnteriorPendiente, rDblSobrante, rDblFaltante)

                    dblSobrante = rDblSobrante
                    dblFaltante = rDblFaltante
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin Recalcular setCuadreMontos")

                    If Len(Trim(strMensaje)) > 0 Then
                        Me.RegisterStartupScript("errorCuadre", "<script language=javascript>alert('" & strMensaje & "')</script>")
                        Me.setLogCuadreCaja(UCase(Request.Item("tipocuadre")), dblIngreso.ToString(), txtCaja.Text, txtCajaCheque.Text, _
                                                                dblFaltante.ToString(), CDbl(txtRemesa.Text).ToString(), strMensaje, strNodo, strCajaAsignadaID)
                        Exit Sub
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI Recalcular - GetMontoCuadre")
                    objclsOffline = New COM_SIC_OffLine.clsOffline

                    'If strTipoCuadre.Equals("I") Then
                    '    dsReturn = objclsOffline.GetMontoCuadre(CStr(Session("ALMACEN")), CDate(txtFecha.Text).ToString("yyyyMMdd"), codUsuario)
                    'Else
                    '    dsReturn = objclsOffline.GetMontoCuadre(CStr(Session("ALMACEN")), CDate(txtFecha.Text).ToString("yyyyMMdd"), "0")
                    'End If

                    'For i = 0 To dsReturn.Tables(0).Rows.Count - 1
                    '    If Trim(UCase(dsReturn.Tables(0).Rows(i).Item("DESC_CONCEPTO"))) = Trim(UCase(ConfigurationSettings.AppSettings("gConstIngEfe"))) Then
                    '        dblCuadreEfecDia = CDbl(dsReturn.Tables(0).Rows(i).Item("MONTO"))
                    '        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT Cuadre_Efectivo_dia : " & dblCuadreEfecDia.ToString())
                    '        Exit For
                    '    End If
                    'Next

                    'Se cambio para ajustar el efectivo de conf_efectivocaja
                    dblCuadreEfecDia = objclsOffline.GetCalcularEfeCaja(CStr(Session("ALMACEN")), codUsuario, CDate(txtFecha.Text).ToString("yyyyMMdd"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN Recalcular - GetMontoCuadre")
                    'FIN :: MODIFICADO :: Modificado por TS-CCC
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI Recalcular - FP_InsertaEfectivo")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Oficina Vta : " & CStr(Session("ALMACEN")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Cajero : " & CStr(Session("USUARIO")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Diferencia de efectivo : " & CStr((dblCuadreEfecDia - dblTotEfecDia)))

                If Math.Round(dblTotEfecDia, 2) <> Math.Round(dblCuadreEfecDia, 2) Then
                    objCaja.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), (dblCuadreEfecDia - dblTotEfecDia), txtFecha.Text)  'Se inserta la diferencia para nivelar
                    strMensaje = "Se actualizó el saldo de caja."
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: btnRecalcula_Click)"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT MENSAJE : " & strMensaje & MaptPath)
                    'FIN PROY-140126 

                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN Recalcular - FP_InsertaEfectivo")

                If CBool(Session("FlujoAnteriorCuadre")) Then   'Flujo Anterior
                    'Response.Write("<script language=javascript>alert('Se actualizó el saldo de caja')</script>")
                    Me.RegisterStartupScript("RegistraAlerta21", "<script language=javascript>alert('Se actualizó el saldo de caja')</script>")
                Else 'Nuevo FLujo
                    'INICIO :: Agregado por TS-CCC
                    If Len(strMensaje) = 0 Then
                        strMensaje = "El proceso de recalculo se realizó correctamente."
                    End If
                    Me.RegisterStartupScript("RegistraAlerta21", "<script language=javascript>alert('" & strMensaje & "')</script>")
                    Me.setLogCuadreCaja(UCase(Request.Item("tipocuadre")), dblIngreso.ToString(), _
                                            txtCaja.Text, txtCajaCheque.Text, Math.Abs(CDbl(dblFaltante.ToString())).ToString(), _
                                            CDbl(txtRemesa.Text).ToString(), strMensaje, strNodo, strCajaAsignadaID)
                    'FIN :: Agregado por TS-CCC
                End If
            Else
                'Response.Write("<script language=javascript>alert('No se actualizaron los pagos y asignaciones de caja en SAP. Intente mas tarde')</script>")
                mensajeAlert = String.Format("<script language=javascript>alert('Error al procesar las transacciones: \n{0}\n{1}\n{2}');</script>", mensajes(0), mensajes(1), mensajes(2))
                Me.RegisterStartupScript("RegistraAlerta20", mensajeAlert)
                If (Not procesaronPagos) Then
                    Me.RegisterStartupScript("Registra", "<script>proceSar();</script>")
                End If
            End If
            'FFS FIN
        Catch ex As Exception
            Me.RegisterStartupScript("errorCuadre1", "<script language=javascript>alert('" & ex.Message & "')</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " ProcesoCuadre - Recalcular - ERROR :  " & ex.Message)
        Finally
            objclsOffline = Nothing
            objCaja = Nothing
        End Try
    End Sub

    Private Function ValidaEmisionDocPendienteMiClaro() As Boolean
        Dim Procesar As Boolean = False
        If ConfigurationSettings.AppSettings("constFlagPasarelaPagoMC").ToString() = "1" And UCase(Request.Item("tipocuadre")) <> "I" Then
            Dim dsVentasCEL As DataSet
            Dim i As Integer
            Dim oConsulta As New COM_SIC_Cajas.clsConsultaGeneral
            Dim strIdentifyLog As String = Session("ALMACEN")
            Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "------Incio FP_Get_ListaVentasCEL------")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ALMACEN: " & Session("ALMACEN"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "txtFecha.Text: " & txtFecha.Text)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "tipo: " & "I")
                dsVentasCEL = oConsulta.FP_Get_ListaVentasCEL(Session("ALMACEN"), txtFecha.Text, "I", "", "", "")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "dsVentasCEL.Tables.Count:" & dsVentasCEL.Tables.Count.ToString())
                If dsVentasCEL.Tables.Count > 0 Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "dsVentasCEL.Tables(0).Rows.Count:" & dsVentasCEL.Tables(0).Rows.Count.ToString())
                    If dsVentasCEL.Tables(0).Rows.Count > 0 Then
                        Procesar = False
                    Else
                        Procesar = True
                    End If
                Else
                    Procesar = True
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "------Fin FP_Get_ListaVentasCEL---------")
                If (Procesar = True) Then
                    ProcesoCuadre(True)
                Else
                    Response.Write("<script>alert('El punto de venta cuenta con pedidos de chip repuesto pregago pendiente de emisión de comprobante.');</script>")
                End If
            Catch ex As Exception
                'INI PROY-140126
                Dim MaptPath As String
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: ValidaEmisionDocPendienteMiClaro)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR: " & ex.Message.ToString() & MaptPath)
                'FIN PROY-140126

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR: " & ex.StackTrace)
            Finally
                oConsulta = Nothing
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- PROCESAR CUARE DE CAJA?" & Procesar.ToString)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ProcesoCuadre.")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            End Try
        Else
            Procesar = True
        End If
        Return Procesar
    End Function

    Private Function fIngresarPagosyAsignacionesSAP() As Boolean
        Dim vExito As Boolean
        Try
            'Batch de ASIGNACION DE CAJAS
            objAsignacionCajas.RegistrarTransaccionSAP(txtFecha.Text)
            'Batch de PAGOS
            objPagoFijoPaginas.RegistrarTransaccionSAP(txtFecha.Text)
            'Anulaciones de Pagos
            objAnulaciones.AnularTransaccionSAP(txtFecha.Text)
            'Batch de PAGOS RECARGA VIRTUAL Y DTH    
            objPedido.MigrarPedidoPago(txtFecha.Text)
            vExito = True
        Catch ex As Exception
            vExito = False
        End Try

        Return vExito
    End Function

    Private Function fIngresarPagosyAsignacionesSAP(ByRef mensajes() As String) As Boolean
        Dim vExito As Boolean = False
        Dim mensajeAsignacionCajas$, mensajeAnulaciones$, mensajePagoFijo$, mensajePedido$

        Dim objOffline As New COM_SIC_OffLine.clsOffline

        mensajeAsignacionCajas = String.Format("{0}|{1}", Session("ALMACEN"), strUsuario)
        mensajePagoFijo = mensajeAsignacionCajas
        mensajeAnulaciones = mensajeAsignacionCajas
        mensajePedido = mensajeAsignacionCajas

        Dim oficinaVenta As String = Session("ALMACEN")
        Dim cajero = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
        Dim caja$ = ""

        Dim strIdentifyLog As String = Session("ALMACEN") & "|" & strUsuario
        Dim ejecucionAsignacionCajas, ejecucionRecaudaciones, ejecucionAnulacionRecaudo, ejecucionPedido As Boolean

        ejecucionAsignacionCajas = objAsignacionCajas.RegistrarTransaccionSAP(txtFecha.Text, oficinaVenta, mensajeAsignacionCajas)
        ejecucionRecaudaciones = objPagoFijoPaginas.RegistrarTransaccionSAP(txtFecha.Text, Session("USUARIO"), oficinaVenta, mensajePagoFijo)
        ejecucionAnulacionRecaudo = objAnulaciones.AnularTransaccionSAP(txtFecha.Text, Session("USUARIO"), oficinaVenta, mensajeAnulaciones)

        If (objOffline.enviandoPedidosPDV(Session("ALMACEN"))) Then
            mensajePedido = "No se pueden enviar transacciones de pedidos, intente en unos minutos."
            ejecucionPedido = True
        Else
            ejecucionPedido = objPedido.MigrarPedidoPago(txtFecha.Text, cajero, oficinaVenta, mensajePedido)
        End If

        Try
            vExito = ejecucionAsignacionCajas And _
                     ejecucionRecaudaciones And _
                     ejecucionAnulacionRecaudo And _
                     ejecucionPedido

            mensajes(0) = mensajeAsignacionCajas
            mensajes(1) = mensajePagoFijo
            mensajes(2) = mensajePedido
            'vExito = True
        Catch ex As Exception
            mensajes(0) = mensajeAsignacionCajas
            mensajes(1) = mensajePagoFijo
            mensajes(2) = mensajePedido
            vExito = False
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "EJECUCION " & IIf(ejecucionAsignacionCajas, "OK", "ERROR") & " RESPUESTA ENVIO ASIGNACION CAJAS: " & mensajeAsignacionCajas)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "EJECUCION " & IIf(ejecucionPedido, "OK", "ERROR") & " RESPUESTA ENVIO PEDIDOS: " & mensajePedido)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "EJECUCION " & IIf(ejecucionRecaudaciones, "OK", "ERROR") & " RESPUESTA ENVIO RECAUDACIONES: " & mensajePagoFijo)
        End Try
        Return vExito
    End Function
    'Dim strIdentifyLog As String = Session("ALMACEN") & "|" & strUsuario

    Private Sub procesarHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles procesarHandler.Click
        ProcesoCuadre(True)
    End Sub

    'INICIO PRE_CUADRE_OFFLINE :: Agregado por TS-CCC
    Private Sub setLogCuadreCaja(ByVal TipoCuadre As String, _
                                    ByVal ingresoEfectivo As String, ByVal cajaBuzon As String, ByVal cajaBuzonCheque As String, _
                                    ByVal montoFaltante As String, ByVal remesa As String, ByVal comentario As String, _
                                    ByVal nodo As String, ByVal idCajaAsignada As String)

        Dim strMensaje As String = String.Empty
        Dim strIdentifyLog As String = Session("ALMACEN") & "|" & strUsuario
        objclsOffline = New COM_SIC_OffLine.clsOffline
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "setCuadreCajaGen :: INICIO DE LOG CUADRE DE CAJA")
            If TipoCuadre.Equals("I") Then
                strMensaje = objclsOffline.SetLogCuadreDia(idCajaAsignada, ingresoEfectivo, cajaBuzon, _
                                cajaBuzonCheque, montoFaltante, comentario, nodo)

                If Not strMensaje.Equals(String.Empty) Then 'En caso de Error en BD_SICAR
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   strMensaje: " & strMensaje)
                    Exit Sub
                End If
            Else
                strMensaje = objclsOffline.SetLogCuadreGen(idCajaAsignada, ingresoEfectivo, cajaBuzon, _
                            cajaBuzonCheque, montoFaltante, remesa, comentario, nodo)

                If Not strMensaje.Equals(String.Empty) Then 'En caso de Error en BD_SICAR
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   strMensaje: " & strMensaje)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "setCuadreCajaGen:: ERROR AL REGISTRAR LOG CUADRE DE CAJA " & " Mensaje error: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "setCuadreCajaGen :: FIN DE LOG CUADRE DE CAJA")
        End Try
    End Sub

    Private Function setCuadreMontos(ByVal tipoCuadre As String, ByVal mtoSobrante As Decimal, _
                                ByVal mtoFaltante As Decimal, ByVal blnValidacion As Boolean, _
                                ByVal mtoCajabuzonAnterior As Double, _
                                ByVal strFechaAnt As String, _
                                ByVal remesaHoy As Double, _
                                ByVal mtoRemesaAnteriorEnviadaHoy As Double, _
                                ByVal mtoCajaBuzonAnteriorPendiente As Double, _
                                ByRef rMtoSobrante As Decimal, ByRef rMtoFaltante As Decimal) As String
        Dim strIdentifyLog As String = Session("ALMACEN") & "|" & strUsuario
        objclsOffline = New COM_SIC_OffLine.clsOffline
        Try
            Dim strCodOfi As String = CStr(Session("ALMACEN"))
            Dim strOficina As String = CStr(Session("OFICINA"))
            Dim strCodCajero As String = CStr(Session("USUARIO"))
            Dim strCodUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
            Dim strNombreCajero As String = CStr(Session("NOMBRE_COMPLETO"))
            Dim strFechaCierre As String = String.Empty
            Dim strHoraCierre As String = String.Empty
            Dim strCaja As String = String.Empty
            Dim arrayConceptosCuadre(300) As String
            Dim arrayValoresCuadre(300) As String
            Dim dblTotalFacturado, dblTotalFactdoBol, dblTotalFactdoFac As Double
            Dim dsDatosCalculados As DataSet
            Dim dsEstructuraCaja As DataSet
            Dim drDatosCalculados As DataRow
            Dim strMensajeErr As String = String.Empty
            Dim dMtoCajaBuzon As Double = CDbl(txtCaja.Text)
            Dim dMtoRemesaHoy As Double = 0
            Dim dMtoRemesaAyer As Double = 0
            Dim dMtoDiaRemesa As Double = 0
            Dim dMtoDia As Decimal = 0
            Dim dMtoRemesa As Double = 0
            Dim bDatosCompletos As Boolean = False, bProcesoExito As Boolean = False
            Dim iArrayLenght As Int32 = 0
            Dim strCodCaje As String = String.Empty
            Dim dMtoCajaBuzonAnterior As Double = mtoCajabuzonAnterior
            Dim dMtoRemesaAnteriorEnviadaHoy As Double = mtoRemesaAnteriorEnviadaHoy
            Dim dMtoCajaBuzonAnteriorPendiente As Double = mtoCajaBuzonAnteriorPendiente
            Dim strFechaAnterior As String = strFechaAnt
            Dim strMensajeRemesaInfo As String, strMensajeRemesa As String = String.Empty

            If Not txtRemesa.Text.Equals(String.Empty) Then
                dMtoRemesa = CDbl(txtRemesa.Text)
            End If

            'Datos Calculados
            rMtoSobrante = mtoSobrante
            rMtoFaltante = mtoFaltante

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Inicio: GetDatosAsignacionCajero")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Oficina Venta : " & CStr(Session("ALMACEN")))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Fecha : " & txtFecha.Text)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Cajero : " & strCodUsuario)
            Dim dsDatosCaja As DataSet
            If tipoCuadre.Equals("I") Then
                dsDatosCaja = objclsOffline.GetDatosAsignacionCajero(CStr(Session("ALMACEN")), txtFecha.Text, strCodUsuario)
            Else
                dsDatosCaja = objclsOffline.GetDatosAsignacionGeneral(CStr(Session("ALMACEN")), CDate(txtFecha.Text).ToString("yyyyMMdd"), strCodUsuario)
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - OUT Respueta : OK")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Fin: GetDatosAsignacionCajero")

            'INI: Se iguala el monto de caja buzon anterior con el monto Remesa solo para el dia del pase a prod.
            'Dim strFechaLim As String = ConfigurationSettings.AppSettings("FechaFinConsultaRFC_RepCuadre")
            Dim flagSinergia As String
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Inicio: GetFechaOficinaSinergia")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  IN OFICINA : " & CStr(Session("ALMACEN")))
            Dim strFechaLim As String = objclsOffline.GetFechaOficinaSinergia(CStr(Session("ALMACEN")), flagSinergia)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  OUT FECHA : " & strFechaLim)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  OUT FLAG : " & flagSinergia)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Fin: GetFechaOficinaSinergia")
            Dim anio As Integer = CInt(strFechaLim.Substring(6, 4))
            Dim mes As Integer = CInt(strFechaLim.Substring(3, 2))
            Dim dia As Integer = CInt(strFechaLim.Substring(0, 2))
            Dim dFechaLim As New Date(anio, mes, dia)
            Dim dFecha As Date = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)

            If dFecha = dFechaLim Then
                dMtoCajaBuzonAnterior = CDbl(txtRemesa.Text)
            End If
            'FIN: Se iguala el monto de caja buzon anterior con el monto Remesa solo para el dia del pase a prod.

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Inicio: GetDatosCuadre")
            If tipoCuadre.Equals("I") Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Inicio: GetDatosCuadre")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP TipoCuadre : I")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Oficina Vta : " & CStr(Session("ALMACEN")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Fecha : " & CDate(txtFecha.Text).ToString("yyyyMMdd"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Cajero : " & strCodUsuario)
                dsDatosCalculados = objclsOffline.GetDatosCuadre(Funciones.CheckStr(Session("ALMACEN")), CDate(txtFecha.Text).ToString("yyyyMMdd"), strCodUsuario)
                strCodCaje = strCodUsuario
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Inicio: GetDatosCuadre")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP TipoCuadre : G")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Oficina Vta : " & CStr(Session("ALMACEN")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Fecha : " & CDate(txtFecha.Text).ToString("yyyyMMdd"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Cajero : 0")
                dsDatosCalculados = objclsOffline.GetDatosCuadre(CStr(Session("ALMACEN")), CDate(txtFecha.Text).ToString("yyyyMMdd"), "0")
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Fin: GetDatosCuadre")

            If Not dsDatosCaja Is Nothing Then
                For Each row As DataRow In dsDatosCaja.Tables(0).Rows
                    If tipoCuadre.Equals("I") Then
                        If Not IsDBNull(row("FECHA_CIERRE")) Then
                            strFechaCierre = CDate(row("FECHA_CIERRE")).ToString("dd/MM/yyyy")
                            strHoraCierre = CDate(row("FECHA_CIERRE")).ToString("hh:mm:ss")
                        Else
                            strFechaCierre = Date.Now.ToString("dd/MM/yyyy")
                            strHoraCierre = Date.Now.ToString("HH:mm:ss")
                        End If
                        strCaja = CStr(row("CAJA"))
                    Else
                        If row("CUADRE_REALIZADO") = "S" Then
                            strFechaCierre = CDate(row("FECHA_CIERRE")).ToString("dd/MM/yyyy")
                            strHoraCierre = CDate(row("FECHA_CIERRE")).ToString("hh:mm:ss")
                        Else
                            strFechaCierre = Date.Now.ToString("dd/MM/yyyy")
                            strHoraCierre = Date.Now.ToString("HH:mm:ss")
                        End If
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosAsignacionCajero - Fecha Cierre: " & strFechaCierre)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosAsignacionCajero - Hora Cierre: " & strHoraCierre)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosAsignacionCajero - Caja: " & strCaja)
                    Exit For
                Next
            End If

            If Not IsNothing(dsDatosCalculados) Then
                If dsDatosCalculados.Tables(0).Rows.Count > 0 Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - Datos encontrados ")
                    drDatosCalculados = dsDatosCalculados.Tables(0).Rows(0)  'Primera fila.
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - Datos no encontrados")
                    drDatosCalculados = Nothing
                End If

                If Not IsNothing(drDatosCalculados) Then
                    Dim dMtoDevEfec As Double = CDbl(drDatosCalculados("MTO_DEV_EFEC"))
                    Dim dMtoEfectivo As Double = CDbl(drDatosCalculados("MTO_EFEC"))
                    'valida total de cheques sea igual a Caja buzon cheque
                    Dim dTotalChq As Double = CDbl(drDatosCalculados("ING_TOT_CHQ"))

                    'PROY-27440 - INICIO'
                    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "   VALIDANDO SWTICH INTEGRAR CON POS : " & HidIntAutPos.Value)
                    If HidIntAutPos.Value = "1" Then
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencia SICAR - POS - INICIO")
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "DIFERENCIA VISA")
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "VISA - SICAR :" & drDatosCalculados("TOT_VNT_VIS"))
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "VISA - DEVOLUCION :" & drDatosCalculados("TOT_VNT_VIS_DEV"))
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "VISA - POS :" & drDatosCalculados("TOT_VNT_VIS_POS"))
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "DIFERENCIA MASTERCARD")
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "MCD - SICAR :" & drDatosCalculados("TOT_VNT_MC"))
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "MCD - DEVOLUCION :" & drDatosCalculados("TOT_VNT_MC_DEV"))
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "MCD - POS :" & drDatosCalculados("TOT_VNT_MC_POS"))
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "DIFERENCIA AMERICAN EXPRESS")
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "AMEX - SICAR :" & drDatosCalculados("TOT_VNT_AMX"))
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "AMEX - DEVOLUCION :" & drDatosCalculados("TOT_VNT_AMX_DEV"))
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "AMEX - POS :" & drDatosCalculados("TOT_VNT_AMX_POS"))
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "DIFERENCIA DINERS")
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "DINERS - SICAR :" & drDatosCalculados("TOT_VNT_DIN"))
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "DINERS - DEVOLUCION :" & drDatosCalculados("TOT_VNT_DIN_DEV"))
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "DINERS - POS :" & drDatosCalculados("TOT_VNT_DIN_POS"))
                        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Cuadre Caja | " & strIdentifyLog & " - " & "Diferencia SICAR - POS - FIN")

                        Dim dTotalVisa As Double = CDbl(drDatosCalculados("TOT_VNT_VIS"))
                        Dim dTotalVisaDev As Double = CDbl(drDatosCalculados("TOT_VNT_VIS_DEV"))
                        Dim dTotalVisaPos As Double = CDbl(drDatosCalculados("TOT_VNT_VIS_POS"))
                        Dim dTotaMCD As Double = CDbl(drDatosCalculados("TOT_VNT_MC"))
                        Dim dTotalMCDDev As Double = CDbl(drDatosCalculados("TOT_VNT_MC_DEV"))
                        Dim dTotalMCDPos As Double = CDbl(drDatosCalculados("TOT_VNT_MC_POS"))

                        Dim dTotalAmex As Double = CDbl(drDatosCalculados("TOT_VNT_AMX"))
                        Dim dTotalAmexDev As Double = CDbl(drDatosCalculados("TOT_VNT_AMX_DEV"))
                        Dim dTotalAmexPos As Double = CDbl(drDatosCalculados("TOT_VNT_AMX_POS"))
                        Dim dTotalDiners As Double = CDbl(drDatosCalculados("TOT_VNT_DIN"))
                        Dim dTotalDinersDev As Double = CDbl(drDatosCalculados("TOT_VNT_DIN_DEV"))
                        Dim dTotalDinersPos As Double = CDbl(drDatosCalculados("TOT_VNT_DIN_POS"))

                        txtDifVisa.Text = Format(CDbl(dTotalVisa - dTotalVisaPos + dTotalVisaDev), "######0.00")
                        txtDifMCD.Text = Format(CDbl(dTotaMCD - dTotalMCDPos + dTotalMCDDev), "######0.00")
                        txtDifAmex.Text = Format(CDbl(dTotalAmex - dTotalAmexPos + dTotalAmexDev), "######0.00")
                        txtDifDiners.Text = Format(CDbl(dTotalDiners - dTotalDinersPos + dTotalDinersDev), "######0.00")
                    End If

                    'PROY-27440 - FIN'

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - Monto efectivo: " & dMtoEfectivo.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - Monto devol. efectivo: -" & dMtoDevEfec.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - Ingreso Cheque: " & dTotalChq.ToString())

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INI Validar Montos Cheque")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Total Cheque Base : " & dTotalChq.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Total Cheque buzon: " & txtCajaCheque.Text)
                    If dTotalChq <> CDbl(txtCajaCheque.Text) Then
                        strMensajeErr = "Ingreso Total Cheque es diferente a Caja Buzon Cheque."
                        Dim dDiferenciaCheque As Double = dTotalChq - CDbl(txtCajaCheque.Text)
                        If dTotalChq > CDbl(txtCajaCheque.Text) Then
                            setCuadreMontos = strMensajeErr & " El ingreso faltante en cheque es: " & Format(Math.Abs(dDiferenciaCheque), "######0.00")
                        Else
                            setCuadreMontos = strMensajeErr & " El ingreso sobramte en cheque es: " & Format(Math.Abs(dDiferenciaCheque), "######0.00")
                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - Mensaje: " & setCuadreMontos)
                        Exit Function
                    End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - FIN Valida Montos Cheque")

                    If tipoCuadre.Equals("I") Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INI Valida MontoDIA")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - TipoCuadre : I")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INP Monto Efectivo : " & dMtoEfectivo.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INP Monto Sobrante : " & mtoSobrante.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INP Monto Faltante : " & mtoFaltante.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INP Monto Devolucion Efectivo : -" & dMtoDevEfec.ToString())
                        dMtoDia = Math.Round(dMtoEfectivo, 2) + Math.Round(mtoSobrante, 2) + Math.Round(mtoFaltante, 2) + (Math.Round(dMtoDevEfec, 2) * -1)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - OUT Monto Dia : " & dMtoDia.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INT Monto Caja Ingresado : " & txtCaja.Text)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INI Valida mtoSobrante")
                        If Not Math.Round(Math.Abs(mtoFaltante), 2) > 0 Then
                            If CDbl(txtCaja.Text) > Math.Abs(Math.Round(dMtoDia, 2)) Then
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - IN Monto Caja Buzon: " & txtCaja.Text)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - IN Monto dMtoEfectivo: " & dMtoEfectivo.ToString())
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - IN Monto dMtoDevEfec: " & dMtoDevEfec.ToString())
                                mtoSobrante = CDbl(txtCaja.Text) - Math.Round(dMtoEfectivo, 2) + Math.Round(dMtoDevEfec, 2)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - OUT Monto sobrante: " & mtoSobrante.ToString())
                            End If
                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - FIN Valida mtoSobrante")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - FIN Valida MontoDIA")
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INI Valida MontoDIA")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - TipoCuadre : G")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INP Monto Efectivo : " & dMtoEfectivo.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INP Monto Sobrante : " & mtoSobrante.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INP Monto Faltante : " & mtoFaltante.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INP Monto Devolucion Efectivo : -" & dMtoDevEfec.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INP Monto Caja Buzon Anterior : " & Math.Round(dMtoCajaBuzonAnterior, 2).ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INP Monto Remesa : " & dMtoRemesa.ToString())

                        ''MtoSobrante
                        dMtoDia = Math.Round(dMtoEfectivo, 2) + Math.Round(mtoSobrante, 2) + Math.Round(mtoFaltante, 2) + (Math.Round(dMtoDevEfec, 2) * -1)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - OUT Monto Dia : " & dMtoDia.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INT Monto Caja Ingresado : " & txtCaja.Text)

                        If Not Math.Round(Math.Abs(mtoFaltante), 2) > 0 Then
                            If CDbl(txtCaja.Text) > Math.Abs(Math.Round(dMtoDia, 2)) Then
                                mtoSobrante = CDbl(txtCaja.Text) - Math.Abs(Math.Round(dMtoDia, 2))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INT Monto Sobrante : " & mtoSobrante.ToString())
                            End If
                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - FIN Valida MontoDIA")

                        'MtoRemesaAyer, MtoRemesaHoy
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INI Valida Remesa")
                        If dMtoRemesa > 0 Then
                            If dMtoRemesa > Math.Round(dMtoRemesaAnteriorEnviadaHoy, 2) Then
                                dMtoRemesaHoy = Math.Round(dMtoRemesa, 2) - Math.Round(dMtoRemesaAnteriorEnviadaHoy, 2)
                                dMtoRemesaAyer = Math.Round(dMtoRemesaAnteriorEnviadaHoy, 2)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INT Monto Remesa Hoy : " & dMtoRemesaHoy.ToString())
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INT Monto Remesa Ayer : " & dMtoRemesaAyer.ToString())
                            Else
                                dMtoRemesaHoy = 0
                                dMtoRemesaAyer = Math.Round(dMtoRemesa, 2)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INT Monto Remesa Ayer : " & dMtoRemesaAyer.ToString())
                            End If

                            dMtoDiaRemesa = Math.Round(dMtoEfectivo, 2) + Math.Round(mtoSobrante, 2) + Math.Round(mtoFaltante, 2) + (Math.Round(dMtoDevEfec, 2) * -1) + Math.Round(mtoCajabuzonAnterior, 2)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - OUT Monto Dia Remesa : " & dMtoDiaRemesa.ToString())
                            If blnValidacion Then 'Forzar Cuadre
                                If Math.Round(dMtoRemesa, 2) > Math.Round(dMtoDiaRemesa, 2) Then
                                    setCuadreMontos = "Monto excesivo para remesa."
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - Mensaje: " & setCuadreMontos)
                                    Exit Function
                                End If
                            End If
                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - FIN Valida Remesa")

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INI Resultados validados")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - INP Caja buzon: " & txtCaja.Text)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - OUT Monto dia: " & dMtoDia.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - OUT Monto sobrante: " & mtoSobrante.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - OUT Remesa: " & dMtoDiaRemesa.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - OUT Remesa ayer: " & dMtoRemesaAyer.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - GetDatosCuadre - OUT Remesa hoy: " & dMtoRemesaHoy.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - FIN Resultados validados")
                    End If

                    'PROY-27440 - INICIO'
                    'Obtiene la estructura de cuadre de caja
                    Dim strTipCuadr As String = String.Empty
                    If tipoCuadre.Equals("I") Then
                        strTipCuadr = "I"
                    Else
                        strTipCuadr = "G"
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Inicio : GetEstructuraCuadre")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP TipoCuadre : " & strTipCuadr)
                    dsEstructuraCaja = objclsOffline.GetEstructuraCuadre(strTipCuadr)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - filas : " & dsEstructuraCaja.Tables(0).Rows.Count().ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Resultado : OK")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Fin : GetEstructuraCuadre")

                    Try
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Inicio: Generar estructura de cuadre caja")
                        dsDatosCalculados.Tables(1).Columns.Add("ADD", GetType(String))
                        dsDatosCalculados.Tables(2).Columns.Add("ADD", GetType(String))
                        dsDatosCalculados.Tables(3).Columns.Add("ADD", GetType(String))
                        dsDatosCalculados.Tables(4).Columns.Add("ADD", GetType(String))
                        dsDatosCalculados.Tables(5).Columns.Add("ADD", GetType(String))
                        dsDatosCalculados.Tables(6).Columns.Add("ADD", GetType(String))
                        dsDatosCalculados.Tables(7).Columns.Add("ADD", GetType(String))
                        dsDatosCalculados.Tables(8).Columns.Add("ADD", GetType(String))

                        For Each row As DataRow In dsEstructuraCaja.Tables(0).Rows
                            Dim strDescripcion As String = String.Empty
                            Dim strComentario As String = String.Empty
                            Dim strDetalle As String = String.Empty
                            Dim strSubDetalle As String = String.Empty

                            'Comentarios
                            If Not IsDBNull(row("COMENTARIO")) Then
                                If CStr(row("COMENTARIO")).IndexOf("<COD_OFICINA>") <> -1 Then
                                    strComentario = row("COMENTARIO")
                                    strComentario = strComentario.Replace("<COD_OFICINA>", strCodOfi)
                                    strComentario = strComentario.Replace("<NOMBRE_OFICINA>", strOficina.Trim())
                                ElseIf CStr(row("COMENTARIO")).IndexOf("<COD_CAJERO>") <> -1 Then
                                    strComentario = row("COMENTARIO")
                                    strComentario = strComentario.Replace("<COD_CAJERO>", strCodCajero)
                                    strComentario = strComentario.Replace("<NOMBRE_CAJERO>", strNombreCajero.Trim())
                                ElseIf CStr(row("COMENTARIO")).IndexOf("<CAJA_ASIGNADA>") <> -1 Then
                                    strComentario = row("COMENTARIO")
                                    strComentario = strComentario.Replace("<CAJA_ASIGNADA>", strCaja)
                                ElseIf CStr(row("COMENTARIO")).IndexOf("<FECHA_PROC_CUADRE>") <> -1 Then
                                    strComentario = row("COMENTARIO")
                                    strComentario = strComentario.Replace("<FECHA_PROC_CUADRE>", strFechaCierre)
                                ElseIf CStr(row("COMENTARIO")).IndexOf("<HORA_PROC_CUADRE>") <> -1 Then
                                    strComentario = row("COMENTARIO")
                                    strComentario = strComentario.Replace("<HORA_PROC_CUADRE>", strHoraCierre)
                                Else
                                    strComentario = CStr(row("COMENTARIO"))
                                End If

                            Else
                                strComentario = "--"
                            End If

                            'Detalle
                            If Not IsDBNull(row("SUBDETALLE")) Then
                                strDetalle = "--"
                            ElseIf IsDBNull(row("SUBDETALLE")) And Not IsDBNull(row("COMENTARIO")) Then
                                strDetalle = String.Empty
                            Else
                                strDetalle = CStr(row("DETALLE"))
                            End If

                            'Subdetalle

                            If IsDBNull(row("SUBDETALLE")) Then
                                strSubDetalle = String.Empty
                            Else
                                strSubDetalle = CStr(row("SUBDETALLE"))
                            End If

                            strDescripcion = strComentario & " " & strDetalle & " " & strSubDetalle
                            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Descripcion :" & strDescripcion)
                            'Valores
                            If Not IsDBNull(row("VALOR_NETO")) Then
                                If CStr(row("VALOR_NETO")).IndexOf("<TOTAL_FACTURADO>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOTAL_FACTURADO")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOTAL_FACTDO_BOL>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOTAL_FACTURADO_BOL")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOTAL_FACTDO_FACT>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOTAL_FACTURADO_FAC")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOTAL_EFECTIVO>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = dMtoEfectivo
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<DEVOL_EFECTIVO>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = (dMtoDevEfec * -1)
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<MTO_COMP_NC>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("MTO_COMP_NC")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_FIN_CUO>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_FIN_CUO")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_FIN_CUO1>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_FIN_CUO1")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_FIN_CUO6>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_FIN_CUO6")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_FIN_CUO12>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_FIN_CUO12")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_FIN_CUO18>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_FIN_CUO18")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_FIN_CUO24>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_FIN_CUO24")
                                    'INICIATIVA-529 INI
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOTAL_EFE_CUOINI>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOTAL_EFE_CUOINI")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOTAL_AMX_CUOINI>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOTAL_AMX_CUOINI")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOTAL_DIN_CUOINI>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOTAL_DIN_CUOINI")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOTAL_MCD_CUOINI>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOTAL_MCD_CUOINI")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOTAL_VIS_CUOINI>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOTAL_VIS_CUOINI")
                                    'INICIATIVA-529 FIN
                                    'INICIATIVA-565 INI
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOTAL_DSF_EFEC>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOTAL_DSF_EFEC")
                                    'INICIATIVA-565 FIN
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_VOU")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VTA_EFECTIVO>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOTAL_VTA_EFECTIVO")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_AMEX>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX") '"AMEX"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_NC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD") '"ZCAR"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_CHQ>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE") '"ZCHQ"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_CIBK>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB")  '"ZCIB"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_ELEC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON")  '"ZDEL"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_DINN>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS") '"ZDIN"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_MAES>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO") '"ZDMT"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_MAST>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD") '"ZMCD"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_RIP>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY") '"ZRIP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_CMR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA") '"ZSAG"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_VISA>") <> -1 Then
                                    Dim dValVisa As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1"), ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2") '"VISA", "ZVIS"
                                                dValVisa += CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisa.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_TPOS>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO") '"TDPP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_CUO1>") <> -1 Then
                                    Dim dValCuota1 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1") '"TF1C"
                                                dValCuota1 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota1
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_CUO2>") <> -1 Then
                                    Dim dValCuota2 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2") '"TF2C"
                                                dValCuota2 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota2
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_CUO3>") <> -1 Then
                                    Dim dValCuota3 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3") '"TF3C"
                                                dValCuota3 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota3
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_CUO4>") <> -1 Then
                                    Dim dValCuota4 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4") '"TF4C"
                                                dValCuota4 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota4
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_CUO5>") <> -1 Then
                                    Dim dValCuota5 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5") '"TF5C"
                                                dValCuota5 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota5
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_CUO6>") <> -1 Then
                                    Dim dValCuota6 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6") '"TF6C"
                                                dValCuota6 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota6
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_VISI>") <> -1 Then
                                    Dim dValVisaI As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET") '"VISI"
                                                dValVisaI = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisaI
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_CAR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA")  '"ZCRS"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_CURA>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO") '"ZCZO"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_VISW>") <> -1 Then
                                    Dim dVisaW As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISAWEB") '"VISW"
                                                dVisaW = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dVisaW
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_MAEW>") <> -1 Then
                                    Dim dMaestroW As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTROWEB") '"MAEW"
                                                dMaestroW = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dMaestroW
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_CUEC>") <> -1 Then
                                    Dim dEmpClaro As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSCLARO") '"CCLA"
                                                dEmpClaro = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dEmpClaro
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_CUEO>") <> -1 Then
                                    Dim dEmpOver As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSOVERALL") '"COVE"
                                                dEmpOver = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dEmpOver
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_ACEH>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME") '"ZACE"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(1).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VOU_OTR>") <> -1 Then
                                    Dim dContab As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(1).Rows
                                        If IsDBNull(fila("ADD")) Then
                                            If Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EFECTIVO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAWEB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTROWEB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSCLARO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSOVERALL")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME")) Then
                                                dContab += CDbl(fila("MONTO"))
                                            End If
                                        End If
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dContab.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<ING_TOT_CHQ>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("ING_TOT_CHQ")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<P_CAJA_BUZON_CHEQUE>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = (Math.Round(CDec(txtCajaCheque.Text), 2) * -1)
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<P_MTO_SOBRANTE>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = mtoSobrante
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<P_MTO_FALTANTE>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = mtoFaltante
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<P_CAJA_BUZON>") <> -1 Then
                                    If tipoCuadre.Equals("T") Then
                                        arrayValoresCuadre(iArrayLenght) = (Math.Round(CDec(txtCaja.Text), 2) * -1)
                                    Else
                                        arrayValoresCuadre(iArrayLenght) = ((CDbl(txtCaja.Text) + Math.Round(dMtoCajaBuzonAnteriorPendiente, 2)) * -1)
                                    End If
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<P_REMESA>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = Math.Round(dMtoRemesa, 2)
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<REMESA_HOY>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = Math.Round(dMtoRemesaAyer, 2)
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<REMESA_AYER>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = Math.Round(dMtoRemesaHoy, 2)
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_FACTURADO_TRF_GRAT>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_FACTURADO_TRF_GRAT")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_FACTURADO_VTA_CRED>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = 0 'drDatosCalculados("TOT_FACTURADO_VTA_CRED")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_FACTURADO_NC>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_FACTURADO_NC")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_RECAUD_MOV")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_EFE>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_EFECTIVO") '"ZEFE"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_AMEX>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX") '"AMEX"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_NC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD") '"ZCAR"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_CHQ>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE") '"ZCHQ"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_CIBK>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB") '"ZCIB"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_ELEC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON") '"ZDEL"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_DINN>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS") '"ZDIN"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_MAES>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO") '"ZDMT"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_MAST>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD") '"ZMCD"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_RIP>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY") '"ZRIP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_CMR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA") '"ZSAG"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_VISA>") <> -1 Then
                                    Dim dValVisa As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1"), ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2") ' "VISA", "ZVIS"
                                                dValVisa += CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisa.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_TPOS>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO") '"TDPP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_CUO1>") <> -1 Then
                                    Dim dValCuota1 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1") '"TF1C"
                                                dValCuota1 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota1
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_CUO2>") <> -1 Then
                                    Dim dValCuota2 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2") '"TF2C"
                                                dValCuota2 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota2
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_CUO3>") <> -1 Then
                                    Dim dValCuota3 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3") '"TF3C"
                                                dValCuota3 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota3
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_CUO4>") <> -1 Then
                                    Dim dValCuota4 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4") '"TF4C"
                                                dValCuota4 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota4
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_CUO5>") <> -1 Then
                                    Dim dValCuota5 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5") '"TF5C"
                                                dValCuota5 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota5
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_CUO6>") <> -1 Then
                                    Dim dValCuota6 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6") '"TF6C"
                                                dValCuota6 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota6
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_VISI>") <> -1 Then
                                    Dim dValVisaI As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET") '"VISI"
                                                dValVisaI = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisaI
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_CAR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA") '"ZCRS"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_CURA>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO") '"ZCZO"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_MOV_ACEH>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME") '"ZACE"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(2).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_MOV_OTR>") <> -1 Then
                                    Dim dContab As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(2).Rows
                                        If IsDBNull(fila("ADD")) Then
                                            If Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EFECTIVO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAWEB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTROWEB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSCLARO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSOVERALL")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME")) Then
                                                dContab += CDbl(fila("MONTO"))
                                            End If
                                        End If
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dContab.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_RECAUD_DTH")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_EFE>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_EFECTIVO") ' "ZEFE"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_AMEX>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX") '"AMEX"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_NC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD") '"ZCAR"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_CHQ>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE") '"ZCHQ"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_CIBK>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB") '"ZCIB"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_ELEC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON") '"ZDEL"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_DINN>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS") '"ZDIN"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_MAES>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO") '"ZDMT"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_MAST>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD") '"ZMCD"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_RIP>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY") '"ZRIP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_CMR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA") '"ZSAG"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_VISA>") <> -1 Then
                                    Dim dValVisa As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1"), ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2") '"VISA", "ZVIS"
                                                dValVisa += CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisa.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_TPOS>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO") '"TDPP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_CUO1>") <> -1 Then
                                    Dim dValCuota1 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1") '"TF1C"
                                                dValCuota1 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota1
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_CUO2>") <> -1 Then
                                    Dim dValCuota2 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2") '"TF2C"
                                                dValCuota2 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota2
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_CUO3>") <> -1 Then
                                    Dim dValCuota3 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3") '"TF3C"
                                                dValCuota3 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota3
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_CUO4>") <> -1 Then
                                    Dim dValCuota4 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4") '"TF4C"
                                                dValCuota4 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota4
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_CUO5>") <> -1 Then
                                    Dim dValCuota5 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5") '"TF5C"
                                                dValCuota5 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota5
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_CUO6>") <> -1 Then
                                    Dim dValCuota6 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6") '"TF6C"
                                                dValCuota6 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota6
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_VISI>") <> -1 Then
                                    Dim dValVisaI As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET") '"VISI"
                                                dValVisaI = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisaI
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_CAR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA") '"ZCRS"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_CURA>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO") '"ZCZO"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DTH_ACEH>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME") '"ZACE"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(3).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_DTH_OTR>") <> -1 Then
                                    Dim dContab As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(3).Rows
                                        If IsDBNull(fila("add")) Then
                                            If Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EFECTIVO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAWEB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTROWEB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSCLARO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSOVERALL")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME")) Then
                                                dContab += CDbl(fila("MONTO"))
                                            End If
                                        End If
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dContab.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_RECAUD_FIJ")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_EFE>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_EFECTIVO") '"ZEFE"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_AMEX>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX") '"AMEX"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_NC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD") '"ZCAR"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_CHQ>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE") '"ZCHQ"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_CIBK>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB") '"ZCIB"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_ELEC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON") '"ZDEL"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_DINN>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS") '"ZDIN"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_MAES>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO") '"ZDMT"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_MAST>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD") '"ZMCD"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_RIP>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY") '"ZRIP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_CMR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA") '"ZSAG"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_VISA>") <> -1 Then
                                    Dim dValVisa As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1"), ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2") '"VISA", "ZVIS"
                                                dValVisa += CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisa.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_TPOS>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO") '"TDPP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_CUO1>") <> -1 Then
                                    Dim dValCuota1 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1") '"TF1C"
                                                dValCuota1 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota1
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_CUO2>") <> -1 Then
                                    Dim dValCuota2 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2") '"TF2C"
                                                dValCuota2 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota2
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_CUO3>") <> -1 Then
                                    Dim dValCuota3 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3") '"TF3C"
                                                dValCuota3 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota3
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_CUO4>") <> -1 Then
                                    Dim dValCuota4 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4") '"TF4C"
                                                dValCuota4 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota4
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_CUO5>") <> -1 Then
                                    Dim dValCuota5 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5") '"TF5C"
                                                dValCuota5 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota5
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_CUO6>") <> -1 Then
                                    Dim dValCuota6 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6") '"TF6C"
                                                dValCuota6 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota6
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_VISI>") <> -1 Then
                                    Dim dValVisaI As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET") '"VINT"
                                                dValVisaI = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisaI
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_CAR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA") '"ZCRS"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_CURA>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO") '"ZCZO"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_FIJ_ACEH>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME") '"ZACE"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(4).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_FIJ_OTR>") <> -1 Then
                                    Dim dContab As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(4).Rows
                                        If IsDBNull(fila("ADD")) Then
                                            If Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EFECTIVO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAWEB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTROWEB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSCLARO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSOVERALL")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME")) Then
                                                dContab += CDbl(fila("MONTO"))
                                            End If
                                        End If
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dContab.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_RECAUD_PAG")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_EFE>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_EFECTIVO") '"ZEFE"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_AMEX>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX") '"AMEX"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_NC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD") '"ZCAR"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_CHQ>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE") '"ZCHQ"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_CIBK>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB") '"ZCIB"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_ELEC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON") '"ZDEL"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_DINN>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS") '"ZDIN"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_MAES>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO") '"ZDMT"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_MAST>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD") '"ZMCD"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_RIP>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY") '"ZRIP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_CMR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA") '"ZSAG"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_VISA>") <> -1 Then
                                    Dim dValVisa As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1"), ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2")  '"VISA", "ZVIS"
                                                dValVisa += CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisa.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_TPOS>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO") '"TDPP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_CUO1>") <> -1 Then
                                    Dim dValCuota1 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1") '"TF1C"
                                                dValCuota1 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota1
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_CUO2>") <> -1 Then
                                    Dim dValCuota2 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2") '"TF2C"
                                                dValCuota2 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota2
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_CUO3>") <> -1 Then
                                    Dim dValCuota3 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3") '"TF3C"
                                                dValCuota3 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota3
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_CUO4>") <> -1 Then
                                    Dim dValCuota4 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4") '"TF4C"
                                                dValCuota4 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota4
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_CUO5>") <> -1 Then
                                    Dim dValCuota5 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5") '"TF5C"
                                                dValCuota5 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota5
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_CUO6>") <> -1 Then
                                    Dim dValCuota6 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6") '"TF6C"
                                                dValCuota6 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota6
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_VISI>") <> -1 Then
                                    Dim dValVisaI As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET") '"VINT"
                                                dValVisaI = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisaI
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_CAR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA") '"ZCRS"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_CURA>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO") '"ZCZO"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_PAG_ACEH>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME") '"ZACE"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(5).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_PAG_OTR>") <> -1 Then
                                    Dim dContab As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(5).Rows
                                        If IsDBNull(fila("ADD")) Then
                                            If Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EFECTIVO")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAWEB")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTROWEB")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSCLARO")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSOVERALL")) And _
                                             Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME")) Then
                                                dContab += CDbl(fila("MONTO"))
                                            End If
                                        End If
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dContab.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_RECAUD_DAC")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_EFE>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_EFECTIVO") '"ZEFE"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_AMEX>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX") '"AMEX"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_NC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD") '"ZCAR"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_CHQ>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE") '"ZCHQ"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_CIBK>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB") '"ZCIB"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_ELEC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON") '"ZDEL"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_DINN>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS") '"ZDIN"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_MAES>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO") '"ZDMT"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_MAST>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD") '"ZMCD"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_RIP>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY") '"ZRIP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_CMR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA") '"ZSAG"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_VISA>") <> -1 Then
                                    Dim dValVisa As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1"), ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2") '"VISA", "ZVIS"
                                                dValVisa += CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisa.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_TPOS>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO") '"TDPP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_CUO1>") <> -1 Then
                                    Dim dValCuota1 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1") '"TF1C"
                                                dValCuota1 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota1
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_CUO2>") <> -1 Then
                                    Dim dValCuota2 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2") '"TF2C"
                                                dValCuota2 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota2
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_CUO3>") <> -1 Then
                                    Dim dValCuota3 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3") '"TF3C"
                                                dValCuota3 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota3
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_CUO4>") <> -1 Then
                                    Dim dValCuota4 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4") '"TF4C"
                                                dValCuota4 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota4
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_CUO5>") <> -1 Then
                                    Dim dValCuota5 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5") '"TF5C"
                                                dValCuota5 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota5
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_CUO6>") <> -1 Then
                                    Dim dValCuota6 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6") '"TF6C"
                                                dValCuota6 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota6
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_VISI>") <> -1 Then
                                    Dim dValVisaI As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET") '"VINT"
                                                dValVisaI = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisaI
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_CAR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA") '"ZCRS"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_CURA>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO") '"ZCZO"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DAC_ACEH>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME") '"ZACE"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(6).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_DAC_OTR>") <> -1 Then
                                    Dim dContab As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(6).Rows
                                        If IsDBNull(fila("ADD")) Then
                                            If Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EFECTIVO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAWEB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTROWEB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSCLARO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSOVERALL")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME")) Then
                                                dContab += CDbl(fila("MONTO"))
                                            End If
                                        End If
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dContab.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_RECAUD_DRA")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_EFE>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_EFECTIVO") '"ZEFE"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_AMEX>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX") '"AMEX"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_NC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD") '"ZCAR"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_CHQ>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE") '"ZCHQ"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_CIBK>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB") '"ZCIB"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_ELEC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON") '"ZDEL"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_DINN>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS") '"ZDIN"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_MAES>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO") '"ZDMT"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_MAST>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD") '"ZMCD"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_RIP>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY") '"ZRIP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_CMR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA") '"ZSAG"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_VISA>") <> -1 Then
                                    Dim dValVisa As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1"), ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2") '"VISA", "ZVIS"
                                                dValVisa += CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisa.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_TPOS>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO") '"TDPP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_CUO1>") <> -1 Then
                                    Dim dValCuota1 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1") '"TF1C"
                                                dValCuota1 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota1
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_CUO2>") <> -1 Then
                                    Dim dValCuota2 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2") '"TF2C"
                                                dValCuota2 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota2
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_CUO3>") <> -1 Then
                                    Dim dValCuota3 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3") '"TF3C"
                                                dValCuota3 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota3
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_CUO4>") <> -1 Then
                                    Dim dValCuota4 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4") '"TF4C"
                                                dValCuota4 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota4
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_CUO5>") <> -1 Then
                                    Dim dValCuota5 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5") '"TF5C"
                                                dValCuota5 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota5
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_CUO6>") <> -1 Then
                                    Dim dValCuota6 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6") '"TF6C"
                                                dValCuota6 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota6
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_VISI>") <> -1 Then
                                    Dim dValVisaI As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET") '"VINT"
                                                dValVisaI = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisaI
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_CAR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA") '"ZCRS"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_CURA>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO") '"ZCZO"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_RECAUD_DRA_ACEH>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME") '"ZACE"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(7).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_DRA_OTR>") <> -1 Then
                                    Dim dContab As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(7).Rows
                                        If IsDBNull(fila("ADD")) Then
                                            If Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EFECTIVO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAWEB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTROWEB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSCLARO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSOVERALL")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME")) Then
                                                dContab += CDbl(fila("MONTO"))
                                            End If
                                        End If
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dContab.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VNT_VISMC>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_VNT_VISMC")
                                    'PROY BUYBACK INI'
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VNT_CUPON_VALE>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_VNT_CUPON_VALE")
                                    'PROY BUYBACK FIN'
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VNT_VIS>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_VNT_VIS")
                                    'PROY-27440'INICIO

                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<DEV_VNT_VIS>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_VNT_VIS_DEV")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<FAL_VNT_VIS>") <> -1 Then
                                    If ((drDatosCalculados("TOT_VNT_VIS") + drDatosCalculados("TOT_VNT_VIS_DEV")) - drDatosCalculados("TOT_VNT_VIS_POS") > 0) Then
                                        arrayValoresCuadre(iArrayLenght) = ((drDatosCalculados("TOT_VNT_VIS") + drDatosCalculados("TOT_VNT_VIS_DEV")) - drDatosCalculados("TOT_VNT_VIS_POS"))
                                    Else
                                        arrayValoresCuadre(iArrayLenght) = 0
                                    End If
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<SOB_VNT_VIS>") <> -1 Then
                                    If (drDatosCalculados("TOT_VNT_VIS_POS") - (drDatosCalculados("TOT_VNT_VIS") + drDatosCalculados("TOT_VNT_VIS_DEV")) > 0) Then
                                        arrayValoresCuadre(iArrayLenght) = -1 * (drDatosCalculados("TOT_VNT_VIS_POS") - (drDatosCalculados("TOT_VNT_VIS") + drDatosCalculados("TOT_VNT_VIS_DEV")))
                                    Else
                                        arrayValoresCuadre(iArrayLenght) = 0
                                    End If
                                    'PROY-27440'FIN
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VNT_MC>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_VNT_MC")
                                    'PROY-27440'INICIO
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<DEV_VNT_MC>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_VNT_MC_DEV")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<FAL_VNT_MC>") <> -1 Then
                                    If ((drDatosCalculados("TOT_VNT_MC") + drDatosCalculados("TOT_VNT_MC_DEV")) - drDatosCalculados("TOT_VNT_MC_POS") > 0) Then
                                        arrayValoresCuadre(iArrayLenght) = ((drDatosCalculados("TOT_VNT_MC") + drDatosCalculados("TOT_VNT_MC_DEV")) - drDatosCalculados("TOT_VNT_MC_POS"))
                                    Else
                                        arrayValoresCuadre(iArrayLenght) = 0
                                    End If
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<SOB_VNT_MC>") <> -1 Then
                                    If (drDatosCalculados("TOT_VNT_MC_POS") - (drDatosCalculados("TOT_VNT_MC") + drDatosCalculados("TOT_VNT_MC_DEV")) > 0) Then
                                        arrayValoresCuadre(iArrayLenght) = -1 * (drDatosCalculados("TOT_VNT_MC_POS") - (drDatosCalculados("TOT_VNT_MC") + +drDatosCalculados("TOT_VNT_MC_DEV")))
                                    Else
                                        arrayValoresCuadre(iArrayLenght) = 0
                                    End If
                                    'PROY-27440'FIN
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VNT_AMX>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_VNT_AMX")
                                    'PROY-27440'INICIO
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<DEV_VNT_AMX>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_VNT_AMX_DEV")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<FAL_VNT_AMX>") <> -1 Then
                                    If ((drDatosCalculados("TOT_VNT_AMX") + drDatosCalculados("TOT_VNT_AMX_DEV")) - drDatosCalculados("TOT_VNT_AMX_POS") > 0) Then
                                        arrayValoresCuadre(iArrayLenght) = ((drDatosCalculados("TOT_VNT_AMX") + drDatosCalculados("TOT_VNT_AMX_DEV")) - drDatosCalculados("TOT_VNT_AMX_POS"))
                                    Else
                                        arrayValoresCuadre(iArrayLenght) = 0
                                    End If
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<SOB_VNT_AMX>") <> -1 Then
                                    If (drDatosCalculados("TOT_VNT_AMX_POS") - (drDatosCalculados("TOT_VNT_AMX") + drDatosCalculados("TOT_VNT_AMX_DEV")) > 0) Then
                                        arrayValoresCuadre(iArrayLenght) = -1 * (drDatosCalculados("TOT_VNT_AMX_POS") - (drDatosCalculados("TOT_VNT_AMX") + drDatosCalculados("TOT_VNT_AMX_DEV")))
                                    Else
                                        arrayValoresCuadre(iArrayLenght) = 0
                                    End If
                                    'PROY-27440'FIN
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_VNT_DIN>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_VNT_DIN")
                                    'PROY-27440'INICIO
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<DEV_VNT_DIN>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_VNT_DIN_DEV")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<FAL_VNT_DIN>") <> -1 Then
                                    If ((drDatosCalculados("TOT_VNT_DIN") + drDatosCalculados("TOT_VNT_DIN_DEV")) - drDatosCalculados("TOT_VNT_DIN_POS") > 0) Then
                                        arrayValoresCuadre(iArrayLenght) = (drDatosCalculados("TOT_VNT_DIN") + drDatosCalculados("TOT_VNT_DIN_DEV")) - (drDatosCalculados("TOT_VNT_DIN_POS"))
                                    Else
                                        arrayValoresCuadre(iArrayLenght) = 0
                                    End If
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<SOB_VNT_DIN>") <> -1 Then
                                    If (drDatosCalculados("TOT_VNT_DIN_POS") - (drDatosCalculados("TOT_VNT_DIN") + drDatosCalculados("TOT_VNT_DIN_DEV")) > 0) Then
                                        arrayValoresCuadre(iArrayLenght) = -1 * (drDatosCalculados("TOT_VNT_DIN_POS") - (drDatosCalculados("TOT_VNT_DIN") + drDatosCalculados("TOT_VNT_DIN_DEV")))
                                    Else
                                        arrayValoresCuadre(iArrayLenght) = 0
                                    End If
                                    'PROY-27440'FIN
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_EFE>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_GAR_EFE")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_DEV>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = (CDec(drDatosCalculados("TOT_GAR_DEV")) * -1)
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_GAR_VOU")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_AMEX>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX") '"AMEX"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_NC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD") '"ZCAR"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_CHQ>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE") '"ZCHQ"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_CIBK>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB") '"ZCIB"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_ELEC>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON") '"ZDEL"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_DINN>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS") '"ZDIN"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_MAES>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO") '"ZDMT"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_MAST>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD") '"ZMCD"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_RIP>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY") '"ZRIP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_CMR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA") '"ZSAG"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_VISA>") <> -1 Then
                                    Dim dValVisa As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1"), ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2") ' "VISA", "ZVIS"
                                                dValVisa += CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisa.ToString()
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_TPOS>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO") '"TDPP"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_CUO1>") <> -1 Then
                                    Dim dValCuota1 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1") '"TF1C"
                                                dValCuota1 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota1
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_CUO2>") <> -1 Then
                                    Dim dValCuota2 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2") '"TF2C"
                                                dValCuota2 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota2
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_CUO3>") <> -1 Then
                                    Dim dValCuota3 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3") '"TF3C"
                                                dValCuota3 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota3
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_CUO4>") <> -1 Then
                                    Dim dValCuota4 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4") '"TF4C"
                                                dValCuota4 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota4
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_CUO5>") <> -1 Then
                                    Dim dValCuota5 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5") '"TF5C"
                                                dValCuota5 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota5
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_CUO6>") <> -1 Then
                                    Dim dValCuota6 As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6") '"TF6C"
                                                dValCuota6 = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValCuota6
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_VISI>") <> -1 Then
                                    Dim dValVisaI As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET") '"VISI"
                                                dValVisaI = CDbl(fila("MONTO"))
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dValVisaI
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_CAR>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA") '"ZCRS"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_CURA>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO") '"ZCZO"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_ACEH>") <> -1 Then
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        Select Case fila("MEDIO_PAGO")
                                            Case ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME") '"ZACE"
                                                arrayValoresCuadre(iArrayLenght) = fila("MONTO")
                                                fila("ADD") = "X"
                                                dsDatosCalculados.Tables(8).AcceptChanges()
                                                Exit For
                                        End Select
                                    Next
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_OTR>") <> -1 Then
                                    Dim dContab As Double = 0
                                    For Each fila As DataRow In dsDatosCalculados.Tables(8).Rows
                                        If IsDBNull(fila("ADD")) Then
                                            If Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EFECTIVO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_AMEX")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_NETCARD")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CHEQUE")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ZCIB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ELECTRON")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_DINNERS")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTRO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MASTERCARD")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_RIPLEY")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_SAGA")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA1")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISA2")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_POSTPAGO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA1")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA2")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA3")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA4")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA5")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_FINANCUOTA6")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAINTERNET")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CARSA")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_CURACAO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_VISAWEB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_MAESTROWEB")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSCLARO")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_EMPLEADOSOVERALL")) And _
                                            Not Trim(fila("MEDIO_PAGO")).Equals(ConfigurationSettings.AppSettings("constCuadreViaPago_ACEHOME")) Then
                                                dContab += CDbl(fila("MONTO"))
                                            End If
                                        End If
                                    Next
                                    arrayValoresCuadre(iArrayLenght) = dContab.ToString()
                                    'JRM

                                    'INI DELIVERY - MEJORAS
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOTAL_FACTURADO_APK>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOTAL_FACTURADO_APK")
                                ElseIf CStr(row("VALOR_NETO")).IndexOf("<TOT_GAR_VOU_APK>") <> -1 Then
                                    arrayValoresCuadre(iArrayLenght) = drDatosCalculados("TOT_GAR_VOU_APK")
                                    'FIN DELIVERY - MEJORAS 

                                Else
                                    arrayValoresCuadre(iArrayLenght) = 0
                                End If
                            Else
                                arrayValoresCuadre(iArrayLenght) = String.Empty
                            End If

                            If Not IsDBNull(row("VISIBLE")) Then
                                If CStr(row("VISIBLE")).Equals("X") Then
                                    arrayConceptosCuadre(iArrayLenght) = strDescripcion.Trim()
                                End If
                            Else
                                If Not arrayValoresCuadre(iArrayLenght).Equals(String.Empty) Then
                                    If CDbl(arrayValoresCuadre(iArrayLenght)) = 0 Then
                                        arrayConceptosCuadre(iArrayLenght) = String.Empty
                                    Else
                                        arrayConceptosCuadre(iArrayLenght) = strDescripcion.Trim()
                                    End If
                                Else
                                    arrayConceptosCuadre(iArrayLenght) = String.Empty
                                End If
                            End If

                            iArrayLenght += 1
                            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Item:" & iArrayLenght.ToString())
                        Next

                        bDatosCompletos = True
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Fin: Generar estructura de cuadre caja Individual")
                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "setCuadreMontos:: Error al procesar cuadre individual - " & " Mensaje error: " & ex.Message)
                    End Try
                End If
            End If

            rMtoSobrante = mtoSobrante
            rMtoFaltante = mtoFaltante

            ''Registro del Cuadre en TI_MONTOS_CUAD
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Inicio: Registrar montos cuadre")
            If bDatosCompletos Then
                Dim idx As Integer = 1
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Oficina Venta: " & strCodOfi)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Fecha: " & CDate(txtFecha.Text).ToString("yyyyMMdd"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Cajero: " & strCodCaje)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP arrayConceptosCuadre: " & arrayConceptosCuadre.Length.ToString())

                For i As Int32 = 0 To arrayConceptosCuadre.Length - 1
                    If Not arrayConceptosCuadre(i) Is Nothing Then
                        'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP arrayConceptosCuadre " & i.ToString() & " : " & arrayConceptosCuadre(i).ToString())
                        If arrayConceptosCuadre(i).Length > 0 Then
                            Dim strContador As String = idx.ToString().PadLeft(4, "0")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP CONT : " & strContador & " Concepto: " & arrayConceptosCuadre(i) & " Valor : " & arrayValoresCuadre(i))

                            strMensajeErr = objclsOffline.SetMontosCuadreCajero(strCodOfi, CDate(txtFecha.Text).ToString("yyyyMMdd"), _
                                    strContador, strCodCaje, arrayConceptosCuadre(i), arrayValoresCuadre(i))
                            If strMensajeErr.Length > 0 Then
                                setCuadreMontos = strMensajeErr
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - MSJERR: " & strMensajeErr)
                                bProcesoExito = False
                                Exit For
                            Else
                                bProcesoExito = True
                                idx += 1
                            End If
                        End If
                    End If
                Next

                If bProcesoExito Then
                    If Not strMensajeRemesa.Equals(String.Empty) Then
                        setCuadreMontos = strMensajeRemesa
                    Else
                        setCuadreMontos = String.Empty
                    End If
                End If
            Else
                setCuadreMontos = "Cuadre no procesado. No se han registrado ventas y recaudaciones."
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Mensaje: " & setCuadreMontos)
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Fin: Registrar montos cuadre.")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos:: ERROR AL REGISTRAR MONTO DE CUADRE " & " Mensaje error: " & ex.Message)
            setCuadreMontos = ex.Message
        Finally
            objclsOffline = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos :: FIN DE MONTOS CUADRE DE CAJA")
        End Try
    End Function

    Private Function fIngresarPagosSicar(ByRef mensajes() As String) As Boolean
        Dim vExito As Boolean = False
        Dim mensajeRecaudaciones As String = String.Empty
        Dim strOficinaVenta As String = Session("ALMACEN")
        Dim strCajero As String = String.Empty
        Dim strIdentifyLog As String = Session("ALMACEN") & "|" & strUsuario
        Dim bEjecucionRecaudaciones As Boolean
        Dim dsRecaudaciones As DataSet
        Dim bProcesaDeudasFijaMovil, bProcesaDac, bProcesaDra As Boolean
        objclsOffline = New COM_SIC_OffLine.clsOffline

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio : fIngresarPagosSicar")

            If UCase(Request.Item("tipocuadre")) = "I" Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio GetRecaudacionesCuadreInd")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Tipo Cuadre : I")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Oficna venta : " & strOficinaVenta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha : " & txtFecha.Text)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Cajero : " & strUsuario)

                dsRecaudaciones = objclsOffline.GetRecaudacionesCuadreInd(strOficinaVenta, txtFecha.Text, strUsuario)
                strCajero = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT Resultado : OK")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin GetRecaudacionesCuadreInd")
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio GetRecaudacionesCuadreGen")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Tipo Cuadre : G")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Oficna venta : " & strOficinaVenta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha : " & txtFecha.Text)

                dsRecaudaciones = objclsOffline.GetRecaudacionesCuadreGen(strOficinaVenta, txtFecha.Text)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT Resultado : OK")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin GetRecaudacionesCuadreGen")
            End If

            Dim deudaRelation As DataRelation = New DataRelation("deudaRelation", dsRecaudaciones.Tables(0).Columns("ID_T_TRS_REG_DEUDA"), dsRecaudaciones.Tables(1).Columns("ID_T_TRS_REG_DEUDA"))
            Dim dacRelation As DataRelation = New DataRelation("dacRelation", dsRecaudaciones.Tables(2).Columns("NROAT"), dsRecaudaciones.Tables(3).Columns("NROAT"))
            Dim draRelation As DataRelation = New DataRelation("draRelation", dsRecaudaciones.Tables(4).Columns("NROAT"), dsRecaudaciones.Tables(5).Columns("NROAT"))

            Dim iIDVentasFact As Int32 = 0
            Dim strEstadoDoc As String = PAGADO
            dsRecaudaciones.Relations.AddRange(New DataRelation() {deudaRelation, dacRelation, draRelation})

            If Not dsRecaudaciones Is Nothing Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INI: Pago de recaudaciones Movil, fija y paginas.")
                For Each rowDeuda As DataRow In dsRecaudaciones.Tables(0).Rows
                    Dim dsResultado As New DataSet
                    Dim strDescDocumento As String = "PAGOS POR RECAUDACION"
                    Dim strVendedor As String = Convert.ToString(rowDeuda("COD_CAJERO")).Trim().PadLeft(10, CChar("0"))
                    Dim strMoneda As String = String.Empty
                    Dim totalPago As String = rowDeuda("IMPORTE_PAGO")
                    Dim strFecha As String = CDate(txtFecha.Text).ToString("yyyyMMdd")
                    Dim strNroTransaccion As String = String.Empty
                    Dim strNombreDeudor As String = rowDeuda("NOM_DEUDOR")
                    Dim strEstado As String = rowDeuda("ESTADO_TRANSAC")
                    Dim strClaseFactura As String
                    Dim dblCero As Double = 0

                    If rowDeuda("MON_PAGO") = "604" Then
                        strMoneda = "PEN"
                    End If

                    If strEstado.Equals("1") Then
                        strClaseFactura = ConfigurationSettings.AppSettings("constClaseDocumentoMovil")
                    ElseIf strEstado.Equals("3") Then
                        strClaseFactura = ConfigurationSettings.AppSettings("constClaseDocumentoFijo")
                    ElseIf strEstado.Equals("4") Then
                        strClaseFactura = ConfigurationSettings.AppSettings("constClaseDocumentoPagina")
                    End If

                    If Not IsDBNull(rowDeuda("ID_T_TRS_REG_DEUDA")) Then
                        strNroTransaccion = "R" & CStr(rowDeuda("ID_T_TRS_REG_DEUDA"))
                    End If

                    Dim tramaPagos As String = String.Empty

                    For Each rowChild As DataRow In rowDeuda.GetChildRows("deudaRelation")
                        Dim strChildNroTransaccion As String = String.Empty
                        If Not IsDBNull(rowChild("ID_T_TRS_REG_DEUDA")) Then
                            strChildNroTransaccion = "R" & CStr(rowChild("ID_T_TRS_REG_DEUDA"))
                        End If

                        tramaPagos &= strFecha & ";" & strChildNroTransaccion & ";" & strNombreDeudor & ";" & _
                                        strClaseFactura & ";" & CStr(rowChild("VIA_PAGO")) & ";" & _
                                        CStr(rowChild("IMPORTE_PAGADO")) & "|"
                    Next

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INI: Registro en VentasFactCuadre")
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Oficina venta : " & strOficinaVenta)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Fecha : " & strFecha)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Cajero : " & strCajero)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Descripcion Documento : " & strDescDocumento)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Nro Transaccion : " & strNroTransaccion)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Deudor : " & strNombreDeudor)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Vendedor : " & String.Empty)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Moneda : " & strMoneda)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Clase Factura : " & strClaseFactura)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Nro Couta: " & dblCero.ToString())
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Total Pago : " & totalPago.ToString())
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP saldo : " & totalPago.ToString())
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Referencia : " & String.Empty)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Estado : " & strEstadoDoc)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Nodo : " & strNodo)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP trama Pagos : " & tramaPagos)

                    dsResultado = objclsOffline.SetVentasFactCuadre(strOficinaVenta, strFecha, strCajero, strDescDocumento, _
                                        strNroTransaccion, strNombreDeudor, String.Empty, strMoneda, strClaseFactura, _
                                        dblCero, totalPago, totalPago, String.Empty, strEstadoDoc, strNodo, tramaPagos, iIDVentasFact)

                    If Not dsResultado Is Nothing Then
                        If dsResultado.Tables(0).Rows(0).Item("LOG") <> String.Empty Then
                            Dim strMsgErr As String = dsResultado.Tables(0).Rows(0).Item("LOG")
                            mensajeRecaudaciones = strMsgErr
                            mensajes(0) = mensajeRecaudaciones
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR en la transaccion nro: " & iIDVentasFact)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MENSAJE: " & mensajeRecaudaciones)
                            vExito = False
                            bProcesaDeudasFijaMovil = False
                            Exit Function
                        Else
                            bProcesaDeudasFijaMovil = True
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "OUT Pago procesado nro: " & iIDVentasFact)
                        End If
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin: Registro en VentasFactCuadre ")
                Next

                If dsRecaudaciones.Tables(0).Rows.Count = 0 Then
                    bProcesaDeudasFijaMovil = True
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MENSAJE: Pago de recaudaciones Movil y fija. No se encontró datos a procesar.")
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN: Pago de recaudaciones Movil, fija y paginas.")

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INI: Pago de recaudaciones DAC")
                For Each rowDac As DataRow In dsRecaudaciones.Tables(2).Rows
                    Dim dsResultado As New DataSet
                    Dim strDescDocumento As String = "PAGOS POR RECAUDACION"
                    Dim strVendedor As String = Convert.ToString(rowDac("COD_CAJERO")).Trim().PadLeft(10, CChar("0"))
                    Dim strMoneda As String = "PEN"
                    Dim totalPago As String = rowDac("MONTO")
                    Dim strFecha As String = CDate(txtFecha.Text).ToString("yyyyMMdd")
                    Dim strNroTransaccion As String = String.Empty
                    Dim strNombreDeudor As String = rowDac("CLIENTE")
                    Dim strClaseFactura As String = ConfigurationSettings.AppSettings("constClaseDocumentoDAC")
                    Dim dblCero As Double = 0
                    Dim tramaPagos As String = String.Empty

                    If Not IsDBNull(rowDac("NROAT")) Then
                        strNroTransaccion = rowDac("NROAT")
                    End If

                    For Each rowChild As DataRow In rowDac.GetChildRows("dacRelation")
                        Dim strChildNroTransaccion As String = String.Empty
                        If Not IsDBNull(rowChild("NROAT")) Then
                            strChildNroTransaccion = rowChild("NROAT")
                        End If
                        tramaPagos &= strFecha & ";" & strChildNroTransaccion & ";" & strNombreDeudor & ";" & strClaseFactura & ";" & rowChild("VIA_PAGO") & ";" & rowChild("MONTO") & "|"
                    Next

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INI: Registro en VentasFactCuadre")
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Oficina venta : " & strOficinaVenta)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Fecha : " & strFecha)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Cajero : " & strCajero)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Descripcion Documento : " & strDescDocumento)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Nro Transaccion : " & strNroTransaccion)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Deudor : " & strNombreDeudor)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Vendedor : " & String.Empty)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Moneda : " & strMoneda)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Clase Factura : " & strClaseFactura)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Nro Couta: " & dblCero.ToString())
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Total Pago : " & totalPago.ToString())
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP saldo : " & totalPago.ToString())
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Referencia : " & String.Empty)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Estado : " & strEstadoDoc)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Nodo : " & strNodo)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP trama Pagos : " & tramaPagos)

                    dsResultado = objclsOffline.SetVentasFactCuadre(strOficinaVenta, strFecha, strCajero, strDescDocumento, _
                                        strNroTransaccion, strNombreDeudor, String.Empty, strMoneda, strClaseFactura, _
                                        dblCero, totalPago, totalPago, String.Empty, strEstadoDoc, strNodo, tramaPagos, iIDVentasFact)

                    If Not dsResultado Is Nothing Then
                        If dsResultado.Tables(0).Rows(0).Item("LOG") <> String.Empty Then
                            Dim strMsgErr As String = dsResultado.Tables(0).Rows(0).Item("LOG")
                            mensajeRecaudaciones = strMsgErr
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR en la transaccion nro: " & iIDVentasFact)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MENSAJE: " & mensajeRecaudaciones)
                            vExito = False
                            bProcesaDac = False
                            Exit For
                        Else
                            bProcesaDac = True
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "OUT Pago procesado nro: " & iIDVentasFact)
                        End If
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin: Registro en VentasFactCuadre ")
                Next

                If dsRecaudaciones.Tables(2).Rows.Count = 0 Then
                    bProcesaDac = True
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MENSAJE : Pago de recaudaciones DAC. No se encontró datos a procesar.")
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin: Pago de recaudaciones DAC")

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INI: Pago de recaudaciones DRA")
                For Each rowDra As DataRow In dsRecaudaciones.Tables(4).Rows
                    Dim dsResultado As New DataSet
                    Dim strDescDocumento As String = "PAGOS POR RECAUDACION DRA"
                    Dim strVendedor As String = Convert.ToString(rowDra("COD_CAJERO")).Trim().PadLeft(10, CChar("0"))
                    Dim strMoneda As String = "PEN"
                    Dim totalPago As String = rowDra("MONTO")
                    Dim strFecha As String = CDate(txtFecha.Text).ToString("yyyyMMdd")
                    Dim strNroTransaccion As String = String.Empty
                    Dim strNombreDeudor As String = rowDra("CLIENTE")
                    Dim strClaseFactura As String = ConfigurationSettings.AppSettings("constClaseDocumentoDRA")
                    Dim dblCero As Double = 0
                    Dim tramaPagos As String = String.Empty

                    If Not IsDBNull(rowDra("NROAT")) Then
                        strNroTransaccion = rowDra("NROAT")
                    End If

                    For Each rowChild As DataRow In rowDra.GetChildRows("draRelation")
                        Dim strChildNroTransaccion As String = String.Empty
                        If Not IsDBNull(rowChild("NROAT")) Then
                            strChildNroTransaccion = rowChild("NROAT")
                        End If
                        tramaPagos &= strFecha & ";" & strChildNroTransaccion & ";" & strNombreDeudor & ";" & strClaseFactura & ";" & rowChild("VIA_PAGO") & ";" & rowChild("MONTO") & "|"
                    Next

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INI: Registro en VentasFactCuadre")
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Oficina venta : " & strOficinaVenta)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Fecha : " & strFecha)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Cajero : " & strCajero)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Descripcion Documento : " & strDescDocumento)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Nro Transaccion : " & strNroTransaccion)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Deudor : " & strNombreDeudor)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Vendedor : " & String.Empty)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Moneda : " & strMoneda)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Clase Factura : " & strClaseFactura)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Nro Couta: " & dblCero.ToString())
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Total Pago : " & totalPago.ToString())
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP saldo : " & totalPago.ToString())
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Referencia : " & String.Empty)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Estado : " & strEstadoDoc)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP Nodo : " & strNodo)
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  INP trama Pagos : " & tramaPagos)

                    dsResultado = objclsOffline.SetVentasFactCuadre(strOficinaVenta, strFecha, strCajero, strDescDocumento, _
                                        strNroTransaccion, strNombreDeudor, String.Empty, strMoneda, strClaseFactura, _
                                        dblCero, totalPago, totalPago, String.Empty, strEstadoDoc, strNodo, tramaPagos, iIDVentasFact)

                    If Not dsResultado Is Nothing Then
                        If dsResultado.Tables(0).Rows(0).Item("LOG") <> String.Empty Then
                            Dim strMsgErr As String = dsResultado.Tables(0).Rows(0).Item("LOG")
                            mensajeRecaudaciones = strMsgErr
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR en la transaccion nro: " & iIDVentasFact)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MENSAJE: " & mensajeRecaudaciones)
                            vExito = False
                            bProcesaDra = False
                            Exit For
                        Else
                            bProcesaDra = True
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "OUT Pago procesado nro: " & iIDVentasFact)
                        End If
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin: Registro en VentasFactCuadre ")
                Next

                If dsRecaudaciones.Tables(4).Rows.Count = 0 Then
                    bProcesaDra = True
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MENSAJE : Pago de recaudaciones DRA. No se encontró datos a procesar.")
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin: Pago de recaudaciones DRA")

                If bProcesaDeudasFijaMovil = False And (bProcesaDac = False And bProcesaDra = False) Then
                    mensajeRecaudaciones = "No se procesaron las recaudaciones de pagos."
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MENSAJE: " & mensajeRecaudaciones)
                Else
                    mensajeRecaudaciones = "Se procesaron las recaudaciones de pagos correctamente."
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MENSAJE: " & mensajeRecaudaciones)
                End If
            End If

            mensajes(0) = mensajeRecaudaciones
            bEjecucionRecaudaciones = (bProcesaDac Or bProcesaDra) Or bProcesaDeudasFijaMovil
            vExito = bEjecucionRecaudaciones

        Catch ex As Exception
            mensajes(0) = mensajeRecaudaciones
            vExito = False
            objclsOffline = Nothing
        Finally
            objclsOffline = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "EJECUCION " & IIf(bEjecucionRecaudaciones, "OK", "ERROR") & " RESPUESTA ENVIO RECAUDACIONES: " & mensajeRecaudaciones)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin:: fIngresarPagosSicar")
        End Try
        Return vExito
    End Function

    Private Function validaProcesoActivo(ByVal tipoCuadre As String, ByVal oficina As String, ByVal fecha As String) As Boolean
        Dim dsDatosCaja As DataSet
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Try
            If Not tipoCuadre.Equals("I") Then
                dsDatosCaja = objOffline.GetDatosAsignacionGeneral(CStr(Session("ALMACEN")), CDate(txtFecha.Text).ToString("yyyyMMdd"), String.Empty)
                If Not dsDatosCaja Is Nothing Then
                    If dsDatosCaja.Tables(0).Rows.Count > 0 Then
                        Dim row As DataRow = dsDatosCaja.Tables(0).Rows(0)
                        If row("CUADRE_REALIZADO") = "W" Then
                            validaProcesoActivo = True
                        Else
                            validaProcesoActivo = False
                        End If
                    Else
                        validaProcesoActivo = False
                    End If
                Else
                    validaProcesoActivo = False
                End If
            Else
                validaProcesoActivo = False
            End If
        Catch ex As Exception
            validaProcesoActivo = False
            Throw ex
        Finally
            objOffline = Nothing
        End Try
        Return validaProcesoActivo
    End Function

    Private Sub cambiarEstadoProcesando(ByVal IdentifyLog As String, ByVal tipoCuadre As String, _
                                            ByVal IdCaja As String, ByVal Estado As String)
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim dsDatosCaja As DataSet
        Try
            Dim strMensajeGen As String = String.Empty
            Dim strCuadreRealizado As String = String.Empty
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentifyLog & "- " & " Inicio Actualizar Estado Cuadre General - Procesando - cambiarEstadoProcesando()")

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentifyLog & "- " & " IN TipoCuadre : " & tipoCuadre)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentifyLog & "- " & " IN IdCaja : " & IdCaja)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentifyLog & "- " & " IN Estado : " & Estado)

            If Not tipoCuadre.Equals("I") Then
                ' Valida Cierre de Caja
                dsDatosCaja = objOffline.GetDatosAsignacionGeneral(CStr(Session("ALMACEN")), CDate(txtFecha.Text).ToString("yyyyMMdd"), String.Empty)
                If Not dsDatosCaja Is Nothing Then
                    If dsDatosCaja.Tables(0).Rows.Count > 0 Then
                        Dim row As DataRow = dsDatosCaja.Tables(0).Rows(0)
                        strCuadreRealizado = row("CUADRE_REALIZADO")
                    End If
                End If

                If strCuadreRealizado.Equals("N") Or strCuadreRealizado.Equals("W") Then
                    If Not IdCaja.Equals(String.Empty) Then
                        strMensajeGen = objOffline.ActualizarTransCuadreCajeroGen(IdCaja, Estado)
                    End If
                    If Not strMensajeGen.Equals(String.Empty) Then
                        Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strMensajeGen)
                        Me.RegisterStartupScript("RegistraAlerta33", script)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentifyLog & "- " & "No se actualizo estado a Procesando (W). Mensaje: " & strMensajeGen)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentifyLog & "- " & " Fin Actualizar Estado Cuadre General - Procesando - cambiarEstadoProcesando()")
            objOffline = Nothing
        End Try
    End Sub

    'FIN PRE_CUADRE_OFFLINE :: Agregado por TS-CCC
    'proy-27440
    Public Sub btnGrabarOculto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabarOculto.Click
        objclsOffline = New COM_SIC_OffLine.clsOffline
        Dim objclsConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim strTipoCuadre As String = UCase(Request.Item("tipocuadre"))
        Dim strIdentifyLog As String = Session("ALMACEN") & "|" & strUsuario
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - Inicio: btnGrabarOculto_Click")
        Try

            'FFS INICIO (SE AGREGO METODO)
            ' AUTORIZACION DE TRANSACCION DE PRE-CUADRE TS-JTN
            If Not chkCierre.Checked Then
                Dim objConf As New COM_SIC_Configura.clsConfigura
                Try
                    Dim autorizaOperacion As Boolean = False
                    Dim tipoCuadre$ = UCase(Request.Item("tipocuadre"))
                    Dim idOperacion As String = String.Format("CU-{0}{1}", Session("USUARIO"), DateTime.Now.ToString("ddMMyyyy"))
                    Dim intAutoriza% = objConf.FP_Inserta_Aut_Transac(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), Session("USUARIO"), Session("NOMBRE_COMPLETO"), "", "", _
                            "", "", "", idOperacion, 0, 5, 0, 0, 0, 0, 0, 0, "")

                    autorizaOperacion = (intAutoriza = 1)
                    If Not autorizaOperacion Then
                        Dim script$ = "<script language=javascript>alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador.')</script>"
                        Me.RegisterStartupScript("RegistraAlerta3", script)
                        Exit Sub
                    End If
                Catch ex As Exception
                    objConf = Nothing
                    Response.Write("<script> alert('" + ex.Message + "');</script>")
                Finally
                    objConf = Nothing
                End Try
            End If
            '' FIN AUTORIZACION DE TRANSACCION DE PRE-CUADRE TS-JTN

            Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
            Dim asignacionCajeroMensaje = objclsOffline.VerificarAsignacionCajero(Session("ALMACEN"), codUsuario, txtFecha.Text)
            If asignacionCajeroMensaje <> String.Empty Then
                Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", asignacionCajeroMensaje)
                'Response.Write(script)
                Me.RegisterStartupScript("RegistraAlerta3", script)
                Exit Sub
            End If

            'INICIO PRE_CUADRE_OFFLINE :: Agregado por TS-CCC

            Dim dsResultadoCC As DataSet
            Dim dtDocumentos As DataTable
            Dim strCierreCajaMensaje As String = String.Empty
            Dim strCajaBuzonMensaje As String = String.Empty
            Dim strMensajeGen As String

            If Not CBool(Session("FlujoAnteriorCuadre")) Then  'Nuevo Flujo
                Dim bActivo As Boolean = False
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Inicio: ValidaProcesoActivo")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Oficina Venta : " & CStr(Session("ALMACEN")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Fecha : " & txtFecha.Text)
                bActivo = validaProcesoActivo(strTipoCuadre, CStr(Session("ALMACEN")), txtFecha.Text)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - OUT Activo? : " & bActivo)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Fin: ValidaProcesoActivo")
                If bActivo Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - bntForzado_Click - Caja Asignada4: " & strCajaAsignadaID)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - valida el estado bActivo =  " & bActivo)
                    strCierreCajaMensaje = "El proceso de cuadre de caja esta siendo ejecutado."
                    Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strCierreCajaMensaje)
                    Me.RegisterStartupScript("RegistraAlertaProc", script)
                    'Exit Sub  'INC000002440268
                End If

                If strTipoCuadre.Equals("I") Then
                    dsResultadoCC = objclsOffline.GetDatosAsignacionCajero(Session("ALMACEN"), txtFecha.Text, codUsuario)
                    If Not dsResultadoCC Is Nothing Then
                        For i = 0 To dsResultadoCC.Tables(0).Rows.Count - 1
                            If dsResultadoCC.Tables(0).Rows(i).Item("CAJA_CERRADA") <> "N" Then
                                strCierreCajaMensaje = "La caja individual de la oficina de venta " & CStr(Session("ALMACEN")) & " - " & CType(Session("OFICINA"), String).Trim() & " ya ha sido cerrada."
                                Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strCierreCajaMensaje)
                                Me.RegisterStartupScript("RegistraAlertaCC", script)
                                Exit Sub
                            End If
                        Next
                    End If
                Else
                    strMensajeGen = objclsOffline.VerificarOficinaCerrada(Session("ALMACEN"), codUsuario, CDate(txtFecha.Text).ToString("yyyyMMdd"))
                    If strMensajeGen <> String.Empty Then
                        Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strMensajeGen)
                        Me.RegisterStartupScript("RegistraAlerta8", script)
                        Exit Sub
                    End If
                End If

                '<P_CAJA_BUZON> igual a cero el proceso termina indicando el error.
                'If CDbl(txtCaja.Text) = 0 Then
                '    strCajaBuzonMensaje = "Debe ingresar un monto a Caja Buzón."
                '    Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strCajaBuzonMensaje)
                '    Me.RegisterStartupScript("RegistraAlertaCB", script)
                '    Exit Sub
                'End If

                'Valida que no exista docuemntos por pagas pendientes solo cuadre general
                If Not strTipoCuadre.Equals("I") Then
                    Dim dsOficinas As DataSet = objclsOffline.Obtener_NewCodeOficinaVenta(CStr(Session("ALMACEN")))
                    Dim strOficinaVtaNuevo As String = String.Empty
                    Dim iCantidadDoc As Integer = 0
                    Dim drOficina As DataRow

                    If Not dsOficinas Is Nothing Then
                        If dsOficinas.Tables(0).Rows.Count > 0 Then
                            drOficina = dsOficinas.Tables(0).Rows(0)
                            With drOficina
                                strOficinaVtaNuevo = CStr(.Item("PAOFC_OFICINAVENTAS"))
                            End With
                        End If
                    End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Inicio: objclsConsultaMsSap.ConsultaPoolPagos")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Fecha: " & CDate(txtFecha.Text).ToString("dd/MM/yyyy"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Oficina: " & strOficinaVtaNuevo)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Estado: ACT")
                    dtDocumentos = objclsConsultaMsSap.ConsultaPoolPagos(ConfigurationSettings.AppSettings("PEDIC_ESTADO"), CDate(txtFecha.Text).ToString("dd/MM/yyyy"), strOficinaVtaNuevo, String.Empty)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Fin: objclsConsultaMsSap.ConsultaPoolPagos")

                    Dim FechaConsulta As String = CDate(txtFecha.Text).ToString("dd/MM/yyyy")

                    If Not dtDocumentos Is Nothing Then
                        'PROY-140397-MCKINSEY -> JSQ INICIO
                        iCantidadDoc = Validar_Documentos_Multipunto(dtDocumentos, FechaConsulta)
                        'PROY-140397-MCKINSEY -> JSQ FIN
                        If iCantidadDoc > 0 Then
                            Dim strMensajeDocumentoPend As String = "Existen documentos pendientes por pagar."
                            ViewState("MensajeDocumentosPend") = strMensajeDocumentoPend
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  WARNING: " & strMensajeDocumentoPend)
                            If chkCierre.Checked Then
                                Dim strMensajeDocumentoPend2 As String = "El cierre de caja no es posible, debido a que existen documentos por pagar generadas en el CAC." 'PROY-140397-MCKINSEY -> JSQ
                                Dim scriptDP2$ = String.Format("<script language=javascript>alert('{0}')</script>", strMensajeDocumentoPend2)
                                Me.RegisterStartupScript("RegistraAlertaDP2", scriptDP2)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & strMensajeDocumentoPend2)
                                Exit Sub
                            End If
                        End If
                    Else
                        Dim msjDocXPagar = "Error al obtener los documentos pendientes por pagar."
                        Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", msjDocXPagar)
                        Me.RegisterStartupScript("RegistraAlertaDPP", script)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " MENSAJE: " & msjDocXPagar)
                        'Exit Sub
                    End If
                End If

                'Obtiene la caja asignada al cajero
                If strTipoCuadre.Equals("I") Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Inicio: ObtenerCajaAsignadaCuadreIndividual")
                    strCajaAsignadaID = objclsOffline.ObtenerCajaAsignadaCuadreIndividual(Session("ALMACEN"), codUsuario, txtFecha.Text)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Caja Asignada: " & strCajaAsignadaID)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Fin: ObtenerCajaAsignadaCuadreIndividual")
                Else
                    'Se obtiene la caja asignada en caso no este asignado una caja se asigna una.
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Inicio: ObtenerCajaAsignadaCuadreGeneral")
                    strCajaAsignadaID = objclsOffline.ObtenerCajaAsignadaCuadreGeneral(Session("ALMACEN"), codUsuario, CDate(txtFecha.Text).ToString("yyyyMMdd"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Caja Asignada: " & strCajaAsignadaID)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Fin: ObtenerCajaAsignadaCuadreGeneral")
                    Dim strMensajeCajaAsignacion As String = String.Empty
                    If strCajaAsignadaID.Equals("0") Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio SetTransCuadreCajeroGen")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Oficina: " & CStr(Session("ALMACEN")))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp txtFecha: " & CDate(txtFecha.Text).ToString("yyyyMMdd"))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Usuario: " & codUsuario)
                        strMensajeCajaAsignacion = objclsOffline.SetTransCuadreCajeroGen(Session("ALMACEN"), CDate(txtFecha.Text).ToString("yyyyMMdd"), codUsuario, "N", strCajaAsignadaID)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   output Caja Asignada: " & strCajaAsignadaID)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Fin: SetTransCuadreCajeroGen")
                    End If

                    If Not strMensajeCajaAsignacion.Equals(String.Empty) Then
                        Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strMensajeCajaAsignacion)
                        Me.RegisterStartupScript("RegistraAlertaRCA", script)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "No se asigno caja. Mensaje: " & strMensajeCajaAsignacion)
                        Exit Sub
                    Else
                        'Cambia de estado a Procesando :: W = Waiting
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - btnGrabarOculto_Click - Caja Asignada1.0: " & strCajaAsignadaID)
                        cambiarEstadoProcesando(strIdentifyLog, strTipoCuadre, strCajaAsignadaID, WAITING)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - btnGrabarOculto_Click - Caja Asignada1.1: " & strCajaAsignadaID)
                    End If
                End If

                ' Se eliminan los registros de la Tabla <TI_MONTOS_CUAD> 
                Dim strMontosMensaje As String = String.Empty
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio EliminarMontosVentas")
                If strTipoCuadre.Equals("I") Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Tipo Cuadre: " & strTipoCuadre.ToUpper())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Oficina: " & CStr(Session("ALMACEN")))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Cajero: " & codUsuario.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Fecha: " & CDate(txtFecha.Text).ToString("yyyyMMdd"))
                    strMontosMensaje = objclsOffline.EliminarMontosVentas(Session("ALMACEN"), _
                                            codUsuario, CDate(txtFecha.Text).ToString("yyyyMMdd"), strTipoCuadre)
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Tipo Cuadre: G")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Oficina: " & CStr(Session("ALMACEN")))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Cajero: ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Fecha: " & CDate(txtFecha.Text).ToString("yyyyMMdd"))
                    strMontosMensaje = objclsOffline.EliminarMontosVentas(Session("ALMACEN"), _
                                                    String.Empty, CDate(txtFecha.Text).ToString("yyyyMMdd"), strTipoCuadre)
                End If

                If Not strMontosMensaje.Equals(String.Empty) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error: " & strMontosMensaje)
                    Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strMontosMensaje)
                    Me.RegisterStartupScript("RegistraAlertaMM", script)
                    Exit Sub
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin EliminarMontosVentas")
            Else
                'Response.Write("<script language=javascript>alert('Se procederá a actualizar los pagos y asignaciones de caja en SAP')</script>")
                Me.RegisterStartupScript("RegistraAlerta1", "<script language=javascript>alert('Se procederá a actualizar los pagos y asignaciones de caja en SAP')</script>")
            End If
            'FIN PRE_CUADRE_OFFLINE :: Agregado por TS-CCC

            Dim mensajes(2) As String
            Dim mensajeAlert$

            Dim procesaronPagos As Boolean = False
            If CBool(Session("FlujoAnteriorCuadre")) Then 'flujo anterior
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio Envio de pagos y recaudaciones a SAP")
                procesaronPagos = fIngresarPagosyAsignacionesSAP(mensajes)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN Envio de pagos y recaudaciones a SAP")
                mensajeAlert = IIf(procesaronPagos, String.Format("<script language=javascript>alert('{0}\n{1}\n{2}')</script>", mensajes(0), mensajes(1), mensajes(2)), _
                String.Format("<script language=javascript>alert('Estado del envío de transacciones a SAP: \n{0}\n{1}\n{2}');</script>", mensajes(0), mensajes(1), mensajes(2)))
                Me.RegisterStartupScript("RegistraAlerta2", mensajeAlert)
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio Envio de pagos y recaudaciones")
                procesaronPagos = fIngresarPagosSicar(mensajes)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   RESPUESTA Procesaron pagos y recaudaciones : " & procesaronPagos.ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN Envio de pagos y recaudaciones")
            End If

            If procesaronPagos Then
                mensajeAlert = String.Format("<script language=javascript>alert('{0}\n{1}\n{2}')</script>", mensajes(0), mensajes(1), mensajes(2))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Ejecutanto: ValidaEmisionDocPendienteMiClaro")
                Dim Procesar As Boolean = ValidaEmisionDocPendienteMiClaro()
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Resultado ValidaEmisionDocPendienteMiClaro : " + Procesar.ToString)
                If Procesar Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio: ProcesoCuadre")
                    ProcesoCuadre(True)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin: ProcesoCuadre")
                End If
                'Else
                '    mensajeAlert = String.Format("<script language=javascript>alert('Error al procesar las transacciones a SAP: \n{0}\n{1}\n{2}');</script>", mensajes(0), mensajes(1), mensajes(2))
            End If
            'Response.Write(mensajeAlert)
            If (Not procesaronPagos) Then
                Me.RegisterStartupScript("Registra", "<script>proceSar();</script>")
            End If
            'FFS FIN

        Catch ex As Exception
            objclsOffline = Nothing
            objclsConsultaMsSap = Nothing
            Response.Write("<script> alert('" + ex.Message + "');</script>")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - btnGrabarOculto_Click - Caja Asignada2.0: " & strCajaAsignadaID)
            cambiarEstadoProcesando(strIdentifyLog, strTipoCuadre, strCajaAsignadaID, PROCESS)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - btnGrabarOculto_Click - Caja Asignada2.1: " & strCajaAsignadaID)
            objclsOffline = Nothing
            objclsConsultaMsSap = Nothing
        End Try
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - fin: btnGrabarOculto_Click")
    End Sub
    'INICIATIVA-318 INI
    Private Sub MontosFaltantesSobrantes()
        Dim dblFaltante As Decimal
        Dim dblSobrante As Decimal
        Dim dblIngreso As Double = 0
        Dim dblEfectivo As Decimal
        Dim blnValidacion As Boolean = True
        Dim dMtoCajaBuzonAnterior As Double = 0
        Dim strFechaAnterior As String
        Dim dMtoCajaBuzonAnteriorPendiente As Double = 0
        Dim dMtoRemesaAnteriorEnviadaHoy As Double = 0
        Dim dMtoCajaBuzonAnteriorNoEnviadoAyer As Double = 0
        Dim dRemesaHoy As Double = 0
        Dim rDblFaltante As Double
        Dim rDblSobrante As Double

        If CBool(Session("FlujoAnteriorCuadre")) Then 'Flujo Anterior
            dblEfectivo = objCaja.FP_CalculaEfectivo(Session("ALMACEN"), strUsuario, txtFecha.Text)
        Else 'Nuevo Flujo
            dblEfectivo = objclsOffline.GetEfectivo(Session("ALMACEN"), strUsuario, txtFecha.Text)
        End If
        If (Math.Round(dblIngreso, 2) - Math.Round(dblEfectivo, 2)) < 0 Then
            dblFaltante = (Math.Round(dblIngreso, 2) - Math.Round(dblEfectivo, 2))
        Else
            dblSobrante = (Math.Round(dblIngreso, 2) - Math.Round(dblEfectivo, 2))
        End If

        dMtoCajaBuzonAnterior = objclsOffline.GetCajaBuzonAnterior(CStr(Session("ALMACEN")), txtFecha.Text, strFechaAnterior, dMtoCajaBuzonAnteriorPendiente, dMtoRemesaAnteriorEnviadaHoy, dMtoCajaBuzonAnteriorNoEnviadoAyer)
        dRemesaHoy = objclsOffline.GetRemesaHoy(Session("ALMACEN"), txtFecha.Text)

        setCuadreMontos(UCase(Request.Item("tipocuadre")), dblSobrante, _
                        dblFaltante, blnValidacion, dMtoCajaBuzonAnterior, _
                        strFechaAnterior, dRemesaHoy, dMtoRemesaAnteriorEnviadaHoy, _
                        dMtoCajaBuzonAnteriorPendiente, rDblSobrante, rDblFaltante)

        dblSobrante = rDblSobrante
        dblFaltante = rDblFaltante

        If dblSobrante > 0 Then
            txtMontoP.Text = Format(Math.Abs(dblSobrante), "######0.00")
            lblMontoP.Text = "Efectivo Sobrante :" 'PROY -27440
        End If

        If Math.Abs(dblFaltante) >= 0 And dblSobrante = 0 Then
            txtMontoP.Text = Format(Math.Abs(dblFaltante), "######0.00")
            lblMontoP.Text = "Efectivo Faltante :" 'PROY -27440
        End If
    End Sub

    Private Sub ForzarCuadre()
        Dim strTipoCuadre As String = UCase(Request.Item("tipocuadre"))
        If strTipoCuadre.Equals("I") Then
            strUsuario = Session("USUARIO")
        Else
            strUsuario = ""
        End If
        Dim strIdentifyLog As String = Session("ALMACEN") & "|" & strUsuario
        objclsOffline = New COM_SIC_OffLine.clsOffline
        Dim objclsConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
        Dim dsResultadoCC As DataSet
        Dim strMensajeGen As String
        Dim strCierreCajaMensaje As String
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - Inicio: bntForzado_Click")
        Try
            trCierre.Visible = True

            If CBool(Session("FlujoAnteriorCuadre")) Then   'Flujo Anterior
                ProcesoCuadre(False)
            Else
                'INI :: valida que el proceso de forzado ya se este ejecutando.
                Dim bActivo As Boolean = False
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Inicio: ValidaProcesoActivo")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Oficina Venta : " & CStr(Session("ALMACEN")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - INP Fecha : " & txtFecha.Text)
                bActivo = validaProcesoActivo(strTipoCuadre, CStr(Session("ALMACEN")), txtFecha.Text)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - OUT Activo? : " & bActivo)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   setCuadreMontos - Fin: ValidaProcesoActivo")
                If bActivo Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - bntForzado_Click - Caja Asignada3: " & strCajaAsignadaID)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - valida el estado bActivo =  " & bActivo)
                    strCierreCajaMensaje = "El proceso forzado de cuadre de caja ya esta siendo ejecutado."
                    Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strCierreCajaMensaje)
                    Me.RegisterStartupScript("RegistraAlertaProc", script)
                    'Exit Sub  'INC000002440268
                End If
                'FIN
                Dim dtDocumentos As DataTable
                Dim dsOficinas As DataSet = objclsOffline.Obtener_NewCodeOficinaVenta(CStr(Session("ALMACEN")))
                Dim strOficinaVtaNuevo As String = String.Empty
                Dim iCantidadDoc As Integer = 0
                Dim drOficina As DataRow

                'Cuadre Ind y Gen: valida si la caja esta cerrada 
                If strTipoCuadre.Equals("I") Then
                    dsResultadoCC = objclsOffline.GetDatosAsignacionCajero(Session("ALMACEN"), txtFecha.Text, codUsuario)
                    If Not dsResultadoCC Is Nothing Then
                        For i = 0 To dsResultadoCC.Tables(0).Rows.Count - 1
                            If dsResultadoCC.Tables(0).Rows(i).Item("CAJA_CERRADA") <> "N" Then
                                strCierreCajaMensaje = "La caja individual de la oficina de venta " & CStr(Session("ALMACEN")) & " - " & CType(Session("OFICINA"), String).Trim() & " ya ha sido cerrada."
                                Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strCierreCajaMensaje)
                                Me.RegisterStartupScript("RegistraAlertaCC", script)
                                Exit Sub
                            End If
                        Next
                    End If
                Else
                    strMensajeGen = objclsOffline.VerificarOficinaCerrada(Session("ALMACEN"), codUsuario, CDate(txtFecha.Text).ToString("yyyyMMdd"))
                    If strMensajeGen <> String.Empty Then
                        Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strMensajeGen)
                        Me.RegisterStartupScript("RegistraAlerta8", script)
                        Exit Sub
                    End If
                End If

                'Cuadre General: Valida documentos por pagar
                If Not strTipoCuadre.Equals("I") Then
                    If Not dsOficinas Is Nothing Then
                        If dsOficinas.Tables(0).Rows.Count > 0 Then
                            drOficina = dsOficinas.Tables(0).Rows(0)
                            With drOficina
                                strOficinaVtaNuevo = CStr(.Item("PAOFC_OFICINAVENTAS"))
                            End With
                        End If
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Inicio: objclsConsultaMsSap.ConsultaPoolPagos")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Fecha: " & CDate(txtFecha.Text).ToString("dd/MM/yyyy"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Oficina: " & strOficinaVtaNuevo)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Estado: ACT")
                    dtDocumentos = objclsConsultaMsSap.ConsultaPoolPagos(ConfigurationSettings.AppSettings("PEDIC_ESTADO"), CDate(txtFecha.Text).ToString("dd/MM/yyyy"), strOficinaVtaNuevo, String.Empty)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Fin: objclsConsultaMsSap.ConsultaPoolPagos")
                    'PROY-140397-MCKINSEY -> JSQ INICIO
                    Dim FechaConsulta As String = CDate(txtFecha.Text).ToString("dd/MM/yyyy")
                    iCantidadDoc = Validar_Documentos_Multipunto(dtDocumentos, FechaConsulta)
                    'PROY-140397-MCKINSEY -> JSQ FIN
                    If iCantidadDoc > 0 Then
                        Dim strMensajeDocumentoPend As String = "El cuadre no es posible, debido a que existen documentos por pagar."
                        Dim scriptDP$ = String.Format("<script language=javascript>alert('{0}')</script>", strMensajeDocumentoPend)
                        Me.RegisterStartupScript("RegistraAlertaDP", scriptDP)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & strMensajeDocumentoPend)
                        Exit Sub
                    End If
                End If

                'Obtiene la caja asignada al cajero
                If strTipoCuadre.Equals("I") Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Inicio: ObtenerCajaAsignadaCuadreIndividual")
                    strCajaAsignadaID = objclsOffline.ObtenerCajaAsignadaCuadreIndividual(Session("ALMACEN"), codUsuario, txtFecha.Text)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Caja Asignada: " & strCajaAsignadaID)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Fin: ObtenerCajaAsignadaCuadreIndividual")
                Else
                    'Se obtiene la caja asignada en caso no este asignado una caja se asigna una.
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Inicio: ObtenerCajaAsignadaCuadreGeneral")
                    strCajaAsignadaID = objclsOffline.ObtenerCajaAsignadaCuadreGeneral(Session("ALMACEN"), codUsuario, CDate(txtFecha.Text).ToString("yyyyMMdd"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Caja Asignada: " & strCajaAsignadaID)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Fin: ObtenerCajaAsignadaCuadreGeneral")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Inicio: cambiarEstadoProcesando")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - bntForzado_Click - Caja Asignada1.0: " & strCajaAsignadaID)
                    cambiarEstadoProcesando(strIdentifyLog, strTipoCuadre, strCajaAsignadaID, WAITING)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - bntForzado_Click - Caja Asignada1.1: " & strCajaAsignadaID)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Fin: cambiarEstadoProcesando")
                End If

                'Se valida caja individual anterior si se cerro.
                Dim strMensajeCierreAnterior As String = String.Empty
                If strTipoCuadre.Equals("I") Then
                    strMensajeCierreAnterior = objclsOffline.GetCierreCajaIndividualAnterior(Session("ALMACEN"), strCajaAsignadaID, txtFecha.Text)
                    If Not strMensajeCierreAnterior.Equals(String.Empty) Then
                        Dim strMsjContinuar As String = " El proceso continuará."
                        Dim scriptDP$ = String.Format("<script language=javascript>alert('{0}')</script>", strMensajeCierreAnterior & strMsjContinuar)
                        Me.RegisterStartupScript("RegistraAlertaCCA", scriptDP)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & strMensajeCierreAnterior & strMsjContinuar)
                    End If
                Else 'Se valida caja general anterior si se cerro.
                    'Dim strFechaLim As String = ConfigurationSettings.AppSettings("FechaFinConsultaRFC_RepCuadre")
                    Dim flagSinergia As String
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Inicio: GetFechaOficinaSinergia")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  IN OFICINA : " & CStr(Session("ALMACEN")))
                    Dim strFechaLim As String = objclsOffline.GetFechaOficinaSinergia(CStr(Session("ALMACEN")), flagSinergia)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  OUT FECHA : " & strFechaLim)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  OUT FLAG : " & flagSinergia)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Fin: GetFechaOficinaSinergia")

                    Dim anio As Integer = CInt(strFechaLim.Substring(6, 4))
                    Dim mes As Integer = CInt(strFechaLim.Substring(3, 2))
                    Dim dia As Integer = CInt(strFechaLim.Substring(0, 2))
                    Dim dFechaLim As New Date(anio, mes, dia)
                    Dim dFecha As Date = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)

                    If Not (dFecha = dFechaLim) Then 'Diferente de la fecha del Pase a produccion
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INI - Forzar -  GetCierreCajaGeneralAnterior()")
                        strMensajeCierreAnterior = objclsOffline.GetCierreCajaGeneralAnterior(Session("ALMACEN"), txtFecha.Text)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strMensajeCierreAnterior : " & strMensajeCierreAnterior)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN - Forzar -  GetCierreCajaGeneralAnterior()")
                        If Not strMensajeCierreAnterior.Equals(String.Empty) Then
                            Dim scriptDP$ = String.Format("<script language=javascript>alert('{0}')</script>", strMensajeCierreAnterior)
                            Me.RegisterStartupScript("RegistraAlertaCCA", scriptDP)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & strMensajeCierreAnterior)
                            Exit Sub
                        End If
                    End If
                End If

                ' Se eliminan los registros de la Tabla <TI_MONTOS_CUAD> 
                Dim strMontosMensaje As String = String.Empty
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio EliminarMontosVentas")
                If strTipoCuadre.Equals("I") Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Tipo Cuadre: " & strTipoCuadre.ToUpper())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Oficina: " & CStr(Session("ALMACEN")))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Cajero: " & codUsuario.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Fecha: " & CDate(txtFecha.Text).ToString("yyyyMMdd"))
                    strMontosMensaje = objclsOffline.EliminarMontosVentas(Session("ALMACEN"), _
                                            codUsuario, CDate(txtFecha.Text).ToString("yyyyMMdd"), strTipoCuadre)
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Tipo Cuadre: G")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Oficina: " & CStr(Session("ALMACEN")))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Cajero: ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inp Fecha: " & CDate(txtFecha.Text).ToString("yyyyMMdd"))
                    strMontosMensaje = objclsOffline.EliminarMontosVentas(Session("ALMACEN"), _
                                                       String.Empty, CDate(txtFecha.Text).ToString("yyyyMMdd"), strTipoCuadre)
                End If

                If Not strMontosMensaje.Equals(String.Empty) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error: " & strMontosMensaje)
                    Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strMontosMensaje)
                    Me.RegisterStartupScript("RegistraAlertaMM", script)
                    Exit Sub
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin EliminarMontosVentas")

                Dim mensajes(2) As String
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio Envio de pagos y recaudaciones")
                Dim procesaronPagos As Boolean = fIngresarPagosSicar(mensajes)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   RESPUESTA Procesaron pagos y recaudaciones : " & procesaronPagos.ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN Envio de pagos y recaudaciones")
                If procesaronPagos Then
                    ProcesoCuadre(False)
                Else
                    Me.RegisterStartupScript("Registra", "<script>proceSar();</script>")
                End If
            End If
        Catch ex As Exception
            Me.RegisterStartupScript("errorCuadre1", "<script language=javascript>alert('" & ex.Message & "')</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " ProcesoCuadre - Forzar Cuadre- ERROR :  " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - bntForzado_Click - Caja Asignada2.0: " & strCajaAsignadaID)
            cambiarEstadoProcesando(strIdentifyLog, strTipoCuadre, strCajaAsignadaID, PROCESS)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - bntForzado_Click - Caja Asignada2.1: " & strCajaAsignadaID)
            objclsConsultaMsSap = Nothing
            objclsOffline = Nothing
        End Try
       objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INC000002440268 - fin: bntForzado_Click")
    End Sub
    'INICIATIVA-318 FIN

    'INICIATIVA-318 - ARQUEO INI
    Private Sub btnRegregsar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegregsar.Click
        RegresarArqueoCaja(Me.hidEstado.Value)
    End Sub

    Private Sub RegresarArqueoCaja(ByVal strEstado As String)
        Response.Redirect("../Recaudacion/SICAR_Arqueo_Caja.aspx?estado=" & Me.hidEstado.Value & "&fecha=" & txtFecha.Text)
    End Sub
    'INICIATIVA-318 - ARQUEO FIN

End Class
