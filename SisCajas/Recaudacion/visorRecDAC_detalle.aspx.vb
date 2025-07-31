Public Class visorRecDAC_detalle
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
    Dim strUsuario As String = String.Empty
    Dim strIdentifyLog As String = String.Empty
    Dim objclsOffline As COM_SIC_OffLine.clsOffline
    Dim objClsAdmCaja As COM_SIC_Adm_Cajas.clsAdmCajas
    Private Const ESTADOS_DAC As String = "ESTADO_DAC"
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
                If Not Request.QueryString("nd1") Is Nothing Then
                    CargarEstados()
                    CargarDatos()
                End If
            End If
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Dim strURL As String
        Session("EstadosDAC") = Nothing
        Session("TablaRecDACOrdenado") = Nothing
        Session("TablaRecDAC") = Nothing
        strURL = "visorRecDAC.aspx"
        Response.Redirect(strURL)
    End Sub

    Private Sub DGLista_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DGLista.ItemDataBound
        If e.Item.ItemType = ListItemType.EditItem Then
            Dim ddlEstado As DropDownList = DirectCast(e.Item.FindControl("cboEstado"), DropDownList)
            ddlEstado.DataTextField = "DESCRIPCION"
            ddlEstado.DataValueField = "CODIGO"
            ddlEstado.DataSource = DirectCast(Session("EstadosDAC"), DataTable)
            ddlEstado.SelectedValue = CStr(DataBinder.Eval(e.Item.DataItem, "CODESTADO"))
            ddlEstado.DataBind()
        End If
    End Sub

    Private Sub DGLista_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DGLista.EditCommand
        Dim strTrsRegDAC As String = DGLista.DataKeys(e.Item.ItemIndex).ToString()
        Dim dtData As DataTable = CType(Session("TablaRecDac"), DataTable)
        Dim filas() As DataRow = dtData.Select("ID_T_NRO_RECAUDACION_DAC = '" & strTrsRegDAC & "'")
        Dim fila As DataRow
        Dim tipoPago As String
        If Not filas Is Nothing Then
            If filas.Length > 0 Then
                'obtiene el primer registro encontrado FirstOrDefault
                fila = filas(0)
                tipoPago = fila("TIPO_PAGO")
            End If
        End If
        If Not tipoPago.Equals("DRA") Then
            DGLista.EditItemIndex = e.Item.ItemIndex
            CargarDatos()
        Else
            Response.Write("<script language=jscript> alert('El registro seleccionado no es editable.'); </script>")
        End If
    End Sub

    Private Sub DGLista_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DGLista.CancelCommand
        DGLista.EditItemIndex = -1
        CargarDatos()
    End Sub

    Private Sub DGLista_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DGLista.UpdateCommand
        Dim strTrsRegDAC As String = DGLista.DataKeys(e.Item.ItemIndex).ToString()
        Dim objCaja As New COM_SIC_Cajas.clsCajas
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Transac DAC - INI  Update")
            Dim cboEstado As DropDownList = DirectCast(e.Item.FindControl("cboEstado"), DropDownList)
            Dim cboEstadoValue As String = cboEstado.SelectedValue
            objclsOffline = New COM_SIC_OffLine.clsOffline
            Dim strMsjErr As String = String.Empty, strOficina As String = String.Empty, fechaCajas As String = String.Empty
            Dim nImporte As Decimal, strCajero As String = String.Empty
            Dim dtData As DataTable = CType(Session("TablaRecDac"), DataTable)
            Dim filas() As DataRow = dtData.Select("ID_T_NRO_RECAUDACION_DAC = '" & strTrsRegDAC & "'")
            Dim fila As DataRow
            If Not filas Is Nothing Then
                If filas.Length > 0 Then
                    'obtiene el primer registro encontrado FirstOrDefault
                    fila = filas(0)
                    strOficina = fila("OFICINA_VENTA")
                    fechaCajas = fila("FECHA_TRANSAC")
                    nImporte = CDec(fila("EFECTIVO"))
                    strCajero = fila("COD_CAJERO")
                End If
            End If
            strMsjErr = objclsOffline.ActualizarEstadoDAC(CInt(strTrsRegDAC), cboEstadoValue)

            'Anular: Resta el Efectivo Ingresado a CONF_CAJABUZON
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Inicio método : FP_InsertaEfectivo")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Estado DAC: " & cboEstadoValue)
            If cboEstadoValue.Equals("2") Then 'Anulado
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Oficina: " & strOficina)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Cajero: " & strCajero)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Importe: -" & nImporte.ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     fecha: " & fechaCajas)
                objCaja.FP_InsertaEfectivo(strOficina, strCajero, nImporte * (-1),fechaCajas)
            Else 'Activo
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Oficina: " & strOficina)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Cajero: " & strCajero)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Importe: " & nImporte.ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     fecha: " & fechaCajas)
                objCaja.FP_InsertaEfectivo(strOficina, strCajero, nImporte,fechaCajas)
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Fin método : FP_InsertaEfectivo")

            If Not strMsjErr.Equals(String.Empty) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Mensaje ERROR: " & strMsjErr)
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
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Transac Pago - FIN  Update")
            objClsAdmCaja = Nothing
            objCaja = Nothing
        End Try
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
            Dim strNroDoc1 As String, strNroDoc2 As String, strOficinaVta As String, strFechaIni As String
            Dim strFechaFin As String, strMtoPago1 As String, strMtoPago2 As String
            Dim strEstado As String, strCajero As String, strCodCliente1 As String, strCodCliente2 As String
            Dim strCntRegistros As String

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Transac DAC - INI  CargarDatos")

            strNroDoc1 = CStr(Request.QueryString("nd1"))
            strNroDoc2 = CStr(Request.QueryString("nd2"))
            strOficinaVta = CStr(Request.QueryString("of"))
            strFechaIni = CStr(Request.QueryString("fi"))
            strFechaFin = CStr(Request.QueryString("ff"))
            strMtoPago1 = CStr(Request.QueryString("mp1"))
            strMtoPago2 = CStr(Request.QueryString("mp2"))
            strEstado = CStr(Request.QueryString("est"))
            strCajero = CStr(Request.QueryString("cj"))
            strCodCliente1 = CStr(Request.QueryString("cc1"))
            strCodCliente2 = CStr(Request.QueryString("cc2"))
            strCntRegistros = CStr(Request.QueryString("creg"))

            Dim dsResult As New DataSet
            objclsOffline = New COM_SIC_OffLine.clsOffline

            dsResult = objclsOffline.GetRecauDAC(strNroDoc1, strNroDoc2, strOficinaVta, strFechaIni, _
                            strFechaFin, strMtoPago1, strMtoPago2, strEstado, _
                            strCajero, strCodCliente1, strCodCliente2, strCntRegistros)

            Dim dtTblRecDac As New DataTable
            With dtTblRecDac
                .Columns.Add("ID_T_NRO_RECAUDACION_DAC", GetType(System.String))
                .Columns.Add("NROAT", GetType(System.String))
                .Columns.Add("OFICINA_VENTA", GetType(System.String))
                .Columns.Add("NOM_OF_VENTA", GetType(System.String))
                .Columns.Add("FECHA_TRANSAC", GetType(System.DateTime))
                .Columns.Add("HORA_TRANSAC", GetType(System.DateTime))
                .Columns.Add("IMPORTE_PAGO", GetType(System.Decimal))
                .Columns.Add("ESTADO", GetType(System.String))
                .Columns.Add("CODESTADO", GetType(System.String))
                .Columns.Add("COD_CAJERO", GetType(System.Int64))
                .Columns.Add("NOM_CAJERO", GetType(System.String))
                .Columns.Add("COD_CLIENTE", GetType(System.String))
                .Columns.Add("NOMBRE_CLIENTE", GetType(System.String))
                .Columns.Add("EFECTIVO", GetType(System.Decimal))
                .Columns.Add("TIPO_PAGO", GetType(System.String))
            End With

            If dsResult.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In dsResult.Tables(0).Rows
                    Dim newRow As DataRow
                    newRow = dtTblRecDac.NewRow
                    newRow("ID_T_NRO_RECAUDACION_DAC") = row("ID_T_NRO_RECAUDACION_DAC")
                    newRow("NROAT") = row("NROAT")
                    newRow("OFICINA_VENTA") = row("OFICINA_VENTA")
                    newRow("NOM_OF_VENTA") = row("NOM_OF_VENTA")
                    newRow("FECHA_TRANSAC") = row("FECHA_TRANSAC")
                    newRow("HORA_TRANSAC") = row("HORA_TRANSAC")
                    newRow("IMPORTE_PAGO") = CDec(row("IMPORTE_PAGO"))
                    newRow("CODESTADO") = row("COD_ESTADO_PAGO")
                    newRow("ESTADO") = row("ESTADO_PAGO")
                    newRow("COD_CAJERO") = CLng(row("COD_CAJERO"))
                    newRow("NOM_CAJERO") = row("NOM_CAJERO")
                    newRow("COD_CLIENTE") = row("COD_CLIENTE")
                    newRow("NOMBRE_CLIENTE") = row("CLIENTE")
                    newRow("EFECTIVO") = row("EFECTIVO")
                    newRow("TIPO_PAGO") = row("TIPO_PAGO")
                    dtTblRecDac.Rows.Add(newRow)
                Next
            End If

            Session("TablaRecDac") = dtTblRecDac
            DGLista.DataSource = CType(Session("TablaRecDac"), DataTable)
            DGLista.DataBind()

            If dtTblRecDac.Rows.Count = 0 Then
                Response.Write("<script language=jscript> alert('No se encontraron registros'); </script>")
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Transac DAC - Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Transac DAC - FIN  CargarDatos")
            objclsOffline = Nothing
        End Try

    End Sub

    Private Sub CargarEstados()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Transac DAC - INI  CargarEstados")
            Dim dsEstado As New DataSet
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            dsEstado = objClsAdmCaja.GetCodigos(ESTADOS_DAC, String.Empty)
            Session("EstadosDAC") = dsEstado.Tables(0)
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Transac DAC - FIN  CargarEstados")
            objClsAdmCaja = Nothing
        End Try
    End Sub

    Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)
        Dim dvConsulta As New DataView
        If Not Session("TablaRecPag") Is Nothing Then
            dvConsulta = New DataView(CType(Session("TablaRecDac"), DataTable))
            If hdnOrdenacion.Value <> "" Then
                dvConsulta.Sort = sortExpression + " " + direction
            End If
            DGLista.DataSource = dvConsulta
            DGLista.DataBind()
        End If

        Session("TablaRecDACOrdenado") = dvConsulta
    End Sub

#End Region

End Class
