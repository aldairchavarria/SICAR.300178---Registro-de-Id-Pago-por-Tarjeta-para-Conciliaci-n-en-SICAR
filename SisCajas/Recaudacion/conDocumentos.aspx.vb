Imports COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
Imports SisCajas.Funciones

Public Class conDocumentos
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
    Protected WithEvents intCanal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents intAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtTramaDeudaSAP As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnFechaBusqueda As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnRutaLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDetalleLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cmdAnular As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents cmdContinuar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents cmdCancelar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtNumeroDeudaSAP As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents dgrPagos As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgrRecibos As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtFechaDeudaSAP As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtIdentificador As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNombreCliente As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNumeroDocumentos As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtValorDeuda As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdImprmir As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtTipoCli As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hdnTipoCambioPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidEstadoServicioPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnTipoDocumento As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents numeroDocumentoHiden As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidEstadoSicar As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cmdGrabar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dgrPagosE As System.Web.UI.WebControls.DataGrid

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    Public Recibos As String
    Public ContTarjeta As Int16 = 0 'proy-27440
    Dim dsDeuda As DataSet
    Public Const cteCODMONEDA_SOLES = "604"
    Public Const cteVALMONEDA_SOLES = "PEN"
    Public Const cteCODMONEDA_DOLARES = "840"
    Public Const cteVALMONEDA_DOLARES = "USD"
    Dim strNumeroDeudaSAP As String

    '///Pagina Invocante
    Property PAGINA_INICIAL() As String '//E75810
        Get
            Return Funciones.CheckStr(Me.ViewState("PAGINA_INICIAL"))
        End Get
        Set(ByVal Value As String)
            Me.ViewState("PAGINA_INICIAL") = Value
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecFijoPaginas")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecFijoPaginas")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = Me.txtNumeroDeudaSAP.Value
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " Page_Load INICIO "))
        'Introducir aquí el código de usuario para inicializar la página
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                Try
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " LeeParametros INI "))
                    LeeParametros()
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " LeeParametros FIN "))
                    LeeDatos()
                    'INICIATIVA - 529 INI
                    If Request.QueryString("accion") = "E" Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", " QueryString('accion') ", Request.QueryString("accion")))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " Habilitar_Edicion INI "))
                        Habilitar_Edicion()
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " Habilitar_Edicion FIN "))
                    Else
                        cmdGrabar.Visible = False
                    End If
                    'INICIATIVA - 529 FIN
                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", " Page_Load Error ", ex.Message))
                    Response.Write("<SCRIPT> alert('" + ex.Message + "');</SCRIPT>")
                End Try
            End If
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " Page_Load FIN "))
    End Sub

    '///E75810
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

        If Request.QueryString("act") Is Nothing OrElse Request.QueryString("act") = "" Then
            HabilitarBotones(0)
        Else
            HabilitarBotones(Request.QueryString("act"))
        End If

        If Not Request.QueryString("fec") Is Nothing AndAlso Request.QueryString("fec") <> "" Then
            hdnFechaBusqueda.Value = Request.QueryString("fec")
        Else
            hdnFechaBusqueda.Value = ""
        End If
        '--E75810
        If Not Request.QueryString("tc") Is Nothing AndAlso Request.QueryString("tc") <> String.Empty Then
            hdnTipoCambioPago.Value = Request.QueryString("tc")
        Else
            hdnTipoCambioPago.Value = String.Empty
        End If

        If Not Request.QueryString("es") Is Nothing AndAlso Request.QueryString("es") <> String.Empty Then
            hidEstadoServicioPago.Value = Request.QueryString("es")
        Else
            hidEstadoServicioPago.Value = String.Empty
        End If

        ' INICIATIVA-529 INI
        If Not Request.QueryString("estsicar") Is Nothing AndAlso Request.QueryString("estsicar") <> String.Empty Then
            hidEstadoSicar.Value = Request.QueryString("estsicar")
        Else
            hidEstadoSicar.Value = String.Empty
        End If
        ' INICIATIVA-529 FIN

        strNumeroDeudaSAP = Request.QueryString("num")
        Me.ViewState("PAGINA_INICIAL") = Me.Session("PAGINA_INICIAL")   '//Para ir cuando se desea continuar

    End Sub

    Public Sub LeeDatos()
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecFijoPaginas")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecFijoPaginas")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = Me.txtNumeroDeudaSAP.Value
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " LeeDatos INICIO "))
        'Dim objRecBusiness As New COM_SIC_RecBusiness.clsRecBusiness
        'CAMBIADO POR FFS INICIO
        'dsDeuda = ObtenerDocumentoDeudaSAP(txtNumeroDeudaSAP.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " ObtenerDocumentoDeudaSAP INICIO "))
        dsDeuda = ObtenerDocumentoDeudaSAP(strNumeroDeudaSAP)
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " ObtenerDocumentoDeudaSAP FIN "))
        'CAMBIADO POR FFS FIN

        If Not dsDeuda Is Nothing Then
            Dim drFila As DataRow
            For Each drFila In dsDeuda.Tables(2).Rows
                drFila(3) = FormatNumber(drFila(3), 2)
               'proy-27440
                If Convert.ToString(drFila(6)).IndexOf("TARJETA") > -1 Then
                    ContTarjeta = ContTarjeta + 1
                End If
            Next
            'INICIATIVA - 529 INI
            If Request.QueryString("accion") = "E" Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", " Request.QueryString('accion') ", Request.QueryString("accion")))
                dgrPagos.Visible = False
                dgrPagosE.DataSource = dsDeuda.Tables(2)
                dgrPagosE.DataBind()
            Else
                dgrPagosE.Visible = False
            dgrPagos.DataSource = dsDeuda.Tables(2)
            dgrPagos.DataBind()
            End If
            'INICIATIVA - 529 FIN

            dgrRecibos.DataSource = dsDeuda.Tables(1)
            dgrRecibos.DataBind()

            'txtNumeroDeudaSAP.Value = CStr(dsDeuda.Tables(0).Rows(0)("NRO_TRANSACCION"))
            'txtFechaDeudaSAP.Value = CStr(dsDeuda.Tables(0).Rows(0)("FECHA_TRANSAC"))
            'txtIdentificador.Value = CStr(dsDeuda.Tables(0).Rows(0)("RUC_DEUDOR"))
            'txtNombreCliente.Value = CStr(dsDeuda.Tables(0).Rows(0)("NOM_DEUDOR"))
            'txtNumeroDeudaSAP.Value = Convert.ToString(dsDeuda.Tables(0).Rows(0)("NRO_TRANSACCION"))

            txtFechaDeudaSAP.Value = Convert.ToString(dsDeuda.Tables(0).Rows(0)("FECHA_TRANSAC"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", " txtFechaDeudaSAP.Value ", txtFechaDeudaSAP.Value))
            txtIdentificador.Value = Convert.ToString(dsDeuda.Tables(0).Rows(0)("RUC_DEUDOR"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", "  txtIdentificador.Value ", txtIdentificador.Value))
            txtNombreCliente.Value = Convert.ToString(dsDeuda.Tables(0).Rows(0)("NOM_DEUDOR"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", " txtNombreCliente.Value ", txtNombreCliente.Value))


            ''CAMBIADO POR TS.JTN INICIO
            numeroDocumentoHiden.Value = Request.QueryString("num")
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", " numeroDocumentoHiden.Value ", numeroDocumentoHiden.Value))
            ''CAMBIADO POR TS.JTN FIN

            'CAMBIADO POR FFS INICIO
            If Request.QueryString("est") = "PROCESADO" Then
                txtNumeroDeudaSAP.Value = Convert.ToString(dsDeuda.Tables(0).Rows(0)("NRO_TRANSACCION"))
                strNumeroDeudaSAP = Convert.ToString(dsDeuda.Tables(0).Rows(0)("NRO_TRANSACCION"))
            Else
                txtNumeroDeudaSAP.Value = ""
                strNumeroDeudaSAP = Request.QueryString("num")
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", " Request.QueryString('est') ", Request.QueryString("est")))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", " txtNumeroDeudaSAP.Value ", txtNumeroDeudaSAP.Value))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", " strNumeroDeudaSAP ", strNumeroDeudaSAP))
            'CAMBIADO POR FFS FIN

            txtNumeroDocumentos.Value = dsDeuda.Tables(1).Rows.Count.ToString
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", "  txtNumeroDocumentos.Value ", txtNumeroDocumentos.Value))
            Dim sValorDeuda As Double = Funciones.CheckDbl((dsDeuda.Tables(0).Rows(0)("IMPORTE_PAGO")))
            txtValorDeuda.Value = String.Format("{0:f2}", sValorDeuda)
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", " txtValorDeuda.Value ", txtValorDeuda.Value))

            hdnTipoDocumento.Value = dgrRecibos.Items(0).Cells(0).Text()
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", "  hdnTipoDocumento.Value ", hdnTipoDocumento.Value))

        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", " LeeDatos Error - Hubo un error en el procesamientodel pago. "))
            Response.Write("<SCRIPT> alert('Hubo un error en el procesamientodel pago.'); document.location='" & Me.PAGINA_INICIAL & "';</SCRIPT>")
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " LeeDatos FIN "))
    End Sub

    '//E75810
    Private Sub cmdContinuar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdContinuar.ServerClick
        If (Me.PAGINA_INICIAL = String.Empty) Then
            Response.Redirect("bsqDocumentos.aspx")
        Else
            Response.Redirect(Me.PAGINA_INICIAL)
        End If
    End Sub

    '//E75810
    Private Sub cmdCancelar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.ServerClick
        If Me.hdnFechaBusqueda.Value.Trim().Length > 0 Then
            If (Me.PAGINA_INICIAL = String.Empty) Then
                Response.Redirect("grdDocumentos.aspx?fecbus=" + Me.hdnFechaBusqueda.Value.Trim()) '????pooll de anulacion, como
            Else
                Response.Redirect(Me.PAGINA_INICIAL & "?fecbus=" + Me.hdnFechaBusqueda.Value.Trim())
            End If
        Else
            '//Response.Redirect("bsqDocumentos.aspx")
            cmdContinuar_ServerClick(sender, e)    '//E75810
        End If
    End Sub

    Private Sub cmdAnular_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnular.ServerClick

        ' INICIATIVA-529 INI
        If hidEstadoSicar.Value = "ANULADA" Then
            Response.Write("<SCRIPT> alert('" + "El Documento no se puede anular" + "');</SCRIPT>")
            Exit Sub
        End If
        ' INICIATIVA-529 INI

        Dim sMensaje As String = String.Empty
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecFijoPaginas")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecFijoPaginas")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = Me.txtNumeroDeudaSAP.Value
        Dim strIntegracionPOS As String = ""
        ' INI 27440

        'CNH 2017-10-09 INI

        Dim strPtoVenta As String = Funciones.CheckStr(Session("ALMACEN"))
        Dim strIpClient As String = Funciones.CheckStr(Session("IpLocal"))

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : Validacion Integracion INI")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "strPtoVenta : " & strPtoVenta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "strIpClient : " & strIpClient)


        Dim strCodRptaFlag As String = ""
        Dim strMsgRptaFlag As String = ""

        Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS

        'INI CONSULTA INTEGRACION AUTOMATICO POS

        Dim strFlagIntAut As String = ""

        strCodRptaFlag = "" : strMsgRptaFlag = ""
        objConsultaPos.Obtener_Integracion_Auto(Funciones.CheckStr(strPtoVenta), strIpClient, String.Empty, strFlagIntAut, strCodRptaFlag, strMsgRptaFlag)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "strFlagIntAut : " & Funciones.CheckStr(strFlagIntAut))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "strCodRptaFlag : " & Funciones.CheckStr(strCodRptaFlag))
        'INI PROY-140126
        Dim MaptPath As String
        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
        MaptPath = "( Class : " & MaptPath & "; Function: cmdAnular_ServerClick)"
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "strMsgRptaFlag : " & Funciones.CheckStr(strMsgRptaFlag) & MaptPath)
        'FIN PROY-140126



        strIntegracionPOS = Funciones.CheckStr(strFlagIntAut)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : Validacion Integracion FIN")
        'FIN CONSULTA INTEGRACION AUTOMATICO POS
        'CNH 2017-10-09 FIN

        Dim strPedido As String
        Dim strFecha As String
        Dim strNombre As String
        Dim strMontoPago As String
        Dim strFechaTrans As Date
    Dim strEstadoTrans As String = "ERROR DE PROCESO"

    strPedido = Funciones.CheckStr(numeroDocumentoHiden.Value)
    strFecha = Funciones.CheckStr(Me.txtFechaDeudaSAP.Value)
    strNombre = Funciones.CheckStr(Me.txtNombreCliente.Value)
    strMontoPago = Me.txtValorDeuda.Value

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Anular cmdAnular_ServerClick")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Conteo FormaPago - Inicio")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")

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
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    INP txtNumeroDeudaSAP:  " & txtNumeroDeudaSAP.Value)


      'CNH-INI-2017-12-18 '27440
      If strIntegracionPOS = "1" Then
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    INP Inicio INTEGRACION")
            ''CALLBACK ORACLE
        intAutoriza = objConf.FP_Inserta_Aut_Transac(Session("CANAL"), _
        Me.hdnPuntoDeVenta.Value, ConfigurationSettings.AppSettings("codAplicacion"), _
        Me.hdnUsuario.Value, Session("NOMBRE_COMPLETO"), "", "", _
                      "", "", "", Me.numeroDocumentoHiden.Value, 0, 10, 0, 0, 0, 0, 0, 0, "")

            '  intAutoriza = objConf.FP_Inserta_Aut_Transac(Session("CANAL"), Me.hdnPuntoDeVenta.Value, 93, Me.hdnUsuario.Value, Session("NOMBRE_COMPLETO"), "", "", _
            '"", "", "", Me.txtNumeroDeudaSAP.Value, 0, 10, 0, 0, 0, 0, 0, 0, "")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    OUT intAutoriza:  " & intAutoriza.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin  FP_Inserta_Aut_Transac ")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP NroTransac  " & txtNumeroDeudaSAP.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP RutaLog  " & hdnRutaLog.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP DetalleLog  " & Me.hdnDetalleLog.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP EstadoTransActual:  " & hidEstadoServicioPago.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP TipoCambio:  " & hdnTipoCambioPago.Value)


'PROY-27440 - INI
        If intAutoriza = 1 Then
          Response.Redirect("SICAR_FormaPagos.aspx?pacc=" & "1" & "&pid=" & strPedido & "&pfecha=" & strFecha & "&pnombre=" & strNombre & "&pmonto=" & strMontoPago & "&es=" & strEstadoTrans)
        Else
          Response.Write("<script language=jscript> alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador'); </script>")
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    INP Final INTEGRACION")
      Else
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    INP Inicio Flujo Regular")

            ''CALLBACK ORACLE
        intAutoriza = objConf.FP_Inserta_Aut_Transac(Session("CANAL"), Me.hdnPuntoDeVenta.Value, _
        ConfigurationSettings.AppSettings("codAplicacion"), Me.hdnUsuario.Value, Session("NOMBRE_COMPLETO"), "", "", _
                      "", "", "", Me.numeroDocumentoHiden.Value, 0, 10, 0, 0, 0, 0, 0, 0, "")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    OUT intAutoriza:  " & intAutoriza.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin  FP_Inserta_Aut_Transac ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP NroTransac  " & txtNumeroDeudaSAP.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP RutaLog  " & hdnRutaLog.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP DetalleLog  " & Me.hdnDetalleLog.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP EstadoTransActual:  " & hidEstadoServicioPago.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP TipoCambio:  " & hdnTipoCambioPago.Value)
'PROY-27440 - FIN
            If intAutoriza = 1 Then
                Dim dTipoCambio As Double = Funciones.CheckDbl(hdnTipoCambioPago.Value)
                '''CALLBACK ORACLE
                Dim strResult As String = obAnul.AnularPago(ConfigurationSettings.AppSettings("CodAplicacion"), Session("CANAL"), Me.hdnRutaLog.Value, _
                                        Me.hdnDetalleLog.Value, Me.hdnPuntoDeVenta.Value, Me.intCanal.Value, _
                                        Me.hdnBinAdquiriente.Value, Me.hdnCodComercio.Value, Me.hdnUsuario.Value, _
                                        Me.numeroDocumentoHiden.Value, String.Empty, String.Empty, String.Empty, True, _
                                        hidEstadoServicioPago.Value, dTipoCambio)

                '---logs de recibos procesados
                If Not obAnul.sbLineasLog Is Nothing Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, obAnul.sbLineasLog.ToString())
                End If
                obAnul.sbLineasLog = Nothing '//reinicializa
                '--Más logs
                    'INI PROY-140126
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: cmdAnular_ServerClick)"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT strResult:  " & strResult & MaptPath)
                    'FIN PROY-140126
                Dim arrMensaje() As String = strResult.Split("@")

                If ExisteError(arrMensaje) Then
                    Response.Write("<SCRIPT> alert('" + arrMensaje(1) + "');</SCRIPT>")
                    sMensaje = "Hubo un error. " & arrMensaje(1)
                Else
                    '//E75810
                    Dim sPaginaInicial As String
                    If Me.PAGINA_INICIAL <> String.Empty Then
                        sPaginaInicial = Me.PAGINA_INICIAL
                    Else
                        sPaginaInicial = "grdDocumentos.aspx"
                    End If
                    '---
                    sMensaje = "Se procesó exitósamente la anulación."
                    Response.Write("<SCRIPT> alert('Se procesó exitosamente la anulación.'); document.location='" & sPaginaInicial & "';</SCRIPT>")
                End If
            Else
                sMensaje = "Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador."
                Response.Write("<script language=jscript> alert('" & sMensaje & "'); </script>")
            End If


      End If 'PROY-27440 

        Catch ex As Exception
            'INI PROY-140126
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: cmdAnular_ServerClick)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR: " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126
            sMensaje = "Hubo un error. " & ex.Message
            Response.Write("<script language=jscript> alert('" & ex.Message & "'); </script>")
        Finally
            sMensaje = "Anulación de Recaudación. " & sMensaje & ". Datos: Canal=" & intCanal.Value & "|PDV=" & hdnPuntoDeVenta.Value & "|Cajero=" & _
                                    hdnUsuario.Value & "|Nro. Documento SAP=" & Me.txtNumeroDeudaSAP.Value
            '---
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Anular Pago - Fin")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Anular cmdAnular_ServerClick")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")

            objFileLog = Nothing
            '--registra auditoria
            RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("codTrxFijaPagAnularPago"))
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

    Public Function ObtenerDocumentoDeudaSAP(ByVal strNumSAP As String) As DataSet

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        'Dim intSAP = objOffline.Get_ConsultaSAP

        ''Dim obSap As New SAP_SIC_Recaudacion.clsRecaudacion

        'Dim obSap As Object
        'If intSAP = 1 Then
        '    obSap = New SAP_SIC_Recaudacion.clsRecaudacion
        'Else
        '    obSap = New COM_SIC_OffLine.clsOffline
        'End If

        'Return obSap.Get_RegistroDeuda(strNumSAP)
        Return objOffline.GetRegistroDeuda(strNumSAP)

    End Function

    Private Sub HabilitarBotones(ByVal Accion As String)
        Select Case Accion
            Case "1"
                cmdAnular.Visible = True
                cmdCancelar.Visible = False
                cmdImprmir.Visible = True
                cmdContinuar.Visible = True
            Case "2"
                cmdAnular.Visible = True
                cmdCancelar.Visible = True
                cmdImprmir.Visible = True
                cmdContinuar.Visible = False

            Case Else
                cmdAnular.Visible = True
                cmdCancelar.Visible = True
                cmdImprmir.Visible = True
                cmdContinuar.Visible = True
        End Select
    End Sub

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

    '///E75810
    Private Sub dgrRecibos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrRecibos.ItemDataBound
        '--
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblControl As Label = CType(e.Item.FindControl("lblDOC_DescMoneda"), Label)
            Dim lblImporteRecibo As Label = CType(e.Item.FindControl("lblImporteRecibo"), Label)
            'inicio LGZ 09-10-2013
            Dim lblImportePagado As Label = CType(e.Item.FindControl("lblImportePagado"), Label)
            'final LGZ 09-10-2013
            Dim sCodigo As String
            If Not lblControl Is Nothing Then
                sCodigo = lblControl.Text
                '--asigna descripcion correspondiente a moneda
                If sCodigo = cteCODMONEDA_SOLES Then
                    lblControl.Text = cteVALMONEDA_SOLES
                ElseIf sCodigo = cteCODMONEDA_DOLARES Then
                    lblControl.Text = cteVALMONEDA_DOLARES
                Else '//Moneda desconocida
                    lblControl.Text = "MONEDA " & sCodigo
                End If
                lblImporteRecibo.Text = String.Format("{0:f2}", CDbl(lblImporteRecibo.Text))
                'inicio LGZ 09-10-2013
                lblImportePagado.Text = String.Format("{0:f2}", CDbl(lblImportePagado.Text))
                'final LGZ 09-10-2013
            End If
        End If
    End Sub

    Private Sub cmdImprmir_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImprmir.ServerClick

    End Sub
    'INICIATIVA - 529 INI
    Private Function Habilitar_Edicion()
        cmdAnular.Visible = False
        cmdImprmir.Visible = False
        cmdContinuar.Visible = False
    End Function

    Private Sub dgrPagosE_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrPagosE.ItemDataBound
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecFijoPaginas")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecFijoPaginas")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = Me.txtNumeroDeudaSAP.Value
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " dgrPagosE_ItemDataBound INICIO "))
        Try
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " dgrPagosE_ItemDataBound Body INICIO "))
            Dim cboTipDocumento As DropDownList = CType(e.Item.FindControl("cboTipDocumento"), DropDownList)
            Dim txtDoc As TextBox = CType(e.Item.FindControl("txtDoc"), TextBox)
            cboTipDocumento.Attributes.Add("onchange", "javascript:ValidaSeleccion('" + cboTipDocumento.ClientID + "','" + txtDoc.ClientID + "');")
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim dtVias As DataTable
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " Obtener_ConsultaViasPago INICIO "))
            Dim dsFormaPago As DataSet = objOffline.Obtener_ConsultaViasPago(Session("ALMACEN"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " Obtener_ConsultaViasPago FIN "))
            dtVias = New DataTable
            dtVias = VerificarVias(dsFormaPago)
            cboLoad(dtVias, cboTipDocumento)
            cboTipDocumento.SelectedValue = e.Item.Cells(2).Text
            txtDoc.Text = e.Item.Cells(4).Text.Replace("&nbsp;", "")
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " dgrPagosE_ItemDataBound Body FIN "))
        End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", " dgrPagosE_ItemDataBound Error: ", ex.Message))
        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " dgrPagosE_ItemDataBound FIN "))
    End Sub

    Private Function VerificarVias(ByVal ds As DataSet) As DataTable
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecFijoPaginas")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecFijoPaginas")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = Me.txtNumeroDeudaSAP.Value
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " VerificarVias INICIO "))
        Dim dtVias As DataTable = New DataTable("ViasPago")
        Dim dtViasFiltro As New DataTable("ViasPagoF") 'CCC

        Try
            dtVias = ds.Tables(0).Clone
            For Each sRow As DataRow In ds.Tables(0).Rows
                dtVias.ImportRow(sRow)
            Next

            'INI CCC - SD_631020
            Dim strViasNoPermitas As String = FormatCadenaConComillas(ConfigurationSettings.AppSettings("constViasSapRestringida"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "strViasNoPermitas", strViasNoPermitas))
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
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} --{3}:", strIdentifyLog, "LOG_INICIATIVA_529", " VerificarVias Error: ", ex.Message))
            Response.Write("<script language=jscript> alert('" + ex.Message + "');</script>")
            Response.End()
        End Try
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " VerificarVias FIN "))
    End Function

    Private Sub cboLoad(ByVal dsFormaPago As DataTable, ByRef cboCampo As DropDownList)
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecFijoPaginas")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecFijoPaginas")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = Me.txtNumeroDeudaSAP.Value
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " cboLoad INICIO "))
        cboCampo.DataSource = dsFormaPago
        cboCampo.DataTextField = "VTEXT"
        cboCampo.DataValueField = "CCINS"
        cboCampo.DataBind()
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_529", " cboLoad FIN "))
    End Sub

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

    Private Sub cmdGrabar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGrabar.ServerClick
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecFijoPaginas")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecFijoPaginas")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim strNroTransaccion As String
        Dim strRowID As String
        Dim strViaPago As String
        Dim strDescViaPago As String
        Dim strNroCheque As String
        Dim strCodigoRpta As String
        Dim strMensajeRpta As String

        Dim strIdentifyLog As String = Me.txtNumeroDeudaSAP.Value

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdentifyLog, "LOG_INICIATIVA_318", "============================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdentifyLog, "LOG_INICIATIVA_318", "======= INICIO cmdGrabar_ServerClick()  ========"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdentifyLog, "LOG_INICIATIVA_318", "============================================="))

        Dim objGestionaRecaudacion As New COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.BWGestionaRecaudacion
        Dim objGestionaRecaudacionesResponse As New COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.GestionaRecaudacionesResponse
        Dim objGestionaRecaudacionRequest As New GestionaRecaudacionRequest
        Dim objBEAuditoriaREST As New COM_SIC_Procesa_Pagos.DataPowerRest.AuditoriaEWS
        Dim objHeaderRequest As New COM_SIC_Procesa_Pagos.DataPowerRest.HeaderRequest

        Try

            Dim i As Integer
            For i = 0 To dgrPagosE.Items.Count - 1
                Dim ddl As New DropDownList
                ddl = dgrPagosE.Items(i).FindControl("cboTipDocumento")
                Dim txt As New TextBox
                txt = dgrPagosE.Items(i).FindControl("txtDoc")

                'strNroTransaccion = Request.QueryString("num")
                'strRowID = dgrPagosE.Items(i).Cells(6).Text()
                'strViaPago = ddl.SelectedValue
                'strDescViaPago = ddl.SelectedItem.Text
                'strNroCheque = txt.Text
                'objOffline.SetActTipoPago(strNroTransaccion, strRowID, strViaPago, strDescViaPago, strNroCheque)

                objHeaderRequest.consumer = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
                objHeaderRequest.country = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_country"))
                objHeaderRequest.dispositivo = ""
                objHeaderRequest.language = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_language"))
                objHeaderRequest.modulo = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
                objHeaderRequest.msgType = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_msgType"))
                objHeaderRequest.operation = "actualizarTipoPago"
                objHeaderRequest.pid = CheckStr(ConfigurationSettings.AppSettings("pid"))
                objHeaderRequest.system = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
                objHeaderRequest.timestamp = Convert.ToDateTime(String.Format("{0:u}", DateTime.UtcNow))
                objHeaderRequest.userId = ""
                objHeaderRequest.wsIp = Funciones.CheckStr(ConfigurationSettings.AppSettings("DAT_GestionaRecaudacion_wsIp"))



                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "consumer", objHeaderRequest.consumer))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "country", objHeaderRequest.country))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "dispositivo", objHeaderRequest.dispositivo))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "language", objHeaderRequest.language))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "modulo", objHeaderRequest.modulo))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "msgType", objHeaderRequest.msgType))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "operation", objHeaderRequest.operation))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "pid", objHeaderRequest.pid))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "system", objHeaderRequest.system))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "timestamp", objHeaderRequest.timestamp))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "userId", objHeaderRequest.userId))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "wsIp", objHeaderRequest.wsIp))


                objBEAuditoriaREST.IDTRANSACCION = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
                objBEAuditoriaREST.IPAPLICACION = Request.ServerVariables("LOCAL_ADDR")
                objBEAuditoriaREST.idTransaccionNegocio = CurrentTerminal
                objBEAuditoriaREST.USRAPP = CurrentUser
                objBEAuditoriaREST.applicationCodeWS = ConfigurationSettings.AppSettings("strAplicacionSISACT")
                objBEAuditoriaREST.APLICACION = ConfigurationSettings.AppSettings("constAplicacion")
                objBEAuditoriaREST.userId = CurrentUser
                objBEAuditoriaREST.nameRegEdit = ""


                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "IDTRANSACCION", objBEAuditoriaREST.IDTRANSACCION))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "IPAPLICACION", objBEAuditoriaREST.IPAPLICACION))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "idTransaccionNegocio", objBEAuditoriaREST.idTransaccionNegocio))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "USRAPP", objBEAuditoriaREST.USRAPP))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "applicationCodeWS", objBEAuditoriaREST.applicationCodeWS))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "APLICACION", objBEAuditoriaREST.APLICACION))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "userId", objBEAuditoriaREST.userId))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "nameRegEdit", objBEAuditoriaREST.nameRegEdit))


                objGestionaRecaudacionRequest.MessageRequest.Body.nroTransaccion = Request.QueryString("num")
                objGestionaRecaudacionRequest.MessageRequest.Body.rowId = dgrPagosE.Items(i).Cells(6).Text()
                objGestionaRecaudacionRequest.MessageRequest.Body.viaPago = ddl.SelectedValue
                objGestionaRecaudacionRequest.MessageRequest.Body.descViaPago = ddl.SelectedItem.Text.Trim()

                Dim viasPagoNoTarjeta() As String = {"ZBUY", "ZCVB", "ZEFE", "ZCHQ", "ZNCR", "ZEAM", "ZCIB"}

                Dim Items As String
                Dim esTarjeta As Boolean = True
                For Each Items In viasPagoNoTarjeta
                    If ddl.SelectedValue = Items Then
                        esTarjeta = False
                    End If
                Next

                If esTarjeta And txt.Text.Length >= 16 Then
                    objGestionaRecaudacionRequest.MessageRequest.Body.nroCheque = txt.Text.Replace(txt.Text.Substring(6, 6), "******")
                Else
                    objGestionaRecaudacionRequest.MessageRequest.Body.nroCheque = txt.Text
                End If



                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} ", strIdentifyLog, "-----------------------------------------------------"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_318", " INPUT SERVICIO ActualizarTipoPagos "))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "nroTransaccion", Request.QueryString("num")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "rowId", dgrPagosE.Items(i).Cells(6).Text()))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "viaPago", ddl.SelectedValue))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "descViaPago", ddl.SelectedItem.Text))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "nroCheque", txt.Text))


                objGestionaRecaudacionRequest.MessageRequest.Header.HeaderRequest = objHeaderRequest

                objGestionaRecaudacionesResponse = objGestionaRecaudacion.ActualizarTipoPagos(objGestionaRecaudacionRequest, objBEAuditoriaREST)

                strCodigoRpta = objGestionaRecaudacionesResponse.MessageResponse.Body.codigoRespuesta
                strMensajeRpta = objGestionaRecaudacionesResponse.MessageResponse.Body.mensajeRespuesta

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdentifyLog, "LOG_INICIATIVA_318", " OUTPUT SERVICIO ActualizarTipoPagos "))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "strCodigoRpta", strCodigoRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "strMensajeRpta", strMensajeRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} ", strIdentifyLog, "-----------------------------------------------------"))
            Next
            'Response.Redirect(Me.PAGINA_INICIAL & "?fecbus=" + Me.hdnFechaBusqueda.Value.Trim())
            Response.Redirect("conDocumentos.aspx?act=2&num=" + Request.QueryString("num") + "&fec=" + hdnFechaBusqueda.Value + "&est=" + Request.QueryString("est"))




        Catch ex As Exception
            strCodigoRpta = "-1"
            strMensajeRpta = "Error en el Servicio"
            ex.Message.ToString()
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "ERROR - ActualizarTipoPagos() Mensaje ", ex.Message.ToString()))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdentifyLog, "LOG_INICIATIVA_318", "ERROR - Servicio ", "objGestionaRecaudacion.ActualizarTipoPagos"))
        End Try
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdentifyLog, "LOG_INICIATIVA_318", "============================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdentifyLog, "LOG_INICIATIVA_318", "======= FIN ActualizarTipoPagos()  ============"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdentifyLog, "LOG_INICIATIVA_318", "============================================="))
    End Sub
    'INICIATIVA - 529 FIN
End Class
