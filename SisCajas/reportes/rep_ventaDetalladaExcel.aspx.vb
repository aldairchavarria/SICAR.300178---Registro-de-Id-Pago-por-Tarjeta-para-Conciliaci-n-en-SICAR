Imports System.Globalization

Public Class rep_ventaDetalladaExcel
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
            Dim strFecha As String
            Dim strUsuario As String
            Dim strIndividual As String
            Dim dsDatos As DataSet
            Dim objPagos As New SAP_SIC_Pagos.clsPagos

            strFecha = Request.Item("strFecha")
            strIndividual = Request.Item("Individual")

            If Len(Trim(strIndividual)) > 0 Then
                strUsuario = Session("USUARIO")
            Else
                strUsuario = ""
            End If

            'Fecha limite
            Dim flagSinergia As String
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim strFechaLim As String = objOffline.GetFechaOficinaSinergia(CStr(Session("ALMACEN")), flagSinergia)
            'Dim strFechaLim As String = ConfigurationSettings.AppSettings("FechaFinConsultaRFC_RepFactDet")
            Dim anio As Integer = CInt(strFechaLim.Substring(6, 4))
            Dim mes As Integer = CInt(strFechaLim.Substring(3, 2))
            Dim dia As Integer = CInt(strFechaLim.Substring(0, 2))
            Dim dFechaLim As New Date(anio, mes, dia)
            Dim dFecha As Date = DateTime.ParseExact(strFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture)

            If dFecha < dFechaLim Then
                dsDatos = objPagos.Get_CuadreCajaMaterFact(strFecha, Session("ALMACEN"), strUsuario)
            Else
                If Len(Trim(strIndividual)) > 0 Then
                    dsDatos = Session("rptFactMaterXCajero")
                Else
                    dsDatos = Session("rptFactMaterXPdv")
                End If
            End If

            dgReporte.DataSource = dsDatos.Tables(0)
            dgReporte.DataBind()
        End If
    End Sub

    Private Sub dgReporte_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgReporte.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            e.Item.Cells(1).Style("mso-number-format") = "\@"
            e.Item.Cells(3).Style("mso-number-format") = "\@"
            e.Item.Cells(4).Style("mso-number-format") = "\@"
            e.Item.Cells(7).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(8).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(9).Style("mso-number-format") = "\@"
            e.Item.Cells(10).Style("mso-number-format") = "\@"
        End If
    End Sub
End Class
