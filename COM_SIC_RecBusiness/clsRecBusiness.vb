Imports Claro.Datos
Imports System.Configuration

Public Class clsRecBusiness

    Dim strCadenaConexion As String
    Dim objSeg As New Seguridad_NET.clsSeguridad

    Public Function FP_DeudaXRucDNI(ByVal strRucDNI As String) As DataSet

        Dim strReOac As String = ConfigurationSettings.AppSettings("RE_OAC")
        Dim strOrigenBSCS As String = ConfigurationSettings.AppSettings("ORIGEN_BSCS")
        strCadenaConexion = objSeg.FP_GetConnectionString("2", strReOac)

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PC_CURSOR", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PV_DOC_IDE", DbType.String, 12, strRucDNI, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PV_ORIGEN", DbType.String, 12, strOrigenBSCS, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PV_STATUS", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PV_MESSAGE", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "apps.TS_OAC_CL_SICAR_PKG.pr_consulta_clie_corp"
        objRequest.Parameters.AddRange(arrParam)

        FP_DeudaXRucDNI = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_MontoDisputa(ByVal strRucDNI As String) As DataSet

        Dim strReOac As String = ConfigurationSettings.AppSettings("RE_OAC")
        Dim strOrigenBSCS As String = ConfigurationSettings.AppSettings("ORIGEN_BSCS")
        strCadenaConexion = objSeg.FP_GetConnectionString("2", strReOac)

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PC_CURSOR", DbType.Object, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PV_DOCUMENTO", DbType.String, 12, strRucDNI, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PV_ORIGEN", DbType.String, 12, strOrigenBSCS, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PV_TIPO_DOC", DbType.String, 12, "", ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("PV_STATUS", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("PV_MESSAGE", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "apps.TS_OAC_CL_SICAR_PKG.pr_consulta_monto_disputa"
        objRequest.Parameters.AddRange(arrParam)

        FP_MontoDisputa = objRequest.Factory.ExecuteDataset(objRequest)

    End Function

    Public Function FP_TipoCliente(ByVal strTelefono As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SICARBSCS")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_MSISDN", DbType.String, 12, strTelefono, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_TIPO_CLIENTE", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "TIM.TIM100_PKG_CONSULTAS_BSCS.SP_TIPO_CLIENTE"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)
        FP_TipoCliente = CType(objRequest.Parameters(1), IDataParameter).Value

    End Function

    Public Function FP_UltimoRecibo(ByVal strNumTelf As String) As String
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SICARBSCS")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("p_Dn_Num", DbType.String, 20, strNumTelf, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("p_ohrefnum", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "TIM.TIM100_PKG_CONSULTAS_BSCS.sp_Get_ohrefnum_from_co_id"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        FP_UltimoRecibo = CType(objRequest.Parameters(1), IDataParameter).Value

    End Function

    Public Function FP_UltimoReciboDatos(ByVal strNumTelf As String) As String

        Dim strReOac As String = ConfigurationSettings.AppSettings("RE_OAC")
        Dim strOrigenBSCS As String = ConfigurationSettings.AppSettings("ORIGEN_BSCS")
        strCadenaConexion = objSeg.FP_GetConnectionString("2", strReOac)

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PV_TELEFONO", DbType.String, 20, strNumTelf, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("PV_TRX_NUMBER", DbType.String, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("PV_FISCAL_CODE", DbType.String, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("PV_CUSTOMER_NAME", DbType.String, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("PV_ORIGEN", DbType.String, 12, strOrigenBSCS, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("PV_STATUS", DbType.String, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("PV_MESSAGE", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "apps.TS_OAC_CL_SICAR_PKG.pr_ultima_factura"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        FP_UltimoReciboDatos = CType(objRequest.Parameters(1), IDataParameter).Value & ";" & CType(objRequest.Parameters(2), IDataParameter).Value & ";" & CType(objRequest.Parameters(3), IDataParameter).Value

    End Function

    Public Function FP_ConsultaXNombre(ByVal strNomRazoc As String, ByVal strApellido As String) As DataSet

        Dim strReOac As String = ConfigurationSettings.AppSettings("RE_OAC")
        Dim strOrigenBSCS As String = ConfigurationSettings.AppSettings("ORIGEN_BSCS")
        strCadenaConexion = objSeg.FP_GetConnectionString("2", strReOac)

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("PC_CURSOR", DbType.Object, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("PV_NOMBRE", DbType.String, 255, strNomRazoc, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("PV_APELLIDO", DbType.String, 255, strApellido, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("PV_ORIGEN", DbType.String, 12, strOrigenBSCS, ParameterDirection.Input), _
                            New DAAB.DAABRequest.Parameter("PV_STATUS", DbType.String, ParameterDirection.Output), _
                            New DAAB.DAABRequest.Parameter("PV_MESSAGE", DbType.String, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "apps.TS_OAC_CL_SICAR_PKG.pr_consulta_deuda_cliente"
        objRequest.Parameters.AddRange(arrParam)

        FP_ConsultaXNombre = objRequest.Factory.ExecuteDataset(objRequest)

    End Function
End Class

