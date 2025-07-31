<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SICAR_Forma_TPago.aspx.vb" Inherits="SisCajas.SICAR_Forma_TPago"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Formas de Pago</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" name="frmPrincipal" runat="server">
			<table border="0" cellSpacing="0" cellPadding="0" width="600">
				<tr>
					<td vAlign="top" width="100">&nbsp;</td>
					<td vAlign="top" width="810">
						<table border="0" cellSpacing="0" cellPadding="0" width="100%" name="Contenedor">
							<tr>
								<td height="10" align="center"></td>
							</tr>
						</table>
						<table style="WIDTH: 598px; HEIGHT: 432px" border="1" cellSpacing="0" borderColor="#336699"
							cellPadding="0" width="598" align="center" name="Contenedor">
							<tr>
								<td align="center">
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td height="32" vAlign="top" width="10"></td>
											<td style="PADDING-TOP: 4px" class="TituloRConsulta" height="32" vAlign="top" width="98%"
												align="center">Formas de Pago</td>
											<td height="32" vAlign="top" width="10"></td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td vAlign="top" width="14"></td>
											<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 5px"
												width="98%"><br>
                                                <!-- Inicio - INI-936 - YGP - Modificado datagrid para implementar events OnEdit, OnUpdate y OnCancel -->
												<div style="Z-INDEX: 0; BORDER-BOTTOM: 1px; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 580px; HEIGHT: 334px; BORDER-TOP: 1px; BORDER-RIGHT: 1px"
													class="frame2">
                                                    <asp:datagrid id="dgFormaTPago" runat="server" HorizontalAlign="Center" BorderColor="White" AutoGenerateColumns="False"
														CellSpacing="1" Width="100%" CssClass="Arial11B" OnUpdateCommand="ActualizarItem" OnEditCommand="EditarItem" OnCancelCommand="CancelarItem">
														<AlternatingItemStyle BackColor="#DDDEE2"></AlternatingItemStyle>
														<ItemStyle Height="21px" CssClass="Arial11B" BackColor="#E9EBEE"></ItemStyle>
														<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
														<Columns>
															<asp:TemplateColumn Visible="False">
																<ItemTemplate>
																	<asp:Label ID="idTipoPago" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.idTipoPago") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Descripci&#243;n">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container,"DataItem.descripcionTipoPago") %>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox ID="txtDescMedioPago" runat="server" MaxLength="20" Width="150px" CssClass="clsInputEnable" Text='<%# DataBinder.Eval(Container,"DataItem.descripcionTipoPago") %>'>
																	</asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Estado">
																<HeaderStyle Width="9%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:CheckBox id="chkEstadoFPago" runat="server" Checked='<%# DataBinder.Eval(Container,"DataItem.estadoTipoPago") %>' Enabled="False">
																	</asp:CheckBox>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Opciones">
																<HeaderStyle Width="80px"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton ID="imgModificar" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="../images/botones/edit.gif"
																		ToolTip="Modificar" />
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:ImageButton ID="imgGrabar" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="../images/botones/save.png"
																		ToolTip="Grabar" />
																	<asp:ImageButton ID="imgCancelar" runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="../images/botones/cancel.png"
																		ToolTip="Cancelar" />
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn Visible="False" DataField="estadoTipoPago" HeaderText="Estado"></asp:BoundColumn>
														</Columns>
													</asp:datagrid>
												</div>
												<!-- Fin - INI-936 - YGP -->
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="100%" align="center">
							<TR>
								<TD align="center">
									<TABLE border="0" cellSpacing="2" cellPadding="0">
										<TR>
											<TD width="28" align="center"></TD>
											<!-- Inicio - INI-936 - YGP - Reemplazo btnGrabar por btnExportar -->
											<TD width="28" align="center"><asp:button id="btnExportar" runat="server" CssClass="BotonOptm" Width="98px" Text="Exportar"></asp:button></TD>
											<!-- Fin - INI-936 - YGP -->
											<TD width="28" align="center"></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
				</TD></TR></table>
		</form>
	</body>
</HTML>
