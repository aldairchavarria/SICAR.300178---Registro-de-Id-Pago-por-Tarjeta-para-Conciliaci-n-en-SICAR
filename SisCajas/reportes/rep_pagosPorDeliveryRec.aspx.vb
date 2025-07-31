Imports COM_SIC_OffLine
Public Class rep_pagosPorDeliveryRec
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cboTipDocVenta As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboViaPago As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DGLista As System.Web.UI.WebControls.DataGrid
    Protected WithEvents hidCodCajero As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtFechaIni As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechaFin As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents cboPdv As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnExportar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnRegresar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents cboAtributos As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtMonto As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNroPedido As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtSerie As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCorrelativo As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtValor As System.Web.UI.HtmlControls.HtmlInputText
    'Inicio INI-936 - Nuevos controles
    Protected WithEvents btnLimpiar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents btnClear As System.Web.UI.WebControls.Button
    Protected WithEvents hidNroPedido As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCodPdv As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidFechaIni As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidFechaFin As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidFormaPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidMonto As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTipoDoc As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidSerieDoc As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCorrelativoDoc As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidIdAtributo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidValorAtributo As System.Web.UI.HtmlControls.HtmlInputHidden
    'Fin INI-936


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Public sFechaActual, sHoraActual
    Public sValor As String = String.Empty
    Dim objRecordSet As DataTable
    Private Const SORT_ASCENDING As String = "ASC"
    Private Const SORT_DESCENDING As String = "DESC"
    Dim objClsOffline As clsOffline
    Public tblExportar As DataTable 'INI-936 - JH
    Protected WithEvents hdnOrdenacion As System.Web.UI.HtmlControls.HtmlInputHidden

    'Inicio - INI-936 - JCI - Agregadas variables para log'
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = "Log_repPagosPorDeliveryRec"
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    'Fin - INI-936 - JCI

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            'Put user code to initialize the page here
            'Inicio -- INI-936 - JH -- Atributos agregados para los botones
            btnBuscar.Attributes.Add("onClick", "f_Buscar()")
            btnClear.Attributes.Add("onClick", "f_ReestablecerControles(true)")
            'Fin -- INI-936 - JH -- Atributos agregados para los botones

            If Not Page.IsPostBack Then
                Session("TablaRepRec") = Nothing
                Session("reportePagosDeliveryRec") = Nothing 'INI-936 - JH
                sFechaActual = ""
                sHoraActual = ""
                CargarFormasPago()
                CargarPuntosDeVenta()
                CargarTipoDocumentoVenta()
                CargarAtributos()
            Else
                sFechaActual = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000") 'TimeOfDay().Date.Now.ToShortDateString
                sHoraActual = TimeOfDay().Hour & ":" & TimeOfDay().Minute
                If Not Session("TablaRepRec") Is Nothing Then
                    sValor = CType(CType(CType((Session("TablaRepRec")), Object), System.Data.DataTable).Rows, System.Data.DataRowCollection).Count()
                End If
            End If

            'Inicio -- INI-936 - JCI -- Agregados hidden para que al hacer postback no se limpien
            hidNroPedido.Value = txtNroPedido.Value
            hidCodPdv.Value = cboPdv.SelectedValue
            hidFechaIni.Value = txtFechaIni.Value
            hidFechaFin.Value = txtFechaFin.Value
            hidFormaPago.Value = cboViaPago.SelectedValue
            hidMonto.Value = txtMonto.Value
            hidTipoDoc.Value = cboTipDocVenta.SelectedValue
            hidSerieDoc.Value = txtSerie.Value
            hidCorrelativoDoc.Value = txtCorrelativo.Value
            hidIdAtributo.Value = cboAtributos.SelectedValue
            hidValorAtributo.Value = txtValor.Value
            'Fin -- INI-936 - JCI
        End If
    End Sub

    Private Sub DGLista_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DGLista.PageIndexChanged
        DGLista.DataSource = Session("TablaRepRec")
        tblExportar = Session("TablaRepRec") 'INI-936 - JH
        DGLista.CurrentPageIndex = e.NewPageIndex
        DGLista.DataBind()
    End Sub

    'Inicio - INI-936 - JH - Eventos nuevos asociados a controles html nuevos 
    'Evento Click creado en el HTML como boton ASP
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim strNroPedido As String = txtNroPedido.Value
        Dim strSerie As String = txtSerie.Value
        Dim strCorrelativo As String = txtCorrelativo.Value

        If (strSerie.Equals("Serie")) Then
            strSerie = ""
        End If

        If (strCorrelativo.Equals("Correlativo")) Then
            strCorrelativo = ""
        End If

        If (strNroPedido <> "") Then
            CargarTabla()
        Else
            If (strSerie <> "" And strCorrelativo <> "") Then
                CargarTabla()
            Else
                CargarTabla()
            End If
        End If

    End Sub

    'Evento Click creado en el HTML como boton ASP
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        DGLista.DataSource = Nothing
        DGLista.DataBind()
        sValor = ""
        sFechaActual = ""
    End Sub

    'Evento ItemDataBound creado para dar formato a la columna Monto Pagado del reporte
    Private Sub DGLista_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DGLista.ItemDataBound
        Dim i As Integer
        Dim dblCelda As Double
        Dim convertCelda As String
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            For i = 0 To e.Item.Cells.Count - 1
                If (i = 10) Then
                    dblCelda = Val(e.Item.Cells(10).Text)
                    convertCelda = dblCelda.ToString("#,###,##0.00")
                    e.Item.Cells(10).Text = convertCelda
                End If
            Next
        End If
    End Sub
    'Fin - INI-936 - JH

    Private Sub CargarTabla()
        Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap

        'Inicio - INI-936 - JCI - Declaradas variables para el c�digo y mensaje de respuesta e inicio de log
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio m�todo CargarTabla()")
        Dim codRpta As String = String.Empty ' INI-936 - JCI
        Dim msjRpta As String = String.Empty ' INI-936 - JCI
        'Fin - INI-936 - JCI

        Dim strFechaIni As String = txtFechaIni.Value
        Dim strFechaFin As String = txtFechaFin.Value
        Dim TipoDocVenta As String = cboTipDocVenta.SelectedValue
        Dim MedioPago As String = cboViaPago.SelectedValue
        Dim PuntoVenta As String = cboPdv.SelectedValue
        Dim Monto As String = txtMonto.Value
        Dim NroPedido As String = txtNroPedido.Value
        Dim Serie As String = txtSerie.Value
        Dim Correlativo As String = txtCorrelativo.Value
        Dim Atributo As String = cboAtributos.SelectedValue
        Dim Referencia As String = txtValor.Value

        If (Monto = String.Empty) Then
            Monto = 0
        End If

        If (NroPedido = String.Empty) Then
            NroPedido = 0
        End If

        If (Serie.Equals("Serie")) Then
            Serie = ""
        End If

        If (Correlativo.Equals("Correlativo")) Then
            Correlativo = ""
        End If

        If (Referencia.Equals("Valor")) Then
            Referencia = ""
        End If

        Dim dsResult As New DataSet

        'Inicio - INI-936 - JCI - Declaradas variables para el codigo y mensaje de respuesta e inicio de log
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Parametros IN --> strFechaIni : " & strFechaIni)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Parametros IN --> strFechaFin : " & strFechaFin)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Parametros IN --> PuntoVenta : " & PuntoVenta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Parametros IN --> MedioPago : " & MedioPago)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Parametros IN --> NroPedido : " & NroPedido)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Parametros IN --> Monto : " & Monto)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Parametros IN --> Serie : " & Serie)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Parametros IN --> Correlativo : " & Correlativo)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Parametros IN --> Atributo : " & Atributo)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Parametros IN --> Referencia : " & Referencia)
        'Fin - INI-936 - JCI

        dsResult = objConsultaMsSap.ConsultarReporteRec(strFechaIni, strFechaFin, PuntoVenta, MedioPago, TipoDocVenta, Int64.Parse(NroPedido), Decimal.Parse(Monto), Serie, Correlativo, Int32.Parse(Atributo), Referencia, codRpta, msjRpta) ' INI-936 - JCI - Agregadas variables para codigo y mensaje de respuesta

        'Inicio - INI-936 - JCI - Log para recuperar valores de respuesta
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Parametros OUT --> codRpta : " & codRpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Parametros OUT --> msjRpta : " & msjRpta)
        'Fin - INI-936 - JCI

        If codRpta.Equals("0") Then
            If dsResult.Tables(0).Rows.Count() > 0 Then
        sValor = dsResult.Tables(0).Rows.Count()
                objFileLog.Log_WriteLog(pathFile, strArchivo, "(cantidad registros) --> sValor : " & sValor)
                tblExportar = dsResult.Tables(0) 'INI-936 - JH
        Session("TablaRepRec") = dsResult.Tables(0)
        DGLista.DataSource = Session("TablaRepRec")
        DGLista.CurrentPageIndex = 0
        DGLista.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        DGLista.HeaderStyle.Width = New System.Web.UI.WebControls.Unit("10%")
        DGLista.HeaderStyle.Height = New System.Web.UI.WebControls.Unit("22px")
        DGLista.ItemStyle.HorizontalAlign = HorizontalAlign.Center
        DGLista.DataBind()
        Me.DGLista.CurrentPageIndex = 0
        sFechaActual = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000") 'TimeOfDay().Date.Now.ToShortDateString
        sHoraActual = TimeOfDay().Hour & ":" & TimeOfDay().Minute
                Else
                'Inicio - INI-936 - JCI - Agregado mensaje de respuesta en caso de error y el alert para el front
                msjRpta = "No se encontraron resultados para la busqueda indicada. Intente con otros filtros"
                objFileLog.Log_WriteLog(pathFile, strArchivo, msjRpta)
                sValor = 0
                Response.Write("<script language=jscript> alert('" + msjRpta + "'); </script>")
                'Fin - INI-936 - JCI
            End If
                Else
            'Inicio - INI-936 - JCI - Agregado mensaje de respuesta en caso de error y el alert para el front
            sValor = 0
            msjRpta = "Ha ocurrido un error al consultar los registros: " & msjRpta
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Exception - " & msjRpta)
            Response.Write("<script language=jscript> alert('" + msjRpta + "'); </script>")
            'Fin - INI-936 - JCI
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin metodo CargarTabla()")
    End Sub

    Private Sub CargarAtributos()
        Try
            Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
            Dim dsResult As New DataSet
            dsResult = objConsultaMsSap.ObtenerDatosCombo()

            Dim dtResult As New DataTable

            With dtResult
                .Columns.Add("CODIGO", GetType(Int32))
                .Columns.Add("DESCRIPCION", GetType(String))
            End With

            If Not dsResult Is Nothing Then
                Dim dr As DataRow
                For Each item As DataRow In dsResult.Tables(1).Rows
                    dr = dtResult.NewRow
                    dr("CODIGO") = Trim(item("N_ID"))
                    dr("DESCRIPCION") = Trim(item("V_CAMPO"))
                    dtResult.Rows.Add(dr)
                Next
                dtResult.AcceptChanges()
            End If

            cboAtributos.DataSource = dtResult
            cboAtributos.DataTextField = "DESCRIPCION"
            cboAtributos.DataValueField = "CODIGO"
            cboAtributos.DataBind()

            cboAtributos.Items.Insert(0, New ListItem("TODOS", "0"))
            cboAtributos.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CargarFormasPago()
        Try
            Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
            Dim dsResult As New DataSet
            dsResult = objConsultaMsSap.ObtenerDatosCombo()

            Dim dtResult As New DataTable

            With dtResult
                .Columns.Add("CODIGO", GetType(String))
                .Columns.Add("DESCRIPCION", GetType(String))
            End With

            If Not dsResult Is Nothing Then
                Dim dr As DataRow
                For Each item As DataRow In dsResult.Tables(0).Rows
                    dr = dtResult.NewRow
                    dr("CODIGO") = Trim(item("FORMV_CCINS"))
                    dr("DESCRIPCION") = Trim(item("FORMV_DESCRIPCION"))
                    dtResult.Rows.Add(dr)
                Next
                dtResult.AcceptChanges()
            End If

            cboViaPago.DataSource = dtResult
            cboViaPago.DataTextField = "DESCRIPCION"
            cboViaPago.DataValueField = "CODIGO"
            cboViaPago.DataBind()

            cboViaPago.Items.Insert(0, New ListItem("TODOS", ""))
            cboViaPago.SelectedIndex = 0
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        Finally
            objClsOffline = Nothing
        End Try
    End Sub

    Private Sub CargarPuntosDeVenta()
        Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap


        Dim dsResult As New DataSet
        Dim dtResult As New DataTable
        Dim intIndex As Integer = 0
        Try
            dsResult = objConsultaMsSap.ListarOficinas("REC")
            dtResult = dsResult.Tables(0)

            cboPdv.DataSource = dtResult
            cboPdv.DataValueField = "CODIGO"
            cboPdv.DataTextField = "DESCRIPCION"
            cboPdv.DataBind()
            cboPdv.Items.Insert(0, New ListItem("TODOS", ""))
            cboPdv.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CargarTipoDocumentoVenta()
        cboTipDocVenta.Items.Insert(0, New ListItem("TODOS", ""))
        cboTipDocVenta.Items.Insert(1, New ListItem("BOLETA", "BOLETA"))
        cboTipDocVenta.Items.Insert(2, New ListItem("FACTURA", "FACTURA"))
        cboTipDocVenta.Items.Insert(3, New ListItem("RENTA ADELANTADA", "RENTA ADELANTADA"))
        cboTipDocVenta.SelectedIndex = 0
    End Sub

End Class
