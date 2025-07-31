<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RecPagoDocId.aspx.vb" Inherits="SisCajas.RecPagoDocId"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>bsqDocumentos</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../librerias/jquery-1.8.2.min.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
		function solonumeros(e) {
			tecla_codigo = (document.all) ? e.keyCode : e.which;
			if (tecla_codigo == 8 || tecla_codigo == 9) { return true };
			/*para negativos patron =/[0-9\-]/;*/

			patron = /[0-9]/;
			tecla_valor = String.fromCharCode(tecla_codigo);
			return patron.test(tecla_valor);
		}
		$(document).ready(function(){			
			$('#txtDNI').on("paste", function (e) {
				var element = this;
				setTimeout(function () {
					var text = $(element).val();
					text = $.trim(text);
					var lonText = text.length;
					var cadena = '';
					if (lonText > 0) {
						for (var i = 0; i < lonText; i++) {
							var num = String(text).substr(i, 1);
							num = $.trim(num);
							if (num != '') {
								var esNumero = isNaN(num);
								if (!esNumero) {
									cadena += num;
								}
							}
						}
					}
					$('#txtDNI').val(cadena);
				}, 100);
		   });			
		});
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
														<td class="TituloRConsulta" align="center" width="98%" height="32">Recaudación de 
															Clientes por DNI</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="550" align="center" border="0">
													<tr>
														<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 5px"
															width="98%">
															<table cellSpacing="0" cellPadding="0" width="500" border="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table cellSpacing="2" cellPadding="2" width="400" border="0">
																			<tr>
																				<td class="Arial12b" width="108" style="WIDTH: 108px">&nbsp;Número de DNI:</td>
																				<td class="Arial12b" width="100">
																					<asp:textbox id="txtDNI" runat="server" name="txtDNI" Width="125px" CssClass="clsInputEnable"
																					 MaxLength="8" onkeypress="return solonumeros(event)"></asp:textbox></td>
																				<td Width="95">
																					<asp:button id="cmdBuscar" runat="server" Width="100px" CssClass="BotonOptm" Text="Buscar"></asp:button>
																				</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="108" style="WIDTH: 108px">&nbsp;Tipo de Servicio:</td>
																				<td class="Arial12b" width="125">
																					<asp:dropdownlist class="Arial12b" id="cboTipoServicio" runat="server" name="cboTipoServicio" Width="125px">
																						<asp:ListItem Value="00" Selected="True">Todos</asp:ListItem>
																						<asp:ListItem Value="01">Movil</asp:ListItem>
																						<asp:ListItem Value="02">Fijo</asp:ListItem>
																					</asp:dropdownlist>
																				</td>
																				<td>
																				</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" colSpan="4">&nbsp;</td>
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
