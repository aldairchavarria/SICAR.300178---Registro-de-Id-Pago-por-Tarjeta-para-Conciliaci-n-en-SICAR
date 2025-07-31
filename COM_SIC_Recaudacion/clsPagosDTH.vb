Imports SwichTransaccional.Services
Imports System.Configuration

Public Class clsPagosDTH
    Public gstrTracePago As String
    Dim oSwichTransaccional As New ServiciosClaro

    Public Sub New()
        oSwichTransaccional.Url = CStr(ConfigurationSettings.AppSettings("ServiciosClaroURL"))
        oSwichTransaccional.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim iTimeOut As Int32 = 0
        Dim strTimeOut As String = ConfigurationSettings.AppSettings("constSICARPagosST_Timeout")
        If Not strTimeOut Is Nothing Then
            If strTimeOut <> "" Then
                iTimeOut = Convert.ToInt32(strTimeOut)
            End If
        End If
        oSwichTransaccional.Timeout = iTimeOut
    End Sub
    Public Function Pagar(ByVal strLogSET As String, _
                            ByVal strNivelLogSet As String, _
                            ByVal strCodigoPuntoVenta As String, _
                            ByVal strCanal As String, _
                            ByVal strBinAdquiriente As String, _
                            ByVal strCodComercio As String, _
                            ByVal strCodigoCajero As String, _
                            ByVal strTipoIdentificador As String, _
                            ByVal strNumeroIdentificador As String, _
                            ByVal strTramaFormasPago As String, _
                            ByVal dblMontoTotalPagar As Double, _
                            ByVal strRecibosPagar As String, _
                            Optional ByVal strCodigoPlazaRecaudador As String = "", _
                            Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                            Optional ByVal strCodigoCiudadRecaudador As String = "", _
                            Optional ByRef strNumeroOperacionCobranza As String = "", _
                            Optional ByRef strDocumentos As String = "", _
                            Optional ByRef numeroOperacionPago As Integer = 0) As String

        Dim cadResultado As String = PagarRecibos( _
                                                    strLogSET, _
                                                    strNivelLogSet, _
                                                    strCodigoPuntoVenta, _
                                                    strCanal, _
                                                    strBinAdquiriente, _
                                                    strCodComercio, _
                                                    strCodigoCajero, _
                                                    strTipoIdentificador, _
                                                    strNumeroIdentificador, _
                                                    strTramaFormasPago, _
                                                    dblMontoTotalPagar, _
                                                    strRecibosPagar, _
                                                    strCodigoPlazaRecaudador, _
                                                    strCodigoAgenciaRecaudador, _
                                                    strCodigoCiudadRecaudador, _
                                                    strNumeroOperacionCobranza, _
                                                    strDocumentos, _
                                                    numeroOperacionPago)
        Return cadResultado
    End Function

    Private Function PagarRecibos(ByVal strLogSET As String, _
                                    ByVal strNivelLogSet As String, _
                                    ByVal strCodigoPuntoVenta As String, _
                                    ByVal strCanal As String, _
                                    ByVal strBinAdquiriente As String, _
                                    ByVal strCodComercio As String, _
                                    ByVal strCodigoCajero As String, _
                                    ByVal strTipoIdentificador As String, _
                                    ByVal strNumeroIdentificador As String, _
                                    ByVal strTramaFormasPago As String, _
                                    ByVal dblMontoTotalPagar As Double, _
                                    ByVal strRecibosPagar As String, _
                                    Optional ByVal strCodigoPlazaRecaudador As String = "", _
                                    Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                                    Optional ByVal strCodigoCiudadRecaudador As String = "", _
                                    Optional ByRef strNumeroOperacionCobranza As String = "", _
                                    Optional ByRef strDocumentos As String = "", _
                                    Optional ByRef numeroOperacionPago As Integer = 0) As String

        Dim arrDocs
        Dim arrLinDoc
        Dim intNumDocs As Integer

        Dim arrFormasPago
        Dim arrLinFormasPago
        Dim intNumFormasPago As Integer

        Dim strTrace As String
        Dim dblMontoPagar As Decimal
        Dim dblMontoRestante As Decimal
        Dim strResultado As String

        Dim strRespuestaConsulta As String
        Dim strOrigenRpta As String
        Dim strCodigoRpta As String
        Dim strDescripcionRpta As String
        Dim strNombreDeudor As String
        Dim strRucDeudor As String
        'Dim strNumeroOperacionCobranza As String
        Dim strNumeroOperacionAcreedor As String
        Dim dblValorTotal As Double
        Dim intNumeroDocumentos As Integer
        Dim strFechaHoraTransaccion As String

        Dim strMoneda As String
        Dim strServicio As String
        Dim strDesServicio As String
        Dim strTipoDocumento As String
        Dim strNumeroDocumento As String
        Dim dblImporteTotalPagado As Double

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        'Dim intSAP = objOffline.Get_ConsultaSAP
        'Dim objSAP As Object

        'If intSAP = 1 Then
        '    objSAP = New SAP_SIC_Recaudacion.clsRecaudacion
        'Else
        '    objSAP = New COM_SIC_OffLine.clsOffline
        'End If

        Dim arrSigDoc
        Dim dblMontoPAC As Double
        '''TODO: AÑADIDO POR JYMMY TORRES
        Dim montoDeuda As Double
        '''AÑADIDO HASTA AQUI

        '  Try
        dblMontoRestante = dblMontoTotalPagar    'dblMontoRestante = 550
        If Trim(strRecibosPagar) <> "" Then
            arrDocs = Split(strRecibosPagar, "|")
            For intNumDocs = 0 To UBound(arrDocs)
                arrLinDoc = Split(arrDocs(intNumDocs), ";")
                If intNumDocs = UBound(arrDocs) Then
                    dblMontoPagar = dblMontoRestante
                    montoDeuda = CDbl(arrLinDoc(7))
                Else
                    dblMontoPagar = CDbl(Mid(arrLinDoc(7), 1, Len(arrLinDoc(7)) - 2) & "." & Right(arrLinDoc(7), 2)) 'dblMontoPagar = 149.53
                    If dblMontoRestante < dblMontoPagar Then   ' 455.61 < 295.79 
                        dblMontoPagar = dblMontoRestante
                    End If
                End If

                If dblMontoRestante = 0 Then
                    Exit For
                End If

                'dblMontoRestante = Decimal.Round((dblMontoRestante - dblMontoPagar), 2)   'dblMontoRestante = 0.47

                ' Saltar PAC si el restante no alcanza para cubrirlo
                '  If Not (Trim(Left(arrLinDoc(4), 3)) = "PAC" And dblMontoRestante < dblMontoPagar) Then
                ' Validacion si se presenta un PAC como siguiente documento y no alcanzaria
                If (intNumDocs + 1) <= UBound(arrDocs) Then
                    arrSigDoc = Split(arrDocs(intNumDocs + 1), ";")
                    If Left(arrSigDoc(4), 3) = "PAC" Then
                        dblMontoPAC = CDbl(Mid(arrSigDoc(7), 1, Len(arrSigDoc(7)) - 2) & "." & Right(arrSigDoc(7), 2))
                        If dblMontoPAC > Decimal.Round((dblMontoRestante - dblMontoPagar), 2) Then
                            dblMontoPagar += Decimal.Round((dblMontoRestante - dblMontoPagar), 2)
                        End If
                    End If
                End If
                'fin de Validacion
                dblMontoRestante = Decimal.Round((montoDeuda - dblMontoPagar), 2)   'dblMontoRestante = 0.47

                strServicio = Trim(arrLinDoc(0))
                strDesServicio = Trim(arrLinDoc(1))
                strMoneda = arrLinDoc(2)
                strTipoDocumento = arrLinDoc(3)
                strNumeroDocumento = arrLinDoc(4)
                If PagarReciboST(strLogSET, _
                                    strNivelLogSet, _
                                    strCodigoPuntoVenta, _
                                    strCanal, _
                                    strBinAdquiriente, _
                                    strCodComercio, _
                                    strCodigoCajero, _
                                    strTipoIdentificador, _
                                    strNumeroIdentificador, _
                                    dblMontoPagar, _
                                    strMoneda, _
                                    strServicio, _
                                    strTipoDocumento, _
                                    strNumeroDocumento, _
                                    strRespuestaConsulta, _
                                    strOrigenRpta, _
                                    strCodigoRpta, _
                                    strDescripcionRpta, _
                                    strNombreDeudor, _
                                    strRucDeudor, _
                                    strNumeroOperacionCobranza, _
                                    strNumeroOperacionAcreedor, _
                                    dblValorTotal, _
                                    intNumeroDocumentos, _
                                    strDocumentos, _
                                    strFechaHoraTransaccion, _
                                    strCodigoPlazaRecaudador, _
                                    strCodigoAgenciaRecaudador, _
                                    strCodigoCiudadRecaudador, _
                                    numeroOperacionPago) Then

                End If
            Next
        End If
        strResultado = strRespuestaConsulta & "@" & strDescripcionRpta
        PagarRecibos = strResultado
    End Function

    Private Function PagarReciboST(ByVal strLogSET As String, _
                                    ByVal strNivelLogSet As String, _
                                    ByVal strCodigoPuntoVenta As String, _
                                    ByVal strCanal As String, _
                                    ByVal strBinAdquiriente As String, _
                                    ByVal strCodComercio As String, _
                                    ByVal strCodigoCajero As String, _
                                    ByVal strTipoIdentificador As String, _
                                    ByVal strNumeroIdentificador As String, _
                                    ByVal dblMontoPagar As Double, _
                                    ByVal strMoneda As String, _
                                    ByVal strServicio As String, _
                                    ByVal strTipoDocumento As String, _
                                    ByVal strNumeroDocumento As String, _
                                    ByRef strRespuestaConsulta As String, _
                                    ByRef strOrigenRpta As String, _
                                    ByRef strCodigoRpta As String, _
                                    ByRef strDescripcionRpta As String, _
                                    ByRef strNombreDeudor As String, _
                                    ByRef strRucDeudor As String, _
                                    ByRef strNumeroOperacionCobranza As String, _
                                    ByRef strNumeroOperacionAcreedor As String, _
                                    ByRef dblValorTotal As Double, _
                                    ByRef intNumeroDocumentos As Integer, _
                                    ByRef strDocumentos As String, _
                                    ByRef strFechaHoraTransaccion As String, _
                                    Optional ByVal strCodigoPlazaRecaudador As String = "", _
                                    Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                                    Optional ByVal strCodigoCiudadRecaudador As String = "", _
                                    Optional ByRef numeroOperacionPago As Integer = 0, _
                                    Optional ByRef strFechaHoraTransac As String = "") As Boolean

        'Dim objComponente
        Dim strTrace As String
        Dim strRpta As String
        Dim strDocsAux As String

        Dim strCodServicio As String
        Dim strDesServicio As String
        Dim strMonServicio As String
        Dim strTipoDoc As String
        Dim strNumDoc As String
        Dim strFechaEmi As String
        Dim strFechaVen As String
        Dim strMontoDoc As String
        Dim strLinDocs As String = ""

        Dim intNumDocs As Integer
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim intSAP = objOffline.Get_ConsultaSAP
        '''CAMBIADO POR JTN
        Dim clsSap As New COM_SIC_OffLine.clsOffline

        'If intSAP = 1 Then
        '    clsSap = New SAP_SIC_Recaudacion.clsRecaudacion
        'Else
        '    clsSap = New COM_SIC_OffLine.clsOffline
        'End If
        'CAMBIADO POR JTN
        Dim strIngreso As String

        Dim lngLog As Long
        Dim strLinDocsLog As String = ""
        Dim strDocumentosLog As String = ""

        Dim strFecha As String
        Dim strHora As String
        Dim strDescripcionRptaAux As String

        Dim pagoRequest As New PagoRequest
        Dim pagoResponse As New PagoResponse
        Try
            'objComponente = CreateObject("OLCPVUPagos.clsOLCPVUPagos")
            'OBTENER NRO TRACE DE SAP
            'lngLog = objComponente.SetEnv(strLogSET, strNivelLogSet)
            strIngreso = ";" & strCodigoPuntoVenta & ";" & strCanal & ";" & strTipoIdentificador & ";" & strNumeroIdentificador & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";;" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero
            strTrace = clsSap.SetLogRecaudacion(strIngreso, "") '''CAMBIADO POR JTN
            strTrace = Right(strTrace, 6)
            strTrace = "0000" & strTrace
            gstrTracePago = strTrace

            'Dim wsPago As New ServiciosClaro
            Dim productos(0) As ProductoServicioAPagar
            Dim documentos(0) As DocumentoAPagar

            documentos(0) = New DocumentoAPagar
            productos(0) = New ProductoServicioAPagar

            With documentos(0)
                .importeAPagar = String.Format("{0:N}", dblMontoPagar)
                .importeAPagarSpecified = True
                .importeDeudaOriginal = String.Format("{0:N}", 0)
                .importeDeudaOriginalSpecified = True
                .numeroDocumento = strNumeroDocumento
                .tipoDocumento = strServicio
            End With

            With productos(0)
                .codigo = strServicio
                .documentos = documentos
                .montoTotalAPagar = String.Format("{0:N}", dblMontoPagar)
                .montoTotalAPagarSpecified = True
                .numeroTotalDocumentosAPagar = 1
                .numeroTotalDocumentosAPagarSpecified = True
            End With

            With pagoRequest
                .binAdquiriente = strBinAdquiriente
                .binAdquirienteReenvia = ConfigurationSettings.AppSettings("CONST_BIN_REENVIO")
                .canal = strCanal
                .codigoMoneda = strMoneda
                .fechaCaptura = Date.Now.ToString("yyyy-MM-dd-05:00")
                .fechaCapturaSpecified = True
                .fechaTransaccion = Date.Now.ToString("yyyy-MM-ddTHH:mm:ss.ms-05:00")
                .fechaTransaccionSpecified = True
                .numeroReferencia = ConfigurationSettings.AppSettings("CONST_NUMEROREFERENCIA")
                .numeroTerminal = strCodigoCajero
                .trace = strTrace
                .acreedor = ConfigurationSettings.AppSettings("CONST_ACREEDOR")
                .codigoMoneda = strMoneda
                .importePago = String.Format("{0:N}", dblMontoPagar)
                .importePagoSpecified = True
                .importePagoEfectivo = String.Format("{0:N}", dblMontoPagar)
                .importePagoEfectivoSpecified = True
                .numeroIdentificacionDeudor = strNumeroIdentificador
                .pagoTotal = String.Format("{0:N}", dblMontoPagar)
                .pagoTotalSpecified = True
                .productos = productos
                .tipoIdentificacionDeudor = ConfigurationSettings.AppSettings("CONST_CODIGO_FORMATO")
            End With

            pagoResponse = oSwichTransaccional.pago(pagoRequest)


            With pagoResponse
                strRpta = .codigoRespuesta
                strRespuestaConsulta = strRpta
                strOrigenRpta = .codigoOrigenRespuesta
                strCodigoRpta = .codigoRespuesta
                strDescripcionRpta = .descripcionExtendidaRespuesta

                numeroOperacionPago = .numeroOperacionCobranza

                Select Case Trim(strRpta)
                    Case "00"
                        strNombreDeudor = .nombreDeudor
                        strRucDeudor = .rucDeudor
                        strNumeroOperacionCobranza = .numeroOperacionCobranza
                        strNumeroOperacionAcreedor = .numeroOperacionAcreedor
                        dblValorTotal = .importePago
                        intNumeroDocumentos = .productos(0).numeroDocumentosPagados
                        strFechaHoraTransaccion = .fechaTransaccion
                        strFechaHoraTransac = .fechaTransaccion

                        strDocumentos = .datosTransaccion

                        For intNumDocs = 0 To CInt(intNumeroDocumentos) - 1
                            strDocsAux = Mid(strDocumentos, (67 * intNumDocs) + 1, 67)
                            strCodServicio = Trim(Mid(strDocsAux, 1, 3))
                            strDesServicio = Trim(Mid(strDocsAux, 4, 15))
                            strMonServicio = Trim(Mid(strDocsAux, 19, 3))
                            strTipoDoc = Trim(Mid(strDocsAux, 22, 3))
                            strNumDoc = Trim(Mid(strDocsAux, 25, 16))
                            strFechaEmi = Trim(Mid(strDocsAux, 41, 8))
                            strFechaVen = Trim(Mid(strDocsAux, 49, 8))
                            strMontoDoc = Trim(Mid(strDocsAux, 57, 11))
                            If Trim(strMontoDoc) = "" Then
                                strMontoDoc = "0.00"
                            Else
                                strMontoDoc = CDbl(strMontoDoc)
                            End If
                            strLinDocs = strLinDocs & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                            strLinDocsLog = strLinDocsLog & strTrace & ";" & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                        Next

                        strDocumentos = Mid(strLinDocs, 1, Len(strLinDocs) - 1)
                        strDocumentosLog = Mid(strLinDocsLog, 1, Len(strLinDocsLog) - 1)
                        If Trim(strFechaHoraTransaccion) <> "" Then
                            strFecha = Mid(strFechaHoraTransaccion, 3, 2) & "/" & Mid(strFechaHoraTransaccion, 1, 2) & "/" & Year(Now)
                            strHora = Mid(strFechaHoraTransaccion, 5, 2) & ":" & Mid(strFechaHoraTransaccion, 7, 2) & ":" & Mid(strFechaHoraTransaccion, 9, 2)
                        End If

                        strIngreso = strTrace & ";" & strCodigoPuntoVenta & ";" & strCanal & ";" & strTipoIdentificador & ";" & strNumeroIdentificador & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal
                        strTrace = clsSap.SetLogRecaudacion(strIngreso, strDocumentosLog) '''CAMBIADO POR JTN

                        PagarReciboST = True
                    Case Else
                        strNombreDeudor = ""
                        strRucDeudor = ""
                        dblValorTotal = 0
                        strNumeroOperacionCobranza = ""
                        intNumeroDocumentos = 0
                        strDocumentos = ""
                        strFechaHoraTransaccion = ""
                        PagarReciboST = False

                        strIngreso = strTrace & ";" & strCodigoPuntoVenta & ";" & strCanal & ";" & strTipoIdentificador & ";" & strNumeroIdentificador & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal
                        strTrace = clsSap.SetLogRecaudacion(strIngreso, strDocumentosLog) '''CAMBIADO POR JTN

                        If strOrigenRpta = 3 Then
                            strDescripcionRptaAux = BuscarMensaje(CInt(strCodigoRpta))
                            If Trim(strDescripcionRptaAux) <> "" Then
                                strDescripcionRpta = strDescripcionRptaAux
                            Else
                                strDescripcionRpta = BuscarMensaje(8)
                            End If
                        Else
                            ' MOSTRARA MESAJE DE ERROR GNERAL PARA LOS DEMAS CASOS DE CODIGO DE ORIGEN DIFERENTE DE 3
                            'strDescripcionRpta = BuscarMensaje(8)'COMENTADO POR TS.JTN
                            strDescripcionRpta = strDescripcionRpta
                        End If
                End Select
            End With
            clsSap = Nothing
        Catch ex As Exception
            strRespuestaConsulta = ""
            strDescripcionRpta = BuscarMensaje(9)
            PagarReciboST = False
            clsSap = Nothing
            pagoRequest = Nothing
            pagoResponse = Nothing
            oSwichTransaccional.Dispose()
        Finally
            pagoRequest = Nothing
            pagoResponse = Nothing
            oSwichTransaccional.Dispose()
        End Try
    End Function

    Private Function BuscarMensaje(ByVal codErr As Integer) As String
        Dim strMensaje As String = ""
        Select Case codErr
            Case 2
                strMensaje = "No se encontraron documentos pendientes de pago"
            Case 3
                strMensaje = "Documento / Recibo a pagar no válido"
            Case 5
                strMensaje = "En este momento no podemos procesar su operación. Por favor comuníquese con Service Desk"
            Case 6
                strMensaje = "La anulación no se pudo realizar. Por favor comuníquese con Service Desk"
            Case 7
                strMensaje = "Número telefónico o factura no existe."
            Case 8
                strMensaje = "Verifique los datos ingresados. Por favor vuelva a intentar."
            Case 9
                strMensaje = "Ocurrió un error inesperado al realizar la consulta al componente OLC."
        End Select
        Return strMensaje
    End Function

End Class
