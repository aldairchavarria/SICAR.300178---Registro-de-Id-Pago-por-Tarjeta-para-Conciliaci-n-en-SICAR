Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Globalization
Imports System.Web
Imports COM_SIC_Activaciones

Public Class BLDatosCBIO_App
    Dim objFileLog As New SrvPago_Log
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim nameLogCBIO As String = objFileLog.Log_CrearNombreArchivo("Log_CBIO")
    Dim objCbioWS As New BWServicesCBIO

    'INI: INICIATIVA-219
    Public Function LeerDatosCliente(ByVal nroTelefono As String, ByVal strCustCode As String, ByVal strempleado As String) As clsClienteBSCS

        Dim objCliente As New clsClienteBSCS
        Dim objDatosBSCS As New clsDatosPostpagoNegocios
        Dim objCBIO As New ClsActivacionCBIO
        Dim mensajeError As String = String.Empty
        Dim strOrigenLinea As String = String.Empty
        Dim strCodigoRespuesta As String = String.Empty
        Dim strMensajeRespuesta As String = String.Empty

        Try
            strOrigenLinea = objCbioWS.ConsultarLineaCuentaWSCBIO(nroTelefono)

            If strOrigenLinea = "1" Then 'BSCS9(CBIO)
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIO][INICIATIVA-219][LeerDatosCliente]")

                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][INPUT][PI_NUMERO_LINEA]", Funciones.CheckStr(nroTelefono)))

                objCliente = objCBIO.ObtenerDatosClienteCBIO(nroTelefono, strCodigoRespuesta, strMensajeRespuesta)

                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_NUMERO_LINEA]", Funciones.CheckStr(objCliente.numero_telefono)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_TIPO_DOCUMENTO]", Funciones.CheckStr(objCliente.tip_doc)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_NUMERO_DOCUMENTO]", Funciones.CheckStr(objCliente.num_doc)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_NOMBRES]", Funciones.CheckStr(objCliente.Nombre)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_APELLIDOS]", Funciones.CheckStr(objCliente.apellidos)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_RUC_DNI]", Funciones.CheckStr(objCliente.ruc_dni)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_NUMERO_CUENTA]", Funciones.CheckStr(objCliente.cuenta)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_CO_ID]", Funciones.CheckStr(objCliente.co_id)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_CO_ID_PUB]", Funciones.CheckStr(objCliente.co_id_pub)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_CUSTOMER_ID]", Funciones.CheckStr(objCliente.customerId)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_CUSTOMER_ID_PUB]", Funciones.CheckStr(objCliente.customer_id_pub)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_IMSI]", Funciones.CheckStr(objCliente.imsi)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_CICLO_FACT]", Funciones.CheckStr(objCliente.ciclo_fac)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_BILLING_ACCOUNT_ID]", Funciones.CheckStr(objCliente.billingAccountId)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_PO_ID_PLAN_NUEVO]", Funciones.CheckStr(objCliente.productOfferingIdNew)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_PO_ID_PLAN_ANTERIOR]", Funciones.CheckStr(objCliente.productOfferingIdOld)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_TIPO_CLIENTE]", Funciones.CheckStr(objCliente.tipo_cliente)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_CODIGO_TIPO_CLIENTE]", Funciones.CheckStr(objCliente.codigo_tipo_cliente)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_DIRECCION_FAC]", Funciones.CheckStr(objCliente.direccion_fac)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_URBANIZACION_FAC]", Funciones.CheckStr(objCliente.urbanizacion_fac)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_DEPARTAMENTO_FAC]", Funciones.CheckStr(objCliente.departamento_fac)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_PROVINCIA_FAC]", Funciones.CheckStr(objCliente.provincia_fac)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CURSOR_RENOVACION][TRANSV_DISTRITO_FAC]", Funciones.CheckStr(objCliente.distrito_fac)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_CODIGO_RESPUESTA]", Funciones.CheckStr(strCodigoRespuesta)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][LeerDatosCliente][OUTPUT][PO_MENSAJE_RESPUESTA]", Funciones.CheckStr(strMensajeRespuesta)))

            Else 'BSCS
                objCliente = objDatosBSCS.LeerDatosCliente(nroTelefono, "", strempleado, "")
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIATIVA-219][LeerDatosCliente][Ocurrio un error al obtener datos del cliente")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][LeerDatosCliente][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
        End Try

        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[FIN][INICIATIVA-219][LeerDatosCliente]", String.Empty))

        Return objCliente

    End Function

    Public Function CambioPlanCBIO(ByVal strCodigoInteraccion As String, ByVal strNumeroLinea As String, ByVal strFechaProgramacion As String, ByVal strIdContrato As Int64, ByVal strNumeroSEC As Int64, ByVal strUsuario As String) As Boolean
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIO][INICIATIVA-219][CambioPlanCBIO]")
        Dim objRequest As New RequestMigracionPlan
        Dim objResponse As New ResponseMigracionPlan
        Dim objCambioPlan As New BECambioPlanCBIO
        Dim objNegocio As New ClsActivacionCBIO
        Dim objCliente As New clsClienteBSCS
        Dim lstOpcional As New ListaOpcional
        Dim lstServicios As New ArrayList
        Dim lstBonos As New ArrayList
        Dim objListaServicios As New ArrayList
        Dim strCodigoRespuesta As String = String.Empty
        Dim strMensajeRespuesta As String = String.Empty
        Dim blResultado As Boolean = False
        Dim cantServicios As Integer = 0
        Dim cantBonos As Integer = 0

        Try

            objCliente = LeerDatosCliente(strNumeroLinea, String.Empty, String.Empty)

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIO][INICIATIVA-219][DatosCambioPlanCBIO]")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][DatosCambioPlanCBIO][INPUT][PI_NUMERO_SEC]", Funciones.CheckStr(strNumeroSEC)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][DatosCambioPlanCBIO][INPUT][PI_NUMERO_ACUERDO]", Funciones.CheckStr(strIdContrato)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][DatosCambioPlanCBIO][INPUT][PI_NUMERO_TELEFONO]", Funciones.CheckStr(strNumeroLinea)))

            objCambioPlan = objNegocio.DatosCambioPlanCBIO(strNumeroSEC, strIdContrato, strNumeroLinea, lstServicios, lstBonos, strCodigoRespuesta, strMensajeRespuesta)

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][DatosCambioPlanCBIO][OUTPUT][PO_CODIGO_RESPUESTA]", Funciones.CheckStr(strCodigoRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][DatosCambioPlanCBIO][OUTPUT][PO_MENSAJE_RESPUESTA]", Funciones.CheckStr(strMensajeRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[FIN][INICIATIVA-219][DatosCambioPlanCBIO]")

            objRequest.programarMigracionPlanRequest.idNegocio = Funciones.CheckStr(strIdContrato)
            objRequest.programarMigracionPlanRequest.aplicacion = "SISACT"
            objRequest.programarMigracionPlanRequest.usuarioAplicacion = "USRSISACT"
            objRequest.programarMigracionPlanRequest.usuarioSistema = Funciones.CheckStr(strUsuario)
            objRequest.programarMigracionPlanRequest.descCoSer = "CAMBIO DE PLAN + RENOVACION"
            objRequest.programarMigracionPlanRequest.codigoInteraccion = strCodigoInteraccion
            objRequest.programarMigracionPlanRequest.nroCuenta = Funciones.CheckStr(objCliente.cuenta)
            objRequest.programarMigracionPlanRequest.fechaProgramacion = Funciones.CheckStr(strFechaProgramacion)
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.flagAplicaOCC = "1" 'Por defecto para SISACT debe ir 1
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.montoOCC = String.Empty 'El monto OCC se genera en la venta si en caso tiene un monto de reintegro mayor que 0
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.canal = "CAC"
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.codigoPuntoVenta = objCambioPlan.CodigoOficinaVenta
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.sec = Funciones.CheckStr(strNumeroSEC)
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.tipoOperacion = objCambioPlan.TipoOperacion
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.fechaCreSolicitud = objCambioPlan.FechaSolicitudReno
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosCliente.csId = Funciones.CheckStr(objCliente.customerId)
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosCliente.csIdPub = Funciones.CheckStr(objCliente.customer_id_pub)
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosCliente.customerSegment = objCambioPlan.SegmentoCliente
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosCliente.customerBehaviorPaid = objCambioPlan.ComportamientoPago
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosCliente.customerBillingAccountCode = Funciones.CheckStr(objCliente.billingAccountId)
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosCliente.customerBillCycleId = Funciones.CheckStr(objCliente.ciclo_fac)
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.msisdn = strNumeroLinea
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.coId = Funciones.CheckStr(objCliente.co_id)
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.coIdPub = Funciones.CheckStr(objCliente.co_id_pub)
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.especialCase = objCambioPlan.CasoEspecial
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.campain = objCambioPlan.Campana
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.tope = objCambioPlan.TopeConsumo
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.suscripcionCorreo = IIf(Funciones.CheckStr(objCambioPlan.FlagCorreo).Equals(""), "0", "1")
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.marcaEquipo = objCambioPlan.MarcaEquipo
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.modeloEquipo = objCambioPlan.ModeloEquipo
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.precioVentaEquipo = objCambioPlan.PrecioVenta
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.fechaActivacion = objCambioPlan.FechaActivacionAlta
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.fechaInicioContrato = objCambioPlan.FechaInicioContratoActual
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.plazoContrato = objCambioPlan.PlazoContratoActual
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.tipoProducto = objCambioPlan.TipoProducto
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.tipoTecnologia = objCambioPlan.TipoTecnologia
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.tipoSuscripcion = objCambioPlan.TipoSuscripcion
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.modalidadPago = objCambioPlan.ModalidadPago
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.poIdOld = objCambioPlan.PoIdActual
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.poNameOld = objCambioPlan.PoIdActualDescripcion
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.poIdOldCargoFijo = objCambioPlan.CargoFijoActualPlan
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.poIdNew = objCambioPlan.PoIdNuevo
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.poNameNew = objCambioPlan.PoIdNuevoDescripcion
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.poIdNewCargoFijo = objCambioPlan.CargoFijoNuevoPlan
            objRequest.programarMigracionPlanRequest.listaOpcional = Nothing

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][idNegocio]", objRequest.programarMigracionPlanRequest.idNegocio))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][aplicacion]", objRequest.programarMigracionPlanRequest.aplicacion))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][usuarioAplicacion]", objRequest.programarMigracionPlanRequest.usuarioAplicacion))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][usuarioSistema]", objRequest.programarMigracionPlanRequest.usuarioSistema))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][descCoSer]", objRequest.programarMigracionPlanRequest.descCoSer))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][codigoInteraccion]", objRequest.programarMigracionPlanRequest.codigoInteraccion))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][fechaProgramacion]", objRequest.programarMigracionPlanRequest.fechaProgramacion))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][flagAplicaOCC]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.flagAplicaOCC))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][montoOCC]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.montoOCC))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][canal]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.canal))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][codigoPuntoVenta]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.codigoPuntoVenta))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][sec]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.sec))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][tipoOperacion]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.tipoOperacion))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][fechaCreSolicitud]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.fechaCreSolicitud))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosCliente][csId]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosCliente.csId))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosCliente][csIdPub]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosCliente.csIdPub))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosCliente][customerSegment]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosCliente.customerSegment))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosCliente][customerBehaviorPaid]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosCliente.customerBehaviorPaid))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosCliente][customerBillingAccountCode]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosCliente.customerBillingAccountCode))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosCliente][customerBillCycleId]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosCliente.customerBillCycleId))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][msisdn]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.msisdn))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][coId]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.coId))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][coIdPub]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.coIdPub))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][especialCase]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.especialCase))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][campain]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.campain))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][tope]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.tope))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][suscripcionCorreo]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.suscripcionCorreo))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][marcaEquipo]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.marcaEquipo))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][modeloEquipo]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.modeloEquipo))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][precioVentaEquipo]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.precioVentaEquipo))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][fechaActivacion]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.fechaActivacion))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][fechaInicioContrato]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.fechaInicioContrato))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][plazoContrato]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.plazoContrato))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][tipoProducto]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.tipoProducto))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][tipoTecnologia]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.tipoTecnologia))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][tipoSuscripcion]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.tipoSuscripcion))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][modalidadPago]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.modalidadPago))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][poIdOld]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.poIdOld))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][poNameOld]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.poNameOld))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][poIdOldCargoFijo]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.poIdOldCargoFijo))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][poIdNew]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.poIdNew))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][poNameNew]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.poNameNew))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][datosMigracionPlan][datosLinea][poIdNewCargoFijo]", objRequest.programarMigracionPlanRequest.datosMigracionPlan.datosLinea.poIdNewCargoFijo))

            'SERVICIOS ADICIONALES DEL NUEVO PLAN PARA ACTIVAR
            If Not IsNothing(lstServicios) Then
                If lstServicios.Count > 0 Then
                    For Each strServicio As String In lstServicios
                        Dim objServicio As New serviciosAdicionales
                        cantServicios += 1
                        objServicio.codigoServicio = Funciones.CheckStr(strServicio)
                        objServicio.estado = "A"
                        objServicio.tipoServicio = Funciones.CheckStr(ConfigurationSettings.AppSettings("CodTipoServicioAdicional"))

                        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][Nro Item Servicio]", Funciones.CheckStr(cantServicios)))
                        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][serviciosAdicionales][codigoServicio]", objServicio.codigoServicio))
                        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][serviciosAdicionales][estado]", objServicio.estado))
                        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][serviciosAdicionales][tipoServicio]", objServicio.tipoServicio))

                        objListaServicios.Add(objServicio)
                    Next
                    objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][Cantidad de Servicios adicionales a activar]", Funciones.CheckStr(cantServicios)))
                    cantServicios = 0
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][serviciosAdicionales]", "No tiene Servicios adicionales por activar"))
            End If

            'BONOS COMPATIBLES CON EL NUEVO PLAN NUEVO PARA ACTIVAR
            If Not IsNothing(lstBonos) Then
                If lstBonos.Count > 0 Then
                    For Each strBonos As String In lstBonos
                        Dim objServicio As New serviciosAdicionales
                        cantBonos += 1
                        objServicio.codigoServicio = Funciones.CheckStr(strBonos)
                        objServicio.tipoServicio = Funciones.CheckStr(ConfigurationSettings.AppSettings("CodTipoBonoCompatible"))

                        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][Nro Item Bono]", Funciones.CheckStr(cantBonos)))
                        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][BonosCompatibles][codigoServicio]", objServicio.codigoServicio))
                        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][BonosCompatibles][tipoServicio]", objServicio.tipoServicio))

                        objListaServicios.Add(objServicio)
                    Next
                    objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][Cantidad de Bonos compatibles a activar]", Funciones.CheckStr(cantBonos)))
                    cantServicios = 0
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][BonosCompatibles]", "No tiene Bonos compatibles por activar"))
            End If
            ''----
            'TOPE DE CONSUMO
            Dim objConsumo As New serviciosAdicionales
            objConsumo.categoriaServicio = "TOPE DE CONSUMO"
            objConsumo.codigoServicio = "CreditLimitType"
            objConsumo.estado = "A"
            objConsumo.tipoServicio = "CARACTERISTICA"
            objConsumo.valorServicio = Funciones.CheckStr(objCambioPlan.TopeConsumo)

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][Tope de Consumo][categoriaServicio]", Funciones.CheckStr(objConsumo.categoriaServicio)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][Tope de Consumo][codigoServicio]", Funciones.CheckStr(objConsumo.codigoServicio)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][Tope de Consumo][estado]", Funciones.CheckStr(objConsumo.estado)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][Tope de Consumo][tipoServicio]", Funciones.CheckStr(objConsumo.tipoServicio)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][Tope de Consumo][valorServicio]", Funciones.CheckStr(objConsumo.valorServicio)))
            objListaServicios.Add(objConsumo)

            'MONTO TOPE DE CONSUMO
            Dim objMonto As New serviciosAdicionales
            objMonto.categoriaServicio = "TOPE DE CONSUMO"
            objMonto.codigoServicio = "CreditLimitUsageThreshold_7000"
            objMonto.estado = "A"
            objMonto.tipoServicio = "CARACTERISTICA"
            objMonto.valorServicio = Funciones.CheckStr(objCambioPlan.MontoTopeConsumo)

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][Monto Tope de Consumo][categoriaServicio]", Funciones.CheckStr(objMonto.categoriaServicio)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][Monto Tope de Consumo][codigoServicio]", Funciones.CheckStr(objMonto.codigoServicio)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][Monto Tope de Consumo][estado]", Funciones.CheckStr(objMonto.estado)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][Monto Tope de Consumo][tipoServicio]", Funciones.CheckStr(objMonto.tipoServicio)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][CambioPlanCBIO][INPUT][Monto Tope de Consumo][valorServicio]", Funciones.CheckStr(objMonto.valorServicio)))
            objListaServicios.Add(objMonto)
            ''----
            objRequest.programarMigracionPlanRequest.datosMigracionPlan.serviciosAdicionales = objListaServicios

            objResponse = objCbioWS.CambioPlanWSCBIO(objRequest)

            If objResponse.programarMigracionPlanResponse.responseAudit.codigoRespuesta = "0" Then
                blResultado = True
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIATIVA-219][CambioPlanCBIO][Ocurrio un error al ejecutar el cambio de plan en CBIO")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][CambioPlanCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
            Throw ex
        End Try

        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[FIN][INICIATIVA-219][CambioPlanCBIO]", String.Empty))

        Return blResultado

    End Function
    'FIN: INICIATIVA-219
End Class
