Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB

Public Class clsConsultaPvu

    Dim objFileLog As New SICAR_Log 'NUEVO
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga") 'NUEVO
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga") 'NUEVO
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile) 'NUEVO



    Dim objSeg As New Seguridad_NET.clsSeguridad
    Dim strCadenaConexion As String

    Dim PKG_SISACT_PKG_CONS_MAESTRA_SAP As String = "SISACT_PKG_CONS_MAESTRA_SAP_6"
    Dim SP_SISACT_VENDEDORES_SAP_CONS As String = ".SISACT_VENDEDORES_SAP_CONS"
    Dim SP_SISACT_OFICINA_DOCU_CONS As String = ".SISACT_OFICINA_DOCU_CONS"


    Dim PKG_SISACT_PKG_NUEVA_LISTAPRECIOS As String = "SISACT_PKG_NUEVA_LISTAPRECIOS"
    Dim SP_SISACSS_CONSULTAR_LISTAPRECIOS As String = ".SISACSS_CONSULTAR_LISTAPRECIOS"


    Dim PKG_SISACT_PKG_ACUERDO As String = "SISACT_PKG_ACUERDO_6"
    Dim SP_SP_CON_ACUERDOS_X_DOCSAP As String = ".SP_CON_ACUERDOS_X_DOCSAP"


    Public Function ConsultaClasePedido(ByVal p_tofic_codigo As String, _
                                                ByVal p_docu_estado As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_tofic_codigo", DbType.String, 10, p_tofic_codigo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("p_docu_estado", DbType.String, 4, p_docu_estado, ParameterDirection.Input), _
                           New DAAB.DAABRequest.Parameter("p_result", DbType.Int64, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("p_listado", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "sisact_pkg_cons_maestra_sap_6.sisact_oficina_docu_cons"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaClasePedido = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    '**** SINERGIA 60 *****'
    Public Function ConsultaCuotas() As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CURSOR", DbType.Object, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_GENERAL.SP_CON_TIPO_CUOTA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaCuotas = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '***SINERGIA60 ***'
    Public Function ConsultaMotivoPedido(ByVal p_moti_opera As String, ByVal p_moti_estado As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_moti_opera", DbType.String, p_moti_opera, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_moti_estado", DbType.String, p_moti_estado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_result", DbType.Int64, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("p_listado", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "sisact_pkg_cons_maestra_sap_6.sisact_motivo_venta_cons"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaMotivoPedido = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '*** SINERGIA 60 ****'
    Public Function ConsultaParametrosOficina(ByVal k_oficc_codigooficina As String, _
                                              ByRef k_nrolog As Int64, _
                                              ByRef k_deslog As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("k_oficc_codigooficina", DbType.String, 4, k_oficc_codigooficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("p_result", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("k_nrolog", DbType.Int64, k_nrolog, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("k_deslog", DbType.String, k_deslog, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "sisact_pkg_cons_maestra_sap_6.ssapss_parametrooficina"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaParametrosOficina = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    'SINEGIA 60
    Public Function ConsultaTipoDocumento(ByVal P_FLAG_CON As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_FLAG_CON", DbType.String, P_FLAG_CON, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONSULTA_GENERAL.SISACT_CON_TIPO_DOC"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaTipoDocumento = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function ConsultarPedidosPVU(ByVal P_NRO_DOC_SAP As String, _
                                        ByRef P_CODIGO_RESPUESTA As String, _
                                        ByRef P_MENSAJE_RESPUESTA As String, _
                                        ByRef C_VENTA As DataTable, _
                                        ByRef C_VENTA_DET As DataTable) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC") '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim dsPool As DataSet
        Dim dtPool As New DataTable

        Dim drFila As DataRow
        Dim dsReturn As New DataSet

        Dim i As Integer

        Dim arrDeuda As Object
        Dim arrRecibos As Object
        Dim arrRecibosLin As Object
        Dim arrPagos As Object
        Dim arrPagosLin As Object
        Dim dsResult As Integer = 0

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRO_DOC_SAP", DbType.String, 10, P_NRO_DOC_SAP, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_VENTA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_VENTA_DET", DbType.Object, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA" & ".SP_CON_VENTA_X_DOCSAP"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)

            Dim pSalida As IDataParameter

            P_CODIGO_RESPUESTA = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            P_MENSAJE_RESPUESTA = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            C_VENTA = dsPool.Tables(0)
            C_VENTA_DET = dsPool.Tables(1)

            Return dsResult
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarPedidosPrepago(ByVal P_NRO_DOC_SAP As String, _
                                        ByRef P_CODIGO_RESPUESTA As String, _
                                        ByRef P_MENSAJE_RESPUESTA As String, _
                                        ByRef C_VENTA As DataTable, _
                                        ByRef C_VENTA_DET As DataTable) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC") '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim dsPool As DataSet
        Dim dtPool As New DataTable

        Dim drFila As DataRow
        Dim dsReturn As New DataSet

        Dim i As Integer

        Dim arrDeuda As Object
        Dim arrRecibos As Object
        Dim arrRecibosLin As Object
        Dim arrPagos As Object
        Dim arrPagosLin As Object
        Dim dsResult As Integer = 0

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRO_DOC_SAP", DbType.String, 10, P_NRO_DOC_SAP, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_VENTA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_VENTA_DET", DbType.Object, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA" & ".SP_CON_VENTA_PREPAGO"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)

            Dim pSalida As IDataParameter

            P_CODIGO_RESPUESTA = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            P_MENSAJE_RESPUESTA = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            C_VENTA = dsPool.Tables(0)
            C_VENTA_DET = dsPool.Tables(1)

            Return dsResult
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultaAcuerdoPCS(ByVal p_nro_contrato As Int64, _
                                             ByVal p_nro_sub_contrato As Object) As DataSet

        Dim dsPool As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_nro_contrato", DbType.Int64, p_nro_contrato, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("p_nro_sub_contrato", DbType.Int64, p_nro_sub_contrato, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("p_cod_resp", DbType.String, ParameterDirection.Output), _
                                                            New DAAB.DAABRequest.Parameter("p_msg_resp", DbType.String, ParameterDirection.Output), _
                                                            New DAAB.DAABRequest.Parameter("c_contrato", DbType.Object, ParameterDirection.Output), _
                                                            New DAAB.DAABRequest.Parameter("c_contrato_det", DbType.Object, ParameterDirection.Output), _
                                                            New DAAB.DAABRequest.Parameter("c_contrato_serv", DbType.Object, ParameterDirection.Output), _
                                                            New DAAB.DAABRequest.Parameter("c_direccion", DbType.Object, ParameterDirection.Output), _
                                                            New DAAB.DAABRequest.Parameter("c_garantia", DbType.Object, ParameterDirection.Output)}


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "sisact_pkg_acuerdo_6.sp_con_acuerdos"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaAcuerdoPCS = dsPool   'retormanos los datos como un DataSet
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerDrsap(ByVal P_NRODOC_SAP As String, _
                                    ByRef P_COD_RESP As String, _
                                    ByRef P_MSG_RESP As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim dtPool As New DataTable

        Dim drFila As DataRow
        Dim dsReturn As New DataSet

        Dim i As Integer

        Dim arrDeuda As Object
        Dim arrRecibos As Object
        Dim arrRecibosLin As Object
        Dim arrPagos As Object
        Dim arrPagosLin As Object

        Dim strIdentifyLog As String =Funciones.CheckStr( P_NRODOC_SAP ) ''NUEVO

        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRODOC_SAP", DbType.String,Funciones.CheckStr(P_NRODOC_SAP), ParameterDirection.Input), _
                              New DAAB.DAABRequest.Parameter("P_COD_RESP", DbType.String, ParameterDirection.Output), _
                              New DAAB.DAABRequest.Parameter("P_MSG_RESP", DbType.String, ParameterDirection.Output), _
                              New DAAB.DAABRequest.Parameter("CUR_DATOS_SAP", DbType.Object, ParameterDirection.Output), _
                              New DAAB.DAABRequest.Parameter("CUR_ACUERDO_DET", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_SISACT_PKG_ACUERDO & SP_SP_CON_ACUERDOS_X_DOCSAP
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)

            P_COD_RESP = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value) 'NUEVO
            P_MSG_RESP = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value) 'NUEVO

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - ObtenerDrSap ", strIdentifyLog) & " - P_COD_RESP : " & P_COD_RESP)   'NUEVO  
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - ObtenerDrSap ", strIdentifyLog) & " - P_MSG_RESP : " & P_MSG_RESP)   'NUEVO  


            ObtenerDrsap = dsPool

        Catch ex As Exception
            P_COD_RESP = 1
            P_MSG_RESP = ex.Message

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - ObtenerDrSap ", strIdentifyLog) & " - P_MSG_RESP_EXCEPTION : " & P_MSG_RESP)   'NUEVO  


            Return Nothing
        End Try
    End Function




    '**SINERGIA 60 ***
    Public Function ConsultaCentroCostos(ByVal p_cod_pdv As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_cod_pdv", DbType.String, 10, p_cod_pdv, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("c_centro_costo", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONS_MAESTRA_SAP_6.sp_con_centro_costo"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaCentroCostos = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'PKG: SISACT_PKG_CONS_MAESTRA_SAP
    'SP: SISACT_VENDEDORES_SAP_CONS
    Public Function ConsultaVendedor(ByVal P_PDV_CODIGO As String, _
                                        ByVal P_VEND_ESTADO As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_pdv_codigo", DbType.String, 10, P_PDV_CODIGO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("p_vend_estado", DbType.String, 4, P_VEND_ESTADO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("p_result", DbType.Int64, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("p_listado", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONS_MAESTRA_SAP_6.SISACT_VENDEDORES_SAP_CONS"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaVendedor = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'PKG: SISACT_PKG_CONS_MAESTRA_SAP
    'SP: SISACT_OFICINA_DOCU_CONS
    Public Function ConsultaClasePedidoOficina(ByVal P_TOFIC_CODIGO As String, ByVal P_DOCU_ESTADO As String) As DataSet


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim dsPool As DataSet
        Dim dtPool As New DataTable

        Dim drFila As DataRow
        Dim dsReturn As New DataSet

        Dim i As Integer

        Dim arrDeuda As Object
        Dim arrRecibos As Object
        Dim arrRecibosLin As Object
        Dim arrPagos As Object
        Dim arrPagosLin As Object

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TOFIC_CODIGO", DbType.String, 10, P_TOFIC_CODIGO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_DOCU_ESTADO", DbType.String, 4, P_DOCU_ESTADO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURPOOLPAGOS", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_SISACT_PKG_CONS_MAESTRA_SAP & SP_SISACT_OFICINA_DOCU_CONS
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaClasePedidoOficina = dsPool
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    '*** PLANES TARIFARIOS:
    Public Function ConsultaPlanXTipoVenta(ByVal P_PLNV_CODIGO As String, _
                                            ByVal P_PLNV_DESCRIPCION As String, _
                                            ByVal P_PLNV_TIPO_VENTA As String, _
                                            ByVal P_PLNC_ESTADO As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_PLNV_CODIGO", DbType.String, 5, P_PLNV_CODIGO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_PLNV_DESCRIPCION", DbType.String, 100, P_PLNV_DESCRIPCION, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_PLNV_TIPO_VENTA", DbType.String, 2, P_PLNV_TIPO_VENTA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_PLNC_ESTADO", DbType.String, 1, P_PLNC_ESTADO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CURSOR", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_MANT_CONVERGENTE_6.SP_CON_PLANES_TIPO_VENTA"        '**QA
            'objRequest.Command = "Sisact_Pkg_Mant_Convergente_Rm.SP_CON_PLANES_TIPO_VENTA"         '**HardCode
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaPlanXTipoVenta = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '***SINERGIA 60 ****'
    Public Function ConsultaCampanaXTipoVenta(ByVal P_CAMPV_CODIGO As Object, _
                                                ByVal P_CAMPV_DESCRIPCION As Object, _
                                                ByVal P_CAMPV_TIPO_VENTA As String, _
                                                ByVal P_CAMPC_ESTADO As Object) As DataSet


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CAMPV_CODIGO", DbType.String, 4, P_CAMPV_CODIGO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CAMPV_DESCRIPCION", DbType.String, 50, P_CAMPV_DESCRIPCION, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CAMPV_TIPO_VENTA", DbType.String, 2, P_CAMPV_TIPO_VENTA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CAMPC_ESTADO", DbType.String, 1, P_CAMPC_ESTADO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CURSOR", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_MANT_CONVERGENTE_6.SP_CON_CAMPANHAS_TIPO_VENTA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaCampanaXTipoVenta = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '**Revisado:
    Public Function ConsultaListaPrecios(ByVal P_TPROC_CODIGO As String, _
                                            ByVal P_TVENC_CODIGO As String, _
                                            ByVal P_CANAC_CODIGO As String, _
                                            ByVal P_DEPARTAMENTO As Object, _
                                            ByVal P_MATEC_CODIGO As String, _
                                            ByVal P_CAMPC_CODIGO As String, _
                                            ByVal P_TOPEC_CODIGO As Int64, _
                                            ByVal P_PLAZC_CODIGO As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TPROC_CODIGO", DbType.String, 2, P_TPROC_CODIGO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_TVENC_CODIGO", DbType.String, 2, P_TVENC_CODIGO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CANAC_CODIGO", DbType.String, 2, P_CANAC_CODIGO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_DEPARTAMENTO", DbType.String, 2, P_DEPARTAMENTO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_MATEC_CODIGO", DbType.String, 18, P_MATEC_CODIGO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CAMPC_CODIGO", DbType.String, 4, P_CAMPC_CODIGO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_TOPEC_CODIGO", DbType.Int64, P_TOPEC_CODIGO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_PLAZC_CODIGO", DbType.String, 2, P_PLAZC_CODIGO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_SALIDA", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_NUEVA_LISTAPRECIOS.SISACSS_CONSULTAR_LISTAPRECIOS" ' PKG_SISACT_PKG_NUEVA_LISTAPRECIOS & SP_SISACSS_CONSULTAR_LISTAPRECIOS
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaListaPrecios = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '** datos del cliente **'
    Public Function consultaDatosCliente(ByVal k_cliec_tipodoccliente As String, _
                                            ByVal k_cliev_nrodoccliente As String, _
                                            ByRef k_nrolog As Int64, _
                                            ByRef k_deslog As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("k_cliec_tipodoccliente", DbType.String, 200, k_cliec_tipodoccliente, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("k_cliev_nrodoccliente", DbType.String, 200, k_cliev_nrodoccliente, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("k_nrolog", DbType.Int64, k_nrolog, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("k_deslog", DbType.String, 100, k_deslog, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("k_cu_codcliente", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "sisact_pkg_cons_maestra_sap_6.ssapss_cliente"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            consultaDatosCliente = dsPool

            k_nrolog = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            k_deslog = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    '**Actualizar / registrar clientes en PVU : utilizado por las ventas rapidas.
    Public Function ActualizaDatosCliente(ByVal v_cliev_nro_documento As String, _
                                            ByVal v_cliec_tipo_documento As String, _
                                            ByVal v_cliev_nombre As String, _
                                            ByVal v_cliev_apellido_paterno As String, _
                                            ByVal v_cliev_apellido_materno As String, _
                                            ByVal v_cliev_razon_social As String, _
                                            ByVal v_clied_fecha_nacimiento As Date, _
                                            ByVal v_cliev_telefono As String, _
                                            ByVal v_cliev_e_mail As String, _
                                            ByVal v_cliec_sexo As String, _
                                            ByVal v_cliec_estado_civil As String, _
                                            ByVal v_cliec_titulo As String, _
                                            ByVal v_cliec_carga_familiar As String, _
                                            ByVal v_cliev_conyuge_nombre As String, _
                                            ByVal v_cliev_conyuge_ape_pat As String, _
                                            ByVal v_cliev_conyuge_ape_mat As String, _
                                            ByVal v_cliev_direccion_legal_pref As String, _
                                            ByVal v_cliev_direccion_legal As String, _
                                            ByVal v_cliev_direccion_legal_refer As String, _
                                            ByVal v_cliev_ubigeo_legal As String, _
                                            ByVal v_cliev_telef_legal_pref As String, _
                                            ByVal v_cliev_telef_legal As String, _
                                            ByVal v_cliev_direccion_fact_pref As String, _
                                            ByVal v_cliev_direccion_fact As String, _
                                            ByVal v_cliev_direccion_fact_refer As String, _
                                            ByVal v_cliev_ubigeo_fact As String, _
                                            ByVal v_cliev_telef_fact_pref As String, _
                                            ByVal v_cliev_telef_fact As String, _
                                            ByVal v_cliec_replegal_tipo_doc As String, _
                                            ByVal v_cliev_replegal_nro_doc As String, _
                                            ByVal v_cliev_replegal_nombre As String, _
                                            ByVal v_cliev_replegal_ape_pat As String, _
                                            ByVal v_cliev_replegal_ape_mat As String, _
                                            ByVal v_clied_replegal_fecha_nac As Date, _
                                            ByVal v_cliev_replegal_telefono As String, _
                                            ByVal v_cliec_replegal_sexo As String, _
                                            ByVal v_cliec_replegal_est_civ As String, _
                                            ByVal v_cliec_replegal_titulo As String, _
                                            ByVal v_cliec_contacto_tipo_doc As String, _
                                            ByVal v_cliev_contacto_nro_doc As String, _
                                            ByVal v_cliev_contacto_nombre As String, _
                                            ByVal v_cliev_contacto_ape_pat As String, _
                                            ByVal v_cliev_contacto_ape_mat As String, _
                                            ByVal v_cliev_contacto_telefono As String, _
                                            ByVal v_clien_cond_cliente As Int32, _
                                            ByVal v_cliev_empresa_labora As String, _
                                            ByVal v_cliev_empresa_cargo As String, _
                                            ByVal v_cliev_empresa_telefono As String, _
                                            ByVal v_clien_ingreso_bruto As Double, _
                                            ByVal v_clien_otros_ingresos As Double, _
                                            ByVal v_cliev_tcredito_tipo As String, _
                                            ByVal v_cliev_tcredito_num As String, _
                                            ByVal v_cliec_tcredito_moneda As String, _
                                            ByVal v_clien_tcredito_linea_cred As Double, _
                                            ByVal v_cliec_tcredito_fecha_venc As String, _
                                            ByVal v_cliev_observaciones As String, _
                                            ByVal v_cliev_codigo_sap As String, _
                                            ByVal v_cliev_vendedor_sap As String, _
                                            ByVal v_cliev_usuario_crea As String, _
                                            ByVal v_cliev_tipo_cliente As String, _
                                            ByRef p_resultado As Int16) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim rpta As Int16 = 0

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("v_cliev_nro_documento", DbType.String, 16, v_cliev_nro_documento, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliec_tipo_documento", DbType.String, 2, v_cliec_tipo_documento, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_nombre", DbType.String, 40, v_cliev_nombre, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_apellido_paterno", DbType.String, 40, v_cliev_apellido_paterno, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_apellido_materno", DbType.String, 40, v_cliev_apellido_materno, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_razon_social", DbType.String, 40, v_cliev_razon_social, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_clied_fecha_nacimiento", DbType.Date, DBNull.Value, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_telefono", DbType.String, 15, v_cliev_telefono, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_e_mail", DbType.String, 30, v_cliev_e_mail, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliec_sexo", DbType.String, 1, v_cliec_sexo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliec_estado_civil", DbType.String, 1, v_cliec_estado_civil, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliec_titulo", DbType.String, 2, v_cliec_titulo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliec_carga_familiar", DbType.String, 2, v_cliec_carga_familiar, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_conyuge_nombre", DbType.String, 40, v_cliev_conyuge_nombre, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_conyuge_ape_pat", DbType.String, 40, v_cliev_conyuge_ape_pat, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_conyuge_ape_mat", DbType.String, 40, v_cliev_conyuge_ape_mat, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_direccion_legal_pref", DbType.String, 10, v_cliev_direccion_legal_pref, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_direccion_legal", DbType.String, 40, v_cliev_direccion_legal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_direccion_legal_refer", DbType.String, 40, v_cliev_direccion_legal_refer, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_ubigeo_legal", DbType.String, 9, v_cliev_ubigeo_legal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_telef_legal_pref", DbType.String, 2, v_cliev_telef_legal_pref, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_telef_legal", DbType.String, 15, v_cliev_telef_legal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_direccion_fact_pref", DbType.String, 10, v_cliev_direccion_fact_pref, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_direccion_fact", DbType.String, 40, v_cliev_direccion_fact, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_direccion_fact_refer", DbType.String, 40, v_cliev_direccion_fact_refer, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_ubigeo_fact", DbType.String, 9, v_cliev_ubigeo_fact, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_telef_fact_pref", DbType.String, 2, v_cliev_telef_fact_pref, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_telef_fact", DbType.String, 15, v_cliev_telef_fact, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliec_replegal_tipo_doc", DbType.String, 2, v_cliec_replegal_tipo_doc, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_replegal_nro_doc", DbType.String, 16, v_cliev_replegal_nro_doc, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_replegal_nombre", DbType.String, 40, v_cliev_replegal_nombre, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_replegal_ape_pat", DbType.String, 40, v_cliev_replegal_ape_pat, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_replegal_ape_mat", DbType.String, 40, v_cliev_replegal_ape_mat, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_clied_replegal_fecha_nac", DbType.Date, v_clied_replegal_fecha_nac, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_replegal_telefono", DbType.String, 15, v_cliev_replegal_telefono, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliec_replegal_sexo", DbType.String, 1, v_cliec_replegal_sexo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliec_replegal_est_civ", DbType.String, 1, v_cliec_replegal_est_civ, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliec_replegal_titulo", DbType.String, 2, v_cliec_replegal_titulo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliec_contacto_tipo_doc", DbType.String, 2, v_cliec_contacto_tipo_doc, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_contacto_nro_doc", DbType.String, 16, v_cliev_contacto_nro_doc, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_contacto_nombre", DbType.String, 40, v_cliev_contacto_nombre, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_contacto_ape_pat", DbType.String, 40, v_cliev_contacto_ape_pat, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_contacto_ape_mat", DbType.String, 40, v_cliev_contacto_ape_mat, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_contacto_telefono", DbType.String, 15, v_cliev_contacto_telefono, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_clien_cond_cliente", DbType.Int32, v_clien_cond_cliente, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_empresa_labora", DbType.String, 40, v_cliev_empresa_labora, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_empresa_cargo", DbType.String, 40, v_cliev_empresa_cargo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_empresa_telefono", DbType.String, 15, v_cliev_empresa_telefono, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_clien_ingreso_bruto", DbType.Double, v_clien_ingreso_bruto, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_clien_otros_ingresos", DbType.Double, v_clien_otros_ingresos, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_tcredito_tipo", DbType.String, 40, v_cliev_tcredito_tipo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_tcredito_num", DbType.String, 20, v_cliev_tcredito_num, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliec_tcredito_moneda", DbType.String, 3, v_cliec_tcredito_moneda, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_clien_tcredito_linea_cred", DbType.Double, v_clien_tcredito_linea_cred, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliec_tcredito_fecha_venc", DbType.String, 7, v_cliec_tcredito_fecha_venc, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_observaciones", DbType.String, 40, v_cliev_observaciones, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_codigo_sap", DbType.String, 10, v_cliev_codigo_sap, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_vendedor_sap", DbType.String, 10, v_cliev_vendedor_sap, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_usuario_crea", DbType.String, 10, v_cliev_usuario_crea, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cliev_tipo_cliente", DbType.String, 5, v_cliev_tipo_cliente, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("p_resultado", DbType.Int16, ParameterDirection.Output)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            '**************************************************************************************************************
            objRequest.Command = "sisact_pkg_cons_maestra_sap_6.ssapsu_cliente"            '*** linea QA   - produccion  ****'
            '**************************************************************************************************************

            'objRequest.Command = "sisact_pkg_cons_maestra_sap_v2.ssapsu_cliente"          '*** linea desarrollo ***' HARDCODE
            objRequest.Parameters.AddRange(arrParam)
            rpta = objRequest.Factory.ExecuteNonQuery(objRequest)

            p_resultado = Convert.ToString(CType(objRequest.Parameters(60), IDataParameter).Value)

        Catch ex As Exception
            Return rpta
        End Try
    End Function


    '**UBIGEO:
    Public Function CargarDepartamento(ByVal K_COD_DEPARTAMENTO As String, _
                                        ByVal K_ESTADO As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_COD_DEPARTAMENTO", DbType.String, 2, K_COD_DEPARTAMENTO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_ESTADO", DbType.String, 2, K_ESTADO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SECP_PKG_MAESTROS.SECSS_CON_DEPARTAMENTO"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            CargarDepartamento = dsPool

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CargarProvincia(ByVal K_COD_PROVINCIA As String, _
                                       ByVal K_COD_DEPARTAMENTO As String, _
                                        ByVal K_ESTADO As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_COD_PROVINCIA", DbType.String, 2, K_COD_PROVINCIA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_DEPARTAMENTO", DbType.String, 2, K_COD_DEPARTAMENTO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_ESTADO", DbType.String, 2, K_ESTADO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SECP_PKG_MAESTROS.SECSS_CON_PROVINCIA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            CargarProvincia = dsPool

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CargarDistrito(ByVal K_COD_DISTRITO As String, _
                                      ByVal K_COD_PROVINCIA As String, _
                                        ByVal K_COD_DEPARTAMENTO As String, _
                                            ByVal K_ESTADO As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_COD_DISTRITO", DbType.String, 100, K_COD_DISTRITO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_PROVINCIA", DbType.String, 10, K_COD_PROVINCIA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_DEPARTAMENTO", DbType.String, 10, K_COD_DEPARTAMENTO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_ESTADO", DbType.String, 2, K_ESTADO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SECP_PKG_MAESTROS.SECSS_CON_DISTRITO"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            CargarDistrito = dsPool

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerDatosTransaccionales(ByVal P_DRAV_GENERADO As String, _
                                              ByRef P_DRAV_NRO_ASOCIADO As String, _
                                              ByRef P_DRAV_CODAPLIC As String, _
                                              ByRef P_DRAV_USUARIOAPLIC As String, _
                                              ByRef P_DRAV_DESC_TRS As String, _
                                              ByRef P_DRAV_CODPAGO As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        'Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_DRAV_GENERADO", DbType.String, 0, P_DRAV_GENERADO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_DRAV_NRO_ASOCIADO", DbType.String, P_DRAV_NRO_ASOCIADO, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("P_DRAV_CODAPLIC", DbType.String, P_DRAV_CODAPLIC, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("P_DRAV_USUARIOAPLIC", DbType.String, P_DRAV_USUARIOAPLIC, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("P_DRAV_DESC_TRS", DbType.String, P_DRAV_DESC_TRS, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("P_DRAV_CODIGO_PAGO", DbType.String, P_DRAV_CODPAGO, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_TRANSACCION_DRA.SP_OBTENER_DATOSTRANSACT"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteReader(objRequest)


            P_DRAV_NRO_ASOCIADO = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            P_DRAV_CODAPLIC = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            P_DRAV_USUARIOAPLIC = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            P_DRAV_DESC_TRS = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
            P_DRAV_CODPAGO = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)

            'dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            'ConsultaParametrosOficina = dsPool

            ObtenerDatosTransaccionales = True
        Catch ex As Exception
            ObtenerDatosTransaccionales = False
        End Try
    End Function

    Public Function ObtenerRentaAdelantada(ByVal K_PEDIDO_MODULO As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim dtPool As New DataTable

        Dim drFila As DataRow
        Dim dsReturn As New DataSet

        Dim i As Integer

        Dim arrDeuda As Object
        Dim arrRecibos As Object
        Dim arrRecibosLin As Object
        Dim arrPagos As Object
        Dim arrPagosLin As Object

        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIDO_MODULO", DbType.String, K_PEDIDO_MODULO, ParameterDirection.Input), _
                              New DAAB.DAABRequest.Parameter("K_RESULTSET", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            'objRequest.Command = PKG_SISACT_PKG_ACUERDO & SP_SP_CON_ACUERDOS_X_DOCSAP
            objRequest.Command = "SISACT_IMPRESION_DRA. SP_IMPRESION_DRA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ObtenerRentaAdelantada = dsPool

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ImpresionRentaAdelantada(ByVal P_DRAV_GENERADO As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim dtPool As New DataTable

        Dim drFila As DataRow
        Dim dsReturn As New DataSet

        Dim i As Integer

        Dim arrDeuda As Object
        Dim arrRecibos As Object
        Dim arrRecibosLin As Object
        Dim arrPagos As Object
        Dim arrPagosLin As Object

        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_RESULTADO", DbType.Object, ParameterDirection.Output), _
                                                           New DAAB.DAABRequest.Parameter("P_DRAV_GENERADO", DbType.String, P_DRAV_GENERADO, ParameterDirection.Input)}

            objRequest.CommandType = CommandType.StoredProcedure
            'objRequest.Command = "SISACT_PKG_DRA_CVE_6.SISACSS_CONSULTAR_PAGO_DRA"
            objRequest.Command = "SISACT_PKG_DRA_CVE_6.SISACSS_CONSPAGO_SICARDRA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ImpresionRentaAdelantada = dsPool

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ObetenerDatosVenta(ByVal NROINTERNO As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet

        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NROINTERNO", DbType.String, NROINTERNO, ParameterDirection.Input), _
                                                           New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_EXPRESS_6.SP_OBTENER_CAMPANIA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ObetenerDatosVenta = dsPool

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerDatosVentaPrepago(ByVal NROINTERNO As String, ByRef MsgExc As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet

        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, NROINTERNO, ParameterDirection.Input), _
                                                           New DAAB.DAABRequest.Parameter("P_CONSULTA", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_REPOSICION_S6.SISACTSS_CONS_VENTA_REPO_V2"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ObtenerDatosVentaPrepago = dsPool
            MsgExc = "Consulta Exitosa"
        Catch ex As Exception
            MsgExc = "Ingreso a EX (ObtenerDatosVentaPrepago): " & ex.Message.ToString()
            Return Nothing
        End Try
    End Function

    Public Function ValidacionPagodeVentaAltaconReno(ByVal NROPEDIDO As String, _
                                              ByRef NROPEDIDOASOC As String, _
                                              ByRef MENSAJE As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        'Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("V_NROPEDIDO", DbType.String, NROPEDIDO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("V_NROPEDIDOASOC", DbType.String, NROPEDIDOASOC, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("V_MENSAJE", DbType.String, 200, MENSAJE, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "sisact_pkg_venta_express_6.SP_VALIDAR_VR_COMBO_PAGO"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteReader(objRequest)


            NROPEDIDOASOC = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            MENSAJE = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)

            ValidacionPagodeVentaAltaconReno = NROPEDIDOASOC & "||" & MENSAJE
        Catch ex As Exception
            ValidacionPagodeVentaAltaconReno = "Ingreso a EX (ValidacionPagodeVentaAltaconReno): " & ex.Message.ToString()
            NROPEDIDOASOC = ""
            MENSAJE = ""
            Throw New Exception(ValidacionPagodeVentaAltaconReno)
        End Try
    End Function
    
    ''DIL.C(20160229) :: INI - PROY 21987-IDEA 28251
    Public Function ObtenerDatosCobertura(ByVal int64IdVenta As Int64, ByRef strRespuesta As String) As ArrayList
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim lstRespuesta As New ArrayList
        Dim int32RespuestaCodigo As Int32
        Dim strRespuestaMensaje As String
        Dim strRespuestaEstado As String
        Dim strDatosCobertura As String

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("P_IN_IDVENTA", DbType.Int64, int64IdVenta, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("P_OUT_CURSOR", DbType.Object, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("P_OUT_RPTCODIGO", DbType.Int32, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("P_OUT_RPTMENSAJE", DbType.String, 400, ParameterDirection.Output) _
        }

        For i As Integer = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(0).Value = int64IdVenta

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTA_PREPAGO2_S6.SISACTSS_COBERTURA_VENTA"
        objRequest.Parameters.AddRange(arrParam)

        Try
            Dim dr As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
            strDatosCobertura = String.Empty

            While dr.Read
                Dim item As New BECobertura
                With item
                    .strDVPR_DEPAC_CODIGO = Funciones.CheckStr(dr("DVPR_DEPAC_CODIGO"))
                    .strDVPR_DEPAC_DESC = Funciones.CheckStr(dr("DVPR_DEPAC_DESC"))
                    .strDVPR_PROVC_CODIGO = Funciones.CheckStr(dr("DVPR_PROVC_CODIGO"))
                    .strDVPR_PROVV_DESC = Funciones.CheckStr(dr("DVPR_PROVV_DESC"))
                    .strDVPR_DISTC_CODIGO = Funciones.CheckStr(dr("DVPR_DISTC_CODIGO"))
                    .strDVPR_DISTV_DESC = Funciones.CheckStr(dr("DVPR_DISTV_DESC"))
                    .strDVPR_UBIGEO_INEI = Funciones.CheckStr(dr("DVPR_UBIGEO_INEI"))
                    .strDVPR_CPF_DESC = Funciones.CheckStr(dr("DVPR_CPF_DESC"))
                End With

                lstRespuesta.Add(item)

                strDatosCobertura = strDatosCobertura & _
                                    Funciones.CheckStr(dr("DVPR_DEPAC_CODIGO")) & "," & _
                                    Funciones.CheckStr(dr("DVPR_DEPAC_DESC")) & "," & _
                                    Funciones.CheckStr(dr("DVPR_PROVC_CODIGO")) & "," & _
                                    Funciones.CheckStr(dr("DVPR_PROVV_DESC")) & "," & _
                                    Funciones.CheckStr(dr("DVPR_DISTC_CODIGO")) & "," & _
                                    Funciones.CheckStr(dr("DVPR_DISTV_DESC")) & "," & _
                                    Funciones.CheckStr(dr("DVPR_UBIGEO_INEI")) & "," & _
                                    Funciones.CheckStr(dr("DVPR_CPF_DESC")) & "|"
            End While

            int32RespuestaCodigo = Funciones.CheckInt(CType(objRequest.Parameters(2), IDataParameter).Value) 'Codigo de Respuesta
            strRespuestaMensaje = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value) 'Mensaje de Respuesta
            strRespuestaEstado = IIf(int32RespuestaCodigo >= 0, "Correcto", "Incorrecto")

        Catch ex As Exception
            lstRespuesta = Nothing
            int32RespuestaCodigo = -1
            strRespuestaMensaje = "ErrorAPP[" + ex.Message + "]"
            strRespuestaEstado = "Incorrecto"
            strDatosCobertura = String.Empty
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        strRespuesta = int32RespuestaCodigo.ToString() & ";" & strRespuestaMensaje & ";" & strRespuestaEstado & ";" & strDatosCobertura

        Return lstRespuesta
    End Function
    ''DIL.C(20160229) :: FIN - PROY 21987-IDEA 28251

	Public Function ObtenerPedidoPackAccesorio(ByVal P_NROPEDIDO_ACC As String, ByRef P_RESULTADO As String) As DataSet 'PROY-23111-IDEA-29841 - INICIO

          strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
  
          Dim dsPool As DataSet
  
          Try
              Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
              Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_PEDIDO_ACC", DbType.String, P_NROPEDIDO_ACC, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output), _
                                                              New DAAB.DAABRequest.Parameter("K_RESULTADO", DbType.String, ParameterDirection.Output)}
  
              objRequest.CommandType = CommandType.StoredProcedure
              objRequest.Command = "SISACT_PKG_VENTA_EXPRESS_6.SP_OBTENER_PACK_ACC"
              objRequest.Parameters.AddRange(arrParam)
              dsPool = objRequest.Factory.ExecuteDataset(objRequest)            
              ObtenerPedidoPackAccesorio = dsPool
              P_RESULTADO = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
  
          Catch ex As Exception
              Return Nothing
          End Try
    	End Function 'PROY-23111-IDEA-29841 - FIN

 
    Public Function ActualizaVentaRenoWL(ByVal NRO_Telefono As String, ByVal Estado As String) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim P_Estado As String

        P_Estado = Estado

        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRO_LINEA", DbType.String, NRO_Telefono, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, P_Estado, ParameterDirection.Input), _
                                                           New DAAB.DAABRequest.Parameter("P_RESULTADO", DbType.Int16, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_WL_RENO_PREPAGO.SISACTSU_ACTUALIZA_ESTA_LINEA"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteDataset(objRequest)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
   
    Public Function ConsultaVentaRenoWL(ByVal NRO_Telefono As String, ByVal P_Estado As String) As DataTable

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim dtResultado As DataTable
            Dim resultado As DataSet
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRO_LINEA", DbType.String, NRO_Telefono, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_Estado", DbType.String, P_Estado, ParameterDirection.Input), _
                                                           New DAAB.DAABRequest.Parameter("K_DATOS_LINEA", DbType.Object, ParameterDirection.Output)}
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_WL_RENO_PREPAGO.SISACTSS_OBTENER_DATOS_LINEA"
            objRequest.Parameters.AddRange(arrParam)

            resultado = objRequest.Factory.ExecuteDataset(objRequest)
            If resultado.Tables.Count > 0 Then
                dtResultado = resultado.Tables(0)
            End If

            Return dtResultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ActualizarWLxDocSap(ByVal Doc_Sap As String, ByVal Estado As String) As Integer

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim Rpta As String
            Dim resultado As DataSet
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_WLRPV_DOCUMENTO_SAP", DbType.String, Doc_Sap, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, Estado, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_RESULTADO", DbType.Int16, ParameterDirection.Output)}
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_WL_RENO_PREPAGO.SISACTSU_ACTUALIZA_ESTA_SAP"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
 
	'PROY-23700-IDEA-29415 - INI
    Public Function ConsultaMotivosCanje(ByVal P_TIPO_OPERACION As String, _
                                  ByVal P_CANAL As String, _
                                  ByVal P_ESTADO As String, _
                                  ByRef P_CODIGO_RESPUESTA As String, _
                                  ByRef P_MENSAJE_RESPUESTA As String) As DataTable

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

            Dim dtPool As New DataTable
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                            New DAAB.DAABRequest.Parameter("P_TIPO_OPERACION", DbType.String, P_TIPO_OPERACION, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("P_CANAL", DbType.String, P_CANAL, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, P_ESTADO, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("P_CUR_SALIDA", DbType.Object, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("P_CODIGO_RESPUESTA", DbType.String, P_CODIGO_RESPUESTA, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("P_MENSAJE_RESPUESTA", DbType.String, 200, P_MENSAJE_RESPUESTA, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONSULTA_GENERAL.SP_CON_MOTIVO_VENTA"  'ssapss_consultacanje
            objRequest.Parameters.AddRange(arrParam)
            dtPool = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
            ConsultaMotivosCanje = dtPool
            P_CODIGO_RESPUESTA = CType(objRequest.Parameters(4), IDataParameter).Value
            P_MENSAJE_RESPUESTA = CType(objRequest.Parameters(5), IDataParameter).Value

        Catch ex As Exception
            P_CODIGO_RESPUESTA = 99
            P_MENSAJE_RESPUESTA = ex.Message.ToString()
            Return Nothing
        End Try
    End Function

    'PROY-23700-IDEA-29415 - FIN
    'PROY-23700-IDEA-29415 - INI 
    Public Function ConsultaParametros(ByVal strGrupo As String) As ArrayList
        Dim lstRespuesta As New ArrayList
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            'Se quita valor de longitud de dato de P_PARAN_GRUPO - PROY 25335 R2
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                    New DAAB.DAABRequest.Parameter("P_PARAN_GRUPO", DbType.String, 10, strGrupo, ParameterDirection.Input), _
                                                    New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONSULTA_GENERAL.SECSS_CON_PARAMETRO_GP"
            objRequest.Parameters.AddRange(arrParam)
            Dim dr As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
            While dr.Read
                Dim item As New BEParametros
                With item
                    .intCodigo = Funciones.CheckInt64(dr("PARAN_CODIGO"))
                    .strDescripcion = Funciones.CheckStr(dr("PARAV_DESCRIPCION"))
                    .strValor = Funciones.CheckStr(dr("PARAV_VALOR"))
                    .strValor1 = Funciones.CheckStr(dr("PARAV_VALOR1"))
                End With
                lstRespuesta.Add(item)
                ConsultaParametros = lstRespuesta
            End While
            ConsultaParametros = lstRespuesta
        Catch ex As Exception

        End Try

    End Function
    'PROY-23700-IDEA-29415 - FIN 

   ' INICIO - PROY-25335-RELEASE 0 - Oscar Atencio Timana

    Public Function ConsultarSecConCartaPoder(ByVal sec As String, ByVal nroPedido As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_SCPN_SOLIN_CODIGO", DbType.String, sec, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_SCPN_NRO_PEDIDO", DbType.String, nroPedido, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CUR_SALIDA", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("P_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("P_MENSAJE_RESPUESTA", DbType.String, 200, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISACT_CARTA_PODER.SISACTSS_CARTA_PODER"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteReader(objRequest)

            ConsultarSecConCartaPoder = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            ConsultarSecConCartaPoder = "1"
        End Try


    End Function


    Public Function ValidarIdentidad(ByVal strTipoDoc As String, ByVal strNumDoc As String, ByVal intNumSec As Int64, ByVal intNunPedido As Int64, ByRef strCodRpta As String, ByRef strMgsRpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        'INC000004636534
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TIPO_DOCUMENTO", DbType.String, strTipoDoc, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NUM_DOCUMENTO", DbType.String, strNumDoc, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NRO_PEDIDO", DbType.Int64, intNunPedido, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NUM_SEC", DbType.Int64, 0, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CURSOR", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("P_COD_RESPUESTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("P_MSJ_RESPUESTA", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_VALIDADOR_IDENTIDAD.VIDSS_IDENTIDAD_HIT"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)
            strMgsRpta = Convert.ToString(CType(objRequest.Parameters(6), IDataParameter).Value) 'PROY-25335 Contratacion Electronica R2 - GAPS
            ValidarIdentidad = dsPool
        Catch ex As Exception
strCodRpta=99
strMgsRpta=ex.message.trim()
            Return Nothing
        End Try

    End Function




    Public Function ListaRepresentanteLegal(ByVal NroSec As Int64) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_SRLN_SOLIN_CODIGO", DbType.Int64, NroSec, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CUR_SALIDA", DbType.Object, ParameterDirection.Output), _
                           New DAAB.DAABRequest.Parameter("P_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("P_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISACT_REPRESENTANTE_LEGAL.SISACTSS_REPRESENTANTE_LEGAL"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ListaRepresentanteLegal = dsPool
        Catch ex As Exception
            Return Nothing
        End Try


    End Function

    Public Function ListaParametrosGrupo(ByVal ParGrup As Int64) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_PARAN_GRUPO", DbType.Int64, ParGrup, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONSULTA_GENERAL.SECSS_CON_PARAMETRO_GP"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ListaParametrosGrupo = dsPool
        Catch ex As Exception
            Return Nothing
        End Try


    End Function

    Public Function ObtenerSec(ByVal nroContrato As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_SOLIV_NUM_PED", DbType.String, nroContrato, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_EXP_3PLAY_6.SP_OBTENERSEC_PEDIDO"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)


            ObtenerSec = Convert.ToString(dsPool.Tables(0).Rows(0).Item("SOLIN_CODIGO"))

        Catch ex As Exception
            ObtenerSec = "1"
        End Try



    End Function

    Public Function ConsultaPuntoVenta_Codigo(ByVal P_OVENC_CODIGO As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OVENC_CODIGO", DbType.String, 10, P_OVENC_CODIGO, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_SALIDA", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_DATOS_OFICINA.GET_DATOS_OFICINA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaPuntoVenta_Codigo = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

 ' FIN - PROY-25335-RELEASE 0 - Oscar Atencio Timana

    'PROY-26366-IDEA-34247 FASE 1 - INICIO
    Public Function ObtenerSISACT_Parametros(ByVal P_PARAN_GRUPO As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet

        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_PARAN_GRUPO", DbType.String, P_PARAN_GRUPO, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONSULTA_GENERAL.secss_con_parametro_gp"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ObtenerSISACT_Parametros = dsPool

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerPrecio_Prepago(ByVal P_MATERIAL As String, ByVal P_LISTAPRECIO As String) As Decimal

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim P_PRECIOPREPAGO As Decimal
        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                      New DAAB.DAABRequest.Parameter("P_MATERIAL", DbType.String, P_MATERIAL, ParameterDirection.Input), _
                      New DAAB.DAABRequest.Parameter("P_LISTAPRECIO", DbType.String, P_LISTAPRECIO, ParameterDirection.Input), _
                      New DAAB.DAABRequest.Parameter("P_PRECIOPREPAGO", DbType.Decimal, P_PRECIOPREPAGO, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_NUEVA_LISTAPRE_6.SISACTSI_PRECIO_PREPAGO"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteReader(objRequest)

            P_PRECIOPREPAGO = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            ObtenerPrecio_Prepago = P_PRECIOPREPAGO

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'PROY-26366-IDEA-34247 FASE 1 - FIN
	
    'INICIO - PROY-30160
    Public Function ConsultaReposicionPrepagoTGFI(ByVal P_NRO_PEDIDO As String) As String()
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim param_monto, param_motivo, param_codigo, param_mensaje As String

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_NRO_PEDIDO", DbType.String, 10, P_NRO_PEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_MONTO_DESC", DbType.Decimal, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MOTIVO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_CODIGO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MENSAJE", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_REPOSICION_S6.SISACTSS_CONS_VENTA_REPO_TGFI" 
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteReader(objRequest)
            param_monto = Funciones.CheckStr(CType(objRequest.Parameters(1), IDataParameter).Value)
            param_motivo = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)
            param_codigo = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value)
            param_mensaje = Funciones.CheckStr(CType(objRequest.Parameters(4), IDataParameter).Value)

            ConsultaReposicionPrepagoTGFI = New String() {param_monto, param_motivo, param_codigo, param_mensaje}

        Catch ex As Exception
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Activaciones\clsConsultaPvu.vb; Function: ConsultaReposicionPrepagoTGFI)"
            ConsultaReposicionPrepagoTGFI = New String() {"", "", "-1", ex.Message.ToString() & MaptPath}
            'FIN PROY-140126

        End Try

    End Function
    'FIN - PROY-30160
	


' INICIO - PROY-25335-RELEASE 2 - Gustavo Palomino Sosa
    Public Function GenerarIdTrazabilidad(ByVal intTipoTrx As Integer) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam As DAABRequest.Parameter() = {New DAABRequest.Parameter("p_tipo_id", DbType.Int32, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("p_id_trx_bio", DbType.String, ParameterDirection.ReturnValue)}


        arrParam(0).Value = DBNull.Value
        arrParam(1).Value = intTipoTrx


        Dim salida As String = ""
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_BIOMETRIA" + ".SISACTFUN_GEN_ID_TX_BIO"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Transactional = True

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Factory.CommitTransaction()

            salida = Funciones.CheckStr(DirectCast(objRequest.Parameters(1), IDataParameter).Value)
        Catch ex As Exception
            objRequest.Factory.RollBackTransaction()
        Finally

            objRequest.Factory.Dispose()
        End Try
        Return salida
    End Function

    Public Function ConsultarExpedienteVentaEstado(ByVal sevnID As Integer) As DataSet
        Dim dr As IDataReader = Nothing
        Dim beExpVenta As New BEExpedienteVenta
        Dim dsPool As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim obRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Try

            Dim arrParam As DAABRequest.Parameter() = {New DAABRequest.Parameter("PI_SEVV_ID_CONTRATO", DbType.Int32, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("PO_CURSOR", DbType.Object, ParameterDirection.Output), _
                                                       New DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                                                       New DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output)}

            For i As Integer = 0 To arrParam.Length - 1
                arrParam(i).Value = DBNull.Value
            Next

            arrParam(0).Value = sevnID

            obRequest.CommandType = CommandType.StoredProcedure
            obRequest.Command = "PKG_SISACT_EXPEDIENTES_VENTA" + ".SISACTSS_EXPE_VENTA_ESTADO"
            obRequest.Transactional = True
            obRequest.Parameters.AddRange(arrParam)

            dsPool = obRequest.Factory.ExecuteDataset(obRequest)
            ConsultarExpedienteVentaEstado = dsPool

            Dim parSalida1 As IDataParameter
            Dim parSalida2 As IDataParameter
            parSalida1 = DirectCast(obRequest.Parameters(2), IDataParameter)
            parSalida2 = DirectCast(obRequest.Parameters(3), IDataParameter)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ActualizarFlagCorreoExpedienteVenta(ByVal SevnID As String, ByVal Correo As Integer, ByVal usuariomod As String, ByRef P_CODIGO_RESPUESTA As String, _
                                  ByRef P_MENSAJE_RESPUESTA As String) As String

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim resultado As DataSet
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_SEVV_ID_CONTRATO", DbType.String, SevnID, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("PI_SEVN_FLAG_CORREO", DbType.String, Correo, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("PI_SEVV_USUARIO_MODIFICA", DbType.String, usuariomod, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                                                            New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output)}
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISACT_EXPEDIENTES_VENTA" + ".SISACTSU_EXPE_VENTA_CORREO"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            resultado = objRequest.Factory.ExecuteDataset(objRequest)

            P_CODIGO_RESPUESTA = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            P_MENSAJE_RESPUESTA = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultaCorreoCliente(ByVal strTipoDoc As String, _
                                        ByVal strNumeroDocumento As String, _
                                        ByRef P_CODIGO_RESPUESTA As String, _
                                        ByRef P_MENSAJE_RESPUESTA As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_CLIEC_TIPO_DOCUMENTO", DbType.String, strTipoDoc, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_CLIEV_NRO_DOCUMENTO", DbType.String, strNumeroDocumento, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_CURSOR", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONS_MAESTRA_SAP_6.SISACTSS_CLIENTE_DATOS_PERS"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaCorreoCliente = dsPool
            P_CODIGO_RESPUESTA = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            P_MENSAJE_RESPUESTA = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    'Agregador por: Jesus Abregu C. - 24/01/2018
    Public Function ConsultarExpedienteVenta(ByVal intNroContrato As Integer, ByRef strCodRespuesta As String, ByRef strMsjRespuesta As String) As BEExpedienteVenta
        Dim oRespuesta As BEExpedienteVenta = Nothing
        Dim dr As IDataReader = Nothing

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Try


            Dim arrParam As DAAB.DAABRequest.Parameter() = {New DAAB.DAABRequest.Parameter("PI_SEVV_ID_CONTRATO", DbType.Int32, intNroContrato, ParameterDirection.Input), _
                              New DAAB.DAABRequest.Parameter("PO_CURSOR", DbType.Object, ParameterDirection.Output), _
                              New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                              New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output)}

            For i As Integer = 0 To i < arrParam.Length
                arrParam(i).Value = DBNull.Value
            Next
            arrParam(0).Value = intNroContrato


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISACT_EXPEDIENTES_VENTA" + ".SISACTSS_EXPEDIENTES_VENTA"

            objRequest.Transactional = True
            objRequest.Parameters.AddRange(arrParam)


            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
            If Not dr Is Nothing Then
                While dr.Read()
                    oRespuesta = New BEExpedienteVenta
                    oRespuesta.SEVN_ID = Funciones.CheckInt(dr("SEVN_ID"))
                    oRespuesta.SEVN_TIPO_EXPEDIENTE = Funciones.CheckInt64(dr("SEVN_TIPO_EXPEDIENTE"))
                    oRespuesta.SEVV_ID_CONTRATO = Funciones.CheckInt(dr("SEVV_ID_CONTRATO"))
                    oRespuesta.SEVN_SOLIN_CODIGO = Funciones.CheckInt(dr("SEVN_SOLIN_CODIGO"))
                    oRespuesta.SEVN_SOLIN_CODIGO_DET = Funciones.CheckInt64(dr("SEVN_SOLIN_CODIGO_DET"))
                    oRespuesta.SEVN_NUMSCL = Funciones.CheckStr(dr("SEVN_NUMSCL"))
                    oRespuesta.SEVN_PEDIN_SINERGIA = Funciones.CheckInt64(dr("SEVN_PEDIN_SINERGIA"))
                    oRespuesta.SEVN_TIPO_PRODUCTO = Funciones.CheckStr(dr("SEVN_TIPO_PRODUCTO"))
                    oRespuesta.SEVV_PRODUCTO = Funciones.CheckStr(dr("SEVV_PRODUCTO"))
                    oRespuesta.SEVN_MODALIDAD = Funciones.CheckStr(dr("SEVN_MODALIDAD"))
                    oRespuesta.SEVN_TIPO_OPERACION = Funciones.CheckStr(dr("SEVN_TIPO_OPERACION"))
                    oRespuesta.SEVV_ESTADO = Funciones.CheckStr(dr("SEVV_ESTADO"))
                    oRespuesta.SEVV_COMPLETO = Funciones.CheckStr(dr("SEVV_COMPLETO"))
                    oRespuesta.SEVV_FINALIZA = Funciones.CheckStr(dr("SEVV_FINALIZA"))
                    oRespuesta.SEVN_COD_PDV = Funciones.CheckStr(dr("SEVN_COD_PDV"))
                    oRespuesta.SEVN_FLAG_SIGEXP = Funciones.CheckInt64(dr("SEVN_FLAG_SIGEXP"))
                    oRespuesta.FECHA_CREACION = Funciones.CheckDate(dr("SEVD_FECHA_CREACION"))
                    oRespuesta.USUARIO_CREACION = Funciones.CheckStr(dr("SEVV_USUARIO_CREA"))
                    oRespuesta.FECHA_MODIFICACION = Funciones.CheckDate(dr("SEVD_FECHA_MODIFICA"))
                    oRespuesta.USUARIO_MODIFICACION = Funciones.CheckStr(dr("SEVV_USUARIO_MODIFICA"))
                End While
            End If

            Dim parSalida1 As IDataParameter
            Dim parSalida2 As IDataParameter
            parSalida1 = CType(objRequest.Parameters(2), IDataParameter)
            parSalida2 = CType(objRequest.Parameters(3), IDataParameter)
            strCodRespuesta = Funciones.CheckStr(parSalida1.Value)
            strMsjRespuesta = Funciones.CheckStr(parSalida2.Value)

        Catch ex As Exception
            oRespuesta = Nothing
            strCodRespuesta = "-2"
            strMsjRespuesta = Funciones.CheckStr(ex.Message)
        Finally
            If Not dr Is Nothing AndAlso Not dr.IsClosed Then dr.Close()
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return oRespuesta
    End Function
    ' FIN - PROY-25335-RELEASE 2 - Gustavo Palomino Sosa

    'INICIO PROY-140578 - Luis Muñante
    Public Function ConsultarExpedienteVentaPrepago(ByVal intNroPedido As Integer, ByRef strCodRespuesta As String, ByRef strMsjRespuesta As String) As BEExpedienteVenta
        Dim oRespuesta As BEExpedienteVenta = Nothing
        Dim dr As IDataReader = Nothing

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Try


            Dim arrParam As DAAB.DAABRequest.Parameter() = {New DAAB.DAABRequest.Parameter("PI_SEVN_PEDIN_SINERGIA", DbType.Int32, intNroPedido, ParameterDirection.Input), _
                              New DAAB.DAABRequest.Parameter("PO_CURSOR", DbType.Object, ParameterDirection.Output), _
                              New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                              New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output)}

            For i As Integer = 0 To i < arrParam.Length
                arrParam(i).Value = DBNull.Value
            Next
            arrParam(0).Value = intNroPedido


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISACT_EXPEDIENTES_VENTA" + ".SISACTSS_EXPE_VENTA_PRE"

            objRequest.Transactional = True
            objRequest.Parameters.AddRange(arrParam)


            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
            If Not dr Is Nothing Then
                While dr.Read()
                    oRespuesta = New BEExpedienteVenta
                    oRespuesta.SEVN_ID = Funciones.CheckInt(dr("SEVN_ID"))
                    oRespuesta.SEVN_TIPO_EXPEDIENTE = Funciones.CheckInt64(dr("SEVN_TIPO_EXPEDIENTE"))
                    oRespuesta.SEVV_ID_CONTRATO = Funciones.CheckInt(dr("SEVV_ID_CONTRATO"))
                    oRespuesta.SEVN_SOLIN_CODIGO = Funciones.CheckInt(dr("SEVN_SOLIN_CODIGO"))
                    oRespuesta.SEVN_SOLIN_CODIGO_DET = Funciones.CheckInt64(dr("SEVN_SOLIN_CODIGO_DET"))
                    oRespuesta.SEVN_NUMSCL = Funciones.CheckStr(dr("SEVN_NUMSCL"))
                    oRespuesta.SEVN_PEDIN_SINERGIA = Funciones.CheckInt64(dr("SEVN_PEDIN_SINERGIA"))
                    oRespuesta.SEVN_TIPO_PRODUCTO = Funciones.CheckStr(dr("SEVN_TIPO_PRODUCTO"))
                    oRespuesta.SEVV_PRODUCTO = Funciones.CheckStr(dr("SEVV_PRODUCTO"))
                    oRespuesta.SEVN_MODALIDAD = Funciones.CheckStr(dr("SEVN_MODALIDAD"))
                    oRespuesta.SEVN_TIPO_OPERACION = Funciones.CheckStr(dr("SEVN_TIPO_OPERACION"))
                    oRespuesta.SEVV_ESTADO = Funciones.CheckStr(dr("SEVV_ESTADO"))
                    oRespuesta.SEVV_COMPLETO = Funciones.CheckStr(dr("SEVV_COMPLETO"))
                    oRespuesta.SEVV_FINALIZA = Funciones.CheckStr(dr("SEVV_FINALIZA"))
                    oRespuesta.SEVN_COD_PDV = Funciones.CheckStr(dr("SEVN_COD_PDV"))
                    oRespuesta.SEVN_FLAG_SIGEXP = Funciones.CheckInt64(dr("SEVN_FLAG_SIGEXP"))
                    oRespuesta.FECHA_CREACION = Funciones.CheckDate(dr("SEVD_FECHA_CREACION"))
                    oRespuesta.USUARIO_CREACION = Funciones.CheckStr(dr("SEVV_USUARIO_CREA"))
                    oRespuesta.FECHA_MODIFICACION = Funciones.CheckDate(dr("SEVD_FECHA_MODIFICA"))
                    oRespuesta.USUARIO_MODIFICACION = Funciones.CheckStr(dr("SEVV_USUARIO_MODIFICA"))
                    oRespuesta.SEVN_FLAG_CORREO = Funciones.CheckStr(dr("SEVN_FLAG_CORREO"))
                End While
            End If

            Dim parSalida1 As IDataParameter
            Dim parSalida2 As IDataParameter
            parSalida1 = CType(objRequest.Parameters(2), IDataParameter)
            parSalida2 = CType(objRequest.Parameters(3), IDataParameter)
            strCodRespuesta = Funciones.CheckStr(parSalida1.Value)
            strMsjRespuesta = Funciones.CheckStr(parSalida2.Value)
        Catch ex As Exception
            oRespuesta = Nothing
            strCodRespuesta = "-2"
            strMsjRespuesta = Funciones.CheckStr(ex.Message)
        Finally
            If Not dr Is Nothing AndAlso Not dr.IsClosed Then dr.Close()
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return oRespuesta
    End Function
    ' FIN - PROY-140578 - Luis Muñante

    ' PROY 31850 FASE IV INICIO
    Public Function GetDatosVentasOLO(ByVal PI_LINEA As Int64, _
                                        ByRef K_COD_RESPUESTA As Int64, _
                                        ByRef K_MSJ_RESPUESTA As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC") ' user: pvu | password: pvu | BD: TIMDEV
        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_LINEA", DbType.Int64, PI_LINEA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_RESPUESTA_CODIGO", DbType.Int64, K_COD_RESPUESTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_RESPUESTA_MENSAJE", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_CURSOR_VENTA", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_OLO_VENTAACTIVACION.SISACSS_DATOS_LINEA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            GetDatosVentasOLO = dsPool
            K_COD_RESPUESTA = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            K_MSJ_RESPUESTA = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    ' PROY 31850 FASE IV FIN
    'INI: PROY-32815 RESOLUCIÒN SUNAT - RMZ
    Public Function ConsultarTipoOperacion(ByVal pi_flag) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_FLAG_CON", DbType.String, pi_flag, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONSULTA_GENERAL.SISACT_CON_TIPO_DOC"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultarTipoOperacion = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'FIN: PROY-32815 RESOLUCIÒN SUNAT - RMZ
    'PROY-31766 - INICIO
    Public Function ObtenerIGV(ByRef strCodRpta As String, ByRef strMsgRpta As String) As Double
        Dim dblIGV As Double
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PO_IGV", DbType.Double, ParameterDirection.Output), _
                                                            New DAAB.DAABRequest.Parameter("PO_CODRESPUESTA", DbType.String, ParameterDirection.Output), _
                                                            New DAAB.DAABRequest.Parameter("PO_MSGRESPUESTA", DbType.String, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONSULTA_GENERAL.SECSS_CON_IGV"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteDataset(objRequest)

            dblIGV = Funciones.CheckDbl(CType(objRequest.Parameters(0), IDataParameter).Value)
            strCodRpta = Funciones.CheckStr(CType(objRequest.Parameters(1), IDataParameter).Value)
            strMsgRpta = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)

        Catch ex As Exception
            dblIGV = Funciones.CheckDecimal(ConfigurationSettings.AppSettings("constIGV"))
            strCodRpta = -99
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Activaciones\clsConsultaPvu.vb; Function: ObtenerIGV)"
            strMsgRpta = "ERROR: " & ex.Message.ToString() & MaptPath
            'FIN PROY-140126

        End Try

        Return dblIGV

    End Function
    'PROY-31766 - FIN

    'PROY-33313 RP INICIO
    Public Function FnActualizaFlagCuota(ByVal sNroPedido As Int64, ByVal sEstadoFlag As String, ByRef sMsjRespuesta As String) As Boolean

        Try
            Dim sCodRespuesta As String
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("P_NRO_PEDIDO", DbType.Int64, sNroPedido, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_ESTADO", DbType.String, sEstadoFlag, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_NRO_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("P_DES_RESULTADO", DbType.String, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_DRA_CVE_6.SISACSU_ACTUALIZAR_CUOTA"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            sCodRespuesta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            sMsjRespuesta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

            If sCodRespuesta.Equals("0") Then
                FnActualizaFlagCuota = True
            End If

            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()

        Catch ex As Exception
            FnActualizaFlagCuota = False
            sMsjRespuesta = ex.Message.ToString()
        End Try

    End Function

    Public Function FnTipificaChipSueltoUnaCuota(ByVal oBETipiChipSuelto As BETipiChipSuelto, ByRef sCodRespuesta As String, ByRef sMsjRespuesta As String) As Boolean

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("p_ventv_nropedido", DbType.String, oBETipiChipSuelto.VENTV_NROPEDIDO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_nrosec", DbType.String, oBETipiChipSuelto.VENTV_NROSEC, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventc_tipodocumento", DbType.String, oBETipiChipSuelto.VENTC_TIPODOCUMENTO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_nrodocumento", DbType.String, oBETipiChipSuelto.VENTV_NRODOCUMENTO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventc_linea", DbType.String, oBETipiChipSuelto.VENTC_LINEA, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_producto", DbType.String, oBETipiChipSuelto.VENTV_PRODUCTO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventd_fec_operacion", DbType.Date, oBETipiChipSuelto.VENTD_FEC_OPERACION, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_aplicacion", DbType.String, oBETipiChipSuelto.VENTV_APLICACION, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_tip_operacion", DbType.String, oBETipiChipSuelto.VENTV_TIP_OPERACION, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_pdv", DbType.String, oBETipiChipSuelto.VENTV_PDV, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_estado", DbType.String, oBETipiChipSuelto.VENTV_ESTADO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_modalidad", DbType.String, oBETipiChipSuelto.VENTV_MODALIDAD, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventc_tipdocvend", DbType.String, oBETipiChipSuelto.VENTC_TIPDOCVEND, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_nrodocvend", DbType.String, oBETipiChipSuelto.VENTV_NRODOCVEND, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_codvendedor", DbType.String, oBETipiChipSuelto.VENTV_CODVENDEDOR, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_iccid", DbType.String, oBETipiChipSuelto.VENTV_ICCID, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_plan", DbType.String, oBETipiChipSuelto.VENTV_PLAN, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_canal", DbType.String, oBETipiChipSuelto.VENTV_CANAL, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_usuario_crea", DbType.String, oBETipiChipSuelto.VENTV_USUARIO_CREA, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_usuario_modif", DbType.String, oBETipiChipSuelto.VENTV_USUARIO_MODIF, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventn_monto", DbType.Decimal, oBETipiChipSuelto.VENTN_MONTO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_imei", DbType.String, oBETipiChipSuelto.VENTV_IMEI, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_tipo", DbType.String, oBETipiChipSuelto.VENTV_TIPO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_id_onbase", DbType.String, oBETipiChipSuelto.VENTV_ID_ONBASE, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("P_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_ACUERDO_6.SISACTSI_CLIENTE_VENTA_TIPI" 'PCK ORIGINAL: SISACT_PKG_ACUERDO_6 / SISACT_PKG_ACUERDO_6_P33313
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            sCodRespuesta = Convert.ToString(CType(objRequest.Parameters(24), IDataParameter).Value)  'PROY-140360-IDEA-46301 
            sMsjRespuesta = Convert.ToString(CType(objRequest.Parameters(25), IDataParameter).Value)  'PROY-140360-IDEA-46301

            If sCodRespuesta.Equals("0") Then
                FnTipificaChipSueltoUnaCuota = True
            End If

            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()

        Catch ex As Exception
            FnTipificaChipSueltoUnaCuota = False
            sMsjRespuesta = ex.Message.ToString()
        End Try

    End Function
    'PROY-33313 RP FIN
    ' PROY-140360-IDEA-46301 INICIO
    Public Function FnTipificaContratoCodeUnaCuota(ByVal oBETipiContratoCode As BETipiChipSuelto, ByRef strCodigoRespuesta As String, ByRef strMsgRespuesta As String) As Boolean

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("p_ventv_nropedido", DbType.String, oBETipiContratoCode.VENTV_NROPEDIDO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_nrosec", DbType.String, oBETipiContratoCode.VENTV_NROSEC, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventc_tipodocumento", DbType.String, oBETipiContratoCode.VENTC_TIPODOCUMENTO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_nrodocumento", DbType.String, oBETipiContratoCode.VENTV_NRODOCUMENTO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventc_linea", DbType.String, oBETipiContratoCode.VENTC_LINEA, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_producto", DbType.String, oBETipiContratoCode.VENTV_PRODUCTO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventd_fec_operacion", DbType.Date, oBETipiContratoCode.VENTD_FEC_OPERACION, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_aplicacion", DbType.String, oBETipiContratoCode.VENTV_APLICACION, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_tip_operacion", DbType.String, oBETipiContratoCode.VENTV_TIP_OPERACION, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_pdv", DbType.String, oBETipiContratoCode.VENTV_PDV, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_estado", DbType.String, oBETipiContratoCode.VENTV_ESTADO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_modalidad", DbType.String, oBETipiContratoCode.VENTV_MODALIDAD, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventc_tipdocvend", DbType.String, oBETipiContratoCode.VENTC_TIPDOCVEND, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_nrodocvend", DbType.String, oBETipiContratoCode.VENTV_NRODOCVEND, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_codvendedor", DbType.String, oBETipiContratoCode.VENTV_CODVENDEDOR, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_iccid", DbType.String, oBETipiContratoCode.VENTV_ICCID, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_plan", DbType.String, oBETipiContratoCode.VENTV_PLAN, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_canal", DbType.String, oBETipiContratoCode.VENTV_CANAL, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_usuario_crea", DbType.String, oBETipiContratoCode.VENTV_USUARIO_CREA, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_usuario_modif", DbType.String, oBETipiContratoCode.VENTV_USUARIO_MODIF, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventn_monto", DbType.Decimal, oBETipiContratoCode.VENTN_MONTO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_imei", DbType.String, oBETipiContratoCode.VENTV_IMEI, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_tipo", DbType.String, oBETipiContratoCode.VENTV_TIPO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_id_onbase", DbType.String, oBETipiContratoCode.VENTV_ID_ONBASE, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("P_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output)}
            'p_ventv_id_onbase 
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_ACUERDO_6.SISACTSI_CLIENTE_VENTA_TIPI" 'PCK ORIGINAL: SISACT_PKG_ACUERDO_6 / SISACT_PKG_ACUERDO_6_P33313
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            strCodigoRespuesta = Convert.ToString(CType(objRequest.Parameters(24), IDataParameter).Value)
            strMsgRespuesta = Convert.ToString(CType(objRequest.Parameters(25), IDataParameter).Value)

            If strCodigoRespuesta.Equals("0") Then
                FnTipificaContratoCodeUnaCuota = True
            End If

            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()

        Catch ex As Exception
            FnTipificaContratoCodeUnaCuota = False
            strMsgRespuesta = ex.Message.ToString()
        End Try

    End Function

    Public Function FnAnularTipificacion(ByVal strIdentificador As String, ByVal strNroPedido As String, ByVal strEstadoPedido As String, ByRef strCodRespAnulacion As String, ByRef strMsgRespAnulacion As String) As Boolean

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("p_ventv_identificador", DbType.String, strIdentificador, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_nropedido", DbType.String, strNroPedido, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("p_ventv_estadopedido", DbType.String, strEstadoPedido, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_CODIGO_RESPUESTA", DbType.String, strCodRespAnulacion, ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("P_MENSAJE_RESPUESTA", DbType.String, strMsgRespAnulacion, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_ACUERDO_6.SISACTSU_CLIENTE_VENTA_TIPI"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            strCodRespAnulacion = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            strMsgRespAnulacion = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)

            If strCodRespAnulacion.Equals("0") And strMsgRespAnulacion.Equals("OK") Then
                FnAnularTipificacion = True
            End If

            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()

        Catch ex As Exception
            FnAnularTipificacion = False
            strMsgRespAnulacion = ex.Message.ToString()
        End Try

    End Function

    'FIN PROY-140360-IDEA-46301

    Public Function ValidarIdentidadXSec(ByVal intInputNumSec As Int64, ByRef strOutCodRpta As String, ByRef strOutMgsRpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NUM_SEC", DbType.Int64, intInputNumSec, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CURSOR", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("P_COD_RESPUESTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("P_MSJ_RESPUESTA", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_VALIDADOR_IDENTIDAD.VIDSS_IDENTIDAD_HITXSEC"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)

            strOutCodRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            strOutMgsRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            ValidarIdentidadXSec = dsPool
        Catch ex As Exception
            strOutCodRpta = 99
            strOutMgsRpta = ex.Message.Trim()
            Return Nothing
        End Try

    End Function

    'INI INC000002161718
    Public Function ValidarWhitelist(ByRef P_EXISTE As String, _
                                    ByRef P_CF_MAX As Double, _
                                    ByVal P_TIPO_DOC As String, _
                                    ByVal P_NRO_DOC As String, _
                                    ByVal P_CASO_ESPECIAL As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_EXISTE", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_CF_MAX", DbType.Double, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_TIPO_DOC", DbType.String, P_TIPO_DOC, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NRO_DOC", DbType.String, P_NRO_DOC, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CASO_ESPECIAL", DbType.String, P_CASO_ESPECIAL, ParameterDirection.Input)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_EVALUACION_UNI.SP_CON_CE_WHITELIST"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteDataset(objRequest)

            P_EXISTE = Funciones.CheckStr(CType(objRequest.Parameters(0), IDataParameter).Value)
            P_CF_MAX = Funciones.CheckDbl(CType(objRequest.Parameters(1), IDataParameter).Value)

            ValidarWhitelist = True
        Catch ex As Exception
            P_EXISTE = ""
            P_CF_MAX = 0
            ValidarWhitelist = False
        End Try
        'FIN INC000002161718
    End Function

    Public Function GetDatosPedidoDelivery(ByVal intIdPedido As Int64, ByVal intIdSec As String, ByVal strTipoDocMot As String, ByVal strNumDocMot As String, ByRef strOutCodRpta As String, ByRef strOutMgsRpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_NROPEDIDO", DbType.Int64, intIdPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_NROSEC", DbType.Int64, intIdSec, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TIPODOCM", DbType.String, strTipoDocMot, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_NRODOCM", DbType.String, strNumDocMot, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_CURSOR_PEDIDO", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSG", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_EXP_3PLAY_6.BIOMOVSS_PEDIDODELIVERY"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)

            strOutCodRpta = Funciones.CheckStr(CType(objRequest.Parameters(5), IDataParameter).Value)
            strOutMgsRpta = Funciones.CheckStr(CType(objRequest.Parameters(6), IDataParameter).Value)
            GetDatosPedidoDelivery = dsPool
        Catch ex As Exception
            strOutCodRpta = "-99"
            strOutMgsRpta = String.Format("{0} => {1}", "Error: GetDatosPedidoDelivery", Funciones.CheckStr(ex.Message))
            Return Nothing
        End Try

    End Function


    Public Function validarPinPortabilidad(ByVal sNroSec As String, ByRef strOutCodRpta As String) As Boolean 'PROY-140485 - INICIO

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim Resultado As Boolean = False

        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_NRO_SEC", DbType.String, sNroSec, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, ParameterDirection.Output), _
                                                            New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SMS_PORTABILIDADES.SISACTS_VALIDA_PIN_SMS"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteReader(objRequest)
            Resultado = True

            strOutCodRpta = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)

        Catch ex As Exception
            Resultado = False
            Return Resultado
        End Try
        validarPinPortabilidad = Resultado
    End Function 'PROY-140585-IDEA-29841 - FIN

    'PROY-140590 IDEA142068 - INICIO
    Public Function ConsultaCampaniaBin(ByVal strCampana As String, ByVal strBin As String, ByRef strCodRpta As String, ByRef strMgsRpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_CAMPANIA", DbType.String, strCampana, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_BIN", DbType.String, strBin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_CUR_SALIDA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSG_RPTA", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CAMPANA.SISACTSS_CAMPANIA_BIN"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value)
            strMgsRpta = Funciones.CheckStr(CType(objRequest.Parameters(4), IDataParameter).Value)
            ConsultaCampaniaBin = dsPool
        Catch ex As Exception
            strCodRpta = "-99"
            strMgsRpta = String.Format("{0} => {1}", "Error: ConsultaCampaniaBin", Funciones.CheckStr(ex.Message))
            Return Nothing
        End Try
    End Function

    Public Function GuardarCampaniaPedidoBin(ByVal strNumPedido As Int64, ByVal strCodTarjeta As String, ByVal strTipoTarjeta As Int64, ByVal strOrigen As String, ByVal strUsuario As String, _
    ByVal strTipoDoc As String, ByVal strNumDoc As String, ByVal strFechaVenta As String, ByVal strTipoOper As String, ByVal strTipoVenta As String, ByVal strDescVenta As String, _
    ByVal strMontoTotal As String, ByVal strMontoPago As String, ByRef strMsjRpta As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam As DAABRequest.Parameter() = {New DAABRequest.Parameter("PI_PEDIN_NROPEDIDO", DbType.Int64, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("PI_CODIGO_TARJETA", DbType.String, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("PI_TIPO_TARJETA", DbType.Int64, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("PI_APPORIGEN", DbType.String, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("PI_USUARIO_REGISTRO", DbType.String, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("PI_TIPO_DOC", DbType.String, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("PI_NUMERO_DOC", DbType.String, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("PI_FECHA_VENTA", DbType.Date, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("PI_TIPO_OPER", DbType.String, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("PI_TIPO_VENTA", DbType.String, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("PI_DESC_VENTA", DbType.String, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("PI_MONTO_TOTAL", DbType.Double, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("PI_MONTO_PAGO", DbType.Double, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("PO_COD_RPTA", DbType.String, ParameterDirection.Output), _
                                                   New DAABRequest.Parameter("PO_MSG_RPTA", DbType.String, ParameterDirection.Output)}


        arrParam(0).Value = Funciones.CheckInt64(strNumPedido)
        arrParam(1).Value = Funciones.CheckStr(strCodTarjeta)
        arrParam(2).Value = Funciones.CheckInt64(strTipoTarjeta)
        arrParam(3).Value = Funciones.CheckStr(strOrigen)
        arrParam(4).Value = Funciones.CheckStr(strUsuario)
        arrParam(5).Value = Funciones.CheckStr(strTipoDoc)
        arrParam(6).Value = Funciones.CheckStr(strNumDoc)

        If Not strFechaVenta = String.Empty Then
            arrParam(7).Value = Funciones.CheckDate(strFechaVenta)
        End If

        arrParam(8).Value = Funciones.CheckStr(strTipoOper)
        arrParam(9).Value = Funciones.CheckStr(strTipoVenta)
        arrParam(10).Value = Funciones.CheckStr(strDescVenta)

        If Not strMontoTotal = String.Empty Then
            arrParam(11).Value = Funciones.CheckDbl(strMontoTotal)
        End If

        If Not strMontoPago = String.Empty Then
            arrParam(12).Value = Funciones.CheckDbl(strMontoPago)
        End If

        arrParam(13).Value = DBNull.Value
        arrParam(14).Value = DBNull.Value

        Dim salida As String = String.Empty

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_CAMPANA.SISACTSI_CAMPANIA_PEDIDO_BIN"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Transactional = True

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Factory.CommitTransaction()

            salida = Funciones.CheckStr(DirectCast(objRequest.Parameters(13), IDataParameter).Value)
            strMsjRpta = Funciones.CheckStr(DirectCast(objRequest.Parameters(14), IDataParameter).Value)

        Catch ex As Exception
            salida = "-99"
            strMsjRpta = String.Format("{0} => {1}", "Error: GuardarCampaniaPedidoBin", Funciones.CheckStr(ex.Message))
            objRequest.Factory.RollBackTransaction()
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return salida

    End Function

    Public Function ConsultaDatosCampania(ByVal strNroPedido As String, ByVal strTipoVenta As String, ByVal strOperacion As String, ByRef strCodRpta As String, ByRef strMgsRpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_PEDIN_NROPEDIDO", DbType.String, strNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TIPO_VENTA", DbType.String, strTipoVenta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_OPERACION", DbType.String, strOperacion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_CUR_SALIDA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSG_RPTA", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CAMPANA.SISACTSS_DATOS_VENTA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Funciones.CheckStr(CType(objRequest.Parameters(4), IDataParameter).Value)
            strMgsRpta = Funciones.CheckStr(CType(objRequest.Parameters(5), IDataParameter).Value)
            ConsultaDatosCampania = dsPool
        Catch ex As Exception
            strCodRpta = "-99"
            strMgsRpta = String.Format("{0} => {1}", "Error: ConsultaDatosCampania", Funciones.CheckStr(ex.Message))
            Return Nothing
        End Try
    End Function
    'PROY-140590 IDEA142068 - FIN

'JLOPETAS - PROY 140589 - INI
    Public Function ConsultarFlagsPicking(ByVal codigo_oficina As String, ByRef cod_rpta As String, ByRef msj_rpta As String) As ItemGenerico

        Dim oRespuesta As ItemGenerico = Nothing
        Dim dr As IDataReader = Nothing
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_COD_OFI", DbType.String, codigo_oficina, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESP", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSG_RESP", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_CURSOR", DbType.Object, ParameterDirection.Output)}

        For i As Integer = 0 To i < arrParam.Length
            arrParam(i).Value = DBNull.Value
        Next
        arrParam(0).Value = codigo_oficina

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONSULTA_GENERAL.SISACTSS_OFI_PICKING"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Transactional = True
            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader

            If Not dr Is Nothing Then
                While dr.Read()
                    oRespuesta = New ItemGenerico
                    oRespuesta.DESCRIPCION = Funciones.CheckStr(dr("DESCRIPCION_OFI")) 'Funciones.CheckStr(dr("DESCRIPCION_OFI"))
                    oRespuesta.FLAG_PICKING = Funciones.CheckStr(dr("FLAG_PDK"))
                    oRespuesta.FLAG_DLV = Funciones.CheckStr(dr("FLAG_DELIVERY"))
                    oRespuesta.FLAG_PAGOENLINEA = Funciones.CheckStr(dr("FLAG_PAGOENLINEA"))
                End While
            End If

            Dim parSalida1 As IDataParameter
            Dim parSalida2 As IDataParameter

            parSalida1 = CType(objRequest.Parameters(1), IDataParameter)
            parSalida2 = CType(objRequest.Parameters(2), IDataParameter)

            cod_rpta = Funciones.CheckStr(parSalida1.Value)
            msj_rpta = Funciones.CheckStr(parSalida2.Value)

        Catch ex As Exception
            oRespuesta = Nothing
            cod_rpta = "-2"
            msj_rpta = Funciones.CheckStr(ex.Message)

        Finally
            If Not dr Is Nothing AndAlso Not dr.IsClosed Then dr.Close()
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return oRespuesta
    End Function
    'JLOPETAS - PROY 140589 - FIN

   'PROY-140662 - DLV-F4 INI
    Public Function GetOrdenTOA(ByVal intNroPedido As Int64, ByRef flagDLV As String, ByRef IdOrdenTOA As String, ByRef strCodRPT As String, ByRef strMsjRpt As String) As Boolean

        Dim blRespuesta As Boolean
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NROPEDIDO", DbType.Int64, intNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_FLAG_DLV", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_ID_ORDENTOA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSG", DbType.String, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_EXP_3PLAY_6.SISACTS_GET_ORDEN_TOA"
            objRequest.Parameters.AddRange(arrParam)


            flagDLV = Funciones.CheckStr(CType(objRequest.Parameters(1), IDataParameter).Value)
            IdOrdenTOA = Funciones.CheckStr(CType(objRequest.Parameters(2), IDataParameter).Value)
            strCodRPT = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value)
            strMsjRpt = Funciones.CheckStr(CType(objRequest.Parameters(4), IDataParameter).Value)

            If strCodRPT = "0" OrElse strCodRPT = "1" Then
                blRespuesta = True

            Else
                blRespuesta = False

            End If

        Catch ex As Exception
            blRespuesta = False
            strCodRPT = "-99"
            strMsjRpt = String.Format("{0} => {1}", "Error: GetOrdenTOA", Funciones.CheckStr(ex.StackTrace))
        End Try

        Return blRespuesta

    End Function

    Public Function InsertDLVProg(ByVal intNroPedido As Int64, ByVal idSubEstado As Integer, ByVal idSubMotivo As Integer, ByVal strDesMotivo As String, ByVal strDesObservacion As String, ByVal dateFecAnterior As Date, _
    ByVal dateFecRreprog As Date, ByVal strTipoEntrega As String, ByVal strFranjaHoraria As String, ByVal strUsuarioCrea As String, ByVal strSistema As String, ByRef strCodResult As String) As Boolean

        Dim blRespuesta As Boolean
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_SDVN_NROPEDIDO", DbType.Int64, intNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_SDVN_IDSUBESTADO", DbType.Int64, idSubEstado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_SDVN_IDMOTIVO", DbType.String, idSubMotivo, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_SDVV_DESC_MOTIVO", DbType.String, strDesMotivo, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_SDVV_OBSERVACION", DbType.String, strDesObservacion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_SDVD_FECANTERIOR", DbType.Date, dateFecAnterior, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_SDVD_FECREPROG", DbType.Date, dateFecRreprog, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_SDVV_TIPOENTREGA", DbType.String, strTipoEntrega, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_SDVV_FRANJAHORARIA", DbType.String, strFranjaHoraria, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_SDVV_USUARIOCREA", DbType.String, strUsuarioCrea, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_SDVD_FECHACREA", DbType.Date, Date.Now, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_SDMV_IDMOTIVOADC", DbType.Int64, idSubMotivo, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_SDVV_SISTEMA", DbType.String, strSistema, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_EXP_3PLAY_6.SISACSI_DLV_REPROG"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Transactional = True

            strCodResult = Funciones.CheckStr(DirectCast(objRequest.Parameters(13), IDataParameter).Value)

            If strCodResult = "1" Then
                'objRequest.Factory.CommitTransaction()

            End If

        Catch ex As Exception
            objRequest.Factory.RollBackTransaction()
            'strMsjRpta = String.Format("{0} => {1}", "Error: GuardarCampaniaPedidoBin", Funciones.CheckStr(ex.Message))
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()

        End Try

    End Function

    'PROY-140662 - DLV-F4 FIN


End Class
