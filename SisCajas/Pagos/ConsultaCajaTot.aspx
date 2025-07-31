<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConsultaCajaTot.aspx.vb" Inherits="SisCajas.ConsultaCajaTot" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>America Movil</title>
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
			with(frmConsultaCajaTot) {
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
				document.getElementById("txtCaja").value = "";
				document.getElementById("hidCodOficina").value = "";
				document.getElementById("hidCodCajero").value = "";
				document.getElementById("hidCodCaja").value = "";
				var lb1 = document.getElementById("lbCodOficina");
				var lb2 = document.getElementById("lbOficina");
				var lb3 = document.getElementById("lbCodCajero");
				var lb4 = document.getElementById("lbCajero");
				removeAllItems(lb1);
				removeAllItems(lb2);
				removeAllItems(lb3);
				removeAllItems(lb4);
				document.getElementById("lbCodOficina").className = "";
				document.getElementById("lbOficina").className = "";
				document.getElementById("lbCodCajero").className = "";
				document.getElementById("lbCajero").className = "";
			}
		}
		
		function f_OpenOficina(){
			document.getElementById("hidSwOficina").value = "";
			bEnable = document.getElementById("chkTodosOf").checked;
			if (!bEnable){
				Direcc = "ConsultaCajaTot_oficina.aspx"
				window.open(Direcc,"OpenOfT","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
			}
		}
		
		function f_OpenCajero(){
			bEnable = document.getElementById("chkTodos").checked;
			document.getElementById("hidSwCajero").value = "";
			if (!bEnable){
				var of = document.getElementById("hidCodOficina").value;
				Direcc = "ConsultaCajaInd_Cajero.aspx?of=" + of;
				window.open(Direcc,"OpenCaj","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
			}
		}
		
		function f_OpenCaja(){
			var of = document.getElementById("hidCodOficina").value;
			Direcc = "ConsultaCajaTot_caja.aspx?of=" + of;
			window.open(Direcc,"OpenCaT","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_CargarDatosOficina(){
			frmConsultaCajaTot.loadDataHandler.click();			
		}
		
		function f_CargarDatosCajero(){
			frmConsultaCajaTot.loadDataHandler.click();
		}
		
		function f_CargarDatosCaja(){
			frmConsultaCajaTot.loadDataHandler.click();
		}
		
		function ValidaNumero(obj){
			var KeyAscii = window.event.keyCode;
			if (KeyAscii==13) return;	
			if (!(KeyAscii >= 47 && KeyAscii<=57) ){		
				window.event.keyCode = 0;
			}	
		}
		
		function checkEnabled(bCheck){
			if(bCheck){			
				var lb1 = document.getElementById("lbCodCajero");
				var lb2 = document.getElementById("lbCajero");
				removeAllItems(lb1);
				removeAllItems(lb2);
				document.getElementById("lbCajero").disabled = true;
				document.getElementById("lbCodCajero").disabled = true;
				document.getElementById("hidCodCajero").value = "";
				document.getElementById("lbCajero").className = "listBozCssClass";
				document.getElementById("lbCodCajero").className = "listBozCssClass";
			}
			else{
				document.getElementById("lbCodCajero").className = "";
				document.getElementById("lbCajero").className = "";
			}
			document.getElementById("hidSwCajero").value = "1";
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
			}
			else{
				document.getElementById("lbCodOficina").className = "";
				document.getElementById("lbOficina").className = "";
			}
			document.getElementById("hidSwOficina").value = "1";
		}
		
		function f_buscarCajero(){
			if(event.keyCode == 13 ){
				frmConsultaCajaTot.loadCajeroHandler.click();
			}
		}
		
		function removeAllItems(listBox){
			var i;
			for(i=listBox.options.length-1;i>=0;i--){
				listBox.remove(i);
			}
		}
		
		</script>
		<style type="text/css">			
			.listBozCssClass { 
				BORDER-BOTTOM: #bfbee9 1px solid; 
				BORDER-LEFT: #bfbee9 1px solid; 
				BACKGROUND-COLOR: #bfbee9; 
				BORDER-TOP: #bfbee9 1px solid; 
				BORDER-RIGHT: #bfbee9 1px solid 
			}
		</style>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmConsultaCajaTot" method="post" name="frmConsultaCajaTot" runat="server">
			<input id="hidCodCajero" runat="server" type="hidden" name="hidCodCajero"> 
			<input id="hidSwCajero" runat="server" type="hidden" name="hidSwCajero">
			<input id="hidCodOficina" runat="server" type="hidden" name="hidCodOficina">
			<input id="hidSwOficina" runat="server" type="hidden" name="hidSwOficina">
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
											<td class="TituloRConsulta" height="30" align="center">Consulta de Caja - Total</td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td>
												<table border="0" cellSpacing="2" cellPadding="0" align="center">
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Oficina de Venta :</td>
														<td class="Arial12b" style="WIDTH: 100px">
															&nbsp;<asp:ListBox id="lbCodOficina" runat="server" Width="90px" BorderWidth="0px" Rows="3" Font-Name="Arial"
																Font-Size="8"></asp:ListBox>
														</td>
														<td class="Arial12b" colspan="2">
															<div style="FLOAT:left;PADDING-TOP:2px">
																<asp:ListBox id="lbOficina" runat="server" Width="140px" BorderWidth="0px" Rows="3" Font-Name="Arial"
																	Font-Size="8"></asp:ListBox>
															</div>
															<div style="PADDING-LEFT:5px;FLOAT:left;PADDING-TOP:20px">
																<IMG style="CURSOR: hand" onclick="f_OpenOficina()" alt="Buscar oficina" src="../images/botones/btn_Iconolupa.gif">
															</div>
															<div style="CLEAR:both"></div>
														</td>
														<td class="Arial12b">
															<asp:CheckBox ID="chkTodosOf" Runat="server" Checked="False" Text="Todos"></asp:CheckBox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha :</td>
														<td colspan="4" class="Arial12b">
															<input id="txtFechaIni" class="clsInputEnable" maxLength="10" size="12" name="txtFechaIni"
																runat="server" style="WIDTH: 95px; HEIGHT: 17px"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmConsultaCajaTot.txtFechaIni');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
															&nbsp;&nbsp;&nbsp;a &nbsp;&nbsp;&nbsp;&nbsp;<input id="txtFechaFin" class="clsInputEnable" maxLength="10" size="12" name="txtFechaFin"
																runat="server" style="WIDTH: 90px; HEIGHT: 17px"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmConsultaCajaTot.txtFechaFin');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Cajero :
														</td>
														<td class="Arial12b">
															&nbsp;<asp:ListBox id="lbCodCajero" runat="server" Width="90px" BorderWidth="0px" Rows="3" Font-Name="Arial"
																Font-Size="8"></asp:ListBox>
														</td>
														<td class="Arial12b" colspan="2">
															<div style="FLOAT:left;PADDING-TOP:2px">
																<asp:ListBox id="lbCajero" runat="server" Width="140px" BorderWidth="0px" Rows="3" Font-Name="Arial"
																	Font-Size="8"></asp:ListBox>
															</div>
															<div style="PADDING-LEFT:5px;FLOAT:left;PADDING-TOP:20px">
																<IMG style="CURSOR: hand" onclick="f_OpenCajero()" alt="Buscar cajero" src="../images/botones/btn_Iconolupa.gif">
															</div>
															<div style="CLEAR:both"></div>
														</td>
														<td class="Arial12b">
															<asp:CheckBox ID="chkTodos" Runat="server" Checked="False" Text="Todos"></asp:CheckBox>
														</td>
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
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Descripción :
														</td>
														<td class="Arial12b" colspan="4">
															<asp:textbox id="txtDescripcion" runat="server" MaxLength="50" CssClass="clsInputEnable" Width="240px"></asp:textbox>
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
								<td><img height="8" src="" width="1" border="0"></td>
							</tr>
						</table>
						<br>
						<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="360">
							<tr>
								<td align="center">
									<input style="WIDTH: 100px" id="cmdBuscar" class="BotonOptm" value="Buscar" type="button"
										name="cmdBuscar" runat="server">&nbsp;&nbsp; 
									<input class="BotonOptm" style="WIDTH: 100px" onclick="f_LimpiaControl();" type="button"
										value="Limpiar" name="btnLimpiar" id="btnLimpiar" runat="server">
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
