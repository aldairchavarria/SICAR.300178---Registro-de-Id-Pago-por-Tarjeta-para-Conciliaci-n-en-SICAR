
Imports System
Imports System.Collections
Imports System.Web
Imports System.Data
Imports System.Configuration
Imports System.Text


Public Class ConsultaClavesNegocio

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

    Public Sub New()
    End Sub

    Public Function ejecutarConsultaClave(ByVal auditoria As AuditoriaEWS, ByVal strCodAplicacion As String, ByVal strUsuario As String, ByVal strClave As String, ByRef strUsuarioDesencrypt As String, ByRef strClaveDesencrypt As String, ByRef mensajeError As String) As Boolean
        Dim codError As String = String.Empty
        Dim idTransaccion As String = String.Empty
        Dim flag As Boolean = False
        Try
            Dim objServicio As ConsultaClavesWS.ebsConsultaClavesService = New ConsultaClavesWS.ebsConsultaClavesService
            objServicio.Url = ConfigurationSettings.AppSettings("strURLConsultaClavesWS")
            objServicio.Credentials = System.Net.CredentialCache.DefaultCredentials
            codError = objServicio.desencriptar(idTransaccion, auditoria.IPAPLICACION, auditoria.IDTRANSACCION, auditoria.USRAPP, auditoria.APLICACION, strCodAplicacion, strUsuario, strClave, mensajeError, strUsuarioDesencrypt, strClaveDesencrypt)

            objFileLog.Log_WriteLog(pathFile, strArchivo, "" & "- " & "Metodo: ejecutarConsultaClave - objServicio.Url : " & objServicio.Url)

            If codError = "0" Then
                flag = True
            Else
                flag = False
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, "" & "- " & "Fin ejecutarConsultaClave -codError: " & codError)

        Catch e As Exception
            mensajeError = e.Message.ToString()
            codError = "-1"
            flag = False
            objFileLog.Log_WriteLog(pathFile, strArchivo, "" & "- " & "ejecutarConsultaClave- e.Message.ToString(): " & e.Message.ToString())
        End Try

        Return flag
    End Function
End Class
