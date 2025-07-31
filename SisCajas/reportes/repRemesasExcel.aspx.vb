Public Class repRemesasExcel
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgRemesas As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim objPagos As New COM_SIC_OffLine.clsOffline

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
            Dim dsReporte As DataSet
            Dim strFecIni As String
            Dim strFecFin As String
            Dim strOficina As String

            strFecIni = Request.Item("strFecIni")
            strFecFin = Request.Item("strFecFin")
            strOficina = Request.Item("strOficina")


            'dsReporte = objCajas.FP_ListaRemesa(strOficina, CDate(strFecIni), CDate(strFecFin))
            dsReporte = objCajas.FP_ListaRemesa(strOficina, strFecIni, strFecFin)
            dgRemesas.DataSource = dsReporte.Tables(0)
        dgRemesas.DataBind()
        End If
    End Sub

    Private Sub dgRemesas_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgRemesas.ItemDataBound
        Dim dsOficina As DataSet

        If e.Item.Cells(0).Text <> "" Then
            If Len(e.Item.Cells(0).Text) <= 5 Then
                dsOficina = objPagos.ParametrosVenta(e.Item.Cells(0).Text)
                e.Item.Cells(0).Text = dsOficina.Tables(0).Rows(0).Item("BEZEI")
            End If
        End If

    End Sub
End Class
