Imports System.Configuration
Public Class BWRegistrarTrazabilidad
    'INICIO-PROY-25335-Contratacion Electronica R2 - GAPS
    Private objRegistraTrazabilidad As New WSRegistrarTrazabilidad.ebsRegistrarTrazabilidadService

    Public Sub New()
        objRegistraTrazabilidad.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("constRegistraTrazabilidadTimeOut"))
        objRegistraTrazabilidad.Url = ConfigurationSettings.AppSettings("constRegistraTrazabilidadUrl")
    End Sub

    Public Function registrarTrazabilidad(ByVal objRequest As BERegistrarTrazabilidadRequest) As BERegistrarTrazabilidadResponse
        Try
            Dim objResponse As New BERegistrarTrazabilidadResponse
            Dim request As New WSRegistrarTrazabilidad.registrarRequest
            Dim auditRequest As New WSRegistrarTrazabilidad.parametrosAuditRequest
            Dim response As New WSRegistrarTrazabilidad.registrarResponse

            auditRequest.idTransaccion = objRequest.auditRequest.idTransaccion
            auditRequest.ipAplicacion = objRequest.auditRequest.ipAplicacion
            auditRequest.nombreAplicacion = objRequest.auditRequest.nombreAplicacion
            auditRequest.usuarioAplicacion = objRequest.auditRequest.usuarioAplicacion

            request.auditRequest = auditRequest
            request.biometria = New WSRegistrarTrazabilidad.parametrosBiometria
            request.biometria.idPadre = objRequest.idPadre
            request.biometria.codOperacion = objRequest.codOperacion
            request.biometria.sistema = objRequest.sistema
            request.biometria.codCanal = objRequest.codCanal
            request.biometria.codPdv = objRequest.codPdv
            request.biometria.codModalVenta = objRequest.codModalVenta
            request.biometria.tipoDocumento = objRequest.tipoDocumento
            request.biometria.numeroDocumento = objRequest.numeroDocumento
            request.biometria.lineas = objRequest.lineas
            request.biometria.veprnId = objRequest.veprnId
            request.biometria.dniAutorizado = objRequest.dniAutorizado
            request.biometria.usuarioCtaRed = objRequest.usuarioCtaRed
            request.biometria.idHijo = objRequest.idHijo
            request.biometria.padreAnt = objRequest.padreAnt
            request.biometria.dniConsultado = objRequest.dniConsultado
            request.biometria.wsOrigen = objRequest.wsOrigen
            request.biometria.tipoValidacion = objRequest.tipoValidacion
            request.biometria.origenTipo = objRequest.origenTipo
            request.biometria.codigoError = objRequest.codigoError
            request.biometria.mensajeProceso = objRequest.mensajeProceso
            request.biometria.estado = objRequest.estado
            request.biometria.flag = objRequest.flag

            If objRequest.listaOpcional Is Nothing Then
                request.listaCamposAdicionales = New WSRegistrarTrazabilidad.ListaCamposAdicionalesTypeCampoAdicional(0) {}
            Else
                request.listaCamposAdicionales = New WSRegistrarTrazabilidad.ListaCamposAdicionalesTypeCampoAdicional(objRequest.listaOpcional.Count - 1) {}
                For i As Integer = 0 To objRequest.listaOpcional.Count - 1
                    Dim l As New WSRegistrarTrazabilidad.ListaCamposAdicionalesTypeCampoAdicional
                    l.nombreCampo = objRequest.listaOpcional(i).campo
                    l.valor = objRequest.listaOpcional(i).valor
                    request.listaCamposAdicionales(i) = l
                Next
            End If

            'Dim response As WSRegistrarTrazabilidad.registrarResponse = objRegistraTrazabilidad.registrarTrazabilidad(request)
            response = objRegistraTrazabilidad.registrarTrazabilidad(request)

            objResponse.idTransaccion = response.auditResponse.idTransaccion
            objResponse.codRespuesta = response.auditResponse.codRespuesta
            objResponse.msjRespuesta = response.auditResponse.msjRespuesta
            Return objResponse
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'FIN-PROY-25335-Contratacion Electronica R2 - GAPS
End Class
