<%@ Page Language="vb" CodePage="1252" CodeBehind="sicar_nota_canje.aspx.vb" AutoEventWireup="false" Inherits="SisCajas.sicar_nota_canje" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ImpresionTicket</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<style> .clsTexto { FONT-SIZE: 10pt; FONT-FAMILY: Arial; }
	.clsTextoDeta { FONT-SIZE: 9pt; FONT-FAMILY: Arial; }
	.clsTextoR { FONT-SIZE: 10pt; FONT-FAMILY: Arial; }
	.clsTextoN { FONT-SIZE: 9.3pt; FONT-FAMILY: Arial; }
	.clsTextoDoc { FONT-SIZE: 13pt; FONT-FAMILY: Arial Narrow; }
	.clsTextoCabecera{ FONT-SIZE: 10pt; FONT-FAMILY: Arial Narrow; }
	.clsNumero { FONT-SIZE: 11pt; FONT-FAMILY: Arial Narrow; }
	.clsVigencia { FONT-SIZE: 10pt; FONT-FAMILY: Tahoma; }
	.tabla_borde { BORDER-RIGHT: #336699 1px solid; BORDER-TOP: #336699 1px solid; FONT-SIZE: 12px; BORDER-LEFT: #336699 1px solid; COLOR: #ff0000; BORDER-BOTTOM: #336699 1px solid; FONT-FAMILY: Arial; TEXT-DECORATION: none }
	.Boton { border-right: #95b7f3 1px solid; border-top: #95b7f3 1px solid; font-weight: bold; font-size: 10px; border-left: #95b7f3 1px solid; cursor: hand; color: #003399; border-bottom: #95b7f3 1px solid; font-family: Verdana; background-color: white; text-align: center; TEXT-DECORATION: none; BACKGROUND-REPEAT: repeat-x; background-color: #e9f2fe; /*BACKGROUND-IMAGE: url(../images/toolgrad.gif); */ border-color :#95b7f3 }
	.BotonResaltado { border-right: #95b7f3 1px solid; border-top: #95b7f3 1px solid; font-weight: bold; font-size: 10px; border-left: #95b7f3 1px solid; cursor: hand; color: #003399; border-bottom: #95b7f3 1px solid; font-family: Verdana; background-color: white; text-align: center; TEXT-DECORATION: none; BACKGROUND-REPEAT: repeat-x; border-color :#95b7f3 }
		</style>
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
        function fe_imprimir(){
			divBotones.style.visibility = "HIDDEN";
			window.print();
			divBotones.style.visibility = "VISIBLE";
		}
		// PBI000002160737
		$(document).ready(function () {
			
			$('body').bind('cut copy paste', function (e) {
				e.preventDefault();
			});
		   
			$("body").on("contextmenu",function(e){
				return false;
			});
		});
		// PBI000002160737
		</script>
		<script runat="server">
        'Impresion en sumatoria
    
        Dim dtVendedor As System.Data.DataTable
        Dim obUbigeo As New COM_SIC_Cajas.clsCajas
	
        Dim cadenaP As String = ""
        Dim CodVendedor As String
        Dim NomVendedor As String
        Dim CodCajero As String '= session("USUARIO")
        Dim NomCajero As String '= session("NOMBRE_COMPLETO")
    
		
        '-------------------------
        '		Para LOGS		  |
        '------------------------- 
        Dim objFileLog As New SisCajas.SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = ""
        
        
        Public Function FncImprCabecera(ByVal dtResult As System.Data.DataTable) As String
            Dim cad As String

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			1.- Imprimir Cabecera   ")

            Dim gStrRazonSocial As String = System.Configuration.ConfigurationSettings.AppSettings("gStrRazonSocial")
            Dim gStrMarca As String = System.Configuration.ConfigurationSettings.AppSettings("gStrMarca")
            Dim gStrRUC As String = System.Configuration.ConfigurationSettings.AppSettings("gStrRUC")
			Dim gStrDireccion As String = strConsDirecClaroCanje 'PROY-23700-IDEA-29415 - ACB
			Dim gStrUrb As String = strConsUrbClaroCanje  'PROY-23700-IDEA-29415 - ACB
			Dim gStrDistrito As String = strConsDistritoClaroCanje 'PROY-23700-IDEA-29415 - ACB
			
            'If dsCompro.Tables(0).Rows.Count > 0 Then
                cad = "<TABLE WIDTH=240 CELLSPACING=0 CELLPADDING=0 BORDER=0>"
                cad = cad & "<TR>"
                cad = cad & "<TD width='160' class='clsTextoCabecera'><b>" & StrConv(LCase(gStrRazonSocial),VbStrConv.ProperCase) & "</b></TD><TD class='clsTextoR' style='text-align:Center;'><b>R.U.C. " & gStrRUC & "</b></TD>"
                cad = cad & "</TR><TR>"
                cad = cad & "<TD class='clsTexto'><b>" & gStrMarca & "</b></TD><TD rowspan='5' class=clsTextoDoc valign='top'; style='text-align:center;'>"
		
	
                cad = cad & "<b>" & CStr(System.Configuration.ConfigurationSettings.AppSettings("NomDocNotaCanje")) & "</b></BR>" 'NUEVA LLAVE (LISTO)

                cad = cad & "<span class='clsTextoN'><b>" & Trim(CStr(dtResult.Rows(0)(3))) & "</b></span></br>"    'LA REFERENCIA NUEVO
		
                cad = cad & "</td></TR>"
				cad = cad & "<TR><td class='clsTextoCabecera'>" & gStrDireccion & "</td></tr>"
                cad = cad & "<TR><td class='clsTextoCabecera'>" & gStrUrb & "</td></tr>"
                cad = cad & "<TR><td class='clsTextoCabecera'>" & gStrDistrito & "</td></tr>"
                cad = cad & "<TR><td class='clsTextoCabecera'>Telefono: " & System.Configuration.ConfigurationSettings.AppSettings("FE_TelefonoClaro") & "</td></tr>"
                cad = cad & "<TR><td class='clsTextoCabecera'>Fax: " & System.Configuration.ConfigurationSettings.AppSettings("FE_FaxClaro") & "</td></tr>"
                cad = cad & "<TR><td class='clsTexto'> " & System.Configuration.ConfigurationSettings.AppSettings("FE_WebClaro") & "</td></tr>"
                'cad=cad & "<TR><td colspan='2' style='text-align:right;' class='clsTexto'><b>" & FormatoFechaFE(cstr(dsCompro.tables(0).rows(0)(3))) & " " & Ucase(now.tostring("t")) & "</b></td></tr>"
                cad = cad & "<TR><td colspan='2' style='text-align:right;' class='clsTexto'><b>" & FormatoFechaFE(CStr(dtResult.Rows(0)(8))) & " " & UCase(Now.ToString("t")) & "</b></td></tr>"  ' FECHA
                cad = cad & "<TR><td colspan='2'></td></tr>"
                cad = cad & "</TABLE>"
                cad = cad & "<TABLE WIDTH=240 CELLSPACING=0 CELLPADDING=0 BORDER=0>"
			
            'End If
            Return cad 
        End Function
        
        Public Function FncImprCliente(ByVal dtResult As System.Data.DataTable) As String
            Dim cad As String
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			2.- Imprimir Cliente")
            'If dsCompro.Tables(0).Rows.Count > 0 Then

                cad = "<tr Style='border-style:Solid; border-bottom: thick'><td colspan='2' class='clsTexto'>"

                cad = cad & "<TABLE WIDTH=240 CELLSPACING=0 CELLPADDING=0 BORDER=0>"
                cad = cad & "<TR><td class='clsTexto'><b>" & Trim(cstr(dtResult.rows(0)(12))) & "</b></td></TR>"                                 
                cad = cad & "<tr><td class='clsTexto'><b>" & Trim(CStr(dtResult.rows(0)(4))) & "</b></TD></tr>"
                'cad = cad & "<TR><td class='clsTexto'><b>" & DirecUbicacion & " - " & NomDepartamento & " - " & NombreDistrito & "</b></td></TR>" 
                cad = cad & "<TR><td class='clsTexto'><b>" & Trim(CStr(dtResult.rows(0)(6))) & "</b></td></TR>" 
                cad = cad & "<TR><td class='clsTexto'><b>" & Trim(CStr(dtResult.rows(0)(5))) & "</b></td></TR>" 	
                'cad = cad & "<TR><td class='clsTexto'><b>OFICINA DE VTA: " & OficinaVta & "</b></td></TR>"
			
                cad = cad & "</TABLE>"
			
                cad = cad & "</BR>"
			
                cad = cad & "</td></TR>"
			
            'End If

            Return cad
        End Function
        
        
        Public Function FncImprDetVentas(ByVal dtResult As System.Data.DataTable,ByVal descripMotivo As String) As String
            Dim cad
           objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			3.- Imprimir Detalle Venta")
	    '----------------------------------------------------------------------------------------------------------------
            '--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
            '----------------------------------------------------------------------------------------------------------------            
            if dtResult.rows.count > 0 then
				Dim dr As System.Data.DataRow			
                Dim i As Integer = 0
	
                cad = "<TR>"
                cad = cad & "<TD STYLE='PADDING-LEFT:0;' STYLE='border-top:solid 1px;'>"
                cad = cad & "</BR>"
                cad = cad & "<TABLE WIDTH=240 CELLSPACING=0 CELLPADDING=0 BORDER=0>"
			
                    cad = cad & "<TR>"
                    cad = cad & "<TD class='clsTextoDeta' width='100%' border='1' STYLE='border-bottom:solid 1px;width:100%;'>"
                    cad = cad & "<b>EQUIPO EN DEVOLUCION</b>"
                cad = cad & "</TD>"                    
                cad = cad & "<TD ALIGN='RIGHT' class='clsTextoDeta'>"
                cad = cad & "S/.<FONT class=clsNumero>" & cant_dec(Trim(CStr(dtResult.Rows(0)(11)))) & "&nbsp;</FONT>"				
                cad = cad & "</TD>"                    
                cad = cad & "</TR>"
							
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------")			
				objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INICIO Mostrar Detalle NotaCanje ")
				objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------")
				
                For Each dr In dtResult.rows					
					dim K_PEDIN_NROPEDIDO as string = IIf(IsDBNull(dtResult.Rows(i)(0)), "", Trim(CStr(dtResult.Rows(i)(0))))
					dim K_PEDIV_DESCTIPOOPERACION as string = IIf(IsDBNull(dtResult.Rows(i)(1)), "", Trim(CStr(dtResult.Rows(i)(1))))
					dim K_PEDIV_DESCCLASEFACTURA as string = IIf(IsDBNull(dtResult.Rows(i)(2)), "", Trim(CStr(dtResult.Rows(i)(2))))
					dim K_PEDIV_NROSUNAT as string = IIf(IsDBNull(dtResult.Rows(i)(3)), "", Trim(CStr(dtResult.Rows(i)(3))))
					dim K_NOMBRECLIENTE as string = IIf(IsDBNull(dtResult.Rows(i)(4)), "", Trim(CStr(dtResult.Rows(i)(4))))
					dim K_CLIEV_NRODOCCLIENTE as string = IIf(IsDBNull(dtResult.Rows(i)(5)), "", Trim(CStr(dtResult.Rows(i)(5))))
					dim K_CLIEV_DIRECCIONCLIENTE as string = IIf(IsDBNull(dtResult.Rows(i)(6)), "", Trim(CStr(dtResult.Rows(i)(6))))
					dim K_PEDIC_ESTADO as string = IIf(IsDBNull(dtResult.Rows(i)(7)), "", Trim(CStr(dtResult.Rows(i)(7))))
					dim K_PAGOD_FECHACONTA as string = IIf(IsDBNull(dtResult.Rows(i)(8)), "", Trim(CStr(dtResult.Rows(i)(8))))
					dim K_PEDIV_USUARIOCREA as string = IIf(IsDBNull(dtResult.Rows(i)(9)), "", Trim(CStr(dtResult.Rows(i)(9))))
					dim K_CODCAJERO as string = IIf(IsDBNull(dtResult.Rows(i)(10)), "", Trim(CStr(dtResult.Rows(i)(10))))
					'dim K_PEDIV_USUARIOCREA_2 as string = IIf(IsDBNull(dtResult.Rows(i)(11)), "", Trim(CStr(dtResult.Rows(i)(11))))
					dim K_TOTAL as string = IIf(IsDBNull(dtResult.Rows(i)(11)), "", Trim(CStr(dtResult.Rows(i)(11))))
					dim K_DESCRIPCION_OFICINA as string					
					If IsDBNull(dtResult.Rows(i)(12)) Then
                         K_DESCRIPCION_OFICINA= ""
                    Else
                         K_DESCRIPCION_OFICINA = Trim(CStr(dtResult.Rows(i)(12)))
                    End If
			
					dim K_DESCRIPCION_MATERIAL as string = IIf(IsDBNull(dtResult.Rows(i)(13)), "", Trim(CStr(dtResult.Rows(i)(13))))
					dim K_SERIE_MATERIAL as string = IIf(IsDBNull(dtResult.Rows(i)(14)), "", Trim(CStr(dtResult.Rows(i)(14))))
					dim K_MONTO_LEYENDA as string = IIf(IsDBNull(dtResult.Rows(i)(15)), "", Trim(CStr(dtResult.Rows(i)(15))))
				
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_PEDIN_NROPEDIDO : " & K_PEDIN_NROPEDIDO)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_PEDIV_DESCTIPOOPERACION : " & K_PEDIV_DESCTIPOOPERACION)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_PEDIV_DESCCLASEFACTURA : " & K_PEDIV_DESCCLASEFACTURA)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_PEDIV_NROSUNAT : " & K_PEDIV_NROSUNAT)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_NOMBRECLIENTE : " & K_NOMBRECLIENTE)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_CLIEV_NRODOCCLIENTE : " & K_CLIEV_NRODOCCLIENTE)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_CLIEV_DIRECCIONCLIENTE : " & K_CLIEV_DIRECCIONCLIENTE)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_PEDIC_ESTADO : " & K_PEDIC_ESTADO)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_PAGOD_FECHACONTA : " & K_PAGOD_FECHACONTA)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_PEDIV_USUARIOCREA : " & K_PEDIV_USUARIOCREA)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_CODCAJERO : " & K_CODCAJERO)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_TOTAL : " & K_TOTAL)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_DESCRIPCION_OFICINA : " & K_DESCRIPCION_OFICINA)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_DESCRIPCION_MATERIAL : " & K_DESCRIPCION_MATERIAL)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_SERIE_MATERIAL : " & K_SERIE_MATERIAL)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "K_MONTO_LEYENDA : " & K_MONTO_LEYENDA)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------")
				
					'Inicio: Proy 31476
                    'cad = cad & "S/.<FONT class=clsNumero>" & cant_dec(Trim(CStr(dtResult.Rows(0)(11)))) & "&nbsp;</FONT>"
                    cad = cad & "S/<FONT class=clsNumero>" & cant_dec(Trim(CStr(dtResult.Rows(0)(11)))) & "&nbsp;</FONT>"
					'Fin: Proy 31476
				
                    cad = cad & "<TR>"
                    cad = cad & "<TD colspan='2' class='clsTextoDeta'>"
                    cad = cad & Trim(CStr(dtResult.Rows(i)(13))) & "&nbsp;" 'Descripcion del material
                    cad = cad & "</TD>"
                    cad = cad & "</TR>"
				
                    cad = cad & "<TR>"
                    cad = cad & "<TD colspan='2' class='clsTextoDeta'>"
                    cad = cad & Trim(CStr(dtResult.Rows(i)(14))) & "&nbsp;" 'Serie del Material
                    cad = cad & "</TD>"
                    cad = cad & "</TR>"
				
                    cad = cad & "<TR>"
                    cad = cad & "<TD colspan='2' class='clsTextoDeta'>"
                    cad = cad & Trim(descripMotivo) & "&nbsp;" 'motivo de devolucion				
                    cad = cad & "</TD>"
                    cad = cad & "</TR>"
				
					i = i+1
				next
                cad = cad & "</TABLE>"
                cad = cad & "</TD>"
                cad = cad & "</TR>"
				
				objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " FIN Mostrar Detalle NotaCanje ")
				objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------")
			end if
            '----------------------------------------------------------------------------------------------------------------
            '--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
            '----------------------------------------------------------------------------------------------------------------
            				           
            Return cad
        End Function
        
        Public Function FncImprTotales(ByVal dtResult As System.Data.DataTable) As String
            Dim cad
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			4.- Imprimir Totales")
	
           'If dsCompro.Tables(0).Rows.Count > 0 Then
    
                'Dim dtCabecera As System.Data.DataTable
                'dtCabecera = dsCompro.Tables(0)
		
		
                cad = "<TR class=clsTexto ><TD COLSPAN='2' STYLE='PADDING-LEFT:0;'>"
                cad = cad + "</BR>"
                cad = cad + "<TABLE WIDTH=220 CELLSPACING=0 CELLPADDING=0 BORDER=0>"
                cad = cad + "<TR><TD  class='clsTexto'>** SON: " + dtResult.Rows(0)(15) + " SOLES **</TD></TR>"
                cad = cad + "</TABLE>"
                cad = cad + "</TR>"
            'End If
            Return cad
        End Function
        
        Public Function FncImprVendedores(ByVal dtResult As System.Data.DataTable ,ByVal Reimpresion As String ) As String
            Dim cad
            Dim tam
            Dim tamstrcaj
            Dim nombreVendedor
            Dim nombreCajero
            tam = Len(CodVendedor)
            tamstrcaj = Len(CodCajero)
            CodVendedor = CodVendedor.Replace("0000","")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			7.- Imprimir Vendedores")
            CodCajero=session("USUARIO")
            'CodVendedor = CodVendedor.PadLeft(Math.Abs(tam - tamstrcaj), "0")
			CodVendedor = CodVendedor.Replace("000","")
            nombreCajero=session("NOMBRE_COMPLETO")
			
			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			7.- Imprimir Vendedores")
			
            'If dsCompro.Tables(0).Rows.Count > 0 Then
                'Dim dtCabecera As System.Data.DataTable
                'dtCabecera = dsCompro.Tables(0)
                cad = "<TR>"
                cad = cad & "<TD class=clsTexto>"
                cad = cad & "<FONT class=clsNumero>V: " & CodVendedor & "</FONT> " & Mid(NomVendedor, 1, 14) & "<BR>"
                cad = cad & "<FONT class=clsNumero>C: " & CodCajero & "</FONT> " & Mid(nombreCajero, 1, 14) & "<BR>"
                cad = cad & "</BR>"
                cad = cad & "</TD>"
                cad = cad & "</TR>"
			
                if trim(Reimpresion)<>"" then
				cad=cad & "<TR>"
				cad=cad & "<TD align='center' class=clsTexto>"
				cad=cad & "***REIMPRESION***"
				cad=cad & "</TD>"
				cad=cad & "</TR>"
		     	end if 

			
            'End If
            Return cad
        End Function
        
        Public Function FncImprMensajeAcepGarantia() As String
            Dim cad As String
            cad = "<TR>"
            cad = cad + "<BR/>"
            cad = cad + "<TD STYLE='border-top:solid 1px;' align='center'class=clsTexto>" + strConsMensDocNotaCanje + "</td>" 'PROY-23700-IDEA-29415 - ACB
            cad = cad + "</TR>"
            Return cad
        End Function
        
        Public Function FormatoFechaFE(ByVal Fecha As String) As String
            If (Len(Trim(Fecha)) > 0) Then
                FormatoFechaFE = Mid(Fecha, 7, 4) + "/" + Mid(Fecha, 4, 2) + "/" + Mid(Fecha, 1, 2)
            Else
                FormatoFechaFE = ""
            End If
			
        End Function
        
         Public Function cant_dec(ByVal cantidad As String) As String
            Dim ok As Integer = 0
            Dim i As Integer
	   	   
            For i = 1 To Len(cantidad)
                If (Mid(cantidad, i, 1) = ".") Then
                    ok = Len(Mid(Trim(cantidad), i + 1)) 
                End If
            Next
            If ok = 0 Then cant_dec = cantidad & ".00"
            If ok = 1 Then cant_dec = cantidad & "0"
            If ok > 1 Then cant_dec = cantidad
        End Function
    

		</script>
		<!--FIN-->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" runat="server">
			<div id="divBotones">
				<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="400" align="center">
					<tr>
						<td align="center"><input class="Boton" id="btnImprimir" onmouseover="this.className='BotonResaltado'" onclick="javascript:fe_imprimir();"
								onmouseout="this.className='Boton'" size="15" value="Imprimir" name="btnAgregar" runat="server" width="50">
						</td>
						<td align="center"><input class="Boton" id="btnCancelar" onmouseover="this.className='BotonResaltado'" onclick="window.close();"
								onmouseout="this.className='Boton'" size="15" value="Cerrar" name="btnCancelar" runat="server" width="50">
						</td>
					</tr>
				</table>
			</div>
			<input id=strNomDocNotaCanje type=hidden value="<%=strNomDocNotaCanje%>" name=strNomDocNotaCanje>
			<input type="hidden" value="2" name="tipo"> <input id=strConsMensDocNotaCanje type=hidden value="<%=strConsMensDocNotaCanje%>" name=strConsMensDocNotaCanje>
			<input type="hidden" value="2" name="tipo"> <input id=strConsDirecClaroCanje type=hidden value="<%=strConsDirecClaroCanje%>" name=strConsDirecClaroCanje>
			<input type="hidden" value="2" name="tipo"> <input id=strConsDistritoClaroCanje type=hidden value="<%=strConsDistritoClaroCanje%>" name=strConsDistritoClaroCanje>
			<input type="hidden" value="2" name="tipo"> <input id=strConsUrbClaroCanje type=hidden value="<%=strConsUrbClaroCanje%>" name=strConsUrbClaroCanje>
			<input type="hidden" value="2" name="tipo"> <input id=CONS_TIPO_OPE_DEVO type=hidden value="<%=CONS_TIPO_OPE_DEVO%>" name=CONS_TIPO_OPE_DEVO>
			<input type="hidden" value="2" name="tipo">
			<table width="240" border="0" cellspacing="0" cellpadding="0">
				<% 
            '*** VARIABLES LOCALES ***
			Dim dr as System.Data.datarow
			Dim descripMotivo as String
            Dim docSap As String
            Dim msgErr As String = ""
            Dim codMov As String 
            Dim gStrTipoOpe As String = CONS_TIPO_OPE_DEVO 'PROY-23700-IDEA-29415 - ACB
            Dim i As Integer = 0
            Dim Reimpresion as string
            
            docSap = Request.QueryString("codRefer")
			Reimpresion = Request.QueryString("Reimpresion")
            
            Dim dtResult As System.Data.DataTable
            Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
            Dim objConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
            Dim dtResultCanje As System.Data.DataTable
            
            Dim dtResultMotivo As System.Data.DataTable     
            
            Dim dsPedido as System.Data.Dataset      
            
            
            dsPedido = objConsultaMsSap.ConsultaPedido(docSap,"","")
            dtResult = objConsultaMsSap.ConsultaImpresionCanje(docSap,"","")
            
            
            '----------------------------------------------------------------------------------------------------------------
            '--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
            '----------------------------------------------------------------------------------------------------------------   
            'dtResultCanje = objConsultaMsSap.ConsultaMotivoxPedidoSap(docSap,"","")
            
            if dtResult.Rows.Count > 1 then
				dtResultCanje = objConsultaMsSap.ConsultaDevolucion(docSap,"","")
			else
            dtResultCanje = objConsultaMsSap.ConsultaMotivoxPedidoSap(docSap,"","")
            end if            
            '----------------------------------------------------------------------------------------------------------------
            '--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --
            '----------------------------------------------------------------------------------------------------------------   
            
			CodVendedor = dsPedido.Tables(0).Rows(0).Item("VENDEDOR")
			codMov = dtResultCanje.Rows(0).Item("CANJV_MOTIVO")
			
			dtResultMotivo = objConsultaPvu.ConsultaMotivosCanje(gStrTipoOpe,"",1,"","")
			
			
			for each dr in dtResultMotivo.rows
			
			 if dtResultMotivo.Rows(i).Item("MOTI_CODIGO") = codMov then
				descripMotivo = dtResultMotivo.Rows(i).Item("MOTI_DESCRIP")
			    
			 End If
			 i+=1
			next
            
            If Len(Trim(CodVendedor)) > 0 Then
		
                dtVendedor = obUbigeo.ConsultaVendedor(CodVendedor.PadLeft(10, "0"), "", 1)
						
                'Nomvendedor = IIf(IsDBNull(dtVendedor.Rows(0).Item("VEND_NOMBRE")), "", dtVendedor.Rows(0).Item("VEND_NOMBRE"))
                If (Not dtVendedor Is Nothing) AndAlso (dtVendedor.Rows.Count > 0) Then
                    If IsDBNull(dtVendedor.Rows(0).Item("VEND_NOMBRE")) Then
                        NomVendedor = ""
                    Else
                        NomVendedor = dtVendedor.Rows(0).Item("VEND_NOMBRE")
                    End If
                Else
                    CodVendedor = ""
                    NomVendedor = ""
                End If
            Else
                CodVendedor = ""
                NomVendedor = ""
            End If
            
            CodVendedor = CodVendedor.Replace("00000","")
            
            '*** FIN VARIABLES LOCALES *** 
        
            '******************************************************
	
            '*****lee parametros
            'docSap = Request.QueryString("codRefer")
	
            'dsResult = objConsultaMsSap.ConsultaImpresionCanje("3972","","")

         

        %>
				<!--Inicio de Cabecera-->
				<%
            cadenaP = cadenaP & FncImprCabecera(dtResult)
        %>
				<!--Fin de Cabecera-->
				<!--Inicio de Cliente-->
				<%
            cadenaP = cadenaP & FncImprCliente(dtResult)
        %>
				<!--Fin de Cliente-->
				<!--Inicio de Detalle de Ventas-->
				<%
            cadenaP = cadenaP & FncImprDetVentas(dtResult,descripMotivo)
        %>
				<!--Fin de Detalle de Ventas-->
				<!--Inicio Totales-->
				<%
            cadenaP = cadenaP & FncImprTotales(dtResult)
        %>
				<!--Fin Totales-->
				<!--Inicio Comentarios-->
				<%
            cadenaP = cadenaP & FncImprVendedores(dtResult,Reimpresion)
        %>
				<!--Fin Comentarios-->
				<!--Inicio Mensaje de Aceptacion de Condiciones-->
				<%
            cadenaP = cadenaP & FncImprMensajeAcepGarantia()
        %>
				<!--Fin Mensaje de Aceptacion de Condiciones-->
				<!--Inicio Impresion-->
				<%
            Response.Write(cadenaP)
        %>
				<!--Fin Impresion-->
			</table>
		</form>
	</body>
</HTML>
