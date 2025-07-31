Imports System
Imports System.Collections
Imports System.Configuration

Public Class BWBorrarProgramacionCBIO
    Public objFileLog As New SICAR_Log
    Public nameFile As String = "LogCBIO"
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public nameLogCBIO As String = objFileLog.Log_CrearNombreArchivo("LogCBIO")
    Public strIdentifyLog As String = ""

    'INI: INICIATIVA-219
    Public Function BorrarProgramacionWSCBIO(ByVal objRequest As MessageRequestBorrarProgramacion)

        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIO][INICIATIVA-219][BorrarProgramacionWSCBIO]", String.Empty))

        Try

            Dim strUrlWS As String = "UrlTransaccionesPostVentaServicioWS"
            Dim strTimeOutWS As String = "TimeOutTransaccionesPostVentaServicioWS"
            Dim objRequestBorrarProgramacion As MessageRequestBorrarProgramacion = CType(objRequest, MessageRequestBorrarProgramacion)
            Dim objResponseBorrarProgramacion As New MessageResponseBorrarProgramacion

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIO][INICIATIVA-219][BorrarProgramacionCBIO]")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][BorrarProgramacionCBIO][url]", Funciones.CheckStr(strUrlWS)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][BorrarProgramacionCBIO][INPUT][MSISDN]", Funciones.CheckStr(objRequestBorrarProgramacion.borrarProgracionRequest.msisdn)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][BorrarProgramacionCBIO][INPUT][SERVCESTADO]", Funciones.CheckStr(objRequestBorrarProgramacion.borrarProgracionRequest.servcEstado)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][BorrarProgramacionCBIO][INPUT][SERVICOD]", Funciones.CheckStr(objRequestBorrarProgramacion.borrarProgracionRequest.serviCod)))

            Dim objResponse As Object

            Dim paramHeaders As Hashtable = New Hashtable

            paramHeaders.Add("idTransaccion", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("msgId", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("userId", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            paramHeaders.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))

            Dim blDataPower As Boolean
            blDataPower = False
            Dim ResServiceGeneral As New ResServiceGeneral

            objResponse = ResServiceGeneral.PostInvoque(strUrlWS, paramHeaders, objRequestBorrarProgramacion, GetType(MessageResponseBorrarProgramacion), blDataPower)
            objResponseBorrarProgramacion = CType(objResponse, MessageResponseBorrarProgramacion)

            Dim strIdTransaccion As String = objResponseBorrarProgramacion.borrarProgracionResponse.responseAudit.idTransaccion
            Dim strCodigoRespuesta As String = objResponseBorrarProgramacion.borrarProgracionResponse.responseAudit.codigoRespuesta
            Dim strMensajeRespuesta As String = objResponseBorrarProgramacion.borrarProgracionResponse.responseAudit.mensajeRespuesta

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][OUTPUT][STRIDTRANSACCION]", Funciones.CheckStr(strIdTransaccion)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][OUTPUT][STRCODIGORESPUESTA]", Funciones.CheckStr(strCodigoRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][OUTPUT][STRMENSAJERESPUESTA]", Funciones.CheckStr(strMensajeRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[FIN][INICIATIVA-219][ConsultarProgramacionesCBIO]")

            Return objResponseBorrarProgramacion

        Catch ex As Exception

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIATIVA-219][BorrarProgramacionCBIO][Ocurrio un error al obtener datos borrarProgramacion")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][BorrarProgramacionCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))

        End Try

    End Function

    'FIN: INICIATIVA-219

End Class
