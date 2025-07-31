<%@ Page Language="vb" AutoEventWireup="false" Codebehind="mantenimiento.aspx.vb" Inherits="SisCajas.mantenimiento" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>America Movil</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../estilos/est_General.css" type="text/css" rel="styleSheet">
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
												<td class="TituloRConsulta" align="center" width="90%" height="32">Configuración 
													Parametros de Oficina / PDV</td>
												<td vAlign="top" width="14" height="32"></td>
											</tr>
										</table>
										<br>
										<table cellSpacing="2" cellPadding="2" width="475" border="0">
											<TBODY>
												<tr>
													<td class="Arial12b" style="WIDTH: 251px" width="251" colSpan="3"><b>&nbsp;Registro 
															cuentas</b></td>
												</tr>
												<tr>
													<td style="WIDTH: 15px" width="100">&nbsp;</td>
													<td class="Arial12b" align="right" width="100">&nbsp;Cuenta Contable</td>
													<td width="200">&nbsp;<input class="clsInputEnable" id="txtCtaDolar" style="WIDTH:125px" type="text" name="CtaBanco2"
															runat="server"></td>
												</tr>
											</TBODY>
										</table>
										<br>
										<table cellSpacing="2" cellPadding="2" width="475" border="0">
											<TBODY>
												<tr>
													<td class="Arial12b" style="WIDTH: 251px" width="251" colSpan="3"><b>&nbsp;Consideraciones 
															PDV</b></td>
												</tr>
												<tr>
													<td style="WIDTH: 15px" width="100">&nbsp;</td>
													<td class="Arial12b" align="right" width="100">&nbsp;Limite máximo de devolución</td>
													<td class="Arial12b" width="200">&nbsp;<input class="clsInputEnable" id="Text1" style="WIDTH:75px" type="text" name="CtaBanco2"
															runat="server">&nbsp;(S/.)</td>
												</tr>
												<tr>
													<td style="WIDTH: 15px" width="100">&nbsp;</td>
													<td class="Arial12b" align="right" width="100">&nbsp;Tolerancia</td>
													<td class="Arial12b" width="200">&nbsp;<input class="clsInputEnable" id="Text3" style="WIDTH:75px" type="text" name="CtaBanco2"
															runat="server">&nbsp;(S/.)</td>
												</tr>
											</TBODY>
										</table>
										<br>
										<table cellSpacing="2" cellPadding="2" width="475" border="0">
											<TBODY>
												<tr>
													<td class="Arial12b" style="WIDTH: 251px" width="251" colSpan="3"><b>&nbsp;Seguimiento 
															de Solicitud</b></td>
												</tr>
												<tr>
													<td style="WIDTH: 15px" width="100">&nbsp;</td>
													<td class="Arial12b" align="right" width="100">&nbsp;Días maximo de aprobación</td>
													<td width="200">&nbsp;<input class="clsInputEnable" id="Text2" style="WIDTH:50px" type="text" name="CtaBanco2"
															runat="server"></td>
												</tr>
												<tr>
													<td style="WIDTH: 15px" width="100">&nbsp;</td>
													<td class="Arial12b" align="right" width="100">&nbsp;Correo de confirmación</td>
													<td width="200">&nbsp;
														<asp:ListBox id="ListBox1" runat="server" Width="204px" class="Arial11b" SelectionMode="Multiple">
															<asp:ListItem Value="OMAR VARGADA">OMAR VARGADA</asp:ListItem>
															<asp:ListItem Value="CRISTOFER RAMIREZ">CRISTOFER RAMIREZ</asp:ListItem>
															<asp:ListItem Value="JOSE TINEO">JOSE TINEO</asp:ListItem>
															<asp:ListItem Value="PATRICIA MIRANDA">PATRICIA MIRANDA</asp:ListItem>
														</asp:ListBox></td>
												</tr>
											</TBODY>
										</table>
										<br>
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
