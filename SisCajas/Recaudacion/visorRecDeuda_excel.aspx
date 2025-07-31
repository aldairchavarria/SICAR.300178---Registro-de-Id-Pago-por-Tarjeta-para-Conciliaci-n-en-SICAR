<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorRecDeuda_excel.aspx.vb" Inherits="SisCajas.visorRecDeuda_excel" contentType="application/vnd.ms-excel" enableViewState="False" responseEncoding="windows-1250" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>visorRecDeuda_excel</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
	<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="frmPrincipal" method="post" runat="server">
    <table width="800">
		<tr>
			<td>
				<FONT face="Arial" size="2"><STRONG>Detalle Recaudaciones - Deuda</STRONG></FONT>
			</td>
		</tr>
		<tr>
			<td></td>
		</tr>
	</table>
		<table width="800">
				<tr>
					<td>
						<asp:datagrid id="dgReporte" runat="server" AutoGenerateColumns="False">
							<ItemStyle Font-Size="X-Small" Font-Names="Arial"></ItemStyle>
							<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="NRO_TRANSACCION" HeaderText="Nro. Transacción"></asp:BoundColumn>
								<asp:BoundColumn DataField="NOM_DEUDOR" HeaderText="Nombre de deudor"></asp:BoundColumn>
								<asp:BoundColumn DataField="RUC_DEUDOR" HeaderText="Ruc Deudor"></asp:BoundColumn>
								<asp:BoundColumn DataField="NOM_OF_VENTA" HeaderText="Oficina Venta"></asp:BoundColumn>
								<asp:BoundColumn DataField="SUB_OFICINA_DESC" HeaderText="Sub Oficina"></asp:BoundColumn>
								<asp:BoundColumn DataField="FECHA_TRANSAC" HeaderText="Fecha Trans." DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
								<asp:BoundColumn DataField="HORA_TRANSAC" HeaderText="Hora Trans." DataFormatString="{0:HH:mm:ss}"></asp:BoundColumn>
								<asp:BoundColumn DataField="MONEDA" HeaderText="Moneda"></asp:BoundColumn>
								<asp:BoundColumn DataField="IMPORTE_PAGO" HeaderText="Importe Pago" DataFormatString="{0:N2}"></asp:BoundColumn>
								<asp:BoundColumn DataField="ESTADO" HeaderText="Estado"></asp:BoundColumn>
								<asp:BoundColumn DataField="NRO_TELEFONO" HeaderText="Nro. Teléfono"></asp:BoundColumn>
								<asp:BoundColumn DataField="COD_CAJERO" HeaderText="Cod. Cajero"></asp:BoundColumn>
								<asp:BoundColumn DataField="NOM_CAJERO" HeaderText="Cajero"></asp:BoundColumn>
								<asp:BoundColumn DataField="TIPO_DOC_DEUDOR" HeaderText="Tipo Doc. Deudor"></asp:BoundColumn>
								<asp:BoundColumn DataField="NRO_DOC_DEUDOR" HeaderText="Nro Doc Deudor"></asp:BoundColumn>
							</Columns>
						</asp:datagrid>
					</td>
				</tr>
		</table>
    </form>
  </body>
</html>
