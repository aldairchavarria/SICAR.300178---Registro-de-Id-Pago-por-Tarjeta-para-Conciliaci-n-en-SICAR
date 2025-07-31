<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EnvioBuzon.aspx.vb" Inherits="SisCajas.EnvioBuzon" %>
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
		<script language="javascript">
		
		window.onload = window_onload;	
		
		function window_onload() {		
			if (frmPrincipal.txtCajero.value!=""){
			    alert('Se ingresó el monto en la caja buzon');
				f_Imprimir();
				frmPrincipal.action="EnvioBuzon.aspx"
				frmPrincipal.txtCajero.value=""			
				frmPrincipal.txtImporte.value=""
				frmPrincipal.txtNroBolsa.value=""			
			}			
		}
		
		function f_Imprimir(){
	
			var strCajero = frmPrincipal.txtCajero.value;			
			var strImporte = frmPrincipal.txtImporte.value;
			var strBolsa = frmPrincipal.txtNroBolsa.value;
			var strFecha = frmPrincipal.txtFecha.value; //INICIATIVA-565
						
			var objIframe = document.getElementById("IfrmImpresion");
			
			objIframe.style.visibility = "visible";
			objIframe.style.width = 0;
			objIframe.style.height = 0;
			
			objIframe.src = "FormatoBuzon.aspx?strCajero="+ strCajero + "&strMonto=" + strImporte + "&strBolsa=" + strBolsa + "&strFecha=" + strFecha; //INICIATIVA-565
		}
		
		function Imprimir()
	    {					
			var objIframe = document.getElementById("IfrmImpresion");
			window.open(objIframe.contentWindow.location);
			
	    }
	    
	    
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<input id="hldVerif" type="hidden" name="hldVerif" runat="server">
			<div id="overDiv" style="Z-INDEX: 101; WIDTH: 100px; POSITION: absolute"></div>
			<table cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="810">
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
												height="32">Envío a Caja Buzón</td>
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
														<TD style="WIDTH: 66px; HEIGHT: 14px" width="66"></TD>
														<TD style="WIDTH: 212px; HEIGHT: 14px" class="Arial12b">&nbsp;&nbsp; Número de 
															sobre:</TD>
														<TD style="WIDTH: 193px; HEIGHT: 14px" class="Arial12b"><asp:textbox id="txtBolsa" runat="server" MaxLength="10" CssClass="clsInputEnable"></asp:textbox></TD>
														<TD style="WIDTH: 154px; HEIGHT: 14px" class="Arial12b" width="154">Fecha:</TD>
														<td width="172" class="Arial12b" style="WIDTH: 172px; HEIGHT: 14px" valign="middle"><asp:textbox id="txtFecha" runat="server" CssClass="clsInputEnable" Width="144px"></asp:textbox></td>
                                                                                                             <TD style="HEIGHT: 14px" class=Arial12b vAlign=middle 
                                                                                                             width=300><A 
                                                                                                             onmouseover="window.status='Date Picker';return true;" 
                                                                                                             onmouseout="window.status='';return true;" 
                                                                                                             href="javascript:show_calendar('frmPrincipal.txtFecha');"><IMG border="0" src="../../images/botones/btn_Calendario.gif" style="Z-INDEX: 0"></A></TD>
													</TR>
													<tr>
														<td style="WIDTH: 66px" width="66">&nbsp;&nbsp;&nbsp;</td>
														<td class="Arial12b" style="WIDTH: 212px">&nbsp;&nbsp;&nbsp;Importe a enviar:</td>
														<td class="Arial12b" style="WIDTH: 193px"><asp:textbox id="txtMonto" runat="server" CssClass="clsInputEnable" MaxLength="15"></asp:textbox></td>
														<TD class="Arial12b" style="WIDTH: 154px" width="60">Tipo:
														</TD>
														<td width="172" style="WIDTH: 172px">
															<asp:dropdownlist id="cboMoneda" runat="server" CssClass="clsSelectEnable" Width="144px">
																<asp:ListItem Value="1">Efectivo Soles</asp:ListItem>
																<asp:ListItem Value="2">Efectivo Dolares</asp:ListItem>
																<asp:ListItem Value="3">Cheques Soles</asp:ListItem>
																<asp:ListItem Value="4">Cheques Dolares</asp:ListItem>
															</asp:dropdownlist></td>
                                                                                                                <TD 
                                                                                                                 width=300></TD>
													</tr>
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
								<td height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="400" align="center"
							border="1">
							<TR>
								<TD align="center">
									<TABLE cellSpacing="2" cellPadding="0" border="0">
										<TR>
											<TD align="center" width="28"></TD>
											<TD align="center" width="85"><asp:button id="btnGrabar" runat="server" CssClass="BotonOptm" Width="98px" Text="Grabar"></asp:button></TD>
											<TD align="center" width="28"></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td height="10"></td>
				</tr>
			</table>
			</TD></TR></TABLE>
			<table>
				<tr>
					<td>
						<p style="DISPLAY: none"><asp:textbox id="txtCajero" runat="server"></asp:textbox>
							<asp:textbox id="txtImporte" runat="server"></asp:textbox><asp:textbox id="txtNroBolsa" runat="server"></asp:textbox></p>
					</td>
				</tr>
			</table>
			<iframe id="IfrmImpresion" style="VISIBILITY: hidden; WIDTH: 0px; HEIGHT: 0px" name="IfrmImpresion"
				src="#"></iframe>
		</form>
		<script language="javascript" type="text/javascript">

document.frmPrincipal.txtMonto.onkeypress = e_numero;
function e_numero(){
	if ( !( (event.keyCode>=48) && (event.keyCode<=57)) && !(event.keyCode==13) && !(event.keyCode==46) )
		event.keyCode=0;
}

function f_Valida()
{
  event.returnValue = false;
  
  if(!ValidaFechaA('document.frmPrincipal.txtFecha', false))return false  
  if(!ValidaAlfanumerico('document.frmPrincipal.txtBolsa','el campo numero de bolsa ',false)) return false
  if (!ValidaDecimalB('document.frmPrincipal.txtMonto','el campo Salida a Caja Buzon',false))
     return false;
  if (!ValidaCombo('document.frmPrincipal.cboMoneda','el campo Moneda ',false)) return false;

  event.returnValue = true;
  return true
    
}

		</script>
	</body>
</HTML>
