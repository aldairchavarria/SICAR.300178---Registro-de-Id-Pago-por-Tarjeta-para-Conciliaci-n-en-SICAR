Public Class reniec_CargaDatos
    Inherits System.Web.UI.Page

    Const K_BUSCAR = 5
    Const K_TX_DNI = 5001
    Const K_TX_NOMBRE = 2001
    Const K_OPE_CODIGO_RENIEC = 2

    Const K_MSG_BASEDATO_ERROR = "Error consultando información en la base de datos. Comuniquese con el administrador de creditos"
    Const K_MSG_CUOTA_NO_DISPONIBLE = "No tiene cuota disponible para realizar consultas. Comuniquese con el administrador de creditos"
    Const K_MSG_CUOTA_NO_ASIGNADA = "No tiene cuota diaria asignada. Comuniquese con el administrador de creditos"

    Const K_BASEDATO_ERROR = 1
    Const K_CUOTA_NO_DISPONIBLE = 2
    Const K_CUOTA_NO_ASIGNADA = 3

    Const K_TIPO_DOC_DNI = "1"
    Const K_ESTADO_DATA_VIGENTE = 1

    Public strMsgError As String
    Dim intAccion As Integer
    Public blnVerConsolidado As Boolean

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

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

            blnVerConsolidado = False

            If Page.IsPostBack Then 'If (Request.ServerVariables("REQUEST_METHOD") = "POST") Then
                intAccion = CInt(Request("hidAccion"))
                If intAccion = K_BUSCAR Then Call SR_ConsultarDNI()
            End If
        End If
    End Sub

    Function FR_ResolucionBusqueda(ByVal p_Ope_Codigo, ByVal p_Dat_TxnServicio, ByVal p_Dat_TipoDoc, ByVal p_Dat_NumDoc, ByVal p_Dat_Estado) As Object
        Dim lobjLog As New COM_SIC_Tope_Dbo.clsTopeOfVenta

        FR_ResolucionBusqueda = lobjLog.FP_ResolucionBusqueda(p_Ope_Codigo, p_Dat_TxnServicio, p_Dat_TipoDoc, p_Dat_NumDoc, p_Dat_Estado)
        lobjLog = Nothing
    End Function

    Private Sub SR_ConsultarDNI()
        Dim lobjDato As Object
        Dim lstrNroDNI As Object
        Dim lintTXReniec As Object
        Dim lstrEvento As Object

        Dim lintCodigoError As Object
        Dim lstrMensajeError As Object
        Dim lintResolucion As Object

        strMsgError = ""
        Session("objDato") = Nothing
        lobjDato = Nothing
        lstrNroDNI = Request("hidNumDoc")
        lintTXReniec = CInt(Request("hidTXReniec"))

        If Not FP_CuotaDisponible(K_OPE_CODIGO_RENIEC, lintCodigoError, lstrMensajeError) Then
            strMsgError = lstrMensajeError
            Exit Sub
            strMsgError = ""
        End If

        lintResolucion = FR_ResolucionBusqueda(K_OPE_CODIGO_RENIEC, K_TX_DNI, K_TIPO_DOC_DNI, lstrNroDNI, K_ESTADO_DATA_VIGENTE)

        lobjDato = FP_Consolidado(K_TIPO_DOC_DNI, lstrNroDNI)

        If Not lobjDato Is Nothing Then
            If lobjDato("CodigoError") <> 0 Then
                strMsgError = "Código Error: " & lobjDato("CodigoError") & "\n" & lobjDato("MensajeError")
            Else
                blnVerConsolidado = True
                Session("objDato") = lobjDato
                Call SP_LogTransaccion(K_OPE_CODIGO_RENIEC, _
                     K_TIPO_DOC_DNI, _
                     lobjDato("NumDoc"), _
                     lobjDato("ApePaterno"), _
                     lobjDato("ApeMaterno"), _
                     lobjDato("Nombres"), _
                     K_TX_DNI, _
                     lintResolucion, _
                     0, 0, 0)
            End If
        Else
            strMsgError = "DNI no válido, no existe en RENIEC"
        End If




    End Sub

    Public Function FP_CuotaDisponible(ByVal p_intOpe_Codigo, ByRef p_intCodigoError, ByRef p_strMensajeError)
        Dim lobjTope As New COM_SIC_Tope_Dbo.clsTopeOfVenta
        Dim larrTope
        Dim lstrId_Canal
        Dim lstrId_OfVenta
        Dim lstrId_CodVendedor_sap

        lstrId_Canal = Session("CANAL")
        lstrId_OfVenta = Session("ALMACEN")
        lstrId_CodVendedor_sap = Session("USUARIO")

        larrTope = lobjTope.FP_VerificaTope(p_intOpe_Codigo, lstrId_Canal, lstrId_OfVenta, lstrId_CodVendedor_sap)
        lobjTope = Nothing

        If IsArray(larrTope) Then
            If Not IsNothing(larrTope(0)) Then
                If (larrTope(0) > 0) Then
                    p_intCodigoError = 0
                    p_strMensajeError = ""
                Else
                    p_intCodigoError = K_CUOTA_NO_DISPONIBLE
                    p_strMensajeError = K_MSG_CUOTA_NO_DISPONIBLE
                End If
            Else
                p_intCodigoError = K_CUOTA_NO_ASIGNADA
                p_strMensajeError = K_MSG_CUOTA_NO_ASIGNADA
            End If
        Else
            p_intCodigoError = K_BASEDATO_ERROR
            p_strMensajeError = K_MSG_BASEDATO_ERROR
        End If

        FP_CuotaDisponible = (p_intCodigoError = 0)
    End Function

    Public Function FP_Consolidado(ByVal p_strTipDoc, ByVal p_strDNI)
        Dim lobjSTReniec As New COM_SIC_Reniec.clsReniec
        Dim lobjDato
        Dim lstrRutaLog
        Dim lstrCodSession
        Dim lstrId_Canal
        Dim lstrId_OfVenta
        Dim lstrId_CodVendedor_sap
        Dim lintId_Area

        lstrId_Canal = Session("CANAL")
        lstrId_OfVenta = Session("ALMACEN")
        lstrId_CodVendedor_sap = Session("USUARIO")
        lintId_Area = Session("CODIGOAREA")

        lobjDato = Nothing
        lstrRutaLog = Request.ServerVariables("APPL_PHYSICAL_PATH") & "Evaluacion\Log"

        lstrCodSession = FP_getSessionReniec()
        lobjDato = lobjSTReniec.FP_ExtraerConsolidado(p_strTipDoc, p_strDNI, lintId_Area, lstrId_OfVenta, lstrId_Canal, "jtoma", "jtoma", lstrId_CodVendedor_sap, lstrCodSession, lstrRutaLog, "D")

        If Not lobjDato Is Nothing Then
            If lobjDato("CodigoError") = 0 Then
                lstrCodSession = lobjSTReniec.FP_ExtraerSession()
                Call SP_setSessionReniec(lstrCodSession)
            End If
        End If

        lobjSTReniec = Nothing
        FP_Consolidado = lobjDato
    End Function
    Public Sub SP_LogTransaccion(ByVal p_intOpe_Codigo, _
                 ByVal p_strTipoDoc, _
                 ByVal p_strNumDoc, _
                 ByVal p_strApePaterno, _
                 ByVal p_strApeMaterno, _
                 ByVal p_strNombre, _
                 ByVal p_strTxn_Txn, _
                 ByVal p_intResolucion, _
                 ByVal p_intCompCode, _
                 ByVal p_intErrCode, _
                 ByVal p_intEstado)
        Dim lobjTope As New COM_SIC_Tope_Dbo.clsTopeOfVenta
        Dim lrc
        Dim lstrId_Canal
        Dim lstrId_OfVenta
        Dim lstrId_CodVendedor_sap
        Dim lstrLogin_nt
        Dim lintId_Area
        Dim lstrNom_Vendedor

        lstrId_Canal = Session("CANAL")
        lstrId_OfVenta = Session("ALMACEN")
        lstrId_CodVendedor_sap = Session("USUARIO")
        lstrLogin_nt = Session("strUsuario")
        lintId_Area = Session("CODIGOAREA")
        lstrNom_Vendedor = Session("NOMBRE_COMPLETO")


        lrc = lobjTope.FP_Agregar(p_intOpe_Codigo, _
               lstrId_Canal, _
               lstrId_OfVenta, _
               lstrId_CodVendedor_sap, _
               lstrLogin_nt, _
               lintId_Area, _
               lstrNom_Vendedor, _
               p_strTipoDoc, _
               p_strNumDoc, _
               p_strApePaterno, _
               p_strApeMaterno, _
               p_strNombre, _
               p_strTxn_Txn, _
               p_intResolucion, _
               p_intCompCode, _
               p_intErrCode, _
               p_intEstado)
        lobjTope = Nothing
    End Sub

    Public Function FP_getSessionReniec()
        If IsNothing(Application("CodSession")) Then
            Application.Lock()
            Application("CodSession") = ""
            Application.UnLock()
        End If
        FP_getSessionReniec = Application("CodSession")
    End Function

    Public Sub SP_setSessionReniec(ByVal p_strCodSession)
        If (Application("CodSession") <> p_strCodSession) Then
            Application.Lock()
            Application("CodSession") = p_strCodSession
            Application.UnLock()
        End If
    End Sub

End Class
