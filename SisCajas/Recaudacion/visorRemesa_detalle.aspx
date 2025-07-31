<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorRemesa_detalle.aspx.vb" Inherits="SisCajas.visorRemesa_detalle"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>América Móvil</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script type="text/javascript" src="../librerias/Lib_FuncGenerales.js"></script>
		<LINK rel=styleSheet type=text/css href="../estilos/est_General.css" >
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="JavaScript">
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmVisRemDet" method="post" runat="server">
			<input id="hdnOrdenacion" runat="server" type="hidden" name="hdnOrdenacion">
			<table border="0" cellSpacing="0" cellPadding="0">
				<TBODY>
					<tr>
						<td>
							<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; WIDTH: 974px; HEIGHT: 225px; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
								id="Table1" border="0" cellSpacing="0" cellPadding="5" width="974" align="center">
								<TR>
									<TD class="TituloRConsulta" align="center">Detalle Remesas</TD>
								</TR>
								<TR>
									<TD>
										<TABLE id="Table5" class="Arial12b" cellPadding="3">
											<tbody>
												<tr>
													<td style="PADDING-BOTTOM: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 0px">
														<div style="WIDTH: 1024px;OVERFLOW-Y:scroll;OVERFLOW-X:scroll; Height:400px;" class="frame2">
															<!-- INI-936 - CNSO - Agregada columna COMPROBANTE al final de la tabla-->
															<asp:datagrid style="Z-INDEX: 0" id="DGLista" runat="server"
																CssClass="Arial12b" AutoGenerateColumns="False" AllowSorting="True">
																<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
																<ItemStyle CssClass="RowOdd"></ItemStyle>
																<HeaderStyle CssClass="Arial12b"></HeaderStyle>
																<Columns>
																	<asp:TemplateColumn HeaderText="Nro. Bolsa" ItemStyle-HorizontalAlign="Center" SortExpression="BOLSA">
																		<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.BOLSA") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.BOLSA") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Nro. Sobre" ItemStyle-HorizontalAlign="Center" SortExpression="SOBRE">
																		<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.SOBRE") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.SOBRE") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Fecha Remesa" ItemStyle-HorizontalAlign="Center" SortExpression="FECHA_ENVIO_REMESA">
																		<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# format(DataBinder.Eval(Container,"DataItem.FECHA_ENVIO_REMESA"), "dd/MM/yyyy") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# format(DataBinder.Eval(Container,"DataItem.FECHA_ENVIO_REMESA"), "dd/MM/yyyy") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Hora Remesa" ItemStyle-HorizontalAlign="Center" SortExpression="HORA_ENVIO_REMESA">
																		<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# format(DataBinder.Eval(Container,"DataItem.HORA_ENVIO_REMESA"), "HH:mm:ss") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# format(DataBinder.Eval(Container,"DataItem.HORA_ENVIO_REMESA"), "HH:mm:ss") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Fecha Buzón" ItemStyle-HorizontalAlign="Center" SortExpression="BUZON_FECHA">
																		<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# format(DataBinder.Eval(Container,"DataItem.BUZON_FECHA"), "dd/MM/yyyy") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# format(DataBinder.Eval(Container,"DataItem.BUZON_FECHA"), "dd/MM/yyyy") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Hora Buzón" ItemStyle-HorizontalAlign="Center" SortExpression="BUZON_HORA">
																		<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# format(DataBinder.Eval(Container,"DataItem.BUZON_HORA"), "HH:mm:ss") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# format(DataBinder.Eval(Container,"DataItem.BUZON_HORA"), "HH:mm:ss") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Código Usuario" ItemStyle-HorizontalAlign="Center" SortExpression="COD_USUARIO_ENVIA_REMESA">
																		<HeaderStyle Wrap="False" Width="70px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.COD_USUARIO_ENVIA_REMESA") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.COD_USUARIO_ENVIA_REMESA") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Usuario Remesa" ItemStyle-HorizontalAlign="Center" SortExpression="USUARIO_ENVIA_REMESA">
																		<HeaderStyle Wrap="False" Width="200px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.USUARIO_ENVIA_REMESA") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.USUARIO_ENVIA_REMESA") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Importe" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO">
																		<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.MONTO") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.MONTO") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Tipo Remesa" ItemStyle-HorizontalAlign="Center" SortExpression="COD_TIPO">
																		<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.COD_TIPO") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.COD_TIPO") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Descripción remesa" ItemStyle-HorizontalAlign="Center" SortExpression="TIPO">
																		<HeaderStyle Wrap="False" Width="150px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.TIPO") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.TIPO") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Oficina de Venta" ItemStyle-HorizontalAlign="Center" SortExpression="OFICINA">
																		<HeaderStyle Wrap="False" Width="180px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.OFICINA") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.OFICINA") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Doc. Contable" ItemStyle-HorizontalAlign="Center" SortExpression="DOCUMENTO">
																		<HeaderStyle Wrap="False" Width="90px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.DOCUMENTO") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.DOCUMENTO") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Usuario FI" ItemStyle-HorizontalAlign="Center" SortExpression="USUARIO_CONTABILIZA">
																		<HeaderStyle Wrap="False" Width="150px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.USUARIO_CONTABILIZA") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.USUARIO_CONTABILIZA") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Fecha FI" ItemStyle-HorizontalAlign="Center" SortExpression="FECHA_CONTABILIZA">
																		<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.FECHA_CONTABILIZA") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.FECHA_CONTABILIZA") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Hora FI" ItemStyle-HorizontalAlign="Center" SortExpression="HORA_CONTABILIZA">
																		<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.HORA_CONTABILIZA") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.HORA_CONTABILIZA") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Estado" ItemStyle-HorizontalAlign="Center" SortExpression="ESTADO_CONT">
																		<HeaderStyle Wrap="False" Width="160px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.ESTADO_CONT") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.ESTADO_CONT") %>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Comp. Servicio" ItemStyle-HorizontalAlign="Center" SortExpression="COMPROBANTE">
																		<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.COMPROBANTE") %>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<%# DataBinder.Eval(Container,"DataItem.COMPROBANTE") %>
																		</EditItemTemplate>
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
								<div style="clear:both;"></div>
							</div>
						</td>
					</tr>
				</TBODY>
			</table>
		</form>
		<script language="JavaScript">
			function f_Exportar()
			{
				<%Session("ListaConsultarRemesa") = tblExportar%>; //INI-936 - CNSO
				document.frmTmp.action = 'visorRemesa_excel.aspx';
				document.frmTmp.submit();
			}
		</script>
		<form method="post" name="frmTmp" action="" target="_blank">
		</form>
	</body>
</HTML>
