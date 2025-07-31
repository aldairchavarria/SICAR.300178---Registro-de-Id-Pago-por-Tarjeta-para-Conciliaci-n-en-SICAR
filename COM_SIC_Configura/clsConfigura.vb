Imports Claro.Datos
Imports Claro.Datos.DAAB
Imports System.Configuration
Public Class clsConfigura
    Dim strCadenaConexion As String
    Dim objSeg As New Seguridad_NET.clsSeguridad

    Public Function FP_Consulta_BIN(ByVal dblCodBin As Double) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_COD_BIN", DbType.Double, dblCodBin, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_LISTBIN", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_VISUALIZA_BIN"
        objRequest.Parameters.AddRange(arrParam)

        FP_Consulta_BIN = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_Actualiza_BIN(ByVal dblIdCodBin As Double, ByVal dblCodBin As Double, ByVal strDescripcion As String, ByVal intEstado As Integer) As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_ID_CODBIN", DbType.Double, dblIdCodBin, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_BIN", DbType.Double, dblCodBin, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_DESCRIPCION", DbType.String, 100, strDescripcion, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_ESTADO", DbType.Int16, intEstado, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_RETVAL", DbType.Int16, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_ACTUALIZA_BIN"
        objRequest.Parameters.AddRange(arrParam)

        FP_Actualiza_BIN = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_Visualiza_Turno(ByVal dblCodTurno As Double) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODTURNO", DbType.Double, dblCodTurno, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_DATOTURNO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_VISUALIZA_TURNO"
        objRequest.Parameters.AddRange(arrParam)

        FP_Visualiza_Turno = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_Actualiza_Turno(ByVal dblCodTurno As Double, ByVal strDescripcion As String, ByVal strHoraIni As String, ByVal strHoraFin As String, ByVal intTolerancia As Integer, ByVal intEstado As Integer) As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_COD_TURNO", DbType.Double, dblCodTurno, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_DESCRIPCION", DbType.String, 100, strDescripcion, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_HORAINI", DbType.String, strHoraIni, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_HORAFIN", DbType.String, strHoraFin, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_TOLERANCIA", DbType.Int16, intTolerancia, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_ESTADO", DbType.Int16, intEstado, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_RETVAL", DbType.Int16, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_ACTUALIZA_TURNO"
        objRequest.Parameters.AddRange(arrParam)

        FP_Actualiza_Turno = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_Lista_Turno_Activos(ByVal strCanal As String, ByVal intApli As Integer, ByVal strOficina As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_COD_CANAL", DbType.String, 5, strCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_APLIC_COD", DbType.Int16, intApli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_PDV", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_LISTTURNO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_LISTA_TURNO_ACTIVOS"
        objRequest.Parameters.AddRange(arrParam)

        FP_Lista_Turno_Activos = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_Lista_Aut_Transaccion(ByVal strCanal As String, ByVal intApli As Integer, ByVal strOficina As String, ByVal strFecha As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODCANAL", DbType.String, 5, strCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_APLIC_COD", DbType.Int16, intApli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_PDV", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_FEHC_TRX", DbType.String, 10, strFecha, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_LISTAUT", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_LISTA_AUT_TRANSACCION"
        objRequest.Parameters.AddRange(arrParam)

        FP_Lista_Aut_Transaccion = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_Autoriza_Transaccion(ByVal intAuTran As Integer, ByVal intAutoriza As Integer, ByVal strMotivo As String, ByVal strOtroMot As String) As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_COD_AUTRAN", DbType.Int32, intAuTran, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_AUTORIZA", DbType.Int32, intAutoriza, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_MOTIVO", DbType.String, 18, strMotivo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_OTROMOT", DbType.String, 150, strOtroMot, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_RETVAL", DbType.Int16, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_ACT_AUTORIZA_TRANSACCION"
        objRequest.Parameters.AddRange(arrParam)

        FP_Autoriza_Transaccion = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_Lista_Transaccion(ByVal strCanal As String, ByVal intApli As Integer, ByVal strOficina As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODCANAL", DbType.String, 5, strCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_PDV", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_APLIC_COD", DbType.Int16, intApli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_LISTTRAN", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_LISTA_TRANSAC"
        objRequest.Parameters.AddRange(arrParam)

        FP_Lista_Transaccion = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_Elimina_Transac_Rest(ByVal strCanal As String, ByVal intApli As Integer, ByVal strOficina As String) As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODCANAL", DbType.String, 5, strCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_PDV", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_APLIC_COD", DbType.Int16, intApli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_RETVAL", DbType.Int16, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_LIM_TRANSAC_RESTRINGIDA"
        objRequest.Parameters.AddRange(arrParam)

        FP_Elimina_Transac_Rest = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_Actualiza_Transac_Rest(ByVal strCanal As String, ByVal strOficina As String, ByVal intApli As Integer, ByVal intCodTran As Integer) As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODCANAL", DbType.String, 5, strCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_PDV", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_APLIC_COD", DbType.Int16, intApli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CODTRXN", DbType.Int16, intCodTran, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_RETVAL", DbType.Int16, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_ACT_TRANSAC_RESTRINGIDA"
        objRequest.Parameters.AddRange(arrParam)

        FP_Actualiza_Transac_Rest = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_Consulta_Transac_Rest(ByVal strCanal As String, ByVal strOficina As String, ByVal intApli As Integer) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODCANAL", DbType.String, 5, strCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_PDV", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_APLIC_COD", DbType.Int16, intApli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_LISTTRANREST", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_TRANSAC_RESTRINGIDAS"
        objRequest.Parameters.AddRange(arrParam)

        FP_Consulta_Transac_Rest = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_Lista_BIN(ByVal strCanal As String, ByVal intApli As Integer) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODCANAL", DbType.String, 5, strCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_APLIC_COD", DbType.Int16, intApli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_LISTBIN", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_LISTA_BIN"
        objRequest.Parameters.AddRange(arrParam)

        FP_Lista_BIN = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_Lista_Turno(ByVal strCanal As String, ByVal intApli As Integer, ByVal strOficina As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODCANAL", DbType.String, 5, strCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_APLIC_COD", DbType.Decimal, intApli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_PDV", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_LISTTURNO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_LISTA_TURNO"
        objRequest.Parameters.AddRange(arrParam)

        FP_Lista_Turno = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_Ingresa_BIN(ByVal strCanal As String, ByVal strOficina As String, ByVal intApli As Integer, ByVal dblCodBin As Double, ByVal strDescripcion As String, ByVal intEstado As Integer) As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_COD_CANAL", DbType.String, 5, strCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_PDV", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_APLIC_COD", DbType.Int16, intApli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CODBIN", DbType.Double, dblCodBin, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_DESCRIPCION", DbType.String, 100, strDescripcion, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_ESTADO", DbType.Int16, intEstado, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_RETVAL", DbType.Int16, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_INGRESA_BIN"
        objRequest.Parameters.AddRange(arrParam)

        FP_Ingresa_BIN = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_Ingresa_Turno(ByVal strCanal As String, ByVal strOficina As String, ByVal intApli As Integer, ByVal strDescripcion As String, ByVal strHoraIni As String, ByVal strHoraFin As String, ByVal intTolerancia As Integer, ByVal intEstado As Integer) As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_COD_CANAL", DbType.String, 5, strCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_PDV", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_APLIC_COD", DbType.Int16, intApli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_DESCRIPCION", DbType.String, 100, strDescripcion, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_HORAINI", DbType.String, strHoraIni, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_HORAFIN", DbType.String, strHoraFin, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_TOLERANCIA", DbType.Int16, intTolerancia, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_ESTADO", DbType.Int16, intEstado, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_RETVAL", DbType.Int16, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_INGRESA_TURNO"
        objRequest.Parameters.AddRange(arrParam)

        FP_Ingresa_Turno = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_Lista_Bancos() As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CUR_LISTBANC", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_LISTA_BANC"
        objRequest.Parameters.AddRange(arrParam)

        FP_Lista_Bancos = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_Lista_Moneda() As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CUR_LISTMONEDA", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_TIPO_MONEDA"
        objRequest.Parameters.AddRange(arrParam)

        FP_Lista_Moneda = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_Lista_Param_Oficina(ByVal strCanal As String, ByVal strOficina As String, ByVal intApli As Integer) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODCANAL", DbType.String, 5, strCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_PDV", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_APLIC_COD", DbType.Int16, intApli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_LISTPARAMOFIC", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_LISTA_PARAM_OFICINA"
        objRequest.Parameters.AddRange(arrParam)

        FP_Lista_Param_Oficina = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    '******************************************************************************'
    'Funciòn para consultar los nuevos codigos de materiales para recarga virtual
    '" " - 30-12-14 -  EVERIS '
    '*******************************************************************************'
    Public Function ConsultaCodigoMaterialRecargaVirtual(ByVal K_VALORHISTORICO As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_VALORHISTORICO", DbType.String, 50, K_VALORHISTORICO, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_CODMATERIAL", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_MATERIALES_RECARGA"
        objRequest.Parameters.AddRange(arrParam)

        ConsultaCodigoMaterialRecargaVirtual = objRequest.Factory.ExecuteDataset(objRequest)
    End Function





    Public Function FP_Actualiza_Param_Oficina(ByVal strCanal As String, ByVal strOficina As String, ByVal intApli As Integer, ByVal strOrgVta As String, ByVal strCanalDes As String, ByVal strPDVDes As String, ByVal strSecDes As String, ByVal dblMonNac As Double, _
    ByVal dblBanNac As Double, ByVal strCtaNac As String, ByVal dblMonEx As Double, ByVal dblBanEx As Double, ByVal strCtaEx As String, ByVal dblEfecSol As Double, ByVal dblMaxSol As Double, _
    ByVal dblTolSol As Double, ByVal dblEfecDol As Double, ByVal dblMaxDol As Double, ByVal dblTolDol As Double, ByVal intCambio As Integer, ByVal intImpSap As Integer, ByVal intEstado As Integer) As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODCANAL", DbType.String, 5, strCanal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_PDV", DbType.String, 5, strOficina, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_APLIC_COD", DbType.Int16, intApli, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_COD_ORGVTA", DbType.String, 5, strOrgVta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CANAL_DESC", DbType.String, 100, strCanalDes, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_PDV_DESC", DbType.String, 100, strPDVDes, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_SECTOR_DESC", DbType.String, 5, strSecDes, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_MON_NACIONAL", DbType.Double, dblMonNac, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_BANC_MON_NAC", DbType.Double, dblBanNac, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CTA_MON_NAC", DbType.String, 20, strCtaNac, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_MON_EXTRAN", DbType.Double, dblMonEx, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_BANC_MON_EXTRAN", DbType.Double, dblBanEx, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CTA_MON_EXTRAN", DbType.String, 20, strCtaEx, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_EFEC_SOL", DbType.Double, dblEfecSol, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_MAX_SOL", DbType.Int16, dblMaxSol, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_TOL_SOL", DbType.Double, dblTolSol, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_EFE_DOL", DbType.Double, dblEfecDol, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_MAX_DOL", DbType.Double, dblMaxDol, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_TOL_DOL", DbType.Double, dblTolDol, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_VALIDA_CAMBIO_FECHA", DbType.Int16, intCambio, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_VALIDA_IMP_SAP", DbType.Int16, intImpSap, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_ESTADO", DbType.Int16, intEstado, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_RETVAL", DbType.Int16, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_ACTUALIZA_PARAM_OFICINA"
        objRequest.Parameters.AddRange(arrParam)

        FP_Actualiza_Param_Oficina = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_Inserta_Aut_Transac(ByVal strCanal As String, ByVal strOficina As String, ByVal intApli As Integer, ByVal strCodCajero As String, ByVal strNomCajero As String, _
    ByVal strTipDoc As String, ByVal strNumDoc As String, ByVal strNomCliente As String, ByVal strNumTel As String, ByVal strFactSunat As String, ByVal strDocSap As String, ByVal dblMontoNeto As Double, _
    ByVal intTransac As Integer, ByVal dblSaldoIni As Double, ByVal dblEfectivo As Double, ByVal dblCajaBuzon As Double, ByVal dblRemesa As Double, ByVal dblMontoPen As Double, ByVal dblMontoSob As Double, ByVal strMotivo As String, Optional ByVal strAsesor As String = "", Optional ByVal dblMonto As Double = 0) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

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
                          New DAAB.DAABRequest.Parameter("K_RETORNO", DbType.Int16, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_INSERTA_AUT_TRANSACCION"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        FP_Inserta_Aut_Transac = CType(objRequest.Parameters(22), IDataParameter).Value

    End Function

    Public Function FP_Lista_Motivos() As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CUR_MOTIVOS", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_LISTA_MOTIVOS"
        objRequest.Parameters.AddRange(arrParam)

        FP_Lista_Motivos = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_Lista_Rep_Autorizaciones(ByVal strCodPDV As String, ByVal datFecIni As Date, ByVal datFecFin As Date) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODPDV", DbType.String, 5, strCodPDV, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_CODFECINI", DbType.Date, datFecIni, ParameterDirection.Input), _
        New DAAB.DAABRequest.Parameter("K_CODFECFIN", DbType.Date, datFecFin, ParameterDirection.Input), _
         New DAAB.DAABRequest.Parameter("K_CUR_LISTADO", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_LISTA_AUTORIZACIONES"
        objRequest.Parameters.AddRange(arrParam)

        FP_Lista_Rep_Autorizaciones = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_Actualiza_Param_Tarjeta(ByVal dblMonSol As Double, ByVal intCodigo As Integer) As Integer
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_MONTOSOLES", DbType.Double, 10, dblMonSol, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CODIGO", DbType.Int16, 2, intCodigo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_RETVAL", DbType.Int16, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_ACTUALIZA_PARAM_TARJETA"
        objRequest.Parameters.AddRange(arrParam)

        FP_Actualiza_Param_Tarjeta = objRequest.Factory.ExecuteNonQuery(objRequest)

    End Function

    Public Function FP_Lista_Param_Tarjeta(ByVal intCodigo As Integer) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("K_CODIGO", DbType.Int16, 2, intCodigo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CUR_LISTPARAMTAR", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CONF_PARAMETROS_CAJA.CONF_LISTA_PARAM_TARJETA"
        objRequest.Parameters.AddRange(arrParam)

        FP_Lista_Param_Tarjeta = objRequest.Factory.ExecuteDataset(objRequest)

    End Function
    Public Function FP_Consulta_RecargaVirtual(ByVal strValorRecarga As String, ByVal strDescRecarga As String, ByVal strEstadoRecarga As String) As DataSet
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_VALOR_RECARGA", DbType.String, 5, strValorRecarga, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DESCRIP_RECARGA", DbType.String, 30, strDescRecarga, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ESTADO_RECARGA", DbType.String, 1, strEstadoRecarga, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SICAR_PKG_RECARGA_VIRTUAL.SP_CONSULTA_RECARGA_VIRTUAL"
        objRequest.Parameters.AddRange(arrParam)

        Try
            FP_Consulta_RecargaVirtual = objRequest.Factory.ExecuteDataset(objRequest)
        Catch ex As Exception
            FP_Consulta_RecargaVirtual = New DataSet
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

    End Function

    Public Function FP_Grabar_RecargaVirtual(ByVal strValorRecarga As String, ByVal strDescripcion As String, ByVal strEstado As String, ByVal strUsuarioReg As String) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_VALOR_RECARGA", DbType.String, 5, strValorRecarga, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DESCRIP_RECARGA", DbType.String, 30, strDescripcion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ESTADO_RECARGA", DbType.String, 1, strEstado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_USUARIO_REG", DbType.String, 10, strUsuarioReg, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_RESULTADO", DbType.Int16, 1, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SICAR_PKG_RECARGA_VIRTUAL.SP_INSERT_RECARGA_VIRTUAL"
        objRequest.Parameters.AddRange(arrParam)

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            FP_Grabar_RecargaVirtual = CType(objRequest.Parameters(4), IDataParameter).Value
        Catch ex As Exception
            FP_Grabar_RecargaVirtual = 1
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

    End Function

    Public Function FP_Editar_RecargaVirtual(ByVal intID As Integer, ByVal strValorRecarga As String, ByVal strDescripcion As String, ByVal strEstado As String, ByVal strUsuarioUpd As String) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_REVIT_REVIN_ID", DbType.Int16, intID, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_VALOR_RECARGA", DbType.String, 5, strValorRecarga, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_DESCRIP_RECARGA", DbType.String, 30, strDescripcion, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_ESTADO_RECARGA", DbType.String, 1, strEstado, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("P_USUARIO_UPD", DbType.String, 10, strUsuarioUpd, ParameterDirection.Input), _
                                                        New DAAB.DAABRequest.Parameter("K_RESULTADO", DbType.Int16, 1, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "SICAR_PKG_RECARGA_VIRTUAL.SP_UPDATE_RECARGA_VIRTUAL"
        objRequest.Parameters.AddRange(arrParam)

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            FP_Editar_RecargaVirtual = CType(objRequest.Parameters(5), IDataParameter).Value
        Catch ex As Exception
            FP_Editar_RecargaVirtual = 1
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

    End Function
End Class
