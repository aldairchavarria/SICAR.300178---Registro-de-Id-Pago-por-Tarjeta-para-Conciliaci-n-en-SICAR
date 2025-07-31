<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorRemesa.aspx.vb" Inherits="SisCajas.visorRemesa" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>América Móvil</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script type="text/javascript" src="../librerias/Lib_FuncGenerales.js"></script>
		<script language="JavaScript" src="../../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="JavaScript">
			
		function validarNumero(event) {
			if (event.keyCode == 8 || event.keyCode == 46) {
				return;
			}
			if (event.keyCode >= 37 && event.keyCode <= 40) { // Allow directional arrows
				return;
			}
			eventoSoloNumeros(event);
		}
		
		function eventoSoloNumeros(){
			// NOTE: Backspace = 8, Enter = 13, '0' = 48, '9' = 57		
			var key = event.keyCode;	
			if ((key <= 13 || (key >= 48 && key <= 57) || (key >= 96 && key <= 105) || (key == 33) || (key == 34) || (key == 35) || (key == 36)|| (key == 37) || (key == 38) || (key == 39) || (key == 40)|| (key == 45) || (key == 46) || (key == 17) || (key == 86) || (key == 67) || (key == 88) || (key == 82))==true)
				event.returnValue = true;
			else
				event.returnValue = false;	
		}
		
		function ValidaNumero(obj){
			var KeyAscii = window.event.keyCode;
			if (KeyAscii==13) return;	
			if (!(KeyAscii >= 46 && KeyAscii<=57) | (KeyAscii==46 && obj.value.indexOf(".")>=0) ){		
				window.event.keyCode = 0;
			}	
			else
			{	
				if (obj.value.indexOf(".")>=0 ){		
					if (KeyAscii!=46  && obj.value.substring(obj.value.indexOf(".")+1,obj.value.length).length>1)
						window.event.keyCode = 0;	
				}
			}
		}
		
		function f_LimpiaControl(){
			var nIndice = 0;
			with(frmVisRem) {
				var today = new Date();
				var dd = today.getDate();
				var mm = today.getMonth() + 1;
				var yyyy = today.getFullYear();
				
				if(dd < 10){
					dd = '0' + dd;
				}
				
				if(mm < 10){
					mm = '0' + mm;
				}
				
				today = dd + '/' + mm + '/' + yyyy;
			
				reset();
				
				cboTipoRemesa.selectedIndex = nIndice;
							
				//document.getElementById("txtFechaIni").value = today;
				//document.getElementById("txtFechaFin").value = today;
				
				document.getElementById("hidCodOficina").value = "";
				document.getElementById("hidCodCajero").value = "";
				document.getElementById("hidCodUsuario").value = "";
				document.getElementById("txtCodOficina").value = "";
				document.getElementById("txtCodCajero").value = "";
				document.getElementById("txtOficina").value = "";
				document.getElementById("txtCajero").value = "";
				
				document.getElementById("txtCodUsuarioFI").value = "";
				document.getElementById("txtUsuarioFI").value = "";
				
				document.getElementById("txtCntRegistros").value = "250";
			}
		}
		
		function f_OpenOficina(){
			Direcc = "visorRecTrsPago_oficina.aspx";
			window.open(Direcc,"OpenOf","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_CargarDatosOficina(){
			frmVisRem.loadDataHandler.click();
		}
		
		function f_OpenCajero(){
			var of = document.getElementById("hidCodOficina").value;
			document.getElementById("hidFlag").value = "0";
			Direcc = "visorRecTrsPago_cajero.aspx?of=" + of;
			window.open(Direcc,"OpenCaj","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_CargarDatosCajero(){
			frmVisRem.loadDataHandler.click();
		}
		
		function f_OpenUsuario(){
			var of = document.getElementById("hidCodOficina").value;
			document.getElementById("hidFlag").value = "1";
			Direcc = "visorRecTrsPago_cajero.aspx?of=" + of;
			window.open(Direcc,"OpenUsu","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_CargarDatosUsuario(){
			frmVisRem.loadDataHandler.click();
		}
		
		function f_buscarOficina(){
			if(event.keyCode == 13 ){
				frmVisRem.loadOficinaHandler.click();
			}
		}
		
		function f_buscarCajero(){
			if(event.keyCode == 13 ){
				frmVisRem.loadCajeroHandler.click();
			}
		}
		
		function f_buscarUsuario(){
			if(event.keyCode == 13 ){
				frmVisRem.loadUsuarioHandler.click();
			}
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmVisRem" method="post" runat="server">
			<input id="hidCodCajero" runat="server" type="hidden" name="hidCodCajero"> 
			<input id="hidCodOficina" runat="server" type="hidden" name="hidCodOficina">
			<input id="hidCodUsuario" runat="server" type="hidden" name="hidCodUsuario"> 
			<input id="hidFlag" runat="server" type="hidden" name="hidFlag"> 
			<table border="0" cellSpacing="0" cellPadding="0" width="820">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="820" align="center">
						<table border="0" cellSpacing="0" cellPadding="0" width="820">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="810">
							<tr>
								<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 5px"
									width="98%">
									<table border="0" cellSpacing="0" cellPadding="0" width="100%">
										<tr>
											<td class="TituloRConsulta" height="30" align="center">Consulta Remesas</td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td>
												<table border="0" cellSpacing="2" cellPadding="0" align="center">
													<tr>
														<td class="Arial12b" style="WIDTH: 140px">&nbsp;&nbsp;&nbsp;Nro Bolsa :</td>
														<td style="WIDTH: 100px" class="Arial12b"><asp:textbox id="txtBolsa01" runat="server" Width="96px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox></td>
														<td class="Arial12b" width="10%" align="center">a</td>
														<td class="Arial12b">
															<asp:textbox id="txtBolsa02" runat="server" Width="90px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 140px">&nbsp;&nbsp;&nbsp;Nro Sobre :</td>
														<td style="WIDTH: 100px" class="Arial12b"><asp:textbox id="txtSobre01" runat="server" Width="96px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox></td>
														<td class="Arial12b" width="10%" align="center">a</td>
														<td class="Arial12b">
															<asp:textbox id="txtSobre02" runat="server" Width="90px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha Buzón:</td>
														<td class="Arial12b"><input style="WIDTH: 97px; HEIGHT: 17px" id="txtFechaBuzIni" class="clsInputEnable" maxLength="10"
																size="10" name="txtFechaBuzIni" runat="server">&nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmVisRem.txtFechaBuzIni');"><IMG style="Z-INDEX: 0" border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
														<td class="Arial12b" width="5%" align="center">a
														</td>
														<td class="Arial12b"><input style="WIDTH: 90px; HEIGHT: 17px" id="txtFechaBuzFin" class="clsInputEnable" maxLength="10"
																size="12" name="txtFechaBuzFin" runat="server"> &nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmVisRem.txtFechaBuzFin');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha Envio de Remesa:</td>
														<td class="Arial12b"><input style="WIDTH: 97px; HEIGHT: 17px" id="txtFechaIni" class="clsInputEnable" maxLength="10"
																size="10" name="txtFechaIni" runat="server">&nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmVisRem.txtFechaIni');"><IMG style="Z-INDEX: 0" border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
														<td class="Arial12b" width="5%" align="center">a
														</td>
														<td class="Arial12b"><input style="WIDTH: 90px; HEIGHT: 17px" id="txtFechaFin" class="clsInputEnable" maxLength="10"
																size="12" name="txtFechaFin" runat="server"> &nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmVisRem.txtFechaFin');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Usuario Envio Remesa :</td>
														<td class="Arial12b">
															<asp:textbox id="txtCodCajero" runat="server" Width="96px" CssClass="clsInputEnable" MaxLength="10"></asp:textbox>&nbsp;&nbsp;
														</td>
														<td class="Arial12b" colSpan="2">
															<div style="FLOAT:left;PADDING-TOP:2px">
																<asp:textbox id="txtCajero" runat="server" Width="140px" CssClass="clsInputDisable" MaxLength="100"
																	Enabled="False"></asp:textbox>&nbsp;
															</div>
															<div style="FLOAT:left;PADDING-TOP:2px">
																<IMG style="Z-INDEX: 0;CURSOR: hand" onclick="f_OpenCajero()" alt="Buscar Cajero" src="../images/botones/btn_Iconolupa.gif">
															</div>
															<div style="CLEAR:both"></div>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Importe :</td>
														<td style="WIDTH: 106px" class="Arial12b"><asp:textbox id="txtMonto01" runat="server" Width="96px" CssClass="clsInputEnable" MaxLength="10"></asp:textbox></td>
														<td class="Arial12b" width="5%" align="center">a
														</td>
														<td class="Arial12b"><asp:textbox id="txtMonto02" runat="server" Width="90px" CssClass="clsInputEnable" MaxLength="10"></asp:textbox></td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Tipo Remesa :</td>
														<td class="Arial12b" colSpan="3"><asp:dropdownlist id="cboTipoRemesa" runat="server" Width="290px" CssClass="clsSelectEnable" Height="20px"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Oficina de Venta :</td>
														<td style="WIDTH: 100px" class="Arial12b">
															<asp:textbox id="txtCodOficina" runat="server" Width="97px" CssClass="clsInputEnable" MaxLength="4"></asp:textbox>
														</td>
														<td class="Arial12b" colspan="2">
															<div style="FLOAT:left;PADDING-TOP:2px">
																<asp:textbox id="txtOficina" runat="server" Width="140px" CssClass="clsInputDisable" MaxLength="50"
																	Enabled="False"></asp:textbox>&nbsp;
															</div>
															<div style="FLOAT:left;PADDING-TOP:2px">
																<IMG style="Z-INDEX: 0;CURSOR: hand" onclick="f_OpenOficina()" alt="Buscar oficina" src="../images/botones/btn_Iconolupa.gif">
															</div>
															<div style="CLEAR:both"></div>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 140px">&nbsp;&nbsp;&nbsp;Documento Contable :</td>
														<td style="WIDTH: 100px" class="Arial12b"><asp:textbox id="txtDocContableIni" runat="server" Width="96px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox></td>
														<td class="Arial12b" width="10%" align="center">a</td>
														<td class="Arial12b">
															<asp:textbox id="txtDocContableFin" runat="server" Width="90px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Usuario FI :</td>
														<td class="Arial12b">
															<asp:textbox id="txtCodUsuarioFI" runat="server" Width="96px" CssClass="clsInputEnable" MaxLength="10"></asp:textbox>&nbsp;&nbsp;
														</td>
														<td class="Arial12b" colSpan="2">
															<div style="FLOAT:left;PADDING-TOP:2px">
																<asp:textbox id="txtUsuarioFI" runat="server" Width="140px" CssClass="clsInputDisable" MaxLength="100"
																	Enabled="False"></asp:textbox>&nbsp;
															</div>
															<div style="FLOAT:left;PADDING-TOP:2px">
																<IMG style="Z-INDEX: 0;CURSOR: hand" onclick="f_OpenUsuario()" alt="Buscar Cajero" src="../images/botones/btn_Iconolupa.gif">
															</div>
															<div style="CLEAR:both"></div>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha FI:</td>
														<td class="Arial12b"><input style="WIDTH: 97px; HEIGHT: 17px" id="txtFechaFI1" class="clsInputEnable" maxLength="10"
																size="10" name="txtFechaFI1" runat="server">&nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmVisRem.txtFechaFI1');"><IMG style="Z-INDEX: 0" border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
														<td class="Arial12b" width="5%" align="center">a
														</td>
														<td class="Arial12b"><input style="WIDTH: 90px; HEIGHT: 17px" id="txtFechaFI2" class="clsInputEnable" maxLength="10"
																size="12" name="txtFechaFI2" runat="server"> &nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmVisRem.txtFechaFI2');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Número de Registros :</td>
														<td style="WIDTH: 115px" class="Arial12b" colSpan="3">
															<asp:textbox id="txtCntRegistros" runat="server" Width="96px" CssClass="clsInputEnable" MaxLength="6">250</asp:textbox>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td><img border="0" src="" width="1" height="8"></td>
							</tr>
						</table>
						<br>
						<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="360">
							<tr>
								<td align="center"><input style="WIDTH: 100px" id="cmdBuscar" class="BotonOptm" value="Buscar" type="button"
										name="cmdBuscar" runat="server">&nbsp;&nbsp; <input style="WIDTH: 100px" class="BotonOptm" onclick="f_LimpiaControl();" value="Limpiar"
										type="button" name="btnLimpiar">
									<asp:Button id="loadDataHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
									<asp:Button id="loadOficinaHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
									<asp:Button id="loadCajeroHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
									<asp:Button id="loadUsuarioHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br>
			<br>
		</form>
	</body>
</HTML>
