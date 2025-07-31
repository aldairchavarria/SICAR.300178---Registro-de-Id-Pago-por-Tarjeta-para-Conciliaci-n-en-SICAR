Imports System.EnterpriseServices
Imports System.Configuration
Public Class ConsultarClave

    Public Function f_strObtenerClaveWeb(ByVal URL As String, _
    ByVal strIdTransaccion As String, ByVal strIpAplicacion As String, ByVal strIpTransicion As String, _
    ByVal strUsrAplicacion As String, ByVal strIdAplicacion As String, ByVal strCodigoAplicacion As String, _
    ByVal strUsrAplicacionEncriptado As String, ByVal strClaveUsrEncriptado As String, _
    ByRef strMensaje As String, ByRef strUsuarioDesencriptado As String, _
    ByRef strClaveDesencriptado As String) As String
        Dim objServicio As New ConsultaClavesWS.ebsConsultaClavesService
        objServicio.Url = URL
        objServicio.Credentials = System.Net.CredentialCache.DefaultCredentials
        
        Dim strResultado As String = String.Empty
        Try

            strResultado = objServicio.desencriptar(strIdTransaccion, strIpAplicacion, strIpTransicion, strUsrAplicacion, _
            strIdAplicacion, strCodigoAplicacion, strUsrAplicacionEncriptado, strClaveUsrEncriptado, strMensaje, strUsuarioDesencriptado, strClaveDesencriptado)

            If strResultado.Equals("0") Then
                strUsuarioDesencriptado = strUsuarioDesencriptado
                strClaveDesencriptado = strClaveDesencriptado
                strMensaje = strMensaje
            Else
                strMensaje = strMensaje
            End If
        Catch ex As Exception
            strResultado = "999"
            strMensaje = ex.Message
        Finally
            objServicio = Nothing
        End Try

        Return strResultado
    End Function
End Class

