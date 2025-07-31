<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ParamTarjeta.aspx.vb" Inherits="SisCajas.ParamTarjeta"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>America Movil</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmConfig" name="frmConfig" method="post" runat="server">
			<table>
				<tr>
					<td>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="600" align="left" border="1">
							<TBODY>
								<tr>
									<td>
										<table class="Arial10B" cellSpacing="0" cellPadding="0" width="475" align="center" border="0">
											<tr>
												<td width="10" height="4" border="0"></td>
												<td class="TituloRConsulta" align="center" width="90%" height="32">
													Configuración Parametro Monto de Pago con Tarjeta</td>
												<td vAlign="top" width="14" height="32"></td>
											</tr>
										</table>
										<br>
										<br>
										<table cellSpacing="2" cellPadding="2" width="475" border="0">
											<TBODY>
												<tr>
													<td class="Arial12b" width="300" colSpan="2"><b>&nbsp;Consideraciones para Scaneado de 
															Documentos</b></td>
													<td vAlign="top" width="90">&nbsp;</td>
													<td style="WIDTH: 15px" width="15">&nbsp;</td>
												</tr>
												<tr>
													<td colSpan="4">&nbsp;</td>
												</tr>
												<tr>
													<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Monto 
														mínimo en Soles:</td>
													<td style="WIDTH: 75px">&nbsp;<input class="clsInputEnable" id="txtMontoSoles" style="WIDTH: 70px" type="text" runat="server"></td>
													<td align="right" width="50"></td>
													<td style="WIDTH: 75px">&nbsp;</td>
												</tr>
												<tr class="Arial12b" style="DISPLAY: none; VISIBILITY: hidden">
													<td align="right" width="150">&nbsp;Dolares</td>
													<td style="WIDTH: 75px">&nbsp;<input class="clsInputEnable" id="txtCajaDolar" style="WIDTH: 70px" type="text" runat="server"></td>
													<td align="right" width="50">Tolerancia:</td>
													<td style="WIDTH: 75px">&nbsp;<input class="clsInputEnable" id="txtTolDolar" style="WIDTH: 70px" type="text" runat="server"></td>
												</tr>
												<tr>
													<td colSpan="4">&nbsp;</td>
												</tr>
											</TBODY>
										</table>
									</td>
								</tr>
							</TBODY>
						</table>
					</td>
				</tr>
				<tr>
					<td><br>
						<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="360" align="center"
							border="1">
							<tr>
								<td>
									<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td align="center"><input class="BotonOptm" id="cmdGrabar" style="WIDTH: 100px" type="button" value="Grabar"
													name="cmdGrabar" runat="server">&nbsp;&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br>
			</TD></TR><tr>
				<td><br>
					<br>
				</td>
			</tr>
			</TBODY></TABLE></form>
	</body>
</HTML>
