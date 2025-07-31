Imports System
Imports System.Collections
Imports System.Configuration

Public Class BWDesactivacionContratoCBIO
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = "Log_CBIO"
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

    Dim oDesactivacionContratoCBIO As New DesactivacionContratoCBIOWS.DesactivacionContratoCBIOWSService

    'INI: INICIATIVA-219
    Public Sub New()

        oDesactivacionContratoCBIO.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConstUrlDesactivacionContratoCBIOWS"))
        oDesactivacionContratoCBIO.Credentials = System.Net.CredentialCache.DefaultCredentials
        oDesactivacionContratoCBIO.Timeout = ConfigurationSettings.AppSettings("ConstTimeOutDesactivacionContratoCBIOWS")

    End Sub

    Public Function DesactivacionContratoCBIO(ByVal oContratoCBIO As BEContratoCBIO, ByRef strCodRespuesta As String, ByRef strMsjRespuesta As String)
        Dim objRequest As New DesactivacionContratoCBIOWS.desactivarContratoWSRequest
        Dim objResponse As New DesactivacionContratoCBIOWS.desactivarContratoWSResponse

        Dim idLog As String = Funciones.CheckStr(oContratoCBIO.co_id)

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, "[INICIATIVA-489][INICIO DesactivacionContratoCBIOWS]")
            objRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            objRequest.usuario_aplicacion = oContratoCBIO.usuario_aplicacion
            objRequest.usuario_sistema = oContratoCBIO.usuario_sistema
            objRequest.password_usuario = Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))

            objRequest.cs_id = Funciones.CheckInt64(oContratoCBIO.cs_id)
            objRequest.co_id = Funciones.CheckInt64(oContratoCBIO.co_id)
            objRequest.msisdn = oContratoCBIO.msisdn
            objRequest.motivo = Funciones.CheckInt64(oContratoCBIO.motivo)
            objRequest.p_observaciones = oContratoCBIO.p_observaciones
            objRequest.flag_occ_apadece = oContratoCBIO.flag_occ_apadece
            objRequest.flag_ND_PCS = oContratoCBIO.flag_ND_PCS
            objRequest.NDArea = oContratoCBIO.NDArea
            objRequest.NDMotivo = oContratoCBIO.NDMotivo
            objRequest.NDSubMotivo = oContratoCBIO.NDSubMotivo
            objRequest.CacDac = oContratoCBIO.CacDac
            objRequest.cicloFact = oContratoCBIO.cicloFact
            objRequest.idTipoCliente = oContratoCBIO.idTipoCliente
            objRequest.numDoc = oContratoCBIO.numDoc
            objRequest.clienteCta = oContratoCBIO.clienteCta
            objRequest.montoPCS = Funciones.CheckDbl(oContratoCBIO.montoPCS)
            objRequest.mto_fidelizacion = Funciones.CheckDbl(oContratoCBIO.mto_fidelizacion)
            objRequest.fecha_ejecucion = oContratoCBIO.fecha_ejecucion
            objRequest.trace = oContratoCBIO.trace
            objRequest.fecha_actual = oContratoCBIO.fecha_actual
            objRequest.poId = oContratoCBIO.poId
            objRequest.billCycleId = oContratoCBIO.billCycleId
            objRequest.cuentaAsesor = oContratoCBIO.cuentaAsesor
            objRequest.nombresCliente = oContratoCBIO.nombresCliente
            objRequest.apellidosCliente = oContratoCBIO.apellidosCliente
            objRequest.emailCliente = oContratoCBIO.emailCliente
            objRequest.coIdPub = oContratoCBIO.coIdPub
            objRequest.csIdPub = oContratoCBIO.csIdPub

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[DesactivacionContratoCBIO.Url]", Funciones.CheckStr(ConfigurationSettings.AppSettings("ConstUrlDesactivacionContratoCBIOWS"))))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.idTransaccion]", objRequest.idTransaccion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.usuario_aplicacion]", objRequest.usuario_aplicacion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.usuario_sistema]", objRequest.usuario_sistema))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.cs_id]", Funciones.CheckStr(objRequest.cs_id)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.co_id]", Funciones.CheckStr(objRequest.co_id)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.msisdn]", objRequest.msisdn))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.motivo]", Funciones.CheckStr(objRequest.motivo)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.p_observaciones]", objRequest.p_observaciones))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.flag_occ_apadece]", objRequest.flag_occ_apadece))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.flag_ND_PCS]", objRequest.flag_ND_PCS))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.NDArea]", objRequest.NDArea))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.NDMotivo]", objRequest.NDMotivo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.NDSubMotivo]", objRequest.NDSubMotivo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.CacDac]", objRequest.CacDac))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.cicloFact]", objRequest.cicloFact))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.idTipoCliente]", objRequest.idTipoCliente))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.numDoc]", objRequest.numDoc))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.idTransaccion]", objRequest.idTransaccion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.clienteCta]", objRequest.clienteCta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.montoPCS]", Funciones.CheckStr(objRequest.montoPCS)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.mto_fidelizacion]", Funciones.CheckStr(objRequest.mto_fidelizacion)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.fecha_ejecucion]", objRequest.fecha_ejecucion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.trace]", objRequest.trace))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.fecha_actual]", objRequest.fecha_actual))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.billCycleId]", objRequest.billCycleId))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.cuentaAsesor]", objRequest.cuentaAsesor))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.nombresCliente]", objRequest.nombresCliente))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.apellidosCliente]", objRequest.apellidosCliente))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.emailCliente]", objRequest.emailCliente))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.coIdPub]", objRequest.coIdPub))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Request.csIdPub]", objRequest.csIdPub))

            objResponse = oDesactivacionContratoCBIO.desactivarContratoWS(objRequest)

            strCodRespuesta = Funciones.CheckStr(objResponse.auditResponse.codigo_retorno)
            strMsjRespuesta = Funciones.CheckStr(objResponse.auditResponse.descripcion_retorno)

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Response.codigoRespuesta]", Funciones.CheckStr(objResponse.auditResponse.codigo_retorno)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-489][DesactivacionContratoCBIOWS]", "[Response.mensajeRespuesta]", Funciones.CheckStr(objResponse.auditResponse.descripcion_retorno)))

            objFileLog.Log_WriteLog(pathFile, strArchivo, "[INICIATIVA-489][FIN DesactivacionContratoCBIOWS]")

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-->{2}", "[INICIATIVA-489][ERROR DesactivacionContratoCBIOWS]", ex.Message.ToString()))
        End Try
    End Function
    'FIN: INICIATIVA-219

End Class
