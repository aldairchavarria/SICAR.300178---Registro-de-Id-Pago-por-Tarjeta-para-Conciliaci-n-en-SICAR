<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListarImpresora.aspx.vb" Inherits="SisCajas.ListarImpresora" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>América Móvil</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="JavaScript">
	
	function f_LimpiaControl() {
		with(frmPrincipal) {
			reset();
			document.getElementById("txtOficina").value = "";
			document.getElementById("hidCodOficina").value = "";
		}
	}
	
	function f_OpenOficina(){
		Direcc = "ConsultaCajaTot_oficina.aspx"
		window.open(Direcc,"OpenOfLI","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
	}
	
	function f_CargarDatosOficina(){
		frmPrincipal.loadDataHandler.click();			
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
		<form id="frmPrincipal" method="post" runat="server">
			<input id="hidCodOficina" runat="server" type="hidden" name="hidCodOficina">
			<table cellSpacing="0" cellPadding="0" width="820" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" align="center" width="820">
						<table height="14" cellSpacing="0" cellPadding="0" width="810" border="0">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="810">
							<tr>
								<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 5px"
									width="98%">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="TituloRConsulta" align="center" height="30">Consulta de Ticketera - 
												Oficina</td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td>
												<table cellSpacing="2" cellPadding="0" border="0" align="center">
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Oficina :</td>
														<td class="Arial12b" style="WIDTH: 100px">
															&nbsp;<asp:ListBox id="lbCodOficina" runat="server" Width="90px" BorderWidth="0px" Rows="3" Font-Name="Arial"
																Font-Size="8"></asp:ListBox>
														</td>
														<td class="Arial12b" colspan="2">
															<div style="FLOAT:left">
																<asp:ListBox id="lbOficina" runat="server" Width="140px" BorderWidth="0px" Rows="3" Font-Name="Arial"
																	Font-Size="8"></asp:ListBox>
															</div>
															<div style="PADDING-LEFT:5px;FLOAT:left;PADDING-TOP:20px">
																<IMG style="CURSOR: hand" onclick="f_OpenOficina()" alt="Buscar oficina" src="../images/botones/btn_Iconolupa.gif">
															</div>
															<div style="CLEAR:both"></div>
														</td>
														<td class="Arial12b">
															<asp:CheckBox ID="chkTodosOf" Runat="server" Checked="False" Text="Todos" onclick="checkEnabledOfi(this.checked)"></asp:CheckBox>
														</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Serie :
														</td>
														<td colspan="3">
															<asp:textbox id="txtSerie" runat="server" Width="240px" CssClass="clsInputEnable" MaxLength="25"></asp:textbox>&nbsp;&nbsp;
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
									<asp:Button id="loadDataHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
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
