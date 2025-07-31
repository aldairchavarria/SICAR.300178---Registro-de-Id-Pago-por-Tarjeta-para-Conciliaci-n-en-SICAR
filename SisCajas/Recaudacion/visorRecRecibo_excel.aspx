<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorRecRecibo_excel.aspx.vb" Inherits="SisCajas.visorRecRecibo_excel" contentType="application/vnd.ms-excel" enableViewState="False" responseEncoding="windows-1250" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>visorRecRecibo_excel</title>
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
					<FONT face="Arial" size="2"><STRONG>Detalle Recaudaciones - Recibo</STRONG></FONT>
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
							<asp:BoundColumn DataField="TIPO_DOC_RECAUD" HeaderText="Tipo Doc Recaud"></asp:BoundColumn>
							<asp:BoundColumn DataField="NRO_DOC_RECAUD" HeaderText="Nro Doc Recaud"></asp:BoundColumn>
							<asp:BoundColumn DataField="MONEDA" HeaderText="Moneda"></asp:BoundColumn>
							<asp:BoundColumn DataField="IMPORTE_RECIBO" HeaderText="Importe Recibo" DataFormatString="{0:N2}"></asp:BoundColumn>
							<asp:BoundColumn DataField="IMPORTE_PAGADO" HeaderText="Importe Pagado" DataFormatString="{0:N2}"></asp:BoundColumn>
							<asp:BoundColumn DataField="NRO_COBRANZA" HeaderText="Nro. Cobranza"></asp:BoundColumn>
							<asp:BoundColumn DataField="NRO_OPE_ACREE" HeaderText="Nro. Acreedor"></asp:BoundColumn>
							<asp:BoundColumn DataField="FECHA_EMISION" HeaderText="Fecha Emisión" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
							<asp:BoundColumn DataField="FECHA_PAGO" HeaderText="Fecha Pago" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
							<asp:BoundColumn DataField="NRO_DOC_DEUDOR" HeaderText="Nro Doc Deudor"></asp:BoundColumn>
							<asp:BoundColumn DataField="NRO_TRACE_PAGO" HeaderText="Trace pago"></asp:BoundColumn>
							<asp:BoundColumn DataField="NRO_TRACE_ANUL" HeaderText="Trace anulación"></asp:BoundColumn>
							<asp:BoundColumn DataField="DESC_SERVICIO" HeaderText="Desc. Servicio"></asp:BoundColumn>
						</Columns>
					</asp:datagrid>
				</td>
			</tr>
		</table>
    </form>
  </body>
</html>
