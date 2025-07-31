Public Class CorrecRemesaCont
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtBolsa As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgSobres As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnEliminar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim objCajas As New COM_SIC_Cajas.clsCajas
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
            Dim dsRemesa As DataSet
            Dim strBolsa As String = Session("BOLREM")

            If Not Page.IsPostBack Then
                txtBolsa.Text = strBolsa
                dsRemesa = objCajas.FP_DetalleRemesa(strBolsa)
                dgSobres.DataSource = dsRemesa.Tables(0)
                dgSobres.DataBind()
                If Not dsRemesa Is Nothing Then
                    If dsRemesa.Tables(0).Rows.Count = 0 Then
                        btnEliminar.Enabled = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

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
        Dim Detalle(2, 3) As String

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        ' fin de variables de auditoria

        'AUDITORIA
        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcAcRm")
        wParam5 = 1
        wParam6 = "Eliminacion de Remesa"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtAcRm")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        Detalle(1, 1) = "OfVta"
        Detalle(1, 2) = Session("ALMACEN")
        Detalle(1, 3) = "Oficina de Venta"

        Detalle(2, 1) = "Bolsa"
        Detalle(2, 2) = Session("BOLREM")
        Detalle(2, 3) = "Bolsa"

        'FIN DE AUDITORIA

        Try
            Dim strMsgError As String

            objCajas.FP_EliminaRemesa(Session("ALMACEN"), Session("BOLREM"))
        Catch ex As Exception
        End Try
        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
        Session("MENSAJE") = "La remesa se eliminó con éxito"
        Response.Redirect("CorrecRemesaBolsa.aspx")

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("CorrecRemesaBolsa.aspx")
    End Sub
End Class
