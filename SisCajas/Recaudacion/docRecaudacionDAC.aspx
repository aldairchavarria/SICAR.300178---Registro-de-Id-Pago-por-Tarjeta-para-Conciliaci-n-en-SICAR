<%@ Page Language="vb" AutoEventWireup="false" Codepage="1252"  Codebehind="docRecaudacionDAC.aspx.vb" Inherits="SisCajas.docRecaudacionDAC"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>docRecaudacion</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<LINK href="../estilos/est_General.css" type="text/css" rel="stylesheet">
		<style type="text/css"> @page { margin : 0cm; }
			
			.tabla_borde
			{
				BORDER-RIGHT: #336699 1px solid;
				BORDER-TOP: #336699 1px solid;
				FONT-SIZE: 12px;
				BORDER-LEFT: #336699 1px solid;
				COLOR: #ff0000;
				BORDER-BOTTOM: #336699 1px solid;
				FONT-FAMILY: Arial;
				TEXT-DECORATION: none
			}
			
			.Boton
			{
				border-right: #95b7f3 1px solid;
				border-top: #95b7f3 1px solid;
				font-weight: bold;
				font-size: 10px;
				border-left: #95b7f3 1px solid;
				cursor: hand;
				color: #003399;
				border-bottom: #95b7f3 1px solid;
				font-family: Verdana;
				background-color: white;
				text-align: center;
				TEXT-DECORATION: none;
				BACKGROUND-REPEAT: repeat-x;
				background-color: #e9f2fe;
				/*BACKGROUND-IMAGE: url(../images/toolgrad.gif); */
				border-color :#95b7f3	
			}
			
			.BotonResaltado
			{
				
				border-right: #95b7f3 1px solid;
				border-top: #95b7f3 1px solid;
				font-weight: bold;
				font-size: 10px;
				border-left: #95b7f3 1px solid;
				cursor: hand;
				color: #003399;
				border-bottom: #95b7f3 1px solid;
				font-family: Verdana;
				background-color: white;
				text-align: center;
				TEXT-DECORATION: none;
				BACKGROUND-REPEAT: repeat-x; 	
				border-color :#95b7f3
				
			}
		
		</style>
		<script language="jscript">
		function PrintDocument()
		{
			window.parent.Imprimir();
		}
		function imprimir_dac(){
			divBotones.style.visibility = "HIDDEN";
			window.print();
			divBotones.style.visibility = "VISIBLE";
		}
		</script>
	</HEAD>
	<body onload="PrintDocument();">
		<form id="frmPrincipal" method="post" runat="server">
		<div id="divBotones">
					<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="400" align="center">
						<tr>
							<td align="center"><input class="Boton" id="btnImprimir" onmouseover="this.className='BotonResaltado'" onclick="javascript:imprimir_dac();"
									onmouseout="this.className='Boton'" size="15" value="Imprimir" name="btnAgregar" runat="server" width="50">
							</td>
							<td align="center"><input class="Boton" id="btnCancelar" onmouseover="this.className='BotonResaltado'" onclick="window.close();"
									onmouseout="this.className='Boton'" size="15" value="Cerrar" name="btnCancelar" runat="server" width="50">
							</td>
						</tr>
					</table>
				</div>
			<!--<table>
				<tr>
					<td width="800">&nbsp;</td>
					<td> -->
			<table border="0" class="Arial12b">
				<thead>
					<tr>
						<!-- td>CONSTANCIA DE PAGO - RECAUDACIONES 75</td -->
						<td height="75"></td>
					</tr>
					<tr>
						<td><%=ConfigurationSettings.AppSettings("gStrRazonSocial")%></td>
					</tr>
					<tr>
						<td colspan="3" height="10"></td>
					</tr>
				</thead>
				<tbody>
					<tr>
						<td>
							<table width="150" class="Arial12b" border="0">
								<tr>
									<td width="100">TITULAR</td>
									<td><%=request.Item("Dealer")%></td>
								</tr>
								<tr>
									<td>IMPORTE</td>
									<td>S/
										<%=request.Item("MontoTotalPagado")%>
									</td>
								</tr>
								<tr>
									<td colspan="2" height="10"></td>
								</tr>
								<tr>
									<td colspan="2">FORMAS DE PAGO</td>
								</tr>
								<%
								dim arrFormas() as string
								dim arrLinFormas() as string
								dim i as integer
								arrFormas = split(request.item("strTrama"),"|")
								
								for i=0 to ubound(arrFormas)
								  arrLinFormas = split(arrFormas(i),";")
								'for each drFila in dsDeuda.Tables(2).rows								
								%>
								<tr>
									<td colspan="2">
										<table width="150" class="Arial12b">
											<tr>
												<td width="10">&nbsp;</td>
												<td><%=arrLinFormas(0) & "  " & arrLinFormas(1)%></td>
											</tr>
										</table>
									</td>
								</tr>
								<%
								
								next
								%>
								<tr>
									<td colspan="2" height="10"></td>
								</tr>
								<tr>
									<td>CAJERO</td>
									<td>
										<%
							'PROY-26366-IDEA-34247 FASE 1 - INICIO
							Dim Cajero01 as String 
							Dim Cajero as String 
							Cajero01= request.item("Cajero")
							Cajero = cstr(Session("USUARIO")).padleft(10,"0") & " - " & Session("NOMBRE_COMPLETO")
							if Cajero01="" Then
							  Response.Write(Cajero)
							  else
							  Response.Write(Cajero01)
							End If
							'PROY-26366-IDEA-34247 FASE 1 - FIN
									%>
						
									</td>
								</tr>
								<tr>
									<td>PDV</td>
									<td>
										<%
							'PROY-26366-IDEA-34247 FASE 1 - INICIO
							Dim PDV01 as String 
							Dim PDV as String 
							PDV01= request.item("PDV")
							PDV = Session("ALMACEN") & " - " & Session("OFICINA")
							if PDV01="" Then
							  Response.Write(PDV)
							  else
							  Response.Write(PDV01)
							End If
							'PROY-26366-IDEA-34247 FASE 1 - FIN
									%>
									</td>
								</tr>
								<tr>
									<td>FECHA&nbsp;-&nbsp;HORA</td>
									<td>
										<%
							'PROY-26366-IDEA-34247 FASE 1 - INICIO
							Dim Fecha01 as String 
							Dim Fecha02 as String 
							Fecha01= request.item("FechaRegistro")
							Fecha02 = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000") & " " & format(Now.Hour,"00") & ":" & format(Now.Minute,"00")  & ":" & format(Now.Second,"00")
							if Fecha01="" Then
							  Response.Write(Fecha02)
							  else
							  Response.Write(Fecha01)
							End If
							'PROY-26366-IDEA-34247 FASE 1 - FIN
									%>

									</td>
								</tr>
								<tr>
									<td colspan="2" height="10"></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr> <!-- Para incluir TC -->
						<td align="center">
							TC: S/
							<%
							  'dim objSAP as new SAP_SIC_Pagos.clsPagos
							  Dim objOffline As New COM_SIC_OffLine.clsOffline
							  dim dblTipCam as double
							  dblTipCam = objOffline.Obtener_TipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"))
							  'dblTipCam = objSAP.Get_TipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"))
							  Response.Write(dblTipCam.ToString("N2"))
							%>
						</td>
					</tr>
					<tr> <!-- Para incluir TC -->
						<td align="center">
							<%
							   'PROY-26366-IDEA-34247 FASE 1 - INICIO
							Dim reimpre as String 
							reimpre= request.item("Reimpresion")
							if reimpre="1" Then
							  Response.Write("***REIMPRESION***")
							End If
							   'PROY-26366-IDEA-34247 FASE 1 - FIN
							%>
						</td>
					</tr>
				</tbody>
			</table>
			<!--</td>
				</tr>
			</table> --> <!-- CAP -->
		</form>
	</body>
</HTML>
