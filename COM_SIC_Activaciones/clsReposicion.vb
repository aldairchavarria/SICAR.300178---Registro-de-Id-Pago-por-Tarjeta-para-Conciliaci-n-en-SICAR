Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB
Public Class clsReposicion

    Dim strCadenaConexion As String
    Dim objSeg As New Seguridad_NET.clsSeguridad
    Dim strCadenaEsquema As String

    Public Function InsertarReposion4G(ByVal strCoID As Integer, ByVal strLinea As String, ByVal strEstado As String, _
                                        ByVal strTipoOperacion As String, ByVal strTrama As String, ByVal strObservaciones As String, _
                                        ByVal strFecha As String, ByVal strUsuReg As String, ByRef strCodResp As String, _
                                        ByRef strMsgResp As String) As Integer

        Try

            Dim stFechaCP As String

            If strFecha = "" Then
                stFechaCP = String.Empty
            Else
                stFechaCP = Convert.ToDateTime(strFecha)
            End If


            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")


            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_CO_ID", DbType.Int32, strCoID, ParameterDirection.Input), _
                              New DAAB.DAABRequest.Parameter("P_TELEFONO", DbType.String, strLinea, ParameterDirection.Input), _
                              New DAAB.DAABRequest.Parameter("P_ESTADO", DbType.String, strEstado, ParameterDirection.Input), _
                              New DAAB.DAABRequest.Parameter("P_TIPO_OPERACION", DbType.String, strTipoOperacion, ParameterDirection.Input), _
                              New DAAB.DAABRequest.Parameter("P_TRAMA", DbType.String, DBNull.Value, ParameterDirection.Input), _
                              New DAAB.DAABRequest.Parameter("P_OBSERVACIONES", DbType.String, DBNull.Value, ParameterDirection.Input), _
                              New DAAB.DAABRequest.Parameter("P_FECHA_CP", DbType.DateTime, strFecha, ParameterDirection.Input), _
                              New DAAB.DAABRequest.Parameter("P_USU_REG", DbType.String, strUsuReg, ParameterDirection.Input), _
                              New DAAB.DAABRequest.Parameter("P_CODIGO_RESPUESTA", DbType.String, strCodResp, ParameterDirection.Output), _
                              New DAAB.DAABRequest.Parameter("P_MENSAJE_RESPUESTA", DbType.String, strMsgResp, ParameterDirection.Output)}


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISACT_REPO_ACT.SISACTSI_VENTA_REPO_PEN"
            objRequest.Parameters.AddRange(arrParam)

            InsertarReposion4G = objRequest.Factory.ExecuteNonQuery(objRequest)

            strCodResp = CType(objRequest.Parameters(8), IDataParameter).Value
            strMsgResp = CType(objRequest.Parameters(9), IDataParameter).Value


        Catch ex As Exception
            strCodResp = -1
            strMsgResp = ex.Message.ToString()
        End Try


    End Function
    
	' Add Ini MVC 12/08/2016
    Public Sub GestionarCambioSim(ByVal p_Trama As String, ByVal strNroPedido As String, ByVal strCurrentUser As String, _
                                  ByVal strCurrentTerminal As String, ByRef strCodRpta As String, ByRef strMsgRpta As String)

        Dim oGestionarCambioSim As New GestionCambioSIMWS.GestionCambioSIMService
        oGestionarCambioSim.Url = ConfigurationSettings.AppSettings("ConstGestionCambioSim_Url")
        oGestionarCambioSim.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("ConstTimeOutGestionCambioSim"))
        oGestionarCambioSim.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim auditoria As New GestionCambioSIMWS.parametrosAuditRequest
        auditoria.idTransaccion = Format(Now, "yyyyMMddmmss") & strNroPedido
        auditoria.ipAplicacion = strCurrentTerminal
        auditoria.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
        auditoria.usuarioAplicacion = strCurrentUser

        Dim oRequest As New GestionCambioSIMWS.encolarReposicionesRequest
        oRequest.datosLineas = p_Trama

        oRequest.auditRequest = auditoria

        Dim listaRequestOpcional(0) As GestionCambioSIMWS.parametrosRequestObjetoRequestOpcional

        Dim oResponse As New GestionCambioSIMWS.encolarReposicionesResponse
        oResponse = oGestionarCambioSim.encolarReposiciones(oRequest)

        strCodRpta = oResponse.auditResponse.codigoRespuesta
        strMsgRpta = oResponse.auditResponse.mensajeRespuesta

    End Sub
    ' Fin MVC 12/08/2016


     'RepoChip    
     Public Function RegistrarReposicion(ByVal strTrama As String, ByVal strUsuario As String, _
                                        ByRef strCodRpta As String, _
                                        ByRef strMsgRpta As String) As Integer

        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_SEC")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("P_TRAMA", DbType.String, 4000, strTrama, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_USUARIO_REG", DbType.String, strUsuario, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("P_CODIGO_RESPUESTA", DbType.String, strCodRpta, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("P_MENSAJE_RESPUESTA", DbType.String, strMsgRpta, ParameterDirection.Output)}


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PKG_SISACT_REPO_ACT_ALTER.SISACTSI_VENTA_REPO_PEN_ARRAY"
        objRequest.Parameters.AddRange(arrParam)

        RegistrarReposicion = objRequest.Factory.ExecuteNonQuery(objRequest)

        strCodRpta = CType(objRequest.Parameters(2), IDataParameter).Value
        strMsgRpta = CType(objRequest.Parameters(3), IDataParameter).Value

    End Function

    Public Sub EnviarDatosReposicion(ByVal strRemitente As String, ByVal strDestinatario As String, _
                                    ByVal strAsunto As String, ByVal strMensaje As String, _
                                    ByVal strHTMLFlag As String, ByVal strNroPedido As String, _
                                    ByVal strCurrentUser As String, ByVal strCurrentTerminal As String, _
                                    ByRef strCodRpta As String, ByRef strMsgRpta As String)

        Try

            Dim oEnvioCorreo As New EnviarCorreoWS.envioCorreoWSService
            oEnvioCorreo.Url = ConfigurationSettings.AppSettings("ConsEnvioCorreoWS")
            oEnvioCorreo.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("ConsEnvioCorreoWSTimeOut"))
            oEnvioCorreo.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim auditoria As New EnviarCorreoWS.AuditTypeRequest

            auditoria.idTransaccion = Format(Now, "yyyyMMddmmss") & strNroPedido
            auditoria.ipAplicacion = strCurrentTerminal
            auditoria.codigoAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
            auditoria.usrAplicacion = strCurrentUser

            Dim oParametroOpcional() As EnviarCorreoWS.ParametroOpcionalComplexType

            oEnvioCorreo.enviarCorreo(auditoria, strRemitente, strDestinatario, strAsunto, strMensaje, strHTMLFlag, oParametroOpcional, oParametroOpcional)

            strCodRpta = "0"
            strMsgRpta = "OK"

        Catch ex As Exception

            strCodRpta = "-1"
            strMsgRpta = ex.Message.ToString()

        End Try

    End Sub

    Public Sub EncolarReposicion(ByVal p_Trama As String, ByVal strNroPedido As String, ByVal strCurrentUser As String, ByVal strCurrentTerminal As String, ByRef strCodRpta As String, ByRef strMsgRpta As String)

        Dim oEncolar As New GestionReposicionWS.GestionCambioSIMService
        oEncolar.Url = ConfigurationSettings.AppSettings("GestionReposicionWS_url")
        oEncolar.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("GestionReposicionWS_timeout"))
        oEncolar.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim auditoria As New GestionReposicionWS.parametrosAuditRequest
        auditoria.idTransaccion = Format(Now, "yyyyMMddmmss") & strNroPedido
        auditoria.ipAplicacion = strCurrentTerminal
        auditoria.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
        auditoria.usuarioAplicacion = strCurrentUser

        Dim oRequest As New GestionReposicionWS.encolarReposicionesRequest
        oRequest.datosLineas = p_Trama

        oRequest.auditRequest = auditoria

        Dim listaRequestOpcional(0) As GestionReposicionWS.parametrosRequestObjetoRequestOpcional

        Dim oResponse As New GestionReposicionWS.encolarReposicionesResponse
        oResponse = oEncolar.encolarReposiciones(oRequest)

        strCodRpta = oResponse.auditResponse.codigoRespuesta
        strMsgRpta = oResponse.auditResponse.mensajeRespuesta

    End Sub
    'RepoChip  

End Class
