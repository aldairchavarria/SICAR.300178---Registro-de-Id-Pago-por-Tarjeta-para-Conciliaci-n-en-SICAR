Public Class rep_pagosPorDeliveryRecExcel
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
        'Put user code to initialize the page here
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                Llenar_grid() 'INI-936 - JH - Modificado metodo para no enviar parametros
            End If

            Dim strFechaArchivo As String = String.Format("{0}{1}{2}", Format(Now.Day, "00"), Format(Now.Month, "00"), Format(Now.Year, "0000"))
            Dim strHoraArchivo As String = String.Format("{0}{1}{2}", Format(TimeOfDay().Hour, "00"), Format(TimeOfDay().Minute, "00"), Format(TimeOfDay().Second, "00")) 'INI-936 - JCI - Agregado formato para incluir el 0 delante de las horas, minutos y segundos
            Dim strNombreArchivo As String = String.Format("RepPagosRecDelivery_{0}{1}", strFechaArchivo, strHoraArchivo)

            Response.AddHeader("Content-Disposition", "attachment;filename=" + strNombreArchivo + ".xls")
            Response.ContentType = "application/vnd.ms-excel"

        End If

    End Sub

    'INI-936 - JH - Metodo modificado para no recibir parametros de entrada ni consultar BD y obtener los datos de sesion
    Private Sub Llenar_grid()
        Dim objRecordSet As DataTable 'INI-936-JH
        Dim strMensaje As String = "Ha ocurrido un error al exportar los registros en excel. Intentelo nuevamente."
        Try
            objRecordSet = Session("reportePagosDeliveryRec") 'INI-936-JH

            If Not objRecordSet Is Nothing Then
                dgReporte.DataSource = objRecordSet
                dgReporte.DataBind()
            Else
                Response.Write("<script language=jscript> alert('" + strMensaje + "'); </script>")
            End If

        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + strMensaje + "'); </script>")
        End Try
    End Sub

    'INI-936 - JH - Evento ItemDataBound habilitado para dar formatos a las celdas al exportar en excel
    Private Sub dgReporte_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgReporte.ItemDataBound
        Dim i As Integer
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            For i = 0 To e.Item.Cells.Count - 1
                If (i = 10) Then
                    e.Item.Cells(i).Style("mso-number-format") = "\#\,\#\#0\.00"
                Else
                    e.Item.Cells(i).Style("mso-number-format") = "\@"
                End If
            Next
        End If
    End Sub

End Class
