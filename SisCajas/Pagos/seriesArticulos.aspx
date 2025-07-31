<%@ Page Language="vb" AutoEventWireup="false" Codebehind="seriesArticulos.aspx.vb" Inherits="SisCajas.seriesArticulos"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>seriesArticulos</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script type="text/javascript">

/***********************************************
* Switch Content script- © Dynamic Drive (www.dynamicdrive.com)
* This notice must stay intact for legal use. Last updated Oct 21st, 2003.
* Visit http://www.dynamicdrive.com/ for full source code
***********************************************/

var enablepersist="on" //Enable saving state of content structure using session cookies? (on/off)
var collapseprevious="yes" //Collapse previously open content when opening present? (yes/no)

if (document.getElementById){
document.write('<style type="text/css">')
document.write('.switchcontent{display:none;}')
document.write('</style>')
}
function getElementbyClass(classname){
	ccollect=new Array()
	var inc=0
	var alltags=document.all? document.all : document.getElementsByTagName("*")
	for (i=0; i<alltags.length; i++){
	if (alltags[i].className==classname)
	ccollect[inc++]=alltags[i]
	}
}
function contractcontent(omit){
	var inc=0
	while (ccollect[inc]){
	if (ccollect[inc].id!=omit)
	ccollect[inc].style.display="none"
	inc++
	}
}
function expandcontent(cid){
	if (typeof ccollect!="undefined"){
	if (collapseprevious=="yes")
	contractcontent(cid)
	document.getElementById(cid).style.display=(document.getElementById(cid).style.display!="block")? "block" : "none"
	}
}
function revivecontent(){
	contractcontent("omitnothing")
	selectedItem=getselectedItem()
	selectedComponents=selectedItem.split("|")
	for (i=0; i<selectedComponents.length-1; i++)
	document.getElementById(selectedComponents[i]).style.display="block"
}
function get_cookie(Name) { 
	var search = Name + "="
	var returnvalue = "";
	if (document.cookie.length > 0) {
		offset = document.cookie.indexOf(search)
	if (offset != -1) { 
		offset += search.length
		end = document.cookie.indexOf(";", offset);
	if (end == -1) end = document.cookie.length;
		returnvalue=unescape(document.cookie.substring(offset, end))
	}
	}
	return returnvalue;
}
function getselectedItem(){
	if (get_cookie(window.location.pathname) != ""){
		selectedItem=get_cookie(window.location.pathname)
		return selectedItem
	}
	else
		return ""
}
function saveswitchstate(){
	var inc=0, selectedItem=""
	while (ccollect[inc]){
	if (ccollect[inc].style.display=="block")
		selectedItem+=ccollect[inc].id+"|"
		inc++
	}
	document.cookie=window.location.pathname+"="+selectedItem
}
function do_onload(){
	getElementbyClass("switchcontent")
	if (enablepersist=="on" && typeof ccollect!="undefined")
	revivecontent()
}
if (window.addEventListener)
	window.addEventListener("load", do_onload, false)
else if (window.attachEvent)
	window.attachEvent("onload", do_onload)
else if (document.getElementById)
	window.onload=do_onload
if (enablepersist=="on" && document.getElementById)
	window.onunload=saveswitchstate
		</script>
		<script language="JavaScript">
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

function f_VerificaAgregar() {

	if (!ValidaNumero('document.frmPrincipal.txtCDesde','el campo Desde ',false)) return false;
	if (!ValidaNumero('document.frmPrincipal.txtCHasta','el campo Hasta ',true)) return false;
	
	if (trim(document.frmPrincipal.txtCHasta.value)!="") {
		if (parseFloat(document.frmPrincipal.txtCDesde.value) > parseFloat(document.frmPrincipal.txtCHasta.value)){
			alert("Valor del campo Desde debe ser menor o igual que el Campo Hasta")
			return false;
		}
		if (eval(document.frmPrincipal.txtCHasta.value - document.frmPrincipal.txtCDesde.value) > 999){
			alert("Rango no debe ser mayor de 1000 series consecutivas")
			return false;
		}
	}
	return true;
}
//-->
		</script>
		<meta http-equiv="pragma" content="no-cache">
	</HEAD>
	<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="850" border="0">
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
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="90%" align="center" border="0">
										<tr>
											<td class="TituloRConsulta" align="center" colSpan="4" height="30">Venta Rapida - 
												Series</td>
										</tr>
										<tr>
											<td>
												<div class="frame2" style="Z-INDEX: 1; BORDER-BOTTOM: 1px; POSITION: relative; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 775px; HEIGHT: 170px; BORDER-TOP: 1px; BORDER-RIGHT: 1px"><asp:datagrid id="dgSeries" runat="server" BorderColor="White" AutoGenerateColumns="False" CssClass="Arial12b"
														Width="528px" BorderWidth="1px" CellPadding="1" CellSpacing="1">
														<AlternatingItemStyle Height="22px" BackColor="#E9EBEE"></AlternatingItemStyle>
														<ItemStyle Height="22px" CssClass="Arial11B" BackColor="#DDDEE2"></ItemStyle>
														<Columns>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" Width="4%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton id="iBtnDelete" runat="server" ImageUrl="../../images/iconos/ico_Eliminar.gif"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn DataField="Pos" HeaderText="Pos">
																<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" Width="14%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="Desde" HeaderText="Desde">
																<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" Width="24%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="Hasta" HeaderText="Hasta">
																<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" Width="24%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="Cantidad" HeaderText="Cantidad">
																<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" Width="24%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
														</Columns>
													</asp:datagrid></div>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="5"></td>
							</tr>
						</table>
						<br>
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="790" align="center">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td class="TituloRConsulta" align="center" colSpan="4" height="30"><b>Nueva Serie</b></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td>
												<table borderColor="#d0d8f0" cellSpacing="0" cellPadding="0" width="780" align="center"
													bgColor="#ffffff" border="0">
													<tr>
														<td height="4"></td>
													</tr>
													<tr>
														<td height="18">
															<table cellSpacing="1" cellPadding="0" border="0">
																<tr class="Arial12b">
																	<td width="200"><font color="#ff0000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Serie</b></font></td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td height="4"></td>
													</tr>
													<tr>
														<td>
															<table cellSpacing="2" cellPadding="0" width="100%" border="0">
																<tr>
																	<td width="25">&nbsp;</td>
																	<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Desde :
																	</td>
																	<td class="Arial12b" width="180"><asp:textbox id="txtCDesde" runat="server" CssClass="clsInputEnable" Width="127px"></asp:textbox></td>
																	<td width="25">&nbsp;</td>
																	<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Hasta :
																	</td>
																	<td class="Arial12b" width="180"><asp:textbox id="txtCHasta" runat="server" CssClass="clsInputEnable" Width="127px"></asp:textbox></td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td height="8"></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="360" align="center"
							border="1">
							<tr>
								<td align="center"><asp:button id="btnAgregar" runat="server" CssClass="BotonOptm" Width="100px" Text="Agregar"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="btnContinuar" runat="server" CssClass="BotonOptm" Width="100px" Text="Continuar"></asp:button></td>
							</tr>
						</table>
						<br>
						<br>
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
			<script language="javascript">
	function f_Agregar(){
	    event.returnValue = false;
		if (f_VerificaAgregar()) {
			/*document.frmPrincipal.codOperacion.value = "05";
			document.frmPrincipal.action='Operaciones.asp';
			document.frmPrincipal.submit();*/
			event.returnValue = true;
		}
	}

	function f_Eliminar(orden) {
		document.frmPrincipal.codOrden.value = orden;
		document.frmPrincipal.codOperacion.value = "06";
		document.frmPrincipal.action='Operaciones.asp';
		document.frmPrincipal.submit();
	}

	function f_Continuar() {
		document.frmPrincipal.codOperacion.value = "07";
		document.frmPrincipal.action='Operaciones.asp';
		document.frmPrincipal.submit();
	}
			</script>
		</form>
	</body>
</HTML>
