<%@ Page Language="vb" AutoEventWireup="false" Codebehind="datosCliente.aspx.vb" Inherits="SisCajas.datosCliente" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>datosCliente</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../librerias/ubigeo.js"></script>
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../librerias/msrsclient.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="JavaScript">


var serverURL = "ifrm_Combos.aspx"; //"http://localhost/SampleRemoteScriptingWeb/MyServerSidePage.aspx";
var blnResult;
var strHTML;
var strSel;

function f_CambiaDepar()
{
  msrsPOST = true;
  strSel = document.frmPrincipal.cboProv.value
  RSExecute(serverURL,"CargaProv",document.frmPrincipal.cboDepa.value,cb,cbError,"X");
  
  f_CambiaProv();
}


function f_CambiaProv()
{
  msrsPOST = true;
  strSel = document.frmPrincipal.cboDstr.value
  RSExecute(serverURL,"CargaDist",document.frmPrincipal.cboDepa.value,document.frmPrincipal.cboProv.value,cb2,cbError,"X");
   
}

function cb(co) {

if (co.return_value) {
			document.getElementById("tdProv").innerHTML = co.return_value;
			document.frmPrincipal.cboProv.value = strSel
		}
  
  }
  
function cb2(co) {

if (co.return_value) {
			document.getElementById("tdDist").innerHTML = co.return_value;
			document.frmPrincipal.cboDstr.value = strSel
		}
  
  }

function cbError(co) {
		alert("Error callback fired.");
		if (co.message) {
			alert("Context:" + co.context + "\nError: " + co.message);
		}
	  }







function f_Cancelar() { 
		if(document.frmPrincipal.pagAnt.value=="../VentaR/"){
			document.frmPrincipal.action = document.frmPrincipal.pagAnt.value + 'ventaArticulos.asp';
		} else {
			document.frmPrincipal.action = document.frmPrincipal.pagAnt.value + 'busqCliente.asp';
		}
		document.frmPrincipal.submit();
}
function f_Grabar() {
		event.returnValue = false;
		if (f_Validar()) {
			event.returnValue = true;
		}
}

function f_Vacio(cadena) {
    var blanco = " \n\t" + String.fromCharCode(13); // blancos                         
    var i                             
    var es_vacio; 
    for(i = 0, es_vacio = true; (i < cadena.length) && es_vacio; i++) // INICIO
      es_vacio = blanco.indexOf(cadena.charAt(i)) != - 1;
    return(es_vacio);  
}

function f_ValidaMail(correo){
	var cad="!#$%&/()=?'\¡¿~[{^}]`;,:><|~ ¡¢£¤¥¦§¨©ª«¬®¯°±²³´µ¶·¸¹º»¼½¾ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàááâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿŒœŠšŸŽžƒˆ˜";
	if (correo.indexOf("@")<0) { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; }
	if (correo.indexOf("@",correo.indexOf("@")+1)>=0) { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; }
	if (correo.indexOf("@.")>=0) { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; } 
	if (correo.indexOf(".@")>=0) { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; } 
	if (correo.indexOf("@-")>=0) { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; } 
	if (correo.indexOf("-@")>=0) { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; } 
	if (correo.indexOf("@_")>=0) { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; } 
	if (correo.indexOf("_@")>=0) { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; } 
	if (correo.indexOf("..")>=0) { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; } 
	if (correo.indexOf("--")>=0) { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; } 
	if (correo.indexOf("__")>=0) { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; } 
	if (correo.charAt(correo.length-1)=="@") { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; }
	if (correo.charAt(0)=="@") { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; } 
	if (correo.charAt(correo.length-1)==".") { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; }
	if (correo.charAt(0)==".") { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; } 
	if (correo.charAt(correo.length-1)=="-") { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; }
	if (correo.charAt(0)=="-") { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; } 
	if (correo.charAt(correo.length-1)=="_") { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; }; 
	if (correo.charAt(0)=="_") { alert("Dirección de e-mail inválida"); document.frmPrincipal.txtEmail.focus();return false; } 
	for (i=0;i<cad.length;i++) {
		if(correo.indexOf(cad.charAt(i))>0) {
			alert("Dirección de e-mail inválida");
			document.frmPrincipal.txtEmail.focus();
			return false;
		}
	}
	return true;
}

function f_TelPag(){

	if (document.frmPrincipal.chkTelPag.checked){
		document.getElementById("TelPag1").style.display = "block"
		document.getElementById("TelPag2").style.display = "none"
	}
	else{
		document.getElementById("TelPag1").style.display = "none"
		document.getElementById("TelPag2").style.display = "block"
	}
}

function f_Prefijo(){
	frmPrincipal.txtPrefijo.value = frmPrincipal.cboPref.options[frmPrincipal.cboPref.selectedIndex].value;
	//alert (frmPrincipal.txtPrefijo.value);
}


		</script>
		<% If Trim(Request.Item("strTipDoc")) = 6 then %>
		<script language="JavaScript">

function f_Validar() {// valida campos
event.returnValue = false;
	if (!ValidaAlfanumerico('document.frmPrincipal.txtRSocial','el campo Nombre ',false)) return false;
	if (!checkEmail('document.frmPrincipal.txtEmail','el campo e-Mail es incorrecto',true)) return false;
	
	if (document.frmPrincipal.txtPrefijo.value != 1){
		if (!ValidaTelefonoProv('document.frmPrincipal.txtTelefono','el campo número de Teléfono ',false)) return false;
	}
	else{
		if (document.frmPrincipal.chkTelPag.checked){
			if (!ValidaTelefono('document.frmPrincipal.txtTelefono','el campo número de Teléfono ',false)) return false;
		}
		else{	
			if (!ValidaTelefono('document.frmPrincipal.txtTelefono','el campo número de Teléfono ',false)) return false;
		}
	}
	if (!ValidaTelefono('document.frmPrincipal.txtFax','el campo número de Fax ',true)) return false;

	if (!ValidaCombo('document.frmPrincipal.cboPrefijo','el campo Prefijo ',false)) return false;
	if (!ValidaDireccion('document.frmPrincipal.txtDireccion','el campo Dirección ',false)) return false;
	
	//if (!ValidaCombo('document.frmPrincipal.cboDepa','el campo Departamento ',false)) return false;
	//if (!ValidaCombo('document.frmPrincipal.cboProv','el campo Provincia ',false)) return false;
	//if (!ValidaCombo('document.frmPrincipal.cboDstr','el campo Distrito ',false)) return false;
	
	
event.returnValue = true;
	return true;
};

		</script>
		<% else %>
		<script language="JavaScript">

function f_Validar() {// valida campos
	var cadena,ano,mes,dia,anonuevo,fechanac;
	var fecha,anosist,messist,diasist,fechasist;
	event.returnValue = false;
	if (!ValidaNombre('document.frmPrincipal.txtNombre','el campo Nombre ',false)) return false;
	if (!ValidaNombre('document.frmPrincipal.txtAPaterno','el campo Apellido Paterno ',false)) return false;
	if (!ValidaNombre('document.frmPrincipal.txtAMaterno','el campo Apellido Materno ',false)) return false;

	//if (!ValidaFechaA('document.frmPrincipal.txtFecNacimiento',false)) return false;
	if (!FechaMayorSistema('document.frmPrincipal.txtFecNacimiento',' la fecha de nacimiento ')) return false;
	//if (!ValidaCombo('document.frmPrincipal.cboSexo','el campo Sexo ',false)) return false;
	//if (!ValidaCombo('document.frmPrincipal.cboTitulo','el campo Título ',false)) return false;
	if (!checkEmail('document.frmPrincipal.txtEmail','el campo e-Mail es incorrecto',true)) return false;
	
	//if (document.frmPrincipal.txtPrefijo.value != 1){
	//	if (!ValidaTelefonoProv('document.frmPrincipal.txtTelefono','el campo número de Teléfono ',false)) return false;
	//}
	//else{
	//	if (document.frmPrincipal.chkTelPag.checked){
	//		if (!ValidaTelefono('document.frmPrincipal.txtTelefono','el campo número de Teléfono ',false)) return false;
	//	}
	//	else{	
	//		if (!ValidaTelefono('document.frmPrincipal.txtTelefono','el campo número de Teléfono ',false)) return false;
	//	}
	//}
	if (!ValidaTelefono('document.frmPrincipal.txtFax','el campo número de Fax ',true)) return false;
	if (!ValidaNumero('document.frmPrincipal.txtCFamiliar','el campo Carga Familiar ',true)) return false;
	//if (!ValidaCombo('document.frmPrincipal.cboTitulo','el campo Título ',false)) return false;

	if (!ValidaNombre('document.frmPrincipal.txtNConyuge','el campo Nombre del Cónyuge ',true)) return false;
	if (document.frmPrincipal.txtNConyuge.value.length>0) {
		if (document.frmPrincipal.cboECivil.value!='1') {
			alert("Debe seleccionar el estado civil casado para ingresar cónyuge");
			return false;
		}
	}
	//if (!ValidaCombo('document.frmPrincipal.cboECivil','el campo Estado Civil ',false)) return false;
	//if (!ValidaCombo('document.frmPrincipal.cboPrefijo','el campo Prefijo ',false)) return false;
	//if (!ValidaDireccion('document.frmPrincipal.txtDireccion','el campo Dirección ',false)) return false;
	if (!ValidaCombo('document.frmPrincipal.cboDepa','el campo Departamento ',false)) return false;
	if (!ValidaCombo('document.frmPrincipal.cboProv','el campo Provincia ',false)) return false;
	if (!ValidaCombo('document.frmPrincipal.cboDstr','el campo Distrito ',false)) return false;

//CARIAS: se comenta
/*	cadena = document.frmPrincipal.txtFecNacimiento.value;
	ano = cadena.substr(6,4);
	mes = cadena.substr(3,2);
	dia = cadena.substr(0,2);
	anonuevo = parseInt(ano) + 18; 
	fechanac = anonuevo + mes + dia;
	fecha = '<%=Session("FechaAct")%>';
	
	
	anosist = fecha.substr(6,4);
	messist = fecha.substr(3,2);
	diasist = fecha.substr(0,2);
	fechasist = anosist + messist + diasist;*/
	//if (fechasist < fechanac)
	//	alert("Se está registrando datos de un cliente menor de edad");
	event.returnValue = true;
	return true;  
};

function f_CambiaTitulo(){
   frmPrincipal.cboSexo.selectedIndex=0
   if (frmPrincipal.cboTitulo.value==''){
      frmPrincipal.cboSexo.selectedIndex=0;
   }
   if (frmPrincipal.cboTitulo.value=='1'){
      frmPrincipal.cboSexo.selectedIndex=1;
   }
   if (frmPrincipal.cboTitulo.value=='5'){
      frmPrincipal.cboSexo.selectedIndex=1;
   }
   if (frmPrincipal.cboTitulo.value=='2' || frmPrincipal.cboTitulo.value=='3'){
      frmPrincipal.cboSexo.selectedIndex=2;
   }
}

<% end if %>



		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<div id="overDiv" style="Z-INDEX: 1; POSITION: absolute; WIDTH: 100px"></div>
			<table cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="820">
						<table height="14" cellSpacing="0" cellPadding="0" width="820" border="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="790" align="center">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="TituloRConsulta" align="center" colSpan="4" height="30">Mantenimiento 
												Cliente</td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="800" border="0">
										<tr>
											<td>
												<table cellSpacing="2" cellPadding="0" width="100%" border="0">
													<tr>
														<td class="Arial12br" colSpan="5" height="26">&nbsp;&nbsp;&nbsp;Datos del Cliente
														</td>
													</tr>
													<tr>
														<td class="Arial12b" width="220">&nbsp;&nbsp;&nbsp;Tipo de Doc. Identidad :</td>
														<td class="Arial12b" width="100"><asp:textbox id="txtTipDocumento" runat="server" ReadOnly="True" Width="187px" CssClass="clsInputDisable"></asp:textbox></td>
														<td width="25" height="25">&nbsp;</td>
														<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;&nbsp;Nro. de Doc. Identidad :</td>
														<td class="Arial12b" width="250">&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:textbox id="txtNumDocumento" runat="server" ReadOnly="True" Width="187px" CssClass="clsInputDisable"></asp:textbox></td>
													</tr>
													<tr>
														<td class="Arial12b" width="220">&nbsp;&nbsp;&nbsp;Tipo de Cliente :</td>
														<td class="Arial12b" width="100" colSpan="4"><asp:textbox id="txtTipoCliente" runat="server" ReadOnly="True" Width="187px" CssClass="clsInputDisable"></asp:textbox></td>
													</tr>
													<tr id="trRazSoc" runat="server">
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Razón Social (*):</td>
														<td class="Arial12b" colSpan="4"><asp:textbox id="txtRSocial" runat="server" Width="575px" CssClass="clsInputEnable"></asp:textbox></td>
													</tr>
													<tr id="trNomApPat" runat="server">
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Nombre (*):</td>
														<td class="Arial12b"><asp:textbox id="txtNombre" runat="server" Width="187px" CssClass="clsInputEnable"></asp:textbox></td>
														<td width="25" height="22">&nbsp;</td>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;&nbsp;Apellido Paterno (*):</td>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:textbox id="txtAPaterno" runat="server" Width="187px" CssClass="clsInputEnable"></asp:textbox></td>
													</tr>
													<tr id="trApMat" runat="server">
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Apellido Materno (*):</td>
														<td class="Arial12b" colSpan="4"><asp:textbox id="txtAMaterno" runat="server" Width="187px" CssClass="clsInputEnable"></asp:textbox></td>
													</tr>
													<tr id="trTitulo" runat="server" style="DISPLAY: none; VISIBILITY: hidden">
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Título:</td>
														<td class="Arial12b" colSpan="4"><asp:dropdownlist id="cboTitulo" runat="server" CssClass="clsSelectEnableC">
																<asp:ListItem></asp:ListItem>
																<asp:ListItem Value="1">Sr.</asp:ListItem>
																<asp:ListItem Value="2">Sra.</asp:ListItem>
																<asp:ListItem Value="3">Srta.</asp:ListItem>
																<asp:ListItem Value="4">Cia.</asp:ListItem>
																<asp:ListItem Value="5">Dr.</asp:ListItem>
															</asp:dropdownlist></td>
													</tr>
													<tr id="trFecNac" runat="server" style="DISPLAY: none; VISIBILITY: hidden">
														<td class="Arial12b">&nbsp;<b>&nbsp;&nbsp;</b>Fecha de Nac.:</td>
														<td class="Arial12b"><asp:textbox id="txtFecNacimiento" runat="server" Width="166px" CssClass="clsInputEnable"></asp:textbox>&nbsp;
															<A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmPrincipal.txtFecNacimiento');"><IMG src="../../images/botones/btn_Calendario.gif" border="0">
															</A>
														</td>
														<td>&nbsp;</td>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;&nbsp;Sexo:</td>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;
															<asp:dropdownlist id="cboSexo" runat="server" CssClass="clsSelectEnableC" Enabled="False">
																<asp:ListItem></asp:ListItem>
																<asp:ListItem Value="M">Masculino</asp:ListItem>
																<asp:ListItem Value="F">Femenino</asp:ListItem>
															</asp:dropdownlist></td>
													</tr>
													<tr style="DISPLAY: none; VISIBILITY: hidden">
														<td colSpan="5">
															<table>
																<tr>
																	<td class="Arial12b" width="250">&nbsp; Consulta a Páginas Blancas / Amarillas:</td>
																	<td><asp:checkbox id="chkTelPag" runat="server" Width="8px" Text=" "></asp:checkbox></td>
																</tr>
															</table>
														</td>
													</tr>
													<tr style="DISPLAY: none; VISIBILITY: hidden">
														<td colSpan="5">
															<table border="0">
																<tr>
																	<td class="Arial12b" width="165">&nbsp; Prefijo Teléfono Legal:</td>
																	<td class="Arial12b"><asp:dropdownlist id="cboPref" runat="server" Width="140px" CssClass="clsSelectEnableC"></asp:dropdownlist></td>
																	<td class="Arial12b"><asp:textbox id="txtPrefijo" runat="server" ReadOnly="True" Width="48px" CssClass="clsInputDisable"></asp:textbox></td>
																	<td class="Arial12b" width="23">&nbsp;</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td>
															<div class="dropcontent" id="TelPag1">
																<table>
																	<tr>
																		<td class="Arial12b" width="170">&nbsp; Teléfono Legal:</td>
																	</tr>
																</table>
															</div>
														<td class="Arial12b"><asp:textbox id="txtTelefono" runat="server" Width="187px" CssClass="clsInputEnable"></asp:textbox></td>
														<td class="Arial12b">&nbsp;</td>
														<td class="Arial12b" height="25">&nbsp;&nbsp;&nbsp;&nbsp;Fax :</td>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:textbox id="txtFax" runat="server" Width="187px" CssClass="clsInputEnable"></asp:textbox></td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Email :</td>
														<td class="Arial12b"><asp:textbox id="txtEmail" runat="server" Width="187px" CssClass="clsInputEnable"></asp:textbox></td>
														<td class="Arial12b" colSpan="3">&nbsp;</td>
													</tr>
													<tr id="trEstCiv" runat="server" style="DISPLAY: none; VISIBILITY: hidden">
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Estado Civil:</td>
														<td class="Arial12b"><asp:dropdownlist id="cboECivil" runat="server" CssClass="clsSelectEnableC"></asp:dropdownlist></td>
														<td class="Arial12b">&nbsp;</td>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;&nbsp;Carga Familiar :</td>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:textbox id="txtCFamiliar" runat="server" Width="48px" CssClass="clsInputEnable"></asp:textbox></td>
													</tr>
													<tr id="trConyuge" runat="server" style="DISPLAY: none; VISIBILITY: hidden">
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Nombre del Cónyuge :</td>
														<td class="Arial12b" colSpan="4"><asp:textbox id="txtNConyuge" runat="server" Width="583px" CssClass="clsInputEnable"></asp:textbox></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="770" border="0">
										<tr>
											<td>
												<table cellSpacing="2" cellPadding="0" width="100%" border="0">
													<tr>
														<td class="Arial12br" colSpan="5" height="26">&nbsp;&nbsp;&nbsp;Dirección Legal del 
															Cliente
														</td>
													</tr>
													<tr>
														<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Prefijo:</td>
														<td class="Arial12b" width="190"><asp:dropdownlist id="cboPrefijo" runat="server" CssClass="clsSelectEnableC">
																<asp:ListItem></asp:ListItem>
																<asp:ListItem Value="AA.HH">AA.HH</asp:ListItem>
																<asp:ListItem Value="Av.">Av.</asp:ListItem>
																<asp:ListItem Value="Calle">Calle</asp:ListItem>
																<asp:ListItem Value="Coop.">Coop.</asp:ListItem>
																<asp:ListItem Value="Jr.">Jr.</asp:ListItem>
																<asp:ListItem Value="Prolong.">Prolong.</asp:ListItem>
																<asp:ListItem Value="Psje.">Psje.</asp:ListItem>
																<asp:ListItem Value="Urb.">Urb.</asp:ListItem>
																<asp:ListItem Value="SUBIDA">Subida</asp:ListItem>
																<asp:ListItem Value="BAJADA">Bajada</asp:ListItem>
																<asp:ListItem Value="ASOC.">ASOC.</asp:ListItem>
															</asp:dropdownlist></td>
														<td align="left" colSpan="3">&nbsp;</td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Dirección:</td>
														<td class="Arial12b" colSpan="4"><asp:textbox id="txtDireccion" runat="server" Width="579px" CssClass="clsInputEnable"></asp:textbox></td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Referencia:</td>
														<td class="Arial12b" colSpan="4"><asp:textbox id="txtReferencia" runat="server" Width="579px" CssClass="clsInputEnable"></asp:textbox></td>
													</tr>
													<tr>
														<td class="Arial12b" style="HEIGHT: 17px">&nbsp;&nbsp;&nbsp;Departamento:</td>
														<td class="Arial12b" style="HEIGHT: 17px"><asp:dropdownlist id="cboDepa" runat="server" CssClass="clsSelectEnableC"></asp:dropdownlist></td>
														<td style="HEIGHT: 17px" width="25">&nbsp;</td>
														<td class="Arial12b" style="HEIGHT: 17px" width="170">&nbsp;&nbsp;&nbsp;Provincia:</td>
														<td class="Arial12b" id="tdProv" style="HEIGHT: 17px"><asp:dropdownlist id="cboProv" runat="server" CssClass="clsSelectEnableC"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Distrito:</td>
														<td class="Arial12b" id="tdDist"><asp:dropdownlist id="cboDstr" runat="server" CssClass="clsSelectEnableC"></asp:dropdownlist></td>
														<td align="left">&nbsp;</td>
														<td class="Arial12b">&nbsp;&nbsp;&nbsp;Código Postal :</td>
														<td class="Arial12b"><asp:textbox id="txtCodPostal" runat="server" ReadOnly="True" Width="187px" CssClass="clsInputDisable"></asp:textbox></td>
													</tr>
													<tr>
														<td colSpan="6" height="14"></td>
													</tr>
													<tr>
														<td class="Arial12b" colSpan="5">&nbsp;&nbsp;&nbsp;Los campos en (*) son los campos 
															obligatorios que se deben llenar en el formulario.
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
						<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="400" align="center">
							<tr>
								<td align="center"><asp:button id="btnGrabar" runat="server" Width="100px" CssClass="BotonOptm" Text="Grabar"></asp:button>&nbsp;&nbsp;&nbsp;
									<asp:button id="Grabar" runat="server" Width="100px" CssClass="BotonOptm" Text="Cancelar"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="JavaScript" type="text/javascript">
var esNavegador, esIExplorer;
  
esNavegador = (navigator.appName == "Netscape") ? true : false;
esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

function e_mayuscula() {
	if ((event.keyCode>96&&event.keyCode<123)||(event.keyCode==241)||(event.keyCode==250)||(event.keyCode==243)||(event.keyCode==237)||(event.keyCode==233)||(event.keyCode==225))
		event.keyCode=event.keyCode-32;
}

function e_minuscula(){
	if (event.keyCode>64&&event.keyCode<91)
		event.keyCode=event.keyCode+32;
}


<% If Trim(Request.Item("strTipDoc")) = 6 then %>

if (esIExplorer) {
	window.document.frmPrincipal.txtRSocial.onkeypress=e_mayuscula;
	window.document.frmPrincipal.txtDireccion.onkeypress=e_mayuscula;
	window.document.frmPrincipal.txtReferencia.onkeypress=e_mayuscula;
}

<% else %>

if (esIExplorer) {
	window.document.frmPrincipal.txtNombre.onkeypress=e_mayuscula;
	window.document.frmPrincipal.txtAPaterno.onkeypress=e_mayuscula;
	window.document.frmPrincipal.txtAMaterno.onkeypress=e_mayuscula;
	window.document.frmPrincipal.txtNConyuge.onkeypress=e_mayuscula;
	window.document.frmPrincipal.txtDireccion.onkeypress=e_mayuscula;
	window.document.frmPrincipal.txtReferencia.onkeypress=e_mayuscula;
}
<% end if %>


//======================================================================================
// 20040921:  Percy Silva
//======================================================================================
/*
function fn_Direccion_Cliente()
{
	var strCodPrefijo = document.frmPrincipal.cboPrefijo.options[document.frmPrincipal.cboPrefijo.selectedIndex].value;
	var strPrefijo = document.frmPrincipal.cboPrefijo.options[document.frmPrincipal.cboPrefijo.selectedIndex].text;
	var strCodDepa = document.frmPrincipal.cboDepa.options[document.frmPrincipal.cboDepa.selectedIndex].value; 	
	var strDepa = document.frmPrincipal.cboDepa.options[document.frmPrincipal.cboDepa.selectedIndex].text; 	
	var strCodProv = document.frmPrincipal.cboProv.options[document.frmPrincipal.cboProv.selectedIndex].value;
	var strProv = document.frmPrincipal.cboProv.options[document.frmPrincipal.cboProv.selectedIndex].text;
	var strCodDstr = document.frmPrincipal.cboDstr.options[document.frmPrincipal.cboDstr.selectedIndex].value;
	var strDstr = document.frmPrincipal.cboDstr.options[document.frmPrincipal.cboDstr.selectedIndex].text;

	while(document.frmPrincipal.cboPrefijo.length > 0)
		document.frmPrincipal.cboPrefijo.options[0] = null;
	while(document.frmPrincipal.cboDepa.length > 0)
		document.frmPrincipal.cboDepa.options[0] = null;
	while(document.frmPrincipal.cboProv.length > 0)
		document.frmPrincipal.cboProv.options[0] = null;
	while(document.frmPrincipal.cboDstr.length > 0)
		document.frmPrincipal.cboDstr.options[0] = null;

	var optPrefijo = new Option(strPrefijo, strCodPrefijo);
	document.frmPrincipal.cboPrefijo.options[0] = optPrefijo;
	var optDepa = new Option(strDepa, strCodDepa);
	document.frmPrincipal.cboDepa.options[0] = optDepa;
	var optProv = new Option(strProv, strCodProv);
	document.frmPrincipal.cboProv.options[0] = optProv;
	var optDist = new Option(strDstr, strCodDstr);
	document.frmPrincipal.cboDstr.options[0] = optDist;

	document.frmPrincipal.txtDireccion.readOnly = true;
	document.frmPrincipal.cboDepa.onchange = "";
	document.frmPrincipal.cboProv.onchange = "";
	document.frmPrincipal.cboDstr.onchange = "";

}


//======================================================================================
//======================================================================================
f_Prefijo();*/
f_CambiaDepar();
		</script>
	</body>
</HTML>
