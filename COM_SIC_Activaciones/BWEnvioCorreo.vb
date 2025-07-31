Imports System.Configuration

Public Class BWEnvioCorreo
    Private oEnvioCorreo As New EnviarCorreoWS.envioCorreoWSService

    Public Sub New()
        oEnvioCorreo.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConsEnvioCorreoWS"))
        oEnvioCorreo.Credentials = System.Net.CredentialCache.DefaultCredentials
        oEnvioCorreo.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("ConsEnvioCorreoWSTimeOut"))
    End Sub


    Public Function EnviarCorreoWS(ByVal _idTransaccion As String, ByVal _ipAplicacion As String, _
    ByVal _nombreAplicacion As String, ByVal _usuarioAplicacion As String, _
    ByVal _NroSec As String, ByVal _MsgService As String, ByRef _MensajeRpta As String) As String

        Dim strRespuesta As String = ""

        Dim objRequest As New EnviarCorreoWS.AuditTypeRequest
        Dim objResponse As New EnviarCorreoWS.AuditTypeResponse
        Dim oAuditRequest As New EnviarCorreoWS.AuditTypeRequest


        Try
            Dim strObservacion As String = ""
            Dim strRemitente As String = "", strDestinatario As String = ""
            Dim strAsunto As String = "", strCuerpo As String = ""
            Dim strDestinoEmail As String = ""
            Dim strDestinatarioCC As String = ""

            strObservacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConsPortaObservacionesSP"))

            strRemitente = Funciones.CheckStr(ConfigurationSettings.AppSettings("SP_Remitente"))
            strDestinatario = Funciones.CheckStr(ConfigurationSettings.AppSettings("SP_ParaDefault"))
            strDestinatarioCC = Funciones.CheckStr(ConfigurationSettings.AppSettings("SP_CC"))

            strDestinoEmail = strDestinatario + ";" + strDestinatarioCC


            strAsunto = String.Format(Funciones.CheckStr(ConfigurationSettings.AppSettings("SP_Asunto")), _NroSec)
            strCuerpo = String.Format(Funciones.CheckStr(ConfigurationSettings.AppSettings("SP_Cuerpo")), _NroSec, _MsgService)

            strCuerpo = strCuerpo.Replace("(p)", "<p>") : strCuerpo = strCuerpo.Replace("(_p)", "</p>")
            strCuerpo = strCuerpo.Replace("(b)", "<b>") : strCuerpo = strCuerpo.Replace("(_b)", "</b>")



            oAuditRequest.idTransaccion = _idTransaccion
            oAuditRequest.ipAplicacion = _ipAplicacion
            oAuditRequest.codigoAplicacion = _nombreAplicacion
            oAuditRequest.usrAplicacion = _usuarioAplicacion



            objResponse = oEnvioCorreo.enviarCorreo(objRequest, strRemitente, strDestinoEmail, strAsunto, strCuerpo, "1", Nothing, Nothing)


            strRespuesta = objResponse.codigoRespuesta

            _MensajeRpta = objResponse.mensajeRespuesta


        Catch ex As Exception
            strRespuesta = ex.Message
        Finally
        End Try
        Return strRespuesta

    End Function

    'PROY-24724-IDEA-28174 - KASS :: INI
    Public Function EnviarCorreoPM(ByVal strRemitente As String, ByVal strDestinatario As String, ByRef strAsunto As String, _
                                   ByVal strMensaje As String, ByVal strflag As String, ByVal strIP As String, _
                                   ByVal strUsuario As String, ByVal strCodRespuesta As String, _
                                   ByVal strMgsRespuesta As String) As String

        Dim objAuditRequest As New EnviarCorreoWS.AuditTypeRequest
        Dim objAuditResponse As New EnviarCorreoWS.AuditTypeResponse

        objAuditRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff")
        objAuditRequest.ipAplicacion = strIP
        objAuditRequest.codigoAplicacion = ConfigurationSettings.AppSettings("CodAplicacion")
        objAuditRequest.usrAplicacion = strUsuario

        oEnvioCorreo.enviarCorreo(objAuditRequest, strRemitente, strDestinatario, strAsunto, strMensaje, strflag, Nothing, Nothing)

        strCodRespuesta = objAuditResponse.codigoRespuesta
        strMgsRespuesta = objAuditResponse.mensajeRespuesta

    End Function
    'PROY-24724-IDEA-28174 - KASS :: FIN
    'INI PROY-140336
    Public Function EnviarCorreoCP(ByVal strRemitente As String, ByVal strDestinatario As String, ByVal strAsunto As String, _
                                   ByVal strMensaje As String, ByVal strflag As String, ByVal strIP As String, _
                                   ByVal strUsuario As String, ByRef strCodRespuesta As String, _
                                   ByRef strMgsRespuesta As String) As String

        Dim objAuditRequest As New EnviarCorreoWS.AuditTypeRequest
        Dim objAuditResponse As New EnviarCorreoWS.AuditTypeResponse

        objAuditRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff")
        objAuditRequest.ipAplicacion = strIP
        objAuditRequest.codigoAplicacion = ConfigurationSettings.AppSettings("CodAplicacion")
        objAuditRequest.usrAplicacion = strUsuario

        oEnvioCorreo.enviarCorreo(objAuditRequest, strRemitente, strDestinatario, strAsunto, strMensaje, strflag, Nothing, Nothing)

        strCodRespuesta = objAuditResponse.codigoRespuesta
        strMgsRespuesta = objAuditResponse.mensajeRespuesta

    End Function
    'FIN PROY-140336
End Class
