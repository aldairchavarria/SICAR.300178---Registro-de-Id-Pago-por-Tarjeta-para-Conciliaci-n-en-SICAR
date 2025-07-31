<%@ Page Language="vb" AutoEventWireup="false" Codebehind="conRepVentaDetallada.aspx.vb" Inherits="SisCajas.conRepVentaDetallada" %>
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

				document.getElementById("hidCodVendedor").value = "";
				document.getElementById("txtFechaIni").value = "";
				document.getElementById("txtFechaFin").value = "";
				document.getElementById("txtVendedor").value = "";
				document.getElementById("txtDocSunat01").value = "";
				document.getElementById("txtDocSunat02").value = "";
				document.getElementById("txtMontoFact01").value = "";
				document.getElementById("txtMontoFact02").value = "";
				document.getElementById("txtCntRegistros").value = "250";
			}
		}
		
		function f_OpenOficina(){
			Direcc = "visorRecTrsPago_oficina.aspx"
			window.open(Direcc,"OpenOfVF","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
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
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmVisVtaFact" method="post" runat="server">
			<input id="hidCodCajero" type="hidden" name="hidCodCajero" runat="server"> <input id="hidCodOficina" type="hidden" name="hidCodOficina" runat="server">&nbsp;
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
											<td class="TituloRConsulta" height="30" align="center">Consulta de Reporte Venta 
												Detallada</td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td>
												<table border="0" cellSpacing="2" cellPadding="0" align="center">
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
														<td style="WIDTH: 130px" class="Arial12b">&nbsp;&nbsp;&nbsp;Tipo Documento :</td>
														<td class="Arial12b" colSpan="3"><asp:dropdownlist id="cboTipDoc" runat="server" AutoPostBack="True" Width="265px" CssClass="clsSelectEnable"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td style="WIDTH: 140px" class="Arial12b">&nbsp;&nbsp;&nbsp;Doc. SUNAT :</td>
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
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Forma de Pago :</td>
														<td class="Arial12b" colSpan="3"><asp:dropdownlist id="cboViaPago" runat="server" Width="265px" CssClass="clsSelectEnable"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Nro Cuotas :</td>
														<td class="Arial12b" colSpan="3"><asp:dropdownlist id="cboCuotas" runat="server" Width="265px" CssClass="clsSelectEnable"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td style="WIDTH: 130px" class="Arial12b">&nbsp;&nbsp;&nbsp;Estado Documento :</td>
														<td class="Arial12b" colSpan="3"><asp:dropdownlist id="cboEstado" runat="server" Width="265px" CssClass="clsSelectEnable"></asp:dropdownlist></td>
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
						<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="360">
							<tr>
								<td align="center"><input style="WIDTH: 100px" id="cmdBuscar" class="BotonOptm" value="Buscar" type="button"
										name="cmdBuscar" runat="server">&nbsp;&nbsp; <input style="WIDTH: 100px" class="BotonOptm" onclick="f_LimpiaControl();" value="Limpiar"
										type="button" name="btnLimpiar">
									<asp:button style="DISPLAY: none" id="loadDataHandler" runat="server" Text="Button"></asp:button><asp:button style="DISPLAY: none" id="loadVendedorHandler" runat="server" Text="Button"></asp:button></td>
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
