Imports System.IO
Imports COM_SIC_Log4Net 'PROY-140126
Public Class SICAR_Log


    Public Function Log_CrearNombreArchivo(ByVal strTransaccion As String) As String
        Dim strFecha = Date.Now.ToString("yyyyMMdd")
        Dim Archivo As String = ""
        Archivo = strTransaccion & "_" & strFecha
        Return Archivo
    End Function

    Public Function Log_WriteLog(ByVal strLOGPATH As String, ByVal strNombreArchivoLog As String, ByVal strTexto As String) As String
        Dim objFSO As Scripting.FileSystemObject
        Dim objFile0 As Scripting.TextStream
        Dim Archivo As String
        Dim strFecha = Date.Now.ToString("yyyy-MM-dd-hh:mm:ss")
        Dim vTexto As String = ""
        Dim Resul As String = ""
        Try
            NetLogger.EscribirLog(NetLogger.NivelLog.Aplicacion, strNombreArchivoLog.Trim(), vTexto & strFecha & " -" & strTexto)
        Catch ex As Exception
            Resul = ex.Message
            NetLogger.EscribirLog(NetLogger.NivelLog.Aplicacion, strNombreArchivoLog.Trim(), ex.Message)
        End Try
        Return Resul
    End Function
End Class
