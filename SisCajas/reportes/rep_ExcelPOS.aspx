<%@ Page Language="vb" AutoEventWireup="false" Codebehind="rep_ExcelPOS.aspx.vb" Inherits="SisCajas.rep_ExcelPOS" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>rep_ExcelPOS</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style>
		</style>
	</HEAD>
	<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0">
		<form id="Form1" method="post" runat="server">
			<table width="1000">
				<tr>
					<td style=" TEXT-ALIGN: center; FONT-SIZE: 20px; FONT-WEIGHT: bold" colSpan="4">REPORTE 
						DE CAJAS - PAGOS CON TARJETA DE CREDITO Y/O DEBITO</td>
				<tr>
					<td><asp:datagrid id="dgTransacciones" runat="server" AutoGenerateColumns="False" CssClass="Arial11B"
							Width="100%">
							<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
							<ItemStyle BackColor="#EEEEEE"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999"
								CssClass="Arial12B"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="codPdv" HeaderText="PUNTO VENTA">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="usuavCajero" HeaderText="CAJERO">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="posvIdestablec" HeaderText="CODIGO COMERCIO">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="trnsvNumPedido" HeaderText="NUMERO PEDIDO">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="trnsvIdRef" HeaderText="REF ID">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="trnsvTipoTrans" HeaderText="TIPO TRANSACCION">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="trnsvEstado" HeaderText="ESTADO">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="trnscOperacion" HeaderText="OPERACION">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="trnsvTipoTarjetaPos" HeaderText="TIPO TARJETA">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="trnsvNroTarjeta" HeaderText="NUMERO TARJETA">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="trnsnMonto" HeaderText="MONTO" DataFormatString="{0:0.00}">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="fechaTransaccionPos" HeaderText="FECHA TRANSACCION">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="horaTransaccionPos" HeaderText="HORA TRANSACCION">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
