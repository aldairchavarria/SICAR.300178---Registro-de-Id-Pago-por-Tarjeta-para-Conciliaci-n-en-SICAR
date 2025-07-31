#Region "Imports"
Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB
#End Region

Public Class clsAdmCajas

#Region "Variables"
    Dim strCadenaConexion As String
    Dim objSeg As New Seguridad_NET.clsSeguridad
    Dim pkgNameOffSAP As String = "PCK_SICAR_OFF_SAP"
#End Region

#Region "Propiedades"

#End Region

#Region "Metodos"

#Region "CAJA - OFICINA"

    Public Function GetCajaOficina(ByVal oficina As String, ByVal estado As String, ByVal IdCajaOficina As Integer) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_DESC", DbType.String, 2000, oficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 20, estado, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_IDCAJ_OF", DbType.Int32, IdCajaOficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetCajaOficina"
        objRequest.Parameters.AddRange(arrParam)
        GetCajaOficina = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function SetCajaOficina(ByVal oficinaVenta As String, ByVal nombreCaja As String, _
                                   ByVal nroCaja As String, ByVal estado As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficinaVenta, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_NOMBRECAJA", DbType.String, 30, nombreCaja, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_NROCAJA", DbType.String, 2, nroCaja, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 20, estado, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MsgErr", DbType.String, 200, String.Empty, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_RegistrarCajaOficina"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(4), IDataParameter).Value.ToString
    End Function

    Public Function ActualizarCajaOficina(ByVal CajaOficinaID As Int32, ByVal oficinaVenta As String, _
                                ByVal nombreCaja As String, ByVal nroCaja As String, ByVal estado As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_ID_CAJA", DbType.Int32, CajaOficinaID, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficinaVenta, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_NOMBRECAJA", DbType.String, 30, nombreCaja, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_NROCAJA", DbType.String, 2, nroCaja, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 1, estado, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MsgErr", DbType.String, 200, String.Empty, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_ActualizarCajaOficina"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(5), IDataParameter).Value.ToString
    End Function

    Public Function EliminarCajaOficina(ByVal CajaOficinaID As Int32) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_ID_CAJA", DbType.Int32, CajaOficinaID, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 1000, String.Empty, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_EliminarCajaOficina"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(1), IDataParameter).Value.ToString
    End Function

    Public Function GetOficinas(ByVal strDescripcion As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_DESC", DbType.String, 20, strDescripcion, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetParamVenta"
        objRequest.Parameters.AddRange(arrParam)
        GetOficinas = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function GetCajas(ByVal strDescripcion As String, ByVal strOficina As String, ByVal strEstado As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_DESC", DbType.String, 20, strDescripcion, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 2000, strOficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 20, strEstado, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetCaja"
        objRequest.Parameters.AddRange(arrParam)
        GetCajas = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

#End Region

#Region "Lista - Cuadre Ind y Gen"

    Public Function GetVendedores(ByVal usuario As String, ByVal oficina As String, ByVal rol As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 50, usuario, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 2000, oficina, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ROL", DbType.String, 5, rol, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetVendedor"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function

    Public Function GetMontoCuadreConsultaTotal(ByVal oficinaVta As String, ByVal fechaIni As String, _
                                                ByVal fechaFin As String, ByVal cajero As String, _
                                                ByVal caja As String, ByVal estado As String, _
                                                ByVal descripcion As String, ByVal cntRegistros As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 2000, oficinaVta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHAINI", DbType.String, 8, fechaIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHAFIN", DbType.String, 8, fechaFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 500, cajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CAJA", DbType.String, 2, caja, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 1, estado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DESC", DbType.String, 50, descripcion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CNTREG", DbType.String, 6, cntRegistros, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetMontosCuadreTot"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function


    Public Function GetCajaIndividual(ByVal oficinaVta As String, ByVal fechaIni As String, _
                                            ByVal fechaFin As String, ByVal cajero As String, _
                                            ByVal caja As String, ByVal estado As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 1000, oficinaVta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHAINI", DbType.String, 10, fechaIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHAFIN", DbType.String, 10, fechaFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CAJA", DbType.String, 2, caja, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 1, estado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetCajaIndividual"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function

    Public Function RetirarAsignacionCajaDiario(ByVal CajaDiarioID As Long) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_ID_CAJA", DbType.Int64, CajaDiarioID, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 200, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_RetirarAsignacionCaja"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(1), IDataParameter).Value.ToString
    End Function

    Public Function LiberarCuadreCajaDiario(ByVal CajaDiarioID As Long) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_ID_CAJA", DbType.Int64, CajaDiarioID, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 200, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_LiberarCuadreCaja"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(1), IDataParameter).Value.ToString
    End Function

    Public Function GetCajaGeneral(ByVal oficinaVta As String, ByVal fechaIni As String, _
                                            ByVal fechaFin As String, ByVal cajero As String, _
                                            ByVal estado As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 2000, oficinaVta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHAINI", DbType.String, 8, fechaIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHAFIN", DbType.String, 8, fechaFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 1, estado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetCajaGeneral"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function

    Public Function LiberarCuadreCajaGeneral(ByVal CajaDiarioID As Long) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim strUserIDBD As String = Me.GetUserIDByProvider(strCadenaConexion, "1")
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_ID_CAJA", DbType.Int64, CajaDiarioID, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_USER", DbType.String, strUserIDBD, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 200, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_LiberarCuadreGeneral"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(2), IDataParameter).Value.ToString
    End Function

#End Region

#Region "Contabilizacion Manual"

    Public Function GetTransaccionesRecaudacion(ByVal oficina As String, ByVal nroTransacIni As String, ByVal nroTransacFin As String, _
                                            ByVal fechaIni As String, ByVal fechaFin As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NROTRS_INI", DbType.String, 15, nroTransacIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROTRS_FIN", DbType.String, 15, nroTransacFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_INI", DbType.String, 10, fechaIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_FIN", DbType.String, 10, fechaFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 1000, oficina, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS_TIPO1", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS_TIPO2", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS_DET", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_GetContabilizaRec"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function

    Public Function ActualizarTransacPagos(ByVal trsPagoID As Int32, ByVal viaPago As String, _
                                ByVal docContable As String, ByVal descErr As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim strUserIDBD As String = Me.GetUserIDByProvider(strCadenaConexion, "1")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_ID_TRS_PAGO", DbType.Int32, trsPagoID, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_VIA_PAGO", DbType.String, 4, viaPago, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_DOC_CONTABLE", DbType.String, 10, docContable, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 12, strUserIDBD.ToUpper(), ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_DESC_ERR", DbType.String, 50, descErr, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 2000, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_ActualizarTrsPagos"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(5), IDataParameter).Value.ToString
    End Function

    Public Function GetRecauTrsPagos(ByVal nroTransacIni As String, ByVal nroTransacFin As String, _
                                        ByVal oficinaVenta As String, ByVal fechaIni As String, _
                                        ByVal fechaFin As String, ByVal montoPagado1 As String, _
                                        ByVal montoPagado2 As String, ByVal mtoTotPag1 As String, _
                                        ByVal mtoTotPag2 As String, ByVal estado As String, _
                                        ByVal nroTelefonoIni As String, ByVal nroTelefonoFin As String, _
                                        ByVal cajero As String, ByVal viaPago As String, _
                                        ByVal nroChequeIni As String, ByVal nroChequeFin As String, _
                                        ByVal docContableIni As String, ByVal docContableFin As String, _
                                        ByVal rucIni As String, ByVal rucFin As String, ByVal servicio As String, _
                                        ByVal cntRegistros As String, _
                                        ByVal SuboficinaVenta As String, _
                                        ByRef StrRespuesta As String) As DataSet

        Try

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NROTRS_1", DbType.String, 15, nroTransacIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROTRS_2", DbType.String, 15, nroTransacFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficinaVenta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_SUB_OFICINA", DbType.String, 1000, SuboficinaVenta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_INI", DbType.String, 10, fechaIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_FIN", DbType.String, 10, fechaFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MTOPAG_1", DbType.String, 12, montoPagado1, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MTOPAG_2", DbType.String, 12, montoPagado2, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MTOTOTPAG_1", DbType.String, 12, mtoTotPag1, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MTOTOTPAG_2", DbType.String, 12, mtoTotPag2, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 2, estado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROTEL_1", DbType.String, 15, nroTelefonoIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROTEL_2", DbType.String, 15, nroTelefonoFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_VIAPAGO", DbType.String, 4, viaPago, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROCHQ_1", DbType.String, 20, nroChequeIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROCHQ_2", DbType.String, 20, nroChequeFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DOCCONT_1", DbType.String, 10, docContableIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DOCCONT_2", DbType.String, 10, docContableFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_RUCDEU_1", DbType.String, 15, rucIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_RUCDEU_2", DbType.String, 15, rucFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_SERVICIO", DbType.String, 3, servicio, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CNTREG", DbType.String, 6, cntRegistros, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_GetRecauTrsPagos"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)

        StrRespuesta = "OK"

        Return ds

        Catch ex As Exception

            StrRespuesta = ex.Message.ToString()
            Return Nothing
        End Try

    End Function

    Public Function ActualizarDataDeuda(ByVal IdTransacDeuda As Int32, ByVal estado As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_ID_TRS_REG", DbType.Int32, IdTransacDeuda, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 1, estado, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MsgErr", DbType.String, 500, ParameterDirection.Output)}
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_ActualizarDataDeuda"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(2), IDataParameter).Value.ToString
    End Function

    Public Function GetRecauDeuda(ByVal nroTransacIni As String, ByVal nroTransacFin As String, _
                                        ByVal oficinaVenta As String, ByVal SuboficinaVenta As String, ByVal fechaIni As String, _
                                        ByVal fechaFin As String, ByVal moneda As String, _
                                        ByVal mtoTotPag1 As String, ByVal mtoTotPag2 As String, _
                                        ByVal estado As String, _
                                        ByVal nroTelefonoIni As String, ByVal nroTelefonoFin As String, _
                                        ByVal cajero As String, ByVal tipoDocumento As String, _
                                        ByVal nroDocumentoIni As String, ByVal nroDocumentoFin As String, _
                                        ByVal rucIni As String, ByVal rucFin As String, ByVal cntRegistros As String, ByRef strRespuesta As String) As DataSet

        Try
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NROTRS_1", DbType.String, 15, nroTransacIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROTRS_2", DbType.String, 15, nroTransacFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficinaVenta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_SUB_OFICINA", DbType.String, 1000, SuboficinaVenta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_INI", DbType.String, 10, fechaIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_FIN", DbType.String, 10, fechaFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MONEDA", DbType.String, 10, moneda, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MTOTOTPAG_1", DbType.String, 12, mtoTotPag1, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MTOTOTPAG_2", DbType.String, 12, mtoTotPag2, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, 2, estado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROTEL_1", DbType.String, 15, nroTelefonoIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROTEL_2", DbType.String, 15, nroTelefonoFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CAJERO", DbType.String, 10, cajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_TIPDOC", DbType.String, 2, tipoDocumento, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NRODOC_1", DbType.String, 21, nroDocumentoIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NRODOC_2", DbType.String, 21, nroDocumentoFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_RUCDEU_1", DbType.String, 15, rucIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_RUCDEU_2", DbType.String, 15, rucFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CNTREG", DbType.String, 6, cntRegistros, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_GetRecauDeuda"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        strRespuesta = "OK"

        Return ds
        Catch ex As Exception
            strRespuesta = ex.Message.ToString()
            Return Nothing
        End Try

    End Function

    Public Function GetRecauPagos(ByVal nroTransacIni As String, ByVal nroTransacFin As String, _
                                    ByVal viaPago As String, _
                                    ByVal mtoTotPag1 As String, ByVal mtoTotPag2 As String, _
                                    ByVal nroChequeIni As String, ByVal nroChequeFin As String, _
                                    ByVal docContableIni As String, ByVal docContableFin As String, ByVal cntRegistros As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NROTRS_1", DbType.String, 15, nroTransacIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROTRS_2", DbType.String, 15, nroTransacFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_VIAPAGO", DbType.String, 4, viaPago, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MTOTOTPAG_1", DbType.String, 12, mtoTotPag1, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MTOTOTPAG_2", DbType.String, 12, mtoTotPag2, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROCHQ_1", DbType.String, 20, nroChequeIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROCHQ_2", DbType.String, 20, nroChequeIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DOCCONT_1", DbType.String, 10, docContableIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DOCCONT_2", DbType.String, 10, docContableFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CNTREG", DbType.String, 6, cntRegistros, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_GetRecauPagos"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function


    Public Function GetRecauRecibos(ByVal nroTransacIni As String, ByVal nroTransacFin As String, _
                                        ByVal tipoDocRec As String, ByVal nroDocRecIni As String, _
                                        ByVal nroDocRecFin As String, ByVal moneda As String, _
                                        ByVal importeRecibidoIni As String, ByVal importeRecibidoFin As String, _
                                        ByVal importePagadoIni As String, ByVal importePagadoFin As String, _
                                        ByVal nroCobranzaIni As String, ByVal nroCobranzaFin As String, _
                                        ByVal nroAcreedorIni As String, ByVal nroAcreedorFin As String, _
                                        ByVal fechaIni As String, ByVal fehcaFin As String, _
                                        ByVal nroDocDeudorIni As String, ByVal nroDocDeudorFin As String, _
                                        ByVal nroTracePagoIni As String, ByVal nroTracePagoFin As String, ByVal cntRegistro As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NROTRS_1", DbType.String, 15, nroTransacIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROTRS_2", DbType.String, 15, nroTransacFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_TIPDOC_REC", DbType.String, 3, tipoDocRec, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NRODOC_REC1", DbType.String, 16, nroDocRecIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NRODOC_REC2", DbType.String, 16, nroDocRecFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_MONEDA", DbType.String, 4, moneda, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_IMPREC_1", DbType.String, 12, importeRecibidoIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_IMPREC_2", DbType.String, 12, importeRecibidoFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_IMPPAG_1", DbType.String, 12, importePagadoIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_IMPPAG_2", DbType.String, 12, importePagadoFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROCOB_1", DbType.String, 12, nroCobranzaIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROCOB_2", DbType.String, 12, nroCobranzaFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROACRE_1", DbType.String, 12, nroAcreedorIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROACRE_2", DbType.String, 12, nroAcreedorFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_INI", DbType.String, 10, fechaIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_FIN", DbType.String, 10, fehcaFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NRODOC_DEU1", DbType.String, 21, nroDocDeudorIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NRODOC_DEU2", DbType.String, 21, nroDocDeudorFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROTRAC_1", DbType.String, 12, nroTracePagoIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROTRAC_2", DbType.String, 12, nroTracePagoFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CNTREG", DbType.String, 6, cntRegistro, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_GetRecauRecibos"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function


    Public Sub RegistrarLog(ByVal descripcion As String, ByVal codigo As String, ByVal deserror As String, _
                            ByVal usuario As String, ByVal nombretabla As String, ByVal archivo As String)

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_DESC_TRANSACCION", DbType.String, 600, descripcion, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_CODIGO_TRANSACCION", DbType.String, 50, codigo, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_DESC_ERROR", DbType.String, 500, deserror, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_USUARIO_TRANSACCION", DbType.String, 20, usuario, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_NOMBRE_TABLA", DbType.String, 40, nombretabla, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_ARCHIVO_LOG", DbType.String, 10000, IIf(archivo.Equals(String.Empty), 0, archivo), ParameterDirection.Input) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_Trs_Registra_Log_Trs"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
    End Sub

#End Region

#Region "CODIGO PARAMETROS"

    Public Function GetCodigos(ByVal agrupador As String, ByVal descripcion As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_DESC", DbType.String, 50, descripcion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_AGRUPADOR", DbType.String, 30, agrupador, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_GetCodigosCte"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function

#End Region

#Region "Configuracion Cuenta Remesa"

    Public Function GetCtaRemesa(ByVal oficina As String, ByVal IDCtaRemesa As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_ID_CTAREMESA", DbType.String, 15, IDCtaRemesa, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_GetCtaRemesa"
        objRequest.Parameters.AddRange(arrParam)
        GetCtaRemesa = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function SetCtaRemesa(ByVal oficinaVenta As String, ByVal paycall As String, _
                                ByVal tipoRemesa As String, ByVal cuenta As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficinaVenta, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_PAYCALL", DbType.String, 18, paycall, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_TIPO_REMESA", DbType.String, 2, tipoRemesa, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_CTABANCO", DbType.String, 10, cuenta, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 500, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_RegistrarCtaRemesa"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(4), IDataParameter).Value.ToString
    End Function

    Public Function ActualizarCtaRemesa(ByVal CtaRemesaID As Int32, ByVal oficinaVenta As String, _
                                ByVal paycall As String, ByVal tipoRemesa As String, ByVal cuenta As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_ID_CTAREMESA", DbType.Int32, CtaRemesaID, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficinaVenta, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_PAYCALL", DbType.String, 18, paycall, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_TIPO_REMESA", DbType.String, 2, tipoRemesa, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_CTABANCO", DbType.String, 10, cuenta, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 500, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_ActualizarCtaRemesa"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(5), IDataParameter).Value.ToString
    End Function

    Public Function EliminarCtaRemesa(ByVal CtaRemesaID As Int32) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_ID_CTAREMESA", DbType.Int32, CtaRemesaID, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".CONF_EliminarCtaRemesa"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(1), IDataParameter).Value.ToString
    End Function

#End Region

#Region "Contabilizacion Manual - Remesa"

    Public Function GetRemesaContabilizar(ByVal oficina As String, ByVal fechaDocumento As String, _
                                            ByVal fechaContabilizar As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 1000, oficina, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fechaDocumento, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_CTB", DbType.String, 10, fechaContabilizar, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS_TIPO1", DbType.Object, ParameterDirection.Output), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS_TIPO2", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_GetContabilizaRem"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function

    Public Function ActualizarRemesaContab(ByVal oficinaVta As String, ByVal fechaDoc As String, _
                              ByVal tipoRemesa As String, ByVal docContable As String, ByVal descErr As String, ByVal usuario As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        'Dim strUserIDBD As String = Me.GetUserIDByProvider(strCadenaConexion, "1")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficinaVta, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_FECHA", DbType.String, 10, fechaDoc, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_TIPO_REM", DbType.String, 2, tipoRemesa, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_DOC_CONTABLE", DbType.String, 10, docContable, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 12, usuario, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_DESC_ERR", DbType.String, 50, descErr, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 2000, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_ActualizarRemContab"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(6), IDataParameter).Value.ToString
    End Function

    Public Function GetRemesa(ByVal BolsaIni As String, ByVal BolsaFin As String, _
                                ByVal SobreIni As String, ByVal SobreFin As String, _
                                ByVal FechaIni As String, ByVal FechaFin As String, _
                                ByVal FechaBuzIni As String, ByVal FechaBuzFin As String, _
                                ByVal Cajero As String, ByVal MtoIni As String, _
                                ByVal MtoFin As String, ByVal TipoRemesa As String, _
                                ByVal OficinaVta As String, ByVal DocContableIni As String, _
                                ByVal DocContableFin As String, ByVal CodUsuario As String, _
                                ByVal FechaFIIni As String, ByVal FechaFIFin As String, _
                                ByVal cntRegistros As String) As DataSet

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim ds As DataSet
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_NROBLS_1", DbType.String, 10, BolsaIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROBLS_2", DbType.String, 10, BolsaFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROSBR_1", DbType.String, 10, SobreIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_NROSBR_2", DbType.String, 10, SobreFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_INI", DbType.String, 10, FechaIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_FIN", DbType.String, 10, FechaFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECBUZ_INI", DbType.String, 10, FechaBuzIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECBUZ_FIN", DbType.String, 10, FechaBuzFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_USUARIO", DbType.String, 10, Cajero, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_IMPPAG_1", DbType.String, 20, MtoIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_IMPPAG_2", DbType.String, 20, MtoFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_TIPREM", DbType.String, 2, TipoRemesa, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, OficinaVta, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DOCCONT_1", DbType.String, 10, DocContableIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DOCCONT_2", DbType.String, 10, DocContableFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_C1", DbType.String, 10, FechaFIIni, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_FECHA_C2", DbType.String, 10, FechaFIFin, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_USUARIO_CON", DbType.String, 50, CodUsuario, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_CNTREG", DbType.String, 6, cntRegistros, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".PROC_GetRemesas"
        objRequest.Parameters.AddRange(arrParam)

        ds = objRequest.Factory.ExecuteDataset(objRequest)
        Return ds
    End Function

#End Region

#Region "TICKETERA - OFICINA"

    Public Function GetTicketeraOficina(ByVal oficina As String, ByVal serie As String, ByVal idTicketeraOficina As Integer) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                        New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 25, oficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_SERIE", DbType.String, 20, serie, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("P_ID_LISTA", DbType.Int32, idTicketeraOficina, ParameterDirection.Input), _
                                        New DAAB.DAABRequest.Parameter("C_DATOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_GetImpresoras"
        objRequest.Parameters.AddRange(arrParam)
        GetTicketeraOficina = objRequest.Factory.ExecuteDataset(objRequest)
    End Function

    Public Function SetTicketeraOficina(ByVal oficina As String, ByVal caja As String, _
                                   ByVal descripcion As String, ByVal serie As String, ByVal usuadoPor As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficina, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_CAJA", DbType.String, 5, caja, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_DESC", DbType.String, 40, descripcion, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_SERIE", DbType.String, 20, serie, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_USADO", DbType.String, 10, usuadoPor, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MsgErr", DbType.String, 2000, String.Empty, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_RegistrarImpresora"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(5), IDataParameter).Value.ToString
    End Function

    Public Function ActualizarTicketeraOficina(ByVal IDLista As Int32, ByVal oficinaVenta As String, _
                                ByVal caja As String, ByVal descripcion As String, ByVal serie As String, ByVal usadoPor As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_ID_LISTA", DbType.Int32, IDLista, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_OFICINA", DbType.String, 4, oficinaVenta, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_CAJA", DbType.String, 5, caja, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_DESC", DbType.String, 40, descripcion, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_SERIE", DbType.String, 20, serie, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_USADO", DbType.String, 10, usadoPor, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MsgErr", DbType.String, 2000, String.Empty, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_ActualizarImpresora"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(6), IDataParameter).Value.ToString
    End Function

    Public Function EliminarTicketeraOficina(ByVal idLista As Int32) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")
        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                                                       New DAAB.DAABRequest.Parameter("P_ID_LISTA", DbType.Int32, idLista, ParameterDirection.Input), _
                                                       New DAAB.DAABRequest.Parameter("P_MSGERR", DbType.String, 2000, String.Empty, ParameterDirection.Output) _
                                                       }
        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = pkgNameOffSAP & ".MIG_EliminarImpresora"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)
        Return CType(objRequest.Parameters(1), IDataParameter).Value.ToString
    End Function

#End Region

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

End Class
