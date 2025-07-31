Imports System.Configuration
Public Class clsBonoRenovaPrepago
    Public Function AsignarBonoPrepago(ByVal vId As String, ByVal vMsisdn As String, _
                                       ByVal vImei As String, ByVal vArticulo As String, ByVal montoVenta As String, ByVal montoVentaSinIgv As String, ByVal origenProceso As String, ByRef vRespuesta As String) As String
        Dim strResultado As String
        Try
            Dim nombreServer As String = System.Net.Dns.GetHostName()
            Dim vIpAplicacion As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString()
            Dim vAplicacion As String = ConfigurationSettings.AppSettings("constAplicacion")

            Dim objbonoWS As New BonoRenovacionReposicion.BonoRenovacionReposicionWS
            objbonoWS.Url = Configuration.ConfigurationSettings.AppSettings("ConstUrlBonoPrepago")
            objbonoWS.Credentials = System.Net.CredentialCache.DefaultCredentials
            objbonoWS.Timeout = Configuration.ConfigurationSettings.AppSettings("ConstTimeOutBonoPrepago")

            'Método Registro Bono
            strResultado = objbonoWS.asignaBono(vId, vIpAplicacion, vAplicacion, vMsisdn, vImei, vArticulo, montoVenta, montoVentaSinIgv, origenProceso, vRespuesta)
        Catch ex As Exception
            strResultado = 9
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Activaciones\clsBonoRenovaPrepago.vb; Function: AsignarBonoPrepago)"
            vRespuesta = String.Format("Exception. {0}|{1}|{2}", strResultado, vRespuesta, ex.Message & MaptPath)
            'FIN PROY-140126 
        End Try
        Return strResultado
    End Function

    Public Function AnularBonoPrepago(ByVal vId As String, ByVal vMsisdn As String, _
                                       ByVal vImei As String, ByRef vRespuesta As String) As String
        Dim strResultado As String
        Try
            Dim nombreServer As String = System.Net.Dns.GetHostName()
            Dim vIpAplicacion As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString()
            Dim vAplicacion As String = ConfigurationSettings.AppSettings("constAplicacion")

            Dim objbonoWS As New BonoRenovacionReposicion.BonoRenovacionReposicionWS
            objbonoWS.Url = Configuration.ConfigurationSettings.AppSettings("ConstUrlBonoPrepago")
            objbonoWS.Credentials = System.Net.CredentialCache.DefaultCredentials
            objbonoWS.Timeout = Configuration.ConfigurationSettings.AppSettings("ConstTimeOutBonoPrepago")

            'Método Anula Bono
            strResultado = objbonoWS.anulaBono(vId, vIpAplicacion, vAplicacion, vMsisdn, vImei, vRespuesta)
        Catch ex As Exception
            strResultado = 9
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Activaciones\clsBonoRenovaPrepago.vb; Function: AnularBonoPrepago)"
            vRespuesta = String.Format("Exception. {0}|{1}|{2}", strResultado, vRespuesta, ex.Message & MaptPath)
            'FIN PROY-140126
        End Try
        Return strResultado
    End Function

End Class
