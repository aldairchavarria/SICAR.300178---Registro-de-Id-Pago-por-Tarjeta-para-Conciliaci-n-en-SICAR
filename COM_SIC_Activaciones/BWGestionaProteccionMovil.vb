Imports System.Configuration
Imports System.Net

'PROY-24724-IDEA-28174 - INI
Public Class BWGestionaProteccionMovil

    Dim oGestionaProteccionMovil As New GestionaProteccionMovilWS.GestionaProteccionMovilWSService

    Public Function ProcesarProteccionMovil(ByVal strNroSEC As String, _
                                            ByVal strNroTelefono As String, _
                                            ByVal strNroCertificado As String, _
                                            ByVal strEstadoRpta As String, _
                                            ByVal strUsuario As String, _
                                            ByVal strTerminal As String, _
                                            ByRef strCodRpta As String, _
                                            ByRef strMgsRpta As String)

        Dim objRequest As New GestionaProteccionMovilWS.procesarPagoPMRequest
        Dim objResponse As New GestionaProteccionMovilWS.procesarPagoPMResponse
        oGestionaProteccionMovil.Url = ConfigurationSettings.AppSettings("consGestionaProteccionMovilWS_URL")
        oGestionaProteccionMovil.Timeout = ConfigurationSettings.AppSettings("consGestionaProteccionMovilWS_TimeOut") 

        Dim objAuditRequest As New GestionaProteccionMovilWS.auditRequestType
        objAuditRequest.idTransaccion = strNroSEC & DateTime.Now.ToString("yyyyMMddHHmmssfff")
        objAuditRequest.ipAplicacion = strTerminal
        objAuditRequest.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
        objAuditRequest.usuarioAplicacion = strUsuario

        objRequest.auditRequest = objAuditRequest
        objRequest.nroSec = strNroSEC
        objRequest.nroTelefono = strNroTelefono
        objRequest.nroCertificado = strNroCertificado
        objRequest.estadoRpta = strEstadoRpta

        objResponse = oGestionaProteccionMovil.procesarPagoPM(objRequest)

        strCodRpta = Funciones.CheckInt(objResponse.auditResponse.codigoRespuesta)
        strMgsRpta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)

    End Function

    Public Function EliminarProteccionMovil(ByVal strNroSEC As String, _
    					    ByVal strNroCertificado As String, _ 
                                            ByVal strUsuario As String, _
                                            ByVal strTerminal As String, _
                                            ByRef strCodRpta As String, _
                                            ByRef strMgsRpta As String)

        Dim objRequest = New GestionaProteccionMovilWS.eliminarPrimaRequest
        Dim objResponse = New GestionaProteccionMovilWS.eliminarPrimaResponse

        oGestionaProteccionMovil.Url = ConfigurationSettings.AppSettings("consGestionaProteccionMovilWS_URL")
        oGestionaProteccionMovil.Timeout = ConfigurationSettings.AppSettings("consGestionaProteccionMovilWS_TimeOut")

        Dim objAuditRequest As New GestionaProteccionMovilWS.auditRequestType
        objAuditRequest.idTransaccion = strNroSEC & DateTime.Now.ToString("yyyyMMddHHmmssfff")
        objAuditRequest.ipAplicacion = strTerminal
        objAuditRequest.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
        objAuditRequest.usuarioAplicacion = strUsuario

        objRequest.auditRequest = objAuditRequest
        'objRequest.certifTemp = ""
        objRequest.certifTemp = strNroCertificado
        objRequest.nroSec = strNroSEC

        objResponse = oGestionaProteccionMovil.eliminarPrima(objRequest)

        strCodRpta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
        strMgsRpta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)

    End Function

End Class
'PROY-24724-IDEA-28174 - FIN