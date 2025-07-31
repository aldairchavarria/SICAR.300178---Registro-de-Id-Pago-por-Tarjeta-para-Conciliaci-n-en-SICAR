<%@ Page Language="vb" AutoEventWireup="false" Codebehind="configPaycall_nuevo.aspx.vb" Inherits="SisCajas.configPaycall_nuevo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>América Móvil</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<link href="../estilos/est_General.css" rel="styleSheet" type="text/css">
		<script language="javascript">
		function f_Cancelar(){
			window.close();
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
			if ((key <= 13 || (key >= 48 && key <= 57) || (key >= 96 && key <= 105) || (key == 33) || (key == 34) || (key == 35) || (key == 36)|| (key == 37) || (key == 38) || (key == 39) || (key == 40)|| (key == 45) || (key == 46))==true)
				event.returnValue = true;
			else
				event.returnValue = false;	
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="450" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td style="WIDTH: 832px" vAlign="top" width="480">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" name="Contenedor">
							<tr>
								<td align="center" height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="444" align="center"
							border="1" style="WIDTH: 444px; HEIGHT: 240px">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10" height="32"></td>
											<td class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="top" align="center" width="98%"
												height="32">
												<asp:Label ID="lblTitNuevo" Runat="server">Detalle Configuraci&oacute;n Paycall</asp:Label>
											</td>
											<td vAlign="top" width="10" height="32"></td>
										</tr>
									</table>
									<table width="350" border="0" align="center" cellpadding="4" cellspacing="0" bordercolor="#336699">
										<tr>
											<td><asp:Label id="lblOficina" runat="server" CssClass="Arial12b">Oficina</asp:Label></td>
											<td>
												<asp:dropdownlist id="cboOficina" runat="server" CssClass="clsSelectEnable" Enabled="False"></asp:dropdownlist>
											</td>
										</tr>
										<tr>
											<td><asp:Label id="lblPaycall" runat="server" CssClass="Arial12b">Paycall</asp:Label></td>
											<td><input name="txtPaycall" style="WIDTH:144px; HEIGHT:17px" class="clsInputEnable" id="txtPaycall"
													size="18" maxlength="18" runat="server"></td>
										</tr>
										<tr>
											<td><asp:Label id="lblTipoRemesa" runat="server" CssClass="Arial12b">Tipo Remesa</asp:Label></td>
											<td>
												<asp:dropdownlist id="cboTipoRemesa" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist>
											</td>
										</tr>
										<tr>
											<td><asp:Label id="lblCtaMayor" runat="server" CssClass="Arial12b">Cta Mayor</asp:Label></td>
											<td>
												<input name="txtCtaMayor" style="WIDTH:144px; HEIGHT:17px" class="clsInputEnable" id="txtCtaMayor"
													size="18" maxlength="10" runat="server">
											</td>
										</tr>
									</table>
									<br>
									<table width="298" border="1" align="center" cellpadding="4" cellspacing="0" bordercolor="#336699"
										style="WIDTH: 298px; HEIGHT: 50px">
										<tr>
											<td>
												<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="Arial10B">
													<tr>
														<td align="center">
															<input name="btnGrabar" type="button" class="BotonOptm" style="WIDTH:100px" value="Grabar"
																id="cmdGrabar" runat="server">&nbsp;&nbsp;</td>
														<td align="center">
															<input name="btnCancelar" type="button" class="BotonOptm" style="WIDTH:100px" onClick="javascript:f_Cancelar();"
																value="Cancelar" id="cmdCancelar">&nbsp;&nbsp;</td>
													</tr>
												</table>
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
	</body>
</HTML>
