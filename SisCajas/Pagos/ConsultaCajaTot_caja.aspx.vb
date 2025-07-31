Public Class ConsultaCajaTot_caja
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitulo As System.Web.UI.WebControls.Label
    Protected WithEvents lblFiltro As System.Web.UI.WebControls.Label
    Protected WithEvents DGLista As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtFiltro As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents cmdAceptar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents cmdCancelar As System.Web.UI.HtmlControls.HtmlInputButton

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
    Dim objclsAdmCaja As COM_SIC_Adm_Cajas.clsAdmCajas
    Private Const optActivos As String = "1"
    Private Const optTodos As String = "T"
    Private Const optInactivos As String = "0"
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
                txtFiltro.Attributes.Add("onkeydown", "f_buscar();")
                Session("ConCajTot_codCaja") = Nothing
                Session("dgListaCajero") = Nothing
                Session("dgListaOficina") = Nothing
                Session("dgListaCaja") = Nothing
                Buscar()
            End If
        End If
    End Sub

    Private Sub DGLista_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DGLista.PageIndexChanged
        DGLista.DataSource = Session("dgListaCaja")
        DGLista.CurrentPageIndex = e.NewPageIndex
        DGLista.DataBind()
    End Sub

    Private Sub cmdBuscar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.ServerClick
        Buscar()
    End Sub

    Private Sub cmdAceptar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAceptar.ServerClick
        If Len(Trim(Request.Item("rbSel"))) > 0 Then
            Session("ConCajTot_codCaja") = Trim(Request.Item("rbSel"))
            Response.Write("<script>window.opener.f_CargarDatosCaja();</script>")
            Response.Write("<script>window.close();</script>")
        End If
    End Sub

#End Region

#Region "Metodos"

    Private Sub Buscar()
        Try
            Dim strCaja As String = txtFiltro.Value
            objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim strOficina As String = "0"

            If Not Request.QueryString("of") Is Nothing Then
                If CStr(Request.QueryString("of")).Length = 0 Then
                    strOficina = "0"
                Else
                    strOficina = CStr(Request.QueryString("of"))
                End If
            End If

            Dim dsResult As DataSet = objclsAdmCaja.GetCajas(strCaja.ToUpper(), strOficina, optTodos)

            Session("dgListaCaja") = dsResult.Tables(0)
            Me.DGLista.DataSource = Session("dgListaCaja")
            Me.DGLista.CurrentPageIndex = 0
            Me.DGLista.DataBind()

            If dsResult.Tables(0).Rows.Count <= 0 Then
                Response.Write("<script language=jscript> alert('No se encontró datos'); </script>")
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try
    End Sub

#End Region

End Class
