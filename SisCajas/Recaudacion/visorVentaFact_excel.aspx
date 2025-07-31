<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorVentaFact_excel.aspx.vb" Inherits="SisCajas.visorVentaFact_excel" contentType="application/vnd.ms-excel" enableViewState="False" responseEncoding="windows-1250"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>visorVentaFact_excel</title>
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
					<FONT face="Arial" size="2"><STRONG>Facturación Detallada</STRONG></FONT>
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
							<asp:BoundColumn DataField="DESC_OFICINA" HeaderText="Oficina de Venta"></asp:BoundColumn>
							<asp:BoundColumn DataField="SUB_OFICINA_DESC" HeaderText="Sub Oficina"></asp:BoundColumn>
							<asp:BoundColumn DataField="CAJERO" HeaderText="Cod. Cajero"></asp:BoundColumn>
							<asp:BoundColumn DataField="NOM_CAJERO" HeaderText="Cajero"></asp:BoundColumn>
							<asp:BoundColumn DataField="FECHA" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
							<asp:BoundColumn DataField="TIPO_DOCUMENTO" HeaderText="Tipo Documento"></asp:BoundColumn>
							<asp:BoundColumn DataField="DESC_DOCUMENTO" HeaderText="Documento"></asp:BoundColumn>
							<asp:BoundColumn DataField="FACTURA_FICTICIA" HeaderText="Nro. de Pedido"></asp:BoundColumn>
							<asp:BoundColumn DataField="REFERENCIA" HeaderText="Doc. SUNAT"></asp:BoundColumn>
							<asp:BoundColumn DataField="COD_VENDEDOR" HeaderText="cod. Vendedor"></asp:BoundColumn>
							<asp:BoundColumn DataField="VENDEDOR" HeaderText="Vendedor"></asp:BoundColumn>
							<asp:BoundColumn DataField="MONEDA" HeaderText="Moneda"></asp:BoundColumn>
							<asp:BoundColumn DataField="CLASE_FACTURA_COD" HeaderText="Clase Factura"></asp:BoundColumn>
							<asp:BoundColumn DataField="CUOTA" HeaderText="Cuotas"></asp:BoundColumn>
							<asp:BoundColumn DataField="TOTFA" HeaderText="Total Doc."></asp:BoundColumn>
							<asp:BoundColumn DataField="ZEFE" HeaderText="Efectivo"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZAEX" HeaderText="American Exp."></asp:BoundColumn>
							<asp:BoundColumn DataField="ZCAR" HeaderText="NetCard"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZCHQ" HeaderText="Cheque"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZCIB" HeaderText="Cob. Interbank"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZDEL" HeaderText="Electron"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZDIN" HeaderText="Dinners"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZDMT" HeaderText="Maestro"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZMCD" HeaderText="MasterCard"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZRIP" HeaderText="Ripley"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZSAG" HeaderText="CMR"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZVIS" HeaderText="Visa"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZCRS" HeaderText="Carsa"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZCZO" HeaderText="Curacao"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZACE" HeaderText="ACE Home Center"></asp:BoundColumn>
							<asp:BoundColumn DataField="TDPP" HeaderText="Trans. deuda post"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZNCR" HeaderText="Nota credito"></asp:BoundColumn>
							<asp:BoundColumn DataField="ZEAM" HeaderText="Cuotas Empl. Claro"></asp:BoundColumn>
							<asp:BoundColumn DataField="SALDO" HeaderText="Saldo"></asp:BoundColumn>
							<asp:BoundColumn DataField="CUO1" HeaderText="1 Cuota"></asp:BoundColumn>
							<asp:BoundColumn DataField="CUO6" HeaderText="6 Cuotas"></asp:BoundColumn>
							<asp:BoundColumn DataField="CUO12" HeaderText="12 Cuotas"></asp:BoundColumn>
							<asp:BoundColumn DataField="CUO18" HeaderText="18 Cuotas"></asp:BoundColumn>
							<asp:BoundColumn DataField="CUO24" HeaderText="24 Cuotas"></asp:BoundColumn>
							<asp:BoundColumn DataField="DES_ESTADO" HeaderText="Estado"></asp:BoundColumn>
						</Columns>
					</asp:datagrid>
				</td>
			</tr>
		</table>
    </form>
  </body>
</html>
