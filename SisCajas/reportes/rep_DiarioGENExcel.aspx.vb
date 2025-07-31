Public Class rep_DiarioGENExcel
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblfecha As System.Web.UI.WebControls.Label
    Protected WithEvents lblHora As System.Web.UI.WebControls.Label
    Protected WithEvents dgDiarioE As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim objSAP As New SAP_SIC_Pagos.clsPagos
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
            'Parametro de entrada ... Query string
            Dim strFechaIni As String = Request.QueryString("strFecIni")
            Dim strFechaFin As String = Request.QueryString("strFecFin")

            lblfecha.Text = strFechaIni & " - " & strFechaFin
            lblHora.Text = DateTime.Now.ToString("t")

            If Not Page.IsPostBack Then
                llena_grid(strFechaIni, strFechaFin)
            End If
            Response.AddHeader("Content-Disposition", "attachment;filename=DiarioElectrGEN.xls")
            Response.ContentType = "application/vnd.ms-excel"
        End If
    End Sub

    Private Sub llena_grid(ByVal fechaIni As String, ByVal fechaFin As String)
        Dim objCajas As New COM_SIC_Cajas.clsCajas

        Dim dsDiario As DataSet = objCajas.FP_ListDiarioGEN(fechaIni, fechaFin)
        Dim i As Integer
        Dim j As Integer
        Dim intFilas As Integer

        If Not dsDiario Is Nothing Then
            dgDiarioE.DataSource = dsDiario.Tables(0)
            dgDiarioE.DataBind()
        End If


    End Sub

    Private Sub dgDiarioE_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDiarioE.ItemDataBound
        'Dim dsOficina As DataSet

        'If e.Item.Cells(0).Text <> "" Then
        '    If Len(e.Item.Cells(0).Text) <= 5 Then
        '        dsOficina = objSAP.Get_ParamGlobal(e.Item.Cells(0).Text)
        '        e.Item.Cells(0).Text = dsOficina.Tables(0).Rows(0).Item("BEZEI")
        '    End If
        'End If

    End Sub
End Class
