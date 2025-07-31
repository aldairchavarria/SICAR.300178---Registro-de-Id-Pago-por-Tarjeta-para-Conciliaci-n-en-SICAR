Public Class visorRemesa_detalle
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DGLista As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents hdnOrdenacion As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Variables"
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogVisorRemesa")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogVisorRemesa")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String = String.Empty
    Dim strIdentifyLog As String = String.Empty
    Dim objClsAdmCaja As COM_SIC_Adm_Cajas.clsAdmCajas
    Private Const SORT_ASCENDING As String = "ASC"
    Private Const SORT_DESCENDING As String = "DESC"
    Public tblExportar As DataTable 'INI-936 - CNSO
#End Region

#Region "Eventos"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not IsPostBack Then
                If Not Session("ReporteRemesas") Is Nothing Then  'INI-936 - CNSO
                    CargarDatos()
                End If
            End If
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Dim strURL As String
        Session("TablaRemesaOrdenado") = Nothing
        Session("ReporteRemesas") = Nothing 'INI-936 - CNSO
        strURL = "visorRemesa.aspx"
        Response.Redirect(strURL)
    End Sub

    Private Sub DGLista_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DGLista.SortCommand
        Dim sortExpression As String = e.SortExpression
        Dim CurrentSortDirection As String
        CurrentSortDirection = CType(ViewState("SortDirection"), String)

        If CurrentSortDirection = SORT_ASCENDING Then
            ViewState("SortDirection") = SORT_DESCENDING
            hdnOrdenacion.Value = SORT_DESCENDING
            SortGridView(sortExpression, hdnOrdenacion.Value)
        Else
            ViewState("SortDirection") = SORT_ASCENDING
            hdnOrdenacion.Value = SORT_ASCENDING
            SortGridView(sortExpression, hdnOrdenacion.Value)
        End If
    End Sub

#End Region

#Region "Metodos"

    'INI-936 - CNSO - Metodo modificado para leer datos de la sesion y no consultar a BD
    Private Sub CargarDatos()
        Dim dtDatos As DataTable
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Detalle Remesas - INI  CargarDatos")
            dtDatos = Session("ReporteRemesas")

            Dim dtTblRemesa As New DataTable
            With dtTblRemesa
                .Columns.Add("SOBRE", GetType(System.String))
                .Columns.Add("BOLSA", GetType(System.String))
                .Columns.Add("COD_USUARIO_ENVIA_REMESA", GetType(System.String))
                .Columns.Add("USUARIO_ENVIA_REMESA", GetType(System.String))
                .Columns.Add("FECHA_ENVIO_REMESA", GetType(System.DateTime))
                .Columns.Add("HORA_ENVIO_REMESA", GetType(System.DateTime))
                .Columns.Add("BUZON_FECHA", GetType(System.DateTime))
                .Columns.Add("BUZON_HORA", GetType(System.DateTime))
                .Columns.Add("COD_OFICINA", GetType(System.String))
                .Columns.Add("OFICINA", GetType(System.String))
                .Columns.Add("MONTO", GetType(System.Decimal))
                .Columns.Add("COD_TIPO", GetType(System.String))
                .Columns.Add("TIPO", GetType(System.String))
                .Columns.Add("DOCUMENTO", GetType(System.String))
                .Columns.Add("USUARIO_CONTABILIZA", GetType(System.String))
                .Columns.Add("FECHA_CONTABILIZA", GetType(System.String))
                .Columns.Add("HORA_CONTABILIZA", GetType(System.String))
                .Columns.Add("ESTADO_CONT", GetType(System.String))
                .Columns.Add("COMPROBANTE", GetType(System.String)) 'INI-936 - CNSO - Se agrego al final
            End With

            If dtDatos.Rows.Count > 0 Then
                For Each row As DataRow In dtDatos.Rows 
                    Dim newRow As DataRow
                    newRow = dtTblRemesa.NewRow
                    newRow("SOBRE") = row("SOBRE")
                    newRow("BOLSA") = row("BOLSA")
                    newRow("COD_USUARIO_ENVIA_REMESA") = row("COD_USUARIO_ENVIA_REMESA")
                    newRow("USUARIO_ENVIA_REMESA") = row("USUARIO_ENVIA_REMESA")
                    newRow("FECHA_ENVIO_REMESA") = CDate(row("FECHA_ENVIO_REMESA"))
                    newRow("HORA_ENVIO_REMESA") = CDate(row("HORA_ENVIO_REMESA"))
                    newRow("BUZON_FECHA") = CDate(row("BUZON_FECHA"))
                    newRow("BUZON_HORA") = CDate(row("BUZON_HORA"))
                    newRow("COD_OFICINA") = row("COD_OFICINA")
                    newRow("OFICINA") = row("OFICINA")
                    newRow("MONTO") = CDec(row("MONTO"))
                    newRow("COD_TIPO") = row("COD_TIPO")
                    newRow("TIPO") = row("TIPO")
                    newRow("DOCUMENTO") = row("DOCUMENTO")
                    newRow("USUARIO_CONTABILIZA") = row("USUARIO_CONTABILIZA")
                    If Not IsDBNull(row("FECHA_CONTABILIZA")) Then
                        newRow("FECHA_CONTABILIZA") = row("FECHA_CONTABILIZA")
                    End If
                    If Not IsDBNull(row("HORA_CONTABILIZA")) Then
                        newRow("HORA_CONTABILIZA") = row("HORA_CONTABILIZA")
                    End If
                    newRow("ESTADO_CONT") = row("ESTADO_CONT")
                    newRow("COMPROBANTE") = row("COMPROBANTE") 'INI-936 - CNSO - Se agrego al final
                    dtTblRemesa.Rows.Add(newRow)
                Next
            End If

            DGLista.DataSource = dtTblRemesa
            DGLista.DataBind()
            tblExportar = dtTblRemesa
            Session("TablaRemesaOrdenado") = Nothing
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Detalle Remesas - Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Detalle Remesas - FIN  CargarDatos")
            objClsAdmCaja = Nothing
        End Try

    End Sub

    Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)
        Dim dvConsulta As New DataView
        If Not Session("TablaRemesa") Is Nothing Then
            dvConsulta = New DataView(CType(Session("TablaRemesa"), DataTable))
            If hdnOrdenacion.Value <> "" Then
                dvConsulta.Sort = sortExpression + " " + direction
            End If
            DGLista.DataSource = dvConsulta
            DGLista.DataBind()
        End If

        Session("TablaRemesaOrdenado") = dvConsulta
    End Sub

#End Region

End Class
