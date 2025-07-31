Public Class visorRemesa_excel
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
                If Not Session("TablaRemesaOrdenado") Is Nothing Then
                    dgReporte.DataSource = CType(Session("TablaRemesaOrdenado"), DataView)
                Else
                    dtDatos = CType(Session("ListaConsultarRemesa"), DataTable) 'INI-936 - CNSO
                    dgReporte.DataSource = dtDatos
                    dtDatos.Dispose()
                End If
                dgReporte.DataBind()
            Catch ex As Exception
                Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            End Try
        End If

        'Inicio - INI-936 - CNSO - Variables para generar Excel
        Dim strFechaArchivo As String = String.Format("{0}{1}{2}", Format(Now.Day, "00"), Format(Now.Month, "00"), Format(Now.Year, "0000"))
        Dim strHoraArchivo As String = String.Format("{0}{1}{2}", Format(TimeOfDay().Hour, "00"), Format(TimeOfDay().Minute, "00"), Format(TimeOfDay().Second, "00"))
        Dim strNombreArchivo As String = String.Format("RepDetalleRemesa_{0}{1}", strFechaArchivo, strHoraArchivo)

        Response.AddHeader("Content-Disposition", "attachment;filename=" + strNombreArchivo + ".xls")
        Response.ContentType = "application/vnd.ms-excel"
        'Fin - INI-936 - CNSO
    End Sub

    Private Sub dgReporte_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgReporte.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            e.Item.Cells(9).Style("mso-number-format") = "\@"
        End If
    End Sub
End Class
