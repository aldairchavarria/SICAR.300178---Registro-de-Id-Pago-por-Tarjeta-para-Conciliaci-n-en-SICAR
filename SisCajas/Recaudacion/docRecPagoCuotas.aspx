<%@ Page Language="vb" AutoEventWireup="false" Codepage="1252"  Codebehind="docRecPagoCuotas.aspx.vb" Inherits="SisCajas.docRecPagoCuotas"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>docRecaudacion</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<style type="text/css">@page {margin: 0cm; }
			
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
		
		function Imp_cuotas()
		{
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
							<td align="center"><input class="Boton" id="btnImprimir" onmouseover="this.className='BotonResaltado'" onclick="javascript:Imp_cuotas();"
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
			<table class="Arial12b" border="0">
				<thead>
					<tr>
						<!-- td>CONSTANCIA DE PAGO - RECAUDACIONES 75</td -->
						<!--<td height="75"></td>--></tr>
					<tr>
						<td><%=ConfigurationSettings.AppSettings("gStrRazonSocial")%>
						</td>
					</tr>
					<tr>
						<td colSpan="3" height="10"></td>
					</tr>
					<tr>
						<td colSpan="3" height="10">PAGO DE CUOTA PREPAGO</td>
					</tr>
				</thead>
				<tbody>
					<tr>
						<td>
							<table class="Arial12b" width="150" border="0">
								<tr>
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
								<% if Trim(request.Item("strTelefono")) <> "" then %>
								<tr>
									<td>TELEFONO:</td>
									<td><%=request.Item("strTelefono")%>
									</td>
								</tr>
								<%   dim dblTotal as Double			%>
								<%   dim objCuotas as new SAP_SIC_Recaudacion.clsRecaudacion   %>
								<%   dim objCuotasSans as new NEGOCIO_SIC_SANS.SansNegocio   %>
								<%   dim dsResult as System.Data.DataSet  %>
								<%--   dsResult = objCuotas.Get_DatosCuotas(request.Item("strTelefono"),"","","",0,0,dblTotal) --%>
								<%   dsResult = objCuotasSans.Get_DatosCuotas(request.Item("strTelefono"),"","","",0,0,dblTotal,"","") %>
								<tr>
									<td>MONTO TOTAL DEL DOCUMENTO:</td>
									<td>S/
										<%=FormatNumber(dblTotal,2)%>
									</td>
								</tr>
								<%  dim dsResult2 as System.Data.DataSet   %>
								<%--  dsResult2 = objCuotas.Get_VentaxTelefono(request.Item("strTelefono")) --%>
								<%  dsResult2 = objCuotasSans.Get_VentaxTelefono(request.Item("strTelefono"), "", "") %>
								<tr>
									<td>MATERIAL:
									</td>
									<td>
									<% if Not dsResult2 Is Nothing  then %>
									<%=dsResult2.Tables(0).Rows(0).Item("MAKTX_IMEI")%>
									<% end if  %>
									</td>
								</tr>
								<tr>
									<td>&nbsp;
									</td>
									<td>
									<% if Not dsResult2 Is Nothing  then %>
									<%=dsResult2.Tables(0).Rows(0).Item("SERNR_IMEI")%>
									<% end if  %>
									</td>
								</tr>
								<tr>
									<td>NUMERO DE SERIE
									</td>
									<td>
									<% if Not dsResult2 Is Nothing  then %>
									<%=dsResult2.Tables(0).Rows(0).Item("SERNR")%>
									<% end if  %>
									</td>
								</tr>
								<% end if  %>
								<tr>
									<td colSpan="2" height="10"></td>
								</tr>
								<tr>
									<td colSpan="2">FORMAS DE PAGO</td>
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
									<td colSpan="2">
										<table class="Arial12b" width="150">
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
									<td colSpan="2" height="10"></td>
								</tr>
								<tr>
									<td>CAJERO</td>
									<td><%=cstr(Session("USUARIO")).padleft(10,"0")%>
										&nbsp;-&nbsp;
										<%=Session("NOMBRE_COMPLETO")%>
									</td>
								</tr>
								<tr>
									<td>PDV</td>
									<td><%=Session("ALMACEN")%>&nbsp;-&nbsp;<%=Session("OFICINA")%>
									</td>
								</tr>
								<tr>
									<td>FECHA&nbsp;-&nbsp;HORA</td>
									<td><%=Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000")%>&nbsp;<%=format(Now.Hour,"00") & ":" & format(Now.Minute,"00")  & ":" & format(Now.Second,"00")%>
									</td>
								</tr>
								<tr>
									<td colSpan="2" height="10"></td>
								</tr>
							</table>
						</td>
					</tr>
					<% if Trim(request.Item("strTelefono")) <> "" then %>
					<tr id="trPagadas" runat="server">
						<td align="center">CUOTAS CANCELADAS
						</td>
					</tr>
					<tr>
						<td align="center"><asp:datagrid id="dgCanceladas" runat="server" AutoGenerateColumns="False" CssClass="Arial11b">
								<ItemStyle Font-Size="X-Small"></ItemStyle>
								<HeaderStyle Font-Size="X-Small"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="BKTXT" HeaderText="Cuota">
										<HeaderStyle Wrap="False" Width="55px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FECHA" HeaderText="Fec.Ven.">
										<HeaderStyle Wrap="False" Width="50px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MONTO" HeaderText="Monto">
										<HeaderStyle Wrap="False" Width="35px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FECHA_PAGO" HeaderText="Fec.Can.">
										<HeaderStyle Wrap="False" Width="50px"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></td>
					</tr>
					<tr>
						<td align="center">&nbsp;
						</td>
					</tr>
					<tr id="trCuotas" runat="server">
						<td align="center">CUOTAS PENDIENTES
						</td>
					</tr>
					<tr id="trCuotasTAB" runat="server">
						<td align="center"><asp:datagrid id="dgCuotas" runat="server" AutoGenerateColumns="False" CssClass="Arial11b">
								<ItemStyle Font-Size="X-Small"></ItemStyle>
								<HeaderStyle Font-Size="X-Small"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="BKTXT" HeaderText="Cuota">
										<HeaderStyle Wrap="False" Width="55px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
										<HeaderStyle Wrap="False" Width="50px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MONTO" HeaderText="Monto">
										<HeaderStyle Wrap="False" Width="35px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FECHA_PAGO" HeaderText="Fec.Can.">
										<HeaderStyle Wrap="False" Width="50px"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></td>
					</tr>
					<%end if%>
					<tr> <!-- Para incluir TC -->
						<td align="center">TC: S/
							<%
							  dim objSAP as new SAP_SIC_Pagos.clsPagos
							  dim dblTipCam as double
							  dblTipCam = objSAP.Get_TipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"))
							  Response.Write(dblTipCam.ToString("N3")) 'aotane 05.08.2013
							%>
						</td>
					</tr>					
				</tbody>
			</table>
			<!--</td>
				</tr>
			</table> --> <!-- CAP --></form>
	</body>
</HTML>
