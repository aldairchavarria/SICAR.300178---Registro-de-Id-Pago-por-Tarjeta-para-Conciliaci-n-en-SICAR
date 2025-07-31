Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB

'INI PROY-27440
Public Class clsTransaccionPOS

  Dim objSeg As New Seguridad_NET.clsSeguridad
  Dim strCadenaConexion As String
  Dim strPkgSisCajaPOS As String = "PKG_SISCAJ_POS."
  Dim strPck_SicarOffSap As String = "PCK_SICAR_OFF_SAP."
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

  Public Function Obtener_TipoPOS(ByVal strCodSapTar As String, ByVal strOficina As String, _
  ByRef strCodRpta As String, ByRef strMsgRpta As String) As String

    Dim objDs As DataSet

    strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
    Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
    Dim arrParam() As DAAB.DAABRequest.Parameter = { _
    New DAAB.DAABRequest.Parameter("PI_OFICINA", DbType.String, strOficina, ParameterDirection.Input), _
    New DAAB.DAABRequest.Parameter("PI_CCINS", DbType.String, strCodSapTar, ParameterDirection.Input), _
    New DAAB.DAABRequest.Parameter("PO_CURSOR_DATA", DbType.Object, ParameterDirection.Output), _
    New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
    New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, 200, ParameterDirection.Output)}

    Try
      objRequest.CommandType = CommandType.StoredProcedure
      objRequest.Command = strPkgSisCajaPOS & "SICASS_VIAS_PAGO"
      objRequest.Parameters.AddRange(arrParam)
      objDs = objRequest.Factory.ExecuteDataset(objRequest)

      strCodRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
      strMsgRpta = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)

      If objDs.Tables(0).Rows.Count > 0 Then
        Obtener_TipoPOS = Funciones.CheckStr(objDs.Tables(0).Rows(0)("TIP_TARJETA")) _
        & "#" & Funciones.CheckStr(objDs.Tables(0).Rows(0)("VTEXT"))
      Else
        Obtener_TipoPOS = ""
      End If
    Catch ex As Exception
      strCodRpta = "1"
      strMsgRpta = "Error: al obtener los datos de tipo de pago - " & ex.Message.ToString()
    End Try
  End Function


  Public Function ConsultarDatosPOS(ByVal strIp As String, ByVal strPdv As String, _
                                    ByVal strPosEst As String, ByVal strTipoTarj As String _
  ) As DataSet

    strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
    Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
    Dim arrParam() As DAAB.DAABRequest.Parameter = _
    {New DAAB.DAABRequest.Parameter("p_posn_id", DbType.Int64, Nothing, ParameterDirection.Input), _
     New DAAB.DAABRequest.Parameter("p_posv_ipcaja", DbType.String, strIp, ParameterDirection.Input), _
     New DAAB.DAABRequest.Parameter("p_cod_pdv", DbType.String, strPdv, ParameterDirection.Input), _
     New DAAB.DAABRequest.Parameter("p_flag_buscar", DbType.String, "", ParameterDirection.Input), _
     New DAAB.DAABRequest.Parameter("p_posc_estado", DbType.String, strPosEst, ParameterDirection.Input), _
     New DAAB.DAABRequest.Parameter("p_tipo_tarj", DbType.String, strTipoTarj, ParameterDirection.Input), _
     New DAAB.DAABRequest.Parameter("k_cur_salida", DbType.Object, ParameterDirection.Output)}

    Try
      objRequest.CommandType = CommandType.StoredProcedure
      objRequest.Command = strPkgSisCajaPOS & "siscajs_con_pos_mc"
      objRequest.Parameters.AddRange(arrParam)
      ConsultarDatosPOS = objRequest.Factory.ExecuteDataset(objRequest)
    Catch ex As Exception
      ConsultarDatosPOS = Nothing
    End Try
  End Function




  Public Function ConsultarPedidoPOS(ByVal strNroPedido As String, ByVal strNroRef As String, _
  ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet

    strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
    Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
    Dim arrParam() As DAAB.DAABRequest.Parameter = _
        {New DAAB.DAABRequest.Parameter("k_idpedido", DbType.String, 200, strNroPedido, ParameterDirection.Input), _
     New DAAB.DAABRequest.Parameter("k_docreferencia", DbType.String, strNroRef, ParameterDirection.Input), _
     New DAAB.DAABRequest.Parameter("k_coderror", DbType.String, 200, ParameterDirection.Output), _
     New DAAB.DAABRequest.Parameter("k_msgerror", DbType.String, 200, ParameterDirection.Output), _
     New DAAB.DAABRequest.Parameter("k_cur_salida", DbType.Object, ParameterDirection.Output)}

    Try
      objRequest.CommandType = CommandType.StoredProcedure
      objRequest.Command = strPkgSisCajaPOS & "SICASS_PAGOS_REALIZADOS_PEDIDO"
      objRequest.Parameters.AddRange(arrParam)
      ConsultarPedidoPOS = objRequest.Factory.ExecuteDataset(objRequest)
      strCodRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
      strMsgRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
    Catch ex As Exception
      ConsultarPedidoPOS = Nothing
      strCodRpta = "-99"
      strMsgRpta = "Error: Consultar Datos ConsultarPedidoPOS - " & ex.Message.ToString()
    End Try
  End Function
  Public Function FP_Inserta_Aut_Transac_Pos(ByVal strCanal As String, ByVal strOficina As String, ByVal intApli As Integer, ByVal strCodCajero As String, ByVal strNomCajero As String, _
    ByVal strTipDoc As String, ByVal strNumDoc As String, ByVal strNomCliente As String, ByVal strNumTel As String, ByVal strFactSunat As String, ByVal strDocSap As String, ByVal dblMontoNeto As Double, _
    ByVal intTransac As Integer, ByVal dblSaldoIni As Double, ByVal dblEfectivo As Double, ByVal dblCajaBuzon As Double, ByVal dblRemesa As Double, ByVal dblMontoPen As Double, ByVal dblMontoSob As Double, ByVal strMotivo As String, ByVal intIdTran As Int64, Optional ByVal strAsesor As String = "", Optional ByVal dblMonto As Double = 0) As Integer

    strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
    Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

    Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODCANAL", DbType.String, 5, strCanal, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_CODPDV", DbType.String, 5, strOficina, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_CODAPLIC", DbType.Int16, intApli, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_CODCAJERO", DbType.String, 6, strCodCajero, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_NOMCAJERO", DbType.String, 60, strNomCajero, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_TIP_DOC", DbType.String, 5, strTipDoc, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_NUM_DOC", DbType.String, 20, strNumDoc, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_NOMCLIENTE", DbType.String, 60, strNomCliente, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_NUMTEL", DbType.String, 10, strNumTel, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_FACTSUNAT", DbType.String, 20, strFactSunat, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_DOCSAP", DbType.String, 20, strDocSap, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_MONTONETO", DbType.Double, dblMontoNeto, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_IDCONFTRAN", DbType.Int16, intTransac, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_SALDOINI", DbType.Double, dblSaldoIni, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_INGEFECTIVO", DbType.Double, dblEfectivo, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_CAJABUZON", DbType.Double, dblCajaBuzon, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_REMESA", DbType.Double, dblRemesa, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_MONTO_PEN", DbType.Double, dblMontoPen, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_MONTO_SOB", DbType.Double, dblMontoSob, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_MOTIVO", DbType.String, 18, strMotivo, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_ASESOR", DbType.String, 60, strAsesor, ParameterDirection.Input), _
                  New DAAB.DAABRequest.Parameter("K_MONTODEV", DbType.Double, dblMonto, ParameterDirection.Input), _
New DAAB.DAABRequest.Parameter("K_TRNSN_ID", DbType.Int64, intIdTran, ParameterDirection.Input), _
New DAAB.DAABRequest.Parameter("K_RETORNO", DbType.Int16, ParameterDirection.Output)}

    objRequest.CommandType = CommandType.StoredProcedure
    objRequest.Command = strPkgSisCajaPOS & "SICASS_RPTA_AUTORIZACION"
    objRequest.Parameters.AddRange(arrParam)

    objRequest.Factory.ExecuteNonQuery(objRequest)

    FP_Inserta_Aut_Transac_Pos = CType(objRequest.Parameters(23), IDataParameter).Value

  End Function


    Public Function ObtenerTiposTarjeta(ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet


    strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
    Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("C_TIPOS_TARJETA_POS", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, 200, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, 200, ParameterDirection.Output)}

    Try
      objRequest.CommandType = CommandType.StoredProcedure
      objRequest.Command = strPck_SicarOffSap & "MIG_GetTipoTarjetaPOS"
      objRequest.Parameters.AddRange(arrParam)
      ObtenerTiposTarjeta = objRequest.Factory.ExecuteDataset(objRequest)


            strCodRpta = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)

    Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: Obtener Tipos de Tarjeta  - " & ex.Message.ToString()
      ObtenerTiposTarjeta = Nothing
    End Try

  End Function

    Public Function ObtenerTiposTransaccion(ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet

    strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
    Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("C_TIPOS_TRANSACCION_POS", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, 200, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, 200, ParameterDirection.Output)}

    Try
      objRequest.CommandType = CommandType.StoredProcedure
      objRequest.Command = strPck_SicarOffSap & "MIG_GetTipoTransaccionPOS"
      objRequest.Parameters.AddRange(arrParam)
      ObtenerTiposTransaccion = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)

    Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: Obtener Tipos de Transaccion  - " & ex.Message.ToString()
      ObtenerTiposTransaccion = Nothing
    End Try

  End Function

    Public Function ObtenerTiposOperacion(ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet

    strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
    Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("C_TIPOS_OPERACION_POS", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, 200, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, 200, ParameterDirection.Output)}

    Try
      objRequest.CommandType = CommandType.StoredProcedure
      objRequest.Command = strPck_SicarOffSap & "MIG_GetTipoOperacionPOS"
      objRequest.Parameters.AddRange(arrParam)
      ObtenerTiposOperacion = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)

    Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: Obtener Tipos de Operacion  - " & ex.Message.ToString()
      ObtenerTiposOperacion = Nothing
    End Try

  End Function

    Public Function ObtenerEstadosTransaccion(ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet

    strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
    Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("C_ESTADOS_TRANSACCION_POS", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, 200, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, 200, ParameterDirection.Output)}

    Try
      objRequest.CommandType = CommandType.StoredProcedure
      objRequest.Command = strPck_SicarOffSap & "MIG_GetEstadoTransaccionPOS"
      objRequest.Parameters.AddRange(arrParam)
      ObtenerEstadosTransaccion = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)

    Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: Obtener Tipos de Estado de Transaccion  - " & ex.Message.ToString()
      ObtenerEstadosTransaccion = Nothing
    End Try

  End Function
    Public Function GuardarTransacPOS(ByVal objTransacPos As BeEnvioTransacPOS, ByRef strCodRpta As String, ByRef strMsgRpta As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_TRNSN_ID_CAB", DbType.Int32, IIf(objTransacPos.idCabecera = "", DBNull.Value, Funciones.CheckInt(objTransacPos.idCabecera)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_NUMPEDIDO", DbType.String, objTransacPos.numPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_COD_PDV", DbType.String, objTransacPos.codVenta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_NROTIENDA", DbType.String, objTransacPos.nroTienda, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_NROCAJA", DbType.String, objTransacPos.nroCaja, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_IDESTABLEC", DbType.String, objTransacPos.codEstablecimiento, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_IPCAJA", DbType.String, objTransacPos.ipCaja, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_USUAV_CAJERO", DbType.String, objTransacPos.codCajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_USUAV_ANULADOR", DbType.String, objTransacPos.codAnulador, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_COD_TERMINAL", DbType.String, objTransacPos.numSeriePos, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_COD_EQUIPO", DbType.String, objTransacPos.nombreEquipoPos, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_IP_CLIENTE", DbType.String, objTransacPos.ipCliente, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_IP_SERVIDOR", DbType.String, objTransacPos.ipServidor, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_NOMBRE_CLIENTE", DbType.String, objTransacPos.nombrePcCliente, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_NOMBRE_SERVIDOR", DbType.String, objTransacPos.nombrePcServidor, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_TRANSACCION_POS", DbType.String, objTransacPos.idTransaccionPos, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_NRO_REFERENCIA", DbType.String, objTransacPos.nroReferencia, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_NRO_APROBACION", DbType.String, objTransacPos.nroAprobacion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSC_OPERACION_ID", DbType.String, objTransacPos.codOperacion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_DES_OPERACION", DbType.String, objTransacPos.desOperacion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSC_TIPO_OPERACION", DbType.String, objTransacPos.tipoOperacion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSN_MONTO", DbType.Decimal, IIf(objTransacPos.montoOperacion = "", DBNull.Value, Funciones.CheckDecimal(objTransacPos.montoOperacion)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_ID_CONFTIP_MONEDA", DbType.Int32, IIf(objTransacPos.idCabecera = "", DBNull.Value, Funciones.CheckInt(objTransacPos.monedaOperacion)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSD_FEC_TRANS", DbType.Date, IIf(objTransacPos.fechaTransaccion = "", DBNull.Value, Funciones.CheckDate(objTransacPos.fechaTransaccion)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_NRO_TARJETA", DbType.String, objTransacPos.nroTarjeta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_FEC_EXPIRACION", DbType.String, objTransacPos.fecExpiracion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSC_OBS_ANULACION", DbType.String, objTransacPos.obsAnulacion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_CLIENTE", DbType.String, objTransacPos.nombreCliente, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSN_ID_ANULACION", DbType.Int32, IIf(objTransacPos.idAnulacion = "", DBNull.Value, Funciones.CheckInt(objTransacPos.idAnulacion)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSC_TIPO_TARJETA", DbType.String, objTransacPos.tipoTarjeta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_VOUCHER", DbType.String, objTransacPos.impresionVoucher, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_USUARIO", DbType.String, objTransacPos.usuarioRed, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_TIPOPAGO", DbType.String, objTransacPos.tipoPago, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_ESTADO", DbType.String, objTransacPos.estadoTransaccion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_RESP_TRANS", DbType.String, objTransacPos.codRespTransaccion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_CODAPROB", DbType.String, objTransacPos.codAprobTransaccion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_RESULT_TRANS", DbType.String, objTransacPos.descTransaccion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_ID_REF", DbType.String, objTransacPos.numVoucher, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSN_ID_AUTORIZ", DbType.Int32, IIf(objTransacPos.numTransaccion = "", DBNull.Value, Funciones.CheckInt(objTransacPos.numTransaccion)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_TIPO_TARJETA_POS", DbType.String, objTransacPos.tipoPos, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_TIPO_TRANS", DbType.String, objTransacPos.tipoTransaccion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_FECHA_TRANSACCION_POS", DbType.String, objTransacPos.fechaTransaccionPos, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_HORA_TRANSACCION_POS", DbType.String, objTransacPos.horaTransaccionPos, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSN_ID", DbType.Int32, IIf(objTransacPos.nroRegistro = "", DBNull.Value, Funciones.CheckInt(objTransacPos.nroRegistro)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_TRNSN_ID_CAB", DbType.Int32, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_TRNSN_ID", DbType.Int32, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, ParameterDirection.Output)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SICASI_TRANS_POS_CAB"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteDataset(objRequest)


            Dim strCodCabez As String = Convert.ToString(CType(objRequest.Parameters(44), IDataParameter).Value)
            Dim strTransId As String = Convert.ToString(CType(objRequest.Parameters(45), IDataParameter).Value)
            strCodRpta = Convert.ToString(CType(objRequest.Parameters(46), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(47), IDataParameter).Value) & "|" & strTransId & "|" & strCodCabez

        Catch ex As Exception
            GuardarTransacPOS = strCodRpta & "|" & strMsgRpta
            strCodRpta = "-99"
            strMsgRpta = "Error: GuardarTransacPOS - " & ex.Message.ToString()
        End Try

    End Function


    Public Function UpdateDetTransaccPOS(ByVal objTransacPos As BeEnvioTransacPOS, ByRef strCodRpta As String, ByRef strMsgRpta As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_TRNSN_ID", DbType.Int32, IIf(objTransacPos.TransId = "", DBNull.Value, Funciones.CheckInt(objTransacPos.TransId)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_TRANSACCION_POS", DbType.String, objTransacPos.IdTransPos, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_ID_CONFTIP_MONEDA", DbType.Int32, IIf(objTransacPos.monedaOperacion = "", DBNull.Value, Funciones.CheckInt(objTransacPos.monedaOperacion)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSN_MONTO", DbType.Decimal, IIf(objTransacPos.montoOperacion = "", DBNull.Value, Funciones.CheckDecimal(objTransacPos.montoOperacion)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSN_ID", DbType.Int32, IIf(objTransacPos.nroRegistro = "", DBNull.Value, Funciones.CheckInt(objTransacPos.nroRegistro)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_ID_REF", DbType.String, objTransacPos.numVoucher, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSN_ID_AUTORIZ", DbType.Int32, IIf(objTransacPos.numTransaccion = "", DBNull.Value, Funciones.CheckInt(objTransacPos.numTransaccion)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_RESP_TRANS", DbType.String, objTransacPos.codRespTransaccion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_RESULT_TRANS", DbType.String, objTransacPos.descTransaccion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_CODAPROB", DbType.String, objTransacPos.codAprobTransaccion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_TIPO_TARJETA_POS", DbType.String, objTransacPos.tipoPos, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_NRO_TARJETA", DbType.String, objTransacPos.nroTarjeta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_FECHA_TRANSACCION_POS", DbType.String, objTransacPos.fechaTransaccionPos, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_HORA_TRANSACCION_POS", DbType.String, objTransacPos.horaTransaccionPos, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_FEC_EXPIRACION", DbType.String, objTransacPos.fecExpiracion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_CLIENTE", DbType.String, objTransacPos.nombreCliente, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_VOUCHER", DbType.String, objTransacPos.impresionVoucher, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_USUAV_ANULADOR", DbType.String, objTransacPos.codAnulador, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_COD_TERMINAL", DbType.String, objTransacPos.numSeriePos, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_ID_REF_ANUL", DbType.String, objTransacPos.IdRefAnu, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, ParameterDirection.Output)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SICASU_TRANSPOS"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(20), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(21), IDataParameter).Value)

            If (strCodRpta = "0") Then

                Dim objRequestEst As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
                Dim arrParamEst() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_TRNSN_ID", DbType.Int32, IIf(objTransacPos.TransId = "", DBNull.Value, Funciones.CheckInt(objTransacPos.TransId)), ParameterDirection.Input), _
                                                                New DAAB.DAABRequest.Parameter("PI_TRNSV_ESTADO", DbType.String, objTransacPos.estadoTransaccion, ParameterDirection.Input), _
                                                                New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                                New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, ParameterDirection.Output)}

                objRequestEst.CommandType = CommandType.StoredProcedure
                objRequestEst.Command = strPkgSisCajaPOS & "SICASU_ESTTRANS"
                objRequestEst.Parameters.AddRange(arrParamEst)
                objRequestEst.Factory.ExecuteDataset(objRequestEst)

                strCodRpta = Convert.ToString(CType(objRequestEst.Parameters(2), IDataParameter).Value)
                strMsgRpta = Convert.ToString(CType(objRequestEst.Parameters(3), IDataParameter).Value)

            End If

        Catch ex As Exception

            strCodRpta = "-99"
            strMsgRpta = "Error: Transacciones POS  - " & ex.Message.ToString()
        End Try

    End Function

    Public Function UpdateCabTransaccPOS(ByVal objTransacPos As BeEnvioTransacPOS, ByRef strCodRpta As String, ByRef strMsgRpta As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_TRNSN_ID_CAB", DbType.Int32, IIf(objTransacPos.idCabecera = "", DBNull.Value, Funciones.CheckInt(objTransacPos.idCabecera)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_NUMPEDIDO", DbType.String, objTransacPos.numPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, ParameterDirection.Output)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SICASU_TRANSPOS_CAB"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

         

        Catch ex As Exception

            strCodRpta = "-99"
            strMsgRpta = "Error: Transacciones POS  - " & ex.Message.ToString()
        End Try

    End Function
    Public Function MovCajaPOS(ByVal objTransacPos As BeEnvioTransacPOS, ByRef strCodRpta As String, ByRef strMsgRpta As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_POSN_ID", DbType.Int32, IIf(objTransacPos.nroRegistro = "", DBNull.Value, Funciones.CheckInt(objTransacPos.nroRegistro)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POS_FEC_MOV", DbType.Date, IIf(objTransacPos.fechaTransaccionPos = "", DBNull.Value, Funciones.CheckDate(objTransacPos.fechaTransaccionPos)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POS_TIPO_MOV", DbType.String, objTransacPos.tipoOperacion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POS_EST_MOV", DbType.String, objTransacPos.estadoTransaccion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POS_DET_ERROR", DbType.String, objTransacPos.descTransaccion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POS_USUARIO", DbType.String, objTransacPos.codCajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POS_FEC_REG", DbType.DateTime, IIf(objTransacPos.fechaTransaccion = "", DBNull.Value, Funciones.CheckDate(objTransacPos.fechaTransaccion)), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, ParameterDirection.Output)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SICASI_MOV_CAJA"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(7), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(8), IDataParameter).Value)



        Catch ex As Exception

            strCodRpta = "-99"
            strMsgRpta = "Error: Transacciones POS  - " & ex.Message.ToString()
        End Try

    End Function



    Public Function TransaccionesPOS(ByVal strPdv As String, ByVal strTipoTarjetaPos As String, ByVal strNroCaja As String, _
                                     ByVal strFechaInicial As String, ByVal strFechaFinal As String, ByVal strUsuarioCajero As String, _
                                     ByVal strTipoTransaccion As String, ByVal strTipoOperacionId As String, ByVal strEstado As String, _
                                     ByVal strNroRef As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_COD_PDV", DbType.String, strPdv, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_TIPO_TARJETA_POS", DbType.String, strTipoTarjetaPos, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_NROCAJA", DbType.String, strNroCaja, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_FEC_INI", DbType.String, strFechaInicial, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_FEC_FIN", DbType.String, strFechaFinal, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_USUAV_CAJERO", DbType.String, strUsuarioCajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_TIPO_TRANS", DbType.String, strTipoTransaccion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSC_OPERACION_ID", DbType.String, strTipoOperacionId, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRNSV_ESTADO", DbType.String, strEstado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_ID_REF", DbType.String, strNroRef, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_CURSOR_DATA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, ParameterDirection.Output)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SICASS_REP_PAGO"
            objRequest.Parameters.AddRange(arrParam)
            TransaccionesPOS = objRequest.Factory.ExecuteDataset(objRequest)
            strCodRpta = Convert.ToString(CType(objRequest.Parameters(11), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(12), IDataParameter).Value)

        Catch ex As Exception
            TransaccionesPOS = Nothing
            strCodRpta = "-99"
            strMsgRpta = "Error: Transacciones POS  - " & ex.Message.ToString()
        End Try

    End Function
'PROY-27440 INI
    Public Function ObtenerFormasDePagoRecTrans(ByVal strNroPedido As String, ByVal strMonto As String, _
                                        ByVal strTipoPago As String, ByVal strFormaPago As String, ByVal stCodCaja As String, ByVal strCodPDV As String, ByRef strCodRpta As String, ByRef strMsgRpta As String, Optional ByVal strFechaPago As String = "") As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_IDPEDIDO", DbType.String, strNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_MONTO", DbType.String, strMonto, ParameterDirection.Input), _
                                                         New DAAB.DAABRequest.Parameter("K_TIPO_TARJETA", DbType.String, strFormaPago, ParameterDirection.Input), _
                                                         New DAAB.DAABRequest.Parameter("K_TIPOPAGO", DbType.String, strTipoPago, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_COD_PDV", DbType.String, strCodPDV, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_POSV_NROCAJA", DbType.String, stCodCaja, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_FECHA_PAGO", DbType.String, strFechaPago, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_CODERROR", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_MSGERROR", DbType.String, 200, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Try

            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Anulacion de Recaudacion INICIO - SICASS_DETAPAGO_REC")



            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SICASS_DETAPAGO_REC"
            objRequest.Parameters.AddRange(arrParam)
            ObtenerFormasDePagoRecTrans = objRequest.Factory.ExecuteDataset(objRequest)

 
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Anulacion de Recaudacion FIN - SICASS_DETAPAGO_REC")

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(7), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(8), IDataParameter).Value)

            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Anulacion de Recaudacion INPUT : [strCodRpta - " & strCodRpta & "] - SICASS_DETAPAGO_REC")
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Anulacion de Recaudacion INPUT : [strMsgRpta - " & strMsgRpta & "] - SICASS_DETAPAGO_REC")

        Catch ex As Exception
            ObtenerFormasDePagoRecTrans = Nothing
            strCodRpta = "-99"
            strMsgRpta = "Error: Obtener Formas De Pago Trans - " & ex.Message.ToString()
        End Try

    End Function
    'PROY-27440 FIN
    'PROY-27440 INI - WND
    Public Function ObtenerFormasDePagoTrans(ByVal strNroPedido As String, ByVal strFormaPago As String, ByVal strMonto As String, _
                                           ByVal strTipoPago As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_IDPEDIDO", DbType.String, strNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_MONTO", DbType.String, strMonto, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_TIPOPAGO", DbType.String, strTipoPago, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_TIPO_TARJETA", DbType.String, strFormaPago, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_CODERROR", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_MSGERROR", DbType.String, 200, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SICASS_DETAPAGO"
            objRequest.Parameters.AddRange(arrParam)
            ObtenerFormasDePagoTrans = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)

        Catch ex As Exception
            ObtenerFormasDePagoTrans = Nothing
            strCodRpta = "-99"
            strMsgRpta = "Error: Obtener Formas De Pago Trans - " & ex.Message.ToString()
        End Try

    End Function
    'PROY-27440 FIN - WND

    'CNH INI
    Public Function Obtener_Integracion_Auto(ByVal strPdv As String, ByVal strIp As String, _
    ByVal strTiTarj As String, ByRef strRptFlag As String, _
    ByRef strCodRpta As String, ByRef strMsgRpta As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
        New DAAB.DAABRequest.Parameter("pi_cod_pdv", DbType.String, strPdv, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("pi_posv_ipcaja", DbType.String, strIp, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("pi_flag_tipo_tarj", DbType.String, strTiTarj, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("po_posc_flg_sicar", DbType.String, ParameterDirection.Output), _
        New DAAB.DAABRequest.Parameter("po_cod_resultado", DbType.String, ParameterDirection.Output), _
        New DAAB.DAABRequest.Parameter("po_msj_resultado", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SICASS_FLG_SICAR"
            objRequest.Parameters.AddRange(arrParam)
            arrParam(5).Size = 500
            objRequest.Factory.ExecuteNonQuery(objRequest)

            strRptFlag = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            strCodRpta = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)

        Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: Obtener_Integracion_Auto - " & ex.Message.ToString()
        End Try
    End Function

    Public Function Obtener_Pago_Auto(ByVal strPdv As String, ByVal strIp As String, _
    ByVal strTiTarj As String, ByRef strRptFlag As String, _
    ByRef strCodRpta As String, ByRef strMsgRpta As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
        New DAAB.DAABRequest.Parameter("pi_cod_pdv", DbType.String, strPdv, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("pi_posv_ipcaja", DbType.String, strIp, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("pi_flag_tipo_tarj", DbType.String, strTiTarj, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("po_posc_flg_autom", DbType.String, ParameterDirection.Output), _
        New DAAB.DAABRequest.Parameter("po_cod_resultado", DbType.String, ParameterDirection.Output), _
        New DAAB.DAABRequest.Parameter("po_msj_resultado", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SICASS_FLG_AUTOM"
            objRequest.Parameters.AddRange(arrParam)
            arrParam(5).Size = 500
            objRequest.Factory.ExecuteDataset(objRequest)

            strRptFlag = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            strCodRpta = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)

        Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: Obtener_Integracion_Auto - " & ex.Message.ToString()
        End Try
    End Function
    'PROY-27440 -INI
    Public Function ObtenerOficinasxCajasPOS(ByVal strPOS_ID As String, ByVal strCODPDV As String, _
                                                    ByVal strPOSV_IPCaja As String, ByVal strFLAG_TIPOTARJ As String, _
                                                    ByVal strCOD_PDV_TODOS As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_POSN_ID", DbType.String, strPOS_ID, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_COD_PDV", DbType.String, strCODPDV, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_IPCAJA", DbType.String, strPOSV_IPCaja, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_FLAG_TIPO_TARJ", DbType.String, strFLAG_TIPOTARJ, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_COD_PDV_TODOS", DbType.String, strCOD_PDV_TODOS, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_CURSOR_SALIDA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, 200, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SICASS_CONF_POS"
            objRequest.Parameters.AddRange(arrParam)
            ObtenerOficinasxCajasPOS = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(6), IDataParameter).Value)

        Catch ex As Exception
            ObtenerOficinasxCajasPOS = Nothing
            strCodRpta = "-99"
            strMsgRpta = "Error: ObtenerOficinasxCajasPOS - " & ex.Message.ToString()
        End Try

    End Function

    Public Function RegistrarOficinasxCajasPOS(ByVal strPOSV_IPCaja As String, ByVal strFLAG_TIPOTARJ As String, ByVal strUsuario As String, _
                                                    ByVal strNroTienda As String, ByVal strNroCaja As String, ByVal strIDEstablec As String, _
                                                    ByVal strCodPDV As String, ByVal strIpCajaUpdate As String, ByVal strEstado As String, _
                                                    ByVal strEquipo As String, ByVal strCodTerminal As String, ByVal strFLagSicar As String, _
                                                    ByVal strFlagAutoPago As String, ByVal strFlagTodos As String, ByVal strFlagIntegra As String, _
                                                    ByRef strCodRpta As String, ByRef strMsgRpta As String)


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_POSV_IPCAJA_ORIGEN", DbType.String, strPOSV_IPCaja, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_FLAG_TIPO_TARJ", DbType.String, strFLAG_TIPOTARJ, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_USUARIO", DbType.String, strUsuario, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_NROTIENDA", DbType.String, strNroTienda, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_NROCAJA", DbType.String, strNroCaja, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_IDESTABLEC", DbType.String, strIDEstablec, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_COD_PDV", DbType.String, strCodPDV, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_IPCAJA_UPDATE", DbType.String, strIpCajaUpdate, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSC_ESTADO", DbType.String, strEstado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_EQUIPO", DbType.String, strEquipo, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSV_COD_TERMINAL", DbType.String, strCodTerminal, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSC_FLG_SICAR", DbType.String, strFLagSicar, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_POSC_FLG_AUTOM", DbType.String, strFlagAutoPago, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_FLAG_TODOS", DbType.String, strFlagTodos, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_FLAG_INTEGRACION", DbType.String, strFlagIntegra, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, ParameterDirection.Output)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPkgSisCajaPOS & "SICASU_CONF_POS"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(13), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(14), IDataParameter).Value)



        Catch ex As Exception

            strCodRpta = "-99"
            strMsgRpta = "Error: Transacciones POS  - " & ex.Message.ToString()
        End Try

    End Function
'PROY-27440 - FIN

End Class
'FIN PROY-27440