'PROY-31850 - INICIO
Imports System
Imports System.Collections
Imports System.Net
Imports System.Text
Imports COM_SIC_Activaciones.BWMobileNumberService
Imports System.Configuration

Public Class BWMobileNumberService

    Dim _oTransaccion As New WSMobileNumberService.BSS_MobileNumberService

    Public Sub New()
        _oTransaccion.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("UrlListMobileNumbers"))
        _oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
        _oTransaccion.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("TimeOutListMobileNumbers"))
    End Sub

    Public Function ObtenerLineaOLO(ByVal strIdTransaccion As String, ByVal strCodAplicacion As String, _
                                    ByVal strUserAplicacion As String, ByRef strCodMensaje As String, ByRef strDescMensaje As String) As String

        Dim strLineaOLO As String = ""
        Dim keyOLO As New clsKeyOLO

        Dim strRespuesta As String = ""

        Dim cntNumeros As Integer
        Dim objHeaderRequest As New WSMobileNumberService.HeaderRequest
        Dim objRequestMobile As New WSMobileNumberService.ListMobileNumbersRequestType

        Dim oResponseMobile As New WSMobileNumberService.ListMobileNumbersResponseType
        Dim objHeaderResponse As New WSMobileNumberService.HeaderResponse

        Try
            'HeaderRequest
            objHeaderRequest.channel = keyOLO.strMobileNumberWS_Channel
            objHeaderRequest.idApplication = strCodAplicacion
            objHeaderRequest.userApplication = strUserAplicacion
            objHeaderRequest.userSession = strUserAplicacion
            objHeaderRequest.idBusinessTransaction = strIdTransaccion
            objHeaderRequest.idESBTransaction = strIdTransaccion
            objHeaderRequest.startDate = Nothing
            objHeaderRequest.additionalNode = keyOLO.strMobileNumberWS_AdditionalNode

            'Request
            objRequestMobile.username = keyOLO.strMobileNumberWS_UserName
            objRequestMobile.password = keyOLO.strMobileNumberWS_Password
            objRequestMobile.agentID = keyOLO.strMobileNumberWS_AgentID
            objRequestMobile.groupID = keyOLO.strMobileNumberWS_GroupID
            objRequestMobile.state = keyOLO.strMobileNumberWS_State
            objRequestMobile.pageKey = keyOLO.strMobileNumberWS_PageKey
            objRequestMobile.limit = keyOLO.strMobileNumberWS_Limit
            objRequestMobile.isAscending = keyOLO.strMobileNumberWS_IsAscending
            objRequestMobile.paidType = keyOLO.strMobileNumberWS_PaIDType

            Dim oRequestOpcional() As WSMobileNumberService.AttributeValuePair = New WSMobileNumberService.AttributeValuePair((1) - 1) {}
            Dim oMobileNumberOpcional As WSMobileNumberService.AttributeValuePair = New WSMobileNumberService.AttributeValuePair

            oMobileNumberOpcional.attributeName = keyOLO.strMobileNumberWS_attributeName
            oMobileNumberOpcional.attributeValue = keyOLO.strMobileNumberWS_attributeValue
            oRequestOpcional(0) = oMobileNumberOpcional
            objRequestMobile.listaRequestOpcional = oRequestOpcional

            'Ejecuta Header
            _oTransaccion.headerRequest = objHeaderRequest

            'Ejecuta OSB - Carga Response
            oResponseMobile = _oTransaccion.listMobileNumbers(objRequestMobile)

            'Captura Response
            objHeaderResponse = _oTransaccion.headerResponse

            cntNumeros = oResponseMobile.responseData.parametersBody.Length
            strCodMensaje = oResponseMobile.responseStatus.codeResponse
            strDescMensaje = oResponseMobile.responseStatus.descriptionResponse

            Dim Random As New Random
            Dim randomNumber As Integer = Random.Next(1, cntNumeros)
            strLineaOLO = oResponseMobile.responseData.parametersBody(randomNumber).identifier

        Catch ex As Exception
            strCodMensaje = "99"
            strDescMensaje = ex.Message
        Finally
            oResponseMobile = Nothing
            objRequestMobile = Nothing
        End Try

        Return strLineaOLO

    End Function
End Class
'PROY-31850 - FIN