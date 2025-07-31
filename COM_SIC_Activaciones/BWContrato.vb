''DIL.C(20160202) :: INI - PROY 21987-IDEA 28251
Imports System.Configuration
Imports System.Net

Public Class BWContrato
    Public Function ContratoAceptado(ByVal strTelefono As String) As String
        Dim strRespuesta As String = String.Empty
        Dim strRespuestaCodigo As String = String.Empty
        Dim strRespuestaMensaje As String = String.Empty
        Dim strIdTransaccion As String = String.Empty

        Try
            strIdTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            Dim strAplicacion As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            Dim strUsrApp As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("Usuario_Aplicacion"))
            Dim strMsisdn As String = Funciones.CheckStr(strTelefono)
            Dim strEstadoContrato As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("AceptarContratoEstado"))
            Dim strRespuestaConfirmacion As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("AceptarContratoRespuesta"))

            Dim objAuditoria As New WSContrato.AuditTypeRequest
            objAuditoria.idTransaccion = strIdTransaccion
            objAuditoria.ipAplicacion = Funciones.CheckStr(Dns.GetHostByName(Dns.GetHostName()).AddressList(0))
            objAuditoria.aplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("constNombreAplicacion"))
            objAuditoria.usrAplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("constUsuarioAplicacionSISACT"))

            Dim objProcesaDOL As New WSContrato.ebsProcesaDOLWS
            objProcesaDOL.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("constUrlProcesaDOL"))
            objProcesaDOL.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("TimeOutProcesaDOL"))

            strRespuestaCodigo = objProcesaDOL.procesarDOL(strIdTransaccion, strAplicacion, strUsrApp, strMsisdn, strEstadoContrato, strRespuestaConfirmacion, strRespuestaMensaje)
        Catch ex As Exception
            strRespuestaCodigo = "-1"
            strRespuestaMensaje = "ErrorWS[" & ex.Message & "]"
            strIdTransaccion = String.Empty
        Finally
            strRespuesta = strRespuestaCodigo & ";" & strRespuestaMensaje & ";" & strIdTransaccion
        End Try

        Return strRespuesta
    End Function
End Class
''DIL.C(20160202) :: FIN - PROY 21987-IDEA 28251
