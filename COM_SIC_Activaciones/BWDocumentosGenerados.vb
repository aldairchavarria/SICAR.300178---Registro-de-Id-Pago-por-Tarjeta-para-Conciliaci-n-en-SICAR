'/*----------------------------------------------------------------------------------------------------------------*/
'/*--EL BUEN YISUS  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
'/*----------------------------------------------------------------------------------------------------------------*/
Imports System.EnterpriseServices
Imports System.Configuration
Imports System.Net
Imports Microsoft
'Imports Microsoft.Web.Services2.Security
'Imports Microsoft.Web.Services2.Security.Tokens
Imports System.IO
Imports System.Xml

Public Class BWDocumentosGenerados
    Public objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

    '/*----------------------------------------------------------------------------------------------------------------*/
    Public Function EjecutarTranssaccionGeneracionDocumento(ByVal strCanal As String, _
                                        ByVal strUserSesion As String, _
                                        ByVal documentos As BEDocumentosGenerados, _
                                        ByVal itemHeaderDataPower As BEHeaderDataPower, _
                                        ByVal user As String, _
                                        ByVal password As String, _
                                        ByVal ipAplicacion As String) As BEResponseDocumentosGenerados

        Dim m_nodelist As String = ""
        Dim xmlCrearRV As String = ""
        Dim response As String = ""
        Dim codigoRespuesta As String = ""
        Dim DescripcionRespuesta As String = ""
        objFileLog.Log_WriteLog(pathFile, strArchivo, ipAplicacion & "- " & "Inicio EjecutarTranssaccionGeneracionDocumento x")

        Dim respuestaBERecargaVirtual As New BEResponseDocumentosGenerados
        Dim m_xmld As XmlDocument
        Dim ns As XmlNamespaceManager
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, ipAplicacion & "- " & "Inicio EjecutarTranssaccionGeneracionDocumento")
            If CrearTransaccionDocumentosGenerados(response, xmlCrearRV, strCanal, strUserSesion, documentos, _
                                    itemHeaderDataPower, user, password, ipAplicacion) Then

                Try
                    m_xmld = New XmlDocument
                    m_xmld.LoadXml(response)

                    ns = New XmlNamespaceManager(m_xmld.NameTable)
                    ns.AddNamespace("v1", "http://claro.com.pe/generic/messageFormat/v1.0/")

                    codigoRespuesta = m_xmld.SelectSingleNode("//Status//code", ns).InnerText
                    DescripcionRespuesta = m_xmld.SelectSingleNode("//Status//message", ns).InnerText

                    respuestaBERecargaVirtual.k_codigo_respuesta = codigoRespuesta
                    respuestaBERecargaVirtual.k_descripcion = DescripcionRespuesta

                    objFileLog.Log_WriteLog(pathFile, strArchivo, ipAplicacion & "- " & "codigoRespuesta : " & codigoRespuesta)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, ipAplicacion & "- " & "DescripcionRespuesta : " & DescripcionRespuesta)


                    respuestaBERecargaVirtual.k_XML_Request = xmlCrearRV
                    respuestaBERecargaVirtual.k_XML_Response = response

                Catch ex As Exception
                    respuestaBERecargaVirtual.k_codigo_respuesta = "-1"
                    respuestaBERecargaVirtual.k_descripcion = "Error en el WS respuestaBERecargaVirtual: " & ex.Message
                    respuestaBERecargaVirtual.k_XML_Request = xmlCrearRV
                    respuestaBERecargaVirtual.k_XML_Response = response
                End Try
            Else
                respuestaBERecargaVirtual.k_codigo_respuesta = "-1"
                respuestaBERecargaVirtual.k_descripcion = "Error con el WS, response: " '& response
                respuestaBERecargaVirtual.k_XML_Request = xmlCrearRV
                respuestaBERecargaVirtual.k_XML_Response = response
            End If
        Catch ex As Exception
            respuestaBERecargaVirtual.k_codigo_respuesta = "-1"
            respuestaBERecargaVirtual.k_descripcion = "Error con el metodo WS. " & ex.Message
        Finally
            m_nodelist = ""
            respuestaBERecargaVirtual.k_XML_Request = xmlCrearRV
            respuestaBERecargaVirtual.k_XML_Response = response
        End Try

        Return respuestaBERecargaVirtual

    End Function
    '/*----------------------------------------------------------------------------------------------------------------*/

    '/*----------------------------------------------------------------------------------------------------------------*/
    Public Function CrearTransaccionDocumentosGenerados(ByRef response As String, _
                                         ByRef xmlCrearRV As String, _
                                         ByVal strCanal As String, _
                                         ByVal strUserSesion As String, _
                                         ByVal documentos As BEDocumentosGenerados, _
                                         ByVal itemHeaderDataPower As BEHeaderDataPower, _
                                         ByVal user As String, _
                                         ByVal password As String, _
                                         ByVal ipAplicacion As String) As Boolean
        Dim fileReader As String
        Dim url As String = ConfigurationSettings.AppSettings("UrlDocumentosGenerados")
        Dim request As HttpWebRequest
        Try



            objFileLog.Log_WriteLog(pathFile, strArchivo, ipAplicacion & "- " & "Inicio CrearTransaccionDocumentosGenerados")

            fileReader = System.IO.File.OpenText(ConfigurationSettings.AppSettings("strRutaXMLDocumentosGenerados") & "DocumentosGenerados.xml").ReadToEnd

            Dim objRequestDocumentosGenerados As XmlDocument

            objRequestDocumentosGenerados = New XmlDocument
            objRequestDocumentosGenerados.LoadXml(fileReader)
            objFileLog.Log_WriteLog(pathFile, strArchivo, ipAplicacion & "- " & "Inicio objRequestDocumentosGenerados")

            Dim wsse = New XmlNamespaceManager(objRequestDocumentosGenerados.NameTable)
            wsse.AddNamespace("wsse", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")
            objRequestDocumentosGenerados.SelectSingleNode("//wsse:Username", wsse).InnerText = user
            objRequestDocumentosGenerados.SelectSingleNode("//wsse:Password", wsse).InnerText = password

            objFileLog.Log_WriteLog(pathFile, strArchivo, ipAplicacion & "- " & "Inicio wsse")

            objRequestDocumentosGenerados.SelectSingleNode("//country").InnerText = itemHeaderDataPower.country
            objRequestDocumentosGenerados.SelectSingleNode("//language").InnerText = itemHeaderDataPower.language
            objRequestDocumentosGenerados.SelectSingleNode("//consumer").InnerText = itemHeaderDataPower.consumer
            objRequestDocumentosGenerados.SelectSingleNode("//system").InnerText = itemHeaderDataPower.system
            objRequestDocumentosGenerados.SelectSingleNode("//modulo").InnerText = itemHeaderDataPower.modulo
            objRequestDocumentosGenerados.SelectSingleNode("//pid").InnerText = itemHeaderDataPower.pid
            objRequestDocumentosGenerados.SelectSingleNode("//userId").InnerText = itemHeaderDataPower.userId
            objRequestDocumentosGenerados.SelectSingleNode("//dispositivo").InnerText = itemHeaderDataPower.dispositivo
            objRequestDocumentosGenerados.SelectSingleNode("//wsIp").InnerText = itemHeaderDataPower.wsIp
            objRequestDocumentosGenerados.SelectSingleNode("//operation").InnerText = itemHeaderDataPower.operation
            objRequestDocumentosGenerados.SelectSingleNode("//timestamp").InnerText = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffzzz")
            objRequestDocumentosGenerados.SelectSingleNode("//msgType").InnerText = itemHeaderDataPower.msgType


            Dim bas = New XmlNamespaceManager(objRequestDocumentosGenerados.NameTable)
            bas.AddNamespace("bas", "http://claro.com.pe/eai/ws/baseschema")
            objFileLog.Log_WriteLog(pathFile, strArchivo, ipAplicacion & "- " & "Inicio bas")

            objRequestDocumentosGenerados.SelectSingleNode("//bas:idTransaccion", bas).InnerText = documentos.idTransaccion.ToString()

            objRequestDocumentosGenerados.SelectSingleNode("//bas:ipAplicacion", bas).InnerText = documentos.ipAplicacion.ToString()
            objRequestDocumentosGenerados.SelectSingleNode("//bas:nombreAplicacion", bas).InnerText = documentos.nombreAplicacion.ToString()
            objRequestDocumentosGenerados.SelectSingleNode("//bas:usuarioAplicacion", bas).InnerText = documentos.usuarioAplicacion.ToString()

            Dim sch As New XmlNamespaceManager(objRequestDocumentosGenerados.NameTable)
            sch.AddNamespace("typ", "http://claro.com.pe/eai/ws/ventas/documentosgeneradosws/types")
            objFileLog.Log_WriteLog(pathFile, strArchivo, ipAplicacion & "- " & "Inicio sch")

            objRequestDocumentosGenerados.SelectSingleNode("//typ:nroPedido", sch).InnerText = documentos.nroPedido.ToString()
            objRequestDocumentosGenerados.SelectSingleNode("//typ:idVenta", sch).InnerText = documentos.idVenta.ToString()
            objRequestDocumentosGenerados.SelectSingleNode("//typ:nroContrato", sch).InnerText = documentos.nroContrato.ToString()
            objRequestDocumentosGenerados.SelectSingleNode("//typ:nroDocumento", sch).InnerText = documentos.nroDocumento.ToString()
            objRequestDocumentosGenerados.SelectSingleNode("//typ:tipoDocumento", sch).InnerText = documentos.tipoDocumento.ToString()
            objRequestDocumentosGenerados.SelectSingleNode("//typ:correo", sch).InnerText = documentos.correo.ToString()
            objRequestDocumentosGenerados.SelectSingleNode("//typ:tipoOperacion", sch).InnerText = documentos.tipoOperacion.ToString()

            objFileLog.Log_WriteLog(pathFile, strArchivo, ipAplicacion & "- " & "Inicio fileReader")

            fileReader = objRequestDocumentosGenerados.InnerXml

            xmlCrearRV = fileReader 'devuelve el xml creado

            request = WebRequest.Create(url)
            request.Method = "POST"
            request.Accept = "application/json"
            request.Credentials = System.Net.CredentialCache.DefaultCredentials
            request.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("WS_DocumentosGenerados_timeout"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, ipAplicacion & "- " & "Inicio UTF8Encoding")

            Dim encoding As System.Text.UTF8Encoding = New System.Text.UTF8Encoding
            Dim byteArray As Byte() = encoding.GetBytes(fileReader)
            request.ContentType = "text/xml"
            request.ContentLength = byteArray.Length

            objFileLog.Log_WriteLog(pathFile, strArchivo, ipAplicacion & "- " & "Inicio dataStream")


            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()

            objFileLog.Log_WriteLog(pathFile, strArchivo, ipAplicacion & "- " & "Inicio httpResponse")
            Dim httpResponse As HttpWebResponse = CType(request.GetResponse(), WebResponse)
            Dim StreamReader As StreamReader
            objFileLog.Log_WriteLog(pathFile, strArchivo, ipAplicacion & "- " & "Inicio StreamReader")
            StreamReader = New StreamReader(httpResponse.GetResponseStream())
            response = StreamReader.ReadToEnd().ToString

            Return True
        Catch ex As Exception
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Activaciones\BWDocumentosGenerados.vb; Function: CrearTransacionDocuemntosGenerados)"
            response = "Error: " & ex.Message & MaptPath
            'FIN 'PROY-140126

            xmlCrearRV = "Error : " & fileReader 'devuelve el xml creado
            Return False
        End Try
    End Function

End Class

'/*----------------------------------------------------------------------------------------------------------------*/
'/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
'/*----------------------------------------------------------------------------------------------------------------*/