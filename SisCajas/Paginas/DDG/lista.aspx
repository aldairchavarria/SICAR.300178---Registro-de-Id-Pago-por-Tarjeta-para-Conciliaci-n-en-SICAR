<%@ Page Language="vb" AutoEventWireup="false" Codebehind="lista.aspx.vb" Inherits="SisCajas.lista"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<HTML>
	<HEAD>
		<title>Listado de Solicitudes</title>
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
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="750">
						<table cellSpacing="0" cellPadding="0" width="750" align="center" border="0" name="Contenedor">
							<tr>
								<td align="center">
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="800" align="center"
										border="1">
										<tr>
											<td align="center">
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td class="TituloRConsulta" align="center" width="98%" height="32">Lista de 
															Solicitudes</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="750" align="center" border="0">
													<tr>
														<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
															width="98%">
															<table cellSpacing="0" cellPadding="0" width="750" border="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td><table cellSpacing="1" cellPadding="1" width="750" border="1" BorderColor="#999999" >
																			<tr>
																				<td class="Arial12b" width="60"><B>&nbsp;Num Solic</B></td>
																				<td class="Arial12b" width="90"><B>&nbsp;PDV</B></td>
																				<td class="Arial12b" width="150"><B>&nbsp;Cliente</B></td>
																				<td class="Arial12b" width="75"><B>&nbsp;Dep. Garantía (S/.)</B></td>
																				<td class="Arial12b" width="70"><B>&nbsp;Fecha Termino</B></td>
																				<td class="Arial12b" width="70"><B>&nbsp;Fecha Solc</B></td>
																				<td class="Arial12b" width="75"><B>&nbsp;Estado</B></td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="60" style="cursor:hand" onclick="f_Visualizar()">&nbsp;<U>00001</U></td>
																				<td class="Arial12b" width="90">&nbsp;CAC Begonias</td>
																				<td class="Arial12b" width="150">&nbsp;Omar Salvador Vargada</td>
																				<td class="Arial12b" width="75">&nbsp;600.00</td>
																				<td class="Arial12b" width="70">&nbsp;29/08/2006</td>
																				<td class="Arial12b" width="70">&nbsp;08/09/2006</td>
																				<td class="Arial12b" width="75">Revision Cobranza</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="60" style="cursor:hand" onclick="f_VisContabil()">&nbsp;<U>00002</U></td>
																				<td class="Arial12b" width="90">&nbsp;CAC Megaplaza</td>
																				<td class="Arial12b" width="150">&nbsp;Marco Silva Noriega</td>
																				<td class="Arial12b" width="75">&nbsp;185.30</td>
																				<td class="Arial12b" width="70">&nbsp;31/08/2006</td>
																				<td class="Arial12b" width="70">&nbsp;21/09/2006</td>
																				<td class="Arial12b" width="75">Contabilidad</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="60" style="cursor:hand" onclick="f_Visualizar()">&nbsp;<U>00003</U></td>
																				<td class="Arial12b" width="90">&nbsp;CAC Lince</td>
																				<td class="Arial12b" width="150">&nbsp;Nilton Cesar Mori Leon</td>
																				<td class="Arial12b" width="75">&nbsp;250.00</td>
																				<td class="Arial12b" width="70">&nbsp;01/09/2006</td>
																				<td class="Arial12b" width="70">&nbsp;07/09/2006</td>
																				<td class="Arial12b" width="75">Revision Facturación</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" width="60" style="cursor:hand" onclick="f_Visualizar()">&nbsp;<U>00004</U></td>
																				<td class="Arial12b" width="90">&nbsp;CAC Primavera</td>
																				<td class="Arial12b" width="150">&nbsp;Carlos Arias Preciado</td>
																				<td class="Arial12b" width="75">&nbsp;315.50</td>
																				<td class="Arial12b" width="70">&nbsp;07/09/2006</td>
																				<td class="Arial12b" width="70">&nbsp;08/09/2006</td>
																				<td class="Arial12b" width="75">Revision Facturación</td>
																			</tr>
																		<br>
																		</table>
																		<table>
																			<tr><td>&nbsp;
																			</td></tr>
																			<tr><td>&nbsp;
																			</td></tr>
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
function f_Visualizar(){
	// Direcc = "DetalleSolicitud.aspx"
	// window.open(Direcc,"Detalles","directories=no,menubar=no,scrollbars=no,top=200,resizable=yes,left=185,width=550,height=250");
	window.location = "DetalleSolicitud.aspx"
}
function f_VisContabil(){
	window.location = "DetalleContabilidad.aspx"
}
		</script>
	</body>
</HTML>
