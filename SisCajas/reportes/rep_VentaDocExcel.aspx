<%@ Page Language="vb" AutoEventWireup="false" Codebehind="rep_VentaDocExcel.aspx.vb" Inherits="SisCajas.rep_VentaDocExcel" contentType="application/vnd.ms-excel" enableViewState="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>rep_VentaDocExcel</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" runat="server">
			<table width="800">
				<tr>
					<td width="2"><FONT face="Arial" size="2"><STRONG>REPORTE :</STRONG></FONT>
					</td>
					<td width="2"><STRONG><FONT face="Arial" size="2">CIERRE&nbsp;DE&nbsp;CAJA</FONT> </STRONG>
					</td>
					<td width="2"><FONT face="Arial" size="2"><STRONG>VENTA&nbsp;POR&nbsp;TIPO&nbsp;DE&nbsp;DOCUMENTO
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
			<input type="hidden" name="nroCell" value="36">
			<table width="800">
				<tr>
					<td><asp:datagrid id="dgReporte" runat="server" AutoGenerateColumns="False">
							<ItemStyle Font-Size="X-Small" Font-Names="Arial"></ItemStyle>
							<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="DSCOM" HeaderText="Tipo Documento"></asp:BoundColumn>
								<asp:BoundColumn DataField="VBELN" HeaderText="Nro. de Pedido" DataFormatString="&amp;nbsp;{0}"></asp:BoundColumn>
								<asp:BoundColumn DataField="XBLNR" HeaderText="Doc. SUNAT"></asp:BoundColumn>
								<asp:BoundColumn DataField="KUNNR" HeaderText="Vendedor"></asp:BoundColumn>
								<asp:BoundColumn DataField="WAERK" HeaderText="Moneda"></asp:BoundColumn>
								<asp:BoundColumn DataField="FKART" HeaderText="Clase Factura"></asp:BoundColumn>
								<asp:BoundColumn DataField="CUOTA" HeaderText="Cuotas"></asp:BoundColumn>
								<asp:BoundColumn DataField="TOTFA" HeaderText="Total Doc."></asp:BoundColumn>
								<asp:BoundColumn DataField="ZEFE" HeaderText="Efectivo"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZAEX" HeaderText="American Exp."></asp:BoundColumn>
								<asp:BoundColumn DataField="ZCAR" HeaderText="NetCard"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZCHQ" HeaderText="Cheque"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZCIB" HeaderText="Cob. Interbank"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZDEL" HeaderText="Electron"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZDIN" HeaderText="Diners"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZDMT" HeaderText="Maestro"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZMCD" HeaderText="MasterCard"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZRIP" HeaderText="Ripley"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZSAG" HeaderText="CMR"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZVIS" HeaderText="Visa"></asp:BoundColumn>
								<asp:BoundColumn DataField="VIIN" HeaderText="Visa Internet"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZCRS" HeaderText="Carsa"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZCZO" HeaderText="Curacao"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZVW1" HeaderText="Visa WEB"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZVW2" HeaderText="Maestro WEB"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZACE" HeaderText="ACE Home Center"></asp:BoundColumn>
								<asp:BoundColumn DataField="CUO1" HeaderText="1 Cuota"></asp:BoundColumn>
								<asp:BoundColumn DataField="CUO3" HeaderText="3 Cuotas"></asp:BoundColumn>
								<asp:BoundColumn DataField="CUO6" HeaderText="6 Cuotas"></asp:BoundColumn>
								<asp:BoundColumn DataField="CUO12" HeaderText="12 Cuotas"></asp:BoundColumn>
								<asp:BoundColumn DataField="CUO18" HeaderText="18 Cuotas"></asp:BoundColumn>
								<asp:BoundColumn DataField="CUO24" HeaderText="24 Cuotas"></asp:BoundColumn>
								<asp:BoundColumn DataField="NODEF" HeaderText="Moneda No Definida"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZEAM" HeaderText="Cuotas Empl. Claro"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZEOV" HeaderText="Cuotas Empl. Overall"></asp:BoundColumn>
								<asp:BoundColumn DataField="SALDO" HeaderText="Saldo"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
