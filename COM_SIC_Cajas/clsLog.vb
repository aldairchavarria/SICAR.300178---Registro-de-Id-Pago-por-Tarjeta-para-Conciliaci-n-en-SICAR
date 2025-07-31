Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB

Public Class clsLog
    Dim strCadenaConexion As String
    Dim objSeg As New Seguridad_NET.clsSeguridad
    Public Function FK_RegistrarLog(ByVal p_Transaccion As String, _
                                ByVal p_UsuarioLogin As String, _
                                ByVal p_IpCliente As String, ByVal p_NombreCliente As String, _
                                ByVal p_IpServidor As String, ByVal p_NombreServidor As String, _
                                ByVal p_ParamsIn As String, ByVal p_ParamsOut As String, _
                                ByVal p_Exception As String) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TRANSACCION", DbType.String, 50, p_Transaccion, ParameterDirection.Input), _
                         New DAAB.DAABRequest.Parameter("P_USUARIO_LOGIN", DbType.String, 30, p_UsuarioLogin, ParameterDirection.Input), _
                         New DAAB.DAABRequest.Parameter("P_IP_CLIENTE", DbType.String, 15, p_IpCliente, ParameterDirection.Input), _
                         New DAAB.DAABRequest.Parameter("P_NOMBRE_CLIENTE", DbType.String, 30, p_NombreCliente, ParameterDirection.Input), _
                         New DAAB.DAABRequest.Parameter("P_IP_SERVIDOR", DbType.String, 15, p_IpServidor, ParameterDirection.Input), _
                         New DAAB.DAABRequest.Parameter("P_NOMBRE_SERVIDOR", DbType.String, 30, p_NombreServidor, ParameterDirection.Input), _
                         New DAAB.DAABRequest.Parameter("P_PARAMS_IN", DbType.String, 1000, p_ParamsIn, ParameterDirection.Input), _
                         New DAAB.DAABRequest.Parameter("P_PARAMS_OUT", DbType.String, 1000, p_ParamsOut, ParameterDirection.Input), _
                         New DAAB.DAABRequest.Parameter("P_EXCEPTION_TRANS", DbType.String, 1000, p_Exception, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SICAJ_UTIL.SP_REGISTRAR_LOG"
        objRequest.Parameters.AddRange(arrParam)
        FK_RegistrarLog = objRequest.Factory.ExecuteNonQuery(objRequest)
    End Function
End Class
