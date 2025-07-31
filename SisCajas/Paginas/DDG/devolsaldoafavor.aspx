<%@ Page Language="vb" AutoEventWireup="false" Codebehind="devolsaldoafavor.aspx.vb" Inherits="SisCajas.devolsaldoafavor" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HEAD>
	<title>Devolución Saldo a Favor</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="../../../estilos/est_General.css" type="text/css" rel="styleSheet">
	<script language="javascript">
			
	</script>
</HEAD>
<body link="#ceefff" vlink="#ceefff" alink="#ceefff">
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
													<td class="TituloRConsulta" align="center" width="98%" height="32">Devolución de 
														Saldo a Favor</td>
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
																<td><table cellSpacing="2" cellPadding="2" width="500" border="0">
																		<tr>
																			<td class="Arial12b" width="120">&nbsp;Tipo documento:</td>
																			<td class="Arial12b" width="125"><asp:dropdownlist class="Arial12b" id="cboTipoIdentificador" runat="server" name="selTipo" Width="125px">
																					<asp:ListItem Value="01" Selected="True">Número telefonico</asp:ListItem>
																					<asp:ListItem Value="02">DNI</asp:ListItem>
																					<asp:ListItem Value="03">RUC</asp:ListItem>
																				</asp:dropdownlist></td>
																			<td class="Arial12b" width="125">&nbsp;Número documento:</td>
																			<td class="Arial12b" width="100"><asp:textbox id="txtIdentificador" runat="server" name="txtNumero" Width="75px" CssClass="clsInputEnable"
																					MaxLength="15"></asp:textbox></td>
																		</tr>
																		<tr>
																			<td class="Arial12b" colSpan="4">&nbsp;</td>
																		</tr>
																		<tr>
																				<td class="Arial12b" align="center" colSpan="4">
																					<input class="BotonOptm" id="cmdBuscar" style="WIDTH: 100px" onclick="f_Buscar()" type="button"
																						value="Buscar" name="cmdBuscar"></td>
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
	</form>
	</TR></TABLE></FORM>
</body>

<script language=javascript>

function f_Buscar(){
	Direcc = "DatosCliente.aspx"
	window.open(Direcc,"Datos","directories=no,menubar=no,scrollbars=no,top=200,resizable=yes,left=185,width=510,height=400");
}

</script>
