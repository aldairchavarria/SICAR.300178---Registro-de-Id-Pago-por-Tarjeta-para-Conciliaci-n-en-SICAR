<%@ Page Language="vb" AutoEventWireup="false" Codebehind="compensacion.aspx.vb" Inherits="SisCajas.compensacion"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Aplicativo TIM</title>
		<meta http-equiv="Pragma" content="no-cache">
		<META http-equiv="Expires" content="Mon, 06 Jan 1990 00:00:01 GMT">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
			<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
			<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
			<script language="JavaScript">

<!--

function popup(url) {
	msg= window.open(url,"popi","toolbar=no,left=58,top=50,width=680,height=480,directories=no,status=no,scrollbars=yes,resize=no,menubar=no");
}

function MM_findObj(n, d) { //v4.01
	var p,i,x;
	if (!d) d = document;
	if ((p=n.indexOf("?"))>0&&parent.frames.length) {
		d = parent.frames[n.substring(p+1)].document;
		n = n.substring(0,p);
	}
	if (!(x=d[n])&&d.all) x=d.all[n];
	for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
	for (i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
	if (!x && d.getElementById) x=d.getElementById(n);
	return x;
}

function MM_showHideLayers() { //v6.0
	var i,p,v,obj,args=MM_showHideLayers.arguments;
	for (i=0; i<(args.length-2); i+=3)
		if ((obj=MM_findObj(args[i]))!=null) {
			v=args[i+2];
			if (obj.style) {
				obj=obj.style;
				v=(v=='show')?'visible':(v=='hide')?'hidden':v; 
			}
			obj.visibility=v;
		}
}

function f_Grabar(){ 
	if (f_Validar())
	  return true;
	else
  	  return false;  
	
}

function f_Cancelar(){
	document.frmPrincipal.action='poolPagos.aspx';
	document.frmPrincipal.submit();
}


function f_Validar(){
	if(document.frmPrincipal.txtNroSunat.value.length < 7){
    alert("El Numero Sunat debe tener 7 Digitos")
	return false	
	}
	return true
}


/*function _onload(){
	document.frmPrincipal.cboTipDocumento.focus();
}*/

function f_CambiaTipo(){
	document.frmPrincipal.txtNumDocumento.value='';
}

function textCounter(obj) {
	var maximo;
	switch (parseInt(document.frmPrincipal.cboTipDocumento.options[document.frmPrincipal.cboTipDocumento.selectedIndex].value)) {
		case 6 : 
			maximo = 11;
			break;
		case 1 : 
			maximo = 8;
			break;
		default : 
			maximo = 15;
			break;
	}
	if (obj.value.length > maximo) // if too long...trim it!
		obj.value = obj.value.substring(0, maximo); // otherwise, update 'characters left' counter
}

//-->
			</script>
			<meta http-equiv="pragma" content="no-cache">
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<div id="overDiv" style="Z-INDEX: 1; WIDTH: 100px; POSITION: absolute"></div>
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<input type="hidden" value="1" name="CantPaginas">
			<table cellSpacing="0" cellPadding="0" width="1000" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="790">
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1" name="Contenedor">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10" height="32"></td>
											<td class="TituloRConsulta" vAlign="top" align="center" width="98%" height="32">Compensación 
												de Pagos</td>
											<td vAlign="top" width="14" height="32"></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td width="98%">
												<table class="Arial12B" cellSpacing="2" cellPadding="0" width="82%" align="center" border="0">
													<tr>
														<td>&nbsp;&nbsp;&nbsp;Doc. Sap :</td>
														<td><asp:textbox id="txtDocSap" runat="server" ReadOnly="True" Width="147px" MaxLength="15" CssClass="clsInputDisable"></asp:textbox></td>
														<td>&nbsp;</td>
														<td>&nbsp;&nbsp;&nbsp;Fecha del Doc.:</td>
														<td><asp:textbox id="txtFecha" runat="server" ReadOnly="True" Width="147px" CssClass="clsInputDisable"></asp:textbox></td>
													</tr>
													<tr>
														<td>&nbsp;&nbsp;&nbsp;Factura SUNAT:</td>
														<td><asp:textbox id="txtNumFact" runat="server" ReadOnly="True" Width="147px" MaxLength="15" CssClass="clsInputDisable"></asp:textbox></td>
														<td>&nbsp;</td>
														<td>&nbsp;&nbsp;&nbsp;Saldo :</td>
														<td><asp:textbox id="txtSaldo" runat="server" ReadOnly="True" Width="147px" MaxLength="15" CssClass="clsInputDisable"></asp:textbox></td>
													</tr>
													<tr>
														<td>&nbsp;&nbsp;&nbsp;Nro. Nota Crédito:</td>
														<td><asp:textbox id="txtNroSunat" runat="server" Width="147px" MaxLength="20" CssClass="clsInputEnable"></asp:textbox></td>
														<td>&nbsp;</td>
														<td>&nbsp;</td>
														<td>&nbsp;</td>
													</tr>
													<input type="hidden" name="txtNumSunat">
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
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="400" align="center"
							border="1">
							<tr>
								<td align="center">
									<table cellSpacing="2" cellPadding="0" border="0">
										<tr>
											<td align="center" width="28"></td>
											<td align="center" width="85"><asp:button id="btnGrabar" runat="server" Width="100px" CssClass="BotonOptm" Text="Grabar"></asp:button></td>
											<td align="center" width="28"></td>
											<td align="center" width="85"><asp:button id="btnCancelar" runat="server" Width="100px" CssClass="BotonOptm" Text="Cancelar"></asp:button></td>
											<td align="center" width="28"></td>
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

function e_mayuscula() {
	if (event.keyCode>96&&event.keyCode<123)
		event.keyCode=event.keyCode-32;
}

function e_numero() {
	if ( !( (event.keyCode>=48) && (event.keyCode<=57)) && !(event.keyCode==13) )
		event.keyCode=0;
}

if (esIExplorer) {
	//document.onkeypress = e_envio;
}

			</script>
		</form>
	</body>
</HTML>
