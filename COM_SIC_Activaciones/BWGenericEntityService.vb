'PROY-31850 - INICIO
Imports System
Imports System.Collections
Imports System.Net
Imports System.Text

Imports System.Configuration

Public Class BWGenericEntityService

    Dim _oTransaccion As New WSGenericEntityService.BSS_ConsultaEquipoMovil

    Public Sub New()
        _oTransaccion.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("UrlGenericEntityService"))
        _oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
        _oTransaccion.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("TimeOutGenericEntityService"))
    End Sub

    Public Function ReservarLineaOLO(ByVal strIdTransaccion As String, ByVal strCodAplicacion As String, ByVal strUserAplicacion As String, _
                                     ByVal strLineaOLO As String, ByRef strCodRpta As String, ByRef strMgsRpta As String) As String

        Dim keyOLO As New clsKeyOLO
        Dim strRespuesta As String = ""

        Dim objHeaderRequest As New WSGenericEntityService.HeaderRequest
        Dim objRequestGeneric As New WSGenericEntityService.UpdateEntityRequestType

        Dim objResponseGeneric As New WSGenericEntityService.UpdateEntityResponseType
        Dim objHeaderResponse As New WSGenericEntityService.HeaderResponse

        'oOpcional
        Dim oOpcional() As WSGenericEntityService.AttributeValuePair


        Try
            'Header
            objHeaderRequest.channel = keyOLO.strGenericEntityWS_Channel
            objHeaderRequest.idApplication = strCodAplicacion
            objHeaderRequest.userApplication = strUserAplicacion
            objHeaderRequest.userSession = strUserAplicacion
            objHeaderRequest.idBusinessTransaction = strIdTransaccion
            objHeaderRequest.idESBTransaction = strIdTransaccion
            objHeaderRequest.startDate = Nothing
            objHeaderRequest.additionalNode = keyOLO.strGenericEntityWS_AdditionalNode

            objRequestGeneric.username = keyOLO.strGenericEntityWS_UserName
            objRequestGeneric.password = keyOLO.strGenericEntityWS_Password
            objRequestGeneric.transactionID = keyOLO.strGenericEntityWS_TransactionID
            objRequestGeneric.agentID = keyOLO.strGenericEntityWS_AgentID

            Dim objProperties(1) As WSGenericEntityService.Properties
            objProperties(0) = New WSGenericEntityService.Properties
            objProperties(0).msisdn = strLineaOLO
            objProperties(0).reserveDays = keyOLO.strGenericEntityWS_ReserveDays
            objProperties(0).reserve = keyOLO.strGenericEntityWS_Reserve_SicarOLO
            objProperties(0).spid = keyOLO.strGenericEntityWS_SpID

            objRequestGeneric.parametersHeader = objProperties

            Dim oRequestOpcional() As WSGenericEntityService.AttributeValuePair = New WSGenericEntityService.AttributeValuePair((1) - 1) {}
            Dim oGenericEntityOpcional As WSGenericEntityService.AttributeValuePair = New WSGenericEntityService.AttributeValuePair

            oGenericEntityOpcional.attributeName = keyOLO.strGenericEntityWS_attributeName
            oGenericEntityOpcional.attributeValue = keyOLO.strGenericEntityWS_attributeValue
            oRequestOpcional(0) = oGenericEntityOpcional
            objRequestGeneric.listaRequestOpcional = oRequestOpcional

            objRequestGeneric.listaRequestOpcional = oOpcional
            _oTransaccion.headerRequest = objHeaderRequest
            objResponseGeneric = _oTransaccion.updateEntity(objRequestGeneric)
            objHeaderResponse = _oTransaccion.headerResponse

            strCodRpta = objResponseGeneric.responseStatus.codeResponse
            strMgsRpta = objResponseGeneric.responseStatus.descriptionResponse

        Catch ex As Exception
            strMgsRpta = ex.Message
            strCodRpta = "99"
        Finally
            objResponseGeneric = Nothing
            objRequestGeneric = Nothing
        End Try
        Return strMgsRpta
    End Function

End Class
'PROY-31850 - FIN