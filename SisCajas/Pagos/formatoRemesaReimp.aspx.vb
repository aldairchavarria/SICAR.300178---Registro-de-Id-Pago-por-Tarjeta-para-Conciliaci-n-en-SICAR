Public Class formatoRemesaReimp
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgSobres As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblFecha As System.Web.UI.WebControls.Label
    Protected WithEvents lblBolsa As System.Web.UI.WebControls.Label
    Protected WithEvents lblNombre As System.Web.UI.WebControls.Label
    Protected WithEvents lblCodigo As System.Web.UI.WebControls.Label
    Protected WithEvents lblImpSol As System.Web.UI.WebControls.Label
    Protected WithEvents lblChSol As System.Web.UI.WebControls.Label
    Protected WithEvents lblChDol As System.Web.UI.WebControls.Label
    Protected WithEvents lblTSol As System.Web.UI.WebControls.Label
    Protected WithEvents lblTDol As System.Web.UI.WebControls.Label
    Protected WithEvents lblImpDol As System.Web.UI.WebControls.Label

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
            Dim objCajas As New COM_SIC_Cajas.clsCajas
            Dim dsDetalle As DataSet
            Dim dsSobres As DataSet

            dsDetalle = objCajas.FP_DetRemesa2(Session("ALMACEN"), Request.Item("strBolsa"))

            If Not IsNothing(dsDetalle) Then
                If dsDetalle.Tables(0).Rows.Count > 0 Then
                    lblFecha.Text = dsDetalle.Tables(0).Rows(0).Item("REMES_FECHA")
                    lblCodigo.Text = dsDetalle.Tables(0).Rows(0).Item("REMES_USUARIO")
                    lblNombre.Text = dsDetalle.Tables(0).Rows(0).Item("REMES_NOMUSUARIO")
                    lblBolsa.Text = dsDetalle.Tables(0).Rows(0).Item("REMES_BOLSA")

                    lblImpSol.Text = Format(dsDetalle.Tables(0).Rows(0).Item("REMES_MONTEFSOL"), "#####0.00")
                    lblImpDol.Text = Format(dsDetalle.Tables(0).Rows(0).Item("REMES_MONTEFDOL"), "#####0.00")
                    lblChSol.Text = Format(dsDetalle.Tables(0).Rows(0).Item("REMES_MONTCHSOL"), "#####0.00")
                    lblChDol.Text = Format(dsDetalle.Tables(0).Rows(0).Item("REMES_MONTCHDOL"), "#####0.00")

                    lblTSol.Text = Format(CDbl(lblImpSol.Text) + CDbl(lblChSol.Text), "#####0.00")
                    lblTDol.Text = Format(CDbl(lblImpDol.Text) + CDbl(lblChDol.Text), "#####0.00")

                    dsSobres = objCajas.FP_DetalleRemesa(Request.Item("strBolsa"))

                    dgSobres.DataSource = dsSobres
                    dgSobres.DataBind()

                End If
        End If
        End If
    End Sub

End Class
