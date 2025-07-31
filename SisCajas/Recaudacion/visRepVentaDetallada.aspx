<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visRepVentaDetallada.aspx.vb" Inherits="SisCajas.visRepVentaDetallada" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>América Móvil</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script type="text/javascript" src="../librerias/Lib_FuncGenerales.js"></script>
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="JavaScript">
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmVisVtaFactDet" method="post" runat="server">
			<input id="hdnOrdenacion" runat="server" type="hidden" name="hdnOrdenacion">
			<table border="0" cellSpacing="0" cellPadding="0">
				<TBODY>
					<tr>
						<td>
							<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; WIDTH: 974px; HEIGHT: 225px; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
								id="Table1" border="0" cellSpacing="0" cellPadding="5" width="974" align="center">
								<TR>
									<TD class="TituloRConsulta" align="center">Facturación Detallada</TD>
								</TR>
								<TR>
									<TD>
										<TABLE id="Table5" class="Arial12b" cellPadding="3">
											<tbody>
												<tr>
													<td style="PADDING-BOTTOM: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 0px">
														<div style="WIDTH: 1024px;OVERFLOW-Y:scroll;OVERFLOW-X:scroll; Height:400px;" class="frame2">
															<asp:datagrid style="Z-INDEX: 0" id="DGLista" runat="server" CssClass="Arial12b" AutoGenerateColumns="False"
																AllowSorting="True" PageSize="15" AllowPaging="True">
																<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
																<ItemStyle CssClass="RowOdd"></ItemStyle>
																<HeaderStyle CssClass="Arial12b"></HeaderStyle>
																<Columns>
																	<asp:TemplateColumn SortExpression="DESC_OFICINA" HeaderText="Oficina de Venta">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="150px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.DESC_OFICINA") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="CAJERO" HeaderText="Cod. Cajero">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="80px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.CAJERO") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="NOM_CAJERO" HeaderText="Cajero">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="200px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.NOM_CAJERO") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="FECHA" HeaderText="Fecha">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="80px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.FECHA")  %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="TIPO_DOCUMENTO" HeaderText="Tipo Documento">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="220px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.TIPO_DOCUMENTO") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="DESC_DOCUMENTO" HeaderText="Documento">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="210px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.DESC_DOCUMENTO") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="FACTURA_FICTICIA" HeaderText="Nro. de Pedido">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="140px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.FACTURA_FICTICIA") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="REFERENCIA" HeaderText="Doc Sunat">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="130px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.REFERENCIA") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="COD_VENDEDOR" HeaderText="Cod. Vendedor">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="90px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.COD_VENDEDOR") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="VENDEDOR" HeaderText="Vendedor">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="200px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.VENDEDOR") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="MONEDA" HeaderText="Moneda">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="70px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.MONEDA") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="CLASE_FACTURA_COD" HeaderText="Clase Factura">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="90px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.CLASE_FACTURA_COD") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="CUOTA" HeaderText="Cuotas">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.CUOTA"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="TOTFA" HeaderText="Total Doc.">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="90px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.TOTFA"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZEFE" HeaderText="Efectivo">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZEFE"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZAEX" HeaderText="American Exp.">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZAEX"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZCAR" HeaderText="NetCard">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZCAR"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZCHQ" HeaderText="Cheque">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZCHQ"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZCIB" HeaderText="Cob. Interbank">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZCIB"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZDEL" HeaderText="Electron">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZDEL"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZDIN" HeaderText="Dinners">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZDIN"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZDMT" HeaderText="Maestro">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZDMT"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZMCD" HeaderText="MasterCard">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZMCD"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZRIP" HeaderText="Ripley">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZRIP"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZSAG" HeaderText="CMR">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZSAG"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZVIS" HeaderText="Visa">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZVIS"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZCRS" HeaderText="Carsa">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZCRS"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZCZO" HeaderText="Curacao">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZCZO"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZACE" HeaderText="ACE Home Center">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZACE"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="TDPP" HeaderText="Trans. deuda post">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.TDPP"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZNCR" HeaderText="Nota credito">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZNCR"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="ZEAM" HeaderText="Cuotas Empl. Claro">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.ZEAM"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="SALDO" HeaderText="Saldo">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.SALDO"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="CUO1" HeaderText="1 Cuota">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.CUO1"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="CUO6" HeaderText="6 Cuotas">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.CUO6"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="CUO12" HeaderText="12 Cuotas">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.CUO12"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="CUO18" HeaderText="18 Cuotas">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.CUO18"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="CUO24" HeaderText="24 Cuotas">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<%# Format(DataBinder.Eval(Container,"DataItem.CUO24"),"###0.00") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="DES_ESTADO" HeaderText="Estado">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="70px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.DES_ESTADO") %>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																</Columns>
																<PagerStyle VerticalAlign="Middle" Mode="NumericPages"></PagerStyle>
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
								<div style="PADDING-LEFT:400px; FLOAT:left">
									<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; WIDTH: 245px; HEIGHT: 48px; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
										id="Table2" border="0" cellSpacing="0" cellPadding="2" align="center">
										<TR>
											<TD align="center">
												<TABLE style="WIDTH: 250px; HEIGHT: 40px" id="Table4" align="center">
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
								<div style="CLEAR:both"></div>
							</div>
						</td>
					</tr>
				</TBODY>
			</table>
		</form>
		<script language="JavaScript">
			function f_Exportar()
			{
				document.frmTmp.action = 'visorVentaFact_excel.aspx';
				document.frmTmp.submit();
			}
		</script>
		<form method="post" name="frmTmp" action="" target="_blank">
		</form>
	</body>
</HTML>
