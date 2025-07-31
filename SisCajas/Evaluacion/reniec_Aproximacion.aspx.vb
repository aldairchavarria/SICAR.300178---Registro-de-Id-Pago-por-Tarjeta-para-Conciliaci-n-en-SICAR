Public Class reniec_Aproximacion
    Inherits System.Web.UI.Page

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

    Public K_REGRESAR = 1
    Public K_AGREGAR = 2
    Public K_MODIFICAR = 3
    Public K_ELIMINAR = 4
    Public K_BUSCAR = 5
    Public K_GRABAR = 6

    Public K_PAGINAR = 10
    Public K_PAGINA_INICIO = 11
    Public K_PAGINA_ANTERIOR = 12
    Public K_PAGINA_SIGUIENTE = 13
    Public K_PAGINA_FINAL = 14

    Public K_EXP_EXCEL = 1
    Public K_EXP_WORD = 2

    Public K_OFICINA_VENTA = "O"
    Public K_VENDEDOR = "V"

    Public K_OPE_CODIGO_RENIEC = 2
    Public K_TX_DNI = 5001
    Public K_TX_NOMBRE = 2001


    Public K_TIPO_DOC_DNI = "1"
    Public K_MAX_REGISTRO_RENIEC = 28


    Public K_MSG_BASEDATO_ERROR = "Error consultando información en la base de datos. Comuniquese con el administrador de creditos"
    Public K_MSG_CUOTA_NO_DISPONIBLE = "No tiene cuota disponible para realizar consultas. Comuniquese con el administrador de creditos"
    Public K_MSG_CUOTA_NO_ASIGNADA = "No tiene cuota diaria asignada. Comuniquese con el administrador de creditos"

    Public K_BASEDATO_ERROR = 1
    Public K_CUOTA_NO_DISPONIBLE = 2
    Public K_CUOTA_NO_ASIGNADA = 3

    Public intNroPagina As Integer
    Public intNroMaxPagina As Integer
    Public intNroMaxBloquePagina As Integer
    Public intNroBloquePagina As Integer
    Public strApePaterno As String
    Public strApeMaterno As String
    Public strNombre As String

    Public intNroReg As Integer
    Public objValores As Collection


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
            Dim objRegistro
            Dim intIndice
            Dim intAccion

            intNroMaxBloquePagina = 0
            intNroBloquePagina = 1

            intNroMaxPagina = 0
            intNroPagina = 1
            intNroReg = 0

            objValores = Nothing

            If Page.IsPostBack Then 'If (Request.ServerVariables("REQUEST_METHOD") = "POST") Then
                intAccion = CInt(Request("hidAccion"))
                If (intAccion = K_PAGINAR) Then
                    strApePaterno = Trim(Request("hidApePaterno"))
                    strApeMaterno = Trim(Request("hidApeMaterno"))
                    strNombre = Trim(Request("hidNombre"))
                    intNroMaxPagina = CInt(Request("hidNroMaxPagina"))
                    intNroPagina = CInt(Request("hidNroPagina"))
                    intNroMaxBloquePagina = CInt(Request("hidNroMaxBloquePagina"))
                    intNroBloquePagina = CInt(Request("hidNroBloquePagina"))
                    Call SR_Aproximacion()
                End If
                If (intAccion = K_REGRESAR) Then Call SR_Regresar()
            Else
                strApePaterno = Trim(Request("strApePaterno"))
                strApeMaterno = Trim(Request("strApeMaterno"))
                strNombre = Trim(Request("strNombre"))
                Call SR_Aproximacion()
            End If
        End If
    End Sub

    Private Sub SR_Aproximacion()
        Dim lintOffSet
        Dim larrDetalle(3, 3)
        Dim lintCodigoError
        Dim lstrMensajeError

        If Not FP_CuotaDisponible(K_OPE_CODIGO_RENIEC, lintCodigoError, lstrMensajeError) Then
            Session("STRMessage") = lstrMensajeError
            Call SR_Regresar()
        End If

        Session("STRMessage") = ""
        lintOffSet = ((intNroPagina - 1) * K_MAX_REGISTRO_RENIEC)
        'lintOffSet =  1

        'Response.Write(strApePaterno)
        'Response.Write(strApeMaterno)
        'Response.Write(strNombre)
        'Response.Write(K_MAX_REGISTRO_RENIEC)
        'Response.Write(lintOffSet)
        'Response.End()

        objValores = FP_Aproximacion(strApePaterno, strApeMaterno, strNombre, K_MAX_REGISTRO_RENIEC, lintOffSet)

        If Not objValores Is Nothing Then
            If objValores("CodigoError") <> 0 Then
                Session("STRMessage") = "Código Error: " & objValores("CodigoError") & "\n" & objValores("MensajeError")
            Else
                If (objValores("RegEnviados") > 0 And objValores("RegTotal") > 0) Then
                    intNroReg = objValores("RegEnviados")
                    If intNroMaxPagina = 0 Then
                        intNroMaxPagina = (objValores("RegTotal") \ K_MAX_REGISTRO_RENIEC)
                        If ((objValores("RegTotal") Mod K_MAX_REGISTRO_RENIEC) > 0) Then intNroMaxPagina = intNroMaxPagina + 1
                    End If
                    If intNroMaxBloquePagina = 0 Then
                        intNroMaxBloquePagina = (intNroMaxPagina \ 15)
                        If ((intNroMaxPagina Mod 15) > 0) Then intNroMaxBloquePagina = intNroMaxBloquePagina + 1
                    End If
                    Call SP_LogTransaccion(K_OPE_CODIGO_RENIEC, _
                         0, _
                         "", _
                         strApePaterno, _
                         strApeMaterno, _
                         strNombre, _
                         K_TX_NOMBRE, _
                         0, 0, 0, 0)
                Else
                    Session("STRMessage") = "Error, datos no existen en RENIEC"
                End If
            End If
        Else
            Session("STRMessage") = "Error, datos no existen en RENIEC"
        End If


        If Session("STRMessage") <> "" Then
            Call SR_Regresar()
        End If
    End Sub

    Private Sub SR_Regresar()
        Response.Write("<script language=javascript>alert('" & Session("STRMessage") & "');window.location='reniec.aspx?intTXReniec=" & K_TX_NOMBRE & "' </script>")
        'Response.Redirect("reniec.aspx?intTXReniec=" & K_TX_NOMBRE)
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

        'lobjTope = Server.CreateObject("COM_PVU_Tope_Bus.clsTopeOfVenta")
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

    Public Function FP_Aproximacion(ByVal p_strApePaterno, ByVal p_strApeMaterno, ByVal p_strNombre, ByVal p_intRegSol, ByVal p_intOffSet)
        Dim lobjValores
        Dim lobjSTReniec As New COM_SIC_Reniec.clsReniec
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

        lobjValores = Nothing
        lstrRutaLog = Request.ServerVariables("APPL_PHYSICAL_PATH") & "Evaluacion\Log"

        lstrCodSession = FP_getSessionReniec()
        'lobjSTReniec = CreateObjectReniec(lstrId_OfVenta)
        'response.write(lintId_Area)
        'response.write(",")
        'response.write(lstrId_OfVenta)
        'response.write(",")		
        'response.write(lstrId_Canal)
        'response.write(",")
        'response.write(lstrId_CodVendedor_sap)
        'response.write(",")
        'response.write(lstrCodSession)
        'response.write(",")
        'response.write(lstrRutaLog)
        'response.write(",")
        'response.end

        'Response.Write(" p_strApePaterno " & p_strApePaterno & "<br>")
        'Response.Write(" p_strApeMaterno " & p_strApeMaterno & "<br>")
        'Response.Write(" p_strNombre " & p_strNombre & "<br>")
        'Response.Write(" p_intRegSol " & p_intRegSol & "<br>")
        'Response.Write(" p_intOffSet " & p_intOffSet & "<br>")
        'Response.Write(" lintId_Area " & lintId_Area & "<br>")
        'Response.Write(" lstrId_OfVenta " & lstrId_OfVenta & "<br>")
        'Response.Write(" lstrId_Canal " & lstrId_Canal & "<br>")
        'Response.Write(" lstrId_CodVendedor_sap " & lstrId_CodVendedor_sap & "<br>")
        'Response.Write(" lstrCodSession " & lstrCodSession & "<br>")
        'Response.Write(" lstrRutaLog " & lstrRutaLog & "<br>")
        'Response.End()

        lobjValores = lobjSTReniec.FP_ExtraerAproximacion(p_strApePaterno, p_strApeMaterno, p_strNombre, p_intRegSol, p_intOffSet, lintId_Area, lstrId_OfVenta, lstrId_Canal, "jtoma", "jtoma", lstrId_CodVendedor_sap, lstrCodSession, lstrRutaLog, "D")

        If Not lobjValores Is Nothing Then
            If lobjValores("CodigoError") = 0 Then
                lstrCodSession = lobjSTReniec.FP_ExtraerSession()
                Call SP_setSessionReniec(lstrCodSession)
            End If
        End If

        lobjSTReniec = Nothing
        FP_Aproximacion = lobjValores
    End Function

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
        Dim lrc As Integer
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

        'lobjTope = Server.CreateObject("COM_PVU_Tope_Bus.clsLogTransaccion")
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

End Class
