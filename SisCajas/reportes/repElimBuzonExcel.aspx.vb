Public Class repElimBuzonExcel
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgElimin As System.Web.UI.WebControls.DataGrid

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
            Dim strOficina As String
            Dim strFechaIni As String
            Dim strFechaFin As String

            Dim dsElimBuzon As DataSet
            Dim objCaja As New COM_SIC_Cajas.clsCajas

            strOficina = Request.Item("strOficina")
            strFechaIni = Request.Item("strFechaIni")
            strFechaFin = Request.Item("strFechaFin")

            'dsElimBuzon = objCaja.FP_ListaElimBuzon(strOficina, CDate(strFechaIni), CDate(strFechaFin))
            dsElimBuzon = objCaja.FP_ListaElimBuzon(strOficina, strFechaIni, strFechaFin)

            dgElimin.DataSource = dsElimBuzon.Tables(0)

            dgElimin.DataBind()
        End If
    End Sub

    Private Sub dgElimin_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgElimin.ItemDataBound
        Dim dsOficina As DataSet

        If e.Item.Cells(0).Text <> "" Then
            If Len(e.Item.Cells(0).Text) <= 5 Then
                dsOficina = objPagos.ParametrosVenta(e.Item.Cells(0).Text)
                e.Item.Cells(0).Text = dsOficina.Tables(0).Rows(0).Item("BEZEI")
            End If
        End If
    End Sub
End Class
