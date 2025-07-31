Imports System.Configuration
Imports System.Net
Public Class BWGestionaPostventaProteccionMovil 'PROY-24724-IDEA-28174 - INICIO


    Dim oGestionaPostVentaProteccionMovil As New GestionaPostventaProteccionMovilWS.GestionaPostventaProteccionMovilWS


    Public Function RegistrarAltaProteccionMovil(ByVal strUsuario As String, _
                                                 ByVal strTerminal As String, _
                                                 ByVal strNroSEC As String, _
                                                 ByVal strNroTelefono As String, _
                                                 ByVal strNroCertificado As String, _
                                                 ByVal strEstadRptaServ As String, _
                                                 ByVal strIdCanje As String, _
                                                 ByRef strCodRpta As String, _
                                                 ByRef strMgsRpta As String)


        Dim objRequest = New GestionaPostventaProteccionMovilWS.registrarAltaAsurionRequest
        Dim objResponse = New GestionaPostventaProteccionMovilWS.registrarAltaAsurionResponse

        oGestionaPostVentaProteccionMovil.Url = ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_URL")
        oGestionaPostVentaProteccionMovil.Timeout = ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_TimeOut")

        Dim objAuditRequest As New GestionaPostventaProteccionMovilWS.auditRequestType
        objAuditRequest.idTransaccion = strNroSEC & DateTime.Now.ToString("yyyyMMddHHmmssfff")
        objAuditRequest.ipAplicacion = strTerminal
        objAuditRequest.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
        objAuditRequest.usuarioAplicacion = strUsuario

        objRequest.auditRequest = objAuditRequest
        objRequest.numSEC = strNroSEC
        objRequest.msisdn = strNroTelefono
        objRequest.numCertf = strNroCertificado
        objRequest.ipAplicacion = strTerminal
        objRequest.usuarioAplicacion = strUsuario
        objRequest.estadoRpta = strEstadRptaServ
        objRequest.idCanje = strIdCanje

        objResponse = oGestionaPostVentaProteccionMovil.registrarAltaAsurion(objRequest)

        strCodRpta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
        strMgsRpta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)

    End Function
    Public Function RegistrarTipificacion(ByVal strIdTransaccion As String, ByVal strIpAplicacion As String, ByVal strNombreaplicacion As String, _
                                          ByVal strUsuarioAplicacion As String, ByVal strUsrProceso As String, ByVal strAgente As String, _
                                          ByVal strNotas As String, ByVal strSubClase As String, ByVal strNroTelefono As String, ByVal strNroCertificado As String, _
                                          ByVal strFechaAfiliacion As String, ByVal strEstado As String, ByVal strTipoOperacion As String, _
                                          ByVal strDescEquipoActual As String, ByVal strImeiActual As String, ByVal strPrimaActual As String, _
                                          ByVal strDanioActual As String, ByVal strRoboActual As String, ByVal strDescEquipoAnterior As String, _
                                          ByVal strImeiAnterior As String, ByVal strPrimaAnterior As String, ByVal strDanioAnterior As String, _
                                          ByVal strRoboAnterior As String, ByVal strFechaCambio As String, ByVal strFechaCompraEquipo As String, _
                                          ByVal strFlagPlantilla As String, ByVal strCACEntrega As String, ByVal strNroSiniestro As String, _
                                          ByVal strTipoSiniestro As String, ByVal strFechaPago As String, ByVal strDevuelveEquipo As String, ByVal strDedudcible As String, ByVal strMotivoCancel As String, _   
                                          ByRef strIdTransaccionRpta As String, ByRef strCodRpta As String, ByRef strMsjRpta As String)   'PROY-31836 - Se agrego el parametro strMotivoCancel


        Dim objRequest = New GestionaPostventaProteccionMovilWS.tipificarProteccionMovilRequest
        Dim objResponse = New GestionaPostventaProteccionMovilWS.tipificarProteccionMovilResponse
        Dim objAuditRequest As New GestionaPostventaProteccionMovilWS.auditRequestType
        Dim objInteraccion = New GestionaPostventaProteccionMovilWS.interaccionType
        Dim objInteraccionPlus = New GestionaPostventaProteccionMovilWS.interaccionPlusType
        Dim objResponseAudit = New GestionaPostventaProteccionMovilWS.auditResponseType

        oGestionaPostVentaProteccionMovil.Url = ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_URL")
        oGestionaPostVentaProteccionMovil.Timeout = ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_TimeOut")


        objAuditRequest.idTransaccion = strIdTransaccion
        objAuditRequest.ipAplicacion = strIpAplicacion
        objAuditRequest.nombreAplicacion = strNombreaplicacion
        objAuditRequest.usuarioAplicacion = strUsuarioAplicacion

        objInteraccion.usrProceso = strUsrProceso
        objInteraccion.agente = strUsrProceso
        objInteraccion.notas = strNotas
        objInteraccion.subClase = strSubClase
        objInteraccion.telefono = strNroTelefono

        objInteraccionPlus.nroCertificado = strNroCertificado
        objInteraccionPlus.nroLinea = strNroTelefono
        objInteraccionPlus.fechAfiliacion = strFechaAfiliacion
        objInteraccionPlus.estado = strEstado
        objInteraccionPlus.tipoOperacion = strTipoOperacion
        objInteraccionPlus.equipo = strDescEquipoActual
        objInteraccionPlus.imei = strImeiActual
        objInteraccionPlus.montoPrima = strPrimaActual
        objInteraccionPlus.evalDanio = strDanioActual
        objInteraccionPlus.evalRobo = strRoboActual
        objInteraccionPlus.equipoAnterior = strDescEquipoAnterior
        objInteraccionPlus.imeiAnterior = strImeiAnterior
        objInteraccionPlus.primaAnterior = strPrimaAnterior
        objInteraccionPlus.evalDanioAnterior = strDanioAnterior
        objInteraccionPlus.evalRoboAnterior = strRoboAnterior
        objInteraccionPlus.fechCambio = strFechaCambio
        objInteraccionPlus.fechCompraEquipo = strFechaCompraEquipo
        objInteraccionPlus.flagPlantilla = strFlagPlantilla
        'PROY-24724 - Iteracion 2 Siniestros - INI
        objInteraccionPlus.cacEntrega = strCACEntrega
        objInteraccionPlus.nroSiniestro = strNroSiniestro
        objInteraccionPlus.tipoSiniestro = strTipoSiniestro
        objInteraccionPlus.fechPago = strFechaPago
        objInteraccionPlus.devuelveEquipo = strDevuelveEquipo
        objInteraccionPlus.deducible = strDedudcible
        'PROY-24724 - Iteracion 2 Siniestros - FIN
        objInteraccionPlus.motivoCancel = strMotivoCancel 'PROY-31836 IDEA-43582_Mejora de Procesos Postventa del servicio Proteccion Movil
        objRequest.auditRequest = objAuditRequest
        objRequest.interaccion = objInteraccion
        objRequest.interaccionPlus = objInteraccionPlus

        objResponse = oGestionaPostVentaProteccionMovil.tipificarProteccionMovil(objRequest)
        'PROY-24724 - Iteracion 2 Siniestros - INI
        strIdTransaccionRpta = objResponse.auditResponse.idTransaccion
        strCodRpta = objResponse.auditResponse.codigoRespuesta
        strMsjRpta = objResponse.auditResponse.mensajeRespuesta
        'PROY-24724 - Iteracion 2 Siniestros - FIN
    End Function
    'PROY-24724 - IIteracion 3 - INICIO
    Public Function ObtenerDatosProteccionMovil(ByVal strNroPedido As String, ByVal strIdTransaccion As String, ByVal strIpAplicacion As String, ByVal strNombreaplicacion As String, _
                                          ByVal strUsuarioAplicacion As String, ByRef strCodRpta As String, ByRef strMgsRpta As String, ByRef listaDatosProteccion As ArrayList)

        Dim objRequest = New GestionaPostventaProteccionMovilWS.obtenerInformacionPorPedidoRequest
        Dim objResponse = New GestionaPostventaProteccionMovilWS.obtenerInformacionPorPedidoResponse
        Dim objDatosProteccionList = New GestionaPostventaProteccionMovilWS.listaInformacionPorPedidoType
        oGestionaPostVentaProteccionMovil.Url = ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_URL")
        oGestionaPostVentaProteccionMovil.Timeout = ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_TimeOut")

        Dim objAuditRequest As New GestionaPostventaProteccionMovilWS.auditRequestType
        objAuditRequest.idTransaccion = strIdTransaccion
        objAuditRequest.ipAplicacion = strIpAplicacion
        objAuditRequest.nombreAplicacion = strNombreaplicacion
        objAuditRequest.usuarioAplicacion = strUsuarioAplicacion

        objRequest.auditRequest = objAuditRequest
        objRequest.numPedido = strNroPedido

        objResponse = oGestionaPostVentaProteccionMovil.obtenerInformacionPorPedido(objRequest)

        strCodRpta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
        strMgsRpta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)
        If strCodRpta = 0 Then
            objDatosProteccionList = objResponse.listaInformacionPorPedido

            For Each itemInformacionPedido As GestionaPostventaProteccionMovilWS.listaInformacionPorPedidoType In objResponse.listaInformacionPorPedido
                listaDatosProteccion.Add(itemInformacionPedido)
            Next
        End If


    End Function
    'PROY-24724 - IIteracion 3 - FIN
    'PROY-24724 - Iteracion 2 Siniestros  FIN
    Public Function MostrarDetalleSiniestro(ByVal strIdTransaccion As String, ByVal strTelefono As String, ByVal strEstado As String, ByVal strUsuario As String, _
                                            ByVal strTerminal As String, ByRef strCodRpta As String, ByRef strMgsRpta As String, ByRef lstDetalleSiniestro As ArrayList)


        Dim objRequest = New GestionaPostventaProteccionMovilWS.obtenerDetalleSiniestroRequest
        Dim objResponse = New GestionaPostventaProteccionMovilWS.obtenerDetalleSiniestroResponse
        Dim objDatosSiniestroList = New GestionaPostventaProteccionMovilWS.listaObtenerDetalleSiniestroType
        oGestionaPostVentaProteccionMovil.Url = ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_URL")
        oGestionaPostVentaProteccionMovil.Timeout = ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_TimeOut")

        Dim objAuditRequest As New GestionaPostventaProteccionMovilWS.auditRequestType
        objAuditRequest.idTransaccion = strIdTransaccion
        objAuditRequest.ipAplicacion = strTerminal
        objAuditRequest.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
        objAuditRequest.usuarioAplicacion = strUsuario

        objRequest.auditRequest = objAuditRequest
        objRequest.nroCertif = ""
        objRequest.customerId = ""
        objRequest.nroSiestro = ""
        objRequest.nroTelefono = strTelefono
        objRequest.estado = strEstado

        objResponse = oGestionaPostVentaProteccionMovil.obtenerDetalleSiniestro(objRequest)

        strCodRpta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
        strMgsRpta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)
        If strCodRpta = 0 Then
            objDatosSiniestroList = objResponse.listaObtenerDetalleSiniestro
            For Each itemDetalleSiniestro As GestionaPostventaProteccionMovilWS.listaObtenerDetalleSiniestroType In objResponse.listaObtenerDetalleSiniestro
                lstDetalleSiniestro.Add(itemDetalleSiniestro)
            Next
        End If
    End Function

    Public Function RegistrarAltaSiniestro(ByVal lstSiniestro As ArrayList, _
                                           ByVal strIdTransaccion As String, _
                                           ByVal strFlagSiniestro As String, _
                                           ByVal strEstado As String, _
                                           ByVal strTerminal As String, _
                                           ByVal strUsuario As String, _
                                           ByRef strCodRpta As String, _
                                           ByRef strMgsRpta As String)


        Dim objRequest = New GestionaPostventaProteccionMovilWS.registrarAltaSiniestroAsurionRequest
        Dim objResponse = New GestionaPostventaProteccionMovilWS.registrarAltaSiniestroAsurionResponse

        oGestionaPostVentaProteccionMovil.Url = ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_URL")
        oGestionaPostVentaProteccionMovil.Timeout = ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_TimeOut")

        Dim objAuditRequest As New GestionaPostventaProteccionMovilWS.auditRequestType
        objAuditRequest.idTransaccion = strIdTransaccion
        objAuditRequest.ipAplicacion = strTerminal
        objAuditRequest.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
        objAuditRequest.usuarioAplicacion = strUsuario

        objRequest.auditRequest = objAuditRequest
        objRequest.nroCertif = lstSiniestro(0).nroCertif
        objRequest.customerId = lstSiniestro(0).customerId
        objRequest.nroSiniestro = lstSiniestro(0).nroSiniestro
        objRequest.nroTelefono = lstSiniestro(0).nroTelefono
        objRequest.codMaterial = lstSiniestro(0).codigoNuevo
        objRequest.desMaterial = Funciones.CheckStr(lstSiniestro(0).marcEquiNuev) + " " + Funciones.CheckStr(lstSiniestro(0).modelEquiNuev)
        objRequest.imeiNuevo = lstSiniestro(0).imeiNuev
        objRequest.usuarioModif = strUsuario
        objRequest.flagSiniestro = strFlagSiniestro
        objRequest.estado = strEstado


        objResponse = oGestionaPostVentaProteccionMovil.registrarAltaSiniestroAsurion(objRequest)

        strCodRpta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
        strMgsRpta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)

    End Function

    Public Function ActualizarEstadoPagoSiniestro(ByVal lstSiniestro As ArrayList, _
                                                  ByVal strIdTransaccion As String, _
                                                  ByVal strEstado As String, _
                                                  ByVal strUsuario As String, _
                                                  ByVal strTerminal As String, _
                                                  ByRef strCodRpta As String, _
                                                  ByRef strMgsRpta As String)


        Dim objRequest = New GestionaPostventaProteccionMovilWS.actualizarEstadoPagoSiniestroRequest
        Dim objResponse = New GestionaPostventaProteccionMovilWS.actualizarEstadoPagoSiniestroResponse

        oGestionaPostVentaProteccionMovil.Url = ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_URL")
        oGestionaPostVentaProteccionMovil.Timeout = ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_TimeOut")

        Dim objAuditRequest As New GestionaPostventaProteccionMovilWS.auditRequestType
        objAuditRequest.idTransaccion = strIdTransaccion
        objAuditRequest.ipAplicacion = strTerminal
        objAuditRequest.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
        objAuditRequest.usuarioAplicacion = strUsuario


        objRequest.auditRequest = objAuditRequest
        objRequest.nroCertif = Funciones.CheckStr(lstSiniestro(0).nroCertif)
        objRequest.customerId = Funciones.CheckStr(lstSiniestro(0).customerId)
        objRequest.nroSiniestro = Funciones.CheckStr(lstSiniestro(0).nroSiniestro)
        objRequest.estado = strEstado
        objRequest.usuarioModif = strUsuario

        objResponse = oGestionaPostVentaProteccionMovil.actualizarEstadoPagoSiniestro(objRequest)

        strCodRpta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
        strMgsRpta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)

    End Function
    Public Function BuscarCanjeVentaIndividual(ByVal strIdTransaccion As String, ByVal strNroPedidoPM As String, _
                                             ByVal strTerminal As String, ByVal strUsuario As String, ByRef strCodRpta As String, _
                                           ByRef strMgsRpta As String, ByRef lstDatosCanjeVI As ArrayList)


        Dim objRequest = New GestionaPostventaProteccionMovilWS.buscarSeguroCanjeRequest
        Dim objResponse = New GestionaPostventaProteccionMovilWS.buscarSeguroCanjeResponse
        Dim objCanjeVIList = New GestionaPostventaProteccionMovilWS.SeguroCanjeType
        oGestionaPostVentaProteccionMovil.Url = ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_URL")
        oGestionaPostVentaProteccionMovil.Timeout = ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_TimeOut")

        Dim objAuditRequest As New GestionaPostventaProteccionMovilWS.auditRequestType
        objAuditRequest.idTransaccion = strIdTransaccion
        objAuditRequest.ipAplicacion = strTerminal
        objAuditRequest.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
        objAuditRequest.usuarioAplicacion = strUsuario

        objRequest.auditRequest = objAuditRequest
        objRequest.nroPedidoOri = ""
        objRequest.nroPedidoPM = strNroPedidoPM
        objRequest.idCanje = ""

        objResponse = oGestionaPostVentaProteccionMovil.buscarSeguroCanje(objRequest)

        strCodRpta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
        strMgsRpta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)
        If strCodRpta = 0 Then
            objCanjeVIList = objResponse.listaSeguroCanje
            For Each itemDetalleSiniestro As GestionaPostventaProteccionMovilWS.SeguroCanjeType In objResponse.listaSeguroCanje
                lstDatosCanjeVI.Add(itemDetalleSiniestro)
            Next
        End If
    End Function
    'PROY-24724 - Iteracion 2 Siniestros  FIN
End Class
'PROY-24724-IDEA-28174 - FIN