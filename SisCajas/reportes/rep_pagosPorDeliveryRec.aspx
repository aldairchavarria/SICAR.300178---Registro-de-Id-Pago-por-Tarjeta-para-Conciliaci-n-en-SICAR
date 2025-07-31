<%@ Page Language="vb" AutoEventWireup="false" Codebehind="rep_pagosPorDeliveryRec.aspx.vb" Inherits="SisCajas.rep_pagosPorDeliveryRec" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>rep_pagosPorDeliveryRec</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/jquery.js"></script> <!-- INI -936 -->
		<script>
		
		// Inicio INI-936 - JCI - Método refactorizado
		function validarPost(){
			var valor = document.getElementById("sValor").value;
			var fecha = document.getElementById("sFecha").value;
			var display = "block";
			
			if (valor == '' && fecha == ''){
				display = "none";
				}
			else if(valor == 0) {
				display = "none";
			}
			
			document.getElementById("tbReporte").style.display = display;
			document.getElementById("tbExportar").style.display = display;
			
			document.getElementById("txtNroPedido").value = document.getElementById("hidNroPedido").value;
			document.getElementById("txtFechaIni").value = document.getElementById("hidFechaIni").value;
			document.getElementById("txtFechaFin").value = document.getElementById("hidFechaFin").value;
			document.getElementById("txtMonto").value = document.getElementById("hidMonto").value;
			
			// Inicio INI-936 - JCI - Agregada la obtenciòn de valores de hidden y seteo a los campos luego del postback.
			var codPdv = document.getElementById("hidCodPdv").value;
			var codMedioPago = document.getElementById("hidFormaPago").value;
			var codTipoDocVenta = document.getElementById("hidTipoDoc").value;
			var codAtributo = document.getElementById("hidIdAtributo").value;
			var txtSerie = document.getElementById("hidSerieDoc").value;
			var txtCorrelativo = document.getElementById("hidCorrelativoDoc").value;
			var txtValor = document.getElementById("hidValorAtributo").value;
			
			if(codPdv != "") $("#cboPdv option[value='" + codPdv + "']").prop('selected', true);
			if(codMedioPago != "") $("#cboViaPago option[value='" + codMedioPago + "']").prop('selected', true);
			if(codTipoDocVenta != "") $("#cboTipDocVenta option[value='" + codTipoDocVenta + "']").prop('selected', true);
			if(codAtributo != "") $("#cboAtributos option[value='" + codAtributo + "']").prop('selected', true);
			if(txtSerie != "") document.getElementById("txtSerie").value = txtSerie;
			if(txtCorrelativo != "") document.getElementById("txtCorrelativo").value = txtCorrelativo;
			if(txtValor != "") document.getElementById("txtValor").value = txtValor;
			
			f_ReestablecerControles(false);
		};
		// Fin INI-936 - JCI
		
		// INI-936 - JH - Método refactorizado para ya no consultar BD al exportar y solo tomar la data de sesión
		function f_Exportar(){                        
			<%Session("reportePagosDeliveryRec") = tblExportar%>; //INI-936-JH
			document.frmTmp.action = "rep_pagosPorDeliveryRecExcel.aspx"; //INI-936-JH
			document.frmTmp.submit(); 
		};
		
		// Inicio INI-936 - JH - Funciones nuevas
		function f_ReestablecerControles(limpiar){
			var nIndice = 0;
			var txtNroPedido = document.getElementById("txtNroPedido").value;
			var txtSerie = document.getElementById("txtSerie").value;
			var txtCorrelativo = document.getElementById("txtCorrelativo").value
			var idCampo = document.getElementById("cboAtributos").selectedIndex
			var resultado = false;
						
			if(limpiar) {
				document.getElementById("txtNroPedido").value  = "";
				document.getElementById("txtFechaIni").value = "";
				document.getElementById("txtFechaFin").value  = "";
				document.getElementById("txtMonto").value = "";	
				document.getElementById("txtSerie").value  = "Serie";
				document.getElementById("txtCorrelativo").value  = "Correlativo";	
				document.getElementById("txtValor").value = "Valor";
				document.getElementById("cboPdv").selectedIndex = nIndice;
				document.getElementById("cboTipDocVenta").selectedIndex = nIndice;
				document.getElementById("cboViaPago").selectedIndex = nIndice;
				document.getElementById("cboAtributos").selectedIndex = nIndice;
				
				document.getElementById("hidNroPedido").value = "";
				document.getElementById("hidCodPdv").value = "";
				document.getElementById("hidFechaIni").value = "";
				document.getElementById("hidFechaFin").value = "";
				document.getElementById("hidFormaPago").value = "";
				document.getElementById("hidMonto").value = "";
				document.getElementById("hidTipoDoc").value = "";
				document.getElementById("hidSerieDoc").value = "";
				document.getElementById("hidCorrelativoDoc").value = "";
				document.getElementById("hidIdAtributo").value = "";
				document.getElementById("hidValorAtributo").value = "";		
				}
				else{
				if(txtNroPedido != '') {
					document.getElementById("txtSerie").value  = "Serie";
					document.getElementById("txtCorrelativo").value  = "Correlativo";
					resultado = true;
				}
				else if(txtSerie != "Serie" || txtCorrelativo != "Correlativo") {
					document.getElementById("txtNroPedido").value  = "";				
					resultado = true;
				}
				else if(idCampo != "0") {
					document.getElementById("txtSerie").value  = "Serie";
					document.getElementById("txtCorrelativo").value  = "Correlativo";
					document.getElementById("txtNroPedido").value  = "";
					resultado = true;
				}
				
				$("#txtSerie").blur();
				$("#txtCorrelativo").blur();
				$("#txtValor").blur();
				
				if(resultado) {
					document.getElementById("txtFechaIni").value = "";
					document.getElementById("txtFechaFin").value  = "";
					document.getElementById("txtMonto").value = "";
					document.getElementById("cboPdv").selectedIndex = nIndice;
					document.getElementById("cboTipDocVenta").selectedIndex = nIndice;
					document.getElementById("cboViaPago").selectedIndex = nIndice;
					document.getElementById("cboAtributos").selectedIndex = nIndice;
				}
			}
			
			return resultado;
		};
		
		function f_Buscar(){
			event.returnValue = false;
			if(f_ValidarControles()) {
				event.returnValue = true;
			}
		};
			
		function f_ValidarControles(){
			if ( f_ReestablecerControles(false) ) return true;
				else{
				if ( !ValidaFechaA('document.frmPrincipal.txtFechaIni',false) ) return false;
				if ( !ValidaFechaA('document.frmPrincipal.txtFechaFin',false) ) return false;
				if ( !FechaMayorSistema('document.frmPrincipal.txtFechaIni','FECHA INICIO') ) return false;
				if ( !FechaMayor('document.frmPrincipal.txtFechaFin','document.frmPrincipal.txtFechaIni','FECHA FIN','FECHA INICIO') ) return false;
				if ( DiferenciaFechaB('document.frmPrincipal.txtFechaFin','document.frmPrincipal.txtFechaIni') > 31 ){
					alert('No se puede realizar una búsqueda mayor a 31 días.');
					return false;
				}
				return true;	
			}
		};
		
		function f_SoloNumerosEnteros(control, evt){
			var valor = control.value;
			var unicode = evt.charCode ? evt.charCode : evt.keyCode
			
			for (i = 0; i < valor.length; i++) {
				var code = valor.charCodeAt(i);
				if(code < 48 || code > 57){          
					control.value = ""; 
					return;
				}
			}
			
			if (unicode != 8 && unicode != 9 && unicode != 44) {
				if (unicode < 48 || unicode > 57) { //Si no es un numero
					alert('Solo se permite números enteros.');
					return false; //Desactiva key press
				}     
			}
		};
		
		function f_focusInput(control) {
			var txtValue = control.value;
			if (txtValue === '')
				return true;
			else {
				control.value = '';
				control.style.color = '#000';
			}
		}
		
		function f_blurInput(control, text) {
			var txtValue = control.value;
			if (txtValue === '' || txtValue == text) {
				control.value = text;
				control.style.color = '#aaa';	
			}
			else {
				control.value = txtValue;
				control.style.color = '#000';
			}
		}
			
		function f_SoloDecimales(control, evt) {
			var charCode = (evt.which) ? evt.which : event.keyCode;
			var number = control.value.split('.');
			if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
				return false;
			}
			if (number.length > 1 && charCode == 46){
				return false;
			}
			var caratPos = getSelectionStart(control);
			var dotPos = control.value.indexOf(".");
			if(caratPos > dotPos && dotPos > -1 && (number[1].length > 1)){
				return false;
			}
			return true;
		};
		
		function getSelectionStart(control) {
			if (control.createTextRange) {
				var r = document.selection.createRange().duplicate();
				r.moveEnd('character', control.value.length);
				if (r.text == '') 
					return control.value.length
				return control.value.lastIndexOf(r.text)
			} else 
				return control.selectionStart
		};
		// Fin INI-936 - JH - Funciones nuevas		
		</script>
	</HEAD>
	<body onload="validarPost()" leftMargin="0" topMargin="0">
		<form id="frmPrincipal" method="post" runat="server">
			<input id=sValor value="<%=sValor%>" type=hidden name=sValor> <input id=sFecha value="<%=sFechaActual%>" type=hidden name=sFechaActual>
			<table id="tbFiltro" border="0" cellSpacing="0" cellPadding="0" width="820">
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
											<td class="TituloRConsulta" height="30" align="center">Reporte Pagos Delivery por 
												PDV</td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td>
												<table border="0" cellSpacing="2" cellPadding="0" align="center">
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha :</td>
														<td class="Arial12b" align="center">del: <input style="WIDTH: 80px; HEIGHT: 17px" id="txtFechaIni" class="clsInputEnable" maxLength="10"
																size="10" name="txtFechaIni" runat="server">&nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmPrincipal.txtFechaIni');"><IMG style="Z-INDEX: 0" border="0" src="../images/botones/btn_Calendario.gif"></A>
														</td>
														<td class="Arial12b" align="center">al: <input style="WIDTH: 80px; HEIGHT: 17px" id="txtFechaFin" class="clsInputEnable" maxLength="10"
																size="10" name="txtFechaFin" runat="server"> &nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmPrincipal.txtFechaFin');"><IMG style="Z-INDEX: 0" border="0" src="../images/botones/btn_Calendario.gif"></A>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Punto de Venta :</td>
														<td class="Arial12b" colSpan="3"><asp:dropdownlist id="cboPdv" runat="server" CssClass="clsSelectEnable" Width="280px"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td style="WIDTH: 130px" class="Arial12b">&nbsp;&nbsp;&nbsp;Tipo Documento :</td>
														<td class="Arial12b" colSpan="3"><asp:dropdownlist id="cboTipDocVenta" runat="server" CssClass="clsSelectEnable" Width="280px"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Forma de Pago :</td>
														<td class="Arial12b" colSpan="3"><asp:dropdownlist id="cboViaPago" runat="server" CssClass="clsSelectEnable" Width="280px"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Monto :</td>
														<td>
															<!--Se agrego el evento onkeypress - INI-936-JH -->
															<input style="WIDTH: 155px; HEIGHT: 17px" id="txtMonto" class="clsInputEnable" onkeypress="return f_SoloDecimales(this,event)" runat="server">
														</td>
													</tr>
												</table>
											</td>
											<td>
												<table>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;N°de Pedido :</td>
														<td>
															<!-- INI-936-JH - Se agrego la funcion "f_SoloNumerosEnteros" al evento "onkeypress" -->
															<input style="WIDTH: 155px; HEIGHT: 17px" id="txtNroPedido" class="clsInputEnable" onkeypress="return f_SoloNumerosEnteros(this, event)" runat="server">
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Documento :</td>
														<td>
															<!-- INI-936-JH - Se agregaron las funciones "f_focusInput" y "f_blurInput" a los eventos "onfocus" y "onblur" -->
															<input style="WIDTH: 45px; HEIGHT: 17px; COLOR: #aaa" id="txtSerie" class="clsInputEnable" value="Serie" runat="server" 
																onfocus="f_focusInput(this)" onblur="f_blurInput(this, 'Serie')">
															- <input style="WIDTH: 85px; HEIGHT: 17px; COLOR: #aaa" id="txtCorrelativo" class="clsInputEnable" value="Correlativo" runat="server" 
																onfocus="f_focusInput(this)" onblur="f_blurInput(this, 'Correlativo')">
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Atributos :</td>
														<td class="Arial12b" colSpan="3"><asp:dropdownlist id="cboAtributos" runat="server" CssClass="clsSelectEnable" Width="110px"></asp:dropdownlist>
                                                                                                                       <!-- INI-936-JH - Se agregaron las funciones "f_focusInput" y "f_blurInput" a los eventos "onfocus" y "onblur" -->
															<input style="WIDTH: 85px; HEIGHT: 17px; COLOR: #aaa" id="txtValor" class="clsInputEnable"
																value="Valor" runat="server" onfocus="f_focusInput(this)" onblur="f_blurInput(this, 'Valor')">
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
								<td align="center">
									<!-- INI-936 - JH - Se elimino el input cmdBuscar anterior, y se creó un botón ASP -->
									<asp:button id="btnBuscar" runat="server" Width="100px" CssClass="BotonOptm" Text="Buscar"></asp:button>
									<!-- INI-936 - JH - Se elimino el input btnLimpiar anterior, y se creó un botón ASP -->
									<asp:button id="btnClear" runat="server" Width="100px" CssClass="BotonOptm" Text="Limpiar"></asp:button>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br>
			<br>
			<table style="DISPLAY: none" id="tbReporte" border="0" cellSpacing="0" cellPadding="0"
				width="820">
				<tr>
					<td vAlign="top" width="820"><br>
						<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="790" align="center">
							<tr>
								<td align="center">
									<table class="Arial10B" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td height="4" width="10" border="0"></td>
											<td class="TituloRConsulta" height="32" width="98%" align="center">Datos</td>
											<td height="32" vAlign="top" width="14"></td>
										</tr>
									</table>
								</td>
							</tr>
							<TR>
								<TD>
									<TABLE id="Table5" class="Arial12b" cellPadding="3">
										<tbody>
											<tr>
												<td style="PADDING-BOTTOM: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 0px">
													<div style="Z-INDEX: 0; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 800px; HEIGHT: 400px" class="frame2">
                                                                                                                <!-- INI-936 - Modificado PageSize de 5 a 10 -->
                                                                                                                <asp:datagrid style="Z-INDEX: 0" id="DGLista" runat="server" CssClass="Arial12b" EnableViewState="True"
															PageSize="10" PagerStyle-Mode="NumericPages" AllowPaging="true" AutoGenerateColumns="True">
															<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
															<ItemStyle CssClass="RowOdd"></ItemStyle>
															<HeaderStyle CssClass="Arial12b"></HeaderStyle>
															<PagerStyle Mode="NumericPages"></PagerStyle>
														</asp:datagrid></div>
												</td>
											</tr>
										</tbody>
									</TABLE>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			</TD></TR></TABLE>
			<table style="DISPLAY: none" id="tbExportar" border="0" cellSpacing="0" cellPadding="0"
				width="800">
				<tr>
					<td>
						<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="4" width="360" align="center">
							<tr>
								<td>
									<table class="Arial10B" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td align="center"><input style="WIDTH: 100px" id="btnExportar" class="BotonOptm" onclick="javascript:f_Exportar();"
													value="Exportar Excel" type="button" name="btnExportar" runat="server">&nbsp;&nbsp;
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
		<form method="post" name="frmTmp" action="" target="_blank">
		</form>
                <!-- Inicio - INI-936 - JCI - Agregados Hidden para guardar valors de búsqueda -->
		<input type="hidden" id="hidNroPedido" runat="server">
		<input type="hidden" id="hidCodPdv" runat="server">
		<input type="hidden" id="hidFechaIni" runat="server">
		<input type="hidden" id="hidFechaFin" runat="server">
		<input type="hidden" id="hidFormaPago" runat="server">
		<input type="hidden" id="hidMonto" runat="server">
		<input type="hidden" id="hidTipoDoc" runat="server">
		<input type="hidden" id="hidSerieDoc" runat="server">
		<input type="hidden" id="hidCorrelativoDoc" runat="server">
		<input type="hidden" id="hidIdAtributo" runat="server">
		<input type="hidden" id="hidValorAtributo" runat="server">
                <!-- Fin - INI-936 - JCI -->
	</body>
</HTML>
