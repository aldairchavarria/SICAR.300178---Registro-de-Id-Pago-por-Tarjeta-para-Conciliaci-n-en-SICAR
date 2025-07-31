Public Class clsDOL

    Public Function RegistroDOL(ByVal vCliente As clsCliente, _
                                    ByVal vURL As String, _
                                    ByVal intTimeOut As Integer, _
                                    ByRef strId As String, _
                                    ByRef strMensaje As String) As String


        Dim strResultado As String

        Try

            Dim oTransaccion As New WSDOL.EbsDolWS
            Dim oDOLRequest As New WSDOL.DOLRequest
            Dim oDOLResponse As New WSDOL.DOLResponse

            oTransaccion.Url = vURL
            oTransaccion.Timeout = intTimeOut
            oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials

            oDOLRequest.nroTransaccion = vCliente.NROTRANSACCION
            oDOLRequest.msisdn = vCliente.MSISDN
            oDOLRequest.nombres = vCliente.NOMBRES
            oDOLRequest.apellidos = vCliente.APELLIDOS
            oDOLRequest.tipoDocumento = vCliente.TIPODOCUMENTO
            oDOLRequest.numeroDocumento = vCliente.NUMERODOCUMENTO
            oDOLRequest.telefonoReferencia = vCliente.TELEFONOREFERENCIA
            oDOLRequest.fechaNacimiento = vCliente.FECHANACIMIENTO
            oDOLRequest.lugarNacimiento = vCliente.LUGARNACIMIENTO
            oDOLRequest.motivoRegistro = vCliente.MOTIVOREGISTRO
            oDOLRequest.direccionCompleta = vCliente.DIRECCIONCOMPLETA
            oDOLRequest.ciudad = vCliente.CIUDAD
            oDOLRequest.codigoEmpleado = vCliente.CODIGOEMPLEADO
            oDOLRequest.codigoSistema = vCliente.CODIGOSISTEMA
            oDOLRequest.tipo = vCliente.TIPO
            oDOLRequest.texto = vCliente.TEXTO

            'Invocacion Metodo
            oDOLResponse = oTransaccion.dol(oDOLRequest)

            strId = oDOLResponse.transaccion
            strMensaje = oDOLResponse.descripcion
            strResultado = oDOLResponse.resultado

            oDOLRequest = Nothing
            oDOLResponse = Nothing
            oTransaccion.Dispose()

        Catch ex As Exception

            strResultado = "999"
            strMensaje = "El servicio no se encuentra disponible en este momento. " + ex.Message
        End Try

        Return strResultado

    End Function
End Class
