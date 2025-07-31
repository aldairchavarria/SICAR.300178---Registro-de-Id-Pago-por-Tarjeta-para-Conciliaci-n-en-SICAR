<%@ Page Language="vb" Codepage="1252"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>OperacionesImp</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<link href="../estilos/est_General.css" rel="styleSheet" type="text/css">
		
		<script language="Javascript">
					function doprint() {
						printbtn.style.visibility = "HIDDEN";
						window.print();
						printbtn.style.visibility = "VISIBLE";
					}
		</script>
		
		
		<script runat="server">

	public Function negativo(byval cadena as string) as string
		if(len(trim(cadena))>4) then
			negativo = "(" + trim(mid(cadena,2)) + ")"
		else
			negativo = cadena
		end if
	end Function

	public Function cant_dec(byval cantidad as string) as string
	   Dim ok as integer = 0
	   dim i as integer

	   for i=1 to len(cantidad)
			if(mid(cantidad,i,1)=".") then
				ok = len(mid(trim(cantidad),i+1))
			end if
	   Next
	   if ok=0 then cant_dec = cantidad & ".00"
	   if ok=1 then cant_dec = cantidad & "0"
	   if ok>1 then cant_dec = cantidad
	end Function

 	public	Function no_dec(byval valor as string) as string
		Dim cant =""
		dim i as integer

		for i=1 to len(valor)
			if mid(cstr(valor),i,1)="." then
				exit for
			else
				cant = cant + mid(valor,i,1)
			end if
		Next
		no_dec = cant
	end Function

	public function valida_dato(byval valor as string) as string
		if(len(trim(valor)) > 1) then
			valida_dato = valor
		else
			valida_dato = "&nbsp;"
		end if
	end Function

	public function format(byval cadena as string) as string
		Dim dec, ent,  aux as string
		dim i, j as integer
		j=0
	    for i=1 to len(cadena)
			if (mid(cadena,i,1)=".") then
				dec = mid (cadena,i)
				ent = mid(cadena,1,j)
			else
				j =j + 1
			end if
		Next
		if len(ent) > 3 and len(ent) < 6 then
			aux = mid(ent,1,len(ent)-3)
			ent = aux + "," + mid(ent,len(aux)+1)
		end if
		format =  ent + dec
	end Function


	Function Dist_Desc(byval cadena1 as string, byval cadena2 as string) as string
		Dim objSap as new SAP_SIC_Pagos.clsPagos
		dim dsDist as System.Data.DataSet = objSap.Get_ConsultaDistritos()
		dim sReturn as string = ""
		if dsDist.tables(0).rows.count>0 then
			dim drFila as System.Data.DataRow
			for each drFila in dsDist.tables(0).rows
				if trim(cstr(drFila(1)))=cadena1 andalso trim(cstr(drFila(2)))=cadena2 then
					sReturn = trim(cstr(drFila(3)))
					exit for
				end if
			next
		end if
		return sReturn
	end Function


		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" runat="server">
			<div id="printbtn" STYLE="LEFT:50px; POSITION:absolute; TOP:0px">
				<!--<a href="javascript:doprint();"><img src="../../images/botones/btn_Imprimir.gif" border="0" style="cursor:hand;"></a>&nbsp;&nbsp;-->
				<!--<a href="javascript:window.close();"><img src="../../images/botones/btn_Cerrar.gif" border="0" style="cursor:hand;"></a>-->
			</div>
			<!-- Cabecera BOLETA MUNDOS-->
			<%

		Dim intNumeroCuotas, strNuevaDescrDocumento
    '*** PMO - CUOTAS EN VENTA RAPIDA - FIN ***

    Dim strNbsp

	Dim strCabeceraComprobante
	Dim strDetalleComprobante
	Dim strAdicionalComprobante
	Dim strPieComprobante

	Dim objComponente
	Dim objRecordSet, objRecordSetA, objRecordSetB, objRecordSetC, objRecordSetD, objRecordSetE
	Dim strTipoOficina, oficinaVenta, fechaVenta, docSap
	Dim sValorA, sValorB, msgErr, total, i, ajuste, ttotal, cant,StrXml
    Dim tipo,desc,recarga, scFecha, sdist, sdept, sprov, sfechan, strTemp
	Dim StrXmlNumRef,objRecordSetPagos,strInfoSunat,OutPutParams

	Dim ErrorMessage
	Dim IpMetaFrame,PorMetaFrame
	dim posicion
	Dim objComVen, StrXmlVen, strNum_Doc, objComCam, StrXmlCam, objRSCam, strMsgGlosa
	Dim numtelPre, numtelPos
	Dim TipoTicket

	'********************************
	dim strMarca as string = "" 'temporal
	dim strRaz as string = ""
	dim msgResp as string
	dim cadRV as string = "0"
	'*************************
	dim recargaefectiva, valorventa, descuento, igv, totalpago, numrecarga
	'********************************
	dim objComponentePagos as SAP_SIC_Pagos.clsPagos
	dim objAct as COM_SIC_Activaciones.clsConsultaMsSap	
	Dim objOffline as COM_SIC_OffLine.clsOffline
	
	dim dsTmp as System.Data.DataSet
	'*********************************************************************************************'
	dim dsCompro as System.Data.DataSet		'datse primcipal con el que se muestran los datos
	'*********************************************************************************************'
	
	'*********************************************************************************************
	dim dsPedidoSap as System.Data.DataSet     'dataset para almacenar datos del pedido PROY-23700-IDEA-29415
	'*********************************************************************************************
	
	dim dsComproOffline as System.Data.DataSet
	dim cteMSG_GLOSA_VENTA_CUOTAS as string
	dim cteMSG_GLOSA_RECARGAVIRTUAL_1 as string
	dim cteMSG_GLOSA_RECARGAVIRTUAL_2 as string
	dim k_Prefijo_Ticket  as string
	dim dsOficina as System.Data.DataSet
	
	Dim fromOffline as Boolean

	'*****************************
	Dim strUltNum as string
	Dim UltNumstrRef as string
	Dim NumRefAsigDocum AS String
	'*****************************
	Dim dtIGV As System.Data.DataTable
	Dim IGVactual As string 
	IGVactual = "0"
	

	'*******************************
	' VARIABLES DE SESION
	'*******************************
	Dim Const_IGV as String 
	Const_IGV = "0"
	

		
		
	
	strTipoOficina = Session("CANAL")
	
	'*********************************************************************************
	'Variables utiles para la impresiòn
	oficinaVenta = Session("ALMACEN")
	'codRefer
	docSap = request.QueryString("codRefer")              'strCodSAP=PEDIN_NROPEDIDO
	'*********************************************************************************
	
	cteMSG_GLOSA_VENTA_CUOTAS  = ConfigurationSettings.AppSettings("cteMSG_GLOSA_VENTA_CUOTAS")
	cteMSG_GLOSA_RECARGAVIRTUAL_1  = ConfigurationSettings.AppSettings("cteMSG_GLOSA_RECARGAVIRTUAL_1")
	cteMSG_GLOSA_RECARGAVIRTUAL_2  = ConfigurationSettings.AppSettings("cteMSG_GLOSA_RECARGAVIRTUAL_2")
	k_Prefijo_Ticket = ConfigurationSettings.AppSettings("k_Prefijo_Ticket")
	'*******************************



	posicion = "440"
	IpMetaFrame=Request.ServerVariables("REMOTE_ADDR")
	'para pruebas: 20040813
	'Response.Redirect "OperacionesImp_Meta.asp?codRefer=" & trim(request.QueryString("codRefer")) & "&FactSunat=" & trim(request.QueryString("FactSunat"))
	'Response.End


	IF INSTR(1,SESSION("IPMETAFRAME"),IpMetaFrame)>0 THEN
	   PorMetaFrame="S"
	   Response.Redirect("OperacionesImp_Meta.aspx?codRefer=" + trim(request.QueryString("codRefer")) + "&FactSunat=" + trim(request.QueryString("FactSunat")))
	   Response.End
	else
	   PorMetaFrame="N"
	END IF

	fromOffline = request.QueryString("isOffline")="1"
	
	'-------------------------------------------------------------------------------------------------------------'
	objComponentePagos = new SAP_SIC_Pagos.clsPagos()		'***************Get_ConsultaComprobante*************
	objAct= new COM_SIC_Activaciones.clsConsultaMsSap()	
	'-------------------------------------------------------------------------------------------------------------'
	
	
	if(fromOffline) then
		objOffline = new COM_SIC_OffLine.clsOffline()	
	end if

	'-------------------------------------------------------------------------------------------------------------'
	'Repite la line Nº 222. dgueval :: analizando el codigo ::
	objComponentePagos = new SAP_SIC_Pagos.clsPagos()
	'-------------------------------------------------------------------------------------------------------------'

IF MID(TRIM(cstr(request.QueryString("FactSunat"))),1,2)=k_Prefijo_Ticket and  trim(cstr(session("CodImprTicket")))<>"" then

	'--****Consulta a SAP ******--'
	objComponentePagos.Get_NumeroSUNAT(oficinaVenta,"ZFBR",trim(cstr(session("CodImprTicket"))),"x",UltNumstrRef,strUltNum)


	IF trim(request.QueryString("Reimpresion"))<>"" then
		if trim(UltNumstrRef)<>trim(cstr(request.QueryString("FactSunat"))) then
'			Response.write("")
%>
			<script language="javascript">
			  alert('No se puede Reimprimir este Documento <%=trim(cstr(request.QueryString("FactSunat")))%> debido a que el ultimo documento impreso es el <%=UltNumstrRef%>');
			</script>
			<%
			Response.End
		end if
	END IF
END IF
%>
		<!-- Inicio Comentado porque no se consulta a SAP
				'aca estaba unas consulta a SAP
			FIn Comentado porque no se consulta a SAPInicio Comentado porque no se consulta a SAP-->
			<%
			
			'**********************************************************************'
			'**Consulta SAP:
			'Comentado porque no se consulta a SAP
			'dsOficina = objComponentePagos.Get_ParamGlobal(Session("ALMACEN"))
			'**********************************************************************'
			

'IF (trim(dsOficina.Tables(0).Rows(0).Item("IMPRESION_SAP"))<>"" and trim(request.QueryString("Reimpresion"))="") then

'*****************Inicio COmentado
'if dsOficina.Tables(0).Rows(0).Item("IMPRESION_SAP")<>"" and trim(Session("PoolPagados")) <> "1" then
 '  Response.End
'end if
'*****************Fin COmentado
'Session("PoolPagados")=""

'IF (trim(session("varval121"))<>"" and not( trim(cstr(session("CodImprTicket")))="" and  mid(trim(cstr(request.QueryString("FactSunat"))),1,2)=k_Prefijo_Ticket )) then 'and trim(cstr(session("CodImprTicket")))<>"" THEN

'*****************Inicio COmentado
'IF (trim(dsOficina.Tables(0).Rows(0).Item("IMPRESION_SAP"))<>"" and not( trim(cstr(session("CodImprTicket")))="" and  mid(trim(cstr(request.QueryString("FactSunat"))),1,2)=k_Prefijo_Ticket )) then 'and trim(cstr(session("CodImprTicket")))<>"" THEN
'**********************
'  IF trim(request.QueryString("Reimpresion"))<>"" THEN
'		dsTmp = objComponentePagos.Set_PrintDocumentoSAP(Cstr(docSap))
'		If dsTmp.Tables(0).Rows.Count > 0 Then
'			Dim drMsg As System.Data.DataRow
'			For Each drMsg In dsTmp.Tables(0).Rows
'				If CStr(drMsg("TYPE")) = "E" Then
'					msgResp = "Error :" + CStr(drMsg("MESSAGE"))
'				End If
'				If CStr(drMsg("TYPE")) = "I" Then
'					msgResp = CStr(drMsg("MESSAGE"))
'				end if
'			Next
'		else
'			msgResp = "Error de Impresion por SAP"
'		End If
		
		'Response.Write("")
'		Response.End
 ' ELSE
 '  		Response.End
 '  END IF
 '**********************
'END If '(Agregado)
'*****************Fin COmentado
'ELSE (comentado)
IF "1"="1" '(Agregado)
'**********************
	
	'*** 
	'Response.Write("docSap: " & docSap & " oficinaVenta: " & oficinaVenta)
	
	
	'*****************************************************************************************
	'QUERY PRINCIPAL: CONECTANDO A SAP ACTUALMENTE.
	'dsCompro = objComponentePagos.Get_ConsultaComprobante(Cstr(docSap),Cstr(oficinaVenta))
	'--NUEVA LLAMDA AL MÈTODO:
	Session("msgErrorGenerarSot")=""
	dsCompro=objAct.ConsultarComprobante(Cstr(docSap))
	
	'PROY-23700-IDEA-29415 - INI	
	dsPedidoSap = objAct.ConsultaPedido(Cstr(docSap),"","")
	'PROY-23700-IDEA-29415 - FIN
	
	'***************************************************************************************************
	
	'MODIFICADO FFS INICIO
	if(fromOffline) then
		dsComproOffline = objOffline.GetConsultaPedido(docSap)			'PENDIENTE DE CONSULTA
		
		
		Dim dtFechaIGV as string = Convert.ToDateTime(Convert.ToString(dsComproOffline.Tables(0).Rows(0)("AUDAT"))).ToString("dd/MM/yyyy")
		dtIGV = Session("Lista_Impuesto")
		if dtIGV.rows.count = 0 then
			%>
				<script language="javascript">
					alert('No existe IGV configurado');
				</script>
			<% Response.end
		end if
		For Each row As System.Data.DataRow In dtIGV.Rows
			If (CDate(dtFechaIGV) >= CDate(row("impudFecIniVigencia").ToString.Trim) And CDate(dtFechaIGV) <= CDate(row("impudFecFinVigencia").ToString.Trim) And CInt(row("impunTipDoc").ToString.Trim) = 0) Then
				IGVactual = Math.Round(CDec(row("IGV").ToString.Trim()) / 100, 2).ToString
				Const_IGV = Cint((CDec(IGVactual)*100)).ToString
				Exit For
			End If
		Next
	
	
	end if
	'MODIFICADO FFS FIN
	
	
	'--------------------------------------------------------------------------------------------------------------------------------------'
	'El procedimiento SSAPSS_IMPRESIONTICKET retorna un dataset con 4 tablas.
	'El bloque serà comentado para evitar los errores...07/11/2014
	'If dsCompro.Tables(5).Rows.Count > 0 Then ' modificado JCR
		'If Not dsCompro Is Nothing Then
		'	Dim drMsg As System.Data.DataRow
		'	For Each drMsg In dsCompro.Tables(5).Rows		'** Error de referencia a la tabla de la consulta, solo hay 4 tablas
		'		If CStr(drMsg("TYPE")) = "E" Then
		'			msgErr = CStr(drMsg("MSG"))
		'		End If
		'	Next
		'else
		'	msgErr = "Error en el de Documento de Impresión " + docSap + " " + oficinaVenta
		'End If
	'--------------------------------------------------------------------------------------------------------------------------------------'

	'If dsCompro.Tables(0).Rows.Count > 0 Then 'modificado JCR
	'MODIFCADO POR FFS INICIO
	if(fromOffline) then
		'----------------------------------------------------------------------------------------'
		'**consultar**:
		NumRefAsigDocum = dsComproOffline.Tables(0).Rows(0)("XBLNR")		'PENDIENTE DE CONSULTA
	end if
	'MODIFICADO POR FFS FIN
	
	If Not dsCompro Is Nothing Then
		If dsCompro.Tables(0).Rows.Count > 0 then
		'----------------------------------------------------------------------------------------'
		'RECUPERA EL Nº DE REFERENCIA QUE SERIA LA COLUMNA: NRO_REFERECIA
			NumRefAsigDocum=replace(trim(cstr(dsCompro.Tables(0).Rows(0)(5))),"*","")
		'----------------------------------------------------------------------------------------'
			
			'Validaciòn:
			if trim(request.QueryString("Reimpresion"))="" then
				if mid(cstr(request.QueryString("FactSunat")),1,2)=k_Prefijo_Ticket and  trim(cstr(session("CodImprTicket")))<>"" then
					if trim(NumRefAsigDocum)<>trim(UltNumstrRef) then
						Response.End
					end if
				end if
			end if
			
		End IF
	End If

    'if dsCompro.Tables(0).Rows.Count > 0 then 'modificado JCR
	If Not dsCompro Is Nothing Then
		
            if(fromOffline) then
				'*** En la version 6.0 no va entrar en los fromOffline ***'
               recargaefectiva= cdec(dsComproOffline.Tables(1).Rows(0)("REC_EFECTIVA"))'0
               valorventa= cdec(dsComproOffline.Tables(1).Rows(0)("VAL_VENTA"))'VALORVENTA
               descuento= cdec(dsComproOffline.Tables(1).Rows(0)("DESCUENTO"))'DESCUENTO
               igv= cdec(dsComproOffline.Tables(1).Rows(0)("IGV"))'IGV
               totalpago= cdec(dsComproOffline.Tables(1).Rows(0)("TOTAL_PAGO"))'TOTAL
               numrecarga= cstr(dsComproOffline.Tables(1).Rows(0)("NRO_REC_SWITCH"))'# RECAGRA ST
               cadRV="1"
            Else
            '=========================================================================================================='
            '10/11/2014 09:30am
            'Colsultar los datos referente a lo que retornar el procedure:
            'Voy a comentar el codigo para seguir avanznado mientras se consulta el tema- 
            '=========================================================================================================='
				'*****Recarga Virtual****
				'consulta para recargas virtuales
				'If dsCompro.Tables(4).Rows.Count > 0 Then		'linea actual
				'If dsCompro.Tables(1).Rows.Count > 0 Then		'linea de test	
				'	'IF cdec(dsCompro.Tables(4).Rows(0)(4))>0 THEN		'Error... no hay tabla 4
				'	IF cdec(dsCompro.Tables(1).Rows(0)(9))>0 THEN		'asumo que es: PEDIN_RECEFECTIVA
				'	'**consultar, campos para las variables +++++++++++++++++++++++++++++++
				'		recargaefectiva= cdec(dsCompro.Tables(4).Rows(0)(4))
				'		valorventa= cdec(dsCompro.Tables(4).Rows(0)(5))
				'		descuento= cdec(dsCompro.Tables(4).Rows(0)(6))
				'		igv= cdec(dsCompro.Tables(4).Rows(0)(7))
				'		totalpago= cdec(dsCompro.Tables(4).Rows(0)(8))
				'		numrecarga= cstr(dsCompro.Tables(4).Rows(0)(9))
				'		cadRV="1"
				'	END IF
				'END IF
			'=========================================================================================================='
			END IF
			
			'*****Recarga Virtual****
			If dsCompro.Tables(0).Rows.Count > 0 then
				'**consultar: 
				tipo = trim(cstr(dsCompro.Tables(0).Rows(0)(8)))
			else
				if(fromOffline) then
					tipo = "ZBOL"
				else
					tipo = ""
				end if				
			end if

			'lINEA EN PRODUCCION - 
			'TipoTicket=left(NumRefAsigDocum,2)		'ME EXTRAE LOS DOS DIGITOS D ELA DERECHA DE LA REFERENCIA.
			
			'************************************************'
			'**POR EL TIPO DE DOCUMENTO: SE CAMBIARA TEMPORAL:
			'**Camianos el Key del Ticket de 12 por T
			'<add key="k_Prefijo_Ticket" value="T" />
			TipoTicket=left(NumRefAsigDocum,1)
			'************************************************'
			

			'CARIAS: Mensaje de error mostrado en javascript y ya no con un response.write
			'HHA - 11/01/2006   : Redireccionando a los nuevos formatos de boletas y facturas

		'==================================================================================================================================================
		'" ":
		'CORTA LA IMPRESION DEBIDO A QUE NO ESTA TRAYENDO EL TIPO DOCUMENTO
		'==================================================================================================================================================
            'PROY-23700-IDEA-29415 - INI CAMBIO
            Dim strClaseFactura = String.Empty
            Dim strClaseFacturaNotaCanje = ConfigurationSettings.AppSettings("ClaseNotaCanje").ToString()
            If not dsPedidoSap Is Nothing AndAlso dsPedidoSap.Tables.Count > 0 AndAlso dsPedidoSap.Tables(0).Rows.Count > 0 Then
				strClaseFactura = dsPedidoSap.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA").ToString()
            End If 
			if len(trim(tipo)) = 0 and strClaseFactura <> strClaseFacturaNotaCanje then 
			'PROY-23700-IDEA-29415 - FIN
			%>            
			<script language="javascript">
                alert('<%="No existe tipo de documento de impresión asociado a este trámite." + docSap & "Tipo Doc:" & tipo%>');
			</script>
			<%  Response.end
			end if
		'==================================================================================================================================================
            'PROY-23700-IDEA-29415 - INI CAMBIO
			if (len(tipo) > 0 OR strClaseFactura = strClaseFacturaNotaCanje) then
            'PROY-23700-IDEA-29415 - FIN
				'if (tipo="ZFAC" or tipo="ZBOL") and (session("VarVal2") = "MT") then
				'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
				'if TipoTicket=k_Prefijo_Ticket then
				'if TipoTicket=k_Prefijo_Ticket or (TipoTicket="07" and mid(NumRefAsigDocum,4,1) <> "0") then
					
										
					Dim cadenaDTH as string
					cadenaDTH = "&strCodPago=" & request.QueryString("strCodPago") & "&strFechaExpira=" & request.QueryString("strFechaExpira") & "&strNroRecarga=" & request.QueryString("strNroRecarga")
					'Response.Redirect("ImpresionTicket.aspx?codRefer=" & trim(request.QueryString("codRefer")) & "&FactSunat=" & trim(request.QueryString("FactSunat")) & "&Reimpresion=" & trim(request.QueryString("Reimpresion")) & "&strEfectivo=" & request.QueryString("strEfectivo") & "&strRecibido=" & request.QueryString("strRecibido") & "&strEntregar=" & request.QueryString("strEntregar") & cadenaDTH & "&flagVentaEquipoPrepago=" & request.QueryString("flagVentaEquipoPrepago") & "&isOffline=" & request.QueryString("isOffline"))
					
					'*******30-04-2015***'
					'Dim strRecibidoUS As String = "0"
					'Response.Redirect("sicar_fe_comprobante.aspx?codRefer=" & trim(request.QueryString("codRefer")) & "&FactSunat=" & trim(request.QueryString("FactSunat")) & "&Reimpresion=" & trim(request.QueryString("Reimpresion")) & "&strEfectivo=" & request.QueryString("strEfectivo") & "&strRecibido=" & request.QueryString("strRecibido") & "&strRecibidoUS=" & strRecibidoUS & "&strEntregar=" & request.QueryString("strEntregar") & cadenaDTH & "&flagVentaEquipoPrepago=" & request.QueryString("flagVentaEquipoPrepago") & "&isOffline=" & request.QueryString("isOffline")) 
					
					'PROY-23700-IDEA-29415 - INI					
					if strClaseFactura = strClaseFacturaNotaCanje then
						Response.Redirect("sicar_nota_canje.aspx?codRefer=" & trim(request.QueryString("codRefer")) & "&FactSunat=" & trim(request.QueryString("FactSunat")) & "&Reimpresion=" & trim(request.QueryString("Reimpresion")) & "&strEfectivo=" & request.QueryString("strEfectivo") & "&strRecibido=" & request.QueryString("strRecibido") & "&strRecibidoUS=" & request.QueryString("strRecibidoUS") & "&strEntregar=" & request.QueryString("strEntregar") & cadenaDTH & "&flagVentaEquipoPrepago=" & request.QueryString("flagVentaEquipoPrepago") & "&isOffline=" & request.QueryString("isOffline")& "&strOrigenVenta=" & request.QueryString("strOrigenVenta"))
					else	
					Response.Redirect("sicar_fe_comprobante.aspx?codRefer=" & trim(request.QueryString("codRefer")) & "&FactSunat=" & trim(request.QueryString("FactSunat")) & "&Reimpresion=" & trim(request.QueryString("Reimpresion")) & "&strEfectivo=" & request.QueryString("strEfectivo") & "&strRecibido=" & request.QueryString("strRecibido") & "&strRecibidoUS=" & request.QueryString("strRecibidoUS") & "&strEntregar=" & request.QueryString("strEntregar") & cadenaDTH & "&flagVentaEquipoPrepago=" & request.QueryString("flagVentaEquipoPrepago") & "&isOffline=" & request.QueryString("isOffline")& "&strOrigenVenta=" & request.QueryString("strOrigenVenta"))
					end if
					
					'PROY-23700-IDEA-29415 - FIN
					
					
					
					'*********************'
					
					Response.End()
				'end if
				'end if
				'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

				'+++VALIDA CON TIPOS, ACTUALMENTE NO ESTA ENVIANDO NINGUN TIPO +++++
				if tipo="FAC" or tipo="B/V" or tipo="N/C" or tipo="NPED" or tipo="DEV" then
				   Response.Redirect( "OperacionesImp_DAC.aspx?codRefer=" & trim(request.QueryString("codRefer")) & "&FactSunat=" & trim(request.QueryString("FactSunat")))
				   Response.End
				end if
				Session("codRefer") = ""
				    'Response.Write tipo
				'Las Notas de Devolucion se muestran como Nota de Pedido
				if tipo="ZNDV" then
				   tipo="NPED"
				end if
				if(tipo="ZDAU") then
					recarga =  cdec(dsCompro.Tables(0).Rows(0)(9)) 'Trim(objRecordSetA.Fields(9).Value)
					if(fromOffline) then
						recarga =  cdec(dsComproOffline.Tables(1).Rows(0)("REC_EFECTIVA")) 'Trim(objRecordSetA.Fields(9).Value)
					end if

					%>
			<div ID="" STYLE="LEFT:30px; POSITION:absolute; TOP:80px" class="clsCourier10">
				<table border="0" bordercolor="#000000" width="650">
					<tr class="clsCourier10B">
						<td align="center" colspan="3">DOCUMENTO AUTORIZADO</td>
					</tr>
					<tr>
						<td colspan="3">&nbsp;</td>
					</tr>
					<tr>
						<td colspan="3">_____________________________________________________________________________________________</td>
					</tr>
					<tr class="clsCourier10">
						<td colspan="2">Operación:</td>
						<td align="right"><%=Trim(cstr(dsCompro.Tables(0).Rows(0)(3)))%></td>
					</tr>
					<tr>
						<td colspan="3">_____________________________________________________________________________________________</td>
					</tr>
					<tr>
						<td colspan="3">&nbsp;</td>
					</tr>
					<tr class="clsCourier10B">
						<td colspan="2">AMERICA MOVIL PERU S.A.C.</td>
						<td align="right">RUC:20467534026</td>
					</tr>
					<tr>
						<td class="clsCourier10B">DOCUMENTO AUTORIZADO</td>
						<td class="clsCourier10">:</td>
						<td class="clsCourier10"><%= mid(cstr(dsCompro.Tables(0).Rows(0)(5)),2)  %></td>
					</tr>
					<tr>
						<td class="clsCourier10B">RAZON SOCIAL/NOMBRE</td>
						<td class="clsCourier10">:</td>
						<td class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(0))) %></td>
					</tr>
					<tr>
						<td class="clsCourier10B">RUC/DNI</td>
						<td class="clsCourier10">:</td>
						<td class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(2)))  %></td>
					</tr>
				</table>
			</div>
			<%				else
					strCabeceraComprobante = ""
					if(tipo="NPED") then %>
			<div ID="" STYLE="LEFT:670px; POSITION:absolute; TOP:20px" class="clsCourier10"><%'= now.Date.ToString("d")%></div>
			<div ID="" STYLE="LEFT:655px; POSITION:absolute; TOP:40px" class="clsCourier10"><%= now.ToString("t") %></div>
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:40px" class="clsCourier10"><%=strRaz%></div> <!--CAP: Razon Social. Remover despues -->
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:55px" class="clsCourier10"><%=Trim(cstr(dsCompro.Tables(0).Rows(0)(7)))%></div>
			<div ID="" STYLE="LEFT:360px; POSITION:absolute; TOP:55px" class="clsCourier10"><%=strTipoOficina%></div>
			<div ID="" STYLE="LEFT:565px; POSITION:absolute; TOP:55px" class="clsCourier10"><%=oficinaVenta%></div>
			<div ID="" STYLE="LEFT:645px; POSITION:absolute; TOP:55px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(3))) %></div>
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:72px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(10))) %></div>
			<%
							strTemp = Trim(cstr(dsCompro.Tables(0).Rows(0)(11)))   'objRecordSetA.Fields(11).Value)
							if (len(strTemp)>21) then strTemp = Left(strTemp,21)
						%>
			<div ID="" STYLE="LEFT:290px; POSITION:absolute; TOP:72px" class="clsCourier10"><%= strTemp %></div>
			<%
							if (len(Trim(cstr(dsCompro.Tables(0).Rows(0)(1))))<35) then
						%>
			<div ID="" STYLE="LEFT:500px; POSITION:absolute; TOP:72px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(1))) %></div>
			<%
							else
						%>
			<div ID="" STYLE="LEFT:500px; POSITION:absolute; TOP:72px" class="clsCourier10"><%= Left(Trim(cstr(dsCompro.Tables(0).Rows(0)(1))),34) & "_" %></div>
			<div ID="" STYLE="LEFT:70px; POSITION:absolute; TOP:90px" class="clsCourier10"><%= Mid(Trim(cstr(dsCompro.Tables(0).Rows(0)(1))),35) %></div>
			<%
							end if

							sdist = Trim(cstr(dsCompro.Tables(0).Rows(0)(15)))
							if (sdist<>"") then
								sdept = Mid(sdist,3,3)
								sprov = Mid(sdist,6)
							else
								sdept = ""
								sprov = ""
							end if
						%>
			<div ID="" STYLE="LEFT:340px; POSITION:absolute; TOP:90px" class="clsCourier10"><%=Dist_Desc(sdept,sprov)%></div>
			<%
							sfechan = Trim(cstr(dsCompro.Tables(0).Rows(0)(12)))
							if (sfechan="12:00:00 a.m.") then sfechan = ""
						%>
			<div ID="" STYLE="LEFT:650px; POSITION:absolute; TOP:90px" class="clsCourier10"><%= sfechan %></div>
			<div ID="" STYLE="LEFT:150px; POSITION:absolute; TOP:110px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(13))) %></div>
			<%
							select case Trim(cstr(dsCompro.Tables(0).Rows(0)(14)))
								case "1"
						%>
			<div ID="" STYLE="LEFT:390px; POSITION:absolute; TOP:110px" class="clsCourier10">X</div>
			<%
								case "2"
						%>
			<div ID="" STYLE="LEFT:422px; POSITION:absolute; TOP:110px" class="clsCourier10">X</div>
			<%
								case "3"
						%>
			<div ID="" STYLE="LEFT:454px; POSITION:absolute; TOP:110px" class="clsCourier10">X</div>
			<%
								case "4"
						%>
			<div ID="" STYLE="LEFT:486px; POSITION:absolute; TOP:110px" class="clsCourier10">X</div>
			<%
								case "6"
						%>
			<div ID="" STYLE="LEFT:518px; POSITION:absolute; TOP:110px" class="clsCourier10">X</div>
			<%
								case "7"
						%>
			<div ID="" STYLE="LEFT:578px; POSITION:absolute; TOP:110px" class="clsCourier10">X</div>
			<%
							end select
						%>
			<div ID="" STYLE="LEFT:640px; POSITION:absolute; TOP:110px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(2))) %></div>
			<%					else
						IF	trim(tipo) = "ZBOL" THEN	%>
			<!--	DATOS PARA LA IMPRESION DE LA BOLETA	125-->
			<%						
						if(fromOffline) then
						strCabeceraComprobante = Mid(Trim(CStr(NumRefAsigDocum)), 2) & ";"
						strCabeceraComprobante = strCabeceraComprobante & Trim(cstr(dsComproOffline.Tables(0).Rows(0)("NOMBRE_CLIENTE"))) & ";"
						strCabeceraComprobante = strCabeceraComprobante & ";"
						strCabeceraComprobante = strCabeceraComprobante & Trim(cstr(dsComproOffline.Tables(0).Rows(0)("CLIENTE"))) & ";" ' DOCUMENTO CLIENTE
						strCabeceraComprobante = strCabeceraComprobante & Trim(cstr(dsComproOffline.Tables(0).Rows(0)("AUDAT"))) & ";"
						strCabeceraComprobante = strCabeceraComprobante & Trim(cstr(dsComproOffline.Tables(0).Rows(0)("ID_T_TRS_PEDIDO"))) & ";"
						strCabeceraComprobante = strCabeceraComprobante & Trim(cstr(dsComproOffline.Tables(0).Rows(0)("VENDEDOR"))) & " " & Trim(SESSION("NOMBRE_COMPLETO")) & ";"
						else
							strCabeceraComprobante = Mid(Trim(cstr(dsCompro.Tables(0).Rows(0)(5))),2) & ";"
							strCabeceraComprobante = strCabeceraComprobante & Trim(cstr(dsCompro.Tables(0).Rows(0)(0))) & ";"
							strCabeceraComprobante = strCabeceraComprobante & Trim(cstr(dsCompro.Tables(0).Rows(0)(1))) & ";"
							strCabeceraComprobante = strCabeceraComprobante & Trim(cstr(dsCompro.Tables(0).Rows(0)(2))) & ";"
							strCabeceraComprobante = strCabeceraComprobante & Trim(cstr(dsCompro.Tables(0).Rows(0)(3))) & ";"
							'++error...
							If Convert.IsDBNull(dsCompro.Tables(0).Rows(0)(4)) Then
								strCabeceraComprobante = strCabeceraComprobante & "" & ";"
							else
								strCabeceraComprobante = strCabeceraComprobante & Trim(cstr(dsCompro.Tables(0).Rows(0)(4))) & ";"
							end iF 
							'++++++
							strCabeceraComprobante = strCabeceraComprobante & Trim(cstr(dsCompro.Tables(0).Rows(0)(6))) & " " & Trim(cstr(dsCompro.Tables(0).Rows(0)(7))) & ";"
						end if
			%>
			<%if fromOffline then%>
			<% dim fechaBD$
					fechaBD = Convert.ToString(dsComproOffline.Tables(0).Rows(0)("AUDAT"))
					Dim fecha$ = Convert.ToDateTime(fechaBD).ToString("dd/MM/yyyy")
					Dim objOff as new COM_SIC_OffLine.clsOffline
					Dim nombreVendedor$ = objOff.ObtenerNombreCajero(cstr(dsComproOffline.tables(0).rows(0)("VENDEDOR")))
				
				%>
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:85px" class="clsCourier10"><%=strRaz%></div>
			<div ID="" STYLE="LEFT:500px; POSITION:absolute; TOP:120px" class="clsCourier10"><%= Trim(cstr(dsComproOffline.Tables(0).Rows(0)("XBLNR"))) %></div> <!--Sunat-->
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:120px" class="clsCourier10"><%= Trim(cstr(dsComproOffline.Tables(0).Rows(0)("NOMBRE_CLIENTE"))) %></div> <!--Nombre-->
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:150px" class="clsCourier10"><%= Trim(Convert.ToString(dsComproOffline.Tables(0).Rows(0)("DIRECCION_CLIENTE")) )%></div> <!--Dirección  DIRECCION_CLIENTE-->
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:178px" class="clsCourier10"><%= Trim(cstr(dsComproOffline.Tables(0).Rows(0)("CLIENTE"))) %></div> <!--RUC-->
			<div ID="" STYLE="LEFT:320px; POSITION:absolute; TOP:178px" class="clsCourier10"><%= Trim(cstr(fecha)) %></div> <!--Fecha Factura--> <!-- .substring(6,2) + "/" + Trim(cstr(dsCompro.Tables(0).Rows(0)(3))).substring(4,2) + "/" + Trim(cstr(dsCompro.Tables(0).Rows(0)(3))).substring(0,4)   -->
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:193px" class="clsCourier10"><%= Trim(cstr(dsComproOffline.Tables(0).Rows(0)("ID_T_TRS_PEDIDO"))) %></div> <!--Referencia-->
			<div ID="" STYLE="LEFT:390px; POSITION:absolute; TOP:178px" class="clsCourier10"><%= Trim(cstr(dsComproOffline.Tables(0).Rows(0)("VENDEDOR"))) %><%= Trim(cstr(nombreVendedor)) %></div> <!--Usuario-->
			<% Else %>
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:85px" class="clsCourier10"><%=strRaz%></div> <!--CAP: Razon Social, eliminar despues-->
			<div ID="" STYLE="LEFT:500px; POSITION:absolute; TOP:120px" class="clsCourier10"><%= mid(Trim(cstr(dsCompro.Tables(0).Rows(0)(5))),2) %></div> <!--Sunat-->
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:120px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(0))) %></div> <!--Nombre-->
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:150px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(1))) %></div> <!--Dirección-->
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:178px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(2))) %></div> <!--RUC-->
			<div ID="" STYLE="LEFT:320px; POSITION:absolute; TOP:178px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(3)))%></div> <!--Fecha Factura--> <!-- .substring(6,2) + "/" + Trim(cstr(dsCompro.Tables(0).Rows(0)(3))).substring(4,2) + "/" + Trim(cstr(dsCompro.Tables(0).Rows(0)(3))).substring(0,4)   -->
			
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:193px" class="clsCourier10">
			<%= 
				Trim(cstr(dsCompro.Tables(0).Rows(0)(4)))
			%>
			</div> <!--Referencia-->
			
			<div ID="" STYLE="LEFT:390px; POSITION:absolute; TOP:178px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(6))).PadLeft(10, "0") %><%= cstr(dsCompro.Tables(0).Rows(0)(7)) %></div> <!--Usuario-->
			<%end if%>
			<%				ELSE	%>
			<!--	DATOS PARA LA IMPRESION DE LA FACTURA	145-->
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:85px" class="clsCourier10"><%= strRaz %></div> <!--CAP: Razon Social.Eliminar despues-->
			<div ID="" STYLE="LEFT:500px; POSITION:absolute; TOP:130px" class="clsCourier10"><%= mid(Trim(cstr(dsCompro.Tables(0).Rows(0)(5))),2) %></div> <!--Sunat-->
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:115px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(0))) %></div> <!--Nombre-->
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:145px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(1))) %></div> <!--Dirección-->
			<div ID="" STYLE="LEFT:100px; POSITION:absolute; TOP:175px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(2))) %></div> <!--RUC-->
			<%if trim(tipo)="ZNCV" then%>
			<div ID="" STYLE="LEFT:500px; POSITION:absolute; TOP:175px" class="clsCourier10">Ref. 
				Orig. :&nbsp;<%= Trim(cstr(dsCompro.Tables(0).Rows(0)(18))) %></div> <!--TipoDoc-->
			<%end if%>
			<div ID="" STYLE="LEFT:110px; POSITION:absolute; TOP:210px" class="clsCourier10">
			<%= 
				Trim(cstr(dsCompro.Tables(0).Rows(0)(4))) 
			%>
			</div> <!--Referencia-->
			<div ID="" STYLE="LEFT:320px; POSITION:absolute; TOP:210px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(3))) %></div> <!--Fecha Factura--> <!-- .substring(4,2) + "/" + Trim(cstr(dsCompro.Tables(0).Rows(0)(3))).substring(0,4) -->
			<div ID="" STYLE="LEFT:390px; POSITION:absolute; TOP:210px" class="clsCourier10"><%= Trim(cstr(dsCompro.Tables(0).Rows(0)(6))).PadLeft(10, "0") %><%= cstr(dsCompro.Tables(0).Rows(0)(7))%></div> <!--Usuario-->
			<%				END IF	%>
			<%
					end if
				end if
			end if
		end if
	end if
	
	
	'Set objRecordSetA = Nothing
if (len(tipo) > 0) then'1 	%>
			<!-- Detalle -->
			<% if(tipo="ZDAU") then'2   %>
			<div ID="" STYLE="LEFT:200px; POSITION:absolute; TOP:300px">
				<table border="0" bordercolor="#000000" width="400">
					<% ''Set objRecordSetB = objComponente.Get_RS02()
				'Set objRecordSetB = XmlToRecordset(StrXml,"RS02")
				'Set objRecordSetB = XmlToRecordset(StrXml,"RS02")
				if dsCompro.Tables(2).Rows.Count > 0 then '3
				'if not objRecordSetB is nothing then'3
					'if not objRecordSetB.eof then '4
						'objRecordSetB.MoveFirst
						desc = negativo(dsCompro.Tables(2).Rows(0)(10))
						%>
					<tr>
						<td class="clsCourier10B" width="200">VENTA DE RECARGA CLARO</td>
						<td class="clsCourier10" width="50">:</td>
						<td class="clsCourier10" width="150" align="right"><%=cant_dec((cdbl(replace(cstr(dsCompro.Tables(2).Rows(0)(9)),".",","))) + (cdbl(replace(cstr(dsCompro.Tables(2).Rows(0)(11)),".",","))))%></td>
					</tr>
					<tr>
						<td class="clsCourier10B">RECARGA EFECTIVA</td>
						<td class="clsCourier10">:</td>
						<td class="clsCourier10" align="right"><%=recarga%></td>
					</tr>
					<%		'end if'4
				end if '3
				'Set objRecordSetB = Nothing
				%>
				</table>
			</div>
			<% else
	if(tipo = "NPED") then'3a %>
			<div ID="" STYLE="LEFT:30px; POSITION:absolute; TOP:190px">
				<table border="0" bordercolor="#000000" width="650">
					<%
			dim Cant1
			dim Cant2
			''Set objRecordSetB = objComponente.Get_RS02()
			'Set objRecordSetB = XmlToRecordset(StrXml,"RS02")
			'Set objRecordSetB = XmlToRecordset(StrXml,"RS02")
			if dsCompro.Tables(2).Rows.Count > 0 then '4a
			'if not objRecordSetB is nothing then'4a
			'	if not objRecordSetB.eof then  '5a
					'objRecordSetB.MoveFirst
					dim obFilaD as System.Data.datarow
					for each obFilaD in dsCompro.Tables(2).Rows
					'Do While (Not objRecordSetB.BOF AND Not objRecordSetB.EOF) %>
					<tr class="clsCourier10" height="18">
						<td><%= obFilaD(4) %></td>
						<td><%= obFilaD(12)%></td>
						<td><%= obFilaD(1)%></td>
						<%  Cant1 = obFilaD(9)
							Cant2 = obFilaD(11)
							'total =  (cdbl(replace(objRecordSetB.Fields(9).Value,".",","))) + (cdbl(replace(objRecordSetB.Fields(11).Value,".",",")))
							total = (cdbl(Cant1)) + cdbl((Cant2)) %>
						<td align="right"><%=cant_dec(total)%></td>
					</tr>
					<%
						'objRecordSetB.MoveNext()
						'Loop
					next
				'end if'5a
			end if '4a
			'Set objRecordSetB = Nothing %>
				</table>
			</div>
			<%if cadRV = "1" then
			posicion = "220"
		%>
			<div ID="" STYLE="position:absolute; left:50; top:<%=posicion%>;">
				<table border="0" bordercolor="#000000" width="250">
					<tr class="clsCourier10">
						<td align="right">&nbsp;</td>
						<td align="right">Nro. Recarga</td>
						<td align="right"><%=numrecarga%></td>
					</tr>
					<tr class="clsCourier10">
						<td align="right">&nbsp;</td>
						<td align="right">Recarga Efectiva</td>
						<td align="right"><%=recargaefectiva%></td>
					</tr>
					<tr class="clsCourier10">
						<td align="right">&nbsp;</td>
						<td align="right">Valor de Venta</td>
						<td align="right"><%=valorventa%></td>
					</tr>
					<tr class="clsCourier10">
						<td align="right">&nbsp;</td>
						<td align="right">Descuento</td>
						<td align="right"><%=descuento%></td>
					</tr>
					<tr class="clsCourier10">
						<td align="right">&nbsp;</td>
						<td align="right">SubTotal</td>
						<td align="right"><%=valorventa - descuento%></td>
					</tr>
					<tr class="clsCourier10">
						<td align="right">&nbsp;</td>
						<td align="right">I.G.V.(<%=cdbl(Const_IGV) & "%"%>)</td>
						<td align="right"><%=igv%></td>
					</tr>
					<tr class="clsCourier10">
						<td align="right">&nbsp;</td>
						<td align="right">Ajuste Redondeo</td>
						<td align="right">0.00</td>
					</tr>
					<tr class="clsCourier10">
						<td align="right">&nbsp;</td>
						<td align="right">Total</td>
						<td align="right"><%=totalpago%></td>
					</tr>
				</table>
				<table border="0" bordercolor="#000000" width="600">
					<tr class="clsCourier10">
						<td align="right">&nbsp;</td>
					</tr>
					<tr class="clsCourier10">
						<td align="left"><%=cteMSG_GLOSA_RECARGAVIRTUAL_1%></td>
					</tr>
					<tr class="clsCourier10">
						<td align="left"><%=cteMSG_GLOSA_RECARGAVIRTUAL_2%></td>
					</tr>
				</table>
			</div>
			<%END IF
		posicion = "630"
		%>
		
			<%   else	%>
			<%	IF	trim(tipo) = "ZBOL" THEN	'4c

		strCabeceraComprobante = strCabeceraComprobante & "CODIGO;CANT;UN;DESCRIPCION;PRECIO UNITARIO;"
		strCabeceraComprobante = strCabeceraComprobante & "DESCUENTO;PRECIO NETO;TOTAL"
	%>
			<!--	DATOS PARA LA IMPRESION DE LA BOLETA	-->
			<div ID="" STYLE="LEFT:60px; POSITION:absolute; TOP:215px">
				<table border="0" bordercolor="#000000" width="650">
					<tr class="clsCourier10">
						<td align="center">CODIGO</td>
						<td align="center">CANT</td>
						<td align="center">UN</td>
						<td align="center">DESCRIPCION</td>
						<td align="center">PRECIO UNITARIO</td>
						<td align="center">DESCUENTO</td>
						<td align="center">PRECIO NETO</td>
						<td align="center">TOTAL</td>
					</tr>
					<%	ELSE%>
					<!--	DATOS PARA LA IMPRESION DE LA FACTURA	-->
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:250px">
						<table border="0" bordercolor="#000000" width="650">
							<tr class="clsCourier10">
								<td align="center">CODIGO</td>
								<td align="center">CANT</td>
								<td align="center">UN</td>
								<td align="center">DESCRIPCION</td>
								<td align="center">PRECIO UNITARIO</td>
								<td align="center">DESCUENTO</td>
								<td align="center">PRECIO NETO</td>
								<td align="center">TOTAL</td>
							</tr>
							<%	END IF'4c%>
							<%	ttotal = 0
			''Set objRecordSetB = objComponente.Get_RS02()
			'Set objRecordSetB = XmlToRecordset(StrXml,"RS02")

			'Set objRecordSetB = XmlToRecordset(StrXml,"RS02")
			strDetalleComprobante = ""
			if dsCompro.Tables(2).Rows.Count > 0 Or fromOffline then '5c
			'if not objRecordSetB is nothing then '5c
			'	if not objRecordSetB.eof then  '6c
					'objRecordSetB.MoveFirst
					dim obFilaD as System.Data.datarow
					Dim strTelefono as string 
					
					'for each obFilaD in dsCompro.Tables(2).Rows	*****linea actual
					for each obFilaD in dsCompro.Tables(1).Rows		'*****liena prueba
					'Do While (Not objRecordSetB.BOF AND Not objRecordSetB.EOF)

						' INICIO FMES : VER SI ES UNA VENTA PREPAGO
						Dim dtDatos As New System.Data.DataTable
						strTelefono = ""

						
						Dim objBus As New COM_SIC_Activaciones.ClsActivacionPel
						dtDatos = objBus.ListarDatosCabeceraVenta(Cstr(docSap))
						
						If dtDatos.Rows.Count > 0 Then		'***entra cuando es una venta prepago
							Dim strCodigoResp As String = ""
							Dim lista As ArrayList = objBus.Lis_Lista_Detalle_Venta_Prepago(Cstr(docSap), strCodigoResp)
							
							For Each item As COM_SIC_Activaciones.DetalleVentaPrepago In lista
							
								if cstr(obFilaD(12)).IndexOf(item.SERIE_CHIP) > -1  then
									strTelefono = Microsoft.VisualBasic.Strings.Right("000000000000000000" & cstr(item.LINEA), 15) 
								end if
								
								if cstr(obFilaD(12)).IndexOf(item.SERIE_EQUI) > -1  then
									strTelefono = Microsoft.VisualBasic.Strings.Right("000000000000000000" & cstr(item.LINEA), 15) 
								end if
							next
						End If
						' FIN FMES : VER SI ES UNA VENTA PREPAGO

				'***LOK	************************
						strDetalleComprobante = strDetalleComprobante & cstr(obFilaD(1)) & ";"
						strDetalleComprobante = strDetalleComprobante & no_dec(cstr(obFilaD(2))) & ";"
						strDetalleComprobante = strDetalleComprobante & cstr(obFilaD(3)) & ";"
						strDetalleComprobante = strDetalleComprobante & cstr(obFilaD(4)) & ";"
						strDetalleComprobante = strDetalleComprobante & cstr(obFilaD(5)) & ";"
						strDetalleComprobante = strDetalleComprobante & cstr(obFilaD(6)) & ";"
				'*******************************
						
						If Convert.IsDBNull(obFilaD(8)) Then
							strDetalleComprobante = strDetalleComprobante & "" & ";"
						else
							strDetalleComprobante = strDetalleComprobante & cstr(obFilaD(8)) & ";"
						end if 
						
						strDetalleComprobante = strDetalleComprobante & cstr(obFilaD(11)) & ";"
						strDetalleComprobante = strDetalleComprobante & cstr(obFilaD(12))  & "|"
					%>
							<tr class="clsCourier10">
								<td><%=  cstr(obFilaD(1))  %></td>
								<td align="right"><%= no_dec(cstr(obFilaD(2)) )%></td>
								<td><%= cstr(obFilaD(3)) %></td>
								<td><%= cstr(obFilaD(4)) %></td>
								<td align="right"><%= cstr(obFilaD(5)) %></td>
								<td align="right"><%= cdec(obFilaD(6)).tostring("N2") %></td>
								<%
					   if(tipo="ZFAC" or tipo="ZNCV" or tipo="ZNDV") then
							'total =  ((cdbl(objRecordSetB.Fields(11).Value)) - (cdbl(objRecordSetB.Fields(10).Value)))
							total = cdbl(obFilaD(11))
					   else
					   		'total =  ((cdbl(objRecordSetB.Fields(9).Value)) - (cdbl(objRecordSetB.Fields(10).Value)) + (cdbl(objRecordSetB.Fields(11).Value)))
							total = cdbl(obFilaD(11))
					   end if
					   ttotal = ttotal + total
					%>
								<td align="right"><%= cstr(obFilaD(8)) %></td>
								<td align="right"><%= cstr(obFilaD(11)) %></td>
								<!--<td align="right"><%=cant_dec(total)%></td>-->
							</tr>
							<%      if (Len(Trim( cstr(obFilaD(12)) ))) > 0 then 
										if strTelefono <> "" and cstr(obFilaD(12)).IndexOf("Teléfono") <= -1  then %>
										<tr class="clsCourier10">
											<td colspan="5">&nbsp;&nbsp;<%=  cstr(obFilaD(12)) + " Teléfono: " + strTelefono %></td>
										</tr>
									<% else%>
							<tr class="clsCourier10">
								<td colspan="5">&nbsp;&nbsp;<%=  cstr(obFilaD(12)) %></td>
							</tr>
									<% end if%>
							
							<%		end if
						'objRecordSetB.MoveNext()
					next
					if(fromOffline) then
						strDetalleComprobante = strDetalleComprobante & cstr(dsComproOffline.Tables(1).Rows(0)("MATNR")) & ";"
							strDetalleComprobante = strDetalleComprobante & no_dec(cstr(dsComproOffline.Tables(1).Rows(0)("KWMENG"))) & ";"
							strDetalleComprobante = strDetalleComprobante & "UN;"
							strDetalleComprobante = strDetalleComprobante & cstr(dsComproOffline.Tables(1).Rows(0)("DESCRIPCION_PRODUCTO")) & ";"
							strDetalleComprobante = strDetalleComprobante & cstr(System.Configuration.ConfigurationSettings.AppSettings("valVentaRecVirtual")) & ";"
							strDetalleComprobante = strDetalleComprobante & cstr(dsComproOffline.Tables(1).Rows(0)("DESCUENTO")) & ";"
							strDetalleComprobante = strDetalleComprobante & cstr(dsComproOffline.Tables(1).Rows(0)("PLAN_TARIFARIO")) & ";"
							strDetalleComprobante = strDetalleComprobante & cstr(dsComproOffline.Tables(1).Rows(0)("TOTAL_PAGO")) & ";"
							strDetalleComprobante = strDetalleComprobante & cstr("Teléfono: 000000" & dsComproOffline.Tables(1).Rows(0)("ZZNRO_TELEF"))  & "|"
						%>
							<tr class="clsCourier10">
								<td><%=  cstr(dsComproOffline.Tables(1).Rows(0)("MATNR"))  %></td>
								<td align="right"><%= no_dec(cstr(dsComproOffline.Tables(1).Rows(0)("KWMENG")) )%></td>
								<td><%= cstr("UN") %></td>
								<td><%= cstr(dsComproOffline.Tables(1).Rows(0)("DESCRIPCION_PRODUCTO")) %></td>
								<td align="right"><%= cstr(System.Configuration.ConfigurationSettings.AppSettings("valVentaRecVirtual")) %></td>
								<td align="right"><%= "0.00" %></td>
								<%total = cdbl(dsComproOffline.Tables(1).Rows(0)("TOTAL_PAGO"))
									ttotal = ttotal + total%>
								<td align="right"><%= cstr("1.0") %></td>
								<td align="right"><%= cstr(dsComproOffline.Tables(1).Rows(0)("TOTAL_PAGO")) %></td>
								<!--<td align="right"><%=cant_dec(total)%></td>-->
							</tr>
							<%if (Len(Trim( cstr(dsComproOffline.Tables(1).Rows(0)("ZZNRO_TELEF")) ))) > 0 then %>
							<tr class="clsCourier10">
								<td colspan="5">&nbsp;&nbsp;<%=  cstr("Teléfono: 000000" & dsComproOffline.Tables(1).Rows(0)("ZZNRO_TELEF")) %></td>
							</tr>
							<%		end if						
					end if
					'Loop
				'end if'6c
			end if '5c
			%>
						</table>
						<table>
							<tr>
								<td colspan="8">&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td colspan="8"><%= "" %></td> <!-- OBTENER GLOSA CAMPAÑA -->
							</tr>
						</table>
					</div>
					<!-- Cuotas -->
					<%

		strAdicionalComprobante = ""
		intNumeroCuotas = 0

		if cadRV = "1" then%>
					<div ID="" STYLE="position:absolute; left:50; top:<%=posicion%>;">
						<table border="0" bordercolor="#000000" width="250">
							<tr class="clsCourier10">
								<td align="right">&nbsp;</td>
								<td align="right">Nro. Recarga</td>
								<td align="right"><%=numrecarga%></td>
							</tr>
							<tr class="clsCourier10">
								<td align="right">&nbsp;</td>
								<td align="right">Recarga Efectiva</td>
								<td align="right"><%= String.Format("{0:n2}", recargaefectiva) %></td>
							</tr>
							<tr class="clsCourier10">
								<td align="right">&nbsp;</td>
								<td align="right">Valor de Venta</td>
								<td align="right"><%=valorventa%></td>
							</tr>
							<tr class="clsCourier10">
								<td align="right">&nbsp;</td>
								<td align="right">Descuento</td>
								<td align="right"><%=descuento%></td>
							</tr>
							<tr class="clsCourier10">
								<td align="right">&nbsp;</td>
								<td align="right">SubTotal</td>
								<td align="right"><%=valorventa - descuento%></td>
							</tr>
							<tr class="clsCourier10">
								<td align="right">&nbsp;</td>
								<td align="right">I.G.V.(<%=cdbl(Const_IGV) & "%"%>)</td>
								<td align="right"><%=igv%></td>
							</tr>
							<tr class="clsCourier10">
								<td align="right">&nbsp;</td>
								<td align="right">Ajuste Redondeo</td>
								<td align="right">0.00</td>
							</tr>
							<tr class="clsCourier10">
								<td align="right">&nbsp;</td>
								<td align="right">Total</td>
								<td align="right"><%= String.Format("{0:n2}", totalpago) %></td>
							</tr>
						</table>
						<table border="0" bordercolor="#000000" width="600">
							<tr class="clsCourier10">
								<td align="right">&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td align="left"><%=cteMSG_GLOSA_RECARGAVIRTUAL_1%></td>
							</tr>
							<tr class="clsCourier10">
								<td align="left"><%=cteMSG_GLOSA_RECARGAVIRTUAL_2%></td>
							</tr>
						</table>
					</div>
					<%else
			posicion=630
		%>
					
					
					<%	posicion=200
			IF	trim(tipo) = "ZBOL" THEN	%>
					<!--	DATOS PARA LA IMPRESION DE LA BOLETA	-->
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:465px">
						<table border="0" bordercolor="#000000" width="250">
							<%  ''Set objRecordSetC = objComponente.Get_RS03()
				    'Set objRecordSetC = XmlToRecordset(StrXml,"RS03")
				    'Set objRecordSetC = XmlToRecordset(StrXml,"RS03")
				    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
				    ' Consulta Datos Cuotas
				    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
				    '-----------------------------------------------------
				    ' 25/11/2011: No debe Imprimirse las Cuotas
				    '-----------------------------------------------------
				    if false then 'dsCompro.Tables(1).Rows.Count > 1 then '5c
					'if not objRecordSetC is nothing then
					'	if not objRecordSetC.eof then
					'		if objRecordSetC.RecordCount > 1 then
					'		objRecordSetC.MoveFirst
					%>
							<tr class="clsCourier10">
								<td><%=  cstr(dsCompro.Tables(1).Rows(0)(0))  %></td>
							</tr>
							<%' Forzamos que siempre contenga una línea
								strAdicionalComprobante =  cstr(dsCompro.Tables(1).Rows(0)(0)) & ";;|" ' objRecordSetC.Fields(0).Value & ";;|"

								Dim dsCuotasPorta as System.Data.DataSet
								dim obPagos as new COM_SIC_Cajas.clsCajas
								dsCuotasPorta = obPagos.FP_Get_Cuotas(docSap)
								obPagos = nothing

								if ((not dsCuotasPorta is nothing) AndAlso (not dsCuotasPorta.Tables(0) is nothing) AndAlso (dsCuotasPorta.Tables(0).Rows.Count > 0)) then
									dim drCuota as System.Data.Datarow
									intNumeroCuotas = 1
							%>
							<tr class="clsCourier10">
								<td>&nbsp;</td>
								<td>&nbsp;</td>
								<td align="right">&nbsp;</td>
							</tr>
							<%
									for each drCuota in dsCuotasPorta.tables(0).rows
										intNumeroCuotas = intNumeroCuotas + 1
							%>
							<tr class="clsCourier10">
								<td><%= cstr(drCuota(0)) %></td>
								<td><%= String.Format("{0:dd/MM/yyyy}",drCuota(1)) %></td>
								<td align="right"><%= format(cstr(drCuota(2))) %></td>
							</tr>
							<%
										strAdicionalComprobante = strAdicionalComprobante & cstr(drCuota(0)) & ";"
										strAdicionalComprobante = strAdicionalComprobante & String.Format("{0:dd/MM/yyyy}",drCuota(1)) & ";"
										strAdicionalComprobante = strAdicionalComprobante & cstr(drCuota(2)) & "|"
									next
									drCuota = nothing
									dsCuotasPorta = nothing
								else
										dim obFilaD as System.Data.datarow
										for each obFilaD in dsCompro.Tables(1).Rows
										'Do While (Not objRecordSetC.BOF AND Not objRecordSetC.EOF)
											intNumeroCuotas = intNumeroCuotas + 1
							%>
							<tr class="clsCourier10">
								<td><%= cstr(obFilaD(1))  %>
									<% if intNumeroCuotas = 1 then
														dim dsPedido as System.Data.Dataset = objComponentePagos.Get_ConsultaPedido("",Cstr(oficinaVenta),Cstr(docSap),"")
														if dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") then
															response.write(ConfigurationSettings.AppSettings("gConstPorcPrePago") & "%" )
														end if
														dsPedido = nothing
													end if
													%>
								</td>
								<%
													scFecha = cstr(obFilaD(2)) ' objRecordSetC.Fields(2).Value
													if (Trim(scFecha)="12:00:00 a.m.") then scFecha = ""
												%>
								<td><% = valida_dato(scFecha)%></td>
								<td align="right"><%= cstr(obFilaD(3)) %></td>
							</tr>
							<%
												strAdicionalComprobante = strAdicionalComprobante & cstr(obFilaD(1)) & ";"
												strAdicionalComprobante = strAdicionalComprobante & valida_dato(scFecha) & ";"
												strAdicionalComprobante = strAdicionalComprobante & cstr(obFilaD(3)) & "|"
											'objRecordSetC.MoveNext
										'Loop
										next
								end if
					else %>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<%		'end if
					end if
					'Set objRecordSetC = Nothing
				%>
						</table>
					</div>
					<%	ELSE%>
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:440px">
						<table border="0" bordercolor="#000000" width="250">
							<%  ''Set objRecordSetC = objComponente.Get_RS03()
				    'Set objRecordSetC = XmlToRecordset(StrXml,"RS03")
				    'Set objRecordSetC = XmlToRecordset(StrXml,"RS03")
				    '-----------------------------------------------------
				    ' 25/11/2011: No debe Imprimirse las Cuotas
				    '-----------------------------------------------------
				    if false then 'dsCompro.Tables(1).Rows.Count > 1 then '5c
					'if not objRecordSetC is nothing then
					'	if not objRecordSetC.eof then
					'		if objRecordSetC.RecordCount > 1 then
					'		objRecordSetC.MoveFirst
					%>
							<tr class="clsCourier10">
								<td><%= cstr(dsCompro.Tables(1).Rows(0)(0)) %></td>
							</tr>
							<%
							dim obFilaD as System.Data.datarow
							for each  obFilaD in dsCompro.Tables(1).Rows
							'Do While (Not objRecordSetC.BOF AND Not objRecordSetC.EOF)
									intNumeroCuotas = intNumeroCuotas + 1
								%>
							<tr class="clsCourier10">
								<td><%=  cstr(obFilaD(1)) %>
									<%if intNumeroCuotas = 1 then
								    dim dsPedido as System.Data.Dataset = objComponentePagos.Get_ConsultaPedido("",Cstr(oficinaVenta),Cstr(docSap),"")
								    if dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") then
								       response.write(ConfigurationSettings.AppSettings("gConstPorcPrePago") & "%" )
								    end if
								    dsPedido = nothing
								  end if
								%>
								</td>
								<%
										scFecha = cstr(obFilaD(2)) ' objRecordSetC.Fields(2).Value
										if (Trim(scFecha)="12:00:00 a.m.") then scFecha = ""
									%>
								<td><%=valida_dato(scFecha)%></td>
								<td align="right"><%=  cstr(obFilaD(3)) %></td>
							</tr>
							<%
								'objRecordSetC.MoveNext
								'Loop
							next
							'end if
					else     %>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td>&nbsp;&nbsp;</td>
							</tr>
							<%
					'end if
					end if
					'Set objRecordSetC = Nothing
				%>
						</table>
					</div>
					<%	END IF%>
					<%  end if

	end if
   end if %>
					<!--Pie -->
					<%
	if(tipo="ZDAU") then
%>
					<div ID="" STYLE="LEFT:240px; POSITION:absolute; TOP:340px">
						<table border="0" bordercolor="#000000" width="400">
							<%
		''Set objRecordSetD = objComponente.Get_RS04()
		'Set objRecordSetD = XmlToRecordset(StrXml,"RS04")

		'Set objRecordSetD = XmlToRecordset(StrXml,"RS04")
		if dsCompro.Tables(3).Rows.Count > 0 then
		'if not objRecordSetD is nothing then
		'	if not objRecordSetD.eof then
		'		objRecordSetD.MoveFirst
%>
							<tr>
								<td class="clsCourier10B" width="200">Valor de Venta</td>
								<td class="clsCourier10" width="50">:</td>
								<td class="clsCourier10" width="150" align="right"><%=format(cstr(dsCompro.Tables(3).Rows(0)(2)))%></td>
							</tr>
							<tr>
								<td class="clsCourier10B">Descuento</td>
								<td class="clsCourier10">:</td>
								<td class="clsCourier10" align="right"><%=desc%></td>
							</tr>
							<tr>
								<td class="clsCourier10B">I.G.V. (<%=trim( cstr(dsCompro.Tables(3).Rows(0)(4))  )%>%)</td>
								<td class="clsCourier10">:</td>
								<td class="clsCourier10" align="right"><%=format(cstr(dsCompro.Tables(3).Rows(0)(3)))%></td>
							</tr>
							<tr>
								<td class="clsCourier10B">Ajuste Redondeo</td>
								<td class="clsCourier10">:</td>
								<td class="clsCourier10" align="right"><%=negativo(cstr(dsCompro.Tables(3).Rows(0)(5)))%></td>
							</tr>
							<tr>
								<td class="clsCourier10B">Total</td>
								<td class="clsCourier10">:</td>
								<td class="clsCourier10" align="right"><%=format(cstr(dsCompro.Tables(2).Rows(0)(6)))%></td>
							</tr>
							<tr>
								<td colspan="3">&nbsp;</td>
							</tr>
							<tr class="clsCourier10">
								<td colspan="3">SON :
									<%= cstr(dsCompro.Tables(3).Rows(0)(1)) %>
								</td>
							</tr>
							<tr>
								<td colspan="3">&nbsp;</td>
							</tr>
							<tr class="clsCourier10B">
								<td colspan="3">Vigencia de la recarga : 180 días</td>
							</tr>
							<tr class="clsCourier10B">
								<td colspan="3">Para Consultas o reclamos llamar al 151</td>
							</tr>
							<tr>
								<td colspan="3">&nbsp;</td>
							</tr>
						</table>
					</div>
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:530px">
						<table border="0" bordercolor="#000000" width="650">
							<tr>
								<td colspan="3">_______________________________________________________________________________________</td>
							</tr>
							<tr class="clsCourier10">
								<td align="center" colspan="3">Documento autorizado para fines tributarios e 
									impuesto a la renta e IGV.</td>
							</tr>
							<tr class="clsCourier10">
								<td align="center" colspan="3">(Base legal: art.4 - Reglamento de comprobantes de 
									pago)</td>
							</tr>
							<tr>
								<td colspan="3">_______________________________________________________________________________________</td>
							</tr>
						</table>
						<%
			'end if
		end if

%>
					</div>
					<div ID="" STYLE="LEFT:10px; POSITION:absolute; TOP:600px">
						<table border="0" bordercolor="#000000" width="650">
							<tr class="clsArial18">
								<td align="right"><%=  cstr(dsCompro.Tables(3).Rows(0)(7)) %></td>
							</tr>
						</table>
					</div>
					<%
			'Set objRecordSetD = Nothing
	else
		if(tipo="NPED") then '--------C-----------
			''Set objRecordSetD = objComponente.Get_RS04()
			'Set objRecordSetD = XmlToRecordset(StrXml,"RS04")
			if dsCompro.Tables(3).Rows.Count > 0 then

			'if not objRecordSetD is nothing then
			'	if not objRecordSetD.eof then
			'		objRecordSetD.MoveFirst
			 %>
					<div ID="" class="clsArial11" STYLE="LEFT:30px; POSITION:absolute; TOP:365px"><%=strMarca%></div> <!--CAP: Marca. Remover despues -->
					<div ID="" class="clsArial11" STYLE="LEFT:655px; POSITION:absolute; TOP:365px"><%=format( cstr(dsCompro.Tables(2).Rows(0)(6)) )%></div>
					<div ID="" STYLE="LEFT:10px; POSITION:absolute; TOP:415px">
						<table border="0" bordercolor="#000000" width="650">
							<tr class="clsArial18">
								<td align="right"><%= cstr(dsCompro.Tables(3).Rows(0)(7)) %></td>
							</tr>
						</table>
					</div>
					<%
			'	end if
			end if
			'Set objRecordSetD = Nothing
		else
			''Set objRecordSetD = objComponente.Get_RS04()
			'Set objRecordSetD = XmlToRecordset(StrXml,"RS04")
			'Set objRecordSetD = XmlToRecordset(StrXml,"RS04")
			if dsCompro.Tables(3).Rows.Count > 0 then '--------B-----------

			'if not objRecordSetD is nothing then'--------B-----------
			'	if not objRecordSetD.eof then'--------A-----------
			'	objRecordSetD.MoveFirst
%>
					<% IF	trim(tipo) = "ZBOL" THEN



			strPieComprobante = cstr(dsCompro.Tables(3).Rows(0)(0))  & ";"
			strPieComprobante = strPieComprobante & "Valor Venta;" & cant_dec(ttotal) & ";"
			strPieComprobante = strPieComprobante & cstr(dsCompro.Tables(3).Rows(0)(1)) & ";"
			'strPieComprobante = strPieComprobante & "Ajuste Redondeo;" & negativo(cstr(dsCompro.Tables(3).Rows(0)(5))) & ";"
			strPieComprobante = strPieComprobante & "Ajuste Redondeo;" & negativo(cstr(dsCompro.Tables(2).Rows(0)(5))) & ";"
			'strPieComprobante = strPieComprobante & cstr(dsCompro.Tables(3).Rows(0)(6))	'***total
			strPieComprobante = strPieComprobante & cstr(dsCompro.Tables(2).Rows(0)(6))

		%>
					<!--	DATOS PARA LA IMPRESION DE LA BOLETA	-->
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:760px" class="clsCourier10"><%=strMarca%></div> <!--CAP: Linea temporal.Borrar despues -->
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:790px" class="clsCourier10">
						<table border="0" bordercolor="#000000" width="650">
							<tr class="clsCourier10">
								<td align="left"><%=  cstr("DESCUENTO TOTAL SIN IGV: 0 SOLES") %></td> <!-- dsCompro.Tables(3).Rows(0)(0)) -->
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then %>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% else %>
								<td align="right">Valor Venta</td>
								<td align="right"><%=cant_dec(ttotal)%></td>
								<% end if %>
							</tr>
							<tr class="clsCourier10">
								<%
									Dim conversor as New SisCajas.Numalet()
									dim monto as double = CDbl(ttotal)
									dim montoString as string = conversor.ConvertirLetras(monto, 0, "", "", False, True, False, False, False)
								%>
								<td align="left"><%= cstr(UCase(montoString) & " Y 00/100 SOLES") %></td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="left">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then %>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% else
								    'CAP: Colocar espacios para alinear el signo, segun la cantidad de caracteres
								     strNbsp = ""
								     for i = 0  to (len(trim(cstr(dsCompro.Tables(2).Rows(0)(6)) )) - len(trim(negativo(cstr(dsCompro.Tables(2).Rows(0)(5)) ))))
								        strNbsp = strNbsp & "&nbsp;"
								     next

								%>
								<td align="right">Ajuste Redondeo</td>
								<!--<td align="right">S/&nbsp;&nbsp;&nbsp;<%=strNbsp & negativo(cstr(dsCompro.Tables(3).Rows(0)(5)) )%></td>-->
								<td align="right">S/&nbsp;&nbsp;&nbsp;<%=strNbsp & negativo(cstr(dsCompro.Tables(2).Rows(0)(5)) )%></td>
								<% end if %>
							</tr>
						</table>
					</div>
					<% ELSE		%>
					<!--	DATOS PARA LA IMPRESION DE LA FACTURA	-->
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:710px" class="clsCourier10"><%=strMarca%></div> <!--CAP: Linea temporal.Borrar despues -->
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:740px" class="clsCourier10">
						<table border="0" bordercolor="#000000" width="650">
							<tr class="clsCourier10">
								<td align="left"><%=  cstr(dsCompro.Tables(3).Rows(0)(0)) %></td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then %>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% else %>
								<td align="right">Valor Venta</td>
								<td align="right"><%=cant_dec(ttotal)%></td>
								<% end if %>
							</tr>
							<tr class="clsCourier10">
								<td align="left"><%= cstr(dsCompro.Tables(3).Rows(0)(1)) %></td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="left">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then %>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% else %>
								<td align="right">Ajuste Redondeo</td>
								<td align="right"><%=negativo(cstr(dsCompro.Tables(3).Rows(0)(5)) )%></td>
								<% end if %>
							</tr>
						</table>
					</div>
					<%	END IF	%>
					<!--	DATOS PARA LA IMPRESION DE LA BOLETA	-->
					<% IF	trim(tipo) = "ZBOL" THEN	%>
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:825px">
						<table border="0" bordercolor="#000000" width="650">
							<tr class="clsCourier10">
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then %>
								<td align="right"><%=format(cstr(dsCompro.Tables(3).Rows(0)(2)))%></td>
								<% else %>
								<%'CAP: Signo de S/%>
								<td align="right">S/&nbsp;<%= cstr(dsCompro.Tables(2).Rows(0)(6)) %></td>
								<% end if %>
							</tr>
						</table>
					</div>
					<%
				strPieComprobante = strPieComprobante & cstr(dsCompro.Tables(2).Rows(0)(6)) & ";"

				ELSE	%>
					<!--	DATOS PARA LA IMPRESION DE LA FACTURA	-->
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:805px">
						<table border="0" bordercolor="#000000" width="650">
							<tr class="clsCourier10">
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then %>
								<td align="right"><%=format( cstr(dsCompro.Tables(3).Rows(0)(2)) )%></td>
								<% else %>
								<td align="right"><%=cstr(dsCompro.Tables(2).Rows(0)(6)) %></td>
								<% end if %>
							</tr>
						</table>
					</div>
					<%	END IF	%>
					<%	IF	trim(tipo) = "ZFAC" THEN %>
					<!--	DATOS PARA LA IMPRESION DE LA FACTURA	-->
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:860px">
						<table border="0" bordercolor="#000000" width="650">
							<tr class="clsCourier10">
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<% if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then %>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;<%=trim(cstr(dsCompro.Tables(3).Rows(0)(4)))+"%"%></td>
								<%
								   'CAP: Colocar espacios para alinear el signo, segun la cantidad de caracteres
								   strNbsp = ""
								   for i = 0  to (len(trim(cstr(dsCompro.Tables(2).Rows(0)(6)))) - len(trim(format(cstr(dsCompro.Tables(2).Rows(0)(3)))))) + 1
								      strNbsp = strNbsp & "&nbsp;"
								   next
								%>
								<td align="right">S/&nbsp;<%=strNbsp & format(cstr(dsCompro.Tables(3).Rows(0)(3)))%></td>
								<% else %>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% end if %>
							</tr>
						</table>
					</div>
					<%	ELSE	%>
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:835px">
						<table border="0" bordercolor="#000000" width="650">
							<tr class="clsCourier10">
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<% if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then %>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;<%=trim(cstr(dsCompro.Tables(3).Rows(0)(4)) )+"%"%></td>
								<% 'CAP: Signo de soles %>
								<td align="right">S/&nbsp;<%=format(cstr(dsCompro.Tables(3).Rows(0)(3)))%></td>
								<% else %>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% end if %>
							</tr>
						</table>
					</div>
					<%	END IF	%>
					<%	IF	trim(tipo) = "ZFAC" THEN %>
					<!--	DATOS PARA LA IMPRESION DE LA FACTURA	-->
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:885px">
						<table border="0" bordercolor="#000000" width="650">
							<tr class="clsCourier10">
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<% if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then %>
								<%
							     'CAP: Se colocan espacios adicionales para el signo de S/
							     strNbsp = ""
						         for i = 0  to (len(trim(cstr(dsCompro.Tables(2).Rows(0)(6)))) - len(trim(negativo(cstr(dsCompro.Tables(2).Rows(0)(5)))))) + 1
								    strNbsp = strNbsp & "&nbsp;"
								 next
							%>
								<td align="right">Ajuste Redondeo</td>
								<td align="right">S/&nbsp;&nbsp;&nbsp;<%=strNbsp & negativo(cstr(dsCompro.Tables(3).Rows(0)(5)))%></td>
								<% else %>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% end if %>
							</tr>
						</table>
					</div>
					<%	ELSE%>
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:860px">
						<table border="0" bordercolor="#000000" width="650">
							<tr class="clsCourier10">
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<% if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then %>
								<td align="right">Ajuste Redondeo</td>
								<%'CAP: Signo de S/ %>
								<td align="right">S/&nbsp;<%=negativo(cstr(dsCompro.Tables(3).Rows(0)(5)))%></td>
								<% else %>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% end if %>
							</tr>
						</table>
					</div>
					<%	END IF%>
					<%	IF	trim(tipo) = "ZFAC" THEN %>
					<!--	DATOS PARA LA IMPRESION DE LA FACTURA	-->
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:915px">
						<table border="0" bordercolor="#000000" width="650">
							<tr class="clsCourier10">
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% 'CAP : Signo de S/ %>
								<% if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then %>
								<td align="right">S/&nbsp;<%=format(cstr(dsCompro.Tables(2).Rows(0)(6)))%></td>
								<% else %>
								<td align="right">&nbsp;</td>
								<% end if %>
							</tr>
						</table>
					</div>
					<%	ELSE	%>
					<div ID="" STYLE="LEFT:50px; POSITION:absolute; TOP:890px">
						<table border="0" bordercolor="#000000" width="650">
							<tr class="clsCourier10">
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<td align="right">&nbsp;</td>
								<% if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then %>
								<% 'CAP: Signo de S/ %>
								<td align="right">S/&nbsp;<%=format( cstr(dsCompro.Tables(2).Rows(0)(6)) )%></td>
								<% else %>
								<td align="right">&nbsp;</td>
								<% end if %>
							</tr>
						</table>
					</div>
					<%	END IF

			'*** PMO - CUOTAS EN VENTA RAPIDA - INICIO ***
			'*** FECHA: 22/11/2005 - AUTOR: GGT

				'Response.Write intNumeroCuotas
				'Response.Write tipo
				'Response.End

				'-----------------------------------------------------
				' 25/11/2011: No debe Imprimirse las Cuotas
				'-----------------------------------------------------
				'If intNumeroCuotas > 1 then
				if dsCompro.Tables(1).Rows.Count > 1 then
					intNumeroCuotas = intNumeroCuotas - 1
					'strNuevaDescrDocumento = Replace(cteMSG_GLOSA_VENTA_CUOTAS, "#", cstr(intNumeroCuotas))
					strNuevaDescrDocumento = Replace(cteMSG_GLOSA_VENTA_CUOTAS, "#", "")
				else
					'strNuevaDescrDocumento = cstr(dsCompro.Tables(3).Rows(0)(7))	'*** Linea anterior
					'-------------------------------------------------------------------------------------------
					'**CONTROLAMOS EL VALOR EN NULL**' 
					If Convert.IsDBNull(dsCompro.Tables(2).Rows(0)(7)) Then
						strNuevaDescrDocumento = ""	 
					else
						strNuevaDescrDocumento = cstr(dsCompro.Tables(2).Rows(0)(7))	'*** cambio 07-01-2015
					end if 
					'-------------------------------------------------------------------------------------------
				end if
			'*** PMO - CUOTAS EN VENTA RAPIDA - FIN ***

				'LOK
				strPieComprobante = strPieComprobante & strNuevaDescrDocumento & ";"
				strPieComprobante = strPieComprobante & now.Date.ToString("d") & ";" & now.ToString("t")

		%>
					<div ID="" STYLE="LEFT:30px; POSITION:absolute; TOP:840px">
						<table border="0" bordercolor="#000000" width="650">
							<!--<tr class="clsArial18">-->
							<tr class="clsCourier10">
								<td align="left"><%=strNuevaDescrDocumento%></td>
							</tr>
						</table>
					</div>
					<div ID="" STYLE="LEFT:30px; POSITION:absolute; TOP:925px">
						<table border="0" bordercolor="#000000" width="650">
							<tr class="clsCourier10">
									<td width="100"><%= "" & now.Date.ToString("d") & "" %></td>
									<td><%= "" & now.ToString("t") & "" %></td>
							</tr>
						</table>
					</div>
					<%
				'end if'--------A-----------
			end if'--------B-----------
			'Set objRecordSetD = Nothing
		end if'--------C-----------
	end if'--------D-----------
else
	'Response.Write "No existe tipo de documento de impresión asociado a este trámite." + docSap & "Tipo Doc:" & tipo
end if
%>
					<%
	'if (Session("sTipDocVenta")="ZTGR") or (Session("sTipDocVenta")="ZDTR") or (Session("sTipDocVenta")="ZPFR") then
			''Set objRecordSetD = objComponente.Get_RS04()
	'		Set objRecordSetD = XmlToRecordset(StrXml,"RS")
			'Set objRecordSetD = XmlToRecordset(StrXml,"RS04")
	'		if not objRecordSetD is nothing then
	'			if not objRecordSetD.eof then
	'				objRecordSetD.MoveFirst
	'				end if
	'			end if
	'			Set objRecordSetD = Nothing
	'end if
%>
		</form>
	</body>
</HTML>
