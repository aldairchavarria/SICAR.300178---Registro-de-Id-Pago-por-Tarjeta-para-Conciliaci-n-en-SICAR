<%@ Page Language="vb" AutoEventWireup="false" Codepage="1252"  Codebehind="docRecaudacion.aspx.vb" Inherits="SisCajas.docRecaudacion"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>docRecaudacion</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../estilos/est_General.css" type="text/css" rel="stylesheet">
		<style type="text/css"> @page { margin : 0cm; }
		</style>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="jscript">
		function PrintDocument()
		{
			if(document.frmPrincipal.hidFlagConRedondeo.value == "1"){
				document.getElementById("trRedondeo").style.display ='';
			}
			print();
		}
		</script>
	</HEAD>
	<body onload="PrintDocument();" scroll="yes">
		<form id="frmPrincipal" method="post" runat="server">
			<!--	<table>
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
						<td><%=Me.NomEmpresa%></td>
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
									<td width="110">IDENTIFICADOR</td>
									<td><%=Me.NumeroDocumentoDeudor%></td>
								</tr>
								<tr>
									<td>TITULAR</td>
									<td><%=Me.NombreCliente%></td>
								</tr>
								<tr>
									<td>TRANSACCION</td>
									<td><%=Me.NumeroDeudaSAP%></td>
								</tr>
								<tr>
									<td style="HEIGHT: 14px">IMPORTE</td>
									<td style="HEIGHT: 14px"><%= Me.TipoMoneda%>&nbsp;
										<%=Me.Importe.tostring("N2")%>
									</td>
								</tr>
								<tr>
									<td colspan="2" height="10"></td>
								</tr>
								<tr>
									<td colspan="2">FORMAS DE PAGO</td>
								</tr>
								<%
								
								Dim ctePAGO_VIA_PAGO, _
									ctePAGO_IMPORTE_PAGADO, _
									ctePAGO_NRO_CHEQUE

								ctePAGO_VIA_PAGO = 6
								ctePAGO_IMPORTE_PAGADO = 3
								ctePAGO_NRO_CHEQUE = 4
								
								dim drFila as System.Data.DataRow
								for each drFila in dsDeuda.Tables(2).rows								
								%>
								<tr>
									<td colspan="2">
										<table width="150" class="Arial12b">
											<tr>
												<td width="10">&nbsp;</td>
												<td><%=drFila(ctePAGO_VIA_PAGO) & "  " & drFila(ctePAGO_NRO_CHEQUE)%>
												</td>
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
									<td><%=Me.CodCajero%>
										-
										<%=Me.NombreCajero%>
									</td>
								</tr>
								<tr>
									<td>PDV</td>
									<td><%=Me.CodPuntoDeVenta%>
										-
										<%=Me.PuntoDeVenta%> <br>
										<%=Me.SubOficina%>
										
									</td>
								</tr>
								<tr>
									<td>FECHA - HORA</td>
									<td><%=Me.FechaDeudaSAP%>&nbsp;<%=GetHour(Me.HoraDeudaSAP)%></td>
								</tr>
								<tr>
									<td colspan="2" height="10"></td>
								</tr>
								<tr>
									<td colspan="2">RECIBOS</td>
								</tr>
								<%
								Dim arrRecibo
								Dim cteRECIBO_POSICION, _
									cteRECIBO_TIPO_DOC_RECAUD, _
									cteRECIBO_NRO_DOC_RECAUD, _
									cteRECIBO_MONEDA_DOCUM, _
									cteRECIBO_IMPORTE_RECIBO, _
									cteRECIBO_IMPORTE_PAGADO, _
									cteRECIBO_NRO_COBRANZA, _
									cteRECIBO_NRO_OPE_ACREE, _
									cteRECIBO_FECHA_EMISION, _
									cteRECIBO_FECHA_PAGO, _
									cteRECIBO_NRO_TRACE_ANUL, _
									cteRECIBO_NRO_TRACE_PAGO, _
									cteRECIBO_DESCR_RECIBO

								cteRECIBO_POSICION = 1
								cteRECIBO_TIPO_DOC_RECAUD = 2
								cteRECIBO_NRO_DOC_RECAUD = 3
								cteRECIBO_MONEDA_DOCUM = 4
								cteRECIBO_IMPORTE_RECIBO = 5
								cteRECIBO_IMPORTE_PAGADO = 6
								cteRECIBO_NRO_COBRANZA = 7
								cteRECIBO_NRO_OPE_ACREE = 8
								cteRECIBO_FECHA_EMISION = 9
								cteRECIBO_FECHA_PAGO = 10
								cteRECIBO_NRO_TRACE_ANUL = 11
								cteRECIBO_NRO_TRACE_PAGO = 12
								cteRECIBO_DESCR_RECIBO = 13

								
								for each drFila in dsDeuda.Tables(1).rows	
								'for i = 0 to ubound(Me.Recibos)
								'	if len(trim(Me.Recibos(i))) > 0 then
								'		arrRecibo = split(Me.Recibos(i), ";")
								%>
								<tr>
									<td colspan="2">
										<table width="150" class="Arial12b">
											<tr>
												<td width="10">&nbsp;</td>
												<td><%=drFila(cteRECIBO_NRO_DOC_RECAUD)%>&nbsp;&nbsp;<%=drFila(cteRECIBO_DESCR_RECIBO)%>&nbsp;&nbsp;<%=drFila(cteRECIBO_FECHA_EMISION)%></td>
											</tr>
										</table>
									</td>
								</tr>
								<%	'end if
								next %>
							</table>
						</td>
					</tr>
					<tr> <!-- Para incluir TC -->
						<td align="center">
							TC: S/
							<%=Me.dblTipCam%>
						</td>
					</tr>
					<tr id="trRedondeo" style="DISPLAY:none"> <!-- Redondeo hacia arriba -->
						<td>
							<table width="200" class="Arial12b" border="0">
								<tr>
									<td><%=Me.strDescRedondeoSolicitado%>										
										<%=Me.ValorRedondeoSolicitado.tostring("N2") %>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr> <!-- REIMPRESION -->
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
			<!--	</td>
				</tr>
			</table> --> <!-- CAP -->
			<input type="hidden" id="hidFlagConRedondeo" runat="server">
			<input type="hidden" id="hidTipoDocumento" runat="server">
		</form>
	</body>
</HTML>
