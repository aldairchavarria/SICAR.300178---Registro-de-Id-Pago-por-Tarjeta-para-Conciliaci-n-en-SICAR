'PROY-31850 - INICIO
Imports System
Imports System.Collections
Imports System.Net
Imports System.Text

Imports System.Configuration

Public Class BWProcesaVentaActivacionOlo

    Dim _oTransaccion As New WSProcesaVentaActivacionOlo.ProcesaVentaActivacionOloWSService

    Public Sub New()

        _oTransaccion.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("UrlProcesaVentaOLO"))
        _oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
        _oTransaccion.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("TimeOutProcesaVentaOLO"))

    End Sub

    Public Function ProcesarActivacionOLO(ByVal strIdPedido As String, ByVal strIdVenta As String, ByVal strTipoDocumento As String, ByVal strNumDocumento As String, _
                                          ByVal strIdTransaccion As String, ByVal strCodAplicacion As String, ByVal strIpAplicacion As String, _
                                          ByVal strUSerAplicacion As String, ByVal strTipoOperacionOLO As String, ByVal strIdPlanOLO As String, ByVal strIdService As String, _
                                          ByRef strCodRpta As String, ByRef strMgsRpta As String)

        Dim keyOLO As New clsKeyOLO
        Dim strRespuesta As String = ""

        Dim objHeaderRequest As New WSProcesaVentaActivacionOlo.auditRequestType
        Dim objRequestProcesa As New WSProcesaVentaActivacionOlo.procesarVentaActivacionRequest

        Dim objResponseProcesa As New WSProcesaVentaActivacionOlo.procesarVentaActivacionResponse
        Dim objHeaderResponse As New WSProcesaVentaActivacionOlo.auditResponseType


        Try
            'Header
            objHeaderRequest.idTransaccion = strIdTransaccion
            objHeaderRequest.ipAplicacion = strIpAplicacion
            objHeaderRequest.usuarioAplicacion = strUSerAplicacion
            objHeaderRequest.nombreAplicacion = strUSerAplicacion
            objHeaderRequest.msgid = strIdTransaccion

            'Request
            objRequestProcesa.tipoDocumento = strTipoDocumento
            objRequestProcesa.nroDocumento = strNumDocumento
            objRequestProcesa.idPedido = strIdPedido
            objRequestProcesa.idVenta = strIdVenta
            objRequestProcesa.tipoOperacion = strTipoOperacionOLO
            objRequestProcesa.idRecarga = strIdPlanOLO
            objRequestProcesa.serviceId = strIdService
            objRequestProcesa.auditRequest = objHeaderRequest


            Dim oRequestOpcional() As WSProcesaVentaActivacionOlo.parametrosTypeObjetoOpcional = New WSProcesaVentaActivacionOlo.parametrosTypeObjetoOpcional((1) - 1) {}
            Dim oProcesaVentaOpcional As WSProcesaVentaActivacionOlo.parametrosTypeObjetoOpcional = New WSProcesaVentaActivacionOlo.parametrosTypeObjetoOpcional
            'Que envia
            oProcesaVentaOpcional.campo = ""
            oProcesaVentaOpcional.valor = ""
            oRequestOpcional(0) = oProcesaVentaOpcional
            objRequestProcesa.listaRequestOpcional = oRequestOpcional

            objResponseProcesa = _oTransaccion.procesarVentaActivacion(objRequestProcesa)

            strCodRpta = Funciones.CheckStr(objResponseProcesa.auditResponse.codigoRespuesta)
            strMgsRpta = Funciones.CheckStr(objResponseProcesa.auditResponse.mensajeRespuesta)

        Catch ex As Exception
            strMgsRpta = ex.Message
            strCodRpta = "99"
        Finally
            objResponseProcesa = Nothing
            objRequestProcesa = Nothing
        End Try

    End Function

End Class
'PROY-31850 - FIN