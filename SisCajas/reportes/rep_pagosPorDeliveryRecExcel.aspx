<%@ Page Language="vb" AutoEventWireup="false" Codebehind="rep_pagosPorDeliveryRecExcel.aspx.vb" Inherits="SisCajas.rep_pagosPorDeliveryRecExcel" contentType="application/vnd.ms-excel" enableViewState="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>rep_pagosPorDeliveryRecExcel</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" runat="server">
			<table width="800">
				<tr>
					<td>
						<asp:datagrid id="dgReporte" runat="server" AutoGenerateColumns="True" BorderColor="Black" CssClass="Arial11B">
							<ItemStyle Font-Size="X-Small" Font-Names="Arial" BackColor="#E9EBEE"></ItemStyle>
							<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center"
								BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B" VerticalAlign="Top"></HeaderStyle>
							
						</asp:datagrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
