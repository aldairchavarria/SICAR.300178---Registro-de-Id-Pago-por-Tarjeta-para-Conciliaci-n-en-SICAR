Public Class visorRecDeuda_detalle
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
    Private Const TABLA_ESTADOS_TRANS As String = "ESTADO TRANSACCION"
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
                    strUsuario = Session("USUARIO")
                    strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
                    CargarDatos()
                    CargarEstados()
                End If
            End If
        End If
    End Sub

    Private Sub btnCancelar_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Dim strURL As String
        Session("EstadosTransac") = Nothing
        Session("TablaDeudaOrdenado") = Nothing
        Session("TablaDeuda") = Nothing
        strURL = "visorRecDeuda.aspx"
        Response.Redirect(strURL)
    End Sub

    Private Sub DGLista_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DGLista.EditCommand
        DGLista.EditItemIndex = e.Item.ItemIndex
        CargarDatos()
    End Sub

    Private Sub DGLista_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DGLista.ItemDataBound
        If e.Item.ItemType = ListItemType.EditItem Then
            Dim ddlEstado As DropDownList = DirectCast(e.Item.FindControl("cboEstado"), DropDownList)
            ddlEstado.DataTextField = "DESCRIPCION"
            ddlEstado.DataValueField = "CODIGO"
            ddlEstado.DataSource = DirectCast(Session("EstadosTransac"), DataTable)
            ddlEstado.SelectedValue = CStr(DataBinder.Eval(e.Item.DataItem, "ESTADO_TRANSAC"))
            ddlEstado.DataBind()
        End If
    End Sub

    Private Sub DGLista_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DGLista.UpdateCommand
        Dim strTrsRegDeuda As String = DGLista.DataKeys(e.Item.ItemIndex).ToString()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Deuda - INI  Update")
            Dim cboEstado As DropDownList = DirectCast(e.Item.FindControl("cboEstado"), DropDownList)
            Dim cboEstadoValue As String = cboEstado.SelectedValue
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim strMsjErr As String = String.Empty

            strMsjErr = objClsAdmCaja.ActualizarDataDeuda(CInt(strTrsRegDeuda), cboEstadoValue)

            If Not strMsjErr.Equals(String.Empty) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Deuda - Mensaje ERROR: " & strMsjErr)
                Response.Write("<script>alert('" & strMsjErr & "');</script>")
                DGLista.EditItemIndex = -1
                CargarDatos()
                Exit Sub
            Else
                Response.Write("<script language=jscript> alert('Actualizado correctamente.'); </script>")
                DGLista.EditItemIndex = -1
                CargarDatos()
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Deuda - Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Deuda - FIN  Update")
            objClsAdmCaja = Nothing
        End Try
    End Sub

    Private Sub DGLista_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DGLista.CancelCommand
        DGLista.EditItemIndex = -1
        CargarDatos()
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
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Deuda - INI  CargarDatos")

            Dim strNroTransac1 As String, strNroTransac2 As String, strOficinaVta As String, strFechaIni As String
            Dim strFechaFin As String, strMoneda As String, strMtoTotPago1 As String, strMtoTotPago2 As String
            Dim strEstado As String, strNroTel1 As String, strNroTel2 As String, strCajero As String
            Dim strTipoDoc As String, strNroDoc1 As String, strNroDoc2 As String, strRuc1 As String, strRuc2 As String, strCntRegistros As String
            Dim strSubOficinaVta As String

            strNroTransac1 = CStr(Request.QueryString("nt1"))
            strNroTransac2 = CStr(Request.QueryString("nt2"))
            strOficinaVta = CStr(Request.QueryString("of"))
            strSubOficinaVta = CStr(Request.QueryString("subof"))
            strFechaIni = CStr(Request.QueryString("fi"))
            strFechaFin = CStr(Request.QueryString("ff"))
            strMoneda = CStr(Request.QueryString("mn"))
            strMtoTotPago1 = CStr(Request.QueryString("mtp1"))
            strMtoTotPago2 = CStr(Request.QueryString("mtp2"))
            strEstado = CStr(Request.QueryString("est"))
            strNroTel1 = CStr(Request.QueryString("tel1"))
            strNroTel2 = CStr(Request.QueryString("tel2"))
            strCajero = CStr(Request.QueryString("cj"))
            strTipoDoc = CStr(Request.QueryString("td"))
            strNroDoc1 = CStr(Request.QueryString("ndoc1"))
            strNroDoc2 = CStr(Request.QueryString("ndoc2"))
            strRuc1 = CStr(Request.QueryString("rc1"))
            strRuc2 = CStr(Request.QueryString("rc2"))
            strCntRegistros = CStr(Request.QueryString("creg"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strNroTransac1 :" & strNroTransac1)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strNroTransac2 :" & strNroTransac2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strOficinaVta :" & strOficinaVta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strSubOficinaVta :" & strSubOficinaVta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strFechaIni :" & strFechaIni)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strFechaFin :" & strFechaFin)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strMoneda :" & strMoneda)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strMtoTotPago1 :" & strMtoTotPago1)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strMtoTotPago2 :" & strMtoTotPago2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strEstado :" & strEstado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strNroTel1 :" & strNroTel1)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strNroTel2 :" & strNroTel2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strCajero :" & strCajero)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strTipoDoc :" & strTipoDoc)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strNroDoc1 :" & strNroDoc1)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strNroDoc2 :" & strNroDoc2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strRuc1 :" & strRuc1)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strRuc2 :" & strRuc2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strCntRegistros :" & strCntRegistros)

            Dim dsResult As New DataSet
            Dim strRespuesta As String
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas

            dsResult = objClsAdmCaja.GetRecauDeuda(strNroTransac1, strNroTransac2, strOficinaVta, strSubOficinaVta, strFechaIni, _
                            strFechaFin, strMoneda, strMtoTotPago1, strMtoTotPago2, _
                            strEstado, strNroTel1, strNroTel2, strCajero, strTipoDoc, _
                            strNroDoc1, strNroDoc2, strRuc1, strRuc2, strCntRegistros, strRespuesta)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strRespuesta :" & strRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Cursor Count :" & dsResult.Tables(0).Rows.Count)

            Dim dtTblDeuda As New DataTable
            With dtTblDeuda
                .Columns.Add("ID_T_TRS_REG_DEUDA", GetType(System.String))
                .Columns.Add("NRO_TRANSACCION", GetType(System.String))
                .Columns.Add("NOM_DEUDOR", GetType(System.String))
                .Columns.Add("RUC_DEUDOR", GetType(System.String))
                .Columns.Add("OFICINA_VENTA", GetType(System.String))
                .Columns.Add("NOM_OF_VENTA", GetType(System.String))
                .Columns.Add("CASOV_SUB_OFICINA", GetType(System.String))
                .Columns.Add("SUB_OFICINA_DESC", GetType(System.String))
                .Columns.Add("FECHA_TRANSAC", GetType(System.DateTime))
                .Columns.Add("HORA_TRANSAC", GetType(System.DateTime))
                .Columns.Add("MON_PAGO", GetType(System.String))
                .Columns.Add("MONEDA", GetType(System.String))
                .Columns.Add("IMPORTE_PAGO", GetType(System.Decimal))
                .Columns.Add("ESTADO_TRANSAC", GetType(System.String))
                .Columns.Add("ESTADO", GetType(System.String))
                .Columns.Add("NRO_TELEFONO", GetType(System.String))
                .Columns.Add("COD_CAJERO", GetType(System.Int64))
                .Columns.Add("NOM_CAJERO", GetType(System.String))
                .Columns.Add("TIPO_DOC_DEUDOR", GetType(System.String))
                .Columns.Add("NRO_DOC_DEUDOR", GetType(System.String))
            End With

            If dsResult.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In dsResult.Tables(0).Rows
                    Dim newRow As DataRow
                    newRow = dtTblDeuda.NewRow
                    newRow("ID_T_TRS_REG_DEUDA") = row("ID_T_TRS_REG_DEUDA")
                    newRow("NRO_TRANSACCION") = row("NRO_TRANSACCION")
                    newRow("NOM_DEUDOR") = row("NOM_DEUDOR")
                    newRow("RUC_DEUDOR") = row("RUC_DEUDOR")
                    newRow("OFICINA_VENTA") = row("OFICINA_VENTA")
                    newRow("NOM_OF_VENTA") = row("NOM_OF_VENTA")
                    newRow("CASOV_SUB_OFICINA") = row("CASOV_SUB_OFICINA")
                    newRow("SUB_OFICINA_DESC") = row("SUB_OFICINA_DESC")
                    newRow("FECHA_TRANSAC") = row("FECHA_TRANSAC")
                    newRow("HORA_TRANSAC") = row("HORA_TRANSAC")
                    newRow("MON_PAGO") = row("MON_PAGO")
                    newRow("MONEDA") = row("MONEDA")
                    newRow("IMPORTE_PAGO") = CDec(row("IMPORTE_PAGO"))
                    newRow("ESTADO_TRANSAC") = row("ESTADO_TRANSAC")
                    newRow("ESTADO") = row("ESTADO")
                    newRow("NRO_TELEFONO") = row("NRO_TELEFONO")
                    newRow("COD_CAJERO") = CLng(row("COD_CAJERO"))
                    newRow("NOM_CAJERO") = row("NOM_CAJERO")
                    newRow("TIPO_DOC_DEUDOR") = row("TIPO_DOC_DEUDOR")
                    newRow("NRO_DOC_DEUDOR") = row("NRO_DOC_DEUDOR")
                    dtTblDeuda.Rows.Add(newRow)
                Next
            End If

            Session("TablaDeuda") = dtTblDeuda
            DGLista.DataSource = CType(Session("TablaDeuda"), DataTable)
            DGLista.DataBind()

            If dtTblDeuda.Rows.Count = 0 Then
                Response.Write("<script language=jscript> alert('No se encontraron registros'); </script>")
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Deuda - Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Deuda - FIN  CargarDatos")
            objClsAdmCaja = Nothing
        End Try

    End Sub

    Private Sub CargarEstados()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Deuda - INI  CargarEstados")
            Dim dsEstado As New DataSet
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            dsEstado = objClsAdmCaja.GetCodigos(TABLA_ESTADOS_TRANS, String.Empty)
            Session("EstadosTransac") = dsEstado.Tables(0)
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Deuda - FIN  CargarEstados")
            objClsAdmCaja = Nothing
        End Try
    End Sub

    Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)
        Dim dvConsulta As New DataView
        If Not Session("TablaDeuda") Is Nothing Then
            dvConsulta = New DataView(CType(Session("TablaDeuda"), DataTable))
            If hdnOrdenacion.Value <> "" Then
                dvConsulta.Sort = sortExpression + " " + direction
            End If
            DGLista.DataSource = dvConsulta
            DGLista.DataBind()
        End If

        Session("TablaDeudaOrdenado") = dvConsulta
    End Sub

#End Region

End Class
