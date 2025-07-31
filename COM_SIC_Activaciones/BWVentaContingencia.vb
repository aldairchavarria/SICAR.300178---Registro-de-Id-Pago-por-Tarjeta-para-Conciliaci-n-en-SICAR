Imports System
Imports System.Collections
Imports System.Configuration


Public Class BWVentaContingencia
    Public objFileLog As New SICAR_Log
    Public nameFile As String = "LogVentaContingencia"
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public nameLogContingencia As String = objFileLog.Log_CrearNombreArchivo("LogVentaContingencia")
    Public strIdentifyLog As String = String.Empty
    Dim strNombreUrl As String = String.Empty
    Dim strNombreTimeOut As String = "Time_Out_VentasContingencia"
    Dim user_VentasContingencia As String = ConfigurationSettings.AppSettings("User_VentasContingencia")
    Dim pass_VentasContingencia As String = ConfigurationSettings.AppSettings("Pass_VentasContingencia")

    Public Function RegistrarVentaContingencia(ByVal objRequest As RegistrarVentaContRequest, ByVal strUsuario As String)
        Dim objResponseMessage As New RegistrarVentaContResponse
        objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][INICIO] [RegistrarVentaContingencia]", String.Empty))

        Try
            Dim objResponse As Object
            Dim ResService As New ResService
            Dim strCodRspt, strMsjRspt, strIdTransaccion As String
            strNombreUrl = "consUrlRegistrarVentasCtg"
            Dim strNombreTimOut As String = ConfigurationSettings.AppSettings(strNombreTimeOut)
            Dim paramHeaders As Hashtable = New Hashtable
            Dim objRequestMessage As RegistrarVentaContRequest = CType(objRequest, RegistrarVentaContRequest)

            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][INPUT][strNombreUrl] ==> ", strNombreUrl))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][INPUT][strNombreTimeOut] ==> ", strNombreTimeOut))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][INPUT][user_VentasContingencia] ==> ", user_VentasContingencia))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][INPUT][pass_VentasContingencia] ==> ", pass_VentasContingencia))

            paramHeaders.Add("idTransaccion", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("msgId", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("userId", Funciones.CheckStr(strUsuario))
            paramHeaders.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))
            paramHeaders.Add("aplicacion", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))

            objResponse = ResService.PostInvoque2(strNombreUrl, paramHeaders, objRequestMessage, user_VentasContingencia, pass_VentasContingencia, strNombreTimOut, GetType(RegistrarVentaContResponse))
            objResponseMessage = CType(objResponse, RegistrarVentaContResponse)

            strCodRspt = objResponseMessage.MessageResponse.Body.registrarVentaContingenciaResponse.auditResponse.codigoRespuesta
            strMsjRspt = objResponseMessage.MessageResponse.Body.registrarVentaContingenciaResponse.auditResponse.mensajeRespuesta
            strIdTransaccion = objResponseMessage.MessageResponse.Body.registrarVentaContingenciaResponse.auditResponse.idTransaccion

            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][RegistrarVentaContingencia][OUTPUT][strCodRspt] ==> ", strCodRspt))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][RegistrarVentaContingencia][OUTPUT][strMsjRspt] ==> ", strMsjRspt))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][RegistrarVentaContingencia][OUTPUT][strIdTransaccion] ==> ", strIdTransaccion))

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][RegistrarVentaContingencia][CATCH]", String.Empty))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][RegistrarVentaContingencia][CATCH] [StackTrace]", ex.StackTrace))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][RegistrarVentaContingencia][CATCH] [Message]", ex.Message))
        End Try
        objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][FIN] [RegistrarVentaContingencia]", String.Empty))
        Return objResponseMessage
    End Function

    Public Function ConsultarVentaContingencia(ByVal strNroPedido As String)
        Dim objResponseMessage As New ConsultarVentaContResponse

        Try
            Dim objResponse As Object
            Dim ResService As New ResService
            Dim strCodRspt, strMsjRspt, strIdTransaccion As String
            Dim strNombreUrl As String = "consUrlConsultarVentasCtg"
            Dim paramHeaders As Hashtable = New Hashtable
            Dim paramQueryString As Hashtable = New Hashtable

            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][INPUT][strNombreUrl] ==> ", strNombreUrl))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][INPUT][strNombreTimeOut] ==> ", strNombreTimeOut))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][INPUT][user_VentasContingencia] ==> ", user_VentasContingencia))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][INPUT][pass_VentasContingencia] ==> ", pass_VentasContingencia))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][INPUT][strNroPedido] ==> ", strNroPedido))

            'Parametros para QueryString - INICIO
            paramQueryString.Add("numPedido", strNroPedido)
            'Parametros para QueryString - FIN

            'Parametros para Header DP - INICIO
            paramHeaders.Add("idTransaccion", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("msgId", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("userId", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            paramHeaders.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))
            paramHeaders.Add("aplicacion", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            'Parametros para Header DP - FIN

            objResponse = ResService.GetInvoque(strNombreUrl, strNombreTimeOut, paramHeaders, GetType(ConsultarVentaContResponse), paramQueryString, user_VentasContingencia, pass_VentasContingencia)
            objResponseMessage = CType(objResponse, ConsultarVentaContResponse)

            strCodRspt = objResponseMessage.MessageResponse.Body.consultarVentasContingenciaResponse.auditResponse.codigoRespuesta
            strMsjRspt = objResponseMessage.MessageResponse.Body.consultarVentasContingenciaResponse.auditResponse.mensajeRespuesta
            strIdTransaccion = objResponseMessage.MessageResponse.Body.consultarVentasContingenciaResponse.auditResponse.idTransaccion

            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][OUTPUT][strCodRspt] ==> ", strCodRspt))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][OUTPUT][strMsjRspt] ==> ", strMsjRspt))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][OUTPUT][strIdTransaccion] ==> ", strIdTransaccion))

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][CATCH]", String.Empty))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][CATCH] [StackTrace]", ex.StackTrace))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][CATCH] [Message]", ex.Message))
        End Try
        objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][ConsultarVentasContingencia][FIN]", String.Empty))
        Return objResponseMessage
    End Function

End Class
