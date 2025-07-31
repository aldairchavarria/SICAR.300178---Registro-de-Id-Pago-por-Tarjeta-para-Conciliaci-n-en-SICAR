Imports System.EnterpriseServices
Imports System.Configuration
Public Class clsMigracionSaldoGSMWS

    Public Function RegistrarMigracionSaldo(ByVal vTransaccion As String, _
                                             ByVal vIPAplicacion As String, _
                                             ByVal vAplicacion As String, _
                                             ByVal vUsrAplicacion As String, _
                                             ByVal vTelefono As String) As clsDatosInteraccion


        Dim blnRespuesta As Boolean = False
        Dim strCodigoRespuesta, strIdTransaccion, strMensajeRespuesta As String

        Dim objMigracionSaldoGSMWS As New MigracionSaldoGSMWS.ebsMigracionSaldoGSMService
        objMigracionSaldoGSMWS.Url = ConfigurationSettings.AppSettings("consRutaWSMigracionSaldoGSMWS").ToString()
        objMigracionSaldoGSMWS.Credentials = System.Net.CredentialCache.DefaultCredentials
        objMigracionSaldoGSMWS.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("ConstTimeOutMigracionSaldoGSM").ToString())
        Dim oDatosInteraccion As New clsDatosInteraccion

        Dim objRequestMigracionSaldoGSM As New MigracionSaldoGSMWS.transferirSaldoRequest
        Dim objRequestAudit As New MigracionSaldoGSMWS.auditType
        Dim objResponseMigracionSaldoGSM As New MigracionSaldoGSMWS.transferirSaldoResponse


        objRequestAudit.idTransaccion = vTransaccion
        objRequestAudit.ipAplicacion = vIPAplicacion
        objRequestAudit.aplicacion = vAplicacion
        objRequestAudit.usrAplicacion = vUsrAplicacion

        objRequestMigracionSaldoGSM.telefono = vTelefono
        objRequestMigracionSaldoGSM.audit = objRequestAudit

        objResponseMigracionSaldoGSM = objMigracionSaldoGSMWS.transferirSaldo(objRequestMigracionSaldoGSM)

        oDatosInteraccion.Codigo_Respuesta = objResponseMigracionSaldoGSM.audit.codigoRespuesta
        oDatosInteraccion.Id_Transaccion = objResponseMigracionSaldoGSM.audit.idTransaccion
        oDatosInteraccion.Mensaje_Respuesta = objResponseMigracionSaldoGSM.audit.mensajeRespuesta
        
        Return oDatosInteraccion
    End Function






End Class
