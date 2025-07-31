Imports System.Text
Imports System.Configuration
Imports SwichTransaccional.Services
Imports SwichTransaccional.DistribuidoresWS
Imports SwichTransaccional.ConsultaPagoDRAWS

Public Class clsConsultas

    '''cambiado por jtn
    '''se agrego en New
    Public sbLineasLog As New StringBuilder

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
        Dim strTimeOut As String = ConfigurationSettings.AppSettings("constSICARConsultasST_Timeout")
        If Not strTimeOut Is Nothing Then
            If strTimeOut <> "" Then
                iTimeOut = Convert.ToInt32(strTimeOut)
            End If
        End If
        oSwichTransaccional.Timeout = iTimeOut
    End Sub

    Public Function ConsultarRecibos(ByVal strLogSET As String, _
                             ByVal strNivelLogSet As String, _
                             ByVal strCodigoPuntoDeVenta As String, _
                             ByVal strCanal As String, _
                             ByVal strBinAdquiriente As String, _
                             ByVal strCodComercio As String, _
                             ByVal strCodigoCajero As String, _
                             ByVal strTipoIdentificadorDeudor As String, _
                             ByVal strNumeroIdentificadorDeudor As String, _
                             Optional ByVal strCodigoPlazaRecaudador As String = "", _
                             Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                             Optional ByVal strCodigoCiudadRecaudador As String = "") As String

        Dim intCodigo As Integer
        Dim strResultado As String

        Dim strTraceGen As String
        Dim strRespuestaConsulta As String
        Dim strOrigenRpta As String
        Dim strCodigoRpta As String
        Dim strDescripcionRpta As String
        Dim strNombreDeudor As String
        Dim strRucDeudor As String
        Dim strNumeroOperacionCobranza As String
        Dim dblValorTotal As Double
        Dim intNumeroDocumentos As Integer
        Dim strDocumentos As String
        Dim strFechaHoraTransaccion As String

        Dim brest As Boolean = ConsultarRecibosST(strLogSET, _
                                    strNivelLogSet, _
                                    strCodigoPuntoDeVenta, _
                                    strCanal, _
                                    strBinAdquiriente, _
                                    strCodComercio, _
                                    strCodigoCajero, _
                                    strTipoIdentificadorDeudor, _
                                    strNumeroIdentificadorDeudor, _
                                    strTraceGen, _
                                    strRespuestaConsulta, _
                                    strOrigenRpta, _
                                    strCodigoRpta, _
                                    strDescripcionRpta, _
                                    strNombreDeudor, _
                                    strRucDeudor, _
                                    strNumeroOperacionCobranza, _
                                    dblValorTotal, _
                                    intNumeroDocumentos, _
                                    strDocumentos, _
                                    strFechaHoraTransaccion, _
                                    strCodigoPlazaRecaudador, _
                                    strCodigoAgenciaRecaudador, _
                                    strCodigoCiudadRecaudador)

        If brest Then
            'strResultado = strRespuestaConsulta & "@verdadero"
            strResultado = strRespuestaConsulta & "@" & strNombreDeudor & ";" & strRucDeudor & ";" & strNumeroOperacionCobranza & ";" & dblValorTotal & ";" & intNumeroDocumentos & ";" & strFechaHoraTransaccion & ";" & strTraceGen & "@" & strDocumentos

        Else
            'strResultado = strRespuestaConsulta & "@falso"
            strResultado = strRespuestaConsulta & "@" & strDescripcionRpta
        End If

        Return strResultado

    End Function

    '**** E75810
    'LLAMA A OTRO METODO
    Public Function ConsultarRecibos_23(ByVal strLogSET As String, _
                 ByVal strNivelLogSet As String, _
                 ByVal strCodigoPuntoDeVenta As String, _
                 ByVal strCanal As String, _
                 ByVal strBinAdquiriente As String, _
                 ByVal strCodComercio As String, _
                 ByVal strCodigoCajero As String, _
                 ByVal strTipoIdentificadorDeudor As String, _
                 ByVal strNumeroIdentificadorDeudor As String, _
                 ByVal strCodServicio As String, _
                 ByRef hayDocumentosPorConsultar As Boolean, _
                 ByRef posicionNuevaConsultaDocumento As Integer, _
                 ByRef numeroDdocumentosDevueltos As Integer, _
                 Optional ByVal strCodigoPlazaRecaudador As String = "", _
                 Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                 Optional ByVal strCodigoCiudadRecaudador As String = "") As String

        Dim intCodigo As Integer
        Dim strResultado As String

        Dim strTraceGen As String
        Dim strRespuestaConsulta As String
        Dim strOrigenRpta As String
        Dim strCodigoRpta As String
        Dim strDescripcionRpta As String
        Dim strNombreDeudor As String
        Dim strRucDeudor As String
        Dim strNumeroOperacionCobranza As String
        Dim dblValorTotal As Double
        Dim intNumeroDocumentos As Integer
        Dim strDocumentos As String
        Dim strFechaHoraTransaccion As String
        sbLineasLog = New StringBuilder '//crea objeto de lineas para los logs

        Dim brest As Boolean = ConsultarRecibosST_23(strLogSET, strNivelLogSet, strCodigoPuntoDeVenta, strCanal, _
                      strBinAdquiriente, strCodComercio, strCodigoCajero, strTipoIdentificadorDeudor, _
                      strNumeroIdentificadorDeudor, strCodServicio, strTraceGen, strRespuestaConsulta, _
                      strOrigenRpta, strCodigoRpta, strDescripcionRpta, strNombreDeudor, _
                      strRucDeudor, strNumeroOperacionCobranza, dblValorTotal, intNumeroDocumentos, _
                      strDocumentos, strFechaHoraTransaccion, hayDocumentosPorConsultar, posicionNuevaConsultaDocumento, numeroDdocumentosDevueltos, strCodigoPlazaRecaudador, strCodigoAgenciaRecaudador, _
                      strCodigoCiudadRecaudador)

        If brest Then
            'strResultado = strRespuestaConsulta & "@verdadero"
            strResultado = strRespuestaConsulta & "@" & strNombreDeudor & ";" & strRucDeudor & ";" & strNumeroOperacionCobranza & ";" & dblValorTotal & ";" & intNumeroDocumentos & ";" & strFechaHoraTransaccion & ";" & strTraceGen & "@" & strDocumentos

        Else
            'strResultado = strRespuestaConsulta & "@falso"
            strResultado = strRespuestaConsulta & "@" & strDescripcionRpta
        End If
        Return strResultado
    End Function

    '''CONSULTA RECIBO DTH
    '''CONSULTA PAGO RECIBOS
    Private Function ConsultarRecibosST(ByVal strLogSET As String, _
                               ByVal strNivelLogSet As String, _
                               ByVal strCodigoPuntoDeVenta As String, _
                               ByVal strCanal As String, _
                               ByVal strBinAdquiriente As String, _
                               ByVal strCodComercio As String, _
                               ByVal strCodigoCajero As String, _
                               ByVal strTipoIdentificadorDeudor As String, _
                               ByVal strNumeroIdentificadorDeudor As String, _
                               ByRef strTraceGen As String, _
                               ByRef strRespuestaConsulta As String, _
                               ByRef strOrigenRpta As String, _
                               ByRef strCodigoRpta As String, _
                               ByRef strDescripcionRpta As String, _
                               ByRef strNombreDeudor As String, _
                               ByRef strRucDeudor As String, _
                               ByRef strNumeroOperacionCobranza As String, _
                               ByRef dblValorTotal As Double, _
                               ByRef intNumeroDocumentos As Integer, _
                               ByRef strDocumentos As String, _
                               ByRef strFechaHoraTransaccion As String, _
                               Optional ByVal strCodigoPlazaRecaudador As String = "", _
                               Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                               Optional ByVal strCodigoCiudadRecaudador As String = "") As Boolean 'Variant 'Array


        Dim strIdentifyLog As String = strTipoIdentificadorDeudor & "|" & strNumeroIdentificadorDeudor
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ConsultarRecibosST")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

        'Dim objComponente 'As New OLCPVUPagos.clsOLCPVUPagos
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
        '***** EN SAP Set_LogRecaudacion
        'Dim clsSap As New SAP_SIC_Recaudacion.clsRecaudacion
        Dim strIngreso As String
        '******
        Dim strLinDocsLog As String = ""
        Dim strDocumentosLog As String = ""
        Dim lngLog As Long

        Dim strFecha As String
        Dim strHora As String

        Dim strDescripcionRptaAux As String
        '''CAMBIADO POR JTN
        'Dim objOffline As New COM_SIC_OffLine.clsOffline
        'Dim intSAP = objOffline.Get_ConsultaSAP

        Dim clsSap As New COM_SIC_OffLine.clsOffline
        'If intSAP = 1 Then
        '    clsSap = New SAP_SIC_Recaudacion.clsRecaudacion
        'Else
        '    clsSap = New COM_SIC_OffLine.clsOffline
        'End If
        Dim consultaRespuesta As New ConsultaDeudaResponse
        Dim consultaEnvio As New ConsultaDeudaRequest
        Try
            'objComponente = CreateObject("OLCPVUPagos.clsOLCPVUPagos")

            'lngLog = objComponente.SetEnv(strLogSET, strNivelLogSet)
            'If lngLog = 0 Then
            '***** EN SAP Set_LogRecaudacion
            strIngreso = ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";;;;;;;" & strBinAdquiriente & ";" & strCodComercio & ";;;;;;" & strCodigoCajero
            strTrace = clsSap.SetLogRecaudacion(strIngreso, "")
            strTrace = Right(strTrace, 6)
            strTrace = "0000" & strTrace
            'CARIAS: Esto se hace por emergencia, no se debe pasar de 1000000 09/02/2007
            'If CDbl(strTrace) > 999999 Then
            'strTrace = Format(CDbl(strTrace) - 999999, "0000000000")
            'End If
            'CARIAS: fin
            '*****
            strTraceGen = strTrace

            Dim codigoFormato As String = ConfigurationSettings.AppSettings("CONST_CODIGO_FORMATO")
            Dim acreedor As String = ConfigurationSettings.AppSettings("CONST_ACREEDOR")
            Dim productoMovil As String = ConfigurationSettings.AppSettings("CONST_PRODUCTOMOVIL")

            'Dim wsConsulta As New ServiciosClaro

            With consultaEnvio

                .binAdquiriente = strBinAdquiriente
                .canal = strCanal
                .fechaCaptura = Date.Now.ToString("yyyy-MM-dd-05:00")
                .fechaTransaccion = Date.Now.ToString("yyyy-MM-ddTHH:mm:ss.ms-05:00")
                .fechaCapturaSpecified = True
                .fechaTransaccionSpecified = True
                .acreedor = acreedor
                .numeroIdentificacionDeudor = strNumeroIdentificadorDeudor
                .codigoFormato = codigoFormato
                .producto = productoMovil
                .tipoIdentificacionDeudor = strTipoIdentificadorDeudor
                .trace = strTraceGen

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  TerminalID : " & strCodigoPuntoDeVenta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Trace : " & strTrace)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Canal : " & strCanal)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  TipoIdentificacionDeudor : " & strTipoIdentificadorDeudor)

                '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  NumeroIdentificacionDeudor : " & strNumeroIdentificadorDeudor)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodigoPlazaRecaudador : " & strCodigoPlazaRecaudador)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodigoAgenciaRecaudador : " & strCodigoAgenciaRecaudador)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodigoCiudadRecaudador : " & strCodigoCiudadRecaudador)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  BinAdquiriente : " & strBinAdquiriente)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodComercio : " & strCodComercio)
            End With
            consultaRespuesta = oSwichTransaccional.consultaDeuda(consultaEnvio)
            With consultaRespuesta
                strRpta = .codigoRespuesta
                strOrigenRpta = .codigoOrigenRespuesta
                strCodigoRpta = .codigoRespuesta
                strDescripcionRpta = .descripcionExtendidaRespuesta
                strRespuestaConsulta = strRpta
                Select Case Trim(strRpta)
                    Case "00"
                        strNombreDeudor = .nombreDeudor
                        strRucDeudor = .rucDeudor
                        strNumeroOperacionCobranza = .productosServicios(0).documentos(0).referenciaDeuda
                        dblValorTotal = .productosServicios(0).montoDeuda
                        intNumeroDocumentos = .productosServicios(0).documentos.Length
                        strFechaHoraTransaccion = .fechaTransaccion

                        '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CABECERA : " & strNombreDeudor & ";" & strRucDeudor & ";" & strNumeroOperacionCobranza & ";" & dblValorTotal.ToString() & ";" & intNumeroDocumentos & ";" & strFechaHoraTransaccion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CABECERA : " & ";" & strRucDeudor & ";" & strNumeroOperacionCobranza & ";" & dblValorTotal.ToString() & ";" & intNumeroDocumentos & ";" & strFechaHoraTransaccion)

                        'strDocumentos = .Documentos
                        Dim doc As New Documento
                        For intNumDocs = 0 To .productosServicios(0).documentos.Length - 1
                            Dim documento As Documento = .productosServicios(0).documentos(intNumDocs)
                            Dim fechaEmicion$ = documento.fechaEmision.ToString("yyyyMMdd")
                            Dim fechaVencimiento$ = documento.fechaVencimiento.ToString("yyyyMMdd")

                            Dim lineaDocumento$ = String.Format("{0};{1};{2};{3};{4};{5};{6};{7:0##########}|", .productosServicios(0).codigo, .productosServicios(0).descripcion, .productosServicios(0).codigoMoneda, .productosServicios(0).codigo, documento.numeroDocumento, fechaEmicion, fechaVencimiento, documento.importeSaldoDeuda * 100)
                            strDocumentos = strDocumentos & lineaDocumento

                            strDocsAux = Mid(strDocumentos, (67 * intNumDocs) + 1, 67)
                            strCodServicio = .productosServicios(0).codigo
                            strDesServicio = .productosServicios(0).descripcion
                            strMonServicio = .productosServicios(0).codigoMoneda
                            strTipoDoc = .productosServicios(0).codigo 'TODO: CAMBIADO POR JYMMY TORRES Trim(Mid(strDocsAux, 25, 3)) ''TODO: CAMBIADO 22 POR EL 25
                            strNumDoc = documento.numeroDocumento
                            strFechaEmi = documento.fechaEmision.ToString("yyyyMMdd")
                            strFechaVen = documento.fechaVencimiento.ToString("yyyyMMdd")
                            strMontoDoc = documento.importeSaldoDeuda
                            If Trim(strMontoDoc) = String.Empty Then
                                strMontoDoc = "0.00"
                            Else
                                strMontoDoc = strMontoDoc
                            End If
                            strLinDocs = strLinDocs & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                            strLinDocsLog = strLinDocsLog & strTrace & ";" & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                        Next


                        'For intNumDocs = 0 To CInt(intNumeroDocumentos) - 1
                        'strDocsAux = Mid(strDocumentos, (67 * intNumDocs) + 1, 67)
                        'strCodServicio = Trim(Mid(strDocsAux, 1, 3))
                        'strDesServicio = Trim(Mid(strDocsAux, 4, 15))
                        'strMonServicio = Trim(Mid(strDocsAux, 19, 3))
                        'strTipoDoc = Trim(Mid(strDocsAux, 22, 3))
                        'strNumDoc = Trim(Mid(strDocsAux, 25, 16))
                        'strFechaEmi = Trim(Mid(strDocsAux, 41, 8))
                        'strFechaVen = Trim(Mid(strDocsAux, 49, 8))
                        'strMontoDoc = Trim(Mid(strDocsAux, 57, 11))
                        'If Trim(strMontoDoc) = "" Then
                        '    strMontoDoc = "0.00"
                        'Else
                        '    strMontoDoc = strMontoDoc
                        'End If
                        'strLinDocs = strLinDocs & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                        'strLinDocsLog = strLinDocsLog & strTrace & ";" & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                        'Next

                        strDocumentos = Mid(strLinDocs, 1, Len(strLinDocs) - 1)
                        strDocumentosLog = Mid(strLinDocsLog, 1, Len(strLinDocsLog) - 1)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  RECIBOS : " & strDocumentos)

                        If Trim(strFechaHoraTransaccion) <> "" Then
                            strFecha = Mid(strFechaHoraTransaccion, 3, 2) & "/" & Mid(strFechaHoraTransaccion, 1, 2) & "/" & Year(Now)
                            strHora = Mid(strFechaHoraTransaccion, 5, 2) & ":" & Mid(strFechaHoraTransaccion, 7, 2) & ":" & Mid(strFechaHoraTransaccion, 9, 2)
                        End If
                        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";;;;" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";;;;;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal
                        '''CAMBIADO POR JTN
                        'strTrace = clsSap.Set_LogRecaudacion(strIngreso, strDocumentosLog)
                        strTrace = clsSap.SetLogRecaudacion(strIngreso, strDocumentosLog)
                        '''CAMBIADO HASTA AQUI
                        ConsultarRecibosST = True
                    Case Else
                        strNombreDeudor = ""
                        strRucDeudor = ""
                        dblValorTotal = 0
                        strNumeroOperacionCobranza = ""
                        intNumeroDocumentos = 0
                        strDocumentos = ""
                        strFechaHoraTransaccion = ""
                        ConsultarRecibosST = False

                        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";;;;" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";;;;;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal
                        '''CAMBIADO POR JTN
                        'strTrace = clsSap.Set_LogRecaudacion(strIngreso, strDocumentosLog)
                        strTrace = clsSap.SetLogRecaudacion(strIngreso, strDocumentosLog)
                        '''CAMBIADO HASTA AQUI

                        If strOrigenRpta = 3 Then
                            strDescripcionRptaAux = BuscarMensaje(CInt(strCodigoRpta))
                            If Trim(strDescripcionRptaAux) <> "" Then
                                strDescripcionRpta = strDescripcionRptaAux
                            Else
                                strDescripcionRpta = BuscarMensaje(8)
                            End If
                        Else
                            'HHA: 20051111 MOSTRARA MESAJE DE ERROR GNERAL PARA LOS DEMAS CASOS DE CODIGO DE ORIGEN DIFERENTE DE 3
                            If strDescripcionRpta Is Nothing OrElse strDescripcionRpta = "" Then
                                strDescripcionRpta = BuscarMensaje(8)
                            End If
                            'App.LogEvent "strRpta:" & strRpta & ";strOrigenRpta: " & strOrigenRpta & ";strDescripcionRpta: " & strDescripcionRpta, vbLogEventTypeError
                        End If
                End Select
            End With
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Consulta : " & strRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  OrigenRpta : " & strOrigenRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CodigoRpta : " & strCodigoRpta)
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: ConsultarReciboST)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  DescripcionRpta : " & strDescripcionRpta & MaptPath)
            'FIN PROY-140126

            'With objComponente
            '.TerminalID = strCodigoPuntoDeVenta
            '.Trace = strTrace
            '.Canal = strCanal
            '.TipoIdentificacionDeudor = strTipoIdentificadorDeudor
            '.NumeroIdentificacionDeudor = strNumeroIdentificadorDeudor
            '.CodigoPlazaRecaudador = strCodigoPlazaRecaudador
            '.CodigoAgenciaRecaudador = strCodigoAgenciaRecaudador
            '.CodigoCiudadRecaudador = strCodigoCiudadRecaudador
            '.BinAdquiriente = strBinAdquiriente
            '.CodComercio = strCodComercio

            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  TerminalID : " & strCodigoPuntoDeVenta)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Trace : " & strTrace)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Canal : " & strCanal)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  TipoIdentificacionDeudor : " & strTipoIdentificadorDeudor)

            ''''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  NumeroIdentificacionDeudor : " & strNumeroIdentificadorDeudor)

            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodigoPlazaRecaudador : " & strCodigoPlazaRecaudador)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodigoAgenciaRecaudador : " & strCodigoAgenciaRecaudador)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodigoCiudadRecaudador : " & strCodigoCiudadRecaudador)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  BinAdquiriente : " & strBinAdquiriente)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodComercio : " & strCodComercio)

            'strRpta = .Consulta

            'strOrigenRpta = .OrigenRpta
            'strCodigoRpta = .CodigoRpta
            'strDescripcionRpta = .DescripcionRpta
            'strRespuestaConsulta = strRpta

            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Consulta : " & strRpta)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  OrigenRpta : " & strOrigenRpta)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CodigoRpta : " & strCodigoRpta)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  DescripcionRpta : " & strDescripcionRpta)

            'Select Case Trim(strRpta)
            '    Case "0"
            '        strNombreDeudor = .NombreDeudor
            '        strRucDeudor = .RUCDeudor
            '        strNumeroOperacionCobranza = .NumeroOperacionCobranza
            '        dblValorTotal = .ValorTotal
            '        intNumeroDocumentos = .NumeroDocumentos
            '        strFechaHoraTransaccion = .FechaHoraTransaccion

            '        '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CABECERA : " & strNombreDeudor & ";" & strRucDeudor & ";" & strNumeroOperacionCobranza & ";" & dblValorTotal.ToString() & ";" & intNumeroDocumentos & ";" & strFechaHoraTransaccion)
            '        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CABECERA : " & ";" & strRucDeudor & ";" & strNumeroOperacionCobranza & ";" & dblValorTotal.ToString() & ";" & intNumeroDocumentos & ";" & strFechaHoraTransaccion)

            '        strDocumentos = .Documentos

            '        For intNumDocs = 0 To CInt(intNumeroDocumentos) - 1
            '            strDocsAux = Mid(strDocumentos, (67 * intNumDocs) + 1, 67)
            '            strCodServicio = Trim(Mid(strDocsAux, 1, 3))
            '            strDesServicio = Trim(Mid(strDocsAux, 4, 15))
            '            strMonServicio = Trim(Mid(strDocsAux, 19, 3))
            '            strTipoDoc = Trim(Mid(strDocsAux, 22, 3))
            '            strNumDoc = Trim(Mid(strDocsAux, 25, 16))
            '            strFechaEmi = Trim(Mid(strDocsAux, 41, 8))
            '            strFechaVen = Trim(Mid(strDocsAux, 49, 8))
            '            strMontoDoc = Trim(Mid(strDocsAux, 57, 11))
            '            If Trim(strMontoDoc) = "" Then
            '                strMontoDoc = "0.00"
            '            Else
            '                strMontoDoc = strMontoDoc
            '            End If
            '            strLinDocs = strLinDocs & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
            '            strLinDocsLog = strLinDocsLog & strTrace & ";" & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
            '        Next
            '        strDocumentos = Mid(strLinDocs, 1, Len(strLinDocs) - 1)
            '        strDocumentosLog = Mid(strLinDocsLog, 1, Len(strLinDocsLog) - 1)

            '        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  RECIBOS : " & strDocumentos)

            '        If Trim(strFechaHoraTransaccion) <> "" Then
            '            strFecha = Mid(strFechaHoraTransaccion, 3, 2) & "/" & Mid(strFechaHoraTransaccion, 1, 2) & "/" & Year(Now)
            '            strHora = Mid(strFechaHoraTransaccion, 5, 2) & ":" & Mid(strFechaHoraTransaccion, 7, 2) & ":" & Mid(strFechaHoraTransaccion, 9, 2)
            '        End If
            '        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";;;;" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";;;;;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal
            '        '''CAMBIADO POR JTN
            '        'strTrace = clsSap.Set_LogRecaudacion(strIngreso, strDocumentosLog)
            '        strTrace = clsSap.SetLogRecaudacion(strIngreso, strDocumentosLog)
            '        '''CAMBIADO HASTA AQUI
            '        '''
            '        ConsultarRecibosST = True
            '    Case Else
            '        strNombreDeudor = ""
            '        strRucDeudor = ""
            '        dblValorTotal = 0
            '        strNumeroOperacionCobranza = ""
            '        intNumeroDocumentos = 0
            '        strDocumentos = ""
            '        strFechaHoraTransaccion = ""
            '        ConsultarRecibosST = False

            '        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";;;;" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";;;;;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal
            '        '''CAMBIADO POR JTN
            '        'strTrace = clsSap.Set_LogRecaudacion(strIngreso, strDocumentosLog)
            '        strTrace = clsSap.SetLogRecaudacion(strIngreso, strDocumentosLog)
            '        '''CAMBIADO HASTA AQUI

            '        If strOrigenRpta = 3 Then
            '            strDescripcionRptaAux = BuscarMensaje(CInt(strCodigoRpta))
            '            If Trim(strDescripcionRptaAux) <> "" Then
            '                strDescripcionRpta = strDescripcionRptaAux
            '            Else
            '                strDescripcionRpta = BuscarMensaje(8)
            '            End If
            '        Else
            '            'HHA: 20051111 MOSTRARA MESAJE DE ERROR GNERAL PARA LOS DEMAS CASOS DE CODIGO DE ORIGEN DIFERENTE DE 3

            '            If strDescripcionRpta Is Nothing OrElse strDescripcionRpta = "" Then
            '                strDescripcionRpta = BuscarMensaje(8)
            '            End If
            '            'App.LogEvent "strRpta:" & strRpta & ";strOrigenRpta: " & strOrigenRpta & ";strDescripcionRpta: " & strDescripcionRpta, vbLogEventTypeError
            '        End If
            'End Select
            'End With
            'Else
            '    strDescripcionRpta = "ERROR " & lngLog
            '    strNombreDeudor = ""
            '    strRucDeudor = ""
            '    dblValorTotal = 0
            '    strNumeroOperacionCobranza = ""
            '    intNumeroDocumentos = 0
            '    strDocumentos = ""
            '    strFechaHoraTransaccion = ""
            '    ConsultarRecibosST = False
            'End If
        Catch webEx As System.Net.WebException
            If CType(webEx, System.Net.WebException).Status = Net.WebExceptionStatus.Timeout Then
                strDescripcionRpta = "Tiempo de espera excedido."
            Else
                strDescripcionRpta = webEx.Message
            End If
            strNombreDeudor = ""
            strRucDeudor = ""
            dblValorTotal = 0
            strNumeroOperacionCobranza = ""
            intNumeroDocumentos = 0
            strDocumentos = ""
            strFechaHoraTransaccion = ""
            ConsultarRecibosST = False
            consultaEnvio = Nothing
            consultaRespuesta = Nothing
            oSwichTransaccional.Dispose()
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: ConsultarReciboST)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ERROR : " & webEx.Message.ToString() & MaptPath)
            'FIN PROY-140126


        Catch ex As Exception
            strDescripcionRpta = ex.Message
            strNombreDeudor = ""
            strRucDeudor = ""
            dblValorTotal = 0
            strNumeroOperacionCobranza = ""
            intNumeroDocumentos = 0
            strDocumentos = ""
            strFechaHoraTransaccion = ""
            ConsultarRecibosST = False
            consultaEnvio = Nothing
            consultaRespuesta = Nothing
            oSwichTransaccional.Dispose()

            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: ConsultarRecibosST)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ERROR : " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126            
        Finally
            clsSap = Nothing 'e75810
            oSwichTransaccional.Dispose()
            consultaEnvio = Nothing
            consultaRespuesta = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ConsultarRecibosST")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try

    End Function

    '***E75810
    '''ESTE ES
    '''AQUI 
    '''AQUI
    Private Function ConsultarRecibosST_23(ByVal strLogSET As String, _
                                                         ByVal strNivelLogSet As String, _
                                                         ByVal strCodigoPuntoDeVenta As String, _
                                                         ByVal strCanal As String, _
                                                         ByVal strBinAdquiriente As String, _
                                                         ByVal strCodComercio As String, _
                                                         ByVal strCodigoCajero As String, _
                                                         ByVal strTipoIdentificadorDeudor As String, _
                                                         ByVal strNumeroIdentificadorDeudor As String, _
                                                         ByVal strCodigoServicio As String, _
                                                         ByRef strTraceGen As String, _
                                                         ByRef strRespuestaConsulta As String, _
                                                         ByRef strOrigenRpta As String, _
                                                         ByRef strCodigoRpta As String, _
                                                         ByRef strDescripcionRpta As String, _
                                                         ByRef strNombreDeudor As String, _
                                                         ByRef strRucDeudor As String, _
                                                         ByRef strNumeroOperacionCobranza As String, _
                                                         ByRef dblValorTotal As Double, _
                                                         ByRef intNumeroDocumentos As Integer, _
                                                         ByRef strDocumentos As String, _
                                                         ByRef strFechaHoraTransaccion As String, _
                                                         ByRef hayDocumentosPorConsultar As Boolean, _
                                                         ByRef posicionNuevaConsultaDocumento As Integer, _
                                                         ByRef numeroDdocumentosDevueltos As Integer, _
                                                         Optional ByVal strCodigoPlazaRecaudador As String = "", _
                                                         Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                                                         Optional ByVal strCodigoCiudadRecaudador As String = "") As Boolean 'Variant 'Array

        Dim bRespuesta As Boolean = False
        'Dim objComponente 'As New OLCPVUPagos.clsOLCPVUPagos
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
        Dim intNumDocs As Integer
        '***** EN SAP Set_LogRecaudacion
        Dim strIngreso As String
        '******
        Dim strLinDocsLog As String = String.Empty
        Dim strDocumentosLog As String = String.Empty
        Dim lngLog As Long

        Dim strFecha As String
        Dim strHora As String
        Dim strDescripcionRptaAux As String

        '---crea objeto para guardar logs
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim intSAP = objOffline.Get_ConsultaSAP
        'Dim clsSap As Object
        Dim clsSap As New COM_SIC_OffLine.clsOffline
        'If intSAP = 1 Then
        '    clsSap = New SAP_SIC_Recaudacion.clsRecaudacion
        'Else
        '    clsSap = New COM_SIC_OffLine.clsOffline
        'End If
        '---inicializa
        ''VALORES DE LA TRAMA A CONCATENAR Y ENVIAR
        strNombreDeudor = String.Empty
        strRucDeudor = String.Empty
        strNumeroOperacionCobranza = String.Empty
        dblValorTotal = 0
        intNumeroDocumentos = 0
        strFechaHoraTransaccion = String.Empty
        strDocumentos = String.Empty
        'INI PROY-140126
        Dim sMarLeft As String = "[" & strArchivoRfp & "] " & Date.Now.ToString("yyyy-MM-dd-hh:mm:ss") & " -" & strNumeroIdentificadorDeudor & "- " & vbTab & vbTab
        'FIN PROY-140126
        sbLineasLog.Append(vbTab & vbTab & "--------------------------------------------------------" & vbCrLf)
        sbLineasLog.Append(sMarLeft & "ConsultarRecibosST_23 (Fijo y Páginas) - Inicio " & vbCrLf) ' //& DateTime.Now.ToShortDateString & " " & DateTime.Now.ToShortTimeString
        sbLineasLog.Append(sMarLeft & "--------------------------------------------------------" & vbCrLf)
        sMarLeft = sMarLeft & vbTab

        Dim consultaEnvio As New ConsultaDeudaRequest
        Dim consultaRespuesta As New ConsultaDeudaResponse
        Try
            '---
            'objComponente = CreateObject("OLCPVUPagos.clsOLCPVUPagos") '''INSTANCIANDO EL ST
            'lngLog = objComponente.SetEnv(strLogSET, strNivelLogSet)

            'Dim wsConsulta As New ServiciosClaro
            Dim codpagoDolares = ConfigurationSettings.AppSettings("constCodigoServFactDolares")
            With consultaEnvio
                .acreedor = "111111" '''--> FIJO WILLIAM JARA
                .agencia = strCodigoAgenciaRecaudador
                .binAdquiriente = strBinAdquiriente
                .binAdquirienteReenvia = ConfigurationSettings.AppSettings("cteCODIGO_BINADQUIRIENTE") '--> debe ser configurable por ignacio campos
                .canal = strCanal
                .trace = strTrace
                .codigoFormato = "01" '--> FIjo segun william jara
                .fechaCaptura = Date.Now.ToString("yyyy-MM-dd-05:00")
                .fechaTransaccion = Date.Now.ToString("yyyy-MM-ddTHH:mm:ss.ms-05:00")
                .fechaCapturaSpecified = True
                .fechaTransaccionSpecified = True
                .numeroComercio = strCodComercio
                .nombreComercio = "" '''--> REVISAR SI TRAE EL NOMBRE DEL COMERCIO
                .numeroIdentificacionDeudor = strNumeroIdentificadorDeudor
                '.numeroReferencia = "99888899" '--> REVISAR SI TRAE EL NUMERO DE REFERENCIA
                .tipoIdentificacionDeudor = strTipoIdentificadorDeudor
                .plaza = strCodigoPlazaRecaudador
                .ciudad = strCodigoCiudadRecaudador
                .producto = strCodigoServicio
                '.numeroTerminal = "X1X1X1"
                '.procesador = "000002"
                .posicionUltimoDocumento = posicionNuevaConsultaDocumento
                .posicionUltimoDocumentoSpecified = True
                .tamanioMaximoBloque = 2541 '4096 '4KB
                .tamanioMaximoBloqueSpecified = True
                .codigoMoneda = IIf(strCodigoServicio = codpagoDolares, ConfigurationSettings.AppSettings("constMONCodigoDolares"), ConfigurationSettings.AppSettings("constMONCodigoSoles")) '''OK

            End With

            sbLineasLog.Append(sMarLeft & "INP TerminalID: " & strCodigoPuntoDeVenta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP Trace: " & strTrace & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP Canal: " & strCanal & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP TipoIdentificacionDeudor: " & strTipoIdentificadorDeudor & vbCrLf)

            '''sbLineasLog.Append(sMarLeft & "INP NumeroIdentificacionDeudor: " & strNumeroIdentificadorDeudor & vbCrLf)

            sbLineasLog.Append(sMarLeft & "INP CodigoPlazaRecaudador: " & strCodigoPlazaRecaudador & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP CodigoAgenciaRecaudador: " & strCodigoAgenciaRecaudador & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP CodigoCiudadRecaudador: " & strCodigoCiudadRecaudador & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP BinAdquiriente: " & strBinAdquiriente & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP CodComercio: " & strCodComercio & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP Servicio: " & strCodigoServicio & vbCrLf)


            '''EJECUCION DEL WS CONSULTA DEUDA
            consultaRespuesta = oSwichTransaccional.consultaDeuda(consultaEnvio)
            Dim tramaCabecera$, tramaDetalle$

            With consultaRespuesta
                strRpta = .codigoRespuesta
                strOrigenRpta = .codigoOrigenRespuesta
                strCodigoRpta = .codigoRespuesta
                strDescripcionRpta = .descripcionExtendidaRespuesta
                strRespuestaConsulta = strRpta
                If Trim(strRpta) = "00" Then '//éxito
                    strNombreDeudor = .nombreDeudor
                    strRucDeudor = .rucDeudor
                    dblValorTotal = .productosServicios(0).montoDeuda ' .montoTotalDeuda
                    strNumeroOperacionCobranza = .numeroReferencia
                    intNumeroDocumentos = .productosServicios(0).documentos.Length
                    numeroDdocumentosDevueltos = .numeroTotalDocumentosPendientes
                    strFechaHoraTransaccion = .fechaTransaccion

                    hayDocumentosPorConsultar = .hayMasDocumentos
                    posicionNuevaConsultaDocumento = .posicionUltimoDocumento

                    For intNumDocs = 0 To .productosServicios(0).documentos.Length - 1
                        Dim documento As Documento = .productosServicios(0).documentos(intNumDocs)
                        Dim fechaEmicion$ = documento.fechaEmision.ToString("yyyyMMdd")
                        Dim fechaVencimiento$ = documento.fechaVencimiento.ToString("yyyyMMdd")

                        strDocumentos = strDocumentos & String.Format("{0};{1};{2};{3};{4};{5};{6};{7:0##########}|", .productosServicios(0).codigo, .productosServicios(0).descripcion, .productosServicios(0).codigoMoneda, .productosServicios(0).codigo, documento.numeroDocumento, fechaEmicion, fechaVencimiento, documento.importeSaldoDeuda * 100)

                        strDocsAux = Mid(strDocumentos, (67 * intNumDocs) + 1, 67)
                        strCodServicio = .productosServicios(0).codigo
                        strDesServicio = .productosServicios(0).descripcion
                        strMonServicio = .productosServicios(0).codigoMoneda
                        strTipoDoc = Trim(Mid(strDocsAux, 22, 3))
                        strNumDoc = documento.numeroDocumento
                        strFechaEmi = documento.fechaEmision.ToShortDateString
                        strFechaVen = documento.fechaVencimiento.ToShortDateString
                        strMontoDoc = documento.importeSaldoDeuda '.productosServicios(0).montoDeuda
                        If Trim(strMontoDoc) = String.Empty Then
                            strMontoDoc = "0.00"
                        Else
                            strMontoDoc = strMontoDoc
                        End If
                        strLinDocs = strLinDocs & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                        strLinDocsLog = strLinDocsLog & strTrace & ";" & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                    Next

                    strDocumentos = Mid(strDocumentos, 1, Len(strDocumentos) - 1)

                    If Trim(strFechaHoraTransaccion) <> "" Then
                        strFecha = Mid(strFechaHoraTransaccion, 3, 2) & "/" & Mid(strFechaHoraTransaccion, 1, 2) & "/" & Year(Now)
                        strHora = Mid(strFechaHoraTransaccion, 5, 2) & ":" & Mid(strFechaHoraTransaccion, 7, 2) & ":" & Mid(strFechaHoraTransaccion, 9, 2)
                    End If
                    '---guarda log de consulta de recibo
                    strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";;;;" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";;;;;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal
                    strTrace = clsSap.Set_LogRecaudacion(strIngreso, strDocumentosLog)
                    '--
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
                    '--guarda log de consulta
                    strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";;;;" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";;;;;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";;;;" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal
                    strTrace = clsSap.Set_LogRecaudacion(strIngreso, strDocumentosLog)
                    '--
                    strDescripcionRpta = clsMensajes.DeterminaMensaje(clsMensajes.kGrupo_FijoPaginas, CInt(strCodigoRpta), strDescripcionRpta)
                End If
            End With

            If lngLog = 0 Then
                '***** EN SAP Set_LogRecaudacion
                '---guarda el log de la recaudacion y obtiene el número de trace
                strIngreso = ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";;;;;;;" & strBinAdquiriente & ";" & strCodComercio & ";;;;;;" & strCodigoCajero
                strTrace = clsSap.Set_LogRecaudacion(strIngreso, String.Empty)
                strTrace = "0000" & strTrace
                strTrace = Right(strTrace, 6) '//solo seis caracteres
                strTraceGen = strTrace
                If Trim(strRpta) = "0" Then '//éxito
                    'bRespuesta = True
                Else '//fracazo

                End If
                'End With
            Else '//Error al registrar el primer log a través de Switch
                '''strDescripcionRpta = "ERROR " & lngLog
                ''ConsultarRecibosST_23 = False
                strRespuestaConsulta = CStr(lngLog)
                'strOrigenRpta = objComponente.OrigenRpta
                'strCodigoRpta = objComponente.CodigoRpta
                'strDescripcionRpta = objComponente.DescripcionRpta
            End If
        Catch webEx As System.Net.WebException
            strRespuestaConsulta = "-1000"
            If CType(webEx, System.Net.WebException).Status = Net.WebExceptionStatus.Timeout Then
                strDescripcionRpta = "Tiempo de espera excedido."
            Else
                strDescripcionRpta = webEx.Message
            End If
            oSwichTransaccional.Dispose()
            consultaRespuesta = Nothing
            consultaEnvio = Nothing
        Catch ex As Exception
            strRespuestaConsulta = "-1000"
            strDescripcionRpta = ex.Message
            oSwichTransaccional.Dispose()
            consultaRespuesta = Nothing
            consultaEnvio = Nothing
        Finally
            clsSap = Nothing
            oSwichTransaccional.Dispose()
            consultaRespuesta = Nothing
            consultaEnvio = Nothing
            sbLineasLog.Append(sMarLeft & "OUT strRpta(funcion): " & strRespuestaConsulta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "OUT OrigenRpta: " & strOrigenRpta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "OUT CodigoRpta: " & strCodigoRpta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "OUT DescripcionRpta: " & strDescripcionRpta & vbCrLf)
            sMarLeft = sMarLeft.Substring(0, sMarLeft.Length - 1)
            sbLineasLog.Append(sMarLeft & "---------------------------------------------------------------" & vbCrLf)
            sbLineasLog.Append(sMarLeft & "ConsultarRecibosST_23 - Fin" & vbCrLf)
            sbLineasLog.Append(sMarLeft & "---------------------------------------------------------------" & vbCrLf)
        End Try
        '--
        Return bRespuesta
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
        End Select
        Return strMensaje
    End Function


    ''AGREGADO POR CCC-TS
#Region "RECAUDACIONES DAC"

    Public Function ConsultarDeudaDAC(ByVal strVersionSap As String, _
                                        ByVal strCliente As String, _
                                        ByVal strUsuario As String, _
                                        ByVal strIPApp As String, _
                                        ByRef strName As String, _
                                        ByRef dMontoDeuda As Double, _
                                        ByRef strDoc As String) As String

        Dim strResultado As String = String.Empty
        Dim strRespuestaConsulta As String = String.Empty
        Dim strDescripcionRpta As String = String.Empty
        Dim strCodigoRpta As String = String.Empty
        'Return Values
        Dim strIdTransaccion As String = String.Empty


        Dim brest As Boolean = GetDeudaDAC(strCliente, strVersionSap, strUsuario, strIPApp, strIdTransaccion, _
                                    strRespuestaConsulta, strDescripcionRpta, strCodigoRpta, _
                                    strName, dMontoDeuda, strDoc)

        If brest Then
            strResultado = strRespuestaConsulta & "@" & strName & ";" & strDoc & ";" & dMontoDeuda
        Else
            strResultado = strRespuestaConsulta & "@" & strDescripcionRpta
        End If

        Return strResultado
    End Function

    Private Function GetDeudaDAC(ByVal strCliente As String, _
                               ByVal strVersionSap As String, _
                               ByVal strUsuario As String, _
                               ByVal strIPApp As String, _
                               ByRef IdTransaccion As String, _
                               ByRef strRespuestaConsulta As String, _
                               ByRef strDescripcionRpta As String, _
                               ByRef strCodigoRpta As String, _
                               ByRef strNombreDeudor As String, _
                               ByRef dblValorTotal As Double, _
                               ByRef strDocumento As String) As Boolean

        Dim strIdentifyLog As String = strCliente
        Dim bResult As Boolean = False
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "Inicio GetDeudaDAC")
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "-------------------------------------------------")

            Dim strNombreAplicacion As String = ConfigurationSettings.AppSettings("NombreAplicacionDAC")
            Dim strIpAplicacion As String = strIPApp
            Dim strIdTransaccion As String = DateTime.Now.Ticks.ToString().Substring(7, 11)
            Dim strRpta As String = String.Empty

            Dim objAuditoriaConsultarDeuda As New AuditType
            Dim consultaEnvio As New ConsultarDeudaRequest
            Dim consultaRespuesta As New ConsultarDeudaResponse

            With objAuditoriaConsultarDeuda
                .idTransaccion = strIdTransaccion
                .ipApplicacion = strIpAplicacion
                .nombreAplicacion = strNombreAplicacion
                .usuarioAplicacion = strUsuario
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   ---- INICIO ::: AUDITORIA CONSULTAR DEUDA ----")
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  IPAPLICACION : " & strIpAplicacion)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  IDTRANSACCION : " & strIdTransaccion)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  APLICACION : " & strNombreAplicacion)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  USUARIO : " & strUsuario)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   ---- FIN ::: AUDITORIA CONSULTAR DEUDA ----")
            End With

            With consultaEnvio
                .auditRequest = objAuditoriaConsultarDeuda
                .numeroCliente = strCliente
                .version = strVersionSap
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  Numero Cliente : " & strCliente)
                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   INP  Version SAP : " & strVersionSap)
            End With
            consultaRespuesta = ConexionWSDAC().consultarDeuda(consultaEnvio)
            With consultaRespuesta
                strDescripcionRpta = .auditResponse.mensajeRespuesta
                strCodigoRpta = .auditResponse.codigoRespuesta
                IdTransaccion = .auditResponse.idTransaccion

                Select Case Trim(strCodigoRpta)
                    Case "0"
                        'strNombreDeudor = .nombre
                        'dblValorTotal = .monto
                        'strDocumento = .numeroIdentificacionFiscal
                        'bResult = True
                        'strRespuestaConsulta = "0"
                        Dim strType As String = String.Empty
                        If Not .listaBapiret2 Is Nothing Then
                            Dim detRpta As Bapiret2Bean()
                            detRpta = .listaBapiret2

                            If detRpta.Length > 0 Then
                                If detRpta(0).type <> "E" Then
                                    strNombreDeudor = .nombre
                                    dblValorTotal = .monto
                                    strDocumento = .numeroIdentificacionFiscal
                                    bResult = True
                                    strRespuestaConsulta = "0"
                                    objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRespuestaConsulta)
                                Else
                                    bResult = False
                                    strRespuestaConsulta = detRpta(0).message.ToString()
                                    strNombreDeudor = ""
                                    dblValorTotal = 0
                                    strDocumento = ""
                                    strRpta &= "E" 'E == ERROR
                                    strRpta &= "@" & detRpta(0).message & ";" & detRpta(0).type & ";" & detRpta(0).id & ";" & detRpta(0).number
                                    objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRpta)
                                End If
                            Else
                                strNombreDeudor = .nombre
                                dblValorTotal = .monto
                                strDocumento = .numeroIdentificacionFiscal
                                bResult = True
                                strRespuestaConsulta = "0"
                                objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRespuestaConsulta)
                            End If
                        End If
                    Case Else
                        strRespuestaConsulta = "Error Conexion a SAP"
                        strNombreDeudor = ""
                        dblValorTotal = 0
                        strDocumento = ""
                        bResult = False
                End Select
            End With

            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  nombre : " & strNombreDeudor)
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  monto : " & dblValorTotal)
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  numeroIdentificacionFiscal : " & strDocumento)

        Catch ex As Exception
            bResult = False
            strRespuestaConsulta = "Ocurrió un error al intentar obtener los datos."
            strDescripcionRpta = ex.Message
            strNombreDeudor = ""
            dblValorTotal = 0
            strDocumento = ""
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   ERROR Respuesta: " & strRespuestaConsulta)
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   ERROR : " & strDescripcionRpta)
        Finally
            ConexionWSDAC.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  IdTransaccion : " & IdTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  CodigoRpta : " & strCodigoRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  Respuesta : " & strRespuestaConsulta)
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "   OUT  Mensaje Respuesta : " & strDescripcionRpta)

            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "Fin GetDeudaDAC")
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try

        Return bResult
    End Function

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

#End Region

    '//-- GB 05/2015
#Region "DRA"

    Public Function ConexionWSDRA() As ConsultaPagoDRA
        Dim oSwichTransaccional As New ConsultaPagoDRA
        oSwichTransaccional.Url = CStr(ConfigurationSettings.AppSettings("WSConsultaDRA"))
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

    Public Function GetDeudaDRA(ByVal strCliente As String, ByRef strCodIdCliente As String, ByRef strRazonSocialCliente As String, ByRef strImporteDRA As String, ByRef strRespuestaConsulta As String) As DataSet
        Dim strIdentifyLog As String = strCliente
        Dim ds As New DataSet
        Dim table As DataTable = New DataTable("Data")
        Dim strCodigoRpta As String
        Dim strDescripcionRpta As String
        Dim consultaRespuestaCab() As CabeceraDRAType
        Dim consultaRespuesta() As DetalleDRAType

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Inicio GetDeudaDRA")
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")

            Dim strCodAplicacion As String = ConfigurationSettings.AppSettings("CodigoAplicacionDRA")
            Dim strUsuarioAplicacion As String = ConfigurationSettings.AppSettings("UsuarioAplicacionDRA")
            Dim strRpta As String = String.Empty
            Dim txtId As String = "1"

            table.Columns.Add("TIPO_DOCUMENTO", GetType(System.String))
            table.Columns.Add("NRO_DOCUMENTO", GetType(System.String))
            table.Columns.Add("DESCRIPCION_DOCUMENTO", GetType(System.String))
            table.Columns.Add("IMPORTE", GetType(System.String))
            table.Columns.Add("FEC_EMISION", GetType(System.String))
            table.Columns.Add("FEC_VENC", GetType(System.String))
            table.Columns.Add("LINEA", GetType(System.String))
            table.Columns.Add("NRO_CONTRATO_SISACT", GetType(System.String))
            table.Columns.Add("ORIGEN_CUENTA", GetType(System.String))
            table.Columns.Add("COD_CUENTA", GetType(System.String))
            table.Columns.Add("COD_CLIENTE_SAP", GetType(System.String))
            table.Columns.Add("NRO_REFERENCIA_SAP", GetType(System.String))
            table.Columns.Add("ESTADO", GetType(System.String))

            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   INP  Numero Cliente : " & strCliente)
            consultaRespuestaCab = ConexionWSDRA().consultaPagoDRA(txtId, strCodAplicacion, strUsuarioAplicacion, strCliente, consultaRespuesta, strCodigoRpta, strDescripcionRpta)

            Select Case Trim(strCodigoRpta)
                Case "0"
                    'CABECERA
                    strCodIdCliente = consultaRespuestaCab(0).DOCID_CLIENTE
                    strRazonSocialCliente = consultaRespuestaCab(0).RAZON_SOCIAL_CLIENTE
                    strImporteDRA = consultaRespuestaCab(0).IMPORTE_DRA

                    'DETALLE
                    For x As Int32 = 0 To consultaRespuesta.Length - 1
                        Dim dr As DataRow
                        dr = table.NewRow()
                        dr("TIPO_DOCUMENTO") = consultaRespuesta(x).TIPO_DOCUMENTO
                        dr("NRO_DOCUMENTO") = consultaRespuesta(x).NRO_DOCUMENTO
                        dr("DESCRIPCION_DOCUMENTO") = consultaRespuesta(x).DESCRIPCION_DOCUMENTO
                        dr("IMPORTE") = consultaRespuesta(x).IMPORTE
                        dr("FEC_EMISION") = consultaRespuesta(x).FEC_EMISION.ToShortDateString()
                        dr("FEC_VENC") = consultaRespuesta(x).FEC_VENC.ToShortDateString()
                        dr("LINEA") = consultaRespuesta(x).LINEA
                        dr("NRO_CONTRATO_SISACT") = consultaRespuesta(x).NRO_CONTRATO_SISACT
                        dr("ORIGEN_CUENTA") = consultaRespuesta(x).ORIGEN_CUENTA
                        dr("COD_CUENTA") = consultaRespuesta(x).COD_CUENTA
                        dr("COD_CLIENTE_SAP") = consultaRespuesta(x).COD_CLIENTE_SAP
                        dr("NRO_REFERENCIA_SAP") = consultaRespuesta(x).NRO_REFERENCIA_SAP
                        dr("ESTADO") = consultaRespuesta(x).ESTADO

                        table.Rows.Add(dr)
                    Next
                    ds.Tables.Add(table)
                Case Else
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: GetDeudaDRA)"
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR Codigo Respuesta: " & strCodigoRpta & MaptPath)
                    'FIN PROY-140126
                    objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR : " & strDescripcionRpta)
                    strRespuestaConsulta = strDescripcionRpta
            End Select
        Catch ex As Exception
            strRespuestaConsulta = "Ocurrió un error al intentar obtener los datos."
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: GetDeudaDRA)"
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR Codigo Respuesta: " & strCodigoRpta & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   ERROR : " & strDescripcionRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   EXCEPTION : " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   OUT  CodigoRpta : " & strCodigoRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   OUT  Respuesta : " & strRespuestaConsulta)
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "   OUT  Mensaje Respuesta : " & strDescripcionRpta)
            ConexionWSDRA().Dispose()
            consultaRespuesta = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "Fin GetDeudaDRA")
            objFileLog.Log_WriteLog(pathFile, strArchivoDRA, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try

        Return ds
    End Function

#End Region
    '//--


End Class