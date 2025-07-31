<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListarCaja_Nuevo.aspx.vb" Inherits="SisCajas.ListarCaja_Nuevo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>America Movil</title>
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
		
		function f_OpenOficina(){
			Direcc = "ConsultaCajaInd_oficina.aspx"
			window.open(Direcc,"OpenOfLCJ","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=325,width=420,height=450");
		}
		
		function f_CargarDatosOficina(){
			frmCajaOficinaNuevo.loadDataHandler.click();			
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmCajaOficinaNuevo" method="post" runat="server">
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
												<asp:Label ID="lblTitNuevoCaja" Runat="server">Nueva Caja</asp:Label>
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
											<td><asp:Label id="lblNroCaja" runat="server" CssClass="Arial12b">Nº de Caja</asp:Label></td>
											<td><input name="txtNroCaja" style="WIDTH:50px" class="clsInputEnable" id="txtNroCaja" size="25"
													maxlength="2" runat="server"></td>
										</tr>
										<tr>
											<td><asp:Label id="lblDescripcion" runat="server" CssClass="Arial12b">Descripción</asp:Label></td>
											<td><input name="txtDescripcion" style="WIDTH:200px" class="clsInputEnable" id="txtDescripcion"
													size="25" maxlength="30" runat="server"></td>
										</tr>
										<tr>
											<td><asp:Label id="lblEstado" runat="server" CssClass="Arial12b">Estado</asp:Label></td>
											<td>
												<asp:dropdownlist id="ddlEstado" runat="server" CssClass="clsSelectEnable">
													<asp:ListItem Value="">INACTIVO</asp:ListItem>
													<asp:ListItem Value="X" Selected="True">ACTIVO</asp:ListItem>
												</asp:dropdownlist>
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
