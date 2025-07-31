Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB
Imports COM_SIC_Activaciones
 
Public Class clsCajas
    Dim strCadenaConexion As String
    Dim objSeg As New Seguridad_NET.clsSeguridad
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPoolPagos")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public objFileLog As New SICAR_Log
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

    Public Function FP_CalculaEfectivoDia(ByVal OficinaVta As String, ByVal Usuario As String, ByVal Fecha As String) As Double

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 4, OficinaVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("USUARIO", DbType.String, 12, Usuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("FECHA", DbType.String, Fecha, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("MONTOEFEC", DbType.Decimal, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_EFECTIVODIA"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        FP_CalculaEfectivoDia = CType(objRequest.Parameters(3), IDataParameter).Value

    End Function
    Public Function FP_CalculaEfectivo(ByVal OficinaVta As String, ByVal Usuario As String, ByVal Fecha As String) As Double

        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 4, OficinaVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("USUARIO", DbType.String, 12, Usuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("FECHA", DbType.String, Fecha, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("MONTOCAJA", DbType.Decimal, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_EFECTIVO"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        FP_CalculaEfectivo = IIf(IsDBNull(CType(objRequest.Parameters(3), IDataParameter).Value), 0, CType(objRequest.Parameters(3), IDataParameter).Value)

    End Function

    Public Function FP_CalculaCajaBuzon(ByVal OficinaVta As String, ByVal Usuario As String, ByVal Fecha As String) As Double

        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 4, OficinaVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("USUARIO", DbType.String, 12, Usuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("FECHA", DbType.String, Fecha, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("MONTOBUZON", DbType.Decimal, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_MONTOBUZON"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        FP_CalculaCajaBuzon = CType(objRequest.Parameters(3), IDataParameter).Value

    End Function

    Public Function FP_CalculaCajaBuzonCheque(ByVal OficinaVta As String, ByVal Usuario As String, ByVal Fecha As String) As Double

        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 4, OficinaVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("USUARIO", DbType.String, 12, Usuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("FECHA", DbType.String, Fecha, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("MONTOBUZON", DbType.Decimal, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_MONTOBUZON_CHEQUE"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        FP_CalculaCajaBuzonCheque = CType(objRequest.Parameters(3), IDataParameter).Value

    End Function

    Public Function FP_InsertaCajaBuzon(ByVal OficinaVta As String, ByVal Usuario As String, ByVal Monto As Double, ByVal strTipoVia As String, ByVal strBolsa As String, ByVal dblTipCam As Double, ByVal strNomUsuario As String, ByVal fecha As DateTime) As Integer 'INICIATIVA-565
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 4, OficinaVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("USUARIO", DbType.String, 12, Usuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("MONTO", DbType.Decimal, Monto, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("TIPOVIA", DbType.String, 1, strTipoVia, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("BOLSA", DbType.String, 10, strBolsa, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("TIPCAM", DbType.Double, dblTipCam, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("NOMUSUARIO", DbType.String, 80, strNomUsuario, ParameterDirection.Input), _
                         New DAAB.DAABRequest.Parameter("FECHA", DbType.DateTime, fecha, ParameterDirection.Input)} 'INICIATIVA-565
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJI_MONTOBUZON"
        objRequest.Parameters.AddRange(arrParam)

        FP_InsertaCajaBuzon = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_InsertaEfectivo(ByVal OficinaVta As String, ByVal Usuario As String, ByVal Monto As Double, Optional ByVal strFecha As String = "") As Integer

        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 4, OficinaVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("USUARIO", DbType.String, 12, Usuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("MONTOCAJA", DbType.Double, Monto, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("FECHA", DbType.String, 10, strFecha, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJI_EFECTIVO"
        objRequest.Parameters.AddRange(arrParam)

        FP_InsertaEfectivo = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_Get_ListaParamOficina(ByVal CodCanal As String, ByVal CodAplicacion As String, ByVal OficinaVta As String) As DataSet

        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODCANAL", DbType.String, 4, CodCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_APLIC_COD", DbType.String, 12, CodAplicacion, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_PDV", DbType.String, 4, OficinaVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_LISTPARAMOFIC", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_LISTA_PARAM_OFICINA"
        objRequest.Parameters.AddRange(arrParam)

        FP_Get_ListaParamOficina = objRequest.Factory.ExecuteDataset(objRequest)

    End Function


    '/// anulaciones
    Public Function FP_Libera_ClieRec(ByVal p_str_Acuerdo As String, _
                                       ByVal p_str_NumOpeINFO As String, _
                                       ByVal p_str_NumOpeEFT As String, _
                                       ByVal p_str_Reserv1 As String, _
                                       ByVal p_str_Reserv2 As String) As Integer

        ' 

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_INFREC") '"user id=usrinfocorp;data source=timdev;password=usrinfocorp"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("pvAcuerdo", DbType.String, 15, p_str_Acuerdo, ParameterDirection.Input), _
                                New DAABRequest.Parameter("pvNumOpeINFO", DbType.String, 20, p_str_NumOpeINFO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("pvNumOpeEFT", DbType.String, 20, p_str_NumOpeEFT, ParameterDirection.Input), _
                                New DAABRequest.Parameter("pvReserv1", DbType.String, 10, p_str_Reserv1, ParameterDirection.Input), _
                                New DAABRequest.Parameter("adVarChar", DbType.String, 10, p_str_Reserv2, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "pkgSER_BSCS.ADMISS_Free_RegCliente"
        objRequest.Parameters.AddRange(arrParam)

        Return objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FK_LiberaCarpeta(ByVal p_int_SEQCARPETA As Double, ByVal p_str_CANAL As String) As String
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_ECREDITICIA") '"user id=webtim;data source=webtim;password=webtim"
        Dim p_str_ERRFLAG As String

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("K_SEQCARPETA", DbType.Decimal, ParameterDirection.Input), _
                                New DAABRequest.Parameter("K_CANAL", DbType.String, 2, p_str_CANAL, ParameterDirection.Input), _
                                New DAABRequest.Parameter("K_ERRFLAG", DbType.String, 1, p_str_ERRFLAG, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SOLCRED.SOLCRSU_LIBERACARPETA"
        objRequest.Parameters.AddRange(arrParam)

        Return objRequest.Factory.ExecuteNonQuery(objRequest)
    End Function

    Public Function FP_Actualiza_Contrato_Solicitud(ByVal P_SOLIN_CODIGO As Double, _
                                           ByVal P_SOLIV_NUM_CON As String) As String

        ' 

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC") '"user id=usrinfocorp;data source=timdev;password=usrinfocorp"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("p_SOLIN_CODIGO", DbType.Double, P_SOLIN_CODIGO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_SOLIV_NUM_CON", DbType.String, 20, P_SOLIV_NUM_CON, ParameterDirection.Input)}


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "secp_pkg_solicitudes.SECSS_ACT_CONTRATO_SOLICITUD"

        objRequest.Parameters.AddRange(arrParam)
        'Try
        Return objRequest.Factory.ExecuteNonQuery(objRequest)
        'Catch ex As Exception
        '    Return "-1"
        'End Try

    End Function

    Public Function FP_Cab_Oper(ByVal strCodCanal As String, ByVal strOficina As String, ByVal intCodApli As Integer, ByVal strCajero As String, _
        ByVal strTipDocCli As String, ByVal strNumDocCli As String, ByVal strTipDocVta As String, ByVal strDocSAP As String, _
        ByVal strNumRef As String, ByVal dblSubTotal As Double, ByVal dblImpuesto As Double, ByVal dblTotal As Double, ByVal strTipOper As String) As Int32
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("v_cod_canal", DbType.String, 4, strCodCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cod_pdv", DbType.String, 4, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cod_aplic", DbType.Decimal, intCodApli, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("v_cod_cajero", DbType.String, 6, strCajero, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_tip_doccli", DbType.String, 2, strTipDocCli, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("v_num_doc_cli", DbType.String, 15, strNumDocCli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_tip_docvta", DbType.String, strTipDocVta, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("v_num_docsap", DbType.String, 15, strDocSAP, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_num_ref", DbType.String, 20, strNumRef, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("v_sub_tot", DbType.Double, dblSubTotal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_impuesto", DbType.Double, dblImpuesto, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("v_total", DbType.Double, dblTotal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_tipo_oper", DbType.String, 1, strTipOper, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("ULT_CORR", DbType.Int32, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJI_CAB_OPER"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        FP_Cab_Oper = CType(objRequest.Parameters(13), IDataParameter).Value

    End Function


    Public Function FP_Cab_Oper(ByVal strCodCanal As String, ByVal strOficina As String, _
                                ByVal intCodApli As Integer, ByVal strCajero As String, _
                                ByVal strTipDocCli As String, ByVal strNumDocCli As String, _
                                ByVal strTipDocVta As String, ByVal strDocSAP As String, _
                                ByVal strNumRef As String, ByVal dblSubTotal As Double, _
                                ByVal dblImpuesto As Double, ByVal dblTotal As Double, _
                                ByVal strTipOper As String, ByVal decImportePEN As Decimal, _
                                ByVal decImporteUSD As Decimal, ByVal decVuelto As Decimal) As Int32
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("v_cod_canal", DbType.String, 4, strCodCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cod_pdv", DbType.String, 4, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cod_aplic", DbType.Decimal, intCodApli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cod_cajero", DbType.String, 6, strCajero, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_tip_doccli", DbType.String, 2, strTipDocCli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_num_doc_cli", DbType.String, 15, strNumDocCli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_tip_docvta", DbType.String, strTipDocVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_num_docsap", DbType.String, 15, strDocSAP, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_num_ref", DbType.String, 20, strNumRef, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_sub_tot", DbType.Double, dblSubTotal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_impuesto", DbType.Double, dblImpuesto, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_total", DbType.Double, dblTotal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_tipo_oper", DbType.String, 1, strTipOper, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_importe_pen", DbType.Decimal, decImportePEN, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_importe_usd", DbType.Decimal, decImporteUSD, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_vuelto", DbType.Decimal, decVuelto, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("ULT_CORR", DbType.Int32, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJI_CAB_OPER_PAGOS"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        FP_Cab_Oper = CType(objRequest.Parameters(16), IDataParameter).Value

    End Function

    Public Function FP_Det_Oper(ByVal intIdOper As Int32, ByVal intIdDetalle As Integer, ByVal strCodArti As String, _
                                ByVal strNumSerie As String, ByVal strNumTelf As String, ByVal intCandidad As Integer, _
                                ByVal dblSubTotal As Double, ByVal dblImpuesto As Double, ByVal dblTotal As Double) As Integer
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("v_id_confoper", DbType.Int32, intIdOper, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_id_detalle", DbType.Int16, intIdDetalle, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cod_articulo", DbType.String, strCodArti, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("v_num_serie", DbType.String, 18, strNumSerie, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_num_telef", DbType.String, strNumTelf, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("v_cantidad", DbType.Int16, intCandidad, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("v_subtotal", DbType.Double, dblSubTotal, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("v_impuesto", DbType.Double, dblImpuesto, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("v_total", DbType.Double, dblTotal, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJI_DET_OPER"
        objRequest.Parameters.AddRange(arrParam)

        FP_Det_Oper = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_Pag_Oper(ByVal intIdOper As Int32, ByVal intIdDetalle As Integer, ByVal strCodVia As String, _
                                    ByVal strNumTarj As String, ByVal dblMonto As Double) As Integer
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("v_id_confoper", DbType.Int32, intIdOper, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_id_detalle", DbType.Int16, intIdDetalle, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cod_via", DbType.String, 5, strCodVia, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("v_num_tarjeta", DbType.String, 16, strNumTarj, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_monto", DbType.Double, dblMonto, ParameterDirection.Input)}


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJI_PAG_OPER"
        objRequest.Parameters.AddRange(arrParam)

        FP_Pag_Oper = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_InsertaRemesa(ByVal OficinaVta As String, ByVal Usuario As String, ByVal strBolsa As String, ByVal dblTipCam As Double, ByVal datFecha As Date, _
                    ByRef dblMontEfSol As Double, ByRef dblMontChSol As Double, ByRef dblMontEfDol As Double, ByRef dblMontChDol As Double) As Integer

        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 4, OficinaVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("USUARIO", DbType.String, 12, Usuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("BOLSA", DbType.String, 10, strBolsa, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("TIPCAM", DbType.Double, dblTipCam, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("FECHA", DbType.Date, datFecha, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("MONTEFSOL", DbType.Decimal, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("MONTCHSOL", DbType.Decimal, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("MONTEFDOL", DbType.Decimal, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("MONTCHDOL", DbType.Decimal, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJI_REMESA"
        objRequest.Parameters.AddRange(arrParam)

        FP_InsertaRemesa = objRequest.Factory.ExecuteNonQuery(objRequest)

        dblMontEfSol = CType(objRequest.Parameters(5), IDataParameter).Value
        dblMontChSol = CType(objRequest.Parameters(6), IDataParameter).Value
        dblMontEfDol = CType(objRequest.Parameters(7), IDataParameter).Value
        dblMontChDol = CType(objRequest.Parameters(8), IDataParameter).Value

    End Function

    Public Function FP_InsertaRemesa2(ByVal OficinaVta As String, ByVal Usuario As String, ByVal strBolsa As String, ByVal dblTipCam As Double, ByVal datFecha As String, _
                        ByRef dblMontEfSol As Double, ByRef dblMontChSol As Double, ByRef dblMontEfDol As Double, ByRef dblMontChDol As Double, ByVal strNomUsuario As String) As Integer

        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 4, OficinaVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("USUARIO", DbType.String, 12, Usuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("BOLSA", DbType.String, 10, strBolsa, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("TIPCAM", DbType.Double, dblTipCam, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("FECHA", DbType.String, datFecha, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("MONTEFSOL", DbType.Decimal, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("MONTCHSOL", DbType.Decimal, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("MONTEFDOL", DbType.Decimal, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("MONTCHDOL", DbType.Decimal, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("NOMUSUARIO", DbType.String, 80, strNomUsuario, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJI_REMESA2"
        objRequest.Parameters.AddRange(arrParam)

        FP_InsertaRemesa2 = objRequest.Factory.ExecuteNonQuery(objRequest)

        dblMontEfSol = CType(objRequest.Parameters(5), IDataParameter).Value
        dblMontChSol = CType(objRequest.Parameters(6), IDataParameter).Value
        dblMontEfDol = CType(objRequest.Parameters(7), IDataParameter).Value
        dblMontChDol = CType(objRequest.Parameters(8), IDataParameter).Value

    End Function

    Public Function FP_SobreRemesa(ByVal OficinaVta As String, ByVal strBolsa As String, ByVal strBolRem As String) As Integer

        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 4, OficinaVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("BOLSA", DbType.String, 10, strBolsa, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("BOLREM", DbType.String, 20, strBolRem, ParameterDirection.Input)} 'INICIATIVA - 529


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJU_SOBREREMESA"
        objRequest.Parameters.AddRange(arrParam)

        FP_SobreRemesa = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function


    Public Function FP_CalculaRemesa(ByVal OficinaVta As String, ByVal datFecha As String) As Double

        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 4, OficinaVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("FECHA", DbType.String, datFecha, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("MONTOREMESA", DbType.Decimal, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_REMESA"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        FP_CalculaRemesa = CType(objRequest.Parameters(2), IDataParameter).Value


    End Function

    Public Function FP_BolsasLibres(ByVal OficinaVta As String, ByVal strUsuario As String) As DataSet

        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 4, OficinaVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("USUARIO", DbType.String, 5, strUsuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURBUZON", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_BOLSALIBRE"
        objRequest.Parameters.AddRange(arrParam)

        FP_BolsasLibres = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_DetalleRemesa(ByVal strBolsa As String) As DataSet
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("BOLREM", DbType.String, 10, strBolsa, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURBOLSAS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_REMESASOBRES"
        objRequest.Parameters.AddRange(arrParam)

        FP_DetalleRemesa = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function FP_DetalleBuzon(ByVal strOficina As String, ByVal strBolsa As String) As DataSet
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("BOLSA", DbType.String, 10, strBolsa, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURSOBRE", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_DETALLEBUZON"
        objRequest.Parameters.AddRange(arrParam)

        FP_DetalleBuzon = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function FP_DetRemesa2(ByVal strOficina As String, ByVal strBolsa As String) As DataSet
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("BOLSA", DbType.String, 10, strBolsa, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURBOLSA", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_DETALLEREMESA2"
        objRequest.Parameters.AddRange(arrParam)

        FP_DetRemesa2 = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function FP_EliminaBuzon(ByVal strOficina As String, ByVal strBolsa As String, ByVal strUsuario As String, ByVal strNomUsuario As String) As Integer
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("BOLSA", DbType.String, 10, strBolsa, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("USUELM", DbType.String, 12, strUsuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("NOMUSUELM", DbType.String, 80, strNomUsuario, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJD_MONTOBUZON"
        objRequest.Parameters.AddRange(arrParam)

        FP_EliminaBuzon = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_EliminaRemesa(ByVal strOficina As String, ByVal strBolsa As String) As Integer
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("BOLSA", DbType.String, 10, strBolsa, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJD_REMESA"
        objRequest.Parameters.AddRange(arrParam)

        FP_EliminaRemesa = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_ListaRemesa(ByVal strOficina As String, ByVal datFecIni As String, ByVal datFecFin As String) As DataSet
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("FECHAINI", DbType.Date, datFecIni, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("FECHAFIN", DbType.Date, datFecFin, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURREMESA", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_LISTREMESA"
        objRequest.Parameters.AddRange(arrParam)

        FP_ListaRemesa = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_ListaElimBuzon(ByVal strOficina As String, ByVal datFecIni As String, ByVal datFecFin As String) As DataSet
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("FECHAINI", DbType.Date, datFecIni, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("FECHAFIN", DbType.Date, datFecFin, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURBUZON", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_LISTELMBUZON"
        objRequest.Parameters.AddRange(arrParam)

        FP_ListaElimBuzon = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_ListOperaciones(ByVal strCodOficina As String, ByVal fecOperacion As DateTime) As DataSet
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim obCursor As Object
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("v_pdv", DbType.String, 5, CObj(strCodOficina), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_fecha", DbType.DateTime, CObj(fecOperacion), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("cursor_operacion", DbType.Object, obCursor, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_OPERACION"
        objRequest.Parameters.AddRange(arrParam)

        Return objRequest.Factory.ExecuteDataset(objRequest)

    End Function
    Public Function FP_ListDiarioE(ByVal strCodOficina As String, ByVal fecOperacion As DateTime) As DataSet
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim obCursor As Object
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("V_PDV", DbType.String, 5, CObj(strCodOficina), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("V_FECHA", DbType.DateTime, CObj(fecOperacion), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("cursor_diario", DbType.Object, obCursor, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_DIARIO"
        objRequest.Parameters.AddRange(arrParam)

        Return objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_ListOperaciones(ByVal strCodOficina As String, ByVal fecOperacion As DateTime, ByVal strCodCajero As String) As DataSet
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim obCursor As Object
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("v_pdv", DbType.String, 5, CObj(strCodOficina), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_fecha", DbType.DateTime, CObj(fecOperacion), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("v_cajero", DbType.String, 6, CObj(strCodCajero), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("cursor_operacion_ind", DbType.Object, obCursor, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_OPERACION_IND"
        objRequest.Parameters.AddRange(arrParam)

        Return objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_ListDiarioE(ByVal strCodOficina As String, ByVal fecOperacion As DateTime, ByVal strCodCajero As String) As DataSet
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim obCursor As Object
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("V_PDV", DbType.String, 5, CObj(strCodOficina), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("V_FECHA", DbType.DateTime, CObj(fecOperacion), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("V_CAJERO", DbType.String, 6, CObj(strCodCajero), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("cursor_diario_ind", DbType.Object, obCursor, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_DIARIO_IND"
        objRequest.Parameters.AddRange(arrParam)

        Return objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_ListDiarioGEN(ByVal fecOperacionIni As String, ByVal fecOperacionFin As String) As DataSet
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim obCursor As Object
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("V_FECHA_INI", DbType.DateTime, CObj(fecOperacionIni), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("V_FECHA_FIN", DbType.DateTime, CObj(fecOperacionFin), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("cursor_diario", DbType.Object, obCursor, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_DIARIO_GEN"
        objRequest.Parameters.AddRange(arrParam)

        Return objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    '//SISCAJS_VALIDA_BIN

    Public Function FP_ValidaBin(ByVal strCodBin As String) As Boolean
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("V_BIN", DbType.String, 6, CObj(strCodBin), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_FLAG", DbType.String, 1, CObj(""), ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_VALIDA_BIN"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)

        If (CType(objRequest.Parameters(1), IDbDataParameter).Value) = "0" Then
            Return False
        Else
            Return True
        End If

    End Function

    '//SISCAJS_BIN
    Public Function FP_ListaBIN() As DataSet
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim obCursor As Object
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("cursor_bin", DbType.Object, obCursor, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_BIN"
        objRequest.Parameters.AddRange(arrParam)

        Return objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_Busqueda_Solicitud(ByVal dbl_SOLIN_CODIGO As Double, _
                                    ByVal dbl_SEGMN_CODIGO As Double, _
                                    ByVal str_TPROC_CODIGO As String, _
                                    ByVal str_TCLIC_CODIGO As String, _
                                    ByVal str_TACTC_CODIGO As String, _
                                    ByVal str_PACUC_CODIGO As String, _
                                    ByVal str_FPAGC_CODIGO As String, _
                                    ByVal str_TDOCC_CODIGO As String, _
                                    ByVal str_CLIEC_NUM_DOC As String, _
                                    ByVal str_SOLID_FEC_REG_INI As String, _
                                    ByVal str_SOLID_FEC_REG_FIN As String, _
                                    ByVal str_ESTAC_CODIGO As String, _
                                    ByVal str_TEVAC_CODIGO As String, _
                                    ByVal str_RFINC_CODIGO As String, _
                                    ByVal str_OVENC_CODIGO As String, _
                                    ByVal str_SOLIC_FLA_TER As String, _
                                    ByVal dbl_VIGENCIA As Double, _
                                    ByVal str_canac_codigo As String, _
                                    ByVal str_regic_codigo As String, _
                                    ByVal str_SOLIN_USU_SUP As String, _
                                    ByVal str_solin_usu_con As String, _
                                    ByVal p_OPERC_CODIGO As Double, _
                                    ByVal strCodTipoVenta As String) As DataSet
        ' 

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC") '"user id=usrinfocorp;data source=timdev;password=usrinfocorp"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("p_SOLIN_CODIGO", DbType.Double, dbl_SOLIN_CODIGO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_SEGMN_CODIGO", DbType.Double, dbl_SEGMN_CODIGO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_TPROC_CODIGO", DbType.String, 2, str_TPROC_CODIGO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_TCLIC_CODIGO", DbType.String, 2, str_TCLIC_CODIGO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_TACTC_CODIGO", DbType.String, 2, str_TACTC_CODIGO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_PACUC_CODIGO", DbType.String, 2, str_PACUC_CODIGO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_FPAGC_CODIGO", DbType.String, 2, str_FPAGC_CODIGO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_TDOCC_CODIGO", DbType.String, 2, str_TDOCC_CODIGO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_CLIEC_NUM_DOC", DbType.String, 16, str_CLIEC_NUM_DOC, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_FECHA_INI", DbType.String, 10, str_SOLID_FEC_REG_INI, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_FECHA_FIN", DbType.String, 10, str_SOLID_FEC_REG_FIN, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_ESTAC_CODIGO", DbType.String, 2, str_ESTAC_CODIGO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_TEVAC_CODIGO", DbType.String, 2, str_TEVAC_CODIGO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_RFINC_CODIGO", DbType.String, 2, str_RFINC_CODIGO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_OVENC_CODIGO", DbType.String, 4, str_OVENC_CODIGO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_SOLIC_FLA_TER", DbType.String, 1, str_SOLIC_FLA_TER, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_Vigencia", DbType.Double, dbl_VIGENCIA, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_canac_codigo", DbType.String, 2, str_canac_codigo, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_regic_codigo", DbType.String, 2, str_regic_codigo, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_SOLIN_USU_SUP", DbType.String, 10, str_SOLIN_USU_SUP, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_solin_usu_con", DbType.String, 10, str_solin_usu_con, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_topen_codigo", DbType.Double, p_OPERC_CODIGO, ParameterDirection.Input), _
                                New DAABRequest.Parameter("p_TVENC_CODIGO", DbType.String, 2, strCodTipoVenta, ParameterDirection.Input), _
                                New DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "secp_pkg_solicitudes.SECSS_CON_SOLICITUD"

        objRequest.Parameters.AddRange(arrParam)
        'Try
        Return objRequest.Factory.ExecuteDataset(objRequest)
        'Catch ex As Exception
        '    Return "-1"
        'End Try
    End Function

    Function FP_Actualiza_Pago_Solicitud(ByVal P_SOLIN_CODIGO As Int64, _
                                            ByVal P_SOLIV_FLG_PAG_DOC As String, _
                                            ByRef p_msg As String) As Integer

        Dim objDBAccess As Object
        Dim aArrparams As Object
        Dim retorno As Integer = 0

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("p_SOLIN_CODIGO", DbType.Int64, P_SOLIN_CODIGO, ParameterDirection.Input), _
                            New DAABRequest.Parameter("p_SOLIV_FLG_PAG_DOC", DbType.String, 1, P_SOLIV_FLG_PAG_DOC, ParameterDirection.Input)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "secp_pkg_solicitudes.SECSS_ACT_PAGO_SOLICITUD"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
            retorno = 1
            p_msg = "OK"
            Return retorno
        Catch ex As Exception
            retorno = 2
            p_msg = "Mensaje de Error" & ex.Message.ToString
            Return retorno
        End Try
    End Function


    Function FP_RegistroDespachoPDV(ByVal dblCodSEC As Double, ByVal strUsuario As String) As Double

        'Declaracion de Variables

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("K_RESULTADO", DbType.Double, ParameterDirection.Output), _
                                    New DAABRequest.Parameter("P_SOLIN_CODIGO", DbType.Double, 1, dblCodSEC, ParameterDirection.Input), _
                                    New DAABRequest.Parameter("P_USUAC_CODIGO", DbType.String, 1, strUsuario, ParameterDirection.Input)}


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PVU_PKG_BUSINESS.ACUPR_DESPACHO_PDV"

        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        Return CType(objRequest.Parameters(0), IDataParameter).Value

    End Function

    ' agregado por JCR
    Public Function FP_ListTipoDocClte(ByVal strCodTipoDoc As String) As DataSet

        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim obCursor As Object
        Dim arrParam() As DAAB.DAABRequest.Parameter = _
                         {New DAAB.DAABRequest.Parameter("codigo_d", DbType.String, 2, CObj(strCodTipoDoc), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("cursor_operacion", DbType.Object, obCursor, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SICAR.SP_SICAR_TIP_DOC_CLTE"
        objRequest.Parameters.AddRange(arrParam)

        Return objRequest.Factory.ExecuteDataset(objRequest)

    End Function
    Public Function FP_MontosCuadreCaja(ByVal OficinaVta As String, ByVal datFecha As Date, _
                    ByRef dblMontSolHoy As Double, ByRef dblMontSolAyer As Double, ByRef dblMontDolHoy As Double, ByRef dblMontDolAyer As Double, _
                    ByRef dblTipCamHoy As Double, ByRef dblTipCamAyer As Double) As Integer
        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 4, OficinaVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("FECHA", DbType.Date, datFecha, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("MONTEFSOLAYER", DbType.Decimal, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("MONTEFSOLHOY", DbType.Decimal, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("MONTEFDOLAYER", DbType.Decimal, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("MONTEFDOLHOY", DbType.Decimal, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("TIPOCAMBIOHOY", DbType.Decimal, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("TIPOCAMBIOAYER", DbType.Decimal, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJS_CUADRE_CAJA"
        objRequest.Parameters.AddRange(arrParam)
        FP_MontosCuadreCaja = objRequest.Factory.ExecuteNonQuery(objRequest)
        dblMontSolHoy = CType(objRequest.Parameters(2), IDataParameter).Value
        dblMontSolAyer = CType(objRequest.Parameters(3), IDataParameter).Value
        dblMontDolHoy = CType(objRequest.Parameters(4), IDataParameter).Value
        dblMontDolAyer = CType(objRequest.Parameters(5), IDataParameter).Value
        dblTipCamHoy = CType(objRequest.Parameters(6), IDataParameter).Value
        dblTipCamAyer = CType(objRequest.Parameters(7), IDataParameter).Value
    End Function

    Public Function FP_Get_TelefonosPorta(ByVal strNroPedido As String, ByVal strTipoVenta As Integer, ByRef intRetorno As Integer) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
           New DAAB.DAABRequest.Parameter("P_NUM_PEDIDO", DbType.String, strNroPedido, ParameterDirection.Input), _
           New DAAB.DAABRequest.Parameter("P_TIPOVENTA", DbType.Int64, strTipoVenta, ParameterDirection.Input), _
           New DAAB.DAABRequest.Parameter("K_RETORNO", DbType.Int64, ParameterDirection.Output), _
           New DAAB.DAABRequest.Parameter("CUR_TELEFONO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_PROVISION_ACT_DESAC.SP_GET_TELEFONOS_PORT"
        objRequest.Parameters.AddRange(arrParam)

        Try
            Return objRequest.Factory.ExecuteDataset(objRequest)
            intRetorno = CType(objRequest.Parameters(2), IDataParameter).Value
        Catch ex As Exception
            intRetorno = 1
            Return New DataSet
        End Try

    End Function

    Public Function RollbackDetalleVenta(ByVal msisdn As String, ByVal NroSEC As Int64) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
           New DAAB.DAABRequest.Parameter("P_MSISDN", DbType.String, msisdn, ParameterDirection.Input), _
           New DAAB.DAABRequest.Parameter("P_SOLIN_CODIGO", DbType.Int64, NroSEC, ParameterDirection.Input), _
           New DAAB.DAABRequest.Parameter("K_RESULTADO", DbType.Int64, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_PROVISION_ACT_DESAC.SP_ROLLBACK_VENTA"
            objRequest.Parameters.AddRange(arrParam)

            RollbackDetalleVenta = objRequest.Factory.ExecuteNonQuery(objRequest)
        Catch ex As Exception
            RollbackDetalleVenta = 1
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

    End Function

    Public Function ActualizaPago(ByVal strNroPedido As String, ByVal strTipo As Integer, ByVal intRetorno As Int64) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
           New DAAB.DAABRequest.Parameter("P_NRO_PEDIDO", DbType.String, strNroPedido, ParameterDirection.Input), _
           New DAAB.DAABRequest.Parameter("P_TIPO", DbType.Int32, strTipo, ParameterDirection.Input), _
           New DAAB.DAABRequest.Parameter("K_RESULTADO", DbType.Int64, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_PROVISION_ACT_DESAC.SP_ACTUALIZA_PAGO"
            objRequest.Parameters.AddRange(arrParam)

            ActualizaPago = objRequest.Factory.ExecuteNonQuery(objRequest)
        Catch ex As Exception
            ActualizaPago = 1
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

    End Function
    Public Function FP_Actualiza_Bolsa_Fecha(ByVal Oficina As String, ByVal Bolsa As String, ByVal FechaBolsa As Date) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, Oficina, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_BOLSA", DbType.String, 10, Bolsa, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.DateTime, FechaBolsa, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("OUT_RETORNO", DbType.Int32, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SISCAJU_BUZON_FECHA"
        objRequest.Parameters.AddRange(arrParam)

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            FP_Actualiza_Bolsa_Fecha = CType(objRequest.Parameters(3), IDataParameter).Value
        Catch ex As Exception
            FP_Actualiza_Bolsa_Fecha = 1
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

    End Function
    Public Function FP_Anula_RenovacionPrepago(ByVal strNroPedido As String) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_REPREV_NRO_RENOVADO", DbType.String, strNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_RETORNO", DbType.Int64, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_EXPRESS_6.SP_ANULA_RENOVACION_PRE"
            objRequest.Parameters.AddRange(arrParam)

            FP_Anula_RenovacionPrepago = objRequest.Factory.ExecuteNonQuery(objRequest)

        Catch ex As Exception
            FP_Anula_RenovacionPrepago = 0
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function FP_ConsultaFCaducidaRPM6(ByVal strNroTelefonico As String, ByVal strCodRenovacion As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SICARBSCS")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_msisdn", DbType.String, 255, strNroTelefonico, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_cod_rnv", DbType.String, 255, strCodRenovacion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_fecha_venc", DbType.Date, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("p_result", DbType.Int32, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "TIM.TIM084_SP_FECHA_VIGENC"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Dim FechaRetorno = CType(objRequest.Parameters(2), IDataParameter).Value
        If (FechaRetorno.GetType().ToString() = "System.DBNull") Then
            FechaRetorno = String.Empty
        Else
            FechaRetorno = CDate(FechaRetorno).ToShortDateString()
        End If

        FP_ConsultaFCaducidaRPM6 = CType(objRequest.Parameters(3), IDataParameter).Value & ";" & FechaRetorno
    End Function

    '<33062>
    Public Function FP_InsertaMail(ByVal CustomerId As String, _
                                                ByVal Cuenta As String, _
                                                ByVal Email As String, _
                                                ByVal FlagAct As String) As Boolean

        FP_InsertaMail = True
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SICARBSCS")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_customer_id", DbType.Int64, CustomerId, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_cuenta", DbType.String, Cuenta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_email", DbType.String, Email, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_flag_act", DbType.String, FlagAct, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_result", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("p_error", DbType.String, ParameterDirection.Output) _
                                                        }
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "TIM.TIM131_PKG_FACT_ELECT.SP_INSERTA_MAIL"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
            Dim result As String = CType(objRequest.Parameters(4), IDataParameter).Value

            If result = "OK" Then
                FP_InsertaMail = True
            Else
                FP_InsertaMail = False
            End If
        Catch ex As Exception
            FP_InsertaMail = False
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

    End Function
    '</33062>

    Public Function FP_RegistrarRenovacionRPM6(ByVal NroTelefonico As String, _
                                                ByVal CodRenov As String, _
                                                ByVal Accion As Integer, _
                                                ByVal DescTickler As String, _
                                                ByVal PuntoVenta As String, _
                                                ByVal strUsuario As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SICARBSCS")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_msisdn", DbType.String, 255, NroTelefonico, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_cod_rnv", DbType.String, 255, CodRenov, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_accion", DbType.Int32, Accion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_des_tickler", DbType.String, 255, DescTickler, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_punto_venta", DbType.String, 255, PuntoVenta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_usuario", DbType.String, 255, strUsuario, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_fecha_vig", DbType.DateTime, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("p_result", DbType.Int32, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("p_msgerr", DbType.String, 150, ParameterDirection.Output) _
                                                        }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "TIM.TIM083_SP_RENOVACION"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)

        Dim FechaRetorno = CType(objRequest.Parameters(6), IDataParameter).Value
        If (FechaRetorno.GetType().ToString() = "System.DBNull") Then
            FechaRetorno = String.Empty
        Else
            FechaRetorno = CDate(FechaRetorno).ToShortDateString()
        End If
        FP_RegistrarRenovacionRPM6 = CType(objRequest.Parameters(7), IDataParameter).Value & ";" & CType(objRequest.Parameters(8), IDataParameter).Value & ";" & FechaRetorno

    End Function

    Public Function FP_RegistrarLogRenovacionRPM6(ByVal NroTelefonico As String, _
                                                ByVal CodRenov As String, _
                                                ByVal Accion As Integer, _
                                                ByVal CodOcc As Integer, _
                                                ByVal NroCuotas As Integer, _
                                                ByVal Monto As Double, _
                                                ByVal Remark As String, _
                                                ByVal TipoTickler As String, _
                                                ByVal DescTickler As String, _
                                                ByVal strUsuario As String, _
                                                ByVal strUsuarioTransaccion As String, _
                                                ByVal strResultado As String, _
                                                ByVal FechaCaduca As String, _
                                                ByVal PuntoVenta As String) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim Fecha As Date
        If FechaCaduca Is Nothing Or FechaCaduca.Length = 0 Then
            Fecha = CDate("01/01/1901")
        Else
            Fecha = CDate(FechaCaduca)
        End If
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_msisdn", DbType.String, 255, NroTelefonico, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_cod_rnv", DbType.String, 255, CodRenov, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_accion", DbType.Int32, Accion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_cod_occ", DbType.Int32, CodOcc, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_nro_coutas", DbType.Int32, NroCuotas, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_monto_occ", DbType.Double, Monto, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_remark", DbType.String, 255, Remark, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_tipo_tickler", DbType.String, 255, TipoTickler, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_des_tickler", DbType.String, 255, DescTickler, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_usuario", DbType.String, 255, strUsuario, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_usu_transac", DbType.String, 255, strUsuarioTransaccion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_result", DbType.String, 255, strResultado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_fecha_vig", DbType.DateTime, Fecha, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_punto_venta", DbType.String, 255, PuntoVenta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_res_salida", DbType.Int32, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "USRPVU.SP_LOG_RENOVACION"
            objRequest.Parameters.AddRange(arrParam)
            FP_RegistrarLogRenovacionRPM6 = objRequest.Factory.ExecuteNonQuery(objRequest)
        Catch ex As Exception
            FP_RegistrarLogRenovacionRPM6 = 0
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
    End Function
    Public Function FP_ConsultaParametros(ByVal strGrupo As String) As DataSet

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("SPARV_GRUPO", DbType.String, 10, strGrupo, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISCAJ.SICAR_CONSULTA_PARAM"
            objRequest.Parameters.AddRange(arrParam)

            FP_ConsultaParametros = objRequest.Factory.ExecuteDataset(objRequest)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function FP_Get_Cuotas(ByVal strNroPedido As String) As DataSet
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                New DAAB.DAABRequest.Parameter("P_NUM_PEDIDO", DbType.String, strNroPedido, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("K_CONSULTA", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_PORTABILIDAD_6.SP_LISTAR_CUOTAS_TICKET"
            objRequest.Parameters.AddRange(arrParam)

            FP_Get_Cuotas = objRequest.Factory.ExecuteDataset(objRequest)
        Catch ex As Exception
            FP_Get_Cuotas = Nothing
        End Try

    End Function

    Public Function FP_VENTAS_REGISTRADAS(ByVal strNroTELEFONO As String, ByVal strfecpago As Date, ByVal strmonto As Double, ByVal strCodAsesor As String, ByVal strCacAsesor As String, ByRef descerror As String) As Integer
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "ESBD")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        Catch ex As Exception
            descerror = " Error de cadena conexion: " & ex.Message

        End Try

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Try
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
               New DAAB.DAABRequest.Parameter("P_NUM_TELEF", DbType.String, strNroTELEFONO, ParameterDirection.Input), _
               New DAAB.DAABRequest.Parameter("P_FEC_PAGO", DbType.Date, strfecpago, ParameterDirection.Input), _
               New DAAB.DAABRequest.Parameter("P_MONTO", DbType.Double, strmonto, ParameterDirection.Input), _
               New DAAB.DAABRequest.Parameter("P_COD_ASESOR", DbType.String, strCodAsesor, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_CAC_ASESOR", DbType.String, strCacAsesor, ParameterDirection.Input), _
               New DAAB.DAABRequest.Parameter("P_CODERROR", DbType.Int32, ParameterDirection.Output), _
               New DAAB.DAABRequest.Parameter("P_MSJERROR", DbType.String, ParameterDirection.Output)}

            Dim ownerESBD As String
            ownerESBD = String.Empty

            ownerESBD = ConfigurationSettings.AppSettings("ownerESBD")

            objRequest.CommandType = CommandType.StoredProcedure
            If ownerESBD = "" Then
                objRequest.Command = "PKG_ENCUESTA_VIA_SMS.SP_GUARDAR_VENTAS_REGISTRADAS"
            Else
                objRequest.Command = ownerESBD & ".PKG_ENCUESTA_VIA_SMS.SP_GUARDAR_VENTAS_REGISTRADAS"
            End If
            ' objRequest.Command = "SES.PKG_ENCUESTA_VIA_SMS.SP_GUARDAR_VENTAS_REGISTRADAS"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)

            Dim p_coderror = CType(objRequest.Parameters(5), IDataParameter).Value
            FP_VENTAS_REGISTRADAS = p_coderror

        Catch ex As Exception
            FP_VENTAS_REGISTRADAS = -1
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Cajas\clsCajas.vb; Function: FP_VENTAS_REGISTRADAS)"
            descerror = "Error FP_VENTAS_REGISTRADAS: " & ex.Message & MaptPath
            'FIN PROY-140126
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try


    End Function

    'Renovacion Corporativa
    Public Function FP_GrabarPagoRenovB2E(ByVal p_nro_pedido As String, ByVal p_usuario_pago As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("v_vrenv_nro_pedido", DbType.String, 15, p_nro_pedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("v_vrenv_usuario_pago", DbType.String, 10, p_usuario_pago, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("v_flag_exito", DbType.String, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "pkg_renovacion_linea.sp_update_venta_renov_b2e"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            If CType(CType(objRequest.Parameters(2), IDataParameter).Value, String).Equals("1") Then
                FP_GrabarPagoRenovB2E = True
            Else
                FP_GrabarPagoRenovB2E = False
            End If
        Catch
            FP_GrabarPagoRenovB2E = False
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function FP_GrabarPagoRenovBusiness(ByVal p_nro_pedido As String, ByVal p_usuario_pago As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("v_vrenv_nro_pedido", DbType.String, 255, p_nro_pedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("v_vrenv_usuario_pago", DbType.String, 255, p_usuario_pago, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("v_flag_exito", DbType.String, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "pkg_renovacion_linea.sp_update_venta_renov_bus"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            If CType(CType(objRequest.Parameters(2), IDataParameter).Value, String).Equals("1") Then
                FP_GrabarPagoRenovBusiness = True
            Else
                FP_GrabarPagoRenovBusiness = False
            End If
        Catch
            FP_GrabarPagoRenovBusiness = False
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function FP_ConsultarDatosRenov(ByVal p_nro_pedido As String, ByVal p_tipo_renov As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_nro_pedido", DbType.String, 255, p_nro_pedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_tipo_renov", DbType.String, 255, p_tipo_renov, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_cur_salida", DbType.Object, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "pkg_renovacion_linea.sp_lista_datos_bscs"
            objRequest.Parameters.AddRange(arrParam)

            FP_ConsultarDatosRenov = objRequest.Factory.ExecuteDataset(objRequest)
        Catch
            FP_ConsultarDatosRenov = Nothing
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function FP_GrabarRenovCorpBSCS(ByVal p_nro_telefono As String, _
                                                ByVal p_nro_sec As String, _
                                                ByVal p_fec_entrega As Date, _
                                                ByVal p_fec_renov As Date, _
                                                ByVal p_oficina_venta As String, _
                                                ByVal p_con_sin_equipo As String, _
                                                ByVal p_plazo_linea As String, _
                                                ByRef msgError As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SICARBSCS")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_DN_NUM", DbType.String, 255, p_nro_telefono, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NUMERO_SEC", DbType.String, 255, p_nro_sec, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_ENTREGA", DbType.Date, p_fec_entrega, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_ACTIVACION", DbType.Date, p_fec_renov, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CONSULTOR", DbType.String, 255, p_oficina_venta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_C_S_EQUIPO", DbType.String, 255, p_con_sin_equipo, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_PLAZA_ACUERDO", DbType.String, 255, p_plazo_linea, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CO_ERROR", DbType.Int32, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_DES_ERROR", DbType.String, 255, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "TIM.TIM089_UPT_VENTA"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            msgError = IIf(IsDBNull(CType(objRequest.Parameters(8), IDataParameter).Value), "", CType(objRequest.Parameters(8), IDataParameter).Value)
            FP_GrabarRenovCorpBSCS = CType(objRequest.Parameters(7), IDataParameter).Value

        Catch ex As Exception
            msgError = ex.Message
            FP_GrabarRenovCorpBSCS = "999"
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function FP_GrabarRenovCorpBSCSNuevo(ByVal p_nro_telefono As String, _
                                                ByVal p_nro_sec As String, _
                                                ByVal p_fec_entrega As String, _
                                                ByVal p_fec_renov As String, _
                                                ByVal p_oficina_venta As String, _
                                                ByVal p_con_sin_equipo As String, _
                                                ByVal p_plazo_linea As String, _
                                                ByRef msgError As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SICARBSCS")
        Dim result As Integer = 0
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_DN_NUM", DbType.String, 255, p_nro_telefono, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_ENTREGA", DbType.String, 10, p_fec_entrega, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_ACTIVACION", DbType.String, 10, p_fec_renov, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CONSULTOR", DbType.String, 255, p_oficina_venta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_C_S_EQUIPO", DbType.String, 255, p_con_sin_equipo, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CO_ERROR", DbType.Int32, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_DES_ERROR", DbType.String, 255, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "TIM.TIM150_RENOV_BUSINESS.RENOVAR"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
            'codError = IIf(IsDBNull(CType(objRequest.Parameters(5), IDataParameter).Value), -1, CType(objRequest.Parameters(5), IDataParameter).Value)
            msgError = IIf(IsDBNull(CType(objRequest.Parameters(6), IDataParameter).Value), "", CType(objRequest.Parameters(6), IDataParameter).Value)
            result = CType(objRequest.Parameters(5), IDataParameter).Value

            FP_GrabarRenovCorpBSCSNuevo = result.ToString
        Catch ex As Exception
            msgError = ex.Message
            FP_GrabarRenovCorpBSCSNuevo = "999"
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function FP_AnularRenovCorpBSCS(ByVal p_nro_telefono As String, ByRef msgError As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SICARBSCS")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_DN_NUM", DbType.String, 255, p_nro_telefono, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CO_ERROR", DbType.Int32, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_DES_ERROR", DbType.String, 255, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "TIM.TIM090_UPT_RENOV"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            msgError = IIf(IsDBNull(CType(objRequest.Parameters(2), IDataParameter).Value), "", CType(objRequest.Parameters(2), IDataParameter).Value)
            FP_AnularRenovCorpBSCS = CType(objRequest.Parameters(1), IDataParameter).Value

        Catch ex As Exception
            msgError = ex.Message
            FP_AnularRenovCorpBSCS = "999"
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function FP_AnularRenovCorpBSCSNuevo(ByVal p_nro_telefono As String, ByRef msgError As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SICARBSCS")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_DN_NUM", DbType.String, 255, p_nro_telefono, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CO_ERROR", DbType.Int32, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_DES_ERROR", DbType.String, 255, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "TIM150_RENOV_BUSINESS.DESHACER_RENOV"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            msgError = IIf(IsDBNull(CType(objRequest.Parameters(2), IDataParameter).Value), "", CType(objRequest.Parameters(2), IDataParameter).Value)
            FP_AnularRenovCorpBSCSNuevo = CType(objRequest.Parameters(1), IDataParameter).Value

        Catch ex As Exception
            msgError = ex.Message
            FP_AnularRenovCorpBSCSNuevo = "999"
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function FP_AnularPagoRenovCorp(ByVal p_nro_pedido As String, ByVal p_tipo_renov As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_nro_pedido", DbType.String, 255, p_nro_pedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_tipo_renov", DbType.String, 255, p_tipo_renov, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_cod_resultado", DbType.String, 255, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "pkg_renovacion_linea.sp_anula_pago_renov"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            If CType(CType(objRequest.Parameters(2), IDataParameter).Value, String).Equals("1") Then
                FP_AnularPagoRenovCorp = True
            Else
                FP_AnularPagoRenovCorp = False
            End If
        Catch
            FP_AnularPagoRenovCorp = False
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

    End Function

    Public Function FP_AnularVentaRenovCorp(ByVal p_nro_pedido As String, ByVal p_tipo_renov As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_nro_pedido", DbType.String, 255, p_nro_pedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_tipo_renov", DbType.String, 255, p_tipo_renov, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_cod_resultado", DbType.String, 255, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "pkg_renovacion_linea.sp_anula_venta_renov"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            If CType(CType(objRequest.Parameters(2), IDataParameter).Value, String).Equals("1") Then
                FP_AnularVentaRenovCorp = True
            Else
                FP_AnularVentaRenovCorp = False
            End If
        Catch
            FP_AnularVentaRenovCorp = False
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
    End Function
    'Renovacion Corporativa


    Public Function ValidaPromoModem(ByVal vCampana As String, ByVal vListaPrecio As String, ByVal vPlanTarifa As String, ByVal vNroSolicitud As String) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("P_CAMPANA", DbType.String, 5, vCampana, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_LISTAPRECIO", DbType.String, 5, vListaPrecio, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_PLANTARIFA", DbType.String, 5, vPlanTarifa, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_ACUERDOPLAZO", DbType.String, 5, vNroSolicitud, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_RESPUESTA", DbType.Int32, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTAS.SP_VALIDA_PROM_MODEM"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        ValidaPromoModem = CType(objRequest.Parameters(4), IDataParameter).Value
    End Function

    Public Function InsVtaPromoModemCab(ByVal vTipoDoc As String, ByVal vNumDoc As String, ByVal vCodPDV As String, _
                                        ByVal vNroDocSap As String, ByVal vUsuario As String, ByVal vTipoVenta As String, _
                                        ByVal vTipoOp As String, ByVal vPlazoAcuerdo As String, ByVal vFechaDocSap As String) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("P_TIPODOC", DbType.String, 2, vTipoDoc, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NUMDOC", DbType.String, 20, vNumDoc, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CODPDV", DbType.String, 5, vCodPDV, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NRODOCSAP", DbType.String, 20, vNroDocSap, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, vUsuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_TIPOVENTA", DbType.String, 2, vTipoVenta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_TIPOOP", DbType.String, 2, vTipoOp, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_PLAZOACUERDO", DbType.String, 5, vPlazoAcuerdo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_FECHADOCSAP", DbType.String, 10, vFechaDocSap, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_IDVENTA", DbType.Int32, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTAS.SP_INS_VENTA_SAP_CAB"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        InsVtaPromoModemCab = CType(objRequest.Parameters(9), IDataParameter).Value
    End Function


    Public Function InsVtaPromoModemDet(ByVal vIdVenta As String, ByVal vCodMaterial As String, ByVal vDesMaterial As String, _
                                    ByVal vSerie As String, ByVal vTelefono As String, ByVal vCodCampana As String, _
                                    ByVal vCodListPrecio As String, ByVal vCodPlan As String) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("P_IDVENTA", DbType.Int32, vIdVenta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CODMATERIAL", DbType.String, 20, vCodMaterial, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_DESCMATERIAL", DbType.String, 100, vDesMaterial, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_SERIE", DbType.String, 20, vSerie, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_TELEFONO", DbType.String, 15, vTelefono, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CODCAMPANA", DbType.String, 5, vCodCampana, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CODLISTPRECIO", DbType.String, 5, vCodListPrecio, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CODPLAN", DbType.String, 5, vCodPlan, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_RETONO", DbType.Int32, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTAS.SP_INS_VENTA_SAP_DET"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        InsVtaPromoModemDet = CType(objRequest.Parameters(8), IDataParameter).Value
    End Function

    Public Function LisPlazoAcuerdo(ByVal vNroSolicitud As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("P_NROSOLICITUD", DbType.Int32, vNroSolicitud, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_PLAZOACUERDO", DbType.String, 5, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTAS.SP_LIS_PLAZO_ACUERDO"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        LisPlazoAcuerdo = CType(objRequest.Parameters(1), IDataParameter).Value
    End Function

    Public Function validaAnulaVtaProm(ByVal vNroDocSap As String) As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("P_NRODOCSAP", DbType.String, 20, vNroDocSap, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_RESPUESTA", DbType.Int32, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTAS.SP_VALIDA_ANULA_DOC"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        validaAnulaVtaProm = CType(objRequest.Parameters(1), IDataParameter).Value
    End Function

    Public Function AnulaVtaProm(ByVal vNroDocSap As String, ByVal vEstado As String) As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("P_NRODOCSAP", DbType.String, 20, vNroDocSap, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_VENC_ESTADO", DbType.String, 1, vEstado, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_RESPUESTA", DbType.Int32, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTAS.SP_ANULA_VTA_PROM"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        AnulaVtaProm = CType(objRequest.Parameters(2), IDataParameter).Value
    End Function

    Public Function ObtenerSecByPedido(ByVal nroPedido As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim dr As New DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                    New DAABRequest.Parameter("P_SOPOC_NROPEDIDO", DbType.String, 20, nroPedido, ParameterDirection.Input), _
                    New DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_EXPRESS_PORTA.SP_OBTENER_SEC_BY_PEDIDO"
        objRequest.Parameters.AddRange(arrParam)

        dr = objRequest.Factory.ExecuteDataset(objRequest)

        If dr.Tables(0).Rows.Count = 0 Then
            ObtenerSecByPedido = ""
        Else
            ObtenerSecByPedido = CType(dr.Tables(0).Rows(0).Item("SOLIN_CODIGO"), String)
        End If
    End Function

    Public Function ObtenerSecByContrato(ByVal nroContrato As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim dr As New DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                   New DAABRequest.Parameter("P_SOLIV_NUM_CON", DbType.String, 20, nroContrato, ParameterDirection.Input), _
                   New DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_EXPRESS_PORTA.SP_OBTENER_SEC_BY_CONTRATO"
        objRequest.Parameters.AddRange(arrParam)

        dr = objRequest.Factory.ExecuteDataset(objRequest)

        If dr.Tables(0).Rows.Count = 0 Then
            ObtenerSecByContrato = ""
        Else
            ObtenerSecByContrato = CType(dr.Tables(0).Rows(0).Item("SOLIN_CODIGO"), String)
        End If
    End Function

    Public Function ObtenerSec(ByVal nroContrato As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim dr As New DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                    New DAABRequest.Parameter("P_SOLIV_NUM_CON", DbType.String, 20, nroContrato, ParameterDirection.Input), _
                    New DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTA_EXPRESS_6.SP_OBTENER_SEC"
        objRequest.Parameters.AddRange(arrParam)

        'objRequest.Factory.ExecuteNonQuery(objRequest)
        dr = objRequest.Factory.ExecuteDataset(objRequest)
        If dr.Tables(0).Rows.Count = 0 Then
            ObtenerSec = ""
        Else
            ObtenerSec = CType(dr.Tables(0).Rows(0).Item("SOLIN_CODIGO"), String)
        End If

    End Function
    'fin agregado Ugo Blanco

    Public Function ObtenerParamByGrupo(ByVal grupo As Int64) As DataSet 'PROY-24724-IDEA-28174 -PARAMETROS
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_PARAN_GRUPO", DbType.Decimal, grupo, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONSULTA_GENERAL.SISACT_CON_PARAM_BY_GRUPO"
            objRequest.Parameters.AddRange(arrParam)

            ObtenerParamByGrupo = objRequest.Factory.ExecuteDataset(objRequest)
        Catch ex As Exception
            ObtenerParamByGrupo = Nothing
        End Try
    End Function

    'DTH UB
    Public Function ObtenerTramaSGA(ByVal nroContrato As String, ByRef cadena1 As String, ByRef cadena6 As String, _
                                    ByRef codError As String, ByRef descError As String) As DataSet
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_NRO_CONTRATO", DbType.String, 20, nroContrato, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_CURCADENA05", DbType.Object, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_CADENA01", DbType.String, 4000, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_CADENA02", DbType.Object, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_CADENA03", DbType.Object, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_CADENA06", DbType.String, 4000, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_COD_RETORNO", DbType.String, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_DESC_RETORNO", DbType.String, 4000, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_DTH.SP_GENERA_TRAMA_VENTA_SGA"
            objRequest.Parameters.AddRange(arrParam)

            ObtenerTramaSGA = objRequest.Factory.ExecuteDataset(objRequest)
            cadena1 = CType(objRequest.Parameters(2), IDataParameter).Value
            cadena6 = CType(objRequest.Parameters(5), IDataParameter).Value
            codError = CType(objRequest.Parameters(6), IDataParameter).Value
            descError = CType(objRequest.Parameters(7), IDataParameter).Value
        Catch ex As Exception
            ObtenerTramaSGA = Nothing
        End Try
    End Function


    Public Function validaDocSAPxDTH(ByVal nroSap As String, ByRef nroContrato As Integer, ByRef nroSec As String, ByRef nroDocSAP As String, _
                                     ByRef nroDepSap As String, ByRef codRespuesta As Integer, ByRef mensaje As String) As Boolean
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, 20, nroSap, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_NRO_CONTRATO", DbType.Int64, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_NRO_SEC", DbType.Int64, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_NRO_DOCUMENTO_SAP", DbType.String, 50, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_NRO_DEPOSITO_SAP", DbType.String, 50, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_COD_RESP", DbType.Int32, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_MENSAJE", DbType.String, 100, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_DTH.SP_CON_DOCUMENTOSAP"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            nroContrato = IIf(IsDBNull(CType(objRequest.Parameters(1), IDataParameter).Value), 0, CType(objRequest.Parameters(1), IDataParameter).Value)
            nroSec = IIf(IsDBNull(CType(objRequest.Parameters(2), IDataParameter).Value), "", CType(objRequest.Parameters(2), IDataParameter).Value)
            nroDocSAP = IIf(IsDBNull(CType(objRequest.Parameters(3), IDataParameter).Value), "", CType(objRequest.Parameters(3), IDataParameter).Value)
            nroDepSap = IIf(IsDBNull(CType(objRequest.Parameters(4), IDataParameter).Value), "", CType(objRequest.Parameters(4), IDataParameter).Value)
            codRespuesta = CType(objRequest.Parameters(5), IDataParameter).Value
            mensaje = CType(objRequest.Parameters(6), IDataParameter).Value
            validaDocSAPxDTH = True
        Catch ex As Exception
            validaDocSAPxDTH = False
        End Try
    End Function

    Public Function Consulta_estado_contrato(ByVal nroContrato As Int64) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                    New DAAB.DAABRequest.Parameter("P_CONTRATO", DbType.Int64, nroContrato, ParameterDirection.Input), _
                                                    New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 5, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_DTH.SP_CONS_ESTADO_CONTRATO"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            Consulta_estado_contrato = CType(objRequest.Parameters(1), IDataParameter).Value
        Catch ex As Exception
            Consulta_estado_contrato = "-1"
        End Try
    End Function
    Public Function getCorrelativoSGA() As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_VALOR", DbType.String, 5, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_DTH.SP_CORRELATIVO_SGA"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            getCorrelativoSGA = CType(objRequest.Parameters(0), IDataParameter).Value
        Catch ex As Exception
            getCorrelativoSGA = ""
        End Try
    End Function

    Public Function Actualizar_estado_contrato(ByVal nroContrato As Int64, ByVal estado As String, ByVal nroSot As String) As Int64
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_CONTN_NUMERO_CONTRATO", DbType.Int64, nroContrato, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_CONTC_ESTADO", DbType.String, estado, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_NRO_SOT", DbType.String, nroSot, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_COD_RESP", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_DTH.SP_UPD_ESTADO_CONTRATO"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Actualizar_estado_contrato = CType(objRequest.Parameters(3), IDataParameter).Value

    End Function
    'fin DTH UB
    Public Function Consulta_Venta_Cuota(ByVal nroSEC As Int64, ByRef monto_cuota As Double) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                    New DAAB.DAABRequest.Parameter("P_SOLIN_CODIGO", DbType.Int64, nroSEC, ParameterDirection.Input), _
                                                    New DAAB.DAABRequest.Parameter("P_COD_RESP", DbType.Int64, ParameterDirection.Output), _
                                                    New DAAB.DAABRequest.Parameter("P_MONTO_CUOTA", DbType.Double, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_GENERAL_II.SP_CON_DATOS_VENTA_CUOTA"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            Consulta_Venta_Cuota = CType(objRequest.Parameters(1), IDataParameter).Value
            monto_cuota = CType(objRequest.Parameters(2), IDataParameter).Value
        Catch ex As Exception
            Consulta_Venta_Cuota = "-1"
        End Try
    End Function

    Public Function ConsultarVentaRenovPostCAC(ByVal v_documentoSAP As String) As VentaRenovaPost

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_CONSULTA", DbType.Object, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, ParameterDirection.Input) _
                                                }


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(1).Value = v_documentoSAP

        Dim dr As IDataReader = Nothing
        Dim item As VentaRenovaPost = Nothing

        Try


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_EXPRESS_6.SISACT_CONS_VENTA_RENOV_CAC"

            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            While (dr.Read())
                item = New VentaRenovaPost
                item.VENDEDOR = Funciones.CheckStr(dr("VENDEDOR"))
                item.TIPO_RENOVACION = Funciones.CheckStr(dr("TIPO_RENOVACION"))
                item.FLAG_EXONERACION = Funciones.CheckInt(dr("FLAG_EXONERACION"))
                item.FLAG_DESCUENTO = Funciones.CheckInt(dr("FLAG_DESCUENTO"))
                item.TITULAR = Funciones.CheckStr(dr("TITULAR"))
                item.REPRESENTANTE = Funciones.CheckStr(dr("REPRESENTANTE"))
                item.TELEFONO = Funciones.CheckStr(dr("TELEFONO"))
                item.INTERACCION = Funciones.CheckStr(dr("INTERACION"))
                item.TIPO_DOCUMENTO = Funciones.CheckStr(dr("TIPO_DOCUMENTO"))
                item.DOC_CLIEN_NUMERO = Funciones.CheckStr(dr("DOC_CLIE_NUMERO"))
                item.FLAG_FIDELIZADO_RETENIDO = Funciones.CheckStr(dr("FLAG_FIDELIZADO_RETENIDO"))
                item.SOLIN_CODIGO = Funciones.CheckInt64(dr("SOLIN_CODIGO"))
                item.PLAN_NUEVO = Funciones.CheckStr(dr("PLAN_NUEVO"))
                item.TOPE_CONSUMO = Funciones.CheckStr(dr("TOPE_CONSUMO"))
                item.LIMITE_CREDITO = Funciones.CheckDbl(dr("LIMITE_CREDITO"))
                item.CICLO_FACT = Funciones.CheckStr(dr("CICLO_FACT"))
                item.VIGENCIA_PLAN = Funciones.CheckStr(dr("VIGENCIA_PLAN"))
                item.FLAG_CHIP = Funciones.CheckStr(dr("FLAG_RENOV_Y_CHIP"))
                item.NUMERO_CONTRATO = Funciones.CheckStr(dr("CONTRATO_SAP")) 'SD854808 Relanzamiento Cambio de Plan JAZ
                item.CANAL = Funciones.CheckStr(dr("CANAL")) 'SD854808 Relanzamiento Cambio de Plan JAZ
                '<33062>
                item.FLAG_AFILIA_CORREO = Funciones.CheckStr(dr("FLAG_AFILIA_CORREO"))
                '</33062>
                item.FECHA_REGISTRO = Funciones.CheckStr(dr("FECHA_REGISTRO")) 'INICIATIVA 315 - JFG
                item.MONTO_APADECE = Funciones.CheckStr(dr("MONTO_APADECE")) 'INICIATIVA 315 - JFG
            End While


        Catch ex As Exception
            Throw ex
        Finally

            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return item

    End Function

    Public Function ConsultarDatosComplementariosVentaRenovacion(ByVal v_documentoSAP As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_CONSULTA", DbType.Object, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, ParameterDirection.Input) _
                                                }


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(1).Value = v_documentoSAP

        Dim dr As IDataReader = Nothing
        Dim strCFActual As String
        Dim strCFNuevo As String
        Dim strDescuento As String
        Dim strMotivReno As String
        'AGREGADO 22062015
        Dim strNroSec As String


        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_EXPRESS_6.SP_SELECT_VENTARENOV_COMPL"

            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            While (dr.Read())

                strDescuento = IIf(IsDBNull(dr(0)), "", dr(0))
                strCFActual = IIf(IsDBNull(dr(1)), "", dr(1))
                strCFNuevo = IIf(IsDBNull(dr(2)), "", dr(2))
                strMotivReno = IIf(IsDBNull(dr(3)), "", dr(3))
                strNroSec = IIf(IsDBNull(dr(4)), "", dr(4))
            End While


        Catch ex As Exception
            Dim msg As String = ex.Message
        Finally

            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        'modificado 22062015
        Dim result As String = strDescuento & "," & strCFActual & "," & strCFNuevo & "," & strMotivReno & "," & strNroSec

        Return result
    End Function

    Public Function ConsultarClaroPuntosCanje(ByVal v_documentoSAP As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_CONSULTA", DbType.Object, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, ParameterDirection.Input) _
                                                }


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(1).Value = v_documentoSAP

        Dim dr As IDataReader = Nothing
        Dim strpuntosUti As String
        Dim strSolesDcto As String

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_EXPRESS_6.SISACT_CONS_CLAROPUNTOSCANJE"

            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            While (dr.Read())

                strpuntosUti = IIf(IsDBNull(dr(0)), "", dr(0))
                strSolesDcto = IIf(IsDBNull(dr(1)), "", dr(1))
            End While


        Catch ex As Exception
            Dim msg As String = ex.Message
        Finally

            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Dim result As String = strpuntosUti & "," & strSolesDcto

        Return result


    End Function

    Public Function ActualizarCanjePuntosCC(ByVal v_documentoSAP As String, ByVal v_notaCredito As String, ByVal v_usuarioCanje As String, ByVal v_flagCanje As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_NRO_NOTA_CREDITO", DbType.String, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_USUARIO_CANJE", DbType.String, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_FLAG_CANJE", DbType.String, ParameterDirection.Input) _
                                                }


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(0).Value = v_documentoSAP
        arrParam(1).Value = v_notaCredito
        arrParam(2).Value = v_usuarioCanje
        arrParam(3).Value = v_flagCanje

        Dim result As Boolean

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_PROCESOS.MANTSU_UPDATE_CP"

            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
            result = True
        Catch ex As Exception
            Dim msg As String = ex.Message
            result = False
        Finally

            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return result

    End Function

    Public Function ActualizaInteraccionCanjePuntos(ByVal p_nroSap As String, ByVal p_iterac As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                        New DAAB.DAABRequest.Parameter("P_RESULTADO", DbType.Int32, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_INTERACCION", DbType.String, ParameterDirection.Input)}


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(1).Value = p_nroSap
        arrParam(2).Value = p_iterac

        Dim result As Boolean = False
        Dim resultado As Int32 = 1

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_RENOVACION.SISACT_UPD_INTERAC_CANJE"

            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
            result = True
        Catch ex As Exception
            objRequest.Factory.RollBackTransaction()
            result = False
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return result
    End Function

    Public Function ActualizarInteraccion_VentaRenovPostCAC(ByVal interaccion As String, ByVal documentoSAP As String) As Boolean

        Dim vReturn As Boolean = True

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_INTERACCION", DbType.String, interaccion, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, documentoSAP, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_RESULTADO", DbType.Int16, ParameterDirection.Output)}
	'INC000001305625 Inicio	 
        objFileLog.Log_WriteLog(pathFile, strArchivo, " Actualiza interaccion RenovCAC - " & "interaccion : " & interaccion)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " Actualiza interaccion RenovCAC - " & "documentoSAP : " & documentoSAP)
        
        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        objFileLog.Log_WriteLog(pathFile, strArchivo, " Actualiza interaccion RenovCAC - " & "arrParam(0) -  1 : " & arrParam(0).Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " Actualiza interaccion RenovCAC - " & "arrParam(1) -  1 : " & arrParam(1).Value)

        arrParam(0).Value = interaccion
        arrParam(1).Value = documentoSAP
        'INC000001305625 Fin

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTA_EXPRESS_6.SISACT_UPD_INTERAC_RENOV_POST"
        objRequest.Parameters.AddRange(arrParam)

        Try

            objRequest.Factory.ExecuteNonQuery(objRequest)

            If (Integer.Parse(CType(objRequest.Parameters(2), IDataParameter).Value) <> 0) Then
                vReturn = False
            End If

        Catch ex As Exception
            vReturn = False
            Throw ex
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()

        End Try

        Return vReturn

    End Function

    'PLSR OBTENER CODIGO OPCION
    Public Function Obtener_codigo_opcion(ByVal strCodMaterial As String, ByVal strListaPrecio As String, ByVal strCampana As String, ByVal strGrupoEquipo As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_COD_MATERIAL", DbType.String, strCodMaterial, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_LIST_PRECIO", DbType.String, strListaPrecio, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_CAMPANA", DbType.String, strCampana, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_GRUP_EQUIPO", DbType.String, strGrupoEquipo, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_RESULTADO", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SP_OBTENER_COD_OPCION"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Obtener_codigo_opcion = IIf(IsDBNull(CType(objRequest.Parameters(4), IDataParameter).Value), "", CType(objRequest.Parameters(4), IDataParameter).Value)

    End Function
    Public Function Registrar_Migracion_Saldo(ByVal numeroTelefono As String) As Int64
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_STSAV_NNRO_TLF", DbType.String, numeroTelefono, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_RESULTADO", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_MANTENIMIENTO.SP_INS_TRANS_SALDO_GSM"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Registrar_Migracion_Saldo = CType(objRequest.Parameters(1), IDataParameter).Value

    End Function

    'promo modem + laptop
    Public Function ValidaPromocion(ByVal vCampana As String, ByVal vListaPrecio As String, ByVal vPlanTarifa As String, ByVal vNroSolicitud As String, ByVal vGrupo As String) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("P_CAMPANA", DbType.String, 5, vCampana, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_LISTAPRECIO", DbType.String, 5, vListaPrecio, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_PLANTARIFA", DbType.String, 5, vPlanTarifa, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_ACUERDOPLAZO", DbType.String, 5, vNroSolicitud, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_GRUPO", DbType.String, 50, vGrupo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_RESPUESTA", DbType.Int32, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTAS.SP_VALIDA_PROMOCION"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        ValidaPromocion = CType(objRequest.Parameters(5), IDataParameter).Value
    End Function

    Public Function obtenerNroSapAc(ByVal vNroSap As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("P_NRODOCSAP", DbType.String, 50, vNroSap, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NRODOCSAPACC", DbType.String, 50, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTAS.SP_LIS_VTA_ASOC"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        obtenerNroSapAc = CType(objRequest.Parameters(1), IDataParameter).Value
    End Function

    ' 16/01/2013 - ACTUALIZACION DE TABLAS SISACT PREPAGO - INICIO
    Public Function ListarDatosCabeceraVenta(ByVal nroDoc As String) As DataTable
        Try
            Dim resultado As DataSet
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_ID", DbType.String, nroDoc, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_RESULT", DbType.Int32, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_LISTADO", DbType.Object, ParameterDirection.Output) _
                                                }
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_PREPAGO.SISACTS_CON_VPREPAGO"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteDataset(objRequest)
            If resultado.Tables.Count > 0 Then
                Return resultado.Tables(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function actualizaReferencia(ByVal p_pdv As String, ByVal p_docu As String, ByVal p_refe As String) As String
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_PDV", DbType.String, p_pdv, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_DOCU", DbType.String, p_docu, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_REFE", DbType.String, p_refe, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_RESULTADO", DbType.Int32, ParameterDirection.Output) _
                                                }
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_PREPAGO.SISACTU_NROREFERENCIA"
            objRequest.Parameters.AddRange(arrParam)
            Try
                objRequest.Factory.ExecuteNonQuery(objRequest)
                Dim pSalida As String
                pSalida = CType(objRequest.Parameters(3), IDataParameter).Value
                Return pSalida
            Catch ex As Exception
                Return 0
            End Try
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ActualizarPagoCorner(ByVal p_id As String, ByVal p_estado As String, _
                                        ByVal p_almacen As String, ByVal p_tipoDoc As String, _
                                        ByVal p_nroDocPago As String, ByVal p_ticket As String, _
                                        ByRef p_resultado As Integer) As Boolean
        Try
            Dim resultado As Boolean = False
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_ID", DbType.String, p_id, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, p_estado, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_ALMACEN", DbType.String, p_almacen, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_TIPO_DOC_PAGO", DbType.String, p_tipoDoc, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_NUMERO_REFERENCIA", DbType.String, p_nroDocPago, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_NUMERO_TICKET", DbType.String, p_ticket, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_RESULTADO", DbType.Int32, ParameterDirection.Output) _
                                                }
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_PREPAGO2_S6.SISACTU_VENTA_PREPAGO"
            objRequest.Parameters.AddRange(arrParam)
            Try
                objRequest.Factory.ExecuteNonQuery(objRequest)
                Dim pSalida As String
                pSalida = CType(objRequest.Parameters(6), IDataParameter).Value
                If pSalida = "1" Then
                    resultado = True
                End If
                objRequest.Factory.Dispose()
                Return resultado
            Catch ex As Exception
                Return False
            End Try
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' 16/01/2013 - ACTUALIZACION DE TABLAS SISACT PREPAGO - FIN

    ' Roaming
    Public Function ObtenerLineasRoaming(ByVal nroDocumento As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRO_SAP", DbType.String, nroDocumento, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_ROAMING.SISACT_CON_ROAMING"
        objRequest.Parameters.AddRange(arrParam)

        Try
            Return objRequest.Factory.ExecuteDataset(objRequest)
        Catch ex As Exception
            Return New DataSet
        End Try

    End Function

    Public Function InsertarLineasRoaming(ByVal linea As String, ByVal codPlan As String) As String

        Dim retorno As String = ""
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SICARBSCS")


        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam As DAABRequest.Parameter() = {New DAABRequest.Parameter("P_MSISDN", DbType.String, 15, linea, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("P_PARAM_RATEPLAN", DbType.String, 10, codPlan, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("", DbType.String, 1024, ParameterDirection.ReturnValue)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "TIM.TFUN103_INSERTA_LINEA_ROAMING"
        objRequest.Parameters.AddRange(arrParam)

        Try
            objRequest.Factory.ExecuteScalar(objRequest)
            Dim parSalida1 As IDataParameter
            parSalida1 = CType(objRequest.Parameters(2), IDataParameter)
            retorno = CStr(parSalida1.Value)

        Catch ex As Exception
            retorno = ex.Message
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return retorno
    End Function

    Public Function ActualizarLineasRoaming(ByVal linea As String, ByVal codPlan As String, ByVal estado As String) As String

        Dim salida As String = ""
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SICARBSCS")


        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam As DAABRequest.Parameter() = {New DAABRequest.Parameter("P_MSISDN", DbType.String, 15, linea, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("p_param_roam_rateplan", DbType.String, 10, codPlan, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("p_estado_nuevo", DbType.String, 1, estado, ParameterDirection.Input), _
                                                   New DAABRequest.Parameter("", DbType.String, 1024, ParameterDirection.ReturnValue)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "TIM.TFUN104_ACTUALIZA_EST_ROAMING"
        objRequest.Parameters.AddRange(arrParam)

        Try
            objRequest.Factory.ExecuteScalar(objRequest)
            Dim parSalida As IDataParameter
            parSalida = CType(objRequest.Parameters(3), IDataParameter)
            salida = CStr(parSalida.Value)

        Catch ex As Exception
            salida = ex.Message
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return salida
    End Function

    Public Function InsertarStockPELCambiSIM(ByVal codMaterial As String, _
                                            ByVal ICCID As String, _
                                            ByVal codOficina As String, _
                                            ByVal clienteSap As String, _
                                            ByVal nroTelefono As String, _
                                            ByRef codRespuesta As String, _
                                            ByRef mensaje As String) As Boolean

        Dim retorno As Boolean
        Try
            Dim key As String = ConfigurationSettings.AppSettings("BD_PEL")
            strCadenaConexion = objSeg.FP_GetConnectionString("2", key)
            Dim esquema As String = ConfigurationSettings.AppSettings("EsquemaPEL")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam As DAABRequest.Parameter() = {New DAABRequest.Parameter("P_COD_RESP", DbType.Int32, 10, ParameterDirection.Output), _
                                                        New DAABRequest.Parameter("p_MENSAJE", DbType.String, 300, ParameterDirection.Output), _
                                                       New DAABRequest.Parameter("P_MATNR", DbType.String, 18, codMaterial, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("P_SERNR", DbType.String, 18, ICCID, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("P_OFICINA_VENTAS", DbType.String, 4, codOficina, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("P_CLIENTE_SAP", DbType.String, 10, clienteSap, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("P_MSISDN", DbType.String, 20, nroTelefono, ParameterDirection.Input)}

            objRequest.CommandType = CommandType.StoredProcedure
            If esquema <> "" Then
                objRequest.Command = esquema & ".PKG_CAMBIOSIM_2.SP_INSR_STOCKS_CAMBIO_SIM"
            Else
                objRequest.Command = "PKG_CAMBIOSIM_2.SP_INSR_STOCKS_CAMBIO_SIM"
            End If
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteScalar(objRequest)
            Dim parSalida1 As IDataParameter
            Dim parSalida2 As IDataParameter
            parSalida1 = CType(objRequest.Parameters(0), IDataParameter)
            parSalida2 = CType(objRequest.Parameters(1), IDataParameter)
            codRespuesta = CStr(parSalida1.Value)
            mensaje = CStr(parSalida2.Value)
            objRequest.Factory.Dispose()

            If codRespuesta = "0" Then
                retorno = True
            End If
        Catch ex As Exception
            codRespuesta = "-99"
            mensaje = ex.Message
        End Try
        Return retorno
    End Function

    Public Function ConsultaNroSot(ByVal nroSec As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                    New DAAB.DAABRequest.Parameter("P_SOLIN_CODIGO", DbType.String, nroSec, ParameterDirection.Input), _
                                                    New DAAB.DAABRequest.Parameter("P_SOT", DbType.String, 20, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_DTH.SP_CONSULTA_SOT"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            ConsultaNroSot = CType(objRequest.Parameters(1), IDataParameter).Value
        Catch ex As Exception
            ConsultaNroSot = "0"
        End Try
    End Function

    Public Function ActualizaContratoSot(ByVal nroContrato As String, _
                                        ByVal nroSot As String, _
                                        ByVal nroProyecto As String, _
                                        ByVal fechaInstalProg As String, _
                                        ByVal horaInstProg As String, _
                                        ByVal codContratista As String, _
                                        ByVal descContratista As String, _
                                        ByVal codCliente As String) As Boolean
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Try
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                    New DAAB.DAABRequest.Parameter("P_COD_RESP", DbType.String, 20, ParameterDirection.Output), _
                                                    New DAAB.DAABRequest.Parameter("P_NUMERO_CONTRATO", DbType.String, nroContrato, ParameterDirection.Input), _
                                                    New DAAB.DAABRequest.Parameter("P_NUMERO_SOT", DbType.String, nroSot, ParameterDirection.Input), _
                                                    New DAAB.DAABRequest.Parameter("P_NRO_PROYECTO", DbType.String, nroProyecto, ParameterDirection.Input), _
                                                    New DAAB.DAABRequest.Parameter("P_FECHA_INST_PROG", DbType.String, fechaInstalProg, ParameterDirection.Input), _
                                                    New DAAB.DAABRequest.Parameter("P_HORA_INST_PROG", DbType.String, horaInstProg, ParameterDirection.Input), _
                                                    New DAAB.DAABRequest.Parameter("P_COD_CONTRATISTA", DbType.String, codContratista, ParameterDirection.Input), _
                                                    New DAAB.DAABRequest.Parameter("P_CONTRATISTA_DET", DbType.String, descContratista, ParameterDirection.Input), _
                                                    New DAAB.DAABRequest.Parameter("P_CODIGO_CLIENTE_SGA", DbType.String, codCliente, ParameterDirection.Input)}



            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_DTH.SP_UPD_CONTRATO_SOT"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'SP_UPD_CONTRATO_SOT

    Public Function ListarCanjePuntos(ByVal P_NRO_DOC_SAP_NC As String) As DataTable
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim dtResultado As DataTable
            Dim resultado As DataSet
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRO_DOC_SAP_NC", DbType.String, P_NRO_DOC_SAP_NC, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("K_CUR_CLAROPUNTOS_DET", DbType.Object, ParameterDirection.Output)} 'PROY-26366 - FASE II
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_PROCESOS.MANTSS_SELECT_CANJE_PUNTOS"
            objRequest.Parameters.AddRange(arrParam)

            dtResultado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return dtResultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ListarXDocSAP(ByVal P_NRO_DOC_SAP_NC As String) As DataTable
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim dtResultado As DataTable
            Dim resultado As DataSet
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, P_NRO_DOC_SAP_NC, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_PROCESOS.MANTS_SELECT_PUNTOS_CC_DOC_SAP"
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

    Public Sub actualizarPuntosClaroClub(ByVal P_NRO_DOC_SAP_NC As String, _
                                         ByVal NroReferenciaImprimir As String, _
                                         ByVal P_USUARIO_CANJE As String, _
                                         ByVal P_FLAG_CANJE As String)
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_NRO_DOC_SAP_NC", DbType.String, P_NRO_DOC_SAP_NC, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_NRO_NOTA_CREDITO", DbType.String, NroReferenciaImprimir, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_USUARIO_CANJE", DbType.String, P_USUARIO_CANJE, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_FLAG_CANJE", DbType.String, P_FLAG_CANJE, ParameterDirection.Input) _
                                                }
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_PROCESOS.MANTSU_UPDATE_CANJE_PUNTOS"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ValidaBloqueoBolsa(ByVal k_tipo_doc As String, _
                                  ByVal k_num_doc As String, _
                                  ByVal k_tipo_clie As String, _
                                  ByRef k_tipo_doc2 As String, _
                                  ByRef k_estado As String, _
                                  ByRef k_coderror As Double, _
                                  ByRef k_descerror As String)

        Try
            Dim key As String = ConfigurationSettings.AppSettings("BD_PUNTOSCC")
            strCadenaConexion = objSeg.FP_GetConnectionString("2", key)
            Dim esquema As String = ConfigurationSettings.AppSettings("EsquemaPUNTOSCC")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam As DAABRequest.Parameter() = {New DAABRequest.Parameter("K_TIPO_DOC", DbType.String, k_tipo_doc, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("K_NUM_DOC", DbType.String, k_num_doc, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("K_TIPO_CLIE", DbType.String, k_tipo_clie, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("K_TIPO_DOC2", DbType.String, k_tipo_doc2, ParameterDirection.Output), _
                                                       New DAABRequest.Parameter("K_ESTADO", DbType.String, k_estado, ParameterDirection.Output), _
                                                       New DAABRequest.Parameter("K_CODERROR", DbType.Double, k_coderror, ParameterDirection.Output), _
                                                       New DAABRequest.Parameter("K_DESCERROR", DbType.String, k_descerror, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            If esquema <> "" Then
                objRequest.Command = esquema & ".PKG_CC_TRANSACCION.ADMPS_VALBLOQUEOBOLSA"
            Else
                objRequest.Command = "PKG_CC_TRANSACCION.ADMPS_VALBLOQUEOBOLSA"
            End If
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteScalar(objRequest)

            Dim parSalida1 As IDataParameter
            Dim parSalida2 As IDataParameter
            Dim parSalida3 As IDataParameter
            Dim parSalida4 As IDataParameter
            parSalida1 = CType(objRequest.Parameters(3), IDataParameter)
            parSalida2 = CType(objRequest.Parameters(4), IDataParameter)
            parSalida3 = CType(objRequest.Parameters(5), IDataParameter)
            parSalida4 = CType(objRequest.Parameters(6), IDataParameter)
            k_tipo_doc2 = CStr(parSalida1.Value)
            k_estado = CStr(parSalida2.Value)
            k_coderror = CStr(parSalida3.Value)
            k_descerror = CStr(parSalida4.Value)
            objRequest.Factory.Dispose()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ActualizarServRoaming(ByVal nroSap As String, ByVal nroTelefono As String, ByVal flagPago As String, ByRef p_resultado As Int64, ByRef p_mensaje As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRO_SAP", DbType.String, 20, nroSap, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_SOLIV_NUM_TELF_VENTA", DbType.String, 20, (nroTelefono), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FLAG_PAGO", DbType.Int64, (flagPago), ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_RESULTADO", DbType.String, 10, "", ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_MENSAJE", DbType.String, 200, "", ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_ROAMING_SERV.SISACT_UDP_ROM_SERV"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            Dim parSalida1 As IDataParameter
            Dim parSalida2 As IDataParameter
            parSalida1 = CType(objRequest.Parameters(3), IDataParameter)
            parSalida2 = CType(objRequest.Parameters(4), IDataParameter)
            p_resultado = CInt(parSalida1.Value)
            p_mensaje = CStr(parSalida2.Value)
            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function

    Public Function ConsultarSolDireccion(ByVal nroSEC As Int64) As DataTable
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim dtResultado As DataTable
            Dim resultado As DataSet
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("P_CURSOR", DbType.Object, ParameterDirection.Output), _
                                                            New DAAB.DAABRequest.Parameter("P_SOLIN_GRUPO_SEC", DbType.Int64, nroSEC, ParameterDirection.Input)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_GENERAL.SISACT_CON_SOL_DIRECCION_VENTA"
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

    ' Inicio IDEA-13006 ClaroClub - Mejoras en notas de créditos

    ''' <summary>
    ''' Verifica si existe una nota de débito pendiente (diferente de '1') en Claro Club por canjear, 
    ''' identificado por el nùmero de documento SAP.
    ''' </summary>
    ''' <remarks>
    ''' Autor: E77568
    ''' IDEA-13006 ClaroClub - Mejoras en notas de créditos
    ''' </remarks>
    'Public Function ListarCanjePuntosDevolucion(ByVal P_NRO_DOC_SAP_NC As String) As DataTable
    '    Try
    '        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
    '        Dim dtResultado As DataTable
    '        Dim resultado As DataSet
    '        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
    '        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_ID_CANJE", DbType.String, P_NRO_DOC_SAP_NC, ParameterDirection.Input), _
    '                                                        New DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

    '        objRequest.CommandType = CommandType.StoredProcedure
    '        objRequest.Command = "SISACT_PKG_PROCESOS.MANT_SELECT_CANJE_PUNTOS_DEVOL"
    '        objRequest.Parameters.AddRange(arrParam)

    '        resultado = objRequest.Factory.ExecuteDataset(objRequest)
    '        If resultado.Tables.Count > 0 Then
    '            dtResultado = resultado.Tables(0)
    '        End If

    '        Return dtResultado
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function
    ''' <summary>
    ''' Actualiza la operaciòn de devolución de puntos Claro Club, cuando se efectua el pago de la nota de débito.
    ''' </summary>
    ''' <remarks>
    ''' Autor: E77568.
    ''' IDEA-13006 ClaroClub - Mejoras en notas de créditos.
    ''' </remarks>
    Public Sub actualizarDevolucionPuntosCC(ByVal P_NRO_DOCSAP_ND As String, _
                                            ByVal P_USUARIO_FLAG_DEV As String, _
                                            ByVal P_FLAG_DEVOL As String)
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_NRO_DOCSAP_ND", DbType.String, P_NRO_DOCSAP_ND, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_USUARIO_FLAG_DEV", DbType.String, P_USUARIO_FLAG_DEV, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_FLAG_DEVOL", DbType.String, P_FLAG_DEVOL, ParameterDirection.Input) _
                                                }
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_PROCESOS.MANT_UPDATE_CANJE_PUNTOS_DEVOL"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Retorna los datos de la devolución de un pedido en renovación o alta, identificado por el número de nota de crédito.
    ''' </summary>
    ''' <remarks>
    ''' Autor: E77568
    ''' IDEA-13006 ClaroClub - Mejoras en notas de créditos.
    ''' </remarks>
    Public Function ListarDevolXNotaCredito(ByVal P_NRO_DOCSAP_NC As String) As DataTable
        Try

            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim dtResultado As DataTable
            Dim resultado As DataSet
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRO_DOCSAP_NC", DbType.String, P_NRO_DOCSAP_NC, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_PROCESOS.MANT_SEL_CANJE_PUNTOS_DEVOL_NC"
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
    ''' <summary>
    ''' Retorna los datos de la devolución de un pedido en renovación o alta, identificado por el número de nota de débito.
    ''' </summary>
    ''' <remarks>
    ''' Autor: E77568
    ''' IDEA-13006 ClaroClub - Mejoras en notas de créditos.
    ''' </remarks>
    Public Function ListarDevolXNotaDebito(ByVal P_NRO_DOCSAP_ND As String) As DataTable
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim dtResultado As DataTable
            Dim resultado As DataSet
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRO_DOCSAP_ND", DbType.String, P_NRO_DOCSAP_ND, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_PROCESOS.MANT_SEL_CANJE_PUNTOS_DEVOL_ND"
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
    ''' <summary>
    ''' Retorna los datos de la devolución de un pedido en renovación o alta, identificado por el número de nota de débito
    ''' del descuento de equipo.
    ''' </summary>
    ''' <remarks>
    ''' Autor: E77568
    ''' IDEA-13006 ClaroClub - Mejoras en notas de créditos.
    ''' </remarks>
    Public Function ListarDevolXNotaDebitoDscto(ByVal P_NRO_DOCSAP_ND_DSCTO As String) As DataTable
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim dtResultado As DataTable
            Dim resultado As DataSet
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRO_DOCSAP_ND_DSCTO", DbType.String, P_NRO_DOCSAP_ND_DSCTO, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_PROCESOS.MANT_SEL_CANJE_PUNTS_DEV_DSCTO"
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
    Public Sub actualizarNcDevolPuntosCC(ByVal P_NRO_DOCSAP_NC As String, _
                                         ByVal P_USUARIO_UPD_NC As String)
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_NRO_DOCSAP_NC", DbType.String, P_NRO_DOCSAP_NC, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_USUARIO_UPD_NC", DbType.String, P_USUARIO_UPD_NC, ParameterDirection.Input) _
                                                }
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_PROCESOS.MANT_UPD_CANJE_PUNTOS_DEVOL_NC"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub actualizarNdDevolDscto(ByVal P_NRO_DOCSAP_ND_DSCTO As String, _
                                      ByVal P_USUARIO_UPD_ND_DSCTO As String)
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_NRO_DOCSAP_ND_DSCTO", DbType.String, P_NRO_DOCSAP_ND_DSCTO, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_USUARIO_UPD_ND_DSCTO", DbType.String, P_USUARIO_UPD_ND_DSCTO, ParameterDirection.Input) _
                                                }
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_PROCESOS.MANT_UPD_CANJE_PUNTS_DEV_DSCTO"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ' Fin IDEA-13006 ClaroClub - Mejoras en notas de créditos

    ' Inicio IDEA-11056 Renovación TFI para CAC's
    ''' <summary>
    ''' Invoca el proceso de entrega de bono par renovación TFI.
    ''' </summary>
    ''' <param name="P_TIPO_PROC">Envía RENOV/REPOS</param>
    ''' <param name="P_IMEI">El código de IMEI en caso de REPOSICION(REPOS)</param>
    ''' <param name="P_MSISDN">Nro telefónico en formato internacional</param>
    ''' <param name="P_FECHA_PROCESO">Día de ejecución, por ejemplo el sysdate</param>
    ''' <param name="P_USUARIO">Usuario/Sistema que esta ejecutando</param>
    ''' <param name="P_COD_RESULTADO">0: éxito, otro valor: error</param>
    ''' <param name="P_MSJ_RESULTADO">
    ''' 0 Bono Programado correctamente.
    ''' -1 Se requiere IMEI del equipo para el bono.
    ''' -2 Cliente NO se encuentra registrado como TFI.
    ''' -3 La campaña ha vencido.
    ''' -4 La campaña está bloqueada.
    ''' -5 El PLAN del Cliente no esta permitido a recibir Bono de Activacion.
    ''' -6 Cliente ya recibio los bonos permitidos para este año.
    '''</param>
    Public Sub ProgramarBonoRenovRepos(ByVal P_TIPO_PROC As String, _
                                        ByVal P_IMEI As String, _
                                        ByVal P_MSISDN As String, _
                                        ByVal P_FECHA_PROCESO As DateTime, _
                                        ByVal P_USUARIO As String, _
                                        ByRef P_COD_RESULTADO As String, _
                                        ByRef P_MSJ_RESULTADO As String)
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PROM_DOL")

            Dim EsquemaPROMDB As String = ConfigurationSettings.AppSettings("EsquemaPROMDB")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_TIPO_PROC", DbType.String, P_TIPO_PROC, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_IMEI", DbType.String, P_IMEI, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_MSISDN", DbType.String, P_MSISDN, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_FECHA_PROCESO", DbType.Date, P_FECHA_PROCESO, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, P_USUARIO, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_COD_RESULTADO", DbType.String, P_COD_RESULTADO, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_MSJ_RESULTADO", DbType.String, P_MSJ_RESULTADO, ParameterDirection.Output) _
                                                }
            objRequest.CommandType = CommandType.StoredProcedure
            If EsquemaPROMDB <> "" Then
                objRequest.Command = EsquemaPROMDB & "." & "IDE003_PKG_CONTROL_PROMOCIONES.SP_BONO_RENOV_REPOS"
            Else
                objRequest.Command = "IDE003_PKG_CONTROL_PROMOCIONES.SP_BONO_RENOV_REPOS"
            End If

            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)

            P_COD_RESULTADO = CType(objRequest.Parameters(5), IDataParameter).Value
            P_MSJ_RESULTADO = CType(objRequest.Parameters(6), IDataParameter).Value

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ' Fin IDEA-11056 Renovación TFI para CAC's 



    Public Function Lista_Venta_Reposicion(ByVal strDocumentoSAP As String) As ArrayList

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC") 'strCadenaConexion = "user id=pvu;data source=timdev;password=pvu" 

        Dim dtResultado As DataTable
        Dim resultado As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("P_CONSULTA", DbType.Object, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, strDocumentoSAP, ParameterDirection.Input)}


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTA_REPOSICION.SISACTSS_CONS_VENTA_REPO"
        objRequest.Parameters.AddRange(arrParam)

        resultado = objRequest.Factory.ExecuteDataset(objRequest)
        If resultado.Tables.Count > 0 Then
            dtResultado = resultado.Tables(0)
        End If

        Dim filas As New ArrayList

        If Not dtResultado Is Nothing Then
            For i As Integer = 0 To dtResultado.Rows.Count - 1

                Dim item As New VentaReposicion
                Dim dr As DataRow = dtResultado.Rows(i)

                item.FECHA_REGISTRO = Funciones.CheckDate(dr(0))
                item.TIPO_OFICINA = Funciones.CheckStr(dr(1))
                item.OFICINA = Funciones.CheckStr(dr(2))
                item.OFICINA_DESC = Funciones.CheckStr(dr(3))
                item.VENDEDOR = Funciones.CheckStr(dr(4))
                item.USUARIO = Funciones.CheckStr(dr(5))
                item.FLAG_BLOQUEO = Funciones.CheckStr(dr(6))
                item.COD_BLOQUEO = Funciones.CheckStr(dr(7))
                item.TELEFONO = Funciones.CheckStr(dr(9)) 'CORRIGIO A PARTIR DE AQUI
                item.TELEFONO_REFERENCIA = Funciones.CheckStr(dr(10))
                item.ICCID_SERIE_NUEVO = Funciones.CheckStr(dr(11))
                item.ICCID_SERIE_ACTUAL = Funciones.CheckStr(dr(12))
                item.INTER_REPO_EQUI = Funciones.CheckStr(dr(13))
                item.INTER_REPO_SIM = Funciones.CheckStr(dr(14))
                item.INTER_DESBLOQ = Funciones.CheckStr(dr(15))
                item.PRECIO_NETO = Funciones.CheckDbl(dr(16))
                item.PRECIO_BRUTO = Funciones.CheckDbl(dr(17))
                item.ESTADO_REGISTRO = Funciones.CheckStr(dr(18))
                item.TIPO_DOCUMENTO = Funciones.CheckStr(dr(19))
                item.DOC_CLIE_NUMERO = Funciones.CheckStr(dr(20))
                item.TITULAR_NOMBRE = Funciones.CheckStr(dr(21))
                item.TITULAR_APELLIDO = Funciones.CheckStr(dr(22))
                item.REPRESENTANTE = Funciones.CheckStr(dr(23))
                item.MOTI_REPOSICION_COD = Funciones.CheckStr(dr(24))
                item.MOTI_REPOSICION_DES = Funciones.CheckStr(dr(25))
                item.TICKET_PRE_VENTA = Funciones.CheckStr(dr(26))
                filas.Add(item)

            Next
        End If

        Return filas

    End Function

    '------------------------------------------------------------------------------------------------------------------------------

    Public Function RegistrarPagoReposicionPrepago(ByVal strDocumentoSAP As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC") 'strCadenaConexion = "user id=pvu;data source=timdev;password=pvu" 

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("P_VALOR_RETORNO", DbType.Int32, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, strDocumentoSAP, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTA_REPOSICION.SISACTSU_REG_PAGO_VENTA_REPO"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)

        If (CType(objRequest.Parameters(0), IDbDataParameter).Value) = "1" Then
            Return True
        Else
            Return False
        End If

    End Function

    '------------------------------------------------------------------------------------------------------------------------------

    Public Function EjecutarCambioSim(ByRef vIdTransaccion As String, ByVal vIpAplicacion As String, ByVal vUsuarioApp As String, _
        ByVal vTipoDocumento As String, ByVal vNumeroDocumento As String, ByVal vMsisdn As String, ByVal vIccidNuevo As String, _
        ByVal vCodigoBloqueo As String, ByVal vCanalAtencion As String, ByVal vTipoBloqueo As String, ByRef vMensajeRespuesta As String) As Boolean


        Dim Respuesta As Boolean = False

        Dim objCambioSim As New CambioSimWS.ebsCambioSimPrepagoService
        objCambioSim.Timeout = Configuration.ConfigurationSettings.AppSettings("ConstTimeOutBonoPrepago")
        objCambioSim.Url = ConfigurationSettings.AppSettings("consRutaWSCambioSimPrepago")
        Dim strRespuesta As String = ""

        Try

            strRespuesta = objCambioSim.ejecutarCambioSim(vIdTransaccion, vIpAplicacion, vUsuarioApp, vTipoDocumento, vNumeroDocumento, vMsisdn, vIccidNuevo, vCodigoBloqueo, vCanalAtencion, vTipoBloqueo, vMensajeRespuesta)

        Catch ex As Exception

            Respuesta = False
            Throw ex

        Finally

            If strRespuesta = "0" Then
                Respuesta = True
            End If


        End Try

        Return Respuesta

    End Function

    '------------------------------------------------------------------------------------------------------------------------------

    Public Function AsignarBonoPrepago(ByVal idTransaccion As String, ByVal msisdn As String, ByVal imei As String, ByVal codigoArticulo As String, ByVal strMontoVenta As String, ByVal strMontoVentaSinIGV As String, ByVal Proceso As String, ByRef msgSalida As String) As String



        Dim _oTransaccion As New BonoRenovacionReposicionPrepagoWS.BonoRenovacionReposicionWS
        _oTransaccion.Url = ConfigurationSettings.AppSettings("ConstUrlBonoPrepago").ToString()
        _oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
        _oTransaccion.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("ConstTimeOutBonoPrepago").ToString())

        Dim codSalida As String = ""
        Dim nombreServer As String = System.Net.Dns.GetHostName()
        Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString()
        Dim aplicacion As String = ConfigurationSettings.AppSettings("constAplicacion").ToString()

        Try
            codSalida = _oTransaccion.asignaBonoRepPre(idTransaccion, ipServer, aplicacion, msisdn, imei, codigoArticulo, strMontoVenta, strMontoVentaSinIGV, Proceso, msgSalida)

        Catch ex As Exception
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Cajas\clsCajas.vb; Function: AsignarBonoPrepago)"
            msgSalida = String.Format("Exception. {0}|{1}|{2}", codSalida, msgSalida, ex.Message & MaptPath)
            'FIN PROY-140126

            codSalida = "9"
        End Try


        Return codSalida

    End Function

    Public Function AnularBonoPrepago(ByVal vId As String, ByVal vMsisdn As String, _
                                   ByVal vImei As String, ByRef vRespuesta As String) As String
        Dim strResultado As String
        Try
            Dim nombreServer As String = System.Net.Dns.GetHostName()
            Dim vIpAplicacion As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString()
            Dim vAplicacion As String = ConfigurationSettings.AppSettings("constAplicacion")

            Dim objbonoWS As New BonoRenovacionReposicion.BonoRenovacionReposicionWS
            objbonoWS.Url = Configuration.ConfigurationSettings.AppSettings("ConstUrlBonoPrepago")
            objbonoWS.Credentials = System.Net.CredentialCache.DefaultCredentials
            objbonoWS.Timeout = Configuration.ConfigurationSettings.AppSettings("ConstTimeOutBonoPrepago")

            'Método Anula Bono
            strResultado = objbonoWS.anulaBonoRepPre(vId, vIpAplicacion, vAplicacion, vMsisdn, vImei, vRespuesta)
        Catch ex As Exception
            strResultado = 9
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Cajas\clsCajas.vb; Function: AnularBonoPrepago)"
            vRespuesta = String.Format("Exception. {0}|{1}|{2}", strResultado, vRespuesta, ex.Message & MaptPath)
            'FIN PROY-140126

        End Try
        Return strResultado
    End Function

    '------------------------------------------------------------------------------------------------------------------------------

    Public Function Consulta_Existe_Pedido_Reposicion(ByVal v_documentoSAP As String, ByVal v_estado_registro As String, ByRef intFlagBloqueo As Integer, ByRef strCodBloqueo As String, ByRef strTipoBloqueo As String, ByRef strIccidActual As String, ByRef strIccidNuevo As String, ByRef strUsuario As String, ByRef strTipoVenta As String) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC") 'strCadenaConexion = "user id=pvu;data source=timdev;password=pvu" 
        Dim intExiste As Integer = 0
        Dim strTicket As String = ""
        Try

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, v_documentoSAP, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_ESTADO_REGISTRO", DbType.String, v_estado_registro, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_EXISTE", DbType.String, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("P_TIENE_BLOQUEO", DbType.String, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("P_COD_BLOQUEO", DbType.String, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("P_TIPO_BLOQUEO", DbType.String, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("P_ICCID_SERIE_ACTUAL", DbType.String, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("P_ICCID_SERIE_NUEVO", DbType.String, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("P_TIPO_VENTA_REPO", DbType.String, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("P_TICKET_PRE_VENTA", DbType.String, ParameterDirection.Output)}


            'P_DOCUMENTO_SAP IN SISACT_VENTA_REPO_PRE.DOCUMENTO_SAP%TYPE,
            'P_ESTADO_REGISTRO IN SISACT_VENTA_REPO_PRE.ESTADO_REGISTRO%TYPE,
            'P_EXISTE OUT NUMBER,
            'P_TIENE_BLOQUEO OUT NUMBER,
            'P_COD_BLOQUEO OUT SISACT_VENTA_REPO_PRE.COD_BLOQUEO%TYPE,
            'P_TIPO_BLOQUEO OUT SISACT_VENTA_REPO_PRE.TIPO_BLOQUEO%TYPE,
            'P_ICCID_SERIE_ACTUAL OUT SISACT_VENTA_REPO_PRE.ICCID_SERIE_ACTUAL%TYPE,
            'P_ICCID_SERIE_NUEVO OUT SISACT_VENTA_REPO_PRE.ICCID_SERIE_NUEVO%TYPE,
            'P_USUARIO OUT SISACT_VENTA_REPO_PRE.USUARIO%TYPE

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_REPOSICION.SISACTSS_EXISTE_PED_REPO"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            Dim parSalida2 As IDataParameter
            parSalida2 = CType(objRequest.Parameters(2), IDataParameter)
            intExiste = Funciones.CheckInt(parSalida2.Value)

            Dim parSalida3 As IDataParameter
            parSalida3 = CType(objRequest.Parameters(3), IDataParameter)
            intFlagBloqueo = Funciones.CheckInt(parSalida3.Value)

            Dim parSalida4 As IDataParameter
            parSalida4 = CType(objRequest.Parameters(4), IDataParameter)
            strCodBloqueo = Funciones.CheckStr(parSalida4.Value)

            Dim parSalida5 As IDataParameter
            parSalida5 = CType(objRequest.Parameters(5), IDataParameter)
            strTipoBloqueo = Funciones.CheckStr(parSalida5.Value)

            Dim parSalida6 As IDataParameter
            parSalida6 = CType(objRequest.Parameters(6), IDataParameter)
            strIccidActual = Funciones.CheckStr(parSalida6.Value)

            Dim parSalida7 As IDataParameter
            parSalida7 = CType(objRequest.Parameters(7), IDataParameter)
            strIccidNuevo = Funciones.CheckStr(parSalida7.Value)

            Dim parSalida8 As IDataParameter
            parSalida8 = CType(objRequest.Parameters(8), IDataParameter)
            strUsuario = Funciones.CheckStr(parSalida8.Value)

            Dim parSalida9 As IDataParameter
            parSalida9 = CType(objRequest.Parameters(9), IDataParameter)
            strTipoVenta = Funciones.CheckStr(parSalida9.Value)

            Dim parSalida10 As IDataParameter
            parSalida10 = CType(objRequest.Parameters(10), IDataParameter)
            strTicket = Funciones.CheckStr(parSalida10.Value)

        Catch ex As Exception
            intExiste = -1
            Throw ex
        End Try


        Return intExiste

    End Function

    '------------------------------------------------------------------------------------------------------------------------------

    Public Function insertarBonoReposicion(ByRef P_VALOR_RETORNO As Integer, ByVal P_DOCUMENTO_SAP As String, ByVal P_ENTREGA_BONO As String, _
                                     ByVal P_TIPO_OFICINA As String, ByVal P_OFICINA As String, ByVal P_FECHA_VENTA As String, _
                                     ByVal P_TELEFONO As String, ByVal P_EQUIPO_SERIE As String, ByVal P_CODIGO_SERVICIO As String, _
                                     ByVal P_MENSAJE_SERVICIO As String) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC") 'strCadenaConexion = "user id=pvu;data source=timdev;password=pvu" 
        Dim intExiste As Integer = 0

        Try

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                New DAAB.DAABRequest.Parameter("p_valor_retorno", DbType.Double, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("p_documento_sap", DbType.String, P_DOCUMENTO_SAP, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("p_entrega_bono", DbType.String, P_ENTREGA_BONO, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("p_tipo_oficina", DbType.String, P_TIPO_OFICINA, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("p_oficina", DbType.String, P_OFICINA, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("p_telefono", DbType.String, P_TELEFONO, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("p_equipo_serie", DbType.String, P_EQUIPO_SERIE, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("p_codigo_servicio", DbType.String, P_CODIGO_SERVICIO, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("p_mensaje_servicio", DbType.String, P_MENSAJE_SERVICIO, ParameterDirection.Input)}


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "sisact_pkg_venta_reposicion.sisactsu_ins_venta_repo_bono"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            Dim parSalida As IDataParameter
            parSalida = CType(objRequest.Parameters(0), IDataParameter)
            intExiste = Funciones.CheckInt(parSalida.Value)
            P_VALOR_RETORNO = intExiste

        Catch ex As Exception
            intExiste = -1
            Throw ex
        End Try


        Return intExiste

    End Function


    Public Function AnularReposicionPrepagoSisact(ByVal vDocumentoSap As String, ByVal strEstado As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC") 'strCadenaConexion = "user id=pvu;data source=timdev;password=pvu" '

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
        New DAAB.DAABRequest.Parameter("P_VALOR_RETORNO", DbType.Int32, ParameterDirection.Output), _
        New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, vDocumentoSap, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("P_ESTADO_REGISTRO", DbType.String, strEstado, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTA_REPOSICION.SISACTSU_ANULA_VENTA_REPO"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)

        Dim salida As Boolean = False
        Dim resultado As Integer = 0

        Try

            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Factory.CommitTransaction()
            salida = True

        Catch ex As Exception

        Finally
            Dim parSalida As IDataParameter
            parSalida = CType(objRequest.Parameters(0), IDataParameter)
            resultado = Funciones.CheckInt(parSalida.Value)
            salida = (resultado = 1)

        End Try

        Return salida

    End Function

    Public Function ObtenerDetalleVentaSisact(ByVal nroSec As Int64) As DataSet
        Dim ds As DataSet
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("p_solin_codigo", DbType.Int64, nroSec, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("k_cur_plan", DbType.Object, ParameterDirection.Output), _
                                        New DAAB.DAABRequest.Parameter("k_cur_servicio", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "sisact_pkg_general.sisact_con_plan_det_venta"
            objRequest.Parameters.AddRange(arrParam)
            ds = objRequest.Factory.ExecuteDataset(objRequest)
        Catch ex As Exception
            ds = Nothing
        End Try
        Return ds
    End Function


    Public Function AnulacionReserva(ByVal strComercio As String, _
                                            ByVal strCodTxn As String, _
                                            ByVal strIccid As String, _
                                            ByVal strImei As String, _
                                            ByVal strMsisdn As String, _
                                            ByVal strCodPtoVenta As String, _
                                            ByVal strCodVendedor As String, _
                                            ByVal strOperacionClaro As String, _
                                            ByRef strCodigoRespuesta As String, _
                                            ByRef strDescripcionRespuesta As String) As Boolean

        Dim retorno As Boolean = False

        Try

            Dim strIdentifyLog As String = "Anulacion Reserva - "
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Anulacion Reserva")
            Dim key As String = ConfigurationSettings.AppSettings("BD_SIXPROV")
            strCadenaConexion = objSeg.FP_GetConnectionString("2", key)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strCadenaConexion : " & strCadenaConexion)

            Dim esquema As String = ConfigurationSettings.AppSettings("EsquemaSIXPROV")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Esquema : " & esquema)


            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam As DAABRequest.Parameter() = {New DAABRequest.Parameter("P_comercio", DbType.String, 40, strComercio, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("P_CODTXN", DbType.String, 40, strCodTxn, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("p_iccid", DbType.String, 20, strIccid, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("p_imei", DbType.String, 20, strImei, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("p_msisdn", DbType.String, 20, strMsisdn, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("p_codPtoVenta", DbType.String, 20, strCodPtoVenta, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("p_codVendedor", DbType.String, 20, strCodVendedor, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("p_numOperacionClaro", DbType.String, 40, strOperacionClaro, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("Po_CODRESP", DbType.String, 20, ParameterDirection.Output), _
                                                       New DAABRequest.Parameter("Po_DESCRESP", DbType.String, 100, ParameterDirection.Output)}





            objRequest.CommandType = CommandType.StoredProcedure
            If esquema <> "" Then
                objRequest.Command = esquema & ".CLARO019_SISACT_V2.SEGPROV_ANULACION_RESERVA"
            Else
                objRequest.Command = "CLARO019_SISACT_V2.SEGPROV_ANULACION_RESERVA"
            End If


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strComercio : " & strComercio)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strCodTxn : " & strCodTxn)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strIccid : " & strIccid)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strImei : " & strImei)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strMsisdn : " & strMsisdn)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strCodPtoVenta : " & strCodPtoVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strCodVendedor : " & strCodVendedor)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strOperacionClaro : " & strOperacionClaro)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "SP: " & objRequest.Command)


            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            'objRequest.Factory.ExecuteScalar(objRequest)
            Dim parSalida1 As IDataParameter
            Dim parSalida2 As IDataParameter
            parSalida1 = CType(objRequest.Parameters(8), IDataParameter)
            parSalida2 = CType(objRequest.Parameters(9), IDataParameter)
            strCodigoRespuesta = CStr(parSalida1.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strCodigoRespuesta: " & strCodigoRespuesta)
            strDescripcionRespuesta = CStr(parSalida2.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strDescripcionRespuesta: " & strDescripcionRespuesta)
            objRequest.Factory.CommitTransaction()
            'objRequest.Factory.Dispose()

            If strCodigoRespuesta = "0" Or strCodigoRespuesta = "00" Then
                retorno = True
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "retorno: " & retorno)

        Catch ex As Exception
            strCodigoRespuesta = "999"
            strDescripcionRespuesta = ex.Message

        End Try
        Return retorno
    End Function

    Public Function RegistroLineaCobertura(ByVal MSISDN As String, _
                                            ByVal Departamento As String, _
                                            ByVal Provincia As String, _
                                            ByVal Distrito As String, _
                                            ByVal CPF As String, _
                                            ByVal Cobertura As String, _
                                            ByVal Usuario As String, _
                                            ByRef CodResp As Integer) As String


        Dim DescripcionRespuesta As String = ""

        Try
            Dim key As String = ConfigurationSettings.AppSettings("BD_EAI")
            strCadenaConexion = objSeg.FP_GetConnectionString("2", key)
            'strCadenaConexion = "user id=USREAIDESA;data source=timdev;password=USREAIDESA"
            Dim esquema As String = ConfigurationSettings.AppSettings("EsquemaEAI")



            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam As DAABRequest.Parameter() = {New DAABRequest.Parameter("P_MSISDN", DbType.String, 20, MSISDN, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("P_DEPARTAMENTO", DbType.String, 50, Departamento, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("P_PROVINCIA", DbType.String, 50, Provincia, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("P_DISTRITO", DbType.String, 50, Distrito, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("P_CPF", DbType.String, 50, CPF, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("P_COBERTURA", DbType.String, 10, Cobertura, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("P_USUARIO", DbType.String, 20, Usuario, ParameterDirection.Input), _
                                                       New DAABRequest.Parameter("P_CODE", DbType.Int16, 4, ParameterDirection.Output), _
                                                       New DAABRequest.Parameter("P_MSG", DbType.String, 200, ParameterDirection.Output)}


            objRequest.CommandType = CommandType.StoredProcedure
            If esquema <> "" Then
                objRequest.Command = esquema & ".PKG_EAI_COBERTURA.SP_INSERTA"
            Else
                objRequest.Command = "PKG_EAI_COBERTURA.SP_INSERTA"
            End If
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteScalar(objRequest)
            Dim parSalida1 As IDataParameter
            Dim parSalida2 As IDataParameter
            parSalida1 = CType(objRequest.Parameters(7), IDataParameter)
            parSalida2 = CType(objRequest.Parameters(8), IDataParameter)
            CodResp = CInt(parSalida1.Value)
            DescripcionRespuesta = CStr(parSalida2.Value)
            objRequest.Factory.Dispose()

        Catch ex As Exception
            DescripcionRespuesta = "Error al Registrar la cobertura en DBEAI. \nMensaje : " & ex.Message
        End Try

        Return DescripcionRespuesta

    End Function


    '***********************************'
    '*** FLUJO DE CAJA : 09.03.2014 ****'
    '***********************************'
    '1. NUEVO :
    Public Function RegistrarPago(ByVal P_OFICINA_VENTA As String, _
                                    ByVal P_FECHA As String, _
                                    ByVal P_CAJERO As String, _
                                    ByVal P_DESC_DOCUMENTO As String, _
                                    ByVal P_FACTURA_FICTICIA As String, _
                                    ByVal P_REFERENCIA As String, _
                                    ByVal P_VENDEDOR As String, _
                                    ByVal P_MONEDA As String, _
                                    ByVal P_CLASE_FACTURA_COD As String, _
                                    ByVal P_NRO_CUOTAS As String, _
                                    ByVal P_TOTAL_FACTURA As Double, _
                                    ByVal P_SALDO As Double, _
                                    ByVal P_REFERENCIA_ORIG As String, _
                                    ByVal P_ESTADO As String, _
                                    ByVal P_NODO As String, _
                                    ByVal P_USUARIO_CREACION As String, _
                                    ByRef P_ID_TI_VENTAS_FACT As String, _
                                    ByVal P_MSGERR As String, _
                                    Optional ByVal P_NRO_TELEFONO As String = "", _
                                    Optional ByVal P_NROOPE_TRACE As String = "") As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA_VENTA", DbType.String, 4, P_OFICINA_VENTA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 8, P_FECHA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, P_CAJERO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_DESC_DOCUMENTO", DbType.String, P_DESC_DOCUMENTO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_FACTURA_FICTICIA", DbType.String, 10, P_FACTURA_FICTICIA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_REFERENCIA", DbType.String, 16, P_REFERENCIA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_VENDEDOR", DbType.String, 10, P_VENDEDOR, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_MONEDA", DbType.String, 10, P_MONEDA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CLASE_FACTURA_COD", DbType.String, 4, P_CLASE_FACTURA_COD, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NRO_CUOTAS", DbType.String, 2, P_NRO_CUOTAS, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_TOTAL_FACTURA", DbType.Double, P_TOTAL_FACTURA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_SALDO", DbType.Double, P_SALDO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_REFERENCIA_ORIG", DbType.String, 16, P_REFERENCIA_ORIG, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 2, P_ESTADO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NODO", DbType.String, 2, P_NODO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_USUARIO_CREACION", DbType.String, 10, P_USUARIO_CREACION, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NUMERO_TELEFONO", DbType.String, 20, P_NRO_TELEFONO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NROOPE_TRACE", DbType.String, 20, P_NROOPE_TRACE, ParameterDirection.Input), _
                          New DAABRequest.Parameter("P_ID_TI_VENTAS_FACT", DbType.String, 22, ParameterDirection.Output), _
                          New DAABRequest.Parameter("P_MSGERR", DbType.String, 100, ParameterDirection.Output)}


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PCK_SICAR_OFF_SAP.MIG_RegistrarVentasFactCuadre"
        objRequest.Parameters.AddRange(arrParam)
        RegistrarPago = objRequest.Factory.ExecuteNonQuery(objRequest)

        P_ID_TI_VENTAS_FACT = Convert.ToString(CType(objRequest.Parameters(18), IDataParameter).Value)
        If P_ID_TI_VENTAS_FACT = "" Then
            P_MSGERR = Convert.ToString(CType(objRequest.Parameters(19), IDataParameter).Value)
        End If
    End Function


    '***********************************'
    '*** FLUJO DE CAJA : 19.10.2020 ****'
    '***********************************'
    '1. NUEVO :
    Public Function RegistrarPagoAPK(ByVal P_OFICINA_VENTA As String, _
                                    ByVal P_FECHA As String, _
                                    ByVal P_CAJERO As String, _
                                    ByVal P_DESC_DOCUMENTO As String, _
                                    ByVal P_FACTURA_FICTICIA As String, _
                                    ByVal P_REFERENCIA As String, _
                                    ByVal P_VENDEDOR As String, _
                                    ByVal P_MONEDA As String, _
                                    ByVal P_CLASE_FACTURA_COD As String, _
                                    ByVal P_NRO_CUOTAS As String, _
                                    ByVal P_TOTAL_FACTURA As Double, _
                                    ByVal P_SALDO As Double, _
                                    ByVal P_REFERENCIA_ORIG As String, _
                                    ByVal P_ESTADO As String, _
                                    ByVal P_NODO As String, _
                                    ByVal P_USUARIO_CREACION As String, _
                                    ByVal P_PAGOAPK As String, _
                                    ByRef P_ID_TI_VENTAS_FACT As String, _
                                    ByVal P_MSGERR As String, _
                                    Optional ByVal P_NRO_TELEFONO As String = "", _
                                    Optional ByVal P_NROOPE_TRACE As String = "") As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA_VENTA", DbType.String, 4, P_OFICINA_VENTA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 8, P_FECHA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, P_CAJERO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_DESC_DOCUMENTO", DbType.String, P_DESC_DOCUMENTO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_FACTURA_FICTICIA", DbType.String, 10, P_FACTURA_FICTICIA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_REFERENCIA", DbType.String, 16, P_REFERENCIA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_VENDEDOR", DbType.String, 10, P_VENDEDOR, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_MONEDA", DbType.String, 10, P_MONEDA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CLASE_FACTURA_COD", DbType.String, 4, P_CLASE_FACTURA_COD, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NRO_CUOTAS", DbType.String, 2, P_NRO_CUOTAS, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_TOTAL_FACTURA", DbType.Double, P_TOTAL_FACTURA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_SALDO", DbType.Double, P_SALDO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_REFERENCIA_ORIG", DbType.String, 16, P_REFERENCIA_ORIG, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 2, P_ESTADO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NODO", DbType.String, 2, P_NODO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_USUARIO_CREACION", DbType.String, 10, P_USUARIO_CREACION, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NUMERO_TELEFONO", DbType.String, 20, P_NRO_TELEFONO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NROOPE_TRACE", DbType.String, 20, P_NROOPE_TRACE, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_PAGOAPK", DbType.String, 20, P_PAGOAPK, ParameterDirection.Input), _
                          New DAABRequest.Parameter("P_ID_TI_VENTAS_FACT", DbType.String, 22, ParameterDirection.Output), _
                          New DAABRequest.Parameter("P_MSGERR", DbType.String, 100, ParameterDirection.Output)}


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PCK_SICAR_OFF_SAP.MIG_RegistrarVentasFactCuadre2"
        objRequest.Parameters.AddRange(arrParam)
        RegistrarPagoAPK = objRequest.Factory.ExecuteNonQuery(objRequest)

        P_ID_TI_VENTAS_FACT = Convert.ToString(CType(objRequest.Parameters(19), IDataParameter).Value)
        If P_ID_TI_VENTAS_FACT = "" Then
            P_MSGERR = Convert.ToString(CType(objRequest.Parameters(20), IDataParameter).Value)
        End If
    End Function

    '2. NUEVO: ERROR:
    Public Function RegistrarPagoDetalle(ByVal P_ID_TI_VENTAS_FACT As Int64, _
                                        ByVal P_FECHA As String, _
                                        ByVal P_FACTURA_FICTICIA As String, _
                                        ByVal P_REFERENCIA As String, _
                                        ByVal P_CLASE_FACTURA_COD As String, _
                                        ByVal P_MEDIO_PAGO As String, _
                                        ByVal P_REF_NC As String, _
                                        ByVal P_MONTO As Double, _
                                        ByVal P_ESTADO As String, _
                                        ByVal P_USUARIO_CREACION As String, _
                                        ByRef P_MSGERR As String) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_ID_TI_VENTAS_FACT", DbType.Int64, P_ID_TI_VENTAS_FACT, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 8, P_FECHA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_FACTURA_FICTICIA", DbType.String, 10, P_FACTURA_FICTICIA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_REFERENCIA", DbType.String, 16, P_REFERENCIA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CLASE_FACTURA_COD", DbType.String, 4, P_CLASE_FACTURA_COD, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_MEDIO_PAGO", DbType.String, 4, P_MEDIO_PAGO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_REF_NC", DbType.String, 16, P_REF_NC, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_MONTO", DbType.Double, P_MONTO, ParameterDirection.Input), _
                           New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, P_ESTADO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_USUARIO_CREACION", DbType.String, 10, P_USUARIO_CREACION, ParameterDirection.Input), _
                          New DAABRequest.Parameter("P_MSGERR", DbType.String, 100, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PCK_SICAR_OFF_SAP.MIG_RegVentasFactMedioCua"
        objRequest.Parameters.AddRange(arrParam)
        RegistrarPagoDetalle = objRequest.Factory.ExecuteNonQuery(objRequest)

        P_MSGERR = Convert.ToString(CType(objRequest.Parameters(10), IDataParameter).Value)

    End Function

    '** 3. NUEVO :
    Public Function RegistrarDetalleCuota(ByVal P_ID_TI_VENTAS_FACT As Int64, _
                                            ByVal P_FECHA As String, _
                                            ByVal P_FACTURA_FICTICIA As String, _
                                            ByVal P_REFERENCIA As String, _
                                            ByVal P_CLASE_FACTURA_COD As String, _
                                            ByVal P_NUMERO_CUOTAS As String, _
                                            ByVal P_MONTO As Double, _
                                            ByVal P_USUARIO_CREACION As String, _
                                            ByRef P_MSGERR As String) As Integer


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_ID_TI_VENTAS_FACT", DbType.Int64, P_ID_TI_VENTAS_FACT, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 8, P_FECHA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_FACTURA_FICTICIA", DbType.String, 10, P_FACTURA_FICTICIA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_REFERENCIA", DbType.String, 16, P_REFERENCIA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CLASE_FACTURA_COD", DbType.String, 4, P_CLASE_FACTURA_COD, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_NUMERO_CUOTAS", DbType.String, 4, P_NUMERO_CUOTAS, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_MONTO", DbType.Double, P_MONTO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_USUARIO_CREACION", DbType.String, P_USUARIO_CREACION, ParameterDirection.Input), _
                          New DAABRequest.Parameter("P_MSGERR", DbType.String, 100, ParameterDirection.Output)}


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PCK_SICAR_OFF_SAP.MIG_RegVentasFactCuota"
        objRequest.Parameters.AddRange(arrParam)
        RegistrarDetalleCuota = objRequest.Factory.ExecuteNonQuery(objRequest)

        P_MSGERR = Convert.ToString(CType(objRequest.Parameters(8), IDataParameter).Value)

    End Function

    '** 4. NUEVO :
    Public Function RegistrarAnulaDetallePago(ByVal P_FACTURAFICTICIA As String, _
                                              ByVal P_ESTADO As String, _
                                              ByVal P_USUARIO As String, _
                                              ByRef P_CODERR As String, _
                                              ByRef P_MSGERR As String) As Integer


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_FACTURAFICTICIA", DbType.String, 10, P_FACTURAFICTICIA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 2, P_ESTADO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, P_USUARIO, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CODERR", DbType.String, ParameterDirection.Output), _
                                                        New DAABRequest.Parameter("P_MSGERR", DbType.String, 100, ParameterDirection.Output)}


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PCK_SICAR_OFF_SAP.MIG_ActEstadoVentasFactMedio"
        objRequest.Parameters.AddRange(arrParam)
        RegistrarAnulaDetallePago = objRequest.Factory.ExecuteNonQuery(objRequest)

        P_CODERR = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
        P_MSGERR = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
    End Function

    '** 5. NUEVO 
    Public Function RegistrarVentaDetalle(ByVal P_OFICINA_VENTA As String, _
                                          ByVal P_FECHA As String, _
                                          ByVal P_CAJERO As String, _
                                          ByVal P_DESC_DOCUMENTO As String, _
                                          ByVal P_FACTURA_FICTICIA As String, _
                                          ByVal P_REFERENCIA As String, _
                                          ByVal P_VENDEDOR As String, _
                                          ByVal P_MATERIAL As String, _
                                          ByVal P_DESC_MATERIAL As String, _
                                          ByVal P_UNIDAD As String, _
                                          ByVal P_CANTIDAD_FACTURADA As Int64, _
                                          ByVal P_VALOR_NETO As Double, _
                                          ByVal P_NRO_SERIE As String, _
                                          ByVal P_MODALIDAD As String, _
                                          ByVal P_DENOMINACION As String, _
                                          ByVal P_NODO As String, _
                                          ByVal P_USUARIO_CREACION As String, _
                                          ByRef P_MSGERR As String) As Integer


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA_VENTA", DbType.String, 4, P_OFICINA_VENTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 8, P_FECHA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, P_CAJERO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DESC_DOCUMENTO", DbType.String, 30, P_DESC_DOCUMENTO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FACTURA_FICTICIA", DbType.String, 10, P_FACTURA_FICTICIA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_REFERENCIA", DbType.String, 16, P_REFERENCIA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_VENDEDOR", DbType.String, 10, P_VENDEDOR, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MATERIAL", DbType.String, 18, P_MATERIAL, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DESC_MATERIAL", DbType.String, 50, P_DESC_MATERIAL, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_UNIDAD", DbType.String, 3, P_UNIDAD, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CANTIDAD_FACTURADA", DbType.Int64, 16, P_CANTIDAD_FACTURADA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_VALOR_NETO", DbType.Double, 16, P_VALOR_NETO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NRO_SERIE", DbType.String, 18, P_NRO_SERIE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MODALIDAD", DbType.String, 3, P_MODALIDAD, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DENOMINACION", DbType.String, 25, P_DENOMINACION, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NODO", DbType.String, 2, P_NODO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_USUARIO_CREACION", DbType.String, 10, P_USUARIO_CREACION, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_MSGERR", DbType.String, 100, ParameterDirection.Output)}





        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PCK_SICAR_OFF_SAP.MIG_RegistrarMaterFact"
        objRequest.Parameters.AddRange(arrParam)
        RegistrarVentaDetalle = objRequest.Factory.ExecuteNonQuery(objRequest)

        P_MSGERR = Convert.ToString(CType(objRequest.Parameters(17), IDataParameter).Value)
    End Function

    '**FACTURA:
    Public Function GetDepartamento(ByVal codDep As String, ByVal estado As String) As DataTable
        Try
            Dim resultado As DataSet
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("K_COD_DEPARTAMENTO", DbType.String, codDep, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("K_ESTADO", DbType.String, estado, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output) _
                                                }
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SECP_PKG_MAESTROS.SECSS_CON_DEPARTAMENTO"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteDataset(objRequest)
            If resultado.Tables.Count > 0 Then
                Return resultado.Tables(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetDistrito(ByVal codDist As String, ByVal codProv As String, ByVal codDep As String, ByVal estado As String) As DataTable
        Try
            Dim resultado As DataSet
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("K_COD_DISTRITO", DbType.String, codDist, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("K_COD_PROVINCIA", DbType.String, codProv, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("K_COD_DEPARTAMENTO", DbType.String, codDep, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("K_ESTADO", DbType.String, estado, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output) _
                                                }
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SECP_PKG_MAESTROS.SECSS_CON_DISTRITO"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteDataset(objRequest)
            If resultado.Tables.Count > 0 Then
                Return resultado.Tables(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'FE - Fin Lisetti Macedo
    'FE - Cambio de direccion
    '***ESTE METODO NO SE ESTA USANDO***'
    Public Function GetDireccion(ByVal nroDocSap As Integer, ByRef cod_resp As String, ByRef msg_resp As String) As DataTable
        Try
            Dim resultado As DataSet
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("P_DOC_SAP", DbType.Int32, nroDocSap, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("P_COD_RESP", DbType.String, cod_resp, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("P_MSG_RESP", DbType.String, msg_resp, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("C_DIRECCION", DbType.Object, ParameterDirection.Output) _
                                                }


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_ACUERDO_46.SP_DIRECCION"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteDataset(objRequest)
            If resultado.Tables.Count > 0 Then
                Return resultado.Tables(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'FE - Fin Lisetti Macedo

    'FE Mejora Salto Correlativo
    Public Function SP_UPD_FLAG_PAPER(ByVal NumFactSap As String, ByVal FlagPaper As String, ByVal Referencia As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("NUM_FACT_SAP", DbType.String, NumFactSap, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("FLAG_PAPER", DbType.String, FlagPaper, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PREFERENCIA", DbType.String, Referencia, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("COD_RPTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("MENSAJE_RPTA", DbType.String, ParameterDirection.Output) _
}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SP_UPD_FLAG_PAPER"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        Dim Num_Error = CType(objRequest.Parameters(3), IDataParameter).Value
        Dim Descr_Error = CType(objRequest.Parameters(4), IDataParameter).Value


        objRequest.Parameters.Clear()
        objRequest.Factory.Dispose()

        SP_UPD_FLAG_PAPER = Num_Error
    End Function

    Public Function Actualiza_Del_CC(ByVal nroSec As Int64, ByVal nroDocSap As String) As Boolean
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_SOLINCODIGO", DbType.Int64, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_COD_ERROR", DbType.Int32, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_MENS_ERROR", DbType.String, ParameterDirection.Output)}


        arrParam(0).Value = nroSec
        arrParam(1).Value = nroDocSap

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTA_EXP_3PLAY_6.SP_UPDATE_DEL_CC"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Transactional = True
        objRequest.Factory.ExecuteNonQuery(objRequest)
        objRequest.Factory.CommitTransaction()

        If Funciones.CheckStr(CType(objRequest.Parameters(2), IDbDataParameter).Value) = "1" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ConsultaDocPagos(ByVal NroDocSAP As String, ByVal origen_aplic As String) As DataSet
        'Conexion
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("NUM_FACT_SAP", DbType.String, NroDocSAP, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("ORIGEN_APLIC", DbType.String, origen_aplic, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("COD_RPTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("MENSAJE_RPTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("CAMPOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SP_CON_DOCUMENTO"
        objRequest.Parameters.AddRange(arrParam)

        ConsultaDocPagos = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function ConsultaPlanSEC(ByVal pstrNroSec As String, ByRef pstrCodigoBSCS As String) As String
        Dim strRspta As String = ""

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam As DAABRequest.Parameter() = {New DAAB.DAABRequest.Parameter("P_CURSOR", DbType.Object, ParameterDirection.Output), _
                                                   New DAABRequest.Parameter("P_SOLICITUD", DbType.Int64, pstrNroSec, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_GENERAL.sp_traer_planxtelefono"
        objRequest.Parameters.AddRange(arrParam)
        Dim dr As IDataReader

        Try
            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader

            While dr.Read
                strRspta = Funciones.CheckStr(dr("PLANC_CODIGO"))
                pstrCodigoBSCS = Funciones.CheckStr(dr("CODIGO_BSCS"))
            End While
        Catch e As Exception
            Throw e
        Finally
            If Not dr Is Nothing AndAlso dr.IsClosed = False Then dr.Close()
            objRequest.Parameters.Clear()
        End Try

        Return strRspta
    End Function

    Public Function Actualizar_Estado_Pago(ByVal NumFactSap As String, ByVal Origen As String, ByVal Estado As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("NUM_FACT_SAP", DbType.String, 20, NumFactSap, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("ORIGEN_PAGO", DbType.String, 1, Origen, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("ESTADO_PAGO", DbType.String, 1, Estado, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("COD_RPTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("MENSAJE_RPTA", DbType.String, ParameterDirection.Output) _
                            }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SP_UPD_ESTADO_DOCUM"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        Dim Num_Error = CType(objRequest.Parameters(3), IDataParameter).Value
        Dim Descr_Error = CType(objRequest.Parameters(4), IDataParameter).Value

        Actualizar_Estado_Pago = Num_Error & "-" & Descr_Error

    End Function
    'FE Mejora Salto Correlativo

    '****CONSULTA VENDEDOR
    Public Function ConsultaVendedor(ByVal CodVendedor As String, ByVal PuntoVenta As String, ByVal Estado As Int32) As DataTable
        Try
            Dim resultado As DataSet
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                New DAAB.DAABRequest.Parameter("p_codigo", DbType.String, CodVendedor, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("p_pdv_codigo", DbType.String, PuntoVenta, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("p_estado", DbType.Int32, Estado, ParameterDirection.Input), _
                                                New DAAB.DAABRequest.Parameter("p_result", DbType.Int32, ParameterDirection.Output), _
                                                New DAAB.DAABRequest.Parameter("p_listado", DbType.Object, ParameterDirection.Output)}


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONS_MAESTRA_SAP_6.SISACT_VENDEDORES_MSSAP_CONS"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteDataset(objRequest)

            If Not resultado Is Nothing Then
                If resultado.Tables.Count > 0 Then
                    Return resultado.Tables(0)
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function AnularPedido_CuadreCaja(ByVal P_FACTURAFICTICIA As String, _
                                             ByVal P_ESTADO As String, _
                                             ByVal P_USUARIO As String, _
                                             ByRef P_CODERR As String, _
                                             ByRef P_MSGERR As String) As Integer


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_FACTURAFICTICIA", DbType.String, 10, P_FACTURAFICTICIA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 2, P_ESTADO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, P_USUARIO, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CODERR", DbType.String, ParameterDirection.Output), _
                                                        New DAABRequest.Parameter("P_MSGERR", DbType.String, 100, ParameterDirection.Output)}


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PCK_SICAR_OFF_SAP.MIG_ActEstadoVentasFact"
        objRequest.Parameters.AddRange(arrParam)
        AnularPedido_CuadreCaja = objRequest.Factory.ExecuteNonQuery(objRequest)

        P_CODERR = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
        P_MSGERR = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
    End Function

    Public Function AnularPago_CuadreCaja_cadena(ByVal P_CodigoDRa As String, _
                                                ByVal P_ESTADO As String, _
                                                ByVal P_USUARIO As String, _
                                                ByRef P_CODERR As String, _
                                                ByRef P_MSGERR As String) As Integer


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CODIGODRA", DbType.String, 12, P_CodigoDRa, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 1, P_ESTADO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, P_USUARIO, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CODERR", DbType.String, ParameterDirection.Output), _
                                                        New DAABRequest.Parameter("P_MSGERR", DbType.String, 100, ParameterDirection.Output)}


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PCK_SICAR_OFF_SAP.MIG_ACTESTADODRA"
        objRequest.Parameters.AddRange(arrParam)
        AnularPago_CuadreCaja_cadena = objRequest.Factory.ExecuteNonQuery(objRequest)

        P_CODERR = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
        P_MSGERR = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
    End Function

    Public Function AnulaPagoSICAR(ByVal strNroPedido As String, ByVal strTipoDoc As String, _
                                   ByVal strSerie As String, ByVal strCorrelativo As String, _
                                   ByRef P_CODRPTA As String, ByRef P_MSJRPTA As String) As String

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRO_PEDIDO", DbType.String, strNroPedido, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_TIPODOC", DbType.String, strTipoDoc, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_SERIE", DbType.String, strSerie, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_CORRELATIVO", DbType.String, strCorrelativo, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_COD_RPTA", DbType.String, ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("P_MSJ_RPTA", DbType.String, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISCAJ.SISCAJD_PAGO"
            objRequest.Parameters.AddRange(arrParam)
            AnulaPagoSICAR = objRequest.Factory.ExecuteNonQuery(objRequest)

            P_CODRPTA = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
            P_MSJRPTA = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)

        Catch ex As Exception
            P_CODRPTA = "-1"
            P_MSJRPTA = ex.Message.ToString()
        End Try
    End Function


    Public Function Actualizar_Cuotas(ByVal P_CON_SISACT As String, ByVal K_RESULTADO As Integer)
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CON_SISACT", DbType.String, 50, P_CON_SISACT, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("K_RESULTADO", DbType.Int32, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_DRA_CVE_6.SISACSS_ACTUALIZAR_CUOTA"
        objRequest.Parameters.AddRange(arrParam)
        Actualizar_Cuotas = objRequest.Factory.ExecuteNonQuery(objRequest)

        K_RESULTADO = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
    End Function

    'Inicio SD854808 Relanzamiento Cambio de Plan JAZ		
    Public Function RegistrarRelanzamientoCambioPlan(ByVal sTelefono As String, _
                                       ByVal nPedidoSap As Int64, _
                                       ByVal nContrato As Int64, _
                                       ByVal sDocumentoCli As String, _
                                       ByVal sNombresCli As String, _
                                       ByVal sFechaMig As String, _
                                       ByVal sDescError As String, _
                                       ByVal sCicloFac As String, _
                                       ByVal sCanal As String, _
                                       ByVal sUsuarioCreacion As String, _
                                       ByRef nCodRespuesta As Integer, ByRef sMsgRespuesta As String) As Boolean
        Dim salida As Boolean = False

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TELEFONO", DbType.String, 30, sTelefono, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_PEDIDOSAP", DbType.String, nPedidoSap, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_CONTRATO", DbType.String, nContrato, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_DOCUMENTO_CLI", DbType.String, 20, sDocumentoCli, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_NOMBRE_CLI", DbType.String, 400, sNombresCli, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_FECHA_MIG", DbType.String, 20, sFechaMig, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_DESC_ERROR", DbType.String, 400, sDescError, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_CICLO_FAC", DbType.String, 2, sCicloFac, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_CANAL", DbType.String, 20, sCanal, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_USUARIO_CREACION", DbType.String, 50, sUsuarioCreacion, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_CODIGO_RESPUESTA", DbType.Int32, ParameterDirection.Output), _
                                                            New DAAB.DAABRequest.Parameter("P_MENSAJE_RESPUESTA", DbType.String, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISACT_PEND_CAMBIOPLAN.SISACTSI_PEND_CAMBIOPLAN"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            salida = True

            nCodRespuesta = Funciones.CheckInt(CType(objRequest.Parameters(10), IDataParameter).Value)
            sMsgRespuesta = Funciones.CheckStr(CType(objRequest.Parameters(11), IDataParameter).Value)

        Catch ex As Exception
            nCodRespuesta = "-1"
            sMsgRespuesta = ex.Message.ToString()
            salida = False
        End Try
        Return salida
    End Function
    'Fin SD854808 Relanzamiento Cambio de Plan JAZ		

	'INC000000824753 - 25/07/2017 nuevo
    Public Function Obtener_CO_SER(ByVal strNroSec As Integer, ByRef CodRespuesta As Integer, ByRef sMsgRespuesta As String) As ArrayList

        Dim filas As New ArrayList
        Dim dr As IDataReader = Nothing

        Try
	strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion) 
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
               New DAAB.DAABRequest.Parameter("NRO_SEC", DbType.Int32, strNroSec, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_COD_RESP", DbType.Int32, CodRespuesta, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("P_MSG_RESP", DbType.String, sMsgRespuesta, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("C_CURSOR", DbType.Object, ParameterDirection.Output)}

            Dim i As Integer
            For i = 0 To arrParam.Length - 1
                arrParam(i).Value = DBNull.Value
            Next

            arrParam(0).Value = strNroSec

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_ACUERDO_6.SP_OBTENER_CO_SER"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            While (dr.Read())
                Dim item As New Int32
                item = Funciones.CheckInt(dr("SERVI_CO_SER"))
                filas.Add(item)
            End While

            CodRespuesta = CType(objRequest.Parameters(1), IDataParameter).Value.ToString
            sMsgRespuesta = CType(objRequest.Parameters(2), IDataParameter).Value.ToString

            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()

        Catch ex As Exception

            CodRespuesta = -1
            sMsgRespuesta = ex.Message.ToString

        Finally

            dr.Close()

        End Try

        Return filas
    End Function
'PROY-26366 - FASE II - INICIO		
    Public Function ListarCanjePuntosDet(ByVal P_NRO_DOC_SAP_NC As String) As DataSet
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim dtResultado As DataSet
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRO_DOC_SAP_NC", DbType.String, P_NRO_DOC_SAP_NC, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("K_CUR_CLAROPUNTOS_DET", DbType.Object, ParameterDirection.Output)}
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_PROCESOS.MANTSS_SELECT_CANJE_PUNTOS"
            objRequest.Parameters.AddRange(arrParam)
            dtResultado = objRequest.Factory.ExecuteDataset(objRequest)
            Return dtResultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'PROY-26366 - FASE II - FIN

    'PROY 26210 BEGIN
    Public Function GetNombreTipoDevo(ByVal cod As String, ByVal tipo As String, ByVal estado As String, ByRef flError As Boolean) As String


        Dim dr As IDataReader = Nothing
        flError = True
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam As DAABRequest.Parameter() = { _
                        New DAABRequest.Parameter("p_tipo_operacion", DbType.[String], ParameterDirection.Input), _
                        New DAABRequest.Parameter("p_estado", DbType.[String], ParameterDirection.Input), _
                        New DAABRequest.Parameter("c_motivos", DbType.[Object], ParameterDirection.Output)}

        For i As Integer = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next
        arrParam(0).Value = tipo
        arrParam(1).Value = estado

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_CONSULTA_GENERAL.sp_con_motivo_venta"
        objRequest.Parameters.AddRange(arrParam)

        Dim strTipoDEv As String = ""

        Try
            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader

            While dr.Read()
                If dr("moti_codigo") = cod Then
                    strTipoDEv = dr("MOTI_DESCRIP").ToString
                End If
            End While
        Catch e As Exception
            strTipoDEv = e.Message
            flError = False
            'Throw e
        Finally
            If Not dr Is Nothing AndAlso dr.IsClosed = False Then dr.Close()
            objRequest.Parameters.Clear()

        End Try

        Return strTipoDEv
    End Function
    'PROY 26210 END

    'PROY-33313 RP INICIO
    Public Function FnActualizaTipoCVE(ByVal sNroPedido As String, ByRef sMsjRespuesta As String) As Boolean

        Try
            Dim sCodRespuesta As String
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("PI_NROPEDIDO", DbType.String, sNroPedido, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("PO_COD_RESP", DbType.String, ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("PO_MSG_RESP", DbType.String, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_DRA_CVE_6_33313.SISACSU_ACTUALIZAR_TIPO_CVE" 'PCK ORIGINAL: SISACT_PKG_DRA_CVE_6
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            sCodRespuesta = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            sMsjRespuesta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)

            If sCodRespuesta.Equals("0") Then
                FnActualizaTipoCVE = True
            End If

            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()

        Catch ex As Exception
            FnActualizaTipoCVE = False
            sMsjRespuesta = ex.Message.ToString()
        End Try

    End Function
    'PROY-33313 RP FIN
End Class
