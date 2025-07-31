<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DevolSaldoFavor.aspx.vb" Inherits="SisCajas.DevolSaldoFavor" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>América Móvil</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script src="../librerias/Lib_FuncGenerales.js" type="text/javascript"></script>
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="JavaScript">
		
		function f_LimpiaControl() {
			with(frmDevolEfectivo) {

				reset();
				document.getElementById("hidCodOficina").value = "";
				document.getElementById("txtCodigoCuenta").value = "";
				document.getElementById("txtMontoSaldoFavor").value = "";
				
			    document.getElementById("hidCodOficina").value = "";
				document.getElementById("txtTipoProducto").value = "";	
				
			}
		}
		
		function validarNumero(event) {
			if (event.keyCode == 8 || event.keyCode == 46) {
				return;
			}
			if (event.keyCode >= 37 && event.keyCode <= 40) { // Allow directional arrows
				return;
			}
			eventoSoloNumeros(event);
		}
		
		function eventoSoloNumeros(){
			// NOTE: Backspace = 8, Enter = 13, '0' = 48, '9' = 57		
			var key = event.keyCode;	
			if ((key <= 13 || (key >= 48 && key <= 57) || (key >= 96 && key <= 105) || (key == 33) || (key == 34) || (key == 35) || (key == 36)|| (key == 37) || (key == 38) || (key == 39) || (key == 40)|| (key == 45) || (key == 46) || (key == 82))==true)
				event.returnValue = true;
			else
				event.returnValue = false;	
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
			}
			else{
				document.getElementById("lbCodOficina").className = "";
				document.getElementById("lbOficina").className = "";
			}
			document.getElementById("hidSwOficina").value = "1";
		}
		
		function removeAllItems(listBox){
			var i;
			for(i=listBox.options.length-1;i>=0;i--){
				listBox.remove(i);
			}
		}
		
		function f_Buscar()
		{
			if(ValidaFechaA('document.frmDevolEfectivo.txtFecha', true))
			{
				document.frmDevolEfectivo.submit();
				f_LimpiaControl();
			}
		}
		
		function f_Devolucion()
		{	
			if (document.getElementById("hidCargaPool").value=="0"){
				alert('Seleccione un pedido para continuar con el proceso de devolucion.');
				
				return;
			}
				
			var strCod = "";			
			var devolvTipDoc="";
			var devolvNroDoc = "";
			var devolvProducto="";
			var devolvCuenta="";
			var devolvFecha="";
			var devolvOficina="";
			var devolvBioHit="";
			
			for (var i = 1; i < dgPool.rows.length; i++){
					if (dgPool.rows[i].cells[0].children.rbPagos.checked){
						strCod = dgPool.rows[i].cells[0].children.rbPagos.value;
						devolvTipDoc =dgPool.rows[i].cells[2].innerText; 
						devolvNroDoc =dgPool.rows[i].cells[3].innerText;  
						devolvProducto=  dgPool.rows[i].cells[4].innerText; 
						devolvCuenta= dgPool.rows[i].cells[5].innerText; 
						devolvFecha =dgPool.rows[i].cells[6].innerText; 
						devolvOficina =dgPool.rows[i].cells[7].innerText; 
						devolvBioHit =dgPool.rows[i].cells[8].innerText; 
						break;																						
					}
			}
			
			if (strCod==""){
				alert('Seleccione un pedido para continuar con el proceso de pago.');
				
				return;
			}

			document.getElementById("hidCodDevolSaldo").value = strCod;
			document.getElementById("hidCodOficina").value = devolvOficina;
			document.getElementById("txtCodigoCuenta").value = devolvCuenta;
			document.getElementById("txtMontoSaldoFavor").value = "";
			document.getElementById("txtTipoProducto").value = devolvProducto;
			document.getElementById("hidTipoProducto").value = devolvProducto;
			document.getElementById("hidCodigoCuenta").value = devolvCuenta;
			document.getElementById("hidBiometriaHit").value = devolvBioHit;
			
		}
		
		
  function f_Boton()
  { 
	var rval = "";
	var strEstado = "";
	
    for (var i = 1; i < dgPool.rows.length; i++){
		if (dgPool.rows[i].cells[0].children.rbPagos.checked){
           rval = dgPool.rows[i].cells[0].children.rbPagos.value;           
           break;
         }  
       }
    if (rval == "")
    {
      alert('Debe Seleccionar un Documento');
      return false;
    }
    else		
      return true;
  }
  
  function f_Devol()
  {  
	event.returnValue = false; 
	
	var strCod = "";			
	var strEstado="";
	var strCodProcesar = "";
	var strAuart="";
	var strCorrelativoSunat="";
	
	for (var i = 1; i < dgPool.rows.length; i++){
			if (dgPool.rows[i].cells[0].children.rbPagos.checked){
				strCod = dgPool.rows[i].cells[0].children.rbPagos.value;		
				break;				
			}
	}
	
	if (strCod==""){
		alert('Seleccione un documento para continuar con el proceso de devolucion.');
		return;
	}
	
	strCod =trim(strCod);
		
    if (f_Boton())
    {			
		return true;	
    }
    else
    {
		return false;
    }
  }
  
  
		</script>
		<style type="text/css">.listBozCssClass { BORDER-RIGHT: #bfbee9 1px solid; BORDER-TOP: #bfbee9 1px solid; BORDER-LEFT: #bfbee9 1px solid; BORDER-BOTTOM: #bfbee9 1px solid; BACKGROUND-COLOR: #bfbee9 }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmDevolEfectivo" method="post" runat="server">
			<input id="hidCodOficina" type="hidden" name="hidCodOficina" runat="server"> <input id="hidSwOficina" type="hidden" name="hidSwOficina" runat="server">
			<input id="hidCodDevolSaldo" type="hidden" name="hidCodDevolSaldo" runat="server">
			<input id="hidCargaPool" type="hidden" name="hidCargaPool" runat="server"> <input id="hidTipoProducto" type="hidden" name="hidTipoProducto" runat="server">
			<input id="hidCodigoCuenta" type="hidden" name="hidCodigoCuenta" runat="server">
			<input id="hidMontoSaldo" type="hidden" name="hidMontoSaldo" runat="server"> <input id="hidBiometriaHit" type="hidden" name="hidBiometriaHit" runat="server">
			<div id="Busqueda">
				<table style="WIDTH: 100%; HEIGHT: 396px" cellSpacing="0" cellPadding="0" width="1008"
					border="0">
					<tr>
						<td vAlign="top" width="10">&nbsp;</td>
						<td vAlign="top" align="center" width="820">
							<table height="14" cellSpacing="0" cellPadding="0" width="810" border="0">
								<tr>
									<td style="WIDTH: 884px" align="center"></td>
								</tr>
							</table>
							<table class="tabla_borde" style="WIDTH: 824px; HEIGHT: 408px" cellSpacing="0" cellPadding="0"
								width="824">
								<tr>
									<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
										width="98%">
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td class="TituloRConsulta" align="center" height="30">Devolucion de Saldo a Favor</td>
											</tr>
										</table>
										<table class="Arial12b" style="WIDTH: 100.03%; HEIGHT: 304px" cellPadding="3">
											<thead>
												<tr>
													<td class="Arial12br" style="HEIGHT: 20px" colSpan="7"><asp:label id="totalRecibos" CssClass="Arial12br" Runat="server">Documentos</asp:label></td>
												</tr>
											</thead>
											<tbody>
												<tr>
													<td style="WIDTH: 736px" colSpan="7">
														<div class="frame2" style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 775px; BORDER-BOTTOM: 1px; HEIGHT: 238px; TEXT-ALIGN: center"><asp:datagrid id="dgPool" runat="server" CssClass="Arial11B" AutoGenerateColumns="False" Width="750px"
																CellSpacing="1" BorderColor="White" Height="30px">
																<AlternatingItemStyle HorizontalAlign="Center" Height="21px" BackColor="#DDDEE2"></AlternatingItemStyle>
																<ItemStyle HorizontalAlign="Center" Height="21px" BackColor="#E9EBEE"></ItemStyle>
																<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
																<Columns>
																	<asp:TemplateColumn HeaderText="Opci&#243;n">
																		<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="2%"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<input id=rbPagos type=radio value='<%# DataBinder.Eval(Container,"DataItem.devolvCodigo")  %>'  name=rbPagos  >
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:BoundColumn DataField="devolvCodigo" HeaderText="Cod. Devolucion">
																		<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="2%"></HeaderStyle>
																		<ItemStyle Height="29px"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="devolvTipDoc" HeaderText="TipoDocumento">
																		<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="3%"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="devolvNroDoc" HeaderText="Nro Documento">
																		<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="7%"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="devolvProducto" HeaderText="Producto">
																		<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="devolvCuenta" HeaderText="Cuenta">
																		<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="devolvFecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
																		<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="devolvOficina" HeaderText="Oficina">
																		<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="7%"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="devolvBioHit" HeaderText="Biometria">
																		<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="4%"></HeaderStyle>
																	</asp:BoundColumn>
																</Columns>
															</asp:datagrid></div>
													</td>
												</tr>
											</tbody>
										</table>
										<table style="WIDTH: 514px; HEIGHT: 61px" borderColor="#336699" cellSpacing="0" cellPadding="0"
											width="514" align="center" border="1">
											<tr>
												<td align="center">
													<table cellSpacing="2" cellPadding="0" border="0">
														<tr>
															<td align="center" width="13"></td>
															<td class="Arial12b" width="50">&nbsp;Desde :</td>
															<td style="DISPLAY: none" align="center" width="13"></td>
															<td vAlign="middle" align="center" width="100"><asp:textbox id="txtFecha" runat="server" CssClass="clsInputEnable" Width="62px" MaxLength="10"></asp:textbox>&nbsp;<A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																	href="javascript:show_calendar('frmDevolEfectivo.txtFecha');"><IMG src="../images/botones/btn_Calendario.gif" border="0" DESIGNTIMEDRAGDROP="5712"></A>
															</td>
															<td align="center" width="85"><INPUT class="BotonOptm" id="btnBuscar" style="WIDTH: 100px" onclick="f_Buscar();" type="button"
																	value="Buscar" name="btnBuscar"></td>
															<td align="center" width="13"></td>
															<td align="center" width="85"><INPUT class="BotonOptm" id="btnDeolucion" style="WIDTH: 100px" onclick="f_Devolucion();"
																	type="button" value="Procesar" name="btnDeolucion"></td>
															<td align="center" width="13"></td>
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
				<br>
			</div>
			<DIV id="Botones" runat="server">
				<TABLE style="WIDTH: 840px; HEIGHT: 316px" cellSpacing="0" cellPadding="0" width="840"
					border="0">
					<TR>
						<TD style="WIDTH: 1063px" vAlign="top" align="center" width="1063">
							<TABLE class="tabla_borde" style="WIDTH: 774px; HEIGHT: 296px" cellSpacing="0" cellPadding="0"
								width="774">
								<TR>
									<TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
										width="98%">
										<TABLE style="WIDTH: 731px; HEIGHT: 185px" cellSpacing="0" cellPadding="0" width="731"
											align="center" border="0">
											<TR>
												<TD class="Arial12br" style="HEIGHT: 20px" colSpan="7"><asp:label id="Label1" CssClass="Arial12br" Runat="server">Registro Devolucion</asp:label></TD>
											</TR>
											<TR>
												<TD>
													<TABLE cellSpacing="2" cellPadding="0" align="center" border="0">
														<TR>
															<TD class="Arial12b" style="HEIGHT: 10px">&nbsp;&nbsp;&nbsp;Tipo 
																de&nbsp;Producto&nbsp;:</TD>
															<TD class="Arial12b" style="PADDING-LEFT: 3px; HEIGHT: 10px"><asp:textbox id="txtTipoProducto" runat="server" CssClass="clsInputEnable" Width="160px" MaxLength="15"
																	Enabled="False"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="Arial12b">&nbsp;&nbsp;&nbsp;Codigo Cuenta&nbsp;:</TD>
															<TD class="Arial12b" style="PADDING-LEFT: 3px"><asp:textbox id="txtCodigoCuenta" runat="server" CssClass="clsInputEnable" Width="160px" MaxLength="15"
																	Enabled="False"></asp:textbox></TD>
														</TR>
													</TABLE>
													&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
													<TABLE style="WIDTH: 166px; HEIGHT: 41px" borderColor="#336699" cellSpacing="0" cellPadding="0"
														width="166" align="center" border="1">
														<TR>
															<TD align="center"><asp:button id="btnValidar" runat="server" CssClass="BotonOptm" Width="100px" Text="Validar"></asp:button>&nbsp;&nbsp;
															</TD>
														</TR>
													</TABLE>
													&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
													<TABLE cellSpacing="2" cellPadding="0" align="center" border="0">
														<TR>
															<TD class="Arial12b">&nbsp;&nbsp;&nbsp;Monto Saldo a Favor :</TD>
															<TD class="Arial12b" style="PADDING-LEFT: 3px">&nbsp; S/. &nbsp;
																<asp:textbox id="txtMontoSaldoFavor" runat="server" CssClass="clsInputEnable" Width="64px" MaxLength="15"
																	Enabled="False"></asp:textbox></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
										<DIV id="Botones2">
											<TABLE style="WIDTH: 38.62%; HEIGHT: 46px" borderColor="#336699" cellSpacing="0" cellPadding="0"
												align="center" border="1">
												<TR>
													<TD align="center">
														<TABLE cellSpacing="2" cellPadding="0" border="0">
															<TR>
																<TD align="center" width="28"></TD>
																<TD style="WIDTH: 134px" align="center" width="134"><asp:button id="cmdProcesar" runat="server" CssClass="BotonOptm" Width="100px" Text="Procesar"></asp:button>&nbsp;&nbsp;
																	<asp:button id="loadDataHandler" style="DISPLAY: none" runat="server" Text="Button"></asp:button><asp:button id="loadOficinaHandler" style="DISPLAY: none" runat="server" Text="Button"></asp:button></TD>
																<TD align="center" width="28"></TD>
																<TD align="center" width="60"><INPUT class="BotonOptm" id="btnLimpiar" style="WIDTH: 100px" onclick="f_LimpiaControl();"
																		type="button" value="Cancelar" name="btnLimpiar" runat="server">
																</TD>
																<TD align="center" width="28"></TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
											</TABLE>
										</DIV>
										<DIV id="divTitulo" style="DISPLAY: none">
											<TABLE style="WIDTH: 392px; HEIGHT: 36px" cellSpacing="2" cellPadding="0" width="392" align="center"
												border="0">
												<TR>
													<TD class="TituloRConsulta" align="center">La&nbsp;devolucion de Saldo a Favor 
														se&nbsp;está&nbsp;procesando</TD>
												</TR>
											</TABLE>
										</DIV>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</DIV>
		</form>
	</body>
</HTML>
