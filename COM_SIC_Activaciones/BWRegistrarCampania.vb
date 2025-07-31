'INC000002161718  inicio
Imports System
Imports System.Collections
Imports System.Configuration

Public Class BWRegistrarCampania

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Public strIdentifyLog As String = ""


    Public Function RegistrarCampaniasResponse(ByVal objRequest As Object, ByVal objAuditoriaRequest As AuditoriaEWS)

        Dim oCampanas As Object
        Dim objRegistrarCampaniasRequest As RegistrarCampaniasRequest = CType(objRequest, RegistrarCampaniasRequest)
        Dim objRegistrarCampaniasResponse As New RegistrarCampaniasResponse

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY-140245 BWRegistrarCampania - Inicio RegistrarCampaniasResponse")

        oCampanas = RegistrarCampania(objRegistrarCampaniasRequest, GetType(RegistrarCampaniasResponse), "RegistrarCampanaRest", objAuditoriaRequest)
        objRegistrarCampaniasResponse = oCampanas
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY-140245 BWRegistrarCampania -Fin RegistrarCampaniasResponse")

        Return objRegistrarCampaniasResponse

    End Function


    Public Function RegistrarCampania(ByVal objRegistrarCampaniasRequest As RegistrarCampaniasRequest, ByVal objRegistrarCampaniasResponse As Type, ByVal rutaOSB As String, ByVal objAuditoriaRequest As AuditoriaEWS)
        Dim paramHeaders As Hashtable = New Hashtable

        paramHeaders.Add("idTransaccion", objAuditoriaRequest.IDTRANSACCION)
        paramHeaders.Add("ipAplicacion", objAuditoriaRequest.IPAPLICACION)
        paramHeaders.Add("idTransaccionNegocio", objAuditoriaRequest.idTransaccionNegocio)
        paramHeaders.Add("usuarioAplicacion", objAuditoriaRequest.USRAPP)
        paramHeaders.Add("applicationCodeWS", objAuditoriaRequest.applicationCodeWS)
        paramHeaders.Add("applicationCode", objAuditoriaRequest.APLICACION)
        paramHeaders.Add("nombreAplicacion", objAuditoriaRequest.USRAPP)
        paramHeaders.Add("nameRegEdit", objAuditoriaRequest.USRAPP)
        paramHeaders.Add("userId", objAuditoriaRequest.userId)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY-140245 BwRegistrarCampania -PostInvoque")

        Dim ResService As New ResService
        Return ResService.PostInvoque(rutaOSB, paramHeaders, objRegistrarCampaniasRequest, objRegistrarCampaniasResponse)
    End Function

End Class
'INC000002161718 fin