Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB
Imports System.Data.SqlClient

Public Class clsBDSiscajas
    Dim strCadenaConexion As String
    Dim objSeg As New Seguridad_NET.clsSeguridad

    Public Function FP_RegistrarDOL(ByVal MSISDN As String, _
                                    ByVal TipoDocumento As String, _
                                    ByVal NumDocumento As String, _
                                    ByVal Usuario As String, _
                                    ByVal Oficina As String, _
                                    ByVal Ruta As String, _
                                    ByVal Sistema As String, _
                                    ByVal Estado As String, _
                                    ByVal FlagDummy As String, _
                                    ByVal FechaNac As String) As Int64

        Dim intOut As Int64

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PRE_DOL")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                New DAAB.DAABRequest.Parameter("P_MSISDN", DbType.String, 15, MSISDN, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_TIPO_DOCUMENTO", DbType.String, 2, TipoDocumento, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_NUMERO_DOCUMENTO", DbType.String, 12, NumDocumento, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 15, Usuario, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, Oficina, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_RUTA", DbType.String, 200, Ruta, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_SISTEMA", DbType.String, 2, Sistema, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 1, Estado, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_FLAG_DUMMY", DbType.String, 1, FlagDummy, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_FECHA_NACIMIENTO", DbType.String, 8, FechaNac, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_CODIGO_REGISTRO", DbType.Int64, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("K_RESULTADO", DbType.Int32, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_PROCESO_DOL.SP_INSERT_IMAGEN"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)
            intOut = CType(objRequest.Parameters(10), IDataParameter).Value

        Catch ex As Exception
            intOut = 999
        End Try

        FP_RegistrarDOL = intOut

    End Function

    Public Function FP_ConsultaUsuarioRed(ByVal codigoSAP As String, ByRef descRespuesta As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_ECLARDB"))
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = _
        {New DAAB.DAABRequest.Parameter("P_COD_SAP", DbType.String, codigoSAP, ParameterDirection.Input), _
         New DAAB.DAABRequest.Parameter("P_CURSOR", DbType.Object, ParameterDirection.Output)}

        Dim strCadenaEsquema As String

        strCadenaEsquema = ConfigurationSettings.AppSettings("ESQUEMA_ECLARDB")

        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_INTRATIM.SP_GET_USUARIO_RED"
            objRequest.Parameters.AddRange(arrParam)
            FP_ConsultaUsuarioRed = objRequest.Factory.ExecuteDataset(objRequest)

            descRespuesta = "OK"

        Catch ex As Exception
            descRespuesta = ex.Message.ToString()
            FP_ConsultaUsuarioRed = Nothing
        End Try


    End Function

    Public Function FP_RegistrarBonoDOL(ByVal IMEI As String, _
                                        ByVal MSISDN As String, _
                                        ByVal Origen As String, _
                                        ByVal Usuario As String, _
                                        ByVal Region As String, _
                                        ByVal Promocion As String, _
                                        ByRef mensaje As String, _
                                        ByRef p_offer_out As String, _
                                        ByRef p_indneg_out As String) As Integer

        Dim intOut As Integer

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PROM_DOL")
            Dim EsquemaPROMDB As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("EsquemaPROMDB"))

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                New DAAB.DAABRequest.Parameter("P_IMEI", DbType.String, 20, IMEI, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_MSISDN", DbType.String, 20, MSISDN, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_ORIGEN", DbType.String, 10, Origen, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_COD_DPTO", DbType.String, 10, Region, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_PROMOCION", DbType.String, 15, Promocion, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 15, Usuario, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("p_offer_out", DbType.String, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("p_indneg_out", DbType.String, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("VOUT_MSJ", DbType.String, ParameterDirection.Output), _
                New DAAB.DAABRequest.Parameter("VOUT_COD", DbType.Int32, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            If EsquemaPROMDB <> "" Then
                objRequest.Command = EsquemaPROMDB & ".IDE002_PKG_CONTROL_PROMOCIONES.SP_INSERTA_CALL_DOLL"
            Else
                objRequest.Command = "IDE002_PKG_CONTROL_PROMOCIONES.SP_INSERTA_CALL_DOLL"
            End If

            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)
            p_offer_out = IIf(IsDBNull(CType(objRequest.Parameters(6), IDataParameter).Value), "", CType(objRequest.Parameters(6), IDataParameter).Value)
            p_indneg_out = IIf(IsDBNull(CType(objRequest.Parameters(7), IDataParameter).Value), "", CType(objRequest.Parameters(7), IDataParameter).Value)
            mensaje = IIf(IsDBNull(CType(objRequest.Parameters(8), IDataParameter).Value), "", CType(objRequest.Parameters(8), IDataParameter).Value)
            intOut = IIf(IsDBNull(CType(objRequest.Parameters(8), IDataParameter).Value), "", CType(objRequest.Parameters(9), IDataParameter).Value)
        Catch ex As Exception
            intOut = 999
            mensaje = ex.Message
        End Try

        FP_RegistrarBonoDOL = intOut

    End Function

    Public Function FP_ConsultaMaterialBBA(ByVal strGrupo As String) As DataSet

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("SPARV_GRUPO", DbType.String, 10, strGrupo, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISCAJ.SICAR_CONSULTA_PARAM"
            objRequest.Parameters.AddRange(arrParam)

            FP_ConsultaMaterialBBA = objRequest.Factory.ExecuteDataset(objRequest)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function FP_Desactiva_Contact_Status(ByVal strTelefono As String, ByRef strMensaje As String, ByRef strCodRespuesta As String) As Boolean

        Dim blnOK As Boolean
        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_CLARIFY")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("P_TELEFONO", DbType.String, 20, strTelefono, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_MENSAJE", DbType.String, 1000, ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("P_RETORNO", DbType.String, 5, ParameterDirection.Output)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PCK_CONSULTA_CLFY.SP_UPD_CONTACT_ESTADO_BY_TLF"
            objRequest.Parameters.AddRange(arrParam)

            objRequest.Factory.ExecuteNonQuery(objRequest)
            strMensaje = CType(objRequest.Parameters(1), IDataParameter).Value
            strCodRespuesta = CType(objRequest.Parameters(2), IDataParameter).Value

            If strCodRespuesta.Equals("0") Then
                blnOK = True
            End If
        Catch ex As Exception
            blnOK = False
            strMensaje = ex.Message.ToString()
            strCodRespuesta = "1"
        End Try

        Return blnOK
    End Function

    Public Sub ObtenerDatosVentaSGA(ByVal nroSec As String, ByRef oVenta As SGAVenta, ByRef oListaServicio As ArrayList, ByRef oListaPromocion As ArrayList)
        Try

            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

            Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("p_solin_codigo", DbType.String, 20, nroSec, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("cur_regventa", DbType.Object, ParameterDirection.Output), _
                                        New DAAB.DAABRequest.Parameter("cur_servicio", DbType.Object, ParameterDirection.Output), _
                                        New DAAB.DAABRequest.Parameter("cur_promocion", DbType.Object, ParameterDirection.Output)}

            Dim tablas() As String = {"regVenta", "servicio", "promocion"}
            objRequest.TableNames = tablas
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_DTH.SP_CON_DATOS_VENTA"
            objRequest.Parameters.AddRange(arrParam)

            Dim ds As DataSet
            ds = objRequest.Factory.ExecuteDataset(objRequest)

            Dim dt As New DataTable
            dt = ds.Tables("regVenta")

            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    oVenta.idnegocio = Funciones.CheckStr(dr("IDNEGOCIO_SGA"))
                    '//oVenta.codcliente            = Funciones.CheckStr(dr("SOLIN_CODIGO"))
                    oVenta.tipoDocCliente = Funciones.CheckStr(dr("TDOCC_CODIGO_SGA"))
                    oVenta.nroDocCliente = Funciones.CheckStr(dr("CLIEC_NUM_DOC"))
                    oVenta.apePaterno = Funciones.CheckStr(dr("CLIEV_APE_PAT"))
                    oVenta.apeMaterno = Funciones.CheckStr(dr("CLIEV_APE_MAT"))
                    oVenta.nombreCliente = Funciones.CheckStr(dr("NOMBRE_RAZON_SOCIAL")) & " " & Funciones.CheckStr(dr("CLIEV_APE_PAT")) & " " & Funciones.CheckStr(dr("CLIEV_APE_MAT"))
                    oVenta.nombreComercial = Funciones.CheckStr(dr("NOMBRE_RAZON_SOCIAL"))
                    oVenta.codTelef1 = Funciones.CheckStr(dr("CODTELEF1"))
                    oVenta.nroTelefono1 = Funciones.CheckStr(dr("TELEFONO1"))
                    oVenta.codTelef2 = Funciones.CheckStr(dr("CODTELEF2"))
                    oVenta.nroTelefono2 = Funciones.CheckStr(dr("TELEFONO2"))
                    '//oVenta.codTelefMovil1        = Funciones.CheckStr(dr("CLIEV_PER_REF_1"))
                    oVenta.nroTelefMovil1 = Funciones.CheckStr(dr("TELEFONO_REF_1"))
                    '//oVenta.codTelefMovil2        = Funciones.CheckStr(dr("CLIEV_PER_REF2"))
                    oVenta.nroTelefMovil2 = Funciones.CheckStr(dr("TELEFONO_REF_2"))
                    oVenta.correo = Funciones.CheckStr(dr("CORREO_RECIBO"))
                    '//oVenta.codSucursalInst       = Funciones.CheckStr(dr("SOLIN_CODIGO"))
                    oVenta.tipoViaInst = Funciones.CheckStr(dr("TIPO_VIA_INST"))
                    oVenta.nombreViaInst = Funciones.CheckStr(dr("NOMBRE_VIA_INST"))
                    oVenta.nroViaInst = Funciones.CheckStr(dr("NRO_PUERTA_INST"))
                    oVenta.tipoUrbInst = Funciones.CheckStr(dr("TIPO_URB_INST"))
                    oVenta.nombreUrbInst = Funciones.CheckStr(dr("NOMBRE_URB_INST"))
                    oVenta.manzazaInst = Funciones.CheckStr(dr("MANZANA_INST"))
                    oVenta.loteInst = Funciones.CheckStr(dr("LOTE_INST"))
                    oVenta.ubigeoInst = Funciones.CheckStr(dr("UBIGEO_INST"))
                    oVenta.referenciaInst = Funciones.CheckStr(dr("REFERENCIA_INST"))
                    '//oVenta.codSucursalFact       = Funciones.CheckStr(dr("CLIEV_PRE_DIR_FAC_SGA"))
                    oVenta.tipoViaFact = Funciones.CheckStr(dr("TIPO_VIA_FACT"))
                    oVenta.nombreViaFact = Funciones.CheckStr(dr("NOMBRE_VIA_FACT"))
                    oVenta.nroViaFact = Funciones.CheckStr(dr("NRO_PUERTA_FACT"))
                    oVenta.tipoUrbFact = Funciones.CheckStr(dr("TIPO_URB_FACT"))
                    oVenta.nombreUrbFact = Funciones.CheckStr(dr("NOMBRE_URB_FACT"))
                    oVenta.manzazaFact = Funciones.CheckStr(dr("MANZANA_FACT"))
                    oVenta.loteFact = Funciones.CheckStr(dr("LOTE_FACT"))
                    oVenta.ubigeoFact = Funciones.CheckStr(dr("UBIGEO_FACT"))
                    oVenta.referenciaFact = Funciones.CheckStr(dr("REFERENCIA_FACT"))
                    '//oVenta.codContacto           = Funciones.CheckStr(dr("SOLIN_CODIGO"))
                    '//oVenta.apePaternoCont        = Funciones.CheckStr(dr("SOLIN_CODIGO"))
                    '//oVenta.apeMaternoCont        = Funciones.CheckStr(dr("SOLIN_CODIGO"))
                    '//oVenta.nombreContacto        = Funciones.CheckStr(dr("SOLIN_CODIGO"))
                    '//oVenta.usuarioRegistro       = Funciones.CheckStr(dr("SOLIN_CODIGO"))
                    oVenta.fechaRegistro = Funciones.CheckStr(dr("FECHA"))
                    oVenta.codCanalVenta = Funciones.CheckStr(dr("CANAL_VENTA_SGA"))
                    oVenta.codSupervisor = Funciones.CheckStr(dr("SUPERVISOR_SGA"))
                    oVenta.tipoSupervisor = Funciones.CheckStr(dr("TIPO_VENDEDOR_SGA"))
                    oVenta.tipoDocVendedor = Funciones.CheckStr(dr("TIPO_DOC_SGA"))
                    oVenta.nroDocVendedor = Funciones.CheckStr(dr("DNI_VENDEDOR_SGA"))
                    oVenta.nombreCompletoVendedor = Funciones.CheckStr(dr("NOMB_VENDEDOR_SGA"))
                    oVenta.idPaq = Funciones.CheckStr(dr("PAQUETE_SGA"))
                    oVenta.nroContrato = Funciones.CheckStr(dr("NRO_CONTRATO"))
                    oVenta.fechaInstalacion = Funciones.CheckStr(dr("FECHA_INST"))
                    oVenta.flagRecarga = Funciones.CheckStr(dr("FLAG_REC_VIRT_SGA"))
                    oVenta.tipoPago = Funciones.CheckStr(dr("TIPO_PAGO_SGA"))
                    oVenta.datoPago = Funciones.CheckStr(dr("DATO_PAGO"))             ' //desc plazo de acuerdo
                    oVenta.datoRecarga = Funciones.CheckStr(dr("DATO_RECARGA"))          ' // costo de instalacion 
                    oVenta.correoAfiliacion = Funciones.CheckStr(dr("cliev_correo_iclaro"))     ' //?
                    oVenta.nroCartaPreSeleccion = Funciones.CheckStr(dr("CARTA"))
                    oVenta.codOperadorLD = Funciones.CheckStr(dr("OPERADORLD"))
                    oVenta.flag_Presuscrito = Funciones.CheckStr(dr("cliev_presuscrito"))
                    oVenta.flag_Publicar = Funciones.CheckStr(dr("PUBLICAR"))
                    oVenta.idPlano = Funciones.CheckStr(dr("PLANO"))
                Next
            End If

            dt = ds.Tables("servicio")
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim oServicio As New SGAServicio
                    oServicio.numsec = Funciones.CheckStr(dr("SOLIN_CODIGO"))
                    oServicio.iddet = Funciones.CheckStr(dr("IDDET"))
                    oServicio.idlinea = Funciones.CheckStr(dr("IDLINEA"))
                    oServicio.coequipo = Funciones.CheckStr(dr("CODEQUIPO"))
                    oServicio.cantidad = Funciones.CheckStr(dr("CANTIDAD"))
                    oListaServicio.Add(oServicio)
                Next
            End If

            'dt = ds.Tables("promocion")
            'If dt.Rows.Count > 0 Then
            '    For Each dr As DataRow In dt.Rows
            '        Dim oPromocion As New SGAPromocion
            '        oPromocion.numsec = Funciones.CheckStr(dr("SOLIN_CODIGO"))
            '        oPromocion.iddet = Funciones.CheckStr(dr("IDDET"))
            '        oPromocion.idprom = Funciones.CheckStr(dr("IDPROM"))
            '        oListaPromocion.Add(oPromocion)
            '    Next
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Function ListaTipoDocumento(ByVal flag_ruc As String) As ArrayList

        'strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                        New DAAB.DAABRequest.Parameter("P_FLAG_CON", DbType.String, ParameterDirection.Input), _
                                                    New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        Dim i As Integer
        For i = 0 To arrParam.Length - 1
            arrParam(i).Value = DBNull.Value
        Next
        arrParam(0).Value = flag_ruc
        Dim dr As IDataReader = Nothing
        Dim arrListaDoc As New ArrayList
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SISACT_PKG_CONSULTA_GENERAL.SISACT_CON_TIPO_DOC"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            dr = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader()

            While (dr.Read())
                Dim item As New ItemGenerico
                item.CODIGO = dr("TDOCC_CODIGO")
                item.CODIGO2 = dr("ID_CCLUB")
                item.DESCRIPCION = dr("TDOCV_DESCRIPCION")
                arrListaDoc.Add(item)
            End While

        Catch ex As Exception
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return arrListaDoc
    End Function

    Public Function ConsultaAcuerdosXDocSap(ByVal p_nrodoc_sap As String, ByRef p_cod_resp As String, ByRef p_msg_resp As String) As DataSet
        Dim ds As New DataSet
        p_cod_resp = ""

        'strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                    New DAAB.DAABRequest.Parameter("p_nrodoc_sap", DbType.String, 20, p_nrodoc_sap, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_cod_resp", DbType.String, ParameterDirection.Output), _
                                    New DAAB.DAABRequest.Parameter("p_msg_resp", DbType.String, ParameterDirection.Output), _
                                    New DAAB.DAABRequest.Parameter("cur_datos_sap", DbType.Object, ParameterDirection.Output), _
                                    New DAAB.DAABRequest.Parameter("cur_acuerdo_det", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "sisact_pkg_acuerdo_6.sp_con_acuerdos_x_docsap"
        objRequest.Parameters.AddRange(arrParam)
        Try

            ds = objRequest.Factory.ExecuteDataset(objRequest)
            p_cod_resp = IIf(IsDBNull(CType(objRequest.Parameters(1), IDataParameter).Value), "", CType(objRequest.Parameters(1), IDataParameter).Value)
            p_msg_resp = IIf(IsDBNull(CType(objRequest.Parameters(2), IDataParameter).Value), "", CType(objRequest.Parameters(2), IDataParameter).Value)
        Catch ex As Exception
            Throw ex
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return ds
    End Function

    Public Function AsignarPagoAcuerdosXDocSap(ByVal p_nro_documento As String, ByVal p_nro_referencia As String, ByVal p_monto_pago As Double, ByVal p_usuario As String, ByRef p_msg_resp As String) As String
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

    Public Function ActualizaEstadoContrato(ByVal p_nro_contrato As String, _
                                            ByVal p_codigo_bscs As String, _
                                            ByVal p_customer_id As String, _
                                            ByVal p_estado As String, _
                                            ByVal p_usuario As String, _
                                            ByVal p_usuario_des As String, _
                                            ByVal p_observacion As String, _
                                            ByVal p_contrato_bscs As String, _
                                            ByRef p_msg_resp As String) As String
        Dim p_cod_resp As String = ""
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                    New DAAB.DAABRequest.Parameter("p_nro_contrato", DbType.String, 20, p_nro_contrato, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_codigo_bscs", DbType.String, 50, p_codigo_bscs, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_customer_id", DbType.String, 50, p_customer_id, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_estado", DbType.String, 50, p_estado, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_usuario", DbType.String, 50, p_usuario, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_usuario_des", DbType.String, 50, p_usuario_des, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_observacion", DbType.String, 50, p_observacion, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_contrato_bscs", DbType.String, 50, p_contrato_bscs, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_retorno", DbType.String, ParameterDirection.Output), _
                                    New DAAB.DAABRequest.Parameter("p_mensaje", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SISACT_PKG_CONTRATO_.sisact_actualiza_contrato"
        objRequest.Parameters.AddRange(arrParam)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            p_cod_resp = IIf(IsDBNull(CType(objRequest.Parameters(8), IDataParameter).Value), "", CType(objRequest.Parameters(8), IDataParameter).Value)
            p_msg_resp = IIf(IsDBNull(CType(objRequest.Parameters(9), IDataParameter).Value), "", CType(objRequest.Parameters(9), IDataParameter).Value)
        Catch ex As Exception
            Throw ex
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return p_cod_resp
    End Function

    Public Function CambiarEstadoAcuerdo(ByVal p_numero_acuerdo As String, _
                                        ByVal p_estado As String, _
                                        ByVal p_usuario As String, _
                                        ByRef p_msg_resp As String) As String
        Dim p_cod_resp As String = ""

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                    New DAAB.DAABRequest.Parameter("p_numero_acuerdo", DbType.String, 20, p_numero_acuerdo, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_estado", DbType.String, 3, p_estado, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_usuario", DbType.String, 20, p_usuario, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_cod_resp", DbType.String, ParameterDirection.Output), _
                                    New DAAB.DAABRequest.Parameter("p_msg_resp", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "sisact_pkg_acuerdo_6.sp_upd_estado_acuerdo"
        objRequest.Parameters.AddRange(arrParam)

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            p_cod_resp = IIf(IsDBNull(CType(objRequest.Parameters(3), IDataParameter).Value), "", CType(objRequest.Parameters(3), IDataParameter).Value)
            p_msg_resp = IIf(IsDBNull(CType(objRequest.Parameters(4), IDataParameter).Value), "", CType(objRequest.Parameters(4), IDataParameter).Value)
        Catch ex As Exception
            Throw ex
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return p_cod_resp
    End Function

    Public Function AnularVentaSisact(ByVal p_nro_documento As String, ByVal p_usuario As String, ByRef p_id_venta As Int64, ByRef p_id_contrato As Int64, ByRef p_msg_resp As String) As String
        Dim p_cod_resp As String = ""
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                    New DAAB.DAABRequest.Parameter("p_nro_documento", DbType.String, 50, p_nro_documento, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_usuario", DbType.String, 50, p_usuario, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_cod_resp", DbType.String, ParameterDirection.Output), _
                                    New DAAB.DAABRequest.Parameter("p_msg_resp", DbType.String, ParameterDirection.Output), _
                                    New DAAB.DAABRequest.Parameter("p_id_venta", DbType.Int64, ParameterDirection.Output), _
                                    New DAAB.DAABRequest.Parameter("p_id_contrato", DbType.Int64, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "sisact_pkg_acuerdo_6.sp_anular_venta_x_docsap"
        objRequest.Parameters.AddRange(arrParam)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            p_cod_resp = IIf(IsDBNull(CType(objRequest.Parameters(2), IDataParameter).Value), "", CType(objRequest.Parameters(2), IDataParameter).Value)
            p_msg_resp = IIf(IsDBNull(CType(objRequest.Parameters(3), IDataParameter).Value), "", CType(objRequest.Parameters(3), IDataParameter).Value)

            If p_cod_resp = "0" Then
                p_id_venta = Funciones.CheckInt64(IIf(IsDBNull(CType(objRequest.Parameters(4), IDataParameter).Value), "", CType(objRequest.Parameters(4), IDataParameter).Value))
                p_id_contrato = Funciones.CheckInt64(IIf(IsDBNull(CType(objRequest.Parameters(5), IDataParameter).Value), "", CType(objRequest.Parameters(5), IDataParameter).Value))
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return p_cod_resp
    End Function

    Public Function AnularCanjeEquipo(ByVal p_Id_Venta As Int64, ByRef p_ost As String, ByRef p_msg_resp As String) As String
        Dim p_cod_resp As String = ""
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                    New DAAB.DAABRequest.Parameter("p_id_venta", DbType.Int64, p_Id_Venta, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("p_cod_resp", DbType.String, ParameterDirection.Output), _
                                    New DAAB.DAABRequest.Parameter("p_msg_resp", DbType.String, ParameterDirection.Output), _
                                    New DAAB.DAABRequest.Parameter("p_ost", DbType.String, 100, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "sisact_pkg_acuerdo_6.sp_anular_canje_equipo"
        objRequest.Parameters.AddRange(arrParam)
        Try

            objRequest.Factory.ExecuteNonQuery(objRequest)

            p_cod_resp = IIf(IsDBNull(CType(objRequest.Parameters(1), IDataParameter).Value), "", CType(objRequest.Parameters(1), IDataParameter).Value)
            p_msg_resp = IIf(IsDBNull(CType(objRequest.Parameters(2), IDataParameter).Value), "", CType(objRequest.Parameters(2), IDataParameter).Value)
            p_ost = IIf(IsDBNull(CType(objRequest.Parameters(3), IDataParameter).Value), "", CType(objRequest.Parameters(3), IDataParameter).Value)
        Catch ex As Exception
            Throw ex
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return p_cod_resp
    End Function

    Public Function AnularVentaPVUDB(ByVal p_nro_documento As String, ByVal p_usuario As String, ByRef p_id_venta As Int64, ByRef p_id_contrato As Int64, ByRef p_msg_resp As String) As String
        Dim p_cod_resp As String = ""
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                    New DAAB.DAABRequest.Parameter("P_IN_DOCUMENTO_SAP", DbType.String, 50, p_nro_documento, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("P_IN_CODIGO_USUARIO", DbType.String, 50, p_usuario, ParameterDirection.Input), _
                                    New DAAB.DAABRequest.Parameter("P_OUT_ID_VENTA", DbType.Int64, ParameterDirection.Output), _
                                    New DAAB.DAABRequest.Parameter("P_OUT_ID_CONTRATO", DbType.Int64, ParameterDirection.Output), _
                                    New DAAB.DAABRequest.Parameter("P_OUT_RESPUESTA_CODIGO", DbType.String, ParameterDirection.Output), _
                                    New DAAB.DAABRequest.Parameter("P_OUT_RESPUESTA_MENSAJE", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "sisact_pkg_acuerdo_6.SISACTSU_ANULAR_VENTA_X_DOCSAP"
        objRequest.Parameters.AddRange(arrParam)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            p_cod_resp = Funciones.CheckStr(CType(objRequest.Parameters(4), IDataParameter).Value) 'IIf(IsDBNull(CType(objRequest.Parameters(4), IDataParameter).Value), "", CType(objRequest.Parameters(4), IDataParameter).Value)
            p_msg_resp = Funciones.CheckStr(CType(objRequest.Parameters(5), IDataParameter).Value) 'IIf(IsDBNull(CType(objRequest.Parameters(5), IDataParameter).Value), "", CType(objRequest.Parameters(5), IDataParameter).Value)

            If p_cod_resp = "0" Then
                p_id_venta = Funciones.CheckInt64(CType(objRequest.Parameters(2), IDataParameter).Value) ' Funciones.CheckInt64(IIf(IsDBNull(CType(objRequest.Parameters(2), IDataParameter).Value), "", CType(objRequest.Parameters(2), IDataParameter).Value))
                p_id_contrato = Funciones.CheckInt64(CType(objRequest.Parameters(3), IDataParameter).Value) 'Funciones.CheckInt64(IIf(IsDBNull(CType(objRequest.Parameters(3), IDataParameter).Value), "", CType(objRequest.Parameters(3), IDataParameter).Value))
            End If
        Catch ex As Exception
            p_cod_resp = "-99"
            p_msg_resp = ex.Message.ToString()
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try
        Return p_cod_resp
    End Function
End Class
