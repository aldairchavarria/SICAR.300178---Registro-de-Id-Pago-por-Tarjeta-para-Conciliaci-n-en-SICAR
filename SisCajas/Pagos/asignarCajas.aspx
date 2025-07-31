<%@ Page Language="vb" AutoEventWireup="false" Codebehind="asignarCajas.aspx.vb" Inherits="SisCajas.asignarCajas"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EnvioBuzon</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript" src="../librerias/ubigeo.js"></script>
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="600" border="0">
				<tr>
					<td vAlign="top" width="100">&nbsp;</td>
					<td vAlign="top" width="810">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" name="Contenedor">
							<tr>
								<td align="center" height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1" name="Contenedor">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10" height="32"></td>
											<td class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="top" align="center" width="98%"
												height="32">Asignación de Cajeros</td>
											<td vAlign="top" width="10" height="32"></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="14"></td>
											<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 5px"
												width="98%"><br>
												<div class="frame2" style="BORDER-BOTTOM: 1px; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 580px; HEIGHT: 130px; BORDER-TOP: 1px; BORDER-RIGHT: 1px">
													<asp:datagrid id="dgCajas" runat="server" CssClass="Arial11B" Width="100%" CellSpacing="1" AutoGenerateColumns="False"
														BorderColor="White">
														<AlternatingItemStyle BackColor="#DDDEE2"></AlternatingItemStyle>
														<ItemStyle Height="21px" CssClass="Arial11B" BackColor="#E9EBEE"></ItemStyle>
														<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
														<Columns>
															<asp:TemplateColumn HeaderText="Seleccionar">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="1%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<input type=radio name="rbCaja" id="rbcaja" value='<%# DataBinder.Eval(Container,"DataItem.CASNR")%>'>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn DataField="CASNR" HeaderText="Numero">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="BEZEI" HeaderText="Descripcion">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
														</Columns>
													</asp:datagrid></div>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td align="center">
									<table border="0" align="center" width="100%">
										<tr>
											<td class="TituloRConsulta" align="center">Seleccion del Cajero
											</td>
										</tr>
									</table>
									<table border="0" align="center" width="100%">
										<tr>
											<td class="Arial12B">Cajero:
											</td>
											<td>
												<asp:DropDownList id="cboCajeros" runat="server" CssClass="clsSelectEnable" Width="317px"></asp:DropDownList>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="400" align="center"
							border="1">
							<TR>
								<TD align="center">
									<TABLE cellSpacing="2" cellPadding="0" border="0">
										<TR>
											<TD align="center" width="28"></TD>
											<TD align="center" width="85"><asp:button id="btnGrabar" runat="server" CssClass="BotonOptm" Width="98px" Text="Grabar"></asp:button></TD>
											<TD align="center" width="28"></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			</TD></TR></TABLE></form>
	</body>
</HTML>
