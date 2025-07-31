Imports COM_SIC_INActChip
Imports System.IO
Imports COM_SIC_Activaciones
Imports COM_SIC_Servicios.Funciones
Imports COM_SIC_Servicios.clsActivaciones
Imports System.Text
Imports System.Net
Imports System.Globalization
Imports COM_SIC_Cajas               '*** agregado para el flujo de caja ***'
Imports COM_SIC_FacturaElectronica
Imports System.Configuration
Imports COM_SIC_Entidades

Public Class RentaAdelantada

#Region "Variables Globales"

    Dim dsFormaPago As DataSet
    'MODIFICADO EMANUEL BARBARAN EVERIS PERU
    Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
    Dim objTrsMsSap As New COM_SIC_Activaciones.clsTrsMsSap
    Dim objCajas As New COM_SIC_Cajas.clsCajas            '*** para el registro de datos flujo de caja ***'
    Dim PEDIN_PEDIDOSAP As String = ""

    Dim arrayListaRefSunat(7) As String                 '** Correlativo generado NC         **'
    Dim arrayListaReferencia(7) As String               '** Numero SAP de la NC             **'
    Dim arrayListaRefPedido(7) As String                '** Numero pedido de la NC          **'

    Dim resCajas As Integer = 0
    Dim P_CODERR As String = ""
    Dim P_MSGERR As String = ""
    Dim strNombreCaja As String = ""
    Dim P_ID_TI_VENTAS_FACT As String = ""
    Dim fechaCajas As String = ""
    Dim dsDatosPedido As DataSet
    Dim objOfflineCaja As COM_SIC_OffLine.clsOffline
    Dim dsCajeroA As DataSet
    Dim dsCajeroB As DataSet
    '*************************************************************************************************'

    Dim dsPedido As DataSet
    Dim objPagos As New SAP_SIC_Pagos.clsPagos
    Dim objVentas As New SAP_SIC_Ventas.clsVentas
    Dim objSapCajas As New SAP_SIC_Cajas.clsCajas
    Dim blnError As Boolean
    Dim objFileLog As New SrvPago_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRentaCac")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim numeroTelefono As String = ""
    Dim numeroReferenciaSunat$ = ""
    Dim montoRecarga As String = String.Empty
    Dim numeroOperacion$
    Dim numeroOperacionST$

    Dim desdeRVFrecuente As Boolean = False
    'PROY-27440 INI
    Dim nameFilePos As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivoPos As String = objFileLog.Log_CrearNombreArchivo(nameFilePos)
    'PROY-27440 FIN
    Dim arrParametrosFormaPagoPerfil As ArrayList   'INICIATIVA-318
    Dim strIdentifyLogGeneral As String = "" ' INICIATIVA-318
    Dim strCodPerfilFormaPago As String = ""  ' INICIATIVA-318


    Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
    '==================================================================='
    ' PARAMETROS DE RETORNO DE GUARDAR PAGO
    Dim K_PAGOV_CORRELATIVO As String = ""
    Dim K_PAGOC_CORRELATIVO As String = ""

    Dim consultaDato As DataSet

    Dim K_NROLOG As String = ""
    Dim K_DESLOG As String = ""
    Dim K_NROLOG_DET As String = ""
    Dim K_DESLOG_DET As String = ""
    Dim K_PAGON_IDPAGO As Int64 = 0
    Dim K_CU_CORRELATIVOFE As String = ""

    Dim objContrato As DataSet          '*** guardar la informaciòn recuperada del contrato - dg ***'
    Dim P_COD_RESP As String = ""       '*** variables del contrato -dg ***'
    Dim P_MSG_RESP As String = ""       '*** variables del contrato -dg ***'

    'FIN DE PARAMETROS DE RETORNO DE GUARDAR PAGO 
    '==================================================================='


    ' PARAMETROS DE RETORNO DE CONSULTA PEDIDO PVU
    Dim P_CODIGO_RESPUESTA As String = ""
    Dim P_MENSAJE_RESPUESTA As String = ""
    Dim C_VENTA As DataTable
    Dim C_VENTA_DET As DataTable
    'FIN PARAMETROS DE RETORNO DE CONSULTA PEDIDO PVU


    Dim strNumAsignado As String
    Dim strNumSunat As String
    Dim i As Integer
    Dim j As Integer
    Dim strDetallePago As String
    Dim dsResult As DataSet
    Dim strNumAsignaSUNAT As String
    Dim strErrorMsg As String
    Dim blnSunat As Boolean
    Dim strNroDoc As String
    Dim strPrepago As String
    Dim strPostPago As String
    Dim intCeros As Integer
    Dim blnFlagPago As Boolean
    Dim strMensaje As String
    Dim strEstado As String
    Dim strCadenaCam As String
    Dim strContrato As String           '*** en el conectado ****'

    Dim PorMigracion As String
    Dim PorRenovacion As String
    Dim PorReposicion As String
    Dim PorAprobador As String
    Dim EstadoActualAcuerdo As String
    Dim strObsEstado As String

    'CARIAS
    'Conectado
    Dim strNroSEC, strNroDocSEC, strTipDocSEC, strNroAprobacion, strMotivoMig, strConSinEq As String
    'FIN CARIAS

    Dim strIND_ACTIV_BSCS As String
    Dim dsAcuerdo As DataSet
    Dim dsEstado As DataSet
    Dim dsDatGenerales As DataSet
    Dim conEquipo As Boolean

    Dim strValSAP As String
    Dim blnDebeSerRevisado As Boolean
    Dim strRespuesta As String

    Dim dblEfectivo As Double
    Dim dblEfecCaja As Double
    Dim dblTolerancia As Double
    Dim dsEfectivo As DataSet
    Dim dblMontoTarjeta As Double
    Dim dblTotalTarjeta As Double

    Dim objActivaciones As New COM_SIC_Activaciones.clsActivacion
    Dim objConfig As New COM_SIC_Configura.clsConfigura
    Dim objClsPagosWS As New COM_SIC_Activaciones.clsPagosWS

    Dim objActiv As New COM_SIC_Activaciones.clsBDSiscajas
    Dim msgj As String = ""


    '****************************** Para Impresion
    'Dim strDocSap As String = "" 'drPagos.Item("VBELN")TODOEB
    Dim strDocSap As String = String.Empty 'dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")


    Dim strDocSunat As String = ""
    Dim sTipoVenta, sTipoOperacion As String

    'Dim strDocSunat As String = drPagos.Item("XBLNR")TODOEB
    ' Dim strDocSunat As String = drPagos.Item("PAGOC_CODSUNAT") '"AC111"

    Dim strNroDG As String = "" 'drPagos.Item("NRO_DEP_GARANTIA")TODOEB
    'Dim strTipDoc As String = drPagos.Item("FKART")TODOEB

    '****************************************************************************
    'VARIABLES: INFORMACIÒN DEL PEDIDO
    '****************************************************************************
    Dim strTipDoc As String = String.Empty 'dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")

    '****************************************************************************

    Dim intOper As Int32
    '*******************************

    ' FLAG PILOTO
    Dim booDol As Boolean = True

    'Variables de Auditoria
    Dim wParam1 As Long
    Dim wParam2 As String
    Dim wParam3 As String
    Dim wParam4 As Long
    Dim wParam5 As Integer
    Dim wParam6 As String
    Dim wParam7 As Long
    Dim wParam8 As Long
    Dim wParam9 As Long
    Dim wParam10 As String
    Dim wParam11 As Integer
    Dim Detalle(,) As Object
    Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
    Dim dtDatos As New DataTable
    Dim objBus As New COM_SIC_Cajas.clsCajas

    '###############################
    '##### VARIABLES GLOBALES ######

    Dim AppCurrentUser As String = String.Empty 'CurrentUser


    Dim AppQpDocSap As String = String.Empty 'Request.QueryString("pDocSap")
    Dim AppQnumeroTelefono As String = String.Empty 'Request.QueryString("numeroTelefono")
    Dim AppQmontoRecarga As String = String.Empty 'Request.QueryString("montoRecarga")


    Dim AppcboTipDocumento1 As String = String.Empty 'cboTipDocumento1.SelectedValue
    Dim ApptxtMonto1 As String = String.Empty 'txtMonto1.Text
    Dim ApptxtNeto As String = String.Empty 'txtNeto.Text
    Dim ApptxtVuelto As String = String.Empty 'txtVuelto.text
    Dim ApptxtTipoCambio As String = String.Empty 'txtTipoCambio.text
    Dim ApptxtNumSunat As String = String.Empty 'txtNumSunat.text
    Dim ApptxtCompleto As String = String.Empty 'txtCompleto.text
    Dim ApptxtRecibidoPen As String = String.Empty 'txtRecibidoPen.text
    Dim ApptxtRecibidoUsd As String = String.Empty 'txtRecibidoUsd.text
    Dim ApptxtCorrelativo As String = String.Empty 'txtCorrelativo.text
    Dim ApptxtDoc1 As String = String.Empty 'txtDoc1.text
    Dim ApptxtNomCliente As String = String.Empty 'txtNomCliente.text
    Dim ApptxtCuotaIni As String = String.Empty 'txtCuotaIni.text
    Dim ApptxtSaldo As String = String.Empty 'txtSaldo.text

    Dim flagPagoExito As Boolean = False

    Dim ApperrorGeneral As String = String.Empty
    Dim AppMensajeExito As String = String.Empty


    Dim ApphidNumFilas As String = "1" 'hidNumFilas.Value
    Dim AppHidIntAutPos As String = "0" 'HidIntAutPos.Value
    Dim ApphidDocSap As String = String.Empty 'hidDocSap.Value
    Dim ApphidNumeroTelefono As String = String.Empty 'hidNumeroTelefono.Value
    Dim ApphidFlagCargaDoc As String = String.Empty 'hidFlagCargaDoc.Value
    Dim ApphidFlagVentaCuota As String = String.Empty 'hidFlagVentaCuota.Value
    Dim ApphidMontoCuota As String = String.Empty 'hidMontoCuota.Value
    Dim ApphidFlagRenovacionRMP As String = String.Empty ' hidFlagRenovacionRMP.Value


    Dim AppCANAL As String = String.Empty 'Session("CANAL")
    Dim AppALMACEN As String = String.Empty 'Session("ALMACEN")
    Dim AppUSUARIO As String = String.Empty 'Session("USUARIO")
    Dim AppstrUsuario As String = String.Empty 'Session("strUsuario")
    Dim AppcodPerfil As String = String.Empty 'Session("codPerfil")
    Dim AppIpLocal As String = String.Empty 'Session("IpLocal")
    Dim ApprecargaVirtual As Boolean = False 'Session("recargaVirtual")
    Dim AppCarga As String = String.Empty  'Session("Carga")
    Dim AppVias_Pago As DataSet = Nothing 'Session("Vias_Pago")
    Dim AppCodImprTicket As String = String.Empty 'Session("CodImprTicket")
    Dim AppPedidoAlta As Int64 = 0
    Dim AppTipoDocVendedor As String = String.Empty
    Dim AppNumDocVendedor As String = String.Empty
    Dim AppUUID As String = String.Empty
    Dim AppNumeroReferencia As String = String.Empty
    Dim AppCodMedioPago As String = String.Empty
    Dim AppFlagBiometria As String = String.Empty
    Dim AppMontoPagar As String = String.Empty
    Dim AppFlagTiendaVirtual As String = String.Empty
    Dim AppTipoOperacion As String = String.Empty
    Dim AppActivosPostpagoConvergente As String = String.Empty
    Dim AppPaperless As String = String.Empty
    Dim AppActualizarMSSAP As String = String.Empty
    Dim AppActualizarContrato As String = String.Empty
    Dim AppEnvioSAP As String = String.Empty

    Dim strIdentifyLog As String = String.Empty
    '###############################

#End Region

#Region "Load"
    Private Function Load(ByVal strDocumentoSap As String, _
                     ByVal strNumeroDocumento As String, _
                     ByVal strMontoRecarga As String, _
                     ByVal strCodigoPago As String, _
                     ByVal oEmpleados As BEDatosEmpleado, _
                     ByVal strIpLocal As String, _
                     ByVal strTipoDocVendedor As String, _
                     ByVal strNumDocVendedor As String, _
                     ByVal strNumeroReferencia As String, _
                     ByVal strTipoOperacionServ As String, _
                     ByVal strMontoTotal As String, _
                     ByVal strFlagTiendaVirtual As String, _
                     ByVal strFlagBiometria As String, _
                     ByVal idPedido As String, _
                     ByVal UUID As String) As Boolean

        Dim rpta As Boolean = False

        Try

            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio Page_Load")


            AppQpDocSap = strDocumentoSap
            AppQnumeroTelefono = strNumeroDocumento
            AppQmontoRecarga = strMontoRecarga
            AppcboTipDocumento1 = strCodigoPago
            ApphidDocSap = strDocumentoSap

            AppCANAL = oEmpleados.CANAL
            AppALMACEN = oEmpleados.ALMACEN
            AppUSUARIO = oEmpleados.CODUSUARIO
            AppstrUsuario = oEmpleados.CURRENT_USER
            AppIpLocal = strIpLocal
            AppCurrentUser = oEmpleados.CURRENT_USER
            AppcodPerfil = oEmpleados.PERFIL

            AppPedidoAlta = Funciones.CheckInt64(idPedido)
            AppTipoDocVendedor = strTipoDocVendedor
            AppNumDocVendedor = strNumDocVendedor
            AppUUID = UUID
            AppNumeroReferencia = strNumeroReferencia

            AppMontoPagar = strMontoTotal
            AppFlagBiometria = strFlagBiometria
            AppCodMedioPago = strCodigoPago
            AppTipoOperacion = strTipoOperacionServ
            AppFlagTiendaVirtual = strFlagTiendaVirtual


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppQpDocSap", Funciones.CheckStr(AppQpDocSap)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppQnumeroTelefono", Funciones.CheckStr(AppQnumeroTelefono)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppQmontoRecarga", Funciones.CheckStr(AppQmontoRecarga)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppcboTipDocumento1", Funciones.CheckStr(AppcboTipDocumento1)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "ApphidDocSap", Funciones.CheckStr(ApphidDocSap)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppCANAL", Funciones.CheckStr(AppCANAL)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppALMACEN", Funciones.CheckStr(AppALMACEN)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppUSUARIO", Funciones.CheckStr(AppUSUARIO)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppstrUsuario", Funciones.CheckStr(AppstrUsuario)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppIpLocal", Funciones.CheckStr(AppIpLocal)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppCurrentUser", Funciones.CheckStr(AppCurrentUser)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppcodPerfil", Funciones.CheckStr(AppcodPerfil)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppPedidoAlta", Funciones.CheckStr(AppPedidoAlta)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppTipoDocVendedor", Funciones.CheckStr(AppTipoDocVendedor)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppNumDocVendedor", Funciones.CheckStr(AppNumDocVendedor)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppUUID", Funciones.CheckStr(AppUUID)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppNumeroReferencia", Funciones.CheckStr(AppNumeroReferencia)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppMontoPagar", Funciones.CheckStr(AppMontoPagar)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppFlagBiometria", Funciones.CheckStr(AppFlagBiometria)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppCodMedioPago", Funciones.CheckStr(AppCodMedioPago)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppTipoOperacion", Funciones.CheckStr(AppTipoOperacion)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "AppFlagTiendaVirtual", Funciones.CheckStr(AppFlagTiendaVirtual)))



            '****** CONSULTA PRINCIPAL PARA EL PREOCESO *****'
            '***************************************************************************************************'
            '***************************************************************************************************'
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta los datos del pedido=> " & Funciones.CheckStr(drPagos.Item("PEDIN_NROPEDIDO")))
            dsPedido = objConsultaMsSap.ConsultaPedido(strDocumentoSap, "", "") 'TODOEB
            ' objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta pedido.")
            '***************************************************************************************************'
            '***************************************************************************************************'
            '***************************************************************************************************'

            strDocSap = dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")
            strTipDoc = dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")

            'dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")

            AppHidIntAutPos = "0"


            Dim strCorrelativo As String = String.Empty
            Dim strCompleto As String = String.Empty

            Dim dsReturn As DataSet
            'Dim dsPedido As DataSet
            Dim i As Integer
            '--IDENTIFICADOR DEL LOG :-----------------------------------------------------------------------------------------------------------------------------------------------------------------
            '-------------------------------------------------------------------------------------------------------------------------------------------------------------------

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio asignaciòn de variables por POST.")
            If Not AppQpDocSap Is Nothing Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Var numeroOperacion : " & AppQpDocSap)
                numeroOperacion = AppQpDocSap
            End If

            If Not AppQnumeroTelefono Is Nothing Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Var numeroTelefono : " & AppQnumeroTelefono)
                numeroTelefono = AppQnumeroTelefono
            End If

            If Not AppQmontoRecarga Is Nothing Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Var montoRecarga : " & AppQmontoRecarga)
                montoRecarga = AppQmontoRecarga
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin asignaciòn de variables por POST.")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Asigna Fecha a la variable Session(FechaPago): " & String.Format("{0:dd/MM/yyyy}", DateTime.Now).ToString)
            'Session("FechaPago") = String.Format("{0:dd/MM/yyyy}", DateTime.Now)



            ''JTN INICIO
            Dim TIPO_VENTA, NRO_CUOTAS, NUMERO_CONTRATO, ARTICULO, TIPO_DOC_CLIENTE As String
            Dim NRO_APROBACION$ = ""
            Dim CLASE_VENTA$ = "01"

            '***********************************************************************************'
            '**ESTA PUESTO EN DURO:
            NRO_CUOTAS = "00"
            TIPO_VENTA = "02"
            NUMERO_CONTRATO = ""
            ARTICULO = "SERECVILIM"
            TIPO_DOC_CLIENTE = "01"


            ApphidDocSap = numeroOperacion
            ApphidNumeroTelefono = numeroTelefono
            ''JTN FIN

            'AVILLAR

            Dim objOffline As New COM_SIC_OffLine.clsOffline
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Verifica si el pedido es una recarga virtal.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Var recargaVirtual: " & Funciones.CheckStr(ApprecargaVirtual))
            Dim isRecargaVirtual As Boolean = ApprecargaVirtual


            '*** FORMAS DE PAGO *******************************************************************************************************************************************
            Dim PuntoVentaSinergia As String = ""

            Try
                PuntoVentaSinergia = Funciones.CheckStr(objConsultaMsSap.ConsultaPuntoVenta(AppALMACEN).Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA"))
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al consulta los Puntos de Venta para Sinergia")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error: " & PuntoVentaSinergia)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & ex.Message.ToString)
                PuntoVentaSinergia = ""
            End Try

            If PuntoVentaSinergia = "" Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PuntoVentaSinergia: " & PuntoVentaSinergia)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MEnsaje: No existe Codigo de Punto de Venta Nuevo.")
                'Response.Write("<script>alert('No existe Codigo de Punto de Venta.')</script>")
                ApperrorGeneral = "No existe Codigo de Punto de Venta."
                Exit Function
            End If


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio consulta las formas de Pago")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Método: Obtener_ConsultaViasPago(USRSICAR.PCK_SICAR_OFF_SAP.PROC_DATOS_VIAS_PAGO)")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parámetros: ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param1: " & PuntoVentaSinergia)
            dsFormaPago = objOffline.Obtener_ConsultaViasPago(AppALMACEN)

            AppVias_Pago = Nothing
            AppVias_Pago = dsFormaPago
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta las formas de Pago")
            '**************************************************************************************************************************************************************

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio asignaciòn de variables: ")
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nombre del Cliente: " & Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIV_NOMBRECLIENTE")))
            'ApptxtNomCliente = dsPedido.Tables(0).Rows(0).Item("PEDIV_NOMBRECLIENTE")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Valor Neto: " & Funciones.CheckStr(Format(dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), "###0.00")))
            ApptxtNeto = Format(dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), "###0.00")
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Valor Cuota Inicial: " & Funciones.CheckStr(Format(dsPedido.Tables(0).Rows(0).Item("PAGON_INICIAL"), "###0.00")))
            'ApptxtCuotaIni = Format(dsPedido.Tables(0).Rows(0).Item("PAGON_INICIAL"), "###0.00")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin asignaciòn de variables: ")

            Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia asignaciòn del saldo - pedido")
                'If CDbl(dsPedido.Tables(0).Rows(0).Item("PEDIN_PAGO")) <> 0 Then
                '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Var INPAN_TOTALDOCUMENTO: " & Funciones.CheckStr(Format(dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), "###0.00")))
                '    ApptxtSaldo = Format(dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), "###0.00")
                'Else
                '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Var PEDIN_SALDO: " & Funciones.CheckStr(Format(dsPedido.Tables(0).Rows(0).Item("PEDIN_SALDO"), "###0.00")))
                '    ApptxtSaldo = Format(dsPedido.Tables(0).Rows(0).Item("PEDIN_SALDO"), "###0.00")
                'End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin asignaciòn del saldo - pedido")
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al tratar de asignar el saldo - pedido")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & ex.Message.ToString)
            End Try


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia validaciòn si el pedido seleccionado es una NC")
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "isNotaCredigo : " & Funciones.CheckStr(Session("NC")))
            'Dim isNotaCredigo As Boolean = Session("NC")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Termina validaciòn si el pedido seleccionado es una NC")

            'INICIATIVA-318 INI
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicializa los controles")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Método: inicializaControles")
            'Me.inicializaControles()
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin controles")
            'INICIATIVA-318 FIN

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia asignaciòn el importe total")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INPAN_TOTALDOCUMENTO : " & Funciones.CheckStr(Format(dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), "###0.00")))
            ApptxtMonto1 = Format(dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), "###0.00")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin asignaciòn el importe total")

            AppCarga = 0
            ApphidFlagCargaDoc = "0"

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Asigna(PEDIN_NRODOCUMENTO): " & Funciones.CheckStr(AppQpDocSap))
            Dim PEDIN_NRODOCUMENTO As Int64 = AppQpDocSap

            'Prepago cuotas 23/10/2006

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Evalua si el pedido a procesar no es una RV.")
            If Not isRecargaVirtual Then
                'dsPedido = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drPagos.Item("PEDIV_PEDIDOSAP"), "") ''TODO: CALLBACK SAP
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "El pedido No es una RV")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicial consulta del Pedido(objConsultaMsSap.ConsultaPedido -  PKG_MSSAP.SSAPSS_PEDIDO)")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Paràmetros: ")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param1: " & Funciones.CheckStr(PEDIN_NRODOCUMENTO))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param2: " & "")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param3: " & "")
                dsPedido = objConsultaMsSap.ConsultaPedido(PEDIN_NRODOCUMENTO, "", "")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta del Pedido.")

                Try
                    If Not dsPedido Is Nothing Then
                        NRO_CUOTAS = 0
                        'TIPO_VENTA = dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") TODOE
                        TIPO_VENTA = dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA")
                        'NUMERO_CONTRATO = CheckStr(dsPedido.Tables(0).Rows(0).Item("NUMERO_CONTRATO")) TODOE
                        TIPO_DOC_CLIENTE = dsPedido.Tables(0).Rows(0)("CLIEC_TIPODOCCLIENTE")
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "No se encontraron registros para el pedido: " & Funciones.CheckStr(PEDIN_NRODOCUMENTO))
                    End If
                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al asignar los valores del pedido.")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Err. " & ex.Message.ToString)
                End Try
            End If

            'si es venta prepago y ademas tiene cuotas
            'If TIPO_VENTA = ConfigurationSettings.AppSettings("strTVPrepago") And CDbl(NRO_CUOTAS) > 0 Then
            '    ApptxtMonto1 = Math.Round(CDbl(ApptxtMonto1) * CDbl(ConfigurationSettings.AppSettings("gConstPorcPrePago")) / 100, 2)
            'ElseIf TIPO_VENTA = ConfigurationSettings.AppSettings("strTVPostpago") And CheckInt64(NRO_CUOTAS) > 0 Then
            '    Try
            '        Dim nroContrato As String = NUMERO_CONTRATO     '** dg: verificar en el datos del pedido **'
            '        Dim nroSEC As Int64
            '        Dim objCajas As New COM_SIC_Cajas.clsCajas
            '        Dim dblCuotaInicial As Double
            '        Dim dsAcuerdo As New DataSet

            '        'dsAcuerdo = objPagos.Get_ConsultaAcuerdoPCS(nroContrato) ''TODO: CALLBACK SAP PARA OBTENER EL NUMERO DE APROBACION
            '        'nroSEC = CheckInt64(NRO_APROBACION)

            '        '***** VERIFICA SI ES UNA VENTA EN CUOTAS ******'
            '        '***** Verificar la NroSEC *********************'
            '        Dim flagVentaCuota As String = objCajas.Consulta_Venta_Cuota(nroSEC, dblCuotaInicial)
            '        If flagVentaCuota = "1" Then
            '            ApphidFlagVentaCuota = flagVentaCuota
            '            ApphidMontoCuota = dblCuotaInicial
            '            ApptxtCuotaIni = Format(dblCuotaInicial, "###0.00")
            '            ApptxtMonto1 = Format(dblCuotaInicial, "###0.00")
            '        End If

            '    Catch ex As Exception
            '        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR Consulta_Venta_Cuota:" & ex.Message.ToString())
            '        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR Consulta_Venta_Cuota:" & ex.StackTrace.ToString())
            '        ApphidFlagVentaCuota = "0"
            '    End Try
            'End If

            ApphidFlagRenovacionRMP = "N"

            'If Not Session("msgCaducidadRMP6") Is Nothing And Session("msgCaducidadRMP6") <> "" Then
            '    Response.Write("<script>alert('" & Session("msgCaducidadRMP6") & "')</script>")
            '    Session.Remove("msgCaducidadRMP6")
            'End If

            ApptxtRecibidoPen = ApptxtMonto1
            ApptxtRecibidoUsd = "0.00"
            ApptxtVuelto = "0.00"
            ''TODO: CAMBIADO POR JYMMY TORRES
            ApptxtTipoCambio = objOffline.Obtener_TipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000")).ToString("N3") 'aotane 05.08.2013
            ''CAMBIADO HASTA AQUI

            ''TODO: CARGA LAS TARJETAS DE CREDITO
            'LeeDatosValidar()


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Get_NumeroSUNAT(ZPVU_RFC_TRS_GET_NRO_SUNAT)")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp ALMACEN: " & AppALMACEN)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp ClaseFactura(PEDIC_CLASEFACTURA): " & Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"))) 'TODOEB

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp CodImprTicket: " & AppCodImprTicket)

            Dim codUsuario As String = Convert.ToString(AppUSUARIO).PadLeft(10, CChar("0"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out strCompleto(Referencia): " & strCompleto)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out strCorrelativo(Ultimo numero): " & strCorrelativo)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Get_NumeroSUNAT")

            ApptxtCorrelativo = strCorrelativo
            ApptxtCompleto = strCompleto


            '*** Si es NC o TG, no se va a mostrar el detalle de la formas de pago:
            Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Se identifica el tipo de documento que se esta procesando.")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Para el caso de NC/TG, se oculta el detalle de formas de pago.")
                If dsPedido Is Nothing Then
                    dsPedido = objConsultaMsSap.ConsultaPedido(Funciones.CheckInt64(numeroOperacion), "", "")
                End If

                '******************************************************************************************************************************************************************************************************************************************************************************************************************************************'
                If ((Funciones.CheckDbl(dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")) = 0 And dsPedido.Tables(0).Rows(0).Item("PEDIC_ESQUEMACALCULO") = ConfigurationSettings.AppSettings("ESQUEMACALCULO_TG") And dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA") <> ConfigurationSettings.AppSettings("strTipDoc")) Or _
                    (Funciones.CheckDbl(dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")) >= 0 And dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA") = ConfigurationSettings.AppSettings("strTipDoc"))) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Oculta las formas de pago.")
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Muestra las formas de pago.")
                End If
                '******************************************************************************************************************************************************************************************************************************************************************************************************************************************'

                'PROY-27440 INI
                'If Not IsPostBack Then
                '    Me.validar_pedido_pos()
                'Else
                '    load_values_pos()
                'End If

                'PROY-27440 FIN

            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Error al tratar de ocultar las formas de pago")
            End Try

            rpta = True
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Page_Load")

        Catch ex As Exception

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR LOAD" & Funciones.CheckStr(ex.Message))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR LOAD=> " & Funciones.CheckStr(ex.StackTrace))

        End Try

        Return rpta

    End Function

#End Region
    'INICIATIVA-318 INI
    Private Function ConsultaParametrosFormaPagoPerfil(ByVal strIdenLog As String)

        Try

            Dim objpvuDB As New COM_SIC_Activaciones.clsConsultaPvu
            Dim oParamteros As New COM_SIC_Activaciones.BEParametros
            Dim strCodGrupo As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("key_ParanGrupoFormaPagoPerfil"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdenLog & "- " & "--Inicio Proceso Consulta Parametros--")
            If strCodGrupo <> "" Then
                arrParametrosFormaPagoPerfil = objpvuDB.ConsultaParametros(strCodGrupo)
            End If

            Dim codAplicacion As String = ConfigurationSettings.AppSettings("codAplicacion")
            Dim objAuditoriaWS As New AuditoriaWS.EbsAuditoriaService
            Dim oAccesoRequest As New AuditoriaWS.AccesoRequest
            Dim oAccesoResponse As New AuditoriaWS.AccesoResponse

            objAuditoriaWS.Url = ConfigurationSettings.AppSettings("consRutaWSSeguridad").ToString()
            objAuditoriaWS.Credentials = System.Net.CredentialCache.DefaultCredentials
            objAuditoriaWS.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("ConstTimeOutEmpleado").ToString())

            oAccesoRequest.usuario = AppstrUsuario
            oAccesoRequest.aplicacion = codAplicacion

            oAccesoResponse = objAuditoriaWS.leerDatosUsuario(oAccesoRequest)

            If oAccesoResponse.resultado.estado = "1" Then

                If oAccesoResponse.auditoria.AuditoriaItem.item.Length > 0 Then
                    Dim item As New COM_SIC_Seguridad.EntidadConsulSeguridad
                    For i As Integer = 0 To oAccesoResponse.auditoria.AuditoriaItem.item.Length - 1
                        strCodPerfilFormaPago = strCodPerfilFormaPago & oAccesoResponse.auditoria.AuditoriaItem.item(i).perfil & ","
                    Next
                    strCodPerfilFormaPago = strCodPerfilFormaPago.Substring(0, strCodPerfilFormaPago.Length - 1)
                Else
                    strCodPerfilFormaPago = AppcodPerfil
                End If

            End If

        Catch ex As Exception

        End Try
    End Function


    'Private Sub load_data_param_pos()
    '    Dim strPedidoLog As String = "Pedido: [" & Funciones.CheckStr(AppQpDocSap) & "] "

    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "Inicio : ")

    '    Dim strIpClient As String = Funciones.CheckStr(AppIpLocal)

    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : Validacion Integracion INI")
    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidPtoVenta : " & AppALMACEN)
    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIpClient : " & strIpClient)


    '    Dim strCodRptaFlag As String = ""
    '    Dim strMsgRptaFlag As String = ""

    '    Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS

    '    'INI CONSULTA INTEGRACION AUTOMATICO POS

    '    Dim strFlagIntAut As String = ""

    '    strCodRptaFlag = "" : strMsgRptaFlag = ""
    '    objConsultaPos.Obtener_Integracion_Auto(Funciones.CheckStr(AppALMACEN), strIpClient, String.Empty, strFlagIntAut, strCodRptaFlag, strMsgRptaFlag)

    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strFlagIntAut : " & Funciones.CheckStr(strFlagIntAut))
    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strCodRptaFlag : " & Funciones.CheckStr(strCodRptaFlag))
    '    'INI PROY-140126
    '    Dim MaptPath As String
    '    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
    '    MaptPath = "( Class : " & MaptPath & "; Function: load_data_param_pos)"
    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strMsgRptaFlag : " & Funciones.CheckStr(strMsgRptaFlag) & MaptPath)
    '    'FIN PROY-140126

    '    AppHidIntAutPos = Funciones.CheckStr(strFlagIntAut)

    '    'FIN CONSULTA INTEGRACION AUTOMATICO POS

    '    'INI CONSULTA PAGO AUTOMATICO POS

    '    Dim strFlagPagAut As String = ""

    '    objConsultaPos.Obtener_Pago_Auto(Funciones.CheckStr(AppALMACEN), strIpClient, String.Empty, strFlagPagAut, strCodRptaFlag, strMsgRptaFlag)
    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strFlagPagAut : " & Funciones.CheckStr(strFlagPagAut))
    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strCodRptaFlag : " & Funciones.CheckStr(strCodRptaFlag))
    '    'INI PROY-140126
    '    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
    '    MaptPath = "( Class : " & MaptPath & "; Function: load_data_param_pos)"
    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strMsgRptaFlag : " & Funciones.CheckStr(strMsgRptaFlag) & MaptPath)
    '    'FIN PROY-140126

    '    'Me.HidGrabAuto.Value = Funciones.CheckStr(strFlagPagAut)

    '    'FIN CONSULTA PAGO AUTOMATICO POS
    '    'PROY-31949 - Inicio
    '    'HidNumIntentosPago.Value = ClsKeyPOS.strNumIntentosPago
    '    'HidNumIntentosAnular.Value = ClsKeyPOS.strNumIntentosAnular
    '    'HidMsjErrorNumIntentos.Value = ClsKeyPOS.strMsjErrorNumIntentos
    '    'HidMsjErrorTimeOut.Value = ClsKeyPOS.strMsjErrorTimeOut
    '    'HidMsjNumIntentosPago.Value = ClsKeyPOS.strMsjPagoNumIntentos

    '    Dim objOfflineCaja As New COM_SIC_OffLine.clsOffline
    '    Dim cultureNameX As String = "es-PE"
    '    Dim cultureX As CultureInfo = New CultureInfo(cultureNameX)
    '    Dim dateTimeValueCaja As DateTime = Convert.ToDateTime(DateTime.Now, cultureX)
    '    Dim sFechaCaj As String = dateTimeValueCaja.ToLocalTime.ToShortDateString
    '    dateTimeValueCaja = Convert.ToDateTime(DateTime.Now, cultureX)
    '    sFechaCaj = dateTimeValueCaja.ToString("dd/MM/yyyy")
    '    dsCajeroA = objOfflineCaja.GetDatosAsignacionCajero(AppALMACEN, sFechaCaj, Funciones.CheckStr(Session("USUARIO")).PadLeft(10, "0"))

    '    ' Validar cierre de caja
    '    If dsCajeroA.Tables(0).Rows.Count > 0 Then
    '        For cont As Int32 = 0 To dsCajeroA.Tables(0).Rows.Count - 1
    '            If dsCajeroA.Tables(0).Rows(cont).Item("CAJA_CERRADA") = "S" Then
    '                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & " - " & "MENSAJE : " & ClsKeyPOS.strMsjCajaCerrada)
    '                HidMsjCajaCerrada.Value = ClsKeyPOS.strMsjCajaCerrada
    '                HidFlagCajaCerrada.Value = 1
    '            Else
    '                HidFlagCajaCerrada.Value = 0
    '            End If
    '        Next
    '    Else
    '        HidMsjCajaCerrada.Value = ClsKeyPOS.strMsjCajaCerrada
    '        HidFlagCajaCerrada.Value = 1
    '    End If
    '    'PROY-31949 - Fin
    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : Validacion Integracion FIN")

    '    Me.HidCodOpera.Value = ClsKeyPOS.strCodOpeVE & "|" & _
    '    ClsKeyPOS.strCodOpeVC & "|" & ClsKeyPOS.strCodOpeAN

    '    Me.HidDesOpera.Value = ClsKeyPOS.strDesOpeVE & "|" & _
    '    ClsKeyPOS.strDesOpeVC & "|" & ClsKeyPOS.strDesOpeAN
    '    Me.HidTipoOpera.Value = ClsKeyPOS.strOpeFina & "|" & ClsKeyPOS.strNoFina 'OPE FI(90)
    '    Me.HidTipoTarjeta.Value = ClsKeyPOS.strCodTarjetaVS & "|" & ClsKeyPOS.strCodTarjetaMC

    '    If Not Request.QueryString("docSunat") Is Nothing Then
    '        Me.HidTipoPago.Value = ClsKeyPOS.strTipPagoDxP '03 Documentos por pagar
    '    Else
    '        Me.HidTipoPago.Value = ClsKeyPOS.strTipPagoVR '01 Venta Rapida
    '    End If

    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidIntAutPos : " & AppHidIntAutPos)
    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidGrabAuto : " & HidGrabAuto.Value)
    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidCodOpera : " & HidCodOpera.Value)
    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidTipoOpera : " & HidCodOpera.Value)

    '    Me.HidEstTrans.Value = ClsKeyPOS.strEstTRanPen & "|" & ClsKeyPOS.strEstTRanPro _
    '    & "|" & ClsKeyPOS.strEstTRanAce & "|" & ClsKeyPOS.strEstTRanRec & "|" & ClsKeyPOS.strEstTRanInc

    '    Me.HidTipoPOS.Value = ClsKeyPOS.strTipoPosVI & "|" & ClsKeyPOS.strTipoPosMC _
    '    & "|" & ClsKeyPOS.strTipoPosAM & "|" & ClsKeyPOS.strTipoPosDI

    '    Me.HidTipoTran.Value = ClsKeyPOS.strTipoTransPAG & "|" & ClsKeyPOS.strTipoTransANU & "|" _
    '    & ClsKeyPOS.strTipoTransRIM & "|" & ClsKeyPOS.strTipoTransRDO & "|" & _
    '    ClsKeyPOS.strTipoTransRTO & "|" & ClsKeyPOS.strTipoTransAPP & "|" & ClsKeyPOS.strTipoTransCIP

    '    Me.HidTipoMoneda.Value = ClsKeyPOS.strTipoMonSoles

    '    Me.HidTransMC.Value = ClsKeyPOS.strTranMC_Compra & "|" & ClsKeyPOS.strTranMC_Anulacion & "|" _
    '    & ClsKeyPOS.strTranMC_RepDetallado & _
    '    "|" & ClsKeyPOS.strTranMC_RepTotales & "|" & ClsKeyPOS.strTranMC_ReImpresion & _
    '    "|" & ClsKeyPOS.strTranMC_Cierre & "|" & ClsKeyPOS.strPwdComercio_MC



    '    Me.HidMonedaMC.Value = ClsKeyPOS.strMonedaMC_Soles & "|" & ClsKeyPOS.strMonedaMC_Dolares
    '    Me.HidApliPOS.Value = ClsKeyPOS.strConstMC_POS
    '    Me.HidMonedaVisa.Value = ClsKeyPOS.strMonedaVisa_Soles & "|" & ClsKeyPOS.strMonedaVisa_Dolares

    '    'DATOS DEL POS
    '    Me.HidDatoPosVisa.Value = ""
    '    Me.HidDatoPosMC.Value = ""

    '    Me.HidDatoAuditPos.Value = Funciones.CheckStr(CurrentTerminal()) & "|" & _
    '    Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")) & "|" & _
    '    Funciones.CheckStr(Session("USUARIO"))

    '    If AppHidIntAutPos = "1" Then
    '        Dim objSicarDB As New COM_SIC_Activaciones.clsTransaccionPOS
    '        Dim strIp As String = CurrentTerminal()
    '        Dim strEstadoPos As String = "1"
    '        Dim strTipoVisa As String = "V"
    '        Dim ds As DataSet
    '        Dim bvalida As Integer = 0

    '        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIpClient : " & strIpClient)

    '        'VISA INICIO
    '        strTipoVisa = "V"

    '        Dim arrIPDesc(2) As String
    '        arrIPDesc(0) = strIpClient

    '        ds = objSicarDB.ConsultarDatosPOS(strIpClient, AppALMACEN, strEstadoPos, strTipoVisa)

    '        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIp : " & strIp)
    '        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIpClient : " & strIpClient)
    '        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidPtoVenta : " & AppALMACEN)
    '        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strEstadoPos : " & strEstadoPos)
    '        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strTipoVisa : " & strTipoVisa)
    '        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos Visa : " & "Count : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

    '        Dim strMensajeVisa As String = ClsKeyPOS.strIPMsjDesconfigurado

    '        If ds.Tables(0).Rows.Count > 0 Then
    '            Me.HidDatoPosVisa.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
    '            & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
    '            & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
    '            & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
    '            & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
    '            & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
    '            & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
    '            & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA")) _
    '            & "|POSV_FLAGPOS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSC_FLG_SICAR"))

    '            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidDatoPosVisa : " & HidDatoPosVisa.Value)
    '        Else
    '            bvalida = 1
    '            Response.Write("<script>alert('" & strMensajeVisa & "');</script>")
    '        End If
    '        'VISA FIN

    '        'MC INICIO
    '        strTipoVisa = "M"
    '        ds = objSicarDB.ConsultarDatosPOS(strIpClient, AppALMACEN, strEstadoPos, strTipoVisa)

    '        Dim strMensajeMC As String = ClsKeyPOS.strIPMsjDesconfigurado

    '        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strIp : " & strIp)
    '        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidPtoVenta : " & AppALMACEN)
    '        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strEstadoPos : " & strEstadoPos)
    '        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "strTipoVisa : " & strTipoVisa)
    '        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos MC : " & "Count : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

    '        If ds.Tables(0).Rows.Count > 0 Then
    '            Me.HidDatoPosMC.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
    '            & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
    '            & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
    '            & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
    '            & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
    '            & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
    '            & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
    '            & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA")) _
    '            & "|POSV_FLAGPOS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSC_FLG_SICAR"))

    '            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "HidDatoPosMC : " & HidDatoPosMC.Value)
    '        Else
    '            If bvalida = 0 Then
    '                Response.Write("<script>alert('" & strMensajeMC & "');</script>")
    '            End If
    '        End If
    '        'MC FIN

    '        'MEDIOS DE PAGO PERMITIDO CON EL POS - INICIO'
    '        Dim dtViaPagoPOS As DataTable
    '        Dim strMsgRpta As String
    '        Dim strCodRpta As String

    '        Dim objConsultasTarjetaPos As New COM_SIC_Activaciones.clsTarjetasPOS
    '        Dim mediosPagoPermitidos As String
    '        mediosPagoPermitidos = ""
    '        dtViaPagoPOS = objConsultasTarjetaPos.ConsultarViasPagoPos("", strCodRpta, strMsgRpta)

    '        If dtViaPagoPOS.Rows.Count > 0 Then
    '            For Each item As DataRow In dtViaPagoPOS.Rows
    '                mediosPagoPermitidos = mediosPagoPermitidos & Funciones.CheckStr(item("CCINS")) & ";" & Funciones.CheckStr(item("TIP_TARJETA")) & "|"
    '            Next
    '        End If
    '        HidMedioPagoPermitidas.Value = mediosPagoPermitidos
    '        'MEDIOS DE PAGO PERMITIDO CON EL POS - FIN'

    '    End If
    '    objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strPedidoLog & "load_data_param_pos : " & "Fin : ")
    'End Sub

    Public Function Pagar(ByVal strDocumentoSap As String, _
                     ByVal strNumeroDocumento As String, _
                     ByVal strMontoRecarga As String, _
                     ByVal strCodigoPago As String, _
                     ByVal oEmpleados As BEDatosEmpleado, _
                     ByVal strIpLocal As String, _
                     ByVal strTipoDocVendedor As String, _
                     ByVal strNumDocVendedor As String, _
                     ByVal strNumReferencia As String, _
                     ByVal strTipoOperacionServ As String, _
                     ByVal strMontoTotal As String, _
                     ByVal strFlagTiendaVirtual As String, _
                     ByVal strFlagBiometria As String, _
                     ByVal idPedido As String, _
                     ByVal UUID As String, _
                     ByRef objBEResponseWebMethod As BEResponseWebMethod) As Boolean

        objBEResponseWebMethod = New BEResponseWebMethod
        Dim rpta As Boolean = False

        Try

            strIdentifyLog = String.Format("{0} || {1}", idPedido, UUID)

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "strDocumentoSap", Funciones.CheckStr(strDocumentoSap)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "strNumeroDocumento", Funciones.CheckStr(strNumeroDocumento)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "strMontoRecarga", Funciones.CheckStr(strMontoRecarga)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "strCodigoPago", Funciones.CheckStr(strCodigoPago)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "oEmpleados.ALMACEN", Funciones.CheckStr(oEmpleados.ALMACEN)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "oEmpleados.CANAL", Funciones.CheckStr(oEmpleados.CANAL)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "oEmpleados.COD_VENDEDOR", Funciones.CheckStr(oEmpleados.COD_VENDEDOR)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "oEmpleados.CODUSUARIO", Funciones.CheckStr(oEmpleados.CODUSUARIO)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "oEmpleados.CURRENT_USER", Funciones.CheckStr(oEmpleados.CURRENT_USER)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "oEmpleados.NOMBRE_COMPLETO", Funciones.CheckStr(oEmpleados.NOMBRE_COMPLETO)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "oEmpleados.OFICINA", Funciones.CheckStr(oEmpleados.OFICINA)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "oEmpleados.PERFIL", Funciones.CheckStr(oEmpleados.PERFIL)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "strIpLocal", Funciones.CheckStr(strIpLocal)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "strTipoDocVendedor", Funciones.CheckStr(strTipoDocVendedor)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "strNumDocVendedor", Funciones.CheckStr(strNumDocVendedor)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "strNumReferencia", Funciones.CheckStr(strNumReferencia)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "idPedido", Funciones.CheckStr(idPedido)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "UUID", Funciones.CheckStr(UUID)))


            If Load(strDocumentoSap, _
                 strNumeroDocumento, _
                 strMontoRecarga, _
                 strCodigoPago, _
                 oEmpleados, _
                 strIpLocal, _
                 strTipoDocVendedor, _
                 strNumDocVendedor, _
                 strNumReferencia, _
                 strTipoOperacionServ, _
                 strMontoTotal, _
                 strFlagTiendaVirtual, _
                 strFlagBiometria, _
                 idPedido, _
                 UUID) Then


                If guardarConectado() Then

                    Dim fechaRegistro As DateTime = DateTime.Now
                    Dim strCodRpta As String = String.Empty
                    Dim strMsjRpta As String = String.Empty

                    rpta = True
                    objBEResponseWebMethod.CodigoRespuesta = "0"
                    objBEResponseWebMethod.MensajeRespuesta = KeySettings.Key_MsjRptaExitoPagoRASICAR

                    Try


                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Numero Pedido", Funciones.CheckStr(AppQpDocSap)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Punto de Venta", Funciones.CheckStr(AppALMACEN)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Identificador", Funciones.CheckStr(AppUUID)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Fecha Registro", Funciones.CheckStr(fechaRegistro)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Es Renta", "S"))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Pedido Alta", Funciones.CheckStr(AppPedidoAlta)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Monto Calculado", Funciones.CheckStr(AppMontoPagar)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Monto Pagado", Funciones.CheckStr(ApptxtMonto1)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "TipoDoc Vendedor", Funciones.CheckStr(AppTipoDocVendedor)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "NumDoc Vendedor", Funciones.CheckStr(AppNumDocVendedor)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Numero Referencia", Funciones.CheckStr(AppNumeroReferencia)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Cod Medio Pago", Funciones.CheckStr(AppCodMedioPago)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Tipo Operacion", Funciones.CheckStr(AppTipoOperacion)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Flag Biometria", Funciones.CheckStr(AppFlagBiometria)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Flag Actualizar MSSAP", Funciones.CheckStr(AppActualizarMSSAP)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Flag ActivosPostpagoConvergente", Funciones.CheckStr(AppActivosPostpagoConvergente)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Flag Actualizar Contrato", Funciones.CheckStr(AppActualizarContrato)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Flag Envio a SAP", Funciones.CheckStr(AppEnvioSAP)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Flag Envio a Paperless", Funciones.CheckStr(AppPaperless)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Flag Tienda Virtual", Funciones.CheckStr(AppFlagTiendaVirtual)))


                        Dim idNumeroPedido As Int64 = Funciones.CheckInt64(AppQpDocSap)
                        Dim dblMontoCalculado As Double = Funciones.CheckDbl(AppMontoPagar)
                        Dim dblMontoPagado As Double = Funciones.CheckDbl(ApptxtMonto1)

                        objTrsMsSap.RegistrarPagoSrvSicar(idNumeroPedido, _
                                                          AppALMACEN, _
                                                          AppUUID, _
                                                          AppstrUsuario, _
                                                          fechaRegistro, _
                                                          "S", _
                                                          AppPedidoAlta, _
                                                          dblMontoCalculado, _
                                                          dblMontoPagado, _
                                                          AppTipoDocVendedor, _
                                                          AppNumDocVendedor, _
                                                          AppNumeroReferencia, _
                                                          AppFlagBiometria, _
                                                          AppActualizarMSSAP, _
                                                          AppActivosPostpagoConvergente, _
                                                          AppActualizarContrato, _
                                                          AppEnvioSAP, _
                                                          AppPaperless, _
                                                          AppCodMedioPago, _
                                                          AppFlagTiendaVirtual, _
                                                          AppTipoOperacion, _
                                                          strCodRpta, _
                                                          strMsjRpta)


                        rpta = True
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Codigo Respuesta", Funciones.CheckStr(strCodRpta)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Mensaje Respuesta", Funciones.CheckStr(strMsjRpta)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "objBEResponseWebMethod.CodigoRespuesta", Funciones.CheckStr(objBEResponseWebMethod.CodigoRespuesta)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "objBEResponseWebMethod.MensajeRespuesta", Funciones.CheckStr(objBEResponseWebMethod.MensajeRespuesta)))

                    Catch ex As Exception

                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "ERROR AL GRABAR EN SSAPT_PAGO_SRVSICAR", Funciones.CheckStr(ex.Message)))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "ERROR AL GRABAR EN SSAPT_PAGO_SRVSICAR", Funciones.CheckStr(ex.StackTrace)))

                    End Try
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "strErrorGeneral", Funciones.CheckStr(ApperrorGeneral)))
                    objBEResponseWebMethod.CodigoRespuesta = "-1"
                    objBEResponseWebMethod.MensajeRespuesta = Funciones.CheckStr(ApperrorGeneral)
                End If

            Else

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Ocurrio un error en el metodo LOAD"))
                objBEResponseWebMethod.CodigoRespuesta = "-1"
                objBEResponseWebMethod.MensajeRespuesta = "Ocurrio un error en el Load del metodo pagar de RA" 'Key_MsjRptaErrorLoadRA
            End If
        Catch ex As Exception

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "ERROR AL REALIZAR PAGO RA", Funciones.CheckStr(ex.Message)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "ERROR AL REALIZAR PAGO RA", Funciones.CheckStr(ex.StackTrace)))

            objBEResponseWebMethod.CodigoRespuesta = "-1"
            objBEResponseWebMethod.MensajeRespuesta = "Ocurrio un error al realizar el pago de Renta Adelantada" 'Key_MsjRptaErrorPagoRA

        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}- {1} : {2}", strIdentifyLog, "Respuesta final", Funciones.CheckStr(rpta)))

        Return rpta
    End Function

    Private Function guardarConectado() As Boolean

        'NRO CONTRATO  TODOEB 
        Dim strIdentifyLog As String = dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")      'strNroPedidoSAP
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Grabar pagos.")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")

        '************************************************************************************************************'
        'CONSULTA EL CONTRATO:
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Obtener contrato.")
            If Not ApprecargaVirtual Then
                objContrato = objClsConsultaPvu.ObtenerDrsap(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO"), P_COD_RESP, P_MSG_RESP)
            Else
                objContrato = Nothing
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato Obtenido: " & Funciones.CheckStr(objContrato.Tables(0).Rows(0).Item("ID_CONTRATO")))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato P_COD_RESP : " & Funciones.CheckStr(P_COD_RESP))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato P_MSG_RESP : " & Funciones.CheckStr(P_MSG_RESP))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Obtener contrato.")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Error en obtener contrato :" & dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Err." & ex.Message.ToString)
        End Try
        '************************************************************************************************************'

        'Dim strIdentifyLog As String = drPagos.Item("PEDIN_NROPEDIDO")      'strNroPedidoSAP
        'Dim strIdentifyLog As String = drPagos.Item("VBELN") TODOEB

        'Validacion de Tarjeta de Credito
        'If Not valida_tarjeta() Then Exit Sub
        'Fin Validacion de Tarjeta de Credito

        'dsEfectivo = objCajas.FP_Get_ListaParamOficina(Session("CANAL"), ConfigurationSettings.AppSettings("CodAplicacion"), Session("ALMACEN"))
        'dblEfecCaja = objCajas.FP_CalculaEfectivo(Session("ALMACEN"), "60138")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Consulta el efectivo para la caja")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Parametro de consulta :" & AppALMACEN)
        '** CONSULTA EFECTIVO:
        dsEfectivo = objCajas.FP_Get_ListaParamOficina(AppCANAL, ConfigurationSettings.AppSettings("CodAplicacion"), AppALMACEN)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Fin consulta paràmetros oficina")

        'dblEfecCaja = objCajas.FP_CalculaEfectivo(Session("ALMACEN"), Session("USUARIO"), drPagos.Item("FKDAT")) TODOEB
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Inicia Consulta Càlculo Efectivo")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Paràmetros: Punto Venta: " & AppALMACEN & "  Fecha: " & DateTime.Now)
        'dblEfecCaja = objCajas.FP_CalculaEfectivo(Session("ALMACEN"), Session("USUARIO"), DateTime.Now)
        '** CONSULTA EFECTIVO CAJA:

        ' Dim dsPedido As DataSet
        '****** CONSULTA PRINCIPAL PARA EL PREOCESO *****'
        '***************************************************************************************************'
        '***************************************************************************************************'
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta los datos del pedido=> " & Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")))
        dsPedido = objConsultaMsSap.ConsultaPedido(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO"), "", "") 'TODOEB
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta pedido.")
        '***************************************************************************************************'
        '***************************************************************************************************'
        '***************************************************************************************************'

        Dim cultureNameCaja As String = "es-PE"
        Dim cultureCaja As CultureInfo = New CultureInfo(cultureNameCaja)
        Dim dateTimeValueCajaEfe As DateTime
        dateTimeValueCajaEfe = Convert.ToDateTime(DateTime.Now, cultureCaja)
        Dim dFechaCaj0 As Date
        Dim sFechaCaj0 As String = dateTimeValueCajaEfe.ToLocalTime.ToShortDateString
        'PROY-140397-MCKINSEY -> JSQ INICIO
        sFechaCaj0 = DateTime.Now().ToString("dd/MM/yyyy")
        'PROY-140397-MCKINSEY -> JSQ FIN
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Fecha Pedido : " & sFechaCaj0)

        dblEfecCaja = objCajas.FP_CalculaEfectivo(AppALMACEN, AppUSUARIO, sFechaCaj0)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Paràmetros: Punto Venta: " & AppALMACEN & "  Fecha: " & Funciones.CheckStr(DateTime.Now))

        '*** AVILLAR  ****'
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -Consulta el monto configurado de tarjeta.")
            dsDatGenerales = objConfig.FP_Lista_Param_Tarjeta(1)
            'Valor de monto de tarjeta configurado.
            If Not dsDatGenerales Is Nothing Then
                dblMontoTarjeta = dsDatGenerales.Tables(0).Rows(0).Item("PARTN_MONTO")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -dblMontoTarjeta: " & Funciones.CheckStr(dsDatGenerales.Tables(0).Rows(0).Item("PARTN_MONTO")))
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Error al consultar el monto configurado para la tarjeta.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Err." & ex.Message.ToString)
        End Try
        ' *** AVILLAR  ****'


        If dsEfectivo.Tables(0).Rows.Count = 0 Then
            'Response.Write("<script>alert('No existe configuracion de caja para este punto de venta.')</script>")
            ApperrorGeneral = "No existe configuracion de caja para este punto de venta."
            Exit Function
        End If

        dblTolerancia = dsEfectivo.Tables(0).Rows(0).Item("CAJA_TOLERANCIA_SOL")

        If dblEfecCaja >= dsEfectivo.Tables(0).Rows(0).Item("CAJA_MAX_DISP_SOL") + dblTolerancia Then
            'Response.Write("<script>alert('Ha alcanzado su maximo disponible de efectivo en caja. Debe depositar en caja buzón')</script>")
            ApperrorGeneral = "Ha alcanzado su maximo disponible de efectivo en caja. Debe depositar en caja buzón"
        Else

            'blnSunat = (drPagos.Item("XBLNR") = "" Or drPagos.Item("XBLNR") = "0000000000000000") TODOEB
            If Not Convert.IsDBNull(dsPedido.Tables(0).Rows(0).Item("PAGOC_CODSUNAT")) Then
                blnSunat = (dsPedido.Tables(0).Rows(0).Item("PAGOC_CODSUNAT") = "" Or dsPedido.Tables(0).Rows(0).Item("PAGOC_CODSUNAT") = "0000000000000000")
            Else
                blnSunat = False
            End If

            'blnSunat = False
            ' objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PAGOC_CODSUNAT : " & drPagos.Item("PAGOC_CODSUNAT").ToString()) 'TODOEB
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "AC111 : " & "AC111")
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "blnSunat : " & blnSunat)

            If Trim(ApptxtNumSunat) = "" Then
                strNumSunat = ApptxtCorrelativo
            Else
                strNumSunat = ApptxtNumSunat
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "NumSunat : " & strNumSunat.ToString())

            If blnSunat Then
                strNumAsignaSUNAT = ApptxtCompleto
            Else
                strNumAsignaSUNAT = strNumSunat
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strNumAsignaSUNAT : " & strNumAsignaSUNAT.ToString())

            'Prepago cuotas 23/10/2006
            'Dim dsPedido As DataSet '= objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drPagos.Item("VBELN"), "") TODOEB

            ''****** CONSULTA PRINCIPAL PARA EL PREOCESO *****'
            ''***************************************************************************************************'
            ''***************************************************************************************************'
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta los datos del pedido=> " & Funciones.CheckStr(drPagos.Item("PEDIN_NROPEDIDO")))
            'dsPedido = objConsultaMsSap.ConsultaPedido(drPagos.Item("PEDIN_NROPEDIDO"), "", "") 'TODOEB
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta pedido.")
            ''***************************************************************************************************'
            ''***************************************************************************************************'
            ''***************************************************************************************************'

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Validacion que se ha pagado el documento")
            Dim valida_pago As String = ""

            Try
                valida_pago = dsPedido.Tables(0).Rows(0).Item("PEDIC_ESTADO")

                If valida_pago = ConfigurationSettings.AppSettings("ESTADO_PAG") Then
                    'Session("Valida_Pago_MSG") = "El documento se encuentra Pagado."
                    'Response.Redirect("PoolPagos.aspx", False)
                    ApperrorGeneral = "El documento se encuentra Pagado."
                    Exit Function
                End If
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & ex.StackTrace)
                'Session("Valida_Pago_MSG") = "Ocurrio un error al consultar los datos para el Pago"
                'Response.Redirect("PoolPagos.aspx", False)
                ApperrorGeneral = "Ocurrio un error al consultar los datos para el Pago"
                Exit Function
            End Try

            Dim K_COD_RESPUESTA As String
            Dim K_MSJ_RESPUESTA As String
            Dim K_ID_TRANSACCION As String

            '*******************************************************************************************************
            '**1. sisact_info_venta_sap => consulta con el numero de pedido(nro_documento), retorna el n_id_venta.
            '**2. sisact_ap_venta v / sisact_ap_contrato, donde  v.id_documento=n_id_venta => c_venta
            '**3. sisact_ap_venta_detalle vd => vd.id_documento = n_id_venta => c_venta_det  
            Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia consulta datos pedido en PVU")
                objClsConsultaPvu.ConsultarPedidosPVU(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO"), _
                                                              P_CODIGO_RESPUESTA, _
                                                              P_MENSAJE_RESPUESTA, _
                                                              C_VENTA, _
                                                              C_VENTA_DET)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin consulta datos pedido en PVU")
                If P_MENSAJE_RESPUESTA <> "OK" Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  No se encontro el pedido en la tabla : sisact_info_venta_sap. pkg:sisact_pkg_venta sp:sp_con_venta_x_docsap => " & dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO"))
                End If
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Error, no encontro datos al ejecutar el sp: SP_CON_VENTA_X_DOCSAP , para el pedido => " & dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO"))
            End Try
            '*******************************************************************************************************

            '-------------------------------------------------------------------------------------------------------'
            '*** Son DataTables que retornan del procedimiento, y consultan el PVU con el NroPedido             ***'
            '*** actualmente van a retornan en vacio, debido a que el pedido no se registra en la tabla:        ***'
            '##     Tabla: sisact_info_venta_sap => sap.nro_documento = PEDIN_NROPEDIDO'
            '##     sisact_ap_venta v, sisact_ap_contrato co =>C_VENTA
            '##     sisact_ap_venta_detalle => C_VENTA_DET
            '-------------------------------------------------------------------------------------------------------'


            If C_VENTA.Rows.Count = 0 Then
                C_VENTA = Nothing
            End If

            If C_VENTA_DET.Rows.Count = 0 Then
                C_VENTA_DET = Nothing
            End If


            C_VENTA_DET = C_VENTA_DET
            C_VENTA = C_VENTA

            Dim dblTotPreCuo As Double
            Dim dblTotalReg As Double
            'si es venta prepago y ademas tiene cuotas  
            'inicio promocion modem + laptop



            '********************************************************************************************'
            '** Estas variables no estan siendo utilizadas, lo comentamos
            'sTipoVenta = CheckStr(drPagos.Item("PEDIC_TIPOVENTA"))
            'sTipoOperacion = CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION"))
            '********************************************************************************************'


            ' E75893 - Validacion Adjuntar Documento Cliente solo Ventas Altas Prepago

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia ValidacionVentaDOL")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ValidacionVentaDOL")


            '***IDG****'
            '*****Validación Pago del documento total **********************************************************************************************'
            If Trim(ApptxtMonto1) <> "" Then
                dblTotalReg = CDbl(ApptxtMonto1)
            End If
            'If Trim(txtMonto2.Text) <> "" Then
            '    dblTotalReg += CDbl(txtMonto2.Text)
            'End If
            'If Trim(txtMonto3.Text) <> "" Then
            '    dblTotalReg += CDbl(txtMonto3.Text)
            'End If
            'If Trim(txtMonto4.Text) <> "" Then
            '    dblTotalReg += CDbl(txtMonto4.Text)
            'End If
            'If Trim(txtMonto5.Text) <> "" Then
            '    dblTotalReg += CDbl(txtMonto5.Text)
            'End If

            If Math.Round(dblTotalReg, 2) <> Math.Round(Funciones.CheckDbl(ApptxtNeto), 2) Then
                ' Response.Write("<script>alert('No se permiten pagos parciales. El pago debe ser por el total del documento.')</script>")
                ApperrorGeneral = "No se permiten pagos parciales. El pago debe ser por el total del documento."
                Exit Function
            End If
            '***************************************************************************************************************************************'
            '***FDG******'



            '----- CAMPAÑA PRE + POST
            If blnSunat Then
                'dsResult = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drPagos.Item("VBELN"), "")
                dsResult = objConsultaMsSap.ConsultaPedido(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO"), "", "")

                If Not IsNothing(dsResult) Then
                    'strNroDoc = dsResult.Tables(0).Rows(0).Item("DOCUMENTO") TODOEB
                    'strNroDoc = dsResult.Tables(0).Rows(0).Item("PEDIV_NRODOCCLIENTE")
                    strNroDoc = dsResult.Tables(0).Rows(0).Item("CLIEV_NRODOCCLIENTE")
                Else
                    strNroDoc = ""
                End If

                blnFlagPago = True
            End If
            ' FIN CAMPAÑA PRE + POST
            dblEfectivo = 0
            dblTotalTarjeta = 0
            If ApphidNumFilas = "" Then ApphidNumFilas = 0
            strDetallePago = ""
            'PROY-27440 INI
            Dim strMonto As String = ""

            Dim strTipoDocPago As String = ""
            'PROY-27440 FIN

            For i = 1 To ApphidNumFilas

                'PROY-27440 INI

                'objTxt = CType(Me.FindControl("txtMonto1"), TextBox)
                'strMonto = "" : strMonto = Trim(objTxt.Text)
                strMonto = "" : strMonto = Trim(ApptxtMonto1)

                'objCbo = CType(Me.FindControl("cboTipDocumento1"), DropDownList)
                'strTipoDocPago = "" : strTipoDocPago = Trim(objCbo.SelectedValue)
                strTipoDocPago = "" : strTipoDocPago = Trim(AppcboTipDocumento1)

                strDetallePago = strDetallePago & ";"
                'strDetallePago = strDetallePago & Session("ALMACEN") & ";" ' Concatenar detalles de pag
                strDetallePago = strDetallePago & AppALMACEN & ";"    ' Concatenar detalles de pago
                strDetallePago = strDetallePago & strTipoDocPago & ";"    ' Codigo del Tipo de documento (Via de Pago) 'PROY-27440
                strDetallePago = strDetallePago & ";"
                strDetallePago = strDetallePago & ";"
                strDetallePago = strDetallePago & strMonto & ";"    ' Monto de pago  'PROY-27440
                'If (Session("VarVal2") = Session("CANAL")) Then      ' averiguar en que variable se guarda canal - MONEDA
                strDetallePago = strDetallePago & "PEN" & ";"
                'Else
                '    strDetallePago = strDetallePago & "L" & ";"
                'End If

                strDetallePago = strDetallePago & ";"

                strDetallePago = strDetallePago & strNumAsignaSUNAT & ";"
                strDetallePago = strDetallePago & Trim(strTipoDocPago) & ";"    ' GLOSA QUE ES LO MISMO DE VIA_PAGO 'PROY-27440
                '  strDetallePago = strDetallePago &  DateTime.Now & ";"    ' Fecha de documento que esta en el detalle - F_PEDIDO TODOEB
                strDetallePago = strDetallePago & DateTime.Now & ";"    ' Fecha de documento que esta en el detalle - F_PEDIDO
                strDetallePago = strDetallePago & ";"

                'If (Session("VarVal2") = Session("CANAL")) Then
                strDetallePago = strDetallePago & ";"
                'Else
                '    strDetallePago = strDetallePago & strTipoFact & ";"
                'End If

                If Trim(strTipoDocPago) = "ZEFE" Then
                    '27444 INI

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Monto Efectivo : " & strMonto)

                    dblEfectivo += CDbl(Trim(strMonto))
                    '27444 FIN
                End If

                If (i < CInt(ApphidNumFilas)) Then
                    strDetallePago = strDetallePago & "|"
                End If

                'Valida sumatoria de tarjetas....
                'AVILLAR : ZNCR
                '27440 INI
                If Trim(strTipoDocPago) <> "ZNCR" And Trim(strTipoDocPago) <> "ZEFE" And Trim(strTipoDocPago) <> "ZEAM" And Trim(strTipoDocPago) <> "ZEOV" And Trim(strTipoDocPago) <> "TDPP" And Trim(strTipoDocPago) <> "ZDEL" Then
                    dblTotalTarjeta += CDbl(Trim(strMonto))
                End If
                '27440 FIN
                'FIN AVILLAR
            Next


            If (dblEfectivo + dblEfecCaja) >= dblTolerancia + dsEfectivo.Tables(0).Rows(0).Item("CAJA_MAX_DISP_SOL") Then
                'Response.Write("<script>alert('Este pago excede su tolerancia de monto máximo de efectivo en caja')</script>")
                'Session("strMensajeCaja") = "Este pago excedió su tolerancia de monto máximo de efectivo en caja"
                ApperrorGeneral = "Este pago excedió su tolerancia de monto máximo de efectivo en caja"
            End If
            'Else

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ListarDatosCabeceraVenta - strDocSap: " & strDocSap)
            dtDatos = objBus.ListarDatosCabeceraVenta(strDocSap)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ListarDatosCabeceraVenta - dtDatos: " & dtDatos.Rows.Count.ToString)

            ' INICIO FMES :DETERMINA SI TIENE EL FLAG DE PILOTO VENTA EN CAC
            If dtDatos.Rows.Count > 0 Then
                If Funciones.CheckStr(dtDatos.Rows(0).Item("VEPR_FLAG_VENTA_CAC").ToString()) = "1" Then
                    booDol = False
                End If
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ListarDatosCabeceraVenta - booDol: " & booDol.ToString)
            ' FIN FMES :DETERMINA SI TIENE EL FLAG DE PILOTO VENTA EN CAC


            If 1 = 1 Then 'blnSunat Then TODOEB

                ''''***IDG*****************************************************************************************************************************************************'
                '''**** CONSULTA DATOS DEL CAJERO ****'
                Dim cultureNameX As String = "es-PE"
                Dim cultureX As CultureInfo = New CultureInfo(cultureNameX)
                Dim dateTimeValueCaja As DateTime
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Iniciamos la consulta de los datos CAJA")
                dateTimeValueCaja = Convert.ToDateTime(DateTime.Now, cultureX)
                Dim strFecha As String = ""
                strFecha = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000")

                Try
                    objOfflineCaja = New COM_SIC_OffLine.clsOffline
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "---------------------------------------------------- ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Paràmetros : GetDatosAsignacionCajero")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param1: " & Funciones.CheckStr(AppALMACEN))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param2: " & Funciones.CheckStr(dateTimeValueCaja.ToShortDateString))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param2: " & Funciones.CheckStr(strFecha.ToString))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param3: " & "00000" & Funciones.CheckStr(AppUSUARIO))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "---------------------------------------------------- ")



                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-----------------------------------------------------------------------------")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Test de fechas:")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- strFecha " & strFecha.ToString)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- dateTimeValueCaja " & dateTimeValueCaja.ToLocalTime.ToShortDateString)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- fecha pedido " & CStr(dsPedido.Tables(0).Rows(0).Item("PEDID_FECHADOCUMENTO")))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-----------------------------------------------------------------------------")


                    'dsCajeroA = objOfflineCaja.GetDatosAsignacionCajero(Funciones.CheckStr(Session("ALMACEN")), Funciones.CheckStr(dateTimeValueCaja.ToShortDateString), "00000" & Funciones.CheckStr(Session("USUARIO")).ToString)
                    Dim dFecha As Date
                    Dim sFecha As String = dateTimeValueCaja.ToLocalTime.ToShortDateString
                    'PROY-140397-MCKINSEY -> JSQ INICIO
                    sFecha = DateTime.Now().ToString("dd/MM/yyyy")
                    'PROY-140397-MCKINSEY -> JSQ FIN
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Fecha Pedido : " & sFecha)
                    Dim codigo_vendedor_session As String = ""
                    codigo_vendedor_session = Funciones.CheckStr(AppUSUARIO).PadLeft(10, "0")

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "---------------------------------------------------- ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Paràmetros : GetDatosAsignacionCajero")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param1: " & Funciones.CheckStr(AppALMACEN))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param2: " & Funciones.CheckStr(dateTimeValueCaja.ToShortDateString))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param2: " & Funciones.CheckStr(strFecha.ToString))
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param3: " & "00000" & Funciones.CheckStr(Session("USUARIO")))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param3: " & codigo_vendedor_session)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "---------------------------------------------------- ")

                    'dsCajeroA = objOfflineCaja.GetDatosAsignacionCajero(Funciones.CheckStr(Session("ALMACEN")), sFecha, "00000" & Funciones.CheckStr(Session("USUARIO")).ToString)
                    'dsCajeroA = objOfflineCaja.GetDatosAsignacionCajero(Funciones.CheckStr(AppALMACEN), sFecha, codigo_vendedor_session)
                    'If (dsCajeroA Is Nothing OrElse dsCajeroA.Tables(0).Rows.Count <= 0) Then
                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Cantidad de Registros devueltos: " & dsCajeroA.Tables(0).Rows.Count.ToString())
                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- No se pudo determinar el numero de la caja asignada.")
                    '    'Response.Write("<script>alert('No se encontro el Nùmero/Nombre de caja asignado.')</script>")
                    '    ApperrorGeneral = "Este pago excedió su tolerancia de monto máximo de efectivo en caja"
                    '    Exit Sub
                    'End If

                    '' Validar cierre de caja
                    'If Not dsCajeroA Is Nothing Then
                    '    For cont As Int32 = 0 To dsCajeroA.Tables(0).Rows.Count - 1
                    '        If dsCajeroA.Tables(0).Rows(cont).Item("CAJA_CERRADA") = "S" Then
                    '            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE : " & "Caja Cerrada, no es posible realizar el pago.")
                    '            'Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", "Caja Cerrada, no es posible realizar el pago.")
                    '            'Me.RegisterStartupScript("RegistraAlerta", script)
                    '            ApperrorGeneral = "Caja Cerrada, no es posible realizar el pago."
                    '            Exit Sub
                    '        End If
                    '    Next
                    'End If

                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Error al tratar de consultar los datos de CAJA")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Error: " & ex.Message.ToString)
                End Try

                '**Buscamos el nombre de la Caja
                'If dsCajeroA.Tables(0).Rows.Count > 0 Then
                '    dsCajeroB = objOfflineCaja.Get_CajaOficinas(AppALMACEN)
                '    If (dsCajeroB Is Nothing OrElse dsCajeroA.Tables(0).Rows.Count <= 0) Then
                '        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- No se pudo determinar el nombre de la caja asignada.")
                '        'Response.Write("<script>alert('No se encontro el Nùmero/Nombre de caja asignado.')</script>")
                '        ApperrorGeneral = "No se encontro el Nùmero/Nombre de caja asignado."
                '        Exit Sub
                '    Else
                '        For c As Int16 = 0 To dsCajeroB.Tables(0).Rows.Count - 1
                '            '**Comparamos los numeros de caja:
                '            If dsCajeroA.Tables(0).Rows(0).Item("CAJA") = dsCajeroB.Tables(0).Rows(c).Item("CASNR") Then
                '                strNombreCaja = Funciones.CheckStr(dsCajeroB.Tables(0).Rows(c).Item("BEZEI")).ToString.Trim
                '            End If
                '        Next
                '    End If
                'End If
                '***FDG*****************************************************************************************************************************************************'

                '**IDG**
                '***Cuando el pago se hace con forma NC y no cumple las validaciones ***

                ''DMZ - Comentado porque no se usa Nota de Credito
                'Try
                '    If ConsultaFormaPagoNotaCredito() = False Then
                '        Exit Sub    '** terminamos el proceso **'
                '    End If
                'Catch ex As Exception

                'End Try
                ''DMZ - Comentado porque no se usa Nota de Credito
                '**FDG*********************************************************************

                '''INI : Validacion de Medios de pago perimitidos RA TS-CCC
                '****************************Consulta de Datos********************************
                Dim Tipo_docume_nuevo As String
                Dim swError As Boolean = False
                Dim strMensajeAlertMP As String

                '27440 INI

                'objCbo = CType(Me.FindControl("cboTipDocumento1"), DropDownList)
                'strTipoDocPago = "" : strTipoDocPago = Trim(objCbo.SelectedValue)
                strTipoDocPago = "" : strTipoDocPago = Trim(AppcboTipDocumento1)

                Dim cboMedioPagoSeleted As String = strTipoDocPago

                '27440 FIN
                Try
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "INI Log para obtener el Formas de Pago de PVU.  Stored Procedure : Get_NuevoCodMedioPago ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "IN Medio De pago: " & cboMedioPagoSeleted)
                    Dim dsMP As DataSet = objOfflineCaja.Get_NuevoCodMedioPago(cboMedioPagoSeleted)
                    If dsMP.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(dsMP.Tables(0).Rows(0).Item(0)) Then
                            Tipo_docume_nuevo = dsMP.Tables(0).Rows(0).Item(0)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "OUT Tipo_docume_nuevo: " & Tipo_docume_nuevo)
                            If Tipo_docume_nuevo.ToUpper() = "NO APLICA" Then
                                swError = True
                            End If
                        Else
                            swError = True
                        End If
                    Else
                        swError = True
                    End If
                    If swError Then
                        strMensajeAlertMP = "Medio de Pago Invalido para Pago de DRA"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Error al obtener el EQ Formas de Pago PVU: " & strMensajeAlertMP)
                        'Response.Write("<script language=jscript> alert('" & strMensajeAlertMP & "'); </script>")
                        ApperrorGeneral = "Medio de Pago Invalido para Pago de DRA"
                        Exit Function
                    End If
                Catch ex As Exception
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: guardarConectado)"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception: " & ex.ToString & MaptPath)
                    'FIN PROY-140126

                Finally
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "FIN Log para obtener el Formas de Pago de PVU.  Stored Procedure : Get_NuevoCodMedioPago ")
                End Try
                '''FIN : Validacion de Medios de pago perimitidos RA - TS-CCC

                Try
                    '*************************'
                    '**** BLOQUE II *******:
                    '*************************'
                    Dim txtMonto As Double
                    Dim cboTipDocumento As String
                    Dim cboDescTipDocumento As String

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio del registro del pago")
                    objTrsMsSap.RegistrarPago(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO"), DBNull.Value, _
                                                 System.Configuration.ConfigurationSettings.AppSettings("ESTADO_PAG"), _
                                                 "CAJA PRUEBA", _
                                                 0, _
                                                  System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                                  AppUSUARIO, _
                                                  DBNull.Value, _
                                                  AppUSUARIO, _
                                                  DBNull.Value, _
                                                  K_NROLOG, K_DESLOG, K_PAGON_IDPAGO, K_PAGOC_CORRELATIVO)


                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin del registro del pago")
                    If K_DESLOG = "OK" Then
                        'Session.Add("msgErrorGenerarSot", "Registro de Pago Correctamente")
                        AppMensajeExito = "Registro de Pago Correctamente"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Registro de forma correcta del la cabecera del pago :" & K_PAGON_IDPAGO)

                        '************************************************************************
                        If (dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA") <> ConfigurationSettings.AppSettings("strTipDoc") And Funciones.CheckDbl(dsPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")) > 0) Then
                            '** se preocesan los otros tipos de documentos.
                            For i = 1 To ApphidNumFilas

                                'PROY-27440 INI

                                'objTxt = CType(Me.FindControl("txtMonto1"), TextBox)
                                'strMonto = "" : strMonto = Trim(objTxt.Text)

                                strMonto = "" : strMonto = Trim(ApptxtMonto1)

                                'objCbo = CType(Me.FindControl("cboTipDocumento1"), DropDownList)
                                'strTipoDocPago = "" : strTipoDocPago = Trim(objCbo.SelectedValue)

                                strTipoDocPago = "" : strTipoDocPago = Trim(AppcboTipDocumento1)

                                cboTipDocumento = strTipoDocPago
                                txtMonto = Double.Parse(strMonto)

                                'PROY-27440 FIN
                                cboDescTipDocumento = DevuelteTipoDescPago(cboTipDocumento)

                                objTrsMsSap.RegistrarDetallePago(K_PAGON_IDPAGO, cboTipDocumento, _
                                                                    cboDescTipDocumento, _
                                                                    System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                                                    txtMonto, _
                                                                    AppUSUARIO, _
                                                                    DBNull.Value, AppUSUARIO, _
                                                                    DBNull.Value, _
                                                                    IIf(arrayListaReferencia(i) = "", "", Funciones.CheckStr(arrayListaReferencia(i))), _
                                                                    arrayListaRefPedido(i), _
                                                                    K_NROLOG_DET, K_DESLOG_DET)
                            Next
                        Else
                            '** Registro del detalle de pago de la Nota de Crèdito/ TG  **
                            cboDescTipDocumento = DevuelteTipoDescPago(AppcboTipDocumento1)
                            objTrsMsSap.RegistrarDetallePago(K_PAGON_IDPAGO, AppcboTipDocumento1, _
                                                              cboDescTipDocumento, _
                                                             System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                                             Funciones.CheckDbl(ApptxtMonto1), _
                                                             AppUSUARIO, _
                                                             DBNull.Value, AppUSUARIO, _
                                                             DBNull.Value, _
                                                             "", 0, _
                                                             K_NROLOG_DET, K_DESLOG_DET)
                        End If

                        '************************************************************************

                        Dim FE_NRO_ERROR As Int16 = 0
                        Dim FE_DES_ERROR As String = ""
                        Dim K_PAGOV_RECUP_CORRELATIVO As String = ""
                        Dim flag_paperless As String = ""
                        Dim correlativo_recu As String = ""

                        Dim Origen As String = "3" 'Valor para validar que sea SICAR 6.0


                        If K_DESLOG_DET = "OK" Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Registro de forma correcta del detalle del pago :" & K_DESLOG_DET & " - " & K_PAGON_IDPAGO)
                            Try
                                '=================================================================================================================================================================================================================='
                                'CORRELATIVO Y ACTUALIZACIÒN DEL PAGO PEDIDO'
                                '=================================================================================================================================================================================================================='

                                K_NROLOG = ""
                                K_DESLOG = ""

                                '************************************REnta Adelante Valor por por defecto del Correlativo en caso de ser Renta*********************************
                                If Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")) = ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA") Then
                                    K_CU_CORRELATIVOFE = ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA")
                                End If
                                '************************************REnta Adelante Valor por por defecto del Correlativo en caso de ser Renta*********************************
                                If K_CU_CORRELATIVOFE <> "" Then
                                    K_NROLOG = ""
                                    K_DESLOG = ""

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Inicia actualizar pago: ActualizarPagodelPedido")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "SP: " & "PKG_MSSAP.SSAPSU_UPDATEPAGO")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param: " & Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param: " & Funciones.CheckStr(K_PAGON_IDPAGO))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param: " & Funciones.CheckStr(K_CU_CORRELATIVOFE))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param: PEDIC_CLASEFACTURA" & Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param: " & Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("OFICV_CODOFICINA")))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param: " & Funciones.CheckStr(K_NROLOG))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Param: " & Funciones.CheckStr(K_DESLOG))

                                    'PEDIC_CLASEFACTURA
                                    objTrsMsSap.ActualizarPagodelPedido(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO"), _
                                                                                        K_PAGON_IDPAGO, _
                                                                                        K_CU_CORRELATIVOFE, _
                                                                                        dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"), _
                                                                                        dsPedido.Tables(0).Rows(0).Item("OFICV_CODOFICINA"), _
                                                                                        K_NROLOG, K_DESLOG)

                                    If (K_DESLOG = "OK") Then
                                        AppActualizarMSSAP = "0"
                                    Else
                                        AppActualizarMSSAP = "1"
                                    End If
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  K_DESLOG :" & K_DESLOG)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  K_NROLOG :" & K_NROLOG)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Fin actualizar pago: ActualizarPagodelPedido")
                                Else
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  ERROR EN EL Correlativo :" & K_CU_CORRELATIVOFE)
                                    '** RollBack a los Pagos ************************'
                                    DeshacerCambiosPagos(K_PAGON_IDPAGO)
                                    'Response.Write("<script>alert('No tiene configurado correlativo para el punto de venta : ' & '" & dsPedido.Tables(0).Rows(0).Item("OFICV_CODOFICINA") & "')</script>")
                                    ApperrorGeneral = "No tiene configurado correlativo para el punto de venta : ' & '" & dsPedido.Tables(0).Rows(0).Item("OFICV_CODOFICINA") & ""
                                    Exit Function
                                    '*************************************************'
                                End If


                                If K_DESLOG = "OK" Then
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  sE ACTUALIZORN LOS PAGOS :" & K_PAGON_IDPAGO)
                                Else
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  ERROR EN LA ACTUALIZACIÒN")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  K_DESLOG :" & K_DESLOG)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  K_NROLOG :" & K_NROLOG)
                                    '** RollBack a los Pagos en la Actualizaciòn pagos ************************'
                                    DeshacerCambiosPagos(K_PAGON_IDPAGO)
                                    '*************************************************'
                                End If
                                '=================================================================================================================================================================================================================='
                            Catch ex As Exception
                                'INI PROY-140126
                                Dim MaptPath As String
                                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                                MaptPath = "( Class : " & MaptPath & "; Function: guardarConectado)"
                                objFileLog.Log_WriteLog(pathFile, strArchivo, " - " & "ERROR: " & ex.Message.ToString() & MaptPath)
                                'FIN PROY-140126
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  K_DESLOG :" & K_DESLOG)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  K_NROLOG :" & K_NROLOG)
                                DeshacerCambiosPagos(K_PAGON_IDPAGO)
                                'Session.Add("msgErrorGenerarSot", "Ocurrio un error al tratar de obtener el Correlativo")
                                'Response.Redirect("PoolPagos.aspx")
                                ApperrorGeneral = "Ocurrio un error al tratar de obtener el Correlativo"
                                Exit Function
                            End Try
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Registro de forma correcta el detalle del Pago :" & K_PAGON_IDPAGO)
                        Else
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Ocurrio un error al tratar de registrar el detalle del pedido :" & K_PAGON_IDPAGO)
                            '** RollBack a los Pagos ************************'
                            DeshacerCambiosPagos(K_PAGON_IDPAGO)
                            '*************************************************'
                        End If
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  No registro de forma correcta el Pago :" & K_PAGON_IDPAGO)
                        '** RollBack al registro del pago ************************'
                        DeshacerCambiosPagos(K_PAGON_IDPAGO)
                        '*************************************************'
                    End If
                Catch ex As Exception
                    'Session.Add("msgErrorGenerarSot", "Error Al registrar Pagos")
                    'Response.Redirect("PoolPagos.aspx")
                    ApperrorGeneral = "Error Al registrar Pagos"
                    Exit Function
                End Try

                ''FIN DEL METODO GRABAR TRY
                Dim blnErrorPago As Boolean = False

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Fin del proceso de pagos")

                If K_DESLOG <> "OK" Then
                    strErrorMsg = K_DESLOG
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR : " & strErrorMsg)
                End If



                If Len(Trim(strErrorMsg)) > 0 Then     'se produjo algun error al momento de pagar
                    'Session("strMensajeCaja") = strErrorMsg
                    'Response.Redirect("PoolPagos.aspx")
                    ApperrorGeneral = strErrorMsg
                    Exit Function
                Else
                    '**Inicio Pago de Rentas Adelantadas en Tablas propias de Rentas
                    '*********************************************************************************************************

                    Dim respuesta As Boolean = False

                    Dim listaTipoDoc As DataSet
                    Dim tipoDocumento As String
                    Dim tipoDocIdent As String = ""
                    Dim codigoPago As String
                    Dim cod_pago As String
                    Dim sImporte As String
                    Dim sImporte2 As String
                    'Dim sFechaReg As String
                    'Dim sFecVencimiento As String
                    'Dim sNroDocumento As String
                    Dim sNroAcuerdo As String
                    ' Dim sTipoDocIdentidad As String
                    Dim sCodDocumento As String
                    Dim sDesDocumento As String = ""
                    Dim dsDra As DataSet
                    Dim dsFormaPago As DataSet

                    Dim txtMonto_1 As Double
                    Dim cboTipDocumento_1 As String
                    'Dim Tipo_docume_nuevo As String
                    Dim cboDescTipDocumento_1 As String
                    Dim Numero_Tarjeta As String
                    Dim h As Integer
                    Dim m As Integer

                    '27440 INI
                    'objTxt = CType(Me.FindControl("txtMonto1"), TextBox)
                    'strMonto = "" : strMonto = Trim(objTxt.Text)

                    strMonto = "" : strMonto = Trim(ApptxtMonto1)

                    'objCbo = CType(Me.FindControl("cboTipDocumento1"), DropDownList)
                    'strTipoDocPago = "" : strTipoDocPago = Trim(objCbo.SelectedValue)

                    strTipoDocPago = "" : strTipoDocPago = Trim(AppcboTipDocumento1)

                    cboTipDocumento_1 = strTipoDocPago

                    txtMonto_1 = Funciones.CheckDbl(strMonto)


                    Dim strNumero_Tarjeta = ""
                    'Dim objTxtTarjeta As TextBox
                    'objTxtTarjeta = CType(Me.FindControl("txtDoc1"), TextBox)
                    'strNumero_Tarjeta = "" : strNumero_Tarjeta = Trim(objTxtTarjeta.Text)

                    strNumero_Tarjeta = "" : strNumero_Tarjeta = Trim(ApptxtDoc1)

                    Numero_Tarjeta = strNumero_Tarjeta 'verificar

                    '27440 FIN




                    Dim cod_Rpta As String = ""
                    Dim msg_Rpta As String = ""


                    '****************************Consulta de Datos********************************

                    'Comentado por SD_668541
                    'Try
                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Log para obtener el Formas de Pago de PVU.  Stored Procedure : sisacss_CodPago_TipoDoc ")
                    '    dsFormaPago = objConsultaMsSap.Lista_Formas_Pago_pvu()
                    '    If dsFormaPago.Tables(0).Rows.Count > 0 Then
                    '        For j = 0 To dsFormaPago.Tables(0).Rows.Count - 1
                    '            If cboTipDocumento_1 = dsFormaPago.Tables(0).Rows(j).Item("CODIGO_ANTIGUO_MEDIO").ToString Then
                    '                Tipo_docume_nuevo = dsFormaPago.Tables(0).Rows(j).Item("COD_NUEVO_MEDIO").ToString
                    '                Exit For
                    '            End If
                    '        Next
                    '    End If
                    'Catch ex As Exception
                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception: " & ex.ToString)
                    '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Se realiza el rollback al mòdulo.")
                    '    DeshacerCambiosPagos(K_PAGON_IDPAGO)
                    'End Try

                    Try

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Log para obtener el codigo DRAV.  Stored Procedure : sisacss_CodPago_TipoDoc ")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codigo_Pedido: " & dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO"))

                        respuesta = objConsultaMsSap.obtenerTipoDocumento_y_codigoPago(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO"), tipoDocumento, codigoPago)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "codigoPago: " & codigoPago)

                        cod_pago = codigoPago

                        dsDra = objConsultaMsSap.Consultar_Pago_Dra("", "", cod_pago, cod_Rpta, msg_Rpta)

                        If dsDra.Tables(0).Rows.Count > 0 Then
                            'sImporte2 = dsDra.Tables(1).Rows(0)("DRAN_IMPORTE_DRA")
                            sImporte = Funciones.CheckStr(dsDra.Tables(1).Rows(0)("DRAN_IMPORTE_DRA").ToString)
                            'sImporte=sImporte2.IndexOf(

                            'sNroDocumento = dsDra.Tables(0).Rows(0)("DRAV_DOCUMENTO_CLIENTE").ToString	

                            sNroAcuerdo = dsDra.Tables(1).Rows(0)("DRAV_NRO_DRA").ToString
                            'sTipoDocIdentidad = tipoDocumento
                            'sFechaReg = DateTime.Parse(dsDra.Tables(1).Rows(0)("DRAD_FECHA_EMISION").ToString).ToShortDateString
                            'sFecVencimiento = DateTime.Parse(dsDra.Tables(1).Rows(0)("DRAD_FECHA_VENCIMIENTO").ToString).ToShortDateString
                        Else
                            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception: " & ex.ToString)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Se realiza el rollback al mòdulo.")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " dsDra.Tables(0).Rows.Count : " & Funciones.CheckStr(dsDra.Tables(0).Rows.Count))
                            'DeshacerCambiosPagos(K_PAGON_IDPAGO)
                        End If


                        '*********************Grabar Datos*****************************************
                        Dim DRAV_COD_APLIC As String = ConfigurationSettings.AppSettings("Codigo_Aplicacion_DRA")
                        Dim DRAV_USUARIO_APLIC As String = ConfigurationSettings.AppSettings("Usuario_Aplicacion")
                        Dim DRAC_TIPO_RA As String = ConfigurationSettings.AppSettings("Nombre_Corte_Renta")
                        Dim DRAV_DESCRIPCION_DOC As String = ConfigurationSettings.AppSettings("Descripcion_Renta")
                        Dim DRAV_CODIGO_MONEDA As String = System.Configuration.ConfigurationSettings.AppSettings("MONEDA")
                        Dim DRAD_FECHA_PAGO As String = DateTime.Now.ToString
                        Dim DRAN_TRACE_ID As String = System.Configuration.ConfigurationSettings.AppSettings("DRAN_TRACE_ID")
                        Dim DRAN_NUM_OPE_PAGO As String = ConfigurationSettings.AppSettings("Codigo_Aplicacion_DRA") 'Numero_Tarjeta ' 
                        Dim DRAV_COD_BANCO As String = System.Configuration.ConfigurationSettings.AppSettings("Cod_Banco") '"12450" 'Verificar con papucho
                        Dim DRAV_COD_PDV As String = AppALMACEN
                        'Dim CADENA_MEDIO_PAGO As String = Tipo_docume_nuevo + ";" + DRAV_COD_BANCO + ";" + Numero_Tarjeta + ";" + txtMonto1.Text 'Me.ddlformaPago.SelectedValue + ";" + Me.ddlBanco.SelectedValue + ";" + Me.txtCheque.Text + ";" + importe
                        Dim CADENA_MEDIO_PAGO As String = Tipo_docume_nuevo + ";" + DRAV_COD_BANCO + ";" + Numero_Tarjeta + ";" + sImporte ' validar el importe de sisact

                        Dim ID_OPE_PAGO_SISACT As Int64
                        Dim COD_RPTAA As Int64
                        Dim MSG_RPTAA As String = ""
                        Dim retorno As Boolean = False

                        '*********************Log para Grabar en la tabla *****************************************
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Log para Grabar parametros de entrada al Stored Procedure : SP_CREAR_PAGO_DRA ")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAV_COD_APLIC: " & DRAV_COD_APLIC)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAV_USUARIO_APLIC: " & DRAV_USUARIO_APLIC)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "cod_pago: " & cod_pago)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAC_TIPO_RA: " & DRAC_TIPO_RA)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sNroAcuerdo: " & sNroAcuerdo)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAV_DESCRIPCION_DOC: " & DRAV_DESCRIPCION_DOC)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAV_CODIGO_MONEDA: " & DRAV_CODIGO_MONEDA)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "txtMonto1.Text: " & ApptxtMonto1)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sImporte : " & sImporte)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAD_FECHA_PAGO: " & DRAD_FECHA_PAGO)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAN_TRACE_ID: " & DRAN_TRACE_ID)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAN_NUM_OPE_PAGO: " & DRAN_NUM_OPE_PAGO)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAV_COD_BANCO: " & DRAV_COD_BANCO)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "CADENA_MEDIO_PAGO: " & CADENA_MEDIO_PAGO)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DRAV_COD_PDV: " & DRAV_COD_PDV)


                        retorno = objConsultaMsSap.Crear_Pago_Dra(DRAV_COD_APLIC, DRAV_USUARIO_APLIC, cod_pago, DRAC_TIPO_RA, sNroAcuerdo, DRAV_DESCRIPCION_DOC, _
                                                        DRAV_CODIGO_MONEDA, sImporte, DRAD_FECHA_PAGO, DRAN_TRACE_ID, DRAN_NUM_OPE_PAGO, _
                                                        DRAV_COD_BANCO, DRAV_COD_PDV, CADENA_MEDIO_PAGO, ID_OPE_PAGO_SISACT, COD_RPTAA, MSG_RPTAA)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Parametros de Salida del Stored Procedure : SP_CREAR_PAGO_DRA ")

                        If COD_RPTAA = 0 And retorno Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Parametros de Salida Validos del Stored Procedure : SP_CREAR_PAGO_DRA ")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ID_OPE_PAGO_SISACT: " & ID_OPE_PAGO_SISACT)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "COD_RPTAA: " & COD_RPTAA)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MSG_RPTAA: " & MSG_RPTAA)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "FIN Parametros de Salida Validos del Stored Procedure : SP_CREAR_PAGO_DRA ")


                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Parametros de Entrada AsignarPagoAcuerdosXDocSap PAgo Exitoso ")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in strDocSap: " & strDocSap)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in Referencia: " & sNroAcuerdo)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in mporte: " & sImporte)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in Current user: " & AppCurrentUser)

                            objActiv.AsignarPagoAcuerdosXDocSap(strDocSap, sNroAcuerdo, CheckDbl(sImporte), AppCurrentUser, msgj)


                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "out msg: " & msgj)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " FIN Parametros de Entrada AsignarPagoAcuerdosXDocSap PAgo Exitoso ")

                        Else
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Parametros de Salida No Validos del Stored Procedure : SP_CREAR_PAGO_DRA ")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ID_OPE_PAGO_SISACT: " & ID_OPE_PAGO_SISACT)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "COD_RPTAA: " & COD_RPTAA)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MSG_RPTAA: " & MSG_RPTAA)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Se realiza el rollback al mòdulo.")

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "inicio ROllback Parametros de Entrada AsignarPagoAcuerdosXDocSap PAgo Exitoso ")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in strDocSap: " & strDocSap)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in Referencia: " & "")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in mporte: " & "0")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "in Current user: " & AppCurrentUser)
                            objActiv.AsignarPagoAcuerdosXDocSap(strDocSap, "", 0, AppCurrentUser, msgj)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "out msg: " & msgj)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " FIN ROllback Parametros de Entrada AsignarPagoAcuerdosXDocSap PAgo Exitoso ")

                            'DeshacerCambiosPagos(K_PAGON_IDPAGO)
                            'Exit Sub
                        End If

                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Catch del Stored Procedure : SP_CREAR_PAGO_DRA ")
                        'INI PROY-140126
                        Dim MaptPath As String
                        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                        MaptPath = "( Class : " & MaptPath & "; Function: guardarConectado)"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception: " & ex.ToString & MaptPath)
                        'FIN PROY-140126

                        'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Se realiza el rollback al mòdulo.")
                        'DeshacerCambiosPagos(K_PAGON_IDPAGO)
                        'Exit Sub
                    End Try
                    '**FIN Pago de Rentas Adelantadas en Tablas propias de Rentas
                    '*********************************************************************************************************

                    'objCajas.FP_InsertaEfectivo(Session("ALMACEN"), "60138", dblEfectivo)  'Se inserta el efectivo obtenido
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio FP_InsertaEfectivo.")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp ALMACEN: " & AppALMACEN)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp USUARIO: " & AppUSUARIO)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp dblEfectivo: " & CStr(dblEfectivo))
                    Dim dFecha As Date
                    Dim sFecha As String = Format(Now.Day, "00").ToString.Trim & "/" & Format(Now.Month, "00").ToString.Trim & "/" & Format(Now.Year, "0000").ToString.Trim

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Fecha Pedido : " & sFecha)

                    Try
                        objCajas.FP_InsertaEfectivo(AppALMACEN, AppUSUARIO, dblEfectivo, sFecha)     'Se inserta el efectivo obtenido
                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Entro al Error de insertar efectivo")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Error " & ex.Message.ToString())
                    End Try

                    'objCajas.FP_InsertaEfectivo(Session("ALMACEN"), Session("USUARIO"), dblEfectivo)     'Se inserta el efectivo obtenido
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin FP_InsertaEfectivo.")


                    '***********************************************************************************************************
                    '**INICIO FC***'
                    '*** REGISTRO DE DATOS PARA EL FLUJO DE CAJA ***'
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Registro datos del Flujo Caja")
                    objCajas = New COM_SIC_Cajas.clsCajas
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Seteamos la fecha para la concatenaciòn")
                    Dim cultureName As String = "es-PE"
                    Dim culture As CultureInfo = New CultureInfo(cultureName)
                    Dim dateTimeValue As DateTime
                    dateTimeValue = Convert.ToDateTime(DateTime.Now, culture)
                    Dim vDRAV_NRO_DRA As String = sNroAcuerdo

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "---------------------------------------------------------------------------------------------------------- ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Registro datos del Flujo Caja")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Formato de fechas para FC: yyyymmdd")

                    '''*****fechaCajas = dateTimeValue.ToShortDateString.Substring(6, 4) & dateTimeValue.ToShortDateString.Substring(3, 2) & dateTimeValue.ToShortDateString.Substring(0, 2)
                    'PROY-140397-MCKINSEY -> JSQ INICIO
                    fechaCajas = Format(Now.Year, "0000").ToString.Trim & Format(Now.Month, "00").ToString.Trim & Format(Now.Day, "00").ToString.Trim
                    'PROY-140397-MCKINSEY -> JSQ FIN
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- fechaCajas : " & fechaCajas.ToString)
                    dsDatosPedido = objConsultaMsSap.ConsultaPedido(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO"), "", "")
                    ''''1. Registro de pago: TABLA "TI_VENTAS_FACT"
                    '''*Para las notas de crèdito el monto del saldo enviado debe ser 0.00

                    Dim PEDIC_CLASEFACTURA As String = "" 'valor que sera reemplazado por una key si es una recarga virtual frecuente

                    Try
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio método : RegistrarPago")

                        If dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEPEDIDO") = System.Configuration.ConfigurationSettings.AppSettings("PEDIC_CLASEPEDIDO_RVF") Then
                            PEDIC_CLASEFACTURA = System.Configuration.ConfigurationSettings.AppSettings("DESC_BOLETA_RVF")
                        Else
                            PEDIC_CLASEFACTURA = Funciones.CheckStr(dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"))
                        End If

                        resCajas = objCajas.RegistrarPagoAPK(AppALMACEN, fechaCajas, _
                                                       Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & AppUSUARIO), 10), _
                                                       dsDatosPedido.Tables(0).Rows(0).Item("PEDIV_DESCCLASEFACTURA"), _
                                                       strDocSap, vDRAV_NRO_DRA, _
                                                       Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & dsDatosPedido.Tables(0).Rows(0).Item("VENDEDOR")), 10), _
                                                       System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                                       PEDIC_CLASEFACTURA, _
                                                       dsDatosPedido.Tables(0).Rows(0).Item("DEPEN_NROCUOTA"), _
                                                       dsDatosPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), _
                                                       IIf(dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA") <> ConfigurationSettings.AppSettings("strTipDoc"), dsDatosPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO"), 0), _
                                                       IIf(IsDBNull(dsDatosPedido.Tables(0).Rows(0).Item("PEDIV_NROREFNCND")), "", dsDatosPedido.Tables(0).Rows(0).Item("PEDIV_NROREFNCND")), _
                                                       System.Configuration.ConfigurationSettings.AppSettings("ESTADO_VENTA_FLUJOCAJA"), _
                                                       Funciones.CheckStr(Right(System.Net.Dns.GetHostName, 2)), _
                                                       System.Configuration.ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), _
                                                       "1", P_ID_TI_VENTAS_FACT, P_MSGERR)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin método : RegistrarPago")
                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al tratar de ejecutar el método:  RegistrarPago")
                    End Try

                    If P_ID_TI_VENTAS_FACT > 0 Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Código generado en la tabla TI_VENTAS_FACT=> " & P_ID_TI_VENTAS_FACT.ToString)

                        '''''''''2. Registro RegistrarPagoDetalle
                        If Funciones.CheckStr(dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")) <> ConfigurationSettings.AppSettings("strTipDoc") Then '** Las notas de crèdito no van a formar parte de este proceso **'
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia el proceso para el RegistrarPagoDetalle")
                            For i = 1 To ApphidNumFilas
                                Dim txtMonto As Double
                                Dim cboTipDocumento As String
                                Dim cboDescTipDocumento As String

                                'PROY-27440 INI

                                'objTxt = CType(Me.FindControl("txtMonto1"), TextBox)
                                'strMonto = "" : strMonto = Trim(objTxt.Text)
                                strMonto = "" : strMonto = Trim(ApptxtMonto1)

                                cboTipDocumento = strTipoDocPago
                                txtMonto = Double.Parse(strMonto)
                                cboDescTipDocumento = DevuelteTipoDescPago(cboTipDocumento)
                                resCajas = 0

                                '''''''''''**Registra los medios de pago seleccionado.
                                '''''''''IIf(arrayListaReferencia(i) = "", "", Funciones.CheckStr(arrayListaReferencia(i))), _
                                resCajas = objCajas.RegistrarPagoDetalle(P_ID_TI_VENTAS_FACT, fechaCajas, strDocSap, _
                                                               vDRAV_NRO_DRA, PEDIC_CLASEFACTURA, _
                                                               cboTipDocumento, _
                                                               IIf(arrayListaRefSunat(i) = "", "", Funciones.CheckStr(arrayListaRefSunat(i))), _
                                                               txtMonto, _
                                                               System.Configuration.ConfigurationSettings.AppSettings("ESTADO_VENTA_FLUJOCAJA"), _
                                                               System.Configuration.ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), _
                                                               P_MSGERR)

                            Next
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Termina el proceso para el RegistrarPagoDetalle")
                            '''''''''''''''''''* FIN 2 *'

                            ''''''''''* 3. RegistrarVentaDetalle, consulto el pedido para asegurar la data
                            '''''''''** Registro de los MATERIALES(TI_MATER_FACT):
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia el proceso para el RegistrarVentaDetalle.")
                            If Not dsDatosPedido Is Nothing Then
                                If dsDatosPedido.Tables(1).Rows.Count > 0 Then
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia el proceso para registrar la venta en el detalle.")
                                    resCajas = 0
                                    For k As Integer = 0 To dsDatosPedido.Tables(1).Rows.Count - 1
                                        resCajas = objCajas.RegistrarVentaDetalle(AppALMACEN, fechaCajas, _
                                                                                    Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & AppUSUARIO), 10), _
                                                                                    dsDatosPedido.Tables(0).Rows(0).Item("PEDIV_DESCCLASEFACTURA"), _
                                                                                    strDocSap, vDRAV_NRO_DRA, _
                                                                                    Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & dsDatosPedido.Tables(0).Rows(0).Item("VENDEDOR")), 10), _
                                                                                    dsDatosPedido.Tables(1).Rows(k).Item("DEPEC_CODMATERIAL"), _
                                                                                    dsDatosPedido.Tables(1).Rows(k).Item("DEPEV_DESCMATERIAL"), _
                                                                                    "UN", 1, _
                                                                                    dsDatosPedido.Tables(1).Rows(k).Item("DEPEN_PRECIOIGV"), _
                                                                                    IIf(IsDBNull(dsDatosPedido.Tables(1).Rows(k).Item("SERIC_CODSERIE")), "", dsDatosPedido.Tables(1).Rows(k).Item("SERIC_CODSERIE")), _
                                                                                    IIf(IsDBNull(dsDatosPedido.Tables(1).Rows(k).Item("DEPEV_CODIGOLP")), "", dsDatosPedido.Tables(1).Rows(k).Item("DEPEV_CODIGOLP")), _
                                                                                    IIf(IsDBNull(dsDatosPedido.Tables(1).Rows(k).Item("DEPEV_DESCRIPCIONLP")), "", dsDatosPedido.Tables(1).Rows(k).Item("DEPEV_DESCRIPCIONLP")), _
                                                                                    Funciones.CheckStr(Right(System.Net.Dns.GetHostName, 2)), _
                                                                                    System.Configuration.ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), _
                                                                                    P_MSGERR)
                                    Next
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin el proceso para registrar la venta en el detalle.")
                                    If P_MSGERR <> "" Then
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al registrar la venta detalle.")
                                        DeshacerCambiosPagos(K_PAGON_IDPAGO)
                                        'Response.Write("<script>alert('" & P_MSGERR & "')</script>")
                                        ApperrorGeneral = P_MSGERR
                                        Exit Function
                                    Else
                                        '**********************************************
                                        '*** 4. Registramos RegistrarDetalleCuota   ***'
                                        '*** DETALLA EL NUMERO DE CUOTAS DEL PEDIDO ***'
                                        '**********************************************
                                        flagPagoExito = True
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-{1} => {2}", strIdentifyLog, "flagPagoExito", Funciones.CheckStr(flagPagoExito)))

                                        resCajas = 0
                                        If dsDatosPedido.Tables(0).Rows.Count > 0 Then
                                            '**REGISTRA LAS CUTOAS DEL PEDIDO SI EXISTIERA **'
                                            If dsDatosPedido.Tables(0).Rows(0).Item("DEPEN_NROCUOTA") > 0 Then
                                                '''For c As Integer = 1 To dsDatosPedido.Tables(0).Rows(0).Item("DEPEN_NROCUOTA")
                                                resCajas = objCajas.RegistrarDetalleCuota(P_ID_TI_VENTAS_FACT, fechaCajas, _
                                                                                          Funciones.CheckStr(strDocSap), _
                                                                                          vDRAV_NRO_DRA, dsDatosPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"), _
                                                                                          dsDatosPedido.Tables(0).Rows(0).Item("DEPEN_NROCUOTA"), _
                                                                                          Funciones.CheckDbl(dsDatosPedido.Tables(0).Rows(0).Item("INPAN_TOTALDOCUMENTO")), _
                                                                                          System.Configuration.ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario"), _
                                                                                          P_MSGERR)
                                                ''''Next
                                            End If
                                        End If
                                    End If
                                Else
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "No se encontraron datos al consultar el pedido.")
                                End If
                            Else
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "La consulta no se realizo correctamente.")
                            End If
                        End If  '** FIN IF DE LA NC
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Error devuelto : " & P_MSGERR.ToString)
                        'Response.Write("<script>alert('" & P_MSGERR & "')</script>")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Error, no se realizo el registro en TI_VENTAS_FACT")
                        'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Se realiza el rollback al mòdulo.")
                        'DeshacerCambiosPagos(K_PAGON_IDPAGO)
                        ApperrorGeneral = P_MSGERR
                        Exit Function
                    End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Registro datos del Flujo Caja")
                    '**FIN FC
                    '*********************************************************************************************************

                End If
            End If
            'K_NROLOG, K_DESLOG
        End If

        Return flagPagoExito
    End Function



    Private Sub DeshacerCambiosPagos(ByVal K_PAGON_IDPAGO As Int64)
        '*** PROCESO DE ROLLBACK AL PROCESO DE PAGOS *****************************************'
        If K_PAGON_IDPAGO > 0 Then
            Dim K_NROLOG As String = ""
            Dim K_DESLOG As String = ""
            Dim strIdentifyLog As String = String.Format("--[{0}]--", numeroOperacion)

            objTrsMsSap.DeshacerCambiosPedidoPagado(K_PAGON_IDPAGO, K_NROLOG, K_DESLOG)
            If K_DESLOG = "OK" Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  EL RollBack al pedido :" & K_PAGON_IDPAGO & " se ejecuto con errores.")
                'Response.Redirect("PoolPagos.aspx")
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  EL RollBack al pedido :" & K_PAGON_IDPAGO & " se ejecuto correctamene.")
            End If
        End If
    End Sub

    Public Function DevuelteTipoDescPago(ByVal cboTipDocumento As String) As String


        Dim cboDescTipDocumento As String


        Dim ds_FormaPago As DataSet = CType(AppVias_Pago, DataSet)

        Dim j As Integer = 0
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - " & "Utilizacion de la variable Session: Vias_Pago")

            If ds_FormaPago.Tables(0).Rows.Count > 0 Then
                If Not IsNothing(ds_FormaPago) Then
                    For j = 0 To ds_FormaPago.Tables(0).Rows.Count - 1
                        If (ds_FormaPago.Tables(0).Rows(j)("CCINS") = cboTipDocumento) Then
                            cboDescTipDocumento = Trim(ds_FormaPago.Tables(0).Rows(j)("VTEXT"))
                            Exit For
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - " & "error Utilizacion de la variable Session: Vias_Pago")
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: DevuelveTipoDescPago)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - " & "ERROR: " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

        End Try
        'Session("FormasPAgo") = Nothing

        Return cboDescTipDocumento
        'FPPB

    End Function

    Public Sub RegistrarDetaMedPago(ByVal NroPedido As String, ByVal ArrayMedPago As Array)
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio del registro de Detalle Medio Pago")
            Dim idNumeroPedido As Int64 = Funciones.CheckInt64(NroPedido)

            For index As Integer = 0 To ArrayMedPago.Length - 1
                Dim aux As String = ArrayMedPago(index).split("-")(0)
                Dim idCampo As Int32 = Int32.Parse(aux.Split("_")(0))
                Dim valor As String = aux.Split("_")(1)
                objTrsMsSap.RegistrarDetMedPago(idNumeroPedido, idCampo, valor)
            Next

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin del registro de Detalle Medio Pago")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - " & "error en el registro de Detalle Medio Pago")
        End Try
    End Sub

End Class
