'PROY-26963 INI
Imports System.Configuration

Public Class BWRegistraPorta

    Private oRegistraPorta As New RegistraPortaWS.RegistraPortaWSService


    Public Sub New()

        oRegistraPorta.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConstUrlRegistraPortaWS"))
        oRegistraPorta.Credentials = System.Net.CredentialCache.DefaultCredentials
        oRegistraPorta.Timeout = ConfigurationSettings.AppSettings("ConstTimeOutRegistraPortaWS")

    End Sub

    Public Function ProcesarSolicitud(ByVal _objAudit As ItemGenerico, ByVal _NroPedido As String, ByVal _SolinCodigo As String, ByRef _CodigoError As String, ByRef _MensajeError As String)

        Dim objRequest As New RegistraPortaWS.procesarSolicitudPortabilidadRequest
        Dim objResponse As New RegistraPortaWS.procesarSolicitudPortabilidadResponse
        Dim oAuditRequest As New RegistraPortaWS.auditRequestType

        Try

            oAuditRequest.ipAplicacion = _objAudit.CODIGO
            oAuditRequest.nombreAplicacion = _objAudit.CODIGO2
            oAuditRequest.usuarioAplicacion = _objAudit.CODIGO3
            oAuditRequest.idTransaccion = _objAudit.DESCRIPCION

            objRequest.auditRequest = oAuditRequest

            objRequest.nroPedido = _NroPedido
            objRequest.solinCodigo = _SolinCodigo

            'INI: PROY-140262 BLACKOUT
            If (_objAudit.CODIGO4.Equals("1")) Then                
                _CodigoError = "1"
                _MensajeError = _objAudit.DESCRIPCION4
            Else
            objResponse = oRegistraPorta.procesarSolicitudPortabilidad(objRequest)
            _CodigoError = objResponse.auditResponse.codigoRespuesta
            _MensajeError = objResponse.auditResponse.mensajeRespuesta
            End If
            'FIN: PROY-140262 BLACKOUT
        Catch ex As Exception
            _CodigoError = 99
            _MensajeError = ex.Message
        Finally

        End Try
    End Function

End Class
'PROY-26963 FIN