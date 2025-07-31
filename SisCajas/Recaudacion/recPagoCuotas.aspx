<%@ Page Language="vb" AutoEventWireup="false" Codebehind="recPagoCuotas.aspx.vb" Inherits="SisCajas.recPagoCuotas"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>recPagoCuotas</title>
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
		    
		   }
		}
		
		function f_Imprimir()
		{
		   var objIframe = document.getElementById("ifrmRecDac")
		   
		   objIframe.style.visibility = "visible";
		   objIframe.style.width = 0;
		   objIframe.style.height = 0;
		   
		   objIframe.src = "docRecPagoCuotas.aspx?strTrama=" + document.frmPrincipal.txtTrama.value + "&MontoTotalPagado=" + document.frmPrincipal.txtMonto.value + "&Dealer=" + document.frmPrincipal.txtDealer.value + "&strTelefono=" + document.frmPrincipal.txtTelef.value;
		
		}
		
		function Imprimir(){
		var objIframe = document.getElementById("ifrmRecDac");
		window.open(objIframe.contentWindow.location);
		
	}
		
		
		function ValidaNumero(obj){
			var KeyAscii = window.event.keyCode;
			if (KeyAscii==13) return;	
			if (!(KeyAscii >= 47 && KeyAscii<=57) ){		
				window.event.keyCode = 0;
			}	
		}
		
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
														<td class="TituloRConsulta" align="center" width="98%" height="32">
															Recaudación - Pago de Cuotas</td>
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
																				<td class="Arial12b" width="200" style="WIDTH: 120px" align="center">
																					&nbsp;&nbsp;Número Telefónico:</td>
																				<td class="Arial12b" width="270" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtNroTelef" runat="server" name="txtNumero" Width="104px" CssClass="clsInputEnable"
																						MaxLength="15"></asp:textbox>
																					<asp:RequiredFieldValidator id="rfvCodCliente" runat="server" ErrorMessage="*" ControlToValidate="txtNroTelef">*</asp:RequiredFieldValidator></td>
																			</tr>
																			<tr>
																				<td class="Arial12b" colSpan="4">&nbsp;</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" align="center" colSpan="4"><asp:button id="cmdBuscar" runat="server" Width="100px" CssClass="BotonOptm" Text="Buscar"></asp:button></td>
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
			<p style="DISPLAY: none">
				<asp:textbox id="txtTrama" runat="server" Width="104px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox>
				<asp:textbox id="txtMonto" runat="server" Width="104px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox>
				<asp:textbox id="txtDealer" runat="server" Width="104px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox>
				<asp:textbox id="txtTelef" runat="server" Width="104px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox>				
			</p>
			<iframe id="ifrmRecDac" style="VISIBILITY:hidden;WIDTH:0px;HEIGHT:0px" src="#">
			</iframe>
		</form>
	</body>
</HTML>
