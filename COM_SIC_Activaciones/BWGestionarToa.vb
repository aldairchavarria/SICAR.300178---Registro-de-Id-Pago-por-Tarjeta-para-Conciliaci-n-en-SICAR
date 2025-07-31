Imports System.Net
Imports System.Configuration
Imports System.Xml
Imports System.IO
Imports System.Text
Imports Util
Imports COM_SIC_Seguridad



Public Class BWGestionarToa

    Dim objFileLog As SICAR_Log
    Public pathFile As String = "<----LOG_Gestionar_TOA---->"
    Public nameLog As String = objFileLog.Log_CrearNombreArchivo("LogOrdenesTOA")

    Public Function GenerarReservaToa(ByVal CurrentUser As String, ByVal strPuntoVenta As String, ByVal strNrPedido As String) As Boolean
        Dim blRespuesta As Boolean = True

        Try
            Dim request As HttpWebRequest = CreateWebRequest()
            Dim ObtenerOrdenTrabajo As String = ObtenerTipoTrabajo(strPuntoVenta)
            Dim AuditRequest As String = GenerarCadenaAuditXML(CurrentUser)
            Dim Cabecera As String = GenerarCadenaCabeceraXML()
            Dim Comando As String = GenerarCadenaComandoXML()
            Dim OrdenTrabajo As String = GenerarCadenaOrdenTrabajoXML(strNrPedido, ObtenerOrdenTrabajo)
            'Dim Propiedades As String = String.Empty

            'XML REQUEST
            Dim soapEnvelopeXml As New XmlDocument
            Dim sb As StringBuilder

            sb.AppendFormat("{0}", "<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:typ=""http://claro.com.pe/ebsADMCUAD/Inbound/types"" xmlns:base=""http://claro.com.pe/ebsADMCUAD/Inbound/base"">")
            sb.AppendFormat("{0}", "<soapenv:Header/>")
            sb.AppendFormat("{0}", "<soapenv:Body>")
            sb.AppendFormat("{0}", "<typ:gestionarOrdenRequest>")
            sb.AppendFormat("{0}", AuditRequest)
            sb.AppendFormat("{0}", Cabecera)
            sb.AppendFormat("{0}", Comando)
            sb.AppendFormat("{0}", "<typ:ordenTrabajo>")
            sb.AppendFormat("{0}", OrdenTrabajo)
            'sb.AppendFormat("{0}", Propiedades) 'Propiedades
            sb.AppendFormat("{0}", "</typ:ordenTrabajo>")
            sb.AppendFormat("{0}", "</typ:gestionarOrdenRequest>")
            sb.AppendFormat("{0}", "</soapenv:Body>")
            sb.AppendFormat("{0}", "</soapenv:Envelope>")

            soapEnvelopeXml.LoadXml(sb.ToString())
            Dim stream As Stream = request.GetRequestStream()
            soapEnvelopeXml.Save(stream)
            stream.Close()

            ' EJECUCION DE EL SERVICIO
            Dim response As WebResponse = request.GetResponse()
            Dim rd As StreamReader = New StreamReader(response.GetResponseStream())
            Dim soapResult As String = rd.ReadToEnd()
            'Dim xmlcod As New XmlDocument
            'xmlcod.LoadXml(soapResult)


        Catch ex As Exception
            blRespuesta = False
        End Try

        Return blRespuesta
    End Function

    Private Function CreateWebRequest() As HttpWebRequest

        Dim urlHP As String = ConfigurationSettings.AppSettings("WSInboundToaUrl")
        Dim Headers As String = ConfigurationSettings.AppSettings("WS_Headers")
        Dim ContentType As String = ConfigurationSettings.AppSettings("WS_ContentType")
        Dim Accept As String = ConfigurationSettings.AppSettings("WS_Accept")
        Dim Method As String = ConfigurationSettings.AppSettings("WS_Method")

        Dim webRequest As HttpWebRequest = CType(webRequest.Create(urlHP), HttpWebRequest)


        webRequest.Headers.Add(Headers)

        webRequest.ContentType = ContentType
        webRequest.Accept = Accept
        webRequest.Method = Method
        webRequest.KeepAlive = False
        webRequest.ServicePoint.Expect100Continue = False
        webRequest.ProtocolVersion = HttpVersion.Version10

        Return webRequest

    End Function

    Private Function GenerarCadenaAuditXML(ByVal strCurrentUsert As String) As String

        Dim strCadenaXML As String
        strCadenaXML += "<typ:auditRequest>"
        strCadenaXML += String.Format("{0}{1}{2}", "<base:idTransaccion>", DateTime.Now.ToString("yyyyMMddHHmmssfff"), "</base:idTransaccion>")
        strCadenaXML += String.Format("{0}{1}{2}", "<base:ipAplicacion>", DateTime.Now.ToString("yyyyMMddHHmmssfff"), "</base:ipAplicacion>")
        strCadenaXML += String.Format("{0}{1}{2}", "<base:nombreAplicacion>", ConfigurationSettings.AppSettings("constAplicacion"), "</base:nombreAplicacion>") 'VALIDAR CON ALEX
        strCadenaXML += String.Format("{0}{1}{2}", "<base:usuarioAplicacion>", strCurrentUsert, "</base:usuarioAplicacion>")
        strCadenaXML += "</typ:auditRequest>"

        Return strCadenaXML

    End Function

    Private Function GenerarCadenaCabeceraXML() As String

        Dim strCadenaXML As String
        strCadenaXML += "<typ:cabecera>"
        strCadenaXML += String.Format("{0}{1}{2}", "<typ:modoCargaPropiedades>", ReadKeySettings.Key_ModoCargaPropiedadesUpdate, "</typ:modoCargaPropiedades>")
        strCadenaXML += String.Format("{0}{1}{2}", "<typ:tipoCarga>", ReadKeySettings.Key_TipoCarga, "</typ:tipoCarga>")
        strCadenaXML += "<typ:configuracionSOT>"
        strCadenaXML += String.Format("{0}{1}{2}", "<typ:camposClave>", ReadKeySettings.Key_ConfiguracionSOT, "</typ:camposClave>")
        strCadenaXML += "</typ:configuracionSOT>"
        strCadenaXML += "<typ:configuracionInventario>"
        strCadenaXML += String.Format("{0}{1}{2}", "<typ:camposClave>", ReadKeySettings.Key_ConfiguracionInventario, "</typ:camposClave>")
        strCadenaXML += "</typ:configuracionInventario>"
        strCadenaXML += "</typ:cabecera>"

        Return strCadenaXML

    End Function

    Private Function GenerarCadenaComandoXML() As String

        Dim strCadenaXML As String
        strCadenaXML += "<typ:comando>"
        strCadenaXML += String.Format("{0}{1}{2}", "<typ:tipoComando>", ReadKeySettings.Key_TipoComando, "</typ:tipoComando>")
        strCadenaXML += "</typ:comando>"

        Return strCadenaXML
    End Function

    Private Function GenerarCadenaOrdenTrabajoXML(ByVal strNrPedido As String, ByVal strTipoTrabajo As String) As String

        Dim strCadenaXML As String
        strCadenaXML += String.Format("{0}{1}{2}", "<typ:idAgenda>", strNrPedido, "</typ:idAgenda>") 'VALIDAR SI ES EL NUMERO DE PEDIDO O ES EL QUE RETORNA TOA
        strCadenaXML += String.Format("{0}{1}{2}", "<typ:tipoTrabajo>", strTipoTrabajo, "</typ:tipoTrabajo>")
        Return strCadenaXML

    End Function


    Private Function ObtenerTipoTrabajo(ByVal strPuntoVenta As String) As String
        Dim strTipoTrabajo As String = String.Empty

        Try

            Dim PuntoVenta As String = Funciones.CheckStr(ReadKeySettings.Key_PDVVentaExpress)

            If PuntoVenta <> "" AndAlso Not PuntoVenta Is Nothing Then

                Dim arrPuntoVenta As String() = PuntoVenta.Split("|"c)
                Dim descPuntoVenta As String = String.Empty
                Dim blValor As Boolean = False

                For i As Integer = 0 To arrPuntoVenta.Length - 1

                    descPuntoVenta = Funciones.CheckStr(arrPuntoVenta(i))

                    If descPuntoVenta = strPuntoVenta AndAlso Not descPuntoVenta Is Nothing Then
                        blValor = True
                        Exit For
                    End If

                Next

                If blValor Then
                    strTipoTrabajo = ReadKeySettings.Key_TipoOrdenExpress

                Else
                    strTipoTrabajo = ReadKeySettings.Key_TipoOrdenRegular
                End If

            Else
                strTipoTrabajo = ReadKeySettings.Key_TipoOrdenRegular
            End If


        Catch ex As Exception
            strTipoTrabajo = ReadKeySettings.Key_TipoOrdenRegular
        End Try


    End Function

End Class
