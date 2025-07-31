Option Strict Off
Imports System.Web.Mail
Imports System.Web
Imports System.Configuration

Public NotInheritable Class Funciones

    Public Shared Function CheckStr(ByVal value As Object) As String

        Dim salida As String = ""
        If IsNothing(value) Or IsDBNull(value) Then
            salida = ""
        Else
            salida = value.ToString()
        End If

        Return salida.Trim()
    End Function

    Public Shared Function CheckDbl(ByVal value As Object) As Double
        Dim salida As Double = 0
        If IsNothing(value) Or IsDBNull(value) Then
            salida = 0
        Else
            If Convert.ToString(value) = "" Then
                salida = 0
            Else
                salida = Convert.ToDouble(value)
            End If
        End If

        Return salida
    End Function

    Public Shared Function CheckDate(ByVal value As Object) As DateTime
        Dim salida As DateTime
        If IsNothing(value) Or IsDBNull(value) Then
            salida = DateTime.Now
        Else
            salida = Convert.ToDateTime(value)
        End If

        Return salida
    End Function

    Public Shared Function CheckInt64(ByVal value As Object) As Int64
        Dim salida As Int64 = 0
        If IsNothing(value) Or IsDBNull(value) Then
            salida = 0
        Else
            If Convert.ToString(value) = "" Then
                salida = 0
            Else
                salida = Convert.ToInt64(value)
            End If
        End If

        Return salida
    End Function

    Public Shared Function CheckInt(ByVal value As Object) As Int32
        Dim salida As Int64 = 0
        If IsNothing(value) Or IsDBNull(value) Then
            salida = 0
        Else
            If Convert.ToString(value) = "" Then
                salida = 0
            Else
                salida = Convert.ToInt32(value)
            End If
        End If
        Return salida
    End Function


    Public Shared Function EnviarEmail(ByVal vRemitente As String, _
                                      ByVal vPara As String, _
                                      ByVal vCC As String, _
                                      ByVal vBCC As String, _
                                      ByVal vAsunto As String, _
                                      ByVal vMensaje As String, _
                                      ByVal vAdjunto As String) As String
        Dim salida As String = ""
        Dim oMail As New MailMessage
        oMail.From = vRemitente
        oMail.To = vPara
        oMail.Cc = vCC
        oMail.Bcc = vBCC
        oMail.Subject = vAsunto
        oMail.Body = System.Web.HttpContext.Current.Server.HtmlDecode(vMensaje)
        oMail.BodyFormat = MailFormat.Html
        Try
            Dim arrAdjuntos As String() = vAdjunto.Split(Char.Parse("|"))
            For Each sArchivo As String In arrAdjuntos
                If System.IO.File.Exists(sArchivo) Then oMail.Attachments.Add(New MailAttachment(sArchivo))
            Next
            SmtpMail.SmtpServer = ConfigurationSettings.AppSettings("strEmailSmtp").ToString()
            SmtpMail.Send(oMail)
            salida = "OK"
        Catch ex As Exception
            salida = ex.Message
        Finally
            oMail = Nothing
        End Try
        Return salida
    End Function

    Public Shared Function CheckStr2(ByVal value As Object) As String

        Dim salida As String = ""
        If IsNothing(value) Or IsDBNull(value) Then
            salida = ""
        Else
            salida = value.ToString()
        End If

        Return salida
    End Function

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Implementado Por  : Junior Espinoza Lozano - SEL
    ' Fecha Creación    : 30/05/2017
    ' Proposito         : Permite devolver una cadena aleatoria para enviarla como idTransaccion en los WebServices
    ' Output            : strValue
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Shared Function CadenaAleatoria() As String
        Try
            Dim strValue As String = String.Empty
            Dim objAleatorio As Random = New Random
            Try
                Dim i As Integer = 0
                Do While (i < 8)
                    strValue = (strValue + objAleatorio.Next(0, 10).ToString)
                    i = (i + 1)
                Loop
            Catch ex As Exception
                Return ""
            Finally
                objAleatorio = Nothing
            End Try
            Return strValue
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

