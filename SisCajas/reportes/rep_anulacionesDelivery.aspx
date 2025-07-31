<%@ Page Language="vb" AutoEventWireup="false" Codebehind="rep_anulacionesDelivery.aspx.vb" Inherits="SisCajas.rep_anulacionesDelivery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>rep_anulacionesDelivery</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script>
			
		function validarPost(){
			var valor = document.getElementById("sValor").value;
			var fecha = document.getElementById("sFecha").value;
			
			if (valor == '' && fecha == ''){
				document.getElementById("tbReporte").style.display = "none";
				document.getElementById("tbExportar").style.display = "none";
			}else{
				if(valor == 0){
				  alert('No se encontraron resultados para la búsqueda indicada. Intente con otros filtros.');
				  document.getElementById("tbReporte").style.display = "none";
				  document.getElementById("tbExportar").style.display = "none";
				}
				else{
				  document.getElementById("tbReporte").style.display = "block";
				  document.getElementById("tbExportar").style.display = "block";
				}
			}
		};
		
		function f_Exportar(){
			var fechaIni = document.getElementById("txtFechaIni").value;
			var fechaFin = document.getElementById("txtFechaFin").value;
			var PuntoVenta = document.getElementById("cboPdv").value;
			var MedioPago = document.getElementById("cboViaPago").value;
			var Monto = document.getElementById("txtMonto").value;
			var NroPedido = document.getElementById("txtNroPedido").value;
			var IdVenta = document.getElementById("txtValor").value;
			
			document.frmTmp.action = "rep_anulacionesDeliveryExcel.aspx?strFechaIni="+fechaIni+"&strFechaFin="+fechaFin+"&strPDV="+PuntoVenta+"&strMedioPago="+MedioPago+"&strMonto="+Monto+"&strNroPedido="+NroPedido+"&strIdVenta="+IdVenta;
			document.frmTmp.submit(); 
		};
				
		</script>
	</HEAD>
	<body onload="validarPost()" leftMargin="0" topMargin="0">
		<form id="frmPrincipal" method="post" runat="server">
			<input 
id=sValor value="<%=sValor%>" type=hidden name=sValor> <input id=sFecha value="<%=sFechaActual%>" type=hidden name=sFechaActual>
			<table id="tbFiltro" border="0" cellSpacing="0" cellPadding="0" width="820">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="820" align="center">
						<table border="0" cellSpacing="0" cellPadding="0" width="820">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="810">
							<tr>
								<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 5px"
									width="98%">
									<table border="0" cellSpacing="0" cellPadding="0" width="100%">
										<tr>
											<td class="TituloRConsulta" height="30" align="center">Reporte Anulaciones Delivery 
												por PDV</td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td>
												<table border="0" cellSpacing="2" cellPadding="0" align="center">
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha :</td>
														<td class="Arial12b" align="center">del: <input style="WIDTH: 80px; HEIGHT: 17px" id="txtFechaIni" class="clsInputEnable" maxLength="10"
																size="10" name="txtFechaIni" runat="server">&nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmPrincipal.txtFechaIni');"><IMG style="Z-INDEX: 0" border="0" src="../images/botones/btn_Calendario.gif"></A>
														</td>
														<td class="Arial12b" align="center">al: <input style="WIDTH: 80px; HEIGHT: 17px" id="txtFechaFin" class="clsInputEnable" maxLength="10"
																size="10" name="txtFechaFin" runat="server"> &nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmPrincipal.txtFechaFin');"><IMG style="Z-INDEX: 0" border="0" src="../images/botones/btn_Calendario.gif"></A>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Punto de Venta :</td>
														<td class="Arial12b" colSpan="3"><asp:dropdownlist id="cboPdv" runat="server" Width="280px" CssClass="clsSelectEnable"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Forma de Pago :</td>
														<td class="Arial12b" colSpan="3"><asp:dropdownlist id="cboViaPago" runat="server" Width="280px" CssClass="clsSelectEnable"></asp:dropdownlist></td>
													</tr>
												</table>
											</td>
											<td>
												<table>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Monto :</td>
														<td class="Arial12b" colSpan="3"><input style="WIDTH: 155px; HEIGHT: 17px" id="txtMonto" class="clsInputEnable" name="txtMonto"
																runat="server"></td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;N°de Pedido :</td>
														<td class="Arial12b" colSpan="3"><input style="WIDTH: 155px; HEIGHT: 17px" id="txtNroPedido" class="clsInputEnable" name="txtNroPedido"
																runat="server"></td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Id Venta :</td>
														<td class="Arial12b" colSpan="3"><input style="WIDTH: 155px; HEIGHT: 17px" id="txtValor" class="clsInputEnable" name="txtValor"
																runat="server">
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td><img border="0" src="" width="1" height="8"></td>
							</tr>
						</table>
						<br>
						<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="360">
							<tr>
								<td align="center"><input style="WIDTH: 100px" id="cmdBuscar" class="BotonOptm" value="Buscar" type="button"
										name="cmdBuscar" runat="server">&nbsp;&nbsp; <input style="WIDTH: 100px" class="BotonOptm" onclick="f_LimpiaControl();" value="Limpiar"
										type="button" name="btnLimpiar"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br>
			<br>
			<table style="DISPLAY: none" id="tbReporte" border="0" cellSpacing="0" cellPadding="0"
				width="820">
				<tr>
					<td vAlign="top" width="820"><br>
						<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="790" align="center">
							<tr>
								<td align="center">
									<table class="Arial10B" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td height="4" width="10" border="0"></td>
											<td class="TituloRConsulta" height="32" width="98%" align="center">Datos</td>
											<td height="32" vAlign="top" width="14"></td>
										</tr>
									</table>
								</td>
							</tr>
							<TR>
								<TD>
									<TABLE id="Table5" class="Arial12b" cellPadding="3">
										<tbody>
											<tr>
												<td style="PADDING-BOTTOM: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 0px">
													<div style="Z-INDEX: 0; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 800px; HEIGHT: 400px"
														class="frame2"><asp:datagrid style="Z-INDEX: 0" id="DGLista" runat="server" CssClass="Arial12b" AutoGenerateColumns="False"
															AllowPaging="true" PagerStyle-Mode="NumericPages" PageSize="5" EnableViewState="True">
															<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
															<ItemStyle CssClass="RowOdd"></ItemStyle>
															<HeaderStyle CssClass="Arial12b"></HeaderStyle>
															<PagerStyle Mode="NumericPages"></PagerStyle>
															<Columns>
																<asp:TemplateColumn SortExpression="FECHA_ANULACION" HeaderText="Fecha hora anulación">
																	<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="70px"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	<ItemTemplate>
																		<%# DataBinder.Eval(Container,"DataItem.FECHA_ANULACION") %>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn SortExpression="OFICINA" HeaderText="Oficina">
																	<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="70px"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	<ItemTemplate>
																		<%# DataBinder.Eval(Container,"DataItem.OFICINA") %>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn SortExpression="NRO_PEDIDO" HeaderText="N° Pedido">
																	<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="70px"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	<ItemTemplate>
																		<%# DataBinder.Eval(Container,"DataItem.NRO_PEDIDO") %>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn SortExpression="TIPO_DOCUMENTO" HeaderText="Tipo documento">
																	<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="70px"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	<ItemTemplate>
																		<%# DataBinder.Eval(Container,"DataItem.TIPO_DOCUMENTO") %>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn SortExpression="MODALIDAD_PAGO" HeaderText="Modalidad pago">
																	<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="70px"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	<ItemTemplate>
																		<%# DataBinder.Eval(Container,"DataItem.MODALIDAD_PAGO") %>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn SortExpression="MEDIO_PAGO" HeaderText="Medio pago">
																	<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="70px"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	<ItemTemplate>
																		<%# DataBinder.Eval(Container,"DataItem.MEDIO_PAGO") %>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn SortExpression="ID_VENTA" HeaderText="ID de venta">
																	<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="70px"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	<ItemTemplate>
																		<%# DataBinder.Eval(Container,"DataItem.ID_VENTA") %>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn SortExpression="MONTO_PEDIDO" HeaderText="Monto pedido">
																	<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="70px"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	<ItemTemplate>
																		<%# DataBinder.Eval(Container,"DataItem.MONTO_PEDIDO") %>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn SortExpression="MONTO_PAGADO" HeaderText="Monto pagado">
																	<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="70px"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	<ItemTemplate>
																		<%# DataBinder.Eval(Container,"DataItem.MONTO_PAGADO") %>
																	</ItemTemplate>
																</asp:TemplateColumn>
															</Columns>
														</asp:datagrid></div>
												</td>
											</tr>
										</tbody>
									</TABLE>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			</TD></TR></TABLE>
			<table style="DISPLAY: none" id="tbExportar" border="0" cellSpacing="0" cellPadding="0"
				width="800">
				<tr>
					<td>
						<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="4" width="360" align="center">
							<tr>
								<td>
									<table class="Arial10B" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td align="center"><input style="WIDTH: 100px" id="btnExportar" class="BotonOptm" onclick="javascript:f_Exportar();"
													value="Exportar Excel" type="button" name="btnExportar" runat="server">&nbsp;&nbsp;
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<form method="post" name="frmTmp" action="" target="_blank">
		</form>
	</body>
</HTML>
