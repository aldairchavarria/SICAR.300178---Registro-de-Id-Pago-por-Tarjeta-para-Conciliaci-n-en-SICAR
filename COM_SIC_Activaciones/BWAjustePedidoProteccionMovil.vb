'PROY-24724 - IIteracion 3 - INICIO
Imports System.Configuration
Imports System.Net

Public Class BWAjustePedidoProteccionMovil

    Dim oAjustePedidoProteccionMovil As New AjustePedidoAsurionWS.AjustePedidoAsurionWS

    Public Function AjustarPedido(ByVal strNroPedido As String, ByVal strIdTransaccion As String, ByVal strIpAplicacion As String, ByVal strNombreaplicacion As String, _
                                          ByVal strUsuarioAplicacion As String, ByRef strCodRpta As String, ByRef strMgsRpta As String)


        Dim objRequest = New AjustePedidoAsurionWS.ajustarPedidoRequest
        Dim objResponse = New AjustePedidoAsurionWS.ajustarPedidoResponse
        Dim objAuditRequest = New AjustePedidoAsurionWS.auditRequestType

        oAjustePedidoProteccionMovil.Url = ConfigurationSettings.AppSettings("consAjusteProteccionMovilWS_URL")
        oAjustePedidoProteccionMovil.Timeout = ConfigurationSettings.AppSettings("consAjusteProteccionMovilWS_TimeOut")

        objAuditRequest.idTransaccion = strIdTransaccion
        objAuditRequest.ipAplicacion = strIpAplicacion
        objAuditRequest.nombreAplicacion = strNombreaplicacion
        objAuditRequest.usuarioAplicacion = strUsuarioAplicacion

        objRequest.auditRequest = objAuditRequest
        objRequest.numPedido = strNroPedido

        objResponse = oAjustePedidoProteccionMovil.ajustarPedido(objRequest)

        strCodRpta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
        strMgsRpta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)

    End Function

End Class
'PROY-24724 - IIteracion 3 - FIN
