<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorRecRecibo_detalle.aspx.vb" Inherits="SisCajas.visorRecRecibo_detalle"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>América Móvil</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script type="text/javascript" src="../librerias/Lib_FuncGenerales.js"></script>
		<script language="JavaScript">
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmVisorRecReciboDet" method="post" runat="server">
		<input id="hdnOrdenacion" runat="server" type="hidden" name="hdnOrdenacion">
			<table border="0" cellSpacing="0" cellPadding="0">
				<TBODY>
					<tr>
						<td>
							<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; WIDTH: 974px; HEIGHT: 225px; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
								id="Table1" border="0" cellSpacing="0" cellPadding="5" width="974" align="center">
								<TR>
									<TD class="TituloRConsulta" align="center">Detalle Recaudaciones - Recibo</TD>
								</TR>
								<TR>
									<TD>
										<TABLE id="Table5" class="Arial12b" cellPadding="3">
											<tbody>
												<tr>
													<td style="PADDING-BOTTOM: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 0px">
														<div style="WIDTH: 1024px;OVERFLOW-Y:scroll;OVERFLOW-X:scroll; Height:400px;" class="frame2">
															<asp:datagrid style="Z-INDEX: 0" id="DGLista" runat="server" AutoGenerateColumns="False" CssClass="Arial12b" AllowSorting="True">
																<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
																<ItemStyle CssClass="RowOdd"></ItemStyle>
																<HeaderStyle CssClass="Arial12b"></HeaderStyle>
																<Columns>
																	<asp:TemplateColumn HeaderText="Nro. Transacción" ItemStyle-HorizontalAlign="Center" SortExpression="NRO_TRANSACCION" >
																		<HeaderStyle Wrap="False" Width="110px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_TRANSACCION") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Tipo Doc Recaud" ItemStyle-HorizontalAlign="Center" SortExpression="TIPO_DOC_RECAUD">
																		<HeaderStyle Wrap="False" Width="110px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.TIPO_DOC_RECAUD") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Nro Doc Recaud" ItemStyle-HorizontalAlign="Center" SortExpression="NRO_DOC_RECAUD">
																		<HeaderStyle Wrap="False" Width="110px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_DOC_RECAUD") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Moneda" ItemStyle-HorizontalAlign="Center" SortExpression="MONEDA">
																		<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.MONEDA") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Importe Recibo" ItemStyle-HorizontalAlign="Right" SortExpression="IMPORTE_RECIBO">
																		<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.IMPORTE_RECIBO"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Importe Pagado" ItemStyle-HorizontalAlign="Right" SortExpression="IMPORTE_PAGADO">
																		<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.IMPORTE_PAGADO"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Nro. Cobranza" ItemStyle-HorizontalAlign="Center" SortExpression="NRO_COBRANZA">
																		<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_COBRANZA") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Nro. Acreedor" ItemStyle-HorizontalAlign="Center" SortExpression="NRO_OPE_ACREE">
																		<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_OPE_ACREE") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Fecha Emisión" ItemStyle-HorizontalAlign="Center" SortExpression="FECHA_EMISION">
																		<HeaderStyle Wrap="False" Width="90px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# format(DataBinder.Eval(Container,"DataItem.FECHA_EMISION"), "dd/MM/yyyy") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Fecha Pago" ItemStyle-HorizontalAlign="Center" SortExpression="FECHA_PAGO">
																		<HeaderStyle Wrap="False" Width="90px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# format(DataBinder.Eval(Container,"DataItem.FECHA_PAGO"), "dd/MM/yyyy") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Nro Doc Deudor" ItemStyle-HorizontalAlign="Center" SortExpression="NRO_DOC_DEUDOR">
																		<HeaderStyle Wrap="False" Width="90px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_DOC_DEUDOR") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Trace pago" ItemStyle-HorizontalAlign="Center" SortExpression="NRO_TRACE_PAGO">
																		<HeaderStyle Wrap="False" Width="90px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_TRACE_PAGO") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Trace anulación" ItemStyle-HorizontalAlign="Center" SortExpression="NRO_TRACE_ANUL">
																		<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_TRACE_ANUL") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Desc. Servicio" ItemStyle-HorizontalAlign="Center" SortExpression="DESC_SERVICIO">
																		<HeaderStyle Wrap="False" Width="120px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.DESC_SERVICIO") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																</Columns>
															</asp:datagrid>
														</div>
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
											<TABLE id="Table4" style="WIDTH: 250px; HEIGHT: 40px"  align="center">
												<TR>
													<TD style="WIDTH: 250px">
														&nbsp;&nbsp;&nbsp; 
														<input style="WIDTH: 100px" id=btnExcel class=BotonOptm onclick=f_Exportar() value="Exportar Excel" type=button name=btnExcel>
														&nbsp;&nbsp;&nbsp;<asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Text="Regresar" Width="100px"></asp:button>
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
				document.frmTmp.action = 'visorRecRecibo_excel.aspx';
				document.frmTmp.submit();
			}
		</script>
		<form method=post name=frmTmp action="" target=_blank>
		</FORM>
	</body>
</HTML>
