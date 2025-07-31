'PROY-26963 INI
Imports System.Configuration

Public Class BWProcesoPortabilidad
    Private oProcesoPortabilidad As New ProcesoPortabilidadWS.BSS_ProcesoPortabilidadSOAP11BindingQSService


    Public Sub New()

        oProcesoPortabilidad.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConstUrlProcesoPortaWS"))
        oProcesoPortabilidad.Credentials = System.Net.CredentialCache.DefaultCredentials
        oProcesoPortabilidad.Timeout = ConfigurationSettings.AppSettings("ConstTimeOutProcesoPortaWS")

    End Sub

    Public Function RealizarProgramacion(ByVal _objAudit As ItemGenerico, ByVal _idSolicitud As String, ByRef _CodigoError As String, ByRef _MensajeError As String)

        Dim objRequest As New ProcesoPortabilidadWS.realizarProgramacionRequest
        Dim objResponse As New ProcesoPortabilidadWS.realizarProgramacionResponse
        Dim oAuditRequest As New ProcesoPortabilidadWS.HeaderRequestType

        Try
            oAuditRequest.idAplicacion = _objAudit.CODIGO
            oAuditRequest.usuarioAplicacion = _objAudit.CODIGO2
            oAuditRequest.usuarioSesion = _objAudit.CODIGO3
            oAuditRequest.idTransaccionESB = _objAudit.DESCRIPCION

            oProcesoPortabilidad.headerRequest = oAuditRequest

            objRequest.idSolicitud = _idSolicitud
            'INI: PROY-140262 BLACKOUT
            If (_objAudit.CODIGO4.Equals("1")) Then
                _CodigoError = "0"
                _MensajeError = _objAudit.DESCRIPCION4
            Else
            objResponse = oProcesoPortabilidad.realizarProgramacion(objRequest)
            _CodigoError = objResponse.responseStatus.codigoRespuesta
            _MensajeError = objResponse.responseStatus.descripcionRespuesta
            End If
            'FIN: PROY-140262 BLACKOUT
        Catch ex As Exception
            _CodigoError = 99
            _MensajeError = ex.Message
        Finally

        End Try
    End Function

    Public Function ComprobarDeuda(ByVal NroSec As String, ByVal NumLinea As String, ByVal oAudit As ItemGenerico, ByRef _CodigoError As String, ByRef _MensajeError As String)

        Dim Salida As String
        Dim objRequest As New ProcesoPortabilidadWS.comprobarDeudaRequest
        Dim objResponse As New ProcesoPortabilidadWS.comprobarDeudaResponse
        Dim oAuditRequest As New ProcesoPortabilidadWS.HeaderRequestType
        Try
            oAuditRequest.idTransaccionESB = oAudit.CODIGO
            oAuditRequest.idAplicacion = oAudit.CODIGO2
            oAuditRequest.usuarioAplicacion = oAudit.DESCRIPCION
            oAuditRequest.usuarioAplicacion = oAudit.DESCRIPCION2

            objRequest.solinCodigo = NroSec
            objRequest.numLinea = NumLinea

            objResponse = oProcesoPortabilidad.comprobarDeuda(objRequest)

            _CodigoError = Convert.ToString(objResponse.responseStatus.codigoRespuesta)
            _MensajeError = Convert.ToString(objResponse.responseStatus.descripcionRespuesta)

        Catch ex As Exception
            _MensajeError = ex.Message.ToString()
        Finally

        End Try
        Return Salida
    End Function
End Class
'PROY-26963 INI