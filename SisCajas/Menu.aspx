<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Menu.aspx.vb" Inherits="SisCajas.Menu"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Menu</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>

		<form id="Form1" method="post" runat="server">
		<!--#include file=menu1.htm -->
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="200" border="1" bgColor="#ff0000">
				<TR>
					<TD><a href=webForms/PoolPagos.aspx><font color="#ffffff" style="FONT-WEIGHT: bold; FONT-SIZE: 11px; FONT-STYLE: italic; FONT-FAMILY: Verdana">Pagos</font></a></TD>
				</TR>
				<TR>
					<TD><a href=webForms/poolConsultaPagos.aspx><font color="#ffffff" style="FONT-WEIGHT: bold; FONT-SIZE: 11px; FONT-STYLE: italic; FONT-FAMILY: Verdana">Consulta de Pagos</font></a></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
