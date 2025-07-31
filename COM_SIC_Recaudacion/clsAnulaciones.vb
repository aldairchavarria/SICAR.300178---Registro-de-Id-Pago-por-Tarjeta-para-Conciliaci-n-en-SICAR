Imports System.Configuration
Imports System.Text
Imports SwichTransaccional.Services

Public Class clsAnulaciones
    Public gstrTracePago As String
    Public gstrTraceAnulacion As String

    Public Const gstrCodTelefono = "01"
    Public Const gstrEstadoPago = 1
    Public Const gstrEstadoAnulado = 2
    Public sbLineasLog As StringBuilder

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    'PROY-140126 INI
    Public nameFileRfp As String = ConfigurationSettings.AppSettings("constNameLogRecFijoPaginas")
    Public strArchivoRfp As String = objFileLog.Log_CrearNombreArchivo(nameFileRfp)
    'PROY 140126 FIN
    Dim oSwichTransaccional As New ServiciosClaro

    Public Sub New()
        oSwichTransaccional.Url = CStr(ConfigurationSettings.AppSettings("ServiciosClaroURL"))
        oSwichTransaccional.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim iTimeOut As Int32 = 0
        Dim strTimeOut As String = ConfigurationSettings.AppSettings("constSICARAnulacionesST_Timeout")
        If Not strTimeOut Is Nothing Then
            If strTimeOut <> "" Then
                iTimeOut = Convert.ToInt32(strTimeOut)
            End If
        End If
        oSwichTransaccional.Timeout = iTimeOut
    End Sub

    Public Function Anular(ByVal strLogSET As String, _
                        ByVal strNivelLogSet As String, _
                        ByVal strCodigoPuntoDeVenta As String, _
                        ByVal strCanal As String, _
                        ByVal strBinAdquiriente As String, _
                        ByVal strCodComercio As String, _
                        ByVal strCodigoCajero As String, _
                        ByVal strTipoIdentificadorDeudor As String, _
                        ByVal strNumeroIdentificadorDeudor As String, _
                        ByVal strRecibosAnular As String, _
                        Optional ByVal strCodigoPlazaRecaudador As String = "", _
                        Optional ByVal strCodigoAgenciaRecaudador As String = "", _
                        Optional ByVal strCodigoCiudadRecaudador As String = "") As String


        Dim strIdentifyLog As String = strTipoIdentificadorDeudor & "|" & strNumeroIdentificadorDeudor
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Anular")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

        Dim intLineasAnular As Integer
        Dim strLineasAnular As String
        Dim arrAnular
        Dim arrLineaAnular

        Dim dblMontoPagar As Double
        Dim strMoneda As String
        Dim strServicio As String
        Dim strTipoDocumento As String
        Dim strNumeroDocumento As String
        Dim strNumeroOperacionCobranzaOriginal As String
        Dim strNumeroOperacionAcreedorOriginal As String
        Dim strTraceOriginal As String
        Dim strFechaHoraTransaccionOriginal As String
        Dim blnAnular As Boolean

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
        'Dim objSAP As New SAP_SIC_Recaudacion.clsRecaudacion
        Dim strResultadoSAP As String
        Dim strRespuestaAnulacion As String

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim intSAP = objOffline.Get_ConsultaSAP
        Dim objSAP As Object

        If intSAP = 1 Then
            objSAP = New SAP_SIC_Recaudacion.clsRecaudacion
        Else
            objSAP = New COM_SIC_OffLine.clsOffline
        End If


        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp strRecibosAnular;" & strRecibosAnular)
        If Trim(strRecibosAnular) <> "" Then
            arrAnular = Split(strRecibosAnular, "|")
            For intLineasAnular = 0 To UBound(arrAnular)
                arrLineaAnular = Split(arrAnular(intLineasAnular), ";")
                dblMontoPagar = CDbl(arrLineaAnular(0))
                strMoneda = arrLineaAnular(1)
                strServicio = Trim(arrLineaAnular(2))
                strNumeroOperacionCobranzaOriginal = Right("000000000000" & arrLineaAnular(3), 12)
                strNumeroOperacionAcreedorOriginal = Right("000000000000" & arrLineaAnular(4), 12)
                strTipoDocumento = arrLineaAnular(5)
                strNumeroDocumento = arrLineaAnular(6)
                strTraceOriginal = arrLineaAnular(7)
                strFechaHoraTransaccionOriginal = arrLineaAnular(8)

                Dim posTmp As String = CStr(intLineasAnular + 1).PadLeft(6, "0")

                'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Inicio Set_EstadoRecaudacion")
                'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp strTraceOriginal;" & strTraceOriginal)
                'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp gstrEstadoAnulado;" & gstrEstadoAnulado)
                'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp posTmp;" & posTmp)
                'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp gstrTraceAnulacion;" & gstrTraceAnulacion)

                'Dim dsLog As DataSet = objSAP.Set_EstadoRecaudacion(strTraceOriginal, gstrEstadoAnulado, posTmp, gstrTraceAnulacion)

                'Dim i As Integer
                'Dim blnError As Boolean
                'blnError = False
                'For i = 0 To dsLog.Tables(0).Rows.Count - 1
                '    If dsLog.Tables(0).Rows(i).Item("TYPE") = "E" Then
                '        blnError = True
                '    End If
                'Next

                'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out blnError;" & blnError)
                'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Fin Set_EstadoRecaudacion")

                'If Not blnError Then

                If AnularPagoST(strLogSET, strNivelLogSet, strCodigoPuntoDeVenta, _
                        strCanal, _
                        strBinAdquiriente, _
                        strCodComercio, _
                        strCodigoCajero, _
                        strTipoIdentificadorDeudor, _
                        strNumeroIdentificadorDeudor, _
                        dblMontoPagar, _
                        strMoneda, _
                        strServicio, _
                        strTipoDocumento, strNumeroDocumento, _
                        strNumeroOperacionCobranzaOriginal, _
                        strNumeroOperacionAcreedorOriginal, _
                        strTraceOriginal, _
                        strFechaHoraTransaccionOriginal, _
                        strRespuestaAnulacion, strOrigenRpta, strCodigoRpta, strDescripcionRpta, _
                        strNombreDeudor, _
                        strRucDeudor, _
                        strNumeroOperacionCobranza, _
                        strNumeroOperacionAcreedor, _
                        dblValorTotal, _
                        intNumeroDocumentos, _
                        strDocumentos, _
                        strFechaHoraTransaccion, _
                        strCodigoPlazaRecaudador, strCodigoAgenciaRecaudador, strCodigoCiudadRecaudador) Then


                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Inicio Set_EstadoRecaudacion")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp strTraceOriginal;" & strTraceOriginal)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp gstrEstadoAnulado;" & gstrEstadoAnulado)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp posTmp;" & posTmp)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp gstrTraceAnulacion;" & gstrTraceAnulacion)

                    Dim dsLog As DataSet = objSAP.Set_EstadoRecaudacion(strTraceOriginal, gstrEstadoAnulado, posTmp, gstrTraceAnulacion)
                    Dim i As Integer
                    Dim blnError As Boolean
                    blnError = False
                    For i = 0 To dsLog.Tables(0).Rows.Count - 1
                        If dsLog.Tables(0).Rows(i).Item("TYPE") = "E" Then
                            'INI PROY-140126
                            Dim MaptPath As String
                            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                            MaptPath = "( Class : " & MaptPath & "; Function: Anular)"
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out ERROR: " & dsLog.Tables(0).Rows(i).Item("MESSAGE") & MaptPath)
                            'FIN PROY-140126                           
                            blnError = True
                        End If
                    Next

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out blnError;" & blnError)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Fin Set_EstadoRecaudacion")

                Else
                    Anular = strCodigoRpta & "@" & strDescripcionRpta & "@"
                End If
                'End If
            Next
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Anular")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

        'Catch ex As Exception
        '    Anular = "8@Verifique los datos ingresados. Por favor vuelva a intentar." & "@"
        'End Try

    End Function

    Public Function AnularPagoST(ByVal strLogSET As String, ByVal strNivelLogSet As String, ByVal strCodigoPuntoDeVenta As String, _
             ByVal strCanal As String, _
             ByVal strBinAdquiriente As String, _
             ByVal strCodComercio As String, _
             ByVal strCodigoCajero As String, _
             ByVal strTipoIdentificadorDeudor As String, _
             ByVal strNumeroIdentificadorDeudor As String, _
             ByVal dblMontoPagar As Double, _
             ByVal strMoneda As String, _
             ByVal strServicio As String, _
             ByVal strTipoDocumento As String, ByVal strNumeroDocumento As String, _
             ByVal strNumeroOperacionCobranzaOriginal As String, _
             ByVal strNumeroOperacionAcreedorOriginal As String, _
             ByVal strTraceOriginal As String, _
             ByVal strFechaHoraTransaccionOriginal As String, _
             ByRef strRespuestaAnulacion As String, ByRef strOrigenRpta As String, ByRef strCodigoRpta As String, ByRef strDescripcionRpta As String, _
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
             Optional ByVal strCodigoCiudadRecaudador As String = "") As Boolean


        Dim strIdentifyLog As String = strTipoIdentificadorDeudor & "|" & strNumeroIdentificadorDeudor
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio AnularPagoST")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

        Dim objComponente
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
        Dim strLinDocs As String

        Dim intNumDocs As Integer
        'Dim clsSap As New SAP_SIC_Recaudacion.clsRecaudacion
        Dim strIngreso As String

        Dim lngLog As Long
        Dim strLinDocsLog As String
        Dim strDocumentosLog As String

        Dim strFecha As String
        Dim strHora As String
        Dim strFechaOrig As String
        Dim strHoraOrig As String

        Dim strDescripcionRptaAux As String

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        'Dim intSAP = objOffline.Get_ConsultaSAP
        'Dim clsSap As Object

        'CAMBIADO POR FFS INICIO
        'If intSAP = 1 Then
        '    clsSap = New SAP_SIC_Recaudacion.clsRecaudacion
        'Else
        '    clsSap = New COM_SIC_OffLine.clsOffline
        'End If


        '  Try
        'objComponente = CreateObject("OLCPVUPagos.clsOLCPVUPagos")

        'OBTENER NRO TRACE DE SAP
        'lngLog = objComponente.SetEnv(strLogSET, strNivelLogSet)

        'CAMBIADO POR FFS FIN

        strIngreso = ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";;" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero
        'strTrace = clsSap.Set_LogRecaudacion(strIngreso, "")

        'strTrace = objOffline.Set_LogRecaudacion(strIngreso, "")


        'CARIAS: Esto se hace por emergencia, no se debe pasar de 1000000 09/02/2007
        'If CDbl(strTrace) > 999999 Then
        'strTrace = Format(CDbl(strTrace) - 999999, "0000000000")
        'End If
        'strTrace = Right(strTrace, 6)
        'strTrace = "0000" & strTrace
        'CARIAS: fin
        strTrace = Convert.ToInt32(strTrace).ToString()
        gstrTraceAnulacion = strTrace
        strTraceOriginal = Convert.ToInt32(strTraceOriginal).ToString()

        'Dim wsSwitchTransaccional As New ServiciosClaro
        Dim anulacionPago As New AnulacionPagoRequest
        Dim anulacionPagoRespuesta As New AnulacionPagoResponse
        With anulacionPago
            .binAdquiriente = strBinAdquiriente
            .binAdquirienteReenvia = ConfigurationSettings.AppSettings("CONST_BIN_REENVIO")
            .canal = strCanal
            .codigoMoneda = strMoneda
            .fechaCaptura = Date.Now.ToString("yyyy-MM-dd-05:00")
            .fechaTransaccion = Date.Now.ToString("yyyy-MM-ddTHH:mm:ss.ms-05:00")
            .fechaTransaccionSpecified = True
            .fechaCapturaSpecified = True
            .numeroTerminal = strBinAdquiriente
            .trace = strTrace
            .acreedor = ConfigurationSettings.AppSettings("CONST_ACREEDOR")
            .agencia = ConfigurationSettings.AppSettings("CONST_AGENCIA")
            .binAdquirienteOriginal = strBinAdquiriente
            .binAdquirienteReenvia = ConfigurationSettings.AppSettings("CONST_BIN_REENVIO")
            .ciudad = strCodigoCiudadRecaudador
            .fechaTransaccionOriginal = strFechaHoraTransaccionOriginal
            .fechaTransaccionOriginalSpecified = True
            .importePago = String.Format("{0:F}", dblMontoPagar)
            .importePagoSpecified = True
            .numeroIdentificacionDeudor = strNumeroIdentificadorDeudor
            .numeroOperacionAcreedorOriginal = "000000000000" 'strNumeroOperacionAcreedorOriginal
            .numeroOperacionCobranzaOriginal = strNumeroOperacionCobranzaOriginal
            .plaza = ""
            .procesador = ConfigurationSettings.AppSettings("CONST_PROCESADOR")
            .tipoIdentificacionDeudor = "01"
            .traceOriginal = strTraceOriginal
        End With

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  TerminalID : " & strCodigoPuntoDeVenta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Trace : " & strTrace)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Canal : " & strCanal)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  TipoIdentificacionDeudor : " & strTipoIdentificadorDeudor)

        '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  NumeroIdentificacionDeudor : " & strNumeroIdentificadorDeudor)

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
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strNumeroOperacionCobranzaOriginal : " & strNumeroOperacionCobranzaOriginal)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strNumeroOperacionAcreedorOriginal : " & strNumeroOperacionAcreedorOriginal)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strTraceOriginal : " & strTraceOriginal)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strFechaHoraTransaccionOriginal : " & strFechaHoraTransaccionOriginal)

        Try
            anulacionPagoRespuesta = oSwichTransaccional.anulacionPago(anulacionPago)
            strDescripcionRpta = anulacionPagoRespuesta.descripcionExtendidaRespuesta
            With anulacionPagoRespuesta
                strRpta = .codigoRespuesta
                strOrigenRpta = .codigoOrigenRespuesta
                strCodigoRpta = .codigoRespuesta
                'strDescripcionRpta = strCodigoPuntoDeVenta & "," & strTrace & "," & strCanal & "," & strTipoIdentificadorDeudor & "," & strNumeroIdentificadorDeudor & ","
                'strDescripcionRpta &= dblMontoPagar & "," & strMoneda & "," & strServicio & "," & strBinAdquiriente & "," & strCodComercio & "," & strTipoDocumento & "," & strNumeroDocumento & ","
                'strDescripcionRpta &= strNumeroOperacionCobranzaOriginal & "," & strNumeroOperacionAcreedorOriginal & "," & strTraceOriginal & "," & strFechaHoraTransaccionOriginal
                strDescripcionRpta = .descripcionExtendidaRespuesta
                strRespuestaAnulacion = strRpta

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Anulacion : " & strRpta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  OrigenRpta : " & strOrigenRpta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CodigoRpta : " & strCodigoRpta)
                'INI PROY-140126
                Dim MaptPath As String
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: AnularPagoST)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  DescripcionRpta : " & strDescripcionRpta & MaptPath)
                'FIN PROY-140126
                Select Case Trim(strRpta)
                    Case "00"
                        strNombreDeudor = .nombreDeudor
                        strRucDeudor = .rucDeudor
                        strNumeroOperacionCobranza = .numeroOperacionCobranza
                        strNumeroOperacionAcreedor = .numeroOperacionAcreedor
                        dblValorTotal = .importePago
                        intNumeroDocumentos = .numeroServiciosAnulados
                        strFechaHoraTransaccion = .fechaTransaccion

                        '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CABECERA : " & strNombreDeudor & ";" & strRucDeudor & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & dblValorTotal.ToString() & ";" & intNumeroDocumentos.ToString() & ";" & strFechaHoraTransaccion & ";" & strFechaHoraTransaccion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  CABECERA : " & ";" & strRucDeudor & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & dblValorTotal.ToString() & ";" & intNumeroDocumentos.ToString() & ";" & strFechaHoraTransaccion & ";" & strFechaHoraTransaccion)


                        For intNumDocs = 0 To .productos.Length - 1
                            strDocsAux = Mid(strDocumentos, (67 * intNumDocs) + 1, 67)
                            strCodServicio = .productos(intNumDocs).codigo
                            strDesServicio = .productos(intNumDocs).descripcion
                            strMonServicio = .productos(intNumDocs).codigoMoneda
                            strTipoDoc = .productos(intNumDocs).documentos(0).tipoDocumento
                            strNumDoc = .productos(intNumDocs).documentos(0).numeroDocumento
                            strFechaEmi = .productos(intNumDocs).documentos(0).fechaEmision
                            strFechaVen = .productos(intNumDocs).documentos(0).fechaVencimiento
                            strMontoDoc = .productos(intNumDocs).documentos(0).importeAnulado
                            If Trim(strMontoDoc) = "" Then
                                strMontoDoc = "0.00"
                            Else
                                strMontoDoc = strMontoDoc
                            End If
                            strLinDocs = strLinDocs & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                            strLinDocsLog = strLinDocsLog & strTrace & ";" & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                        Next
                        strDocumentos = Mid(strLinDocs, 1, Len(strLinDocs) - 1)
                        strDocumentosLog = Mid(strLinDocsLog, 1, Len(strLinDocsLog) - 1)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  RECIBOS : " & strDocumentos)

                        'If Trim(strFechaHoraTransaccion) <> "" Then
                        '    strFecha = Mid(strFechaHoraTransaccion, 3, 2) & "/" & Mid(strFechaHoraTransaccion, 1, 2) & "/" & Year(Now)
                        '    strHora = Mid(strFechaHoraTransaccion, 5, 2) & ":" & Mid(strFechaHoraTransaccion, 7, 2) & ":" & Mid(strFechaHoraTransaccion, 9, 2)
                        'End If

                        'If Trim(strFechaHoraTransaccionOriginal) <> "" Then
                        '    strFechaOrig = Mid(strFechaHoraTransaccionOriginal, 3, 2) & "/" & Mid(strFechaHoraTransaccionOriginal, 1, 2) & "/" & Year(Now)
                        '    strHoraOrig = Mid(strFechaHoraTransaccionOriginal, 5, 2) & ":" & Mid(strFechaHoraTransaccion, 7, 2) & ":" & Mid(strFechaHoraTransaccionOriginal, 9, 2)
                        'End If
                        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";" & strTraceOriginal & ";" & strFechaOrig & ";" & strHoraOrig & ";" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal
                        'strTrace = clsSap.Set_LogRecaudacion(strIngreso, strDocumentosLog)
                        AnularPagoST = True
                    Case Else
                        strNombreDeudor = ""
                        strRucDeudor = ""
                        dblValorTotal = 0
                        strNumeroOperacionCobranza = ""
                        intNumeroDocumentos = 0
                        strDocumentos = ""
                        strFechaHoraTransaccion = ""
                        strRespuestaAnulacion = 1
                        AnularPagoST = False

                        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";" & strTraceOriginal & ";" & strFechaOrig & ";" & strHoraOrig & ";" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal
                        If IsNothing(strDocumentosLog) Then
                            strDocumentosLog = ""
                        End If
                        'strTrace = clsSap.Set_LogRecaudacion(strIngreso, strDocumentosLog)

                        If strOrigenRpta = 3 Then
                            strDescripcionRptaAux = BuscarMensaje(CInt(strCodigoRpta))

                            'Identificar si se trata de un Pago cliente castigado
                            Try
                                If strNumeroIdentificadorDeudor.Length > System.Configuration.ConfigurationSettings.AppSettings("codPrefijoClienteCastigado").ToString().Length _
                                    AndAlso Left(strNumeroIdentificadorDeudor, 3).ToUpper() = System.Configuration.ConfigurationSettings.AppSettings("codPrefijoClienteCastigado").ToString() Then
                                    If strCodigoRpta = 6 Then
                                        strDescripcionRptaAux = System.Configuration.ConfigurationSettings.AppSettings("constMsgAnulacionDXX").ToString()
                                    End If
                                End If
                            Catch ex As Exception
                            End Try

                            If Trim(strDescripcionRptaAux) <> "" Then
                                strDescripcionRpta = strDescripcionRptaAux
                            Else
                                strDescripcionRpta = BuscarMensaje(8)
                            End If
                        Else
                            'HHA: 20051111 MOSTRARA MESAJE DE ERROR GNERAL PARA LOS DEMAS CASOS DE CODIGO DE ORIGEN DIFERENTE DE 3
                            strDescripcionRpta = BuscarMensaje(8)
                        End If
                End Select
            End With
            'clsSap = Nothing

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin AnularPagoST")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            Return anulacionPagoRespuesta.codigoRespuesta.StartsWith("00")
        Catch webEx As System.Net.WebException
            If CType(webEx, System.Net.WebException).Status = Net.WebExceptionStatus.Timeout Then
                strDescripcionRpta = "Tiempo de espera excedido."
            Else
                strDescripcionRpta = webEx.Message
            End If
            AnularPagoST = False
            strNombreDeudor = ""
            strRucDeudor = ""
            dblValorTotal = 0
            strNumeroOperacionCobranza = ""
            intNumeroDocumentos = 0
            strDocumentos = ""
            strFechaHoraTransaccion = ""
            strRespuestaAnulacion = 1
            anulacionPago = Nothing
            anulacionPagoRespuesta = Nothing
            oSwichTransaccional.Dispose()
        Catch ex As Exception
            strDescripcionRpta = ex.Message
            strNombreDeudor = ""
            strRucDeudor = ""
            dblValorTotal = 0
            strNumeroOperacionCobranza = ""
            intNumeroDocumentos = 0
            strDocumentos = ""
            strFechaHoraTransaccion = ""
            strRespuestaAnulacion = 1
            AnularPagoST = False
            anulacionPago = Nothing
            anulacionPagoRespuesta = Nothing
            oSwichTransaccional.Dispose()
            'clsSap = Nothing
        Finally
            anulacionPago = Nothing
            anulacionPagoRespuesta = Nothing
            oSwichTransaccional.Dispose()
        End Try
    End Function

    '///E75810
    Private Function AnularPagoST_23( _
                             ByVal strLogSET As String, _
                             ByVal strNivelLogSet As String, _
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
                             ByVal strNumeroOperacionCobranzaOriginal As String, _
                             ByVal strNumeroOperacionAcreedorOriginal As String, _
                             ByVal strTraceOriginal As String, _
                             ByVal strFechaHoraTransaccionOriginal As String, _
                             ByRef strRespuestaAnulacion As String, _
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
                             Optional ByVal strCodigoCiudadRecaudador As String = "") As Boolean

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
        Dim strLinDocs As String

        Dim intNumDocs As Integer
        'Dim clsSap As New SAP_SIC_Recaudacion.clsRecaudacion
        Dim strIngreso As String

        Dim lngLog As Long
        Dim strLinDocsLog As String
        Dim strDocumentosLog As String

        Dim strFecha As String
        Dim strHora As String
        Dim strFechaOrig As String
        Dim strHoraOrig As String
        Dim strDescripcionRptaAux As String

        '--determina si hay conexion SAP
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim intSAP = objOffline.Get_ConsultaSAP
        objOffline = Nothing
        '--crea objeto LOG
        'INI PROY-140126
        Dim sMarLeft As String = "[" & strArchivoRfp & "] " & Date.Now.ToString("yyyy-MM-dd-hh:mm:ss") & " -" & strNumeroIdentificadorDeudor & "- " & vbTab & vbTab
        'FIN PROY 140126
        sbLineasLog.Append(sMarLeft & "--------------------------------------------------------" & vbCrLf)
        sbLineasLog.Append(sMarLeft & "AnularPagoST_23 (Fijo y Páginas) - Inicio " & vbCrLf)
        sbLineasLog.Append(sMarLeft & "--------------------------------------------------------" & vbCrLf)
        sMarLeft = sMarLeft & vbTab
        '--crea objeto para ANulacion
        Dim clsSap As New COM_SIC_OffLine.clsOffline

        Dim anularPagoEnvio As New AnulacionPagoRequest
        Dim anularPagoResultado As New AnulacionPagoResponse
        'Dim wsSwichTransaccional As New ServiciosClaro

        Try
            'If intSAP = 1 Then
            '    clsSap = New SAP_SIC_Recaudacion.clsRecaudacion
            'Else
            clsSap = New COM_SIC_OffLine.clsOffline
            'End If

            'objComponente = CreateObject("OLCPVUPagos.clsOLCPVUPagos")
            '--Obtiene NRO TRACE DE SAP
            'lngLog = objComponente.SetEnv(strLogSET, strNivelLogSet)
            strIngreso = ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";;" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero
            strTrace = clsSap.Set_LogRecaudacion(strIngreso, "")
            strTrace = "0000" & strTrace
            strTrace = Right(strTrace, 6)
            gstrTraceAnulacion = strTrace

            With anularPagoEnvio
                .binAdquiriente = strBinAdquiriente
                .binAdquirienteReenvia = ConfigurationSettings.AppSettings("CONST_BIN_REENVIO")
                .canal = strCanal
                .codigoMoneda = strMoneda
                .fechaCaptura = Date.Now.ToString("yyyy-MM-dd-05:00")
                .fechaTransaccion = Date.Now.ToString("yyyy-MM-ddTHH:mm:ss.ms-05:00")
                .fechaTransaccionSpecified = True
                .fechaCapturaSpecified = True
                .numeroReferencia = ConfigurationSettings.AppSettings("CONST_NUMEROREFERENCIA")
                .numeroTerminal = strBinAdquiriente
                .trace = strTrace
                .acreedor = ConfigurationSettings.AppSettings("CONST_ACREEDOR")
                .agencia = ConfigurationSettings.AppSettings("CONST_AGENCIA")
                .binAdquirienteOriginal = strBinAdquiriente
                .binAdquirienteReenvioOriginal = ConfigurationSettings.AppSettings("CONST_BIN_REENVIO")
                .ciudad = strCodigoCiudadRecaudador
                .codigoFormato = ConfigurationSettings.AppSettings("CONST_CODIGO_FORMATO")
                .fechaTransaccionOriginal = strFechaHoraTransaccionOriginal
                .fechaTransaccionOriginalSpecified = True
                .importePago = String.Format("{0:F}", dblMontoPagar)
                .importePagoSpecified = True
                .numeroIdentificacionDeudor = strNumeroIdentificadorDeudor
                .numeroOperacionAcreedorOriginal = "000000000000" 'strNumeroOperacionAcreedorOriginal
                .numeroOperacionCobranzaOriginal = strNumeroOperacionCobranzaOriginal
                .plaza = ""
                .procesador = ConfigurationSettings.AppSettings("CONST_PROCESADOR")
                .tipoIdentificacionDeudor = "01"
                .traceOriginal = strTraceOriginal
            End With

            sbLineasLog.Append(sMarLeft & "INP TerminalID: " & strCodigoPuntoDeVenta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP Trace: " & strTrace & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP Canal: " & strCanal & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP TipoIdentificacionDeudor: " & strTipoIdentificadorDeudor & vbCrLf)

            '''sbLineasLog.Append(sMarLeft & "INP NumeroIdentificacionDeudor: " & strNumeroIdentificadorDeudor & vbCrLf)

            sbLineasLog.Append(sMarLeft & "INP Monto: " & dblMontoPagar & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP Moneda: " & strMoneda & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP Servicio: " & strServicio & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP CodigoPlazaRecaudador: " & strCodigoPlazaRecaudador & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP CodigoAgenciaRecaudador: " & strCodigoAgenciaRecaudador & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP CodigoCiudadRecaudador: " & strCodigoCiudadRecaudador & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP BinAdquiriente: " & strBinAdquiriente & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP CodComercio: " & strCodComercio & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP TipoDocumento: " & strTipoDocumento & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP NumeroDocumento: " & strNumeroDocumento & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP NumeroOperacionCobranzaOriginal: " & strNumeroOperacionCobranzaOriginal & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP NumeroOperacionAcreedorOriginal: " & strNumeroOperacionAcreedorOriginal & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP TraceOriginal: " & strTraceOriginal & vbCrLf)
            sbLineasLog.Append(sMarLeft & "INP FechaHoraTransaccionOriginal: " & strFechaHoraTransaccionOriginal & vbCrLf)

            '''ANULAMOS EL PAGO
            anularPagoResultado = oSwichTransaccional.anulacionPago(anularPagoEnvio)


            With anularPagoResultado
                strOrigenRpta = .codigoOrigenRespuesta
                strCodigoRpta = .codigoRespuesta
                strDescripcionRpta = .descripcionRespuesta
                strRpta = .codigoRespuesta
                strRespuestaAnulacion = strRpta

                Select Case Trim(strRpta)
                    Case "00"
                        strNombreDeudor = .nombreDeudor
                        strRucDeudor = .rucDeudor
                        strNumeroOperacionCobranza = .numeroOperacionCobranza
                        strNumeroOperacionAcreedor = .numeroOperacionAcreedor
                        dblValorTotal = .importePago
                        intNumeroDocumentos = .productos(0).numeroDocumentosAnulados
                        strFechaHoraTransaccion = .fechaTransaccion

                        '''AÑADIDO PARA RECUPERAR LA TRAMA DE DOCUMENTOS
                        Dim doc As New Documento
                        For intNumDocs = 0 To .productos(0).documentos.Length - 1
                            Dim documento As DocumentoAnulado = .productos(0).documentos(intNumDocs)
                            Dim fechaEmicion$ = documento.fechaEmision '.ToString("yyyyMMdd")
                            Dim fechaVencimiento$ = documento.fechaVencimiento '.ToString("yyyyMMdd")

                            strDocumentos = strDocumentos & String.Format("{0};{1};{2};{3};{4};{5};{6}{7:0##########}|", .productos(0).codigo, .productos(0).descripcion, .productos(0).codigoMoneda, .productos(0).codigo, documento.numeroDocumento, fechaEmicion, fechaVencimiento, documento.importeAnulado * 100)

                            strDocsAux = Mid(strDocumentos, (67 * intNumDocs) + 1, 67)
                            strCodServicio = .productos(0).codigo
                            strDesServicio = .productos(0).descripcion
                            strMonServicio = .productos(0).codigoMoneda
                            strTipoDoc = Trim(Mid(strDocsAux, 22, 3))
                            strNumDoc = documento.numeroDocumento
                            strFechaEmi = documento.fechaEmision '.ToShortDateString
                            strFechaVen = documento.fechaVencimiento '.ToShortDateString
                            strMontoDoc = .productos(0).importeAnulado
                            If Trim(strMontoDoc) = String.Empty Then
                                strMontoDoc = "0.00"
                            Else
                                strMontoDoc = strMontoDoc
                            End If
                            strLinDocs = strLinDocs & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                            strLinDocsLog = strLinDocsLog & strTrace & ";" & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                        Next
                        '''AÑADIDO HASTA AQUI



                        'strDocumentos = .Documentos

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
                                strMontoDoc = strMontoDoc
                            End If
                            strLinDocs = strLinDocs & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                            strLinDocsLog = strLinDocsLog & strTrace & ";" & strCodServicio & ";" & strDesServicio & ";" & strMonServicio & ";" & strTipoDoc & ";" & strNumDoc & ";" & strFechaEmi & ";" & strFechaVen & ";" & strMontoDoc & "|"
                        Next
                        strDocumentos = Mid(strLinDocs, 1, Len(strLinDocs) - 1)
                        strDocumentosLog = Mid(strLinDocsLog, 1, Len(strLinDocsLog) - 1)

                        If Trim(strFechaHoraTransaccion) <> String.Empty Then
                            strFecha = Mid(strFechaHoraTransaccion, 3, 2) & "/" & Mid(strFechaHoraTransaccion, 1, 2) & "/" & Year(Now)
                            strHora = Mid(strFechaHoraTransaccion, 5, 2) & ":" & Mid(strFechaHoraTransaccion, 7, 2) & ":" & Mid(strFechaHoraTransaccion, 9, 2)
                        End If

                        If Trim(strFechaHoraTransaccionOriginal) <> String.Empty Then
                            strFechaOrig = Mid(strFechaHoraTransaccionOriginal, 3, 2) & "/" & Mid(strFechaHoraTransaccionOriginal, 1, 2) & "/" & Year(Now)
                            strHoraOrig = Mid(strFechaHoraTransaccionOriginal, 5, 2) & ":" & Mid(strFechaHoraTransaccion, 7, 2) & ":" & Mid(strFechaHoraTransaccionOriginal, 9, 2)
                        End If

                        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";" & strTraceOriginal & ";" & strFechaOrig & ";" & strHoraOrig & ";" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal
                        strTrace = clsSap.SetLogRecaudacion(strIngreso, strDocumentosLog)
                        '-----
                        AnularPagoST_23 = True

                    Case Else  '//no se pudo anular el documento
                        strNombreDeudor = String.Empty
                        strRucDeudor = String.Empty
                        dblValorTotal = 0
                        strNumeroOperacionCobranza = String.Empty
                        intNumeroDocumentos = 0
                        strDocumentos = String.Empty
                        strFechaHoraTransaccion = String.Empty
                        strRespuestaAnulacion = 1
                        AnularPagoST_23 = False

                        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";" & strTraceOriginal & ";" & strFechaOrig & ";" & strHoraOrig & ";" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal
                        If IsNothing(strDocumentosLog) Then
                            strDocumentosLog = String.Empty
                        End If
                        strTrace = clsSap.Set_LogRecaudacion(strIngreso, strDocumentosLog)

                        strDescripcionRpta = clsMensajes.DeterminaMensaje(clsMensajes.kGrupo_FijoPaginas, CInt(strCodigoRpta), strDescripcionRpta)

                        '''If strOrigenRpta = 3 Then
                        '''    strDescripcionRptaAux = BuscarMensaje(CInt(strCodigoRpta))
                        '''    If Trim(strDescripcionRptaAux) <> String.Empty Then
                        '''        strDescripcionRpta = strDescripcionRptaAux
                        '''    Else
                        '''        strDescripcionRpta = BuscarMensaje(8)
                        '''    End If
                        '''Else
                        '''    'HHA: 20051111 MOSTRARA MESAJE DE ERROR GNERAL PARA LOS DEMAS CASOS DE CODIGO DE ORIGEN DIFERENTE DE 3
                        '''    strDescripcionRpta = BuscarMensaje(8)
                        '''End If
                End Select
            End With

            'With objComponente
            '    '--setea datos para anulación
            '    .TerminalID = strCodigoPuntoDeVenta
            '    .Trace = strTrace
            '    .Canal = strCanal
            '    .TipoIdentificacionDeudor = strTipoIdentificadorDeudor
            '    .NumeroIdentificacionDeudor = strNumeroIdentificadorDeudor
            '    .Monto = dblMontoPagar
            '    .Moneda = strMoneda
            '    .Servicio = strServicio
            '    .CodigoPlazaRecaudador = strCodigoPlazaRecaudador
            '    .CodigoAgenciaRecaudador = strCodigoAgenciaRecaudador
            '    .CodigoCiudadRecaudador = strCodigoCiudadRecaudador
            '    .BinAdquiriente = strBinAdquiriente
            '    .CodComercio = strCodComercio
            '    .TipoDocumento = strTipoDocumento
            '    .NumeroDocumento = strNumeroDocumento
            '    .NumeroOperacionCobranzaOriginal = strNumeroOperacionCobranzaOriginal
            '    .NumeroOperacionAcreedorOriginal = strNumeroOperacionAcreedorOriginal
            '    .TraceOriginal = strTraceOriginal
            '    .FechaHoraTransaccionOriginal = strFechaHoraTransaccionOriginal
            '----LOGS
            'sbLineasLog.Append(sMarLeft & "INP TerminalID: " & strCodigoPuntoDeVenta & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP Trace: " & strTrace & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP Canal: " & strCanal & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP TipoIdentificacionDeudor: " & strTipoIdentificadorDeudor & vbCrLf)

            ''''sbLineasLog.Append(sMarLeft & "INP NumeroIdentificacionDeudor: " & strNumeroIdentificadorDeudor & vbCrLf)

            'sbLineasLog.Append(sMarLeft & "INP Monto: " & dblMontoPagar & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP Moneda: " & strMoneda & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP Servicio: " & strServicio & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP CodigoPlazaRecaudador: " & strCodigoPlazaRecaudador & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP CodigoAgenciaRecaudador: " & strCodigoAgenciaRecaudador & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP CodigoCiudadRecaudador: " & strCodigoCiudadRecaudador & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP BinAdquiriente: " & strBinAdquiriente & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP CodComercio: " & strCodComercio & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP TipoDocumento: " & strTipoDocumento & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP NumeroDocumento: " & strNumeroDocumento & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP NumeroOperacionCobranzaOriginal: " & strNumeroOperacionCobranzaOriginal & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP NumeroOperacionAcreedorOriginal: " & strNumeroOperacionAcreedorOriginal & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP TraceOriginal: " & strTraceOriginal & vbCrLf)
            'sbLineasLog.Append(sMarLeft & "INP FechaHoraTransaccionOriginal: " & strFechaHoraTransaccionOriginal & vbCrLf)

            '---realiza anulación
            'strRpta = .Anulacion
            '--obtiene datos de resultado
            'strOrigenRpta = .OrigenRpta
            'strCodigoRpta = .CodigoRpta
            'strDescripcionRpta = .DescripcionRpta
            'strRespuestaAnulacion = strRpta

            'Select Case Trim(strRpta)
            '    Case "0"
            '        strNombreDeudor = .NombreDeudor
            '        strRucDeudor = .RUCDeudor
            '        strNumeroOperacionCobranza = .NumeroOperacionCobranza
            '        strNumeroOperacionAcreedor = .NumeroOperacionAcreedor
            '        dblValorTotal = .ValorTotal
            '        intNumeroDocumentos = .NumeroDocumentos
            '        strFechaHoraTransaccion = .FechaHoraTransaccion

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

            '        If Trim(strFechaHoraTransaccion) <> String.Empty Then
            '            strFecha = Mid(strFechaHoraTransaccion, 3, 2) & "/" & Mid(strFechaHoraTransaccion, 1, 2) & "/" & Year(Now)
            '            strHora = Mid(strFechaHoraTransaccion, 5, 2) & ":" & Mid(strFechaHoraTransaccion, 7, 2) & ":" & Mid(strFechaHoraTransaccion, 9, 2)
            '        End If

            '        If Trim(strFechaHoraTransaccionOriginal) <> String.Empty Then
            '            strFechaOrig = Mid(strFechaHoraTransaccionOriginal, 3, 2) & "/" & Mid(strFechaHoraTransaccionOriginal, 1, 2) & "/" & Year(Now)
            '            strHoraOrig = Mid(strFechaHoraTransaccionOriginal, 5, 2) & ":" & Mid(strFechaHoraTransaccion, 7, 2) & ":" & Mid(strFechaHoraTransaccionOriginal, 9, 2)
            '        End If

            '        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";" & strTraceOriginal & ";" & strFechaOrig & ";" & strHoraOrig & ";" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal
            '        strTrace = clsSap.SetLogRecaudacion(strIngreso, strDocumentosLog)
            '        '-----
            '        AnularPagoST_23 = True

            '    Case Else  '//no se pudo anular el documento
            '        strNombreDeudor = String.Empty
            '        strRucDeudor = String.Empty
            '        dblValorTotal = 0
            '        strNumeroOperacionCobranza = String.Empty
            '        intNumeroDocumentos = 0
            '        strDocumentos = String.Empty
            '        strFechaHoraTransaccion = String.Empty
            '        strRespuestaAnulacion = 1
            '        AnularPagoST_23 = False

            '        strIngreso = strTrace & ";" & strCodigoPuntoDeVenta & ";" & strCanal & ";" & strTipoIdentificadorDeudor & ";" & strNumeroIdentificadorDeudor & ";" & dblMontoPagar & ";" & strMoneda & ";" & strServicio & ";" & strCodigoPlazaRecaudador & ";" & strCodigoAgenciaRecaudador & ";" & strCodigoCiudadRecaudador & ";" & strBinAdquiriente & ";" & strCodComercio & ";" & strNumeroOperacionCobranza & ";" & strNumeroOperacionAcreedor & ";" & strTipoDocumento & ";" & strNumeroDocumento & ";;" & strCodigoCajero & ";" & strNombreDeudor & ";" & strRucDeudor & ";" & intNumeroDocumentos & ";" & strFecha & ";" & strHora & ";" & strTraceOriginal & ";" & strFechaOrig & ";" & strHoraOrig & ";" & strOrigenRpta & ";" & strCodigoRpta & ";" & strDescripcionRpta & ";" & dblValorTotal
            '        If IsNothing(strDocumentosLog) Then
            '            strDocumentosLog = String.Empty
            '        End If
            '        strTrace = clsSap.Set_LogRecaudacion(strIngreso, strDocumentosLog)

            '        strDescripcionRpta = clsMensajes.DeterminaMensaje(clsMensajes.kGrupo_FijoPaginas, CInt(strCodigoRpta), strDescripcionRpta)

            '        '''If strOrigenRpta = 3 Then
            '        '''    strDescripcionRptaAux = BuscarMensaje(CInt(strCodigoRpta))
            '        '''    If Trim(strDescripcionRptaAux) <> String.Empty Then
            '        '''        strDescripcionRpta = strDescripcionRptaAux
            '        '''    Else
            '        '''        strDescripcionRpta = BuscarMensaje(8)
            '        '''    End If
            '        '''Else
            '        '''    'HHA: 20051111 MOSTRARA MESAJE DE ERROR GNERAL PARA LOS DEMAS CASOS DE CODIGO DE ORIGEN DIFERENTE DE 3
            '        '''    strDescripcionRpta = BuscarMensaje(8)
            '        '''End If
            'End Select
            '-----

            'End With
        Catch webEx As System.Net.WebException
            If CType(webEx, System.Net.WebException).Status = Net.WebExceptionStatus.Timeout Then
                strDescripcionRpta = "Tiempo de espera excedido."
            Else
                strDescripcionRpta = webEx.Message
            End If
            anularPagoEnvio = Nothing
            anularPagoResultado = Nothing
            clsSap = Nothing
            strRpta = "-1001"
            oSwichTransaccional.Dispose()
        Catch ex As Exception
            '---
            clsSap = Nothing
            strRpta = "-1001"
            strDescripcionRpta = ex.Message
            anularPagoEnvio = Nothing
            anularPagoResultado = Nothing
            oSwichTransaccional.Dispose()
        Finally
            anularPagoEnvio = Nothing
            anularPagoResultado = Nothing
            oSwichTransaccional.Dispose()
            sbLineasLog.Append(sMarLeft & "OUT strRpta(funcion): " & strRpta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "OUT OrigenRpta: " & strOrigenRpta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "OUT CodigoRpta: " & strCodigoRpta & vbCrLf)
            sbLineasLog.Append(sMarLeft & "OUT DescripcionRpta: " & strDescripcionRpta & vbCrLf)
            sMarLeft = sMarLeft.Substring(0, sMarLeft.Length - 1)
            sbLineasLog.Append(sMarLeft & "---------------------------------------------------------------" & vbCrLf)
            sbLineasLog.Append(sMarLeft & "AnularPagoST_23 - Fin" & vbCrLf)
            sbLineasLog.Append(sMarLeft & "---------------------------------------------------------------" & vbCrLf)
        End Try

    End Function

    '////E75810
    Public Function AnularPago( _
       ByVal intCodAplicacion As Integer, _
       ByVal strCanalOF As String, _
       ByVal strLogSET As String, _
        ByVal strNivelLogSet As String, _
        ByVal strCodigoPuntoDeVenta As String, _
        ByVal strCanal As String, _
        ByVal strBinAdquiriente As String, _
        ByVal strCodComercio As String, _
        ByVal strCodigoCajero As String, _
        ByVal strTransaccion As String, _
        Optional ByVal strCodigoPlazaRecaudador As String = "", _
        Optional ByVal strCodigoAgenciaRecaudador As String = "", _
        Optional ByVal strCodigoCiudadRecaudador As String = "", _
        Optional ByVal blnDescuenta As Boolean = True, _
        Optional ByVal pEstadoTransacion As String = "1", _
        Optional ByVal pTipoCambioFechaAnulacion As Decimal = 0) As String

        Dim strResultado As String '//Para retornar resultado

        Dim strTipoIdentificadorDeudor As String
        Dim strNumeroIdentificadorDeudor As String
        Dim strOficina As String
        Dim strUsuario As String
        Dim strClienteRec As String
        Dim bEsTipo23 As Boolean = False '//Para anulacion de pagos Fijo y Paginas
        Dim blnExito As Boolean

        Dim strPosicion As String
        Dim dblMontoPagar As Double
        Dim strMoneda As String
        Dim strServicio As String
        Dim strTipoDocumento As String
        Dim strNumeroDocumento As String
        Dim strNumeroOperacionCobranzaOriginal As String
        Dim strNumeroOperacionAcreedorOriginal As String
        Dim strTraceOriginal As String
        Dim strFechaHoraTransaccionOriginal As String

        Dim strRespuestaAnulacion As String
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

        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim clsPagos As New clsPagos
        '--
        Dim i As Integer
        Dim sMensaje As String

        '--crear objeto 
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim intSAP = objOffline.Get_ConsultaSAP
        Dim objclsOffline As New COM_SIC_OffLine.clsOffline
        Dim objSAP As New COM_SIC_OffLine.clsOffline
        '''CAMBIADO POR JTN
        'If intSAP = 1 Then
        '    objSAP = New SAP_SIC_Recaudacion.clsRecaudacion
        'Else
        '    objSAP = New COM_SIC_OffLine.clsOffline
        'End If
        '''CAMBIADO HASTA AQUI

        ' Try
        Me.sbLineasLog = New StringBuilder '//crea objeto de lineas para los logs
        '--obtiene
        Dim dsResult As DataSet = objSAP.GetRegistroDeuda(strTransaccion)
        '---
        Dim sMarLeft As String
        Dim strIdentifyLog As String

        Dim codServicio As String

        If (Not dsResult Is Nothing) AndAlso (Not dsResult.Tables Is Nothing) Then

            If dsResult.Tables(0).Rows.Count > 0 Then
                '---
                strTipoIdentificadorDeudor = dsResult.Tables(0).Rows(0)("TIPO_DOC_DEUDOR")     'arrDeuda(14)
                strNumeroIdentificadorDeudor = dsResult.Tables(0).Rows(0)("NRO_DOC_DEUDOR")
                strOficina = dsResult.Tables(0).Rows(0)("OFICINA_VENTA")
                'strUsuario = Right(dsResult.Tables(0).Rows(0)("COD_CAJERO"), 5)
                strUsuario = Trim(dsResult.Tables(0).Rows(0)("COD_CAJERO"))
                strClienteRec = dsResult.Tables(0).Rows(0)("RUC_DEUDOR")

                strIdentifyLog = strTipoIdentificadorDeudor & "|" & strNumeroIdentificadorDeudor
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strTransaccion:" & strTransaccion)
                'INI PROY-140126
                sMarLeft = "[" & strArchivoRfp & "] " & Date.Now.ToString("yyyy-MM-dd-hh:mm:ss") & " -" & strNumeroIdentificadorDeudor & "- " & vbTab & vbTab
                'FIN PROY-140126
                sbLineasLog.Append(sMarLeft & "--------------------------------------------------------" & vbCrLf)
                sbLineasLog.Append(sMarLeft & "Inicio AnularPago  " & vbCrLf)
                sbLineasLog.Append(sMarLeft & "--------------------------------------------------------" & vbCrLf)

                sbLineasLog.Append(sMarLeft & "     strTipoIdentificadorDeudor:" & strTipoIdentificadorDeudor & vbCrLf)
                '''sbLineasLog.Append(sMarLeft & "     strNumeroIdentificadorDeudor:" & strNumeroIdentificadorDeudor & vbCrLf)

                Dim sTotalDocAnulados As Integer = 0
                Dim drRecibo As DataRow
                '''blnExito = True
                blnExito = False

                For Each drRecibo In dsResult.Tables(1).Rows
                    '--recupera datos de recibo actual
                    strPosicion = Convert.ToString(drRecibo(1))
                    dblMontoPagar = drRecibo(6)
                    strMoneda = drRecibo(4)
                    strServicio = Trim(drRecibo(15))
                    If (strServicio = ConfigurationSettings.AppSettings("constCodigoServFactDolares").ToString) Then
                        pEstadoTransacion = "3"
                        pTipoCambioFechaAnulacion = objOffline.Obtener_TipoCambio("")
                    End If

                    strTipoDocumento = drRecibo(2)
                    strNumeroDocumento = drRecibo(3)
                    strNumeroOperacionCobranzaOriginal = Right("000000000000" & drRecibo(7), 12)
                    strNumeroOperacionAcreedorOriginal = Right("000000000000" & drRecibo(8), 12)
                    strTraceOriginal = drRecibo(12)
                    strFechaHoraTransaccionOriginal = drRecibo(14)
                    sbLineasLog.Append(sMarLeft & "     pEstadoTransacion:" & pEstadoTransacion & vbCrLf)
                    If pEstadoTransacion = CStr(gstrEstadoPago) Then
                        If AnularPagoST(strLogSET, strNivelLogSet, strCodigoPuntoDeVenta, strCanal, _
                                strBinAdquiriente, strCodComercio, strCodigoCajero, strTipoIdentificadorDeudor, _
                                strNumeroIdentificadorDeudor, dblMontoPagar, strMoneda, strServicio, _
                                strTipoDocumento, strNumeroDocumento, strNumeroOperacionCobranzaOriginal, strNumeroOperacionAcreedorOriginal, _
                                strTraceOriginal, strFechaHoraTransaccionOriginal, strRespuestaAnulacion, strOrigenRpta, _
                                strCodigoRpta, strDescripcionRpta, strNombreDeudor, strRucDeudor, _
                                strNumeroOperacionCobranza, strNumeroOperacionAcreedor, dblValorTotal, intNumeroDocumentos, _
                                strDocumentos, strFechaHoraTransaccion, strCodigoPlazaRecaudador, strCodigoAgenciaRecaudador, _
                                strCodigoCiudadRecaudador) Then
                            sTotalDocAnulados = sTotalDocAnulados + 1

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Set_EstadoRecaudacion")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	INP strTransaccion:" & strTransaccion)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	INP gstrEstadoAnulado:" & gstrEstadoAnulado)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	INP strPosicion:" & strPosicion)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	INP strTransaccion:" & strTransaccion)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "	INP gstrTraceAnulacion:" & gstrTraceAnulacion)

                            'CAMBIADO POR FFS INICIO .......
                            '..........ANULAR EN SICAR
                            Dim dsLog As DataSet = objclsOffline.SetEstadoRecaudacion(strTransaccion, gstrEstadoAnulado, strPosicion, gstrTraceAnulacion)
                            'CAMBIADO POR FFS FIN ..........

                            Try
                                For i = 0 To dsLog.Tables(0).Rows.Count - 1
                                    If dsLog.Tables(0).Rows(i).Item("TYPE") = "E" Then
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " ERROR: " & dsLog.Tables(0).Rows(i).Item("MESSAGE"))
                                    Else
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " MESSAGE: " & dsLog.Tables(0).Rows(i).Item("MESSAGE"))
                                    End If
                                Next
                            Catch ex As Exception
                            End Try

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Set_EstadoRecaudacion")
                            '''Else
                            '''blnExito = False
                        End If
                    ElseIf pEstadoTransacion = ConfigurationSettings.AppSettings("constEstadoPagoFijo").ToString Or pEstadoTransacion = ConfigurationSettings.AppSettings("constEstadoPagoPaginas").ToString Or pEstadoTransacion = ConfigurationSettings.AppSettings("constEstadoPagoFijoDolares").ToString Then
                        '---se realiza esto porque de SAP viene todo en soles
                        Dim sCodigoFactDolares As String = ConfigurationSettings.AppSettings("constCodigoServFactDolares").ToString
                        If (strServicio = sCodigoFactDolares) Then
                            If (pTipoCambioFechaAnulacion <= 0) Then
                                Throw New ArgumentOutOfRangeException("Tipo Cambio", "Tipo de cambio tiene un dato incosistente.")
                            Else
                                strMoneda = "840"
                                dblMontoPagar = Math.Round(dblMontoPagar / pTipoCambioFechaAnulacion, 2)
                            End If
                        End If

                        '----ANULA PAGO ST FIJAS
                        If AnularPagoST_23(strLogSET, strNivelLogSet, strCodigoPuntoDeVenta, strCanal, _
                            strBinAdquiriente, strCodComercio, strCodigoCajero, strTipoIdentificadorDeudor, _
                            strNumeroIdentificadorDeudor, dblMontoPagar, strMoneda, strServicio, _
                            strTipoDocumento, strNumeroDocumento, strNumeroOperacionCobranzaOriginal, strNumeroOperacionAcreedorOriginal, _
                            strTraceOriginal, strFechaHoraTransaccionOriginal, strRespuestaAnulacion, strOrigenRpta, _
                            strCodigoRpta, strDescripcionRpta, strNombreDeudor, strRucDeudor, _
                            strNumeroOperacionCobranza, strNumeroOperacionAcreedor, dblValorTotal, intNumeroDocumentos, _
                            strDocumentos, strFechaHoraTransaccion, strCodigoPlazaRecaudador, strCodigoAgenciaRecaudador, _
                            strCodigoCiudadRecaudador) Then
                            '----
                            sTotalDocAnulados = sTotalDocAnulados + 1
                            'CAMBIADO POR FFS INICIO ...............                           
                            '..........ANULAR EN SICAR
                            Dim dsLog As DataSet = objclsOffline.SetEstadoRecaudacion(strTransaccion, gstrEstadoAnulado, strPosicion, gstrTraceAnulacion)
                            'CAMBIADO POR FFS FIN ..................
                            '----
                            sMensaje = String.Empty
                            For i = 0 To dsLog.Tables(0).Rows.Count - 1
                                sMensaje = sMensaje & dsLog.Tables(0).Rows(i).Item("MESSAGE")
                                If dsLog.Tables(0).Rows(i).Item("TYPE") = "E" Then
                                    Exit For
                                End If
                            Next
                            '---log
                            sbLineasLog.Append(sMarLeft & "OUT Set_EstadoRecaudacion : MESSAGE = " & sMensaje & vbCrLf)
                        End If

                    Else  '///
                        '//Nothing
                    End If
                Next
                '--
                blnExito = ((dsResult.Tables(1).Rows.Count > 0) AndAlso (dsResult.Tables(1).Rows.Count = sTotalDocAnulados))
                '---
                If blnExito Then
                    Dim strTramaPagos As String = String.Empty '//inicializa
                    If blnDescuenta Then
                        For Each drRecibo In dsResult.Tables(2).Rows
                            '---concatena forma de pago para el log de cuadre caja
                            If Len(strTramaPagos) > 0 Then
                                strTramaPagos = strTramaPagos & "|"
                            End If
                            strTramaPagos = strTramaPagos & drRecibo.Item("VIA_PAGO") & ";" & drRecibo.Item("IMPORTE_PAGADO") & ";" & drRecibo.Item("NRO_CHEQUE")
                            '---descuenta el monto de pago del documento
                            If drRecibo.Item("VIA_PAGO") = "ZEFE" Then
                                objCajas.FP_InsertaEfectivo(strOficina, strUsuario, CDbl(drRecibo.Item("IMPORTE_PAGADO")) * (-1)) 'CALLBACK  ORACLE
                            End If
                        Next
                    End If
                    '--
                    clsPagos.GuardarLogCuadreCaja(intCodAplicacion, strCanalOF, strOficina, strUsuario, strNumeroIdentificadorDeudor, strTramaPagos, dsResult.Tables(1).Rows(0).Item(6), dsResult.Tables(1).Rows(0).Item(3), _
                            strClienteRec, 0, 0, 0, "", "", "A")
                End If
                '---
                If CInt(strRespuestaAnulacion) = 0 Then
                    '''strResultado = strRespuestaAnulacion & "@" & strNombreDeudor & ";" & strRucDeudor & ";" & strNumeroOperacionCobranza & ";" & dblValorTotal & ";" & intNumeroDocumentos & ";" & strFechaHoraTransaccion & ";" & strTraceOriginal & "@" & strDocumentos
                    'CAMBIADO POR FFS INICIO
                    'strResultado = strRespuestaAnulacion & "@" & ";" & strRucDeudor & ";" & strNumeroOperacionCobranza & ";" & dblValorTotal & ";" & intNumeroDocumentos & ";" & strFechaHoraTransaccion & ";" & strTraceOriginal & "@" & strDocumentos
                    strResultado = strRespuestaAnulacion & "@" & strDescripcionRpta
                    'CAMBIADO POR FFS FIN
                Else
                    strResultado = strRespuestaAnulacion & "@" & strDescripcionRpta
                End If

            End If
        Else '//
            Throw New ArgumentOutOfRangeException("Nro. Transacción", "El Documento a anular ya no existe o no fue posible obtener los datos del documento a anular.")
        End If


        AnularPago = strResultado

        sbLineasLog.Append(sMarLeft & "     strResultado: " & strResultado & vbCrLf)

        sbLineasLog.Append(sMarLeft & "--------------------------------------------------------" & vbCrLf)
        sbLineasLog.Append(sMarLeft & "Fin AnularPago  " & vbCrLf)
        sbLineasLog.Append(sMarLeft & "--------------------------------------------------------" & vbCrLf)
        'Catch ex As Exception
        '    AnularPago = "8@" & ex.Message & "@"
        'End Try

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

End Class