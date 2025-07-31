Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB


Public Class clsConsultaMsSap
    Dim strCadenaEsquema As String
    Dim objSeg As New Seguridad_NET.clsSeguridad
    Dim strCadenaConexion As String
    '*** Referencia a los procedimientos almacenados ***'
    Dim SP_SSAPSS_ESTADOPEDIDO As String = ".SSAPSS_DIP_POOLPAGOS"
    Dim SP_SSAPSS_MATERIALXOFICINA As String = ".SAPSS_MATERIALXOFICINA"
    Dim SP_SSAPSS_STOCK As String = ".SSAPSS_STOCK"
    Dim SP_SSAPSS_VALIDASERIE As String = ".SSAPSS_VALIDASERIE"
    Dim SP_SSAPSS_PARAMETROOFICINA As String = ".SSAPSS_PARAMETROOFICINA"
    Dim SP_SSAPFT_CALCULOCORRELATIVO As String = ".SSAPFT_CORRELATIVO"
    Dim SISACS_DATOS_OFICINA As String = ".SISACS_DATOS_OFICINA"
    Dim SSAPSS_OFICINA As String = ".SSAPSS_OFICINA"
    Dim SP_SSAPSS_IMPRESIONTICKET As String = ".SSAPSS_IMPRESIONTICKET"
    Dim SSAPSS_FORMASDEPAGO As String = ".SSAPSS_FORMASDEPAGO"
    Dim SSAPSS_FORMASDEPAGODAC As String = ".SSAPSS_FORMASDEPAGODAC"
    Dim SP_SSAPSS_DIP_PAGOSRECHAZADOS As String = ".SSAPSS_DIP_PAGOSRECHAZADOS"
    Dim SP_SSAPSS_DIP_NOTASCANJE As String = ".SSAPSS_DIP_NOTASCANJE"
    Dim SP_SSAPSS_CORRELATIVONC As String = ".SSAPSS_CORRELATIVONC"
    Dim SP_SSAPSS_CONSULTACANJE As String = ".SSAPSS_CONSULTACANJE"
    Dim SP_GET_DATOS_OFICINA As String = ".GET_DATOS_OFICINA" 'PROY-30182-IDEA-40535
    Dim SP_SSAPSS_CONF_SEEDSTOCK As String = ".SSAPSS_CONF_SEEDSTOCK" 'PROY-SIS
    Dim EstablecimientoSUNATWS As New EstablecimientoSUNATWS.EstablecimientoSUNATWSService 'PROY-140336
    Dim SP_SSAPSS_CONSULTA_CUPON As String = ".SSAPSS_CONSULTA_CUPON" '//PROY BUYBACK
    Dim SP_SSAPSU_ACTUALIZA_CUPON As String = ".SSAPSU_ACTUALIZA_CUPON" '//PROY BUYBACK

    Public Function ReservarSerie(ByVal K_SERIC_CODSERIE As String) As Int64
        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym

        Dim dsPool As DataSet
        Dim rpta As Integer = 0
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_SERIC_CODSERIE", DbType.String, 18, K_SERIC_CODSERIE, ParameterDirection.Input)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_VENTA.SSAPSU_RESERVATEMPORAL"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            rpta = objRequest.Factory.ExecuteNonQuery(objRequest)
            Return rpta
        Catch ex As Exception
            Return rpta
        End Try
    End Function

    '**Actualizar el estado de la serie, al grabar=>  PED \ Eliminar de la Grilla => DIS **'
    Public Function CambiarEstadoSerie(ByVal K_SERIC_CODSERIE As String, _
                                        ByVal K_SERIC_ESTADO As String, _
                                            ByVal K_SERIV_TIPOTRAN As String) As Int16

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym

        Dim dsPool As DataSet
        Dim rpta As Integer = 0
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_SERIC_CODSERIE", DbType.String, 18, K_SERIC_CODSERIE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_SERIC_ESTADO", DbType.String, 3, K_SERIC_ESTADO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_SERIV_TIPOTRAN", DbType.String, 200, K_SERIV_TIPOTRAN, ParameterDirection.Input)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.CommandType = strCadenaEsquema & "PKG_VENTA.SSAPSU_ACTUALIZARESTADOSERIE"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            rpta = objRequest.Factory.ExecuteNonQuery(objRequest)
            Return rpta
        Catch ex As Exception
            Return rpta
        End Try
    End Function

    '**************************************************************************************************'
    '** " "- EVERIS ******************************************************************************'
    '******CONSULTA DEL PUNTO DE VENTA ****************************************************************'
    '**************************************************************************************************'
    Public Function ConsultaPuntoVenta(ByVal P_OVENC_CODIGO As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
   
        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OVENC_CODIGO", DbType.String, 10, P_OVENC_CODIGO, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_SALIDA", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_NUEVA_LISTAPRECIOS" & SISACS_DATOS_OFICINA
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaPuntoVenta = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function ConsultarPagoHist(ByVal K_CLIEC_TIPODOCCLIENTE As String, _
                                        ByVal K_CLIEV_NRODOCCLIENTE As String, _
                                        ByVal K_OFICV_CODOFICINA As String, _
                                        ByVal K_FECHA_INICIAL As String, _
                                        ByVal K_FECHA_FINAL As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))


        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym

        Dim dsPool As DataSet
        Dim num_rows As Integer
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CLIEC_TIPODOCCLIENTE", DbType.String, 2, K_CLIEC_TIPODOCCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_CLIEV_NRODOCCLIENTE", DbType.String, 16, K_CLIEV_NRODOCCLIENTE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_OFICV_CODOFICINA", DbType.String, 4, K_OFICV_CODOFICINA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_FECHA_INICIAL", DbType.String, 10, K_FECHA_INICIAL, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_FECHA_FINAL", DbType.String, 10, K_FECHA_FINAL, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_RESULT_SET", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_PAGOHISTORICO"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            num_rows = dsPool.Tables(0).Rows.Count
            ConsultarPagoHist = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    'SINERGIA 6.0 :
    Public Function ConsultaSerieXMaterial(ByVal K_OFICV_CODOFICINA As String, _
                                          ByVal K_OFICV_DESCRIPCION As String, _
                                          ByVal K_OFICC_CODCENTRO As String, _
                                          ByVal K_OFICC_CODALMACEN As String, _
                                          ByVal K_MATEC_CODMATERIAL As String, _
                                          ByVal K_MATEV_DESCMATERIAL As String, _
                                          ByVal K_OFICC_TIPOOFICINA As String) As DataSet



        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym


        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_OFICV_CODOFICINA", DbType.String, 4, K_OFICV_CODOFICINA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_OFICV_DESCRIPCION", DbType.String, DBNull.Value, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_OFICC_CODCENTRO", DbType.String, DBNull.Value, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_OFICC_CODALMACEN", DbType.String, DBNull.Value, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_MATEC_CODMATERIAL", DbType.String, 18, K_MATEC_CODMATERIAL, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_MATEV_DESCMATERIAL", DbType.String, "", ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_OFICC_TIPOOFICINA", DbType.String, 2, K_OFICC_TIPOOFICINA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_RESULT_SET", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_SERIEXMATERIAL"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaSerieXMaterial = dsPool
        Catch ex As Exception
            Return Nothing
        End Try


    End Function

    '***CONSULTA PARA LISTAR LOS MATERIALES EN LA VENTA RAPIDA *****'
    Public Function ConsultaStock(ByVal K_OFICV_CODOFICINA As String, _
                                    ByVal K_OFICV_DESCRIPCION As String, _
                                        ByVal K_OFICC_TIPOOFICINA As String, _
                                            ByVal K_OFICC_FLAGSERVICIO As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym


        Dim dsPool As DataSet

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_OFICV_CODOFICINA", DbType.String, 10, K_OFICV_CODOFICINA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_OFICV_DESCRIPCION", DbType.String, 50, K_OFICV_DESCRIPCION, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_OFICC_TIPOOFICINA", DbType.String, 2, K_OFICC_TIPOOFICINA, ParameterDirection.Input), _
                           New DAAB.DAABRequest.Parameter("K_OFICC_FLAGSERVICIO", DbType.String, 1, K_OFICC_FLAGSERVICIO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_RESULT_SET", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_STOCK"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaStock = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function ConsultaOfina(ByVal K_OFICV_CODOFICINA As String, ByVal K_OFICV_DESCRIPCION As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym

        Dim dsPool As DataSet
        Dim dtPool As New DataTable

        Dim drFila As DataRow
        Dim dsReturn As New DataSet

        Dim i As Integer

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_OFICV_CODOFICINA", DbType.String, 10, K_OFICV_CODOFICINA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_OFICV_DESCRIPCION", DbType.String, 100, K_OFICV_DESCRIPCION, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_RESULT_SET", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = strCadenaEsquema & "PKG_CONSULTA" & SSAPSS_OFICINA
        objRequest.Parameters.AddRange(arrParam)
        dsPool = objRequest.Factory.ExecuteDataset(objRequest)
        ConsultaOfina = dsPool
    End Function


    '**** CONSULTAR CAJA: ************'
    Public Function ObtenerCajaAsignada(ByVal P_FECHA As String, _
                                        ByVal P_USUARIO As String, _
                                        ByVal P_OFICINA As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, P_FECHA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, P_USUARIO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, P_OFICINA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PCK_SICAR_OFF_SAP.MIG_ObtenerCajaAsignada"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ObtenerCajaAsignada = dsPool
        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    'SSAPSS_VALIDASERIE - ConsultarSerie - listo 
    Public Function ConsultarSerie(ByVal K_SERIC_CODSERIE As String) As DataTable

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
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_SERIC_CODSERIE", DbType.String, 10, K_SERIC_CODSERIE, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_MSSAP" & "SP_SSAPSS_VALIDASERIE"
        objRequest.Parameters.AddRange(arrParam)
        ' dtPool = objRequest.Factory.ExecuteDataset(objRequest)
        dtPool = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader

        ConsultarSerie = dtPool


    End Function

    Public Function ConsultaPoolPagos(ByVal K_PEDIC_ESTADO As String, _
                                        ByVal K_PEDID_FECHADOCUMENTO As String, _
                                        ByVal K_OFICV_CODOFICINA As String, _
                                        ByVal K_PAGOV_USUARIOCREA As String, _
                                        Optional ByVal K_CLIEC_TIPODOCCLIENTE As String = "", _
                                        Optional ByVal K_CLIEV_NRODOCCLIENTE As String = "") As DataTable

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym


        Dim dtPool As New DataTable
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIC_ESTADO", DbType.String, 3, K_PEDIC_ESTADO, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("K_PEDID_FECHADOCUMENTO", DbType.Date, K_PEDID_FECHADOCUMENTO, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("K_OFICV_CODOFICINA", DbType.String, 10, K_OFICV_CODOFICINA, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("K_PAGOV_USUARIOCREA", DbType.String, K_PAGOV_USUARIOCREA, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("K_CLIEC_TIPODOCCLIENTE", DbType.String, 2, K_CLIEC_TIPODOCCLIENTE, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("K_CLIEV_NRODOCCLIENTE", DbType.String, 16, K_CLIEV_NRODOCCLIENTE, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("CU_ESTADOPEDIDO", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA" & SP_SSAPSS_ESTADOPEDIDO
            objRequest.Parameters.AddRange(arrParam)
            dtPool = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
            ConsultaPoolPagos = dtPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    '***Consulta Comprobante - Kerly Adriana
    Public Function ConsultarComprobante(ByVal K_NROINTERNO As Int32) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsPool As DataSet
        Dim dtPool As New DataTable

        Dim drFila As DataRow
        Dim dsReturn As New DataSet

        Dim i As Integer

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_NROINTERNO", DbType.Int32, K_NROINTERNO, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_CU_CABECERA", DbType.Object, ParameterDirection.Output), _
        New DAAB.DAABRequest.Parameter("K_CU_DETALLE", DbType.Object, ParameterDirection.Output), _
        New DAAB.DAABRequest.Parameter("K_NRO_ERROR", DbType.Int32, ParameterDirection.Output), _
        New DAAB.DAABRequest.Parameter("K_DES_ERROR", DbType.String, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_PAPERLESS_FE"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultarComprobante = dsPool

        Catch ex As Exception

        End Try
    End Function
    '************************************************************************************

    'SSAPSS_PEDIDO - ConsultaPedido - listo
    Public Function ConsultaPedido(ByVal K_PEDIN_NROPEDIDO As Int64, _
                                    ByVal K_OFICV_CODOFICINA As String, _
                                    ByVal K_INTEV_CODINTERLOCUTOR As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym

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

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                       New DAAB.DAABRequest.Parameter("K_OFICV_CODOFICINA", DbType.String, K_OFICV_CODOFICINA, ParameterDirection.Input), _
                       New DAAB.DAABRequest.Parameter("K_INTEV_CODINTERLOCUTOR", DbType.String, K_INTEV_CODINTERLOCUTOR, ParameterDirection.Input), _
                       New DAAB.DAABRequest.Parameter("K_RESULT_HEADER", DbType.Object, ParameterDirection.Output), _
                       New DAAB.DAABRequest.Parameter("K_RESULT_DETAIL", DbType.Object, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure         '** HARDCODE **'
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_PEDIDO"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaPedido = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    'SSAPSS_MAALXOFICINA METODO ConsultaMaterialXOficina  - listo
    Public Function ConsultaMaterialXOficina(ByVal K_OFICV_CODOFICINA As String, ByVal K_OFICV_DESCRIPCION As String, ByVal K_OFICC_CODCENTRO As String, ByVal K_OFICC_CODALMACEN As String, ByVal K_OFICC_TIPOOFICINA As String) As DataTable

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        Dim dtPool As New DataTable
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_OFICV_CODOFICINA", DbType.String, 10, K_OFICV_CODOFICINA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_OFICV_DESCRIPCION", DbType.String, 50, K_OFICV_DESCRIPCION, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_OFICC_CODCENTRO", DbType.String, 4, K_OFICC_CODCENTRO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_OFICC_CODALMACEN", DbType.String, 4, K_OFICC_CODALMACEN, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_OFICC_TIPOOFICINA", DbType.String, 2, K_OFICC_TIPOOFICINA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_RESULT_SET", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_MSSAP" & SP_SSAPSS_MATERIALXOFICINA
            objRequest.Parameters.AddRange(arrParam)
            dtPool = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
            ConsultaMaterialXOficina = dtPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    'SP_SSAPFT_CALCULOCORRELATIVO - ObtenerCorrelativoSunat - listo
    Public Function ObtenerCorrelativoSunat(ByVal K_CORRC_CODOFICINAVENTA As Double, ByVal K_CORRC_CODTIPODOCUMENTO As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CORRC_CODOFICINAVENTA", DbType.String, 10, K_CORRC_CODOFICINAVENTA, ParameterDirection.Input), _
                       New DAAB.DAABRequest.Parameter("K_CORRC_CODTIPODOCUMENTO", DbType.String, 2, K_CORRC_CODTIPODOCUMENTO, ParameterDirection.Input), _
                       New DAAB.DAABRequest.Parameter("CORRELATIVO_SUNAT", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_MSSAP" & SP_SSAPFT_CALCULOCORRELATIVO
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ObtenerCorrelativoSunat = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '** Agregar en documentaciòn: ***
    Public Function ConsultarPrecioVenta(ByVal K_PREVC_CODMATERIAL As String, _
                                         ByVal K_PREVC_CODSERIE As String, _
                                         ByVal K_PREVN_PRECIOBASE As Double, _
                                         ByVal K_PREVN_PRECIOVENTA As Double, _
                                         ByRef K_PREVC_CODMATERIAL_OUT As String, _
                                         ByRef K_PREVV_DESCRIPCION_OUT As String, _
                                         ByRef K_PREVC_CODSERIE_OUT As String, _
                                         ByRef K_PREVN_PRECIOBASE_OUT As Double, _
                                         ByRef K_PREVN_PRECIOVENTA_OUT As Double, _
                                         ByRef K_PREVN_DESCUENTO_OUT As Double, _
                                         ByRef K_PREVN_IGV_OUT As Double, _
                                         ByVal K_PREVN_TOTAL_OUT As Double)



        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PREVC_CODMATERIAL", DbType.String, 18, K_PREVC_CODMATERIAL, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PREVC_CODSERIE", DbType.String, 18, K_PREVC_CODSERIE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PREVN_PRECIOBASE", DbType.Double, 18, K_PREVN_PRECIOBASE, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PREVN_PRECIOVENTA", DbType.Double, 18, K_PREVN_PRECIOVENTA, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_PREVC_CODMATERIAL_OUT", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_PREVV_DESCRIPCION_OUT", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_PREVC_CODSERIE_OUT", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_PREVN_PRECIOBASE_OUT", DbType.Double, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_PREVN_PRECIOVENTA_OUT", DbType.Double, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_PREVN_DESCUENTO_OUT", DbType.Double, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_PREVN_IGV_OUT", DbType.Double, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_PREVN_TOTAL_OUT", DbType.Double, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_PREVENTA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultarPrecioVenta = dsPool

            K_PREVC_CODMATERIAL_OUT = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
            K_PREVV_DESCRIPCION_OUT = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)
            K_PREVC_CODSERIE_OUT = Convert.ToString(CType(objRequest.Parameters(6), IDataParameter).Value)
            K_PREVN_PRECIOBASE_OUT = Convert.ToDouble(CType(objRequest.Parameters(7), IDataParameter).Value)
            K_PREVN_PRECIOVENTA_OUT = Convert.ToDouble(CType(objRequest.Parameters(8), IDataParameter).Value)
            K_PREVN_DESCUENTO_OUT = Convert.ToDouble(CType(objRequest.Parameters(9), IDataParameter).Value)
            K_PREVN_IGV_OUT = Convert.ToDouble(CType(objRequest.Parameters(10), IDataParameter).Value)
            K_PREVN_TOTAL_OUT = Convert.ToDouble(CType(objRequest.Parameters(11), IDataParameter).Value)

            objRequest = Nothing

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '**Agregar en documentaciòn: ***
    Public Function ConsultarPrecioBase(ByVal K_MATEC_CODMATERIAL As String, _
                                        ByVal K_MATEV_DESCMATERIAL As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym


        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_MATEC_CODMATERIAL", DbType.String, 18, K_MATEC_CODMATERIAL, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_MATEV_DESCMATERIAL", DbType.String, 100, K_MATEV_DESCMATERIAL, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_RESULT_SET", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_PRECIOBASE"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultarPrecioBase = dsPool
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ConsultaPedidoXCorrelativo(ByVal K_PEDIV_NROSUNAT As String, _
                                                ByVal K_PDV As String, _
                                                ByRef K_NROLOG As String, _
                                                ByRef K_DESLOG As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        'bym
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
        'bym


        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIV_NROSUNAT", DbType.String, 17, K_PEDIV_NROSUNAT, ParameterDirection.Input), _
                           New DAAB.DAABRequest.Parameter("K_PEDIV_OFICINA", DbType.String, K_PDV, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, K_NROLOG, ParameterDirection.Output), _
                           New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, K_DESLOG, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("K_RESULT_SET", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_PEDIDOCORRELATIVO"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaPedidoXCorrelativo = dsPool

            K_NROLOG = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            K_DESLOG = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    '***Funciones para Factura Electrònica  - 05.05.2015 ***'
    Public Function Get_ConsultaCliente(ByVal NroDocCliente As String, ByVal TipoDocCliente As String) As DataSet

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
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CLIEC_TIPODOCCLIENTE", DbType.String, TipoDocCliente, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("K_CLIEV_NRODOCCLIENTE", DbType.String, NroDocCliente, ParameterDirection.Input), _
            New DAAB.DAABRequest.Parameter("CU_CLIENTE", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_CLIENTE"
            objRequest.Parameters.AddRange(arrParam)

            Get_ConsultaCliente = objRequest.Factory.ExecuteDataset(objRequest)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ConsultarFormasPago(ByVal K_NROPAGOINTERNO As Int32) As DataTable

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsPool As DataSet
        Dim dtPool As New DataTable

        Dim drFila As DataRow
        Dim dsReturn As New DataSet

        Dim i As Integer

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_NROPAGOINTERNO", DbType.Int32, K_NROPAGOINTERNO, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_RESULT", DbType.Object, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA" & SSAPSS_FORMASDEPAGO
            objRequest.Parameters.AddRange(arrParam)
            dtPool = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
            ConsultarFormasPago = dtPool
            objRequest = Nothing
        Catch ex As Exception

        End Try
    End Function

    Public Function ConsultarFormasPagoDac(ByVal K_NROPAGOINTERNO As Int32) As DataTable

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dtPool As New DataTable

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_NROPAGOINTERNO", DbType.Int32, K_NROPAGOINTERNO, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_RESULT", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA" & SSAPSS_FORMASDEPAGODAC
            objRequest.Parameters.AddRange(arrParam)
            dtPool = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
            ConsultarFormasPagoDac = dtPool
            objRequest = Nothing
        Catch ex As Exception
	    Return Nothing
        End Try
    End Function

    Public Function Verificar_Devolucion_Efectivo(ByVal ID_NROPEDIDO As Integer, _
                                                    ByVal IDPAGO As Integer, _
                                                    ByVal MONTO As Double, _
                                                    ByVal Corr_Sunat_origen As String, _
                                                  ByVal codUsuario As String, _
                                                  ByVal P_DEVOL As String, _
                                               ByRef K_NROLOG As String, _
                                               ByRef K_DESLOG As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))


        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, ID_NROPEDIDO, ParameterDirection.Input), _
                           New DAAB.DAABRequest.Parameter("K_PAGON_IDPAGO", DbType.Int64, IDPAGO, ParameterDirection.Input), _
                           New DAAB.DAABRequest.Parameter("K_DEPAN_MONTO", DbType.Double, MONTO, ParameterDirection.Input), _
                           New DAAB.DAABRequest.Parameter("K_DEPAV_NUEVONROSUNATREF", DbType.String, Corr_Sunat_origen, ParameterDirection.Input), _
                           New DAAB.DAABRequest.Parameter("K_DEPAV_USUARIOPAGO", DbType.String, codUsuario, ParameterDirection.Input), _
                           New DAAB.DAABRequest.Parameter("K_RESULT_SET", DbType.Object, ParameterDirection.Output), _
                           New DAAB.DAABRequest.Parameter("K_NRO_ERROR", DbType.String, K_NROLOG, ParameterDirection.Output), _
                           New DAAB.DAABRequest.Parameter("K_DES_ERROR", DbType.String, K_DESLOG, ParameterDirection.Output), _
                           New DAAB.DAABRequest.Parameter("K_DEVEV_P_DEVOL", DbType.String, P_DEVOL, ParameterDirection.Input)}

        Dim K_RESULT_SET As DataSet

        Try
            objRequest.CommandType = CommandType.StoredProcedure

            objRequest.Command = strCadenaEsquema & "PKG_VENTA.SSAPSU_DEVOLUCIONEFECTIVO"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            Verificar_Devolucion_Efectivo = dsPool

            K_NROLOG = Convert.ToString(CType(objRequest.Parameters(6), IDataParameter).Value)
            K_DESLOG = Convert.ToString(CType(objRequest.Parameters(7), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'INICIO - IDEA-141711 - Pack Internet Móvil Prepago
    Public Function Validar_PagoRecarga(ByVal ID_NROPEDIDO As Int64, _
                                      ByRef ISRecarga As String, _
                                      ByRef COD_RPTA As String, _
                                      ByRef MSG_RPTA As String)
        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))


        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsPool As DataSet
        Dim rpta As String
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_PEDIDO", DbType.Int64, ID_NROPEDIDO, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("PO_ISBOLETAPAGO", DbType.String, ISRecarga, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, COD_RPTA, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, MSG_RPTA, ParameterDirection.Output)}
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_CONSULTA.SSAPSS_VALIDAPAGORECARGA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)

            ISRecarga = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value).Trim
            COD_RPTA = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            MSG_RPTA = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

            objRequest = Nothing

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'FIN - IDEA-141711 - Pack Internet Móvil Prepago



    Public Function Validar_PagoRenta(ByVal ID_NROPEDIDO As Int64, _
                                      ByRef ISRenta As String, _
                                      ByRef NROLOG As String, _
                                      ByRef DESLOG As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))


        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsPool As DataSet
        Dim rpta As String
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, ID_NROPEDIDO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_ISRENTAPAGO", DbType.String, ISRenta, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, NROLOG, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, DESLOG, ParameterDirection.Output)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_VALIDAPAGORENTA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            'Validar_PagoRenta = dsPool

            ISRenta = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value).Trim
            NROLOG = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            DESLOG = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)

            objRequest = Nothing

        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function obtenerTipoDocumento_y_codigoPago(ByVal idPedido As String, _
                                                      ByRef tipoCodumento As String, _
                                                      ByRef codigoPago As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_drav_generado", DbType.String, idPedido, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("p_TipoDocumento", DbType.String, tipoCodumento, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("p_CodigoPago", DbType.String, codigoPago, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_DRA_CVE_6.sisacss_CodPago_TipoDoc"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)

            tipoCodumento = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value).Trim
            codigoPago = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function Consultar_Pago_Dra(ByVal PI_COD_APLIC As String, _
                                    ByVal PI_USER_APLIC As String, _
                                    ByVal PI_CODIGO_PAGO As String, _
                                    ByRef COD_RPTA As String, _
                                    ByRef MSG_RPTA As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim dtPool As New DataTable

        Dim drFila As DataRow
        Dim dsReturn As New DataSet


        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_COD_APLIC", DbType.String, PI_COD_APLIC, ParameterDirection.Input), _
                       New DAAB.DAABRequest.Parameter("PI_USER_APLIC", DbType.String, PI_USER_APLIC, ParameterDirection.Input), _
                       New DAAB.DAABRequest.Parameter("PI_CODIGO_PAGO", DbType.String, PI_CODIGO_PAGO, ParameterDirection.Input), _
                       New DAAB.DAABRequest.Parameter("PO_CUR_CONSULTA_CAB", DbType.Object, ParameterDirection.Output), _
                       New DAAB.DAABRequest.Parameter("PO_CUR_CONSULTA_DET", DbType.Object, ParameterDirection.Output), _
                       New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, COD_RPTA, ParameterDirection.Output), _
                       New DAAB.DAABRequest.Parameter("PO_MSG_RPTA", DbType.String, MSG_RPTA, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_TRANSACCION_DRA.SP_CONSULTA_PAGO_DRA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            Consultar_Pago_Dra = dsPool

            COD_RPTA = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value).Trim
            MSG_RPTA = Convert.ToString(CType(objRequest.Parameters(6), IDataParameter).Value).Trim

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ListaTipoDocumento(ByVal Flag_ruc As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim dtPool As New DataTable

        Dim drFila As DataRow
        Dim dsReturn As New DataSet

        Dim i As Integer

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_FLAG_CON", DbType.String, Flag_ruc, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONSULTA_GENERAL.SISACT_CON_TIPO_DOC"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ListaTipoDocumento = dsPool

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function AnularExtornarPagoDRA(ByVal PI_COD_APLI As String, _
                                            ByVal PI_USER_APLI As String, _
                                            ByVal PI_NUM_OPE_PAGO_ST As String, _
                                            ByVal PI_DRAV_DESC_TRS As String, _
                                            ByRef PO_NUM_OPE_PAGO_ANUL_EXT As Int64, _
                                            ByRef PO_COD_RPTA As Int64, _
                                            ByRef PO_MSG_RPTA As String)

        'Dim dtSet As New DataTable
        Dim resultado As Int16

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_COD_APLI", DbType.String, PI_COD_APLI, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_USER_APLI", DbType.String, PI_USER_APLI, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_NUM_OPE_PAGO_ST", DbType.String, PI_NUM_OPE_PAGO_ST, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PI_DRAV_DESC_TRS", DbType.String, PI_DRAV_DESC_TRS, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_NUM_OPE_PAGO_ANUL_EXT", DbType.Int64, PO_NUM_OPE_PAGO_ANUL_EXT, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.Int64, PO_COD_RPTA, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSG_RPTA", DbType.String, PO_MSG_RPTA, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_TRANSACCION_DRA.SP_ANULAR_EXTORNAR_PAGO_DRA"
            objRequest.Parameters.AddRange(arrParam)
            resultado = objRequest.Factory.ExecuteNonQuery(objRequest)

            PO_NUM_OPE_PAGO_ANUL_EXT = Convert.ToInt64(CType(objRequest.Parameters(4), IDataParameter).Value)
            PO_COD_RPTA = Convert.ToInt64(CType(objRequest.Parameters(5), IDataParameter).Value)
            PO_MSG_RPTA = Convert.ToString(CType(objRequest.Parameters(6), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Crear_Pago_Dra(ByVal DRAV_COD_APLIC As String, ByVal DRAV_USUARIO_APLIC As String, ByVal DRAV_CODIGO_PAGO As String, _
                                    ByVal DRAC_TIPO_RA As String, ByVal DRAV_NRO_ASOCIADO As String, _
                                    ByVal DRAV_DESCRIPCION_DOC As String, ByVal DRAV_CODIGO_MONEDA As String, ByVal DRAN_IMPORTE As Decimal, _
                                    ByVal DRAD_FECHA_PAGO As DateTime, ByVal DRAN_TRACE_ID As String, ByVal DRAN_NUM_OPE_PAGO As String, _
                                    ByVal DRAV_COD_BANCO As String, ByVal DRAV_COD_PDV As String, ByVal CADENA_MEDIO_PAGO As String, _
                                    ByRef PO_ID_OPE_PAGO_SISACT As Int64, ByRef PO_COD_RPTA As Int64, _
                                    ByRef PO_MSG_RPTA As String) As Boolean

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("PI_COD_APLIC", DbType.String, DRAV_COD_APLIC, ParameterDirection.Input), _
               New DAABRequest.Parameter("PI_USER_APLI", DbType.String, DRAV_USUARIO_APLIC, ParameterDirection.Input), _
               New DAABRequest.Parameter("PI_COD_PAGO_DRA", DbType.String, DRAV_CODIGO_PAGO, ParameterDirection.Input), _
               New DAABRequest.Parameter("PI_TIPO_DOC", DbType.String, DRAC_TIPO_RA, ParameterDirection.Input), _
               New DAABRequest.Parameter("PI_NRO_DOC", DbType.String, DRAV_NRO_ASOCIADO, ParameterDirection.Input), _
               New DAABRequest.Parameter("PI_DESC_DOC", DbType.String, DRAV_DESCRIPCION_DOC, ParameterDirection.Input), _
               New DAABRequest.Parameter("PI_COD_MONEDA", DbType.String, DRAV_CODIGO_MONEDA, ParameterDirection.Input), _
               New DAABRequest.Parameter("PI_IMPORTE_PAGO", DbType.Decimal, DRAN_IMPORTE, ParameterDirection.Input), _
               New DAABRequest.Parameter("PI_FECHA_PAGO", DbType.DateTime, DRAD_FECHA_PAGO, ParameterDirection.Input), _
               New DAABRequest.Parameter("PI_TRACE_ID", DbType.String, DRAN_TRACE_ID, ParameterDirection.Input), _
               New DAABRequest.Parameter("PI_NRO_OPE_PAGO", DbType.String, DRAN_NUM_OPE_PAGO, ParameterDirection.Input), _
               New DAABRequest.Parameter("PI_COD_BANCO", DbType.String, DRAV_COD_BANCO, ParameterDirection.Input), _
               New DAABRequest.Parameter("PI_COD_PDV", DbType.String, DRAV_COD_PDV, ParameterDirection.Input), _
               New DAABRequest.Parameter("PI_CADENA_MEDIO_PAGO", DbType.String, CADENA_MEDIO_PAGO, ParameterDirection.Input), _
               New DAABRequest.Parameter("PI_MONTO_TOTAL_PAGO", DbType.Decimal, DRAN_IMPORTE, ParameterDirection.Input), _
               New DAABRequest.Parameter("PO_ID_OPE_PAGO_SISACT", DbType.Int64, ParameterDirection.Output), _
               New DAABRequest.Parameter("PO_COD_RPTA", DbType.Int64, ParameterDirection.Output), _
               New DAABRequest.Parameter("PO_MSG_RPTA", DbType.String, 250, "", ParameterDirection.Output)}


        Dim retorno As Boolean = False
        Dim dsPool As DataSet

        Crear_Pago_Dra = False

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_TRANSACCION_DRA.SP_CREAR_PAGO_DRA"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            PO_ID_OPE_PAGO_SISACT = Convert.ToInt64(CType(objRequest.Parameters(15), IDataParameter).Value)
            PO_COD_RPTA = Convert.ToInt64(CType(objRequest.Parameters(16), IDataParameter).Value)
            PO_MSG_RPTA = Convert.ToString(CType(objRequest.Parameters(17), IDataParameter).Value)

            If PO_COD_RPTA = 0 Then
                Crear_Pago_Dra = True
            End If
        Catch ex As Exception
            Return Crear_Pago_Dra = False
        End Try
    End Function

    Public Function Lista_Formas_Pago_pvu() As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim dsPool As DataSet
        Dim dtPool As New DataTable

        Dim drFila As DataRow
        Dim dsReturn As New DataSet

        Dim i As Integer

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_PAGOS", DbType.Object, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_TRANSACCION_DRA.SP_LISTAR_FORMAS_PAGO"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            Lista_Formas_Pago_pvu = dsPool

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Valida_Renta(ByVal K_PEDIN_NROPEDIDO As Int64, _
                                      ByRef K_RPTA_OUTPUT As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))


        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsPool As DataSet
        Dim rpta As String
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_RPTA_OUTPUT", DbType.String, K_RPTA_OUTPUT, ParameterDirection.Output)}

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_VALIDARENTA"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            'Validar_PagoRenta = dsPool

            K_RPTA_OUTPUT = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)

            objRequest = Nothing

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Function ConsultaPagosRechazados(ByVal K_PEDIC_ESTADO As String, _
                           ByVal K_PEDID_FECHADOC_INI As Date, _
                           ByVal K_PEDID_FECHADOC_FIN As Date, _
                           ByVal K_OFICV_CODOFICINA As String) As DataTable

        Dim dtPool As New DataTable

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_PEDIC_ESTADO", DbType.String, K_PEDIC_ESTADO, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_PEDID_FECHADOC_INI", DbType.Date, K_PEDID_FECHADOC_INI.ToShortDateString(), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_PEDID_FECHADOC_FIN", DbType.Date, K_PEDID_FECHADOC_FIN.ToShortDateString(), ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_OFICV_CODOFICINA", DbType.String, K_OFICV_CODOFICINA, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("CU_RECHAZADO", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA" & SP_SSAPSS_DIP_PAGOSRECHAZADOS
            objRequest.Parameters.AddRange(arrParam)
            dtPool = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
            ConsultaPagosRechazados = dtPool

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    '*** Para Notas de Canje
    Public Function ConsultaPoolNotasCanje(ByVal K_PEDIC_ESTADO As String, _
                                        ByVal K_PEDID_FECHADOCUMENTO As String, _
                                        ByVal K_OFICV_CODOFICINA As String, _
                                        ByVal K_PAGOV_USUARIOCREA As String) As DataTable

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dtPool As New DataTable
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                        New DAAB.DAABRequest.Parameter("K_PEDIC_ESTADO", DbType.String, 3, K_PEDIC_ESTADO, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("K_PEDID_FECHADOCUMENTO", DbType.Date, K_PEDID_FECHADOCUMENTO, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("K_OFICV_CODOFICINA", DbType.String, 10, K_OFICV_CODOFICINA, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("K_PAGOV_USUARIOCREA", DbType.String, K_PAGOV_USUARIOCREA, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("CU_ESTADOPEDIDO", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA" & SP_SSAPSS_DIP_NOTASCANJE
            objRequest.Parameters.AddRange(arrParam)
            dtPool = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
            ConsultaPoolNotasCanje = dtPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultaCorrelativoNotasCanje(ByRef K_CORR_NOTACANJE As String, _
                                        ByRef K_NROLOG As String, _
                                        ByRef K_DESLOG As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                        New DAAB.DAABRequest.Parameter("K_CORR_NOTACANJE", DbType.String, K_CORR_NOTACANJE, ParameterDirection.Output), _
                        New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, K_NROLOG, ParameterDirection.Output), _
                        New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, K_DESLOG, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA" & SP_SSAPSS_CORRELATIVONC
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            K_CORR_NOTACANJE = CType(objRequest.Parameters(0), IDataParameter).Value
            K_NROLOG = CType(objRequest.Parameters(1), IDataParameter).Value
            K_DESLOG = CType(objRequest.Parameters(2), IDataParameter).Value

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultaCanje(ByVal nroPedido As String, _
                                  ByRef K_NROLOG As String, _
                                  ByRef K_DESLOG As String) As DataTable


        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
            strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
            If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
                strCadenaEsquema = strCadenaEsquema & "."
            Else
                strCadenaEsquema = String.Empty
            End If

            Dim dtPool As New DataTable
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                            New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.String, nroPedido, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("CU_CONSULTA_CANJE", DbType.Object, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, K_NROLOG, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, 200, K_DESLOG, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CANJE" & SP_SSAPSS_CONSULTACANJE
            objRequest.Parameters.AddRange(arrParam)
            dtPool = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
            ConsultaCanje = dtPool
            K_NROLOG = CType(objRequest.Parameters(2), IDataParameter).Value
            K_DESLOG = CType(objRequest.Parameters(3), IDataParameter).Value

        Catch ex As Exception
            K_NROLOG = 99
            K_DESLOG = ex.Message.ToString()
            Return Nothing
        End Try
    End Function

    '/*----------------------------------------------------------------------------------------------------------------*/
    '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
    '/*----------------------------------------------------------------------------------------------------------------*/
    'Metodo que invocara al SP para listar eld etalle de pedido de devolucion (mas de un registro) para las NOTAS DE CANJE
    Public Function ConsultaDevolucion(ByVal nroPedido As String, _
                                  ByRef K_NROLOG As String, _
                                  ByRef K_DESLOG As String) As DataTable
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
            strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
            If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
                strCadenaEsquema = strCadenaEsquema & "."
            Else
                strCadenaEsquema = String.Empty
            End If

            Dim dtPool As New DataTable
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                            New DAAB.DAABRequest.Parameter("PI_NROPEDIDO", DbType.String, nroPedido, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("PO_CUR_SALIDA", DbType.Object, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("PO_COD_RESP", DbType.String, K_NROLOG, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("PO_MEN_RESP", DbType.String, 200, K_DESLOG, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CANJE.SSAPSS_CONSULTADEVOLUCION"

            objRequest.Parameters.AddRange(arrParam)
            dtPool = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
            ConsultaDevolucion = dtPool
            K_NROLOG = CType(objRequest.Parameters(2), IDataParameter).Value
            K_DESLOG = CType(objRequest.Parameters(3), IDataParameter).Value
        Catch ex As Exception
            K_NROLOG = 99
            K_DESLOG = ex.Message.ToString()
            Return Nothing
        End Try
    End Function
    '/*----------------------------------------------------------------------------------------------------------------*/
    '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
    '/*----------------------------------------------------------------------------------------------------------------*/
    Public Function ValidarPagoPackAccesorio(ByVal P_NROPEDIDO_PACK As String, ByRef P_RESULTADO As String) As DataSet 'PROY-23111-IDEA-29841 - INICIO

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsPool As DataSet
        Dim rpta As String
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_PEDIDO_PACK", DbType.String, P_NROPEDIDO_PACK, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_RESULTADO", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_PEDIDO_PACK_ACC"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ValidarPagoPackAccesorio = dsPool

            P_RESULTADO = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function 'PROY-23111-IDEA-29841 - FIN

    Public Function ConsultarPuntoVentaPedido(ByVal P_COD_PEDIDO As String, ByRef P_COD_RPTA As String, ByRef P_MSGE_RPTA As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim rpta As String
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_COD_PEDIDO", DbType.String, P_COD_PEDIDO, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_COD_RPTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("K_MSGE_RPTA", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_MSSAP.SSAPT_VALIDA_PDV_VIRTUAL"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            P_COD_RPTA = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value)
            P_MSGE_RPTA = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'PROY-23700-IDEA-29415 - INI
    Public Function ConsultaImpresionCanje(ByVal nroPedido As String, _
                                  ByRef K_NROLOG As String, _
                                  ByRef K_DESLOG As String) As DataTable


        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
            strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
            If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
                strCadenaEsquema = strCadenaEsquema & "."
            Else
                strCadenaEsquema = String.Empty
            End If

            Dim dtPool As New DataTable
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                            New DAAB.DAABRequest.Parameter("K_NROINTERNO", DbType.String, nroPedido, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("K_CU_CANJE", DbType.Object, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("K_NRO_ERROR", DbType.String, K_NROLOG, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("K_DES_ERROR", DbType.String, 200, K_DESLOG, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CANJE.SSAPSS_IMPRESIONCANJE"
            objRequest.Parameters.AddRange(arrParam)
            dtPool = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
            ConsultaImpresionCanje = dtPool
            K_NROLOG = CType(objRequest.Parameters(2), IDataParameter).Value
            K_DESLOG = CType(objRequest.Parameters(3), IDataParameter).Value

        Catch ex As Exception
            K_NROLOG = 99
            K_DESLOG = ex.Message.ToString()
            Return Nothing
        End Try
    End Function

    Public Function ConsultaMotivoxPedidoSap(ByVal nroPedido As String, _
                                  ByRef K_NROLOG As String, _
                                  ByRef K_DESLOG As String) As DataTable

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
            strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
            If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
                strCadenaEsquema = strCadenaEsquema & "."
            Else
                strCadenaEsquema = String.Empty
            End If

            Dim dtPool As New DataTable
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                            New DAAB.DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.String, nroPedido, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("CU_CONSULTA_CANJE", DbType.Object, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("K_NROLOG", DbType.String, K_NROLOG, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("K_DESLOG", DbType.String, 200, K_DESLOG, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CANJE.SSAPSS_CONSULTACANJE"
            objRequest.Parameters.AddRange(arrParam)
            dtPool = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
            ConsultaMotivoxPedidoSap = dtPool
            K_NROLOG = CType(objRequest.Parameters(2), IDataParameter).Value
            K_DESLOG = CType(objRequest.Parameters(3), IDataParameter).Value

        Catch ex As Exception
            K_NROLOG = 99
            K_DESLOG = ex.Message.ToString()
            Return Nothing
        End Try
    End Function
    'PROY-23700-IDEA-29415 - FIN 

    'PROY-30182-IDEA-40535-INICIO
    Public Function ConsultaOutoffbioTiendaVirtual(ByVal P_OVENC_CODIGO As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OVENC_CODIGO", DbType.String, 10, P_OVENC_CODIGO, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_SALIDA", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_DATOS_OFICINA" & SP_GET_DATOS_OFICINA
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaOutoffbioTiendaVirtual = dsPool
        Catch ex As Exception
            Return Nothing
        End Try
    End Function 
    'PROY-30182-IDEA-40535-FIN

    'PROY-26366-IDEA-34247 FASE 1 - INICIO
    Public Function ConsultaComprobante(ByVal PI_NRO_SUNAT As String, _
                                           ByVal PI_TIPO_DOCU As String, _
                                           ByRef PO_NRO_PEDIDO As String, _
                                           ByRef PO_COD_RESULTADO As String, _
                                           ByRef PO_MSJ_RESULTADO As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_NRO_SUNAT", DbType.String, 17, PI_NRO_SUNAT, ParameterDirection.Input), _
                           New DAAB.DAABRequest.Parameter("PI_TIPO_DOCU", DbType.String, PI_TIPO_DOCU, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_NRO_PEDIDO", DbType.String, PO_NRO_PEDIDO, ParameterDirection.Output), _
                           New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, PO_COD_RESULTADO, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, PO_MSJ_RESULTADO, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_ID_COMPROBANTE"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaComprobante = dsPool

            PO_NRO_PEDIDO = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            PO_COD_RESULTADO = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            PO_MSJ_RESULTADO = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultaMaterial(ByVal K_MATEC_CODMATERIAL As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If
  
        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_MATEC_CODMATERIAL", DbType.String, 30, K_MATEC_CODMATERIAL, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("K_RESULT_SET", DbType.Object, ParameterDirection.Output)}


        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_MATERIAL"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaMaterial = dsPool

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultaDetPedido(ByVal K_PEDIN_NROPEDIDO As Int64) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_PEDIN_NROPEDIDO", DbType.Int64, K_PEDIN_NROPEDIDO, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("PO_CUR_RESULTADO", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_PEDIDO_DET_ADD"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)
            ConsultaDetPedido = dsPool

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'PROY-26366-IDEA-34247 FASE 1 - FIN
    'COMPROBANTE 1812
    Public Function ConsultaSeedsSock(ByVal codserie As String, ByRef codRespuesta As String) As String
        Dim msjRespuesta As String = "OK"

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_CODSERIE", DbType.String, codserie, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_CODIGO_RESPUESTA", DbType.String, codRespuesta, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MENSAJE_RESPUESTA", DbType.String, 2000, msjRespuesta, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA" & SP_SSAPSS_CONF_SEEDSTOCK
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)

            codRespuesta = Convert.ToString(CType(objRequest.Parameters(1), IDataParameter).Value).Trim

            If codRespuesta = "0" Then
                msjRespuesta = Convert.ToString(CType(objRequest.Parameters(2), IDataParameter).Value)
            End If
            Return msjRespuesta
        Catch ex As Exception
            Return msjRespuesta
        End Try
    End Function
    'COMPROBANTE 1812
    'INI PROY-140336
    Public Function ObtenerCodigoProducto(ByVal pi_codMaterial As String, _
                                           ByRef po_codProducto As String, _
                                           ByRef po_codmsg As String, _
                                           ByRef po_mensaje As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))


        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                        New DAAB.DAABRequest.Parameter("PI_CODMATERIAL", DbType.String, pi_codMaterial, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("PO_COD_PROD_SUNAT", DbType.String, ParameterDirection.Output), _
                        New DAAB.DAABRequest.Parameter("PO_COD_RESPUESTA", DbType.String, ParameterDirection.Output), _
                        New DAAB.DAABRequest.Parameter("PO_MSJ_RESPUESTA", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_PROD_SUNAT"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            Dim parSalida1 As IDataParameter
            Dim parSalida2 As IDataParameter
            Dim parSalida3 As IDataParameter
            parSalida1 = CType(objRequest.Parameters(3), IDataParameter)
            parSalida2 = CType(objRequest.Parameters(4), IDataParameter)
            parSalida3 = CType(objRequest.Parameters(5), IDataParameter)

            po_codProducto = Funciones.CheckStr(parSalida1.Value)
            po_codmsg = Funciones.CheckStr(parSalida2.Value)
            po_mensaje = Funciones.CheckStr(parSalida3.Value)

            ObtenerCodigoProducto = "CODPRODUCTOSUNAT: " & po_codProducto & " - CODMENSAJE: " & po_codmsg & _
                                      " - MENSAJE: " & po_mensaje

        Catch ex As Exception
            ObtenerCodigoProducto = ex.Message.ToString()
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
    End Function
    Public Function ObtenerCodigoProductoWS(ByVal pi_codMaterial As String, _
                                        ByRef po_codProducto As String, _
                                        ByRef po_codmsg As String, _
                                        ByRef po_mensaje As String) As String


        Try
            EstablecimientoSUNATWS.Url = ConfigurationSettings.AppSettings("RutaWS_EstablecimientoSUNAT").ToString()
            Dim p As New System.Net.WebProxy
            p.Credentials = System.Net.CredentialCache.DefaultCredentials
            EstablecimientoSUNATWS.Proxy = p
            EstablecimientoSUNATWS.Timeout = ConfigurationSettings.AppSettings("TimeoutWS").ToString()
            Dim objObtenerCodigoProductoResponse As New EstablecimientoSUNATWS.obtenerCodigoProductoSUNATResponse
            Dim objlistaRequestOpcional(0) As EstablecimientoSUNATWS.parametrosTypeObjetoOpcional
            objlistaRequestOpcional(0) = New EstablecimientoSUNATWS.parametrosTypeObjetoOpcional
            objlistaRequestOpcional(0).campo = String.Empty
            objlistaRequestOpcional(0).valor = String.Empty
            Dim objcodigoProductoRequest As New EstablecimientoSUNATWS.obtenerCodigoProductoSUNATRequest

            objcodigoProductoRequest.codigoMaterial = pi_codMaterial

            objcodigoProductoRequest.auditRequest = New EstablecimientoSUNATWS.auditRequestType
            objcodigoProductoRequest.auditRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHss")
            objcodigoProductoRequest.auditRequest.ipAplicacion = ConfigurationSettings.AppSettings("CodAplicacion")
            objcodigoProductoRequest.auditRequest.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
            objcodigoProductoRequest.auditRequest.usuarioAplicacion = String.Empty

            objcodigoProductoRequest.listaRequestOpcional = objlistaRequestOpcional

            objObtenerCodigoProductoResponse = EstablecimientoSUNATWS.obtenerCodigoProductoSUNAT(objcodigoProductoRequest)

            po_codProducto = objObtenerCodigoProductoResponse.codigoProductoSunat
            po_codmsg = objObtenerCodigoProductoResponse.auditResponse.codigoRespuesta
            po_mensaje = objObtenerCodigoProductoResponse.auditResponse.mensajeRespuesta

            ObtenerCodigoProductoWS = "CODPRODUCTOSUNAT: " & po_codProducto & " - CODMENSAJE: " & po_codmsg & _
                                      " - MENSAJE: " & po_mensaje
        Catch ex As Exception
            po_codmsg = "-1"
            po_mensaje = ex.Message.ToString()
        End Try
        Return ObtenerCodigoProductoWS
    End Function
    'FIN PROY-140336
    '//PROY BUYBACK INI
    Public Function ConsultaCupon(ByVal strNroPedido As String, ByRef strCodRespuesta As String, ByRef strMsjRespuesta As String) As DataTable
        Dim msjRespuesta As String = "OK"

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsPool As DataSet
        Dim dsDT As DataTable
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_CUPN_NROPEDIDO", DbType.String, strNroPedido, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_CUR_SALIDA", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_CODRESPUESTA", DbType.String, strCodRespuesta, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MSJRESPUESTA", DbType.String, 2000, strMsjRespuesta, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_BUYBACK" & SP_SSAPSS_CONSULTA_CUPON
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRespuesta = Convert.ToString(CType(objRequest.Parameters(objRequest.Parameters.Count - 2), IDataParameter).Value).Trim

            If strCodRespuesta = "0" Then
                dsDT = dsPool.Tables(0)
                msjRespuesta = Convert.ToString(CType(objRequest.Parameters(objRequest.Parameters.Count - 1), IDataParameter).Value)
            End If
            Return dsDT
        Catch ex As Exception
            Return dsDT
        End Try
    End Function

    Public Function ActualizaCupon(ByVal strCodCupon As String, ByVal strEstdo As String, ByVal strUsuario As String, ByRef strCodRespuesta As String, ByRef strMsjRespuesta As String) As String
        Dim msjRespuesta As String = "OK"

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsPool As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_CUPV_NROCUPON", DbType.String, strCodCupon, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_CUPC_ESTADO", DbType.String, strEstdo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_CUPV_USUARIOMODI", DbType.String, strUsuario, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PI_CUPD_FECHAMODI", DbType.Date, Date.Now.ToString("dd/MM/yyyy"), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_CODRESPUESTA", DbType.String, strCodRespuesta, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MSJRESPUESTA", DbType.String, 2000, strMsjRespuesta, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_BUYBACK" & SP_SSAPSU_ACTUALIZA_CUPON
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Transactional = True
            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Factory.CommitTransaction()
            strCodRespuesta = Convert.ToString(CType(objRequest.Parameters(objRequest.Parameters.Count - 2), IDataParameter).Value).Trim

            If strCodRespuesta = "0" Then
                msjRespuesta = Convert.ToString(CType(objRequest.Parameters(objRequest.Parameters.Count - 1), IDataParameter).Value)
            End If
            Return msjRespuesta
        Catch ex As Exception
            objRequest.Factory.RollBackTransaction()
            Return msjRespuesta
        End Try
    End Function
    '//PROY BUYBACK FIN

    'INICIO - IDEA-141711 - Pack Internet Móvil Prepago
    Public Function ObtenerPaquete(ByRef strCodRespuesta As String, ByRef strMsjRespuesta As String) As DataSet
        Dim msjRespuesta As String = "OK"

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsDatosPedido As DataSet
        Dim dsDT As DataTable
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PO_CURSOR", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, strCodRespuesta, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, 2000, strMsjRespuesta, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_SERVICIO_RECARGA"
            objRequest.Parameters.AddRange(arrParam)
            dsDatosPedido = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRespuesta = Convert.ToString(CType(objRequest.Parameters(objRequest.Parameters.Count - 2), IDataParameter).Value).Trim

            If strCodRespuesta = "0" Then
                dsDT = dsDatosPedido.Tables(0)
                msjRespuesta = Convert.ToString(CType(objRequest.Parameters(objRequest.Parameters.Count - 1), IDataParameter).Value)
            End If

            Return dsDatosPedido

        Catch ex As Exception
            Return dsDatosPedido
        End Try
    End Function

    Public Function ConsultaPaqueteAdicional(ByVal intNroPedido As Int64, ByRef strCodRespuesta As String, ByRef strMsjRespuesta As String) As DataSet
        Dim msjRespuesta As String = "OK"

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim dsDatosPedido As DataSet
        Dim dsDT As DataTable
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_PEDIDO", DbType.Int64, intNroPedido, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_CURSOR", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, strCodRespuesta, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, 2000, strMsjRespuesta, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_CONSULTARECARGA"
            objRequest.Parameters.AddRange(arrParam)
            dsDatosPedido = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRespuesta = Convert.ToString(CType(objRequest.Parameters(objRequest.Parameters.Count - 2), IDataParameter).Value).Trim

            If strCodRespuesta = "0" Then
                dsDT = dsDatosPedido.Tables(0)
                msjRespuesta = Convert.ToString(CType(objRequest.Parameters(objRequest.Parameters.Count - 1), IDataParameter).Value)
            End If

            Return dsDatosPedido

        Catch ex As Exception
            Return dsDatosPedido
        End Try
    End Function
    'FIN - IDEA-141711 - Pack Internet Móvil Prepago

    Public Function ConsultarDatosDeliveryPedido(ByVal idPedido As Int64, _
                                                ByVal strCodProducto As String, _
                                                ByRef po_montopagar As String, _
                                                ByRef po_montopagarRA As String, _
                                                ByRef po_estadopedido As String, _
                                                ByRef po_montoPM As String, _
                                                ByRef po_codmsg As String, _
                                                ByRef po_mensaje As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        Dim strFlagReqBiometria As String = String.Empty

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                        New DAAB.DAABRequest.Parameter("PI_PEDIN_NRO_PEDIDO", DbType.Int64, idPedido, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("PI_PRODUCTO", DbType.String, strCodProducto, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("PO_MONTOPAGAR", DbType.Double, ParameterDirection.Output), _
                         New DAAB.DAABRequest.Parameter("PO_MONTO_RA", DbType.Double, ParameterDirection.Output), _
                        New DAAB.DAABRequest.Parameter("PO_ESTADOPEDIDO", DbType.String, ParameterDirection.Output), _
                        New DAAB.DAABRequest.Parameter("PO_FLAGREQBIOMETRIA", DbType.String, ParameterDirection.Output), _
                        New DAAB.DAABRequest.Parameter("PO_MONTO_PM", DbType.Double, ParameterDirection.Output), _
                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, ParameterDirection.Output), _
                        New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_DATOS_DLV_PEDIDO"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            Dim parSalida2 As IDataParameter
            Dim parSalida3 As IDataParameter
            Dim parSalida4 As IDataParameter
            Dim parSalida5 As IDataParameter
            Dim parSalida6 As IDataParameter
            Dim parSalida7 As IDataParameter
            Dim parSalida8 As IDataParameter

            parSalida2 = CType(objRequest.Parameters(2), IDataParameter)
            parSalida3 = CType(objRequest.Parameters(3), IDataParameter)
            parSalida4 = CType(objRequest.Parameters(4), IDataParameter)
            parSalida5 = CType(objRequest.Parameters(5), IDataParameter)
            parSalida6 = CType(objRequest.Parameters(6), IDataParameter)
            parSalida7 = CType(objRequest.Parameters(7), IDataParameter)
            parSalida8 = CType(objRequest.Parameters(8), IDataParameter)

            po_montopagar = Funciones.CheckStr(parSalida2.Value)
            po_montopagarRA = Funciones.CheckStr(parSalida3.Value)
            po_estadopedido = Funciones.CheckStr(parSalida4.Value)
            strFlagReqBiometria = Funciones.CheckStr(parSalida5.Value)
            po_montoPM = Funciones.CheckStr(parSalida6.Value) 'PROY-140582
            po_codmsg = Funciones.CheckStr(parSalida7.Value)
            po_mensaje = Funciones.CheckStr(parSalida8.Value)


        Catch ex As Exception
            po_codmsg = "-99"
            po_mensaje = String.Format("{0} => {1}", "Error: ConsultarDatosDeliveryPedido", Funciones.CheckStr(ex.Message))
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return strFlagReqBiometria
    End Function

 'PROY-140533 INI - JRM
    Public Function ConsultarReporte(ByVal K_FECHA As String, _
                                    ByVal K_COD_OFICINA As String, ByRef strCodRespuesta As String, ByRef strMsjRespuesta As String) As DataTable

        Dim msjRespuesta As String = String.Empty

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If


        Dim dsPool As DataSet
        Dim dsDT As DataTable

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_FECHA", DbType.String, 10, K_FECHA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_CODPDV", DbType.String, 50, K_COD_OFICINA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_DATOS_PAG", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, strCodRespuesta, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, 2000, strMsjRespuesta, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_PAGO.SSAPSS_PAGO_SRVSICAR"
            objRequest.Parameters.AddRange(arrParam)
            dsPool = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRespuesta = Convert.ToString(CType(objRequest.Parameters(objRequest.Parameters.Count - 2), IDataParameter).Value).Trim

            If strCodRespuesta = "0" Then
                dsDT = dsPool.Tables(0)
                msjRespuesta = Convert.ToString(CType(objRequest.Parameters(objRequest.Parameters.Count - 1), IDataParameter).Value)
            End If

            Return dsDT

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'PROY-140533 FIN - JRM


    'JRM - INI MEJORA DELIVERY
    Public Function ConsultarReporteRec(ByVal K_FECHAINI As String, _
                                        ByVal K_FECHAFIN As String, ByVal K_CODPDV As String, _
                                        ByVal K_MEDIOPAGO As String, ByVal K_TIPODOC As String, _
                                        ByVal K_NROPEDIDO As Int64, ByVal K_MONTOPAG As Decimal, _
                                        ByVal K_SERIE As String, ByVal K_CORRELATIVO As String, _
                                        ByVal K_IDCAMPO As Int32, ByVal K_REFERENCIA As String, _
                                        ByRef strCodRespuesta As String, ByRef strMsjRespuesta As String) As DataSet

        Dim msjRespuesta As String = String.Empty

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If


        Try
        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            'INI-936 - JCI - Modificados tipo de datos de los parametros PI_FECHA_INI y PI_FECHA_FIN de string a date
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_FECHA_INI", DbType.Date, Funciones.CheckDateNull(K_FECHAINI, "dd/MM/yyyy"), ParameterDirection.Input), _
                              New DAAB.DAABRequest.Parameter("PI_FECHA_FIN", DbType.Date, Funciones.CheckDateNull(K_FECHAFIN, "dd/MM/yyyy"), ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_CODPDV", DbType.String, 50, K_CODPDV, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_CODMEDIOPAGO", DbType.String, 50, K_MEDIOPAGO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_TIPODOC", DbType.String, 50, K_TIPODOC, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_NROPEDIDO", DbType.Int64, K_NROPEDIDO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_MONTOPAGADO", DbType.Decimal, K_MONTOPAG, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_SERIE", DbType.String, 50, K_SERIE, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_CORRELATIVO", DbType.String, 50, K_CORRELATIVO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_IDCAMPO", DbType.Int32, K_IDCAMPO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_VALOR", DbType.String, 50, K_REFERENCIA, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_DATOS_PAG", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, strCodRespuesta, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, 2000, strMsjRespuesta, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_PAGO.SSAPSS_REP_RECAUDACION"
            objRequest.Parameters.AddRange(arrParam)
            ds = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRespuesta = Convert.ToString(CType(objRequest.Parameters(objRequest.Parameters.Count - 2), IDataParameter).Value).Trim

            strMsjRespuesta = Convert.ToString(CType(objRequest.Parameters(objRequest.Parameters.Count - 1), IDataParameter).Value).Trim ' INI-936 - JCI - Agregada obtencion del mensaje de respuesta 

            ConsultarReporteRec = ds

        Catch ex As Exception
            'Inicio INI-936 - JCI - Agregada codigo y mensaje de respuesta en caso de error 
            strCodRespuesta = "-1"
            strMsjRespuesta = Funciones.CheckStr(ex.Message)
            'Fin INI-936 - JCI
            Return Nothing
        End Try
    End Function

    Public Function ObtenerDatosCombo() As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PO_CUR_MEDPAGO", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_CUR_ATRIB", DbType.Object, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_FORMAS_PAGO"
            objRequest.Parameters.AddRange(arrParam)
            ds = objRequest.Factory.ExecuteDataset(objRequest)

            ObtenerDatosCombo = ds

        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    Public Function ConsultarReporteAnulacionDelivery(ByVal K_FECHAINI As String, _
                                            ByVal K_FECHAFIN As String, ByVal K_CODPDV As String, _
                                            ByVal K_MEDIOPAGO As String, _
                                            ByVal K_NROPEDIDO As Int64, ByVal K_MONTOPAG As Decimal, _
                                            ByVal K_IDVenta As String, ByRef strCodRespuesta As String, _
                                            ByRef strMsjRespuesta As String) As DataSet

        Dim msjRespuesta As String = String.Empty

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If


        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_FECHA_INI", DbType.String, 10, K_FECHAINI, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_FECHA_FIN", DbType.String, 50, K_FECHAFIN, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_CODPDV", DbType.String, 50, K_CODPDV, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_CODMEDIOPAGO", DbType.String, 50, K_MEDIOPAGO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_NROPEDIDO", DbType.Int64, K_NROPEDIDO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_MONTOPAGADO", DbType.Decimal, K_MONTOPAG, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PI_IDVENTA", DbType.String, 50, K_IDVenta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PO_DATOS_REP", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, strCodRespuesta, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, 2000, strMsjRespuesta, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_REP_ANULACIONDELIVERY"
            objRequest.Parameters.AddRange(arrParam)
            ds = objRequest.Factory.ExecuteDataset(objRequest)

            strCodRespuesta = Convert.ToString(CType(objRequest.Parameters(objRequest.Parameters.Count - 2), IDataParameter).Value).Trim

            strMsjRespuesta = "OK"

            ConsultarReporteAnulacionDelivery = ds

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ListarOficinas(ByVal TipoRep As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")

        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_TIPO_REP", DbType.String, 3, TipoRep, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_CUR_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_PDVDELIVERY"
        objRequest.Parameters.AddRange(arrParam)
        ListarOficinas = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    'JRM - FIN MEJORA DELIVERY

    'JLOPETAS - PROY 140589 - INI
    Public Function validaPagoDLV(ByVal nroPedido As Int64, ByRef isCostoDLV As String, ByRef isPedCostoDLV As String, ByRef nroPedDLV As String, ByRef isPorta As String, ByRef flag_Porta As String, ByRef cod_rpta As String, ByRef msj_rpta As String) As ArrayList

        Dim oItem As ItemGenerico = Nothing
        Dim arrLista As New ArrayList
        Dim dr As IDataReader = Nothing

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")

        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If


        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_PEDIN_NROPEDIDO", DbType.Int64, nroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_FLAG_COSTO_DLV", DbType.Int64, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_FLAG_ISPEDIDO_COSTODLV", DbType.Int64, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_NROPED_COSTODLV", DbType.Int64, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_NROPED_ISPORTA", DbType.Int64, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_FLAG_PORTA", DbType.Int64, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_CUR_DATOS", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSG_RPTA", DbType.String, ParameterDirection.Output)}


        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_VALIDA_PAGODLV"
            objRequest.Parameters.AddRange(arrParam)
            'objRequest.Factory.ExecuteNonQuery(objRequest)
            'objRequest.Transactional = True
            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader



            If Not dr Is Nothing Then
                While dr.Read()
                    oItem = New ItemGenerico
                    oItem.CODIGO = Funciones.CheckStr(dr("PEDIV_NROPEDIDO"))
                    oItem.CODIGO2 = Funciones.CheckStr(dr("PEDIV_COD_TIPOVENTA"))
                    arrLista.Add(oItem)
                End While
            End If


            Dim parSalida1 As IDataParameter
            Dim parSalida2 As IDataParameter
            Dim parSalida3 As IDataParameter
            Dim parSalida4 As IDataParameter
            Dim parSalida5 As IDataParameter
            Dim parSalida7 As IDataParameter
            Dim parSalida8 As IDataParameter

            parSalida1 = CType(objRequest.Parameters(1), IDataParameter)
            parSalida2 = CType(objRequest.Parameters(2), IDataParameter)
            parSalida3 = CType(objRequest.Parameters(3), IDataParameter)
            parSalida4 = CType(objRequest.Parameters(4), IDataParameter)
            parSalida5 = CType(objRequest.Parameters(5), IDataParameter)
            parSalida7 = CType(objRequest.Parameters(7), IDataParameter)
            parSalida8 = CType(objRequest.Parameters(8), IDataParameter)

            isCostoDLV = Funciones.CheckStr(parSalida1.Value)
            isPedCostoDLV = Funciones.CheckStr(parSalida2.Value)
            nroPedDLV = Funciones.CheckStr(parSalida3.Value)
            isPorta = Funciones.CheckStr(parSalida4.Value)
            flag_Porta = Funciones.CheckStr(parSalida5.Value)
            cod_rpta = Funciones.CheckStr(parSalida7.Value)
            msj_rpta = Funciones.CheckStr(parSalida8.Value)

           
        Catch ex As Exception
            cod_rpta = "-99"
            msj_rpta = String.Format("{0} => {1}", "Error: AL validaPagoDLV", Funciones.CheckStr(ex.Message))
        Finally
            If Not dr Is Nothing AndAlso Not dr.IsClosed Then dr.Close()
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return arrLista
    End Function
    'JLOPETAS - PROY 140589 - FIN

#Region " INICIATIVA - 1006 | Tienda Virtual - ACCESORIO CON COSTO | Metodo validar si existe pedido de Acc con costo"

    Public Function validaPedidoAccCosto(ByVal NroPedido As Int64, ByRef NroPedidoACC As Int64, ByRef FlagPedidoACC As String, ByRef strEstadoPedidoACC As String, ByRef cod_rpta As String, ByRef msj_rpta As String)

        Dim dr As IDataReader = Nothing

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")

        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PI_NROPEDIDO", DbType.Int64, NroPedido, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("PO_PED_ACC_COSTO", DbType.Int64, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_EXISACC_COSTO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_ESTADO_PED", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_COD_RESULTADO", DbType.String, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("PO_MSJ_RESULTADO", DbType.String, ParameterDirection.Output)}


        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_CONSULTA_ACC_COSTO"
            'objRequest.Command = strCadenaEsquema & "PKG_CONSULTA_INI803.SSAPSS_CONSULTA_ACC_COSTO"
            objRequest.Parameters.AddRange(arrParam)
            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader

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

            NroPedidoACC = Funciones.CheckInt64(parSalida1.Value)
            FlagPedidoACC = Funciones.CheckStr(parSalida2.Value)
            strEstadoPedidoACC = Funciones.CheckStr(parSalida3.Value)
            cod_rpta = Funciones.CheckStr(parSalida4.Value)
            msj_rpta = Funciones.CheckStr(parSalida5.Value)

        Catch ex As Exception
            cod_rpta = "-99"
            msj_rpta = String.Format("{0} => {1}", "Error: AL validaPagoDLV", Funciones.CheckStr(ex.Message))
        Finally
            If Not dr Is Nothing AndAlso Not dr.IsClosed Then dr.Close()
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

    End Function

    Public Function ConsultarDatosDeliveryPedido2(ByVal idPedido As Int64, _
                                                  ByVal strCodProducto As String, _
                                                  ByRef po_codmsg As String, _
                                                  ByRef po_mensaje As String) As ArrayList

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))

        Dim dr As IDataReader = Nothing
        Dim oItem As ItemGenerico = Nothing
        Dim arrLista As New ArrayList

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                        New DAAB.DAABRequest.Parameter("PI_PEDIN_NRO_PEDIDO", DbType.Int64, idPedido, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("PI_PRODUCTO", DbType.String, strCodProducto, ParameterDirection.Input), _
                        New DAAB.DAABRequest.Parameter("PO_CUR_OUT", DbType.Object, ParameterDirection.Output), _
                        New DAAB.DAABRequest.Parameter("PO_COD_RPTA", DbType.String, ParameterDirection.Output), _
                        New DAAB.DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_CONSULTA.SSAPSS_DATOS_DLV_PEDIDO2"
            'objRequest.Command = strCadenaEsquema & "PKG_CONSULTA_INI803.SSAPSS_DATOS_DLV_PEDIDO2"
            objRequest.Parameters.AddRange(arrParam)
            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader

            Dim parSalida1 As IDataParameter
            Dim parSalida2 As IDataParameter

            parSalida1 = CType(objRequest.Parameters(3), IDataParameter)
            parSalida2 = CType(objRequest.Parameters(4), IDataParameter)


            po_codmsg = Funciones.CheckStr(parSalida1.Value)
            po_mensaje = Funciones.CheckStr(parSalida2.Value)


            If Not dr Is Nothing Then
                While dr.Read()
                    oItem = New ItemGenerico
                    oItem.MONTO_PAGAR = Funciones.CheckStr(dr("MONTO_TOT_PAG"))
                    oItem.MONTO_PAGAR_RA = Funciones.CheckStr(dr("MONTO_RA"))
                    oItem.ESTADO_PEDIDO = Funciones.CheckStr(dr("ESTADO_PEDIDO"))
                    oItem.FLAG_BIOMETRIA = Funciones.CheckStr(dr("FLAG_BIOMETRIA"))
                    oItem.MONTO_PM = Funciones.CheckStr(dr("MONTO_PM"))
                    oItem.MONTO_ACC_COSTO = Funciones.CheckStr(dr("MONTO_ACC_COSTO"))
                    arrLista.Add(oItem)
                End While
            End If

        Catch ex As Exception
            po_codmsg = "-99"
            po_mensaje = String.Format("{0} => {1}", "Error: ConsultarDatosDeliveryPedido", Funciones.CheckStr(ex.Message))
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return arrLista
    End Function

#End Region

End Class
