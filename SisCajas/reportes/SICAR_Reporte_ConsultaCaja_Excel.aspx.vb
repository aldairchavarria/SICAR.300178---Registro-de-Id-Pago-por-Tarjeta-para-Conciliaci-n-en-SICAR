Imports SisCajas.GenFunctions
Imports SisCajas.clsAudi
Imports COM_SIC_INActChip
Imports System.IO
Imports COM_SIC_Activaciones
Imports SisCajas.Funciones
Imports SisCajas.clsActivaciones
Imports System.Text
Imports System.Net
Imports System.Globalization
Imports COM_SIC_Cajas
Imports COM_SIC_FacturaElectronica
Imports NEGOCIO_SIC_SANS
Imports System.Xml
Imports System.Configuration

Public Class SICAR_Reporte_ConsultaCaja_Excel
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DGListaInd As System.Web.UI.WebControls.DataGrid
    Protected WithEvents DGListaGen As System.Web.UI.WebControls.DataGrid

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
    'INI-936 - JH - Variables nuevas
    Dim dtResult As DataTable
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = "Log_ConsultaIndividual"
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogConsultaCuadre")
    Dim strIdentifyLog As String = String.Empty
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
#End Region

    'INI-936 - CNSO - Modificado para no recibir parametros por querystring y obtener la informacion a exportar de sesion 
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then 'INI-936 - JH - Se reestructuro toda la condicion IF
                strIdentifyLog = Session("strUsuario")
                Dim strOrigen As String = Request.QueryString("origen")
                Dim strFechaArchivo As String = String.Format("{0}{1}{2}", Format(Now.Day, "00"), Format(Now.Month, "00"), Format(Now.Year, "0000")) 'INI-936 - JH
                Dim strHoraArchivo As String = String.Format("{0}{1}{2}", Format(TimeOfDay().Hour, "00"), Format(TimeOfDay().Minute, "00"), Format(TimeOfDay().Second, "00")) 'INI-936 - JH
                Dim strNombreArchivo As String = ""
                Dim strTipoReporte As String = ""
                  
                If strOrigen = "I" Then
                    strTipoReporte = "ConsultaCajaIndvidual"
                    llena_grid("ExportarExcel_ConsultaCajaInd", DGListaInd)
                ElseIf strOrigen = "G" Then
                    strTipoReporte = "ConsultaCajaGeneral"
                    llena_grid("ExportarConsultaCajaGen", DGListaGen)
                End If

                nameFile = "Log_" & strTipoReporte

                strNombreArchivo = String.Format("{0}_{1}{2}", strTipoReporte, strFechaArchivo, strHoraArchivo)
                Response.AddHeader("Content-Disposition", "attachment;filename=" + strNombreArchivo + ".xls")
                Response.ContentType = "application/vnd.ms-excel"

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Archivo exportado --> " & strNombreArchivo)
            End If
         End If
    End Sub

    'INI-936 - JH - Metodo nuevo que reemplaza a los dos anteriores ("llena_grid_ind" y "llena_grid_gen")
    Private Sub llena_grid(ByVal strSession As String, ByVal dgLista As DataGrid)
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Inicio --> Metodo: llena_grid()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- parametro: strSession --> " & strSession)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- parametro: dgLista --> " & dgLista.ID)

            dtResult = Session(strSession)

            dgLista.Visible = True
            dgLista.DataSource = dtResult
            dgLista.DataBind()

            If dtResult.Rows.Count <= 0 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Mensaje: No se encontraron datos")
                Response.Write("<script language=jscript> alert('No se encontró datos'); </script>")
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- DataTable: dtResult --> " & dtResult.Rows.Count)
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Error: --> " & ex.Message.ToString())
            Response.Write("<script language=jscript> alert('" + ex.Message.ToString() + "'); </script>")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Fin --> Metodo: llena_grid()")
        End Try
    End Sub
End Class
