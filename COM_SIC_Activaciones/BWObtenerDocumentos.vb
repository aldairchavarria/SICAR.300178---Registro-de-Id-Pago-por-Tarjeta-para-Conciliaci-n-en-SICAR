Imports System
Imports System.Collections
Imports System.Configuration

Public Class BWObtenerDocumentos
    Public objFileLog As New SICAR_Log
    Public nameFile As String = "LogCBIO"
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public nameLog As String = objFileLog.Log_CrearNombreArchivo("LogObtenerDocumentosOpsitel")
    Public strIdentifyLog As String = ""

    Public Function ObtenerDocumentosWS(ByVal objRequest As ObtenerDocumentosRequest)

        Dim objResponseMessage As New ObtenerDocumentosResponse

        objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[INICIO][PROY-140623 IDEA-142200 Nuevo formato contratos Osiptel][ObtenerDocumentosWS]", String.Empty))
        Try
            Dim strUrlWS As String = "PostContratoOsiptel"
            Dim objRequestMessage As ObtenerDocumentosRequest = CType(objRequest, ObtenerDocumentosRequest)

            objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[PROY-140623 IDEA-142200 Nuevo formato contratos Osiptel][URL WS]", strUrlWS))

            Dim objResponse As Object

            Dim paramHeaders As Hashtable = New Hashtable
            Dim user_ContratoOsiptel As String
            Dim pass_ContratoOsiptel As String
            Dim TimeOut_ContratoOsiptel As String
            user_ContratoOsiptel = ConfigurationSettings.AppSettings("user_ContratoOsiptel")
            pass_ContratoOsiptel = ConfigurationSettings.AppSettings("pass_ContratoOsiptel")
            TimeOut_ContratoOsiptel = ConfigurationSettings.AppSettings("TimeOut_ContratoOsiptel")

            objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[PROY-140623 IDEA-142200 Nuevo formato contratos Osiptel][user_ContratoOsiptel]", user_ContratoOsiptel))
            objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[PROY-140623 IDEA-142200 Nuevo formato contratos Osiptel][pass_ContratoOsiptel]", pass_ContratoOsiptel))
            objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[PROY-140623 IDEA-142200 Nuevo formato contratos Osiptel][TimeOut_ContratoOsiptel]", TimeOut_ContratoOsiptel))


            paramHeaders.Add("idTransaccion", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("msgId", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("userId", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            paramHeaders.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))
            paramHeaders.Add("aplicacion", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))

            Dim ResService As New ResService

            objResponse = ResService.PostInvoque2(strUrlWS, paramHeaders, objRequestMessage, user_ContratoOsiptel, pass_ContratoOsiptel, TimeOut_ContratoOsiptel, GetType(ObtenerDocumentosResponse))
            objResponseMessage = CType(objResponse, ObtenerDocumentosResponse)

            Dim strIdTransaccion As String = objResponseMessage.MessageResponse.Body.obtenerDocumentoResponse.auditResponse.idTransaccion
            Dim strCodigoRespuesta As String = objResponseMessage.MessageResponse.Body.obtenerDocumentoResponse.auditResponse.codigoRespuesta
            Dim strMensajeRespuesta As String = objResponseMessage.MessageResponse.Body.obtenerDocumentoResponse.auditResponse.mensajeRespuesta

            objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[PROY-140623 IDEA-142200 Nuevo formato contratos Osiptel][strIdTransaccion]", strIdTransaccion))
            objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[PROY-140623 IDEA-142200 Nuevo formato contratos Osiptel][strCodigoRespuesta]", strCodigoRespuesta))
            objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[PROY-140623 IDEA-142200 Nuevo formato contratos Osiptel][strMensajeRespuesta]", strMensajeRespuesta))

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[PROY-140623 IDEA-142200 Nuevo formato contratos Osiptel][Error]", String.Empty))
            objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[ERROR]", ex.Message))
            objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[ERROR]", ex.StackTrace))
        End Try
        objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[FIN][PROY-140623 IDEA-142200 Nuevo formato contratos Osiptel][ObtenerDocumentosWS]", String.Empty))

        Return objResponseMessage

    End Function
End Class
