Imports System.Configuration
Public Class clsConsultaProgramacion

    Public Function ConsultaProgramacion(ByVal NroLinea As String, ByVal strTipTransaccion As String, ByVal strEstadoTrans As String, ByVal strTipServicio As String, _
                                        ByVal strFechaDesde As String, ByVal strFechaHasta As String, ByVal strAsesor As String, ByVal strCuenta As String, _
                                        ByVal strCodInteraccion As String, ByVal strCadDac As String, ByVal strServCod As String, ByVal strUsuario As String, ByRef strMensajeError As String) As CambioPlanPostpago

        Dim _DatosPostpago As New ConsultaProgramacion.ConsultaProgramacionService

        _DatosPostpago.Url = ConfigurationSettings.AppSettings("consConsultaProgramacion")
        _DatosPostpago.Credentials = System.Net.CredentialCache.DefaultCredentials
        _DatosPostpago.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("consConsultaProgramacion_timeout"))

        'Auditoria
        Dim auditoriaWS As New ConsultaProgramacion.parametrosAuditRequest
        auditoriaWS.idTransaccion = DateTime.Now.ToString("yyyyMMddHHss")
        auditoriaWS.ipAplicacion = ConfigurationSettings.AppSettings("CodAplicacion")
        auditoriaWS.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
        auditoriaWS.usuarioAplicacion = strUsuario

        'Response de ejecucion de consulta
        Dim objConsultarServicioProgramadoResponse As New ConsultaProgramacion.parametrosAuditResponse

        'Lista Opcional Request
        Dim objlistaRequestOpcional(1) As ConsultaProgramacion.parametrosRequestObjetoRequestOpcional
        objlistaRequestOpcional(1) = New ConsultaProgramacion.parametrosRequestObjetoRequestOpcional
        objlistaRequestOpcional(1).campo = String.Empty
        objlistaRequestOpcional(1).valor = String.Empty

        'Lista de datos de salida
        Dim objListaConsultaServicioProgramado(1) As ConsultaProgramacion.TypeServicioProgramado '---
        objListaConsultaServicioProgramado(1) = New ConsultaProgramacion.TypeServicioProgramado

        'Response Opcional de salida
        Dim objlistaResponseOpcional(1) As ConsultaProgramacion.parametrosResponseObjetoResponseOpcional
        objlistaResponseOpcional(1) = New ConsultaProgramacion.parametrosResponseObjetoResponseOpcional

        Dim obj As New COM_SIC_Activaciones.CambioPlanPostpago
        Try

            objConsultarServicioProgramadoResponse = _DatosPostpago.consultarServicioProgramado(auditoriaWS, NroLinea, strFechaDesde, strFechaHasta, strEstadoTrans, strAsesor, _
                                                                                                strCuenta, strTipTransaccion, strCodInteraccion, strCadDac, strTipServicio, strServCod, _
                                                                                                objlistaRequestOpcional, objListaConsultaServicioProgramado, objlistaResponseOpcional)

            If objConsultarServicioProgramadoResponse.codigoRespuesta.ToString.Equals("0") Then
                For i As Integer = 0 To objListaConsultaServicioProgramado.Length - 1
                    obj.coId = objListaConsultaServicioProgramado(i).CoId.ToString
                    obj.descripcionTipoTpe = objListaConsultaServicioProgramado(i).DescEstado.ToString
                    obj.strServiEst = objListaConsultaServicioProgramado(i).Estado.ToString 'ADD: PROY 26210 - RMZ
                    obj.strServiCod = objListaConsultaServicioProgramado(i).ServiCod.ToString 'ADD: PROY 26210 - RMZ
                Next
            Else
                strMensajeError = objConsultarServicioProgramadoResponse.mensajeRespuesta.ToString()
            End If

        Catch ex As Exception
            strMensajeError = "Error del Servicio :" & ex.Message
        End Try
        Return obj
    End Function

End Class
