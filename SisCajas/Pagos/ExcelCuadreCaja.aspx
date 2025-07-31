<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ExcelCuadreCaja.aspx.vb" Inherits="SisCajas.ExcelCuadreCaja" contentType="application/vnd.ms-excel" responseEncoding="windows-1250" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ExcelCuadreCaja</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" runat="server">
			<table style="WIDTH: 424px" border="1">
				<tr>
					<td><b>REPORTE</b></td>
					<td><b>CIERRE DE CAJA</b></td>
					<td><b>CUADRE DE CAJA</b></td>
				</tr>
				<tr>
					<td><b>TIENDA</b></td>
					<td id="tdCodTienda" runat="server"></td>
					<td id="tdDesTienda" runat="server"></td>
				</tr>
				<tr>
					<td colspan="3"></td>
				</tr>
				<tr>
					<td colspan="3">
						<asp:DataGrid id="dgCuadre" runat="server" AutoGenerateColumns="False" Width="100%">
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="CONTADOR" HeaderText="Orden">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DESC_CONCEPTO" HeaderText="Descripci&#243;n"></asp:BoundColumn>
								<asp:BoundColumn DataField="MONTO" HeaderText="Monto">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
