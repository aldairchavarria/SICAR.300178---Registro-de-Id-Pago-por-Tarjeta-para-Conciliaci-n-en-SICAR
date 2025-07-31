''DIL.C(20160226) :: INI - PROY 21987-IDEA 28251
Imports System.Configuration
Imports System.Net

Public Class BECobertura
    Public strDVPR_DEPAC_CODIGO As String
    Public strDVPR_DEPAC_DESC As String
    Public strDVPR_PROVC_CODIGO As String
    Public strDVPR_PROVV_DESC As String
    Public strDVPR_DISTC_CODIGO As String
    Public strDVPR_DISTV_DESC As String
    Public strDVPR_UBIGEO_INEI As String
    Public strDVPR_CPF_DESC As String
End Class

Public Class BWCobertura
    Public Function ObtenerCobertura(ByVal strUbigeo As String, ByVal strCentroPoblado As String, ByRef strCobertura As String) As String
        Dim strRespuesta As String = String.Empty
        Dim strRespuestaCodigo As String = String.Empty
        Dim strRespuestaMensaje As String = String.Empty

        Try
            Dim objRequest As New WSCobertura.buscarCcppPorUbigeoRequest
            objRequest.ubigeo = strUbigeo

            objRequest.audit = New WSCobertura.auditRequest
            objRequest.audit.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            objRequest.audit.ipAplicacion = Funciones.CheckStr(Dns.GetHostByName(Dns.GetHostName()).AddressList(0))
            objRequest.audit.aplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            objRequest.audit.usrAplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("Usuario_Aplicacion"))

            Dim objCCPP As New WSCobertura.BuscarCcppWSService
            objCCPP.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("constUrlBuscarCcpp"))
            objCCPP.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("TimeOutBuscarCcpp"))

            Dim objResponse As New WSCobertura.buscarCcppPorUbigeoResponse
            objResponse = objCCPP.buscarCcppPorUbigeo(objRequest)

            strRespuestaCodigo = objResponse.audit.codigoRespuesta
            strRespuestaMensaje = objResponse.audit.mensajeRespuesta

            Dim intRespuestaCantidad As Int32 = objResponse.ccppBeanPorUbigeo.Length
            If intRespuestaCantidad > 0 Then
                strCentroPoblado = strCentroPoblado.ToUpper
                For i As Integer = 0 To intRespuestaCantidad - 1
                    Dim strCentroPobladoObtenido As String = objResponse.ccppBeanPorUbigeo(i).ccpp.ToUpper

                    If strCentroPoblado.Equals(strCentroPobladoObtenido) Then
                        strCobertura = objResponse.ccppBeanPorUbigeo(i).cob2g
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            strRespuestaCodigo = "-1"
            strRespuestaMensaje = "ErrorWS[" & ex.Message & "]"
        Finally
            strRespuesta = strRespuestaCodigo & ";" & strRespuestaMensaje
        End Try

        Return strRespuesta
    End Function
End Class
''DIL.C(20160226) :: FIN - PROY 21987-IDEA 28251
