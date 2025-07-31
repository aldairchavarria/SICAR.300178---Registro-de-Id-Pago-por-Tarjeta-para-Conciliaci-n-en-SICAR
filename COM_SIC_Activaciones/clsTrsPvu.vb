Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB

Public Class clsTrsPvu

    Dim strCadenaEsquema As String  'PROY-26366 - INI
    Dim objSeg As New Seguridad_NET.clsSeguridad
    Dim strCadenaConexion As String
    Dim PKGProteccionMovil As String = "PKG_TRANS_ASURION." 'PROY-31836 - Mejoras Proteccion Movil
    Dim pkgTitularidadCliente As String = "SISACT_PKG_TITULARIDAD_CLIENTE." 'PROY-140600 -  IDEA 142054 Contablizar líneas a personas extranjeras

    '************************************'
    '*** Registra la venta en PVU ***'
    '************************************'
    Public Function RegistrarVenta(ByVal p_tipo_documento As String, _
                                        ByVal p_canal As String, _
                                        ByVal p_oficina_venta As String, _
                                        ByVal p_tipo_doc_cliente As String, _
                                        ByVal p_nro_doc_cliente As String, _
                                        ByVal p_moneda As String, _
                                        ByVal p_topen_codigo As String, _
                                        ByVal p_total_venta As Double, _
                                        ByVal p_subtotal_impuesto As Double, _
                                        ByVal p_subtotal_venta As Double, _
                                        ByVal p_observacion As String, _
                                        ByVal p_tven_codigo As String, _
                                        ByVal p_numero_referencia As String, _
                                        ByVal p_usuario_crea As String, _
                                        ByVal p_numero_cuotas As String, _
                                        ByVal p_vendedor As String, _
                                        ByVal p_org_venta As String, _
                                        ByVal p_numero_sec As Int64, _
                                        ByRef p_resultado As Int64, _
                                        ByRef p_msgerr As String, _
                                        ByRef p_documento As Int64) As DataSet

        Dim dtSet As New DataTable
        Dim resultado As Int16

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_resultado", DbType.Int64, p_resultado, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("p_msgerr", DbType.String, p_msgerr, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("p_documento", DbType.Int64, p_documento, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("p_tipo_documento", DbType.String, p_tipo_documento, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_canal", DbType.String, p_canal, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_oficina_venta", DbType.String, p_oficina_venta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_tipo_doc_cliente", DbType.String, p_tipo_doc_cliente, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_nro_doc_cliente", DbType.String, p_nro_doc_cliente, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_moneda", DbType.String, p_moneda, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_topen_codigo", DbType.String, p_topen_codigo, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_total_venta", DbType.Double, p_total_venta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_subtotal_impuesto", DbType.Double, p_subtotal_impuesto, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_subtotal_venta", DbType.Double, p_subtotal_venta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_observacion", DbType.String, p_observacion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_tven_codigo", DbType.String, p_tven_codigo, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_numero_referencia", DbType.String, p_numero_referencia, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_usuario_crea", DbType.String, p_usuario_crea, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_numero_cuotas", DbType.String, p_numero_cuotas, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_vendedor", DbType.String, p_vendedor, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_org_venta", DbType.String, p_org_venta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_numero_sec", DbType.String, p_numero_sec, ParameterDirection.Input)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "sisact_pkg_acuerdo_6.sp_reg_venta"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteNonQuery(objRequest)

            p_resultado = Convert.ToInt64(CType(objRequest.Parameters(0), IDataParameter).Value)
            p_msgerr = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            p_documento = Convert.ToInt64(CType(objRequest.Parameters(2), IDataParameter).Value)

            objRequest = Nothing

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '*****************************************'
    '*** Registra detalle de venta en PVU ***'
    '*****************************************'
    Public Function RegistrarVentaDetalle(ByVal p_correlativo As Int64, _
                                            ByVal p_documento As Int64, _
                                            ByVal p_material As String, _
                                            ByVal p_material_desc As String, _
                                            ByVal p_plan As String, _
                                            ByVal p_plan_desc As String, _
                                            ByVal p_telefono As String, _
                                            ByVal p_campana As String, _
                                            ByVal p_campana_desc As String, _
                                            ByVal p_cantidad As String, _
                                            ByVal p_precio As Double, _
                                            ByVal p_descuento As Double, _
                                            ByVal p_subtotal As Double, _
                                            ByVal p_igv As Double, _
                                            ByVal p_total As Double, _
                                            ByVal p_lista_precio As String, _
                                            ByVal p_lista_precio_desc As String, _
                                            ByVal p_imei19 As String, _
                                            ByVal p_cuotas As String, _
                                            ByVal p_des_cuotas As String, _
                                            ByVal p_prdc_codigo As String, _
                                            ByVal p_resultado As Int64, _
                                            ByVal p_msgerr As String)


        Dim dtSet As New DataTable
        Dim resultado As Int16

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_resultado", DbType.Int64, p_resultado, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("p_msgerr", DbType.String, p_msgerr, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("p_correlativo", DbType.Int64, p_correlativo, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_documento", DbType.Int64, p_documento, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_material", DbType.String, p_material, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_material_desc", DbType.String, p_material_desc, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_plan", DbType.String, p_plan, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_plan_desc", DbType.String, p_plan_desc, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_telefono", DbType.String, p_telefono, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_campana", DbType.String, p_campana, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_campana_desc", DbType.String, p_campana_desc, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_cantidad", DbType.String, p_cantidad, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_precio", DbType.Double, p_precio, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_descuento", DbType.Double, p_descuento, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_subtotal", DbType.Double, p_subtotal, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_igv", DbType.Double, p_igv, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_total", DbType.Double, p_total, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_lista_precio", DbType.String, p_lista_precio, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_lista_precio_desc", DbType.String, p_lista_precio_desc, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_imei19", DbType.String, p_imei19, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_cuotas", DbType.String, p_cuotas, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_des_cuotas", DbType.String, p_des_cuotas, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_prdc_codigo", DbType.String, p_prdc_codigo, ParameterDirection.Input)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "sisact_pkg_acuerdo_6.sp_reg_venta_detalle"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteNonQuery(objRequest)

            p_resultado = Convert.ToInt64(CType(objRequest.Parameters(0), IDataParameter).Value)
            p_msgerr = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)

            objRequest = Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '******************************************'
    '*** Registrar informaciòn de Venta PVU ***'
    '******************************************'
    Public Function GrabarInfoVentaSap(ByRef p_cod_respuesta As String, _
                                        ByRef p_msj_respuesta As String, _
                                        ByVal p_id_venta As Int64, _
                                        ByVal p_nro_documento As String, _
                                        ByVal p_tipo_documento As String, _
                                        ByVal p_monto_documento As Double)


        Dim dtSet As New DataTable
        Dim resultado As Int16

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_cod_respuesta", DbType.String, p_cod_respuesta, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("p_msj_respuesta", DbType.String, p_msj_respuesta, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("p_id_venta", DbType.Int64, p_id_venta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_nro_documento", DbType.String, p_nro_documento, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_tipo_documento", DbType.String, p_tipo_documento, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_monto_documento", DbType.Double, p_monto_documento, ParameterDirection.Input)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "sisact_pkg_venta.sp_reg_info_venta_sap"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteNonQuery(objRequest)

            p_cod_respuesta = Convert.ToInt64(CType(objRequest.Parameters(0), IDataParameter).Value)
            p_msj_respuesta = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function AnularExtornarPagoDRA(ByVal PI_COD_APLI As String, _
                                        ByVal PI_USER_APLI As String, _
                                        ByVal PI_DRAV_NRO_ASOCIADO_ST As String, _
                                        ByVal PI_DRAV_DESC_TRS As String, _
                                        ByRef PO_NUM_OPE_PAGO_ANUL_EXT As Int64, _
                                        ByRef PO_COD_RPTA As Int64, _
                                        ByRef PO_MSG_RPTA As String)

        'Dim dtSet As New DataTable
        Dim resultado As Int16

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_COD_APLIC", DbType.String, PI_COD_APLI, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_USER_APLI", DbType.String, PI_USER_APLI, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_DRAV_NRO_ASOCIADO_ST", DbType.String, PI_DRAV_NRO_ASOCIADO_ST, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_DRAV_DESC_TRS", DbType.String, PI_DRAV_DESC_TRS, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_NUM_OPE_PAGO_ANUL_EXT", DbType.Int64, PO_NUM_OPE_PAGO_ANUL_EXT, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.Int64, PO_COD_RPTA, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSG_RPTA", DbType.String, PO_MSG_RPTA, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_TRANSACCION_DRA.SP_ANULAR_EXTPAGO_XCODDRAM"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteNonQuery(objRequest)

            PO_NUM_OPE_PAGO_ANUL_EXT = Funciones.CheckInt64(CType(objRequest.Parameters(4), IDataParameter).Value)
            PO_COD_RPTA = Funciones.CheckInt64(CType(objRequest.Parameters(5), IDataParameter).Value)
            PO_MSG_RPTA = Funciones.CheckStr(CType(objRequest.Parameters(6), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Anular_Pedido_RentaAdelantada(ByVal PI_COD_APLI As String, _
                                    ByVal PI_USER_APLI As String, _
                                    ByVal PI_DRAV_NRO_ASOCIADO_ST As String, _
                                    ByVal PI_DRAV_DESC_TRS As String, _
                                    ByRef PO_NUM_OPE_PAGO_ANUL_EXT As Int64, _
                                    ByRef PO_COD_RPTA As Int64, _
                                    ByRef PO_MSG_RPTA As String)

        'Dim dtSet As New DataTable
        Dim resultado As Int16

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_COD_APLIC", DbType.String, PI_COD_APLI, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_USER_APLI", DbType.String, PI_USER_APLI, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_DRAV_NRO_ASOCIADO_ST", DbType.String, PI_DRAV_NRO_ASOCIADO_ST, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_DRAV_DESC_TRS", DbType.String, PI_DRAV_DESC_TRS, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_NUM_OPE_PAGO_ANUL_EXT", DbType.Int64, PO_NUM_OPE_PAGO_ANUL_EXT, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.Int64, PO_COD_RPTA, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSG_RPTA", DbType.String, PO_MSG_RPTA, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_TRANSACCION_DRA.SP_ANULAR_EXTPAGO_POOLPAGAR"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteNonQuery(objRequest)

            PO_NUM_OPE_PAGO_ANUL_EXT = Funciones.CheckInt64(CType(objRequest.Parameters(4), IDataParameter).Value)
            PO_COD_RPTA = Funciones.CheckInt64(CType(objRequest.Parameters(5), IDataParameter).Value)
            PO_MSG_RPTA = Funciones.CheckStr(CType(objRequest.Parameters(6), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Actualizar_Prepago_AltaYPorta(ByVal I_P_ID As String, _
                                                ByVal I_P_ESTADO As String, _
                                                ByVal I_P_ALMACEN As String, _
                                                ByVal I_P_TIPO_DOC_PAGO As String, _
                                                ByVal I_P_NUMERO_REFERENCIA As String, _
                                                ByVal I_P_NUMERO_TICKET As String, _
                                                ByRef O_P_RESULTADO As Int64)

        'Dim dtSet As New DataTable
        Dim resultado As Int64

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_ID", DbType.String, I_P_ID, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, I_P_ESTADO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ALMACEN", DbType.String, I_P_ALMACEN, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_TIPO_DOC_PAGO", DbType.String, I_P_TIPO_DOC_PAGO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NUMERO_REFERENCIA", DbType.String, I_P_NUMERO_REFERENCIA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NUMERO_TICKET", DbType.String, I_P_NUMERO_TICKET, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_RESULTADO", DbType.Int64, O_P_RESULTADO, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_PREPAGO2_S6.SISACTU_VENTA_PREPAGO"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteNonQuery(objRequest)

            O_P_RESULTADO = Funciones.CheckStr(CType(objRequest.Parameters(6), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Actualizar_Prepago_RenoYRepo(ByVal I_P_DOCUMENTO_SAP As String, _
                                            ByVal I_P_TIPO_DOC_PAGO As String, _
                                            ByVal I_P_NUMERO_REFERENCIA As String, _
                                            ByVal I_P_NUMERO_TICKET As String, _
                                            ByRef O_P_VALOR_RETORNO As Int64)


        'Dim dtSet As New DataTable
        Dim resultado As Int64

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_VALOR_RETORNO", DbType.Int64, O_P_VALOR_RETORNO, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, I_P_DOCUMENTO_SAP, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_TIPO_DOC_PAGO", DbType.String, I_P_TIPO_DOC_PAGO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NUMERO_REFERENCIA", DbType.String, I_P_NUMERO_REFERENCIA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NUMERO_TICKET", DbType.String, I_P_NUMERO_TICKET, ParameterDirection.Input)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_REPOSICION_S6.SISACTSU_REG_PAGO_VENTA_REPO"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteNonQuery(objRequest)

            O_P_VALOR_RETORNO = Funciones.CheckStr(CType(objRequest.Parameters(0), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function LiberarDevolucion(ByVal nroSerie As String, _
                                      ByRef CODLOG As String, _
                                      ByRef DESLOG As String) As String


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_SERIE", DbType.String, nroSerie, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_COD_RESPUESTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_MSJ_RESPUESTA", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_EAI.SP_LIBERAR_DEVOLUCION"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            CODLOG = Funciones.CheckStr(CType(objRequest.Parameters(1), IDataParameter).Value)
            DESLOG = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'PROY-23111-IDEA-29841 - INICIO
    Public Function AnularVentaAccesorios(ByVal strNroPedido As String, ByVal strEstado As String, _
                                          ByVal strUsuario As String, ByRef strCodRpta As String, _
                                          ByRef strMsgRtpa As String) As String


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NROPEDIDO", DbType.String, strNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, strEstado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_USUARIO_MODIF", DbType.String, strUsuario, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_COD_RESPUESTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_MSJ_RESPUESTA", DbType.String, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_MANT_PROMO_ACC.SISACTSU_RELACION_DETA_VTA_ACC"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            strCodRpta = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value)
            strMsgRtpa = Funciones.CheckStr(CType(objRequest.Parameters(4), IDataParameter).Value)

        Catch ex As Exception
            Return ex.Message.ToString()
        End Try
    End Function
    'PROY-23111-IDEA-29841 - FIN

 'gdelasca Inicio Proy-9067
    Public Function ActualizarRenoAnticipada(ByVal vNroPedido As Integer, _
                                             ByVal vEstado As String, _
                                             ByVal vCodEquipo As String, _
                                             ByVal vCodMaterial As String, _
                                             ByVal vUsuActualiza As String, _
                                             ByVal vObservaciones As String, _
                                             ByRef vCodRpta As Integer, _
                                             ByRef vMsj As String) As DataSet


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim resultado As Int16

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("N_NRO_PEDIDO", DbType.Int64, vNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_NRO_CONTRATO", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_MSISDN", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_CO_ID", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_CUSTOMER_ID", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_TIPO_DOCUMENTO", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_NRO_DOCUMENTO", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_TIPO_CLIENTE", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_COD_EQUIPO", DbType.String, vCodEquipo, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_COD_MATERIAL", DbType.String, vCodMaterial, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_ESTADO", DbType.String, vEstado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_TIPO_RENOVACION", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_NRO_SEC", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_ID_ACUERDO", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("D_FECHA_ACUERDO", DbType.DateTime, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_ESTADO_ACUERDO", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_MONTO_ORIGINAL", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_MONTO_REINTEGRO", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_MONTO_FIDELIZA", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_FLAG_APLICA_REINTEGRO", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_FLAG_FIDELIZA", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("CL_DATOS_ACTUALIZA", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("CL_DATOS_ROLLBACK", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("CL_OBSERVACIONES", DbType.String, vObservaciones, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_ID_ACUERDO_SIGA", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_COD_EQUIPO_CANJE", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_COD_MATERIAL_CANJE", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_BD_ORIGEN", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_CANAL", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_USU_ACTUALIZACION", DbType.String, vUsuActualiza, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CODIGO_RESPUESTA", DbType.Int64, vCodRpta, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_MENSAJE_RESPUESTA", DbType.String, vMsj, ParameterDirection.Output)}


        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISACT_ACUERDO_RENOV.SISACTSU_ACUERDO_RENOVACION"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteNonQuery(objRequest)

            vCodRpta = Convert.ToInt64(CType(objRequest.Parameters(30), IDataParameter).Value)
            vMsj = Funciones.CheckStr(CType(objRequest.Parameters(31), IDataParameter).Value)


            objRequest = Nothing

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'Fin Proy-9067


    'gdelasca Inicio Proy-9067
    Public Function ConsultaEstRenoAnticipada(ByVal vNumPedido As Integer, _
                                                   ByRef vCurSalida As Object, _
                                                    ByRef vCodRpta As Integer, _
                                                    ByRef vMsjRpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim dsConsulta As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("N_NRO_PEDIDO", DbType.Int64, vNumPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CUR_SALIDA", DbType.Object, vCurSalida, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_CODIGO_RESPUESTA", DbType.Int64, vCodRpta, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_MENSAJE_RESPUESTA", DbType.String, vMsjRpta, ParameterDirection.Output)}


        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISACT_ACUERDO_RENOV.SISACTSS_ACUERDO_RENOVACION"
            objRequest.Parameters.AddRange(arrParam)
            dsConsulta = objRequest.Factory.ExecuteDataset(objRequest)

            ConsultaEstRenoAnticipada = dsConsulta ''Devuelve un DataSet
            vCodRpta = Convert.ToInt64(CType(objRequest.Parameters(2), IDataParameter).Value)
            vMsjRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ''FIN Proy-9067


    'gdelasca Inicio Proy-9067
    Public Function ConsultaEquipoCanje_XIdPedidoActual(ByVal vNroPedido As Integer, _
                                                    ByRef vCursorSalida As Object, _
                                                    ByRef vCod_Rpta As String, _
                                                    ByRef vMsj_Rpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        Dim dsConsulta As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, vNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("CU_CONSULTA_CANJE", DbType.Object, vCursorSalida, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, vCod_Rpta, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, vMsj_Rpta, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_CANJE.SSAPSS_CONSULTACANJE"
            objRequest.Parameters.AddRange(arrParam)
            dsConsulta = objRequest.Factory.ExecuteDataset(objRequest)


            ConsultaEquipoCanje_XIdPedidoActual = dsConsulta
            vCod_Rpta = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)
            vMsj_Rpta = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value)



        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ''gdelasca FIN Proy-9067


    'gdelasca Inicio Proy-9067
    Public Function ConsultaEquipoCanje_xIdPadre(ByVal vNroPedido As Integer, _
                                                 ByRef vCursorSalida As Object, _
                                                 ByRef vCod_Rpta As String, _
                                                 ByRef vMsj_Rpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        Dim dsConsulta As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CANJI_ID", DbType.Int64, vNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("CU_CANJE", DbType.Object, vCursorSalida, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, vCod_Rpta, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, vMsj_Rpta, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_CANJE.SSAPSS_CONSULTACANJE_X_IDPADRE"
            objRequest.Parameters.AddRange(arrParam)
            dsConsulta = objRequest.Factory.ExecuteDataset(objRequest)

            ConsultaEquipoCanje_xIdPadre = dsConsulta
            vCursorSalida = Funciones.CheckStr(CType(objRequest.Parameters(1), IDataParameter).Value)
            vCod_Rpta = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)
            vMsj_Rpta = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value)



        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'gdelasca FIN Proy-9067

    'gdelasca Inicio Proy-9067
    Public Function ConsultaEquipoCanje_xSeriTipoNum(ByVal vSerie As String, _
                                                     ByVal vTipoDoc As String, _
                                                     ByVal vNroDoc As String, _
                                                     ByRef vCurPedido As Object, _
                                                     ByRef vNroLog As String, _
                                                     ByRef vDesLog As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        Dim dsConsulta As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParame() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_SERIE", DbType.String, vSerie, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("K_TIPO_DOC", DbType.String, vTipoDoc, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("K_NRO_DOC", DbType.String, vNroDoc, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("CU_PEDIDO", DbType.Object, vCurPedido, ParameterDirection.Output), _
                                                       New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, vNroLog, ParameterDirection.Output), _
                                                       New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, vDesLog, ParameterDirection.Output)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_CANJE.SSAPSS_CONSULTAPEDIDOORIGEN"
            objRequest.Parameters.AddRange(arrParame)
            dsConsulta = objRequest.Factory.ExecuteDataset(objRequest)


            ConsultaEquipoCanje_xSeriTipoNum = dsConsulta
            vCurPedido = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value)
            vNroLog = Funciones.CheckStr(CType(objRequest.Parameters(4), IDataParameter).Value)
            vDesLog = Funciones.CheckStr(CType(objRequest.Parameters(5), IDataParameter).Value)



        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'gdelasca FIN Proy-9067
'gdelasca Inicio Proy-9067
    Public Function ActualizarRenoAnticipadaCanje(ByVal vNroPedido As Integer, _
                                             ByVal vEstado As String, _
                                             ByVal vCodEquipo As String, _
                                             ByVal vCodMaterial As String, _
                                             ByVal vUsuActualiza As String, _
                                             ByVal vObservaciones As String, _
                                             ByRef vCodRpta As Integer, _
                                             ByRef vMsj As String) As DataSet


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim resultado As Int16

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("N_NRO_PEDIDO", DbType.Int64, vNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_NRO_CONTRATO", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_MSISDN", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_CO_ID", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_CUSTOMER_ID", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_TIPO_DOCUMENTO", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_NRO_DOCUMENTO", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_TIPO_CLIENTE", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_COD_EQUIPO", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_COD_MATERIAL", DbType.String, vCodMaterial, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_ESTADO", DbType.String, vEstado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_TIPO_RENOVACION", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_NRO_SEC", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_ID_ACUERDO", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("D_FECHA_ACUERDO", DbType.DateTime, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_ESTADO_ACUERDO", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_MONTO_ORIGINAL", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_MONTO_REINTEGRO", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_MONTO_FIDELIZA", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_FLAG_APLICA_REINTEGRO", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_FLAG_FIDELIZA", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("CL_DATOS_ACTUALIZA", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("CL_DATOS_ROLLBACK", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("CL_OBSERVACIONES", DbType.String, 200, vObservaciones, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("N_ID_ACUERDO_SIGA", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_COD_EQUIPO_CANJE", DbType.String, vCodEquipo, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_COD_MATERIAL_CANJE", DbType.String, vCodMaterial, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_BD_ORIGEN", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_CANAL", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("V_USU_ACTUALIZACION", DbType.String, vUsuActualiza, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CODIGO_RESPUESTA", DbType.Int64, vCodRpta, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_MENSAJE_RESPUESTA", DbType.String, vMsj, ParameterDirection.Output)}


        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISACT_ACUERDO_RENOV.SISACTSU_ACUERDO_RENOVACION"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteNonQuery(objRequest)

            vCodRpta = Convert.ToInt64(CType(objRequest.Parameters(30), IDataParameter).Value)
            vMsj = Funciones.CheckStr(CType(objRequest.Parameters(31), IDataParameter).Value)


            objRequest = Nothing

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'Fin Proy-9067
	'Inicio Renovacion  B2E - Incidencia Tipi Cambio Plan
    Public Function ObtenerDatosTopeConsumo(ByVal vSolinCodigo As Int64, _
                                            ByRef vCodServicioTope As String, _
                                            ByRef vCargoFijoTope As String) 

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim dsConsulta As DataSet
        Dim dtDatos As DataTable
        Dim vCurSalida As Object
Try
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_SOLIN_CODIGO", DbType.Int64, vSolinCodigo, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, vCurSalida, ParameterDirection.Output)}

        

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONSULTA_GENERAL.SISACT_CON_TOPE_RENO_SEC"
            objRequest.Parameters.AddRange(arrParam)
            dsConsulta = objRequest.Factory.ExecuteDataset(objRequest)

            dtDatos = dsConsulta.Tables(0)

            vCurSalida = dsConsulta ''Devuelve un DataSet
 If dtDatos.Rows.Count > 0 Then
            vCodServicioTope = Funciones.CheckStr(dtDatos.Rows(0)("SERVC_TOPE"))
            vCargoFijoTope = Funciones.CheckStr(dtDatos.Rows(0)("PSRVN_CARGO_FIJO"))
End If

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    'Fin Renovacion  B2E - Incidencia Tipi Cambio Plan
	
    'PROY-26366 - INI
    Public Function ConsultaEquipoCanje_XIdPedidoActual_2(ByVal vNroPedido As Integer, _
                                                    ByRef vCursorSalida As Object, _
                                                    ByRef vCod_Rpta As String, _
                                                    ByRef vMsj_Rpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsConsulta As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, vNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("CU_CONSULTA_CANJE", DbType.Object, vCursorSalida, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, vCod_Rpta, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, vMsj_Rpta, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CANJE.SSAPSS_CONSULTACANJE"
            objRequest.Parameters.AddRange(arrParam)
            dsConsulta = objRequest.Factory.ExecuteDataset(objRequest)

            ConsultaEquipoCanje_XIdPedidoActual_2 = dsConsulta
            vCod_Rpta = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)
            vMsj_Rpta = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            vCod_Rpta = ex.StackTrace.ToString()
            vMsj_Rpta = ex.Message.ToString()
            Return Nothing
        End Try
    End Function
    '/*----------------------------------------------------------------------------------------------------------------*/
    '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
    '/*----------------------------------------------------------------------------------------------------------------*/
    Public Function ConsultaDevolucion(ByVal vNroPedido As Integer, _
                                       ByRef vCursorSalida As Object, _
                                       ByRef vCod_Rpta As String, _
                                       ByRef vMsj_Rpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsConsulta As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_NROPEDIDO", DbType.Int64, vNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_CUR_SALIDA", DbType.Object, vCursorSalida, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESP", DbType.String, vCod_Rpta, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MEN_RESP", DbType.String, vMsj_Rpta, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CANJE.SSAPSS_CONSULTADEVOLUCION"
            objRequest.Parameters.AddRange(arrParam)
            dsConsulta = objRequest.Factory.ExecuteDataset(objRequest)

            ConsultaDevolucion = dsConsulta
            vCod_Rpta = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)
            vMsj_Rpta = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            vCod_Rpta = ex.StackTrace.ToString()
            vMsj_Rpta = ex.Message.ToString()
            Return Nothing
        End Try
    End Function
    '/*----------------------------------------------------------------------------------------------------------------*/
    '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
    '/*----------------------------------------------------------------------------------------------------------------*/

    Public Function ConsultaEquipoCanje_xSeriTipoNum_2(ByVal vSerie As String, _
                                                        ByVal vTipoDoc As String, _
                                                        ByVal vNroDoc As String, _
                                                        ByRef vCurPedido As Object, _
                                                        ByRef vNroLog As String, _
                                                        ByRef vDesLog As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsConsulta As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParame() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_SERIE", DbType.String, vSerie, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("K_TIPO_DOC", DbType.String, vTipoDoc, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("K_NRO_DOC", DbType.String, vNroDoc, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("CU_PEDIDO", DbType.Object, vCurPedido, ParameterDirection.Output), _
                                                       New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, vNroLog, ParameterDirection.Output), _
                                                       New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, vDesLog, ParameterDirection.Output)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CANJE.SSAPSS_CONSULTAPEDIDOORIGEN"
            objRequest.Parameters.AddRange(arrParame)
            dsConsulta = objRequest.Factory.ExecuteDataset(objRequest)


            ConsultaEquipoCanje_xSeriTipoNum_2 = dsConsulta
            vCurPedido = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value)
            vNroLog = Funciones.CheckStr(CType(objRequest.Parameters(4), IDataParameter).Value)
            vDesLog = Funciones.CheckStr(CType(objRequest.Parameters(5), IDataParameter).Value)

        Catch ex As Exception
            vNroLog = ex.StackTrace.ToString()
            vDesLog = ex.Message.ToString()
            Return Nothing
        End Try

    End Function
    'PROY-26366 - FIN
    ' PROY-30166 -IDEA-38863: INI Claro UP    
    Public Function ObtenerMontoInicialxPedidos(ByVal p_NroPedidos As String, ByVal p_Contrato As String, ByVal p_Telefono As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet
        Dim ds As DataSet


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CONCAT_PEDIDOS", DbType.String, 8000, p_NroPedidos, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CONTRATO", DbType.String, p_Contrato, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_TELEFONO", DbType.String, p_Telefono, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_COD_RESPUESTA", DbType.String, 250, strCodRpta, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_MSJ_RESPUESTA", DbType.String, strMsgRpta, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_CUR_RESPUESTA", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "sisact_pkg_general_3play_6.SISACTSS_CUOTA_INICIAL"
            objRequest.Parameters.AddRange(arrParam)
            ObtenerMontoInicialxPedidos = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToInt64(CType(objRequest.Parameters(3), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)

            If (strCodRpta = "-1") Then  'PROY-30166-JB-INI
                ObtenerMontoInicialxPedidos = Nothing
            End If                       'PROY-30166-JB-FIN

        Catch ex As Exception
            ObtenerMontoInicialxPedidos = Nothing
            strCodRpta = "-99"
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Activaciones/clsTrsPvu.vb; Function: ObtenerMontoInicialxPedidos)"
            strMsgRpta = "Error: ObtenerMontoInicialxPedidos- " & ex.Message.ToString() & MaptPath
            'FIN PROY-140126

        End Try
    End Function
    ' PROY-30166 -IDEA-38863: FIN Claro UP    
    
     'Inicio PROY-31836 IDEA-43582_Mejora de Procesos Postventa del servicio Proteccion Movil
    Public Function Consultar_ProteccionMovil_Equipo(ByVal Telefono As String, _
                                                        ByRef CodMensaje As String, _
                                                        ByRef mensaje As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_VENTV_MSISDN_RPTA", DbType.String, Telefono, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_CURSOR_PM", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_RESPUESTA_CODIGO", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_RESPUESTA_MENSAJE", DbType.String, ParameterDirection.Output)}
 
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKGProteccionMovil & "SISACTSS_VENTA_SEGURO_EQUIPO"
            objRequest.Parameters.AddRange(arrParam)
            Consultar_ProteccionMovil_Equipo = objRequest.Factory.ExecuteDataset(objRequest)
            CodMensaje = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)
            mensaje = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value)
        Catch ex As Exception
            CodMensaje = "-1"
            mensaje = "Error en Consultar_ProteccionMovil_Equipo: " & ex.Message
            Return Nothing
        End Try
    End Function

    Public Sub Insertar_SeguroEquipo_Historico(ByVal MSISDN_RPTA As String, _
                                                        ByRef CodMensaje As String, _
                                                         ByRef Mensaje As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_VENTV_MSISDN_RPTA", DbType.String, MSISDN_RPTA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_RESPUESTA_CODIGO", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_RESPUESTA_MENSAJE", DbType.String, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = PKGProteccionMovil & "SISACTSI_VENTA_SEG_EQUI_HIST"
            objRequest.Parameters.AddRange(arrParam)
        objRequest.Transactional = True

        Try

            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Factory.CommitTransaction()

            CodMensaje = Funciones.CheckStr(CType(objRequest.Parameters(1), IDataParameter).Value)
            Mensaje = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)

        Catch ex As Exception
            CodMensaje = "-1"
            Mensaje = "Error en Insertar_SeguroEquipo_Historico: " & ex.Message
            objRequest.Factory.RollBackTransaction()
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Sub
    Public Sub Actualizar_SeguroEquipo(ByVal MSISDN_RPTA As String, _
                                                    ByVal Nro_Certf_Req As String, _
                                                    ByVal Des_Material As String, _
                                                    ByVal Imei_Req As String, _
                                                    ByVal MSISDN_RPTA_N As String, _
                                                    ByRef CodMensaje As String, _
                                                    ByRef Mensaje As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_MSISDN_RPTA", DbType.String, MSISDN_RPTA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_COD_MAT_REQ", DbType.String, Nro_Certf_Req, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_DES_MATERIAL", DbType.String, Des_Material, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_IMEI_REQ", DbType.String, Imei_Req, ParameterDirection.Input), _
                           New DAAB.DAABRequest.Parameter("PI_MSISDN_NUEVO_RPTA", DbType.String, MSISDN_RPTA_N, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_RESPUESTA_CODIGO", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_RESPUESTA_MENSAJE", DbType.String, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = PKGProteccionMovil & "SISACTSU_VENTA_SEGURO_EQUIPO"
            objRequest.Parameters.AddRange(arrParam)
        objRequest.Transactional = True

        Try

            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Factory.CommitTransaction()

            CodMensaje = Funciones.CheckStr(CType(objRequest.Parameters(5), IDataParameter).Value)
            Mensaje = Funciones.CheckStr(CType(objRequest.Parameters(6), IDataParameter).Value)

        Catch ex As Exception
            CodMensaje = "-1"
            Mensaje = "Error en Actualizar_SeguroEquipo: " & ex.Message
            objRequest.Factory.RollBackTransaction()
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Sub

    Public Sub Insertar_Siniestro_Robo(ByVal NroSiniestro As String, _
                                                   ByVal NroCertif As String, _
                                                   ByVal NroTelef As String, _
                                                   ByVal TipoSiniestro As String, _
                                                   ByVal DescSiniestro As String, _
                                                   ByVal MarcaEquipo As String, _
                                                   ByVal ModeloEquipo As String, _
                                                   ByVal EquipoIMEI As String, _
                                                   ByVal EstadoSinies As String, _
                                                   ByVal UsuCrea As String, _
                                                   ByRef RptCodigo As String, _
                                                   ByRef RptMensaje As String)


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_NRO_SINIESTRO", DbType.String, NroSiniestro, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_NRO_CERTIF", DbType.String, NroCertif, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_NRO_TELEFONO", DbType.String, NroTelef, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_TIPO_SINIESTR", DbType.String, TipoSiniestro, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_DESC_SINIESTR", DbType.String, DescSiniestro, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_MARC_EQUI_ORIG", DbType.String, MarcaEquipo, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_MODL_EQUI_ORIG", DbType.String, ModeloEquipo, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_IMEI_ORIGINAL", DbType.String, EquipoIMEI, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_ESTADO", DbType.String, EstadoSinies, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_USUARIO_CREA", DbType.String, UsuCrea, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PO_RESPUESTA_CODIGO", DbType.String, RptCodigo, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("PO_RESPUESTA_MENSAJE", DbType.String, RptMensaje, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = PKGProteccionMovil & "SISACTSI_BLOQ_IMEI_ASURION"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Transactional = True

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Factory.CommitTransaction()

            RptCodigo = Funciones.CheckStr(CType(objRequest.Parameters(10), IDataParameter).Value)
            RptMensaje = Funciones.CheckStr(CType(objRequest.Parameters(11), IDataParameter).Value)

        Catch ex As Exception
            RptCodigo = ex.StackTrace.ToString()
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Activaciones/clsTrsPvu.vb; Function: Insertar_Siniestro_Robo)"
            RptMensaje = ex.Message.ToString() & MaptPath
            'FIN PROY-140126            
            objRequest.Factory.RollBackTransaction()
        Finally
            objRequest.Factory.Dispose()
        End Try

    End Sub
    'Fin PROY-31836 IDEA-43582_Mejora de Procesos Postventa del servicio Proteccion Movil

    'Iniciativa-770 Inicio
    Public Function ConsultarDetalleCuotas(ByVal pNumeroPedido As String) As DataTable


        Dim dsPool As DataSet
        Dim dtResultado As DataTable
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NUMERO_PEDIDO", DbType.String, pNumeroPedido, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("K_RESULTADO", DbType.Object, ParameterDirection.Output)}


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_DRA_CVE_6.SISACTS_CONSULTA_CUOTA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            Dim nRows As Integer = 0
            If dsPool.Tables.Count > 0 Then
                nRows = dsPool.Tables(0).Rows.Count
                dtResultado = dsPool.Tables(0)
            Else
                dtResultado = New DataTable
            End If
            Return dtResultado

        Catch ex As Exception
            dtResultado = New DataTable
            Return dtResultado
        End Try
    End Function
    'Iniciativa-770 Fin

    ' PROY IFI
    Public Function ObtenerDireccion_IFI(ByVal vNumPedido As String, _
                                                  ByRef vCurSalida As Object, _
                                                   ByRef vCodRpta As Integer, _
                                                   ByRef vMsjRpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        '  Dim dsConsulta As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_sap_pedido", DbType.String, 50, vNumPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_cursor_dir_out", DbType.Object, vCurSalida, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("p_codigo_respuesta", DbType.String, vCodRpta, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("p_mensaje_respuesta", DbType.String, 900, vMsjRpta, ParameterDirection.Output)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_GENERAL_3PLAY_6.SISACTSS_DIRMOV_IFI_PED"
            objRequest.Parameters.AddRange(arrParam)
            ObtenerDireccion_IFI = objRequest.Factory.ExecuteDataset(objRequest)

            ' ConsultaEstRenoAnticipada2 = dsConsulta ''Devuelve un DataSet
            vCodRpta = Convert.ToInt64(CType(objRequest.Parameters(2), IDataParameter).Value)
            vMsjRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ' PROY IFI

    ' INI IDEA-142717
    Public Function ConsultaCampanaVacunaton(ByVal strDocCliente As String, ByVal numNumCliente As String, ByRef strRptaCodigo As String, ByRef strRptaMensaje As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        '  Dim dsConsulta As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_DOC_CLIENTE", DbType.String, strDocCliente, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_NUM_CLIENTE", DbType.String, numNumCliente, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, strRptaCodigo, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, strRptaMensaje, ParameterDirection.Output)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_GENERAL_3PLAY_6.SISACTSS_CAMPANA_VACUNATON"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            strRptaCodigo = Convert.ToInt64(CType(objRequest.Parameters(2), IDataParameter).Value)
            strRptaMensaje = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            Return True
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ' FIN IDEA-142717

    'INICIO PROY-140600 -  IDEA 142054 Contablizar líneas a personas extranjeras
    Public Sub ActualizarTitularidadCliente(ByVal strTipoVenta As String, _
                                                       ByVal strDatosCabecera As String, _
                                                       ByVal strDatosDetalle As String, _
                                                       ByRef RptCodigo As String, _
                                                       ByRef RptMensaje As String)


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_TIPO_VENTA", DbType.String, strTipoVenta, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_DATOS_TIPI_CABECERA", DbType.String, 9999, strDatosCabecera, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_DATOS_TIPI_DETALLE", DbType.String, 9999, strDatosDetalle, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, RptCodigo, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, RptMensaje, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgTitularidadCliente & "SISACTSU_LINEAS_DECLARA_JURADA"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Transactional = True

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Factory.CommitTransaction()

            RptCodigo = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value)
            RptMensaje = Funciones.CheckStr(CType(objRequest.Parameters(4), IDataParameter).Value)

        Catch ex As Exception
            RptCodigo = ex.StackTrace.ToString()
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Activaciones/clsTrsPvu.vb; Function: ActualizarTitularidadCliente)"
            RptMensaje = ex.Message.ToString() & MaptPath           
            objRequest.Factory.RollBackTransaction()
        Finally
            objRequest.Factory.Dispose()
        End Try

    End Sub
    'FIN PROY-140600 -  IDEA 142054 Contablizar líneas a personas extranjeras
End Class
