<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BusConfTran.aspx.vb" Inherits="SisCajas.BusConfTran"%>
<HTML>
	<HEAD>
		<title>America Movil</title>
		<meta http-equiv="Pragma" content="no-cache">
		<META http-equiv="Expires" content="Mon, 06 Jan 1990 00:00:01 GMT">
		<LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
			<script language="JavaScript" src="../../librerias/date-picker.js"></script>
			<script language="JavaScript" src="../../../librerias/Lib_FuncValidacion.js"></script>
			<meta http-equiv="pragma" content="no-cache">
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<div id="overDiv" style="Z-INDEX: 1; WIDTH: 100px; POSITION: absolute"></div>
			<input id="codOperacion" type="hidden" name="codOperacion"> <input id="txtCierre" type="hidden" name="txtCierre">
			<table cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<td vAlign="top" width="800">
						<table height="14" cellSpacing="0" cellPadding="0" width="800" border="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="750" align="center" border="0" name="Contenedor">
							<tr>
								<td align="center">
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="750" align="center"
										border="1">
										<tr>
											<td align="center">
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td class="TituloRConsulta" align="center" width="98%" height="32">Detalle de 
															Procesos con Autorización</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="700" align="center" border="0">
													<tr>
														<td width="98%">
															<table cellSpacing="0" cellPadding="0" width="700" border="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table cellSpacing="2" cellPadding="0" width="80%" border="0">
																			<tr>
																				<td width="250">&nbsp;</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Fecha Transacción :</td>
																				<td class="Arial12b" width="200"><input class="clsInputEnable" id="txtFecha" tabIndex="34" type="text" maxLength="10" size="10"
																						name="txtFecha" runat="server"> &nbsp;&nbsp;<A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																						href="javascript:show_calendar('frmPrincipal.txtFecha');"><IMG src="../../../images/botones/btn_Calendario.gif" border="0"></A>
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
									<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="360" align="center"
										border="1">
										<tr>
											<td>
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td align="center">
															<asp:Button id="cmdBuscar" runat="server" Text="Buscar" CssClass="BotonOptm" Width="57px"></asp:Button>&nbsp;&nbsp;</td>
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
		<script language="JavaScript" type="text/javascript">
	var esNavegador, esIExplorer;
	esNavegador = (navigator.appName == "Netscape") ? true : false;
	esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;
	function e_mayuscula(){
		if (event.keyCode>96&&event.keyCode<123)
			event.keyCode=event.keyCode-32;
	}
	function e_minuscula(){
		if (event.keyCode>64&&event.keyCode<91)
			event.keyCode=event.keyCode+32;
	}
	if (esIExplorer) {
		//window.document.frmPrincipal.txtPNombre.onkeypress=e_mayuscula;
	}
		</script>
	</body>
</HTML>
