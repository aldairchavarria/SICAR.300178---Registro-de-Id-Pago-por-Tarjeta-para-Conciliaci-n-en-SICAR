Imports Claro.Datos
Imports Claro.Datos.DAAB
Imports System.Configuration

Public Class clsRecaudacionDAC
    Dim objSeg As New Seguridad_NET.clsSeguridad
    Dim strCadenaConexion As String = objSeg.FP_GetConnectionString("2", "SISCAJA")
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strPkgSiscajRecdac As String = "PKG_SISCAJ_RECDAC"

    Public Function ConsultarParametros( _
            ByVal str_Grupo As String, ByVal str_Key As String, ByVal str_Value As String, _
            ByRef strRptaCod As String, ByRef strRptaMsg As String) As DataTable
        Dim dtSubOficina As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("PI_GRUPO", DbType.String, str_Grupo, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_KEY", DbType.String, str_Key, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SPARV_VALUE", DbType.String, str_Value, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PO_CURSOR_DATOS", DbType.Object, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, strRptaCod, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("PO_MSG_RESULTADO", DbType.String, strRptaMsg, ParameterDirection.Output) _
        }

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSiscajRecdac & ".RECDACSS_PARAMETROS"
            objRequest.Parameters.AddRange(arrParam)
            dtSubOficina = objRequest.Factory.ExecuteDataset(objRequest)

            strRptaCod = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
            strRptaMsg = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)

            ConsultarParametros = dtSubOficina.Tables(0)

        Catch ex As Exception
            strRptaCod = "-99"
            strRptaMsg = "Error: al obtener parámetros - " & ex.Message.ToString()
        End Try
    End Function

    Public Function FiltrarSubOficina( _
       ByVal str_filtro As String, ByRef strRptaCod As String, ByRef strRptaMsg As String) As DataTable
        Dim dtSubOficina As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("PI_FILTRO", DbType.String, str_filtro, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, strRptaCod, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("PO_MSG_RESLTADO", DbType.String, strRptaMsg, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("PO_CUR_SALIDA", DbType.Object, ParameterDirection.Output) _
        }

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSiscajRecdac & ".RECDACSS_FILTRARSUBOF"
            objRequest.Parameters.AddRange(arrParam)
            dtSubOficina = objRequest.Factory.ExecuteDataset(objRequest)

            strRptaCod = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            strRptaMsg = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

            FiltrarSubOficina = dtSubOficina.Tables(0)

        Catch ex As Exception
            strRptaCod = "-99"
            strRptaMsg = "Error: al obtener al Filtrar Sub Oficinas - " & ex.Message.ToString()
        End Try
    End Function

    Public Function ConsultarSubOficina( _
        ByVal str_SUOFV_ID As String, ByVal str_SUOFC_PUNTO_VENTA As String, ByVal str_SUOFV_SUB_OFICINA As String, _
        ByRef strRptaCod As String, ByRef strRptaMsg As String) As DataTable
        Dim dtSubOficina As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_ID", DbType.String, str_SUOFV_ID, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFC_PUNTO_VENTA", DbType.String, str_SUOFC_PUNTO_VENTA, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_SUB_OFICINA", DbType.String, str_SUOFV_SUB_OFICINA, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, strRptaCod, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("PO_MSG_RESLTADO", DbType.String, strRptaMsg, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("PO_CUR_SALIDA", DbType.Object, ParameterDirection.Output) _
        }

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSiscajRecdac & ".RECDACSS_LISTARSUBOF"
            objRequest.Parameters.AddRange(arrParam)
            dtSubOficina = objRequest.Factory.ExecuteDataset(objRequest)

            strRptaCod = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            strRptaMsg = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)

            ConsultarSubOficina = dtSubOficina.Tables(0)

        Catch ex As Exception
            strRptaCod = "-99"
            strRptaMsg = "Error: al obtener las Sub Oficinas - " & ex.Message.ToString()
        End Try
    End Function

    Public Function InsertarSubOficina( _
        ByVal str_Punto_Venta As String, ByVal str_Sub_Oficina As String, ByVal str_Comentario As String, ByVal str_Estado As String, _
        ByVal str_CondiPago As String, ByVal str_ControlCre As String, ByVal str_CtaContable As String, ByVal str_Usuario As String, _
        ByRef strRptaCod As String, ByRef strRptaMsg As String) As Integer
        Dim dsInsert As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("PI_SUOFC_PUNTO_VENTA", DbType.String, 200, Funciones.CheckStr(str_Punto_Venta), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_SUB_OFICINA", DbType.String, 200, Funciones.CheckStr(str_Sub_Oficina), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_COMENTARIO", DbType.String, 200, Funciones.CheckStr(str_Comentario), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_ESTADO", DbType.String, 200, Funciones.CheckStr(str_Estado), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_CONDIPAGO", DbType.String, 200, Funciones.CheckStr(str_CondiPago), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_CONTROLCRE", DbType.String, 200, Funciones.CheckStr(str_ControlCre), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_CUENTACONTABLE", DbType.String, 200, Funciones.CheckStr(str_CtaContable), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_USER", DbType.String, 200, Funciones.CheckStr(str_Usuario), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, 200, strRptaCod, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("PO_MSG_RESLTADO", DbType.String, 200, strRptaMsg, ParameterDirection.Output) _
        }

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSiscajRecdac & ".RECDACSI_MNTSUBOF"
            objRequest.Parameters.AddRange(arrParam)
            dsInsert = objRequest.Factory.ExecuteDataset(objRequest)

            strRptaCod = Funciones.CheckStr(CType(objRequest.Parameters(8), IDataParameter).Value)
            strRptaMsg = Funciones.CheckStr(CType(objRequest.Parameters(9), IDataParameter).Value)

        Catch ex As Exception
            strRptaCod = "-99"
            strRptaMsg = "Error: al insertar Sub Oficina - " & ex.Message.ToString()
        End Try
    End Function

    Public Function ModificarSubOficina( _
        ByVal str_ID As String, ByVal str_Punto_Venta As String, ByVal str_Sub_Oficina As String, ByVal str_Comentario As String, _
        ByVal str_Estado As String, ByVal str_CondiPago As String, ByVal str_ControlCre As String, ByVal str_CtaContable As String, ByVal str_Usuario As String, _
        ByRef strRptaCod As String, ByRef strRptaMsg As String) As Integer
        Dim dsUpdate As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_ID", DbType.String, 200, Funciones.CheckStr(str_ID), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFC_PUNTO_VENTA", DbType.String, 200, Funciones.CheckStr(str_Punto_Venta), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_SUB_OFICINA", DbType.String, 200, Funciones.CheckStr(str_Sub_Oficina), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_COMENTARIO", DbType.String, 200, Funciones.CheckStr(str_Comentario), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_ESTADO", DbType.String, 200, Funciones.CheckStr(str_Estado), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_CONDIPAGO", DbType.String, 200, Funciones.CheckStr(str_CondiPago), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_CONTROLCRE", DbType.String, 200, Funciones.CheckStr(str_ControlCre), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_CUENTACONTABLE", DbType.String, 200, Funciones.CheckStr(str_CtaContable), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_SUOFV_USER", DbType.String, 200, Funciones.CheckStr(str_Usuario), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, 200, strRptaCod, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("PO_MSG_RESLTADO", DbType.String, 200, strRptaMsg, ParameterDirection.Output) _
        }

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSiscajRecdac & ".RECDACSU_MNTSUBOF"
            objRequest.Parameters.AddRange(arrParam)
            dsUpdate = objRequest.Factory.ExecuteDataset(objRequest)

            strRptaCod = Funciones.CheckStr(CType(objRequest.Parameters(9), IDataParameter).Value)
            strRptaMsg = Funciones.CheckStr(CType(objRequest.Parameters(10), IDataParameter).Value)

        Catch ex As Exception
            strRptaCod = "-99"
            strRptaMsg = "Error: al modificar Sub Oficina - " & ex.Message.ToString()
        End Try
    End Function

    Public Function ConsultarCajeroDAC( _
        ByVal str_ID As String, ByVal str_SubOficina As String, ByVal str_Cajero As String, _
        ByRef strRptaCod As String, ByRef strRptaMsg As String) As DataTable
        Dim dtSubOficina As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("PI_CASOV_ID", DbType.String, Funciones.CheckStr(str_ID), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_CASOV_SUB_OFICINA", DbType.String, Funciones.CheckStr(str_SubOficina), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_CASOC_CAJERO", DbType.String, Funciones.CheckStr(str_Cajero), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, strRptaCod, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("PO_MSG_RESLTADO", DbType.String, strRptaMsg, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("PO_CUR_SALIDA", DbType.Object, ParameterDirection.Output) _
        }

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSiscajRecdac & ".RECDACSS_LISTACAJXSUBOF"
            objRequest.Parameters.AddRange(arrParam)
            dtSubOficina = objRequest.Factory.ExecuteDataset(objRequest)

            strRptaCod = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            strRptaMsg = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)

            ConsultarCajeroDAC = dtSubOficina.Tables(0)

        Catch ex As Exception
            strRptaCod = "-99"
            strRptaMsg = "Error: al obtener cajero DAC - " & ex.Message.ToString()
        End Try
    End Function

    Public Function InsertarCajeroDAC( _
    ByVal str_SubOficina As String, ByVal str_Cajero As String, ByVal str_Comentario As String, ByVal str_Estado As String, ByVal str_Usuario As String, _
        ByRef strRptaCod As String, ByRef strRptaMsg As String) As Integer
        Dim dsInsert As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("PI_CASOV_SUB_OFICINA", DbType.String, 200, Funciones.CheckStr(str_SubOficina), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_CASOC_CAJERO", DbType.String, 200, Funciones.CheckStr(str_Cajero), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_CASOV_COMENTARIO", DbType.String, 200, Funciones.CheckStr(str_Comentario), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_CASOV_ESTADO", DbType.String, 200, Funciones.CheckStr(str_Estado), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_CASOV_USER", DbType.String, 200, Funciones.CheckStr(str_Usuario), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, 200, strRptaCod, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("PO_MSG_RESLTADO", DbType.String, 200, strRptaMsg, ParameterDirection.Output) _
        }

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSiscajRecdac & ".RECDACSI_MNTCAJXSUBOF"
            objRequest.Parameters.AddRange(arrParam)
            dsInsert = objRequest.Factory.ExecuteDataset(objRequest)

            strRptaCod = Funciones.CheckStr(CType(objRequest.Parameters(5), IDataParameter).Value)
            strRptaMsg = Funciones.CheckStr(CType(objRequest.Parameters(6), IDataParameter).Value)

        Catch ex As Exception
            strRptaCod = "-99"
            strRptaMsg = "Error: al insertar cajero DAC - " & ex.Message.ToString()
        End Try
    End Function

    Public Function ModificarCajeroDAC( _
        ByVal str_ID As String, ByVal str_SubOficina As String, ByVal str_Cajero As String, _
        ByVal str_Comentario As String, ByVal str_Estado As String, ByVal str_Usuario As String, _
        ByRef strRptaCod As String, ByRef strRptaMsg As String) As Integer
        Dim dsUpdate As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("PI_CASOV_ID", DbType.String, 200, Funciones.CheckStr(str_ID), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_CASOV_SUB_OFICINA", DbType.String, 200, Funciones.CheckStr(str_SubOficina), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_CASOC_CAJERO", DbType.String, 200, Funciones.CheckStr(str_Cajero), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_CASOV_COMENTARIO", DbType.String, 200, Funciones.CheckStr(str_Comentario), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_CASOV_ESTADO", DbType.String, 200, Funciones.CheckStr(str_Estado), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PI_CASOV_USER", DbType.String, 200, Funciones.CheckStr(str_Usuario), ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, 200, strRptaCod, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("PO_MSG_RESLTADO", DbType.String, 200, strRptaMsg, ParameterDirection.Output) _
        }

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSiscajRecdac & ".RECDACSU_MNTCAJXSUBOF"
            objRequest.Parameters.AddRange(arrParam)
            dsUpdate = objRequest.Factory.ExecuteDataset(objRequest)

            strRptaCod = Funciones.CheckStr(CType(objRequest.Parameters(6), IDataParameter).Value)
            strRptaMsg = Funciones.CheckStr(CType(objRequest.Parameters(7), IDataParameter).Value)

        Catch ex As Exception
            strRptaCod = "-99"
            strRptaMsg = "Error: al modificar cajero DAC - " & ex.Message.ToString()
        End Try
    End Function

End Class
