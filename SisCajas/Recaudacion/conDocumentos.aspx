<%@ Page Language="vb" AutoEventWireup="false" Codebehind="conDocumentos.aspx.vb" Inherits="SisCajas.conDocumentos"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>conDocumentos</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="stylesheet">
		<script src="../librerias/Lib_FuncGenerales.js" type="text/javascript"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<!--	<script language="vbscript" src="../librerias/Lib_Imprimir.vbs"></script> -->
		<script language="jscript">
		
			function f_imprimir(){
				//CAMBIADO POR JTN
				var tipoDocumento = getValue('hdnTipoDocumento');
				var numeroDocumento = getValue('numeroDocumentoHiden');				
				var urlRedirect = "docRecaudacion.aspx?p_tipodoc="+ tipoDocumento +"&p_nrosap="+ numeroDocumento;				
				//window.open("docRecaudacion.aspx?p_tipodoc="+getValue('hdnTipoDocumento')+"&p_nrosap="+frmPrincipal.txtNumeroDeudaSAP.value,"docRecaudacion","menubar=false,width=325,height=420");				
				window.open(urlRedirect,"docRecaudacion","menubar=false,width=325,height=420");
				//CAMBIADO HASTA AQUI
				//AbreCajonera();
				event.returnValue = false;
			}
		
			//INICIATIVA - 529 INI
			function solonumeros(e) {
				tecla_codigo = (document.all) ? e.keyCode : e.which;
				if (tecla_codigo == 8 || tecla_codigo == 9) { return true };
				/*para negativos patron =/[0-9\-]/;*/

				patron = /[0-9]/;
				tecla_valor = String.fromCharCode(tecla_codigo);
				return patron.test(tecla_valor);
			}
			function ValidaSeleccion(val1,val2){
				document.getElementById(val2).value="";
				if (document.getElementById(val1).value == "ZEFE"){
					document.getElementById(val2).disabled=true;
				}else{
					document.getElementById(val2).disabled=false;
				}
			}
			function ValidaSeleccion2(){		
				for (var i = 0; i < 9; i++) {
					var a = document.getElementById("dgrPagosE__ctl" + i + "_cboTipDocumento")
					var b = document.getElementById("dgrPagosE__ctl" + i + "_txtDoc")
					if (a != null){
						if (a.value == "ZEFE"){
							b.disabled=true;
						}else{
							b.disabled=false;
						}
					}
				}
			}
			
			//INICIATIVA - 529 FIN
		
		</script>
		<!--INICIATIVA - 529 INI-->
		<style>.hiddencol { DISPLAY: none }
	</style>
		<!--INICIATIVA - 529 FIN-->
	</HEAD>
	<body onload="ValidaSeleccion2();">
		<form id="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td>
						<!-- Atributos de la Página -->
						<table id="Table1" style="BORDER-BOTTOM: #336699 2px solid; BORDER-LEFT: #336699 2px solid; BORDER-TOP: #336699 2px solid; BORDER-RIGHT: #336699 2px solid"
							cellSpacing="0" cellPadding="5" align="left" border="0">
							<thead>
								<tr>
									<td class="TituloRConsulta" align="center">Recaudación - Consulta Documento 
										Recaudación</td>
								</tr>
							</thead>
							<tbody>
								<tr>
									<td>
										<table class="Arial12b" id="Table6" cellPadding="3">
											<thead>
												<tr>
													<td class="Arial12br" colSpan="4">Documento&nbsp;Recaudación</td>
												</tr>
											</thead>
											<tbody>
												<tr>
													<td>Número Doc. SAP:</td>
													<td><input class="clsInputDisable" id="txtNumeroDeudaSAP" readOnly size="16" name="txtNumeroDeudaSAP"
															runat="server"></td>
													<td>Fecha Creación:</td>
													<td><INPUT class="clsInputDisable" id="txtFechaDeudaSAP" readOnly size="12" name="txtFechaDeudaSAP"
															runat="server"></td>
												</tr>
											</tbody>
										</table>
										<br>
										<table class="Arial12b" id="Table2" cellPadding="3">
											<thead>
												<tr>
													<td class="Arial12br" colSpan="3">Datos Cliente</td>
													<td class="Arial12br" align="right"><label></label></td>
												</tr>
											</thead>
											<tbody>
												<tr>
													<td>Doc. Identidad:</td>
													<td><input class="clsInputDisable" id="txtIdentificador" readOnly size="16" name="txtIdentificador"
															runat="server">
													</td>
													<td>Nombre Cliente:</td>
													<td><input class="clsInputDisable" id="txtNombreCliente" readOnly size="60" name="txtNombreCliente"
															runat="server"></td>
												</tr>
												<tr>
													<td>Cantidad Docs Pagados:</td>
													<td><input class="clsInputDisable" id="txtNumeroDocumentos" readOnly size="3" name="txtNumeroDocumentos"
															runat="server"></td>
													<td>Monto Total Pagado:</td>
													<td><input class="clsInputDisable" id="txtValorDeuda" readOnly size="12" name="txtValorDeuda"
															runat="server"></td>
												</tr>
												<tr>
													<td>Tipo de Cliente:</td>
													<td><input class="clsInputDisable" id="txtTipoCli" readOnly size="16" name="txtNumeroDocumentos"
															runat="server"></td>
													<td></td>
													<td></td>
												</tr>
											</tbody>
										</table>
										<br>
										<table class="Arial12b" id="Table3" cellPadding="3">
											<thead>
												<tr>
													<td class="Arial12br" colSpan="3">Pagos</td>
												</tr>
											</thead>
											<tbody>
												<tr>
													<td colSpan="3"><asp:datagrid id="dgrPagos" runat="server" AutoGenerateColumns="False" CssClass="Arial12b">
															<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
															<ItemStyle CssClass="RowOdd"></ItemStyle>
															<Columns>
																<asp:BoundColumn DataField="DESC_VIA_PAGO" HeaderText="Forma de Pago">
																	<HeaderStyle Wrap="False" Width="150px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="NRO_CHEQUE" HeaderText="Nro. Tarjeta/Documento">
																	<HeaderStyle Wrap="False" Width="200px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="IMPORTE_PAGADO" HeaderText="Monto Pagado">
																	<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																</asp:BoundColumn>
															</Columns>
														</asp:datagrid><asp:datagrid id="dgrPagosE" runat="server" AutoGenerateColumns="False" CssClass="Arial12b">
															<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
															<ItemStyle CssClass="RowOdd"></ItemStyle>
															<Columns>
																<asp:TemplateColumn HeaderText="Forma de Pago">
																	<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="135px"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	<ItemTemplate>
																		<asp:dropdownlist id="cboTipDocumento" tabIndex="6" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn HeaderText="Nro. Tarjeta/Documento">
																	<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="135px"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	<ItemTemplate>
																		<asp:textbox id="txtDoc" tabIndex="7" runat="server" CssClass="clsInputEnable" Width="147px" 
 MaxLength="18" onkeypress="return solonumeros(event)" onpaste="return false;"></asp:textbox>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:BoundColumn DataField="VIA_PAGO" HeaderText="VIA PAGO" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
																	<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="DESC_VIA_PAGO" HeaderText="Forma de Pago" ItemStyle-CssClass="hiddencol" 
 HeaderStyle-CssClass="hiddencol">
																	<HeaderStyle Wrap="False" Width="150px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="NRO_CHEQUE" HeaderText="Nro. Tarjeta/Documento" ItemStyle-CssClass="hiddencol" 
 HeaderStyle-CssClass="hiddencol">
																	<HeaderStyle Wrap="False" Width="200px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="IMPORTE_PAGADO" HeaderText="Monto Pagado">
																	<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="ROWID" HeaderText="ROWID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
																	<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																</asp:BoundColumn>
															</Columns>
														</asp:datagrid></td>
												</tr>
											</tbody>
										</table>
										<br>
										<table class="Arial12b" id="Table5" cellPadding="3">
											<thead>
												<tr>
													<td class="Arial12br" colSpan="7">Documentos</td>
												</tr>
											</thead>
											<tbody>
												<tr>
													<td colSpan="7"><asp:datagrid id="dgrRecibos" runat="server" AutoGenerateColumns="False" CssClass="Arial12b">
															<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
															<ItemStyle CssClass="RowOdd"></ItemStyle>
															<Columns>
																<asp:BoundColumn DataField="TIPO_DOC_RECAUD" HeaderText="Tipo Documento">
																	<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="NRO_DOC_RECAUD" HeaderText="N&#250;mero Documento">
																	<HeaderStyle Wrap="False" Width="120px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="DESC_SERVICIO" HeaderText="Descripci&#243;n Servicio">
																	<HeaderStyle Wrap="False" Width="150px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="FECHA_EMISION" HeaderText="Fecha Emisi&#243;n">
																	<HeaderStyle Wrap="False" Width="90px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:TemplateColumn HeaderText="Moneda">
																	<ItemTemplate>
																		<asp:Label id="lblDOC_DescMoneda" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MONEDA_DOCUM") %>'>
																		</asp:Label>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn HeaderText="Total Documento">
																	<ItemTemplate>
																		<asp:Label id="lblImporteRecibo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTE_RECIBO") %>'>
																		</asp:Label>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn HeaderText="Total Pagado">
																	<ItemTemplate>
																		<asp:Label id="lblImportePagado" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTE_PAGADO") %>'>
																		</asp:Label>
																	</ItemTemplate>
																</asp:TemplateColumn>
															</Columns>
														</asp:datagrid></td>
												</tr>
											</tbody>
										</table>
										<br>
									</td>
								</tr>
							</tbody>
						</table>
					</td>
				</tr>
				<tr>
					<td><br>
						<div id="divComandos">
							<TABLE id="Table4" style="BORDER-BOTTOM: #336699 2px solid; BORDER-LEFT: #336699 2px solid; BORDER-TOP: #336699 2px solid; BORDER-RIGHT: #336699 2px solid"
								cellSpacing="0" cellPadding="2" align="center" border="0">
								<TR>
									<TD align="center">
										<table align="center">
											<TR>
												<TD><INPUT class="BotonOptm" id="cmdAnular" style="WIDTH: 93px; HEIGHT: 19px" type="button"
														value="Anular" name="cmdAnular" runat="server"></TD>
												<TD><INPUT style="WIDTH: 93px; HEIGHT: 19px" id="cmdGrabar" class="BotonOptm" value="Grabar"
														type="button" name="cmdGrabar" runat="server"></TD><!--INICIATIVA - 529-->
												<TD><INPUT class="BotonOptm" id="cmdImprmir" style="WIDTH: 93px; HEIGHT: 19px" onclick="f_imprimir();"
														type="button" value="Imprimir" name="cmdImprmir" runat="server"></TD>
												<TD><INPUT class="BotonOptm" id="cmdContinuar" style="WIDTH: 93px; HEIGHT: 19px" type="button"
														value="Terminar" name="cmdContinuar" runat="server"></TD>
												<TD><INPUT class="BotonOptm" id="cmdCancelar" style="WIDTH: 93px; HEIGHT: 19px" type="button"
														value="Cancelar" name="cmdCancelar" runat="server"></TD>
											</TR>
										</table>
									</TD>
								</TR>
							</TABLE>
						</div>
					</td>
				</tr>
			</table>
			<input id="hdnMensaje" type="hidden" name="hdnMensaje" runat="server"> <input id="hdnPuntoDeVenta" type="hidden" name="hdnPuntoDeVenta" runat="server">
			<input id="hdnUsuario" type="hidden" name="hdnUsuario" runat="server"> <input id="hdnBinAdquiriente" type="hidden" name="hdnBinAdquiriente" runat="server">
			<input id="hdnCodComercio" type="hidden" name="hdnCodComercio" runat="server"> <input id="intCanal" type="hidden" name="intCanal" runat="server">
			<input id="intAccion" type="hidden" name="intAccion" runat="server"> <input id="txtTramaDeudaSAP" type="hidden" name="txtTramaDeudaSAP" runat="server">
			<input id="hdnFechaBusqueda" type="hidden" name="hdnFechaBusqueda" runat="server">
			<input id="hdnTipoCambioPago" type="hidden" name="hdnTipoCambioPago" runat="server">
			<input id="hidEstadoServicioPago" type="hidden" name="hidEstadoServicioPago" runat="server">
			<!-- INICIATIVA-529 INI -->
			<input id="hidEstadoSicar" type="hidden" name="hidEstadoSicar" runat="server"> 
			<!-- INICIATIVA-529 FIN -->
			<input id="hdnTipoDocumento" type="hidden" name="hdnTipoDocumento" runat="server">
			<input id="hdnRutaLog" type="hidden" name="hdnRutaLog" runat="server"> <input id="hdnDetalleLog" type="hidden" name="hdnDetalleLog" runat="server">
			<!-- AGREGADO POR TS.JTN-->
			<input id="numeroDocumentoHiden" type="hidden" name="hdnDetalleLog" runat="server">&nbsp; 
			<!-- FIN AGREGADO POR TS.JTN-->
		</form>
	</body>
</HTML>
