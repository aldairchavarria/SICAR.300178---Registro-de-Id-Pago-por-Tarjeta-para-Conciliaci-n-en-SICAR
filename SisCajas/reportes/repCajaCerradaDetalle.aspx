<%@ Page Language="vb" AutoEventWireup="false" Codebehind="repCajaCerradaDetalle.aspx.vb" Inherits="SisCajas.repCajaCerradaDetalle" %>
<HTML>
	<HEAD>
		<title>Aplicativo TIM</title>
		<meta content="no-cache" http-equiv="Pragma">
		<META content="Mon, 06 Jan 1990 00:00:01 GMT" http-equiv="Expires">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
			<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
			<script language="JavaScript">
			
				function inicializar()
				{
					var url = location.href;
					var index = url.indexOf("?");
					index = url.indexOf('pfecha',index) + 'pfecha'.length;
					if (url.charAt(index) == "="){
						var result = url.indexOf("&amp;",index);						
						if (result == -1)
						{
							result=url.length;
						}
						var parametro = url.substring(index + 1,result);
					}	
					//cargarFormulario(parametro);
				}
				
				function cargarFormulario(parametro)
				{					
					var fi = parametro.substr(6,2) + '/'+ parametro.substr(4,2) + '/'+ parametro.substr(0,4);
					var ff = parametro.substr(14,2) + '/'+ parametro.substr(12,2) + '/'+ parametro.substr(8,4);
					
					frmPrincipal.txtFecInicio.value = fi;
					frmPrincipal.txtFecFinal.value = ff;
					
					var Feci = document.getElementById("hdnFecInicio");
					var Fecf = document.getElementById("hdnFecFinal");				
					
					Feci.value = fi.toString();
					Fecf.value = ff.toString();
					
					alert('Feci : ' + Feci.value.toString() + ' - Fecf : ' + Fecf.value.toString());
				}
				
			</script>
			<meta content="no-cache" http-equiv="pragma">
	</HEAD>
	<body onload="javascript:inicializar();" leftMargin="0" topMargin="0" marginheight="0"
		marginwidth="0">
		<form id="frmPrincipal" method="post" runat="server">
			<table border="0" cellSpacing="0" cellPadding="0" width="975">
				<tr>
					<!--<td width="170" valign="top"></td>-->
					<!--<td width="10" valign="top">&nbsp;</td>-->
					<td vAlign="top" width="820">
						<table border="0" cellSpacing="0" cellPadding="0" width="820" height="14" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table border="0" cellSpacing="0" cellPadding="0" width="790" align="center" name="Contenedor">
							<tr>
								<td align="center">
									<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="790" align="center">
										<tr>
											<td align="center">
												<table class="Arial10B" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
													<tr>
														<td height="4" width="10" border="0"></td>
														<td class="TituloRConsulta" height="32" width="98%" align="center">Reporte de cajas 
															cerradas y pendientes de cerrar</td>
														<td height="32" vAlign="top" width="14"></td>
													</tr>
												</table>
												<table border="0" cellSpacing="0" cellPadding="0" width="790" align="center">
													<tr>
														<td width="98%">
															<table border="0" cellSpacing="0" cellPadding="0" width="770">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table border="0" cellSpacing="2" cellPadding="0" width="100%">
																			<tr>
																				<td width="30">&nbsp;</td>
																				<td class="Arial12b" width="166">&nbsp;&nbsp;&nbsp;Fec. Inicio:</td>
																				<td class="Arial12b" width="170">&nbsp;
																					<asp:textbox id="txtFecInicio" runat="server" CssClass="clsInputDisable" Width="145px"></asp:textbox></td>
																				<td width="30">&nbsp;</td>
																				<td class="Arial12b" width="166">&nbsp;&nbsp;&nbsp;Fec. Final :</td>
																				<td class="Arial12b" width="170">&nbsp;
																					<asp:textbox id="txtFecFinal" runat="server" CssClass="clsInputDisable" Width="145px"></asp:textbox></td>
																			</tr>
																		</table>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<br>
									<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="790" align="center">
										<tr>
											<td align="center">
												<table border="0" cellSpacing="0" cellPadding="0" width="790" align="center">
													<tr>
														<td width="98%">
															<table border="0" cellSpacing="0" cellPadding="0" width="770">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td height="18">
																		<div style="Z-INDEX: 0; BORDER-BOTTOM: 1px; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 770px; HEIGHT: 300px; BORDER-TOP: 1px; BORDER-RIGHT: 1px"
																			class="frame2">
																			<table class="tabla_interna_borde1" cellSpacing="1" cellPadding="1" width="1800">
																				<TR class="Arial12B" height="21">
																				</TR>
																				<TR height="1">
																				</TR>
																			</table>
																			<asp:datagrid id="DgCajaCerrada" runat="server" CssClass="Arial11B" Width="100%" Height="30px"
																				CellSpacing="1" AutoGenerateColumns="False" BorderColor="White">
																				<AlternatingItemStyle BackColor="#DDDEE2"></AlternatingItemStyle>
																				<ItemStyle BackColor="#E9EBEE"></ItemStyle>
																				<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"
																					VerticalAlign="Top"></HeaderStyle>
																				<Columns>
																					<asp:BoundColumn DataField="OFICINA_DE_VENTA" HeaderText="CAC">
																						<HeaderStyle Wrap="False" Width="50px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="CAJERO" HeaderText="Cajero">
																						<HeaderStyle Wrap="False" Width="70px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="CAJA" HeaderText="Caja Asignada">
																						<HeaderStyle Wrap="False" Width="70px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="FECHA_ASIGNACION" HeaderText="Fecha Asignada">
																						<HeaderStyle Wrap="False" Width="70px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="HORA_ASIGNACION" HeaderText="Hora Asignada">
																						<HeaderStyle Wrap="False" Width="50px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="ESTADO" HeaderText="Estado">
																						<HeaderStyle Wrap="False" Width="80px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="FECHA_CIERRE" HeaderText="Fecha Cierre">
																						<HeaderStyle Wrap="False" Width="70px"></HeaderStyle>
																					</asp:BoundColumn>
																				</Columns>
																			</asp:datagrid></div>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
												<asp:button style="Z-INDEX: 0" id="btnRegresar" runat="server" CssClass="BotonOptm" Width="100"
													Text="Regresar"></asp:button></td>
										</tr>
									</table>
									<br>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="divPie"><INPUT id="hdnFecInicio" type="hidden" name="hdnFecInicio" runat="server">
				<INPUT id="hdnFecFinal" type="hidden" name="hdnFecFinal" runat="server"> <INPUT id="hdnPuntoDeVenta" type="hidden" name="hdnPuntoDeVenta" runat="server">
				<INPUT id="hidIdentificador" type="hidden" name="hidIdentificador" runat="server">
			</div>
		</form>
		<form method="post" name="frmTmp" action="" target="_blank">
		</form>
	</body>
</HTML>
