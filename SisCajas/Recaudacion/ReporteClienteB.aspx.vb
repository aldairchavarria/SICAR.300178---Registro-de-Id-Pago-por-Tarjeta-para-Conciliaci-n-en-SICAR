Public Class ReporteClienteB
    Inherits System.Web.UI.Page

#Region " C�digo generado por el Dise�ador de Web Forms "

    'El Dise�ador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmdBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents txtNumero As System.Web.UI.HtmlControls.HtmlInputText

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
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            cmdBuscar.Attributes.Add("onClick", "f_Valida()")
        End If
    End Sub

    Private Sub cmdBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        If Trim(txtNumero.Value) = "" Then
            Response.Write("<script>alert('No ha ingresado un n�mero de RUC correcto')</script>")
        Else
            Response.Redirect("ListadoClienteB.aspx?strRUC=" + txtNumero.Value)
        End If
    End Sub
End Class
