Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB

Public Class ClsActivacionPel


    Dim strCadenaConexion As String
    Dim strCadenaEsquema As String = ""
    Dim objSeg As New Seguridad_NET.clsSeguridad
    Public objsegu As COM_SIC_Seguridad.Configuracion
    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogReservaActivacion")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogReservaActivacion")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)


    Function Lis_Lista_Detalle_Venta_Prepago(ByVal strDocumentoSAP As String, ByRef strCodigoResp As String) As ArrayList

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
               New DAABRequest.Parameter("P_NRO_DOC_SAP", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_RESULT", DbType.String, ParameterDirection.Output), _
               New DAABRequest.Parameter("P_LISTADO", DbType.Object, ParameterDirection.Output) _
               }


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next


        arrParam(0).Value = strDocumentoSAP


        Dim dr As IDataReader = Nothing
        Dim arrListaDoc As New ArrayList
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_PREPAGO2_S6.sisact_detalle_select"
            objRequest.Parameters.AddRange(arrParam)
            'objRequest.Factory.ExecuteNonQuery(objRequest)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            While (dr.Read())
                Dim item As New DetalleVentaPrepago
                item.ID = Funciones.CheckStr(dr(0))
                item.TIPO_DOC = Funciones.CheckStr(dr(1))
                item.NUM_DOC = Funciones.CheckStr(dr(2))
                item.COD_CAN = Funciones.CheckStr(dr(3))
                item.COD_PDV = Funciones.CheckStr(dr(4))
                item.TIPO_VENT = Funciones.CheckStr(dr(5))
                item.TIPO_OPERACION = Funciones.CheckStr(dr(6))
                item.TIPO_PROD = Funciones.CheckStr(dr(7))
                item.COD_DEPA = Funciones.CheckStr(dr(8))
                item.COD_VEN_PEL = Funciones.CheckStr(dr(9))
                item.COD_PDV_PEL = Funciones.CheckStr(dr(10))
                item.COD_PROD = Funciones.CheckStr(dr(11))
                item.NU_SECU = Funciones.CheckStr(dr(12))
                item.FLAG_PACK = Funciones.CheckStr(dr(13))
                item.SERIE_CHIP = Funciones.CheckStr(dr(14))
                item.COD_MATERIAL_CHIP = Funciones.CheckStr(dr(15))
                item.SERIE_EQUI = Funciones.CheckStr(dr(16))
                item.COD_MATERIAL_EQUI = Funciones.CheckStr(dr(17))
                item.COD_CAMPANA = Funciones.CheckStr(dr(18))
                item.COD_LISTA_PRE = Funciones.CheckStr(dr(19))
                item.COD_PROMOCION = Funciones.CheckStr(dr(20))
                item.COD_PLAN = Funciones.CheckStr(dr(21))
                item.COD_PROD_PREP = Funciones.CheckStr(dr(22))
                item.LINEA = Funciones.CheckStr(dr(23))
                item.COD_ACTIVACION = Funciones.CheckStr(dr(24))
                item.COD_TXN = Funciones.CheckStr(dr(25))
                item.NUM_OPER = Funciones.CheckStr(dr(26))
                item.IMSI = Funciones.CheckStr(dr(27))
                item.TIPO_ACTI = Funciones.CheckStr(dr(28))
                item.TIPO_ACTI_V2 = Funciones.CheckStr(dr(29))
                item.DESCRIPCION_PRODUCTO = Funciones.CheckStr(dr(30))
                item.CAR1 = Funciones.CheckStr(dr(31))
                arrListaDoc.Add(item)
            End While

        Catch ex As Exception
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return arrListaDoc


    End Function

    '---------------------------------------------------------------------------------

    Function DatoServicio(ByVal strCliente As String, ByVal strKeyRegEdit As String) As String()

        Dim mSigCom(2) As String
        'ClaroBDConfiguracion(); 

        Dim objUser As Object
        Dim strCadena As String
        Dim BaseDatos As String = ""
        Dim Usuario As String = ""
        Dim Password As String = ""
        Dim Servidor As String

        Dim objDatosConex As COM_SIC_Seguridad.ClsConexion
        objsegu = New COM_SIC_Seguridad.Configuracion(strKeyRegEdit)

        objDatosConex = objsegu.GetConexion()
        BaseDatos = objDatosConex.GetBD
        Usuario = objDatosConex.GetUsuario
        Password = objDatosConex.GetPassword
        Servidor = objDatosConex.GetServidor

        mSigCom(0) = Usuario
        mSigCom(1) = Password
        mSigCom(2) = BaseDatos

        Return mSigCom


    End Function

    '---------------------------------------------------------------------------------

    Public Function AltaPrepago(ByVal strSERIE_CHIP As String, ByVal strCOD_PDV As String, ByVal strCOD_DEPA As String, ByVal strOPCION As String, ByVal strCOD_VEN_PEL As String, _
                                ByVal strChipPack As String, ByVal strServicioAdicional5 As String, ByVal strCOD_PROD As String, ByVal strCOD_PLAN As String, _
                                ByVal strCOD_PROMOCION As String, ByVal strTIPO_PROD As String, ByVal strCAR5 As String, _
                                ByVal strCAR6 As String, ByVal strTRACE As String, ByVal strOperacionClaro As String, ByVal strCAR1 As String, _
                                ByRef strEstadoActivacion As String, ByRef strsDocumentoSap As String, ByRef strCodigoRespuesta As String, ByRef strDescripcionRespuesta As String) As String


        Dim strLinea As String
        Dim strAplicacion As String

        Try

            Dim strIdentifyLog As String = "ServicioPEL - " & strsDocumentoSap
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Servicio PEL")
            Dim _oTransaccion As New ServicioPEL.TransaccionService
            Dim obj As New ServicioPEL.reqAltaPrePago
            Dim respOperacion As New ServicioPEL.respOperacion

            '		DOLWS.DOLResponse _oDOLResponse = new Claro.SisAct.Negocios.DOLWS.DOLResponse(););



            _oTransaccion.Url = ConfigurationSettings.AppSettings("constRutaWSPEL").ToString()
            _oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
            _oTransaccion.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("constTimeOutWSPEL").ToString())

            obj.codigoProceso = ConfigurationSettings.AppSettings("CodigoProcesoPEL").ToString()

            strAplicacion = ConfigurationSettings.AppSettings("constPELCodAplicacion").ToString()

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Url Servicio PEL : " & _oTransaccion.Url)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codigo de Proceso PEL : " & obj.codigoProceso)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Aplicacion : " & strAplicacion)

            '--- REGEDIT ---//
            Dim _objServicio() As String
            _objServicio = DatoServicio("2", strAplicacion)
            obj.usuario = _objServicio(0).ToString() ' usuario
            obj.password = _objServicio(1).ToString() ' password
            obj.terminalID = _objServicio(2).ToString() ' base de datos
            '--- REGEDIT ---//

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio _objServicio strAplicacion : " & strAplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio _objServicio usuario : " & obj.usuario)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio _objServicio password : " & obj.password)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Servicio PEL Terminal : " & obj.terminalID)



            obj.canal = ConfigurationSettings.AppSettings("CanalPEL").ToString()
            obj.binCanalInterno = ConfigurationSettings.AppSettings("CanalInternoPEL").ToString()
            obj.comercio = ConfigurationSettings.AppSettings("ComercioPEL").ToString()
            obj.iccid = strSERIE_CHIP
            obj.pdv = strCOD_PDV
            obj.departamento = strCOD_DEPA
            '            obj.car1 = ConfigurationSettings.AppSettings("car1").ToString()
            obj.car1 = strCAR1
            obj.servicioAdicional1 = strOPCION
            obj.servicioAdicional2 = strCOD_VEN_PEL
            obj.servicioAdicional3 = ConfigurationSettings.AppSettings("Ciudad").ToString()
            obj.servicioAdicional4 = strChipPack
            obj.servicioAdicional5 = strServicioAdicional5
            obj.producto = strCOD_PROD
            obj.plan = strCOD_PLAN
            obj.car2 = strSERIE_CHIP
            obj.car3 = strCOD_PROMOCION
            obj.tipoDocumento = "_"
            obj.serie = "_"
            obj.correlativo = "_"
            obj.car4 = strTIPO_PROD
            obj.car5 = strCAR5
            obj.car6 = strCAR6
            obj.car7 = "_"
            obj.car8 = "_"
            obj.trace = strTRACE
            obj.numOperacionClaro = strOperacionClaro

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Canal PEL : " & obj.canal)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Canal Interno PEL : " & obj.binCanalInterno)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Comercio PEL : " & obj.comercio)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ICCID del CHIP : " & obj.iccid)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codigo Punto Venta PEL : " & obj.pdv)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codigo Departamento : " & obj.departamento)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Car1 : " & obj.car1)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Servicio Adicional 1 : " & obj.servicioAdicional1)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Servicio Adicional 2 : " & obj.servicioAdicional2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Servicio Adicional 3 : " & ConfigurationSettings.AppSettings("Ciudad").ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Servicio Adicional 4 : " & strChipPack)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Servicio Adicional 5 : " & strServicioAdicional5)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codigo Producto : " & obj.producto)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codigo Plan : " & obj.plan)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Car2 : " & obj.car2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Car3 : " & obj.car3)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Tipo Documento : " & "_")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Serie : " & "_")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Correlativo : " & "_")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Car4 : " & strTIPO_PROD)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Car5 : " & strCAR5)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Car6 : " & strCAR6)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Car7 : " & "_")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Car8 : " & "_")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Trace : " & strTRACE)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Numero Operacion Claro : " & strOperacionClaro)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Total Parametros : " & obj.binCanalInterno & " / " & obj.canal & " / " & obj.car1 & " / " & obj.car2 & " / " & obj.car3 & " / " & strTIPO_PROD & " / " & strCAR5 & " / " & strCAR6 & " / " & "_" & " / " & "_" & " / " & obj.codigoProceso & " / " & obj.comercio & " / " & "_" & " / " & obj.departamento & " / " & obj.iccid & " / " & strOperacionClaro & " / " & obj.password & " / " & obj.pdv & " / " & obj.plan & " / " & obj.producto & " / " & "_" & " / " & obj.servicioAdicional1 & " / " & obj.servicioAdicional2 & " / " & ConfigurationSettings.AppSettings("Ciudad").ToString() & " / " & strChipPack & " / " & strServicioAdicional5 & " / " & obj.terminalID & " / " & "_" & " / " & strTRACE & " / " & obj.usuario)
            'Invocacion Metodo
            respOperacion = _oTransaccion.AltaPrepago(obj)

            strEstadoActivacion = respOperacion.estadoActivacion.ToString()
            strCodigoRespuesta = respOperacion.codigoRespuesta.ToString()
            strDescripcionRespuesta = respOperacion.descripcionRespuesta.ToString()
            strLinea = respOperacion.msisdn.ToString()

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Estado de Activacion : " & strEstadoActivacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codido de Respuesta : " & strCodigoRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Descripcion Respuesta : " & strDescripcionRespuesta)

            obj = Nothing
            respOperacion = Nothing
            _oTransaccion.Dispose()



        Catch ex As Exception

            strEstadoActivacion = "999"
            strCodigoRespuesta = "999"
            strLinea = ""
            strDescripcionRespuesta = ex.Message
            Dim strIdentifyLog As String = "ServicioPEL - " & strsDocumentoSap
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Estado de Activacion : " & strEstadoActivacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codido de Respuesta : " & strCodigoRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Descripcion Respuesta : " & strDescripcionRespuesta)
            Return strLinea
        End Try

        Return strLinea


    End Function

    '---------------------------------------------------------------------------------

    Public Function actualizaTelefono(ByVal p_serie As String, ByVal p_codigo As String, ByVal p_linea As String) As Boolean


        'strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim bResultado As Boolean = False

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
               New DAABRequest.Parameter("P_SERIE", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_CODIGO", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_LINEA", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_RESULTADO", DbType.Int32, ParameterDirection.Output) _
               }


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(0).Value = p_serie
        arrParam(1).Value = p_codigo
        arrParam(2).Value = p_linea

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTA_PREPAGO.SISACTU_NROTELEFONO"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)

        Try

            objRequest.Factory.ExecuteNonQuery(objRequest)
            Dim pSalida As IDataParameter = CType(objRequest.Parameters(3), IDataParameter).Value
            Dim DatoRetorno As Integer = Funciones.CheckInt(pSalida.Value)

            If DatoRetorno > 0 Then
                bResultado = True
            Else
                bResultado = False
            End If


        Catch ex As Exception
            Return False
        Finally

        End Try


        Return bResultado

    End Function

    '---------------------------------------------------------------------------------

    Public Function actualizaErrorActivacionPEL(ByVal p_serie As String, ByVal p_codigo As String, ByVal p_codigoError As String, ByVal p_descripcionError As String) As Boolean


        'strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim bResultado As Boolean = False

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
              New DAABRequest.Parameter("P_SERIE", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_CODIGO", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_CODIGOERROR", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_DESCRIPCIONERROR", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_RESULTADO", DbType.Int32, ParameterDirection.Output) _
               }


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(0).Value = p_serie
        arrParam(1).Value = p_codigo
        arrParam(2).Value = p_codigoError
        arrParam(3).Value = p_descripcionError

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTA_PREPAGO2.SISACTU_ERRORPEL"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)

        Try

            objRequest.Factory.ExecuteNonQuery(objRequest)
            Dim pSalida As IDataParameter = CType(objRequest.Parameters(4), IDataParameter).Value
            Dim DatoRetorno As Integer = Funciones.CheckInt(pSalida.Value)

            If DatoRetorno > 0 Then
                bResultado = True
            Else
                bResultado = False
            End If


        Catch ex As Exception
            Return False
        Finally

        End Try


        Return bResultado

    End Function

    '---------------------------------------------------------------------------------

    Public Function ListarDatosCabeceraVenta(ByVal strDocumentoSAP As String) As DataTable


        Dim dtbLista As New DataTable
        dtbLista.Columns.Add("VEPR_NOM_CLIE")
        dtbLista.Columns.Add("VEPR_APE_CLIE")
        dtbLista.Columns.Add("VEPR_NUM_DOC")
        dtbLista.Columns.Add("VEPR_TIPO_DOC")
        dtbLista.Columns.Add("VEPR_COD_VEN")

        'strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                  New DAABRequest.Parameter("P_ID", DbType.String, ParameterDirection.Input), _
                                                  New DAABRequest.Parameter("P_RESULT", DbType.Int32, ParameterDirection.Output), _
                                                  New DAABRequest.Parameter("P_LISTADO", DbType.Object, ParameterDirection.Output) _
               }


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(0).Value = strDocumentoSAP

        Dim dr As IDataReader = Nothing
        Dim arrListaDoc As New ArrayList
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_VENTA_PREPAGO2.SISACTS_CON_VPREPAGO"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
            While (dr.Read())

                Dim drNewRow As DataRow = dtbLista.NewRow
                drNewRow("VEPR_NOM_CLIE") = Funciones.CheckStr(dr("VEPR_NOM_CLIE"))
                drNewRow("VEPR_APE_CLIE") = Funciones.CheckStr(dr("VEPR_APE_CLIE"))
                drNewRow("VEPR_NUM_DOC") = Funciones.CheckStr(dr("VEPR_NUM_DOC"))
                drNewRow("VEPR_TIPO_DOC") = Funciones.CheckStr(dr("VEPR_TIPO_DOC"))
                drNewRow("VEPR_COD_VEN") = Funciones.CheckStr(dr("VEPR_COD_VEN"))

                dtbLista.Rows.Add(drNewRow)


            End While

        Catch ex As Exception
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return dtbLista


    End Function

    '---------------------------------------------------------------------------------
    Public Function ActivacionLinea(ByVal strMsisdn As String, ByVal strIccid As String, ByVal strImsi As String, ByVal strImei As String, ByVal strTipoPreActivacion As String, _
                                    ByVal strTipoActivacion As String, ByVal strPromocion As String, ByVal strCodigoMaterial As String, ByVal strPuntoVenta As String, _
                                    ByVal strDocumentoSAP As String, ByRef strMensajeRespuesta As String) As String

        Dim strRespuesta As String = ""
        Dim strIdTransaccion As String = strDocumentoSAP
        'Dim HelperLog As New SICAR_Log


        Dim strIdentifyLog As String = "ServicioNuevoPEL - " & strDocumentoSAP
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Servicio NuevoPEL")
        Try

            Dim _oTransaccion As New ServicioNuevoPEL.ebsActivacionNuevoPELService
            _oTransaccion.Url = ConfigurationSettings.AppSettings("constRutaWSPELNew").ToString()
            _oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
            _oTransaccion.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("constTimeOutWSPELNew").ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Url Servicio NuevoPEL : " & _oTransaccion.Url)

            Dim strUsrAplicacion As String = ConfigurationSettings.AppSettings("constDatoAplicacionWSPELNew").ToString()
            Dim strIpAplicacion As String = ConfigurationSettings.AppSettings("constIpAplicacionWSPELNew").ToString()
            Dim strTipoAlta As String = ConfigurationSettings.AppSettings("constTipoAltaWSPELNew").ToString()
            Dim strAppOrigen As String = ConfigurationSettings.AppSettings("constAppOrigenWSPELNew").ToString()
            Dim strFlujoActivacion As String = ConfigurationSettings.AppSettings("constFlujoActiWSPELNew").ToString()

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Usuario Aplicacion : " & strUsrAplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Ip Aplicacion : " & strIpAplicacion)

            '			string strTipoPreActivacion = ConfigurationSettings.AppSettings("constPreActivPrepInternet").ToString()
            '			string strTipoActivacion = ConfigurationSettings.AppSettings("constActivModemPrepago").ToString()

            'HelperLog.CrearArchivolog("ServicioPelNegocios", "IdTransaccion: " + strIdTransaccion, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "Msisdn: " + strMsisdn, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "Iccid: " + strIccid, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "Imsi: " + strImsi, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "Imei: " + strImei, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "TipoPreActivacion: " + strTipoPreActivacion, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "TipoActivacion: " + strTipoActivacion, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "Promocion: " + strPromocion, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "TipoAlta: " + strTipoAlta, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "AppOrigen: " + strAppOrigen, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "CodigoMaterial: " + strCodigoMaterial, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "PuntoVenta: " + strPuntoVenta, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "FlujoActivacion: " + strFlujoActivacion, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "Total Parametros: " + strIdTransaccion + "/" + strUsrAplicacion + "/" + strIpAplicacion + "/" + strMsisdn + "/" + strIccid + "/" + strImsi + "/" + strImei + "/" + strTipoPreActivacion + "/" + strTipoActivacion + "/" + strPromocion + "/" + strTipoAlta + "/" + strAppOrigen + "/" + strCodigoMaterial + "/" + strPuntoVenta + "/" + strFlujoActivacion, "", "", "")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Transaccion : " & strIdTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Msisdn : " & strMsisdn)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Iccid : " & strIccid)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Imsi : " & strImsi)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "TipoPreActivacion : " & strTipoPreActivacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "TipoActivacion : " & strTipoActivacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Promocion : " & strPromocion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "TipoAlta : " & strTipoAlta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "AppOrigen : " & strAppOrigen)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "CodigoMaterial : " & strCodigoMaterial)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PuntoVenta : " & strPuntoVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FlujoActivacion : " & strFlujoActivacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Total Parametros : " & strIdTransaccion & " / " & strUsrAplicacion & " / " & strIpAplicacion & " / " & strMsisdn & " / " & strIccid & " / " & strImsi & " / " & strImei & " / " & strTipoPreActivacion & " / " & strTipoActivacion & " / " & strPromocion & " / " & strTipoAlta & " / " & strAppOrigen & " / " & strCodigoMaterial & " / " & strPuntoVenta & " / " & strFlujoActivacion)
            strRespuesta = _oTransaccion.ejecutarActivacion(strIdTransaccion, strUsrAplicacion, strIpAplicacion, strMsisdn, strIccid, strImsi, strImei, strTipoPreActivacion, strTipoActivacion, strPromocion, strTipoAlta, strAppOrigen, strCodigoMaterial, strPuntoVenta, strFlujoActivacion, strMensajeRespuesta)

            'HelperLog.CrearArchivolog("ServicioPelNegocios", "Respuesta: " + strRespuesta, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "MensajeRespuesta: " + strMensajeRespuesta, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "IdTransaccion: " + strIdTransaccion, "", "", "")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Respuesta : " & strRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MensajeRespuesta : " & strMensajeRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "IdTransaccion : " & strIdTransaccion)


            _oTransaccion.Dispose()


        Catch ex As Exception

            'HelperLog.CrearArchivolog("ServicioPelNegocios", "Respuesta: " + strRespuesta, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "IdTransaccion: " + strIdTransaccion, "", "", "")
            'HelperLog.CrearArchivolog("ServicioPelNegocios", "Mensaje de error: " + ex.Message, "", "", "")
            strRespuesta = "999"
            strMensajeRespuesta = "Error en la Activación Nuevo PEL"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Respuesta : " & strRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje de Respuesta : " & strMensajeRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "IdTransaccion : " & strIdTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje de error : " & ex.Message)

        End Try

        Return strRespuesta



    End Function

    '---------------------------------------------------------------------------------

    Public Function ConsultaGrabarDatosActivacion(ByVal strTipoDocumento As String, ByVal strNumeroDocumento As String, _
                                                  ByVal strMsisdn As String, ByVal strPuntoVenta As String, ByVal strVendedor As String, ByVal strOperacionClaro As String, _
                                                  ByRef strCodigoRespuesta As String, ByRef strDescripcionRespuesta As String) As String

        Dim strIdentifyLog As String = "ConsultaGrabarDatosActivacion"
        Dim key As String = ConfigurationSettings.AppSettings("BD_SIXPROV")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio BD_SIXPROV")
        strCadenaConexion = objSeg.FP_GetConnectionString("2", key)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strCadenaConexion- " & strCadenaConexion)
        Dim esquema As String = ConfigurationSettings.AppSettings("EsquemaSIXPROV")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "esquema- " & esquema)


        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim bResultado As Boolean = False

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
             New DAABRequest.Parameter("p_comercio", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_CODTXN", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_iccid", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_imei", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_msisdn", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_codPtoVenta", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_codVendedor", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_car1", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_car2", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_car3", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_car4", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_car5", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_car6", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_car7", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_car8", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_tipo_documento", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_serie", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_correlativo", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_numOperacionClaro", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_TipoDoc", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("p_Documento", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("Po_CODRESP", DbType.String, ParameterDirection.Output), _
               New DAABRequest.Parameter("Po_DESCRESP", DbType.String, ParameterDirection.Output) _
               }


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next




        Dim lstReservaNumero As New ReservaNumero
        lstReservaNumero = ConsultaDatosReserva(strMsisdn, strPuntoVenta, strVendedor)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.CodigoRespuesta- " & lstReservaNumero.CodigoRespuesta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.DescripcionRespuesta- " & lstReservaNumero.DescripcionRespuesta)
        If (lstReservaNumero.CodigoRespuesta <> "00") Then

            strCodigoRespuesta = lstReservaNumero.CodigoRespuesta
            strDescripcionRespuesta = lstReservaNumero.DescripcionRespuesta

            Return strCodigoRespuesta

        End If

        strCodigoRespuesta = lstReservaNumero.CodigoRespuesta
        strDescripcionRespuesta = lstReservaNumero.DescripcionRespuesta




        arrParam(0).Value = ConfigurationSettings.AppSettings("constComercioBAM").ToString()
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "p_comercio- " & ConfigurationSettings.AppSettings("constComercioBAM").ToString())
        arrParam(1).Value = lstReservaNumero.CodigoTxn
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.CodigoTxn - " & lstReservaNumero.CodigoTxn)
        arrParam(2).Value = lstReservaNumero.Iccid
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.Iccid -" & lstReservaNumero.Iccid)
        arrParam(3).Value = lstReservaNumero.Imei
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.Imei - " & lstReservaNumero.Imei)
        arrParam(4).Value = strMsisdn
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strMsisdn -" & strMsisdn)
        arrParam(5).Value = strPuntoVenta
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strPuntoVenta - " & strPuntoVenta)
        arrParam(6).Value = strVendedor
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strVendedor - " & strVendedor)
        arrParam(7).Value = lstReservaNumero.TipoActivacion
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.TipoActivacion : " & lstReservaNumero.TipoActivacion)
        arrParam(8).Value = lstReservaNumero.Iccid
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.Iccid - " & lstReservaNumero.Iccid)
        arrParam(9).Value = lstReservaNumero.Promocion
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.Promocion -" & lstReservaNumero.Promocion)
        arrParam(10).Value = lstReservaNumero.CodTipoProducto
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.CodTipoProducto - " & lstReservaNumero.CodTipoProducto)
        arrParam(11).Value = lstReservaNumero.CodigoAdicional1
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.CodigoAdicional1 - " & lstReservaNumero.CodigoAdicional1)
        arrParam(12).Value = lstReservaNumero.CodigoAdicional2
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.CodigoAdicional2 - " & lstReservaNumero.CodigoAdicional2)
        arrParam(13).Value = lstReservaNumero.Car7
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.Car7 - " & lstReservaNumero.Car7)
        arrParam(14).Value = lstReservaNumero.Car8
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.Car8 - " & lstReservaNumero.Car8)
        arrParam(15).Value = lstReservaNumero.CodDocumento
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.CodDocumento" & "lstReservaNumero.CodDocumento")
        arrParam(16).Value = lstReservaNumero.CodSerie
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "lstReservaNumero.CodSerie - " & lstReservaNumero.CodSerie)
        arrParam(17).Value = "_"
        arrParam(18).Value = strOperacionClaro
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strOperacionClaro - " & strOperacionClaro)
        arrParam(19).Value = strTipoDocumento
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strTipoDocumento - " & strTipoDocumento)
        arrParam(20).Value = strNumeroDocumento
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strNumeroDocumento - " & strNumeroDocumento)

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = esquema & ".CLARO003_IVR_HP.SEGPROV_ACT_DAT_ACTIVACION"
        objRequest.Parameters.AddRange(arrParam)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Envio Parametros - " & esquema & ".CLARO003_IVR_HP.SEGPROV_ACT_DAT_ACTIVACION")

        Try

            objRequest.Factory.ExecuteNonQuery(objRequest)

            strCodigoRespuesta = Convert.ToString(CType(objRequest.Parameters(21), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strCodigoRespuesta - " & strCodigoRespuesta)

            strDescripcionRespuesta = Convert.ToString(CType(objRequest.Parameters(22), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strDescripcionRespuesta - " & strDescripcionRespuesta)


        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Respuesta - " & ex.Message.ToString)
            Return False
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try


        Return strCodigoRespuesta


    End Function

    '---------------------------------------------------------------------------------

    Public Function ConsultaDatosReserva(ByVal strMsisdn As String, ByVal strPuntoVenta As String, ByVal strVendedor As String) As ReservaNumero

        Dim strIdentifyLog As String = "ConsultaDatosReserva"
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio BD_SIXPROV")
        Dim key As String = ConfigurationSettings.AppSettings("BD_SIXPROV")
        strCadenaConexion = objSeg.FP_GetConnectionString("2", key)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strCadenaConexion - " & strCadenaConexion)

        Dim esquema As String = ConfigurationSettings.AppSettings("EsquemaSIXPROV")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "esquema - " & esquema)

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim bResultado As Boolean = False

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                    New DAABRequest.Parameter("P_msisdn", DbType.String, ParameterDirection.Input), _
         New DAABRequest.Parameter("P_puntoVenta", DbType.String, ParameterDirection.Input), _
         New DAABRequest.Parameter("P_codVendedor", DbType.String, ParameterDirection.Input), _
         New DAABRequest.Parameter("Po_CODRESP", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_CODTXN", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_DESCRESP", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_iccid", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_departamento", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_car1", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_servicioAdicional1", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_servicioAdicional3", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_servicioAdicional4", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_servicioAdicional5", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_producto", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_plan", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_tipo_documento", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_serie", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_correlativo", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_car2", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_car3", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_car4", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_car5", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_car6", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_car7", DbType.String, ParameterDirection.Output), _
         New DAABRequest.Parameter("Po_car8", DbType.String, ParameterDirection.Output)}


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next


        arrParam(0).Value = strMsisdn
        arrParam(1).Value = strPuntoVenta
        arrParam(2).Value = strVendedor


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = esquema & ".CLARO003_IVR_HP.SEGPROV_CONSULTA_DATOS_RESERVA"
        objRequest.Parameters.AddRange(arrParam)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Envio Parametros - " & esquema & ".CLARO003_IVR_HP.SEGPROV_CONSULTA_DATOS_RESERVA")
        Dim item As New ReservaNumero

        Try

            objRequest.Factory.ExecuteNonQuery(objRequest)

            item.CodigoRespuesta = Convert.ToString(CType(objRequest.Parameters(3), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.CodigoRespuesta - " & item.CodigoRespuesta)

            item.CodigoTxn = Convert.ToString(CType(objRequest.Parameters(4), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.CodigoTxn - " & item.CodigoTxn)

            item.DescripcionRespuesta = Convert.ToString(CType(objRequest.Parameters(5), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.DescripcionRespuesta - " & item.DescripcionRespuesta)

            item.Iccid = Convert.ToString(CType(objRequest.Parameters(6), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.Iccid - " & item.Iccid)

            item.Departamento = Convert.ToString(CType(objRequest.Parameters(7), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.Departamento - " & item.Departamento)

            item.TipoActivacion = Convert.ToString(CType(objRequest.Parameters(8), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.TipoActivacion - " & item.TipoActivacion)

            item.Opcion = Convert.ToString(CType(objRequest.Parameters(9), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.Opcion- " & item.Opcion)

            item.Ciudad = Convert.ToString(CType(objRequest.Parameters(10), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.Ciudad - " & item.Ciudad)

            Dim chipPak As String = Convert.ToString(CType(objRequest.Parameters(11), IDataParameter).Value)
            If (chipPak = "" Or chipPak Is dbnull.Value) Then
                item.ChipPack = " "
            Else
            item.ChipPack = Convert.ToString(CType(objRequest.Parameters(11), IDataParameter).Value)
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.ChipPack - " & item.ChipPack)

            item.Imei = Convert.ToString(CType(objRequest.Parameters(12), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.Imei - " & item.Imei)

            item.CodProducto = Convert.ToString(CType(objRequest.Parameters(13), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.CodProducto- " & item.CodProducto)

            item.CodPlan = Convert.ToString(CType(objRequest.Parameters(14), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.CodPlan - " & item.CodPlan)

            item.CodDocumento = Convert.ToString(CType(objRequest.Parameters(15), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.CodDocumento - " & item.CodDocumento)

            item.CodSerie = Convert.ToString(CType(objRequest.Parameters(16), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.CodSerie - " & item.CodSerie)

            item.CodPlan = Convert.ToString(CType(objRequest.Parameters(17), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "iitem.CodPlan- " & item.CodPlan)

            item.Iccid = Convert.ToString(CType(objRequest.Parameters(18), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.Iccid - " & item.Iccid)

            item.Promocion = Convert.ToString(CType(objRequest.Parameters(19), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.Promocion - " & item.Promocion)

            item.CodTipoProducto = Convert.ToString(CType(objRequest.Parameters(20), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.CodTipoProducto - " & item.CodTipoProducto)

            item.CodigoAdicional1 = Convert.ToString(CType(objRequest.Parameters(21), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.CodigoAdicional1 - " & item.CodigoAdicional1)

            item.CodigoAdicional2 = Convert.ToString(CType(objRequest.Parameters(22), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.CodigoAdicional2 - " & item.CodigoAdicional2)

            item.Car7 = Convert.ToString(CType(objRequest.Parameters(23), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.Car7 - " & item.Car7)

            item.Car8 = Convert.ToString(CType(objRequest.Parameters(24), IDataParameter).Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "item.Car8 - " & item.Car8)

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Respuesta - " & ex.Message.ToString)
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

        Return item

    End Function

    '---------------------------------------------------------------------------------

    Public Function AnularPagoReposicionPrepagoSisact(ByVal vDocumentoSap As String, ByVal strEstado As String) As Boolean

        'strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim bResultado As Boolean = False

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
              New DAABRequest.Parameter("P_VALOR_RETORNO", DbType.Int32, ParameterDirection.Output), _
              New DAABRequest.Parameter("P_DOCUMENTO_SAP", DbType.String, ParameterDirection.Input), _
              New DAABRequest.Parameter("P_ESTADO_REGISTRO", DbType.String, ParameterDirection.Input) _
               }


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        arrParam(1).Value = vDocumentoSap
        arrParam(2).Value = strEstado

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_VENTA_REPOSICION.SISACTSU_ANULA_VENTA_REPO"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)

        Try

            objRequest.Factory.ExecuteNonQuery(objRequest)
            Dim pSalida As IDataParameter = CType(objRequest.Parameters(4), IDataParameter).Value
            Dim DatoRetorno As Integer = Funciones.CheckInt(pSalida.Value)

            If (DatoRetorno = 1) Then
                bResultado = True
            Else
                bResultado = False
            End If



        Catch ex As Exception
            Return False
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()

        End Try


        Return bResultado


    End Function

    '---------------------------------------------------------------------------------

    Public Function GrabarErrorActivacionPel(ByVal strSERIE_CHIP As String, ByVal strCOD_PDV As String, ByVal strCOD_DEPA As String, ByVal strOPCION As String, ByVal strCOD_VEN_PEL As String, _
                                ByVal strChipPack As String, ByVal strServicioAdicional5 As String, ByVal strCOD_PROD As String, ByVal strCOD_PLAN As String, _
                                ByVal strCOD_PROMOCION As String, ByVal strTIPO_PROD As String, ByVal strCAR5 As String, _
                                ByVal strCAR6 As String, ByVal strTRACE As String, ByVal strOperacionClaro As String, _
                                ByVal strEstadoActivacion As String, ByVal strCodigoRespuesta As String, ByVal strDescripcionRespuesta As String, ByVal strCAR1 As String, ByVal strLinea As String, _
                                ByRef strNOM_CLI As String, ByRef strAPE_CLI As String, ByRef strTIPO_DOC As String, ByRef strNUM_DOC As String, ByRef strTEL_REF As String, _
                                ByRef str_PROCESO As String, ByRef strDocumentoSap As String, ByRef strRespuesta As String) As Boolean


        Dim strIdentifyLog As String = "ServicioPEL - " & strDocumentoSap
        'strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Grabar PEL - TIMEAI")
        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_EAI"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cadena de conexion TIMEAI - " & strCadenaConexion)
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim esquema As String = ConfigurationSettings.AppSettings("EsquemaEAI")

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Esquema - " & esquema)

        Dim bResultado As Boolean = False

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
               New DAABRequest.Parameter("P_SERRV_SERIE_CHIP", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_COD_PDV", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_COD_DEPA", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_OPCION", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_COD_VEN_PEL", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_CHIPPACK", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_SERVICIOADICIONAL5", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_COD_PROD", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_COD_PLAN", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_COD_PROMOCION", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_TIPO_PROD", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_CAR5", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_CAR6", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_TRACE_", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_OPERACIONCLARO", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_ESTADOACTIVACION", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_CODIGORESPUESTA", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_DESCRIPCIONRESPUESTA", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_CAR1", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_MSISDN", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRC_FLAG_ENVIO", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_NOMBRES", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_APELLIDOS", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_TIPODOC", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_NRODOC", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_TELEF", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRC_ESTADOPROC", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("codError", DbType.String, ParameterDirection.Output), _
               New DAABRequest.Parameter("desError", DbType.String, ParameterDirection.Output) _
        }


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_SERIE_CHIP - " & strSERIE_CHIP)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_COD_PDV - " & strCOD_PDV)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_COD_DEPA - " & strCOD_DEPA)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_OPCION - " & strOPCION)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_COD_VEN_PEL - " & strCOD_VEN_PEL)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_CHIPPACK - " & strChipPack)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_SERVICIOADICIONAL5 - " & strServicioAdicional5)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_COD_PROD - " & strCOD_PROD)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_COD_PLAN - " & strCOD_PLAN)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_COD_PROMOCION - " & strCOD_PROMOCION)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_TIPO_PROD - " & strTIPO_PROD)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_CAR5 - " & strCAR5)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_CAR6 - " & strCAR6)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_TRACE_ - " & strTRACE)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_OPERACIONCLARO - " & strOperacionClaro)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_ESTADOACTIVACION - " & strEstadoActivacion)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_CODIGORESPUESTA - " & strCodigoRespuesta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_DESCRIPCIONRESPUESTA - " & strDescripcionRespuesta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_CAR1 - " & strCAR1)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_MSISDN - " & strLinea)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRC_FLAG_ENVIO - " & ConfigurationSettings.AppSettings("Flag_Envio"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_NOMBRES - " & strNOM_CLI)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_APELLIDOS - " & strAPE_CLI)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_TIPODOC - " & strTIPO_DOC)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_NRODOC - " & strNUM_DOC)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_TELEF - " & strTEL_REF)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRC_ESTADOPROC - " & str_PROCESO)

        arrParam(0).Value = strSERIE_CHIP
        arrParam(1).Value = strCOD_PDV
        arrParam(2).Value = strCOD_DEPA
        arrParam(3).Value = strOPCION
        arrParam(4).Value = strCOD_VEN_PEL
        arrParam(5).Value = strChipPack
        arrParam(6).Value = strServicioAdicional5
        arrParam(7).Value = strCOD_PROD
        arrParam(8).Value = strCOD_PLAN
        arrParam(9).Value = strCOD_PROMOCION
        arrParam(10).Value = strTIPO_PROD
        arrParam(11).Value = strCAR5
        arrParam(12).Value = strCAR6
        arrParam(13).Value = strTRACE
        arrParam(14).Value = strOperacionClaro
        arrParam(15).Value = strEstadoActivacion
        arrParam(16).Value = strCodigoRespuesta
        arrParam(17).Value = strDescripcionRespuesta
        arrParam(18).Value = strCAR1
        arrParam(19).Value = strLinea
        arrParam(20).Value = ConfigurationSettings.AppSettings("Flag_Envio")
        arrParam(21).Value = strNOM_CLI
        arrParam(22).Value = strAPE_CLI
        arrParam(23).Value = strTIPO_DOC
        arrParam(24).Value = strNUM_DOC
        arrParam(25).Value = strTEL_REF
        arrParam(26).Value = str_PROCESO

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = esquema & ".PKG_RELANZAMIENTO_PREPAGO.EAISI_ERR_ACTIVAPEL"
        objRequest.Parameters.AddRange(arrParam)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Envio Parametros - " & esquema & ".PKG_RELANZAMIENTO_PREPAGO.EAISI_ERR_ACTIVAPEL")

        Try

            objRequest.Factory.ExecuteNonQuery(objRequest)
            strRespuesta = CType(objRequest.Parameters(28), IDataParameter).Value
            bResultado = True

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Respuesta - " & strRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado - " & bResultado)


        Catch ex As Exception
            bResultado = False

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Respuesta - " & ex.Message.ToString)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado - " & bResultado)
        Finally

        End Try


        Return bResultado

    End Function

    '---------------------------------------------------------------------------------

    Public Function GrabarErrorActivacionNuevoPel(ByVal strMsisdn As String, ByVal strIccid As String, ByVal strImsi As String, ByVal strImei As String, _
                                                 ByVal strTipopreactivacion As String, ByVal strTipoactivacion As String, ByVal strPromocion As String, ByVal strCodigomaterial As String, _
                                                 ByVal strPuntoventa As String, ByVal strDocumentosap As String, ByVal strMensajerespuesta As String, ByVal strCodigoRespuesta As String, _
                                                 ByRef strNOM_CLI As String, ByRef strAPE_CLI As String, ByRef strTIPO_DOC As String, ByRef strNUM_DOC As String, ByRef strTEL_REF As String, _
                                                 ByRef strCOD_PDV_PEL As String, ByRef strCOD_VEN_PEL As String, ByRef strNUM_OPE As String, ByRef strTRACE As String, _
                                                 ByRef str_PROCESO As String, ByRef strRespuesta As String) As Boolean


        'strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim strIdentifyLog As String = "ServicioNuevoPEL - " & strDocumentosap
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Grabar NUEVOPEL - TIMEAI")
        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_EAI"))
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cadena de conexion TIMEAI - " & strCadenaConexion)
        Dim esquema As String = ConfigurationSettings.AppSettings("EsquemaEAI")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Esquema - " & esquema)

        Dim bResultado As Boolean = False

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
              New DAABRequest.Parameter("P_SERRV_MSISDN", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_ICCID", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_IMSI", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_IMEI", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_TIPOPREACTIVACION", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_TIPOACTIVACION", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_PROMOCION", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_CODIGOMATERIAL", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_PUNTOVENTA", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_DOCUMENTOSAP", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_MENSAJERESPUESTA", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_RESPUESTA", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRC_FLAG_ENVIO", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_NOMBRES", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_APELLIDOS", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_TIPODOC", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_NRODOC", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_TELEF", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_COD_VEN_PEL", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_COD_PDV_PEL", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_OPERACIONCLARO", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRV_TRACE_", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("P_SERRC_ESTADOPROC", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("codError", DbType.String, ParameterDirection.Output), _
               New DAABRequest.Parameter("desError", DbType.String, ParameterDirection.Output) _
        }


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_MSISDN - " & strMsisdn)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_ICCID - " & strIccid)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_IMSI - " & strImsi)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_IMEI - " & strImei)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_TIPOPREACTIVACION - " & strTipopreactivacion)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_TIPOACTIVACION - " & strTipoactivacion)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_PROMOCION - " & strPromocion)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_CODIGOMATERIAL - " & strCodigomaterial)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_PUNTOVENTA - " & strPuntoventa)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_DOCUMENTOSAP - " & strDocumentosap)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_MENSAJERESPUESTA - " & strMensajerespuesta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_RESPUESTA - " & strCodigoRespuesta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRC_FLAG_ENVIO - " & ConfigurationSettings.AppSettings("Flag_Envio"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_NOMBRES - " & strNOM_CLI)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_APELLIDOS - " & strAPE_CLI)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_TIPODOC - " & strTIPO_DOC)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_NRODOC - " & strNUM_DOC)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_TELEF - " & strTEL_REF)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_COD_VEN_PEL - " & strCOD_PDV_PEL)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_COD_PDV_PEL - " & strCOD_VEN_PEL)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_OPERACIONCLARO - " & strNUM_OPE)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRV_TRACE_ - " & strTRACE)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "P_SERRC_ESTADOPROC - " & str_PROCESO)

        arrParam(0).Value = strMsisdn
        arrParam(1).Value = strIccid
        arrParam(2).Value = strImsi
        arrParam(3).Value = strImei
        arrParam(4).Value = strTipopreactivacion
        arrParam(5).Value = strTipoactivacion
        arrParam(6).Value = strPromocion
        arrParam(7).Value = strCodigomaterial
        arrParam(8).Value = strPuntoventa
        arrParam(9).Value = strDocumentosap
        arrParam(10).Value = strMensajerespuesta
        arrParam(11).Value = strCodigoRespuesta
        arrParam(12).Value = ConfigurationSettings.AppSettings("Flag_Envio")
        arrParam(13).Value = strNOM_CLI
        arrParam(14).Value = strAPE_CLI
        arrParam(15).Value = strTIPO_DOC
        arrParam(16).Value = strNUM_DOC
        arrParam(17).Value = strTEL_REF
        arrParam(18).Value = strCOD_PDV_PEL
        arrParam(19).Value = strCOD_VEN_PEL
        arrParam(20).Value = strNUM_OPE
        arrParam(21).Value = strTRACE
        arrParam(22).Value = str_PROCESO

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = esquema & ".PKG_RELANZAMIENTO_PREPAGO.EAISI_ERR_ACTIVANUEVOPEL"
        objRequest.Parameters.AddRange(arrParam)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Envio Parametros - " & esquema & ".PKG_RELANZAMIENTO_PREPAGO.EAISI_ERR_ACTIVANUEVOPEL")

        Try

            objRequest.Factory.ExecuteNonQuery(objRequest)
            strRespuesta = CType(objRequest.Parameters(24), IDataParameter).Value
            bResultado = True

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Respuesta - " & strRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado - " & bResultado)

        Catch ex As Exception
            bResultado = False
            strRespuesta = ex.Message.ToString
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Respuesta - " & ex.Message.ToString)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado - " & bResultado)
        Finally

        End Try
        Return bResultado
    End Function

    Public Function ActualizarTelefonoMSINCDB(ByVal iDetalle As clsDetallePedidoMsSap, ByVal rspta As String)
        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_MSSAP"))
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_SINERGIA")
        rspta = String.Empty

        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
              New DAABRequest.Parameter("K_PEDIN_NROPEDIDO", DbType.Int64, ParameterDirection.Input), _
               New DAABRequest.Parameter("K_INTEV_CODINTERLOCUTOR", DbType.Int64, ParameterDirection.Input), _
               New DAABRequest.Parameter("K_SERIC_CODSERIE", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("K_DEPEV_NROTELEFONO", DbType.String, ParameterDirection.Input), _
               New DAABRequest.Parameter("K_NROLOG", DbType.String, ParameterDirection.Output), _
               New DAABRequest.Parameter("K_DESLOG", DbType.String, ParameterDirection.Output) _
               }


        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next


        arrParam(0).Value = iDetalle.K_PEDIN_NROPEDIDO
        arrParam(1).Value = DBNull.Value
        arrParam(2).Value = iDetalle.K_SERIC_CODSERIE
        arrParam(3).Value = iDetalle.K_DEPEV_NROTELEFONO

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = strCadenaEsquema & "PKG_VENTA.SSAPSU_ACTUALIZATELEFONO"
        objRequest.Parameters.AddRange(arrParam)

        Try

            objRequest.Factory.ExecuteNonQuery(objRequest)
            objRequest.Factory.CommitTransaction()
            rspta = CType(objRequest.Parameters(4), IDataParameter).Value
            rspta = rspta & "|" & CType(objRequest.Parameters(5), IDataParameter).Value

        Catch ex As Exception
            objRequest.Factory.RollBackTransaction()
            rspta = "-1" & ex.Message

        Finally
            objRequest.Factory.Dispose()
        End Try


        Return (Funciones.CheckStr(rspta.Split("|")(0)) = "0")

    End Function
    '---------------------------------------------------------------------------------

End Class
