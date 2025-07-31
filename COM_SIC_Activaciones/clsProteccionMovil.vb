Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB

'PROY-24724-IDEA-28174 - INI
Public Class clsProteccionMovil

    Dim objSeg As New Seguridad_NET.clsSeguridad
    Dim strCadenaConexion As String
    Dim strPKGProteccionMovil As String = "PKG_TRANS_ASURION."

    Public Function ProcesarPagoProteccionMovil(ByVal strSEC As String, ByVal strTelefono As String, ByVal strCertificado As String, _
                                                ByVal strUsuario As String, ByVal strIP As String, ByVal strEstado As String, _
                                                ByVal strIdCanje As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_NRO_SEC", DbType.String, strSEC, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NRO_TELEFONO", DbType.String, strTelefono, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NRO_CERTIFICADO", DbType.String, strCertificado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_USR_APLICACION", DbType.String, strUsuario, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_IP", DbType.String, strIP, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_ESTADO_RPTA_SERVICIO", DbType.String, strEstado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_ID_CANJE", DbType.String, strIdCanje, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DATA_CURSOR", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_MSJ_RESULTADO", DbType.String, 200, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPKGProteccionMovil & "SISACTSI_ALTA_ASURION"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
            strCodRpta = Convert.ToString(CType(objRequest.Parameters(8), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(9), IDataParameter).Value)
        Catch ex As Exception
            strCodRpta = "-1"
            strMsgRpta = "Error: Procesar Pago Proteccion Movil - " & ex.Message.ToString()
        End Try
    End Function

    Public Function ConsultarProteccionMovil(ByVal strNroPedido As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_NRO_PEDIDO", DbType.String, strNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DATOS_EVAL", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_MSJ_RESULTADO", DbType.String, 200, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPKGProteccionMovil & "SISACTSS_CONSULTAR_SEGURO"
            objRequest.Parameters.AddRange(arrParam)
            ConsultarProteccionMovil = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            ConsultarProteccionMovil = Nothing
            strCodRpta = "-99"
            strMsgRpta = "Error: Consultar Datos Proteccion Movil - " & ex.Message.ToString()
        End Try

    End Function

    Public Function EliminarServicioProteccionMovil(ByVal intNroSEC As Integer, ByVal strCodServicio As String, _
                                                    ByRef strCodRpta As String, ByRef strMsgRpta As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim SplnCodigo As Int64 = 0
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_COD_SEC", DbType.Int64, intNroSEC, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_COD_SERV", DbType.String, strCodServicio, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_SLPLN_CODIGO", DbType.Int64, SplnCodigo, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_MSJ_RESULTADO", DbType.String, 400, strCodRpta, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPKGProteccionMovil & "SISACTSD_BORRAR_SERV_VENTA"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)
            strCodRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
        Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: Eliminar Servicio Proteccion Movil - " & ex.Message.ToString()
        End Try
    End Function

    Public Function EliminarEvaluacionProteccionMovil(ByVal strNroSEC As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CERTIF_TEMP", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NRO_SEC", DbType.String, strNroSEC, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_MSJ_RESULTADO", DbType.String, 200, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPKGProteccionMovil & "SISACTSU_ELIMINA_EVAL_SEGURO"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

            Return True

        Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: Eliminar Evaluacion Proteccion Movil - " & ex.Message.ToString()
            Return False
        End Try
    End Function

    Public Function ValidaPagoEquipoProteccionMovil(ByVal strNroPedido As String, ByRef strNroPedidoEquipo As String, _
                                                    ByRef strCodRpta As String, ByRef strMsgRpta As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        Dim strCadenaEsquema As String = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODPEDIDO", DbType.Int64, strNroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_CODPEDIDO_EQUIP", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_COD_RPTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_MSGE_RPTA", DbType.String, 200, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_VALIDA_ASURION"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            strNroPedidoEquipo = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            strCodRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            strNroPedidoEquipo = ""
            strCodRpta = "-99"
            strMsgRpta = "Error: Valida Pago Proteccion Movil - " & ex.Message.ToString()
        End Try
    End Function

    Public Function ConsultarPedidoOrigen(ByVal strNroPedidoPM As Int64, ByRef strNroPedidoEquipo As Int64, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataTable

        Dim dtProteccionMovil As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        Dim strCadenaEsquema As String = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_NROPEDIDO_PM", DbType.Int64, strNroPedidoPM, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NROPEDIDO_EQUIPO", DbType.Int64, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, 200, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_CONSULTAPEDIDOORIGEN"
            objRequest.Parameters.AddRange(arrParam)
            dtProteccionMovil = objRequest.Factory.ExecuteDataset(objRequest)

            If Not dtProteccionMovil Is Nothing AndAlso dtProteccionMovil.Tables.Count > 0 Then
                ConsultarPedidoOrigen = dtProteccionMovil.Tables(0)
            Else
                ConsultarPedidoOrigen = Nothing
            End If

            strNroPedidoEquipo = Funciones.CheckStr(CType(objRequest.Parameters(1), IDataParameter).Value)
            strCodRpta = Funciones.CheckStr(CType(objRequest.Parameters(3), IDataParameter).Value)
            strMsgRpta = Funciones.CheckStr(CType(objRequest.Parameters(4), IDataParameter).Value)

        Catch ex As Exception
            ConsultarPedidoOrigen = Nothing
            strCodRpta = "-99"
            strMsgRpta = "Error: ConsultarPedidoOrigen -> " & ex.Message.ToString()
        End Try

    End Function
    Public Function ConsultarSeguroPlan(ByVal strNroSEC As String, ByVal strFlagVI As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataTable

        Dim dtProteccionMovil As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_NRO_SEC", DbType.String, strNroSEC, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_FLAG_VI", DbType.String, strFlagVI, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DATOS_EVAL", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_MSJ_RESULTADO", DbType.String, 200, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPKGProteccionMovil & "SISASS_CONSULTAR_SEGURO_PLAN"
            objRequest.Parameters.AddRange(arrParam)
            dtProteccionMovil = objRequest.Factory.ExecuteDataset(objRequest)

            If Not dtProteccionMovil Is Nothing Then
                ConsultarSeguroPlan = dtProteccionMovil.Tables(0)
            Else
                ConsultarSeguroPlan = Nothing
            End If

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)

        Catch ex As Exception
            ConsultarSeguroPlan = Nothing
            strCodRpta = "-99"
            strMsgRpta = "Error: ConsultarSeguroPlan -> " & ex.Message.ToString()
        End Try

    End Function


    Public Function ObtenerHistoricoEvaluacion(ByVal strNroSEC As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataTable

        Dim dtProteccionMovil As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_NRO_SEC", DbType.String, strNroSEC, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CURSOR_HIST", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_MSJ_RESULTADO", DbType.String, 200, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPKGProteccionMovil & "SISACTSS_HISTORICO_EVAL"
            objRequest.Parameters.AddRange(arrParam)
            dtProteccionMovil = objRequest.Factory.ExecuteDataset(objRequest)


            If Not dtProteccionMovil Is Nothing Then
                ObtenerHistoricoEvaluacion = dtProteccionMovil.Tables(0)
            Else
                ObtenerHistoricoEvaluacion = Nothing
            End If

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            ObtenerHistoricoEvaluacion = Nothing
            strCodRpta = "-99"
            strMsgRpta = "Error: ObtenerHistoricoEvaluacion -> " & ex.Message.ToString()
        End Try

    End Function
    Public Function MostrarDatosProteccionMovil(ByVal strSEC As String, ByVal strSerieEquipoOrigen As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet
        Dim ds As DataSet
        Dim dtProteccionMovil As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_NRO_SEC", DbType.String, strSEC, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_SERIE", DbType.String, strSerieEquipoOrigen, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_CURSOR_DATOS", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_MSJ_RESULTADO", DbType.String, 200, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPKGProteccionMovil & "SISASS_CONSU_EVAL_SEGU"
            objRequest.Parameters.AddRange(arrParam)
            MostrarDatosProteccionMovil = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToInt64(CType(objRequest.Parameters(3), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)

        Catch ex As Exception
            MostrarDatosProteccionMovil = Nothing
            strCodRpta = "-99"
            strMsgRpta = "Error: MostrarDatosProteccionMovil- " & ex.Message.ToString()
        End Try
    End Function

    Public Function AnularCanjeProteccionMovil(ByVal strSEC As String, ByVal strSerieEquipoOri As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_NRO_SEC", DbType.String, strSEC, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NRO_IMEI", DbType.String, strSerieEquipoOri, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_MSJ_RESULTADO", DbType.String, 200, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPKGProteccionMovil & "SISASU_ANULA_CANJE"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: AnularPedidoCanjeProteccionMovil - " & ex.Message.ToString()
        End Try
    End Function
    'PROY-24724-IDEA-28174 - FIN
    'PROY-24724 - Iteracion 2 Siniestros  INI
    Public Function ConsultarCanjedeVentaIndividual(ByVal strNroPedidoPM As String, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet
        Dim ds As DataSet
        Dim dtProteccionMovil As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_NRO_PEDIDO_ORI", DbType.String, "", ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_NRO_PEDIDO_PM", DbType.String, strNroPedidoPM, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_ID_CANJE", DbType.String, "", ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_CURSOR_DATA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, 200, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strPKGProteccionMovil & "SISASS_BUSCR_SEGURO_CANJE"
            objRequest.Parameters.AddRange(arrParam)
            ConsultarCanjedeVentaIndividual = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRpta = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)

        Catch ex As Exception
            ConsultarCanjedeVentaIndividual = Nothing
            strCodRpta = "-99"
            strMsgRpta = "Error: ConsultarCanjedeVentaIndividual- " & ex.Message.ToString()
        End Try
    End Function
    'PROY-24724 - Iteracion 2 Siniestros FIN

    'PROY-24724 = 26-03-2018 - INICIO CONTINGENCIA 
    Public Function AAjustePedidoAsurion(ByVal strNroPedidoPM As Integer, ByRef strCodRpta As String, ByRef strMsgRpta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        Dim strCadenaEsquema As String = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_NRO_PEDIDO", DbType.Int64, strNroPedidoPM, ParameterDirection.Input), _
                                                         New DAAB.DAABRequest.Parameter("K_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                         New DAAB.DAABRequest.Parameter("K_MSJ_RESULTADO", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_VENTA.SSAPSU_CALC_IGV"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteDataset(objRequest)


            strCodRpta = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            strMsgRpta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)

        Catch ex As Exception
            strCodRpta = "-99"
            strMsgRpta = "Error: AjustePedidoAsurion - " & ex.Message.ToString()
        End Try
    End Function
    'PROY-24724 = 26-03-2018 - FIN CONTINGENCIA 




    'PROY-31836 IDEA-43582_Mejora de Procesos Postventa del servicio Proteccion Movil   
    Public Function Consulta_linea_robo(ByVal Numtelefono As String, ByRef cod_msj As String, ByRef msj As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SICARBSCS")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_NRO_CERTIF", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NRO_TELEFONO", DbType.String, Numtelefono, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_IMEI_NUEVO", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_ESTADO", DbType.String, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_CURSOR_DATA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("P_MSJ_RESULTADO", DbType.String, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "TIM.PKG_TRANS_ASURION.BSCSSS_OBT_HIST_SINIEST"
            objRequest.Parameters.Clear()
            objRequest.Parameters.AddRange(arrParam)
            Consulta_linea_robo = objRequest.Factory.ExecuteDataset(objRequest)


            cod_msj = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)
            msj = Convert.ToString(CType(objRequest.Parameters(6), IDataParameter).Value)

        Catch ex As Exception
            cod_msj = "-99"
            msj = "Error: Metodo Consulta_linea_robo  - " & ex.Message.ToString()
        End Try
    End Function
    'PROY-31836 IDEA-43582_Mejora de Procesos Postventa del servicio Proteccion Movil  

End Class