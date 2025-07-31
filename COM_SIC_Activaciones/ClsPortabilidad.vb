Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB

Public Class ClsPortabilidad

    Dim objSeg As New Seguridad_NET.clsSeguridad
    Dim strCadenaConexion As String

    Dim PKG_SISACT_PKG_PORTABILIDAD_6 As String = "SISACT_PKG_PORTABILIDAD_6"
    Dim SISACTSS_VALIDA_PAGO As String = ".SISACTSS_VALIDA_PAGO"

    Dim PKG_SISACT_PKG_PORTABILIDAD_PREP As String = "SISACT_PKG_PORTABILIDAD_PREP"
    Dim SP_GET_TELEFONO_PORTABILIDAD As String = ".SP_GET_TELEFONO_PORTABILIDAD"
 Dim PKG_PORTABILIDAD_MIGRA As String = "SISACT_PKG_PORTABILIDAD_MIGRA" 'PROY-26963 - F2 
    Dim SP_SISASS_VALIDA_SEC_SP As String = ".SISASS_VALIDA_SEC_SP" 'PROY-26963 - F2 
    Dim SP_ListarLineasPendientes As String = ".SISASS_LISTAR_LINEAS_PEND" 'PROY-26963 - F2

    Public Function ObtenerLineasPortabilidadPost(ByVal P_SOLIN_CODIGO As Int64, _
                                            ByVal P_PORT_SOLICITUD_ID As Int64, _
                                            ByVal P_EST_SEC As String, _
                                            ByVal P_EST_SP As String, _
                                            ByVal P_SOPOC_CHIPPACK As String _
                                            ) As DataSet


        Dim strPortId As String
        If P_PORT_SOLICITUD_ID = 0 Then
            strPortId = Nothing
        Else
            strPortId = P_PORT_SOLICITUD_ID
        End If




        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPedido As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                  New DAAB.DAABRequest.Parameter("P_SOLIN_CODIGO", DbType.Int64, 10, P_SOLIN_CODIGO, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("P_PORT_SOLICITUD_ID", DbType.Int64, 4, strPortId, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("P_EST_SEC", DbType.String, 20, P_EST_SEC, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("P_EST_SP", DbType.String, 20, P_EST_SP, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("P_SOPOC_CHIPPACK", DbType.String, 4, P_SOPOC_CHIPPACK, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_SISACT_PKG_PORTABILIDAD_6 & SISACTSS_VALIDA_PAGO
            objRequest.Parameters.AddRange(arrParam)
            dsPedido = objRequest.Factory.ExecuteDataset(objRequest)
            ObtenerLineasPortabilidadPost = dsPedido
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function ObtenerLineasPortabilidadPre(ByVal nroSec As Int64, _
                                            ByVal conEquipo As String _
                                            ) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPedido As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                  New DAAB.DAABRequest.Parameter("P_SOLIN_CODIGO", DbType.Int64, 10, nroSec, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("P_CON_EQUIPO", DbType.String, 10, conEquipo, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_SISACT_PKG_PORTABILIDAD_PREP & SP_GET_TELEFONO_PORTABILIDAD
            objRequest.Parameters.AddRange(arrParam)
            dsPedido = objRequest.Factory.ExecuteDataset(objRequest)
            ObtenerLineasPortabilidadPre = dsPedido
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'PROY-26963 F2 ini
    Public Function ValidarSEC(ByVal strNroSec As Int64, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPedido As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                  New DAAB.DAABRequest.Parameter("PI_SOLIN_CODIGO", DbType.Int64, 10, strNroSec, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("PO_CURSALIDA", DbType.Object, ParameterDirection.Output), _
                  New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, 2, ParameterDirection.Output), _
                  New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, 100, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_PORTABILIDAD_MIGRA & SP_SISASS_VALIDA_SEC_SP
            objRequest.Parameters.AddRange(arrParam)
            dsPedido = objRequest.Factory.ExecuteDataset(objRequest)
            strCodRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

            Return dsPedido
        Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: ValidarSEC - " & ex.Message.ToString()
            Return Nothing
        End Try
    End Function

    'INC000000961273 - INICIO
    Public Function Consulta_Ope_cedente(ByVal P_NRO_SEC As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRO_SEC", DbType.String, 10, P_NRO_SEC, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("P_CURSOR", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_SISACT_PKG_PORTABILIDAD_6 & ".SP_CON_OPE_CEDENTE"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            Consulta_Ope_cedente = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'INC000000961273 - FIN

    'PROY 31393 (Omision) - Inicio
    Public Function PorttValidaABCDP(ByVal objPortConfiguracion As BEPorttConfiguracion) As Array

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim result(2) As String
        result("0") = "1"
        result("1") = "Sin recuperar configuracion CP"
        result("2") = "|C|P|E|"

        Dim dr As IDataReader
        Dim dsPedido As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                  New DAABRequest.Parameter("PI_PORTV_EST_PROCESO", DbType.String, ParameterDirection.Input), _
                  New DAABRequest.Parameter("PI_PORTV_MOTIVO", DbType.String, ParameterDirection.Input), _
                  New DAABRequest.Parameter("PI_PORTN_FLAG_ACREDITA", DbType.Int64, ParameterDirection.Input), _
                  New DAABRequest.Parameter("PI_PORTV_OPERADOR", DbType.String, ParameterDirection.Input), _
                  New DAABRequest.Parameter("PI_PORTV_TIPO_PRODUCTO", DbType.String, ParameterDirection.Input), _
                  New DAABRequest.Parameter("PI_PORTV_TIPO_VENTA", DbType.String, ParameterDirection.Input), _
                  New DAABRequest.Parameter("PI_PORTV_APLICACION", DbType.String, ParameterDirection.Input), _
                  New DAABRequest.Parameter("PI_PORTV_MOD_VENTA", DbType.String, ParameterDirection.InputOutput), _
                  New DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, ParameterDirection.Output), _
                  New DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output)}


        For i As Integer = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(0).Value = objPortConfiguracion.PORTV_EST_PROCESO
        arrParam(1).Value = objPortConfiguracion.PORTV_MOTIVO
        arrParam(2).Value = objPortConfiguracion.PORTV_FLAG_ACREDITA
        arrParam(3).Value = objPortConfiguracion.PORTV_OPERADOR
        arrParam(4).Value = objPortConfiguracion.PORTC_TIPO_PRODUCTO
        arrParam(5).Value = objPortConfiguracion.PORTV_TIPO_VENTA
        arrParam(6).Value = objPortConfiguracion.PORTV_APLICACION
        arrParam(7).Value = objPortConfiguracion.PORTV_MOD_VENTA

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_SISACT_PKG_PORTABILIDAD_6 + ".SISACTSS_PORTT_VALIDA_ABDCP"
            objRequest.Parameters.AddRange(arrParam)
            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            result(0) = CType(objRequest.Parameters()(objRequest.Parameters.Count - 2), IDataParameter).Value
            result(1) = CType(objRequest.Parameters()(objRequest.Parameters.Count - 1), IDataParameter).Value
            result(2) = CType(objRequest.Parameters()(objRequest.Parameters.Count - 3), IDataParameter).Value

        Catch ex As Exception
            result(0) = "-1"
            result(1) = ex.Message
        Finally
            dr.Close()
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return result
    End Function
    'PROY 31393 (Omision) - Fin

 'INI ROY 32089
    Public Function Obtener_tipo_producto(ByVal pSolin_codigo As String, ByVal pnro_pedido As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                  New DAAB.DAABRequest.Parameter("PI_SOLIN_CODIGO", DbType.Int64, 10, pSolin_codigo, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("PI_SOPOC_NROPEDIDO", DbType.String, 20, pnro_pedido, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("PO_CURSALIDA", DbType.Object, ParameterDirection.Output), _
                  New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, 2, ParameterDirection.Output), _
                  New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, 100, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = PKG_PORTABILIDAD_MIGRA & ".SISASS_LISTAR_LINEAS_PEND"
            objRequest.Parameters.AddRange(arrParam)
            ds = objRequest.Factory.ExecuteDataset(objRequest)
            strCodRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
        Catch ex As Exception
            Return Nothing
        End Try
        Return ds
    End Function
    'FIN PROY 32089
End Class
