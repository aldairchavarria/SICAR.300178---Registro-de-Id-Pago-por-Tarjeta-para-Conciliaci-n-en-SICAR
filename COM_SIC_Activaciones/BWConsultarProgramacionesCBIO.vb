Imports System
Imports System.Collections
Imports System.Configuration

Public Class BWConsultarProgramacionesCBIO
    Public objFileLog As New SICAR_Log
    Public nameFile As String = "LogCBIO"
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public nameLogCBIO As String = objFileLog.Log_CrearNombreArchivo("LogCBIO")
    Public strIdentifyLog As String = ""


    'INI: INICIATIVA-219
    Public Function ConsultarProgramacionesWSCBIO(ByVal objRequest As MessageRequestConsultarProgramaciones)

        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIO][INICIATIVA-219][ConsultarProgramacionesCBIO]", String.Empty))

        Try
            Dim strUrlWS As String = "UrlConsultarProgramacionesWS"
            Dim strTimeOutWS As String = "TimeOutConsultarProgramacionesWS"
            Dim objRequestProgramacion As MessageRequestConsultarProgramaciones = CType(objRequest, MessageRequestConsultarProgramaciones)
            Dim objResponseProgramaciones As New MessageResponseConsultarProgramaciones

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][URL WS]", Funciones.CheckStr(ConfigurationSettings.AppSettings(strUrlWS))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][TIMEOUT WS]", Funciones.CheckStr(ConfigurationSettings.AppSettings(strTimeOutWS))))

            Dim objResponse As Object

            Dim paramHeaders As Hashtable = New Hashtable

            paramHeaders.Add("idTransaccion", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("msgId", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("userId", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            paramHeaders.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))

            Dim blDataPower As Boolean
            blDataPower = False
            Dim ResServiceGeneral As New ResServiceGeneral

            objResponse = ResServiceGeneral.PostInvoque(strUrlWS, paramHeaders, objRequestProgramacion, GetType(MessageResponseConsultarProgramaciones), blDataPower)
            objResponseProgramaciones = CType(objResponse, MessageResponseConsultarProgramaciones)

            Dim strIdTransaccion As String = objResponseProgramaciones.consultarProgramacionesResponse.responseAudit.idTransaccion
            Dim strCodigoRespuesta As String = objResponseProgramaciones.consultarProgramacionesResponse.responseAudit.codigoRespuesta
            Dim strMensajeRespuesta As String = objResponseProgramaciones.consultarProgramacionesResponse.responseAudit.mensajeRespuesta

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][OUTPUT][STRIDTRANSACCION]", Funciones.CheckStr(strIdTransaccion)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][OUTPUT][STRCODIGORESPUESTA]", Funciones.CheckStr(strCodigoRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][OUTPUT][STRMENSAJERESPUESTA]", Funciones.CheckStr(strMensajeRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[FIN][INICIATIVA-219][ConsultarProgramacionesCBIO]")

            Return objResponseProgramaciones

        Catch ex As Exception

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIATIVA-219][ConsultarProgramacionesCBIO][Ocurrio un error al obtener datos consultarProgramacion")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))

        End Try

    End Function

    'FIN: INICIATIVA-219

End Class
