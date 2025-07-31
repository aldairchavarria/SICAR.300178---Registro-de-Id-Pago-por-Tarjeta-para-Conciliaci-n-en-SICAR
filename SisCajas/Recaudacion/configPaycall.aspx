<%@ Page Language="vb" AutoEventWireup="false" Codebehind="configPaycall.aspx.vb" Inherits="SisCajas.configPaycall" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>América Móvil</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
			Direcc = "configPaycall_oficina.aspx"
			window.open(Direcc,"OpenOf","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_CargarDatosOficina(){
			frmPrincipal.loadDataHandler.click();			
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
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
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="811" style="WIDTH: 811px; HEIGHT: 80px">
							<tr>
								<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 5px"
									width="98%">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="TituloRConsulta" align="center" height="30">Configuración Paycall</td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td>
												<table cellSpacing="2" cellPadding="0" border="0" align="center">
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Oficina de Ventas :</td>
														<td class="Arial12b">
															<asp:textbox id="txtOficina" runat="server" Width="130px" CssClass="clsInputEnable" ReadOnly="True"></asp:textbox>
														</td>
														<td class="Arial12b">
															<IMG style="CURSOR: hand" onclick="f_OpenOficina()" alt="Buscar oficina" src="../images/botones/btn_Iconolupa.gif">
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
