<%@ Page Language="vb" AutoEventWireup="false" Codebehind="repRemesasExcel.aspx.vb" Inherits="SisCajas.repRemesasExcel" enableViewState="False" contentType="application/vnd.ms-excel"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>repRemesasExcel</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" runat="server">
			<table>
				<tr>
					<td align="center"><b>REPORTE DE ENVIOS DE REMESA</b>
					</td>
				</tr>
				<tr>
					<td align="center"><b>DEL
							<%=Request.Item("strFecIni")%>
							AL
							<%=Request.Item("strFecFin")%>
						</b>
					</td>
				</tr>
				<tr>
					<td align="center">&nbsp;</B>
					</td>
				</tr>
				<tr>
					<td>
						<asp:DataGrid id="dgRemesas" runat="server" AutoGenerateColumns="False">
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="REMES_OFICINA" HeaderText="OFICINA"></asp:BoundColumn>
								<asp:BoundColumn DataField="REMES_FECHA" HeaderText="FECHA" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="REMES_NOMUSUARIO" HeaderText="USUARIO"></asp:BoundColumn>
								<asp:BoundColumn DataField="REMES_BOLSA" HeaderText="BOLSA"></asp:BoundColumn>
								<asp:BoundColumn DataField="REMES_MONTEFSOL" HeaderText="EFECTIVO SOLES"></asp:BoundColumn>
								<asp:BoundColumn DataField="REMES_MONTEFDOL" HeaderText="EFECTIVO DOLARES"></asp:BoundColumn>
								<asp:BoundColumn DataField="REMES_MONTCHSOL" HeaderText="CHEQUES SOLES"></asp:BoundColumn>
								<asp:BoundColumn DataField="REMES_MONTCHDOL" HeaderText="CHEQUES DOLARES"></asp:BoundColumn>
								<asp:BoundColumn DataField="REMES_TIPCAM" HeaderText="TIPO DE CAMBIO"></asp:BoundColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
