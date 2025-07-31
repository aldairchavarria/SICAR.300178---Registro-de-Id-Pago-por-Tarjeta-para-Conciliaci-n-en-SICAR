Imports COM_SIC_OffLine
Public Class rep_anulacionesDelivery
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cboPdv As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboViaPago As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DGLista As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtFechaIni As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechaFin As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtMonto As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNroPedido As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtValor As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnExportar As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim sNumPages As Int32
    Public sFechaActual, sHoraActual
    Public sValor As String = String.Empty
    Dim objClsOffline As clsOffline

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                Session("TablaRepRec") = Nothing
                sFechaActual = ""
                sHoraActual = ""
                CargarFormasPago()
                CargarPuntosDeVenta()
            Else
                sFechaActual = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000") 'TimeOfDay().Date.Now.ToShortDateString
                sHoraActual = TimeOfDay().Hour & ":" & TimeOfDay().Minute
                If Not Session("TablaRepRec") Is Nothing Then
                    sValor = CType(CType(CType((Session("TablaRepRec")), Object), System.Data.DataTable).Rows, System.Data.DataRowCollection).Count()
                End If
            End If
        End If
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
            dsResult = objConsultaMsSap.ListarOficinas("ANU")
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

    Private Sub cmdBuscar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuscar.ServerClick
        Dim strFechaIni As String = txtFechaIni.Value
        Dim strFechaFin As String = txtFechaFin.Value

        If (strFechaIni <> "" And strFechaFin <> "") Then

            Dim nroDias As Int32 = DateDiff(DateInterval.Day, Date.Parse(strFechaIni), Date.Parse(strFechaFin))
            If (nroDias > 31) Then
                Response.Write("<script language=jscript> alert('" + "Error, No se puede realizar una bùsqueda mayor a 31 dìas." + "'); </script>")
            Else
                If (nroDias < 0) Then
                    Response.Write("<script language=jscript> alert('" + "Error, No se puede realizar una búsqueda de una fecha mayor a menor" + "'); </script>")
                Else
                    CargarTabla()
                End If
            End If

        End If
    End Sub

    Private Sub CargarTabla()
        Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim strFechaIni As String = txtFechaIni.Value
        Dim strFechaFin As String = txtFechaFin.Value
        Dim MedioPago As String = cboViaPago.SelectedValue
        Dim PuntoVenta As String = cboPdv.SelectedValue
        Dim Monto As String = txtMonto.Value
        Dim NroPedido As String = txtNroPedido.Value
        Dim IdVenta As String = txtValor.Value


        If (Monto = String.Empty) Then
            Monto = 0
        End If

        If (NroPedido = String.Empty) Then
            NroPedido = 0
        End If

        If (IdVenta = String.Empty) Then
            IdVenta = 0
        End If


        Dim dsResult As New DataSet
        dsResult = objConsultaMsSap.ConsultarReporteAnulacionDelivery(strFechaIni, strFechaFin, PuntoVenta, MedioPago, Int64.Parse(NroPedido), Decimal.Parse(Monto), IdVenta, "", "")
        sValor = dsResult.Tables(0).Rows.Count()
        Session("TablaRepRec") = dsResult.Tables(0)
        DGLista.DataSource = Session("TablaRepRec")
        DGLista.CurrentPageIndex = 0
        DGLista.DataBind()
        sValor = dsResult.Tables(0).Rows.Count
        sFechaActual = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000") 'TimeOfDay().Date.Now.ToShortDateString
        sHoraActual = TimeOfDay().Hour & ":" & TimeOfDay().Minute
    End Sub

End Class
