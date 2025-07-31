Imports System
Imports System.Collections
Imports System.Configuration
Public Class BWVentaCuotas
    Public objFileLog As New SICAR_Log
    Public nameFile As String = "LogVentaCuota"
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public nameLogContingencia As String = objFileLog.Log_CrearNombreArchivo("LogVentaCuota")
    Public strIdentifyLog As String = String.Empty
    Dim strNombreUrl As String = String.Empty
    Dim strNombreTimeOut As String = "TimeOutVtaCuoAcc"
    Dim user_VentasAcc As String = ConfigurationSettings.AppSettings("User_VentaCuoAcc")
    Dim pass_VentasAcc As String = ConfigurationSettings.AppSettings("Password_VentaCuoAcc")

    Public Function ConsultarVentaAcc(ByVal strNroPedido As String)
        Dim objResponseMessage As New ObtenerDatosPedidoAccCuotasResponse
        objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140743 - IDEA-141192 - Venta en cuotas accesorios con cargo al recibo fijo movil][ConsultarVentaAcc][INI]", String.Empty))
        Try
            Dim objResponse As Object
            Dim ResService As New ResService
            Dim strCodRspt, strMsjRspt, strIdTransaccion As String
            Dim strNombreUrl As String = "consObtenerDatosPedidoAccCuotas"
            Dim paramHeaders As Hashtable = New Hashtable
            Dim paramQueryString As Hashtable = New Hashtable

            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140743 - IDEA-141192 - Venta en cuotas accesorios con cargo al recibo fijo movil][ConsultarVentaAcc][INPUT][strNombreUrl] ==> ", strNombreUrl))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140743 - IDEA-141192 - Venta en cuotas accesorios con cargo al recibo fijo movil][ConsultarVentaAcc][INPUT][strNombreTimeOut] ==> ", strNombreTimeOut))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140743 - IDEA-141192 - Venta en cuotas accesorios con cargo al recibo fijo movil][ConsultarVentaAcc][INPUT][user_VentasAcc] ==> ", user_VentasAcc))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140743 - IDEA-141192 - Venta en cuotas accesorios con cargo al recibo fijo movil][ConsultarVentaAcc][INPUT][pass_VentasAcc] ==> ", pass_VentasAcc))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140743 - IDEA-141192 - Venta en cuotas accesorios con cargo al recibo fijo movil][ConsultarVentaAcc][INPUT][strNroPedido] ==> ", strNroPedido))

            'Parametros para QueryString - INICIO
            paramQueryString.Add("numeroPedido", strNroPedido)
            'Parametros para QueryString - FIN

            'Parametros para Header DP - INICIO
            paramHeaders.Add("idTransaccion", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("msgId", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("userId", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            paramHeaders.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))
            paramHeaders.Add("aplicacion", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            'Parametros para Header DP - FIN

            objResponse = ResService.GetInvoque(strNombreUrl, strNombreTimeOut, paramHeaders, GetType(ObtenerDatosPedidoAccCuotasResponse), paramQueryString, user_VentasAcc, pass_VentasAcc)
            objResponseMessage = CType(objResponse, ObtenerDatosPedidoAccCuotasResponse)

            strCodRspt = objResponseMessage.MessageResponse.Body.datosPedidoResponse.responseStatus.codigoRespuesta
            strMsjRspt = objResponseMessage.MessageResponse.Body.datosPedidoResponse.responseStatus.mensajeRespuesta
            strIdTransaccion = objResponseMessage.MessageResponse.Body.datosPedidoResponse.responseStatus.idTransaccion

            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140743 - IDEA-141192 - Venta en cuotas accesorios con cargo al recibo fijo movil][ConsultarVentaAcc][OUTPUT][strCodRspt] ==> ", strCodRspt))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140743 - IDEA-141192 - Venta en cuotas accesorios con cargo al recibo fijo movil][ConsultarVentaAcc][OUTPUT][strMsjRspt] ==> ", strMsjRspt))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140743 - IDEA-141192 - Venta en cuotas accesorios con cargo al recibo fijo movil][ConsultarVentaAcc][OUTPUT][strIdTransaccion] ==> ", strIdTransaccion))

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140743 - IDEA-141192 - Venta en cuotas accesorios con cargo al recibo fijo movil][ConsultarVentaAcc][CATCH]", String.Empty))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140743 - IDEA-141192 - Venta en cuotas accesorios con cargo al recibo fijo movil][ConsultarVentaAcc][CATCH] [StackTrace]", ex.StackTrace))
            objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140743 - IDEA-141192 - Venta en cuotas accesorios con cargo al recibo fijo movil][ConsultarVentaAcc][CATCH] [Message]", ex.Message))
        End Try
        objFileLog.Log_WriteLog(pathFile, nameLogContingencia, String.Format("{0}-->{1}", "[PROY-140743 - IDEA-141192 - Venta en cuotas accesorios con cargo al recibo fijo movil][ConsultarVentaAcc][FIN]", String.Empty))
        Return objResponseMessage
    End Function

End Class
