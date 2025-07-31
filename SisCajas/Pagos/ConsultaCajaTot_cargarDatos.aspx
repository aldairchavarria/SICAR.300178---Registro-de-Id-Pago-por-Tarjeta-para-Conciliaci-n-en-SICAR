<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConsultaCajaTot_cargarDatos.aspx.vb" Inherits="SisCajas.ConsultaCajaTot_CargarDatos" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>América Móvil</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../Estilos/est_General.css">
		<script type="text/javascript" src="../librerias/Lib_FuncGenerales.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
		
			function f_buscar(){
				if(event.keyCode == 13 ){
					frmCCTCargarDatos.cmdBuscar.click();
				}
			}
			
		</script>
		
	</HEAD>
	<body>
		<form id="frmCCTCargarDatos" method="post" runat="server">
			<input id="hdnOrdenacion" runat="server" type="hidden" name="hdnOrdenacion">
			<table border="0" cellSpacing="0" cellPadding="0">
				<TBODY>
					<tr>
						<td>
							<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
								id="Table1" border="0" cellSpacing="0" cellPadding="5" width="850" align="center">
								<TR>
									<TD class="TituloRConsulta" align="center">Cuadre Total</TD>
								</TR>
								<tr>
									<td>
										<TABLE id="Table2" class="Arial12b" cellPadding="2">
											<tbody>
												<tr>
													<td><asp:label id="lblFiltro" runat="server" CssClass="Arial12b">Buscar :</asp:label></td>
													<td><input style="WIDTH: 200px" id="txtFiltro" class="clsInputEnable" maxLength="30" size="25"
															name="txtFiltro" runat="server"></td>
													<td><input style="WIDTH: 100px" id="cmdBuscar" class="BotonOptm" value="Buscar" type="button"
															name="cmdBuscar" runat="server">&nbsp;&nbsp;
													</td>
												</tr>
											</tbody>
										</TABLE>
									</td>
								</tr>
								<TR>
									<TD>
										<table cellSpacing="0" cellPadding="0" width="790" align="center" border="0">
											<tr>
												<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
													width="98%">
													<table cellSpacing="0" cellPadding="0" width="770" border="0">
														<tr>
															<td height="4"></td>
														</tr>
														<tr>
															<td style="PADDING-LEFT: 5px" height="18">
																<div class="frame2" style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; Z-INDEX: 1; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 820px; BORDER-BOTTOM: 1px; POSITION: relative; HEIGHT: 420px; TEXT-ALIGN: center; padding-right:5px;">
																	<asp:datagrid style="Z-INDEX: 0" id="DGLista" runat="server" BorderWidth="0px" CellPadding="1"
																		CellSpacing="1" CssClass="tabla_interna_borde1" Width="100%" AutoGenerateColumns="False"
																		AllowSorting="True">
																		<AlternatingItemStyle Height="25px" BackColor="#DEE9FA"></AlternatingItemStyle>
																		<ItemStyle Height="25px" BorderWidth="0px" CssClass="Arial12B" BackColor="#D0D8F0"></ItemStyle>
																		<HeaderStyle HorizontalAlign="Center" Height="21px" CssClass="Arial12B"></HeaderStyle>
																		<Columns>
																			<asp:BoundColumn DataField="OFICINA" HeaderText="Oficina" SortExpression="OFICINA">
																				<HeaderStyle Wrap="False" Width="120px" HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left"></ItemStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="FECHA" HeaderText="Fecha" SortExpression="FECHA" DataFormatString="{0:dd/MM/yyyy}">
																				<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Center"></ItemStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="CAJERO" HeaderText="Cajero" SortExpression="CAJERO">
																				<HeaderStyle Wrap="False" Width="150px" HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left"></ItemStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="DESCRIPCION" HeaderText="Descripción" SortExpression="DESCRIPCION">
																				<HeaderStyle Wrap="False" Width="350px" HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left"></ItemStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="MONTO" HeaderText="Monto" DataFormatString="{0:N2}" SortExpression="MONTO">
																				<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Right"></ItemStyle>
																			</asp:BoundColumn>
																		</Columns>
																	</asp:datagrid>
																</div>
															</td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</TD>
								</TR>
							</TABLE>
							<br>
							<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="360" align="center"
								border="1">
								<tr>
									<td>
										<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
											<tr>
												<td align="center">
													&nbsp;&nbsp;&nbsp; <input style="WIDTH: 100px" id="btnExcel" class="BotonOptm" onclick="f_Exportar()" value="Exportar Excel"
																		type="button" name="btnExcel">
													&nbsp;<asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Width="100px" Text="Regresar"></asp:button>
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<br>
						</td>
					</tr>
				</TBODY>
			</table>
		</form>
		<script language=JavaScript>
			function f_Exportar()
			{
				document.frmTmp.action = 'ConsultaCajaTot_excel.aspx';
				document.frmTmp.submit();
			}
		</script>
		<form method=post name=frmTmp action="" target=_blank>
		</form>
	</body>
</HTML>
