Public Class clsContantes_site

    'Constante del Site ITGestion'
    '(Codigo de Aplicacion)'
    Public Const codAplicacion = 102

    '(Directorio Virtual)'
    'Public Const strRutaSite = "http://sa8pvu/PVU"
    Public Const strRutaSite = "http://tim-it2desar/sistemacajas/siscajas"

    ''Mantenimiento Cliente
    Public Const strOptMantCliente = 3338

    ''Ventas
    Public Const strOptVentas = 3339

    ''Devolucion
    Public Const strOptDevolucion = 3340

    ''Devolucion por Documento de Identidad
    Public Const strOptDevolPorDI = 3345

    ''Devolucion por Dcoumento de Venta
    Public Const strOptDevolPorDV = 3346

    ''Venta Rapida
    Public Const strOptVentaRapido = 3341

    ''Venta SAP Tiendas
    Public Const strOptVentaSapT = 3557


    ''Pagos
    Public Const strOptPagos = 3342

    ''Documento por Pagar
    Public Const strOptDocPorPagar = 3347

    ''Consulta de Pagos
    Public Const strOptConsultaPago = 3349

    ''Historico de Pagos
    Public Const strOptHistoricoPago = 3350

    ''Cuadre Caja
    Public Const strOptCuadreCaja = 3348

    ''Acuerdo PCS
    Public Const strOptAcuerdoPCS = 3343

    ''Acuerdo Activacion
    Public Const strOptAcuerdoActiv = 3351

    ''Acuerdo Consulta
    Public Const strOptAcuerdoConsulta = 3352

    ''Acuerdo Reporte Hist
    Public Const strOptAcuerdoReporteH = 3353

    ''Consulta Reporte
    Public Const strOptConsultaReporte = 3344

    ''Avance Ventas
    Public Const strOptAvanceVentas = 3354

    ''Consulta Inventario
    Public Const strOptConsultaInv = 3355

    ''Consulta Equipo
    Public Const strOptConsultaEquipo = 3356

    ''SUNAT
    Public Const strOptSUNAT = 3365

    ''Anulación Preventiva
    Public Const strOptAnulPreven = 3366

    ''Anulación para Reutil
    Public Const strOptAnulReutil = 3367

    ''Opciones Detallista
    ' Liquidacion diaria
    Public Const strOptLiqDiaria = 4088

    ' mantenimiento de Clientes
    Public Const strOptMantClientes = 4089

    ' mantenimientos de tipo de cliente
    Public Const strOptMantTipCli = 4090

    ' mantenimiento de motivos
    Public Const strOptMantMotivos = 4091

    ' mantenimiento de Vendedores
    Public Const strOptMantVend = 4092

    ' mantenimientos de Zonas
    Public Const strOptMantZonas = 4093

    ' mantenimiento de Stocks
    Public Const strOptMantStock = 4097

    ' mantenimientos de cartera de cliente
    Public Const strOptCartCli = 4155

    ' mantenimiento de lista de precios
    Public Const strOptLisPre = 4170

    ' Asignacion de Tarjetas
    Public Const strOptAsigTar = 4087


    'Constante para ocultar las capas en el menú izquierdo'
    'Public Const strSiteOcultarCapas="MM_showHideLayers('Solicitudes','','hide','CapaOculta','','hide','Proyectos','','hide','Auditoria','','hide')"
    Public Const strSiteOcultarCapas = ""

    '************************************
    '	EVENTOS DE RECARGA DETALLISTA
    '************************************
    Public Const strAdicTipoCli = 2839   'Adicionar tipo de cliente
    Public Const strAdicZonaVen = 2840   'Adicionar Zona de Venta
    Public Const strAdicVendedor = 2841  'Adicionar Vendendor 
    Public Const strAdicMotivos = 2842   ' Adicionar Motivos
    Public Const strAdicStocks = 2843    ' Adicinar Stocks
    Public Const strAdicListaPre = 2844  ' Adicionar Lista de Precios
    Public Const strAdicRegCob = 2845    ' Adicionar Registro de Cobranza
    Public Const strAdicRegFal = 2846    ' Adicionar Registros de tarjetas faltantes
    Public Const strAdicRegDev = 2847    ' Adicionar Registro de Devoluciones

    Public Const strModCliente = 2848    ' Modificar Cliente
    Public Const strModVenta = 2849      ' Modificar Venta
    Public Const strModTipoCli = 2850    ' Modificar Tipo de CLiente
    Public Const strModZonaVen = 2851    ' Modificar Zona de Venta
    Public Const strModVendedor = 2852   ' Modificar Vendedor
    Public Const strModMotivos = 2853    ' Modficar Motivos
    Public Const strModListaPre = 2854   ' Modificar Lista de Precios
    Public Const strModRegFal = 2855     ' Modificar Registro de Faltantes

    Public Const strAsigTarje = 2856     ' Asignacion de Tarjetas


    Public Const strBusqClien = 2964    ' Busqueda de cliente
    Public Const strAdicObj = 2965      ' Añadir objetivos
    Public Const strModObj = 2966       ' Modificar objetivos  
    Public Const strElmMotivos = 2967   ' Eliminar motivos
    Public Const strAdicDocs = 2968     ' Adicionar Documentos de cobro
    Public Const strAdicVenta = 2969    ' Adicionar Ventas
    Public Const strAdicCliente = 2970  ' Adicionar Cliente
    Public Const strElmTipoCli = 2971   ' Eliminar Tipo de Cliente

    Public Const strEvtCartCli = 3003      ' Cartera de Clientes
    Public Const strEvtGenLiq = 3002       ' Genrerar Liquidacion


    '************************************
    '	EVENTOS DEL APLICATIVO
    '************************************
    ''Busqueda de Cliente
    Public Const strEvtBuscarCliente = 2015

    ''Historico Cliente
    Public Const strEvtHistoricoCliente = 2016

    ''Devolucion por Documento de Identidad
    Public Const strEvtDevolPorDI = 2017

    ''Devolucion por Documento de Venta
    Public Const strEvtDevolPorDV = 2018

    ''Documento por Pagar
    Public Const strEvtDocPorPagar = 2019

    ''Cuadre Caja
    Public Const strEvtCuadreCaja = 2020

    ''Consulta Pago
    Public Const strEvtConsultaPago = 2021

    ''Consulta Activacion
    Public Const strEvtConsultaAct = 2022

    ''Reporte Historico
    Public Const strEvtReporteHist = 2023

    ''Avance Venta
    Public Const strEvtAvanceVenta = 2024

    ''Consulta Inventario
    Public Const strEvtConsultaInv = 2025

    ''Consulta Equipo
    Public Const strEvtConsultaEquipo = 2026

    ''Mantenimiento Cliente
    Public Const strEvtMantenimientoCliente = 2027

    ''Venta
    Public Const strEvtVenta = 2028

    ''Venta Rapida
    Public Const strEvtVentaRapida = 2031

    ''Activacion Acuerdo
    Public Const strEvtActivaciones = 2029

    ''Anulacion Preventiva
    Public Const strEvtPrevent = 2038

    ''Anulacion para Reutil
    Public Const strEvtReutil = 2039

    ''Modificacion Cliente
    Public Const strEvtModificarCliente = 2030

    '' Grabar Detalle Pago
    Public Const strEvtGrabarDetPago = 3004

    '' Compensar Documento
    Public Const strEvtCompensarDoc = 3005

    '' Guardar numero de referencia SUNAT
    Public Const strEvtGuardNumSUNAT = 3006

    '' Anular Documento de Pago
    Public Const strEvtAnularDocPago = 3007

    '' Anular Pago
    Public Const strEvtAnularPago = 3008

    '' Anular Compensacion
    Public Const strEvtAnularCompen = 3009

    '************************************
    '	Tipo de Ventas
    '************************************
    ''Venta Postpago
    Public Const strTVPostpago = "01"

    ''Venta Prepago
    Public Const strTVPrepago = "02"

    ''Venta Recarga
    Public Const strTVRecarga = "03"

    ''Venta Control
    Public Const strTVControl = "04"

    ''Venta Todos los Tipos
    Public Const strTVTodos = "00"

    '************************************
    '	Clase de Venta
    '************************************
    ''Venta Postpago
    Public Const strDTVAlta = "01"

    ''Venta Prepago
    Public Const strDTVMigracion = "02"

    ''Venta Recarga
    Public Const strDTVReposicionDOA = "03"

    ''Venta Control
    Public Const strDTVRenovacion = "04"

    ''Venta Postpago
    Public Const strDTVFidelizacion = "05"

    ''Venta Prepago
    Public Const strDTVReposicionCbr = "06"

    ''Venta Recarga
    Public Const strDTVReposicionCC = "07"

    ''Venta Control
    Public Const strDTVOtros = "08"

    ''Venta Reposicion
    Public Const strDTVReposicionChip = "11"

    '---------  Fondo de Tablas  ---------

    'Color del Menu de Opciones Izquierdo
    Public Const strBgMenu = "#FF4900"

    'Background del Menu de Opciones
    Public Const strBckMenu = "../images/fondos/fnd_MenuDespleg.gif"

    'Color del Borde de la Tabla Resultados
    Public Const strBrColorResult = "#9CAACC"

    'Color de Filas de Tabla de Resultados
    Public Const strColorResult = "#ECF0F9"

    'Color de Filas de Tabla de Resultados DESHABILITADO
    Public Const strColorDes = "#F2F2F4"

    'Color del Borde de las Celdas de Subtitulos de los Resultados
    Public Const strBrColorSubTitResult = "#9CAACC"

    'Configuracion del Top.htm
    Dim strFromMailApp

    'Public Const Const_IGV = 0.19 

    Public Const strTipoDNI = "1"

    'Busqueda Reniec
    Public Const strOptReniec = 3378

    'Busqueda por Aproximación
    Public Const strEvtBuscarAproximacion = 2157

    'Busqueda por Nro DNI
    Public Const strEvtBuscarDNI = 2158

    'Busqueda Detalle por Aproximacion
    Public Const strEvtDetAproximacion = 2159

    'Asignar Tope
    Public Const strOptPVU_AST = 3564

    'Asignar Topes de Usuario
    Public Const strEvtPVU_ATOP = 2209

    'Modificar Topes de Usuario
    Public Const strEvtPVU_MTOP = 2210

    'Eliminar Asignación de Usuario
    Public Const strEvtPVU_ETOP = 2211

    'Consulta Resumen Tope
    Public Const strOptPVU_CRT = 3564

    'Recarga Virtual
    Public Const OficRV = "S029"

    'Resumen de Topes
    Public Const strOptPVU_RET = 3024

    'Movimientos de Vendedor
    Public Const strOptPVU_MVE = 3025

    'INFOCORP - Inicio
    Public Const strOptInfocorp = "3726"

    'INFOCORP - Informe Respuesta
    Public Const strEvtInfrespuesta = "2315"

    'INFOCORP - Informe Crediticio
    Public Const strEvtInfCrediticio = "2316"

    'INFOCORP - Informe Historico
    Public Const strEvtInfHistorico = "2317"

    'INFOCORP - Datos Consulta
    Public Const strEvtDatosConsulta = "2318"

    'INFOCORP - Tipo de Cambio
    Public Const strTipoCambio = "3.50"

    'INFOCORP - Region
    Public Const strRegion = "REG"

    Public Const Ofic1 = "0009"
    Public Const Ofic2 = "0006"
    'Public Const Ofic2 ="XXXX"
    Public Const Ofic3 = "0008"
    Public Const Ofic4 = "0007"
    Public Const Ofic5 = "0011"
    Public Const Ofic6 = "0097"
    Public Const Ofic7 = "0013"
    Public Const Ofic8 = "0017"
    Public Const Ofic9 = "FR46" '-FR13
    Public Const Ofic10 = "FR35" 'FR12
    Public Const Ofic11 = "FR62" '-FR13
    Public Const Ofic12 = "FR60" 'FR12
    Public Const Ofic13 = "FR32" '-FR13
    Public Const Ofic14 = "FR47" '--FR14
    Public Const Ofic15 = "FR45" 'FR12
    Public Const Ofic16 = "FR48" '--FR14
    Public Const Ofic17 = "FR50" '--FR14
    Public Const Ofic18 = "FR33" 'FR12
    Public Const Ofic19 = "FR19" '--FR14
    Public Const Ofic20 = "FR34" '--FR14
    Public Const Ofic21 = "FR51" '-FR13

    Public Const Ofic22 = "RPPR"
    Public Const Ofic23 = "S023"
    Public Const Ofic24 = "S024"
    Public Const Ofic25 = "S025"
    Public Const Ofic26 = "S026"
    Public Const Ofic27 = "S027"
    Public Const Ofic28 = "S029"
    Public Const Ofic29 = "S030"
    Public Const Ofic30 = "S031"
    Public Const Ofic31 = "S032"
    Public Const Ofic32 = "S033"

    'DCOM
    'Public Const Ofic33="0006"
    Public Const Ofic33 = "XXXX"

    Public Const Ofic34 = "FR12" '----FR15	'Franquicia Tarapoto
    Public Const Ofic35 = "FR13" '----FR15	'Franquicia Pucallpa
    Public Const Ofic36 = "FR14" '-----FR16	'Franquicia Ilo
    Public Const Ofic37 = "FR15" '-----FR16	'Franquicia Tacna
    Public Const Ofic38 = "FR16" '-----FR16	'Franquicia Juliaca
    Public Const Ofic39 = "FR17" '*****FR16'Franquicia Arequipa
    Public Const Ofic40 = "FR18" '----FR15	'Franquicia Tumbes
    Public Const Ofic41 = "FR20" '-----FR16	'Franquicia Puno
    Public Const Ofic42 = "FR23" '-----FR16	'Franquicia Cusco
    Public Const Ofic43 = "FR24" '-----FR16	'Franquicia Abancay
    Public Const Ofic44 = "FR25" '----FR15	'Franquicia Chachapoyas
    Public Const Ofic45 = "FR26" '-----FR16	'Franquicia Pto. Maldonado
    Public Const Ofic46 = "FR27" '-----FR16	'Franquicia Huancavelica
    Public Const Ofic47 = "FR28" '----FR15	'Franquicia Ayacucho
    Public Const Ofic48 = "FR29" '-----FR16	'Franquicia Huanuco
    Public Const Ofic49 = "FR30" '-----FR16	'Franquicia Cerro de Pasco
    Public Const Ofic50 = "FR31" '----FR15	'Franquicia Huaraz
    Public Const Ofic51 = "FR37" '----FR15	'Franquicia Huancayo
    Public Const Ofic52 = "FR38" '----FR15	'Franquicia Cajamarca
    Public Const Ofic53 = "FR39" '----FR15	'Franquicia Iquitos
    Public Const Ofic54 = "FR40" '----FR15	'Franquicia Chiclayo
    Public Const Ofic55 = "FR41" '----FR15	'Franquicia Trujillo
    Public Const Ofic56 = "FR42" '----FR15	'Franquicia Chimbote
    Public Const Ofic57 = "FR43" '-----FR16	'Franquicia Ica
    Public Const Ofic58 = "FR44" '----FR15	'Franquicia Piura
    Public Const Ofic59 = "FR52" '-----FR16	'Franquicia Moquegua
    Public Const Ofic60 = "FR53" '----FR15	'Franquicia Chiclayo2
    Public Const Ofic61 = "FR54" '-----FR16	'Franquicia Cusco2
    Public Const Ofic62 = "FR55" '----FR15	'Franquicia Trujillo2
    Public Const Ofic63 = "FR56" '----FR15	'Franquicia Piura2
    Public Const Ofic64 = "FR57" '-----FR16	'Franquicia Tacna2
    Public Const Ofic65 = "FR58" '-----FR16	'Franquicia Puno2
    Public Const Ofic66 = "FR59" '-----FR16	'Franquicia Juliaca2
    Public Const Ofic67 = "FR61" '----FR15	'Franquicia Iquitos2
    Public Const Ofic68 = "FR63" '-----FR16	'Dealer Suc Ica2
    Public Const Ofic69 = "FR64" '----FR15	'Dealer Cajamarca2
    Public Const Ofic70 = "FR65" '-----FR16	'Dealer Arequipa
    Public Const Ofic71 = "FR66" '----FR15	'Dealer Suc Huancayo2
    Public Const Ofic72 = "FR68" '----FR15	'Dealer Trujillo3

    Public Const Ofic73 = "FR67" '-FR13
    Public Const Ofic74 = "FR69" '---FR15

    'ELIMINAR ESTA CONSTANTE
    Public Const Ofic75 = "0010" '---0010
    '------------------------->
    '===============================================================================
    '20040908:
    'Public Const gStrUrlSvr = "http://172.19.32.70:7040/wliAppWebv1/processes/"
    'Public Const gStrUrlSvr = "http://172.19.32.70:7011/wliAppWebv1/processes/"
    'Public Const gStrUrlSvr = "http://172.19.32.51:8001/wliAppWebv1/processes/"
    'Public Const gStrUrlSvr = "http://172.19.32.70:7021/wliAppWebv1/processes/"

    ' se debe tomar: http://172.19.32.70:7040/wliAppWebv1/processes/
    'Public Const gStrUrlSvr = "http://172.19.32.70:7021/wliAppWebv1/processes/;http://172.19.32.70:7041/wliAppWebv1/processes/;http://172.19.32.71:7042/wliAppWebv1/processes/;http://172.19.32.71:7044/wliAppWebv1/processes/"
    'Public Const gStrUrlSvr = "http://172.19.32.70:7021/wliAppWebv1/processes/"
    Public Const gStrUrlSvr = "http://172.19.26.71:8001/wliAppWebv1/processes/"

    'pvuServiciosDemo.jws
    ' Aquí se deberá colocar las Oficinas separadas con ";" a las que se les
    ' desea activar el llamado al WebService
    Public Const gStrLstOfiActivacion = "0006;"
    '
    '20040914:
    Public Const gStrServicioActivacionWS = "S_PVU_ACTIVACIONES"
    Public Const gStrUsuarioActivacionWS = "USRSAPOSC"
    Public Const gStrUserWS = ""
    Public Const gStrPasswordWS = ""



    '20041027:
    Public Const gStrNroAprobActivacion = "2004;2005;2006;"

    'para buscar carpetas observadas
    'Public Const gStrUrlSvrCO = "http://172.19.32.51:8001/wliAppWebv1/webservices/"   'para produccion
    'Public Const gStrUrlSvrCO = "http://172.19.32.70:7021/wliAppWebv1/webservices/"
    'Public Const gStrUrlSvrCO = "http://172.19.32.49:8001/wliAppWebv1/webservices/"
    Public Const gStrUrlSvrCO = "http://172.19.32.71:8001/wliAppWebv1/webservices/"

    Public Const gStrServicioCO = "carpObsvWs.jws"
    Public Const gStrUsuarioCO = "USRSAPOSC"


    '===============================================================================


    '*******************************************************************************
    ' GESTIÓN DE CARPETAS
    '*******************************************************************************
    '20040915:

    ''Busqueda de Carpetas
    Public Const strOptBuscarCarpetas = 3728

    ''Busqueda de Notas de Pedido
    Public Const strOptBuscarNotaPedido = 3729

    ''Obtener datos del Documento
    Public Const strOptObtenerDocumento = 3730

    ''Grabar Acuerdo y Revision
    Public Const strOptGuardarAcuerdo = 3731

    ''Grabar Nota de Pedido y Revision
    Public Const strOptGuardarNPedido = 3732

    ''Grabar Documento y Revision
    Public Const strOptGuardarDocumento = 3733

    '==============================================
    ''Grabar Documento y Revision
    Public Const strOptEjecutarDTS = 3734
    ''Grabar Documento y Revision
    Public Const strOptProcesarBD = 3735
    '==============================================


    ''Buscar de Carpetas
    Public Const strEvtBuscarCarpetas = 2309

    ''Buscar de Notas de Pedido
    Public Const strEvtBuscarNotaPedido = 2310

    ''Obtener datos del Documento
    Public Const strEvtObtenerDocumento = 2311

    ''Grabar Acuerdo y Revision
    Public Const strEvtGuardarAcuerdo = 2312

    ''Grabar Nota de Pedido y Revision
    Public Const strEvtGuardarNPedido = 2313

    ''Grabar Documento y Revision
    Public Const strEvtGuardarDocumento = 2314

    'GCarpetas - Crear Resultado
    Public Const strEvtCrearResultado = 3100

    'GCarpetas - Modificar Resultado
    Public Const strEvtModificarResultado = 3101

    'GCarpetas - Eliminar Resultado
    Public Const strEvtEliminarResultado = 3102

    'GCarpetas - Buscar Resultado
    Public Const strEvtBuscarResultado = 3103

    'GCarpetas - Crear Tipificación
    Public Const strEvtCrearTipificacion = 3104

    'GCarpetas - Modificar Tipificación
    Public Const strEvtModificarTipificacion = 3105

    'GCarpetas - Eliminar Tipificación
    Public Const strEvtEliminarTipificacion = 3106

    'GCarpetas - Buscar Tipificación
    Public Const strEvtBuscarTipificacion = 3107

    'GCarpetas - Crear Detalle
    Public Const strEvtCrearDetalle = 3108

    'GCarpetas - Modificar Detalle
    Public Const strEvtModificarDetalle = 3109

    'GCarpetas - Eliminar Detalle
    Public Const strEvtEliminarDetalle = 3110

    'GCarpetas - Buscar Detalle
    Public Const strEvtBuscarDetalle = 3111

    'GCarpetas - Crear Posible Solución
    Public Const strEvtCrearPossol = 3112

    'GCarpetas - Modificar Posible Solución
    Public Const strEvtModificarPossol = 3113

    'GCarpetas - Eliminar Posible Solución
    Public Const strEvtEliminarPossol = 3114

    'GCarpetas - Buscar Posible Solución
    Public Const strEvtBuscarPossol = 3115

    'GCarpetas - Busqueda de Carpetas - Cargos
    Public Const strEvtBusquedaCarpetas = 0

    'GCarpetas - Creacion de Cargo  
    Public Const strEvtCreaCargos = 0

    'GCarpetas - Modificacion de Cargo
    Public Const strEvtModCargos = 0

    'GCarpetas - Anulacion de Cargo
    Public Const strEvtAnulCargos = 0

    'GCarpetas - Impresion de Cargo
    Public Const strEvtImpreCargos = 0

    'GCarpetas - Creacion de Filtro
    Public Const strEvtCreaFiltro = 0

    'GCarpetas - Modificacion de Filtro
    Public Const strEvtModFiltro = 0

    'GCarpetas - Eliminacion de Filtro
    Public Const strEvtElimFiltro = 0

    'GCarpetas - Busqueda de Carpetas segun Estado
    Public Const strEvtBusquedaCarpEst = 0

    'GCarpetas - Resultados Obtenidos
    Public Const strEvtResCarpEst = 0

    'GCarpetas - Ningun Resultado Obtenido
    Public Const strEvtNoResCarpEst = 0

    'GCarpetas - Opcion Crear Cargos
    Public Const strOptCreaCargos = 5498

    'GCarpetas - Opcion Modificar Cargos
    Public Const strOptModCargos = 5499

    'GCarpetas - Opcion Imprimir Cargos
    Public Const strOptImpCargos = 5501

    'GCarpetas - Opcion Filtros de carga
    Public Const strOptMantFiltCarga = 5497

    'GCarpetas - Opcion Consulta carpetas a devolver
    Public Const strOptConsCarpDev = 6093

    '-----------------------------------------------------
    'GCarpetas - Consulta de Ventas Irregulares
    Public Const strOptConVenIrreg = 0
    Public Const strEvtConVenIrreg = 0

    'GCarpetas - Visualizar Datos de Venta Grabados en PVU  
    Public Const strOptDatVenGraPVU = 0
    Public Const strEvtDatVenGraPVU = 0

    'GCarpetas - Opcion Servicios Adicionales
    Public Const strOptServAdicionales = 0

    'GCarpetas - Documento de Sustento
    Public Const strOptDocSustento = 0

    'GCarpetas - Formatos Adjuntos
    Public Const strOptFormAdjunto = 0

    'GCarpetas - Modificar Sustento
    Public Const strEvtModificarSust = 0

    'GCarpetas - Agregar TipoDocumento
    Public Const strEvtAgregarTipoDoc = 0

    'GCarpetas - Eliminar TipoDocumento
    Public Const strEvtEliminarTipoDoc = 0

    'GCarpetas - Eliminar DocSustento
    Public Const strEvtEliminarDocSust = 0

    'GCarpetas - Agregar DocSustento
    Public Const strEvtAgregarDocSust = 0

    'GCarpetas - ModificarFormAdj
    Public Const strEvtModificarFormAdj = 0

    'GCarpetas - Agregar Acciones
    Public Const strEvtAgregarAcciones = 0

    'GCarpetas - Eliminar Acciones
    Public Const strEvtEliminarAcc = 0

    'GCarpetas - Eliminar FormAdj
    Public Const strEvtEliminarFormAdj = 0

    'GCarpetas - Agregar FormAdj
    Public Const strEvtAgregarFormAdj = 0

    'GCarpetas - Modificar Acciones
    Public Const strEvtModificarAcc = 0

    'GCarpetas - Agregar ServiciosAdicionales
    Public Const strEvtAgregarServAdic = 0

    'GCarpetas - Modificar ServiciosAdicionales
    Public Const strEvtModificarServAdic = 0

    'GCarpetas - Eliminar ServiciosAdicionales
    Public Const strEvtEliminarServAdic = 0

    'GCarpetas - Agregar DocSustentoTipoDoc
    Public Const strEvtAgregarDocSusTipoDoc = 0

    'GCarpetas - Agregar FormatoAdjuntoAccion
    Public Const strEvtAgregarFormAdjAcc = 0
    '-----------------------------------------------------



    'RECEPCION CARPETAS ==========================
    Public Const strOptRecepCarpeta = 7700     'GCarpetas - Opcion revision carpetas
    Public Const strEvtBuscarRecepCarpeta = 7701   'GCarpetas - Evento buscar carpeta
    Public Const strEvtRecibirRecepCarpeta = 7702  'GCarpetas - Evento recibir carpeta
    '=============================================

    'REVISION CARPETAS===========================
    Public Const strOptRevCarpeta = 7703     'GCarpetas - Opcion revision carpetas
    Public Const strEvtBuscarRevCarpeta = 7704    'GCarpetas - Evento buscar carpeta
    Public Const strEvtAsignarRevCarpeta = 7705    'GCarpetas - Evento asignar carpeta
    Public Const strEvtPendienteRevCarpeta = 7706  'GCarpetas - Evento pendiente carpeta
    Public Const strEvtServicioRevCarpeta = 7707   'GCarpetas - Evento servicios carpeta
    Public Const strEvtGrabarRevCarpeta = 7708    'GCarpetas - Evento grabar carpeta
    '=============================================

    'TIEMPOS DE DEVOLUCION =======================
    Public Const strOptTiemposDev = 7709       'GCarpetas - Opcion tiempos de devolucion

    Public Const strEvtNuevoTiemposDev = 7710     'GCarpetas - Evento nuevo tiempos de devolucion
    Public Const strEvtEliminarTiemposDev = 7711  'GCarpetas - Evento eliminar tiempos de devolucion

    Public Const strEvtAnadirOfiRegTiemposDev = 7712   'GCarpetas - Evento anadir oficina-region tiempos de devolucion
    Public Const strEvtEliminarOfiRegTiemposDev = 7713 'GCarpetas - Evento eliminar oficina-region tiempos de devolucion
    Public Const strEvtBuscarOfiRegTiemposDev = 7714   'GCarpetas - Evento buscar oficina-region tiempos de devolucion
    '=============================================


    '==============================================
    ''Ejecutar DTS
    Public Const strEvtEjecutarDTS = 2315
    ''Procesar Datos en BD
    Public Const strEvtProcesarBD = 2316
    '==============================================



    Public Const gStrCodPerfilGC = 2 ' Perfil de GCARPETAS
    Public Const gStrEsquemaGC = "USRBDGCARPETAS"


    '20041115:  Nuevas constantes para GCarpetas
    'Percy Silva
    Public Const strOptRepVtaIrreg = 3736
    Public Const strEvtRepVtaIrreg = 3737
    Public Const strOptRepVtaNoRev = 3738
    Public Const strEvtRepVtaNoRev = 3739

    '20050920: Constantes para reportes 
    '? Debe ser actualizado por los números definidos 
    Public Const strOptRepVtaCominyNoComin = 3740
    Public Const strEvtRepVtaCominyNoComin = 3741
    Public Const strOptRepProductxVerif = 3742
    Public Const strEvtRepProductxVerif = 3743
    Public Const strOptRepDevxPtoVenta = 3744
    Public Const strEvtRepDevxPtoVenta = 3745
    Public Const strOptRepAvancxPtoVenta = 3746
    Public Const strEvtRepAvancxPtoVenta = 3747

    '*******************************************************************************

    '===============================================================================
    ' Prueba en Desarrollo: Uso de Contenedor de Variables
    '===============================================================================
    Public gObjCOMContenedor
    '===============================================================================


    '===============================================================================
    ' 20040921: Constante para evitar modificación de Dirección de Cliente.
    '===============================================================================
    Public Const gStrSAP_KTOKD_ZNJC = "ZNJC"
    '===============================================================================

    '===============================================================================
    ' 20041019: Constante para Upload de Archivos hacia el FileServer:
    '===============================================================================
    'Public Const  k_gStrRutaUploadFile = "\\FileServer1\IntraDoc\Solicitudes\Archivos\"
    Public Const k_gStrRutaUploadFile = "/pvu_x/paginas/GCarpetas/upload"

    '===============================================================================

    '===============================================================================
    ' 20050308: Constante de multiplicación de 'Otros Ingresos' para tipo de
    '			documento de sustentación 'Recibo de Competencia'.
    '===============================================================================
    Public Const K_MULTI_REC_COMP = 12
    '===============================================================================

    Public Const EVENT_INFOCORP = 2864



    Public Const RENOV_OBS = "EL PLAN CONTRATADO EN EL NUEVO ACUERDO SUSCRITO POR EL CLIENTE REEMPLAZA CUALQUIER PLAN TARIFARIO EXISTENCIA EN LA LINEA TELEFONICA MATERIA DEL PRESENTE CONTRATO, ACEPTANDO LAS CONDICIONES DEL PLAN TARIFARIO NUEVO QUE ESTA CONTRATANDO EN EL PRESENTE ACUERDO Y SOBRE EL CUAL CONOCE LAS CARACTERISTICA. EN UN PERIODO MÁXIMO DE 48 HORAS, SE PROCEDERA CON LA ACTUALIZACIÓN DEL PLAN TARITARIO NUEVO MATERIA DE ESTE ACUERDO."
    Public Const ConstAlt = 150
    Public Const ConstAltMeta = 110



    Public Const gstrMensajeActGIPREP = "No se puede realizar el cambio de chip en línea, por favor comunicarse con el 123 para que realicen el cambio"
    Public Const gstrOKAnulCHIPREP = "Se pudo realizar la solicitud de cambio de chip en linea"
    Public Const gstrErrAnulCHIPREP = "No se puede realizar la anulacion del documento por tener una activación en linea"
    Public Const gstrEstSolicNuevo = "N"
    Public Const gstrEstSolicVariado = "V"
    Public Const gstrEstSolicAnulado = "A"
    Public Const gstrErrDatIncon = "-100"
    Public Const gstrErrInst = "1"
    Public Const gstrErrAct = "2"
    Public Const gstrErrInact = "3"
    Public Const gstrErrDesact = "4"
    Public Const gstrErrExpi = "5"
    Public Const gstrErrBloq = "8"
    Public Const gstrMsgErrBloq = "El teléfono no se encuentra bloqueado"
    Public Const gstrMsgErrExpi = "El número telefónico no está habilitado para hacer cambio de chip"
    Public Const gstrCHIPREPPREPAGO = "PREPAGO"
    Public Const gstrCHIPREPCONTROL = "CONTROL"
    Public Const gstrOficinaInhab = "La oficina de ventas no tiene habilitado el servicio de cambio de chip automático."
    Public Const gStrUrlSvrPCR = "http://172.19.26.71:8001/wliAppWebv1/webservices/"
    Public Const gStrServicioPCR = "tuxedoWs.jws"
    Public Const gStrUsuarioPCR = "T10768"




    ' =====================Opciones de SOLCRED ===========================================
    Public Const strOptNuevaSolPDV = 4105  ' Nueva Solicitud PDV
    Public Const strOptNuevaSolCRE = 4106  ' Nueva Solicitud CRE
    Public Const strOptConSolPDV = 4107  ' Consulta Solicitud  PDV
    Public Const strOptConSolCre = 4108  ' Consulta Solicitud Créditos
    Public Const strOptSolEvalSup = 4109  ' Solicitudes de Evaluación Supervisores
    Public Const strOptSolEvalCre = 4110  ' Solicitudes de Evaluación Créditos
    Public Const strOptConActiv = 4111  ' Consulta de Activaciones
    Public Const strOptRepCre = 4112  ' Reporte de Créditos
    Public Const strOptRepTie = 4113  ' Reporte de Tiempos
    Public Const strOptConLog = 4114  ' Consulta de Logs
    Public Const strOptManMotRech = 4115 ' Mantenimiento de Motivos de rechazo

    Public Const strMsgPlanTarifario = "No pueden elegir Planes Tarifarios mayores al aprobado en Evaluación Crediticia"  ' Mensaje para validacion de Plan Tarifario

    ' ====================================================================================
    '=====================Eventos SolCRED ================================================
    Public Const strEvtBusCliente = 3801 'Búsqueda de Cliente
    Public Const strEvtCreCliente = 3802 'Crear Cliente
    Public Const strEvtActCliente = 3803 'Actualizar Cliente
    Public Const strEvtRegRegSolPvta = 3804 'Registro de Solicitud Punto de Venta
    Public Const strEvtRegRegsolCre = 3805 'Registro de Solicitud Créditos
    Public Const strEvtBusSolicit = 3806 'Búsqueda de Solicitudes
    Public Const strEvtModSolicit = 3807 'Modificación de Solicitud 
    Public Const strEvtRegNueSol = 3808 'Registro de nueva solicitud 
    Public Const strEvtConSolPvta = 3809 'Consulta de Solicitud Puntos de Venta
    Public Const strEvtConSolCre = 3810 'Consulta de Solicitud Créditos
    Public Const strEvtConPoolSolEvalSup = 3811 'Consulta Pool Solicitud Evaluación Supervisores
    Public Const strEvtConPoolSolEvalCre = 3812 'Consulta Pool Solicitud Evaluación Créditos
    Public Const strEvtConActiv = 3813 'Consulta de Activaciones
    Public Const strEvtEmiRepCred = 3814 'Emisión de Reporte de Créditos
    Public Const strEvtEmiRepTiem = 3815 'Emisión de Reporte de Tiempos
    Public Const strEvtConLog = 3816 'Consulta de Log
    Public Const strEvtRegMotRech = 3817 'Registrar Motivo de Rechazo
    Public Const strEvtModMotRech = 3818 'Modificar Motivo de Rechazo
    Public Const strEvtEliMotRech = 3819 'Eliminar Motivo de Rechazo
    Public Const strEvtConMotRech = 3820 'Consultar Motivo de Rechazo
    Public Const strEvtIntegracionPVU = 3821 'Integración PVU

    Public Const strTipoVentaSC = "01;POSTPAGO|04;CONTROL"
    Public Const strTipoOperacionSC = "01;VENTA NORMAL/ALTA|02;MIGRACION"
    Public Const strCaracClienteSC = "S;DEPENDIENTE|N;INDEPENDIENTE|R;RECURRENTE"
    Public Const strMonedaTarjetaSC = "L;LOCAL|D;DOLARES"

    '=====================================
    Public Const strOptManteUbicaciones = 4003
    Public Const strEvtBuscarUbicaciones = 4004
    Public Const strOptMntoUbicaciones = 4005 'Codigo Opcion
    Public Const strEvtCrearUbicacion = 4006 'Codigo Evento
    Public Const strEvtModificarUbicacion = 4007 'Codigo Evento
    Public Const strEvtEliminarUbicaciones = 4008
    Public Const strOptMntoPeriodos = 4009
    Public Const strEvtCrearPeriodo = 4010
    Public Const strEvtEliminarPeriodo = 4011
    Public Const strEvtModificarPeriodo = 4012
    Public Const strEvtBuscarPeriodos = 4013 'Evento Buscar Periodos 
    'Opciones de Tipificaciones

    Public Const strOptResultados = 0
    Public Const strOptTipificacion = 0
    Public Const strOptDetalle = 0
    Public Const strOptPossol = 0
    '===============================================================================
    ' Constante para que indica que codigo es Venta Irregular.
    '===============================================================================
    Public Const gIntIND_VENTA_IRREGULAR = 2
    Public Const gstrMenDatosVenta = "No existen datos registrados en PVU"

    '--Campaña Octubre
    Public Const gstrMsgPagodePre = "Debe cancelar previamente el documento asociado"
    Public Const gstrMsgPagodePost = "No se puede pagar el documento postpago de la campaña"
    Public Const gstrMsgPago = "No se puede pagar el documento de la campaña"
    Public Const gstrMsgPrepago = "Puede ingresar solamente artículos asociados (Pack + Chip) para la campaña elegida."
    Public Const gstrMsgCancelarPagoVR = "Venta completada con exito. Para pagar este documento acceda a través del Pool de Documentos por Pagar."
    Public Const gstrCamEstadoLibre = "1"
    Public Const gstrCamEstadoUsado = "2"
    Public Const gstrCamEstadoAnulado = "3"
    Public Const gstrCamEstadoPagado = "4"
    Public Const gstrCamEstadoVendido = "5"
    '==================================================================================
    '===============================================================================
    ' Constantes para el cambio de nombre y  marca registrada
    '===============================================================================
    Public Const strRaz = "América Móvil Perú S.A.C."
    Public Const strMarca = "Marca licenciada por TIM Italia S.p.A."

    '@@@ BEGIN
    'Resposanble		: Narciso Lema Ch.
    'Modificación		: Agregar constante para mínimos y máximos de longitud de Carné de Extranjería.
    Public Const gIntCarneExtranjeriaMin = 7
    Public Const gIntCarneExtranjeriaMax = 9
    '@@@ END

    '=====================Cliente Recurrente ================================================

    Public Const gstrTituloConsultaCR = "Registros en Infocorp"
    Public Const gstrCodigoObcCR = "008"

    Public Const gStrUrlSvrACR = "http://172.19.26.71:8001/wli_Activaciones/webservices/"
    Public Const gStrServicioMCR = "ObtieneCuentasCliente.jws"
    Public Const gStrServicioACR = "ActivaAcuerdo.jws"
    Public Const gStrUsuarioACR = "T10768"
    Public Const gStrMsgCargoFijo = "No pueden elegir planes tarifarios mayores al aprobado en Infocorp" ' Mensaje para validacion de Plan Tarifario
    Public Const gStrMsgConsultaInfocorp = "No existen evaluaciones para el cliente solicitado"
    Public Const gStrMsgVigencia = "Las evaluaciones encontradas no se encuentran vigentes"
    Public Const gStrMsgConsultaST = "No se pueden realizar consultas en este momento"
    Public Const gStrValorRec = "1"
    Public Const gStrValorActLin = "X"
    Public Const gStrMsgInfocorp1 = "CONSULTAR CON CREDITOS"
    Public Const gStrResutAprobar = "aprobar"
    Public Const gStrCodAprobar = "01"
    Public Const gStrResutRechazar = "rechazar"
    Public Const gStrCodRechazar = ""

    Public Const gStrResutOtro1 = ""
    Public Const gStrCodOtro1 = ""
    Public Const gStrResutOtro2 = ""
    Public Const gStrCodOtro2 = ""
    Public Const gStrCodFacturar = "0010;"

    Public Const gStrTituloInfocorp = "Este Analisis es confidencial y solo debera usarse para la celebración de negocios. Prohibida su reproducción, divulgación y entrega a terceros.<br> El análisis se ha realizado aplicando la política de AMERICA MOVIL PERU S.A.C.. La evaluación de la solicitud de crédito y la decisión de otorgarlo o no es de exclusiva responsabilidad de AMERICA MOVIL PERU S.A.C."
    Public Const gStrEvalCredInfocorp = "Evaluación Crediticia - Infocorp"
    Public Const gStrModeloInfocorp = "AMERICA MOVIL - PERU"
    Public Const gStrErrorInfocorpControl = "[974]"
    Public Const gStrErrorInfocorpConsulta = "[975]"
    '==================================================================================

    Public Const gstrRENREPHDC = "120"

    '***************************************************************
    '* RECAUDACIONES
    '***************************************************************
    Const cteCODIGO_BINADQUIRIENTE = "620700"
    Const cteCODIGO_CANAL = "91"
    Const cteCODIGO_RUTALOG = "D:\Intratim\PVUD\TMPREC"
    Const cteCODIGO_DETALLELOG = "DEB"

    'Opcion Recaudaciones

    Public Const strOptRecaudacion = 6495

    'Eventos Recaudaciones

    Public Const strEvtObtDocporPagar = 3858     'Obtener Documentos por pagar
    Public Const strEvtPagarDocumentos = 3859   'Pagar Documentos
    Public Const strEvtObtDocDeudaSAP = 3860   'Obtener Documento Deuda SAP
    Public Const strEvtAnuDocDeudaSAP = 3861  'Anular Documento Deuda SAP
    Public Const strEvtBuscarDocumentos = 3862  'Buscar Documentos

    '***************************************************************

    Public Const cteMSG_GLOSA_VENTA_CUOTAS = "***FINANCIADO EN # CUOTA(S) EN RECIBO POSTPAGO.***"
    'Deposito en garantia
    Public Const cteTIPODOC_DEPOSITOGARANTIA = "DG"
    Public Const K_MSG_NO_REGLA_EXCEP = "No existen políticas de créditos que permitan la venta"
    Public Const K_str_Apro = "APROBAR"
    Public Const K_str_Apro_Ver_BOL = "APROBAR__VERIFICAR_BOLETA"
    Public Const K_str_Obs = "OBSERVAR"
    Public Const K_str_Rec = "RECHAZAR"
    Public Const K_str_Cond_CargoFijo = "El cargo fijo solicitado supera el permitido por la política de créditos"
    Public Const K_Str_OfVtaTC01 = "0001"
    Public Const k_Flag_NoExisteBSCS = "0"
    Public Const k_Flag_TipoEvalCli_Nuevo = "0"
    Public Const k_meses_Validos = 13


    '----------PCCASH - TICKETERA-----------
    Public Const k_Prefijo_Ticket = "12"

    '----------IMPRESION SAP----------------
    Public Const k_Prt_Anulacion_Ticket = "RVTICKET_ANULDO"
    Public Const k_Prt_Deposito_Garantia = "ZRVDEP_GARANTIA"
    Public Const k_Prt_Recaudacion = "ZRVRECAUDACION"

End Class
