<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AsignaTurno.aspx.vb" Inherits="SisCajas.AsignaTurno" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>America Móvil</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 136px; POSITION: absolute; TOP: 32px" borderColor="#336699"
				cellSpacing="0" cellPadding="4" width="475" align="center" border="1">
				<tr>
					<td align="center"><asp:label class="TituloRConsulta" id="lblTitActualizaBIN" runat="server">Asignación de Turnos</asp:label></td>
				</tr>
			</TABLE>
			<table id="Table2" style="Z-INDEX:102;LEFT:128px;POSITION:absolute;TOP:88px" cellSpacing="2"
				cellPadding="2" width="400" border="0">
				<tr class="Arial12b">
					<td style="WIDTH:50px" align="right"></td>
					<td width="200" align="center">&nbsp;Turnos:</td>
					<td width="75" align="center">&nbsp;Hora Inicio</td>
					<td width="75" align="center">&nbsp;Hora Fin</td>
				</tr>
				<tr>
					<td class="Arial12b" style="WIDTH:50px" align="right">&nbsp;</td>
					<td class="Arial12b" width="200" align="center"><asp:dropdownlist class="Arial11b" id="SelTurnos" runat="server" Width="125px" AutoPostBack="True"></asp:dropdownlist></td>
					<td width="75" align="center"><input class="clsInputEnable" id="txtHoraIni" style="WIDTH: 75px" type="text" runat="server"
							NAME="txtHoraIni"></td>
					<td width="75" align="center"><input class="clsInputEnable" id="txtHoraFin" style="WIDTH:75px" type="text" runat="server"
							NAME="txtHoraFin"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
