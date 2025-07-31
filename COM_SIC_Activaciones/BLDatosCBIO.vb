Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Globalization
Imports System.Web
Imports COM_SIC_Activaciones.ActualizarParticipante
Imports COM_SIC_Activaciones.ActualizarDatosFacturacion
Imports COM_SIC_Activaciones.ActivarServiciosAdicionalesCBIORest
Imports COM_SIC_Activaciones.claro_int_consultaclienteCBIORest.consultarDatosLineaCta
Imports COM_SIC_Seguridad
Imports System.Xml

Public Class BLDatosCBIO

    Dim objFileLog As New SICAR_Log
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
	Dim TipoOperacionCambioPlanCBIO As String = ConfigurationSettings.AppSettings("constTipoOperacionCambioPlanCBIO")
    Dim nameLogCBIO As String = objFileLog.Log_CrearNombreArchivo("Log_CBIO")
    Dim objCbioWS As New BWServicesCBIO

    'INI: INICIATIVA-219
    Public Function ObtenerTipoDocumentoCBIO(ByVal strTipoDocumento As String) As String
        Dim objTipoDocumento As New TipoDocumento
        Dim objCBIO As New ClsActivacionCBIO
        Dim strTipoDocCBIO = String.Empty
        Dim objTipoDoc As New ArrayList
        objTipoDoc = objCBIO.ListarTipoDocumento()

        For Each item As TipoDocumento In objTipoDoc
            If item.TDOCC_CODIGO = strTipoDocumento Then
                strTipoDocCBIO = Funciones.CheckStr(item.ID_BSCS_IX)
                Exit For
            End If
        Next

        Return strTipoDocCBIO
    End Function

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
                HttpContext.Current.Session("ClienteCBIO") = Nothing
                HttpContext.Current.Session("ClienteCBIO") = objCliente

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

	Public Function ObtenerDatosClienteRenovacionCBIO(ByVal strOrigenLinea As String, ByVal strNumeroLinea As String, ByVal strNumeroSEC As Int64, ByVal strNumeroContrato As Int64) As Boolean
        Dim blResultado As Boolean = False
        Dim objDatarenovacion As New BECambioPlanCBIO
        Dim objNegocio As New ClsActivacionCBIO
        Dim strCodigoRespuesta As String = String.Empty
        Dim strMensajeRespuesta As String = String.Empty
        Dim lstServicios As New ArrayList
        Dim lstBonos As New ArrayList
        Try
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[INICIO][INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO]", String.Empty))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][INPUT][strNumeroSEC]", strNumeroSEC))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][INPUT][strNumeroContrato]", strNumeroContrato))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][INPUT][strNumeroLinea]", strNumeroLinea))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][INPUT][strOrigenLinea]", strOrigenLinea))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][SP][PVUDB]", "SISACT_PKG_GENERAL_CBIO.SISACTSS_DATOS_MIGRACION_PLAN"))
            objDatarenovacion = objNegocio.DatosCambioPlanCBIO(strNumeroSEC, strNumeroContrato, strNumeroLinea, lstServicios, lstBonos, strCodigoRespuesta, strMensajeRespuesta)
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][PO_CODIGO_RESPUESTA]", Funciones.CheckStr(strCodigoRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][PO_MENSAJE_RESPUESTA]", Funciones.CheckStr(strMensajeRespuesta)))
            If strCodigoRespuesta <> "0" Then
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO] AL NO ENCONTRAR INFORMACION NO SE DEBE CONTINUAR CON EL PROCESO")
                Return blResultado
            End If

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][Campana]", Funciones.CheckStr(objDatarenovacion.Campana)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][CanalVenta]", Funciones.CheckStr(objDatarenovacion.CanalVenta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][CargoFijoActualPlan]", Funciones.CheckStr(objDatarenovacion.CargoFijoActualPlan)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][CargoFijoNuevoPlan]", Funciones.CheckStr(objDatarenovacion.CargoFijoNuevoPlan)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][CodigoInterlocutor]", Funciones.CheckStr(objDatarenovacion.CodigoInterlocutor)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][CodigoOficinaVenta]", Funciones.CheckStr(objDatarenovacion.CodigoOficinaVenta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][ComportamientoPago]", Funciones.CheckStr(objDatarenovacion.ComportamientoPago)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][Iccid]", Funciones.CheckStr(objDatarenovacion.Iccid)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][MarcaEquipo]", Funciones.CheckStr(objDatarenovacion.MarcaEquipo)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][ModalidadPago]", Funciones.CheckStr(objDatarenovacion.ModalidadPago)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][NumeroDocumento]", Funciones.CheckStr(objDatarenovacion.NumeroDocumento)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][NumeroLinea]", Funciones.CheckStr(objDatarenovacion.NumeroLinea)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][NumeroSec]", Funciones.CheckStr(objDatarenovacion.NumeroSec)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][PoIdActual]", Funciones.CheckStr(objDatarenovacion.PoIdActual)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][PoIdActualDescripcion]", Funciones.CheckStr(objDatarenovacion.PoIdActualDescripcion)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][PoIdNuevo]", Funciones.CheckStr(objDatarenovacion.PoIdNuevo)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][PoIdNuevoDescripcion]", Funciones.CheckStr(objDatarenovacion.PoIdNuevoDescripcion)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][TipoDocumento]", Funciones.CheckStr(objDatarenovacion.TipoDocumento)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][TipoOperacion]", Funciones.CheckStr(objDatarenovacion.TipoOperacion)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][OUTPUT][TipoProducto]", Funciones.CheckStr(objDatarenovacion.TipoProducto)))

            blResultado = True
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[FIN][INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO]")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][Ocurrio un error al obtener datos del cliente]")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
        Finally
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[FIN][INICIATIVA-219][ObtenerDatosClienteRenovacionCBIO]", String.Empty))
        End Try
        Return blResultado
    End Function
	
    Public Function ActualizarLimiteCreditoCBIO(ByVal strTipoDoc As String, ByVal strNumeroDOC As String, ByVal strLimiteCredito As String)
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIO][INICIATIVA-219][ActualizarLimiteCreditoCBIO]")
        Dim objParticipanteRequest As New ActualizarParticipanteRequest
        Dim objParticipanteResponse As New ActualizarParticipanteResponse
        Dim strTipoDocCBIO As String = String.Empty
        Dim strCodClienteUnico As String = String.Empty
        Dim strCodigoRespuesta As String = String.Empty
        Dim strMensajeRespuesta As String = String.Empty

        Try
            strTipoDocCBIO = ObtenerTipoDocumentoCBIO(strTipoDoc) 'Obtener Tipo Documento de CBIO

            strCodClienteUnico = Funciones.CheckStr(String.Format("{0}{1}{2}", strTipoDocCBIO, "-", strNumeroDOC))

            objParticipanteRequest.tipoDocumento = Funciones.CheckStr(strTipoDocCBIO)
            objParticipanteRequest.numeroDocumento = Funciones.CheckStr(strNumeroDOC)
            objParticipanteRequest.codigoClienteUnico = strCodClienteUnico
            objParticipanteRequest.participante.limiteCredito = Funciones.CheckStr(strLimiteCredito)

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarLimiteCreditoCBIO][INPUT][tipoDocumento]", Funciones.CheckStr(objParticipanteRequest.tipoDocumento)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarLimiteCreditoCBIO][INPUT][numeroDocumento]", Funciones.CheckStr(objParticipanteRequest.numeroDocumento)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarLimiteCreditoCBIO][INPUT][CodClienteUnico]", Funciones.CheckStr(strCodClienteUnico)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarLimiteCreditoCBIO][INPUT][LimiteCredito]", Funciones.CheckStr(strLimiteCredito)))

            objParticipanteResponse = objCbioWS.ActualizarParticipanteWSCBIO(objParticipanteRequest)

            If Not IsNothing(objParticipanteResponse) Then
                strCodigoRespuesta = Funciones.CheckStr(objParticipanteResponse.codigoRespuesta)
                strMensajeRespuesta = Funciones.CheckStr(objParticipanteResponse.mensajeRespuesta)

                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarLimiteCreditoCBIO][OUTPUT][strCodigoRespuesta]", Funciones.CheckStr(strCodigoRespuesta)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarLimiteCreditoCBIO][OUTPUT][strMensajeRespuesta]", Funciones.CheckStr(strMensajeRespuesta)))
            Else
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarLimiteCreditoCBIO][objParticipanteResponse]", "Nulo o Vacio"))
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIATIVA-219][ActualizarLimiteCreditoCBIO][Ocurrio un error al actualizar el Limite de Credito del Cliente en CBIO")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][ActualizarLimiteCreditoCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
        End Try

        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[FIN][INICIATIVA-219][ActualizarLimiteCreditoCBIO]", String.Empty))

    End Function

    Public Function ActualizarAfiliacionCorreoCBIO(ByVal strCustomerId As String, ByVal strEstadoAfiliacion As String, ByVal strCorreoElectronico As String)
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIO][INICIATIVA-219][ActualizarAfiliacionCorreoCBIO]")
        Dim objDatosFacturacionRequest As New ActualizarDatosFacturacionRequest
        Dim objDatosFacturacionResponse As New ActualizarDatosFacturacionResponse
        Dim strIdTransaccion As String = String.Empty
        Dim strCodigoRespuesta As String = String.Empty
        Dim strMensajeRespuesta As String = String.Empty

        Try
            objDatosFacturacionRequest.csId = Funciones.CheckStr(strCustomerId)

            If strEstadoAfiliacion = "1" Or strEstadoAfiliacion = "2" Then
                objDatosFacturacionRequest.bmId = "19" 'AFILIADO
            ElseIf strEstadoAfiliacion = String.Empty Or strEstadoAfiliacion Is Nothing Then
                objDatosFacturacionRequest.bmId = "18" 'NO AFILIADO
            End If

            objDatosFacturacionRequest.adrEmail = Funciones.CheckStr(strCorreoElectronico)

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarAfiliacionCorreoCBIO][INPUT][CustomerId]", Funciones.CheckStr(objDatosFacturacionRequest.csId)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarAfiliacionCorreoCBIO][INPUT][Afiliado O NO Afiliado]", Funciones.CheckStr(objDatosFacturacionRequest.bmId)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarAfiliacionCorreoCBIO][INPUT][Email]", Funciones.CheckStr(objDatosFacturacionRequest.adrEmail)))

            objDatosFacturacionResponse = objCbioWS.ActualizarDatosFacturacionWSCBIO(objDatosFacturacionRequest)

            If Not IsNothing(objDatosFacturacionResponse) Then
                strIdTransaccion = Funciones.CheckStr(objDatosFacturacionResponse.responseAudit.idTransaccion)
                strCodigoRespuesta = Funciones.CheckStr(objDatosFacturacionResponse.responseAudit.codigoRespuesta)
                strMensajeRespuesta = Funciones.CheckStr(objDatosFacturacionResponse.responseAudit.mensajeRespuesta)

                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarAfiliacionCorreoCBIO][OUTPUT][strIdTransaccion]", Funciones.CheckStr(strIdTransaccion)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarAfiliacionCorreoCBIO][OUTPUT][strCodigoRespuesta]", Funciones.CheckStr(strCodigoRespuesta)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarAfiliacionCorreoCBIO][OUTPUT][strMensajeRespuesta]", Funciones.CheckStr(strMensajeRespuesta)))
            Else
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarAfiliacionCorreoCBIO][objDatosFacturacionResponse]", "Nulo o Vacio"))
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIATIVA-219][ActualizarAfiliacionCorreoCBIO][Ocurrio un error al actualizar el Estado de Afiliacion y el correo del Cliente en CBIO")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][ActualizarAfiliacionCorreoCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
        End Try

        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[FIN][INICIATIVA-219][ActualizarAfiliacionCorreoCBIO]", String.Empty))

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
        'Encolar
        Dim objEncolarCBIO As New BWEncolarTransaccionesCBIO
        Dim objEncolarRequest As New EncolarTransaccionRequest
        Dim objEncolarResponse As New EncolarTransaccionResponse
        Dim objAuditoriaWS As New AuditoriaEWS
        'Parametro XML
        Dim strBldTransaccion As String = String.Empty
        Try
            If Not IsNothing(HttpContext.Current.Session("ClienteCBIO")) Then
                objCliente = CType(HttpContext.Current.Session("ClienteCBIO"), clsClienteBSCS)
            Else
                objCliente = LeerDatosCliente(strNumeroLinea, String.Empty, String.Empty)
            End If

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIO][INICIATIVA-219][DatosCambioPlanCBIO]")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][DatosCambioPlanCBIO][INPUT][PI_NUMERO_SEC]", Funciones.CheckStr(strNumeroSEC)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][DatosCambioPlanCBIO][INPUT][PI_NUMERO_ACUERDO]", Funciones.CheckStr(strIdContrato)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][DatosCambioPlanCBIO][INPUT][PI_NUMERO_TELEFONO]", Funciones.CheckStr(strNumeroLinea)))

            objCambioPlan = objNegocio.DatosCambioPlanCBIO(strNumeroSEC, strIdContrato, strNumeroLinea, lstServicios, lstBonos, strCodigoRespuesta, strMensajeRespuesta)

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][DatosCambioPlanCBIO][OUTPUT][PO_CODIGO_RESPUESTA]", Funciones.CheckStr(strCodigoRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][DatosCambioPlanCBIO][OUTPUT][PO_MENSAJE_RESPUESTA]", Funciones.CheckStr(strMensajeRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[FIN][INICIATIVA-219][DatosCambioPlanCBIO]")

            If strCodigoRespuesta <> "0" Then
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][DatosCambioPlanCBIO] AL NO ENCONTRAR INFORMACION NO SE DEBE REALIZAR LA PROGRAMACION"))
                blResultado = False
                Return blResultado
            End If

            If (Funciones.CheckStr(ReadKeySettings.key_FlagMejoraRenovacion) = "1") Then
                'Armado XML para encolaTransaccion
                strBldTransaccion = ArmarEstructuraXMLCambioPlanCBIO(strIdContrato, strUsuario, strCodigoInteraccion, objCliente, strFechaProgramacion, objCambioPlan, strNumeroLinea, Funciones.CheckStr(strNumeroSEC), lstServicios, lstBonos)

                'EncolarTransaccion
                Dim FechaTransaccion As String = DateTime.Now.ToString("yyyyMMddHHmmss")
                Dim idTransaccion As String = String.Empty
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("[INICIATIVA-219][CambioPlanCBIO] - [Inicio EncolarTransaccionCBIO()"))
                If (strNumeroLinea.Substring(0, 2) = "51") Then
                    idTransaccion = FechaTransaccion + strNumeroLinea + "22"
                Else
                    idTransaccion = FechaTransaccion + "51" + strNumeroLinea + "22"
                End If
                objEncolarRequest.encolarTransaccionRequest.idNegocio = Funciones.CheckStr(strIdContrato)
                objEncolarRequest.encolarTransaccionRequest.idNegocioAux = Nothing
                objEncolarRequest.encolarTransaccionRequest.tipoOperacion = TipoOperacionCambioPlanCBIO
                objEncolarRequest.encolarTransaccionRequest.nombreAplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
                objEncolarRequest.encolarTransaccionRequest.xmlDatos = strBldTransaccion
                objEncolarRequest.encolarTransaccionRequest.listaOpcional = Nothing 'No se usuara
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("[INICIATIVA-219][CambioPlanCBIO] - [objEncolarRequest.encolarTransaccionRequest] - [{0} : {1}]", "IdNegocio", Funciones.CheckStr(objEncolarRequest.encolarTransaccionRequest.idNegocio)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("[INICIATIVA-219][CambioPlanCBIO] - [objEncolarRequest.encolarTransaccionRequest] - [{0} : {1}]", "IdNegocioAux", Funciones.CheckStr(objEncolarRequest.encolarTransaccionRequest.idNegocioAux)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("[INICIATIVA-219][CambioPlanCBIO] - [objEncolarRequest.encolarTransaccionRequest] - [{0} : {1}]", "tipoOperacion", Funciones.CheckStr(objEncolarRequest.encolarTransaccionRequest.tipoOperacion)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("[INICIATIVA-219][CambioPlanCBIO] - [objEncolarRequest.encolarTransaccionRequest] - [{0} : {1}]", "nombreAplicacion", Funciones.CheckStr(objEncolarRequest.encolarTransaccionRequest.nombreAplicacion)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("[INICIATIVA-219][CambioPlanCBIO] - [objEncolarRequest.encolarTransaccionRequest] - [{0} : {1}]", "xmlDatos", Funciones.CheckStr(objEncolarRequest.encolarTransaccionRequest.xmlDatos)))

                objAuditoriaWS.IDTRANSACCION = idTransaccion
                objAuditoriaWS.msgId = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
                objAuditoriaWS.userId = strUsuario
                objAuditoriaWS.timestamp = System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("[INICIATIVA-219][CambioPlanCBIO] - [Parametros Header Request] [{0}] -  INI", "objAuditoriaWS"))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("[INICIATIVA-219][CambioPlanCBIO] - [objAuditoriaWS] - [{0} : {1}]", "IDTRANSACCION", Funciones.CheckStr(objAuditoriaWS.IDTRANSACCION)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("[INICIATIVA-219][CambioPlanCBIO] - [objAuditoriaWS] - [{0} : {1}]", "msgId", Funciones.CheckStr(objAuditoriaWS.msgId)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("[INICIATIVA-219][CambioPlanCBIO] - [objAuditoriaWS] - [{0} : {1}]", "userId", Funciones.CheckStr(objAuditoriaWS.userId)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("[INICIATIVA-219][CambioPlanCBIO] - [objAuditoriaWS] - [{0} : {1}]", "timestamp", Funciones.CheckStr(objAuditoriaWS.timestamp)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("[INICIATIVA-219][CambioPlanCBIO] - [Parametros Header Request] [{0}] -  FIN", "objAuditoriaWS"))

                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("[INICIATIVA-219][CambioPlanCBIO] - [URL : {0}] - [nroAcuerdo : {1}] - [idTrs : {2}]", Funciones.CheckStr(ConfigurationSettings.AppSettings("EncolarReposicionRest")), Funciones.CheckStr(objEncolarRequest.encolarTransaccionRequest.idNegocio), Funciones.CheckStr(objAuditoriaWS.IDTRANSACCION)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[INICIATIVA-219][CambioPlanCBIO][Ejecutando: objEncolarCBIO.EncolarTransaccionCBIO2()]", String.Empty))

                objEncolarResponse = objEncolarCBIO.EncolarTransaccionCBIO2(objEncolarRequest, objAuditoriaWS)

                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("[INICIATIVA-219][CambioPlanCBIO] - [objEncolarResponse] - [objEncolarResponse.encolarReposicionResponse.responseAudit.codigoRespuesta: {0}] - [nroAcuerdo: {1}] - [idTrs: {2}]", Funciones.CheckStr(objEncolarResponse.encolarReposicionResponse.responseAudit.codigoRespuesta), Funciones.CheckStr(objEncolarRequest.encolarTransaccionRequest.idNegocio), Funciones.CheckStr(objAuditoriaWS.IDTRANSACCION)))
                If objEncolarResponse.encolarReposicionResponse.responseAudit.codigoRespuesta = "0" Then
                    blResultado = True
                End If
            Else
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
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIATIVA-219][CambioPlanCBIO][Ocurrio un error al ejecutar el cambio de plan en CBIO")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][CambioPlanCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
            Throw ex
        End Try
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[INICIATIVA-219][CambioPlanCBIO][blResultado]", Funciones.CheckStr(blResultado)))
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[FIN][INICIATIVA-219][CambioPlanCBIO]", String.Empty))
        Return blResultado
    End Function

    Private Function ArmarEstructuraXMLCambioPlanCBIO(ByVal strIdContrato As String, ByVal strUsuario As String, ByVal strCodigoInteraccion As String, ByVal objCliente As clsClienteBSCS, ByVal strFechaProgramacion As String, ByVal objCambioPlan As BECambioPlanCBIO, ByVal strNumeroLinea As String, ByVal strNumeroSEC As String, ByVal lstServicios As ArrayList, ByVal lstBonos As ArrayList)
        Dim strBldTransaccion As New System.Text.StringBuilder
        Dim cantServicios As Integer = 0
        Dim cantBonos As Integer = 0
        Dim objListaServicios As New ArrayList
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[INICIO][INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO]", String.Empty))
        Try
            strBldTransaccion.Append("<![CDATA[<?xml version=""1.0"" encoding=""UTF-8""?>\n")
            strBldTransaccion.Append("<ProgramarMigracionPlanRequestType>\n")
            strBldTransaccion.Append("<idNegocio>" + Funciones.CheckStr(strIdContrato) + "</idNegocio>\n")
            strBldTransaccion.Append("<aplicacion>" + "SISACT" + "</aplicacion>\n")
            strBldTransaccion.Append("<usuarioAplicacion>" + "USRSISACT" + "</usuarioAplicacion>\n")
            strBldTransaccion.Append("<usuarioSistema>" + Funciones.CheckStr(strUsuario) + "</usuarioSistema>\n")
            strBldTransaccion.Append("<descCoSer>" + "CAMBIO DE PLAN + RENOVACION" + "</descCoSer>\n")
            strBldTransaccion.Append("<codigoInteraccion>" + Funciones.CheckStr(strCodigoInteraccion) + "</codigoInteraccion>\n")
            strBldTransaccion.Append("<nroCuenta>" + Funciones.CheckStr(objCliente.cuenta) + "</nroCuenta>\n")
            strBldTransaccion.Append("<fechaProgramacion>" + Funciones.CheckStr(strFechaProgramacion) + "</fechaProgramacion>\n")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][strBldTransaccion][Datos Cabecera]", Funciones.CheckStr(strBldTransaccion)))
            'datosMigracionPlan
            strBldTransaccion.Append("<datosMigracionPlan>\n")
            strBldTransaccion.Append("<flagAplicaOCC>" + "1" + "</flagAplicaOCC>\n")
            strBldTransaccion.Append("<montoOCC>" + String.Empty + "</montoOCC>\n")
            strBldTransaccion.Append("<canal>" + "CAC" + "</canal>\n")
            strBldTransaccion.Append("<codigoPuntoVenta>" + Funciones.CheckStr(objCambioPlan.CodigoOficinaVenta) + "</codigoPuntoVenta>\n")
            strBldTransaccion.Append("<sec>" + Funciones.CheckStr(strNumeroSEC) + "</sec>\n")
            strBldTransaccion.Append("<tipoOperacion>" + Funciones.CheckStr(objCambioPlan.TipoOperacion) + "</tipoOperacion>\n")
            strBldTransaccion.Append("<fechaCreSolicitud>" + Funciones.CheckStr(objCambioPlan.FechaSolicitudReno) + "</fechaCreSolicitud>\n")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][strBldTransaccion][Otros]", "OK"))
            'DatosCliente
            strBldTransaccion.Append("<datosCliente>\n")
            strBldTransaccion.Append("<csId>" + Funciones.CheckStr(objCliente.customerId) + "</csId>\n")
            strBldTransaccion.Append("<csIdPub>" + Funciones.CheckStr(objCliente.customer_id_pub) + "</csIdPub>\n")
            strBldTransaccion.Append("<customerSegment>" + Funciones.CheckStr(objCambioPlan.SegmentoCliente) + "</customerSegment>\n")
            strBldTransaccion.Append("<customerBehaviorPaid>" + Funciones.CheckStr(objCambioPlan.ComportamientoPago) + "</customerBehaviorPaid>\n")
            strBldTransaccion.Append("<customerBillingAccountCode>" + Funciones.CheckStr(objCliente.billingAccountId) + "</customerBillingAccountCode>\n")
            strBldTransaccion.Append("<customerBillCycleId>" + Funciones.CheckStr(objCliente.ciclo_fac) + "</customerBillCycleId>\n")
            strBldTransaccion.Append("</datosCliente>\n")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][strBldTransaccion][Datos Cliente]", "OK"))
            'DatosLinea
            strBldTransaccion.Append("<datosLinea>\n")
            strBldTransaccion.Append("<msisdn>" + Funciones.CheckStr(strNumeroLinea) + "</msisdn>\n")
            strBldTransaccion.Append("<coId>" + Funciones.CheckStr(objCliente.co_id) + "</coId>\n")
            strBldTransaccion.Append("<coIdPub>" + Funciones.CheckStr(objCliente.co_id_pub) + "</coIdPub>\n")
            strBldTransaccion.Append("<especialCase>" + Funciones.CheckStr(objCambioPlan.CasoEspecial) + "</especialCase>\n")
            strBldTransaccion.Append("<campain>" + Funciones.CheckStr(objCambioPlan.Campana) + "</campain>\n")
            strBldTransaccion.Append("<tope>" + Funciones.CheckStr(objCambioPlan.TopeConsumo) + "</tope>\n")
            strBldTransaccion.Append("<suscripcionCorreo>" + IIf(Funciones.CheckStr(objCambioPlan.FlagCorreo).Equals(""), "0", "1") + "</suscripcionCorreo>\n")
            strBldTransaccion.Append("<marcaEquipo>" + Funciones.CheckStr(objCambioPlan.MarcaEquipo) + "</marcaEquipo>\n")
            strBldTransaccion.Append("<modeloEquipo>" + Funciones.CheckStr(objCambioPlan.ModeloEquipo) + "</modeloEquipo>\n")
            strBldTransaccion.Append("<precioVentaEquipo>" + Funciones.CheckStr(objCambioPlan.PrecioVenta) + "</precioVentaEquipo>\n")
            strBldTransaccion.Append("<fechaActivacion>" + Funciones.CheckStr(objCambioPlan.FechaActivacionAlta) + "</fechaActivacion>\n")
            strBldTransaccion.Append("<fechaInicioContrato>" + Funciones.CheckStr(objCambioPlan.FechaInicioContratoActual) + "</fechaInicioContrato>\n")
            strBldTransaccion.Append("<plazoContrato>" + Funciones.CheckStr(objCambioPlan.PlazoContratoActual) + "</plazoContrato>\n")
            strBldTransaccion.Append("<tipoProducto>" + Funciones.CheckStr(objCambioPlan.TipoProducto) + "</tipoProducto>\n")
            strBldTransaccion.Append("<tipoTecnologia>" + Funciones.CheckStr(objCambioPlan.TipoTecnologia) + "</tipoTecnologia>\n")
            strBldTransaccion.Append("<tipoSuscripcion>" + Funciones.CheckStr(objCambioPlan.TipoSuscripcion) + "</tipoSuscripcion>\n")
            strBldTransaccion.Append("<modalidadPago>" + Funciones.CheckStr(objCambioPlan.ModalidadPago) + "</modalidadPago>\n")
            strBldTransaccion.Append("<poIdOld>" + Funciones.CheckStr(objCambioPlan.PoIdActual) + "</poIdOld>\n")
            strBldTransaccion.Append("<poNameOld>" + Funciones.CheckStr(objCambioPlan.PoIdActualDescripcion) + "</poNameOld>\n")
            strBldTransaccion.Append("<poIdOldCargoFijo>" + Funciones.CheckStr(objCambioPlan.CargoFijoActualPlan) + "</poIdOldCargoFijo>\n")
            strBldTransaccion.Append("<poIdNew>" + Funciones.CheckStr(objCambioPlan.PoIdNuevo) + "</poIdNew>\n")
            strBldTransaccion.Append("<poNameNew>" + Funciones.CheckStr(objCambioPlan.PoIdNuevoDescripcion) + "</poNameNew>\n")
            strBldTransaccion.Append("<poIdNewCargoFijo>" + Funciones.CheckStr(objCambioPlan.CargoFijoNuevoPlan) + "</poIdNewCargoFijo>\n")
            strBldTransaccion.Append("</datosLinea>\n")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][strBldTransaccion][Datos Linea]", "OK"))
            'SERVICIOS ADICIONALES DEL NUEVO PLAN PARA ACTIVAR
            If Not IsNothing(lstServicios) Then
                If lstServicios.Count > 0 Then
                    For Each strServicio As String In lstServicios
                        cantServicios += 1
                        strBldTransaccion.Append("<serviciosAdicionales>\n")
                        strBldTransaccion.Append("<codigoServicio>" + Funciones.CheckStr(strServicio) + "</codigoServicio>\n")
                        strBldTransaccion.Append("<estado>" + "A" + "</estado>\n")
                        strBldTransaccion.Append("<tipoServicio>" + Funciones.CheckStr(ConfigurationSettings.AppSettings("CodTipoServicioAdicional")) + "</tipoServicio>\n")
                        strBldTransaccion.Append("</serviciosAdicionales>\n")
                    Next
                    objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][Cantidad de Servicios adicionales a activar]", Funciones.CheckStr(cantServicios)))
                    objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][strBldTransaccion][Servicios Adicionales]", "OK"))
                    cantServicios = 0
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][INPUT][serviciosAdicionales]", "No tiene Servicios adicionales por activar"))
            End If
            'BONOS COMPATIBLES CON EL NUEVO PLAN NUEVO PARA ACTIVAR
            If Not IsNothing(lstBonos) Then
                If lstBonos.Count > 0 Then
                    For Each strBonos As String In lstBonos
                        cantBonos += 1
                        strBldTransaccion.Append("<serviciosAdicionales>\n")
                        strBldTransaccion.Append("<codigoServicio>" + Funciones.CheckStr(strBonos) + "</codigoServicio>\n")
                        strBldTransaccion.Append("<tipoServicio>" + Funciones.CheckStr(ConfigurationSettings.AppSettings("CodTipoBonoCompatible")) + "</tipoServicio>\n")
                        strBldTransaccion.Append("</serviciosAdicionales>\n")
                    Next
                    objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][Cantidad de Bonos compatibles a activar]", Funciones.CheckStr(cantBonos)))
                    objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][strBldTransaccion][Servicios Adicionales]", "OK"))
                    cantBonos = 0
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][ArmarEstructuraXMLCambioPlanCBIO][INPUT][BonosCompatibles]", "No tiene Bonos compatibles por activar"))
            End If
            'TOPE DE CONSUMO
            strBldTransaccion.Append("<serviciosAdicionales>\n")
            strBldTransaccion.Append("<categoriaServicio>" + "TOPE DE CONSUMO" + "</categoriaServicio>\n")
            strBldTransaccion.Append("<codigoServicio>" + "CreditLimitType" + "</codigoServicio>\n")
            strBldTransaccion.Append("<estado>" + "A" + "</estado>\n")
            strBldTransaccion.Append("<tipoServicio>" + "CARACTERISTICA" + "</tipoServicio>\n")
            strBldTransaccion.Append("<valorServicio>" + Funciones.CheckStr(objCambioPlan.TopeConsumo) + "</valorServicio>\n")
            strBldTransaccion.Append("</serviciosAdicionales>\n")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][strBldTransaccion][Servicios Adicionales-Tope Consumo]", "OK"))
            'MONTO TOPE DE CONSUMO
            strBldTransaccion.Append("<serviciosAdicionales>\n")
            strBldTransaccion.Append("<categoriaServicio>" + "TOPE DE CONSUMO" + "</categoriaServicio>\n")
            strBldTransaccion.Append("<codigoServicio>" + "CreditLimitUsageThreshold_7000" + "</codigoServicio>\n")
            strBldTransaccion.Append("<estado>" + "A" + "</estado>\n")
            strBldTransaccion.Append("<tipoServicio>" + "CARACTERISTICA" + "</tipoServicio>\n")
            strBldTransaccion.Append("<valorServicio>" + Funciones.CheckStr(objCambioPlan.MontoTopeConsumo) + "</valorServicio>\n")
            strBldTransaccion.Append("</serviciosAdicionales>\n")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][strBldTransaccion][Servicios Adicionales-Monto Tope Consumo]", "OK"))
            strBldTransaccion.Append("</datosMigracionPlan>\n")
            strBldTransaccion.Append("</ProgramarMigracionPlanRequestType>\n")
            strBldTransaccion.Append("]]>")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][strBldTransaccion][Resultado XML]", Funciones.CheckStr(strBldTransaccion)))
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][Ocurrio un error al ejecutar el cambio de plan en CBIO")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
            Throw ex
        End Try
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}{1}", "[FIN][INICIATIVA-219][ArmarEstructuraXMLCambioPlanCBIO]", String.Empty))
        Return Funciones.CheckStr(strBldTransaccion)
    End Function

    Public Function ActualizarTransaccionVentaCBIO(ByVal detalleVenta As String, ByRef strCodigoRespuesta As String, ByRef strMensajeRespuesta As String)

        Dim objClsActivacionCBIO As New ClsActivacionCBIO

        Try
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIO][INICIATIVA-219][ActualizarTransaccionVentaCBIO]")

            Dim datos() As String = detalleVenta.Split(",")

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarTransaccionVentaCBIO][INPUT][PI_ID_NEGOCIO]", Funciones.CheckStr(datos(0))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarTransaccionVentaCBIO][INPUT][PI_ID_NEGOCIO_AUX]", Funciones.CheckStr(datos(1))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarTransaccionVentaCBIO][INPUT][PI_TIPO_TRANSACCION]", Funciones.CheckStr(datos(2))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarTransaccionVentaCBIO][INPUT][PI_USUARIO]", Funciones.CheckStr(datos(3))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarTransaccionVentaCBIO][INPUT][PI_ESTADO]", Funciones.CheckStr(datos(4))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarTransaccionVentaCBIO][INPUT][PI_OBSERVACION]", Funciones.CheckStr(datos(5))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarTransaccionVentaCBIO][INPUT][PI_CSID_PUB]", Funciones.CheckStr(datos(6))))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarTransaccionVentaCBIO][INPUT][PI_COID_PUB]", Funciones.CheckStr(datos(7))))

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarTransaccionVentaCBIO][EJECUTANDO SP]", "SISACTSU_TRANSACCION"))

            objClsActivacionCBIO.ActualizarTransaccionVentaCBIO(detalleVenta, strCodigoRespuesta, strMensajeRespuesta)

            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarTransaccionVentaCBIO][OUTPUT][PO_CODIGO_RESPUESTA]", Funciones.CheckStr(strCodigoRespuesta)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActualizarTransaccionVentaCBIO][OUTPUT][PO_MENSAJE_RESPUESTA]", Funciones.CheckStr(strMensajeRespuesta)))


        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIATIVA-219][ActualizarTransaccionVentaCBIO][Ocurrio un error al actualizar el estado de la transaccion venta")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][ActualizarTransaccionVentaCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
        End Try

    End Function
    'FIN INICIATIVA-219

    Public Function ConsultarProgramacionesCBIO(ByVal objRequest As MessageRequestConsultarProgramaciones, ByVal blExitoSer As Boolean, ByVal blTieneProgPendiente As Boolean)

        objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}{1}", "[INICIO][INICIATIVA-219][ConsultarProgramacionesCBIO]", String.Empty))

        Dim objWSConsultarProgramacionesCBIO As New BWConsultarProgramacionesCBIO
        Dim objResponseConsultarProgramaciones As New MessageResponseConsultarProgramaciones
        Dim strCodigoRespuesta As String = String.Empty
        blExitoSer = False
        blTieneProgPendiente = False

        objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][INPUT][MSISDN]", Funciones.CheckStr(objRequest.consultarProgramacionesRequest.msisdn)))
        objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][INPUT][FECHADESDE]", Funciones.CheckStr(objRequest.consultarProgramacionesRequest.fechaDesde)))
        objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][INPUT][FECHAHASTA]", Funciones.CheckStr(objRequest.consultarProgramacionesRequest.fechaHasta)))
        objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][INPUT][CUENTA]", Funciones.CheckStr(objRequest.consultarProgramacionesRequest.cuenta)))
        objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][INPUT][TIPOTRANSACCION]", Funciones.CheckStr(objRequest.consultarProgramacionesRequest.tipoTransaccion)))
        objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][INPUT][CODIGOINTERACCION]", Funciones.CheckStr(objRequest.consultarProgramacionesRequest.codigoInteraccion)))
        objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][INPUT][ASESOR]", Funciones.CheckStr(objRequest.consultarProgramacionesRequest.asesor)))
        objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][INPUT][CADDAC]", Funciones.CheckStr(objRequest.consultarProgramacionesRequest.cadDac)))

        Try

            objResponseConsultarProgramaciones = objWSConsultarProgramacionesCBIO.ConsultarProgramacionesWSCBIO(objRequest)

            strCodigoRespuesta = Funciones.CheckStr(objResponseConsultarProgramaciones.consultarProgramacionesResponse.responseAudit.codigoRespuesta)

            If strCodigoRespuesta = "0" Then
                blExitoSer = True
            End If

            If Not IsNothing(objResponseConsultarProgramaciones) Then

                If Not IsNothing(objResponseConsultarProgramaciones.consultarProgramacionesResponse) Then

                    If Not IsNothing(objResponseConsultarProgramaciones.consultarProgramacionesResponse.responseData) Then

                        If Not IsNothing(objResponseConsultarProgramaciones.consultarProgramacionesResponse.responseData.listaProgramaciones) Then

                            If objResponseConsultarProgramaciones.consultarProgramacionesResponse.responseData.listaProgramaciones.Length > 0 Then
                                blTieneProgPendiente = True
                                objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][ListaProgramaciones]", "Se encontraron programaciones pendientes"))
                            Else
                                objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][ListaProgramaciones]", "No se encontraron programaciones pendientes"))
                            End If

                        End If

                    End If

                End If

            End If

            Return objResponseConsultarProgramaciones

        Catch ex As Exception

            objFileLog.Log_WriteLog("", nameLogCBIO, "[INICIATIVA-219][ConsultarProgramacionesCBIO][Ocurrio un error al consultar programaciones pendientes del cliente en CBIO")
            objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][ConsultarProgramacionesCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))

        End Try

        objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}{1}", "[FIN][INICIATIVA-219][ConsultarProgramacionesCBIO]", String.Empty))

    End Function

    Public Function RollbackCambioPlanCBIO(ByVal strNumero As String, ByVal CodCambioPlan As Int64, ByVal EstCambioPlan As String)
        objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}{1}", "[INICIO][INICIATIVA-219][RollbackCambioPlanCBIO]", String.Empty))

        Dim strCodigoRespuesta As String = String.Empty
        Dim objMessageRequestBorrarProgramacion As New MessageRequestBorrarProgramacion
        Dim objBWBorrarProgramacionCBIO As New BWBorrarProgramacionCBIO
        Dim objMessageResponseBorrarProgramacion As New MessageResponseBorrarProgramacion

        objMessageRequestBorrarProgramacion.borrarProgracionRequest.msisdn = Funciones.CheckStr(strNumero)
        objMessageRequestBorrarProgramacion.borrarProgracionRequest.serviCod = Funciones.CheckStr(CodCambioPlan)
        objMessageRequestBorrarProgramacion.borrarProgracionRequest.servcEstado = Funciones.CheckStr(EstCambioPlan)

        objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][RollbackCambioPlanCBIO][INPUT][MSISDN]", Funciones.CheckStr(strNumero)))
        objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][RollbackCambioPlanCBIO][INPUT][SERVICOD]", Funciones.CheckStr(CodCambioPlan)))
        objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][RollbackCambioPlanCBIO][INPUT][SERVCESTADO]", Funciones.CheckStr(EstCambioPlan)))
        Try

            objMessageResponseBorrarProgramacion = objBWBorrarProgramacionCBIO.BorrarProgramacionWSCBIO(objMessageRequestBorrarProgramacion)

            strCodigoRespuesta = Funciones.CheckStr(objMessageResponseBorrarProgramacion.borrarProgracionResponse.responseAudit.codigoRespuesta)

            objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][RollbackCambioPlanCBIO][OUTPUT][CODIGORESPUESTA]", Funciones.CheckStr(strCodigoRespuesta)))
            objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][RollbackCambioPlanCBIO][OUTPUT][MENSAJERESPUESTA]", Funciones.CheckStr(objMessageResponseBorrarProgramacion.borrarProgracionResponse.responseAudit.mensajeRespuesta)))

            Return strCodigoRespuesta

        Catch ex As Exception

            objFileLog.Log_WriteLog("", nameLogCBIO, "[INICIATIVA-219][RollbackCambioPlanCBIO][Ocurrio un error al realizar el rollback programaciones pendientes del cliente en CBIO")
            objFileLog.Log_WriteLog("", nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][RollbackCambioPlanCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))

        End Try


    End Function

    Public Function ConsultarDatosLineaCtaWSCBIO(ByVal strCsCode As String, ByVal strNumeroLinea As String) As ResponseConsultarDatosLineaCta
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIO][INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO]")
        Dim objConsultarDatosLineaCtaRequest As New RequestConsultarDatosLineaCta
        Dim objConsultarDatosLineaCtaResponse As New ResponseConsultarDatosLineaCta
        Dim strTransactionId As String = String.Empty
        Dim strCodigoRespuesta As String = String.Empty
        Dim strMensajeRespuesta As String = String.Empty
        Try
            'objConsultarDatosLineaCtaRequest.consultarDatosLineaCtaRequest.csCode = strCsCode 'INC000003246390
            objConsultarDatosLineaCtaRequest.consultarDatosLineaCtaRequest.dirNum = String.Format("{0}{1}", "51", strNumeroLinea)
            objConsultarDatosLineaCtaRequest.consultarDatosLineaCtaRequest.listaOpcional = Nothing
            'objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][INPUT][csCode]", Funciones.CheckStr(objConsultarDatosLineaCtaRequest.consultarDatosLineaCtaRequest.csCode))) 'INC000003246390
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][INPUT][dirNum]", Funciones.CheckStr(objConsultarDatosLineaCtaRequest.consultarDatosLineaCtaRequest.dirNum)))
            objConsultarDatosLineaCtaResponse = objCbioWS.ConsultarDatosLineaCtaWSCBIO(objConsultarDatosLineaCtaRequest)
            If Not IsNothing(objConsultarDatosLineaCtaResponse) Then
                strTransactionId = Funciones.CheckStr(objConsultarDatosLineaCtaResponse.consultarDatosLineaCtaResponse.responseAudit.idTransaccion)
                strCodigoRespuesta = Funciones.CheckStr(objConsultarDatosLineaCtaResponse.consultarDatosLineaCtaResponse.responseAudit.codigoRespuesta)
                strMensajeRespuesta = Funciones.CheckStr(objConsultarDatosLineaCtaResponse.consultarDatosLineaCtaResponse.responseAudit.mensajeRespuesta)
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][OUTPUT][strTransactionId]", Funciones.CheckStr(strTransactionId)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][OUTPUT][strCodigoRespuesta]", Funciones.CheckStr(strCodigoRespuesta)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][OUTPUT][strMensajeRespuesta]", Funciones.CheckStr(strMensajeRespuesta)))
            Else
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][objConsultarDatosLineaCtaResponse]", "Nulo o Vacio"))
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][Ocurrio un error al consultar los Datos de la Linra en CBIO")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
        End Try
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[FIN][INICIATIVA-219][ConsultarDatosLineaCtaWSCBIO]")
        Return objConsultarDatosLineaCtaResponse
    End Function

    Public Function ActivarServiciosAdicionalesCBIO(ByVal strLinea As String, ByVal strBillCycleId As String, ByVal strCodIdPub As String, ByVal strCsIdPub As String, _
                                                    ByVal strCsId As String, ByVal strCodTransaction As String, ByVal strPoType As String, ByVal strProductOfferingId As String, _
                                                    ByVal strAction As String, ByVal strNroAcuerdo As String) As Boolean
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIO][INICIATIVA-219][ActivarServiciosAdicionalesCBIO]")
        Dim objServicioAdicionalRequest As New ActivarServiciosAdicionalesRequest
        Dim objServicioAdicionalResponse As New ActivarServiciosAdicionalesResponse
        Dim strOrderId As String = String.Empty
        Dim strCodigoRespuesta As String = String.Empty
        Dim strMensajeRespuesta As String = String.Empty
        Dim resp As Boolean = False
        Try
            objServicioAdicionalRequest.linea = strLinea
            objServicioAdicionalRequest.billCycleId = strBillCycleId
            objServicioAdicionalRequest.coIdPub = strCodIdPub
            objServicioAdicionalRequest.csIdPub = strCsIdPub
            objServicioAdicionalRequest.csId = strCsId
            objServicioAdicionalRequest.codTransaction = strCodTransaction
            objServicioAdicionalRequest.poType = strPoType
            objServicioAdicionalRequest.productOfferingId = strProductOfferingId
            objServicioAdicionalRequest.action = strAction
            objServicioAdicionalRequest.nroAcuerdo = strNroAcuerdo
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][INPUT][linea]", Funciones.CheckStr(objServicioAdicionalRequest.linea)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][INPUT][billCycleId]", Funciones.CheckStr(objServicioAdicionalRequest.billCycleId)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][INPUT][coIdPub]", Funciones.CheckStr(objServicioAdicionalRequest.coIdPub)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][INPUT][csIdPub]", Funciones.CheckStr(objServicioAdicionalRequest.csIdPub)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][INPUT][csId]", Funciones.CheckStr(objServicioAdicionalRequest.csId)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][INPUT][codTransaction]", Funciones.CheckStr(objServicioAdicionalRequest.codTransaction)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][INPUT][poType]", Funciones.CheckStr(objServicioAdicionalRequest.poType)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][INPUT][productOfferingId]", Funciones.CheckStr(objServicioAdicionalRequest.productOfferingId)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][INPUT][action]", Funciones.CheckStr(objServicioAdicionalRequest.action)))
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][INPUT][nroAcuerdo]", Funciones.CheckStr(objServicioAdicionalRequest.nroAcuerdo)))
            objServicioAdicionalResponse = objCbioWS.ActivarServiciosAdicionalesWSCBIO(objServicioAdicionalRequest)
            If Not IsNothing(objServicioAdicionalResponse) Then
                strOrderId = Funciones.CheckStr(objServicioAdicionalResponse.orderID)
                strCodigoRespuesta = Funciones.CheckStr(objServicioAdicionalResponse.codigoRespuesta)
                strMensajeRespuesta = Funciones.CheckStr(objServicioAdicionalResponse.mensajeRespuesta)
                resp = IIf(strCodigoRespuesta.Equals("0"), True, False)
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][OUTPUT][strOrderId]", Funciones.CheckStr(strOrderId)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][OUTPUT][strCodigoRespuesta]", Funciones.CheckStr(strCodigoRespuesta)))
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][OUTPUT][strMensajeRespuesta]", Funciones.CheckStr(strMensajeRespuesta)))
            Else
                objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0}-->{1}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][objServicioAdicionalResponse]", "Nulo o Vacio"))
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][Ocurrio un error al activar los servicios adicionales en CBIO")
            objFileLog.Log_WriteLog(pathFile, nameLogCBIO, String.Format("{0} {1} | {2}", "[INICIATIVA-219][ActivarServiciosAdicionalesCBIO][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)))
        End Try
        objFileLog.Log_WriteLog(pathFile, nameLogCBIO, "[FIN][INICIATIVA-219][ActivarServiciosAdicionalesCBIO]")
        Return resp
    End Function
    'FIN: INICIATIVA-219

End Class
