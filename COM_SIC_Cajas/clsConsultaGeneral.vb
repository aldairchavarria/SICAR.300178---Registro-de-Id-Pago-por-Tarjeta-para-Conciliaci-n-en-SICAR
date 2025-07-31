Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB

'**E75810 11/02/2011
Public Class clsConsultaGeneral
		Dim CadenaConexion As String
    Dim objSeg As New Seguridad_NET.clsSeguridad

    Public Function FP_Get_ListaServicios() As DataTable
        Dim datDatos As DataTable
        Dim dseDatos As DataSet
        Dim objRequest As DAAB.DAABRequest
        Try
            Me.CadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
            objRequest = New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, Me.CadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                     New DAAB.DAABRequest.Parameter("P_DATOS", DbType.Object, ParameterDirection.Output) _
                      }

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISCAJ.SP_GET_SERVICIOS"
            objRequest.Parameters.AddRange(arrParam)

            dseDatos = objRequest.Factory.ExecuteDataset(objRequest)
            '--recupera conjuntos de datos
            If ((Not dseDatos Is Nothing) AndAlso (Not dseDatos.Tables Is Nothing)) Then
                datDatos = dseDatos.Tables(0)
            End If
            dseDatos = Nothing
        Catch ex As Exception
            Throw ex
        Finally
            objRequest.Factory.Dispose()
            objRequest.Parameters.Clear()
        End Try
        '--
        Return datDatos
    End Function

    '**E75810 24/05/2011
    Public Shared Function FP_GetCodigoTramaViaPago(ByVal pCodigoSAP_SGA As String) As String
        '---
        Dim oCodigoTrama As Object
        Dim objSeg As New Seguridad_NET.clsSeguridad
        Dim sCadenaConexion As String = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, sCadenaConexion)

        Try
            '--define e inicializa parámetros
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                New DAAB.DAABRequest.Parameter("P_CODIGO_SAP_SGA", DbType.String, 6, pCodigoSAP_SGA, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_CODIGO_TRAMA", DbType.String, 2, ParameterDirection.Output) _
                }
            objRequest.Parameters.AddRange(arrParam)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISCAJ.SP_GET_CODIGO_TRAMA_VIA"
            '--
            objRequest.Factory.ExecuteScalar(objRequest)
            '--
            oCodigoTrama = CType(objRequest.Parameters(1), IDataParameter).Value
            '--retorna respuesta
            If (IsNothing(oCodigoTrama) Or IsDBNull(oCodigoTrama)) Then
                Return String.Empty
            Else
                Return oCodigoTrama.ToString
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objRequest.Factory.Dispose()
            objRequest.Parameters.Clear()
        End Try
    End Function

    Public Function FP_Get_ListaBancos() As DataTable
        Dim datDatos As DataTable
        Dim dseDatos As DataSet
        Dim objRequest As DAAB.DAABRequest
        Try
            Me.CadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
            objRequest = New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, Me.CadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                     New DAAB.DAABRequest.Parameter("P_DATOS", DbType.Object, ParameterDirection.Output) _
                      }

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISCAJ.SP_GET_BANCOS_TRAMA"
            objRequest.Parameters.AddRange(arrParam)

            dseDatos = objRequest.Factory.ExecuteDataset(objRequest)
            '--recupera conjuntos de datos
            If ((Not dseDatos Is Nothing) AndAlso (Not dseDatos.Tables Is Nothing)) Then
                datDatos = dseDatos.Tables(0)
            End If
            dseDatos = Nothing
        Catch ex As Exception
            Throw ex
        Finally
            objRequest.Factory.Dispose()
            objRequest.Parameters.Clear()
        End Try
        '--
        Return datDatos
    End Function

    'E75810 30/05/2011
    Public Function FP_Get_ListaViasPago() As DataTable
        Dim datDatos As DataTable
        Dim dseDatos As DataSet
        Dim objRequest As DAAB.DAABRequest
        Try
            Me.CadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
            objRequest = New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, Me.CadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                     New DAAB.DAABRequest.Parameter("P_DATOS", DbType.Object, ParameterDirection.Output) _
                      }

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISCAJ.SP_GET_VIAS_PAGO_TRAMA"
            objRequest.Parameters.AddRange(arrParam)

            dseDatos = objRequest.Factory.ExecuteDataset(objRequest)
            '--recupera conjuntos de datos
            If ((Not dseDatos Is Nothing) AndAlso (Not dseDatos.Tables Is Nothing)) Then
                datDatos = dseDatos.Tables(0)
            End If
            dseDatos = Nothing
        Catch ex As Exception
            Throw ex
        Finally
            objRequest.Factory.Dispose()
            objRequest.Parameters.Clear()
        End Try
        '--
        Return datDatos
    End Function


    '//---------------------------------------------PasarelaDePago__OLIVER_RIVERA---------------------------------------------//

    Public Function FP_Get_ListaVentasCEL(ByVal P_OFICINA_VENTA As String, _
                                          ByVal P_FECHA_VENTA As String, _
                                          ByVal P_TIPO_POOL As String, _
                                          ByVal P_FECHA_HASTA As String, _
                                          ByVal P_NRO_DOC_CLIENTE As String, _
                                          ByVal P_TIPO_DOC_CLIENTE As String) As DataSet
        Dim datDatos As DataTable
        Dim dseDatos As DataSet
        Dim objRequest As DAAB.DAABRequest
        Dim strOwner As String = ConfigurationSettings.AppSettings("strOwnerCAE").ToString()
        Try

            Me.CadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("strKeyRegEditCAE"))
            objRequest = New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, Me.CadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CUR_RESP", DbType.Object, ParameterDirection.Output), _
                                                            New DAAB.DAABRequest.Parameter("P_OFICINA_VENTA", DbType.String, 20, P_OFICINA_VENTA, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_FECHA_VENTA", DbType.String, 10, P_FECHA_VENTA, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_TIPO_POOL", DbType.String, 1, P_TIPO_POOL, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_FECHA_HASTA", DbType.String, 10, P_FECHA_HASTA, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_NRO_DOC_CLIENTE", DbType.String, 10, P_NRO_DOC_CLIENTE, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_TIPO_DOC_CLIENTE", DbType.String, 10, P_TIPO_DOC_CLIENTE, ParameterDirection.Input)}


            objRequest.CommandType = CommandType.StoredProcedure
            If strOwner <> "" Then
                objRequest.Command = strOwner & ".PKG_PASAT_REPORTE.CE_CON_POOL_PAGOS"
            Else
                objRequest.Command = "PKG_PASAT_REPORTE.CE_CON_POOL_PAGOS"
            End If

            objRequest.Parameters.AddRange(arrParam)

            FP_Get_ListaVentasCEL = objRequest.Factory.ExecuteDataset(objRequest)

        Catch ex As Exception
            Throw ex
        Finally
            objRequest.Factory.Dispose()
            objRequest.Parameters.Clear()
        End Try

    End Function


    Public Function FP_Actualiza_Estado_Emision_CEL(ByVal P_OFICINA_VENTA As String, _
                                                    ByVal P_NRO_SAP As String) As Integer

        Dim datDatos As DataTable
        Dim dseDatos As DataSet
        Dim objRequest As DAAB.DAABRequest
        Dim strOwner As String = ConfigurationSettings.AppSettings("strOwnerCAE").ToString()
        Try

            Me.CadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("strKeyRegEditCAE"))
            objRequest = New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, Me.CadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("P_RESP", DbType.Int32, 10, ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("P_MENSAJE", DbType.String, 100, ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("P_OFICINA_VENTA", DbType.String, 20, P_OFICINA_VENTA, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_NRO_DOC_SAP", DbType.String, 15, P_NRO_SAP, ParameterDirection.Input)}


            objRequest.CommandType = CommandType.StoredProcedure
            If strOwner <> "" Then
                objRequest.Command = strOwner & ".PKG_PASAT_REPORTE.CE_UPD_FLAG_COMPROBANTE"
            Else
                objRequest.Command = "PKG_PASAT_REPORTE.CE_UPD_FLAG_COMPROBANTE"
            End If

            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)

            FP_Actualiza_Estado_Emision_CEL = CType(objRequest.Parameters(0), IDataParameter).Value

        Catch ex As Exception
            Throw ex
        Finally
            objRequest.Factory.Dispose()
            objRequest.Parameters.Clear()
        End Try


    End Function


    '//---------------------------------------------PasarelaDePago__OLIVER_RIVERA---------------------------------------------//

    Public Function ListadoPlazoAcuerdo() As DataSet
        CadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, CadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CONSULTA", DbType.Object, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_EVALUACION_CONS_2.SISACT_LISTAR_PLAZOACUERDO"
        objRequest.Parameters.AddRange(arrParam)
        ListadoPlazoAcuerdo = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

End Class
