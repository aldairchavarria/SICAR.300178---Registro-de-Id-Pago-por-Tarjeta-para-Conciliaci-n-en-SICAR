Imports System
Imports System.Collections
Imports System.Configuration

Public Class BWCancelarOrdenTOA
    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Public strIdentifyLog As String = ""


    Public Function CancelarOrdenTOA(ByVal objRequest As Object, ByVal objAuditoriaRequest As AuditoriaEWS)

        Dim oCancelarToa As Object
        Dim objReqCanOrdenTOA As CancelarOrdenRequest = CType(objRequest, CancelarOrdenRequest)
        Dim objRespCanOrdenTOA As New CancelarOrdenResponse

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY-140662 BWCancelarOrdenTOA - Inicio CancelarOrdenResponse")

        oCancelarToa = CancOrdenTOA(objReqCanOrdenTOA, GetType(CancelarOrdenResponse), "ConsUrlCancelarOrdenTOA", objAuditoriaRequest)
        objRespCanOrdenTOA = oCancelarToa
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY-140662 BWCancelarOrdenTOA - Fin CancelarOrdenResponse")

        Return objRespCanOrdenTOA

    End Function


    Public Function CancOrdenTOA(ByVal objReqCanOrdenTOA As CancelarOrdenRequest, ByVal objRespCanOrdenTOA As Type, ByVal rutaCOTOA As String, ByVal objAuditoriaRequest As AuditoriaEWS)
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
        Return ResService.PostInvoque(rutaCOTOA, paramHeaders, objReqCanOrdenTOA, objRespCanOrdenTOA)
    End Function

End Class
