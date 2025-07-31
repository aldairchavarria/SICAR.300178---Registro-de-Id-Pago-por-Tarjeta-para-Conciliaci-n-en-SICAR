'INC000002161718 inicio
Imports System
Imports System.Net
Imports System.Collections
Imports System.Reflection
Imports System.Text
Imports System.IO
Imports AjaxPro
Imports System.Configuration

Public Class ResService

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Public strIdentifyLog As String = ""

    Public Function GetHeaders(ByVal table As Hashtable)
        Dim Headers As New WebHeaderCollection

        For Each entry As DictionaryEntry In table
            If entry.Key <> "" And entry.Value <> "" Then
                Headers.Add(entry.Key.ToString(), entry.Value.ToString())
            End If
        Next

        Dim strEncryptedBase64 As String
        Dim Des As New DES

        strEncryptedBase64 = Des.GetEncryptedBase64(table)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY-140245 ResService" & strEncryptedBase64)

        If (strEncryptedBase64 <> "1" And strEncryptedBase64 <> "-1" And strEncryptedBase64 <> "-2" And strEncryptedBase64 <> "-3") Then
            Headers.Add("Authorization", "Basic " + strEncryptedBase64)

        End If
        Return Headers

    End Function


    Public Function PostInvoque(ByVal name As String, ByVal table As Hashtable, ByVal objRequest As Object, ByVal objResponse As Type)

        Dim request As HttpWebRequest = HttpWebRequest.Create(ConfigurationSettings.AppSettings(name))
        request.Method = "POST"
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - request.Method:" & request.Method)

        request.Headers = GetHeaders(table)
        request.Accept = "application/json"
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - request.Accept:" & request.Accept)

        request.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("strRelationPlanTimeout"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - request.Timeout:" & request.Timeout)

        request.ContentType = "application/json;charset=UTF-8" '//PROY-140379
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - request.ContentType:" & request.ContentType)

        Dim data As String = JavaScriptSerializer.Serialize(objRequest)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - data:" & data)
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(data)
        request.ContentLength = byteArray.Length
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(byteArray, 0, byteArray.Length)
        dataStream.Close()
        Dim ws As WebResponse
        ws = request.GetResponse()

        Dim stream As Stream
        Dim reader As StreamReader
        stream = ws.GetResponseStream()
        reader = New StreamReader(stream, Encoding.UTF8)
        Dim responseString As String = reader.ReadToEnd()
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - responseString:" & responseString)'//PROY-140379
        Return JavaScriptDeserializer.DeserializeFromJson(responseString, objResponse)

    End Function

    Public Function PostInvoque2(ByVal name As String, ByVal table As Hashtable, ByVal objRequest As Object, ByVal userEncriptado As String, ByVal passEncriptado As String, ByVal strNombreTimeOut As String, ByVal objResponse As Type)

        Dim request As HttpWebRequest = HttpWebRequest.Create(ConfigurationSettings.AppSettings(name))
        request.Method = "POST"
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - request.Method:" & request.Method)

        request.Headers = GetHeaders2(table, userEncriptado, passEncriptado)
        request.Accept = "application/json"
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - request.Accept:" & request.Accept)

        request.Timeout = Convert.ToInt32(strNombreTimeOut)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - request.Timeout:" & request.Timeout)

        request.ContentType = "application/json;charset=UTF-8" '//PROY-140379
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - request.ContentType:" & request.ContentType)

        Dim data As String = JavaScriptSerializer.Serialize(objRequest)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - data:" & data)
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(data)
        request.ContentLength = byteArray.Length
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(byteArray, 0, byteArray.Length)
        dataStream.Close()
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

    Public Function GetHeaders2(ByVal table As Hashtable, ByVal userEncriptado As String, ByVal passEncriptado As String)
        Dim Headers As New WebHeaderCollection

    
        Dim oConsultaClaves As New ConsultaClavesWS.ebsConsultaClavesService
        Dim strEncryptedBase64 As String
        Dim strCodigoResultado As String = String.Empty
        Dim listParams As ArrayList

        For Each entry As DictionaryEntry In table
            If entry.Key <> "" And entry.Value <> "" Then
                Headers.Add(entry.Key.ToString(), entry.Value.ToString())
            End If
        Next

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
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Proy-140623 QueryKeys- strCodigoResultado : " & strCodigoResultado)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Proy-140623 QueryKeys - strMensajeResultado : " & strMensajeResultado)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Proy-140623 QueryKeys - strUsuarioAplicacionDesencriptado : " & strUsuarioAplicacionDesencriptado)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Proy-140623 QueryKeys - strClaveDesencriptado : " & strClaveDesencriptado)

        If (strCodigoResultado = "0") Then
            strEncryptedBase64 = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(String.Format("{0}:{1}", strUsuarioAplicacionDesencriptado, strClaveDesencriptado)))
            If (strEncryptedBase64 <> "1" And strEncryptedBase64 <> "-1" And strEncryptedBase64 <> "-2" And strEncryptedBase64 <> "-3") Then
                Headers.Add("Authorization", "Basic " + strEncryptedBase64)

            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PROY-140623 ResService" & strEncryptedBase64)
        End If

        Return Headers

    End Function

    'PROY-140715  - IDEA 140805 | No biometría en SISACT en caída RENIEC | INICIO
    Public Function GetInvoque(ByVal name As String, ByVal nameTimeOut As String, ByVal table As Hashtable, ByVal objResponse As Type, ByVal tbParametros As Hashtable, ByVal userEncriptado As String, ByVal passEncriptado As String)

        Dim request As HttpWebRequest = HttpWebRequest.Create(getUri(name, tbParametros))
        request.Method = "GET"
        request.Headers = GetHeaders2(table, userEncriptado, passEncriptado)
        request.Accept = "application/json"
        request.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings(nameTimeOut))
        request.ContentType = "application/json"
        Dim ws As WebResponse

        ws = request.GetResponse()

        Dim stream As Stream
        Dim reader As StreamReader
        stream = ws.GetResponseStream()
        reader = New StreamReader(stream, Encoding.UTF8)
        Dim responseString As String = reader.ReadToEnd()
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - responseString:" & responseString)
        Return JavaScriptDeserializer.DeserializeFromJson(responseString, objResponse)

    End Function

    Public Function getUri(ByVal nombreKeyUrl As String, ByVal tbParametros As Hashtable) As String
        Dim strUrl As String = ConfigurationSettings.AppSettings(nombreKeyUrl)
        Dim strRetornoUri As String = strUrl
        Dim blFirstParam As Boolean = True

        For Each entry As DictionaryEntry In tbParametros
            If entry.Key <> "" And entry.Value <> "" Then
                strRetornoUri = String.Format("{0}{1}", strRetornoUri, IIf(blFirstParam, "?", "&"))
                strRetornoUri = String.Format("{0}{1}{2}{3}", strRetornoUri, entry.Key, "=", entry.Value)
                blFirstParam = False
            End If
        Next

        Return strRetornoUri
    End Function


    'PROY-140715  - IDEA 140805 | FIN
End Class
'INC000002161718 fin 