Imports System.Configuration
Imports System.Net
Imports Claro.Datos.DAAB
Imports Claro.Datos
Imports System
Imports System.Xml
Imports System.text
Imports System.IO
' PROY-27790-IDEA-35384- Venta y Post venta de equipos PLC :: NGC   -   27/06/2017
    Public Class BWActualizaEquipoContrato

    'Dim oTransaccionesInteracciones As New TransaccionesInteraccionesAsyncWS.TransaccionInteraccionesAsync
    Dim oActualizaEquipoContrato As New ActualizaEquipoContratoWS.ActualizaEquipoContratoWSService

    ' PROY-27790-IDEA-35384- Venta y Post venta de equipos PLC :: NGC   -   27/06/2017

    Public Function actualizarEquiposVarios(ByVal strIdTransaccion As String, ByVal strIPAplicacion As String, ByVal strNombaplicacion As String, ByVal strUsrProceso As String, ByVal strCodCliente As String, ByVal strNumPedido As String, ByVal strUsuarioModi As String) As String

        Dim objRequest As New ActualizaEquipoContratoWS.actualizarEquipoContratoRequest
        Dim objResponse As New ActualizaEquipoContratoWS.actualizarEquipoContratoResponse
        Dim objAuditRequest As New ActualizaEquipoContratoWS.auditRequestType
        Dim objAuditResponse As New ActualizaEquipoContratoWS.auditResponseType
        Dim objInteraccionPlus As New ActualizaEquipoContratoWS.parametrosTypeObjetoOpcional

        Dim strSalida As String
        strSalida = ""
        oActualizaEquipoContrato.Url = ConfigurationSettings.AppSettings("ActualizaEquipoContratoWS_URL")
        oActualizaEquipoContrato.Timeout = ConfigurationSettings.AppSettings("ActualizaEquipoContratoWS_TimeOut")

        Try


            objRequest.codCliente = strCodCliente.PadLeft(16, "0")
            objRequest.nroPedido = strNumPedido
            objRequest.usuarioModificacion = strUsuarioModi

            objAuditRequest.idTransaccion = strIdTransaccion
            objAuditRequest.ipAplicacion = strIPAplicacion
            objAuditRequest.nombreAplicacion = strNombaplicacion
            objAuditRequest.usuarioAplicacion = strUsrProceso

            objRequest.auditRequest = objAuditRequest

            objResponse = oActualizaEquipoContrato.actualizarEquipoContrato(objRequest)

            Dim strCodRpta = objResponse.auditResponse.codigoRespuesta
            Dim strMsjRpta = objResponse.auditResponse.mensajeRespuesta
            Dim idTransaccion = objResponse.auditResponse.idTransaccion
            Dim tipoMate = objResponse.tipoMaterial
            Dim CustomerID = objResponse.idCliente

            strSalida = strCodRpta + ";" + strMsjRpta + ";" + idTransaccion + ";" + tipoMate + ";" + CustomerID

        Catch ex As Exception

            Print(ex.Message.ToString)

        End Try
        Return strSalida.ToString()
    End Function

    ' END PROY-27790-IDEA-35384- Venta y Post venta de equipos PLC :: NGC   -   27/06/2017
End Class
