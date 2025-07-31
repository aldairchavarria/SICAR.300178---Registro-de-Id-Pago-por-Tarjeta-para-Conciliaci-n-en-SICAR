<%@ Page Language="vb" aspcompat="true" AutoEventWireup="false" Codebehind="rep_Diario.aspx.vb" Inherits="SisCajas.rep_Diario" %>
<HTML>
	<HEAD>
		<title>Aplicativo TIM</title>
		<meta http-equiv="Pragma" content="no-cache">
		<META http-equiv="Expires" content="Mon, 06 Jan 1990 00:00:01 GMT">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
			<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
			<script language="JavaScript">
<!--

function f_Excel(){
	document.frmPrincipal.action = "ExcelCaja.asp";
	document.frmPrincipal.submit();
};
//-->
			</script>
			<meta http-equiv="pragma" content="no-cache">
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<form id="frmPrincipal" name="frmPrincipal" method="post" target="_blank">
			<input id=strFecha type=hidden value="<%=strFecha%>" name=strFecha> <input type="hidden" value="2" name="tipo">
			<table cellSpacing="0" cellPadding="0" width="975" border="0">
				<tr>
					<!--<td width="170" valign="top"></td>-->
					<!--<td width="10" valign="top">&nbsp;</td>-->
					<td vAlign="top" width="820">
						<table height="14" cellSpacing="0" cellPadding="0" width="820" border="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="790" align="center" border="0" name="Contenedor">
							<tr>
								<td align="center">
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="790" align="center"
										border="1">
										<tr>
											<td align="center">
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td class="TituloRConsulta" align="center" width="98%" height="32">Detalle de 
															Diario Electrónico</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="790" align="center" border="0">
													<tr>
														<td width="98%">
															<table cellSpacing="0" cellPadding="0" width="770" border="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td height="18">
																		<table cellSpacing="1" cellPadding="0" border="0">
																			<tr class="Arial12br">
																				<td width="250">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Datos 
																						Generales</b></td>
																			</tr>
																		</table>
																	</td>
																</tr>
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table cellSpacing="2" cellPadding="0" width="100%" border="0">
																			<tr>
																				<td width="30">&nbsp;</td>
																				<td class="Arial12b" width="166">&nbsp;&nbsp;&nbsp;Fecha :</td>
																				<td class="Arial12b" width="170"><input class=clsInputDisable id=txtNombres4 type=text 
																					maxLength=15 size=30 value="<%=sFechaActual%>" 
																					name=txtNombres></td>
																				<td width="30">&nbsp;</td>
																				<td class="Arial12b" width="120">&nbsp;&nbsp;&nbsp;Hora :</td>
																				<td class="Arial12b" width="220"><input 
																					class=clsInputDisable id=txtNombres4 readOnly type=text maxLength=15 size=30 
																					value="<%=sHoraActual%>" name=txtNombres 
																				></td>
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
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="790" align="center"
										border="1">
										<tr>
											<td align="center">
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td class="TituloRConsulta" align="center" width="98%" height="32">Datos</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="790" align="center" border="0">
													<tr>
														<td width="98%">
															<table cellSpacing="0" cellPadding="0" width="770" border="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td height="18">
																		<div class="frame2" style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 770px; BORDER-BOTTOM: 1px; HEIGHT: 300px; TEXT-ALIGN: center">
																			<table class="tabla_interna_borde1" cellSpacing="1" cellPadding="1" width="1800">
																				<TR class="Arial12B" height="21">
																				</TR>
																				<TR height="1">
																				</TR>
																			</table>
																			<asp:datagrid id="dgDiarioE" Width="100%" Height="30px" CellSpacing="1" AutoGenerateColumns="False"
																				CssClass="Arial11B" BorderColor="White" runat="server">
																				<AlternatingItemStyle BackColor="#DDDEE2"></AlternatingItemStyle>
																				<ItemStyle BackColor="#E9EBEE"></ItemStyle>
																				<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"
																					VerticalAlign="Top"></HeaderStyle>
																				<Columns>
																					<asp:BoundColumn DataField="COD_CAJERO" HeaderText="Cajero">
																						<HeaderStyle Wrap="False" Width="60px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="TIPO_OPER" HeaderText="Tipo Operaci&#243;n">
																						<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="TIP_DOCCLI" HeaderText="Tipo Doc. Cliente">
																						<HeaderStyle Wrap="False" Width="150px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="NUM_DOC_CLI" HeaderText="Nro Doc. Cliente">
																						<HeaderStyle Wrap="False" Width="150px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="TIP_DOCVTA" HeaderText="Tipo Doc. Venta">
																						<HeaderStyle Wrap="False" Width="120px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="NUM_DOCSAP" HeaderText="Nro Doc. SAP">
																						<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="NUM_REF" HeaderText="Nro Referencia">
																						<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="FECHA_HORA" HeaderText="Fecha Operaci&#243;n">
																						<HeaderStyle Wrap="False" Width="160px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="IMPORTE_PAGADO_PEN" HeaderText="Importe Pagado PEN">
																						<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="IMPORTE_PAGADO_USD" HeaderText="Importe Pagado USD">
																						<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="VUELTO" HeaderText="Vuelto">
																						<HeaderStyle Wrap="False" Width="85px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="SUB_TOT" HeaderText="SubTotal Doc.">
																						<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="IMPUESTO" HeaderText="Impuesto Doc.">
																						<HeaderStyle Wrap="False" Width="80px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="TOTAL" HeaderText="Total Doc.">
																						<HeaderStyle Wrap="False" Width="80px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="COD_ARTICULO" HeaderText="Art&#237;culo">
																						<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="NUM_SERIE" HeaderText="Nro Serie">
																						<HeaderStyle Wrap="False" Width="60px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="NUM_TELEF" HeaderText="Telefono">
																						<HeaderStyle Wrap="False" Width="80px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="CANTIDAD" HeaderText="Cantidad">
																						<HeaderStyle Wrap="False" Width="60px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="SUBTOTAL" HeaderText="SubTotal Art.">
																						<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="IMPUESTOART" HeaderText="Impuesto Art.">
																						<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="TOTALART" HeaderText="Total Art.">
																						<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="COD_VIA" HeaderText="V&#237;a">
																						<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="NUM_TARJETA" HeaderText="Nro Tarjeta">
																						<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="MONTO" HeaderText="Monto">
																						<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
																					</asp:BoundColumn>
																				</Columns>
																			</asp:datagrid></div>
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
									<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="360" align="center"
										border="1">
										<tr>
											<td>
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td align="center"><input class="BotonOptm" style="WIDTH: 100px" onclick="javascript:f_Exportar();" type="button"
																value="Exportar Excel" name="btnBuscar">&nbsp;&nbsp;
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<iframe id="ifraExcel" style="DISPLAY: none"></iframe>
					</td>
				</tr>
			</table>
			<script language="JavaScript" type="text/javascript">
var esNavegador, esIExplorer;

esNavegador = (navigator.appName == "Netscape") ? true : false;
esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

if (esIExplorer) {
}

			</script>
		</form>
		<script language="JavaScript">
	function f_Exportar()
	{
		//alert("rep_DiarioExcel.aspx?pfecha="+frmPrincipal.strFecha.value);
		
		document.all.ifraExcel.src="rep_DiarioExcel.aspx?pfecha="+frmPrincipal.strFecha.value;
		//document.frmTmp.action = '<%=strRuta%>/reportes/toExcel.aspx?tipo=2&Individual=0&strFecha=<%=strFecha%>';
		//document.frmTmp.submit();
	}

		</script>
		<form name="frmTmp" action="" method="post" target="_blank">
		</form>
	</body>
</HTML>
