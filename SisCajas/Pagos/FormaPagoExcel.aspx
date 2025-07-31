<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FormaPagoExcel.aspx.vb" Inherits="SisCajas.FormaPagoExcel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>rep_ExcelPOS</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<style>
		</style>
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<form id="Form1" method="post" runat="server">
			<table width="1000">
				<tr>
					<td style="TEXT-ALIGN: center; FONT-SIZE: 20px; FONT-WEIGHT: bold" colSpan="4">FORMA 
						DE PAGO DEL DOCUMENTO PAGADO</td>
				<tr>
					<td><asp:datagrid id="dgFormasPago" runat="server" Width="100%" CssClass="Arial11B" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#FFFFFF"></AlternatingItemStyle>
							<ItemStyle BackColor="#EEEEEE"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" Font-Bold="true"
								CssClass="Arial12B"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="DEPAV_DESCPAGO" HeaderText="Forma Pago" Visible="true">
									<HeaderStyle Wrap="False" Width="70px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TIPO_TARJETAPOS" HeaderText="Tipo Tarjeta" Visible="true">
									<HeaderStyle Wrap="False" Width="80px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NRO_TARJETA" HeaderText="Nro. Tarjeta/Documento">
									<HeaderStyle Wrap="False" Width="160px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DEPAN_IMPORTE" HeaderText="Monto Pagado">
									<HeaderStyle Wrap="False" Width="86px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ESTADO_ANULACION" HeaderText="Estado Anulacion">
									<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="RESULTADO_PROCESO" HeaderText="Resultado Proceso">
									<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
