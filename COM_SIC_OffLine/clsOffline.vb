Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB

Public Class clsOffline
     Dim strCadenaConexion As String
    Dim objSeg As New Seguridad_NET.clsSeguridad
    Dim pkgNameOffSAP As String = "PCK_SICAR_OFF_SAP"
    Dim strCadenaEsquema As String = ""
    'Dim pkgNameOffSAP As String = "PCK_SICAR_QA"

    '//-- GB
    Public Function Get_NuevoCodMedioPago(ByVal strCodAntiguoMedioPago As String) As DataSet
        'CAMBIADO POR FFS INICIO
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CODANTIGUOMEDIO", DbType.String, 10, strCodAntiguoMedioPago, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_GetNuevoMedoPago"
        objRequest.Parameters.AddRange(arrParam)

        Get_NuevoCodMedioPago = objRequest.Factory.ExecuteDataset(objRequest)
    End Function
    '//--


    Public Function Get_ConsultaVend(ByVal strNUsuario As String) As DataSet
        'CAMBIADO POR FFS INICIO
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_VENDEDOR", DbType.String, 10, strNUsuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURUSUARIO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_Mae_Datos_Vendedor"

        'objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Mae_Datos_Vendedor"
        objRequest.Parameters.AddRange(arrParam)
        'CAMBIADO POR FFS FIN
        Get_ConsultaVend = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function Get_ConsultaSAP() As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_PARAM_SAP", DbType.Int32, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_CON_SAP"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        Get_ConsultaSAP = CType(objRequest.Parameters(0), IDataParameter).Value
    End Function

    Public Function Get_ListaImpresoras(ByVal strOficina As String) As DataSet
        'CAMBIADO POR FFS INICIO
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_VKBUR", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURIMPRES", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        'objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Prt_Lista_Impresoras"
        objRequest.Command = pkgNameOffSAP & ".CONF_Prt_Lista_Impr"
        objRequest.Parameters.AddRange(arrParam)

        Get_ListaImpresoras = objRequest.Factory.ExecuteDataset(objRequest)
        'CAMBIADO POR FFS FIN
    End Function

    Public Function Get_ConsultaOficinaVenta(ByVal strOficina As String, ByVal strCanal As String) As DataSet
        'CAMBIADO POR FFS INICIO
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CANAL", DbType.String, 2, strCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CUROFICINAS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        'objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Mae_Oficina_Venta"
        objRequest.Command = pkgNameOffSAP & ".CONF_Mae_Oficina_Venta"
        objRequest.Parameters.AddRange(arrParam)

        Get_ConsultaOficinaVenta = objRequest.Factory.ExecuteDataset(objRequest)
        'CAMBIADO PO FFS FIN
    End Function

    Public Function Get_ConsultaViasPago(ByVal strOficina As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURVIAS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Mae_Vias_Pago"
        objRequest.Parameters.AddRange(arrParam)

        Get_ConsultaViasPago = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function Get_Tarjeta_Credito() As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("CURTARJ", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Mae_Tarjeta_Credito"
        objRequest.Parameters.AddRange(arrParam)

        Get_Tarjeta_Credito = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function Get_TipoCambio(ByVal strFecha As String) As Double
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TIPCAM", DbType.Double, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Con_Tipo_Cambio"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        Get_TipoCambio = CType(objRequest.Parameters(0), IDataParameter).Value
    End Function

    Public Function Set_LogRecaudacion(ByVal strLog As String, ByVal strDocum As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_RECAUD", DbType.String, 2000, strLog, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_DOCUM", DbType.String, 2000, strDocum, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_AUTOGEN", DbType.String, 10, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Trs_Log_Recaudacion"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Set_LogRecaudacion = CType(objRequest.Parameters(2), IDataParameter).Value

    End Function

    Public Function Set_RegistroDeuda(ByVal strDeuda As String, ByVal strRecibos As String, ByVal strPagos As String, ByRef strValorResultado As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_DEUDA", DbType.String, 2000, strDeuda, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_RECIBOS", DbType.String, 2000, strRecibos, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_PAGOS", DbType.String, 2000, strPagos, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_AUTOGEN", DbType.String, 20, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Trs_Reg_Deuda"
        objRequest.Parameters.AddRange(arrParam)

        strValorResultado = "0"
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Set_RegistroDeuda = "0" + "@" + CType(objRequest.Parameters(3), IDataParameter).Value + ";" + "TRANSACCION OK"
    End Function

    Public Function Set_EstadoRecaudacion(ByVal strTraceOriginal As String, ByVal strEstadoAnulado As String, ByVal strTmp As String, ByVal strTraceAnulacion As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim dsReturn As New DataSet
        Dim dtReturn As New DataTable
        Dim dcReturn As DataRow

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TRACEORG", DbType.String, 2000, strTraceOriginal, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_ESTAN", DbType.String, 2000, strEstadoAnulado, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_POSTMP", DbType.String, 2000, strTmp, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_TRACEANL", DbType.String, 2000, strTraceAnulacion, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Trs_Act_Est_Recaud"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        dtReturn.Columns.Add("TYPE", GetType(System.String))

        dcReturn = dtReturn.NewRow
        dcReturn.Item("TYPE") = "I"

        dtReturn.Rows.Add(dcReturn)
        dsReturn.Tables.Add(dtReturn)

        Set_EstadoRecaudacion = dsReturn

    End Function

    Public Function Set_RegSAP(ByVal intSAP As Integer) As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_DESTINO", DbType.Int32, intSAP, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_ACT_PARAM_DESTINO"
        objRequest.Parameters.AddRange(arrParam)

        Set_RegSAP = objRequest.Factory.ExecuteNonQuery(objRequest)
    End Function

    Public Function Get_RegistroDeuda(ByVal strNroTransaccion As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim dsDeuda As DataSet
        Dim dtDeuda As New DataTable
        Dim dtRecibos As New DataTable
        Dim dtPagos As New DataTable
        Dim dtLog As New DataTable
        Dim drFila As DataRow
        Dim dsReturn As New DataSet

        Dim i As Integer
        Dim j As Integer

        Dim arrDeuda As Object
        Dim arrRecibos As Object
        Dim arrRecibosLin As Object
        Dim arrPagos As Object
        Dim arrPagosLin As Object

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_AUTOGEN", DbType.String, 20, strNroTransaccion, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURRECAUD", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Con_Deuda"
        objRequest.Parameters.AddRange(arrParam)

        dsDeuda = objRequest.Factory.ExecuteDataset(objRequest)

        ' Construccion de la tabla cabecera
        dtDeuda.Columns.Add("NRO_TRANSACCION", GetType(System.String))
        dtDeuda.Columns.Add("FECHA_TRANSAC", GetType(System.String))
        dtDeuda.Columns.Add("RUC_DEUDOR", GetType(System.String))
        dtDeuda.Columns.Add("NOM_DEUDOR", GetType(System.String))
        dtDeuda.Columns.Add("IMPORTE_PAGO", GetType(System.String))
        dtDeuda.Columns.Add("COD_CAJERO", GetType(System.String))
        dtDeuda.Columns.Add("OFICINA_VENTA", GetType(System.String))
        dtDeuda.Columns.Add("TIPO_DOC_DEUDOR", GetType(System.String))
        dtDeuda.Columns.Add("NRO_DOC_DEUDOR", GetType(System.String))
        dtDeuda.Columns.Add("HORA_TRANSAC", GetType(System.String))
        dtDeuda.Columns.Add("NOM_CAJERO", GetType(System.String))
        dtDeuda.Columns.Add("NOM_OF_VENTA", GetType(System.String))


        ' Construccion de la tabla recibos
        dtRecibos.Columns.Add("NRO_TRANSACCION", GetType(System.String))
        dtRecibos.Columns.Add("POSICION", GetType(System.String))
        dtRecibos.Columns.Add("TIPO_DOC_RECAUD", GetType(System.String))
        dtRecibos.Columns.Add("NRO_DOC_RECAUD", GetType(System.String))
        dtRecibos.Columns.Add("MONEDA_DOCUM", GetType(System.String))
        dtRecibos.Columns.Add("IMPORTE_RECIBO", GetType(System.String))
        dtRecibos.Columns.Add("IMPORTE_PAGADO", GetType(System.String))
        dtRecibos.Columns.Add("NRO_COBRANZA", GetType(System.String))
        dtRecibos.Columns.Add("NRO_OPE_ACREE", GetType(System.String))
        dtRecibos.Columns.Add("FECHA_EMISION", GetType(System.String))
        dtRecibos.Columns.Add("FECHA_PAGO", GetType(System.String))
        dtRecibos.Columns.Add("NRO_TRACE_ANUL", GetType(System.String))
        dtRecibos.Columns.Add("NRO_TRACE_PAGO", GetType(System.String))
        dtRecibos.Columns.Add("DESC_SERVICIO", GetType(System.String))
        dtRecibos.Columns.Add("FECHA_HORA", GetType(System.String))
        dtRecibos.Columns.Add("SERVICIO", GetType(System.String))


        ' Construccion de la tabla pagos
        dtPagos.Columns.Add("NRO_TRANSACCION", GetType(System.String))
        dtPagos.Columns.Add("POSICION", GetType(System.String))
        dtPagos.Columns.Add("VIA_PAGO", GetType(System.String))
        dtPagos.Columns.Add("IMPORTE_PAGADO", GetType(System.String))
        dtPagos.Columns.Add("NRO_CHEQUE", GetType(System.String))
        dtPagos.Columns.Add("BELNR", GetType(System.String))
        dtPagos.Columns.Add("DESC_VIA_PAGO", GetType(System.String))


        ' Construccion de la tabla de respuesta
        dtLog.Columns.Add("TYPE", GetType(System.String))
        dtLog.Columns.Add("MESSAGE", GetType(System.String))


        For i = 0 To dsDeuda.Tables(0).Rows.Count - 1
            arrDeuda = Split(dsDeuda.Tables(0).Rows(i).Item("RECAU_DEUDA"), ";")
            arrRecibos = Split(dsDeuda.Tables(0).Rows(i).Item("RECAU_RECIBOS"), "|")
            arrPagos = Split(dsDeuda.Tables(0).Rows(i).Item("RECAU_PAGOS"), "|")

            'Llenado de Cabecera
            drFila = dtDeuda.NewRow

            drFila("NRO_TRANSACCION") = strNroTransaccion
            drFila("FECHA_TRANSAC") = Split(arrRecibos(0), ";")(10)
            drFila("RUC_DEUDOR") = arrDeuda(2)
            drFila("NOM_DEUDOR") = arrDeuda(1)
            drFila("IMPORTE_PAGO") = arrDeuda(6)
            drFila("COD_CAJERO") = arrDeuda(11)
            drFila("OFICINA_VENTA") = arrDeuda(3)
            drFila("TIPO_DOC_DEUDOR") = arrDeuda(14)
            drFila("NRO_DOC_DEUDOR") = arrDeuda(15)
            drFila("HORA_TRANSAC") = "      "
            drFila("NOM_CAJERO") = ""
            drFila("NOM_OF_VENTA") = ""


            dtDeuda.Rows.Add(drFila)

            'Llenado de Recibos
            For j = 0 To UBound(arrRecibos) - 1
                arrRecibosLin = Split(arrRecibos(j), ";")
                drFila = dtRecibos.NewRow


                drFila("NRO_TRANSACCION") = strNroTransaccion
                drFila("POSICION") = j
                drFila("NRO_COBRANZA") = arrRecibosLin(7)
                drFila("NRO_OPE_ACREE") = arrRecibosLin(8)
                drFila("SERVICIO") = arrRecibosLin(15)
                drFila("FECHA_HORA") = arrRecibosLin(14)
                drFila("NRO_TRACE_PAGO") = arrRecibosLin(12)

                drFila("TIPO_DOC_RECAUD") = arrRecibosLin(2)
                drFila("NRO_DOC_RECAUD") = arrRecibosLin(3)
                drFila("DESC_SERVICIO") = arrRecibosLin(13)
                drFila("FECHA_EMISION") = arrRecibosLin(9)
                drFila("MONEDA_DOCUM") = arrRecibosLin(4)
                drFila("IMPORTE_RECIBO") = arrRecibosLin(5)
                drFila("IMPORTE_PAGADO") = arrRecibosLin(6)



                dtRecibos.Rows.Add(drFila)
                drFila = Nothing

            Next
            'Llenado de Pagos

            For j = 0 To UBound(arrPagos) - 1
                arrPagosLin = Split(arrPagos(j), ";")
                drFila = dtPagos.NewRow

                drFila("DESC_VIA_PAGO") = arrPagosLin(2)
                drFila("VIA_PAGO") = arrPagosLin(2)
                drFila("NRO_CHEQUE") = arrPagosLin(4)
                drFila("IMPORTE_PAGADO") = arrPagosLin(3)

                dtPagos.Rows.Add(drFila)
                drFila = Nothing

            Next
        Next

        drFila = dtLog.NewRow
        drFila("TYPE") = "I"
        drFila("MESSAGE") = "OK"
        dtLog.Rows.Add(drFila)
        drFila = Nothing

        dsReturn.Tables.Add(dtDeuda)
        dsReturn.Tables.Add(dtRecibos)
        dsReturn.Tables.Add(dtPagos)
        dsReturn.Tables.Add(dtLog)

        Get_RegistroDeuda = dsReturn

    End Function


    Public Function Get_PoolRecaudacion(ByVal strFechaTransaccion As String, ByVal strOficinaVenta As String, _
                                        ByVal strNroTransaccion As String, ByVal strRucDeudor As String, _
                                        ByVal strNroTelefono As String, ByVal strEstadoTransaccion As String) As DataSet


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

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
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, strOficinaVenta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, strFechaTransaccion, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURLOG", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Con_Pool_Recaudacion"
        objRequest.Parameters.AddRange(arrParam)

        dsPool = objRequest.Factory.ExecuteDataset(objRequest)

        ' Construccion de la tabla 
        dtPool.Columns.Add("NRO_TRANSACCION", GetType(System.String))
        dtPool.Columns.Add("FECHA_TRANSAC", GetType(System.String))
        dtPool.Columns.Add("RUC_DEUDOR", GetType(System.String))
        dtPool.Columns.Add("NOM_DEUDOR", GetType(System.String))
        dtPool.Columns.Add("MON_PAGO", GetType(System.String))
        dtPool.Columns.Add("IMPORTE_PAGO", GetType(System.String))


        For i = 0 To dsPool.Tables(0).Rows.Count - 1
            drFila = dtPool.NewRow

            drFila("NRO_TRANSACCION") = dsPool.Tables(0).Rows(i).Item("RECAU_AUTOGEN") 'modificado por JCR - cambio de cero por i
            drFila("FECHA_TRANSAC") = Split(dsPool.Tables(0).Rows(i).Item("RECAU_RECIBOS"), ";")(10)
            drFila("RUC_DEUDOR") = Split(dsPool.Tables(0).Rows(i).Item("RECAU_DEUDA"), ";")(2)
            drFila("NOM_DEUDOR") = Split(dsPool.Tables(0).Rows(i).Item("RECAU_DEUDA"), ";")(1)
            drFila("MON_PAGO") = Split(dsPool.Tables(0).Rows(i).Item("RECAU_DEUDA"), ";")(5)
            drFila("IMPORTE_PAGO") = Split(dsPool.Tables(0).Rows(i).Item("RECAU_DEUDA"), ";")(6)

            dtPool.Rows.Add(drFila)
        Next

        dsReturn.Tables.Add(dtPool)

        Get_PoolRecaudacion = dsReturn

    End Function

    Public Function Get_Con_Recaudacion() As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("CURLOG", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Con_Recaudacion"
        objRequest.Parameters.AddRange(arrParam)

        Get_Con_Recaudacion = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function Get_Con_LogRecaudacion() As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("CURLOG", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Con_Log_Recaudacion"
        objRequest.Parameters.AddRange(arrParam)

        Get_Con_LogRecaudacion = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function Get_Con_EstRecaudacion() As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("CURLOG", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Con_Est_Recaudacion"
        objRequest.Parameters.AddRange(arrParam)

        Get_Con_EstRecaudacion = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function Get_EliminaOffline() As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_Elimina_OffLine"

        Get_EliminaOffline = objRequest.Factory.ExecuteNonQuery(objRequest)
    End Function



#Region "GENERICOS"


    'FFS Obtiene las  tarjetas de Credito de la tabla TI_TARJETA_CREDITO en pagos de recibos
    Public Function Obtener_Tarjeta_Credito() As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("C_TARJETA", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_DATOS_TARJETA_CREDITO"
        objRequest.Parameters.AddRange(arrParam)

        Obtener_Tarjeta_Credito = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    'FFS Obtiene las consultas vias de Pago 
    Public Function Obtener_ConsultaViasPago(ByVal strOficina As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("C_VIA_PAGO", DbType.Object, ParameterDirection.Output)}


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_DATOS_VIAS_PAGO"
        objRequest.Parameters.AddRange(arrParam)

        Obtener_ConsultaViasPago = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    'FFS Obtiene el Tipo de Cambio 
    Public Function Obtener_TipoCambio(ByVal strFecha As String) As Double
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TIPCAM", DbType.Double, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_Con_Tipo_Cambio"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        Obtener_TipoCambio = CType(objRequest.Parameters(0), IDataParameter).Value
    End Function

    'CCC Obtiene el nuevo codigo de oficina SAP - teamsoft
    Public Function Obtener_NewCodeOficinaVenta(ByVal strOficina As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("k_oficc_codigooficina", DbType.String, 4, strOficina, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("p_result", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("k_nrolog", DbType.Int32, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("k_deslog", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "sisact_pkg_cons_maestra_sap_6.ssapss_parametrooficina"
        objRequest.Parameters.AddRange(arrParam)

        Obtener_NewCodeOficinaVenta = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    'CCC Obtiene todas las vias de pago.
    Public Function Get_ViasPagos() As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("C_VIA_PAGO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Mig_ViasPagos"
        objRequest.Parameters.AddRange(arrParam)

        Get_ViasPagos = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

#End Region

#Region "RECAUDACION DAC"

    Public Function Get_ConsultaDeudaDAC(ByVal strCliente As String, ByRef strName As String, ByRef dblMonto As Double, ByRef strDoc As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_KUNNR", DbType.String, 10, strCliente, ParameterDirection.Input), _
                                                    New DAAB.DAABRequest.Parameter("P_NAME1", DbType.String, 50, ParameterDirection.Output), _
                                                    New DAAB.DAABRequest.Parameter("P_MONTO", DbType.String, 20, ParameterDirection.Output), _
                                                    New DAAB.DAABRequest.Parameter("P_STCD1", DbType.String, 15, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_ConDeudaDAC"
        objRequest.Parameters.AddRange(arrParam)

        Get_ConsultaDeudaDAC = objRequest.Factory.ExecuteDataset(objRequest)
        Dim Monto As String

        strName = IIf(IsDBNull(CType(objRequest.Parameters(1), IDataParameter).Value), "", CType(objRequest.Parameters(1), IDataParameter).Value)
        'Monto = CType(objRequest.Parameters(2), IDataParameter).Value
        'dblMonto = IIf(IsDBNull(Monto), 0, Monto)
        Monto = IIf(IsDBNull(CType(objRequest.Parameters(2), IDataParameter).Value), "0", CType(objRequest.Parameters(2), IDataParameter).Value)
        strDoc = IIf(IsDBNull(CType(objRequest.Parameters(3), IDataParameter).Value), "", CType(objRequest.Parameters(3), IDataParameter).Value)


        dblMonto = CDbl(Monto)

        'objRequest.Factory.ExecuteNonQuery(objRequest)

        Return Get_ConsultaDeudaDAC

    End Function

    ''INICIO :: AGREGADO POR CCC-TS - teamsoft
    Public Function GetPoolRecaudacionDAC(ByVal strOficinaVenta As String, _
                                     ByVal strUsuario As String, ByVal strFecha As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim dsPool As DataSet
        Dim dtPool As New DataTable

        Dim drFila As DataRow
        Dim dsReturn As New DataSet
        Dim i As Integer

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = _
                         {New DAAB.DAABRequest.Parameter("P_Oficina", DbType.String, 4, strOficinaVenta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_Usuario", DbType.String, 10, strUsuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_Fecha", DbType.String, 10, strFecha, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("C_PAGOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Mig_Listar_Recaudaciones_DAC"
        objRequest.Parameters.AddRange(arrParam)

        dsPool = objRequest.Factory.ExecuteDataset(objRequest)

        ' Construccion de la tabla 
        dtPool.Columns.Add("id_t_nro_recaudacion_dac", GetType(System.String))
        dtPool.Columns.Add("nroat", GetType(System.String))
        dtPool.Columns.Add("COD_CLIENTE", GetType(System.String))
        dtPool.Columns.Add("CLIENTE", GetType(System.String))
        dtPool.Columns.Add("monto", GetType(System.String))
        dtPool.Columns.Add("uname", GetType(System.String))
        dtPool.Columns.Add("rectp", GetType(System.String))
        dtPool.Columns.Add("oficina", GetType(System.String))
        dtPool.Columns.Add("estado", GetType(System.String))
        dtPool.Columns.Add("usuario_registro", GetType(System.String))
        dtPool.Columns.Add("fecha_registro", GetType(System.String))
        dtPool.Columns.Add("usuario_modifica", GetType(System.String))
        dtPool.Columns.Add("fecha_modifica", GetType(System.String))


        For i = 0 To dsPool.Tables(0).Rows.Count - 1
            drFila = dtPool.NewRow

            drFila("id_t_nro_recaudacion_dac") = dsPool.Tables(0).Rows(i).Item("id_t_nro_recaudacion_dac")
            drFila("nroat") = dsPool.Tables(0).Rows(i).Item("nroat")
            drFila("COD_CLIENTE") = dsPool.Tables(0).Rows(i).Item("COD_CLIENTE")
            drFila("CLIENTE") = dsPool.Tables(0).Rows(i).Item("CLIENTE")
            drFila("monto") = dsPool.Tables(0).Rows(i).Item("monto")
            drFila("uname") = dsPool.Tables(0).Rows(i).Item("uname")
            drFila("rectp") = dsPool.Tables(0).Rows(i).Item("rectp")
            drFila("oficina") = dsPool.Tables(0).Rows(i).Item("oficina")
            drFila("estado") = dsPool.Tables(0).Rows(i).Item("estado_pago")
            drFila("usuario_registro") = dsPool.Tables(0).Rows(i).Item("usuario")
            drFila("fecha_registro") = dsPool.Tables(0).Rows(i).Item("fecha_registro")
            drFila("usuario_modifica") = dsPool.Tables(0).Rows(i).Item("usuario_modifica")
            drFila("fecha_modifica") = dsPool.Tables(0).Rows(i).Item("fecha_modifica")

            dtPool.Rows.Add(drFila)
        Next

        dsReturn.Tables.Add(dtPool)

        GetPoolRecaudacionDAC = dsReturn

    End Function

    Public Function GetRecaudacionDACDetalle(ByVal strNroAt As String, ByVal IdDac As Int64) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim dsDacDet As DataSet
        Dim dtDacDet As New DataTable

        Dim drFila As DataRow
        Dim dsReturn As New DataSet
        Dim i As Integer

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = _
                         {New DAAB.DAABRequest.Parameter("P_NROAT", DbType.String, 12, strNroAt, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_ID", DbType.Int64, IdDac, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("C_PAGOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Mig_Listar_Recaud_DAC_Det"
        objRequest.Parameters.AddRange(arrParam)

        dsDacDet = objRequest.Factory.ExecuteDataset(objRequest)

        ' Construccion de la tabla 
        dtDacDet.Columns.Add("COD_CLIENTE", GetType(System.String))
        dtDacDet.Columns.Add("VIA_PAGO", GetType(System.String))
        dtDacDet.Columns.Add("MONTO", GetType(System.String))
        dtDacDet.Columns.Add("FECHA", GetType(System.String))
        dtDacDet.Columns.Add("HORA", GetType(System.String))
        dtDacDet.Columns.Add("CODIGO_BANCO", GetType(System.String))
        dtDacDet.Columns.Add("BANCO", GetType(System.String))
        dtDacDet.Columns.Add("UNAME", GetType(System.String))
        dtDacDet.Columns.Add("NRO_TARJETA_CHEQUE", GetType(System.String))
        dtDacDet.Columns.Add("CLIENTE", GetType(System.String))
        dtDacDet.Columns.Add("VTEXT", GetType(System.String))
        'PROY-26366 - FASE I - INICIO
        dtDacDet.Columns.Add("USUARIO", GetType(System.String))
        dtDacDet.Columns.Add("NOMBRE", GetType(System.String))
        dtDacDet.Columns.Add("VKBUR", GetType(System.String))
        dtDacDet.Columns.Add("BEZEI", GetType(System.String))
        'PROY-26366 - FASE I - FIN
        For i = 0 To dsDacDet.Tables(0).Rows.Count - 1
            drFila = dtDacDet.NewRow

            drFila("COD_CLIENTE") = dsDacDet.Tables(0).Rows(i).Item("COD_CLIENTE")
            drFila("VIA_PAGO") = dsDacDet.Tables(0).Rows(i).Item("VIA_PAGO")
            drFila("MONTO") = dsDacDet.Tables(0).Rows(i).Item("MONTO")
            drFila("FECHA") = dsDacDet.Tables(0).Rows(i).Item("FECHA")
            drFila("HORA") = dsDacDet.Tables(0).Rows(i).Item("HORA")
            drFila("CODIGO_BANCO") = dsDacDet.Tables(0).Rows(i).Item("CODIGO_BANCO")
            drFila("BANCO") = dsDacDet.Tables(0).Rows(i).Item("BANCO")
            drFila("UNAME") = dsDacDet.Tables(0).Rows(i).Item("UNAME")
            drFila("NRO_TARJETA_CHEQUE") = dsDacDet.Tables(0).Rows(i).Item("NRO_TARJETA_CHEQUE")
            drFila("CLIENTE") = dsDacDet.Tables(0).Rows(i).Item("CLIENTE")
            drFila("VTEXT") = dsDacDet.Tables(0).Rows(i).Item("VTEXT")
            'PROY-26366 - FASE I - INICIO
            drFila("USUARIO") = dsDacDet.Tables(0).Rows(i).Item("USUARIO")
            drFila("NOMBRE") = dsDacDet.Tables(0).Rows(i).Item("NOMBRE")
            drFila("VKBUR") = dsDacDet.Tables(0).Rows(i).Item("VKBUR")
            drFila("BEZEI") = dsDacDet.Tables(0).Rows(i).Item("BEZEI")
            'PROY-26366 - FASE I - FIN
            dtDacDet.Rows.Add(drFila)
        Next

        dsReturn.Tables.Add(dtDacDet)

        GetRecaudacionDACDetalle = dsReturn

    End Function

    Public Sub RegistrarDRAPoolDocPagados(ByVal K_PEDIN_NROPEDIDO As Int32, _
                                            ByVal K_PEDIV_OFICINAPAGO As String, _
                                            ByRef K_NROLOG As String, _
                                            ByRef K_DESLOG As String)

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

            strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
            If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
                strCadenaEsquema = strCadenaEsquema & "."
            Else
                strCadenaEsquema = String.Empty
            End If

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("K_PEDIV_OFICINAPAGO", DbType.String, K_PEDIV_OFICINAPAGO, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_MSSAP.SSAPSU_OFICINAPAGO"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)

            K_NROLOG = CType(objRequest.Parameters(2), IDataParameter).Value
            K_DESLOG = CType(objRequest.Parameters(3), IDataParameter).Value

            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Public Function GetCodigoPedidoMsSap(ByVal codigoDRA As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_CODIGODRA", DbType.String, 15, codigoDRA, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_NROPEDIDO", DbType.String, 20, 0, ParameterDirection.Output), _
                                        New DAAB.DAABRequest.Parameter("P_CODIGO", DbType.String, 2, 0, ParameterDirection.Output), _
                                        New DAAB.DAABRequest.Parameter("P_MENSAJE", DbType.String, 1000, 0, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetObtenerNroPedido"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteDataset(objRequest)
        Dim strCodigo As String = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
        Dim strDescripcion As String = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
        Return Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
    End Function

    Public Function RegistrarPago(ByVal K_PEDIN_NROPEDIDO As Int64, _
                                       ByVal K_PAGOD_FECHACONTA As Object, _
                                       ByVal K_PAGOC_ESTADO As String, _
                                       ByVal K_PAGOV_NOMBRECAJA As String, _
                                       ByVal K_PAGON_NROCAJA As Double, _
                                       ByVal K_PAGOC_MONEDA As String, _
                                       ByVal K_PAGOV_USUARIOCREA As String, _
                                       ByVal K_PAGOD_FECHACREA As Object, _
                                       ByVal K_PAGOV_USUARIOMODI As String, _
                                       ByVal K_PAGOD_FECHAMODI As Object, _
                                       ByRef K_NROLOG As String, _
                                       ByRef K_DESLOG As String, _
                                       ByRef K_PAGON_IDPAGO As Int64, _
                                       ByRef K_PAGOC_CORRELATIVO As String) As String

        Dim strErr As String = ""
        Dim dtSet As New DataTable
        Dim procesaPagos As Int32
        K_NROLOG = ""
        K_DESLOG = ""
        K_PAGON_IDPAGO = 0
        K_PAGOC_CORRELATIVO = ""

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PAGOD_FECHACONTA", DbType.Date, K_PAGOD_FECHACONTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PAGOC_ESTADO", DbType.String, K_PAGOC_ESTADO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PAGOV_NOMBRECAJA", DbType.String, K_PAGOV_NOMBRECAJA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PAGON_NROCAJA", DbType.Double, K_PAGON_NROCAJA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PAGOC_MONEDA", DbType.String, K_PAGOC_MONEDA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PAGOV_USUARIOCREA", DbType.String, K_PAGOV_USUARIOCREA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PAGOD_FECHACREA", DbType.Date, K_PAGOD_FECHACREA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PAGOV_USUARIOMODI", DbType.String, K_PAGOV_USUARIOMODI, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PAGOD_FECHAMODI", DbType.Date, K_PAGOD_FECHAMODI, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_PAGON_IDPAGO", DbType.Int64, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_PAGOC_CORRELATIVO", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_PAGO.SSAPSI_PAGO2"
            objRequest.Parameters.AddRange(arrParam)
            procesaPagos = objRequest.Factory.ExecuteNonQuery(objRequest)

            K_NROLOG = Convert.ToString(CType(objRequest.Parameters(10), IDataParameter).Value)
            K_DESLOG = Convert.ToString(CType(objRequest.Parameters(11), IDataParameter).Value)
            K_PAGON_IDPAGO = Convert.ToInt64(CType(objRequest.Parameters(12), IDataParameter).Value)
            K_PAGOC_CORRELATIVO = Convert.ToString(CType(objRequest.Parameters(13), IDataParameter).Value)

            objRequest = Nothing
            Return strErr
        Catch ex As Exception
            strErr = ex.Message.ToString()
            Return strErr
        End Try
    End Function

    Public Function RegistrarDetallePago(ByVal K_PAGON_IDPAGO As Int64, _
                                            ByVal K_DEPAV_FORMAPAGO As String, _
                                            ByVal K_DEPAV_DESCPAGO As String, _
                                            ByVal K_DEPAC_MONEDA As String, _
                                            ByVal K_DEPAN_IMPORTE As Double, _
                                            ByVal K_DEPAV_USUARIOCREA As String, _
                                            ByVal K_DEPAD_FECHACREA As Object, _
                                            ByVal K_DEPAV_USUARIOMODI As String, _
                                            ByVal K_DEPAD_FECHAMODI As Object, _
                                            ByVal K_DEPAV_REFERENCIANC As String, _
                                            ByVal K_DEPAN_PEDIDOREFERENCIA As Int64, _
                                            ByRef K_NROLOG As String, _
                                            ByRef K_DESLOG As String) As String
        Dim strErr As String = ""
        Dim dtSet As New DataSet
        Dim procesaDetallePagos As Int32

        K_NROLOG = ""
        K_DESLOG = ""

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PAGON_IDPAGO", DbType.Int64, K_PAGON_IDPAGO, ParameterDirection.Input), _
                                                                                        New DAAB.DAABRequest.Parameter("K_DEPAV_FORMAPAGO", DbType.String, 4, K_DEPAV_FORMAPAGO, ParameterDirection.Input), _
                                                                                        New DAAB.DAABRequest.Parameter("K_DEPAV_DESCPAGO", DbType.String, 40, K_DEPAV_DESCPAGO, ParameterDirection.Input), _
                                                                                        New DAAB.DAABRequest.Parameter("K_DEPAC_MONEDA", DbType.String, 3, K_DEPAC_MONEDA, ParameterDirection.Input), _
                                                                                        New DAAB.DAABRequest.Parameter("K_DEPAN_IMPORTE", DbType.Double, K_DEPAN_IMPORTE, ParameterDirection.Input), _
                                                                                        New DAAB.DAABRequest.Parameter("K_DEPAV_USUARIOCREA", DbType.String, 10, K_DEPAV_USUARIOCREA, ParameterDirection.Input), _
                                                                                        New DAAB.DAABRequest.Parameter("K_DEPAD_FECHACREA", DbType.Date, K_DEPAD_FECHACREA, ParameterDirection.Input), _
                                                                                        New DAAB.DAABRequest.Parameter("K_DEPAV_USUARIOMODI", DbType.String, 10, K_DEPAV_USUARIOMODI, ParameterDirection.Input), _
                                                                                        New DAAB.DAABRequest.Parameter("K_DEPAD_FECHAMODI", DbType.Date, K_DEPAD_FECHAMODI, ParameterDirection.Input), _
                                                                                        New DAAB.DAABRequest.Parameter("K_DEPAV_REFERENCIANC", DbType.String, 20, K_DEPAV_REFERENCIANC, ParameterDirection.Input), _
                                                                                        New DAAB.DAABRequest.Parameter("K_DEPAN_PEDIDOREFERENCIA", DbType.Int64, K_DEPAN_PEDIDOREFERENCIA, ParameterDirection.Input), _
                                                                                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, ParameterDirection.Output), _
                                                                                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_PAGO.SSAPSI_DETALLEPAGO"
            objRequest.Parameters.AddRange(arrParam)

            procesaDetallePagos = objRequest.Factory.ExecuteNonQuery(objRequest)

            K_NROLOG = Convert.ToString(CType(objRequest.Parameters(11), IDataParameter).Value)
            K_DESLOG = Convert.ToString(CType(objRequest.Parameters(12), IDataParameter).Value)

            objRequest = Nothing
            Return strErr
        Catch ex As Exception
            strErr = ex.Message.ToString()
            Return strErr
        End Try
    End Function


    Public Function GetNombreCajaAsignada(ByVal oficina As String, ByVal fecha As String, ByVal usuario As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, usuario, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetNombreCajaAsignada"
        objRequest.Parameters.AddRange(arrParam)
        GetNombreCajaAsignada = objRequest.Factory.ExecuteDataset(objRequest)
    End Function


    Public Function GetRecauDAC(ByVal nroDocIni As String, ByVal nroDocFin As String, _
                                ByVal oficinaVenta As String, ByVal fechaIni As String, _
                                ByVal fechaFin As String, ByVal montoPago1 As String, _
                                ByVal montoPago2 As String, ByVal estado As String, _
                                ByVal cajero As String, _
                                ByVal codClienteIni As String, ByVal codClienteFin As String, _
                                ByVal cntRegistros As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRODOC_1", DbType.String, 12, nroDocIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NRODOC_2", DbType.String, 12, nroDocFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficinaVenta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_INI", DbType.String, 10, fechaIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_FIN", DbType.String, 10, fechaFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MTOPAG_1", DbType.String, 15, montoPago1, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MTOPAG_2", DbType.String, 15, montoPago2, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 1, estado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CODCLI_1", DbType.String, 12, codClienteIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CODCLI_2", DbType.String, 12, codClienteFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CNTREG", DbType.String, 6, cntRegistros, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_GetRecauDAC"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function


    Public Function ActualizarEstadoDAC(ByVal IdDac As Int32, ByVal estado As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_ID", DbType.Int32, IdDac, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 1, estado, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MsgErr", DbType.String, 500, String.Empty, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_ActualizarEstadoDAC"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(2), IDataParameter).Value.ToString
    End Function

    Public Function GetNroPagoDra() As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NRO_PAG_DRA", DbType.String, 20, String.Empty, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetNroPagoDRA"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(0), IDataParameter).Value.ToString
    End Function

    Public Function AsignarPagoAcuerdosXDocSap(ByVal p_nro_documento As String, ByVal p_nro_referencia As String, _
                                               ByVal p_monto_pago As Double, ByVal p_usuario As String, _
                                               ByRef p_msg_resp As String) As String
        Dim p_cod_resp As String = ""
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                    New DAAB.DAABRequest.Parameter("p_nro_documento", DbType.String, 20, p_nro_documento, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_nro_referencia", DbType.String, 50, p_nro_referencia, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_monto_pago", DbType.Double, p_monto_pago, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_usuario", DbType.String, 20, p_usuario, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_cod_resp", DbType.String, ParameterDirection.Output), _
                                    New DAAB.DAABRequest.Parameter("p_msg_resp", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "sisact_pkg_acuerdo_6.sp_asignar_pago"
        objRequest.Parameters.AddRange(arrParam)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            p_cod_resp = IIf(IsDBNull(CType(objRequest.Parameters(4), IDataParameter).Value), "", CType(objRequest.Parameters(4), IDataParameter).Value)
            p_msg_resp = IIf(IsDBNull(CType(objRequest.Parameters(5), IDataParameter).Value), "", CType(objRequest.Parameters(5), IDataParameter).Value)
        Catch ex As Exception
            Throw ex
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return p_cod_resp
    End Function

    Public Function ObtenerDatosTransaccionales(ByVal P_DRAV_GENERADO As String, _
                                                  ByRef P_DRAV_NRO_ASOCIADO As String, _
                                                  ByRef P_DRAV_CODAPLIC As String, _
                                                  ByRef P_DRAV_USUARIOAPLIC As String, _
                                                  ByRef P_DRAV_DESC_TRS As String, _
                                                  ByRef P_DRAV_CODPAGO As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

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

            ObtenerDatosTransaccionales = True
        Catch ex As Exception
            ObtenerDatosTransaccionales = False
        End Try
    End Function

    Public Function AnularExtornarPagoDRA(ByVal PI_COD_APLI As String, _
                                        ByVal PI_USER_APLI As String, _
                                        ByVal PI_DRAV_NRO_ASOCIADO_ST As String, _
                                        ByVal PI_DRAV_DESC_TRS As String, _
                                        ByRef PO_NUM_OPE_PAGO_ANUL_EXT As Int64, _
                                        ByRef PO_COD_RPTA As Int64, _
                                        ByRef PO_MSG_RPTA As String)

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

            PO_NUM_OPE_PAGO_ANUL_EXT = Convert.ToInt64(CType(objRequest.Parameters(4), IDataParameter).Value)
            PO_COD_RPTA = Convert.ToInt64(CType(objRequest.Parameters(5), IDataParameter).Value)
            PO_MSG_RPTA = Convert.ToString(CType(objRequest.Parameters(6), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''FIN :: AGREGADO POR CCC-TS
#End Region

#Region "Impresoras"
    Public Function Set_ActUsoImpresora(ByVal strOficina As String, ByVal strUsuario As String, ByVal strCaja As String, ByVal strMensaje As String)
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_VKBUR", DbType.String, 4, strOficina, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, strUsuario, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_CAJA", DbType.String, 5, strCaja, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_MENSAJE", DbType.String, 400, strMensaje, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_Trs_SetImpresora_Usuario"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)
        'Set_ActUsoImpresora = CType(objRequest.Parameters(0), IDataParameter).Value
    End Function

    Public Function ObtenerCaja(ByVal cajero As String, ByVal tienda As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_Oficina", DbType.String, 4, tienda, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_PAGO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Conf_Obtenercaja"
        objRequest.Parameters.AddRange(arrParam)
        Dim dsReturn As DataSet = objRequest.Factory.ExecuteDataset(objRequest)
        Dim tbUsuarioImpresora As DataTable = dsReturn.Tables(0)
        Dim caja As String = String.Empty
        If tbUsuarioImpresora.Rows.Count > 0 Then
            caja = Convert.ToString(tbUsuarioImpresora.Rows(0)(0))
        End If
        Return caja
    End Function


#End Region

#Region "FIJO PAGINAS"

    Public Function SetRegistroDeuda(ByVal tramaDeudaSAP As String, ByVal tramaRecibosSAP As String, ByVal tramaPagosSAP As String, ByRef resultado As String) As String
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim returnValue$ = String.Empty

        Dim codigoRecaudacion$ = String.Empty
        Dim ID_T_TRS_REG_DEUDA As Integer
        Dim registraSap As String = ConfigurationSettings.AppSettings("REGISTRA_SAP")

        Dim objRequest2 As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrPrmsCabecera() As DAAB.DAABRequest.Parameter = { _
                                                                New DAAB.DAABRequest.Parameter("P_ID_T_TRS_REG_DEUDA", DbType.Int32, 20, ParameterDirection.Output), _
                                                                New DAAB.DAABRequest.Parameter("P_RECAUDACIONID", DbType.String, 15, ParameterDirection.Output), _
                                                                New DAAB.DAABRequest.Parameter("P_REGISTRASAP", DbType.String, 1, registraSap, ParameterDirection.Input) _
                                                              }
        objRequest2.CommandType = CommandType.StoredProcedure
        objRequest2.Command = pkgNameOffSAP & ".CONF_Trs_DeudaCabecera"
        objRequest2.Parameters.AddRange(arrPrmsCabecera)
        resultado = "0"
        objRequest2.Factory.ExecuteNonQuery(objRequest2)
        ID_T_TRS_REG_DEUDA = Convert.ToInt32(CType(objRequest2.Parameters(0), IDataParameter).Value)
        codigoRecaudacion = CType(objRequest2.Parameters(1), IDataParameter).Value.ToString

        Dim importeTotal As Double = 0
        Dim importeTotalRecibo As Double = 0
        Dim tipoDocumento As String = ""
        Dim facDolares As String = ConfigurationSettings.AppSettings("constCodigoServFactDolares")
        Dim constFPCodigoEfectivo As String = ConfigurationSettings.AppSettings("constFPCodigoEfectivo")
        Dim pr As New PagoRecaudacion(tramaPagosSAP.Split(CChar("|"))(0).Split(CChar(";")))
        Dim medioPago As String = pr.viaPago

        If tramaRecibosSAP.Length > 0 Then
            Dim tramaRecibos() As String = tramaRecibosSAP.Split(CChar("|"))
            Dim index%
            For index = 0 To tramaRecibos.Length - 1
                If tramaRecibos(index).Length > 0 Then
                    Dim objRecibo As New ReciboRecaudacion(tramaRecibos(index).Split(CChar(";")))
                    Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
                    With objRecibo
                        importeTotal += .importePagado
                        importeTotalRecibo += .importeRecibo
                        tipoDocumento = .tipoDocumento
                    End With
                End If
            Next
        End If
        If (tipoDocumento = facDolares) And (constFPCodigoEfectivo.StartsWith(medioPago)) Then
            importeTotal = redondeoSicar(importeTotal)
        End If

        'New DAAB.DAABRequest.Parameter("P_ImportePago", .ImportePago), _
        If tramaDeudaSAP.Length > 0 Then
            Dim objDeuda As New DeudaRecaudacion(tramaDeudaSAP.Split(CChar(";")))
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            With objDeuda
                Dim arrParameters() As DAAB.DAABRequest.Parameter = { _
                    New DAAB.DAABRequest.Parameter("P_NumeroTransaccion", .NumeroTransaccion), _
                    New DAAB.DAABRequest.Parameter("P_NombreDeudor", .NombreDeudor), _
                    New DAAB.DAABRequest.Parameter("P_RucDeudor", .RucDeudor), _
                    New DAAB.DAABRequest.Parameter("P_OficinaVenta", .OficinaVenta), _
                    New DAAB.DAABRequest.Parameter("P_NombreOficinaVenta", .NombreOficinaVenta), _
                    New DAAB.DAABRequest.Parameter("P_MontoPago", .MontoPago), _
                    New DAAB.DAABRequest.Parameter("P_ImportePago", importeTotal), _
                    New DAAB.DAABRequest.Parameter("P_FechaTransaccion", CStr(IIf(.FechaTransaccion = "", Date.Today.ToShortDateString, .FechaTransaccion))), _
                    New DAAB.DAABRequest.Parameter("P_HoraTransaccion", .HoraTransaccion), _
                    New DAAB.DAABRequest.Parameter("P_EstadoTransaccion", .EstadoTransaccion), _
                    New DAAB.DAABRequest.Parameter("P_Telefono", .Telefono), _
                    New DAAB.DAABRequest.Parameter("P_CodigoCajero", .CodigoCajero), _
                    New DAAB.DAABRequest.Parameter("P_NombreCajero", .NombreCajero), _
                    New DAAB.DAABRequest.Parameter("P_NumeroTraceCons", .NumeroTraceCons), _
                    New DAAB.DAABRequest.Parameter("P_TipoDocumentoDeudor", .TipoDocumentoDeudor), _
                    New DAAB.DAABRequest.Parameter("P_NumeroDocumentoDeudor", .NumeroDocumentoDeudor), _
                    New DAAB.DAABRequest.Parameter("P_RECAUDACIONID", codigoRecaudacion), _
                    New DAAB.DAABRequest.Parameter("P_ID_T_TRS_REG_DEUDA", ID_T_TRS_REG_DEUDA)}

                objRequest.CommandType = CommandType.StoredProcedure
                objRequest.Command = pkgNameOffSAP & ".CONF_Trs_RegistroDeuda"
                objRequest.Parameters.AddRange(arrParameters)
                resultado = "0"
                objRequest.Factory.ExecuteNonQuery(objRequest)
                codigoRecaudacion = CType(objRequest.Parameters(16), IDataParameter).Value.ToString
                returnValue = String.Format("0@{0};TRANSACCION OK", codigoRecaudacion)
            End With
        End If

        If tramaRecibosSAP.Length > 0 Then
            Dim tramaRecibos() As String = tramaRecibosSAP.Split(CChar("|"))
            Dim index%
            For index = 0 To tramaRecibos.Length - 1
                If tramaRecibos(index).Length > 0 Then
                    Dim objRecibo As New ReciboRecaudacion(tramaRecibos(index).Split(CChar(";")))
                    Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
                    With objRecibo
                        Dim arrParameters() As DAAB.DAABRequest.Parameter = { _
                            New DAAB.DAABRequest.Parameter("P_NumeroTransaccion", .nroTransaccion), _
                            New DAAB.DAABRequest.Parameter("P_posicion", .posicion), _
                            New DAAB.DAABRequest.Parameter("P_tipoDocumento", .tipoDocumento), _
                            New DAAB.DAABRequest.Parameter("P_nroDocumentoRecaud", .nroDocumentoRecaud), _
                            New DAAB.DAABRequest.Parameter("P_monedaDocumento", .monedaDocumento), _
                            New DAAB.DAABRequest.Parameter("P_importeRecibo", .importeRecibo), _
                            New DAAB.DAABRequest.Parameter("P_importePagado", .importePagado), _
                            New DAAB.DAABRequest.Parameter("P_nroCobranza", .nroCobranza), _
                            New DAAB.DAABRequest.Parameter("P_nroOpeAcree", .nroOpeAcree), _
                            New DAAB.DAABRequest.Parameter("P_fechaEmision", .fechaEmision), _
                            New DAAB.DAABRequest.Parameter("P_fechaPago", .fechaPago), _
                            New DAAB.DAABRequest.Parameter("P_nroTraceAnul", .nroTraceAnul), _
                            New DAAB.DAABRequest.Parameter("P_nroTracePago", .nroTracePago), _
                            New DAAB.DAABRequest.Parameter("P_descuentoServicio", .descuentoServicio), _
                            New DAAB.DAABRequest.Parameter("P_fechaHora", .fechaHora), _
                            New DAAB.DAABRequest.Parameter("P_servicio", .servicio), _
                            New DAAB.DAABRequest.Parameter("P_RECAUDACIONID", codigoRecaudacion), _
                            New DAAB.DAABRequest.Parameter("P_ID_T_TRS_REG_DEUDA", ID_T_TRS_REG_DEUDA)}

                        objRequest.CommandType = CommandType.StoredProcedure
                        objRequest.Command = pkgNameOffSAP & ".CONF_Trs_RegistroRecibo"
                        objRequest.Parameters.AddRange(arrParameters)
                        resultado = "0"
                        objRequest.Factory.ExecuteNonQuery(objRequest)
                    End With
                End If
            Next
        End If
        'New DAAB.DAABRequest.Parameter("P_importePagado", .importePagado), _
        If tramaPagosSAP.Length > 0 Then
            Dim tramaPago() As String = tramaPagosSAP.Split(CChar("|"))
            Dim numeroPagos As Integer = tramaPago.Length - 1
            Dim index%
            For index = 0 To tramaPago.Length - 1
                If tramaPago(index).Length > 0 Then
                    Dim objPago As New PagoRecaudacion(tramaPago(index).Split(CChar(";")))
                    Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
                    With objPago
                        'descViaPago-->DESCRIPCION DEL TIPO DE PAGO CHEQUE EFECTIVO TARJETA
                        Dim arrParameters() As DAAB.DAABRequest.Parameter = { _
                            New DAAB.DAABRequest.Parameter("P_numeroTransaccion", .numeroTransaccion), _
                            New DAAB.DAABRequest.Parameter("P_posicion", .posicion), _
                            New DAAB.DAABRequest.Parameter("P_viaPago", .viaPago), _
                            New DAAB.DAABRequest.Parameter("P_importePagado", IIf(numeroPagos > 1, .importePagado, importeTotal)), _
                            New DAAB.DAABRequest.Parameter("P_numeroCheke", .numeroCheke), _
                            New DAAB.DAABRequest.Parameter("P_belnr", .belnr), _
                            New DAAB.DAABRequest.Parameter("P_descViaPago", .descViaPago), _
                            New DAAB.DAABRequest.Parameter("P_RECAUDACIONID", codigoRecaudacion), _
                            New DAAB.DAABRequest.Parameter("P_ID_T_TRS_REG_DEUDA", ID_T_TRS_REG_DEUDA)}

                        objRequest.CommandType = CommandType.StoredProcedure
                        objRequest.Command = pkgNameOffSAP & ".CONF_Trs_RegistroPagos"
                        objRequest.Parameters.AddRange(arrParameters)
                        resultado = "0"
                        objRequest.Factory.ExecuteNonQuery(objRequest)
                    End With
                End If
            Next
        End If
        Return returnValue
    End Function

    Public Function GetRegistroDeuda(ByVal strNroTransaccion As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim dsDeuda As DataSet

        Dim dtLog As New DataTable
        Dim drFila As DataRow

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TRANSACCIONID", DbType.String, 20, strNroTransaccion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DEUDA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_RECIBO", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_PAGO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_ListarPagoDocumentosFP"
        objRequest.Parameters.AddRange(arrParam)

        dsDeuda = objRequest.Factory.ExecuteDataset(objRequest)

        ' Construccion de la tabla de respuesta
        dtLog.Columns.Add("TYPE", GetType(System.String))
        dtLog.Columns.Add("MESSAGE", GetType(System.String))
        drFila = dtLog.NewRow
        drFila("TYPE") = "I"
        drFila("MESSAGE") = "OK"
        dtLog.Rows.Add(drFila)
        dsDeuda.Tables.Add(dtLog)
        Return dsDeuda
    End Function

    Public Function SetLogRecaudacion(ByVal strLog As String, ByVal strDocum As String) As String
        'Dim strCadenaConexion = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                New DAAB.DAABRequest.Parameter("P_RECAUD", strLog), _
                                New DAAB.DAABRequest.Parameter("P_DOCUM", strDocum), _
                                New DAAB.DAABRequest.Parameter("P_AUTOGEN", DbType.String, 10, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_Trs_LogRecaudacion"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(2), IDataParameter).Value.ToString()
    End Function

    Public Function SetEstadoRecaudacion(ByVal strTraceOriginal As String, ByVal strEstadoAnulado As String, ByVal strTmp As String, ByVal strTraceAnulacion As String) As DataSet
        'Dim strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim datasetReturn As New DataSet
        Dim tabla As New DataTable
        Dim row As DataRow
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_RECAUDACIONID", strTraceOriginal), _
                                New DAAB.DAABRequest.Parameter("P_ESTAN", strEstadoAnulado), _
                                New DAAB.DAABRequest.Parameter("P_POSTMP", strTmp), _
                                New DAAB.DAABRequest.Parameter("P_TRACEANL", strTraceAnulacion)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_Trs_SetEstadoRecaudacion"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        tabla.Columns.Add("TYPE", GetType(System.String))
        tabla.Columns.Add("MESSAGE", GetType(System.String))
        row = tabla.NewRow
        row.Item("TYPE") = "I"
        row.Item("MESSAGE") = "OK"
        tabla.Rows.Add(row)
        datasetReturn.Tables.Add(tabla)
        Return datasetReturn
    End Function

    Function redondeoSicar(ByVal numeroRedondear As String) As String
        Dim separadorDecimal As String = "."
        Dim indiceSeparador As Integer = numeroRedondear.IndexOf(separadorDecimal)
        If (indiceSeparador = -1) Then Return numeroRedondear
        Dim parteDecimal As String = numeroRedondear.Split(separadorDecimal)(1)

        Dim nuevoNumero As String = numeroRedondear.Substring(0, indiceSeparador + 2)
        Dim nuevoDigito As String = String.Empty
        If (parteDecimal.Length >= 2) Then
            Dim ultimoDigito As Integer = Convert.ToInt32(parteDecimal.Substring(1, 1))
            Select Case ultimoDigito
                Case 0 To 4
                    nuevoDigito = "0"
                Case 5 To 9
                    nuevoDigito = "5"
            End Select
        End If
        nuevoNumero += nuevoDigito
        Return nuevoNumero
    End Function

#End Region

#Region "RECAUDACION DTH"

    Public Function GetConsultaCliente(ByVal oficina As String, ByVal tipoDocCliente As String, ByVal cliente As String) As DataSet
        Dim dtSet As New DataSet
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_oficina", oficina), _
                                New DAAB.DAABRequest.Parameter("P_tipoDocCliente", tipoDocCliente), _
                                New DAAB.DAABRequest.Parameter("P_cliente", cliente), _
                                New DAAB.DAABRequest.Parameter("C_TI_CLIENTE", DbType.Object, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_Con_Cliente"
        objRequest.Parameters.AddRange(arrParam)
        dtSet = objRequest.Factory.ExecuteDataset(objRequest)
        Return dtSet
    End Function

    Public Function SetActualizaCreaClienteSap(ByVal NroDocCliente As String, ByVal arrCliente As String()) As DataSet
        Dim dtSet As New DataSet
        Dim tramaCLiente$ = Join(arrCliente, ";")

        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CODIGO", NroDocCliente), _
                                                        New DAAB.DAABRequest.Parameter("P_TRAMA", tramaCLiente)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_ManteCliente"
        objRequest.Parameters.AddRange(arrParam)

        Dim tabla As New DataTable
        tabla.Columns.Add("TYPE", GetType(System.String))
        Dim row As DataRow
        row = tabla.NewRow
        row.Item("TYPE") = ""
        tabla.Rows.Add(row)
        dtSet.Tables.Add(tabla)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return dtSet
    End Function

    Public Function GetConsultaCierreCaja(ByVal Oficina As String, ByVal Fecha As String, ByVal Usuario As String, ByRef strRealizado As String) As System.Data.DataSet
        Dim dtSet As New DataSet

        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_Oficina", Oficina), _
                                                        New DAAB.DAABRequest.Parameter("P_Fecha", Fecha), _
                                                        New DAAB.DAABRequest.Parameter("P_Usuario", Usuario), _
                                                        New DAAB.DAABRequest.Parameter("CURCAJAS", DbType.Object, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Conf_Verif_Cajacerrada"
        objRequest.Parameters.AddRange(arrParam)

        Dim tabla As New DataTable
        tabla.Columns.Add("TYPE", GetType(System.String))
        Dim row As DataRow
        row = tabla.NewRow
        row.Item("TYPE") = IIf(tabla.Rows.Count > 0, "", "E")
        strRealizado = IIf(tabla.Rows.Count > 0, "", "N")
        tabla.Rows.Add(row)
        dtSet.Tables.Add(tabla)
        dtSet = objRequest.Factory.ExecuteDataset(objRequest)
        'ESTE METODO RETORNA UNA ESTRUCTURA COMUN PARA VARIAS
        Return dtSet
    End Function

    Public Function GetVerificaVendedor(ByVal Oficina As String, ByVal Vendedor As String) As DataSet
        Dim dtSet As New DataSet
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_USUARIO", Vendedor), _
                                                        New DAAB.DAABRequest.Parameter("P_TIENDA", Vendedor), _
                                                        New DAAB.DAABRequest.Parameter("C_CUR_VEND", DbType.Object, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_CON_VerificaVendedor"
        objRequest.Parameters.AddRange(arrParam)
        Dim tabla As New DataTable
        tabla.Columns.Add("TYPE", GetType(System.String))
        Dim row As DataRow
        row = tabla.NewRow
        row.Item("TYPE") = IIf(tabla.Rows.Count > 0, "", "E")
        tabla.Rows.Add(row)
        dtSet.Tables.Add(tabla)
        dtSet = objRequest.Factory.ExecuteDataset(objRequest)
        'ESTE METODO RETORNA UNA ESTRUCTURA COMUN PARA VARIAS
        Return dtSet
    End Function

    'Public Function GetParamGlobal(ByVal OficVenta As String) As DataSet
    '    Return New DataSet
    'End Function

    'Public Function ActualizarCrearClienteOnSap(ByVal oficinaVenta As String, ByVal datosCliente As String()) As DataSet
    '    Return New DataSet
    'End Function

    <Obsolete("Metodo remplazado por CreaPedidoFactura")> _
        Public Function CrearPedidoA(ByVal cadenaCabecera As String, ByVal cadenaDetalle As String, ByVal cadenaSeries As String, ByVal cadenaServicioAdicional As String, ByVal datosAcuerdo As String(), _
                                    ByVal entrega As String, ByVal factura As String, _
                                    ByVal nroContrato As String, ByVal nroDocumentoCliente As String, _
                                    ByVal nroPedido As String, ByVal refHistorico As String, _
                                    ByVal tipoDocumentoCliente As String, ByVal descuento As Double) As DataSet

        Dim dtSet As New DataSet : Dim tramaDatosAcuerdo$ = Join(datosAcuerdo, ";")

        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CABECERA", cadenaCabecera), _
                                                        New DAAB.DAABRequest.Parameter("P_DETALLE", cadenaDetalle), _
                                                        New DAAB.DAABRequest.Parameter("P_SERIES", cadenaSeries), _
                                                        New DAAB.DAABRequest.Parameter("P_SERVICIOADICIONAL", cadenaServicioAdicional), _
                                                        New DAAB.DAABRequest.Parameter("P_DATOSACUERTO", tramaDatosAcuerdo), _
                                                        New DAAB.DAABRequest.Parameter("P_ENTREGA", entrega), _
                                                        New DAAB.DAABRequest.Parameter("P_FACTURA", factura), _
                                                        New DAAB.DAABRequest.Parameter("P_NROCONTRATO", nroContrato), _
                                                        New DAAB.DAABRequest.Parameter("P_NRODOCCLIENTE", nroDocumentoCliente), _
                                                        New DAAB.DAABRequest.Parameter("P_NROPEDIDO", nroPedido), _
                                                        New DAAB.DAABRequest.Parameter("P_HISTORICO", refHistorico), _
                                                        New DAAB.DAABRequest.Parameter("P_TIPODOCUMENTO", tipoDocumentoCliente), _
                                                        New DAAB.DAABRequest.Parameter("P_DESCUENTO", descuento)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_CrearPedidoA"
        objRequest.Parameters.AddRange(arrParam)

        Dim tabla As New DataTable
        Dim row As DataRow
        row = tabla.NewRow
        row.Item("LOG") = ""
        tabla.Rows.Add(row)
        dtSet.Tables.Add(New DataTable) '''AÑADE TB ADICIONAL PARA NO ALTERAR MUXO EL CODIGO
        dtSet.Tables.Add(tabla)
        dtSet = objRequest.Factory.ExecuteDataset(objRequest)
        Return dtSet
    End Function


    ''' <summary>
    ''' REMPLAZA AL NUEVO RFC DE PEDIDO
    ''' </summary>
    ''' <remarks></remarks>
    Public Function CreaPedidoFactura(ByVal factura As String, ByVal pedido As String, ByVal tramaCabeceraPedido As String, ByVal tramaDetallePedido As String, ByVal tramaPagos As String) As Int32
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim codigoOperacion% = 0
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_Factura", factura), _
                                                        New DAAB.DAABRequest.Parameter("P_PEDIDO", pedido), _
                                                        New DAAB.DAABRequest.Parameter("P_CodigoPedido", DbType.Int32, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Mig_Trs_Pedido_Cabecera"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)

        codigoOperacion = CType(objRequest.Parameters(2), IDataParameter).Value
        objRequest = New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        If tramaCabeceraPedido.Length > 0 Then
            Dim datosCabecera() As String = tramaCabeceraPedido.Split(CChar(";"))
            Dim parametrosCabecera() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_AUART", datosCabecera(0)), _
                                                                      New DAAB.DAABRequest.Parameter("P_VKORG", datosCabecera(1)), _
                                                                      New DAAB.DAABRequest.Parameter("P_VTWEG", datosCabecera(2)), _
                                                                      New DAAB.DAABRequest.Parameter("P_SPART", datosCabecera(3)), _
                                                                      New DAAB.DAABRequest.Parameter("P_TIPO_DOC_CLIENTE", datosCabecera(4)), _
                                                                      New DAAB.DAABRequest.Parameter("P_CLIENTE", datosCabecera(5)), _
                                                                      New DAAB.DAABRequest.Parameter("P_AUDAT", datosCabecera(6)), _
                                                                      New DAAB.DAABRequest.Parameter("P_VKBUR", datosCabecera(7)), _
                                                                      New DAAB.DAABRequest.Parameter("P_XBLNR", datosCabecera(8)), _
                                                                      New DAAB.DAABRequest.Parameter("P_VENDEDOR", datosCabecera(9)), _
                                                                      New DAAB.DAABRequest.Parameter("P_WAERK", datosCabecera(10)), _
                                                                      New DAAB.DAABRequest.Parameter("P_TIPO_VENTA", datosCabecera(11)), _
                                                                      New DAAB.DAABRequest.Parameter("P_CLASE_VENTA", datosCabecera(12)), _
                                                                      New DAAB.DAABRequest.Parameter("P_NOM_CLIENTE", datosCabecera(13)), _
                                                                      New DAAB.DAABRequest.Parameter("P_DIR_CLIENTE", datosCabecera(14)), _
                                                                      New DAAB.DAABRequest.Parameter("P_COD_PEDIDO", codigoOperacion)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = pkgNameOffSAP & ".MIG_REGISTRA_CAB_PEDIDO"
            objRequest.Parameters.AddRange(parametrosCabecera)
            objRequest.Factory.ExecuteNonQuery(objRequest)
        End If

        If tramaDetallePedido.Length > 0 Then
            Dim filasDetalle() As String = tramaDetallePedido.Split(CChar("|"))
            Dim i%
            For i = 0 To filasDetalle.Length - 1
                objRequest = New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
                Dim datosFila() As String = filasDetalle(i).Split(CChar(";"))


                Dim parametrosDetalle() As DAAB.DAABRequest.Parameter = { _
                                                          New DAAB.DAABRequest.Parameter("P_POSNR", datosFila(0)), _
                                                          New DAAB.DAABRequest.Parameter("P_MATNR", datosFila(1)), _
                                                          New DAAB.DAABRequest.Parameter("P_KWMENG", datosFila(2)), _
                                                          New DAAB.DAABRequest.Parameter("P_CAMPANA", datosFila(3)), _
                                                          New DAAB.DAABRequest.Parameter("P_PLAN_TARIFARIO", datosFila(4)), _
                                                          New DAAB.DAABRequest.Parameter("P_ZZNRO_TELEF", datosFila(5)), _
                                                          New DAAB.DAABRequest.Parameter("P_VKAUS", datosFila(6)), _
                                                          New DAAB.DAABRequest.Parameter("P_REC_EFECTIVA", datosFila(7)), _
                                                          New DAAB.DAABRequest.Parameter("P_VAL_VENTA", datosFila(8)), _
                                                          New DAAB.DAABRequest.Parameter("P_DESCUENTO", datosFila(9)), _
                                                          New DAAB.DAABRequest.Parameter("P_IGV", datosFila(10)), _
                                                          New DAAB.DAABRequest.Parameter("P_TOTAL_PAGO", datosFila(11)), _
                                                          New DAAB.DAABRequest.Parameter("P_NRO_REC_SWITCH", datosFila(12)), _
                                                          New DAAB.DAABRequest.Parameter("P_DESCRIPCION", datosFila(13)), _
                                                          New DAAB.DAABRequest.Parameter("P_COD_PEDIDO", codigoOperacion)}
                objRequest.CommandType = CommandType.StoredProcedure
                objRequest.Command = pkgNameOffSAP & ".MIG_REGISTRA_DET_PEDIDO"
                objRequest.Parameters.AddRange(parametrosDetalle)
                objRequest.Factory.ExecuteNonQuery(objRequest)
            Next
        End If

        If tramaPagos.Length > 0 Then
            Dim filasPago() As String = tramaPagos.Split(CChar(";"))
            'Dim i%
            'For i = 0 To filasPago.Length - 1
            objRequest = New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim parametrosDetalle() As DAAB.DAABRequest.Parameter = { _
                                                      New DAAB.DAABRequest.Parameter("P_ORG_VTAS", filasPago(0)), _
                                                      New DAAB.DAABRequest.Parameter("P_OF_VTAS", filasPago(1)), _
                                                      New DAAB.DAABRequest.Parameter("P_VIA_PAGO", filasPago(2)), _
                                                      New DAAB.DAABRequest.Parameter("P_CONC_SBQDA", filasPago(3)), _
                                                      New DAAB.DAABRequest.Parameter("P_SOLICITANTE", filasPago(4)), _
                                                      New DAAB.DAABRequest.Parameter("P_IMPORTE", filasPago(5)), _
                                                      New DAAB.DAABRequest.Parameter("P_MONEDA", filasPago(6)), _
                                                      New DAAB.DAABRequest.Parameter("P_T_CAMBIO", filasPago(7)), _
                                                      New DAAB.DAABRequest.Parameter("P_REFERENCIA", filasPago(8)), _
                                                      New DAAB.DAABRequest.Parameter("P_GLOSA", filasPago(9)), _
                                                      New DAAB.DAABRequest.Parameter("P_F_PEDIDO", filasPago(10)), _
                                                      New DAAB.DAABRequest.Parameter("P_COND_PAGO", filasPago(11)), _
                                                      New DAAB.DAABRequest.Parameter("P_NRO_EXACTUS", filasPago(12)), _
                                                      New DAAB.DAABRequest.Parameter("P_POS", filasPago(13)), _
                                                      New DAAB.DAABRequest.Parameter("P_COD_PEDIDO", codigoOperacion)}
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = pkgNameOffSAP & ".MIG_REGISTRA_PAGOS_RV_DTH"
            objRequest.Parameters.AddRange(parametrosDetalle)
            objRequest.Factory.ExecuteNonQuery(objRequest)
            'Next
        End If
        Return codigoOperacion
    End Function

    Public Function ConsultarPoolFacturas(ByVal oficinaVenta As String, ByVal fechaVenta As String, _
                                          ByVal tipoPool As String, ByVal fechaHasta As String, _
                                          ByVal nroDocumentoCliente As String, ByVal tipoDocumento As String, _
                                          ByVal cantidadRegistros As String, ByVal mostrarPagina As String) As DataSet
        Dim dtSet As New DataSet
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", oficinaVenta), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHAVENTA", fechaVenta), _
                                                        New DAAB.DAABRequest.Parameter("P_TIPOPOOL", tipoPool), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHAHASTA", fechaHasta), _
                                                        New DAAB.DAABRequest.Parameter("P_NRODOCUMENTO", nroDocumentoCliente), _
                                                        New DAAB.DAABRequest.Parameter("P_TIPODOCUMENTO", tipoDocumento), _
                                                        New DAAB.DAABRequest.Parameter("C_POOL_FACT", DbType.Object, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_CON_ConsultaPoolFactura"
        objRequest.Parameters.AddRange(arrParam)
        dtSet = objRequest.Factory.ExecuteDataset(objRequest)
        Return dtSet
    End Function


    Public Sub ActualizaMontosPedido(ByVal idPedido As String, ByVal recaudacionEfectiva As String)
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_ID_T_TRS_PEDIDO", idPedido), _
                                                        New DAAB.DAABRequest.Parameter("P_REC_EFECTIVA", recaudacionEfectiva)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_ACTUALIZA_MONTO_PEDIDO"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
    End Sub


    Public Sub AnularPedidoFactura(ByVal numeroOperacion As String)
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NumeroOperacion", numeroOperacion)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_Trs_ActualizaEstadoPago"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
    End Sub


    Public Function _SetPagosCajeroo(ByVal FECHA_CONTAB As String, ByVal strPagos As String, ByVal USUARIO As String, ByVal NumeroReferenciaSunat As String, ByVal codigoOficina As String, ByVal numeroTransaccion As String) As DataSet
        Dim dtSet As New DataSet
        Dim tramaPago = strPagos.Split(CChar(";"))
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_ORG_VTAS", tramaPago(0)), _
                                                        New DAAB.DAABRequest.Parameter("P_OF_VTAS", tramaPago(1)), _
                                                        New DAAB.DAABRequest.Parameter("P_VIA_PAGO", tramaPago(2)), _
                                                        New DAAB.DAABRequest.Parameter("P_CONC_BSQDA", tramaPago(3)), _
                                                        New DAAB.DAABRequest.Parameter("P_SOLICITANTE", tramaPago(4)), _
                                                        New DAAB.DAABRequest.Parameter("P_IMPORTE", tramaPago(5)), _
                                                        New DAAB.DAABRequest.Parameter("P_MONEDA", tramaPago(6)), _
                                                        New DAAB.DAABRequest.Parameter("P_T_CAMBIO", tramaPago(7)), _
                                                        New DAAB.DAABRequest.Parameter("P_REFERENCIA", tramaPago(8)), _
                                                        New DAAB.DAABRequest.Parameter("P_GLOSA", tramaPago(9)), _
                                                        New DAAB.DAABRequest.Parameter("P_F_PEDIDO", tramaPago(10)), _
                                                        New DAAB.DAABRequest.Parameter("P_COND_PAGO", tramaPago(11)), _
                                                        New DAAB.DAABRequest.Parameter("P_NRO_EXACTUS", tramaPago(12)), _
                                                        New DAAB.DAABRequest.Parameter("P_POS", tramaPago(13)), _
                                                        New DAAB.DAABRequest.Parameter("P_Id_T_Trs_Pagos_Cajero", numeroTransaccion), _
                                                        New DAAB.DAABRequest.Parameter("P_Fecha_Contab", FECHA_CONTAB), _
                                                        New DAAB.DAABRequest.Parameter("P_Usuario", USUARIO), _
                                                        New DAAB.DAABRequest.Parameter("P_Referencia_Sunat", NumeroReferenciaSunat), _
                                                        New DAAB.DAABRequest.Parameter("P_OFICINA_VENTA", codigoOficina)}


        '                                                New DAAB.DAABRequest.Parameter("P_FECHA", Fecha), _
        '                                                New DAAB.DAABRequest.Parameter("P_NOTACREDITO", NotaCredito), _
        '                                                New DAAB.DAABRequest.Parameter("P_USUARIO", Usuario)

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Conf_Trs_RegistropagosCajero"
        objRequest.Parameters.AddRange(arrParam)

        Dim tabla As New DataTable
        tabla.Columns.Add("LOG", GetType(String))
        Dim row As DataRow
        row = tabla.NewRow
        row.Item("LOG") = ""
        tabla.Rows.Add(row)
        dtSet.Tables.Add(tabla) '''AÑADE TB ADICIONAL PARA NO ALTERAR EL CODIGO
        'dtSet.Tables.Add(tabla)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return dtSet
    End Function

    Public Function GetConsultaPedido(ByVal numeroOperacion As String) As DataSet
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TRANSACCIONID", numeroOperacion), _
                          New DAAB.DAABRequest.Parameter("C_CABECERA", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("C_DETALLE", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("C_PAGO", DbType.Object, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_CON_IMPRESION"
        objRequest.Parameters.AddRange(arrParam)
        Dim dsReturn As DataSet = objRequest.Factory.ExecuteDataset(objRequest)
        dsReturn.Tables(0).TableName = "CABECERA"
        dsReturn.Tables(1).TableName = "DETALLE"
        dsReturn.Tables(2).TableName = "PAGOS"
        Return dsReturn
    End Function

    Public Sub SetNumeroSunat(ByVal CodigoTransaccion As String, ByVal referenciaSunat As String, ByVal numeroOperacionST As String, ByVal viaPago As String)
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CodigoOperacion", CodigoTransaccion), _
                                                        New DAAB.DAABRequest.Parameter("P_Codsunat", referenciaSunat), _
                                                        New DAAB.DAABRequest.Parameter("P_NRORECST", numeroOperacionST), _
                                                        New DAAB.DAABRequest.Parameter("P_ViaPago", viaPago)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Conf_Mig_Setnumsunat"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
    End Sub

    ''Conf_SetErrorRecarga

    Public Sub SetErrorRecarga(ByVal CodigoTransaccion As String)
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CodigoOperacion", CodigoTransaccion)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Conf_SetErrorRecarga"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
    End Sub


#End Region

#Region "DTOs"

    Public Class DeudaRecaudacion
        Private nomDeudor, numRucDeudor, oficinaVta, nomOficina, horaTransac, numTelefono As String
        Private nroTransaccion, estadoTransac, nroTraceCons As String
        Private monPago, impoPago As String
        Private fechaTransac As String
        Private codCajero, nomCajero, tipoDocDeudor, nroDocDeudor As String

        Public Property NumeroTransaccion() As String
            Get
                Return nroTransaccion
            End Get
            Set(ByVal Value As String)
                nroTransaccion = Value
            End Set
        End Property
        Public Property NombreDeudor() As String
            Get
                Return nomDeudor
            End Get
            Set(ByVal Value As String)
                nomDeudor = Value
            End Set
        End Property
        Public Property RucDeudor() As String
            Get
                Return numRucDeudor
            End Get
            Set(ByVal Value As String)
                numRucDeudor = Value
            End Set
        End Property
        Public Property OficinaVenta() As String
            Get
                Return oficinaVta
            End Get
            Set(ByVal Value As String)
                oficinaVta = Value
            End Set
        End Property
        Public Property NombreOficinaVenta() As String
            Get
                Return nomOficina
            End Get
            Set(ByVal Value As String)
                nomOficina = Value
            End Set
        End Property
        Public Property MontoPago() As String
            Get
                Return monPago
            End Get
            Set(ByVal Value As String)
                monPago = Value
            End Set
        End Property
        Public Property ImportePago() As String
            Get
                Return impoPago
            End Get
            Set(ByVal Value As String)
                impoPago = Value
            End Set
        End Property
        Public Property FechaTransaccion() As String
            Get
                Return fechaTransac
            End Get
            Set(ByVal Value As String)
                fechaTransac = Value
            End Set
        End Property
        Public Property HoraTransaccion() As String
            Get
                Return horaTransac
            End Get
            Set(ByVal Value As String)
                horaTransac = Value
            End Set
        End Property
        Public Property EstadoTransaccion() As String
            Get
                Return estadoTransac
            End Get
            Set(ByVal Value As String)
                estadoTransac = Value
            End Set
        End Property
        Public Property Telefono() As String
            Get
                Return numTelefono
            End Get
            Set(ByVal Value As String)
                numTelefono = Value
            End Set
        End Property
        Public Property CodigoCajero() As String
            Get
                Return codCajero
            End Get
            Set(ByVal Value As String)
                codCajero = Value
            End Set
        End Property
        Public Property NombreCajero() As String
            Get
                Return nomCajero
            End Get
            Set(ByVal Value As String)
                nomCajero = Value
            End Set
        End Property
        Public Property NumeroTraceCons() As String
            Get
                Return nroTraceCons
            End Get
            Set(ByVal Value As String)
                nroTraceCons = Value
            End Set
        End Property
        Public Property TipoDocumentoDeudor() As String
            Get
                Return tipoDocDeudor
            End Get
            Set(ByVal Value As String)
                tipoDocDeudor = Value
            End Set
        End Property
        Public Property NumeroDocumentoDeudor() As String
            Get
                Return nroDocDeudor
            End Get
            Set(ByVal Value As String)
                nroDocDeudor = Value
            End Set
        End Property

        Public Sub New(ByVal Trama As String())
            Me.nroTransaccion = Trama(0)
            Me.nomDeudor = Trama(1)
            Me.numRucDeudor = Trama(2)
            Me.oficinaVta = Trama(3)
            Me.nomOficina = Trama(4)
            Me.monPago = Trama(5)
            Me.impoPago = Trama(6)
            Me.fechaTransac = Trama(7)
            Me.horaTransac = Format(DateTime.Now, "HH:mm:ss")
            Me.estadoTransac = Trama(9)
            Me.numTelefono = Trama(10)
            Me.codCajero = Trama(11)
            Me.nomCajero = Trama(12)
            Me.nroTraceCons = Trama(13)
            Me.tipoDocDeudor = Trama(14)
            Me.nroDocDeudor = Trama(15)
        End Sub
        Public Sub New()

        End Sub
    End Class

    Public Class ReciboRecaudacion
        Public nroTransaccion, posicion, monedaDocumento, nroTraceAnul, nroTracePago As String
        Public tipoDocumento, nroDocumentoRecaud, nroCobranza, nroOpeAcree, descServicio, fechaHora, servicio As String
        Public fechaEmision, fechaPago As String
        Public importeRecibo, importePagado, descuentoServicio As String

        Public Sub New(ByVal trama() As String)
            Me.nroTransaccion = trama(0)
            Me.posicion = trama(1)
            Me.tipoDocumento = trama(2)
            Me.nroDocumentoRecaud = trama(3)
            Me.monedaDocumento = trama(4)
            Me.importeRecibo = trama(5)
            Me.importePagado = trama(6)
            Me.nroCobranza = trama(7)
            Me.nroOpeAcree = trama(8)
            Me.fechaEmision = FormatoFecha(trama(9))
            'Me.fechaPago = FormatoFecha(trama(10))
            Me.fechaPago = FormatoFecha(Date.Now.ToString("dd/MM/yyyy"))  'Me.fechaEmision
            Me.nroTraceAnul = trama(11)
            Me.nroTracePago = trama(12)
            Me.descuentoServicio = trama(13)
            Me.fechaHora = trama(14)
            Me.servicio = trama(15)
        End Sub
        Public Sub New()

        End Sub
    End Class

    Public Class PagoRecaudacion
        Public numeroTransaccion, posicion, viaPago, importePagado, numeroCheke, belnr, descViaPago As String

        Public Sub New(ByVal trama() As String)
            Me.numeroTransaccion = trama(0)
            Me.posicion = trama(1)
            Me.viaPago = trama(2)
            Me.importePagado = trama(3)
            Me.numeroCheke = trama(4)
            Me.belnr = trama(5)
            Me.descViaPago = trama(6)
        End Sub
    End Class

    Shared Function FormatoFecha(ByVal Fecha As String) As String
        If (Fecha.Length > 0) Then
            If (Fecha.Length = 8) Then
                Return Fecha.Substring(6, 2) + "/" + Fecha.Substring(4, 2) + "/" + Fecha.Substring(0, 4)
            Else
                Return Fecha.Substring(6, 4) + "/" + Fecha.Substring(3, 2) + "/" + Fecha.Substring(0, 2)
            End If
            Return "0000/00/00"
        End If
    End Function

#End Region

#Region "Recaudacion DACS"

    Public Class PagosDealer
        Public dMonto, strViaPago, strDocumento, strBanco As String

        Public Property Documento() As String
            Get
                Return strDocumento
            End Get
            Set(ByVal Value As String)
                strDocumento = Value
            End Set
        End Property

        Public Property Monto() As String
            Get
                Return dMonto
            End Get
            Set(ByVal Value As String)
                dMonto = Value
            End Set
        End Property

        Public Property ViaPago() As String
            Get
                Return strViaPago
            End Get
            Set(ByVal Value As String)
                strViaPago = Value
            End Set
        End Property

        Public Property Banco() As String
            Get
                Return strBanco
            End Get
            Set(ByVal Value As String)
                strBanco = Value
            End Set
        End Property

    End Class

    ''MODIFICADO POR CCC-TS
    Public Function Set_RecaudacionDAC(ByVal OficinaVentas As String, ByVal strCliente As String, _
                                        ByVal strNombreCliente As String, ByVal fecha As String, _
                                        ByVal tramaPagosSAP As String, ByVal Usuario As String, _
                                        ByVal MontoPago As String, ByVal strRectp As String, _
                                        ByVal strNodo As String, ByVal Tuser As String, _
                                        ByVal NroAt As String, ByVal strTipoPago As String) As DataSet
        'Dim strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim strUserIDBD As String = Me.GetUserIDByProvider(strCadenaConexion, "1")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim tabla As New DataTable
        Dim dsReturn As New DataSet
        Try
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                New DAAB.DAABRequest.Parameter("P_NROAT", DbType.String, 12, NroAt, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_COD_CLIENTE", DbType.String, 12, strCliente, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_CLIENTE", DbType.String, 20, strNombreCliente, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_MONTO", DbType.Double, CDbl(MontoPago), ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_UNAME", DbType.String, 10, Tuser, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_RECTP", DbType.String, 10, strRectp, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, OficinaVentas, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_COD_CAJERO", DbType.String, 20, Usuario, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_NODO", DbType.String, 20, strNodo, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_USUARIO_REGISTRO", DbType.String, 20, strUserIDBD, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_TIPO_PAGO", DbType.String, 5, strTipoPago, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_TRANS_RECAUD", DbType.Int64, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("P_PROCEDURE", DbType.String, 500, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("P_TABLA", DbType.String, 500, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = pkgNameOffSAP & ".MIG_Registrar_Recaudacion_DAC"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            'tabla.Columns.Add("TYPE")
            'Dim dr As DataRow
            'dr = tabla.NewRow
            'dr("TYPE") = ""
            'dsReturn.Tables.Add(tabla)
            'Dim Nrotrans As Int32 = CType(objRequest.Parameters(10), IDataParameter).Value

            If tramaPagosSAP.Length > 0 Then
                Dim tramaPago() As String = tramaPagosSAP.Split(CChar("|"))

                Dim index%
                Dim arr() As String
                For index = 0 To tramaPago.Length - 1
                    If tramaPago(index).Length > 0 Then

                        'With objPago
                        'Dim num As Integer
                        'num = CType(DateTime.Now.Ticks Mod System.Int32.MaxValue, Integer)
                        'Dim rnd As New Random(num)
                        'Dim strNroRan As String = CType(rnd.Next(), String)

                        'Dim cadena$ = "0000000000"
                        'tramaPago(index) = Right(cadena & strCliente, 10) & ";" & FormatoFecha(fecha) & ";" & strNroRan & ";" & tramaPago(index)

                        'Dim objPago As New PagosDealer(tramaPago(index).Split(CChar(";")))
                        arr = tramaPago(index).Split(CChar(";"))

                        Dim objRequest2 As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

                        Dim arrParameters() As DAAB.DAABRequest.Parameter = { _
                            New DAAB.DAABRequest.Parameter("P_NROAT", DbType.String, 12, NroAt, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("P_COD_CLIENTE", DbType.String, 10, strCliente, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("P_VIA_PAGO", DbType.String, 4, arr(2), ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("P_MONTO", DbType.Decimal, 10, Convert.ToDecimal(arr(1)), ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("P_NRO_CHQ_TRJ", DbType.String, 20, arr(0), ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("P_CODIGO_BANCO", DbType.String, 10, arr(3), ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("P_USUARIO_REGISTRO", DbType.String, 20, strUserIDBD, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("P_PROCEDURE", DbType.String, 500, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("P_TABLA", DbType.String, 500, ParameterDirection.Output)}
                        objRequest2.CommandType = CommandType.StoredProcedure
                        objRequest2.Command = pkgNameOffSAP & ".MIG_Registrar_Recaud_DAC_DET"
                        objRequest2.Parameters.AddRange(arrParameters)
                        objRequest2.Factory.ExecuteNonQuery(objRequest2)

                    End If
                Next

                Dim drFila As DataRow
                tabla.Columns.Add("TYPE", GetType(System.String))
                tabla.Columns.Add("MESSAGE", GetType(System.String))
                drFila = tabla.NewRow
                drFila("TYPE") = "I"
                drFila("MESSAGE") = "OK"
                tabla.Rows.Add(drFila)
                dsReturn.Tables.Add(tabla)
            End If
        Catch ex As Exception
            Dim drFila As DataRow
            tabla.Columns.Add("TYPE", GetType(System.String))
            tabla.Columns.Add("MESSAGE", GetType(System.String))
            drFila = tabla.NewRow
            drFila("TYPE") = "E" ' I: Information w: Warning E:Error
            drFila("MESSAGE") = ex.Message
            tabla.Rows.Add(drFila)
            dsReturn.Tables.Add(tabla)
        End Try

        Return dsReturn
    End Function

    Private Function GetUserIDByProvider(ByVal strCadenaCnx As String, ByVal ProviderDB As String) As String
        Dim resul As String = String.Empty
        If ProviderDB = "1" Then ' ORACLE
            'format cnx = user id=" & Usuario & ";data source=" & BaseDatos & ";password=" & Password
            Dim arrayCNX() As String = strCadenaCnx.Split(";")

            If arrayCNX(0) <> String.Empty Then
                Dim userID As String = arrayCNX(0).ToString()
                Dim aCNX() As String = userID.Split("=")
                If aCNX(1) <> String.Empty Then
                    resul = aCNX(1).ToString()
                Else
                    resul = ""
                End If
            Else
                resul = ""
            End If
        Else ' Otra BD= SQL SERVER , MYSQL

        End If

        Return resul

    End Function

#End Region

#Region "Asignacion de Cajeros"
    'FFS (CONSULTA CAJERO POR OFICINA)
    Public Function Get_ConsultaCajeros(ByVal strOficina As String, ByVal strTipoUsuario As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, strOficina, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("P_TIPOSUARIO", DbType.String, 1, strTipoUsuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURCAJERO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_Con_Cajeros_Oficina"
        objRequest.Parameters.AddRange(arrParam)

        Get_ConsultaCajeros = objRequest.Factory.ExecuteDataset(objRequest)

    End Function
    'FFS (CONSULTA CAJA OFICINA)
    Public Function Get_CajaOficinas(ByVal strOficina As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURCAJAS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_Con_Cajas_Oficina"
        objRequest.Parameters.AddRange(arrParam)

        Get_CajaOficinas = objRequest.Factory.ExecuteDataset(objRequest)

    End Function
    Public Function Set_CajeroDiario(ByVal strOficina As String, ByVal strUsuario As String, ByVal strFecha As String, ByVal strCaja As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim dsReturn As New DataSet
        Dim dtReturn As New DataTable
        Dim dcReturn As DataRow

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CAJA", DbType.String, 2, strCaja, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, strFecha, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, strUsuario, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, strOficina, ParameterDirection.Input), _
                                 New DAAB.DAABRequest.Parameter("P_MENSAJE", DbType.AnsiString, 2000, Nothing, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_Trs_Cajero_Diario"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        dtReturn.Columns.Add("TYPE", GetType(System.String))
        dtReturn.Columns.Add("MESSAGE", GetType(System.String))

        Dim mensage As String = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)

        dcReturn = dtReturn.NewRow
        If IsDBNull(CType(objRequest.Parameters(4), IDataParameter).Value) Then
            dcReturn.Item("TYPE") = "I"
            dcReturn.Item("MESSAGE") = "OK"
        Else
            dcReturn.Item("TYPE") = "E"
            dcReturn.Item("MESSAGE") = CType(objRequest.Parameters(4), IDataParameter).Value
        End If

        dtReturn.Rows.Add(dcReturn)
        dsReturn.Tables.Add(dtReturn)

        Set_CajeroDiario = dsReturn

    End Function
    Public Function Set_Cerrar_Caja(ByVal strOficina As String, ByVal strUsuario As String, ByVal strFecha As String, ByVal strCaja As String, ByVal strEstado As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        Dim dsReturn As New DataSet
        Dim dtReturn As New DataTable
        Dim dcReturn As DataRow

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CAJA", DbType.String, 2, strCaja, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, strFecha, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, strUsuario, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, strOficina, ParameterDirection.Input), _
                                 New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 1, strEstado, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_Trs_CierraCaja"
        objRequest.Parameters.AddRange(arrParam)

        Set_Cerrar_Caja = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function



    Public Function VerificarAsignacionCajero(ByVal codigoOficina As String, ByVal codigoUsuario As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim fechaAsignacion = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fechaAsignacion, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, codigoUsuario, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, codigoOficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_MENSAJE", DbType.String, 50, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_VerificarAsignacionCajero"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteDataset(objRequest)
        Return Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
    End Function

    Public Function VerificarAsignacionCajero(ByVal codigoOficina As String, ByVal codigoUsuario As String, ByVal fechaAsignacion As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        'Dim fechaAsignacion$ = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fechaAsignacion, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, codigoUsuario, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, codigoOficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_MENSAJE", DbType.String, 50, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_VerificarAsignacionCajero"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteDataset(objRequest)
        Return Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
    End Function

#End Region

#Region "RECARGA VIRTUAL"

    'Descripcion_Vias_Pago
    Public Function DescripcionViaPagoByCode(ByVal codigoViaPago As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_Cod_Via_Pago", DbType.String, 4, codigoViaPago, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_VIAPAGO", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Descripcion_Vias_Pago"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(1), IDataParameter).Value.ToString
    End Function


    'CONF_Mae_Vendedor
    ''TODO: FUNCION Get_ConsultaVendedor DE SAP
    Public Function ListarVenderoresPorTienda(ByVal codigoTienda As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TIENDA", DbType.String, 4, codigoTienda, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("CURUSUARIO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_Mae_Vendedor"
        objRequest.Parameters.AddRange(arrParam)
        Return objRequest.Factory.ExecuteDataset(objRequest)
    End Function
    ''CONF_Mae_TipoDocCliente
    ''TODO: FUNCION Get_LeeTipoDocCliente DE SAP
    Public Function ListarTipoDocumentosCliente() As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("CURUSUARIO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_Mae_TipoDocCliente"
        objRequest.Parameters.AddRange(arrParam)

        Return objRequest.Factory.ExecuteDataset(objRequest)
    End Function
    ''CONF_Mae_ClasesDocTienda
    ''TODO: FUNCION Get_ConsultaClasePedido DE SAP
    Public Function ListarClasePedido(ByVal codigoTienda As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TIENDA", DbType.String, 4, codigoTienda, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("CURUSUARIO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_Mae_ClasesDocTienda"
        objRequest.Parameters.AddRange(arrParam)
        Return objRequest.Factory.ExecuteDataset(objRequest)
    End Function
    ''Conf_Parametrosventa
    ''TODO: FUNCION Get_ParamGlobal DE SAP
    Public Function ParametrosVenta(ByVal codigoTienda As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TIENDA", DbType.String, 4, codigoTienda, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("CURUSUARIO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Conf_Parametrosventa"
        objRequest.Parameters.AddRange(arrParam)
        Return objRequest.Factory.ExecuteDataset(objRequest)
    End Function
    ''Proc_Act_Estadopedido
    ''TODO: FUNCION Set_ActualizaEstadoPedido DE SAP
    Public Function ActualizaEstadoPedido(ByVal numeroPedido As String, ByVal oficinaVenta As String, ByVal almacenero As String, ByVal despachador As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        Dim oracleRequest As New DAAB.DAABRequest(DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim requestParameters() As DAAB.DAABRequest.Parameter = { _
            New DAAB.DAABRequest.Parameter("P_Nropedido", DbType.String, 10, numeroPedido, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("P_Oficina_Venta", DbType.String, 10, oficinaVenta, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("P_Almacenero", DbType.String, 25, almacenero, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("P_Despachador", DbType.String, 25, despachador, ParameterDirection.Input) _
        }

        oracleRequest.CommandType = CommandType.StoredProcedure
        oracleRequest.Command = pkgNameOffSAP & ".Proc_Act_Estadopedido"
        oracleRequest.Parameters.AddRange(requestParameters)
        oracleRequest.Factory.ExecuteNonQuery(oracleRequest)
        Dim datasetReturn As New DataSet
        Dim tableReturn As New DataTable

        tableReturn.Columns.Add("TYPE", GetType(System.String))
        Dim row As DataRow
        row = tableReturn.NewRow
        row.Item("TYPE") = ""
        tableReturn.Rows.Add(row)
        datasetReturn.Tables.Add(tableReturn)
        Return datasetReturn
    End Function

    Public Function SetGet_LogActivacionCHIP(ByVal NRO_DOCUMENTO As String, ByVal OFI_VENTA As String, ByVal strLog As String, ByRef NRO_SOLICITUD As String) As DataSet
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim strTrama() As String = strLog.Split(CChar(";"))
        Dim returnValue$ = String.Empty
        Dim codigoRecaudacion$ = String.Empty
        If strTrama.Length > 0 Then

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

            Dim arrParameters() As DAAB.DAABRequest.Parameter = { _
                New DAAB.DAABRequest.Parameter("P_NumeroTransaccion", strTrama(0)), _
                New DAAB.DAABRequest.Parameter("P_NRO_TELEFONO", strTrama(1)), _
                New DAAB.DAABRequest.Parameter("P_SERIE_ACTUAL", strTrama(2)), _
                New DAAB.DAABRequest.Parameter("P_IMSI_ACTUAL", strTrama(3)), _
                New DAAB.DAABRequest.Parameter("P_OFICINA_VENTA", strTrama(4)), _
                New DAAB.DAABRequest.Parameter("P_DOCUMENTO", strTrama(5)), _
                New DAAB.DAABRequest.Parameter("P_MOTIVO_PEDIDO", strTrama(6)), _
                New DAAB.DAABRequest.Parameter("P_SERIE_NUEVA", strTrama(7)), _
                New DAAB.DAABRequest.Parameter("P_IMSI_NUEVO", strTrama(8)), _
                New DAAB.DAABRequest.Parameter("P_VENDEDOR", strTrama(9)), _
                New DAAB.DAABRequest.Parameter("P_ESTADO_INI_TELEF", strTrama(10)), _
                New DAAB.DAABRequest.Parameter("P_FECHA_CREACION", strTrama(11)), _
                New DAAB.DAABRequest.Parameter("P_HORA_CREACION", strTrama(12)), _
                New DAAB.DAABRequest.Parameter("P_ESTADO_SOLICITUD", strTrama(13)), _
                New DAAB.DAABRequest.Parameter("P_ENVIADO_VARIACIO", strTrama(14)), _
                New DAAB.DAABRequest.Parameter("P_ESTADO_FIN_TELEF", strTrama(15)), _
                New DAAB.DAABRequest.Parameter("P_FECHA_ACTUALIZ", strTrama(16)), _
                New DAAB.DAABRequest.Parameter("P_HORA_ACTUALIZ", strTrama(17)), _
                New DAAB.DAABRequest.Parameter("P_NumeroDocumento", NRO_DOCUMENTO)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = pkgNameOffSAP & ".Proc_Logactivacionchip"
            objRequest.Parameters.AddRange(arrParameters)
            objRequest.Factory.ExecuteNonQuery(objRequest)
            codigoRecaudacion = CType(objRequest.Parameters(16), IDataParameter).Value.ToString
            returnValue = String.Format("0@{0};TRANSACCION OK", codigoRecaudacion)
        End If
    End Function

    '--Get_Direccion_Pdv

    Public Function ObtenerDireccionPDV(ByVal codigoVKBUR As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_Vkbur", DbType.String, 4, codigoVKBUR, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_Direccion", DbType.String, 400, Nothing, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Get_Direccion_Pdv"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(1), IDataParameter).Value.ToString
    End Function


    Public Function ObtenerNombreCajero(ByVal codigoCajero As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CodigoVendedor", DbType.String, 10, codigoCajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_Vendedor", DbType.String, 40, Nothing, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Conf_NombreCajero"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(1), IDataParameter).Value.ToString
    End Function


    Public Function getNumeroSunat(ByVal numeroTransaccion As String) As String
        'Conf_GetNumeroSunat

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CodigoOperacion", DbType.String, 7, numeroTransaccion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_PAGO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Conf_GetNumeroSunat"
        objRequest.Parameters.AddRange(arrParam)
        Dim dsResult As DataSet = objRequest.Factory.ExecuteDataset(objRequest)
        Dim numeroSunat$ = IIf(dsResult.Tables.Count > 0& & dsResult.Tables(0).Rows.Count > 0, "", dsResult.Tables(0).Rows(0)("xblnr"))
        Return numeroSunat$
    End Function


#End Region

#Region "VENTA RAPIDA"
    ''CONF_CLASEPEDIDO
    ''Get_ConsultaClasePedido()
    Public Function ConsultaClasePedido(ByVal codigoTienda As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TIENDA", DbType.String, 10, codigoTienda, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_POOL_FACT", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_CLASEPEDIDO"
        objRequest.Parameters.AddRange(arrParam)
        Return objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    ''Conf_Motivopedido
    ''Get_ConsultaMotivoPedido
    Public Function ConsultaMotivoPedido(ByVal motivoPedido As String, ByVal codigoTienda As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_Motivo", DbType.String, 10, motivoPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_TIENDA", DbType.String, 10, codigoTienda, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_POOL_FACT", DbType.Object, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Conf_Motivopedido"
        objRequest.Parameters.AddRange(arrParam)
        Return objRequest.Factory.ExecuteDataset(objRequest)
    End Function

#End Region

#Region "POOL RECAUDACIONES PROCESADAS"

    Public Function GetPoolRecaudacion(ByVal strFechaTransaccion As String, ByVal strOficinaVenta As String, _
                                        ByVal strTipo As String, ByVal usuario As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

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
        Dim arrParam() As DAAB.DAABRequest.Parameter = _
                         {New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, strFechaTransaccion, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, strOficinaVenta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_TIPO", DbType.String, 1, strTipo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.AnsiString, 10, usuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURPOOLRECAUDACION", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_Con_Pool_Recaudacion"
        objRequest.Parameters.AddRange(arrParam)

        dsPool = objRequest.Factory.ExecuteDataset(objRequest)

        ' Construccion de la tabla 
        'dtPool.Columns.Add("NRO_TRANSACCION", GetType(System.String))
        'dtPool.Columns.Add("FECHA_TRANSAC", GetType(System.String))
        'dtPool.Columns.Add("RUC_DEUDOR", GetType(System.String))
        'dtPool.Columns.Add("NOM_DEUDOR", GetType(System.String))
        'dtPool.Columns.Add("MON_PAGO", GetType(System.String))
        'dtPool.Columns.Add("IMPORTE_PAGO", GetType(System.String))


        'For i = 0 To dsPool.Tables(0).Rows.Count - 1
        '    drFila = dtPool.NewRow

        '    drFila("NRO_TRANSACCION") = dsPool.Tables(0).Rows(i).Item("RECAU_AUTOGEN") 'modificado por JCR - cambio de cero por i
        '    drFila("FECHA_TRANSAC") = Split(dsPool.Tables(0).Rows(i).Item("RECAU_RECIBOS"), ";")(10)
        '    drFila("RUC_DEUDOR") = Split(dsPool.Tables(0).Rows(i).Item("RECAU_DEUDA"), ";")(2)
        '    drFila("NOM_DEUDOR") = Split(dsPool.Tables(0).Rows(i).Item("RECAU_DEUDA"), ";")(1)
        '    drFila("MON_PAGO") = Split(dsPool.Tables(0).Rows(i).Item("RECAU_DEUDA"), ";")(5)
        '    drFila("IMPORTE_PAGO") = Split(dsPool.Tables(0).Rows(i).Item("RECAU_DEUDA"), ";")(6)

        '    dtPool.Rows.Add(drFila)
        'Next

        'dsReturn.Tables.Add(dtPool)

        GetPoolRecaudacion = dsPool

    End Function

#End Region

#Region "Documentos Pagados"
    Public Function GetConsultaPagosUsuario(ByVal FechaIni As String, ByVal FechaFin As String, ByVal strSoloAnul As String, _
                                        ByVal strUsuario As String, ByVal strOficina As String, ByVal decIGV As Decimal) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

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

        ''' 0000010554 --> formato cod vendedor
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_FECHAINI", DbType.String, 10, FechaIni, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, strUsuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_IGV", DbType.Decimal, 10, decIGV, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURPOOLPAGOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_Con_Pool_Pagos"
        objRequest.Parameters.AddRange(arrParam)
        dsPool = objRequest.Factory.ExecuteDataset(objRequest)
        GetConsultaPagosUsuario = dsPool
    End Function

    Public Function GetConsultaPagosUsuario(ByVal FechaIni As String, ByVal FechaFin As String, ByVal strSoloAnul As String, _
                                    ByVal strUsuario As String, ByVal strOficina As String, ByVal estado As String, ByVal decIGV As Decimal) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

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
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_FECHAINI", DbType.String, 10, FechaIni, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, strUsuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_TIPO", DbType.String, 1, estado, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_IGV", DbType.Decimal, 10, decIGV, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CURPOOLPAGOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_Con_Pool_Pagos"
        objRequest.Parameters.AddRange(arrParam)
        dsPool = objRequest.Factory.ExecuteDataset(objRequest)
        GetConsultaPagosUsuario = dsPool
    End Function

    Public Function Set_ProcesaPagoDocumento0(ByVal DocumentoSAP As String) As Int32
        Dim dtSet As New DataSet
        Dim procesaPedidos As Int32
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_VBELN", DbType.String, 12, DocumentoSAP, ParameterDirection.Input)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_Trs_Registra_Documentos"
        objRequest.Parameters.AddRange(arrParam)
        procesaPedidos = objRequest.Factory.ExecuteNonQuery(objRequest)
        Return procesaPedidos
    End Function
#End Region

#Region "Paginacion"
    Public Function GetConsultaPaginacion(ByVal SessionID As String, ByVal Numero_Pagina As Integer) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA") '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        Dim dsPool As DataSet
        Dim dtPool As New DataTable
        Dim drFila As DataRow
        Dim dsReturn As New DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                        New DAAB.DAABRequest.Parameter("P_SESSION_ID", DbType.String, 50, SessionID, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NUMERO_PAGINA", DbType.Int64, 20, Numero_Pagina, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_PAGINAS", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_SUMA", DbType.Object, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Conf_Paginacion"
        objRequest.Parameters.AddRange(arrParam)
        GetConsultaPaginacion = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function GetSumaMontosPaginacion(ByVal SessionID As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA") '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_SESSION_ID", DbType.String, 50, SessionID, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_Suma", DbType.String, ParameterDirection.Output) _
                                                        }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Conf_GetMontosPaginacion"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Dim montoDevuelto$ = IIf(Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value) = String.Empty, 0, Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value))

        Return montoDevuelto$
    End Function

    Public Function Set_Registra_Paginacion(ByVal NumeroRecibo As String, ByVal Seleccion As String, _
                                                ByVal SessionID As String, ByVal Monto As String, ByVal NroPagina As Integer, _
                                                ByVal tramaRecibo As String, ByVal montoRecibo As String, ByVal ultimoRecibo As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        'Dim dsReturn As New DataSet
        'Dim dtReturn As New DataTable
        'Dim dcReturn As DataRow
        'P_MONTO_RECIBO
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NUMERO_RECIBO", DbType.String, 20, NumeroRecibo, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_FLAG_SELECCION", DbType.String, 1, Seleccion, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_SESSION_ID", DbType.String, 50, SessionID, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_MONTO", DbType.String, 20, Monto, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_NUMERO_PAGINA", DbType.Int32, 20, NroPagina, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_TRAMA_RECIBO", DbType.String, 300, tramaRecibo, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_MONTO_RECIBO", DbType.String, 20, montoRecibo, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_ULTIMO_RECIBO", DbType.String, 20, ultimoRecibo, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Conf_Registrar_Paginacion"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        'Set_Registra_Paginacion = dsReturn

    End Function
    Public Sub EliminaPaginacion(ByVal sessionID As String)
        'Conf_Eliminarpaginacion

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA") '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_Session_Id", DbType.String, 50, sessionID, ParameterDirection.Input) _
                                                        }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".Conf_Eliminarpaginacion"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
    End Sub
#End Region

#Region "CUADRE DE CAJA"
    Public Sub ActualizarEstadoCuadre(ByVal oficinaVenta As String, ByVal vendedor As String, ByVal fechaCierre As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA") '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                        New DAAB.DAABRequest.Parameter("P_OficinaVenta", DbType.String, 5, oficinaVenta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_Vendedor", DbType.String, 10, vendedor, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_Fecha", DbType.String, 10, fechaCierre, ParameterDirection.Input) _
                                                        }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_ActualizarEstadoCuadre"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
    End Sub

    Public Function CuadreCajeroRealizado(ByVal oficinaVenta As String, ByVal vendedor As String) As Boolean
        Dim valRerturn% = 0

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA") '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                        New DAAB.DAABRequest.Parameter("P_OficinaVenta", DbType.String, 5, oficinaVenta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_Vendedor", DbType.String, 10, vendedor, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MENSAJE", DbType.String, 50, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_CuadreCajeroRealizado"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteDataset(objRequest)
        valRerturn = Convert.ToInt32(CType(objRequest.Parameters(2), IDataParameter).Value)
        Return valRerturn > 0
    End Function

    Public Function enviandoPedidosPDV(ByVal pdv As String) As Boolean

        Dim valRerturn% = 0

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA") '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 5, pdv, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_COUNT", DbType.Int32, 50, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_VERIF_EJEC_CUADRE_PDV"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteDataset(objRequest)
        valRerturn = Convert.ToInt32(CType(objRequest.Parameters(1), IDataParameter).Value)

        Return valRerturn > 0

    End Function

    'INICIO PRE_CUADRE_OFFLINE :: Agregado por TS-CCC
    Public Function EliminarMontosVentas(ByVal oficinaVenta As String, ByVal cajero As String, ByVal fecha As String, ByVal tipoCuadre As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA") '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_oficina", DbType.String, 4, oficinaVenta, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_fecha", DbType.String, 8, fecha, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_cajero", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_tipo", DbType.String, 1, tipoCuadre, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MsgErr", DbType.String, 200, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_EliminarMontosVentas"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(4), IDataParameter).Value.ToString
    End Function

    Public Function SetTransCuadreCajeroGen(ByVal oficina As String, ByVal fecha As String, _
                                    ByVal cajero As String, ByVal cuadreRealizado As String, _
                                    ByRef idTranCuadre As String) As String

        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim strUserIDBD As String = Me.GetUserIDByProvider(strCadenaConexion, "1")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                              New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 8, fecha, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_CUADRE_REALIZADO", DbType.String, 1, cuadreRealizado, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_USUARIO_CREACION", DbType.String, 10, strUserIDBD.ToUpper.Trim(), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_ID_TRANS_CUADRE", DbType.String, 18, ParameterDirection.Output), _
                                                              New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 500, ParameterDirection.Output) _
                                                              }

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_RegistrarTrsCuadre"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        idTranCuadre = CType(objRequest.Parameters(5), IDataParameter).Value.ToString
        Return CType(objRequest.Parameters(6), IDataParameter).Value.ToString
    End Function

    Public Function ActualizarTransCuadreCajeroGen(ByVal idCuadreAsignado As String, ByVal cuadreRealizado As String) As String
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim strUserIDBD As String = Me.GetUserIDByProvider(strCadenaConexion, "1")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                              New DAAB.DAABRequest.Parameter("P_ID_TRS_CUADRE_GEN", DbType.String, idCuadreAsignado, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_CUADRE_REALIZADO", DbType.String, 1, cuadreRealizado, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_USUARIO_MODIFICA", DbType.String, 10, strUserIDBD.ToUpper.Trim(), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 500, ParameterDirection.Output) _
                                                              }

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_ActualizaTrsCuadre"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(3), IDataParameter).Value.ToString
    End Function

    Public Function SetLogCuadreDia(ByVal idTransCuadre As String, ByVal ingresoEfectivo As String, _
                                    ByVal cajaBuzon As String, ByVal cajaBuzonCheque As String, _
                                    ByVal montoFaltante As String, ByVal comentario As String, _
                                    ByVal nodo As String) As String

        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim strUserIDBD As String = Me.GetUserIDByProvider(strCadenaConexion, "1")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                              New DAAB.DAABRequest.Parameter("P_TRS_CUADRE", DbType.Int64, CLng(idTransCuadre), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_INGRESO_EFECTIVO", DbType.Double, CDbl(ingresoEfectivo), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_CAJA_BUZON", DbType.Double, CDbl(cajaBuzon), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_CAJA_BUZON_CHEQUE", DbType.Double, CDbl(cajaBuzonCheque), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_MONTO_FALTANTE", DbType.Double, CDbl(montoFaltante), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_COMENTARIO", DbType.String, 50, comentario, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_NODO", DbType.String, 2, nodo, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_USUARIO_CREACION", DbType.String, 10, strUserIDBD.ToUpper.Trim(), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 500, ParameterDirection.Output) _
                                                              }

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_RegistrarLogCuadreDia"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(8), IDataParameter).Value.ToString
    End Function

    Public Function SetLogCuadreGen(ByVal idTransCuadre As String, ByVal ingresoEfectivo As String, _
                                    ByVal cajaBuzon As String, ByVal cajaBuzonCheque As String, _
                                    ByVal montoFaltante As String, ByVal remesa As String, _
                                    ByVal comentario As String, ByVal nodo As String) As String

        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim strUserIDBD As String = Me.GetUserIDByProvider(strCadenaConexion, "1")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                              New DAAB.DAABRequest.Parameter("P_TRS_CUADRE", DbType.Int64, CLng(idTransCuadre), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_INGRESO_EFECTIVO", DbType.Double, CDbl(ingresoEfectivo), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_CAJA_BUZON", DbType.Double, CDbl(cajaBuzon), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_CAJA_BUZON_CHEQUE", DbType.Double, CDbl(cajaBuzonCheque), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_MONTO_FALTANTE", DbType.Double, CDbl(montoFaltante), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_REMESA", DbType.Double, CDbl(remesa), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_COMENTARIO", DbType.String, 50, comentario, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_NODO", DbType.String, 2, nodo, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_USUARIO_CREACION", DbType.String, 10, strUserIDBD.ToUpper.Trim(), ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 500, ParameterDirection.Output) _
                                                              }

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_RegistrarLogCuadreGen"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(9), IDataParameter).Value.ToString
    End Function

    Public Function SetMontosCuadreCajero(ByVal oficina As String, ByVal fecha As String, ByVal contador As String, _
                                            ByVal cajero As String, ByVal desc_concepto As String, ByVal monto As String) As String
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim strUserIDBD As String = Me.GetUserIDByProvider(strCadenaConexion, "1")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim dMonto As Double
        If monto <> String.Empty Then
            dMonto = CDbl(monto)
        End If
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                            New DAAB.DAABRequest.Parameter("P_OFICINA_VENTA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 8, fecha, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_CONTADOR", DbType.String, 4, contador, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_DESC_CONCEPTO", DbType.String, 50, desc_concepto, ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_MONTO", DbType.Double, IIf(monto = String.Empty, DBNull.Value, dMonto), ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_USUARIO_CREACION", DbType.String, 10, strUserIDBD.ToUpper.Trim(), ParameterDirection.Input), _
                                                            New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 500, ParameterDirection.Output) _
                                                        }

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_RegistrarMontosCuadre"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(7), IDataParameter).Value.ToString
    End Function

    Public Function GetDatosAsignacionCajero(ByVal oficina As String, ByVal fecha As String, ByVal codigoUsuario As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, codigoUsuario, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_ObtenerCajaAsignada"
        objRequest.Parameters.AddRange(arrParam)
        GetDatosAsignacionCajero = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function GetDatosAsignacionGeneral(ByVal oficina As String, ByVal fecha As String, ByVal codigoUsuario As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, codigoUsuario, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_ObtenerCajaAsignadaGen"
        objRequest.Parameters.AddRange(arrParam)
        GetDatosAsignacionGeneral = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function GetRecaudacionesCuadreInd(ByVal oficinaVta As String, ByVal fecha As String, ByVal cajero As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficinaVta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DEUDA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_PAGO", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_TRDAC", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_TRDAC_DET", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_TRDRA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_TRDRA_DET", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetRecaudacionCuadreInd"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function

    Public Function GetRecaudacionesCuadreGen(ByVal oficinaVta As String, ByVal fecha As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficinaVta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DEUDA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_PAGO", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_TRDAC", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_TRDAC_DET", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_TRDRA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_TRDRA_DET", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetRecaudacionCuadreGen"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function

    Public Function SetVentasFactCuadre(ByVal oficina As String, ByVal fecha As String, ByVal cajero As String, _
                                        ByVal desc_documento As String, ByVal factura_ficticia As String, ByVal referencia As String, _
                                        ByVal vendedor As String, ByVal moneda As String, ByVal clase_factura_cod As String, _
                                        ByVal nroCuotas As String, ByVal total_factura As String, _
                                        ByVal saldo As String, ByVal referencia_ori As String, ByVal estado As String, ByVal nodo As String, _
                                        ByVal tramaPagos As String, ByRef ID As String, Optional ByVal P_NRO_TELEFONO As String = "", _
                                        Optional ByVal P_NROOPE_TRACE As String = "") As DataSet

        Dim dtSet As New DataSet
        Dim strMensajeError As String = String.Empty
        Try
            strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA")
            Dim strUserIDBD As String = Me.GetUserIDByProvider(strCadenaConexion, "1")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                                  New DAAB.DAABRequest.Parameter("P_OFICINA_VENTA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_DESC_DOCUMENTO", DbType.String, 30, desc_documento, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_FACTURA_FICTICIA", DbType.String, 10, factura_ficticia, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_REFERENCIA", DbType.String, 16, referencia, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_VENDEDOR", DbType.String, 10, vendedor, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_MONEDA", DbType.String, 5, moneda, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_CLASE_FACTURA_COD", DbType.String, 4, clase_factura_cod, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_NRO_CUOTAS", DbType.String, 2, nroCuotas, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_TOTAL_FACTURA", DbType.Double, CDbl(total_factura), ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_SALDO", DbType.Double, CDbl(saldo), ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_REFERENCIA_ORIG", DbType.String, 8, referencia_ori, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 2, estado, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_NODO", DbType.String, 4, nodo, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_USUARIO_CREACION", DbType.String, 10, strUserIDBD.ToUpper.Trim(), ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_NUMERO_TELEFONO", DbType.String, 20, P_NRO_TELEFONO, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_NROOPE_TRACE", DbType.String, 20, P_NROOPE_TRACE, ParameterDirection.Input), _
                                                                  New DAAB.DAABRequest.Parameter("P_ID_TI_VENTAS_FACT", DbType.String, 20, ParameterDirection.Output), _
                                                                  New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 500, ParameterDirection.Output) _
                                                                  }

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = pkgNameOffSAP & ".MIG_RegistrarVentasFactCuadre"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            ID = CType(objRequest.Parameters(18), IDataParameter).Value.ToString
            strMensajeError = CType(objRequest.Parameters(19), IDataParameter).Value.ToString

            If strMensajeError.Length = 0 Then
                If tramaPagos.Length > 0 Then
                    Dim tramaPago() As String = tramaPagos.Split(CChar("|"))

                    Dim index%
                    Dim arr() As String
                    For index = 0 To tramaPago.Length - 1
                        If tramaPago(index).Length > 0 Then
                            arr = tramaPago(index).Split(CChar(";"))

                            Dim objRequest2 As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

                            Dim arrParameters() As DAAB.DAABRequest.Parameter = { _
                                New DAAB.DAABRequest.Parameter("P_ID_TI_VENTAS_FACT", DbType.String, 20, ID, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, arr(0), ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_FACTURA_FICTICIA", DbType.String, 10, arr(1), ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_REFERENCIA", DbType.String, 16, arr(2), ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_CLASE_FACTURA_COD", DbType.String, 4, arr(3), ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_MEDIO_PAGO", DbType.String, 4, arr(4), ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_REF_NC", DbType.String, 16, String.Empty, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_MONTO", DbType.Decimal, 20, Convert.ToDecimal(arr(5)), ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 2, estado, ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_USUARIO_CREACION", DbType.String, 10, strUserIDBD.ToUpper.Trim(), ParameterDirection.Input), _
                                New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 500, ParameterDirection.Output)}
                            objRequest2.CommandType = CommandType.StoredProcedure
                            objRequest2.Command = pkgNameOffSAP & ".MIG_RegVentasFactMedioCua"
                            objRequest2.Parameters.AddRange(arrParameters)
                            objRequest2.Factory.ExecuteNonQuery(objRequest2)

                            strMensajeError = CType(objRequest2.Parameters(10), IDataParameter).Value.ToString

                            If strMensajeError.Length <> 0 Then
                                Exit For
                            End If
                        End If
                    Next
                End If
            End If

            Dim tabla As New DataTable
            tabla.Columns.Add("LOG", GetType(String))
            Dim row As DataRow
            row = tabla.NewRow
            row.Item("LOG") = strMensajeError
            tabla.Rows.Add(row)
            dtSet.Tables.Add(tabla)

        Catch ex As Exception
            Dim tabla As New DataTable
            tabla.Columns.Add("LOG", GetType(String))
            Dim row As DataRow
            row = tabla.NewRow
            row.Item("LOG") = ex.Message
            tabla.Rows.Add(row)
            dtSet.Tables.Add(tabla)
        End Try

        Return dtSet
    End Function

    Public Function VerificarOficinaCerrada(ByVal codigoOficina As String, ByVal codigoUsuario As String, ByVal fecha As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 8, fecha, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, codigoUsuario, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, codigoOficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_MENSAJE", DbType.String, 50, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_VerificarOficinaCerrada"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteDataset(objRequest)
        Return Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
    End Function

    Public Function ObtenerCajaAsignadaCuadreGeneral(ByVal codigoOficina As String, ByVal codigoUsuario As String, ByVal fecha As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 8, fecha, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, codigoUsuario, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, codigoOficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_CAJA_ASIG", DbType.String, 20, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetCajaAsignadaCuadGen"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteDataset(objRequest)
        Return Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
    End Function

    Public Function ObtenerCajaAsignadaCuadreIndividual(ByVal codigoOficina As String, ByVal codigoUsuario As String, ByVal fecha As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, codigoUsuario, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, codigoOficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_CAJA_ASIG", DbType.String, 20, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetCajaAsignadaCuadInd"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteDataset(objRequest)
        Return Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
    End Function

    Public Function GetDatosCuadre(ByVal oficinaVta As String, ByVal fecha As String, ByVal cajero As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 8, fecha, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficinaVta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_DET_VOU", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_DET_MOV", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_DET_DTH", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_DET_FIJ", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_DET_PAG", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_DET_DAC", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_DET_DRA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_DET_GAR", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetDatosCuadre"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function

    Public Function GetCajaBuzonAnterior(ByVal oficina As String, ByVal fecha As String, _
                                        ByRef fechaAnterior As String, ByRef buzonAnteriorPendiente As Double, _
                                        ByRef remesaAnteriorEnviadaHoy As Double, ByRef dMtoCajaBuzonAnteriorNoEnviadoAyer As Double) As Double
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                              New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_FECANT", DbType.String, 10, "", ParameterDirection.Output), _
                                                              New DAAB.DAABRequest.Parameter("P_MTOANT", DbType.Decimal, 15, 0, ParameterDirection.Output), _
                                                              New DAAB.DAABRequest.Parameter("P_MTOANTPEN", DbType.Decimal, 15, 0, ParameterDirection.Output), _
                                                              New DAAB.DAABRequest.Parameter("P_MTOREMANT", DbType.Decimal, 15, 0, ParameterDirection.Output), _
                                                              New DAAB.DAABRequest.Parameter("P_BUZANTNOENVAYER", DbType.Decimal, 15, 0, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = pkgNameOffSAP & ".mig_getmontocajabuzonanterior"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
            fechaAnterior = CType(objRequest.Parameters(2), IDataParameter).Value
            buzonAnteriorPendiente = CType(objRequest.Parameters(4), IDataParameter).Value
            remesaAnteriorEnviadaHoy = CType(objRequest.Parameters(5), IDataParameter).Value
            dMtoCajaBuzonAnteriorNoEnviadoAyer = CType(objRequest.Parameters(6), IDataParameter).Value
        Catch ex As Exception
            GetCajaBuzonAnterior = 0
            fechaAnterior = ex.Message & "Datos: " & oficina & " - " & fecha
        End Try
        GetCajaBuzonAnterior = CType(objRequest.Parameters(3), IDataParameter).Value
    End Function

    Public Function GetMontoCuadre(ByVal oficinaVta As String, ByVal fecha As String, ByVal cajero As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 8, fecha, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficinaVta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetMontosCuadre"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function

    Public Function VerificarCuadreGeneralRealizado(ByVal codigoOficina As String, ByVal fecha As String) As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, codigoOficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 8, fecha, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_MENSAJE", DbType.Int32, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_VerificarCuadreGenCajero"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteDataset(objRequest)
        Return Convert.ToInt32(CType(objRequest.Parameters(2), IDataParameter).Value)
    End Function

    Public Function GetEstructuraCuadre(ByVal stipoCuadre As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TIPO_CUADRE", DbType.String, 1, stipoCuadre, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetEstructuraCaja"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function

    Public Function GetCierreCajaIndividualAnterior(ByVal codigoOficina As String, ByVal caja As String, ByVal fecha As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, codigoOficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_CAJAID", DbType.Int64, CLng(caja), ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_CODIGO", DbType.String, 2, 0, ParameterDirection.Output), _
                                        New DAAB.DAABRequest.Parameter("P_MENSAJE", DbType.String, 1000, 0, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetCierreCajaIndAnterior"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteDataset(objRequest)
        Dim strCodigo As String = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
        Return Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
    End Function

    Public Function GetCierreCajaGeneralAnterior(ByVal codigoOficina As String, ByVal fecha As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, codigoOficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_CODIGO", DbType.String, 2, 0, ParameterDirection.Output), _
                                        New DAAB.DAABRequest.Parameter("P_MENSAJE", DbType.String, 1000, 0, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetCierreCajaGenAnterior"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteDataset(objRequest)
        Dim strCodigo As String = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
        Return Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
    End Function

    Public Function GetRemesaHoy(ByVal oficina As String, ByVal fecha As String) As Double
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                              New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_MTOREM", DbType.Decimal, 15, 0, ParameterDirection.Output) _
                                                              }
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = pkgNameOffSAP & ".MIG_GetRemesaHoy"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
        Catch ex As Exception
            GetRemesaHoy = 0
        End Try
        GetRemesaHoy = CType(objRequest.Parameters(2), IDataParameter).Value
    End Function

    Public Function GetEfectivo(ByVal oficina As String, ByVal usuario As String, _
                                            ByVal fecha As String) As Double
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                              New DAAB.DAABRequest.Parameter("OFICINA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("USUARIO", DbType.String, 10, usuario, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("MONTOCAJA", DbType.Decimal, 15, 0, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = pkgNameOffSAP & ".MIG_efectivo"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
        Catch ex As Exception
            Return 0
        End Try
        GetEfectivo = IIf(IsDBNull(CType(objRequest.Parameters(3), IDataParameter).Value), 0, CType(objRequest.Parameters(3), IDataParameter).Value)
    End Function

    Public Function GetFechaOficinaSinergia(ByVal oficina As String, ByRef flag As String) As String
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                              New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, "", ParameterDirection.Output), _
                                                              New DAAB.DAABRequest.Parameter("P_FLAG", DbType.String, 20, "", ParameterDirection.Output) _
                                                              }
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = pkgNameOffSAP & ".MIG_ObtenerFechaCac"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
        Catch ex As Exception
            GetFechaOficinaSinergia = ex.Message()
        End Try
        GetFechaOficinaSinergia = CType(objRequest.Parameters(1), IDataParameter).Value
        flag = CType(objRequest.Parameters(2), IDataParameter).Value
    End Function

    Public Function DeshacerCambiosFlujoCaja(ByVal ID_TI_VENTAS_FACT As Int32, ByVal sMensaje As String) As Boolean
        Dim sResult As Boolean = False
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                            New DAAB.DAABRequest.Parameter("P_ID_VENTA_FACT", DbType.Int64, ID_TI_VENTAS_FACT, ParameterDirection.Input), _
                                            New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 500, String.Empty, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = pkgNameOffSAP & ".MIG_DeshacerRegCuadre"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteDataset(objRequest)
            sMensaje = CType(objRequest.Parameters(1), IDataParameter).Value
            If sMensaje <> "OK" Then
                sResult = False
            Else
                sResult = True
            End If
        Catch ex As Exception
            sMensaje = ex.Message
        End Try
        Return sResult
    End Function

    Public Function GetCalcularEfeCaja(ByVal oficina As String, ByVal cajero As String, _
                                        ByVal fecha As String) As Double
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                              New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                                              New DAAB.DAABRequest.Parameter("P_MONTO", DbType.Decimal, 15, 0, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = pkgNameOffSAP & ".MIG_CalcularEfeCaja"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
        Catch ex As Exception
            Return 0
        End Try
        GetCalcularEfeCaja = IIf(IsDBNull(CType(objRequest.Parameters(3), IDataParameter).Value), 0, CType(objRequest.Parameters(3), IDataParameter).Value)
    End Function
    'FIN PRE_CUADRE_OFFLINE :: Agregado por TS-CCC

#End Region

#Region "REPORTES"

    Public Function GetFacturacionDet(ByVal oficina As String, ByVal fecha As String, ByVal cajero As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetFacturacionDet"
        objRequest.Parameters.AddRange(arrParam)
        GetFacturacionDet = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function GetFactMaterialDet(ByVal oficina As String, ByVal fecha As String, ByVal cajero As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fecha, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetFactMaterialesDet"
        objRequest.Parameters.AddRange(arrParam)
        GetFactMaterialDet = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function GetVisFacturaDet(ByVal oficinaVenta As String, ByVal SuboficinaVenta As String, ByVal fechaIni As String, _
                                        ByVal fechaFin As String, ByVal tipoDoc As String, _
                                        ByVal docSunat1 As String, ByVal docSunat2 As String, _
                                        ByVal montoPagado1 As String, ByVal montoPagado2 As String, _
                                        ByVal cajero As String, ByVal vendedor As String, _
                                        ByVal viaPago As String, ByVal cuotas As String, _
                                        ByVal estado As String, ByVal cntRegistros As String, ByRef strRespuesta As String) As DataSet

        Try
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 1000, oficinaVenta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_SUB_OFICINA", DbType.String, 1000, SuboficinaVenta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_INI", DbType.String, 10, fechaIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_FIN", DbType.String, 10, fechaFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_TIPO_DOC", DbType.String, 4, tipoDoc, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DOCSUN_1", DbType.String, 20, docSunat1, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DOCSUN_2", DbType.String, 20, docSunat2, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_TOTFAC_1", DbType.String, 20, montoPagado1, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_TOTFAC_2", DbType.String, 20, montoPagado2, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_VENDEDOR", DbType.String, 10, vendedor, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_VIA_PAGO", DbType.String, 4, viaPago, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NRO_CUO", DbType.String, 2, cuotas, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_EST_DOC", DbType.String, 2, estado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CNTREG", DbType.String, 6, cntRegistros, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetFactDetalle"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)

        strRespuesta = "OK"

        GetVisFacturaDet = ds
        Catch ex As Exception

            strRespuesta = ex.Message.ToString()

            GetVisFacturaDet = Nothing
        End Try

    End Function


#End Region

    Public Function Get_CajeroVirtual(ByVal strCodPDV As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_CODPV", DbType.String, 4, strCodPDV, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_DATOS", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, 200, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, 200, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = pkgNameOffSAP & ".CONF_Caj_VirtualPorPDV"
            objRequest.Parameters.AddRange(arrParam)
            Get_CajeroVirtual = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: Obtener Cajero Virtual  - " & ex.Message.ToString()
            Get_CajeroVirtual = Nothing
        End Try

    End Function

'PROY-140662 INICIO
    Public Function Get_MantCajeroVirtual(ByVal strCodPDV As String, ByVal strCodCajero As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet


        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_CODPV", DbType.String, strCodPDV, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_CODCAJERO", DbType.String, strCodCajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_DATOS", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, 200, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, 200, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = pkgNameOffSAP & ".CONF_Mant_Caj_VirtualPorPDV"
            objRequest.Parameters.AddRange(arrParam)
            Get_MantCajeroVirtual = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: Obtener Cajero Virtual  - " & ex.Message.ToString()
            Get_MantCajeroVirtual = Nothing
        End Try

    End Function

    Public Function Get_MantCajeroVirtualPorID(ByVal strID As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_ID", DbType.String, strID, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_DATOS", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, 200, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, 200, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "USRSICAR.PCK_SICAR_OFF_SAP.CONF_Mant_Caj_VirtualPorID" 'pkgNameOffSAP & ".CONF_Mant_Caj_VirtualPorID"
            objRequest.Parameters.AddRange(arrParam)
            Get_MantCajeroVirtualPorID = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: Obtener Cajero Virtual  - " & ex.Message.ToString()
            Get_MantCajeroVirtualPorID = Nothing
        End Try

    End Function

    Public Function Get_Registro_MantCajeroVirtual(ByVal strID As String, ByVal strCodPDV As String, ByVal strCodCajero As String, ByVal strCodCaja As String, ByVal strUsuario As String, ByVal strEstado As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_ID", DbType.String, strID, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_COD_PDV", DbType.String, strCodPDV, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_ID_VENDEDOR", DbType.String, strCodCajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_ID_CAJA_OFI", DbType.String, strCodCaja, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_USUARIO_REG", DbType.String, strUsuario, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_ESTADO", DbType.String, strEstado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "USRSICAR.PCK_SICAR_OFF_SAP.CONF_Mant_Registro_Caj_Virtual" 'pkgNameOffSAP & ".CONF_Mant_Registro_Caj_Virtual"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Transactional = True
            objRequest.Factory.ExecuteNonQuery(objRequest)
            Get_Registro_MantCajeroVirtual = True

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(6), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(7), IDataParameter).Value)

            If strCodRpta = "0" Then
                objRequest.Factory.CommitTransaction()
            End If

        Catch ex As Exception
            objRequest.Factory.RollBackTransaction()
            strCodRpta = "-99"
            strMsgRpta = "Error: Obtener Cajero Virtual  - " & ex.Message.ToString()
            Get_Registro_MantCajeroVirtual = False
        End Try

    End Function
'PROY-140662 FIN

End Class