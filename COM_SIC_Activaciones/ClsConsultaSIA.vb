Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB
Public Class ClsConsultaSIA

    Dim objSeg As New Seguridad_NET.clsSeguridad
    Dim strCadenaConexion As String
    Dim strCadenaEsquema As String = ""

    Dim EstablecimientoSUNATWS As New EstablecimientoSUNATWS.EstablecimientoSUNATWSService

    Public Function ConsultaEstablecimiento(ByVal pi_sociedad As String, _
                                            ByVal pi_tipodoc As String, _
                                            ByVal pi_serie As String, _
                                            ByRef po_codest As String, _
                                            ByRef po_codmsg As String, _
                                            ByRef po_mensaje As String) As String

        strCadenaConexion = objSeg.FP_GetConnectionString("2", ConfigurationSettings.AppSettings("BD_SIAPDV"))

        strCadenaEsquema = ConfigurationSettings.AppSettings("EsquemaSIAPDV")

        If (Not IsDBNull(strCadenaEsquema) AndAlso strCadenaEsquema.Length > 0) Then
            strCadenaEsquema = strCadenaEsquema & "."
        Else
            strCadenaEsquema = String.Empty
        End If

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = { _
                          New DAAB.DAABRequest.Parameter("K_SOCIEDAD", DbType.String, pi_sociedad, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_TIPOCOMPROBANTE", DbType.String, pi_tipodoc, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_SERIE", DbType.String, pi_serie, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("K_CODESTABLECIMIENTO", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("K_CODMENSAJE", DbType.String, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("K_MENSAJE", DbType.String, ParameterDirection.Output)}

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = strCadenaEsquema & "PKG_ESTABLECIMIENTO_SUNAT.SP_CONSULTA_ESTABLECIMIENTO"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            Dim parSalida1 As IDataParameter
            Dim parSalida2 As IDataParameter
            Dim parSalida3 As IDataParameter
            parSalida1 = CType(objRequest.Parameters(3), IDataParameter)
            parSalida2 = CType(objRequest.Parameters(4), IDataParameter)
            parSalida3 = CType(objRequest.Parameters(5), IDataParameter)

            po_codest = Funciones.CheckStr(parSalida1.Value)
            po_codmsg = Funciones.CheckStr(parSalida2.Value)
            po_mensaje = Funciones.CheckStr(parSalida3.Value)

            ConsultaEstablecimiento = "CODESTABLECIMIENTO: " & po_codest & " - CODMENSAJE: " & po_codmsg & _
                                      " - MENSAJE: " & po_mensaje

        Catch ex As Exception
            ConsultaEstablecimiento = ex.Message.ToString()
        Finally
            objRequest.Parameters.Clear()
            objRequest.Factory.Dispose()
        End Try

    End Function

    Public Function ConsultaEstablecimientoWS(ByVal pi_sociedad As String, _
                                            ByVal pi_tipodoc As String, _
                                            ByVal pi_serie As String, _
                                            ByRef po_codest As String, _
                                            ByRef po_codmsg As String, _
                                            ByRef po_mensaje As String) As String


        Try
            EstablecimientoSUNATWS.Url = ConfigurationSettings.AppSettings("RutaWS_EstablecimientoSUNAT").ToString()
            Dim p As New System.Net.WebProxy
            p.Credentials = System.Net.CredentialCache.DefaultCredentials
            EstablecimientoSUNATWS.Proxy = p
            EstablecimientoSUNATWS.Timeout = ConfigurationSettings.AppSettings("TimeoutWS").ToString()
            Dim objEstablecimientoResponse As New EstablecimientoSUNATWS.establecimientoSUNATResponse

            Dim objEstablecimientoType As New EstablecimientoSUNATWS.establecimientoSUNATType
            objEstablecimientoType.sociedad = pi_sociedad
            objEstablecimientoType.tipoComprobante = pi_tipodoc
            objEstablecimientoType.serie = pi_serie
            Dim objlistaRequestOpcional(0) As EstablecimientoSUNATWS.parametrosTypeObjetoOpcional
            objlistaRequestOpcional(0) = New EstablecimientoSUNATWS.parametrosTypeObjetoOpcional
            objlistaRequestOpcional(0).campo = String.Empty
            objlistaRequestOpcional(0).valor = String.Empty
            Dim objEstablecimientoRequest As New EstablecimientoSUNATWS.establecimientoSUNATRequest
            objEstablecimientoRequest.auditRequest = New EstablecimientoSUNATWS.auditRequestType
            objEstablecimientoRequest.auditRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHss")
            objEstablecimientoRequest.auditRequest.ipAplicacion = ConfigurationSettings.AppSettings("CodAplicacion")
            objEstablecimientoRequest.auditRequest.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
            objEstablecimientoRequest.auditRequest.usuarioAplicacion = String.Empty

            objEstablecimientoRequest.establecimientoSUNAT = objEstablecimientoType
            objEstablecimientoRequest.listaRequestOpcional = objlistaRequestOpcional

            objEstablecimientoResponse = EstablecimientoSUNATWS.establecerSUNAT(objEstablecimientoRequest)

            po_codest = objEstablecimientoResponse.codEstablecimiento
            po_codmsg = objEstablecimientoResponse.auditResponse.codigoRespuesta
            po_mensaje = objEstablecimientoResponse.auditResponse.mensajeRespuesta

            ConsultaEstablecimientoWS = "CODESTABLECIMIENTO: " & po_codest & " - CODMENSAJE: " & po_codmsg & _
                                      " - MENSAJE: " & po_mensaje
        Catch ex As Exception
            po_codmsg = "-1"
            po_mensaje = ex.Message.ToString()
        End Try
        Return ConsultaEstablecimientoWS
    End Function
End Class
