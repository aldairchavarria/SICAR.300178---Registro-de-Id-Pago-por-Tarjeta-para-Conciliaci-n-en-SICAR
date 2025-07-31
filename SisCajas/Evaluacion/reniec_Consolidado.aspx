<%@ Page Language="vb" AutoEventWireup="false" Codebehind="reniec_Consolidado.aspx.vb" Inherits="SisCajas.reniec_Consolidado"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Aplicativo CLARO</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../estilos/est_General.css" rel="styleSheet" type="text/css">
		<script language="JavaScript" src="../librerias/Lib_FuncBloqueEvento.js"></script>
		
		<script language=JavaScript>
<!--
	var objImg;
	var intFactorZoom=0.05;
	var intFlagZoom=1;
	var intMaxZoom=550;
	var intPtrFuncZoom;

	function f_reZoom() {
		if (parseInt(objImg.style.width) < intMaxZoom && parseInt(objImg.style.height) < intMaxZoom) {
			objImg.style.width=parseInt(objImg.style.width) + parseInt(objImg.style.width)*intFactorZoom*intFlagZoom;
			objImg.style.height=parseInt(objImg.style.height) + parseInt(objImg.style.height)*intFactorZoom*intFlagZoom;
		}		
	}

	function f_reSize(p_Width, p_Height, p_Img, p_Estado) {
		if (!document.all && !document.getElementById) return
		objImg = eval("document.images." + p_Img);
		intFlagZoom = (p_Estado == "in")? 1 : -1;
		if (objImg.style.width == "" || p_Estado == "restore") {
			objImg.style.width = p_Width;
			objImg.style.height = p_Height;
			if (p_Estado == "restore") return
		}
		else
			f_reZoom();
		intPtrFuncZoom = setInterval("f_reZoom()",100);
	}

	function f_reClearZoom() {
		if (window.intPtrFuncZoom) clearInterval(intPtrFuncZoom)
	}

	function doprint() {
		if (window.print) {
			divMensaje.style.visibility = "VISIBLE";
			divPrintbtn.style.visibility = "HIDDEN";
			window.print();
			divPrintbtn.style.visibility = "VISIBLE";
			divMensaje.style.visibility = "HIDDEN";
		}
		else
			alert('Su browser no acepta la opción de impresión');
	}
//-->
</script>
		
	</HEAD>
	<body class="barra" LANGUAGE="javascript">
		<div id="divPrintbtn" STYLE="LEFT:5px; POSITION:absolute; TOP:7px">
			<input name="btnImprimir" type="button" class="BotonOptm" style="WIDTH:100px" onClick="doprint();"
				value="Imprimir">&nbsp;&nbsp; <input name="btnCerrar" type="button" class="BotonOptm" style="WIDTH:100px" onClick="window.close();"
				value="Cerrar">
		</div>
		<div ID="overDiv" STYLE="Z-INDEX:1; LEFT:5px; WIDTH:550px; POSITION:absolute; TOP:35px">
			<table width="550" bordercolor="#336699" cellspacing="0" cellpadding="0" align="center"
				style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid">
				<tr>
					<td width="98%" height="25" align="center" class="TituloConsulta" valign="middle" bgColor="#d6dbf7"
						style="BORDER-BOTTOM: 1px solid">DATOS DEL CIUDADANO</td>
				</tr>
				<tr>
					<td width="98%" height="25" align="center" class="Arial12b"><font color="red">La 
							Información mostrada no es una consulta al Padrón Electoral</font></td>
				</tr>
			</table>
			<table border="0" width="100" cellpadding="0" cellspacing="0">
				<tr>
					<td><img src="" border="0" width="1" height="4"></td>
				</tr>
			</table>
			<table width="550" bordercolor="#336699" cellspacing="0" cellpadding="0" align="center"
				style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid">
				<tr>
					<td>
						<table width="100%" border="0">
							<tr>
								<td width="40%" valign="middle">
									<table width="100%" border="0" cellspacing="0" cellpadding="0">
										<tbody>
											<tr>
												<td align="center" class="Arial12b">
													<div>
														<a href="#" onmouseover="f_reSize(157,220,'imgFoto','in');" onmouseout="f_reClearZoom();"
															style="TEXT-DECORATION:none">Aumentar</a>&nbsp;|&nbsp;<a href="#" onmouseover="f_reSize(157,220,'imgFoto','restore');" style="TEXT-DECORATION:none">Normal</a>
													</div>
												</td>
											</tr>
											<tr>
												<td>
													<table style="Z-INDEX:10; POSITION:relative" cellSpacing="0" cellPadding="0" align="center"
														border="0">
														<tbody>
															<tr>
																<td colspan="3"><IMG height="10" width="164" src="../../images/bordes/brd_MarcoTop.gif" border="0"></td>
															</tr>
															<tr>
																<td bgColor="#333366">
																	<IMG class="nospace" height="1" width="1" src="../../images/bordes/brd_MarcoBlank.gif"
																		border="0">
																</td>
																<td align="center">
																	<div id="divFoto" style="WIDTH:157px; POSITION:relative; HEIGHT:220px">
																		<div style="Z-INDEX:10; LEFT:0px; POSITION:absolute">
																			<img id="imgFoto" name="imgFoto" src="./Imagen/<%=strArchivoFoto%>" width="157" height="220" align="middle" border="0">
																		</div>
																	</div>
																</td>
																<td bgColor="#333366">
																	<IMG class="nospace" height="1" width="1" src="../../images/bordes/brd_MarcoBlank.gif"
																		border="0">
																</td>
															</tr>
															<tr>
																<td colspan="3"><IMG height="10" width="164" src="../../images/bordes/brd_MarcoBottom.gif" border="0"></td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
											<tr>
												<td><img height="50" src="../../images/bordes/brd_MarcoBlank.gif" width="1" border="0"></td>
											</tr>
											<tr>
												<td align="center" class="Arial12b" style="HEIGHT: 15px">
													<a href="#" onmouseover="f_reSize(224,75,'imgFirma','in');" onmouseout="f_reClearZoom();"
														style="TEXT-DECORATION:none">Aumentar</a>&nbsp;|&nbsp;<a href="#" onmouseover="f_reSize(224,75,'imgFirma','restore');" style="TEXT-DECORATION:none">Normal</a>
												</td>
											</tr>
											<tr>
												<td>
													<table style="POSITION:relative; TOP:0px" cellSpacing="0" cellPadding="0" align="center"
														border="0">
														<tbody>
															<tr>
																<td colspan="3"><IMG height="10" width="228" src="../../images/bordes/brd_MarcoTop.gif" border="0"></td>
															</tr>
															<tr>
																<td bgColor="#333366">
																	<IMG class="nospace" height="1" width="1" src="../../images/bordes/brd_MarcoBlank.gif"
																		border="0">
																</td>
																<td align="center">
																	<div id="divFirma" style="WIDTH:224px; POSITION:relative; HEIGHT:75px">
																		<div style="LEFT: 0px; POSITION: absolute">
																			<img id="imgFirma" name="imgFirma" src="./Imagen/<%=strArchivoFirma%>" width="224" height="75" align="middle" border="0">
																		</div>
																	</div>
																</td>
																<td bgColor="#333366">
																	<IMG class="nospace" height="1" width="1" src="../../images/bordes/brd_MarcoBlank.gif"
																		border="0">
																</td>
															<tr>
																<td colspan="3"><IMG height="10" width="228" src="../../images/bordes/brd_MarcoBottom.gif" border="0"></td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
								</td>
								<td width="57%" valign="top">
									<table id="tblDatos" width="100%" bordercolor="#336699" cellspacing="1" cellpadding="1"
										style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid">
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Número 
												de Documento:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strNumDoc%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Apellido 
												Paterno:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strApePaterno%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Apellido 
												Materno:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strApeMaterno%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Nombres:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strNombres%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Fecha 
												de Nacimiento:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strFechaNac%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Sexo:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strSexo%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Estado 
												Civil:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strEstadoCivil%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Grado 
												de Instrucción:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strGradoInstruccion%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Estatura:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strEstatura%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Departamento 
												de Nacimiento:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strDepartamentoNac%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Provincia 
												de Nacimiento:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strProvinciaNac%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Distrito 
												de Nacimiento:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strDistritoNac%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Restricciones:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strRestricciones%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Fecha 
												de Expedición:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strFechaExpedicion%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Nombre 
												del Padre:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strNombrePadre%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Nombre 
												de la Madre:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strNombreMadre%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Fecha 
												de Inscripción:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strFechaInscripcion%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Departamento 
												de Domicilio:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strDepartamento%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Provincia 
												de Domicilio:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strProvincia%></td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Distrito 
												de Domicilio:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strDistrito%></td>
										</tr>
										<tr>
											<td width="150" valign="top" class="Arial12b" style="BORDER-RIGHT: 1px solid; PADDING-TOP: 3px; BORDER-BOTTOM: 1px solid">Domicilio:</td>
											<td>
												<table bordercolor="#336699" border="0" cellspacing="0" cellpadding="1" width="100%">
													<tr>
														<td width="163" height="30" class="Arial12b" valign="top" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strDireccion%></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td width="150" class="Arial12b" style="BORDER-RIGHT: 1px solid; BORDER-BOTTOM: 1px solid">Constancia 
												de Votación:</td>
											<td width="163" class="Arial12b" style="BORDER-BOTTOM: 1px solid">&nbsp;<%=strConstanciaVotacion%></td>
										</tr>
										<tr>
											<td colspan="2" width="100%" class="Arial12b" style="BORDER-BOTTOM: 1px solid">Consulta 
												sólo para uso interno de :<br>
												AMERICA MOVIL PERU S.A.C</td>
										</tr>
										<tr>
											<td colspan="2" width="100%" class="Arial12b" style="BORDER-BOTTOM: 1px solid">Usuario:<%=Session("strUsuario")%>
												--
												<%=FR_FmtFechaHora(Now(),"DMY")%>
											</td>
										</tr>
										<tr>
											<td colspan="2" width="100%" class="Arial14b" align="center"><b>Hoja Informativa 
													emitida a través de Consultas en Línea Internet, No tiene validez para ningún 
													trámite administrativo, judicial u otros.</b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div>
		<div id="divMensaje" STYLE="LEFT:245px; VISIBILITY:hidden; POSITION:absolute; TOP:138px">
			<img id="imgMensaje" width="298" height="345" src="../../images/fondos/fnd_ProhibidoVta.gif"
				border="0">
		</div>
	</body>
</HTML>
