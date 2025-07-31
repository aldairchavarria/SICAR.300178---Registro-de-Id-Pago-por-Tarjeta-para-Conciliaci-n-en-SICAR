<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorRecRecibo.aspx.vb" Inherits="SisCajas.visorRecRecibo" %>
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
			with(frmVisorRecRecibo) {
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
				cboMoneda.selectedIndex = nIndice;
				cboTipDocRecaudacion.selectedIndex = nIndice;
				
				//document.getElementById("txtFechaIni").value = today;
				//document.getElementById("txtFechaFin").value = today;
				
				document.getElementById("txtNroTran01").value = "";
				document.getElementById("txtNroTran02").value = "";
				document.getElementById("txtMtoTotPagado01").value = "";
				document.getElementById("txtMtoTotPagado02").value = "";	
				document.getElementById("txtCntRegistros").value = "250";
			}
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmVisorRecRecibo" method="post" runat="server">
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
												Recibo</td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td>
												<table border="0" cellSpacing="2" cellPadding="0" align="center">
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Nro Transacción :</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroTran01" runat="server" MaxLength="15" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
														<td class="Arial12b" width="10%" align="center">
															a
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroTran02" runat="server" MaxLength="15" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Tipo Doc Recaud :</td>
														<td class="Arial12b" colspan="3">
															<asp:dropdownlist id="cboTipDocRecaudacion" runat="server" CssClass="clsSelectEnable" Width="256px"
																AutoPostBack="True"></asp:dropdownlist>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Nro Doc Recaud :</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroDocRec1" runat="server" MaxLength="15" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
														<td class="Arial12b" width="10%" align="center">
															a
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroDocRec2" runat="server" MaxLength="15" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Moneda :</td>
														<td class="Arial12b" colspan="3">
															<asp:dropdownlist id="cboMoneda" runat="server" CssClass="clsSelectEnable" Width="256px"></asp:dropdownlist>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Importe Recibo :</td>
														<td class="Arial12b">
															<asp:textbox id="txtImporteRec1" runat="server" MaxLength="10" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
														<td class="Arial12b" width="10%" align="center">
															a
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtImporteRec2" runat="server" MaxLength="10" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Importe Pagado :</td>
														<td class="Arial12b">
															<asp:textbox id="txtMtoTotPagado01" runat="server" MaxLength="10" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
														<td class="Arial12b" width="10%" align="center">
															a
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtMtoTotPagado02" runat="server" MaxLength="10" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Nro. Cobranza :</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroCobranza01" runat="server" MaxLength="10" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
														<td class="Arial12b" width="10%" align="center">
															a
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroCobranza02" runat="server" MaxLength="10" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Nro. Acreedor :</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroAcreedor1" runat="server" MaxLength="10" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
														<td class="Arial12b" width="10%" align="center">
															a
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroAcreedor2" runat="server" MaxLength="10" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Fecha Pago :</td>
														<td class="Arial12b">
															<input id="txtFechaIni" class="clsInputEnable" maxLength="10" size="12" name="txtFechaIni"
																runat="server" style="WIDTH: 90px; HEIGHT: 17px"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmVisorRecRecibo.txtFechaIni');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
														<td class="Arial12b" width="10%" align="center">
															a
														</td>
														<td class="Arial12b">
															<input id="txtFechaFin" class="clsInputEnable" maxLength="10" size="12" name="txtFechaFin"
																runat="server" style="WIDTH: 90px; HEIGHT: 17px"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmVisorRecRecibo.txtFechaFin');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Nro. Doc. Deudor :</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroDoc01" runat="server" MaxLength="15" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
														<td class="Arial12b" width="10%" align="center">
															a
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtNroDoc02" runat="server" MaxLength="15" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b" style="WIDTH: 130px">&nbsp;&nbsp;&nbsp;Trace Pago :</td>
														<td class="Arial12b">
															<asp:textbox id="txtTracePago1" runat="server" MaxLength="15" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
														<td class="Arial12b" width="10%" align="center">
															a
														</td>
														<td class="Arial12b">
															<asp:textbox id="txtTracePago2" runat="server" MaxLength="15" CssClass="clsInputEnable" Width="90px"></asp:textbox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Número de Registros :</td>
														<td class="Arial12b" colspan="3" style="WIDTH: 115px">
															<asp:textbox id="txtCntRegistros" runat="server" MaxLength="6" CssClass="clsInputEnable" Width="90px">250</asp:textbox>
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
									<asp:button id="cmdBuscar" runat="server" Width="100px" CssClass="BotonOptm" Text="Buscar"></asp:button>&nbsp;&nbsp;
									<input class="BotonOptm" style="WIDTH: 100px" onclick="f_LimpiaControl();" type="button"
										value="Limpiar" name="btnLimpiar">
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
