<%@ Page Language="vb" AutoEventWireup="false" Codebehind="repAutorizaciones.aspx.vb" Inherits="SisCajas.repAutorizaciones"%>
<HTML>
	<HEAD>
		<title>Aplicativo TIM</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta http-equiv="Pragma" content="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="Mon, 06 Jan 1990 00:00:01 GMT">
		<link href="../estilos/est_General.css" rel="styleSheet" type="text/css">
			<script language="JavaScript" src="../librerias/date-picker.js"></script>
			<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
			<meta http-equiv="pragma" content="no-cache">
	</HEAD>
	<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0">
		<form name="frmPrincipal" id="frmPrincipal" method="post" runat="server">
			<div ID="overDiv" STYLE="Z-INDEX:1; WIDTH:100px; POSITION:absolute"></div>
			<table width="800" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td width="800" valign="top">
						<table width="800" height="14" border="0" cellspacing="0" cellpadding="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table width="750" border="0" cellspacing="0" cellpadding="0" name="Contenedor" align="center">
							<tr>
								<td align="center">
									<table width="750" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#336699">
										<tr>
											<td align="center">
												<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="Arial10B">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td width="98%" height="32" align="center" class="TituloRConsulta">
															Reporte de Autorizacion de Anulaciones 
													        <% if request.item("Adm") <> "" then %> &nbsp;(Administrador)
													        <% else %> &nbsp;(Supervisor)
													        <% end if%>		
															</td>
														<td valign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table width="700" border="0" align="center" cellpadding="0" cellspacing="0">
													<tr>
														<td width="98%">
															<table width="700" border="0" cellspacing="0" cellpadding="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table border="0" width="80%" cellspacing="2" cellpadding="0">
																			<tr>
																				<td width="250">&nbsp;</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Fecha&nbsp;Inicio :</td>
																				<td class="Arial12b" width="200">
																					<input name="txtFechaIni" type="text" class="clsInputEnable" id="txtFechaIni" tabindex="34" value="<%=Now.Date.ToString("d")%>"
																						size="10" maxlength="10"> <a href="javascript:show_calendar('frmPrincipal.txtFechaIni');" onMouseOut="window.status='';return true;"
																						onMouseOver="window.status='Date Picker';return true;"><img border="0" src="../../images/botones/btn_Calendario.gif"></a>
																				</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Fecha&nbsp;Fin :</td>
																				<td class="Arial12b" width="200">
																					<input name="txtFechaFin" type="text" class="clsInputEnable" id="txtFechaFin" tabindex="34" value="<%=Now.Date.ToString("d")%>"
																						size="10" maxlength="10"> <a href="javascript:show_calendar('frmPrincipal.txtFechaFin');" onMouseOut="window.status='';return true;"
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
														<td align="center">
															<asp:button id="btnReporte" runat="server" Width="60px" CssClass="BotonOptm" Text="Reporte"></asp:button>
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
		<form name="frmExp" id="frmExp" method="post" target="_blank">
		</form>
		<script language="JavaScript" type="text/javascript">
var esNavegador, esIExplorer;
  
esNavegador = (navigator.appName == "Netscape") ? true : false;
esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

function e_mayuscula(){
	if (event.keyCode>96&&event.keyCode<123)
		event.keyCode=event.keyCode-32;
}

function e_minuscula(){
	if (event.keyCode>64&&event.keyCode<91)
		event.keyCode=event.keyCode+32;
}

function f_Reporte()
{
   if(ValidaFechaA('document.frmPrincipal.txtFechaIni', false) && ValidaFechaA('document.frmPrincipal.txtFechaFin', false))
   {  
   		strFechaIni = document.frmPrincipal.txtFechaIni.value 
		strFechaFin = document.frmPrincipal.txtFechaFin.value 
		   
		document.frmExp.action="repAutorizacionesExcel.aspx?FechaIni=" + strFechaIni + "&FechaFin=" + strFechaFin + "&Adm=<%=request.Item("Adm")%>"
		document.frmExp.submit();
   }
   else
     event.returnValue = false;
}

if (esIExplorer) {
	//window.document.frmPrincipal.txtPNombre.onkeypress=e_mayuscula;
}
		</script>
	</body>
</HTML>
