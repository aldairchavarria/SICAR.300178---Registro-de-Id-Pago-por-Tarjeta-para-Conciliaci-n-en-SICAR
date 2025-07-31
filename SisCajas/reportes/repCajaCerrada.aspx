<%@ Page Language="vb" AutoEventWireup="false" Codebehind="repCajaCerrada.aspx.vb" Inherits="SisCajas.repCajaCerrada" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>repCajaCerrada</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript">
		<!--

		function popup(url) {
		msg= window.open(url,"popi","toolbar=no,left=58,top=50,width=680,height=480,directories=no,status=no,scrollbars=yes,resize=no,menubar=no");
		}

		function MM_findObj(n, d) { //v4.01
		var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
			d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);
			}
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
				    				    
					window.location='repCajaCerradaDetalle.aspx?pfecha='+ frmPrincipal.txtFecInicio.value.substr(6,4)+frmPrincipal.txtFecInicio.value.substr(3,2)+frmPrincipal.txtFecInicio.value.substr(0,2) + frmPrincipal.txtFecFinal.value.substr(6,4)+frmPrincipal.txtFecFinal.value.substr(3,2)+frmPrincipal.txtFecFinal.value.substr(0,2);
				}
		};
		
		

		//function f_Validar(){
		//	if ((frmPrincipal.txtFecInicio.value == null) || (frmPrincipal.txtFecInicio.value == "") || (frmPrincipal.txtFecFinal.value == "") || (frmPrincipal.txtFecFinal.value == "")){
		//	 alert('debe ingresar ambas fechas');
		//	 return false;
		//	}			
		//	return true;	
		//}

		//-->
		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" name="frmPrincipal" runat="server">
			<table border="0" cellSpacing="0" cellPadding="0" width="600">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="600">
						<table border="0" cellSpacing="0" cellPadding="0" width="600" height="14" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table border="0" cellSpacing="0" cellPadding="0" width="600" align="center" name="Contenedor">
							<tr>
								<td align="center">
									<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="550" align="center">
										<tr>
											<td align="center">
												<table class="Arial10B" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
													<tr>
														<td height="4" width="10" border="0"></td>
														<td class="TituloRConsulta" height="32" width="98%" align="center">Reporte de cajas 
															cerradas y pendientes de cerrar</td>
														<td height="32" vAlign="top" width="14"></td>
													</tr>
												</table>
												<table border="0" cellSpacing="0" cellPadding="0" width="550" align="center">
													<tr>
														<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 5px"
															width="98%">
															<table border="0" cellSpacing="0" cellPadding="0" width="500">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table border="0" cellSpacing="2" cellPadding="2" width="500">
																			<tr>
																				<td class="Arial12b" width="120">&nbsp;&nbsp;&nbsp;Fec. Inicio:</td>
																				<td class="Arial12b" width="125"><input id="txtFecInicio" class="clsInputEnable" tabIndex="34" readOnly maxLength="10" size="10"
																						name="txtFecInicio" runat="server"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																						href="javascript:show_calendar('frmPrincipal.txtFecInicio');"><IMG border="0" src="../images/botones/btn_Calendario.gif"></A>
																				</td>
																				<td class="Arial12b" width="120">&nbsp;&nbsp;&nbsp;Fec. Final:</td>
																				<td class="Arial12b" width="125"><input id="txtFecFinal" class="clsInputEnable" tabIndex="34" readOnly maxLength="10" size="10"
																						name="txtFecFinal" runat="server"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																						href="javascript:show_calendar('frmPrincipal.txtFecFinal');"><IMG border="0" src="../images/botones/btn_Calendario.gif"></A>
																				</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" colSpan="4" align="center">&nbsp;
																					<asp:label id="lblAlerta" runat="server" ForeColor="Red"></asp:label></td>
																			</tr>
																			<tr>
																				<td class="Arial12b" colSpan="4" align="center"><asp:button id="btnBuscar" runat="server" Width="100" CssClass="BotonOptm" Text="Buscar"></asp:button></td>
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
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="hdnMensaje" type="hidden" name="hdnMensaje" runat="server"> 
			<!-- Valor que lanza los mensajes -->
			<!-- Atributos de la Página --><input id="hdnPuntoDeVenta" type="hidden" name="hdnPuntoDeVenta" runat="server">
			<input id="hdnUsuario" type="hidden" name="hdnUsuario" runat="server"> <input id="hdnBinAdquiriente" type="hidden" name="hdnBinAdquiriente" runat="server">
			<input id="hdnCodComercio" type="hidden" name="hdnCodComercio" runat="server"> <input id="intCanal" type="hidden" name="intCanal" runat="server">
			<input id="hdnRutaLog" type="hidden" name="hdnRutaLog" runat="server"> <input id="hdnDetalleLog" type="hidden" name="hdnDetalleLog" runat="server">
		</form>
	</body>
</HTML>
