<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CorrecRemesaCont.aspx.vb" Inherits="SisCajas.CorrecRemesaCont"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CorrecRemesaCont</title>
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
			<div id="overDiv" style="Z-INDEX: 101; WIDTH: 100px; POSITION: absolute"></div>
			<table cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
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
												height="32">Contenido de Remesa</td>
											<td vAlign="top" width="10" height="32"></td>
										</tr>
									</table>
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
														<TD style="WIDTH: 66px; HEIGHT: 18px" width="66"></TD>
														<TD class="Arial12b" style="WIDTH: 121px; HEIGHT: 18px">&nbsp;&nbsp; Número de 
															Bolsa</TD>
														<TD class="Arial12b" style="WIDTH: 85px; HEIGHT: 18px"><asp:textbox id="txtBolsa" runat="server" CssClass="clsInputDisable" MaxLength="10" ReadOnly="True"></asp:textbox></TD>
														<TD class="Arial12b" style="WIDTH: 60px; HEIGHT: 18px" width="60"></TD>
														<TD style="HEIGHT: 18px" width="300"></TD>
													</TR>
													<TR>
														<TD style="WIDTH: 66px; HEIGHT: 18px" width="66"></TD>
														<TD class="Arial12b" style="WIDTH: 121px; HEIGHT: 18px"></TD>
														<TD class="Arial12b" style="WIDTH: 85px; HEIGHT: 18px"></TD>
														<TD class="Arial12b" style="WIDTH: 60px; HEIGHT: 18px" width="60"></TD>
														<TD style="HEIGHT: 18px" width="300"></TD>
													</TR>
													<TR>
														<TD colSpan="5">
															<div class="frame2" style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 775px; BORDER-BOTTOM: 1px; HEIGHT: 350px; TEXT-ALIGN: center"><asp:datagrid id="dgSobres" runat="server" Width="750px" CellSpacing="1" CellPadding="1" BorderWidth="1px"
																	AutoGenerateColumns="False" BorderColor="White">
																	<AlternatingItemStyle BackColor="#E9EBEE"></AlternatingItemStyle>
																	<ItemStyle CssClass="Arial11B" BackColor="#DDDEE2"></ItemStyle>
																	<HeaderStyle HorizontalAlign="Center" Height="22px" CssClass="Arial12B"></HeaderStyle>
																	<Columns>
																		<asp:BoundColumn DataField="BUZON_BOLSA" HeaderText="N&#176; de sobre">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="90px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="BUZON_FECHA" HeaderText="Fecha">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="BUZON_TIPOVIA" HeaderText="Tipo Via">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="250px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="BUZON_MONTO" HeaderText="Importe" DataFormatString="{0:0.00}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="BUZON_USUARIO" HeaderText="Usuario">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="120px"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																	</Columns>
																</asp:datagrid></div>
														</TD>
													</TR>
												</table>
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
											<TD align="center" width="85"><asp:button id="btnEliminar" runat="server" CssClass="BotonOptm" Width="98px" Text="Eliminar"></asp:button></TD>
											<TD align="center" width="28"></TD>
											<TD align="center" width="85"><asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Width="98px" Text="Cancelar"></asp:button></TD>
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
