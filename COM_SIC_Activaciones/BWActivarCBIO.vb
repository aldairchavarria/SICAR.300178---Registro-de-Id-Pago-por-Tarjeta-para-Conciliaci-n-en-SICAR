Imports System
Imports System.Collections
Imports System.Configuration

Public Class BWActivarCBIO

    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = "Log_CBIO"
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

    Private objActivarCBIO As New WSActivacionPostPagoCBIO.ActivacionPostpagoCBIOWSService

    'INI: INICIATIVA-219
    Public Sub New()

        objActivarCBIO.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConstUrlActivarCBIOWS"))
        objActivarCBIO.Credentials = System.Net.CredentialCache.DefaultCredentials
        objActivarCBIO.Timeout = ConfigurationSettings.AppSettings("ConstTimeOutActivarCBIOWS")

    End Sub

    Public Function ActivarCBIO(ByVal strDocSap As String, ByVal nroAcuerdo As String, ByVal codTipoProducto As String, ByVal CurrentTerminal As String, ByVal CurrentUser As String, ByRef strCodRespuesta As String, ByRef strMsjRespuesta As String)
        Dim objRequest As New WSActivacionPostPagoCBIO.ejecutarActivacionRequest
        Dim objResponse As New WSActivacionPostPagoCBIO.ejecutarActivacionResponse

        objRequest.auditRequest = New WSActivacionPostPagoCBIO.auditRequestType

        Dim idLog As String = Funciones.CheckStr(strDocSap)

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, "[INICIATIVA-219][INICIO WSActivacionPostPagoCBIO]")
            objRequest.auditRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            objRequest.auditRequest.ipAplicacion = Funciones.CheckStr(CurrentTerminal)
            objRequest.auditRequest.nombreAplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            objRequest.auditRequest.usuarioAplicacion = Funciones.CheckStr(CurrentUser)

            objRequest.numeroAcuerdo = Funciones.CheckStr(nroAcuerdo)
            objRequest.tipoProducto = Funciones.CheckStr(codTipoProducto)

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-219][WSActivacionPostPagoCBIO]", "[Request.idTransaccion]", objRequest.auditRequest.idTransaccion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-219][WSActivacionPostPagoCBIO]", "[Request.ipAplicacion]", objRequest.auditRequest.ipAplicacion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-219][WSActivacionPostPagoCBIO]", "[Request.nombreAplicacion]", objRequest.auditRequest.nombreAplicacion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-219][WSActivacionPostPagoCBIO]", "[Request.usuarioAplicacion]", objRequest.auditRequest.usuarioAplicacion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-219][WSActivacionPostPagoCBIO]", "[Request.numeroAcuerdo]", objRequest.numeroAcuerdo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-219][WSActivacionPostPagoCBIO]", "[Request.tipoProducto]", objRequest.tipoProducto))

            objResponse = objActivarCBIO.ejecutarActivacion(objRequest)

            strCodRespuesta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
            strMsjRespuesta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-219][WSActivacionPostPagoCBIO]", "[Response.codigoRespuesta]", Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}{1}-->{2}", "[INICIATIVA-219][WSActivacionPostPagoCBIO]", "[Response.mensajeRespuesta]", Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)))

            objFileLog.Log_WriteLog(pathFile, strArchivo, "[INICIATIVA-219][FIN WSActivacionPostPagoCBIO]")

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-->{1}", "[INICIATIVA-219][ERROR WSActivarCBIO]", ex.Message.ToString()))
        End Try
    End Function
    'FIN: INICIATIVA-219

End Class
