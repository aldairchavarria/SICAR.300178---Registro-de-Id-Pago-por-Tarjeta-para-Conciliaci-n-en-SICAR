<%@ Page Language="vb" AutoEventWireup="false" Codebehind="rep_ventaDetalladaExcel.aspx.vb" Inherits="SisCajas.rep_ventaDetalladaExcel" contentType="application/vnd.ms-excel" enableViewState="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>rep_ventaDetalladaExcel</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" runat="server">
			<table width="800">
				<tr>
					<td width="2"><FONT face="Arial" size="2"><STRONG>REPORTE :</STRONG></FONT>
					</td>
					<td width="2"><STRONG><FONT face="Arial" size="2">CIERRE&nbsp;DE&nbsp;CAJA</FONT> </STRONG>
					</td>
					<td width="2"><FONT face="Arial" size="2"><STRONG>VENTA&nbsp;DETALLADA
								<% if len(trim(request.item("Individual"))) > 0 %>
								(Individual)<%end if %>
							</STRONG></FONT>
					</td>
					<td></td>
				</tr>
				<tr>
					<td style="WIDTH: 2px"><FONT face="Arial" size="2"><STRONG>TIENDA :</STRONG></FONT>
					</td>
					<td><FONT face="Arial" size="2"><STRONG>'<%=Session("ALMACEN")%></STRONG></FONT></td>
					<td><FONT face="Arial" size="2"><%=Session("OFICINA")%></STRONG></FONT></td>
					<td></td>
				</tr>
				<tr>
					<td style="WIDTH: 2px"><FONT face="Arial" size="2"><STRONG>FECHA :</STRONG></FONT>
					</td>
					<td><FONT face="Arial" size="2"><%= format(now.day,"00") & "/" & format(now.month,"00") & "/" & now.year %></STRONG></FONT></td>
					<td><FONT face="Arial" size="2"><STRONG>HORA :</STRONG></FONT>
					</td>
					<td><FONT face="Arial" size="2"><STRONG><%=format(now.hour,"00") & ":" & format(now.minute,"00") & ":" & format(now.second,"00") %></STRONG></FONT></td>
				</tr>
			</table>
			<table width="800">
				<tr>
					<td>
						<asp:datagrid id="dgReporte" runat="server" AutoGenerateColumns="False">
							<ItemStyle Font-Size="X-Small" Font-Names="Arial"></ItemStyle>
							<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="DSCOM" HeaderText="Tipo Documento"></asp:BoundColumn>
								<asp:BoundColumn DataField="VBELN" HeaderText="Fact. SAP" DataFormatString="&amp;nbsp;{0}"></asp:BoundColumn>
								<asp:BoundColumn DataField="XBLNR" HeaderText="Doc. SUNAT"></asp:BoundColumn>
								<asp:BoundColumn DataField="KUNNR" HeaderText="Vendedor"></asp:BoundColumn>
								<asp:BoundColumn DataField="MATNR" HeaderText="Material"></asp:BoundColumn>
								<asp:BoundColumn DataField="ARKTX" HeaderText="Descripcion"></asp:BoundColumn>
								<asp:BoundColumn DataField="MEINS" HeaderText="UMB"></asp:BoundColumn>
								<asp:BoundColumn DataField="FKIMG" HeaderText="Cantidad"></asp:BoundColumn>
								<asp:BoundColumn DataField="NETWR" HeaderText="Valor Venta"></asp:BoundColumn>
								<asp:BoundColumn DataField="SERNR" HeaderText="Numero Serie" DataFormatString="&amp;nbsp;{0}"></asp:BoundColumn>
								<asp:BoundColumn DataField="VKAUS" HeaderText="Utilizaci&#243;n"></asp:BoundColumn>
								<asp:BoundColumn DataField="BEZEI" HeaderText="Descripcion"></asp:BoundColumn>
							</Columns>
						</asp:datagrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
