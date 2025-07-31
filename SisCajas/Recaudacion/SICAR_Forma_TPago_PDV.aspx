<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SICAR_Forma_TPago_PDV.aspx.vb" Inherits="SisCajas.SICAR_Forma_TPago_PDV" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Mantenimiento de Formas de Pago por PDV</title> 
		<!-- Inicio - INI-1019 - YGP - Nueva pagina para Mantenimiento de Formas de Pago por PDV -->
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<link rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="javascript">
			
			function f_Nuevo()
			{
				var pagina = "SICAR_Forma_TPago_PDV_popup.aspx";
				var parametros = "directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=250";
				msg = window.open(pagina,"SICAR_Forma_TPago_PDV_popup", parametros);
			}
			function refrescarGrid(codigoPDV, codigoMedioPago)
			{
				var comboPDV = document.getElementById("ddlPDV");
				var comboMedioPago = document.getElementById("ddlFP");
				setearValorCombo(comboPDV, codigoPDV);
				setearValorCombo(comboMedioPago, codigoMedioPago);
				document.getElementById('btnBuscar').click();
			}
			function mensajeAlerta(msj)
			{
				alert(msj);
			}
			
			function setearValorCombo(combo, valor) {
				for(var i = 0; i < combo.options.length; i++) {
					if(combo.options[i].value == valor) {
						combo.options[i].selected = true;
						return;
					}
				}
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPricipal" method="post" runat="server">
			<table>
				<tr>
					<td>
						<table style="Z-INDEX: 0; WIDTH: 809px; HEIGHT: 45px" class="tabla_borde" border="1">
							<tr>
								<td class="TituloRConsulta">
									<div style="TEXT-ALIGN: center">Mantenimiento de Formas de Pago por PDV</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td></td>
				<tr>
					<td>
						<table style="WIDTH: 720px; HEIGHT: 146px" class="tabla_borde">
							<tr>
								<td>
									<table style="WIDTH: 797px; HEIGHT: 92px" border="0">
										<tr>
											<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 5px">
												<table style="WIDTH: 784px; HEIGHT: 59px" border="0">
													<tr>
														<td style="WIDTH: 150px; HEIGHT: 24px" class="Arial12b"></td>
														<td style="WIDTH: 116px; HEIGHT: 24px" class="Arial12b">&nbsp;PDV:</td>
														<td style="WIDTH: 282px; HEIGHT: 24px" class="Arial12b">
															<asp:dropdownlist style="Z-INDEX: 0" id="ddlPDV" CssClass="clsSelectEnable" Width="248px" Runat="server"
																Height="24px" AutoPostBack="True"></asp:dropdownlist>
														</td>
														<td style="WIDTH: 94px; HEIGHT: 24px" class="Arial12b"></td>
													</tr>
													<tr>
														<td style="WIDTH: 150px; HEIGHT: 28px" class="Arial12b"></td>
														<td style="WIDTH: 116px; HEIGHT: 28px" class="Arial12b">&nbsp;Forma de Pago&nbsp;:</td>
														<td style="WIDTH: 282px; HEIGHT: 28px" class="Arial12b">
															<asp:dropdownlist id="ddlFP" CssClass="clsSelectEnable" Width="248px" Runat="server" Height="24px"></asp:dropdownlist>
														</td>
														<td style="WIDTH: 94px; HEIGHT: 28px" class="Arial12b"></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<table style="Z-INDEX: 0; WIDTH: 799px">
										<tbody>
											<tr>
												<td style="WIDTH: 280px"></td>
												<td style="WIDTH: 105px">
													<asp:button id="btnBuscar" runat="server" CssClass="BotonOptm" Width="112px" Text="Buscar"></asp:button>
												</td>
												<td style="WIDTH: 98px">
													<asp:button id="btnLimpiar" runat="server" CssClass="BotonOptm" Width="112px" Text="Limpiar"></asp:button>
												</td>
												<td style="WIDTH: 264px"></td>
											</tr>
										</tbody>
									</table>
								</td>
							</tr>
						</table>
						<table style="WIDTH: 100%; HEIGHT: 25px">
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
						<table style="WIDTH: 100%" class="tabla_borde">
							<tr>
								<td>
									<table>
										<tr>
											<td style="PADDING-LEFT: 5px">
												<div style="Z-INDEX: 0; BORDER-BOTTOM: 1px; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 784px; HEIGHT: 350px; BORDER-TOP: 1px; BORDER-RIGHT: 1px"
													class="frame2">
													<asp:datagrid id="gridDetalle" runat="server" Width="750px" AutoGenerateColumns="False" CellSpacing="1"
														CellPadding="1" BorderWidth="1px" BorderColor="White" PageSize="10" PagerStyle-Mode="NumericPages"
														AllowPaging="True" OnUpdateCommand="ActualizarItem" OnEditCommand="EditarItem" OnCancelCommand="CancelarItem">
														<AlternatingItemStyle HorizontalAlign="Center" BackColor="#E9EBEE"></AlternatingItemStyle>
														<ItemStyle HorizontalAlign="Center" Height="25px" CssClass="Arial11B" BackColor="#DDDEE2"></ItemStyle>
														<HeaderStyle HorizontalAlign="Center" Height="22px" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
														<Columns>
															<asp:TemplateColumn Visible="False">
																<ItemTemplate>
																	<asp:Label ID="id" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.id") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn Visible="False">
																<ItemTemplate>
																	<asp:Label ID="estadoMedioPago" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.estadoMedioPago") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="PDV">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container,"DataItem.descOficinaVenta") %>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Forma de Pago">
																<ItemTemplate>
																	<asp:Label ID="descMedioPago" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.descMedioPago") %>'></asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Estado">
																<HeaderStyle Width="9%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:CheckBox id="chkestado" runat="server" Checked='<%# DataBinder.Eval(Container,"DataItem.estado") %>' Enabled="False">
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
														</Columns>
														<PagerStyle Mode="NumericPages" VerticalAlign="Middle" CssClass="Arial12BldB" BackColor="#DDDEE2"></PagerStyle>
													</asp:datagrid>
												</div>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table>
							<tr>
								<td>
									<div>
										<asp:datagrid style="Z-INDEX: 0" id="gridExportar" runat="server" AutoGenerateColumns="False"
											Visible="False">
											<Columns>
												<asp:TemplateColumn HeaderText="Codigo PDV" HeaderStyle-Font-Bold="True">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.codigoPDV") %>													
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="PDV" HeaderStyle-Font-Bold="True">
													<ItemTemplate>
														<%# DataBinder.Eval(Container,"DataItem.descOficinaVenta") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="CCINS" HeaderStyle-Font-Bold="True">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.ccins") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Forma de Pago" HeaderStyle-Font-Bold="True">
													<ItemTemplate>
														<%# DataBinder.Eval(Container,"DataItem.descMedioPago") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Estado" HeaderStyle-Font-Bold="True">
													<ItemTemplate>
														<%# DataBinder.Eval(Container,"DataItem.estado") %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid>
									</div>
								</td>
							</tr>
						</table>
						<table style="WIDTH: 812px">
							<tbody>
								<tr style="HEIGHT: 35px">
									<td></td>
									<td style="WIDTH: 228px" class="tabla_borde">
										<asp:button id="btnNuevo" runat="server" CssClass="BotonOptm" Width="112px" Text="Nuevo"></asp:button>
										<asp:button id="btnExportar" runat="server" CssClass="BotonOptm" Width="112px" Text="Exportar"></asp:button>
										<!--<asp:button id="loadDataHandler" runat="server" Text="Button" style="DISPLAY: none"></asp:button>-->
									</td>
									<td></td>
								</tr>
							</tbody>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<!-- Fin - INI-1019 - YGP -->
	</body>
</HTML>
