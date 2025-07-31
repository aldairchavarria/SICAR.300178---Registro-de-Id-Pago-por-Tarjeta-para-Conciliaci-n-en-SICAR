Public Class visorRecPago_detalle
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
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogVisorRecaudacion")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogVisorRecaudacion")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String
    Dim strIdentifyLog As String
    Dim objClsAdmCaja As COM_SIC_Adm_Cajas.clsAdmCajas
    Private Const SORT_ASCENDING As String = "ASC"
    Private Const SORT_DESCENDING As String = "DESC"

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
                If Not Request.QueryString("nt1") Is Nothing Then
                    CargarDatos()
                End If
            End If
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Dim strURL As String
        strURL = "visorRecPago.aspx"
        Session("TablaPagosOrdenado") = Nothing
        Session("TablaPagos") = Nothing
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

    Private Sub CargarDatos()
        Try
            Dim strNroTransac1 As String, strNroTransac2 As String, strViaPago As String, strImportePago1 As String
            Dim strImportePago2 As String, strNroChq1 As String, strNroChq2 As String, strNroCont1 As String
            Dim strNroCont2 As String, strCantidadReg As String

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Pago - INI  CargarDatos")

            strNroTransac1 = CStr(Request.QueryString("nt1"))
            strNroTransac2 = CStr(Request.QueryString("nt2"))
            strViaPago = CStr(Request.QueryString("vp"))
            strImportePago1 = CStr(Request.QueryString("ip1"))
            strImportePago2 = CStr(Request.QueryString("ip2"))
            strNroChq1 = CStr(Request.QueryString("nchq1"))
            strNroChq2 = CStr(Request.QueryString("nchq2"))
            strNroCont1 = CStr(Request.QueryString("nc1"))
            strNroCont2 = CStr(Request.QueryString("nc2"))
            strCantidadReg = CStr(Request.QueryString("creg"))

            Dim dsResult As New DataSet
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas

            dsResult = objClsAdmCaja.GetRecauPagos(strNroTransac1, strNroTransac2, strViaPago, _
                            strImportePago1, strImportePago2, _
                            strNroChq1, strNroChq2, _
                            strNroCont1, strNroCont2, strCantidadReg)

            Dim dtTblPagos As New DataTable
            With dtTblPagos
                .Columns.Add("ID_T_TRS_REG_DEUDA", GetType(System.String))
                .Columns.Add("NRO_TRANSACCION", GetType(System.String))
                .Columns.Add("VIA_PAGO", GetType(System.String))
                .Columns.Add("DESC_VIA_PAGO", GetType(System.String))
                .Columns.Add("IMPORTE_PAGADO", GetType(System.Decimal))
                .Columns.Add("NRO_CHEQUE", GetType(System.String))
                .Columns.Add("DOC_CONTABLE", GetType(System.String))
                .Columns.Add("USUARIO_FI", GetType(System.String))
                .Columns.Add("FECHA_FI", GetType(System.DateTime))
                .Columns.Add("ESTADO_CONT", GetType(System.String))
            End With

            If dsResult.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In dsResult.Tables(0).Rows
                    Dim newRow As DataRow
                    newRow = dtTblPagos.NewRow
                    newRow("ID_T_TRS_REG_DEUDA") = row("ID_T_TRS_REG_DEUDA")
                    newRow("NRO_TRANSACCION") = row("NRO_TRANSACCION")
                    newRow("VIA_PAGO") = row("VIA_PAGO")
                    newRow("DESC_VIA_PAGO") = row("DESC_VIA_PAGO")
                    newRow("IMPORTE_PAGADO") = CDec(row("IMPORTE_PAGADO"))
                    newRow("NRO_CHEQUE") = row("NRO_CHEQUE")
                    newRow("DOC_CONTABLE") = row("DOC_CONTABLE")
                    newRow("USUARIO_FI") = row("USUARIO_FI")
                    newRow("FECHA_FI") = row("FECHA_FI")
                    newRow("ESTADO_CONT") = row("ESTADO_CONT")
                    dtTblPagos.Rows.Add(newRow)
                Next
            End If

            Session("TablaPagos") = dtTblPagos
            DGLista.DataSource = CType(Session("TablaPagos"), DataTable)
            DGLista.DataBind()

            If dsResult.Tables(0).Rows.Count = 0 Then
                Response.Write("<script language=jscript> alert('No se encontraron registros'); </script>")
            End If

        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Pago - Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Pago - FIN  CargarDatos")
            objClsAdmCaja = Nothing
        End Try

    End Sub

    Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)
        Dim dvConsulta As New DataView
        If Not Session("TablaPagos") Is Nothing Then
            dvConsulta = New DataView(CType(Session("TablaPagos"), DataTable))
            If hdnOrdenacion.Value <> "" Then
                dvConsulta.Sort = sortExpression + " " + direction
            End If
            DGLista.DataSource = dvConsulta
            DGLista.DataBind()
        End If

        Session("TablaPagosOrdenado") = dvConsulta
    End Sub

#End Region

End Class
