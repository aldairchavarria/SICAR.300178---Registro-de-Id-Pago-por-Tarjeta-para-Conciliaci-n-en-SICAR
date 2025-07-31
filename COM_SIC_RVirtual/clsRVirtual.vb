Imports System.EnterpriseServices
Imports SwichTransaccional.Services

'<Transaction(TransactionOption.Required), JustInTimeActivation(True), Synchronization(SynchronizationOption.Required)> _
Public Class clsRVirtual
    '   Inherits ServicedComponent
    Public Enum eNvTIMPPVError
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Nombre            : eNvTIMPPVError
        ' Implementado Por  : Richard Carlo Rodriguez Balcazar
        ' Fecha Creación    : 21/07/2004
        ' Proposito         : Define los codigos de error devueltos por los metodos
        ' Modificado Por    : JYMMY TORRES
        ' Fecha Modificación: 2013.07.17
        ' Modificado Por    : xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        ' Fecha Modificación: xxxx.xx.xx
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        OperacionExitosa = 0
        ErrorDeComunicaciones = -1  ' Error de comunicaciones. En la propiedad CodigoRpta se almacena el codigo de error generado
        ErrorAplicativo = -2        ' Error del Aplicativo. Revisar las propiedades OrigenRpta y CodigoRpta para determinar que sistema contesto la transaccion y el código de rechazo
        ErrorDeExcepcion = -3       ' Error de comunicaciones. En la propiedad CodigoRpta se almacena el codigo de error generado
    End Enum
    Dim NvTIMPPVComponentName As String = "NvTIMPPVx.NvTIMPPV"
    Private Const NvTIMPPVCaller As Long = 3

    '  <AutoComplete(True)> _
    Public Function Recarga(ByVal codOficina As String, ByVal pvTerminalID As String, _
                                ByVal pvTrace As String, _
                                ByVal pvCanal As String, _
                                ByVal pvTelefono As String, _
                                ByVal pvMonto As String, _
                                ByVal pvMoneda As String, _
                                ByVal pvProducto As String, _
                                ByVal pvBinAdquiriente As String, _
                                ByVal pvCodCadena As String, _
                                ByVal pvCodComercio As String, _
                                ByVal pvDatosUsuario As String, _
                                Optional ByVal pvRUC As String = "", _
                                Optional ByVal pvNombreCliente As String = "") _
                                As String
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Implementado Por  : Richard Carlo Rodriguez Balcazar
        ' Fecha Creación    : 21/07/2004
        ' Proposito         : Permite realizar una recarga a un celular determinado
        '                     Esta transacción permite efectuar una recarga en línea
        '                     en el celular especificado.
        ' Modificado Por    : JYMMY TORRES
        ' Fecha Modificación: 2013.07.17
        ' Modificado Por    : xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        ' Fecha Modificación: xxxx.xx.xx
        ' Output            : Error, OrigenRpta, CodigoRpta, DescripcionRpta,
        '                     SaldoResultante, MonedaSaldo, NumeroRecarga,
        '                     NumeroDocumentoAutorizado, ValorRecarga, ValorVenta,
        '                     ValorDescuento, ValorSubTotal, ValorIGV, ValorTotal
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim lError As eNvTIMPPVError
        Dim lOrigenRpta As Long
        Dim lCodigoRpta As Long
        Dim lDescripcionRpta As String
        Dim lSaldoResultante As Double
        Dim lMonedaSaldo As Long
        Dim lNumeroRecarga As Long
        Dim lNumeroDocumentoAutorizado As String
        Dim lValorRecarga As Double
        Dim lValorVenta As Double
        Dim lValorDescuento As Double
        Dim lValorSubTotal As Double
        Dim lValorIGV As Double
        Dim lValorTotal As Double
        Dim objComponente
        Dim usuario As String
        Dim fechaTransaccion As String
        Dim traceRecarga$


        lError = eNvTIMPPVError.OperacionExitosa ' Por defecto se asume la operacion como exitosa
        ' On Error GoTo HandlerErrorApp

        Dim wsSwichTransaccional As New ServiciosClaro
        Dim recargaVirtual As New RecargaRequest
        Dim recargaVirtualResponse As New RecargaResponse

        Try
            '//PBI000002202042 INI SE QUITO VALIDACION

                    With recargaVirtual
                        .binAdquiriente = System.Configuration.ConfigurationSettings.AppSettings("BIN_ADQUIRIENTE")

                        .binAdquirienteReenvia = System.Configuration.ConfigurationSettings.AppSettings("CONST_BIN_REENVIO")
                        .canal = pvCanal
                        .codigoMoneda = pvMoneda
                        .fechaCaptura = Date.Now.ToString("yyyy-MM-dd-05:00")
                        .fechaTransaccion = Date.Now.ToString("yyyy-MM-ddTHH:mm:ss.ms-05:00")
                        .fechaCapturaSpecified = True
                        .fechaTransaccionSpecified = True
                        .datosUsuario = pvDatosUsuario 'IDEA-141711 - Pack Internet Móvil Prepago
                        .numeroTerminal = Left(Trim(pvTerminalID), 8)
                        .trace = pvTrace
                        .montoRecarga = pvMonto
                        .montoRecargaSpecified = True
                        .numeroTelefono = FormatoTelefono(Trim(pvTelefono))
                        .producto = pvProducto 'IDEA-141711 - Pack Internet Móvil Prepago
                        .codigoFormato = System.Configuration.ConfigurationSettings.AppSettings("COD_FORMATO_RV")
                        .numeroComercio = pvCodComercio

                        .nombreComercio = System.Configuration.ConfigurationSettings.AppSettings("CONST_NOMBRE_COMERCIO")
                        '.binAdquiriente = Left(Trim(pvBinAdquiriente), 6)
                        '.codigoCadena = Left(Trim(pvCodCadena), 15)
                        '.numeroComercio = Left(Trim(pvCodComercio), 15)
                        '.nombreComprador = Left(Trim(pvNombreCliente), 50)
                        .procesador = System.Configuration.ConfigurationSettings.AppSettings("CONST_PROCESADOR")
                        .telco = System.Configuration.ConfigurationSettings.AppSettings("CONST_TELCO")
                        .tipoDocumentoComprador = "1"
                        .codigoCadena = System.Configuration.ConfigurationSettings.AppSettings("CODIGO_CADENA")
                        .numeroReferencia = System.Configuration.ConfigurationSettings.AppSettings("CONST_NUMEROREFERENCIA")




                    End With

                    recargaVirtualResponse = wsSwichTransaccional.recarga(recargaVirtual)

                    With recargaVirtualResponse
                        lOrigenRpta = .codigoOrigenRespuesta
                        lCodigoRpta = .codigoRespuesta
                        lDescripcionRpta = .descripcionExtendidaRespuesta

                        If CInt(lCodigoRpta) = eNvTIMPPVError.OperacionExitosa Then     '** Linea original **'
                            'If CInt(lCodigoRpta) = "80" Then                                 '** Para pruebas   **'
                            lSaldoResultante = .montoSaldo
                            lMonedaSaldo = .codigoMonedaSaldo
                            lNumeroRecarga = .numeroRecarga
                            lNumeroDocumentoAutorizado = .numeroDocumentoAutorizado
                            lValorRecarga = .valorRecarga
                            lValorVenta = .valorVenta
                            lValorDescuento = .descuento
                            lValorSubTotal = .subTotal
                            lValorIGV = .igv
                            lValorTotal = .total
                            fechaTransaccion = .fechaTransaccion
                            traceRecarga = .trace
                            'Call SP_comSetComplete()
                            'GoTo Fin
                            'Else
                            '   GoTo HandlerErrorComponente
                        End If
                    End With

            '//PBI000002202042 FIN SE COMENTO VALIDACION

        Catch ex As Exception
            'HandlerErrorApp:
            ' Controlar y devolver error del aplicativo
            lError = eNvTIMPPVError.ErrorAplicativo
            lOrigenRpta = NvTIMPPVCaller
            lCodigoRpta = Err.Number
            lDescripcionRpta = Err.Description

            'HandlerErrorComponente:
            'Call SP_comSetAbort()

            'Fin:
        Finally
            objComponente = Nothing

            ' Valores devueltos por el metodo
            Recarga = Trim(CStr(lError)) & ";" & Trim(CStr(lOrigenRpta)) & ";" & _
                      Trim(CStr(lCodigoRpta)) & ";" & Trim(CStr(lDescripcionRpta)) & ";" & _
                      Trim(CStr(lSaldoResultante)) & ";" & Trim(CStr(lMonedaSaldo)) & ";" & _
                      Trim(CStr(lNumeroRecarga)) & ";" & Trim(CStr(lNumeroDocumentoAutorizado)) & ";" & _
                      Trim(CStr(lValorRecarga)) & ";" & Trim(CStr(lValorVenta)) & ";" & _
                      Trim(CStr(lValorDescuento)) & ";" & Trim(CStr(lValorSubTotal)) & ";" & _
                      Trim(CStr(lValorIGV)) & ";" & Trim(CStr(lValorTotal)) & ";" & fechaTransaccion & ";" & traceRecarga
        End Try

        'Try
        '    Select Case codOficina
        '        Case "1"
        '            ' Inicio logica de la operacion
        '            objComponente = CreateObject(NvTIMPPVComponentName)
        '            With objComponente
        '                .TerminalID = Left(Trim(pvTerminalID), 8)
        '                .Trace = pvTrace
        '                .Canal = pvCanal
        '                '.Telefono = Left(Trim(pvTelefono), 11) 'modificado JCR
        '                .Telefono = FormatoTelefono(Trim(pvTelefono))
        '                .Monto = pvMonto
        '                .Moneda = pvMoneda
        '                .Producto = pvProducto
        '                .BinAdquiriente = Left(Trim(pvBinAdquiriente), 6)
        '                .CodCadena = Left(Trim(pvCodCadena), 15)
        '                .CodComercio = Left(Trim(pvCodComercio), 15)
        '                .Ruc = Left(Trim(pvRUC), 14)
        '                .NombreCliente = Left(Trim(pvNombreCliente), 50)

        '                lError = .Recarga

        '                lOrigenRpta = .OrigenRpta
        '                lCodigoRpta = .CodigoRpta
        '                lDescripcionRpta = .DescripcionRpta

        '                If lError = eNvTIMPPVError.OperacionExitosa Then
        '                    lSaldoResultante = .SaldoResultante
        '                    lMonedaSaldo = .MonedaSaldo
        '                    lNumeroRecarga = .NumeroRecarga
        '                    lNumeroDocumentoAutorizado = .NumeroDocumentoAutorizado
        '                    lValorRecarga = .ValorRecarga
        '                    lValorVenta = .ValorVenta
        '                    lValorDescuento = .ValorDescuento
        '                    lValorSubTotal = .ValorSubTotal
        '                    lValorIGV = .ValorIGV
        '                    lValorTotal = .ValorTotal
        '                    'Call SP_comSetComplete()
        '                    'GoTo Fin
        '                    'Else
        '                    '   GoTo HandlerErrorComponente
        '                End If
        '            End With
        '            ' Fin logica de la operacion
        '        Case Else
        '            Recarga = "0;2;0;RESPUESTA OK;;;;;;;;;;;"
        '    End Select
        '    '      Exit Function
        'Catch ex As Exception
        '    'HandlerErrorApp:
        '    ' Controlar y devolver error del aplicativo
        '    lError = eNvTIMPPVError.ErrorAplicativo
        '    lOrigenRpta = NvTIMPPVCaller
        '    lCodigoRpta = Err.Number
        '    lDescripcionRpta = Err.Description

        '    'HandlerErrorComponente:
        '    'Call SP_comSetAbort()

        '    'Fin:
        'Finally
        '    objComponente = Nothing

        '    ' Valores devueltos por el metodo
        '    Recarga = Trim(CStr(lError)) & ";" & Trim(CStr(lOrigenRpta)) & ";" & _
        '              Trim(CStr(lCodigoRpta)) & ";" & Trim(CStr(lDescripcionRpta)) & ";" & _
        '              Trim(CStr(lSaldoResultante)) & ";" & Trim(CStr(lMonedaSaldo)) & ";" & _
        '              Trim(CStr(lNumeroRecarga)) & ";" & Trim(CStr(lNumeroDocumentoAutorizado)) & ";" & _
        '              Trim(CStr(lValorRecarga)) & ";" & Trim(CStr(lValorVenta)) & ";" & _
        '              Trim(CStr(lValorDescuento)) & ";" & Trim(CStr(lValorSubTotal)) & ";" & _
        '              Trim(CStr(lValorIGV)) & ";" & Trim(CStr(lValorTotal))
        'End Try
    End Function


    '------------------------------------------------------------------------------------------------------------------'
    ' <AutoComplete(True)> _
    Public Function Consulta(ByVal CodOficina As String, ByVal pvTerminalID As String, _
                             ByVal pvTrace As String, _
                             ByVal pvCanal As String, _
                             ByVal pvTelefono As String, _
                             ByVal pvBinAdquiriente As String, _
                             ByVal pvCodCadena As String, _
                             ByVal pvCodComercio As String) _
                             As String
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Implementado Por  : Richard Carlo Rodriguez Balcazar
        ' Fecha Creación    : 21/07/2004
        ' Proposito         : Consulta de Saldo de celular
        '                     Esta transacción permite consultar al sistema Prepago de TIM
        '                     la disponibilidad de un celular para recibir una recarga.
        ' Modificado Por    : JYMMY TORRES
        ' Fecha Modificación: 2013.07.17
        ' Modificado Por    : xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        ' Fecha Modificación: xxxx.xx.xx
        ' Output            : Error, OrigenRpta, CodigoRpta, DescripcionRpta,
        '                     SaldoResultante, MonedaSaldo
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim lError As eNvTIMPPVError
        Dim lOrigenRpta As Long
        Dim lCodigoRpta As Long
        Dim lDescripcionRpta As String
        Dim lSaldoResultante As Double
        Dim lMonedaSaldo As Long
        Dim objComponente

        Dim wsSwichTransaccional As New ServiciosClaro
        Dim consultaRecarga As New ConsultaRecargaRequest
        Dim consultaRecargaResponse As New ConsultaRecargaResponse

        lError = eNvTIMPPVError.OperacionExitosa   ' Por defecto se asume la operacion como exitosa

        '     On Error GoTo HandlerErrorApp
        Try
            '//PBI000002202042 INI SE QUITO VALIDACION

                    With consultaRecarga
                        .binAdquiriente = System.Configuration.ConfigurationSettings.AppSettings("BIN_ADQUIRIENTE")

                        .binAdquirienteReenvia = System.Configuration.ConfigurationSettings.AppSettings("CONST_BIN_REENVIO")
                        .canal = pvCanal
                        '.codigoMoneda = pvMoneda
                        .fechaCaptura = Date.Now.ToString("yyyy-MM-dd-05:00")
                        .fechaTransaccion = Date.Now.ToString("yyyy-MM-ddTHH:mm:ss.ms-05:00")
                        .fechaCapturaSpecified = True
                        .fechaTransaccionSpecified = True
                        '.numeroComercio = Left(Trim(pvCodComercio), 15)
                        .numeroTerminal = "PVU"
                        '.codigoCadena = "001"
                        '.codigoFormato = "2"
                        .numeroComercio = pvCodComercio

                        .nombreComercio = System.Configuration.ConfigurationSettings.AppSettings("CONST_NOMBRE_COMERCIO")
                        .numeroTelefono = FormatoTelefono(Trim(pvTelefono))
                        .procesador = System.Configuration.ConfigurationSettings.AppSettings("CONST_PROCESADOR")
                        .telco = System.Configuration.ConfigurationSettings.AppSettings("CONST_TELCO")
                        .producto = "1"
                        .trace = pvTrace
                        .codigoCadena = System.Configuration.ConfigurationSettings.AppSettings("CODIGO_CADENA")
                        .numeroReferencia = System.Configuration.ConfigurationSettings.AppSettings("CONST_NUMEROREFERENCIA")
                    End With
                    consultaRecargaResponse = wsSwichTransaccional.consultaSaldo(consultaRecarga)
                    With consultaRecargaResponse
                        lOrigenRpta = .codigoOrigenRespuesta
                        lCodigoRpta = .codigoRespuesta
                        lDescripcionRpta = .descripcionExtendidaRespuesta

                        If lError = eNvTIMPPVError.OperacionExitosa Then
                            lSaldoResultante = .minutosSaldo
                            lMonedaSaldo = .montoSaldo
                            'Call SP_comSetComplete()
                            'GoTo Fin
                            'Else
                            '   GoTo HandlerErrorComponente
                        End If
                    End With

            '//PBI000002202042 FIN SE QUITO VALIDACION

        Catch Ex As Exception
            'HandlerErrorApp:
            ' Controlar y devolver error del aplicativo
            lError = eNvTIMPPVError.ErrorAplicativo
            lOrigenRpta = NvTIMPPVCaller
            lCodigoRpta = Err.Number
            lDescripcionRpta = Err.Description

            'HandlerErrorComponente:
            '  Call SP_comSetAbort()

            'Fin:
        Finally
            objComponente = Nothing

            ' Valores devueltos por el metodo
            Consulta = Trim(CStr(lError)) & ";" & Trim(CStr(lOrigenRpta)) & ";" & _
                       Trim(CStr(lCodigoRpta)) & ";" & Trim(CStr(lDescripcionRpta)) & ";" & _
                       Trim(CStr(lSaldoResultante)) & ";" & Trim(CStr(lMonedaSaldo))


        End Try

        'Try
        '    If Left(Trim(CodOficina), 1) = "S" Then
        '        CodOficina = "0"
        '    Else
        '        CodOficina = "1"
        '    End If
        '    Select Case CodOficina
        '        Case "1"
        '            ' Inicio logica de la operacion
        '            objComponente = CreateObject(NvTIMPPVComponentName)
        '            With objComponente
        '                .TerminalID = Left(Trim(pvTerminalID), 8)
        '                .Trace = pvTrace
        '                .Canal = pvCanal
        '                '.Telefono = Left(Trim(pvTelefono), 11) 'modificado JCR
        '                .Telefono = FormatoTelefono(Trim(pvTelefono))
        '                .BinAdquiriente = Left(Trim(pvBinAdquiriente), 6)
        '                .CodCadena = Left(Trim(pvCodCadena), 15)
        '                .CodComercio = Left(Trim(pvCodComercio), 15)

        '                lError = .Consulta

        '                lOrigenRpta = .OrigenRpta
        '                lCodigoRpta = .CodigoRpta
        '                lDescripcionRpta = .DescripcionRpta

        '                If lError = eNvTIMPPVError.OperacionExitosa Then
        '                    lSaldoResultante = .SaldoResultante
        '                    lMonedaSaldo = .MonedaSaldo
        '                    'Call SP_comSetComplete()
        '                    'GoTo Fin
        '                    'Else
        '                    '   GoTo HandlerErrorComponente
        '                End If
        '            End With
        '            ' Fin logica de la operacion
        '        Case Else
        '            Consulta = "0;2;0;RESPUESTA OK;;;;;;;;;;;"
        '    End Select
        '    'Exit Function
        'Catch Ex As Exception
        '    'HandlerErrorApp:
        '    ' Controlar y devolver error del aplicativo
        '    lError = eNvTIMPPVError.ErrorAplicativo
        '    lOrigenRpta = NvTIMPPVCaller
        '    lCodigoRpta = Err.Number
        '    lDescripcionRpta = Err.Description

        '    'HandlerErrorComponente:
        '    '  Call SP_comSetAbort()

        '    'Fin:
        'Finally
        '    objComponente = Nothing

        '    ' Valores devueltos por el metodo
        '    Consulta = Trim(CStr(lError)) & ";" & Trim(CStr(lOrigenRpta)) & ";" & _
        '               Trim(CStr(lCodigoRpta)) & ";" & Trim(CStr(lDescripcionRpta)) & ";" & _
        '               Trim(CStr(lSaldoResultante)) & ";" & Trim(CStr(lMonedaSaldo))
        'End Try
    End Function

    '------------------------------------------------------------------------------------------------------------------'
    '<AutoComplete(True)> _
    Public Function Anulacion(ByVal CodOficina As String, ByVal pvTerminalID As String, _
                              ByVal pvTrace As String, _
                              ByVal pvCanal As String, _
                              ByVal pvTelefono As String, _
                              ByVal pvMonto As String, _
                              ByVal pvMoneda As String, _
                              ByVal pvProducto As String, _
                              ByVal pvBinAdquiriente As String, _
                              ByVal pvCodCadena As String, _
                              ByVal pvCodComercio As String, _
                              ByVal pvNumeroRecargaOriginal As String, _
                              ByVal fechaTransaccionOriginal As String, _
                              Optional ByVal pvRUC As String = "", _
                              Optional ByVal pvNombreCliente As String = "") _
                              As String
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Implementado Por  : Richard Carlo Rodriguez Balcazar
        ' Fecha Creación    : 21/07/2004
        ' Proposito         : Anulacion de recarga. La anulacion se hace con el numero
        '                     de recarga original. Si el cliente no conoce el numero de
        '                     recarga, salvo que la aplicacion PVU tenga una base de
        '                     datos donde pueda encontrar la recarga original no se
        '                     podra realizar la anulacion
        '                     Esta transacción permite extornar la recarga efectuada.
        '                     Para que la anulación sea aprobada debe cumplir con
        '                     las siguientes condiciones
        '                     (estas condiciones son validadas por el switch):
        '                     - La anulación deberá efectuarse el mismo día que se realizó la compra.
        '                     - La anulación será procesada hasta 5 horas después de efectuada la recarga.
        '                     - La anulación será aprobada si el celular cuenta con el importe total recargado
        ' Modificado Por    : xxxxxxxxxxxxxxxxxxxxxxx
        ' Fecha Modificación: xxxxxxxxxxxxxxxxxxxxxxx
        ' Output            : Error, OrigenRpta, CodigoRpta, DescripcionRpta,
        '                     SaldoResultante, MonedaSaldo, NumeroRecarga,
        '                     NumeroDocumentoAutorizadoAnulado
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim lError As eNvTIMPPVError
        Dim lOrigenRpta As Long 'eNvTIMPPVOrigenRpta
        Dim lCodigoRpta As Long
        Dim lDescripcionRpta As String
        Dim lSaldoResultante As Double
        Dim lMonedaSaldo As Long
        Dim lNumeroRecarga As Long
        Dim lNumeroDocumentoAutorizado As String
        Dim objComponente

        lError = eNvTIMPPVError.OperacionExitosa   ' Por defecto se asume la operacion como exitosa


        Dim wsSwichtransaccional As New ServiciosClaro
        Dim anulacionRecarga As New AnulacionRecargaRequest
        Dim anulacionRecargaResponse As New AnulacionRecargaResponse

        Try
            With anulacionRecarga

                .numeroRecargaOriginal = pvNumeroRecargaOriginal
                .binAdquiriente = System.Configuration.ConfigurationSettings.AppSettings("BIN_ADQUIRIENTE")
                .binAdquirienteReenvia = System.Configuration.ConfigurationSettings.AppSettings("CONST_BIN_REENVIO")
                .canal = pvCanal '91
                .codigoMoneda = pvMoneda '604
                .fechaCaptura = Date.Now.ToString("yyyy-MM-dd-05:00")
                .fechaTransaccion = Date.Now.ToString("yyyy-MM-ddTHH:mm:ss.ms-05:00")
                .fechaCapturaSpecified = True
                .fechaTransaccionSpecified = True
                .nombreComercio = "SICAR"
                .numeroComercio = Left(Trim(pvCodComercio), 15) 'session("almacen")
                .numeroTerminal = Left(Trim(pvTerminalID), 8) 'PVU
                .binAdquirienteOriginal = .binAdquiriente
                .binAdquirienteReenvioOriginal = .binAdquirienteReenvia
                .codigoCadena = System.Configuration.ConfigurationSettings.AppSettings("CODIGO_CADENA")
                .codigoFormato = System.Configuration.ConfigurationSettings.AppSettings("COD_FORMATO_RV")
                .fechaTransaccionOriginal = fechaTransaccionOriginal
                .fechaTransaccionOriginalSpecified = True
                .indicadorNumDocAutGenPorAdq = False
                .indicadorNumDocAutGenPorAdqSpecified = True
                .montoRecarga = pvMonto
                .montoRecargaSpecified = True
                .nombreComprador = ""
                .numeroDocumentoComprador = ""
                .numeroTelefono = FormatoTelefono(Trim(pvTelefono))
                .procesador = System.Configuration.ConfigurationSettings.AppSettings("CONST_PROCESADOR")
                .producto = System.Configuration.ConfigurationSettings.AppSettings("COD_PRODUCTO_RV")
                .telco = System.Configuration.ConfigurationSettings.AppSettings("CONST_TELCO")
                .numDocAutorizado = ""
                .tipoDocumentoComprador = ""
                .traceOriginal = pvTrace
            End With
            anulacionRecargaResponse = wsSwichtransaccional.anulacionRecarga(anulacionRecarga)
            With anulacionRecargaResponse
                lCodigoRpta = .codigoRespuesta
                lDescripcionRpta = .descripcionRespuesta

                If lError = eNvTIMPPVError.OperacionExitosa Then
                    lSaldoResultante = .montoSaldo
                    lMonedaSaldo = .codigoMonedaSaldo
                    lNumeroRecarga = .numeroRecarga
                    lNumeroDocumentoAutorizado = .numeroDocumentoAutorizado
                    'Call SP_comSetComplete()
                    'GoTo Fin
                    'Else
                    '   GoTo HandlerErrorComponente
                End If
            End With
        Catch ex As Exception
            'HandlerErrorApp:
            ' Controlar y devolver error del aplicativo
            lError = eNvTIMPPVError.ErrorAplicativo
            lOrigenRpta = NvTIMPPVCaller
            lCodigoRpta = Err.Number
            lDescripcionRpta = Err.Description

            'HandlerErrorComponente:
            'Call SP_comSetAbort()

            'Fin:
        Finally
            objComponente = Nothing

            ' Valores devueltos por el metodo
            Anulacion = Trim(CStr(lError)) & ";" & Trim(CStr(lOrigenRpta)) & ";" & _
                        Trim(CStr(lCodigoRpta)) & ";" & Trim(CStr(lDescripcionRpta)) & ";" & _
                        Trim(CStr(lSaldoResultante)) & ";" & Trim(CStr(lMonedaSaldo)) & ";" & _
                        Trim(CStr(lNumeroRecarga)) & ";" & Trim(CStr(lNumeroDocumentoAutorizado))
        End Try

        'On Error GoTo HandlerErrorApp
        'Try
        '    If Left(Trim(CodOficina), 1) = "S" Then
        '        CodOficina = "0"
        '    Else
        '        CodOficina = "1"
        '    End If
        '    Select Case CodOficina
        '        Case "1"
        '            ' Inicio logica de la operacion
        '            objComponente = CreateObject(NvTIMPPVComponentName)
        '            With objComponente
        '                .TerminalID = Left(Trim(pvTerminalID), 8)
        '                .Trace = pvTrace
        '                .Canal = pvCanal
        '                .Telefono = Left(Trim(pvTelefono), 11) 'modificado JCR
        '                .Telefono = FormatoTelefono(Trim(pvTelefono))
        '                .Monto = pvMonto
        '                .Moneda = pvMoneda
        '                .Producto = pvProducto
        '                .BinAdquiriente = Left(Trim(pvBinAdquiriente), 6)
        '                .CodCadena = Left(Trim(pvCodCadena), 15)
        '                .CodComercio = Left(Trim(pvCodComercio), 15)
        '                .NumeroRecargaOriginal = pvNumeroRecargaOriginal
        '                .Ruc = Left(Trim(pvRUC), 14)
        '                .NombreCliente = Left(Trim(pvNombreCliente), 50)

        '                lError = .Anulacion

        '                lOrigenRpta = .OrigenRpta
        '                lCodigoRpta = .CodigoRpta
        '                lDescripcionRpta = .DescripcionRpta

        '                If lError = eNvTIMPPVError.OperacionExitosa Then
        '                    lSaldoResultante = .SaldoResultante
        '                    lMonedaSaldo = .MonedaSaldo
        '                    lNumeroRecarga = .NumeroRecarga
        '                    lNumeroDocumentoAutorizado = .NumeroDocumentoAutorizado
        '                    'Call SP_comSetComplete()
        '                    'GoTo Fin
        '                    'Else
        '                    '   GoTo HandlerErrorComponente
        '                End If
        '            End With
        '            ' Fin logica de la operacion
        '        Case Else
        '            Anulacion = "0;2;0;RESPUESTA OK;;;;;;;;;;;"
        '    End Select
        '    'Exit Function
        'Catch ex As Exception
        '    'HandlerErrorApp:
        '    ' Controlar y devolver error del aplicativo
        '    lError = eNvTIMPPVError.ErrorAplicativo
        '    lOrigenRpta = NvTIMPPVCaller
        '    lCodigoRpta = Err.Number
        '    lDescripcionRpta = Err.Description

        '    'HandlerErrorComponente:
        '    'Call SP_comSetAbort()

        '    'Fin:
        'Finally
        '    objComponente = Nothing

        '    ' Valores devueltos por el metodo
        '    Anulacion = Trim(CStr(lError)) & ";" & Trim(CStr(lOrigenRpta)) & ";" & _
        '                Trim(CStr(lCodigoRpta)) & ";" & Trim(CStr(lDescripcionRpta)) & ";" & _
        '                Trim(CStr(lSaldoResultante)) & ";" & Trim(CStr(lMonedaSaldo)) & ";" & _
        '                Trim(CStr(lNumeroRecarga)) & ";" & Trim(CStr(lNumeroDocumentoAutorizado))
        'End Try
    End Function

    Public Function FormatoTelefono(ByVal pvTelefono As String) _
                                   As String
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Implementado Por  : Pedro Marcos / Jose Crisostomo
        ' Fecha Creación    : 03/04/2007
        ' Proposito         : Devuelve el numero telefonico con formato nacional para la recarga
        ' Modificado Por    : xxxxxxxxxxxxxxxxxxxxxxx
        ' Fecha Modificación: xxxxxxxxxxxxxxxxxxxxxxx
        ' Output            : numero telefono
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim aux As String
        aux = pvTelefono

        If aux <> "" Then
            Dim longitud
            Dim posicion
            longitud = Len(pvTelefono)
            If longitud > 0 Then
                'posicion = 1
                Do While InStr(1, aux, "0") = 1
                    aux = Mid(aux, 2, Len(aux))
                Loop
            End If

            If InStr(1, aux, "1") = 1 Then 'Si es lima adicionar 0 adelante
                aux = "0" & aux
            End If

        End If

        If aux = "" Then
            FormatoTelefono = pvTelefono
        Else
            FormatoTelefono = aux
        End If

    End Function

End Class
