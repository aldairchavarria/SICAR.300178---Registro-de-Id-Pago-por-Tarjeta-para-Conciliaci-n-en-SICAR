Imports System.Configuration
Public Class BWSIMPOST

    Dim oTransaccion As New COM_SIC_Activaciones.CambioSIMPostWS.CambioSIMPostpagoWS



    Public Sub New()

    End Sub

    Public Function CambioSIMPostpago(ByVal p_customerId As String, ByVal p_contratoId As String, ByVal p_iccid As String, ByVal p_nroTelefono As String, ByVal p_usuarioSistema As String, ByVal p_usuarioAplicacion As String, ByVal p_usuarioPass As String, ByVal p_fechaEjecucion As DateTime, _
     ByVal p_fecha As DateTime, _
     ByVal p_motivoCambio As String, _
     ByVal p_metodoContacto As String, _
     ByVal p_trace As String, _
     ByRef p_msg As String) As Boolean

        oTransaccion.Url = ConfigurationSettings.AppSettings("consRutaWSCambioSimPostpago").ToString()
        oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
        oTransaccion.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("ConstTimeOutActivacion").ToString())

        Dim idRetorno As String
        Dim vexito As Boolean = False
        Dim objRequest As New COM_SIC_Activaciones.CambioSIMPostWS.CAMBIO_SIM_REQUEST


        Dim objResponse As New COM_SIC_Activaciones.CambioSIMPostWS.CAMBIO_SIM_RESPONSE

        Try

            objRequest.CS_ID = Funciones.CheckInt64(p_customerId)
            objRequest.CO_ID = Funciones.CheckInt64(p_contratoId)
            objRequest.FECHA = p_fecha
            objRequest.FECHA_EJECUCION = p_fechaEjecucion
            objRequest.MOTIVO_CAMBIO = p_motivoCambio
            objRequest.METODO_CONTACTO = p_metodoContacto
            objRequest.MSISDN = p_nroTelefono
            objRequest.SERIAL_NUMBER_NEW = p_iccid
            objRequest.TRACE = p_trace
            objRequest.USUARIO_APLICACION = p_usuarioAplicacion
            objRequest.USUARIO_SISTEMA = p_usuarioSistema
            objRequest.PASSWORD_USUARIO = p_usuarioPass

            objResponse = oTransaccion.cambioSIMPostPago(objRequest)


            idRetorno = Funciones.CheckStr(objResponse.CODIGO_RETORNO)
            If (idRetorno.Equals("0") Or idRetorno.Equals("SER04018")) Then

                vexito = True
            End If
            p_msg = Funciones.CheckStr(objResponse.DESCRIPCION_RETORNO) + idRetorno


        Catch ex As Exception

            vexito = False
            p_msg = "El servicio esta temporalmente fuera de servicio. " + ex.Message + idRetorno

        Finally

            oTransaccion.Dispose()
        End Try

        Return vexito



    End Function



End Class
