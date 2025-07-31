<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorRecDeuda_detalle.aspx.vb" Inherits="SisCajas.visorRecDeuda_detalle"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>América Móvil</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script type="text/javascript" src="../librerias/Lib_FuncGenerales.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="JavaScript">
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmVisDeuDet" method="post" runat="server">
		<input id="hdnOrdenacion" runat="server" type="hidden" name="hdnOrdenacion">
			<table border="0" cellSpacing="0" cellPadding="0">
				<TBODY>
					<tr>
						<td>
							<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; WIDTH: 974px; HEIGHT: 225px; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
								id="Table1" border="0" cellSpacing="0" cellPadding="5" width="974" align="center">
								<TR>
									<TD class="TituloRConsulta" align="center">Detalle Recaudaciones - Deuda</TD>
								</TR>
								<TR>
									<TD>
										<TABLE id="Table5" class="Arial12b" cellPadding="3">
											<tbody>
												<tr>
													<td style="PADDING-BOTTOM: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 0px">
														<div style="WIDTH: 1024px;OVERFLOW-Y:scroll;OVERFLOW-X:scroll; Height:400px;" class="frame2">
															<asp:datagrid style="Z-INDEX: 0" id="DGLista" runat="server" AutoGenerateColumns="False" CssClass="Arial12b"
																DataKeyField="ID_T_TRS_REG_DEUDA" AllowSorting="True">
																<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
																<ItemStyle CssClass="RowOdd"></ItemStyle>
																<HeaderStyle CssClass="Arial12b"></HeaderStyle>
																<Columns>
																	<asp:TemplateColumn HeaderText="Nro. Transacción" ItemStyle-HorizontalAlign="Center" SortExpression="NRO_TRANSACCION">
																		<HeaderStyle Wrap="False" Width="110px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_TRANSACCION") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_TRANSACCION") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Nombre de deudor" ItemStyle-HorizontalAlign="Left" SortExpression="NOM_DEUDOR">
																		<HeaderStyle Wrap="False" Width="110px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NOM_DEUDOR") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NOM_DEUDOR") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Ruc Deudor" ItemStyle-HorizontalAlign="Center" SortExpression="RUC_DEUDOR">
																		<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.RUC_DEUDOR") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.RUC_DEUDOR") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Oficina Venta" ItemStyle-HorizontalAlign="Left" SortExpression="NOM_OF_VENTA">
																		<HeaderStyle Wrap="False" Width="160px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NOM_OF_VENTA") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NOM_OF_VENTA") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Sub Oficina" ItemStyle-HorizontalAlign="Center" SortExpression="SUB_OFICINA_DESC">
																		<HeaderStyle Wrap="False" Width="160px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																		<%# DataBinder.Eval(Container,"DataItem.SUB_OFICINA_DESC") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																		<%# DataBinder.Eval(Container,"DataItem.SUB_OFICINA_DESC") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Fecha Trans." ItemStyle-HorizontalAlign="Center" SortExpression="FECHA_TRANSAC">
																		<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# format(DataBinder.Eval(Container,"DataItem.FECHA_TRANSAC"), "dd/MM/yyyy") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# format(DataBinder.Eval(Container,"DataItem.FECHA_TRANSAC"), "dd/MM/yyyy")  %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Hora Trans." ItemStyle-HorizontalAlign="Center" SortExpression="HORA_TRANSAC">
																		<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# format(DataBinder.Eval(Container,"DataItem.HORA_TRANSAC"), "HH:mm:ss") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# format(DataBinder.Eval(Container,"DataItem.HORA_TRANSAC"), "HH:mm:ss") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Moneda" ItemStyle-HorizontalAlign="Center" SortExpression="MONEDA">
																		<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.MONEDA") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.MONEDA") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Importe Pago" ItemStyle-HorizontalAlign="Right" SortExpression="IMPORTE_PAGO">
																		<HeaderStyle Wrap="False" Width="70px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.IMPORTE_PAGO"),"###0.00") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.IMPORTE_PAGO"),"###0.00") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Estado" ItemStyle-HorizontalAlign="Center" SortExpression="ESTADO">
																		<HeaderStyle Wrap="False" Width="120px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.ESTADO") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<asp:DropDownList ID="cboEstado" Runat="server" CssClass="clsSelectEnable"></asp:DropDownList>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Nro. Teléfono" ItemStyle-HorizontalAlign="Center" SortExpression="NRO_TELEFONO">
																		<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_TELEFONO") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_TELEFONO") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Cod. Cajero" ItemStyle-HorizontalAlign="Center" SortExpression="COD_CAJERO">
																		<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.COD_CAJERO") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.COD_CAJERO") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Cajero" ItemStyle-HorizontalAlign="Center" SortExpression="NOM_CAJERO">
																		<HeaderStyle Wrap="False" Width="270px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NOM_CAJERO") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NOM_CAJERO") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Tipo Doc. Deudor" ItemStyle-HorizontalAlign="Center" SortExpression="TIPO_DOC_DEUDOR">
																		<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.TIPO_DOC_DEUDOR") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.TIPO_DOC_DEUDOR") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Nro Doc Deudor" ItemStyle-HorizontalAlign="Center" SortExpression="NRO_DOC_DEUDOR">
																		<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_DOC_DEUDOR") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_DOC_DEUDOR") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:EditCommandColumn HeaderText="Opción" ButtonType="LinkButton" UpdateText="Aceptar" CancelText="Cancelar"
																		EditText="Editar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px"
																		ItemStyle-Wrap="False"></asp:EditCommandColumn>
																</Columns>
															</asp:datagrid></div>
													</td>
												</tr>
											</tbody>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
							<br>
							<div id="divComandos">
								<div style="float:left; padding-left: 350px;">
								<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; WIDTH: 245px; HEIGHT: 50px; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
									id="Table2" border="0" cellSpacing="0" cellPadding="2" align="center">
									<TR>
										<TD align="center">
											<TABLE id="Table4" align="center" style="WIDTH: 248px; HEIGHT: 44px">
												<TR>
													<TD style="WIDTH: 250px">
														&nbsp;&nbsp;&nbsp; <input style="WIDTH: 100px" id="btnExcel" class="BotonOptm" onclick="f_Exportar()" value="Exportar Excel"
															type="button" name="btnExcel"> &nbsp;&nbsp;&nbsp;<asp:button style="Z-INDEX: 0" id="btnCancelar" runat="server" CssClass="BotonOptm" Width="100px"
															Text="Regresar"></asp:button>
													</TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
								</TABLE>
								</div>
								<div style="clear:both;"></div>
							</div>
						</td>
					</tr>
				</TBODY>
			</table>
		</form>
		<script language=JavaScript>
			function f_Exportar()
			{
				document.frmTmp.action = 'visorRecDeuda_excel.aspx';
				document.frmTmp.submit();
			}
		</script>
		<form method=post name=frmTmp action="" target=_blank>
		</FORM>
	</body>
</HTML>
