<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorRecPago_excel.aspx.vb" Inherits="SisCajas.visorRecPago_excel" contentType="application/vnd.ms-excel" enableViewState="False" responseEncoding="windows-1250" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>visorRecPago_excel</title>
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
				<FONT face="Arial" size="2"><STRONG>Detalle Recaudaciones - Pago</STRONG></FONT>
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
								<asp:BoundColumn DataField="DESC_VIA_PAGO" HeaderText="Via Pago"></asp:BoundColumn>
								<asp:BoundColumn DataField="IMPORTE_PAGADO" HeaderText="Importe Pago" DataFormatString="{0:N2}"></asp:BoundColumn>
								<asp:BoundColumn DataField="NRO_CHEQUE" HeaderText="Nro. Cheque / Tarjeta"></asp:BoundColumn>
								<asp:BoundColumn DataField="DOC_CONTABLE" HeaderText="Doc. Contable"></asp:BoundColumn>
								<asp:BoundColumn DataField="USUARIO_FI" HeaderText="Usuario FI"></asp:BoundColumn>
								<asp:BoundColumn DataField="FECHA_FI" HeaderText="Fecha Envio"></asp:BoundColumn>
								<asp:BoundColumn DataField="ESTADO_CONT" HeaderText="Estado"></asp:BoundColumn>
							</Columns>
						</asp:datagrid>
					</td>
				</tr>
		</table>
    </form>
  </body>
</html>
