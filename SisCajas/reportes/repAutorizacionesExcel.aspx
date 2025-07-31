<%@ Page Language="vb" AutoEventWireup="false" Codebehind="repAutorizacionesExcel.aspx.vb" Inherits="SisCajas.repAutorizacionesExcel"  enableViewState="False" contentType="application/vnd.ms-excel"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>repAutorizacionesExcel</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table>
				<tr>
					<td align="center"><B>REPORTE DE AUTORIZACION DE ANULACIONES</B></td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<tr>
					<td>
						<asp:DataGrid id="dgReporte" style="Z-INDEX: 101; LEFT: 272px; TOP: 248px" runat="server" AutoGenerateColumns="False">
							<Columns>
								<asp:BoundColumn DataField="cod_pdv" HeaderText="Punto de Venta"></asp:BoundColumn>
								<asp:BoundColumn DataField="autran_nomcajero" HeaderText="Cajero"></asp:BoundColumn>
								<asp:BoundColumn DataField="autran_asesor" HeaderText="Asesor"></asp:BoundColumn>
								<asp:BoundColumn DataField="autran_fecha" HeaderText="Fecha"></asp:BoundColumn>
								<asp:BoundColumn DataField="motiv_desmotivo" HeaderText="Motivo"></asp:BoundColumn>
								<asp:BoundColumn DataField="autran_otromot" HeaderText="Especificacion"></asp:BoundColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
