<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SICAR_FormaPagos_Excel.aspx.vb" Inherits="SisCajas.SICAR_FormaPagos_Excel"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SICAR_FormaPagos_Excel</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="1000">
				<tr>
					<td style=" TEXT-ALIGN: center; FONT-SIZE: 20px; FONT-WEIGHT: bold" colSpan="4">Devolución 
						de Efectivo&nbsp;<B>Formas de Pago del Documento Pagado</B></td>
				</tr>
				<tr>
					<td>
						<table border="1" cellSpacing="2" borderColor="white" cellPadding="0" width="100%">
							<tr>
								<td width="20">&nbsp;</td>
								<td class="Arial12b" style="FONT-WEIGHT: bold"  width="160">&nbsp;&nbsp;&nbsp;Nombre Cliente :</td>
								<td width="170" colSpan="4"><asp:label id="txtNombCli" runat="server" Width="632px" Height="22"></asp:label>
								</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td class="Arial12b"  style="FONT-WEIGHT: bold" width="170">Nro Fact SUNAT:</td>
								<td class="Arial12b"><asp:label id="txtBolFact" runat="server" Width="208px" Height="20px"></asp:label></td>
								<td height="22" width="25">&nbsp;</td>
								<td class="Arial12b"  style="FONT-WEIGHT: bold" width="146" style="WIDTH: 146px">&nbsp;&nbsp;&nbsp;Fecha :</td>
								<td class="Arial12b"><asp:label id="txtFechPago" tabIndex="2" runat="server" Width="208px" Height="20px"></asp:label>
								</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td class="Arial12b"  style="FONT-WEIGHT: bold" width="170">&nbsp;&nbsp;&nbsp;Nro Pedido :</td>
								<td class="Arial12b"><asp:label id="txtNroPedido" runat="server" Width="208px" Height="20px"></asp:label></td>
								<td height="22" width="25">&nbsp;</td>
								<td class="Arial12b"  style="FONT-WEIGHT: bold" width="146" style="WIDTH: 146px">&nbsp;&nbsp;&nbsp;Monto 
									Pagado :</td>
								<td class="Arial12b"><asp:label id="txtMonto" runat="server" Width="208px" Height="20px"></asp:label></td>
							</tr>
						</table>
					</td>
				<tr>
					<td><asp:datagrid id="dgTransacciones" runat="server" AutoGenerateColumns="False" CssClass="Arial11B"
							Width="100%">
							<AlternatingItemStyle BackColor="#FFFFFF"></AlternatingItemStyle>
							<ItemStyle BackColor="#EEEEEE"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" Font-Bold="True"
								CssClass="Arial12B"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="FORMA_PAGO" HeaderText="Forma Pago">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TIPO_TARJETA_POS" HeaderText="Tipo Tarjeta">
									<HeaderStyle Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NRO_TARJETA" HeaderText="Numero Tarjeta">
									<HeaderStyle Width="30%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MONTO" HeaderText="Monto Pagado">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ESTADO_ANULACION" HeaderText="Estado Anulacion">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="RESULTADO_PROCESO" HeaderText="Resultado Proceso">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
