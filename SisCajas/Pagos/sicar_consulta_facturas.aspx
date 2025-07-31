<%@ Page Language="vb" AutoEventWireup="false" Codebehind="sicar_consulta_facturas.aspx.vb" Inherits="SisCajas.sicar_consulta_facturas" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>sicar_consulta_facturas</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPricipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="975" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="790">
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10" height="32"></td>
											<td class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="top" align="center" width="98%"
												height="32">Consulta de Facturas</td>
											<td vAlign="top" width="10" height="32"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="800" border="0">
							<tr>
								<td vAlign="top" width="10">&nbsp;</td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="14"></td>
											<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
												width="98%">
												<table cellSpacing="0" cellPadding="0" width="770" border="0">
													<tr>
														<td height="4"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="770" border="0">
													<TR>
														<TD class="Arial12b" style="WIDTH: 20px; HEIGHT: 28px" width="20"></TD>
														<TD class="Arial12b" style="WIDTH: 99px; HEIGHT: 28px" width="99">&nbsp;Nro 
															documento :</TD>
														<TD class="Arial12b" style="HEIGHT: 28px" width="250"><asp:textbox id="txtNroDocumento" runat="server" CssClass="clsInputEnable" MaxLength="15"></asp:textbox></TD>
														<TD class="Arial12b" style="WIDTH: 98px; HEIGHT: 28px" width="98"></TD>
														<TD class="Arial12b" style="WIDTH: 84px; HEIGHT: 28px" width="84">
														</TD>
													</TR>
												</table>
											</td>
										</tr>
									</table>
									<table>
										<tr>
											<td>
												<table cellSpacing="0" cellPadding="4">
													<tr>
														<td>
															<asp:button id="btnBuscar" runat="server" Width="98px" CssClass="BotonOptm" Text="Buscar"></asp:button>
														</td>
														<td>
															<asp:button id="btnLimpiar" runat="server" Width="98px" CssClass="BotonOptm" Text="Limpiar"></asp:button>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="800" border="0">
							<tr>
								<td vAlign="top" width="10">&nbsp;</td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td>
									<table class="Arial12B" cellSpacing="0" cellPadding="0" width="775" align="left" border="0">
										<tr>
											<td style="PADDING-LEFT: 5px">
												<div class="frame2" style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; Z-INDEX: 1; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 775px; BORDER-BOTTOM: 1px; POSITION: relative; HEIGHT: 300px; TEXT-ALIGN: center">
													<asp:datagrid id="dgDetalle" runat="server" Width="700px" BorderColor="White" BorderWidth="1px"
														CellPadding="1" CellSpacing="1" AutoGenerateColumns="False">
														<AlternatingItemStyle HorizontalAlign="Center" BackColor="#E9EBEE" Width="200px"></AlternatingItemStyle>
														<ItemStyle HorizontalAlign="Center" Height="25px" Width="200px" CssClass="Arial11B" BackColor="#DDDEE2"></ItemStyle>
														<HeaderStyle HorizontalAlign="Center" Height="22px" CssClass="Arial12B" BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
														<Columns>
															<asp:BoundColumn DataField="TELEFONO" HeaderText="Teléfono" HeaderStyle-Width="200px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="CONCEPTO" HeaderText="Concepto" HeaderStyle-Width="200px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="F_VENCIMIENTO" HeaderText="Fecha Vencimiento" HeaderStyle-Width="200px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="IMPORTE_PAGO" HeaderText="Importe Pago (S/)" HeaderStyle-Width="150px">
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
						<table>
							<tr>
								<td height="5"></td>
							</tr>
						</table>
					</td>
					<td vAlign="top" width="10">&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
