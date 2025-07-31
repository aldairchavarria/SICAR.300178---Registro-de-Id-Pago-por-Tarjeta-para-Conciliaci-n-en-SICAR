<%@ Page Language="vb" AutoEventWireup="false" Codebehind="poolConsultaPagosCEL.aspx.vb" Inherits="SisCajas.poolConsultaPagosCEL" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Aplicativo PVU</title>
		<meta http-equiv="Pragma" content="no-cache">
		<META http-equiv="Expires" content="Mon, 06 Jan 1990 00:00:01 GMT">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
			<script language="JavaScript" src="../librerias/date-picker.js"></script>
			<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
			<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
			<script language="JavaScript">


			function f_Buscar() {
				if(ValidaFechaA('document.frmPrincipal.txtFecha', true))
				{
					document.frmPrincipal.submit();
				}
			}
			
			function f_imprimirTodo(){
				var estado = document.getElementById("CheckTodos").checked;
				var row = document.getElementById("dgPool").rows.length;
				if (estado == true){
					if (row > 1){
						if(confirm("Esta seguro de imprimir todo los documentos?")) {return true;}
						else {event.returnValue = false;}
					}else{
						alert('No hay transacciones pendientes..!!');
						event.returnValue = false;	
					}
				}	
			}
			
			function Imprimir()
			{					
				var objIframe = document.getElementById("IfrmImpresion");
				if(IfrmImpresion.window.document.all["printbtn"])IfrmImpresion.window.document.all["printbtn"].style.visibility = "HIDDEN";//'?
				
				window.frames["IfrmImpresion"].focus();
				window.frames["IfrmImpresion"].print();
				
				objIframe.style.visibility = "hidden";		
				objIframe.style.width = 0;
				objIframe.style.height = 0;
				objIframe.contentWindow.location.replace('#');
			}				
	
			function f_Imprimir(strCodSAP,strCodSunat,strDepGar,strTipoDoc){
						
					var objIframe = document.getElementById("IfrmImpresion");
					
					objIframe.style.visibility = "visible";
					objIframe.style.width = 0;
					objIframe.style.height = 0;					
					
					objIframe.src = "OperacionesImp.aspx?codRefer="+ strCodSAP+ "&FactSunat="+strCodSunat+"&Reimpresion=1&TipoDoc="+strTipoDoc;
						
			}	
			</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<iframe id="IfrmImpresion" style="VISIBILITY: hidden; WIDTH: 0px; HEIGHT: 0px" name="IfrmImpresion"
			src="#"></iframe>
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="975" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" align="center" width="800">
						<table cellSpacing="2" cellPadding="0" width="90%" align="center" border="0">
							<tr>
								<td class="Arial12B" height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1" name="Contenedor">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="11" height="32"></td>
											<td class="TituloRConsulta" vAlign="top" align="center" width="807" height="32">Documentos 
												de pago MiClaro</td>
											<td vAlign="top" width="9" height="32"></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10"></td>
											<td width="98%">
												<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td>
															<div class="frame2" style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 775px; BORDER-BOTTOM: 1px; HEIGHT: 330px; TEXT-ALIGN: center"><asp:datagrid id="dgPool" runat="server" AutoGenerateColumns="False" Height="30px" Width="1200px"
																	CssClass="Arial11B" CellSpacing="1" BorderColor="White">
																	<AlternatingItemStyle HorizontalAlign="Center" Height="30px" BackColor="#DDDEE2"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Center" Height="30px" BackColor="#E9EBEE"></ItemStyle>
																	<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
																	<Columns>
																		<asp:TemplateColumn HeaderText="Opci&#243;n" ItemStyle-HorizontalAlign="Center">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																			<ItemTemplate>
																				<INPUT id="rbPagos" type="radio" value='<%# DataBinder.Eval(Container,"DataItem.VBELN") %>' name="rbPagos">
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="NAME1" HeaderText="Nombre del Cliente">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="15%"></HeaderStyle>
																			<ItemStyle Height="29px"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Total" HeaderText="Importe" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="DCORTA" HeaderText="Tipo">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="XBLNR" HeaderText="Fact. SUNAT">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="FKDAT" HeaderText="Fecha" DataFormatString="{0:D}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="8%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="VBELN" HeaderText="Doc. SAP">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="8%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="WAERK" HeaderText="Moneda">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="NETWR" HeaderText="Neto" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="MWSBK" HeaderText="Impuesto" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="FKART" HeaderText="FKART" Visible="False">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																	</Columns>
																</asp:datagrid></div>
														</td>
													</tr>
												</table>
											</td>
											<td vAlign="top" align="right" width="14"></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td height="4"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td align="center" height="30">
									<table cellSpacing="1" cellPadding="0" align="center" border="0">
										<tr>
											<td align="center" width="52"><asp:button id="btnImprimir" runat="server" Width="140px" CssClass="BotonOptm" Text="Imprimir Comprobante"></asp:button></td>
											<td align="center" width="4">&nbsp;</td>
											<td align="center" width="50">&nbsp;
												<asp:checkbox id="CheckTodos" runat="server" Width="109px" CssClass="Arial12B" Text="Imprimir todos"></asp:checkbox></td>
											<td align="center" width="4">&nbsp;</td>
											<td align="center" width="52">&nbsp;</td>
											<td align="center" width="4">&nbsp;</td>
											<td align="center" width="52">&nbsp;</td>
											<td align="center" width="4">&nbsp;</td>
											<td align="center" width="52">&nbsp;</td>
											<td align="center" width="4">&nbsp;</td>
											<td align="center" width="52">&nbsp;</td>
											<td align="center" width="4">&nbsp;</td>
											<td vAlign="middle" align="center" width="70"><asp:textbox id="txtFecha" runat="server" Width="68px" CssClass="clsInputEnable"></asp:textbox></td>
											<td align="left" width="30"><A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
													href="javascript:show_calendar('frmPrincipal.txtFecha');"><IMG src="../../images/botones/btn_Calendario.gif" border="0"></A>
											</td>
											<td align="center" width="52"><INPUT class="BotonOptm" id="btnBuscar" style="WIDTH: 80px" onclick="f_Buscar()" type="button"
													value="Buscar" name="btnBuscar"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
