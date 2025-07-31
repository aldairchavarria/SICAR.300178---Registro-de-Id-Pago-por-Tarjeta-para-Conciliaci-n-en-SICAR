Public Class visorRecRecibo_detalle
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
        strURL = "visorRecRecibo.aspx"
        Session("TablaReciboOrdenado") = Nothing
        Session("TablaRecibo") = Nothing
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
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI - Recibo - CargarDatos")

            Dim strNroTransac1 As String, strNroTransac2 As String, strTipoDocRec As String, strNroDocRec1 As String
            Dim strNroDocRec2 As String, strMoneda As String, strImporteRecibo1 As String, strImporteRecibo2 As String
            Dim strMtoTotPago1 As String, strMtoTotPago2 As String, strNroCob1 As String, strNroCob2 As String
            Dim strNroAcre1 As String, strNroAcre2 As String, strFechaIni As String, strFechaFin As String
            Dim strNroDoc1 As String, strNroDoc2 As String, strTrcPago1 As String, strTrcPago2 As String, strCntRegistros As String

            strNroTransac1 = CStr(Request.QueryString("nt1"))
            strNroTransac2 = CStr(Request.QueryString("nt2"))
            strTipoDocRec = CStr(Request.QueryString("tdr"))
            strNroDocRec1 = CStr(Request.QueryString("ndr1"))
            strNroDocRec2 = CStr(Request.QueryString("ndr2"))
            strMoneda = CStr(Request.QueryString("mn"))
            strImporteRecibo1 = CStr(Request.QueryString("ir1"))
            strImporteRecibo2 = CStr(Request.QueryString("ir2"))
            strMtoTotPago1 = CStr(Request.QueryString("mtp1"))
            strMtoTotPago2 = CStr(Request.QueryString("mtp2"))
            strNroCob1 = CStr(Request.QueryString("nc1"))
            strNroCob2 = CStr(Request.QueryString("nc2"))
            strNroAcre1 = CStr(Request.QueryString("na1"))
            strNroAcre2 = CStr(Request.QueryString("na2"))
            strFechaIni = CStr(Request.QueryString("fi"))
            strFechaFin = CStr(Request.QueryString("ff"))
            strNroDoc1 = CStr(Request.QueryString("ndoc1"))
            strNroDoc2 = CStr(Request.QueryString("ndoc2"))
            strTrcPago1 = CStr(Request.QueryString("tp1"))
            strTrcPago2 = CStr(Request.QueryString("tp2"))
            strCntRegistros = CStr(Request.QueryString("creg"))

            Dim dsResult As New DataSet
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas

            dsResult = objClsAdmCaja.GetRecauRecibos(strNroTransac1, strNroTransac2, strTipoDocRec, _
                            strNroDocRec1, strNroDocRec2, strMoneda, strImporteRecibo1, strImporteRecibo2, _
                            strMtoTotPago1, strMtoTotPago2, strNroCob1, strNroCob2, _
                            strNroAcre1, strNroAcre2, strFechaIni, strFechaFin, strNroDoc1, strNroDoc2, _
                            strTrcPago1, strTrcPago2, strCntRegistros)

            Dim dtTblRecibo As New DataTable
            With dtTblRecibo
                .Columns.Add("NRO_TRANSACCION", GetType(System.String))
                .Columns.Add("TIPO_DOC_RECAUD", GetType(System.String))
                .Columns.Add("NRO_DOC_RECAUD", GetType(System.String))
                .Columns.Add("MONEDA_DOCUM", GetType(System.String))
                .Columns.Add("MONEDA", GetType(System.String))
                .Columns.Add("NRO_OPE_ACREE", GetType(System.Int64))
                .Columns.Add("IMPORTE_RECIBO", GetType(System.Decimal))
                .Columns.Add("IMPORTE_PAGADO", GetType(System.Decimal))
                .Columns.Add("NRO_COBRANZA", GetType(System.Int64))
                .Columns.Add("FECHA_PAGO", GetType(System.DateTime))
                .Columns.Add("FECHA_EMISION", GetType(System.DateTime))
                .Columns.Add("NRO_DOC_DEUDOR", GetType(System.String))
                .Columns.Add("NRO_TRACE_PAGO", GetType(System.String))
                .Columns.Add("NRO_TRACE_ANUL", GetType(System.String))
                .Columns.Add("DESC_SERVICIO", GetType(System.String))
            End With

            If dsResult.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In dsResult.Tables(0).Rows
                    Dim newRow As DataRow
                    newRow = dtTblRecibo.NewRow
                    newRow("NRO_TRANSACCION") = row("NRO_TRANSACCION")
                    newRow("TIPO_DOC_RECAUD") = row("TIPO_DOC_RECAUD")
                    newRow("NRO_DOC_RECAUD") = row("NRO_DOC_RECAUD")
                    newRow("MONEDA_DOCUM") = row("MONEDA_DOCUM")
                    newRow("MONEDA") = row("MONEDA")
                    newRow("NRO_OPE_ACREE") = CLng(row("NRO_OPE_ACREE"))
                    newRow("IMPORTE_RECIBO") = CDec(row("IMPORTE_RECIBO"))
                    newRow("IMPORTE_PAGADO") = CDec(row("IMPORTE_PAGADO"))
                    newRow("NRO_COBRANZA") = CLng(row("NRO_COBRANZA"))
                    newRow("FECHA_PAGO") = row("FECHA_PAGO")
                    newRow("FECHA_EMISION") = row("FECHA_EMISION")
                    newRow("NRO_DOC_DEUDOR") = row("NRO_DOC_DEUDOR")
                    newRow("NRO_TRACE_PAGO") = row("NRO_TRACE_PAGO")
                    newRow("NRO_TRACE_ANUL") = row("NRO_TRACE_ANUL")
                    newRow("DESC_SERVICIO") = row("DESC_SERVICIO")
                    dtTblRecibo.Rows.Add(newRow)
                Next
            End If

            Session("TablaRecibo") = dtTblRecibo
            DGLista.DataSource = CType(Session("TablaRecibo"), DataTable)
            DGLista.DataBind()

            If dsResult.Tables(0).Rows.Count = 0 Then
                Response.Write("<script language=jscript> alert('No se encontraron registros'); </script>")
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Recibo - Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN - Recibo - CargarDatos")
            objClsAdmCaja = Nothing
        End Try
    End Sub

    Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)
        Dim dvConsulta As New DataView
        If Not Session("TablaRecibo") Is Nothing Then
            dvConsulta = New DataView(CType(Session("TablaRecibo"), DataTable))
            If hdnOrdenacion.Value <> "" Then
                dvConsulta.Sort = sortExpression + " " + direction
            End If
            DGLista.DataSource = dvConsulta
            DGLista.DataBind()
        End If

        Session("TablaReciboOrdenado") = dvConsulta
    End Sub

#End Region

End Class
