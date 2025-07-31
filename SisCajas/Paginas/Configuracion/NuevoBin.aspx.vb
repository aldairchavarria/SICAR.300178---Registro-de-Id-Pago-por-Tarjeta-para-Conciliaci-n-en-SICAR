Imports System.Data.OracleClient
Public Class NuevoBin
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitActualizaBIN As System.Web.UI.WebControls.Label
    Protected WithEvents lblCodigoBin As System.Web.UI.WebControls.Label
    Protected WithEvents lblDesBIN As System.Web.UI.WebControls.Label
    Protected WithEvents imgGrabar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents imgCancelar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNumero As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtDescripcion As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents selEstado As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents cmdGrabar As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region
    Dim IdBin As Integer
    Dim objConfig As New COM_SIC_Configura.clsConfigura

    Private Sub cmdGrabar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGrabar.ServerClick
        If Trim(txtNumero.Value) = "" Then
            Response.Write("<script>alert('Ingrese el BIN');</script>")
            Exit Sub
        End If
        If Trim(txtDescripcion.Value) = "" Then
            Response.Write("<script>alert('Ingrese una descripción');</script>")
            Exit Sub
        End If

        Dim intResultado As Integer

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
        Dim Detalle(6, 3) As String

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        ' fin de variables de auditoria

        'AUDITORIA
        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcMBIN")
        wParam5 = 1
        wParam6 = "Mantenimiento de BINs. Nuevo"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtMnBn")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        Detalle(1, 1) = "Canal"
        Detalle(1, 2) = Session("CANAL")
        Detalle(1, 3) = "Canal"

        Detalle(2, 1) = "OfVta"
        Detalle(2, 2) = Session("ALMACEN")
        Detalle(2, 3) = "Oficina de venta"

        Detalle(3, 1) = "CodApl"
        Detalle(3, 2) = ConfigurationSettings.AppSettings("codAplicacion")
        Detalle(3, 3) = "Codigo de Aplicacion"

        Detalle(4, 1) = "Numero"
        Detalle(4, 2) = CStr(txtNumero.Value)
        Detalle(4, 3) = "Numero"

        Detalle(5, 1) = "Descripcion"
        Detalle(5, 2) = CStr(txtDescripcion.Value)
        Detalle(5, 3) = "Descripcion"

        Detalle(6, 1) = "Estado"
        Detalle(6, 2) = CStr(selEstado.Value)
        Detalle(6, 3) = "Estado"

        'FIN DE AUDITORIA



        'Dim orConector As New OracleConnection
        'Dim orComando As New OracleCommand

        'orConector.ConnectionString = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        'orConector.Open()
        'orComando.Connection = orConector
        'orComando.Parameters.Add("K_COD_CANAL", OracleType.VarChar, 5)
        'orComando.Parameters.Add("K_COD_PDV", OracleType.VarChar, 5)
        'orComando.Parameters.Add("K_APLIC_COD", OracleType.Number)
        'orComando.Parameters.Add("K_CODBIN", OracleType.Number)
        'orComando.Parameters.Add("K_DESCRIPCION", OracleType.VarChar, 100)
        'orComando.Parameters.Add("K_ESTADO", OracleType.Number)
        'orComando.Parameters.Add("K_RETVAL", OracleType.Number)

        'Dim i As Integer
        'For i = 0 To 5
        '    orComando.Parameters(i).Direction = ParameterDirection.Input
        'Next
        'orComando.Parameters(6).Direction = ParameterDirection.Output

        'orComando.Parameters(0).Value = Session("CANAL")
        'orComando.Parameters(1).Value = Session("ALMACEN")
        'orComando.Parameters(2).Value = ConfigurationSettings.AppSettings("codAplicacion")
        'orComando.Parameters(3).Value = CStr(txtNumero.Value)
        'orComando.Parameters(4).Value = CStr(txtDescripcion.Value)
        'orComando.Parameters(5).Value = CStr(selEstado.Value)

        'orComando.CommandText = "CONF_PARAMETROS_CAJA.CONF_INGRESA_BIN"
        'orComando.CommandType = CommandType.StoredProcedure

        'intResultado = orComando.ExecuteNonQuery
        intResultado = objConfig.FP_Ingresa_BIN(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), CStr(txtNumero.Value), CStr(txtDescripcion.Value), CStr(selEstado.Value))

        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)

        Response.Write("<script>window.opener.f_Refrescar();</script>")
        Response.Write("<script>window.close();</script>")

    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        End If
    End Sub
End Class
