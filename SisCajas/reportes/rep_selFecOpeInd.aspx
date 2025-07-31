<%@ Page Language="vb" AutoEventWireup="false" Codebehind="rep_selFecOpeInd.aspx.vb" Inherits="SisCajas.rep_selFecOpeInd" %>
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

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_showHideLayers() { //v6.0
  var i,p,v,obj,args=MM_showHideLayers.arguments;
  for (i=0; i<(args.length-2); i+=3) if ((obj=MM_findObj(args[i]))!=null) { v=args[i+2];
    if (obj.style) { obj=obj.style; v=(v=='show')?'visible':(v=='hide')?'hidden':v; }
    obj.visibility=v; }
}

function f_Aprobar(){
	var chk = f_Validar();
		if(chk == true){			
			//alert(frmPrincipal.txtFecha.value.substr(3,2));
			//alert('rep_RVentFact.aspx?pfecha='+ frmPrincipal.txtFecha.value.substr(6,4)+frmPrincipal.txtFecha.value.substr(3,2)+frmPrincipal.txtFecha.value.substr(0,2));
			//document.frmPrincipal.codOperacion.value = "08";
			//document.frmPrincipal.action = 'rep_OperaDetallada.aspx?pfecha='+ frmPrincipal.txtFecha.value.substr(6,4)+frmPrincipal.txtFecha.value.substr(3,2)+frmPrincipal.txtFecha.value.substr(0,2);
		    //document.frmPrincipal.submit();
		    // ='rep_RVentFact.aspx?pfecha='+ frmPrincipal.txtFecha.value.substr(6,4)+frmPrincipal.txtFecha.value.substr(3,2)+frmPrincipal.txtFecha.value.substr(0,2);
		    
		    window.location='rep_OperaDetInd.aspx?pfecha='+ frmPrincipal.txtFecha.value.substr(6,4)+frmPrincipal.txtFecha.value.substr(3,2)+frmPrincipal.txtFecha.value.substr(0,2)+"&pusuario="+frmPrincipal.txtUsuario.value;
		}
};

function f_Validar(){
	return true;	
}

//-->
			</script>
			<meta http-equiv="pragma" content="no-cache">
	</HEAD>
	<body topmargin="0" leftmargin="0" onClick="<%=strSiteOcultarCapas%>">
		<form name="frmPrincipal" id="frmPrincipal" method="post" runat="server">
			<div ID="overDiv" STYLE="Z-INDEX:1; WIDTH:100px; POSITION:absolute"></div>
			<input type="hidden" id="codOperacion" name="codOperacion"> <input name="txtCierre" type="hidden" id="txtCierre">
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
														<td width="98%" height="32" align="center" class="TituloRConsulta">Detalle de 
															Operaciones Procesadas - Individual
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
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Fecha Operación:</td>
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
			<INPUT type="hidden" id="txtUsuario" runat="server">
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
