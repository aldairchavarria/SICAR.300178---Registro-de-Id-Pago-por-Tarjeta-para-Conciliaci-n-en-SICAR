Imports System.IO
Imports COM_SIC_Log4Net
Public Class SrvPago_Log
    Public Function Log_CrearNombreArchivo(ByVal strTransaccion As String) As String
        Dim strFecha = Date.Now.ToString("yyyyMMdd")
        Dim Archivo As String = ""
        Archivo = strTransaccion & "_" & strFecha
        Return Archivo
    End Function

    Public Function Log_WriteLog(ByVal strLOGPATH As String, ByVal strNombreArchivoLog As String, ByVal strTexto As String) As String
        Dim strFecha = Date.Now.ToString("yyyy-MM-dd-hh:mm:ss")
        Dim vTexto As String = ""
        Dim Resul As String = ""
        Try
            NetLogger.EscribirLog(NetLogger.NivelLog.Aplicacion, strNombreArchivoLog, vTexto & strFecha & " -" & strTexto) 'INI PROY-140126
        Catch ex As Exception
            Resul = ex.Message
            NetLogger.EscribirLog(NetLogger.NivelLog.Aplicacion, strNombreArchivoLog, ex.Message)
        End Try
        Return Resul
    End Function
    Public Function EscribirLog(ByVal strLOGPATH As String, ByVal strNombreArchivoLog As String, ByVal idLog As String, ByVal ParamArray args() As Object) As String
        Dim strFecha = Date.Now.ToString("yyyy-MM-dd-hh:mm:ss")
        Dim vTexto As String = ""
        Dim Resul As String = ""
        Try
            Dim strTexto As String
            strTexto &= idLog & " - "
            For Each item As Object In args
                strTexto &= "[" & Funciones.CheckStr2(item) & "]"
            Next
            NetLogger.EscribirLog(NetLogger.NivelLog.Aplicacion, strNombreArchivoLog.Trim(), vTexto & strFecha & " " & strTexto)
        Catch ex As Exception
            Resul = ex.Message
            NetLogger.EscribirLog(NetLogger.NivelLog.Aplicacion, strNombreArchivoLog, ex.Message) 'FIN PROY-140126
        End Try
        Return Resul
    End Function
End Class
