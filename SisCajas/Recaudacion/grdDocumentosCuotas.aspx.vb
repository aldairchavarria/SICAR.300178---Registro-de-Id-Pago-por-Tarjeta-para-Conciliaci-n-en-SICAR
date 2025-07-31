Public Class grdDocumentosCuotas
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgrRecauda As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtTrama As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdImprimir As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtFecha As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtTelef As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim objRecaud As New SAP_SIC_Recaudacion.clsRecaudacion
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
            Dim dsPool As DataSet
            Dim dvFiltro As New DataView


            If Not Page.IsPostBack Then
                txtFecha.Value = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000") 'Now.Today.ToString("d")
            End If

            dsPool = objRecaud.Get_PoolDAC(Session("ALMACEN"), Session("USUARIO"), txtFecha.Value)

            dvFiltro.Table = dsPool.Tables(0)
            dvFiltro.RowFilter = "RECTP='2'"

            dgrRecauda.DataSource = dvFiltro

        dgrRecauda.DataBind()

        End If
    End Sub

    Private Sub cmdImprimir_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImprimir.ServerClick
        Dim dsConsulta As DataSet
        Dim strDealer As String
        Dim dblMonto As Double
        Dim strTrama As String
        Dim i As Integer

        dsConsulta = objRecaud.Get_ConsultaPagoDAC(Request.Item("rdoDocumentoSAP").Split(";")(0))

        strDealer = Replace(dsConsulta.Tables(0).Rows(0).Item("NAME1"), "&", " ")
        strTrama = ""

        For i = 0 To dsConsulta.Tables(0).Rows.Count - 1
            strTrama &= dsConsulta.Tables(0).Rows(i).Item("VTEXT") & ";|"
            dblMonto += dsConsulta.Tables(0).Rows(i).Item("MONTO")
        Next

        If Len(strTrama) > 0 Then
            strTrama = Left(strTrama, Len(strTrama) - 1)
        End If

        txtTrama.Text = strTrama
        txtMonto.Text = dblMonto
        txtDealer.Text = strDealer
        txtTelef.Text = Request.Item("rdoDocumentoSAP").Split(";")(1)
        'Response.Write(txtTrama.Text & "<BR>")
        'Response.Write(txtMonto.Text & "<BR>")
        'Response.Write(txtDealer.Text & "<BR>")
        'Response.Write(txtTelef.Text) : Response.End()
        'Response.Write("<script language=javascript>window.open('docRecaudacionDAC.aspx?Dealer=" & strDealer & "&MontoTotalPagado=" & dblMonto & "&strTrama=" & strTrama & "','docRecaudacion','menubar=false,width=325,height=420')</script>")
    End Sub

    
End Class
