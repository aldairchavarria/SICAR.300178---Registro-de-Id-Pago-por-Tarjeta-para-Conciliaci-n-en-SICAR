Public Class rep_pagosPorDeliveryExcel
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
            Dim strMensaje As String = "Ha ocurrio un error al exportar los registros en excel. Inténtelo nuevamente."

            Try
                Dim strFecha As String = String.Empty
                Dim strOficina As String = String.Empty

                strFecha = Funciones.CheckStr(Request.QueryString("strFecha"))
                strOficina = Funciones.CheckStr(Session("ALMACEN"))

                If Not Page.IsPostBack Then
                    Llenar_grid(strFecha, strOficina)
                End If

                Dim strFechaArchivo As String = String.Format("{0}{1}{2}", Format(Now.Day, "00"), Format(Now.Month, "00"), Format(Now.Year, "0000"))
                Dim strHoraArchivo As String = String.Format("{0}{1}{2}", TimeOfDay().Hour, TimeOfDay().Minute, TimeOfDay().Second)
                Dim strNombreArchivo As String = String.Format("RepPagosDelivery_{0}{1}", strFechaArchivo, strHoraArchivo)

                Response.AddHeader("Content-Disposition", "attachment;filename=" + strNombreArchivo + ".xls")
                Response.ContentType = "application/vnd.ms-excel"

            Catch ex As Exception
                Response.Write("<script language=jscript> alert('" + strMensaje + "'); </script>")
            End Try
        End If

    End Sub

    Private Sub Llenar_grid(ByVal strFecha As String, ByVal strOficina As String)

        Dim objRecordSet As DataTable
        Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim strCod As String = String.Empty
        Dim msj As String = String.Empty
        Dim i As Integer
        Dim strMensaje As String = "Ha ocurrio un error al exportar los registros en excel. Inténtelo nuevamente."
        Try
            objRecordSet = objConsultaMsSap.ConsultarReporte(strFecha, strOficina, strCod, msj)

            If Not objRecordSet Is Nothing Then
                If objRecordSet.Rows.Count > 0 Then
                    For i = 0 To objRecordSet.Rows.Count - 1
                        If (objRecordSet.Rows(i).Item(26) Is DBNull.Value) Then
                            objRecordSet.Rows(i).Item(26) = Decimal.Parse(0)
                        End If
                    Next
                    dgReporte.DataSource = objRecordSet
                    dgReporte.DataBind()
                Else
                        Response.Write("<script language=jscript> alert('" + strMensaje + "'); </script>")
                End If
            Else
                Response.Write("<script language=jscript> alert('" + strMensaje + "'); </script>")
            End If

        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + strMensaje + "'); </script>")
        End Try
    End Sub

    Private Sub dgReporte_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgReporte.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            e.Item.Cells(0).Style("mso-number-format") = "\@"
            e.Item.Cells(1).Style("mso-number-format") = "\@"
            e.Item.Cells(2).Style("mso-number-format") = "\@"
            e.Item.Cells(2).Style("mso-number-format") = "\@"
            e.Item.Cells(4).Style("mso-number-format") = "\@"
            e.Item.Cells(5).Style("mso-number-format") = "\@"
            e.Item.Cells(6).Style("mso-number-format") = "\@"
            e.Item.Cells(7).Style("mso-number-format") = "\@"
            e.Item.Cells(8).Style("mso-number-format") = "\@"
            e.Item.Cells(9).Style("mso-number-format") = "\#\,\#\#0\.00"
        End If
    End Sub
End Class
