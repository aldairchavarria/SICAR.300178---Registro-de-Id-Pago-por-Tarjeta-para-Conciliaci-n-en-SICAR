<%@ Page Language="vb" AutoEventWireup="false" Codebehind="contabilizarRec.aspx.vb" Inherits="SisCajas.contabilizarRec" %>
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
			with(frmContaRec) {
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
				document.getElementById("hidCodOficina").value = "";
				document.getElementById("txtFechaIni").value = today;
				document.getElementById("txtFechaFin").value = today;
				document.getElementById("txtNroTran01").value = "";
				document.getElementById("txtNroTran02").value = "";
				
				var lb1 = document.getElementById("lbCodOficina");
				var lb2 = document.getElementById("lbOficina");
				removeAllItems(lb1);
				removeAllItems(lb2);
				document.getElementById("lbCodOficina").className = "";
				document.getElementById("lbOficina").className = "";
			}
		}
		
		function f_Procesar(){
			document.getElementById("Botones").style.display ="none";
			document.getElementById("divTitulo").style.display ="block";
		}
		
		function f_ShowButtons(){
			document.getElementById("Botones").style.display ="block";
			document.getElementById("divTitulo").style.display ="none";
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
			if ((key <= 13 || (key >= 48 && key <= 57) || (key >= 96 && key <= 105) || (key == 33) || (key == 34) || (key == 35) || (key == 36)|| (key == 37) || (key == 38) || (key == 39) || (key == 40)|| (key == 45) || (key == 46) || (key == 82))==true)
				event.returnValue = true;
			else
				event.returnValue = false;	
		}
		
		function f_OpenOficina(){
			bEnable = document.getElementById("chkTodosOf").checked;
			if (!bEnable){
				Direcc = "contabilizarRec_oficina.aspx"
				window.open(Direcc,"OpenRRO","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
			}
		}
		
		function f_CargarDatosOficina(){
			frmContaRec.loadDataHandler.click();			
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
				BORDER-RIGHT: #bfbee9 1px solid }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmContaRec" method="post" runat="server">
			<input id="hidCodOficina" runat="server" type="hidden" name="hidCodOficina"> <input id="hidSwOficina" runat="server" type="hidden" name="hidSwOficina">
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
											<td class="TituloRConsulta" height="30" align="center">Contabilización Recaudación</td>
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
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Nro Transacción :</td>
														<td class="Arial12b" style="Padding-left:3px;">
															<asp:textbox id="txtNroTran01" runat="server" MaxLength="15" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
														<td class="Arial12b">
															a
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroTran02" runat="server" MaxLength="15" CssClass="clsInputEnable" Width="130px"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha Transacción :</td>
														<td colspan="3" class="Arial12b" style="Padding-left:3px;">
															<input id="txtFechaIni" class="clsInputEnable" maxLength="10" size="12" name="txtFechaIni"
																runat="server" style="WIDTH: 90px; HEIGHT: 17px"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmContaRec.txtFechaIni');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
															&nbsp;&nbsp;a &nbsp;&nbsp;<input id="txtFechaFin" class="clsInputEnable" maxLength="10" size="12" name="txtFechaFin"
																runat="server" style="WIDTH: 105px; HEIGHT: 17px"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmContaRec.txtFechaFin');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
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
						<div id="Botones">
							<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="400" align="center"
								border="1">
								<tr>
									<td align="center">
										<table cellSpacing="2" cellPadding="0" border="0">
											<tr>
												<td align="center" width="28"></td>
												<td align="center" width="60">
													<asp:button id="cmdProcesar" runat="server" Width="100px" CssClass="BotonOptm" Text="Procesar"></asp:button>&nbsp;&nbsp;
													<asp:Button id="loadDataHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
													<asp:Button id="loadOficinaHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
												</td>
												<td align="center" width="28"></td>
												<td align="center" width="60">
													<input class="BotonOptm" style="WIDTH: 100px" onclick="f_LimpiaControl();" type="button"
														value="Limpiar" name="btnLimpiar" id="btnLimpiar" runat="server">
												</td>
												<td align="center" width="28"></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</div>
						<div id="divTitulo" style="DISPLAY: none">
							<table cellSpacing="2" cellPadding="0" width="199" align="center" border="0" style="WIDTH: 199px; HEIGHT: 36px">
								<tr>
									<td class="TituloRConsulta" align="center">La&nbsp;contabilizaci&oacute;n&nbsp;se&nbsp;est&aacute;&nbsp;procesando</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<br>
			<br>
		</form>
	</body>
</HTML>
