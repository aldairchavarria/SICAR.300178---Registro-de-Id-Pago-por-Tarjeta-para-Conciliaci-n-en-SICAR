<%@ Page Language="vb" AutoEventWireup="false" Codebehind="rep_Recaudacion.aspx.vb" Inherits="SisCajas.rep_Recaudacion"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>rep_Recaudacion</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </HEAD>
  <body MS_POSITIONING="GridLayout">

<form id="frnRecaudacion" method="post" runat="server">
			<table width="1000">
				<tr>
					<td style=" TEXT-ALIGN: center; FONT-SIZE: 20px; FONT-WEIGHT: bold" >REPORTE 
						DE RECAUDACIONES PROCESADAS</td>
				<tr>
					<td>
					<asp:datagrid id="dgRecaudacion" runat="server" AutoGenerateColumns="False" CssClass="Arial11B"
							Width="100%">
							<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
							<ItemStyle BackColor="#EEEEEE"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999"
								CssClass="Arial12B"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="NRO_TRANSACCION" HeaderText="NRO DOCUMENTO">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NOM_DEUDOR" HeaderText="NOMBRE DE CLIENTE">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="RUC_DEUDOR" HeaderText="DOC. DE IDENTIDAD">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="IMPORTE_PAGO" HeaderText="IMPORTE PAGO" DataFormatString={0:0.00}>
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FECHA_TRANSAC" HeaderText="FECHA TRANSACCION">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HORA_TRANSAC" HeaderText="HORA DE TRANSACCION">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ESTADO_TRANSAC" HeaderText="ESTADO">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="OFICINA_VENTA" HeaderText="OFICINA DE VENTA" >
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" ></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TIPOPAGO" HeaderText="TIPO DE PAGO" >
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>								
								<asp:BoundColumn DataField="ESTADO_SICAR" HeaderText="ESTADO SICAR">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid>
					</td>
				</tr>
			</table>
		</form>
  </body>
</HTML>
