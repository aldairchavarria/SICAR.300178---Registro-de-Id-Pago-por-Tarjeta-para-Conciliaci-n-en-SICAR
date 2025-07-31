<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorRecDeuda.aspx.vb" Inherits="SisCajas.visorRecDeuda" %>
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
		//Proy-33111
		function f_DesactivarControles()
		{
			document.getElementById("btnOpenSubOficina").style.visibility = "hidden";
			document.getElementById("btnOpenOficina").style.visibility = "hidden";
			document.getElementById("chkSubOficina").style.visibility = "hidden";
		
			document.getElementById("txtCodOficina").disabled = true;
			document.getElementById("lbCodOficina").disabled = true;
			document.getElementById("lbOficina").disabled = true;
					
		}
		
		function f_OpenSubOficina(){
			Direcc = "../Pagos/ConsultaSubOficina.aspx"
			window.open(Direcc,"OpenOfLC","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_CargarDatosSubOficina(){
			document.getElementById('loadDataSubOfi').click();
		}
		
		function checkEnabledOfi(bCheck){
			if(bCheck){
				var lb1 = document.getElementById("lbCodOficina");
				var lb2 = document.getElementById("lbOficina");
				removeAllItems(lb1);
				removeAllItems(lb2);
				document.getElementById("lbOficina").disabled = true;
				document.getElementById("lbCodOficina").disabled = true;
				document.getElementById("hidSubOficina").value = "";
				document.getElementById("lbOficina").className = "listBozCssClass";
				document.getElementById("lbCodOficina").className = "listBozCssClass";
			}else{
				document.getElementById("lbCodOficina").className = "";
				document.getElementById("lbOficina").className = "";
			}
		}
		
		function removeAllItems(listBox){
			var i;
			for(i=listBox.options.length-1;i>=0;i--){
				listBox.remove(i);
			}
		}
		//Proy-33111
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
			with(frmVisDeuda) {
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
				cboMoneda.selectedIndex = nIndice;
				
				//document.getElementById("txtFechaIni").value = today;
				//document.getElementById("txtFechaFin").value = today;
				
				document.getElementById("hidCodOficina").value = "";
				document.getElementById("hidCodCajero").value = "";
				//document.getElementById("txtCodOficina").value = "";
				document.getElementById("txtCodCajero").value = "";
				//document.getElementById("txtOficina").value = "";
				document.getElementById("txtCajero").value = "";
				document.getElementById("txtTipoDoc").value = "";
				
				document.getElementById("txtNroTran01").value = "";
				document.getElementById("txtNroTran02").value = "";
				document.getElementById("txtMtoTotPagado01").value = "";
				document.getElementById("txtMtoTotPagado02").value = "";
				document.getElementById("txtRuc1").value = "";
				document.getElementById("txtRuc2").value = "";
				document.getElementById("txtCntRegistros").value = "250";
				
				if (document.getElementById("chkSubOficina").style.visibility != "hidden")
				{
					document.getElementById("chkTodosOf").checked = true;
					checkEnabledOfi(true);
					document.getElementById("txtCodOficina").value = "";
				    document.getElementById("txtOficina").value = "";
				}
			}
		}
			
		function f_OpenOficina(){
			Direcc = "visorRecTrsPago_oficina.aspx"
			window.open(Direcc,"OpenOf","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
			
		function f_CargarDatosOficina(){
			frmVisDeuda.loadDataHandler.click();			
		}
		
		function f_OpenCajero(){
			var of = document.getElementById("hidCodOficina").value;	
			Direcc = "visorRecTrsPago_cajero.aspx?of=" + of;
			window.open(Direcc,"OpenCaj","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_CargarDatosCajero(){
			frmVisDeuda.loadDataHandler.click();			
		}
		
		function f_buscarOficina(){
			if(event.keyCode == 13 ){
				frmVisDeuda.loadOficinaHandler.click();
			}
		}
		
		function f_buscarCajero(){
			if(event.keyCode == 13 ){
				frmVisDeuda.loadCajeroHandler.click();
			}
		}
			
	</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmVisDeuda" method="post" runat="server">
			<input id="hidCodCajero" runat="server" type="hidden" name="hidCodCajero"> 
			<input id="hidCodOficina" runat="server" type="hidden" name="hidCodOficina">
			<table border="0" cellSpacing="0" cellPadding="0" width="820">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="820" align="center">
						<table border="0" cellSpacing="0" cellPadding="0" width="810" height="14">
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
											<td class="TituloRConsulta" height="30" align="center">Consulta Recaudaciones - 
												Deuda</td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td>
												<table border="0" cellSpacing="2" cellPadding="0" align="center">
													<tr>
														<td class="Arial12b" style="WIDTH: 140px">&nbsp;&nbsp;&nbsp;Nro Transacción :</td>
														<td class="Arial12b" style="WIDTH: 100px">
															<asp:textbox id="txtNroTran01" runat="server" MaxLength="15" CssClass="clsInputEnable" Width="97px"></asp:textbox>
														</td>
														<td class="Arial12b" width="10%" align="center">
															a
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroTran02" runat="server" MaxLength="15" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Oficina de Venta :</td>
														<td style="WIDTH: 100px" class="Arial12b">
															<asp:textbox id="txtCodOficina" runat="server" Width="97px" CssClass="clsInputEnable" MaxLength="4"></asp:textbox>
														</td>
														<td class="Arial12b" colspan="2">
															<div style="float:left;padding-top:2px;">
																<asp:textbox id="txtOficina" runat="server" Width="140px" CssClass="clsInputDisable" MaxLength="50" Enabled="False"></asp:textbox>&nbsp;
															</div>
															<div id="btnOpenOficina" style="float:left;padding-top:2px;">
																<IMG style="CURSOR: hand;Z-INDEX: 0;" onclick="f_OpenOficina()" alt="Buscar oficina" src="../images/botones/btn_Iconolupa.gif">
															</div>
															<div style="clear:both;"></div> 
														</td>
													</tr>
													<!--PROY-33111 INI-->
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Sub Oficina de Venta :</td>
														<td class="Arial12b" colspan="3">
															<table>
																<tr>
																	<td>
																		<asp:listbox id="lbCodOficina" runat="server" Font-Size="8" Font-Name="Arial" Rows="3" BorderWidth="0px"
																			Width="80px"></asp:listbox>&nbsp;&nbsp;&nbsp;
																		<asp:listbox id="lbOficina" runat="server" Font-Size="8" Font-Name="Arial" Rows="3" BorderWidth="0px"
																			Width="120px"></asp:listbox>
																	<td>
																		<IMG id="btnOpenSubOficina" style="Z-INDEX: 0;CURSOR: hand" onclick="f_OpenSubOficina()"
																			alt="Buscar Sub oficina" src="../images/botones/btn_Iconolupa.gif">
																	</td>
																	<td id="chkSubOficina">
																		<asp:checkbox CssClass="Arial12b" id="chkTodosOf" onclick="checkEnabledOfi(this.checked)" Checked="False"
																			Runat="server" Text="Todos"></asp:checkbox>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
													<!--PROY-33111 INI-->
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Fecha Transacción :</td>
														<td class="Arial12b">
															<input id="txtFechaIni" class="clsInputEnable" maxLength="10" size="12" name="txtFechaIni"
																runat="server" style="WIDTH: 97px; HEIGHT: 17px">&nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmVisDeuda.txtFechaIni');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
														<td class="Arial12b" width="5%" align="center">
															a
														</td>
														<td class="Arial12b">
															<input id="txtFechaFin" class="clsInputEnable" maxLength="10" size="12" name="txtFechaFin"
																runat="server" style="WIDTH: 90px; HEIGHT: 17px"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmVisDeuda.txtFechaFin');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px; HEIGHT: 19px">&nbsp;&nbsp;&nbsp;Moneda :</td>
														<td class="Arial12b" colspan="3" style="HEIGHT: 19px">
															<asp:dropdownlist id="cboMoneda" runat="server" CssClass="clsSelectEnable" Width="265px"></asp:dropdownlist>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Importe Pago :</td>
														<td class="Arial12b">
															<asp:textbox id="txtMtoTotPagado01" runat="server" MaxLength="10" CssClass="clsInputEnable" Width="97px"></asp:textbox>
														</td>
														<td class="Arial12b" width="5%" align="center">
															a
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtMtoTotPagado02" runat="server" MaxLength="10" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Estado Transacción :</td>
														<td class="Arial12b" colspan="3">
															<asp:dropdownlist id="cboEstado" runat="server" CssClass="clsSelectEnable" Width="265px"></asp:dropdownlist>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Nro. Teléfono :</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroTelefono01" runat="server" MaxLength="10" CssClass="clsInputEnable" Width="97px"></asp:textbox>
														</td>
														<td class="Arial12b" width="5%" align="center">
															a
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroTelefono02" runat="server" MaxLength="10" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Cajero :</td>
														<td class="Arial12b">
															<asp:textbox id="txtCodCajero" runat="server" Width="96px" CssClass="clsInputEnable" MaxLength="10" ></asp:textbox>&nbsp;&nbsp;
														</td>
														<td class="Arial12b" colspan="2">
															<div style="float:left;padding-top:2px;">
																<asp:textbox id="txtCajero" runat="server" Width="140px" CssClass="clsInputDisable" MaxLength="100" Enabled="False"></asp:textbox>&nbsp;
															</div>
															<div style="float:left;padding-top:2px;">
																<IMG style="CURSOR: hand;Z-INDEX: 0;" onclick="f_OpenCajero()" alt="Buscar Cajero" src="../images/botones/btn_Iconolupa.gif">
															</div>
															<div style="clear:both;"></div>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px; HEIGHT: 18px">&nbsp;&nbsp;&nbsp;Tipo Doc. 
															Deudor :</td>
														<td class="Arial12b" colspan="3" style="HEIGHT: 18px">
															<asp:textbox id="txtTipoDoc" runat="server" MaxLength="2" CssClass="clsInputEnable" Width="97px"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Nro. Doc. Deudor :</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroDoc01" runat="server" MaxLength="15" CssClass="clsInputEnable" Width="97px"></asp:textbox>
														</td>
														<td class="Arial12b" width="5%" align="center">
															a
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroDoc02" runat="server" MaxLength="15" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Ruc Deudor :</td>
														<td style="WIDTH: 106px" class="Arial12b"><asp:textbox id="txtRuc1" runat="server" Width="96px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox></td>
														<td class="Arial12b" width="5%" align="center">a
														</td>
														<td class="Arial12b"><asp:textbox id="txtRuc2" runat="server" Width="90px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox></td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Número de Registros :</td>
														<td class="Arial12b" colspan="3" style="WIDTH: 115px">
															<asp:textbox id="txtCntRegistros" runat="server" MaxLength="6" CssClass="clsInputEnable" Width="97px">250</asp:textbox>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td><img height="8" src="" width="1" border="0"></td>
							</tr>
						</table>
						<br>
						<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="360">
							<tr>
								<td align="center">
									<input style="WIDTH: 100px" id="cmdBuscar" class="BotonOptm" value="Buscar" type="button" name="cmdBuscar" runat="server">&nbsp;&nbsp;
									<input class="BotonOptm" style="WIDTH: 100px" onclick="f_LimpiaControl();" type="button"
										value="Limpiar" name="btnLimpiar">
									<asp:Button id="loadDataHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
									<asp:Button id="loadOficinaHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
									<asp:Button id="loadCajeroHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
									<asp:Button id="loadDataSubOfi" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
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
