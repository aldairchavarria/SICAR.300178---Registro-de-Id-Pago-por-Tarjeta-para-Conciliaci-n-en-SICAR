<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DatMotLlamada.aspx.vb" Inherits="SisCajas.DatMotLlamada"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DatMotLlamada</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="Arial12b" id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 0px"
				cellSpacing="0" cellPadding="1" width="400" border="1" borderColor="#999999">
				<TR>
					<TD>Fecha:</TD>
					<TD>Motivo</TD>
					<TD>Detalle</TD>
					<TD>...</TD>
				</TR>
				<TR>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
			</TABLE>
			<TABLE class="Arial12b" id="Table1" style="Z-INDEX: 101; LEFT:8px; POSITION: absolute; TOP:66px"
				cellSpacing="0" cellPadding="0" width="400" border="0" borderColor="#999999">
				<TR>
					<TD valign="top">
						<asp:TextBox class="Arial10b" id="TextBox1" runat="server" Width="300px" Height="33px"></asp:TextBox></TD>
					<TD valign="top">
						<asp:Button id="cmdClteAntiguo" runat="server" Text="Grabar" CssClass="Botonoptm" Width="80px"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
