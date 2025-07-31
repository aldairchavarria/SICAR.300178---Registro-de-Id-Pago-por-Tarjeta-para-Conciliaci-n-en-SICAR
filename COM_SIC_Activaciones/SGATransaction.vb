Imports System.Configuration
Public Class SGATransaction

    Dim oTransaccion As New SGATransactionWS.TransaccionVentaService

    Public Sub New()
        oTransaccion.Url = Funciones.CheckStr(ConfigurationSettings.AppSettings("constSGATransaccion_Url"))
        oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
        oTransaccion.Timeout = Funciones.CheckInt64(ConfigurationSettings.AppSettings("constSGATransaccion_Timeout"))
    End Sub

    Public Function GenerarSot(ByVal cadenaRegVenta As String, ByVal cadenaServicio As String, ByVal cadenaPromocion As String, ByVal oAudit As ItemGenerico) As SGAResponseVenta

        Dim oSGAResponseVenta As New SGAResponseVenta
        Dim oRequest As New SGATransactionWS.procesarGeneracionProyRequest
        Dim oResponse As New SGATransactionWS.procesarGeneracionProyResponse
        Dim oRegVenta_Out As New SGATransactionWS.RegVenta_OutType
        Dim oAuditResponse As New SGATransactionWS.auditTypeResponse

        Dim x As Integer = 0

        Dim oRegVenta As New SGATransactionWS.RegVentaType
        If Len(cadenaRegVenta) > 0 Then
            Dim arrRegVenta() As String = cadenaRegVenta.Split(";"c)
            oRegVenta.idprocess = Funciones.CheckInt64(arrRegVenta(0))
            oRegVenta.idnegocio = Funciones.CheckInt64(arrRegVenta(1))
            oRegVenta.codcli = arrRegVenta(2)
            oRegVenta.tipdide = arrRegVenta(3)
            oRegVenta.ntdide = arrRegVenta(4)
            oRegVenta.apepat = arrRegVenta(5)
            oRegVenta.apemat = arrRegVenta(6)
            oRegVenta.nomcli = arrRegVenta(7)
            oRegVenta.nomabr = arrRegVenta(8)
            oRegVenta.codprvf1 = arrRegVenta(9)
            oRegVenta.telefonof1 = arrRegVenta(10)
            oRegVenta.codprvf2 = arrRegVenta(11)
            oRegVenta.telefonof2 = arrRegVenta(12)
            oRegVenta.codprvm1 = arrRegVenta(13)
            oRegVenta.telefonom1 = arrRegVenta(14)
            oRegVenta.codprvm2 = arrRegVenta(15)
            oRegVenta.telefonom2 = arrRegVenta(16)
            oRegVenta.mail = arrRegVenta(17)
            oRegVenta.codsucins = arrRegVenta(18)
            oRegVenta.tipvia_i = arrRegVenta(19)
            oRegVenta.nomvia_i = arrRegVenta(20)
            oRegVenta.nrovia_i = arrRegVenta(21)
            oRegVenta.tipourb_i = Funciones.CheckInt64(arrRegVenta(22))
            oRegVenta.nomurb_i = arrRegVenta(23)
            oRegVenta.manzana_i = arrRegVenta(24)
            oRegVenta.lote_i = arrRegVenta(25)
            oRegVenta.codubi_i = arrRegVenta(26)
            oRegVenta.referencia_i = arrRegVenta(27)
            oRegVenta.codsucfac = arrRegVenta(28)
            oRegVenta.tipvia_f = arrRegVenta(29)
            oRegVenta.nomvia_f = arrRegVenta(30)
            oRegVenta.nrovia_f = arrRegVenta(31)
            oRegVenta.tipourb_f = Funciones.CheckInt64(arrRegVenta(32))
            oRegVenta.nomurb_f = arrRegVenta(33)
            oRegVenta.manzana_f = arrRegVenta(34)
            oRegVenta.lote_f = arrRegVenta(35)
            oRegVenta.codubi_f = arrRegVenta(36)
            oRegVenta.referencia_f = arrRegVenta(37)
            oRegVenta.codcnt = arrRegVenta(38)
            oRegVenta.apepatcnt = arrRegVenta(39)
            oRegVenta.apematcnt = arrRegVenta(40)
            oRegVenta.nomcnt = arrRegVenta(41)
            oRegVenta.codsolot = Funciones.CheckInt64(arrRegVenta(42))
            oRegVenta.usuregistro = arrRegVenta(43)
            oRegVenta.fecregistro = Funciones.CheckDate(arrRegVenta(44))
            oRegVenta.codcanalvta = arrRegVenta(45)
            oRegVenta.codmotivoanu = Funciones.CheckInt64(arrRegVenta(46))
            oRegVenta.codsupvta = arrRegVenta(47)
            oRegVenta.codcon = Funciones.CheckInt64(arrRegVenta(48))
            oRegVenta.tipo = arrRegVenta(49)
            oRegVenta.tipdide_ect = arrRegVenta(50)
            oRegVenta.ntdide_ect = arrRegVenta(51)
            oRegVenta.nomect = arrRegVenta(52)
            oRegVenta.idpaq = Funciones.CheckInt64(arrRegVenta(53))
            oRegVenta.nrocontrato = arrRegVenta(54)
            oRegVenta.fecins = Funciones.CheckDate(arrRegVenta(55))
            oRegVenta.flg_recarga = arrRegVenta(56)
            oRegVenta.tipopago = arrRegVenta(57)
            oRegVenta.datopago = arrRegVenta(58)
            oRegVenta.datorecarga = arrRegVenta(59)
            oRegVenta.mail_afil = arrRegVenta(60)
            oRegVenta.carta = Funciones.CheckInt64(arrRegVenta(61))
            oRegVenta.ope_id = arrRegVenta(62)
            oRegVenta.flg_presus = Funciones.CheckInt64(arrRegVenta(63))
            oRegVenta.flg_publicar = Funciones.CheckInt64(arrRegVenta(64))
            oRegVenta.idplano = arrRegVenta(65)
            oRegVenta.usureg = arrRegVenta(66)
            oRegVenta.fecreg = Funciones.CheckDate(arrRegVenta(67))
            oRegVenta.usumod = arrRegVenta(68)
            oRegVenta.fecmod = Funciones.CheckDate(arrRegVenta(69))
        End If

        Dim oListaServicio(0) As SGATransactionWS.ServicioType
        Dim i As Integer
        If Len(cadenaServicio) > 0 Then
            Dim arrServicios() As String = cadenaServicio.Split("|"c)
            ReDim oListaServicio(arrServicios.Length)
            For i = 0 To arrServicios.Length - 1
                Dim oServicio As New SGATransactionWS.ServicioType
                Dim arrServicio() As String = arrServicios(i).Split(";"c)
                oServicio.numsec = Funciones.CheckInt64(arrServicio(0))
                oServicio.iddet = Funciones.CheckInt64(arrServicio(1))
                If arrServicio(2) <> "" Then
                    oServicio.idlinea = Funciones.CheckInt64(arrServicio(2))
                End If
                If arrServicio(3) <> "" Then
                    oServicio.coequipo = arrServicio(3)
                End If
                oServicio.cantidad = Funciones.CheckInt64(arrServicio(4))
                oListaServicio(i) = oServicio
            Next
        End If

        Dim oListaPromocion(0) As SGATransactionWS.PromocionType
        If Len(cadenaPromocion) > 0 Then
            Dim arrPromociones() As String = cadenaPromocion.Split("|"c)
            ReDim oListaPromocion(arrPromociones.Length)
            For i = 0 To arrPromociones.Length - 1
                Dim oPromocion As New SGATransactionWS.PromocionType
                Dim arrPromocion() As String = arrPromociones(i).Split(";"c)
                oPromocion.sumsec = Funciones.CheckInt64(arrPromocion(0))
                oPromocion.iddet = Funciones.CheckInt64(arrPromocion(1))
                oPromocion.idprom = Funciones.CheckInt64(arrPromocion(2))
                oListaPromocion(i) = oPromocion
            Next
        End If

        Dim audit As New SGATransactionWS.auditType
        audit.idTransaccion = oAudit.CODIGO
        audit.aplicacion = oAudit.DESCRIPCION
        audit.ipAplicacion = oAudit.DESCRIPCION2
        Dim oListaLicencias() As SGATransactionWS.CantLicenciaType

        oRequest.auditType = audit
        oRequest.objeto_RegVentaType = oRegVenta
        oRequest.lista_ServicioType = oListaServicio
        oRequest.lista_PromocionType = oListaPromocion
        oRequest.lista_CantLicenciaType = oListaLicencias

        oResponse = oTransaccion.procesarGeneracionProy(oRequest)

        oRegVenta_Out = oResponse.objeto_RegVenta_OutType
        oAuditResponse = oResponse.audit

        oSGAResponseVenta.codRepuesta = oAuditResponse.codigoRespuesta
        oSGAResponseVenta.msgRepuesta = oAuditResponse.mensajeRespuesta

        If oSGAResponseVenta.codRepuesta = "0" Then
            oSGAResponseVenta.codcli = oRegVenta_Out.codcli
            oSGAResponseVenta.codsucins = oRegVenta_Out.codsucins
            oSGAResponseVenta.codsucfac = oRegVenta_Out.codsucfac
            oSGAResponseVenta.codect = oRegVenta_Out.codect
            oSGAResponseVenta.numslc = oRegVenta_Out.numslc
            oSGAResponseVenta.codsolot = oRegVenta_Out.codsolot
            oSGAResponseVenta.tiptra = oRegVenta_Out.tiptra
            oSGAResponseVenta.estsol = oRegVenta_Out.estsol
            oSGAResponseVenta.fecagenda = oRegVenta_Out.fecagenda
            oSGAResponseVenta.hora = oRegVenta_Out.hora
            oSGAResponseVenta.codcon = oRegVenta_Out.codcon
            oSGAResponseVenta.codcuadri = oRegVenta_Out.codcuadri
        End If

        Return oSGAResponseVenta

    End Function


    Public Function ObtenerCadenaRegVentaSGA(ByVal oVenta As SGAVenta) As String
        Dim arrCadena(69) As String
        arrCadena(0) = oVenta.idprocess
        arrCadena(1) = oVenta.idnegocio
        arrCadena(2) = oVenta.codcliente
        arrCadena(3) = oVenta.tipoDocCliente
        arrCadena(4) = oVenta.nroDocCliente
        arrCadena(5) = oVenta.apePaterno
        arrCadena(6) = oVenta.apeMaterno
        arrCadena(7) = oVenta.nombreCliente.Trim()
        arrCadena(8) = oVenta.nombreComercial
        arrCadena(9) = oVenta.codTelef1
        arrCadena(10) = oVenta.nroTelefono1
        arrCadena(11) = oVenta.codTelef2
        arrCadena(12) = oVenta.nroTelefono2
        arrCadena(13) = oVenta.codTelefMovil1
        arrCadena(14) = oVenta.nroTelefMovil1
        arrCadena(15) = oVenta.codTelefMovil2
        arrCadena(16) = oVenta.nroTelefMovil2
        arrCadena(17) = oVenta.correo
        arrCadena(18) = oVenta.codSucursalInst
        arrCadena(19) = oVenta.tipoViaInst
        arrCadena(20) = oVenta.nombreViaInst
        arrCadena(21) = oVenta.nroViaInst
        arrCadena(22) = oVenta.tipoUrbInst
        arrCadena(23) = oVenta.nombreUrbInst
        arrCadena(24) = oVenta.manzazaInst
        arrCadena(25) = oVenta.loteInst
        arrCadena(26) = oVenta.ubigeoInst
        arrCadena(27) = oVenta.referenciaInst
        arrCadena(28) = oVenta.codSucursalFact
        arrCadena(29) = oVenta.tipoViaFact
        arrCadena(30) = oVenta.nombreViaFact
        arrCadena(31) = oVenta.nroViaFact
        arrCadena(32) = oVenta.tipoUrbFact
        arrCadena(33) = oVenta.nombreUrbFact
        arrCadena(34) = oVenta.manzazaFact
        arrCadena(35) = oVenta.loteFact
        arrCadena(36) = oVenta.ubigeoFact
        arrCadena(37) = oVenta.referenciaFact
        arrCadena(38) = oVenta.codContacto
        arrCadena(39) = oVenta.apePaternoCont
        arrCadena(40) = oVenta.apeMaternoCont
        arrCadena(41) = oVenta.nombreContacto
        arrCadena(42) = oVenta.codSolot
        arrCadena(43) = oVenta.usuarioRegistro
        arrCadena(44) = oVenta.fechaRegistro
        arrCadena(45) = oVenta.codCanalVenta
        arrCadena(46) = oVenta.codMotivoAnulacion
        arrCadena(47) = oVenta.codSupervisor
        arrCadena(48) = oVenta.codCon
        arrCadena(49) = oVenta.tipoSupervisor
        arrCadena(50) = oVenta.tipoDocVendedor
        arrCadena(51) = oVenta.nroDocVendedor
        arrCadena(52) = oVenta.nombreCompletoVendedor
        arrCadena(53) = oVenta.idPaq
        arrCadena(54) = oVenta.nroContrato
        arrCadena(55) = oVenta.fechaInstalacion
        arrCadena(56) = oVenta.flagRecarga
        arrCadena(57) = oVenta.tipoPago
        arrCadena(58) = oVenta.datoPago
        arrCadena(59) = oVenta.datoRecarga
        arrCadena(60) = oVenta.correoAfiliacion
        arrCadena(61) = oVenta.nroCartaPreSeleccion
        arrCadena(62) = oVenta.codOperadorLD
        arrCadena(63) = oVenta.flag_Presuscrito
        arrCadena(64) = oVenta.flag_Publicar
        arrCadena(65) = oVenta.idPlano
        arrCadena(66) = oVenta.usuarioRegistro
        arrCadena(67) = oVenta.fechaRegistro
        arrCadena(68) = oVenta.usuarioRegistro 'oVenta.usuModifica
        arrCadena(69) = oVenta.fechaRegistro 'oVenta.fechaModifica

        Return Join(arrCadena, ";")
    End Function

    Public Function ObtenerCadenaServicioSGA(ByVal oListaServicios As ArrayList) As String
        If oListaServicios.Count = 0 Then
            Return ""
        End If
        Dim cadena As String
        For Each oServicio As SGAServicio In oListaServicios
            cadena &= "|" & oServicio.numsec
            cadena &= ";" & oServicio.iddet
            cadena &= ";" & oServicio.idlinea
            cadena &= ";" & oServicio.coequipo
            cadena &= ";" & oServicio.cantidad
        Next
        Return Funciones.CheckStr(IIf(cadena = "", "", cadena.Substring(1)))
    End Function

    Public Function ObtenerCadenaPromocionSGA(ByVal oListaPromocion As ArrayList) As String
        If oListaPromocion.Count = 0 Then
            Return ""
        End If
        Dim cadena As String
        For Each oPromocion As SGAPromocion In oListaPromocion
            cadena &= "|" & oPromocion.numsec
            cadena &= ";" & oPromocion.iddet
            cadena &= ";" & oPromocion.idprom
        Next
        Return Funciones.CheckStr(IIf(cadena = "", "", cadena.Substring(1)))
    End Function

    Public Function AnularSot(ByVal nroSot As String, ByVal observacion As String, ByVal oAudit As ItemGenerico) As SGAResponseVenta
        Dim oSGAResponseVenta As New SGAResponseVenta
        Dim audit As New SGATransactionWS.auditType
        audit.idTransaccion = oAudit.CODIGO
        audit.aplicacion = oAudit.DESCRIPCION
        audit.ipAplicacion = oAudit.DESCRIPCION2
        Dim oListaLicencias() As SGATransactionWS.CantLicenciaType

        Dim oRequest As New SGATransactionWS.anularSotRequest
        Dim oResponse As New SGATransactionWS.anularSotResponse
        Dim oAuditReponse As New SGATransactionWS.auditTypeResponse

        oRequest.codsolot = Funciones.CheckDecimal(nroSot)
        oRequest.audit = audit
        oRequest.observacion = observacion

        oResponse = oTransaccion.anularSot(oRequest)

        oAuditReponse = oResponse.audit
        oSGAResponseVenta.msgRepuesta = oAuditReponse.mensajeRespuesta
        oSGAResponseVenta.codsolot = oAuditReponse.codigoRespuesta

        Return oSGAResponseVenta
    End Function
End Class
