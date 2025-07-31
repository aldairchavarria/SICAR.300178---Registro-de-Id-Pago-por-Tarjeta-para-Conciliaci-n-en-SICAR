'/*----------------------------------------------------------------------------------------------------------------*/
'/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
'/*----------------------------------------------------------------------------------------------------------------*/
Imports System.EnterpriseServices
Imports System.Configuration
Imports System.Net
Imports System.IO
Imports System.Xml

Public Class clsBSS_RVirtual
    '/*----------------------------------------------------------------------------------------------------------------*/
    Public Function CrearRecargaVirtual(ByVal strCanal As String, _
                                        ByVal strUserSesion As String, _
                                        ByVal itemRecarga As BERecargaVirtual, _
                                        ByVal itemHeaderDataPower As BEHeaderDataPower, _
                                        ByVal user As String, _
                                        ByVal password As String, _
                                        ByVal ipAplicacion As String) As BEResponseRecargaVirtual

        Dim m_nodelist As String = ""
        Dim xmlCrearRV As String = ""
        Dim response As String = ""
        Dim respuestaBERecargaVirtual As New COM_SIC_RVirtual.BEResponseRecargaVirtual

        Try

            If CrearRecargaResponse(response, xmlCrearRV, strCanal, strUserSesion, itemRecarga, _
                                    itemHeaderDataPower, user, password, ipAplicacion) Then
                Dim m_xmld As XmlDocument = New XmlDocument
                Try
                    m_xmld.LoadXml(response)

                    Dim ns = New XmlNamespaceManager(m_xmld.NameTable)
                    ns.AddNamespace("ns1", "http://claro.com.pe/esb/message/BSS_RecargaVirtual_v1/crearRecarga/v1/")

                    respuestaBERecargaVirtual.K_estado = m_xmld.SelectSingleNode("//Status//message").InnerText
                    respuestaBERecargaVirtual.k_codigo_respuesta = m_xmld.SelectSingleNode("//ns1:codigoRespuesta", ns).InnerText
                    respuestaBERecargaVirtual.k_descripcion = m_xmld.SelectSingleNode("//ns1:descripcionRespuesta", ns).InnerText
                    respuestaBERecargaVirtual.k_ubicacionError = ""
                    respuestaBERecargaVirtual.k_fecha = ""
                    respuestaBERecargaVirtual.k_origen = ""
                    respuestaBERecargaVirtual.k_XML_Request = xmlCrearRV
                    respuestaBERecargaVirtual.k_XML_Response = response

                Catch ex As Exception
                    m_nodelist = m_xmld.SelectSingleNode("//detail").InnerText
                    respuestaBERecargaVirtual.k_origen = m_nodelist
                    'respuestaBERecargaVirtual.k_codigo_respuesta = "-1"
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
    Public Function ActualizarRecargaVirtual(ByVal strCanal As String, _
                                             ByVal strUserSesion As String, _
                                             ByVal itemRecarga As BERecargaVirtual, _
                                             ByVal itemHeaderDataPower As BEHeaderDataPower, _
                                             ByVal user As String, _
                                             ByVal password As String, _
                                             ByVal ipAplicacion As String) As BEResponseRecargaVirtual

        Dim respuestaBERecargaVirtual As New COM_SIC_RVirtual.BEResponseRecargaVirtual
        Dim m_nodelist As String = ""
        Dim xmlCrearRV As String = ""
        Dim response As String = ""
        Dim objDocXMLModificarRecargaRequest As XmlDocument
        Dim objNamespaceNS1 As XmlNamespaceManager

        Try
            'Llamamos al metodo que invoca el OSB Data Power ModificarRecarga
            If ActualizarRecargaResponse(response, xmlCrearRV, strCanal, strUserSesion, itemRecarga, itemHeaderDataPower, _
                                    user, password, ipAplicacion) Then

                objDocXMLModificarRecargaRequest = New XmlDocument
                objDocXMLModificarRecargaRequest.LoadXml(response)

                objNamespaceNS1 = New XmlNamespaceManager(objDocXMLModificarRecargaRequest.NameTable)
                objNamespaceNS1.AddNamespace("ns1", "http://claro.com.pe/esb/message/BSS_RecargaVirtual_v1/actualizarRecarga/v1/")

                Try
                    'seteamos las respuestas del Data Power
                    respuestaBERecargaVirtual.K_estado = objDocXMLModificarRecargaRequest.SelectSingleNode("//Status//message").InnerText
                    respuestaBERecargaVirtual.k_codigo_respuesta = objDocXMLModificarRecargaRequest.SelectSingleNode("//ns1:codigoRespuesta", objNamespaceNS1).InnerText
                    respuestaBERecargaVirtual.k_descripcion = objDocXMLModificarRecargaRequest.SelectSingleNode("//ns1:descripcionRespuesta", objNamespaceNS1).InnerText
                    respuestaBERecargaVirtual.k_ubicacionError = ""
                    respuestaBERecargaVirtual.k_fecha = ""
                    respuestaBERecargaVirtual.k_origen = ""
                    respuestaBERecargaVirtual.k_XML_Request = xmlCrearRV
                    respuestaBERecargaVirtual.k_XML_Response = response
                Catch ex As Exception
                    m_nodelist = objDocXMLModificarRecargaRequest.SelectSingleNode("//detail").InnerText
                    respuestaBERecargaVirtual.k_codigo_respuesta = "-1"
                    respuestaBERecargaVirtual.k_descripcion = "Error en el WS. " & ex.Message
                End Try
            Else
                respuestaBERecargaVirtual.k_codigo_respuesta = "-1"
                respuestaBERecargaVirtual.k_descripcion = "Error con el WS. " & response.ToString
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
    Public Function RevertirRecargaVirtual(ByVal strCanal As String, _
                                           ByVal strUserSesion As String, _
                                           ByVal itemRecarga As BERecargaVirtual, _
                                           ByVal itemHeaderDataPower As BEHeaderDataPower, _
                                           ByVal user As String, _
                                           ByVal password As String, _
                                           ByVal ipAplicacion As String) As BEResponseRecargaVirtual

        Dim respuestaBERecargaVirtual As New COM_SIC_RVirtual.BEResponseRecargaVirtual
        Dim m_nodelist As String = ""
        Dim xmlCrearRV As String = ""
        Dim response As String = ""
        Dim objDocXMLActualizarRecargaRequest As XmlDocument
        Dim objNamespaceNS1 As XmlNamespaceManager

        Try
            'Llamamos al metodo que invoca el OSB Data Power ModificarRecarga
            If RevertirRecargaResponse(response, xmlCrearRV, strCanal, strUserSesion, itemRecarga, itemHeaderDataPower, _
                                       user, password, ipAplicacion) Then

                objDocXMLActualizarRecargaRequest = New XmlDocument
                objDocXMLActualizarRecargaRequest.LoadXml(response)

                objNamespaceNS1 = New XmlNamespaceManager(objDocXMLActualizarRecargaRequest.NameTable)
                objNamespaceNS1.AddNamespace("ns1", "http://claro.com.pe/esb/message/BSS_RecargaVirtual_v1/revertirRecargaVirtual/v1/")

                Try
                    'seteamos las respuestas del Data Power
                    respuestaBERecargaVirtual.K_estado = objDocXMLActualizarRecargaRequest.SelectSingleNode("//Status//message").InnerText
                    respuestaBERecargaVirtual.k_codigo_respuesta = objDocXMLActualizarRecargaRequest.SelectSingleNode("//ns1:codigoRespuesta", objNamespaceNS1).InnerText
                    respuestaBERecargaVirtual.k_descripcion = objDocXMLActualizarRecargaRequest.SelectSingleNode("//ns1:descripcionRespuesta", objNamespaceNS1).InnerText
                    respuestaBERecargaVirtual.k_ubicacionError = ""
                    respuestaBERecargaVirtual.k_fecha = ""
                    respuestaBERecargaVirtual.k_origen = ""
                    respuestaBERecargaVirtual.k_XML_Request = xmlCrearRV
                    respuestaBERecargaVirtual.k_XML_Response = response
                Catch ex As Exception
                    m_nodelist = objDocXMLActualizarRecargaRequest.SelectSingleNode("//detail").InnerText
                    respuestaBERecargaVirtual.k_codigo_respuesta = "-1"
                    respuestaBERecargaVirtual.k_descripcion = "Error en el WS. " & ex.Message
                End Try
            Else
                respuestaBERecargaVirtual.k_codigo_respuesta = "-1"
                respuestaBERecargaVirtual.k_descripcion = "Error con el WS. response: " & response
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
    Public Function CrearRecargaResponse(ByRef response As String, _
                                         ByRef xmlCrearRV As String, _
                                         ByVal strCanal As String, _
                                         ByVal strUserSesion As String, _
                                         ByVal itemRecarga As BERecargaVirtual, _
                                         ByVal itemHeaderDataPower As BEHeaderDataPower, _
                                         ByVal user As String, _
                                         ByVal password As String, _
                                         ByVal ipAplicacion As String) As Boolean
        Dim fileReader As String = ""
        Try
            Dim url As String = ConfigurationSettings.AppSettings("constRutaCrearRecargaVirtual")
            Dim request As HttpWebRequest

            fileReader = System.IO.File.OpenText(ConfigurationSettings.AppSettings("strRutaSiteApp") & "CrearRecargaVirtualRequest.xml").ReadToEnd

            Dim objRequestCrearRecarga As XmlDocument

            objRequestCrearRecarga = New XmlDocument
            objRequestCrearRecarga.LoadXml(fileReader)
       
            Dim wsse = New XmlNamespaceManager(objRequestCrearRecarga.NameTable)
            wsse.AddNamespace("wsse", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")
            objRequestCrearRecarga.SelectSingleNode("//wsse:Username", wsse).InnerText = user
            objRequestCrearRecarga.SelectSingleNode("//wsse:Password", wsse).InnerText = password

            'Dim v11 = New XmlNamespaceManager(objRequestCrearRecarga.NameTable)
            'v11.AddNamespace("v11", "http://claro.com.pe/generic/messageFormat/v1.0/")
            objRequestCrearRecarga.SelectSingleNode("//country").InnerText = itemHeaderDataPower.country
            objRequestCrearRecarga.SelectSingleNode("//language").InnerText = itemHeaderDataPower.language
            objRequestCrearRecarga.SelectSingleNode("//consumer").InnerText = itemHeaderDataPower.consumer
            objRequestCrearRecarga.SelectSingleNode("//system").InnerText = itemHeaderDataPower._system
            objRequestCrearRecarga.SelectSingleNode("//modulo").InnerText = itemHeaderDataPower.modulo
            objRequestCrearRecarga.SelectSingleNode("//pid").InnerText = itemHeaderDataPower.pid
            objRequestCrearRecarga.SelectSingleNode("//userId").InnerText = itemHeaderDataPower.userId
            objRequestCrearRecarga.SelectSingleNode("//dispositivo").InnerText = itemHeaderDataPower.dispositivo
            objRequestCrearRecarga.SelectSingleNode("//wsIp").InnerText = itemHeaderDataPower.wsIp
            objRequestCrearRecarga.SelectSingleNode("//operation").InnerText = itemHeaderDataPower.operation
            objRequestCrearRecarga.SelectSingleNode("//timestamp").InnerText = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffzzz")
            objRequestCrearRecarga.SelectSingleNode("//msgType").InnerText = itemHeaderDataPower.msgType

            Dim v1 = New XmlNamespaceManager(objRequestCrearRecarga.NameTable)
            v1.AddNamespace("v1", "http://claro.com.pe/esb/data/generico/claroGenericHeader/v1/")
            objRequestCrearRecarga.SelectSingleNode("//v1:canal", v1).InnerText = strCanal
            objRequestCrearRecarga.SelectSingleNode("//v1:idAplicacion", v1).InnerText = ConfigurationSettings.AppSettings("CodAplicacion")
            objRequestCrearRecarga.SelectSingleNode("//v1:usuarioAplicacion", v1).InnerText = ConfigurationSettings.AppSettings("Usuario_Aplicacion")
            objRequestCrearRecarga.SelectSingleNode("//v1:usuarioSesion", v1).InnerText = strUserSesion
            objRequestCrearRecarga.SelectSingleNode("//v1:idTransaccionESB", v1).InnerText = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            objRequestCrearRecarga.SelectSingleNode("//v1:idTransaccionNegocio", v1).InnerText = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            objRequestCrearRecarga.SelectSingleNode("//v1:fechaInicio", v1).InnerText = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffzzz")
            objRequestCrearRecarga.SelectSingleNode("//v1:nodoAdicional", v1).InnerText = ipAplicacion

            Dim v12 = New XmlNamespaceManager(objRequestCrearRecarga.NameTable)
            v12.AddNamespace("v12", "http://claro.com.pe/esb/message/BSS_RecargaVirtual_v1/crearRecarga/v1/")
            objRequestCrearRecarga.SelectSingleNode("//v12:linea", v12).InnerText = itemRecarga.linea
            objRequestCrearRecarga.SelectSingleNode("//v12:estado", v12).InnerText = itemRecarga.estado
            objRequestCrearRecarga.SelectSingleNode("//v12:fecha", v12).InnerText = itemRecarga.fecha
            objRequestCrearRecarga.SelectSingleNode("//v12:nombreUsuario", v12).InnerText = itemRecarga.nombreUsuario
            objRequestCrearRecarga.SelectSingleNode("//v12:puntoVenta", v12).InnerText = itemRecarga.puntoVenta
            objRequestCrearRecarga.SelectSingleNode("//v12:tipoDocumento", v12).InnerText = itemRecarga.tipoDocumento
            objRequestCrearRecarga.SelectSingleNode("//v12:numeroDocumento", v12).InnerText = itemRecarga.numeroDocumento
            objRequestCrearRecarga.SelectSingleNode("//v12:lineaCliente", v12).InnerText = itemRecarga.lineaCliente
            objRequestCrearRecarga.SelectSingleNode("//v12:montoRecarga", v12).InnerText = itemRecarga.montoRecarga
            objRequestCrearRecarga.SelectSingleNode("//v12:fechaSwTrx", v12).InnerText = itemRecarga.fechaSwTrx
            objRequestCrearRecarga.SelectSingleNode("//v12:valorVenta", v12).InnerText = itemRecarga.valorVenta
            objRequestCrearRecarga.SelectSingleNode("//v12:valorDescuento", v12).InnerText = itemRecarga.valorDescuento
            objRequestCrearRecarga.SelectSingleNode("//v12:valorSubTotal", v12).InnerText = itemRecarga.valorSubTotal
            objRequestCrearRecarga.SelectSingleNode("//v12:valorIGV", v12).InnerText = itemRecarga.valorIGV
            objRequestCrearRecarga.SelectSingleNode("//v12:valorTotal", v12).InnerText = itemRecarga.valorTotal
            objRequestCrearRecarga.SelectSingleNode("//v12:estadoInsertar", v12).InnerText = itemRecarga.estadoInsertar
            objRequestCrearRecarga.SelectSingleNode("//v12:trace", v12).InnerText = "" 'itemRecarga.trace

            fileReader = objRequestCrearRecarga.InnerXml

            xmlCrearRV = fileReader 'devuelve el xml creado

            request = WebRequest.Create(url)
            request.Method = "POST"
            request.Accept = "application/json"
            request.Credentials = System.Net.CredentialCache.DefaultCredentials
            request.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("intTimeOutBSS_RecargaVirtual"))

            Dim encoding As System.Text.UTF8Encoding = New System.Text.UTF8Encoding
            Dim byteArray As Byte() = encoding.GetBytes(fileReader)
            request.ContentType = "text/xml"
            request.ContentLength = byteArray.Length

            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()

            Dim httpResponse As HttpWebResponse = CType(request.GetResponse(), WebResponse)
            Dim StreamReader As StreamReader

            StreamReader = New StreamReader(httpResponse.GetResponseStream())
            response = StreamReader.ReadToEnd().ToString

            Return True
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_RVirtual\clsBSS_RVirtual.vb; Function: CrearRecargaResponse)"
            response = "Error: " & ex.Message & MaptPath
            'FIN PROY-140126

            xmlCrearRV = "Error : " & fileReader 'devuelve el xml creado
            Return False
        End Try
    End Function
    '/*----------------------------------------------------------------------------------------------------------------*/
    Public Function ActualizarRecargaResponse(ByRef response As String, _
                                              ByRef xmlCrearRV As String, _
                                              ByVal strCanal As String, _
                                              ByVal strUserSesion As String, _
                                              ByVal itemRecarga As BERecargaVirtual, _
                                              ByVal itemHeaderDataPower As BEHeaderDataPower, _
                                              ByVal user As String, _
                                              ByVal password As String, _
                                              ByVal ipAplicacion As String) As String
        Dim objArchivoXML As String
        Dim url As String = ConfigurationSettings.AppSettings("constRutaActualizarRecargaVirtual")
        Dim objRequest As HttpWebRequest

        Try
            objArchivoXML = System.IO.File.OpenText(ConfigurationSettings.AppSettings("strRutaSiteApp") & "ActualizarRecargaVirtualRequest.xml").ReadToEnd
            Dim objRequestActualizarRecarga As XmlDocument

            objRequestActualizarRecarga = New XmlDocument
            objRequestActualizarRecarga.LoadXml(objArchivoXML)

            Dim wsse = New XmlNamespaceManager(objRequestActualizarRecarga.NameTable)
            wsse.AddNamespace("wsse", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")
            objRequestActualizarRecarga.SelectSingleNode("//wsse:Username", wsse).InnerText = user
            objRequestActualizarRecarga.SelectSingleNode("//wsse:Password", wsse).InnerText = password

            Dim v1 = New XmlNamespaceManager(objRequestActualizarRecarga.NameTable)
            v1.AddNamespace("v1", "http://claro.com.pe/esb/data/generico/claroGenericHeader/v1/")
            objRequestActualizarRecarga.SelectSingleNode("//v1:canal", v1).InnerText = strCanal
            objRequestActualizarRecarga.SelectSingleNode("//v1:idAplicacion", v1).InnerText = ConfigurationSettings.AppSettings("CodAplicacion")
            objRequestActualizarRecarga.SelectSingleNode("//v1:usuarioAplicacion", v1).InnerText = ConfigurationSettings.AppSettings("Usuario_Aplicacion")
            objRequestActualizarRecarga.SelectSingleNode("//v1:usuarioSesion", v1).InnerText = strUserSesion
            objRequestActualizarRecarga.SelectSingleNode("//v1:idTransaccionESB", v1).InnerText = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            objRequestActualizarRecarga.SelectSingleNode("//v1:idTransaccionNegocio", v1).InnerText = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            objRequestActualizarRecarga.SelectSingleNode("//v1:fechaInicio", v1).InnerText = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffzzz")
            objRequestActualizarRecarga.SelectSingleNode("//v1:nodoAdicional", v1).InnerText = ipAplicacion

            objRequestActualizarRecarga.SelectSingleNode("//country").InnerText = itemHeaderDataPower.country
            objRequestActualizarRecarga.SelectSingleNode("//language").InnerText = itemHeaderDataPower.language
            objRequestActualizarRecarga.SelectSingleNode("//consumer").InnerText = itemHeaderDataPower.consumer
            objRequestActualizarRecarga.SelectSingleNode("//system").InnerText = itemHeaderDataPower._system
            objRequestActualizarRecarga.SelectSingleNode("//modulo").InnerText = itemHeaderDataPower.modulo
            objRequestActualizarRecarga.SelectSingleNode("//pid").InnerText = itemHeaderDataPower.pid
            objRequestActualizarRecarga.SelectSingleNode("//userId").InnerText = itemHeaderDataPower.userId
            objRequestActualizarRecarga.SelectSingleNode("//dispositivo").InnerText = itemHeaderDataPower.dispositivo
            objRequestActualizarRecarga.SelectSingleNode("//wsIp").InnerText = itemHeaderDataPower.wsIp
            objRequestActualizarRecarga.SelectSingleNode("//operation").InnerText = itemHeaderDataPower.operation
            objRequestActualizarRecarga.SelectSingleNode("//timestamp").InnerText = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffzzz")
            objRequestActualizarRecarga.SelectSingleNode("//msgType").InnerText = itemHeaderDataPower.msgType

            Dim v12 = New XmlNamespaceManager(objRequestActualizarRecarga.NameTable)
            v12.AddNamespace("v12", "http://claro.com.pe/esb/message/BSS_RecargaVirtual_v1/actualizarRecarga/v1/")
            objRequestActualizarRecarga.SelectSingleNode("//v12:lineaCliente", v12).InnerText = itemRecarga.lineaCliente
            objRequestActualizarRecarga.SelectSingleNode("//v12:estado", v12).InnerText = itemRecarga.estado
            objRequestActualizarRecarga.SelectSingleNode("//v12:puntoVenta", v12).InnerText = itemRecarga.puntoVenta
            objRequestActualizarRecarga.SelectSingleNode("//v12:tipoDocumento", v12).InnerText = itemRecarga.tipoDocumento
            objRequestActualizarRecarga.SelectSingleNode("//v12:numeroDocumento", v12).InnerText = itemRecarga.numeroDocumento
            objRequestActualizarRecarga.SelectSingleNode("//v12:montoRecarga", v12).InnerText = itemRecarga.montoRecarga
            objRequestActualizarRecarga.SelectSingleNode("//v12:fechaSwTrx", v12).InnerText = itemRecarga.fechaSwTrx
            objRequestActualizarRecarga.SelectSingleNode("//v12:valorVenta", v12).InnerText = itemRecarga.valorVenta
            objRequestActualizarRecarga.SelectSingleNode("//v12:valorDescuento", v12).InnerText = itemRecarga.valorSubTotal
            objRequestActualizarRecarga.SelectSingleNode("//v12:valorSubTotal", v12).InnerText = itemRecarga.valorSubTotal
            objRequestActualizarRecarga.SelectSingleNode("//v12:valorIGV", v12).InnerText = itemRecarga.valorIGV
            objRequestActualizarRecarga.SelectSingleNode("//v12:valorTotal", v12).InnerText = itemRecarga.valorTotal
            objRequestActualizarRecarga.SelectSingleNode("//v12:estadoActualizar", v12).InnerText = itemRecarga.estadoActualizar
            objRequestActualizarRecarga.SelectSingleNode("//v12:trace", v12).InnerText = itemRecarga.trace
            objRequestActualizarRecarga.SelectSingleNode("//v12:nombreUsuario", v12).InnerText = itemRecarga.nombreUsuario

            objArchivoXML = objRequestActualizarRecarga.InnerXml

            objRequest = WebRequest.Create(url)
            objRequest.Method = "POST"
            objRequest.Accept = "application/json"
            objRequest.Credentials = System.Net.CredentialCache.DefaultCredentials
            objRequest.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("intTimeOutBSS_RecargaVirtual"))

            Dim encoding As System.Text.UTF8Encoding = New System.Text.UTF8Encoding
            Dim byteArray As Byte() = encoding.GetBytes(objArchivoXML)
            objRequest.ContentType = "text/xml"
            objRequest.ContentLength = byteArray.Length

            Dim dataStream As Stream = objRequest.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()

            Dim httpResponse As HttpWebResponse = CType(objRequest.GetResponse(), WebResponse)
            Dim StreamReader As StreamReader

            StreamReader = New StreamReader(httpResponse.GetResponseStream())

            response = StreamReader.ReadToEnd()
            xmlCrearRV = objArchivoXML 'devuelve el xml creado

            Return True
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_RVirtual\clsBSS_RVirtual.vb; Function: ActualizarRecargaResponse)"
            response = "Error: " & ex.Message & MaptPath
            'FIN PROY-140126

            xmlCrearRV = "Error : " & objArchivoXML 'devuelve el xml creado
            Return False
        End Try
    End Function
    '/*----------------------------------------------------------------------------------------------------------------*/
    Public Function RevertirRecargaResponse(ByRef response As String, _
                                            ByRef xmlCrearRV As String, _
                                            ByVal strCanal As String, _
                                            ByVal strUserSesion As String, _
                                            ByVal itemRecarga As BERecargaVirtual, _
                                            ByVal itemHeaderDataPower As BEHeaderDataPower, _
                                            ByVal user As String, _
                                            ByVal password As String, _
                                            ByVal ipAplicacion As String) As String
        Dim objArchivoXML As String
        Dim url As String = ConfigurationSettings.AppSettings("constRutaRevertirRecargaVirtual")
        Dim objRequest As HttpWebRequest
        Dim objXMLRevertirRecargaRequest As XmlDocument = New XmlDocument

        Try
            objArchivoXML = System.IO.File.OpenText(ConfigurationSettings.AppSettings("strRutaSiteApp") & "RevertirRecargaVirtualRequest.xml").ReadToEnd
            objXMLRevertirRecargaRequest.LoadXml(objArchivoXML)

            Dim wsse = New XmlNamespaceManager(objXMLRevertirRecargaRequest.NameTable)
            wsse.AddNamespace("wsse", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")
            objXMLRevertirRecargaRequest.SelectSingleNode("//wsse:Username", wsse).InnerText = user
            objXMLRevertirRecargaRequest.SelectSingleNode("//wsse:Password", wsse).InnerText = password

            objXMLRevertirRecargaRequest.SelectSingleNode("//country").InnerText = itemHeaderDataPower.country
            objXMLRevertirRecargaRequest.SelectSingleNode("//language").InnerText = itemHeaderDataPower.language
            objXMLRevertirRecargaRequest.SelectSingleNode("//consumer").InnerText = itemHeaderDataPower.consumer
            objXMLRevertirRecargaRequest.SelectSingleNode("//system").InnerText = itemHeaderDataPower._system
            objXMLRevertirRecargaRequest.SelectSingleNode("//modulo").InnerText = itemHeaderDataPower.modulo
            objXMLRevertirRecargaRequest.SelectSingleNode("//pid").InnerText = itemHeaderDataPower.pid
            objXMLRevertirRecargaRequest.SelectSingleNode("//userId").InnerText = itemHeaderDataPower.userId
            objXMLRevertirRecargaRequest.SelectSingleNode("//dispositivo").InnerText = itemHeaderDataPower.dispositivo
            objXMLRevertirRecargaRequest.SelectSingleNode("//wsIp").InnerText = itemHeaderDataPower.wsIp
            objXMLRevertirRecargaRequest.SelectSingleNode("//operation").InnerText = itemHeaderDataPower.operation
            objXMLRevertirRecargaRequest.SelectSingleNode("//timestamp").InnerText = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffzzz")

            Dim v1 = New XmlNamespaceManager(objXMLRevertirRecargaRequest.NameTable)
            v1.AddNamespace("v1", "http://claro.com.pe/esb/data/generico/claroGenericHeader/v1/")

            objXMLRevertirRecargaRequest.SelectSingleNode("//v1:canal", v1).InnerText = strCanal
            objXMLRevertirRecargaRequest.SelectSingleNode("//v1:idAplicacion", v1).InnerText = ConfigurationSettings.AppSettings("CodAplicacion")
            objXMLRevertirRecargaRequest.SelectSingleNode("//v1:usuarioAplicacion", v1).InnerText = ConfigurationSettings.AppSettings("Usuario_Aplicacion")
            objXMLRevertirRecargaRequest.SelectSingleNode("//v1:usuarioSesion", v1).InnerText = strUserSesion
            objXMLRevertirRecargaRequest.SelectSingleNode("//v1:idTransaccionESB", v1).InnerText = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            objXMLRevertirRecargaRequest.SelectSingleNode("//v1:idTransaccionNegocio", v1).InnerText = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            objXMLRevertirRecargaRequest.SelectSingleNode("//v1:fechaInicio", v1).InnerText = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffzzz")
            objXMLRevertirRecargaRequest.SelectSingleNode("//v1:nodoAdicional", v1).InnerText = ipAplicacion

            'Asignamos los valroes del Body del WS
            Dim v12 = New XmlNamespaceManager(objXMLRevertirRecargaRequest.NameTable)
            v12.AddNamespace("v12", "http://claro.com.pe/esb/message/BSS_RecargaVirtual_v1/revertirRecargaVirtual/v1/")
            'objXMLActualizarRecargaRequest.SelectSingleNode("//v12:lineaCliente", v12).InnerText = itemRecarga.lineaCliente
            objXMLRevertirRecargaRequest.SelectSingleNode("//v12:linea", v12).InnerText = itemRecarga.linea
            objXMLRevertirRecargaRequest.SelectSingleNode("//v12:estado", v12).InnerText = itemRecarga.estado
            'objXMLActualizarRecargaRequest.SelectSingleNode("//v12:puntoVenta", v12).InnerText = itemRecarga.puntoVenta
            'objXMLActualizarRecargaRequest.SelectSingleNode("//v12:tipoDocumento", v12).InnerText = itemRecarga.tipoDocumento
            'objXMLActualizarRecargaRequest.SelectSingleNode("//v12:numeroDocumento", v12).InnerText = itemRecarga.numeroDocumento
            'objXMLActualizarRecargaRequest.SelectSingleNode("//v12:montoRecarga", v12).InnerText = itemRecarga.montoRecarga
            'objXMLActualizarRecargaRequest.SelectSingleNode("//v12:fechaSwTrx", v12).InnerText = itemRecarga.fechaSwTrx
            'objXMLActualizarRecargaRequest.SelectSingleNode("//v12:valorVenta", v12).InnerText = itemRecarga.valorVenta
            'objXMLActualizarRecargaRequest.SelectSingleNode("//v12:valorDescuento", v12).InnerText = itemRecarga.valorSubTotal
            'objXMLActualizarRecargaRequest.SelectSingleNode("//v12:valorSubTotal", v12).InnerText = itemRecarga.valorSubTotal
            'objXMLActualizarRecargaRequest.SelectSingleNode("//v12:valorIGV", v12).InnerText = itemRecarga.valorIGV
            'objXMLActualizarRecargaRequest.SelectSingleNode("//v12:valorTotal", v12).InnerText = itemRecarga.valorTotal
            objXMLRevertirRecargaRequest.SelectSingleNode("//v12:estadoActualizar", v12).InnerText = itemRecarga.estadoActualizar
            'objXMLActualizarRecargaRequest.SelectSingleNode("//v12:trace", v12).InnerText = "" 'itemRecarga.trace
            'objXMLActualizarRecargaRequest.SelectSingleNode("//v12:nombreUsuario", v12).InnerText = itemRecarga.nombreUsuario

            'obtenemos el archivo XWL Request 
            objArchivoXML = objXMLRevertirRecargaRequest.InnerXml

            'Asignamos los valores a los atributos del objRequest
            objRequest = WebRequest.Create(url)
            objRequest.Method = "POST"
            objRequest.Accept = "application/json"
            objRequest.Credentials = System.Net.CredentialCache.DefaultCredentials
            objRequest.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("intTimeOutBSS_RecargaVirtual"))

            Dim encoding As System.Text.UTF8Encoding = New System.Text.UTF8Encoding
            Dim byteArray As Byte() = encoding.GetBytes(objArchivoXML)
            objRequest.ContentType = "text/xml"
            objRequest.ContentLength = byteArray.Length

            Dim dataStream As Stream = objRequest.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()

            Dim httpResponse As HttpWebResponse = CType(objRequest.GetResponse(), WebResponse)
            Dim StreamReader As StreamReader

            StreamReader = New StreamReader(httpResponse.GetResponseStream())

            'asignamos los valroes de repsuesta
            response = StreamReader.ReadToEnd()
            xmlCrearRV = objArchivoXML
            Return True
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_RVirtual\clsBSS_RVirtual.vb; Function: RevertirRecargaResponse)"
            response = "Error: " & ex.Message & MaptPath
            'FIN PROY-140126

            xmlCrearRV = "Error : " & objArchivoXML 'devuelve el xml creado
            Return False
        End Try
    End Function
End Class

'/*----------------------------------------------------------------------------------------------------------------*/
'/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
'/*----------------------------------------------------------------------------------------------------------------*/