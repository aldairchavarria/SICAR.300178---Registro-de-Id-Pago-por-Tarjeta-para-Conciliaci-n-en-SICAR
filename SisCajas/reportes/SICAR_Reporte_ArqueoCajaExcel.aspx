<%@ Page Language="vb" AutoEventWireup="false" Codepage="1252" Codebehind="SICAR_Reporte_ArqueoCajaExcel.aspx.vb" Inherits="SisCajas.SICAR_Reporte_ArqueoCajaExcel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Reporte_ArqueoCaja</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0">
		<form id="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="1000" border="0">
				<tr>
					<td colspan="4">Reporte Arqueo Caja</td>
				<tr>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="1000" border="0">
				<tr>
					<td height="50" class="xl64" width="500" style="WIDTH: 500pt; HEIGHT: 15pt; FONT-WEIGHT: bold">
						CAC con más arqueos</td>
				<tr>
				</tr>
			</table>
			<table>
				<tr>
					<td width="100">FechaDesde:</td>
					<td width="200">
						<asp:Label id="lblfechadesde" runat="server"></asp:Label></td>
				</tr>
			</table>
			<table>
				<tr>
					<td width="100">FechaHasta:</td>
					<td width="200">
						<asp:Label id="lblfechahasta" runat="server"></asp:Label></td>
					<td width="100"></td>
					<td width="100">Hora Reporte:</td>
					<td width="200">
						<asp:Label id="lblHora" runat="server">Label</asp:Label></td>
				</tr>
			</table>
			</TR>
			<tr>
				<td colspan="4">
					<table width="100%">
						<TR>
							<td>
								<asp:datagrid id="DgArqueoCaja" runat="server" BorderColor="Black" CssClass="Arial11B" AutoGenerateColumns="False"
									CellSpacing="1" Height="30px" Width="100%">
									<EditItemStyle ForeColor="#8080FF" BackColor="#C0C0FF"></EditItemStyle>
									<AlternatingItemStyle BackColor="#DDDEE2"></AlternatingItemStyle>
									<ItemStyle BackColor="#E9EBEE"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"
										VerticalAlign="Top"></HeaderStyle>
									<Columns>
										<asp:BoundColumn Visible="False" DataField="region" HeaderText="Region">
											<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
											<FooterStyle Font-Bold="True"></FooterStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="codigo" HeaderText="PDV">
											<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
											<FooterStyle Font-Bold="True"></FooterStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="descripcion" HeaderText="CAC">
											<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
											<FooterStyle Font-Bold="True"></FooterStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="diasArqueo" HeaderText="Dias de Arqueo">
											<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
											<FooterStyle Font-Bold="True"></FooterStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="diasLaborales" HeaderText="Dias de Laborables">
											<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
											<FooterStyle Font-Bold="True"></FooterStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="efectividad" HeaderText="% Efectividad">
											<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
											<FooterStyle Font-Bold="True"></FooterStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="pendiente" HeaderText="Pendiente">
											<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
										</asp:BoundColumn>
									</Columns>
								</asp:datagrid>
							</td>
						</TR>
					</table>
				</td>
			</tr>
			</TABLE>
		</form>
	</body>
</HTML>
