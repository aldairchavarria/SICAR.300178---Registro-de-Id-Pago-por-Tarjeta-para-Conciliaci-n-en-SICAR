<%@ Page Language="vb" AutoEventWireup="false" Codebehind="recListaNombreApellido.aspx.vb" Inherits="SisCajas.recListaNombreApellido"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>recListaNombreApellido</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td class="TituloRConsulta" align="center">Resultados de la búsqueda
											</td>
										</tr>
									</table>
									<br>
									<div style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 740px; BORDER-BOTTOM: 1px; HEIGHT: 330px; TEXT-ALIGN: center"><asp:datagrid class="Arial11B" id="DGReporte" runat="server" Width="700px" AutoGenerateColumns="False"
											CellPadding="0" CellSpacing="1" BorderColor="White">
											<AlternatingItemStyle CssClass="RowOdd"></AlternatingItemStyle>
											<ItemStyle Height="22px" CssClass="RowEven"></ItemStyle>
											<HeaderStyle Wrap="False" HorizontalAlign="Center" CssClass="Arial12B"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="CUSTOMER_ID" HeaderText="Id.Cliente">
													<HeaderStyle Width="40px" CssClass="ColumnHeader"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CCNAME" HeaderText="Raz&#243;n social">
													<HeaderStyle Width="250px" CssClass="ColumnHeader"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CCFNAME" HeaderText="Nombre">
													<HeaderStyle Width="250px" CssClass="ColumnHeader"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CCLNAME" HeaderText="Apellido">
													<HeaderStyle Width="250px" CssClass="ColumnHeader"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Telefono">
													<HeaderStyle Width="70px" CssClass="ColumnHeader"></HeaderStyle>
													<ItemTemplate>
														<a href='resDocumentos.aspx?pTipoIdent=01&pIdent=<%# DataBinder.Eval(Container,"DataItem.DN_NUM") %>'
														</a>
														<%# DataBinder.Eval(Container,"DataItem.DN_NUM") %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td class="Arial12B" id="tdTotal" align="right" height="10"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<div id="Botones">
							<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="400" align="center"
								border="1">
								<tr>
									<td align="center">
										<table cellSpacing="2" cellPadding="0" border="0">
											<tr>
												<td align="center" width="28"></td>
												<td align="center" width="60"><asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Width="100px" Text="Cancelar"></asp:button></td>
												<td align="center" width="28"></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
