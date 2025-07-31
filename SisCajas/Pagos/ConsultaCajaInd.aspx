<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConsultaCajaInd.aspx.vb" Inherits="SisCajas.ConsultaCajaInd" %>
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
			
		function f_LimpiaControl() {
			var nIndice = 0;
			with(frmConsultaCajaInd) {
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
				document.getElementById("txtFechaIni").value = today;
				document.getElementById("txtFechaFin").value = today;
				document.getElementById("txtCajero").value = "";
				document.getElementById("txtCaja").value = "";
				document.getElementById("hidCodOficina").value = "";
				document.getElementById("hidCodCajero").value = "";
				document.getElementById("hidCodCaja").value = "";
				document.getElementById("txtCodCajero").value = "";
				var lb1 = document.getElementById("lbCodOficina");
				var lb2 = document.getElementById("lbOficina");
				removeAllItems(lb1);
				removeAllItems(lb2);
				document.getElementById("lbCodOficina").className = "";
				document.getElementById("lbOficina").className = "";
			}
		}
		
		function f_OpenOficina(){
			Direcc = "ConsultaCajaTot_oficina.aspx"
			window.open(Direcc,"OpenOfI","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_OpenCajero(){
			var of = document.getElementById("hidCodOficina").value;
			Direcc = "ConsultaCajaTot_Cajero.aspx?of=" + of;
			window.open(Direcc,"OpenCajI","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_OpenCaja(){
			var of = document.getElementById("hidCodOficina").value;
			Direcc = "ConsultaCajaTot_caja.aspx?of=" + of;
			window.open(Direcc,"OpenCaI","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_CargarDatosOficina(){
			frmConsultaCajaInd.loadDataHandler.click();			
		}
		
		function f_CargarDatosCajero(){
			frmConsultaCajaInd.loadDataHandler.click();
		}
		
		function f_CargarDatosCaja(){
			frmConsultaCajaInd.loadDataHandler.click();
		}
		
		function ValidaNumero(obj){
			var KeyAscii = window.event.keyCode;
			if (KeyAscii==13) return;	
			if (!(KeyAscii >= 47 && KeyAscii<=57) ){		
				window.event.keyCode = 0;
			}	
		}
		
		function f_buscarCajero(){
			if(event.keyCode == 13 ){
				frmConsultaCajaInd.loadCajeroHandler.click();
			}
		}
		
		function checkEnabledOfi(bCheck){
			if(bCheck){
				var lb1 = document.getElementById("lbCodOficina");
				var lb2 = document.getElementById("lbOficina");
				removeAllItems(lb1);
				removeAllItems(lb2);
				document.getElementById("lbOficina").disabled = true;
				document.getElementById("lbCodOficina").disabled = true;
				document.getElementById("hidCodOficina").value = "";
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
		//Inicio - INI-936 - JH - Creadas funciones de validacion
		function f_Buscar(){
			event.returnValue = false;
			if(f_ValidarControles()){
				event.returnValue = true;
			}
		};
		function f_ValidarControles(){
			var vrOficinas = document.getElementById("hidCodOficina").value;
			var vrChecked = document.getElementById("chkTodosOf");
			
			if(vrOficinas.length === 0 && !vrChecked.checked){
				alert('Seleccione oficina.');
				return false;
			}else{
				if (!ValidaFechaA('document.frmConsultaCajaInd.txtFechaIni',false)) return false;
				if (!ValidaFechaA('document.frmConsultaCajaInd.txtFechaFin',false)) return false;
				if (!FechaMayorSistema('document.frmConsultaCajaInd.txtFechaIni','FECHA INICIO')) return false;
				if (!FechaMayor('document.frmConsultaCajaInd.txtFechaFin','document.frmConsultaCajaInd.txtFechaIni','FECHA FIN','FECHA INICIO')) return false;
				return true;
			}
		};
		//Fin - INI-936 - JH - Creadas funciones de validacion
		
		</script>
		
		<style type="text/css">
			
			.listBozCssClass{
				BORDER-RIGHT: #bfbee9 1px solid;
				BORDER-TOP: #bfbee9 1px solid;
				BORDER-LEFT: #bfbee9 1px solid;
				BORDER-BOTTOM: #bfbee9 1px solid;
				BACKGROUND-COLOR: #bfbee9
			}
		
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmConsultaCajaInd" method="post" runat="server">
			<input id="hidCodCajero" runat="server" type="hidden" name="hidCodCajero"> <input id="hidCodOficina" runat="server" type="hidden" name="hidCodOficina">
			<input id="hidCodCaja" runat="server" type="hidden" name="hidCodCaja">
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
											<td class="TituloRConsulta" height="30" align="center">Consulta de Caja - 
												Individual</td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td>
												<table border="0" cellSpacing="2" cellPadding="0" align="center">
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;Oficina de Venta :</td>
														<td class="Arial12b" style="WIDTH: 100px">
															&nbsp;<asp:ListBox id="lbCodOficina" runat="server" Width="90px" BorderWidth="0px" Rows="3" Font-Name="Arial" Font-Size="8"></asp:ListBox>
														</td>
														<td class="Arial12b" colspan="2">
															<div style="FLOAT:left;">
																<asp:ListBox id="lbOficina" runat="server" Width="140px" BorderWidth="0px" Rows="3" Font-Name="Arial" Font-Size="8"></asp:ListBox>
															</div>
															<div style="FLOAT:left;PADDING-TOP:20px;padding-left:5px">
																<IMG style="CURSOR: hand" onclick="f_OpenOficina()" alt="Buscar oficina" src="../images/botones/btn_Iconolupa.gif">
															</div>
															<div style="CLEAR:both"></div>
														</td>
														<td class="Arial12b">
															<asp:CheckBox ID="chkTodosOf" Runat="server" Checked="False" Text="Todos" onclick="checkEnabledOfi(this.checked)"></asp:CheckBox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha :</td>
														<td colspan="4" class="Arial12b">
															<input id="txtFechaIni" class="clsInputEnable" maxLength="10" size="12" name="txtFechaIni"
																runat="server" style="WIDTH: 95px; HEIGHT: 17px"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmConsultaCajaInd.txtFechaIni');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
															&nbsp;&nbsp;&nbsp;a &nbsp;&nbsp;&nbsp;&nbsp;<input id="txtFechaFin" class="clsInputEnable" maxLength="10" size="12" name="txtFechaFin"
																runat="server" style="WIDTH: 95px; HEIGHT: 17px"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmConsultaCajaInd.txtFechaFin');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Cajero :
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtCodCajero" runat="server" Width="96px" CssClass="clsInputEnable" MaxLength="10"></asp:textbox>&nbsp;&nbsp;
														</td>
														<td class="Arial12b" colspan="2">
															<div style="FLOAT:left;PADDING-TOP:2px">
																<asp:textbox id="txtCajero" runat="server" Width="140px" CssClass="clsInputDisable" MaxLength="100"
																	Enabled="False"></asp:textbox>&nbsp;
															</div>
															<div style="FLOAT:left;PADDING-TOP:2px">
																<IMG style="CURSOR: hand" onclick="f_OpenCajero()" alt="Buscar cajero" src="../images/botones/btn_Iconolupa.gif">
															</div>
															<div style="CLEAR:both"></div>
														</td>
														<td class="Arial12b">&nbsp;</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Caja :
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtCaja" runat="server" MaxLength="2" CssClass="clsInputEnable" Width="95px"
																ReadOnly="True"></asp:textbox>
														</td>
														<td class="Arial12b" colspan="3">
															<IMG style="CURSOR: hand" onclick="f_OpenCaja()" alt="Buscar caja" src="../images/botones/btn_Iconolupa.gif">
														</td>
													</tr>
													<tr>
														<td class="Arial12b">
															&nbsp;&nbsp;&nbsp;Estado :
														</td>
														<td class="Arial12b" colspan="4">
															<asp:dropdownlist id="cboEstado" runat="server" CssClass="clsSelectEnable" Width="95px">
																<asp:ListItem Selected="True" Value="0">TODOS</asp:ListItem>
																<asp:ListItem Value="N">ABIERTO</asp:ListItem>
																<asp:ListItem Value="S">CERRADO</asp:ListItem>
															</asp:dropdownlist>
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
									<!--INI-936 - JH - Se elimino el input cmdBuscar anterior, y se creó un botón ASP-->
									<asp:button id="btnBuscar" runat="server" Width="100px" Text="Buscar" CssClass="BotonOptm"></asp:button> 
									<input class="BotonOptm" style="WIDTH: 100px" onclick="f_LimpiaControl();" type="button"
										value="Limpiar" name="btnLimpiar">
									<asp:Button id="loadDataHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
									<asp:Button id="loadOficinaHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
									<asp:Button id="loadCajeroHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
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
