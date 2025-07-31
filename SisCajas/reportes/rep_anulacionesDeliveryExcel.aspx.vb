Public Class rep_anulacionesDeliveryExcel
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
            Dim strFechaIni As String = String.Empty
            Dim strFechaFin As String = String.Empty
            Dim strPDV As String = String.Empty
            Dim strMedioPago As String = String.Empty
            Dim strMonto As String = String.Empty
            Dim strNroPedido As String = String.Empty
            Dim strIdVenta As String = String.Empty

            strFechaIni = Funciones.CheckStr(Request.QueryString("strFechaIni"))
            strFechaFin = Funciones.CheckStr(Request.QueryString("strFechaFin"))
            strPDV = Funciones.CheckStr(Request.QueryString("strPDV"))
            strMedioPago = Funciones.CheckStr(Request.QueryString("strMedioPago"))

            strMonto = Funciones.CheckStr(Request.QueryString("strMonto"))
            strNroPedido = Funciones.CheckStr(Request.QueryString("strNroPedido"))
            strIdVenta = Funciones.CheckStr(Request.QueryString("strIdVenta"))


            If (strMonto = String.Empty) Then
                strMonto = 0
            End If

            If (strNroPedido = String.Empty) Then
                strNroPedido = 0
            End If


            If Not Page.IsPostBack Then
                Llenar_grid(strFechaIni, strFechaFin, strPDV, strMedioPago, strMonto, strNroPedido, strIdVenta)
            End If

            Dim strFechaArchivo As String = String.Format("{0}{1}{2}", Format(Now.Day, "00"), Format(Now.Month, "00"), Format(Now.Year, "0000"))
            Dim strHoraArchivo As String = String.Format("{0}{1}{2}", TimeOfDay().Hour, TimeOfDay().Minute, TimeOfDay().Second)
            Dim strNombreArchivo As String = String.Format("RepAnulacionesDelivery_{0}{1}", strFechaArchivo, strHoraArchivo)

            Response.AddHeader("Content-Disposition", "attachment;filename=" + strNombreArchivo + ".xls")
            Response.ContentType = "application/vnd.ms-excel"

        End If

    End Sub

    Private Sub Llenar_grid(ByVal strFechaIni As String, ByVal strFechaFin As String, _
                            ByVal strPDV As String, ByVal strMedioPago As String, _
                            ByVal strMonto As String, ByVal strNroPedido As String, _
                            ByVal strIdVenta As String)

        Dim objRecordSet As DataSet
        Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim strCod As String = String.Empty
        Dim msj As String = String.Empty
        Dim i As Integer
        Dim strMensaje As String = "Ha ocurrio un error al exportar los registros en excel. Inténtelo nuevamente."
        Try
            objRecordSet = objConsultaMsSap.ConsultarReporteAnulacionDelivery(strFechaIni, strFechaFin, strPDV, strMedioPago, Int64.Parse(strNroPedido), Decimal.Parse(strMonto), strIdVenta, "", "")

            If Not objRecordSet Is Nothing Then
                dgReporte.DataSource = objRecordSet
                dgReporte.DataBind()
            Else
                Response.Write("<script language=jscript> alert('" + strMensaje + "'); </script>")
            End If

        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + strMensaje + "'); </script>")
        End Try
    End Sub

End Class
