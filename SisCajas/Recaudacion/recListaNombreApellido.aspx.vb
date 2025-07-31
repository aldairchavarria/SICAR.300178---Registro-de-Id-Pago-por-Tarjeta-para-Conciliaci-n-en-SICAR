Public Class recListaNombreApellido
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DGReporte As System.Web.UI.WebControls.DataGrid
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
            Dim strNomRaz As String
            Dim strApellido As String
            Dim objListado As New COM_SIC_RecBusiness.clsRecBusiness
            Dim dsListado As DataSet

            If Len(Trim(Session("strMENSREC"))) > 0 Then
                Response.Write("<script language=javascript>alert('" & Session("strMENSREC") & "')</script>")
                Session("strMENSREC") = ""
            End If


            If Not Page.IsPostBack Then
                strNomRaz = Request.Item("txtNomRaz")
                strApellido = Request.Item("txtApellido")

                dsListado = objListado.FP_ConsultaXNombre(strNomRaz, strApellido)

                DGReporte.DataSource = dsListado.Tables(0)
                DGReporte.DataBind()

        End If
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        Response.Redirect("recBusNomAp.aspx")
    End Sub
End Class
