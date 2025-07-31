<%@ Page Language="vb" AutoEventWireup="false" Codepage="1252" Codebehind="rep_AnulaExcel.aspx.vb" Inherits="SisCajas.rep_AnulaExcel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>rep_OperaDetExcel</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<form id="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="1000" border="0">
				<tr>
					<td colSpan="4">Cuadre de Caja - Anulaciones por Cajero</td>
				<tr>
				</tr>
			</table>
			<table>
				<tr>
					<td width="100">Fecha:</td>
					<td width="200"><asp:label id="lblfecha" runat="server"></asp:label></td>
					<td width="100">Hora:</td>
					<td width="200"><asp:label id="lblHora" runat="server">Label</asp:label></td>
				</tr>
			</table>
			</TR><tr>
				<td colspan="4">
					<table width="100%">
						<TR>
							<td><asp:datagrid id="DgAnulacion" runat="server" Width="100%" Height="30px" CellSpacing="1" AutoGenerateColumns="False"
									CssClass="Arial11B" BorderColor="Black">
									<ItemStyle BackColor="#E9EBEE"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"
										VerticalAlign="Top"></HeaderStyle>
									<Columns>
										<asp:BoundColumn DataField="vbeln" HeaderText="Fact. SAP">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="50px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="xblnr" HeaderText="Doc. Sunat">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="80px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="netwr" HeaderText="Total Factura">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="150px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="D_ABRVW" HeaderText="Forma Pago">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="180px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="xblnr" HeaderText="Nro Tarjeta / Documento">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="100px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="mwsbk" HeaderText="Monto Pagado">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="120px"></HeaderStyle>
										</asp:BoundColumn>
									</Columns>
								</asp:datagrid></td>
						</TR>
					</table>
		</form>
	</body>
</HTML>
