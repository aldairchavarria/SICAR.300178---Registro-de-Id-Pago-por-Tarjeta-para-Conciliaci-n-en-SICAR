Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB

Public Class ClsActivacionCBIO

    Dim objSeg As New Seguridad_NET.clsSeguridad
    Dim strCadenaConexion As String

    Dim PKG_SISACT_GENERAL_CBIO As String = "SISACT_PKG_GENERAL_CBIO"
    Dim SISACT_PKG_PROMO_COMBO_CLARO As String = "SISACT_PKG_PROMO_COMBO_CLA_v2" 'CAMPANIA NAVIADAD COMBO
    Dim SISACT_PKG_PROMO_PREPAGO As String = "SISACT_PKG_COMBO_PREPAGO_V2" 'CAMPAÑA COMBO PREPAGO

    'INI: INICIATIVA-219
    Public Function consultaTransaccion(ByVal idNegocioAux As String, ByVal idTransaccion As String, ByRef strCodResp As String, ByRef strMsgResp As String) As ArrayList

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("PI_ID_NEGOCIO", DbType.Int64, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_ID_NEGOCIO_AUX", DbType.String, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_TIPO_TRANSACCION", DbType.String, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_ESTADO", DbType.String, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_FLAG_ACTUALIZA", DbType.String, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(1).Value = idNegocioAux 'Telefono
        arrParam(2).Value = idTransaccion 'Tipo de operacion pvu
        arrParam(3).Value = "0"
        Dim dr As IDataReader = Nothing
        Dim arrListaNum As New ArrayList
        Dim item As ItemGenerico
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_SISACT_GENERAL_CBIO + ".SISACTSS_TRANSACCION"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            While (dr.Read())
                item = New ItemGenerico
                item.CODIGO = dr("SOACN_ID_NEGOCIO")
                item.CODIGO2 = dr("SOACN_ID_CODIGO_AUX")
                arrListaNum.Add(item)
            End While

            strCodResp = CType(objRequest.Parameters(5), IDataParameter).Value
            strMsgResp = CType(objRequest.Parameters(6), IDataParameter).Value

        Catch ex As Exception
            arrListaNum = Nothing
            strCodResp = "-99"
            strMsgResp = ex.Message.ToString
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return arrListaNum

    End Function

'PBI000002155923
    Public Function consultaTransaccion1(ByVal idNegocio As String, ByVal idNegocioAux As String, ByVal idTransaccion As String, ByRef strCodResp As String, ByRef strMsgResp As String) As ArrayList

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("PI_ID_NEGOCIO", DbType.Int64, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_ID_NEGOCIO_AUX", DbType.String, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_TIPO_TRANSACCION", DbType.String, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_ESTADO", DbType.String, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_FLAG_ACTUALIZA", DbType.String, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(0).Value = idNegocio 'NroAcuerdo

        arrParam(1).Value = idNegocioAux 'Telefono
        arrParam(2).Value = idTransaccion 'Tipo de operacion pvu
        arrParam(3).Value = "0"
        Dim dr As IDataReader = Nothing
        Dim arrListaNum As New ArrayList
        Dim item As ItemGenerico
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_SISACT_GENERAL_CBIO + ".SISACTSS_TRANSACCION"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            While (dr.Read())
                item = New ItemGenerico
                item.CODIGO = dr("SOACN_ID_NEGOCIO")
                item.CODIGO2 = dr("SOACN_ID_CODIGO_AUX")
                arrListaNum.Add(item)
            End While

            strCodResp = CType(objRequest.Parameters(5), IDataParameter).Value
            strMsgResp = CType(objRequest.Parameters(6), IDataParameter).Value

        Catch ex As Exception
            arrListaNum = Nothing
            strCodResp = "-99"
            strMsgResp = ex.Message.ToString
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return arrListaNum

    End Function
'PBI000002155923
    Public Function ListarTipoDocumento()
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("P_CURSOR", DbType.Object, ParameterDirection.Output)}

        Dim dr As IDataReader = Nothing
        Dim arrListaDocumentos As New ArrayList
        Dim item As TipoDocumento

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_SISACT_GENERAL_CBIO + ".SISACTSS_TIPO_DOCUMENTO"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader

            While (dr.Read())
                item = New TipoDocumento
                item.TDOCC_CODIGO = Funciones.CheckStr(dr("DOCC_CODIGO"))
                item.ID_BSCS_IX = Funciones.CheckStr(dr("DOCC_COD_BSCS_IX"))
                arrListaDocumentos.Add(item)
            End While
        Catch ex As Exception
            Throw ex
        End Try

        Return arrListaDocumentos

    End Function

    Public Function DatosCambioPlanCBIO(ByVal NumeroSEC As Int64, ByVal NumeroAcuerdo As Int64, ByVal strNumeroLinea As String, ByRef arrServicios As ArrayList, ByRef arrBonos As ArrayList, ByRef strCodigoRespuesta As String, ByRef strMensajeRespuesta As String) As BECambioPlanCBIO
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
              New DAAB.DAABRequest.Parameter("PI_NUMERO_SEC", DbType.Int64, ParameterDirection.Input), _
              New DAAB.DAABRequest.Parameter("PI_NUMERO_ACUERDO", DbType.Int64, ParameterDirection.Input), _
              New DAAB.DAABRequest.Parameter("PI_NUMERO_TELEFONO", DbType.String, ParameterDirection.Input), _
              New DAAB.DAABRequest.Parameter("PO_CURSOR_DATOS", DbType.Object, ParameterDirection.Output), _
              New DAAB.DAABRequest.Parameter("PO_CURSOR_SERVICIOS", DbType.Object, ParameterDirection.Output), _
              New DAAB.DAABRequest.Parameter("PO_CURSOR_BONOS", DbType.Object, ParameterDirection.Output), _
              New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, 10, ParameterDirection.Output), _
              New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, 500, ParameterDirection.Output)}

        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(0).Value = Funciones.CheckInt64(NumeroSEC)
        arrParam(1).Value = Funciones.CheckInt64(NumeroAcuerdo)
        arrParam(2).Value = Funciones.CheckStr(strNumeroLinea)

        arrParam(6).Size = 10
        arrParam(7).Size = 500

        Dim objCambioPlan As New BECambioPlanCBIO
        Dim dr As IDataReader = Nothing
        arrServicios = New ArrayList
        arrBonos = New ArrayList

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_SISACT_GENERAL_CBIO + ".SISACTSS_DATOS_MIGRACION_PLAN"
            objRequest.Parameters.AddRange(arrParam)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            While (dr.Read())
                objCambioPlan.CanalVenta = Funciones.CheckStr(dr("CANAL_VENTA"))
                objCambioPlan.SegmentoCliente = Funciones.CheckStr(dr("SEGMENTO_CLIENTE"))
                objCambioPlan.TipoDocumento = Funciones.CheckStr(dr("TIPO_DOCUMENTO"))
                objCambioPlan.NumeroDocumento = Funciones.CheckStr(dr("NUMERO_DOCUMENTO"))
                objCambioPlan.FechaNacimiento = Funciones.CheckStr(dr("FECHA_NACIMIENTO"))
                objCambioPlan.GeneroCliente = Funciones.CheckStr(dr("GENERO"))
                objCambioPlan.ComportamientoPago = Funciones.CheckStr(dr("COMPORTAMIENTO_PAGO"))
                objCambioPlan.NumeroSec = Funciones.CheckStr(dr("SHOPPING_CARTID")) 'Numero SEC
                objCambioPlan.TipoOperacion = Funciones.CheckStr(dr("TIPO_OPERACION"))
                objCambioPlan.FechaSolicitudReno = Funciones.CheckStr(dr("FECHA_SOLICITUD_RENO"))
                objCambioPlan.CodigoInterlocutor = Funciones.CheckStr(dr("COD_PDV"))
                objCambioPlan.CodigoOficinaVenta = Funciones.CheckStr(dr("COD_PDV_PVU"))
                objCambioPlan.NumeroLinea = Funciones.CheckStr(dr("LINEA"))
                objCambioPlan.Iccid = Funciones.CheckStr(dr("ICCID"))
                objCambioPlan.CasoEspecial = Funciones.CheckStr(dr("CASO_ESPECIAL"))
                objCambioPlan.Campana = Funciones.CheckStr(dr("CAMPANA"))
                objCambioPlan.TopeConsumo = Funciones.CheckStr(dr("TOPE_CONSUMO"))
                objCambioPlan.MontoTopeConsumo = Funciones.CheckStr(dr("MONTO_TOPE_CONSUMO"))
                objCambioPlan.Correo = Funciones.CheckStr(dr("CORREO"))
                objCambioPlan.FlagCorreo = Funciones.CheckStr(dr("FLAG_CORREO"))
                objCambioPlan.MarcaEquipo = Funciones.CheckStr(dr("MARCA_EQUIPO"))
                objCambioPlan.ModeloEquipo = Funciones.CheckStr(dr("MODELO_EQUIPO"))
                objCambioPlan.PrecioVenta = Funciones.CheckStr(dr("PRECIO_VENTA"))
                objCambioPlan.FechaInicioContratoActual = Funciones.CheckStr(dr("FECHA_INICIO_CONTRATO_ACTUAL"))
                objCambioPlan.FechaActivacionAlta = Funciones.CheckStr(dr("FECHA_ACTIVACION"))
                objCambioPlan.TipoProducto = Funciones.CheckStr(dr("TIPO_PRODUCTO"))
                objCambioPlan.TipoTecnologia = Funciones.CheckStr(dr("TIPO_TECNOLOGIA"))
                objCambioPlan.PoIdNuevo = Funciones.CheckStr(dr("PO_ID_DESTINO"))
                objCambioPlan.PoIdNuevoDescripcion = Funciones.CheckStr(dr("PO_ID_NOMBRE_DESTINO"))
                objCambioPlan.ModalidadPago = Funciones.CheckStr(dr("MODALIDAD_PAGO"))
                objCambioPlan.CargoFijoNuevoPlan = Funciones.CheckStr(dr("CARGO_FIJO_DESTINO"))
                objCambioPlan.PlazoContratoActual = Funciones.CheckStr(dr("PLAZO_CONTRATO_ACTUAL"))
                objCambioPlan.PoIdActual = Funciones.CheckStr(dr("PO_ID_ACTUAL"))
                objCambioPlan.PoIdActualDescripcion = Funciones.CheckStr(dr("PO_ID_NOMBRE_ACTUAL"))
                objCambioPlan.CargoFijoActualPlan = Funciones.CheckStr(dr("CARGO_FIJO_ACTUAL"))
                objCambioPlan.FechaTransaccion = Funciones.CheckStr(dr("FECHA_TRANSACCION"))
                objCambioPlan.TipoSuscripcion = Funciones.CheckStr(dr("TIPO_SUSCRIPCION"))
                objCambioPlan.MontoOCC = Funciones.CheckStr(dr("MONTO_OCC"))
            End While

            dr.NextResult()

            While (dr.Read())
                arrServicios.Add(Funciones.CheckStr(dr("SERVV_PO_ID")))
            End While

            dr.NextResult()

            While (dr.Read())
                arrBonos.Add(Funciones.CheckStr(dr("TRANSV_PO_ID_BONO")))
            End While

            strCodigoRespuesta = Funciones.CheckStr(CType(objRequest.Parameters(6), IDataParameter).Value)
            strMensajeRespuesta = Funciones.CheckStr(CType(objRequest.Parameters(7), IDataParameter).Value)

        Catch ex As Exception
            arrServicios = Nothing
            strCodigoRespuesta = "-99"
            strMensajeRespuesta = ex.Message.ToString
            Throw ex
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return objCambioPlan

    End Function

    Public Function ObtenerDatosClienteCBIO(ByVal strNumeroLinea As String, ByRef strCodigoRespuesta As String, ByRef strMensajeRespuesta As String) As clsClienteBSCS
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
              New DAAB.DAABRequest.Parameter("PI_NUMERO_LINEA", DbType.String, ParameterDirection.Input), _
              New DAAB.DAABRequest.Parameter("PO_CURSOR_RENOVACION", DbType.Object, ParameterDirection.Output), _
              New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, 10, ParameterDirection.Output), _
              New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, 500, ParameterDirection.Output)}

        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(0).Value = strNumeroLinea

        Dim dr As IDataReader = Nothing
        Dim objCliente As New clsClienteBSCS

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_SISACT_GENERAL_CBIO + ".SISACTSS_TRANSACCION_RENO"
            objRequest.Parameters.AddRange(arrParam)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            While (dr.Read())
                objCliente.numero_telefono = Funciones.CheckStr(dr("TRANSV_NUMERO_LINEA"))
                objCliente.tip_doc = Funciones.CheckStr(dr("TRANSV_TIPO_DOCUMENTO"))
                objCliente.num_doc = Funciones.CheckStr(dr("TRANSV_NUMERO_DOCUMENTO"))
                objCliente.Nombre = Funciones.CheckStr(dr("TRANSV_NOMBRES"))
                objCliente.apellidos = Funciones.CheckStr(dr("TRANSV_APELLIDOS"))
                objCliente.ruc_dni = Funciones.CheckStr(dr("TRANSV_RUC_DNI"))
                objCliente.cuenta = Funciones.CheckStr(dr("TRANSV_NUMERO_CUENTA"))
                objCliente.co_id = Funciones.CheckStr(dr("TRANSV_CO_ID"))
                objCliente.co_id_pub = Funciones.CheckStr(dr("TRANSV_CO_ID_PUB"))
                objCliente.customerId = Funciones.CheckStr(dr("TRANSV_CUSTOMER_ID"))
                objCliente.customer_id_pub = Funciones.CheckStr(dr("TRANSV_CUSTOMER_ID_PUB"))
                objCliente.imsi = Funciones.CheckStr(dr("TRANSV_IMSI"))
                objCliente.ciclo_fac = Funciones.CheckStr(dr("TRANSV_CICLO_FACT"))
                objCliente.billingAccountId = Funciones.CheckStr(dr("TRANSV_BILLING_ACCOUNT_ID"))
                objCliente.productOfferingIdNew = Funciones.CheckStr(dr("TRANSV_PO_ID_PLAN_NUEVO"))
                objCliente.productOfferingIdOld = Funciones.CheckStr(dr("TRANSV_PO_ID_PLAN_ANTERIOR"))
                objCliente.tipo_cliente = Funciones.CheckStr(dr("TRANSV_TIPO_CLIENTE"))
                objCliente.codigo_tipo_cliente = Funciones.CheckStr(dr("TRANSV_CODIGO_TIPO_CLIENTE"))
                objCliente.direccion_fac = Funciones.CheckStr(dr("TRANSV_DIRECCION_FAC"))
                objCliente.urbanizacion_fac = Funciones.CheckStr(dr("TRANSV_URBANIZACION_FAC"))
                objCliente.departamento_fac = Funciones.CheckStr(dr("TRANSV_DEPARTAMENTO_FAC"))
                objCliente.provincia_fac = Funciones.CheckStr(dr("TRANSV_PROVINCIA_FAC"))
                objCliente.distrito_fac = Funciones.CheckStr(dr("TRANSV_DISTRITO_FAC"))
            End While

            strCodigoRespuesta = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)
            strMensajeRespuesta = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            strCodigoRespuesta = "-99"
            strMensajeRespuesta = ex.Message.ToString
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return objCliente

    End Function

    Public Function ActualizarTransaccionVentaCBIO(ByVal detalleVenta As String, ByRef strCodigoRespuesta As String, ByRef strMensajeRespuesta As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim datos() As String = detalleVenta.Split(",")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
               New DAAB.DAABRequest.Parameter("PI_ID_NEGOCIO", DbType.String, 20, ParameterDirection.Input), _
               New DAAB.DAABRequest.Parameter("PI_ID_NEGOCIO_AUX", DbType.String, 10, ParameterDirection.Input), _
               New DAAB.DAABRequest.Parameter("PI_TIPO_TRANSACCION", DbType.String, 2, ParameterDirection.Input), _
               New DAAB.DAABRequest.Parameter("PI_USUARIO", DbType.String, 20, ParameterDirection.Input), _
               New DAAB.DAABRequest.Parameter("PI_ESTADO", DbType.String, 2, ParameterDirection.Input), _
               New DAAB.DAABRequest.Parameter("PI_OBSERVACION", DbType.String, 1000, ParameterDirection.Input), _
               New DAAB.DAABRequest.Parameter("PI_CSID_PUB", DbType.String, 20, ParameterDirection.Input), _
               New DAAB.DAABRequest.Parameter("PI_COID_PUB", DbType.String, 20, ParameterDirection.Input), _
               New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, 3, ParameterDirection.Output), _
               New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, 100, ParameterDirection.Output)}


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        Dim j As Integer
        For j = 0 To datos.Length - 1
            arrParam(j).Value = Funciones.CheckStr(datos(j))
        Next

        Dim dr As IDataReader = Nothing

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_SISACT_GENERAL_CBIO + ".SISACTSU_TRANSACCION"
            objRequest.Parameters.AddRange(arrParam)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            strCodigoRespuesta = CType(objRequest.Parameters(8), IDataParameter).Value
            strMensajeRespuesta = CType(objRequest.Parameters(9), IDataParameter).Value

        Catch ex As Exception
            strCodigoRespuesta = "-99"
            strMensajeRespuesta = ex.Message.ToString
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

    End Function

    Public Function ListarRequestDesactivacionContrato(ByVal strTelefono As String, ByRef strCodResp As String, ByRef strMsgResp As String) As ArrayList

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("PI_TELEFONO", DbType.String, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_DESACTIV_CONTRATO", DbType.Object, ParameterDirection.Output)}

        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(0).Value = strTelefono 'Telefono

        Dim dr As IDataReader = Nothing

        Dim arrListaNum As New ArrayList
        Dim item As BERequestDesactivacionContratoCBIO

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_SISACT_GENERAL_CBIO + ".SISACTSS_DESACTIV_CONTRATO"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            While (dr.Read())

                item = New BERequestDesactivacionContratoCBIO

                item.SOACV_CSID_PUB = Funciones.CheckStr(dr("SOACV_CSID_PUB"))
                item.SOACV_COID_PUB = Funciones.CheckStr(dr("SOACV_COID_PUB"))
                item.CONTC_ESTADO = Funciones.CheckStr(dr("CONTC_ESTADO"))
                item.CONTN_CUSTOMER_ID = Funciones.CheckStr(dr("CONTN_CUSTOMER_ID"))
                item.CONTV_OFICINA_VENTA_DESC = Funciones.CheckStr(dr("CONTV_OFICINA_VENTA_DESC"))
                item.CONTV_COD_BSCS = Funciones.CheckStr(dr("CONTV_COD_BSCS"))
                item.CONTV_VENDEDOR = Funciones.CheckStr(dr("CONTV_VENDEDOR"))
                item.CONTV_NOMBRE_USUARIO = Funciones.CheckStr(dr("CONTV_NOMBRE_USUARIO"))
                item.CONTV_NOMBRE = Funciones.CheckStr(dr("CONTV_NOMBRE"))
                item.CONTV_APE_MAT = Funciones.CheckStr(dr("CONTV_APE_MAT"))
                item.CONTV_APE_PAT = Funciones.CheckStr(dr("CONTV_APE_PAT"))
                item.CONTV_NRO_DOC_CLIENTE = Funciones.CheckStr(dr("CONTV_NRO_DOC_CLIENTE"))
                item.CONTN_NUMERO_CONTRATO = Funciones.CheckStr(dr("CONTN_NUMERO_CONTRATO"))
                item.CICLOFACTURACION = Funciones.CheckStr(dr("CICLOFACTURACION"))
                item.PLAN_TARIFAR = Funciones.CheckStr(dr("PLAN_TARIFAR"))
                item.SOLIN_CODIGO = Funciones.CheckStr(dr("SOLIN_CODIGO"))
                item.CO_ID = Funciones.CheckStr(dr("CO_ID"))
                item.IMEI19 = Funciones.CheckStr(dr("IMEI19"))
                item.TELEFONO = Funciones.CheckStr(dr("TELEFONO"))
                item.COD_EQUIPO = Funciones.CheckStr(dr("COD_EQUIPO"))
                item.DES_EQUIPO = Funciones.CheckStr(dr("DES_EQUIPO"))
                item.SERIE_EQUIPO = Funciones.CheckStr(dr("SERIE_EQUIPO"))
                item.PLNV_PO_ID = Funciones.CheckStr(dr("PLNV_PO_ID"))
                item.CORREO = Funciones.CheckStr(dr("CORREO"))

                arrListaNum.Add(item)

            End While

            strCodResp = CType(objRequest.Parameters(1), IDataParameter).Value
            strMsgResp = CType(objRequest.Parameters(2), IDataParameter).Value

        Catch ex As Exception
            arrListaNum = Nothing
            strCodResp = "-99"
            strMsgResp = ex.Message.ToString
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return arrListaNum

    End Function

    Public Function RollBackTransaccionCBIO(ByVal idNegocio As Int64, ByVal tipoTransaccion As String, ByRef codRespuesta As String, ByRef msjRespuesta As String) As Boolean
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("PI_ID_NEGOCIO", DbType.Int64, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_TIPO_TRANSACCION", DbType.String, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output)}
        Dim i As Integer
        Dim salida As Boolean = False
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next
        arrParam(0).Value = idNegocio
        arrParam(1).Value = tipoTransaccion
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_SISACT_GENERAL_CBIO + ".SISACTSD_TRANSACCION"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Transactional = True
            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Factory.CommitTransaction()
            codRespuesta = CType(objRequest.Parameters(2), IDataParameter).Value
            msjRespuesta = CType(objRequest.Parameters(3), IDataParameter).Value
            If (String.Equals(codRespuesta, "0")) Then
                salida = True
            End If
        Catch ex As Exception
            codRespuesta = "-99"
            msjRespuesta = ex.Message.ToString
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return salida
    End Function
    'FIN: INICIATIVA-219

    'INICIATIVA-710 - INICIO
    Public Function ValidaPagoComboPrepago(ByVal nroPedido As String, ByRef codRespuesta As String, ByRef msjRespuesta As String) As Boolean
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("PI_PROPN_PEDIDO", DbType.String, 15, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, 10, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, 500, ParameterDirection.Output)}
        Dim i As Integer
        Dim salida As Boolean = False
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        If (nroPedido > 0) Then
            arrParam(0).Value = nroPedido
            arrParam(1).Size = 10
            arrParam(2).Size = 500
        End If

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = SISACT_PKG_PROMO_PREPAGO + ".SISACTSS_VALIDA_PAGO_COMBO"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
            codRespuesta = Funciones.CheckStr(CType(objRequest.Parameters(1), IDataParameter).Value)
            msjRespuesta = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)
            If (String.Equals(codRespuesta, "0")) Then
                salida = True
        End If
        Catch ex As Exception
            codRespuesta = "-99"
            msjRespuesta = ex.Message.ToString
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return salida
    End Function

    Public Function ValidaAnulacionComboPrepago(ByVal nroPedido As String, ByRef codRespuesta As String, ByRef msjRespuesta As String) As Boolean
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("PI_PROPN_PEDIDO", DbType.String, 15, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, 10, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, 500, ParameterDirection.Output)}
        Dim i As Integer
        Dim salida As Boolean = False
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        If (nroPedido > 0) Then
            arrParam(0).Value = nroPedido
            arrParam(1).Size = 10
            arrParam(2).Size = 500
        End If

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = SISACT_PKG_PROMO_PREPAGO + ".sisactss_val_anulacion"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Transactional = True
            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Factory.CommitTransaction()
            codRespuesta = Funciones.CheckStr(CType(objRequest.Parameters(1), IDataParameter).Value)
            msjRespuesta = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)
            If (String.Equals(codRespuesta, "0")) Then
                salida = True
            End If
        Catch ex As Exception
            codRespuesta = "-99"
            msjRespuesta = ex.Message.ToString
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return salida
    End Function

    Public Function AnulaCombosPrepago(ByVal nroPedido As String, ByRef codRespuesta As String, ByRef msjRespuesta As String) As Boolean
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("PI_PROPN_PEDIDO", DbType.String, 15, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, 10, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, 500, ParameterDirection.Output)}
        Dim i As Integer
        Dim salida As Boolean = False
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        If (nroPedido > 0) Then
            arrParam(0).Value = nroPedido
            arrParam(1).Size = 10
            arrParam(2).Size = 500
        End If

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = SISACT_PKG_PROMO_PREPAGO + ".SISACTSD_PROMOCION_PREPAGO"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Transactional = True
            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Factory.CommitTransaction()
            codRespuesta = Funciones.CheckStr(CType(objRequest.Parameters(1), IDataParameter).Value)
            msjRespuesta = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)
            If (String.Equals(codRespuesta, "0")) Then
                salida = True
            End If
        Catch ex As Exception
            codRespuesta = "-99"
            msjRespuesta = ex.Message.ToString
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return salida
    End Function

    Public Function ValidaAnulacionComboPostpago(ByVal nroPedido As String, ByRef codRespuesta As String, ByRef msjRespuesta As String) As Boolean
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("PI_COMBN_NROPEDIDO", DbType.String, 15, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, 10, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, 500, ParameterDirection.Output)}
        Dim i As Integer
        Dim salida As Boolean = False
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        If (nroPedido > 0) Then
            arrParam(0).Value = nroPedido
            arrParam(1).Size = 10
            arrParam(2).Size = 500
        End If

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = SISACT_PKG_PROMO_COMBO_CLARO + ".sisactss_val_anulacion"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Transactional = True
            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Factory.CommitTransaction()
            codRespuesta = Funciones.CheckStr(CType(objRequest.Parameters(1), IDataParameter).Value)
            msjRespuesta = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)
            If (String.Equals(codRespuesta, "0")) Then
                salida = True
            End If
        Catch ex As Exception
            codRespuesta = "-99"
            msjRespuesta = ex.Message.ToString
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return salida
    End Function

    Public Function AnulaCombosPostpago(ByVal nroPedido As String, ByRef codRespuesta As String, ByRef msjRespuesta As String) As Boolean
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("PI_COMBN_NROPEDIDO", DbType.String, 15, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("po_codigo_respuesta", DbType.String, 10, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("po_mensaje_respuesta", DbType.String, 500, ParameterDirection.Output)}
        Dim i As Integer
        Dim salida As Boolean = False
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        If (nroPedido > 0) Then
            arrParam(0).Value = nroPedido
            arrParam(1).Size = 10
            arrParam(2).Size = 500
        End If

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = SISACT_PKG_PROMO_COMBO_CLARO + ".sisactsd_promo_combo"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Transactional = True
            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Factory.CommitTransaction()
            codRespuesta = CType(objRequest.Parameters(1), IDataParameter).Value
            msjRespuesta = CType(objRequest.Parameters(2), IDataParameter).Value
            If (String.Equals(codRespuesta, "0")) Then
                salida = True
            End If
        Catch ex As Exception
            codRespuesta = "-99"
            msjRespuesta = ex.Message.ToString
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return salida
    End Function
    'INICIATIVA-710 - FIN

End Class
