<%@ Page Language="vb" AutoEventWireup="false" Codebehind="reniec.aspx.vb" Inherits="SisCajas.reniec"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>reniec</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript">
<!--
	document.oncontextmenu = function()
	{
		return false;
	}

	if(document.layers)
	{
		window.onmousedown = function(e)
		{
			if(e.target==document)
				return false;
		}
	}

	document.onkeypress = function()
	{
		if (event.keyCode==13) f_Buscar();
	}
//-->
		</script>
		<script language="JavaScript">
<!--

	var gbFlag=0;

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

	function f_Validar() {
		with(frmPrincipal) {
			if (cboCriterio.value == <%=K_TX_DNI%>) {
				if (eval(txtNumDocumento.value)==0) {
					alert('DNI no válido, no existe en RENIEC')
					txtNumDocumento.value = "";
					txtNumDocumento.focus();
					return false;
				}
				else
					if (!ValidaDNI('document.frmPrincipal.txtNumDocumento','numero documento no valido',false)) return false;
			}
			else {
				if (!ValidaNombre('document.frmPrincipal.txtApePaterno','el campo Apellido Paterno ',false)) return false;
				if (!ValidaNombre('document.frmPrincipal.txtApeMaterno','el campo Apellido Materno ',false)) return false;
			}
		}
		return true;
	}

	function f_Buscar() {
		if ( f_Validar() ) {
			with(frmPrincipal) {
				if (cboCriterio.value == <%=K_TX_DNI%>) {
					btnBuscar.disabled = true;
					btnLimpiar.disabled = true;
					ifraCargaDatos.document.frm.hidNumDoc.value = txtNumDocumento.value;
					ifraCargaDatos.document.frm.hidTXReniec.value = <%=K_TX_DNI%>;
					ifraCargaDatos.document.frm.hidAccion.value = <%=K_BUSCAR%>;
					ifraCargaDatos.document.frm.submit();
				}
				else {
					hidTXReniec.value = cboCriterio.value;
					hidAccion.value = <%=K_BUSCAR%>;
					submit();
				}
			}
		}
	}

	function f_MuestraCriterio() {
		var bBuscarPorDNI = (frmPrincipal.cboCriterio.value == <%=K_TX_DNI%>);
		spPorNroDoc.style.display = (bBuscarPorDNI?'':'none');
		spPorAprox.style.display = (bBuscarPorDNI?'none':'');
		if (bBuscarPorDNI) frmPrincipal.txtNumDocumento.focus();
		else frmPrincipal.txtNombre.focus();
	}

	function f_LimpiaControl() {
	var nIndice = 0;
		with(frmPrincipal) {
			nIndice = cboCriterio.selectedIndex;
			reset();
			cboCriterio.selectedIndex = nIndice;
		}
	}

	function f_ValidaLetra()
	{
		var result = ( event.keyCode >= 65 && event.keyCode <= 90 );
		result |= ( result || ( event.keyCode >= 97 && event.keyCode <= 122 ) );
		result |= ( result || ( event.keyCode == 241 || event.keyCode == 209 || event.keyCode == 32 ) );
		result |= ( result || ( event.keyCode == 39 ) );
		f_Mayuscula();
		if (!result) event.returnValue = false;
	}

	function f_Mayuscula()
	{
		if ( (event.keyCode > 96 && event.keyCode < 123) || (event.keyCode==241) || (event.keyCode==250) || (event.keyCode==243) || (event.keyCode==237) || (event.keyCode==233) || (event.keyCode==225) )
			event.keyCode = (event.keyCode - 32);
	}

//-->
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<input id="hidAccion" type="hidden" value="0" name="hidAccion"> <input id="hidTXReniec" type="hidden" name="hidTXReniec">
			<table cellSpacing="0" cellPadding="0" width="820" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" align="center" width="820">
						<table height="14" cellSpacing="0" cellPadding="0" width="810" border="0">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="810">
							<tr>
								<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
									width="98%">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="TituloRConsulta" align="center" height="30">Evaluación Crediticia - 
												Autenticación</td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td>
												<table cellSpacing="2" cellPadding="0" border="0">
													<tr>
														<td class="Arial12br" colSpan="5" height="26">&nbsp;&nbsp;&nbsp;Reniec
														</td>
													</tr>
													<tr>
														<td class="Arial12b" width="96">&nbsp;&nbsp;&nbsp;Buscar por :</td>
														<td>
															<asp:DropDownList id="cboCriterio" runat="server" CssClass="clsSelectEnable" Width="185px"></asp:DropDownList>
														</td>
													</tr>
													<tr>
														<td colSpan="2" height="4"></td>
													</tr>
												</table>
												<span id="spPorNroDoc">
													<table cellSpacing="2" cellPadding="0" border="0">
														<tr>
															<td class="Arial12b" width="96">&nbsp;&nbsp;&nbsp;Nro. de DNI :</td>
															<td class="Arial12b"><input class="clsInputEnable" onkeypress="if (event.keyCode < 45 || event.keyCode > 57) event.returnValue = false;"
																	tabIndex="12" type="text" maxLength="8" size="18" name="txtNumDocumento"></td>
														</tr>
													</table>
												</span><span id="spPorAprox" style="DISPLAY: none">
													<table cellSpacing="2" cellPadding="0" border="0">
														<tr>
															<td class="Arial12b" width="140">&nbsp;&nbsp;&nbsp;Nombre :</td>
															<td class="Arial12b"><input class="clsInputEnable" onkeypress="f_ValidaLetra();" tabIndex="14" type="text" maxLength="40"
																	size="40" name="txtNombre"></td>
														</tr>
														<tr>
															<td class="Arial12b" width="140">&nbsp;&nbsp;&nbsp;Apellido Paterno (*) :</td>
															<td class="Arial12b"><input class="clsInputEnable" onkeypress="f_ValidaLetra();" tabIndex="15" type="text" maxLength="20"
																	size="40" name="txtApePaterno"></td>
														</tr>
														<tr>
															<td class="Arial12b" width="140">&nbsp;&nbsp;&nbsp;Apellido Materno (*) :</td>
															<td class="Arial12b"><input class="clsInputEnable" onkeypress="f_ValidaLetra();" tabIndex="16" type="text" maxLength="20"
																	size="40" name="txtApeMaterno"></td>
														</tr>
													</table>
													<table cellSpacing="2" cellPadding="0" border="0">
														<tr>
															<td>&nbsp;</td>
														</tr>
														<tr>
															<td class="Arial12b"><small>&nbsp;&nbsp;&nbsp;Los campos en (*) son los campos 
																	obligatorios que se deben llenar en el formulario.</small></td>
														</tr>
													</table>
												</span>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td><img height="8" src="" width="1" border="0"></td>
							</tr>
						</table>
						<br>
						<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="360">
							<tr>
								<td align="center"><input class="BotonOptm" style="WIDTH: 100px" onclick="f_Buscar();" type="button" value="Buscar"
										name="btnBuscar">&nbsp;&nbsp; <input class="BotonOptm" style="WIDTH: 100px" onclick="f_LimpiaControl();" type="button"
										value="Limpiar" name="btnLimpiar">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div style="VISIBILITY: hidden">
			    <iframe id="ifraCargaDatos" src="./reniec_CargaDatos.aspx" width="0" height="0">
			    </iframe>
			</div>
		</form>
		<script language="JavaScript">f_MuestraCriterio();</script>
		
	</body>
</HTML>
