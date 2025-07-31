Imports System.Collections
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Configuration
Imports System.Text
Imports System.Data
Imports System.Net

Public Class BWBonosServicios

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)


    Public Sub New()
    End Sub

    Public Function insertarBonosMantener(ByVal headerDP As BEHeaderDataPower, ByVal auditoria As AuditoriaEWS, ByVal codSolicitud As String, ByVal flagRegistraBono As String, ByVal fechaProgramacion As DateTime, ByRef mensajeError As String) As String
        Dim codErrorDatapower As String = ""
        Dim mensajeErrorDatapower As String = ""
        Dim codError As String = ""
        Try
            Dim objServicio As ServiciosBonosWS.ServiciosyBonosMigrarWSService = New ServiciosBonosWS.ServiciosyBonosMigrarWSService
            objServicio.Url = ConfigurationSettings.AppSettings("gConstWSServiciosyBonosMigrar")
            objServicio.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim objReq As ServiciosBonosWS.registrarBonosMantenerRequest = New ServiciosBonosWS.registrarBonosMantenerRequest
            Dim objRes As ServiciosBonosWS.registrarBonosMantenerResponse = New ServiciosBonosWS.registrarBonosMantenerResponse
            Dim objSecurity As ServiciosBonosWS.SecurityType = New ServiciosBonosWS.SecurityType
            Dim objToken As ServiciosBonosWS.UsernameTokenType = New ServiciosBonosWS.UsernameTokenType

            objFileLog.Log_WriteLog(pathFile, strArchivo, codSolicitud & "- " & "Metodo: insertarBonosMantener - objServicio.Url : " & objServicio.Url)
            'Desencripta usuario y clave

            Dim objClave As ConsultaClavesNegocio = New ConsultaClavesNegocio

            Dim codAplicacionClave As String = ConfigurationSettings.AppSettings("strAplicacionSISACT")
            Dim usuarioEncrypt As String = ConfigurationSettings.AppSettings("strUserWSTransaccionMigracion")
            Dim claveEncrypt As String = ConfigurationSettings.AppSettings("strPassWSTransaccionMigracion")
            Dim usuarioDesencrypt As String = String.Empty
            Dim claveDesencrypt As String = String.Empty

            objClave.ejecutarConsultaClave(auditoria, codAplicacionClave, usuarioEncrypt, claveEncrypt, usuarioDesencrypt, claveDesencrypt, mensajeError)
            objToken.Username = usuarioDesencrypt
            objToken.Password = claveDesencrypt
            objSecurity.UsernameToken = objToken
            objServicio.Security = objSecurity

           
            Dim objHeader As ServiciosBonosWS.HeaderRequestType = New ServiciosBonosWS.HeaderRequestType
            objHeader.country = headerDP.country
            objHeader.language = headerDP.language
            objHeader.consumer = headerDP.consumer
            objHeader.system = headerDP.system
            objHeader.modulo = headerDP.modulo
            objHeader.pid = headerDP.pid
            objHeader.userId = headerDP.userId
            objHeader.dispositivo = headerDP.dispositivo
            objHeader.wsIp = headerDP.wsIp
            objHeader.operation = headerDP.operation
            objHeader.timestamp = headerDP.timestamp
            objHeader.msgType = headerDP.msgType
            objServicio.HeaderRequest = objHeader
            Dim objAud As ServiciosBonosWS.AuditRequestType = New ServiciosBonosWS.AuditRequestType
            Dim objOpcional As ServiciosBonosWS.RequestOpcionalTypeRequestOpcional() = New ServiciosBonosWS.RequestOpcionalTypeRequestOpcional(0) {}
            Dim objOpcional2 As ServiciosBonosWS.RequestOpcionalTypeRequestOpcional = New ServiciosBonosWS.RequestOpcionalTypeRequestOpcional
            objOpcional2.campo = ""
            objOpcional2.valor = ""
            objOpcional(0) = objOpcional2
            objAud.idTransaccion = auditoria.IDTRANSACCION
            objAud.ipAplicacion = auditoria.IPAPLICACION
            objAud.nombreAplicacion = auditoria.APLICACION
            objAud.usuarioAplicacion = auditoria.USRAPP
            objReq.auditRequest = objAud
            objReq.fechaProgramacion = fechaProgramacion
            objReq.flagRegistraBono = flagRegistraBono
            objReq.codSolicitud = codSolicitud
            objReq.listaRequestOpcional = objOpcional

            Dim objBSCS As ServiciosBonosWS.BonoBscsType() = New ServiciosBonosWS.BonoBscsType(0) {}
            objReq.BonosBscs = objBSCS

            Try
                objRes = objServicio.registrarBonosMantener(objReq)

                codErrorDatapower = objServicio.HeaderResponse.Status.code
                mensajeErrorDatapower = objServicio.HeaderResponse.Status.message

                objFileLog.Log_WriteLog(pathFile, strArchivo, codSolicitud & "- " & "Respuesta Data Power : " & "")
                objFileLog.Log_WriteLog(pathFile, strArchivo, codSolicitud & "- " & "codErrorDatapower : " & codErrorDatapower)
                objFileLog.Log_WriteLog(pathFile, strArchivo, codSolicitud & "- " & "codErrorDatapower : " & mensajeErrorDatapower)
            Catch ex As Exception
                mensajeError = ex.Message.ToString()
                objFileLog.Log_WriteLog(pathFile, strArchivo, codSolicitud & "- " & "ex.Message.ToString(): " & ex.Message.ToString())
                codErrorDatapower = "-1"
                mensajeErrorDatapower = "Ocurrió un error al consultar el servicio con Datapower"
            End Try

            If codErrorDatapower = "0" Then
                codError = objRes.auditResponse.codigoRespuesta
                mensajeError = objRes.auditResponse.mensajeRespuesta

                objFileLog.Log_WriteLog(pathFile, strArchivo, codSolicitud & "- " & "Respuesta Servicio TransaccionMigracion : " & String.Format("  codError:{0}-mensajeError:{1}", codError, mensajeError))

            End If
        Catch e As Exception
            mensajeError = e.Message.ToString()
            objFileLog.Log_WriteLog(pathFile, strArchivo, codSolicitud & "- " & "e.Message.ToString(): " & e.Message.ToString())
            codError = "-1"
        End Try
        objFileLog.Log_WriteLog(pathFile, strArchivo, codSolicitud & "- " & "FIN metodo insertarBonosMantener: " & "")
        Return codError
    End Function
End Class
