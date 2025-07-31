<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorRemesa_excel.aspx.vb" Inherits="SisCajas.visorRemesa_excel" contentType="application/vnd.ms-excel" enableViewState="False" responseEncoding="windows-1250" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>visorRemesa_excel</title>
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
				<FONT face="Arial" size="2"><STRONG>Detalle Remesas</STRONG></FONT>
			</td>
		</tr>
		<tr>
			<td></td>
		</tr>
	</table>
	<table width="800">
		<tr>
			<td>    <!-- INI-936 - CSNO - Agregada columna "COMPROBANTE" y se movio al final de la tabla-->
				<asp:datagrid id="dgReporte" runat="server" AutoGenerateColumns="False">
					<ItemStyle Font-Size="X-Small" Font-Names="Arial"></ItemStyle>
					<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="BOLSA" HeaderText="Nro. Bolsa"></asp:BoundColumn>
						<asp:BoundColumn DataField="SOBRE" HeaderText="Nro. Sobre"></asp:BoundColumn>
						<asp:BoundColumn DataField="FECHA_ENVIO_REMESA" HeaderText="Fecha Remesa" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
						<asp:BoundColumn DataField="HORA_ENVIO_REMESA" HeaderText="Hora Remesa" DataFormatString="{0:HH:mm:ss}"></asp:BoundColumn>
						<asp:BoundColumn DataField="BUZON_FECHA" HeaderText="Fecha Buzón" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
						<asp:BoundColumn DataField="BUZON_HORA" HeaderText="Hora Buzón" DataFormatString="{0:HH:mm:ss}"></asp:BoundColumn>
						<asp:BoundColumn DataField="COD_USUARIO_ENVIA_REMESA" HeaderText="Código Usuario"></asp:BoundColumn>
						<asp:BoundColumn DataField="USUARIO_ENVIA_REMESA" HeaderText="Usuario Remesa"></asp:BoundColumn>
						<asp:BoundColumn DataField="MONTO" HeaderText="Importe"></asp:BoundColumn>
						<asp:BoundColumn DataField="COD_TIPO" HeaderText="Tipo Remesa"></asp:BoundColumn>
						<asp:BoundColumn DataField="TIPO" HeaderText="Descripción remesa"></asp:BoundColumn>
						<asp:BoundColumn DataField="OFICINA" HeaderText="Oficina de Venta"></asp:BoundColumn>
						<asp:BoundColumn DataField="DOCUMENTO" HeaderText="Doc. Contable"></asp:BoundColumn>
						<asp:BoundColumn DataField="USUARIO_CONTABILIZA" HeaderText="Usuario FI"></asp:BoundColumn>
						<asp:BoundColumn DataField="FECHA_CONTABILIZA" HeaderText="Fecha FI"></asp:BoundColumn>
						<asp:BoundColumn DataField="HORA_CONTABILIZA" HeaderText="Hora FI"></asp:BoundColumn>
						<asp:BoundColumn DataField="ESTADO_CONT" HeaderText="Estado"></asp:BoundColumn>
						<asp:BoundColumn DataField="COMPROBANTE" HeaderText="Comp. Servicio"></asp:BoundColumn>
					</Columns>
				</asp:datagrid>
			</td>
		</tr>
	</table>
    </form>
  </body>
</html>
