<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SICAR_Arqueo_Caja.aspx.vb" Inherits="SisCajas.SICAR_Arqueo_Caja"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>SICAR_Arqueo_Caja</TITLE>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../librerias/msrsclient.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" name="frmPrincipal" runat="server">
			<input id="hidVerif" type="hidden" name="hidVerif" runat="server"> <input id="hidValor" type="hidden" name="hidValor" runat="server">
			<input id="hidTipoArqueo" type="hidden" name="hidTipoArqueo" runat="server"> <input id="hidRespCaja" type="hidden" name="hidRespCaja" runat="server">
			<input id="hidRespArqueo" type="hidden" name="hidRespArqueo" runat="server"> <input id="hidCantidad" type="hidden" name="hidCantidad" runat="server">
			<input id="hidMonto" type="hidden" name="hidMonto" runat="server"> <input id="hidMontoSicar" type="hidden" name="hidMontoSicar" runat="server">
			<input id="hidMontoFondo" type="hidden" name="hidMontoFondo" runat="server"> <input id="hidMontoArqueo" type="hidden" name="hidMontoArqueo" runat="server">
			<input id="hidTotalEfectivoSicar" type="hidden" name="hidTotalEfectivoSicar" runat="server">
			<input id="hidTotalTarjetaSicar" type="hidden" name="hidTotalTarjetaSicar" runat="server">
			<input id="hidTotalChequeSicar" type="hidden" name="hidTotalChequeSicar" runat="server">
			<input id="hidTotalEfectivo" type="hidden" name="hidTotalEfectivo" runat="server">
			<input id="hidTotalTarjeta" type="hidden" name="hidTotalTarjeta" runat="server">
			<input id="hidTotalCheque" type="hidden" name="hidTotalCheque" runat="server"> <input id="hidIsPostBack" type="hidden" name="hidIsPostBack" runat="server">
			<input id="hidDiv" type="hidden" name="hidDiv" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="850">
				<TR>
					<TD vAlign="top" width="10">&nbsp;</TD>
					<TD vAlign="top" rowSpan="2" width="820">
						<TABLE id="Table7" class="tabla_borde" cellSpacing="0" cellPadding="0" width="790" align="center">
							<TR>
								<TD align="center">
									<TABLE style="WIDTH: 811px; HEIGHT: 160px" id="Table8" border="0" cellSpacing="0" cellPadding="0"
										width="811" align="center">
										<TR>
											<TD>
												<TABLE id="Table9" class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="100%"
													align="center">
													<TR>
														<TD height="10">
															<TABLE id="Table10" border="0" cellSpacing="1" cellPadding="0" align="center">
																<TR class="Arial12b" bgColor="white">
																	<TD class="TituloRConsulta" align="center">Arqueo de Caja</TD>
																</TR>
															</TABLE>
														</TD>
													</TR>
												</TABLE>
												<TABLE style="WIDTH: 810px; HEIGHT: 22px" id="Table11" class="Arial12B" border="0" cellSpacing="2"
													cellPadding="0" width="810" align="center">
													<TR>
														<TD width="25">&nbsp;</TD>
														<TD style="WIDTH: 144px" width="144">&nbsp;&nbsp;&nbsp;Fecha de&nbsp;Arqueo :</TD>
														<TD style="WIDTH: 227px" width="227">
															<DIV id="divNat" class="dropcont"><asp:textbox id="txtFechaArqueo" runat="server" CssClass="clsInputEnable" MaxLength="10"></asp:textbox><A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																	href="javascript:show_calendar('frmPrincipal.txtFechaArqueo');"><IMG style="WIDTH: 28px; HEIGHT: 33px" border="0" src="../images/botones/btn_Calendario.gif"
																		width="28" height="33"></A></DIV>
														</TD>
														<TD style="WIDTH: 96px" width="96">&nbsp;</TD>
														<TD width="170">&nbsp;&nbsp;&nbsp;</TD>
														<TD align="left">&nbsp;&nbsp;<A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmPrincipal.txtFechaPrecioVenta');"></A>
														</TD>
													</TR>
												</TABLE>
												<TABLE id="Table12" class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="100%"
													align="center">
													<TR>
														<TD width="25">&nbsp;</TD>
														<TD style="WIDTH: 144px" width="144">&nbsp;&nbsp;&nbsp;Resp. Caja :</TD>
														<TD style="WIDTH: 238px" width="238">
															<DIV id="divNat2" class="dropcont"><asp:textbox id="txtRespCaja" runat="server" CssClass="clsInputEnable" MaxLength="12" Width="220px"></asp:textbox></DIV>
														</TD>
														<TD style="WIDTH: 12px" width="12">&nbsp;</TD>
														<TD style="WIDTH: 106px" width="106">&nbsp;&nbsp;&nbsp;Resp.&nbsp;Arqueo :</TD>
														<TD align="left"><asp:textbox id="txtRespArqueo" runat="server" CssClass="clsInputEnable" MaxLength="12" Width="220px"></asp:textbox></TD>
													</TR>
												</TABLE>
												<DIV id="dcodcomer" class="dropcont">
													<TABLE id="Table13" class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="100%"
														align="center">
														<TR>
															<TD width="25">&nbsp;</TD>
															<TD style="WIDTH: 144px" width="144">&nbsp;&nbsp;&nbsp;Valor&nbsp;Sicar&nbsp;:</TD>
															<TD style="WIDTH: 178px" width="178"><asp:textbox id="txtMontoSicar" runat="server" CssClass="clsInputEnable" MaxLength="12"></asp:textbox></TD>
															<TD style="WIDTH: 71px" width="71">&nbsp;</TD>
															<TD style="WIDTH: 107px" width="107">&nbsp;&nbsp;&nbsp;Monto&nbsp;Fondo&nbsp;:</TD>
															<TD width="170"><asp:textbox id="txtMontoFondo" runat="server" CssClass="clsInputEnable" MaxLength="12"></asp:textbox></TD>
															<TD colSpan="3">&nbsp;</TD>
														</TR>
													</TABLE>
												</DIV>
											</TD>
										</TR>
									</TABLE>
									<asp:label style="Z-INDEX: 0" id="lblInfo" runat="server">lblInfo</asp:label></TD>
							</TR>
						</TABLE>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<TABLE style="WIDTH: 815px; HEIGHT: 80px" id="Table18" class="tabla_borde" cellSpacing="0"
							cellPadding="0" width="815" align="center">
							<TR>
								<TD align="center">
									<TABLE id="Table19" border="0" cellSpacing="0" cellPadding="0" width="810" align="center">
										<TR>
											<TD class="TituloRConsulta" height="30" colSpan="4" align="center"><B>Nuevo Item</B></TD>
										</TR>
									</TABLE>
									<TABLE style="WIDTH: 810px; HEIGHT: 40px" id="Table20" border="0" cellSpacing="0" cellPadding="0"
										width="810" align="center">
										<TR>
											<TD>
												<TABLE style="WIDTH: 780px; HEIGHT: 24px" id="Table21" border="0" cellSpacing="0" borderColor="#d0d8f0"
													cellPadding="0" width="780" bgColor="#ffffff" align="center">
													<TR>
														<TD>
															<TABLE id="Table22" border="0" cellSpacing="2" cellPadding="0" width="100%">
																<TR>
																	<TD width="10">&nbsp;</TD>
																	<TD style="WIDTH: 59px" class="Arial12b" width="59">&nbsp;&nbsp;&nbsp;Tipo :
																	</TD>
																	<TD style="WIDTH: 198px" class="Arial12b" width="198"><asp:dropdownlist id="cboTipoArqueo" runat="server" CssClass="clsSelectEnable5" Width="142px" AutoPostBack="True"></asp:dropdownlist></TD>
																	<TD style="WIDTH: 75px" class="Arial12b" width="75">&nbsp;&nbsp;&nbsp;Valor :
																	</TD>
																	<TD style="WIDTH: 252px" id="tdValor" width="252">&nbsp;
																		<asp:dropdownlist id="cboValor" runat="server" CssClass="clsSelectEnable5" Width="142px" AutoPostBack="True"></asp:dropdownlist></TD>
																	<TD style="WIDTH: 59px" class="Arial12b" width="59">&nbsp;&nbsp;&nbsp;Cantidad :
																	</TD>
																	<TD style="WIDTH: 85px" width="85">&nbsp;
																		<asp:textbox id="txtCantidad" runat="server" CssClass="clsInputEnable" MaxLength="12" Width="51px"></asp:textbox></TD>
																	<TD style="WIDTH: 59px" class="Arial12b" width="59">&nbsp;&nbsp;&nbsp;Monto :
																	</TD>
																	<TD style="WIDTH: 121px" width="121">&nbsp;
																		<asp:textbox id="txtMonto" runat="server" CssClass="clsInputEnable" MaxLength="12" Width="67px"></asp:textbox></TD>
																</TR>
															</TABLE>
														</TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
						<TABLE id="Table27">
							<TR>
								<TD height="2"></TD>
							</TR>
						</TABLE>
						<DIV id="BMenu">
							<TABLE id="Table29" class="tabla_borde" cellSpacing="0" cellPadding="4" width="400" align="center">
								<TR>
									<TD align="center"><asp:button id="btnAgregar" runat="server" CssClass="BotonOptm" Width="100px" Text="Agregar"></asp:button></TD>
									<TD id="tdBtnGrabar" align="center" runat="server"><asp:button id="btnGrabar" runat="server" CssClass="BotonOptm" Width="100px" Text="Grabar"></asp:button></TD>
									<TD style="DISPLAY: none" align="center"><INPUT style="WIDTH: 153px; HEIGHT: 19px" id="btnClaroClub" class="BotonOptm" onclick="abrirPopClaroClub()"
											value="Canje de Puntos ClaroClub" type="button" name="btnClaroClub">
									</TD>
								</TR>
							</TABLE>
						</DIV>
						<TABLE id="Table30">
							<TR>
								<TD height="2"></TD>
							</TR>
						</TABLE>
						<TABLE id="Table31" class="tabla_borde" cellSpacing="0" cellPadding="0" width="790" align="center">
							<TR>
								<TD align="center">
									<TABLE id="Table32" border="0" cellSpacing="0" cellPadding="0" width="810" align="center">
										<TR>
											<TD class="TituloRConsulta" height="30" colSpan="4" align="center">Detalle de 
												Arqueo</TD>
										</TR>
									</TABLE>
									<TABLE id="Table33" border="0" cellSpacing="0" cellPadding="0" width="810" align="center">
										<TR>
											<TD>
												<TABLE id="Table34" border="0" cellSpacing="0" cellPadding="0" width="780" align="center">
													<TR>
														<TD>
															<DIV style="BORDER-BOTTOM: 1px; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 775px; HEIGHT: 130px; BORDER-TOP: 1px; BORDER-RIGHT: 1px"
																class="frame2"><asp:datagrid id="dgDetalle" runat="server" Width="732px" AutoGenerateColumns="False" CellSpacing="1"
																	CellPadding="1" BorderWidth="1px" BorderColor="White">
																	<AlternatingItemStyle HorizontalAlign="Center" BackColor="#E9EBEE"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Center" Height="25px" CssClass="Arial11B" BackColor="#DDDEE2"></ItemStyle>
																	<HeaderStyle HorizontalAlign="Center" Height="22px" CssClass="Arial12B"></HeaderStyle>
																	<Columns>
																		<asp:TemplateColumn>
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="50px"></HeaderStyle>
																			<ItemTemplate>
																				<P align="center">
																					<asp:ImageButton id="iBtnDelete" runat="server" ImageUrl="../images/botones/ico_Eliminar.gif"></asp:ImageButton></P>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="Tipo" HeaderText="Tipo">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="150px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Valor" HeaderText="Valor">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="valorSicar" HeaderText="Valor Sicar">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="100px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Cantidad" HeaderText="Cantidad">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="50px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Monto" HeaderText="Monto">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="100px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Total" HeaderText="Total">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="100px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="idtipo" HeaderText="idtipo"></asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="montoSicar" HeaderText="montoSicar"></asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="montoFondo" HeaderText="montoFondo"></asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="montoArqueo" HeaderText="montoArqueo"></asp:BoundColumn>
																	</Columns>
																</asp:datagrid></DIV>
														</TD>
													</TR>
													<TR class="Arial12B" align="center">
														<TD align="right">&nbsp;</TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
						<TABLE id="Table35">
							<TR>
								<TD height="2"></TD>
							</TR>
						</TABLE>
						<TABLE id="Table36" class="tabla_borde" cellSpacing="0" cellPadding="0" width="815" align="center"
							style="WIDTH: 815px; HEIGHT: 127px">
							<TR>
								<TD>
									<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<TR>
											<TD vAlign="top" width="10"></TD>
											<TD vAlign="middle" width="100%">
												<TABLE id="Table4" class="Arial12B" border="0" cellSpacing="2" cellPadding="0" align="center">
													<TR>
														<TD width="125" style="WIDTH: 125px"></TD>
														<TD style="WIDTH: 89px">Valor Sicar</TD>
														<TD style="WIDTH: 89px">Valor Ingresado</TD>
														<TD style="WIDTH: 89px">Diferencia</TD>
														<TD></TD>
													</TR>
													<TR>
														<TD width="125" style="WIDTH: 125px">Total Efectivo:</TD>
														<TD style="WIDTH: 89px">
															<asp:textbox style="Z-INDEX: 0" id="txtTotalEfectivoSicar" runat="server" CssClass="clsInputDisable"
																Width="81px" ReadOnly="True">0.00</asp:textbox>
														</TD>
														<TD style="WIDTH: 88px">
															<asp:textbox style="Z-INDEX: 0" id="txtTotalEfectivoIngresado" runat="server" CssClass="clsInputDisable"
																Width="81px" ReadOnly="True">0.00</asp:textbox>
														</TD>
														<TD id="tdVuelto">
															<asp:textbox id="txtDiferenciaEfectivo" runat="server" CssClass="clsInputDisable" ReadOnly="True"
																Width="81px" style="Z-INDEX: 0">0.00</asp:textbox></TD>
													</TR>
													<TR>
														<TD width="125" style="WIDTH: 125px">Total Tarjeta:</TD>
														<TD style="WIDTH: 89px">
															<asp:textbox style="Z-INDEX: 0" id="txtTotalTarjetaSicar" runat="server" CssClass="clsInputDisable"
																Width="81px" ReadOnly="True">0.00</asp:textbox></TD>
														<TD style="WIDTH: 88px">
															<asp:textbox style="Z-INDEX: 0" id="txtTotalTarjetaIngresado" runat="server" CssClass="clsInputDisable"
																Width="81px" ReadOnly="True">0.00</asp:textbox></TD>
														<TD>
															<asp:textbox style="Z-INDEX: 0" id="txtDiferenciaTarjeta" runat="server" CssClass="clsInputDisable"
																Width="81px" ReadOnly="True">0.00</asp:textbox></TD>
													</TR>
													<TR>
														<TD width="125" style="WIDTH: 125px">Total Cheque:</TD>
														<TD style="WIDTH: 89px">
															<asp:textbox style="Z-INDEX: 0" id="txtTotalChequeSicar" runat="server" CssClass="clsInputDisable"
																Width="81px" ReadOnly="True">0.00</asp:textbox></TD>
														<TD style="WIDTH: 88px">
															<asp:textbox style="Z-INDEX: 0" id="txtTotalChequeIngresado" runat="server" CssClass="clsInputDisable"
																Width="81px" ReadOnly="True">0.00</asp:textbox></TD>
														<TD>
															<asp:textbox style="Z-INDEX: 0" id="txtDiferenciaCheque" runat="server" CssClass="clsInputDisable"
																Width="81px" ReadOnly="True">0.00</asp:textbox></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			var strSel;
			var serverURL =  '../Pos/ProcesoPOS.aspx';
			var varTipArqEfe = 'ARQUEO EFECTIVO';
			var varTipArqTar = 'ARQUEO TARJETA';
			var varTipArqChe = 'ARQUEO CHEQUE';
			document.frmPrincipal.txtFechaArqueo.focus();
			
			f_ValidarCuadreProceso();
				
			function f_Agregar()
			{
				document.frmPrincipal.hidVerif.value = "0";
				document.frmPrincipal.hidCantidad.value = document.frmPrincipal.txtCantidad.value;
				document.frmPrincipal.hidMonto.value = document.frmPrincipal.txtMonto.value;
				document.frmPrincipal.hidTipoArqueo.value = document.frmPrincipal.cboTipoArqueo[document.frmPrincipal.cboTipoArqueo.selectedIndex].text;

				if (document.frmPrincipal.cboTipoArqueo[document.frmPrincipal.cboTipoArqueo.selectedIndex].text == '--Seleccione--'){
					alert("Debe seleccionar un tipo de arqueo");
					return false;
				}
				
				if (document.frmPrincipal.cboValor[document.frmPrincipal.cboValor.selectedIndex].text == '--Seleccione--'){
					alert("Debe seleccionar un valor");
					return false;
				}
				
				if (!ValidaNumero('document.frmPrincipal.txtCantidad','el campo Cantidad', false)) return false;
				if (!ValidaDecimalB('document.frmPrincipal.txtMonto','el campo Monto', false)) return false;
				
				
				if (parseInt(document.frmPrincipal.txtCantidad.value) < 0){
					alert("El campo Cantidad debe ser mayor o igual a uno");
					return false;
				}
				
				if (parseInt(document.frmPrincipal.txtMonto.value) < 0){
					alert("El campo Monto debe ser mayor o igual a uno");
					return false;
				}
				
				var tabla = document.getElementById("dgDetalle")
				var trs = tabla.getElementsByTagName("tr");
				for (i=1;i<trs.length;i++)
				{
					var tds = trs[i].getElementsByTagName("td");
					if (tds.length > 0)
						if (tds[2].innerText == document.frmPrincipal.cboValor[document.frmPrincipal.cboValor.selectedIndex].text)
						{
							alert("El item ya fue agregado");
							return false;
						}
				}
				
				event.returnValue = true;
				document.frmPrincipal.hidVerif.value = "1";
				return true;
			}
			
			function f_Grabar()
			{
				var tabla = document.getElementById("dgDetalle")
				var trs = tabla.getElementsByTagName("tr");
							  
				if (trs.length <= 1)
				{
					alert("Debe haber al menos un item en el detalle de arqueo de caja.");
					return false;
				}
				
				f_ValidarDiferencias();
			
				return true;       
			}
			
			function f_ValidarDiferencias()
			{
				document.frmPrincipal.hidTotalEfectivoSicar.value = document.frmPrincipal.txtTotalEfectivoSicar.value
				document.frmPrincipal.hidTotalTarjetaSicar.value = document.frmPrincipal.txtTotalTarjetaSicar.value
				document.frmPrincipal.hidTotalChequeSicar.value = document.frmPrincipal.txtTotalChequeSicar.value
				 
				if (document.frmPrincipal.txtDiferenciaEfectivo.value != "0.00" || document.frmPrincipal.txtDiferenciaTarjeta.value != "0.00" || document.frmPrincipal.txtDiferenciaCheque.value != "0.00" )
				{
					alert('Se han encontrado diferencias en el arqueo.');
				}
			}
			
			function f_ValidarCuadreProceso()
			{		
				if (document.frmPrincipal.hidIsPostBack.value == "1") {
					alert("Por favor antes de realizar el Arqueo de Caja, debe realizar el Proceso de Cuadre de Caja");
					if(confirm("¿Está seguro de realizar el proceso de cuadre de caja individual?"))
					{
						document.getElementById("cboTipoArqueo").disabled=false;
						location.href="../Pagos/repCuadreProceso.aspx?tipocuadre=i&botonregresar=1";
					}
					else{
						document.getElementById("cboTipoArqueo").disabled=true;
						document.frmPrincipal.hidDiv.value = "1";
					}
				}
				else
				{
					if(document.frmPrincipal.hidDiv.value == "1")
					{
						document.getElementById("cboTipoArqueo").disabled=true;
					}else
					{
						document.getElementById("cboTipoArqueo").disabled=false;
					}
				}
			}
					
		</script>
	</body>
</HTML>
