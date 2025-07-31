<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorRecPago_detalle.aspx.vb" Inherits="SisCajas.visorRecPago_detalle"%>
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
		<form id="frmVisPagDet" method="post" runat="server">
		<input id="hdnOrdenacion" runat="server" type="hidden" name="hdnOrdenacion">
			<table border="0" cellSpacing="0" cellPadding="0">
				<TBODY>
					<tr>
						<td>
							<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; WIDTH: 800px; HEIGHT: 225px; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
								id="Table1" border="0" cellSpacing="0" cellPadding="3" width="800" align="center">
								<TR>
									<TD class="TituloRConsulta" align="center">Detalle Recaudaciones - Pago</TD>
								</TR>
								<TR>
									<TD>
										<TABLE id="Table5" class="Arial12b" cellPadding="2">
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
																		<HeaderStyle Wrap="False" Width="130px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_TRANSACCION") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Via Pago" ItemStyle-HorizontalAlign="Center" SortExpression="DESC_VIA_PAGO">
																		<HeaderStyle Wrap="False" Width="150px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.DESC_VIA_PAGO") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Importe Pago" ItemStyle-HorizontalAlign="Right" SortExpression="IMPORTE_PAGADO">
																		<HeaderStyle Wrap="False" Width="90px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.IMPORTE_PAGADO"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Nro. Cheque / Tarjeta" ItemStyle-HorizontalAlign="Center" SortExpression="NRO_CHEQUE">
																		<HeaderStyle Wrap="False" Width="130px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NRO_CHEQUE") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>																	
																	<asp:TemplateColumn HeaderText="Doc. Contable" ItemStyle-HorizontalAlign="Center" SortExpression="DOC_CONTABLE">
																		<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.DOC_CONTABLE") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Usuario FI" ItemStyle-HorizontalAlign="Center" SortExpression="USUARIO_FI">
																		<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.USUARIO_FI") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Fecha Envio" ItemStyle-HorizontalAlign="Center" SortExpression="FECHA_FI">
																		<HeaderStyle Wrap="False" Width="150px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.FECHA_FI") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Estado" ItemStyle-HorizontalAlign="Center" SortExpression="ESTADO_CONT">
																		<HeaderStyle Wrap="False" Width="160px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.ESTADO_CONT") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
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
								<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; WIDTH: 255px; HEIGHT: 50px; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
									id="Table2" border="0" cellSpacing="0" cellPadding="2" align="center">
									<TR>
										<TD align="center">
											<TABLE id="Table4" align="center" style="WIDTH: 255px; HEIGHT: 44px">
												<TR>
													<TD style="WIDTH: 250px">
														&nbsp;&nbsp;&nbsp; <input style="WIDTH: 100px" id="btnExcel" class="BotonOptm" onclick="f_Exportar()" value="Exportar Excel"
															type="button" name="btnExcel"> &nbsp;&nbsp;&nbsp;
														<asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Text="Regresar" Width="100px"></asp:button>
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
				document.frmTmp.action = 'visorRecPago_excel.aspx';
				document.frmTmp.submit();
			}
		</script>
		<form method=post name=frmTmp action="" target=_blank>
		</FORM>
	</body>
</HTML>
