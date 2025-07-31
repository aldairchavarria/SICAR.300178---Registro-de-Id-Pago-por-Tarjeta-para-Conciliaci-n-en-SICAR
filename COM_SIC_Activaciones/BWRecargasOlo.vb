'PROY-31850 FASE IV - INICIO
Imports System
Imports System.Collections
Imports System.Net
Imports System.Text
Imports COM_SIC_Activaciones.WSRecargasOlo
Imports System.Configuration
Public Class BWRecargasOlo
    Dim _oTransaccion As New WSRecargasOlo.MED_RecargasOloSOAP11BindingQSService
    Private strUsuarioOLO As String
    Private strPasswordOLO As String
    Private KEY_RECARGAS_OLO As String
    Public objseg As COM_SIC_Seguridad.Configuracion
    Public Sub New()
        _oTransaccion.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("UrlListRecargaOlo"))
        _oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
        _oTransaccion.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("TimeOutListRecargaOlo"))
        KEY_RECARGAS_OLO = ConfigurationSettings.AppSettings("KEY_RECARGAS_OLO")

    End Sub
    Public Function ObtenerPlanesRecarga(ByVal StrNumeroTelefono As String, ByVal strIdTransaccion As String, _
        ByVal strCodAplicacion As String, ByVal strUserAplicacion As String, _
        ByRef listaPlanesRecarga As ArrayList, ByRef numeroDocumentoCli As String) 'PROY-31850 - INCIDENCIA RECARGA POR NUMERO DOCUMENTO (numeroDocumentoCli)


        Dim keyOLO As New clsKeyOLO
        Dim objHeaderRequest As New WSRecargasOlo.HeaderRequestType
        Dim objRequestOlo As New WSRecargasOlo.buscarPlanesRecargaRequestType
        Dim oResponseOlo As New WSRecargasOlo.buscarPlanesRecargaResponse
        Dim objHeaderResponse As New WSRecargasOlo.HeaderResponseType
        Dim objResponsePlanes As New WSRecargasOlo.PlanesPlan

        Dim strCodRpta As String = ""
        Dim strMgsRpta As String = ""

        Try
            objHeaderRequest.canal = keyOLO.strRecargaOloWSCanal
            objHeaderRequest.idAplicacion = strCodAplicacion
            objHeaderRequest.usuarioAplicacion = strCodAplicacion
            objHeaderRequest.usuarioSesion = strUserAplicacion
            objHeaderRequest.idTransaccionESB = strIdTransaccion
            objHeaderRequest.idTransaccionNegocio = strIdTransaccion
            objHeaderRequest.fechaInicio = Nothing
            objHeaderRequest.nodoAdicional = keyOLO.strRecargaOloWS_AdditionalNode

            'Request
            objRequestOlo.usuario = keyOLO.strMobileNumberWS_UserName
            objRequestOlo.contrasenia = keyOLO.strMobileNumberWS_Password
            objRequestOlo.numeroTelefono = StrNumeroTelefono
            objRequestOlo.grupoCanal = keyOLO.strRecargaOloWS_gpcanal
            objRequestOlo.version = keyOLO.strRecargaOloWS_version
            objRequestOlo.codigoCompania = keyOLO.strRecargaOloWS_compania

            Dim listaResquestOpcional() As WSRecargasOlo.RequestOpcionalTypeRequestOpcional = New WSRecargasOlo.RequestOpcionalTypeRequestOpcional((1) - 1) {}
            Dim oRecargaOLOLineaOpcional As WSRecargasOlo.RequestOpcionalTypeRequestOpcional = New WSRecargasOlo.RequestOpcionalTypeRequestOpcional
            oRecargaOLOLineaOpcional.campo = keyOLO.strRecargaOloWS_Campo
            oRecargaOLOLineaOpcional.valor = keyOLO.strRecargaOloWS_Valor
            listaResquestOpcional(0) = oRecargaOLOLineaOpcional
            objRequestOlo.listaResquestOpcional = listaResquestOpcional
            'Ejecuta Header
            _oTransaccion.headerRequest = objHeaderRequest

            'Ejecuta OSB - Carga Response

            oResponseOlo = _oTransaccion.buscarPlanesRecarga(objRequestOlo)

            strCodRpta = Funciones.CheckStr(oResponseOlo.responseStatus.codigoRespuesta)
            strMgsRpta = Funciones.CheckStr(oResponseOlo.responseStatus.descripcionRespuesta)
            Dim lst = oResponseOlo.responseData.planesCliente

            numeroDocumentoCli = oResponseOlo.responseData.numeroDoc   'PROY-31850 - INCIDENCIA RECARGA POR NUMERO DOCUMENTO
            If oResponseOlo.responseData Is Nothing OrElse oResponseOlo.responseData.planesCliente.Length > 0 Then
                For i As Integer = 0 To oResponseOlo.responseData.planesCliente.Length - 1
                    Dim arrItem = lst(i)
                    listaPlanesRecarga.Add(arrItem)
                Next
            Else
                listaPlanesRecarga = Nothing
            End If
        Catch ex As Exception
            listaPlanesRecarga = Nothing
            strMgsRpta = "-1"
        End Try
        'objHeaderRequest.

    End Function
    Public Function RecargaOlo(ByVal StrNumeroTelefono As String, ByVal strPrecio As String, ByVal strIdTransaccion As String, _
        ByVal strCodAplicacion As String, ByVal strUserAplicacion As String, _
        ByRef listaRecarga As ArrayList)

        Dim keyOLO As New clsKeyOLO
        Dim objHeaderRequest As New WSRecargasOlo.HeaderRequestType
        Dim objRequestOlo As New WSRecargasOlo.recargarRequestType
        Dim oResponseOlo As New WSRecargasOlo.recargarResponseType
        Dim objHeaderResponse As New WSRecargasOlo.HeaderResponseType

        Try
            'HeaderRequest

            objHeaderRequest.canal = keyOLO.strRecargaOloWSCanal
            objHeaderRequest.idAplicacion = strCodAplicacion
            objHeaderRequest.usuarioAplicacion = strUserAplicacion
            objHeaderRequest.usuarioSesion = strUserAplicacion
            objHeaderRequest.idTransaccionESB = strIdTransaccion
            objHeaderRequest.idTransaccionNegocio = strIdTransaccion
            objHeaderRequest.fechaInicio = Nothing
            objHeaderRequest.nodoAdicional = keyOLO.strRecargaOloWS_AdditionalNode

            'Request
            objRequestOlo.version = ""
            objRequestOlo.usuario = strUsuarioOLO
            objRequestOlo.contrasenia = strPasswordOLO
            objRequestOlo.precio = strPrecio
            objRequestOlo.idPlan = ""
            objRequestOlo.numeroTelefono = StrNumeroTelefono
            objRequestOlo.grupoCanal = ""
            objRequestOlo.codigoCompania = ""
            objRequestOlo.fecha = Nothing
            objRequestOlo.idTransaccionCompania = ""

            Dim listaResquestOpcional() As WSRecargasOlo.RequestOpcionalTypeRequestOpcional = New WSRecargasOlo.RequestOpcionalTypeRequestOpcional((1) - 1) {}
            Dim oRecargaOLOLineaOpcional As WSRecargasOlo.RequestOpcionalTypeRequestOpcional = New WSRecargasOlo.RequestOpcionalTypeRequestOpcional
            oRecargaOLOLineaOpcional.campo = keyOLO.strRecargaOloWS_Campo
            oRecargaOLOLineaOpcional.valor = keyOLO.strRecargaOloWS_Valor
            listaResquestOpcional(0) = oRecargaOLOLineaOpcional
            objRequestOlo.listaResquestOpcional = listaResquestOpcional
            'Ejecuta Header
            _oTransaccion.headerRequest = objHeaderRequest

            'Ejecuta OSB - Carga Response

            oResponseOlo = _oTransaccion.recargar(objRequestOlo)
            Dim lst = oResponseOlo.responseStatus
            Dim strCodRpta As String = ""
            Dim strMgsRpta As String = ""
            strCodRpta = Funciones.CheckStr(oResponseOlo.responseStatus.codigoRespuesta)
            strMgsRpta = Funciones.CheckStr(oResponseOlo.responseStatus.descripcionRespuesta)
            If strCodRpta = 0 Then
                listaRecarga.Add(oResponseOlo.responseStatus.codigoRespuesta)
                listaRecarga.Add(oResponseOlo.responseStatus.descripcionRespuesta)
                listaRecarga.Add(oResponseOlo.responseStatus.fecha)
                listaRecarga.Add(oResponseOlo.responseStatus.codigoRespuesta)
            End If
        Catch ex As Exception

        End Try


    End Function

End Class
'PROY-31850 FASE IV - INICIO
