<%@ Page Language="vb" AutoEventWireup="false" Codebehind="grdSubOficinas.aspx.vb" Inherits="SisCajas.grdSubOficinas" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>grdSubOficinas</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
		<style>
		.tbl_Cajas { BORDER-BOTTOM: #999999 1px solid; BORDER-LEFT: #999999 1px solid; FONT-FAMILY: Arial; FONT-SIZE: 10px; BORDER-TOP: #999999 1px solid; BORDER-RIGHT: #999999 1px solid; TEXT-DECORATION: none }
		.tbl_Cajas TH { TEXT-ALIGN: center; PADDING-BOTTOM: 5px; BACKGROUND-COLOR: #21618c; HEIGHT: 22px; COLOR: #ffffff; PADDING-TOP: 5px }
		.tbl_Cajas TD { BORDER-BOTTOM: #999999 1px solid; TEXT-ALIGN: center; BORDER-LEFT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-RIGHT: #999999 1px solid }
		.listBozCssClass { BORDER-BOTTOM: #bfbee9 1px solid; BORDER-LEFT: #bfbee9 1px solid; BACKGROUND-COLOR: #bfbee9; BORDER-TOP: #bfbee9 1px solid; BORDER-RIGHT: #bfbee9 1px solid }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table style="margin:0px; padding:0px;" cellSpacing="0" cellPadding="0" width="975" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="790">
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10" height="30"></td>
											<td class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="center" align="center" width="98%"
												height="30">Mantenimiento de Sub Oficinas</td>
											<td vAlign="top" width="10" height="30"></td>
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
													<tr>
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
													</tr>
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
									<asp:button id="btnBuscar" runat="server" Width="100px" Text="Buscar" CssClass="BotonOptm"></asp:button>&nbsp;&nbsp;
									<asp:button id="btnLimpiar" runat="server" Width="100px" Text="Limpiar" CssClass="BotonOptm"></asp:button>
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
								<td>
									<table borderColor="#ffffff" cellSpacing="1" cellPadding="1" width="100%" border="1">
										<tr class="Arial12B" height="21">
											<td style="TEXT-ALIGN: center; WIDTH: 100%">
												<table class="tbl_Cajas" id="tbl_CajasPos" style="BORDER-COLLAPSE: collapse">
													<tr>
														<td style="PADDING-LEFT: 5px; WIDTH: 100%">
															<div class="frame2" style="Z-INDEX: 1; BORDER-BOTTOM: 1px; POSITION: relative; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 775px; HEIGHT: 275px; BORDER-TOP: 1px; BORDER-RIGHT: 1px">
																<asp:datagrid id="gridDetalle" runat="server" BorderWidth="1px" Width="750px" AutoGenerateColumns="False"
																	CellSpacing="1" CellPadding="1" BorderColor="White">
																	<AlternatingItemStyle HorizontalAlign="Center" BackColor="#E9EBEE" Width="200px"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Center" Height="25px" Width="200px" CssClass="Arial11B" BackColor="#DDDEE2"></ItemStyle>
																	<HeaderStyle HorizontalAlign="Center" Height="22px" CssClass="Arial12B" BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																	<Columns>
																		<asp:BoundColumn DataField="SUOFV_ID" HeaderText="ID" HeaderStyle-Width="10%">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:TemplateColumn HeaderText="Punto de Venta" HeaderStyle-Width="10%">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																			<ItemTemplate>
																				<%# DataBinder.Eval(Container,"DataItem.SUOFC_PUNTO_VENTA") & " - " & DataBinder.Eval(Container,"DataItem.BEZEI")%>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="SUOFV_SUB_OFICINA" HeaderText="Sub Oficina" HeaderStyle-Width="10%">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:TemplateColumn HeaderText="Condición de Pago" HeaderStyle-Width="10%">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																			<ItemTemplate>
																				<%# DataBinder.Eval(Container,"DataItem.SUOFV_CONDIPAGO") & " - " & DataBinder.Eval(Container,"DataItem.SUOFV_CONDIPAGO_DESC")%>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="SUOFV_COMENTARIO" HeaderText="Comentario" HeaderStyle-Width="10%">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:TemplateColumn HeaderText="Control de Crédito" HeaderStyle-Width="10%">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																			<ItemTemplate>
																				<%# DataBinder.Eval(Container,"DataItem.SUOFV_CONTROLCRE") & " - " & DataBinder.Eval(Container,"DataItem.SUOFV_CONTROLCRE_DESC")%>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="SUOFV_CUENTACONTABLE" HeaderText="Cuenta Contable" HeaderStyle-Width="10%">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:TemplateColumn HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																			<ItemTemplate>
																				<%# IIf(DataBinder.Eval(Container,"DataItem.SUOFV_ESTADO") = "1", "ACTIVO", "INACTIVO")%>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																			<ItemTemplate>
																				<a  href='javascript:f_Editar("<%# DataBinder.Eval(Container.DataItem, "SUOFV_ID")%>")'>
																					Editar </a>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderText="Asignar Cajero" ItemStyle-HorizontalAlign="Center">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																			<ItemTemplate>
																				<a  href='javascript:f_AsignarCajero("<%# DataBinder.Eval(Container.DataItem, "SUOFV_ID")%>","<%# DataBinder.Eval(Container.DataItem, "SUOFV_SUB_OFICINA")%>","<%# DataBinder.Eval(Container.DataItem, "SUOFC_PUNTO_VENTA")%>","<%# DataBinder.Eval(Container.DataItem, "BEZEI")%>")'>
																					Asignar </a>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																	</Columns>
																</asp:datagrid>
															</div>
														</td>
													</tr>
												</table>
												<asp:textbox id="txtOculto" Style="display:none;" runat="server" Width="0px" Height="0px"></asp:textbox></td>
										</tr>
										<tr>
											<td align="center" height="10"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="360" align="center">
							<tr>
								<td align="center">
									<input type="button" style="WIDTH: 100px" class="BotonOptm" value="Nuevo" onclick="f_RegistroNuevo()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" runat="server" id="hidCodOficina" name="hidCodOficina">
			<input type="hidden" runat="server" id="hdnGCodOficina" name="hdnGCodOficina">
			<input type="hidden" runat="server" id="hdnCodUsuario" name="hdnCodUsuario">
			<asp:button id="loadDataHandler" style="DISPLAY: none" runat="server" Text="Button"></asp:button>
		</form>
		<script language="javascript" type="text/javascript">
	function f_OpenOficina(){
		Direcc = "../../Pagos/ConsultaCajaTot_oficina.aspx"
		window.open(Direcc,"OpenOfLC","directories=no,menubar=no,scrollbars=no,top=100,resizable=no,left=185,width=420,height=450");
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
	
	function f_CargarDatosOficina(){
		document.getElementById('loadDataHandler').click();
	}
	
	function f_RegistroNuevo(){
		var arrVariables = new Object();
		arrVariables.strOpcion = 'Insert';
		arrVariables.strID = "";
		
		var strVariables = "?strOption=Insert&strID=";
		
		var url = 'mntSuboficina.aspx';
		var retVal = window.showModalDialog(url+strVariables,arrVariables, 'dialogWidth:420px;dialogHeight:450px;Menubar=no;Status=no;Titlebar=no;Toolbar=no;Location=no');
		if (retVal != undefined){
			if(retVal.blnRefresh==true){
				document.getElementById('btnBuscar').click();
			}
		}
	}
	
	function f_Editar(strID){
		var arrVariables = new Object();
		arrVariables.strOpcion = 'Update';
		arrVariables.strID = strID;
		
		var strVariables = '?strOption='+arrVariables.strOpcion+'&strID='+arrVariables.strID;
		
		var url = 'mntSuboficina.aspx';
		var retVal = window.showModalDialog(url+strVariables,arrVariables, 'dialogWidth:420px;dialogHeight:450px;Menubar=no;Status=no;Titlebar=no;Toolbar=no;Location=no');
		if (retVal != undefined){
			if(retVal.blnRefresh==true){
				document.getElementById('btnBuscar').click();
			}
		}
	}
	
	function f_AsignarCajero(strCodSubOficina,strDesSubOficina,strCodOficina,strDesOficina){
		var strURL = "grdCajeroxSubOf.aspx";
		var strVariables = "?CodSubOficina=" + strCodSubOficina + "&DesSubOficina=" + strDesSubOficina + "&CodOficina=" + strCodOficina + "&DesOficina=" + strDesOficina;
		window.location = strURL + strVariables;
	}
		</script>
	</body>
</HTML>
