<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConsultaCajaInd_detalle.aspx.vb" Inherits="SisCajas.ConsultaCajaInd_detalle" %>
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
		
		
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmCCIDetalle" method="post" runat="server">
			<table border="0" cellSpacing="0" cellPadding="0">
				<TBODY>
					<tr>
						<td>
							<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
								id="Table1" border="0" cellSpacing="0" cellPadding="5" width="850" align="center">
								<TR>
									<TD class="TituloRConsulta" align="center">Cuadre Individual</TD>
								</TR>
								<TR>
									<TD>
										<table cellSpacing="0" cellPadding="0" width="790" align="center" border="0">
											<tr>
												<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 5px"
													width="98%">
													<table cellSpacing="0" cellPadding="0" width="770" border="0">
														<tr>
															<td height="4"></td>
														</tr>
														<tr>
															<td style="PADDING-LEFT: 1px" height="18">
																<div class="frame2" style="Z-INDEX: 1; BORDER-BOTTOM: 1px; POSITION: relative; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 820px; PADDING-RIGHT: 5px; HEIGHT: 480px; BORDER-TOP: 1px; BORDER-RIGHT: 1px">
																	<asp:datagrid style="Z-INDEX: 0" id="DGLista" runat="server" BorderWidth="0px" CellPadding="1"
																		CellSpacing="1" CssClass="tabla_interna_borde1" Width="100%" AutoGenerateColumns="False">
																		<AlternatingItemStyle Height="25px" BackColor="#DEE9FA"></AlternatingItemStyle>
																		<ItemStyle Height="25px" BorderWidth="0px" CssClass="Arial12B" BackColor="#D0D8F0"></ItemStyle>
																		<HeaderStyle HorizontalAlign="Center" Height="21px" CssClass="Arial12B"></HeaderStyle>
																		<Columns>
																			<asp:BoundColumn DataField="OFICINA" HeaderText="Oficina">
																				<HeaderStyle Wrap="False" Width="120px" HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left"></ItemStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
																				<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Center"></ItemStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="CAJERO" HeaderText="Cajero">
																				<HeaderStyle Wrap="False" Width="150px" HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left"></ItemStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="DESCRIPCION" HeaderText="Descripción">
																				<HeaderStyle Wrap="False" Width="300px" HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="MONTO" HeaderText="Monto" DataFormatString="{0:N2}">
																				<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Right" CssClass="clsPafLeft"></ItemStyle>
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
						</td>
					</tr>
				</TBODY>
			</table>
			<div id="divComandos" style="PADDING-TOP:10px">
				<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; WIDTH: 151px; HEIGHT: 21px; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
					id="Table2" border="0" cellSpacing="0" cellPadding="2" align="center">
					<TR>
						<TD align="center">
							<TABLE style="WIDTH: 136px; HEIGHT: 13px" id="Table4" align="center">
								<TR>
									<TD style="WIDTH: 250px">
										&nbsp;&nbsp;&nbsp; <input style="WIDTH: 100px" id="btnExcel" class="BotonOptm" onclick="f_Exportar()" value="Exportar Excel"
											type="button" name="btnExcel">
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</div>
		</form>
		<script language=JavaScript>
			function f_Exportar()
			{
				document.frmTmp.action = 'ConsultaCajaInd_excel.aspx';
				document.frmTmp.submit();
			}
		</script>
		<form method=post name=frmTmp action="" target=_blank>
		</form>
	</body>
</HTML>
