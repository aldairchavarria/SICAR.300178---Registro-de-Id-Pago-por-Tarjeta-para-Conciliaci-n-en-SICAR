<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DatosCliente.aspx.vb" Inherits="SisCajas.DatosCliente1"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Datos del Cliente</title>
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
			<table cellSpacing="0" cellPadding="0" width="600" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="500">
						<table height="14" cellSpacing="0" cellPadding="0" width="600" border="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="500" align="center" border="0" name="Contenedor">
							<tr>
								<td align="center">
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="510" align="center"
										border="1">
										<tr>
											<td align="center">
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td class="TituloRConsulta" align="center" width="98%" height="32">Devolución 
															Depositos en Garantía</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
													<tr>
														<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
															width="98%">
															<table cellSpacing="1" cellPadding="1" width="500" border="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table cellSpacing="2" cellPadding="2" width="500" border="0">
																			<tr>
																				<td class="Arial12b" width="85">&nbsp;Cliente:</td>
																				<td class="Arial12b" colSpan="3">&nbsp;Omar Salvador Vargada Bermudez</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="85">&nbsp;PDV:</td>
																				<td class="Arial12b" width="150">&nbsp;CAC Begonias</td>
																				<td class="Arial12b" width="80">&nbsp;Asesor:</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;Castillo Meyling Rosell</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="85">&nbsp;Dep Garantia:</td>
																				<td class="Arial12b" width="150">&nbsp;600.00</td>
																				<td class="Arial12b" width="80">&nbsp;Fec Dep:</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;01/03/2006
																				</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="85">&nbsp;</td>
																				<td class="Arial12b" width="150">&nbsp;</td>
																				<td class="Arial12b" width="80">&nbsp;Fec Termino:</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;07/09/2006
																				</td>
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
																						value="Generar Solicitud" name="cmdBuscar"></td>
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
	Direcc = "NuevaSolicitud.aspx"
	window.open(Direcc,"NuevaSol","directories=no,menubar=no,scrollbars=no,top=200,resizable=yes,left=185,width=510,height=405");
}


		</script>
	</body>
</HTML>
