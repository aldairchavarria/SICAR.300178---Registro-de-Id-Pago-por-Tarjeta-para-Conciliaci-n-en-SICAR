'PROY-31766 - INICIO
Imports System.Configuration
Imports System.Net

Public Class BWConsultaIGV

    Dim oConsultaIGV As New ConsultaIGVWS.ConsultaIGVWSService

    Public Function ObtenerIGV(ByVal strUpdateCache As String, ByRef strCodRpta As String, ByRef strMgsRpta As String, ByRef strIGVD As Double) As Double

        Dim objRequest = New ConsultaIGVWS.consultarIGVRequest
        Dim objResponse = New ConsultaIGVWS.consultarIGVResponse
        Dim objDatosConsultaIGVList = New ConsultaIGVWS.ListaIGVSResponseType
        oConsultaIGV.Url = ConfigurationSettings.AppSettings("consConsultaIGV_Url")
        oConsultaIGV.Timeout = ConfigurationSettings.AppSettings("consConsultaIGV_TimeOut")

        Dim objAuditRequest As New ConsultaIGVWS.AuditRequestType

        objAuditRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff")
        objAuditRequest.ipAplicacion = Funciones.CheckStr(Dns.GetHostByName(Dns.GetHostName()).AddressList(0))
        objAuditRequest.nombreAplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
        objAuditRequest.usuarioAplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("Usuario_Aplicacion"))
        objRequest.auditoria = objAuditRequest
        objRequest.updateCache = strUpdateCache

        objResponse = oConsultaIGV.consultarIGV(objRequest)

        strCodRpta = Funciones.CheckStr(objResponse.defaultServiceResponse.idRespuesta)
        strMgsRpta = Funciones.CheckStr(objResponse.defaultServiceResponse.mensaje)
        objDatosConsultaIGVList = objResponse.listaIGVS

        Dim thisDate As Date = Today
        Dim intTipDoc As Int32 = 1

        For Each itemInformacionIGV As ConsultaIGVWS.ListaIGVSResponseType In objResponse.listaIGVS
            If (itemInformacionIGV.impudFecFinVigencia > thisDate.ToString("yyyy-MM-dd")) Then
                If (Funciones.CheckInt(itemInformacionIGV.impunTipDoc) = intTipDoc) Then
                    strIGVD = itemInformacionIGV.igvD
                End If
            End If
        Next
    End Function
End Class
'PROY-31766 - FIN
