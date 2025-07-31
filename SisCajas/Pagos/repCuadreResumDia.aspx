<%@ Page Language="vb" AutoEventWireup="false" Codebehind="repCuadreResumDia.aspx.vb" Inherits="SisCajas.repCuadreResumDia" enableViewState="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Aplicativo SIC</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../estilos/est_General.css" rel="styleSheet" type="text/css">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
	</HEAD>
	<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0">
		<form name="frmPrincipal" id="frmPrincipal" method="post" runat="server">
			<div ID="overDiv" STYLE="Z-INDEX:1; WIDTH:100px; POSITION:absolute"></div>
			<table width="850" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td width="10" valign="top">&nbsp;</td>
					<td width="820" valign="top">
						<table width="820" height="14" border="0" cellspacing="0" cellpadding="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table width="790" border="0" cellspacing="0" cellpadding="0" name="Contenedor" align="center">
							<tr>
								<td align="center">
									<table width="790" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#336699">
										<tr>
											<td align="center">
												<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="Arial10B">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td width="98%" height="32" align="center" class="TituloRConsulta">Cuadre Diario <% if ucase(request.item("tipocuadre")) = "I" then %> Individual <% end if %></td>
														<td valign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table width="790" border="0" align="center" cellpadding="0" cellspacing="0">
													<tr>
														<td width="98%" style="PADDING-RIGHT:5px; PADDING-LEFT:5px; PADDING-BOTTOM:5px; PADDING-TOP:5px">
															<table width="770" border="0" cellspacing="0" cellpadding="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table border="0" width="80%" cellspacing="2" cellpadding="0">
																			<tr>
																				<td width="250">&nbsp;</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Fecha Proceso :</td>
																				<td class="Arial12b" width="200">
																					<asp:TextBox id="txtFecha" runat="server" CssClass="clsInputEnable" Width="65px" MaxLength="10"></asp:TextBox>&nbsp;
																					<a href="javascript:show_calendar('frmPrincipal.txtFecha');" onMouseOut="window.status='';return true;"
																						onMouseOver="window.status='Date Picker';return true;"><img border="0" src="../../images/botones/btn_Calendario.gif"></a>
																				</td>
																			</tr>
																		</table>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<br>
									<table width="360" border="1" align="center" cellpadding="4" cellspacing="0" bordercolor="#336699">
										<tr>
											<td>
												<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="Arial10B">
													<tr>
														<td align="center">&nbsp;
															<asp:Button id="btnBuscar" runat="server" Width="100px" CssClass="BotonOptm" Text="Buscar"></asp:Button>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<br>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="JavaScript" type="text/javascript">
var esNavegador, esIExplorer;
  
esNavegador = (navigator.appName == "Netscape") ? true : false;
esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

function f_Envio()
{
  if (!ValidaFechaA('document.frmPrincipal.txtFecha',false)) return false;
  
  window.frmPrincipal.action="CuadreCaja.aspx"
  window.frmPrincipal.submit();

}

function e_mayuscula(){
	if (event.keyCode>96&&event.keyCode<123)
		event.keyCode=event.keyCode-32;
}

function e_minuscula(){
	if (event.keyCode>64&&event.keyCode<91)
		event.keyCode=event.keyCode+32;
}

if (esIExplorer) {
	//window.document.frmPrincipal.txtPNombre.onkeypress=e_mayuscula;
}
		</script>
	</body>
</HTML>
