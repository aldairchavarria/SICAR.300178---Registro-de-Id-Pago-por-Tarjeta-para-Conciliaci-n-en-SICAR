<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PoolPagos.aspx.vb" Inherits="SisCajas.PoolPagos" enableViewState="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Sistema de Cajas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script type="text/javascript" src="../librerias/Lib_FuncGenerales.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
		
		window.onload = window_onload;							
		
		function window_onload() {			
			if (frmPrincipal.txtpImp.value!=""){
				if (frmPrincipal.txtpImp.value=="S") {
					f_Imprimir();
					frmPrincipal.action="PoolPagos.aspx"
					frmPrincipal.txtDocSap.value=""			
				}
				else{
					//AbreCajonera();
				}				
			}			
		}
                //PROY-140335 INI
		function f_habilitarDiv(){
		document.getElementById("divTitulo").style.display ="block"
		}
		function f_ProcesarPago(){
		f_habilitarDiv();
		setValue('txtsession',0);
		var nroPedido= frmPrincipal.txtRbPagos.value;
	 
		for (var i = 1; i < dgPool.rows.length; i++){
		var nroPedidoDg = dgPool.rows[i].cells[0].children.rbPagos.value;
		if(nroPedidoDg==nroPedido){
		dgPool.rows[i].cells[0].children.rbPagos.checked=true;
		
			 break;
				}
		}
	         document.frmPrincipal.cmdProcesarPago.click();
                }
                 //PROY-140335 FIN
		
	  function f_Compensar()
	{
		event.returnValue=false;
		var strCod = "";					
		var strEstado="";
		var strCodProcesar = "";
		var numroSunat = "";
		for (var i = 1; i < dgPool.rows.length; i++){
				if (dgPool.rows[i].cells[0].children.rbPagos.checked){
					strCod = dgPool.rows[i].cells[0].children.rbPagos.value;
					strEstado =dgPool.rows[i].cells[17].innerText;	//PROY-30166 -IDEA-38863 - INICIO
					strCodProcesar=dgPool.rows[i].cells[18].children.IdCodigo.value;//PROY-30166 
					numroSunat = dgPool.rows[i].cells[7].innerText; //PROY-30166 -IDEA-38863 - FIN
					break;
				}
		}
		strCod =trim(strCod);
		strEstado =trim(strEstado.toLowerCase());
			
				if (strCod == "") {
						document.frmPrincipal.action="";
						alert("Debe Seleccionar un Documento para Compensar el Pago");
						return false;
				}
				else
				{			
					//strEstado =trim(strEstado.toLowerCase());					
					if ((strEstado == "procesado") && (dgPool.rows[i].cells[7].innerText != "" || dgPool.rows[i].cells[6].innerText != "0000000000000000")) //PROY-30166 -IDEA-38863 
					{
							frmPrincipal.txtRbPagos.value = strCod;
							frmPrincipal.hidAccion.value= "COM";
							frmPrincipal.submit();
							return true;
					}
					else
					{
						alert('No es posible Compensar, doc sin N° SAP');
						return false;
					}
			}
	}
	
	function Imprimir(){
		var objIframe = document.getElementById("IfrmImpresion");
		window.open(objIframe.contentWindow.location);
	}
	
	function f_Imprimir(){
	<%Session("PoolPagados") = "" %>
			var strCodSAP = frmPrincipal.txtDocSap.value;			
			var strCodSunat = frmPrincipal.txtDocSunat.value;
			var strDepGar = frmPrincipal.txtNroDG.value;
			var strTipoDoc = frmPrincipal.txtTipDoc.value;
			var objIframe = document.getElementById("IfrmImpresion");
			
			var strEfectivo = frmPrincipal.txtEfectivo.value;
			var strRecibido = frmPrincipal.txtRecibido.value;
			var strEntregar = frmPrincipal.txtEntregar.value;	
			var flagVentaEquipoPrepago = frmPrincipal.flagVentaEquipoPrepago.value;	
			
			///JTN  INICIO
			var isOffline = frmPrincipal.txtOffline.value;
			///JTN FIN
			
			objIframe.style.visibility = "visible";
			objIframe.style.width = 0;
			objIframe.style.height = 0;
			
			
			//alert(strCodSAP);
			
			
			
			if (strTipoDoc=="ZFPA" || strTipoDoc=="G/R"){
			}
			else{
				if (strTipoDoc=="DG") {
					objIframe.src = "OperacionesImp_DG.aspx?numDepGar="+ strCodSAP;
				}
				else {
					//descomentra el llamado...
					//alert("from pool: OperacionesImp.aspx?codRefer="+ strCodSAP+ "&FactSunat="+strCodSunat+"&TipoDoc="+strTipoDoc  + "&strEfectivo=" + strEfectivo + "&strRecibido=" + strRecibido + "&strEntregar=" + strEntregar + "&flagVentaEquipoPrepago=" + flagVentaEquipoPrepago + "&isOffline=" + isOffline);
					objIframe.src = "OperacionesImp.aspx?codRefer="+ strCodSAP+ "&FactSunat="+strCodSunat+"&TipoDoc="+strTipoDoc  + "&strEfectivo=" + strEfectivo + "&strRecibido=" + strRecibido + "&strEntregar=" + strEntregar + "&flagVentaEquipoPrepago=" + flagVentaEquipoPrepago + "&isOffline=" + isOffline;
				}
			}
		}
		
		function f_Reasignacion()
		{
		   event.returnValue = false;
		   //AGREGADO POR FFS INICIO
		   	var strCod = "";			
			var strEstado="";
			var strCodProcesar = "";
			
			for (var i = 1; i < dgPool.rows.length; i++){
					if (dgPool.rows[i].cells[0].children.rbPagos.checked){
						strCod = dgPool.rows[i].cells[0].children.rbPagos.value;
						strEstado =dgPool.rows[i].cells[17].innerText; //PROY-30166 -IDEA-38863 
						strCodProcesar=dgPool.rows[i].cells[18].innerText; //PROY-30166 -IDEA-38863 
						strCodProcesar=dgPool.rows[i].cells[18].children.IdCodigo.value; //PROY-30166 -IDEA-38863  
						break;
					}
			}
			strCod =trim(strCod);
			strEstado =trim(strEstado.toLowerCase());
			//AGREGADO POR FFS FIN	
			//MODIFICADO POR FFS INICIO
			
		   if (f_Boton())
		   {				
				if (strEstado == "procesado")
					{
						frmPrincipal.txtRbPagos.value = strCod;
								
							for (i=1;i<dgPool.rows.length;i++)
							{
								if (dgPool.rows[i].cells[0].children.rbPagos.checked)
								{
									if (dgPool.rows[i].cells[16].children.PEDIC_CLASEFACTURA.value == "<%= configurationsettings.appsettings("cteTIPODOC_DEPOSITOGARANTIA")%>" ) //PROY-30166 -IDEA-38863 
									{
										alert('No se puede realizar Reasignación con este documento.');
										return false;
									}
						             
									if (dgPool.rows[i].cells[7].innerText == "" || dgPool.rows[i].cells[7].innerText == "0000000000000000") //PROY-30166 -IDEA-38863 
									{
										alert('Error documento no tiene asignado Número Sunat');
										return false;
									} 
									else
									{
										event.returnValue = true;
										return true;
									}
								  
								}
							}		      
						}
						else
						{
							alert('No es posible Reasignar, doc sin N° SAP');
							return false;
						}
             //return false;
	    }
	    //MODIFICADO POR FFS FIN
		}
		
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<iframe id="IfrmImpresion" style="VISIBILITY: hidden; WIDTH: 0px; HEIGHT: 0px" name="IfrmImpresion"
			src="#"></iframe>
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
															height="32">Pool de Documentos por Pagar</td>
														<td vAlign="top" width="9" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="780" align="center" border="0">
													<tr>
														<td>
															<div class="frame2" style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 775px; BORDER-BOTTOM: 1px; HEIGHT: 330px; TEXT-ALIGN: center"><asp:datagrid id="dgPool" runat="server" Height="30px" CssClass="Arial11B" BorderColor="White"
																	CellSpacing="1" Width="1200px" AutoGenerateColumns="False">
																	<AlternatingItemStyle HorizontalAlign="Center" Height="21px" BackColor="#DDDEE2"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Center" Height="21px" BackColor="#E9EBEE"></ItemStyle>
																	<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
																	<Columns>
																		<asp:TemplateColumn HeaderText="Opci&#243;n" ItemStyle-HorizontalAlign="Center">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																			<ItemTemplate>
																				<input id=rbPagos type=radio value='<%# DataBinder.Eval(Container,"DataItem.PEDIN_NROPEDIDO")  %>'  name=rbPagos  >
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="PEDIV_NOMBRECLIENTE" HeaderText="Nombre del Cliente">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="15%"></HeaderStyle>
																			<ItemStyle Height="29px"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="INPAN_TOTALDOCUMENTO" HeaderText="Importe" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PAGON_INICIAL" HeaderText="Cuota Inicial" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PEDIN_SALDO" HeaderText="Saldo" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PEDIV_DESCCLASEFACTURA" HeaderText="Tipo">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PAGOC_CODSUNAT" HeaderText="Fact. SUNAT">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PEDID_FECHADOCUMENTO" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="8%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PEDIN_PEDIDOSAP" HeaderText="Doc. SAP Ref">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="8%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="DEPEV_DESCRIPCIONLP" HeaderText="Utilizaci&#243;n">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="8%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="DEPEN_NROCUOTA" HeaderText="Cuota">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PEDIC_MONEDA" HeaderText="Moneda">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="INPAN_TOTALMERCADERIA" HeaderText="Neto" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="INPAN_TOTALIMPUESTO" HeaderText="Impuesto" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="INPAN_TOTALDOCUMENTO" HeaderText="Pago" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:TemplateColumn HeaderStyle-BackColor="#ffffff" ItemStyle-BackColor="#ffffff" ItemStyle-ForeColor="#ffffff">
																			<HeaderStyle Width="0%" BorderWidth="0" BorderStyle="None"></HeaderStyle>
																			<ItemTemplate>
																				<INPUT id=PEDIN_PEDIDOALTA style="WIDTH: 1px; HEIGHT: 22px" type=hidden size=1 value='<%# DataBinder.Eval(Container,"DataItem.PEDIN_PEDIDOALTA") %>'>
																				<INPUT id=PEDIC_CLASEFACTURA style="WIDTH: 1px; HEIGHT: 22px" type=hidden size=1 value='<%# DataBinder.Eval(Container,"DataItem.PEDIC_CLASEFACTURA") %>'>
                                                                                                                                                                 <%--PROY-140335-INI--%>
																			         <INPUT id=PEDIC_PORTA style="WIDTH: 1px; HEIGHT: 22px" type=hidden size=1 value='<%# DataBinder.Eval(Container,"DataItem.PEDIC_PORTA") %>'>
                                                                                                                                                                  <%--PROY-140335-FIN--%>
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
																		<asp:BoundColumn DataField="DEPEV_NROTELEFONO" HeaderText="Núm Telefónico">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="9%"></HeaderStyle>
																			<ItemStyle Height="29px"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PEDIN_NROPEDIDO" HeaderText="NROPEDIDO">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<%--PROY-140397-MCKINSEY JSQ INICIO--%>
																		<asp:BoundColumn DataField="TIPO_ENTREGA" HeaderText="Tipo Entrega">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<%--PROY-140397-MCKINSEY JSQ FIN--%>
																		<%--<asp:TemplateColumn>
																			<HeaderStyle Width="0%" BorderWidth="0" BorderStyle="None"></HeaderStyle>
																			<ItemStyle ForeColor="White" BackColor="White"></ItemStyle>
																			<ItemTemplate>
																				<INPUT id="IdCodigo" name="IdCodigo" style="WIDTH: 1px; HEIGHT: 22px" type="hidden" size="1" value='<%# DataBinder.Eval(Container,"DataItem.ID_T_TRS_PEDIDO")   %>
																		<%--'>
																			</ItemTemplate>
																		</asp:TemplateColumn>--%>
																		<%-- 
																		'----------------------------------------------------------------------------
																		'--RMZ INICIO - PROY-32280 - PICKING -- 
																		'----------------------------------------------------------------------------
																		--%>
																		<asp:TemplateColumn Visible="False">
																			<HeaderStyle BorderWidth="0px" BorderStyle="None" Width="0%"></HeaderStyle>
																			<ItemTemplate>
																				<INPUT id=FLAG_MULTIPUNTO style="WIDTH: 0; HEIGHT: 0" type=hidden size=0 runat="server" value='<%# DataBinder.Eval(Container,"DataItem.FLAG_MULTIPUNTO")%>'>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<%-- 
																		'----------------------------------------------------------------------------
																		'--RMZ FIN    - PROY-32280 - PICKING -- 
																		'----------------------------------------------------------------------------
																		--%>
																	</Columns>
																</asp:datagrid></div>
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
														<td align="center" width="60"><asp:button id="btnPagar" runat="server" CssClass="BotonOptm" Width="100px" Text="Pagos"></asp:button></td>
														<td align="center" width="13"></td>
														<td align="center" width="1" style="DISPLAY:none">
															<asp:button id="btnCompensar" runat="server" CssClass="BotonOptm" Width="100px" Text="Compensar"
																Visible="False"></asp:button>
														</td>
														<td align="center" width="13" style="DISPLAY:none"></td>
														<td align="center" width="1" style="DISPLAY:none"><asp:button id="btnReasignar" runat="server" CssClass="BotonOptm" Width="100px" Text="Reasignación"
																Visible="False"></asp:button></td>
														<td align="center" width="13" style="DISPLAY:none"></td>
														<td align="center" width="85"><INPUT class="BotonOptm" id="btnAnular" title="Se elimina el documento seleccionado" style="WIDTH: 100px"
																onclick="f_Anular();" type="button" value="Anular" name="btnAnular"></td>
														<td align="center" width="74" style="WIDTH: 60px"></td>
														<!-- INCIDENCIA MEJORA SICAR - INI -->
														<td align="center" width="100" valign="middle">
															<asp:DropDownList id="cboTipDocumento" runat="server" CssClass="clsSelectEnable"></asp:DropDownList>
														</td>
														<td align="center" width="28"></td>
														<td align="center" width="100" valign="middle">
															<asp:TextBox id="txtNumDocumento" runat="server" CssClass="clsInputEnable" Width="97px"></asp:TextBox></td>
														<td align="center" width="50"></td>
														<!-- INCIDENCIA MEJORA SICAR - FIN -->
														<!-- PROY - MCKINSEY - JSQ INICIO -->
														<td class="Arial12b" width="50">&nbsp;Desde :</td>														
														<!-- PROY - MCKINSEY -JSQ FIN --->
														<td align="center" width="1" style="DISPLAY:none"><INPUT class="BotonOptm" id="btnImpAcuerdo" style="WIDTH: 100px; VISIBILITY: hidden" type="button"
																value="Acuerdo Alquiler" name="btnImpAcuerdo"></td>
														<td align="center" width="13" style="DISPLAY:none"></td>
														<td vAlign="middle" align="center" width="100">
															<asp:textbox id="txtFecha" runat="server" CssClass="clsInputEnable" Width="62px" MaxLength="10"></asp:textbox>&nbsp;<A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmPrincipal.txtFecha');"><IMG src="../../images/botones/btn_Calendario.gif" border="0"></A>
														</td>
														<td align="center" width="85">
															<INPUT class="BotonOptm" id="btnBuscar" style="WIDTH: 100px" onclick="f_Buscar();" type="button"
																value="Buscar" name="btnBuscar"></td>
														<td align="center" width="13"></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
                        <!-- PROY-140335-INI -->
			<div style="DISPLAY: none" id="divTitulo">
				<table border="0" cellSpacing="2" cellPadding="0" width="50" align="LEFT">
					<tr>
						<td class="TituloRConsulta" width="28" align="center">Procesando...</td>
					</tr>
				</table>
			</div>
                         <!-- PROY-140335-FIN -->
						<p style="DISPLAY: none">
							<asp:button id="cmdAnular" runat="server" Text="Anular"></asp:button>
							<asp:button id="cmdProcesar" runat="server" Text="ProcesarPagos"></asp:button>
                                                       <!-- PROY-140335-INI -->
                                                       </asp:button><asp:button id="cmdProcesarSP" runat="server" Text=""></asp:button>
                                                       <asp:button id="cmdProcesarPago" runat="server" Text=""></asp:button>
                                                       <!-- PROY-140335-FIN -->					
							<INPUT id="txtRbPagos" type="hidden" name="txtRbPagos" runat="server">
							<asp:textbox id="txtDocSap" runat="server"></asp:textbox><asp:textbox id="txtDocSunat" runat="server"></asp:textbox><asp:textbox id="txtNroDG" runat="server"></asp:textbox><asp:textbox id="txtTipDoc" runat="server"></asp:textbox><asp:textbox id="txtpImp" runat="server"></asp:textbox>
							<asp:TextBox id="txtOffline" runat="server"></asp:TextBox></p>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtEfectivo" name="txtEfectivo" runat="server"> <input type="hidden" id="txtRecibido" name="txtRecibido" runat="server">
			<input type="hidden" id="txtEntregar" name="txtEntregar" runat="server"> <input type="hidden" id="flagVentaEquipoPrepago" name="flagVentaEquipoPrepago" runat="server">
			<input type="hidden" id="txtsession" name="txtsession" runat="server"> <input type="hidden" id="hidAccion" name="hidAccion" runat="server">
			<input type="hidden" id="hdnBuscarPool" name="hdnBuscarPool" runat="server" value="0">
		</form>
		<script language="javascript">

  function f_Boton()
  { 
	var rval = "";
	var strEstado = "";
	
    for (var i = 1; i < dgPool.rows.length; i++){
		if (dgPool.rows[i].cells[0].children.rbPagos.checked){
         //AGREGADO POR FFS INICIO
           rval = dgPool.rows[i].cells[0].children.rbPagos.value;           
           break;
           //AGREGADO POR FFS FIN
         }  
       }
     //alert(rval);
    if (rval == "")
    {
      alert('Debe Seleccionar un Documento de Pago');
      return false;
    }
    else		
      return true;
  }
  
  function f_Pagos()
  {  
	event.returnValue = false; 
	
	var strCod = "";			
	var strEstado="";
	var strCodProcesar = "";
	var strAuart="";
	var strCorrelativoSunat="";
	var strFlagPorta=""; //PROY-140335
	for (var i = 1; i < dgPool.rows.length; i++){
			if (dgPool.rows[i].cells[0].children.rbPagos.checked){
			//alert(dgPool.rows[i].cells[0].children.rbPagos);
				strCod = dgPool.rows[i].cells[0].children.rbPagos.value;
				//alert(dgPool.rows[i].cells[13].innerText);
				strEstado =dgPool.rows[i].cells[14].innerText; //PROY-30166 -IDEA-38863 
				strCorrelativoSunat =dgPool.rows[i].cells[6].innerText; //PROY-30166 -IDEA-38863 
				strCodProcesar= "00000"; //dgPool.rows[i].cells[16].children.IdCodigo.value;
				strAuart=dgPool.rows[i].cells[15].children.PEDIC_CLASEFACTURA.value;	 //PROY-30166 -IDEA-38863 
				//alert(strAuart);	
				strFlagPorta= dgPool.rows[i].cells[15].children.PEDIC_PORTA.value; //PROY-140335	
				break;				
			}
	}
	
	if (strCod==""){
		alert('Seleccione un pedido para continuar con el proceso de pago.');
		return;
	}
	
	if(strAuart== "0007"){
	
		//if(strCorrelativoSunat != "0000000000000000"){
		//	alert('El pedido seleccionado se encuentra pagado, correlativo SUNAT: ' + strCorrelativoSunat);
		//	return;
		//} 
	}
	
	strCod =trim(strCod);
	strEstado =trim(strEstado.toLowerCase());
	strCodProcesar =trim(strCodProcesar);
			
    if (f_Boton())
    {			
					//if (strEstado == "procesado")
					//{
						frmPrincipal.txtRbPagos.value = strCod;
						//frmPrincipal.txtDocSap.
						//siempre que es recarga virtual es 00
							
						if (f_DepositoG())
							{
								// mi comentario
								window.frmPrincipal.hidVerif.value = 1;
								if ((strAuart == "ZPBR" || strAuart == "ZPVR") && strEstado!="procesado")
								{
								setValue('txtsession',1);
								frmPrincipal.txtRbPagos.value=strCodProcesar;
								}
								else{
									setValue('txtsession',0);
								}
                                                                //PROY-140335-INI
								 if(strFlagPorta== "P"){ 
								event.returnValue = false;
								document.frmPrincipal.cmdProcesarSP.click();
								
								return;
								 }else{
								event.returnValue = true;
								return true;
							}
								
							}
                                                    //PROY-140335-FIN
						else
							{
								alert('Primero debe pagar el Depósito de Garantía asociado');
								window.frmPrincipal.hidVerif.value = 0;
								return false;
							}      
							return;
							
					/*}
					else
					{
							answer = confirm ("Este Documento requiere ser procesado para continuar con la operación ¿Desea procesar el Documento?");
							
							if (answer) {					
								frmPrincipal.txtRbPagos.value = strCodProcesar;
								frmPrincipal.cmdProcesar.click();
								return true;						
							}
							else{								
								return false;
							}
					}*/
    }
    else
    {
    return false;
    }
  }
    
  function f_DepositoG()
  {
     
     if (f_ValueFKART() == "DG")
       return true;
     
     if (parseFloat(f_ValueNRO_DEP_GARANTIA()) != 0) {
			/*if (f_ValueNRO_REF_DEP_GAR() != "") { //TODO
				return true;
			}
			else{
				return false;
			}*/return true;
		}
		else{
			return true;
		}
  }  
    
  function f_ValueNRO_DEP_GARANTIA() {
		var rval, ff;
        ff = document.frmPrincipal.rbPagos.length;
		rval="xxx";
		if(ff == 1){
				if (document.frmPrincipal.rbPagos.checked) {
				rval = document.frmPrincipal.PEDIN_PEDIDOALTA.value;
				}
		}
		else{
			for(i=0; i<document.frmPrincipal.PEDIN_PEDIDOALTA.length; i++){
			   if (document.frmPrincipal.rbPagos[i].checked){
			       rval=document.frmPrincipal.PEDIN_PEDIDOALTA[i].value;
			       break;
			   }
			}
		}

		return rval;
	}

	function f_ValueNRO_REF_DEP_GAR() {
		var rval, ff;
        ff = document.frmPrincipal.rbPagos.length;
		rval="xxx";
		if(ff == 1){
				if (document.frmPrincipal.rbPagos.checked) {
				rval = document.frmPrincipal.NRO_REF_DEP_GAR.value;
				}
		}
		else{
			for(i=0; i<document.frmPrincipal.NRO_REF_DEP_GAR.length; i++){
			   if (document.frmPrincipal.rbPagos[i].checked){
			       rval=document.frmPrincipal.NRO_REF_DEP_GAR[i].value;
			       break;
			   }
			}
		}
		return rval;
	}
    
  function f_ValueFKART() {
		var rval, ff;
        ff = document.frmPrincipal.rbPagos.length;
		rval="xxx";
		if(ff == 1){
				if (document.frmPrincipal.rbPagos.checked) {
				rval = document.frmPrincipal.PEDIC_CLASEFACTURA.value;
				}
		}
		else{
			for(i=0; i<document.frmPrincipal.PEDIC_CLASEFACTURA.length; i++){
			   if (document.frmPrincipal.rbPagos[i].checked){
			       rval=document.frmPrincipal.PEDIC_CLASEFACTURA[i].value;
			       break;
			   }
			}
		}

		return rval;
	}
  
    
  function f_Anular()
  {  
  	var strCod = "";
  	var strEstado="";
  	var strCodProcesar = "";
  	var strAuart="";
  	
  	  	
			for (var i = 1; i < dgPool.rows.length; i++){
					if (dgPool.rows[i].cells[0].children.rbPagos.checked){
						strCod = dgPool.rows[i].cells[0].children.rbPagos.value;
						//alert(dgPool.rows[i].cells[0].children.rbPagos.value);
						
						strEstado =dgPool.rows[i].cells[7].innerText; //PROY-30166 -IDEA-38863 
						//alert(strEstado);
						//strCodProcesar=dgPool.rows[i].cells[17].children.IdCodigo.value;
						strCodProcesar= "00000";	
						//alert(strCodProcesar);
						strAuart=dgPool.rows[i].cells[15].children.PEDIC_CLASEFACTURA.value;	//PROY-30166 -IDEA-38863
						//alert(strAuart);
						break;
					}
			}
		strCod =trim(strCod);
		strEstado =trim(strEstado.toLowerCase());

				if (strCod == "") {
						document.frmPrincipal.action="";
						alert("Debe Seleccionar un Documento para Anular Pago");					
						return false;
				}
				else
				{				
					strEstado =trim(strEstado.toLowerCase());
					/*if (strEstado == "procesado")
					{*/
							frmPrincipal.txtRbPagos.value = strCod;
							
							//inicio
							if ((strAuart == "ZPBR" || strAuart == "ZPVR") && (strEstado!="procesado")){
								setValue('txtsession',1);
								frmPrincipal.txtRbPagos.value=strCodProcesar;
								}
								else{
									setValue('txtsession',0);
							}
							//fin
							
									
							frmPrincipal.cmdAnular.click();
							return true;
					/*}
					else
					{
						alert("Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador");
						return false;
					}*/
					
					/*else
					{
							answer = confirm ("Este Documento requiere ser procesado para continuar con la operación ¿Desea procesar el Documento?");							
							if (answer) {					
								frmPrincipal.txtRbPagos.value = strCodProcesar;
								frmPrincipal.cmdProcesar.click();
								return true;						
							}
							else
							{								
								return false;
							}
					}*/
				}
			//}
	
  } 
  
  function f_Buscar()
  {
	//INCIDENCIA MEJORA SICAR - INI
	if(ValidaCombo('document.frmPrincipal.cboTipDocumento','el campo tipo de Doc. de Identidad ',true) && document.frmPrincipal.txtNumDocumento.value != ''){
		switch (parseInt(document.frmPrincipal.cboTipDocumento.options[document.frmPrincipal.cboTipDocumento.selectedIndex].value)) {
			case 1 : if (!ValidaDNI('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;	break;
			case 2 : if (!ValidaFP('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;	break;
			case 3 : if (!ValidaFA('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;	break;
			case 4 : if (!ValidaEXTR('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;	break;
			case 6 : if (!ValidaRUC('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;	break;
			case 7 : if (!ValidaPAS('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;	break;
			default:if (!ValidaAlfanumerico('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',true)) return false;break;
		}
	}
	if(document.frmPrincipal.txtNumDocumento.value != '')
	{
		if (!ValidaCombo('document.frmPrincipal.cboTipDocumento','el campo tipo de Doc. de Identidad ',true)) return false;
	}
     if(ValidaFechaA('document.frmPrincipal.txtFecha', true))
		{
		    setEnabled('btnBuscar',false,'Boton');
		    frmPrincipal.txtpImp.value = "";
		    frmPrincipal.hdnBuscarPool.value = "1";
			document.frmPrincipal.submit();
		}
  }
  
  function f_CambiaTipo(){
		document.frmPrincipal.txtNumDocumento.value='';
	}
  function textCounter(obj) {
		var maximo;
		switch (parseInt(document.frmPrincipal.cboTipDocumento.options[document.frmPrincipal.cboTipDocumento.selectedIndex].value)) {
			case 1 : maximo = 8;	break;
			case 2 : maximo = 10;	break;
			case 3 : maximo = 10;	break;
			case 4 : maximo = 9;	break;
			case 6 : maximo = 11;	break;
			case 7 : maximo = 10;	break;
			default: maximo = 15;	break;
		}
		if (obj.value.length > maximo)
			obj.value = obj.value.substring(0, maximo);
	}
  //INCIDENCIA MEJORA SICAR - FIN
  
  
		</script>
	</body>
</HTML>
