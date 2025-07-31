<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NuevaSolicitud.aspx.vb" Inherits="SisCajas.NuevaSolicitud"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Nueva Solicitud</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="javascript">
			
		</script>
	</HEAD>
	<body vLink="#ceefff" aLink="#ceefff" link="#ceefff">
		<form id="frmRecauda" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="400" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="490">
						<table height="14" cellSpacing="0" cellPadding="0" width="490" border="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="490" align="center" border="0" name="Contenedor">
							<tr>
								<td align="center">
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="475" align="center"
										border="1">
										<tr>
											<td align="center">
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td class="TituloRConsulta" align="center" width="98%" height="32">Solicitd de 
															Devolución</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="450" align="center" border="0">
													<tr>
														<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
															width="98%">
															<table cellSpacing="1" cellPadding="1" width="450" border="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table cellSpacing="2" cellPadding="2" width="450" border="0">
																			<tr>
																				<td class="Arial12b" width="125">&nbsp;Cod. Cliente:</td>
																				<td class="Arial12b" colSpan="3">10524</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="125">&nbsp;Nombre Cliente:</td>
																				<td class="Arial12b" colSpan="3">Omar Salvador Vargada Bermudez</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="125">&nbsp;Monto a devolver:</td>
																				<td class="Arial12b" width="100"><input type="text" id="txtMonto" name="txtMonto" class="Arial11b" style="WIDTH:70px; HEIGHT:18px"
																						size="11">&nbsp;(S/.)</td>
																				<td class="Arial12b" width="80">&nbsp;</td>
																				<td class="Arial12b" width="150">&nbsp;</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" colspan="4">&nbsp;</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" align="center" colSpan="2">
																					<input class="BotonOptm" id="cmdCancelar" style="WIDTH: 100px" onclick="f_Cancelar()" type="button"
																						value="Cancelar" name="cmdCancelar"></td>
																				<td class="Arial12b" align="center" colSpan="2">
																					<input class="BotonOptm" id="cmdBuscar" style="WIDTH: 100px" onclick="f_NuevaSol()" type="button"
																						value="Grabar" name="cmdBuscar"></td>
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
		<script language="javascript">
function f_Cancelar(){
	window.close()
}
function f_NuevaSol(){
	window.close()
}


		</script>
	</body>
</HTML>
