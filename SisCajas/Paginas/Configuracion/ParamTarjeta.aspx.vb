Imports System.Data.OracleClient

Public Class ParamTarjeta
    Inherits System.Web.UI.Page
    Dim i, intResultado As Integer
    Protected WithEvents txtCajaDolar As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtTolDolar As System.Web.UI.HtmlControls.HtmlInputText
    Dim objConfig As New COM_SIC_Configura.clsConfigura
#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtMontoSoles As System.Web.UI.HtmlControls.HtmlInputText
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim dsBancos As New DataSet
            Dim dsTipMoneda As New DataSet
            Dim dsDatGenerales As New DataSet

            If Not Page.IsPostBack Then

                dsDatGenerales = objConfig.FP_Lista_Param_Tarjeta(1)

                txtMontoSoles.Value = dsDatGenerales.Tables(0).Rows(0).Item("PARTN_MONTO")

        End If
        End If
    End Sub

    Private Sub cmdGrabar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGrabar.ServerClick

        Dim intValCamFec As Integer
        Dim intValImpSap As Integer

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
        Dim Detalle(4, 3) As String

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        ' fin de variables de auditoria


        If Trim(txtMontoSoles.Value) = "" Then
            Response.Write("<script>alert('Ingrese un monto en soles!')</script>")
            Exit Sub
        End If

        'AUDITORIA
        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcMPDV")
        wParam5 = 1
        wParam6 = "Parámetros de Oficina"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtMPDV")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        Detalle(1, 1) = "Canal"
        Detalle(1, 2) = Session("CANAL")
        Detalle(1, 3) = "Canal"

        Detalle(2, 1) = "OfVta"
        Detalle(2, 2) = Session("ALMACEN")
        Detalle(2, 3) = "Oficina de Venta"

        Detalle(3, 1) = "CodApl"
        Detalle(3, 2) = ConfigurationSettings.AppSettings("codAplicacion")
        Detalle(3, 3) = "Codigo de Aplicacion"

        Detalle(4, 1) = "MonSol"
        Detalle(4, 2) = txtMontoSoles.Value
        Detalle(4, 3) = "Monto en Soles"

        'FIN DE AUDITORIA


        intResultado = objConfig.FP_Actualiza_Param_Tarjeta(txtMontoSoles.Value, 1)

        If CStr(intResultado) <> "1" Then
            Response.Write("<script>alert('Ocurrió un error al momento de actualizar el monto!')</script>")
        Else
            Response.Write("<script>alert('El monto se actualizó correctamente!')</script>")
        End If

        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)

    End Sub
End Class
