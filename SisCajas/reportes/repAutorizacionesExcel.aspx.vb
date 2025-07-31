Public Class repAutorizacionesExcel
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgReporte As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim objSAP As New COM_SIC_OffLine.clsOffline
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
            Dim strFechaIni As String
            Dim strFechaFin As String
            Dim strCodPdv As String
            Dim strCAC As String


            Dim dsData As DataSet
            Dim objConf As New COM_SIC_Configura.clsConfigura

            strCAC = Request.Item("Adm")  'Si lo consulta un administrador, de lo contrario es supervisor de Caja
            strFechaIni = Request.Item("FechaIni")
            strFechaFin = Request.Item("FechaFin")

            If strCAC <> "" Then
                strCodPdv = Session("ALMACEN")
            Else
                strCodPdv = ""
            End If

            dsData = objConf.FP_Lista_Rep_Autorizaciones(strCodPdv, strFechaIni, strFechaFin)
            dgReporte.DataSource = dsData.Tables(0)

        dgReporte.DataBind()
        End If
    End Sub

    Private Sub dgReporte_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgReporte.ItemDataBound
        Dim dsOficina As DataSet

        If e.Item.Cells(0).Text <> "" Then
            If Len(e.Item.Cells(0).Text) <= 5 Then
                dsOficina = objSAP.ParametrosVenta(e.Item.Cells(0).Text)
                e.Item.Cells(0).Text = dsOficina.Tables(0).Rows(0).Item("BEZEI")
            End If
        End If
    End Sub
End Class
