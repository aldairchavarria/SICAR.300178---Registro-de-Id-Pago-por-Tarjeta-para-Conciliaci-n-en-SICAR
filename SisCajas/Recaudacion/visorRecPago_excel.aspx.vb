Public Class visorRecPago_excel
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents dgReporte As System.Web.UI.WebControls.DataGrid

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
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim dtDatos As DataTable
            Try
                If Not Session("TablaPagosOrdenado") Is Nothing Then
                    dgReporte.DataSource = CType(Session("TablaPagosOrdenado"), DataView)
                Else
                    dtDatos = CType(Session("TablaPagos"), DataTable)
                    dgReporte.DataSource = dtDatos
                    dtDatos.Dispose()
                End If
                dgReporte.DataBind()
            Catch ex As Exception
                Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            End Try
        End If
    End Sub

    Private Sub dgReporte_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgReporte.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            e.Item.Cells(2).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(3).Style("mso-number-format") = "\@"
        End If
    End Sub
End Class
