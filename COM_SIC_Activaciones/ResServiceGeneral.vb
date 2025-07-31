Imports System
Imports System.Net
Imports System.Collections
Imports System.Reflection
Imports System.Text
Imports System.IO
Imports AjaxPro
Imports System.Configuration

Public Class ResServiceGeneral

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Public strIdentifyLog As String = ""

    'INI: INICIATIVA-219
    Public Function GetHeaders(ByVal table As Hashtable, ByVal blDataPower As Boolean)
        Dim Headers As New WebHeaderCollection

        For Each entry As DictionaryEntry In table
            If entry.Key <> "" And entry.Value <> "" Then
                Headers.Add(entry.Key.ToString(), entry.Value.ToString())
            End If
        Next

        Dim strEncryptedBase64 As String
        Dim Des As New DES

        If blDataPower Then
            strEncryptedBase64 = Des.GetEncryptedBase64(table)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "ResServiceGeneral" & strEncryptedBase64)

            If (strEncryptedBase64 <> "1" And strEncryptedBase64 <> "-1" And strEncryptedBase64 <> "-2" And strEncryptedBase64 <> "-3") Then
                Headers.Add("Authorization", "Basic " + strEncryptedBase64)

            End If
        End If
        Return Headers

    End Function


    Public Function PostInvoque(ByVal name As String, ByVal table As Hashtable, ByVal objRequest As Object, ByVal objResponse As Type, ByVal blDataPower As Boolean)

        Dim request As HttpWebRequest = HttpWebRequest.Create(ConfigurationSettings.AppSettings(name))
        request.Method = "POST"
        request.Headers = GetHeaders(table, blDataPower)
        request.Accept = "application/json"
        request.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("TimeOutWService"))
        request.ContentType = "application/json"
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
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - responseString:" & responseString) 'INICIATIVA-219
        Return JavaScriptDeserializer.DeserializeFromJson(responseString, objResponse)

    End Function
	
    Public Function PostInvoque2(ByVal name As String, ByVal table As Hashtable, ByVal objRequest As Object, ByVal objResponse As Type, ByVal blDataPower As Boolean)

        Dim request As HttpWebRequest = HttpWebRequest.Create(ConfigurationSettings.AppSettings(name))
        request.Method = "POST"
        request.Headers = GetHeaders(table, blDataPower)
        request.Accept = "application/json"
        request.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("TimeOutWService"))
        request.ContentType = "application/json"
        Dim data As String = JavaScriptSerializer.Serialize(objRequest)
        data = data.Replace("\\", "\")
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
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & "PostInvoque - responseString:" & responseString) 'INICIATIVA-219
        Return JavaScriptDeserializer.DeserializeFromJson(responseString, objResponse)

    End Function	
    'FIN: INICIATIVA-219
End Class