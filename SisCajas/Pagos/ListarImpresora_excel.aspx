<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListarImpresora_excel.aspx.vb" Inherits="SisCajas.ListarImpresora_excel" contentType="application/vnd.ms-excel" enableViewState="False" responseEncoding="windows-1250" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>ListarImpresora_excel</title>
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
					<FONT face="Arial" size="2"><STRONG>Detalle Ticketera - Oficina</STRONG></FONT>
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
							<asp:BoundColumn DataField="OFICINA" HeaderText="Oficina"></asp:BoundColumn>
							<asp:BoundColumn DataField="CAJA" HeaderText="Ticketera"></asp:BoundColumn>
							<asp:BoundColumn DataField="DESCRIPCION" HeaderText="Descripción" ></asp:BoundColumn>
							<asp:BoundColumn DataField="SERIE" HeaderText="Serie" ></asp:BoundColumn>
							<asp:BoundColumn DataField="USADO_POR" HeaderText="Creado Por" ></asp:BoundColumn>
						</Columns>
					</asp:datagrid>
				</td>
			</tr>
		</table>
    </form>
  </body>
</html>
