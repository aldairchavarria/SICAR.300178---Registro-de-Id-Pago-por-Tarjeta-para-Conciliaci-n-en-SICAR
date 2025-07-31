Public Class SICAR_FormaPagos_Excel
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgTransacciones As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNombCli As System.Web.UI.WebControls.Label
    Protected WithEvents txtBolFact As System.Web.UI.WebControls.Label
    Protected WithEvents txtFechPago As System.Web.UI.WebControls.Label
    Protected WithEvents txtNroPedido As System.Web.UI.WebControls.Label
    Protected WithEvents txtMonto As System.Web.UI.WebControls.Label

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
        End If
        Dim strFecha As String = Request.QueryString("pfecha")
        Dim strNombreCli As String = Request.QueryString("pnombrecli")
        Dim strNroPedido As String = Request.QueryString("pnropedid")
        Dim strMonto As String = Request.QueryString("pmonto")
        Dim strNroFactura As String = Request.QueryString("pidtransac")
        Dim strNombreFile As String = "FormasDePagoDelDocumentoPagado_" & strFecha & Now.ToString("HHmmss") & ".xls"
        txtNombCli.Text = strNombreCli
        txtBolFact.Text = strNroFactura
        txtNroPedido.Text = strNroPedido
        txtFechPago.Text = strFecha
        txtMonto.Text = strMonto
        If Not Page.IsPostBack Then
            llena_grid()
        End If
        Response.AddHeader("Content-Disposition", "attachment;filename=" & strNombreFile)
        Response.ContentType = "application/vnd.ms-excel"
        'End If
    End Sub
    Private Sub llena_grid()

        Dim dsPagos As DataSet
        Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim CodCajero As String = Session("USUARIO")
        Dim CodPDV As String = Session("ALMACEN")
        Dim codRespuesta As String = ""
        Dim msjRespuesta As String = ""
        Dim NroPedido As String = txtBolFact.Text.ToString
        Dim TipoPago As String = ClsKeyPOS.strTipPagoRP
        dsPagos = objConsultaPos.ObtenerFormasDePagoRecTrans(NroPedido, "", TipoPago, "", CodCajero, CodPDV, codRespuesta, msjRespuesta, txtFechPago.Text.ToString)
        dgTransacciones.DataSource = dsPagos.Tables(0)
        dgTransacciones.DataBind()

    End Sub
End Class
