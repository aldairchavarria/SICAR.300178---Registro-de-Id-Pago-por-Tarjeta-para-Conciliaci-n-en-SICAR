Public Class CorrecRemesaBolsa
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtBolsa As System.Web.UI.WebControls.TextBox
    Protected WithEvents hldVerif As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnVerConten As System.Web.UI.WebControls.Button

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
            btnVerConten.Attributes.Add("OnClick", "f_Valida()")
            If Len(Trim(Session("MENSAJE"))) > 0 Then
                Response.Write("<script>alert('" & Session("MENSAJE") & "')</script>")
                Session("MENSAJE") = ""
            End If
        End If
    End Sub

    Private Sub btnVerConten_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerConten.Click
        Session("BOLREM") = txtBolsa.Text
        Response.Redirect("CorrecRemesaCont.aspx")
    End Sub
End Class
