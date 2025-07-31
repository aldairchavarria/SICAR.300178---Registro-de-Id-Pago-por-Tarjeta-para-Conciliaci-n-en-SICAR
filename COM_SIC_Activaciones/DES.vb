'INC000002161718 inicio
Imports System
Imports System.Configuration
Imports System.Collections
Imports System.Text

Public Class DES

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Public strIdentifyLog As String = ""


    Public Function GetEncryptedBase64(ByVal table As Hashtable)

        Dim responseDecrypt As New ArrayList
        responseDecrypt = QueryKeys(table)

        If (Convert.ToString(responseDecrypt(0)) = "0") Then
            Return EncryptedBase64(Convert.ToString(responseDecrypt(2)), Convert.ToString(responseDecrypt(3)))
        Else
            Return Convert.ToString(responseDecrypt(0))
        End If

    End Function

    Private Function EncryptedBase64(ByVal user As String, ByVal key As String)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "User" & user)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Proy140245- EncryptedBase64" & "Key" & key)
        Return Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(String.Format("{0}:{1}", user, key)))

    End Function

    Public Function QueryKeys(ByVal table As Hashtable)

        Dim oConsultaClaves As New ConsultaClavesWS.ebsConsultaClavesService

        Dim strCodigoResultado As String = String.Empty

        Dim listParams As ArrayList

        Try

            oConsultaClaves.Url = ConfigurationSettings.AppSettings("strURLConsultaClavesWS")
            oConsultaClaves.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim strIdTransaccion As String = Convert.ToString(table("idTransaccion"))
            Dim strIpAplicacion As String = Convert.ToString(table("ipAplicacion"))
            Dim strIpTransicion As String = Convert.ToString(table("idTransaccionNegocio"))
            Dim strUsrAplicacion As String = Convert.ToString(table("usuarioAplicacion"))
            Dim strCodigoAplicacion As String = Convert.ToString(table("applicationCode"))
            Dim strIdAplicacion As String = Convert.ToString(table("applicationCodeWS"))
            Dim strUsuarioAplicacionEncriptado As String = Convert.ToString(ConfigurationSettings.AppSettings("strUserEncriptado")) 'usuario
            Dim strClaveEncriptado As String = Convert.ToString(ConfigurationSettings.AppSettings("strPassEncriptado"))
            Dim strMensajeResultado As String = String.Empty
            Dim strUsuarioAplicacionDesencriptado As String = String.Empty
            Dim strClaveDesencriptado As String = String.Empty

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "[INICIO] QueryKeys")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strIdTransaccion : " & strIdTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strIpAplicacion : " & strIpAplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strUsrAplicacion : " & strUsrAplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strCodigoAplicacion : " & strCodigoAplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strIdAplicacion : " & strIdAplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strUsuarioAplicacionEncriptado : " & strUsuarioAplicacionEncriptado)

            strCodigoResultado = oConsultaClaves.desencriptar(strIdTransaccion, strIpAplicacion, strIpTransicion, strUsrAplicacion, strIdAplicacion, strCodigoAplicacion, strUsuarioAplicacionEncriptado, strClaveEncriptado, strMensajeResultado, strUsuarioAplicacionDesencriptado, strClaveDesencriptado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Proy-140245 QueryKeys- strCodigoResultado : " & strCodigoResultado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Proy-140245 QueryKeys - strMensajeResultado : " & strMensajeResultado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Proy-140245 QueryKeys - strUsuarioAplicacionDesencriptado : " & strUsuarioAplicacionDesencriptado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Proy-140245 QueryKeys - strClaveDesencriptado : " & strClaveDesencriptado)

            listParams = New ArrayList
            listParams.Add(strCodigoResultado)
            listParams.Add(strMensajeResultado)
            listParams.Add(strUsuarioAplicacionDesencriptado)
            listParams.Add(strClaveDesencriptado)


        Catch ex As Exception

            Throw ex

        Finally

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " PROY-140245 " & "[FIN] QueryKeys")
        End Try

        Return listParams
    End Function

End Class
'INC000002161718 fin 