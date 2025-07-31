<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConfTransaccion.aspx.vb" Inherits="SisCajas.ConfTransaccion"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ConfTransaccion</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmTransaccion" method="post" runat="server">
			<table>
				<tr>
					<td>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="600" align="left" border="1">
							<TBODY>
								<tr>
									<td>
										<table class="Arial10B" cellSpacing="0" cellPadding="0" width="475" align="center" border="0">
											<tr>
												<td width="10" height="4" border="0"></td>
												<td class="TituloRConsulta" align="center" width="90%" height="32">Permisos por 
													transacción</td>
												<td vAlign="top" width="14" height="32"></td>
											</tr>
										</table>
										<br>
										<br>
										<table cellSpacing="2" cellPadding="2" width="500" border="0">
											<tbody>
												<tr>
													<td class="Arial12b" style="WIDTH: 251px" width="251"><b>&nbsp;&nbsp;&nbsp;&nbsp;Registro 
															de transacciones &nbsp;</b></td>
													<td vAlign="top" width="150">&nbsp;</td>
												</tr>
												<tr>
													<td align="center" colSpan="2">&nbsp;
														<asp:datagrid class="Arial12b" id="DGTransaccion" runat="server" Width="392px" AutoGenerateColumns="False">
															<HeaderStyle Font-Bold="True"></HeaderStyle>
															<Columns>
																<asp:BoundColumn Visible="False" DataField="ID_CONFTRAN"></asp:BoundColumn>
																<asp:BoundColumn DataField="TRANS_ABREV" HeaderText="Abrev">
																	<HeaderStyle Width="100px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="TRANS_DESC" HeaderText="Descripci&#243;n">
																	<HeaderStyle Width="200px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:TemplateColumn HeaderText="Requiere permiso">
																	<HeaderStyle Width="110px"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
																	<ItemTemplate>
																		<asp:CheckBox id="CheckBox1" runat="server"></asp:CheckBox>
																	</ItemTemplate>
																</asp:TemplateColumn>
															</Columns>
														</asp:datagrid></td>
												</tr>
											</tbody>
										</table>
										<br>
										<br>
									</td>
								</tr>
							</TBODY>
						</table>
					</td>
				</tr>
				<tr>
					<td><br>
						<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="450" align="center"
							border="1">
							<tr>
								<td>
									<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td align="center">
												<input class="BotonOptm" id="cmdNuevo" style="WIDTH: 100px" onclick="javascript:f_Nuevo()"
													value="Nuevo" name="cmdNuevo"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<input class="BotonOptm" id="cmdCancelar" style="WIDTH: 100px" onclick="javascript:f_Cancelar();"
													value="Cancelar" name="cmdCancelar"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<input class="BotonOptm" id="cmdGrabar" style="WIDTH: 100px" type="button" value="Grabar"
													name="btnGrabar" runat="server">&nbsp;&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br>
			</TD></TR></form>
		<script language="javascript">
	function f_Cancelar(){
		history.back();
	}
	function f_Nuevo(){
		Direcc = "NuevoPermisoTrans.aspx?CodTurno=NUEVO"
		window.open(Direcc,"Adiciona","directories=no,menubar=no,scrollbars=no,top=200,resizable=yes,left=185,width=430,height=375");
  }
  
		</script>
	</body>
</HTML>
