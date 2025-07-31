<%@ Page Language="vb" aspcompat=true AutoEventWireup="false" Codebehind="rep_ventaDetalladaInd.aspx.vb" Inherits="SisCajas.rep_ventaDetalladaInd"%>
<HTML>
	<HEAD>
		<title>Aplicativo TIM</title>
		<meta http-equiv="Pragma" content="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="Mon, 06 Jan 1990 00:00:01 GMT">
		<link href="../estilos/est_General.css" rel="styleSheet" type="text/css">
			<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
			<script LANGUAGE="JavaScript">
<!--

function f_Excel(){
	document.frmPrincipal.action = "ExcelCaja.asp";
	document.frmPrincipal.submit();
};
//-->
			</script>
			<meta http-equiv="pragma" content="no-cache">
	</HEAD>
	<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0">
		<form name="frmPrincipal" id="frmPrincipal" method="post" TARGET="_blank">
			<input type=hidden name="strFecha" value="<%=strFecha%>"> <input type="hidden" name="tipo" value="2">
			<table width="975" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<!--
					<td width="11" valign="top"></td>
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
														<td width="98%" height="32" align="center" class="TituloRConsulta">
															Cuadre de Caja -&nbsp;Ventas Detallada Individual</td>
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
																				<td class="Arial12b" width="170"><input name="txtNombres" type="text"  class="clsInputDisable"  id="txtNombres4" value="<%=sFechaActual%>" size="30" maxlength="15"></td>
																				<td width="30">&nbsp;</td>
																				<td width="120" class="Arial12b">&nbsp;&nbsp;&nbsp;Hora :</td>
																				<td class="Arial12b" width="220"><input name="txtNombres" type="text"  class="clsInputDisable"  id="txtNombres4" value="<%=sHoraActual%>" size="30" maxlength="15" readonly></td>
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
																		<div style="BORDER-RIGHT:1px; BORDER-TOP:1px; OVERFLOW-Y:scroll; Z-INDEX:1; OVERFLOW-X:scroll; BORDER-LEFT:1px; WIDTH:770px; BORDER-BOTTOM:1px; POSITION:relative; HEIGHT:310px; TEXT-ALIGN:center"
																			class="frame2">
																			<table class="tabla_interna_borde1" width="1800" cellspacing="1" cellpadding="1">
																				<TR class="Arial12B" height="21">
																					<TD align="center" width="5%" class="tabla_interna_borde2" height="22">Tipo 
																						Documento</TD>
																					<TD align="center" width="8%" class="tabla_interna_borde2" height="22">Fact. SAP</TD>
																					<TD align="center" width="8%" class="tabla_interna_borde2" height="22">Doc. SUNAT</TD>
																					<TD align="center" width="5%" class="tabla_interna_borde2" height="22">Vendedor</TD>
																					<TD align="center" width="5%" class="tabla_interna_borde2" height="22">Material</TD>
																					<TD align="center" width="10%" class="tabla_interna_borde2" height="22">Descripción</TD>
																					<TD align="center" width="3%" class="tabla_interna_borde2" height="22">UMB</TD>
																					<TD align="center" width="4%" class="tabla_interna_borde2" height="22">Cantidad</TD>
																					<TD align="center" width="6%" class="tabla_interna_borde2" height="22">Valor Venta</TD>
																					<TD align="center" width="8%" class="tabla_interna_borde2" height="22">Número Serie</TD>
																					<TD align="center" width="3%" class="tabla_interna_borde2" height="22">Utilización</TD>
																					<TD align="center" width="3%" class="tabla_interna_borde2" height="22">Descripción</TD>
																				</TR>
																				<TR height="1">
																					<TD width="5%" class="tabla_interna_borde2"></TD>
																					<TD width="8%" class="tabla_interna_borde2"></TD>
																					<TD width="8%" class="tabla_interna_borde2"></TD>
																					<TD width="5%" class="tabla_interna_borde2"></TD>
																					<TD width="5%" class="tabla_interna_borde2"></TD>
																					<TD width="10%" class="tabla_interna_borde2"></TD>
																					<TD width="3%" class="tabla_interna_borde2"></TD>
																					<TD width="4%" class="tabla_interna_borde2"></TD>
																					<TD width="6%" class="tabla_interna_borde2"></TD>
																					<TD width="8%" class="tabla_interna_borde2"></TD>
																					<TD width="3%" class="tabla_interna_borde2"></TD>
																					<TD width="3%" class="tabla_interna_borde2"></TD>
																				</TR>
																				<%=strcadenaprint%>
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
									<table width="360" border="1" align="center" cellpadding="4" cellspacing="0" bordercolor="#336699">
										<tr>
											<td>
												<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="Arial10B">
													<tr>
														<td align="center">
															<input name="btnBuscar" type="button" class="BotonOptm" style="WIDTH:100px" onClick="javascript:f_Exportar();"
																value="Exportar Excel">&nbsp;&nbsp;
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
		//document.frmTmp.action = '<%=strRuta%>/reportes/toExcel.aspx?tipo=2&Individual=1&strFecha=<%=strFecha%>';
		document.frmTmp.action = 'rep_ventaDetalladaExcel.aspx?Individual=1&strFecha=<%=strFecha%>';
		document.frmTmp.submit();
	}

		</script>
		<form name="frmTmp" method="post" action="" target="_blank">
		</form>
	</body>
</HTML>
