Public Class RegistroDatosVentaPrepago

    Dim oTransaccion As New WSRegistroDatosVentaPrepago.ebsRegistroDatosVentaPreService

    Public Sub New()
        oTransaccion.Url = Configuration.ConfigurationSettings.AppSettings("WSRegistroDatosVentaPrepago_url")
        oTransaccion.Timeout = Funciones.CheckInt(Configuration.ConfigurationSettings.AppSettings("WSRegistroDatosVentaPrepago_timeout"))
        oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
    End Sub

    Public Function registrarVenta( _
                        ByRef idTransaccion As String, _
                        ByVal ipAplicacion As String, _
                        ByVal nombreAplicacion As String, _
                        ByVal chippack As String, _
                        ByVal opcion As String, _
                        ByVal msisdn As String, _
                        ByVal numeroin As String, _
                        ByVal promocion As String, _
                        ByVal tipoActivacion As String, _
                        ByVal tipoProducto As String, _
                        ByVal adicional1 As String, _
                        ByVal adicional2 As String, _
                        ByVal puntoVenta As String, _
                        ByVal departamento As String, _
                        ByVal fechaRegistro As Date, _
                        ByVal origen As String, _
                        ByVal imei As String, _
                        ByVal iccid As String, _
                        ByVal codpromo As String, _
                        ByVal offer As String, _
                        ByVal subscriberstatusFinal As String, _
                        ByVal usuario As String, _
                        ByRef mensajeRespuesta As String) As String
        Dim strCodRespuesta As String
        Try
            strCodRespuesta = oTransaccion.registrarVenta(idTransaccion, _
                                        ipAplicacion, _
                                        nombreAplicacion, _
                                        chippack, _
                                        opcion, _
                                        msisdn, _
                                        numeroin, _
                                        promocion, _
                                        tipoActivacion, _
                                        tipoProducto, _
                                        adicional1, _
                                        adicional2, _
                                        puntoVenta, _
                                        departamento, _
                                        fechaRegistro, _
                                        origen, _
                                        imei, _
                                        iccid, _
                                        codpromo, _
                                        offer, _
                                        subscriberstatusFinal, _
                                        usuario, _
                                        mensajeRespuesta)
        Catch ex As Exception
            strCodRespuesta = "-99"
            mensajeRespuesta = ex.Message.ToString()
        End Try
        Return strCodRespuesta
    End Function

End Class
