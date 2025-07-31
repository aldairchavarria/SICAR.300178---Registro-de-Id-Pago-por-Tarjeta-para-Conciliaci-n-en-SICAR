Imports System.Configuration
Imports System.Net
Imports Claro.Datos.DAAB
Imports Claro.Datos
Imports System
Imports System.Xml
Imports System.text
Imports System.IO

Public Class BWTipificaciones

    ' PROY-27790-IDEA-35384- Venta y Post venta de equipos PLC :: NGC   -   27/06/2017

    Dim oTransaccionesInteracciones As New TipificacionFijaWS.TipificacionFijaWSService


    ' PROY-27790-IDEA-35384- Venta y Post venta de equipos PLC :: NGC   -   27/06/2017
    Public Function TipificarHFC(ByVal txId As String, ByVal item As InteraccionTipificacion) As String

        Dim objAuditType As New TipificacionFijaWS.auditRequestType ' TransaccionesInteraccionesAsyncWS.AuditType
        Dim objDetalle(0) As TipificacionFijaWS.parametrosTypeObjetoOpcional ' TransaccionesInteraccionesAsyncWS.DetInteraccionType
        Dim objInteraccion As New TipificacionFijaWS.parametrosTypeObjetoOpcional   'TransaccionesInteraccionesAsyncWS.InteraccionType
        Dim objCabecera As New TipificacionFijaWS.registrarTipiFijaRequest
        'Dim objInteraccionPlus As New TransaccionesInteraccionesAsyncWS.InteraccionPlusType
        Dim objResponse As New TipificacionFijaWS.registrarTipiFijaResponse

        oTransaccionesInteracciones.Url = ConfigurationSettings.AppSettings("consTransaccioninteraccionAsyncWS_URL")
        oTransaccionesInteracciones.Timeout = ConfigurationSettings.AppSettings("consTransaccioninteraccionAsyncWS_TimeOut")


        Try
            objAuditType.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            objAuditType.ipAplicacion = ConfigurationSettings.AppSettings("CodAplicacion").ToString()
            objAuditType.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion").ToString() '	  Funciones.CheckStr(ConfigurationSettings.AppSettings("constNombreAplicacion"))
            objAuditType.usuarioAplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("Usuario_Aplicacion"))

            objCabecera.idContacto = item.OBJID_CONTACTO '0
            objCabecera.idSite = item.OBJID_SITE '0
            objCabecera.cuenta = item.CUENTA ' ""
            objCabecera.telefono = item.TELEFONO '"M2473760"
            objCabecera.tipo = item.TIPO ' "dth"
            objCabecera.clase = item.CLASE '"variacion"
            objCabecera.subClase = item.SUBCLASE '"ventas varios"
            objCabecera.metodoContacto = item.METODO_CONTACTO ' "Teléfono"
            objCabecera.tipoInter = item.TIPO_INTER ' "ENTRANTE"
            objCabecera.agente = item.AGENTE '"USRSICAR"
            objCabecera.usrProceso = item.USUARIO_PROCESO '"USRSICAR"
            objCabecera.hechoEnUno = item.HECHO_EN_UNO ' 0
            objCabecera.notas = item.NOTAS ' "NOTAGENERAA PARA ..."
            objCabecera.flagCaso = item.FLAG_CASO '0
            objCabecera.resultado = item.RESULTADO ' "ninguno"

            objCabecera.auditRequest = objAuditType
            objCabecera.listaRequestOpcional = objDetalle
            'datos entrada que deben pertenecer al nuevo servicio
            'objInteraccion.tipoInteraccion = item.RESULTADO
            'objInteraccion.tipoInteraccion = item.P_INCONVEN
            'objInteraccion.tipoInteraccion = item.P_SERVAFECT_CODE
            'objInteraccion.tipoInteraccion = item.P_INCONVEN_CODE
            'objInteraccion.tipoInteraccion = item.P_CO_ID
            'objInteraccion.tipoInteraccion = item.P_COD_PLANO
            'objInteraccion.tipoInteraccion = item.P_VALOR1
            'objInteraccion.tipoInteraccion = item.P_VALOR2


            Dim strFlagCaso = ""

            objResponse = oTransaccionesInteracciones.registrarTipiFija(objCabecera)   'oTransaccionesInteracciones.crearInteraccion(txId, objInteraccion, objInteraccionPlus, objDetalle, strFlagCaso)

            Dim strcodigoError = objResponse.auditResponse.codigoRespuesta '.errorCode
            Dim msjError = objResponse.msgRespuesta ' objResponse.errorMsg
            Dim salida As String = ""

            If strcodigoError = "0" Then
                Dim Transaccion As String = objResponse.idInteraccion ' objResponse.txId
                salida = String.Format("{0};{1};{2}", strcodigoError, msjError, Transaccion)
            Else
                salida = String.Format("{0};{1};{2}", strcodigoError, msjError, "")
            End If

            Return salida


        Catch ex As Exception

            Dim strmensaje = ex.Message

        End Try


    End Function

    'END  PROY-27790-IDEA-35384- Venta y Post venta de equipos PLC  :: NGC   -   27/06/2017




End Class
