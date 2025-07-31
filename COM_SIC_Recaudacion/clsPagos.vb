Imports System.Configuration
Imports COM_SIC_Cajas
Imports System.Text
Imports SwichTransaccional.Services
Imports SwichTransaccional.DistribuidoresWS
Imports SwichTransaccional.TransaccionPagoDRAWS

Public Class clsPagos

    Public gstrTracePago As String
    Public gstrTraceAnulacion As String
    Public Const gstrCodTelefono = "01"
    Public Const gstrEstadoPago = 1
    Public Const gstrEstadoAnulado = 2

    '''AGREGADO POR JYMMYT

    Public Const TIPO_PAGO_EFECTIVO As String = "00"
    Public Const TIPO_PAGO_CHEQUE As String = "01"
    Public Const TIPO_PAGO_TARJETA As String = "02"
    Private Const TIPO_PAGO_TARJETADEBIO As String = "03"

    Public sbLineasLog As StringBuilder

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
	Public nameFileDac As String = ConfigurationSettings.AppSettings("constNameLogRecaudacionDAC")
    Public strArchivoDac As String = objFileLog.Log_CrearNombreArchivo(nameFileDac)
    'INI PROY-140126
    Public nameFileRfp As String = ConfigurationSettings.AppSettings("constNameLogRecFijoPaginas")
    Public strArchivoRfp As String = objFileLog.Log_CrearNombreArchivo(nameFileRfp)
    'FIN PROY-140126
    '//-- GB 05/2015
    Public nameFileDRA As String = ConfigurationSettings.AppSettings("constNameLogDRA")
    Public strArchivoDRA As String = objFileLog.Log_CrearNombreArchivo(nameFileDRA)
    '//--
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

    Public Function Pagar_DNI(ByVal strLogSET As String, _
                               ByVal strNivelLogSet As String, _
                               ByVal strCodigoPuntoDeVenta As String, _
                               ByVal nombrePuntoDeVenta As String, _
                               ByVal strCanal As String, _
                               ByVal strBinAdquiriente As String, _
                               ByVal strCodComercio As String, _
                               ByVal strCodigoCajero As String, _
                               ByVal nombreCajero As String, _
                               ByVal strTipoIdentificadorDeudor As String, _
                               ByVal strNumeroIdentificadorDeudor As String, _
                               ByVal strFormasPago As String, _
                               ByVal dblMontoTotalPagar As Double, _
                               ByVal strRecibosPagar As String, _
                               ByVal strTraceConsulta As String, _
                               ByVal strDocCliente As String, _
                               ByVal strTipoAlmacen As String, _
                               ByVal intCodAplicacion As String, _
                               ByVal decImpRecPen As Decimal, _
                               ByVal decImpRecUsd As Decimal, _
                               ByVal decVuelto As Decimal, _
                               ByVal numeroMac As String, _
                               ByVal codigoAcreedor As String, _
                               ByVal binAdquirienteRenvio As String, _
                               ByVal codigoFormato As String, _
                               ByVal nombreComercio As String, _
                               ByVal codigoPlazaRecaudador As String, _
                               ByVal medioPagoAuxiliar As String, _
                               ByVal codigoEstadoDeudor As String, _
                               ByVal codigoProcesador As String, _
                               Optional ByVal strCodigoPlazaRecaudador As String = "", _
                               Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                               Optional ByVal strCodigoCiudadRecaudador As String = "", Optional ByRef strFechaHoraTransac As String = "", _
                               Optional ByRef pstrNumeroOperacionCobranza As String = "", Optional ByRef pstrNumeroOperacionAcreedor As String = "", _
                               Optional ByVal strDeudaDni As Boolean = False) As String

        Dim strResultado As String = PagarRecibos_DNI(strLogSET, _
                                strNivelLogSet, _
                                strCodigoPuntoDeVenta, _
                                nombrePuntoDeVenta, _
                                strCanal, _
                                strBinAdquiriente, _
                                strCodComercio, _
                                strCodigoCajero, _
                                nombreCajero, _
                                strTipoIdentificadorDeudor, _
                                strNumeroIdentificadorDeudor, _
                                strFormasPago, _
                                dblMontoTotalPagar, _
                                strRecibosPagar, _
                                strTraceConsulta, _
                                strDocCliente, _
                                strTipoAlmacen, _
                                intCodAplicacion, _
                                decImpRecPen, _
                                decImpRecUsd, _
                                decVuelto, _
                                numeroMac, _
                                codigoAcreedor, _
                                binAdquirienteRenvio, _
                                codigoFormato, _
                                nombreComercio, _
                                codigoPlazaRecaudador, _
                                medioPagoAuxiliar, _
                                codigoEstadoDeudor, _
                                codigoProcesador, _
                                strCodigoPlazaRecaudador, _
                                strCodigoAgenciaRecaudador, _
                                strCodigoCiudadRecaudador, strFechaHoraTransac, pstrNumeroOperacionCobranza, pstrNumeroOperacionAcreedor, strDeudaDni)
        Return strResultado
    End Function

    Private Function PagarRecibos_DNI(ByVal strLogSET As String, _
                           ByVal strNivelLogSet As String, _
                           ByVal strCodigoPuntoDeVenta As String, _
                           ByVal nombrePuntoDeVenta As String, _
                           ByVal strCanal As String, _
                           ByVal strBinAdquiriente As String, _
                           ByVal strCodComercio As String, _
                           ByVal strCodigoCajero As String, _
                           ByVal nombreCajero As String, _
                           ByVal strTipoIdentificadorDeudor As String, _
                           ByVal strNumeroIdentificadorDeudor As String, _
                           ByVal strFormasPago As String, _
                           ByVal dblMontoTotalPagar As Double, _
                           ByVal strRecibosPagar As String, _
                           ByVal strTraceConsulta As String, _
                           ByVal strDocCliente As String, _
                           ByVal strTipoAlmacen As String, _
                           ByVal intCodAplicacion As String, _
                           ByVal decImpRecPen As Decimal, _
                           ByVal decImpRecUsd As Decimal, _
                           ByVal decVuelto As Decimal, _
                           ByVal numeroMac As String, _
                           ByVal codigoAcreedor As String, _
                           ByVal binAdquirienteRenvio As String, _
                           ByVal codigoFormato As String, _
                           ByVal nombreComercio As String, _
                           ByVal codigoPlazaRecaudador As String, _
                           ByVal medioPagoAuxiliar As String, _
                           ByVal codigoEstadoDeudor As String, _
                           ByVal codigoProcesador As String, _
                           Optional ByVal strCodigoPlazaRecaudador As String = "", _
                           Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                           Optional ByVal strCodigoCiudadRecaudador As String = "", Optional ByRef strFechaHoraTransac As String = "", _
                           Optional ByRef pstrNumeroOperacionCobranza As String = "", Optional ByRef pstrNumeroOperacionAcreedor As String = "", _
                           Optional ByVal strDeudaDni As Boolean = False) As String



        Dim strIdentifyLog As String = strTipoIdentificadorDeudor & "|" & strNumeroIdentificadorDeudor
        Dim arrDocs
        Dim arrLinDoc
        Dim intNumDocs As Integer

        Dim arrFormasPago
        Dim arrLinFormasPago
        Dim intNumFormasPago As Integer

        Dim strRespPagoMasivo As String
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
        Dim strNumeroOperacionCobranza As String
        Dim strNumeroOperacionAcreedor As String
        Dim dblValorTotal As Double
        Dim intNumeroDocumentos As Integer
        Dim strDocumentos As String
        Dim strFechaHoraTransaccion As String

        Dim strMoneda As String
        Dim strServicio As String
        Dim strDesServicio As String
        Dim strTipoDocumento As String
        Dim strNumeroDocumento As String
        Dim strImporteRecibo As String
        Dim dblImporteTotalPagado As Decimal

        Dim strCustomerId As String
        Dim strTipoServicio As String
        Dim strNroLinea As String

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        ''TODO: CAMBIADO POR JYMMY TORRES
        Dim objSAP As COM_SIC_OffLine.clsOffline
        objSAP = New COM_SIC_OffLine.clsOffline
        'End If
        ''CAMBIADO HASTA AQUI


        Dim listaAnulacion As New System.Collections.ArrayList
        Dim tramaAnulacion As String = String.Empty
        Dim itemAnulacion As ItemAnulacion

        Dim strNumeroTelefono As String
        Dim strNombreDeudorSAP As String
        Dim strRucDeudorSAP As String
        Dim strFechaEmision As String
        Dim strFechaPago As String
        Dim strValorResultado As String
        Dim strRecibosAnular As String
        Dim strAnularPago As String
        Dim clsAnular As New clsAnulaciones

        Dim gstrDeuda As String = ""
        Dim gstrRecibos As String = ""
        Dim gstrPagos As String = ""

        Dim arrSigDoc
        Dim dblMontoPAC As Double

        Try
            dblMontoRestante = dblMontoTotalPagar    'dblMontoRestante = 550

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio PagarRecibos")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	dblMontoRestante: " & dblMontoRestante)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strRecibosPagar: " & strRecibosPagar)

            If Trim(strRecibosPagar) <> "" Then
                arrDocs = Split(strRecibosPagar, "|")



                For intNumDocs = 0 To UBound(arrDocs)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	Inicio PagoDocumento Nro: " & intNumDocs.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

                    arrLinDoc = Split(arrDocs(intNumDocs), ";")
                    If intNumDocs = UBound(arrDocs) Then
                        dblMontoPagar = dblMontoRestante
                    Else
                        dblMontoPagar = CDbl(arrLinDoc(7)) ''CDbl(Mid(arrLinDoc(7), 1, Len(arrLinDoc(7)) - 2) & "." & Right(arrLinDoc(7), 2))       'dblMontoPagar = 149.53
                        If dblMontoRestante < dblMontoPagar Then      ' 455.61 < 295.79 
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
                            dblMontoPAC = CDbl(arrSigDoc(7)) 'CDbl(Mid(arrSigDoc(7), 1, Len(arrSigDoc(7)) - 2) & "." & Right(arrSigDoc(7), 2)) ' TODO CAMBIADO POR JYMMY TORRES
                            If dblMontoPAC > Decimal.Round((dblMontoRestante - dblMontoPagar), 2) Then
                                dblMontoPagar += Decimal.Round((dblMontoRestante - dblMontoPagar), 2)
                            End If
                        End If
                    End If
                    'fin de Validacion
                    dblMontoRestante = Decimal.Round((dblMontoRestante - dblMontoPagar), 2)      'dblMontoRestante = 0.47
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	dblMontoPagar: " & dblMontoPagar.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	dblMontoRestante: " & dblMontoRestante.ToString())

                    '''TAMA DOCUMENTO
                    strServicio = Trim(arrLinDoc(0))
                    strDesServicio = Trim(arrLinDoc(1))
                    strMoneda = arrLinDoc(2)
                    strTipoDocumento = arrLinDoc(3)
                    strNumeroDocumento = arrLinDoc(4)
                    strImporteRecibo = arrLinDoc(7) 'CDbl(Mid(arrLinDoc(7), 1, Len(arrLinDoc(7)) - 2) & "." & Right(arrLinDoc(7), 2))
                    strFechaEmision = Mid(arrLinDoc(5), 7, 2) & "/" & Mid(arrLinDoc(5), 5, 2) & "/" & Mid(arrLinDoc(5), 1, 4)

                    'MSG Si el Pago viene por la consulta por DNI
                    If (strDeudaDni = True) Then
                        strCustomerId = arrLinDoc(8)
                        strTipoServicio = arrLinDoc(9)
                        strNroLinea = arrLinDoc(10)
                        'strTipoIdentificadorDeudor = strTipoDocumento
                        If (strServicio = "REC") Then
                            strNumeroIdentificadorDeudor = strNroLinea
                            'ElseIf (strServicio = "101") Then
                        Else
                            strNumeroIdentificadorDeudor = "23" & strCustomerId
                        End If
                    End If

                    If strTipoIdentificadorDeudor = gstrCodTelefono Then
                        strNumeroTelefono = strNumeroIdentificadorDeudor
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	Tipo de Servicio: " & strServicio)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	Dato Identificacion: " & strNumeroIdentificadorDeudor)


                    If PagarReciboST_DNI(strLogSET, strNivelLogSet, _
                     strCodigoPuntoDeVenta, _
                     strCanal, _
                     strBinAdquiriente, _
                     strCodComercio, _
                     strCodigoCajero, _
                     strTipoIdentificadorDeudor, _
                     strNumeroIdentificadorDeudor, _
                     dblMontoPagar, _
                     strMoneda, _
                     strServicio, _
                     strTipoDocumento, _
                     strNumeroDocumento, _
                     strRespuestaConsulta, strOrigenRpta, _
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
                     numeroMac, _
                     codigoAcreedor, _
                     binAdquirienteRenvio, _
                     codigoFormato, _
                     nombreComercio, _
                     codigoPlazaRecaudador, _
                     medioPagoAuxiliar, _
                     codigoEstadoDeudor, _
                     codigoProcesador, _
                     strCodigoPlazaRecaudador, strCodigoAgenciaRecaudador, strCodigoCiudadRecaudador, "", strDeudaDni) Then

                        strFechaHoraTransac = strFechaHoraTransaccion
                        pstrNumeroOperacionCobranza = strNumeroOperacionCobranza
                        pstrNumeroOperacionAcreedor = strNumeroOperacionAcreedor

                        strNombreDeudorSAP = strNombreDeudor
                        strRucDeudorSAP = strRucDeudor

                        If Len(Trim(strRucDeudorSAP)) = 0 Then
                            strRucDeudorSAP = "99999999"
                        End If

                        dblImporteTotalPagado = dblImporteTotalPagado + dblValorTotal

                        strFechaPago = Mid(strFechaHoraTransaccion, 1, 8)
                        strFechaPago = Mid(strFechaPago, 3, 2) & "/" & Mid(strFechaPago, 1, 2) & "/" & Year(Now)

                        'ESTRUCTURA (TI_RECIBOS) PARA GRABAR PAGO EN SAP
                        If (strServicio = "REC") Then
                            gstrRecibos = gstrRecibos & ";000001;" & strTipoDocumento & ";" & strNumeroDocumento & ";" & strMoneda & ";" & strImporteRecibo & ";" & dblMontoPagar & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strFechaEmision & ";" & strFechaPago & ";;" & gstrTracePago & ";" & strDesServicio & ";" & strFechaHoraTransaccion & ";" & strServicio & "|"
                        Else
                            Dim oDescripcionFija = ConfigurationSettings.AppSettings("Const_DescripcionFija")
                            gstrRecibos = gstrRecibos & ";000001;" & strTipoDocumento & ";" & strNumeroDocumento & ";" & strMoneda & ";" & strImporteRecibo & ";" & dblMontoPagar & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strFechaEmision & ";" & strFechaPago & ";;" & gstrTracePago & ";" & oDescripcionFija & ";" & strFechaHoraTransaccion & ";" & strServicio & "|"
                        End If
                        strAnularPago = strAnularPago & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";" & gstrTracePago & ";" & strFechaHoraTransaccion & "|"

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	dblImporteTotalPagado: " & dblImporteTotalPagado.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	gstrRecibos: " & gstrRecibos)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strAnularPago: " & strAnularPago)

                        'INICIO TS-JTN
                        itemAnulacion = New ItemAnulacion(dblMontoPagar, strNumeroDocumento, strNumeroOperacionCobranza, strNumeroOperacionAcreedor, strFechaHoraTransaccion, gstrTracePago)
                        listaAnulacion.Add(itemAnulacion)

                        'FIN TS-JTN
                    Else
                        Throw New System.Net.WebException(strDescripcionRpta)
                    End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	Fin PagoDocumento Nro: " & intNumDocs.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
                    '  End If  'FIN de Filtro de PAC
                Next

                If strAnularPago <> String.Empty Then
                    strAnularPago = Mid(strAnularPago, 1, Len(strAnularPago) - 1)
                End If
                'ESTRUCTURA (TI_DEUDA) PARA GRABAR PAGO EN SAP
                Dim oFijo$ = ConfigurationSettings.AppSettings("constEstadoPagoFijo")
                If (strServicio = "REC") Then
                    gstrDeuda = ";" & strNombreDeudorSAP & ";" & strRucDeudorSAP & ";" & strCodigoPuntoDeVenta & ";" & nombrePuntoDeVenta & ";" & strMoneda & ";" & dblImporteTotalPagado & ";;;" & gstrEstadoPago & ";" & strNumeroTelefono & ";" & strCodigoCajero & ";" & nombreCajero & ";" & strTraceConsulta & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor
                Else
                    gstrDeuda = ";" & strNombreDeudorSAP & ";" & strRucDeudorSAP & ";" & strCodigoPuntoDeVenta & ";" & nombrePuntoDeVenta & ";" & strMoneda & ";" & dblImporteTotalPagado & ";;;" & oFijo & ";" & strNroLinea & ";" & strCodigoCajero & ";" & nombreCajero & ";" & strTraceConsulta & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strAnularPago: " & strAnularPago)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strFormasPago: " & strFormasPago)

                'FORMAS DE PAGO
                If Trim(strFormasPago) <> "" Then
                    arrFormasPago = Split(strFormasPago, "|")
                    For intNumFormasPago = 0 To UBound(arrFormasPago)
                        arrLinFormasPago = Split(arrFormasPago(intNumFormasPago), ";")
                        'ESTRUCTURA (TI_PAGOS) PARA GRABAR PAGO EN SAP
                        If dblImporteTotalPagado <= CDec(arrLinFormasPago(1)) Then
                            gstrPagos = gstrPagos & ";;" & arrLinFormasPago(0) & ";" & dblImporteTotalPagado & ";" & arrLinFormasPago(2) & ";" & arrLinFormasPago(3) & ";" & arrLinFormasPago(4) & "|"
                            Exit For
                        Else
                            gstrPagos = gstrPagos & ";;" & arrLinFormasPago(0) & ";" & arrLinFormasPago(1) & ";" & arrLinFormasPago(2) & ";" & arrLinFormasPago(3) & ";" & arrLinFormasPago(4) & "|"
                            dblImporteTotalPagado = dblImporteTotalPagado - CDec(arrLinFormasPago(1))
                        End If
                    Next
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	gstrPagos: " & gstrPagos)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	dblImporteTotalPagado: " & dblImporteTotalPagado.ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strRespuestaConsulta ST: " & strRespuestaConsulta)

                If Trim(strRespuestaConsulta) = "" Then
                    strRespuestaConsulta = "0"
                End If

                If intNumDocs = 0 And CInt(strRespuestaConsulta) <> 0 Then
                    strResultado = strRespuestaConsulta & "@" & strDescripcionRpta & "@"
                Else
                    If CInt(strRespuestaConsulta) = 0 Then
                        'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Set_RegistroDeuda (Zpvu_Rfc_Trs_Reg_Deuda)")

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio SetRegistroDeuda (ObjOffline.SetRegistroDeuda)" & vbCrLf)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Stored Procedure: CONF_Trs_DeudaCabecera" & vbCrLf)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Stored Procedure: CONF_Trs_RegistroDeuda" & vbCrLf)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Stored Procedure: CONF_Trs_RegistroRecibo" & vbCrLf)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Stored Procedure: CONF_Trs_RegistroPagos" & vbCrLf)


                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	INP gstrDeuda: " & gstrDeuda)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	INP gstrRecibos: " & gstrRecibos)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	INP gstrPagos: " & gstrPagos)

                        '''TODO: CAMBIADO POR JYMMY TORRES
                        '''
                        '''////////////////////////
                        'strResultado = objSAP.Set_RegistroDeuda(gstrDeuda, gstrRecibos, gstrPagos, strValorResultado)
                        strResultado = objSAP.SetRegistroDeuda(gstrDeuda, gstrRecibos, gstrPagos, strValorResultado)
                        '''////////////////////////
                        '''
                        '''CAMBIADO HASTA AQUI

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	OUT strValorResultado: " & strValorResultado)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin SetRegistroDeuda (ObjOffline.SetRegistroDeuda)")
                        'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Set_RegistroDeuda (Zpvu_Rfc_Trs_Reg_Deuda)")
                    Else
                        strResultado = strRespuestaConsulta & "@" & strDescripcionRpta & "@"
                    End If
                End If
                '********************************** Log en BD para cuadre de Caja
                GuardarLogCuadreCaja(intCodAplicacion, _
                 strTipoAlmacen, _
                 strCodigoPuntoDeVenta, _
                 strCodigoCajero, _
                 strNumeroTelefono, _
                 strFormasPago, _
                 dblMontoTotalPagar, _
                 strNumeroDocumento, _
                 strDocCliente, _
                 decImpRecPen, _
                 decImpRecUsd, _
                 decVuelto, _
                 strResultado, _
                 strValorResultado) ''TODO: CALLBACK ORACLE
                '**********************************
                If strValorResultado = "" Then
                    strValorResultado = "1"
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strResultado: " & strResultado)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strValorResultado: " & strValorResultado)
                Try
                    ' normalmente deberian ser iguales (CDbl(strValorResultado) y CDbl(Split(strResultado, "@")(0)))
                    If CDbl(strValorResultado) <> 0 Or CDbl(Split(strResultado, "@")(0)) <> 0 Then
                        strRecibosAnular = clsAnular.Anular(strLogSET, _
                          strNivelLogSet, _
                          strCodigoPuntoDeVenta, _
                          strCanal, _
                          strBinAdquiriente, _
                          strCodComercio, _
                          strCodigoCajero, _
                          strTipoIdentificadorDeudor, _
                          strNumeroIdentificadorDeudor, _
                          strAnularPago, _
                          strCodigoPlazaRecaudador, _
                          strCodigoAgenciaRecaudador, _
                          strCodigoCiudadRecaudador)
                    End If
                Catch
                    'Pero se ha dado el caso en que llega en blanco para strResultado, asi que solo se pregunta por 1
                    If CDbl(strValorResultado) <> 0 Then
                        strRecibosAnular = clsAnular.Anular(strLogSET, _
                          strNivelLogSet, _
                          strCodigoPuntoDeVenta, _
                          strCanal, _
                          strBinAdquiriente, _
                          strCodComercio, _
                          strCodigoCajero, _
                          strTipoIdentificadorDeudor, _
                          strNumeroIdentificadorDeudor, _
                          strAnularPago, _
                          strCodigoPlazaRecaudador, _
                          strCodigoAgenciaRecaudador, _
                          strCodigoCiudadRecaudador)
                    End If
                End Try
            End If
            PagarRecibos_DNI = strResultado
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin PagarRecibos")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")


            Return strResultado
        Catch webEx As System.Net.WebException
            PagarRecibos_DNI = BuscarMensaje(9) ' "8@Verifique los datos ingresados. Por favor vuelva a intentar." & "@"
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: PagarRecibos_DNI)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	Ocurrio el error: " & webEx.Message & MaptPath)
            'FIN PROY-140126
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Anulando Pagos Enviados TOTAL: " & listaAnulacion.Count)
            Dim obAnul As New COM_SIC_Recaudacion.clsAnulaciones
            Dim numeroAnulados As Int32
            For Each itemAnulacion In listaAnulacion
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	Anulando RECIBO: " & itemAnulacion.numeroDocumento & " TRACE: " & itemAnulacion.tracePago)
                Dim success As Boolean = obAnul.AnularPagoST(strLogSET, strNivelLogSet, strCodigoPuntoDeVenta, strCanal, strBinAdquiriente, strCodComercio, strCodigoCajero, strTipoIdentificadorDeudor, _
                 strNumeroIdentificadorDeudor, itemAnulacion.montoPagado, strMoneda, strServicio, strTipoDocumento, itemAnulacion.numeroDocumento, itemAnulacion.numeroOperacionCobranza, itemAnulacion.numeroOperacionAcreedor, itemAnulacion.tracePago, itemAnulacion.fechaHoraTransaccion, _
                 "", "", "", "", "", "", "", "", 0, 0, "", "")
                'INI PROY-140126               
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: PagarRecibos_DNI)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & IIf(success, String.Format("RECIBO {0} ANULADO CORRECTAMENTE", itemAnulacion.numeroDocumento), String.Format("ERROR ANULANDO RECIBO {0}", itemAnulacion.numeroDocumento & MaptPath)))
                'FIN PROY-140126

                numeroAnulados += IIf(success, 1, 0)
            Next
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Anulando Pagos Enviados TOTAL ANULADOS: " & numeroAnulados)
        Catch ex As Exception
            PagarRecibos_DNI = "8@Verifique los datos ingresados. Por favor vuelva a intentar." & "@"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	Ocurrio el error: " & ex.Message)
        End Try

    End Function

    '''PAGO CORPORATIVO EN ST
    Private Function PagarReciboST_DNI(ByVal strLogSET As String, ByVal strNivelLogSet As String, _
                               ByVal strCodigoPuntoDeVenta As String, _
                               ByVal strCanal As String, _
                               ByVal strBinAdquiriente As String, _
                               ByVal strCodComercio As String, _
                               ByVal strCodigoCajero As String, _
                               ByVal strTipoIdentificadorDeudor As String, _
                               ByVal strNumeroIdentificadorDeudor As String, _
                               ByVal dblMontoPagar As Double, _
                               ByVal strMoneda As String, _
                               ByVal strServicio As String, _
                               ByVal strTipoDocumento As String, _
                               ByVal strNumeroDocumento As String, _
                               ByRef strRespuestaConsulta As String, ByRef strOrigenRpta As String, _
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
                               ByVal numeroMac As String, _
                               ByVal codigoAcreedor As String, _
                               ByVal binAdquirienteRenvio As String, _
                               ByVal codigoFormato As String, _
                               ByVal nombreComercio As String, _
                               ByVal codigoPlazaRecaudador As String, _
                               ByVal medioPagoAuxiliar As String, _
                               ByVal codigoEstadoDeudor As String, _
                               ByVal codigoProcesador As String, _
                               Optional ByVal strCodigoPlazaRecaudador As String = "", _
                               Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                               Optional ByVal strCodigoCiudadRecaudador As String = "", Optional ByRef strFechaHoraTransac As String = "", _
                               Optional ByVal strDeudaDni As Boolean = False) As Boolean

        '---CLIENTE CORPORATIVO PAGO
        Dim strIdentifyLog As String = strTipoIdentificadorDeudor & "|" & strNumeroIdentificadorDeudor

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "URL WS : " & oSwichTransaccional.Url)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Metodo : " & "pago")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio PagarReciboST")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

        'Dim objComponente
        Dim strTrace As String
        Dim strRpta As String
        Dim strDocsAux As String

        Dim strCodServicio As String
        Dim strDesServicio As String
        Dim strMonServicio As String
        Dim strTipoDoc As String = ConfigurationSettings.AppSettings("CONST_PRODUCTOMOVIL")
        strTipoDoc = strTipoDocumento 'asignando el tipo servicio que venga de la consulta

        Dim strNumDoc As String
        Dim strFechaEmi As String
        Dim strFechaVen As String
        Dim strMontoDoc As String
        Dim strLinDocs As String = ""

        Dim intNumDocs As Integer
        'Dim clsSap As New SAP_SIC_Recaudacion.clsRecaudacion
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        'Dim intSAP = objOffline.Get_ConsultaSAP
        Dim clsSap As COM_SIC_OffLine.clsOffline

        'If intSAP = 1 Then
        '    clsSap = New SAP_SIC_Recaudacion.clsRecaudacion
        'Else
        clsSap = New COM_SIC_OffLine.clsOffline
        'End If
        Dim sapOperations = New SAP_SIC_Recaudacion.clsRecaudacion
        Dim strIngreso As String
        Dim lngLog As Long
        Dim strLinDocsLog As String = ""
        Dim strDocumentosLog As String = ""

        Dim strFecha As String
        Dim strHora As String
        Dim strDescripcionRptaAux As String

        Dim pagoEnvio As New PagoRequest
        Dim pagoRespuesta As New PagoResponse

        Try
            strIngreso = ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";;" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero

            '''CAMBIADO POR JTN
            strTrace = clsSap.SetLogRecaudacion(strIngreso, "")
            '''CAMBIADO HASTA AQUI

            strTrace = Right(strTrace, 6)
            strTrace = "0000" & strTrace
            'CARIAS: fin
            gstrTracePago = strTrace

            Dim itemProducto As New ProductoServicioAPagar
            Dim itemDocumento As New DocumentoAPagar
            Dim listaProductos(0) As ProductoServicioAPagar
            Dim listaDocumento(0) As DocumentoAPagar

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio crear itemDocumento")
            With itemDocumento

                .importeAPagar = dblMontoPagar
                .importeAPagarSpecified = True
                .importeDeudaOriginal = String.Format("{0:F}", dblValorTotal) '''dblValorTotal

                'Solo desarrollo MSG
                '.importeDeudaOriginal = dblValorTotal

                .importeDeudaOriginalSpecified = True
                .numeroDocumento = strNumeroDocumento
                .tipoDocumento = strTipoDoc

            End With
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin crear itemDocumento")

            listaDocumento(0) = itemDocumento

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio crear itemProducto")
            With itemProducto

                .codigo = strTipoDoc
                .documentos = listaDocumento
                .montoTotalAPagar = String.Format("{0:F}", dblMontoPagar)

                'Solo desarrollo MSG
                '.montoTotalAPagar = dblMontoPagar

                .montoTotalAPagarSpecified = True
                .numeroTotalDocumentosAPagar = 1
                .numeroTotalDocumentosAPagarSpecified = True

                .estadoDeudor = "A"

            End With
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin crear itemProducto")
            listaProductos(0) = itemProducto

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio crear pagoEnvio")

            With pagoEnvio

                If (strServicio = "REC") Then

                Else 'FIJA
                    .mac = ConfigurationSettings.AppSettings("CONST_MAC")
                    .codigoFormato = "1"
                    .codigoMonedaPago = strMoneda
                    .numeroDocumentosPagados = 1
                    .numeroProductosPagados = 1
                    .procesador = ConfigurationSettings.AppSettings("CONST_PROCESADOR")
                End If

                .binAdquiriente = strBinAdquiriente
                .binAdquirienteReenvia = ConfigurationSettings.AppSettings("CONST_BIN_REENVIO")
                .canal = strCanal
                .codigoMoneda = strMoneda
                .fechaCaptura = Date.Now.ToString("yyyy-MM-dd-05:00")
                .fechaCapturaSpecified = True
                .fechaTransaccion = Date.Now.ToString("yyyy-MM-ddTHH:mm:ss.ms-05:00")
                .fechaTransaccionSpecified = True
                .numeroReferencia = ConfigurationSettings.AppSettings("CONST_NUMEROREFERENCIA")
                .numeroTerminal = strBinAdquiriente
                .trace = strTrace
                .numeroComercio = .binAdquiriente
                .acreedor = ConfigurationSettings.AppSettings("CONST_ACREEDOR")
                .codigoMoneda = strMoneda
                .importePago = String.Format("{0:F}", dblMontoPagar)

                'Solo desarrollo MSG
                '.importePago = dblMontoPagar

                .importePagoSpecified = True

                .importePagoEfectivo = String.Format("{0:F}", dblMontoPagar)

                'Solo desarrollo MSG
                '.importePagoEfectivo = dblMontoPagar

                .importePagoEfectivoSpecified = True

                .numeroIdentificacionDeudor = strNumeroIdentificadorDeudor

                .pagoTotal = String.Format("{0:F}", dblMontoPagar)

                'Solo desarrollo MSG
                '.pagoTotal = dblMontoPagar

                .pagoTotalSpecified = True
                .productos = listaProductos
                .tipoIdentificacionDeudor = ConfigurationSettings.AppSettings("CONST_CODIGO_FORMATO")

                .tipoIdentificacionDeudor = "1" 'MSG

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  TerminalID : " & strCodigoPuntoDeVenta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Trace : " & strTrace)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Canal : " & strCanal)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  TipoIdentificacionDeudor : " & strTipoIdentificadorDeudor)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Monto : " & CDbl(dblMontoPagar).ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Moneda : " & strMoneda)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Servicio : " & strServicio)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodigoPlazaRecaudador : " & strCodigoPlazaRecaudador)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodigoAgenciaRecaudador : " & strCodigoAgenciaRecaudador)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodigoCiudadRecaudador : " & strCodigoCiudadRecaudador)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  BinAdquiriente : " & strBinAdquiriente)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodComercio : " & strCodComercio)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  TipoDocumento : " & strTipoDocumento)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  NumeroDocumento : " & strNumeroDocumento)
            End With
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin crear pagoEnvio")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio envio pago al ST")

            pagoRespuesta = oSwichTransaccional.pago(pagoEnvio)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin envio pago al ST")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo : " & "pago")

            With pagoRespuesta

                strRpta = .codigoRespuesta
                strRespuestaConsulta = strRpta
                strOrigenRpta = .codigoOrigenRespuesta
                strCodigoRpta = .codigoRespuesta
                strDescripcionRpta = .descripcionExtendidaRespuesta

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Pago : " & strRpta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  OrigenRpta : " & strOrigenRpta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CodigoRpta : " & strCodigoRpta)
                'INI PROY-140126
                Dim MaptPath As String
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: PagarReciboST_DNI)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  DescripcionRpta : " & strDescripcionRpta & MaptPath)
                'FIN PROY-140126
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

                        'strDocumentos = .Documentos

                        '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CABECERA : " & strNombreDeudor & ";" & strRucDeudor & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & dblValorTotal.ToString() & ";" & intNumeroDocumentos.ToString() & ";" & strFechaHoraTransaccion & ";" & strFechaHoraTransac)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CABECERA : " & ";" & strRucDeudor & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & dblValorTotal.ToString() & ";" & intNumeroDocumentos.ToString() & ";" & strFechaHoraTransaccion & ";" & strFechaHoraTransac)

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

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  RECIBOS : " & strDocumentos)

                        If Trim(strFechaHoraTransaccion) <> "" Then
                            strFecha = Mid(strFechaHoraTransaccion, 3, 2) & "/" & Mid(strFechaHoraTransaccion, 1, 2) & "/" & Year(Now)
                            strHora = Mid(strFechaHoraTransaccion, 5, 2) & ":" & Mid(strFechaHoraTransaccion, 7, 2) & ":" & Mid(strFechaHoraTransaccion, 9, 2)
                        End If

                        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal

                        '''CAMBIADO POR JTN
                        strTrace = clsSap.Set_LogRecaudacion(strIngreso, strDocumentosLog)
                        '''CAMBIADO HASTA AQUI

                        PagarReciboST_DNI = True
                    Case Else
                        strNombreDeudor = ""
                        strRucDeudor = ""
                        dblValorTotal = 0
                        strNumeroOperacionCobranza = ""
                        intNumeroDocumentos = 0
                        strDocumentos = ""
                        strFechaHoraTransaccion = ""
                        PagarReciboST_DNI = False

                        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal

                        '''CAMBIADO POR JTN
                        strTrace = clsSap.Set_LogRecaudacion(strIngreso, strDocumentosLog)
                        '''CAMBIADO HASTA AQUI

                        If Trim(strDescripcionRpta) = String.Empty Then
                            strDescripcionRpta = BuscarMensaje(8)
                        End If
                End Select
            End With
            clsSap = Nothing
        Catch webEx As System.Net.WebException
            PagarReciboST_DNI = False
            clsSap = Nothing
            pagoRespuesta = Nothing
            pagoEnvio = Nothing
            oSwichTransaccional.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "EXCEPCION: " & webEx.Message)
            Throw webEx
        Catch ex As Exception
            Dim stackTrace As New System.Diagnostics.StackTrace(ex)
            PagarReciboST_DNI = False
            clsSap = Nothing
            pagoRespuesta = Nothing
            pagoEnvio = Nothing
            oSwichTransaccional.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "EXCEPCION: " & ex.Message)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "EXCEPCION en linea de codigo : " & stackTrace.GetFrame(0).GetFileLineNumber())
        Finally
            pagoRespuesta = Nothing
            pagoEnvio = Nothing
            oSwichTransaccional.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin PagarReciboST")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try
    End Function


    ''PAGO CLIENTE CORPORATIVO CON CALLBACK HACIA EL ST
    Public Function Pagar(ByVal strLogSET As String, _
                            ByVal strNivelLogSet As String, _
                            ByVal strCodigoPuntoDeVenta As String, _
                            ByVal nombrePuntoDeVenta As String, _
                            ByVal strCanal As String, _
                            ByVal strBinAdquiriente As String, _
                            ByVal strCodComercio As String, _
                            ByVal strCodigoCajero As String, _
                            ByVal nombreCajero As String, _
                            ByVal strTipoIdentificadorDeudor As String, _
                            ByVal strNumeroIdentificadorDeudor As String, _
                            ByVal strFormasPago As String, _
                            ByVal dblMontoTotalPagar As Double, _
                            ByVal strRecibosPagar As String, _
                            ByVal strTraceConsulta As String, _
                            ByVal strDocCliente As String, _
                            ByVal strTipoAlmacen As String, _
                            ByVal intCodAplicacion As String, _
                            ByVal decImpRecPen As Decimal, _
                            ByVal decImpRecUsd As Decimal, _
                            ByVal decVuelto As Decimal, _
                            ByVal numeroMac As String, _
                            ByVal codigoAcreedor As String, _
                            ByVal binAdquirienteRenvio As String, _
                            ByVal codigoFormato As String, _
                            ByVal nombreComercio As String, _
                            ByVal codigoPlazaRecaudador As String, _
                            ByVal medioPagoAuxiliar As String, _
                            ByVal codigoEstadoDeudor As String, _
                            ByVal codigoProcesador As String, _
                            Optional ByVal strCodigoPlazaRecaudador As String = "", _
                            Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                            Optional ByVal strCodigoCiudadRecaudador As String = "", Optional ByRef strFechaHoraTransac As String = "", _
                            Optional ByRef pstrNumeroOperacionCobranza As String = "", Optional ByRef pstrNumeroOperacionAcreedor As String = "") As String

        Dim strResultado As String = PagarRecibos(strLogSET, _
                                strNivelLogSet, _
                                strCodigoPuntoDeVenta, _
                                nombrePuntoDeVenta, _
                                strCanal, _
                                strBinAdquiriente, _
                                strCodComercio, _
                                strCodigoCajero, _
                                nombreCajero, _
                                strTipoIdentificadorDeudor, _
                                strNumeroIdentificadorDeudor, _
                                strFormasPago, _
                                dblMontoTotalPagar, _
                                strRecibosPagar, _
                                strTraceConsulta, _
                                strDocCliente, _
                                strTipoAlmacen, _
                                intCodAplicacion, _
                                decImpRecPen, _
                                decImpRecUsd, _
                                decVuelto, _
                                numeroMac, _
                                codigoAcreedor, _
                                binAdquirienteRenvio, _
                                codigoFormato, _
                                nombreComercio, _
                                codigoPlazaRecaudador, _
                                medioPagoAuxiliar, _
                                codigoEstadoDeudor, _
                                codigoProcesador, _
                                strCodigoPlazaRecaudador, _
                                strCodigoAgenciaRecaudador, _
                                strCodigoCiudadRecaudador, strFechaHoraTransac, pstrNumeroOperacionCobranza, pstrNumeroOperacionAcreedor)
        Return strResultado
    End Function

    Private Function PagarRecibos(ByVal strLogSET As String, _
                                ByVal strNivelLogSet As String, _
                                ByVal strCodigoPuntoDeVenta As String, _
                                ByVal nombrePuntoDeVenta As String, _
                                ByVal strCanal As String, _
                                ByVal strBinAdquiriente As String, _
                                ByVal strCodComercio As String, _
                                ByVal strCodigoCajero As String, _
                                ByVal nombreCajero As String, _
                                ByVal strTipoIdentificadorDeudor As String, _
                                ByVal strNumeroIdentificadorDeudor As String, _
                                ByVal strFormasPago As String, _
                                ByVal dblMontoTotalPagar As Double, _
                                ByVal strRecibosPagar As String, _
                                ByVal strTraceConsulta As String, _
                                ByVal strDocCliente As String, _
                                ByVal strTipoAlmacen As String, _
                                ByVal intCodAplicacion As String, _
                                ByVal decImpRecPen As Decimal, _
                                ByVal decImpRecUsd As Decimal, _
                                ByVal decVuelto As Decimal, _
                                ByVal numeroMac As String, _
                                ByVal codigoAcreedor As String, _
                                ByVal binAdquirienteRenvio As String, _
                                ByVal codigoFormato As String, _
                                ByVal nombreComercio As String, _
                                ByVal codigoPlazaRecaudador As String, _
                                ByVal medioPagoAuxiliar As String, _
                                ByVal codigoEstadoDeudor As String, _
                                ByVal codigoProcesador As String, _
                                Optional ByVal strCodigoPlazaRecaudador As String = "", _
                                Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                                Optional ByVal strCodigoCiudadRecaudador As String = "", Optional ByRef strFechaHoraTransac As String = "", _
                                Optional ByRef pstrNumeroOperacionCobranza As String = "", Optional ByRef pstrNumeroOperacionAcreedor As String = "") As String



        Dim strIdentifyLog As String = strTipoIdentificadorDeudor & "|" & strNumeroIdentificadorDeudor
        Dim arrDocs
        Dim arrLinDoc
        Dim intNumDocs As Integer

        Dim arrFormasPago
        Dim arrLinFormasPago
        Dim intNumFormasPago As Integer

        Dim strRespPagoMasivo As String
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
        Dim strNumeroOperacionCobranza As String
        Dim strNumeroOperacionAcreedor As String
        Dim dblValorTotal As Double
        Dim intNumeroDocumentos As Integer
        Dim strDocumentos As String
        Dim strFechaHoraTransaccion As String

        Dim strMoneda As String
        Dim strServicio As String
        Dim strDesServicio As String
        Dim strTipoDocumento As String
        Dim strNumeroDocumento As String
        Dim strImporteRecibo As String
        Dim dblImporteTotalPagado As Decimal


        Dim objOffline As New COM_SIC_OffLine.clsOffline
        ''TODO: CAMBIADO POR JYMMY TORRES
        Dim objSAP As COM_SIC_OffLine.clsOffline
        objSAP = New COM_SIC_OffLine.clsOffline
        'End If
        ''CAMBIADO HASTA AQUI


        Dim listaAnulacion As New System.Collections.ArrayList
        Dim tramaAnulacion As String = String.Empty
        Dim itemAnulacion As ItemAnulacion

        Dim strNumeroTelefono As String
        Dim strNombreDeudorSAP As String
        Dim strRucDeudorSAP As String
        Dim strFechaEmision As String
        Dim strFechaPago As String
        Dim strValorResultado As String
        Dim strRecibosAnular As String
        Dim strAnularPago As String
        Dim clsAnular As New clsAnulaciones

        Dim gstrDeuda As String = ""
        Dim gstrRecibos As String = ""
        Dim gstrPagos As String = ""

        Dim arrSigDoc
        Dim dblMontoPAC As Double

        Try
            dblMontoRestante = dblMontoTotalPagar    'dblMontoRestante = 550

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio PagarRecibos")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	dblMontoRestante: " & dblMontoRestante)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strRecibosPagar: " & strRecibosPagar)

            If Trim(strRecibosPagar) <> "" Then
                arrDocs = Split(strRecibosPagar, "|")



                For intNumDocs = 0 To UBound(arrDocs)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	Inicio PagoDocumento Nro: " & intNumDocs.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

                    arrLinDoc = Split(arrDocs(intNumDocs), ";")
                    If intNumDocs = UBound(arrDocs) Then
                        dblMontoPagar = dblMontoRestante
                    Else
                        dblMontoPagar = CDbl(arrLinDoc(7)) ''CDbl(Mid(arrLinDoc(7), 1, Len(arrLinDoc(7)) - 2) & "." & Right(arrLinDoc(7), 2))       'dblMontoPagar = 149.53
                        If dblMontoRestante < dblMontoPagar Then      ' 455.61 < 295.79 
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
                            dblMontoPAC = CDbl(arrSigDoc(7)) 'CDbl(Mid(arrSigDoc(7), 1, Len(arrSigDoc(7)) - 2) & "." & Right(arrSigDoc(7), 2)) ' TODO CAMBIADO POR JYMMY TORRES
                            If dblMontoPAC > Decimal.Round((dblMontoRestante - dblMontoPagar), 2) Then
                                dblMontoPagar += Decimal.Round((dblMontoRestante - dblMontoPagar), 2)
                            End If
                        End If
                    End If
                    'fin de Validacion
                    dblMontoRestante = Decimal.Round((dblMontoRestante - dblMontoPagar), 2)      'dblMontoRestante = 0.47
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	dblMontoPagar: " & dblMontoPagar.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	dblMontoRestante: " & dblMontoRestante.ToString())

                    '''TAMA DOCUMENTO
                    strServicio = Trim(arrLinDoc(0))
                    strDesServicio = Trim(arrLinDoc(1))
                    strMoneda = arrLinDoc(2)
                    strTipoDocumento = arrLinDoc(3)
                    strNumeroDocumento = arrLinDoc(4)
                    strImporteRecibo = arrLinDoc(7) 'CDbl(Mid(arrLinDoc(7), 1, Len(arrLinDoc(7)) - 2) & "." & Right(arrLinDoc(7), 2))
                    strFechaEmision = Mid(arrLinDoc(5), 7, 2) & "/" & Mid(arrLinDoc(5), 5, 2) & "/" & Mid(arrLinDoc(5), 1, 4)
                    If strTipoIdentificadorDeudor = gstrCodTelefono Then
                        strNumeroTelefono = strNumeroIdentificadorDeudor
                    End If
                    '''TRAMA DOCUMENTO

                    If PagarReciboST(strLogSET, strNivelLogSet, _
                     strCodigoPuntoDeVenta, _
                     strCanal, _
                     strBinAdquiriente, _
                     strCodComercio, _
                     strCodigoCajero, _
                     strTipoIdentificadorDeudor, _
                     strNumeroIdentificadorDeudor, _
                     dblMontoPagar, _
                     strMoneda, _
                     strServicio, _
                     strTipoDocumento, _
                     strNumeroDocumento, _
                     strRespuestaConsulta, strOrigenRpta, _
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
                     numeroMac, _
                     codigoAcreedor, _
                     binAdquirienteRenvio, _
                     codigoFormato, _
                     nombreComercio, _
                     codigoPlazaRecaudador, _
                     medioPagoAuxiliar, _
                     codigoEstadoDeudor, _
                     codigoProcesador, _
                     strCodigoPlazaRecaudador, strCodigoAgenciaRecaudador, strCodigoCiudadRecaudador) Then

                        strFechaHoraTransac = strFechaHoraTransaccion
                        pstrNumeroOperacionCobranza = strNumeroOperacionCobranza
                        pstrNumeroOperacionAcreedor = strNumeroOperacionAcreedor

                        strNombreDeudorSAP = strNombreDeudor
                        strRucDeudorSAP = strRucDeudor

                        '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strNombreDeudorSAP: " & strNombreDeudorSAP)
                        '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strRucDeudorSAP: " & strRucDeudorSAP)

                        If Len(Trim(strRucDeudorSAP)) = 0 Then
                            strRucDeudorSAP = "99999999"
                        End If

                        dblImporteTotalPagado = dblImporteTotalPagado + dblValorTotal

                        strFechaPago = Mid(strFechaHoraTransaccion, 1, 8)
                        strFechaPago = Mid(strFechaPago, 3, 2) & "/" & Mid(strFechaPago, 1, 2) & "/" & Year(Now)

                        'ESTRUCTURA (TI_RECIBOS) PARA GRABAR PAGO EN SAP
                        gstrRecibos = gstrRecibos & ";000001;" & strTipoDocumento & ";" & strNumeroDocumento & ";" & strMoneda & ";" & strImporteRecibo & ";" & dblMontoPagar & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strFechaEmision & ";" & strFechaPago & ";;" & gstrTracePago & ";" & strDesServicio & ";" & strFechaHoraTransaccion & ";" & strServicio & "|"
                        strAnularPago = strAnularPago & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";" & gstrTracePago & ";" & strFechaHoraTransaccion & "|"

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	dblImporteTotalPagado: " & dblImporteTotalPagado.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	gstrRecibos: " & gstrRecibos)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strAnularPago: " & strAnularPago)

                        'INICIO TS-JTN
                        itemAnulacion = New ItemAnulacion(dblMontoPagar, strNumeroDocumento, strNumeroOperacionCobranza, strNumeroOperacionAcreedor, strFechaHoraTransaccion, gstrTracePago)
                        listaAnulacion.Add(itemAnulacion)

                        'FIN TS-JTN
                    Else
                        Throw New System.Net.WebException(strDescripcionRpta)
                    End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	Fin PagoDocumento Nro: " & intNumDocs.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
                    '  End If  'FIN de Filtro de PAC
                Next



                If strAnularPago <> String.Empty Then
                    strAnularPago = Mid(strAnularPago, 1, Len(strAnularPago) - 1)
                End If
                'ESTRUCTURA (TI_DEUDA) PARA GRABAR PAGO EN SAP
                gstrDeuda = ";" & strNombreDeudorSAP & ";" & strRucDeudorSAP & ";" & strCodigoPuntoDeVenta & ";" & nombrePuntoDeVenta & ";" & strMoneda & ";" & dblImporteTotalPagado & ";;;" & gstrEstadoPago & ";" & strNumeroTelefono & ";" & strCodigoCajero & ";" & nombreCajero & ";" & strTraceConsulta & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strAnularPago: " & strAnularPago)

                '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	gstrDeuda: " & gstrDeuda)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strFormasPago: " & strFormasPago)

                'FORMAS DE PAGO
                If Trim(strFormasPago) <> "" Then
                    arrFormasPago = Split(strFormasPago, "|")
                    For intNumFormasPago = 0 To UBound(arrFormasPago)
                        arrLinFormasPago = Split(arrFormasPago(intNumFormasPago), ";")
                        'ESTRUCTURA (TI_PAGOS) PARA GRABAR PAGO EN SAP
                        If dblImporteTotalPagado <= CDec(arrLinFormasPago(1)) Then
                            gstrPagos = gstrPagos & ";;" & arrLinFormasPago(0) & ";" & dblImporteTotalPagado & ";" & arrLinFormasPago(2) & ";" & arrLinFormasPago(3) & ";" & arrLinFormasPago(4) & "|"
                            Exit For
                        Else
                            gstrPagos = gstrPagos & ";;" & arrLinFormasPago(0) & ";" & arrLinFormasPago(1) & ";" & arrLinFormasPago(2) & ";" & arrLinFormasPago(3) & ";" & arrLinFormasPago(4) & "|"
                            dblImporteTotalPagado = dblImporteTotalPagado - CDec(arrLinFormasPago(1))
                        End If
                    Next
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	gstrPagos: " & gstrPagos)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	dblImporteTotalPagado: " & dblImporteTotalPagado.ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strRespuestaConsulta ST: " & strRespuestaConsulta)

                If Trim(strRespuestaConsulta) = "" Then
                    strRespuestaConsulta = "0"
                End If

                If intNumDocs = 0 And CInt(strRespuestaConsulta) <> 0 Then
                    strResultado = strRespuestaConsulta & "@" & strDescripcionRpta & "@"
                Else
                    If CInt(strRespuestaConsulta) = 0 Then
                        'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Set_RegistroDeuda (Zpvu_Rfc_Trs_Reg_Deuda)")

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio SetRegistroDeuda (ObjOffline.SetRegistroDeuda)" & vbCrLf)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Stored Procedure: CONF_Trs_DeudaCabecera" & vbCrLf)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Stored Procedure: CONF_Trs_RegistroDeuda" & vbCrLf)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Stored Procedure: CONF_Trs_RegistroRecibo" & vbCrLf)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Stored Procedure: CONF_Trs_RegistroPagos" & vbCrLf)


                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	INP gstrDeuda: " & gstrDeuda)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	INP gstrRecibos: " & gstrRecibos)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	INP gstrPagos: " & gstrPagos)

                        '''TODO: CAMBIADO POR JYMMY TORRES
                        '''
                        '''////////////////////////
                        'strResultado = objSAP.Set_RegistroDeuda(gstrDeuda, gstrRecibos, gstrPagos, strValorResultado)
                        strResultado = objSAP.SetRegistroDeuda(gstrDeuda, gstrRecibos, gstrPagos, strValorResultado)
                        '''////////////////////////
                        '''
                        '''CAMBIADO HASTA AQUI

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	OUT strValorResultado: " & strValorResultado)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin SetRegistroDeuda (ObjOffline.SetRegistroDeuda)")
                        'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Set_RegistroDeuda (Zpvu_Rfc_Trs_Reg_Deuda)")
                    Else
                        strResultado = strRespuestaConsulta & "@" & strDescripcionRpta & "@"
                    End If
                End If
                '********************************** Log en BD para cuadre de Caja
                GuardarLogCuadreCaja(intCodAplicacion, _
                 strTipoAlmacen, _
                 strCodigoPuntoDeVenta, _
                 strCodigoCajero, _
                 strNumeroTelefono, _
                 strFormasPago, _
                 dblMontoTotalPagar, _
                 strNumeroDocumento, _
                 strDocCliente, _
                 decImpRecPen, _
                 decImpRecUsd, _
                 decVuelto, _
                 strResultado, _
                 strValorResultado) ''TODO: CALLBACK ORACLE
                '**********************************
                If strValorResultado = "" Then
                    strValorResultado = "1"
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strResultado: " & strResultado)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	strValorResultado: " & strValorResultado)
                Try
                    ' normalmente deberian ser iguales (CDbl(strValorResultado) y CDbl(Split(strResultado, "@")(0)))
                    If CDbl(strValorResultado) <> 0 Or CDbl(Split(strResultado, "@")(0)) <> 0 Then
                        strRecibosAnular = clsAnular.Anular(strLogSET, _
                          strNivelLogSet, _
                          strCodigoPuntoDeVenta, _
                          strCanal, _
                          strBinAdquiriente, _
                          strCodComercio, _
                          strCodigoCajero, _
                          strTipoIdentificadorDeudor, _
                          strNumeroIdentificadorDeudor, _
                          strAnularPago, _
                          strCodigoPlazaRecaudador, _
                          strCodigoAgenciaRecaudador, _
                          strCodigoCiudadRecaudador)
                    End If
                Catch
                    'Pero se ha dado el caso en que llega en blanco para strResultado, asi que solo se pregunta por 1
                    If CDbl(strValorResultado) <> 0 Then
                        strRecibosAnular = clsAnular.Anular(strLogSET, _
                          strNivelLogSet, _
                          strCodigoPuntoDeVenta, _
                          strCanal, _
                          strBinAdquiriente, _
                          strCodComercio, _
                          strCodigoCajero, _
                          strTipoIdentificadorDeudor, _
                          strNumeroIdentificadorDeudor, _
                          strAnularPago, _
                          strCodigoPlazaRecaudador, _
                          strCodigoAgenciaRecaudador, _
                          strCodigoCiudadRecaudador)
                    End If
                End Try
            End If
            PagarRecibos = strResultado
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin PagarRecibos")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")


            Return strResultado
        Catch webEx As System.Net.WebException
            PagarRecibos = BuscarMensaje(9) ' "8@Verifique los datos ingresados. Por favor vuelva a intentar." & "@"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	Ocurrio el error: " & webEx.Message)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Anulando Pagos Enviados TOTAL: " & listaAnulacion.Count)
            Dim obAnul As New COM_SIC_Recaudacion.clsAnulaciones
            Dim numeroAnulados As Int32
            For Each itemAnulacion In listaAnulacion
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	Anulando RECIBO: " & itemAnulacion.numeroDocumento & " TRACE: " & itemAnulacion.tracePago)
                Dim success As Boolean = obAnul.AnularPagoST(strLogSET, strNivelLogSet, strCodigoPuntoDeVenta, strCanal, strBinAdquiriente, strCodComercio, strCodigoCajero, strTipoIdentificadorDeudor, _
                 strNumeroIdentificadorDeudor, itemAnulacion.montoPagado, strMoneda, strServicio, strTipoDocumento, itemAnulacion.numeroDocumento, itemAnulacion.numeroOperacionCobranza, itemAnulacion.numeroOperacionAcreedor, itemAnulacion.tracePago, itemAnulacion.fechaHoraTransaccion, _
                 "", "", "", "", "", "", "", "", 0, 0, "", "")
                'INI PROY-140126
                Dim MaptPath As String
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: PagarRecibos)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & IIf(success, String.Format("RECIBO {0} ANULADO CORRECTAMENTE", itemAnulacion.numeroDocumento), String.Format("ERROR ANULANDO RECIBO {0}", itemAnulacion.numeroDocumento & MaptPath)))
                'FIN PROY-140126
                numeroAnulados += IIf(success, 1, 0)
            Next
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Anulando Pagos Enviados TOTAL ANULADOS: " & numeroAnulados)
        Catch ex As Exception
            PagarRecibos = "8@Verifique los datos ingresados. Por favor vuelva a intentar." & "@"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	Ocurrio el error: " & ex.Message)
        End Try

    End Function


    '''PAGO CORPORATIVO EN ST
    Private Function PagarReciboST(ByVal strLogSET As String, ByVal strNivelLogSet As String, _
                               ByVal strCodigoPuntoDeVenta As String, _
                               ByVal strCanal As String, _
                               ByVal strBinAdquiriente As String, _
                               ByVal strCodComercio As String, _
                               ByVal strCodigoCajero As String, _
                               ByVal strTipoIdentificadorDeudor As String, _
                               ByVal strNumeroIdentificadorDeudor As String, _
                               ByVal dblMontoPagar As Double, _
                               ByVal strMoneda As String, _
                               ByVal strServicio As String, _
                               ByVal strTipoDocumento As String, _
                               ByVal strNumeroDocumento As String, _
                               ByRef strRespuestaConsulta As String, ByRef strOrigenRpta As String, _
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
                               ByVal numeroMac As String, _
                               ByVal codigoAcreedor As String, _
                               ByVal binAdquirienteRenvio As String, _
                               ByVal codigoFormato As String, _
                               ByVal nombreComercio As String, _
                               ByVal codigoPlazaRecaudador As String, _
                               ByVal medioPagoAuxiliar As String, _
                               ByVal codigoEstadoDeudor As String, _
                               ByVal codigoProcesador As String, _
                               Optional ByVal strCodigoPlazaRecaudador As String = "", _
                               Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                               Optional ByVal strCodigoCiudadRecaudador As String = "", Optional ByRef strFechaHoraTransac As String = "") As Boolean

        ''TODO AGREGADO POR JYMMY TORRES
        ''numeroMac
        ''numeroAcreedor
        ''binAdquirienteRenvio
        ''codigoFormato
        ''nombreComercio
        ''codigoPlazaRecaudador
        ''medioPagoAuxiliar
        ''codigoEstadoDeudor
        ''codigoProcesador

        '---CLIENTE CORPORATIVO PAGO
        Dim strIdentifyLog As String = strTipoIdentificadorDeudor & "|" & strNumeroIdentificadorDeudor
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio PagarReciboST")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

        'Dim objComponente
        Dim strTrace As String
        Dim strRpta As String
        Dim strDocsAux As String

        Dim strCodServicio As String
        Dim strDesServicio As String
        Dim strMonServicio As String
        Dim strTipoDoc As String = ConfigurationSettings.AppSettings("CONST_PRODUCTOMOVIL")
        Dim strNumDoc As String
        Dim strFechaEmi As String
        Dim strFechaVen As String
        Dim strMontoDoc As String
        Dim strLinDocs As String = ""

        Dim intNumDocs As Integer
        'Dim clsSap As New SAP_SIC_Recaudacion.clsRecaudacion
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        'Dim intSAP = objOffline.Get_ConsultaSAP
        Dim clsSap As COM_SIC_OffLine.clsOffline

        'If intSAP = 1 Then
        '    clsSap = New SAP_SIC_Recaudacion.clsRecaudacion
        'Else
        clsSap = New COM_SIC_OffLine.clsOffline
        'End If
        Dim sapOperations = New SAP_SIC_Recaudacion.clsRecaudacion
        Dim strIngreso As String
        Dim lngLog As Long
        Dim strLinDocsLog As String = ""
        Dim strDocumentosLog As String = ""

        Dim strFecha As String
        Dim strHora As String
        Dim strDescripcionRptaAux As String

        Dim pagoEnvio As New PagoRequest
        Dim pagoRespuesta As New PagoResponse

        Try
            'objComponente = CreateObject("OLCPVUPagos.clsOLCPVUPagos") '''LLAMADO AL ST
            'OBTENER NRO TRACE DE SAP
            'lngLog = objComponente.SetEnv(strLogSET, strNivelLogSet) OBTENER NUMERO DE TRACE
            strIngreso = ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";;" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero

            '''CAMBIADO POR JTN
            strTrace = clsSap.SetLogRecaudacion(strIngreso, "")
            '''CAMBIADO HASTA AQUI

            'CARIAS: Esto se hace por emergencia, no se debe pasar de 1000000 09/02/2007
            'If CDbl(strTrace) > 999999 Then
            'strTrace = Format(CDbl(strTrace) - 999999, "0000000000")
            'End If
            strTrace = Right(strTrace, 6)
            strTrace = "0000" & strTrace
            'CARIAS: fin
            gstrTracePago = strTrace

            'Dim wsPagos As New ServiciosClaro

            Dim itemProducto As New ProductoServicioAPagar
            Dim itemDocumento As New DocumentoAPagar
            Dim listaProductos(0) As ProductoServicioAPagar
            Dim listaDocumento(0) As DocumentoAPagar

			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio crear itemDocumento")
            With itemDocumento
                '.tipoDocumento = strTipoDocumento ''' TIPODOCUMENTO
                '.numeroDocumento = strNumeroDocumento ''' NUMERODOCUMENTO 005004394375
                '.datosDocumento = "" '''FILLER
                ''.codigoConcepto1 = "CONFIGURABLE"
                ''.codigoConcepto2 = "CONFIGURABLE"
                ''.codigoConcepto3 = "CONFIGURABLE"
                ''.codigoConcepto4 = "CONFIGURABLE"
                ''.codigoConcepto5 = "CONFIGURABLE"
                '.importeAPagar = String.Format("{0:F}", dblMontoPagar)

                '.importeDeudaOriginal = String.Format("{0:F}", dblValorTotal) '''dblValorTotal
                '.importeDeudaOriginalSpecified = True
                '.importeAPagarSpecified = True

                '.importeConcepto1 = "0.00"
                '.importeConcepto1Specified = True
                '.importeConcepto2 = "0.00"
                '.importeConcepto2Specified = True
                '.importeConcepto3 = "0.00"
                '.importeConcepto3Specified = True
                '.importeConcepto4 = "0.00"
                '.importeConcepto4Specified = True
                '.importeConcepto5 = "0.00"
                '.importeConcepto5Specified = True


                .importeAPagar = dblMontoPagar
                .importeAPagarSpecified = True
                .importeDeudaOriginal = String.Format("{0:F}", dblValorTotal) '''dblValorTotal
                .importeDeudaOriginalSpecified = True
                .numeroDocumento = strNumeroDocumento
                .tipoDocumento = strTipoDoc
            End With
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin crear itemDocumento")

            listaDocumento(0) = itemDocumento

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio crear itemProducto")
            With itemProducto
                '.codigo = strTipoDocumento 'strCodServicio ''' OK-> SERVICIO
                '.documentos = listaDocumento '''OK
                '.montoTotalAPagar = String.Format("{0:F}", dblMontoPagar) '''OK
                '.montoTotalAPagarSpecified = True
                '.estadoDeudor = codigoEstadoDeudor '"A"
                '.numeroTotalDocumentosAPagar = 1 '''-->SICAR SIEMPRE ENCIA DE 1 EN 1 :F
                '.numeroTotalDocumentosAPagarSpecified = True


                .codigo = strTipoDoc
                .documentos = listaDocumento
                .montoTotalAPagar = String.Format("{0:F}", dblMontoPagar)
                .montoTotalAPagarSpecified = True
                .numeroTotalDocumentosAPagar = 1
                .numeroTotalDocumentosAPagarSpecified = True
            End With
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin crear itemProducto")
            listaProductos(0) = itemProducto

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio crear pagoEnvio")
            With pagoEnvio
                .binAdquiriente = strBinAdquiriente
                .binAdquirienteReenvia = ConfigurationSettings.AppSettings("CONST_BIN_REENVIO")
                .canal = strCanal
                .codigoMoneda = strMoneda
                .fechaCaptura = Date.Now.ToString("yyyy-MM-dd-05:00")
                .fechaCapturaSpecified = True
                .fechaTransaccion = Date.Now.ToString("yyyy-MM-ddTHH:mm:ss.ms-05:00")
                .fechaTransaccionSpecified = True
                .numeroReferencia = ConfigurationSettings.AppSettings("CONST_NUMEROREFERENCIA")
                .numeroTerminal = strBinAdquiriente
                .trace = strTrace
                .numeroComercio = .binAdquiriente
                .acreedor = ConfigurationSettings.AppSettings("CONST_ACREEDOR")
                .codigoMoneda = strMoneda
                .importePago = String.Format("{0:F}", dblMontoPagar)
                .importePagoSpecified = True

                .importePagoEfectivo = String.Format("{0:F}", dblMontoPagar)
                .importePagoEfectivoSpecified = True

                .numeroIdentificacionDeudor = strNumeroIdentificadorDeudor
                .pagoTotal = String.Format("{0:F}", dblMontoPagar)
                .pagoTotalSpecified = True
                .productos = listaProductos
                .tipoIdentificacionDeudor = ConfigurationSettings.AppSettings("CONST_CODIGO_FORMATO")


                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  TerminalID : " & strCodigoPuntoDeVenta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Trace : " & strTrace)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Canal : " & strCanal)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  TipoIdentificacionDeudor : " & strTipoIdentificadorDeudor)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Monto : " & CDbl(dblMontoPagar).ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Moneda : " & strMoneda)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Servicio : " & strServicio)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodigoPlazaRecaudador : " & strCodigoPlazaRecaudador)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodigoAgenciaRecaudador : " & strCodigoAgenciaRecaudador)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodigoCiudadRecaudador : " & strCodigoCiudadRecaudador)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  BinAdquiriente : " & strBinAdquiriente)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodComercio : " & strCodComercio)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  TipoDocumento : " & strTipoDocumento)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  NumeroDocumento : " & strNumeroDocumento)
            End With
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin crear pagoEnvio")

			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio envio pago al ST")

            pagoRespuesta = oSwichTransaccional.pago(pagoEnvio)
			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin envio pago al ST")

            With pagoRespuesta

                strRpta = .codigoRespuesta
                strRespuestaConsulta = strRpta
                strOrigenRpta = .codigoOrigenRespuesta
                strCodigoRpta = .codigoRespuesta
                strDescripcionRpta = .descripcionExtendidaRespuesta

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Pago : " & strRpta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  OrigenRpta : " & strOrigenRpta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CodigoRpta : " & strCodigoRpta)
                'INI PROY-140126
                Dim MaptPath As String
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: PagarReciboST)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  DescripcionRpta : " & strDescripcionRpta & MaptPath)
                'FIN PROY-140126


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

                        'strDocumentos = .Documentos

                        '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CABECERA : " & strNombreDeudor & ";" & strRucDeudor & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & dblValorTotal.ToString() & ";" & intNumeroDocumentos.ToString() & ";" & strFechaHoraTransaccion & ";" & strFechaHoraTransac)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CABECERA : " & ";" & strRucDeudor & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & dblValorTotal.ToString() & ";" & intNumeroDocumentos.ToString() & ";" & strFechaHoraTransaccion & ";" & strFechaHoraTransac)

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

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  RECIBOS : " & strDocumentos)

                        If Trim(strFechaHoraTransaccion) <> "" Then
                            strFecha = Mid(strFechaHoraTransaccion, 3, 2) & "/" & Mid(strFechaHoraTransaccion, 1, 2) & "/" & Year(Now)
                            strHora = Mid(strFechaHoraTransaccion, 5, 2) & ":" & Mid(strFechaHoraTransaccion, 7, 2) & ":" & Mid(strFechaHoraTransaccion, 9, 2)
                        End If

                        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal

                        '''CAMBIADO POR JTN
                        strTrace = clsSap.Set_LogRecaudacion(strIngreso, strDocumentosLog)
                        '''CAMBIADO HASTA AQUI

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

                        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal

                        '''CAMBIADO POR JTN
                        strTrace = clsSap.Set_LogRecaudacion(strIngreso, strDocumentosLog)
                        '''CAMBIADO HASTA AQUI

                        If Trim(strDescripcionRpta) = String.Empty Then
                            strDescripcionRpta = BuscarMensaje(8)
                        End If
                End Select
            End With
            clsSap = Nothing
        Catch webEx As System.Net.WebException
            PagarReciboST = False
            clsSap = Nothing
            pagoRespuesta = Nothing
            pagoEnvio = Nothing
            oSwichTransaccional.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "EXCEPCION: " & webEx.Message)
            Throw webEx
        Catch ex As Exception
            Dim stackTrace As New System.Diagnostics.StackTrace(ex)
            PagarReciboST = False
            clsSap = Nothing
            pagoRespuesta = Nothing
            pagoEnvio = Nothing
            oSwichTransaccional.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "EXCEPCION: " & ex.Message)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "EXCEPCION en linea de codigo : " & stackTrace.GetFrame(0).GetFileLineNumber())
        Finally
            pagoRespuesta = Nothing
            pagoEnvio = Nothing
            oSwichTransaccional.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin PagarReciboST")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try
    End Function


#Region "Llamado antiguo al ST 23"
    Private Function oPagarReciboST_23(ByVal strLogSET As String, ByVal strNivelLogSet As String, ByVal strCodigoPuntoDeVenta As String, _
                            ByVal strCanal As String, ByVal strBinAdquiriente As String, ByVal strCodComercio As String, _
                            ByVal strCodigoCajero As String, ByVal strTipoIdentificadorDeudor As String, ByVal strNumeroIdentificadorDeudor As String, _
                            ByVal strFormaPago As String, ByVal dblMontoPagar As Double, ByVal strMoneda As String, _
                            ByVal strCodigoServicio As String, ByVal strTipoDocumento As String, ByVal strNumeroDocumento As String, _
                            ByRef strRespuestaConsulta As String, ByRef strOrigenRpta As String, ByRef strCodigoRpta As String, _
                            ByRef strDescripcionRpta As String, ByRef strNombreDeudor As String, ByRef strRucDeudor As String, _
                            ByRef strNumeroOperacionCobranza As String, ByRef strNumeroOperacionAcreedor As String, ByRef dblValorTotal As Double, _
                            ByRef intNumeroDocumentos As Integer, ByRef strDocumentos As String, ByRef strFechaHoraTransaccion As String, _
                            Optional ByVal strCodigoPlazaRecaudador As String = "", Optional ByVal strCodigoAgenciaRecaudador As String = "", Optional ByVal strCodigoCiudadRecaudador As String = "", _
                            Optional ByRef strFechaHoraTransac As String = "") As Boolean

        Dim bRespuesta As Boolean = False
        Dim objComponente
        'Dim objComponente2 As ServiciosClaro

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
        Dim strLinDocs As String = String.Empty

        Dim intNumDocs As Integer '//contador

        Dim objOffline As New COM_SIC_OffLine.clsOffline

        'Dim intSAP = objOffline.Get_ConsultaSAP
        Dim clsSap As COM_SIC_OffLine.clsOffline
        'If intSAP = 1 Then
        '    clsSap = New SAP_SIC_Recaudacion.clsRecaudacion
        'Else
        clsSap = New COM_SIC_OffLine.clsOffline
        'End If

        Dim strIngreso As String

        Dim lngLog As Long
        Dim strLinDocsLog As String = String.Empty
        Dim strDocumentosLog As String = String.Empty

        Dim strFecha As String
        Dim strHora As String

        Dim sCodigoFormaPago As String '//para obtner la forma de pago (es uan sola forma de apgo)
        Dim sCodigoVP_Trama As String '//
        Dim sCodigoVP_SGA As String '//
        Dim sNroDocPago As String '//De tarjeta o cheque
        Dim sCodigoBco01 As String
        'INI PROY-140126
        Dim sMarLeft As String = "[" & strArchivoRfp & "] " & Date.Now.ToString("yyyy-MM-dd-hh:mm:ss") & " -" & strNumeroIdentificadorDeudor & "- " & vbTab & vbTab
        'FIN PROY-140126
        sbLineasLog.Append(vbTab & vbTab & "--------------------------------------------------------" & vbCrLf)
        sbLineasLog.Append(sMarLeft & "PagarReciboST_23 (Fijo y Páginas) - Inicio " & vbCrLf) ' //& DateTime.Now.ToShortDateString & " " & DateTime.Now.ToShortTimeString
        sbLineasLog.Append(sMarLeft & "--------------------------------------------------------" & vbCrLf) '?????
        sMarLeft = sMarLeft & vbTab
        Try
            '--obtiene código de forma de pago
            Dim arrDatosFP() As String = strFormaPago.Split(";")
            If arrDatosFP.Length >= 4 Then
                sCodigoFormaPago = arrDatosFP(0)  '///
                If (sCodigoFormaPago.Length = 8) Then 'ABCDMNXY, ABCD -> Codigo SAP, MN -> Codigo SGA y XY -> Código a enviar a Trama
                    sCodigoVP_SGA = sCodigoFormaPago.Substring(4, 2) '//para SGA
                    sCodigoVP_Trama = sCodigoFormaPago.Substring(6, 2) '//para Componente 
                    '--obtiene código a enviar a SAP, reactualiza datos a enviar
                    sCodigoFormaPago = sCodigoFormaPago.Substring(0, 4) '//para SAP
                    arrDatosFP(0) = sCodigoFormaPago
                    strFormaPago = Join(arrDatosFP, ";")
                Else
                    Throw New Exception("Los tamaños de los campos códigos de Formas o Vías de Pago no son correctos.")
                End If
                sNroDocPago = arrDatosFP(2)
                sCodigoBco01 = arrDatosFP(3)
            Else
                Throw New Exception("Los datos de Formas o Vías de Pago para el pago no son correctos.")
            End If
            '----
            objComponente = CreateObject("OLCPVUPagos.clsOLCPVUPagos") '''INSTANCIANDO AL ST
            '''objComponente---> switchTransaccional

            'objComponente2 = New ServiciosClaro

            '---OBTENER NRO TRACE DE SAP            
            lngLog = objComponente.SetEnv(strLogSET, strNivelLogSet)
            '---




            If (lngLog = 0) Then
                strIngreso = ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & CStr(dblMontoPagar) & ";" & strMoneda & ";" & strCodigoServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";;" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero
                '''CAMBIADO POR JTN
                strTrace = clsSap.SetLogRecaudacion(strIngreso, String.Empty)
                '''CAMBIADO HASTA AQUI

                strTrace = Right(strTrace, 6)
                strTrace = "0000" & strTrace
                gstrTracePago = strTrace
                '---
                With objComponente
                    .LimpiarCabeceraPago()
                    '--setea datos de entrada
                    .TerminalID = strCodigoPuntoDeVenta
                    .Trace = strTrace
                    .Canal = strCanal
                    .TipoIdentificacionDeudor = strTipoIdentificadorDeudor
                    .NumeroIdentificacionDeudor = strNumeroIdentificadorDeudor
                    .Moneda = strMoneda
                    .Servicio = strCodigoServicio
                    .CodigoPlazaRecaudador = strCodigoPlazaRecaudador
                    .CodigoAgenciaRecaudador = strCodigoAgenciaRecaudador
                    .CodigoCiudadRecaudador = strCodigoCiudadRecaudador
                    .BinAdquiriente = strBinAdquiriente
                    .CodComercio = strCodComercio
                    .TipoDocumento = strTipoDocumento
                    .NumeroDocumento = strNumeroDocumento
                    .MedioPago = sCodigoVP_Trama
                    .FillerPago = sCodigoVP_SGA

                    sbLineasLog.Append(sMarLeft & "INP MedioPago: " & sCodigoVP_Trama & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP FillerPago: " & sCodigoVP_SGA & vbCrLf)

                    If (sCodigoVP_Trama = .MP_EFECTIVO) Then
                        .Monto = CDbl(dblMontoPagar)
                        sbLineasLog.Append(sMarLeft & "INP Monto: " & CStr(dblMontoPagar) & vbCrLf)
                    ElseIf (sCodigoVP_Trama = .MP_CHEQUE) Then

                        .NroCheque01 = sNroDocPago
                        .BancoGirador01 = sCodigoBco01
                        .MontoCheque01 = dblMontoPagar
                        .TipoMedioPagoCheque01 = "1"

                        .NroCheque02 = String.Empty
                        .BancoGirador02 = String.Empty
                        .MontoCheque02 = 0
                        .TipoMedioPagoCheque02 = "1"

                        .NroCheque03 = String.Empty
                        .BancoGirador03 = String.Empty
                        .MontoCheque03 = 0
                        .TipoMedioPagoCheque03 = "1"

                        '---logs
                        sbLineasLog.Append(sMarLeft & "INP NroCheque01: " & sNroDocPago & vbCrLf)
                        sbLineasLog.Append(sMarLeft & "INP BancoGirador01: " & sCodigoBco01 & vbCrLf)
                        sbLineasLog.Append(sMarLeft & "INP MontoCheque01: " & CStr(dblMontoPagar) & vbCrLf)
                        sbLineasLog.Append(sMarLeft & "INP TipoMedioPagoCheque01: 1" & vbCrLf)

                        sbLineasLog.Append(sMarLeft & "INP NroCheque02: " & vbCrLf)
                        sbLineasLog.Append(sMarLeft & "INP BancoGirador02: " & vbCrLf)
                        sbLineasLog.Append(sMarLeft & "INP MontoCheque02: 0" & vbCrLf)
                        sbLineasLog.Append(sMarLeft & "INP TipoMedioPagoCheque02: 1" & vbCrLf)

                        sbLineasLog.Append(sMarLeft & "INP NroCheque03: " & vbCrLf)
                        sbLineasLog.Append(sMarLeft & "INP BancoGirador03: " & vbCrLf)
                        sbLineasLog.Append(sMarLeft & "INP MontoCheque03: 0" & vbCrLf)
                        sbLineasLog.Append(sMarLeft & "INP TipoMedioPagoCheque03: 1" & vbCrLf)

                    ElseIf ((sCodigoVP_Trama = .MP_TC) Or (sCodigoVP_Trama = .MP_TD) Or (sCodigoVP_Trama = .MP_TV) Or (sCodigoVP_Trama = .MP_CARGO) _
                            Or (sCodigoVP_Trama = .MP_CHE_CARGO)) Then
                        .MontoCuenta = dblMontoPagar
                        sbLineasLog.Append(sMarLeft & "INP MontoCuenta: " & CStr(dblMontoPagar) & vbCrLf)
                    End If
                    '--
                    sbLineasLog.Append(sMarLeft & "INP TerminalID: " & strCodigoPuntoDeVenta & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP Trace: " & strTrace & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP Canal: " & strCanal & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP TipoIdentificacionDeudor: " & strTipoIdentificadorDeudor & vbCrLf)

                    '''sbLineasLog.Append(sMarLeft & "INP NumeroIdentificacionDeudor: " & strNumeroIdentificadorDeudor & vbCrLf)

                    sbLineasLog.Append(sMarLeft & "INP Moneda: " & strMoneda & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP Servicio: " & strCodigoServicio & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP CodigoPlazaRecaudador: " & strCodigoPlazaRecaudador & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP CodigoAgenciaRecaudador: " & strCodigoAgenciaRecaudador & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP CodigoCiudadRecaudador: " & strCodigoCiudadRecaudador & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP BinAdquiriente: " & strBinAdquiriente & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP CodComercio: " & strCodComercio & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP TipoDocumento: " & strTipoDocumento & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP NumeroDocumento: " & strNumeroDocumento & vbCrLf)

                    '---
                    strRpta = .PagoExt() '//ejecuta pago

                    strRespuestaConsulta = strRpta
                    '---
                    strOrigenRpta = .OrigenRpta
                    strCodigoRpta = .CodigoRpta
                    strDescripcionRpta = .DescripcionRpta

                    '---
                    If Trim(strRpta) = "0" Then '//éxito
                        '--recupera datos
                        strNombreDeudor = .NombreDeudor
                        strRucDeudor = .RUCDeudor
                        strNumeroOperacionCobranza = .NumeroOperacionCobranza
                        strNumeroOperacionAcreedor = .NumeroOperacionAcreedor
                        dblValorTotal = .ValorTotal
                        intNumeroDocumentos = .NumeroDocumentos
                        strFechaHoraTransaccion = .FechaHoraTransaccion
                        strFechaHoraTransac = .FechaHoraTransaccion
                        strDocumentos = .Documentos

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

                            If strMontoDoc = String.Empty Then
                                strMontoDoc = "0.00"
                            End If

                            'strMontoDoc = IIf(strMontoDoc = String.Empty, "0.00", strMontoDoc) 'use this

                            '---estructura datos para guardar el logs
                            strLinDocs = strLinDocs & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                            strLinDocsLog = strLinDocsLog & strTrace & ";" & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                        Next

                        strDocumentos = Mid(strLinDocs, 1, Len(strLinDocs) - 1)
                        strDocumentosLog = Mid(strLinDocsLog, 1, Len(strLinDocsLog) - 1)
                        If Trim(strFechaHoraTransaccion) <> "" Then
                            strFecha = Mid(strFechaHoraTransaccion, 3, 2) & "/" & Mid(strFechaHoraTransaccion, 1, 2) & "/" & Year(Now)
                            strHora = Mid(strFechaHoraTransaccion, 5, 2) & ":" & Mid(strFechaHoraTransaccion, 7, 2) & ":" & Mid(strFechaHoraTransaccion, 9, 2)
                        End If

                        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & _
                                CStr(dblMontoPagar) & ";" & strMoneda & ";" & strCodigoServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & _
                                strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & _
                                strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & _
                                strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & CStr(dblValorTotal)

                        '''CAMBIADO POR JTN
                        strTrace = clsSap.SetLogRecaudacion(strIngreso, strDocumentosLog)
                        '''CAMBIADO HASTA AQUI
                        bRespuesta = True
                    Else '//fracazo
                        strNombreDeudor = String.Empty
                        strRucDeudor = String.Empty
                        dblValorTotal = 0
                        strNumeroOperacionCobranza = String.Empty
                        intNumeroDocumentos = 0
                        strDocumentos = String.Empty
                        strFechaHoraTransaccion = String.Empty
                        '---
                        bRespuesta = False

                        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & CStr(dblMontoPagar) & ";" & strMoneda & ";" & strCodigoServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & CStr(dblValorTotal)
                        '''CAMBIADO POR JTN
                        strTrace = clsSap.SetLogRecaudacion(strIngreso, strDocumentosLog)

                        '''
                        '---obtiene respuesta
                        strDescripcionRpta = clsMensajes.DeterminaMensaje(clsMensajes.kGrupo_FijoPaginas, CInt(strCodigoRpta), strDescripcionRpta)
                    End If
                End With
                clsSap = Nothing
            Else
                strRespuestaConsulta = CStr(lngLog)
                strOrigenRpta = objComponente.OrigenRpta
                strCodigoRpta = objComponente.CodigoRpta
                strDescripcionRpta = objComponente.DescripcionRpta
            End If

        Catch ex As Exception
            clsSap = Nothing
            strRespuestaConsulta = "-1000"
            strDescripcionRpta = ex.Message
        Finally
            sbLineasLog.Append(sMarLeft & "OUT strRpta(funcion): " & strRespuestaConsulta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "OUT OrigenRpta: " & strOrigenRpta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "OUT CodigoRpta: " & strCodigoRpta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "OUT DescripcionRpta: " & strDescripcionRpta & vbCrLf)
            sMarLeft = sMarLeft.Substring(0, sMarLeft.Length - 1)
            sbLineasLog.Append(sMarLeft & "---------------------------------------------------------------" & vbCrLf)
            sbLineasLog.Append(sMarLeft & "PagarReciboST_23 - Fin" & vbCrLf)
            sbLineasLog.Append(sMarLeft & "---------------------------------------------------------------" & vbCrLf)
        End Try
        '---
        Return bRespuesta
    End Function
#End Region

    '*****E75810
    '''DEMASIADOS CALLBACKS
    '''CAMBIADO A WS JTN
#Region "Web Service Swich Transaccional"
    Private Function PagarReciboST_23(ByVal strLogSET As String, ByVal strNivelLogSet As String, ByVal strCodigoPuntoDeVenta As String, _
                                        ByVal strCanal As String, ByVal strBinAdquiriente As String, ByVal strCodComercio As String, _
                                        ByVal strCodigoCajero As String, ByVal strTipoIdentificadorDeudor As String, ByVal strNumeroIdentificadorDeudor As String, _
                                        ByVal strFormaPago As String, ByVal dblMontoPagar As Double, ByVal strMoneda As String, _
                                        ByVal strCodigoServicio As String, ByVal strTipoDocumento As String, ByVal strNumeroDocumento As String, _
                                        ByRef strRespuestaConsulta As String, ByRef strOrigenRpta As String, ByRef strCodigoRpta As String, _
                                        ByRef strDescripcionRpta As String, ByRef strNombreDeudor As String, ByRef strRucDeudor As String, _
                                        ByRef strNumeroOperacionCobranza As String, ByRef strNumeroOperacionAcreedor As String, ByRef dblValorTotal As Double, _
                                        ByRef intNumeroDocumentos As Integer, ByRef strDocumentos As String, ByRef strFechaHoraTransaccion As String, _
                                        Optional ByVal strCodigoPlazaRecaudador As String = "", Optional ByVal strCodigoAgenciaRecaudador As String = "", Optional ByVal strCodigoCiudadRecaudador As String = "", _
                                        Optional ByRef strFechaHoraTransac As String = "") As Boolean

        Dim bRespuesta As Boolean = False
        '''Dim objComponente'''YA NO SE USARA
        'Dim swichTransaccional As ServiciosClaro

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
        Dim strLinDocs As String = String.Empty

        Dim intNumDocs As Integer '//contador

        Dim objOffline As New COM_SIC_OffLine.clsOffline

        'Dim intSAP = objOffline.Get_ConsultaSAP
        Dim clsSap As COM_SIC_OffLine.clsOffline
        'If intSAP = 1 Then
        '    clsSap = New SAP_SIC_Recaudacion.clsRecaudacion
        'Else
        clsSap = New COM_SIC_OffLine.clsOffline
        'End If

        Dim strIngreso As String

        Dim lngLog As Long
        Dim strLinDocsLog As String = String.Empty
        Dim strDocumentosLog As String = String.Empty

        Dim strFecha As String
        Dim strHora As String

        Dim sCodigoFormaPago As String '//para obtner la forma de pago (es uan sola forma de apgo)
        Dim sCodigoVP_Trama As String '//
        Dim sCodigoVP_SGA As String '//
        Dim sNroDocPago As String '//De tarjeta o cheque
        Dim sCodigoBco01 As String
        'INI PROY-140126
        Dim sMarLeft As String = "[" & strArchivoRfp & "] " & Date.Now.ToString("yyyy-MM-dd-hh:mm:ss") & " -" & strNumeroIdentificadorDeudor & "- " & vbTab & vbTab
        'FIN PROY-140126
        sbLineasLog.Append(vbTab & vbTab & "--------------------------------------------------------" & vbCrLf)
        sbLineasLog.Append(sMarLeft & "PagarReciboST_23 (Fijo y Páginas) - Inicio " & vbCrLf) ' //& DateTime.Now.ToShortDateString & " " & DateTime.Now.ToShortTimeString
        sbLineasLog.Append(sMarLeft & "--------------------------------------------------------" & vbCrLf) '?????
        sMarLeft = sMarLeft & vbTab

        Dim pagoEnvio As New PagoRequest
        Dim pagoRespuesta As New PagoResponse

        Try
            '--obtiene código de forma de pago
            Dim arrDatosFP() As String = strFormaPago.Split(";")
            If arrDatosFP.Length >= 4 Then
                sCodigoFormaPago = arrDatosFP(0)  '///
                If (sCodigoFormaPago.Length = 8) Then 'ABCDMNXY, ABCD -> Codigo SAP, MN -> Codigo SGA y XY -> Código a enviar a Trama
                    sCodigoVP_SGA = sCodigoFormaPago.Substring(4, 2) '//para SGA
                    sCodigoVP_Trama = sCodigoFormaPago.Substring(6, 2) '//para Componente 
                    '--obtiene código a enviar a SAP, reactualiza datos a enviar
                    sCodigoFormaPago = sCodigoFormaPago.Substring(0, 4) '//para SAP
                    arrDatosFP(0) = sCodigoFormaPago
                    strFormaPago = Join(arrDatosFP, ";")
                Else
                    Throw New Exception("Los tamaños de los campos códigos de Formas o Vías de Pago no son correctos.")
                End If
                sNroDocPago = arrDatosFP(2)
                sCodigoBco01 = arrDatosFP(3)
            Else
                Throw New Exception("Los datos de Formas o Vías de Pago para el pago no son correctos.")
            End If
            'swichTransaccional = New ServiciosClaro
            strIngreso = ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & CStr(dblMontoPagar) & ";" & strMoneda & ";" & strCodigoServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";;" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero
            strTrace = clsSap.SetLogRecaudacion(strIngreso, String.Empty)

            strTrace = Right(strTrace, 6)
            strTrace = "000000" & strTrace
            strTrace = strTrace.Substring(strTrace.Length - 6, 6)
            gstrTracePago = strTrace

            Dim itemProducto As New ProductoServicioAPagar
            Dim itemDocumento As New DocumentoAPagar

            Dim documentosProducto(0) As DocumentoAPagar
            Dim productosPago(0) As ProductoServicioAPagar

            With itemDocumento
                .tipoDocumento = strTipoDocumento ''' TIPODOCUMENTO
                .numeroDocumento = strNumeroDocumento ''' NUMERODOCUMENTO 005004394375
                .datosDocumento = sCodigoVP_SGA '''FILLER
                '.codigoConcepto1 = "CONFIGURABLE"
                '.codigoConcepto2 = "CONFIGURABLE"
                '.codigoConcepto3 = "CONFIGURABLE"
                '.codigoConcepto4 = "CONFIGURABLE"
                '.codigoConcepto5 = "CONFIGURABLE"
                .importeAPagar = String.Format("{0:F}", dblMontoPagar)

                .importeDeudaOriginal = String.Format("{0:F}", dblValorTotal) '''dblValorTotal
                .importeDeudaOriginalSpecified = True
                .importeAPagarSpecified = True

                .importeConcepto1 = "0.00"
                .importeConcepto1Specified = True
                .importeConcepto2 = "0.00"
                .importeConcepto2Specified = True
                .importeConcepto3 = "0.00"
                .importeConcepto3Specified = True
                .importeConcepto4 = "0.00"
                .importeConcepto4Specified = True
                .importeConcepto5 = "0.00"
                .importeConcepto5Specified = True
            End With
            documentosProducto(0) = itemDocumento

            With itemProducto
                .codigo = strCodigoServicio ''' OK-> SERVICIO
                .documentos = documentosProducto '''OK
                .montoTotalAPagar = String.Format("{0:F}", dblMontoPagar) '''OK
                .montoTotalAPagarSpecified = True
                .estadoDeudor = "A"
                .numeroTotalDocumentosAPagar = 1 '''-->SICAR SIEMPRE ENCIA DE 1 EN 1 :F
                .numeroTotalDocumentosAPagarSpecified = True
            End With
            productosPago(0) = itemProducto

            Dim codpagoDolares = ConfigurationSettings.AppSettings("constCodigoServFactDolares")

            With pagoEnvio
                .mac = ConfigurationSettings.AppSettings("CONST_MAC")
                .acreedor = ConfigurationSettings.AppSettings("CONST_ACREEDOR")
                '.agencia = "CH01"
                .agencia = strCodigoAgenciaRecaudador '''OK
                .binAdquiriente = CStr(IIf(strBinAdquiriente = "", "0006", strBinAdquiriente)) '''OK 0006 HARCODEADO PARA PRUEBAS
                .binAdquirienteReenvia = ConfigurationSettings.AppSettings("cteCODIGO_BINADQUIRIENTE") ' "620700" '''620700 DEBE SER CONFIGURABLE   strCodigoAgenciaRecaudador
                .canal = strCanal '''OK
                .ciudad = strCodigoCiudadRecaudador ''' OK
                .codigoMedioPago = sCodigoVP_Trama ''' MEDIOPAGO
                .codigoMoneda = IIf(strCodigoServicio = codpagoDolares, ConfigurationSettings.AppSettings("constMONCodigoDolares"), strMoneda) '''OK
                .codigoFormato = "1"
                .fechaCaptura = Date.Now.ToString("yyyy-MM-dd-05:00")
                .fechaTransaccion = Date.Now.ToString("yyyy-MM-ddTHH:mm:ss.ms-05:00")
                .fechaTransaccionSpecified = True
                .fechaCapturaSpecified = True
                .importePago = String.Format("{0:F}", dblMontoPagar)
                .importePagoSpecified = True
                '.importePagoEfectivo = String.Format("{0:F}", dblMontoPagar)
                '.importePagoEfectivoSpecified = True
                '.medioPagoAuxiliar = "07"
                '.nombreComercio = "000000000000666"
                .numeroComercio = .binAdquiriente
                .numeroComercio = strCodComercio ''' OK
                .numeroIdentificacionDeudor = strNumeroIdentificadorDeudor '''OK
                .numeroReferencia = ConfigurationSettings.AppSettings("CONST_NUMEROREFERENCIA")
                .numeroTerminal = strCodigoPuntoDeVenta '''OK
                .pagoTotal = String.Format("{0:F}", dblMontoPagar) '''OK
                .plaza = strCodigoPlazaRecaudador '''OK
                .trace = strTrace '''OK
                .procesador = ConfigurationSettings.AppSettings("CONST_PROCESADOR")
                .numeroDocumentosPagados = 1 '''VALORES NO INCUIDOS EN LA TRAMA DE PAGO
                .numeroProductosPagados = 1 '''VALORES NO INCUIDOS EN LA TRAMA DE PAGO
                .productos = productosPago
                '.fechaExpiracionTarjeta = Date.Now.ToString("yyyyMM")
                .tipoIdentificacionDeudor = strTipoIdentificadorDeudor '''OK/'''VALORES NO INCUIIDOS EN LA TRAMA DE PAGO
                .codigoMonedaPago = strMoneda '''VALORES NO INCUIDOS EN LA TRAMA DE PAGO
                .pagoTotalSpecified = True


                .codigoMedioPago = sCodigoVP_Trama

                sbLineasLog.Append(sMarLeft & "INP MedioPago: " & sCodigoVP_Trama & vbCrLf)
                sbLineasLog.Append(sMarLeft & "INP FillerPago: " & sCodigoVP_SGA & vbCrLf)
                '''REVISAR TABLA DE TIPO DE PAGO
                If (sCodigoVP_Trama = TIPO_PAGO_EFECTIVO) Then
                    .importePagoEfectivo = String.Format("{0:F}", dblMontoPagar)
                    .importePagoEfectivoSpecified = True
                ElseIf (sCodigoVP_Trama = TIPO_PAGO_CHEQUE) Then
                    .numeroCheque1 = sNroDocPago
                    .bancoGiradorCheque1 = sCodigoBco01
                    .importeCheque1 = String.Format("{0:F}", dblMontoPagar)
                    .importeCheque1Specified = True
                    .plazaBancoCheque1 = "1"

                    .numeroCheque2 = String.Empty
                    .bancoGiradorCheque2 = String.Empty
                    .importeCheque2 = 0
                    .plazaBancoCheque2 = "1"

                    .numeroCheque3 = String.Empty
                    .bancoGiradorCheque3 = String.Empty
                    .importeCheque3 = 0
                    .plazaBancoCheque3 = "1"

                    '.importePagoCargoCuenta = String.Format("{0:F}", dblMontoPagar)

                    '---logs
                    sbLineasLog.Append(sMarLeft & "INP NroCheque01: " & sNroDocPago & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP BancoGirador01: " & sCodigoBco01 & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP MontoCheque01: " & CStr(dblMontoPagar) & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP TipoMedioPagoCheque01: 1" & vbCrLf)

                    sbLineasLog.Append(sMarLeft & "INP NroCheque02: " & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP BancoGirador02: " & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP MontoCheque02: 0" & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP TipoMedioPagoCheque02: 1" & vbCrLf)

                    sbLineasLog.Append(sMarLeft & "INP NroCheque03: " & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP BancoGirador03: " & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP MontoCheque03: 0" & vbCrLf)
                    sbLineasLog.Append(sMarLeft & "INP TipoMedioPagoCheque03: 1" & vbCrLf)

                    '''AGREGADO POR JYMMY TORRES
                    '''CAMBIADO 08032014 JYMMY TORRES
                ElseIf sCodigoVP_Trama = TIPO_PAGO_TARJETA Or sCodigoVP_Trama = TIPO_PAGO_TARJETADEBIO Then
                    '.numeroTarjeta = sNroDocPago
                    .importePagoCargoCuenta = String.Format("{0:F}", dblMontoPagar)
                    .importePagoCargoCuentaSpecified = True
                    .importePagoEfectivoSpecified = False
                    '.importePagoCargoCuenta = dblMontoPagar
                    '''AGREGADO HASTA AQUI

                ElseIf ((sCodigoVP_Trama = "MP_TC") Or (sCodigoVP_Trama = "MP_TD") Or _
                        (sCodigoVP_Trama = "MP_TV") Or (sCodigoVP_Trama = "MP_CARGO") Or _
                        (sCodigoVP_Trama = "MP_CHE_CARGO")) Then
                    '.numeroTarjeta = sNroDocPago
                    .importePagoCargoCuenta = dblMontoPagar
                    sbLineasLog.Append(sMarLeft & "INP MontoCuenta: " & CStr(dblMontoPagar) & vbCrLf)
                End If
            End With
            '--
            sbLineasLog.Append(sMarLeft & "INP TerminalID: " & strCodigoPuntoDeVenta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP Trace: " & strTrace & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP Canal: " & strCanal & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP TipoIdentificacionDeudor: " & strTipoIdentificadorDeudor & vbCrLf)

            '''sbLineasLog.Append(sMarLeft & "INP NumeroIdentificacionDeudor: " & strNumeroIdentificadorDeudor & vbCrLf)

            sbLineasLog.Append(sMarLeft & "INP Moneda: " & strMoneda & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP Servicio: " & strCodigoServicio & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP CodigoPlazaRecaudador: " & strCodigoPlazaRecaudador & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP CodigoAgenciaRecaudador: " & strCodigoAgenciaRecaudador & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP CodigoCiudadRecaudador: " & strCodigoCiudadRecaudador & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP BinAdquiriente: " & strBinAdquiriente & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP CodComercio: " & strCodComercio & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP TipoDocumento: " & strTipoDocumento & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP NumeroDocumento: " & strNumeroDocumento & vbCrLf)

            'strRpta = .PagoExt() '//ejecuta pago
            pagoRespuesta = oSwichTransaccional.pago(pagoEnvio)
            With pagoRespuesta
                strRpta = pagoRespuesta.codigoRespuesta
                strRespuestaConsulta = strRpta
                strOrigenRpta = .codigoOrigenRespuesta
                strCodigoRpta = .codigoRespuesta
                strDescripcionRpta = .descripcionExtendidaRespuesta

                'strDocumentos = .productos(0).documentos FALTA DEFINIR
                '---
                If Trim(strRpta) = "00" Then '//éxito
                    '--recupera datos
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

                        If strMontoDoc = String.Empty Then
                            strMontoDoc = "0.00"
                        End If
                        'strMontoDoc = IIf(strMontoDoc = String.Empty, "0.00", strMontoDoc) 'use this

                        '---estructura datos para guardar el logs
                        strLinDocs = strLinDocs & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                        strLinDocsLog = strLinDocsLog & strTrace & ";" & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"

                    Next

                    strDocumentos = Mid(strLinDocs, 1, Len(strLinDocs) - 1)
                    strDocumentosLog = Mid(strLinDocsLog, 1, Len(strLinDocsLog) - 1)
                    If Trim(strFechaHoraTransaccion) <> "" Then
                        strFecha = Mid(strFechaHoraTransaccion, 3, 2) & "/" & Mid(strFechaHoraTransaccion, 1, 2) & "/" & Year(Now)
                        strHora = Mid(strFechaHoraTransaccion, 5, 2) & ":" & Mid(strFechaHoraTransaccion, 7, 2) & ":" & Mid(strFechaHoraTransaccion, 9, 2)
                    End If

                    strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & _
                            CStr(dblMontoPagar) & ";" & strMoneda & ";" & strCodigoServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & _
                            strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & _
                            strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & _
                            strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & CStr(dblValorTotal)

                    '''CAMBIADO POR JTN
                    strTrace = clsSap.SetLogRecaudacion(strIngreso, strDocumentosLog)
                    '''CAMBIADO HASTA AQUI
                    bRespuesta = True
                Else '//fracazo
                    strNombreDeudor = String.Empty
                    strRucDeudor = String.Empty
                    dblValorTotal = 0
                    strNumeroOperacionCobranza = String.Empty
                    intNumeroDocumentos = 0
                    strDocumentos = String.Empty
                    strFechaHoraTransaccion = String.Empty
                    '---
                    bRespuesta = False

                    strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & CStr(dblMontoPagar) & ";" & strMoneda & ";" & strCodigoServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & CStr(dblValorTotal)
                    '''CAMBIADO POR JTN
                    strTrace = clsSap.SetLogRecaudacion(strIngreso, strDocumentosLog)
                    '''
                    '---obtiene respuesta
                    strDescripcionRpta = clsMensajes.DeterminaMensaje(clsMensajes.kGrupo_FijoPaginas, CInt(strCodigoRpta), strDescripcionRpta)
                End If
            End With

            'End With
            clsSap = Nothing
            'Else
            '    strRespuestaConsulta = CStr(lngLog)
            '    strOrigenRpta = objComponente.OrigenRpta
            '    strCodigoRpta = objComponente.CodigoRpta
            '    strDescripcionRpta = objComponente.DescripcionRpta
            'End If

        Catch ex As Exception
            clsSap = Nothing
            strRespuestaConsulta = "-1000"
            'strDescripcionRpta = ex.Message
            strDescripcionRpta = BuscarMensaje(9)
            pagoEnvio = Nothing
            pagoRespuesta = Nothing
            oSwichTransaccional.Dispose()
        Finally
            sbLineasLog.Append(sMarLeft & "OUT strRpta(funcion): " & strRespuestaConsulta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "OUT OrigenRpta: " & strOrigenRpta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "OUT CodigoRpta: " & strCodigoRpta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "OUT DescripcionRpta: " & strDescripcionRpta & vbCrLf)
            sMarLeft = sMarLeft.Substring(0, sMarLeft.Length - 1)
            sbLineasLog.Append(sMarLeft & "---------------------------------------------------------------" & vbCrLf)
            sbLineasLog.Append(sMarLeft & "PagarReciboST_23 - Fin" & vbCrLf)
            sbLineasLog.Append(sMarLeft & "---------------------------------------------------------------" & vbCrLf)
            pagoEnvio = Nothing
            pagoRespuesta = Nothing
            oSwichTransaccional.Dispose()
        End Try
        '---
        Return bRespuesta
    End Function

#End Region


    Private Function CheckStr(ByVal sParam As Object)
        Dim salida As String = ""
        If IsNothing(sParam) Or IsDBNull(sParam) Then
            salida = ""
        Else
            salida = sParam.ToString()
        End If

        Return salida.Trim()
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
                strMensaje = "En este momento no podemos procesar su operación. Vuelva a intentar dentro de unos momentos"
        End Select
        Return strMensaje
    End Function

    '-----------------------------------

    Public Function PagarSR(ByVal strLogSET As String, _
                            ByVal strNivelLogSet As String, _
                            ByVal strCodigoPuntoDeVenta As String, _
                            ByVal nombrePuntoDeVenta As String, _
                            ByVal strCanal As String, _
                            ByVal strBinAdquiriente As String, _
                            ByVal strCodComercio As String, _
                            ByVal strCodigoCajero As String, _
                            ByVal nombreCajero As String, _
                            ByVal strTipoIdentificadorDeudor As String, _
                            ByVal strNumeroIdentificadorDeudor As String, _
                            ByVal strFormasPago As String, _
                            ByVal dblMontoTotalPagar As Double, _
                            ByVal strRecibosPagar As String, _
                            ByVal strTraceConsulta As String, _
                            ByVal strDocCliente As String, _
                            ByVal strTipoAlmacen As String, _
                            ByVal intCodAplicacion As String, _
                            ByVal decImpRecPen As Decimal, _
                            ByVal decImpRecUsd As Decimal, _
                            ByVal decVuelto As Decimal, _
                            ByVal numeroMac As String, _
                            ByVal codigoAcreedor As String, _
                            ByVal binAdquirienteRenvio As String, _
                            ByVal codigoFormato As String, _
                            ByVal nombreComercio As String, _
                            ByVal codigoPlazaRecaudador As String, _
                            ByVal medioPagoAuxiliar As String, _
                            ByVal codigoEstadoDeudor As String, _
                            ByVal codigoProcesador As String, _
                            Optional ByVal strCodigoPlazaRecaudador As String = "", _
                            Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                            Optional ByVal strCodigoCiudadRecaudador As String = "", Optional ByRef strFechaHoraTransac As String = "", _
                            Optional ByRef pstrNumeroOperacionCobranza As String = "", Optional ByRef pstrNumeroOperacionAcreedor As String = "") As String

        Dim strResultado As String = PagarRecibosSR(strLogSET, _
                                strNivelLogSet, _
                                strCodigoPuntoDeVenta, _
                                nombrePuntoDeVenta, _
                                strCanal, _
                                strBinAdquiriente, _
                                strCodComercio, _
                                strCodigoCajero, _
                                nombreCajero, _
                                strTipoIdentificadorDeudor, _
                                strNumeroIdentificadorDeudor, _
                                strFormasPago, _
                                dblMontoTotalPagar, _
                                strRecibosPagar, _
                                strTraceConsulta, _
                                strDocCliente, _
                                strTipoAlmacen, _
                                intCodAplicacion, _
                                decImpRecPen, _
                                decImpRecUsd, _
                                decVuelto, _
                                numeroMac, _
                                codigoAcreedor, _
                                binAdquirienteRenvio, _
                                codigoFormato, _
                                nombreComercio, _
                                codigoPlazaRecaudador, _
                                medioPagoAuxiliar, _
                                codigoEstadoDeudor, _
                                codigoProcesador, _
                                strCodigoPlazaRecaudador, _
                                strCodigoAgenciaRecaudador, _
                                strCodigoCiudadRecaudador, strFechaHoraTransac, pstrNumeroOperacionCobranza, pstrNumeroOperacionAcreedor)

        Return strResultado
    End Function



    Private Function PagarRecibosSR(ByVal strLogSET As String, _
                                ByVal strNivelLogSet As String, _
                                ByVal strCodigoPuntoDeVenta As String, _
                                ByVal nombrePuntoDeVenta As String, _
                                ByVal strCanal As String, _
                                ByVal strBinAdquiriente As String, _
                                ByVal strCodComercio As String, _
                                ByVal strCodigoCajero As String, _
                                ByVal nombreCajero As String, _
                                ByVal strTipoIdentificadorDeudor As String, _
                                ByVal strNumeroIdentificadorDeudor As String, _
                                ByVal strFormasPago As String, _
                                ByVal dblMontoTotalPagar As Double, _
                                ByVal strRecibosPagar As String, _
                                ByVal strTraceConsulta As String, _
                                ByVal strDocCliente As String, _
                                ByVal strTipoAlmacen As String, _
                                ByVal intCodAplicacion As String, _
                                ByVal decImpRecPen As Decimal, _
                                ByVal decImpRecUsd As Decimal, _
                                ByVal decVuelto As Decimal, _
                                ByVal numeroMac As String, _
                                ByVal codigoAcreedor As String, _
                                ByVal binAdquirienteRenvio As String, _
                                ByVal codigoFormato As String, _
                                ByVal nombreComercio As String, _
                                ByVal codigoPlazaRecaudador As String, _
                                ByVal medioPagoAuxiliar As String, _
                                ByVal codigoEstadoDeudor As String, _
                                ByVal codigoProcesador As String, _
                                Optional ByVal strCodigoPlazaRecaudador As String = "", _
                                Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                                Optional ByVal strCodigoCiudadRecaudador As String = "", Optional ByRef strFechaHoraTransac As String = "", _
                                Optional ByRef pstrNumeroOperacionCobranza As String = "", Optional ByRef pstrNumeroOperacionAcreedor As String = "") As String


        Dim arrDocs
        Dim arrLinDoc
        Dim intNumDocs As Integer

        Dim arrFormasPago
        Dim arrLinFormasPago
        Dim intNumFormasPago As Integer

        Dim strRespPagoMasivo As String
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
        Dim strNumeroOperacionCobranza As String
        Dim strNumeroOperacionAcreedor As String
        Dim dblValorTotal As Double
        Dim intNumeroDocumentos As Integer
        Dim strDocumentos As String
        Dim strFechaHoraTransaccion As String

        Dim strMoneda As String
        Dim strServicio As String
        Dim strDesServicio As String
        Dim strTipoDocumento As String
        Dim strNumeroDocumento As String
        Dim strImporteRecibo As String
        Dim dblImporteTotalPagado As Double

        'Dim objSAP As New SAP_SIC_Recaudacion.clsRecaudacion
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        'Dim intSAP = objOffline.Get_ConsultaSAP

        '''CAMBIADO BY JYMMYT
        'Dim objSAP As Object
        'If intSAP = 1 Then
        '    objSAP = New SAP_SIC_Recaudacion.clsRecaudacion
        'Else
        '    objSAP = New COM_SIC_OffLine.clsOffline
        'End If
        Dim objSAP As New COM_SIC_OffLine.clsOffline
        '''CAMBIADO HASTA AQUI

        Dim strNumeroTelefono As String
        Dim strNombreDeudorSAP As String
        Dim strRucDeudorSAP As String
        Dim strFechaEmision As String
        Dim strFechaPago As String
        Dim strValorResultado As String
        Dim strRecibosAnular As String
        Dim strAnularPago As String
        Dim clsAnular As New clsAnulaciones


        Dim gstrDeuda As String
        Dim gstrRecibos As String
        Dim gstrPagos As String

        Dim arrSigDoc
        Dim dblMontoPAC As Double

        '  Try
        dblMontoRestante = dblMontoTotalPagar    'dblMontoRestante = 550
        If Trim(strRecibosPagar) <> "" Then
            arrDocs = Split(strRecibosPagar, "|")

            For intNumDocs = 0 To UBound(arrDocs)
                arrLinDoc = Split(arrDocs(intNumDocs), ";")
                'If intNumDocs = UBound(arrDocs) Then
                dblMontoPagar = dblMontoRestante
                'Else
                '        dblMontoPagar = CDbl(Mid(arrLinDoc(7), 1, Len(arrLinDoc(7)) - 2) & "." & Right(arrLinDoc(7), 2)) 'dblMontoPagar = 149.53
                '        If dblMontoRestante < dblMontoPagar Then   ' 455.61 < 295.79 
                '            dblMontoPagar = dblMontoRestante
                '        End If
                'End If

                '    If dblMontoRestante = 0 Then
                '        Exit For
                '    End If

                '    'dblMontoRestante = Decimal.Round((dblMontoRestante - dblMontoPagar), 2)   'dblMontoRestante = 0.47

                '    ' Saltar PAC si el restante no alcanza para cubrirlo
                '    '  If Not (Trim(Left(arrLinDoc(4), 3)) = "PAC" And dblMontoRestante < dblMontoPagar) Then
                '    ' Validacion si se presenta un PAC como siguiente documento y no alcanzaria
                '    If (intNumDocs + 1) <= UBound(arrDocs) Then
                '        arrSigDoc = Split(arrDocs(intNumDocs + 1), ";")
                '        If Left(arrSigDoc(4), 3) = "PAC" Then
                '            dblMontoPAC = CDbl(Mid(arrSigDoc(7), 1, Len(arrSigDoc(7)) - 2) & "." & Right(arrSigDoc(7), 2))
                '            If dblMontoPAC > Decimal.Round((dblMontoRestante - dblMontoPagar), 2) Then
                '                dblMontoPagar += Decimal.Round((dblMontoRestante - dblMontoPagar), 2)
                '            End If
                '        End If
                '    End If
                '    'fin de Validacion
                '    dblMontoRestante = Decimal.Round((dblMontoRestante - dblMontoPagar), 2)   'dblMontoRestante = 0.47


                strServicio = Trim(arrLinDoc(0))     'REC
                strDesServicio = Trim(arrLinDoc(1))  'Recibo Postpago
                strMoneda = arrLinDoc(2)             '604
                strTipoDocumento = arrLinDoc(3)      'REC
                strNumeroDocumento = arrLinDoc(4)    'T001-XXXXXX

                strImporteRecibo = dblMontoTotalPagar 'CDbl(Mid(arrLinDoc(7), 1, Len(arrLinDoc(7)) - 2) & "." & Right(arrLinDoc(7), 2))
                '    strFechaEmision = Mid(arrLinDoc(5), 7, 2) & "/" & Mid(arrLinDoc(5), 5, 2) & "/" & Mid(arrLinDoc(5), 1, 4)

                dblMontoPagar = dblMontoTotalPagar

                If strTipoIdentificadorDeudor = gstrCodTelefono Then
                    strNumeroTelefono = strNumeroIdentificadorDeudor
                End If

                If PagarReciboST(strLogSET, strNivelLogSet, _
                                   strCodigoPuntoDeVenta, _
                                   strCanal, _
                                   strBinAdquiriente, _
                                   strCodComercio, _
                                   strCodigoCajero, _
                                   strTipoIdentificadorDeudor, _
                                   strNumeroIdentificadorDeudor, _
                                   dblMontoPagar, _
                                   strMoneda, _
                                   strServicio, _
                                   strTipoDocumento, _
                                   strNumeroDocumento, _
                                   strRespuestaConsulta, strOrigenRpta, _
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
                                   numeroMac, _
                                   codigoAcreedor, _
                                   binAdquirienteRenvio, _
                                   codigoFormato, _
                                   nombreComercio, _
                                   codigoPlazaRecaudador, _
                                   medioPagoAuxiliar, _
                                   codigoEstadoDeudor, _
                                   codigoProcesador, _
                                   strCodigoPlazaRecaudador, strCodigoAgenciaRecaudador, strCodigoCiudadRecaudador) Then

                    strFechaHoraTransac = strFechaHoraTransaccion
                    pstrNumeroOperacionCobranza = strNumeroOperacionCobranza
                    pstrNumeroOperacionAcreedor = strNumeroOperacionAcreedor

                    strNombreDeudorSAP = strNombreDeudor
                    strRucDeudorSAP = strRucDeudor
                    If Len(Trim(strRucDeudorSAP)) = 0 Then
                        strRucDeudorSAP = "99999999"
                    End If
                    dblImporteTotalPagado = dblImporteTotalPagado + dblValorTotal
                    strFechaPago = Mid(strFechaHoraTransaccion, 1, 8)
                    strFechaPago = Mid(strFechaPago, 3, 2) & "/" & Mid(strFechaPago, 1, 2) & "/" & Year(Now)
                    'ESTRUCTURA (TI_RECIBOS) PARA GRABAR PAGO EN SAP
                    gstrRecibos = gstrRecibos & ";000001;" & strTipoDocumento & ";" & strNumeroDocumento & ";" & strMoneda & ";" & strImporteRecibo & ";" & dblMontoPagar & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strFechaEmision & ";" & strFechaPago & ";;" & gstrTracePago & ";" & strDesServicio & ";" & strFechaHoraTransaccion & ";" & strServicio & "|"
                    strAnularPago = strAnularPago & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";" & gstrTracePago & ";" & strFechaHoraTransaccion & "|"
                End If
                '  End If  'FIN de Filtro de PAC
            Next

            If strAnularPago <> "" Then
                strAnularPago = Mid(strAnularPago, 1, Len(strAnularPago) - 1)
            End If
            'ESTRUCTURA (TI_DEUDA) PARA GRABAR PAGO EN SAP
            gstrDeuda = ";" & strNombreDeudorSAP & ";" & strRucDeudorSAP & ";" & strCodigoPuntoDeVenta & ";" & nombrePuntoDeVenta & ";" & strMoneda & ";" & dblImporteTotalPagado & ";;;" & gstrEstadoPago & ";" & strNumeroTelefono & ";" & strCodigoCajero & ";" & nombreCajero & ";" & strTraceConsulta & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor
            'FORMAS DE PAGO
            If Trim(strFormasPago) <> "" Then
                arrFormasPago = Split(strFormasPago, "|")
                For intNumFormasPago = 0 To UBound(arrFormasPago)
                    arrLinFormasPago = Split(arrFormasPago(intNumFormasPago), ";")
                    'ESTRUCTURA (TI_PAGOS) PARA GRABAR PAGO EN SAP
                    If dblImporteTotalPagado <= CDbl(arrLinFormasPago(1)) Then
                        gstrPagos = gstrPagos & ";;" & arrLinFormasPago(0) & ";" & dblImporteTotalPagado & ";" & arrLinFormasPago(2) & ";" & arrLinFormasPago(3) & ";" & arrLinFormasPago(4) & "|"
                        Exit For
                    Else
                        gstrPagos = gstrPagos & ";;" & arrLinFormasPago(0) & ";" & arrLinFormasPago(1) & ";" & arrLinFormasPago(2) & ";" & arrLinFormasPago(3) & ";" & arrLinFormasPago(4) & "|"
                        dblImporteTotalPagado = dblImporteTotalPagado - CDbl(arrLinFormasPago(1))
                    End If
                Next
            End If

            If Trim(strRespuestaConsulta) = "" Then
                strRespuestaConsulta = "0"
            End If
            If CInt(strRespuestaConsulta) <> 0 Then
                strResultado = strRespuestaConsulta & "@" & strDescripcionRpta & "@"
            Else
                If CInt(strRespuestaConsulta) = 0 Then
                    '''CAMBIADO POR JYMMY
                    'strResultado = objSAP.Set_RegistroDeuda(gstrDeuda, gstrRecibos, gstrPagos, strValorResultado)
                    strResultado = objSAP.SetRegistroDeuda(gstrDeuda, gstrRecibos, gstrPagos, strValorResultado)
                    '''CAMBIADO HASTA AQUI
                Else
                    strResultado = strRespuestaConsulta & "@" & strDescripcionRpta & "@"
                End If
            End If
            '********************************** Log en BD para cuadre de Caja
            GuardarLogCuadreCaja(intCodAplicacion, _
                            strTipoAlmacen, _
                            strCodigoPuntoDeVenta, _
                            strCodigoCajero, _
                            strNumeroTelefono, _
                            strFormasPago, _
                            dblMontoTotalPagar, _
                            strNumeroDocumento, _
                            strDocCliente, _
                            decImpRecPen, _
                            decImpRecUsd, _
                            decVuelto, _
                            strResultado, _
                            strValorResultado)
            '**********************************
            If strValorResultado = "" Then
                strValorResultado = "1"
            End If
            Try
                ' normalmente deberian ser iguales (CDbl(strValorResultado) y CDbl(Split(strResultado, "@")(0)))
                If CDbl(strValorResultado) <> 0 Or CDbl(Split(strResultado, "@")(0)) <> 0 Then
                    strRecibosAnular = clsAnular.Anular(strLogSET, _
                                        strNivelLogSet, _
                                        strCodigoPuntoDeVenta, _
                                        strCanal, _
                                        strBinAdquiriente, _
                                        strCodComercio, _
                                        strCodigoCajero, _
                                        strTipoIdentificadorDeudor, _
                                        strNumeroIdentificadorDeudor, _
                                        strAnularPago, _
                                        strCodigoPlazaRecaudador, _
                                        strCodigoAgenciaRecaudador, _
                                        strCodigoCiudadRecaudador)
                End If
            Catch
                'Pero se ha dado el caso en que llega en blanco para strResultado, asi que solo se pregunta por 1
                If CDbl(strValorResultado) <> 0 Then
                    strRecibosAnular = clsAnular.Anular(strLogSET, _
                                        strNivelLogSet, _
                                        strCodigoPuntoDeVenta, _
                                        strCanal, _
                                        strBinAdquiriente, _
                                        strCodComercio, _
                                        strCodigoCajero, _
                                        strTipoIdentificadorDeudor, _
                                        strNumeroIdentificadorDeudor, _
                                        strAnularPago, _
                                        strCodigoPlazaRecaudador, _
                                        strCodigoAgenciaRecaudador, _
                                        strCodigoCiudadRecaudador)

                End If
            End Try
        End If  'If Trim(strRecibosPagar) <> "" Then
        PagarRecibosSR = strResultado
        'Catch ex As Exception
        '    PagarRecibos = "8@Verifique los datos ingresados. Por favor vuelva a intentar." & "@"
        'End Try
    End Function


    Public Sub GuardarLogCuadreCaja(ByVal intCodAplicacion As Integer, _
                            ByVal strCanal As String, _
                            ByVal strCodigoPuntoDeVenta As String, _
                            ByVal strCodigoCajero As String, _
                            ByVal strNumeroTelefono As String, _
                            ByVal strFormasPago As String, _
                            ByVal dblMontoTotalPagar As Double, _
                            ByVal strNroRecibo As String, _
                            ByVal strDocCliente As String, _
                            ByVal decImpRecPen As Decimal, _
                            ByVal decImpRecUsd As Decimal, _
                            ByVal decVuelto As Decimal, _
                            ByRef strResultado As String, _
                            ByRef strValorResultado As String, Optional ByVal Accion As String = "R")

        Dim strIdentifyLog As String = strNumeroTelefono

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio GuardarLogCuadreCaja")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

        Dim i As Integer
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INP strCanal: " & strCanal)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INP strCodigoPuntoDeVenta: " & strCodigoPuntoDeVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INP intCodAplicacion: " & intCodAplicacion.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INP strCodigoCajero: " & strCodigoCajero)

            '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INP strDocCliente: " & strDocCliente)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INP strNroRecibo: " & strNroRecibo)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INP dblMontoTotalPagar: " & dblMontoTotalPagar.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INP Accion: " & Accion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INP decImpRecPen: " & decImpRecPen.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INP decImpRecUsd: " & decVuelto.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INP decVuelto: " & decVuelto.ToString())

            Dim intOper As Integer = objCajas.FP_Cab_Oper(strCanal, strCodigoPuntoDeVenta, intCodAplicacion, strCodigoCajero, "", _
                                               strDocCliente, "REC", "", strNroRecibo, _
                                               dblMontoTotalPagar, 0, dblMontoTotalPagar, Accion, _
                                               decImpRecPen, decImpRecUsd, decVuelto)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " OUT Accion: " & intOper.ToString)
            objCajas.FP_Det_Oper(intOper, 1, "", "", strNumeroTelefono, 1, 0, 0, 0)

            Dim arrFormasPago() As String = Split(strFormasPago, "|")
            For i = 0 To arrFormasPago.Length - 1
                Dim arrDet() As String = arrFormasPago(i).Split(";")
                If arrDet.Length > 0 Then
                    objCajas.FP_Pag_Oper(intOper, i + 1, arrDet(0), arrDet(2), Double.Parse(arrDet(1)))
                End If
            Next
        Catch ex As Exception
            '  strValorResultado = "1"
            'strResultado = "8:" + ex.Message
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin GuardarLogCuadreCaja")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try

    End Sub

    '//E75810 25/02/2011
    '''PAGA A SAP Y ST AQUI AQUI AQUI =]
    Public Function PagarRecibos_23(ByVal strLogSET As String, _
                    ByVal strNivelLogSet As String, _
                    ByVal strCodigoPuntoDeVenta As String, _
                    ByVal nombrePuntoDeVenta As String, _
                    ByVal strCanal As String, _
                    ByVal strBinAdquiriente As String, _
                    ByVal strCodComercio As String, _
                    ByVal strCodigoCajero As String, _
                    ByVal nombreCajero As String, _
                    ByVal strTipoIdentificadorDeudor As String, _
                    ByVal strNumeroIdentificadorDeudor As String, _
                    ByVal sCodEstadoTransaccion As String, _
                    ByVal strFormaPago As String, _
                    ByVal dblMontoTotalPagar As Double, _
                    ByVal strRecibosPagar As String, _
                    ByVal strTraceConsulta As String, _
                    ByVal strDocCliente As String, _
                    ByVal strTipoAlmacen As String, _
                    ByVal intCodAplicacion As String, _
                    ByVal decImpRecPen As Decimal, _
                    ByVal decImpRecUsd As Decimal, _
                    ByVal decVuelto As Decimal, _
                    ByVal pValorTipoCambioDolares As Decimal, _
                    Optional ByVal strCodigoPlazaRecaudador As String = "", _
                    Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                    Optional ByVal strCodigoCiudadRecaudador As String = "", _
                    Optional ByRef strFechaHoraTransac As String = "", _
                    Optional ByRef pstrNumeroOperacionCobranza As String = "", _
                    Optional ByRef pstrNumeroOperacionAcreedor As String = "") As String
        Dim bSeEjecuto As Boolean = False
        Dim arrDocs
        Dim arrLinDoc
        Dim d As Integer '//contador de documentos

        Dim arrFormasPago
        Dim arrLinFormasPago
        Dim intNumFormasPago As Integer

        Dim strTrace As String
        Dim dblMontoPagar As Double
        Dim dblMontoRestante As Double
        Dim strResultado As String

        Dim strRespuestaConsulta As String
        Dim strOrigenRpta As String
        Dim strCodigoRpta As String
        Dim strDescripcionRpta As String
        Dim strNombreDeudor As String
        Dim strRucDeudor As String
        Dim dblValorTotal As Double
        Dim intNumeroDocumentos As Integer
        Dim strDocumentos As String

        Dim strMoneda As String
        Dim strCodServicio As String
        Dim strDesServicio As String
        Dim strTipoDocumento As String
        Dim strNumeroDocumento As String
        Dim dblImporteRecibo As Double
        Dim dblImporteTotalPagado As Double

        '---crea objeto
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        'Dim intSAP = objOffline.Get_ConsultaSAP
        Dim objSAP As COM_SIC_OffLine.clsOffline

        Dim recad As SAP_SIC_Recaudacion.clsRecaudacion

        'If intSAP = 1 Then
        '    objSAP = New SAP_SIC_Recaudacion.clsRecaudacion
        'Else
        objSAP = New COM_SIC_OffLine.clsOffline
        'End If
        '---
        Dim strNumeroTelefono As String
        Dim strFechaEmision As String
        Dim strFechaPago As String
        Dim strValorResultado As String
        Dim strRecibosAnular As String
        Dim strAnularPago As String
        Dim clsAnular As New clsAnulaciones
        Dim gstrDeuda As String
        Dim gstrRecibos As String
        Dim gstrPagos As String

        sbLineasLog = New StringBuilder '//crea objeto de lineas para los logs

        Dim sCodigoSoles As String = ConfigurationSettings.AppSettings("constMONCodigoSoles")

        If strRecibosPagar <> String.Empty Then '//hay recibos por pagar
            '--
            arrDocs = Split(strRecibosPagar, "|") '//obtiene arreglo de recibos
            '---realiza el pago de cada documento (Ejm: recibo)
            For d = 0 To UBound(arrDocs)
                '--obtiene arreglo con los campos del recibo actual y obtiene cada campo
                arrLinDoc = Split(arrDocs(d), ";")
                '--recupera campos
                strCodServicio = Trim(arrLinDoc(0))
                strDesServicio = Trim(arrLinDoc(1))
                strMoneda = arrLinDoc(2)
                strTipoDocumento = arrLinDoc(3)
                strNumeroDocumento = arrLinDoc(4)
                strFechaEmision = Mid(arrLinDoc(5), 7, 2) & "/" & Mid(arrLinDoc(5), 5, 2) & "/" & Mid(arrLinDoc(5), 1, 4)
                dblImporteRecibo = CDbl(Mid(arrLinDoc(7), 1, Len(arrLinDoc(7)) - 2) & "." & Right(arrLinDoc(7), 2)) '//se usa solo para enviar a SAP
                dblValorTotal = dblImporteRecibo
                dblMontoPagar = CDbl(Mid(arrLinDoc(8), 1, Len(arrLinDoc(8)) - 2) & "." & Right(arrLinDoc(8), 2))

                '---
                strNumeroTelefono = String.Empty '//para SAP
                '''If strTipoIdentificadorDeudor = gstrCodTelefono Then
                '''    strNumeroTelefono = strNumeroIdentificadorDeudor
                '''End If

                '--procesa pago a través del SWICTH, en el campo dblMontoPagar solo el importe a pagar por  el recibo actual, no la deuda
                bSeEjecuto = PagarReciboST_23(strLogSET, strNivelLogSet, strCodigoPuntoDeVenta, strCanal, strBinAdquiriente, _
                                                strCodComercio, strCodigoCajero, strTipoIdentificadorDeudor, strNumeroIdentificadorDeudor, _
                                                strFormaPago, dblMontoPagar, strMoneda, strCodServicio, strTipoDocumento, strNumeroDocumento, _
                                                strRespuestaConsulta, strOrigenRpta, strCodigoRpta, strDescripcionRpta, strNombreDeudor, _
                                                strRucDeudor, pstrNumeroOperacionCobranza, pstrNumeroOperacionAcreedor, dblValorTotal, intNumeroDocumentos, _
                                                strDocumentos, strFechaHoraTransac, strCodigoPlazaRecaudador, strCodigoAgenciaRecaudador, strCodigoCiudadRecaudador)

                'bSeEjecuto = True

                If (bSeEjecuto) Then
                    '----
                    If Len(Trim(strRucDeudor)) = 0 Then
                        strRucDeudor = "99999999"
                    End If
                    '---convierte todo a soles para enviar a SAP
                    If (sCodigoSoles = strMoneda) Then
                        dblImporteTotalPagado = dblImporteTotalPagado + dblValorTotal
                    Else '//es dolares, entocnes convierte a soles
                        dblImporteTotalPagado = dblImporteTotalPagado + dblValorTotal * pValorTipoCambioDolares
                        strMoneda = sCodigoSoles
                        dblMontoPagar = dblMontoPagar * pValorTipoCambioDolares
                        dblImporteRecibo = dblImporteRecibo * pValorTipoCambioDolares
                    End If
                    '---
                    strFechaPago = Mid(strFechaHoraTransac, 1, 8)
                    strFechaPago = Mid(strFechaPago, 3, 2) & "/" & Mid(strFechaPago, 1, 2) & "/" & Year(Now) '????verificar orden de mes

                    '---estructura (TI_RECIBOS) PARA GRABAR PAGO EN SAP
                    gstrRecibos = gstrRecibos & ";000001;" & strTipoDocumento & ";" & strNumeroDocumento & ";" & strMoneda & ";" & _
                        CStr(dblImporteRecibo) & ";" & CStr(dblMontoPagar) & ";" & pstrNumeroOperacionCobranza & ";" & pstrNumeroOperacionAcreedor & ";" & _
                        strFechaEmision & ";" & strFechaPago & ";;" & gstrTracePago & ";" & strDesServicio & ";" & _
                        strFechaHoraTransac & ";" & strCodServicio & "|"
                    '--estructura para el caso de anular
                    strAnularPago = strAnularPago & CStr(dblMontoPagar) & ";" & strMoneda & ";" & strCodServicio & ";" & pstrNumeroOperacionCobranza & ";" & pstrNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";" & gstrTracePago & ";" & strFechaHoraTransac & "|"
                Else '//No se ejecutó, se debe cancelar transaccion 
                    '--setea variables para anular pagos realizados
                    strValorResultado = String.Empty
                    '--sale de bucle para cancelar operacion
                    Exit For
                End If
            Next

            '---
            If strAnularPago <> String.Empty Then
                strAnularPago = Mid(strAnularPago, 1, Len(strAnularPago) - 1)
            End If

            If (bSeEjecuto) Then
                '---Estructura (TI_DEUDA) PARA GRABAR PAGO EN SAP
                gstrDeuda = ";" & strNombreDeudor & ";" & strRucDeudor & ";" & strCodigoPuntoDeVenta & ";" & nombrePuntoDeVenta & ";" & strMoneda & ";" & CStr(dblImporteTotalPagado) & ";;;" & sCodEstadoTransaccion & ";" & strNumeroTelefono & ";" & strCodigoCajero & ";" & nombreCajero & ";" & strTraceConsulta & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor
                '----FORMAS DE PAGO
                Dim sCodigoFP As String = String.Empty
                If Trim(strFormaPago) <> String.Empty Then
                    arrFormasPago = Split(strFormaPago, "|")
                    For intNumFormasPago = 0 To UBound(arrFormasPago)
                        arrLinFormasPago = Split(arrFormasPago(intNumFormasPago), ";")
                        sCodigoFP = arrLinFormasPago(0).Substring(0, 4) '//para quitar el último caracter agregado (MasterCard debito y Credito)
                        '---Estructura (TI_PAGOS) PARA GRABAR PAGO EN SAP
                        arrLinFormasPago(3) = "" ' Para comunicaciones con SAP se envía en blanco el código de banco (AF Jaime Arrizueño)
                        If dblImporteTotalPagado <= CDbl(arrLinFormasPago(1)) Then
                            gstrPagos = gstrPagos & ";;" & sCodigoFP & ";" & dblImporteTotalPagado & ";" & arrLinFormasPago(2) & ";" & arrLinFormasPago(3) & ";" & arrLinFormasPago(4) & "|"
                            Exit For
                        Else
                            gstrPagos = gstrPagos & ";;" & sCodigoFP & ";" & arrLinFormasPago(1) & ";" & arrLinFormasPago(2) & ";" & arrLinFormasPago(3) & ";" & arrLinFormasPago(4) & "|"
                            dblImporteTotalPagado = dblImporteTotalPagado - CDbl(arrLinFormasPago(1))
                        End If
                    Next
                End If
            End If

            '----
            If Trim(strRespuestaConsulta) = String.Empty Then
                strRespuestaConsulta = "0"
            End If
            '--
            '''If (d = 0) And (strRespuestaConsulta <> "0") Then '//NO existen recibos y No fue posible pagar algun recibo
            '''    strResultado = strRespuestaConsulta & "@" & strDescripcionRpta & "@"
            '''Else '//existen recibos o fue posible pagar los recibos
            'INI PROY-140126
            Dim sMarLeft As String = "[" & strArchivoRfp & "] " & Date.Now.ToString("yyyy-MM-dd-hh:mm:ss") & " -" & strNumeroIdentificadorDeudor & "- " & vbTab & vbTab
            'FIN PROY-140126
            sbLineasLog.Append(sMarLeft & " strRespuestaConsulta: " & strRespuestaConsulta & vbCrLf)
            If (strRespuestaConsulta = "00") Then
                'sbLineasLog.Append(sMarLeft & "Inicio Set_RegistroDeuda(Zpvu_Rfc_Trs_Reg_Deuda) " & vbCrLf)
                sbLineasLog.Append(sMarLeft & "Inicio Set_RegistroDeuda " & vbCrLf)
                sbLineasLog.Append(sMarLeft & "Stored Procedure: CONF_Trs_DeudaCabecera" & vbCrLf)
                sbLineasLog.Append(sMarLeft & "Stored Procedure: CONF_Trs_RegistroDeuda" & vbCrLf)
                sbLineasLog.Append(sMarLeft & "Stored Procedure: CONF_Trs_RegistroRecibo" & vbCrLf)
                sbLineasLog.Append(sMarLeft & "Stored Procedure: CONF_Trs_RegistroPagos" & vbCrLf)

                sbLineasLog.Append(sMarLeft & "     INP gstrDeuda: " & gstrDeuda & vbCrLf)
                sbLineasLog.Append(sMarLeft & "     INP gstrRecibos: " & gstrRecibos & vbCrLf)
                sbLineasLog.Append(sMarLeft & "     INP gstrPagos: " & gstrPagos & vbCrLf)
                '''
                '''CAMBIADO POR JTN
                'strResultado = objSAP.Set_RegistroDeuda(gstrDeuda, gstrRecibos, gstrPagos, strValorResultado)
                'gstrPagos = ";;ZEFE;4331.45;;;|" '''COMENTAR PARA PRUEBAS
                strResultado = objSAP.SetRegistroDeuda(gstrDeuda, gstrRecibos, gstrPagos, strValorResultado)
                '''CAMBIADO HASTA AQUI
                '''
                sbLineasLog.Append(sMarLeft & "     OUT strValorResultado: " & strValorResultado & vbCrLf)
                sbLineasLog.Append(sMarLeft & "Fin Set_RegistroDeuda " & vbCrLf)
                bSeEjecuto = bSeEjecuto And (CDbl(strValorResultado) = 0)
            Else
                strResultado = strRespuestaConsulta & "@" & strDescripcionRpta & "@"
            End If
            '''End If

            '********************************** Log en BD para cuadre de Caja
            If (bSeEjecuto) Then '//se ejecutaron con éxito todos los documento de pago
                GuardarLogCuadreCaja(intCodAplicacion, strTipoAlmacen, strCodigoPuntoDeVenta, strCodigoCajero, _
                    strNumeroTelefono, strFormaPago, dblMontoTotalPagar, strNumeroDocumento, _
                    strDocCliente, decImpRecPen, decImpRecUsd, decVuelto, strResultado, strValorResultado)
            End If
            '**********************************

            If strValorResultado = String.Empty Then
                strValorResultado = "1"
            End If

            Try
                ' normalmente deberian ser iguales (CDbl(strValorResultado) y CDbl(Split(strResultado, "@")(0)))
                If ((CDbl(strValorResultado) <> 0 Or CDbl(Split(strResultado, "@")(0)) <> 0) And (d > 0)) Then
                    strRecibosAnular = clsAnular.Anular(strLogSET, strNivelLogSet, strCodigoPuntoDeVenta, strCanal, _
                            strBinAdquiriente, strCodComercio, strCodigoCajero, strTipoIdentificadorDeudor, _
                            strNumeroIdentificadorDeudor, strAnularPago, strCodigoPlazaRecaudador, strCodigoAgenciaRecaudador, _
                            strCodigoCiudadRecaudador)
                End If
            Catch
                'Pero se ha dado el caso en que llega en blanco para strResultado, asi que solo se pregunta por 1
                If CDbl(strValorResultado) <> 0 Then
                    strRecibosAnular = clsAnular.Anular(strLogSET, strNivelLogSet, strCodigoPuntoDeVenta, strCanal, _
                            strBinAdquiriente, strCodComercio, strCodigoCajero, strTipoIdentificadorDeudor, _
                            strNumeroIdentificadorDeudor, strAnularPago, strCodigoPlazaRecaudador, strCodigoAgenciaRecaudador, _
                            strCodigoCiudadRecaudador)
                End If
            End Try
        End If '**Hay recibos
        '--retorna respuesta de la operacion
        Return strResultado
    End Function

#Region "RECAUDACIONES DAC"

    Public Function ConexionWSDAC() As DistribuidoresSAPService
        Dim oSwichTransaccional As New DistribuidoresSAPService
        oSwichTransaccional.Url = CStr(ConfigurationSettings.AppSettings("WSRecaudacionDAC"))
        oSwichTransaccional.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim iTimeOut As Int32 = 0
        Dim strTimeOut As String = ConfigurationSettings.AppSettings("constSICARConsultasST_Timeout")
        If Not strTimeOut Is Nothing Then
            If strTimeOut <> "" Then
                iTimeOut = Convert.ToInt32(strTimeOut)
            End If
        End If
        oSwichTransaccional.Timeout = iTimeOut
        Return oSwichTransaccional
    End Function

    '//-- MODIFICADO: GB 05/2015 => SE AGREGO EL CAMPO TIPO PAGO
    Public Function Set_RecaudacionDAC(ByVal strVersionSap As String, _
                                        ByVal strOficina As String, _
                                        ByVal strOficinaAnt As String, _
                                        ByVal strCliente As String, _
                                        ByVal strNombreCliente As String, _
                                        ByVal strFecha As String, _
                                        ByVal strMonto As String, _
                                        ByVal strNodo As String, _
                                        ByVal strPagos As String, _
                                        ByVal strUsuario As String, _
                                        ByVal strTUser As String, _
                                        ByVal strIpApp As String, _
                                        ByVal strDefinir As String, _
                                        ByVal strNroFiscal As String, _
                                        ByRef strNroAt As String, _
                                        ByVal strTipoPago As String) As String

        Dim strIdentifyLog As String = strCliente

        objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "Inicio Set_RecaudacionDAC - ClsPagos")
        objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "-------------------------------------------------")

        Dim bTodoOK As Boolean = False
        Dim objOffLine As New COM_SIC_OffLine.clsOffline
        Dim strCodigoRpta As String
        Dim strRpta As String
        Dim dsresult As New DataSet
        Dim strNroDoc As String = String.Empty
        Dim strIpAplicacion As String = strIpApp
        Dim strIdTransaccion As String = DateTime.Now.Ticks.ToString().Substring(7, 11)
        Dim strNombreAplicacion As String = ConfigurationSettings.AppSettings("NombreAplicacionDAC")
        Dim strFlag As String = ConfigurationSettings.AppSettings("FlagDac")

        Dim pagarDeudaEnvio As New PagarDeudaRequest
        Dim auditPagarDeudaEnvio As New AuditType
        Dim pagarDeudaRespuesta As New PagarDeudaResponse
        Try
            'ENVIO DE PAGO A SAP
            Dim listaDetallePago(2) As PagoBean
            Dim tramaPago() As String = strPagos.Split(CChar("|"))
            Dim arr() As String

            Dim iRnd As Int32 = Int32.Parse("1" & DateTime.Now.Ticks.ToString().Substring(9, 1) & DateTime.Now.Ticks.ToString().Substring(10, 8))
            Dim rnd As New Random(iRnd)
            Dim strNroRan As String = Convert.ToString(rnd.Next())

            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "Inicio crear detalle Pago DAC")

            For index As Integer = 0 To tramaPago.Length - 1
                If tramaPago(index).Length > 0 Then
                    arr = tramaPago(index).Split(CChar(";"))

                    Dim objDetallePago As New PagoBean
                    objDetallePago.documento = arr(0)
                    objDetallePago.monto = arr(1)
                    objDetallePago.viaPago = arr(2)
                    objDetallePago.banco = arr(3)
                    objDetallePago.fecha = strFecha
                    objDetallePago.nombre = strNombreCliente
                    objDetallePago.numeroCliente = strCliente
                    objDetallePago.numeroIdentificacionFiscal = strNroFiscal
                    objDetallePago.numeroOperacion = strNroRan

                    objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "  DETALLE PAGO ITEM " & index.ToString() & " - " & " documento : " & arr(0).ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "  DETALLE PAGO ITEM " & index.ToString() & " - " & " monto : " & arr(1))
                    objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "  DETALLE PAGO ITEM " & index.ToString() & " - " & " viaPago : " & arr(2))
                    objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "  DETALLE PAGO ITEM " & index.ToString() & " - " & " banco : " & arr(3))
                    objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "  DETALLE PAGO ITEM " & index.ToString() & " - " & " fecha : " & strFecha)
                    objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "  DETALLE PAGO ITEM " & index.ToString() & " - " & " nombre : " & strNombreCliente)
                    objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "  DETALLE PAGO ITEM " & index.ToString() & " - " & " numeroCliente : " & strCliente)
                    objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "  DETALLE PAGO ITEM " & index.ToString() & " - " & " numeroIdentificacionFiscal : " & strNroFiscal)
                    objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "  DETALLE PAGO ITEM " & index.ToString() & " - " & " numeroOperacion : " & strNroRan)

                    listaDetallePago(index) = objDetallePago
                End If
            Next

            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "Inicio crear envio pagarDeudaEnvio")

            With auditPagarDeudaEnvio
                .idTransaccion = strIdTransaccion
                .ipApplicacion = strIpAplicacion
                .nombreAplicacion = strNombreAplicacion
                .usuarioAplicacion = strTUser
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   ---- INICIO ::: AUDITORIA REGISTRAR DEUDA ----")

                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP AUDITORIA_PAGAR_ENVIO_DAC idTransaccion : " & strIdTransaccion)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP AUDITORIA_PAGAR_ENVIO_DAC ipApplicacion : " & strIpAplicacion)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP AUDITORIA_PAGAR_ENVIO_DAC nombreAplicacion : " & strNombreAplicacion)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP AUDITORIA_PAGAR_ENVIO_DAC usuarioAplicacion : " & strTUser)

                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   ---- FIN ::: AUDITORIA REGISTRAR DEUDA ----")
            End With

            With pagarDeudaEnvio
                .auditRequest = auditPagarDeudaEnvio
                .definir = strDefinir
                .oficinaVenta = strOficina
                .usuario = strUsuario
                .version = strVersionSap
                .listaPago = listaDetallePago

                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  strDefinir : " & strDefinir)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  oficinaVenta : " & strOficina)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  usuario : " & strUsuario)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  version sap: " & strVersionSap)
            End With

            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "Fin crear pagarDeudaEnvio")

            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "Inicio envio pago deuda DAC al SAP")
            pagarDeudaRespuesta = ConexionWSDAC().pagarDeuda(pagarDeudaEnvio)
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "Fin envio pago deuda DAC al SAP")

            With pagarDeudaRespuesta
                strCodigoRpta = .auditResponse.codigoRespuesta

                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  CodigoRespuesta: " & strCodigoRpta)
                Select Case Trim(strCodigoRpta)
                    Case 0
                        If Not .listaBapiret2 Is Nothing Then
                            strNroDoc = .numeroAutorizacion
                            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  Numero de Autorizacion : " & strNroDoc)
                            If strNroDoc <> String.Empty Then
                                bTodoOK = True
                                Dim detRpta As Bapiret2Bean()
                                detRpta = .listaBapiret2
                                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  Mensaje WS: " & detRpta(0).message)
                                strRpta &= "C@" & .auditResponse.mensajeRespuesta 'C == CORRECTO
                                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRpta)
                            Else
                                bTodoOK = False
                                Dim detRpta As Bapiret2Bean()
                                strRpta &= "E" 'E == ERROR
                                detRpta = .listaBapiret2
                                strRpta &= "@" & detRpta(0).message & ";" & detRpta(0).type & ";" & detRpta(0).id & ";" & detRpta(0).number
                                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRpta)
                            End If
                        End If
                    Case Else
                        bTodoOK = False
                        strRpta &= "E" 'E == ERROR
                        strRpta &= "@" & "Error en recaudacion de dacs"
                        objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRpta)
                        objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  Respuesta SAP: " & .auditResponse.mensajeRespuesta.ToString())
                End Select
            End With

            'ENVIO DE PAGO A BASE DE DATOS SICAR
            If bTodoOK Then
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & " INICIO :: ENVIO DE PAGO A BASE DE DATOS SICAR")
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  oficinaVenta : " & strOficina)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  oficinaVenta Ant: " & strOficinaAnt)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  codigo cliente : " & strCliente)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  cliente : " & strNombreCliente)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  fecha: " & strFecha)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  Pagos: " & strPagos)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  Usuario: " & strUsuario)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  Monto: " & strMonto)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  Nodo: " & strNodo)

                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  Tuser: " & strTUser)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  Numero Autorizacion: " & strNroDoc)

                '//-- MODIFICADO: GB 05/2015 => SE AGREGO EL CAMPO TIPO PAGO
                dsresult = objOffLine.Set_RecaudacionDAC(strOficinaAnt, strCliente, strNombreCliente, strFecha, strPagos, strUsuario, strMonto, "1", strNodo, strTUser, strNroDoc, strTipoPago)
                '//--

                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & " FIN :: ENVIO DE PAGO A BASE DE DATOS SICAR")
                For i As Integer = 0 To dsresult.Tables(0).Rows.Count - 1
                    If (dsresult.Tables(0).Rows(i).Item("type") = "W" Or dsresult.Tables(0).Rows(i).Item("type") = "E") Then
                        strRpta = dsresult.Tables(0).Rows(i).Item("type") & "@" & "Error en recaudacion de dacs." & dsresult.Tables(0).Rows(i).Item("message")
                        objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "Error Respuesta - BD SICAR: " & strRpta)
                        Exit For
                    End If
                Next
            End If

            Return strRpta
        Catch webEx As System.Net.WebException
            objOffLine = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "EXCEPCION: " & webEx.Message)
            Throw webEx
        Catch ex As Exception
            Dim stackTrace As New System.Diagnostics.StackTrace(ex)
            objOffLine = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "EXCEPCION: " & ex.Message)
        Finally
            ConexionWSDAC().Dispose()
            pagarDeudaEnvio = Nothing
            auditPagarDeudaEnvio = Nothing
            pagarDeudaRespuesta = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "Fin Set_RecaudacionDAC - ClsPagos")
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try
    End Function

#End Region


    '//-- GB 05/2015
#Region "RECAUDACIONES DRA"

    Public Function ConexionWSDRA() As TransaccionPagoDRA
        Dim oSwichTransaccional As New TransaccionPagoDRA
        oSwichTransaccional.Url = CStr(ConfigurationSettings.AppSettings("WSPagosDRA"))
        oSwichTransaccional.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim iTimeOut As Int32 = 0
        Dim strTimeOut As String = ConfigurationSettings.AppSettings("constSICARConsultasST_Timeout")
        If Not strTimeOut Is Nothing Then
            If strTimeOut <> "" Then
                iTimeOut = Convert.ToInt32(strTimeOut)
            End If
        End If
        oSwichTransaccional.Timeout = iTimeOut
        Return oSwichTransaccional
    End Function

    ' CREAR PAGO
    Public Function Set_RecaudacionDRA(ByVal strCodPagoDra As String, _
                                       ByVal strTipoDoc As String, _
                                       ByVal strNroDoc As String, _
                                       ByVal strDescripDoc As String, _
                                       ByVal strImportePago As String, _
                                       ByVal strTraceID As String, _
                                       ByVal strCodPDV As String, _
                                       ByVal strListaMedioPago As String, _
                                       ByVal strImporteTotalPago As Decimal, _
                                       ByRef strNroOpePagoSISACT As String, _
                                       ByRef strCodRespuesta As String, _
                                       ByRef strMsgRespuesta As String, _
                                       ByVal strTipoPago As String, _
                                       ByVal strOficinaAnt As String, _
                                       ByVal strCliente As String, _
                                       ByVal strNombreCliente As String, _
                                       ByVal strFecha As String, _
                                       ByVal strPagos As String, _
                                       ByVal strUsuario As String, _
                                       ByVal strMonto As String, _
                                       ByVal strNodo As String, _
                                       ByVal strTUser As String, _
                                       ByVal strTrama As String, _
                                       ByVal strCodOficinaNuevo As String) As String

        Dim strIdentifyLog As String = strCodPagoDra
        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Inicio Set_RecaudacionDRA - ClsPagos")
        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")

        Dim dsresult As New DataSet
        Dim objOffLine As New COM_SIC_OffLine.clsOffline
        Dim txtID As String
        Dim strCodigoAplicacion As String = ConfigurationSettings.AppSettings("CodigoAplicacionDRA")
        Dim strUsuarioAplicacion As String = ConfigurationSettings.AppSettings("UsuarioAplicacionDRA")
        Dim strCodMoneda As String = ConfigurationSettings.AppSettings("CodMonSoles")
        Dim strCodBanco As String = ConfigurationSettings.AppSettings("CodBancoSICAR")
        Dim strFechaPago As String = Date.Now.ToString("yyyy-MM-ddTHH:mm:ss.ms-05:00")
        Dim strFlag As String = ConfigurationSettings.AppSettings("FlagDra")
        Dim strRpta As String
        Dim strNroPedido As String
        Dim strFechaNow As String = Date.Now.ToString("dd/MM/yyyy")

        'Dim iRnd As Int32 = Int32.Parse("1" & DateTime.Now.Ticks.ToString().Substring(9, 1) & DateTime.Now.Ticks.ToString().Substring(10, 8))
        'Dim rnd As New Random(iRnd)
        'Dim strNroRan As String = Convert.ToString(rnd.Next())
        Dim strNroOperacionPago As String
        Dim strNombreCaja As String
        Dim strNroCaja As String
        Dim dsResultado As DataSet
        Dim K_NROLOG As String = ""
        Dim K_DESLOG As String = ""
        Dim K_NROLOG_DET As String = ""
        Dim K_DESLOG_DET As String = ""
        Dim K_PAGON_IDPAGO As Int64 = 0
        Dim K_PAGOC_CORRELATIVO As String = ""
        Dim codUsuario As String
        Dim strError As String = ""
        Dim msgj As String = ""

        Try
            strNroOperacionPago = objOffLine.GetNroPagoDra()
            strTraceID = strNroOperacionPago

            Dim nroPedidoMsSap As String = String.Empty
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  Inicio : GetCodigoPedidoMsSap")
            nroPedidoMsSap = objOffLine.GetCodigoPedidoMsSap(strNroDoc)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Nro Pedido MSSAP :" & nroPedidoMsSap)
            If nroPedidoMsSap.Equals(String.Empty) Then
                strCodRespuesta = "1"
                objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  ERROR al obtener el nro de Pedido MSSAP :" & nroPedidoMsSap)
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  Fin : GetCodigoPedidoMsSap")

            'ENVIO DE PAGO A SAP
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Inicio crear detalle Pago DRA")

            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " CodigoAplicacion : " & strCodigoAplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " UsuarioAplicacion : " & strUsuarioAplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " CodPagoDra : " & strCodPagoDra)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " TipoDoc : " & strTipoDoc)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " NroDoc : " & strNroDoc)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " DescripDoc : " & strDescripDoc)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " CodMoneda : " & strCodMoneda)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " ImportePago : " & strImportePago)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " FechaPago : " & strFechaPago)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " TraceID : " & strTraceID)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " NroOperacionPago : " & strNroOperacionPago)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " CodBanco : " & strCodBanco)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " CodPDV : " & strCodPDV)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " ListaMedioPago : " & strListaMedioPago)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " ImporteTotalPago : " & strImporteTotalPago.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE PAGO " & " - " & " NroOpePagoSISACT : " & strNroOpePagoSISACT)

            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Inicio envio pago deuda DRA al ST")
            strNroPedido = ConexionWSDRA().crearPagoDRA(txtID, strCodigoAplicacion, strUsuarioAplicacion, strCodPagoDra, strTipoDoc, _
                                           strNroDoc, strDescripDoc, strCodMoneda, strImportePago, strFechaPago, strTraceID, _
                                           strNroOperacionPago, strCodBanco, strCodPDV, strListaMedioPago, strImporteTotalPago, _
                                           strCodRespuesta, strMsgRespuesta)

            Select Case Trim(strCodRespuesta)
                Case 0
                    strRpta = "Se realizó el Pago. " & " | " & strMsgRespuesta
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & strRpta)

                    'Agregado por TS-CCC: 05.06.2015
                    Dim strCodMSSAP As String, strRptaMSSAP As String
                    objOffLine.RegistrarDRAPoolDocPagados(nroPedidoMsSap, strCodOficinaNuevo, strCodMSSAP, strRptaMSSAP)

                    If Not strCodMSSAP.Equals("0") Then
                        strCodRespuesta = "1"
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  CODIGO ERROR al registra pago DRA :" & strCodMSSAP)
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  MENSAJE ERROR al registra pago DRA :" & strRptaMSSAP)
                    End If

                    'Agregado por TS-CCC: 20.07.2015
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Parametros de Entrada AsignarPagoAcuerdosXDocSap PAgo Exitoso ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in strDocSap: " & nroPedidoMsSap)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in Referencia: " & strNroDoc)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in mporte: " & strImportePago)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in Current user: " & strUsuario)

                    objOffLine.AsignarPagoAcuerdosXDocSap(nroPedidoMsSap, strNroDoc, CheckDbl(strImportePago), strUsuario, msgj)

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "out msg: " & msgj)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " FIN Parametros de Entrada AsignarPagoAcuerdosXDocSap PAgo Exitoso ")

                    codUsuario = Convert.ToString(strUsuario).PadLeft(10, CChar("0"))
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  Inicio - GetNombreCajaAsignada")
                    dsResultado = objOffLine.GetNombreCajaAsignada(strCodPDV, strFechaNow, codUsuario)
                    If Not dsResultado Is Nothing Then
                        For i As Int32 = 0 To dsResultado.Tables(0).Rows.Count - 1
                            strNombreCaja = dsResultado.Tables(0).Rows(i).Item("NOMBRECAJA")
                            strNroCaja = dsResultado.Tables(0).Rows(i).Item("NROCAJA")
                            Exit For
                        Next
                    End If

                    If strNombreCaja Is Nothing Then
                        strNombreCaja = "CAJA PRUEBA"
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  Nombre de Caja: " & strNombreCaja)
                    End If
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  Fin - GetNombreCajaAsignada")

                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  Inicio del registro del pago - MSSAP")
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN Nro Pedido: " & nroPedidoMsSap)
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN Estado: " & System.Configuration.ConfigurationSettings.AppSettings("ESTADO_PAG"))
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN Nombre Caja: " & strNombreCaja)
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN Nro Caja: " & strNroCaja)
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN Moneda: " & System.Configuration.ConfigurationSettings.AppSettings("MONEDA"))
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN usuario: " & strUsuario)

                    strError = objOffLine.RegistrarPago(nroPedidoMsSap, DBNull.Value, _
                                             System.Configuration.ConfigurationSettings.AppSettings("ESTADO_PAG"), _
                                             strNombreCaja, _
                                             strNroCaja, _
                                             System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                             strUsuario, _
                                             DBNull.Value, _
                                             strUsuario, _
                                             DBNull.Value, _
                                             K_NROLOG, K_DESLOG, _
                                             K_PAGON_IDPAGO, K_PAGOC_CORRELATIVO)

                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  OUT K_NROLOG: " & K_NROLOG)
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  OUT K_DESLOG: " & K_DESLOG)
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  OUT K_PAGON_IDPAGO: " & K_PAGON_IDPAGO)
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  OUT K_PAGOC_CORRELATIVO: " & K_PAGOC_CORRELATIVO)
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Fin del registro del pago - MSSAP")

                    If K_DESLOG = "OK" Then
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  Id Pago :" & K_PAGON_IDPAGO)
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  Inicio del detalle del pago - MSSAP")
                        Dim tramaPago() As String = strTrama.Split(CChar("|"))
                        Dim arr() As String
                        For i As Integer = 0 To tramaPago.Length - 1
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  Trama " & i.ToString() & " : " & tramaPago(i))
                            arr = tramaPago(i).Split(CChar(";"))
                            Dim strTipDocumento As String, strMontoDetalle As Double, strDesTipoDoc As String

                            strTipDocumento = arr(2)
                            strMontoDetalle = Double.Parse(arr(1))
                            strDesTipoDoc = arr(4)

                            strError = objOffLine.RegistrarDetallePago(K_PAGON_IDPAGO, strTipDocumento, _
                                                           strDesTipoDoc, _
                                                           System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                                           strMontoDetalle, _
                                                           strUsuario, _
                                                           DBNull.Value, strUsuario, _
                                                           DBNull.Value, _
                                                           String.Empty, _
                                                           0, _
                                                           K_NROLOG_DET, K_DESLOG_DET)

                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  OUT K_NROLOG_DET: " & K_NROLOG_DET)
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  OUT K_DESLOG_DET: " & K_DESLOG_DET)
                            If K_DESLOG_DET <> "OK" Then
                                strCodRespuesta = "1"
                                objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR RegistrarDetallePago : " & strError)
                            End If
                        Next
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  Fin del detalle del pago - MSSAP")

                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  Ini Grabado SICAR BD")
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN strOficinaAnt : " & strOficinaAnt)
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN strCliente : " & strCliente)
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN strNroDoc : " & strNroDoc)
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN strFecha : " & strFecha)
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN strTrama : " & strTrama)
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN strUsuario : " & strUsuario)
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN strMonto : " & strMonto)
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN strNodo : " & strNodo)
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN strTUser : " & strTUser)
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN strNroOperacionPago : " & strNroOperacionPago)
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  IN strTipoPago : " & strTipoPago)

                        dsresult = objOffLine.Set_RecaudacionDAC(strOficinaAnt, strCliente, strNroDoc, strFecha, strTrama, strUsuario, strMonto, "1", strNodo, strTUser, strNroOperacionPago, strTipoPago)
                        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  Fin Grabado SICAR BD")

                        If Not dsresult Is Nothing Then
                            For i As Integer = 0 To dsresult.Tables(0).Rows.Count - 1
                                If (dsresult.Tables(0).Rows(i).Item("type") = "W" Or dsresult.Tables(0).Rows(i).Item("type") = "E") Then
                                    strCodRespuesta = "1"
                                    strRpta = dsresult.Tables(0).Rows(i).Item("type") & "@" & "Error en Pago DRA." & dsresult.Tables(0).Rows(i).Item("message")
                                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Error Respuesta - BD SICAR: " & strRpta)
                                    Exit For
                                End If
                            Next
                        End If
                    Else
                        Try
                            Dim resp As Boolean
                            Dim codApli, userApli, numAsociado, dravDescTrs, dravCodPago, msgRespuesta As String
                            Dim numAnulado As Int64 = 0
                            Dim codRespuesta As Int64 = 0

                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & " - " & "OBTENIENDO DATOS PARA LA ANULACIÓN")
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & " - " & "NRO. GENRADO SAP" & nroPedidoMsSap)

                            resp = objOffLine.ObtenerDatosTransaccionales(nroPedidoMsSap, numAsociado, codApli, userApli, dravDescTrs, dravCodPago)

                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & " - " & "DATOS OBNTENIDOS")
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & " - " & "NRO. GENRADO SAP" & nroPedidoMsSap)
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & " - " & "NRO. ASOCIADO" & numAsociado)
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & " - " & "CÓDIGO APLICACIÓN" & codApli)
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & " - " & "USUARIO APLICACIÓN" & userApli)
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & " - " & "DESC. TRS." & dravDescTrs)
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & " - " & "CODIGO DE PAGO :" & dravCodPago)

                            objOffLine.AnularExtornarPagoDRA(codApli, userApli, numAsociado, dravDescTrs, numAnulado, codRespuesta, msgRespuesta)
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & " - " & "numAnulado :" & numAnulado)
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & " - " & "codRespuesta :" & codRespuesta)
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & " - " & "msgRespuesta :" & msgRespuesta)

                            objOffLine.AsignarPagoAcuerdosXDocSap(nroPedidoMsSap, "", 0, strUsuario, msgj)
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR AsignarPagoAcuerdosXDocSap Rpta : " & msgj)
                            'INI PROY-140126
                            Dim MaptPath As String
                            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                            MaptPath = "( Class : " & MaptPath & "; Function: Set_RecaudacionDRA)"
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR Codigo Respuesta: " & strCodRespuesta & MaptPath)
                            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                            MaptPath = "( Class : " & MaptPath & "; Function: Set_RecaudacionDRA)"
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR : " & strMsgRespuesta & MaptPath)
                            'FIN PROY-140126
                            strCodRespuesta = "1"
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR RegistrarPago : " & strError)
                        Catch ex As Exception
                            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "EXCEPCION DRA: " & ex.Message)
                            Throw ex
                        End Try
                    End If
                Case Else
                    objOffLine.AsignarPagoAcuerdosXDocSap(nroPedidoMsSap, "", 0, strUsuario, msgj)
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR AsignarPagoAcuerdosXDocSap Rpta : " & msgj)
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: Set_RecaudacionDA)"
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR Codigo Respuesta: " & strCodRespuesta & MaptPath)                    
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: Set_RecaudacionDRA)"
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR : " & strMsgRespuesta & MaptPath)
                    'FIN PROY-140126                    
            End Select
            Return strRpta
        Catch webEx As System.Net.WebException
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "EXCEPCION: " & webEx.Message)
            Throw webEx
        Catch ex As Exception
            Dim stackTrace As New System.Diagnostics.StackTrace(ex)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "EXCEPCION: " & ex.Message)
        Finally
            ConexionWSDRA().Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Fin Set_RecaudacionDRA - ClsPagos")
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try
    End Function


    ' ANULAR PAGO
    Public Function Set_AnulacionDRA(ByVal strNroOperacionPago As String, _
                                     ByRef strCodRespuesta As String, _
                                     ByRef strMsgRespuesta As String) As String

        Dim strIdentifyLog As String = strNroOperacionPago
        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Inicio Set_AnulacionDRA - ClsPagos")
        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Inicio anular Pago DRA")
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE ANULACION " & " - " & " NroOperacionPago : " & strNroOperacionPago)

            Dim txtID As String
            Dim strCodigoAplicacion As String = ConfigurationSettings.AppSettings("CodigoAplicacionDRA")
            Dim strUsuarioAplicacion As String = ConfigurationSettings.AppSettings("UsuarioAplicacionDRA")
            Dim strRpta As String

            strRpta = ConexionWSDRA().anularPagoDRA(txtID, strCodigoAplicacion, strUsuarioAplicacion, _
                                                    strNroOperacionPago, strCodRespuesta, strMsgRespuesta)

            Select Case Trim(strCodRespuesta)
                Case 0
                    strRpta = "Se realizó la anulación del Número de Operación " & strNroOperacionPago & " | " & strMsgRespuesta
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & strRpta)
                Case Else
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: Set_AnulacionDRA)"
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR Codigo Respuesta: " & strCodRespuesta & MaptPath)                   
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: Set_AnulacionDRA)"
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR : " & strMsgRespuesta & MaptPath)
                    'FIN PROY-140126
            End Select

            Return strRpta
        Catch webEx As System.Net.WebException
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "EXCEPCION: " & webEx.Message)
            Throw webEx
        Catch ex As Exception
            Dim stackTrace As New System.Diagnostics.StackTrace(ex)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "EXCEPCION: " & ex.Message)
        Finally
            ConexionWSDRA().Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Fin Set_AnulacionDRA - ClsPagos")
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try
    End Function


    ' EXTORNAR PAGO
    Public Function Set_ExtornoPagoDRA(ByVal strTraceID As String, _
                                       ByRef strCodRespuesta As String, _
                                       ByRef strMsgRespuesta As String) As String

        Dim strIdentifyLog As String = strTraceID
        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Inicio Set_ExtornoPagoDRA - ClsPagos")
        objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Inicio extornar Pago DRA")
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "  DETALLE EXTORNO " & " - " & " TraceID : " & strTraceID)

            Dim txtID As String
            Dim strCodigoAplicacion As String = ConfigurationSettings.AppSettings("CodigoAplicacionDRA")
            Dim strUsuarioAplicacion As String = ConfigurationSettings.AppSettings("UsuarioAplicacionDRA")
            Dim strRpta As String

            strRpta = ConexionWSDRA().extornoPagoDRA(txtID, strCodigoAplicacion, strUsuarioAplicacion, _
                                             strTraceID, strCodRespuesta, strMsgRespuesta)

            Select Case Trim(strCodRespuesta)
                Case 0
                    strRpta = "Se realizó el extorno de Pago para el TraceID " & strTraceID & " | " & strMsgRespuesta
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & strRpta)
                Case Else
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: Set_ExtornoPagoDRA)"
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR Codigo Respuesta: " & strCodRespuesta & MaptPath)
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: Set_ExtornoPagoDRA)"
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR : " & strMsgRespuesta & MaptPath)
                    'FIN PROY-140126
            End Select

            Return strRpta
        Catch webEx As System.Net.WebException
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "EXCEPCION: " & webEx.Message)
            Throw webEx
        Catch ex As Exception
            Dim stackTrace As New System.Diagnostics.StackTrace(ex)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "EXCEPCION: " & ex.Message)
        Finally
            ConexionWSDRA().Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Fin Set_ExtornoPagoDRA - ClsPagos")
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try
    End Function

    Private Function CheckDbl(ByVal value As Object) As Double
        Dim salida As Double = 0
        If IsNothing(value) Or IsDBNull(value) Then
            salida = 0
        Else
            If Convert.ToString(value) = "" Then
                salida = 0
            Else
                salida = Convert.ToDouble(value)
            End If
        End If

        Return salida
    End Function

#End Region
    '//--


End Class


Public Class ItemAnulacion
    Public montoPagado As Double
    Public numeroDocumento, numeroOperacionCobranza, numeroOperacionAcreedor, fechaHoraTransaccion As String
    Public tracePago As Integer

    Public Sub New(ByVal monto As Decimal, ByVal numeroDoc As String, ByVal numCobranza As String, ByVal numAcree As String, ByVal fechaHora As String, ByVal trace As String)
        Me.montoPagado = monto
        Me.numeroDocumento = numeroDoc
        Me.numeroOperacionCobranza = numCobranza
        Me.numeroOperacionAcreedor = numAcree
        Me.fechaHoraTransaccion = fechaHora
        Me.tracePago = trace
    End Sub

    Public Sub New()

    End Sub

End Class