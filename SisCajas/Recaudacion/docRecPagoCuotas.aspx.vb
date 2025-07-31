Public Class docRecPagoCuotas
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgCuotas As System.Web.UI.WebControls.DataGrid
    Protected WithEvents trCuotas As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents dgCanceladas As System.Web.UI.WebControls.DataGrid
    Protected WithEvents trPagadas As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trCuotasTAB As System.Web.UI.HtmlControls.HtmlTableRow

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
            Dim dsCuotas As DataSet
            Dim dvFiltro As New DataView
            Dim dvFiltro2 As New DataView

            'Dim objRec As New SAP_SIC_Recaudacion.clsRecaudacion
            Dim objRec As New NEGOCIO_SIC_SANS.SansNegocio
            Dim usuario_id As String = Session("codUsuario")

            'dsCuotas = objRec.Get_DatosCuotas(Request.Item("strTelefono"), "", "", "", 0, 0, 0)
            dsCuotas = objRec.Get_DatosCuotas(Request.Item("strTelefono"), "", "", "", 0, 0, 0, "", "", usuario_id)

            If Not dsCuotas Is Nothing Then
            dvFiltro.Table = dsCuotas.Tables(0)
            dvFiltro.RowFilter = "TRIM(FECHA_PAGO) = ''"

            dgCuotas.DataSource = dvFiltro
            dgCuotas.DataBind()

            dvFiltro2.Table = dsCuotas.Tables(0)
            dvFiltro2.RowFilter = "TRIM(FECHA_PAGO) <> ''"

            dgCanceladas.DataSource = dvFiltro2
            dgCanceladas.DataBind()
            End If

            If dgCuotas.Items.Count = 0 Then
                trCuotas.Visible = False
                trCuotasTAB.Visible = False
            End If
            If dgCanceladas.Items.Count = 0 Then
                trPagadas.Visible = False
            End If
        End If
    End Sub

End Class
