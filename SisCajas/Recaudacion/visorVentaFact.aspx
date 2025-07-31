<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorVentaFact.aspx.vb" Inherits="SisCajas.visorVentaFact"%>
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
	
		function f_DesactivarControles()
		{
			document.getElementById("btnOpenSubOficina").style.visibility = "hidden";
			document.getElementById("btnOpenOficina").style.visibility = "hidden";
			document.getElementById("chkSubOficina").style.visibility = "hidden";
			document.getElementById("chkOficina").style.visibility = "hidden";
			
	
			document.getElementById("lbOficina").disabled = true;
			document.getElementById("lbCodSubOficina").disabled = true;
			document.getElementById("lbSubOficina").disabled = true;
		}
		
		function f_OpenSubOficina(){
			Direcc = "../Pagos/ConsultaSubOficina.aspx"
			window.open(Direcc,"OpenOfLC","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_CargarDatosSubOficina(){
			document.getElementById('loadDataSubOfi').click();
		}
		
		function checkEnabledSubOfi(bCheck){
			if(bCheck){
				var lb1 = document.getElementById("lbCodSubOficina");
				var lb2 = document.getElementById("lbSubOficina");
				removeAllItems(lb1);
				removeAllItems(lb2);
				document.getElementById("lbSubOficina").disabled = true;
				document.getElementById("lbCodSubOficina").disabled = true;
				document.getElementById("hidSubOficina").value = "";
				document.getElementById("lbSubOficina").className = "listBozCssClass";
				document.getElementById("lbCodSubOficina").className = "listBozCssClass";
			}else{
				document.getElementById("lbCodSubOficina").className = "";
				document.getElementById("lbSubOficina").className = "";
			}
		}
	
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
			with(frmVisVtaFact) {
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
				
				cboEstado.selectedIndex = nIndice;
				cboViaPago.selectedIndex = nIndice;
				cboCuotas.selectedIndex = nIndice;
				cboTipDoc.selectedIndex = nIndice;
				
				//document.getElementById("txtFechaIni").value = today;
				//document.getElementById("txtFechaFin").value = today;
				
				document.getElementById("hidCodCajero").value = "";
				document.getElementById("hidCodOficina").value = "";
				document.getElementById("hidCodVendedor").value = "";
				//document.getElementById("txtCodOficina").value = "";
				document.getElementById("txtCodCajero").value = "";
				//document.getElementById("txtOficina").value = "";
				document.getElementById("txtCajero").value = "";
				document.getElementById("txtCodVendedor").value = "";
				document.getElementById("txtVendedor").value = "";
				
				document.getElementById("txtDocSunat01").value = "";
				document.getElementById("txtDocSunat02").value = "";
				document.getElementById("txtMontoFact01").value = "";
				document.getElementById("txtMontoFact02").value = "";
				document.getElementById("txtCntRegistros").value = "250";
				
				if (document.getElementById("chkSubOficina").style.visibility != "hidden")
				{
					document.getElementById("chkTodosSubOf").checked = true;
					checkEnabledSubOfi(true);
					checkEnabledOfi(true);
				
				}
			}
		}
		
		function f_OpenOficina(){
			//Direcc = "visorRecTrsPago_oficina.aspx"
			Direcc = "../Pagos/ConsultaCajaTot_oficina.aspx";
			window.open(Direcc,"OpenOfI","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_CargarDatosOficina(){
			frmVisVtaFact.loadDataHandler.click();			
		}
		
		function f_buscarOficina(){
			if(event.keyCode == 13 ){
				frmVisVtaFact.loadOficinaHandler.click();
			}
		}		
		
		function f_OpenCajero(){
			var of = document.getElementById("hidCodOficina").value;	
			Direcc = "visorRecTrsPago_cajero.aspx?of=" + of;
			window.open(Direcc,"OpenCajVF","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_CargarDatosCajero(){
			frmVisVtaFact.loadDataHandler.click();			
		}
		
		function f_buscarCajero(){
			if(event.keyCode == 13 ){
				frmVisVtaFact.loadCajeroHandler.click();
			}
		}
		
		function f_OpenVendedor(){
			var of = document.getElementById("hidCodVendedor").value;	
			Direcc = "visorVentaFact_vendedor.aspx?of=" + of;
			window.open(Direcc,"OpenVenVF","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_CargarDatosVendedor(){
			frmVisVtaFact.loadDataHandler.click();			
		}
		
		function f_buscarVendedor(){
			if(event.keyCode == 13 ){
				frmVisVtaFact.loadVendedorHandler.click();
			}
		}
		
		function checkEnabledOfi(bCheck){
			if(bCheck){
				var lb2 = document.getElementById("lbOficina");
				removeAllItems(lb2);
				lb2.disabled = true;
				document.getElementById("hidCodOficina").value = "";
				document.getElementById("lbOficina").className = "listBozCssClass";
			}else{
				document.getElementById("lbOficina").className = "";
			}
		}

		function removeAllItems(listBox){
			var i;
			for(i=listBox.options.length-1;i>=0;i--){
				listBox.remove(i);
			}
		}
		
		function f_OcultarBotonera(){
			setVisible('trOpciones', false);
			setVisible('trProcesando', true);
		}
		
		</script>
		<style type="text/css">
			.listBozCssClass { BORDER-BOTTOM: #bfbee9 1px solid; BORDER-LEFT: #bfbee9 1px solid; BACKGROUND-COLOR: #bfbee9; BORDER-TOP: #bfbee9 1px solid; BORDER-RIGHT: #bfbee9 1px solid }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmVisVtaFact" method="post" runat="server">
			<input id="hidCodCajero" runat="server" type="hidden" name="hidCodCajero"> <input id="hidCodOficina" runat="server" type="hidden" name="hidCodOficina">
			<input id="hidCodVendedor" runat="server" type="hidden" name="hidCodVendedor">
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
											<td class="TituloRConsulta" height="30" align="center">Consulta Facturación 
												Detallada</td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td>
												<table border="0" cellSpacing="2" cellPadding="0" align="center">
													<tr id="rowOficina">
														<td class="Arial12b">&nbsp;&nbsp;Oficina de Venta :</td>
														<td class="Arial12b" colspan="3">
															<table>
																<tr>
																	<td>
																		<div>
																<asp:ListBox id="lbOficina" runat="server" Width="180px" BorderWidth="0px" Rows="3" Font-Name="Arial"
																	Font-Size="8" Height="70px"></asp:ListBox>
															</div>
														</td>
														<td class="Arial12b">
																		<div style="POSITION:relative;PADDING-LEFT:5px">
																			<IMG id="btnOpenOficina" style="CURSOR: hand" onclick="f_OpenOficina()" alt="Buscar oficina"
																				src="../images/botones/btn_Iconolupa.gif">
															</div>
														</td>
																	<td id="chkOficina" class="Arial12b">
																		<div style="POSITION:relative">
															<asp:CheckBox ID="chkTodosOf" Runat="server" Checked="False" Text="Todos" onclick="checkEnabledOfi(this.checked)"></asp:CheckBox>
															</div>	
														</td>
													</tr>
															</table>
														</td>
													<!--PROY-33111 INI-->
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Sub Oficina de Venta :</td>
														<td class="Arial12b" colspan="3">
															<table>
																<tr>
																	<td>
																		<asp:listbox id="lbCodSubOficina" runat="server" Font-Size="8" Font-Name="Arial" Rows="3" BorderWidth="0px"
																			Width="80px"></asp:listbox>&nbsp;&nbsp;&nbsp;
																		<asp:listbox id="lbSubOficina" runat="server" Font-Size="8" Font-Name="Arial" Rows="3" BorderWidth="0px"
																			Width="120px"></asp:listbox>
																	<td>
																		<IMG id="btnOpenSubOficina" style="Z-INDEX: 0;CURSOR: hand" onclick="f_OpenSubOficina()"
																			alt="Buscar Sub oficina" src="../images/botones/btn_Iconolupa.gif">
																	</td>
																	<td id="chkSubOficina">
																		<asp:checkbox CssClass="Arial12b" id="chkTodosSubOf" onclick="checkEnabledSubOfi(this.checked)"
																			Checked="False" Runat="server" Text="Todos"></asp:checkbox>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
													<!--PROY-33111 INI-->
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha :</td>
														<td class="Arial12b"><input style="WIDTH: 97px; HEIGHT: 17px" id="txtFechaIni" class="clsInputEnable" maxLength="10"
																size="10" name="txtFechaIni" runat="server">&nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmVisVtaFact.txtFechaIni');"><IMG style="Z-INDEX: 0" border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
														<td class="Arial12b" width="5%" align="center">a
														</td>
														<td class="Arial12b"><input style="WIDTH: 90px; HEIGHT: 17px" id="txtFechaFin" class="clsInputEnable" maxLength="10"
																size="12" name="txtFechaFin" runat="server"> &nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmVisVtaFact.txtFechaFin');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Tipo Documento :</td>
														<td class="Arial12b" colspan="3">
															<asp:dropdownlist id="cboTipDoc" runat="server" CssClass="clsSelectEnable" Width="265px" AutoPostBack="True"></asp:dropdownlist>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 140px">&nbsp;&nbsp;&nbsp;Doc. SUNAT :</td>
														<td style="WIDTH: 100px" class="Arial12b"><asp:textbox id="txtDocSunat01" runat="server" Width="96px" CssClass="clsInputEnable" MaxLength="20"></asp:textbox></td>
														<td class="Arial12b" width="10%" align="center">a
														</td>
														<td class="Arial12b"><asp:textbox id="txtDocSunat02" runat="server" Width="90px" CssClass="clsInputEnable" MaxLength="20"></asp:textbox></td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Total Facturado :</td>
														<td style="WIDTH: 106px" class="Arial12b"><asp:textbox id="txtMontoFact01" runat="server" Width="96px" CssClass="clsInputEnable" MaxLength="10"></asp:textbox></td>
														<td class="Arial12b" width="5%" align="center">a
														</td>
														<td class="Arial12b"><asp:textbox id="txtMontoFact02" runat="server" Width="90px" CssClass="clsInputEnable" MaxLength="10"></asp:textbox></td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Cajero :</td>
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
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Vendedor :</td>
														<td class="Arial12b">
															<asp:textbox id="txtCodVendedor" runat="server" Width="96px" CssClass="clsInputEnable" MaxLength="10"></asp:textbox>&nbsp;&nbsp;
														</td>
														<td class="Arial12b" colSpan="2">
															<div style="FLOAT:left;PADDING-TOP:2px">
																<asp:textbox id="txtVendedor" runat="server" Width="140px" CssClass="clsInputDisable" MaxLength="100"
																	Enabled="False"></asp:textbox>&nbsp;
															</div>
															<div style="FLOAT:left;PADDING-TOP:2px">
																<IMG style="Z-INDEX: 0;CURSOR: hand" onclick="f_OpenVendedor()" alt="Buscar Vendedor"
																	src="../images/botones/btn_Iconolupa.gif">
															</div>
															<div style="CLEAR:both"></div>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Forma de Pago :</td>
														<td class="Arial12b" colSpan="3">
															<asp:dropdownlist id="cboViaPago" runat="server" Width="265px" CssClass="clsSelectEnable"></asp:dropdownlist>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Nro Cuotas :</td>
														<td class="Arial12b" colSpan="3">
															<asp:dropdownlist id="cboCuotas" runat="server" Width="265px" CssClass="clsSelectEnable"></asp:dropdownlist>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Estado Documento :</td>
														<td class="Arial12b" colspan="3">
															<asp:dropdownlist id="cboEstado" runat="server" CssClass="clsSelectEnable" Width="265px"></asp:dropdownlist>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Número de Registros :</td>
														<td style="WIDTH: 115px" class="Arial12b" colSpan="3"><asp:textbox id="txtCntRegistros" runat="server" Width="96px" CssClass="clsInputEnable" MaxLength="6">250</asp:textbox></td>
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
						<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="550" align="center" border="1">
							<tr id="trOpciones">
								<td align="center"><input style="WIDTH: 100px" id="cmdBuscar" class="BotonOptm" value="Buscar" type="button"
										name="cmdBuscar" runat="server">&nbsp;&nbsp; <input style="WIDTH: 100px" class="BotonOptm" onclick="f_LimpiaControl();" value="Limpiar"
										type="button" name="btnLimpiar">
									<asp:Button id="loadDataHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
									<asp:Button id="loadOficinaHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
									<asp:Button id="loadCajeroHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
									<asp:Button id="loadVendedorHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
									<asp:Button id="loadDataSubOfi" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
								</td>
							</tr>
							<tr id="trProcesando" align="center" style="display:none;color: #F00; font-weight: bold; font-size: 16px;">
								<td>
									<label>PROCESANDO…</label><img src="..\images\ajax-loader.gif"></img>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br>
			<br>
			<input id="hidSubOficina" runat="server" type="hidden" name="hidSubOficina">
		</form>
	</body>
</HTML>
