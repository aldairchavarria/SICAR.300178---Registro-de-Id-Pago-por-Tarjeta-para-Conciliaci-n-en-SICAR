<%@ Register TagPrefix="uc1" TagName="ctr_Menu" Src="ctr_Menu.ascx" %>
<%@ Page Language="vb" aspcompat="true" AutoEventWireup="false" Codebehind="Inicial.aspx.vb" Inherits="SisCajas.Inicial"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>Inicial</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
  </HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" runat="server">
			<table style="POSITION: absolute; TOP: 2px; LEFT: 2px" border="0" width="100%" height="100%">
				<tr>
					<td style="WIDTH: 161px" vAlign="top" width="161"><uc1:ctr_menu id="Ctr_Menu1" runat="server"></uc1:ctr_menu></td>
					
					<td width="*"><iframe id="dataFrame" class="iFramInicio" height="100%" src="about:blank" frameBorder="no"
							width="100%" scrolling="yes"> </iframe>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
