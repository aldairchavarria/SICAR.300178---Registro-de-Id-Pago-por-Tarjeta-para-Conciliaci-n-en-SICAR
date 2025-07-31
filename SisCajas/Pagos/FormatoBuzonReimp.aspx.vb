Public Class FormatoBuzonReimp
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblCajero As System.Web.UI.WebControls.Label
    Protected WithEvents lblOficina As System.Web.UI.WebControls.Label
    Protected WithEvents lblBolsa As System.Web.UI.WebControls.Label
    Protected WithEvents lblMonto As System.Web.UI.WebControls.Label
    Protected WithEvents lblFecha As System.Web.UI.WebControls.Label
    Protected WithEvents lblHora As System.Web.UI.WebControls.Label

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
            Dim dsDetalle As DataSet
            Dim objCajas As New COM_SIC_Cajas.clsCajas
            dsDetalle = objCajas.FP_DetalleBuzon(Session("ALMACEN"), Request.Item("strBolsa"))

            If Not IsNothing(dsDetalle) Then
                If dsDetalle.Tables(0).Rows.Count > 0 Then
                    If dsDetalle.Tables(0).Rows(0).Item("BUZON_NOMUSUARIO").GetType Is GetType(System.DBNull) Then
                        lblCajero.Text = ""
                    Else
                        lblCajero.Text = dsDetalle.Tables(0).Rows(0).Item("BUZON_NOMUSUARIO")
                    End If

                    lblOficina.Text = Session("OFICINA")
                    lblMonto.Text = IIf(dsDetalle.Tables(0).Rows(0).Item("BUZON_TIPOVIA") = 1 Or dsDetalle.Tables(0).Rows(0).Item("BUZON_TIPOVIA") = 3, "S/", "US$") & Format(dsDetalle.Tables(0).Rows(0).Item("BUZON_MONTO"), "#####0.00")
                    lblBolsa.Text = dsDetalle.Tables(0).Rows(0).Item("BUZON_BOLSA")
                    lblFecha.Text = Format(dsDetalle.Tables(0).Rows(0).Item("BUZON_FECHA"), "d")
                    lblHora.Text = Format(dsDetalle.Tables(0).Rows(0).Item("BUZON_FECHA"), "hh:mm:ss")
                End If
            End If
        End If
    End Sub

    
End Class
