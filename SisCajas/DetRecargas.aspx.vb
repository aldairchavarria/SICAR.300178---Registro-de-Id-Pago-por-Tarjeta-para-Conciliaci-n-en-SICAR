Public Class DetRecargas
    Inherits System.Web.UI.Page

#Region " C�digo generado por el Dise�ador de Web Forms "

    'El Dise�ador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox

    'NOTA: el Dise�ador de Web Forms necesita la siguiente declaraci�n del marcador de posici�n.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Dise�ador de Web Forms requiere esta llamada de m�todo
        'No la modifique con el editor de c�digo.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aqu� el c�digo de usuario para inicializar la p�gina
        'Response.Write("<script language='javascript' src='/librerias/Lib_FuncValidacion.js'></script>")
        'Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        'If (Session("USUARIO") Is Nothing) Then
        '    Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
        '    Response.Redirect(strRutaSite)
        '    Response.End()
        '    Exit Sub
        'End If
    End Sub

End Class
