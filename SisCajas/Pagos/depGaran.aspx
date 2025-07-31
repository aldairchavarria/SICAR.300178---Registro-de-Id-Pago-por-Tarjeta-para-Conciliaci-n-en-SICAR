<%@ Page Language="vb" aspcompat=true AutoEventWireup="false" Codebehind="depGaran.aspx.vb" Inherits="SisCajas.depGaran" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>depGaran</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript" src="../librerias/ubigeo.js"></script>
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="javascript" type="text/javascript">
			function Inicio(){
				document.getElementById('btnPreGrabar').disabled = false;
			}
			function Validar(){			
				document.getElementById('btnPreGrabar').disabled = true;
				document.getElementById('btnGrabar').click();				
			}			
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0" onload="Inicio();">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<div id="overDiv" style="Z-INDEX: 1; WIDTH: 100px; POSITION: absolute"></div>
			<table cellSpacing="0" cellPadding="0" width="900" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="810">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" name="Contenedor">
							<tr>
								<td align="center" height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1" name="Contenedor">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10" height="32"></td>
											<td class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="top" align="center" width="98%"
												height="32">
												<asp:Label id="lblTitulo" runat="server"></asp:Label></td>
											<td vAlign="top" width="10" height="32"></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="14"></td>
											<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
												width="98%">
												<table cellSpacing="0" cellPadding="0" width="770" border="0">
													<tr>
														<td height="4"></td>
													</tr>
													<tr>
														<td height="18">
															<table cellSpacing="1" cellPadding="0" border="0">
																<tr class="Arial12b" bgColor="white">
																	<td width="200"><font color="#ff0000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Datos 
																				del Depósito</b></font></td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td height="4"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="770" border="0">
													<tr>
														<td>
															<table borderColor="white" cellSpacing="2" cellPadding="0" width="100%" border="1">
																<TBODY>
																	<tr>
																		<td style="HEIGHT: 20px" width="25">&nbsp;</td>
																		<td class="Arial12b" style="HEIGHT: 20px" width="150">&nbsp;&nbsp;&nbsp;Tipo de 
																			Doc. Identidad :</td>
																		<td class="Arial12b" style="HEIGHT: 20px" width="170"><asp:textbox id="txtTipoDoc" runat="server" ReadOnly="True" Width="147px" CssClass="clsInputDisable"></asp:textbox></td>
																		<td style="HEIGHT: 20px" width="25" height="20">&nbsp;</td>
																		<td class="Arial12b" style="HEIGHT: 20px" width="170">&nbsp;&nbsp;&nbsp;Nro. de 
																			Doc. Identidad :</td>
																		<td class="Arial12b" style="HEIGHT: 20px" width="150"><asp:textbox id="txtNumDocumento" runat="server" ReadOnly="True" Width="147px" CssClass="clsInputDisable"></asp:textbox></td>
																	</tr>
																	<tr>
																		<td>&nbsp;</td>
																		<td class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha de Reg. :</td>
																		<td vAlign="middle" width="150"><asp:textbox id="txtFecReg" runat="server" ReadOnly="True" Width="147px" CssClass="clsInputDisable"></asp:textbox></td>
																		<td width="25" height="22">&nbsp;</td>
																		<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Fecha de Venc. (*):</td>
																		<td vAlign="middle" width="150"><asp:textbox id="txtFecVenc" runat="server" ReadOnly="True" Width="147px" CssClass="clsInputDisable"></asp:textbox></td>
																	</tr>
																	<tr>
																		<td align="left">&nbsp;</td>
																		<td class="Arial12b">&nbsp;&nbsp;&nbsp;Importe (*):</td>
																		<td class="Arial12b"><asp:textbox id="txtImporte" runat="server" ReadOnly="True" Width="147px" CssClass="clsInputDisable"></asp:textbox></td>
																		<td class="Arial12b">&nbsp;</td>
																		<td class="Arial12b" width="165"></td>
																		<td class="Arial12b"></td>
																	</tr>
																	<tr>
																		<td class="Arial12b">&nbsp;</td>
																		<td class="Arial12b">&nbsp;&nbsp;&nbsp;Via de Pago (*):</td>
																		<td class="Arial12b"><asp:dropdownlist id="cboViaPago" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist></td>
														</td>
														<td class="Arial12b">&nbsp;</td>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Correlativo Ref. (*):</td>
														<td class="Arial12b"><asp:textbox id="txtCorrRef" runat="server" Width="147px" CssClass="clsInputEnable"></asp:textbox></td>
													</tr>
													<TR>
														<TD class="Arial12b"></TD>
														<TD class="Arial12b">&nbsp; &nbsp;Tipo de Cargo :&nbsp;
														</TD>
														<TD class="Arial12b">
															<asp:textbox id="txtTipoCargo" runat="server" CssClass="clsInputDisable" Width="147px" ReadOnly="True"></asp:textbox></TD>
														<TD class="Arial12b"></TD>
														<TD class="Arial12b">&nbsp;&nbsp; Número Operación :</TD>
														<TD class="Arial12b">
															<asp:textbox id="txtNumOperacion" runat="server" CssClass="clsInputEnable" Width="147px" MaxLength="20"></asp:textbox></TD>
													</TR>
													<tr>
														<td class="Arial12b">&nbsp;</td>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Imprimir Documento ?:</td>
														<td class="Arial12b"><input id="chkImprimir" type="checkbox" value="1" name="chkImpresion" runat="server" CHECKED></td>
														<td></td>
														<td></td>
														<td></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
								<td vAlign="top" width="14"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td height="10"></td>
				</tr>
			</table>
			<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="400" align="center"
				border="1">
				<tr>
					<td align="center">
						<table cellSpacing="2" cellPadding="0" border="0">
							<tr>
								<td align="center" width="28"></td>
								<td align="center" width="85"><input class="BotonOptm" id="btnPreGrabar" onmouseover="this.className='BotonOptm';"
													style="WIDTH: 100px; CURSOR: hand; HEIGHT: 19px" onclick="javascript:Validar();" onmouseout="this.className='BotonOptm';"
													type="button" value="Grabar" name="btnPreGrabar"></td>
								<td align="center" width="28"></td>
								<td align="center" width="85"><asp:button id="btnCancelar" runat="server" Width="98px" CssClass="BotonOptm" Text="Cancelar"></asp:button></td>
								<td align="center" width="28"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			</TD></TR></TBODY></TABLE>
			<div style="Display:none">
				<asp:button id="btnGrabar" runat="server" Width="98px" CssClass="BotonOptm" Text="Grabar"></asp:button>
			</div>
			</form>
	</body>
</HTML>
<script language="javascript">
function f_Validar() {

    event.returnValue = false;
	if (!ValidaNumero('document.frmPrincipal.txtCorrRef','el campo Correlativo Ref. ',false)) return false;
	if (document.frmPrincipal.txtNumOperacion.value != ""){
		if (!ValidaAlfanumerico('document.frmPrincipal.txtNumOperacion','el campo Número de Operación ',false)) return false;
	}
	
	event.returnValue = true;
	return true;  
};


</script>
