<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ErrorIngresoUrl.aspx.vb" Inherits="SisCajas.ErrorIngresoUrl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ErrorIngresoUrl</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="665" align="center" border="0">
				<tr>
					<td>
						<table cellSpacing="0" style="Z-INDEX: 101; LEFT: 176px; POSITION: absolute; TOP: 9px"
							cellPadding="0" width="665" align="center" border="0">
							<tr>
								<td colSpan="3" height="94">&nbsp;</td>
							</tr>
							<tr>
								<td vAlign="top" align="center" width="25" bgColor="#ed1c24">
									<IMG height="25" src="images/sq_top01.gif" width="25">
								</td>
								<td vAlign="top" align="center" width="621" bgColor="#ed1c24">&nbsp;</td>
								<td vAlign="top" align="center" width="26" bgColor="#ed1c24">
									<IMG height="25" src="images/sq_top02.gif" width="26">
								</td>
							</tr>
						</table>
						<table style="Z-INDEX: 104; LEFT: 176px; POSITION: absolute; TOP: 448px" cellSpacing="0"
							cellPadding="0" width="665" align="center" border="0">
							<tr bgColor="#c2e8f5">
								<td vAlign="top" width="25">
									<IMG height="23" src="images/sq_pie01.gif" width="25">
								</td>
								<td vAlign="top" bgColor="#c2e8f5">&nbsp;</td>
								<td vAlign="top" align="right" width="26">
									<IMG height="23" src="images/sq_pie02.gif" width="26">
								</td>
							</tr>
						</table>
						<table style="Z-INDEX: 103; LEFT: 176px; POSITION: absolute; TOP: 128px" cellSpacing="0"
							cellPadding="0" width="665" align="center" border="0">
							<tr bgColor="#ed1c24">
								<td vAlign="top" width="25" height="90">&nbsp;</td>
								<td vAlign="top" width="129">
									<IMG height="78" src="images/graf_ico_error.gif" width="128">
								</td>
								<td style="PADDING-RIGHT: 18px" vAlign="middle" align="right" width="485">&nbsp;</td>
								<td vAlign="top" width="26">&nbsp;</td>
							</tr>
						</table>
						<table style="Z-INDEX: 102; LEFT: 176px; POSITION: absolute; TOP: 216px" cellSpacing="0"
							cellPadding="0" width="665" align="center" border="0">
							<tr bgColor="#c2e8f5">
								<td vAlign="top" width="25" bgColor="#c2e8f5" height="235">&nbsp;</td>
								<td vAlign="bottom" width="257" bgColor="#c2e8f5">
									<IMG height="231" src="images/img_marca.jpg" width="222">
								</td>
								<td style="PADDING-BOTTOM: 8px" vAlign="bottom" align="center" width="357" bgColor="#c2e8f5">
									<table height="204" cellSpacing="0" cellPadding="0" width="99%" border="0">
										<tr>
											<td colSpan="2" height="134">
												<strong>
													<font face="Arial" color="#ff0000" size="3">Esta intentando acceder por una 
														ruta incorrecta, por favor ingresar por el 
														<br><a href='<%= ConfigurationSettings.AppSettings("ConstPathPortal") %>'>
														portal de aplicaciones</a> o por el link 
														<a href='<%= ConfigurationSettings.AppSettings("ConstPathAplicacion") %>'><%= ConfigurationSettings.AppSettings("ConstPathAplicacion") %></a><br>
													</font>
												</strong>
											</td>
										</tr>
										<tr>
											<td width="200" height="25">&nbsp;</td>
											<td align="left">
												<A href="http://eclaro"><IMG height="30" src="images/btn_regresar_a.gif" width="110" border="0"></A>
											</td>
										</tr>
									</table>
								</td>
								<td vAlign="top" width="26">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
