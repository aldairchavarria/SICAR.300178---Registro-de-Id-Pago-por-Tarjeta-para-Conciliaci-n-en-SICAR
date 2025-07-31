<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DetalleContabilidad.aspx.vb" Inherits="SisCajas.DetalleContabilidad"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Detalle de la Solicitud</title>
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
														<td class="TituloRConsulta" align="center" width="98%" height="32">Detalle de la 
															Solicitud</td>
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
																	<td><table cellSpacing="2" cellPadding="2" width="550" border="0">
																			<tr class="Arial12b">
																				<td><b>Datos del Cliente</b></td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="130">&nbsp;Nombre Cliente:</td>
																				<td class="Arial12b" width="200" colspan="3">Marco Silva Noriega</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="130">&nbsp;Deposito Garantía (S/.)</td>
																				<td class="Arial12b" width="150">&nbsp;185.30</td>
																				<td class="Arial12b" width="125">Fec Deposito:</td>
																				<td class="Arial12b" width="100" align="left">31/02/2006</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="130">&nbsp;</td>
																				<td class="Arial12b" width="150">&nbsp;</td>
																				<td class="Arial12b" width="125">Fec Termino:</td>
																				<td class="Arial12b" width="100">31/08/2006</td>
																			</tr>
																		</table>
																		<br>
																		<table cellSpacing="2" cellPadding="2" width="450" border="0">
																			<tr class="Arial12b">
																				<td><b>Cobranzas</b></td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="130">&nbsp;&nbsp;&nbsp;Código BSCS</td>
																				<td class="Arial12b" width="200"><input type="text" id="txtCodigoBSCS" name="txtCodigoBSCS" class="Arial12b" value="1.01244"
																						disabled style="WIDTH:100px;HEIGHT:18px"></td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="130">&nbsp;&nbsp;&nbsp;Importe a aplicar</td>
																				<td class="Arial12b" width="200"><input type="text" id="txtImporteBSCS" name="txtImporteBSCS" value="185.30" disabled class="Arial12b"
																						style="WIDTH:65px;HEIGHT:18px">&nbsp;(S/.)</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="130">&nbsp;&nbsp;&nbsp;Observaciones</td>
																				<td class="Arial12b" width="200" valign="top"><TEXTAREA style="WIDTH:226px;HEIGHT:60px" rows="3" cols="25" disabled>Se aplica todo el monto, cliente no tiene deuda</TEXTAREA></td>
																			</tr>
																		</table>
																		<br>
																		<table cellSpacing="2" cellPadding="2" width="450" border="0">
																			<tr class="Arial12b">
																				<td><b>Facturación</b></td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="130">&nbsp;&nbsp;&nbsp;Código SAP</td>
																				<td class="Arial12b" width="200"><input type="text" id="txtCodigoSAP" name="txtCodigoSAP" class="Arial12b" style="WIDTH:100px;HEIGHT:18px"
																						value="182455" disabled></td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="130">&nbsp;&nbsp;&nbsp;Importe a aplicar</td>
																				<td class="Arial12b" width="200"><input type="text" id="txtImporteSAP" name="txtImporteSAP" class="Arial12b" style="WIDTH:65px;HEIGHT:18px"
																						disabled value="185.30">&nbsp;(S/.)</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="130">&nbsp;&nbsp;&nbsp;Observaciones</td>
																				<td class="Arial12b" width="200" valign="top"><TEXTAREA style="WIDTH:228px;HEIGHT:60px" rows="3" cols="26" disabled>Se aplica todo</TEXTAREA></td>
																			</tr>
																		</table>
																		<br>
																		<table cellSpacing="2" cellPadding="2" width="450" border="0">
																			<tr class="Arial12b">
																				<td><b>Contabilidad</b></td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="130">&nbsp;&nbsp;&nbsp;Observaciones</td>
																				<td class="Arial12b" width="200" valign="top"><TEXTAREA style="WIDTH:228px;HEIGHT:60px" rows="3" cols="26">Se aplica a la cuenta contable</TEXTAREA></td>
																			</tr>
																		</table>
																		<br>
																		<table cellSpacing="2" cellPadding="2" width="600" border="0">
																			<tr>
																				<td class="Arial12b" align="center" colSpan="2">
																					<input class="BotonOptm" id="cmdCancelar" style="WIDTH: 100px" onclick="f_Cancelar()" type="button"
																						value="Cancelar" name="cmdCancelar"></td>
																				<td class="Arial12b" align="center">
																					<input class="BotonOptm" id="cmdGrabar" style="WIDTH: 100px" onclick="f_Grabar()" type="button"
																						value="Grabar" name="cmdGrabar"></td>
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
					</td>
				</tr>
			</table>
		</form>
		</TR></TABLE></FORM>
		<script language="javascript">
function f_Grabar(){
	alert("Se grabo la solicitud")
	history.back()
}
function f_Cancelar(){
	history.back()
}
		</script>
	</body>
</HTML>
