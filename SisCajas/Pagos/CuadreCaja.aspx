<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CuadreCaja.aspx.vb" Inherits="SisCajas.CuadreCaja" enableViewState="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CuadreCaja</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="850" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="820">
						<table height="14" cellSpacing="0" cellPadding="0" width="820" border="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="790" align="center" border="0" name="Contenedor">
							<tr>
								<td align="center">
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="790" align="center"
										border="1">
										<tr>
											<td align="center">
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td class="TituloRConsulta" align="center" width="98%" height="32">Cuadre de Caja - 
															Cuadre Diario
															<% if ucase(request.item("tipocuadre")) = "I" then %>
															Individual
															<% end if %>
														</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="790" align="center" border="0">
													<tr>
														<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
															width="98%">
															<table cellSpacing="0" cellPadding="0" width="770" border="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td style="PADDING-LEFT: 5px" height="18">
																		<div class="frame2" style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; Z-INDEX: 1; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 770px; BORDER-BOTTOM: 1px; POSITION: relative; HEIGHT: 340px; TEXT-ALIGN: center"><asp:datagrid id="dgCuadre" runat="server" BorderWidth="0px" CellPadding="1" CellSpacing="1" CssClass="tabla_interna_borde1"
																				Width="100%" AutoGenerateColumns="False">
																				<AlternatingItemStyle Height="25px" BackColor="#DEE9FA"></AlternatingItemStyle>
																				<ItemStyle Height="25px" BorderWidth="0px" CssClass="Arial12B" BackColor="#D0D8F0"></ItemStyle>
																				<HeaderStyle HorizontalAlign="Center" Height="21px" CssClass="Arial12B"></HeaderStyle>
																				<Columns>
																					<asp:BoundColumn DataField="CONTADOR" HeaderText="Orden">
																						<HeaderStyle Width="5%" CssClass="tabla_interna_borde2"></HeaderStyle>
																						<ItemStyle HorizontalAlign="Center"></ItemStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="DESC_CONCEPTO" HeaderText="Descripci&#243;n">
																						<HeaderStyle Width="20%" CssClass="tabla_interna_borde2"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="MONTO" HeaderText="Monto" DataFormatString="{0:0.00}">
																						<HeaderStyle Width="5%" CssClass="tabla_interna_borde2"></HeaderStyle>
																						<ItemStyle HorizontalAlign="Center"></ItemStyle>
																					</asp:BoundColumn>
																				</Columns>
																			</asp:datagrid></div>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<br>
									<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="360" align="center"
										border="1">
										<tr>
											<td>
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td align="center">&nbsp; <input class="BotonOptm" id="btnExcel" style="WIDTH: 100px" onclick="f_Exportar()" type="button"
																value="Exportar Excel" name="btnExcel">&nbsp;&nbsp;&nbsp;
															<asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Width="100px" Text="Regresar"></asp:button></td>
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
			<script language="JavaScript" type="text/javascript">
var esNavegador, esIExplorer;

esNavegador = (navigator.appName == "Netscape") ? true : false;
esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

if (esIExplorer) {
}

			</script>
		</form>
		<script language="JavaScript">
	function f_Exportar()
	{
		document.frmTmp.action = "ExcelCuadreCaja.aspx"
		document.frmTmp.submit();
	}
	
		</script>
		<form name="frmTmp" action="" method="post" target="_blank">
		</form>
	</body>
</HTML>
