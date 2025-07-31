'INICIO PROY-140379
Imports System
Imports System.Collections
Imports System.Configuration

Public Class BWConsultasProcesos_IOT

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Public strIdentifyLog As String = ""


    Public Function ConsultarProcesosIOTResponse(ByVal objRequest As Object, ByVal objAuditoriaRequest As AuditoriaEWS)

        Dim oConsultar As Object
        Dim objConsultarProcesosIOTRequest As SmartWatchRequest = CType(objRequest, SmartWatchRequest)
        Dim objConsultarProcesosIOTResponse As New SmartWatchResponse

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY-140379 BWConsultasProcesos_IOT - Inicio ConsultarProcesosIOTResponse")

        oConsultar = ConsultarProcesoIOT(objConsultarProcesosIOTRequest, GetType(SmartWatchResponse), "key_consultarProcesarIOT", objAuditoriaRequest)
        objConsultarProcesosIOTResponse = oConsultar

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "codeResponse: " & objConsultarProcesosIOTResponse.MessageResponse.Body.consultarProcesarIotResponse.responseStatus.codeResponse.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "descriptionResponse: " & objConsultarProcesosIOTResponse.MessageResponse.Body.consultarProcesarIotResponse.responseStatus.descriptionResponse.ToString())

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY-140379 BWConsultasProcesos_IOT - Fin ConsultarProcesosIOTResponse")

        Return objConsultarProcesosIOTResponse

    End Function


    Public Function ConsultarProcesoIOT(ByVal objConsultarProcesosIOTRequest As SmartWatchRequest, ByVal objConsultarProcesosIOTResponse As Type, ByVal rutaOSB As String, ByVal objAuditoriaRequest As AuditoriaEWS)

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


        paramHeaders.Add("channel", objAuditoriaRequest.USRAPP)
        paramHeaders.Add("idApplication", objAuditoriaRequest.APLICACION)
        paramHeaders.Add("userApplication", objAuditoriaRequest.APLICACION)
        paramHeaders.Add("userSession", objAuditoriaRequest.USRAPP)
        paramHeaders.Add("idESBTransaction", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))
        paramHeaders.Add("startDate", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))
        paramHeaders.Add("idBusinessTransaction", DateTime.UtcNow.ToString("ddMMyyyyHHmmss") + "_" + objConsultarProcesosIOTRequest.MessageRequest.Body.consultarProcesarIotRequest.msisdnpAct)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Channel : " & objAuditoriaRequest.USRAPP)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- idApplication : " & objAuditoriaRequest.APLICACION)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- userApplication : " & objAuditoriaRequest.APLICACION)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- userSession : " & objAuditoriaRequest.USRAPP)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- idESBTransaction : " & DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- startDate : " & DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- idBusinessTransaction :" & DateTime.UtcNow.ToString("ddMMyyyyHHmmss") + "_" + objConsultarProcesosIOTRequest.MessageRequest.Body.consultarProcesarIotRequest.msisdnpAct)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY-140245 BwRegistrarCampania -PostInvoque")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "Request: (SmartWatchRequest)")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "consumer:" & objConsultarProcesosIOTRequest.MessageRequest.Header.HeaderRequest.consumer.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "country : " & objConsultarProcesosIOTRequest.MessageRequest.Header.HeaderRequest.country.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "dispositivo : " & objConsultarProcesosIOTRequest.MessageRequest.Header.HeaderRequest.dispositivo.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "language : " & objConsultarProcesosIOTRequest.MessageRequest.Header.HeaderRequest.language.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "modulo : " & objConsultarProcesosIOTRequest.MessageRequest.Header.HeaderRequest.modulo.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "msgType : " & objConsultarProcesosIOTRequest.MessageRequest.Header.HeaderRequest.msgType.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "operation : " & objConsultarProcesosIOTRequest.MessageRequest.Header.HeaderRequest.operation.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "pid : " & objConsultarProcesosIOTRequest.MessageRequest.Header.HeaderRequest.pid.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "system : " & objConsultarProcesosIOTRequest.MessageRequest.Header.HeaderRequest.system.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "timestamp : " & objConsultarProcesosIOTRequest.MessageRequest.Header.HeaderRequest.timestamp.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "userId : " & objConsultarProcesosIOTRequest.MessageRequest.Header.HeaderRequest.userId.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "wsIp : " & objConsultarProcesosIOTRequest.MessageRequest.Header.HeaderRequest.wsIp.ToString())


        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "Body : (SmartWatchRequest)")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "appIOT : " & objConsultarProcesosIOTRequest.MessageRequest.Body.consultarProcesarIotRequest.appIOT.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "IccidP : " & objConsultarProcesosIOTRequest.MessageRequest.Body.consultarProcesarIotRequest.IccidP.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "imeiP : " & objConsultarProcesosIOTRequest.MessageRequest.Body.consultarProcesarIotRequest.imeiP.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "imsiP : " & objConsultarProcesosIOTRequest.MessageRequest.Body.consultarProcesarIotRequest.imsiP.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "motivo : " & objConsultarProcesosIOTRequest.MessageRequest.Body.consultarProcesarIotRequest.motivo.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "msisdnD1 : " & objConsultarProcesosIOTRequest.MessageRequest.Body.consultarProcesarIotRequest.msisdnD1.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "msisdnD2 : " & objConsultarProcesosIOTRequest.MessageRequest.Body.consultarProcesarIotRequest.msisdnD2.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "msisdnpAct : " & objConsultarProcesosIOTRequest.MessageRequest.Body.consultarProcesarIotRequest.msisdnpAct.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "proceso : " & objConsultarProcesosIOTRequest.MessageRequest.Body.consultarProcesarIotRequest.proceso.ToString())


        Dim ResService As New ResService
        Return ResService.PostInvoque(rutaOSB, paramHeaders, objConsultarProcesosIOTRequest, objConsultarProcesosIOTResponse)
    End Function
    Public Function consultarProcesarUnico(ByVal telefono As String, ByVal StrMotivoDevolSW As String, ByVal strCurrentuser As String, ByVal strParametrosAcction As String, ByVal strSerie_entrante As String, ByVal strIccid_baja As String, ByVal strIpAplicacion As String, ByVal CurrentTerminal As String) As String
        Dim objConsultar As New COM_SIC_Activaciones.BWConsultasProcesos_IOT
        Dim objConsultarProcesosIOTRequest As New COM_SIC_Activaciones.SmartWatchRequest
        Dim objHeaderRequest As New COM_SIC_Activaciones.HeaderRequest
        Dim objauditRequest As New COM_SIC_Activaciones.BEAuditRequest
        Dim objConsultarProcesoIOT As New COM_SIC_Activaciones.BESmartWatchRequest
        Dim objlistaOpcional As New ArrayList
        Dim objBEAuditoriaREST As New COM_SIC_Activaciones.AuditoriaEWS
        Dim objConsultarProcesoIOTResponse As New COM_SIC_Activaciones.SmartWatchResponse
        Dim strCodRpt, strMsjRpt As String
        Dim strBosyRequest As String = COM_SIC_Seguridad.ReadKeySettings.Key_requestBodyIOT
        Dim strParametroBaja() As String = Funciones.CheckStr(strParametrosAcction).Split("|"c) ' //baja / Actualizacion IMEI
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ejecutarConsultarProcesarIOT")
        Dim objOpcional As BElistaOpcional
        Dim arrHeaderRequest() As String = COM_SIC_Seguridad.ReadKeySettings.Key_HeaderResquest.Split("|"c)
        Try

            objHeaderRequest.consumer = arrHeaderRequest(0)
            objHeaderRequest.country = arrHeaderRequest(1)
            objHeaderRequest.dispositivo = arrHeaderRequest(2)
            objHeaderRequest.language = arrHeaderRequest(3)
            objHeaderRequest.modulo = arrHeaderRequest(4)
            objHeaderRequest.msgType = arrHeaderRequest(5)
            objHeaderRequest.operation = COM_SIC_Seguridad.ReadKeySettings.Key_serviceNameSW.Split("|"c)(0)
            objHeaderRequest.pid = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            objHeaderRequest.system = arrHeaderRequest(4)
            objHeaderRequest.timestamp = Convert.ToDateTime(String.Format("{0:u}", DateTime.UtcNow))
            objHeaderRequest.userId = DateTime.Now.ToString("ddMMyyyy")
            objHeaderRequest.wsIp = Funciones.CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultarProcesarIOT_wsIp"))

            objConsultarProcesosIOTRequest.MessageRequest.Header.HeaderRequest = objHeaderRequest

            objConsultarProcesoIOT.appIOT = strBosyRequest.Split("|"c)(0)
            objConsultarProcesoIOT.proceso = strBosyRequest.Split("|"c)(1)
            objConsultarProcesoIOT.motivo = StrMotivoDevolSW.ToUpper()
            objConsultarProcesoIOT.msisdnpAct = telefono
            objConsultarProcesoIOT.imsiP = String.Empty
            objConsultarProcesoIOT.imeiP = strSerie_entrante
            objConsultarProcesoIOT.msisdnD1 = String.Empty
            objConsultarProcesoIOT.msisdnD2 = String.Empty
            objConsultarProcesoIOT.IccidP = String.Empty

            For i As Integer = 0 To strParametroBaja.Length - 1
                objOpcional = New BElistaOpcional
                objOpcional.campo = strParametroBaja(i).Split(";"c)(0)
                objOpcional.valor = strParametroBaja(i).Split(";"c)(1)
                objlistaOpcional.Add(objOpcional)

            Next
            objConsultarProcesoIOT.listaOpcional = objlistaOpcional

            objConsultarProcesosIOTRequest.MessageRequest.Body.consultarProcesarIotRequest = objConsultarProcesoIOT

            objBEAuditoriaREST.IDTRANSACCION = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
            objBEAuditoriaREST.IPAPLICACION = strIpAplicacion 
            objBEAuditoriaREST.idTransaccionNegocio = CurrentTerminal
            objBEAuditoriaREST.USRAPP = strCurrentuser
            objBEAuditoriaREST.applicationCodeWS = ConfigurationSettings.AppSettings("strAplicacionSISACT") 
            objBEAuditoriaREST.APLICACION = ConfigurationSettings.AppSettings("constAplicacion")
            objBEAuditoriaREST.userId = strCurrentuser
            objBEAuditoriaREST.nameRegEdit = ""

            objConsultarProcesoIOTResponse = objConsultar.ConsultarProcesosIOTResponse(objConsultarProcesosIOTRequest, objBEAuditoriaREST)
            strCodRpt = objConsultarProcesoIOTResponse.MessageResponse.Body.consultarProcesarIotResponse.responseStatus.codeResponse
            strMsjRpt = objConsultarProcesoIOTResponse.MessageResponse.Body.consultarProcesarIotResponse.responseStatus.descriptionResponse
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strCodRpt: " & strCodRpt)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strMsjRpt: " & strMsjRpt)
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "error ejecutarConsultarProcesarIOT Message : " & ex.Message)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "error ejecutarConsultarProcesarIOT Trace : " & ex.StackTrace)
            Throw New Exception(ex.Message)
        End Try
        Return String.Format("{0}|{1}", strCodRpt, strMsjRpt)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ejecutarConsultarProcesarIOT")
    End Function

End Class
'FIN PROY-140379