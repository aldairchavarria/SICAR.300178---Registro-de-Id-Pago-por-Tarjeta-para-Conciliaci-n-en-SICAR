Imports System.Configuration
Imports System.Net
Imports System.Data

Public Class BWEnvioTransacPOS
  Dim oEnvioTransacPagosPosWS As New EnvioTransacPagosPosWS.EnvioTransacPagosPOSService

  Public Sub RegistrarTransaction(ByVal objTran As BeEnvioTransacPOS, _
  ByRef strCodRpta As String, ByRef strMgsRpta As String)

    Dim objRequest = New EnvioTransacPagosPosWS.guardarTransacRequest
    Dim objResponse = New EnvioTransacPagosPosWS.guardarTransacResponse
    Dim strIdTransaccion As String = Funciones.CheckStr(objTran.nroTelefono) & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff")
    Dim strIpAplicacion As String = Funciones.CheckStr(objTran.ipServidor)
    Dim strNombreAplicacion As String = ConfigurationSettings.AppSettings("constAplicacion")
    Dim strUsuarioAplicacion As String = Funciones.CheckStr(objTran.UserAplicacion)

    oEnvioTransacPagosPosWS.Url = ConfigurationSettings.AppSettings("consEnvioTransacPagosPosWS_URL")
    oEnvioTransacPagosPosWS.Timeout = ConfigurationSettings.AppSettings("consEnvioTransacPagosPosWS_TimeOut")

        Dim objAuditRequest As New EnvioTransacPagosPosWS.auditRequestType

        Try

      
        objAuditRequest.idTransaccion = strIdTransaccion
        objAuditRequest.ipAplicacion = strIpAplicacion
        objAuditRequest.nombreAplicacion = strNombreAplicacion
        objAuditRequest.usuarioAplicacion = strUsuarioAplicacion


        objRequest.auditRequest = objAuditRequest

        'Parametros cabecera'
        objRequest.trnsnIdCab = objTran.idCabecera
        objRequest.trnsvNumpedido = objTran.numPedido
        objRequest.codPdv = objTran.codVenta
        objRequest.posvNrotienda = objTran.nroTienda
        objRequest.posvNrocaja = objTran.nroCaja
        objRequest.posvIdestablec = objTran.codEstablecimiento
        objRequest.posvIpcaja = objTran.ipCaja
        objRequest.usuavCajero = objTran.codCajero
        objRequest.usuavAnulador = objTran.codAnulador
        objRequest.posvCodTerminal = objTran.numSeriePos
        objRequest.posvCodEquipo = objTran.nombreEquipoPos
        objRequest.posvIpCliente = objTran.ipCliente
        objRequest.posvIpServidor = objTran.ipServidor
        objRequest.posvNombreCliente = objTran.nombrePcCliente
        objRequest.posvNombreServidor = objTran.nombrePcServidor

        'Parametros Detalle'

        objRequest.trnsvTransaccionPos = objTran.idTransaccionPos
        objRequest.trnsvNroReferencia = objTran.nroReferencia
        objRequest.trnsvNroAprobacion = objTran.nroAprobacion
        objRequest.trnscOperacionId = objTran.codOperacion
        objRequest.trnsvDesOperacion = objTran.desOperacion
        objRequest.trnscTipoOperacion = objTran.tipoOperacion
        objRequest.trnsnMonto = objTran.montoOperacion
        objRequest.idConftipMoneda = objTran.monedaOperacion
        objRequest.trnsdFecTrans = objTran.fechaTransaccion
        objRequest.trnsvNroTarjeta = objTran.nroTarjeta
        objRequest.trnsvFecExpiracion = objTran.fecExpiracion
        objRequest.trnscObsAnulacion = objTran.obsAnulacion
        objRequest.trnsvCliente = objTran.nombreCliente
        objRequest.trnsnIdAnulacion = objTran.idAnulacion
        objRequest.trnscTipoTarjeta = objTran.tipoTarjeta
        objRequest.trnsvVoucher = objTran.impresionVoucher
        objRequest.trnsvUsuario = objTran.usuarioRed
        objRequest.trnsvTipoPago = objTran.tipoPago
        objRequest.trnsvEstado = objTran.estadoTransaccion
        objRequest.trnsvRespTrans = objTran.codRespTransaccion
        objRequest.trnsvCodAprob = objTran.codAprobTransaccion
        objRequest.trnsvResultTrans = objTran.descTransaccion
        objRequest.trnsvIdRef = objTran.numVoucher
        'objRequest.seriePOS = objTran.numSeriePos
        'objRequest.equipoPOS = objTran.nombreEquipoPos
        objRequest.trnsnIdAutoriz = objTran.numTransaccion
        objRequest.trnsvTipoTarjetaPos = objTran.tipoPos
        objRequest.trnsvTipoTrans = objTran.tipoTransaccion
        objRequest.fechaTransaccionPos = objTran.fechaTransaccionPos
        objRequest.horaTransaccionPos = objTran.horaTransaccionPos
        objRequest.posnId = objTran.nroRegistro
        objRequest.trnscObsAnulacion = objTran.obsAnulacion



        objResponse = oEnvioTransacPagosPosWS.guardarTransac(objRequest)

        strCodRpta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)


        Dim strTransId As String = Funciones.CheckStr(objResponse.codTransaccion)
        Dim strCodCabez As String = Funciones.CheckStr(objResponse.codTransaccionCab)

            strMgsRpta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta) & "|" _
            & strTransId & "|" & strCodCabez

        Catch ex As Exception
            strCodRpta = -99
            strMgsRpta = ex.Message.ToString()

        End Try

  End Sub

  Public Sub ActualizarTransaction(ByVal objTran As BeEnvioTransacPOS, _
  ByRef strCodRpta As String, ByRef strMgsRpta As String)

    Dim objRequest = New EnvioTransacPagosPosWS.actualizarTransacRequest
    Dim objResponse = New EnvioTransacPagosPosWS.actualizarTransacResponse
    Dim strIdTransaccion As String = Funciones.CheckStr(objTran.TransId) & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff")
    Dim strIpAplicacion As String = Funciones.CheckStr(objTran.ipServidor)
    Dim strNombreAplicacion As String = ConfigurationSettings.AppSettings("constAplicacion")
    Dim strUsuarioAplicacion As String = Funciones.CheckStr(objTran.UserAplicacion)

    oEnvioTransacPagosPosWS.Url = ConfigurationSettings.AppSettings("consEnvioTransacPagosPosWS_URL")
    oEnvioTransacPagosPosWS.Timeout = ConfigurationSettings.AppSettings("consEnvioTransacPagosPosWS_TimeOut")

        Dim objAuditRequest As New EnvioTransacPagosPosWS.auditRequestType

        Try

            objAuditRequest.idTransaccion = strIdTransaccion
            objAuditRequest.ipAplicacion = strIpAplicacion
            objAuditRequest.nombreAplicacion = strNombreAplicacion
            objAuditRequest.usuarioAplicacion = strUsuarioAplicacion

            objRequest.auditRequest = objAuditRequest

            objRequest.flagPago = objTran.FlagPago
            objRequest.trnsnIdCab = Funciones.CheckStr(objTran.idCabecera)
            objRequest.trnsvNumpedido = Funciones.CheckStr(objTran.numPedido)
            objRequest.trnsvEstado = objTran.estadoTransaccion
            objRequest.trnsnId = objTran.TransId
            objRequest.trnsvTransaccionPos = objTran.IdTransPos
            objRequest.idConftipMoneda = objTran.monedaOperacion
            objRequest.trnsnMonto = objTran.montoOperacion
            objRequest.posnId = objTran.nroRegistro
            objRequest.trnsvIdRef = objTran.numVoucher
            objRequest.trnsnIdAutoriz = objTran.numTransaccion
            objRequest.trnsvRespTrans = objTran.codRespTransaccion
            objRequest.trnsvResultTrans = objTran.descTransaccion
            objRequest.trnsvCodAprob = objTran.codAprobTransaccion
            objRequest.trnsvTipoTarjetaPos = objTran.tipoPos
            objRequest.trnsvNroTarjeta = objTran.nroTarjeta
            objRequest.fechaTransaccionPos = objTran.fechaTransaccionPos
            objRequest.horaTransaccionPos = objTran.horaTransaccionPos
            objRequest.trnsvFecExpiracion = objTran.fecExpiracion
            objRequest.trnsvCliente = objTran.nombreCliente
            objRequest.trnsvVoucher = objTran.impresionVoucher
            objRequest.usuavAnulador = objTran.codAnulador
            objRequest.posvCodTerminal = objTran.numSeriePos
            objRequest.trnsvIdRefAnul = objTran.IdRefAnu
            'objRequest.equipoPOS = objTran.nombreEquipoPos


            objResponse = oEnvioTransacPagosPosWS.actualizarTransac(objRequest)

            strCodRpta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
            strMgsRpta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)

        Catch ex As Exception
            strCodRpta = -99
            strMgsRpta = ex.Message.ToString()

        End Try
  End Sub


    Public Sub GuardarTransactionAperturaCierre(ByVal objTran As BeEnvioTransacPOS, _
    ByRef strCodRpta As String, ByRef strMgsRpta As String)

        Try
        Dim objRequest = New EnvioTransacPagosPosWS.guardarTransacAperturaCierreRequest
        Dim objResponse = New EnvioTransacPagosPosWS.guardarTransacAperturaCierreResponse
        Dim strIdTransaccion As String = Funciones.CheckStr(objTran.TransId) & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff")
        Dim strIpAplicacion As String = Funciones.CheckStr(objTran.ipServidor)
        Dim strNombreAplicacion As String = ConfigurationSettings.AppSettings("constAplicacion")
        Dim strUsuarioAplicacion As String = Funciones.CheckStr(objTran.UserAplicacion)

        oEnvioTransacPagosPosWS.Url = ConfigurationSettings.AppSettings("consEnvioTransacPagosPosWS_URL")
        oEnvioTransacPagosPosWS.Timeout = ConfigurationSettings.AppSettings("consEnvioTransacPagosPosWS_TimeOut")

        Dim objAuditRequest As New EnvioTransacPagosPosWS.auditRequestType
        objAuditRequest.idTransaccion = strIdTransaccion
        objAuditRequest.ipAplicacion = strIpAplicacion
        objAuditRequest.nombreAplicacion = strNombreAplicacion
        objAuditRequest.usuarioAplicacion = strUsuarioAplicacion

        objRequest.auditRequest = objAuditRequest

        objRequest.nroRegistro = objTran.nroRegistro
        objRequest.tipoMovimiento = objTran.tipoOperacion
        objRequest.fechaMovimiento = objTran.fechaTransaccion
        objRequest.tipoEstado = objTran.estadoTransaccion
        objRequest.detalleError = objTran.descTransaccion
        objRequest.usrRegistro = objTran.codCajero
        objRequest.fechaRegistro = objTran.fechaTransaccion


        objResponse = oEnvioTransacPagosPosWS.guardarTransacAperturaCierre(objRequest)

        strCodRpta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
        strMgsRpta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)

        Catch ex As Exception

            strCodRpta = -99
            strMgsRpta = ex.Message.ToString()

        End Try

    End Sub

    Public Sub ConsultaDetalleReporte(ByVal objTran As BeEnvioTransacPOS, _
                                     ByRef strFechIni As String, ByRef strFechFin As String, ByRef strCodRpta As String, ByRef strMgsRpta As String, ByRef listaResponse As ArrayList)

        Dim objRequest = New EnvioTransacPagosPosWS.detalleReporteTransacRequest
        Dim objResponse = New EnvioTransacPagosPosWS.detalleReporteTransacResponse
        Dim strIdTransaccion As String = Funciones.CheckStr(objTran.TransId) & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff")
        Dim strIpAplicacion As String = Funciones.CheckStr(objTran.ipServidor)
        Dim strNombreAplicacion As String = ConfigurationSettings.AppSettings("constAplicacion")
        Dim strUsuarioAplicacion As String = Funciones.CheckStr(objTran.UserAplicacion)


        Dim objReporteTransacList = New EnvioTransacPagosPosWS.ReporteDetType
        Try


            oEnvioTransacPagosPosWS.Url = ConfigurationSettings.AppSettings("consEnvioTransacPagosPosWS_URL")
            oEnvioTransacPagosPosWS.Timeout = ConfigurationSettings.AppSettings("consEnvioTransacPagosPosWS_TimeOut")

            Dim objAuditRequest As New EnvioTransacPagosPosWS.auditRequestType
            objAuditRequest.idTransaccion = strIdTransaccion
            objAuditRequest.ipAplicacion = strIpAplicacion
            objAuditRequest.nombreAplicacion = strNombreAplicacion
            objAuditRequest.usuarioAplicacion = strUsuarioAplicacion

            objRequest.auditRequest = objAuditRequest

            objRequest.codPtoVenta = objTran.codVenta
            objRequest.tipoTarjeta = objTran.tipoTarjeta
            objRequest.nroCaja = objTran.codEstablecimiento
            objRequest.fecIniTransaccion = strFechIni
            objRequest.fecFinTransaccion = strFechFin
            objRequest.codCajero = objTran.codCajero
            objRequest.tipoTransaccion = objTran.tipoTransaccion
            objRequest.codOperacion = objTran.codOperacion
            objRequest.estadoTransaccion = objTran.estadoTransaccion
            objRequest.idRef = objTran.numVoucher

            objResponse = oEnvioTransacPagosPosWS.detalleReporteTransac(objRequest)


            strCodRpta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
            strMgsRpta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)


            If strCodRpta <> 0 Or objResponse.listaReporteDet Is Nothing Then
                listaResponse = Nothing
            Else
                objReporteTransacList = objResponse.listaReporteDet

                For Each itemTransaccion As EnvioTransacPagosPosWS.ReporteDetType In objResponse.listaReporteDet
                    listaResponse.Add(itemTransaccion)
                Next
            End If

        Catch ex As Exception

            listaResponse = Nothing
            strCodRpta = -99
            strMgsRpta = ex.Message.ToString()

        End Try


    End Sub


    'detaPagoRecTransac
    Public Function ConsultaDetallePagoRec(ByVal objTran As BeEnvioTransacPOS, ByRef strCodRpta As String, _
    ByRef strMgsRpta As String, ByRef listaResponse As ArrayList)

        Dim ds As DataSet
        Dim objRequest = New EnvioTransacPagosPosWS.detaPagoRecTransacRequest
        Dim objResponse = New EnvioTransacPagosPosWS.detaPagoRecTransacResponse



        Dim objReporteTransacList = New EnvioTransacPagosPosWS.DetaPagoRecTransacType 'ReporteDetType
        Try


            oEnvioTransacPagosPosWS.Url = ConfigurationSettings.AppSettings("consEnvioTransacPagosPosWS_URL")
            oEnvioTransacPagosPosWS.Timeout = ConfigurationSettings.AppSettings("consEnvioTransacPagosPosWS_TimeOut")


            Dim strIdTransaccion As String = Funciones.CheckStr(objTran.TransId) & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff")
            Dim strIpAplicacion As String = Funciones.CheckStr(objTran.ipServidor)
            Dim strNombreAplicacion As String = ConfigurationSettings.AppSettings("constAplicacion")
            Dim strUsuarioAplicacion As String = Funciones.CheckStr(objTran.UserAplicacion)

            Dim objAuditRequest As New EnvioTransacPagosPosWS.auditRequestType
            objAuditRequest.idTransaccion = strIdTransaccion
            objAuditRequest.ipAplicacion = strIpAplicacion
            objAuditRequest.nombreAplicacion = strNombreAplicacion
            objAuditRequest.usuarioAplicacion = strUsuarioAplicacion

            objRequest.auditRequest = objAuditRequest

            objRequest.pedido = objTran.numPedido
            objRequest.monto = ""
            objRequest.tipoTarjeta = ""
            objRequest.tipoPago = objTran.tipoPago
            objRequest.fechaPago = objTran.fechaTransaccionPos
            objRequest.codPdv = objTran.codVenta
            objRequest.numeroCaja = objTran.nroCaja

            objResponse = oEnvioTransacPagosPosWS.detaPagoRecTransac(objRequest)

            strCodRpta = Funciones.CheckStr(objResponse.auditResponse.codigoRespuesta)
            strMgsRpta = Funciones.CheckStr(objResponse.auditResponse.mensajeRespuesta)


            If strCodRpta <> 0 Or objResponse.listaDetaPagoRecTransac Is Nothing Then
                listaResponse = Nothing
            Else
                objReporteTransacList = objResponse.listaDetaPagoRecTransac

                For Each itemTransaccion As EnvioTransacPagosPosWS.DetaPagoRecTransacType In objResponse.listaDetaPagoRecTransac
                    listaResponse.Add(itemTransaccion)
                Next
            End If


        Catch ex As Exception

            listaResponse = Nothing
            strCodRpta = -99
            strMgsRpta = ex.Message.ToString()

        End Try

    End Function

End Class
