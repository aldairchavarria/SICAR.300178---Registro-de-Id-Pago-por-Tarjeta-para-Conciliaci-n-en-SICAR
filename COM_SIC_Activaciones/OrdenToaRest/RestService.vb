Imports System
Imports System.Net
Imports System.Collections
Imports System.Reflection
Imports System.Text
Imports System.IO
Imports AjaxPro
Imports System.Configuration

Public Class RestService

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Public strIdentifyLog As String = ""

    Public Function GetHeaders(ByVal table As Hashtable, ByVal userEncriptado As String, ByVal passEncriptado As String, ByVal UseDP As Boolean)
        Dim Headers As New WebHeaderCollection


        Dim oConsultaClaves As New ConsultaClavesWS.ebsConsultaClavesService
        Dim strEncryptedBase64 As String = String.Empty
        Dim strCodigoResultado As String = String.Empty
        Dim listParams As ArrayList

        For Each entry As DictionaryEntry In table
            If entry.Key <> "" And entry.Value <> "" Then
                Headers.Add(entry.Key.ToString(), entry.Value.ToString())
            End If
        Next

        If UseDP Then

            oConsultaClaves.Url = ConfigurationSettings.AppSettings("strURLConsultaClavesWS")
            oConsultaClaves.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim strIdTransaccion As String = Convert.ToString(table("idTransaccion"))
            Dim strIpAplicacion As String = String.Empty
            Dim strIpTransicion As String = String.Empty
            Dim strUsrAplicacion As String = String.Empty
            Dim strCodigoAplicacion As String = ConfigurationSettings.AppSettings("strAplicacionSISACT")
            Dim strIdAplicacion As String = String.Empty
            Dim strUsuarioAplicacionEncriptado As String = userEncriptado
            Dim strClaveEncriptado As String = passEncriptado
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

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Proy-140662 QueryKeys- strCodigoResultado : " & strCodigoResultado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Proy-140662 QueryKeys - strMensajeResultado : " & strMensajeResultado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Proy-140662 QueryKeys - strUsuarioAplicacionDesencriptado : " & strUsuarioAplicacionDesencriptado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Proy-140662 QueryKeys - strClaveDesencriptado : " & strClaveDesencriptado)

            If (strCodigoResultado = "0") Then
                strEncryptedBase64 = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(String.Format("{0}:{1}", strUsuarioAplicacionDesencriptado, strClaveDesencriptado)))
                If (strEncryptedBase64 <> "1" And strEncryptedBase64 <> "-1" And strEncryptedBase64 <> "-2" And strEncryptedBase64 <> "-3") Then
                    Headers.Add("Authorization", "Basic " + strEncryptedBase64)

                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY-140662 ResService" & strEncryptedBase64)
            End If

        Else
            Headers.Add("Authorization", "Basic " + userEncriptado)
        End If


        Return Headers

    End Function

    Public Function GetInvoque(ByVal name As String, ByVal table As Hashtable, ByVal userEncriptado As String, ByVal passEncriptado As String, ByVal strTimeOut As String, ByVal strDatosUrl As String, ByVal objResponse As Type, ByVal UseDP As Boolean)

        Dim strURL As String = String.Empty
        strURL = Funciones.CheckStr(ConfigurationSettings.AppSettings(name))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY - 140662  DLV-F4 - GetInvoque - strURL:" & strURL)

        'cancatenamos los datos para la URL de una variable
        strURL = String.Format("{0}/{1}", strURL, strDatosUrl)
        Dim request As HttpWebRequest = HttpWebRequest.Create(strURL)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY - 140662  DLV-F4 - GetInvoque - strURLFinal:" & strURL)

        'Se arman los datos del header
        request.Method = "GET"
        request.Accept = "application/json"
        request.Headers = GetHeaders(table, userEncriptado, passEncriptado, UseDP)
        request.Timeout = Convert.ToInt32(strTimeOut)
        request.ContentType = "application/json;charset=UTF-8"

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY - 140662  DLV-F4 - GetInvoque - Method:" & request.Method)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY - 140662  DLV-F4 - GetInvoque - Accept:" & request.Accept)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY - 140662  DLV-F4 - GetInvoque - Timeout:" & request.Timeout)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY - 140662  DLV-F4 - GetInvoque - ContentType:" & request.ContentType)

        'Consulta a al servicio rest
        Dim ws As WebResponse
        ws = request.GetResponse()

        Dim stream As Stream
        Dim reader As StreamReader
        stream = ws.GetResponseStream()
        reader = New StreamReader(stream, Encoding.UTF8)
        Dim responseString As String = reader.ReadToEnd()
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - responseString:" & responseString) '//PROY-140379
        Return JavaScriptDeserializer.DeserializeFromJson(responseString, objResponse)

    End Function

End Class
