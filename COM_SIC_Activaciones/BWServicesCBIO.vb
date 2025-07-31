Imports System
Imports System.Collections
Imports System.Configuration
Imports COM_SIC_Activaciones.ActualizarParticipante
Imports COM_SIC_Activaciones.ActualizarDatosFacturacion
Imports COM_SIC_Activaciones.ConsultaLineaCuentaWS
Imports COM_SIC_Activaciones.ActivarServiciosAdicionalesCBIORest
Imports COM_SIC_Activaciones.claro_int_consultaclienteCBIORest.consultarDatosLineaCta

Public Class BWServicesCBIO

    Dim objFileLog As New SICAR_Log
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim nameLogCBIO As String = objFileLog.Log_CrearNombreArchivo("Log_CBIO")
    Dim objRestService As New ResServiceGeneral
    Dim objLineaCuenta As New ConsultaLineaCuentaWSService

    Public Function ConsultarLineaCuentaWSCBIO(ByVal strNumeroLinea As String) As String
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIATIVA-219][INICIO ConsultarLineaCuentaWSCBIO]")
        Dim strOrigenLinea As String = String.Empty

        Try
            objLineaCuenta.Url = ConfigurationSettings.AppSettings("constUrlConsultaLineaCuenta").ToString()
            objLineaCuenta.Credentials = System.Net.CredentialCache.DefaultCredentials
            objLineaCuenta.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("strRelationPlanTimeout").ToString())

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarLineaCuentaWSCBIO][Url a Validar]", Funciones.CheckStr(objLineaCuenta.Url)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarLineaCuentaWSCBIO][TimeOut de Servicio]", Funciones.CheckStr(objLineaCuenta.Timeout)))

            Dim objConsultaLineaRequest As New consultarLineaCuentaRequest
            Dim objConsultaLineaResponse As New consultarLineaCuentaResponse

            objConsultaLineaRequest.tipoConsulta = "1"
            objConsultaLineaRequest.valorConsulta = Funciones.CheckStr(strNumeroLinea)

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarLineaCuentaWSCBIO][INPUT][tipoConsulta]", "1"))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarLineaCuentaWSCBIO][INPUT][valorConsulta]", Funciones.CheckStr(strNumeroLinea)))

            objConsultaLineaResponse = objLineaCuenta.consultarLineaCuenta(objConsultaLineaRequest)

            strOrigenLinea = Funciones.CheckStr(objConsultaLineaResponse.rptaConsulta)

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarLineaCuentaWSCBIO][OUTPUT][RespuestaConsulta]", Funciones.CheckStr(strOrigenLinea)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}---{1}", "[FIN][INICIATIVA-219][ConsultarLineaCuentaWSCBIO]", String.Empty))

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarLineaCuentaWSCBIO][Ocurrio un error con el servicio]", Funciones.CheckStr(ConfigurationSettings.AppSettings("constUrlConsultaLineaCuenta"))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][ConsultarLineaCuentaWSCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
        End Try

        Return strOrigenLinea

    End Function

    Public Function ActualizarParticipanteWSCBIO(ByVal objParticipanteRequest As ActualizarParticipanteRequest) As ActualizarParticipanteResponse
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIO][INICIATIVA-219][ActualizarParticipanteWSCBIO]")
        Dim objParticipanteResponse As New ActualizarParticipanteResponse
        Dim objResponse As Object

        Try
            Dim datosHeader As Hashtable = New Hashtable
            datosHeader.Add("idTransaccion", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            datosHeader.Add("msgId", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            datosHeader.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))
            datosHeader.Add("userId", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            datosHeader.Add("channel", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            datosHeader.Add("idApplication", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))

            Dim strActualizarParticipanteCBIO As String = "url_ActualizarParticipanteCBIO"
            Dim strIdTransaccion As String = String.Empty
            Dim strCodigoRespuesta As String = String.Empty
            Dim strMensajeRespuesta As String = String.Empty
            Dim blDataPower As Boolean = False

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarParticipanteWSCBIO][Url a Validar]", Funciones.CheckStr(ConfigurationSettings.AppSettings("url_ActualizarParticipanteCBIO"))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarParticipanteWSCBIO][TimeOut de Servicio]", Funciones.CheckStr(ConfigurationSettings.AppSettings("strRelationPlanTimeout"))))

            objResponse = objRestService.PostInvoque(strActualizarParticipanteCBIO, datosHeader, objParticipanteRequest, GetType(ActualizarParticipanteResponse), blDataPower)

            objParticipanteResponse = objResponse

            strCodigoRespuesta = Funciones.CheckStr(objParticipanteResponse.codigoRespuesta)
            strMensajeRespuesta = Funciones.CheckStr(objParticipanteResponse.mensajeRespuesta)

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarParticipanteWSCBIO][OUTPUT][strCodigoRespuesta]", Funciones.CheckStr(strCodigoRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarParticipanteWSCBIO][OUTPUT][strMensajeRespuesta]", Funciones.CheckStr(strMensajeRespuesta)))

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[FIN][INICIATIVA-219][ActualizarParticipanteWSCBIO]", String.Empty))

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarParticipanteWSCBIO][Ocurrio un error con el servicio]", Funciones.CheckStr(ConfigurationSettings.AppSettings("url_ActualizarParticipanteCBIO"))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][ActualizarParticipanteWSCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
        End Try

        Return objParticipanteResponse

    End Function

    Public Function ActualizarDatosFacturacionWSCBIO(ByVal objDatosFacturacionRequest As ActualizarDatosFacturacionRequest) As ActualizarDatosFacturacionResponse
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIO][INICIATIVA-219][ActualizarDatosFacturacionWSCBIO]")
        Dim objDatosFacturacionResponse As New ActualizarDatosFacturacionResponse
        Dim objResponse As Object

        Try
            Dim datosHeader As Hashtable = New Hashtable
            datosHeader.Add("idTransaccion", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            datosHeader.Add("msgId", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            datosHeader.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))
            datosHeader.Add("userId", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            datosHeader.Add("channel", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            datosHeader.Add("idApplication", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))

            Dim strActualizarDatosFacturacionCBIO As String = "url_ActualizarDatosFacturacionCBIO"
            Dim strIdTransaccion As String = String.Empty
            Dim strCodigoRespuesta As String = String.Empty
            Dim strMensajeRespuesta As String = String.Empty
            Dim blDataPower As Boolean = False

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarDatosFacturacionWSCBIO][Url a Validar]", Funciones.CheckStr(ConfigurationSettings.AppSettings("url_ActualizarDatosFacturacionCBIO"))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarDatosFacturacionWSCBIO][TimeOut de Servicio]", Funciones.CheckStr(ConfigurationSettings.AppSettings("strRelationPlanTimeout"))))

            objResponse = objRestService.PostInvoque(strActualizarDatosFacturacionCBIO, datosHeader, objDatosFacturacionRequest, GetType(ActualizarDatosFacturacionResponse), blDataPower)

            objDatosFacturacionResponse = objResponse

            strIdTransaccion = Funciones.CheckStr(objDatosFacturacionResponse.responseAudit.idTransaccion)
            strCodigoRespuesta = Funciones.CheckStr(objDatosFacturacionResponse.responseAudit.codigoRespuesta)
            strMensajeRespuesta = Funciones.CheckStr(objDatosFacturacionResponse.responseAudit.mensajeRespuesta)

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarDatosFacturacionWSCBIO][OUTPUT][strIdTransaccion]", Funciones.CheckStr(strIdTransaccion)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarDatosFacturacionWSCBIO][OUTPUT][strCodigoRespuesta]", Funciones.CheckStr(strCodigoRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarDatosFacturacionWSCBIO][OUTPUT][strMensajeRespuesta]", Funciones.CheckStr(strMensajeRespuesta)))

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[FIN][INICIATIVA-219][ActualizarDatosFacturacionWSCBIO]", String.Empty))

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarDatosFacturacionWSCBIO][Ocurrio un error con el servicio]", Funciones.CheckStr(ConfigurationSettings.AppSettings("url_ActualizarDatosFacturacionCBIO"))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][ActualizarDatosFacturacionWSCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
        End Try

        Return objDatosFacturacionResponse

    End Function

    Public Function CambioPlanWSCBIO(ByVal objRequestPlanCBIO As RequestMigracionPlan) As ResponseMigracionPlan
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIO][INICIATIVA-219][CambioPlanWSCBIO]")
        Dim FechaTransaccion As String = DateTime.Now.ToString("yyyyMMddHHmmss")
        Dim idTransaccion As String = String.Empty
        Dim strNumeroLinea As String = objRequestPlanCBIO.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.msisdn.ToString()
        Dim objResponsePlanCBIO As New ResponseMigracionPlan
        Dim objResponse As Object

        Try
            If (strNumeroLinea.Substring(0, 2) = "51") Then
                idTransaccion = FechaTransaccion + strNumeroLinea + "22"
            Else
                idTransaccion = FechaTransaccion + "51" + strNumeroLinea + "22"
            End If
            Dim datosHeader As Hashtable = New Hashtable
            datosHeader.Add("idTransaccion", idTransaccion)
            datosHeader.Add("msgId", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            datosHeader.Add("userId", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            datosHeader.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))

            Dim strCambiarPlanCBIO As String = "url_CambiarPlanCBIO"
            Dim strIdTransaccion As String = String.Empty
            Dim strCodigoRespuesta As String = String.Empty
            Dim strMensajeRespuesta As String = String.Empty
            Dim blDataPower As Boolean = False

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanWSCBIO][Url a Validar]", Funciones.CheckStr(ConfigurationSettings.AppSettings("url_CambiarPlanCBIO"))))

            objResponse = objRestService.PostInvoque(strCambiarPlanCBIO, datosHeader, objRequestPlanCBIO, GetType(ResponseMigracionPlan), blDataPower)

            objResponsePlanCBIO = objResponse

            strIdTransaccion = Funciones.CheckStr(objResponsePlanCBIO.programarMigracionPlanResponse.responseAudit.idTransaccion)
            strCodigoRespuesta = Funciones.CheckStr(objResponsePlanCBIO.programarMigracionPlanResponse.responseAudit.codigoRespuesta)
            strMensajeRespuesta = Funciones.CheckStr(objResponsePlanCBIO.programarMigracionPlanResponse.responseAudit.mensajeRespuesta)

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanWSCBIO][OUTPUT][strIdTransaccion]", Funciones.CheckStr(strIdTransaccion)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanWSCBIO][OUTPUT][strCodigoRespuesta]", Funciones.CheckStr(strCodigoRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanWSCBIO][OUTPUT][strMensajeRespuesta]", Funciones.CheckStr(strMensajeRespuesta)))

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[FIN][INICIATIVA-219][CambioPlanWSCBIO]")

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanWSCBIO][Ocurrio un error con el servicio]", Funciones.CheckStr(ConfigurationSettings.AppSettings("url_CambiarPlanCBIO"))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][CambioPlanWSCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
            Throw ex
        End Try

        Return objResponsePlanCBIO

    End Function

    Public Function ConsultarDatosLineaCtaWSCBIO(ByVal objRequestDatosLineaCta As RequestConsultarDatosLineaCta) As ResponseConsultarDatosLineaCta
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[INICIO][INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO]", String.Empty))
        Dim objResponseDatosLineaCta As ResponseConsultarDatosLineaCta = New ResponseConsultarDatosLineaCta
        Dim objResponse As Object
        Dim strUrlWS As String = "UrlConsultarDatosLineaCtaWS"
        Dim strTimeOutWS As String = "TimeOutWService"
        Dim strIdTransaccion As String = String.Empty
        Dim strCodigoRespuesta As String = String.Empty
        Dim strMensajeRespuesta As String = String.Empty
        Dim blDataPower As Boolean = False
        Dim datosHeader As Hashtable = New Hashtable
        Try
            'objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][strCsCode]", Funciones.CheckStr(objRequestDatosLineaCta.consultarDatosLineaCtaRequest.csCode)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][strNumeroLinea]", Funciones.CheckStr(objRequestDatosLineaCta.consultarDatosLineaCtaRequest.dirNum)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} : {1} | {2} : {3}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][NameKeyWS]", strUrlWS, "[ValorKeyWS]", Funciones.CheckStr(ConfigurationSettings.AppSettings("UrlConsultarDatosLineaCtaWS"))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} : {1} | {2} : {3}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][NameTimeOutWS]", strTimeOutWS, "[ValorTimeOutWS]", Funciones.CheckStr(ConfigurationSettings.AppSettings("TimeOutWService"))))
            datosHeader.Add("idTransaccion", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            datosHeader.Add("msgId", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            datosHeader.Add("userId", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            datosHeader.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))

            objResponse = objRestService.PostInvoque(strUrlWS, datosHeader, objRequestDatosLineaCta, GetType(ResponseConsultarDatosLineaCta), blDataPower)
            objResponseDatosLineaCta = objResponse
            strIdTransaccion = Funciones.CheckStr(objResponseDatosLineaCta.consultarDatosLineaCtaResponse.responseAudit.idTransaccion)
            strCodigoRespuesta = Funciones.CheckStr(objResponseDatosLineaCta.consultarDatosLineaCtaResponse.responseAudit.codigoRespuesta)
            strMensajeRespuesta = Funciones.CheckStr(objResponseDatosLineaCta.consultarDatosLineaCtaResponse.responseAudit.mensajeRespuesta)
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][OUTPUT][strIdTransaccion]", Funciones.CheckStr(strIdTransaccion)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][OUTPUT][strCodigoRespuesta]", Funciones.CheckStr(strCodigoRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][OUTPUT][strMensajeRespuesta]", Funciones.CheckStr(strMensajeRespuesta)))
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][Ocurrio un error con el servicio]", Funciones.CheckStr(ConfigurationSettings.AppSettings("UrlConsultarDatosLineaCtaWS"))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
        End Try
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[FIN][INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO]", String.Empty))
        Return objResponseDatosLineaCta
    End Function

    Public Function ActivarServiciosAdicionalesWSCBIO(ByVal objServicioAdicionalRequest As ActivarServiciosAdicionalesRequest) As ActivarServiciosAdicionalesResponse
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIO][INICIATIVA-219][ActivarServiciosAdicionalesWSCBIO]")
        Dim objServicioAdicionalResponse As New ActivarServiciosAdicionalesResponse
        Dim objResponse As Object
        Dim strActivarServiciosAdicionalesCBIO As String = "url_ActivarServiciosAdicionalesCBIO"
        Dim strOrderId As String = String.Empty
        Dim strCodigoRespuesta As String = String.Empty
        Dim strMensajeRespuesta As String = String.Empty
        Dim blDataPower As Boolean = False
        Dim datosHeader As Hashtable = New Hashtable
        Try
            datosHeader.Add("idTransaccion", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            datosHeader.Add("msgId", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            datosHeader.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))
            datosHeader.Add("userId", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesWSCBIO][Url a Validar]", Funciones.CheckStr(ConfigurationSettings.AppSettings("url_ActivarServiciosAdicionalesCBIO"))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesWSCBIO][TimeOut de Servicio]", Funciones.CheckStr(ConfigurationSettings.AppSettings("TimeOutWService"))))

            objResponse = objRestService.PostInvoque(strActivarServiciosAdicionalesCBIO, datosHeader, objServicioAdicionalRequest, GetType(ActivarServiciosAdicionalesResponse), blDataPower)
            objServicioAdicionalResponse = objResponse
            strOrderId = Funciones.CheckStr(objServicioAdicionalResponse.orderID)
            strCodigoRespuesta = Funciones.CheckStr(objServicioAdicionalResponse.codigoRespuesta)
            strMensajeRespuesta = Funciones.CheckStr(objServicioAdicionalResponse.mensajeRespuesta)
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesWSCBIO][OUTPUT][strOrderId]", Funciones.CheckStr(strOrderId)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesWSCBIO][OUTPUT][strCodigoRespuesta]", Funciones.CheckStr(strCodigoRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesWSCBIO][OUTPUT][strMensajeRespuesta]", Funciones.CheckStr(strMensajeRespuesta)))
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesWSCBIO][Ocurrio un error con el servicio]", Funciones.CheckStr(ConfigurationSettings.AppSettings("url_ActivarServiciosAdicionalesCBIO"))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][ActivarServiciosAdicionalesWSCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
        End Try
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[FIN][INICIATIVA-219][ActivarServiciosAdicionalesWSCBIO]")
        Return objServicioAdicionalResponse
    End Function

End Class
