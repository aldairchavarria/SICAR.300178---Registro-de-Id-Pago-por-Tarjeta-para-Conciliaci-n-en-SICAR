<%@ Page Language="vb" AutoEventWireup="false" Codebehind="depGaranManual.aspx.vb" Inherits="SisCajas.depGaranManual" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>depGaranManual</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../librerias/ubigeo.js"></script>
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
<!--

	document.onclick = document_onclick;

	function document_onclick() {
		var obj = event.srcElement;
		switch (obj.tagName) {			
			case "INPUT":
				switch (obj.id) {
					case "btnGrabar":						
						if (!f_Grabar()) event.returnValue = false;
						break;							
				}
				break;
		}
	}

	function f_Grabar()
	{
		if (f_Validar())
			return true;
		else
			return false;
	}

	function f_Validar() {// valida campos
		//if (!ValidaCombo('document.frmPrincipal.cbotipoDoc','el campo Tipo de Doc. ',true)) return false;
		//if (!ValidaEXTR('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
		//alert(document.frmPrincipal.cbotipoDoc.value);
		if (!ValidaCombo('document.frmPrincipal.cbotipoDoc','el campo tipo de Doc. de Identidad ',false)) return false;
		switch (parseInt(document.frmPrincipal.cbotipoDoc.options[document.frmPrincipal.cbotipoDoc.selectedIndex].value)) {
			case 1 : 
				if (!ValidaDNI('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
				break;
			case 2 : 
				if (!ValidaFP('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
				break;
			case 3 : 
				if (!ValidaFA('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
				break;
			case 4 : 
				if (!ValidaEXTR('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
				break;
			case 6 : 
				if (!ValidaRUC('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
				break;
			case 7 : 
				if (!ValidaPAS('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
				break;
			default : 
				if (!ValidaAlfanumerico('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
				break;
		}

		
		if (!ValidaFechaA('document.frmPrincipal.txtFecReg',false)) return false;
		if (!ValidaFechaA('document.frmPrincipal.txtFecVenc',false)) return false;
		if (!ValidaDecimal('document.frmPrincipal.txtImporte','el campo Importe ',false)) return false;
	//	if (!ValidaCombo('document.frmPrincipal.cboMoneda','el campo Moneda ',false)) return false;
		if (!ValidaCombo('document.frmPrincipal.cboViaPago','el campo Via de Pago ',false)) return false;
		if (!ValidaNumero('document.frmPrincipal.txtCorrRef','el campo Correlativo Ref. ',false)) return false;
		return true;  
	};


	function f_Cancelar() { 	
	}
	function textCounter(obj) {
		var maximo;
		
		switch (parseInt(document.frmPrincipal.cbotipoDoc.options[document.frmPrincipal.cbotipoDoc.selectedIndex].value)) {
			case 1 : 
				maximo = 8;
				break;
			case 2 : 
				maximo = 10;
				break;
			case 3 : 
				maximo = 10;
				break;
			//@@@ BEGIN
			//Responsable		: Narciso Lema Ch.
			//Modificación	:	Requerimiento PMO. Registro de Clientes - Carné de Extranjería.
			case 4 : maximo = <%=gIntCarneExtranjeriaMax%>;	break;
			//@@@ END
			case 6 : 
				maximo = 11;
				break;
			case 7 : 
				maximo = 10;
				break;
			default : 
				maximo = 15;
				break;
		}
		if (obj.value.length > maximo) // if too long...trim it!
			obj.value = obj.value.substring(0, maximo); // otherwise, update 'characters left' counter
	}

//-->
		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server">
			<table border="0" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<table style="WIDTH: 750px" borderColor="#336699" cellSpacing="0" cellPadding="0" width="822"
							align="left" border="1" name="Contenedor">
							<tr>
								<td vAlign="top" align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10" height="32"></td>
											<td class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="top" align="center" width="98%"
												height="32">Depósito en Prenda</td>
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
																		<td width="25">&nbsp;</td>
																		<td class="Arial12b" width="150">&nbsp;&nbsp;&nbsp;Tipo de Doc. Identidad :</td>
																		<td class="Arial12b" width="170"><asp:dropdownlist id="cbotipoDoc" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist></td>
																		<td width="25" height="22">&nbsp;</td>
																		<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Nro. de Doc. Identidad :</td>
																		<td class="Arial12b" width="150"><input class="clsInputEnable" id="txtNumDocumento" onkeydown="textCounter(this)" onkeyup="textCounter(this)"
																				tabIndex="3" type="text" maxLength="15" size="30" name="txtNumDocumento" runat="server"></td>
																	</tr>
																	<tr>
																		<td>&nbsp;</td>
																		<td class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha de Reg. :</td>
																		<td vAlign="middle" width="150"><input class="clsInputEnable" id="txtFecReg" tabIndex="4" type="text" maxLength="10" size="25"
																				name="txtFecReg" runat="server"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																				href="javascript:show_calendar('frmPrincipal.txtFecReg');"><IMG src="../../images/botones/btn_Calendario.gif" border="0"></A>
																		</td>
																		<td width="25" height="22">&nbsp;</td>
																		<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Fecha de Venc. (*):</td>
																		<td vAlign="middle" width="150"><input class="clsInputEnable" id="txtFecVenc" tabIndex="5" type="text" maxLength="10" size="25"
																				name="txtFecVenc" runat="server"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																				href="javascript:show_calendar('frmPrincipal.txtFecVenc');"><IMG src="../../images/botones/btn_Calendario.gif" border="0"></A>
																		</td>
																	</tr>
																	<tr>
																		<td align="left">&nbsp;</td>
																		<td class="Arial12b">&nbsp;&nbsp;&nbsp;Importe (*):</td>
																		<td class="Arial12b"><input class="clsInputEnable" id="txtImporte" tabIndex="6" type="text" maxLength="14" size="30"
																				name="txtImporte" runat="server"></td>
																		<td class="Arial12b">&nbsp;</td>
																		<td class="Arial12b" width="165"></td>
																		<td class="Arial12b"></td>
																	</tr>
																	<tr>
																		<td class="Arial12b">&nbsp;</td>
																		<td class="Arial12b">&nbsp;&nbsp;&nbsp;Via de Pago (*):</td>
																		<td class="Arial12b"><asp:dropdownlist id="cboViaPago" tabIndex="8" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist></td>
														</td>
														<td class="Arial12b">&nbsp;</td>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Correlativo Ref. (*):</td>
														<td class="Arial12b"><input class="clsInputEnable" id="txtCorrRef" tabIndex="9" type="text" maxLength="5" size="30"
																name="txtCorrRef" runat="server"></td>
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
			</TD></TR>
			<tr>
				<td>
					<table style="WIDTH: 822px; HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="822" align="center"
						border="0">
						<tr>
							<td height="10"></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="400" align="center"
						border="1">
						<tr>
							<td align="center">
								<table cellSpacing="2" cellPadding="0" border="0">
									<tr>
										<td align="center" width="28"></td>
										<td align="center" width="85">
											<asp:Button id="btnGrabar" runat="server" CssClass="BotonOptm" Text="Grabar" Width="98px"></asp:Button></td>
										<td align="center" width="28"></td>
										<td align="center" width="85"><INPUT class="BotonOptm" id="btnCancelar" style="WIDTH: 98px" onclick="f_Cancelar()" type="button"
												value="Cancelar" name="btnCancelar"></td>
										<td align="center" width="28"></td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			</TBODY></TABLE>
		</form>
	</body>
</HTML>
