<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CobranzaDACs.aspx.vb" Inherits="SisCajas.CobranzaDACs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CobranzaDACs</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="jscript">
		
		window.onload = window_onload;	
		
		function window_onload()
		{
		   if (document.frmPrincipal.txtTrama.value != "")
		   {
		     f_Imprimir();    
		     //document.frmPrincipal.txtTrama.value = "";
		     document.getElementById("txtTrama").value ="";
		   }
		}
		
		function f_Imprimir()
		{
		   var objIframe = document.getElementById("ifrmRecDac")
		   
		   objIframe.style.visibility = "visible";
		   objIframe.style.width = 0;
		   objIframe.style.height = 0;
		   
		   objIframe.src = "docRecaudacionDAC.aspx?strTrama=" + document.frmPrincipal.txtTrama.value + "&MontoTotalPagado=" + document.frmPrincipal.txtMonto.value + "&Dealer=" + document.frmPrincipal.txtDealer.value;
		
		}
		
		function Imprimir(){
		var objIframe = document.getElementById("ifrmRecDac");
		if(ifrmRecDac.window.document.all["printbtn"])ifrmRecDac.window.document.all["printbtn"].style.visibility = "HIDDEN";//'?
		
		window.frames["ifrmRecDac"].focus();
		window.frames["ifrmRecDac"].print();
		
		objIframe.style.visibility = "hidden";
		objIframe.style.width = 0;
		objIframe.style.height = 0;
		objIframe.contentWindow.location.replace('#');
		
	}
		
		
		function ValidaNumero(obj){
			var KeyAscii = window.event.keyCode;
			if (KeyAscii==13) return;	
			if (!(KeyAscii >= 47 && KeyAscii<=57) ){		
				window.event.keyCode = 0;
			}	
		}
		
		//-- GB 05/2015
		function GetTipoPago()
		{
			document.frmPrincipal.txtTipoPago.value = document.frmPrincipal.cboOpcionPago.value;
			//alert(document.frmPrincipal.cboOpcionPago.value)
		}
		//--
		
		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="600" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="600">
						<table height="14" cellSpacing="0" cellPadding="0" width="600" border="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="600" align="center" border="0" name="Contenedor">
							<tr>
								<td align="center">
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="550" align="center"
										border="1">
										<tr>
											<td align="center">
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td class="TituloRConsulta" align="center" width="98%" height="32">Recaudación - 
															Cobranza DAC / Renta Adelantada</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="550" align="center" border="0">
													<tr>
														<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
															width="98%">
															<table cellSpacing="0" cellPadding="0" width="500" border="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table cellSpacing="2" cellPadding="2" width="500" border="0">
																			<tr>
																				<td class="Arial12b" style="WIDTH: 120px" align="center" width="200">&nbsp;&nbsp; 
																					Tipo de Pago :</td>
																				<td class="Arial12b" colSpan="4">&nbsp;
																					<asp:dropdownlist id="cboOpcionPago" runat="server" Width="136px" onchange="GetTipoPago()"></asp:dropdownlist></td>
																				<td class="Arial12b" width="270" colSpan="3"><asp:textbox id="txtCodCliente" runat="server" name="txtNumero" Width="104px" CssClass="clsInputEnable"
																						MaxLength="15"></asp:textbox><asp:requiredfieldvalidator id="rfvCodCliente" runat="server" ErrorMessage="*" ControlToValidate="txtCodCliente">*</asp:requiredfieldvalidator></td>
																			</tr>
																			<tr>
																				<td style="WIDTH: 120px"></td>
																			</tr>
																			<tr>
																				<td class="Arial12b" style="WIDTH: 120px" align="center" width="200"></td>
																				<td class="Arial12b" colSpan="4">&nbsp;
																					<asp:button id="cmdBuscar" runat="server" Width="100px" CssClass="BotonOptm" Text="Buscar"></asp:button></td>
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
			<p style="DISPLAY: none"><asp:textbox id="txtTrama" runat="server" Width="104px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox><asp:textbox id="txtMonto" runat="server" Width="104px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox><asp:textbox id="txtDealer" runat="server" Width="104px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox></p>
			<iframe id="ifrmRecDac" style="VISIBILITY: hidden; WIDTH: 0px; HEIGHT: 0px" src="#">
			</iframe>
			<p style="DISPLAY: none">
				<asp:textbox id="txtTipoPago" runat="server"></asp:textbox>
			</p>
		</form>
	</body>
</HTML>
