Imports System.EnterpriseServices
Imports System.Configuration

Public Class clsEmpleadoWS

    Dim _oTransaccion As New EmpleadoWS.EbsDatosEmpleadoService
    Public Sub New()
        'Get Url-Credentials-TimeOut
        _oTransaccion.Url = ConfigurationSettings.AppSettings("ConstUrlEmpleado").ToString()
        _oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
        _oTransaccion.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("ConstTimeOutEmpleado").ToString())
    End Sub

    Public Function GetDatosEmpleado(ByVal strIdUsuario As String, ByVal strLogin As String, ByRef msgSalida As String) As clsUsuario
        Dim _oRequest As New EmpleadoWS.DatosEmpleadoRequest
        Dim _oResponse As New EmpleadoWS.DatosEmpleadoResponse
        Dim oUsuario As New clsUsuario
        Try
            'Set oRequest
            _oRequest.idUsu = strIdUsuario
            _oRequest.loginNt = strLogin
            'Invocar Método
            _oResponse = _oTransaccion.obtenerDatosEmpleadoPorId(_oRequest)
            'Exito de la consulta
            If _oResponse.codRes = "0" Then
                oUsuario.UsuarioId = _oResponse.empleado.idEmp
                oUsuario.Login = _oResponse.empleado.login
                oUsuario.Nombre = _oResponse.empleado.nombre
                oUsuario.Apellido = _oResponse.empleado.apellido
                oUsuario.ApellidoMaterno = _oResponse.empleado.apellidoMaterno
                oUsuario.NombreCompleto = _oResponse.empleado.nomCompleto
                oUsuario.CodigoVendedor = _oResponse.empleado.idCodvendedorSap
                oUsuario.AreaId = _oResponse.empleado.idArea
                oUsuario.AreaDescripcion = _oResponse.empleado.descArea
            Else
                oUsuario = Nothing
            End If

        Catch ex As Exception
            oUsuario = Nothing
            msgSalida = "Error. " + ex.Message.ToString()
        Finally
            _oRequest = Nothing
            _oResponse = Nothing
        End Try

        Return oUsuario
    End Function

End Class
