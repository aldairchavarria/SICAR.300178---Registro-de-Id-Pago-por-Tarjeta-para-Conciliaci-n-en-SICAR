Imports System.EnterpriseServices
Imports System.Configuration
Imports Microsoft.Web.Services2.Security
Imports Microsoft.Web.Services2.Security.Tokens
Public Class clsCambioSIM

    Public Function CambioSIM(ByVal vMSISDN As String, _
                                ByVal vICCID As String, _
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
            Dim oCambioChipRequest As New ESBTransaccionesWS.CambioChipRequest
            Dim oCambioChipResponse As New ESBTransaccionesWS.CambioChipResponse

            oTransaccion.Url = vURL
            oTransaccion.Timeout = intTimeOut
            oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials

            oCambioChipRequest.msisdn = vMSISDN
            oCambioChipRequest.iccidNuevo = vICCID
            oCambioChipRequest.id = vID
            oCambioChipRequest.ip = vIP
            oCambioChipRequest.empleado = vEmpleado
            oCambioChipRequest.aplicacion = vAplicacion

            'Invocacion Metodo
            oCambioChipResponse = oTransaccion.ejecutarCambioChip(oCambioChipRequest)

            strResultado = oCambioChipResponse.resultado
            strMensaje = oCambioChipResponse.mensaje

            oCambioChipResponse = Nothing
            oCambioChipRequest = Nothing
            oTransaccion.Dispose()

        Catch ex As Exception
            strResultado = "999"
            strMensaje = ex.Message
        End Try

        Return strResultado

    End Function

    Public Function CambioSIMPrepago(ByVal strCanal As String, _
                             ByVal strIdAplicacion As String, _
                             ByVal strUsrAplicacion As String, _
                             ByVal strUsrSesion As String, _
                             ByVal strIdTransaccionESB As String, _
                             ByVal strIdTransaccionNegocio As String, _
                             ByVal dtFechaInicio As DateTime, _
                             ByVal strNodoAdicional As String, _
                             ByVal strMSISDN As String, _
                             ByVal strTipoDocumento As String, _
                             ByVal strNumeroDocumento As String, _
                             ByVal strICCIDNuevo As String, _
                             ByVal strCanalAtencion As String, _
                             ByVal strCodigoMaterial As String, _
                             ByVal strOficinaVenta As String, _
                             ByVal strClienteSap As String, _
                             ByVal strCodigoBloqueo As String, _
                             ByVal intTimeOut As Integer, _
                             ByVal vURL As String, _
                             ByRef strEstado As Integer, _
                             ByRef strCodigoRespuesta As String, _
                             ByRef strDescripcionRespuesta As String, _
                             ByRef strUbicacionError As String, _
                             ByVal strFecha As DateTime, _
                             ByVal strOrigen As String, _
                             ByVal country As String, _
                             ByVal language As String, _
                             ByVal consumer As String, _
                             ByVal _system As String, _
                             ByVal modulo As String, _
                             ByVal pid As String, _
                             ByVal userId As String, _
                             ByVal dispositivo As String, _
                             ByVal wsIp As String, _
                             ByVal operation As String, _
                             ByVal msgType As String, ByVal user As String, _
                             ByVal password As String) As Integer

        Dim intRespuesta As Integer
        Dim oCambioSIMPre As BSS_CambioSimPrepago.BSS_CambioSimPrepago_WS = New BSS_CambioSimPrepago.BSS_CambioSimPrepago_WS
        Dim objHeaderRequest As BSS_CambioSimPrepago.HeaderRequestType = New BSS_CambioSimPrepago.HeaderRequestType
        Dim objCambioRequest As BSS_CambioSimPrepago.ejecutarCambioSimPreRequest = New BSS_CambioSimPrepago.ejecutarCambioSimPreRequest
        Dim objResponse As BSS_CambioSimPrepago.ejecutarCambioSimPreResponse = New BSS_CambioSimPrepago.ejecutarCambioSimPreResponse
        Try

            oCambioSIMPre.Url = vURL
            oCambioSIMPre.Credentials = System.Net.CredentialCache.DefaultCredentials
            oCambioSIMPre.Timeout = intTimeOut


            objHeaderRequest.country = country
            objHeaderRequest.language = language
            objHeaderRequest.consumer = consumer
            objHeaderRequest._system = _system
            objHeaderRequest.modulo = modulo
            objHeaderRequest.pid = pid
            objHeaderRequest.userId = userId
            objHeaderRequest.dispositivo = dispositivo
            objHeaderRequest.wsIp = wsIp
            objHeaderRequest.operation = operation
            objHeaderRequest.timestamp = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffzzz")
            objHeaderRequest.msgType = msgType

            Dim objHeaderRequestType1 As BSS_CambioSimPrepago.HeaderRequestType1 = New BSS_CambioSimPrepago.HeaderRequestType1

            objHeaderRequestType1.canal = strCanal
            objHeaderRequestType1.idAplicacion = strIdAplicacion
            objHeaderRequestType1.idAplicacionSpecified = True
            objHeaderRequestType1.usuarioAplicacion = strUsrAplicacion
            objHeaderRequestType1.usuarioSesion = strUsrSesion
            objHeaderRequestType1.idTransaccionESB = strIdTransaccionESB
            objHeaderRequestType1.idTransaccionNegocio = strIdTransaccionNegocio
            objHeaderRequestType1.fechaInicioSpecified = True
            objHeaderRequestType1.fechaInicio = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffzzz")
            objHeaderRequestType1.nodoAdicional = strNodoAdicional

            Dim type As BSS_CambioSimPrepago.cambioSimPrepagoType = New BSS_CambioSimPrepago.cambioSimPrepagoType

            type.msisdn = strMSISDN
            type.tipoDocumento = strTipoDocumento
            type.numeroDocumento = strNumeroDocumento
            type.iccidNuevo = strICCIDNuevo
            type.canalAtencion = strCanalAtencion
            type.codigoMaterial = strCodigoMaterial
            type.oficinaVenta = strOficinaVenta
            type.clienteSap = strClienteSap
            type.codigoBloqueo = strCodigoBloqueo

            Dim req_opc As BSS_CambioSimPrepago.RequestOpcionalTypeRequestOpcional = New BSS_CambioSimPrepago.RequestOpcionalTypeRequestOpcional
            req_opc.campo = ""
            req_opc.valor = ""

            Dim arr_req_opc As BSS_CambioSimPrepago.RequestOpcionalTypeRequestOpcional() = New BSS_CambioSimPrepago.RequestOpcionalTypeRequestOpcional(0) {}
            arr_req_opc(0) = req_opc
            objCambioRequest.listaRequestOpcional = arr_req_opc


            objCambioRequest.cambioSimPrepago = type

            oCambioSIMPre.headerReq = objHeaderRequestType1
            oCambioSIMPre.HeaderRequest = objHeaderRequest

            oCambioSIMPre.Credentials = System.Net.CredentialCache.DefaultCredentials  ''New System.Net.NetworkCredential() With {.UsernameToken  = "Integracion", .PasswordOption = "Synopsis2017"}
            Dim token As UsernameToken = New UsernameToken(user, password, PasswordOption.SendPlainText)
            Dim requestContext As Microsoft.Web.Services2.SoapContext = oCambioSIMPre.RequestSoapContext
            requestContext.Security.Tokens.Add(token)
            objResponse = oCambioSIMPre.ejecutarCambioSimPre(objCambioRequest)
            strEstado = objResponse.responseStatus.estado
            strCodigoRespuesta = objResponse.responseStatus.codigoRespuesta
            strDescripcionRespuesta = objResponse.responseStatus.descripcionRespuesta
            intRespuesta = Int32.Parse(objResponse.responseStatus.codigoRespuesta)
            strUbicacionError = objResponse.responseStatus.ubicacionError
            strFecha = objResponse.responseStatus.fecha
            strOrigen = objResponse.responseStatus.origen

        Catch ex As Exception
            strCodigoRespuesta = "-1"
            strDescripcionRespuesta = "Error con el metodo WS. " & ex.Message
            intRespuesta = -1
            'strUbicacionError = objResponse.responseStatus.ubicacionError
            'strFecha = objResponse.responseStatus.fecha
            'strOrigen = objResponse.responseStatus.origen
        Finally

            oCambioSIMPre = Nothing
            objCambioRequest = Nothing
            objResponse = Nothing
        End Try

        Return intRespuesta
    End Function
End Class
