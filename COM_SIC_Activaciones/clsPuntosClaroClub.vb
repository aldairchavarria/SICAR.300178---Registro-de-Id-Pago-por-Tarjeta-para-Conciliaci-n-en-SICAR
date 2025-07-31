Imports System.EnterpriseServices
Imports System.Configuration
Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class clsPuntosClaroClub

    Private _IP_SERVER As String
    Private _APLICACION As String
    Private _NOMBRE_SERVER As String
    Public clsPuntosClaroClub() 'PROY-140126

    Public Property IP_SERVER() As String
        Set(ByVal value As String)
            _IP_SERVER = value
        End Set
        Get
            Return _IP_SERVER
        End Get
    End Property

    Public Property APLICACION() As String
        Set(ByVal value As String)
            _APLICACION = value
        End Set
        Get
            Return _APLICACION
        End Get
    End Property

    Public Property NOMBRE_SERVER() As String
        Set(ByVal value As String)
            _NOMBRE_SERVER = value
        End Set
        Get
            Return _NOMBRE_SERVER
        End Get
    End Property

    'Public Sub liberarPuntos(ByVal K_TIPO_DOC As String, _
    '                         ByVal K_NUM_DOC As String, _
    '                         ByVal tipoClie As String, _
    '                         ByRef txId As String, _
    '                         ByRef errorCode As String, _
    '                         ByRef errorMsg As String)

    '    Dim objGestionarPuntosService As New GestionarPuntosClaroClubWS.ebsGestionarPuntosService
    '    Dim ConstTimeOutGestionarPuntos As Integer
    '    objGestionarPuntosService.Url = ConfigurationSettings.AppSettings("WSGestionarPuntosCC_url")
    '    objGestionarPuntosService.Credentials = System.Net.CredentialCache.DefaultCredentials
    '    objGestionarPuntosService.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("WSGestionarPuntosCC_timeout"))

    '    Dim objLiberarPuntosRequest As New GestionarPuntosClaroClubWS.liberarPuntosRequest
    '    Dim objLiberarPuntosResponse As New GestionarPuntosClaroClubWS.liberarPuntosResponse
    '    objLiberarPuntosRequest.ipAplicacion = IP_SERVER
    '    objLiberarPuntosRequest.aplicacion = APLICACION
    '    objLiberarPuntosRequest.tipoDoc = K_TIPO_DOC
    '    objLiberarPuntosRequest.numDoc = K_NUM_DOC
    '    objLiberarPuntosRequest.tipoClie = tipoClie

    '    Try
    '        objLiberarPuntosResponse = objGestionarPuntosService.liberarPuntos(objLiberarPuntosRequest)

    '        txId = objLiberarPuntosResponse.audit.txId
    '        errorCode = objLiberarPuntosResponse.audit.errorCode
    '        errorMsg = objLiberarPuntosResponse.audit.errorMsg
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objGestionarPuntosService = Nothing
    '        objLiberarPuntosRequest = Nothing
    '        objLiberarPuntosResponse = Nothing
    '    End Try
    'End Sub
    Public Function consultarPuntos(ByVal K_TIPO_DOC As String, _
                            ByVal K_NUM_DOC As String, _
                            ByVal usuarioAplicacion As String, _
                            ByVal K_COD_CLIENTE As String)

        Dim objConsultarPuntosService As New ConsultarPuntosWS.ebsConsultaPuntosClaroClubService
        Dim ConstTimeOutGestionarPuntos As Integer
        objConsultarPuntosService.Url = ConfigurationSettings.AppSettings("ConstUrlConsultarPuntos")
        objConsultarPuntosService.Credentials = System.Net.CredentialCache.DefaultCredentials
        objConsultarPuntosService.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("ConstTimeOutConsultarPuntos"))


        Dim objConsultarPuntosRequest As New ConsultarPuntosWS.consultarPuntosRequest
        Dim objConsultarPuntosResponse As New ConsultarPuntosWS.consultarPuntosResponse
        objConsultarPuntosRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmss")
        objConsultarPuntosRequest.ipAplicacion = IP_SERVER
        objConsultarPuntosRequest.aplicacion = APLICACION '"127.0.0.1"
        objConsultarPuntosRequest.usuarioAplicacion = usuarioAplicacion '"E77113";
        objConsultarPuntosRequest.tipoDoc = K_TIPO_DOC
        objConsultarPuntosRequest.numDoc = K_NUM_DOC
        objConsultarPuntosRequest.codigoCliente = K_COD_CLIENTE

        Try
            objConsultarPuntosResponse = objConsultarPuntosService.consultarPuntosClaroClub(objConsultarPuntosRequest)
            Return objConsultarPuntosResponse
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            objConsultarPuntosService = Nothing
            objConsultarPuntosRequest = Nothing
            objConsultarPuntosResponse = Nothing
        End Try
    End Function
    Public Sub bloquearPuntos(ByVal K_TIPO_DOC As String, _
                           ByVal K_NUM_DOC As String, _
                           ByVal tipoClie As String, _
                           ByVal usuario As String, _
                           ByRef txId As String, _
                           ByRef errorCode As String, _
                           ByRef errorMsg As String)

        Dim objGestionarPuntosService As New GestionarPuntosClaroClubWS.ebsGestionarPuntosService
        Dim ConstTimeOutGestionarPuntos As Integer
        objGestionarPuntosService.Url = ConfigurationSettings.AppSettings("ConstUrlGestionarPuntos")
        objGestionarPuntosService.Credentials = System.Net.CredentialCache.DefaultCredentials
        objGestionarPuntosService.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("ConstTimeOutGestionarPuntos"))

        Dim objBloquearPuntosRequest As New GestionarPuntosClaroClubWS.bloquearPuntosRequest
        Dim objBloquearPuntosResponse As New GestionarPuntosClaroClubWS.bloquearPuntosResponse
        objBloquearPuntosRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmss")
        objBloquearPuntosRequest.ipAplicacion = IP_SERVER
        objBloquearPuntosRequest.aplicacion = APLICACION '"127.0.0.1"
        objBloquearPuntosRequest.usuarioAplicacion = usuario '"E77113";
        objBloquearPuntosRequest.tipoDoc = K_TIPO_DOC
        objBloquearPuntosRequest.numDoc = K_NUM_DOC
        objBloquearPuntosRequest.tipoClie = tipoClie
        objBloquearPuntosRequest.usuario = usuario '"E77113";


        Try
            objBloquearPuntosResponse = objGestionarPuntosService.bloquearPuntos(objBloquearPuntosRequest)

            txId = objBloquearPuntosResponse.audit.txId
            errorCode = objBloquearPuntosResponse.audit.errorCode
            errorMsg = objBloquearPuntosResponse.audit.errorMsg
        Catch ex As Exception
            Throw ex
        Finally
            objGestionarPuntosService = Nothing
            objBloquearPuntosRequest = Nothing
            objBloquearPuntosResponse = Nothing
        End Try
    End Sub

    Public Sub liberarPuntos(ByVal K_TIPO_DOC As String, _
                          ByVal K_NUM_DOC As String, _
                          ByVal tipoClie As String, _
                          ByRef txId As String, _
                          ByRef errorCode As String, _
                          ByRef errorMsg As String)

        Dim objGestionarPuntosService As New GestionarPuntosClaroClubWS.ebsGestionarPuntosService
        Dim ConstTimeOutGestionarPuntos As Integer
        objGestionarPuntosService.Url = ConfigurationSettings.AppSettings("ConstUrlGestionarPuntos")
        objGestionarPuntosService.Credentials = System.Net.CredentialCache.DefaultCredentials
        objGestionarPuntosService.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("ConstTimeOutGestionarPuntos"))

        Dim objLiberarPuntosRequest As New GestionarPuntosClaroClubWS.liberarPuntosRequest
        Dim objLiberarPuntosResponse As New GestionarPuntosClaroClubWS.liberarPuntosResponse
        objLiberarPuntosRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmss")
        objLiberarPuntosRequest.ipAplicacion = IP_SERVER
        objLiberarPuntosRequest.aplicacion = APLICACION '"127.0.0.1"
        objLiberarPuntosRequest.usuarioAplicacion = "" '"E77113";
        objLiberarPuntosRequest.tipoDoc = K_TIPO_DOC
        objLiberarPuntosRequest.numDoc = K_NUM_DOC
        objLiberarPuntosRequest.tipoClie = tipoClie


        Try
            objLiberarPuntosResponse = objGestionarPuntosService.liberarPuntos(objLiberarPuntosRequest)
            txId = objLiberarPuntosResponse.audit.txId
            errorCode = objLiberarPuntosResponse.audit.errorCode
            errorMsg = objLiberarPuntosResponse.audit.errorMsg
        Catch ex As Exception
            Throw ex
        Finally
            objGestionarPuntosService = Nothing
            objLiberarPuntosRequest = Nothing
            objLiberarPuntosResponse = Nothing
        End Try
    End Sub


    Public Sub New()
        _NOMBRE_SERVER = System.Net.Dns.GetHostName()
        IP_SERVER = System.Net.Dns.GetHostByName(_NOMBRE_SERVER).AddressList(0).ToString()
        APLICACION = ConfigurationSettings.AppSettings("constAplicacion").ToString()
    End Sub
End Class
