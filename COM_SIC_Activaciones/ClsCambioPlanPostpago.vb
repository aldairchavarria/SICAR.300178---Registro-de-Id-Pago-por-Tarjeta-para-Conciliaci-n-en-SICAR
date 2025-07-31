Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB
Imports COM_SIC_Activaciones


Public Class ClsCambioPlanPostpago

    Dim strCadenaConexion As String
    Dim objSeg As New Seguridad_NET.clsSeguridad


    Public Function ConsultaSolicitudNroSEC(ByVal strNroSEC As String) As ArrayList

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("P_SOLIN_CODIGO", DbType.String, strNroSEC, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(0).Value = strNroSEC

        Dim dr As IDataReader = Nothing
        Dim filas As New ArrayList

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_RENOVACION.SECT_SEL_SOLICITUD_POSPAGO"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            While (dr.Read())
                Dim item As New clsSolicitudPersona
                item.SOLIN_CODIGO = Funciones.CheckInt64(dr("SOLIN_CODIGO"))
                item.CLIEC_NUM_DOC = Funciones.CheckStr(dr("CLIEC_NUM_DOC"))
                item.TDOCC_CODIGO = Funciones.CheckStr(dr("TDOCC_CODIGO"))
                item.TPROC_CODIGO = Funciones.CheckStr(dr("TPROC_CODIGO"))
                item.TPROV_DESCRIPCION = Funciones.CheckStr(dr("TPROC_CODIGO"))
                item.SOPLN_TOPE_CONSUMO = Funciones.CheckStr(dr("SOPLN_TOPE_CONSUMO"))
                item.SOPLC_MONTO_TOTAL = Funciones.CheckStr(dr("SOPLC_MONTO_TOTAL"))
                item.SOPLN_TOPE_CF = Funciones.CheckStr(dr("SOPLC_MONTO_TOTAL"))
                item.PRDC_CODIGO = Funciones.CheckStr(dr("PRDC_CODIGO"))
                item.TOPEN_CODIGO = Funciones.CheckStr(dr("TOPEN_CODIGO"))

                filas.Add(item)
            End While
        Catch ex As Exception

        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()

        End Try

        Return filas

    End Function

    Public Function ListarEquitSadicionales(ByVal P_PLAN As String) As DataTable

        Dim dtResultado As New DataTable
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "BD_EIA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("P_PLAN", DbType.String, P_PLAN, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(0).Value = P_PLAN

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_MANT.EQUIT_SEL_SADICIONALES"
            objRequest.Parameters.AddRange(arrParam)

            dtResultado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Throw ex
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return dtResultado
    End Function

    Public Function ListarEquitCintsistema(ByVal P_PLAN As String) As DataTable

        Dim dtResultado As New DataTable
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "BD_EIA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("P_PLAN", DbType.String, P_PLAN, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(0).Value = P_PLAN

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_MANT.EQUIT_SEL_CINTSISTEMA"
            objRequest.Parameters.AddRange(arrParam)

            dtResultado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Throw ex
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return dtResultado
    End Function

    'agregado whzr 22062015

    Public Function ObtenerSolicitudPersonaCons(ByVal nroSEC As String) As clsSolicitudPersona
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                               New DAAB.DAABRequest.Parameter("P_SOLIN_CODIGO", DbType.Int64, ParameterDirection.Input), _
                                               New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output) _
                                               }


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next
        arrParam(0).Value = nroSEC


        Dim dr As IDataReader = Nothing
        Dim item As New clsSolicitudPersona

        Try


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_EVAL_CONS_2_3PLAY_.SECSS_DET_SOL_PERS_CONS"

            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()


            While (dr.Read())
                item.PACUV_DESCRIPCION = Funciones.CheckStr(dr("PACUV_DESCRIPCION"))
                item.PRDC_CODIGO = Funciones.CheckStr(dr("PRDC_CODIGO")) ''PROY-140573 -F2
            End While


        Catch ex As Exception
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return item
    End Function

    'agregado whzr 22062015
'pROY 25335 R2 INI

    Public Function ConsultaSolicitud_NROSEC(ByVal strNroSEC As String) As ArrayList

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("P_SOLIN_CODIGO", DbType.String, strNroSEC, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(0).Value = strNroSEC

        Dim dr As IDataReader = Nothing
        Dim filas As New ArrayList

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "sisact_pkg_cons_maestra_sap_6.SECT_SOLICITUD_POSPAGO"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            While (dr.Read())
                Dim item As New clsSolicitudPersona
                item.SOLIN_CODIGO = Funciones.CheckInt64(dr("SOLIN_CODIGO"))
                item.CLIEC_NUM_DOC = Funciones.CheckStr(dr("CLIEC_NUM_DOC"))
                item.MODALIDAD_VENTA = Funciones.CheckStr(dr("MODALIDAD_VENTA"))
                item.TPROC_CODIGO = Funciones.CheckStr(dr("TPROC_CODIGO"))
                item.TDOCC_CODIGO = Funciones.CheckStr(dr("TDOCC_CODIGO"))
                item.PRDC_CODIGO = Funciones.CheckStr(dr("PRDC_CODIGO"))
                item.TOPEN_CODIGO = Funciones.CheckStr(dr("TOPEN_CODIGO"))

                filas.Add(item)
            End While
        Catch ex As Exception

        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()

        End Try

        Return filas

    End Function
'pROY 25335 R2 FIN

End Class
