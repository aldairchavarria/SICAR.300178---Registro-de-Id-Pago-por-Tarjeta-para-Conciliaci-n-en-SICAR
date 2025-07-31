Imports System.Configuration

Public Class clsPagosWS

    'Dim oTransaccion As New COM_SIC_Activaciones.WSSimCards.ebsSimcards
    Dim oTransaccionS As New COM_SIC_Activaciones.PagosWS.ebsPagoWS

    '**IDG: ANULACIONES:
    Public Function AnularPedidoPago(ByVal numeroPedidoVenta As String, _
                                    ByVal numeroPagoInterno As String, _
                                    ByVal claseFactura As String, _
                                    ByVal sociedadVentaPedido As String, _
                                    ByVal canalDistribucion As String, _
                                    ByVal sector As String, _
                                    ByVal numeroDocumentoSAP As String, _
                                    ByVal fechaContable As String, _
                                    ByVal sociedadVentaPago As String, _
                                    ByVal documentoCompensacion As String, _
                                    ByVal numeroPuntoVenta As String, _
                                    ByVal flag As String, _
                                    ByVal CurrentUser As String, _
                                    ByVal CurrentTerminal As String, _
                                    ByRef K_COD_RESPUESTA As String, _
                                    ByRef K_MSJ_RESPUESTA As String, _
                                    ByRef K_ID_TRANSACCION As String)

        Dim objRequest
        objRequest = New COM_SIC_Activaciones.PagosWS.anularPedidoPagoRequest

        Dim objResponse
        objResponse = New COM_SIC_Activaciones.PagosWS.anularPedidoPagoResponse
        oTransaccionS.Url = Configuration.ConfigurationSettings.AppSettings("consRutaWSPagos_Sinergia")

        Try


            Dim objResponseAuditRequest As New COM_SIC_Activaciones.PagosWS.AuditRequest

            objResponseAuditRequest.idTransaccion = Format(Now, "yyyyMMddmmss") & numeroPedidoVenta.ToString
            objResponseAuditRequest.ipAplicacion = CurrentTerminal
            objResponseAuditRequest.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
            objResponseAuditRequest.usuarioAplicacion = CurrentUser


            objRequest.numeroPedidoVenta = numeroPedidoVenta
            objRequest.numeroPagoInterno = numeroPagoInterno
            objRequest.claseFactura = claseFactura
            objRequest.sociedadVentaPedido = sociedadVentaPedido
            objRequest.canalDistribucion = canalDistribucion
            objRequest.sector = sector
            objRequest.numeroDocumentoSAP = numeroDocumentoSAP
            objRequest.fechaContable = fechaContable
            objRequest.sociedadVentaPago = sociedadVentaPago
            objRequest.documentoCompensacion = documentoCompensacion
            objRequest.numeroPuntoVenta = numeroPuntoVenta
            objRequest.flag = flag
            objRequest.auditRequest = objResponseAuditRequest


            objResponse = oTransaccionS.anularPedidoPago(objRequest)

            K_COD_RESPUESTA = objResponse.auditResponse.mensajeRespuesta.ToString()
            K_ID_TRANSACCION = objResponse.auditResponse.idTransaccion
            K_MSJ_RESPUESTA = objResponse.auditResponse.mensajeRespuesta.ToString

        Catch ex As Exception
            K_COD_RESPUESTA = "1"
            K_ID_TRANSACCION = "0"
            K_MSJ_RESPUESTA = "Error. " + ex.Message.ToString()
        Finally
            objRequest = Nothing
            objResponse = Nothing
        End Try

    End Function



    '**IDG: DEVOLUCIÒN 07.05.2015:
    Public Function DevolucionEfectivo(ByVal sociedad As String, _
                                       ByVal puntoVenta As String, _
                                        ByVal fechaRegistro As String, _
                                            ByVal documentoSAPFactura As String, _
                                              ByVal documentoSAPNotaFactura As String, _
                                       ByVal origen As String, _
                                                ByVal montoDevolucion As String, _
                                                    ByVal usuario As String, _
                                                        ByVal numeroPedidoVenta As String, _
                                                            ByVal numeroPagoInterno As String, _
                                                             ByVal CurrentUser As String, _
                                                                ByVal CurrentTerminal As String, _
                                       ByVal Moneda As String, _
                                       ByVal TipoDevolucion As String, _
                                                                    ByRef K_COD_RESPUESTA As String, _
                                                                        ByRef K_MSJ_RESPUESTA As String, _
                                                                            ByRef K_ID_TRANSACCION As String)

        Dim objRequest
        objRequest = New COM_SIC_Activaciones.PagosWS.devolverEfectivoRequest

        Dim objResponse
        objResponse = New COM_SIC_Activaciones.PagosWS.devolverEfectivoResponse
        oTransaccionS.Url = Configuration.ConfigurationSettings.AppSettings("consRutaWSPagos_Sinergia")

        Try


            Dim objResponseAuditRequest As New COM_SIC_Activaciones.PagosWS.AuditRequest

            objResponseAuditRequest.idTransaccion = Format(Now, "yyyyMMddmmss") & numeroPedidoVenta.ToString
            objResponseAuditRequest.ipAplicacion = CurrentTerminal
            objResponseAuditRequest.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
            objResponseAuditRequest.usuarioAplicacion = CurrentUser

            objRequest.sociedad = sociedad
            objRequest.puntoVenta = puntoVenta
            objRequest.fechaRegistro = fechaRegistro
            objRequest.documentoSAPFactura = documentoSAPFactura
            objRequest.documentoSAPNotaFactura = documentoSAPNotaFactura
            objRequest.origen = origen
            objRequest.montoDevolucion = montoDevolucion
            objRequest.tipoMoneda = Moneda
            objRequest.usuario = usuario
            objRequest.numeroPedidoVenta = numeroPedidoVenta
            objRequest.numeroPagoInterno = numeroPagoInterno

            Dim oRequestOpcional() As PagosWS.ListaCamposOpcionalesObjetoResponseOpcional = New PagosWS.ListaCamposOpcionalesObjetoResponseOpcional((1) - 1) {}
            Dim oParametroOpcional As PagosWS.ListaCamposOpcionalesObjetoResponseOpcional = New PagosWS.ListaCamposOpcionalesObjetoResponseOpcional
            'oRequestOpcional(0) = New PagosWS.ListaCamposOpcionalesObjetoResponseOpcional
            oParametroOpcional.campo = "P_DEVOL"
            oParametroOpcional.valor = TipoDevolucion
            oRequestOpcional(0) = oParametroOpcional
            objRequest.listaRequestOpcional = oRequestOpcional



            'Dim oRequestOpcional() As WSGenericEntityService.AttributeValuePair = New WSGenericEntityService.AttributeValuePair((1) - 1) {}
            'Dim oGenericEntityOpcional As WSGenericEntityService.AttributeValuePair = New WSGenericEntityService.AttributeValuePair

            'oGenericEntityOpcional.attributeName = keyOLO.strGenericEntityWS_attributeName
            'oGenericEntityOpcional.attributeValue = keyOLO.strGenericEntityWS_attributeValue
            'oRequestOpcional(0) = oGenericEntityOpcional
            'objRequestGeneric.listaRequestOpcional = oRequestOpcional






            objRequest.auditRequest = objResponseAuditRequest
            objResponse = oTransaccionS.devolverEfectivo(objRequest)

            K_COD_RESPUESTA = objResponse.auditResponse.mensajeRespuesta.ToString()
            K_ID_TRANSACCION = objResponse.auditResponse.idTransaccion
            K_MSJ_RESPUESTA = objResponse.auditResponse.mensajeRespuesta.ToString

        Catch ex As Exception
            K_COD_RESPUESTA = "1"
            K_ID_TRANSACCION = "0"
            K_MSJ_RESPUESTA = "Error. " + ex.Message.ToString()
        Finally
            objRequest = Nothing
            objResponse = Nothing
        End Try

    End Function

    Public Function RegistrarPagoSap(ByVal K_PEDIN_NROPEDIDO As Int64, _
                                     ByVal K_PAGON_IDPAGO As Int64, _
                                     ByVal CurrentUser As String, _
                                     ByVal CurrentTerminal As String, _
                                     ByRef K_COD_RESPUESTA As String, _
                                     ByRef K_MSJ_RESPUESTA As String, _
                                     ByRef K_ID_TRANSACCION As String, _
                                     ByVal K_USUARIO As String)

        Dim objRequest
        objRequest = New COM_SIC_Activaciones.PagosWS.registrarPagoRequest

        Dim objResponse
        objResponse = New COM_SIC_Activaciones.PagosWS.registrarPagoResponse
        oTransaccionS.Url = Configuration.ConfigurationSettings.AppSettings("consRutaWSPagos_Sinergia")

        Try


            Dim objResponseAuditRequest As New COM_SIC_Activaciones.PagosWS.AuditRequest

            objResponseAuditRequest.idTransaccion = Format(Now, "yyyyMMddmmss") & K_PEDIN_NROPEDIDO.ToString
            objResponseAuditRequest.ipAplicacion = CurrentTerminal
            objResponseAuditRequest.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
            objResponseAuditRequest.usuarioAplicacion = CurrentUser

            objRequest.sap = ConfigurationSettings.AppSettings("constSinergia")
            objRequest.numeroPedidoVenta = K_PEDIN_NROPEDIDO
            objRequest.numeroPagoInterno = K_PAGON_IDPAGO
            objRequest.auditRequest = objResponseAuditRequest


            objResponse = oTransaccionS.registrarPago(objRequest)

            K_COD_RESPUESTA = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
            K_ID_TRANSACCION = objResponse.auditResponse.idTransaccion
            K_MSJ_RESPUESTA = objResponse.auditResponse.mensajeRespuesta.ToString

        Catch ex As Exception
            K_COD_RESPUESTA = "1"
            K_ID_TRANSACCION = "0"
            K_MSJ_RESPUESTA = "Error. " + ex.Message.ToString()
        Finally
            objRequest = Nothing
            objResponse = Nothing
        End Try

    End Function


End Class
