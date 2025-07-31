Imports System.EnterpriseServices
Imports System.Configuration
Public Class clsBloqueoDesbloqueo

    Public Function BloqueoDesbloqueo(ByVal vMSISDN As String, _
                                        ByVal vAccion As String, _
                                        ByVal vID As String, _
                                        ByVal vIP As String, _
                                        ByVal vEmpleado As String, _
                                        ByVal vAplicacion As String, _
                                        ByVal intTimeOut As Integer, _
                                        ByVal vURL As String, _
                                        ByRef strMensaje As String) As String


        Dim strResultado As String

        Try

            Dim oTransaccion As New ESBTransaccionesWS.ebsTransaccionesService
            Dim oBloqueoDesbloqueoRequest As New ESBTransaccionesWS.BloqueoDesbloqueoRequest
            Dim oBloqueoDesbloqueoResponse As New ESBTransaccionesWS.BloqueoDesbloqueoResponse

            oTransaccion.Url = vURL
            oTransaccion.Timeout = intTimeOut
            oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials

            oBloqueoDesbloqueoRequest.msisdn = vMSISDN
            oBloqueoDesbloqueoRequest.accion = vAccion
            oBloqueoDesbloqueoRequest.id = vID
            oBloqueoDesbloqueoRequest.ip = vIP
            oBloqueoDesbloqueoRequest.empleado = vEmpleado
            oBloqueoDesbloqueoRequest.aplicacion = vAplicacion

            'Invocacion Metodo
            oBloqueoDesbloqueoResponse = oTransaccion.ejecutarBloqueoDesbloqueo(oBloqueoDesbloqueoRequest)

            strResultado = oBloqueoDesbloqueoResponse.resultado
            strMensaje = oBloqueoDesbloqueoResponse.mensaje

            oBloqueoDesbloqueoResponse = Nothing
            oBloqueoDesbloqueoRequest = Nothing
            oTransaccion.Dispose()

        Catch ex As Exception

            strResultado = "999"
            strMensaje = "El servicio no se encuentra disponible en este momento, por favor espere unos minutos. " + ex.Message
        End Try

        Return strResultado

    End Function

End Class
