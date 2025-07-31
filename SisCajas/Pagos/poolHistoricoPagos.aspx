<%@ Page Language="vb" AutoEventWireup="false" Codebehind="poolHistoricoPagos.aspx.vb" Inherits="SisCajas.poolHistoricoPagos" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>poolHistoricoPagos</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../estilos/est_General.css" rel="styleSheet" type="text/css">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script LANGUAGE="JavaScript">
<!--
	function MM_findObj(n, d) { //v4.01
		var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
		  d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
		if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
		for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
		if(!x && d.getElementById) x=d.getElementById(n); return x;
	}

	function MM_showHideLayers() { //v6.0
		var i,p,v,obj,args=MM_showHideLayers.arguments;
		for (i=0; i<(args.length-2); i+=3) if ((obj=MM_findObj(args[i]))!=null) { v=args[i+2];
		if (obj.style) { obj=obj.style; v=(v=='show')?'visible':(v=='hide')?'hidden':v; }
		obj.visibility=v; }
	}

	function f_Buscar() {
		if (f_Validar()) {
			document.frmPrincipal.submit();
		}
	}

	function f_CambiaTipo(){
		document.frmPrincipal.txtNumDocumento.value='';
	}

	function f_Validar(){
		if (!ValidaCombo('document.frmPrincipal.cboTipDocumento','el campo tipo de Doc. de Identidad ',true)) return false;
		switch (parseInt(document.frmPrincipal.cboTipDocumento.options[document.frmPrincipal.cboTipDocumento.selectedIndex].value)) {
			case 1 : if (!ValidaDNI('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;	break;
			case 2 : if (!ValidaFP('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;	break;
			case 3 : if (!ValidaFA('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;	break;
			case 4 : if (!ValidaEXTR('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;	break;
			case 6 : if (!ValidaRUC('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;	break;
			case 7 : if (!ValidaPAS('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;	break;
			default:if (!ValidaAlfanumerico('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',true)) return false;break;
		}
		if (!ValidaFechaA('document.frmPrincipal.txtFecha',false)) return false;
		if (!FechaMayorSistema('document.frmPrincipal.txtFecha',' El campo fecha desde ')) return false;
		if (!FechaMayorSistema('document.frmPrincipal.txtFecha2',' El campo fecha hasta ')) return false;
		return true;
	}

	function textCounter(obj) {
		var maximo;
		switch (parseInt(document.frmPrincipal.cboTipDocumento.options[document.frmPrincipal.cboTipDocumento.selectedIndex].value)) {
			case 1 : maximo = 8;	break;
			case 2 : maximo = 10;	break;
			case 3 : maximo = 10;	break;
			//@@@ BEGIN
			//Responsable		: Narciso Lema Ch.
			//Modificación	:	Requerimiento PMO. Registro de Clientes - Carné de Extranjería.
			case 4 : maximo = 9;	break;
			//@@@ END
			case 6 : maximo = 11;	break;
			case 7 : maximo = 10;	break;
			default: maximo = 15;	break;
		}
		if (obj.value.length > maximo) // if too long...trim it!
			obj.value = obj.value.substring(0, maximo); // otherwise, update 'characters left' counter
	}

	function Imprimir()
	{
		var objIframe = document.getElementById("IfrmImpresion");
		window.open(objIframe.contentWindow.location);
	}

	function f_Imprimir(){
	
			var strCodSAP = "";			
			var strCodSunat = "";
			var strDepGar = "";
			var strTipoDoc = "";
			var strClaseFactura = "";
			
			for (var i = 1; i < dgPool.rows.length; i++){
					if (dgPool.rows[i].cells[0].children.rbPagos.checked){
						strCodSAP = dgPool.rows[i].cells[0].children.rbPagos.value;
						strDepGar = dgPool.rows[i].cells[0].children.rbPagos.DepositoGar;
						strTipoDoc = dgPool.rows[i].cells[0].children.rbPagos.TipoDoc;
						strCodSunat = dgPool.rows[i].cells[5].innerText;						
						//Clase Factura
						strClaseFactura = dgPool.rows[i].cells[4].innerText;
						break;
					}
			}
			
			if (strCodSAP==""){
				alert("Seleccione alguna transacción..!!");
				return;
			}
				
			strClaseFactura = trim(strClaseFactura);
			var strNotaCanje = '<%= ConfigurationSettings.AppSettings("DesClaseNotaCanje")%>';
			if (strClaseFactura == strNotaCanje) 
			{
				
				//PROY-23700-IDEA-29415 - INI
									var objIframe = document.getElementById("IfrmImpresion");
									
									objIframe.style.visibility = "visible";
									objIframe.style.width = 0;
									objIframe.style.height = 0;
				
									objIframe.src = "OperacionesImp.aspx?codRefer="+ strCodSAP+ "&FactSunat="+strCodSunat+"&Reimpresion=1&TipoDoc="+strTipoDoc;
																			
									return true;
			
				//PROY-23700-IDEA-29415 - FIN
				
			}
			
			var objIframe = document.getElementById("IfrmImpresion");
			
			objIframe.style.visibility = "visible";
			objIframe.style.width = 0;
			objIframe.style.height = 0;
			
			if (strTipoDoc=="ZFPA" || strTipoDoc=="G/R"){
				
			}
			else{
				if (strTipoDoc=="0000") {
					objIframe.src = "OperacionesImp_DG.aspx?numDepGar="+ strDepGar;
				}
				else {
					objIframe.src = "OperacionesImp.aspx?codRefer="+ strCodSAP+ "&FactSunat="+strCodSunat+"&Reimpresion=1&TipoDoc="+strTipoDoc;
				}				
			}
	
	/*var valores;
		if(document.frmPrincipal.AUXi.value > 0){	
		    valores=f_Value();
		  	if (valores[0] > 0){
				var objIframe = document.getElementById("IfrmImpresion");
			
				objIframe.style.visibility = "visible";
				objIframe.style.width = 0;
				objIframe.style.height = 0;
			
				objIframe.contentWindow.location.replace('OperacionesImp.asp?codRefer=' + valores[0]  + '&Reimpresion=1&TipoDoc=' + valores[1] + '&FactSunat=' + valores[2]);
			
				//openwindowscroll('OperacionesImp.asp?codRefer=' + valores[0]  + '&Reimpresion=1&TipoDoc=' + valores[1] + '&FactSunat=' + valores[2],'Impresión',500,800,100,100);
			}else{
				alert("Debe Seleccionar un Documento a Imprimir");
			}
		}else{	
			alert("No Hay Documentos para Imprimir")
		}*/
	}

	function f_Value() {
		//var rval, ff;
		var rval = new Array(2);
		var ff;
		
		ff = document.frmPrincipal.AUXi.value
		rval[0]="0";
		rval[1]="0";
		if(ff == 1){
				if (document.frmPrincipal.rbPagosC.checked) {
				rval[0] = document.frmPrincipal.rbPagosC.value;
				rval[1] = document.frmPrincipal.txttipo.value;
				rval[2] = document.frmPrincipal.txtNumRefSunat.value;
				}
		}
		else{
			for (x = 0; x < document.frmPrincipal.rbPagosC.length; x++) {

				if (document.frmPrincipal.rbPagosC[x].checked) {
					rval[0] = document.frmPrincipal.rbPagosC[x].value;
					rval[1] = document.frmPrincipal.txttipo[x].value;
					rval[2] = document.frmPrincipal.txtNumRefSunat[x].value;
				}
			}
		}
		return rval;
	}
	
	function f_Anterior() {
    var pagina;
    pagina=parseInt(document.frmPrincipal.BuscaPagina.value);
    pagina=pagina-1;
    if (pagina<0){
       pagina=1 ;
    }
    document.frmPrincipal.accionPago.value = "3"
	document.frmPrincipal.action='Operaciones.asp';
	document.frmPrincipal.codOperacion.value   = "01";
	document.frmPrincipal.BuscaPagina.value=pagina;
	document.frmPrincipal.submit();
	}

	function f_Siguiente() {
    var pagina;
    
    pagina=parseInt(document.frmPrincipal.BuscaPagina.value);
    pagina=pagina+1;
    document.frmPrincipal.accionPago.value = "3"
	document.frmPrincipal.action='Operaciones.asp';
	document.frmPrincipal.codOperacion.value  = "01";
	document.frmPrincipal.BuscaPagina.value=pagina;
	document.frmPrincipal.submit();
}
//-->
		</script>
		<meta http-equiv="pragma" content="no-cache">
	</HEAD>
	<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0">
		<form name="frmPrincipal" id="frmPrincipal" method="post" runat="server">
			<input type="hidden" name="BuscaPagina" value="1">
			<table width="975" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td width="10" valign="top"></td>
					<td width="810" valign="top">
						<table width="100%" border="0" cellspacing="0" cellpadding="0" name="Contenedor">
							<tr>
								<td align="center" height="10"></td>
							</tr>
						</table>
						<table width="100%" border="1" cellspacing="0" cellpadding="0" name="Contenedor" align="center"
							bordercolor="#336699">
							<tr>
								<td align="center">
									<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
										<tr>
											<td width="11" valign="top" height="32"></td>
											<td width="807" height="32" align="center" class="TituloRConsulta" valign="top" style="PADDING-TOP:4px">Consulta 
												Histórica de Pagos</td>
											<td valign="top" width="9" height="32"></td>
										</tr>
									</table>
									<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
										<tr>
											<td valign="top" width="14"></td>
											<td width="98%" style="PADDING-RIGHT:5px; PADDING-LEFT:5px; PADDING-BOTTOM:5px; PADDING-TOP:5px">
												<table border="0" cellspacing="2" cellpadding="0" align="center" width="90%">
													<tr>
														<td class="Arial12B" colspan="4"></td>
													</tr>
												</table>
												<table width="780" border="0" cellspacing="0" cellpadding="0" align="center">
													<tr>
														<td>
															<div style="BORDER-RIGHT:1px; BORDER-TOP:1px; OVERFLOW-Y:scroll; OVERFLOW-X:scroll; BORDER-LEFT:1px; WIDTH:775px; BORDER-BOTTOM:1px; HEIGHT:330px; TEXT-ALIGN:center"
																class="frame2">
																<!--<div  style="position:relative; overflow-x:visible; overflow-y:scroll; text-align:center; border = 1; z-index:2; height: 176px; width: 775px; left: 0px; top: 0px;" class="frame2"> -->
																<asp:DataGrid id="dgPool" runat="server" CssClass="Arial11B" AutoGenerateColumns="False" Width="1200px"
																	CellSpacing="1" BorderColor="White">
																	<AlternatingItemStyle HorizontalAlign="Center" Height="30px" BackColor="#DDDEE2"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Center" Height="30px" BackColor="#E9EBEE"></ItemStyle>
																	<HeaderStyle HorizontalAlign="Center" CssClass="Arial12B"></HeaderStyle>
																	<Columns>
																		<asp:TemplateColumn HeaderText="Opci&#243;n" ItemStyle-HorizontalAlign="Center">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																			<ItemTemplate>
																				<INPUT id="rbPagos" name="rbPagos" type="radio" value='<%# DataBinder.Eval(Container,"DataItem.VBELN") %>'  DepositoGar='<%# DataBinder.Eval(Container,"DataItem.PEDIN_NROPEDIDO") %>' TipoDoc='<%# DataBinder.Eval(Container,"DataItem.FKART") %>'>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="NAME1" HeaderText="Nombre del Cliente">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="15%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="TOTAL" HeaderText="Total" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="SALDO" HeaderText="Saldo" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="DCORTA" HeaderText="Tipo">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="XBLNR" HeaderText="Fact. SUNAT">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="FKDAT" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="8%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="VBELN" HeaderText="Doc. SAP">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="8%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="DEPEN_NROCUOTA" HeaderText="Cuota">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="WAERK" HeaderText="Moneda">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="NETWR" HeaderText="Neto">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="MWSBK" HeaderText="Impuesto">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PAGOS" HeaderText="Pago" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="DEPEV_NROTELEFONO" HeaderText="Núm Telefónico">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="9%"></HeaderStyle>
																			<ItemStyle Height="29px"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="DEPEV_DESCRIPCIONLP" HeaderText="Utilizaci&#243;n">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="8%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PEDIN_NROPEDIDO" HeaderText="NROPEDIDO">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																	</Columns>
																</asp:DataGrid>
															</div>
														</td>
													</tr>
												</table>
											</td>
											<td valign="top" width="14" align="right"></td>
										</tr>
									</table>
									<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
										<tr>
											<td height="4"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<table width="800" border="1" cellspacing="0" cellpadding="0" align="center" bordercolor="#336699">
							<tr>
								<td align="center">
									<table width="100%" border="0" cellspacing="1" cellpadding="0">
										<tr>
											<td align="center" width="28"></td>
											<td align="center" width="85"><INPUT type="button" value="Imprimir" id="btnImprimir" name="btnImprimir" class="BotonOptm"
													style="WIDTH:98px" onclick="f_Imprimir()"></td>
											<td align="center" width="28"></td>
											<td align="center" width="100" valign="middle">
												<asp:DropDownList id="cboTipDocumento" runat="server" CssClass="clsSelectEnable"></asp:DropDownList>
											</td>
											<td align="center" width="28"></td>
											<td align="center" width="100" valign="middle">
												<asp:TextBox id="txtNumDocumento" runat="server" CssClass="clsInputEnable" Width="97px"></asp:TextBox></td>
											<td align="center" width="50"></td>
											<td align="center" width="28" class="clsArial10a">Desde:
											</td>
											<td align="center" width="160" valign="middle">
												<asp:TextBox id="txtFecha" tabIndex="34" runat="server" CssClass="clsInputEnable" Width="67px"
													MaxLength="10"></asp:TextBox>
												<a href="javascript:show_calendar('frmPrincipal.txtFecha');" onMouseOut="window.status='';return true;"
													onMouseOver="window.status='Date Picker';return true;"><img border="0" src="../../images/botones/btn_Calendario.gif"></a>
											</td>
											<td align="center" width="28" class="clsArial10a">Hasta:
											</td>
											<td align="center" width="160" valign="middle">
												<asp:TextBox id="txtFecha2" runat="server" CssClass="clsInputEnable" Width="67px" MaxLength="10"></asp:TextBox>&nbsp;
												<a href="javascript:show_calendar('frmPrincipal.txtFecha2');" onMouseOut="window.status='';return true;"
													onMouseOver="window.status='Date Picker';return true;"><img border="0" src="../../images/botones/btn_Calendario.gif"></a>
											</td>
											<td align="center" width="28"></td>
											<td align="center" width="85"><INPUT type="button" value="Buscar" id="btnBuscar" name="btnBuscar" class="BotonOptm" style="WIDTH:98px"
													onclick="f_Buscar()"></td>
											<td align="center" width="28"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<table width="150" align="center" cellpadding="4" cellspacing="0" class="tabla_borde">
							<tr class="Arial12B">
								<td align="center">Página: 1 de 1
								</td>
							</tr>
						</table>
						<table>
							<tr>
								<td height="5"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<script language="JavaScript" type='text/javascript'></script>
			<script language="JavaScript" type="text/javascript">
var esNavegador, esIExplorer;

function e_mayuscula() {
	if ((event.keyCode>96&&event.keyCode<123)||(event.keyCode==241)||(event.keyCode==250)||(event.keyCode==243)||(event.keyCode==237)||(event.keyCode==233)||(event.keyCode==225))
		event.keyCode=event.keyCode-32;
}

esNavegador = (navigator.appName == "Netscape") ? true : false;
esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

if (esIExplorer) {
	window.document.frmPrincipal.txtNumDocumento.onkeypress=e_mayuscula;
}

			</script>
			<iframe id="IfrmImpresion" name="IfrmImpresion" src="#" style="VISIBILITY:hidden;WIDTH:0px;HEIGHT:0px">
			</iframe>
		</form>
	</body>
</HTML>
