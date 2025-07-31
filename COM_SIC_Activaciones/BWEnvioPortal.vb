Imports System.Configuration

Public Class BWEnvioPortal
    Private oEnvioPortal As New EnvioPortalWS.EnvioPortaWSService


    Public Sub New()

        oEnvioPortal.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConstUrlEnvioPortalWS"))
        oEnvioPortal.Credentials = System.Net.CredentialCache.DefaultCredentials
        oEnvioPortal.Timeout = ConfigurationSettings.AppSettings("ConstTimeOutEnvioPortalWS")

    End Sub


    Public Function RealizarSolicitudPortabilidad(ByVal _intNroSec As Int64, _
    ByVal _idTransaccion As String, ByVal _ipAplicacion As String, _
    ByVal _nombreAplicacion As String, ByVal _usuarioAplicacion As String, _
    ByVal _nombreHost As String, ByVal _nombreServidor As String, _
    ByVal _ipServidor As String, ByVal _TipoPorta As String, ByRef _DescripSevice As String, Optional ByVal _FlagNoPago As String = "", Optional ByVal _numPedido As String = "") As String ' PROY 32089

        Dim strRespuesta As String = ""

        Dim objRequest As New EnvioPortalWS.realizarSolicitudPortabilidadRequest
        Dim objResponse As New EnvioPortalWS.realizarSolicitudPortabilidadResponse
        Dim oAuditRequest As New EnvioPortalWS.auditRequestType
' INI PROY 32089
        Dim objOptional_1 As New EnvioPortalWS.parametrosTypeObjetoOpcional
        Dim objOptional_2 As New EnvioPortalWS.parametrosTypeObjetoOpcional
        Dim objListaRequestOptional(2) As EnvioPortalWS.parametrosTypeObjetoOpcional
 ' FIN PROY 32089

        Try
            Dim strObservacion As String = ""

            strObservacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConsPortaObservacionesSP"))

            oAuditRequest.idTransaccion = _idTransaccion
            oAuditRequest.ipAplicacion = _ipAplicacion
            oAuditRequest.nombreAplicacion = _nombreAplicacion
            oAuditRequest.usuarioAplicacion = _usuarioAplicacion

            objRequest.auditRequest = oAuditRequest


            objRequest.codigoAplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConstCodAplicacion"))
            objRequest.flagSP = ""
            objRequest.id = ""
            objRequest.ipServidor = _ipServidor
            objRequest.nombreHost = _nombreHost
            objRequest.nombreServidor = _nombreServidor
            objRequest.numeroSec = _intNroSec.ToString()
            objRequest.observaciones = strObservacion
            objRequest.tipoPort = _TipoPorta
' INI PROY 32089
            If _FlagNoPago.Length > 0 And _numPedido.Length > 0 Then
                objOptional_1.campo = "FlagNoPago"
                objOptional_1.valor = _FlagNoPago
                objListaRequestOptional(0) = objOptional_1

                objOptional_2.campo = "NumPedido"
                objOptional_2.valor = _numPedido
                objListaRequestOptional(1) = objOptional_2

                objRequest.listaRequestOpcional = objListaRequestOptional
            End If
' FIN PROY 32089
            objResponse = oEnvioPortal.realizarSolicitudPortabilidad(objRequest)
            _DescripSevice = objResponse.auditResponse.mensajeRespuesta
            strRespuesta = objResponse.auditResponse.codigoRespuesta

        Catch ex As Exception
            strRespuesta = ex.Message
        Finally
        End Try
        Return strRespuesta


    End Function

End Class
