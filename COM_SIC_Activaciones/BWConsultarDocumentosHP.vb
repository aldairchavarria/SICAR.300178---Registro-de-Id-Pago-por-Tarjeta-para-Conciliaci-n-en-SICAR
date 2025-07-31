Imports System.Configuration
Imports COM_SIC_Activaciones

Public Class BWConsultarDocumentosHP
    'INICIO-PROY-25335-Contratacion Electronica R2 - GAPS
    Private BWTransaccion As New WSconsultarDocumentosHP.ConsultarDocumentosHPWSService

    Public Sub New()
        BWTransaccion.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("consConsultarDocumentosHP_Url"))
        BWTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
        BWTransaccion.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("consConsultarDocumentosHP_TimeOut"))
    End Sub


    Public Function ConsultarDocumentosHP(ByVal strNroAcuerdo As String, ByVal strUsuario As String, ByRef strCodRpta As String, ByRef strMsjRespuesta As String) As ArrayList
        'BEAcuerdoHP obAcuerdoHP = null;
        Dim lstAcuerdoHP As New ArrayList
        strCodRpta = [String].Empty
        strMsjRespuesta = [String].Empty

        Try
            Dim objRequest As New WSconsultarDocumentosHP.consultarDocumentosHPRequest
            Dim objRequestAudit As New WSconsultarDocumentosHP.auditRequestType

            'INICIO|REQUEST
            objRequest.nroAcuerdo = Funciones.CheckStr(strNroAcuerdo)
            objRequest.nroContrato = Funciones.CheckStr(strNroAcuerdo)
            objRequest.nroSec = "0"
            objRequest.nroSot = "0"

            objRequestAudit.idTransaccion = Funciones.CheckStr(DateTime.Now.ToString("yyyyMMddHHmmss"))
            objRequestAudit.ipAplicacion = System.Net.Dns.GetHostName()
            objRequestAudit.usuarioAplicacion = Funciones.CheckStr(strUsuario)
            objRequestAudit.nombreAplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))

            objRequest.auditRequest = objRequestAudit
            'FIN|REQUEST

            Dim objResponse As WSconsultarDocumentosHP.consultarDocumentosHPResponse = BWTransaccion.consultarDocumentosHP(objRequest)

            'INICIO|RESPONSE

            Dim lst = objResponse.listaDocumentosHP

            If objResponse.listaDocumentosHP Is Nothing OrElse objResponse.listaDocumentosHP.Length > 0 Then
                For i As Integer = 0 To objResponse.listaDocumentosHP.Length - 1

                    Dim arrItem = lst(i)

                    Dim obAcuerdoHP As New BEAcuerdoHP
                    obAcuerdoHP.idDetalleHP = arrItem.idDetalleHP
                    obAcuerdoHP.CodigoContrato = arrItem.nroContrato
                    obAcuerdoHP.CodigoSec = arrItem.nroSec
                    obAcuerdoHP.Nro_sot = arrItem.nroSot
                    obAcuerdoHP.CodigoAcuerdo = arrItem.nroAcuerdo
                    obAcuerdoHP.CodigoHP = arrItem.codHP
                    obAcuerdoHP.Estado = arrItem.estado
                    obAcuerdoHP.FechaCreacion = arrItem.fecUsu
                    obAcuerdoHP.RutaHP = arrItem.rutaHP

                    lstAcuerdoHP.Add(obAcuerdoHP)
                Next
            Else
                lstAcuerdoHP = Nothing
            End If
            ConsultarDocumentosHP = lstAcuerdoHP
            strCodRpta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
            strMsjRespuesta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)
            'FIN|RESPONSE
        Catch ex As Exception
            lstAcuerdoHP = Nothing
            strCodRpta = "-1"
            strMsjRespuesta = Funciones.CheckStr(ex.Message)
        End Try
    End Function

    Public Function EnviarDocumentosHP(ByVal strNroAcuerdo As String, ByVal strDestino As String, ByVal strAsunto As String, ByVal strMensajeCorreo As String, ByVal strUsuario As String, ByVal srtTipoVenta As String, ByVal strTipoOperacionDesc As String, ByVal srtNroPedido As String, ByRef strCodRptaEnvio As String, ByRef strMsjRespuestaEnvio As String) 'PROY-140578 - FIRMAS
        strCodRptaEnvio = [String].Empty
        strMsjRespuestaEnvio = [String].Empty
        Dim objsetting As New AppSettings
        Dim strFlagHTML As String = objsetting.Key_FlagHTML

        Try
            Dim objRequestAudit As New WSConsultarDocumentosHP.auditRequestType
            Dim objrequest As New WSConsultarDocumentosHP.enviarDocumentosHPRequest
            Dim objresponse As New WSConsultarDocumentosHP.enviarDocumentosHPResponse

            'INICIO-AUDITORIA
            objRequestAudit.idTransaccion = Funciones.CheckStr(DateTime.Now.ToString("yyyyMMddHHmmss"))
            objRequestAudit.ipAplicacion = System.Net.Dns.GetHostName()
            objRequestAudit.usuarioAplicacion = Funciones.CheckStr(strUsuario)
            objRequestAudit.nombreAplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))

            'PARAMETROS SERVICIO - INI
            objrequest.auditRequest = objRequestAudit
            'PROY-140578 - FIRMAS - INI --REPOSICION CHIP REPUESTO
            If Funciones.CheckStr(srtTipoVenta) = ConfigurationSettings.AppSettings("strTVPrepago") And Funciones.CheckStr(strTipoOperacionDesc) <> ConfigurationSettings.AppSettings("CodDescripcionReposicionPrepago") Then
                objrequest.nroContrato = "0"
                objrequest.nroSec = "0"
                objrequest.nroSot = Funciones.CheckStr(srtNroPedido)
                objrequest.nroAcuerdo = "0"
            Else
                objrequest.nroContrato = Funciones.CheckStr(strNroAcuerdo)
                objrequest.nroSec = "0"
                objrequest.nroSot = "0"
                objrequest.nroAcuerdo = Funciones.CheckStr(strNroAcuerdo)
            End If
            'PROY-140578 - FIRMAS - FIN

            objrequest.destinatarios = Funciones.CheckStr(strDestino)
            objrequest.asunto = Funciones.CheckStr(strAsunto)
            objrequest.mensaje = Funciones.CheckStr(strMensajeCorreo)
            objrequest.flagHtml = Funciones.CheckStr(strFlagHTML)
            'PARAMETROS SERVICIO - FIN

            objresponse = BWTransaccion.enviarDocumentosHP(objrequest)

            strCodRptaEnvio = Funciones.CheckStr(objresponse.auditResponse.codigoRespuesta)
            strMsjRespuestaEnvio = Funciones.CheckStr(objresponse.auditResponse.mensajeRespuesta)

        Catch ex As Exception
            strCodRptaEnvio = "-1"
            strMsjRespuestaEnvio = Funciones.CheckStr(ex.Message)
        End Try

    End Function


    'FIN-PROY-25335-Contratacion Electronica R2 - GAPS
End Class
