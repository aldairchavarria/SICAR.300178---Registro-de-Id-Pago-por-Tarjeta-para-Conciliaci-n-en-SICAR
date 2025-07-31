<%@ Page Language="vb" AutoEventWireup="false" Codebehind="contabilizarRem.aspx.vb" Inherits="SisCajas.contabilizarRem" %>
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
			with(frmContaRem) {
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
				document.getElementById("txtFechaDoc").value = "";
				document.getElementById("txtFecConta").value = today;
				document.getElementById("txtOficinaVenta").value = "";
				
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
		
		function f_OpenOficina(){
			bEnable = document.getElementById("chkTodosOf").checked;
			if (!bEnable){
				Direcc = "contabilizarRec_oficina.aspx"
				window.open(Direcc,"OpenOfREM","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
			}
		}
		
		function f_CargarDatosOficina(){
			frmContaRem.loadDataHandler.click();			
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
		<form id="frmContaRem" method="post" runat="server">
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
											<td class="TituloRConsulta" height="30" align="center">Contabilizar Remesas</td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td>
												<table border="0" cellSpacing="2" cellPadding="0" align="center" style="WIDTH: 470px; HEIGHT: 83px">
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
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha de documento :</td>
														<td colspan="4" class="Arial12b" style="padding-left:2px;">
															<input id="txtFechaDoc" class="clsInputEnable" maxLength="10" size="12" name="txtFechaDoc"
																runat="server" style="WIDTH: 90px; HEIGHT: 17px"> &nbsp;<A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmContaRem.txtFechaDoc');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>&nbsp;&nbsp;
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha contabilización :</td>
														<td colspan="4" class="Arial12b" style="padding-left:2px;">
															<input id="txtFecConta" class="clsInputEnable" maxLength="10" size="12" name="txtFecConta"
																runat="server" style="WIDTH: 90px; HEIGHT: 17px"> &nbsp;<A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmContaRem.txtFecConta');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>&nbsp;&nbsp;
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
													<asp:button id="cmdProcesar" runat="server" Width="100px" CssClass="BotonOptm" Text="Procesar"></asp:button>
													<asp:Button id="loadDataHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
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
									<td class="TituloRConsulta" align="center">La&nbsp;contabilización&nbsp;se&nbsp;está&nbsp;procesando</td>
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
