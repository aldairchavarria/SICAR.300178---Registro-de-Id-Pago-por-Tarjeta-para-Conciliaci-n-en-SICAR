Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB

Public Class clsTarjetasPOS

    Dim objSeg As New Seguridad_NET.clsSeguridad
    Dim strCadenaConexion As String
    Dim strPkgSisCajaPOS As String = "PKG_SISCAJ_POS."
    Dim strPck_SicarOffSap As String = "PCK_SICAR_OFF_SAP."
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

    Public Function CodigosTarjetaPos(ByVal strCodigoTarjeta As String, ByVal strCodigoCCINS As String, ByVal strTarjeta As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataTable

        Dim objDs As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
        New DAAB.DAABRequest.Parameter("P_COTAN_ID", DbType.Int32, IIf(strCodigoTarjeta = "", DBNull.Value, Funciones.CheckInt(strCodigoTarjeta)), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("P_CCINS", DbType.Int32, IIf(strCodigoCCINS = "", DBNull.Value, Funciones.CheckInt(strCodigoCCINS)), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("P_COTAV_ARD_ID", DbType.String, 200, IIf(strTarjeta = "", DBNull.Value, Funciones.CheckInt(strTarjeta)), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_COD_RESULTADO", DbType.String, 200, ParameterDirection.Output), _
        New DAAB.DAABRequest.Parameter("K_RESULTMSG", DbType.String, 200, ParameterDirection.Output), _
        New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SISCAJC_CON_COD_TARJETAS"
            objRequest.Parameters.AddRange(arrParam)
            objDs = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)

            CodigosTarjetaPos = objDs.Tables(0)

        Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: al obtener las codigo de tarjeta del POS - " & ex.Message.ToString()
        End Try
    End Function

    Public Function ConsultarViasPagoPos(ByVal strIdViaPago As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataTable

        Dim objDs As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
        New DAAB.DAABRequest.Parameter("K_ID_T_VIAS_PAGO", DbType.Int32, IIf(strIdViaPago = "", DBNull.Value, Funciones.CheckInt(strIdViaPago)), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_COD_RESULTADO", DbType.String, 200, ParameterDirection.Output), _
        New DAAB.DAABRequest.Parameter("K_RESULTMSG", DbType.String, 200, ParameterDirection.Output), _
        New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SISCAJC_CON_VIAS_PAGO"
            objRequest.Parameters.AddRange(arrParam)
            objDs = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)

            If objDs.Tables(0).Rows.Count > 0 Then
                ConsultarViasPagoPos = objDs.Tables(0)
            Else
                ConsultarViasPagoPos = Nothing
            End If
        Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: al obtener las vias de Pago del POS - " & ex.Message.ToString()
        End Try
    End Function

    Public Function RegistrarViaPagoPos(ByVal strCCINS As String, _
                                         ByVal strCodigoTarjeta As String, _
                                         ByVal strComentario As String, _
                                         ByVal strEstado As String, _
                                         ByVal strUsuario As String, _
                                         ByRef strCodRpta As String, _
                                         ByRef strMsgRpta As String) As DataTable

        Dim objDs As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
        New DAAB.DAABRequest.Parameter("P_CCINS", DbType.Int32, IIf(strCCINS = "", DBNull.Value, Funciones.CheckInt(strCCINS)), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("P_COTAV_ARD_ID", DbType.String, 200, Funciones.CheckStr(strCodigoTarjeta), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("P_COTAV_COMENTARIO", DbType.String, 200, Funciones.CheckStr(strComentario), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("P_COTAN_ESTADO", DbType.Int32, IIf(strEstado = "", DBNull.Value, Funciones.CheckInt(strEstado)), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("P_COTAV_USER_CREA", DbType.String, 200, Funciones.CheckStr(strUsuario), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_COD_RESULTADO", DbType.String, 200, ParameterDirection.Output), _
        New DAAB.DAABRequest.Parameter("K_RESULTMSG", DbType.String, 200, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SISCAJI_INS_COD_TARJETAS"
            objRequest.Parameters.AddRange(arrParam)
            objDs = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Funciones.CheckStr(CType(objRequest.Parameters(5), IDataParameter).Value)
            strMsgRpta = Funciones.CheckStr(CType(objRequest.Parameters(6), IDataParameter).Value)


        Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: al registrar via de Pago del POS - " & ex.Message.ToString()
        End Try
    End Function

    Public Function ActualizarViaPagoPos(ByVal strCOTANID As String, _
                                         ByVal strCCINS As String, _
                                         ByVal strCodigoTarjeta As String, _
                                         ByVal strComentario As String, _
                                         ByVal strEstado As String, _
                                         ByVal strUsuario As String, _
                                         ByRef strCodRpta As String, _
                                         ByRef strMsgRpta As String) As DataTable

        Dim objDs As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
        New DAAB.DAABRequest.Parameter("P_COTAN_ID", DbType.Int32, IIf(strCOTANID = "", DBNull.Value, Funciones.CheckInt(strCOTANID)), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("P_CCINS", DbType.Int32, IIf(strCCINS = "", DBNull.Value, Funciones.CheckInt(strCCINS)), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("P_COTAV_ARD_ID", DbType.String, 200, Funciones.CheckStr(strCodigoTarjeta), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("P_COTAV_COMENTARIO", DbType.String, 200, Funciones.CheckStr(strComentario), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("P_COTAN_ESTADO", DbType.Int32, IIf(strEstado = "", DBNull.Value, Funciones.CheckInt(strEstado)), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("P_COTAV_USER_MOD", DbType.String, 200, Funciones.CheckStr(strUsuario), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_COD_RESULTADO", DbType.String, 200, ParameterDirection.Output), _
        New DAAB.DAABRequest.Parameter("K_RESULTMSG", DbType.String, 200, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SISCAJU_ACT_COD_TARJETAS"
            objRequest.Parameters.AddRange(arrParam)
            objDs = objRequest.Factory.ExecuteDataset(objRequest)
            strCodRpta = Funciones.CheckStr(CType(objRequest.Parameters(6), IDataParameter).Value)
            strMsgRpta = Funciones.CheckStr(CType(objRequest.Parameters(7), IDataParameter).Value)


        Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: al actualizar via de Pago del POS - " & ex.Message.ToString()
        End Try
    End Function

End Class
