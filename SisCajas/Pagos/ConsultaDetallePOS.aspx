<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConsultaDetallePOS.aspx.vb" Inherits="SisCajas.ConsultaDetallePOS" %>
<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Transacciones POS</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/msrsclient.js"></script>
		<script language="JavaScript" src="../Scripts/jquery-1.1.js"></script>
		<script language="JavaScript" src="../Scripts/form.js"></script>
		<script language="JavaScript" src="../Scripts/xml2json.js"></script>
		<script language="JavaScript" src="../Scripts/operacionPOS.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<style>
		</style>
		<script language="javascript">
		
			//PROY-27440 INI
		
			function f_buscar()
			{
				if (f_Validar())
					{
						var btnBuscar = document.getElementById('btnBuscar');
						btnBuscar.click();
					}
			}
		
			function f_Validar() {// valida campos
			     
			     var chCodigoComercio= document.getElementById('chkTodosComercio');
			     if (chCodigoComercio.checked == false)
			     {
					if (!ValidaAlfanumerico('document.frmPrincipal.txtCodComercio','el codigo de comercio ',false)) return false;
			     }
			     			
				 if (!ValidaFechaA('document.frmPrincipal.txtFecIni',false)) return false;	
				 if (!ValidaFechaA('document.frmPrincipal.txtFecFin',false)) return false;					
				 					 
				 var cbCajero = document.getElementById('cboCajero');
				 var chCajero = document.getElementById('chkTodosCajero');	
				  
				 if(cbCajero.selectedIndex == 0 && chCajero.checked == false)
				 {	
					alert("Seleccione un Cajero");
					return false;
				 }
				 		
				return true;  
			}
	        
	           
		</script>
	</HEAD>
	<BODY>
		<div style="LEFT: 18px; WIDTH: 10px; POSITION: absolute; TOP: 8px; HEIGHT: 10px" ms_positioning="text2D">
			<form id="frmPrincipal" method="post" name="frmPrincipal" runat="server">
				<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="800">
					<tr>
						<td>
							<table class="Arial10B" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
								<tr>
									<td height="4" width="10"></td>
									<td class="TituloRConsulta" height="32" width="98%" align="center">Consulta de 
										Cajas - Pagos con Tarjeta de Credito y/o Debito</td>
									<td height="32" width="14"></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>
							<table>
								<tr>
									<td width="250">&nbsp;</td>
									<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Oficina de Venta :
									</td>
									<td class="Arial12b" width="450">&nbsp;
										<asp:listbox id="lstPDV" runat="server" SelectionMode="Single" Font-Name="Arial" Font-Size="8"
											AutoPostBack="True" Width="200px" Rows="6"></asp:listbox>
										<asp:checkbox id="chkTodosOficina" runat="server" AutoPostBack="True" Text="Todos"></asp:checkbox></td>
								</tr>
								<tr>
									<td width="250">&nbsp;</td>
									<td class="Arial12b" width="200">&nbsp;&nbsp; Tipo Tarjeta :
									</td>
									<td class="Arial12b" width="450">&nbsp;
										<asp:dropdownlist id="cboTipoTarjeta" runat="server" Width="200px" CssClass="clsSelectEnable"></asp:dropdownlist></td>
								</tr>
								<tr>
									<td width="250">&nbsp;</td>
									<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Codigo Comercio:</td>
									<td class="Arial12b" width="450">&nbsp;&nbsp;<asp:TextBox id="txtCodComercio" CssClass="clsInputEnable" runat="server"></asp:TextBox>
										<asp:checkbox id="chkTodosComercio" runat="server" AutoPostBack="True" Text="Todos"></asp:checkbox></td>
								</tr>
								<tr runat="server">
									<td style="HEIGHT: 23px" width="250">&nbsp;</td>
									<td style="HEIGHT: 23px" class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Fecha :
									</td>
									<td style="HEIGHT: 23px" class="Arial12b" width="450">&nbsp;
										<asp:TextBox id="txtFecIni" CssClass="clsInputEnable" maxLength="15" runat="server"></asp:TextBox>
										&nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
											href="javascript:show_calendar('frmPrincipal.txtFecIni');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
										&nbsp;a&nbsp;
										<asp:TextBox id="txtFecFin" CssClass="clsInputEnable" maxLength="15" runat="server"></asp:TextBox>
										&nbsp; <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
											href="javascript:show_calendar('frmPrincipal.txtFecFin');"><IMG border="0" src="../../images/botones/btn_Calendario.gif">
										</A>
									</td>
								</tr>
								<tr>
									<td style="HEIGHT: 21px" width="250">&nbsp;</td>
									<td style="HEIGHT: 21px" class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Cajero :
									</td>
									<td style="HEIGHT: 21px" class="Arial12b" width="450">&nbsp;
										<asp:dropdownlist id="cboCajero" runat="server" Width="200px" CssClass="clsSelectEnable"></asp:dropdownlist>
										<asp:CheckBox id="chkTodosCajero" runat="server" AutoPostBack="True" Text="Todos"></asp:CheckBox>
									</td>
								</tr>
								<tr>
									<td width="250"></td>
									<td class="Arial12b" width="200">&nbsp;&nbsp; Tipo Transaccion :</td>
									<td class="Arial12b" width="450">&nbsp;
										<asp:dropdownlist id="cboTipoTransaccion" runat="server" Width="200px" CssClass="clsSelectEnable"></asp:dropdownlist></td>
								</tr>
								<tr>
									<td width="250"></td>
									<td class="Arial12b" width="200">&nbsp;&nbsp; Tipo Operacion :&nbsp;&nbsp;
									</td>
									<td class="Arial12b" width="450">&nbsp;
										<asp:dropdownlist id="cboTipoOperacion" runat="server" Width="200px" CssClass="clsSelectEnable"></asp:dropdownlist></td>
								</tr>
								<tr>
									<td width="250"></td>
									<td class="Arial12b" width="200">&nbsp;&nbsp; Estado Transaccion :</td>
									<td class="Arial12b" width="450">&nbsp;
										<asp:dropdownlist id="cboEstadoOperacion" runat="server" Width="200px" CssClass="clsSelectEnable"></asp:dropdownlist></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>
							<table style="MARGIN-TOP: 20px; MARGIN-BOTTOM: 20px" border="1" cellSpacing="0" borderColor="#336699"
								cellPadding="4" width="550" align="center">
								<tr id="trOpciones">
									<td>
										<table id="Table10" class="Arial10B" border="0" cellSpacing="0" cellPadding="0" width="100%"
											align="center">
											<tr>
												<td style="DISPLAY: none; TEXT-ALIGN: center"><asp:button id="btnBuscar" Width="100" Text="Buscar" CssClass="BotonOptm" Runat="server"></asp:button></td>
												<td style="TEXT-ALIGN: center"><INPUT style="WIDTH: 100px" id="btnBuscarJV" class="BotonOptm" onclick="f_buscar()" value="Buscar"
														type="button">
												</td>
												<td style="TEXT-ALIGN: center"><asp:button id="btnLimpiar" Width="100" Text="Limpiar" CssClass="BotonOptm" Runat="server"></asp:button>
												</td>
												<td style="TEXT-ALIGN: center"><INPUT style="WIDTH: 100px" id="btnExportar" class="BotonOptm" onclick="f_Exportar()" value="Exportar"
														type="button">
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr align="center">
						<td>
							<table style="WIDTH: 100%" id="tbl_Detalle" cellSpacing="0" borderColor="#336699" cellPadding="4"
								align="center">
								<tr>
									<td>
										<table>
											<tr>
												<td style="WIDTH: 100%; TEXT-ALIGN: center"><asp:datagrid id="dgTransacciones" runat="server" Width="100%" CssClass="tabla_interna_borde1"
														AutoGenerateColumns="False" CellSpacing="1" CellPadding="1" BorderWidth="0px">
														<AlternatingItemStyle Height="25px" BackColor="#DEE9FA"></AlternatingItemStyle>
														<ItemStyle Height="25px" HorizontalAlign="Center" BorderWidth="0px" CssClass="Arial12B" BackColor="#D0D8F0"></ItemStyle>
														<HeaderStyle HorizontalAlign="Center" Height="21px" CssClass="Arial12B"></HeaderStyle>
														<Columns>
															<asp:BoundColumn DataField="codPdv" HeaderText="PUNTO VENTA">
																<HeaderStyle Width="10%" CssClass="tabla_interna_borde2"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="usuavCajero" HeaderText="CAJERO">
																<HeaderStyle Width="10%" CssClass="tabla_interna_borde2"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="posvIdestablec" HeaderText="COMERCIO">
																<HeaderStyle Width="10%" CssClass="tabla_interna_borde2"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="trnsvNumPedido" HeaderText="PEDIDO">
																<HeaderStyle Width="10%" CssClass="tabla_interna_borde2"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="trnsvIdRef" HeaderText="REF ID">
																<HeaderStyle Width="10%" CssClass="tabla_interna_borde2"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="trnsvTipoTrans" HeaderText="TIPO TRANSACCION">
																<HeaderStyle Width="10%" CssClass="tabla_interna_borde2"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="trnsvEstado" HeaderText="ESTADO">
																<HeaderStyle Width="10%" CssClass="tabla_interna_borde2"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="trnscOperacion" HeaderText="OPERACION">
																<HeaderStyle Width="10%" CssClass="tabla_interna_borde2"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="trnsvTipoTarjetaPos" HeaderText="TARJETA">
																<HeaderStyle Width="10%" CssClass="tabla_interna_borde2"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="trnsvNroTarjeta" HeaderText="NRO TARJETA">
																<HeaderStyle Width="10%" CssClass="tabla_interna_borde2"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="trnsnMonto" HeaderText="MONTO" DataFormatString="{0:0.00}">
																<HeaderStyle Width="10%" CssClass="tabla_interna_borde2"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
														</Columns>
													</asp:datagrid></td>
											</tr>
											<tr>
												<td><iframe style="DISPLAY: none" id="ifraExcel"></iframe>
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<script language="JavaScript" type="text/javascript">
						var esNavegador, esIExplorer;

						esNavegador = (navigator.appName == "Netscape") ? true : false;
						esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

						if (esIExplorer) {
						}
				</script>
			</form>
			<script language="JavaScript">
	function f_Exportar()
	{
		if (f_Validar())
			{
				
	        var cbCajero = document.getElementById('cboCajero');
		    var chCajero = document.getElementById('chkTodosCajero');
		    var varCajero = ((chCajero.checked == false) ? cbCajero.value : '');
		    
		    
		    var lbPDV = document.getElementById('lstPDV');
		    var chPDV = document.getElementById('chkTodosOficina');	
		    var varPDV = ((chPDV.checked == false) ? lbPDV.value : '');	 
		  
		    var varTipoTarjeta = ((document.getElementById('cboTipoTarjeta').value == "TODOS") ? '' : document.getElementById('cboTipoTarjeta').value);
		 
		 
		    var txCodigoComercio = document.getElementById('txtCodComercio');
		    var chCodigoComercio= document.getElementById('chkTodosComercio');
		    var varCodComercio = ((chCodigoComercio.checked == false) ? txCodigoComercio.value : '');
		    
		   	    
		    var varFechaInicio = document.getElementById('txtFecIni').value;
		    var varFechaFin= document.getElementById('txtFecFin').value;
		    
		    
		    var varTipoTransaccion= ((document.getElementById('cboTipoTransaccion').value =="TODOS") ? '' : document.getElementById('cboTipoTransaccion').value) ;
		    var varTipoOperacion= ((document.getElementById('cboTipoOperacion').value =="TODOS") ? '' :document.getElementById('cboTipoOperacion').value);
		    var varEstadoTransaccion= ((document.getElementById('cboEstadoOperacion').value=="TODOS") ? '': document.getElementById('cboEstadoOperacion').value);
		    
		
			document.all.ifraExcel.src="../reportes/rep_ExcelPOS.aspx?varPDV="+varPDV+"&varTipoTarjeta="+varTipoTarjeta+"&varCodComercio="+varCodComercio+"&varFechaInicio="+varFechaInicio+"&varFechaFin="+varFechaFin+"&varCajero="+varCajero+"&varTipoTransaccion="+varTipoTransaccion+"&varTipoOperacion="+varTipoOperacion+"&varEstadoTransaccion="+varEstadoTransaccion;
		
			}
		}
			</script>
		</div>
	</BODY>
</HTML>
