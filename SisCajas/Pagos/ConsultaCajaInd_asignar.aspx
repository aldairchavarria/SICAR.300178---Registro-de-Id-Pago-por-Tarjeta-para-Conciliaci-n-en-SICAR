<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConsultaCajaInd_asignar.aspx.vb" Inherits="SisCajas.ConsultaCajaInd_asignar" %>
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
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="JavaScript">
		
			function f_Cancelar(){
				window.close();
			}
			
			function f_OpenOficina(){
				Direcc = "ConsultaCajaInd_oficina.aspx"
				window.open(Direcc,"OpenOfA","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=325,width=420,height=450");
			}
			
			function f_OpenCajero(){
				var of = document.getElementById("hidCodOficina").value;
				Direcc = "ConsultaCajaTot_Cajero.aspx?of=" + of;
				window.open(Direcc,"OpenCajA","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=325,width=420,height=450");
			}
			
			function f_CargarDatosOficina(){
				frmAsignarCaj.loadDataHandler.click();			
			}
			
			function f_CargarDatosCajero(){
				frmAsignarCaj.loadDataHandler.click();
			}
			
			function f_CargarDatosCaja(){
				frmAsignarCaj.loadDataHandler.click();
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmAsignarCaj" method="post" runat="server">
			<input id="hidCodCajero" runat="server" type="hidden" name="hidCodCajero"> 
			<input id="hidCodOficina" runat="server" type="hidden" name="hidCodOficina">
			<table cellSpacing="0" cellPadding="0" width="450" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td style="WIDTH: 832px" vAlign="top" width="480">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" name="Contenedor">
							<tr>
								<td align="center" height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10" height="32"></td>
											<td class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="top" align="center" width="98%"
												height="32">
												<asp:Label ID="lblTitNuevoAsignacion" Runat="server">Asignación de Cajeros</asp:Label>
											</td>
											<td vAlign="top" width="10" height="32"></td>
										</tr>
									</table>
									<table width="350" border="0" align="center" cellpadding="4" cellspacing="0" bordercolor="#336699">
										<tr>
											<td><asp:Label id="lblOficina" runat="server" CssClass="Arial12b">Oficina :</asp:Label></td>
											<td style="WIDTH: 190px;">
												<asp:textbox id="txtOficina" runat="server" Width="144px" CssClass="clsInputDisable" MaxLength="100"
													Enabled="False"></asp:textbox>&nbsp;
											</td>
											<td style="WIDTH: 55px">
												<IMG style="CURSOR: hand" onclick="f_OpenOficina()" alt="Buscar oficina" src="../images/botones/btn_Iconolupa.gif">
											</td>
										</tr>
										<tr>
											<td style="HEIGHT: 28px"><asp:Label id="lblCajero" runat="server" CssClass="Arial12b">Cajero :</asp:Label></td>
											<td>
												<asp:textbox id="txtCajero" runat="server" Width="210px" CssClass="clsInputDisable" MaxLength="100"
													Enabled="False"></asp:textbox>&nbsp;
											</td>
											<td>
												<IMG style="CURSOR: hand" onclick="f_OpenCajero()" alt="Buscar cajero" src="../images/botones/btn_Iconolupa.gif">
											</td>
										</tr>
										<tr>
											<td><asp:Label id="lblCaja" runat="server" CssClass="Arial12b">Caja :</asp:Label></td>
											<td colspan="2">
												<asp:dropdownlist id="cboCaja" runat="server" CssClass="clsSelectEnable" Width="144px"></asp:dropdownlist>
											</td>
										</tr>
										<tr>
											<td><asp:Label id="lblFecha" runat="server" CssClass="Arial12b">Fecha :</asp:Label></td>
											<td colspan="2">
												<input id="txtFecha" class="clsInputEnable" maxLength="10" size="12" name="txtFecha" runat="server"
													style="WIDTH: 125px; HEIGHT: 17px" readonly> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
													href="javascript:show_calendar('frmAsignarCaj.txtFecha');"><IMG border="0" src="../images/botones/btn_Calendario.gif"></A>
											</td>
										</tr>
									</table>
									<br>
									<br>
									<table width="300" border="1" align="center" cellpadding="4" cellspacing="0" bordercolor="#336699">
										<tr>
											<td>
												<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="Arial10B">
													<tr>
														<td align="center">
															<input name="btnGrabar" type="button" class="BotonOptm" style="WIDTH:100px" value="Grabar"
																id="cmdGrabar" runat="server">&nbsp;&nbsp;</td>
														<td align="center">
															<input name="btnCancelar" type="button" class="BotonOptm" style="WIDTH:100px" onClick="javascript:f_Cancelar();"
																value="Cancelar" id="cmdCancelar">&nbsp;&nbsp;
															<asp:Button id="loadDataHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<br>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
