<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ReporteClienteB.aspx.vb" Inherits="SisCajas.ReporteClienteB"%>
<HTML>
	<HEAD>
		<title>Reporte Cliete Business</title>
		<meta http-equiv="Pragma" content="no-cache">
		<META http-equiv="Expires" content="Mon, 06 Jan 1990 00:00:01 GMT">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
			<script language="JavaScript" src="../librerias/date-picker.js"></script>
			<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
			<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
			<meta http-equiv="pragma" content="no-cache">
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<div id="overDiv" style="Z-INDEX: 1; WIDTH: 100px; POSITION: absolute"></div>
			<input id="codOperacion" type="hidden" name="codOperacion"> <input id="txtCierre" type="hidden" name="txtCierre">
			<table cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<td vAlign="top" width="800">
						<table height="14" cellSpacing="0" cellPadding="0" width="800" border="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="750" align="center" border="0" name="Contenedor">
							<tr>
								<td align="center">
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="750" align="center"
										border="1">
										<tr>
											<td align="center">
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td class="TituloRConsulta" align="center" width="98%" height="32">Reporte de 
															Clientes Corporativos</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="700" align="center" border="0">
													<tr>
														<td width="98%">
															<table cellSpacing="0" cellPadding="0" width="700" border="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table cellSpacing="2" cellPadding="0" width="70%" border="0">
																			<tr>
																				<td width="350">&nbsp;</td>
																				<td class="Arial12b" align="right" width="200">&nbsp;&nbsp;&nbsp;Número de 
																					RUC:&nbsp;&nbsp;</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;<input class="clsInputEnable" id="txtNumero" tabIndex="34" type="text" maxLength="11" size="11"
																						name="txtNumero" runat="server" style="WIDTH: 102px; HEIGHT: 17px"> &nbsp;&nbsp;
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
									<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="360" align="center"
										border="1">
										<tr>
											<td>
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td align="center">
															<asp:Button id="cmdBuscar" runat="server" Text="Buscar" CssClass="BotonOptm" Width="57px"></asp:Button>&nbsp;&nbsp;</td>
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
	if (esIExplorer) {
		//window.document.frmPrincipal.txtPNombre.onkeypress=e_mayuscula;
	}
	
	function f_Valida()
	{
	  if (!ValidaRUC('document.frmPrincipal.txtNumero','el campo número de Doc. de Identidad ',false)) 
	  {
	   event.returnValue = false;
	  }  
	}
		</script>
	</body>
</HTML>
