Imports System.Configuration
Imports System.IO
Imports System.Xml

Public Class clsConsultaIgv

    Public Function Obtener_Lista(ByRef mensaje As String, ByVal strTransaccion As String, ByVal tipoDocumento As String) As DataTable
        Dim strMensaje As String = String.Empty
        Dim dt As New DataTable
        Try
            Dim servicio As WSConsultaIGV.ConsultaIGVWSService = New WSConsultaIGV.ConsultaIGVWSService
            Dim p As New System.Net.WebProxy
            p.Credentials = System.Net.CredentialCache.DefaultCredentials
            servicio.Proxy = p
            servicio.Url = ConfigurationSettings.AppSettings("RutaWSAdministrarIgv")
            Dim request As WSConsultaIGV.consultarIGVRequest = New WSConsultaIGV.consultarIGVRequest
            Dim response As WSConsultaIGV.consultarIGVResponse = New WSConsultaIGV.consultarIGVResponse
            request.auditoria = New WSConsultaIGV.AuditRequestType

            request.auditoria.idTransaccion = strTransaccion
            request.auditoria.ipAplicacion = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString
            request.auditoria.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
            request.auditoria.usuarioAplicacion = ConfigurationSettings.AppSettings("USRAPP")
            request.updateCache = 0

            Try
                Dim arrayLogError As System.Collections.ArrayList = New System.Collections.ArrayList
                arrayLogError.Add(Serializar(request.GetType(), request))
                response = servicio.consultarIGV(request)

                dt.Columns.Add(New DataColumn("imputId"))
                dt.Columns.Add(New DataColumn("impuvDes"))
                dt.Columns.Add(New DataColumn("igv"))
                dt.Columns.Add(New DataColumn("igvD"))
                dt.Columns.Add(New DataColumn("impunTipDoc"))
                dt.Columns.Add(New DataColumn("impudFecRegistro"))
                dt.Columns.Add(New DataColumn("impudFecIniVigencia"))
                dt.Columns.Add(New DataColumn("impudFecFinVigencia"))

                If Not response.listaIGVS Is Nothing Then
                    If tipoDocumento = "1" Then
                        For Each itemIgv As WSConsultaIGV.ListaIGVSResponseType In response.listaIGVS
                            Dim dr As DataRow
                            dr = dt.NewRow
                            If itemIgv.impunTipDoc = "1" Or itemIgv.impunTipDoc = "2" Then
                                dr("imputId") = itemIgv.imputId
                                dr("impuvDes") = itemIgv.impuvDes
                                dr("igv") = itemIgv.igv
                                dr("igvD") = itemIgv.igvD
                                dr("impunTipDoc") = itemIgv.impunTipDoc
                                dr("impudFecRegistro") = itemIgv.impudFecRegistro
                                dr("impudFecIniVigencia") = itemIgv.impudFecIniVigencia
                                dr("impudFecFinVigencia") = itemIgv.impudFecFinVigencia
                                dt.Rows.Add(dr)
                            End If
                        Next
                    Else
                        For Each itemIgv As WSConsultaIGV.ListaIGVSResponseType In response.listaIGVS
                            Dim dr As DataRow
                            dr = dt.NewRow
                            If itemIgv.impunTipDoc = "0" Or itemIgv.impunTipDoc = "2" Then
                                dr("imputId") = itemIgv.imputId
                                dr("impuvDes") = itemIgv.impuvDes
                                dr("igv") = itemIgv.igv
                                dr("igvD") = itemIgv.igvD
                                dr("impunTipDoc") = itemIgv.impunTipDoc
                                dr("impudFecRegistro") = itemIgv.impudFecRegistro
                                dr("impudFecIniVigencia") = itemIgv.impudFecIniVigencia
                                dr("impudFecFinVigencia") = itemIgv.impudFecFinVigencia
                                dt.Rows.Add(dr)
                            End If
                        Next
                    End If

                    mensaje = response.defaultServiceResponse.mensaje
                End If

                strMensaje = mensaje.ToString()
            Catch x As Exception
                Throw New Exception(x.Message)
            End Try
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function Serializar(ByVal tipoObjeto As System.Type, ByVal objeto As Object) As String
        Try
            Dim oMemoryStream As MemoryStream = New MemoryStream
            Dim oXmlSerializador As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(tipoObjeto)
            Dim oTextWriter As StreamWriter = New StreamWriter(oMemoryStream, System.Text.UnicodeEncoding.Unicode)
            Dim oTextReader As StreamReader = New StreamReader(oMemoryStream, System.Text.UnicodeEncoding.Unicode)
            Dim oXmlWriter As XmlTextWriter = New XmlTextWriter(oTextWriter)
            oXmlSerializador.Serialize(oXmlWriter, objeto)
            oXmlWriter.Flush()
            oMemoryStream.Position = 0
            Dim strStringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder(oTextReader.ReadToEnd())

            Dim txt1 As String = "<?xml version=" + Chr(34) + "1.0" + Chr(34) + " encoding=" + Chr(34) + "utf-16" + Chr(34) + "?>"
            strStringBuilder.Replace(txt1, "")
            Dim txt2 As String = "xmlns:xsd=" + Chr(34) + "http://www.w3.org/2001/XMLSchema" + Chr(34) + " xmlns:xsi=" + Chr(34) + "http://www.w3.org/2001/XMLSchema-instance" + Chr(34) + ""
            strStringBuilder.Replace(txt2, "")
            Return strStringBuilder.ToString()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
