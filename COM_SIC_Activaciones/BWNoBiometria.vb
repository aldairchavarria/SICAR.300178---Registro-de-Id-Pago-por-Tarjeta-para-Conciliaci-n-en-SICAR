''DIL.C(20160226) :: INI - PROY 21987-IDEA 28251
Imports System.Configuration
Imports System.Net

Public Class BWNoBiometria
    Public Function GuardarCobertura(ByVal strUsuario As String, _
                                      ByVal strTelefono As String, _
                                      ByVal strDepartamento As String, _
                                      ByVal strProvincia As String, _
                                      ByVal strDistrito As String, _
                                      ByVal strCentroPoblado As String, _
                                      ByVal strCobertura As String, _
                                      ByVal strDocumentoIdentidadNumero As String) As String

        Dim strRespuesta As String = String.Empty
        Dim strRespuestaCodigo As String = String.Empty
        Dim strRespuestaMensaje As String = String.Empty

        Try
            Dim objRequest As New WSNoBiometria.guardarCoberturaRequest
            objRequest.usuario = strUsuario
            objRequest.telefono = strTelefono
            objRequest.departamento = strDepartamento
            objRequest.provincia = strProvincia
            objRequest.distrito = strDistrito
            objRequest.centroPoblado = strCentroPoblado
            objRequest.cobertura = strCobertura
            objRequest.numeroDocumento = strDocumentoIdentidadNumero

            objRequest.auditRequest = New WSNoBiometria.AuditRequestType
            objRequest.auditRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            objRequest.auditRequest.ipAplicacion = Funciones.CheckStr(Dns.GetHostByName(Dns.GetHostName()).AddressList(0))
            objRequest.auditRequest.nombreAplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("constNombreAplicacion"))
            objRequest.auditRequest.usuarioAplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("constUsuarioAplicacionSISACT"))

            Dim objNoBiometria As New WSNoBiometria.ebsIdentificacionCliente
            objNoBiometria.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("constUrlNoBio"))
            objNoBiometria.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("TimeOutNoBio"))

            Dim objResponse As New WSNoBiometria.guardarCoberturaResponse
            objResponse = objNoBiometria.guardarCobertura(objRequest)

            strRespuestaCodigo = objResponse.auditResponse.codigoRespuesta
            strRespuestaMensaje = objResponse.auditResponse.mensajeRespuesta
        Catch ex As Exception
            strRespuestaCodigo = "-1"
            strRespuestaMensaje = "ErrorWS[" & ex.Message & "]"
        Finally
            strRespuesta = strRespuestaCodigo & ";" & strRespuestaMensaje
        End Try

        Return strRespuesta
    End Function
End Class
''DIL.C(20160226) :: FIN - PROY 21987-IDEA 28251
