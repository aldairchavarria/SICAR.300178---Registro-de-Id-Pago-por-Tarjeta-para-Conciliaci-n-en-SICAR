<%@ Page Language="vb" Codepage="1252" aspcompat="true"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>OperacionesImp_Meta</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
	<script runat=server> 
	'*************************************
	'**** POR DEFINIR
	'************************************
    Dim intNumeroCuotas
    dim strNbsp        
    dim recargaefectiva,valorventa,descuento, igv, totalpago, numrecarga
	'**********************PARAMETROS DE LA EMPRESA *****************
   	DIM gStrRazonSocial AS string = System.Configuration.ConfigurationSettings.AppSettings("gStrRazonSocial")
    DIM gStrMarca AS string = System.Configuration.ConfigurationSettings.AppSettings("gStrMarca")
    DIM gStrRUC AS string = System.Configuration.ConfigurationSettings.AppSettings("gStrRUC")
    '**********************PARAMETROS DE LA EMPRESA *****************
   DIM cteMSG_GLOSA_VENTA_CUOTAS AS string = System.Configuration.ConfigurationSettings.AppSettings("cteMSG_GLOSA_VENTA_CUOTAS")
   '******************
   
	Dim objComponente
	Dim objRecordSet, objRecordSetA, objRecordSetB, objRecordSetC, objRecordSetD, objRecordSetE
	Dim strTipoOficina, oficinaVenta, fechaVenta, docSap
	Dim sValorA, sValorB, msgErr, total, i, ajuste, ttotal, cant,StrXml
    Dim tipo,desc,recarga, scFecha, sdist, sdept, sprov, sfechan, strTemp
    Dim CuentaFilas,CuentaFilasNped
    Dim CuentaLineaVenc
    Dim ContFor
    dim objRecordSetRV
    Dim cadRV
    Dim DocFactSunat
    '***************************
    Dim dsCompro as System.Data.DataSet
    Dim obSap as new SAP_SIC_PAGOS.clsPagos
    '***************************
	Dim StrRutaFisicaSite,Fso,TextStream
	
	Dim dtIGV As System.Data.DataTable	
	
	Dim IGVactual As string 
	IGVactual = "0"
	
	Dim Const_IGV as String 
	Const_IGV = "0"
	
	'*** PMO - CUOTAS EN VENTA RAPIDA - INICIO ***
	'*** FECHA: 23/11/2005 - AUTOR: NLCH	   
	public Function GetTramaFinalCuotasDescDocumento(byval strNumeroCuotas ) as string
		Dim strCadena
		strCadena = Replace(cteMSG_GLOSA_VENTA_CUOTAS, "#", strNumeroCuotas)
		GetTramaFinalCuotasDescDocumento = strCadena
	end function
    '*** PMO - CUOTAS EN VENTA RAPIDA - FIN ***
	
	public Function Dist_Desc(byval cadena1 as string, byval cadena2 as string) as string
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

	public Sub CrearArchivo(byval archivo as string, byval HTML as string,byval carpeta as string)	
		Fso = Server.CreateObject("Scripting.FileSystemObject")
		TextStream = Fso.CreateTextFile(StrRutaFisicaSite & "\" & carpeta & "\" & archivo)
		TextStream.Write(HTML)
		TextStream.Close
	End Sub



	</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server">
<%

dtIGV = Session("Lista_Impuesto")
if dtIGV.rows.count = 0 then
	%>
		<script language="javascript">
				alert('No existe IGV configurado');
		</script>
	<% Response.end
	end if
	
	Dim dtFechaIGV as string = Convert.ToDateTime(Session("FechaAct").ToString).ToString("dd/MM/yyyy")
	For Each row As System.Data.DataRow In dtIGV.Rows
		If (CDate(dtFechaIGV) >= CDate(row("impudFecIniVigencia").ToString.Trim) And CDate(dtFechaIGV) <= CDate(row("impudFecFinVigencia").ToString.Trim) And CInt(row("impunTipDoc").ToString.Trim) = 0) Then
			IGVactual = Math.Round(CDec(row("IGV").ToString.Trim()) / 100, 2).ToString
			Const_IGV = Cint((CDec(IGVactual)*100)).ToString
			Exit For
		End If    
	Next
	 
strTipoOficina = Session("CANAL")
oficinaVenta = Session("ALMACEN")
fechaVenta = Session("FechaAct")

docSap = request.QueryString("codRefer")
DocFactSunat=Request.QueryString("FactSunat") 

msgErr=""
'SET objComponente= CreateObjectComprobante(Session("ALMACEN"))

dsCompro = obSap.Get_ConsultaComprobante(Cstr(docSap),Cstr(oficinaVenta))

If dsCompro.Tables(5).Rows.Count > 0 Then
	Dim drMsg As System.Data.DataRow
	For Each drMsg In dsCompro.Tables(5).Rows
		If CStr(drMsg("TYPE")) = "E" Then
			msgErr = CStr(drMsg("MSG"))
		End If
	Next
else
	msgErr = "Error en el de Documento de Impresión " + docSap + " " + oficinaVenta
End If	
if msgErr.trim().length>0 then
	response.write("<script language=jscript> alert('"+msgErr+"'); </script>")	
end if

Dim StrHtml as string = ""
StrHtml = StrHtml & "<html>"
StrHtml = StrHtml & "<head>"
StrHtml = StrHtml & "<title>Reporte de Impresión</title>"
StrHtml = StrHtml & "</head>"
StrHtml = StrHtml & "<SCRIPT ID=clientEventHandlersVBS LANGUAGE=vbscript>" & vbCr 
StrHtml = StrHtml & "<!-- "& vbCr 
StrHtml = StrHtml & "Sub window_onload "& vbCr 
StrHtml = StrHtml & "	  window.close "& vbCr 
StrHtml = StrHtml & "End Sub "& vbCr 
StrHtml = StrHtml & "--> "& vbCr 
StrHtml = StrHtml & "</SCRIPT>"& vbCr 
StrHtml = StrHtml & "<body>"

'<!-- Cabecera BOLETA MUNDOS-->
if dsCompro.Tables(0).Rows.Count > 0 then
	'consulta para recargas virtuales
	cadRV="0"
			
	'*****Recarga Virtual****
	if dsCompro.Tables(4).Rows.Count > 0 then
		if dsCompro.Tables(4).Rows(0)(4) >0 then
			recargaefectiva= dsCompro.Tables(4).Rows(0)(4)
			valorventa= dsCompro.Tables(4).Rows(0)(5)
			descuento= dsCompro.Tables(4).Rows(0)(6)
			igv= dsCompro.Tables(4).Rows(0)(7)
			totalpago= dsCompro.Tables(4).Rows(0)(8)
			numrecarga= dsCompro.Tables(4).Rows(0)(9)
			cadRV="1"
		end if
	end if
	
	tipo = trim(cstr(dsCompro.Tables(0).Rows(0)(8)))
	if (len(tipo) > 0) then 
		'Las Notas de Devolucion se muestran como Nota de Pedido
		if tipo="ZNDV" then
			tipo="NPED"
		end if 
		if tipo="N/C" then
			tipo="FAC"
		end if 
		
		if(tipo="ZDAU") then  
			recarga = Trim(cstr(dsCompro.Tables(5).Rows(0)(9)))
			StrHtml = StrHtml & "<div ID='' STYLE='position:absolute; left:50; top:80;'  class=clsCourier10>"
			StrHtml = StrHtml & "<table border='0' bordercolor='#000000' width='650'>"
			StrHtml = StrHtml & "	<tr class=clsCourier10B>"
			StrHtml = StrHtml & "		<td align='center' colspan='3'>DOCUMENTO AUTORIZADO</td>"
			StrHtml = StrHtml & "	</tr>"
			StrHtml = StrHtml & "	<tr><td colspan='3'>&nbsp;</td></tr>"
			StrHtml = StrHtml & "	<tr><td colspan='3'>_____________________________________________________________________________________________</td></tr>"
			StrHtml = StrHtml & "	<tr class=clsCourier10>"
			StrHtml = StrHtml & "		<td colspan='2'>Operación:</td>"
			StrHtml = StrHtml & "		<td align='right'>" & cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(6,2)&"/"&cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(4,2) &"/"&cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(0,4)& "</td>"
			StrHtml = StrHtml & "	</tr> "
			StrHtml = StrHtml & "	<tr><td colspan='3'>_____________________________________________________________________________________________</td></tr>"
			StrHtml = StrHtml & "	<tr><td colspan='3'>&nbsp;</td></tr>"
			StrHtml = StrHtml & "	<tr class=clsCourier10B>"
			StrHtml = StrHtml & "		<td colspan='2'>" & gStrRazonSocial & "</td>"
			StrHtml = StrHtml & "		<td align='right'> RUC:" & gStrRUC & "</td>"
			StrHtml = StrHtml & "	</tr>"
			StrHtml = StrHtml & "	<tr>"
			StrHtml = StrHtml & "		<td class=clsCourier10B>DOCUMENTO AUTORIZADO</td>"
			StrHtml = StrHtml & "		<td class=clsCourier10>:</td>"
			StrHtml = StrHtml & "		<td class=clsCourier10>" & mid(Trim(cstr(dsCompro.Tables(0).Rows(0)(5))),2)  & "</td>"
			StrHtml = StrHtml & "	</tr>"
			StrHtml = StrHtml & "	<tr>"
			StrHtml = StrHtml & "		<td class=clsCourier10B>RAZON SOCIAL/NOMBRE</td>"
			StrHtml = StrHtml & "		<td class=clsCourier10>:</td>"
			StrHtml = StrHtml & "		<td class=clsCourier10>" & Trim(cstr(dsCompro.Tables(0).Rows(0)(0)))  & "</td>"
			StrHtml = StrHtml & "	</tr>"
			StrHtml = StrHtml & "	<tr>"
			StrHtml = StrHtml & "		<td class=clsCourier10B>RUC/DNI</td>"
			StrHtml = StrHtml & "		<td class=clsCourier10>:</td>"
			StrHtml = StrHtml & "		<td class=clsCourier10>" & Trim(cstr(dsCompro.Tables(0).Rows(0)(2))) & "</td>"
			StrHtml = StrHtml & "	</tr>"
			StrHtml = StrHtml & "</table>"
			StrHtml = StrHtml & "</div>"
		else
			if(tipo="NPED") then 
				StrHtml = StrHtml & "<TABLE WIDTH=700 BORDER=0 cellspacing=0>"
				StrHtml = StrHtml & "	<TR HEIGHT=42><!---MARGEN SUPERIOR-->   "
				StrHtml = StrHtml & "		<TD WIDTH='2%'>&nbsp;</TD><!---MARGEN IZQUIERDO-->"
				StrHtml = StrHtml & "		<TD >&nbsp;</TD>"
				StrHtml = StrHtml & "	</TR>"
				StrHtml = StrHtml & "	<TR>   "
				StrHtml = StrHtml & "		<TD>&nbsp;</TD>"
				StrHtml = StrHtml & "		<TD WIDTH='93%'>"
				StrHtml = StrHtml & "		    <TABLE BORDER=0 CELLSPACING=0 WIDTH=690>"
				StrHtml = StrHtml & "		    </TABLE>"

				StrHtml = StrHtml & "		        <TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 WIDTH=689>"
				StrHtml = StrHtml & "		        <TR>"
				StrHtml = StrHtml & "		           <TD WIDTH='80%'>&nbsp;</TD>"
				'StrHtml = StrHtml & "		           <TD WIDTH='19%' align=right><FONT  face='Arial' size=1>&nbsp;" & DocFactSunat  & "</FONT></TD>"
				if trim(DocFactSunat)="" then
					DocFactSunat=replace(Trim(cstr(dsCompro.Tables(0).Rows(0)(5))),"*","")
				end if 
				StrHtml = StrHtml & "		           <TD WIDTH='19%' align=right><FONT  face='Arial' size=1>&nbsp;" & DocFactSunat  & "</FONT></TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='1%'>&nbsp;</TD>"
				StrHtml = StrHtml & "		        </TR>"
				StrHtml = StrHtml & "		        <TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 WIDTH=689>"
				StrHtml = StrHtml & "		        <TR>"
				' StrHtml = StrHtml & "		           <TD WIDTH='89%'>&nbsp;</TD>"  ' CAP: Linea original.Se agrego razon social
				StrHtml = StrHtml & "		           <TD WIDTH='49%'><FONT  face='Arial' size=1>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & gStrRazonSocial & "</FONT></TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='40%'><FONT  face='Arial' size=1>Nro. Ref Cadena: " & Trim(cstr(dsCompro.Tables(0).Rows(0)(21))) & "&nbsp;" & Trim(cstr(dsCompro.Tables(5).Rows(0)(20))) & "</FONT></TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='10%'><FONT  face='Arial' size=1>&nbsp;" & now.tostring("d")  & "</FONT></TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='1%'>&nbsp;</TD>"
				StrHtml = StrHtml & "		        </TR>"

				StrHtml = StrHtml & "		        </TABLE>"
				StrHtml = StrHtml & "		        <TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 WIDTH=689>"
				
				StrHtml = StrHtml & "		        <TR>"
				StrHtml = StrHtml & "		           <TD WIDTH='10%'>&nbsp;</TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='23%'><FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(7))) & "</FONT></TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='8%'>&nbsp;</TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='19%' ALIGN=middle><FONT  face='Arial' size=1>&nbsp;" & strTipoOficina & "</FONT></TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='10%'>&nbsp;</TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='12%'>&nbsp;<FONT  face='Arial' size=1>" & oficinaVenta & "</FONT></TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='5%'>&nbsp;</TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='13%'><FONT  face='Arial' size=1>&nbsp;" & cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(6,2)&"/"&cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(4,2) &"/"&cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(0,4) & "</FONT></TD>"
				StrHtml = StrHtml & "		        </TR>"
				StrHtml = StrHtml & "		        </TABLE>"

				StrHtml = StrHtml & "		        <TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 WIDTH=689>"
				StrHtml = StrHtml & "		        <TR>"
				StrHtml = StrHtml & "		           <TD WIDTH='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='21%'><FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(10))) & "</FONT></TD>"

					
				strTemp = Trim(cstr(dsCompro.Tables(0).Rows(0)(11)))
						
				if (len(strTemp)>21) then strTemp = Left(strTemp,21)
						
				StrHtml = StrHtml & "		           <TD WIDTH='6%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='21%'><FONT  face='Arial' size=1>&nbsp;" & strTemp & "</FONT></TD>"
					
				if (len(Trim(cstr(dsCompro.Tables(0).Rows(0)(1))))<35) then
					StrHtml = StrHtml & "		           <TD WIDTH='8%' ALIGN=middle><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
					StrHtml = StrHtml & "		           <TD WIDTH='37%'>&nbsp;<FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(1))) & "</FONT>     </TD>"
					StrHtml = StrHtml & "		        </TR>"
					StrHtml = StrHtml & "		        </TABLE>"
					StrHtml = StrHtml & "		        <TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 WIDTH=689>"
					StrHtml = StrHtml & "		        <TR>"
					StrHtml = StrHtml & "		           <TD WIDTH='34%'><FONT  face='Arial' size=1>&nbsp;" & Mid(Trim(cstr(dsCompro.Tables(0).Rows(0)(1))),35) & "</FONT></TD>"
											
					'''StrHtml = StrHtml & "							<div ID='' STYLE='position:absolute; left:500; top:72;' class=clsCourier10><b>" & Trim(objRecordSetA.Fields(1).Value) & "</b></div>"
				else
					StrHtml = StrHtml & "		           <TD WIDTH='8%' ALIGN=middle><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
					StrHtml = StrHtml & "		           <TD WIDTH='37%'>&nbsp;<FONT  face='Arial' size=1>&nbsp;" & Left(Trim(cstr(dsCompro.Tables(0).Rows(0)(1))),34) & "_" & "</FONT>     </TD>"
					StrHtml = StrHtml & "		        </TR>"
					StrHtml = StrHtml & "		        </TABLE>"
					StrHtml = StrHtml & "		        <TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 WIDTH=689>"
					StrHtml = StrHtml & "		        <TR>"
					StrHtml = StrHtml & "		           <TD WIDTH='34%'><FONT  face='Arial' size=1>&nbsp;" & Mid(Trim(cstr(dsCompro.Tables(0).Rows(0)(1) )),35) & "</FONT></TD>"
											
					'''StrHtml = StrHtml & "							<div ID='' STYLE='position:absolute; left:500; top:72;' class=clsCourier10>" & Left(Trim(objRecordSetA.Fields(1).Value),34) & "_" & "</div>"
					'''StrHtml = StrHtml & "							<div ID='' STYLE='position:absolute; left:70; top:90;' class=clsCourier10>" & Mid(Trim(objRecordSetA.Fields(1).Value),35) & "</div>"
				end if

				sdist = Trim(cstr(dsCompro.Tables(0).Rows(0)(15)))
				if (sdist<>"") then
					sdept = Mid(sdist,3,3)
					sprov = Mid(sdist,6)
				else
					sdept = ""
					sprov = ""
				end if

				StrHtml = StrHtml & "		           <TD WIDTH='6%'><FONT  face='Arial' size=1> &nbsp;</FONT></TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='30%' ALIGN=middle><FONT  face='Arial' size=1>&nbsp;" & Dist_Desc(sdept,sprov) & "</FONT></TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='12%'>&nbsp;<FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
													
				sfechan = Trim(cstr(dsCompro.Tables(0).Rows(0)(12)))
				if (sfechan="12:00:00 a.m." or sfechan="00000000") then sfechan = ""
				StrHtml = StrHtml & "		           <TD WIDTH='18%'>&nbsp;<FONT  face='Arial' size=1>&nbsp;" &  sfechan  & "</FONT></TD>"
				StrHtml = StrHtml & "		        </TR>"
				StrHtml = StrHtml & "		        </TABLE>"
				StrHtml = StrHtml & "		        <TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 WIDTH=689>"
				StrHtml = StrHtml & "		        <TR>"
				StrHtml = StrHtml & "		           <TD WIDTH='15%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='13%'>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(13))) & "</TD>"
				
				
				select case Trim(cstr(dsCompro.Tables(0).Rows(0)(14)))
					case "1"
						StrHtml = StrHtml & "		           <TD WIDTH='17%'><FONT  face='Arial' size=1> &nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='5%'><FONT  face='Arial' size=1>&nbsp;X</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='5%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='9%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='3%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
												
						'''StrHtml = StrHtml & "							<div ID='' STYLE='position:absolute; left:390; top:110;' class=clsCourier10>X</div>"
					
					case "2"
												
						StrHtml = StrHtml & "		           <TD WIDTH='17%'><FONT  face='Arial' size=1> &nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='5%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='5%'><FONT  face='Arial' size=1>&nbsp;X</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='9%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='3%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						'StrHtml = StrHtml & "							<div ID='' STYLE='position:absolute; left:422; top:110;' class=clsCourier10>X</div>"
												
					case "3"
						StrHtml = StrHtml & "		           <TD WIDTH='17%'><FONT  face='Arial' size=1> &nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='5%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='5%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='4%'><FONT  face='Arial' size=1>&nbsp;X</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='9%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='3%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
									
					case "4"
						StrHtml = StrHtml & "		           <TD WIDTH='17%'><FONT  face='Arial' size=1> &nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='5%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='5%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='4%'><FONT  face='Arial' size=1>&nbsp;X</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='9%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='3%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
												
						'StrHtml = StrHtml & "							<div ID='' STYLE='position:absolute; left:486; top:110;' class=clsCourier10>X</div>"
												
					case "6"
						StrHtml = StrHtml & "		           <TD WIDTH='17%'><FONT  face='Arial' size=1> &nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='5%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='5%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='9%'><FONT  face='Arial' size=1>&nbsp;X</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='3%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
												
						'StrHtml = StrHtml & "							<div ID='' STYLE='position:absolute; left:518; top:110;' class=clsCourier10>X</div>"
												
					case "7"
						StrHtml = StrHtml & "		           <TD WIDTH='17%'><FONT  face='Arial' size=1> &nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='5%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='5%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='9%'><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='3%'><FONT  face='Arial' size=1>&nbsp;X</FONT></TD>"
												
						'StrHtml = StrHtml & "							<div ID='' STYLE='position:absolute; left:578; top:110;' class=clsCourier10>X</div>"
												
				end select
				'StrHtml = StrHtml & "		           <TD WIDTH='22%' align=center><FONT  face='Arial' size=2>&nbsp;" &  Trim(objRecordSetA.Fields(2).Value) & "</FONT></TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='22%' align=center>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(2))) & "</TD>"
				StrHtml = StrHtml & "		           <TD WIDTH='1%'><FONT  face='Arial' size=2><b>&nbsp;</FONT></TD>"
				StrHtml = StrHtml & "		        </TR>"
				StrHtml = StrHtml & "		        </TABLE>"
										
						'StrHtml = StrHtml & "						<div ID='' STYLE='position:absolute; left:640; top:110;' class=clsCourier10>" &  Trim(objRecordSetA.Fields(2).Value) & "</div>"
%>
<!--
						<div ID="" STYLE="position:absolute; left:145; top:65;" class=clsCourier10><%=Trim(objRecordSetA.Fields(6).Value)%></div>
						<div ID="" STYLE="position:absolute; left:300; top:65;" class=clsCourier10><%=strTipoOficina%></div>
						<div ID="" STYLE="position:absolute; left:535; top:65;" class=clsCourier10><%=oficinaVenta%></div>
						<div ID="" STYLE="position:absolute; left:660; top:20;" class=clsCourier10><%= now.tostring("d") %></div>
						<div ID="" STYLE="position:absolute; left:660; top:40;" class=clsCourier10><%= now.tostring("t") %></div>
						<div ID="" STYLE="position:absolute; left:660; top:65;" class=clsCourier10><%= Trim(objRecordSetA.Fields(3).Value) %></div>
						<div ID="" STYLE="position:absolute; left:130; top:90;" class=clsCourier10><%= Trim(objRecordSetA.Fields(0).Value) %></div>
						<div ID="" STYLE="position:absolute; left:600; top:90;" class=clsCourier10><%= Trim(objRecordSetA.Fields(2).Value) %></div>
						<div ID="" STYLE="position:absolute; left:100; top:110;" class=clsCourier10><%= Trim(objRecordSetA.Fields(1).Value) %></div>
//-->
<%			else
				IF	trim(tipo) = "ZBOL" THEN	
					'<!--	DATOS PARA LA IMPRESION DE LA BOLETA	-->
					StrHtml = StrHtml & "<table   WIDTH=660 BORDER=0 cellspacing=0>" & vbCr 
					StrHtml = StrHtml & "	<tr>"& vbCr 'Margen Superior
					'	StrHtml = StrHtml & "	 <td height=120><FONT  face='Arial' size=1 >&nbsp;</FONT></td>" & vbCr ' CAP: Linea original. Se redujo altura
					StrHtml = StrHtml & "	 <td height=100><FONT  face='Arial' size=1 >&nbsp;</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	</tr>" & vbCr
					StrHtml = StrHtml & "	<tr>"& vbCr 'CAP : Razon Social
					StrHtml = StrHtml & "	    <td height=18 width='16%'><FONT  face='Arial' size=1 >&nbsp;</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	   	<td width='57%'><FONT  face='Arial' size=1>&nbsp;" & gStrRazonSocial & "</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	   	<td width='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	   	<td width='23%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	</tr>" & vbCr  
					StrHtml = StrHtml & "	<tr  >" & vbCr 
					StrHtml = StrHtml & "	   	<td height=18 width='16%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	   	<td width='57%'><FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(0)))  & "</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	   	<td width='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	   	<td width='23%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	</tr>" & vbCr 
					StrHtml = StrHtml & "	<tr >" & vbCr 
					StrHtml = StrHtml & "	   	<td height=22 width='16%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	   	<td width='57%'><FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(1))) & "</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	   	<td width='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	   	<td width='23%'><FONT  face='Arial'>&nbsp;" &  mid(Trim(cstr(dsCompro.Tables(0).Rows(0)(5))),2) & "</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	</tr>" & vbCr 
					StrHtml = StrHtml & " </table>" & vbCr 
					StrHtml = StrHtml & "  <TABLE  WIDTH=660 BORDER=0 CELLSPACING=0 >" & vbCr 
					StrHtml = StrHtml & "	<tr cellspacing='0' height=50>" & vbCr 
					StrHtml = StrHtml & "	<td width='18%'>&nbsp;</td>" & vbCr 
					StrHtml = StrHtml & "	<td cellspacing='0' width='28%'>" & vbCr 
					StrHtml = StrHtml & "			<TABLE  cellspacing=4 cellpadding=0 width=60 BORDER=0>" & vbCr 
					StrHtml = StrHtml & "			        <TR>" & vbCr 
					StrHtml = StrHtml & "			          <TD><FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(4))) & "</FONT></TD>" & vbCr 
					StrHtml = StrHtml & "			        </TR>" & vbCr 
					StrHtml = StrHtml & "			        <TR>" & vbCr 
					StrHtml = StrHtml & "			          <TD><FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(2))) & "</FONT></TD>" & vbCr 
					StrHtml = StrHtml & "			        </TR>" & vbCr 
					StrHtml = StrHtml & "			</TABLE>" & vbCr 
					StrHtml = StrHtml & "	</td>" & vbCr 
					StrHtml = StrHtml & "	<td width='12%'><FONT  face='Arial' size=1>&nbsp;" & cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(6,2)&"/"&cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(4,2) &"/"&cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(0,4)& "</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	<td width='10%'><FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(6))) & "</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	<td width='46%'><FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(7))) & "</FONT></td>" & vbCr 
					StrHtml = StrHtml & "    </tr>" & vbCr 
					StrHtml = StrHtml & " </TABLE>" & vbCr 

				ELSE
					'<!--	DATOS PARA LA IMPRESION DE LA FACTURA	-->

					IF TRIM(tipo)="B/V" OR TRIM(tipo)="FAC" THEN

						IF TRIM(tipo)="FAC" THEN
							StrHtml = StrHtml & "<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=707>"
							'StrHtml = StrHtml & "	<TR HEIGHT=87><!--MARGEN SUPERIOR-->"
							StrHtml = StrHtml & "	<TR HEIGHT=85><!--MARGEN SUPERIOR-->"
							StrHtml = StrHtml & "		<TD WIDTH='2%'>"'MARGEN IZQUIERDO
							StrHtml = StrHtml & "			 &nbsp;"
							StrHtml = StrHtml & "		</TD>"
							StrHtml = StrHtml & "		<TD WIDTH='98%'>"
							StrHtml = StrHtml & "			 &nbsp;"
							StrHtml = StrHtml & "		</TD>"
							StrHtml = StrHtml & "	</TR>"
						END IF 
						IF TRIM(tipo)="B/V" THEN
							StrHtml = StrHtml & "<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=721>"
							StrHtml = StrHtml & "	<TR HEIGHT=80><!--MARGEN SUPERIOR-->"
							StrHtml = StrHtml & "		<TD WIDTH='4%'>"'MARGEN IZQUIERDO
							StrHtml = StrHtml & "			 &nbsp;"
							StrHtml = StrHtml & "		</TD>"
							StrHtml = StrHtml & "		<TD WIDTH='96%'>"
							StrHtml = StrHtml & "			 &nbsp;"
							StrHtml = StrHtml & "		</TD>"
							StrHtml = StrHtml & "	</TR>"
						END IF 

						StrHtml = StrHtml & "	<TR>"
						StrHtml = StrHtml & "		<TD>"
						StrHtml = StrHtml & "			 &nbsp;"
						StrHtml = StrHtml & "		</TD>"
						StrHtml = StrHtml & "		<TD>"
						StrHtml = StrHtml & "			<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0>"
						StrHtml = StrHtml & "			      <TR>"
									          
						StrHtml = StrHtml & "			          <TD WIDTH='100%'>"
						StrHtml = StrHtml & "			                <TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0>"
						StrHtml = StrHtml & "			                <TR>"
						StrHtml = StrHtml & "			                <TD WIDTH='80%'>"
						StrHtml = StrHtml & " 									<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0>"

						StrHtml = StrHtml & "									<TR>"
						StrHtml = StrHtml & "									   <TD WIDTH='15%' height=8>"
						StrHtml = StrHtml & "									       &nbsp;"
						StrHtml = StrHtml & "									   </TD>   "
						StrHtml = StrHtml & "									   <TD WIDTH='85%'>"
						'StrHtml = StrHtml & "									       &nbsp;"  'CAP : Linea original. Se incluye Razon Social
						StrHtml = StrHtml & "									       &nbsp;<FONT face='Arial' size=1>" & gStrRazonSocial & "</FONT>"
						StrHtml = StrHtml & "									   </TD>"
						StrHtml = StrHtml & "									</TR>"

						StrHtml = StrHtml & "									<TR>"
						StrHtml = StrHtml & "									   <TD WIDTH='15%'>"
						StrHtml = StrHtml & "									       &nbsp;"
						StrHtml = StrHtml & "									   </TD>   "
						StrHtml = StrHtml & "									   <TD WIDTH='85%'>"'SEÑOR(ES)
						StrHtml = StrHtml & "									       &nbsp;<FONT face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(0)))  & "</FONT>"
						StrHtml = StrHtml & "									   </TD>"
						StrHtml = StrHtml & "									</TR>"
						StrHtml = StrHtml & "									<TR>"
						StrHtml = StrHtml & "									   <TD WIDTH='15%'>"
						StrHtml = StrHtml & "									       &nbsp;"
						StrHtml = StrHtml & "									   </TD>   "
						StrHtml = StrHtml & "									   <TD WIDTH='85%'>"'DIRECCION
						StrHtml = StrHtml & "									       &nbsp;<FONT face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(1))) & "</FONT>"
						StrHtml = StrHtml & "									   </TD>"
						StrHtml = StrHtml & "									</TR>"
						StrHtml = StrHtml & "									</TABLE>"
						StrHtml = StrHtml & "							</TD>"
						StrHtml = StrHtml & "							<TD WIDTH='20%'>"

						IF TRIM(tipo)="FAC" THEN
							' Fecha: 20040810, Se agrega línea para que el número de Factura se visualice correctamente:
							StrHtml = StrHtml & "							        <TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0>"
							StrHtml = StrHtml & "							        <TR>"
							StrHtml = StrHtml & "									<TD WIDTH='100%'>"'<!--Numero del documento-->
							StrHtml = StrHtml & "									    <FONT face='Arial' size=2>&nbsp;</FONT>"
							StrHtml = StrHtml & "									</TD>   "
							StrHtml = StrHtml & "									</TR>"
							StrHtml = StrHtml & "							        <TR>"
							StrHtml = StrHtml & "									<TD WIDTH='100%' VALIGN=CENTER>"'<!--Numero del documento-->
							StrHtml = StrHtml & "									    <FONT face='Arial' size=2>&nbsp;<BR>" &  mid(Trim(cstr(dsCompro.Tables(0).Rows(0)(5))),2) & "</FONT>"
							StrHtml = StrHtml & "									</TD>   "
							StrHtml = StrHtml & "									</TR>"
							StrHtml = StrHtml & "							        </TABLE>"
						END IF 
						IF TRIM(tipo)="B/V" THEN
							StrHtml = StrHtml & "							        <TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0>"
							StrHtml = StrHtml & "							        <TR>"
							StrHtml = StrHtml & "									<TD WIDTH='100%'>"'<!--Numero del documento-->
							StrHtml = StrHtml & "									    <FONT face='Arial' size=2>&nbsp;</FONT>"
							StrHtml = StrHtml & "									</TD>   "
							StrHtml = StrHtml & "									</TR>"
							StrHtml = StrHtml & "							        <TR>"
							StrHtml = StrHtml & "									<TD WIDTH='100%'>"'<!--Numero del documento-->
							StrHtml = StrHtml & "									    <FONT face='Arial' size=2>&nbsp;<BR>" &  mid(Trim(cstr(dsCompro.Tables(0).Rows(0)(5))),2) & "</FONT>"
							StrHtml = StrHtml & "									</TD>   "
							StrHtml = StrHtml & "									</TR>"
							StrHtml = StrHtml & "							        </TABLE>"
						END IF 
						StrHtml = StrHtml & "							</TD>"
						StrHtml = StrHtml & "							</TR>"
						StrHtml = StrHtml & "							</TABLE>"
						StrHtml = StrHtml & "			          </TD>"
						StrHtml = StrHtml & "			      </TR>"
			    			    
						StrHtml = StrHtml & "			    <TR><TD WIDTH='100%'>"
						StrHtml = StrHtml & "			       <TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0>"
						StrHtml = StrHtml & "			       <TR>"
						StrHtml = StrHtml & "			           <TD WIDTH='12%'>"
						StrHtml = StrHtml & "			              &nbsp;"
						StrHtml = StrHtml & "			           </TD>"
						if TRIM(tipo)="FAC" THEN
							StrHtml = StrHtml & "			           <TD WIDTH='22%'>"'RUC
							StrHtml = StrHtml & "			              &nbsp;<FONT face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(2))) & "</FONT>"
							StrHtml = StrHtml & "			           </TD>"
						ELSE
							StrHtml = StrHtml & "			           <TD WIDTH='22%'>"'DNI --NO SE MUESTRA--
							StrHtml = StrHtml & "			              &nbsp;<FONT face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(2))) & "</FONT>"
							StrHtml = StrHtml & "			              &nbsp;<FONT face='Arial' size=1>&nbsp;</FONT>"
							StrHtml = StrHtml & "			           </TD>"
						END IF 

						StrHtml = StrHtml & "					   <TD WIDTH='66%'>"'FECHA
						StrHtml = StrHtml & "			              &nbsp;<FONT face='Arial' size=1>&nbsp;" & cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(6,2)&"/"&cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(4,2) &"/"&cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(0,4)& "</FONT>"
						StrHtml = StrHtml & "			           </TD>			           "
						StrHtml = StrHtml & "			       </TR>"
						StrHtml = StrHtml & "			       </TABLE>"
						StrHtml = StrHtml & "			    </TD></TR>					"

					ELSE
						StrHtml = StrHtml & "<table   WIDTH=660 BORDER=0 cellspacing=0>" & vbCr 
						StrHtml = StrHtml & "	<tr>"& vbCr
						'StrHtml = StrHtml & "	 <td height=120><FONT  face='Arial' size=1 ></FONT></td>" & vbCr 'CAP: Linea original. Se incluyo Razon Social
						StrHtml = StrHtml & "	 <td height=100><FONT  face='Arial' size=1 ></FONT></td>" & vbCr  
						StrHtml = StrHtml & "	</tr>" & vbCr 

						'CAP : Bloque Razon Social
						StrHtml = StrHtml & "	<tr  >" & vbCr 
						StrHtml = StrHtml & "	   	<td height=18 width='16%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	   	<td width='57%'><FONT  face='Arial' size=1>&nbsp;" & Trim(gStrRazonSocial) & "</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	   	<td width='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	   	<td width='23%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	</tr>" & vbCr 
						'CAP : Fin de bloque

						StrHtml = StrHtml & "	<tr  >" & vbCr 
						StrHtml = StrHtml & "	   	<td height=18 width='16%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	   	<td width='57%'><FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(0)))  & "</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	   	<td width='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	   	<td width='23%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	</tr>" & vbCr 
						StrHtml = StrHtml & "	<tr >" & vbCr 
						StrHtml = StrHtml & "	   	<td height=22 width='16%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	   	<td width='57%'><FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(1))) & "</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	   	<td width='4%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	   	<td width='23%'><FONT  face='Arial'>&nbsp;" &  mid(Trim(cstr(dsCompro.Tables(0).Rows(0)(5))),2) & "</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	</tr>" & vbCr 
						StrHtml = StrHtml & " </table>" & vbCr 
						StrHtml = StrHtml & "  <TABLE  WIDTH=660 BORDER=0 CELLSPACING=0 >" & vbCr 
						StrHtml = StrHtml & "	<tr cellspacing='0' height=50>" & vbCr 
						StrHtml = StrHtml & "	<td width='18%'>&nbsp;</td>" & vbCr 
						StrHtml = StrHtml & "	<td cellspacing='0' width='28%'>" & vbCr 
						StrHtml = StrHtml & "			<TABLE  cellspacing=4 cellpadding=0 width=60 BORDER=0>" & vbCr 
						StrHtml = StrHtml & "			        <TR>" & vbCr 
						StrHtml = StrHtml & "			          <TD><FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(4))) & "</FONT></TD>" & vbCr 
						StrHtml = StrHtml & "			        </TR>" & vbCr 
						StrHtml = StrHtml & "			        <TR>" & vbCr 
						StrHtml = StrHtml & "			          <TD><FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(2))) & "</FONT></TD>" & vbCr 
						StrHtml = StrHtml & "			        </TR>" & vbCr 
						StrHtml = StrHtml & "			</TABLE>" & vbCr 
						StrHtml = StrHtml & "	</td>" & vbCr 
						StrHtml = StrHtml & "	<td width='12%'><FONT  face='Arial' size=1>&nbsp;" & cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(6,2)&"/"&cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(4,2) &"/"&cstr(dsCompro.Tables(0).Rows(0)(3)).SubString(0,4)& "</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='10%'><FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(6))) & "</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='46%'><FONT  face='Arial' size=1>&nbsp;" & Trim(cstr(dsCompro.Tables(0).Rows(0)(7))) & "</FONT></td>" & vbCr 
						StrHtml = StrHtml & "    </tr>" & vbCr 
						StrHtml = StrHtml & " </TABLE>" & vbCr 
					END IF 'IF TRIM(tipo)="B/V" OR TRIM(tipo)="FAC" THEN
				END IF 'IF	trim(tipo) = "ZBOL" THEN	
			end if 'if(tipo="NPED") then 
		end if 'if(tipo="ZDAU") then  
	end if  'if (len(tipo) > 0) then 
	
	'	end if
	'end if


	if (len(tipo) > 0) then 	%>

<!-- Detalle -->
<%	
		IF (tipo="ZDAU") THEN   
			StrHtml = StrHtml & "<div ID='' STYLE='position:absolute; left:200; top:300;'>"
			StrHtml = StrHtml & "		<table border='0' bordercolor='#000000' width='400'>"

			if dsCompro.Tables(2).Rows.Count > 0 then					
				desc = negativo(dsCompro.Tables(2).Rows(0)(10))
							
				StrHtml = StrHtml & "						<tr>"
				StrHtml = StrHtml & "							<td class=clsCourier10B width='200'>VENTA DE RECARGA TIM</td>"
				StrHtml = StrHtml & "							<td class=clsCourier10 width='50'>:</td>"
				StrHtml = StrHtml & "							<td class=clsCourier10 width='150' align='right'>" & cant_dec((cdbl(replace(objRecordSetB.Fields(9).Value,".",","))) + (cdbl(replace(dsCompro.Tables(2).Rows(0)(11),".",",")))) & "</td>"
				StrHtml = StrHtml & "						</tr>"
				StrHtml = StrHtml & "						<tr>"
				StrHtml = StrHtml & "							<td class=clsCourier10B>RECARGA EFECTIVA</td>"
				StrHtml = StrHtml & "							<td class=clsCourier10>:</td>"
				StrHtml = StrHtml & "							<td class=clsCourier10 align='right'>" & recarga & "</td>"
				StrHtml = StrHtml & "						</tr>"
   			end if 				
			StrHtml = StrHtml & "		</table>"
			StrHtml = StrHtml & "</div>"
		ELSE	 'if(tipo="ZDAU") then
			if(tipo = "NPED") then 
				StrHtml = StrHtml & "		        <TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 WIDTH=689>"
				StrHtml = StrHtml & "		        <TR HEIGHT=60><!--SEPARADOR-->"
				StrHtml = StrHtml & "		           <TD WIDTH='15%' ><FONT  face='Arial' size=1></FONT></TD>"
				StrHtml = StrHtml & "		        </TR>"
				StrHtml = StrHtml & "		        </TABLE>"
			
				'''StrHtml = StrHtml & "		<div ID='' STYLE='position:absolute; left:30; top:190;'>"
				'''StrHtml = StrHtml & "		<table border='0' bordercolor='#000000' width='650'>"
				dim Cant1
				dim Cant2
				''Set objRecordSetB = objComponente.Get_RS02()
				'Set objRecordSetB = XmlToRecordset(StrXml,"RS")
				dim dr as system.data.datarow	
				CuentaFilasNped=0
				if dsCompro.Tables(2).Rows.Count > 0 then
						
					for each dr in dsCompro.Tables(2).Rows
						CuentaFilasNped=CuentaFilasNped+1
						StrHtml = StrHtml & "		        <TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 WIDTH=689>"
						StrHtml = StrHtml & "		        <TR HEIGHT=20>"
						StrHtml = StrHtml & "		           <TD WIDTH='37%' ><FONT  face='Arial' size=1>&nbsp;" & dr(4) & "</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='36%' ><FONT  face='Arial' size=1>&nbsp;" & dr(12) & "</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='12%' ><FONT  face='Arial' size=1>&nbsp;" & dr(1) & "</FONT></TD>"
												
							'''StrHtml = StrHtml & "					<tr class=clsCourier10 height=18>"
							'''StrHtml = StrHtml & "						<td>" & objRecordSetB.Fields(4).Value & "</td>"
							'''StrHtml = StrHtml & "						<td>" & objRecordSetB.Fields(12).Value & "</td>"
							'''StrHtml = StrHtml & "						<td>" & objRecordSetB.Fields(1).Value & "</td>"
						
						Cant1 = dr(9)
						Cant2 = dr(11)
						'total =  (cdbl(replace(objRecordSetB.Fields(9).Value,".",","))) + (cdbl(replace(objRecordSetB.Fields(11).Value,".",","))) 
						total = (cdbl(Cant1)) + cdbl((Cant2)) 
							'''StrHtml = StrHtml & "						<td align=right>" & cant_dec(total) & "</td>"
							'''StrHtml = StrHtml & "					</tr>"
						StrHtml = StrHtml & "		           <TD WIDTH='10%' ALIGN=right><FONT  face='Arial' size=1>&nbsp;" & cant_dec(total) & "</FONT></TD>"
						StrHtml = StrHtml & "		           <TD WIDTH='10%' ALIGN=right><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
						StrHtml = StrHtml & "		        </TR>"
						StrHtml = StrHtml & "		        </TABLE>"
					next
				end if 
			ELSE	
				IF	trim(tipo) = "ZBOL" THEN	
				'	<!--	DATOS PARA LA IMPRESION DE LA BOLETA	-->
					StrHtml = StrHtml & " <TABLE WIDTH=710 BORDER=0 CELLSPACING=0>" & vbCr 
					StrHtml = StrHtml & "	<tr cellspacing='0'><td>&nbsp;</td></tr>" & vbCr 
					StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
					StrHtml = StrHtml & "	<td width='9%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	<td width='11%' align=center><FONT  face='Arial' size=1>&nbsp;CODIGO</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	<td width='5%'><FONT  face='Arial' size=1>&nbsp;CANT</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	<td width='3%'><FONT  face='Arial' size=1>&nbsp;UN</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	<td width='25%' align=center><FONT  face='Arial' size=1>&nbsp;DESCRIPCION</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	<td width='16%' align=right ><FONT  face='Arial' size=1>&nbsp;PRECIO UNITARIO</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	<td width='11%' align=right ><FONT  face='Arial' size=1>&nbsp;DESCUENTO</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	<td width='11%' align=right ><FONT  face='Arial' size=1>&nbsp;PRECIO NETO</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	<td width='8%' align=right><FONT  face='Arial' size=1>&nbsp;TOTAL</FONT></td>" & vbCr 
					StrHtml = StrHtml & "	<td width='1%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
					StrHtml = StrHtml & "    </tr>    " & vbCr 
					StrHtml = StrHtml & "    <tr><td></td></tr>" & vbCr 
					StrHtml = StrHtml & "</table>  " & vbCr 
				ELSE
					'<!--	DATOS PARA LA IMPRESION DE LA FACTURA	-->
					IF TRIM(TIPO)="B/V" OR TRIM(TIPO)="FAC" THEN
						'NO EXISTE CABECERA PARA LOS DATOS DEL DETALLE
						StrHtml = StrHtml & "			    <TR><TD WIDTH='100%'>"'<!--SEPARACION-->
						StrHtml = StrHtml & "			       <TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0>"
						StrHtml = StrHtml & "			       <TR>"
						StrHtml = StrHtml & "			           <TD WIDTH='100%'>"
						StrHtml = StrHtml & "			              &nbsp;<FONT face='Arial' size=1></FONT>"
						StrHtml = StrHtml & "			           </TD>"
						StrHtml = StrHtml & "			       </TR>"
						StrHtml = StrHtml & "			       </TABLE>"
						StrHtml = StrHtml & "			    </TD></TR>"
					ELSE
						StrHtml = StrHtml & " <TABLE WIDTH=710 BORDER=0 CELLSPACING=0>" & vbCr 
						StrHtml = StrHtml & "	<tr cellspacing='0'><td>&nbsp;</td></tr>" & vbCr 
						StrHtml = StrHtml & "	<tr cellspacing='0'><td>&nbsp;</td></tr>" & vbCr 
						StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
						StrHtml = StrHtml & "	<td width='9%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='11%' align=center><FONT  face='Arial' size=1>&nbsp;CODIGO</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='5%'><FONT  face='Arial' size=1>&nbsp;CANT</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='3%'><FONT  face='Arial' size=1>&nbsp;UN</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='25%' align=center><FONT  face='Arial' size=1>&nbsp;DESCRIPCION</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='16%' align=right ><FONT  face='Arial' size=1>&nbsp;PRECIO UNITARIO</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='11%' align=right ><FONT  face='Arial' size=1>&nbsp;DESCUENTO</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='11%' align=right ><FONT  face='Arial' size=1>&nbsp;PRECIO NETO</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='8%' align=right><FONT  face='Arial' size=1>&nbsp;TOTAL</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='1%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "    </tr>    " & vbCr 
						StrHtml = StrHtml & "    <tr><td></td></tr>" & vbCr 
						StrHtml = StrHtml & "</table>  " & vbCr 
					END IF 
				END IF
				ttotal = 0
				''Set objRecordSetB = objComponente.Get_RS02()
				CuentaFilas=0
				'Set objRecordSetB = XmlToRecordset(StrXml,"RS")
				'Set objRecordSetB = XmlToRecordset(StrXml,"RS02")  
			
				if dsCompro.Tables(2).Rows.Count > 0 then
					dim dr as system.data.datarow
					for each dr in dsCompro.Tables(2).Rows
						CuentaFilas=CuentaFilas+1
						if(tipo="ZFAC" or tipo="ZNCV" or tipo="ZNDV") then
 								'total =  ((cdbl(objRecordSetB.Fields(11).Value)) - (cdbl(objRecordSetB.Fields(10).Value)))	
 							total = cdbl(dr(11))
						else
								'total =  ((cdbl(objRecordSetB.Fields(9).Value)) - (cdbl(objRecordSetB.Fields(10).Value)) + (cdbl(objRecordSetB.Fields(11).Value)))	
 							total = cdbl(dr(11))
						end if
						ttotal = ttotal + total 
						IF TRIM(tipo)="B/V" OR TRIM(tipo)="FAC" THEN'DETALLE DE LOS PRODUCTOS
							CuentaFilas=CuentaFilas+1
							StrHtml = StrHtml & "			    <TR><TD WIDTH='100%'>"
							StrHtml = StrHtml & "			       <TABLE BORDER=0 CELLPADDING=1 CELLSPACING=2>"
							StrHtml = StrHtml & "			       <TR>"
							StrHtml = StrHtml & "			           <TD WIDTH='89%'>"
							StrHtml = StrHtml & "							<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0>"

							if no_dec(dr(2))>0 then
								StrHtml = StrHtml & "							<TR>"
								StrHtml = StrHtml & "									<TD WIDTH=15% ALIGN=CENTER>"'<!--CODIGO-->
								StrHtml = StrHtml & "									   &nbsp;<FONT face='Arial' size=1>&nbsp;" & dr(1) & "</FONT>"
								StrHtml = StrHtml & "									</TD>"
								StrHtml = StrHtml & "									<TD WIDTH=7% ALIGN=RIGHT>"'<!--CANT-->
								StrHtml = StrHtml & "									   <FONT face='Arial' size=1>&nbsp;" & no_dec(dr(2)) & "</FONT>"
								StrHtml = StrHtml & "									</TD>"
								StrHtml = StrHtml & "									<TD WIDTH='70%'>"'<!--DESCRIPCION-->
								StrHtml = StrHtml & "									   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<FONT face='Arial' size=1>&nbsp;" & dr(4) & "</FONT>"
								StrHtml = StrHtml & "									</TD>"
								StrHtml = StrHtml & "									<TD WIDTH='17%'>"'<!--PRECIO UNITARIO-->
								StrHtml = StrHtml & "									   <FONT face='Arial' size=1>&nbsp;</FONT>"
								StrHtml = StrHtml & "									</TD>"
								StrHtml = StrHtml & "							</TR>"
							else
								CuentaFilas=CuentaFilas-3
								StrHtml = StrHtml & "							<TR>"
								StrHtml = StrHtml & "									<TD WIDTH=15% ALIGN=CENTER>"'<!--CODIGO-->
								StrHtml = StrHtml & "									   <FONT face='Arial' size=1></FONT>"
								StrHtml = StrHtml & "									</TD>"
								StrHtml = StrHtml & "									<TD WIDTH=8% ALIGN=RIGHT>"'<!--CANT-->
								StrHtml = StrHtml & "									   <FONT face='Arial' size=1></FONT>"
								StrHtml = StrHtml & "									</TD>"
								StrHtml = StrHtml & "									<TD WIDTH='69%'>"'<!--DESCRIPCION-->
								StrHtml = StrHtml & "									   <FONT face='Arial' size=1></FONT>"
								StrHtml = StrHtml & "									</TD>"
								StrHtml = StrHtml & "									<TD WIDTH='17%'>"'<!--PRECIO UNITARIO-->
								StrHtml = StrHtml & "									   <FONT face='Arial' size=1></FONT>"
								StrHtml = StrHtml & "									</TD>"
								StrHtml = StrHtml & "							</TR>"
							end if 

							if (Len(Trim(cstr(dr(12))))) > 0 then 
								CuentaFilas=CuentaFilas+1
								'Modificacion de las lineas si son demasiado grandes
								Dim CuentaLineaSeries,SeriesCont
								Dim TamLinea
								if len(trim(cstr(dr(12))))>70 then
									TamLinea=60
								else
									TamLinea=70
								end if 

								CuentaLineaSeries=len(trim(cstr(dr(12)))) \ TamLinea
								if len(trim(cstr(dr(12))))>TamLinea*CuentaLineaSeries then
									CuentaLineaSeries=CuentaLineaSeries+1
								end if 
								if CuentaLineaSeries<=0 then
									CuentaLineaSeries=1
								end if 
								'CuentaFilas=CuentaFilas+CuentaLineaSeries
								'Bucle para poner lineas demasiado grandes
								for SeriesCont=1 to CuentaLineaSeries
									CuentaFilas=CuentaFilas+1
									StrHtml = StrHtml & "							<TR>"
									StrHtml = StrHtml & "									<TD WIDTH='15%'>"'<!--CODIGO-->
									StrHtml = StrHtml & "									   &nbsp;"
									StrHtml = StrHtml & "									</TD>"
									StrHtml = StrHtml & "									<TD WIDTH='7%'>"'<!--CANT-->
									StrHtml = StrHtml & "									   &nbsp;"
									StrHtml = StrHtml & "									</TD>"
									StrHtml = StrHtml & "									<TD WIDTH='61%'>"'<!--DESCRIPCION-->
									StrHtml = StrHtml & "									   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<FONT face='Arial' size=1>&nbsp;" & mid(trim(dr(12)),(SeriesCont-1)*TamLinea+1,TamLinea) & "</font>"
									'StrHtml = StrHtml & "									   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<FONT face='Arial' size=1>&nbsp;" & trim(objRecordSetB.Fields(12).Value) & "</font>"
									StrHtml = StrHtml & "									</TD>"
									StrHtml = StrHtml & "									<TD WIDTH='17%'>"'<!--PRECIO UNITARIO-->
									StrHtml = StrHtml & "									   &nbsp;<FONT face='Arial' size=1>&nbsp;</FONT>"
									StrHtml = StrHtml & "									</TD>"
									StrHtml = StrHtml & "							</TR>"
								next
							end if 

							StrHtml = StrHtml & "							</TABLE>"
							StrHtml = StrHtml & "			           </TD>"
							' fecha: 20040810, cambio alineación de moneda a efectos que salga con mejor alineamiento:
							StrHtml = StrHtml & "					   <TD WIDTH=3% VALIGN=TOP ALIGN=right>"'<!--PRECIO VENTA-->
							StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;&nbsp;&nbsp;</FONT>"'SOLES
							StrHtml = StrHtml & "			           </TD>			           "

							'En los casos de venta masiva cuando la cantidad 
							'es cero no se muestra el precio
							if no_dec(cstr(dsCompro.Tables(2).Rows(0)(2)))>0 then
								StrHtml = StrHtml & "					   <TD WIDTH=6% VALIGN=TOP ALIGN=RIGHT>"'<!--PRECIO VENTA-->
								'IF TRIM(TIPO)="FAC" THEN
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>S/&nbsp;"  & dr(11) & "</FONT>"
								'ELSE
								'StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;"  & FORMATNUMBER(CSTR(((cdbl(objRecordSetB.Fields(9).Value)) + (cdbl(objRecordSetB.Fields(11).Value)))),2)	 & "</FONT>"
								'END IF 
								StrHtml = StrHtml & "			           </TD>			           "
							else
								StrHtml = StrHtml & "					   <TD WIDTH=6% VALIGN=TOP ALIGN=RIGHT>"'<!--PRECIO VENTA-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1></FONT>"
								StrHtml = StrHtml & "			           </TD>			           "
							end if

							StrHtml = StrHtml & "			       </TR>"
										       
							StrHtml = StrHtml & "			       </TABLE>"
							StrHtml = StrHtml & "			    </TD></TR>"
						ELSE
							StrHtml = StrHtml & " <TABLE WIDTH=710 BORDER=0 CELLSPACING=0>" & vbCr 
							StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
							StrHtml = StrHtml & "	<td width='9%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='11%'><FONT  face='Arial' size=1>&nbsp;" & cstr(dr(1)) & "</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='5%' align=right><FONT  face='Arial' size=1>&nbsp;" & no_dec(cstr(dr(2))) & "</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='3%'><FONT  face='Arial' size=1>&nbsp;" & cstr(dr(3)) & "</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='31%'><FONT  face='Arial' size=1>&nbsp;" & cstr(dr(4)) & "</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial' size=1>&nbsp;" & cstr(dr(5)) & "</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size=1>&nbsp;" & cstr(dr(6)) & "</FONT></td>" & vbCr 
							'StrHtml = StrHtml & "					<td align='right'><b>" & objRecordSetB.Fields(8).Value & "</b></td>"
							'StrHtml = StrHtml & "					<td align='right'>" & objRecordSetB.Fields(11).Value & "</td>"
							StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size=1>&nbsp;" & cstr(dr(8)) & "</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='8%' align=right><FONT  face='Arial' size=1>&nbsp;" & cstr(dr(11)) & "</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='1%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "    </tr>" & vbCr 
							StrHtml = StrHtml & " </TABLE> " & vbCr 
						END IF 
						IF not (TRIM(tipo)="B/V" OR TRIM(tipo)="FAC") THEN
								
							StrHtml = StrHtml & "				</tr>"
							if (Len(Trim(cstr(dr(12))))) > 0 then 
								'StrHtml = StrHtml & "				<tr class=clsCourier10>"
								'StrHtml = StrHtml & "					<td colspan='5'>&nbsp;&nbsp;" & objRecordSetB.Fields(12).Value & "</td>"
								'StrHtml = StrHtml & "				</tr>"
								StrHtml = StrHtml & "				<TABLE WIDTH=780 BORDER=0 CELLSPACING=1>" & vbCr 
								StrHtml = StrHtml & "					<tr cellspacing='0'>" & vbCr 
								StrHtml = StrHtml & "					<td width='8%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "					<td width='92%'><FONT  face='Arial' size=1>&nbsp;" & trim(cstr(dr(12))) & "</FONT></td>" & vbCr 
								StrHtml = StrHtml & "				   </tr>" & vbCr 
								StrHtml = StrHtml & "				</TABLE>" & vbCr 
							end if
						END IF				
					next		
				end if 
				'<!-- Cuotas -->
				intNumeroCuotas = 0
				if cadRV = "1" then
					dim rv					
					%><!-- #include file="IncludeRVirtuallmMeta.aspx" -->
					<%
					cadRV = "0"
				end if

				IF	trim(tipo) = "ZBOL" THEN	
					'<!--	DATOS PARA LA IMPRESION DE LA BOLETA	-->
					'StrHtml = StrHtml & "				<div ID='' STYLE='position:absolute; left:50; top:465;'> "
					'StrHtml = StrHtml & "					<table border='0' bordercolor='#000000' width='250'>"
					StrHtml = StrHtml & " <TABLE WIDTH=710 BORDER=0 CELLSPACING=0>"
									''Set objRecordSetC = objComponente.Get_RS03()
					StrHtml = StrHtml & "						<tr class=clsCourier10><td>&nbsp;&nbsp;</td></tr>"
					StrHtml = StrHtml & "						<tr class=clsCourier10><td>&nbsp;&nbsp;</td></tr>"
					StrHtml = StrHtml & "						<tr class=clsCourier10><td>&nbsp;&nbsp;</td></tr>"
					
						'Set objRecordSetC = XmlToRecordset(StrXml,"RS")
						'Set objRecordSetC = XmlToRecordset(StrXml,"RS03")  
						'if not objRecordSetC is nothing then
					CuentaLineaVenc=0	
					if dsCompro.Tables(1).Rows.Count > 0 then 						
						StrHtml = StrHtml & "	<tr cellspacing='0'>"
						StrHtml = StrHtml & "	<td width='5%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>"
						StrHtml = StrHtml & "	<td width='95%'><FONT  face='Arial' size=1>&nbsp;" & dsCompro.Tables(1).Rows(0)(0) & "</FONT></td>"
						StrHtml = StrHtml & "	</tr>"
						dim dr as System.data.datarow
						for each dr in dsCompro.Tables(1).Rows
										'Do While (Not objRecordSetC.BOF AND Not objRecordSetC.EOF)      
							intNumeroCuotas = intNumeroCuotas + 1
							CuentaLineaVenc=CuentaLineaVenc+1
							'StrHtml = StrHtml & "								<tr class=clsCourier10>"
							'StrHtml = StrHtml & "									<td><b>" & objRecordSetC.Fields(1).Value & "</b></td>"
							StrHtml = StrHtml & "	<tr cellspacing='0'>"
							StrHtml = StrHtml & "	<td width='15%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>"
							StrHtml = StrHtml & "	<td width='30%'><FONT  face='Arial' size=1>&nbsp;" & dr(1) & "</FONT></td>"
															
							scFecha = dr(2)
							if (Trim(scFecha)="12:00:00 a.m.") or Trim(scFecha)="00000000" then scFecha = ""
											
		
							StrHtml = StrHtml & "								<td width='15%'><FONT  face='Arial' size=1>&nbsp;" & valida_dato(scFecha) & "</FONT></td>"
							StrHtml = StrHtml & "								<td width='15%'><FONT  face='Arial' size=1>&nbsp;" & dr(3) & "</FONT></td>"
							StrHtml = StrHtml & "								<td width='25%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>"
							StrHtml = StrHtml & "	</tr>"

						next
					else     
						CuentaLineaVenc=-1					
						StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
						StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
						StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
						StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
						StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
						StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
						StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
						StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
						StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
						StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
						StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
						StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
						StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
						StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"					
					end if					
					StrHtml = StrHtml & "   <table>"
				ELSE						
					IF not(TRIM(tipo)="B/V" OR TRIM(tipo)="FAC") THEN
		
						'StrHtml = StrHtml & "				<div ID='' STYLE='position:absolute; left:50; top:440;'> "
						'StrHtml = StrHtml & "					<table border='0' bordercolor='#000000' width='250'>"
						StrHtml = StrHtml & " <TABLE WIDTH=710 BORDER=0 CELLSPACING=0>"
										''Set objRecordSetC = objComponente.Get_RS03()
						StrHtml = StrHtml & "						<tr class=clsCourier10><td>&nbsp;&nbsp;</td></tr>"
						StrHtml = StrHtml & "						<tr class=clsCourier10><td>&nbsp;&nbsp;</td></tr>"
						StrHtml = StrHtml & "						<tr class=clsCourier10><td>&nbsp;&nbsp;</td></tr>"

						CuentaLineaVenc=-1
						'Set objRecordSetC = XmlToRecordset(StrXml,"RS")
						'Set objRecordSetC = XmlToRecordset(StrXml,"RS03")  
						IF dsCompro.Tables(1).Rows.Count > 0 then
							CuentaLineaVenc=0
							'StrHtml = StrHtml & "								<tr class=clsCourier10>"
							'StrHtml = StrHtml & "									<td>" & objRecordSetC.Fields(0).Value & "</td>"
							'StrHtml = StrHtml & "								</tr>"
							StrHtml = StrHtml & "	<tr cellspacing='0'>"
							StrHtml = StrHtml & "	<td width='5%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>"
							StrHtml = StrHtml & "	<td width='95%'><FONT  face='Arial' size=1>&nbsp;" & 	dsCompro.Tables(1).Rows(0)(0) & "</FONT></td>"
							StrHtml = StrHtml & "	</tr>"
							dim rs as system.data.datarow
							for each rs in dsCompro.Tables(1).Rows
								intNumeroCuotas = intNumeroCuotas + 1
								CuentaLineaVenc=CuentaLineaVenc+1
		
								StrHtml = StrHtml & "	<tr cellspacing='0'>"
								StrHtml = StrHtml & "	<td width='15%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>"
								StrHtml = StrHtml & "	<td width='30%'><FONT  face='Arial' size=1>&nbsp;" & rs(1) & "</FONT></td>"
																	
								scFecha = rs(2)
								if (Trim(scFecha)="12:00:00 a.m.") or scFecha = "00000000" then scFecha = ""
											
		
								StrHtml = StrHtml & "								<td width='15%'><FONT  face='Arial' size=1>&nbsp;" & valida_dato(scFecha) & "</FONT></td>"
								StrHtml = StrHtml & "								<td width='15%'><FONT  face='Arial' size=1>&nbsp;" & rs(3) & "</FONT></td>"
								StrHtml = StrHtml & "								<td width='25%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>"
								StrHtml = StrHtml & "	</tr>"

							next
						else     
							CuentaLineaVenc=-1					
							StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
							StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
							StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
							StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
							StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
							StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
							StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
							StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
							StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
							StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
							StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
							StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
							StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
							StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
						
						end if
						
					END IF 'FIN FAC B/V
				END IF
			end if 
		end if  'IF (tipo="ZDAU") THEN    
	end if   '---- if (len(tipo) > 0) then
	'<!--Pie -->
	
	
		if(tipo="ZDAU") then
			StrHtml = StrHtml & "  <div ID='' STYLE='position:absolute; left:240; top:340;'>"
			StrHtml = StrHtml & "		<table border='0' bordercolor='#000000' width='400'>"

			''Set objRecordSetD = objComponente.Get_RS04()
			'Set objRecordSetD = XmlToRecordset(StrXml,"RS")
			if dsCompro.Tables(3).Rows.Count > 0 then
				StrHtml = StrHtml & "				<tr>"
				StrHtml = StrHtml & "					<td class=clsCourier10B width='200'>Valor de Venta</td>"
				StrHtml = StrHtml & "					<td class=clsCourier10 width='50'>:</td>"
				StrHtml = StrHtml & "					<td class=clsCourier10 width='150' align='right'>" & format(dsCompro.Tables(3).Rows(0)(2)) & "</td>"
				StrHtml = StrHtml & "				</tr>"
				StrHtml = StrHtml & "				<tr>"
				StrHtml = StrHtml & "					<td class=clsCourier10B>Descuento</td>"
				StrHtml = StrHtml & "					<td class=clsCourier10>:</td>"
				StrHtml = StrHtml & "					<td class=clsCourier10 align='right'>" & desc & "</td>"
				StrHtml = StrHtml & "				</tr>"
				StrHtml = StrHtml & "				<tr>"
				StrHtml = StrHtml & "					<td class=clsCourier10B>I.G.V. (" & trim(dsCompro.Tables(3).Rows(0)(4)) & "%)</td>"
				StrHtml = StrHtml & "					<td class=clsCourier10>:</td>"
				StrHtml = StrHtml & "					<td class=clsCourier10 align='right'>" & format(dsCompro.Tables(3).Rows(0)(3)) & "</td>"
				StrHtml = StrHtml & "				</tr>"
				StrHtml = StrHtml & "				<tr>"
				StrHtml = StrHtml & "					<td class=clsCourier10B>Ajuste Redondeo</td>"
				StrHtml = StrHtml & "					<td class=clsCourier10>:</td>"
				StrHtml = StrHtml & "					<td class=clsCourier10 align='right'>" & negativo(dsCompro.Tables(3).Rows(0)(5)) & "</td>"
				StrHtml = StrHtml & "				</tr>"
				StrHtml = StrHtml & "				<tr>"
				StrHtml = StrHtml & "					<td class=clsCourier10B>Total</td>"
				StrHtml = StrHtml & "					<td class=clsCourier10>:</td>"
				StrHtml = StrHtml & "					<td class=clsCourier10 align='right'>" & format(dsCompro.Tables(3).Rows(0)(6)) & "</td>"
				StrHtml = StrHtml & "				</tr>"
				StrHtml = StrHtml & "				<tr><td colspan='3'>&nbsp;</td></tr>"
				StrHtml = StrHtml & "				<tr class=clsCourier10>"
				StrHtml = StrHtml & "					<td colspan='3'>SON : " & dsCompro.Tables(3).Rows(0)(1) & "</td>"
				StrHtml = StrHtml & "				</tr>"
				StrHtml = StrHtml & "				<tr><td colspan='3'>&nbsp;</td></tr>"
				StrHtml = StrHtml & "				<tr class=clsCourier10B>"
				StrHtml = StrHtml & "					<td colspan='3'>Vigencia de la recarga : 180 días</td>"
				StrHtml = StrHtml & "				</tr>"
				StrHtml = StrHtml & "				<tr class=clsCourier10B>"
				StrHtml = StrHtml & "					<td colspan='3'>Para Consultas o reclamos llamar al 151</td>"
				StrHtml = StrHtml & "				</tr>"
				StrHtml = StrHtml & "				<tr><td colspan='3'>&nbsp;</td></tr>"
				StrHtml = StrHtml & "		</table>"
				StrHtml = StrHtml & "		</div>"
				StrHtml = StrHtml & "		<div ID='' STYLE='position:absolute; left:50; top:530;'>"
				StrHtml = StrHtml & "		<table border='0' bordercolor='#000000' width='650'>"
				StrHtml = StrHtml & "				<tr><td colspan='3'>_______________________________________________________________________________________</td></tr>"
				StrHtml = StrHtml & "				<tr class=clsCourier10>"
				StrHtml = StrHtml & "					<td align='center' colspan='3'>Documento autorizado para fines tributarios e impuesto a la renta e IGV.</td>"
				StrHtml = StrHtml & "				</tr>"
				StrHtml = StrHtml & "				<tr class=clsCourier10>"
				StrHtml = StrHtml & "					<td align='center' colspan='3'>(Base legal: art.4 - Reglamento de comprobantes de pago)</td>"
				StrHtml = StrHtml & "				</tr>"
				StrHtml = StrHtml & "				<tr><td colspan='3'>_______________________________________________________________________________________</td></tr>		</table>"
			

				StrHtml = StrHtml & "	</div>"
				StrHtml = StrHtml & "	<div ID='' STYLE='position:absolute; left:10; top:600;'> "
				StrHtml = StrHtml & "		<table border='0' bordercolor='#000000' width='650'> "
				StrHtml = StrHtml & "			<tr class=clsArial18>"
				StrHtml = StrHtml & "				<td align='right'>" & dsCompro.Tables(3).Rows(0)(7)  & "</td>"
				StrHtml = StrHtml & "			</tr>"
				StrHtml = StrHtml & "		</table>"
				StrHtml = StrHtml & "	</div>"
			end if
		else
			if(tipo="NPED") then 			
				if dsCompro.Tables(3).Rows.Count > 0 then
					Dim ConForNped
					StrHtml = StrHtml & "		        <TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 WIDTH=689>"
					if cadRV = "1" then
							%><!-- #include file="IncludeRVirtuallmMeta.aspx" -->
							<%
							cadRV = "0"
					else
						for ConForNped=0 to 10-CuentaFilasNped
							StrHtml = StrHtml & "		           <TR><TD WIDTH='15%' ><FONT  face='Arial' size=1>&nbsp;</FONT></TD></TR>"
						next
					end if
					StrHtml = StrHtml & "		        </TABLE>"

					StrHtml = StrHtml & "		        <TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 WIDTH=689>"
					StrHtml = StrHtml & "		        <TR HEIGHT=20>"
					'StrHtml = StrHtml & "		           <TD WIDTH='80%' ><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"  'CAP : Linea original. Se coloco marca.Remover despues
					StrHtml = StrHtml & "		           <TD WIDTH='80%' ><FONT  face='Arial' size=1>&nbsp;" & gStrMarca &" </FONT></TD>"
					StrHtml = StrHtml & "		           <TD WIDTH='10%' ALIGN=right>&nbsp;" & format(dsCompro.Tables(3).Rows(0)(6)) & "</TD>"
					StrHtml = StrHtml & "		           <TD WIDTH='10%' ><FONT  face='Arial' size=1>&nbsp;</FONT></TD>"
					StrHtml = StrHtml & "		        </TR>"
					StrHtml = StrHtml & "		        </TABLE>"
					StrHtml = StrHtml & "		</TD>"
					StrHtml = StrHtml & "	</TR>"
					StrHtml = StrHtml & "</TABLE></TD></TR></TABLE>"
				end if
			else
				''Set objRecordSetD = objComponente.Get_RS04()
				'Set objRecordSetD = XmlToRecordset(StrXml,"RS")
				
				if dsCompro.Tables(3).Rows.Count > 0 then				
					IF	trim(tipo) = "ZBOL" THEN				
						'<!--	DATOS PARA LA IMPRESION DE LA BOLETA	-->
						
						if CuentaFilas<=18 then
							if CuentaLineaVenc>=0 then
								if CuentaLineaVenc<=9 then
									for ContFor=0 to 9-CuentaLineaVenc
										StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
									next 
								end if 
							end if 
							'for ContFor=0 to 11-CuentaFilas 'CAP: Linea original. Se reduce el espacio para que entre la marca registrada
							for ContFor=0 to 10-CuentaFilas
								StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
							next
						end if 	
						'
						'CAP: Bloque creado solo para mostrar la marca registrada
						StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr 
						StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
						StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='93%'><FONT  face='Arial' size =1>&nbsp;" & gStrMarca & "</FONT></td>" & vbCr 
						strHtml = StrHtml & " </TABLE>" & vbCr 
						'CAP: Fin bloque

						StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr 
						StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
						StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='73%'><FONT  face='Arial' size =1>&nbsp;" & 	dsCompro.Tables(3).Rows(0)(0) & "</FONT></td>" & vbCr 
						if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then 
							'StrHtml = StrHtml & "									<td align='right'>&nbsp;</td>"
							'StrHtml = StrHtml & "									<td align='right'>&nbsp;</td>"
							StrHtml = StrHtml & "	<td width='6%' align=right><FONT  face='Arial' size=1>&nbsp;<FONT  face='Arial' size=1>&nbsp;</FONT> </FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='12%' align=right><FONT  face='Arial'>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='2%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>" & vbCr 
							StrHtml = StrHtml & " </TABLE>" & vbCr 
						else 
							'CAP: Colocar espacios para alinear el signo, segun la cantidad de caracteres
							strNbsp = ""
							StrHtml = StrHtml & "	<td width='6%' align=right><FONT  face='Arial' size=1>Valor Venta</FONT></td>" & vbCr 
							'StrHtml = StrHtml & "	<td width='12%' align=right><FONT  face='Arial' size=1>&nbsp;" & cant_dec(ttotal) & "</FONT></td>" & vbCr 'CAP: Linea original.Se coloca el simbolo S/.
							StrHtml = StrHtml & "	<td width='12%' align=right><FONT  face='Arial' size=1>S/&nbsp;" & strNbsp & cant_dec(ttotal) & "</FONT></td>" & vbCr  
							StrHtml = StrHtml & "	<td width='2%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>" & vbCr 
							StrHtml = StrHtml & " </TABLE>" & vbCr 
						end if 
						StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr 
						StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
						StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='56%'><FONT  face='Arial' size=1>&nbsp;<FONT  face='Arial' size=1>&nbsp;" & 	dsCompro.Tables(3).Rows(0)(1) & "</FONT></FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='14%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then 
							StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial' size=1 ><FONT  face='Arial'>&nbsp;</FONT></FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='2%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "    </tr>" & vbCr 
							StrHtml = StrHtml & " </TABLE>" & vbCr 
						else 								 
							'CAP: Colocar espacios para alinear el signo, segun la cantidad de caracteres
							strNbsp = ""
							
							StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;Ajuste Redondeo</FONT></td>" & vbCr 
							'StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial' size=1 ><FONT  face='Arial'>&nbsp;" & negativo(objRecordSetD.Fields(5).Value) & "</FONT></FONT></td>" & vbCr  'CAP: linea original. Se coloco el signo de S/.
							StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial' size=1 ><FONT  face='Arial'>S/&nbsp;" & strNbsp & negativo(	dsCompro.Tables(3).Rows(0)(5)) & "</FONT></FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='2%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "    </tr>" & vbCr 
							StrHtml = StrHtml & " </TABLE>" & vbCr 

						end if 
					ELSE		
						'<!--	DATOS PARA LA IMPRESION DE LA FACTURA	-->			

						IF TRIM(tipo)="B/V" OR TRIM(tipo)="FAC" THEN'PIE DE PAGINA Y TOTALES

							if TRIM(tipo)="FAC" then
								if cadRV = "1" then
										%><!-- #include file="IncludeRVirtuallmMeta.aspx" -->
										<%
										cadRV = "0"
								else
									'for contfor=0 to 17-CuentaFilas 'CAP: Se reduce el espacio para que entre la marca
									for contfor=0 to 16-CuentaFilas
										StrHtml = StrHtml & "			    <TR><TD><FONT face='Arial' size=1>&nbsp;</FONT></TD></TR>"
									next
								end if
							end if 

							if TRIM(tipo)="B/V" then
								if cadRV = "1" then
									%><!-- #include file="IncludeRVirtuallmMeta.aspx" -->
									<%
									cadRV = "0"
								else
								for contfor=0 to 15-CuentaFilas
									StrHtml = StrHtml & "			    <TR><TD><FONT face='Arial' size=1>&nbsp;</FONT></TD></TR>"
								next 
							end if
						

							IF TRIM(tipo)="B/V" THEN
								StrHtml = StrHtml & "			    <TR><TD WIDTH='100%'>"
								StrHtml = StrHtml & "			       <TABLE BORDER=0 CELLPADDING=1 CELLSPACING=1>"
								StrHtml = StrHtml & "			       <TR>"
								StrHtml = StrHtml & "			           <TD WIDTH='82%'>"'<!--ESPACIO-->
								'StrHtml = StrHtml & "			              <FONT face='Arial' size=1></FONT>"  'CAP: Linea original. Se incluye marca
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>" & gStrMarca &"</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			           <TD WIDTH=7% ALIGN=CENTER>"'<!--ESPACIO-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;Ajuste Redondeo</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			           <TD WIDTH=3% ALIGN=CENTER>"'<!--TOTAL-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;&nbsp;&nbsp;</FONT>"
								StrHtml = StrHtml & "			           </TD>"

								StrHtml = StrHtml & "			           <TD WIDTH=6% ALIGN=RIGHT>"'<!--TOTAL-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>S/&nbsp;" & negativo(	dsCompro.Tables(3).Rows(0)(5)) & "</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			       </TR>"
								StrHtml = StrHtml & "			       </TABLE>"
								StrHtml = StrHtml & "			    </TD></TR>"
							end if 

							IF TRIM(tipo)="FAC" THEN
								StrHtml = StrHtml & "			       <TR>"
								StrHtml = StrHtml & "			           <TD WIDTH='100%'>"'<!--TOTAL-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;" & gStrMarca & "</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			       </TR>"
							end if


							StrHtml = StrHtml & "			    <TR><TD WIDTH='100%'>"
							StrHtml = StrHtml & "			       <TABLE BORDER=0 CELLPADDING=1 CELLSPACING=1>"
							StrHtml = StrHtml & "			       <TR>"
							StrHtml = StrHtml & "			           <TD WIDTH='90%'>"'<!--ESPACIO-->
							StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;" & 	dsCompro.Tables(3).Rows(0)(0) & "</FONT>"
							StrHtml = StrHtml & "			           </TD>"
							StrHtml = StrHtml & "			           <TD WIDTH=3% ALIGN=CENTER>"'<!--TOTAL-->
							StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;&nbsp;&nbsp;</FONT>"
							StrHtml = StrHtml & "			           </TD>"
							StrHtml = StrHtml & "			           <TD WIDTH=6% ALIGN=RIGHT>"'<!--TOTAL-->
							IF TRIM(tipo)="FAC" THEN
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>S/&nbsp;" & cant_dec(ttotal) & "</FONT>"
							ELSE
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>S/&nbsp;" & 	dsCompro.Tables(3).Rows(0)(6) & "</FONT>"
							END IF 
							
							StrHtml = StrHtml & "			           </TD>"
							StrHtml = StrHtml & "			       </TR>"
							StrHtml = StrHtml & "			       </TABLE>"
							StrHtml = StrHtml & "			    </TD></TR>"

							StrHtml = StrHtml & "			    <TR><TD WIDTH='100%'>"
							StrHtml = StrHtml & "			       <TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0>"
							StrHtml = StrHtml & "			       <TR>"
							StrHtml = StrHtml & "			           <TD WIDTH='86%'>"'<!--ESPACIO-->
							StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;SON : " & 	dsCompro.Tables(3).Rows(0)(1) & "</FONT>"
							StrHtml = StrHtml & "			           </TD>"
							StrHtml = StrHtml & "			           <TD WIDTH='14%'>"'<!--TOTAL-->
							StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;</FONT>"
							StrHtml = StrHtml & "			           </TD>"
							StrHtml = StrHtml & "			       </TR>"
							StrHtml = StrHtml & "			       </TABLE>"
							StrHtml = StrHtml & "			    </TD></TR>"
						ELSE
							if CuentaFilas<=18 then
								if CuentaLineaVenc>=0 then
									if CuentaLineaVenc<=9 then
										for ContFor=0 to 9-CuentaLineaVenc
											StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
										next 
									end if 
								end if 	
								for ContFor=0 to 9-CuentaFilas
									StrHtml = StrHtml & "	<tr cellspacing='0'><td width='100%'><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>"
								next
							end if 	

							StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr
							'StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 >" & vbCr  
							StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
							StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='89%'><FONT  face='Arial' size =1>&nbsp;" & gStrMarca & "</FONT></td>" & vbCr 
							StrHtml = StrHtml & " </TABLE>" & vbCr 


							StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr
							'StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 >" & vbCr  
							StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
							StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='56%'><FONT  face='Arial' size =1>&nbsp;" & 	dsCompro.Tables(3).Rows(0)(0) & "</FONT></td>" & vbCr 

							if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then 

								StrHtml = StrHtml & "	<td width='6%' align=right><FONT  face='Arial' size=1>&nbsp;<FONT  face='Arial' size=1>&nbsp;</FONT> </FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='12%' align=right><FONT face='Courier New' size=1>&nbsp;<FONT  face='Arial'>&nbsp;</FONT></FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='5%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>" & vbCr 
								StrHtml = StrHtml & " </TABLE>" & vbCr 

							else 

								StrHtml = StrHtml & "	<td width='6%' align=right><FONT  face='Arial' size=1>&nbsp;<FONT  face='Arial' size=1>Valor Venta</FONT> </FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='12%' align=right><FONT face='Courier New' size=1>&nbsp;<FONT  face='Arial'>&nbsp;" & cant_dec(ttotal) & "</FONT></FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='5%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td></tr>" & vbCr 
								StrHtml = StrHtml & " </TABLE>" & vbCr 

							end if 

							StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr 
							StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
							StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='51%'><FONT  face='Arial' size=1>&nbsp;<FONT  face='Arial' size=1>&nbsp;" & 	dsCompro.Tables(3).Rows(0)(1) & "</FONT></FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='14%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then 

								StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial' size=1 ><FONT  face='Arial'>&nbsp;</FONT></FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "    </tr>" & vbCr 
								StrHtml = StrHtml & " </TABLE>" & vbCr 

							else 

								StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;Ajuste Redondeo</FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial' size=1 ><FONT  face='Arial'>&nbsp;" & negativo(	dsCompro.Tables(3).Rows(0)(5)) & "</FONT></FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "    </tr>" & vbCr 
								StrHtml = StrHtml & " </TABLE>" & vbCr 

							end if 
						END IF ' FIN FAC B/V			
					END IF					
				
					'<!--	DATOS PARA LA IMPRESION DE LA BOLETA	-->
					IF	trim(tipo) = "ZBOL" THEN	

						StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr 
						StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
						StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='56%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='14%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;</FONT></td>" & vbCr 
						if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then 
							'StrHtml = StrHtml & "								<td align='right'>" & format(objRecordSetD.Fields(2).Value) & "</td>"
							StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial'><FONT size=1>&nbsp;" & format(	dsCompro.Tables(3).Rows(0)(2)) & "</FONT></FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='2%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "    </tr>" & vbCr 
							StrHtml = StrHtml & " </TABLE>" & vbCr 
						else 
							'CAP: se coloco el signo
							StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial'><FONT size=1>S/&nbsp;" & 	dsCompro.Tables(3).Rows(0)(6) & "</FONT></FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='2%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "    </tr>" & vbCr 
							StrHtml = StrHtml & " </TABLE>" & vbCr 
						end if 
			
					ELSE	
						'<!--	DATOS PARA LA IMPRESION DE LA FACTURA	-->

						IF TRIM(tipo)="FAC" OR TRIM(tipo)="B/V" THEN

							IF TRIM(tipo)="FAC" THEN
								StrHtml = StrHtml & "			    <TR><TD WIDTH='100%'>"
								StrHtml = StrHtml & "			       <TABLE BORDER=0 CELLPADDING=1 CELLSPACING=1>"
								StrHtml = StrHtml & "			       <TR>"
								StrHtml = StrHtml & "			           <TD WIDTH='82%'>"'<!--ESPACIO-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			           <TD WIDTH=7% ALIGN=CENTER>"'<!--ESPACIO-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;" & formatnumber(cint(replace(Const_IGV,".","")),0) & "</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			           <TD WIDTH=3% ALIGN=CENTER>"'<!--TOTAL-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;&nbsp;&nbsp;</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			           <TD WIDTH=6% ALIGN=RIGHT>"'<!--TOTAL-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>S/&nbsp;" & 	dsCompro.Tables(3).Rows(0)(3) & "</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			       </TR>"
								StrHtml = StrHtml & "			       <TR>"
								StrHtml = StrHtml & "			           <TD WIDTH='82%'>"'<!--ESPACIO-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			           <TD WIDTH=7% ALIGN=CENTER>"'<!--ESPACIO-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;Ajuste Redondeo</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			           <TD WIDTH=3% ALIGN=CENTER>"'<!--TOTAL-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;&nbsp;&nbsp;</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			           <TD WIDTH=6% ALIGN=RIGHT>"'<!--TOTAL-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>S/&nbsp;" & negativo(	dsCompro.Tables(3).Rows(0)(5)) & "</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			       </TR>"
								
								StrHtml = StrHtml & "			       <TR>"
								StrHtml = StrHtml & "			           <TD WIDTH='82%'>"'<!--ESPACIO-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			           <TD WIDTH=7% ALIGN=CENTER>"'<!--ESPACIO-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			           <TD WIDTH=3% ALIGN=CENTER>"'<!--TOTAL-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>&nbsp;&nbsp;&nbsp;</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			           <TD WIDTH=6% ALIGN=RIGHT>"'<!--TOTAL-->
								StrHtml = StrHtml & "			              <FONT face='Arial' size=1>S/&nbsp;" & 	dsCompro.Tables(3).Rows(0)(6) & "</FONT>"
								StrHtml = StrHtml & "			           </TD>"
								StrHtml = StrHtml & "			       </TR>"
								
								StrHtml = StrHtml & "			       </TABLE>"
								StrHtml = StrHtml & "			    </TD></TR>"


							END IF

						ELSE
							StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr 
							StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
							StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='51%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='14%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;</FONT></td>" & vbCr 

							if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then 
								'StrHtml = StrHtml & "								<td align='right'>" & format(objRecordSetD.Fields(2).Value) & "</td>"
								StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial'><FONT size=1>&nbsp;" & format(	dsCompro.Tables(3).Rows(0)(2)) & "</FONT></FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "    </tr>" & vbCr 
								StrHtml = StrHtml & " </TABLE>" & vbCr 

							else 
								'StrHtml = StrHtml & "								<td align='right'>" & objRecordSetD.Fields(6).Value & "</td>"
								StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial'><FONT size=1>&nbsp;" & 	dsCompro.Tables(3).Rows(0)(6) & "</FONT></FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "    </tr>" & vbCr 
								StrHtml = StrHtml & " </TABLE>" & vbCr 

							end if 
						END IF 
					END IF	
					IF	trim(tipo) = "ZFAC" THEN 
						'<!--	DATOS PARA LA IMPRESION DE LA FACTURA	-->
						StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr 
						StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
						StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='51%'><FONT  face='Arial' size=1>&nbsp;<FONT  face='Arial' size=1>&nbsp;</FONT></FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='14%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 

						if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then 
							'StrHtml = StrHtml & "								<td align='right'>&nbsp;&nbsp;&nbsp;&nbsp;" & trim(objRecordSetD.Fields(4).Value)+"%" & "</td>"
							'StrHtml = StrHtml & "								<td align='right'>" & format(objRecordSetD.Fields(3).Value) & "</td>"
							StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;" & trim(	dsCompro.Tables(3).Rows(0)(4))+"%" & "</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial' size=1 >S/&nbsp;" & format(	dsCompro.Tables(3).Rows(0)(3)) & "</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "    </tr>" & vbCr 
							StrHtml = StrHtml & " </TABLE>" & vbCr 

						else 
							'StrHtml = StrHtml & "								<td align='right'>&nbsp;</td>"
							'StrHtml = StrHtml & "								<td align='right'>&nbsp;</td>"
							StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial' size=1 >&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "    </tr>" & vbCr 
							StrHtml = StrHtml & " </TABLE>" & vbCr 

						end if 
			
					ELSE	

						IF not(TRIM(tipo)="B/V" OR TRIM(tipo)="FAC") THEN				
							StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr 
							StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
							StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='51%'><FONT  face='Arial' size=1>&nbsp;<FONT  face='Arial' size=1>&nbsp;</FONT></FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='14%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 

							if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then 
								'StrHtml = StrHtml & "								<td align='right'>&nbsp;&nbsp;&nbsp;&nbsp;" & trim(objRecordSetD.Fields(4).Value)+"%" & "</td>" & vbCr 
								'StrHtml = StrHtml & "								<td align='right'>" & format(objRecordSetD.Fields(3).Value) & "</td>" & vbCr 
								StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;" & trim(	dsCompro.Tables(3).Rows(0)(4))+"%" & "</FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial' size=1 >S/&nbsp;" & format(	dsCompro.Tables(3).Rows(0)(3)) & "</FONT></td>" & vbCr  'CAP : Signo de S/
								StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "    </tr>" & vbCr 
								StrHtml = StrHtml & " </TABLE>" & vbCr 

							else 
								'StrHtml = StrHtml & "								<td align='right'>&nbsp;</td>" & vbCr 
								'StrHtml = StrHtml & "								<td align='right'>&nbsp;</td>" & vbCr 
								StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial' size=1 >&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "    </tr>" & vbCr 
								StrHtml = StrHtml & " </TABLE>" & vbCr 
							end if 

						END IF 'FIN FAC B/V		
					END IF	
				
					IF	trim(tipo) = "ZFAC" THEN 
				
						StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr 
						StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
						StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='51%'><FONT  face='Arial' size=1>&nbsp;<FONT  face='Arial' size=1>&nbsp;</FONT></FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='14%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 

						if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then 
							'StrHtml = StrHtml & "								<td align='right'>Ajuste Redondeo</td>"
							'StrHtml = StrHtml & "								<td align='right'>" & negativo(objRecordSetD.Fields(5).Value) & "</td>"
							StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;Ajuste Redondeo</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial' size=1 >S/&nbsp;" & negativo(	dsCompro.Tables(3).Rows(0)(5)) & "</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "    </tr>" & vbCr 
							StrHtml = StrHtml & " </TABLE>" & vbCr 

						else 

							StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial' size=1 >&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "    </tr>" & vbCr 
							StrHtml = StrHtml & " </TABLE>" & vbCr 

						end if 

					ELSE

						IF not( TRIM(tipo)="B/V" OR TRIM(tipo)="FAC") THEN
							StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr 
							StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
							StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='51%'><FONT  face='Arial' size=1>&nbsp;<FONT  face='Arial' size=1>&nbsp;</FONT></FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='14%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 

							if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then 
								'StrHtml = StrHtml & "								<td align='right'>Ajuste Redondeo</td>" & vbCr 
								'StrHtml = StrHtml & "								<td align='right'>" & negativo(objRecordSetD.Fields(5).Value) & "</td>" & vbCr 
								StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;Ajuste Redondeo</FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial' size=1 >S/&nbsp;" & negativo(	dsCompro.Tables(3).Rows(0)(5)) & "</FONT></td>" & vbCr  'CAP: Signo S/
								StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "    </tr>" & vbCr 
								StrHtml = StrHtml & " </TABLE>" & vbCr 

							else 
								'StrHtml = StrHtml & "								<td align='right'>&nbsp;</td>" & vbCr 
								'StrHtml = StrHtml & "								<td align='right'>&nbsp;</td>" & vbCr 
								StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial' size=1 >&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "    </tr>" & vbCr 
								StrHtml = StrHtml & " </TABLE>" & vbCr 

							end if 

						END IF 'FIN B/V FAC
					END IF
				
					IF	trim(tipo) = "ZFAC" THEN 
						'<!--	DATOS PARA LA IMPRESION DE LA FACTURA	-->

						StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr 
						StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
						StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='51%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='14%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;</FONT></td>" & vbCr 

						if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then 

							StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial'><FONT size=1>S/&nbsp;" & format(	dsCompro.Tables(3).Rows(0)(6)) & "</FONT></FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "    </tr>" & vbCr 
							StrHtml = StrHtml & " </TABLE>" & vbCr 

						else 
							'StrHtml = StrHtml & "								<td align='right'>&nbsp;</td>"
							StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial'><FONT size=1>&nbsp;</FONT></FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "    </tr>" & vbCr 
							StrHtml = StrHtml & " </TABLE>" & vbCr 

						end if 
					ELSE	
		 
						IF not (TRIM(tipo)="B/V" OR TRIM(tipo)="FAC") THEN
							StrHtml = StrHtml & " <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr 
							StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
							StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='51%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='14%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
							StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;</FONT></td>" & vbCr 
							if (tipo = "ZFAC" or tipo="ZNCV" or tipo="ZNDV") then 
								'StrHtml = StrHtml & "								<td align='right'>" & format(objRecordSetD.Fields(6).Value) & "</td>" & vbCr 
								StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial'><FONT size=1>S/&nbsp;" & format(	dsCompro.Tables(3).Rows(0)(6)) & "</FONT></FONT></td>" & vbCr  'CAP: Signo S/
								StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "    </tr>" & vbCr 
								StrHtml = StrHtml & " </TABLE>" & vbCr 

							else 
								'StrHtml = StrHtml & "								<td align='right'>&nbsp;</td>" & vbCr 
								StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial'><FONT size=1>&nbsp;</FONT></FONT></td>" & vbCr 
								StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
								StrHtml = StrHtml & "    </tr>" & vbCr 
								StrHtml = StrHtml & " </TABLE>" & vbCr 

							end if 

						END IF 'FIN B/V FAC
					END IF

					'*** PMO - CUOTAS EN VENTA RAPIDA - INICIO ***
					'*** FECHA: 23/11/2005 - AUTOR: NLCH
					Dim strNuevaDescrDocumento
					If intNumeroCuotas > 1 then
						intNumeroCuotas = intNumeroCuotas - 1
						strNuevaDescrDocumento = GetTramaFinalCuotasDescDocumento(intNumeroCuotas) '"***TENGO UN MONTON DE CUOTAS***"
					else
						strNuevaDescrDocumento = 	dsCompro.Tables(3).Rows(0)(7)
					end if
					'*** PMO - CUOTAS EN VENTA RAPIDA - FIN ***

					StrHtml = StrHtml & "					<table border='0' bordercolor='#000000' width='650'> " & vbCr 
					StrHtml = StrHtml & "						<tr class=clsArial18>" & vbCr 
					StrHtml = StrHtml & "							<td align='right'>" & strNuevaDescrDocumento & "</td>" & vbCr 
					StrHtml = StrHtml & "						</tr>" & vbCr 
					StrHtml = StrHtml & "					</table>" & vbCr 
					IF TRIM(tipo)="B/V" OR TRIM(tipo)="FAC" THEN

						StrHtml = StrHtml & "			</TABLE>"
						StrHtml = StrHtml & "		</TD>"
						StrHtml = StrHtml & "	</TR>"
						StrHtml = StrHtml & "</TABLE>"
					ELSE
						StrHtml = StrHtml & " <TABLE WIDTH=710 height=10 BORDER=0 CELLSPACING=0>" & vbCr 
						StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
						StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "    </tr>" & vbCr 
						StrHtml = StrHtml & " </TABLE>" & vbCr 
						StrHtml = StrHtml & "  <TABLE WIDTH=710  BORDER=0 CELLSPACING=0>" & vbCr 
						StrHtml = StrHtml & "	<tr cellspacing='0'>" & vbCr 
						StrHtml = StrHtml & "	<td width='7%'><FONT  face='Arial' size=1>&nbsp;" &  "" & NOW.tostring("d") & "" & "</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='51%'><FONT  face='Arial' size=1>&nbsp;" &  "" & NOW.tostring("t") & ""  & "</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='14%'><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='11%' align=right><FONT  face='Arial' size =1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='10%' align=right><FONT  face='Arial'><FONT size=1>&nbsp;</FONT></FONT></td>" & vbCr 
						StrHtml = StrHtml & "	<td width='7%' align=right><FONT  face='Arial' size=1>&nbsp;</FONT></td>" & vbCr 
						StrHtml = StrHtml & "    </tr>" & vbCr 
						StrHtml = StrHtml & " </TABLE>" & vbCr 
					END IF 
				end if		'if dsCompro.Tables(3).Rows.Count > 0 then
			end if   'if(tipo="NPED") then 			
		end if 'if(tipo="ZDAU") then
	end if 'if (len(tipo) > 0) then
else
	Response.Write( "No existe tipo de documento de impresión asociado a este trámite." + docSap)
end if


StrHtml = StrHtml & "</body>"

	
	
    'set objRecordSet = nothing
	'Set objComponente = nothing


StrRutaFisicaSite=Request.ServerVariables("APPL_PHYSICAL_PATH")  & "Paginas"  


StrHtml = StrHtml & "</html>"
'Response.Write StrHtml
'Response.Write("Hola")
'Response.End

Dim PDF,HTML,lStrRuta,oShell,lStrCadena,lStrRutaImage,NombreArchivo
lStrRuta=Request.ServerVariables("APPL_PHYSICAL_PATH")


NombreArchivo= oficinaVenta & docSap & REPLACE(now.DATE.tostring("d"),"/","") &  REPLACE(LEFT(now.tostring("T"),8),":","") 
CrearArchivo (NombreArchivo & ".html",StrHtml,"Impresion")



PDF=StrRutaFisicaSite & "\Impresion\" & NombreArchivo & ".pdf"
HTML=StrRutaFisicaSite & "\Impresion\" & NombreArchivo & ".html" 

       
oShell = Server.CreateObject("Wscript.Shell")	
lStrCadena=  "%ComSpec% /c " & trim(left(lStrRuta,2)) & "\HTMLDOC\GHTMLDOC.EXE -t pdf --path " & lStrRuta & " --top 5 --bottom 0 --left 0 --header .f. --footer .f. --size A4 z--browserwidth 680 --headfootsize 8 --webpage --pagemode document " & HTML & " -f " & PDF

'response.write(lStrCadena)

oShell.Run (lStrCadena,0,True)
OShell = Nothing

If FSO.FileExists(HTML) Then
   Fso.DeleteFile (HTML)
end if   




if trim(tipo)="DEV" or trim(tipo)="NPED" or trim(tipo) = "ZBOL" or tipo="ZFAC" or tipo="FAC" or tipo="B/V" then
   Response.Redirect ("../paginas/impresion/" & NombreArchivo & ".pdf")
   %>
			<script language=javascript>
				open('<%="../../paginas/impresion/" & NombreArchivo & ".pdf" %>');
				open('<%="../../paginas/impresion/EliminaPDF.asp?PDF=" & NombreArchivo %>');
			</script>
   <%
end if 
 Fso=nothing
%>

</FORM></TR></TBODY></TABLE></TR></TBODY></TABLE> </BODY></HTML>
