<%@ Page Language="vb" AutoEventWireup="false" Codebehind="repElimBuzonExcel.aspx.vb" Inherits="SisCajas.repElimBuzonExcel" contentType="application/vnd.ms-excel" enableViewState="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>repElimBuzonExcel</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" runat="server">
			<table>
				<tr>
					<td align="center"><B>ANULACIONES DE ENVIO A CAJA BUZON</B>
					</td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<tr>
					<td><asp:datagrid id="dgElimin" runat="server" AutoGenerateColumns="False">
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="BUZEL_OFICINA" HeaderText="OFICINA"></asp:BoundColumn>
								<asp:BoundColumn DataField="BUZEL_FECHAELM" HeaderText="FECHA ELIMINACION"></asp:BoundColumn>
								<asp:BoundColumn DataField="BUZEL_NOMUSUELM" HeaderText="USUARIO ELIMINADOR"></asp:BoundColumn>
								<asp:BoundColumn DataField="BUZEL_BOLSA" HeaderText="BOLSA"></asp:BoundColumn>
								<asp:BoundColumn DataField="BUZEL_TIPOVIA" HeaderText="TIPO VIA"></asp:BoundColumn>
								<asp:BoundColumn DataField="BUZEL_MONTO" HeaderText="IMPORTE"></asp:BoundColumn>
								<asp:BoundColumn DataField="BUZEL_TIPCAM" HeaderText="TIPO DE CAMBIO"></asp:BoundColumn>
								<asp:BoundColumn DataField="BUZEL_NOMUSUARIO" HeaderText="USUARIO CREADOR"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
