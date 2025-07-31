Imports System
Imports System.Collections
Imports System.Configuration

Public Class BWEncolarTransaccionesCBIO
    Public objFileLog As New SICAR_Log
    Public nameFile As String = "LogCBIO"
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public nameLogCBIO As String = objFileLog.Log_CrearNombreArchivo("LogCBIO")
    Public strIdentifyLog As String = ""

    'INI: INICIATIVA-219
    Public Function EncolarTransaccionCBIO(ByVal objRequest As Object, ByVal objAuditoriaRequest As AuditoriaEWS)

        Dim objReposicion As Object
        Dim objEncolarTransaccionCBIORequest As EncolarTransaccionRequest = CType(objRequest, EncolarTransaccionRequest)
        Dim objEncolarTransaccionCBIOResponse As New EncolarTransaccionResponse

        objReposicion = EncolarTransaccionResponse(objEncolarTransaccionCBIORequest, GetType(EncolarTransaccionResponse), "EncolarReposicionRest", objAuditoriaRequest)
        objEncolarTransaccionCBIOResponse = objReposicion

        Return objEncolarTransaccionCBIOResponse

    End Function


    Public Function EncolarTransaccionResponse(ByVal objEncolarReposicionRequest As EncolarTransaccionRequest, ByVal objEncolarReposicionResponse As Type, ByVal rutaOSB As String, ByVal objAuditoriaRequest As AuditoriaEWS)
        Dim paramHeaders As Hashtable = New Hashtable

        paramHeaders.Add("idTransaccion", objAuditoriaRequest.IDTRANSACCION)
        paramHeaders.Add("msgId", objAuditoriaRequest.msgId)
        paramHeaders.Add("userId", objAuditoriaRequest.userId)
        paramHeaders.Add("timestamp", objAuditoriaRequest.timestamp)

        Dim blDataPower As Boolean
        blDataPower = False
        Dim ResServiceGeneral As New ResServiceGeneral
        Return ResServiceGeneral.PostInvoque(rutaOSB, paramHeaders, objEncolarReposicionRequest, objEncolarReposicionResponse, blDataPower)
    End Function
	
	Public Function EncolarTransaccionCBIO2(ByVal objRequest As Object, ByVal objAuditoriaRequest As AuditoriaEWS)
        Dim objReposicion As Object
        Dim objEncolarTransaccionCBIORequest As EncolarTransaccionRequest = CType(objRequest, EncolarTransaccionRequest)
        Dim objEncolarTransaccionCBIOResponse As New EncolarTransaccionResponse
        objReposicion = EncolarTransaccionResponse2(objEncolarTransaccionCBIORequest, GetType(EncolarTransaccionResponse), "EncolarReposicionRest", objAuditoriaRequest)
        objEncolarTransaccionCBIOResponse = objReposicion
        Return objEncolarTransaccionCBIOResponse
    End Function


    Public Function EncolarTransaccionResponse2(ByVal objEncolarReposicionRequest As EncolarTransaccionRequest, ByVal objEncolarReposicionResponse As Type, ByVal rutaOSB As String, ByVal objAuditoriaRequest As AuditoriaEWS)
        Dim paramHeaders As Hashtable = New Hashtable
        paramHeaders.Add("idTransaccion", objAuditoriaRequest.IDTRANSACCION)
        paramHeaders.Add("msgId", objAuditoriaRequest.msgId)
        paramHeaders.Add("userId", objAuditoriaRequest.userId)
        paramHeaders.Add("timestamp", objAuditoriaRequest.timestamp)
        Dim blDataPower As Boolean
        blDataPower = False
        Dim ResServiceGeneral As New ResServiceGeneral
        Return ResServiceGeneral.PostInvoque2(rutaOSB, paramHeaders, objEncolarReposicionRequest, objEncolarReposicionResponse, blDataPower)
    End Function
    'FIN: INICIATIVA-219
End Class
