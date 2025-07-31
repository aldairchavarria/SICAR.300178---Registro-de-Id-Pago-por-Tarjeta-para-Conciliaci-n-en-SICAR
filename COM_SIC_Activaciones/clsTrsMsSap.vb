Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB

Public Class clsTrsMsSap


    Dim objSeg As New Seguridad_NET.clsSeguridad
    Dim strCadenaConexion As String
    Dim strCadenaEsquema As String = ""

    '*************************************************************************'
    Dim SP_SSAPSI_PEDIDO As String = ".SSAPSI_PEDIDO"
    Dim SP_SSAPSI_DETALLEPEDIDO As String = ".SSAPSI_DETALLEPEDIDO"    '**FLUJO CON LAS RECARGAS VIRTUALES**'
    'Dim SP_SSAPSI_DETALLEPEDIDO As String = ".SSAPSI_DETALLEPEDIDOZ"

    '*************************************************************************'
    Dim SP_SSAPSI_PAGO As String = ".SSAPSI_PAGO"
    Dim SP_SSAPSI_PAGO2 As String = ".SSAPSI_PAGO2"
    Dim SP_SSAPSI_DETALLEPAGO As String = ".SSAPSI_DETALLEPAGO"
    '*************************************************************************'

    Dim SP_SSAPSU_ANULARPEDIDO As String = ".SSAPSU_ANULARPEDIDO"
    Dim SP_SSAPSI_RETURNPEDIDO As String = ".SSAPSI_RETURNPEDIDO"
    Dim SP_SSAPSS_RESERVATEMPORAL As String = ".SSAPSS_RESERVATEMPORAL"

    Dim SP_SSAPSI_PAGO_SRVSICAR As String = ".SSAPSI_PAGO_SRVSICAR"

    '**SE CREO ESTE SP PARA GENERAR EL CORRELATIVO PARA LA FE. 05.05.2015 **'
    Public Function CalculoCorrelativoFE(ByVal K_OFICC_CODIGOOFICINA As String, _
                                            ByVal K_CORRC_TIPODOCUMENTO As String, _
                                            ByVal K_CORRC_TIPODOC_REFERENCIA As String, _
                                            ByRef K_NRO_ERROR As Int16, _
                                            ByRef K_DES_ERROR As String, _
                                            ByRef K_CU_CORRELATIVOFE As String)


        Dim proceso As Int32

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA") '
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_OFICC_CODIGOOFICINA", DbType.String, 10, K_OFICC_CODIGOOFICINA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_CORRC_TIPODOCUMENTO", DbType.String, 2, K_CORRC_TIPODOCUMENTO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_CORRC_TIPODOC_REFERENCIA", DbType.String, K_CORRC_TIPODOC_REFERENCIA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NRO_ERROR", DbType.Int16, K_NRO_ERROR, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DES_ERROR", DbType.String, K_DES_ERROR, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_CU_CORRELATIVOFE", DbType.String, K_CU_CORRELATIVOFE, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_CORRELATIVOFE"       '*** HARDCODE **'
            objRequest.Parameters.AddRange(arrParam)
            proceso = objRequest.Factory.ExecuteNonQuery(objRequest)

            K_NRO_ERROR = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            K_DES_ERROR = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
            K_CU_CORRELATIVOFE = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)

            objRequest = Nothing
            Return proceso
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function CalculoCorrelativoSUNAT(ByVal K_CORRV_CODOFICINAVENTA As String, _
                                               ByVal K_CORRC_CODTIPODOCUMENTO As String, _
                                               ByVal K_PEDIN_NROPEDIDO As Int64, _
                                               ByRef K_PAGOV_CORRELATIVO As String)


        Dim proceso As Int32

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA") '
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym


        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CORRV_CODOFICINAVENTA", DbType.String, 10, K_CORRV_CODOFICINAVENTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_CORRC_CODTIPODOCUMENTO", DbType.String, 4, K_CORRC_CODTIPODOCUMENTO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PAGOV_CORRELATIVO", DbType.String, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_MSSAP.SSAPSS_CORRELATIVO"
            objRequest.Parameters.AddRange(arrParam)
            proceso = objRequest.Factory.ExecuteNonQuery(objRequest)

            K_PAGOV_CORRELATIVO = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

            objRequest = Nothing
            Return proceso
        Catch ex As Exception
            K_PAGOV_CORRELATIVO = ""
            Return 0
        End Try
    End Function

    Public Function DeshacerCambiosPedidoPagado(ByVal K_PAGON_IDPAGO As Int64, _
                                                ByRef K_NROLOG As String, ByRef K_DESLOG As String)
        Dim strResultado As Integer = 0
        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP")) '
        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PAGON_IDPAGO", DbType.Int64, K_PAGON_IDPAGO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_PAGO.SSAPSD_DELETEPAGO"
            objRequest.Parameters.AddRange(arrParam)
            strResultado = objRequest.Factory.ExecuteNonQuery(objRequest)

            '** Valores para retornar **************************************************************'
            If strResultado <> 0 Then
                K_NROLOG = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
                K_DESLOG = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
                objRequest = Nothing
                Return strResultado
            Else
                K_NROLOG = ""
                K_DESLOG = ""
            End If
            '****************************************************************************************'

        Catch ex As Exception
            Return strResultado
        End Try
    End Function

    Public Function ActualizarPagodelPedido(ByVal K_PEDIN_NROPEDIDO As Int64, _
                                ByVal K_PAGON_IDPAGO As Int64, _
                                ByVal K_PAGOC_CODSUNAT As String, _
                                ByVal K_PEDIC_CLASEFACTURA As String, _
                                ByVal K_OFICV_CODOFICINA As String, _
                                ByRef K_NROLOG As String, _
                                ByRef K_DESLOG As String)

        Dim dtSet As New DataTable
        Dim proceso As Int32
        K_NROLOG = ""
        K_DESLOG = ""



        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym


        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PAGON_IDPAGO", DbType.Int64, K_PAGON_IDPAGO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PAGOC_CODSUNAT", DbType.String, K_PAGOC_CODSUNAT, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_CLASEFACTURA", DbType.String, K_PEDIC_CLASEFACTURA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_OFICV_CODOFICINA", DbType.String, K_OFICV_CODOFICINA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            'objRequest.Command ="PKG_MSSAP.SSAPSU_UPDATEPAGO"
            objRequest.Command = strCadenaEsquema & "PKG_PAGO.SSAPSU_UPDATEPAGO"
            objRequest.Parameters.AddRange(arrParam)
            proceso = objRequest.Factory.ExecuteNonQuery(objRequest)

            K_NROLOG = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)
            K_DESLOG = Convert.ToString(CType(objRequest.Parameters(6), IDataParameter).Value)

            objRequest = Nothing
            Return proceso
        Catch ex As Exception
            K_NROLOG = "error-eric"
            K_DESLOG = Funciones.CheckStr(ex.Message.ToString())
            Return Nothing
        End Try
    End Function


    Public Function ActualizarPago(ByVal K_PEDIN_NROPEDIDO As Double, ByVal K_PEDIC_ESTADO As String) As Int32


        Dim dtSet As New DataSet
        Dim procesaPedidos As Int32
        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym


        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Double, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_ESTADO", DbType.String, 3, K_PEDIC_ESTADO, ParameterDirection.Input)}
        objRequest.CommandType = CommandType.StoredProcedure
        ' objRequest.Command = "PKG_MSSAP" & ".SSAPSU_ACTUALIZARPAGO"
        objRequest.Command = strCadenaEsquema & "PKG_PAGO" & ".SSAPSU_ACTUALIZARPAGO"
        objRequest.Parameters.AddRange(arrParam)
        procesaPedidos = objRequest.Factory.ExecuteNonQuery(objRequest)
        Return procesaPedidos

    End Function

    '**SINERGIA 6.0 **'
    Public Function RegistrarPedido(ByVal K_OFICV_CODOFICINA As String, _
                                    ByVal K_INTEV_CODINTERLOCUTOR As Object, _
                                    ByVal K_PEDIC_TIPODOCUMENTO As String, _
                                    ByVal K_PEDIC_ORGVENTA As String, _
                                    ByVal K_PEDIC_CANALVENTA As String, _
                                    ByVal K_PEDIC_SECTOR As String, _
                                    ByVal K_PEDIC_TIPOVENTA As String, _
                                    ByVal K_PEDID_FECHADOCUMENTO As Date, _
                                    ByVal K_PEDIV_MOTIVOPEDIDO As String, _
                                    ByVal K_PEDIC_CLASEFACTURA As String, _
                                    ByVal K_PEDIV_DESCCLASEFACTURA As String, _
                                    ByVal K_PEDIV_DESTINOMERCADERIA As String, _
                                    ByVal K_PEDIC_CLASEPEDIDO As String, _
                                    ByVal K_PEDIC_CODTIPOOPERACION As String, _
                                    ByVal K_PEDIV_DESCTIPOOPERACION As String, _
                                    ByVal K_PEDID_FECHAENTREGA As Date, _
                                    ByVal K_PEDIV_SISTEMAVENTA As String, _
                                    ByVal K_PEDIV_CODVENDEDOR As String, _
                                    ByVal K_PEDIC_ESTADO As String, _
                                    ByVal K_PEDIC_ISRENTA As String, _
                                    ByVal K_PEDIN_PEDIDOALTA As Int64, _
                                    ByVal K_PEDIV_UBIGEO As String, _
                                    ByVal K_PEDIC_ESQUEMACALCULO As String, _
                                    ByVal K_PEDIC_TIPODOCCLIENTE As String, _
                                    ByVal K_PEDIV_NRODOCCLIENTE As String, _
                                    ByVal K_PEDIC_TIPOCLIENTE As String, _
                                    ByVal K_PEDIV_NOMBRECLIENTE As String, _
                                    ByVal K_PEDIV_PATERNOCLIENTE As String, _
                                    ByVal K_PEDIV_MATERNOCLIENTE As String, _
                                    ByVal K_PEDID_NACIMIENTOCLIENTE As Object, _
                                    ByVal K_PEDIV_RAZONSOCIAL As String, _
                                    ByVal K_PEDIV_CORREOCLIENTE As String, _
                                    ByVal K_PEDIV_TELEFONOCLIENTE As String, _
                                    ByVal K_PEDIC_ESTADOCIVILCLIENTE As String, _
                                    ByVal K_PEDIV_DIRECCIONCLIENTE As String, _
                                    ByVal K_PEDIN_NUMEROCALLE As Integer, _
                                    ByVal K_PEDIV_DISTRITOCLIENTE As String, _
                                    ByVal K_PEDIC_CODDPTOCLIENTE As String, _
                                    ByVal K_PEDIV_PAISCLIENTE As String, _
                                    ByVal K_PEDIC_RLTIPODOCUMENTO As String, _
                                    ByVal K_PEDIV_RLNRODOCUMENTO As String, _
                                    ByVal K_PEDIV_RLPATERNOCLIENTE As String, _
                                    ByVal K_PEDIV_RLMATERNOCLIENTE As String, _
                                    ByVal K_PEDIV_RLNOMBRE As String, _
                                    ByVal K_PEDIC_TIPOOFICINA As String, _
                                    ByVal K_PEDIC_ISSISCAD As Object, _
                                    ByVal K_PEDIV_USUARIOCREA As String, _
                                    ByVal K_PEDID_FECHACREA As Date, _
                                    ByVal K_PEDIV_USUARIOMODI As String, _
                                    ByVal K_PEDID_FECHAMODI As Object, _
                                    ByVal K_PEDIC_FLAGLP As String, _
                                    ByRef K_PEDIN_NROPEDIDO As Int64, _
                                    ByRef K_NROLOG As String, _
                                    ByRef K_DESLOG As String)


        '***************************************************'
        Dim resultado As Int32
        '**Salidas inicializadas
        K_NROLOG = ""
        K_DESLOG = ""
        K_PEDIN_NROPEDIDO = 0

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym


        '**asignación de paràmetros: **'
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_OFICV_CODOFICINA", DbType.String, 10, K_OFICV_CODOFICINA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_INTEV_CODINTERLOCUTOR", DbType.String, 10, K_INTEV_CODINTERLOCUTOR, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_TIPODOCUMENTO", DbType.String, 4, K_PEDIC_TIPODOCUMENTO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_ORGVENTA", DbType.String, 4, K_PEDIC_ORGVENTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_CANALVENTA", DbType.String, 2, K_PEDIC_CANALVENTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_SECTOR", DbType.String, 2, K_PEDIC_SECTOR, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_TIPOVENTA", DbType.String, 2, K_PEDIC_TIPOVENTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDID_FECHADOCUMENTO", DbType.Date, K_PEDID_FECHADOCUMENTO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_MOTIVOPEDIDO", DbType.String, 3, K_PEDIV_MOTIVOPEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_CLASEFACTURA", DbType.String, 4, K_PEDIC_CLASEFACTURA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_DESCCLASEFACTURA", DbType.String, 60, K_PEDIV_DESCCLASEFACTURA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_DESTINOMERCADERIA", DbType.String, 200, K_PEDIV_DESTINOMERCADERIA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_CLASEPEDIDO", DbType.String, 2, K_PEDIC_CLASEPEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_CODTIPOOPERACION", DbType.String, 2, K_PEDIC_CODTIPOOPERACION, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_DESCTIPOOPERACION", DbType.String, 50, K_PEDIV_DESCTIPOOPERACION, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDID_FECHAENTREGA", DbType.Date, K_PEDID_FECHAENTREGA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_SISTEMAVENTA", DbType.String, 20, K_PEDIV_SISTEMAVENTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_CODVENDEDOR", DbType.String, 10, K_PEDIV_CODVENDEDOR, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_ESTADO", DbType.String, 3, K_PEDIC_ESTADO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_ISRENTA", DbType.String, 1, K_PEDIC_ISRENTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIN_PEDIDOALTA", DbType.Int64, K_PEDIN_PEDIDOALTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_UBIGEO", DbType.String, 20, K_PEDIV_UBIGEO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_ESQUEMACALCULO", DbType.String, 6, K_PEDIC_ESQUEMACALCULO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_TIPODOCCLIENTE", DbType.String, 2, K_PEDIC_TIPODOCCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_NRODOCCLIENTE", DbType.String, 16, K_PEDIV_NRODOCCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_TIPOCLIENTE", DbType.String, 2, K_PEDIC_TIPOCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_NOMBRECLIENTE", DbType.String, 50, K_PEDIV_NOMBRECLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_PATERNOCLIENTE", DbType.String, 50, K_PEDIV_PATERNOCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_MATERNOCLIENTE", DbType.String, 50, K_PEDIV_MATERNOCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDID_NACIMIENTOCLIENTE", DbType.Date, K_PEDID_NACIMIENTOCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_RAZONSOCIAL", DbType.String, 50, K_PEDIV_RAZONSOCIAL, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_CORREOCLIENTE", DbType.String, K_PEDIV_CORREOCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_TELEFONOCLIENTE", DbType.String, 15, K_PEDIV_TELEFONOCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_ESTADOCIVILCLIENTE", DbType.String, 3, K_PEDIC_ESTADOCIVILCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_DIRECCIONCLIENTE", DbType.String, 100, K_PEDIV_DIRECCIONCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIN_NUMEROCALLE", DbType.Int64, K_PEDIN_NUMEROCALLE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_DISTRITOCLIENTE", DbType.String, 20, K_PEDIV_DISTRITOCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_CODDPTOCLIENTE", DbType.String, 2, K_PEDIC_CODDPTOCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_PAISCLIENTE", DbType.String, 20, K_PEDIV_PAISCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_RLTIPODOCUMENTO", DbType.String, 2, K_PEDIC_RLTIPODOCUMENTO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_RLNRODOCUMENTO", DbType.String, 16, K_PEDIV_RLNRODOCUMENTO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_RLPATERNOCLIENTE", DbType.String, 50, K_PEDIV_RLPATERNOCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_RLMATERNOCLIENTE", DbType.String, 50, K_PEDIV_RLMATERNOCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_RLNOMBRE", DbType.String, 50, K_PEDIV_RLNOMBRE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_TIPOOFICINA", DbType.String, 2, K_PEDIC_TIPOOFICINA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_ISSISCAD", DbType.String, 1, K_PEDIC_ISSISCAD, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_USUARIOCREA", DbType.String, 10, K_PEDIV_USUARIOCREA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDID_FECHACREA", DbType.Date, K_PEDID_FECHACREA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_USUARIOMODI", DbType.String, 10, K_PEDIV_USUARIOMODI, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDID_FECHAMODI", DbType.Date, K_PEDID_FECHAMODI, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_FLAGLP", DbType.String, 1, K_PEDIC_FLAGLP, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            'objRequest.Command = "PKG_MSSAP.SSAPSI_PEDIDO"
            objRequest.Command = strCadenaEsquema & "PKG_VENTA.SSAPSI_PEDIDO"

            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteNonQuery(objRequest)

            K_PEDIN_NROPEDIDO = Convert.ToInt64(CType(objRequest.Parameters(51), IDataParameter).Value)
            K_NROLOG = Convert.ToString(CType(objRequest.Parameters(52), IDataParameter).Value)
            K_DESLOG = Convert.ToString(CType(objRequest.Parameters(53), IDataParameter).Value)
            objRequest = Nothing

            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    Public Function RegistraDetallePedido(ByVal K_PEDIN_NROPEDIDO As Int64, _
                                        ByVal K_OFICV_CODOFICINA As String, _
                                        ByVal K_INTEV_CODINTERLOCUTOR As String, _
                                        ByVal K_SERIC_CODSERIE As String, _
                                        ByVal K_DEPEC_CODMATERIAL As String, _
                                        ByVal K_DEPEV_DESCMATERIAL As String, _
                                        ByVal K_DEPEN_CANTIDAD As Int64, _
                                        ByVal K_DEPEN_PRECIOVENTA As Double, _
                                        ByVal K_DEPEV_NROTELEFONO As String, _
                                        ByVal K_DEPEV_NROCLARIFY As String, _
                                        ByVal K_DEPEN_NRORENTA As Int64, _
                                        ByVal K_DEPEN_TOTALRENTA As Double, _
                                        ByVal K_DEPEN_NROCUOTA As Int64, _
                                        ByVal K_DEPEV_CODIGOLP As String, _
                                        ByVal K_DEPEV_DESCRIPCIONLP As String, _
                                        ByVal K_DEPEV_USUARIOCREA As String, _
                                        ByVal K_DEPED_FECHACREA As Date, _
                                        ByVal K_DEPEV_USUARIOMODI As String, _
                                        ByVal K_DEPED_FECHAMODI As Date, _
                                        ByRef K_NROLOG As String, _
                                        ByRef K_DESLOG As String)


        '***************************************************'
        Dim resultado As Int32
        '**Salidas inicializadas
        K_NROLOG = ""
        K_DESLOG = ""

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym

        '**asignación de paràmetros: **'
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_OFICV_CODOFICINA", DbType.String, 10, K_OFICV_CODOFICINA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_INTEV_CODINTERLOCUTOR", DbType.String, 10, K_INTEV_CODINTERLOCUTOR, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_SERIC_CODSERIE", DbType.String, 18, K_SERIC_CODSERIE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEC_CODMATERIAL", DbType.String, 18, K_DEPEC_CODMATERIAL, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEV_DESCMATERIAL", DbType.String, 100, K_DEPEV_DESCMATERIAL, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEN_CANTIDAD", DbType.Int64, K_DEPEN_CANTIDAD, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEN_PRECIOVENTA", DbType.Double, K_DEPEN_PRECIOVENTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEV_NROTELEFONO", DbType.String, 20, K_DEPEV_NROTELEFONO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEV_NROCLARIFY", DbType.String, 20, K_DEPEV_NROCLARIFY, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEN_NRORENTA", DbType.Int64, K_DEPEN_NRORENTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEN_TOTALRENTA", DbType.Double, K_DEPEN_TOTALRENTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEN_NROCUOTA", DbType.Int64, K_DEPEN_NROCUOTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEV_CODIGOLP", DbType.String, 10, K_DEPEV_CODIGOLP, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEV_DESCRIPCIONLP", DbType.String, 200, K_DEPEV_DESCRIPCIONLP, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEV_USUARIOCREA", DbType.String, 10, K_DEPEV_USUARIOCREA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPED_FECHACREA", DbType.Date, K_DEPED_FECHACREA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEV_USUARIOMODI", DbType.String, 10, K_DEPEV_USUARIOMODI, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPED_FECHAMODI", DbType.Date, K_DEPED_FECHAMODI, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, 100, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, 100, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEC_CENTROCOSTO", DbType.String, DBNull.Value, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            ' objRequest.Command = "PKG_MSSAP" & SP_SSAPSI_DETALLEPEDIDO
            objRequest.Command = strCadenaEsquema & "PKG_VENTA" & SP_SSAPSI_DETALLEPEDIDO
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteNonQuery(objRequest)

            '**Retornamos los valores - " " **'
            K_NROLOG = Convert.ToString(CType(objRequest.Parameters(19), IDataParameter).Value)
            K_DESLOG = Convert.ToString(CType(objRequest.Parameters(20), IDataParameter).Value)
            objRequest = Nothing

            '**Retornamos el resultado de la ejecuciòn **'
            Return resultado
        Catch ex As Exception
            K_DESLOG = "Ingreso a EX : " & ex.Message.ToString()
            Return Nothing
        End Try

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
                                    ByRef K_PAGOC_CORRELATIVO As String)

        Dim dtSet As New DataTable
        Dim procesaPagos As Int32
        K_NROLOG = ""
        K_DESLOG = ""
        K_PAGON_IDPAGO = 0
        K_PAGOC_CORRELATIVO = ""

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym

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
            ' objRequest.Command = "PKG_MSSAP" & SP_SSAPSI_PAGO2
            objRequest.Command = strCadenaEsquema & "PKG_PAGO" & SP_SSAPSI_PAGO2
            objRequest.Parameters.AddRange(arrParam)
            procesaPagos = objRequest.Factory.ExecuteNonQuery(objRequest)

            '  valRerturn = Convert.ToInt32(CType(objRequest.Parameters(2), IDataParameter).Value)
            K_NROLOG = Convert.ToString(CType(objRequest.Parameters(10), IDataParameter).Value)
            K_DESLOG = Convert.ToString(CType(objRequest.Parameters(11), IDataParameter).Value)
            K_PAGON_IDPAGO = Convert.ToInt64(CType(objRequest.Parameters(12), IDataParameter).Value)
            K_PAGOC_CORRELATIVO = Convert.ToString(CType(objRequest.Parameters(13), IDataParameter).Value)

            objRequest = Nothing
            Return procesaPagos
        Catch ex As Exception
            Return -1
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
                                            ByRef K_DESLOG As String)
        Dim dtSet As New DataSet
        Dim procesaDetallePagos As Int32

        K_NROLOG = ""
        K_DESLOG = ""

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym


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
            '  objRequest.Command = "PKG_MSSAP" & SP_SSAPSI_DETALLEPAGO
            objRequest.Command = strCadenaEsquema & "PKG_PAGO" & SP_SSAPSI_DETALLEPAGO

            objRequest.Parameters.AddRange(arrParam)

            procesaDetallePagos = objRequest.Factory.ExecuteNonQuery(objRequest)

            K_NROLOG = Convert.ToString(CType(objRequest.Parameters(11), IDataParameter).Value)
            K_DESLOG = Convert.ToString(CType(objRequest.Parameters(12), IDataParameter).Value)

            objRequest = Nothing
            Return procesaDetallePagos
        Catch ex As Exception
            procesaDetallePagos = -1
        End Try

    End Function

    ''SP: SSAPSU_ANULARPEDIDO
    Public Function AnularPedido(ByVal K_PEDIN_NROPEDIDO As Int64, _
                                    ByVal K_PEDIV_MOTIVOPEDIDO As String, _
                                        ByVal K_PEDIC_ESTADO As String, _
                                        ByVal K_PEDIC_TIPOOFICINA As String)
        Dim dtSet As New DataSet
        Dim procesaPedidos As Int32 = 0
        
        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIV_MOTIVOPEDIDO", DbType.String, 3, K_PEDIV_MOTIVOPEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_ESTADO", DbType.String, 3, K_PEDIC_ESTADO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_TIPOOFICINA", DbType.String, 2, K_PEDIC_TIPOOFICINA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, 2, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, 2, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            ' objRequest.Command = "PKG_MSSAP" & SP_SSAPSU_ANULARPEDIDO
            objRequest.Command = strCadenaEsquema & "PKG_VENTA" & SP_SSAPSU_ANULARPEDIDO
            objRequest.Parameters.AddRange(arrParam)
            procesaPedidos = objRequest.Factory.ExecuteNonQuery(objRequest)
            Return procesaPedidos
        Catch ex As Exception
            Return -1
        End Try
    End Function

    ''SP: SSAPSI_RETURNPEDIDO
    Public Function ActualizarPedido(ByVal K_PEDIN_NROPEDIDO As Double, ByVal K_REPEC_TYPE As String, ByVal K_REPEV_MESSAGE As String, ByVal K_REPEV_LOGNO As String, _
                                     ByVal K_REPEV_LOGMSG As String, ByVal K_REPEV_MENSAJEV1 As String, ByVal K_REPEV_MENSAJEV2 As String, ByVal K_REPEV_MENSAJEV3 As String, _
                                     ByVal K_REPEV_USUARIOCREA As String, ByVal K_REPEV_USUARIOMODI As String, ByVal K_REPED_FECHAMODI As String, ByVal K_REPED_FECHACREA As String)
        Dim dtSet As New DataSet
        Dim procesaPedidos As Int32
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Double, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_REPEC_TYPE", DbType.String, 1, K_REPEC_TYPE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_REPEV_MESSAGE", DbType.String, 100, K_REPEV_MESSAGE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_REPEV_LOGNO", DbType.String, 10, K_REPEV_LOGNO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_REPEV_LOGMSG", DbType.String, 100, K_REPEV_LOGMSG, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_REPEV_MENSAJEV1", DbType.String, 100, K_REPEV_MENSAJEV1, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_REPEV_MENSAJEV2", DbType.String, 100, K_REPEV_MENSAJEV2, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_REPEV_MENSAJEV3", DbType.String, 100, K_REPEV_MENSAJEV3, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_REPEV_USUARIOCREA", DbType.String, 10, K_REPEV_USUARIOCREA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_REPED_FECHACREA", DbType.Date, K_REPED_FECHACREA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_REPEV_USUARIOMODI", DbType.String, 20, K_REPEV_USUARIOMODI, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_REPED_FECHAMODI", DbType.Date, K_REPED_FECHAMODI, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_MSSAP" & SP_SSAPSI_RETURNPEDIDO
        objRequest.Parameters.AddRange(arrParam)
        procesaPedidos = objRequest.Factory.ExecuteNonQuery(objRequest)
        Return procesaPedidos

    End Function

    ''SP: SSAPSS_RESERVATEMPORAL
    Public Function ReservarSerie(ByVal K_SERIC_CODSERIE As String)
        Dim dtSet As New DataSet
        Dim procesaPedidos As Int32
        strCadenaConexion$ = objSeg.FP_GetConnectionString("2", "SISCAJA") 'strCadenaConexion$ = "user id=usrbdgcarpetas;data source=timdev;password=clarocarpetas"
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_SERIC_CODSERIE", DbType.String, 18, K_SERIC_CODSERIE, ParameterDirection.Input)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_MSSAP" & SP_SSAPSS_RESERVATEMPORAL
        objRequest.Parameters.AddRange(arrParam)
        procesaPedidos = objRequest.Factory.ExecuteNonQuery(objRequest)
        Return procesaPedidos


    End Function

    Public Function Set_HashCode(ByVal sociedad As String, ByVal tipDocSunat As String, _
                           ByVal origen As String, ByVal serieSunat As String, _
                           ByVal correlativoSunat As Integer, ByVal hashCode As String, _
                           ByVal enviado As String, ByVal email As String, ByVal tipOperacion As String, _
                           ByVal codEstablecimiento As String) As DataSet 'MOD: PROY 32815 - RMZ

        Try
            'strCadenaConexion = objSeg.FP_GetConnectionString("2", "BD_MSSAP")
            strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
            strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
            If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
                strCadenaEsquema = strCadenaEsquema & "."
            Else
                strCadenaEsquema = String.Empty
            End If

            '**Verificando los paràmetros: 
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_SOCIEDAD", DbType.String, sociedad, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_FAELC_TIPODOC", DbType.String, tipDocSunat, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_ORIGEN", DbType.String, origen, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_FAELC_SERIE", DbType.String, serieSunat, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_FAELN_CORRELATIVO", DbType.Int64, correlativoSunat, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_FAELC_HASHCODE", DbType.String, hashCode, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_FAELC_PAPERSTATE", DbType.String, enviado, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_FAELC_E_MAIL", DbType.String, email, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_FAELC_CODESTBL", DbType.String, codEstablecimiento, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_FAELC_TIPOPERACION", DbType.String, tipOperacion, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_NRO_ERROR", DbType.String, ParameterDirection.Output), _
            New DAAB.DAABRequest.Parameter("K_DES_ERROR", DbType.String, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            'objRequest.Command = "PKG_PAGO" & SSAPSU_UPDATEHASHCODE
            objRequest.Command = strCadenaEsquema & "PKG_VENTA.SSAPSU_UPDATEHASHCODE"
            objRequest.Parameters.AddRange(arrParam)

            Set_HashCode = objRequest.Factory.ExecuteDataset(objRequest)
            Dim NumError = CType(objRequest.Parameters(8), IDataParameter).Value
            Dim DescrError = CType(objRequest.Parameters(9), IDataParameter).Value

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function FP_Grabar_PagoBF(ByVal NumFactSap As String, ByVal TipoDoc As String, ByVal SerieSunat As String, ByVal CorrSunat As String, ByVal CodUsuario As String, ByVal Origen As String, ByVal cod_pdv As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("NUM_FACT_SAP", DbType.String, NumFactSap, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("TIPO_DOCU", DbType.String, TipoDoc, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("SERIE_SUNAT", DbType.String, SerieSunat, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("CORR_SUNAT", DbType.String, CorrSunat, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("COD_USUARIO", DbType.String, CodUsuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("ORIGEN", DbType.String, Origen, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("COD_PV", DbType.String, cod_pdv, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("COD_RPTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("MENSAJE_RPTA", DbType.String, ParameterDirection.Output) _
}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISCAJ.SP_SET_CORRELATIVO_BF"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        Dim Num_Error = CType(objRequest.Parameters(7), IDataParameter).Value
        Dim Descr_Error = CType(objRequest.Parameters(8), IDataParameter).Value

        objRequest.Parameters.Clear()
        objRequest.Factory.Dispose()

        FP_Grabar_PagoBF = Num_Error & "-" & Descr_Error

    End Function
    '******ESTADO FLUJO
    'SP:  SSAPSU_ESTADOFLUJODEVOLUCION
    Public Function ActualizaEstadoFlujo(ByVal K_PEDIN_NROPEDIDO As Int64, _
                                        ByVal K_ANUPN_ID As Int64, _
                                        ByVal K_PROCESO As String) As String

        Try

            'strCadenaConexion = objSeg.FP_GetConnectionString("2", "BD_MSSAP")
            strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
            strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
            If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
                strCadenaEsquema = strCadenaEsquema & "."
            Else
                strCadenaEsquema = String.Empty
            End If

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("K_ANUPN_ID", DbType.Int64, K_ANUPN_ID, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("K_PROCESO", DbType.String, K_PROCESO, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_VENTA.SSAPSU_ESTADOFLUJODEVOLUCION"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)

            Dim Num_Error = CType(objRequest.Parameters(3), IDataParameter).Value
            Dim Descr_Error = CType(objRequest.Parameters(4), IDataParameter).Value

            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()

            ActualizaEstadoFlujo = Num_Error & "-" & Descr_Error

        Catch ex As Exception
            Return Nothing
            'Return ex.ToString()
        End Try
    End Function

    '*****ANULAR PAGO
    Public Function AnularPago(ByVal PAGON_IDPAGO As Int32, _
                                ByVal PEDIN_NROPEDIDO As Int32) As DataSet

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

            strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
            If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
                strCadenaEsquema = strCadenaEsquema & "."
            Else
                strCadenaEsquema = String.Empty
            End If

            '**Verificando los paràmetros: 
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PAGON_IDPAGO", DbType.Int32, PAGON_IDPAGO, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int32, PEDIN_NROPEDIDO, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("K_RESULT_SET", DbType.Object, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("K_NROERROR", DbType.String, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("K_DESERROR", DbType.String, ParameterDirection.Output)}

            Dim K_RESULT_SET As DataSet

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_PAGO.SSAPSU_ANULARPAGO"
            objRequest.Parameters.AddRange(arrParam)

            AnularPago = objRequest.Factory.ExecuteDataset(objRequest)
            Dim NumError = CType(objRequest.Parameters(3), IDataParameter).Value
            Dim DescrError = CType(objRequest.Parameters(4), IDataParameter).Value

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
	
	 ''CC- Lisetti Macedo
    Public Function ActualizarDescuentoPedido(ByVal pNropedido As Integer, ByVal pCodEsquema As String, _
                                            ByVal pClaseCondicion As String, _
                                            ByVal ByValpDesctMonto As Decimal, _
                                            ByRef pNrolog As String, _
                                            ByRef pDeslog As String)

        Dim dtSet As New DataTable
        Dim proceso As Int32
        pNrolog = ""
        pDeslog = ""


        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, pNropedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_ESQUC_CODIGOESQUEMA", DbType.String, pCodEsquema, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_CLCOC_CODCLASECONDICION", DbType.String, pClaseCondicion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DESCN_MONTO", DbType.Decimal, ByValpDesctMonto, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_VENTA.SSAPSU_DESCUENTO"
            objRequest.Parameters.AddRange(arrParam)
            proceso = objRequest.Factory.ExecuteNonQuery(objRequest)

            pNrolog = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
            pDeslog = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)

            objRequest = Nothing
            Return proceso
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function RecalculaEsquema(ByVal pNropedido As Integer, ByVal pCodConsecutivo As Integer, _
                                         ByVal pEsquemaCalcu As String, _
                                         ByRef pNrolog As String, _
                                         ByRef pDeslog As String)

        Dim dtSet As New DataTable
        Dim proceso As Int32
        pNrolog = ""
        pDeslog = ""


        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, pNropedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_DEPEN_CONSECUTIVO", DbType.Int64, pCodConsecutivo, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_ESQUEMACALCULO", DbType.String, pEsquemaCalcu, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, ParameterDirection.Output)}



        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_VENTA.SSAPSU_RECALCULARESQUEMA"
            objRequest.Parameters.AddRange(arrParam)
            proceso = objRequest.Factory.ExecuteNonQuery(objRequest)

            pNrolog = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            pDeslog = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)

            objRequest = Nothing
            Return proceso
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function RecalculaDescuento(ByVal pNropedido As Integer, ByVal pEsquemaCalcu As String, _
                                               ByRef pNrolog As String, _
                                               ByRef pDeslog As String)

        Dim dtSet As New DataTable
        Dim proceso As Int32
        pNrolog = ""
        pDeslog = ""


        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, pNropedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PEDIC_ESQUEMACALCULO", DbType.String, pEsquemaCalcu, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_VENTA.SSAPSU_RECALCULARDESCUENTO"
            objRequest.Parameters.AddRange(arrParam)
            proceso = objRequest.Factory.ExecuteNonQuery(objRequest)

            pNrolog = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            pDeslog = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

            objRequest = Nothing
            Return proceso
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function ActualizarAjusteRedondeo(ByVal pNropedido As Integer)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, pNropedido, ParameterDirection.Input)}


        Try

            strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
            If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
                strCadenaEsquema = strCadenaEsquema & "."
            Else
                strCadenaEsquema = String.Empty
            End If

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_VENTA.SSAPSU_ACTUALIZARPEDIDO2"
            objRequest.Parameters.AddRange(arrParam)
            'objRequest.Transactional = True

            objRequest.Factory.ExecuteNonQuery(objRequest)
            'objRequest.Factory.CommitTransaction()
            'objRequest = Nothing
        Catch ex As Exception
            'objRequest.Factory.RollBackTransaction()
            'objRequest = Nothing
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function FP_Grabar_Reciclaje(ByVal cod_pdv As String, ByVal TipoDoc As String, ByVal TipoDocRef As String, ByVal CorrSun As String) As String


        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If


        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_OFICC_CODIGOOFICINA", DbType.String, cod_pdv, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_HICOC_TIPODOCUMENTO", DbType.String, TipoDoc, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_HICOI_TIPODOC_REFERENCIA", DbType.String, TipoDocRef, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_HICOV_CORRELATIVOSUNAT", DbType.String, CorrSun, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_NRO_ERROR", DbType.Int16, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("K_DES_ERROR", DbType.String, ParameterDirection.Output) _
}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_VENTA.SSAPSI_RETURNCORRELATIVO"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            Dim NumError = CType(objRequest.Parameters(4), IDataParameter).Value
            Dim DescrError = CType(objRequest.Parameters(5), IDataParameter).Value

            FP_Grabar_Reciclaje = NumError & "-" & DescrError
        Catch ex As Exception

            FP_Grabar_Reciclaje = ""

        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

    End Function

    Public Function ActualizarEstadoNotaCanje(ByVal nroPedido As String, _
                                              ByVal flagCanje As String, _
                                              ByRef NROLOG As String, _
                                              ByRef DESLOG As String) As String


        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If


        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.String, nroPedido, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_PARAMETRO", DbType.String, flagCanje, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_RESPUESTA", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("K_MSJ_RESPUESTA", DbType.String, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CANJE.SSAPSU_UPDATE_DEVOLUCION"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            NROLOG = CType(objRequest.Parameters(2), IDataParameter).Value
            DESLOG = CType(objRequest.Parameters(3), IDataParameter).Value
            ActualizarEstadoNotaCanje = NROLOG & "-" & DESLOG

        Catch ex As Exception
            ActualizarEstadoNotaCanje = ex.Message.ToString()
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
    End Function

    'INICIO CAMBIAR ESTADOS DE SERIES DE BOLETAS POR CANJE
    Public Function ActualizaEstadoSerieCanje(ByVal K_PEDIN_NROPEDIDO As Int64, _
                                              ByVal K_FLAG_ACCION As String, _
                                              ByRef K_COD_RESPUESTA As String, _
                                              ByRef K_MSJ_RESPUESTA As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_FLAG_ACCION", DbType.String, K_FLAG_ACCION, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_COD_RESPUESTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_MSJ_RESPUESTA", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CANJE.SSAPSU_UPDATE_ESTADOSERIECANJE"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            K_COD_RESPUESTA = CType(objRequest.Parameters(2), IDataParameter).Value
            K_MSJ_RESPUESTA = CType(objRequest.Parameters(3), IDataParameter).Value
            ActualizaEstadoSerieCanje = K_COD_RESPUESTA & "-" & K_MSJ_RESPUESTA

        Catch ex As Exception
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Activaciones\clsTrsMsSap.vb; Function: ActualizaEstadoSerieCanje)"
            ActualizaEstadoSerieCanje = "ERROR: " & ex.Message.ToString() & MaptPath
            'FIN PROY-140126

        End Try
    End Function

    Public Sub RegistrarPagoSrvSicar(ByVal K_PEDIN_NROPEDIDO As Int64, _
                                    ByVal K_CODPDV As String, _
                                    ByVal K_UUID As String, _
                                    ByVal K_USUARIO_PAGO As String, _
                                    ByVal K_FECHA_REGISTRO As DateTime, _
                                    ByVal K_ISRENTA As String, _
                                    ByVal K_PEDIDO_ALTA As Int64, _
                                    ByVal K_MONTOCALCULADO As Double, _
                                    ByVal K_MONTOPAGADO As Double, _
                                    ByVal K_TIPODOCVEN As String, _
                                    ByVal K_NRODOCVEN As String, _
                                    ByVal K_NROREFERENCIA As String, _
                                    ByVal K_TRANS_BIOMETRIA As String, _
                                    ByVal K_TRANS_UPDATE_MSSAP As String, _
                                    ByVal K_TRANS_ENVIO_ACTIV_LINEA As String, _
                                    ByVal K_TRANS_UPDATE_CONTRATO As String, _
                                    ByVal K_TRANS_ENVIO_SAP As String, _
                                    ByVal K_TRANS_ENVIO_PPLS As String, _
                                    ByVal K_COD_MEDIOPAGO As String, _
                                    ByVal K_FLAGTIENDA As String, _
                                    ByVal K_TIPOOPERACION As String, _
                                    ByRef K_NROLOG As String, _
                                    ByRef K_DESLOG As String)

        Dim dtSet As New DataTable
        Dim procesaPagos As Int32
        K_NROLOG = String.Empty
        K_DESLOG = String.Empty

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_PEDIN_NRO_PEDIDO", DbType.Int64, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_CODPDV", DbType.String, K_CODPDV, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_UUID", DbType.String, K_UUID, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_USUARIO_PAGO", DbType.String, K_USUARIO_PAGO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_FECHA_REGISTRO", DbType.DateTime, K_FECHA_REGISTRO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_ISRENTA", DbType.String, K_ISRENTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_PEDIDO_ALTA", DbType.Int64, K_PEDIDO_ALTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_MONTOCALCULADO", DbType.Double, K_MONTOCALCULADO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_MONTOPAGADO", DbType.Double, K_MONTOPAGADO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TIPODOCVEN", DbType.String, K_TIPODOCVEN, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_NRODOCVEN", DbType.String, K_NRODOCVEN, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_NROREFERENCIA", DbType.String, K_NROREFERENCIA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRANS_BIOMETRIA", DbType.String, K_TRANS_BIOMETRIA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRANS_UPDATE_MSSAP", DbType.String, K_TRANS_UPDATE_MSSAP, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRANS_ENVIO_ACTIV_LINEA", DbType.String, K_TRANS_ENVIO_ACTIV_LINEA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRANS_UPDATE_CONTRATO", DbType.String, K_TRANS_UPDATE_CONTRATO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRANS_ENVIO_SAP", DbType.String, K_TRANS_ENVIO_SAP, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TRANS_ENVIO_PPLS", DbType.String, K_TRANS_ENVIO_PPLS, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_COD_MEDIOPAGO", DbType.String, K_COD_MEDIOPAGO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_FLAGTIENDA", DbType.String, K_FLAGTIENDA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_TIPOOPERACION", DbType.String, K_TIPOOPERACION, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_PAGO" & SP_SSAPSI_PAGO_SRVSICAR
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            K_NROLOG = Convert.ToString(CType(objRequest.Parameters(21), IDataParameter).Value)
            K_DESLOG = Convert.ToString(CType(objRequest.Parameters(22), IDataParameter).Value)

            objRequest = Nothing
        Catch ex As Exception

            K_NROLOG = "-99"
            K_DESLOG = String.Format("{0} => {1}", "Error: RegistrarPagoSrvSicar", Funciones.CheckStr(ex.Message))
        End Try


    End Sub

    'JRM
    Public Sub RegistrarDetMedPago(ByVal K_PEDIN_NROPEDIDO As Int64, ByVal K_N_IDCAMPO As Int32, ByVal K_V_VALOR As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_N_NROPEDIDO", DbType.Int64, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_N_IDCAMPO", DbType.String, K_N_IDCAMPO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_V_VALOR", DbType.String, K_V_VALOR, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSI_SSAPT_DET_MEDIOSPAGOS"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            objRequest = Nothing
        Catch ex As Exception

        End Try
    End Sub
    'JRM

    'JLOPETAS - PROY 140589 - INI
    Public Sub ConsultaAnulaPedidoDLV(ByVal NroPedido As Int64, ByRef NroPedCostoDlv As String, ByRef isFlagAnulacion As String, ByRef isFlagCostDLV As String, ByRef codRespuesta As String, ByRef msjRespuesta As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_NROPEDIDO", DbType.Int64, NroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_NRO_COSTODLV", DbType.Int64, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_FLAG_ANULACION", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_FLAG_COSTDLV", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_VENTA.SSAPSU_ANUPED_COSTO_DLV"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)


            Dim parSalida1 As IDataParameter
            Dim parSalida2 As IDataParameter
            Dim parSalida3 As IDataParameter
            Dim parSalida4 As IDataParameter
            Dim parSalida5 As IDataParameter

            parSalida1 = CType(objRequest.Parameters(1), IDataParameter)
            parSalida2 = CType(objRequest.Parameters(2), IDataParameter)
            parSalida3 = CType(objRequest.Parameters(3), IDataParameter)
            parSalida4 = CType(objRequest.Parameters(4), IDataParameter)
            parSalida5 = CType(objRequest.Parameters(5), IDataParameter)

            NroPedCostoDlv = Funciones.CheckStr(parSalida1.Value)
            isFlagAnulacion = Funciones.CheckStr(parSalida2.Value)
            isFlagCostDLV = Funciones.CheckStr(parSalida3.Value)
            codRespuesta = Funciones.CheckStr(parSalida4.Value)
            msjRespuesta = Funciones.CheckStr(parSalida5.Value)

        Catch ex As Exception
            codRespuesta = "-99"
            msjRespuesta = String.Format("{0} => {1}", "Error: AL ConsultaAnulaPedidoDLV", Funciones.CheckStr(ex.Message))
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
    End Sub
    'JLOPETAS - PROY 140589 - FIN

End Class
