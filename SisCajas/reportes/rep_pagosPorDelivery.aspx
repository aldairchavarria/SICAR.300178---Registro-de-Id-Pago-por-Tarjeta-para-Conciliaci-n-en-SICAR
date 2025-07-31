<%@ Page Language="vb" AutoEventWireup="false" Codebehind="rep_pagosPorDelivery.aspx.vb" Inherits="SisCajas.rep_pagosPorDelivery"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>rep_pagosPorDelivery</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script>
		
		function validarPost(){
			var fecha = document.getElementById("txtFechaActual").value;
			if(fecha != ""){
				document.getElementById("tbFiltro").style.display = "none";
				document.getElementById("tbReporte").style.display = "block";
				var sValor = document.getElementById("sValor").value;
				if (sValor != ""){
					document.getElementById("tbExportar").style.display = "block";
				}
				else{
					document.getElementById("tbExportar").style.display = "none";
				}
			}
			else{
				document.getElementById("tbFiltro").style.display = "block";
				document.getElementById("tbReporte").style.display = "none";
				document.getElementById("tbExportar").style.display = "none";
			}
		};
		
		function f_Exportar(){
			var fecha = document.getElementById("txtFecha").value;
			document.frmTmp.action = "rep_pagosPorDeliveryExcel.aspx?strFecha="+fecha;
			document.frmTmp.submit(); 
		};
		
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="validarPost()">
		<form id="frmPrincipal" method="post" name="frmPrincipal" runat="server">
			<input type=hidden name="sValor" id="sValor" value="<%=sValor%>">
			<div style="Z-INDEX: 1; POSITION: absolute; WIDTH: 100px" id="overDiv"></div>
			<input id="codOperacion" type="hidden" name="codOperacion"> <input id="txtCierre" type="hidden" name="txtCierre">
			<table id="tbFiltro" border="0" cellSpacing="0" cellPadding="0" width="800">
				<tr>
					<td vAlign="top" width="800">
						<table border="0" cellSpacing="0" cellPadding="0" width="800" height="14" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table border="0" cellSpacing="0" cellPadding="0" width="750" align="center" name="Contenedor">
							<tr>
								<td align="center">
									<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="750" align="center">
										<tr>
											<td align="center">
												<table class="Arial10B" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
													<tr>
														<td height="4" width="10" border="0"></td>
														<td class="TituloRConsulta" height="32" width="98%" align="center">Pagos por 
															Delivery
														</td>
														<td height="32" vAlign="top" width="14"></td>
													</tr>
												</table>
												<table border="0" cellSpacing="0" cellPadding="0" width="700" align="center">
													<tr>
														<td width="98%">
															<table border="0" cellSpacing="0" cellPadding="0" width="700">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table border="0" cellSpacing="2" cellPadding="0" width="80%">
																			<tr>
																				<td width="250">&nbsp;</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Fecha Proceso :</td>
																				<td class="Arial12b" width="200"><input id="txtFecha" class="clsInputEnable" tabIndex="34" maxLength="10" size="10" name="txtFecha"
																						runat="server"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																						href="javascript:show_calendar('frmPrincipal.txtFecha');"><IMG border="0" src="../images/botones/btn_Calendario.gif"></A>
																				</td>
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
									<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="4" width="360" align="center">
										<tr>
											<td>
												<table class="Arial10B" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
													<tr>
														<td align="center"><asp:button id="btnBuscar" runat="server" Width="100px" CssClass="BotonOptm" Text="Buscar"></asp:button>&nbsp;&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<br>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<form>
			<table width="975" cellpadding="0" cellspacing="0" border="0" id="tbReporte" style="DISPLAY:none">
				<tr>
					<td width="820" valign="top">
						<table width="820" height="14" border="0" cellspacing="0" cellpadding="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table width="790" border="0" cellspacing="0" cellpadding="0" name="Contenedor" align="center">
							<tr>
								<td align="center">
									<table width="790" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#336699">
										<tr>
											<td align="center">
												<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="Arial10B">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td width="98%" height="32" align="center" class="TituloRConsulta">Pagos por 
															Delivery</td>
														<td valign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table width="790" border="0" align="center" cellpadding="0" cellspacing="0">
													<tr>
														<td width="98%">
															<table width="770" border="0" cellspacing="0" cellpadding="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td height="18">
																		<table border="0" cellspacing="1" cellpadding="0">
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
																		<table border="0" width="100%" cellspacing="2" cellpadding="0">
																			<tr>
																				<td width="30">&nbsp;</td>
																				<td width="166" class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha :</td>
																				<td class="Arial12b" width="170"><input name="txtFechaActual" class="clsInputDisable"  id="txtFechaActual" value="<%=sFechaActual%>" size="30" maxlength="15"></td>
																				<td width="30">&nbsp;</td>
																				<td width="120" class="Arial12b">&nbsp;&nbsp;&nbsp;Hora :</td>
																				<td class="Arial12b" width="220"><input name="txtHoraActual" class="clsInputDisable"  id="txtHoraActual" value="<%=sHoraActual%>" size="30" maxlength="15" readonly></td>
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
									<table width="790" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#336699">
										<tr>
											<td align="center">
												<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="Arial10B">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td width="98%" height="32" align="center" class="TituloRConsulta">Datos</td>
														<td valign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table width="790" border="0" align="center" cellpadding="0" cellspacing="0">
													<tr>
														<td width="98%">
															<table width="770" border="0" cellspacing="0" cellpadding="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td height="18">
																		<div style="Z-INDEX:1; BORDER-BOTTOM:1px; POSITION:relative; TEXT-ALIGN:center; BORDER-LEFT:1px; OVERFLOW-X:scroll; OVERFLOW-Y:scroll; WIDTH:770px; HEIGHT:310px; BORDER-TOP:1px; BORDER-RIGHT:1px"
																			class="frame2">
																			<table class="tabla_interna_borde1" width="750" cellspacing="1" cellpadding="1">
																				<TR class="Arial12B" height="21">
																					<TD align="center" width="10%" class="tabla_interna_borde2" height="22">Fecha hora 
																						Registro</TD>
																					<TD align="center" width="10%" class="tabla_interna_borde2" height="22">Número 
																						Pedido</TD>
																					<TD align="center" width="10%" class="tabla_interna_borde2" height="22">Tipo 
																						Documento</TD>
																					<TD align="center" width="10%" class="tabla_interna_borde2" height="22">N° 
																						Documento</TD>
																					<TD align="center" width="10%" class="tabla_interna_borde2" height="22">Fecha Cobro</TD>
																					<TD align="center" width="10%" class="tabla_interna_borde2" height="22">Modalidad Pago</TD>
																					<TD align="center" width="10%" class="tabla_interna_borde2" height="22">Medio Pago</TD>
																					<TD align="center" width="10%" class="tabla_interna_borde2" height="22">Id. Venta</TD>
																					<TD align="center" width="10%" class="tabla_interna_borde2" height="22">N° 
																						Autorización</TD>
																					<TD align="center" width="20%" class="tabla_interna_borde2" height="22">Monto 
																						Pagado</TD>
																				</TR>
																				<TR height="1">
																					<TD width="5%" class="tabla_interna_borde2"></TD>
																					<TD width="8%" class="tabla_interna_borde2"></TD>
																					<TD width="8%" class="tabla_interna_borde2"></TD>
																					<TD width="5%" class="tabla_interna_borde2"></TD>
																					<TD width="5%" class="tabla_interna_borde2"></TD>
																					<TD width="10%" class="tabla_interna_borde2"></TD>
																					<TD width="10%" class="tabla_interna_borde2"></TD>
																					<TD width="3%" class="tabla_interna_borde2"></TD>
																					<TD width="4%" class="tabla_interna_borde2"></TD>
																					<TD width="6%" class="tabla_interna_borde2"></TD>
																				</TR>
																				<%=strcadenaprint%>
																			</table>
																		</div>
																	</td>
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
					</td>
				</tr>
			</table>
		</form>
		<form>
			<table width="800" cellpadding="0" cellspacing="0" border="0" id="tbExportar" style="DISPLAY:none">
				<tr>
					<td>
						<table width="360" border="1" align="center" cellpadding="4" cellspacing="0" bordercolor="#336699">
							<tr>
								<td>
									<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="Arial10B">
										<tr>
											<td align="center">
												<input name="btnExportar" type="button" class="BotonOptm" style="WIDTH:100px" onClick="javascript:f_Exportar();"
													value="Exportar Excel" id="btnExportar" runat="server">&nbsp;&nbsp;
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
		<form name="frmTmp" method="post" action="" target="_blank">
		</form>
	</body>
</HTML>
