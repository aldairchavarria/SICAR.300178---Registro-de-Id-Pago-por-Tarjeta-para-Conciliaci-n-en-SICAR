<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RecargaVirt.aspx.vb" Inherits="SisCajas.RecargaVirt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EnvioBuzon</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript" src="../librerias/ubigeo.js"></script>
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<!-- PROY-31850 - INICIO-->
		<script language="javascript">
		
		function f_ConsultaI(){
					frmPrincipal.btnConsultaI.click();	
			}
	    
		</script>
		<!-- PROY-31850 - FIN-->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<input id="hldVerif" type="hidden" name="hldVerif" runat="server">
			<!-- PROY-31850 - INICIO--><input id="hdlMsj" type="hidden" name="hldMsj" runat="server"><!-- PROY-31850 - FIN-->
			<asp:Button id="btnGrabar" runat="server" Text="Grabar" Visible="False"></asp:Button>
			<div id="overDiv" style="Z-INDEX: 101; WIDTH: 100px; POSITION: absolute">
			</div>
			<table cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td style="WIDTH: 832px" vAlign="top" width="832">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" name="Contenedor">
							<tr>
								<td align="center" height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1" name="Contenedor">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10" height="32"></td>
											<td class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="top" align="center" width="98%"
												height="32">Recarga Virtual</td>
											<td vAlign="top" width="10" height="32"></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="14"></td>
											<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
												width="98%">
												<table cellSpacing="0" cellPadding="0" width="770" border="0">
													<tr>
														<td height="4"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="770" border="0">
													<TR>
														<TD class="Arial12b" style="WIDTH: 20px; HEIGHT: 28px" width="20"></TD>
														<TD class="Arial12b" style="WIDTH: 153px; HEIGHT: 28px" width="153">Tipo de Doc. 
															Identidad :</TD>
														<TD class="Arial12b" style="WIDTH: 84px; HEIGHT: 28px" width="84">
															<P><asp:dropdownlist id="cboTipDocumento" runat="server" CssClass="clsInputDisable" Enabled="False"></asp:dropdownlist></P>
														</TD>
														<TD class="Arial12b" style="WIDTH: 138px; HEIGHT: 28px" width="138">&nbsp;Nro. de 
															Doc. Identidad :</TD>
														<TD class="Arial12b" style="HEIGHT: 28px" width="250"><asp:textbox id="txtNumDocumento" runat="server" CssClass="clsInputDisable" MaxLength="15" Enabled="False"></asp:textbox></TD>
													</TR>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
							<tr>
								<td height="10">
									<TABLE id="Table1" borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
										border="1" name="Contenedor">
										<TR>
											<TD align="center">
												<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<TR>
														<TD vAlign="top" width="10" height="32"></TD>
														<TD class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="top" align="center" width="98%"
															height="32">Datos de la Venta</TD>
														<TD vAlign="top" width="10" height="32"></TD>
													</TR>
												</TABLE>
												<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<TR>
														<TD vAlign="top" width="14"></TD>
														<TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
															width="98%">
															<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="770" border="0">
																<TR>
																	<TD height="4"></TD>
																</TR>
															</TABLE>
															<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="770" border="0">
																<TR>
																	<TD class="Arial12b" style="WIDTH: 20px" width="20"></TD>
																	<TD class="Arial12b" style="WIDTH: 153px" width="153">Tipo de Doc. Venta (*):</TD>
																	<TD class="Arial12b" style="WIDTH: 84px" width="84">
																		<div class="dropcont" id="divNat"><asp:dropdownlist id="cboClasePedido" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist></div>
																		<div class="dropcont" id="divJur"><asp:dropdownlist id="cboClasePedido1" runat="server" CssClass="clsInputDisable" Enabled="False" Width="152px"></asp:dropdownlist></div>
																	</TD>
																	<TD class="Arial12b" style="WIDTH: 138px" width="138">Fecha de Venta (*):</TD>
																	<TD class="Arial12b" width="250"><asp:textbox id="txtFechaPrecioVenta" runat="server" CssClass="clsInputEnable" MaxLength="10"></asp:textbox>&nbsp;
																		<A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																			href="javascript:show_calendar('frmPrincipal.txtFechaPrecioVenta');"><IMG src="../../images/botones/btn_Calendario.gif" border="0"></A></TD>
																</TR>
																<TR>
																	<TD class="Arial12b" style="WIDTH: 20px; HEIGHT: 17px" width="20"></TD>
																	<TD class="Arial12b" style="WIDTH: 153px; HEIGHT: 17px" width="153">Vendedor:</TD>
																	<!-- PROY-31850 - INICIO-->
																	<TD class="Arial12b" style="WIDTH: 382px" width="382" colSpan="3"><asp:dropdownlist id="cboSelectVend" runat="server" CssClass="clsSelectEnable3" Width="480px"></asp:dropdownlist></TD>
																	<!-- PROY-31850 - FIN-->
																</TR>
																<TR>
																	<!-- PROY-31850 - INICIO-->
																	<TD style="WIDTH: 20px" class="Arial12b" width="20"></TD>
																	<TD style="WIDTH: 153px" class="Arial12b" width="153">&nbsp;Nro. de Teléfono:</TD>
																	<TD class="Arial12b" width="250"><asp:textbox id="txtNTelf" runat="server" CssClass="clsInputEnable" MaxLength="15" Width="150"></asp:textbox></TD>
																	<TD style="WIDTH: 138px" class="Arial12b" width="138">Importe a Recargar</TD>
																	<TD style="WIDTH: 84px" class="Arial12b" width="84"><asp:dropdownlist id="cboImporte" runat="server" CssClass="clsSelectEnable" Width="235"></asp:dropdownlist></TD>
																	<!-- PROY-31850 - FIN-->
																</TR>
															</TABLE>
														</TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
							<TR>
								<TD height="10"></TD>
							</TR>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="400" align="center"
							border="1">
							<TR>
								<TD align="center">
									<TABLE cellSpacing="2" cellPadding="0" border="0">
										<TR>
											<TD align="center" width="28"></TD>
											<TD align="center" width="85"><input type="button" class="BotonOptm" id="btnGrabarRec" name="btnGrabarRec" runat="server"
													value="Grabar" onclick="f_Valida();f_ValidaEjecucion();" onserverclick="btnGrabar_Click"></TD>
											<TD align="center" width="28"></TD>
											<!--PROY-31850 - INI -->
											<TD>
												<p style="Z-INDEX: 0; DISPLAY: none"><asp:button id="btnConsultaI" runat="server" Text="Button"></asp:button></p>
											</TD>
											<!--PROY-31850 - FIN-->
										</TR>
									</TABLE>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			</TD></TR></TABLE></form>
		<script language="javascript" type="text/javascript">

//document.frmPrincipal.txtMonto.onkeypress = e_numero;

// Deshabilitar Boton de Envio para evitar multiples clicks. 
var c=0;

function f_ValidaEjecucion(){ 
document.getElementById('btnGrabarRec').disabled=true; 
c=c+1; 
msg = 'Espere Por Favor...('+ c +')!';
document.getElementById('btnGrabarRec').value= msg;
var t=setTimeout('f_ValidaEjecucion()',1000); // Mostrar Contador Hasta que finalize el Submit 
}


function textCounter(obj) {
	var maximo;
	switch (parseInt(document.frmPrincipal.cboTipDocumento.options[document.frmPrincipal.cboTipDocumento.selectedIndex].value)) {
		case 1 :
			maximo = 8;
			break;
		case 2 :
			maximo = 10;
			break;
		case 3 :
			maximo = 10;
			break;
		
		case 4 : maximo = <%=configurationsettings.appsettings("gIntCarneExtranjeriaMax")%>;	break;
		
		case 6 :
			maximo = 11;
			break;
		case 7 :
			maximo = 10;
			break;
		default :
			maximo = 15;
			break;
	}
	if (obj.value.length > maximo) // if too long...trim it!
		obj.value = obj.value.substring(0, maximo); // otherwise, update 'characters left' counter
}

function f_ValidarDI(){
	if (!ValidaCombo('document.frmPrincipal.cboTipDocumento','el campo tipo de Doc. de Identidad ',false)) return false;
	switch (parseInt(document.frmPrincipal.cboTipDocumento.options[document.frmPrincipal.cboTipDocumento.selectedIndex].value)) {
		case 1 :
			if (!ValidaDNI('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
			break;
		case 2 :
			if (!ValidaFP('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
			break;
		case 3 :
			if (!ValidaFA('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
			break;
		case 4 :
			if (!ValidaEXTR('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
			break;
		case 6 :
			if (!ValidaRUC('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
			break;
		case 7 :
			if (!ValidaPAS('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
			break;
		default :
			if (!ValidaAlfanumerico('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
			break;
	}
	return true;
}      

function f_Valida()
{
  event.returnValue = false;
  if (!f_ValidarDI()) return false;
  if (!ValidaCombo('document.frmPrincipal.cboSelectVend','el campo Vendedor ',false)) return false;
  if (!ValidaFechaA('document.frmPrincipal.txtFechaPrecioVenta',false)) return false;
  if (!ValidaAlfanumerico('document.frmPrincipal.txtNTelf','el campo Telefono ',false)) return false;  
  event.returnValue = true;
  return true;
}


/*function f_Valida()
{
	validationOK = !f_ValidarDI() || !ValidaCombo('document.frmPrincipal.cboSelectVend','el campo Vendedor ',false) || !ValidaFechaA('document.frmPrincipal.txtFechaPrecioVenta',false) || !ValidaAlfanumerico('document.frmPrincipal.txtNTelf','el campo Telefono ',false);
	//event.returnValue = !validationOK;
	if(!validationOK){
		var btn = document.getElementById('btnGrabar');
		btn.click();
		f_ValidaEjecucion();
	}
	return false;
}
*/

function f_CambiaTipoDocVenta() {
		var cadena,i;
		cadena = document.frmPrincipal.cboTipDocumento.value;


		if (cadena!="6") {
			document.getElementById("divNat").style.display = "none";
			document.getElementById("divJur").style.display = "block";
		}
		else {
			document.getElementById("divNat").style.display = "block";
			document.getElementById("divJur").style.display = "none";
		}

	}

function e_numero(){
	if ( !( (event.keyCode>=48) && (event.keyCode<=57)) && !(event.keyCode==13) && !(event.keyCode==46) )
		event.keyCode=0;
}

f_CambiaTipoDocVenta();

		</script>
	</body>
</HTML>
