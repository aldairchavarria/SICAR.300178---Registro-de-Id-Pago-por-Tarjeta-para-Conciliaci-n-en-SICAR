Public Class reniec
    Inherits System.Web.UI.Page

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
    Protected WithEvents cboCriterio As System.Web.UI.WebControls.DropDownList
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
            cboCriterio.Attributes.Add("onChange", "f_MuestraCriterio()")

            Dim intAccion
            Dim intTXReniec

            If Page.IsPostBack Then '(Request.ServerVariables("REQUEST_METHOD") = "POST") Then
                intTXReniec = CInt(Request("hidTXReniec"))
                intAccion = CInt(Request("hidAccion"))
                If intAccion = K_BUSCAR Then
                    If CInt(Request("cboCriterio")) = K_TX_NOMBRE Then Response.Redirect("reniec_aproximacion.aspx?strApePaterno=" & Request("txtApePaterno") & "&strApeMaterno=" & Request("txtApeMaterno") & "&strNombre=" & Request("txtNombre"))
                End If
            Else

                cboCriterio.Items.Add(New ListItem("Consulta por DNI", K_TX_DNI))
                cboCriterio.Items.Add(New ListItem("Consulta por Nombre", K_TX_NOMBRE))

                If (Request("intTXReniec") = "") Then
                    intTXReniec = K_TX_DNI
                Else
                    intTXReniec = CInt(Request("intTXReniec"))
                End If
            End If
        End If
    End Sub

End Class
