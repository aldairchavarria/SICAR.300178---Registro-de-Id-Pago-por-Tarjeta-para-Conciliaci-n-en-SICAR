<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConfPosOficina.aspx.vb" Inherits="SisCajas.ConfPosOficina"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ConfPosOficina</title>
		<meta content="True" name="vs_snapToGrid">
		<meta content="True" name="vs_showGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
		<style>.tbl_Cajas { BORDER-BOTTOM: #999999 1px solid; BORDER-LEFT: #999999 1px solid; FONT-FAMILY: Arial; FONT-SIZE: 10px; BORDER-TOP: #999999 1px solid; BORDER-RIGHT: #999999 1px solid; TEXT-DECORATION: none }
	.tbl_Cajas TH { TEXT-ALIGN: center; PADDING-BOTTOM: 5px; BACKGROUND-COLOR: #21618c; HEIGHT: 22px; COLOR: #ffffff; PADDING-TOP: 5px }
	.tbl_Cajas TD { BORDER-BOTTOM: #999999 1px solid; TEXT-ALIGN: center; BORDER-LEFT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-RIGHT: #999999 1px solid }
		</style>
		<script language="javascript" type="text/javascript">	
		
		function f_OpenOficina(){
			Direcc = "../../Pagos/ConsultaCajaTot_oficina.aspx"
			window.open(Direcc,"OpenOfLC","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
		}
		
		function f_CargarDatosOficina(){
			document.getElementById('loadDataHandler').click();
		}
		
		function checkEnabledOfi(bCheck){
			if(bCheck){
				var lb1 = document.getElementById("lbCodOficina");
				var lb2 = document.getElementById("lbOficina");
				removeAllItems(lb1);
				removeAllItems(lb2);
				document.getElementById("lbOficina").disabled = true;
				document.getElementById("lbCodOficina").disabled = true;
				document.getElementById("hidCodOficina").value = "";
				document.getElementById("lbOficina").className = "listBozCssClass";
				document.getElementById("lbCodOficina").className = "listBozCssClass";
			}else{
				document.getElementById("lbCodOficina").className = "";
				document.getElementById("lbOficina").className = "";
			}
		}
		
		function removeAllItems(listBox){
			var i;
			for(i=listBox.options.length-1;i>=0;i--){
				listBox.remove(i);
			}
		}
		
		function f_Editar(cod_pdv, oficina, nro_tienda, nro_caja, idestable, ip_caja,equipo, estado,flag_sicar_v,flag_sicar_m,flag_pagoauto,tipo_tarj){
				//document.getElementById('cmdBuscar').click();
				var txt = document.getElementById("txtOculto");

				var strDatosParametros;
				
				if(flag_pagoauto==''){
					
					flag_pagoauto='0';
				
				}
				
				strDatosParametros = 'cod_pdv=' + cod_pdv ;
				strDatosParametros = strDatosParametros +  '&oficina=' + oficina ;
				strDatosParametros = strDatosParametros +  '&nro_tienda=' + nro_tienda ;
				strDatosParametros = strDatosParametros +  '&nro_caja=' + nro_caja ;
				strDatosParametros = strDatosParametros +  '&idestable=' + idestable ;
				strDatosParametros = strDatosParametros +  '&ip_caja=' + ip_caja ;
				strDatosParametros = strDatosParametros +  '&equipo=' + equipo ;
				strDatosParametros = strDatosParametros +  '&estado=' + estado ;
				strDatosParametros = strDatosParametros +  '&flag_sicar_v=' + flag_sicar_v ;
				strDatosParametros = strDatosParametros +  '&flag_sicar_m=' + flag_sicar_m ;
				strDatosParametros = strDatosParametros +  '&flag_pagoauto=' + flag_pagoauto ;
				strDatosParametros = strDatosParametros +  '&tipo_tarj=' + tipo_tarj ;
				
				txt.value = strDatosParametros;
			
				document.getElementById('loadHidEditar').click();
				
			}
			
			function ChangeAllChecks(chk,indice)
			{
				if(indice == 6)
				{
					if(chk.checked == true){
					
						var r = confirm("¿Esta seguro de activar la integración automática de VISA para los registros de la grilla?");
							if (r == true) {
								habilitarGrilla(6);
								document.getElementById('btnGrabarIntegraVISA').click();
								
							} else {
							   chk.checked = false;
								return;
							}					
					}								
					else
					{
						var r = confirm("¿Esta seguro de desactivar la integración automática de VISA  para los registros de la grilla?");
						if (r == true) {
								deshabilitarGrilla(6);
								document.getElementById('btnGrabarIntegraVISA').click();
							} else {
							    chk.checked = true;
								return;
							}			
					}	
				}
				
				else if(indice == 7)
				{
					if(chk.checked == true){
						
						var r = confirm("¿Esta seguro de activar la integración automática de MCD para los registros de la grilla?");
						if (r == true) {
								habilitarGrilla(7);
								document.getElementById('btnGrabarIntegraMCD').click();
								
							} else {
							   chk.checked = false;
								return;
							}					
					}								
					else
					{
						var r = confirm("¿Esta seguro de desactivar la integración automática de MCD  para los registros de la grilla?");
						if (r == true) {
								deshabilitarGrilla(7);
								document.getElementById('btnGrabarIntegraMCD').click();
							} else {
							    chk.checked = true;
								return;
							}			
					}	
				}
				
				else if(indice == 8){
					if(chk.checked == true){
						var r = confirm("¿Esta seguro de activar el pago automático para los registros de la grilla?");
						
						if (r == true) {
								habilitarGrilla(8);
								document.getElementById('btnGrabarPago').click();
							} else {
							    chk.checked = false;
								return;
							}	
					}else
					{
						var r = confirm("¿Esta seguro de desactivar el pago automático para los registros de la grilla?");
						if (r == true) {
								deshabilitarGrilla(8);
								document.getElementById('btnGrabarPago').click();
							} else {
							    chk.checked = true;
								return;
							}	
					}	
				}
				
			}
			
			function deshabilitarGrilla(celda)
			{
				for (var i = 1; i < gridDetalle.rows.length; i++){
						var fila = gridDetalle.rows[i];
						var chk = fila.cells[celda].children(0);					
						chk.checked = false;
				}
			}
			
			function habilitarGrilla(celda)
			{
				for (var i = 1; i < gridDetalle.rows.length; i++){
						var fila = gridDetalle.rows[i];
						var chk = fila.cells[celda].children(0);						
						chk.checked = true;
				}
			}
			
			
				
		</script>
		<style type="text/css">.listBozCssClass { BORDER-BOTTOM: #bfbee9 1px solid; BORDER-LEFT: #bfbee9 1px solid; BACKGROUND-COLOR: #bfbee9; BORDER-TOP: #bfbee9 1px solid; BORDER-RIGHT: #bfbee9 1px solid }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPricipal" name="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="975" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="790">
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10" height="32"></td>
											<td class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="top" align="center" width="98%"
												height="32">Mantenimiento de Oficinas - POS</td>
											<td vAlign="top" width="10" height="32"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="800" border="0">
							<tr>
								<td vAlign="top" width="10">&nbsp;</td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="14"></td>
											<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 5px"
												width="98%">
												<table cellSpacing="0" cellPadding="0" width="770" border="0">
													<tr>
														<td height="4"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="770" border="0">
													<TR>
														<TD class="Arial12b" style="WIDTH: 137px; HEIGHT: 28px" width="137">Ingrese Punto 
															de Venta:&nbsp;</TD>
														<TD class="Arial12b" style="WIDTH: 273px; HEIGHT: 28px" width="273">&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:listbox id="lbCodOficina" runat="server" Font-Size="8" Font-Name="Arial" Rows="3" BorderWidth="0px"
																Width="90px"></asp:listbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:listbox id="lbOficina" runat="server" Font-Size="8" Font-Name="Arial" Rows="3" BorderWidth="0px"
																Width="140px"></asp:listbox></TD>
														<td style="WIDTH: 37px"><IMG style="WIDTH: 24px; HEIGHT: 20px; CURSOR: hand; align: center" onclick="f_OpenOficina()"
																height="20" alt="Buscar oficina" src="../../images/botones/btn_Iconolupa.gif" width="24">
														</td>
														<td><asp:checkbox class="Arial12b" id="chkTodosOf" onclick="checkEnabledOfi(this.checked)" Checked="False"
																Runat="server" Text="Todos"></asp:checkbox></td>
													</TR>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="360" align="center">
							<tr>
								<td align="center">
									<asp:button id="cmdBuscar" runat="server" Width="100px" Text="Buscar" CssClass="BotonOptm"></asp:button>&nbsp;&nbsp;
									<asp:button id="btnLimpiar" runat="server" Width="100px" Text="Limpiar" CssClass="BotonOptm"></asp:button><asp:button id="loadDataHandler" style="DISPLAY: none" runat="server" Text="Button"></asp:button>
									<asp:button id="loadHidEditar" style="DISPLAY: none" runat="server" Text="Button"></asp:button>
									<asp:button id="btnGrabarIntegraVISA" style="DISPLAY: none" runat="server" Text="Button"></asp:button>
									<asp:button id="btnGrabarIntegraMCD" style="DISPLAY: none" runat="server" Text="Button"></asp:button>
									<asp:button id="btnGrabarPago" style="DISPLAY: none" runat="server" Text="Button"></asp:button></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="800" border="0">
							<tr>
								<td vAlign="top" width="10">&nbsp;</td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td>
									<table borderColor="#ffffff" cellSpacing="1" cellPadding="1" width="100%" border="1">
										<tr class="Arial12B" height="21">
											<td style="TEXT-ALIGN: center; WIDTH: 100%">
												<table class="tbl_Cajas" id="tbl_CajasPos" style="BORDER-COLLAPSE: collapse">
													<tr>
														<td style="PADDING-LEFT: 5px; WIDTH: 100%">
															<div class="frame2" style="Z-INDEX: 1; BORDER-BOTTOM: 1px; POSITION: relative; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 775px; HEIGHT: 300px; BORDER-TOP: 1px; BORDER-RIGHT: 1px"><asp:datagrid id="gridDetalle" runat="server" BorderWidth="1px" Width="750px" AutoGenerateColumns="False"
																	CellSpacing="1" CellPadding="1" BorderColor="White">
																	<AlternatingItemStyle HorizontalAlign="Center" BackColor="#E9EBEE" Width="200px"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Center" Height="25px" Width="200px" CssClass="Arial11B" BackColor="#DDDEE2"></ItemStyle>
																	<HeaderStyle HorizontalAlign="Center" Height="22px" CssClass="Arial12B" BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																	<Columns>
																		<asp:TemplateColumn HeaderText="Oficina Venta" ItemStyle-HorizontalAlign="Center">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																			<ItemTemplate>
																				<%# DataBinder.Eval(Container,"DataItem.COD_PDV") & " - " & DataBinder.Eval(Container,"DataItem.BEZEI")%>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="POSV_IDESTABLEC" HeaderText="Cod Establecimiento" HeaderStyle-Width="10%">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="POSV_NROCAJA" HeaderText="NroCaja" HeaderStyle-Width="10%">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="POSV_IPCAJA" HeaderText="IP Caja" HeaderStyle-Width="10%">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:TemplateColumn HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																			<ItemTemplate>
																				<%# IIf(DataBinder.Eval(Container,"DataItem.POSC_ESTADO") = "1", "ACTIVO", "INACTIVO")%>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																			<ItemTemplate>
																				<a  href='javascript:f_Editar("<%# DataBinder.Eval(Container.DataItem, "COD_PDV")%>","<%# DataBinder.Eval(Container.DataItem, "BEZEI")%>","<%# DataBinder.Eval(Container.DataItem, "POSV_NROTIENDA")%>","<%# DataBinder.Eval(Container.DataItem, "POSV_NROCAJA")%>","<%# DataBinder.Eval(Container.DataItem, "POSV_IDESTABLEC")%>","<%# DataBinder.Eval(Container.DataItem, "POSV_IPCAJA")%>","<%# DataBinder.Eval(Container.DataItem, "POSV_EQUIPO")%>","<%# DataBinder.Eval(Container.DataItem, "POSC_ESTADO")%>","<%# DataBinder.Eval(Container.DataItem, "POSC_FLG_SICAR_V")%>","<%# DataBinder.Eval(Container.DataItem, "POSC_FLG_SICAR_M")%>","<%# DataBinder.Eval(Container.DataItem, "POSC_FLG_AUTOM")%>","<%# DataBinder.Eval(Container.DataItem, "FLAG_TIPO_TARJ")%>")'>
																					Editar </a>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderStyle-Width="10%" HeaderStyle-BorderWidth="1px" HeaderStyle-BorderColor="#999999">
																			<HeaderTemplate>
																				Integracion Automatica VISA
																				<asp:CheckBox ID="chkIntegraVISA" name="chkIntegraVISA" Checked="True" onClick="ChangeAllChecks(this,6)"
																					runat="server" />
																			</HeaderTemplate>
																			<ItemTemplate>
																				<asp:CheckBox ID="cboxSelect3" runat="server" Enabled="False" Checked='<%# Convert.ToBoolean(Convert.ToInt32(IIF(DataBinder.Eval(Container.DataItem , "POSC_FLG_SICAR_V").Equals(DBNull.value),0,DataBinder.Eval(Container.DataItem , "POSC_FLG_SICAR_V"))))%>' />
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderStyle-Width="10%" HeaderStyle-BorderWidth="1px" HeaderStyle-BorderColor="#999999">
																			<HeaderTemplate>
																				Integracion Automatica MCD
																				<asp:CheckBox ID="chkIntegraMCD" name="chkIntegraMCD" Checked="True" onClick="ChangeAllChecks(this,7)"
																					runat="server" />
																			</HeaderTemplate>
																			<ItemTemplate>
																				<asp:CheckBox ID="Checkbox2" runat="server" Enabled="False" Checked='<%# Convert.ToBoolean(Convert.ToInt32(IIF(DataBinder.Eval(Container.DataItem , "POSC_FLG_SICAR_M").Equals(DBNull.value),0,DataBinder.Eval(Container.DataItem , "POSC_FLG_SICAR_M"))))%>' />
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderStyle-Width="10%" HeaderStyle-BorderWidth="1px" HeaderStyle-BorderColor="#999999">
																			<HeaderTemplate>
																				Pago Automatico
																				<asp:CheckBox ID="chkPago" name="chkPago" Checked="True" onClick="ChangeAllChecks(this,8)" 
																					runat="server" />
																			</HeaderTemplate>
																			<ItemTemplate>
																				<asp:CheckBox ID="cboxSelect2" runat="server" Enabled="False" Checked='<%# Convert.ToBoolean(Convert.ToInt32(IIF(DataBinder.Eval(Container.DataItem , "POSC_FLG_AUTOM").Equals(DBNull.value),0,DataBinder.Eval(Container.DataItem , "POSC_FLG_AUTOM"))))%>' />
																			</ItemTemplate>
																		</asp:TemplateColumn>
																	</Columns>
																</asp:datagrid></div>
														</td>
													</tr>
												</table>
												<asp:textbox id="txtOculto" runat="server" Width="0px" Height="0px"></asp:textbox></td>
										</tr>
										<tr>
											<td align="center" height="10"></td>
										</tr>
										<tr>
											<td></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table>
							<tr>
								<td height="5"></td>
							</tr>
						</table>
					</td>
					<td vAlign="top" width="10">&nbsp;</td>
				</tr>
			</table>
		</form>
		<input id="hidCodOficina" type="hidden" name="hidCodOficina" runat="server"> <input id="hidEditar" type="hidden" name="hidEditar" runat="server">
		<input id="hidIntegra" type="hidden" name="hidIntegra" runat="server"> 
	
	</body>
</HTML>
