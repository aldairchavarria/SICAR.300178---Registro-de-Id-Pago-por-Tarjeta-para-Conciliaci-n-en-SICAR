Imports SisCajas.clsContantes_site

Public Class BusConfTran
    Inherits System.Web.UI.Page
    Protected WithEvents txtFecha As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdBuscar As System.Web.UI.WebControls.Button
    Public strSiteOcultarCapas As String = ""

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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
            If Not Page.IsPostBack Then
                If Session("FechaTran") = "" Then
                    txtFecha.Value = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000") 'Format(Now, "d")
                Else
                    txtFecha.Value = Session("FechaTran")
                    Session("FechaTran") = ""
                End If
            End If
        End If
    End Sub

    Private Sub cmdBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        If Trim(txtFecha.Value) = "" Then
            Response.Write("<script>alert('No ha ingresado una fecha correcta')</script>")
        Else
            Session("FechaTran") = Trim(txtFecha.Value)
            Response.Redirect("AutTransaccion.aspx?strFecha=" + txtFecha.Value)
        End If
    End Sub
End Class
