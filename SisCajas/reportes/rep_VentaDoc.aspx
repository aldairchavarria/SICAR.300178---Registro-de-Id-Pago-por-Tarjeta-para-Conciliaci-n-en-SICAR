<%@ Page Language="vb" aspcompat=true AutoEventWireup="false" Codebehind="rep_VentaDoc.aspx.vb" Inherits="SisCajas.rep_VentaDoc"%>

<HTML>
  <HEAD>
		<title>Aplicativo TIM</title>
		<meta http-equiv="Pragma" content="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="Mon, 06 Jan 1990 00:00:01 GMT">
		<link href="../estilos/est_General.css" rel="styleSheet" type="text/css">
			<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
			<script LANGUAGE="JavaScript">
<!--

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
function f_Excel(){
	document.frmPrincipal.action = "toExcel.aspx";
	document.frmPrincipal.submit();
};

//-->
			</script>
			<meta http-equiv="pragma" content="no-cache">
  </HEAD>
	<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0">
		<form name="frmPrincipal" id="frmPrincipal" method="post">
			<input type=hidden name="strFecha" value="<%=strFecha%>"> <input type="hidden" name="tipo" value="1">
			<table width="975" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<!--
		<td width="170" valign="top"></td>
		<td width="10" valign="top">&nbsp;</td>
	-->
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
														<td width="98%" height="32" align="center" class="TituloRConsulta">Cuadre de Caja - 
															Ventas Facturadas</td>
														<td valign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table width="790" border="0" align="center" cellpadding="0" cellspacing="0">
													<tr>
														<td width="98%">
															<table width="770" border="0" cellspacing="0" cellpadding="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td height="18">
																		<table border="0" cellspacing="1" cellpadding="0">
																			<tr class="Arial12br">
																				<td width="250">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Datos 
																						Generales</b></td>
																			</tr>
																		</table>
																	</td>
																</tr>
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table border="0" width="100%" cellspacing="2" cellpadding="0">
																			<tr>
																				<td width="30">&nbsp;</td>
																				<td width="166" class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha :</td>
																				<td class="Arial12b" width="170"><input name="txtNombres" class="clsInputDisable"  id="txtNombres4" value="<%=sFechaActual%>" size="30" maxlength="15"></td>
																				<td width="30">&nbsp;</td>
																				<td width="120" class="Arial12b">&nbsp;&nbsp;&nbsp;Hora :</td>
																				<td class="Arial12b" width="220"><input name="txtNombres" class="clsInputDisable"  id="txtNombres4" value="<%=sHoraActual%>" size="30" maxlength="15" readonly></td>
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
									<table width="790" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#336699">
										<tr>
											<td align="center">
												<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="Arial10B">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td width="98%" height="32" align="center" class="TituloRConsulta">Datos</td>
														<td valign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table width="790" border="0" align="center" cellpadding="0" cellspacing="0">
													<tr>
														<td width="98%">
															<table width="770" border="0" cellspacing="0" cellpadding="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td height="18">
																		<div style="BORDER-RIGHT:1px; BORDER-TOP:1px; OVERFLOW-Y:scroll; Z-INDEX:1; OVERFLOW-X:scroll; BORDER-LEFT:1px; WIDTH:770px; BORDER-BOTTOM:1px; POSITION:relative; HEIGHT:300px; TEXT-ALIGN:center"
																			class="frame2">
																			<table class="tabla_interna_borde1" width="3500" cellspacing="1" cellpadding="1">
																				<tr height="21" class="Arial12B">
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Tipo 
																						Documento</td>
												<% If bMostrar then %>																						
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Fact. SAP</td>
												<% else %>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Nro. de Pedido</td>
												<% End if %>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Doc. SUNAT</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Vendedor</td>
																					<td width="2%" height="22" align="center" class="tabla_interna_borde2">Moneda</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Clase 
																						Factura</td>
																					<% 
												If sTipoTienda = "MT" then
												%>
																					<td width="2%" height="22" align="center" class="tabla_interna_borde2">Cuotas</td>
																					<%
												End If
												%>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Total Doc.</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Efectivo</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">American 
																						Exp.</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">NetCard</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Cheque</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Cob. 
																						Interbank</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Electron</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Diners</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Maestro</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">MasterCard</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Ripley</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">CMR</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Visa</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Visa 
																						Internet</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Carsa</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Curacao</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Visa WEB</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Maestro WEB</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">ACE Home 
																						Center</td>
																					<%
												If sTipoTienda = "MT" then
												%>
													<% If bMostrar then %>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">3 Cuotas</td>
													<% else %>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">1 Cuota</td>
													<% End If%>																					
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">6 Cuotas</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">12 Cuotas</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">18 Cuotas</td>
													<% If Not bMostrar then %>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">24 Cuotas</td>
													<% End If %>									
																					<%
												End If
												%>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Moneda No 
																						Definida</td>
																					<%
												If sTipoTienda = "MT" then
												%>
																					<!--Campaña Empleado-->
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Cuotas Empl. 
																						Claro</td>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Cuotas Empl. 
																						Overall</td>
																					<!--Campaña Empleado-->
																					<%
												End If
												%>
																					<td width="3%" height="22" align="center" class="tabla_interna_borde2">Saldo</td>
																				</tr>
																				<tr height="1">
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="2%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<%
												If sTipoTienda = "MT" then
												%>
																					<td width="2%" class="tabla_interna_borde2"></td>
																					<%
												End If
												%>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<%
												If sTipoTienda = "MT" then
												%>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<%
												End If
												%>
																					<td width="3%" class="tabla_interna_borde2"></td>
																					<td width="3%" class="tabla_interna_borde2"></td>
																				</tr>
																				<%=auxprint%>
																			</table>
																		</div>
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
									<table width="<%=sAncho%>" border="1" align="center" cellpadding="4" cellspacing="0" bordercolor="#336699">
										<tr>
											<td>
												<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="Arial10B">
													<tr>
														<td align="center">
															<%
								If Session("STRMessage") = "" then
								%>
															<input name="btnBuscar" type="button" class="BotonOptm" style="WIDTH:100px" onClick="javascript:f_Exportar();"
																value="Exportar Excel">&nbsp;&nbsp;
															<%
								End If
								%>
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
			<script language="JavaScript" type="text/javascript">
var esNavegador, esIExplorer;

esNavegador = (navigator.appName == "Netscape") ? true : false;
esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

if (esIExplorer) {
}

			</script>
		</form>
		<script language="JavaScript">
	function f_Exportar()
	{
		//document.frmTmp.action = './toExcel.aspx?tipo=1&Individual=0&strFecha=<%=strFecha%>';
		document.frmTmp.action = 'rep_VentaDocExcel.aspx?strFecha=<%=strFecha%>';
		document.frmTmp.submit();
	}

		</script>
		<form name="frmTmp" method="post" action="" target="_blank">
		</form>
	</body>
</HTML>
