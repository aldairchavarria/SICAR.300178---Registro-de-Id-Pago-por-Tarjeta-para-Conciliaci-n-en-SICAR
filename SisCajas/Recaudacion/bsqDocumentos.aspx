<%@ Page Language="vb" AutoEventWireup="false" Codebehind="bsqDocumentos.aspx.vb" Inherits="SisCajas.bsqDocumentos" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>bsqDocumentos</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->		
		<script language=javascript>
			
		</script>
	</HEAD>
	<body>
		<form id="frmRecauda" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="600" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="600">
						<table height="14" cellSpacing="0" cellPadding="0" width="600" border="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="600" align="center" border="0" name="Contenedor">
							<tr>
								<td align="center">
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="550" align="center"
										border="1">
										<tr>
											<td align="center">
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td class="TituloRConsulta" align="center" width="98%" height="32">Recaudación - 
															Búsqueda de Documentos</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="550" align="center" border="0">
													<tr>
														<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
															width="98%">
															<table cellSpacing="0" cellPadding="0" width="500" border="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table cellSpacing="2" cellPadding="2" width="500" border="0">
																			<tr>
																				<td class="Arial12b" width="120">&nbsp;Tipo de Identificador:</td>
																				<!--INICIATIVA-565 INI-->
																				<td class="Arial12b" width="125"><asp:dropdownlist class="Arial12b" id="cboTipoIdentificador" runat="server" name="selTipo" Width="125px">
																						<asp:ListItem Value="01" Selected="True">Tel&#233;fono Cliente</asp:ListItem>
																						<asp:ListItem Value="02">N&#250;mero Recibo</asp:ListItem>
																						<asp:ListItem Value="101">Customer ID Fijo Soles</asp:ListItem>
																						<asp:ListItem Value="103">Customer ID Fijo Dolares</asp:ListItem>
																					</asp:dropdownlist></td>
                                                                                                                                                                 <!--INICIATIVA-565 FIN-->
																				<td class="Arial12b" width="125">&nbsp;Número Identificador:</td>
																				<td class="Arial12b" width="100"><asp:textbox id="txtIdentificador" runat="server" name="txtNumero" Width="75px" CssClass="clsInputEnable"
																						MaxLength="15"></asp:textbox></td>
																			</tr>
																			<tr>
																				<td class="Arial12b" colSpan="4">&nbsp;</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" align="center" colSpan="4"><asp:button id="cmdBuscar" runat="server" Width="100px" CssClass="BotonOptm" Text="Buscar"></asp:button></td>
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
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="hdnMensaje" type="hidden" name="hdnMensaje" runat="server"> 
			<!-- Valor que lanza los mensajes -->
			<!-- Atributos de la Página -->
			<input id="hdnPuntoDeVenta" type="hidden" name="hdnPuntoDeVenta" runat="server">
			<input id="hdnUsuario" type="hidden" name="hdnUsuario" runat="server"> <input id="hdnBinAdquiriente" type="hidden" name="hdnBinAdquiriente" runat="server">
			<input id="hdnCodComercio" type="hidden" name="hdnCodComercio" runat="server"> <input id="intCanal" type="hidden" name="intCanal" runat="server">
			<input id="hdnRutaLog" type="hidden" name="hdnRutaLog" runat="server"> <input id="hdnDetalleLog" type="hidden" name="hdnDetalleLog" runat="server">
		</form>
	</body>
</HTML>
