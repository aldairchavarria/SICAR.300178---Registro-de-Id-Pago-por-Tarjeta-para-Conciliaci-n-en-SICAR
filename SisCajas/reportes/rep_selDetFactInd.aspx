<%@ Page Language="vb" AutoEventWireup="false" Codebehind="rep_selDetFactInd.aspx.vb" Inherits="SisCajas.rep_selDetFactInd"%>
<HTML>
	<HEAD>
		<title>Aplicativo TIM</title>
		<meta http-equiv="Pragma" content="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="Mon, 06 Jan 1990 00:00:01 GMT">
		<link href="../estilos/est_General.css" rel="styleSheet" type="text/css">
			<script language="JavaScript" src="../librerias/date-picker.js"></script>
			<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
			<script LANGUAGE="JavaScript">
<!--

function popup(url) {
  msg= window.open(url,"popi","toolbar=no,left=58,top=50,width=680,height=480,directories=no,status=no,scrollbars=yes,resize=no,menubar=no");
}



function f_Aprobar(){
	var chk = f_Validar();
		if(chk == true){
			document.frmPrincipal.codOperacion.value = "07";
			document.frmPrincipal.action = 'rep_RVentFact.aspx';
			document.frmPrincipal.submit();
		}
};

function f_Validar(){
	return true;	
}

//-->
			</script>
			<meta http-equiv="pragma" content="no-cache">
	</HEAD>
	<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0" onClick="<%=strSiteOcultarCapas%>">
		<form name="frmPrincipal" id="frmPrincipal" method="post">
			<div ID="overDiv" STYLE="Z-INDEX:1; WIDTH:100px; POSITION:absolute"></div>
			<input type="hidden" id="codOperacion" name="codOperacion"> <input name="txtCierre" type="hidden" id="txtCierre">
			<table width="800" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<!--
		<td width="170" valign="top"></td>
		<td width="10" valign="top">&nbsp;</td>
	-->
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
															Ventas Detallada x Cajero</td>
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
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Fecha Proceso :</td>
																				<td class="Arial12b" width="200">
																					<input name="txtFecha" type="text" class="clsInputEnable" id="txtFecha" tabindex=34  value="<%=strFecha%>" size="10" maxlength="10">
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
														<td align="center"><input name="btnBuscar" type="button" class="BotonOptm" style="WIDTH:100px" onClick="javascript:f_Aprobar();"
																value="Buscar">&nbsp;&nbsp;</td>
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
