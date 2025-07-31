<%@ Page Language="vb" AutoEventWireup="false" Codebehind="configPaycall_Lista.aspx.vb" Inherits="SisCajas.configPaycall_Lista" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>América Móvil</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK rel="stylesheet" type="text/css" href="../Estilos/est_General.css">
	<script type="text/javascript" src="../librerias/Lib_FuncGenerales.js"></script>
	<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
	<script language="javascript">

		function f_Nuevo(){
			Direcc = "configPaycall_nuevo.aspx?CodCtaRemesa=0"
			window.open(Direcc,"Adiciona","directories=no,menubar=no,scrollbars=no,top=200,resizable=yes,left=185,width=480,height=290");
		}
		
		function f_Actualizar(var1){
			Direcc = "configPaycall_nuevo.aspx?CodCtaRemesa="+var1
			window.open(Direcc,"Actuali","directories=no,menubar=no,scrollbars=no,top=200,resizable=yes,left=185,width=480,height=290");
		}
		function f_Refrescar(){
			frmListarConfigPayCall.refreshHandler.click();
		}
		function f_Eliminar(var1){
			var rpta = window.confirm('¿Desea eliminar el registro seleccionado?');
			if (rpta) {
				document.frmListarConfigPayCall.hidIDCtaRemesa.value = var1;
				frmListarConfigPayCall.eliminarHandler.click();
			}
		}

	</script>
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="frmListarConfigPayCall" method="post" runat="server">
		<input id="hidIDCtaRemesa" type="hidden" name="hidIDCtaRemesa" value="0">
		<table border="0" cellSpacing="0" cellPadding="0">
			<TBODY>
				<tr>
					<td>
						<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
							id="Table1" border="0" cellSpacing="0" cellPadding="5" width="750" align="center">
							<TR>
								<TD class="TituloRConsulta" align="center">Detalle Configuraci&oacute;n Paycall</TD>
							</TR>
							<TR>
								<TD>
									<TABLE id="Table5" class="Arial12b" cellPadding="3">
										<tbody>
											<tr>
												<td style="PADDING-BOTTOM: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 0px"
													colSpan="7">
													<div style="WIDTH: 550px" class="frame2">
														<asp:datagrid style="Z-INDEX: 0" id="DGLista" runat="server" CssClass="Arial11b" AutoGenerateColumns="False">
															<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
															<ItemStyle CssClass="RowOdd"></ItemStyle>
															<HeaderStyle CssClass="Arial12b"></HeaderStyle>
															<Columns>
																<asp:BoundColumn DataField="ID_CTAREMESA" HeaderText="ID" Visible="False"></asp:BoundColumn>
																<asp:BoundColumn DataField="OFICINA_VENTA" HeaderText="Oficina">
																	<HeaderStyle Wrap="False" Width="200px" HorizontalAlign="Left"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Left"></ItemStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="PAYCALL" HeaderText="Paycall">
																	<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="TIPO_REMESA" HeaderText="Tipo Remesa">
																	<HeaderStyle Wrap="False" Width="70px" HorizontalAlign="Center"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="TIPO_REMESA_DES" HeaderText="Desc. Remesa">
																	<HeaderStyle Wrap="False" Width="150px" HorizontalAlign="Center"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="CTABANCO" HeaderText="Cta Mayor">
																	<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																</asp:BoundColumn>
																<asp:TemplateColumn HeaderText="Opción">
																	<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	<ItemTemplate>
																			<img src="../images/botones/edit.gif" style="cursor:hand" onclick="f_Actualizar('<%# DataBinder.Eval(Container,"DataItem.ID_CTAREMESA")%>')" alt="Editar"> &nbsp;&nbsp;&nbsp;
																			<img src="../images/botones/ico_eliminar.gif" style="cursor:hand" onclick="f_Eliminar('<%# DataBinder.Eval(Container,"DataItem.ID_CTAREMESA")%>')" alt="Eliminar" >
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
							<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
								id="Table2" border="0" cellSpacing="0" cellPadding="2" align="center">
								<TR>
									<TD align="center">
										<TABLE id="Table4" align="center">
											<TR>
												<td><INPUT style="WIDTH: 93px; HEIGHT: 19px" id="cmdNuevo" onclick="f_Nuevo();" class="BotonOptm"
														value="Nuevo" type="submit" name="cmdNuevo" runat="server"></td>
												<TD style="WIDTH: 96px"><asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Width="100px" Text="Cancelar"></asp:button>
													<asp:Button id="eliminarHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
													<asp:Button id="refreshHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</div>
					</td>
				</tr>
			</TBODY>
		</table>
    </form>
  </body>
</html>
