Imports System.EnterpriseServices
Imports System.Configuration
Imports COM_SIC_Promociones
'<Transaction(TransactionOption.Required), Synchronization(SynchronizationOption.Required), JustInTimeActivation(True)> _
Public Class clsActivacion
    '   Inherits ServicedComponent
    '<AutoComplete(True)> _
        Public Function FP_AccionGeneral(ByVal p_strURL As String, ByVal p_strServicio As String, _
                                            ByVal p_strUsuario As String, _
                                            ByVal p_strNombreCampo As String, _
                                            ByVal p_strValorCampo As String) As String

        Dim objComponente As Object

        objComponente = CreateObject("COM_PVU_Activacion.clsActivacion")
        objComponente.FP_SetUrlSvr(p_strURL)

        FP_AccionGeneral = objComponente.FP_AccionGeneral(p_strServicio, p_strUsuario, p_strNombreCampo, p_strValorCampo)

        objComponente = Nothing

    End Function
    '<AutoComplete(True)> _
 Public Function FK_ActivacionClienteRecurrente(ByVal strNumeroContrato As String) As String

        Dim oTransaccion As New ActivacionPostpagoWS.ActivaAcuerdoService
        Dim oCallservice As New ActivacionPostpagoWS.callservice
        Dim oCallserviceHeader As New ActivacionPostpagoWS.callserviceHeader
        Dim oCallserviceDatos As New ActivacionPostpagoWS.callserviceDatos
        Dim oCallserviceHeaderCliente As New ActivacionPostpagoWS.callserviceHeaderCliente
        Dim oCallserviceDatosParametro As New ActivacionPostpagoWS.callserviceDatosParametro

        Dim StrUrl As String = ConfigurationSettings.AppSettings("ConstUrlActivacion").ToString()
        Dim strTimeOut As String = ConfigurationSettings.AppSettings("ConstTimeOutActivacion").ToString()
        Dim strServicio As String = ConfigurationSettings.AppSettings("ConstServicioActivacion").ToString()
        Dim strUsuario As String = ConfigurationSettings.AppSettings("ConstUsuarioActivacion").ToString()
        Dim strClave As String = ConfigurationSettings.AppSettings("ConstClaveActivacion").ToString()
        Dim strAplicacion As String = ConfigurationSettings.AppSettings("ConstCodAplicacion").ToString()
        Dim strParamNombre As String = ConfigurationSettings.AppSettings("ConstNombreParametro").ToString()
        Dim strParamModo As String = ConfigurationSettings.AppSettings("ConstModoParametro").ToString()

        Dim msgSalida As String

        Try
            'Get Url-Credentials-TimeOut
            oTransaccion.Url = StrUrl
            oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
            oTransaccion.Timeout = CInt(strTimeOut)

            'Set callserviceHeader
            oCallserviceHeaderCliente.timeout = strTimeOut
            oCallserviceHeaderCliente.servicio = strServicio
            oCallserviceHeaderCliente.usuario = strUsuario
            oCallserviceHeaderCliente.clave = strClave
            oCallserviceHeaderCliente.aplicacion = strAplicacion

            'Set callserviceHeader
            oCallserviceHeader.cliente = oCallserviceHeaderCliente

            'Set callserviceDatosParametro
            oCallserviceDatosParametro.nombre = strParamNombre
            oCallserviceDatosParametro.modo = strParamModo
            oCallserviceDatosParametro.valor = strNumeroContrato

            'Set callserviceDatos
            Dim parametros(1) As ActivacionPostpagoWS.callserviceDatosParametro
            oCallserviceDatos.parametro = parametros
            oCallserviceDatos.parametro(0) = oCallserviceDatosParametro

            'Set callservice
            oCallservice.header = oCallserviceHeader
            oCallservice.datos = oCallserviceDatos

            'Invocar Método
            msgSalida = oTransaccion.activaAcuerdo(oCallservice)

        Catch ex As Exception
            msgSalida = "Error Activación. " + ex.Message.ToString()
        Finally
            oCallservice = Nothing
            oCallserviceHeader = Nothing
            oCallserviceDatos = Nothing
            oCallserviceHeaderCliente = Nothing
            oCallserviceDatosParametro = Nothing
        End Try

        Return msgSalida

    End Function

    Public Function FK_CuentasClienteRecurrente(ByVal p_ValorCampo As String, _
                                ByVal StrUrl As String, _
                                ByVal StrNomServicio As String, _
                                ByVal strUsuario As String) As String

        Dim objComponente As Object

        objComponente = CreateObject("COM_PVU_Clie_Rec.clsActivacionCR")

        FK_CuentasClienteRecurrente = objComponente.FK_CuentasClienteRecurrente(p_ValorCampo, StrUrl, StrNomServicio, strUsuario)

        objComponente = Nothing

    End Function

    Public Function getCuentasCliente(ByVal strTipoDocumento As String, _
                                ByVal strNumeroDocumento As String, _
                                ByVal strIdTransaccion As String, _
                                ByRef strCodResp As String, _
                                ByRef msgSalida As String) As ArrayList

        Dim oTransaccion As New CuentasClienteWS.ObtieneCuentasClienteService
        Dim oCallservice As New CuentasClienteWS.callservice
        Dim oCallserviceHeader As New CuentasClienteWS.callserviceHeader
        Dim oCallserviceDatos As New CuentasClienteWS.callserviceDatos
        Dim oCallserviceHeaderCliente As New CuentasClienteWS.callserviceHeaderCliente

        Dim oCallserviceDatosParametro1 As New CuentasClienteWS.callserviceDatosParametro
        Dim oCallserviceDatosParametro2 As New CuentasClienteWS.callserviceDatosParametro
        Dim oCallserviceDatosParametro3 As New CuentasClienteWS.callserviceDatosParametro

        Dim StrUrl = ConfigurationSettings.AppSettings("ConstCuentasCliente_Url").ToString()
        Dim strTimeOut As String = ConfigurationSettings.AppSettings("ConstCuentasCliente_TimeOut").ToString()
        Dim strServicio As String = ConfigurationSettings.AppSettings("ConstCuentasCliente_Servicio").ToString()
        Dim strUsuario = ConfigurationSettings.AppSettings("ConstCuentasCliente_Usuario").ToString()
        Dim strClave As String = ConfigurationSettings.AppSettings("ConstCuentasCliente_Clave").ToString()
        Dim strAplicacion As String = ConfigurationSettings.AppSettings("ConstCuentasCliente_CodAplicacion").ToString()
        Dim strParamModo As String = ConfigurationSettings.AppSettings("ConstCuentasCliente_ModoParametro").ToString()

        Dim arrCuentas As New ArrayList

        Try
            'Get Url-Credentials-TimeOut
            oTransaccion.Url = StrUrl
            oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
            oTransaccion.Timeout = CInt(strTimeOut)

            'Set callserviceHeader
            oCallserviceHeaderCliente.timeout = strTimeOut
            oCallserviceHeaderCliente.servicio = strServicio
            oCallserviceHeaderCliente.usuario = strUsuario
            oCallserviceHeaderCliente.clave = strClave
            oCallserviceHeaderCliente.aplicacion = strAplicacion

            'Set callserviceHeader
            oCallserviceHeader.cliente = oCallserviceHeaderCliente

            'Set callserviceDatos
            Dim parametros(2) As CuentasClienteWS.callserviceDatosParametro
            oCallserviceDatos.parametro = parametros

            'Set callserviceDatosParametro
            oCallserviceDatosParametro1.modo = strParamModo
            oCallserviceDatosParametro1.nombre = ConfigurationSettings.AppSettings("ConstCuentasCliente_NombreParametro1").ToString()
            oCallserviceDatosParametro1.valor = strTipoDocumento
            oCallserviceDatos.parametro(0) = oCallserviceDatosParametro1

            oCallserviceDatosParametro2.modo = strParamModo
            oCallserviceDatosParametro2.nombre = ConfigurationSettings.AppSettings("ConstCuentasCliente_NombreParametro2").ToString()
            oCallserviceDatosParametro2.valor = strNumeroDocumento
            oCallserviceDatos.parametro(1) = oCallserviceDatosParametro2

            oCallserviceDatosParametro3.modo = strParamModo
            oCallserviceDatosParametro3.nombre = ConfigurationSettings.AppSettings("ConstCuentasCliente_NombreParametro3").ToString()
            oCallserviceDatosParametro3.valor = strIdTransaccion
            oCallserviceDatos.parametro(2) = oCallserviceDatosParametro3

            'Set callservice
            oCallservice.header = oCallserviceHeader
            oCallservice.datos = oCallserviceDatos

            'Invocar Método
            Dim oDATOSLISTACUENTA As New CuentasClienteWS.DATOSLISTACUENTA
            oDATOSLISTACUENTA = oTransaccion.obtieneCuentasCliente(oCallservice)

            Dim oRespuesta As New CuentasClienteWS.DATOSLISTACUENTARESPUESTA
            oRespuesta = oDATOSLISTACUENTA.RESPUESTA
            msgSalida = oRespuesta.MENSAJE_PVU
            strCodResp = oRespuesta.FLAG_PROCESO

            If strCodResp.Equals("1") Then
                Dim oDATOSLISTACUENTACUENTA As CuentasClienteWS.DATOSLISTACUENTACUENTA
                For Each oDATOSLISTACUENTACUENTA In oDATOSLISTACUENTA.CUENTA
                Dim objCuenta As New clsCuenta
                objCuenta.CS_ID = oDATOSLISTACUENTACUENTA.CS_ID
                objCuenta.CODIGO_BSCS = oDATOSLISTACUENTACUENTA.CODIGO_BSCS
                objCuenta.CS_LEVEL_CODE = oDATOSLISTACUENTACUENTA.CS_LEVEL_CODE
                objCuenta.CS_ID_HIGH = oDATOSLISTACUENTACUENTA.CS_ID_HIGH
                objCuenta.CODIGO_BSCS_HIGH = oDATOSLISTACUENTACUENTA.CODIGO_BSCS_HIGH
                objCuenta.NOMBRE_TITULAR = oDATOSLISTACUENTACUENTA.NOMBRE_TITULAR
                objCuenta.DIRECCION_DESCRIPCION = oDATOSLISTACUENTACUENTA.DIRECCION_DESCRIPCION

                objCuenta.REFERENCIA = oDATOSLISTACUENTACUENTA.REFERENCIA
                objCuenta.DESC_DPTO = oDATOSLISTACUENTACUENTA.DESC_DPTO
                objCuenta.DESC_PROV = oDATOSLISTACUENTACUENTA.DESC_PROV
                objCuenta.DESC_DIST = oDATOSLISTACUENTACUENTA.DESC_DIST
                objCuenta.COD_UBIGEO = oDATOSLISTACUENTACUENTA.COD_UBIGEO
                objCuenta.LIM_CREDITO = oDATOSLISTACUENTACUENTA.LIM_CREDITO
                objCuenta.ESTADO_CUENTA = oDATOSLISTACUENTACUENTA.ESTADO_CUENTA
                objCuenta.RESPONS_PAGO = oDATOSLISTACUENTACUENTA.RESPONS_PAGO

                If Not oDATOSLISTACUENTACUENTA.linea Is Nothing Then
                    Dim i As Integer = 0
                    Dim objLinea(oDATOSLISTACUENTACUENTA.linea.Length - 1) As clsLinea

                    For Each Linea As CuentasClienteWS.lineaType In oDATOSLISTACUENTACUENTA.linea
                        Dim oLinea As New clsLinea
                        oLinea.NUM_TELEFONO = Linea.NUM_TELEFONO
                        oLinea.PLAN = Linea.PLAN
                        oLinea.FECHA_ACTIVAC = Mid(Linea.FECHA_ACTIVAC, 7, 2) & "/" & Mid(Linea.FECHA_ACTIVAC, 5, 2) & "/" & Mid(Linea.FECHA_ACTIVAC, 1, 4)
                        oLinea.ESTADO_LINEA = Linea.ESTADO_LINEA
                        oLinea.FECHA_CAMB_ESTADO = Mid(Linea.FECHA_CAMB_ESTADO, 7, 2) & "/" & Mid(Linea.FECHA_CAMB_ESTADO, 5, 2) & "/" & Mid(Linea.FECHA_CAMB_ESTADO, 1, 4)
                        objLinea(i) = oLinea
                        i = i + 1
                    Next
                    objCuenta.linea = objLinea
                End If
                arrCuentas.Add(objCuenta)
                Next
            End If

        Catch ex As Exception
            strCodResp = "-1"
            msgSalida = ex.Message.ToString()
        Finally
            oCallservice = Nothing
            oCallserviceHeader = Nothing
            oCallserviceDatos = Nothing
            oCallserviceHeaderCliente = Nothing
            oCallserviceDatosParametro1 = Nothing
            oCallserviceDatosParametro2 = Nothing
            oCallserviceDatosParametro3 = Nothing
        End Try

        Return arrCuentas
    End Function

    'Miguel
    Public Function FK_EjecutarActivacionBono(ByVal p_NumeroTelefono As String, ByVal CodOpcion As String, ByRef strMensaje As String)
        Dim ws As New WS_IN_BONO.ActivacionWSService
        Dim objTelefono As New WS_IN_BONO.ClienteRequest
        Dim strResultado As String
        Dim objResponse As New WS_IN_BONO.ClienteResponse
        ws.Url = ConfigurationSettings.AppSettings("gWebServiceINS")
        ws.Credentials = System.Net.CredentialCache.DefaultCredentials
        ws.Timeout = ConfigurationSettings.AppSettings("timeoutWSIN")
        objTelefono.telefono = p_NumeroTelefono
        objTelefono.opcion = CodOpcion
        objResponse = ws.ejecutarActivacion(objTelefono)
        strResultado = objResponse.resultado
        strMensaje = objResponse.mensaje
        ws.Dispose()
        Return strResultado
 End Function

 'Miguel
 Public Sub FK_EjecutarDesactivacion(ByVal p_NumeroTelefono As String, ByRef p_strResultado As String, ByRef p_strTransac As String)
  Dim wse As New WS_IN2.EbsTfiWS
  Dim objTelefono As New WS_IN2.INDatosLineaRequest
  Dim objRespuesta As New WS_IN2.INDatosLineaResponse
  wse.Url = ConfigurationSettings.AppSettings("gWebServiceIN")
  wse.Credentials = System.Net.CredentialCache.DefaultCredentials
  objTelefono.telefono = p_NumeroTelefono
  wse.Timeout = ConfigurationSettings.AppSettings("timeoutWSIN")
  objRespuesta = wse.ejecutarDesactivacionTFI(objTelefono)
  p_strResultado = objRespuesta.resultado
  p_strTransac = objRespuesta.transaccion
  objRespuesta = Nothing
  wse.Dispose()
 End Sub

 'Miguel
    Public Sub FK_EjecutarActivacionTFI(ByVal p_NumeroTelefono As String, ByVal p_Opcion As String, ByRef p_strResultado As String, ByRef p_strMensaje As String, ByRef p_strTransac As String)
  Dim ws As New WS_IN2.EbsTfiWS
  Dim objTelefono As New WS_IN2.TFIRequest
  Dim objRespuesta As New WS_IN2.TFIResponse
  ws.Url = ConfigurationSettings.AppSettings("gWebServiceIN")
  ws.Credentials = System.Net.CredentialCache.DefaultCredentials
  objTelefono.telefono = p_NumeroTelefono
  objTelefono.opcion = p_Opcion
  ws.Timeout = ConfigurationSettings.AppSettings("timeoutWSIN")
  objRespuesta = ws.ejecutarActivacionTFI(objTelefono)
  p_strResultado = objRespuesta.resultado
        p_strMensaje = objRespuesta.mensaje
  p_strTransac = objRespuesta.transaccion
  objRespuesta = Nothing
  ws.Dispose()
 End Sub

    Public Function EjecutarBonoAnualTFI(ByVal strNroTelefono As String, ByVal idTransaccion As String, ByVal IPAplicacion As String, ByVal strAplicacion As String, ByRef strMensaje As String) As String

        Dim objWS As New BonoAnualTFIWS.ebsBonoAnualTFIService
        objWS.Url = ConfigurationSettings.AppSettings("ConstBonoAnualTFI_Url")
        objWS.Timeout = CInt(ConfigurationSettings.AppSettings("ConstBonoAnualTFI_Timeout"))

        Dim strCodRespuesta As String

        Try
            strCodRespuesta = objWS.bonoAnual(idTransaccion, IPAplicacion, strAplicacion, strNroTelefono, strMensaje)
        Catch ex As Exception
            strCodRespuesta = "-1"
            strMensaje = ex.Message.ToString()
        End Try
        Return strCodRespuesta
    End Function

    Public Function alertaNotificacion(ByVal tipoDoc As String, ByVal nroDoc As String, ByVal codMensaje As String, ByVal tipoProducto As String, ByVal descProducto As String, ByVal audit As ItemGenerico, ByRef codigoRespuesta As String, ByRef mensajeRespuesta As String) As String

        Dim oAlertaWS As New AlertaNotificacionWS.ebsAlertaNotificacionService
        oAlertaWS.Url = ConfigurationSettings.AppSettings("WSAlertaNotificacion_url")
        oAlertaWS.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("WSAlertaNotificacion_timeout"))

        Dim oAudit As New AlertaNotificacionWS.audiTypeRequest
        oAudit.idTransaccion = audit.CODIGO
        oAudit.ipAplicacion = audit.CODIGO2
        oAudit.nombreAplicacion = audit.DESCRIPCION
        oAudit.usuario = audit.DESCRIPCION2

        Dim respuesta As String
        respuesta = oAlertaWS.alertaNotificacion(oAudit, tipoDoc, nroDoc, codMensaje, tipoProducto, descProducto, codigoRespuesta, mensajeRespuesta)
        Return respuesta
    End Function

    Public Function AlertaNotificacionPorRenovacion(ByVal tipoDoc As String, ByVal nroDoc As String, ByVal Msisdn As String, ByVal EquipoComprado As String, ByVal EquipoPrecio As String, ByVal TiempoContrato As String, ByVal idCaso As String, ByVal audit As ItemGenerico, ByRef codigoRespuesta As String, ByRef mensajeRespuesta As String) As String

        Dim oAlertaWS As New AlertaNotificacionWS.ebsAlertaNotificacionService
        oAlertaWS.Url = ConfigurationSettings.AppSettings("WSAlertaNotificacion_url")
        oAlertaWS.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("WSAlertaNotificacion_timeout"))

        Dim oAudit As New AlertaNotificacionWS.audiTypeRequest
        oAudit.idTransaccion = audit.CODIGO
        oAudit.ipAplicacion = audit.CODIGO2
        oAudit.nombreAplicacion = audit.DESCRIPCION
        oAudit.usuario = audit.DESCRIPCION2

        Dim respuesta As String
        respuesta = oAlertaWS.AlertaNotificacionPorRenovacion(oAudit, tipoDoc, nroDoc, Msisdn, EquipoComprado, EquipoPrecio, TiempoContrato, idCaso, codigoRespuesta, mensajeRespuesta)
        Return respuesta
    End Function

    Public Function renovarPlanModem(ByVal msisdn As String, ByVal codigoPromocion As String, ByVal audit As ItemGenerico, ByRef mensajeRespuesta As String) As String

        Dim codRespuesta As String
        Try
            Dim oRenovacionModemPrepagoWS As New RenovacionModemPrepagoWS.ebsRenovacionModemPrepagoService
            oRenovacionModemPrepagoWS.Url = ConfigurationSettings.AppSettings("WSRenovacionPlanModem_url")
            oRenovacionModemPrepagoWS.Timeout = CInt(ConfigurationSettings.AppSettings("WSRenovacionPlanModem_timeout"))
            oRenovacionModemPrepagoWS.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim oRequest As New RenovacionModemPrepagoWS.renovarPlanModemRequest
            Dim oResponse As New RenovacionModemPrepagoWS.renovarPlanModemResponse

            oRequest.idTransaccion = audit.CODIGO
            oRequest.nombreAplicacion = audit.CODIGO2
            oRequest.usrAplicacion = audit.DESCRIPCION
            oRequest.ipAplicacion = audit.DESCRIPCION2
            oRequest.msisdn = msisdn
            oRequest.codigoPromocion = codigoPromocion

            oResponse = oRenovacionModemPrepagoWS.renovarPlanModem(oRequest)
            codRespuesta = oResponse.codigoRespuesta
            mensajeRespuesta = oResponse.mensajeRespuesta
        Catch ex As Exception
            mensajeRespuesta = ex.Message.ToString()
            codRespuesta = "-99"
        End Try

        Return codRespuesta
    End Function

    Public Function registrarMetricaVentas(ByVal oAudit As ItemGenerico, ByVal oCabecera As ArrayList, ByVal oDetalle As ArrayList) As String
        Dim codRespuesta As String
        Try
            Dim oRegMetrica As New RegistroMetricasVentaWS.RegistroMetricasVentasWSService
            oRegMetrica.Url = ConfigurationSettings.AppSettings("WSRegMetricaVenta_url").ToString()
            oRegMetrica.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("WSRegMetricaVenta_timeout").ToString())
            oRegMetrica.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim oReq As New RegistroMetricasVentaWS.registrarMetricaVentasRequest
            'auditoria
            Dim oReqAudit As New RegistroMetricasVentaWS.auditRequest
            oReqAudit.idTransaccion = oAudit.CODIGO
            oReqAudit.ipAplicacion = oAudit.DESCRIPCION2
            oReqAudit.aplicacion = oAudit.DESCRIPCION
            oReqAudit.usrAplicacion = oAudit.CODIGO2
            oReq.audit = oReqAudit

            'idmetrica
            oReq.idMetrica = ConfigurationSettings.AppSettings("WSIDRegMetricaVenta").ToString()
            'datos
            Dim oDatos As New RegistroMetricasVentaWS.ventaComplexType

            'cabecera
            Dim oHeader(oCabecera.Count - 1) As RegistroMetricasVentaWS.RequestOpcionalCabeceraComplexType
            Dim i As Integer = 0
            For Each item As ItemGenerico In oCabecera

                Dim obj As New RegistroMetricasVentaWS.RequestOpcionalCabeceraComplexType
                obj.clave = item.CODIGO
                obj.valor = item.DESCRIPCION
                oHeader(i) = obj
                i += 1
            Next

            oDatos.cuerpoMetrica.RequestOpcinalCabecera = oHeader

            Dim oDetail(oDetalle.Count - 1) As RegistroMetricasVentaWS.RequestOpcionalDetalleComplexType
            i = 0
            For Each item As ItemGenerico In oDetalle
                Dim obj As New RegistroMetricasVentaWS.RequestOpcionalDetalleComplexType
                obj.clave = item.Codigo
                obj.valor = item.Descripcion
                oDetail(i) = obj
                i += 1
            Next

            oDatos.cuerpoMetrica.detalle = oDetail

            oReq.datosMetrica = oDatos

            oRegMetrica.registrarMetricaVentas(oReq)
            codRespuesta = "1"
        Catch ex As Exception
            Throw ex
        End Try
        Return codRespuesta
    End Function

    Public Sub EjecutarActivacion(ByVal nroContrato As String, ByVal codTipoProducto As String, ByVal oAudit As ItemGenerico, ByRef codResp As String, ByRef mensaje As String)
        Dim oActivacion As New ActivosPostpagoConvergenteWS.ebsActivosPostpagoConvergente
        oActivacion.Url = ConfigurationSettings.AppSettings("WSActivosPostpagoConvergente_url")
        oActivacion.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("WSActivosPostpagoConvergente_timeout"))
        oActivacion.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim oRequest As New ActivosPostpagoConvergenteWS.EjecutarActivacionRequest
        oRequest.numeroAcuerdo = nroContrato
        oRequest.tipoProducto = codTipoProducto

        Dim auditoria As New ActivosPostpagoConvergenteWS.ParametrosAuditRequest
        auditoria.idTransaccion = oAudit.CODIGO
        auditoria.ipAplicacion = oAudit.CODIGO2
        auditoria.nombreAplicacion = oAudit.DESCRIPCION
        auditoria.usuarioAplicacion = oAudit.DESCRIPCION2

        oRequest.auditRequest = auditoria

        Dim listaRequestOpcional(0) As ActivosPostpagoConvergenteWS.ParametrosRequestObjetoRequestOpcional
        oRequest.listaRequestOpcional = listaRequestOpcional

        Dim oResponse As New ActivosPostpagoConvergenteWS.GenericoResponse
        oResponse = oActivacion.ejecutarActivacion(oRequest)
        codResp = oResponse.auditResponse.codigoRespuesta
        mensaje = oResponse.auditResponse.mensajeRespuesta
    End Sub
End Class
