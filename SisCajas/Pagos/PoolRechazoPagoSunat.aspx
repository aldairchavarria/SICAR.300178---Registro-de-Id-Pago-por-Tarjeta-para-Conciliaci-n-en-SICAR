<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PoolRechazoPagoSunat.aspx.vb" Inherits="SisCajas.PoolRechazoPagoSunat" enableViewState="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Sistema de Cajas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script src="../librerias/Lib_FuncGenerales.js" type="text/javascript"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
		function f_Anular()
			{  
  				var strCod = "";
  				var strEstado="";
  				var strCodProcesar = "";
  				var strAuart="";
  				var strDocSap="";
  				var sun="";
		
						for (var i = 1; i < dgPool.rows.length; i++)
						{
								if (dgPool.rows[i].cells[0].children.rbPagos.checked)
								{	
									strCod = dgPool.rows[i].cells[0].children.rbPagos.value;
									strEstado =dgPool.rows[i].cells[15].innerText;
									strCodProcesar=dgPool.rows[i].cells[16].children.IdCodigo.value;
									strAuart=dgPool.rows[i].cells[14].children.FKART.value;		
									sun = dgPool.rows[i].cells[5].innerText;	
									break;
								}
						}
						if (strCod == "") 
						{
								document.frmPrincipal.action="";
								alert("Debe Seleccionar un Documento para Anular Pago");					
								return false;
						}
						else
						{
							if (!confirm("¿Seguro que Desea Anular el Documento?"))
								{
									return false;
								}
								else
								{
									frmPrincipal.txtDocSap.value = strCod;
									frmPrincipal.txtDocSunat.value = sun;
									frmPrincipal.cmdAnular.click();
								}
						}				
			}			
		
			function f_Buscar()
			{
				if(ValidaFechaA('document.frmPrincipal.txtFecha', true))
				{
						frmPrincipal.txtpImp.value = "";
						document.frmPrincipal.submit();
				}			
		}
	  
		function CambiaOficina()
			{
			var valor;
						
			valor=frmPrincipal.cboOficina.value;
			
			
			document.getElementById("txtCodOficina2").value=valor;
						
		}
		
		
	  
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<!-- <iframe id="IfrmImpresion" style="VISIBILITY: hidden; WIDTH: 0px; HEIGHT: 0px" name="IfrmImpresion"
			src="#"></iframe> -->
		<form id="frmPrincipal" method="post" runat="server">
			<input id="hidVerif" type="hidden" name="hidVerif" runat="server">
			<table cellSpacing="0" cellPadding="0" width="975" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="790">
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" name="Contenedor">
							<tr>
								<td align="center">
									<table cellSpacing="2" cellPadding="0" width="90%" align="center" border="0">
										<tr>
											<td class="Arial12B" height="10"></td>
										</tr>
									</table>
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
										border="1">
										<tr>
											<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
												width="98%">
												<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td vAlign="top" width="11" height="32"></td>
														<td class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="top" align="center" width="807"
															height="32">Consulta de Documentos Rechazados Por SUNAT</td>
														<td vAlign="top" width="9" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="780" align="center" border="0">
													<tr>
														<td>
															<DIV class="frame2" style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 775px; BORDER-BOTTOM: 1px; HEIGHT: 330px; TEXT-ALIGN: center"><asp:datagrid id="dgPool" runat="server" Height="30px" CssClass="Arial11B" BorderColor="White"
																	CellSpacing="1" Width="1200px" AutoGenerateColumns="False">
																	<AlternatingItemStyle HorizontalAlign="Center" Height="21px" BackColor="#DDDEE2"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Center" Height="21px" BackColor="#E9EBEE"></ItemStyle>
																	<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
																	<Columns>
																		<asp:TemplateColumn HeaderText="Opci&#243;n" ItemStyle-HorizontalAlign="Center">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																			<ItemTemplate>
																				<INPUT id=rbPagos type=radio value='<%# DataBinder.Eval(Container,"DataItem.VBELN") %>' name=rbPagos>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="NAME1" HeaderText="Nombre del Cliente">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="15%"></HeaderStyle>
																			<ItemStyle Height="29px"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Total" HeaderText="Importe" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:N2}">
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
																		<asp:BoundColumn DataField="D_ABRVW" HeaderText="Utilizaci&#243;n">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="8%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="NCUOTAS" HeaderText="Cuota">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
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
																		<asp:BoundColumn DataField="PAGOS" HeaderText="Pago" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:TemplateColumn HeaderStyle-BackColor="#ffffff" ItemStyle-BackColor="#ffffff" ItemStyle-ForeColor="#ffffff">
																			<HeaderStyle Width="0%" BorderWidth="0" BorderStyle="None"></HeaderStyle>
																			<ItemTemplate>
																				<INPUT id=NRO_DEP_GARANTIA style="WIDTH: 1px; HEIGHT: 22px" type=hidden size=1 value='<%# DataBinder.Eval(Container,"DataItem.NRO_DEP_GARANTIA") %>'>
																				<INPUT id=NRO_REF_DEP_GAR style="WIDTH: 1px; HEIGHT: 22px" type=hidden size=1 value='<%# DataBinder.Eval(Container,"DataItem.NRO_REF_DEP_GAR") %>'>
																				<INPUT id=FKART style="WIDTH: 1px; HEIGHT: 22px" type=hidden size=1 value='<%# DataBinder.Eval(Container,"DataItem.FKART") %>'>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderText="Estado SAP">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%" />
																			<ItemTemplate>
																				<asp:Label CssClass='<%# IIf(DataBinder.Eval(Container,"DataItem.ESTADO_SAP").ToLower()="procesado", "estadoItemOK","estadoItemError")%>' Text='<%#DataBinder.Eval(Container,"DataItem.ESTADO_SAP")%>' runat="server" ID="Label1">
																				</asp:Label>
																				</asp:Label>
																			</ItemTemplate>
																			<ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
																				Font-Underline="False" HorizontalAlign="Center" />
																		</asp:TemplateColumn>
																		<asp:TemplateColumn>
																			<HeaderStyle Width="0%" BorderWidth="0" BorderStyle="None"></HeaderStyle>
																			<ItemStyle ForeColor="White" BackColor="White"></ItemStyle>
																			<ItemTemplate>
																				<INPUT id="IdCodigo" name="IdCodigo" style="WIDTH: 1px; HEIGHT: 22px" type="hidden" size="1" value='<%# DataBinder.Eval(Container,"DataItem.ID_T_TRS_PEDIDO") %>'>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="NUMBR" HeaderText="Núm Telefónico">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="9%"></HeaderStyle>
																			<ItemStyle Height="29px"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="ENVIADO" HeaderText="Enviado">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																	</Columns>
																</asp:datagrid></DIV>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td height="4"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td height="10"></td>
										</tr>
									</table>
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="800" align="center"
										border="1">
										<tr>
											<td align="center">
												<table cellSpacing="2" cellPadding="0" border="0">
													<tr>
														<td align="center" width="13"></td>
														<td align="center" width="85"><INPUT class="BotonOptm" id="btnAnular" title="Se elimina el documento seleccionado" style="WIDTH: 100px"
																onclick="f_Anular();" type="button" value="Anular" name="btnAnular">
														</td>
														<td align="center" width="13"></td>
														<td align="center" width="28" class="clsArial10a">Desde:
														</td>
														<td align="center" width="160" valign="middle">
															<asp:textbox id="txtFecha" runat="server" CssClass="clsInputEnable" Width="62px" MaxLength="10"></asp:textbox>&nbsp;<A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmPrincipal.txtFecha');"><IMG src="../../images/botones/btn_Calendario.gif" border="0"></A>
														</td>
														<td align="center" width="13"></td>
														<td align="center" width="28" class="clsArial10a">Hasta:
														</td>
														<td align="center" width="160" valign="middle">
															<asp:TextBox id="txtFecha2" runat="server" CssClass="clsInputEnable" Width="67px" MaxLength="10"></asp:TextBox>&nbsp;
															<a href="javascript:show_calendar('frmPrincipal.txtFecha2');" onMouseOut="window.status='';return true;"
																onMouseOver="window.status='Date Picker';return true;"><img border="0" src="../../images/botones/btn_Calendario.gif"></a>
														</td>
														<td align="center" width="13"></td>
														<td align="center" width="85"><INPUT class="BotonOptm" id="btnBuscar" style="WIDTH: 100px" onclick="f_Buscar();" type="button"
																value="Buscar" name="btnBuscar"></td>
														<td align="center" width="13"></td>
													</tr>
												</table>
												<table cellSpacing="2" cellPadding="0" border="0">
													<tr>
														<td class="login01" style="HEIGHT: 20px" vAlign="middle" align="left" width="100">&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:label id="lblOficina" runat="server">Oficina&nbsp;:</asp:label></td>
														<td style="HEIGHT: 20px" align="right" width="200"><asp:dropdownlist id="cboOficina" runat="server" Width="175px" CssClass="clsSelectEnable"></asp:dropdownlist></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<p style="DISPLAY: none"><asp:button id="cmdAnular" runat="server" Text="Anular"></asp:button><INPUT id="txtRbPagos" type="hidden" name="txtRbPagos" runat="server">
							<asp:textbox id="txtDocSap" runat="server"></asp:textbox><asp:textbox id="txtDocSunat" runat="server"></asp:textbox><asp:textbox id="txtNroDG" runat="server"></asp:textbox><asp:textbox id="txtTipDoc" runat="server"></asp:textbox><asp:textbox id="txtpImp" runat="server"></asp:textbox><asp:textbox id="txtOffline" runat="server"></asp:textbox></p>
					</td>
				</tr>
			</table>
			<input id="txtEfectivo" type="hidden" name="txtEfectivo" runat="server"> <input id="txtRecibido" type="hidden" name="txtRecibido" runat="server">
			<input id="txtEntregar" type="hidden" name="txtEntregar" runat="server"> <input id="flagVentaEquipoPrepago" type="hidden" name="flagVentaEquipoPrepago" runat="server">
			<input id="txtsession" type="hidden" name="txtsession" runat="server"> <input id="hidAccion" type="hidden" name="hidAccion" runat="server">
			<input id="txtCodOficina2" type="hidden" name="txtCodOficina2" runat="server"> <input id="txtTipoUsuario" type="hidden" name="txtTipoUsuario" runat="server">
		</form>
	</body>
</HTML>
