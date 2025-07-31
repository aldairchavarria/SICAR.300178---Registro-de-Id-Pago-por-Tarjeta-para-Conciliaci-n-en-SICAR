Imports SisCajas.Funciones
Imports COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
'INICIATIVA 936 INICIO YGP
Public Class SICAR_Forma_TPago_Excel
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgFormaPago As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strIdFormaPago As String = String.Empty

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")

        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            strIdFormaPago = Session("ALMACEN") & "-" & Session("USUARIO") & " --- "

            If Not Page.IsPostBack Then
                Llenar_grid()

                Dim strFechaArchivo As String = String.Format("{0}{1}{2}", Format(Now.Day, "00"), Format(Now.Month, "00"), Format(Now.Year, "0000"))
                Dim strHoraArchivo As String = String.Format("{0}{1}{2}", Format(TimeOfDay().Hour, "00"), Format(TimeOfDay().Minute, "00"), Format(TimeOfDay().Second, "00"))
                Dim strNombreArchivo As String = String.Format("RepFormasPago_{0}{1}", strFechaArchivo, strHoraArchivo)

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_936", "Archivo excel" & strNombreArchivo))

                Response.AddHeader("Content-Disposition", "attachment;filename=" + strNombreArchivo + ".xls")
                Response.ContentType = "application/vnd.ms-excel"
            End If
        End If
    End Sub

    Private Sub Llenar_grid()
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}", strIdFormaPago, "LOG_INICIATIVA_936", "INICIO Llenar_grid()"))
        Dim strMensaje As String = "Ha ocurrido un error al exportar los registros en excel. Intentelo nuevamente."

        Try
            Dim listaFormaPagos As COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.listaFormaPagoType()
            listaFormaPagos = Session("listaFormaPagos")
            Dim intCantidadReg As Integer = Funciones.CheckInt(listaFormaPagos.Length)
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdFormaPago, "LOG_INICIATIVA_936", "Cantidad Registros", intCantidadReg))
            dgFormaPago.DataSource = listaFormaPagos
            dgFormaPago.DataBind()
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdFormaPago, "LOG_INICIATIVA_936", "ERROR - Llenar_grid() - Mensaje ", ex.Message.ToString()))
            Response.Write("<script language=jscript> alert('" + strMensaje + "'); </script>")
        End Try
    End Sub

End Class
