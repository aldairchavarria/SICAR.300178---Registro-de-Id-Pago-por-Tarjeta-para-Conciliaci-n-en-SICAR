<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ParamOficina.aspx.vb" Inherits="SisCajas.ParamOficina"%>
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
												<td class="TituloRConsulta" align="center" width="90%" height="32">Configuración 
													Parametros de Oficina / PDV</td>
												<td vAlign="top" width="14" height="32"></td>
											</tr>
										</table>
										<br>
										<br>
										<table cellSpacing="2" cellPadding="2" width="475" border="0">
											<tbody>
												<tr>
													<td class="Arial12b" style="WIDTH: 251px" width="251" colSpan="4"><b>&nbsp;Datos 
															Generales&nbsp;</b></td>
												</tr>
												<tr>
													<td colSpan="4">&nbsp;</td>
												</tr>
												<tr>
													<td style="WIDTH: 50px" width="50">&nbsp;</td>
													<td class="Arial12b" style="WIDTH: 200px" width="200">&nbsp;&nbsp;&nbsp;&nbsp;Organiz. 
														ventas&nbsp;</td>
													<td width="270" colSpan="2"><input class="clsInputEnable" id="txtOrgVenta" style="WIDTH: 270px" type="text" name="txtOrgVenta"
															runat="server"></td>
												</tr>
												<tr>
													<td style="WIDTH: 50px" width="50">&nbsp;</td>
													<td class="Arial12b" style="WIDTH: 200px" width="200">&nbsp;&nbsp;&nbsp;&nbsp;Canal&nbsp;</td>
													<td width="50"><input class="clsInputEnable" id="txtCanal" style="WIDTH: 50px" type="text" name="txtCanal"
															runat="server"></td>
													<td width="150"><input class="clsInputEnable" id="txtCanalDes" style="WIDTH: 210px" type="text" name="txtCanalDes"
															runat="server"></td>
												</tr>
												<tr>
													<td style="WIDTH: 50px" width="50">&nbsp;</td>
													<td class="Arial12b" style="WIDTH: 200px" width="200">&nbsp;&nbsp;&nbsp;&nbsp;Sector&nbsp;</td>
													<td width="150" colSpan="2"><input class="clsInputEnable" id="txtSector" style="WIDTH: 50px" type="text" name="txtSector"
															runat="server"></td>
												</tr>
												<tr>
													<td style="WIDTH: 50px" width="50">&nbsp;</td>
													<td class="Arial12b" style="WIDTH: 200px" width="200">&nbsp;&nbsp;&nbsp;&nbsp;Oficina 
														ventas&nbsp;</td>
													<td width="50"><input class="clsInputEnable" id="txtOfic" style="WIDTH: 50px" type="text" name="txtOfic"
															runat="server"></td>
													<td width="150"><input class="clsInputEnable" id="txtOficDes" style="WIDTH: 210px" type="text" name="txtOficDes"
															runat="server"></td>
												</tr>
											</tbody>
										</table>
										<br>
										<br>
										<table cellSpacing="2" cellPadding="2" width="475" border="0">
											<TBODY>
												<tr>
													<td class="Arial12b" style="WIDTH: 251px" width="251" colSpan="2"><b>&nbsp;Registro 
															cuenta bancaria &nbsp;</b>(para remesas)</td>
													<td vAlign="top" width="150">&nbsp;</td>
													<td style="WIDTH: 15px" width="15">&nbsp;</td>
												</tr>
												<tr>
													<td colSpan="4">&nbsp;</td>
												</tr>
												<tr>
													<td style="WIDTH: 15px" width="15">&nbsp;</td>
													<td class="Arial12b" style="WIDTH: 250px" width="250">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
														&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Cuentas bancarias 
														asociadas&nbsp;</td>
													<td width="150">&nbsp;</td>
												</tr>
												<tr>
													<td style="WIDTH: 15px" width="15">&nbsp;</td>
													<td class="Arial12b" align="right" width="240"><asp:dropdownlist class="Arial11b" id="SelNomBanco1" runat="server"></asp:dropdownlist><asp:dropdownlist class="Arial11b" id="SelTipMoneda1" runat="server"></asp:dropdownlist>&nbsp;</td>
													<td width="150">&nbsp;<input class="clsInputEnable" id="txtCtaSol" style="WIDTH: 125px" type="text" name="CtaBanco1"
															runat="server"></td>
												</tr>
												<tr>
													<td style="WIDTH: 15px" width="15">&nbsp;</td>
													<td class="Arial12b" align="right" width="240"><asp:dropdownlist class="Arial11b" id="SelNomBanco2" runat="server"></asp:dropdownlist><asp:dropdownlist class="Arial11b" id="SelTipMoneda2" runat="server"></asp:dropdownlist>&nbsp;</td>
													<td width="150">&nbsp;<input class="clsInputEnable" id="txtCtaDolar" style="WIDTH: 125px" type="text" name="CtaBanco2"
															runat="server"></td>
												</tr>
											</TBODY>
										</table>
										<br>
										<br>
										<table cellSpacing="2" cellPadding="2" width="475" border="0">
											<TBODY>
												<tr>
													<td class="Arial12b" width="300" colSpan="2"><b>&nbsp;Consideraciones PDV&nbsp;</b>(configuración)</td>
													<td vAlign="top" width="90">&nbsp;</td>
													<td style="WIDTH: 15px" width="15">&nbsp;</td>
												</tr>
												<tr>
													<td colSpan="4">&nbsp;</td>
												</tr>
												<tr>
													<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Limite 
														máximo en caja</td>
													<td class="Arial12b" style="WIDTH: 70px">&nbsp;</td>
													<td width="50">&nbsp;</td>
													<td width="75">&nbsp;</td>
												</tr>
												<tr class="Arial12b">
													<td align="right" width="150">&nbsp;Soles</td>
													<td style="WIDTH: 75px">&nbsp;<input class="clsInputEnable" id="txtCajaSol" style="WIDTH: 70px" type="text" runat="server"></td>
													<td align="right" width="50">Tolerancia:</td>
													<td style="WIDTH: 75px">&nbsp;<input class="clsInputEnable" id="txtTolSol" style="WIDTH: 70px" type="text" runat="server"></td>
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
												<tr class="Arial12b">
													<td colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Habilitar 
														cambio de fecha</td>
													<td align="left" width="200" colSpan="2"><asp:checkbox id="chkFecha" runat="server"></asp:checkbox>&nbsp;</td>
												</tr>
												<tr class="Arial12b">
													<td colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Habilitar 
														impresión por SAP</td>
													<td align="left" width="200" colSpan="2"><asp:checkbox id="chkImpSAP" runat="server"></asp:checkbox>&nbsp;</td>
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
