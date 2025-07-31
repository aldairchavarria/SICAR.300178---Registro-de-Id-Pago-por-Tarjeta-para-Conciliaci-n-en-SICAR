Imports System
Imports System.Data
Imports SAP_SIC_Pagos
Imports COM_SIC_Activaciones
Imports SisCajas.Funciones

Public Class clsAnulaciones

    '*********************************************************************
    'Funcion:		AnulacionDePago
    'Objetivo:		Anular los Pagos por un documento Sunat
    '*********************************************************************
    Public Sub AnulacionDePago(ByVal strNumDocSap As String, _
                                ByVal strNumDocSunat As String, _
                                ByVal strFechaPago As String, _
                                ByVal strViaPagos() As String, _
                                ByVal strTipoTienda As String, _
                                ByVal strOficina As String, _
                                ByVal strUsuario As String, ByVal strMontos() As String)

        Dim i As Integer
        Dim objCaja As New COM_SIC_Cajas.clsCajas
        Dim obSAP As New clsPagos
        Dim objSapCajas As New SAP_SIC_Cajas.clsCajas
        Dim dsResult As DataSet
        Dim dsAcuerdo As DataSet
        Dim strNroSEC As String
        Dim strContrato As String

        Dim intOper As Int32
        If strTipoTienda = "MT" Then 'si es un CAC
            dsResult = obSAP.Get_ConsultaAnulacion(strNumDocSap)
            If dsResult.Tables(0).Rows.Count > 0 Then
                Dim drMsg As DataRow
                For Each drMsg In dsResult.Tables(0).Rows
                    If CStr(drMsg("TYPE")) = "E" Then
                        Throw New ApplicationException(CStr(drMsg("MESSAGE")))
                    End If
                Next
            End If
        End If
        Dim viaPago As String
        'For Each viaPago In strViaPagos
        For i = 0 To UBound(strViaPagos)
            viaPago = strViaPagos(i)
            If strTipoTienda = "MT" Then 'si es un CAC
                'dsResult = obSAP.Get_RegistroAnulPagos(strFechaPago, strNumDocSunat, viaPago, strOficina, strUsuario)
                dsResult = objSapCajas.Set_AnulPagosCajero(strFechaPago, strNumDocSunat, viaPago, strOficina, strUsuario)
            Else
                'dsResult = obSAP.Get_RegistroAnulPagos(strFechaPago, strNumDocSunat, viaPago, strOficina, "")
                dsResult = objSapCajas.Set_AnulPagosCajero(strFechaPago, strNumDocSunat, viaPago, strOficina, "")
            End If
            If dsResult.Tables(0).Rows.Count > 0 Then
                Dim drMsg As DataRow
                For Each drMsg In dsResult.Tables(0).Rows
                    If CStr(drMsg("TYPE")) = "E" Then
                        Throw New ApplicationException(CStr(drMsg("MSG")))
                    End If
                Next
            End If

            If viaPago = "ZEFE" Then
                objCaja.FP_InsertaEfectivo(strOficina, strUsuario, CDbl(strMontos(i)) * (-1))
            End If
        Next

        'CARIAS: SEC marcada como pagada
        obSAP.Get_ConsultaNroContrato(strOficina, "", strNumDocSap, strContrato)

        If Len(Trim(strContrato)) > 0 Then
            dsAcuerdo = obSAP.Get_ConsultaAcuerdoPCS(strContrato)

            If dsAcuerdo.Tables(0).Rows.Count > 0 Then
                strNroSEC = dsAcuerdo.Tables(0).Rows(0).Item("NRO_CARPETA_OBS")
                If Len(Trim(strNroSEC)) > 0 Then
                    objCaja.FP_Actualiza_Pago_Solicitud(strNroSEC, "0", "")
                End If
            End If
        End If
        'FIN CARIAS: SEC marcada como pagada

        dsResult = obSAP.Get_ConsultaPedido("", strOficina, strNumDocSap, "")
        If Not IsNothing(dsResult) Then
            intOper = objCaja.FP_Cab_Oper(strTipoTienda, strOficina, ConfigurationSettings.AppSettings("codAplicacion"), strUsuario, dsResult.Tables(0).Rows(0).Item("TIPO_DOC_CLIENTE"), dsResult.Tables(0).Rows(0).Item("CLIENTE"), _
                                dsResult.Tables(0).Rows(0).Item("TIPO_DOCUMENTO"), strNumDocSap, strNumDocSunat, dsResult.Tables(0).Rows(0).Item("TOTAL_MERCADERIA"), dsResult.Tables(0).Rows(0).Item("TOTAL_IMPUESTO"), dsResult.Tables(0).Rows(0).Item("TOTAL_DOCUMENTO"), "A")

            For i = 0 To dsResult.Tables(1).Rows.Count - 1
                objCaja.FP_Det_Oper(intOper, i + 1, dsResult.Tables(1).Rows(i).Item("ARTICULO"), dsResult.Tables(1).Rows(i).Item("SERIE"), dsResult.Tables(1).Rows(i).Item("NUMERO_TELEFONO"), dsResult.Tables(1).Rows(i).Item("CANTIDAD"), dsResult.Tables(1).Rows(i).Item("SUBTOTAL"), dsResult.Tables(1).Rows(i).Item("IMPUESTO1"), dsResult.Tables(1).Rows(i).Item("SUBTOTAL") + dsResult.Tables(1).Rows(i).Item("IMPUESTO1"))
            Next
        End If

    End Sub

    '*********************************************************************
    'Funcion:		AnularViasPago
    'Objetivo:		Anular los Pagos por un documento Sunat
    '*********************************************************************
    Public Sub AnularViasPago(ByVal strNumDocSap As String, _
                                ByVal strTipDocVta As String, _
                                ByVal strNumDocSunat As String, _
                                ByVal strFechaPago As String, _
                                ByVal strPedido As String, _
                                ByVal strNroDepGar As String, _
                                ByVal strNroRefDepGar As String, _
                                ByVal strNroContrato As String, _
                                ByVal strNroOpeInfocorp As String, _
                                ByVal strCodAprobacion As String, _
                                ByVal strTipoTienda As String, _
                                ByVal strOficina As String, _
                                ByVal strCodImprTicket As String, _
                                ByVal strUsuario As String, _
                                ByVal numPagos As Decimal, Optional ByVal intPool As Integer = 0, Optional ByVal strPreVenta As String = "")

        Dim bTieneCampaña As Boolean = False
        If strPedido.Trim().Length > 0 Then
            bTieneCampaña = AnulaCampana(strPedido, _
                                           strTipoTienda, _
                                            strOficina, _
                                            strCodImprTicket, _
                                            strUsuario)
        End If

        If Not bTieneCampaña Then
            AnularViasPagoDet(strNumDocSap, _
                                strTipDocVta, _
                                strNumDocSunat, _
                                strFechaPago, _
                                strPedido, _
                                strNroDepGar, _
                                strNroRefDepGar, _
                                False, _
                                strNroContrato, _
                                strNroOpeInfocorp, _
                                strCodAprobacion, _
                                strTipoTienda, _
                                strOficina, _
                                strCodImprTicket, _
                                strUsuario, numPagos, intPool, strPreVenta)
        End If

    End Sub

    '*********************************************************************
    'Funcion:		AnularViasPago
    'Objetivo:		Anular los Pagos por un documento Sunat
    '*********************************************************************
    Public Sub AnularViasPagoDet(ByVal strNumDocSap As String, _
                                ByVal strTipDocVta As String, _
                                ByVal strNumDocSunat As String, _
                                ByVal strFechaPago As String, _
                                ByVal strPedido As String, _
                                ByVal strNroDepGar As String, _
                                ByVal strNroRefDepGar As String, _
                                ByVal bolExistePrepost As Boolean, _
                                ByVal strNroContrato As String, _
                                ByVal strNroOpeInfocorp As String, _
                                ByVal strCodAprobacion As String, _
                                ByVal strTipoTienda As String, _
                                ByVal strOficina As String, _
                                ByVal strCodImprTicket As String, _
                                ByVal strUsuario As String, _
                                ByVal numPagos As Decimal, Optional ByVal intPool As Integer = 0, Optional ByVal strPreVenta As String = "")
        'bolExistePrepost : Si es campaña pregago postpago

        '*************
        Dim cteTIPODOC_DEPOSITOGARANTIA As String = ConfigurationSettings.AppSettings("cteTIPODOC_DEPOSITOGARANTIA")
        Dim k_Prefijo_Ticket As String = ConfigurationSettings.AppSettings("k_Prefijo_Ticket")
        '*************
        Dim obSAP As New SAP_SIC_Pagos.clsPagos
        Dim objSapCajas As New SAP_SIC_Cajas.clsCajas


        Dim strCarpObs As String

        'TODO VERIFICAR SI PUEDE PASAR ESTO
        'If Trim(strNumDocSap) = "" Then
        '    'RRM_DG
        '    If Trim(strTipDocVta) = Trim(cteTIPODOC_DEPOSITOGARANTIA) Then
        '        Dim strCodRefer As String = GetNumeroSap(strPedido, strFechaPago, strOficina)
        '        If strCodRefer = "" Then
        '            'todo revisar anular dep garan
        '            'Call AnularDepGaran("", "")
        '            Exit Sub
        '        End If
        '        'Else
        '        '    strCodRefer = request.Form("codRefer")  ' Captura el codigo de referencia del documento seleccionado
        '    End If
        '    'RRM_DG
        '    'docSap = Left(strCodRefer, 10)   ' Numero SAP del documento seleccionado
        '    'Else
        '    '   strCodRefer = strNumSap
        '    '  docSap = strNumSap
        'End If

        AnularDocCHIPREP(strNumDocSap, strOficina)

        If AnulaRecargaPago(strNumDocSap, strTipoTienda, strOficina) = 1 Then
            ActualizaVPago(strNumDocSap, strOficina)
        End If

        If (strTipoTienda = "MT") Then ''Solo para CACS
            Dim dsConAnul As DataSet = obSAP.Get_ConsultaAnulacion(strNumDocSap)
            If dsConAnul.Tables(0).Rows.Count > 0 Then
                Dim drMsg As DataRow
                For Each drMsg In dsConAnul.Tables(0).Rows
                    If CStr(drMsg("TYPE")) = "E" Then
                        Throw New ApplicationException(CStr(drMsg("MESSAGE")))
                    End If
                Next
            End If
            '********************************************
            Dim strNumRef, strUltNumero As String
            If (k_Prefijo_Ticket = Left(strNumDocSunat, 2) AndAlso strCodImprTicket <> "") Then
                obSAP.Get_NumeroSUNAT(strOficina, "ZFBR", strCodImprTicket, "X", strNumRef, strUltNumero)
                If Trim(strNumRef) <> Trim(strNumDocSunat) Then
                    Throw New ApplicationException("No se puede Anular este Documento " & Trim(strNumDocSunat) & " debido a que el ultimo documento creado es el " & Trim(strNumRef))
                End If
            End If
            '********************************************
            Dim objCaja As New COM_SIC_Cajas.clsCajas
            Dim dsResult3 As DataSet
            dsResult3 = obSAP.Get_ConsultaPedido("", strOficina, strNumDocSap, "")
            strCarpObs = dsResult3.Tables(0).Rows(0).Item("NRO_SOLICITUD")
            'detalle Pagos : OBTENER VIAS DE PAGO

            If numPagos > 0 And intPool > 0 Then  ' si ha realizado pagos y no vino de Pool de Pagos
                Dim dsDetalle As DataSet = obSAP.Get_ConsultaPagos(strOficina, strNumDocSunat, strTipDocVta)
                If dsDetalle.Tables(1).Rows.Count > 0 Then
                    Dim drMsg As DataRow
                    For Each drMsg In dsDetalle.Tables(1).Rows
                        If CStr(drMsg("TYPE")) = "E" Then
                            '  Throw New ApplicationException(CStr(drMsg("MESSAGE")))
                        End If
                    Next
                End If
                Dim drFila As DataRow

                For Each drFila In dsDetalle.Tables(0).Rows 'ELIMINA PAGOS POR VIA DE PAGO
                    Dim dsResult As DataSet

                    If strTipoTienda = "MT" Then 'si es un CAC
                        'dsResult = obSAP.Get_RegistroAnulPagos(strFechaPago, strNumDocSunat, drFila("VIA_PAGO"), strOficina, strUsuario)
                        dsResult = objSapCajas.Set_AnulPagosCajero(strFechaPago, strNumDocSunat, drFila("VIA_PAGO"), strOficina, strUsuario)
                    Else
                        'dsResult = obSAP.Get_RegistroAnulPagos(strFechaPago, strNumDocSunat, drFila("VIA_PAGO"), strOficina, "")
                        dsResult = objSapCajas.Set_AnulPagosCajero(strFechaPago, strNumDocSunat, drFila("VIA_PAGO"), strOficina, "")
                    End If
                    If dsResult.Tables(0).Rows.Count > 0 Then
                        Dim drMsg As DataRow
                        For Each drMsg In dsResult.Tables(0).Rows
                            If CStr(drMsg("TYPE")) = "E" Then
                                Throw New ApplicationException(CStr(drMsg("MSG")))
                            End If
                        Next
                    End If

                    If drFila("VIA_PAGO") = "ZEFE" Then
                        objCaja.FP_InsertaEfectivo(strOficina, strUsuario, CDbl(drFila("IMPORTE")) * (-1))
                    End If
                Next

            End If
            '*************************************************
            'Anulación con RFC ZPVU_RFC_TRS_PEDIDO_ANULACION
            Dim aCadena(35) As String
            aCadena(0) = " "
            aCadena(1) = "ZAFR"
            aCadena(5) = strOficina ' Oficina de Ventas
            aCadena(7) = strNumDocSap ' Numero Documento Sap
            Dim strCadenaDoc As String = String.Join(";", aCadena)

            Dim dsResult2 As DataSet = obSAP.Set_AnularDocumentoJob(CStr(strCadenaDoc), strUsuario)
            If dsResult2.Tables(1).Rows.Count > 0 Then
                Dim drMsg As DataRow
                For Each drMsg In dsResult2.Tables(1).Rows
                    If CStr(drMsg("TYPE")) = "E" Then
                        Throw New ApplicationException(CStr(drMsg("MSG")))
                    End If
                Next
            End If

            'Running Program
            If Trim(strPreVenta) <> "" Then
                obSAP.Set_ActPedidoPreVenta(strPreVenta, "", "", "", "", "X") 'Libero el proceso del pedido preventa
            End If
            ' FIN Running Program

            'Dim objCaja As New COM_SIC_Cajas.clsCajas
            Dim intOper As Int32
            Dim i As Integer

            If Not IsNothing(dsResult3) Then
                If dsResult3.Tables(0).Rows.Count > 0 Then
                    intOper = objCaja.FP_Cab_Oper(strTipoTienda, strOficina, ConfigurationSettings.AppSettings("codAplicacion"), strUsuario, dsResult3.Tables(0).Rows(0).Item("TIPO_DOC_CLIENTE"), dsResult3.Tables(0).Rows(0).Item("CLIENTE"), _
                                        dsResult3.Tables(0).Rows(0).Item("TIPO_DOCUMENTO"), strNumDocSap, strNumDocSunat, dsResult3.Tables(0).Rows(0).Item("TOTAL_MERCADERIA"), dsResult3.Tables(0).Rows(0).Item("TOTAL_IMPUESTO"), dsResult3.Tables(0).Rows(0).Item("TOTAL_DOCUMENTO"), "A")

                    For i = 0 To dsResult3.Tables(1).Rows.Count - 1
                        objCaja.FP_Det_Oper(intOper, i + 1, dsResult3.Tables(1).Rows(i).Item("ARTICULO"), dsResult3.Tables(1).Rows(i).Item("SERIE"), dsResult3.Tables(1).Rows(i).Item("NUMERO_TELEFONO"), dsResult3.Tables(1).Rows(i).Item("CANTIDAD"), dsResult3.Tables(1).Rows(i).Item("SUBTOTAL"), dsResult3.Tables(1).Rows(i).Item("IMPUESTO1"), dsResult3.Tables(1).Rows(i).Item("SUBTOTAL") + dsResult3.Tables(1).Rows(i).Item("IMPUESTO1"))
                    Next
                End If
            End If


        Else 'PARA EL CANAL INDIRECTO : DAC

            'detalle Pagos : OBTENER VIAS DE PAGO
            If numPagos > 0 Then ' si ha realizado pagos
                Dim dsDetalle As DataSet = obSAP.Get_ConsultaPagos(strOficina, strNumDocSunat, strTipDocVta)
                If dsDetalle.Tables(1).Rows.Count > 0 Then
                    Dim drMsg As DataRow
                    For Each drMsg In dsDetalle.Tables(1).Rows
                        If CStr(drMsg("TYPE")) = "E" Then
                            Throw New ApplicationException(CStr(drMsg("MESSAGE")))
                        End If
                    Next
                End If
                Dim drFila As DataRow
                For Each drFila In dsDetalle.Tables(0).Rows 'ELIMINA PAGOS POR VIA DE PAGO
                    Dim dsResult As DataSet
                    If strTipoTienda = "MT" Then 'si es un CAC
                        'dsResult = obSAP.Get_RegistroAnulPagos(strFechaPago, strNumDocSunat, drFila("VIA_PAGO"), strOficina, strUsuario)
                        dsResult = objSapCajas.Set_AnulPagosCajero(strFechaPago, strNumDocSunat, drFila("VIA_PAGO"), strOficina, strUsuario)
                    Else
                        'dsResult = obSAP.Get_RegistroAnulPagos(strFechaPago, strNumDocSunat, drFila("VIA_PAGO"), strOficina, "")
                        dsResult = objSapCajas.Set_AnulPagosCajero(strFechaPago, strNumDocSunat, drFila("VIA_PAGO"), strOficina, "")
                    End If
                    If dsResult.Tables(0).Rows.Count > 0 Then
                        Dim drMsg As DataRow
                        For Each drMsg In dsResult.Tables(0).Rows
                            If CStr(drMsg("TYPE")) = "E" Then
                                Throw New ApplicationException(CStr(drMsg("MSG")))
                            End If
                        Next
                    End If
                Next
            End If

            Dim strNroPedido As String = strNumDocSap
            If Trim(strNumDocSunat) <> "" Then
                strNroPedido = strNroPedido & ";ANUL;"
            Else
                strNroPedido = strNroPedido & ";ELIM;"
            End If
            strNroPedido = strNroPedido + strOficina '+  String. (28, ";")
            Dim i As Integer
            For i = 1 To 28
                strNroPedido += ";"
            Next
            '*****************CAMBIO DE DLL******************
            'objComponente = CreateObjectVenta(Session("ALMACEN"))
            'Dim StrXmlDll2
            'StrXmlDll2 = objComponente.Set_CreaPedidoA(CStr(strNroPedido), "", "", "")
            'objRecordSet = XmlToRecordset(StrXmlDll2, "RS")            
            '************************************************

        End If
        ' SI TIENE DEPOSITO EN GARANTIA
        If Len(Trim(strNroDepGar)) <> 0 AndAlso (Trim(strNroDepGar) <> "0") And CLng(strNroDepGar) > 0 Then
            If Not bolExistePrepost Then
                AnularDepGaran(strNroDepGar, strNroRefDepGar, strUsuario)
            End If
        End If
        '*************************************************
        LiberaCarpeta(strNumDocSap, strOficina, strTipoTienda, strCarpObs)
        LiberaClieRec(strNroContrato, strNroOpeInfocorp, strCodAprobacion)
        '*************************************************


    End Sub

    Public Function AnulaCampana(ByVal strPedido As String, _
                                ByVal strTipoTienda As String, _
                                ByVal strOficina As String, _
                                ByVal strCodImprTicket As String, _
                                ByVal strUsuario As String) As Boolean

        'Private Sub AnulaCampana(ByVal strPedido As String, ByVal strOficina As String)

        '*************
        Dim gstrCamEstadoUsado As String = ConfigurationSettings.AppSettings("gstrCamEstadoUsado")
        Dim gstrCamEstadoAnulado As String = ConfigurationSettings.AppSettings("gstrCamEstadoAnulado")
        Dim gstrCamEstadoPagado As String = ConfigurationSettings.AppSettings("gstrCamEstadoPagado")
        Dim gstrCamEstadoVendido As String = ConfigurationSettings.AppSettings("gstrCamEstadoVendido")
        Dim gstrCamEstadoLibre As String = ConfigurationSettings.AppSettings("gstrCamEstadoLibre")
        '*************
        Dim secuencial, oficVenta, Cli, tipodocCli, numtelPre, numtelPos, numDocPrepago, numDocPostpago, estadoDoc, fechaCreacion As String
        Dim sTipoDoc, sDocAsociado As String
        Dim strAccionPost, strAccionPre As String
        Dim strAccion As String
        Dim strEstado As String
        Dim strDocCampana, strCampana As String
        Dim i As Integer
        '*************
        Dim strDocSAPPrepago As String
        Dim strDocSAPPostpago As String
        Dim strNumDepGaranPrePost As String
        Dim strNumRefDepGaranPrePost As String
        Dim drDocSAPPrepago As DataRow
        Dim drDocSAPPostpago As DataRow
        Dim obSAP As New SAP_SIC_Pagos.clsPagos
        '*************
        Dim bresult As Boolean = False
        '*************

        '***********************

        Dim dsCamp As DataSet = obSAP.ConsultaTriacionPrePost(strPedido, "", "", "")
        Dim drFila As DataRow

        If dsCamp.Tables(0).Rows.Count > 0 Then 'modificado por JCR
            'If Not dsCamp Is Nothing Then

            drDocSAPPrepago = ObtenerLineaDatosDV(CStr(dsCamp.Tables(0).Rows(0)("NRO_DOC_PREPAGO")).PadLeft(Len(strPedido), "0"), CStr(dsCamp.Tables(0).Rows(0)("FECHA_CREACION")), strOficina)
            drDocSAPPostpago = ObtenerLineaDatosDV(CStr(dsCamp.Tables(0).Rows(0)("NRO_DOC_POSTPAGO")).PadLeft(Len(strPedido), "0"), CStr(dsCamp.Tables(0).Rows(0)("FECHA_CREACION")), strOficina)
            If Not drDocSAPPrepago Is Nothing Then
                strDocSAPPrepago = CStr(drDocSAPPrepago("VBELN"))
            End If

            If Not drDocSAPPostpago Is Nothing Then
                strDocSAPPostpago = CStr(drDocSAPPostpago("VBELN"))
                strNumDepGaranPrePost = CStr(drDocSAPPostpago("NRO_DEP_GARANTIA"))
                strNumRefDepGaranPrePost = CStr(drDocSAPPostpago("NRO_REF_DEP_GAR"))
            End If

            'strDocSAPPrepago = GetNumeroSap(CStr(dsCamp.Tables(0).Rows(0)("NRO_DOC_PREPAGO")).PadLeft(Len(strPedido), "0"), CStr(dsCamp.Tables(0).Rows(0)("FECHA_CREACION")), strOficina)
            'strDocSAPPostpago = GetNumeroSap(CStr(dsCamp.Tables(0).Rows(0)("NRO_DOC_POSTPAGO")).PadLeft(Len(strPedido), "0"), CStr(dsCamp.Tables(0).Rows(0)("FECHA_CREACION")), strOficina)

            If Not ((Len(Trim(strDocSAPPrepago)) = 0) And (Len(Trim(strDocSAPPostpago)) = 0)) Then

                For Each drFila In dsCamp.Tables(0).Rows

                    secuencial = CStr(drFila("SECUENCIAL"))
                    oficVenta = CStr(drFila("OFICINA_VENTA"))
                    Cli = CStr(drFila("CLIENTE"))
                    tipodocCli = CStr(drFila("TIPO_DOC_CLIENTE"))
                    numtelPre = CStr(drFila("NRO_TEL_PREPAGO"))
                    numtelPos = CStr(drFila("NRO_TEL_POSTPAGO"))
                    numDocPrepago = CStr(drFila("NRO_DOC_PREPAGO"))
                    numDocPostpago = CStr(drFila("NRO_DOC_POSTPAGO"))
                    estadoDoc = CStr(drFila("ESTADO"))
                    fechaCreacion = CStr(drFila("FECHA_CREACION"))

                    numDocPrepago = numDocPrepago.PadLeft(Len(strPedido), "0")
                    numDocPostpago = numDocPostpago.PadLeft(Len(strPedido), "0")

                    If strPedido = numDocPrepago Then
                        sTipoDoc = "PREPAGO"
                        sDocAsociado = numDocPostpago
                    End If
                    If strPedido = numDocPostpago Then
                        sTipoDoc = "POSTPAGO"
                        sDocAsociado = numDocPrepago
                    End If

                    'If j = 0 Then
                    '    strDocSAPPrepago = GetNumeroSap(numDocPrePago, fechaCreacion, Session("ALMACEN"))
                    '    strLineaPrepago = strLineaCampana
                    '    strDocSAPPostpago = GetNumeroSap(numDocPostPago, fechaCreacion, Session("ALMACEN"))


                    'End If
                    'j = j + 1
                    If Trim(numDocPostpago) <> "0" And sTipoDoc = "PREPAGO" And Trim(estadoDoc) = gstrCamEstadoUsado Then
                        strEstado = gstrCamEstadoAnulado
                        strAccionPre = "A"
                        strAccionPost = "A"
                        'strAccionPostAux = gstrCamEstadoUsado
                    End If
                    If Trim(numDocPostpago) <> "0" And sTipoDoc = "PREPAGO" And Trim(estadoDoc) = gstrCamEstadoPagado Then
                        strEstado = gstrCamEstadoAnulado
                        strAccionPre = "A"
                        strAccionPost = "A"
                        'strFlagPoolPostpago = True
                    End If
                    If Trim(numDocPostpago) <> "0" And sTipoDoc = "PREPAGO" And Trim(estadoDoc) = gstrCamEstadoVendido Then
                        strEstado = gstrCamEstadoAnulado
                        strAccionPre = "A"
                        strAccionPost = "A"
                        'strFlagPoolPostpago = True
                    End If
                    If Trim(numDocPostpago) <> "0" And sTipoDoc = "POSTPAGO" And Trim(estadoDoc) = gstrCamEstadoUsado Then
                        strEstado = gstrCamEstadoLibre
                        strAccionPre = ""
                        strAccionPost = "A"
                        numtelPos = ""
                        numDocPostpago = ""
                    End If
                    If Trim(numDocPostpago) <> "0" And sTipoDoc = "POSTPAGO" And Trim(estadoDoc) = gstrCamEstadoPagado Then
                        strEstado = gstrCamEstadoLibre
                        strAccionPre = ""
                        strAccionPost = "A"
                        numtelPos = ""
                        numDocPostpago = ""
                    End If
                    If Trim(numDocPostpago) <> "0" And sTipoDoc = "POSTPAGO" And Trim(estadoDoc) = gstrCamEstadoVendido Then
                        strEstado = gstrCamEstadoAnulado
                        strAccionPre = "A"
                        strAccionPost = "A"
                        'strLineaCampana = strLineaPrepago
                        'strFlagPostPre = True
                    End If
                    If Trim(numDocPostpago) = "0000000000" And sTipoDoc = "PREPAGO" And Trim(estadoDoc) = gstrCamEstadoLibre Then
                        strEstado = gstrCamEstadoAnulado
                        strAccionPre = "A"
                        strAccionPost = ""
                        'If strAccionPostAux = gstrCamEstadoUsado Then
                        'strAccionPost = "A"
                        'End If
                    End If
                    strDocCampana = numDocPrepago & ";" & strDocSAPPrepago & ";" & strAccionPre & ";" & numDocPostpago & ";" & strDocSAPPostpago & ";" & strAccionPost
                    strCampana = strCampana & secuencial & ";" & oficVenta & ";" & Cli & ";" & tipodocCli & ";" & numtelPre & ";" & numDocPrepago & ";" & numtelPos & ";" & numDocPostpago & ";" & strEstado & "|"
                Next
                Dim arrCampana() As String

                strCampana = Mid(strCampana, 1, Len(strCampana) - 1)
                arrCampana = Split(Trim(strCampana), "|")
                'ANULA POSTPAGO
                If Trim(Split(strDocCampana, ";")(5)) = "A" Then
                    If Len(Trim(Split(strDocCampana, ";")(4))) > 0 Then
                        bresult = True
                        'strNumPedidoAnulacion = Trim(Split(strDocCampana, ";")(3))
                        'AnularViasPago(Split(strDocCampana, ";")(4))
                        AnularViasPagoDet(drDocSAPPostpago("VBELN"), _
                                drDocSAPPostpago("FKART"), _
                                drDocSAPPostpago("XBLNR"), _
                                drDocSAPPostpago("FKDAT"), _
                                drDocSAPPostpago("PEDIDO"), _
                                drDocSAPPostpago("NRO_DEP_GARANTIA"), _
                                drDocSAPPostpago("NRO_REF_DEP_GAR"), _
                                True, _
                                drDocSAPPostpago("NRO_CONTRATO"), _
                                drDocSAPPostpago("NRO_OPE_INFOCORP"), _
                                drDocSAPPostpago("CODIGO_APROBACIO"), _
                                strTipoTienda, _
                                strOficina, _
                                strCodImprTicket, _
                                strUsuario, drDocSAPPostpago("PAGOS"))
                    End If
                End If
                For i = 0 To UBound(arrCampana)
                    If Trim(Split(strDocCampana, ";")(3)) = Split(arrCampana(i), ";")(7) Then
                        Dim ds As DataSet = obSAP.Set_TriacionPrePost(arrCampana(i))
                    End If
                Next

                'ANULAR PREPAGO
                If Trim(Split(strDocCampana, ";")(2)) = "A" Then
                    If Len(Trim(Split(strDocCampana, ";")(1))) > 0 Then
                        bresult = True
                        'strNumPedidoAnulacion = Trim(Split(strDocCampana, ";")(0))
                        'Call AnularViasPago(Split(strDocCampana, ";")(1))
                        AnularViasPagoDet(drDocSAPPrepago("VBELN"), _
                                drDocSAPPrepago("FKART"), _
                                drDocSAPPrepago("XBLNR"), _
                                drDocSAPPrepago("FKDAT"), _
                                drDocSAPPrepago("PEDIDO"), _
                                drDocSAPPrepago("NRO_DEP_GARANTIA"), _
                                drDocSAPPrepago("NRO_REF_DEP_GAR"), _
                                True, _
                                drDocSAPPrepago("NRO_CONTRATO"), _
                                drDocSAPPrepago("NRO_OPE_INFOCORP"), _
                                drDocSAPPrepago("CODIGO_APROBACIO"), _
                                strTipoTienda, _
                                strOficina, _
                                strCodImprTicket, _
                                strUsuario, drDocSAPPrepago("PAGOS"))
                    End If
                End If
                For i = 0 To UBound(arrCampana)
                    If Trim(Split(strDocCampana, ";")(0)) = Split(arrCampana(i), ";")(5) Then
                        If Len(Trim(arrCampana(i))) > 0 Then
                            Dim ds As DataSet = obSAP.Set_TriacionPrePost(arrCampana(i))
                        End If
                    End If
                Next


                If Len(Trim(strNumDepGaranPrePost)) > 0 AndAlso CLng(strNumDepGaranPrePost) > 0 Then
                    AnularDepGaran(strNumDepGaranPrePost, strNumRefDepGaranPrePost, strUsuario)
                End If

            End If

        End If

        Return bresult

    End Function

    '*********************************************************************
    'Funcion:		GetNumeroSap
    'Objetivo:		Anular documento de chip Repuesto
    '*********************************************************************
    Public Sub AnularDocCHIPREP(ByVal strNroPedidoSAP As String, ByVal strOficina As String)
        '***********
        Dim gstrEstSolicVariado As String = ConfigurationSettings.AppSettings("gstrEstSolicVariado")
        Dim gstrEstSolicAnulado As String = ConfigurationSettings.AppSettings("gstrEstSolicAnulado")
        '*********
        Dim strSolicitud As String
        'Dim strResult As String = "S"

        Dim obSAP As New SAP_SIC_Pagos.clsPagos
        Dim dsResult As DataSet = obSAP.SetGet_LogActivacionCHIP(strNroPedidoSAP, strOficina, "", "")

        If dsResult.Tables(1).Rows.Count > 0 Then
            Dim drMsg As DataRow
            For Each drMsg In dsResult.Tables(1).Rows
                If CStr(drMsg("TYPE")) = "E" Then
                    Throw New ApplicationException(CStr(drMsg("MESSAGE")))
                End If
            Next
        End If

        If dsResult.Tables(0).Rows.Count > 0 Then
            Dim i, j As Integer
            For i = 0 To dsResult.Tables(0).Rows.Count - 1
                For j = 0 To dsResult.Tables(0).Columns.Count - 1
                    strSolicitud += dsResult.Tables(0).Rows(i)(j)
                    If j < dsResult.Tables(0).Columns.Count - 1 Then
                        strSolicitud += ";"
                    End If
                Next
                If i < dsResult.Tables(0).Rows.Count - 1 Then strSolicitud += "¿"
            Next
        End If
        If strSolicitud <> "" Then
            Dim arrSolicitud() As String = Split(strSolicitud, ";")
            'if arrSolicitud(14) <> "X" and arrSolicitud(13) = gstrEstSolicNuevo then
            If arrSolicitud(13) <> gstrEstSolicVariado Then
                '       strResult = "N"
                'Else
                '      strResult = "S"
                arrSolicitud(13) = gstrEstSolicAnulado
                arrSolicitud(14) = ""
                obSAP.SetGet_LogActivacionCHIP("", "", String.Join(";", arrSolicitud), "")
            End If
        End If

        'Return strResult

    End Sub


    '**************************************Funcion de Anulacion de Recarga***************************************
    '**************************************Funcion de Anulacion de Recarga***************************************
    '**************************************Funcion de Anulacion de Recarga***************************************
    Public Function AnulaRecargaPago(ByVal NumFacturaSAP As String, ByVal strTipoOficina As String, ByVal strAlmacen As String) As Integer

        Dim pvTerminalID, pvTrace, pvCanal, pvTelefono, pvBinAdquiriente, pvCodCadena, pvCodComercio As String
        Dim pvMonto, pvMoneda, pvProducto, pvNumRecarga As String
        Dim obSAP As New SAP_SIC_Pagos.clsPagos
        pvTerminalID = "PVU"
        pvTrace = Right(NumFacturaSAP, 6)
        pvCanal = "91"

        Dim intResultado As Integer = 0

        Dim codVendedor As String = obSAP.Get_ConsultaVendedorRecarga(strTipoOficina, strAlmacen)

        pvBinAdquiriente = Mid(codVendedor, 4, 6)
        pvCodCadena = Mid(codVendedor, 4, 7)
        pvCodComercio = strAlmacen

        pvMoneda = "604"
        pvProducto = "1"

        Dim dsResult As DataSet = obSAP.Get_ConsultaDetalleRVirtual(NumFacturaSAP, strAlmacen)

        If Not dsResult Is Nothing AndAlso dsResult.Tables(0).Rows.Count > 0 Then
            'TODO FALTA DEFINIR ESTE COMPONENTE
            Dim obRecarga As New COM_SIC_INActChip.clsRecarga

            pvTelefono = dsResult.Tables(0).Rows(0).Item("NRO_TELEFONICO")
            pvMonto = dsResult.Tables(0).Rows(0).Item("REC_EFECTIVA")
            pvNumRecarga = dsResult.Tables(0).Rows(0).Item("NRO_REC_SWITCH")


            If Not (pvNumRecarga Is Nothing OrElse pvNumRecarga = "" OrElse CDbl(dsResult.Tables(0).Rows(0).Item("TOTAL_PAGO")) = 0) Then
                intResultado = 1
                'objComponenteARV = CreateObjectRVirtual(Session("ALMACEN"))

                'objComponenteARV.CodOficina = CStr(Session("ALMACEN"))
                Dim strNvTIMPPV As String = obRecarga.Anulacion( _
                     pvTerminalID, pvTrace, pvCanal, pvTelefono, _
                     pvMonto, pvMoneda, pvProducto, _
                     pvBinAdquiriente, pvCodCadena, pvCodComercio, pvNumRecarga, "", "", strAlmacen)

                'Throw New ApplicationException(strNvTIMPPV)
                Dim arrDatos() As String = Split(strNvTIMPPV, ";")
                Dim saldo As String = decimalesPago(arrDatos(4))


                If CDbl(Trim(arrDatos(0))) < 0 Then
                    Dim Mensaje As String = "Error: La anulacion de la recarga virtual no se ha podido realizar. Motivo: " & arrDatos(3)
                    If Trim(arrDatos(3)) = "" Then
                        Mensaje = "NO SE PUEDEN REALIZAR OPERACIONES DE RECARGA VIRTUAL EN ESTE MOMENTO"
                    End If

                    If CDbl(Trim(arrDatos(2))) <> 40 And CDbl(Trim(arrDatos(2))) <> 19 And CDbl(Trim(arrDatos(2))) <> 48 And CDbl(Trim(arrDatos(2))) <> 43 Then
                        Throw New ApplicationException(Mensaje)
                    Else
                        Mensaje = ""
                    End If



                End If

                '    '- Transaccion ya reversada: ISO 96 PPV 48
                '    '- Codigo PPV diferente    : ISO 21 PPV 43
                '    Dim ObjLog
                '    ObjLog = server.createobject("COM_PVU_LOG.Reg_Log")
                '    ObjLog.GrabarLog("::NumRecarga::" & pvNumRecarga & "::Telefono::" & pvTelefono & "::Factura::" & NumFacturaSAP & "::Oficina::" & Session("ALMACEN") & "::CodPPV::" & Trim(arrDatos(2)) & "::Mensaje::" & Mensaje, "X")
                '    ObjLog = Nothing

                '    If CDbl(Trim(arrDatos(2))) <> 40 And CDbl(Trim(arrDatos(2))) <> 19 And CDbl(Trim(arrDatos(2))) <> 48 And CDbl(Trim(arrDatos(2))) <> 43 Then
                '        Call RedirectPoolPagos(Mensaje)
                '    Else
                '        Mensaje = ""
                '    End If
            End If
        End If
        Return intResultado
    End Function


    Public Sub ActualizaVPago(ByVal NumFacturaSAP As String, ByVal codOficina As String)
        Dim Recarga As String
        Dim NumTel, NumPos As String

        Dim arrPrueba() As String

        Dim obSAP As New SAP_SIC_Pagos.clsPagos
        Dim dsResult As DataSet = obSAP.Get_ConsultaDetalleRVirtual(NumFacturaSAP, codOficina)
        If Not dsResult Is Nothing AndAlso dsResult.Tables(0).Rows.Count > 0 Then
            NumPos = CStr(dsResult.Tables(0).Rows(0)(1)) 'objRecordSetRV.Fields(1).Value
            NumTel = CStr(dsResult.Tables(0).Rows(0)(3)) 'objRecordSetRV.Fields(3).Value
            Recarga = NumFacturaSAP & ";" & NumPos & ";" & codOficina & ";" & NumTel & ";0.00;0.00;0.00;0.00;0.00;"

            Dim ds1 As DataSet = obSAP.Set_VentaRecarga(Recarga, codOficina)
            If ds1.Tables(1).Rows.Count > 0 Then
                Dim drMsg As DataRow
                For Each drMsg In ds1.Tables(1).Rows
                    If CStr(drMsg("TYPE")) = "E" Then
                        Throw New ApplicationException(CStr(drMsg("MESSAGE")))
                    End If
                Next
            End If
        End If



    End Sub

    '*********************************************************************
    'Funcion:		GetNumeroSap
    'Objetivo:		Obtener el NumeroSAP  a partir del Numero de Pedido
    '*********************************************************************
    Public Function GetNumeroSap(ByVal strNumeroPedido As String, ByVal strFecha As String, ByVal strOficina As String) As String
        '*************
        Dim cteTIPODOC_DEPOSITOGARANTIA As String = ConfigurationSettings.AppSettings("cteTIPODOC_DEPOSITOGARANTIA")
        '*************
        Dim objPagos As New clsPagos
        Dim dsPagos As DataSet = objPagos.Get_ConsultaPoolFactura(strOficina, strFecha, "I", "", "", "", "", "")
        Dim drFila As DataRow
        Dim cResult As String

        For Each drFila In dsPagos.Tables(0).Rows
            If CStr(drFila(6)) <> cteTIPODOC_DEPOSITOGARANTIA Then
                If strNumeroPedido = CStr(drFila(31)) Then
                    cResult = CStr(drFila(2))
                    Exit For
                End If
            End If
        Next
        If cResult Is Nothing Then
            dsPagos = objPagos.Get_ConsultaPoolFactura(strOficina, strFecha, "R", "", "", "", "", "")
            For Each drFila In dsPagos.Tables(0).Rows
                If CStr(drFila(6)) <> cteTIPODOC_DEPOSITOGARANTIA Then
                    If strNumeroPedido = CStr(drFila(31)) Then
                        cResult = CStr(drFila(2))
                        Exit For
                    End If
                End If
            Next
        End If

        Return cResult
    End Function

    '*********************************************************************
    'Funcion:		ObtenerLineaDatosDV
    'Objetivo:		Obtener el registro  a partir del Numero de Pedido
    '*********************************************************************
    Public Function ObtenerLineaDatosDV(ByVal strNumeroPedido As String, ByVal strFecha As String, ByVal strOficina As String) As DataRow
        'Obtener el pool de documentos por pagar
        '*************
        Dim cteTIPODOC_DEPOSITOGARANTIA As String = ConfigurationSettings.AppSettings("cteTIPODOC_DEPOSITOGARANTIA")
        '*************
        Dim objPagos As New clsPagos

        Dim dsPagos As DataSet = objPagos.Get_ConsultaPoolFactura(strOficina, strFecha, "I", "", "", "", "", "")
        Dim drFila As DataRow
        Dim drResult As DataRow

        For Each drFila In dsPagos.Tables(0).Rows
            If CStr(drFila(6)) <> cteTIPODOC_DEPOSITOGARANTIA Then
                If strNumeroPedido = CStr(drFila(31)) Then
                    drResult = drFila
                    Exit For
                End If
            End If
        Next
        If drResult Is Nothing Then
            dsPagos = objPagos.Get_ConsultaPoolFactura(strOficina, strFecha, "R", "", "", "", "", "")
            For Each drFila In dsPagos.Tables(0).Rows
                If CStr(drFila(6)) <> cteTIPODOC_DEPOSITOGARANTIA Then
                    If strNumeroPedido = CStr(drFila(31)) Then
                        drResult = drFila
                        Exit For
                    End If
                End If
            Next
        End If

        Return drResult
    End Function



    Public Sub LiberaCarpeta(ByVal strNumDocSapCO As String, ByVal strOficina As String, ByVal strTipoTienda As String, Optional ByVal strNroCarpObs As String = "")

        Dim StrNumAcuerdoLBCO As String
        Dim strNumLBCO As String
        Dim drFila As DataRow
        Dim obSAP As New SAP_SIC_Pagos.clsPagos

        Dim dsConsulta As DataSet = obSAP.Get_ConsultaNroContrato(strOficina, "", strNumDocSapCO, StrNumAcuerdoLBCO)

        'For Each drFila In dsConsulta.Tables(0).Rows
        '    If CStr(drFila(0)) = "X" AndAlso CStr(drFila(1)) = "V1" Then
        '        StrNumAcuerdoLBCO = CStr(drFila(3))
        '        Exit For
        '    End If
        'Next

        If Trim(StrNumAcuerdoLBCO) <> "" And Trim(StrNumAcuerdoLBCO) <> "0000000000" Then
            If CLng(StrNumAcuerdoLBCO) <> 0 Then
                Dim dsResult As DataSet = obSAP.Get_ConsultaAcuerdoPCS(StrNumAcuerdoLBCO)
                If dsResult.Tables(0).Rows.Count > 0 Then
                    strNumLBCO = Trim(CStr(dsResult.Tables(0).Rows(0)("NRO_CARPETA_OBS")))
                End If
            End If
            If Trim(strNumLBCO) <> "" Then
                Dim strDatosLBCO As String = Trim(strNumLBCO) & ";" & Trim(strTipoTienda)
                Dim ob As New COM_SIC_Cajas.clsCajas
                'ob.FK_LiberaCarpeta(Trim(strNumLBCO), strTipoTienda)
                ob.FP_Actualiza_Contrato_Solicitud(Trim(strNumLBCO), "00")
            End If
        Else
            Dim ob As New COM_SIC_Cajas.clsCajas
            'ob.FK_LiberaCarpeta(Trim(strNroCarpObs), strTipoTienda)
            If IsNumeric(strNroCarpObs) Then
                ob.FP_Actualiza_Contrato_Solicitud(Trim(strNroCarpObs), "00")
            End If
        End If


    End Sub


    Public Sub LiberaClieRec(ByVal strNroContrato As String, ByVal strNroOpeInfocorp As String, ByVal strCodAprobacion As String)

        Dim ob As New COM_SIC_Cajas.clsCajas

        If (strNroContrato <> "" AndAlso strNroOpeInfocorp <> "" AndAlso strCodAprobacion <> "") Then
            ob.FP_Libera_ClieRec(strNroContrato.Trim().PadLeft(10, "0"), strCodAprobacion, strNroOpeInfocorp, "", "")
        End If
        ob = Nothing

    End Sub


    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Nombre: AnularDepGaran()
    ' Descripcion:	Procedimiento que anula Deposito de Garantia
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub AnularDepGaran(ByVal strP_NumDepGaran As String, ByVal strP_NumRefDepGaran As String, ByVal strUsuario As String, Optional ByVal flgAnul As String = "")

        Dim obSAP As New SAP_SIC_Pagos.clsPagos
        Dim dsAnul As DataSet
        Dim objCajas As New COM_SIC_Cajas.clsCajas

        Dim dsResult As DataSet = obSAP.Get_DepositoGarantia(strP_NumDepGaran)
        If dsResult.Tables(1).Rows.Count > 0 Then
            Dim drMsg As DataRow
            For Each drMsg In dsResult.Tables(1).Rows
                If CStr(drMsg("TYPE")) = "E" Then
                    Throw New ApplicationException(CStr(drMsg("MESSAGE")))
                End If
            Next
        End If

        Dim strNumDEP As String

        'If dsResult.Tables(0).Rows.Count > 0 AndAlso CStr(dsResult.Tables(0).Rows(0)("BELNR")).Trim() <> "" Then
        If dsResult.Tables(0).Rows.Count > 0 Then
            dsAnul = obSAP.Set_AnulaDepositoGarantia(CStr(dsResult.Tables(0).Rows(0)("FECHA_DEPOSITO")), _
                                            CStr(dsResult.Tables(0).Rows(0)("OFICINA_VENTA")), _
                                            CStr(dsResult.Tables(0).Rows(0)("BELNR")), _
                                            strUsuario, "", strNumDEP)
            'CStr(dsResult.Tables(0).Rows(0)("USU_CREACION")), "", strNumDEP)

            If Trim(strNumDEP) = "" And flgAnul = "S" Then
                If dsAnul.Tables(0).Rows.Count > 0 Then
                    Dim drMsg As DataRow
                    For Each drMsg In dsAnul.Tables(0).Rows
                        If CStr(drMsg("TYPE")) = "E" Then
                            Throw New ApplicationException(CStr(drMsg("MSG")))
                        End If
                    Next
                End If
                Throw New ApplicationException(ConfigurationSettings.AppSettings("constMensajeAnulacionDG"))
            End If

            If dsResult.Tables(0).Rows(0).Item("VIA_PAGO") = "ZEFE" Then
                ' POr ahora se asume efectivo. Luego se verificara con una nueva RFC
                objCajas.FP_InsertaEfectivo(CStr(dsResult.Tables(0).Rows(0)("OFICINA_VENTA")), strUsuario, (CDbl(dsResult.Tables(0).Rows(0)("MONTO_DEPOSITO")) * -1))
            End If

        End If
        'Modificar tabla de Relacion de Entidad Acuerdo - Deposito Garantia
        Dim strP_BELNR As String = ""

        Dim strP_CADENADEPGARAN As String = CStr(dsResult.Tables(0).Rows(0)("NRO_DEP_GARANTIA")) & ";" & CStr(dsResult.Tables(0).Rows(0)("TIPO_DOC_CLIENTE")) & ";" & _
             CStr(dsResult.Tables(0).Rows(0)("CLIENTE")) & ";" & CStr(dsResult.Tables(0).Rows(0)("FECHA_DEPOSITO")) & ";" & _
             CStr(dsResult.Tables(0).Rows(0)("FECHA_VENCIMIENT")) & ";" & CStr(dsResult.Tables(0).Rows(0)("NUMERO_CONTRATO")) & ";" & _
             CStr(dsResult.Tables(0).Rows(0)("DOCUMENTO")) & ";" & strP_BELNR & ";" & _
             CStr(dsResult.Tables(0).Rows(0)("XBLNR")) & ";" & CStr(dsResult.Tables(0).Rows(0)("MONTO_DEPOSITO")) & ";" & _
             CStr(dsResult.Tables(0).Rows(0)("OFICINA_VENTA")) & ";" & CStr(dsResult.Tables(0).Rows(0)("ANULADO")) & ";" & _
             CStr(dsResult.Tables(0).Rows(0)("USU_CREACION")) & ";" & CStr(dsResult.Tables(0).Rows(0)("FEC_CREACION")) & ";" & _
             CStr(dsResult.Tables(0).Rows(0)("HOR_CREACION")) & ";" & strUsuario & ";;;" & _
             CStr(dsResult.Tables(0).Rows(0)("CLDOC")) & ";" & CStr(dsResult.Tables(0).Rows(0)("NRO_CARGOS")) & ";" & CStr(dsResult.Tables(0).Rows(0)("NRO_OPERACION"))

        'CARIAS: Se comenta la llamada a esta RFC
        'dsAnul = obSAP.Set_ModificaDepositoGarantia(strP_CADENADEPGARAN)
        'If dsAnul.Tables(1).Rows.Count > 0 Then
        '    Dim drMsg As DataRow
        '    For Each drMsg In dsAnul.Tables(1).Rows
        '        If CStr(drMsg("TYPE")) = "E" Then
        '            Throw New ApplicationException(CStr(drMsg("MESSAGE")))
        '        End If
        '    Next
        'End If

        If flgAnul <> "S" Then

            dsAnul = obSAP.Get_DepositoGarantia(CStr(dsResult.Tables(0).Rows(0)("NRO_DEP_GARANTIA")))

            If dsAnul.Tables(0).Rows.Count > 0 Then
                If Trim(CStr(dsAnul.Tables(0).Rows(0).Item("NRO_CARPETA_OBS"))) <> "" Then
                    LiberaCarpeta("", CStr(dsResult.Tables(0).Rows(0)("OFICINA_VENTA")), "", CStr(dsAnul.Tables(0).Rows(0).Item("NRO_CARPETA_OBS")))
                End If
            End If


            'Modificar tabla de Relacion de Entidad Acuerdo - Deposito Garantia
            Dim strP_ANULADO As String = "X"
            strP_CADENADEPGARAN = CStr(dsResult.Tables(0).Rows(0)("NRO_DEP_GARANTIA")) & ";" & CStr(dsResult.Tables(0).Rows(0)("TIPO_DOC_CLIENTE")) & ";" & _
                    CStr(dsResult.Tables(0).Rows(0)("CLIENTE")) & ";" & CStr(dsResult.Tables(0).Rows(0)("FECHA_DEPOSITO")) & ";" & _
                    CStr(dsResult.Tables(0).Rows(0)("FECHA_VENCIMIENT")) & ";" & CStr(dsResult.Tables(0).Rows(0)("NUMERO_CONTRATO")) & ";" & _
                    CStr(dsResult.Tables(0).Rows(0)("DOCUMENTO")) & ";" & strP_BELNR & ";" & _
                    CStr(dsResult.Tables(0).Rows(0)("XBLNR")) & ";" & CStr(dsResult.Tables(0).Rows(0)("MONTO_DEPOSITO")) & ";" & _
                    CStr(dsResult.Tables(0).Rows(0)("OFICINA_VENTA")) & ";" & strP_ANULADO & ";" & _
                    CStr(dsResult.Tables(0).Rows(0)("USU_CREACION")) & ";" & CStr(dsResult.Tables(0).Rows(0)("FEC_CREACION")) & ";" & _
                    CStr(dsResult.Tables(0).Rows(0)("HOR_CREACION")) & ";" & strUsuario & ";;;" & _
                    CStr(dsResult.Tables(0).Rows(0)("CLDOC")) & ";" & CStr(dsResult.Tables(0).Rows(0)("NRO_CARGOS")) & ";" & CStr(dsResult.Tables(0).Rows(0)("NRO_OPERACION"))


            dsAnul = obSAP.Set_ModificaDepositoGarantia(strP_CADENADEPGARAN)
            If dsAnul.Tables(1).Rows.Count > 0 Then
                Dim drMsg As DataRow
                For Each drMsg In dsAnul.Tables(1).Rows
                    If CStr(drMsg("TYPE")) = "E" Then
                        Throw New ApplicationException(CStr(drMsg("MESSAGE")))
                    End If
                Next
            End If

        End If

    End Sub

    Public Function strFechaServidor() As String

        Dim strDia, strMes, strAnio, strFecha As String

        strDia = Day(Now)
        strMes = Month(Now)
        strAnio = Year(Now)

        If Len(strDia) = 1 Then strDia = "0" & CStr(strDia)
        If Len(strMes) = 1 Then strMes = "0" & CStr(strMes)

        strFecha = strDia & "/" & strMes & "/" & strAnio

        Return strFecha

    End Function

    Public Function strHoraServidor() As String

        Dim strHora, strMinuto, strSegundo, strHoraS As String

        strHora = Hour(Now)
        strMinuto = Minute(Now)
        strSegundo = Second(Now)

        If Len(strHora) = 1 Then strHora = "0" & CStr(strHora)
        If Len(strMinuto) = 1 Then strMinuto = "0" & CStr(strMinuto)
        If Len(strSegundo) = 1 Then strSegundo = "0" & CStr(strSegundo)

        strHoraS = strHora & ":" & strMinuto & ":" & strSegundo
        Return strHoraS

    End Function

    'Public Function ObtenerDatosDV(ByVal strPuntoDeVenta As String, ByVal strNumeroPedido As String, ByVal strFecha As String) As String
    '    '*************
    '    Dim cteTIPODOC_DEPOSITOGARANTIA As String = ConfigurationSettings.AppSettings("cteTIPODOC_DEPOSITOGARANTIA")
    '    '*************
    '    Dim obSAP As New SAP_SIC_Pagos.clsPagos
    '    Dim strDatosVenta As String

    '    Dim dsResult As DataSet = obSAP.Get_ConsultaPoolFactura(strPuntoDeVenta, strFecha, "I", "", "", "", "", "")
    '    Dim drFila As DataRow
    '    For Each drFila In dsResult.Tables(0).Rows
    '        If CStr(drFila("FKART")) = cteTIPODOC_DEPOSITOGARANTIA Then
    '            strDatosVenta = CStr(drFila("VBELN"))
    '            Exit For
    '        End If
    '    Next
    '    If strDatosVenta Is Nothing Then
    '        dsResult = obSAP.Get_ConsultaPoolFactura(strPuntoDeVenta, strFecha, "R", "", "", "", "", "")

    '    End If


    'End Function


    Private Function decimalesPago(ByVal valor As String) As String
        If Trim(valor) <> "" Then
            If CDbl(Trim(valor)) <> 0 Then
                decimalesPago = Left(valor, Len(valor) - 2) & "." & Mid(valor, Len(valor) - 1, 2)
            Else
                decimalesPago = "0.00"
            End If
        Else
            decimalesPago = "0.00"
        End If
    End Function

    Function Desactiva_Contact_Status_DOL(ByVal strTelefono As String, ByVal strDocSap As String) As Boolean

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogActivacionPrepago")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogActivacionPrepago")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = strDocSap & "|" & strTelefono

        Dim blnOK As Boolean

        Try
            Dim objSicarDB As New COM_SIC_Activaciones.clsBDSiscajas
            Dim strMensaje As String
            Dim strCodRespuesta As String

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio Desactiva_Contact_Status_DOL-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strTelefono : " & strTelefono)

            blnOK = objSicarDB.FP_Desactiva_Contact_Status(strTelefono, strMensaje, strCodRespuesta)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  strMensaje : " & strMensaje)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  strCodRespuesta : " & strCodRespuesta)


        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: Desactiva_Contact_Status_DOL)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  ERROR : " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin Desactiva_Contact_Status_DOL-----------")
            objFileLog = Nothing
        End Try

        Return blnOK
    End Function

    Function ValidacionVentaDOL(ByVal nroDocumSAP As String, ByVal nroPedido As String, ByVal strPuntoVenta As String) As Boolean

        Dim dsPedido As New DataSet
        Dim objPagos As New SAP_SIC_Pagos.clsPagos
        Dim strCodArticulo As String
        Dim flag As Boolean = False

        Try
            dsPedido = objPagos.Get_ConsultaPedido("", strPuntoVenta, nroDocumSAP, "")

            If Not IsNothing(dsPedido) Then
                If dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") And _
                    dsPedido.Tables(0).Rows(0).Item("CLASE_VENTA") = ConfigurationSettings.AppSettings("strDTVAlta") Then

                    For i As Integer = 0 To dsPedido.Tables(1).Rows.Count - 1
                        strCodArticulo = dsPedido.Tables(1).Rows(i).Item("ARTICULO")
                        ' Validacion de la Venta de Pack Simcard
                        If strCodArticulo.Substring(0, 2) = "PS" Then
                            flag = True
                            Exit For
                        End If
                    Next

                    ' Validacion si es Prepago Portabilidad
                    Dim objCajas As New COM_SIC_Cajas.clsCajas
                    Dim dsTelefPorta As New DataSet
                    Dim strTipoVenta As Integer = 0
                    Dim retornoPorta As Integer

                    If Not Trim(nroPedido) = "" Then
                        Try
                            dsTelefPorta = objCajas.FP_Get_TelefonosPorta(nroPedido, strTipoVenta, retornoPorta)
                        Catch ex As Exception
                            retornoPorta = 1
                        End Try

                        ' Retorna Datos Portabilidad
                        If (retornoPorta = 0) And (Not IsNothing(dsTelefPorta)) Then
                            flag = False
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            flag = False
        Finally
            objPagos = Nothing
        End Try

        Return flag

    End Function

    Function AnularDocSapPagado(ByVal strNroDocSap As String, ByVal CodImpresionTicket As String, ByVal strUsuario As String, ByVal strPuntoVenta As String, ByVal strCanal As String, ByRef strMensaje As String) As String

        Dim oAnular As New clsAnulaciones
        Dim drDocSapPagado As DataRow
        Dim strCodRespuesta As String = "0"
        Try
            ' Obtener Documento Pagado
            drDocSapPagado = ObtenerDocSapPagado(strNroDocSap, strUsuario, strPuntoVenta)
            ' Anulación en Sap de Documento Pagado
            oAnular.AnularViasPago(Funciones.CheckStr(drDocSapPagado("VBELN")), _
                                    Funciones.CheckStr(drDocSapPagado("FKART")), _
                                    Funciones.CheckStr(drDocSapPagado("XBLNR")), _
                                    Funciones.CheckStr(drDocSapPagado("FKDAT")), _
                                    Funciones.CheckStr(drDocSapPagado("PEDIDO")), _
                                    Funciones.CheckStr(drDocSapPagado("NRO_DEP_GARANTIA")), _
                                    Funciones.CheckStr(drDocSapPagado("NRO_REF_DEP_GAR")), _
                                    Funciones.CheckStr(drDocSapPagado("NRO_CONTRATO")), _
                                    Funciones.CheckStr(drDocSapPagado("NRO_OPE_INFOCORP")), _
                                    Funciones.CheckStr(drDocSapPagado("CODIGO_APROBACIO")), _
                                    strCanal, _
                                    strPuntoVenta, _
                                    CodImpresionTicket, _
                                    strUsuario, _
                                    Funciones.CheckDbl(drDocSapPagado("PAGOS")), _
                                    1, _
                                    "")
        Catch ex As Exception
            strCodRespuesta = "1"
            strMensaje = ex.Message.ToString()
        Finally
            drDocSapPagado = Nothing
            oAnular = Nothing
        End Try

    End Function

    Function ObtenerDocSapPagado(ByVal strNroDocSap As String, ByVal Usuario As String, ByVal PuntoVenta As String) As DataRow
        Dim dsReturn As DataSet
        Dim drDocSapPagado As DataRow
        Dim oPagos As New SAP_SIC_Pagos.clsPagos
        Dim nroFactura As String = strNroDocSap
        Dim fecha As String = String.Format("{0:dd/MM/yyyy}", Now)
        Try
            ' Obtener Documento Sap Pagado del Cliente
            'dsReturn = oPagos.Get_ConsultaPagosUsuario(fecha, fecha, "", Usuario, PuntoVenta)
            dsReturn = oPagos.Get_ConsultaPagosUsuario(fecha, "", "", Usuario, PuntoVenta)
            'Incidencia demora en la carga de pool documentos pagados --> Solicidtud de Luis Palacios enviar solo una fecha 18/06/2012

            If (Not IsNothing(dsReturn)) AndAlso (dsReturn.Tables(0).Rows.Count > 0) Then
                Dim fila As DataRow
                For Each fila In dsReturn.Tables(0).Rows
                    If Funciones.CheckStr((fila.Item("VBELN"))) = nroFactura Then
                        drDocSapPagado = CType(fila, DataRow)
                        Exit For
                    End If
                Next
                fila = Nothing
            End If
        Catch ex As Exception
            drDocSapPagado = Nothing
        End Try
        dsReturn = Nothing
        Return drDocSapPagado
    End Function

    Public Sub AnulacionDePagoSAP(ByVal strNumDocSap As String, _
                                ByVal strNumDocSunat As String, _
                                ByVal strFechaPago As String, _
                                ByVal strViaPagos() As String, _
                                ByVal strTipoTienda As String, _
                                ByVal strOficina As String, _
                                ByVal strUsuario As String, ByVal strMontos() As String)

        Dim i As Integer
        Dim objCaja As New COM_SIC_Cajas.clsCajas
        Dim obSAP As New clsPagos
        Dim objSapCajas As New SAP_SIC_Cajas.clsCajas
        Dim dsResult As DataSet
        Dim dsAcuerdo As DataSet
        Dim strNroSEC As String
        Dim strContrato As String

        Dim intOper As Double
        If strTipoTienda = "MT" Then 'si es un CAC
            dsResult = obSAP.Get_ConsultaAnulacion(strNumDocSap)
            If dsResult.Tables(0).Rows.Count > 0 Then
                Dim drMsg As DataRow
                For Each drMsg In dsResult.Tables(0).Rows
                    If CStr(drMsg("TYPE")) = "E" Then
                        Throw New ApplicationException(CStr(drMsg("MESSAGE")))
                    End If
                Next
            End If
        End If
        Dim viaPago As String
        'For Each viaPago In strViaPagos
        For i = 0 To UBound(strViaPagos)
            viaPago = strViaPagos(i)
            If strTipoTienda = "MT" Then 'si es un CAC
                'dsResult = obSAP.Get_RegistroAnulPagos(strFechaPago, strNumDocSunat, viaPago, strOficina, strUsuario)
                dsResult = objSapCajas.Set_AnulPagosCajero(strFechaPago, strNumDocSunat, viaPago, strOficina, strUsuario)
            Else
                'dsResult = obSAP.Get_RegistroAnulPagos(strFechaPago, strNumDocSunat, viaPago, strOficina, "")
                dsResult = objSapCajas.Set_AnulPagosCajero(strFechaPago, strNumDocSunat, viaPago, strOficina, "")
            End If
            If dsResult.Tables(0).Rows.Count > 0 Then
                Dim drMsg As DataRow
                For Each drMsg In dsResult.Tables(0).Rows
                    If CStr(drMsg("TYPE")) = "E" Then
                        Throw New ApplicationException(CStr(drMsg("MSG")))
                    End If
                Next
            End If
        Next
    End Sub

    Public Function RollBackSot(ByVal nroSec As String, ByVal nroDocSap As String, ByVal IpTerminal As String) As SGAResponseVenta
        Dim oTransaccion As New SGATransaction
        Dim idLog As String = nroDocSap & " - " & nroSec
        Dim oSGAResponseTrs As New SGAResponseVenta
        Dim observacion As String
        Try
            Dim oAudit As New ItemGenerico
            oAudit.CODIGO = nroSec
            oAudit.DESCRIPCION = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            oAudit.DESCRIPCION2 = IpTerminal
            Dim nroSot As String = "0"
            observacion = nroSec
            oSGAResponseTrs = oTransaccion.AnularSot(nroSot, observacion, oAudit)
        Catch ex As Exception
            oSGAResponseTrs.codRepuesta = -99
            oSGAResponseTrs.msgRepuesta = ex.Message.ToString()
        End Try
        Return oSGAResponseTrs
    End Function

End Class
