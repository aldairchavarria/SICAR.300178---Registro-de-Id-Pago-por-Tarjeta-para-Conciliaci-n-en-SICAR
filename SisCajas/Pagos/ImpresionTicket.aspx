<%@ Page Language="vb" Codepage="1252"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ImpresionTicket</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<STYLE> .clsTexto { FONT-SIZE: 9pt; FONT-FAMILY: Arial Narrow; }
			.clsNumero { FONT-SIZE: 10pt; FONT-FAMILY: Arial Narrow; }
			.clsVigencia { FONT-SIZE: 9pt; FONT-FAMILY: Tahoma; }
	.tabla_borde { BORDER-RIGHT: #336699 1px solid; BORDER-TOP: #336699 1px solid; FONT-SIZE: 12px; BORDER-LEFT: #336699 1px solid; COLOR: #ff0000; BORDER-BOTTOM: #336699 1px solid; FONT-FAMILY: Arial; TEXT-DECORATION: none }
	.Boton { border-right: #95b7f3 1px solid; border-top: #95b7f3 1px solid; font-weight: bold; font-size: 10px; border-left: #95b7f3 1px solid; cursor: hand; color: #003399; border-bottom: #95b7f3 1px solid; font-family: Verdana; background-color: white; text-align: center; TEXT-DECORATION: none; BACKGROUND-REPEAT: repeat-x; background-color: #e9f2fe; /*BACKGROUND-IMAGE: url(../images/toolgrad.gif); */ border-color :#95b7f3 }
	.BotonResaltado { border-right: #95b7f3 1px solid; border-top: #95b7f3 1px solid; font-weight: bold; font-size: 10px; border-left: #95b7f3 1px solid; cursor: hand; color: #003399; border-bottom: #95b7f3 1px solid; font-family: Verdana; background-color: white; text-align: center; TEXT-DECORATION: none; BACKGROUND-REPEAT: repeat-x; border-color :#95b7f3 }
		</STYLE>
		<script language="javascript">
		window.onload = window_onload;
		function window_onload() {	
				window.parent.Imprimir();
		}

		function doprint() {
			printbtn.style.visibility = "HIDDEN";
			window.print();
			printbtn.style.visibility = "VISIBLE";
		}
		
		function Imp_ticket()
		{
			divBotones.style.visibility = "HIDDEN";
			window.print();
			divBotones.style.visibility = "VISIBLE";
		}		
		</script>
		<script runat="server">
    
    dim DesTotal as decimal
				dim servRec as string=""
    
    dim FlagCuotas
    dim NumeroCuotas
    dim CodVendedor as string
    dim NomVendedor as string
    dim CodCajero as string
    dim NomCajero as string
    dim CorrelativoSunat as string
	dim strCodUsuario as string
    dim strNomUsuario as string
    dim Reimpresion as string
    dim FlagPrepago 'as boolean
				'Dim isOffline As Boolean = False
			    
	
			    'Para el funcionamiento del disclaimer'
			    '**************************************************************'
	'JAZ - 02/10/2014
	dim isDiscleimer as Boolean = False
	Dim strLabelDiscleimer As String = ""
	dim strTamanoLabelDiscleimer As String = ""
    'Dim isOffline As Boolean = False
			    '**************************************************************'
    
    
    Public Function IsOffLine() as Boolean
		return Request.QueryString("isOffline") = "1"
    End Function
    
	Public dtIGV As System.Data.DataTable
    '---------------------------------------------------------------------------------------------------------
    'FUNCIÒN PARA IMPRIMIR LA CABECERA DEL DOCUMENTO:
public Function FncImprCabecera (byval dsCompro as System.Data.DataSet) as string
Dim cad as string
'dim CorrelativoSunat as string

	DIM gStrRazonSocial AS string = System.Configuration.ConfigurationSettings.AppSettings("gStrRazonSocial")
    DIM gStrMarca AS string = System.Configuration.ConfigurationSettings.AppSettings("gStrMarca")
    DIM gStrRUC AS string = System.Configuration.ConfigurationSettings.AppSettings("gStrRUC")
   

	if dsCompro.tables(0).rows.count>0 then 
						
						'DATOS GENERALES:
		cad="<TR>"
		cad=cad & "<TD align='center'class=clsTexto>"
		cad=cad & gStrRazonSocial & "<BR>"
		cad=cad & gStrMarca & "<BR>"
						
		if(IsOffLine) then
							'*** consulta en ->
			Dim objOfflin As New COM_SIC_OffLine.clsOffline
			Dim direccionPDV = objOfflin.ObtenerDireccionPDV(Session("ALMACEN"))

			strCodUsuario = Trim(cstr(dsCompro.tables(0).rows(0)("VENDEDOR")) )
			
			cad=cad & Trim(direccionPDV) & "<BR>"'DIRECCION DEL PDV
			cad=cad & "RUC <FONT class=clsNumero>" & gStrRUC & "</FONT><BR>"
			cad=cad & "<FONT class=clsNumero>" & Trim(Convert.ToDateTime(dsCompro.tables(0).rows(0)("AUDAT")).ToString("dd/MM/yyyy")) & " " & now.tostring("t") & "</FONT><BR>"'12/12/2006 11:23:25 p.m.
			CorrelativoSunat=replace(Trim(cstr(dsCompro.tables(0).rows(0)("XBLNR"))),"*","")
			cad=cad & "Tr. <FONT class=clsNumero>" & CorrelativoSunat & "</FONT><BR><FONT class=clsNumero>" & Trim(cstr(dsCompro.tables(0).rows(0)("ID_T_TRS_PEDIDO")) ) & "</FONT><BR>"'15-009-0129959/0101588843
			cad=cad & "Numero de Serie <FONT class=clsNumero>" & trim(cstr(session("SerieImprTicket"))) & "</FONT><BR>"'25049873
			
		else
			'*****************************************************************************************
			'Me esta generando un error, dbido a que retorna NULL
			'toma los datos del nuevo sp de impresion:
			'cad=cad & Trim(cstr(dsCompro.tables(0).rows(0)(19)) ) & "<BR>"'DIRECCION DEL PDV
			If Convert.IsDBNull(dsCompro.tables(0).rows(0)(19)) Then
				cad=cad & "" & "<BR>"'DIRECCION DEL PDV
			else
				cad=cad & Trim(cstr(dsCompro.tables(0).rows(0)(19)) ) & "<BR>"'DIRECCION DEL PDV	
			end if 
			
			'*****************************************************************************************
			
			cad=cad & "RUC <FONT class=clsNumero>" & gStrRUC & "</FONT><BR>"
			cad=cad & "<FONT class=clsNumero>" & Trim(cstr(dsCompro.tables(0).rows(0)(3)) ) & " " & now.tostring("t") & "</FONT><BR>"'12/12/2006 11:23:25 p.m.
			CorrelativoSunat=replace(Trim(cstr(dsCompro.tables(0).rows(0)(5))),"*","")
			
			'*********************************************************************'
			'***Validado para evitar los errores NULL ***'
			If Convert.IsDBNull(dsCompro.tables(0).rows(0)(4)) Then
				cad=cad & "Tr. <FONT class=clsNumero>" & CorrelativoSunat & "</FONT><BR><FONT class=clsNumero>" & "" & "</FONT><BR>"		'15-009-0129959/0101588843
			else
				cad=cad & "Tr. <FONT class=clsNumero>" & CorrelativoSunat & "</FONT><BR><FONT class=clsNumero>" & Trim(cstr(dsCompro.tables(0).rows(0)(4)) ) & "</FONT><BR>"'15-009-0129959/0101588843
			end if 
			'*********************************************************************'
			
			cad=cad & "Numero de Serie <FONT class=clsNumero>" & trim(cstr(session("SerieImprTicket"))) & "</FONT><BR>"'25049873
		end if
		cad=cad & "</TD>"
		cad=cad & "</TR>"
		if left(CorrelativoSunat,2) <> System.Configuration.ConfigurationSettings.AppSettings("k_Prefijo_Ticket") then
			'cad=cad & "<TR>"
			'cad=cad & "<TD>&nbsp;</TD>"
			'cad=cad & "</TR>"
			cad=cad & "<TR>"
			cad=cad & "<TD align='left'class=clsTexto>"
			cad=cad & "NOTA DE CREDITO"
			cad=cad & "</TD>"
			cad=cad & "</TR>"
			cad=cad & "<TR>"
			cad=cad & "<TD align='left' class=clsVigencia>"
			if(IsOffLine) then
				cad=cad & "Ref. Orig. :&nbsp; <FONT class=clsNumero>" & Trim(cstr("000000000"))
			else
			'**Control de Errores NULL **'
			'** " " **'
				If Convert.IsDBNull(dsCompro.Tables(0).Rows(0)(18)) Then
					cad=cad & "Ref. Orig. :&nbsp; <FONT class=clsNumero>" & ""
				else
					cad=cad & "Ref. Orig. :&nbsp; <FONT class=clsNumero>" & Trim(cstr(dsCompro.Tables(0).Rows(0)(18)))
				end if
			'*******************************'
			end if
			cad=cad & "</FONT></TD>"
			cad=cad & "</TR>"
		end if
						
		Dim objOff as new COM_SIC_OffLine.clsOffline
		Dim iCodVend%
		if(IsOffLine) then
			iCodVend= CInt(cstr(dsCompro.tables(0).rows(0)("VENDEDOR")))
			Dim nombreVendedor$ = objOff.ObtenerNombreCajero(cstr(dsCompro.tables(0).rows(0)("VENDEDOR")))

			CodVendedor= String.Format("{0:00000#####}", iCodVend)
			NomVendedor=nombreVendedor
			if(nombreVendedor = "" or IsNothing(nombreVendedor)) then
				CodVendedor= cstr(String.Format("{0:00000#####}", Session("USUARIO")))
				NomVendedor=Trim(cstr(Session("NOMBRE_COMPLETO")))
			end if

			CodCajero = cstr(Session("USUARIO"))
			NomCajero = Trim(cstr(Session("NOMBRE_COMPLETO")))		
		else
			CodVendedor=Trim(cstr(dsCompro.tables(0).rows(0)(6)))
			NomVendedor=Trim(cstr(dsCompro.tables(0).rows(0)(7)))
			CodCajero = strCodUsuario
			NomCajero = strNomUsuario
		end if
	end if
	return cad
End Function
	'---FIN FUNCTION CABECERA ------------------------------------------------------------------------------------------------------


	'---INFORMACIÒN DEL CLIENTE:
public Function FncImprCliente(byval dsCompro as System.Data.DataSet) as string
Dim cad as string
   
	if dsCompro.tables(0).rows.count>0 then
		cad= "<TR>"
		cad= cad & "<TD align='left' class=clsTexto>"
		if(IsOffLine) then
			cad= cad & Trim(cstr(dsCompro.tables(0).rows(0)("NOMBRE_CLIENTE"))) & "<BR>"'JAIME CORNEJO EZETA
			cad= cad & "<FONT class=clsNumero>" & Trim(cstr(dsCompro.tables(0).rows(0)("CLIENTE"))) & "</FONT><BR>"'10810842
		else
									'::IMP TICKET:: NOMBRE Y DOC IDENTIDAD DEL CLIENTE ::'
									cad= cad & Trim(cstr(dsCompro.tables(0).rows(0)(0))) & "<BR>"		'NOMBRE DEL CLIENTE: JAIME CORNEJO EZETA
									cad= cad & "<FONT class=clsNumero>" & Trim(cstr(dsCompro.tables(0).rows(0)(2))) & "</FONT><BR>"'DNI:  10810842
		end if
		cad= cad & "</TD>"
		cad= cad & "</TR>"
	end if 		
	return cad
End Function
	'---FIN DE LA INFORMACIÒN DEL CLIENTE ---'

	
	'=====Detalle del Pedido de la Venta ===================='
public Function FncImprDetVentas(byval dsCompro as System.Data.DataSet) as string
dim cad
    'dim objRecordSetA
    'dim DesTotal
    dim FlagRecarga as boolean
    DesTotal=0
	'Set objRecordSetA = XmlToRecordset(StrXml,"RS02")
					
	if len(trim(FncImprRecargas(dsCompro)))=0 then
	   FlagRecarga=false
	else   
	   FlagRecarga=true
	end if 
					
	'if not objRecordSetA is nothing then
	'	if not objRecordSetA.eof then
	if dsCompro.tables(2).rows.count>0 then
		   	'cad = "<TR><TD>&nbsp;</TD></TR>"		   	
			cad = "<TR>"
			cad = cad & "<TD>"
			cad = cad & "<TABLE WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0>"
			
			dim dr as System.Data.datarow
			dim indexTable as Integer
			'Do While (Not objRecordSetA.BOF AND Not objRecordSetA.EOF)
							
			if(IsOffline) then
				indexTable = 1
			else
								'indexTable = 2
								indexTable = 1		'se modifica debido a que los datos estan en la tabla(1) segùn como retorna el SP.'
			end if
			
							'*** copy disclaimer ***********'
								
			'-----------------------------------------------------
			' 02/10/2014: Adicionando lógica del Discleimer : JAZ
			'-----------------------------------------------------
			Dim dsParam As System.Data.DataSet			
			Dim objSicarDB As New COM_SIC_Cajas.clsCajas				
			Dim strGrupo as String = System.Configuration.ConfigurationSettings.AppSettings("constDisclaimerCodGrupo")		
			Dim strCodMat As String = System.Configuration.ConfigurationSettings.AppSettings("constDisclaimerIdMateriales")
			Dim strCodLabDis As String = System.Configuration.ConfigurationSettings.AppSettings("constDisclaimerIdEtiqueta")
			Dim strCodTamLab AS String = System.Configuration.ConfigurationSettings.AppSettings("constDisclaimerIdTamEtiqueta")
			Dim strCodMateriales() As String

								
			dsParam = objSicarDB.FP_ConsultaParametros(strGrupo)		
			For idx As Integer = 0 To dsParam.Tables(0).Rows.Count - 1
				'Si es igual al Label del Discleimer
				if(dsParam.Tables(0).Rows(idx).Item("SPARN_CODIGO") = strCodLabDis)
					strLabelDiscleimer = dsParam.Tables(0).Rows(idx).Item("SPARV_VALUE")
				'Si es igal al cod de los Ids de Discleimer	
				else if(dsParam.Tables(0).Rows(idx).Item("SPARN_CODIGO") = strCodMat)	
					strCodMateriales = dsParam.Tables(0).Rows(idx).Item("SPARV_VALUE").Split("|")									
				else if (dsParam.Tables(0).Rows(idx).Item("SPARN_CODIGO") = strCodTamLab)					
					strTamanoLabelDiscleimer = dsParam.Tables(0).Rows(idx).Item("SPARV_VALUE")
				end if															
			Next
			
			Dim strMATNR As String = ""
			Dim strNoMATNR As String = ""
			
							'*** end copy disclaimer primera parte *********'
							
							
							'===================================================================='
							'ESTE FOR IMPRIME EL DETALLE DE LA VENTA:
							'TODOS LOS ARTICULOS, ES DECIR LOS MATERIALES : " " 11-11-2014
							'===================================================================='
			for each dr in dsCompro.Tables(indexTable).rows 
				cad = cad & "<TR>"
				cad = cad & "<TD WIDTH=70% class=clsNumero>"																					
				
				if(IsOffLine) then
									'**code anterior **'
									'** servRec = Trim(cstr(dr("MATNR")))
									'** cad = cad & Trim(cstr(dr("MATNR"))) & "&nbsp;&nbsp;&nbsp;" & Trim(FormatNumber(cstr(dr("KWMENG")), 3))
									'**Nuevo code con disclaimer ***'
					servRec = Trim(cstr(dr("MATNR")))
					strMATNR = Trim(cstr(dr("MATNR")))
					strMATNR = "* &nbsp;" & strMATNR	
					'Si existe en el arreglo de materiales, agrega el comodin *
					if FncBuscarCadenaInArray(cstr(dr("MATNR")),strCodMateriales) then
						isDiscleimer = true
						cad = cad & strMATNR & "&nbsp;&nbsp;&nbsp;" & Trim(FormatNumber(cstr(dr("KWMENG")), 3))
					else
						cad = cad & Trim(cstr(dr("MATNR"))) & "&nbsp;&nbsp;&nbsp;" & Trim(FormatNumber(cstr(dr("KWMENG")), 3))	
					end if
				else
									'**Code anterior :
									'servRec = Trim(cstr(dr(1)))
									'EN ESTA LINEA VA EL ARTICULO Y LA CANTIDAD (DEPEC_CODMATERIAL - DEPEN_CANTIDAD)
									'cad = cad & "* " & Trim(cstr(dr(1))) & "&nbsp;&nbsp;&nbsp;" & Trim(cstr(dr(2)))		'Linea Actual
									
									'*************************************************************'
									'** Nuevo code con disclaimer :
					servRec = Trim(cstr(dr(1)))	
					strNoMATNR = Trim(cstr(dr(1)))
					strNoMATNR = "* &nbsp;" & strNoMATNR											
					'Si existe en el arreglo de materiales, agrega el comodin *
					if FncBuscarCadenaInArray(cstr(dr(1)),strCodMateriales) then
						isDiscleimer = true
						cad = cad & strNoMATNR & "&nbsp;&nbsp;&nbsp;" & Trim(cstr(dr(2)))
					else
						cad = cad & Trim(cstr(dr(1))) & "&nbsp;&nbsp;&nbsp;" & Trim(cstr(dr(2)))
					end if	
									'*** hasta este bloque hemos copiado el disclaimer -<	
				end if															
				
				
				Dim preUnit as Double =0
				Dim igvRecaudacion as Double =0
				Dim pUnitRV as Double =0
				Dim Tot as Double = 0
								
								'-----------------------------------------------------------
								'PARA LA VERSION 6.0 SEGUN LO INDICADO NO USA EL IsOffLine
				if(IsOffLine) then
					Tot = CDbl(dr("TOTAL_PAGO"))
					pUnitRV = CDbl(System.Configuration.ConfigurationSettings.AppSettings("precioUnitarioRecarga"))
					preUnit = Math.Round(Tot * pUnitRV, 2)
				end if
								'-----------------------------------------------------------
				
				cad = cad & "</TD>"
				cad = cad & "<TD WIDTH=30% ALIGN=RIGHT class=clsTexto>"
								
				if(IsOffLine) then
					cad = cad & "S/<FONT class=clsNumero>" & cant_dec(Trim(cstr(preUnit))) & "&nbsp;</FONT>"'10350.00
				else
								'DEPEN_TOTALDOCUMENTO:
					cad = cad & "S/<FONT class=clsNumero>" & cant_dec(Trim(cstr(dr(11)))) & "&nbsp;</FONT>"'10350.00
									'cad = cad & "S/<FONT class=clsNumero>" & cant_dec(Trim(cstr(dr(6)))) & "&nbsp;</FONT>"'10350.00
				end if
				
				cad = cad & "</TD>"
				cad = cad & "</TR>"
				cad = cad & "<TR>"
				cad = cad & "<TD WIDTH=70% class=clsTexto >"'colspan='2'
				
				
				
				
				if(IsOffLine) then
					cad = cad & Trim(cstr(dsCompro.tables(1).rows(0)("DESCRIPCION_PRODUCTO"))) & "&nbsp;"
				else
									'DEPEV_DESCMATERIAL: EJEMPLO: PACK BASICO NOKIA 6103
					cad = cad & Trim(cstr(dr(4))) & "&nbsp;"
				end if
								
				cad = cad & "</TD>"
				cad = cad & "</TR>"
								
				if (not FlagRecarga or (dsCompro.Tables(2).Rows(0)(1)="SERRCDTHTM")) then
					cad = cad & "<TR>"
					cad = cad & "<TD WIDTH=70% class=clsTexto >"'colspan='2'
					if(IsOffLine) then						
						'Dim cantidadProducto as Integer
						Dim totalVendido as Double
						Dim precioUnitario as Double
						
						'cantidadProducto = CInt(dr("KWMENG"))
						Dim cantidadProducto as Double = Cdbl(dsCompro.tables(1).rows(0)("KWMENG"))
						'Dim igvRecaudacion as Double = CDbl(System.Configuration.ConfigurationSettings.AppSettings("valorIGV") + 1)
						
						totalVendido = CDbl(dr("TOTAL_PAGO"))

						precioUnitario = Math.Round(totalVendido / igvRecaudacion, 2)

						cad = cad & "Pvta:&nbsp;S/<FONT class=clsNumero>" & cant_dec(Trim(cstr(precioUnitario))) & "</FONT>&nbsp;&nbsp;Dscto:&nbsp;S/<FONT class=clsNumero>" & cant_dec(Trim(cstr(dr("DESCUENTO")))) & "</FONT>&nbsp;</FONT>&nbsp;"
					else
										'Pvta: DEPEN_PRECIOUNITARIO \\ DEPEN_DESCUENTO=10
										'cad = cad & "Pvta: S/.<FONT class=clsNumero>" & cant_dec(Trim(cstr(dr(5)))) & "</FONT> Dscto: S/.<FONT class=clsNumero>" & cant_dec(Trim(cstr(dr(6)))) & "</FONT></FONT>"
										cad = cad & "Pvta: S/<FONT class=clsNumero>" & cant_dec(Trim(cstr(dr(5)))) & " Dscto: S/<FONT class=clsNumero>" & cant_dec(Trim(cstr(dr(6)))) & "</FONT>"
					end if
					cad = cad & "</TD>"
					cad = cad & "</TR>"
				end if 
				
				dim strSerie as string=""
				If Convert.IsDBNull(dr(15)) Then
					strSerie=""
				else
					strSerie =Trim(cstr( dr(15)))
				end if 
								
								'=====================================================================================================================================
								':: EN ESTA PARTE DEBE IR SERIE Y TELEFONO, ACTUALMENTE COMENTADO *** CONSULTAR SU SI VA A MOSTRAR *** Y DE DONDE VIENE ESTOS DATOS**
				cad = cad & "<TR>"
								cad = cad & "<TD WIDTH=70% class=clsTexto colspan='2' >"'colspan='2' aix
				'cad = cad & "<FONT class=clsNumero>8951100104012724782</FONT> &nbsp;/&nbsp;<FONT class=clsNumero>0649312952</FONT>&nbsp;"
								'cad = cad & "<FONT class=clsNumero>Serie(s): " & Trim(cstr( dr(15))) & " </FONT><br/><FONT class=clsNumero>Teléfono: " & Trim(cstr(dr(16))) &"</FONT>"
								cad = cad & "<FONT class=clsNumero>Serie(s): " & Trim(strSerie) & " </FONT><br/><FONT class=clsNumero>Teléfono: " & Trim(cstr(dr(16))) &"</FONT>"
								
								'===================================================================================================================================
				
								
								'Lineas nuevas al migrar la lógica del disclaimer'
				dim docSapPrep as string
				dim strTelefono as string 
    			docSapPrep = request.QueryString("codRefer")  
								'Hasta aqui la migraciòn del code disclaimer 3:04pm
								'*************************************************
								
    
				if(IsOffLine) then
									'Lineas anteriores:
									'If(Not IsNothing(Session("numeroTelefono"))) Then
									'	cad = cad & "<FONT class=clsNumero>" & Trim(cstr("Telefono: 000000" + Session("numeroTelefono"))) & "</FONT>&nbsp;"
									'End If					
									'cad = cad & "</TD>"
									'cad = cad & "</TR>"
									'DesTotal=DesTotal + cant_dec(Trim(cstr(dr("DESCUENTO"))))
								
									'______________________________________________________'
									'*** Nuevas Lineas al migrar el code del disclaimer ***'
					dim numeroTelefono=dsCompro.tables(1).rows(0)("ZZNRO_TELEF")
					If(Not IsNothing(numeroTelefono)) Then
						cad = cad & "<FONT class=clsNumero>" & Trim(cstr("Telefono: 000000" + numeroTelefono)) & "</FONT>&nbsp;"
					End If					
					cad = cad & "</TD>"
					cad = cad & "</TR>"
				
								else
									'=== BLOQUE ANTES DEL DISCLAIMER ==='
									'
									'cad = cad & "<FONT class=clsNumero>" & Trim(cstr(dr(12))) & "</FONT>&nbsp;"  
									'===PORCENTAJE DE DESCUENTO:==== ** CONSULTAR ***-------------------------------
									'En el ejemplo del ticket no lo considera, lo cometare 11.11.14
									'cad = cad & "<FONT class=clsNumero>" & Trim(cstr(dr(7))) & "</FONT>&nbsp;"  
									'-------------------------------------------------------------------------------
					
									'LINEAS QUE ESTAN FUNCIONANDO HASTA ANTES DE MIGRAR EL CODE DEL DISCLAIMER '
									'cad = cad & "</TD>"
									'cad = cad & "</TR>"				
									'DesTotal=DesTotal + cant_dec(Trim(cstr(dr(6))))
									'================================================================================'
					
									'** NUEVO BLOQUE AL MIGRAR EL DISCLAIMER ***'
						' INICIO FMES : VER SI ES UNA VENTA PREPAGO
						Dim strProdPrep As String = "0"						
						Dim dtDatos As New System.Data.DataTable
						strTelefono = ""
						Dim objBus As New COM_SIC_Activaciones.ClsActivacionPel
						dtDatos = objBus.ListarDatosCabeceraVenta(Cstr(docSapPrep))
											
							
								'Continua el code Disclaimer:
									If dtDatos.Rows.Count > 0 Then
							Dim strCodigoResp As String = ""
							Dim lista As ArrayList = objBus.Lis_Lista_Detalle_Venta_Prepago(Cstr(docSapPrep), strCodigoResp)
							
							For Each item As COM_SIC_Activaciones.DetalleVentaPrepago In lista
							
								if cstr(dr(12)).IndexOf(item.SERIE_CHIP) > -1  then
									strTelefono = Microsoft.VisualBasic.Strings.Right("000000000000000000" & cstr(item.LINEA), 15) 
								end if
								
								if cstr(dr(12)).IndexOf(item.SERIE_EQUI) > -1  then
									strTelefono = Microsoft.VisualBasic.Strings.Right("000000000000000000" & cstr(item.LINEA), 15) 
								end if
								If item.COD_PROD_PREP = "01"	    	then
									strProdPrep ="1"
								end if
							next
						End If
						' FIN FMES : VER SI ES UNA VENTA PREPAGO
						
				    If dtDatos.Rows.Count > 0 Then				    	
						cad = cad & "<FONT class=clsNumero>" & Trim(cstr(dr(12))) & "</FONT>&nbsp;"			
						If strProdPrep = "1"	    	then
							cad = cad & "</TD>"
							cad = cad & "</TR>"
				    		cad = cad & "<TR>"
							cad = cad & "<TD WIDTH=70% class=clsTexto >"
						
							cad = cad & "<FONT class=clsNumero>" & Trim(cstr("Teléfono: " + strTelefono)) & "</FONT>&nbsp;"
						end if
					else
										'SE COMENTARA DEBIDO A QUE LO IMPRE LINEA ARRIBA.
										'En esta line impre el campo que hace referencia a la SERIE/TELEFÒNO
										'cad = cad & "<FONT class=clsNumero>" & Trim(cstr(dr(12))) & "</FONT>&nbsp;"  
					end if
					
					cad = cad & "</TD>"
					cad = cad & "</TR>"				
					DesTotal=DesTotal + cant_dec(Trim(cstr(dr(6))))
				end if
			next
							'==FIN DEL FOR ===============================================================================
							
			cad = cad & "</TABLE>"
			cad = cad & "</TD>"
			cad = cad & "</TR>"
		end if 		
return cad
End Function

public Function FncImprRecargas(byval dsCompro as System.Data.DataSet) as string
dim cad
dim cadRV
cadRV="0"
dim recargaefectiva,valorventa,descuento, igv, totalpago, numrecarga as decimal
dim cteMSG_GLOSA_RECARGAVIRTUAL_1 as string = System.Configuration.ConfigurationSettings.AppSettings("cteMSG_GLOSA_RECARGAVIRTUAL_1")
dim cteMSG_GLOSA_RECARGAVIRTUAL_2 as string = System.Configuration.ConfigurationSettings.AppSettings("cteMSG_GLOSA_RECARGAVIRTUAL_2")
Dim indexTable as Integer

if(IsOffLine) then
	indexTable=1
else
			'------------------------------------------------------------------------------------------------------------------
			'indexTable=4		'Se cambiara, debido a que no existe tabla 4, el sp tiene salida tabla(0) a Tabla(3)
			indexTable=3
			'------------------------------------------------------------------------------------------------------------------
end if
'*****Recarga Virtual****
'set objRecordSetRV= XmlToRecordset(StrXml,"RS06")

		dtIGV = Session("Lista_Impuesto")
		if dtIGV.rows.count = 0 then
			%>
				<script language="javascript">
					alert('No existe IGV configurado');
				</script>
		<% Response.end
		end if
		Dim IGVactual As string 
		IGVactual = "0" 
		
		For Each row As System.Data.DataRow In dtIGV.Rows
			If (CDate(Date.Now().ToString("dd/MM/yyyy"))>= CDate(row("impudFecIniVigencia").ToString.Trim) And CDate(Date.Now().ToString("dd/MM/yyyy")) <= CDate(row("impudFecFinVigencia").ToString.Trim) And CInt(row("impunTipDoc").ToString.Trim) = 0) Then
				IGVactual = Math.Round(CDec(row("IGV").ToString.Trim()) / 100, 2).ToString
				Exit For
			End If
		Next
		
		if dsCompro.tables(indexTable).rows.count>0  or IsOffLine then		'ERROR, debido a que el procedure actual no retorna la tabla 4, sino tabla 3
			'if dsCompro.tables(indexTable).rows(0)(4) > 0 or IsOffLine then	'ERROR apunta a un campo fecha, y validad que sea mayor a 4
			if IsOffLine then
		if(IsOffLine) then
			if (dsCompro.tables(1).rows(0)("MATNR").StartsWith("SERECVI")) then
				Dim totalRecargado as Double = Cdbl(dsCompro.tables(1).rows(0)("KWMENG"))
				Dim igvRecaudacion as Double = CDbl(CDec(IGVactual)+ 1)
				Dim pUnitarioRV as Double = CDbl(System.Configuration.ConfigurationSettings.AppSettings("precioUnitarioRecarga"))
				Dim valVen as Double = 0
				recargaefectiva= totalRecargado
				
				valorventa= Math.Round((recargaefectiva/igvRecaudacion), 2)
				'valorventa= Math.Round((recargaefectiva*pUnitarioRV), 2)
				
				descuento= dsCompro.tables(1).rows(0)("DESCUENTO")
				igv= recargaefectiva - valorventa
				
				totalpago= recargaefectiva
				
				'recargaefectiva= dsCompro.tables(1).rows(0)("VAL_VENTA")
				'valorventa= dsCompro.tables(1).rows(0)("VAL_VENTA")
				'descuento= dsCompro.tables(1).rows(0)("DESCUENTO")
				'igv= dsCompro.tables(1).rows(0)("IGV")
				'totalpago= dsCompro.tables(1).rows(0)("TOTAL_PAGO")
				
				numrecarga= dsCompro.tables(1).rows(0)("NRO_REC_SWITCH")
				cadRV="1"
				
			Else
				Return ""
			End If
		else
				'___________________________________________________'
				'--BLOQUE QUE ENTRA IMPRESION xD -------------------
			recargaefectiva= dsCompro.tables(4).rows(0)(4)
			valorventa= dsCompro.tables(4).rows(0)(5)
			descuento= dsCompro.tables(4).rows(0)(6)
			igv= dsCompro.tables(4).rows(0)(7)
			totalpago= dsCompro.tables(4).rows(0)(8)
			numrecarga= dsCompro.tables(4).rows(0)(9)
			cadRV="1"			
				'--BLOQUE QUE ENTRA IMPRESION xD -------------------
				
		end if

		'cad="<TR><TD>&nbsp;</TD></TR>"
		cad = "<TR>"
		cad=cad + "<TD>"
		cad=cad + "<TABLE WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0 class=clsVigencia>" 'class=clsVigencia 
		cad=cad + "<TR >"
		cad=cad + "<TD WIDTH=50% >NUM RECARGA</TD><TD WIDTH=25% >:</TD><TD WIDTH=15% ALIGN=RIGHT class=clsNumero>" & cstr(numrecarga) & "</TD><TD WIDTH=20% ></TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=48% >RECARGA EFECT.</TD><TD WIDTH=2% >:</TD><TD WIDTH=30% ALIGN=RIGHT class=clsNumero>s/" & cant_dec(cstr(recargaefectiva)) & "</TD><TD WIDTH=20% ></TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=48% >VALOR VENTA</TD><TD WIDTH=2% >:</TD><TD WIDTH=30% ALIGN=RIGHT class=clsNumero>s/" & cstr(valorventa) & "</TD><TD WIDTH=20% ></TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=48% >DESCUENTO</TD><TD WIDTH=2% >:</TD><TD WIDTH=30% ALIGN=RIGHT class=clsNumero>s/" & cant_dec(cstr(descuento)) & "</TD><TD WIDTH=20% ></TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=48% >SUB TOTAL</TD><TD WIDTH=2% >:</TD><TD WIDTH=30% ALIGN=RIGHT class=clsNumero>s/" & cstr(valorventa-descuento) & "</TD><TD WIDTH=20% ></TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=48% >I.G.V</TD><TD WIDTH=2% >:</TD><TD WIDTH=30% ALIGN=RIGHT class=clsNumero>s/" & cstr(igv) & "</TD><TD WIDTH=20% ></TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=48% >TOTAL PAGO</TD><TD WIDTH=2% >:</TD><TD WIDTH=30% ALIGN=RIGHT class=clsNumero>s/" & cstr(cant_dec(totalpago)) & "</TD><TD WIDTH=20% ></TD>"
		cad=cad + "</TR>"
		'cad=cad + "<TR><TD>&nbsp;</TD></TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD colspan=4 >" & cteMSG_GLOSA_RECARGAVIRTUAL_1  & cteMSG_GLOSA_RECARGAVIRTUAL_2 & "</TD>"
		cad=cad + "</TR>"
		cad=cad + "</TABLE>"
		cad=cad + "</TD>"
		cad=cad + "</TR>"
		
		end if
	end if

	return cad
End Function

public Function FncImpCuotas(byval dsCompro as System.Data.DataSet) as string
dim cad
dim scFecha
dim strDesCuota
dim PosVen as integer
    'dim objRecordSetA
'	Set objRecordSetA = XmlToRecordset(StrXml,"RS03")
	'if not objRecordSetA is nothing then
	'	if not objRecordSetA.eof then
	if dsCompro.tables(1).rows.count>0 then
			'cad="<TR><TD>&nbsp;</TD></TR>"			
			cad="<TR>"
			cad=cad + "<TD>"
			cad=cad + "<TABLE WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0>"

		   NumeroCuotas=-1
		   dim dr as system.data.datarow
		   for each dr in dsCompro.tables(1).rows
			'Do While (Not objRecordSetA.BOF AND Not objRecordSetA.EOF)
				NumeroCuotas=NumeroCuotas+1
				FlagCuotas=True
				scFecha = cstr(dr(2))
				strDesCuota = trim(cstr(dr(1)))
				PosVen=instr(1,strDesCuota,"Vencimiento")
				if PosVen>0 then
				strDesCuota=mid(strDesCuota,1,posVen-1)
				end if 
				PosVen=instr(1,strDesCuota,":")
				if PosVen>0 then
				strDesCuota=mid(strDesCuota,1,posVen-1)
				end if 
				strDesCuota=trim(strDesCuota)
				if (Trim(scFecha)="12:00:00 a.m." or scFecha= "00000000") then scFecha = ""
			    
			    if NumeroCuotas = 0 then
				     Dim obSap as new SAP_SIC_PAGOS.clsPagos
  				     dim dsPedido as System.Data.Dataset = obSap.Get_ConsultaPedido("",session("ALMACEN"),request.QueryString("codRefer"),"")
					 if dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") then
					    strDesCuota = strDesCuota & "&nbsp;" & ConfigurationSettings.AppSettings("gConstPorcPrePago") & "%" & "&nbsp;"
					    strDesCuota = Replace(strDesCuota," ","&nbsp;")
					 end if
					 dsPedido = nothing
				end if
			
				cad=cad & "<TR>"
				cad=cad & "<TD WIDTH=32% class=clsTexto>"
				cad=cad & strDesCuota & "&nbsp;"'Cuota Inicial
				cad=cad & "</TD>"
				cad=cad & "<TD WIDTH=13% >"
				cad=cad & FormatFecha(scFecha) & "&nbsp;"
				cad=cad & "</TD>"
				cad=cad & "<TD WIDTH=35% ALIGN=RIGHT class=clsTexto>"
				cad=cad & "S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dr(3)))) &  "&nbsp;</FONT>"'10012.30
				cad=cad & "</TD>"
				cad=cad & "<TD WIDTH=20% >"
				cad=cad & "&nbsp;"
				cad=cad & "</TD>"
				cad=cad & "</TR>"
  		    next
	cad=cad + "</TABLE>"
	cad=cad + "</TD>"
	cad=cad + "</TR>"
end if 		
return cad
End Function

public Function FormatFecha(byval strFecha as string)  as string
dim Resp
dim ArrFecha() as string
Resp = strFecha
if trim(strFecha)<>"" then
   ArrFecha= strFecha.split("/")
   if ubound(ArrFecha)>=1 then
	  Resp = ArrFecha(0) & "/" & ArrFecha(1)
	else
	  Resp = strFecha.substring(6,2) & "/" & strFecha.substring(4,2)
   end if 
end if 
FormatFecha=Resp 
End Function 

		'## 10.11.14 ##'
		'## Verificar las columnas para imprimir los totales:
public Function FncImprTotales (byval dsCompro as System.Data.DataSet) as string
dim cad
	Dim indexTable as Integer
	if(IsOffLine) Then
		indexTable = 1
	else
				'indexTable = 3
				'---------------------------------------------------------------------------
				indexTable =2		' lOS DATOS EN EL NUEVO SP ESTAN EN LA TABLA Nº 2
				'---------------------------------------------------------------------------
	end if
	
    if dsCompro.tables(indexTable).rows.count>0 then
		'cad="<TR><TD>&nbsp;</TD></TR>"	
		cad="<TR>"
		cad=cad + "<TD>"
		cad=cad + "<TABLE WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=65% class=clsTexto>"
		cad=cad + "TOTAL DSCTOS."
		cad=cad + "</TD>"
		cad=cad + "<TD WITDH=35% ALIGN=RIGHT class=clsTexto>"
		cad=cad + "&nbsp;S/ <FONT class=clsNumero>" & cant_dec(DesTotal) & "</FONT>"
		cad=cad + "</TD>"
		cad=cad + "</TR>"
				
				'===============================================================================
				'::: SUB TOTAL :::
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=65% class=clsTexto>"
		cad=cad + "SUB TOTAL"
		cad=cad + "</TD>"
		cad=cad + "<TD WITDH=35% ALIGN=RIGHT class=clsTexto>"
		if(IsOffline) Then
			cad=cad + "&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dsCompro.tables(indexTable).rows(0)("VAL_VENTA")))) & "</FONT>"
		else
					'cad=cad + "&nbsp;S/. <FONT class=clsNumero>" & cant_dec(trim(cstr(dsCompro.tables(indexTable).rows(0)(2)))) & "</FONT>"
			cad=cad + "&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dsCompro.tables(indexTable).rows(0)(2)))) & "</FONT>"
		end if
		cad=cad + "</TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
				'===============================================================================
				
		dtIGV = Session("Lista_Impuesto")
		if dtIGV.rows.count = 0 then
			%>
				<script language="javascript">
					alert('No existe IGV configurado');
				</script>
			<% Response.end
		end if
		Dim IGVactual As string
		IGVactual = "0"
		For Each row As System.Data.DataRow In dtIGV.Rows
			If (CDate(Date.Now().ToString("dd/MM/yyyy"))>= CDate(row("impudFecIniVigencia").ToString.Trim) And CDate(Date.Now().ToString("dd/MM/yyyy")) <= CDate(row("impudFecFinVigencia").ToString.Trim) And CInt(row("impunTipDoc").ToString.Trim) = 0) Then
				IGVactual = Math.Round(CDec(row("IGV").ToString.Trim()) / 100, 2).ToString
				Exit For
			End If
		Next		
				
		cad=cad + "<TD WIDTH=65% class=clsTexto>"
		if(IsOffline) Then
			Dim total# =CDbl(dsCompro.tables(1).rows(0)("TOTAL_PAGO"))
			Dim igv# = CDbl(CDec(IGVactual) + 1)
			Dim Redo# = 0
			Dim Sbt#
			Dim MontoIGV#
			Dim preUNit#
	        
			Sbt = total / igv
			Dim monsinr = Math.Round(Sbt, 3)
			Dim monround = Math.Round(Sbt, 2)
			preUNit = Math.Round(Sbt, 2)
			Redo = Math.Round(monround - monsinr, 2)
			MontoIGV# = (total - preUNit) + Redo
		
		
			cad=cad + "I.G.V " & CInt((CDec(IGVactual)*100)).ToString() & "%</FONT>"
			cad=cad + "<TD WITDH=35% ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(MontoIGV))) & "</FONT></TD>"
			cad=cad + "</TR>"
			cad=cad + "<TR>"
			cad=cad + "<TD WIDTH=65% class=clsTexto>AJUSTE REDONDEO</TD>"
			cad=cad + "<TD WITDH=35% ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & negativo(cant_dec(trim(cstr(Redo)))) & "</FONT></TD>"
			cad=cad + "</TR>"
			cad=cad + "<TR>"
			cad=cad + "<TD WIDTH=65% class=clsTexto>TOTAL</TD>"
			cad=cad + "<TD WITDH=35% ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(total))) & "</FONT></TD>"
			cad=cad + "</TR>"
		else
					'========================================================================================'
					'Linea que me esta causando error por el orden de los campos del nuevo procedure.
					'La tabla(3) => que me genera el SP no me retonar el IGV.
					'========================================================================================'
					
					'===DEPEN_IGV===
					'cad=cad + "I.G.V " & trim(cstr(dsCompro.tables(3).rows(0)(4))) & "%</FONT>"	'Line actual
					cad=cad + "I.G.V " & trim(cstr(dsCompro.tables(2).rows(0)(4))) & "%</FONT>"		'Line actualizada					
					'cad=cad + "<TD WITDH=35% ALIGN=RIGHT class=clsTexto>&nbsp;S/. <FONT class=clsNumero>" & cant_dec(trim(cstr(dsCompro.tables(3).rows(0)(3)))) & "</FONT></TD>"
					cad=cad + "<TD WITDH=35% ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dsCompro.tables(2).rows(0)(3)))) & "</FONT></TD>"
			cad=cad + "</TR>"
					
					'===DEPEN_AJUSTE===
			cad=cad + "<TR>"
			cad=cad + "<TD WIDTH=65% class=clsTexto>AJUSTE REDONDEO</TD>"
					'cad=cad + "<TD WITDH=35% ALIGN=RIGHT class=clsTexto>&nbsp;S/. <FONT class=clsNumero>" & negativo(cant_dec(trim(cstr(dsCompro.tables(3).rows(0)(5))))) & "</FONT></TD>"
					cad=cad + "<TD WITDH=35% ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & negativo(cant_dec(trim(cstr(dsCompro.tables(2).rows(0)(5))))) & "</FONT></TD>"
			cad=cad + "</TR>"
					
					'===TOTAL===
			cad=cad + "<TR>"
			cad=cad + "<TD WIDTH=65% class=clsTexto>TOTAL</TD>"
					'cad=cad + "<TD WITDH=35% ALIGN=RIGHT class=clsTexto>&nbsp;S/. <FONT class=clsNumero>" & cant_dec(trim(cstr(dsCompro.tables(3).rows(0)(6)))) & "</FONT></TD>" 'line actual
					cad=cad + "<TD WITDH=35% ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dsCompro.tables(2).rows(0)(6)))) & "</FONT></TD>"  '#temporal
			cad=cad + "</TR>"
		end if
		
		if(IsOffline) Then
			if len(trim(cstr(dsCompro.tables(1).rows(0)("IGV"))))<>0 then
				'cad=cad + "<TR><TD>&nbsp;</TD></TR>"
				cad=cad + "<TR>"				
				cad=cad + "<TD WIDTH=65% class=clsTexto colspan='2'>" & "" & "</TD>"
				'cad=cad + "<TD WIDTH=65% class=clsTexto colspan='2'>" & trim(cstr(0)) & "</TD>" ' ???PARA QUE SERA
				cad=cad + "</TR>"
			end if			
		Else
					'---------------------------------------------------------------------------------------------------'
					'***CONSULTAR BLOQUE: SE VA A COMENTAR 10.11.2014 *** CONSULTAR QUE VA IMPRIMIR DESPUES DEL TOTAL
					'if len(trim(cstr(dsCompro.tables(3).rows(0)(7))))<>0 then
					'	'cad=cad + "<TR><TD>&nbsp;</TD></TR>"		'--- esto ya estaba comentado, asi debe quedar---'
					'	cad=cad + "<TR>"
					'	'cad=cad + "<TD WIDTH=65% class=clsTexto colspan='2'>" & trim(cstr(dsCompro.tables(3).rows(0)(7))) & "</TD>"	 '#Linea como estaba
					'	cad=cad + "</TR>"
					'end if
					'----------------------------------------------------------------------------------------------------'
		End IF		

				'--ESTE BLOQUE SE ENCONTRO COMENTADO:
		'Dim obSap as new SAP_SIC_PAGOS.clsPagos
		'dim dsPagos = obSap.Get_ConsultaPagos(Session("ALMACEN"),replace(Trim(cstr(dsCompro.tables(0).rows(0)(5))),"*",""),"")
		
		'dim dsPedido1 as System.Data.Dataset = obSap.Get_ConsultaPedido("",session("ALMACEN"),request.QueryString("codRefer"),"")
		
	    'FlagPrepago = False	
	    
		'if dsPedido1.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") and dsPedido1.Tables(0).Rows(0).Item("CLASE_VENTA")=ConfigurationSettings.AppSettings("strDTVAlta") then
		'	FlagPrepago = True
		'end if
		'dsPedido1 = nothing		
		
		'dim i as integer
		'if not isnothing(dsPagos) then
		'  if dsPagos.Tables(0).Rows.Count > 0 then
		'	cad=cad + "<TR><TD>&nbsp;</TD></TR>"
		'	cad=cad + "<TR class=clsTexto><TD>FORMAS DE PAGO"
		'	cad=cad + "</TD></TR>"  
		'	for i=0 to dsPagos.Tables(0).Rows.Count-1
		'		cad=cad + "<TR class=clsTexto>"
		'		cad=cad + "<TD WIDTH=65% class=clsTexto>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dsPagos.Tables(0).Rows(i).Item("GLOSA") + "</TD><TD WITDH=35% ALIGN=RIGHT class=clsTexto>&nbsp;S/.<FONT class=clsNumero>" + dsPagos.Tables(0).Rows(i).Item("IMPORTE") + "</FONT></TD>"
		'		cad=cad + "</TR>"
		'	next
		' end if
		'end if
		
		'Prueba
		'cad = cad + "<tr><td>&nbsp;</td></tr>"
		'cad = cad + "<tr><td colspan=2 class=clsTexto>" + obSap.Get_GlosaCampaña(Trim(cstr(dsCompro.tables(0).rows(0)(4))),Session("ALMACEN")) + "</td></tr>"
				'----------------FIN BLOQUE
		
		cad=cad + "</TABLE>"
		cad=cad + "</TD>"
		cad=cad + "</TR>"
  end if 		
return cad
End Function

public Function FncImprVendedores() as string
dim cad
dim tam
dim tamstrcaj
Dim nombreVendedor
Dim nombreCajero
tam=len(codVendedor)
tamstrcaj=len(codCajero)

'codCajero=string(tam-tamstrcaj,"0") & codCajero 
codCajero = codCajero.padleft(tam-tamstrcaj,"0")
'cad="<TR><TD>&nbsp;</TD></TR>"
cad="<TR>"
cad=cad & "<TD class=clsTexto>"
cad=cad & "<FONT class=clsNumero>V: 00000"& codVendedor & "</FONT> " & mid(nomVendedor,1,14) & "<BR>"
cad=cad & "<FONT class=clsNumero>C: " & codCajero & "</FONT> " & mid(nomCajero,1,14) & "<BR>"
cad=cad & "</TD>"
cad=cad & "</TR>"
if trim(Reimpresion)<>"" then
	cad=cad & "<TR>"
	cad=cad & "<TD align='center' class=clsTexto>"
	cad=cad & "***REIMPRESION***"
	cad=cad & "</TD>"
	cad=cad & "</TR>"
end if 
cad=cad & "<TR>"
cad=cad & "<TD align='center' class=clsTexto>"
cad=cad & "Tr. <FONT class=clsNumero>" & CorrelativoSunat & "</FONT>"
cad=cad & "</TD>"
cad=cad & "</TR>"
cad=cad & "<TR>"
dim objPagos as new SAP_SIC_Pagos.clsPagos
Dim objOff as new COM_SIC_OffLine.clsOffline
cad=cad & "<TD align='center' class=clsTexto>"
'cad=cad & "T/C: S/.&nbsp;<FONT class=clsNumero>" & objPagos.Get_TipoCambio(Now.ToString("d")).ToString("N2") & "</FONT>"
cad=cad & "T/C: S/&nbsp;<FONT class=clsNumero>" & objOff.Obtener_TipoCambio(right("00" & Now.Day, 2) & "/" & right("00" & Now.Month, 2) & "/" & right("0000" & Now.Year, 4)).ToString("N3") & "</FONT>"
cad=cad & "</TD>"
cad=cad & "</TR>"
return cad
End Function

public Function FncPieCuotas (byval dsCompro as System.Data.DataSet) as string
dim cad
dim cteMSG_GLOSA_VENTA_CUOTAS = ConfigurationSettings.AppSettings("cteMSG_GLOSA_VENTA_CUOTAS")
If(Not IsOffline) then
	if dsCompro.tables(1).rows.count>0 then 
	'cad="<TR><TD>&nbsp;</TD></TR>"
	cad="<TR>"
	'cad=cad & "<TD class=clsTexto>*** FINANCIADO EN " & cstr(NumeroCuotas) & " CUOTA(S) EN RECIBO POSTPAGO ***</TD>"
	'cad=cad & "<TD class=clsTexto>" & Replace(cteMSG_GLOSA_VENTA_CUOTAS, "#", cstr(NumeroCuotas)) & "</TD>"
	'-----------------------------------------------------
	' 25/11/2011: No debe Imprimirse las Cuotas
	'-----------------------------------------------------
	cad=cad & "<TD class=clsTexto>" & Replace(cteMSG_GLOSA_VENTA_CUOTAS, "#", "") & "</TD>"
	cad=cad & "</TR>"
	end if
End If
return cad
End Function

public Function FncPiePrepago () as string
dim cad
if FlagPrepago then 
'cad="<TR><TD>&nbsp;</TD></TR>"
cad="<TR>"
'cad=cad & "<TD class=clsTexto>*** AL RETIRAR EL CHIP DEL EQUIPO PERDERA TODOS LOS BONOS RECIBIDOS***</TD>"
cad=cad & "<TD class=clsTexto>" & ConfigurationSettings.AppSettings("cteMSG_GLOSA_VENTA_PREPAGO") & "</TD>"
cad=cad & "</TR>"
end if 
return cad
End Function

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
	
public Function negativo(byval cadena as string) as string
		if(len(trim(cadena))>4) then
			negativo = "(" + trim(mid(cadena,2)) + ")"
		else
			negativo = cadena
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
	
	'javier 03/2009
	public Function FncImprFormaPago(byval dsCompro as System.Data.DataSet) as string
		dim cad
		Dim indexTable = IIf(IsOffline,1,3)
		
		if dsCompro.tables(indexTable).rows.count>0 then	
			cad = "<TR>"
			cad=cad + "<TD>"
			'cad=cad + "<TABLE WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0 class=clsVigencia>"  'line actual
			cad=cad + "<TABLE WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0 class=clsTexto>"   'line prueba
			
						
			
			'=====================================================================================================================================
			'**CONSULTAR: PARA LAS FORMAS DE PAGO, ESTA HACIENDO UNA CONSULTA FUERA DE SINERGIA
			'Coment:
			Dim obSap as new SAP_SIC_PAGOS.clsPagos
			dim dsPagos = obSap.Get_ConsultaPagos(Session("ALMACEN"),replace(Trim(cstr(dsCompro.tables(0).rows(0)(5))),"*",""),"")
			'=====================================================================================================================================
			
			
			
			
			'-------------------------------------------------------------------------------'
			'BLOQUE X ORIGINAL:LO COMENTO PARA DEJAR SOLO EL 
			'-------------------------------------------------------------------------------'
			'if(Not IsOffline) Then
			'	dim dsPedido1 as System.Data.Dataset = obSap.Get_ConsultaPedido("",session("ALMACEN"),request.QueryString("codRefer"),"")			
			'	'FlagPrepago = False 'Esto ya estaba comentado " "				
			'	if dsPedido1.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") and dsPedido1.Tables(0).Rows(0).Item("CLASE_VENTA")=ConfigurationSettings.AppSettings("strDTVAlta") then
			'		FlagPrepago = True
			'	end if
			'	dsPedido1 = nothing					
			'else
			'	if dsCompro.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") and dsCompro.Tables(0).Rows(0).Item("CLASE_VENTA")=ConfigurationSettings.AppSettings("strDTVAlta") then
			'		FlagPrepago = True
			'	end if
			'end if
			'-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
			'El BLOQUE X LO REMPLAZARE POR ESTO:
			'Para el tipo la columna sera-> PEDIC_CLASEMENSAJE
			'if dsCompro.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") and dsCompro.Tables(0).Rows(0).Item("CLASE_VENTA")=ConfigurationSettings.AppSettings("strDTVAlta") then	'Como estaba
			'** el campo : CLASE_VENTA --> su equivalente es PEDIC_CLASEMENSAJE???
			if dsCompro.Tables(0).Rows(0).Item("PEDIC_CLASEMENSAJE") = ConfigurationSettings.AppSettings("strTVPrepago") and dsCompro.Tables(0).Rows(0).Item("PEDIC_CLASEMENSAJE")=ConfigurationSettings.AppSettings("strDTVAlta") then	'Como se actualizara 10.11.2014
					FlagPrepago = True
				end if
			'-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
			
			dim i as integer
			If(IsOffline) Then
				if dsCompro.Tables(2).Rows.Count > 0 then
					cad=cad + "<TR><TD colspan=4>FORMAS DE PAGO"
					cad=cad + "</TD></TR>"
					Dim objOffline As New COM_SIC_OffLine.clsOffline
					Dim viaPagoNombre = objOffline.DescripcionViaPagoByCode(dsCompro.Tables(2).Rows(i).Item("Via_Pago").ToString())
					for i=0 to dsCompro.Tables(2).Rows.Count - 1
						cad=cad + "<TR>"
						if (servRec.Substring(0, 7) <> "SERECVI") then
							cad=cad + "<TD WIDTH=50% >" + viaPagoNombre + "<TD WIDTH=20% > </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" + cant_dec(dsCompro.Tables(1).Rows(i).Item("REC_EFECTIVA")) + "</FONT></TD>"
						else
  							cad=cad + "<TD WIDTH=50% >" + viaPagoNombre + "<TD WIDTH=20% > </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" + cant_dec(dsCompro.Tables(1).Rows(i).Item("KWMENG")) + "</FONT></TD>"
						end if
						cad=cad + "</TR>"
					next
				end if
			Else
				'------------------------------------------------------------------------------------
				'PARA LA VERSION 6, ENTRA EN ESTE BLOQUE:
				'Comentare este bloque de còdigo debido a que los datos de la glosa y el total ya lo tengo en 
				'el Nuevo SP para la impresiòn y asi ya no consulto en clspagos, 11.11.14
				'---comento:
				'if not isnothing(dsPagos) then
				'	if dsPagos.Tables(0).Rows.Count > 0 then
				'		'cad=cad + "<TR><TD>&nbsp;</TD></TR>"		''**esto ya estaba comentado***'
				'		cad=cad + "<TR><TD colspan=4>FORMAS DE PAGO"
				'		cad=cad + "</TD></TR>"
				'		for i=0 to dsPagos.Tables(0).Rows.Count-1
				'			cad=cad + "<TR>"							
				'			cad=cad + "<TD WIDTH=50% >" + dsPagos.Tables(0).Rows(i).Item("GLOSA") + "<TD WIDTH=20% > </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" + dsPagos.Tables(0).Rows(i).Item("IMPORTE") + "</FONT></TD>"																	
				'			cad=cad + "</TR>"
				'		next
				'	end if
				'end if
				'-------:fin comento:
				
				'================================================'
				':: Bloque que reemplaza a comento::
				'::EFECTIVO = S/. MONTO
				'================================================'
				if not isnothing(dsCompro) then
					if dsCompro.Tables(3).Rows.Count > 0 then
						'cad=cad + "<TR><TD>&nbsp;</TD></TR>"
						cad=cad + "<TR><TD colspan=4>FORMAS DE PAGO"
						cad=cad + "</TD></TR>"
						for i=0 to dsCompro.Tables(3).Rows.Count-1 
							cad=cad + "<TR>"							
							cad=cad + "<TD WIDTH=50% >" & dsCompro.Tables(3).Rows(i).Item("PAGOV_GLOSA") & " <TD WIDTH=20% ></TD></TD>  <TD WIDTH=30% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" & cant_dec(dsCompro.Tables(3).Rows(i).Item("DEPAN_IMPORTE")) & "</FONT></TD>"																	
							cad=cad + "</TR>"
						next
					end if
				end if
				'------------------------------------------------------------------------------------
			end if
			
			'cad=cad + "<TR><TD>&nbsp;</TD></TR>"
			If(IsOffline) Then
				'cad=cad + "<tr><td colspan=2 class=clsTexto>" + Trim(cstr(dsCompro.tables(2).rows(0)("GLOSA"))) + "</td></tr>"
			else
				'Linea Actua:  cad=cad + "<tr><td colspan=2 class=clsTexto>" + obSap.Get_GlosaCampaña(Trim(cstr(dsCompro.tables(0).rows(0)(4))),Session("ALMACEN")) + "</td></tr>"
				'===SE COMETARA ESTA LINEA, DEBIDO A QUE NO MUESTRA DATOS SIGNIFICATIVOS PARA EL MODELO DE TICKET MOSTRADO
				'cad=cad + "<tr><td colspan=2 class=clsTexto> Y ESTO:" + obSap.Get_GlosaCampaña(Trim(cstr(dsCompro.tables(0).rows(0)(4))),Session("ALMACEN")) + "</td></tr>"
			End If
			
			cad=cad + "</TABLE>"
			cad=cad + "</TD>"
			cad=cad + "</TR>"
						
		end if

		return cad
	End Function
	'fin javier
	
	'Manuel 07/2010
	public Function FncImprImporteVuelto(strEfectivo As String, strRecibido As String, strEntregar As String) as string
		Dim cad	
		
		'================================================================================================'
		'*** Fomàto de tabla: class=clsTexto
		'*** En uso : class=clsVigencia, pero no era uniforme. Utilize el anterior ::: " " :::
		'================================================================================================'
		
		cad = "<TR>"
		cad = cad + "<TD>"
		cad = cad + "<TABLE WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0 class=clsTexto>" 
		cad = cad + "<TR><TD colspan=4>IMPORTE DE VUELTO</TD></TR>"	
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=50% >EFECTIVO:<TD WIDTH=20%> </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" + cant_dec(strEfectivo) + "</FONT></TD>"				
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=50% >RECIBIDO:<TD WIDTH=20%> </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" + cant_dec(strRecibido) + "</FONT></TD>"				
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=50% >A ENTREGAR:<TD WIDTH=20%> </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" + cant_dec(strEntregar) + "</FONT></TD>"				
		cad=cad + "</TR>"	
		cad=cad + "</TABLE>"
		cad=cad + "</TD>"
		cad=cad + "</TR>"					
		
		return cad
	End Function
	
	public Function FncImprRecargaDTH(strCodPago As String, strFechaExpira As String, strNroRecarga As String) as String
		Dim cad	
		
		cad = "<TR>"
		cad = cad + "<TD>"
		cad = cad + "<TABLE WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0 class=clsVigencia>" 
		cad = cad + "<TR>"
		cad = cad + "<TD WIDTH=50% >Código de Pago:<TD WIDTH=20%> </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;<FONT class=clsNumero>" + strCodPago + "</FONT></TD>"				
		cad = cad + "</TR>"
		cad = cad + "<TR>"
		cad = cad + "<TD WIDTH=50% >Fecha de Expiración:<TD WIDTH=20%> </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;<FONT class=clsNumero>" + strFechaExpira + "</FONT></TD>"				
		cad = cad + "</TR>"
		cad = cad + "<TR>"
		cad = cad + "<TD WIDTH=50% >Código Recarga:<TD WIDTH=20%> </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;<FONT class=clsNumero>" + strNroRecarga + "</FONT></TD>"				
		cad = cad + "</TR>"		
		cad = cad + "<TR>"
		cad = cad + "<TD colspan=4 >&nbsp;</TD>"
		cad = cad + "</TR>"		
		cad = cad + "</TABLE>"
		cad = cad + "</TD>"
		cad = cad + "</TR>"					
		
		return cad
	End Function	
	

'==== MENSAJE DE ACEPTACIÒN DE GARANTIA ===='
	public Function FncImprMensajeAcepGarantia() as string
		Dim cad
		cad = "<TR>"
		cad=cad + "<BR/>"
		cad=cad + "<TD align='center'class=clsTexto>" + ConfigurationSettings.AppSettings("mensajeTicketEquipoPrepago") + "</td>"
		cad=cad + "</TR>"
		return cad
	End Function	
'============================================='	
	
'***** lo agregado para el funcionamiento del disclaimer ******'
	'JAZ : 03/10/2014
	public Function FncImprEtiquetaDiscleimer() as string
		Dim cad as string = ""
		
		if(isDiscleimer) then
			cad = "<TR>"
			cad=cad + "<BR/>"
			cad=cad + "<TD align='left'class=clsTexto style='font-size:" + strTamanoLabelDiscleimer +"' >" + strLabelDiscleimer + "</td>"
			cad=cad + "</TR>"
		end if
		return cad
	end function
	
	'JAZ : 02/10/2014
	public Function FncBuscarCadenaInArray(ByVal sCadena As String, ByVal sArray() As String) as Boolean
		Dim bRespuesta as Boolean = false
		for i as integer = 0 to sArray.Length - 1 
			if(sCadena = sArray(i))
				bRespuesta = true
				exit for
			end if
		next		
		return bRespuesta
	End Function	

		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server">
		<div id="divBotones">
					<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="400" align="center">
						<tr>
							<td align="center"><input class="Boton" id="btnImprimir" onmouseover="this.className='BotonResaltado'" onclick="javascript:Imp_ticket();"
									onmouseout="this.className='Boton'" size="15" value="Imprimir" name="btnAgregar" runat="server" width="50">
							</td>
							<td align="center"><input class="Boton" id="btnCancelar" onmouseover="this.className='BotonResaltado'" onclick="window.close();"
									onmouseout="this.className='Boton'" size="15" value="Cerrar" name="btnCancelar" runat="server" width="50">
							</td>
						</tr>
					</table>
				</div>
			<TABLE WIDTH="230" border="0" CELLSPACING="0" CELLPADDING="0">
				<% 
	'********VARIABLES LOCALES
    dim oficinaVenta as string
    dim strTipoOficina as string
    dim fechaVenta as string
    dim docSap as string
    dim msgErr as string=""
    Dim isOffline as Boolean=false
        
    '********FIN VARIABLES LOCALES    
					'******************************************************************************
    Dim dsResult as System.Data.DataSet
    Dim obSap as new SAP_SIC_PAGOS.clsPagos
    Dim objOffline As New COM_SIC_OffLine.clsOffline
	Dim objAct as New COM_SIC_Activaciones.clsConsultaMsSap	'*** 05/10/2014 *******
	'******************************************************************************
    
    strCodUsuario = session("USUARIO")
    strNomUsuario = session("NOMBRE_COMPLETO")
    
    FlagCuotas=false
    NumeroCuotas=0
    
	strTipoOficina = Session("CANAL")
	oficinaVenta = Session("ALMACEN")
	fechaVenta = right("00" & Now.Day, 2) & "/" & right("00" & Now.Month, 2) & "/" & right("0000" & Now.Year, 4) 'now.date.tostring("d") ' Session("FechaAct")					
	
	'*****lee parametros
	docSap = request.QueryString("codRefer")
	Reimpresion = Request.QueryString("Reimpresion")
	
	isOffline= Request.QueryString("isOffline") = "1"
	
	if Request.QueryString("Oficina")<>"" then
	  oficinaVenta = Request.QueryString("Oficina")
	end if
	
	if trim(session("CodImprTicket"))="" then
	   %>
				<script language="javascript">
			 alert('ADVERTENCIA!!! Va a Reimprimir un TICKET via windows asegurese de imprimirlo en una impresora diferente a la ticketera.');
				</script>
				<%
	end if
	
	if(isOffline) then
	'---------------------------------------------------------------'
		dsResult = objOffline.GetConsultaPedido(docSap)
	'---------------------------------------------------------------'
	else
		'1.LLama al ConsultaComprobante se dejara de usar por cambios solicitados y se usara la siguiente
		'dsResult = obSap.Get_ConsultaComprobante(Cstr(docSap),Cstr(oficinaVenta))
		'================================================================================
		'dsResult : es un dataset
		dsResult=objAct.ConsultarComprobante(Cstr(docSap))
		'================================================================================
		
		
		'------------------------------------------------------------------------------------------------
		'------------------------------------------------------------------------------------------------
		'Se comentara para evitar el error: 07/11/2014
		'If dsResult.Tables(5).Rows.Count > 0 Then
		'	Dim drMsg As System.Data.DataRow
		'	For Each drMsg In dsResult.Tables(5).Rows		'---Generara Error con el cambio del SP
		'		If CStr(drMsg("TYPE")) = "E" Then
		'			msgErr = CStr(drMsg("MSG"))
		'		End If
		'	Next
		'else
		'	msgErr = "Error en el de Documento de Impresión " + docSap + " " + oficinaVenta
		'End If
		'------------------------------------------------------------------------------------------------
	end if
	
	 	
	
	if msgErr.trim().length>0 then
		response.write("<script language=jscript> alert('"+msgErr+"'); </script>")	
	end if
	
	'Set objComponente = Server.CreateObject("COM_PVU_Comprobante.SAPComprobante")
	'StrXml = objComponente.Get_ConsultaComprobante(Cstr(docSap),Cstr(oficinaVenta))
	'set objComponente=nothing
	
%>
				<!-- ****************************************************************************************   -->
				<!-- *** IMPRESION DE LOS DOCUMENTOS POR FUNCIONES ***					-->
				<!-- ****************************************************************************************   -->
				<!--Inicio de Cabecera-------------------------------------->
				<%
  'response.write("<script language=jscript> alert('Ini FncImprCabecera'); </script>")	
  Response.Write( FncImprCabecera(dsResult)	)
  'response.write("<script language=jscript> alert('Fin FncImprCabecera'); </script>")	
%>
				<!--Fin de Cabecera----------------------------------------->
				<!--Inicio de Cliente-->
				<%
'response.write("<script language=jscript> alert('Ini FncImprCliente'); </script>")
  Response.Write( FncImprCliente	(dsResult))
'response.write("<script language=jscript> alert('Fin FncImprCliente'); </script>")	
%>
				<!--Fin de Cliente-->
				<!--Inicio de detalle de ventas-->
				<%
'response.write("<script language=jscript> alert('Ini FncImprDetVentas'); </script>")
  Response.Write (FncImprDetVentas(dsResult))
'response.write("<script language=jscript> alert('Fin FncImprDetVentas'); </script>")	
%>
				<!--fin de detalle de ventas-->
				<!--Inicio de Recarga-->
				<%
		'10/11/2014 :: SE CORTA EL FLUJO, VERIFICAR:: 
		'Aqui se cae la aplicaciòn cuando el pedido no tiene detalles, entonces no asigna nada en al SERVREC
		'dim x as integer = servRec.length 
'response.write("<script language=jscript> alert('Ini FncImprRecargas'); </script>")

		if servRec.length >0 then 
  if Request.QueryString("Reimpresion") = "" Or servRec.Substring(0, 7) = "SERECVI" then
	Response.Write( FncImprRecargas(dsResult))
  end if
		end if 
		
  
  ' Impresion Datos Recarga Virtual DTH
	if Request.QueryString("strCodPago") <> "" then
		'Dim fechaFormat = 
		Response.Write(FncImprRecargaDTH(Request.QueryString("strCodPago"),Request.QueryString("strFechaExpira"),Request.QueryString("strNroRecarga")))
	end if
	'response.write("<script language=jscript> alert('Fin FncImprRecargas'); </script>")
%>
				<!--Fin de Recarga-->
				<!--Inicio de Cuotas-->
				<%
'-----------------------------------------------------
' 25/11/2011: No debe Imprimirse las Cuotas
'-----------------------------------------------------
  'Response.Write( FncImpCuotas(dsResult))
%>
				<!--Fin de Cuotas-->
				<!--Inicio Totales-->
				<%
				'response.write("<script language=jscript> alert('"+servRec+"'); </script>")
		if servRec.length >0 then 
  if (servRec.Substring(0, 7) <> "SERECVI") then
  	Response.Write (FncImprTotales(dsResult))
  end if
		end if 
%>
				<!--Fin Totales-->
				<!--Inicio Formas de Pago-->
				<%				
    	Response.Write (FncImprFormaPago(dsResult))  
%>
				<!--Fin Formas de Pago-->
				<!--Inicio de Totales Ruc-->
				<!--Fin de Totales Ruc-->
				<!--Inicio de Totales Normal-->
				<!--<TR>
<TD>
<TABLE WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0>
<TR>
<TD WIDTH=65% class=clsTexto>VALOR VENTA</TD>
<TD WITDH=35% ALIGN=RIGHT class=clsTexto>S/ <FONT class=clsNumero>10023.00</FONT></TD>
</TR>
<TR>
<TD WIDTH=65% class=clsTexto>
AJUSTE REDONDEO</TD>
<TD WITDH=35% ALIGN=RIGHT class=clsTexto>S/ <FONT class=clsNumero>11130.32</FONT></TD>
</TR>
<TR>
<TD WIDTH=65% class=clsTexto>TOTAL</TD>
<TD WITDH=35% ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>22223.32</FONT></TD>
</TR>
</TABLE>

</TD>
</TR>-->
				<!--Fin de Totales Normal-->
				<!--Inicio de Glosa Prepago-->
				<%
		if servRec.length >0 then 
  if (servRec.Substring(0, 7) <> "SERECVI") then
  	Response.Write (FncPiePrepago())
  end if  
		end if 
%>
				<!--Fin de Glosa Prepago-->
				<!--Inicio de Cuotas-->
				<%
		if servRec.length >0 then 
				if(not servRec.StartsWith("SERECVI")) then
				'==== Lo cometare hasta que me indiquen, de que forma identifico el pago por cuotas ===='
				'11/11/2014 - 
				'Response.Write (FncPieCuotas(dsResult))		'Linea actual
			end if
  end if
%>
				<!--Fin de Cuotas-->
				<!--Inicio Comentarios-->
				<%
  Response.Write (FncImprVendedores())
%>
				<!--Fin Comentarios-->
				<!--Inicio Importe de vuelto-->
				<%
		'============================================================================================================================================================
		'=== IMPORTE DEVUELTO ==='
		'============================================================================================================================================================
		'La variable strEfectivo tiene que ser diferente de vacio para que pueda entrar
		'en este bloque se imprime la parte del vuelto:
				if trim(Reimpresion)="" and Request.QueryString("strEfectivo") <> "" then
					Response.Write (FncImprImporteVuelto(Request.QueryString("strEfectivo"), Request.QueryString("strRecibido"), Request.QueryString("strEntregar") ))
				end if
		'============================================================================================================================================================
				%>
				<!--Fin Importe de vuelto-->
				<!--Inicio Mensaje de Aceptacion de Condiciones de garantia (solo prepago)-->
				<%
					If Request.QueryString("flagVentaEquipoPrepago")="1" then
						Response.Write (FncImprMensajeAcepGarantia())
					End If  
				%>
				<!--Fin Mensaje de Aceptacion de Condiciones de garantia (solo prepago)-->
				<!--Inicio Etiqueta de Discleimer-->
				<%
					Response.Write (FncImprEtiquetaDiscleimer())
				%>
				<!--Fin Etiqueta de Discleimer-->
			</TABLE>
		</form>
	</body>
</HTML>
