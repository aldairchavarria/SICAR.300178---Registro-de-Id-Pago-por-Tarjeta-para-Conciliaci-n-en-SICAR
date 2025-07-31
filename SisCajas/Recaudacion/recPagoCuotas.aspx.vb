Public Class recPagoCuotas
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rfvCodCliente As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents cmdBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents txtTrama As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroTelef As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTelef As System.Web.UI.WebControls.TextBox

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
            txtNroTelef.Attributes.Add("onkeypress", "javascript:ValidaNumero(this)")
            If Session("strMensajeDAC") <> "" Then
                Response.Write("<script language=javascript>alert('" & Session("strMensajeDAC") & "')</script>")
                Session("strMensajeDAC") = ""
            End If

            txtTrama.Text = Request.Item("strTrama")
            txtMonto.Text = Request.Item("strMonto")
            txtDealer.Text = Request.Item("Dealer")
            txtTelef.Text = Request.Item("strTelefono")
        End If
    End Sub

    Private Sub cmdBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Session("ClienteDAC") = txtNroTelef.Text
        Response.Redirect("detPagoCuotas.aspx")
    End Sub
End Class
