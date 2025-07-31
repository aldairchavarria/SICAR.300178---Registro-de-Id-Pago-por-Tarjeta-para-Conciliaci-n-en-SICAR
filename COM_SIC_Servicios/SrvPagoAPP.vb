Imports System.Globalization
Imports System.Configuration
Imports COM_SIC_Servicios.Funciones
Imports COM_SIC_Servicios.clsActivaciones
Imports COM_SIC_Servicios.WebComunes
Imports COM_SIC_Activaciones
Imports COM_SIC_Cajas
Imports System.Net
Imports COM_SIC_INActChip
Imports System.IO
Imports System.Text
Imports COM_SIC_FacturaElectronica
Imports NEGOCIO_SIC_SANS
Imports System.Xml
Imports System.Web
Imports COM_SIC_Entidades.DataPowerRest.ProcesarPagosDelivery.RestConsultarDatosPago
Imports COM_SIC_Entidades.DataPowerRest.RestServices.ProcesarPagosDelivery.ActualizarEstadoPago
Imports COM_SIC_Entidades.DataPowerRest.RestServices.ConsultarDatosPago
Imports COM_SIC_Entidades.DataPowerRest.MethodsRest.ProcesarPagosDelivery.RestActualizarEstadoPago
Imports COM_SIC_Entidades

'Imports COM_SIC_Entidades.DataPowerRest.RestServices
'Imports COM_SIC_Entidades.DataPowerRest.RestConsultarDatosPago

Public Class SrvPagoAPP
    'Inherits System.Web.UI.Page

    Private Shared oTipiConfiguration As New clsTipificationConfiguration
    Dim objFileLog As New SrvPago_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim IdentificadorLog As String
    Dim ipServer As String = String.Empty
    Dim usuario_id As String = String.Empty
    Dim ipcliente As String = String.Empty
    Dim isRentaAdelantada As Boolean = False
    Dim drPagos As DataRow
    Dim blnError As Boolean
    Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
    Dim objTrsMsSap As New COM_SIC_Activaciones.clsTrsMsSap
    Dim strRentaAdelantada As String = String.Empty

    Dim strTipoDocClientePed As String = String.Empty
    Dim strNroDocClientePed As String = String.Empty
    Dim strFechaPedido As String = String.Empty

    Dim strNodo As String = String.Empty
    Dim strCanal As String = String.Empty
    Dim strUsuario As String = String.Empty
    Dim strNUsuario As String = String.Empty
    Dim strCodPerfil As String = String.Empty
    Dim strIpLocal As String = String.Empty
    Dim strNumeroDocumento As String = String.Empty
    Dim strMontoRecarga As String = String.Empty
    Dim strCodigoPago As String = String.Empty
    Dim oEmpleados As BEDatosEmpleado = New BEDatosEmpleado
    Dim strTipoDocVendedor As String = String.Empty
    Dim strNumDocVendedor As String = String.Empty
    Dim strTipoOperacionServ As String = String.Empty
    Dim strFlagTiendaVirtual As String = String.Empty
    Dim strMontoTotal As String = String.Empty
    Dim strNumReferencia As String = String.Empty
    Dim strFlagBiometria As String = String.Empty
    Dim strNumeroSEC As String = String.Empty
    Dim strTipoVentaPago As String = String.Empty
    Dim strTipoOperacionPago As String = String.Empty
    Dim blFlagPortabilidad As Boolean = False

    'JRM
    Dim strCodigoPagoRA As String = String.Empty
    Dim strNumReferenciaRA As String = String.Empty
    Dim arrayBoleta As Array
    Dim arrayRA As Array
    'JRM
    'PROY-140582 - INI
    Dim strFlagPM As String = String.Empty
    Dim strPedidoPM As String = String.Empty
    'PROY-140582 - FIN
    'JRM - PROY 140589
    Dim FlagCostoDlv As String
    Dim FlagIsPedidoCostoDlv As String
    Dim NroPedidoCostoDlv As String
    Dim CodRptaCostoDLV As String
    Dim MsjRptaCostoDlv As String
    Dim strFlagBiometriaDlv As String
    Dim strMontoPagarDlv As String
    Dim strMontoPagarRADlv As String
    Dim strEstadoPedidoDlv As String
    Dim strFlagPortabilidad As String
    Dim strIsPortabilidad As String
    'JRM - PROY 140589

    'PROY-140590 IDEA142068 - INICIO
    Dim strTarjeta As String = String.Empty
    Dim strCodCampana As String = String.Empty
    Dim validacionBinTarjeta As Boolean = False
    'PROY-140590 IDEA142068 - FIN

    'INICIATIVA-1006 - INI
    Dim intNroPedACC_cos As Int64
    Dim strFlagACC_cos As String
    Dim strEstadoPed_ACC_Cos As String
    Dim strIdentificadorProceso As String
    'INICIATIVA-1006 - FIN

    Public Function RealizarPago(ByVal UUID As String, ByVal idPedido As String) As BEResponseWebMethod

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "###################################################################################"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "################### RealizarPago - INICIO PROCESO REALIZAR PAGO ###################"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "###################################################################################"))

        IdentificadorLog = String.Format("{0} || {1}", Funciones.CheckStr(idPedido), Funciones.CheckStr(UUID))

        Dim responseRealizarPago As BEResponseWebMethod = New BEResponseWebMethod
        Dim oCabeceraActualizar As BECabeceraEstadoPago = New BECabeceraEstadoPago
        Dim rptaActualizar As Boolean = False
        Dim flagErrorPago As Boolean = False
        Dim flagExitoPago As Boolean = False
        Dim flagErrorPagoRA As Boolean = False
        'PROY-140582 - INI
        Dim flagErrorPagoPM As Boolean = False
        'PROY-140582 - FIM

        Dim flagExitoPagoACC As Boolean = True ' INICIATIVA-1006

        Try

            Dim responseRA As BEResponseWebMethod = New BEResponseWebMethod
            Dim responsePagarBoleta As BEResponseWebMethod = New BEResponseWebMethod
            Dim responseInicio As BEResponseWebMethod = New BEResponseWebMethod
            Dim objRentaAdelantada As RentaAdelantada = New RentaAdelantada
            Dim objDetaPago As DetaPago = New DetaPago
            'PROY-140582 - INI
            Dim responsePM As BEResponseWebMethod = New BEResponseWebMethod
            'PROY-140582 - FIN

            Dim responseACC_Costo As BEResponseWebMethod = New BEResponseWebMethod ' INICIATIVA-1006

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "RealizarPago - Inicio Obtener nodo de pago"))

            strNodo = Funciones.CheckStr(CurrentNodo())

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - Nodo", strNodo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "RealizarPago - Fin Obtener nodo de pago"))

            If Inicio(UUID, idPedido, responseInicio) Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - Inicio => True")

                responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responseInicio.CodigoRespuesta)
                responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responseInicio.MensajeRespuesta)

'JRM - PROY 140589 INI
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "PROY 140589 -- Inicio Pago Dlv")
                Dim ExitoPagoDlv As Boolean = True
                If FlagIsPedidoCostoDlv.Equals("1") Or FlagCostoDlv.Equals("1") Then
                    If strEstadoPedidoDlv <> ConfigurationSettings.AppSettings("ESTADO_PAG") And strEstadoPedidoDlv <> ConfigurationSettings.AppSettings("Estado_Anulacion") Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Realizar Pago Delivery -> El pedido se encuentra en estado -> " & strEstadoPedidoDlv)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Realizar Pago Delivery -> Se procederá con el Pago ")
                        If objDetaPago.RegistrarPagoDLV(NroPedidoCostoDlv, _
                                                  idPedido, _
                                                  strNumeroDocumento, _
                                                  strMontoRecarga, _
                                                  strCodigoPago, _
                                                  oEmpleados, _
                                                  strIpLocal, _
                                                  strTipoDocClientePed, _
                                                  strNroDocClientePed, _
                                                  strTipoDocVendedor, _
                                                  strNumDocVendedor, _
                                                  "", _
                                                  strTipoOperacionServ, _
                                                  strMontoPagarDlv, _
                                                  strFlagTiendaVirtual, _
                                                  strFlagBiometriaDlv, _
                                                  UUID, _
                                                  strFechaPedido, _
                                                  strTipoVentaPago, _
                                                  CheckStr(KeySettings.Key_K_PEDIC_CODTIPOOPERACION), _
                                                  blFlagPortabilidad, _
                                                  responsePagarBoleta) Then
                            objRentaAdelantada.RegistrarDetaMedPago(NroPedidoCostoDlv, arrayBoleta)

                        Else
                            ExitoPagoDlv = False
                            responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta)
                            responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta)
                        End If
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Realizar Pago Delivery -> El pedido se encuentra en estado -> " & strEstadoPedidoDlv)
                    End If
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "PROY 140589 -- Fin Pago Dlv")
                'JRM - PROY 140589 FIN

                'INICIATIVA-1006| TIENDA VIRTUAL - ACC CON COSTO | Pago del accesorio con costo | INI

                If strFlagACC_cos.Equals("1") AndAlso strEstadoPed_ACC_Cos.Equals(ConfigurationSettings.AppSettings("PEDIC_ESTADO")) AndAlso ExitoPagoDlv Then

                    strIdentificadorProceso = "[PAGO ACCESORIO CON COSTO]"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "[INICIATIVA-1006][TIENDA VIRTUAL - ACC CON COSTO][ INICIO PAGO DE ACCESORIO CON COSTO]" & "")

                    If objDetaPago.PagarPM(Funciones.CheckStr(intNroPedACC_cos), _
                                           strNumeroDocumento, _
                                           strMontoRecarga, _
                                           strCodigoPago, _
                                           oEmpleados, _
                                           strIpLocal, _
                                           strTipoDocClientePed, _
                                           strNroDocClientePed, _
                                           strTipoDocVendedor, _
                                           strNumDocVendedor, _
                                           "", _
                                           strTipoOperacionServ, _
                                           strMontoTotal, _
                                           strFlagTiendaVirtual, _
                                           strFlagBiometria, _
                                           UUID, _
                                           False, _
                                           idPedido, _
                                           strIdentificadorProceso, _
                                           responseACC_Costo) Then

                        objRentaAdelantada.RegistrarDetaMedPago(Funciones.CheckStr(intNroPedACC_cos), arrayBoleta)

                    Else
                        flagExitoPagoACC = False
                        flagErrorPago = True

                        responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responseACC_Costo.CodigoRespuesta)
                        responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responseACC_Costo.MensajeRespuesta)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.CodigoRespuesta => " & Funciones.CheckStr(responseACC_Costo.CodigoRespuesta))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.MensajeRespuesta => " & Funciones.CheckStr(responseACC_Costo.MensajeRespuesta))

                    End If


                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "[INICIATIVA-1006][TIENDA VIRTUAL - ACC CON COSTO][ FIN PAGO DE ACCESORIO CON COSTO]" & "")
                End If

                'INICIATIVA-1006| FIN

                If ExitoPagoDlv AndAlso flagExitoPagoACC Then

                If isRentaAdelantada Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - isRentaAdelantada => True")

                    If objRentaAdelantada.Pagar(strRentaAdelantada, _
                                                 strNumeroDocumento, _
                                                 strMontoRecarga, _
                                                 strCodigoPagoRA, _
                                                 oEmpleados, _
                                                 strIpLocal, _
                                                 strTipoDocVendedor, _
                                                 strNumDocVendedor, _
                                                 "", _
                                                 strTipoOperacionServ, _
                                                  strMontoTotal, _
                                                  strFlagTiendaVirtual, _
                                                  strFlagBiometria, _
                                                 idPedido, _
                                                 UUID, _
                                                 responseRA) Then

                        objRentaAdelantada.RegistrarDetaMedPago(strRentaAdelantada, arrayRA)

                        flagExitoPago = True
                        responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responseRA.CodigoRespuesta)
                        responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responseRA.MensajeRespuesta)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "RealizarPago - Pago RA realizado correctamente"))

                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRA.CodigoRespuesta => " & responseRA.CodigoRespuesta)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRA.MensajeRespuesta => " & responseRA.MensajeRespuesta)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - rptaActualizar", Funciones.CheckStr(rptaActualizar)))


                    Else
                        flagErrorPagoRA = True
                        responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responseRA.CodigoRespuesta)
                        responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responseRA.MensajeRespuesta)
                    End If

                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - isRentaAdelantada => False")
                    'PROY-140582 - INI
                    If strFlagPM.Equals("1") And Not ValidarPagoPM(idPedido) Then
                            strIdentificadorProceso = "[PAGO PROTECCIÓN MOVIL]"'INICIATIVA-1006
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "RealizarPago - ValidarPagoPM => No se ha realizado el pago de la PM"))

                        If objDetaPago.PagarPM(strPedidoPM, _
                                                strNumeroDocumento, _
                                                strMontoRecarga, _
                                                strCodigoPago, _
                                                oEmpleados, _
                                                strIpLocal, _
                                                strTipoDocClientePed, _
                                                strNroDocClientePed, _
                                                strTipoDocVendedor, _
                                                strNumDocVendedor, _
                                                "", _
                                                strTipoOperacionServ, _
                                                strMontoTotal, _
                                                strFlagTiendaVirtual, _
                                                strFlagBiometria, _
                                                UUID, _
                                                False, _
                                                idPedido, _
                                                    strIdentificadorProceso, _
                                                responsePM) Then

                            objRentaAdelantada.RegistrarDetaMedPago(strPedidoPM, arrayBoleta)

                            flagErrorPagoPM = True
                            responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responsePM.CodigoRespuesta)
                            responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responsePM.MensajeRespuesta)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - responsePM.CodigoRespuesta", responsePM.CodigoRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - responsePM.MensajeRespuesta", responsePM.MensajeRespuesta))

                    If Not ValidarPagoRA(idPedido) Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - ValidarPagoRA => No se ha realizado el pago de la RA ")

                        If objRentaAdelantada.Pagar(strRentaAdelantada, _
                                                 strNumeroDocumento, _
                                                 strMontoRecarga, _
                                                 strCodigoPago, _
                                                 oEmpleados, _
                                                 strIpLocal, _
                                                 strTipoDocVendedor, _
                                                 strNumDocVendedor, _
                                                 "", _
                                                 strTipoOperacionServ, _
                                                  strMontoTotal, _
                                                  strFlagTiendaVirtual, _
                                                  strFlagBiometria, _
                                                 idPedido, _
                                                 UUID, _
                                                 responseRA) Then

                            objRentaAdelantada.RegistrarDetaMedPago(strRentaAdelantada, arrayBoleta)

                            flagErrorPagoRA = True
                            responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responseRA.CodigoRespuesta)
                            responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responseRA.MensajeRespuesta)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRA.CodigoRespuesta => " & responseRA.CodigoRespuesta)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRA.MensajeRespuesta => " & responseRA.MensajeRespuesta)


                            If objDetaPago.Pagar(idPedido, _
                                                  strNumeroDocumento, _
                                                  strMontoRecarga, _
                                                  strCodigoPago, _
                                                  oEmpleados, _
                                                  strIpLocal, _
                                                  strTipoDocClientePed, _
                                                  strNroDocClientePed, _
                                                  strTipoDocVendedor, _
                                                  strNumDocVendedor, _
                                                  "", _
                                                  strTipoOperacionServ, _
                                                  strMontoTotal, _
                                                  strFlagTiendaVirtual, _
                                                  strFlagBiometria, _
                                                  UUID, _
                                                  strFechaPedido, _
                                                  strTipoVentaPago, _
                                                  strTipoOperacionPago, _
                                                  blFlagPortabilidad, _
                                                  responsePagarBoleta) Then

                                objRentaAdelantada.RegistrarDetaMedPago(idPedido, arrayBoleta)

    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.CodigoRespuesta => " & responsePagarBoleta.CodigoRespuesta)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.MensajeRespuesta => " & responsePagarBoleta.MensajeRespuesta)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.MensajeRespuesta => " & responsePagarBoleta.MensajeErrror)

                                If (Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta) = String.Empty) Then
                                    responsePagarBoleta.CodigoRespuesta = "0"
                                End If
                                If (Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta) = String.Empty) Then
                                    responsePagarBoleta.CodigoRespuesta = "Pago Realizado con éxito."
                                End If

                                responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta)
                                responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta)
                                responseRealizarPago.MensajeErrror = Funciones.CheckStr(responsePagarBoleta.MensajeErrror)

                                flagExitoPago = True

                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRealizarPago.CodigoRespuesta => " & Funciones.CheckStr(responseRealizarPago.CodigoRespuesta))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRealizarPago.MensajeRespuesta => " & Funciones.CheckStr(responseRealizarPago.MensajeRespuesta))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRealizarPago.MensajeErrror => " & Funciones.CheckStr(responseRealizarPago.MensajeErrror))

                            Else
                                flagErrorPago = True
                                responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta)
                                responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.CodigoRespuesta => " & Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.MensajeRespuesta => " & Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta))

                            End If

                        Else

                            responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responseRA.CodigoRespuesta)
                            responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responseRA.MensajeRespuesta)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRA.CodigoRespuesta => " & Funciones.CheckStr(responseRA.CodigoRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRA.MensajeRespuesta => " & Funciones.CheckStr(responseRA.MensajeRespuesta))

                        End If


                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - ValidarPagoRA => True")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - No tiene RA pendiente de pago asociado al pedido")

                        If objDetaPago.Pagar(idPedido, _
                                                  strNumeroDocumento, _
                                                  strMontoRecarga, _
                                                  strCodigoPago, _
                                                  oEmpleados, _
                                                  strIpLocal, _
                                                  strTipoDocClientePed, _
                                                  strNroDocClientePed, _
                                                  strTipoDocVendedor, _
                                                  strNumDocVendedor, _
                                                  "", _
                                                  strTipoOperacionServ, _
                                                  strMontoTotal, _
                                                  strFlagTiendaVirtual, _
                                                  strFlagBiometria, _
                                                  UUID, _
                                                  strFechaPedido, _
                                                  strTipoVentaPago, _
                                                  strTipoOperacionPago, _
                                                  blFlagPortabilidad, _
                                                  responsePagarBoleta) Then

                            objRentaAdelantada.RegistrarDetaMedPago(idPedido, arrayBoleta)

 objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.CodigoRespuesta => " & Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.MensajeRespuesta => " & Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.MensajeRespuesta => " & Funciones.CheckStr(responsePagarBoleta.MensajeErrror))

                            If (Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta) = String.Empty) Then
                                responsePagarBoleta.CodigoRespuesta = "0"
                            End If
                            If (Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta) = String.Empty) Then
                                responsePagarBoleta.CodigoRespuesta = "Pago Realizado con éxito."
                            End If

                            responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta)
                            responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta)
                            responseRealizarPago.MensajeErrror = Funciones.CheckStr(responsePagarBoleta.MensajeErrror)

                            flagExitoPago = True

                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRealizarPago.CodigoRespuesta => " & Funciones.CheckStr(responseRealizarPago.CodigoRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRealizarPago.MensajeRespuesta => " & Funciones.CheckStr(responseRealizarPago.MensajeRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRealizarPago.MensajeRespuesta => " & Funciones.CheckStr(responseRealizarPago.MensajeErrror))

                        Else
                            flagErrorPago = True
                            responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta)
                            responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.CodigoRespuesta => " & Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.MensajeRespuesta => " & Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta))

                        End If

                    End If

                        Else
                            responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responsePM.CodigoRespuesta)
                            responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responsePM.MensajeRespuesta)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - responsePM.CodigoRespuesta", responsePM.CodigoRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - responsePM.MensajeRespuesta", responsePM.MensajeRespuesta))
                        End If

                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "RealizarPago - ValidarPagoPM => True"))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "RealizarPago - ValidarPagoPM => No requiere pago de la PM"))

                    If Not ValidarPagoRA(idPedido) Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - ValidarPagoRA => No se ha realizado el pago de la RA ")

                        If objRentaAdelantada.Pagar(strRentaAdelantada, _
                                                 strNumeroDocumento, _
                                                 strMontoRecarga, _
                                                 strCodigoPago, _
                                                 oEmpleados, _
                                                 strIpLocal, _
                                                 strTipoDocVendedor, _
                                                 strNumDocVendedor, _
                                                 "", _
                                                 strTipoOperacionServ, _
                                                  strMontoTotal, _
                                                  strFlagTiendaVirtual, _
                                                  strFlagBiometria, _
                                                 idPedido, _
                                                 UUID, _
                                                 responseRA) Then

                            objRentaAdelantada.RegistrarDetaMedPago(strRentaAdelantada, arrayBoleta)

                            flagErrorPagoRA = True
                            responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responseRA.CodigoRespuesta)
                            responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responseRA.MensajeRespuesta)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRA.CodigoRespuesta => " & responseRA.CodigoRespuesta)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRA.MensajeRespuesta => " & responseRA.MensajeRespuesta)


                            If objDetaPago.Pagar(idPedido, _
                                                  strNumeroDocumento, _
                                                  strMontoRecarga, _
                                                  strCodigoPago, _
                                                  oEmpleados, _
                                                  strIpLocal, _
                                                  strTipoDocClientePed, _
                                                  strNroDocClientePed, _
                                                  strTipoDocVendedor, _
                                                  strNumDocVendedor, _
                                                  "", _
                                                  strTipoOperacionServ, _
                                                  strMontoTotal, _
                                                  strFlagTiendaVirtual, _
                                                  strFlagBiometria, _
                                                  UUID, _
                                                  strFechaPedido, _
                                                  strTipoVentaPago, _
                                                  strTipoOperacionPago, _
                                                  blFlagPortabilidad, _
                                                  responsePagarBoleta) Then

                                objRentaAdelantada.RegistrarDetaMedPago(idPedido, arrayBoleta)


                                If Not strTarjeta = String.Empty AndAlso Not strCodCampana = String.Empty AndAlso validacionBinTarjeta Then
                                    RegistrarPedidoSTBK(idPedido, strCodCampana, strTarjeta)
                                End If

                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.CodigoRespuesta => " & responsePagarBoleta.CodigoRespuesta)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.MensajeRespuesta => " & responsePagarBoleta.MensajeRespuesta)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.MensajeRespuesta => " & responsePagarBoleta.MensajeErrror)

                                If (Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta) = String.Empty) Then
                                    responsePagarBoleta.CodigoRespuesta = "0"
                                End If
                                If (Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta) = String.Empty) Then
                                    responsePagarBoleta.CodigoRespuesta = "Pago Realizado con éxito."
                                End If

                                responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta)
                                responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta)
                                responseRealizarPago.MensajeErrror = Funciones.CheckStr(responsePagarBoleta.MensajeErrror)

                                flagExitoPago = True

                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRealizarPago.CodigoRespuesta => " & Funciones.CheckStr(responseRealizarPago.CodigoRespuesta))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRealizarPago.MensajeRespuesta => " & Funciones.CheckStr(responseRealizarPago.MensajeRespuesta))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRealizarPago.MensajeErrror => " & Funciones.CheckStr(responseRealizarPago.MensajeErrror))

                            Else
                                flagErrorPago = True
                                responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta)
                                responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.CodigoRespuesta => " & Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.MensajeRespuesta => " & Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta))

                            End If

                        Else

                            responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responseRA.CodigoRespuesta)
                            responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responseRA.MensajeRespuesta)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRA.CodigoRespuesta => " & Funciones.CheckStr(responseRA.CodigoRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRA.MensajeRespuesta => " & Funciones.CheckStr(responseRA.MensajeRespuesta))

                        End If


                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - ValidarPagoRA => True")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - No tiene RA pendiente de pago asociado al pedido")

                        If objDetaPago.Pagar(idPedido, _
                                                  strNumeroDocumento, _
                                                  strMontoRecarga, _
                                                  strCodigoPago, _
                                                  oEmpleados, _
                                                  strIpLocal, _
                                                  strTipoDocClientePed, _
                                                  strNroDocClientePed, _
                                                  strTipoDocVendedor, _
                                                  strNumDocVendedor, _
                                                  "", _
                                                  strTipoOperacionServ, _
                                                  strMontoTotal, _
                                                  strFlagTiendaVirtual, _
                                                  strFlagBiometria, _
                                                  UUID, _
                                                  strFechaPedido, _
                                                  strTipoVentaPago, _
                                                  strTipoOperacionPago, _
                                                  blFlagPortabilidad, _
                                                  responsePagarBoleta) Then

                            objRentaAdelantada.RegistrarDetaMedPago(idPedido, arrayBoleta)

                            If Not strTarjeta = String.Empty AndAlso Not strCodCampana = String.Empty AndAlso validacionBinTarjeta Then
                                RegistrarPedidoSTBK(idPedido, strCodCampana, strTarjeta)
                            End If

                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.CodigoRespuesta => " & Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.MensajeRespuesta => " & Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.MensajeRespuesta => " & Funciones.CheckStr(responsePagarBoleta.MensajeErrror))

                            If (Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta) = String.Empty) Then
                                responsePagarBoleta.CodigoRespuesta = "0"
                            End If
                            If (Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta) = String.Empty) Then
                                responsePagarBoleta.CodigoRespuesta = "Pago Realizado con éxito."
                            End If

                            responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta)
                            responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta)
                            responseRealizarPago.MensajeErrror = Funciones.CheckStr(responsePagarBoleta.MensajeErrror)

                            flagExitoPago = True

                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRealizarPago.CodigoRespuesta => " & Funciones.CheckStr(responseRealizarPago.CodigoRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRealizarPago.MensajeRespuesta => " & Funciones.CheckStr(responseRealizarPago.MensajeRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseRealizarPago.MensajeRespuesta => " & Funciones.CheckStr(responseRealizarPago.MensajeErrror))

                        Else
                            flagErrorPago = True
                            responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta)
                            responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.CodigoRespuesta => " & Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.MensajeRespuesta => " & Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta))

                        End If

                    End If

                End If
                    'PROY-140582 - FIN

                End If

            Else
                responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responseInicio.CodigoRespuesta)
                responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responseInicio.MensajeRespuesta)

                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseInicio.CodigoRespuesta => " & Funciones.CheckStr(responseInicio.CodigoRespuesta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responseInicio.MensajeRespuesta => " & Funciones.CheckStr(responseInicio.MensajeRespuesta))

            End If
            Else
                flagErrorPago = True
                responseRealizarPago.CodigoRespuesta = Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta)
                responseRealizarPago.MensajeRespuesta = Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta)

                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.CodigoRespuesta => " & Funciones.CheckStr(responsePagarBoleta.CodigoRespuesta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - responsePagarBoleta.MensajeRespuesta => " & Funciones.CheckStr(responsePagarBoleta.MensajeRespuesta))
            End If




            If flagExitoPago Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "RealizarPago - Pago realizado correctamente"))

                oCabeceraActualizar.uuid = UUID
                oCabeceraActualizar.codEstado = KeySettings.Key_CodigoEstadoPagoExitoSICAR
                oCabeceraActualizar.codRpta = KeySettings.Key_CodigoRespuestaExito
                oCabeceraActualizar.msjRpta = KeySettings.Key_MsjRptaExitoPagoSICAR
                oCabeceraActualizar.error = responseRealizarPago.MensajeErrror
                oCabeceraActualizar.nodoSicar = strNodo
                oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagNoReintentarPago


                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - oCabeceraActualizar.uuid", oCabeceraActualizar.uuid))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - oCabeceraActualizar.codEstado", oCabeceraActualizar.codEstado))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - oCabeceraActualizar.codRpta", oCabeceraActualizar.codRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - oCabeceraActualizar.msjRpta", oCabeceraActualizar.msjRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - oCabeceraActualizar.error", oCabeceraActualizar.error))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - oCabeceraActualizar.nodoSicar", oCabeceraActualizar.nodoSicar))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - oCabeceraActualizar.flagReintentar", oCabeceraActualizar.flagReintentar))


                rptaActualizar = ActualizarEstadoPedido(oCabeceraActualizar, ipServer, usuario_id, ipcliente)

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - rptaActualizar", Funciones.CheckStr(rptaActualizar)))


            End If


            If flagErrorPago Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "RealizarPago - Ocurrio un error en el Pago de la Boleta/Factura"))
                Throw New Exception
            End If


        Catch ex As Exception

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "RealizarPago - Ocurrio un error general al realizar el pago"))

            oCabeceraActualizar.uuid = UUID
            oCabeceraActualizar.codEstado = KeySettings.Key_CodigoEstadoError
            oCabeceraActualizar.codRpta = KeySettings.Key_CodigoRespuestaError
            oCabeceraActualizar.msjRpta = KeySettings.Key_MsjErrorGeneralSICAR
            oCabeceraActualizar.nodoSicar = strNodo

            If flagErrorPago Then
                oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagReintentarPago
            Else
                oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagNoReintentarPago
            End If


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - oCabeceraActualizar.uuid", oCabeceraActualizar.uuid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - oCabeceraActualizar.codEstado", oCabeceraActualizar.codEstado))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - oCabeceraActualizar.codRpta", oCabeceraActualizar.codRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - oCabeceraActualizar.msjRpta", oCabeceraActualizar.msjRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - oCabeceraActualizar.nodoSicar", oCabeceraActualizar.nodoSicar))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - oCabeceraActualizar.flagReintentar", oCabeceraActualizar.flagReintentar))

            rptaActualizar = ActualizarEstadoPedido(oCabeceraActualizar, ipServer, usuario_id, ipcliente)

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "RealizarPago - rptaActualizar", Funciones.CheckStr(rptaActualizar)))

            responseRealizarPago.CodigoRespuesta = "1"
            responseRealizarPago.MensajeRespuesta = KeySettings.Key_MsjErrorGeneralSICAR

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - response.CodigoRespuesta => " & responseRealizarPago.CodigoRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "RealizarPago - response.MensajeRespuesta => " & responseRealizarPago.MensajeRespuesta)

        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "###################################################################################"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "##################### RealizarPago - FIN PROCESO REALIZAR PAGO ####################"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "###################################################################################"))

        Return responseRealizarPago
    End Function

    Public Function Inicio(ByVal UUID As String, ByVal idPedido As String, ByRef response As BEResponseWebMethod) As Boolean

        Dim responseObtenerDatos As BEResponseWebMethod
        Dim rpta As Boolean = False
        Dim rptaActualizar As Boolean = False
        Dim oCabeceraActualizar As BECabeceraEstadoPago = New BECabeceraEstadoPago

        Try

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Inicio Metodo Inicio()"))

            If ObtenerDatosPedido(UUID, idPedido, responseObtenerDatos) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerDatosPedido => True")
                rpta = True
                response.CodigoRespuesta = responseObtenerDatos.CodigoRespuesta
                response.MensajeRespuesta = responseObtenerDatos.MensajeRespuesta
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerDatosPedido => False")

                response.CodigoRespuesta = Funciones.CheckStr(responseObtenerDatos.CodigoRespuesta)
                response.MensajeRespuesta = Funciones.CheckStr(responseObtenerDatos.MensajeRespuesta)

                oCabeceraActualizar.uuid = UUID
                oCabeceraActualizar.codEstado = KeySettings.Key_CodigoEstadoError
                oCabeceraActualizar.codRpta = KeySettings.Key_CodigoRespuestaError
                oCabeceraActualizar.msjRpta = response.MensajeRespuesta
                oCabeceraActualizar.nodoSicar = strNodo
                oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagNoReintentarPago


                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Inicio - oCabeceraActualizar.uuid", oCabeceraActualizar.uuid))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Inicio - oCabeceraActualizar.codEstado", oCabeceraActualizar.codEstado))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Inicio - oCabeceraActualizar.codRpta", oCabeceraActualizar.codRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Inicio - oCabeceraActualizar.msjRpta", oCabeceraActualizar.msjRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Inicio - oCabeceraActualizar.nodoSicar", oCabeceraActualizar.nodoSicar))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Inicio - oCabeceraActualizar.flagReintentar", oCabeceraActualizar.flagReintentar))


                rptaActualizar = ActualizarEstadoPedido(oCabeceraActualizar, ipServer, usuario_id, ipcliente)
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Inicio - rptaActualizar => " & rptaActualizar)


                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Inicio - response.CodigoRespuesta => " & response.CodigoRespuesta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Inicio - response.MensajeRespuesta => " & response.MensajeRespuesta)

            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Codigo Respuesta", Funciones.CheckStr(response.CodigoRespuesta)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Mensaje Respuesta", Funciones.CheckStr(response.MensajeRespuesta)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Fin Metodo Inicio()"))

        Catch ex As Exception

            oCabeceraActualizar.uuid = UUID
            oCabeceraActualizar.codEstado = KeySettings.Key_CodigoEstadoError
            oCabeceraActualizar.codRpta = KeySettings.Key_CodigoRespuestaError
            oCabeceraActualizar.msjRpta = KeySettings.Key_MsjErrorValidacionDeliverySICAR
            oCabeceraActualizar.nodoSicar = strNodo
            oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagNoReintentarPago


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Inicio - oCabeceraActualizar.uuid", oCabeceraActualizar.uuid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Inicio - oCabeceraActualizar.codEstado", oCabeceraActualizar.codEstado))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Inicio - oCabeceraActualizar.codRpta", oCabeceraActualizar.codRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Inicio - oCabeceraActualizar.msjRpta", oCabeceraActualizar.msjRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Inicio - oCabeceraActualizar.nodoSicar", oCabeceraActualizar.nodoSicar))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Inicio - oCabeceraActualizar.flagReintentar", oCabeceraActualizar.flagReintentar))


            rptaActualizar = ActualizarEstadoPedido(oCabeceraActualizar, ipServer, usuario_id, ipcliente)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Inicio - rptaActualizar => " & rptaActualizar)

            response.CodigoRespuesta = "1"
            response.MensajeRespuesta = KeySettings.Key_MsjErrorValidacionDeliverySICAR

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Inicio - response.CodigoRespuesta => " & response.CodigoRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Inicio - response.MensajeRespuesta => " & response.MensajeRespuesta)

        End Try

        Return rpta

    End Function

    Public Function ObtenerDatosPedido(ByVal UUID As String, ByVal idPedido As String, ByRef response As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Inicio Metodo ObtenerDatosPedido()"))

        Dim objConsultarDatosPedido As ResponseConsultarDatosPago = New ResponseConsultarDatosPago
        Dim oCabeceraActualizar As BECabeceraEstadoPago = New BECabeceraEstadoPago
        Dim tempPagos As String = String.Empty
        Dim tempPagosRA As String = String.Empty
        Dim responseDelivery As BEResponseWebMethod = New BEResponseWebMethod
        response = New BEResponseWebMethod
        Dim objConsMsSap As New COM_SIC_Activaciones.clsConsultaMsSap ' INICIATIVA - 1006

        Dim codigo As String = String.Empty
        Dim respuesta As String = String.Empty

        Dim rpta As Boolean = False
        Dim rptaActualizar As Boolean = False

        Dim nameServer As String = System.Net.Dns.GetHostName
        ipServer = System.Net.Dns.GetHostByName(nameServer).AddressList(0).ToString
        usuario_id = "USERAPP"
        ipcliente = HttpContext.Current.Request.UserHostAddress

        Dim oCabeceraConsulta As BECabeceraDatosPago = New BECabeceraDatosPago
        Dim oDetalleConsulta As BEDetalleDatosPago = New BEDetalleDatosPago

        oCabeceraConsulta.uuid = String.Empty
        oCabeceraConsulta.nroPedido = idPedido

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- UUID : " & UUID)

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- ipServer : " & ipServer)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- usuario_id : " & strUsuario)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- ipcliente : " & ipcliente)

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- oCabeceraConsulta.uuid : " & oCabeceraConsulta.uuid)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- oCabeceraConsulta.nroPedido : " & oCabeceraConsulta.nroPedido)


        Dim objConsultar As RestServiceConsultarDatosPago = New RestServiceConsultarDatosPago

        If objConsultar.ConsultarDatosPago(UUID, idPedido, ipServer, usuario_id, ipcliente, oCabeceraConsulta, oDetalleConsulta) Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "objConsultar.ConsultarDatosPago => True")
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerDatosPedido - oDetalleConsulta.uuid : " & oDetalleConsulta.uuid)

            If UUID = oDetalleConsulta.uuid Then

                strTipoDocVendedor = oDetalleConsulta.tipoDocMot
                strNumDocVendedor = oDetalleConsulta.nroDocMot
                strTipoOperacionServ = oDetalleConsulta.tipoOperacion
                strFlagTiendaVirtual = oDetalleConsulta.flagTiendaVirtual

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerDatosPedido - strTipoDocVendedor", strTipoDocVendedor))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerDatosPedido - strNumDocVendedor", strNumDocVendedor))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerDatosPedido - strTipoOperacionServ", strTipoOperacionServ))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerDatosPedido - strFlagTiendaVirtual", strFlagTiendaVirtual))

                oCabeceraActualizar.uuid = oDetalleConsulta.uuid
                oCabeceraActualizar.codEstado = KeySettings.Key_CodigoEstadoRecibidoSICAR
                oCabeceraActualizar.codRpta = KeySettings.Key_CodigoRespuestaExito
                oCabeceraActualizar.msjRpta = KeySettings.Key_MsjRptaExitoRecibidoSICAR
                oCabeceraActualizar.nodoSicar = strNodo
                oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagNoReintentarPago


                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerDatosPedido - oCabeceraActualizar.uuid", oCabeceraActualizar.uuid))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerDatosPedido - oCabeceraActualizar.codEstado", oCabeceraActualizar.codEstado))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerDatosPedido - oCabeceraActualizar.codRpta", oCabeceraActualizar.codRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerDatosPedido - oCabeceraActualizar.msjRpta", oCabeceraActualizar.msjRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerDatosPedido - oCabeceraActualizar.nodoSicar", oCabeceraActualizar.nodoSicar))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerDatosPedido - oCabeceraActualizar.flagReintentar", oCabeceraActualizar.flagReintentar))

                'JRM
                If Not oDetalleConsulta.codMedioPago Is Nothing Then
                    tempPagos = oDetalleConsulta.codMedioPago
                    strCodigoPago = tempPagos.Split(";")(0)
                    strNumReferencia = tempPagos.Split(";")(2)
                    arrayBoleta = strNumReferencia.Split("-")

                    'PROY-140590 IDEA142068 - INICIO
                    Dim tarjeta As String = String.Empty
                    tarjeta = Funciones.CheckStr(tempPagos.Split(";")(3))

                    If Not tarjeta = String.Empty Then
                        strTarjeta = tarjeta.Replace("-", "")
                    End If
                    'PROY-140590 IDEA142068 - FIN

                End If

                If Not oDetalleConsulta.codMedioPagora Is Nothing Then
                    tempPagosRA = oDetalleConsulta.codMedioPagora
                    strCodigoPagoRA = tempPagosRA.Split(";")(0)
                    strNumReferenciaRA = tempPagosRA.Split(";")(2)
                    arrayRA = strNumReferenciaRA.Split("-")
                End If
                'JRM

'JRM - PROY 140589 INI
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "PROY 140589 -- INICIO CONSULTA COBRO DLV")
                Try
                    Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
                    Dim Array As ArrayList
                    Dim arrLst As ArrayList ' INICIATIVA-1006
                    Array = objConsultaMsSap.validaPagoDLV(idPedido, FlagCostoDlv, FlagIsPedidoCostoDlv, NroPedidoCostoDlv, strIsPortabilidad, strFlagPortabilidad, CodRptaCostoDLV, MsjRptaCostoDlv)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "====IMPUT INI====")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "NroPedido => " & CheckStr(idPedido))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "====IMPUT FIN====")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "====OUTPUT INI====")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "FlagCostoDlv => " & CheckStr(FlagCostoDlv)) '0 No tiene Boleta 1 si tiene boleta
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "FlagIsPedidoCostoDlv => " & CheckStr(FlagIsPedidoCostoDlv)) '0 No es pedido delivery 1 Si es pedido delivery
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "NroPedidoCostoDlv => " & CheckStr(NroPedidoCostoDlv)) ' Nro del pedido Costo Delivery
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "CodRptaCostoDLV => " & CheckStr(CodRptaCostoDLV)) ' 0 Boleta Pagada 1 Boleta sin Pagar
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "MsjRptaCostoDlv => " & CheckStr(MsjRptaCostoDlv)) ' Mensaje de respuesta del SP
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "====OUTPUT FIN====")

                    If FlagIsPedidoCostoDlv.Equals("1") Or FlagCostoDlv.Equals("1") Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "====INICIO OBTENER DATOS COBRO DLV====")
                        Dim strCodMsjDlv As String
                        Dim strRptaMsjDlv As String
                        Dim strMontoPagarPMDLV As String
                        Dim strMontoPagarACCDLV As String ' INICIATIVA-1006
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "====IMPUT INI====")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "NroPedido => " & CheckStr(NroPedidoCostoDlv))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "====IMPUT FIN====")


                        'INICIATIVA-1006 |TIENDA VIARTUAL - ACC CON COSTO | INICIO
                        arrLst = objConsultaMsSap.ConsultarDatosDeliveryPedido2(NroPedidoCostoDlv, "Movil", strCodMsjDlv, strRptaMsjDlv)
                        'strFlagBiometriaDlv = objConsultaMsSap.ConsultarDatosDeliveryPedido(NroPedidoCostoDlv, "Movil", strMontoPagarDlv, strMontoPagarRADlv, strEstadoPedidoDlv, strMontoPagarPMDLV, strCodMsjDlv, strRptaMsjDlv)


                        For Each item As ItemGenerico In arrLst
                            strMontoPagarDlv = item.MONTO_PAGAR
                            strMontoPagarRADlv = item.MONTO_PAGAR_RA
                            strEstadoPedidoDlv = item.ESTADO_PEDIDO
                            strFlagBiometriaDlv = item.FLAG_BIOMETRIA
                            strMontoPagarPMDLV = item.MONTO_PM
                            strMontoPagarACCDLV = item.MONTO_ACC_COSTO
                        Next

                        'INICIATIVA-1006 |TIENDA VIARTUAL - ACC CON COSTO | | FIN

                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "====OUTPUT INI====")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "strMontoPagarDlv => " & CheckStr(strMontoPagarDlv)) '0 No tiene Boleta 1 si tiene boleta
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "strMontoPagarRADlv => " & CheckStr(strMontoPagarRADlv)) '0 No es pedido delivery 1 Si es pedido delivery
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "strEstadoPedidoDlv => " & CheckStr(strEstadoPedidoDlv)) ' Nro del pedido Costo Delivery
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "strCodMsjDlv => " & CheckStr(strCodMsjDlv)) ' 0 Boleta Pagada 1 Boleta sin Pagar
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "strRptaMsjDlv => " & CheckStr(strRptaMsjDlv)) ' Mensaje de respuesta del SP
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "====OUTPUT FIN====")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "====FIN OBTENER DATOS COBRO DLV====")
                        If strCodMsjDlv <> "0" Then
                            response.CodigoRespuesta = "1"
                            response.MensajeRespuesta = ""
                            Exit Function
                        End If
                    Else
                        If FlagIsPedidoCostoDlv.Equals("-1") Then
                            response.CodigoRespuesta = "1"
                            response.MensajeRespuesta = KeySettings.Key_MsjRptaErrorRecibidoSICAR
                            Exit Function
                        End If
                    End If

                Catch ex As Exception
                    response.CodigoRespuesta = "1"
                    response.MensajeRespuesta = KeySettings.Key_MsjRptaErrorRecibidoSICAR
                    Exit Function
                End Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "PROY 140589 -- FIN CONSULTA COBRO DLV")
                'JRM - PROY 140589 FIN


                'INICIATIVA - 1006 | Tienda Virtual - ACCESORIO CON COSTO | Metodo para obtener los datos del SP de consulta de accesorio con costo
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[INICIO] ")

                Try
                    Dim strCodRespuesta As String
                    Dim strMsjRespuesta As String

                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "====IMPUT INI====")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[INPUT]" & Funciones.CheckStr(idPedido))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "====IMPUT FIN====")

                    objConsMsSap.validaPedidoAccCosto(Funciones.CheckInt64(idPedido), intNroPedACC_cos, strFlagACC_cos, strEstadoPed_ACC_Cos, strCodRespuesta, strMsjRespuesta)

                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "====OUTPUT INI====")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[OUTPUT]" & Funciones.CheckStr(intNroPedACC_cos))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[OUTPUT]" & Funciones.CheckStr(strFlagACC_cos))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[OUTPUT]" & Funciones.CheckStr(strCodRespuesta))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[OUTPUT]" & Funciones.CheckStr(strMsjRespuesta))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "====OUTPUT FIN====")

                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[CATCH]-[Message]" & Funciones.CheckStr(ex.Message))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[CATCH]-[StackTrace]" & Funciones.CheckStr(ex.StackTrace))
                End Try

                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "[ INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo ] - [ FIN ] ")
                'INICIATIVA - 1006 | FIN



                'PROY-140582 - INI
                If Not Funciones.CheckStr(oDetalleConsulta.flagPm) Is String.Empty Then
                    strFlagPM = Funciones.CheckStr(oDetalleConsulta.flagPm)
                End If
                'PROY-140582 - FIN

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerDatosPedido - strCodigoPago", strCodigoPago))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerDatosPedido - strNumReferencia", strNumReferencia))

                rptaActualizar = ActualizarEstadoPedido(oCabeceraActualizar, ipServer, usuario_id, ipcliente)
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "rptaActualizar => " & rptaActualizar)

                If rptaActualizar Then

                    If ValidacionesDelivery(oDetalleConsulta, responseDelivery) Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidacionesDelivery => True")
                        rpta = True

                        response.CodigoRespuesta = responseDelivery.CodigoRespuesta
                        response.MensajeRespuesta = responseDelivery.MensajeRespuesta

                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "ValidacionesDelivery => false"))

                        response.CodigoRespuesta = responseDelivery.CodigoRespuesta
                        response.MensajeRespuesta = responseDelivery.MensajeRespuesta

                    End If

                Else
                    response.CodigoRespuesta = "1"
                    response.MensajeRespuesta = KeySettings.Key_MsjRptaErrorRecibidoSICAR
                End If

            Else
                response.CodigoRespuesta = "1"
                response.MensajeRespuesta = KeySettings.Key_IdentificarNoCoincide
            End If
        Else
            response.CodigoRespuesta = "1"
            response.MensajeRespuesta = KeySettings.Key_PedidoNoRetornaDatos
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "rpta => " & rpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "FIN ObtenerDatosPedido")

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Codigo Respuesta", Funciones.CheckStr(response.CodigoRespuesta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Mensaje Respuesta", Funciones.CheckStr(response.MensajeRespuesta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Fin Metodo ObtenerDatosPedido()"))

        Return rpta

    End Function

    Public Function ActualizarEstadoPedido(ByVal oCabecera As BECabeceraEstadoPago, ByVal ipServer As String, ByVal usuario_id As String, ByVal ipcliente As String) As Boolean

        Dim rpta As Boolean = False
        Try

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Inicio Metodo ActualizarEstadoPedido()"))

            Dim objActualizar As RestServiceActualizarEstadoPago = New RestServiceActualizarEstadoPago
            rpta = objActualizar.ActualizarEstadoPago(ipServer, usuario_id, ipcliente, oCabecera)

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Codigo Respuesta", Funciones.CheckStr(rpta)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Fin Metodo ActualizarEstadoPedido()"))

        Catch ex As Exception

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "ERROR ActualizarEstadoPedido()"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ERROR ActualizarEstadoPedido()", Funciones.CheckStr(ex.Message)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ERROR ActualizarEstadoPedido()", Funciones.CheckStr(ex.StackTrace)))
        End Try


        Return rpta
    End Function

    Public Function ValidacionesDelivery(ByVal oDetalle As BEDetalleDatosPago, ByRef response As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Inicio Metodo ValidacionesDelivery()"))

        Dim rpta As Boolean = False
        Dim rptaAuditoria As Boolean = False
        Dim rptaEmpleado As Boolean = False
        Dim rptaAsignacionCajero As Boolean = False
        Dim strOficinaPago As String = String.Empty

        response = New BEResponseWebMethod
        Dim oDatosAuditoria As BEDatosAuditoria = New BEDatosAuditoria
        Dim oDatosEmpleados As BEDatosEmpleado = New BEDatosEmpleado
        Dim oDatosCajeroVirtual As BEDatosEmpleado = New BEDatosEmpleado
        Dim responseCajeroVirtual As BEResponseWebMethod = New BEResponseWebMethod
        Dim responseValidarCajero As BEResponseWebMethod = New BEResponseWebMethod
        Dim responseAuditoria As BEResponseWebMethod = New BEResponseWebMethod
        Dim responseEmpleado As BEResponseWebMethod = New BEResponseWebMethod
        Dim responseAsignacionCajero As BEResponseWebMethod = New BEResponseWebMethod
        Dim responseConsultarDatosPedido As BEResponseWebMethod = New BEResponseWebMethod
        Dim responseBiometria As BEResponseWebMethod = New BEResponseWebMethod

        Dim flagCajeroVirtual As Boolean = False

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Key_FlagCajeroVirtualAutomatico : " & KeySettings.Key_FlagCajeroVirtualAutomatico)


        If ValidarBiometria(oDetalle.pedinNroPedido, oDetalle.tipoDocMot, oDetalle.nroDocMot, strOficinaPago, responseBiometria) Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Pedido NO requiere pasar por Validacion biometrica.")

            'rpta = True

            If KeySettings.Key_FlagCajeroVirtualAutomatico = "1" Then

                flagCajeroVirtual = True

                If ObtenerCajeroVirtual(oDetalle, strOficinaPago, oDatosCajeroVirtual, responseCajeroVirtual) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual => True")
                    If ValidarCajero(oDatosCajeroVirtual.ALMACEN, oDatosCajeroVirtual.CODUSUARIO, oDatosCajeroVirtual.NRO_CAJA, oDatosCajeroVirtual.NOMBRE_CAJA, flagCajeroVirtual, oDetalle, responseValidarCajero) Then

                        rpta = True

                        response.CodigoRespuesta = responseValidarCajero.CodigoRespuesta
                        response.MensajeRespuesta = responseValidarCajero.MensajeRespuesta
                    Else
                        response.CodigoRespuesta = responseValidarCajero.CodigoRespuesta
                        response.MensajeRespuesta = responseValidarCajero.MensajeRespuesta
                    End If

                Else
                    response.CodigoRespuesta = responseCajeroVirtual.CodigoRespuesta
                    response.MensajeRespuesta = responseCajeroVirtual.MensajeRespuesta
                End If

            Else

                If Not oDetalle.usuarioPago Is Nothing Then

                    rptaAuditoria = ObtenerDatosAuditoria(oDetalle.usuarioPago, oDatosAuditoria, responseAuditoria)

                    If rptaAuditoria Then

                        rptaEmpleado = ObtenerDatosEmpleado(oDetalle.usuarioPago, oDatosEmpleados, responseEmpleado)

                        If rptaEmpleado Then

                            oDatosEmpleados.PERFIL = oDatosAuditoria.codPerfil
                            oDatosEmpleados.COD_VENDEDOR = oDatosAuditoria.codUsuario
                            oEmpleados = oDatosEmpleados

                            If ValidarCajero(oDatosEmpleados.ALMACEN, oDatosEmpleados.CODUSUARIO, String.Empty, String.Empty, flagCajeroVirtual, oDetalle, responseAsignacionCajero) Then

                                rpta = True

                                response.CodigoRespuesta = responseAsignacionCajero.CodigoRespuesta
                                response.MensajeRespuesta = responseAsignacionCajero.MensajeRespuesta

                            Else
                                response.CodigoRespuesta = responseAsignacionCajero.CodigoRespuesta
                                response.MensajeRespuesta = responseAsignacionCajero.MensajeRespuesta
                            End If
                        Else
                            response.CodigoRespuesta = responseEmpleado.CodigoRespuesta
                            response.MensajeRespuesta = responseEmpleado.MensajeRespuesta
                        End If

                    Else
                        response.CodigoRespuesta = responseAuditoria.CodigoRespuesta
                        response.MensajeRespuesta = responseAuditoria.MensajeRespuesta
                    End If

                Else
                    response.CodigoRespuesta = "1"
                    response.MensajeRespuesta = KeySettings.Key_CuentaENoEnviada
                End If
            End If
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Pedido debe pasar por validacion biometrica.")
            response.CodigoRespuesta = Funciones.CheckStr(responseBiometria.CodigoRespuesta)
            response.MensajeRespuesta = Funciones.CheckStr(responseBiometria.MensajeRespuesta)
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "rpta", Funciones.CheckStr(rpta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Codigo Respuesta", Funciones.CheckStr(response.CodigoRespuesta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Mensaje Respuesta", Funciones.CheckStr(response.MensajeRespuesta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Fin Metodo ValidacionesDelivery()"))

        Return rpta
    End Function

    Private Function ObtenerCajeroVirtual(ByVal oDetalle As BEDetalleDatosPago, ByVal strOficinaPago As String, ByRef datosEmpleado As BEDatosEmpleado, ByRef response As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Inicio Metodo ObtenerCajeroVirtual()"))

        Dim rpta As Boolean = False
        Dim rptaActualizar As Boolean = False
        Dim strCodRpta As String = String.Empty
        Dim strMsgRpta As String = String.Empty
        Dim dsCajeroVirtual As DataSet
        Dim oCabeceraActualizar As BECabeceraEstadoPago = New BECabeceraEstadoPago
        response = New BEResponseWebMethod

        'CONSULTAR CAJERO VIRTUAL
        Dim objPagos As New COM_SIC_OffLine.clsOffline
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - strOficinaPago : " & strOficinaPago)
        dsCajeroVirtual = objPagos.Get_CajeroVirtual(strOficinaPago, strCodRpta, strMsgRpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - strCodRpta : " & strCodRpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - strMsgRpta : " & strMsgRpta)

        If dsCajeroVirtual Is Nothing OrElse dsCajeroVirtual.Tables(0).Rows.Count > 0 Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - dsCajeroVirtual.Tables(0).Rows.Count > 0")
            datosEmpleado.CODUSUARIO = dsCajeroVirtual.Tables(0).Rows(0).Item(0)
            datosEmpleado.NOMBRE_COMPLETO = dsCajeroVirtual.Tables(0).Rows(0).Item(1)
            datosEmpleado.ALMACEN = dsCajeroVirtual.Tables(0).Rows(0).Item(2)
            datosEmpleado.CURRENT_USER = dsCajeroVirtual.Tables(0).Rows(0).Item(3)
            datosEmpleado.COD_VENDEDOR = dsCajeroVirtual.Tables(0).Rows(0).Item(4)
            datosEmpleado.NRO_CAJA = dsCajeroVirtual.Tables(0).Rows(0).Item(5)
            datosEmpleado.NOMBRE_CAJA = dsCajeroVirtual.Tables(0).Rows(0).Item(6)
            datosEmpleado.PERFIL = "1"

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.USUARIO => " & datosEmpleado.CODUSUARIO)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.NOMBRE_COMPLETO => " & datosEmpleado.NOMBRE_COMPLETO)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.ALMACEN => " & datosEmpleado.ALMACEN)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.CURRENT_USER => " & datosEmpleado.CURRENT_USER)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.COD_VENDEDOR => " & datosEmpleado.COD_VENDEDOR)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.NRO_CAJA => " & datosEmpleado.NRO_CAJA)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.NOMBRE_CAJA => " & datosEmpleado.NOMBRE_CAJA)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.PERFIL => " & datosEmpleado.PERFIL)

            Dim dsCanal As DataSet
            dsCanal = objPagos.Get_ConsultaOficinaVenta(datosEmpleado.ALMACEN, String.Empty)
            objPagos = Nothing

            If dsCanal.Tables(0).Rows.Count > 0 Then
                datosEmpleado.CANAL = dsCanal.Tables(0).Rows(0).Item("VTWEG")
                datosEmpleado.OFICINA = dsCanal.Tables(0).Rows(0).Item("BEZEI")
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.CANAL => " & datosEmpleado.CANAL)
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.OFICINA => " & datosEmpleado.OFICINA)
            End If

            oEmpleados = datosEmpleado
            rpta = True

        Else

            oCabeceraActualizar.uuid = oDetalle.uuid
            oCabeceraActualizar.codEstado = KeySettings.Key_CodigoEstadoError
            oCabeceraActualizar.codRpta = KeySettings.Key_CodigoRespuestaError
            oCabeceraActualizar.msjRpta = String.Format(KeySettings.Key_ErrorCajeroVirtual, strOficinaPago)
            oCabeceraActualizar.nodoSicar = strNodo
            oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagNoReintentarPago

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerCajeroVirtual - oCabeceraActualizar.uuid", oCabeceraActualizar.uuid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerCajeroVirtual - oCabeceraActualizar.codEstado", oCabeceraActualizar.codEstado))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerCajeroVirtual - oCabeceraActualizar.codRpta", oCabeceraActualizar.codRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerCajeroVirtual - oCabeceraActualizar.msjRpta", oCabeceraActualizar.msjRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerCajeroVirtual - oCabeceraActualizar.nodoSicar", oCabeceraActualizar.nodoSicar))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ObtenerCajeroVirtual - oCabeceraActualizar.flagReintentar", oCabeceraActualizar.flagReintentar))

            'rptaActualizar = ActualizarEstadoPedido(oCabeceraActualizar, ipServer, usuario_id, ipcliente)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - rptaActualizar => " & rptaActualizar)

            response.CodigoRespuesta = "1"
            response.MensajeRespuesta = String.Format(KeySettings.Key_ErrorCajeroVirtual, strOficinaPago)

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - response.CodigoRespuesta => " & response.CodigoRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - response.MensajeRespuesta => " & response.MensajeRespuesta)

        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "rpta", Funciones.CheckStr(rpta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Codigo Respuesta", Funciones.CheckStr(response.CodigoRespuesta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Mensaje Respuesta", Funciones.CheckStr(response.MensajeRespuesta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Fin Metodo ObtenerCajeroVirtual()"))

        Return rpta
    End Function

    Private Function ValidarCajero(ByVal strAlmacen As String, ByVal strCodUsuario As String, ByVal strCodCaja As String, ByVal strNombreCaja As String, ByVal flagCajeroVirtual As Boolean, ByVal oDetalle As BEDetalleDatosPago, ByRef response As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Fin Metodo ValidarCajero()"))

        Dim rpta As Boolean = False
        response = New BEResponseWebMethod
        Dim responseAsignacionCajero As BEResponseWebMethod = New BEResponseWebMethod
        Dim responseConsultarDatosPedido As BEResponseWebMethod = New BEResponseWebMethod

        If ValidarAsignacionCajero(strAlmacen, strCodUsuario, strCodCaja, strNombreCaja, flagCajeroVirtual, responseAsignacionCajero) Then

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarAsignacionCajero => True")
            If ConsultarDatos(oDetalle, responseConsultarDatosPedido) Then
                rpta = True
                response.CodigoRespuesta = responseConsultarDatosPedido.CodigoRespuesta
                response.MensajeRespuesta = responseConsultarDatosPedido.MensajeRespuesta
            Else
                response.CodigoRespuesta = responseConsultarDatosPedido.CodigoRespuesta
                response.MensajeRespuesta = responseConsultarDatosPedido.MensajeRespuesta
            End If

        Else
            response.CodigoRespuesta = responseAsignacionCajero.CodigoRespuesta
            response.MensajeRespuesta = responseAsignacionCajero.MensajeRespuesta
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "rpta", Funciones.CheckStr(rpta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Codigo Respuesta", Funciones.CheckStr(response.CodigoRespuesta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Mensaje Respuesta", Funciones.CheckStr(response.MensajeRespuesta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Fin Metodo ValidarCajero()"))

        Return rpta
    End Function

    Public Function ObtenerDatosAuditoria(ByVal strUsuario As String, ByRef datosAuditoria As BEDatosAuditoria, ByRef response As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Fin Metodo ObtenerDatosAuditoria()"))

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- strUsuario : " & strUsuario)
        Dim rpta As Boolean = False
        Dim objAccesoAplicativo As Object
        Dim codAplicacion As String = ConfigurationSettings.AppSettings("codAplicacion")
        response = New BEResponseWebMethod

        Dim objAuditoriaWS As New AuditoriaWS.EbsAuditoriaService
        Dim oAccesoRequest As New AuditoriaWS.AccesoRequest
        Dim oAccesoResponse As New AuditoriaWS.AccesoResponse

        objAuditoriaWS.Url = ConfigurationSettings.AppSettings("consRutaWSSeguridad").ToString()
        objAuditoriaWS.Credentials = System.Net.CredentialCache.DefaultCredentials
        objAuditoriaWS.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("ConstTimeOutEmpleado").ToString())

        oAccesoRequest.usuario = strUsuario
        oAccesoRequest.aplicacion = codAplicacion

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "oAccesoRequest.usuario", Funciones.CheckStr(oAccesoRequest.usuario)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "oAccesoRequest.aplicacion", Funciones.CheckStr(oAccesoRequest.aplicacion)))

        oAccesoResponse = objAuditoriaWS.leerDatosUsuario(oAccesoRequest)

        If oAccesoResponse.resultado.estado = "1" Then

            datosAuditoria.codUsuario = oAccesoResponse.auditoria.AuditoriaItem.item(0).codigo
            datosAuditoria.codPerfil = oAccesoResponse.auditoria.AuditoriaItem.item(0).perfil

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "datosAuditoria.codUsuario", Funciones.CheckStr(datosAuditoria.codUsuario)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "datosAuditoria.codPerfil", Funciones.CheckStr(datosAuditoria.codPerfil)))

            rpta = True

        Else
            response.CodigoRespuesta = "1"
            response.MensajeRespuesta = KeySettings.Key_UsuarioNoCuentaConAcceso
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "rpta", Funciones.CheckStr(rpta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Codigo Respuesta", Funciones.CheckStr(response.CodigoRespuesta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Mensaje Respuesta", Funciones.CheckStr(response.MensajeRespuesta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Fin Metodo ObtenerDatosAuditoria()"))

        Return rpta
    End Function

    Public Function ObtenerDatosEmpleado(ByVal strUsuario As String, ByRef datosEmpleado As BEDatosEmpleado, ByRef response As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Inicio Metodo ObtenerDatosEmpleado()"))

        Dim rpta As Boolean = False
        response = New BEResponseWebMethod
        Dim objEmpleado As New clsUsuario
        Dim objEmpleadoWS As New clsEmpleadoWS
        Dim strCodigoVendedor As String = String.Empty

        objEmpleado = GetDatosEmpleado(strUsuario)

        If objEmpleado Is Nothing OrElse objEmpleado.CodigoVendedor = "" Then

            response.CodigoRespuesta = "1"
            response.MensajeRespuesta = "No se obtuvo datos de empleado" 'Key_MsjRptaErrorWSEmpleado

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "rpta", Funciones.CheckStr(rpta)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Codigo Respuesta", Funciones.CheckStr(response.CodigoRespuesta)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Mensaje Respuesta", Funciones.CheckStr(response.MensajeRespuesta)))

            Return rpta

        End If
        '*************************************************************************'

        strCodigoVendedor = objEmpleado.CodigoVendedor

        Dim dsVendedor As DataSet

        Dim objPagos As New COM_SIC_OffLine.clsOffline
        dsVendedor = objPagos.Get_ConsultaVend(strCodigoVendedor)


        If Not dsVendedor Is Nothing Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- Nro Registro Vendedor : " & dsVendedor.Tables(0).Rows.Count)
            If dsVendedor.Tables(0).Rows.Count > 0 Then
                datosEmpleado.NOMBRE_COMPLETO = dsVendedor.Tables(0).Rows(0).Item(1)
                datosEmpleado.ALMACEN = dsVendedor.Tables(0).Rows(0).Item(2)
                datosEmpleado.OFICINA = dsVendedor.Tables(0).Rows(0).Item(3)
                datosEmpleado.CODUSUARIO = strCodigoVendedor
                datosEmpleado.CURRENT_USER = strUsuario

                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.NOMBRE_COMPLETO => " & datosEmpleado.NOMBRE_COMPLETO)
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.ALMACEN => " & datosEmpleado.ALMACEN)
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.CURRENT_USER => " & datosEmpleado.CURRENT_USER)
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.OFICINA => " & datosEmpleado.OFICINA)
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.CODUSUARIO => " & datosEmpleado.CODUSUARIO)

                Dim dsCanal As DataSet
                dsCanal = objPagos.Get_ConsultaOficinaVenta(datosEmpleado.ALMACEN, String.Empty)
                objPagos = Nothing

                If dsCanal.Tables(0).Rows.Count > 0 Then
                    datosEmpleado.CANAL = dsCanal.Tables(0).Rows(0).Item("VTWEG")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerCajeroVirtual - datosEmpleado.CANAL => " & datosEmpleado.CANAL)
                End If

                rpta = True

            Else
                response.CodigoRespuesta = "1"
                response.MensajeRespuesta = "Consulta al vendedor no retorna datos" 'Key_MsjRptaErrorUSERNoRegistrado
            End If
        Else
            response.CodigoRespuesta = "1"
            response.MensajeRespuesta = "Empleado no se encuentra registrado en BD SICAR" 'Key_MsjRptaErrorUSERNoRegistrado
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "rpta", Funciones.CheckStr(rpta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Codigo Respuesta", Funciones.CheckStr(response.CodigoRespuesta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Mensaje Respuesta", Funciones.CheckStr(response.MensajeRespuesta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Fin Metodo ObtenerDatosEmpleado()"))

        Return rpta

    End Function

    Private Function GetDatosEmpleado(ByVal strUsuario As String)
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "INICIO WS EMPLEADO")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")

            Dim objEmpleado As New clsUsuario
            Dim oTransaccion As New WSEmpleado.ConsultaOpcionesAuditoriaService

            oTransaccion.Url = ConfigurationSettings.AppSettings("ConstUrlEmpleado").ToString()
            oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
            oTransaccion.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("ConstTimeOutEmpleado").ToString())

            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio WS EMPLEADO --> URL: " & oTransaccion.Url & ", TimeOut: " & oTransaccion.Timeout.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")

            Dim objReqPadre As New WSEmpleado.leerDatosEmpleado
            Dim objEmpleadoRequest As New WSEmpleado.DatosEmpleadoRequest
            Dim objAuditRequest As New WSEmpleado.AuditRequest
            'Response
            Dim objRespPadre As New WSEmpleado.leerDatosEmpleadoResponse
            'Set oRequest
            objAuditRequest.aplicacion = ConfigurationSettings.AppSettings("ConsNombreAplicacion")
            objAuditRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHss")
            objAuditRequest.ipAplicacion = ConfigurationSettings.AppSettings("CodAplicacion")
            objAuditRequest.usrAplicacion = strUsuario
            'Invocar Método
            objEmpleadoRequest.login = strUsuario
            objReqPadre.audit = objAuditRequest
            objReqPadre.DatosEmpleadoRequest = objEmpleadoRequest

            objRespPadre = oTransaccion.leerDatosEmpleado(objReqPadre)

            'Auditoria
            Dim vResultado As String = objRespPadre.audit.codigoRespuesta
            Dim msgSalida As String = objRespPadre.audit.mensajeRespuesta

            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "WS EMPLEADO Input: --> strLogin :" + strUsuario + " aplicacion: " + objAuditRequest.aplicacion + ", idTransaccion: " + objAuditRequest.idTransaccion + ", ipAplicacion: " + objAuditRequest.ipAplicacion + " OUTPUT: vResultado: " + vResultado + ", msgSalida : " + msgSalida)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")

            'Exito de la consulta
            If vResultado = "1" Then
                objEmpleado.UsuarioId = objRespPadre.EmpleadoResponse.empleados(0).codigo
                objEmpleado.Login = objRespPadre.EmpleadoResponse.empleados(0).login
                objEmpleado.Nombre = objRespPadre.EmpleadoResponse.empleados(0).nombres
                objEmpleado.Apellido = objRespPadre.EmpleadoResponse.empleados(0).paterno
                objEmpleado.ApellidoMaterno = objRespPadre.EmpleadoResponse.empleados(0).materno
                objEmpleado.NombreCompleto = objRespPadre.EmpleadoResponse.empleados(0).nombres & " " & objRespPadre.EmpleadoResponse.empleados(0).paterno & " " & objRespPadre.EmpleadoResponse.empleados(0).materno
                objEmpleado.CodigoVendedor = objRespPadre.EmpleadoResponse.codigoVendedor
                objEmpleado.AreaId = objRespPadre.EmpleadoResponse.empleados(0).codigoArea
                objEmpleado.AreaDescripcion = objRespPadre.EmpleadoResponse.empleados(0).descripcionArea

                objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
                objFileLog.Log_WriteLog(pathFile, strArchivo, " WS EMPLEADO CorrectoWS OutPut: --> vResultado : " + vResultado + ", msgSalida : " + msgSalida)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "WS EMPLEADO SIN DATOS Output: --> vResultado : " + vResultado + ", msgSalida : " + msgSalida)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
                objEmpleado = Nothing
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "FIN WS EMPLEADO")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")

            Return objEmpleado

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "WS EMPLEADO ERROR : --> Mensaje WS : " + ex.Message)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
        End Try
    End Function

    Private Function ValidarAsignacionCajero(ByVal strAlmacen As String, ByVal strCodUsuario As String, ByVal strCodCaja As String, ByVal strNombreCaja As String, ByVal flagCajeroVirtual As Boolean, ByRef response As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "INICIO ValidarAsignacionCajero")
        objFileLog.Log_WriteLog(pathFile, strArchivo, " ValidarAsignacionCajero - flagCajeroVirtual : " & flagCajeroVirtual)

        Dim rpta As Boolean = False
        response = New BEResponseWebMethod
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim dsCajeroA As DataSet
        Dim dsCajas As DataSet
        Dim codUsuario = Funciones.CheckStr(strCodUsuario).PadLeft(10, "0")
        Dim dateTimeValueCaja As DateTime
        Dim cultureNameX As String = "es-PE"
        Dim cultureX As CultureInfo = New CultureInfo(cultureNameX)
        dateTimeValueCaja = Convert.ToDateTime(DateTime.Now, cultureX)
        Dim sFechaCaj As String = dateTimeValueCaja.ToLocalTime.ToShortDateString

        Dim strCodRpt As String = String.Empty
        Dim strMsgRpt As String = String.Empty

        objFileLog.Log_WriteLog(pathFile, strArchivo, " ValidarAsignacionCajero - Inicio GetDatosAsignacionCajero()")
        objFileLog.Log_WriteLog(pathFile, strArchivo, " GetDatosAsignacionCajero - strAlmacen : " & Funciones.CheckStr(strAlmacen))
        objFileLog.Log_WriteLog(pathFile, strArchivo, " GetDatosAsignacionCajero - sFechaCaj : " & Funciones.CheckStr(sFechaCaj))
        objFileLog.Log_WriteLog(pathFile, strArchivo, " GetDatosAsignacionCajero - codUsuario : " & Funciones.CheckStr(codUsuario))
        dsCajeroA = objOffline.GetDatosAsignacionCajero(Funciones.CheckStr(strAlmacen), sFechaCaj, codUsuario)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " ValidarAsignacionCajero - Fin GetDatosAsignacionCajero()")
        If (dsCajeroA Is Nothing OrElse dsCajeroA.Tables(0).Rows.Count <= 0) Then

            If (flagCajeroVirtual) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarAsignacionCajero - Inicio Set_CajeroDiario()")
                objOffline.Set_CajeroDiario(strAlmacen, codUsuario, Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"), strCodCaja)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarAsignacionCajero - Fin Set_CajeroDiario()")
                strCodRpt = "0"
                strMsgRpt = String.Format("Se detectó que no tiene caja asignada. Se le asignó a la caja: {0}.", strNombreCaja)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "strMsgRpt : " & strMsgRpt)
                rpta = True
            Else
                response.CodigoRespuesta = "1"
                response.MensajeRespuesta = "Cuenta de RED no tiene caja asignada" 'Key_MsjRptaErrorUSERSinCaja
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "response.MensajeRespuesta = " & response.MensajeRespuesta)
                Return rpta
            End If

        Else
            rpta = True
            objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarAsignacionCajero - Tiene Caja asignada")

        End If

        If Not dsCajeroA Is Nothing Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "dsCajeroA no es Nulo")
            For cont As Int32 = 0 To dsCajeroA.Tables(0).Rows.Count - 1
                If dsCajeroA.Tables(0).Rows(cont).Item("CAJA_CERRADA") = "S" And flagCajeroVirtual = False Then
                    response.CodigoRespuesta = "1"
                    response.MensajeRespuesta = "Cuenta de RED tiene caja cerrada" 'Key_MsjRptaErrorCajaCerrada
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "response.MensajeRespuesta = " & response.MensajeRespuesta)
                    Return rpta
                End If
            Next
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "FIN ValidarAsignacionCajero")
        Return rpta

    End Function


    Private Function ConsultarDatos(ByVal oDetalle As BEDetalleDatosPago, ByRef response As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "INICIO ConsultarDatos")
        Dim rpta As Boolean = False

        Dim strTipoVenta As String = String.Empty
        Dim strTipoOperacion As String = String.Empty
        Dim strDescTipoOperacion As String = String.Empty

        Dim rptaNoEsProteccionMovil As Boolean = False
        Dim rptaNoEsOperacionBuyBack As Boolean = False
        Dim rptaVentaPermitida As Boolean = False
        Dim rptaOperacionPermitida As Boolean = False
        Dim rptaProductoPermitido As Boolean = False

        response = New BEResponseWebMethod
        Dim responseConsultarPedido As BEResponseWebMethod = New BEResponseWebMethod
        Dim responseVentaPermitida As BEResponseWebMethod = New BEResponseWebMethod
        Dim responseOperacionPermitida As BEResponseWebMethod = New BEResponseWebMethod
        Dim responseProductoPermitida As BEResponseWebMethod = New BEResponseWebMethod
        Dim responseValidarPM As BEResponseWebMethod = New BEResponseWebMethod
        Dim responseValidarBuyBack As BEResponseWebMethod = New BEResponseWebMethod

        'PROY-140590 IDEA142068 - INICIO
        Dim responseValidarBinTarjeta As BEResponseWebMethod = New BEResponseWebMethod
        'PROY-140590 IDEA142068 - FIN

        If ConsultarDatosPedido(oDetalle, strTipoVenta, strTipoOperacion, strDescTipoOperacion, responseConsultarPedido) Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - ConsultarDatosPedido => True")

            If ValidarTipoVentaPermitido(strTipoVenta, oDetalle, responseVentaPermitida) Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - ValidarTipoVentaPermitido => True")

                If ValidarTipoOperacionPermitida(strTipoVenta, strTipoOperacion, oDetalle, responseOperacionPermitida) Then

                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - ValidarTipoOperacionPermitida => True")

                    If ValidarTipoProductoPermitido(strTipoVenta, strTipoOperacion, strDescTipoOperacion, oDetalle, responseProductoPermitida) Then

                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - ValidarTipoProductoPermitido => True")

                        If ValidarProteccionMovil(oDetalle, responseValidarPM) Then

                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - ValidarProteccionMovil => True")

                            If ValidarOperacionBuyBack(oDetalle, responseValidarBuyBack) Then

                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - ValidarOperacionBuyBack => True")

                                'PROY-140590 IDEA142068 - INICIO
                                If Not strTarjeta = String.Empty Then

                                    Dim codigoCampana As String = String.Empty

                                    If ValidarBinTarjeta(oDetalle, strTarjeta, codigoCampana, responseValidarBinTarjeta) Then

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - ValidarBinTarjeta => True")

                                        strCodCampana = Funciones.CheckStr(codigoCampana)
                                        validacionBinTarjeta = True
                                        rpta = True
                                        response.CodigoRespuesta = Funciones.CheckStr(responseValidarBinTarjeta.CodigoRespuesta)
                                        response.MensajeRespuesta = Funciones.CheckStr(responseValidarBinTarjeta.MensajeRespuesta)

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - CodigoRespuesta => " & response.CodigoRespuesta)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - MensajeRespuesta => " & response.MensajeRespuesta)

                                    Else

                                        response.CodigoRespuesta = Funciones.CheckStr(responseValidarBinTarjeta.CodigoRespuesta)
                                        response.MensajeRespuesta = Funciones.CheckStr(responseValidarBinTarjeta.MensajeRespuesta)

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - ValidarBinTarjeta => False")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - CodigoRespuesta => " & response.CodigoRespuesta)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - MensajeRespuesta => " & response.MensajeRespuesta)

                                    End If

                                Else
                                rpta = True
                                response.CodigoRespuesta = Funciones.CheckStr(responseValidarBuyBack.CodigoRespuesta)
                                response.MensajeRespuesta = Funciones.CheckStr(responseValidarBuyBack.MensajeRespuesta)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - CodigoRespuesta => " & response.CodigoRespuesta)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - MensajeRespuesta => " & response.MensajeRespuesta)

                                End If
                                'PROY-140590 IDEA142068 - FIN

                            Else
                                response.CodigoRespuesta = Funciones.CheckStr(responseValidarBuyBack.CodigoRespuesta)
                                response.MensajeRespuesta = Funciones.CheckStr(responseValidarBuyBack.MensajeRespuesta)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - ValidarOperacionBuyBack => False")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - CodigoRespuesta => " & response.CodigoRespuesta)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - MensajeRespuesta => " & response.MensajeRespuesta)
                            End If

                        Else
                            response.CodigoRespuesta = Funciones.CheckStr(responseValidarPM.CodigoRespuesta)
                            response.MensajeRespuesta = Funciones.CheckStr(responseValidarPM.MensajeRespuesta)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - ValidarProteccionMovil => False")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - CodigoRespuesta => " & response.CodigoRespuesta)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - MensajeRespuesta => " & response.MensajeRespuesta)
                        End If

                    Else
                        response.CodigoRespuesta = Funciones.CheckStr(responseProductoPermitida.CodigoRespuesta)
                        response.MensajeRespuesta = Funciones.CheckStr(responseProductoPermitida.MensajeRespuesta)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - ValidarTipoProductoPermitido => False")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - CodigoRespuesta => " & response.CodigoRespuesta)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - MensajeRespuesta => " & response.MensajeRespuesta)
                    End If

                Else
                    response.CodigoRespuesta = Funciones.CheckStr(responseOperacionPermitida.CodigoRespuesta)
                    response.MensajeRespuesta = Funciones.CheckStr(responseOperacionPermitida.MensajeRespuesta)

                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - ValidarTipoOperacionPermitida => False")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - CodigoRespuesta => " & response.CodigoRespuesta)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - MensajeRespuesta => " & response.MensajeRespuesta)
                End If

            Else
                response.CodigoRespuesta = Funciones.CheckStr(responseVentaPermitida.CodigoRespuesta)
                response.MensajeRespuesta = Funciones.CheckStr(responseVentaPermitida.MensajeRespuesta)

                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - ValidarTipoVentaPermitido => False")
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - CodigoRespuesta => " & response.CodigoRespuesta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - MensajeRespuesta => " & response.MensajeRespuesta)
            End If

        Else
            response.CodigoRespuesta = Funciones.CheckStr(responseConsultarPedido.CodigoRespuesta)
            response.MensajeRespuesta = Funciones.CheckStr(responseConsultarPedido.MensajeRespuesta)

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - ConsultarDatosPedido => False")
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - CodigoRespuesta => " & response.CodigoRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - MensajeRespuesta => " & response.MensajeRespuesta)
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatos - rpta : " & rpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "FIN ConsultarDatos")
        Return rpta

    End Function

    Private Function ConsultarDatosPedido(ByVal oDetalle As BEDetalleDatosPago, ByRef strTipoVenta As String, ByRef strTipoOperacion As String, ByRef strDescTipoOperacion As String, ByRef response As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "INICIO ConsultarDatosPedido")
        Dim rpta As Boolean = False
        Dim rptaActualizar As Boolean = False
        Dim oCabeceraActualizar As BECabeceraEstadoPago = New BECabeceraEstadoPago
        Dim strClaseFactura As String = String.Empty
        response = New BEResponseWebMethod
        Dim responseBiometria As BEResponseWebMethod = New BEResponseWebMethod

        Dim strEstadoPedido = String.Empty

        Dim dsPedido As DataSet
        Dim NumeroPedido As Int64 = CheckInt64(oDetalle.pedinNroPedido)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - NumeroPedido : " & NumeroPedido)
        dsPedido = objConsultaMsSap.ConsultaPedido(NumeroPedido, "", "")

        If Not IsNothing(dsPedido) Then

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - dsPedido not null")

            strTipoVenta = dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA")
            strTipoOperacion = dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION")
            strDescTipoOperacion = dsPedido.Tables(0).Rows(0).Item("PEDIV_DESCTIPOOPERACION")
            strClaseFactura = dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")
            strRentaAdelantada = dsPedido.Tables(0).Rows(0).Item("PEDIN_PEDIDORENTA")

            strTipoDocClientePed = dsPedido.Tables(0).Rows(0).Item("CLIEC_TIPODOCCLIENTE")
            strNroDocClientePed = dsPedido.Tables(0).Rows(0).Item("CLIEV_NRODOCCLIENTE")
            strFechaPedido = dsPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO")
            strEstadoPedido = dsPedido.Tables(0).Rows(0).Item("PEDIC_ESTADO")

            'PROY-140582 - INI
            strPedidoPM = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIN_PEDIDOPM"))
            'PROY-140582 - FIN

            strTipoVentaPago = strTipoVenta
            strTipoOperacionPago = strTipoOperacion

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - strTipoVenta : " & strTipoVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - strTipoOperacion : " & strTipoOperacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - strClaseFactura : " & strClaseFactura)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - strRentaAdelantada : " & strRentaAdelantada)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - strTipoDocClientePed : " & strTipoDocClientePed)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - strNroDocClientePed : " & strNroDocClientePed)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - strFechaPedido : " & strFechaPedido)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - strEstadoPedido : " & strEstadoPedido)

            If strEstadoPedido <> ConfigurationSettings.AppSettings("PEDIC_ESTADO") Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Estado del pedido es diferente a ACT")

                response.CodigoRespuesta = "1"
                response.MensajeRespuesta = "Estado del Pedido no permitido." 'Key_MsjRptaErrorEstadoNoPermitido


            ElseIf strEstadoPedido = ConfigurationSettings.AppSettings("ESTADO_PAG") Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Pedido se encuentra pagado")

                response.CodigoRespuesta = "1"
                response.MensajeRespuesta = "Pedido se encuentra pagado"
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Estado del pedido : PERMITIDO")

                If KeySettings.Key_ClaseFacturaPermitido.IndexOf(strClaseFactura) > -1 Then

                    If strClaseFactura = ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA") Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Pedido es Renta Adelantada")
                        isRentaAdelantada = True
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - isRentaAdelantada : True")

                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Pedido es Boleta o Factura")

                    End If

                    rpta = True

                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Clase de Factura no permitido")
                    response.CodigoRespuesta = "1"
                    response.MensajeRespuesta = KeySettings.Key_MsjRptaErrorClaseFactura
                End If

            End If

        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - dsPedido null")

            oCabeceraActualizar.uuid = oDetalle.uuid
            oCabeceraActualizar.codEstado = KeySettings.Key_CodigoEstadoError
            oCabeceraActualizar.codRpta = KeySettings.Key_CodigoRespuestaError
            oCabeceraActualizar.msjRpta = KeySettings.Key_MsjRptaPedidoSinDatos
            oCabeceraActualizar.nodoSicar = strNodo
            oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagNoReintentarPago


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ConsultarDatosPedido - oCabeceraActualizar.uuid", oCabeceraActualizar.uuid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ConsultarDatosPedido - oCabeceraActualizar.codEstado", oCabeceraActualizar.codEstado))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ConsultarDatosPedido - oCabeceraActualizar.codRpta", oCabeceraActualizar.codRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ConsultarDatosPedido - oCabeceraActualizar.msjRpta", oCabeceraActualizar.msjRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ConsultarDatosPedido - oCabeceraActualizar.nodoSicar", oCabeceraActualizar.nodoSicar))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ConsultarDatosPedido - oCabeceraActualizar.flagReintentar", oCabeceraActualizar.flagReintentar))

            'rptaActualizar = ActualizarEstadoPedido(oCabeceraActualizar, ipServer, usuario_id, ipcliente)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - rptaActualizar : " & rptaActualizar.ToString)

            response.CodigoRespuesta = "1"
            response.MensajeRespuesta = KeySettings.Key_MsjRptaPedidoSinDatos

        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - response.CodigoRespuesta : " & response.CodigoRespuesta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - response.MensajeRespuesta : " & response.MensajeRespuesta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ConsultarDatosPedido - rpta => " & rpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "FIN ConsultarDatosPedido")

        Return rpta
    End Function

    Private Function ValidarBiometria(ByVal NumeroPedido As Int64, ByVal strTipoDocMot As String, ByVal strNroDocMot As String, ByRef strOficinaPago As String, ByRef response As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Inicio Metodo ValidarBiometria()"))

        Dim rpta As Boolean = False
        response = New BEResponseWebMethod

        Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim objConsultaMSSAP As New clsConsultaMsSap
        Dim dsDatosPedido As DataSet

        Dim strCodRpta As String = String.Empty
        Dim strMsjRpta As String = String.Empty
        Dim strProducto As String = String.Empty
        Dim strEstadoBiometria As String = String.Empty

        Dim strFlagReqBiometria As String = String.Empty
        Dim strMontoPagar As String = String.Empty
        Dim strMontoPagarRA As String = String.Empty
        Dim strEstadoPedido As String = String.Empty
        Dim strCodMsj As String = String.Empty
        Dim strRptaMsj As String = String.Empty
        'PROY-140582 - INI
        Dim strMontoPagarPM As String = String.Empty
        'PROY-140582 - FIN
        'INICIATIVA-1006 | Tienda Virtual - ACC con costo | Declaracion de variables | INI
        Dim ArrayLst As ArrayList
        Dim strMontoPagar_ACC As String
        'INICIATIVA-1006 | Tienda Virtual - ACC con costo | Declaracion de variables | INI



        dsDatosPedido = objClsConsultaPvu.GetDatosPedidoDelivery(NumeroPedido, 0, strTipoDocMot, strNroDocMot, strCodRpta, strMsjRpta)

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Codigo Respuesta PVUDB", Funciones.CheckStr(strCodRpta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Mensaje Respuesta PVUDB", Funciones.CheckStr(strMsjRpta)))

        If strCodRpta = "0" AndAlso Not dsDatosPedido Is Nothing AndAlso dsDatosPedido.Tables.Count > 0 AndAlso dsDatosPedido.Tables(0).Rows.Count > 0 Then

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Datos del pedido delivery obtenidos correctamente"))

            strProducto = Funciones.CheckStr(dsDatosPedido.Tables(0).Rows(0).Item("PRODUCTO"))
            strEstadoBiometria = Funciones.CheckStr(dsDatosPedido.Tables(0).Rows(0).Item("ESTADO_BIOMETRIA"))
            strNumeroSEC = Funciones.CheckStr(dsDatosPedido.Tables(0).Rows(0).Item("SEC"))
            strOficinaPago = Funciones.CheckStr(dsDatosPedido.Tables(0).Rows(0).Item("OFICINA_PAGO"))


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Producto", strProducto))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Estado Biometria", strEstadoBiometria))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "strNumeroSEC", strNumeroSEC))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "strOficinaPago", strOficinaPago))

            'INICIATIVA-1006 | Tienda Virtual - ACC con costo | Consulta los datos de Pago | INI
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "[INICIATIVA-1006 - Tienda Virtual][Consulta Datos Monto Pagar][INPUT][NumeroPedido]", Funciones.CheckStr(NumeroPedido)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "[INICIATIVA-1006 - Tienda Virtual][Consulta Datos Monto Pagar][INPUT][strProducto]", strProducto))

            ArrayLst = objConsultaMSSAP.ConsultarDatosDeliveryPedido2(NumeroPedido, strProducto, strCodMsj, strRptaMsj)
            'strFlagReqBiometria = objConsultaMSSAP.ConsultarDatosDeliveryPedido(NumeroPedido, strProducto, strMontoPagar, strMontoPagarRA, strEstadoPedido, strMontoPagarPM, strCodMsj, strRptaMsj) 'PROY-140582

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "[INICIATIVA-1006 - Tienda Virtual][Consulta Datos Monto Pagar][INPUT][strProducto]", Funciones.CheckStr(ArrayLst.Count)))

            If Funciones.CheckInt64(ArrayLst.Count) > 0 Then

                For Each item As ItemGenerico In ArrayLst
                    strMontoPagar = item.MONTO_PAGAR
                    strMontoPagarRA = item.MONTO_PAGAR_RA
                    strEstadoPedido = item.ESTADO_PEDIDO
                    strFlagReqBiometria = item.FLAG_BIOMETRIA
                    strMontoPagarPM = item.MONTO_PM
                    strMontoPagar_ACC = item.MONTO_ACC_COSTO
                Next

            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "[INICIATIVA-1006 - Tienda Virtual][Consulta Datos Monto Pagar][OUTPUT][strMontoPagar]", strMontoPagar))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "[INICIATIVA-1006 - Tienda Virtual][Consulta Datos Monto Pagar][OUTPUT][strMontoPagarRA]", strMontoPagarRA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "[INICIATIVA-1006 - Tienda Virtual][Consulta Datos Monto Pagar][OUTPUT][strEstadoPedido]", strEstadoPedido))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "[INICIATIVA-1006 - Tienda Virtual][Consulta Datos Monto Pagar][OUTPUT][strFlagReqBiometria]", strFlagReqBiometria))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "[INICIATIVA-1006 - Tienda Virtual][Consulta Datos Monto Pagar][OUTPUT][strMontoPagarPM]", strMontoPagarPM))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "[INICIATIVA-1006 - Tienda Virtual][Consulta Datos Monto Pagar][OUTPUT][strMontoPagar_ACC]", strMontoPagar_ACC))

            Dim dblMontoPagar_ACC As Double = Funciones.CheckDbl(strMontoPagar_ACC)

            If dblMontoPagar_ACC > 0 Then

                Dim dblMonto As Double = Funciones.CheckDbl(strMontoPagar)
                Dim dblMontoTOTAL As Double
                dblMontoTOTAL = dblMonto + dblMontoPagar_ACC
                strMontoPagar = Funciones.CheckStr(dblMontoTOTAL)

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "[INICIATIVA-1006 - Tienda Virtual][Calculo Monto Total con ACC con costo][POSTVALIDACION][strMontoPagar]", strMontoPagar))

            End If

            'INICIATIVA-1006 | FIN

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Codigo Respuesta MSSAP", Funciones.CheckStr(strCodRpta)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "Mensaje Respuesta MSSAP", Funciones.CheckStr(strMsjRpta)))

            'PROY-140582 - INI
            If strFlagPM.Equals("1") Then

                Dim dblMontoPM As Double = Funciones.CheckDbl(strMontoPagarPM)
                Dim dblMonto As Double = Funciones.CheckDbl(strMontoPagar)
                Dim dblMontoTotal As Double

                dblMontoTotal = dblMontoPM + dblMonto
                strMontoTotal = Funciones.CheckStr(dblMontoTotal)
            Else
            strMontoTotal = strMontoPagar
            End If
            'PROY-140582 - FIN
            strFlagBiometria = strEstadoBiometria

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "strMontoTotal", strMontoTotal))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "strFlagBiometria", strFlagBiometria))

            If strEstadoBiometria = "1" Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Pedido no tiene biometria vigente"))

                If strFlagReqBiometria = "1" Then

                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Pedido debe pasar por Biometria"))

                    response.CodigoRespuesta = "1"
                    response.MensajeRespuesta = "Pedido debe pasar por Biometria" 'Key_MsjRptaErrorBiometria
                Else

                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Pedido no debe pasar por Biometria"))
                    rpta = True
                End If

            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Pedido tiene biometria vigente"))

                rpta = True

            End If

        Else

            response.CodigoRespuesta = "1"
            response.MensajeRespuesta = "No se encontro datos del pedido delivery" 'Key_MsjRptaErrorDatosDelivery
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "No se encontro datos del pedido delivery"))

        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "rpta", Funciones.CheckStr(rpta)))

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "response.CodigoRespuesta", Funciones.CheckStr(response.CodigoRespuesta)))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "response.MensajeRespuesta", Funciones.CheckStr(response.MensajeRespuesta)))

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Fin Metodo ValidarBiometria()"))

        Return rpta

    End Function

    Private Function ValidarTipoVentaPermitido(ByVal strTipoVenta As String, ByVal oDetalle As BEDetalleDatosPago, ByRef response As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "INICIO ValidarTipoVentaPermitido")

        Dim rpta As Boolean = False
        Dim rptaActualizar As Boolean = False
        response = New BEResponseWebMethod
        Dim oCabeceraActualizar As BECabeceraEstadoPago = New BECabeceraEstadoPago


        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoVentaPermitido - strTipoVenta", strTipoVenta))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoVentaPermitido - Key_TipoVentaPermitido", Funciones.CheckStr(KeySettings.Key_TipoVentaPermitido)))

        If KeySettings.Key_TipoVentaPermitido.IndexOf(strTipoVenta) > -1 Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Tipo de venta permitido")
            rpta = True
        Else

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Tipo de venta NO permitido")

            oCabeceraActualizar.uuid = oDetalle.uuid
            oCabeceraActualizar.codEstado = KeySettings.Key_CodigoEstadoError
            oCabeceraActualizar.codRpta = KeySettings.Key_CodigoRespuestaError
            oCabeceraActualizar.msjRpta = KeySettings.Key_MsjRptaErrorTipoVenta
            oCabeceraActualizar.nodoSicar = strNodo
            oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagNoReintentarPago

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoVentaPermitido - oCabeceraActualizar.uuid", oCabeceraActualizar.uuid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoVentaPermitido - oCabeceraActualizar.codEstado", oCabeceraActualizar.codEstado))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoVentaPermitido - oCabeceraActualizar.codRpta", oCabeceraActualizar.codRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoVentaPermitido - oCabeceraActualizar.msjRpta", oCabeceraActualizar.msjRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoVentaPermitido - oCabeceraActualizar.nodoSicar", oCabeceraActualizar.nodoSicar))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoVentaPermitido - oCabeceraActualizar.flagReintentar", oCabeceraActualizar.flagReintentar))

            'rptaActualizar = ActualizarEstadoPedido(oCabeceraActualizar, ipServer, usuario_id, ipcliente)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoVentaPermitido - rptaActualizar : " & rptaActualizar)

            response.CodigoRespuesta = "1"
            response.MensajeRespuesta = KeySettings.Key_MsjRptaErrorTipoVenta

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoVentaPermitido - response.CodigoRespuesta : " & response.CodigoRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoVentaPermitido - response.MensajeRespuesta : " & response.MensajeRespuesta)

        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoVentaPermitido - rpta => " & rpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "FIN ValidarTipoVentaPermitido")
        Return rpta
    End Function

    Private Function ValidarTipoOperacionPermitida(ByVal strTipoVenta As String, ByVal strTipoOperacion As String, ByVal oDetalle As BEDetalleDatosPago, ByRef response As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "INICIO ValidarTipoOperacionPermitida")
        Dim rpta As Boolean = False
        Dim rptaActualizar As Boolean = False
        response = New BEResponseWebMethod
        Dim responseVentasVarias As BEResponseWebMethod = New BEResponseWebMethod
        Dim oCabeceraActualizar As BECabeceraEstadoPago = New BECabeceraEstadoPago
        Dim configTipoVenta As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("strTVPostpago"))

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoOperacionPermitida - strTipoVenta : " & strTipoVenta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoOperacionPermitida - strTipoOperacion : " & strTipoOperacion)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoOperacionPermitida - configTipoVenta : " & configTipoVenta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoOperacionPermitida - Key_TipoOperacionPostPermitido : " & KeySettings.Key_TipoOperacionPostPermitido)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoOperacionPermitida - Key_TipoOperacionPrePermitido : " & KeySettings.Key_TipoOperacionPrePermitido)

        If strTipoVenta = configTipoVenta Then

            If KeySettings.Key_TipoOperacionPostPermitido.IndexOf(strTipoOperacion) > -1 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Tipo de Operacion Postpago Permitida.")
                rpta = True
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Tipo de Operacion Postpago NO Permitida.")
            End If

        Else
            If KeySettings.Key_TipoOperacionPrePermitido.IndexOf(strTipoOperacion) > -1 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Tipo de Operacion Prepago Permitida.")
                rpta = True
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Tipo de Operacion Prepago NOPermitida.")
            End If
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoOperacionPermitida - rpta : " & rpta)


        If rpta Then

            If strTipoOperacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("COD_TIPO_OPERACION_VENTA_VARIOS")) Then
                If ValidarVentasVarias(oDetalle.pedinNroPedido, responseVentasVarias) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Venta varias puede continuar con el pago.")
                    rpta = True
                Else
                    rpta = False
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Venta varias NO puede continuar con el pago.")

                    response.CodigoRespuesta = responseVentasVarias.CodigoRespuesta
                    response.MensajeRespuesta = responseVentasVarias.MensajeRespuesta

                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoOperacionPermitida - response.CodigoRespuesta : " & response.CodigoRespuesta)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoOperacionPermitida - response.MensajeRespuesta : " & response.MensajeRespuesta)
                End If
            End If

        Else

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Tipo de operacion no permitida")

            oCabeceraActualizar.uuid = oDetalle.uuid
            oCabeceraActualizar.codEstado = KeySettings.Key_CodigoEstadoError
            oCabeceraActualizar.codRpta = KeySettings.Key_CodigoRespuestaError
            oCabeceraActualizar.msjRpta = KeySettings.Key_MsjRptaErrorTipoOperacion
            oCabeceraActualizar.nodoSicar = strNodo
            oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagNoReintentarPago

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoOperacionPermitida - oCabeceraActualizar.uuid", oCabeceraActualizar.uuid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoOperacionPermitida - oCabeceraActualizar.codEstado", oCabeceraActualizar.codEstado))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoOperacionPermitida - oCabeceraActualizar.codRpta", oCabeceraActualizar.codRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoOperacionPermitida - oCabeceraActualizar.msjRpta", oCabeceraActualizar.msjRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoOperacionPermitida - oCabeceraActualizar.nodoSicar", oCabeceraActualizar.nodoSicar))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoOperacionPermitida - oCabeceraActualizar.flagReintentar", oCabeceraActualizar.flagReintentar))

            'rptaActualizar = ActualizarEstadoPedido(oCabeceraActualizar, ipServer, usuario_id, ipcliente)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoOperacionPermitida - rptaActualizar : " & rptaActualizar)

            response.CodigoRespuesta = "1"
            response.MensajeRespuesta = KeySettings.Key_MsjRptaErrorTipoOperacion

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoOperacionPermitida - response.CodigoRespuesta : " & response.CodigoRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoOperacionPermitida - response.MensajeRespuesta : " & response.MensajeRespuesta)

        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoOperacionPermitida - rpta => " & rpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoOperacionPermitida - response.CodigoRespuesta : " & response.CodigoRespuesta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoOperacionPermitida - response.MensajeRespuesta : " & response.MensajeRespuesta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "FIN ValidarTipoOperacionPermitida")

        Return rpta
    End Function

    Private Function ValidarVentasVarias(ByVal strPedidoAcc As String, ByRef responseVentasVarias As BEResponseWebMethod) As Boolean

        Dim rpta As Boolean = False
        Dim dsPedidoAcc As New DataSet
        Dim dsPedidoPack As New DataSet
        Dim strResultado As String
        Dim strPedidoPack As String
        Dim strEstadoPedidoPack As String
        Dim objConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        responseVentasVarias = New BEResponseWebMethod

        Try

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Entrada SP_OBTENER_PACK_ACC: P_PEDIDO_ACC = " & strPedidoAcc)
            dsPedidoAcc = objConsultaPvu.ObtenerPedidoPackAccesorio(strPedidoAcc, strResultado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Salida SP_OBTENER_PACK_ACC: K_RESULTADO = " & strResultado)

            If strResultado = "OK" Then
                If Not dsPedidoAcc Is Nothing Then
                    If dsPedidoAcc.Tables(0).Rows.Count > 0 Then
                        strResultado = String.Empty
                        strPedidoPack = dsPedidoAcc.Tables(0).Rows(0)(1)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Entrada SSAPSS_PEDIDO_PACK_ACC: P_PEDIDO_PACK = " & strPedidoPack)
                        dsPedidoPack = objConsultaMsSap.ValidarPagoPackAccesorio(strPedidoPack, strResultado)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Salida SSAPSS_PEDIDO_PACK_ACC: K_RESULTADO = " & strResultado)

                        If strResultado = "OK" Then
                            If Not dsPedidoPack Is Nothing Then
                                If dsPedidoPack.Tables(0).Rows.Count > 0 Then
                                    strEstadoPedidoPack = dsPedidoPack.Tables(0).Rows(0)(1)

                                    If strEstadoPedidoPack <> ConfigurationSettings.AppSettings("ESTADO_PAG").ToString() Then
                                        'Response.Write("<script>alert('" & ConfigurationSettings.AppSettings("MSG_ERROR_PAGO_PACK").ToString & "');</script>")
                                        responseVentasVarias.CodigoRespuesta = "1"
                                        responseVentasVarias.MensajeRespuesta = Funciones.CheckStr(ConfigurationSettings.AppSettings("MSG_ERROR_PAGO_PACK"))
                                        Exit Function
                                    End If
                                End If
                            End If
                        Else
                            'Response.Write("<script>alert('" & ConfigurationSettings.AppSettings("MSG_ERROR_VALIDAR_ACC").ToString & "');</script>")
                            responseVentasVarias.CodigoRespuesta = "1"
                            responseVentasVarias.MensajeRespuesta = Funciones.CheckStr(ConfigurationSettings.AppSettings("MSG_ERROR_VALIDAR_ACC"))
                            Exit Function
                        End If
                    End If
                End If
            Else
                'Response.Write("<script>alert('" & ConfigurationSettings.AppSettings("MSG_ERROR_VALIDAR_ACC").ToString & "');</script>")
                responseVentasVarias.CodigoRespuesta = "1"
                responseVentasVarias.MensajeRespuesta = Funciones.CheckStr(ConfigurationSettings.AppSettings("MSG_ERROR_VALIDAR_ACC"))
                Exit Function
            End If

            rpta = True
        Catch ex As Exception
            responseVentasVarias.CodigoRespuesta = "1"
            responseVentasVarias.MensajeRespuesta = Funciones.CheckStr(ConfigurationSettings.AppSettings("MSG_ERROR_VALIDAR_ACC"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Error consulta si es accesorio y si tiene pack relacionado pendiente de pago")
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Exception" & "- " & ex.ToString)
        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarVentasVarias - responseVentasVarias.CodigoRespuesta : " & responseVentasVarias.CodigoRespuesta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarVentasVarias - responseVentasVarias.MensajeRespuesta : " & responseVentasVarias.MensajeRespuesta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarVentasVarias - rpta => " & rpta)
        Return rpta
    End Function



    Private Function ValidarTipoProductoPermitido(ByVal strTipoVenta As String, ByVal strTipoOperacion As String, ByVal strDescTipoOperacion As String, ByVal oDetalle As BEDetalleDatosPago, ByRef response As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "INICIO ValidarTipoProductoPermitido")
        Dim rpta As Boolean = False
        Dim rptaActualizar As Boolean = False
        response = New BEResponseWebMethod
        Dim responseValidarEstadoPort As BEResponseWebMethod = New BEResponseWebMethod
        Dim strTipoProducto As String = String.Empty
        Dim configTipoVenta As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("strTVPostpago"))
        Dim oCabeceraActualizar As BECabeceraEstadoPago = New BECabeceraEstadoPago

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - strTipoVenta : " & strTipoVenta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - configTipoVenta : " & configTipoVenta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - strTipoOperacion : " & strTipoOperacion)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - Key_TipoProductoPostPermitido : " & KeySettings.Key_TipoProductoPostPermitido)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - Key_TipoProductoPrePermitido : " & KeySettings.Key_TipoProductoPrePermitido)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - Key_ReposicionPostpago : " & KeySettings.Key_ReposicionPostpago)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - Key_Repo_Reno_RenoPackPrepago : " & KeySettings.Key_Repo_Reno_RenoPackPrepago)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - oDetalle.pedinNroPedido : " & oDetalle.pedinNroPedido)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - strNumeroSEC : " & strNumeroSEC)

        If strTipoVenta = configTipoVenta Then

            strTipoProducto = ObtenerProductoPostpago(oDetalle.pedinNroPedido)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - strTipoProducto : " & strTipoProducto)

            If KeySettings.Key_TipoProductoPostPermitido.IndexOf(strTipoProducto) > -1 Or KeySettings.Key_ReposicionPostpago.IndexOf(strTipoOperacion) > -1 Or KeySettings.Key_VentasVariasPostpago.IndexOf(strTipoOperacion) > -1 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Tipo de producto Postpago permitido.")
                rpta = True
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Tipo de producto Postpago NO permitido.")
            End If

        Else

            strTipoProducto = ObtenerProductoPrepago(oDetalle.pedinNroPedido)

            If KeySettings.Key_TipoProductoPrePermitido.IndexOf(strTipoProducto) > -1 Or KeySettings.Key_Repo_Reno_RenoPackPrepago.IndexOf(strTipoOperacion) > -1 Or KeySettings.Key_VentasVariasPostpago.IndexOf(strTipoOperacion) > -1 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Tipo de producto Prepago permitido.")
                rpta = True
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Tipo de producto Prepago NO permitido.")
            End If
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - rpta : " & Funciones.CheckStr(rpta))


        If rpta Then

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - PRODUCTO PERMITIDO")

            If ValidarEstadoPortabilidad(strTipoVenta, strTipoOperacion, strDescTipoOperacion, strNumeroSEC, oDetalle.pedinNroPedido, responseValidarEstadoPort) Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - ValidarEstadoPortabilidad : TRUE")
                rpta = True

            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - ValidarEstadoPortabilidad : FALSE")

                rpta = False

                response.CodigoRespuesta = responseValidarEstadoPort.CodigoRespuesta
                response.MensajeRespuesta = responseValidarEstadoPort.MensajeRespuesta

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoProductoPermitido - response.CodigoRespuesta ", response.CodigoRespuesta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoProductoPermitido - response.MensajeRespuesta", response.MensajeRespuesta))

                oCabeceraActualizar.uuid = oDetalle.uuid
                oCabeceraActualizar.codEstado = KeySettings.Key_CodigoEstadoError
                oCabeceraActualizar.codRpta = KeySettings.Key_CodigoRespuestaError
                oCabeceraActualizar.msjRpta = responseValidarEstadoPort.MensajeRespuesta
                oCabeceraActualizar.nodoSicar = strNodo
                oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagNoReintentarPago

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoProductoPermitido - oCabeceraActualizar.uuid", oCabeceraActualizar.uuid))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoProductoPermitido - oCabeceraActualizar.codEstado", oCabeceraActualizar.codEstado))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoProductoPermitido - oCabeceraActualizar.codRpta", oCabeceraActualizar.codRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoProductoPermitido - oCabeceraActualizar.msjRpta", oCabeceraActualizar.msjRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoProductoPermitido - oCabeceraActualizar.nodoSicar", oCabeceraActualizar.nodoSicar))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoProductoPermitido - oCabeceraActualizar.flagReintentar", oCabeceraActualizar.flagReintentar))

                'rptaActualizar = ActualizarEstadoPedido(oCabeceraActualizar, ipServer, usuario_id, ipcliente)
                'objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - rptaActualizar : " & rptaActualizar)

                response.CodigoRespuesta = "1"
                response.MensajeRespuesta = KeySettings.Key_MsjRptaErrorEstadoPortabilidad

            End If

        Else

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - PRODUCTO NO PERMITIDO")

            oCabeceraActualizar.uuid = oDetalle.uuid
            oCabeceraActualizar.codEstado = KeySettings.Key_CodigoEstadoError
            oCabeceraActualizar.codRpta = KeySettings.Key_CodigoRespuestaError
            oCabeceraActualizar.msjRpta = KeySettings.Key_MsjRptaErrorTipoProducto
            oCabeceraActualizar.nodoSicar = strNodo
            oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagNoReintentarPago

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoProductoPermitido - oCabeceraActualizar.uuid", oCabeceraActualizar.uuid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoProductoPermitido - oCabeceraActualizar.codEstado", oCabeceraActualizar.codEstado))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoProductoPermitido - oCabeceraActualizar.codRpta", oCabeceraActualizar.codRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoProductoPermitido - oCabeceraActualizar.msjRpta", oCabeceraActualizar.msjRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoProductoPermitido - oCabeceraActualizar.nodoSicar", oCabeceraActualizar.nodoSicar))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarTipoProductoPermitido - oCabeceraActualizar.flagReintentar", oCabeceraActualizar.flagReintentar))

            'rptaActualizar = ActualizarEstadoPedido(oCabeceraActualizar, ipServer, usuario_id, ipcliente)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - rptaActualizar : " & rptaActualizar)

            response.CodigoRespuesta = "1"
            response.MensajeRespuesta = KeySettings.Key_MsjRptaErrorTipoProducto

        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - response.CodigoRespuesta : " & response.CodigoRespuesta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - response.MensajeRespuesta : " & response.MensajeRespuesta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarTipoProductoPermitido - rpta => " & rpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "FIN ValidarTipoProductoPermitido")
        Return rpta
    End Function

    Private Function ValidarEstadoPortabilidad(ByVal strTipoVenta As String, ByVal strTipoOperacion As String, ByVal strDescTipoOperacion As String, ByVal strNumeroSEC As String, ByVal strNumeroPedido As String, ByRef responseValidarEstadoPort As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Evalua si el pedido a procesar no es una RV.")

        Dim rpta As Boolean = False
        Dim PEDIN_NRODOCUMENTO As Int64 = Funciones.CheckInt64(strNumeroPedido)
        Dim dsPedido As DataSet
        Dim strLineaValidar As String = String.Empty
        Dim strTipo As String = String.Empty
        Dim strMensajeAlert As String = String.Empty
        Dim odtLineasEvaluadas As DataTable

        'dsPedido = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drPagos.Item("PEDIV_PEDIDOSAP"), "") ''TODO: CALLBACK SAP
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "El pedido No es una RV")
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Inicial consulta del Pedido(objConsultaMsSap.ConsultaPedido -  PKG_MSSAP.SSAPSS_PEDIDO)")
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Paràmetros: ")
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Param1: " & Funciones.CheckStr(PEDIN_NRODOCUMENTO))
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Param2: " & "")
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Param3: " & "")
        dsPedido = objConsultaMsSap.ConsultaPedido(PEDIN_NRODOCUMENTO, "", "")
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Fin consulta del Pedido.")


        Try
            If Not dsPedido Is Nothing Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "strCodTipOpe: " & strTipoOperacion)
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "strDesTipOpe: " & strDescTipoOperacion)

                'Venta Postpago = 01
                Dim strTVPostpago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("strTVPostpago"))
                Dim strDescriOpePostPago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_POST"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "strTVPostpago: " & strTVPostpago)
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "strDescriOpePostPago: " & strDescriOpePostPago)

                'Venta Prepago  = 02
                Dim strTVPrepago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("strTVPrepago"))
                Dim strDescriOpePrePago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_PRE"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "strTVPrepago: " & strTVPrepago)
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "strDescriOpePrePago: " & strDescriOpePrePago)

                Dim objPorta As New ClsPortabilidad
                Dim FlagPorta As Boolean = False

                If (strTipoVenta = strTVPostpago) And (strDescTipoOperacion = strDescriOpePostPago) Then
                    'POSTPAGO
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "strNroSec: " & strNumeroSEC)
                    odtLineasEvaluadas = ListLineasEvaluadasPostPago(strNumeroSEC)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "odtLineasEvaluadas: " & odtLineasEvaluadas.Rows.Count.ToString())
                    FlagPorta = True
                    strTipo = "POSTPAGO"
                ElseIf (strTipoVenta = strTVPrepago) And (strDescTipoOperacion = strDescriOpePrePago) Then
                    'PREPAGO
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "strNroSec===>: " & strNumeroSEC)
                    odtLineasEvaluadas = ListLineasEvaluadasPrepago(strNumeroSEC)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "odtLineasEvaluadas: " & odtLineasEvaluadas.Rows.Count.ToString())
                    FlagPorta = True
                    strTipo = "PREPAGO"
                End If
                objPorta = Nothing

                If FlagPorta = True Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Ini validate_estado_cp")

                    blFlagPortabilidad = True

                    '*** PROY 32089 INI -  ***'
                    Dim objDatosTipoProducto As New ClsPortabilidad
                    Dim pstrCodRpta1, pstrMsgRpta1 As String

                    Dim dtLineas As DataSet = objDatosTipoProducto.Obtener_tipo_producto(strNumeroSEC, PEDIN_NRODOCUMENTO, pstrCodRpta1, pstrMsgRpta1)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Ini clsKeyAPP.consTipoProductoPermitidosSP" & Funciones.CheckStr(clsKeyAPP.consTipoProductoPermitidosSP))
                    If clsKeyAPP.consTipoProductoPermitidosSP.IndexOf(dtLineas.Tables(0).Rows(0).Item("PRDC_CODIGO")) = -1 Then   'PROY 32089
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "No es un tipo de Producto [consTipoProductoPermitidosSP]" & Funciones.CheckStr(clsKeyAPP.consTipoProductoPermitidosSP))
                        If validate_estado_cp(strNumeroSEC, dsPedido.Tables(0), dsPedido.Tables(1), odtLineasEvaluadas, strLineaValidar, strTipo, strMensajeAlert) = 0 Then
                            responseValidarEstadoPort.CodigoRespuesta = "1"
                            responseValidarEstadoPort.MensajeRespuesta = strMensajeAlert

                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "validate_estado_cp => false")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "strNroSec: " & Funciones.CheckStr(responseValidarEstadoPort.CodigoRespuesta))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "strNroSec: " & Funciones.CheckStr(responseValidarEstadoPort.MensajeRespuesta))
                        Else
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "validate_estado_cp => true")
                            rpta = True
                        End If

                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "consTipoProductoPermitidosSP => true")
                        rpta = True
                    End If
                    '*** PROY 32089 FIN -  ***
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Pedido no es portabilidad")
                    rpta = True
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Fin validate_estado_cp")
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "No se encontraron registros para el pedido: " & Funciones.CheckStr(PEDIN_NRODOCUMENTO))
                responseValidarEstadoPort.CodigoRespuesta = "1"
                responseValidarEstadoPort.MensajeRespuesta = "Ocurrio un error al validar el estado de la portabilidad" 'Key_MsjRptaErrorEstadoPortabilidad

            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Error al asignar los valores del pedido.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- " & "Err. " & ex.Message.ToString)
            responseValidarEstadoPort.CodigoRespuesta = "1"
            responseValidarEstadoPort.MensajeRespuesta = "Ocurrio un error al validar el estado de la portabilidad" 'Key_MsjRptaErrorEstadoPortabilidad
        End Try

        Return rpta

    End Function

    Private Function validate_estado_cp(ByVal NroSec As String, ByVal odtPedido As DataTable, _
    ByVal odtDetalle As DataTable, ByVal odtLineaEva As DataTable, ByRef strLinea As String, ByVal TipoLinea As String, ByRef strMsgAlert As String) As Short

        Dim intValidarCp As Short = 1
        Dim strconEquipo As String = ""
        Dim strTipoMate As String = ""
        Dim strNroTelefono As String = ""
        Dim strAddLinea As String = ""

        'Dim odtListPorta As DataTable = Me.ListLineasEvaluadasPrepago(NroSec)
        Dim odtListPorta As DataTable = odtLineaEva

        'ListLineasEvaluadasPrepago

        Dim odtHeader As DataTable = odtPedido
        Dim odtDetail As DataTable = odtDetalle
        If (odtHeader.Rows.Count > 0 And odtDetail.Rows.Count > 0) Then
            Dim i As Int32 = 0
            For i = 0 To odtDetail.Rows.Count - 1
                strTipoMate = Funciones.CheckStr(odtDetail.Rows(i)("MATEC_TIPOMATERIAL"))
                'Validar que sea el tipo es CHIP
                If strTipoMate = Funciones.CheckStr(ConfigurationSettings.AppSettings("constChips")) Then
                    strNroTelefono = Funciones.CheckStr(odtDetail.Rows(i)("DEPEV_NROTELEFONO"))
                    If validate_linea(strNroTelefono, odtListPorta, TipoLinea, strMsgAlert) = False Then
                        strLinea = strNroTelefono
                        intValidarCp = 0
                        Exit For
                    Else
                        strAddLinea += strNroTelefono & "|"
                        intValidarCp = 1
                    End If
                End If
            Next
        Else
            'error en el pedido
            intValidarCp = 0
        End If

        If strAddLinea.Length > 0 Then
            strAddLinea = strAddLinea.Substring(0, strAddLinea.Length - 1)
            'Me.hidNroLineas.Value = strAddLinea
        End If

        Return intValidarCp


    End Function

    Private Function ListLineasEvaluadasPostPago(ByVal NroSec As String) As DataTable

        Dim dsData As DataSet
        Dim objPorta As New ClsPortabilidad

        Dim estadoSPEjecutarPort As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("constEstadoSPEjecutarPortabilidad"))

        dsData = objPorta.ObtenerLineasPortabilidadPost(NroSec, Nothing, "", estadoSPEjecutarPort, "")

        Return dsData.Tables(0)

    End Function

    Private Function ListLineasEvaluadasPrepago(ByVal NroSec As String) As DataTable
        Dim dsData As DataSet
        Dim objPorta As New ClsPortabilidad
        dsData = objPorta.ObtenerLineasPortabilidadPre(NroSec, "")
        Return dsData.Tables(0)
    End Function

    Private Function validate_linea(ByVal strLinea As String, ByVal listPorta As DataTable, ByVal Tipo As String, ByRef Mensaje As String) As Boolean

        Dim strEstadosPortaCP As String = ConfigurationSettings.AppSettings("ConsEstadosPortaCP").ToString()
        Dim strPortaCPArrys As String()

        If strEstadosPortaCP.IndexOf("|") >= 0 Then
            strPortaCPArrys = strEstadosPortaCP.Split("|"c)
        Else
            strPortaCPArrys = New String(0) {}
            strPortaCPArrys(0) = strEstadosPortaCP
        End If

        Dim bolValidate As Boolean = False
        Dim strEstadoCP As String = ""

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[{0}|{1}]-INICIO VALIDACION CONSULTA PREVIA", strLinea, Tipo))
        For i As Integer = 0 To listPorta.Rows.Count - 1
            If strLinea.Trim() = Funciones.CheckStr(listPorta.Rows(i)("PORT_NUMERO")) Then
                Dim bolExit As Boolean = True
                'PROY-32089 INI
                Dim str_TIPO_VENTA = ""
                Select Case Tipo
                    Case "PREPAGO"
                        str_TIPO_VENTA = ConfigurationSettings.AppSettings("strTVPrepago")
                    Case "POSTPAGO"
                        str_TIPO_VENTA = ConfigurationSettings.AppSettings("strTVPostpago")
                End Select
                Dim oBEPorttConfiguracion As New BEPorttConfiguracion
                With oBEPorttConfiguracion
                    .PORTV_EST_PROCESO = Funciones.CheckStr(listPorta.Rows(i)("SOPOC_ESTA_PROCESO_CP"))
                    .PORTV_MOTIVO = Funciones.CheckStr(listPorta.Rows(i)("SOPOV_MOTIVO_CP"))
                    .PORTV_FLAG_ACREDITA = Funciones.CheckStr(Funciones.CheckInt(listPorta.Rows(i)("SOPOC_FLAG_ACREDITACION")))
                    .PORTV_OPERADOR = Funciones.CheckStr(listPorta.Rows(i)("SOPOC_CODIGO_CEDENTE"))
                    .PORTC_TIPO_PRODUCTO = Funciones.CheckStr(listPorta.Rows(i)("PRDC_CODIGO"))
                    .PORTV_TIPO_VENTA = str_TIPO_VENTA ' PROY-32089
                    .PORTV_MOD_VENTA = IIf(Funciones.CheckStr(listPorta.Rows(i)("SOPOC_CHIPPACK")).Trim() = "C", "|C|", "|P|")
                    .PORTV_APLICACION = ConfigurationSettings.AppSettings("ConsNombreAplicacion")
                End With
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[{0}|{1}]-INICIO VALIDACION CONSULTA PREVIA-[{2}-{3}-{4}-{5}-{6}-{7}]", strLinea, Tipo, oBEPorttConfiguracion.PORTV_EST_PROCESO, oBEPorttConfiguracion.PORTV_MOTIVO, oBEPorttConfiguracion.PORTV_FLAG_ACREDITA, oBEPorttConfiguracion.PORTV_OPERADOR, oBEPorttConfiguracion.PORTC_TIPO_PRODUCTO, oBEPorttConfiguracion.PORTV_MOD_VENTA))
                Dim resultValidacionCP() As String = New ClsPortabilidad().PorttValidaABCDP(oBEPorttConfiguracion)
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[{0}|{1}]-INICIO VALIDACION CONSULTA PREVIA-[{2}-{3}-{4}]", strLinea, Tipo, resultValidacionCP(0), resultValidacionCP(1), resultValidacionCP(2)))
                bolExit = (resultValidacionCP(0) = 0)
                If bolExit = False Then
                    'rechazar el pago no es procedente para la portabiliad
                    bolValidate = False
                    Mensaje = String.Format(ConfigurationSettings.AppSettings("ConsPortaCPRechazoPago"), strLinea.Trim())
                    Exit For
                Else
                    bolValidate = True
                End If
                'PROY-32089 FIN
            End If
        Next
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[{0}|{1}]-FIN VALIDACION CONSULTA PREVIA", strLinea, Tipo))
        Return bolValidate
    End Function

    Private Function ValidarProteccionMovil(ByVal oDetalle As BEDetalleDatosPago, ByRef response As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "INICIO ValidarProteccionMovil")
        Dim rpta As Boolean = False
        Dim rptaActualizar As Boolean = False
        response = New BEResponseWebMethod
        Dim oCabeceraActualizar As BECabeceraEstadoPago = New BECabeceraEstadoPago

        Dim objProteccionMovil As New COM_SIC_Activaciones.clsProteccionMovil
        Dim strNroPedidoEquipo As String = String.Empty
        Dim strCodRpta As String = String.Empty
        Dim strMsgRpta As String = String.Empty

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - pedinNroPedido : " & oDetalle.pedinNroPedido)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - strNroPedidoEquipo : " & strNroPedidoEquipo)

        objProteccionMovil.ValidaPagoEquipoProteccionMovil(oDetalle.pedinNroPedido, strNroPedidoEquipo, strCodRpta, strMsgRpta)

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - strCodRpta : " & strCodRpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - strMsgRpta : " & strMsgRpta)

        If (strCodRpta.Equals("-2")) Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - inicio ConsultarProteccionMovil")
            Dim dsProteccionMovil As DataSet = objProteccionMovil.ConsultarProteccionMovil(oDetalle.pedinNroPedido, strCodRpta, strMsgRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - strCodRpta : " & strCodRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - strMsgRpta : " & strMsgRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - fin ConsultarProteccionMovil")

            If strCodRpta = "0" AndAlso Not dsProteccionMovil Is Nothing AndAlso dsProteccionMovil.Tables.Count > 0 AndAlso dsProteccionMovil.Tables(0).Rows.Count > 0 Then

                'PROY-140582 - INI
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Pedido tiene Proteccion Movil")

                Dim strPermitirPM As String = Funciones.CheckStr(KeySettings.Key_FlagPermitirProteccionMovil)
             
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil - KeySettings.Key_FlagPermitirProteccionMovil", strPermitirPM))

                If strPermitirPM = "0" Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil", "No se permite pago con Proteccion Movil"))
                oCabeceraActualizar.uuid = oDetalle.uuid
                oCabeceraActualizar.codEstado = KeySettings.Key_CodigoEstadoError
                oCabeceraActualizar.codRpta = KeySettings.Key_CodigoRespuestaError
                oCabeceraActualizar.msjRpta = KeySettings.Key_MsjRptaErrorPedidoConPM
                oCabeceraActualizar.nodoSicar = strNodo
                oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagNoReintentarPago


                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil - oCabeceraActualizar.uuid", oCabeceraActualizar.uuid))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil - oCabeceraActualizar.codEstado", oCabeceraActualizar.codEstado))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil - oCabeceraActualizar.codRpta", oCabeceraActualizar.codRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil - oCabeceraActualizar.msjRpta", oCabeceraActualizar.msjRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil - oCabeceraActualizar.nodoSicar", oCabeceraActualizar.nodoSicar))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil - oCabeceraActualizar.flagReintentar", oCabeceraActualizar.flagReintentar))

                'rptaActualizar = ActualizarEstadoPedido(oCabeceraActualizar, ipServer, usuario_id, ipcliente)
                'objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - rptaActualizar : " & rptaActualizar)

                response.CodigoRespuesta = "1"
                response.MensajeRespuesta = KeySettings.Key_MsjRptaErrorPedidoConPM

                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - response.CodigoRespuesta : " & response.CodigoRespuesta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - response.MensajeRespuesta : " & response.MensajeRespuesta)
            Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil", "Se permite pago con Proteccion Movil"))
                    rpta = True
                End If
                'PROY-140582 - FIN
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - dsProteccionMovil.Tables(0).Rows.Count < 0 ")
                rpta = True
            End If

        Else

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Pedido es Proteccion Movil")

            oCabeceraActualizar.uuid = oDetalle.uuid
            oCabeceraActualizar.codEstado = KeySettings.Key_CodigoEstadoError
            oCabeceraActualizar.codRpta = KeySettings.Key_CodigoRespuestaError
            oCabeceraActualizar.msjRpta = KeySettings.Key_MsjRptaErrorPedidoEsPM
            oCabeceraActualizar.nodoSicar = strNodo
            oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagNoReintentarPago

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil - oCabeceraActualizar.uuid", oCabeceraActualizar.uuid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil - oCabeceraActualizar.codEstado", oCabeceraActualizar.codEstado))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil - oCabeceraActualizar.codRpta", oCabeceraActualizar.codRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil - oCabeceraActualizar.msjRpta", oCabeceraActualizar.msjRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil - oCabeceraActualizar.nodoSicar", oCabeceraActualizar.nodoSicar))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarProteccionMovil - oCabeceraActualizar.flagReintentar", oCabeceraActualizar.flagReintentar))

            'rptaActualizar = ActualizarEstadoPedido(oCabeceraActualizar, ipServer, usuario_id, ipcliente)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - rptaActualizar : " & rptaActualizar)

            response.CodigoRespuesta = "1"
            response.MensajeRespuesta = KeySettings.Key_MsjRptaErrorPedidoEsPM

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - response.CodigoRespuesta : " & response.CodigoRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - response.MensajeRespuesta : " & response.MensajeRespuesta)
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarProteccionMovil - rpta => " & rpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "FIN ValidarProteccionMovil")
        Return rpta

    End Function

    Private Function ValidarOperacionBuyBack(ByVal oDetalle As BEDetalleDatosPago, ByVal responseValidarBuyBack As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "INICIO ValidarOperacionBuyBack")

        Dim rpta As Boolean = False
        Dim rptaActualizar As Boolean = False
        responseValidarBuyBack = New BEResponseWebMethod
        Dim oCabeceraActualizar As BECabeceraEstadoPago = New BECabeceraEstadoPago

        Dim objConsultaMSSAP As New clsConsultaMsSap
        Dim strCodRespuesta As String = String.Empty
        Dim strMsjRespuesta As String = String.Empty

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarOperacionBuyBack - pedinNroPedido : " & oDetalle.pedinNroPedido)

        objConsultaMSSAP.ConsultaCupon(oDetalle.pedinNroPedido, strCodRespuesta, strMsjRespuesta)

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarOperacionBuyBack - strCodRespuesta : " & strCodRespuesta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarOperacionBuyBack - strMsjRespuesta : " & strMsjRespuesta)

        If strCodRespuesta.Equals("0") Then

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "Pedido es BuyBack")

            oCabeceraActualizar.uuid = oDetalle.uuid
            oCabeceraActualizar.codEstado = KeySettings.Key_CodigoEstadoError
            oCabeceraActualizar.codRpta = KeySettings.Key_CodigoRespuestaError
            oCabeceraActualizar.msjRpta = KeySettings.Key_MsjRptaPedidoBuyBack
            oCabeceraActualizar.nodoSicar = strNodo
            oCabeceraActualizar.flagReintentar = KeySettings.Key_FlagNoReintentarPago

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarOperacionBuyBack - oCabeceraActualizar.uuid", oCabeceraActualizar.uuid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarOperacionBuyBack - oCabeceraActualizar.codEstado", oCabeceraActualizar.codEstado))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarOperacionBuyBack - oCabeceraActualizar.codRpta", oCabeceraActualizar.codRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarOperacionBuyBack - oCabeceraActualizar.msjRpta", oCabeceraActualizar.msjRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarOperacionBuyBack - oCabeceraActualizar.nodoSicar", oCabeceraActualizar.nodoSicar))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarOperacionBuyBack - oCabeceraActualizar.flagReintentar", oCabeceraActualizar.flagReintentar))

            'rptaActualizar = ActualizarEstadoPedido(oCabeceraActualizar, ipServer, usuario_id, ipcliente)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarOperacionBuyBack - rptaActualizar : " & rptaActualizar)

            responseValidarBuyBack.CodigoRespuesta = "1"
            responseValidarBuyBack.MensajeRespuesta = KeySettings.Key_MsjRptaPedidoBuyBack

            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarOperacionBuyBack - response.CodigoRespuesta : " & responseValidarBuyBack.CodigoRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarOperacionBuyBack - response.MensajeRespuesta : " & responseValidarBuyBack.MensajeRespuesta)

        Else
            rpta = True
            responseValidarBuyBack.CodigoRespuesta = "0"
            responseValidarBuyBack.MensajeRespuesta = "Validaciones Correctas"
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarOperacionBuyBack - rpta => " & rpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "FIN ValidarOperacionBuyBack")
        Return rpta
    End Function

    Private Function ObtenerProductoPostpago(ByVal strNumeroPedido As String) As String
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "INICIO ObtenerProductoPostpago")
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerProductoPostpago - strNumeroPedido : " & strNumeroPedido)

        Dim strCodProducto As String = String.Empty

        Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim P_CODIGO_RESPUESTA As String = String.Empty
        Dim P_MENSAJE_RESPUESTA As String = String.Empty
        Dim C_VENTA As DataTable
        Dim C_VENTA_DET As DataTable

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerProductoPostpago - strNumeroPedido : " & strNumeroPedido)

        objClsConsultaPvu.ConsultarPedidosPVU(strNumeroPedido, _
                                                      P_CODIGO_RESPUESTA, _
                                                      P_MENSAJE_RESPUESTA, _
                                                      C_VENTA, _
                                                      C_VENTA_DET)

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerProductoPostpago - P_CODIGO_RESPUESTA : " & P_CODIGO_RESPUESTA)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerProductoPostpago - P_MENSAJE_RESPUESTA : " & P_MENSAJE_RESPUESTA)

        If P_MENSAJE_RESPUESTA <> "OK" Then
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  No se encontro el pedido en la tabla : sisact_info_venta_sap. pkg:sisact_pkg_venta sp:sp_con_venta_x_docsap => " & drPagos.Item("PEDIN_NROPEDIDO"))
        End If

        If Not C_VENTA_DET Is Nothing Then
            If C_VENTA_DET.Rows.Count > 0 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerProductoPostpago - C_VENTA_DET.Rows.Count > 0")
                strCodProducto = Funciones.CheckStr(C_VENTA_DET.Rows(0).Item("PRDC_CODIGO"))
            End If
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerProductoPostpago - strCodProducto => " & strCodProducto)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "FIN ObtenerProductoPostpago")
        Return strCodProducto

    End Function

    Private Function ObtenerProductoPrepago(ByVal strNumeroPedido As String) As String
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "INICIO ObtenerProductoPrepago")
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerProductoPrepago - strNumeroPedido : " & strNumeroPedido)

        Dim strCodProducto As String = String.Empty

        Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim P_CODIGO_RESPUESTA As String = String.Empty
        Dim P_MENSAJE_RESPUESTA As String = String.Empty
        Dim C_VENTA As DataTable
        Dim C_VENTA_DET As DataTable

        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerProductoPrepago - strNumeroPedido : " & strNumeroPedido)
        objClsConsultaPvu.ConsultarPedidosPrepago(strNumeroPedido, P_CODIGO_RESPUESTA, P_MENSAJE_RESPUESTA, C_VENTA, C_VENTA_DET)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerProductoPrepago - P_CODIGO_RESPUESTA : " & P_CODIGO_RESPUESTA)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerProductoPrepago - P_MENSAJE_RESPUESTA : " & P_MENSAJE_RESPUESTA)

        If P_MENSAJE_RESPUESTA <> "OK" Then
            ' objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  No se encontro el pedido en la tabla : sisact_info_venta_sap. pkg:sisact_pkg_venta sp:sp_con_venta_x_docsap => " & drPagos.Item("PEDIN_NROPEDIDO"))
        End If

        If Not C_VENTA_DET Is Nothing Then
            If C_VENTA_DET.Rows.Count > 0 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerProductoPrepago - C_VENTA_DET.Rows.Count > 0")
                strCodProducto = Funciones.CheckStr(C_VENTA_DET.Rows(0).Item("DVPR_COD_PROD_PREP"))
            End If
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ObtenerProductoPrepago - strCodProducto => " & strCodProducto)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "FIN ObtenerProductoPrepago")
        Return strCodProducto
    End Function

    Private Function ValidarPagoRA(ByVal idPedido As String) As Boolean
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "INICIO ValidarPagoRA")
        '****** CONSULTA SI TIENE RENTA SIN PAGAR PARA SEGUIR EL PREOCESO *****'
        'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia consulta si ha Pagado Renta antes de Pagar")
        Dim COD_MSG_RENTA As String = String.Empty
        Dim COD_RESPUESTA As String = String.Empty
        Dim MSJ_RESPUESTA As String = String.Empty
        Dim rpta As Boolean = False

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarPagoRA - idPedido : " & idPedido)
            objConsultaMsSap.Validar_PagoRenta(Funciones.CheckDbl(idPedido), COD_MSG_RENTA, COD_RESPUESTA, MSJ_RESPUESTA)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarPagoRA - COD_RESPUESTA : " & COD_RESPUESTA)
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "ValidarPagoRA - MSJ_RESPUESTA : " & MSJ_RESPUESTA)


            If COD_MSG_RENTA = "0" Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "El documento tiene asociado RA no Pagada")

            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "El documento tiene asociado RA Pagada")

                rpta = True
            End If

        Catch ex As Exception
            ' objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error consulta si ha Pagado Renta antes de Pagar")
            ' objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception" & "- " & ex.ToString)
            'Exit Function
        End Try
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "rpta => " & rpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "FIN ValidarPagoRA")
        Return rpta
    End Function

    'PROY-140590 IDEA142068 - INICIO
    Private Function ValidarBinTarjeta(ByVal oDetalle As BEDetalleDatosPago, ByVal strTarjeta As String, ByRef descCampana As String, ByRef responseValidarBin As BEResponseWebMethod) As Boolean

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "INICIO ValidarBinTarjeta"))

        Dim rpta As Boolean = False

        responseValidarBin = New BEResponseWebMethod

        Dim objConsultaPvu As New clsConsultaPvu
        Dim objConsultaMsSap As New clsConsultaMsSap

        Dim strCodRespuesta As String = String.Empty
        Dim strMsjRespuesta As String = String.Empty

        Try
            Dim dsPedido As DataSet
            Dim dsDatos As DataSet
            Dim dsCampana As DataSet

            Dim strNroPedido As String = Funciones.CheckStr(oDetalle.pedinNroPedido)
            Dim tipoVenta As String = String.Empty
            Dim operacion As String = String.Empty
            Dim codCampana As String = String.Empty

            Dim validacion As Boolean = False
            Dim validCampana As Boolean = False
            Dim validCodTarjeta As Boolean = False

            Dim codTarjeta As String = String.Empty

            Dim key_campaniasSTBK As String = Funciones.CheckStr(clsKeyAPP.Key_campaniaActivasSTBK())

            Dim NumeroTarjeta As String = strTarjeta.Substring(0, 6)

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} {2}", IdentificadorLog, "ValidarBinTarjeta - pedinNroPedido : ", strNroPedido))

            dsPedido = objConsultaMsSap.ConsultaPedido(strNroPedido, "", "")

            If Not dsPedido Is Nothing AndAlso dsPedido.Tables.Count > 0 AndAlso dsPedido.Tables(0).Rows.Count > 0 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "ValidarBinTarjeta - Consulta de Pedido exitosa en MSSAP"))

                tipoVenta = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA"))
                operacion = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION"))

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} {2}", IdentificadorLog, "ValidarBinTarjeta - Tipo Venta : ", tipoVenta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} {2}", IdentificadorLog, "ValidarBinTarjeta - Operacion : ", operacion))

                dsDatos = objConsultaPvu.ConsultaDatosCampania(strNroPedido, tipoVenta, operacion, strCodRespuesta, strMsjRespuesta)

                If strCodRespuesta = "0" AndAlso Not dsDatos Is Nothing AndAlso dsDatos.Tables.Count > 0 AndAlso dsDatos.Tables(0).Rows.Count > 0 Then

                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "ValidarBinTarjeta - Consulta de Pedido exitosa en PVUDB"))

                    For i As Int32 = 0 To dsDatos.Tables(0).Rows.Count - 1
                        codCampana = Funciones.CheckStr(dsDatos.Tables(0).Rows(i).Item("CAMPANA"))

                        If Not key_campaniasSTBK = String.Empty Then
                            Dim arrayCampana As Array = key_campaniasSTBK.Split("|")
                            For index As Int32 = 0 To arrayCampana.Length - 1
                                Dim campana As String = Funciones.CheckStr(arrayCampana(index))
                                If codCampana = campana Then
                                    validacion = True
                                    Exit For
                                End If
                            Next
                        End If

                        If (validacion) Then

                            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} {2}", IdentificadorLog, "ValidarBinTarjeta - Codigo de Campana : ", codCampana))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} {2}", IdentificadorLog, "ValidarBinTarjeta - Bin de Tarjeta : ", NumeroTarjeta))

                            dsCampana = objConsultaPvu.ConsultaCampaniaBin(codCampana, NumeroTarjeta, strCodRespuesta, strMsjRespuesta)
                            If strCodRespuesta = "0" AndAlso Not dsCampana Is Nothing AndAlso dsCampana.Tables.Count > 0 AndAlso dsCampana.Tables(0).Rows.Count > 0 Then

                                For x As Int32 = 0 To dsCampana.Tables(0).Rows.Count - 1
                                    codTarjeta = Funciones.CheckStr(dsCampana.Tables(0).Rows(x).Item("TARJV_CODIGO"))
                                    If codTarjeta = strCodigoPago Then
                                        validCodTarjeta = True
                                        Exit For
                                    End If
                                Next

                                If (validCodTarjeta) Then
                                    validCampana = True
                                    descCampana = Funciones.CheckStr(codCampana)
                                    Exit For
                                End If

                            End If

                        End If
                    Next

                    If (validCampana) Then

                        rpta = True
                        responseValidarBin.CodigoRespuesta = "0"
                        responseValidarBin.MensajeRespuesta = "Validaciones Correctas"

                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "ValidarBinTarjeta - No se encontro registro entre el Bin y la Campana"))

                        responseValidarBin.CodigoRespuesta = "1"
                        responseValidarBin.MensajeRespuesta = "No se pudo validar Bin de Tarjeta."

                    End If

                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "ValidarBinTarjeta - Error al consultar Pedido en PVUDB"))

                    responseValidarBin.CodigoRespuesta = "1"
                    responseValidarBin.MensajeRespuesta = "No se pudo validar Bin de Tarjeta."

                End If

            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "ValidarBinTarjeta - Error al consultar Pedido en MSSAP"))

                responseValidarBin.CodigoRespuesta = "1"
                responseValidarBin.MensajeRespuesta = "No se pudo validar Bin de Tarjeta."

            End If

        Catch ex As Exception

            responseValidarBin.CodigoRespuesta = "1"
            responseValidarBin.MensajeRespuesta = "No se pudo validar Bin de Tarjeta."

        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} {2}", IdentificadorLog, "ValidarBinTarjeta - rpta => ", rpta))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} {2}", IdentificadorLog, "ValidarBinTarjeta - response.CodigoRespuesta : ", responseValidarBin.CodigoRespuesta))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} {2}", IdentificadorLog, "ValidarBinTarjeta - response.MensajeRespuesta : ", responseValidarBin.MensajeRespuesta))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "FIN ValidarBinTarjeta"))

        Return rpta

    End Function

    Private Sub RegistrarPedidoSTBK(ByVal idPedido As String, ByVal strCodCampana As String, ByVal strTarjeta As String)
        Dim objConsultaPvu As New clsConsultaPvu
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "[INICIO][RegistrarPedidoSTBK]"))
        Try
            Dim dsCampanaBin As DataSet
            Dim dsPedido As DataSet

            Dim strCodRpta As String = String.Empty
            Dim strMsjRpta As String = String.Empty

            Dim NumeroTarjeta As String = strTarjeta.Substring(0, 6)
            Dim NumeroTarjeta2 As String = strTarjeta.Substring(0, 4)
            Dim NumeroTarjeta3 As String = strTarjeta.Substring(4, 2)

            dsCampanaBin = objConsultaPvu.ConsultaCampaniaBin(strCodCampana, NumeroTarjeta, strCodRpta, strMsjRpta)

            If strCodRpta = "0" AndAlso Not dsCampanaBin Is Nothing AndAlso dsCampanaBin.Tables.Count > 0 AndAlso dsCampanaBin.Tables(0).Rows.Count > 0 Then

                Dim IntPedido As Int64 = Funciones.CheckInt64(idPedido)
                Dim CodigoTarjeta As String = String.Format("{0}-{1}XX-XXXX-XXXX", NumeroTarjeta2, NumeroTarjeta3)
                Dim TipoTarjeta As Int64 = Funciones.CheckInt64(dsCampanaBin.Tables(0).Rows(0).Item("BINN_IDTARJETA"))
                Dim AppOrigen As String = Funciones.CheckStr(clsKeyAPP.Key_PagoAppVentas())
                Dim Usuario As String = Funciones.CheckStr("USERAPP")

                Dim strTipoDoc As String = String.Empty
                Dim strNumDoc As String = String.Empty
                Dim strFechaVenta As String = String.Empty
                Dim strTipoOper As String = String.Empty
                Dim strTipoVenta As String = String.Empty
                Dim strDescVenta As String = String.Empty
                Dim strMontoTotal As String = String.Empty
                Dim strMontoPago As String = String.Empty

                dsPedido = objConsultaMsSap.ConsultaPedido(idPedido, "", "")

                If Not dsPedido Is Nothing AndAlso dsPedido.Tables.Count > 0 AndAlso dsPedido.Tables(0).Rows.Count > 0 Then
                    strTipoDoc = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("CLIEC_TIPODOCCLIENTE"))
                    strNumDoc = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("CLIEV_NRODOCCLIENTE"))
                    Dim fecha As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO"))

                    If Not fecha = String.Empty Then
                        Dim fechaSt As DateTime = Funciones.CheckDate(fecha)
                        strFechaVenta = fechaSt.ToString("dd/MM/yyyy")
                    End If

                    strTipoOper = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION"))

                    strTipoVenta = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA"))

                    If strTipoVenta = "01" Then
                        strDescVenta = "POSTPAGO"
                    ElseIf strTipoVenta = "02" Then
                        strDescVenta = "PREPAGO"
                    Else
                        strDescVenta = "OTROS"
                    End If

                    strMontoTotal = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"))

                    strMontoPago = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"))

                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "[INICIO][GuardarCampaniaPedidoBin]"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "PI_PEDIN_NROPEDIDO", IntPedido))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "PI_CODIGO_TARJETA", CodigoTarjeta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "PI_TIPO_TARJETA", TipoTarjeta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "PI_APPORIGEN", AppOrigen))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "PI_USUARIO_REGISTRO", Usuario))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "PI_TIPO_DOC", strTipoDoc))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "PI_NUMERO_DOC", strNumDoc))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "PI_FECHA_VENTA", strFechaVenta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "PI_TIPO_OPER", strTipoOper))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "PI_TIPO_VENTA", strTipoVenta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "PI_DESC_VENTA", strDescVenta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "PI_MONTO_TOTAL", strMontoTotal))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "PI_MONTO_PAGO", strMontoPago))

                strCodRpta = objConsultaPvu.GuardarCampaniaPedidoBin(IntPedido, CodigoTarjeta, TipoTarjeta, AppOrigen, Usuario, strTipoDoc, strNumDoc, strFechaVenta, strTipoOper, strTipoVenta, strDescVenta, strMontoTotal, strMontoPago, strMsjRpta)

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "strCodRpta", strCodRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "strMsjRpta", strMsjRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "[FIN][GuardarCampaniaPedidoBin]"))

            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2} {3}", IdentificadorLog, "ERROR", ex.Message, ex.StackTrace))
        End Try
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "[FIN][RegistrarPedidoSTBK]"))
    End Sub
    'PROY-140590 IDEA142068 - FIN

'PROY-140582 - INI
    Private Function ValidarPagoPM(ByVal idPedido As String) As Boolean
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "INICIO ValidarPagoPM"))
        Dim rpta As Boolean = False
        Try
            Dim objProteccionMovil As New COM_SIC_Activaciones.clsProteccionMovil
            Dim strNroPedidoEquipo As String = String.Empty
            Dim strCodRpta As String = String.Empty
            Dim strMsgRpta As String = String.Empty

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarPagoPM - idPedido", idPedido))

            objProteccionMovil.ValidaPagoEquipoProteccionMovil(idPedido, strNroPedidoEquipo, strCodRpta, strMsgRpta)

            If (strCodRpta.Equals("-2")) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Inicio ConsultarProteccionMovil"))
                Dim dsProteccionMovil As DataSet = objProteccionMovil.ConsultarProteccionMovil(idPedido, strCodRpta, strMsgRpta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ConsultarProteccionMovil - strCodRpta", strCodRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ConsultarProteccionMovil - strMsgRpta", strMsgRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "Fin ConsultarProteccionMovil"))

                If strCodRpta = "0" AndAlso Not dsProteccionMovil Is Nothing AndAlso dsProteccionMovil.Tables.Count > 0 AndAlso dsProteccionMovil.Tables(0).Rows.Count > 0 Then
                    Dim dsPedido As DataSet
                    Dim pedidoPM As Int64
                    Dim estado_pago As String = String.Empty

                    pedidoPM = Funciones.CheckInt64(strPedidoPM)
                    dsPedido = objConsultaMsSap.ConsultaPedido(pedidoPM, "", "")

                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarPagoPM - strPedidoPM", strPedidoPM))

                    If Not dsPedido Is Nothing AndAlso dsPedido.Tables.Count > 0 AndAlso dsPedido.Tables(0).Rows.Count > 0 Then

                        estado_pago = dsPedido.Tables(0).Rows(0).Item("PEDIC_ESTADO")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarPagoPM - estado_pago", estado_pago))

                        If estado_pago = ConfigurationSettings.AppSettings("ESTADO_PAG") Then
                            rpta = True
                        End If

                    Else
                        rpta = True
                    End If
                Else
                    rpta = True
                End If
            Else
                rpta = True
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarPagoPM", ex.Message))
        End Try
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", IdentificadorLog, "ValidarPagoPM - rpta", rpta))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", IdentificadorLog, "FIN ValidarPagoPM"))
        Return rpta
    End Function
    'PROY-140582 - FIN

    Public Shared ReadOnly Property CurrentNodo() As String

        Get
            Dim nodo_Servidor As String = Environment.MachineName

            If Not (nodo_Servidor) Is Nothing AndAlso nodo_Servidor.Length > 2 Then
                Return nodo_Servidor.Substring(nodo_Servidor.Length - 2)
            End If

            Return String.Empty
        End Get
    End Property
End Class
