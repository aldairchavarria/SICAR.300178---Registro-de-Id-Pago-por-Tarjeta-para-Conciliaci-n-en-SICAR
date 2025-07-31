#Region "Imports"

Imports System.Text
Imports System.Configuration
Imports COM_SIC_Adm_Cajas
Imports SwichTransaccional.PortalWS
Imports System.IO

#End Region

Public Class clsContabilizar

#Region "Variables"
    Public objFileLog As New SICAR_Log
    'Recaudacion
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogContabilizarRec")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogContabilizarRec")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    'Remesa
    Public nameFileRem As String = ConfigurationSettings.AppSettings("constNameLogContabilizarRem")
    Public pathFileRem As String = ConfigurationSettings.AppSettings("constRutaLogContabilizarRem")
    Public strArchivoRem As String = objFileLog.Log_CrearNombreArchivo(nameFileRem)

    Dim objAdmCajas As clsAdmCajas
    Dim oSwichTransaccionalContab As New portalService
    Dim oVentasSapWs As New PS_VentasSAPWS.ptype_ventasSOAP11BindingQSService
#End Region

    Public Sub New()
        oSwichTransaccionalContab.Url = CStr(ConfigurationSettings.AppSettings("PortalWSURL"))
        oSwichTransaccionalContab.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim iTimeOut As Int32 = 0
        Dim strTimeOut As String = ConfigurationSettings.AppSettings("constSICARPortalST_Timeout")
        If Not strTimeOut Is Nothing Then
            If strTimeOut <> "" Then
                iTimeOut = Convert.ToInt32(strTimeOut)
            End If
        End If
        oSwichTransaccionalContab.Timeout = iTimeOut

        oVentasSapWs.Url = CStr(ConfigurationSettings.AppSettings("VentasSAP_Url"))
        oVentasSapWs.Credentials = System.Net.CredentialCache.DefaultCredentials
        oVentasSapWs.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("VentasSAP_Timeout"))

    End Sub

#Region "Metodos"

    Public Sub ProcesarContabilizacionRec(ByVal IdentifyLog As String, _
                                       ByVal nroTransacIni As String, _
                                       ByVal nroTransacFin As String, _
                                       ByVal fechaIni As String, _
                                       ByVal fechaFin As String, _
                                       ByVal strIpApp As String, _
                                       ByVal mesContable As String, _
                                       ByVal oficinas As String, _
                                       ByRef cntEnviada As Integer, _
                                       ByRef cntExistoso As Integer, _
                                       ByRef cntFallido As Integer)

        Dim strIdentifyLog As String = IdentifyLog

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ProcesarContabilizacionRec - clsContabilizar")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

        Dim dsRecauda As New DataSet
        Dim dtDetalle As DataTable
        Dim strIpAplicacion As String = strIpApp
        Dim strNombreAplicacion As String = ConfigurationSettings.AppSettings("NombreAplicacionContabilizacionWS")
        Dim strVersion As String = ConfigurationSettings.AppSettings("VersionContabilizacionWS")
        Dim strUsuarioApp As String = ConfigurationSettings.AppSettings("UsuarioAppContabilizacionWS")

        Dim generarAsiento As New GenerarAsientoRequest
        Dim auditGenerarAsiento As New AuditType
        Dim generarAsientoRespuesta As New GenerarAsientoResponse

        'Proy-33111 - inicio'
        Dim auditVentasSAP As New PS_VentasSAPWS.AuditType
        Dim RequestVentasSAP As New PS_VentasSAPWS.GenerarAsientoControlCredito
        Dim ResponseVentasSAP As New PS_VentasSAPWS.GenerarAsientoControlCreditoResponse
        'Proy-33111 - fin'

        Try
            Dim listaDetalle(2) As Bapiret2Bean
            Dim dtContabTipo2 As DataTable
            objAdmCajas = New clsAdmCajas

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio : GetTransaccionesRecaudacion")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  oficinas: " & oficinas)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  nroTransacIni: " & nroTransacIni)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  nroTransacFin: " & nroTransacFin)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  fechaIni: " & fechaIni)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  fechaFin: " & fechaFin)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  version: " & strVersion)

            dsRecauda = objAdmCajas.GetTransaccionesRecaudacion(oficinas, nroTransacIni, nroTransacFin, fechaIni, fechaFin)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin : GetTransaccionesRecaudacion")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

            If Not dsRecauda Is Nothing Then
                dtContabTipo2 = dsRecauda.Tables(1)
                For Each itemType1 As DataRow In dsRecauda.Tables(0).Rows
                    Dim strIdTransaccion As String = DateTime.Now.Ticks.ToString().Substring(7, 11)
                    Dim strNroDoc As String = String.Empty
                    Dim strMsjGral As String = String.Empty
                    Dim strRpta As String = String.Empty
                    Dim strCodigoRpta As String = String.Empty
                    Dim dsDatosXML As New DataSet
                    Dim dtDatosXML As DataTable
                    Dim dtDatosAuditXML As New DataTable("auditGenerarAsientoRequest")
                    Dim dtDatosListaXML As New DataTable("AsientoBeanRequest")
                    Dim dtDatosVersionXML As New DataTable("Version")
                    Dim bRegistroST As Boolean = False
                    Dim strcanal As String = String.Empty
                    dsDatosXML.DataSetName = "DatosContaRec"

                    Dim itemType2 As DataRow
                    Dim s As Integer = 0
                    For Each row As DataRow In dtContabTipo2.Rows
                        If s = cntEnviada Then
                            itemType2 = row
                            Exit For
                        End If
                        s += 1
                    Next
                    cntEnviada += 1

                    'INI :: ENVIO DE contabilizacion  A SAP
                    'Proy-3311 - Ver canal de Pago 01-CAC / 02-DAC'
                    strcanal = itemType1("CANAL")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- CANAL ::: " & strcanal)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- ID TRANSACCION ::: " & strIdTransaccion)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
                    If strcanal = "01" Then 'Para CACs

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- INICIO ::: REQUEST GENERAR ASIENTO CONTABLE CAC----")
                    With auditGenerarAsiento
                        .idTransaccion = strIdTransaccion
                        .ipApplicacion = strIpAplicacion
                        .nombreAplicacion = strNombreAplicacion
                        .usuarioAplicacion = strUsuarioApp

                        With dtDatosAuditXML
                            .Columns.Add("idTransaccion")
                            .Columns.Add("ipApplicacion")
                            .Columns.Add("nombreAplicacion")
                            .Columns.Add("usuarioAplicacion")
                        End With

                        Dim drFilaAudit As DataRow
                        drFilaAudit = dtDatosAuditXML.NewRow
                        drFilaAudit("idTransaccion") = strIdTransaccion
                        drFilaAudit("ipApplicacion") = strIpAplicacion
                        drFilaAudit("nombreAplicacion") = strNombreAplicacion
                        drFilaAudit("usuarioAplicacion") = strUsuarioApp

                        dtDatosAuditXML.Rows.Add(drFilaAudit)
                        dsDatosXML.Tables.Add(dtDatosAuditXML)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- INICIO ::: AUDITORIA REGISTRAR ASIENTO CONTABLE PORTAL SAP----")

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP idTransaccion : " & strIdTransaccion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP ipApplicacion : " & strIpAplicacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP nombreAplicacion : " & strNombreAplicacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP usuarioAplicacion : " & strUsuarioApp)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- FIN ::: AUDITORIA REGISTRAR ASIENTO CONTABLE PORTAL SAP----")
                    End With

                    Dim listaAsientoBean(1) As AsientoBean
                    listaAsientoBean(0) = New AsientoBean

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- INICIO ::: LISTA DE ASIENTOS----")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- INICIO ::: ITEM 1----")

                    With listaAsientoBean(0)
                        .tipoRegistro = IIf(IsDBNull(itemType1("TIPO_REGISTRO")), String.Empty, itemType1("TIPO_REGISTRO"))
                        .fechaDocumento = IIf(IsDBNull(itemType1("FECHA_DOC")), String.Empty, CDate(itemType1("FECHA_DOC")).ToString("yyyy-MM-dd"))
                        .claseDocumento = IIf(IsDBNull(itemType1("CLASEDOCUMENTO")), String.Empty, itemType1("CLASEDOCUMENTO"))
                        .sociedad = IIf(IsDBNull(itemType1("SOCIEDAD")), String.Empty, itemType1("SOCIEDAD"))
                        .fechaContabilizacion = IIf(IsDBNull(itemType1("FEC_CONTAB")), String.Empty, CDate(itemType1("FEC_CONTAB")).ToString("yyyy-MM-dd"))
                        .fechaConversion = IIf(IsDBNull(itemType1("FEC_CONVER")), String.Empty, CDate(itemType1("FEC_CONVER")).ToString("yyyy-MM-dd"))
                        .claveMoneda = IIf(IsDBNull(itemType1("MONEDA")), String.Empty, itemType1("MONEDA"))
                        .textoCabecera = IIf(IsDBNull(itemType1("VIA_PAGO")), String.Empty, itemType1("VIA_PAGO"))
                        .claveContabilizacion = IIf(IsDBNull(itemType1("CLAVE_CONTAB")), String.Empty, itemType1("CLAVE_CONTAB"))
                        .cuenta = IIf(IsDBNull(itemType1("CUENTA")), String.Empty, itemType1("CUENTA"))
                        .importe = IIf(IsDBNull(itemType1("TOTAL")), String.Empty, itemType1("TOTAL"))
                        .numeroAsignacion = IIf(IsDBNull(itemType1("NUMERO_ASIGNA")), String.Empty, itemType1("NUMERO_ASIGNA"))
                        .tipoCambio = IIf(IsDBNull(itemType1("TIPO_CAMBIO")), String.Empty, itemType1("TIPO_CAMBIO"))
                        .mesContable = mesContable
                        .viaPago = IIf(IsDBNull(itemType1("VIA_PAGO")), String.Empty, itemType1("VIA_PAGO"))
                        .division = IIf(IsDBNull(itemType1("DIVISION")), String.Empty, itemType1("DIVISION"))
                        .centroBeneficio = IIf(IsDBNull(itemType1("CENTRO_BENEF")), String.Empty, itemType1("CENTRO_BENEF"))
                        .importeLocal = 0
                        .importeCtaMayor = 0

                        With dtDatosListaXML
                            .Columns.Add("tipoRegistro")
                            .Columns.Add("fechaDocumento")
                            .Columns.Add("claseDocumento")
                            .Columns.Add("sociedad")
                            .Columns.Add("fechaContabilizacion")
                            .Columns.Add("fechaConversion")
                            .Columns.Add("claveMoneda")
                            .Columns.Add("numeroDocRef")
                            .Columns.Add("textoCabecera")
                            .Columns.Add("claveContabilizacion")
                            .Columns.Add("cuenta")
                            .Columns.Add("indicador")
                            .Columns.Add("importe")
                            .Columns.Add("claveCondicion")
                            .Columns.Add("fechaBase")
                            .Columns.Add("claveBloqueo")
                            .Columns.Add("numeroAsignacion")
                            .Columns.Add("textoPosicion")
                            .Columns.Add("centroCoste")
                            .Columns.Add("centroBeneficio")
                            .Columns.Add("numeroOrden")
                            .Columns.Add("tipoCambio")
                            .Columns.Add("calcImpuesto")
                            .Columns.Add("indIVA")
                            .Columns.Add("indRetencion")
                            .Columns.Add("posicionPresupuestaria")
                            .Columns.Add("mesContable")
                            .Columns.Add("viaPago")
                            .Columns.Add("centroGestor")
                            .Columns.Add("division")
                            .Columns.Add("importeLocal")
                            .Columns.Add("importeCtaMayor")
                            .Columns.Add("elementoPlanEstructura")
                        End With

                        Dim drFilaItem As DataRow
                        drFilaItem = dtDatosListaXML.NewRow
                        drFilaItem("tipoRegistro") = .tipoRegistro
                        drFilaItem("fechaDocumento") = .fechaDocumento
                        drFilaItem("claseDocumento") = .claseDocumento
                        drFilaItem("sociedad") = .sociedad
                        drFilaItem("fechaContabilizacion") = .fechaContabilizacion
                        drFilaItem("fechaConversion") = .fechaConversion
                        drFilaItem("claveMoneda") = .claveMoneda
                        drFilaItem("textoCabecera") = .textoCabecera
                        drFilaItem("claveContabilizacion") = .claveContabilizacion
                        drFilaItem("cuenta") = .cuenta
                        drFilaItem("importe") = .importe
                        drFilaItem("numeroAsignacion") = .numeroAsignacion
                        drFilaItem("tipoCambio") = .tipoCambio
                        drFilaItem("mesContable") = .mesContable
                        drFilaItem("viaPago") = .viaPago
                        drFilaItem("division") = .division
                        drFilaItem("importeLocal") = .importeLocal
                        drFilaItem("importeCtaMayor") = .importeCtaMayor
                        dtDatosListaXML.Rows.Add(drFilaItem)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Tipo Registro : " & .tipoRegistro)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha Documento : " & .fechaDocumento)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Clase Documento : " & .claseDocumento)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Sociedad : " & .sociedad)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha Contabilizacion : " & .fechaContabilizacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha Conversion : " & .fechaConversion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Moneda : " & .claveMoneda)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Texto Cabecera : " & .textoCabecera)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Clave Contabilización : " & .claveContabilizacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Cuenta : " & .cuenta)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Importe : " & .importe)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Numero Asignación : " & .numeroAsignacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Tipo Cambio : " & .tipoCambio)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Mes Contable : " & .mesContable)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP División : " & .division)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Importe Local : " & .importeLocal)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Importe Cta Mayor : " & .importeCtaMayor)
                            

                    End With

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- FIN ::: ITEM 1----")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- INICIO ::: ITEM 2----")

                    listaAsientoBean(1) = New AsientoBean
                    With listaAsientoBean(1)
                        .tipoRegistro = IIf(IsDBNull(itemType2("TIPO_REGISTRO")), String.Empty, itemType2("TIPO_REGISTRO"))
                        .claveContabilizacion = IIf(IsDBNull(itemType2("CLAVE_CONTAB")), String.Empty, itemType2("CLAVE_CONTAB"))
                        .cuenta = IIf(IsDBNull(itemType2("CUENTA")), String.Empty, itemType2("CUENTA"))
                        .indicador = IIf(IsDBNull(itemType2("INDICADOR")), String.Empty, itemType2("INDICADOR"))
                        .importe = IIf(IsDBNull(itemType2("TOTAL")), String.Empty, itemType2("TOTAL"))
                        .numeroAsignacion = IIf(IsDBNull(itemType2("NUMERO_ASIGNA")), String.Empty, itemType2("NUMERO_ASIGNA"))
                        .textoPosicion = IIf(IsDBNull(itemType2("TEXTO_POSICION")), String.Empty, itemType2("TEXTO_POSICION"))
                        .tipoCambio = IIf(IsDBNull(itemType2("TIPO_CAMBIO")), String.Empty, itemType2("TIPO_CAMBIO"))
                        .mesContable = mesContable
                        .division = IIf(IsDBNull(itemType2("DIVISION")), String.Empty, itemType2("DIVISION"))
                        .importeLocal = 0
                        .importeCtaMayor = 0

                        Dim drFilaItem As DataRow
                        drFilaItem = dtDatosListaXML.NewRow
                        drFilaItem("tipoRegistro") = .tipoRegistro
                        drFilaItem("claveContabilizacion") = .claveContabilizacion
                        drFilaItem("cuenta") = .cuenta
                        drFilaItem("indicador") = .indicador
                        drFilaItem("importe") = .importe
                        drFilaItem("numeroAsignacion") = .numeroAsignacion
                        drFilaItem("textoPosicion") = .textoPosicion
                        drFilaItem("tipoCambio") = .tipoCambio
                        drFilaItem("mesContable") = .mesContable
                        drFilaItem("division") = .division
                        drFilaItem("importeLocal") = .importeLocal
                        drFilaItem("importeCtaMayor") = .importeCtaMayor
                        dtDatosListaXML.Rows.Add(drFilaItem)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Tipo Registro : " & .tipoRegistro)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Clave Contabilización : " & .claveContabilizacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Cuenta : " & .cuenta)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Indicador : " & .indicador)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Importe : " & .importe)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Numero Asignación : " & .numeroAsignacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Texto Posición : " & .textoPosicion)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Tipo Cambio : " & .tipoCambio)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Mes Contable : " & .mesContable)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP División : " & .division)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Importe Local : " & .importeLocal)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Importe Cta Mayor : " & .importeCtaMayor)

                    End With

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- FIN ::: ITEM 2----")

                    With generarAsiento
                        .auditRequest = auditGenerarAsiento
                        .listaAsiento = listaAsientoBean
                        .version = strVersion

                        With dtDatosVersionXML
                            .Columns.Add("version")
                        End With

                        Dim drFila As DataRow
                        drFila = dtDatosVersionXML.NewRow
                        drFila("version") = .version
                        dtDatosVersionXML.Rows.Add(drFila)

                        dsDatosXML.Tables.Add(dtDatosVersionXML)
                        dsDatosXML.Tables.Add(dtDatosListaXML)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Versión : " & .version)
                    End With

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- FIN ::: REQUEST GENERAR ASIENTO CONTABLE ----")

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio envio de generar asiento contable al ST")

                    generarAsientoRespuesta = oSwichTransaccionalContab.generarAsiento(generarAsiento)

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin envio de generar asiento contable al ST")

                    With generarAsientoRespuesta
                        Dim auditResponse As AuditResponseType = .auditResponse
                        With auditResponse
                            strCodigoRpta = .codigoRespuesta
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT Mensaje respuesta : " & .mensajeRespuesta.ToString())
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT Codigo respuesta : " & .codigoRespuesta.ToString())
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT Id Transaccion : " & .idTransaccion.ToString())
                        End With
                        If Not .claveReferencia Is Nothing Then
                            If .claveReferencia.Length > 1 Then
                                strNroDoc = .claveReferencia.Substring(0, 10)
                            Else
                                strNroDoc = ""
                            End If
                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Numero Documento : " & strNroDoc)
                        Select Case Trim(strCodigoRpta)
                            Case 0
                                If Not .listaBapiret2 Is Nothing Then
                                    Dim detRpta As Bapiret2Bean()
                                    detRpta = .listaBapiret2
                                    If detRpta.Length > 0 Then
                                        dtDatosXML = Bapiret2BeanToTable(detRpta)
                                        If detRpta(0).type <> "E" Then
                                            bRegistroST = True
                                            strMsjGral = "Se registro correctamente. Numero Doc. Contable : " & strNroDoc
                                                strRpta = detRpta(0).message.ToString()
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRpta)
                                        Else
                                            bRegistroST = False
                                            strMsjGral = "Error al contabilizar el documento."
                                            strNroDoc = ""
                                            strRpta = detRpta(0).message.ToString()
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRpta)
                                        End If
                                    End If
                                End If
                            Case Else
                                bRegistroST = False
                                strMsjGral = "Error al contabilizar el documento."
                                strNroDoc = ""
                                strRpta = auditResponse.mensajeRespuesta.ToString()
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRpta)
                        End Select
                    End With
                    'FIN :: ENVIO DE contabilizacion  A SAP
                    ElseIf strcanal = "02" Then 'Para DACs

                        '-------------
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- INICIO ::: REQUEST GENERAR ASIENTO CONTABLE DAC----")
                        With auditVentasSAP
                            .idTransaccion = strIdTransaccion
                            .ipApplicacion = strIpAplicacion
                            .nombreAplicacion = strNombreAplicacion
                            .usuarioAplicacion = strUsuarioApp

                            With dtDatosAuditXML
                                .Columns.Add("idTransaccion")
                                .Columns.Add("ipApplicacion")
                                .Columns.Add("nombreAplicacion")
                                .Columns.Add("usuarioAplicacion")
                            End With

                            Dim drFilaAudit As DataRow
                            drFilaAudit = dtDatosAuditXML.NewRow
                            drFilaAudit("idTransaccion") = strIdTransaccion
                            drFilaAudit("ipApplicacion") = strIpAplicacion
                            drFilaAudit("nombreAplicacion") = strNombreAplicacion
                            drFilaAudit("usuarioAplicacion") = strUsuarioApp

                            dtDatosAuditXML.Rows.Add(drFilaAudit)
                            dsDatosXML.Tables.Add(dtDatosAuditXML)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- INICIO ::: AUDITORIA REGISTRAR ASIENTO CONTABLE VENTAS SAP----")

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP idTransaccion : " & strIdTransaccion)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP ipApplicacion : " & strIpAplicacion)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP nombreAplicacion : " & strNombreAplicacion)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP usuarioAplicacion : " & strUsuarioApp)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- FIN ::: AUDITORIA REGISTRAR ASIENTO CONTABLE VENTAS SAP----")
                        End With

                        Dim listaAsientoVentasSAP(1) As PS_VentasSAPWS.itemAsientoType
                        listaAsientoVentasSAP(0) = New PS_VentasSAPWS.itemAsientoType

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- INICIO ::: LISTA DE ASIENTOS----")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- INICIO ::: ITEM 1----")

                        Dim arrValoresDac() As String = itemType1("VALORES_DAC").Split("|")
                        Dim CondicionPago As String = arrValoresDac(0)
                        Dim ControlCredito As String = arrValoresDac(1)
                        Dim CuentaDac As String = arrValoresDac(2)

                        With listaAsientoVentasSAP(0)
                            .stype = IIf(IsDBNull(itemType1("TIPO_REGISTRO")), String.Empty, itemType1("TIPO_REGISTRO")) 'tipoRegistro
                            .bldat = IIf(IsDBNull(itemType1("FECHA_DOC")), String.Empty, CDate(itemType1("FECHA_DOC")).ToString("yyyy-MM-dd")) 'fechaDocumento
                            .blart = IIf(IsDBNull(itemType1("CLASEDOCUMENTO")), String.Empty, itemType1("CLASEDOCUMENTO")) 'claseDocumento
                            .bukrs = IIf(IsDBNull(itemType1("SOCIEDAD")), String.Empty, itemType1("SOCIEDAD")) 'sociedad
                            .budat = IIf(IsDBNull(itemType1("FEC_CONTAB")), String.Empty, CDate(itemType1("FEC_CONTAB")).ToString("yyyy-MM-dd")) 'fechaContabilizacion
                            .wwert = IIf(IsDBNull(itemType1("FEC_CONVER")), String.Empty, CDate(itemType1("FEC_CONVER")).ToString("yyyy-MM-dd")) 'fechaConversion
                            .waers = IIf(IsDBNull(itemType1("MONEDA")), String.Empty, itemType1("MONEDA")) 'claveMoneda
                            .bktxt = IIf(IsDBNull(itemType1("VIA_PAGO")), String.Empty, itemType1("VIA_PAGO")) 'textoCabecera
                            .newbs = IIf(IsDBNull(itemType1("CLAVE_CONTAB")), String.Empty, itemType1("CLAVE_CONTAB")) 'claveContabilizacion
                            .newko = CuentaDac 'cuenta
                            .wrbtr = IIf(IsDBNull(itemType1("TOTAL")), String.Empty, itemType1("TOTAL")) ' importe
                            .zterm = CondicionPago 'claveCondicion
                            .kkber = ControlCredito 'centro credito
                            .zuonr = IIf(IsDBNull(itemType1("NUMERO_ASIGNA")), String.Empty, itemType1("NUMERO_ASIGNA")) 'numeroAsignacion
                            .sgtxt = IIf(IsDBNull(itemType1("TEXTO_POSICION")), String.Empty, itemType1("TEXTO_POSICION")) 'numeroAsignacion
                            .kursf = IIf(IsDBNull(itemType1("TIPO_CAMBIO")), String.Empty, itemType1("TIPO_CAMBIO")) 'tipoCambio
                            .monat = mesContable
                            .zlsch = IIf(IsDBNull(itemType1("VIA_PAGO")), String.Empty, itemType1("VIA_PAGO")) 'viaPago
                            .gsber = IIf(IsDBNull(itemType1("DIVISION")), String.Empty, itemType1("DIVISION")) 'division
                            .prctr = IIf(IsDBNull(itemType1("CENTRO_BENEF")), String.Empty, itemType1("CENTRO_BENEF")) 'centroBeneficio

                            .dmbtr = 0 'importeLocal
                            .dmbe2 = 0 'importeCtaMayor

                            With dtDatosListaXML
                                .Columns.Add("tipoRegistro")
                                .Columns.Add("fechaDocumento")
                                .Columns.Add("claseDocumento")
                                .Columns.Add("sociedad")
                                .Columns.Add("fechaContabilizacion")
                                .Columns.Add("fechaConversion")
                                .Columns.Add("claveMoneda")
                                .Columns.Add("numeroDocRef")
                                .Columns.Add("textoCabecera")
                                .Columns.Add("claveContabilizacion")
                                .Columns.Add("cuenta")
                                .Columns.Add("indicador")
                                .Columns.Add("importe")
                                .Columns.Add("claveCondicion")
                                .Columns.Add("controlCredito")
                                .Columns.Add("fechaBase")
                                .Columns.Add("claveBloqueo")
                                .Columns.Add("numeroAsignacion")
                                .Columns.Add("textoPosicion")
                                .Columns.Add("centroCoste")
                                .Columns.Add("centroBeneficio")
                                .Columns.Add("numeroOrden")
                                .Columns.Add("tipoCambio")
                                .Columns.Add("calcImpuesto")
                                .Columns.Add("indIVA")
                                .Columns.Add("indRetencion")
                                .Columns.Add("posicionPresupuestaria")
                                .Columns.Add("mesContable")
                                .Columns.Add("viaPago")
                                .Columns.Add("centroGestor")
                                .Columns.Add("division")
                                .Columns.Add("importeLocal")
                                .Columns.Add("importeCtaMayor")
                                .Columns.Add("elementoPlanEstructura")
                            End With

                            Dim drFilaItem As DataRow
                            drFilaItem = dtDatosListaXML.NewRow
                            drFilaItem("tipoRegistro") = .stype
                            drFilaItem("fechaDocumento") = .bldat
                            drFilaItem("claseDocumento") = .blart
                            drFilaItem("sociedad") = .bukrs
                            drFilaItem("fechaContabilizacion") = .budat
                            drFilaItem("fechaConversion") = .wwert
                            drFilaItem("claveMoneda") = .waers
                            drFilaItem("textoCabecera") = .bktxt
                            drFilaItem("claveContabilizacion") = .newbs
                            drFilaItem("cuenta") = .newko
                            drFilaItem("importe") = .wrbtr
                            drFilaItem("numeroAsignacion") = .zuonr
                            drFilaItem("textoPosicion") = .sgtxt
                            drFilaItem("tipoCambio") = .kursf
                            drFilaItem("mesContable") = .monat
                            drFilaItem("viaPago") = .zlsch
                            drFilaItem("division") = .gsber
                            drFilaItem("importeLocal") = .dmbtr
                            drFilaItem("importeCtaMayor") = .dmbe2
                            drFilaItem("claveCondicion") = .zterm
                            drFilaItem("controlCredito") = .newko
                            dtDatosListaXML.Rows.Add(drFilaItem)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Tipo Registro : " & .stype)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha Documento : " & .bldat)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Clase Documento : " & .blart)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Sociedad : " & .bukrs)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha Contabilizacion : " & .budat)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Fecha Conversion : " & .wwert)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Moneda : " & .waers)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Texto Cabecera : " & .bktxt)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Clave Contabilización : " & .newbs)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Cuenta : " & .newko)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Importe : " & .wrbtr)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Numero Asignación : " & .zuonr)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Texto Posicion: " & .sgtxt)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Tipo Cambio : " & .kursf)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Mes Contable : " & .monat)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP via Pago : " & .zlsch)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP División : " & .gsber)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Importe Local : " & .dmbtr)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Importe Cta Mayor : " & .dmbe2)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Clave Condicion : " & .zterm)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Control Credito : " & .kkber)

                        End With

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- FIN ::: ITEM 1----")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- INICIO ::: ITEM 2----")

                        listaAsientoVentasSAP(1) = New PS_VentasSAPWS.itemAsientoType
                        With listaAsientoVentasSAP(1)
                            .stype = IIf(IsDBNull(itemType2("TIPO_REGISTRO")), String.Empty, itemType2("TIPO_REGISTRO"))
                            .newbs = IIf(IsDBNull(itemType2("CLAVE_CONTAB")), String.Empty, itemType2("CLAVE_CONTAB"))
                            .newko = IIf(IsDBNull(itemType2("CUENTA")), String.Empty, itemType2("CUENTA"))
                            .newum = IIf(IsDBNull(itemType2("INDICADOR")), String.Empty, itemType2("INDICADOR"))
                            .wrbtr = IIf(IsDBNull(itemType2("TOTAL")), String.Empty, itemType2("TOTAL"))
                            .zuonr = IIf(IsDBNull(itemType2("NUMERO_ASIGNA")), String.Empty, itemType2("NUMERO_ASIGNA"))
                            .sgtxt = IIf(IsDBNull(itemType2("TEXTO_POSICION")), String.Empty, itemType2("TEXTO_POSICION"))
                            .kursf = IIf(IsDBNull(itemType2("TIPO_CAMBIO")), String.Empty, itemType2("TIPO_CAMBIO"))
                            .monat = mesContable
                            .gsber = IIf(IsDBNull(itemType2("DIVISION")), String.Empty, itemType2("DIVISION"))
                            .dmbtr = 0
                            .dmbe2 = 0

                            Dim drFilaItem As DataRow
                            drFilaItem = dtDatosListaXML.NewRow
                            drFilaItem("tipoRegistro") = .stype
                            drFilaItem("claveContabilizacion") = .newbs
                            drFilaItem("cuenta") = .newko
                            drFilaItem("indicador") = .newum
                            drFilaItem("importe") = .wrbtr
                            drFilaItem("numeroAsignacion") = .zuonr
                            drFilaItem("textoPosicion") = .sgtxt
                            drFilaItem("tipoCambio") = .kursf
                            drFilaItem("mesContable") = .monat
                            drFilaItem("division") = .gsber
                            drFilaItem("importeLocal") = .dmbtr
                            drFilaItem("importeCtaMayor") = .dmbe2
                            dtDatosListaXML.Rows.Add(drFilaItem)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Tipo Registro : " & .stype)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Clave Contabilización : " & .newbs)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Cuenta : " & .newko)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Indicador : " & .newum)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Importe : " & .wrbtr)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Numero Asignación : " & .zuonr)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Texto Posición : " & .sgtxt)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Tipo Cambio : " & .kursf)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Mes Contable : " & .monat)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP División : " & .gsber)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Importe Local : " & .dmbtr)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Importe Cta Mayor : " & .dmbe2)

                        End With

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- FIN ::: ITEM 2----")

                        With RequestVentasSAP
                            .auditRequest = auditVentasSAP
                            .listaAsiento = listaAsientoVentasSAP


                            'With dtDatosVersionXML
                            '    .Columns.Add("version")
                            'End With

                            'Dim drFila As DataRow
                            'drFila = dtDatosVersionXML.NewRow
                            'drFila("version") = .version
                            'dtDatosVersionXML.Rows.Add(drFila)

                            'dsDatosXML.Tables.Add(dtDatosVersionXML)
                            'dsDatosXML.Tables.Add(dtDatosListaXML)
                            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Versión : " & .version)
                        End With

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ---- FIN ::: REQUEST GENERAR ASIENTO CONTABLE VENTAS SAP----")

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio envio de generar asiento contable a VENTAS SAP")

                        ResponseVentasSAP = oVentasSapWs.generarAsientoControlCredito(RequestVentasSAP)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin envio de generar asiento contable a VENTAS SAP")

                        With ResponseVentasSAP
                            Dim auditResponseVentasSap As PS_VentasSAPWS.AuditResponseType = .auditResponse

                            With auditResponseVentasSap
                                strCodigoRpta = .codigoRespuesta
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT Mensaje respuesta : " & .mensajeRespuesta.ToString())
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT Cod respuesta : " & .codigoRespuesta.ToString())
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT Id Transaccion : " & .idTransaccion.ToString())
                            End With

                            Select Case Trim(strCodigoRpta)
                                Case 0
                                    If Not .Bapiret2Bean Is Nothing Then
                                        Dim detRpta As PS_VentasSAPWS.Bapiret2Bean()
                                        detRpta = .Bapiret2Bean
                                        dtDatosXML = ResponseSapToTable(detRpta)
                                        If detRpta(0).type <> "E" Then
                                            bRegistroST = True
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT messageV2 : " & detRpta(0).messageV2.ToString())
                                            If (detRpta(0).messageV2.Length > 10) Then
                                                strNroDoc = detRpta(0).messageV2.Substring(0, 10)
                                            End If
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT strNroDoc : " & strNroDoc.ToString())
                                            strMsjGral = "Se registro correctamente. Numero Doc. Contable : " & strNroDoc
                                            strRpta = detRpta(0).message.ToString()
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRpta)
                                        Else
                                            bRegistroST = False
                                            strMsjGral = "Error al contabilizar el documento."
                                            strNroDoc = ""
                                            strRpta = detRpta(0).message.ToString()
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRpta)
                                        End If

                                    End If
                                Case Else
                                    bRegistroST = False
                                    strMsjGral = "Error al contabilizar el documento."
                                    strNroDoc = ""
                                    strRpta = auditResponseVentasSap.mensajeRespuesta.ToString()
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRpta)
                            End Select
                        End With
                        'FIN :: ENVIO DE contabilizacion  A SAP
                        '-------------


                    End If
                    'INI :: REGISTRO DE LOG
                    Try
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI Registra Log")
                        If Not dtDatosXML Is Nothing Then
                            Dim codigoTransacLog As String = String.Empty
                            Dim nombreTablas As String = "CONTABILIZA_RECAUDACION"
                            If strNroDoc.Equals(String.Empty) Then
                                codigoTransacLog = CDate(itemType1("FECHA_DOC")).ToString("yyyyMMdd") & "|" & _
                                                    itemType1("NUMERO_ASIGNA") & "|" & itemType1("VIA_PAGO") & "|" & _
                                                    itemType1("CLASEDOCUMENTO")
                            Else
                                codigoTransacLog = strNroDoc
                            End If
                            'Proy-33111
                            If dsDatosXML.Tables.CanRemove(dtDatosXML) Then
                                dsDatosXML.Tables.Remove(dtDatosXML)
                            End If
                            'Proy-33111
                            dsDatosXML.Tables.Add(dtDatosXML)
                            Dim tw As TextWriter = New StringWriter
                            dsDatosXML.WriteXml(tw)
                            Dim xml As String = tw.ToString().Replace("\r\n", "")
                            objAdmCajas.RegistrarLog(strMsjGral, codigoTransacLog, strRpta, strUsuarioApp, nombreTablas, xml)
                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN Registra Log")
                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Mensaje Registra Log: " & ex.Message.ToString())
                    Finally
                        If Not dtDatosXML Is Nothing Then 'Proy-33111
                        dtDatosXML.Dispose()
                        dsDatosXML.Dispose()
                        End If 'Proy-33111

                    End Try
                    'FIN :: REGISTRO DE LOG

                    'INI :: REGISTRO EN CASO DE EXITO Y ERROR EN TI_PAGO
                    Dim dvDatos As DataView = dsRecauda.Tables(2).DefaultView
                    dvDatos.RowFilter = "OFICINA_VENTA =  '" & CStr(itemType1("NUMERO_ASIGNA")) & "' AND " & _
                                        "FECHA =  '" & CStr(itemType1("FECHA_DOC")) & "' AND " & _
                                        "VIA_PAGO =  '" & CStr(itemType1("VIA_PAGO")) & "' AND " & _
                                        "TIPOPAGO =  '" & CStr(itemType1("CLASEDOCUMENTO")) & "'"

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INI : Registro en BD Sicar")
                    Dim iTrsPagoID As Int32
                    Dim sViaPago As String = String.Empty
                    Dim strMsjError As String = String.Empty
                    For i As Int32 = 0 To dvDatos.Count - 1
                        Dim itemDet As DataRowView = dvDatos(i)
                        iTrsPagoID = CInt(itemDet("ID_T_TRS_REG_DEUDA"))
                        sViaPago = CStr(itemDet("VIA_PAGO"))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " IN ID_TI_PAGO : " & iTrsPagoID.ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " IN Via Pago : " & sViaPago)
                        strMsjError = ""
                        strMsjError = objAdmCajas.ActualizarTransacPagos(iTrsPagoID, sViaPago, strNroDoc, strRpta)
                        If Not strMsjError.Equals(String.Empty) Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al registrar en BD SICAR: " & strMsjError)
                        Else
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "OUT Respuesta : Registrado Correctamente")
                        End If
                    Next
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " FIN : Registro en BD Sicar")
                    'FIN :: REGISTRO EN CASO DE EXITO Y ERROR EN TI_PAGO

                    If bRegistroST Then
                        cntExistoso += 1
                    Else
                        cntFallido += 1
                    End If
                Next
            End If
        Catch webEx As System.Net.WebException
            objAdmCajas = Nothing
            generarAsiento = Nothing
            generarAsientoRespuesta = Nothing
            auditGenerarAsiento = Nothing
            dsRecauda.Dispose()

            auditVentasSAP = Nothing
            RequestVentasSAP = Nothing
            ResponseVentasSAP = Nothing
            oVentasSapWs.Dispose()

            oSwichTransaccionalContab.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "EXCEPCION: " & webEx.Message)
            Throw webEx
        Catch ex As Exception
            Dim stackTrace As New System.Diagnostics.StackTrace(ex)
            objAdmCajas = Nothing
            generarAsiento = Nothing
            generarAsientoRespuesta = Nothing
            auditGenerarAsiento = Nothing
            dsRecauda.Dispose()

            auditVentasSAP = Nothing
            RequestVentasSAP = Nothing
            ResponseVentasSAP = Nothing
            oVentasSapWs.Dispose()

            oSwichTransaccionalContab.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "EXCEPCION: " & ex.Message)
        Finally
            objAdmCajas = Nothing
            generarAsiento = Nothing
            generarAsientoRespuesta = Nothing
            auditGenerarAsiento = Nothing
            dsRecauda.Dispose()

            auditVentasSAP = Nothing
            RequestVentasSAP = Nothing
            ResponseVentasSAP = Nothing
            oVentasSapWs.Dispose()

            oSwichTransaccionalContab.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ProcesarContabilizacionRec - clsContabilizar")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try
    End Sub


    Public Sub ProcesarContabilizacionRem(ByVal IdentifyLog As String, _
                                   ByVal oficinaVta As String, _
                                   ByVal fechaDoc As String, _
                                   ByVal fechaCont As String, _
                                   ByVal strIpApp As String, _
                                   ByVal mesContable As String, _
                                   ByVal usuario As String, _
                                   ByRef cntEnviada As Integer, _
                                   ByRef cntExistoso As Integer, _
                                   ByRef cntFallido As Integer)

        Dim strIdentifyLog As String = IdentifyLog

        objFileLog.Log_WriteLog(pathFileRem, strArchivoRem, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFileRem, strArchivoRem, strIdentifyLog & "- " & "Inicio ProcesarContabilizacionRem - clsContabilizar")
        objFileLog.Log_WriteLog(pathFileRem, strArchivoRem, strIdentifyLog & "- " & "-------------------------------------------------")

        Dim dsRemesa As New DataSet
        Dim dtDetalle As DataTable
        Dim strIpAplicacion As String = strIpApp
        Dim strNombreAplicacion As String = ConfigurationSettings.AppSettings("NombreAplicacionContabilizacionRemWS")
        Dim strVersion As String = ConfigurationSettings.AppSettings("VersionContabilizacionRemWS")
        Dim strUsuarioApp As String = ConfigurationSettings.AppSettings("UsuarioAppContabilizacionRemWS")

        Dim generarAsiento As New GenerarAsientoRequest
        Dim auditGenerarAsiento As New AuditType
        Dim generarAsientoRespuesta As New GenerarAsientoResponse

        Try
            Dim listaDetalle(2) As Bapiret2Bean
            Dim dtContabTipo2 As DataTable
            objAdmCajas = New clsAdmCajas

            objFileLog.Log_WriteLog(pathFileRem, strArchivoRem, strIdentifyLog & "- " & "   Inicio : GetRemesaContabilizar")
            objFileLog.Log_WriteLog(pathFileRem, strArchivoRem, strIdentifyLog & "- " & "   INP  Oficina: " & oficinaVta)
            objFileLog.Log_WriteLog(pathFileRem, strArchivoRem, strIdentifyLog & "- " & "   INP  Fecha documento: " & fechaDoc)
            objFileLog.Log_WriteLog(pathFileRem, strArchivoRem, strIdentifyLog & "- " & "   INP  Fecha Contabilizacion: " & fechaCont)
            objFileLog.Log_WriteLog(pathFileRem, strArchivoRem, strIdentifyLog & "- " & "   INP  Version: " & strVersion)

            dsRemesa = objAdmCajas.GetRemesaContabilizar(oficinaVta, fechaDoc, fechaCont)

            objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   Fin : GetRemesaContabilizar")

            If Not dsRemesa Is Nothing Then
                dtContabTipo2 = dsRemesa.Tables(1)
                For Each itemType1 As DataRow In dsRemesa.Tables(0).Rows
                    Dim strIdTransaccion As String = DateTime.Now.Ticks.ToString().Substring(7, 11)
                    Dim strNroDoc As String = String.Empty
                    Dim strMsjGral As String = String.Empty
                    Dim strRpta As String = String.Empty
                    Dim strCodigoRpta As String = String.Empty
                    Dim dsDatosXML As New DataSet
                    Dim dtDatosXML As DataTable
                    Dim dtDatosAuditXML As New DataTable("auditGenerarAsientoRequest")
                    Dim dtDatosListaXML As New DataTable("AsientoBeanRequest")
                    Dim dtDatosVersionXML As New DataTable("Version")
                    Dim bRegistroST As Boolean = False
                    dsDatosXML.DataSetName = "DatosContaRem"

                    Dim itemType2 As DataRow
                    Dim s As Integer = 0
                    For Each row As DataRow In dtContabTipo2.Rows
                        If s = cntEnviada Then
                            itemType2 = row
                            Exit For
                        End If
                        s += 1
                    Next
                    cntEnviada += 1

                    'INI :: ENVIO DE contabilizacion  A SAP
                    objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   ---- INICIO ::: REQUEST GENERAR ASIENTO CONTABLE ----")
                    With auditGenerarAsiento
                        .idTransaccion = strIdTransaccion
                        .ipApplicacion = strIpAplicacion
                        .nombreAplicacion = strNombreAplicacion
                        .usuarioAplicacion = strUsuarioApp

                        With dtDatosAuditXML
                            .Columns.Add("idTransaccion")
                            .Columns.Add("ipApplicacion")
                            .Columns.Add("nombreAplicacion")
                            .Columns.Add("usuarioAplicacion")
                        End With

                        Dim drFilaAudit As DataRow
                        drFilaAudit = dtDatosAuditXML.NewRow
                        drFilaAudit("idTransaccion") = strIdTransaccion
                        drFilaAudit("ipApplicacion") = strIpAplicacion
                        drFilaAudit("nombreAplicacion") = strNombreAplicacion
                        drFilaAudit("usuarioAplicacion") = strUsuarioApp

                        dtDatosAuditXML.Rows.Add(drFilaAudit)
                        dsDatosXML.Tables.Add(dtDatosAuditXML)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   ---- INICIO ::: AUDITORIA REGISTRAR ASIENTO CONTABLE ----")

                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP idTransaccion : " & strIdTransaccion)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP ipApplicacion : " & strIpAplicacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP nombreAplicacion : " & strNombreAplicacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP usuarioAplicacion : " & strUsuarioApp)

                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   ---- FIN ::: AUDITORIA REGISTRAR ASIENTO CONTABLE ----")
                    End With

                    Dim listaAsientoBean(1) As AsientoBean
                    listaAsientoBean(0) = New AsientoBean
                    With listaAsientoBean(0)
                        .tipoRegistro = IIf(IsDBNull(itemType1("TIPO_REGISTRO")), String.Empty, itemType1("TIPO_REGISTRO"))
                        .fechaDocumento = IIf(IsDBNull(itemType1("FECHA_DOC")), String.Empty, itemType1("FECHA_DOC"))
                        .claseDocumento = IIf(IsDBNull(itemType1("CLASE_DOCUMENTO")), String.Empty, itemType1("CLASE_DOCUMENTO"))
                        .sociedad = IIf(IsDBNull(itemType1("SOCIEDAD")), String.Empty, itemType1("SOCIEDAD"))
                        .fechaContabilizacion = IIf(IsDBNull(itemType1("FECHA_CONTAB")), String.Empty, itemType1("FECHA_CONTAB"))
                        .fechaConversion = IIf(IsDBNull(itemType1("FECHA_CONVERSION")), String.Empty, itemType1("FECHA_CONVERSION"))
                        .claveMoneda = IIf(IsDBNull(itemType1("CLAVE_MONEDA")), String.Empty, itemType1("CLAVE_MONEDA"))
                        .numeroDocRef = IIf(IsDBNull(itemType1("NUMERODOCREF")), String.Empty, itemType1("NUMERODOCREF"))
                        .textoCabecera = IIf(IsDBNull(itemType1("TEXTO_CABECERA")), String.Empty, itemType1("TEXTO_CABECERA"))
                        .claveContabilizacion = IIf(IsDBNull(itemType1("CLAVE_CONTAB")), String.Empty, itemType1("CLAVE_CONTAB"))
                        .cuenta = IIf(IsDBNull(itemType1("CUENTA")), String.Empty, itemType1("CUENTA"))
                        .importe = IIf(IsDBNull(itemType1("IMPORTE")), String.Empty, itemType1("IMPORTE"))
                        .numeroAsignacion = IIf(IsDBNull(itemType1("NUMERO_ASIG")), String.Empty, itemType1("NUMERO_ASIG"))
                        .tipoCambio = IIf(IsDBNull(itemType1("TIPO_CAMBIO")), String.Empty, itemType1("TIPO_CAMBIO"))
                        .textoPosicion = IIf(IsDBNull(itemType1("TEXTO_POSICION")), String.Empty, CStr(itemType1("TEXTO_POSICION")).Trim())
                        .calcImpuesto = IIf(IsDBNull(itemType1("CALC_IMPUESTO")), String.Empty, itemType1("CALC_IMPUESTO"))
                        .mesContable = mesContable
                        .division = IIf(IsDBNull(itemType1("DIVISION")), String.Empty, itemType1("DIVISION"))
                        .centroBeneficio = IIf(IsDBNull(itemType1("CENTRO_BENEFICIO")), String.Empty, itemType1("CENTRO_BENEFICIO"))
                        .importeLocal = 0
                        .importeCtaMayor = 0

                        With dtDatosListaXML
                            .Columns.Add("tipoRegistro")
                            .Columns.Add("fechaDocumento")
                            .Columns.Add("claseDocumento")
                            .Columns.Add("sociedad")
                            .Columns.Add("fechaContabilizacion")
                            .Columns.Add("fechaConversion")
                            .Columns.Add("claveMoneda")
                            .Columns.Add("numeroDocRef")
                            .Columns.Add("textoCabecera")
                            .Columns.Add("claveContabilizacion")
                            .Columns.Add("cuenta")
                            .Columns.Add("indicador")
                            .Columns.Add("importe")
                            .Columns.Add("claveCondicion")
                            .Columns.Add("fechaBase")
                            .Columns.Add("claveBloqueo")
                            .Columns.Add("numeroAsignacion")
                            .Columns.Add("textoPosicion")
                            .Columns.Add("centroCoste")
                            .Columns.Add("centroBeneficio")
                            .Columns.Add("numeroOrden")
                            .Columns.Add("tipoCambio")
                            .Columns.Add("calcImpuesto")
                            .Columns.Add("indIVA")
                            .Columns.Add("indRetencion")
                            .Columns.Add("posicionPresupuestaria")
                            .Columns.Add("mesContable")
                            .Columns.Add("viaPago")
                            .Columns.Add("centroGestor")
                            .Columns.Add("division")
                            .Columns.Add("importeLocal")
                            .Columns.Add("importeCtaMayor")
                            .Columns.Add("elementoPlanEstructura")
                        End With

                        Dim drFilaItem As DataRow
                        drFilaItem = dtDatosListaXML.NewRow
                        drFilaItem("tipoRegistro") = .tipoRegistro
                        drFilaItem("fechaDocumento") = .fechaDocumento
                        drFilaItem("claseDocumento") = .claseDocumento
                        drFilaItem("sociedad") = .sociedad
                        drFilaItem("fechaContabilizacion") = .fechaContabilizacion
                        drFilaItem("fechaConversion") = .fechaConversion
                        drFilaItem("claveMoneda") = .claveMoneda
                        drFilaItem("numeroDocRef") = .numeroDocRef
                        drFilaItem("textoCabecera") = .textoCabecera
                        drFilaItem("claveContabilizacion") = .claveContabilizacion
                        drFilaItem("cuenta") = .cuenta
                        drFilaItem("importe") = .importe
                        drFilaItem("numeroAsignacion") = .numeroAsignacion
                        drFilaItem("tipoCambio") = .tipoCambio
                        drFilaItem("textoPosicion") = .textoPosicion
                        drFilaItem("mesContable") = .mesContable
                        drFilaItem("division") = .division
                        drFilaItem("calcImpuesto") = .calcImpuesto
                        drFilaItem("centroBeneficio") = .centroBeneficio
                        drFilaItem("importeLocal") = .importeLocal
                        drFilaItem("importeCtaMayor") = .importeCtaMayor
                        dtDatosListaXML.Rows.Add(drFilaItem)

                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Tipo Registro : " & .tipoRegistro)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Fecha Documento : " & .fechaDocumento)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Clase Documento : " & .claseDocumento)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Sociedad : " & .sociedad)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Fecha Contabilizacion : " & .fechaContabilizacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Fecha Conversion : " & .fechaConversion)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Moneda : " & .claveMoneda)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP numeroDocRef : " & .numeroDocRef)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Texto Cabecera : " & .textoCabecera)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Clave Contabilización : " & .claveContabilizacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Cuenta : " & .cuenta)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Importe : " & .importe)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Numero Asignación : " & .numeroAsignacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Tipo Cambio : " & .tipoCambio)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Texto posicion : " & .textoPosicion)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Calc Impuesto : " & .calcImpuesto)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Mes Contable : " & .mesContable)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP División : " & .division)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Centro Beneficio : " & .centroBeneficio)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Importe Local : " & .importeLocal)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Importe Cta Mayor : " & .importeCtaMayor)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   ")
                    End With

                    listaAsientoBean(1) = New AsientoBean
                    With listaAsientoBean(1)
                        .tipoRegistro = IIf(IsDBNull(itemType2("TIPO_REGISTRO")), String.Empty, itemType2("TIPO_REGISTRO"))
                        .claveContabilizacion = IIf(IsDBNull(itemType2("CLAVE_CONTAB")), String.Empty, itemType2("CLAVE_CONTAB"))
                        .cuenta = IIf(IsDBNull(itemType2("CUENTA")), String.Empty, itemType2("CUENTA"))
                        .importe = IIf(IsDBNull(itemType2("IMPORTE")), String.Empty, itemType2("IMPORTE"))
                        .numeroAsignacion = IIf(IsDBNull(itemType2("NUMERO_ASIG")), String.Empty, itemType2("NUMERO_ASIG"))
                        .textoPosicion = IIf(IsDBNull(itemType2("TEXTO_POSICION")), String.Empty, CStr(itemType2("TEXTO_POSICION")).Trim())
                        .tipoCambio = IIf(IsDBNull(itemType2("TIPO_CAMBIO")), String.Empty, itemType2("TIPO_CAMBIO"))
                        .mesContable = mesContable
                        .division = IIf(IsDBNull(itemType2("DIVISION")), String.Empty, itemType2("DIVISION"))
                        .importeLocal = 0
                        .importeCtaMayor = 0

                        Dim drFilaItem As DataRow
                        drFilaItem = dtDatosListaXML.NewRow
                        drFilaItem("tipoRegistro") = .tipoRegistro
                        drFilaItem("claveContabilizacion") = .claveContabilizacion
                        drFilaItem("cuenta") = .cuenta
                        drFilaItem("importe") = .importe
                        drFilaItem("numeroAsignacion") = .numeroAsignacion
                        drFilaItem("textoPosicion") = .textoPosicion
                        drFilaItem("tipoCambio") = .tipoCambio
                        drFilaItem("mesContable") = .mesContable
                        drFilaItem("division") = .division
                        drFilaItem("importeLocal") = .importeLocal
                        drFilaItem("importeCtaMayor") = .importeCtaMayor
                        dtDatosListaXML.Rows.Add(drFilaItem)

                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Tipo Registro : " & .tipoRegistro)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Clave Contabilización : " & .claveContabilizacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Cuenta : " & .cuenta)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Importe : " & .importe)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Numero Asignación : " & .numeroAsignacion)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Texto Posición : " & .textoPosicion)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Tipo Cambio : " & .tipoCambio)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Mes Contable : " & .mesContable)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP División : " & .division)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Importe Local : " & .importeLocal)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Importe Cta Mayor : " & .importeCtaMayor)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   ")
                    End With

                    With generarAsiento
                        .auditRequest = auditGenerarAsiento
                        .listaAsiento = listaAsientoBean
                        .version = strVersion

                        With dtDatosVersionXML
                            .Columns.Add("version")
                        End With

                        Dim drFila As DataRow
                        drFila = dtDatosVersionXML.NewRow
                        drFila("version") = .version
                        dtDatosVersionXML.Rows.Add(drFila)

                        dsDatosXML.Tables.Add(dtDatosVersionXML)
                        dsDatosXML.Tables.Add(dtDatosListaXML)
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INP Versión : " & .version)
                    End With

                    objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   ---- FIN ::: REQUEST GENERAR ASIENTO CONTABLE ----")

                    objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "Inicio envio de generar asiento contable al ST")
                    generarAsientoRespuesta = oSwichTransaccionalContab.generarAsiento(generarAsiento)
                    objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "Fin envio de generar asiento contable al ST")

                    With generarAsientoRespuesta
                        Dim auditResponse As AuditResponseType = .auditResponse
                        With auditResponse
                            strCodigoRpta = .codigoRespuesta
                            objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   OUT Mensaje respuesta : " & .mensajeRespuesta.ToString())
                        End With
                        If Not .claveReferencia Is Nothing Then
                            If .claveReferencia.Length > 1 Then
                                strNroDoc = .claveReferencia.Substring(0, 10)
                            Else
                                strNroDoc = ""
                            End If
                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   OUT  Numero Documento : " & strNroDoc)
                        Select Case Trim(strCodigoRpta)
                            Case 0
                                If Not .listaBapiret2 Is Nothing Then
                                    Dim detRpta As Bapiret2Bean()
                                    detRpta = .listaBapiret2
                                    If detRpta.Length > 0 Then
                                        dtDatosXML = Bapiret2BeanToTable(detRpta)
                                        If detRpta(0).type <> "E" Then
                                            bRegistroST = True
                                            strMsjGral = "Se registro correctamente. Numero Doc. Contable : " & strNroDoc
                                            strRpta = ""
                                            objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRpta)
                                        Else
                                            bRegistroST = False
                                            strMsjGral = "Error al contabilizar el documento."
                                            strNroDoc = ""
                                            strRpta = detRpta(0).message.ToString()
                                            objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRpta)
                                        End If
                                    End If
                                End If
                            Case Else
                                bRegistroST = False
                                strMsjGral = "Error al contabilizar el documento."
                                strNroDoc = ""
                                strRpta = auditResponse.mensajeRespuesta.ToString()
                                objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   OUT  Respuesta: " & strRpta)
                        End Select
                    End With
                    'FIN :: ENVIO DE contabilizacion  A SAP

                    'INI :: REGISTRO DE LOG
                    Try
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   INI Registra Log")
                        If Not dtDatosXML Is Nothing Then
                            Dim codigoTransacLog As String = String.Empty
                            Dim nombreTablas As String = "CONTABILIZA_REMESA"
                            If strNroDoc.Equals(String.Empty) Then
                                If IsDBNull(itemType1("FECHA_DOC")) Then
                                    codigoTransacLog = itemType1("COD_OFICINA") & "|" & _
                                                        itemType1("NUMERO_ASIG") & "|" & itemType1("TEXTO_CABECERA") & "|" & _
                                                        itemType1("CLASE_DOCUMENTO")
                                Else
                                    codigoTransacLog = CDate(itemType1("FECHA_DOC")).ToString("yyyyMMdd") & "|" & _
                                                        itemType1("NUMERO_ASIG") & "|" & itemType1("TEXTO_CABECERA") & "|" & _
                                                        itemType1("CLASE_DOCUMENTO")
                                End If
                            Else
                                codigoTransacLog = strNroDoc
                            End If

                            dsDatosXML.Tables.Add(dtDatosXML)
                            Dim tw As TextWriter = New StringWriter
                            dsDatosXML.WriteXml(tw)
                            Dim xml As String = tw.ToString().Replace("\r\n", "")
                            objAdmCajas.RegistrarLog(strMsjGral, codigoTransacLog, strRpta, strUsuarioApp, nombreTablas, xml)
                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   FIN Registra Log")
                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "   Mensaje Registra Log: " & ex.Message.ToString())
                    Finally
                        dtDatosXML.Dispose()
                        dsDatosXML.Dispose()
                    End Try
                    'FIN :: REGISTRO DE LOG

                    'INI :: REGISTRO EN CASO DE EXITO Y ERROR EN TI_PAGO
                    Dim strOficinaVta As String = itemType1("COD_OFICINA")
                    Dim strTipoRemesa As String = itemType1("COD_TIPO_REMESA")
                    Dim strFechaDoc As String = IIf(IsDBNull(itemType1("FECHA")), String.Empty, itemType1("FECHA"))
                    Dim strMsjError As String = String.Empty
                    Dim strUsuario As String = usuario.PadLeft(10, "0")

                    objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & " INI : Registro en BD Sicar")
                    objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & " IN Oficina Venta : " & strOficinaVta)
                    objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & " IN Fecha : " & strFechaDoc)
                    objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & " IN Tipo Remesa : " & strTipoRemesa)
                    objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & " IN Usuario : " & strUsuario)
                    strMsjError = ""
                    strMsjError = objAdmCajas.ActualizarRemesaContab(strOficinaVta, strFechaDoc, strTipoRemesa, strNroDoc, strRpta, strUsuario)
                    If Not strMsjError.Equals(String.Empty) Then
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "Error al registrar en BD SICAR: " & strMsjError)
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "OUT Respuesta : Registrado Correctamente")
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & " FIN : Registro en BD Sicar")
                    'FIN :: REGISTRO EN CASO DE EXITO Y ERROR EN TI_PAGO

                    If bRegistroST Then
                        cntExistoso += 1
                    Else
                        cntFallido += 1
                    End If
                Next
            End If
        Catch webEx As System.Net.WebException
            objAdmCajas = Nothing
            generarAsiento = Nothing
            generarAsientoRespuesta = Nothing
            auditGenerarAsiento = Nothing
            oSwichTransaccionalContab.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "EXCEPCION: " & webEx.Message)
            Throw webEx
        Catch ex As Exception
            Dim stackTrace As New System.Diagnostics.StackTrace(ex)
            objAdmCajas = Nothing
            generarAsiento = Nothing
            generarAsientoRespuesta = Nothing
            auditGenerarAsiento = Nothing
            oSwichTransaccionalContab.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "EXCEPCION: " & ex.Message)
            Throw ex
        Finally
            objAdmCajas = Nothing
            generarAsiento = Nothing
            generarAsientoRespuesta = Nothing
            auditGenerarAsiento = Nothing
            oSwichTransaccionalContab.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "Fin ProcesarContabilizacionRem - clsContabilizar")
            objFileLog.Log_WriteLog(pathFile, strArchivoRem, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try
    End Sub

    Private Function Bapiret2BeanToTable(ByVal respuestaLog As Bapiret2Bean()) As DataTable
        Dim dtDatos As New DataTable("Bapiret2BeanResponse")
        Try
            With dtDatos
                .Columns.Add("type", GetType(System.String))
                .Columns.Add("id", GetType(System.String))
                .Columns.Add("number", GetType(System.String))
                .Columns.Add("message", GetType(System.String))
                .Columns.Add("logNo", GetType(System.String))
                .Columns.Add("logMsgNo", GetType(System.String))
                .Columns.Add("messageV1", GetType(System.String))
                .Columns.Add("messageV2", GetType(System.String))
                .Columns.Add("messageV3", GetType(System.String))
                .Columns.Add("messageV4", GetType(System.String))
                .Columns.Add("parameter", GetType(System.String))
                .Columns.Add("row", GetType(System.String))
                .Columns.Add("field", GetType(System.String))
                .Columns.Add("system", GetType(System.String))
            End With

            Dim drFila As DataRow
            For x As Integer = 0 To respuestaLog.Length - 1
                drFila = dtDatos.NewRow
                drFila("type") = respuestaLog(x).type
                drFila("id") = respuestaLog(x).id
                drFila("number") = respuestaLog(x).number
                drFila("message") = respuestaLog(x).message
                drFila("logNo") = respuestaLog(x).logNo
                drFila("logMsgNo") = respuestaLog(x).logMsgNo
                drFila("messageV1") = respuestaLog(x).messageV1
                drFila("messageV2") = respuestaLog(x).messageV2
                drFila("messageV4") = respuestaLog(x).messageV3
                drFila("messageV2") = respuestaLog(x).messageV4
                drFila("parameter") = respuestaLog(x).parameter
                drFila("row") = respuestaLog(x).row
                drFila("field") = respuestaLog(x).field
                drFila("system") = respuestaLog(x).system

                dtDatos.Rows.Add(drFila)
            Next

        Catch ex As Exception
            Dim stackTrace As New System.Diagnostics.StackTrace(ex)
            Throw ex
        End Try
        Return dtDatos
    End Function

    Private Function ResponseSapToTable(ByVal respuestaLog As PS_VentasSAPWS.Bapiret2Bean()) As DataTable
        Dim dtDatos As New DataTable("Bapiret2BeanResponse")
        Try
            With dtDatos
                .Columns.Add("type", GetType(System.String))
                .Columns.Add("id", GetType(System.String))
                .Columns.Add("number", GetType(System.String))
                .Columns.Add("message", GetType(System.String))
                .Columns.Add("logNo", GetType(System.String))
                .Columns.Add("logMsgNo", GetType(System.String))
                .Columns.Add("messageV1", GetType(System.String))
                .Columns.Add("messageV2", GetType(System.String))
                .Columns.Add("messageV3", GetType(System.String))
                .Columns.Add("messageV4", GetType(System.String))
                .Columns.Add("parameter", GetType(System.String))
                .Columns.Add("row", GetType(System.String))
                .Columns.Add("field", GetType(System.String))
                .Columns.Add("system", GetType(System.String))
            End With

            Dim drFila As DataRow
            For x As Integer = 0 To respuestaLog.Length - 1
                drFila = dtDatos.NewRow
                drFila("type") = respuestaLog(x).type
                drFila("id") = respuestaLog(x).id
                drFila("number") = respuestaLog(x).number
                drFila("message") = respuestaLog(x).message
                drFila("logNo") = respuestaLog(x).logNo
                drFila("logMsgNo") = respuestaLog(x).logMsgNo
                drFila("messageV1") = respuestaLog(x).messageV1
                drFila("messageV2") = respuestaLog(x).messageV2
                drFila("messageV4") = respuestaLog(x).messageV3
                drFila("messageV2") = respuestaLog(x).messageV4
                drFila("parameter") = respuestaLog(x).parameter
                drFila("row") = respuestaLog(x).row
                drFila("field") = respuestaLog(x).field
                drFila("system") = respuestaLog(x).system

                dtDatos.Rows.Add(drFila)
            Next

        Catch ex As Exception
            Dim stackTrace As New System.Diagnostics.StackTrace(ex)
            Throw ex
        End Try
        Return dtDatos
    End Function

#End Region

End Class
