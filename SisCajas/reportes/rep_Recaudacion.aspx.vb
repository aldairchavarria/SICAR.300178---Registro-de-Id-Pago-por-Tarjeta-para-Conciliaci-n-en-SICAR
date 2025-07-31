Imports System.Data

Public Class rep_Recaudacion
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Protected WithEvents dgRecaudacion As System.Web.UI.WebControls.DataGrid
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Public identificadorLog As String


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else

            If Not Page.IsPostBack Then
                Try
                    identificadorLog = Session("ALMACEN") & "-" & Session("USUARIO") & " --- "
                    objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "Inicio Reporte de Recaudaciones Procesadas" & " ---")
                    CargarDatos()
                    objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "Fin Reporte de Recaudaciones Procesadas" & " ---")
                    Response.AddHeader("Content-Disposition", "attachment;filename=" & Session("USUARIO") & "_" & Request.QueryString("Fecha") & "_ReporteTransacciones.xls")
                    Response.ContentType = "application/vnd.ms-excel"

                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "Error Metodo : CargarDatos()" & ex.Message.ToString())
                End Try
            End If

        End If
    End Sub
    Private Sub CargarDatos()

        Dim strTipoPago As String = Request.QueryString("TipoPago")
        Dim strFechaConsulta As String = Request.QueryString("Fecha")
        Dim strOficina As String = Session("ALMACEN")
        objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "strTipoPago : " & strTipoPago)
        objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "strFechaConsulta :" & strFechaConsulta)

        Dim StrTipo As String = "00"

        If strTipoPago = "00" Then
            StrTipo = "T" 'Todos
        ElseIf strTipoPago = "01" Then
            StrTipo = "M" 'Movil
        ElseIf strTipoPago = "02" Then
            StrTipo = "F" ' Fijo y Paginas
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "StrTipo : " & StrTipo)
        objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "strOficina : " & strOficina)
        objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "Usuario : " & Session("USUARIO"))

        Dim objOffline As New COM_SIC_OffLine.clsOffline

        Dim dsResult As DataSet = objOffline.GetPoolRecaudacion(strFechaConsulta, strOficina, StrTipo, Session("USUARIO"))

        objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "Numero de Registros : " & dsResult.Tables(0).Rows.Count())
        objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "Oicina de Venta : " & Session("OFICINA"))

        Dim dtRecaudaciones As New DataTable
        dtRecaudaciones = dsResult.Tables(0)

        For Each dr As DataRow In dtRecaudaciones.Rows

            dr("OFICINA_VENTA") = dr("OFICINA_VENTA") & " - " & Session("OFICINA")

        Next

        dtRecaudaciones.AcceptChanges()
        dgRecaudacion.DataSource = dsResult.Tables(0)
        dgRecaudacion.DataBind()



    End Sub

End Class
