Public Class impRecBusiness
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Public strDescRedondeoSolicitado As String
    Public strMontoFavor As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        End If

        strMontoFavor = Funciones.CheckStr(Math.Abs(Funciones.CheckDbl(Request.Item("strMontoFavor"))))
        If Funciones.CheckDbl(Request.Item("strMontoFavor")) < 0 Then
            strDescRedondeoSolicitado = ObtenerDescParametro(System.Configuration.ConfigurationSettings.AppSettings("constGrupoMsgTicket"), System.Configuration.ConfigurationSettings.AppSettings("constCodParamImportePendiente"))
        Else
            strDescRedondeoSolicitado = ObtenerDescParametro(System.Configuration.ConfigurationSettings.AppSettings("constGrupoMsgTicket"), System.Configuration.ConfigurationSettings.AppSettings("constCodParamPagoACuenta"))
        End If
    End Sub

    Public Function ObtenerDescParametro(ByVal strGrupo As String, ByVal strCodigo As String) As String

        Dim strDescParametro As String
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_Parametros"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecFijoPaginas")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = strGrupo & "|" & strCodigo
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ObtenerDescParametro.")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP strGrupo  " & strGrupo)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP strCodigo:  " & strCodigo)

        Try
            Dim dsParam As DataSet

            Dim objSicarDB As New COM_SIC_Cajas.clsCajas
            dsParam = objSicarDB.FP_ConsultaParametros(strGrupo)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT dsParam.Tables(0).Rows.Count:  " & dsParam.Tables(0).Rows.Count)
            If Not IsNothing(dsParam) AndAlso dsParam.Tables(0).Rows.Count > 0 Then
                For idx As Integer = 0 To dsParam.Tables(0).Rows.Count - 1
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT SPARN_CODIGO:  " & Funciones.CheckStr(dsParam.Tables(0).Rows(idx).Item("SPARN_CODIGO")))
                    If Funciones.CheckStr(dsParam.Tables(0).Rows(idx).Item("SPARN_CODIGO")).Equals(strCodigo) Then
                        strDescParametro = Funciones.CheckStr(dsParam.Tables(0).Rows(idx).Item("SPARV_DESCRIPCION"))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT SPARV_DESCRIPCION:  " & Funciones.CheckStr(dsParam.Tables(0).Rows(idx).Item("SPARV_DESCRIPCION")))
                        Exit For
                    End If
                Next
            End If
            objSicarDB = Nothing
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     ERROR:" & ex.Message.ToString())
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ObtenerDescParametro.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")

        End Try
        Return strDescParametro
    End Function

End Class
